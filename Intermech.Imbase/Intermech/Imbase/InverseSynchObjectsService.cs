// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.InverseSynchObjectsService
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase;

public class InverseSynchObjectsService : BaseSynchObjectsService
{
  private List<long> _selectedObjs;
  private int _selectedTypeID = -1;
  private string _stepCaption = string.Empty;
  private int _count;
  private int _current;

  public List<int> AttributeIDs { get; set; }

  private InverseSynchObjectsService()
  {
    this._timerForServer = new System.Threading.Timer(new TimerCallback(this.On_timerForServer_Tick), (object) null, -1, -1);
  }

  public InverseSynchObjectsService(List<long> objIDs)
    : this()
  {
    this._selectedObjs = objIDs;
    int capacity = 1;
    if (objIDs != null)
      capacity = objIDs.Count;
    this._synchronizedObjects = new Dictionary<long, string>(capacity);
  }

  public InverseSynchObjectsService(int objTypeID)
    : this()
  {
    this._selectedTypeID = objTypeID;
    this._synchronizedObjects = new Dictionary<long, string>();
  }

  public override List<string> FilterDataSource
  {
    get
    {
      return new List<string>()
      {
        SynchStrHelper.AllValues,
        SynchStrHelper.Synchronized,
        SynchStrHelper.NotSynchronized,
        SynchStrHelper.NotNeedToSync
      };
    }
  }

  public override void SetFilter(string selectedItem)
  {
    if (this._dt == null)
      return;
    this._dt.DefaultView.RowFilter = selectedItem == SynchStrHelper.AllValues ? string.Empty : $"[{SynchStrHelper.COLUMN_NAME_STATUS}]='{selectedItem}'";
  }

  public override DataTable GridDataSource
  {
    get
    {
      if (this._dt == null)
      {
        this._dt = new DataTable();
        this._dt.Columns.AddRange(new DataColumn[5]
        {
          new DataColumn(SynchStrHelper.COLUMN_NAME_OBJECT_ID),
          new DataColumn(SynchStrHelper.COLUMN_NAME_CAPTION),
          new DataColumn(SynchStrHelper.COLUMN_NAME_IMBASE_ID),
          new DataColumn(SynchStrHelper.COLUMN_NAME_IMBASE_CAPTION),
          new DataColumn(SynchStrHelper.COLUMN_NAME_STATUS)
        });
      }
      return this._dt;
    }
  }

  public IInverseImbaseSynchObjectsService Srv
  {
    get
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        return sessionKeeper.Session.GetCustomService(typeof (IInverseImbaseSynchObjectsService)) as IInverseImbaseSynchObjectsService;
    }
  }

  public override void CustomizeGrid(DataGridView dgv)
  {
    dgv.Columns[0].HeaderText = this.COL_CAPTION_ID;
    dgv.Columns[0].Width = 200;
    dgv.Columns[1].HeaderText = this.COL_CAPTION_NAME;
    dgv.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    dgv.Columns[2].HeaderText = LocalizationHolder.rm.GetString("Imbase_ImbaseObject_ID");
    dgv.Columns[2].Width = 200;
    dgv.Columns[3].HeaderText = LocalizationHolder.rm.GetString("Imbase_ImbaseObject_Caption");
    dgv.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    dgv.Columns[4].HeaderText = this.COL_CAPTION_STATUS;
    dgv.Columns[4].Width = 200;
    dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;
  }

  public override string GetReport(DataRow row, int columnIndex)
  {
    long int64 = Convert.ToInt64(row[SynchStrHelper.COLUMN_NAME_OBJECT_ID]);
    return !this._synchronizedObjects.ContainsKey(int64) ? string.Empty : this._synchronizedObjects[int64];
  }

  public override async void StartTask()
  {
    base.StartTask();
    this._stepCaption = LocalizationHolder.rm.GetString("Imbase_Synch_Started");
    this._dataChangedCallback(this._stepCaption, 0, 0);
    try
    {
      await Task.Run((Action) (() => this.Start(this.AttributeIDs)));
    }
    finally
    {
      this.Processing = false;
      this._stepCaption = LocalizationHolder.rm.GetString("Imbase_Synch_Finished");
      if (this._dataChangedCallback != null)
        this._dataChangedCallback(this._stepCaption, this._count, this._count);
      this.OnProgressFinished();
    }
  }

  public override void StopTask()
  {
    base.StopTask();
    this._stepCaption = LocalizationHolder.rm.GetString("Imbase_Synch_Stoped");
    this.Srv.StopTask(this._taskGuid);
  }

  public override void AddResultRow(DataTable table)
  {
    if (table == null)
      return;
    foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
    {
      if (!this._synchronizedObjects.ContainsKey(Convert.ToInt64(row[SynchStrHelper.COLUMN_NAME_OBJECT_ID])))
      {
        this._synchronizedObjects.Add(Convert.ToInt64(row[SynchStrHelper.COLUMN_NAME_OBJECT_ID]), Convert.ToString(row[SynchStrHelper.COLUMN_NAME_REPORT]));
        this._dt.Rows.Add(row[SynchStrHelper.COLUMN_NAME_OBJECT_ID], row[SynchStrHelper.COLUMN_NAME_CAPTION], row[SynchStrHelper.COLUMN_NAME_IMBASE_ID], row[SynchStrHelper.COLUMN_NAME_IMBASE_CAPTION], row[SynchStrHelper.COLUMN_NAME_STATUS]);
      }
    }
  }

  private void On_timerForServer_Tick(object sender)
  {
    DataTable objectsProcessed = this.Srv.GetInfoAboutObjectsProcessed(this._taskGuid, out this._count, out this._current);
    if (objectsProcessed != null && this._addRowCallback != null)
      this._addRowCallback(objectsProcessed);
    if (this._dataChangedCallback == null)
      return;
    this._dataChangedCallback(this._stepCaption, this._count, this._current);
  }

  private void Start(List<int> attrIDs)
  {
    this._timerForServer.Change(1500, 3000);
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (this._selectedObjs != null)
        {
          this.Srv.UpdateInfo(sessionKeeper.Session.SessionGUID, this._taskGuid, this._selectedObjs, attrIDs);
        }
        else
        {
          if (this._selectedTypeID == -1)
            return;
          this.Srv.UpdateInfo(sessionKeeper.Session.SessionGUID, this._taskGuid, this._selectedTypeID, attrIDs);
        }
      }
    }
    finally
    {
      if (this.Processing)
      {
        this._timerForServer.Change(-1, -1);
        this.On_timerForServer_Tick((object) this._timerForServer);
      }
    }
  }
}
