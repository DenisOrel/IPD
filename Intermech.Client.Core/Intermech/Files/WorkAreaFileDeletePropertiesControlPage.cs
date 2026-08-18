
// Type: Intermech.Files.WorkAreaFileDeletePropertiesControlPage
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Files;

public class WorkAreaFileDeletePropertiesControlPage : 
  UserControl,
  IPropertyPage,
  IPropertyPageSearchOptionEvents
{
  private IDBConfigurations _configurations;
  private long _oldDayCountValue = 92;
  private int _oldDayModeValue;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private NumericUpDown dateCountNumeric;
  private Label label1;
  private ComboBox dateModeComboBox;

  public WorkAreaFileDeletePropertiesControlPage()
  {
    this.InitializeComponent();
    this.dateModeComboBox.SelectedIndex = this.dateModeComboBox.Items.Count > 0 ? 0 : -1;
    this._configurations = ApplicationServices.Container.GetService(typeof (IDBConfigurations)) as IDBConfigurations;
    if (this._configurations != null)
    {
      this.DayCount = this._configurations.ReadInteger("CLIENT", "WORKCLEANER", "CleaningPendingDateCount", 92L, DBConfigMode.UserOnly);
      this.DayMode = (int) this._configurations.ReadInteger("CLIENT", "WORKCLEANER", "CleaningPendingDateMode", 0L, DBConfigMode.UserOnly);
    }
    else
    {
      this.DayCount = 92L;
      this.DayMode = 0;
    }
    this._oldDayCountValue = this.DayCount;
    this._oldDayModeValue = this.DayMode;
  }

  public event EventHandler Changed;

  public PropertyPageType Type => PropertyPageType.Control;

  public object Control => (object) this;

  public string PageName => "Время жизни файлов в рабочей области";

  public void Apply()
  {
    if (!(ApplicationServices.Container.GetService(typeof (ICurrentUserAndRole)) is ICurrentUserAndRole service))
      return;
    this._configurations.WriteInteger("CLIENT", "WORKCLEANER", "CleaningPendingDateCount", this.DayCount, service.UserID);
    this._configurations.WriteInteger("CLIENT", "WORKCLEANER", "CleaningPendingDateMode", (long) this.DayMode, service.UserID);
    this._oldDayCountValue = this.DayCount;
    this._oldDayModeValue = this.DayMode;
  }

  public void Cancel()
  {
    this.DayCount = this._oldDayCountValue;
    this.DayMode = this._oldDayModeValue;
  }

  public string HelpTopicID => string.Empty;

  public string HeaderText
  {
    [DebuggerStepThrough] get => this.PageName;
  }

  public List<string> GetOptionNames()
  {
    return !(this.Control is ClassWrapperForPropertyGrid control) ? new List<string>() : IPropertyPageHelper.GetOptionNames((ICustomTypeDescriptor) control);
  }

  public long DayCount
  {
    get => (long) this.dateCountNumeric.Value;
    set => this.dateCountNumeric.Value = (Decimal) value;
  }

  public int DayMode
  {
    get => this.dateModeComboBox.SelectedIndex;
    set
    {
      if (value >= 0 && value <= 3 && this.dateModeComboBox.Items.Count == 4)
        this.dateModeComboBox.SelectedIndex = value;
      else
        this.dateModeComboBox.SelectedIndex = this.dateModeComboBox.Items.Count > 0 ? 0 : -1;
    }
  }

  private void dateCountNumeric_ValueChanged(object sender, EventArgs e)
  {
    EventHandler changed = this.Changed;
    if (changed == null)
      return;
    changed((object) this, (EventArgs) null);
  }

  private void dateModeComboBox_SelectedIndexChanged(object sender, EventArgs e)
  {
    EventHandler changed = this.Changed;
    if (changed == null)
      return;
    changed((object) this, (EventArgs) null);
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
    this.dateCountNumeric = new NumericUpDown();
    this.label1 = new Label();
    this.dateModeComboBox = new ComboBox();
    this.dateCountNumeric.BeginInit();
    this.SuspendLayout();
    this.dateCountNumeric.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.dateCountNumeric.Location = new Point(285, 14);
    this.dateCountNumeric.Maximum = new Decimal(new int[4]
    {
      999999999,
      0,
      0,
      0
    });
    this.dateCountNumeric.Name = "dateCountNumeric";
    this.dateCountNumeric.Size = new Size(80 /*0x50*/, 20);
    this.dateCountNumeric.TabIndex = 0;
    this.dateCountNumeric.Value = new Decimal(new int[4]
    {
      92,
      0,
      0,
      0
    });
    this.dateCountNumeric.ValueChanged += new EventHandler(this.dateCountNumeric_ValueChanged);
    this.dateCountNumeric.TextChanged += new EventHandler(this.dateCountNumeric_ValueChanged);
    this.label1.AutoSize = true;
    this.label1.Location = new Point(3, 16 /*0x10*/);
    this.label1.Name = "label1";
    this.label1.Size = new Size(276, 13);
    this.label1.TabIndex = 1;
    this.label1.Text = "Удалять файлы, если не было обращений в течении:";
    this.dateModeComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.dateModeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
    this.dateModeComboBox.FormattingEnabled = true;
    this.dateModeComboBox.Items.AddRange(new object[4]
    {
      (object) "Дней",
      (object) "Недель",
      (object) "Месяцев",
      (object) "Лет"
    });
    this.dateModeComboBox.Location = new Point(371, 13);
    this.dateModeComboBox.Name = "dateModeComboBox";
    this.dateModeComboBox.Size = new Size(94, 21);
    this.dateModeComboBox.TabIndex = 2;
    this.dateModeComboBox.SelectedIndexChanged += new EventHandler(this.dateModeComboBox_SelectedIndexChanged);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((System.Windows.Forms.Control) this.dateModeComboBox);
    this.Controls.Add((System.Windows.Forms.Control) this.label1);
    this.Controls.Add((System.Windows.Forms.Control) this.dateCountNumeric);
    this.MinimumSize = new Size(480, 50);
    this.Name = nameof (WorkAreaFileDeletePropertiesControlPage);
    this.Size = new Size(480, 50);
    this.dateCountNumeric.EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
