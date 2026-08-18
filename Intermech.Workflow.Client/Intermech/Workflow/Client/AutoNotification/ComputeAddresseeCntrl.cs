// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.AutoNotification.ComputeAddresseeCntrl
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Client.AutoNotification;

public class ComputeAddresseeCntrl : UserControl, ICanSaveNotifSettings
{
  private bool _isChanged;
  private IContainer components;

  public event EventHandler Modified;

  public bool IsChanged
  {
    get => this._isChanged;
    protected set
    {
      this._isChanged = value;
      EventHandler modified = this.Modified;
      if (!value || modified == null)
        return;
      modified((object) this, (EventArgs) null);
    }
  }

  protected ComputeAddresseeCntrl() => this.InitializeComponent();

  public virtual void SaveSettings()
  {
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.SuspendLayout();
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Name = nameof (ComputeAddresseeCntrl);
    this.Size = new Size(516, 327);
    this.ResumeLayout(false);
  }
}
