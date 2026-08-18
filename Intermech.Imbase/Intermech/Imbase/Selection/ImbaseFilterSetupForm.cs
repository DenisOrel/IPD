// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Selection.ImbaseFilterSetupForm
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using ImSSP;
using Intermech.Client.Core;
using Intermech.Imbase.Common;
using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Selection;

public class ImbaseFilterSetupForm : Form
{
  private ImbaseFolderFilterSetupCtrl _fSetupCtrl;
  private ImbaseObjectFilterSetupCtrl _oSetupCtrl;
  private IContainer components;
  private TableLayoutPanel _tlp;
  private Button _btnCancel;
  private Button _btnOk;

  public ImbaseFilterSetupForm(long masterCatalogID, long slaveCatalogID, bool isFolderFilter)
  {
    this.InitializeComponent();
    if (this.DesignMode)
      return;
    UserControl userControl;
    if (isFolderFilter)
    {
      ImbaseFolderFilterSetupCtrl folderFilterSetupCtrl1 = new ImbaseFolderFilterSetupCtrl(masterCatalogID, slaveCatalogID);
      folderFilterSetupCtrl1.Dock = DockStyle.Fill;
      folderFilterSetupCtrl1.TabIndex = 1;
      ImbaseFolderFilterSetupCtrl folderFilterSetupCtrl2 = folderFilterSetupCtrl1;
      this._fSetupCtrl = folderFilterSetupCtrl1;
      userControl = (UserControl) folderFilterSetupCtrl2;
      this._fSetupCtrl.FilterTune.DirtyChanged += new EventHandler(this.OnFolderDirtyStateChanged);
      this._fSetupCtrl.LoadData();
    }
    else
    {
      ImbaseObjectFilterSetupCtrl objectFilterSetupCtrl1 = new ImbaseObjectFilterSetupCtrl();
      objectFilterSetupCtrl1.Dock = DockStyle.Fill;
      objectFilterSetupCtrl1.TabIndex = 1;
      ImbaseObjectFilterSetupCtrl objectFilterSetupCtrl2 = objectFilterSetupCtrl1;
      this._oSetupCtrl = objectFilterSetupCtrl1;
      userControl = (UserControl) objectFilterSetupCtrl2;
      this._oSetupCtrl.FilterTune.DirtyChanged += new EventHandler(this.OnObjectDirtyStateChanged);
      this._oSetupCtrl.LoadData();
      this._oSetupCtrl.ImCatalogID = slaveCatalogID;
    }
    this.SuspendLayout();
    this.Controls.Add((Control) userControl);
    userControl.BringToFront();
    this.ResumeLayout();
  }

  private void OnFolderDirtyStateChanged(object sender, EventArgs e)
  {
    this._btnOk.Enabled = this._fSetupCtrl.FilterTune.Dirty;
  }

  private void OnObjectDirtyStateChanged(object sender, EventArgs e)
  {
    this._btnOk.Enabled = this._oSetupCtrl.FilterTune.Dirty;
  }

  private void On_btnOk_Click(object sender, EventArgs e)
  {
    if (this._fSetupCtrl != null)
    {
      if (!this._fSetupCtrl.FilterTune.Dirty)
        return;
      this._fSetupCtrl.FilterTune.SaveFilter();
    }
    else
    {
      if (!this._oSetupCtrl.FilterTune.Dirty)
        return;
      this._oSetupCtrl.FilterTune.SaveData();
    }
  }

  private void On_btnCancel_Click(object sender, EventArgs e)
  {
    if (this._fSetupCtrl != null)
    {
      if (!this._fSetupCtrl.FilterTune.Dirty)
        return;
      this._fSetupCtrl.FilterTune.LoadFilter();
    }
    else
    {
      if (!this._oSetupCtrl.FilterTune.Dirty)
        return;
      this._oSetupCtrl.FilterTune.LoadData();
    }
  }

  protected override void OnLoad(EventArgs e)
  {
    base.OnLoad(e);
    FormStorage.LoadLayout((Control) this);
  }

  protected override void OnClosing(CancelEventArgs e)
  {
    base.OnClosing(e);
    if (this.DialogResult != DialogResult.Cancel)
    {
      if (this._fSetupCtrl != null)
      {
        if (this._fSetupCtrl.FilterTune.Dirty)
          e.Cancel = !this._fSetupCtrl.FilterTune.SaveFilter();
      }
      else if (this._oSetupCtrl.FilterTune.Dirty)
        this._oSetupCtrl.FilterTune.SaveData();
    }
    FormStorage.SaveLayout((Control) this);
  }

  public static DialogResult ShowSetupDialog(
    IWin32Window owner,
    int masterObjTypeID,
    long slaveCatalogID,
    bool isFolderFilter = true)
  {
    if (isFolderFilter)
    {
      long masterCatalogID = 0;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        List<long> catalogIdForObjType = ImbaseUtils.GetCatalogIDForObjType(new int[1]
        {
          masterObjTypeID
        }, sessionKeeper.Session);
        masterCatalogID = catalogIdForObjType.Count > sc_7896.ssp_techcard_7897(390415575) ? catalogIdForObjType[0] : masterCatalogID;
      }
      return ImbaseFilterSetupForm.ShowSetupDialog(owner, masterCatalogID, slaveCatalogID);
    }
    using (ImbaseFilterSetupForm imbaseFilterSetupForm = new ImbaseFilterSetupForm(0L, slaveCatalogID, isFolderFilter))
      return imbaseFilterSetupForm.ShowDialog(owner);
  }

  public static DialogResult ShowSetupDialog(
    IWin32Window owner,
    int masterObjTypeID,
    int slaveObjTypeID,
    bool isFolderFilter = true)
  {
    if (isFolderFilter)
    {
      long masterCatalogID = 0;
      long slaveCatalogID = 0;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        List<long> catalogIdForObjType1 = ImbaseUtils.GetCatalogIDForObjType(new int[1]
        {
          masterObjTypeID
        }, sessionKeeper.Session);
        masterCatalogID = catalogIdForObjType1.Count > sc_7896.ssp_techcard_7898(1541738975) ? catalogIdForObjType1[0] : masterCatalogID;
        List<long> catalogIdForObjType2 = ImbaseUtils.GetCatalogIDForObjType(new int[1]
        {
          slaveObjTypeID
        }, sessionKeeper.Session);
        slaveCatalogID = catalogIdForObjType2.Count > 0 ? catalogIdForObjType2[0] : (long) slaveObjTypeID;
      }
      return ImbaseFilterSetupForm.ShowSetupDialog(owner, masterCatalogID, slaveCatalogID);
    }
    long slaveCatalogID1 = 0;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      List<long> catalogIdForObjType = ImbaseUtils.GetCatalogIDForObjType(new int[1]
      {
        slaveObjTypeID
      }, sessionKeeper.Session);
      slaveCatalogID1 = catalogIdForObjType.Count > 0 ? catalogIdForObjType[0] : (long) slaveObjTypeID;
    }
    return ImbaseFilterSetupForm.ShowSetupDialog(owner, masterObjTypeID, slaveCatalogID1, false);
  }

  public static DialogResult ShowSetupDialog(
    IWin32Window owner,
    long masterCatalogID,
    long slaveCatalogID,
    bool isFolderFilter = true)
  {
    using (ImbaseFilterSetupForm imbaseFilterSetupForm = new ImbaseFilterSetupForm(masterCatalogID, slaveCatalogID, isFolderFilter))
      return imbaseFilterSetupForm.ShowDialog(owner);
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ImbaseFilterSetupForm));
    this._tlp = new TableLayoutPanel();
    this._btnCancel = new Button();
    this._btnOk = new Button();
    this._tlp.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._tlp, "_tlp");
    this._tlp.Controls.Add((Control) this._btnCancel, 2, 0);
    this._tlp.Controls.Add((Control) this._btnOk, 1, 0);
    this._tlp.Name = "_tlp";
    this._btnCancel.DialogResult = DialogResult.Cancel;
    componentResourceManager.ApplyResources((object) this._btnCancel, "_btnCancel");
    this._btnCancel.Name = "_btnCancel";
    this._btnCancel.UseVisualStyleBackColor = true;
    this._btnCancel.Click += new EventHandler(this.On_btnCancel_Click);
    this._btnOk.DialogResult = DialogResult.OK;
    componentResourceManager.ApplyResources((object) this._btnOk, "_btnOk");
    this._btnOk.Name = "_btnOk";
    this._btnOk.UseVisualStyleBackColor = true;
    this._btnOk.Click += new EventHandler(this.On_btnOk_Click);
    this.AcceptButton = (IButtonControl) this._btnOk;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this._btnCancel;
    this.Controls.Add((Control) this._tlp);
    this.DoubleBuffered = true;
    this.Name = nameof (ImbaseFilterSetupForm);
    this.ShowInTaskbar = false;
    this._tlp.ResumeLayout(false);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
