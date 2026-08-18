// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.Email.DownloadTask
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.Client.Core;
using Intermech.Controls;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Workflow;
using Intermech.Interfaces.Workflow.Email;
using System;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Client.Email;

internal class DownloadTask : CustomBackgroundTask
{
  private string _email;
  private EmailNode _node;
  private Guid _processID;

  public DownloadTask(string email, EmailNode node)
  {
    this._node = node;
    this._email = email;
    this._processID = Guid.NewGuid();
    this._name = string.Format(LocalizationHolder.rm.GetString("Workflow.Client_66"), (object) email);
    this._canStop = true;
    this._canResume = false;
    this._canPause = false;
    this._minValue = 0;
    this._value = 0;
    this._maxValue = 100;
  }

  public void Download(object arg)
  {
    IUserSession userSession = (IUserSession) arg;
    IEmailDownloadService customService = (IEmailDownloadService) userSession.GetCustomService(typeof (IEmailDownloadService));
    try
    {
      customService.StartDownload(userSession.SessionGUID, this._processID, this._email, false);
      this._state = BackgroundTaskState.Running;
      this.OnChanged(BackgroundTaskChangedType.State);
      bool flag = true;
      while (flag)
      {
        EmailDownloadProperties downloadProperties = customService.GetDownloadProperties(this._processID);
        if (downloadProperties == null)
          break;
        switch (downloadProperties.State)
        {
          case EmailDownloadState.Downloading:
            this._value = downloadProperties.Percent > this._maxValue ? this._maxValue : downloadProperties.Percent;
            this.OnChanged(BackgroundTaskChangedType.Value);
            continue;
          case EmailDownloadState.Error:
            this._state = BackgroundTaskState.Error;
            this.OnChanged(BackgroundTaskChangedType.State);
            ExceptionHelper.ExceptionService.ShowException(downloadProperties.ErrorException);
            flag = false;
            continue;
          case EmailDownloadState.Completed:
            this._value = downloadProperties.Percent > this._maxValue ? this._maxValue : downloadProperties.Percent;
            this.OnChanged(BackgroundTaskChangedType.Value);
            int num = (int) IMMessageBox.Show(this._name, string.Format(LocalizationHolder.rm.GetString("Workflow.Client_67"), (object) downloadProperties.CountMessages), MessageBoxButtons.OK, IMMessageBoxImage.Information);
            if (downloadProperties.CountMessages > 0)
              (ApplicationServices.Container.GetService(typeof (INotificationService)) as INotificationService).FireEvent((object) "EmailImported", (NotificationEventArgs) new EmailImportedEventArgs("EmailImported", this._email));
            this._state = BackgroundTaskState.Terminated;
            this.OnChanged(BackgroundTaskChangedType.State);
            flag = false;
            continue;
          default:
            continue;
        }
      }
    }
    catch (Exception ex)
    {
      this._state = BackgroundTaskState.Error;
      this.OnChanged(BackgroundTaskChangedType.State);
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
    finally
    {
      customService?.CompleteDownload(this._processID);
      this.OnChanged(BackgroundTaskChangedType.Dispose);
    }
  }

  public override void Stop()
  {
    if (this.State != BackgroundTaskState.Running)
      return;
    ((IEmailDownloadService) (ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IEmailDownloadService))).StopDownload(this._processID);
    base.Stop();
  }
}
