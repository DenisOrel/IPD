
// Type: Intermech.Search.Navigator.ObjectsNodeQuery
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Queries;
using Intermech.Search.Data;
using Intermech.Search.Data.Adapters;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace Intermech.Search.Navigator;

public class ObjectsNodeQuery(
  ConditionStructure[] conditions,
  INodeQuerySupport nodeQuerySupport,
  IServiceProvider serviceProvider) : ObjectsQuery(nodeQuerySupport, -1, conditions, serviceProvider)
{
  protected override DataTable OnSelect(IUserSession session, DBRecordSetParams queryParams)
  {
    Dictionary<int, List<long>> objectTypes = this.GetObjectTypes(queryParams.Conditions);
    DataTable dataTable1 = (DataTable) null;
    foreach (KeyValuePair<int, List<long>> keyValuePair in objectTypes)
    {
      IDBObjectCollection objectCollection = session.GetObjectCollection(keyValuePair.Key);
      this.PrepareCollection(objectCollection);
      // ISSUE: explicit reference operation
      (^ref queryParams).Conditions = new ConditionStructure[1]
      {
        new ConditionStructure()
        {
          Attribute = (object) ObligatoryObjectAttributes.F_OBJECT_ID,
          RelationalOperator = RelationalOperators.In,
          Value = (object) keyValuePair.Value.ToArray(),
          SQL = string.Empty
        }
      };
      DataTable dataTable2 = objectCollection.Select(queryParams);
      if (dataTable1 == null)
      {
        dataTable1 = dataTable2;
      }
      else
      {
        foreach (DataRow row in (InternalDataCollectionBase) dataTable2.Rows)
          dataTable1.Rows.Add(row.ItemArray);
      }
    }
    if (dataTable1 != null)
    {
      RecordSetParamsAdapter recordSetParamsAdapter = new RecordSetParamsAdapter(queryParams, AttributeSourceTypes.Object);
      IAttributeValueConverter attributeValueConverter = ServiceLocator.Get<IAttributeValueConverter>();
      IOrderedEnumerable<_Object> source = dataTable1.Rows.Cast<DataRow>().Select<DataRow, _Object>((System.Func<DataRow, _Object>) (o => new _Object((IAttributeCollection) new AttributeCollectionDataRowAdapter(o, (IRecordSetParamsAdapter) recordSetParamsAdapter, attributeValueConverter)))).OrderBy<_Object, string>((System.Func<_Object, string>) (o => string.Empty));
      if (queryParams.SortColumns != null && queryParams.SortColumns.Length != 0)
      {
        for (int index = 0; index < queryParams.SortColumns.Length; ++index)
        {
          int sortAttributeTypeID = AttributeTypeHelper.ConvertToAttributeTypeID(queryParams.SortColumns[index]);
          switch (queryParams.Orders[index])
          {
            case SortOrders.ASC:
              source = source.ThenBy<_Object, object>((System.Func<_Object, object>) (o => o.Attributes.GetAttributeValue(sortAttributeTypeID)));
              break;
            case SortOrders.DESC:
              source = source.ThenByDescending<_Object, object>((System.Func<_Object, object>) (o => o.Attributes.GetAttributeValue(sortAttributeTypeID)));
              break;
          }
        }
      }
      object[][] array = source.Select<_Object, object[]>((System.Func<_Object, object[]>) (o => ((AttributeCollectionDataRowAdapter) o.Attributes).DataRow.ItemArray)).ToArray<object[]>();
      dataTable1.Clear();
      foreach (object[] objArray in array)
        dataTable1.Rows.Add(objArray);
    }
    return dataTable1;
  }

  private Dictionary<int, List<long>> GetObjectTypes(ConditionStructure[] conditions)
  {
    Dictionary<int, List<long>> objectTypes = new Dictionary<int, List<long>>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(-1);
      objectCollection.LocalTypesMode = true;
      this.PrepareCollection(objectCollection);
      DBRecordSetParams paramSet = new DBRecordSetParams()
      {
        Columns = new object[2]
        {
          (object) ObligatoryObjectAttributes.F_OBJECT_ID,
          (object) ObligatoryObjectAttributes.F_OBJECT_TYPE
        },
        Conditions = conditions,
        RecordCount = -1
      };
      foreach (DataRow row in (InternalDataCollectionBase) objectCollection.Select(paramSet).Rows)
      {
        long int64Value = DataSetProcessor.GetInt64Value(row, 0, 0L);
        int int32Value = DataSetProcessor.GetInt32Value(row, 1, -1);
        List<long> longList = (List<long>) null;
        if (!objectTypes.TryGetValue(int32Value, out longList))
        {
          longList = new List<long>();
          objectTypes.Add(int32Value, longList);
        }
        longList.Add(int64Value);
      }
    }
    return objectTypes;
  }
}
