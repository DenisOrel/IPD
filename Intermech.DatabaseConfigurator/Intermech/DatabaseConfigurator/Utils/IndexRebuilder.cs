// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.Utils.IndexRebuilder
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using ImSSP;
using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Threading;
using System.Windows.Forms;

#nullable disable
namespace Intermech.DatabaseConfigurator.Utils;

public class IndexRebuilder : CustomBackgroundTask
{
  private Guid _CallSession;
  public static bool Indexing;

  public IndexRebuilder()
  {
    this._name = LocalizationHolder.rm.GetString("DatabaseConfigurator_131");
    this._canStop = true;
    this._canResume = false;
    this._canPause = false;
    this._minValue = 0;
    this._value = 0;
    this._maxValue = 100;
  }

  public void RebuildIndex()
  {
    this._state = BackgroundTaskState.Running;
    this.OnChanged(BackgroundTaskChangedType.State);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(sessionKeeper.Session.GetCustomService(typeof (IAdminUtilsService)) is IAdminUtilsService customService))
        throw new Exception(LocalizationHolder.rm.GetString(sc_5894.ssp_imclient_5895()));
      new Thread(new ThreadStart(this.CallRebuild))
      {
        IsBackground = true
      }.Start();
      OperationStateInfo indexingStateInfo;
      do
      {
        for (int index = 0; index < 100; ++index)
        {
          Thread.Sleep(20);
          if (this._state == BackgroundTaskState.Stopped)
          {
            customService.StopRebuildIndexes(this._CallSession);
            break;
          }
        }
        indexingStateInfo = customService.IndexingStateInfo;
        if (this.State == BackgroundTaskState.Running)
        {
          if (this._name != indexingStateInfo.OperationName)
          {
            this._name = indexingStateInfo.OperationName;
            this.OnChanged(BackgroundTaskChangedType.Text);
          }
          if (this._value != indexingStateInfo.CurrentUnit)
          {
            this._maxValue = indexingStateInfo.MaxUnits;
            this._value = indexingStateInfo.CurrentUnit;
            this.OnChanged(BackgroundTaskChangedType.Value);
          }
        }
      }
      while (indexingStateInfo.State == OperationStates.Processing && this._state == BackgroundTaskState.Running);
    }
  }

  public void CallRebuild()
  {
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        this._CallSession = sessionKeeper.Session.SessionGUID;
        IAdminUtilsService customService = sessionKeeper.Session.GetCustomService(typeof (IAdminUtilsService)) as IAdminUtilsService;
        try
        {
          IndexRebuilder.Indexing = true;
          string[] strArray = customService.RebuildIndexes(this._CallSession);
          if (this.State == BackgroundTaskState.Stopped)
          {
            this.State = BackgroundTaskState.Terminated;
            this.OnChanged(BackgroundTaskChangedType.State);
            int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("DatabaseConfigurator_133"), LocalizationHolder.rm.GetString("DatabaseConfigurator_134"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
          }
          else
          {
            this.State = BackgroundTaskState.Terminated;
            this.OnChanged(BackgroundTaskChangedType.State);
            this.Value = (object) this._maxValue;
            this.OnChanged(BackgroundTaskChangedType.Value);
            if (strArray.Length > 2)
            {
              int num1 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("DatabaseConfigurator_135"), LocalizationHolder.rm.GetString("DatabaseConfigurator_136"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
              int num2 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("DatabaseConfigurator_137"), "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
          }
          IOutputView service = ServicesManager.GetService(typeof (IOutputView)) as IOutputView;
          string category = LocalizationHolder.rm.GetString("DatabaseConfigurator_138");
          foreach (string text in strArray)
            service.WriteString(category, text);
          service.Activate(category);
          service.ShowView();
        }
        finally
        {
          IndexRebuilder.Indexing = false;
          this.OnChanged(BackgroundTaskChangedType.Dispose);
        }
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  public override void Stop()
  {
    base.Stop();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      (sessionKeeper.Session.GetCustomService(typeof (IAdminUtilsService)) as IAdminUtilsService).StopRebuildIndexes(this._CallSession);
  }
}
