
// Type: Intermech.Client.Core.TimeTable.EveryDayControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Calendars;
using Intermech.Interfaces.Client;
using System;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.Client.Core.TimeTable;

public class EveryDayControl : UserControl, ITimesControl
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private DateTimePicker dtStartDate;
  private Label label2;
  private Label label1;
  private RadioButton rbEveryDay;
  private RadioButton rbWorkdays;
  private RadioButton rbHoliday;

  public EveryDayControl()
  {
    this.InitializeComponent();
    ServicesManager.GetService(typeof (ICalendarsService));
  }

  public void Load(ProcessTime time)
  {
    DateTime beginDateTime = time.BeginDateTime;
    this.dtStartDate.Value = time.BeginDateTime;
    if (time.DayExecution == EveryDayExecution.OnWorkdays)
      this.rbWorkdays.Checked = true;
    else if (time.DayExecution == EveryDayExecution.OnHolidays)
      this.rbHoliday.Checked = true;
    else
      this.rbEveryDay.Checked = true;
  }

  public bool Save(ProcessTime time)
  {
    time.BeginDateTime = this.dtStartDate.Value;
    if (this.rbEveryDay.Checked)
      time.DayExecution = EveryDayExecution.EveryDay;
    else if (this.rbWorkdays.Checked)
      time.DayExecution = EveryDayExecution.OnWorkdays;
    else if (this.rbHoliday.Checked)
      time.DayExecution = EveryDayExecution.OnHolidays;
    time.DayOfMonth = 0;
    time.DaysOfWeek = (int[]) null;
    time.Months = (int[]) null;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (EveryDayControl));
    this.dtStartDate = new DateTimePicker();
    this.label2 = new Label();
    this.label1 = new Label();
    this.rbEveryDay = new RadioButton();
    this.rbWorkdays = new RadioButton();
    this.rbHoliday = new RadioButton();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.dtStartDate, "dtStartDate");
    this.dtStartDate.Name = "dtStartDate";
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this.rbEveryDay, "rbEveryDay");
    this.rbEveryDay.Checked = true;
    this.rbEveryDay.Name = "rbEveryDay";
    this.rbEveryDay.TabStop = true;
    this.rbEveryDay.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.rbWorkdays, "rbWorkdays");
    this.rbWorkdays.Name = "rbWorkdays";
    this.rbWorkdays.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.rbHoliday, "rbHoliday");
    this.rbHoliday.Name = "rbHoliday";
    this.rbHoliday.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.rbHoliday);
    this.Controls.Add((Control) this.rbWorkdays);
    this.Controls.Add((Control) this.rbEveryDay);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.dtStartDate);
    this.Controls.Add((Control) this.label2);
    this.Name = nameof (EveryDayControl);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
