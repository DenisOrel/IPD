
// Type: Intermech.Client.Core.TimeTable.OneTimeControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.Client.Core.TimeTable;

public class OneTimeControl : UserControl, ITimesControl
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Label label1;
  private DateTimePicker dtStartDate;

  public OneTimeControl() => this.InitializeComponent();

  public void Load(ProcessTime time)
  {
    DateTime beginDateTime = time.BeginDateTime;
    this.dtStartDate.Value = time.BeginDateTime;
  }

  public bool Save(ProcessTime time)
  {
    time.BeginDateTime = this.dtStartDate.Value;
    time.DayOfMonth = 0;
    time.DaysOfWeek = (int[]) null;
    time.Months = (int[]) null;
    time.DayExecution = EveryDayExecution.None;
    return true;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (OneTimeControl));
    this.label1 = new Label();
    this.dtStartDate = new DateTimePicker();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this.dtStartDate, "dtStartDate");
    this.dtStartDate.Name = "dtStartDate";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.dtStartDate);
    this.Controls.Add((Control) this.label1);
    this.Name = nameof (OneTimeControl);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
