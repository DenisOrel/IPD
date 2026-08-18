// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.Email.TimeTableControl
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Workflow;
using Intermech.Interfaces.Workflow.Email;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Client.Email;

public class TimeTableControl : UserControl, IPropertyPage, IPropertyPageSearchOptionEvents
{
  private IContainer components;
  private GroupBox groupBox1;
  private TextBox tbMachineName;
  private Label label1;
  private CheckBox cbRemoveMessages;
  private CheckBox cbWorkTimeOnly;
  private Button bChoiseCalendar;
  private Label label3;
  private TextBox tbCalendar;
  private NumericUpDown nudPeriod;
  private Label label2;
  private Panel panel1;
  private CheckBox cbEnable;

  public TimeTableControl()
  {
    this.InitializeComponent();
    this.LoadData();
  }

  private void LoadData()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      EmailDownloadSettings downloadSettings = new EmailDownloadSettings();
      downloadSettings.Load(sessionKeeper.Session);
      this.tbMachineName.Text = downloadSettings.ComputerName;
      this.cbRemoveMessages.Checked = downloadSettings.RemoveMessages;
      this.nudPeriod.Value = (Decimal) downloadSettings.Period;
      QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(downloadSettings.CalendarGuid);
      this.tbCalendar.Text = objectInfo.Empty ? LocalizationHolder.rm.GetString("Workflow.Client_61") : objectInfo.Caption;
      this.tbCalendar.Tag = (object) downloadSettings.CalendarGuid;
      this.cbWorkTimeOnly.Checked = downloadSettings.WorkTimeOnly;
      this.cbEnable.Checked = downloadSettings.EnableDownload;
      this.panel1.Enabled = this.cbEnable.Checked;
    }
  }

  private void OnChanged()
  {
    EventHandler changed = this.Changed;
    if (changed == null)
      return;
    changed((object) this, new EventArgs());
  }

  public event EventHandler Changed;

  public PropertyPageType Type => PropertyPageType.Control;

  public object Control => (object) this;

  public string PageName => LocalizationHolder.rm.GetString("Workflow.Client_62");

  public string HeaderText
  {
    [DebuggerStepThrough] get => this.PageName;
  }

  public void Apply()
  {
    EmailDownloadSettings downloadSettings = new EmailDownloadSettings();
    downloadSettings.EnableDownload = this.cbEnable.Checked;
    if (downloadSettings.EnableDownload && this.tbMachineName.Text == string.Empty)
      throw new Exception(LocalizationHolder.rm.GetString("Workflow.Client_63"));
    downloadSettings.ComputerName = this.tbMachineName.Text;
    downloadSettings.RemoveMessages = this.cbRemoveMessages.Checked;
    downloadSettings.Period = Convert.ToInt32(this.nudPeriod.Value);
    downloadSettings.WorkTimeOnly = this.cbWorkTimeOnly.Checked;
    if (downloadSettings.EnableDownload && downloadSettings.WorkTimeOnly && this.tbCalendar.Tag == null)
      throw new Exception(LocalizationHolder.rm.GetString("Workflow.Client_64"));
    downloadSettings.CalendarGuid = (Guid) this.tbCalendar.Tag;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      downloadSettings.Save(sessionKeeper.Session);
      ((IEmailDownloadService) sessionKeeper.Session.GetCustomService(typeof (IEmailDownloadService))).ReloadSettings();
    }
  }

  public void Cancel() => this.LoadData();

  public string HelpTopicID => "2494";

  public List<string> GetOptionNames()
  {
    return !(this.Control is System.Windows.Forms.Control control) ? new List<string>() : IPropertyPageHelper.GetOptionNames(control);
  }

  private void tbMachineName_TextChanged(object sender, EventArgs e) => this.OnChanged();

  private void cbEnable_CheckedChanged(object sender, EventArgs e)
  {
    this.panel1.Enabled = this.cbEnable.Checked;
    this.OnChanged();
  }

  private void cbWorkTimeOnly_CheckedChanged(object sender, EventArgs e)
  {
    this.tbCalendar.Enabled = this.bChoiseCalendar.Enabled = this.cbWorkTimeOnly.Checked;
    this.OnChanged();
  }

  private void nudPeriod_ValueChanged(object sender, EventArgs e) => this.OnChanged();

  private void bChoiseCalendar_Click(object sender, EventArgs e)
  {
    object[] objArray = SelectionWindow.Select(LocalizationHolder.rm.GetString("Workflow.Client_65"), (IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(MetaDataHelper.GetObjectTypeID("cad00d87-306c-11d8-b4e9-00304f19f545")), typeof (IDBTypedObjectID), SelectionOptions.SelectObjects | SelectionOptions.DisableSelectFromTree | SelectionOptions.DisableMultiselect);
    if (objArray == null || objArray.Length != 1)
      return;
    IDBTypedObjectID dbTypedObjectId = objArray[0] as IDBTypedObjectID;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(dbTypedObjectId.ObjectID);
      if (this.tbCalendar.Tag != null && dbObject.ObjectGUID.Equals((Guid) this.tbCalendar.Tag))
        return;
      this.tbCalendar.Text = dbObject.Caption;
      this.tbCalendar.Tag = (object) dbObject.ObjectGUID;
      this.OnChanged();
    }
  }

  private void cbRemoveMessages_CheckedChanged(object sender, EventArgs e) => this.OnChanged();

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.groupBox1 = new GroupBox();
    this.panel1 = new Panel();
    this.label1 = new Label();
    this.label2 = new Label();
    this.cbRemoveMessages = new CheckBox();
    this.nudPeriod = new NumericUpDown();
    this.cbWorkTimeOnly = new CheckBox();
    this.tbCalendar = new TextBox();
    this.tbMachineName = new TextBox();
    this.label3 = new Label();
    this.bChoiseCalendar = new Button();
    this.cbEnable = new CheckBox();
    this.groupBox1.SuspendLayout();
    this.panel1.SuspendLayout();
    this.nudPeriod.BeginInit();
    this.SuspendLayout();
    this.groupBox1.Controls.Add((System.Windows.Forms.Control) this.panel1);
    this.groupBox1.Controls.Add((System.Windows.Forms.Control) this.cbEnable);
    this.groupBox1.Dock = DockStyle.Fill;
    this.groupBox1.Location = new Point(0, 0);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(420, 285);
    this.groupBox1.TabIndex = 2;
    this.groupBox1.TabStop = false;
    this.groupBox1.Text = "Автоматический прием почты";
    this.panel1.Controls.Add((System.Windows.Forms.Control) this.label1);
    this.panel1.Controls.Add((System.Windows.Forms.Control) this.label2);
    this.panel1.Controls.Add((System.Windows.Forms.Control) this.cbRemoveMessages);
    this.panel1.Controls.Add((System.Windows.Forms.Control) this.nudPeriod);
    this.panel1.Controls.Add((System.Windows.Forms.Control) this.cbWorkTimeOnly);
    this.panel1.Controls.Add((System.Windows.Forms.Control) this.tbCalendar);
    this.panel1.Controls.Add((System.Windows.Forms.Control) this.tbMachineName);
    this.panel1.Controls.Add((System.Windows.Forms.Control) this.label3);
    this.panel1.Controls.Add((System.Windows.Forms.Control) this.bChoiseCalendar);
    this.panel1.Location = new Point(0, 53);
    this.panel1.Name = "panel1";
    this.panel1.Size = new Size(420, 231);
    this.panel1.TabIndex = 6;
    this.label1.AutoSize = true;
    this.label1.Location = new Point(14, 10);
    this.label1.Name = "label1";
    this.label1.Size = new Size(367, 13);
    this.label1.TabIndex = 0;
    this.label1.Text = "Имя компьютера на котором принимается почта (сервер приложений)";
    this.label2.AutoSize = true;
    this.label2.Location = new Point(14, 96 /*0x60*/);
    this.label2.Name = "label2";
    this.label2.Size = new Size(111, 13);
    this.label2.TabIndex = 0;
    this.label2.Text = "Периодичность, мин";
    this.cbRemoveMessages.AutoSize = true;
    this.cbRemoveMessages.Location = new Point(17, 52);
    this.cbRemoveMessages.Name = "cbRemoveMessages";
    this.cbRemoveMessages.Size = new Size(308, 17);
    this.cbRemoveMessages.TabIndex = 2;
    this.cbRemoveMessages.Text = "После получения удалять письма на почтовом сервере";
    this.cbRemoveMessages.UseVisualStyleBackColor = true;
    this.cbRemoveMessages.CheckedChanged += new EventHandler(this.cbRemoveMessages_CheckedChanged);
    this.nudPeriod.Location = new Point(17, 112 /*0x70*/);
    this.nudPeriod.Maximum = new Decimal(new int[4]
    {
      1000,
      0,
      0,
      0
    });
    this.nudPeriod.Minimum = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this.nudPeriod.Name = "nudPeriod";
    this.nudPeriod.Size = new Size(63 /*0x3F*/, 20);
    this.nudPeriod.TabIndex = 1;
    this.nudPeriod.Value = new Decimal(new int[4]
    {
      15,
      0,
      0,
      0
    });
    this.nudPeriod.ValueChanged += new EventHandler(this.nudPeriod_ValueChanged);
    this.cbWorkTimeOnly.AutoSize = true;
    this.cbWorkTimeOnly.Checked = true;
    this.cbWorkTimeOnly.CheckState = CheckState.Checked;
    this.cbWorkTimeOnly.Location = new Point(17, 153);
    this.cbWorkTimeOnly.Name = "cbWorkTimeOnly";
    this.cbWorkTimeOnly.Size = new Size(239, 17);
    this.cbWorkTimeOnly.TabIndex = 5;
    this.cbWorkTimeOnly.Text = "Принимать почту только в рабочее время";
    this.cbWorkTimeOnly.UseVisualStyleBackColor = true;
    this.cbWorkTimeOnly.CheckedChanged += new EventHandler(this.cbWorkTimeOnly_CheckedChanged);
    this.tbCalendar.Location = new Point(42, 190);
    this.tbCalendar.Name = "tbCalendar";
    this.tbCalendar.Size = new Size(334, 20);
    this.tbCalendar.TabIndex = 2;
    this.tbMachineName.Location = new Point(17, 26);
    this.tbMachineName.Name = "tbMachineName";
    this.tbMachineName.Size = new Size(373, 20);
    this.tbMachineName.TabIndex = 1;
    this.tbMachineName.TextChanged += new EventHandler(this.tbMachineName_TextChanged);
    this.label3.AutoSize = true;
    this.label3.Location = new Point(39, 174);
    this.label3.Name = "label3";
    this.label3.Size = new Size(62, 13);
    this.label3.TabIndex = 3;
    this.label3.Text = "Календарь";
    this.bChoiseCalendar.Location = new Point(376, 188);
    this.bChoiseCalendar.Name = "bChoiseCalendar";
    this.bChoiseCalendar.Size = new Size(24, 23);
    this.bChoiseCalendar.TabIndex = 4;
    this.bChoiseCalendar.Text = "...";
    this.bChoiseCalendar.UseVisualStyleBackColor = true;
    this.bChoiseCalendar.Click += new EventHandler(this.bChoiseCalendar_Click);
    this.cbEnable.AutoSize = true;
    this.cbEnable.Location = new Point(20, 30);
    this.cbEnable.Name = "cbEnable";
    this.cbEnable.Size = new Size(236, 17);
    this.cbEnable.TabIndex = 0;
    this.cbEnable.Text = "Разрешить автоматический прием почты";
    this.cbEnable.UseVisualStyleBackColor = true;
    this.cbEnable.CheckedChanged += new EventHandler(this.cbEnable_CheckedChanged);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((System.Windows.Forms.Control) this.groupBox1);
    this.Name = nameof (TimeTableControl);
    this.Size = new Size(420, 285);
    this.groupBox1.ResumeLayout(false);
    this.groupBox1.PerformLayout();
    this.panel1.ResumeLayout(false);
    this.panel1.PerformLayout();
    this.nudPeriod.EndInit();
    this.ResumeLayout(false);
  }
}
