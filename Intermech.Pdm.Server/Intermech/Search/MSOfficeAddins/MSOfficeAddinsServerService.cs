// Decompiled with JetBrains decompiler
// Type: Intermech.Search.MSOfficeAddins.MSOfficeAddinsServerService
// Assembly: Intermech.Pdm.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EC8EF964-D01E-4AAA-8100-7A99DC670202
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Pdm.Server.dll

using Intermech.Interfaces;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

#nullable disable
namespace Intermech.Search.MSOfficeAddins;

public sealed class MSOfficeAddinsServerService : LongLifeObject, IMSOfficeAddinsServerService
{
  public Tuple<long, string>[] SynchronizeDocumentCompositionWithObjectsFromUrls(
    Guid userSessionGuid,
    long documentVersionID,
    string[] objectUrls)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
    {
      if (ObjectHelper.IsUnknownObjectVersionID(documentVersionID))
        throw new ArgumentException();
      return objectUrls == null || !((IEnumerable<string>) objectUrls).Any<string>(new System.Func<string, bool>(string.IsNullOrEmpty)) ? this.SynchronizeDocumentCompositionWithObjectsFromUrls(documentVersionID, objectUrls) : throw new ArgumentException();
    }
  }

  private Tuple<long, string>[] SynchronizeDocumentCompositionWithObjectsFromUrls(
    long documentVersionID,
    string[] objectUrls)
  {
    List<Tuple<long, string>> tupleList = new List<Tuple<long, string>>();
    Tuple<long, bool>[] objectIDAddedByReferencePairs = this.FindObjectsAddedByReferenceInDocumentComposition(documentVersionID);
    long[] objectIdsFromObjectUrls = this.GetObjectIdsForObjectUrls(objectUrls);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      long[] array = ((IEnumerable<long>) objectIdsFromObjectUrls).Where<long>((System.Func<long, bool>) (o => ((IEnumerable<Tuple<long, bool>>) objectIDAddedByReferencePairs).All<Tuple<long, bool>>((System.Func<Tuple<long, bool>, bool>) (oo => oo.Item1 != o)))).ToArray<long>();
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(MSOfficeAddinsConstants.ObjectsAddedByReferenceRelationTypeID);
      foreach (long id in array)
      {
        IDBObject objectBaseVersionById = sessionKeeper.Session.GetObjectBaseVersionByID(id, false);
        if (objectBaseVersionById != null)
        {
          try
          {
            relationCollection.Create(documentVersionID, objectBaseVersionById.ObjectID, this.CreateAttributeValuesForAddedByReferenceAttribute());
          }
          catch
          {
            tupleList.Add(new Tuple<long, string>(objectBaseVersionById.ObjectID, objectBaseVersionById.Caption));
          }
        }
      }
      foreach (long partID in ((IEnumerable<long>) objectIdsFromObjectUrls).Where<long>((System.Func<long, bool>) (o => ((IEnumerable<Tuple<long, bool>>) objectIDAddedByReferencePairs).Any<Tuple<long, bool>>((System.Func<Tuple<long, bool>, bool>) (oo => oo.Item1 == o && !oo.Item2)))).ToArray<long>())
        sessionKeeper.Session.GetRelation(documentVersionID, partID).SetAttributesValues(this.CreateAttributeValuesForAddedByReferenceAttribute());
      foreach (long partID in ((IEnumerable<Tuple<long, bool>>) objectIDAddedByReferencePairs).Select<Tuple<long, bool>, long>((System.Func<Tuple<long, bool>, long>) (o => o.Item1)).Where<long>((System.Func<long, bool>) (o => !((IEnumerable<long>) objectIdsFromObjectUrls).Contains<long>(o))).ToArray<long>())
        sessionKeeper.Session.GetRelation(documentVersionID, partID)?.Delete((long) Consts.PurgeMode);
    }
    return tupleList.ToArray();
  }

  private Tuple<long, bool>[] FindObjectsAddedByReferenceInDocumentComposition(
    long documentVersionID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(MSOfficeAddinsConstants.ObjectsAddedByReferenceRelationTypeID);
      relationCollection.FiltrationOwnerID = "cad00601-306c-11d8-b4e9-00304f19f545";
      DBRecordSetParams paramSet = new DBRecordSetParams()
      {
        Columns = new object[2]
        {
          (object) ObligatoryObjectAttributes.F_ID,
          (object) MSOfficeAddinsConstants.AddedByReferenceAttributeTypeID
        }
      };
      DataTable dataTable = relationCollection.ConsistFrom(paramSet, documentVersionID);
      List<Tuple<long, bool>> source = new List<Tuple<long, bool>>();
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        source.Add(new Tuple<long, bool>(DataSetProcessor.GetInt64Value(row, 0, 0L), DataSetProcessor.GetBooleanValue(row, 1, false)));
      return source.Distinct<Tuple<long, bool>>().ToArray<Tuple<long, bool>>();
    }
  }

  private long[] GetObjectIdsForObjectUrls(string[] objectUrls)
  {
    List<long> longList = new List<long>();
    if (objectUrls != null)
    {
      long[] array = ((IEnumerable<string>) objectUrls).Select<string, long>(new System.Func<string, long>(MSOfficeAddinsHelper.GetObjectVersionIDFromObjectUrl)).Where<long>((System.Func<long, bool>) (o => !ObjectHelper.IsUnknownObjectVersionID(o))).Distinct<long>().ToArray<long>();
      if (array.Length != 0)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(-1);
          DBRecordSetParams dbRecordSetParams = new DBRecordSetParams();
          dbRecordSetParams.Columns = new object[1]
          {
            (object) ObligatoryObjectAttributes.F_ID
          };
          ref DBRecordSetParams local = ref dbRecordSetParams;
          ConditionStructure[] conditionStructureArray = new ConditionStructure[2];
          ConditionStructure conditionStructure = new ConditionStructure();
          conditionStructure.Attribute = (object) ObligatoryObjectAttributes.F_OBJECT_ID;
          conditionStructure.RelationalOperator = RelationalOperators.In;
          conditionStructure.Value = (object) array;
          conditionStructure.SQL = string.Empty;
          conditionStructure.LogicalOperator = LogicalOperators.OR;
          conditionStructureArray[0] = conditionStructure;
          conditionStructure = new ConditionStructure();
          conditionStructure.Attribute = (object) ObligatoryObjectAttributes.F_OBJECT_ID;
          conditionStructure.RelationalOperator = RelationalOperators.In;
          conditionStructure.Value = (object) ((IEnumerable<long>) array).Select<long, long>((System.Func<long, long>) (o => -o)).ToArray<long>();
          conditionStructure.SQL = string.Empty;
          conditionStructureArray[1] = conditionStructure;
          local.Conditions = conditionStructureArray;
          dbRecordSetParams.RecordCount = -1;
          DBRecordSetParams paramSet = dbRecordSetParams;
          foreach (DataRow row in (InternalDataCollectionBase) objectCollection.Select(paramSet).Rows)
            longList.Add(DataSetProcessor.GetInt64Value(row, 0, 0L));
        }
      }
    }
    return longList.ToArray();
  }

  private AttributeValues[] CreateAttributeValuesForAddedByReferenceAttribute()
  {
    return new AttributeValues[1]
    {
      new AttributeValues(MSOfficeAddinsConstants.AddedByReferenceAttributeTypeID, (object) true)
    };
  }
}
