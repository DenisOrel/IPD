// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.SynchObjectsService
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase;

public class SynchObjectsService : BaseSynchObjectsService
{
  private Dictionary<int, List<long>> _selectedObjs;
  private int _selectedTypeID = -1;
  private bool _createVersion;
  private int _attrID;
  private string _stepCaption = string.Empty;
  private int _count;
  private int _current;

  public IImbaseSynchObjectsService Srv
  {
    get
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        return sessionKeeper.Session.GetCustomService(typeof (IImbaseSynchObjectsService)) as IImbaseSynchObjectsService;
    }
  }

  private SynchObjectsService(bool createVersion, int attrID)
  {
    this._createVersion = createVersion;
    this._attrID = attrID;
    this._timerForServer = new System.Threading.Timer(new TimerCallback(this.On_timerForServer_Tick), (object) null, -1, -1);
  }

  public SynchObjectsService(Dictionary<int, List<long>> objs, bool createVersion, int attrID)
    : this(createVersion, attrID)
  {
    this._selectedObjs = objs;
    this._synchronizedObjects = new Dictionary<long, string>(objs.SelectMany<KeyValuePair<int, List<long>>, long>((System.Func<KeyValuePair<int, List<long>>, IEnumerable<long>>) (x => x.Value.Select<long, long>((System.Func<long, long>) (y => y)))).Count<long>());
  }

  public SynchObjectsService(int objTypeID, bool createVersion, int attrID)
    : this(createVersion, attrID)
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
        this._dt.Columns.AddRange(new DataColumn[3]
        {
          new DataColumn(SynchStrHelper.COLUMN_NAME_OBJECT_ID),
          new DataColumn(SynchStrHelper.COLUMN_NAME_CAPTION),
          new DataColumn(SynchStrHelper.COLUMN_NAME_STATUS)
        });
      }
      return this._dt;
    }
  }

  public override void CustomizeGrid(DataGridView dgv)
  {
    dgv.Columns[0].HeaderText = this.COL_CAPTION_ID;
    dgv.Columns[0].Width = 200;
    dgv.Columns[1].HeaderText = this.COL_CAPTION_NAME;
    dgv.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    dgv.Columns[2].HeaderText = this.COL_CAPTION_STATUS;
    dgv.Columns[2].Width = 200;
    dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
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
      await Task.Run((Action) (() => this.Start(this._createVersion, this._attrID)));
    }
    finally
    {
      this.Processing = false;
      this._stepCaption = LocalizationHolder.rm.GetString("Imbase_Synch_Finished");
      Action<string, int, int> dataChangedCallback = this._dataChangedCallback;
      if (dataChangedCallback != null)
        dataChangedCallback(this._stepCaption, this._count, this._count);
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
      this._synchronizedObjects.Add(Convert.ToInt64(row[SynchStrHelper.COLUMN_NAME_OBJECT_ID]), Convert.ToString(row[SynchStrHelper.COLUMN_NAME_REPORT]));
      this._dt.Rows.Add(row[SynchStrHelper.COLUMN_NAME_OBJECT_ID], row[SynchStrHelper.COLUMN_NAME_CAPTION], row[SynchStrHelper.COLUMN_NAME_STATUS]);
    }
  }

  private void On_timerForServer_Tick(object sender)
  {
    try
    {
      DataTable objectsProcessed = this.Srv.GetInfoAboutObjectsProcessed(this._taskGuid, out this._count, out this._current);
      if (objectsProcessed != null)
      {
        Action<DataTable> addRowCallback = this._addRowCallback;
        if (addRowCallback != null)
          addRowCallback(objectsProcessed);
      }
      Action<string, int, int> dataChangedCallback = this._dataChangedCallback;
      if (dataChangedCallback == null)
        return;
      dataChangedCallback(this._stepCaption, this._count, this._current);
    }
    catch
    {
    }
  }

  private void Start(bool createVersion, int attrID)
  {
    try
    {
      this._timerForServer.Change(1500, 3000);
      try
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          if (this._selectedObjs != null)
          {
            this.Srv.SynchronizeObjects(sessionKeeper.Session.SessionGUID, this._taskGuid, this._selectedObjs, createVersion, attrID);
          }
          else
          {
            if (this._selectedTypeID == -1)
              return;
            this.Srv.SynchronizeObjects(sessionKeeper.Session.SessionGUID, this._taskGuid, this._selectedTypeID, createVersion, attrID);
          }
        }
      }
      finally
      {
        if (!this.Processing)
          throw new OperationCanceledException();
        this._timerForServer.Change(-1, -1);
        this.On_timerForServer_Tick((object) this._timerForServer);
      }
    }
    catch (OperationCanceledException ex)
    {
    }
  }
}
