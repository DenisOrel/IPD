// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.BackgroundTask.KeyConverterTask
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Imbase;
using Intermech.Localization;
using System;
using System.Diagnostics;
using System.Timers;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.BackgroundTask;

internal class KeyConverterTask : IBackgroundTask
{
  private IKeyConverter _converter;
  private IUserSession _session;
  private System.Timers.Timer _timer;
  private int _imageIndex = -1;
  private string _name = LocalizationHolder.rm.GetString("Imbase_KeyConverterTask_Name");

  public KeyConverterTask(IKeyConverter converter)
  {
    this._converter = converter;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this._session = sessionKeeper.Session.Clone(nameof (KeyConverterTask));
    this._timer = new System.Timers.Timer(1000.0);
    this._timer.Elapsed += new ElapsedEventHandler(this.Timer_Elapsed);
    if (!(ServicesManager.GetService(typeof (INamedImageList)) is INamedImageList service))
      return;
    this._imageIndex = service.ImageIndex("imgRefresh");
  }

  private void Timer_Elapsed(object sender, ElapsedEventArgs e)
  {
    try
    {
      this.OnChanged(BackgroundTaskChangedType.State);
      this.OnChanged(BackgroundTaskChangedType.Value);
      if (this.State != BackgroundTaskState.Terminated)
        return;
      this.Terminate();
    }
    catch (Exception ex)
    {
      Trace.WriteLine(ex.Message);
    }
  }

  protected void OnChanged(BackgroundTaskChangedType type)
  {
    BackgroundTaskChangedEventHandler changed = this.Changed;
    if (changed == null)
      return;
    changed((object) this, type);
  }

  public event BackgroundTaskChangedEventHandler Changed;

  public int ImageIndex => this._imageIndex;

  public string Name => this._name;

  public int MaximumValue
  {
    get => 100;
    set
    {
    }
  }

  public int MinimumValue
  {
    get => 0;
    set
    {
    }
  }

  public object Value
  {
    get
    {
      if (this._converter.IsFirstTaskComplete)
      {
        this._name = LocalizationHolder.rm.GetString("Imbase_KeyConverterTask_MaterialPropertiesObject_Rename");
        this.OnChanged(BackgroundTaskChangedType.Text);
      }
      return (object) this._converter.Value;
    }
    set
    {
    }
  }

  public object Result
  {
    get => (object) 1;
    set
    {
    }
  }

  public BackgroundTaskState State
  {
    get
    {
      switch (this._converter.State)
      {
        case -2:
          return BackgroundTaskState.Terminated;
        case -1:
          return BackgroundTaskState.Paused;
        case 0:
          return BackgroundTaskState.Stopped;
        default:
          return BackgroundTaskState.Running;
      }
    }
    set
    {
    }
  }

  public BackgroundTaskShowMode ShowMode => BackgroundTaskShowMode.Progress;

  public bool Active => this._converter.State > 0;

  public void SetMaxMin(int max, int min)
  {
  }

  public bool CanStop()
  {
    return this.State == BackgroundTaskState.Running || this.State == BackgroundTaskState.Paused;
  }

  public bool CanPause() => this.State == BackgroundTaskState.Running;

  public bool CanResume() => true;

  public bool CanTerminate() => true;

  public void Stop()
  {
    this._timer.Enabled = false;
    this._converter.Stop();
    this.OnChanged(BackgroundTaskChangedType.Dispose);
  }

  public void Pause()
  {
    this._timer.Enabled = false;
    this._converter.Pause();
    this.OnChanged(BackgroundTaskChangedType.State);
  }

  public void Resume()
  {
    this._timer.Enabled = true;
    this._converter.Start(this._session.SessionGUID);
    this.OnChanged(BackgroundTaskChangedType.State);
  }

  public void Terminate()
  {
    this._timer.Enabled = false;
    this._converter.Stop();
    this.OnChanged(BackgroundTaskChangedType.Dispose);
    if (this._converter.ConvertedInfo.Count == 0 || !(ServicesManager.GetService(typeof (IInvokeService)) is IInvokeService service1))
      return;
    IOutputView service2 = ServicesManager.GetService(typeof (IOutputView)) as IOutputView;
    string strMsg = LocalizationHolder.rm.GetString("ConvertKeys_EndDlg_Msg");
    string caption = LocalizationHolder.rm.GetString("Imbase_ConvertKeys_Caption");
    if (service2 == null)
    {
      int num;
      service1.InvokeAction(-1, (Action) (() => num = (int) MessageBox.Show(strMsg, caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk)));
    }
    else
    {
      service2.ClearText(caption);
      string str1 = LocalizationHolder.rm.GetString("Imbase_Object_ID");
      string str2 = LocalizationHolder.rm.GetString("Imbase_Error_Text");
      foreach (ObjectInfoForExteption infoForExteption in this._converter.ConvertedInfo)
      {
        service2.WriteString(caption, $"{str1} :  {infoForExteption.ID}.   {str2} :  {infoForExteption.Message}");
        service2.WriteString(caption, string.Empty);
      }
      if (service1.InvokeFunc<DialogResult>(-1, (Func<DialogResult>) (() => MessageBox.Show(strMsg, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question))) == DialogResult.No)
        return;
      service2.Activate(caption);
      service2.ShowView();
    }
  }
}
