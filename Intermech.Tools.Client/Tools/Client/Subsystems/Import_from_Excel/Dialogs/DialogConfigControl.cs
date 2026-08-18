// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.Subsystems.Import_from_Excel.Dialogs.DialogConfigControl
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools.Client.Subsystems.Import_from_Excel.Dialogs;

public class DialogConfigControl : UserControl
{
  private Configuration[] _configurations;
  private DialogConfigControlType _dialogType;
  private IContainer components;
  private Panel pnlButtons;
  private Button btnCancel;
  private Button btnSubmit;
  private TextBox tbConfigname;
  private Label lblConfigName;
  private ImageList ilConfigs;
  private Panel pnlConfigType;
  private RadioButton rbCommon;
  private RadioButton rbPersonal;
  private Label lblConfigType;
  private Panel pnlMain;
  private ListView lvConfigs;

  public event EventHandler OnAccept;

  public event EventHandler OnCancel;

  public DialogConfigControl(
    DialogConfigControlType dialogType,
    IEnumerable<Configuration> configurations,
    bool isAdmin,
    string configurationName = "",
    ConfigurationType configurationType = ConfigurationType.Personal)
  {
    this.InitializeComponent();
    this._configurations = configurations.ToArray<Configuration>();
    this._dialogType = dialogType;
    if (this._dialogType == DialogConfigControlType.Open)
    {
      this.btnSubmit.Text = "Открыть";
      this.lblConfigName.Enabled = this.tbConfigname.Enabled = this.pnlConfigType.Enabled = false;
    }
    else
    {
      this.btnSubmit.Text = "Сохранить";
      this.lblConfigName.Enabled = this.tbConfigname.Enabled = this.pnlConfigType.Enabled = true;
      if (isAdmin)
      {
        if (configurationType == ConfigurationType.Personal)
          this.rbPersonal.Checked = true;
        else
          this.rbCommon.Checked = true;
      }
      else
      {
        this.rbCommon.Enabled = false;
        this.rbPersonal.Checked = true;
        this.lblConfigName.Enabled = this.pnlConfigType.Enabled = false;
      }
    }
    this.tbConfigname.Text = configurationName;
    this.FillListView();
  }

  public string ConfigurationName => this.tbConfigname.Text;

  public ConfigurationType ConfigurationType
  {
    get => !this.rbCommon.Checked ? ConfigurationType.Personal : ConfigurationType.Common;
  }

  public Configuration Configuration
  {
    get
    {
      return this.lvConfigs.SelectedItems.Count > 0 ? (Configuration) this.lvConfigs.SelectedItems[0].Tag : (Configuration) null;
    }
  }

  private void FillListView()
  {
    this.lvConfigs.Items.Clear();
    foreach (Configuration configuration in this._configurations)
      this.lvConfigs.Items.Add(new ListViewItem(configuration.Name)
      {
        Tag = (object) configuration,
        ImageIndex = (int) configuration.Type
      });
  }

  private void btnSubmit_Click(object sender, EventArgs e)
  {
    EventHandler onAccept = this.OnAccept;
    if (onAccept == null)
      return;
    onAccept((object) this, e);
  }

  private void btnCancel_Click(object sender, EventArgs e)
  {
    EventHandler onCancel = this.OnCancel;
    if (onCancel == null)
      return;
    onCancel((object) this, e);
  }

  private void lvConfigs_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this.lvConfigs.SelectedItems.Count <= 0)
      return;
    Configuration tag = (Configuration) this.lvConfigs.SelectedItems[0].Tag;
    if (tag == null)
      return;
    this.tbConfigname.Text = tag.Name;
    if (tag.Type == ConfigurationType.Personal)
      this.rbPersonal.Checked = true;
    else
      this.rbCommon.Checked = true;
  }

  private void lvConfigs_MouseDoubleClick(object sender, MouseEventArgs e)
  {
    if (this._dialogType != DialogConfigControlType.Open || this.lvConfigs.SelectedItems.Count <= 0)
      return;
    Configuration tag = (Configuration) this.lvConfigs.SelectedItems[0].Tag;
    if (tag == null)
      return;
    this.tbConfigname.Text = tag.Name;
    EventHandler onAccept = this.OnAccept;
    if (onAccept == null)
      return;
    onAccept((object) this, (EventArgs) e);
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (DialogConfigControl));
    this.pnlButtons = new Panel();
    this.pnlConfigType = new Panel();
    this.rbCommon = new RadioButton();
    this.rbPersonal = new RadioButton();
    this.lblConfigType = new Label();
    this.lblConfigName = new Label();
    this.btnCancel = new Button();
    this.btnSubmit = new Button();
    this.tbConfigname = new TextBox();
    this.ilConfigs = new ImageList(this.components);
    this.pnlMain = new Panel();
    this.lvConfigs = new ListView();
    this.pnlButtons.SuspendLayout();
    this.pnlConfigType.SuspendLayout();
    this.pnlMain.SuspendLayout();
    this.SuspendLayout();
    this.pnlButtons.Controls.Add((Control) this.pnlConfigType);
    this.pnlButtons.Controls.Add((Control) this.lblConfigName);
    this.pnlButtons.Controls.Add((Control) this.btnCancel);
    this.pnlButtons.Controls.Add((Control) this.btnSubmit);
    this.pnlButtons.Controls.Add((Control) this.tbConfigname);
    this.pnlButtons.Dock = DockStyle.Bottom;
    this.pnlButtons.Location = new Point(0, 215);
    this.pnlButtons.Name = "pnlButtons";
    this.pnlButtons.Size = new Size(432, 130);
    this.pnlButtons.TabIndex = 0;
    this.pnlConfigType.Controls.Add((Control) this.rbCommon);
    this.pnlConfigType.Controls.Add((Control) this.rbPersonal);
    this.pnlConfigType.Controls.Add((Control) this.lblConfigType);
    this.pnlConfigType.Dock = DockStyle.Top;
    this.pnlConfigType.Location = new Point(0, 0);
    this.pnlConfigType.Name = "pnlConfigType";
    this.pnlConfigType.Size = new Size(432, 53);
    this.pnlConfigType.TabIndex = 7;
    this.rbCommon.AutoSize = true;
    this.rbCommon.Location = new Point(125, 28);
    this.rbCommon.Name = "rbCommon";
    this.rbCommon.Size = new Size(60, 17);
    this.rbCommon.TabIndex = 9;
    this.rbCommon.Text = "Общая";
    this.rbCommon.UseVisualStyleBackColor = true;
    this.rbPersonal.AutoSize = true;
    this.rbPersonal.Checked = true;
    this.rbPersonal.Location = new Point(125, 6);
    this.rbPersonal.Name = "rbPersonal";
    this.rbPersonal.Size = new Size(99, 17);
    this.rbPersonal.TabIndex = 8;
    this.rbPersonal.TabStop = true;
    this.rbPersonal.Text = "Персональная";
    this.rbPersonal.UseVisualStyleBackColor = true;
    this.lblConfigType.AutoSize = true;
    this.lblConfigType.Location = new Point(15, 8);
    this.lblConfigType.Name = "lblConfigType";
    this.lblConfigType.Size = new Size(104, 13);
    this.lblConfigType.TabIndex = 7;
    this.lblConfigType.Text = "Тип конфигурации:";
    this.lblConfigName.AutoSize = true;
    this.lblConfigName.Location = new Point(12, 59);
    this.lblConfigName.Name = "lblConfigName";
    this.lblConfigName.Size = new Size(107, 13);
    this.lblConfigName.TabIndex = 3;
    this.lblConfigName.Text = "Имя конфигурации:";
    this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnCancel.Location = new Point(345, 95);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Size = new Size(75, 23);
    this.btnCancel.TabIndex = 2;
    this.btnCancel.Text = "Отмена";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
    this.btnSubmit.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnSubmit.Location = new Point(264, 95);
    this.btnSubmit.Name = "btnSubmit";
    this.btnSubmit.Size = new Size(75, 23);
    this.btnSubmit.TabIndex = 1;
    this.btnSubmit.UseVisualStyleBackColor = true;
    this.btnSubmit.Click += new EventHandler(this.btnSubmit_Click);
    this.tbConfigname.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.tbConfigname.Location = new Point(125, 59);
    this.tbConfigname.Name = "tbConfigname";
    this.tbConfigname.Size = new Size(295, 20);
    this.tbConfigname.TabIndex = 0;
    this.ilConfigs.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("ilConfigs.ImageStream");
    this.ilConfigs.TransparentColor = Color.Transparent;
    this.ilConfigs.Images.SetKeyName(0, "iconfinder_Configuration-2-01_1976057.png");
    this.ilConfigs.Images.SetKeyName(1, "iconfinder_Gear_4200781.png");
    this.pnlMain.Controls.Add((Control) this.lvConfigs);
    this.pnlMain.Dock = DockStyle.Fill;
    this.pnlMain.Location = new Point(0, 0);
    this.pnlMain.Name = "pnlMain";
    this.pnlMain.Padding = new Padding(10);
    this.pnlMain.Size = new Size(432, 215);
    this.pnlMain.TabIndex = 2;
    this.lvConfigs.Dock = DockStyle.Fill;
    this.lvConfigs.HideSelection = false;
    this.lvConfigs.Location = new Point(10, 10);
    this.lvConfigs.Name = "lvConfigs";
    this.lvConfigs.Size = new Size(412, 195);
    this.lvConfigs.SmallImageList = this.ilConfigs;
    this.lvConfigs.TabIndex = 2;
    this.lvConfigs.UseCompatibleStateImageBehavior = false;
    this.lvConfigs.View = View.List;
    this.lvConfigs.SelectedIndexChanged += new EventHandler(this.lvConfigs_SelectedIndexChanged);
    this.lvConfigs.MouseDoubleClick += new MouseEventHandler(this.lvConfigs_MouseDoubleClick);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.pnlMain);
    this.Controls.Add((Control) this.pnlButtons);
    this.Name = nameof (DialogConfigControl);
    this.Size = new Size(432, 345);
    this.pnlButtons.ResumeLayout(false);
    this.pnlButtons.PerformLayout();
    this.pnlConfigType.ResumeLayout(false);
    this.pnlConfigType.PerformLayout();
    this.pnlMain.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
