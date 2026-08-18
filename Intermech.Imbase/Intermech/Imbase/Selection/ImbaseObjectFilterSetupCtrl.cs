// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Selection.ImbaseObjectFilterSetupCtrl
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Imbase.Controls;
using Intermech.Interfaces.Client;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Selection;

public class ImbaseObjectFilterSetupCtrl : UserControl
{
  private bool _isAdmin;
  private IContainer components;
  private OwnerGuidTune _ownGuidTune;
  private ImbaseObjFilterTune _objFilterTune;

  public long ImCatalogID
  {
    get => this._objFilterTune.ImCatalogID;
    set => this._objFilterTune.ImCatalogID = value;
  }

  public ImbaseObjFilterTune FilterTune => this._objFilterTune;

  public ImbaseObjectFilterSetupCtrl()
  {
    this.InitializeComponent();
    if (this.DesignMode)
      return;
    this._isAdmin = (ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole).IsAdmin;
    this._objFilterTune.ReadOnly = !this._isAdmin;
  }

  private void ownGuidTune_OwnerChanged(object sender, EventArgs e)
  {
    this._objFilterTune.OwnerGuid = this._ownGuidTune.OwnerGuid;
    if (this._isAdmin)
      return;
    this._objFilterTune.ReadOnly = this._ownGuidTune.OwnerGuid == null || this._ownGuidTune.OwnerType != OwnerGuidTune.OwnerFilterType.User;
  }

  public void LoadData() => this._objFilterTune.LoadData();

  public void SaveData() => this._objFilterTune.SaveData();

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this._ownGuidTune = new OwnerGuidTune();
    this._objFilterTune = new ImbaseObjFilterTune();
    this.SuspendLayout();
    this._ownGuidTune.Caption = "Тип фильтра";
    this._ownGuidTune.Dock = DockStyle.Bottom;
    this._ownGuidTune.Location = new Point(0, 347);
    this._ownGuidTune.Name = "_ownGuidTune";
    this._ownGuidTune.Size = new Size(492, 94);
    this._ownGuidTune.TabIndex = 5;
    this._ownGuidTune.OwnerChanged += new EventHandler(this.ownGuidTune_OwnerChanged);
    this._objFilterTune.Dirty = false;
    this._objFilterTune.Dock = DockStyle.Fill;
    this._objFilterTune.ImCatalogID = -1L;
    this._objFilterTune.Location = new Point(0, 0);
    this._objFilterTune.Name = "_objFilterTune";
    this._objFilterTune.OwnerGuid = "";
    this._objFilterTune.ReadOnly = true;
    this._objFilterTune.Size = new Size(492, 347);
    this._objFilterTune.TabIndex = 6;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._objFilterTune);
    this.Controls.Add((Control) this._ownGuidTune);
    this.DoubleBuffered = true;
    this.Name = "ImbaseObjectFilterSetupCtrl_New";
    this.Size = new Size(492, 441);
    this.ResumeLayout(false);
  }
}
