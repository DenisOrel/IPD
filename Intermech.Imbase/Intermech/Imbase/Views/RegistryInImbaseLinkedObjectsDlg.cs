// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Views.RegistryInImbaseLinkedObjectsDlg
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Views;

public class RegistryInImbaseLinkedObjectsDlg : Form
{
  private IContainer components;
  private Panel _pnlBottom;
  private Button _btnOk;
  private Label _lbMessage;
  private DataGridView _dgv;
  private DataGridViewCheckBoxColumn _colCheck;
  private DataGridViewImageColumn _colObjPict;
  private DataGridViewTextBoxColumn _colObjID;
  private DataGridViewTextBoxColumn _colObjCaption;
  private DataGridViewImageColumn _colImbasePict;
  private DataGridViewTextBoxColumn _colImbaseID;
  private DataGridViewTextBoxColumn _colImbaseCaption;
  private Button _btnCancel;

  public List<long> CheckedIDs
  {
    get
    {
      List<long> longList = new List<long>(this._dgv.Rows.Count);
      foreach (DataGridViewRow row in (IEnumerable) this._dgv.Rows)
      {
        if (Convert.ToBoolean(row.Cells[this._colCheck.Name].Value))
          longList.Add(Convert.ToInt64(row.Cells[this._colObjID.Name].Value));
      }
      return longList.Count <= 0 ? (List<long>) null : longList;
    }
  }

  public RegistryInImbaseLinkedObjectsDlg(DataTable dt)
  {
    this.InitializeComponent();
    this.LoadData(dt);
  }

  protected override void OnLoad(EventArgs e)
  {
    base.OnLoad(e);
    FormStorage.LoadLayout((Control) this);
  }

  protected override void OnClosed(EventArgs e)
  {
    base.OnClosed(e);
    FormStorage.SaveLayout((Control) this);
  }

  private void LoadData(DataTable dtSource)
  {
    if (dtSource == null)
      return;
    ICategoryTypeIconService service = ServicesManager.GetService(typeof (ICategoryTypeIconService)) as ICategoryTypeIconService;
    string columnName1 = Convert.ToString(-7);
    string columnName2 = Convert.ToString(-2);
    string columnName3 = Convert.ToString(-50);
    string columnName4 = Convert.ToString(Intermech.Imbase.Consts.ImbaseObjectRefAttID);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (DataRow row1 in (InternalDataCollectionBase) dtSource.Rows)
      {
        DataGridViewRow row2 = this._dgv.Rows[this._dgv.Rows.Add()];
        row2.Cells[this._colObjPict.Name].Value = (object) service.ImageList.Images[service.IndexOf(4, Convert.ToInt32(row1[columnName1]))];
        row2.Cells[this._colObjID.Name].Value = (object) Convert.ToString(row1[columnName2]);
        row2.Cells[this._colObjCaption.Name].Value = (object) Convert.ToString(row1[columnName3]);
        long int64 = Convert.ToInt64(row1[columnName4]);
        QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(int64);
        if (!objectInfo.Empty)
        {
          row2.Cells[this._colImbasePict.Name].Value = (object) service.ImageList.Images[service.IndexOf(4, objectInfo.ObjectTypeID)];
          row2.Cells[this._colImbaseID.Name].Value = (object) Convert.ToString(int64);
          row2.Cells[this._colImbaseCaption.Name].Value = (object) objectInfo.Caption;
        }
      }
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (RegistryInImbaseLinkedObjectsDlg));
    this._pnlBottom = new Panel();
    this._btnCancel = new Button();
    this._btnOk = new Button();
    this._lbMessage = new Label();
    this._dgv = new DataGridView();
    this._colCheck = new DataGridViewCheckBoxColumn();
    this._colObjPict = new DataGridViewImageColumn();
    this._colObjID = new DataGridViewTextBoxColumn();
    this._colObjCaption = new DataGridViewTextBoxColumn();
    this._colImbasePict = new DataGridViewImageColumn();
    this._colImbaseID = new DataGridViewTextBoxColumn();
    this._colImbaseCaption = new DataGridViewTextBoxColumn();
    this._pnlBottom.SuspendLayout();
    ((ISupportInitialize) this._dgv).BeginInit();
    this.SuspendLayout();
    this._pnlBottom.Controls.Add((Control) this._btnCancel);
    this._pnlBottom.Controls.Add((Control) this._btnOk);
    componentResourceManager.ApplyResources((object) this._pnlBottom, "_pnlBottom");
    this._pnlBottom.Name = "_pnlBottom";
    componentResourceManager.ApplyResources((object) this._btnCancel, "_btnCancel");
    this._btnCancel.DialogResult = DialogResult.Cancel;
    this._btnCancel.Name = "_btnCancel";
    this._btnCancel.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._btnOk, "_btnOk");
    this._btnOk.DialogResult = DialogResult.OK;
    this._btnOk.Name = "_btnOk";
    this._btnOk.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._lbMessage, "_lbMessage");
    this._lbMessage.Name = "_lbMessage";
    this._dgv.AllowUserToAddRows = false;
    this._dgv.AllowUserToDeleteRows = false;
    this._dgv.AllowUserToResizeRows = false;
    this._dgv.BackgroundColor = SystemColors.Window;
    this._dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
    this._dgv.Columns.AddRange((DataGridViewColumn) this._colCheck, (DataGridViewColumn) this._colObjPict, (DataGridViewColumn) this._colObjID, (DataGridViewColumn) this._colObjCaption, (DataGridViewColumn) this._colImbasePict, (DataGridViewColumn) this._colImbaseID, (DataGridViewColumn) this._colImbaseCaption);
    componentResourceManager.ApplyResources((object) this._dgv, "_dgv");
    this._dgv.GridColor = SystemColors.Window;
    this._dgv.MultiSelect = false;
    this._dgv.Name = "_dgv";
    this._dgv.RowHeadersVisible = false;
    this._colCheck.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
    componentResourceManager.ApplyResources((object) this._colCheck, "_colCheck");
    this._colCheck.Name = "_colCheck";
    this._colObjPict.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
    componentResourceManager.ApplyResources((object) this._colObjPict, "_colObjPict");
    this._colObjPict.Name = "_colObjPict";
    this._colObjID.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
    componentResourceManager.ApplyResources((object) this._colObjID, "_colObjID");
    this._colObjID.Name = "_colObjID";
    this._colObjCaption.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
    componentResourceManager.ApplyResources((object) this._colObjCaption, "_colObjCaption");
    this._colObjCaption.Name = "_colObjCaption";
    this._colImbasePict.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
    componentResourceManager.ApplyResources((object) this._colImbasePict, "_colImbasePict");
    this._colImbasePict.Name = "_colImbasePict";
    this._colImbaseID.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
    componentResourceManager.ApplyResources((object) this._colImbaseID, "_colImbaseID");
    this._colImbaseID.Name = "_colImbaseID";
    this._colImbaseCaption.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
    componentResourceManager.ApplyResources((object) this._colImbaseCaption, "_colImbaseCaption");
    this._colImbaseCaption.Name = "_colImbaseCaption";
    this.AcceptButton = (IButtonControl) this._btnOk;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this._btnCancel;
    this.Controls.Add((Control) this._dgv);
    this.Controls.Add((Control) this._lbMessage);
    this.Controls.Add((Control) this._pnlBottom);
    this.DoubleBuffered = true;
    this.Name = nameof (RegistryInImbaseLinkedObjectsDlg);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this._pnlBottom.ResumeLayout(false);
    ((ISupportInitialize) this._dgv).EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
