// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Pdm.CompositionCopying.CompositionCopyingServerService
// Assembly: Intermech.Pdm.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EC8EF964-D01E-4AAA-8100-7A99DC670202
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Pdm.Server.dll

using Intermech.Interfaces;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using Intermech.Search.CompositionContexts;
using Intermech.Search.GroupAttributesChanging;
using Intermech.Search.Pdm.Instances;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;

#nullable disable
namespace Intermech.Search.Pdm.CompositionCopying;

public sealed class CompositionCopyingServerService : 
  LongLifeObject,
  ICompositionCopyingServerService
{
  private IInstancesServerService _instancesServerService;
  private IGroupAttributesChangingServerService _groupAttributesChangingServerService;

  public CompositionCopyingServerService(
    IInstancesServerService instancesServerService,
    IGroupAttributesChangingServerService groupAttributesChangingServerService)
  {
    if (instancesServerService == null)
      throw new ArgumentNullException(nameof (instancesServerService));
    if (groupAttributesChangingServerService == null)
      throw new ArgumentNullException(nameof (groupAttributesChangingServerService));
    this._instancesServerService = instancesServerService;
    this._groupAttributesChangingServerService = groupAttributesChangingServerService;
  }

  public DataTable FindComposition(Guid userSessionGuid, FindCompositionParams @params)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
      return @params != null && FindCompositionParams.Check(@params) ? this.FindComposition(@params) : throw new ArgumentException();
  }

  public ObjectBlank[] CreateBlanks(
    Guid userSessionGuid,
    long objectVersionID,
    long[] copyVersionIds)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
    {
      if (ObjectHelper.IsUnknownObjectVersionID(objectVersionID))
        throw new ArgumentException();
      return copyVersionIds == null || !ObjectHelper.IsAnyUnknownObjectVersionID((IEnumerable<long>) copyVersionIds) ? this.CreateBlanks(objectVersionID, copyVersionIds) : throw new ArgumentException();
    }
  }

  public bool CheckObjectReferenceAssociatedWithDocumentElement(
    Guid userSessionGuid,
    long documentVersionID)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
      return !ObjectHelper.IsUnknownObjectVersionID(documentVersionID) ? this.CheckObjectReferenceAssociatedWithDocumentElement(documentVersionID) : throw new ArgumentException();
  }

  public Tuple<ObjectBlank, string> CreateObject(Guid userSessionGuid, ObjectBlank blank)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
      return blank != null ? this.CreateObject(blank) : throw new ArgumentNullException(nameof (blank));
  }

  public string[] CreateComposition(
    Guid userSessionGuid,
    long projectVersionId,
    Tuple<long, long>[] composition)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
    {
      if (ObjectHelper.IsUnknownObjectVersionID(projectVersionId))
        throw new ArgumentException();
      return composition != null && composition.Length != 0 ? this.CreateComposition(projectVersionId, composition) : throw new ArgumentException();
    }
  }

  public void RemoveObjects(Guid userSessionGuid, long[] objectVersionIds)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
    {
      if (objectVersionIds == null || objectVersionIds.Length == 0)
        throw new ArgumentException();
      this.RemoveObjects(objectVersionIds);
    }
  }

  public long FindObjectWithDesignation(Guid userSessionGuid, int objectTypeId, string designation)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
      return !string.IsNullOrEmpty(designation) ? this.FindObjectWithDesignation(objectTypeId, designation) : throw new ArgumentException();
  }

  private long FindObjectWithDesignation(int objectTypeId, string designation)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(objectTypeId);
      objectCollection.ShowAllModifications = true;
      DBRecordSetParams dbRecordSetParams = new DBRecordSetParams();
      dbRecordSetParams.Columns = new object[1]
      {
        (object) ObligatoryObjectAttributes.F_OBJECT_ID
      };
      // ISSUE: explicit reference operation
      (^ref dbRecordSetParams).Conditions = new ConditionStructure[1]
      {
        new ConditionStructure()
        {
          Attribute = (object) Constants.DesignationAttributeTypeID,
          RelationalOperator = RelationalOperators.Equal,
          Value = (object) designation,
          SQL = string.Empty
        }
      };
      dbRecordSetParams.RecordCount = -1;
      DBRecordSetParams paramSet = dbRecordSetParams;
      DataTable dataTable = objectCollection.Select(paramSet);
      return dataTable.Rows.Count == 0 ? 0L : DataSetProcessor.GetInt64Value(dataTable.Rows[0], 0, 0L);
    }
  }

  private void RemoveObjects(long[] objectVersionIds)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (long objectVersionId in objectVersionIds)
      {
        sessionKeeper.Session.GetObject(objectVersionId, false)?.Delete((long) Consts.PurgeMode);
        sessionKeeper.Session.GetObject(-objectVersionId, false)?.Delete((long) Consts.PurgeMode);
      }
    }
  }

  private DataTable FindComposition(FindCompositionParams @params)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DataTable composition = (DataTable) null;
      Dictionary<int, int[]> projectApplicabilites = this.GetProjectApplicabilites(@params);
      int objectTypeColumnIndex = CompositionCopyingServerService.GetObjectTypeColumnIndex(@params);
      int num = 0;
      if (objectTypeColumnIndex < 0)
      {
        num = @params.RecordSetParams.AddColumnDescriptors(new ColumnDescriptor[1]
        {
          new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_TYPE)
        }, (List<int>) null);
        objectTypeColumnIndex = CompositionCopyingServerService.GetObjectTypeColumnIndex(@params);
      }
      foreach (KeyValuePair<int, int[]> keyValuePair in projectApplicabilites)
      {
        IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(keyValuePair.Key);
        relationCollection.ChildObjectTypes = (IList<int>) keyValuePair.Value;
        if (!string.IsNullOrEmpty(@params.FiltrationOwnerID))
          relationCollection.FiltrationOwnerID = @params.FiltrationOwnerID;
        DBRecordSetParams recordSetParams = @params.RecordSetParams;
        if (@params.CompositionContexts != null)
        {
          if (recordSetParams.Tags == null)
            recordSetParams.Tags = new HybridDictionary();
          recordSetParams.Tags[(object) "{AB419A02-DE8A-4A8E-905A-D782F5B720E5}"] = (object) ((IEnumerable<CompositionContext>) @params.CompositionContexts).Select<CompositionContext, long>((System.Func<CompositionContext, long>) (o => o.Value)).ToArray<long>();
        }
        DataTable dataTable = relationCollection.ConsistFrom(recordSetParams, @params.ProjectVersionID);
        if (composition == null)
        {
          composition = dataTable;
          composition.BeginLoadData();
        }
        else
        {
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
            composition.Rows.Add(row.ItemArray);
        }
      }
      if (num > 0)
        composition.Columns.RemoveAt(objectTypeColumnIndex);
      composition?.EndLoadData();
      return composition;
    }
  }

  private static int GetObjectTypeColumnIndex(FindCompositionParams @params)
  {
    int objectTypeColumnIndex = Array.IndexOf<object>(@params.RecordSetParams.Columns, (object) ObligatoryObjectAttributes.F_OBJECT_TYPE);
    if (objectTypeColumnIndex < 0)
      objectTypeColumnIndex = Array.IndexOf<object>(@params.RecordSetParams.Columns, (object) -7);
    return objectTypeColumnIndex;
  }

  private Dictionary<int, int[]> GetProjectApplicabilites(FindCompositionParams @params)
  {
    Dictionary<int, int[]> projectApplicabilites = new Dictionary<int, int[]>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (IGrouping<int, IMSApplicability> source in MetaDataHelper.GetObjectTypeApplicabilities(sessionKeeper.Session.GetObject(@params.ProjectVersionID).ObjectType).GroupBy<IMSApplicability, int>((System.Func<IMSApplicability, int>) (o => o.RelationTypeID)))
      {
        if (((IEnumerable<int>) @params.RelationTypes).Contains<int>(source.Key))
        {
          List<int> intList = MetaDataHelper.OptimizeChildObjectTypes((IEnumerable<int>) source.Select<IMSApplicability, int>((System.Func<IMSApplicability, int>) (o => o.ChildObjectTypeID)).Distinct<int>().ToList<int>());
          projectApplicabilites.Add(source.Key, intList.ToArray());
        }
      }
    }
    return projectApplicabilites;
  }

  private Tuple<ObjectBlank, string> CreateObject(ObjectBlank blank)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      try
      {
        IDBObject dbObject1 = sessionKeeper.Session.GetObject(blank.ObjectVersionID);
        IDBObject dbObject2 = sessionKeeper.Session.GetObjectCollection(dbObject1.ObjectType).Create();
        List<AttributeValues> source = new List<AttributeValues>();
        source.AddRange(blank.Attributes.Where<AttributeBlank>((System.Func<AttributeBlank, bool>) (o => o.IsChanged)).Select<AttributeBlank, AttributeValues>((System.Func<AttributeBlank, AttributeValues>) (o => new AttributeValues(o.AttributeTypeID, o.Value))));
        foreach (AttributeValues attributesValue in dbObject1.GetAttributesValues(GetAttributeValuesModes.IncludeBlobs | GetAttributeValuesModes.IncludeCaption))
        {
          AttributeValues attributeValues = attributesValue;
          if (!source.Any<AttributeValues>((System.Func<AttributeValues, bool>) (o => o.AttributeID == attributeValues.AttributeID)) && attributeValues.AttributeID != InstancesConstants.GroupProductIDAttributeTypeID && attributeValues.AttributeID != CompositionCopyingConstants.PrototypeReferenceAttributeTypeID && attributeValues.ComputeMode == ComputeValueModes.NotComputableValue && attributeValues.AttributeType != FieldTypes.ftFile && attributeValues.AttributeID != Constants.ObjectContentModificationDateAttributeTypeID)
            source.Add(attributeValues);
        }
        source.Add(new AttributeValues(CompositionCopyingConstants.PrototypeReferenceAttributeTypeID, (object) Math.Abs(blank.ObjectVersionID)));
        dbObject2.SetAttributesValues(source.ToArray(), false, true);
        dbObject2.CommitCreation(true, true);
        ObjectBlank objectBlank = this._groupAttributesChangingServerService.FindObjects(sessionKeeper.Session.SessionGUID, new long[1]
        {
          dbObject2.ObjectID
        })[0];
        objectBlank.Statuses = blank.Statuses & ~ObjectBlankStatuses.Copy & ~ObjectBlankStatuses.Error | ObjectBlankStatuses.Sussess;
        return new Tuple<ObjectBlank, string>(objectBlank, (string) null);
      }
      catch (Exception ex)
      {
        string str = ex.Message;
        if (ex is AccessDeniedException)
        {
          string[] checkAccessLog = sessionKeeper.Session.GetCheckAccessLog(GetAccessModes.LastCheck);
          str = $"{str}:{Environment.NewLine}{string.Join("\t" + Environment.NewLine, checkAccessLog)}";
        }
        blank.Statuses |= ObjectBlankStatuses.Error;
        return new Tuple<ObjectBlank, string>(blank, str);
      }
    }
  }

  private string[] CreateComposition(long projectVersionId, Tuple<long, long>[] composition)
  {
    List<string> stringList = new List<string>();
    using (new SessionKeeper())
    {
      foreach (Tuple<long, long> tuple in composition)
      {
        string relation = this.CreateRelation(projectVersionId, tuple.Item1, tuple.Item2);
        if (!string.IsNullOrEmpty(relation))
          stringList.Add(relation);
      }
    }
    return stringList.ToArray();
  }

  private string CreateRelation(
    long projectVersionId,
    long prototypeRelationId,
    long partVersionId)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      try
      {
        IDBRelation relation = sessionKeeper.Session.GetRelation(prototypeRelationId);
        IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(relation.RelationType);
        IDBObject dbObject = sessionKeeper.Session.GetObject(partVersionId);
        NewRelationProperties properties = new NewRelationProperties()
        {
          BeginDate = DateTime.Now,
          EndDate = DateTime.MaxValue,
          PartID = dbObject.ID,
          PartObjectID = dbObject.ObjectID,
          ProjectObjectID = projectVersionId,
          PrototypeRelationID = prototypeRelationId
        };
        IDBAttribute byId = relationCollection.Create(properties).Attributes.FindByID(Constants.ExplicitPartVersionIDAttributeTypeID);
        if (byId != null)
          byId.Value = (object) Math.Abs(partVersionId);
        return (string) null;
      }
      catch (Exception ex)
      {
        string relation = $"Не удалось создать связь между #{projectVersionId} и #{partVersionId}. {ex.Message}";
        if (ex is AccessDeniedException)
        {
          string[] checkAccessLog = sessionKeeper.Session.GetCheckAccessLog(GetAccessModes.LastCheck);
          relation = $"{relation}:{Environment.NewLine}{string.Join("    " + Environment.NewLine, checkAccessLog)}";
        }
        return relation;
      }
    }
  }

  private ObjectBlank[] CreateBlanks(long objectVersionID, long[] copyVersionIds)
  {
    if (copyVersionIds == null || copyVersionIds.Length == 0)
      copyVersionIds = new long[1]{ objectVersionID };
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      long[] instances = this._instancesServerService.FindInstances(sessionKeeper.Session.SessionGUID, objectVersionID);
      List<ObjectBlank> objectBlankList = new List<ObjectBlank>();
      foreach (long num in ((IEnumerable<long>) copyVersionIds).Distinct<long>())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(num);
        List<AttributeBlank> list = ((IEnumerable<AttributeValues>) dbObject.GetAttributesValues(GetAttributeValuesModes.IncludeObligatoryAttributes)).Select<AttributeValues, AttributeBlank>((System.Func<AttributeValues, AttributeBlank>) (o => new AttributeBlank(o.AttributeID, false, GroupAttributesChangingHelper.IsEditableAttribute(dbObject.ObjectType, o.AttributeID), !(o.Value is DBNull) ? o.Value : (object) null))).ToList<AttributeBlank>();
        foreach (AttributeBlank attributeBlank in list)
        {
          if (attributeBlank.IsEditable)
          {
            IMSAttribute4ObjectType attribute4ObjectType = MetaDataHelper.GetAttribute4ObjectType(dbObject.ObjectType, attributeBlank.AttributeTypeID);
            if (attribute4ObjectType != null && attribute4ObjectType.Options.HasFlag((Enum) AttributeOptions.DontCopyPrototypeValue))
              attributeBlank.Value = (object) null;
          }
        }
        ObjectBlank objectBlank = new ObjectBlank(num, dbObject.ObjectType, false, 0L, list.ToArray());
        objectBlank.Statuses = ObjectBlankStatuses.Copy;
        if (((IEnumerable<long>) instances).Contains<long>(num))
          objectBlank.Statuses |= ObjectBlankStatuses.Instance;
        objectBlankList.Add(objectBlank);
      }
      return objectBlankList.ToArray();
    }
  }

  private bool CheckObjectReferenceAssociatedWithDocumentElement(long documentVersionID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttribute attributeById = sessionKeeper.Session.GetObject(documentVersionID).GetAttributeByID(CompositionCopyingConstants.ObjectReferenceAssociatedWithDocumentElementAttributeTypeID);
      if (attributeById != null)
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(attributeById.AsInteger, false);
        if (dbObject != null)
          return !((IEnumerable<int>) CompositionCopyingHelper.GetForbiddenForCreateCopyAssociatedWithDocumentElementObjectTypes()).Contains<int>(dbObject.ObjectType);
      }
    }
    return true;
  }
}
