// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.BackgroundTask.ImbaseIndexesBackgroundTask
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Imbase.Indexes;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Imbase;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.BackgroundTask;

internal class ImbaseIndexesBackgroundTask : Control, IBackgroundTask
{
  private IImbaseIndexingService _iIIS;
  private System.Timers.Timer _timer = new System.Timers.Timer(1000.0);
  protected Guid _taskGuid = Guid.NewGuid();

  public ImbaseIndexesBackgroundTask(IndexesHelper helper)
  {
    this.CreateHandle();
    this.Name = LocalizationHolder.rm.GetString("Imbase_Indexing_Catalog");
    this.MinimumValue = 0;
    this.MaximumValue = 100;
    this.Value = (object) 0;
    this.Result = (object) 1;
    this.ShowMode = BackgroundTaskShowMode.Progress;
    this.ImageIndex = helper != null ? helper.ImageIndex : -1;
    this._timer.Elapsed += new ElapsedEventHandler(this.On_timer_Elapsed);
    this.StartTask(helper);
  }

  private void On_timer_Elapsed(object sender, ElapsedEventArgs e)
  {
    int nState = 0;
    string text = string.Empty;
    this.Value = (object) this._iIIS.GetCompleted(this._taskGuid, out nState, out text);
    this.Name = text;
    if (nState > 0)
    {
      this.OnChanged(BackgroundTaskChangedType.Text);
      this.OnChanged(BackgroundTaskChangedType.State);
      this.OnChanged(BackgroundTaskChangedType.Value);
    }
    else
    {
      this.RemoveTask();
      this.ShowResult();
    }
  }

  private void RemoveTask()
  {
    this._timer.Stop();
    this._timer.Elapsed -= new ElapsedEventHandler(this.On_timer_Elapsed);
    this.OnChanged(BackgroundTaskChangedType.Dispose);
  }

  private void StartTask(IndexesHelper helper)
  {
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        this._iIIS = sessionKeeper.Session.GetCustomService(typeof (IImbaseIndexingService)) as IImbaseIndexingService;
        if (this._iIIS == null)
          throw new Exception(LocalizationHolder.rm.GetString("Imbase_Null_Indexing_Service"));
        switch (helper.Actions)
        {
          case IndexesStatus.Added:
            this._iIIS.Add(sessionKeeper.Session.SessionGUID, this._taskGuid, helper.SourceID, helper.AddedIndexes);
            break;
          case IndexesStatus.Removed:
            this._iIIS.Remove(sessionKeeper.Session.SessionGUID, this._taskGuid, helper.SourceID, helper.RemovedIndexes);
            break;
          case IndexesStatus.Changed:
            this._iIIS.UpdateFlags(sessionKeeper.Session.SessionGUID, this._taskGuid, helper.SourceID, helper.ChangedIndexes);
            break;
          case IndexesStatus.Update:
            this._iIIS.Update(sessionKeeper.Session.SessionGUID, this._taskGuid, helper.SourceID);
            break;
          case IndexesStatus.UpdateLinkData:
            this._iIIS.UpdateAfterTableRefCreated(sessionKeeper.Session.SessionGUID, this._taskGuid, helper.SourceID, true);
            break;
          case IndexesStatus.UpdateTableData:
            this._iIIS.UpdateAfterTableDataChanged(sessionKeeper.Session.SessionGUID, this._taskGuid, helper.SourceID, helper.DeletedRowNums, helper.DeletedColumns);
            break;
          case IndexesStatus.UpdateAfterCopyMove:
            this._iIIS.UpdateAfterCopiedMoved(sessionKeeper.Session.SessionGUID, this._taskGuid, helper.PrevCatalogID, helper.SourceID, helper.PastedObjIDs);
            break;
        }
        this.Resume();
      }
    }
    catch (Exception ex)
    {
      this.State = BackgroundTaskState.Error;
      this.RemoveTask();
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  private void ShowResult()
  {
    if (!(ServicesManager.GetService(typeof (IOutputView)) is IOutputView service))
      return;
    List<Exception> result = this._iIIS.GetResult(this._taskGuid);
    if (result == null)
      return;
    StringBuilder stringBuilder = new StringBuilder();
    foreach (Exception exception in result)
    {
      if (exception is IndexingException indexingException)
      {
        stringBuilder.AppendLine(indexingException.Message);
        if (!string.IsNullOrEmpty(indexingException.ComputerName))
          stringBuilder.AppendLine(string.Format(LocalizationHolder.rm.GetString("Imbase_Indexing_ComputerName"), (object) indexingException.ComputerName));
        if (!string.IsNullOrEmpty(indexingException.TaskName))
          stringBuilder.AppendLine(string.Format(LocalizationHolder.rm.GetString("Imbase_Indexing_TaskName"), (object) indexingException.TaskName));
        if (indexingException.InnerException != null)
          stringBuilder.AppendLine(indexingException.InnerException.Message);
      }
    }
    string category = LocalizationHolder.rm.GetString("Imbase_Indexing_Caption");
    service.ClearText(category);
    service.WriteString(category, stringBuilder.ToString());
    service.Activate(category);
    service.ShowView();
  }

  protected void OnChanged(BackgroundTaskChangedType type)
  {
    BackgroundTaskChangedEventHandler changed = this.Changed;
    if (changed == null)
      return;
    changed((object) this, type);
  }

  public event BackgroundTaskChangedEventHandler Changed;

  public int ImageIndex { get; private set; }

  public new string Name { get; private set; }

  public int MaximumValue { get; set; }

  public int MinimumValue { get; set; }

  public object Value { get; set; }

  public object Result { get; set; }

  public BackgroundTaskState State { get; set; }

  public BackgroundTaskShowMode ShowMode { get; private set; }

  public bool Active => this.State == BackgroundTaskState.Running;

  public void SetMaxMin(int max, int min)
  {
  }

  public bool CanStop() => true;

  public bool CanPause() => false;

  public bool CanResume() => false;

  public bool CanTerminate() => true;

  public void Stop()
  {
    this.State = BackgroundTaskState.Stopped;
    this._iIIS.StopTask(this._taskGuid);
    this.On_timer_Elapsed((object) null, (ElapsedEventArgs) null);
  }

  public void Pause()
  {
  }

  public void Resume()
  {
    this.State = BackgroundTaskState.Running;
    this._timer.Start();
  }

  public void Terminate()
  {
    this.State = BackgroundTaskState.Terminated;
    this._iIIS.RemoveAfterComplete(this._taskGuid);
    this.RemoveTask();
  }
}
