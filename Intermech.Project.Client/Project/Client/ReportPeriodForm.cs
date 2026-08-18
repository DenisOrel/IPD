// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Client.ReportPeriodForm
// Assembly: Intermech.Project.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D968BDD9-29F0-4E24-8F57-6E851EE47258
// Assembly location: D:\IPS\Client\Intermech.Project.Client.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

#nullable disable
namespace Intermech.Project.Client;

public class ReportPeriodForm : Form
{
  private IContainer components;
  private Label _label1;
  private Panel _buttonsPanel;
  private Button _cancButton;
  private Button _okButton;
  private GroupBox _groupBox;
  private RadioButton _lastMonthRB;
  private RadioButton _currentMonthRB;
  private RadioButton _lastDaysRB;
  private NumericUpDown _daysEdit;
  private DateTimePicker _date2Picker;
  private Label _label3;
  private DateTimePicker _date1Picker;
  private RadioButton _rangeRB;
  private Label _label2;

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  internal Label Label1
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._label1.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  internal Panel ButtonsPanel
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._buttonsPanel.CheckInitializedIn<Panel>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  internal Button CancButton
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._cancButton.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  internal Button OkButton
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._okButton.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  internal GroupBox GroupBox
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._groupBox.CheckInitializedIn<GroupBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  internal RadioButton LastMonthRB
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._lastMonthRB.CheckInitializedIn<RadioButton>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  internal RadioButton CurrentMonthRB
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._currentMonthRB.CheckInitializedIn<RadioButton>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  internal RadioButton LastDaysRB
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._lastDaysRB.CheckInitializedIn<RadioButton>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  internal NumericUpDown DaysEdit
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._daysEdit.CheckInitializedIn<NumericUpDown>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  internal DateTimePicker Date2Picker
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._date2Picker.CheckInitializedIn<DateTimePicker>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  internal Label Label3
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._label3.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  internal DateTimePicker Date1Picker
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._date1Picker.CheckInitializedIn<DateTimePicker>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  internal RadioButton RangeRB
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._rangeRB.CheckInitializedIn<RadioButton>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  internal Label Label2
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._label2.CheckInitializedIn<Label>((object) this);
    }
  }

  public ReportPeriodForm() => this.InitializeComponent();

  private void RecalcDates()
  {
    if (this.CurrentMonthRB.Checked)
    {
      int year = DateTime.Now.Year;
      int month = DateTime.Now.Month;
      this.Date1Picker.Value = new DateTime(year, month, 1);
      this.Date2Picker.Value = new DateTime(year, month, DateTime.DaysInMonth(year, month));
    }
    if (this.LastMonthRB.Checked)
    {
      DateTime dateTime = DateTime.Now.AddMonths(-1);
      int year = dateTime.Year;
      int month = dateTime.Month;
      this.Date1Picker.Value = new DateTime(year, month, 1);
      this.Date2Picker.Value = new DateTime(year, month, DateTime.DaysInMonth(year, month));
    }
    if (!this.LastDaysRB.Checked)
      return;
    DateTime dateTime1 = DateTime.Now;
    DateTime dateTime2 = dateTime1.AddDays((double) -this.DaysEdit.Value);
    if (dateTime1 > dateTime2)
    {
      DateTime dateTime3 = dateTime1;
      dateTime1 = dateTime2;
      dateTime2 = dateTime3;
    }
    this.Date1Picker.Value = dateTime1;
    this.Date2Picker.Value = dateTime2;
  }

  private void CurrentMonthRB_CheckedChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.DaysEdit.Enabled = this.LastDaysRB.Checked;
    this.Date1Picker.Enabled = this.RangeRB.Checked;
    this.Date2Picker.Enabled = this.RangeRB.Checked;
    this.RecalcDates();
  }

  public DateTime Start => this.Date1Picker.Value.Date;

  public DateTime Finish => this.Date2Picker.Value.Date.AddDays(1.0).Date.AddSeconds(-1.0);

  private void DaysEdit_ValueChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.RecalcDates();
  }

  [CanBeNull]
  private RadioButton SelectedRadioButton
  {
    get
    {
      return this.GroupBox.Controls.OfType<RadioButton>().FirstOrDefault<RadioButton>((Func<RadioButton, bool>) (radioButton => radioButton.Checked));
    }
  }

  private void ReportPeriodForm_Load([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    if (this.DesignMode)
      return;
    Dictionary<string, object> dictionary = new Dictionary<string, object>();
    dictionary.Add("Selected", (object) 0);
    dictionary.Add("LastDays", (object) 7);
    dictionary.Add("FromDate", (object) DateTime.Now.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    dictionary.Add("ToDate", (object) DateTime.Now.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    Intermech.Client.Core.FormStorage.LoadLayout((Control) this, (IDictionary) dictionary);
    this.Date1Picker.Value = Convert.ToDateTime(dictionary["FromDate"].ToString(), (IFormatProvider) CultureInfo.InvariantCulture);
    this.Date2Picker.Value = Convert.ToDateTime(dictionary["ToDate"].ToString(), (IFormatProvider) CultureInfo.InvariantCulture);
    this.DaysEdit.Value = (Decimal) Convert.ToInt32(dictionary["LastDays"]);
    string str = dictionary["Selected"].ToString();
    foreach (object control in (ArrangedElementCollection) this.GroupBox.Controls)
    {
      if (control is RadioButton radioButton && str.Equals((string) radioButton.Tag))
      {
        radioButton.Checked = true;
        str = (string) null;
        break;
      }
    }
    if (str == null)
      return;
    this.CurrentMonthRB.Checked = true;
  }

  private void ReportPeriodForm_FormClosing([CanBeNull] object sender, [NotNull] FormClosingEventArgs e)
  {
    Dictionary<string, object> dictionary = new Dictionary<string, object>();
    RadioButton selectedRadioButton = this.SelectedRadioButton;
    if (selectedRadioButton != null)
      dictionary.Add("Selected", (object) (selectedRadioButton.Tag?.ToString() ?? string.Empty));
    dictionary.Add("LastDays", (object) this.DaysEdit.Value);
    dictionary.Add("FromDate", (object) this.Date1Picker.Value.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    dictionary.Add("ToDate", (object) this.Date2Picker.Value.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    Intermech.Client.Core.FormStorage.SaveLayout((Control) this, (IDictionary) dictionary);
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this._label1 = new Label();
    this._buttonsPanel = new Panel();
    this._cancButton = new Button();
    this._okButton = new Button();
    this._groupBox = new GroupBox();
    this._lastDaysRB = new RadioButton();
    this._currentMonthRB = new RadioButton();
    this._lastMonthRB = new RadioButton();
    this._daysEdit = new NumericUpDown();
    this._label2 = new Label();
    this._rangeRB = new RadioButton();
    this._date1Picker = new DateTimePicker();
    this._label3 = new Label();
    this._date2Picker = new DateTimePicker();
    this._buttonsPanel.SuspendLayout();
    this._groupBox.SuspendLayout();
    this._daysEdit.BeginInit();
    this.SuspendLayout();
    this._label1.Dock = DockStyle.Top;
    this._label1.Location = new Point(15, 15);
    this._label1.Name = "_label1";
    this._label1.Size = new Size(392, 41);
    this._label1.TabIndex = 0;
    this._label1.Text = "Для формирования отчета требуется указать период, для которого будет вычислена загрузка выбранных исполнителей";
    this._buttonsPanel.BackColor = Color.Transparent;
    this._buttonsPanel.Controls.Add((Control) this._cancButton);
    this._buttonsPanel.Controls.Add((Control) this._okButton);
    this._buttonsPanel.Dock = DockStyle.Bottom;
    this._buttonsPanel.Location = new Point(15, 208 /*0xD0*/);
    this._buttonsPanel.Name = "_buttonsPanel";
    this._buttonsPanel.Size = new Size(392, 37);
    this._buttonsPanel.TabIndex = 2;
    this._cancButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this._cancButton.DialogResult = DialogResult.Cancel;
    this._cancButton.ImeMode = ImeMode.NoControl;
    this._cancButton.Location = new Point(317, 13);
    this._cancButton.Name = "_cancButton";
    this._cancButton.Size = new Size(75, 23);
    this._cancButton.TabIndex = 101;
    this._cancButton.Text = "Отмена";
    this._okButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this._okButton.DialogResult = DialogResult.OK;
    this._okButton.ImeMode = ImeMode.NoControl;
    this._okButton.Location = new Point(236, 13);
    this._okButton.Name = "_okButton";
    this._okButton.Size = new Size(75, 23);
    this._okButton.TabIndex = 1;
    this._okButton.Text = "OK";
    this._groupBox.Controls.Add((Control) this._date2Picker);
    this._groupBox.Controls.Add((Control) this._label3);
    this._groupBox.Controls.Add((Control) this._date1Picker);
    this._groupBox.Controls.Add((Control) this._rangeRB);
    this._groupBox.Controls.Add((Control) this._label2);
    this._groupBox.Controls.Add((Control) this._daysEdit);
    this._groupBox.Controls.Add((Control) this._lastMonthRB);
    this._groupBox.Controls.Add((Control) this._currentMonthRB);
    this._groupBox.Controls.Add((Control) this._lastDaysRB);
    this._groupBox.Dock = DockStyle.Fill;
    this._groupBox.Location = new Point(15, 56);
    this._groupBox.Name = "_groupBox";
    this._groupBox.Size = new Size(392, 152);
    this._groupBox.TabIndex = 1;
    this._groupBox.TabStop = false;
    this._groupBox.Text = "Отобразить в отчете загрузку";
    this._lastDaysRB.AutoSize = true;
    this._lastDaysRB.Location = new Point(15, 85);
    this._lastDaysRB.Name = "_lastDaysRB";
    this._lastDaysRB.Size = new Size(95, 17);
    this._lastDaysRB.TabIndex = 3;
    this._lastDaysRB.Tag = (object) "2";
    this._lastDaysRB.Text = "За последние";
    this._lastDaysRB.UseVisualStyleBackColor = true;
    this._lastDaysRB.CheckedChanged += new EventHandler(this.CurrentMonthRB_CheckedChanged);
    this._currentMonthRB.AutoSize = true;
    this._currentMonthRB.Location = new Point(15, 25);
    this._currentMonthRB.Name = "_currentMonthRB";
    this._currentMonthRB.Size = new Size(119, 17);
    this._currentMonthRB.TabIndex = 1;
    this._currentMonthRB.Tag = (object) "0";
    this._currentMonthRB.Text = "За текущий месяц";
    this._currentMonthRB.UseVisualStyleBackColor = true;
    this._currentMonthRB.CheckedChanged += new EventHandler(this.CurrentMonthRB_CheckedChanged);
    this._lastMonthRB.AutoSize = true;
    this._lastMonthRB.Location = new Point(15, 55);
    this._lastMonthRB.Name = "_lastMonthRB";
    this._lastMonthRB.Size = new Size(122, 17);
    this._lastMonthRB.TabIndex = 2;
    this._lastMonthRB.Tag = (object) "1";
    this._lastMonthRB.Text = "За прошлый месяц";
    this._lastMonthRB.UseVisualStyleBackColor = true;
    this._lastMonthRB.CheckedChanged += new EventHandler(this.CurrentMonthRB_CheckedChanged);
    this._daysEdit.Location = new Point(126, 85);
    this._daysEdit.Margin = new Padding(2);
    this._daysEdit.Maximum = new Decimal(new int[4]
    {
      100000,
      0,
      0,
      0
    });
    this._daysEdit.Minimum = new Decimal(new int[4]
    {
      100000,
      0,
      0,
      int.MinValue
    });
    this._daysEdit.Name = "_daysEdit";
    this._daysEdit.Size = new Size(56, 20);
    this._daysEdit.TabIndex = 5;
    this._daysEdit.Value = new Decimal(new int[4]
    {
      7,
      0,
      0,
      0
    });
    this._daysEdit.ValueChanged += new EventHandler(this.DaysEdit_ValueChanged);
    this._label2.AutoSize = true;
    this._label2.Location = new Point(202, 87);
    this._label2.Name = "_label2";
    this._label2.Size = new Size(31 /*0x1F*/, 13);
    this._label2.TabIndex = 6;
    this._label2.Text = "дней";
    this._rangeRB.AutoSize = true;
    this._rangeRB.Location = new Point(15, 115);
    this._rangeRB.Name = "_rangeRB";
    this._rangeRB.Size = new Size(60, 17);
    this._rangeRB.TabIndex = 7;
    this._rangeRB.Tag = (object) "3";
    this._rangeRB.Text = "С даты";
    this._rangeRB.UseVisualStyleBackColor = true;
    this._rangeRB.CheckedChanged += new EventHandler(this.CurrentMonthRB_CheckedChanged);
    this._date1Picker.CustomFormat = "dd.MM.yy H:mm";
    this._date1Picker.Format = DateTimePickerFormat.Short;
    this._date1Picker.Location = new Point(126, 115);
    this._date1Picker.Name = "_date1Picker";
    this._date1Picker.Size = new Size(100, 20);
    this._date1Picker.TabIndex = 8;
    this._label3.AutoSize = true;
    this._label3.Location = new Point(241, 121);
    this._label3.Name = "_label3";
    this._label3.Size = new Size(19, 13);
    this._label3.TabIndex = 9;
    this._label3.Text = "по";
    this._date2Picker.CustomFormat = "dd.MM.yy H:mm";
    this._date2Picker.Format = DateTimePickerFormat.Short;
    this._date2Picker.Location = new Point(275, 115);
    this._date2Picker.Name = "_date2Picker";
    this._date2Picker.Size = new Size(100, 20);
    this._date2Picker.TabIndex = 10;
    this.AcceptButton = (IButtonControl) this._okButton;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this._cancButton;
    this.ClientSize = new Size(422, 260);
    this.Controls.Add((Control) this._groupBox);
    this.Controls.Add((Control) this._buttonsPanel);
    this.Controls.Add((Control) this._label1);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (ReportPeriodForm);
    this.Padding = new Padding(15);
    this.ShowInTaskbar = false;
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Выбор периода";
    this.FormClosing += new FormClosingEventHandler(this.ReportPeriodForm_FormClosing);
    this.Load += new EventHandler(this.ReportPeriodForm_Load);
    this._buttonsPanel.ResumeLayout(false);
    this._groupBox.ResumeLayout(false);
    this._groupBox.PerformLayout();
    this._daysEdit.EndInit();
    this.ResumeLayout(false);
  }
}
