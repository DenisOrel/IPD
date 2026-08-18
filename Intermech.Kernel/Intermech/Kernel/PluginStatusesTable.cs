// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.PluginStatusesTable
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Kernel;

[Serializable]
public sealed class PluginStatusesTable : LongLifeObject, IPluginStatusesTable
{
  [NonSerialized]
  internal Dictionary<string, DataTable> FPluginStatuses = new Dictionary<string, DataTable>();

  private void CopyDataRow(DataRow source, DataRow dest, List<int> exceptions)
  {
    if (source == null || dest == null)
      return;
    for (int columnIndex = 0; columnIndex < source.Table.Columns.Count; ++columnIndex)
    {
      if (exceptions == null || exceptions.IndexOf(columnIndex) < 0)
        dest[columnIndex] = source[columnIndex];
    }
  }

  public DataTable GetPluginStatusesTable(
    string PluginGuid,
    bool IncludeIcons,
    params int[] statuses)
  {
    DataTable dataTable1 = this.CreateDataTable();
    DataTable dataTable2 = this.CreateDataTable(PluginGuid);
    List<int> exceptions = new List<int>(0);
    if (!IncludeIcons)
      exceptions.Add(dataTable2.Columns.IndexOf(PluginStatusesTableFields.columnImage));
    if (statuses != null && statuses.Length != 0)
    {
      for (int index = 0; index < statuses.Length; ++index)
      {
        DataRow source = dataTable2.Rows.Find((object) statuses[index]);
        if (source != null)
        {
          DataRow dataRow = dataTable1.NewRow();
          this.CopyDataRow(source, dataRow, exceptions);
          dataTable1.Rows.Add(dataRow);
        }
      }
    }
    else
    {
      foreach (DataRow row in (InternalDataCollectionBase) dataTable2.Rows)
      {
        DataRow dataRow = dataTable1.NewRow();
        this.CopyDataRow(row, dataRow, exceptions);
        dataTable1.Rows.Add(dataRow);
      }
    }
    return dataTable1;
  }

  internal DataTable CreateDataTable()
  {
    DataTable dataTable = new DataTable(PluginStatusesTableFields.tableName);
    dataTable.Columns.Add(new DataColumn()
    {
      AllowDBNull = false,
      AutoIncrement = false,
      Caption = PluginStatusesTableFields.captionStatus,
      ColumnMapping = MappingType.Attribute,
      ColumnName = PluginStatusesTableFields.columnStatus,
      DataType = typeof (int),
      ReadOnly = false,
      Unique = true
    });
    dataTable.Columns.Add(new DataColumn()
    {
      AllowDBNull = true,
      AutoIncrement = false,
      Caption = PluginStatusesTableFields.captionDescription,
      ColumnMapping = MappingType.Attribute,
      ColumnName = PluginStatusesTableFields.columnDescription,
      DataType = typeof (string),
      ReadOnly = false,
      Unique = false
    });
    dataTable.Columns.Add(new DataColumn()
    {
      AllowDBNull = true,
      AutoIncrement = false,
      Caption = PluginStatusesTableFields.captionImage,
      ColumnMapping = MappingType.Element,
      ColumnName = PluginStatusesTableFields.columnImage,
      DataType = typeof (byte[]),
      ReadOnly = false,
      Unique = false
    });
    dataTable.Columns.Add(new DataColumn()
    {
      AllowDBNull = true,
      AutoIncrement = false,
      Caption = PluginStatusesTableFields.captionImageCRC32,
      ColumnMapping = MappingType.Attribute,
      ColumnName = PluginStatusesTableFields.columnImageCRC32,
      DataType = typeof (uint),
      ReadOnly = false,
      Unique = false
    });
    DataColumn[] dataColumnArray = new DataColumn[1]
    {
      dataTable.Columns[PluginStatusesTableFields.columnStatus]
    };
    dataTable.PrimaryKey = dataColumnArray;
    dataTable.AcceptChanges();
    return dataTable;
  }

  internal DataTable CreateDataTable(string PluginGuid)
  {
    if (this.FPluginStatuses == null)
      this.FPluginStatuses = new Dictionary<string, DataTable>();
    if (this.FPluginStatuses.ContainsKey(PluginGuid))
      return this.FPluginStatuses[PluginGuid];
    DataTable dataTable = this.CreateDataTable();
    this.FPluginStatuses[PluginGuid] = dataTable;
    return dataTable;
  }

  public void AddStatus(string PluginGuid, int status, string description, byte[] image)
  {
    DataTable dataTable = this.CreateDataTable(PluginGuid);
    DataRow row = dataTable.Rows.Find((object) status);
    if (row == null)
    {
      row = dataTable.NewRow();
      row[PluginStatusesTableFields.columnStatus] = (object) status;
      dataTable.Rows.Add(row);
    }
    row[PluginStatusesTableFields.columnDescription] = (object) description;
    if (image != null && image.Length != 0)
    {
      row[PluginStatusesTableFields.columnImage] = (object) image;
      row[PluginStatusesTableFields.columnImageCRC32] = (object) MyHashCRC32.ArrayHash(image);
    }
    else
    {
      row[PluginStatusesTableFields.columnImage] = (object) DBNull.Value;
      row[PluginStatusesTableFields.columnImageCRC32] = (object) 0;
    }
  }

  public void RemoveStatus(string PluginGuid, int status)
  {
    DataTable dataTable = this.CreateDataTable(PluginGuid);
    DataRow row = dataTable.Rows.Find((object) status);
    if (row == null)
      return;
    dataTable.Rows.Remove(row);
  }

  public void RemoveStatuses(string PluginGuid)
  {
    if (this.FPluginStatuses == null || !this.FPluginStatuses.ContainsKey(PluginGuid))
      return;
    this.FPluginStatuses.Remove(PluginGuid);
  }
}
