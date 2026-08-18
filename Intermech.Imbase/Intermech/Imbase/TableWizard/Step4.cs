// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.TableWizard.Step4
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Client.Core;
using Intermech.Imbase.TableWizard.Interfaces;
using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.TableWizard;

[Serializable]
internal class Step4 : UserControl, IImbaseTableStep
{
  private ImbaseTableWizardForm _wizardForm;
  private Dictionary<System.Type, object> _context;
  private ImageList _imgList;
  private IContainer components;
  private TableLayoutPanel tableLayoutPanel1;
  private Label label2;
  private Label label3;
  private DataGridView dgvTblAttrs;
  private DataGridView dgvRefAttrs;
  private DataSet ds;
  private DataTable dtAttrs;
  private DataColumn colImage;
  private DataColumn colName;
  private DataColumn colID;
  private DataColumn colFilter;
  private DataColumn colVirtual;
  private BindingSource bsTblAttrs;
  private BindingSource bsRefAttrs;
  private Panel pnlRefName;
  private Label lbRefName;
  private TextBox txtRefName;
  private DataGridViewImageColumn dgvRefColImage;
  private DataGridViewTextBoxColumn dgvRefColName;
  private DataGridViewTextBoxColumn dgvRefColID;
  private DataGridViewCheckBoxColumn dgvRefColFilter;
  private DataGridViewCheckBoxColumn dgvRefColVirtual;
  private DataGridViewImageColumn dataGridViewImageColumn1;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
  private DataGridViewImageColumn dataGridViewImageColumn2;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
  private DataGridViewImageColumn dgvColImage;
  private DataGridViewTextBoxColumn dgvColName;
  private DataGridViewCheckBoxColumn dgvColFilter;
  private DataGridViewCheckBoxColumn dgvColVirtual;
  private DataGridViewTextBoxColumn dgvColID;
  private ImageList imgList;
  private Button btnAllLeft;
  private Button btnLeft;
  private Button btnRight;
  private Button btnAllRight;
  private DataGridViewImageColumn dataGridViewImageColumn3;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
  private DataGridViewImageColumn dataGridViewImageColumn4;

  public Step4()
  {
    this.InitializeComponent();
    this.Dock = DockStyle.Fill;
    this._imgList = Statics.IconSrv != null ? Statics.IconSrv.ImageList : (ImageList) null;
  }

  private void On_dgv_SelectionChanged(object sender, EventArgs e)
  {
    this.btnRight.Enabled = this.btnAllRight.Enabled = this.dgvTblAttrs.SelectedRows.Count > 0;
    this.btnLeft.Enabled = this.btnAllLeft.Enabled = this.dgvRefAttrs.SelectedRows.Count > 0;
  }

  public ImbaseTableWizardForm WizardForm
  {
    set
    {
      this._wizardForm = value;
      this._wizardForm._btnFinish.Enabled = true;
      this._wizardForm._btnNext.Enabled = true;
      this._wizardForm._btnPrev.Enabled = !this._wizardForm.DisableFirstStep;
    }
  }

  public Dictionary<System.Type, object> Context
  {
    get
    {
      Step4Params step4Params = new Step4Params(this.ds);
      this._wizardForm.ObjectName = this.txtRefName.Text;
      this._context[typeof (Step4)] = (object) step4Params;
      this.AddVirtualAttsToObject(this.dtAttrs.Select($"{this.colFilter.ColumnName}=true"));
      return this._context;
    }
    set
    {
      this._context = value;
      if (this._context.ContainsKey(typeof (Step4)))
      {
        this.ds = (this._context[typeof (Step4)] as Step4Params).DS;
        this.bsTblAttrs.DataSource = this.bsRefAttrs.DataSource = (object) this.ds;
        this.dtAttrs = this.ds.Tables[this.dtAttrs.TableName];
        this.txtRefName.Text = this._wizardForm.ObjectName;
      }
      else
      {
        if (!(this._context[typeof (Step1)] is Step1Params step1Params))
          return;
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          DataSet tables = TableLoadHelper.GetTables(sessionKeeper.Session, step1Params.TableID, true);
          if (tables == null)
            return;
          DataTable table = tables.Tables["IMS_ATTR_TYPES"];
          if (table == null)
            return;
          foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
          {
            IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(new Guid(Convert.ToString(row["F_ATTRIBUTE_GUID"])), false);
            if (attributeType != null)
            {
              int index = Statics.IconSrv.IndexOf(3, -1, (object) attributeType.AttributeType);
              bool flag = Convert.ToInt32(row["F_REQUIRED"]) != Convert.ToInt32((object) RequiredModes.AutoRequired);
              this.dtAttrs.Rows.Add(this._imgList.Images != null ? (object) this._imgList.Images[index] : (object) (Image) null, (object) attributeType.Name, (object) attributeType.AttributeID, (object) flag, (object) flag);
            }
          }
          IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(this._wizardForm.ObjectID, false);
          if (objectActualCopy != null)
          {
            objectActualCopy.Caption = this._wizardForm.ObjectName;
            IDBAttributeCollection attributes = objectActualCopy.Attributes;
            for (int AttrIndex = 0; AttrIndex < attributes.Count; ++AttrIndex)
            {
              IDBAttribute dbAttribute = attributes[AttrIndex];
              if (dbAttribute.AttributeID == Intermech.Imbase.Consts.ImbaseTableRefAttID)
                dbAttribute.Value = (object) step1Params.TableID;
              DataRow[] dataRowArray = this.dtAttrs.Select($"{this.colID.ColumnName}='{dbAttribute.AttributeID}'");
              if (dataRowArray.Length != 0)
                this.dtAttrs.Rows.Remove(dataRowArray[0]);
            }
          }
        }
        this.txtRefName.Text = this._wizardForm.ObjectName;
      }
      this.btnRight.Enabled = this.btnAllRight.Enabled = this.dgvTblAttrs.SelectedRows.Count > 0;
      this.btnLeft.Enabled = this.btnAllLeft.Enabled = this.dgvRefAttrs.SelectedRows.Count > 0;
    }
  }

  public System.Type NextStep => typeof (Step3);

  public System.Type PrevStep => typeof (Step1);

  private void AddVirtualAttsToObject(DataRow[] rows)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this._wizardForm.ObjectID);
      if (dbObject == null)
        return;
      dbObject.Caption = this._wizardForm.ObjectName;
      if (this.dtAttrs == null || this.dtAttrs.Rows.Count <= 0)
        return;
      IDBAttributeCollection attributes = dbObject.Attributes;
      for (int AttrIndex = 0; AttrIndex < attributes.Count; ++AttrIndex)
      {
        IDBAttribute dbAttribute = attributes[AttrIndex];
        IDBAttributeType4 attributeType = dbAttribute.AttributeType as IDBAttributeType4;
        if (this.dtAttrs.Select($"{this.colID.ColumnName}='{dbAttribute.AttributeID}'").Length != 0)
        {
          if (attributeType == null || attributeType.Required != RequiredModes.AutoRequired)
            dbAttribute.Delete(0L);
        }
        else if (attributeType == null)
          dbAttribute.Delete(0L);
      }
      foreach (DataRow row in rows)
        dbObject.Attributes.AddAttribute(Convert.ToInt32(row[this.colID.ColumnName]), false);
    }
  }

  private void OnButton_Click(object sender, EventArgs e)
  {
    switch (Convert.ToInt16((sender as Button).Tag))
    {
      case 0:
        foreach (DataRow dataRow in this.dtAttrs.Select($"{this.colFilter.ColumnName}=false"))
          dataRow[this.colFilter.ColumnName] = (object) true;
        this.btnAllRight.Enabled = this.btnRight.Enabled = false;
        this.btnAllLeft.Enabled = this.btnLeft.Enabled = true;
        break;
      case 1:
        if (this.dgvTblAttrs.SelectedRows.Count == 0)
          break;
        foreach (DataGridViewRow selectedRow in (BaseCollection) this.dgvTblAttrs.SelectedRows)
          this.dtAttrs.Select($"{this.colID.ColumnName}={selectedRow.Cells[this.dgvColID.Name].Value}")[0][this.colFilter.ColumnName] = (object) true;
        if (this.dgvTblAttrs.SelectedRows.Count == 0 && this.dgvTblAttrs.Rows.Count > 0)
          this.dgvTblAttrs.Rows[this.dgvTblAttrs.Rows.Count - 1].Selected = true;
        this.btnAllRight.Enabled = this.btnRight.Enabled = this.dgvTblAttrs.Rows.Count != 0;
        this.btnAllLeft.Enabled = this.btnLeft.Enabled = true;
        break;
      case 2:
        if (this.dgvRefAttrs.SelectedRows.Count == 0)
          break;
        foreach (DataGridViewRow selectedRow in (BaseCollection) this.dgvRefAttrs.SelectedRows)
          this.dtAttrs.Select($"{this.colID.ColumnName}={selectedRow.Cells[this.dgvRefColID.Name].Value}")[0][this.colFilter.ColumnName] = (object) false;
        if (this.dgvRefAttrs.SelectedRows.Count == 0 && this.dgvRefAttrs.Rows.Count > 0)
          this.dgvRefAttrs.Rows[this.dgvRefAttrs.Rows.Count - 1].Selected = true;
        this.btnAllLeft.Enabled = this.btnLeft.Enabled = this.dgvRefAttrs.Rows.Count != 0;
        this.btnAllRight.Enabled = this.btnRight.Enabled = true;
        break;
      case 3:
        foreach (DataRow dataRow in this.dtAttrs.Select($"{this.colFilter.ColumnName}=true"))
          dataRow[this.colFilter.ColumnName] = (object) false;
        this.btnAllLeft.Enabled = this.btnLeft.Enabled = false;
        this.btnAllRight.Enabled = this.btnRight.Enabled = true;
        break;
    }
  }

  private void OntxtRefName_TextChanged(object sender, EventArgs e)
  {
    this._wizardForm._btnFinish.Enabled = !string.IsNullOrEmpty(this.txtRefName.Text);
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Step4));
    DataGridViewCellStyle gridViewCellStyle1 = new DataGridViewCellStyle();
    DataGridViewCellStyle gridViewCellStyle2 = new DataGridViewCellStyle();
    DataGridViewCellStyle gridViewCellStyle3 = new DataGridViewCellStyle();
    DataGridViewCellStyle gridViewCellStyle4 = new DataGridViewCellStyle();
    DataGridViewCellStyle gridViewCellStyle5 = new DataGridViewCellStyle();
    DataGridViewCellStyle gridViewCellStyle6 = new DataGridViewCellStyle();
    this.tableLayoutPanel1 = new TableLayoutPanel();
    this.dgvTblAttrs = new DataGridView();
    this.dgvColFilter = new DataGridViewCheckBoxColumn();
    this.dgvColVirtual = new DataGridViewCheckBoxColumn();
    this.dgvColImage = new DataGridViewImageColumn();
    this.dgvColName = new DataGridViewTextBoxColumn();
    this.dgvColID = new DataGridViewTextBoxColumn();
    this.bsTblAttrs = new BindingSource(this.components);
    this.ds = new DataSet();
    this.dtAttrs = new DataTable();
    this.colImage = new DataColumn();
    this.colName = new DataColumn();
    this.colID = new DataColumn();
    this.colFilter = new DataColumn();
    this.colVirtual = new DataColumn();
    this.btnAllLeft = new Button();
    this.imgList = new ImageList(this.components);
    this.btnLeft = new Button();
    this.btnRight = new Button();
    this.btnAllRight = new Button();
    this.label2 = new Label();
    this.label3 = new Label();
    this.dgvRefAttrs = new DataGridView();
    this.dgvRefColFilter = new DataGridViewCheckBoxColumn();
    this.dgvRefColVirtual = new DataGridViewCheckBoxColumn();
    this.dgvRefColImage = new DataGridViewImageColumn();
    this.dgvRefColName = new DataGridViewTextBoxColumn();
    this.dgvRefColID = new DataGridViewTextBoxColumn();
    this.bsRefAttrs = new BindingSource(this.components);
    this.pnlRefName = new Panel();
    this.txtRefName = new TextBox();
    this.lbRefName = new Label();
    this.dataGridViewImageColumn1 = new DataGridViewImageColumn();
    this.dataGridViewImageColumn2 = new DataGridViewImageColumn();
    this.dataGridViewImageColumn3 = new DataGridViewImageColumn();
    this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
    this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
    this.dataGridViewImageColumn4 = new DataGridViewImageColumn();
    this.dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
    this.dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
    this.tableLayoutPanel1.SuspendLayout();
    ((ISupportInitialize) this.dgvTblAttrs).BeginInit();
    ((ISupportInitialize) this.bsTblAttrs).BeginInit();
    this.ds.BeginInit();
    this.dtAttrs.BeginInit();
    ((ISupportInitialize) this.dgvRefAttrs).BeginInit();
    ((ISupportInitialize) this.bsRefAttrs).BeginInit();
    this.pnlRefName.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.tableLayoutPanel1, "tableLayoutPanel1");
    this.tableLayoutPanel1.Controls.Add((Control) this.dgvTblAttrs, 0, 4);
    this.tableLayoutPanel1.Controls.Add((Control) this.btnAllLeft, 1, 8);
    this.tableLayoutPanel1.Controls.Add((Control) this.btnLeft, 1, 7);
    this.tableLayoutPanel1.Controls.Add((Control) this.btnRight, 1, 6);
    this.tableLayoutPanel1.Controls.Add((Control) this.btnAllRight, 1, 5);
    this.tableLayoutPanel1.Controls.Add((Control) this.label2, 0, 3);
    this.tableLayoutPanel1.Controls.Add((Control) this.label3, 2, 3);
    this.tableLayoutPanel1.Controls.Add((Control) this.dgvRefAttrs, 2, 4);
    this.tableLayoutPanel1.Controls.Add((Control) this.pnlRefName, 0, 1);
    this.tableLayoutPanel1.Name = "tableLayoutPanel1";
    this.dgvTblAttrs.AllowUserToAddRows = false;
    this.dgvTblAttrs.AllowUserToDeleteRows = false;
    this.dgvTblAttrs.AllowUserToResizeRows = false;
    this.dgvTblAttrs.AutoGenerateColumns = false;
    this.dgvTblAttrs.BackgroundColor = SystemColors.Window;
    this.dgvTblAttrs.BorderStyle = BorderStyle.None;
    this.dgvTblAttrs.CellBorderStyle = DataGridViewCellBorderStyle.None;
    this.dgvTblAttrs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
    this.dgvTblAttrs.Columns.AddRange((DataGridViewColumn) this.dgvColFilter, (DataGridViewColumn) this.dgvColVirtual, (DataGridViewColumn) this.dgvColImage, (DataGridViewColumn) this.dgvColName, (DataGridViewColumn) this.dgvColID);
    this.dgvTblAttrs.DataSource = (object) this.bsTblAttrs;
    componentResourceManager.ApplyResources((object) this.dgvTblAttrs, "dgvTblAttrs");
    this.dgvTblAttrs.Name = "dgvTblAttrs";
    this.dgvTblAttrs.RowHeadersVisible = false;
    this.tableLayoutPanel1.SetRowSpan((Control) this.dgvTblAttrs, 6);
    this.dgvTblAttrs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
    this.dgvTblAttrs.SelectionChanged += new EventHandler(this.On_dgv_SelectionChanged);
    this.dgvColFilter.DataPropertyName = "colFilter";
    componentResourceManager.ApplyResources((object) this.dgvColFilter, "dgvColFilter");
    this.dgvColFilter.Name = "dgvColFilter";
    this.dgvColFilter.ReadOnly = true;
    this.dgvColFilter.Resizable = DataGridViewTriState.False;
    this.dgvColVirtual.DataPropertyName = "colVirtual";
    componentResourceManager.ApplyResources((object) this.dgvColVirtual, "dgvColVirtual");
    this.dgvColVirtual.Name = "dgvColVirtual";
    this.dgvColVirtual.ReadOnly = true;
    this.dgvColVirtual.Resizable = DataGridViewTriState.False;
    this.dgvColImage.DataPropertyName = "colImage";
    gridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
    gridViewCellStyle1.NullValue = componentResourceManager.GetObject("dataGridViewCellStyle1.NullValue");
    gridViewCellStyle1.Padding = new Padding(3, 0, 0, 0);
    this.dgvColImage.DefaultCellStyle = gridViewCellStyle1;
    componentResourceManager.ApplyResources((object) this.dgvColImage, "dgvColImage");
    this.dgvColImage.Name = "dgvColImage";
    this.dgvColImage.ReadOnly = true;
    this.dgvColImage.Resizable = DataGridViewTriState.False;
    this.dgvColName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    this.dgvColName.DataPropertyName = "colName";
    componentResourceManager.ApplyResources((object) this.dgvColName, "dgvColName");
    this.dgvColName.Name = "dgvColName";
    this.dgvColName.ReadOnly = true;
    this.dgvColID.DataPropertyName = "colID";
    componentResourceManager.ApplyResources((object) this.dgvColID, "dgvColID");
    this.dgvColID.Name = "dgvColID";
    this.dgvColID.ReadOnly = true;
    this.dgvColID.Resizable = DataGridViewTriState.False;
    this.dgvColID.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.bsTblAttrs.DataMember = "dtAttrs";
    this.bsTblAttrs.DataSource = (object) this.ds;
    this.bsTblAttrs.Filter = "colFilter=false";
    this.ds.DataSetName = "NewDataSet";
    this.ds.Tables.AddRange(new DataTable[1]{ this.dtAttrs });
    this.dtAttrs.Columns.AddRange(new DataColumn[5]
    {
      this.colImage,
      this.colName,
      this.colID,
      this.colFilter,
      this.colVirtual
    });
    this.dtAttrs.TableName = "dtAttrs";
    this.colImage.Caption = "Image";
    this.colImage.ColumnName = "colImage";
    this.colImage.DataType = typeof (object);
    this.colName.Caption = "Наименование";
    this.colName.ColumnName = "colName";
    this.colID.AllowDBNull = false;
    this.colID.Caption = "ID";
    this.colID.ColumnName = "colID";
    this.colID.DataType = typeof (long);
    this.colID.DefaultValue = (object) -1L;
    this.colFilter.AllowDBNull = false;
    this.colFilter.Caption = "Filter";
    this.colFilter.ColumnName = "colFilter";
    this.colFilter.DataType = typeof (bool);
    this.colFilter.DefaultValue = (object) false;
    this.colVirtual.AllowDBNull = false;
    this.colVirtual.Caption = "Virtual";
    this.colVirtual.ColumnName = "colVirtual";
    this.colVirtual.DataType = typeof (bool);
    this.colVirtual.DefaultValue = (object) false;
    componentResourceManager.ApplyResources((object) this.btnAllLeft, "btnAllLeft");
    this.btnAllLeft.ImageList = this.imgList;
    this.btnAllLeft.Name = "btnAllLeft";
    this.btnAllLeft.Tag = (object) "3";
    this.btnAllLeft.UseVisualStyleBackColor = true;
    this.btnAllLeft.Click += new EventHandler(this.OnButton_Click);
    this.imgList.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imgList.ImageStream");
    this.imgList.TransparentColor = Color.Transparent;
    this.imgList.Images.SetKeyName(0, "RightAll.ico");
    this.imgList.Images.SetKeyName(1, "Right.ico");
    this.imgList.Images.SetKeyName(2, "Left.ico");
    this.imgList.Images.SetKeyName(3, "LeftAll.ico");
    componentResourceManager.ApplyResources((object) this.btnLeft, "btnLeft");
    this.btnLeft.ImageList = this.imgList;
    this.btnLeft.Name = "btnLeft";
    this.btnLeft.Tag = (object) "2";
    this.btnLeft.UseVisualStyleBackColor = true;
    this.btnLeft.Click += new EventHandler(this.OnButton_Click);
    componentResourceManager.ApplyResources((object) this.btnRight, "btnRight");
    this.btnRight.ImageList = this.imgList;
    this.btnRight.Name = "btnRight";
    this.btnRight.Tag = (object) "1";
    this.btnRight.UseVisualStyleBackColor = true;
    this.btnRight.Click += new EventHandler(this.OnButton_Click);
    componentResourceManager.ApplyResources((object) this.btnAllRight, "btnAllRight");
    this.btnAllRight.ImageList = this.imgList;
    this.btnAllRight.Name = "btnAllRight";
    this.btnAllRight.Tag = (object) "0";
    this.btnAllRight.UseVisualStyleBackColor = true;
    this.btnAllRight.Click += new EventHandler(this.OnButton_Click);
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    componentResourceManager.ApplyResources((object) this.label3, "label3");
    this.label3.Name = "label3";
    this.dgvRefAttrs.AllowUserToAddRows = false;
    this.dgvRefAttrs.AllowUserToDeleteRows = false;
    this.dgvRefAttrs.AllowUserToResizeRows = false;
    this.dgvRefAttrs.AutoGenerateColumns = false;
    this.dgvRefAttrs.BackgroundColor = SystemColors.Window;
    this.dgvRefAttrs.BorderStyle = BorderStyle.None;
    this.dgvRefAttrs.CellBorderStyle = DataGridViewCellBorderStyle.None;
    this.dgvRefAttrs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
    this.dgvRefAttrs.Columns.AddRange((DataGridViewColumn) this.dgvRefColFilter, (DataGridViewColumn) this.dgvRefColVirtual, (DataGridViewColumn) this.dgvRefColImage, (DataGridViewColumn) this.dgvRefColName, (DataGridViewColumn) this.dgvRefColID);
    this.dgvRefAttrs.DataSource = (object) this.bsRefAttrs;
    componentResourceManager.ApplyResources((object) this.dgvRefAttrs, "dgvRefAttrs");
    this.dgvRefAttrs.Name = "dgvRefAttrs";
    this.dgvRefAttrs.RowHeadersVisible = false;
    this.tableLayoutPanel1.SetRowSpan((Control) this.dgvRefAttrs, 6);
    this.dgvRefAttrs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
    this.dgvRefAttrs.SelectionChanged += new EventHandler(this.On_dgv_SelectionChanged);
    this.dgvRefColFilter.DataPropertyName = "colFilter";
    componentResourceManager.ApplyResources((object) this.dgvRefColFilter, "dgvRefColFilter");
    this.dgvRefColFilter.Name = "dgvRefColFilter";
    this.dgvRefColFilter.ReadOnly = true;
    this.dgvRefColFilter.Resizable = DataGridViewTriState.False;
    this.dgvRefColVirtual.DataPropertyName = "colVirtual";
    componentResourceManager.ApplyResources((object) this.dgvRefColVirtual, "dgvRefColVirtual");
    this.dgvRefColVirtual.Name = "dgvRefColVirtual";
    this.dgvRefColVirtual.ReadOnly = true;
    this.dgvRefColVirtual.Resizable = DataGridViewTriState.False;
    this.dgvRefColImage.DataPropertyName = "colImage";
    gridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
    gridViewCellStyle2.NullValue = componentResourceManager.GetObject("dataGridViewCellStyle2.NullValue");
    gridViewCellStyle2.Padding = new Padding(3, 0, 0, 0);
    this.dgvRefColImage.DefaultCellStyle = gridViewCellStyle2;
    componentResourceManager.ApplyResources((object) this.dgvRefColImage, "dgvRefColImage");
    this.dgvRefColImage.Name = "dgvRefColImage";
    this.dgvRefColImage.ReadOnly = true;
    this.dgvRefColImage.Resizable = DataGridViewTriState.False;
    this.dgvRefColName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    this.dgvRefColName.DataPropertyName = "colName";
    componentResourceManager.ApplyResources((object) this.dgvRefColName, "dgvRefColName");
    this.dgvRefColName.Name = "dgvRefColName";
    this.dgvRefColName.ReadOnly = true;
    this.dgvRefColID.DataPropertyName = "colID";
    componentResourceManager.ApplyResources((object) this.dgvRefColID, "dgvRefColID");
    this.dgvRefColID.Name = "dgvRefColID";
    this.dgvRefColID.ReadOnly = true;
    this.dgvRefColID.Resizable = DataGridViewTriState.False;
    this.dgvRefColID.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.bsRefAttrs.DataMember = "dtAttrs";
    this.bsRefAttrs.DataSource = (object) this.ds;
    this.bsRefAttrs.Filter = "colFilter=true";
    this.tableLayoutPanel1.SetColumnSpan((Control) this.pnlRefName, 3);
    this.pnlRefName.Controls.Add((Control) this.txtRefName);
    this.pnlRefName.Controls.Add((Control) this.lbRefName);
    componentResourceManager.ApplyResources((object) this.pnlRefName, "pnlRefName");
    this.pnlRefName.Name = "pnlRefName";
    componentResourceManager.ApplyResources((object) this.txtRefName, "txtRefName");
    this.txtRefName.Name = "txtRefName";
    this.txtRefName.TextChanged += new EventHandler(this.OntxtRefName_TextChanged);
    componentResourceManager.ApplyResources((object) this.lbRefName, "lbRefName");
    this.lbRefName.Name = "lbRefName";
    this.dataGridViewImageColumn1.DataPropertyName = "colImage";
    gridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
    gridViewCellStyle3.NullValue = componentResourceManager.GetObject("dataGridViewCellStyle3.NullValue");
    gridViewCellStyle3.Padding = new Padding(3, 0, 0, 0);
    this.dataGridViewImageColumn1.DefaultCellStyle = gridViewCellStyle3;
    componentResourceManager.ApplyResources((object) this.dataGridViewImageColumn1, "dataGridViewImageColumn1");
    this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
    this.dataGridViewImageColumn1.ReadOnly = true;
    this.dataGridViewImageColumn1.Resizable = DataGridViewTriState.False;
    this.dataGridViewImageColumn2.DataPropertyName = "colImage";
    gridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
    gridViewCellStyle4.NullValue = componentResourceManager.GetObject("dataGridViewCellStyle4.NullValue");
    gridViewCellStyle4.Padding = new Padding(3, 0, 0, 0);
    this.dataGridViewImageColumn2.DefaultCellStyle = gridViewCellStyle4;
    componentResourceManager.ApplyResources((object) this.dataGridViewImageColumn2, "dataGridViewImageColumn2");
    this.dataGridViewImageColumn2.Name = "dataGridViewImageColumn2";
    this.dataGridViewImageColumn2.ReadOnly = true;
    this.dataGridViewImageColumn2.Resizable = DataGridViewTriState.False;
    this.dataGridViewImageColumn3.DataPropertyName = "colImage";
    gridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter;
    gridViewCellStyle5.NullValue = componentResourceManager.GetObject("dataGridViewCellStyle5.NullValue");
    gridViewCellStyle5.Padding = new Padding(3, 0, 0, 0);
    this.dataGridViewImageColumn3.DefaultCellStyle = gridViewCellStyle5;
    componentResourceManager.ApplyResources((object) this.dataGridViewImageColumn3, "dataGridViewImageColumn3");
    this.dataGridViewImageColumn3.Name = "dataGridViewImageColumn3";
    this.dataGridViewImageColumn3.ReadOnly = true;
    this.dataGridViewImageColumn3.Resizable = DataGridViewTriState.False;
    this.dataGridViewTextBoxColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    this.dataGridViewTextBoxColumn1.DataPropertyName = "colName";
    componentResourceManager.ApplyResources((object) this.dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
    this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
    this.dataGridViewTextBoxColumn1.ReadOnly = true;
    this.dataGridViewTextBoxColumn2.DataPropertyName = "colID";
    componentResourceManager.ApplyResources((object) this.dataGridViewTextBoxColumn2, "dataGridViewTextBoxColumn2");
    this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
    this.dataGridViewTextBoxColumn2.ReadOnly = true;
    this.dataGridViewTextBoxColumn2.Resizable = DataGridViewTriState.False;
    this.dataGridViewTextBoxColumn2.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.dataGridViewImageColumn4.DataPropertyName = "colImage";
    gridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter;
    gridViewCellStyle6.NullValue = componentResourceManager.GetObject("dataGridViewCellStyle6.NullValue");
    gridViewCellStyle6.Padding = new Padding(3, 0, 0, 0);
    this.dataGridViewImageColumn4.DefaultCellStyle = gridViewCellStyle6;
    componentResourceManager.ApplyResources((object) this.dataGridViewImageColumn4, "dataGridViewImageColumn4");
    this.dataGridViewImageColumn4.Name = "dataGridViewImageColumn4";
    this.dataGridViewImageColumn4.ReadOnly = true;
    this.dataGridViewImageColumn4.Resizable = DataGridViewTriState.False;
    this.dataGridViewTextBoxColumn3.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    this.dataGridViewTextBoxColumn3.DataPropertyName = "colName";
    componentResourceManager.ApplyResources((object) this.dataGridViewTextBoxColumn3, "dataGridViewTextBoxColumn3");
    this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
    this.dataGridViewTextBoxColumn3.ReadOnly = true;
    this.dataGridViewTextBoxColumn4.DataPropertyName = "colID";
    componentResourceManager.ApplyResources((object) this.dataGridViewTextBoxColumn4, "dataGridViewTextBoxColumn4");
    this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
    this.dataGridViewTextBoxColumn4.ReadOnly = true;
    this.dataGridViewTextBoxColumn4.Resizable = DataGridViewTriState.False;
    this.dataGridViewTextBoxColumn4.SortMode = DataGridViewColumnSortMode.NotSortable;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.tableLayoutPanel1);
    this.Name = nameof (Step4);
    this.tableLayoutPanel1.ResumeLayout(false);
    this.tableLayoutPanel1.PerformLayout();
    ((ISupportInitialize) this.dgvTblAttrs).EndInit();
    ((ISupportInitialize) this.bsTblAttrs).EndInit();
    this.ds.EndInit();
    this.dtAttrs.EndInit();
    ((ISupportInitialize) this.dgvRefAttrs).EndInit();
    ((ISupportInitialize) this.bsRefAttrs).EndInit();
    this.pnlRefName.ResumeLayout(false);
    this.pnlRefName.PerformLayout();
    this.ResumeLayout(false);
  }
}
