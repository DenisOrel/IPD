// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.TableLoader
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Imbase.Server;

internal static class TableLoader
{
  internal static void LoadTable(
    IUserSession session,
    long objectId,
    string filter,
    out DataTable recordsTable,
    out AttributeTypeProperties[] columnsAttributes,
    ref ImbaseKeyInfo keyInfo)
  {
    long linkId = -1;
    long tableId = -1;
    recordsTable = (DataTable) null;
    columnsAttributes = (AttributeTypeProperties[]) null;
    TableLoadHelper.CheckObjectId(session, objectId, ref linkId, ref tableId);
    DataSet tables = TableLoadHelper.GetTables(session, tableId, false);
    tables.RemotingFormat = SerializationFormat.Binary;
    recordsTable = tables.Tables["IMS_DATA"];
    DataTable table = tables.Tables["IMS_ATTR_TYPES"];
    recordsTable.ExtendedProperties.Add((object) "CalcContext", (object) new CalcContext(linkId));
    TableLoadHelper.AssignAttributes(session, linkId, tableId, recordsTable, table, out columnsAttributes, new List<CalculatedColumn>(), ref keyInfo);
    recordsTable.ExtendedProperties.Remove((object) "CalcContext");
    foreach (DataColumn column in (InternalDataCollectionBase) recordsTable.Columns)
      column.ColumnName = column.Caption;
    recordsTable.ExtendedProperties[(object) -2] = (object) tableId;
    recordsTable.ExtendedProperties[(object) Intermech.Imbase.Consts.ImbaseLinkRefAttID] = (object) linkId;
    if (!string.IsNullOrEmpty(filter))
    {
      DataRow[] dataRowArray = recordsTable.Select(filter);
      recordsTable = recordsTable.Clone();
      int length = dataRowArray.Length;
      for (int index = 0; index < length; ++index)
        recordsTable.Rows.Add(dataRowArray[index].ItemArray);
    }
    recordsTable.AcceptChanges();
    recordsTable.RemotingFormat = SerializationFormat.Binary;
    keyInfo.TableId = tableId;
    keyInfo.LinkId = linkId;
  }
}
