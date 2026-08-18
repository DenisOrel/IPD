// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.ImbaseNtdDocLink
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

#nullable disable
namespace Intermech.Imbase.Server;

internal class ImbaseNtdDocLink
{
  private static List<long> GetNtdObjectIDs(IUserSession session, long imbaseObjId, long recId)
  {
    List<long> ntdObjectIds = new List<long>();
    if (recId != -1L)
    {
      DataRow recordRow = ImbaseServer.GetRecordRow(session, imbaseObjId, recId, false);
      if (recordRow != null)
      {
        int index = recordRow.Table.Columns.IndexOf(Intermech.Imbase.Consts.ImbaseNTDLinkAttId.ToString());
        if (index != -1)
        {
          DataColumn column = recordRow.Table.Columns[index];
          if (column.DataType == typeof (string))
          {
            Guid result;
            if (Guid.TryParse(Convert.ToString(recordRow[column]), out result))
            {
              QuickObjectInfo objectInfo = session.GetObjectInfo(result);
              if (!objectInfo.Empty)
              {
                ntdObjectIds.Add(objectInfo.ObjectID);
                return ntdObjectIds;
              }
            }
          }
          else if (column.DataType == typeof (ValuesArray) && recordRow[column] is ValuesArray valuesArray && valuesArray.ElementType == typeof (string))
          {
            foreach (object obj in valuesArray.GetArray())
            {
              Guid result;
              if (Guid.TryParse(Convert.ToString(obj), out result))
              {
                QuickObjectInfo objectInfo = session.GetObjectInfo(result);
                if (!objectInfo.Empty)
                  ntdObjectIds.Add(objectInfo.ObjectID);
              }
            }
            if (ntdObjectIds.Count != 0)
              return ntdObjectIds;
          }
        }
      }
    }
    IDBAttribute attributeByGuid = session.GetObject(imbaseObjId, false)?.GetAttributeByGuid(Intermech.Imbase.Consts.ImbaseNTDLinkAttGuid);
    if (attributeByGuid != null && attributeByGuid.DataType == FieldTypes.ftObjectLink)
    {
      foreach (object obj in attributeByGuid.Values)
      {
        long result;
        if (long.TryParse(Convert.ToString(obj), out result))
          ntdObjectIds.Add(result);
      }
    }
    return ntdObjectIds;
  }

  private static bool CheckExistingNtdObjects(
    IUserSession session,
    long objId,
    List<long> ntdObjectIds)
  {
    bool flag = false;
    IDBRelationCollection relationCollection = session.GetRelationCollection(Intermech.Imbase.Consts.IncludeByLinkRelId);
    relationCollection.LocalTypesMode = true;
    ColumnDescriptor[] columns = new ColumnDescriptor[3]
    {
      new ColumnDescriptor((object) -20, AttributeSourceTypes.Relation, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) -3, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) Intermech.Imbase.Consts.IncludeInCompositionByLinkAttId, AttributeSourceTypes.Relation, ColumnContents.String, ColumnNameMapping.ID, SortOrders.NONE, 0)
    };
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(Intermech.Imbase.Consts.IncludeInCompositionByLinkAttId, RelationalOperators.Equal, (object) 1, LogicalOperators.NONE, 0, false)
    }, columns, lastOrderValue: (object) -1);
    List<Tuple<long, long>> list1 = relationCollection.ConsistFrom(paramSet, objId).AsEnumerable().Select<DataRow, Tuple<long, long>>((System.Func<DataRow, Tuple<long, long>>) (x => new Tuple<long, long>(Convert.ToInt64(x[0]), Convert.ToInt64(x[1])))).ToList<Tuple<long, long>>();
    if (list1.Count == 0 && ntdObjectIds.Count == 0)
      return false;
    List<long> second = new List<long>();
    Dictionary<long, long> dictionary = new Dictionary<long, long>();
    foreach (long ntdObjectId in ntdObjectIds)
    {
      QuickObjectInfo objectInfo = session.GetObjectInfo(ntdObjectId);
      dictionary[objectInfo.ID] = ntdObjectId;
    }
    foreach (Tuple<long, long> tuple in list1)
    {
      long num1;
      long num2;
      tuple.Deconstruct<long, long>(out num1, out num2);
      long aRelationID = num1;
      long key = num2;
      long num3;
      if (dictionary.TryGetValue(key, out num3))
      {
        second.Add(num3);
      }
      else
      {
        session.GetRelation(aRelationID, false)?.Delete(0L);
        flag = true;
      }
    }
    List<long> list2 = ntdObjectIds.Except<long>((IEnumerable<long>) second).ToList<long>();
    if (list2.Count == 0)
      return flag;
    foreach (long partObjectID in list2)
      relationCollection.Create(objId, partObjectID, new AttributeValues[1]
      {
        new AttributeValues(Intermech.Imbase.Consts.IncludeInCompositionByLinkAttId, (object) true)
      });
    return true;
  }

  public static bool CheckNtdObjects(
    IUserSession session,
    long imbaseObjId,
    long recId,
    long objId)
  {
    List<long> ntdObjectIds = ImbaseNtdDocLink.GetNtdObjectIDs(session, imbaseObjId, recId);
    return ImbaseNtdDocLink.CheckExistingNtdObjects(session, objId, ntdObjectIds);
  }
}
