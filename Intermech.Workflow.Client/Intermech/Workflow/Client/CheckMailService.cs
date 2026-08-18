// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.CheckMailService
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.Docking;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Workflow;
using Intermech.Navigator.Controls;
using Intermech.Workflow.Design;
using System;
using System.Threading;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Client;

public class CheckMailService : ICheckMailService
{
  private IInvokeService _invokeService;
  private readonly NewMailForm _form;

  public CheckMailService(IInvokeService invokeService)
  {
    this._form = new NewMailForm();
    this._invokeService = invokeService;
  }

  public void StartListener()
  {
    try
    {
      this._invokeService.InvokeAction(-1, (Action) (() =>
      {
        if (this._form == null)
          return;
        this._form.CountMail();
        this._form.StartMonitor();
      }));
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  public void CountUnreadMail(bool showForm)
  {
    try
    {
      this._invokeService.InvokeAction(-1, (Action) (() =>
      {
        if (this._form == null)
          return;
        this._form.CountMail(showForm);
      }));
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  public void BeginUpdate()
  {
    try
    {
      this._invokeService.InvokeAction(-1, (Action) (() =>
      {
        if (this._form == null)
          return;
        this._form.InUpdate = true;
      }));
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  public void EndUpdate(int count)
  {
    try
    {
      this._invokeService.InvokeAction(-1, (Action) (() =>
      {
        if (this._form == null)
          return;
        try
        {
          long num = this._form.LastMailCount[ProcessPriority.Unreal] + (long) count;
          this._form.LastMailCount[ProcessPriority.Unreal] = num;
          MailNode.InboxDescriptor.UnreadCount = num;
        }
        finally
        {
          this._form.InUpdate = false;
          if (this._form.CheckingSkipped)
            this._form.CountMail();
        }
      }));
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  public FormWindowState PreviousMainFormState
  {
    get
    {
      try
      {
        return this._invokeService.InvokeFunc<FormWindowState>(-1, (Func<FormWindowState>) (() => this._form != null ? this._form.PreviousMainFormState : FormWindowState.Normal));
      }
      catch (Exception ex)
      {
        ExceptionHelper.ExceptionService.ShowException(ex);
        return FormWindowState.Normal;
      }
    }
    set
    {
      try
      {
        this._invokeService.InvokeAction(-1, (Action) (() =>
        {
          if (this._form == null)
            return;
          this._form.PreviousMainFormState = value;
        }));
      }
      catch (Exception ex)
      {
        ExceptionHelper.ExceptionService.ShowException(ex);
      }
    }
  }

  public void GoToMail()
  {
    try
    {
      this._invokeService.InvokeAction(-1, (Action) (() =>
      {
        if (this._form != null)
        {
          this._form.GoToMail();
        }
        else
        {
          Form mainForm = ApplicationServices.Container.GetService(typeof (IMainFormUpdate)) is IMainFormUpdate service3 ? service3.MainForm : (Form) null;
          if (mainForm != null)
          {
            mainForm.Activate();
            mainForm.BringToFront();
          }
          if (mainForm != null && mainForm.WindowState == FormWindowState.Minimized)
            mainForm.WindowState = FormWindowState.Normal;
          bool flag = false;
          if (Holder.LastMailTree == null)
          {
            if (ApplicationServices.Container.GetService(typeof (IWellKnownWindowsOpenService)) is IWellKnownWindowsOpenService service4)
            {
              service4.OpenWellKnownWindow(wfClientPlugin.MailWindowName);
              for (int index = 1; index < 5 && Holder.LastMailTree == null; ++index)
              {
                Thread.Sleep(300);
                Application.DoEvents();
              }
            }
          }
          else
          {
            Control control = (Control) Holder.LastMailTree;
            while (control.Parent != null && !(control is DockControl))
              control = control.Parent;
            if (control is DockControl dockControl2)
              dockControl2.Activate();
            flag = control is WellKnownNavWindow wellKnownNavWindow2 && wellKnownNavWindow2.WellKnownName == "mainNavigator";
          }
          if (Holder.LastMailTree == null)
            return;
          Holder.LastMailTree.Browse((flag ? LocalizationHolder.rm.GetString("Workflow.Client_26") : LocalizationHolder.rm.GetString("Workflow.Client_27")) + "*");
        }
      }));
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  public void ShowDebug()
  {
    try
    {
      this._invokeService.InvokeAction(-1, (Action) (() =>
      {
        if (this._form == null)
          return;
        this._form.ShowDebug();
      }));
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }
}
