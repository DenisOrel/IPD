// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.ClearOldProcessPropertyPage
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Workflow.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Client;

public class ClearOldProcessPropertyPage : 
  UserControl,
  IPropertyPage,
  IPropertyPageSearchOptionEvents
{
  private ClearOldProcessSettings _settings = new ClearOldProcessSettings();
  private List<ClearOldProcessPropertyPage.TimeSpanTypeValue> _timeSpanTypeValues = new List<ClearOldProcessPropertyPage.TimeSpanTypeValue>()
  {
    new ClearOldProcessPropertyPage.TimeSpanTypeValue()
    {
      ID = (short) 0,
      DisplayName = "Дней"
    },
    new ClearOldProcessPropertyPage.TimeSpanTypeValue()
    {
      ID = (short) 1,
      DisplayName = "Недель"
    },
    new ClearOldProcessPropertyPage.TimeSpanTypeValue()
    {
      ID = (short) 2,
      DisplayName = "Месяцев"
    },
    new ClearOldProcessPropertyPage.TimeSpanTypeValue()
    {
      ID = (short) 3,
      DisplayName = "Лет"
    }
  };
  private bool _modified;
  private bool _isLoad;
  private IContainer components;
  private CheckBox enableClearOldProcessCheckBox;
  private GroupBox groupBox1;
  private NumericUpDown timeSpanValueNumeric;
  private Label label3;
  private ComboBox timeTypeComboBox;
  private Label label1;
  private Label label2;
  private CheckBox terminatedCheck;
  private CheckBox completedCheck;

  public ClearOldProcessPropertyPage() => this.InitializeComponent();

  public bool Modified
  {
    get => this._modified;
    set
    {
      if (this._modified == value)
        return;
      this._modified = value;
      EventHandler changed = this.Changed;
      if (this._isLoad || !this._modified || changed == null)
        return;
      changed((object) this, (EventArgs) null);
    }
  }

  public event EventHandler Changed;

  public PropertyPageType Type => PropertyPageType.Control;

  public object Control => (object) this;

  public string PageName => "Очистка устаревших процессов";

  public void Apply()
  {
    this._settings.EnableClearOldProcess = this.enableClearOldProcessCheckBox.Checked;
    this._settings.TimeTypeComboBoxSelectedIndex = this.timeTypeComboBox.SelectedIndex;
    this._settings.ClearOldProcessStartTimeValue = (int) this.timeSpanValueNumeric.Value;
    this._settings.ComletedTypeClear = !this.completedCheck.Checked || !this.terminatedCheck.Checked ? (!this.completedCheck.Checked ? (!this.terminatedCheck.Checked ? (short) 3 : (short) 2) : (short) 1) : (short) 0;
    ClearOldProcessSettings.Cfg.Assign(this._settings);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      ClearOldProcessSettings.Cfg.Save(sessionKeeper.Session);
  }

  public void Cancel() => this._settings.Assign(ClearOldProcessSettings.Cfg);

  public string HelpTopicID => string.Empty;

  public string HeaderText => this.PageName;

  public List<string> GetOptionNames()
  {
    return !(this.Control is System.Windows.Forms.Control control) ? new List<string>() : IPropertyPageHelper.GetOptionNames(control);
  }

  protected override void OnLoad(EventArgs e)
  {
    this._isLoad = true;
    base.OnLoad(e);
    this.timeSpanValueNumeric.Maximum = 2147483646M;
    this._settings.Assign(ClearOldProcessSettings.Cfg);
    this.enableClearOldProcessCheckBox.Checked = this._settings.EnableClearOldProcess;
    this.completedCheck.Checked = this._settings.ComletedTypeClear == (short) 0 || this._settings.ComletedTypeClear == (short) 1;
    this.terminatedCheck.Checked = this._settings.ComletedTypeClear == (short) 0 || this._settings.ComletedTypeClear == (short) 2;
    this.timeTypeComboBox.DisplayMember = "DisplayName";
    this.timeTypeComboBox.ValueMember = "ID";
    this.timeTypeComboBox.DataSource = (object) this._timeSpanTypeValues;
    this.timeTypeComboBox.SelectedIndex = 0;
    this.timeTypeComboBox.SelectedIndex = this._settings.TimeTypeComboBoxSelectedIndex;
    this.timeSpanValueNumeric.Value = (Decimal) (this._settings.ClearOldProcessStartTimeValue == 0 ? 1 : this._settings.ClearOldProcessStartTimeValue);
    this.Modified = false;
    this._isLoad = false;
  }

  private void enableClearOldProcessCheckBox_CheckedChanged(object sender, EventArgs e)
  {
    this.groupBox1.Visible = this.enableClearOldProcessCheckBox.Checked;
    this.Modified = true;
  }

  private void processTypesCheckedChanged(object sender, EventArgs e) => this.Modified = true;

  private void timeSpanValueNumeric_ValueChanged(object sender, EventArgs e)
  {
    this.Modified = true;
  }

  private void timeTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
  {
    this.Modified = true;
  }

  private void timeSpanValueNumeric_KeyPress(object sender, KeyPressEventArgs e)
  {
    this.Modified = true;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.enableClearOldProcessCheckBox = new CheckBox();
    this.groupBox1 = new GroupBox();
    this.terminatedCheck = new CheckBox();
    this.completedCheck = new CheckBox();
    this.label2 = new Label();
    this.label1 = new Label();
    this.timeTypeComboBox = new ComboBox();
    this.label3 = new Label();
    this.timeSpanValueNumeric = new NumericUpDown();
    this.groupBox1.SuspendLayout();
    this.timeSpanValueNumeric.BeginInit();
    this.SuspendLayout();
    this.enableClearOldProcessCheckBox.AutoSize = true;
    this.enableClearOldProcessCheckBox.Dock = DockStyle.Top;
    this.enableClearOldProcessCheckBox.Location = new Point(0, 0);
    this.enableClearOldProcessCheckBox.Name = "enableClearOldProcessCheckBox";
    this.enableClearOldProcessCheckBox.Padding = new Padding(0, 0, 0, 3);
    this.enableClearOldProcessCheckBox.Size = new Size(415, 20);
    this.enableClearOldProcessCheckBox.TabIndex = 1;
    this.enableClearOldProcessCheckBox.Text = "Включить чистку устаревших процессов";
    this.enableClearOldProcessCheckBox.UseVisualStyleBackColor = true;
    this.enableClearOldProcessCheckBox.CheckedChanged += new EventHandler(this.enableClearOldProcessCheckBox_CheckedChanged);
    this.groupBox1.Controls.Add((System.Windows.Forms.Control) this.terminatedCheck);
    this.groupBox1.Controls.Add((System.Windows.Forms.Control) this.completedCheck);
    this.groupBox1.Controls.Add((System.Windows.Forms.Control) this.label2);
    this.groupBox1.Controls.Add((System.Windows.Forms.Control) this.label1);
    this.groupBox1.Controls.Add((System.Windows.Forms.Control) this.timeTypeComboBox);
    this.groupBox1.Controls.Add((System.Windows.Forms.Control) this.label3);
    this.groupBox1.Controls.Add((System.Windows.Forms.Control) this.timeSpanValueNumeric);
    this.groupBox1.Dock = DockStyle.Fill;
    this.groupBox1.Location = new Point(0, 20);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(415, 151);
    this.groupBox1.TabIndex = 2;
    this.groupBox1.TabStop = false;
    this.groupBox1.Text = "Настройка времени устаревания процессов";
    this.groupBox1.Visible = false;
    this.terminatedCheck.AutoSize = true;
    this.terminatedCheck.Location = new Point(295, 90);
    this.terminatedCheck.Name = "terminatedCheck";
    this.terminatedCheck.Size = new Size(90, 17);
    this.terminatedCheck.TabIndex = 10;
    this.terminatedCheck.Text = "Прерванные";
    this.terminatedCheck.UseVisualStyleBackColor = true;
    this.terminatedCheck.CheckedChanged += new EventHandler(this.processTypesCheckedChanged);
    this.completedCheck.AutoSize = true;
    this.completedCheck.Location = new Point(192 /*0xC0*/, 90);
    this.completedCheck.Name = "completedCheck";
    this.completedCheck.Size = new Size(97, 17);
    this.completedCheck.TabIndex = 9;
    this.completedCheck.Text = "Выполненные";
    this.completedCheck.UseVisualStyleBackColor = true;
    this.completedCheck.CheckedChanged += new EventHandler(this.processTypesCheckedChanged);
    this.label2.AutoSize = true;
    this.label2.Location = new Point(3, 91);
    this.label2.Name = "label2";
    this.label2.Size = new Size(183, 13);
    this.label2.TabIndex = 8;
    this.label2.Text = "Какие процессы следует удалять: ";
    this.label1.AutoSize = true;
    this.label1.Location = new Point(6, 60);
    this.label1.Name = "label1";
    this.label1.Size = new Size(39, 13);
    this.label1.TabIndex = 6;
    this.label1.Text = "После";
    this.timeTypeComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.timeTypeComboBox.FormattingEnabled = true;
    this.timeTypeComboBox.Location = new Point(271, 57);
    this.timeTypeComboBox.Name = "timeTypeComboBox";
    this.timeTypeComboBox.Size = new Size(135, 21);
    this.timeTypeComboBox.TabIndex = 5;
    this.timeTypeComboBox.SelectedIndexChanged += new EventHandler(this.timeTypeComboBox_SelectedIndexChanged);
    this.label3.AutoSize = true;
    this.label3.Location = new Point(6, 32 /*0x20*/);
    this.label3.Name = "label3";
    this.label3.Size = new Size(387, 13);
    this.label3.TabIndex = 4;
    this.label3.Text = "Задайте временной промежуток после которого процессы будут удалены:";
    this.timeSpanValueNumeric.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.timeSpanValueNumeric.Location = new Point(51, 58);
    this.timeSpanValueNumeric.Maximum = new Decimal(new int[4]
    {
      10000,
      0,
      0,
      0
    });
    this.timeSpanValueNumeric.Minimum = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this.timeSpanValueNumeric.Name = "timeSpanValueNumeric";
    this.timeSpanValueNumeric.Size = new Size(214, 20);
    this.timeSpanValueNumeric.TabIndex = 2;
    this.timeSpanValueNumeric.Value = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this.timeSpanValueNumeric.ValueChanged += new EventHandler(this.timeSpanValueNumeric_ValueChanged);
    this.timeSpanValueNumeric.KeyPress += new KeyPressEventHandler(this.timeSpanValueNumeric_KeyPress);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((System.Windows.Forms.Control) this.groupBox1);
    this.Controls.Add((System.Windows.Forms.Control) this.enableClearOldProcessCheckBox);
    this.MinimumSize = new Size(415, 170);
    this.Name = nameof (ClearOldProcessPropertyPage);
    this.Size = new Size(415, 171);
    this.groupBox1.ResumeLayout(false);
    this.groupBox1.PerformLayout();
    this.timeSpanValueNumeric.EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  private class TimeSpanTypeValue
  {
    public short ID { get; set; }

    public string DisplayName { get; set; }

    public override string ToString() => this.DisplayName;
  }

  private enum TimeSpanDayValues : short
  {
    Day,
    Week,
    Month,
    Year,
  }
}
