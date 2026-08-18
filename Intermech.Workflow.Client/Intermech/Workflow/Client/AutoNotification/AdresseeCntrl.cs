// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.AutoNotification.AdresseeCntrl
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.Interfaces.Workflow.AutoNotification;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Client.AutoNotification;

public class AdresseeCntrl : UserControl, ICanSaveNotifSettings
{
  private AutoNotificationSettings _notifSettings;
  private readonly SpecificAdresseeCntrl specificAddrCntrl;
  private readonly ComputeAddresseeCntrl computeAddrCntrl;
  private bool _isChanged;
  private IContainer components;
  private Panel panel;
  private GroupBox gbAdresseeType;
  private RadioButton rbComputeAdressee;
  private RadioButton rbSpecificAdressee;

  public event EventHandler Modified;

  public bool IsChanged
  {
    get => this._isChanged;
    private set
    {
      this._isChanged = value;
      EventHandler modified = this.Modified;
      if (!value || modified == null)
        return;
      modified((object) this, (EventArgs) null);
    }
  }

  public AdresseeCntrl(AutoNotificationSettings notifSettings)
  {
    this.InitializeComponent();
    this._notifSettings = notifSettings;
    this.specificAddrCntrl = new SpecificAdresseeCntrl(this._notifSettings);
    switch (notifSettings.NotifEventType)
    {
      case NotificationEventType.None:
        this.panel.Controls.Add((Control) this.specificAddrCntrl);
        this.specificAddrCntrl.Dock = DockStyle.Top;
        this.panel.Controls.Add((Control) this.computeAddrCntrl);
        this.computeAddrCntrl.Dock = DockStyle.Top;
        this.computeAddrCntrl.Modified += new EventHandler(this.OnInnerControlModified);
        this.specificAddrCntrl.Modified += new EventHandler(this.OnInnerControlModified);
        this.UpdateAdresseeTypeFromSettings();
        break;
      case NotificationEventType.AddLink:
      case NotificationEventType.DeleteLink:
        this.computeAddrCntrl = (ComputeAddresseeCntrl) new ComputeAdresseeForRelationCntrl(this._notifSettings);
        goto case NotificationEventType.None;
      case NotificationEventType.GetAccess:
        this.computeAddrCntrl = (ComputeAddresseeCntrl) new ComputeAdresseeForAccessCntrl(this._notifSettings);
        goto case NotificationEventType.None;
      default:
        this.computeAddrCntrl = (ComputeAddresseeCntrl) new ComputeAdresseeForObjectCntrl(this._notifSettings);
        goto case NotificationEventType.None;
    }
  }

  private void rbSpecificAdressee_CheckedChanged(object sender, EventArgs e)
  {
    if (this.rbSpecificAdressee.Checked)
      this.specificAddrCntrl.Visible = true;
    else
      this.specificAddrCntrl.Visible = false;
    this.IsChanged = true;
  }

  private void rbComputeAdressee_CheckedChanged(object sender, EventArgs e)
  {
    if (this.rbComputeAdressee.Checked)
      this.computeAddrCntrl.Visible = true;
    else
      this.computeAddrCntrl.Visible = false;
    this.IsChanged = true;
  }

  private void OnInnerControlModified(object sender, EventArgs e) => this.IsChanged = true;

  public void SaveSettings()
  {
    if (this.rbSpecificAdressee.Checked)
      this.specificAddrCntrl.SaveSettings();
    if (this.rbComputeAdressee.Checked)
      this.computeAddrCntrl.SaveSettings();
    this.IsChanged = false;
  }

  public override void Refresh()
  {
    base.Refresh();
    this.specificAddrCntrl.Refresh();
    this.computeAddrCntrl.Refresh();
    this.UpdateAdresseeTypeFromSettings();
  }

  private void UpdateAdresseeTypeFromSettings()
  {
    if (this._notifSettings.Adressee == null || this._notifSettings.Adressee.GetType() == typeof (SpecificAdressee))
    {
      this.computeAddrCntrl.Visible = false;
      this.rbSpecificAdressee.Checked = true;
      this.specificAddrCntrl.Visible = true;
      this.specificAddrCntrl.Enabled = true;
    }
    else
    {
      if (!(this._notifSettings.Adressee.GetType() == typeof (ComputeAdressee)))
        return;
      this.computeAddrCntrl.Visible = true;
      this.computeAddrCntrl.Enabled = true;
      this.rbComputeAdressee.Checked = true;
      this.specificAddrCntrl.Visible = false;
      this.specificAddrCntrl.Enabled = true;
    }
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.panel = new Panel();
    this.gbAdresseeType = new GroupBox();
    this.rbComputeAdressee = new RadioButton();
    this.rbSpecificAdressee = new RadioButton();
    this.gbAdresseeType.SuspendLayout();
    this.SuspendLayout();
    this.panel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.panel.AutoScroll = true;
    this.panel.Location = new Point(0, 77);
    this.panel.Name = "panel";
    this.panel.Size = new Size(834, 442);
    this.panel.TabIndex = 0;
    this.gbAdresseeType.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.gbAdresseeType.BackColor = SystemColors.Control;
    this.gbAdresseeType.Controls.Add((Control) this.rbComputeAdressee);
    this.gbAdresseeType.Controls.Add((Control) this.rbSpecificAdressee);
    this.gbAdresseeType.FlatStyle = FlatStyle.System;
    this.gbAdresseeType.Location = new Point(0, 0);
    this.gbAdresseeType.Name = "gbAdresseeType";
    this.gbAdresseeType.Size = new Size(834, 71);
    this.gbAdresseeType.TabIndex = 1;
    this.gbAdresseeType.TabStop = false;
    this.gbAdresseeType.Text = "Способ выбора адресата";
    this.rbComputeAdressee.AutoSize = true;
    this.rbComputeAdressee.Location = new Point(7, 44);
    this.rbComputeAdressee.Name = "rbComputeAdressee";
    this.rbComputeAdressee.Size = new Size(144 /*0x90*/, 17);
    this.rbComputeAdressee.TabIndex = 1;
    this.rbComputeAdressee.Text = "Вычисляемый  адресат";
    this.rbComputeAdressee.UseVisualStyleBackColor = true;
    this.rbComputeAdressee.CheckedChanged += new EventHandler(this.rbComputeAdressee_CheckedChanged);
    this.rbSpecificAdressee.AutoSize = true;
    this.rbSpecificAdressee.Checked = true;
    this.rbSpecificAdressee.Location = new Point(7, 20);
    this.rbSpecificAdressee.Name = "rbSpecificAdressee";
    this.rbSpecificAdressee.Size = new Size(131, 17);
    this.rbSpecificAdressee.TabIndex = 0;
    this.rbSpecificAdressee.TabStop = true;
    this.rbSpecificAdressee.Text = "Конкретный адресат";
    this.rbSpecificAdressee.UseVisualStyleBackColor = true;
    this.rbSpecificAdressee.CheckedChanged += new EventHandler(this.rbSpecificAdressee_CheckedChanged);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.BackColor = SystemColors.Control;
    this.Controls.Add((Control) this.gbAdresseeType);
    this.Controls.Add((Control) this.panel);
    this.Name = nameof (AdresseeCntrl);
    this.Size = new Size(837, 519);
    this.gbAdresseeType.ResumeLayout(false);
    this.gbAdresseeType.PerformLayout();
    this.ResumeLayout(false);
  }
}
