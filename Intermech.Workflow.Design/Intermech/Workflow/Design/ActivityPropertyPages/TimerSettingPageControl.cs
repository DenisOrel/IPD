// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.ActivityPropertyPages.TimerSettingPageControl
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Interfaces;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design.ActivityPropertyPages;

public class TimerSettingPageControl : UserControl
{
  private bool _readOnly;
  private ActivitySettings _settings;
  private TimePeriodForm _tpf;
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  public TimerSettingPageControl() => this.InitializeComponent();

  public bool ReadOnly
  {
    get => this._readOnly;
    set
    {
      this._readOnly = value;
      if (!this._readOnly)
        return;
      ControlFuncs.SetControlsReadOnly((Control) this, value);
    }
  }

  public bool LoadTimerSettingPageControl(
    ActivitySettings settings,
    IDBObject activityObject,
    IUserSession activitySession)
  {
    this._settings = settings;
    bool flag = false;
    if (settings.ActivityType == wfConsts.TimerTypeID)
    {
      settings.PeriodInformation = new PeriodInformation(activitySession);
      string str = settings.ExtProperties.Read("TimerPeriod");
      if (str == "" && settings.ExtProperties.Ini.Root.Name == "Period")
        str = settings.ExtProperties.Ini.AsString;
      settings.PeriodInformation.AsString = str;
      this._tpf = new TimePeriodForm(settings.ObjectIDwithVars);
      this._tpf.Embedded = true;
      this._tpf.Parent = (Control) this;
      this._tpf.Visible = true;
      this._tpf.Dock = DockStyle.Top;
      this._tpf.SetPeriodInformation(settings.PeriodInformation);
    }
    else
      flag = true;
    return flag;
  }

  public bool Save(IDBObject activityToSave, bool modified)
  {
    PeriodInformation periodInformation = this._settings.PeriodInformation;
    if (periodInformation != null)
    {
      this._tpf.FillPeriodInformation(ref periodInformation, activityToSave.Session);
      if (periodInformation.Modified)
      {
        this._settings.ExtProperties.Write("TimerPeriod", periodInformation.AsString, ExtPropertiesFlag.Timer);
        modified = true;
      }
    }
    return modified;
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.SuspendLayout();
    this.AutoScaleDimensions = new SizeF(120f, 120f);
    this.AutoScaleMode = AutoScaleMode.Dpi;
    this.BackColor = SystemColors.ControlLightLight;
    this.Name = nameof (TimerSettingPageControl);
    this.Size = new Size(580, 326);
    this.ResumeLayout(false);
  }
}
