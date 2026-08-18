// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Services.DataProviders.Versions.TechObjInfoItemsVersionsProvider
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

#nullable disable
namespace Intermech.TechCard.Client.Services.DataProviders.Versions;

internal class TechObjInfoItemsVersionsProvider : 
  ITechCardDataEnumerableProvider<ObjInfoIDItem>,
  ITechCardDataProvider<IEnumerable<ObjInfoIDItem>>
{
  /// <summary>
  /// 
  /// </summary>
  private readonly IEnumerable<ObjInfoItem> _objInfoItems;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objInfoItems"></param>
  public TechObjInfoItemsVersionsProvider([NotNull] IEnumerable<ObjInfoItem> objInfoItems)
  {
    this._objInfoItems = objInfoItems;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public IEnumerable<ObjInfoIDItem> Execute()
  {
    List<ObjInfoIDItem> objInfoIdItemList = new List<ObjInfoIDItem>();
    if (!this._objInfoItems.Any<ObjInfoItem>())
      return (IEnumerable<ObjInfoIDItem>) objInfoIdItemList;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ObjInfoIDItem[] array = this._objInfoItems.Select<ObjInfoItem, ObjInfoIDItem>((System.Func<ObjInfoItem, ObjInfoIDItem>) (item => new ObjInfoIDItem((TypedInfoItem) item))).ToArray<ObjInfoIDItem>();
      ObjInfoHelper.UpdateUnknownInfo((IEnumerable<ObjInfoItem>) array, sessionKeeper.Session);
      ColumnDescriptor[] columns = new ColumnDescriptor[3]
      {
        new ColumnDescriptor((object) -2, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
        new ColumnDescriptor((object) -7, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
        new ColumnDescriptor((object) -3, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0)
      };
      IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(-1);
      objectCollection.LocalTypesMode = true;
      objectCollection.ShowAllModifications = true;
      DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(-3, RelationalOperators.In, (object) ((IEnumerable<ObjInfoIDItem>) array).Select<ObjInfoIDItem, long>((System.Func<ObjInfoIDItem, long>) (item => item.ID)).ToArray<long>(), (object) null, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Object)
      }, columns);
      DataTable dataTable = objectCollection.Select(paramSet);
      if (dataTable == null)
        return (IEnumerable<ObjInfoIDItem>) objInfoIdItemList;
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        objInfoIdItemList.Add(new ObjInfoIDItem(Convert.ToInt64(row["F_OBJECT_ID"]), Convert.ToInt32(row["F_OBJECT_TYPE"]), Convert.ToInt64(row["F_ID"])));
    }
    return (IEnumerable<ObjInfoIDItem>) objInfoIdItemList;
  }
}
