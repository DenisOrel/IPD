// Decompiled with JetBrains decompiler
// Type: Intermech.MRP.Server.Compositions.Filtration.FiltrationCompositionService
// Assembly: Intermech.MRP.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 90CF20BA-CEDA-4320-95C8-661A6AE661C2
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.MRP.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using Intermech.MRP2;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.MRP.Server.Compositions.Filtration;

internal class FiltrationCompositionService
{
  public static void PrepareFilterByDate(object sender, BeforeRecordsSelectEventArgs args)
  {
    if (args.OldParameters.Tags == null || MRP2Consts.attrIdEndDate == 0 || args.OldParameters.Tags[(object) "CC4B5C20-3E62-4436-89E8-699262510FD5"] == null || !Convert.ToBoolean(args.OldParameters.Tags[(object) "CC4B5C20-3E62-4436-89E8-699262510FD5"]) || DBRecordSet.AttributeColumnExists(args.OldParameters, (object) MRP2Consts.attrIdEndDate, AttributeSourceTypes.Relation))
      return;
    ColumnDescriptor[] AddColumns = new ColumnDescriptor[1]
    {
      new ColumnDescriptor((object) MRP2Consts.attrIdEndDate, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, 0)
    };
    args.OldParameters.AddColumnDescriptors(AddColumns, (List<int>) null);
    args.NewParameters = new DBRecordSetParams?(args.OldParameters);
  }

  public static void FilterByDate(DataTable table, DBRecordSetParams parameters)
  {
    if (parameters.Tags == null || MRP2Consts.attrIdEndDate == 0 || parameters.Tags[(object) "CC4B5C20-3E62-4436-89E8-699262510FD5"] == null || !Convert.ToBoolean(parameters.Tags[(object) "CC4B5C20-3E62-4436-89E8-699262510FD5"]) || parameters.Tags[(object) "85357DBA-2685-4F94-8B40-7889D08B322A"] == null)
      return;
    DateTime dateTime = Convert.ToDateTime(parameters.Tags[(object) "85357DBA-2685-4F94-8B40-7889D08B322A"]);
    int columnIndex = DBRecordSet.AttributeColumnIndex(parameters, (object) MRP2Consts.attrIdEndDate, AttributeSourceTypes.Relation, table);
    if (columnIndex < 0)
      return;
    table.BeginLoadData();
    List<DataRow> dataRowList = new List<DataRow>();
    foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
    {
      if (row[columnIndex] != DBNull.Value && DataSetProcessor.GetDateTimeValue(row[columnIndex], DateTime.Now) < dateTime)
        dataRowList.Add(row);
    }
    dataRowList.ForEach((Action<DataRow>) (row => table.Rows.Remove(row)));
    table.EndLoadData();
    table.AcceptChanges();
  }
}
