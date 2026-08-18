// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.AutoNotification.ComputeAdresseeForAccessCntrl
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

public class ComputeAdresseeForAccessCntrl : ComputeAddresseeCntrl
{
  private AdresseeSourceType _adresseeSourceType = AdresseeSourceType.ObjectAuthor;
  private readonly AutoNotificationSettings _notifSettings;
  private IContainer components;
  private GroupBox gbAdressee;
  private RadioButton rbObjectOwner;
  private RadioButton rbObjectAuthor;

  public ComputeAdresseeForAccessCntrl(AutoNotificationSettings notificationSettings)
  {
    this.InitializeComponent();
    this._notifSettings = notificationSettings;
    this.UpdateControl();
  }

  private void UpdateControl()
  {
    if (!(this._notifSettings.Adressee is ComputeAdressee adressee))
      return;
    switch (adressee.AdresseeSource.AdresseeSourceType)
    {
      case AdresseeSourceType.ObjectAuthor:
        this.rbObjectAuthor.Checked = true;
        break;
      case AdresseeSourceType.ObjectOwner:
        this.rbObjectOwner.Checked = true;
        break;
    }
  }

  public override void SaveSettings()
  {
    this._notifSettings.Adressee = (Adressee) new ComputeAdressee(new AdresseeSource(this._adresseeSourceType), new ObjectSetSource(ObjectsCollectMethod.Initiator));
  }

  public override void Refresh()
  {
    base.Refresh();
    this.UpdateControl();
  }

  private void rbObjectAuthor_CheckedChanged(object sender, EventArgs e)
  {
    if (this.rbObjectOwner.Checked)
      this._adresseeSourceType = AdresseeSourceType.ObjectOwner;
    this.IsChanged = true;
  }

  private void rbObjectOwner_CheckedChanged(object sender, EventArgs e)
  {
    if (this.rbObjectOwner.Checked)
      this._adresseeSourceType = AdresseeSourceType.ObjectOwner;
    this.IsChanged = true;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.gbAdressee = new GroupBox();
    this.rbObjectOwner = new RadioButton();
    this.rbObjectAuthor = new RadioButton();
    this.gbAdressee.SuspendLayout();
    this.SuspendLayout();
    this.gbAdressee.Controls.Add((Control) this.rbObjectOwner);
    this.gbAdressee.Controls.Add((Control) this.rbObjectAuthor);
    this.gbAdressee.Cursor = Cursors.Default;
    this.gbAdressee.Dock = DockStyle.Fill;
    this.gbAdressee.FlatStyle = FlatStyle.System;
    this.gbAdressee.Location = new Point(0, 0);
    this.gbAdressee.Name = "gbAdressee";
    this.gbAdressee.Size = new Size(880, 304);
    this.gbAdressee.TabIndex = 4;
    this.gbAdressee.TabStop = false;
    this.gbAdressee.Text = "Адресат";
    this.rbObjectOwner.AutoSize = true;
    this.rbObjectOwner.Location = new Point(7, 43);
    this.rbObjectOwner.Name = "rbObjectOwner";
    this.rbObjectOwner.Size = new Size(119, 17);
    this.rbObjectOwner.TabIndex = 1;
    this.rbObjectOwner.Text = "Владелец объекта";
    this.rbObjectOwner.UseVisualStyleBackColor = true;
    this.rbObjectOwner.CheckedChanged += new EventHandler(this.rbObjectOwner_CheckedChanged);
    this.rbObjectAuthor.AutoSize = true;
    this.rbObjectAuthor.Checked = true;
    this.rbObjectAuthor.Location = new Point(7, 20);
    this.rbObjectAuthor.Name = "rbObjectAuthor";
    this.rbObjectAuthor.Size = new Size(100, 17);
    this.rbObjectAuthor.TabIndex = 0;
    this.rbObjectAuthor.TabStop = true;
    this.rbObjectAuthor.Text = "Автор объекта";
    this.rbObjectAuthor.UseVisualStyleBackColor = true;
    this.rbObjectAuthor.CheckedChanged += new EventHandler(this.rbObjectAuthor_CheckedChanged);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.gbAdressee);
    this.Name = nameof (ComputeAdresseeForAccessCntrl);
    this.Size = new Size(880, 304);
    this.gbAdressee.ResumeLayout(false);
    this.gbAdressee.PerformLayout();
    this.ResumeLayout(false);
  }
}
