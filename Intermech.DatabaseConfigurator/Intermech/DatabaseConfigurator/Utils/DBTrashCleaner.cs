// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.Utils.DBTrashCleaner
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

public class DBTrashCleaner : CustomBackgroundTask
{
  private Guid _CallSession;

  public DBTrashCleaner()
  {
    this._name = LocalizationHolder.rm.GetString("DatabaseConfigurator_123");
    this._canStop = true;
    this._canResume = false;
    this._canPause = false;
    this._minValue = 0;
    this._value = 0;
    this._maxValue = 0;
  }

  public void ClearTrash()
  {
    this._state = BackgroundTaskState.Running;
    this.OnChanged(BackgroundTaskChangedType.State);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(sessionKeeper.Session.GetCustomService(typeof (IAdminUtilsService)) is IAdminUtilsService customService))
        throw new Exception(LocalizationHolder.rm.GetString(sc_5892.ssp_imclient_5893()));
      new Thread(new ThreadStart(this.CallClear))
      {
        IsBackground = true
      }.Start();
      OperationStateInfo clearingStateInfo;
      do
      {
        for (int index = 0; index < 100; ++index)
        {
          Thread.Sleep(20);
          if (this._state == BackgroundTaskState.Stopped)
          {
            customService.StopClearTrash(this._CallSession);
            break;
          }
        }
        clearingStateInfo = customService.ClearingStateInfo;
        if (this.State == BackgroundTaskState.Running)
        {
          if (this._name != clearingStateInfo.OperationName)
          {
            this._name = clearingStateInfo.OperationName;
            this.OnChanged(BackgroundTaskChangedType.Text);
          }
          if (this._value != clearingStateInfo.CurrentUnit)
          {
            this._maxValue = clearingStateInfo.MaxUnits;
            this._value = clearingStateInfo.CurrentUnit;
            this.OnChanged(BackgroundTaskChangedType.Value);
          }
        }
      }
      while (clearingStateInfo.State == OperationStates.Processing && this._state == BackgroundTaskState.Running);
    }
  }

  public void CallClear()
  {
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        this._CallSession = sessionKeeper.Session.SessionGUID;
        IAdminUtilsService customService = sessionKeeper.Session.GetCustomService(typeof (IAdminUtilsService)) as IAdminUtilsService;
        try
        {
          string[] strArray = customService.ClearTrash(this._CallSession);
          if (this.State == BackgroundTaskState.Stopped)
          {
            this.State = BackgroundTaskState.Terminated;
            this.OnChanged(BackgroundTaskChangedType.State);
            int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("DatabaseConfigurator_125"), LocalizationHolder.rm.GetString("DatabaseConfigurator_126"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
          }
          else
          {
            this.State = BackgroundTaskState.Terminated;
            this.OnChanged(BackgroundTaskChangedType.State);
            this.Value = (object) this._maxValue;
            this.OnChanged(BackgroundTaskChangedType.Value);
            if (strArray.Length > 2)
            {
              int num1 = (int) MessageBox.Show("Удаление устаревших данных завершено с сообщениями или ошибками.", LocalizationHolder.rm.GetString("DatabaseConfigurator_128"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
              int num2 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("DatabaseConfigurator_129"), "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
          }
          IOutputView service = ServicesManager.GetService(typeof (IOutputView)) as IOutputView;
          string category = LocalizationHolder.rm.GetString("DatabaseConfigurator_130");
          foreach (string text in strArray)
            service.WriteString(category, text);
          service.Activate(category);
          service.ShowView();
        }
        finally
        {
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
      (sessionKeeper.Session.GetCustomService(typeof (IAdminUtilsService)) as IAdminUtilsService).StopClearTrash(this._CallSession);
  }
}
