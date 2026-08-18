// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Selection.ImbaseFolderFilterSetupCtrl
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Imbase.Controls;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Selection;

public class ImbaseFolderFilterSetupCtrl : UserControl
{
  private bool _isAdmin;
  private readonly DataTable _catalogStructTable = new DataTable();
  private readonly long _masterCatalogID;
  private readonly long _slaveCatalogID;
  private IContainer components;
  private Panel _pnlTop;
  private GroupBox _gbCatalog;
  private ComboBox _cmbCatalogSlave;
  private ComboBox _cmbCatalogMaster;
  private Label _lbMaster;
  private OwnerGuidTune _ownGuidTune;
  private FolderFilterTune _folderFilterTune;

  private void InitializeControlData()
  {
    if (this.DesignMode)
      return;
    this._isAdmin = (ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole).IsAdmin;
    this._catalogStructTable.Columns.AddRange(new DataColumn[2]
    {
      new DataColumn("colID", typeof (long)),
      new DataColumn("colCaption", typeof (string))
    });
    this._folderFilterTune.ReadOnly = !this._isAdmin;
    this._cmbCatalogMaster.BeginUpdate();
    try
    {
      this._cmbCatalogMaster.DisplayMember = "colCaption";
      this._cmbCatalogMaster.ValueMember = "colID";
    }
    finally
    {
      this._cmbCatalogMaster.EndUpdate();
    }
    this._cmbCatalogSlave.BeginUpdate();
    try
    {
      this._cmbCatalogSlave.DisplayMember = "colCaption";
      this._cmbCatalogSlave.ValueMember = "colID";
    }
    finally
    {
      this._cmbCatalogSlave.EndUpdate();
    }
  }

  public long MasterCatalogID
  {
    get
    {
      long result;
      return !long.TryParse(Convert.ToString(this._cmbCatalogMaster.SelectedValue), out result) ? this._masterCatalogID : result;
    }
    set
    {
      if (this.MasterCatalogID == value || value == 0L)
        return;
      this._cmbCatalogMaster.SelectedValue = (object) value;
    }
  }

  public long SlaveCatalogID
  {
    get
    {
      long result;
      return !long.TryParse(Convert.ToString(this._cmbCatalogSlave.SelectedValue), out result) ? this._slaveCatalogID : result;
    }
    set
    {
      if (this.SlaveCatalogID == value || value == 0L)
        return;
      this._cmbCatalogSlave.SelectedValue = (object) value;
    }
  }

  public FolderFilterTune FilterTune => this._folderFilterTune;

  public ImbaseFolderFilterSetupCtrl(long masterCatalogId, long slaveCatalogId)
  {
    this._masterCatalogID = masterCatalogId;
    this._slaveCatalogID = slaveCatalogId;
    this.InitializeComponent();
    this.InitializeControlData();
  }

  protected override void OnHandleCreated(EventArgs e) => base.OnHandleCreated(e);

  private void On_cmbCatalogMaster_SelectedIndexChanged(object sender, EventArgs e)
  {
    long result;
    if (this._cmbCatalogMaster.Tag != null || !long.TryParse(Convert.ToString(this._cmbCatalogMaster.SelectedValue), out result) || result == 0L)
      return;
    this._folderFilterTune.MasterCatalog = result;
  }

  private void On_cmbCatalogSlave_SelectedIndexChanged(object sender, EventArgs e)
  {
    long result;
    if (this._cmbCatalogSlave.Tag != null || !long.TryParse(Convert.ToString(this._cmbCatalogSlave.SelectedValue), out result) || result == 0L)
      return;
    this._folderFilterTune.SlaveCatalog = result;
  }

  private void On_ownGuidTune_OwnerChanged(object sender, EventArgs e)
  {
    if (!this._isAdmin)
      this._folderFilterTune.ReadOnly = this._ownGuidTune.OwnerGuid == null || this._ownGuidTune.OwnerType != OwnerGuidTune.OwnerFilterType.User;
    this._folderFilterTune.OwnerGuid = this._ownGuidTune.OwnerGuid;
  }

  private void LoadCatalogList()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DataTable dataTable1 = sessionKeeper.Session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseCatalogTypeGUID).Select(new DBRecordSetParams(new ConditionStructure[0], new ColumnDescriptor[2]
      {
        new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
        new ColumnDescriptor((object) -50, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.ASC, 0)
      }));
      long masterCatalogId = this.MasterCatalogID;
      long slaveCatalogId = this.SlaveCatalogID;
      this._cmbCatalogMaster.BeginUpdate();
      this._cmbCatalogSlave.BeginUpdate();
      try
      {
        DataTable dataTable2 = this._catalogStructTable.Clone();
        if (dataTable1 != null && dataTable1.Rows.Count > 0)
        {
          foreach (DataRow row in (InternalDataCollectionBase) dataTable1.Rows)
          {
            long result;
            if (long.TryParse(Convert.ToString(row["F_OBJECT_ID"]), out result) && result != 0L)
              dataTable2.Rows.Add((object) result, (object) Convert.ToString(row["CAPTION"]));
          }
        }
        this._cmbCatalogMaster.Tag = (object) true;
        this._cmbCatalogSlave.Tag = (object) true;
        this._cmbCatalogMaster.DataSource = (object) dataTable2;
        this._cmbCatalogSlave.DataSource = (object) dataTable2.Copy();
      }
      finally
      {
        this._cmbCatalogMaster.EndUpdate();
        this._cmbCatalogSlave.EndUpdate();
        this._cmbCatalogMaster.Tag = (object) null;
        this._cmbCatalogSlave.Tag = (object) null;
        this.MasterCatalogID = masterCatalogId;
        this.SlaveCatalogID = slaveCatalogId;
      }
    }
  }

  public void LoadData(bool forceMode = false)
  {
    this.LoadCatalogList();
    this.FilterTune.LoadFilter(forceMode);
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this._pnlTop = new Panel();
    this._gbCatalog = new GroupBox();
    this._cmbCatalogSlave = new ComboBox();
    this._cmbCatalogMaster = new ComboBox();
    this._lbMaster = new Label();
    this._ownGuidTune = new OwnerGuidTune();
    this._folderFilterTune = new FolderFilterTune();
    this._pnlTop.SuspendLayout();
    this._gbCatalog.SuspendLayout();
    this.SuspendLayout();
    this._pnlTop.Controls.Add((Control) this._gbCatalog);
    this._pnlTop.Dock = DockStyle.Top;
    this._pnlTop.Location = new Point(0, 0);
    this._pnlTop.Name = "_pnlTop";
    this._pnlTop.Size = new Size(492, 52);
    this._pnlTop.TabIndex = 3;
    this._gbCatalog.Controls.Add((Control) this._cmbCatalogSlave);
    this._gbCatalog.Controls.Add((Control) this._cmbCatalogMaster);
    this._gbCatalog.Controls.Add((Control) this._lbMaster);
    this._gbCatalog.Dock = DockStyle.Fill;
    this._gbCatalog.Location = new Point(0, 0);
    this._gbCatalog.Name = "_gbCatalog";
    this._gbCatalog.Size = new Size(492, 52);
    this._gbCatalog.TabIndex = 2;
    this._gbCatalog.TabStop = false;
    this._gbCatalog.Text = "Каталоги/справочники Imbase";
    this._cmbCatalogSlave.DropDownStyle = ComboBoxStyle.DropDownList;
    this._cmbCatalogSlave.FormattingEnabled = true;
    this._cmbCatalogSlave.Location = new Point(318, 21);
    this._cmbCatalogSlave.Name = "_cmbCatalogSlave";
    this._cmbCatalogSlave.Size = new Size(160 /*0xA0*/, 21);
    this._cmbCatalogSlave.TabIndex = 3;
    this._cmbCatalogSlave.SelectedIndexChanged += new EventHandler(this.On_cmbCatalogSlave_SelectedIndexChanged);
    this._cmbCatalogMaster.DropDownStyle = ComboBoxStyle.DropDownList;
    this._cmbCatalogMaster.FormattingEnabled = true;
    this._cmbCatalogMaster.Location = new Point(152, 21);
    this._cmbCatalogMaster.Name = "_cmbCatalogMaster";
    this._cmbCatalogMaster.Size = new Size(160 /*0xA0*/, 21);
    this._cmbCatalogMaster.TabIndex = 2;
    this._cmbCatalogMaster.SelectedIndexChanged += new EventHandler(this.On_cmbCatalogMaster_SelectedIndexChanged);
    this._lbMaster.AutoSize = true;
    this._lbMaster.ImeMode = ImeMode.NoControl;
    this._lbMaster.Location = new Point(12, 24);
    this._lbMaster.Name = "_lbMaster";
    this._lbMaster.Size = new Size(140, 13);
    this._lbMaster.TabIndex = 0;
    this._lbMaster.Text = "Основной / Подчиненный:";
    this._ownGuidTune.Caption = "Тип фильтра";
    this._ownGuidTune.Dock = DockStyle.Bottom;
    this._ownGuidTune.Location = new Point(0, 347);
    this._ownGuidTune.Name = "_ownGuidTune";
    this._ownGuidTune.Size = new Size(492, 94);
    this._ownGuidTune.TabIndex = 4;
    this._ownGuidTune.OwnerChanged += new EventHandler(this.On_ownGuidTune_OwnerChanged);
    this._folderFilterTune.Dirty = false;
    this._folderFilterTune.Dock = DockStyle.Fill;
    this._folderFilterTune.Location = new Point(0, 52);
    this._folderFilterTune.MasterCatalog = -1L;
    this._folderFilterTune.Name = "_folderFilterTune";
    this._folderFilterTune.OwnerGuid = "";
    this._folderFilterTune.Padding = new Padding(0, 3, 0, 0);
    this._folderFilterTune.ReadOnly = false;
    this._folderFilterTune.Size = new Size(492, 295);
    this._folderFilterTune.SlaveCatalog = -1L;
    this._folderFilterTune.TabIndex = 5;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._folderFilterTune);
    this.Controls.Add((Control) this._ownGuidTune);
    this.Controls.Add((Control) this._pnlTop);
    this.DoubleBuffered = true;
    this.Name = "ImbaseFolderFilterSetupCtrl_New";
    this.Size = new Size(492, 441);
    this._pnlTop.ResumeLayout(false);
    this._gbCatalog.ResumeLayout(false);
    this._gbCatalog.PerformLayout();
    this.ResumeLayout(false);
  }
}
