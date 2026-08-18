// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.EnterpriseArchive.BackgroundCommandPresenter
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Mvp;
using Intermech.Mvp.Components.Dialogs;
using System;
using System.Threading;

#nullable disable
namespace Intermech.Tools.EnterpriseArchive;

internal abstract class BackgroundCommandPresenter : ExtendedPresenter<IBackgroundCommandView>
{
  protected readonly string commandName;
  protected readonly bool infiniteProgressBar;
  private bool autoCloseOnSuccess;

  protected BackgroundCommandPresenter(string commandName, bool infiniteProgressBar)
  {
    this.commandName = !string.IsNullOrEmpty(commandName) ? commandName : throw new ArgumentException();
    this.infiniteProgressBar = infiniteProgressBar;
  }

  public bool AutoCloseOnSuccess
  {
    get => this.autoCloseOnSuccess;
    set
    {
      this.CheckAllowPropertyChange();
      this.autoCloseOnSuccess = value;
    }
  }

  protected override void OnAttachView()
  {
    base.OnAttachView();
    this.View.SetCaption(this.commandName);
    this.View.SetMessage("Инициализация...");
    this.View.EnableProgressBar(this.infiniteProgressBar);
  }

  protected override void OnStartBackgroundTask()
  {
    base.OnStartBackgroundTask();
    ThreadPool.QueueUserWorkItem(new WaitCallback(this.DoCommand), (object) null);
  }

  private void DoCommand(object state)
  {
    try
    {
      this.PrepareCommand();
      this.DoCommand();
      this.DisplayComplete();
    }
    catch (Exception ex)
    {
      if (this.IsCancelException(ex))
        this.DisplayCancel(ex);
      else
        this.DisplayAbort(ex);
    }
    finally
    {
      this.ResetCommand();
    }
  }

  private void DisplayComplete()
  {
    this.SendToViewThread((Action) (() =>
    {
      this.View.Hide();
      if (this.autoCloseOnSuccess)
        return;
      MvpContext.ViewService.ShowModal((IPresenter) new SimpleMessagePresenter("Команда успешно завершена.", this.commandName, MessageIcon.Information));
    }));
  }

  private void DisplayCancel(Exception x)
  {
    this.SendToViewThread((Action) (() =>
    {
      this.View.Hide();
      string cancelMessage = this.GetCancelMessage(x);
      if (string.IsNullOrEmpty(cancelMessage))
        return;
      MvpContext.ViewService.ShowModal((IPresenter) new SimpleMessagePresenter(cancelMessage, this.commandName, MessageIcon.Information));
    }));
  }

  private void DisplayAbort(Exception x)
  {
    this.SendToViewThread((Action) (() =>
    {
      this.View.Hide();
      MvpContext.ViewService.ShowModal((IPresenter) new SimpleMessagePresenter($"Команда прервана из-за ошибки. {x.Message}.", this.commandName, MessageIcon.Error));
    }));
  }

  protected virtual void PrepareCommand()
  {
  }

  protected virtual void ResetCommand()
  {
  }

  protected virtual void DoCommand()
  {
  }

  protected virtual bool IsCancelException(Exception x) => false;

  protected virtual string GetCancelMessage(Exception x) => x.Message;

  protected void DisplayMessage(string message)
  {
    this.PostToViewThread((Action) (() => this.View.SetMessage(message)));
  }

  protected void DisplayProgress(double percentValue)
  {
    this.PostToViewThread((Action) (() => this.View.SetProgress(percentValue)));
  }

  protected void ShowChildModalView(IPresenter presenter)
  {
    this.SendToViewThread((Action) (() => MvpContext.ViewService.ShowModal(presenter)));
  }
}
