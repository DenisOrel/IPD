// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.BaseSynchObjectsService
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase;

public abstract class BaseSynchObjectsService : IDisposable
{
  protected string COL_CAPTION_ID = LocalizationHolder.rm.GetString("Imbase_Version_Object_ID");
  protected string COL_CAPTION_NAME = LocalizationHolder.rm.GetString("Imbase_Object_Name");
  protected string COL_CAPTION_STATUS = LocalizationHolder.rm.GetString("Imbase_Synch_Status");
  protected Guid _taskGuid = Guid.NewGuid();
  protected DataTable _dt;
  protected Dictionary<long, string> _synchronizedObjects;
  protected Action<DataTable> _addRowCallback;
  protected Action<string, int, int> _dataChangedCallback;
  protected System.Threading.Timer _timerForServer;

  public void Dispose()
  {
    if (this._timerForServer == null)
      return;
    this._timerForServer.Change(-1, -1);
    this._timerForServer.Dispose();
    this._addRowCallback = (Action<DataTable>) null;
    this._dataChangedCallback = (Action<string, int, int>) null;
  }

  public abstract List<string> FilterDataSource { get; }

  public abstract void SetFilter(string selectedItem);

  public abstract DataTable GridDataSource { get; }

  public abstract void CustomizeGrid(DataGridView dgv);

  public abstract string GetReport(DataRow row, int columnIndex);

  public abstract void AddResultRow(DataTable table);

  public virtual void StartTask() => this.Processing = true;

  public virtual void StopTask() => this.Processing = false;

  public bool Processing { get; set; }

  public event Action OnFinished;

  public void Subscribe(Action<DataTable> addRowCallback) => this._addRowCallback = addRowCallback;

  public void Subscribe(Action<string, int, int> dataChangedCallback)
  {
    this._dataChangedCallback = dataChangedCallback;
  }

  protected void OnProgressFinished()
  {
    Action onFinished = this.OnFinished;
    if (onFinished == null)
      return;
    onFinished();
  }
}
