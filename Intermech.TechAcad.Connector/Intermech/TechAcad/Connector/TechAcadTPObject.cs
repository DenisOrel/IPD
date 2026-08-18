// Decompiled with JetBrains decompiler
// Type: Intermech.TechAcad.Connector.TechAcadTPObject
// Assembly: Intermech.TechAcad.Connector, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5A35A651-9A96-41F3-9839-2AAB5A952CB8
// Assembly location: D:\IPS\Client\Intermech.TechAcad.Connector.dll

using ImSSP;
using Intermech.Docking;
using Intermech.Expert;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.TechCard;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.Controls;
using Intermech.Runtime.ComInterop.LocalServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

#nullable disable
namespace Intermech.TechAcad.Connector;

internal class TechAcadTPObject : SingleThreadedObject, ITPObject
{
  private long _id;
  private readonly long _objectId;
  private DataTable _dataTable;
  private bool _draftInfoLoaded;
  private bool _artDraftInfoLoaded;
  private readonly NavWindow _navWindow;
  private int _objectTypeId;
  private string _objectName;
  private string _objectDesign;
  private ITPObject _parentObject;
  private TechAcadTPObjectList _objList;
  private TechAcadDraftObjectList _draftList;
  private TechAcadSketchObjectList _sketchList;
  private TechAcadArtDraftObjectList _artDraftList;

  private void Initialize()
  {
    this._objectTypeId = -1;
    this._objectName = "";
    this._objectDesign = "";
    this._draftInfoLoaded = false;
    this._draftList = (TechAcadDraftObjectList) null;
    this._sketchList = (TechAcadSketchObjectList) null;
  }

  private void InitializeDbData()
  {
    bool flag = false;
    if (this._dataTable != null)
    {
      DataRow[] dataRowArray = this._dataTable.Select($"{"F_OBJECT_ID"} = {this._objectId}");
      if (dataRowArray.Length != 0)
      {
        DataRow dataRow = dataRowArray[0];
        this._id = Convert.ToInt64(dataRow[0]);
        this._objectTypeId = Convert.ToInt32(dataRow[1]);
        object obj1 = dataRow[5];
        this._objectName = obj1 != DBNull.Value ? obj1.ToString() : this._objectName;
        object obj2 = dataRow[6];
        this._objectDesign = obj2 != DBNull.Value ? obj2.ToString() : this._objectDesign;
        if (this._objectName == string.Empty && this._objectDesign == string.Empty)
        {
          object obj3 = dataRow[7];
          this._objectName = obj3 != DBNull.Value ? obj3.ToString() : this._objectName;
        }
        flag = true;
      }
    }
    if (flag)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this.ObjID, false);
      if (dbObject == null)
        return;
      this._id = dbObject.ID;
      this._objectTypeId = dbObject.ObjectType;
      IDBAttribute attributeByGuid1 = dbObject.GetAttributeByGuid(new Guid("cad00020-306c-11d8-b4e9-00304f19f545"), false);
      this._objectName = attributeByGuid1 == null || attributeByGuid1.Value == DBNull.Value ? this._objectName : attributeByGuid1.AsString;
      IDBAttribute attributeByGuid2 = dbObject.GetAttributeByGuid(new Guid("cad0001f-306c-11d8-b4e9-00304f19f545"), false);
      this._objectDesign = attributeByGuid2 == null || attributeByGuid2.Value == DBNull.Value ? this._objectDesign : attributeByGuid2.AsString;
      if (!(this._objectName == string.Empty) || !(this._objectDesign == string.Empty))
        return;
      this._objectName = dbObject.Caption;
    }
  }

  private long GetArticleObjectId(ObjInfoItem objInfo)
  {
    if ((TypedInfoItem) objInfo == (TypedInfoItem) null || objInfo.ObjectID == 0L)
      return 0;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      List<ObjInfoItem> articles4Objects = TechCardObjUtils.Article.GetArticles4Objects(new List<ObjInfoItem>()
      {
        objInfo
      }, sessionKeeper.Session);
      if (articles4Objects == null || articles4Objects.Count == 0)
        return 0;
      foreach (ObjInfoItem objInfoItem in articles4Objects)
      {
        if (!((TypedInfoItem) objInfoItem == (TypedInfoItem) null))
          return objInfoItem.ObjectID;
      }
    }
    return 0;
  }

  private List<long> GetDraftsForArticle(long objectId)
  {
    if (objectId == 0L)
      return (List<long>) null;
    IMSRelationType relationType = MetaDataHelper.GetRelationType(new Guid("cad00154-306c-11d8-b4e9-00304f19f545"));
    if (relationType == null)
      return (List<long>) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      List<int> intList = new List<int>();
      intList.AddRange((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechAcadConsts.ObjTypeAcadDraft));
      intList.AddRange((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechAcadConsts.ObjTypeAcadAssemblyDraft));
      ConditionStructure[] conditions = new ConditionStructure[1]
      {
        new ConditionStructure(-7, RelationalOperators.In, (object) intList.ToArray(), (object) null, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Auto, ColumnContents.Text)
      };
      DataTable childSostavData = DataHelper.GetChildSostavData(objectId, sessionKeeper.Session, (IEnumerable<int>) new int[1]
      {
        relationType.RelationTypeID
      }, false, (IEnumerable<ConditionStructure>) conditions);
      List<long> draftsForArticle = new List<long>();
      if (childSostavData == null || childSostavData.Rows.Count == 0)
        return draftsForArticle;
      foreach (DataRow row in (InternalDataCollectionBase) childSostavData.Rows)
      {
        int int32 = Convert.ToInt32(row["F_OBJECT_TYPE"]);
        if (intList.Contains(int32))
        {
          long int64 = Convert.ToInt64(row["F_OBJECT_ID"]);
          switch (int64)
          {
            case -1:
            case 0:
              continue;
            default:
              draftsForArticle.Add(int64);
              continue;
          }
        }
      }
      return draftsForArticle;
    }
  }

  protected virtual void LoadCompositionInfo()
  {
    if (this._dataTable != null)
      return;
    List<ColumnDescriptor> columnDescriptorList = new List<ColumnDescriptor>();
    columnDescriptorList.Add(new ColumnDescriptor((object) -3, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
    columnDescriptorList.Add(new ColumnDescriptor((object) -7, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
    columnDescriptorList.Add(new ColumnDescriptor((object) -2, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
    columnDescriptorList.Add(new ColumnDescriptor((object) -23, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
    columnDescriptorList.Add(new ColumnDescriptor((object) -21, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
    columnDescriptorList.Add(new ColumnDescriptor((object) new Guid("cad00020-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0));
    columnDescriptorList.Add(new ColumnDescriptor((object) new Guid("cad0001f-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0));
    columnDescriptorList.Add(new ColumnDescriptor((object) -50, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
    columnDescriptorList.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PRJLINK_ID, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
    int[] relations = new int[2]
    {
      TechCardConsts.RelTypes.TechRelationID,
      TechCardConsts.RelTypes.TechDraftRelationID
    };
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this._dataTable = DataHelper.GetChildSostavData(new ObjInfoItem(this._objectId, this._objectTypeId), sessionKeeper.Session, (IEnumerable<int>) relations, true, (IEnumerable<ConditionStructure>) null, (IEnumerable<ColumnDescriptor>) columnDescriptorList.ToArray());
  }

  protected virtual void LoadObjCollInfo(bool forceMode)
  {
    if (this._objList != null && !forceMode)
      return;
    this.LoadCompositionInfo();
    this._objList = new TechAcadTPObjectList();
    if (this._dataTable == null)
      return;
    DataRow[] dataRowArray = this._dataTable.Select($"{"F_RELATION_TYPE"} = {TechCardConsts.RelTypes.TechRelationID} AND {"F_PROJ_ID"} = {this._objectId}");
    if (dataRowArray.Length == 0)
      return;
    foreach (DataRow dataRow in dataRowArray)
    {
      long int64 = Convert.ToInt64(dataRow[2]);
      if (!MetaDataHelper.IsObjectTypeChildOf(Convert.ToInt32(dataRow[1]), TechCardConsts.ObjectTypes.DraftBaseID))
        this._objList.Items.Add(new TechAcadTPObject(int64, this._dataTable, this.NavWindow)
        {
          ParentObject = (ITPObject) this
        });
    }
  }

  protected virtual void LoadDraftInfo()
  {
    if (this._draftInfoLoaded)
      return;
    try
    {
      this._draftList = TechCardUtils.CheckRelationApplicability(this._objectTypeId, TechCardConsts.ObjectTypes.DraftCadmechID, TechCardConsts.RelTypes.TechRelationID, false, false) ? new TechAcadDraftObjectList((ITPObject) this) : (TechAcadDraftObjectList) null;
      this._sketchList = TechCardUtils.CheckRelationApplicability(this._objectTypeId, TechCardConsts.ObjectTypes.DraftCadmechID, TechCardConsts.RelTypes.TechDraftRelationID, false, false) ? new TechAcadSketchObjectList(this) : (TechAcadSketchObjectList) null;
      if (this._draftList == null && this._sketchList == null)
        return;
      this.LoadCompositionInfo();
      if (this._dataTable == null)
        return;
      DataRow[] dataRowArray = this._dataTable.Select($"{"F_RELATION_TYPE"} IN ({TechCardConsts.RelTypes.TechDraftRelationID}, {TechCardConsts.RelTypes.TechRelationID}) AND {"F_PROJ_ID"} = {this._objectId}");
      if (dataRowArray.Length == 0)
        return;
      List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechCardConsts.ObjectTypes.DraftCadmechID);
      GenericListHelper.MakeUnique<int>(childrenIdRecursive);
      Dictionary<long, TechAcadDraftObject> dictionary = new Dictionary<long, TechAcadDraftObject>();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        foreach (DataRow dataRow in dataRowArray)
        {
          ObjInfoItem draftInfoItem = new ObjInfoItem(Convert.ToInt64(dataRow[2]), Convert.ToInt32(dataRow[1]));
          if (childrenIdRecursive.BinarySearch(draftInfoItem.ObjTypeID) >= 0)
          {
            int int32 = Convert.ToInt32(dataRow[3]);
            TechAcadDraftObject draftObject;
            if (!dictionary.TryGetValue(draftInfoItem.ObjectID, out draftObject))
            {
              draftObject = new TechAcadDraftObject(draftInfoItem, this.NavWindow);
              dictionary.Add(draftInfoItem.ObjectID, draftObject);
            }
            if (this._draftList != null && int32 == TechCardConsts.RelTypes.TechRelationID && this._draftList.get_ItemByID(draftInfoItem.ObjectID) == null)
            {
              draftObject = new TechAcadDraftObject(draftInfoItem, this.NavWindow);
              this._draftList.Items.Add(draftObject);
            }
            long int64_1 = Convert.ToInt64(dataRow[8]);
            IDBRelation relation = sessionKeeper.Session.GetRelation(int64_1);
            if (relation != null)
            {
              TechAcadSketchObjectList sketchObjectList = new TechAcadSketchObjectList(this);
              int count = sketchObjectList.Count;
              sketchObjectList.LoadSketchCollection(draftObject, this, (IDBAttributable) relation);
              if (sketchObjectList.Count > count)
              {
                IDBAttribute attributeByGuid = relation.GetAttributeByGuid(TechCardConsts.AttributeTypes.SortAttrTypeGuid, false);
                if (attributeByGuid != null)
                {
                  if (!attributeByGuid.Value.Equals((object) DBNull.Value))
                  {
                    try
                    {
                      long int64_2 = Convert.ToInt64(attributeByGuid.Value);
                      for (int index = count; index < sketchObjectList.Count; ++index)
                        sketchObjectList.Items[index]._orderID = int64_2 + (long) ((index - count) * 10);
                    }
                    catch (FormatException ex)
                    {
                    }
                  }
                }
              }
              if (this._sketchList != null)
              {
                foreach (ISketchObject sketchObject in sketchObjectList.Items)
                {
                  if (draftObject.SketchCollection.GetIndexByID(sketchObject.SketchID) != -1)
                    this._sketchList.Items.Add((TechAcadSketchObject) sketchObject);
                }
              }
            }
          }
        }
      }
    }
    finally
    {
      this._draftInfoLoaded = true;
    }
  }

  protected virtual void LoadArtDraftInfo()
  {
    if (this._artDraftInfoLoaded)
      return;
    try
    {
      long articleObjectId = this.GetArticleObjectId(new ObjInfoItem(this.ObjID, this._objectTypeId));
      if (articleObjectId == 0L)
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        List<long> draftsForArticle = this.GetDraftsForArticle(articleObjectId);
        this._artDraftList = new TechAcadArtDraftObjectList((ITPObject) this);
        foreach (long objectID in draftsForArticle)
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(objectID, false);
          if (dbObject != null)
            this._artDraftList.Items.Add((TechAcadDraftObject) new TechAcadArtDraftObject(new ObjInfoItem(dbObject), this.NavWindow));
        }
      }
    }
    finally
    {
      this._artDraftInfoLoaded = true;
    }
  }

  public virtual void SaveDraftInfo()
  {
    if (!this._draftInfoLoaded)
      return;
    int techDraftRelationId = TechCardConsts.RelTypes.TechDraftRelationID;
    int draftCadmechId = TechCardConsts.ObjectTypes.DraftCadmechID;
    IMSRelationType relationType = MetaDataHelper.GetRelationType(techDraftRelationId);
    int attributeTypeId = MetaDataHelper.GetAttributeTypeID(TechAcadConsts.attributeSketchName);
    if (!TechCardUtils.CheckRelationApplicability(this._objectTypeId, draftCadmechId, techDraftRelationId, false, false))
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if ((relationType.AnyAttributes ? 1 : (MetaDataHelper.GetAttribute4RelationType(techDraftRelationId, attributeTypeId) != null ? 1 : 0)) == 0)
      {
        IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attributeTypeId);
        string str = string.Format(sc_19182.ssp_techacad_19183(), (object) relationType.ShortName, (object) relationType.RelationTypeID, (object) attributeType.Name, (object) attributeType.AttributeID);
        Plugin.LogError(sc_19182.ssp_techacad_19184() + str);
      }
      ICompositionsAutomaticSortingService service1 = ServiceUtils.GetService<ICompositionsAutomaticSortingService>((object) sessionKeeper.Session, true);
      ICompositionsAutomaticSortingSession automaticSortingSession = (ICompositionsAutomaticSortingSession) null;
      IDBTransactions service2 = ServiceUtils.GetService<IDBTransactions>((object) sessionKeeper.Session, false);
      service2?.StartTransaction();
      try
      {
        List<IDBRelation> source1 = new List<IDBRelation>();
        List<IDBRelation> source2 = new List<IDBRelation>();
        List<IMSApplicability> typeApplicabilities = MetaDataHelper.GetObjectTypeApplicabilities(this._objectTypeId);
        IMSApplicability imsApplicability1 = (IMSApplicability) null;
        foreach (IMSApplicability imsApplicability2 in typeApplicabilities)
        {
          if (imsApplicability2 != null && imsApplicability2.RelationTypeID == techDraftRelationId && MetaDataHelper.IsObjectTypeChildOf(draftCadmechId, imsApplicability2.ChildObjectTypeID))
          {
            imsApplicability1 = imsApplicability2;
            break;
          }
        }
        List<ConditionStructure> conditionStructureList = new List<ConditionStructure>();
        List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(draftCadmechId);
        conditionStructureList.Add(new ConditionStructure(-7, RelationalOperators.In, (object) childrenIdRecursive.ToArray(), LogicalOperators.NONE, 0, false));
        DataTable childSostavData = DataHelper.GetChildSostavData(this.ObjID, sessionKeeper.Session, (IEnumerable<int>) new int[1]
        {
          techDraftRelationId
        }, false, (IEnumerable<ConditionStructure>) conditionStructureList.ToArray(), (IEnumerable<ColumnDescriptor>) new List<ColumnDescriptor>()
        {
          new ColumnDescriptor((object) attributeTypeId, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0)
        }.ToArray());
        bool flag = false;
        Dictionary<ObjInfoItem, TechAcadSketchObjectList> dictionary1 = new Dictionary<ObjInfoItem, TechAcadSketchObjectList>();
        if (this._draftList != null)
        {
          foreach (TechAcadDraftObject techAcadDraftObject in this._draftList.Items)
            dictionary1.Add(new ObjInfoItem(techAcadDraftObject.DraftID, techAcadDraftObject.ObjTypeID), new TechAcadSketchObjectList((TechAcadTPObject) null));
        }
        if (this._sketchList != null)
        {
          foreach (TechAcadSketchObject acadSketchObject in this._sketchList.Items)
          {
            if (acadSketchObject.Status != ChangeStatus.None && acadSketchObject.DraftObject != null)
            {
              if (!flag)
                flag = (acadSketchObject.Status & ChangeStatus.Added) == ChangeStatus.Added;
              ObjInfoItem key = new ObjInfoItem(acadSketchObject.DraftObject.DraftID);
              if (!dictionary1.ContainsKey(key))
                dictionary1[key] = new TechAcadSketchObjectList((TechAcadTPObject) null);
              dictionary1[key].Items.Add(acadSketchObject);
            }
          }
        }
        if (flag)
        {
          automaticSortingSession = service1.CreateSession((object) sessionKeeper.Session.SessionGUID);
          automaticSortingSession.PrefetchObjectComposition((IEnumerable<ObjInfoItem>) new ObjInfoItem[1]
          {
            new ObjInfoItem(this.ObjID, this._objectTypeId)
          }, (object) sessionKeeper.Session.SessionGUID);
        }
        try
        {
          IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(techDraftRelationId);
          foreach (KeyValuePair<ObjInfoItem, TechAcadSketchObjectList> keyValuePair in dictionary1)
          {
            if (keyValuePair.Value != null && keyValuePair.Value.Count != 0)
            {
              ObjInfoItem key1 = keyValuePair.Key;
              DataRow[] dataRowArray = childSostavData?.Select("F_OBJECT_ID=" + (object) key1.ObjectID);
              Dictionary<string, DataRow> dictionary2 = new Dictionary<string, DataRow>();
              if (dataRowArray != null)
              {
                foreach (DataRow dataRow in dataRowArray)
                {
                  string key2 = dataRow[TechAcadConsts.attributeSketchName.ToString()].ToString();
                  dictionary2.Add(key2, dataRow);
                }
              }
              foreach (TechAcadSketchObject acadSketchObject in keyValuePair.Value.Items)
              {
                DataRow dataRow;
                dictionary2.TryGetValue(acadSketchObject.SketchID, out dataRow);
                long aRelationID = dataRow != null ? Convert.ToInt64(dataRow["F_PRJLINK_ID"]) : 0L;
                if ((acadSketchObject.Status & ChangeStatus.Deleted) == ChangeStatus.Deleted)
                {
                  if (dataRow != null)
                  {
                    IDBRelation relation = sessionKeeper.Session.GetRelation(aRelationID, false);
                    if (relation != null)
                    {
                      source2.Add(relation);
                      relation.Delete(0L);
                      dictionary2.Remove(acadSketchObject.SketchID);
                    }
                  }
                }
                else
                {
                  if ((acadSketchObject.Status & ChangeStatus.Added) == ChangeStatus.Added)
                  {
                    if (dictionary2.Count > 0 && imsApplicability1 != null && (imsApplicability1.Options & ApplicabilityOptions.EnableMultiLink) != ApplicabilityOptions.EnableMultiLink)
                    {
                      string format = LocalizationHolder.rm.GetString("TechAcad.Connector_13");
                      string relationTypeName = MetaDataHelper.GetRelationTypeName(techDraftRelationId);
                      string objectTypeName1 = MetaDataHelper.GetObjectTypeName(draftCadmechId);
                      string objectTypeName2 = MetaDataHelper.GetObjectTypeName(this._objectTypeId);
                      string objectDesign = this._objectDesign;
                      object[] objArray = new object[4]
                      {
                        (object) (objectDesign != string.Empty ? $"{objectDesign} ({this._objectName})" : this._objectName),
                        (object) relationTypeName,
                        (object) objectTypeName2,
                        (object) objectTypeName1
                      };
                      throw new TechAcadError(string.Format(format, objArray));
                    }
                    IDBRelation dbRelation = relationCollection.Create(this.ObjID, key1.ObjectID);
                    if (dbRelation != null)
                    {
                      aRelationID = dbRelation.RelationID;
                      automaticSortingSession?.ProceedRelation(new CompositionSortingProjInfo(dbRelation.RelationID, dbRelation.RelationType, this.ObjID, this._objectTypeId, key1.ObjTypeID, 0L), (object) sessionKeeper.Session.SessionGUID);
                      dictionary2.Add(acadSketchObject.SketchID, (DataRow) null);
                      source1.Add(dbRelation);
                    }
                  }
                  IDBRelation relation = sessionKeeper.Session.GetRelation(aRelationID);
                  new TechAcadSketchObjectList((TechAcadTPObject) null)
                  {
                    Items = {
                      acadSketchObject
                    }
                  }.SaveSketchCollection((IDBAttributable) relation);
                  List<AttributeValues> attributeValuesList = new List<AttributeValues>()
                  {
                    new AttributeValues(attributeTypeId, (object) acadSketchObject.SketchID)
                  };
                  relation.SetAttributesValues(attributeValuesList.ToArray());
                }
              }
            }
          }
        }
        finally
        {
          if (flag)
            service1.DisposeSession((object) sessionKeeper.Session.SessionGUID);
        }
        this._sketchList?.ClearChangeStatus();
        service2?.Commit();
        this._draftInfoLoaded = true;
        INotificationService service3 = ServiceUtils.GetService<INotificationService>((object) ApplicationServices.Container, false);
        if (service3 == null)
          return;
        if (source1.Count > 0)
          service3.FireEvent((object) null, (NotificationEventArgs) new DBRelationsEventArgs("RelationsCreated", (IList<long>) source1.Select<IDBRelation, long>((System.Func<IDBRelation, long>) (item => item.RelationID)).ToList<long>(), (IList<long>) source1.Select<IDBRelation, long>((System.Func<IDBRelation, long>) (item => item.ProjID)).ToList<long>(), (IList<int>) null, (IList<int>) source1.Select<IDBRelation, int>((System.Func<IDBRelation, int>) (item => item.RelationType)).ToList<int>()));
        if (source2.Count <= 0)
          return;
        service3.FireEvent((object) null, (NotificationEventArgs) new DBRelationsEventArgs("RelationsRemoved", (IList<long>) source2.Select<IDBRelation, long>((System.Func<IDBRelation, long>) (item => item.RelationID)).ToList<long>(), (IList<long>) source2.Select<IDBRelation, long>((System.Func<IDBRelation, long>) (item => item.ProjID)).ToList<long>(), (IList<int>) null, (IList<int>) source2.Select<IDBRelation, int>((System.Func<IDBRelation, int>) (item => item.RelationType)).ToList<int>()));
      }
      catch
      {
        service2?.Rollback();
        throw;
      }
    }
  }

  public void SaveTPSketchCollectionInfo_old(IDraftObject draft)
  {
    if (draft == null)
      return;
    bool flag = false;
    foreach (IDraftObject draftObject in this._draftList.Items)
    {
      if (draftObject.DraftID == draft.DraftID)
      {
        flag = true;
        break;
      }
    }
    if (!flag)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelation relation = sessionKeeper.Session.GetRelation(this.ObjID, draft.DraftID, TechCardConsts.RelTypes.TechDraftRelationID, true);
      if (relation == null)
        return;
      TechAcadSketchObjectList sketchObjectList = new TechAcadSketchObjectList((TechAcadTPObject) null);
      foreach (TechAcadSketchObject acadSketchObject in this._sketchList.Items)
      {
        if (acadSketchObject.DraftObject.DraftID == draft.DraftID)
          sketchObjectList.Items.Add(acadSketchObject);
      }
      sketchObjectList.SaveSketchCollection((IDBAttributable) relation);
    }
  }

  public TechAcadTPObject(long objectId, NavWindow navWindow)
    : this(objectId, (DataTable) null, navWindow)
  {
  }

  public TechAcadTPObject(long objectId, DataTable dataTable, NavWindow navWindow)
  {
    this._objectId = objectId;
    this._dataTable = dataTable;
    this._navWindow = navWindow;
    this.Initialize();
    this.InitializeDbData();
  }

  public TechAcadTPObject(
    long objectId,
    long id,
    int objectTypeId,
    string objName,
    string objDesign,
    NavWindow navWindow)
  {
    this._objectId = objectId;
    this._navWindow = navWindow;
    this.Initialize();
    this._id = id;
    this._objectTypeId = objectTypeId;
    this._objectName = objName;
    this._objectDesign = objDesign;
  }

  public NavWindow NavWindow => this._navWindow;

  public virtual int Active
  {
    get
    {
      try
      {
        return Convert.ToInt32(TechAcadApplication.GetActiveTechObj(this.NavWindow) == this.ObjID);
      }
      catch (Exception ex)
      {
        Plugin.LogError(sc_19182.ssp_techacad_19185() + (object) ex);
        throw;
      }
    }
    set
    {
      if (value != 1)
        return;
      if (Plugin._serviceProvider == null)
        return;
      try
      {
        DockManager service = ServiceUtils.GetService<DockManager>((object) ApplicationServices.Container, false);
        if (service == null)
          return;
        foreach (DockControl dockControl in service.GetDockControls())
        {
          if (dockControl is NavWindow navWindow && navWindow == this.NavWindow)
          {
            navWindow.Activate();
            navWindow.Refresh();
          }
        }
      }
      catch (Exception ex)
      {
        Plugin.LogError(sc_19182.ssp_techacad_19186() + (object) ex);
        throw;
      }
    }
  }

  public virtual string Comment
  {
    get
    {
      try
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          return sessionKeeper.Session.GetObjectAttributeByGuid(this.ObjID, new Guid("cad00021-306c-11d8-b4e9-00304f19f545"))?.AsString ?? string.Empty;
      }
      catch (Exception ex)
      {
        Plugin.LogError(sc_19182.ssp_techacad_19187() + (object) ex);
        throw;
      }
    }
    set
    {
      try
      {
        if (this._objectTypeId == -1)
          return;
        IMSObjectType objectType = MetaDataHelper.GetObjectType(this._objectTypeId);
        if (objectType == null)
          return;
        int attributeId = MetaDataHelper.GetAttributeID((object) new Guid("cad00021-306c-11d8-b4e9-00304f19f545"));
        if (MetaDataHelper.GetAttribute4ObjectType(this._objectTypeId, attributeId) == null && !objectType.AnyAttributes)
          return;
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          sessionKeeper.Session.SetObjectAttributesValues(this.ObjID, false, new AttributeValues[1]
          {
            new AttributeValues(attributeId, (object) value)
          });
      }
      catch (Exception ex)
      {
        Plugin.LogError(sc_19182.ssp_techacad_19188() + (object) ex);
        throw;
      }
    }
  }

  public virtual string Designation => this._objectDesign;

  public virtual IDraftCollection DraftCollection
  {
    get
    {
      if (!this._draftInfoLoaded)
      {
        try
        {
          this.LoadDraftInfo();
        }
        catch (Exception ex)
        {
          Plugin.LogError(sc_19182.ssp_techacad_19189() + (object) ex);
          throw;
        }
      }
      return (IDraftCollection) this._draftList;
    }
  }

  public virtual string Name => this._objectName;

  public virtual ITPObjectCollection ObjCollection
  {
    get
    {
      try
      {
        if (this._objList != null)
          return (ITPObjectCollection) this._objList;
        this.LoadObjCollInfo(false);
        return (ITPObjectCollection) this._objList;
      }
      catch (Exception ex)
      {
        Plugin.LogError(sc_19182.ssp_techacad_19190() + (object) ex);
        throw;
      }
    }
  }

  public virtual long ObjID => this._objectId;

  public virtual ISketchCollection SketchCollection
  {
    get
    {
      if (!this._draftInfoLoaded)
      {
        try
        {
          this.LoadDraftInfo();
        }
        catch (Exception ex)
        {
          Plugin.LogError(sc_19182.ssp_techacad_19191() + (object) ex);
          throw;
        }
      }
      return (ISketchCollection) this._sketchList;
    }
  }

  public virtual ITPObjectType TPObjectType
  {
    get
    {
      if (this._objectTypeId == -1)
        return (ITPObjectType) null;
      try
      {
        return (ITPObjectType) new TechAcadObjectType(this._objectTypeId);
      }
      catch (Exception ex)
      {
        Plugin.LogError(sc_19182.ssp_techacad_19192() + (object) ex);
        throw;
      }
    }
  }

  public virtual IDraftCollection ArticleDraftCollection
  {
    get
    {
      if (!this._artDraftInfoLoaded)
      {
        try
        {
          this.LoadArtDraftInfo();
        }
        catch (Exception ex)
        {
          Plugin.LogError(sc_19182.ssp_techacad_19193() + (object) ex);
          throw;
        }
      }
      return (IDraftCollection) this._artDraftList;
    }
  }

  public virtual ITPObject ParentObject
  {
    get => this._parentObject;
    internal set => this._parentObject = value;
  }
}
