// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.ProcessedItemsInfo
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using System;
using System.Data;

#nullable disable
namespace Intermech.Imbase.Server;

internal class ProcessedItemsInfo
{
  private int _countByType;
  internal DataTable ProcessedInfo;

  internal int Count { get; set; }

  internal int CountByType
  {
    get => this._countByType;
    set => this.Current = this._countByType = value;
  }

  internal int Current { get; set; }

  internal bool TaskRunning { get; set; }

  internal DateTime FinishedTime { get; set; }

  public ProcessedItemsInfo()
  {
    this.ProcessedInfo = new DataTable();
    this.ProcessedInfo.Columns.AddRange(new DataColumn[4]
    {
      new DataColumn(SynchStrHelper.COLUMN_NAME_OBJECT_ID),
      new DataColumn(SynchStrHelper.COLUMN_NAME_CAPTION),
      new DataColumn(SynchStrHelper.COLUMN_NAME_STATUS),
      new DataColumn(SynchStrHelper.COLUMN_NAME_REPORT)
    });
    this.Count = this.CountByType = this.Current = 0;
    this.TaskRunning = true;
  }

  internal void AddNotSynchType(int typeID, string description)
  {
    lock (this.ProcessedInfo)
      this.ProcessedInfo.Rows.Add((object) typeID, (object) MetaDataHelper.GetObjectTypeName(typeID), (object) SynchStrHelper.NotSynchronized, (object) description);
  }

  internal void AddProcessedObject(
    long objID,
    string objCaption,
    string status,
    string description)
  {
    lock (this.ProcessedInfo)
      this.ProcessedInfo.Rows.Add((object) objID, (object) objCaption, (object) status, (object) description);
  }

  internal DataTable ProcessedInfoCopy()
  {
    lock (this.ProcessedInfo)
    {
      DataTable dataTable = this.ProcessedInfo.Copy();
      this.ProcessedInfo.Clear();
      return dataTable;
    }
  }
}
