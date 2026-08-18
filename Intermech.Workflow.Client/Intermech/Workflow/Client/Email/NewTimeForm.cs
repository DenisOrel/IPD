// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.Email.NewTimeForm
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraEditors.Controls;
using Intermech.Client.Core.TimeTable;
using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Client.Email;

public class NewTimeForm : Form
{
  private ProcessTime _time;
  private ITimesControl _control;
  private IContainer components;
  private TimeEdit teBeginTime;
  private Panel panel1;
  private Panel panel2;
  private System.Windows.Forms.ComboBox cbPeriod;
  private Button bOK;
  private Button bCancel;
  private Panel panel3;
  private Label label1;
  private Label label2;
  private GroupBox gbParams;
  private Panel panel4;

  public NewTimeForm()
  {
    this.InitializeComponent();
    this.cbPeriod.SelectedIndexChanged -= new EventHandler(this.cbPeriod_SelectedIndexChanged);
    try
    {
      foreach (TimePeriod timePeriod in Enum.GetValues(typeof (TimePeriod)))
        this.cbPeriod.Items.Add((object) EnumDescConverter.GetEnumDescription((Enum) timePeriod));
    }
    finally
    {
      this.cbPeriod.SelectedIndexChanged += new EventHandler(this.cbPeriod_SelectedIndexChanged);
    }
  }

  public ProcessTime Time
  {
    get => this._time;
    set
    {
      this._time = value;
      this.cbPeriod.SelectedIndex = (int) this._time.Period;
    }
  }

  private void cbPeriod_SelectedIndexChanged(object sender, EventArgs e)
  {
    this._time.Period = (TimePeriod) this.cbPeriod.SelectedIndex;
    this.teBeginTime.EditValue = (object) this._time.BeginDateTime;
    switch (this._time.Period)
    {
      case TimePeriod.OneTime:
        this.gbParams.Text = LocalizationHolder.rm.GetString("Workflow.Client_57");
        this._control = (ITimesControl) new OneTimeControl();
        break;
      case TimePeriod.EveryDay:
        this.gbParams.Text = LocalizationHolder.rm.GetString("Workflow.Client_58");
        this._control = (ITimesControl) new EveryDayControl();
        break;
      case TimePeriod.EveryWeek:
        this.gbParams.Text = LocalizationHolder.rm.GetString("Workflow.Client_59");
        this._control = (ITimesControl) new EveryWeekControl();
        break;
      case TimePeriod.EveryMonth:
        this.gbParams.Text = LocalizationHolder.rm.GetString("Workflow.Client_60");
        this._control = (ITimesControl) new EveryMonthControl();
        break;
    }
    this._control.Load(this._time);
    this.panel4.Controls.Clear();
    this.panel4.Controls.Add(this._control as Control);
  }

  private void bOK_Click(object sender, EventArgs e)
  {
    this._time.Period = (TimePeriod) this.cbPeriod.SelectedIndex;
    if (!this._control.Save(this._time))
      return;
    ProcessTime time = this._time;
    int year = this._time.BeginDateTime.Year;
    int month = this._time.BeginDateTime.Month;
    int day = this._time.BeginDateTime.Day;
    DateTime editValue = (DateTime) this.teBeginTime.EditValue;
    int hour = editValue.Hour;
    editValue = (DateTime) this.teBeginTime.EditValue;
    int minute = editValue.Minute;
    DateTime dateTime = new DateTime(year, month, day, hour, minute, 0);
    time.BeginDateTime = dateTime;
    this.DialogResult = DialogResult.OK;
    this.Close();
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.teBeginTime = new TimeEdit();
    this.panel1 = new Panel();
    this.bOK = new Button();
    this.bCancel = new Button();
    this.panel2 = new Panel();
    this.gbParams = new GroupBox();
    this.panel3 = new Panel();
    this.label1 = new Label();
    this.cbPeriod = new System.Windows.Forms.ComboBox();
    this.label2 = new Label();
    this.panel4 = new Panel();
    this.teBeginTime.Properties.BeginInit();
    this.panel1.SuspendLayout();
    this.panel2.SuspendLayout();
    this.gbParams.SuspendLayout();
    this.panel3.SuspendLayout();
    this.SuspendLayout();
    this.teBeginTime.EditValue = (object) new DateTime(2009, 11, 6, 0, 0, 0, 0);
    this.teBeginTime.Location = new Point(229, 26);
    this.teBeginTime.Name = "teBeginTime";
    this.teBeginTime.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.teBeginTime.Properties.TimeFormat = TimeFormat.HourMin;
    this.teBeginTime.Properties.UseCtrlIncrement = false;
    this.teBeginTime.Size = new Size(78, 20);
    this.teBeginTime.TabIndex = 1;
    this.panel1.Controls.Add((Control) this.bOK);
    this.panel1.Controls.Add((Control) this.bCancel);
    this.panel1.Dock = DockStyle.Bottom;
    this.panel1.Location = new Point(0, 269);
    this.panel1.Name = "panel1";
    this.panel1.Size = new Size(431, 47);
    this.panel1.TabIndex = 1;
    this.bOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.bOK.Location = new Point(263, 9);
    this.bOK.Name = "bOK";
    this.bOK.Size = new Size(75, 23);
    this.bOK.TabIndex = 5;
    this.bOK.Text = "OK";
    this.bOK.UseVisualStyleBackColor = true;
    this.bOK.Click += new EventHandler(this.bOK_Click);
    this.bCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.bCancel.DialogResult = DialogResult.Cancel;
    this.bCancel.Location = new Point(344, 9);
    this.bCancel.Name = "bCancel";
    this.bCancel.Size = new Size(75, 23);
    this.bCancel.TabIndex = 6;
    this.bCancel.Text = "Отмена";
    this.bCancel.UseVisualStyleBackColor = true;
    this.panel2.Controls.Add((Control) this.gbParams);
    this.panel2.Controls.Add((Control) this.panel3);
    this.panel2.Dock = DockStyle.Fill;
    this.panel2.Location = new Point(0, 0);
    this.panel2.Name = "panel2";
    this.panel2.Size = new Size(431, 269);
    this.panel2.TabIndex = 2;
    this.gbParams.Controls.Add((Control) this.panel4);
    this.gbParams.Dock = DockStyle.Fill;
    this.gbParams.Location = new Point(0, 62);
    this.gbParams.Name = "gbParams";
    this.gbParams.Size = new Size(431, 207);
    this.gbParams.TabIndex = 8;
    this.gbParams.TabStop = false;
    this.gbParams.Text = "groupBox2";
    this.panel3.Controls.Add((Control) this.label1);
    this.panel3.Controls.Add((Control) this.cbPeriod);
    this.panel3.Controls.Add((Control) this.label2);
    this.panel3.Controls.Add((Control) this.teBeginTime);
    this.panel3.Dock = DockStyle.Top;
    this.panel3.Location = new Point(0, 0);
    this.panel3.Name = "panel3";
    this.panel3.Size = new Size(431, 62);
    this.panel3.TabIndex = 5;
    this.label1.AutoSize = true;
    this.label1.Location = new Point(26, 9);
    this.label1.Name = "label1";
    this.label1.Size = new Size(153, 13);
    this.label1.TabIndex = 2;
    this.label1.Text = "Периодичность выполнения:";
    this.cbPeriod.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbPeriod.FormattingEnabled = true;
    this.cbPeriod.Location = new Point(29, 25);
    this.cbPeriod.Name = "cbPeriod";
    this.cbPeriod.Size = new Size(166, 21);
    this.cbPeriod.TabIndex = 0;
    this.cbPeriod.SelectedIndexChanged += new EventHandler(this.cbPeriod_SelectedIndexChanged);
    this.label2.AutoSize = true;
    this.label2.Location = new Point(226, 9);
    this.label2.Name = "label2";
    this.label2.Size = new Size(81, 13);
    this.label2.TabIndex = 3;
    this.label2.Text = "Время начала:";
    this.panel4.Dock = DockStyle.Fill;
    this.panel4.Location = new Point(3, 16 /*0x10*/);
    this.panel4.Name = "panel4";
    this.panel4.Size = new Size(425, 188);
    this.panel4.TabIndex = 0;
    this.AcceptButton = (IButtonControl) this.bOK;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.AutoSize = true;
    this.CancelButton = (IButtonControl) this.bCancel;
    this.ClientSize = new Size(431, 316);
    this.Controls.Add((Control) this.panel2);
    this.Controls.Add((Control) this.panel1);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (NewTimeForm);
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Время выполнения задач";
    this.teBeginTime.Properties.EndInit();
    this.panel1.ResumeLayout(false);
    this.panel2.ResumeLayout(false);
    this.gbParams.ResumeLayout(false);
    this.panel3.ResumeLayout(false);
    this.panel3.PerformLayout();
    this.ResumeLayout(false);
  }
}
