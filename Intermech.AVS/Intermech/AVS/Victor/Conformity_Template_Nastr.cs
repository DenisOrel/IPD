// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Victor.Conformity_Template_Nastr
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS.Victor;

public class Conformity_Template_Nastr : Form
{
  private bool isModified;
  private bool isCreate = true;
  public List<One_ImsObjectType_With_One_Ved_Nastr> _list_Ved_Arbeit_ImsObjectType_With_One_Ved_Nastr;
  public List<One_ImsObjectType_With_One_Ved_Nastr> _list_Tabl_Arbeit_ImsObjectType_With_One_Ved_Nastr;
  public List<Vedomost_VB_Static.One_Conformity_Template_Nastr> list_Conformity_Template_Nastr_Ved_Curr;
  public List<Vedomost_VB_Static.One_Conformity_Template_Nastr> list_Conformity_Template_Nastr_Tabl_Curr;
  private bool isAdmin;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  internal Button bCancel;
  private Button buttonSave1;
  internal Button bOK;
  private TabControl tabControl1;
  private TabPage tabPage_Ved;
  private GroupBox groupBox_Ved;
  private DataGridView dataGridView_Ved;
  private TabPage tabPage_Tabl;
  private GroupBox groupBox_Tabl;
  private DataGridView dataGridView_Tabl;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
  private DataGridViewTextBoxColumn ColumnSystem;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
  private Button button_Analiz;
  private TabControl tabControl_Analiz;
  private TabPage tabPage_Confirmity;
  private GroupBox groupBox_Conformity;
  private DataGridView dataGridView_Conformity;
  private TabPage tabPage_Arbeit;
  private GroupBox groupBox_Arbeit;
  private DataGridView dataGridView_Arbeit;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
  private DataGridViewTextBoxColumn Column1;
  private DataGridViewTextBoxColumn Column2;
  private DataGridViewTextBoxColumn Column3;
  private DataGridViewTextBoxColumn Column4;
  private DataGridViewTextBoxColumn Column5;
  private Button buttonDefault;

  public Conformity_Template_Nastr() => this.InitializeComponent();

  private void Conformity_Template_Nastr_Load(object sender, EventArgs e)
  {
    this.Draw_All();
    this.isCreate = false;
    this.dataGridView_Ved.Select();
    if (Vedomost_VB_Static.isComputerName_Victor || Vedomost_VB_Static.isHozain)
      this.button_Analiz.Visible = true;
    else
      this.button_Analiz.Visible = false;
  }

  private void Draw_All()
  {
    this.Draw_Buttons();
    this.Draw_Page();
    if (!Vedomost_VB_Static.isComputerName_Victor && !Vedomost_VB_Static.isHozain)
      return;
    this.tabControl_Analiz.Location = new Point(2, 3);
    this.Draw_dataGridView_Conformity();
  }

  private void Draw_Buttons()
  {
  }

  private void Draw_Page()
  {
    this.Draw_dataGridView_VedTabl(this.dataGridView_Ved, this._list_Ved_Arbeit_ImsObjectType_With_One_Ved_Nastr, this.list_Conformity_Template_Nastr_Ved_Curr);
    this.Draw_dataGridView_VedTabl(this.dataGridView_Tabl, this._list_Tabl_Arbeit_ImsObjectType_With_One_Ved_Nastr, this.list_Conformity_Template_Nastr_Tabl_Curr);
  }

  private void Draw_dataGridView_VedTabl(
    DataGridView dataGridView_VedTabl,
    List<One_ImsObjectType_With_One_Ved_Nastr> list_Arbeit_ImsObjectType_With_One_Ved_Nastr,
    List<Vedomost_VB_Static.One_Conformity_Template_Nastr> list_Conformity_Template_Nastr)
  {
    dataGridView_VedTabl.Rows.Clear();
    if (list_Conformity_Template_Nastr == null || list_Conformity_Template_Nastr.Count <= 0)
      return;
    for (int index = 0; index < list_Conformity_Template_Nastr.Count; ++index)
    {
      One_ImsObjectType_With_One_Ved_Nastr typeWithOneVedNastr = list_Arbeit_ImsObjectType_With_One_Ved_Nastr[index];
      Vedomost_VB_Static.One_Conformity_Template_Nastr conformityTemplateNastr = list_Conformity_Template_Nastr[index];
      DataGridViewRow dataGridViewRow = (DataGridViewRow) dataGridView_VedTabl.Rows[0].Clone();
      dataGridViewRow.Cells[1].Value = (object) typeWithOneVedNastr.imsObjectType.ObjectTypeName;
      if (typeWithOneVedNastr.one_Ved_Nastr != null)
      {
        Guid templateObjectGuid = typeWithOneVedNastr.one_Ved_Nastr._vedomostTemplateObjectGuid;
        dataGridViewRow.Cells[0].Value = (object) Vedomost_VB_Static.Get_NameTypeVed_By_TemplateGuid(list_Conformity_Template_Nastr, templateObjectGuid);
      }
      if (conformityTemplateNastr._guid_Template != Guid.Empty && conformityTemplateNastr._isSecondTemplate)
        dataGridViewRow.Cells[2].Value = (object) "2";
      dataGridView_VedTabl.Rows.Add(dataGridViewRow);
    }
    if (dataGridView_VedTabl.Rows.Count <= 1)
      return;
    dataGridView_VedTabl.Rows[0].Selected = true;
    dataGridView_VedTabl.RowsDefaultCellStyle.SelectionForeColor = Color.White;
  }

  private void buttonSave1_Click(object sender, EventArgs e)
  {
    switch (this.tabControl1.SelectedTab.Name)
    {
      case "tabPage_Ved":
        Vedomost_VB_Static.Write_Conformity_ToBase(this.list_Conformity_Template_Nastr_Ved_Curr, Vedomost_VB_Static.objectID_CONFORMITY_VED, "VED");
        break;
      case "tabPage_Tabl":
        Vedomost_VB_Static.Write_Conformity_ToBase(this.list_Conformity_Template_Nastr_Tabl_Curr, Vedomost_VB_Static.objectID_CONFORMITY_TABL, "TABL");
        break;
    }
  }

  private void buttonDefault_Click(object sender, EventArgs e)
  {
    switch (this.tabControl1.SelectedTab.Name)
    {
      case "tabPage_Ved":
        Vedomost_VB_Static.List_Conformity_Template_Nastr_Filled(this._list_Ved_Arbeit_ImsObjectType_With_One_Ved_Nastr, this.list_Conformity_Template_Nastr_Ved_Curr, Vedomost_VB.TypeDoc.Ved);
        this.Draw_dataGridView_VedTabl(this.dataGridView_Ved, this._list_Ved_Arbeit_ImsObjectType_With_One_Ved_Nastr, this.list_Conformity_Template_Nastr_Ved_Curr);
        break;
      case "tabPage_Tabl":
        Vedomost_VB_Static.List_Conformity_Template_Nastr_Filled(this._list_Tabl_Arbeit_ImsObjectType_With_One_Ved_Nastr, this.list_Conformity_Template_Nastr_Tabl_Curr, Vedomost_VB.TypeDoc.Tabl);
        this.Draw_dataGridView_VedTabl(this.dataGridView_Tabl, this._list_Tabl_Arbeit_ImsObjectType_With_One_Ved_Nastr, this.list_Conformity_Template_Nastr_Tabl_Curr);
        break;
    }
  }

  private void button_Analiz_Click(object sender, EventArgs e)
  {
    this.Location = new Point(5, 5);
    this.Height = 900;
    this.Width = 1900;
    if (this.isAdmin)
    {
      this.tabControl_Analiz.Visible = false;
      this.tabControl1.Visible = true;
      this.isAdmin = false;
    }
    else
    {
      this.tabControl_Analiz.Visible = true;
      this.tabControl1.Visible = false;
      this.isAdmin = true;
    }
  }

  private void Draw_dataGridView_Conformity()
  {
    this.dataGridView_Conformity.Rows.Clear();
    if (this.list_Conformity_Template_Nastr_Ved_Curr != null && this.list_Conformity_Template_Nastr_Ved_Curr.Count > 0)
    {
      for (int index = 0; index < this.list_Conformity_Template_Nastr_Ved_Curr.Count; ++index)
      {
        Vedomost_VB_Static.One_Conformity_Template_Nastr conformityTemplateNastr = this.list_Conformity_Template_Nastr_Ved_Curr[index];
        DataGridViewRow dataGridViewRow = (DataGridViewRow) this.dataGridView_Conformity.Rows[0].Clone();
        dataGridViewRow.Cells[0].Value = (object) conformityTemplateNastr._name_Ved;
        dataGridViewRow.Cells[1].Value = (object) conformityTemplateNastr._guid_TypeVed;
        dataGridViewRow.Cells[2].Value = (object) conformityTemplateNastr._name_Template;
        dataGridViewRow.Cells[3].Value = (object) conformityTemplateNastr._guid_Template;
        dataGridViewRow.Cells[4].Value = (object) conformityTemplateNastr._name_One_Ved_Nastr;
        dataGridViewRow.Cells[5].Value = (object) conformityTemplateNastr._guid_One_Ved_Nastr;
        dataGridViewRow.Cells[6].Value = (object) conformityTemplateNastr._typeDoc.ToString();
        if (conformityTemplateNastr._one_Conformity_Template_Nastr_Parent != null)
          dataGridViewRow.Cells[7].Value = (object) conformityTemplateNastr._one_Conformity_Template_Nastr_Parent._name_Ved;
        this.dataGridView_Conformity.Rows.Add(dataGridViewRow);
      }
    }
    if (this.list_Conformity_Template_Nastr_Tabl_Curr == null || this.list_Conformity_Template_Nastr_Tabl_Curr.Count <= 0)
      return;
    for (int index = 0; index < this.list_Conformity_Template_Nastr_Tabl_Curr.Count; ++index)
    {
      Vedomost_VB_Static.One_Conformity_Template_Nastr conformityTemplateNastr = this.list_Conformity_Template_Nastr_Tabl_Curr[index];
      DataGridViewRow dataGridViewRow = (DataGridViewRow) this.dataGridView_Conformity.Rows[0].Clone();
      dataGridViewRow.Cells[0].Value = (object) conformityTemplateNastr._name_Ved;
      dataGridViewRow.Cells[1].Value = (object) conformityTemplateNastr._guid_TypeVed;
      dataGridViewRow.Cells[2].Value = (object) conformityTemplateNastr._name_Template;
      dataGridViewRow.Cells[3].Value = (object) conformityTemplateNastr._guid_Template;
      dataGridViewRow.Cells[4].Value = (object) conformityTemplateNastr._name_One_Ved_Nastr;
      dataGridViewRow.Cells[5].Value = (object) conformityTemplateNastr._guid_One_Ved_Nastr;
      dataGridViewRow.Cells[6].Value = (object) conformityTemplateNastr._typeDoc.ToString();
      if (conformityTemplateNastr._one_Conformity_Template_Nastr_Parent != null)
        dataGridViewRow.Cells[7].Value = (object) conformityTemplateNastr._one_Conformity_Template_Nastr_Parent._name_Ved;
      this.dataGridView_Conformity.Rows.Add(dataGridViewRow);
    }
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    DataGridViewCellStyle gridViewCellStyle1 = new DataGridViewCellStyle();
    DataGridViewCellStyle gridViewCellStyle2 = new DataGridViewCellStyle();
    DataGridViewCellStyle gridViewCellStyle3 = new DataGridViewCellStyle();
    DataGridViewCellStyle gridViewCellStyle4 = new DataGridViewCellStyle();
    DataGridViewCellStyle gridViewCellStyle5 = new DataGridViewCellStyle();
    DataGridViewCellStyle gridViewCellStyle6 = new DataGridViewCellStyle();
    DataGridViewCellStyle gridViewCellStyle7 = new DataGridViewCellStyle();
    DataGridViewCellStyle gridViewCellStyle8 = new DataGridViewCellStyle();
    DataGridViewCellStyle gridViewCellStyle9 = new DataGridViewCellStyle();
    DataGridViewCellStyle gridViewCellStyle10 = new DataGridViewCellStyle();
    DataGridViewCellStyle gridViewCellStyle11 = new DataGridViewCellStyle();
    DataGridViewCellStyle gridViewCellStyle12 = new DataGridViewCellStyle();
    this.bCancel = new Button();
    this.buttonSave1 = new Button();
    this.bOK = new Button();
    this.tabControl1 = new TabControl();
    this.tabPage_Ved = new TabPage();
    this.groupBox_Ved = new GroupBox();
    this.dataGridView_Ved = new DataGridView();
    this.dataGridViewTextBoxColumn12 = new DataGridViewTextBoxColumn();
    this.dataGridViewTextBoxColumn13 = new DataGridViewTextBoxColumn();
    this.ColumnSystem = new DataGridViewTextBoxColumn();
    this.tabPage_Tabl = new TabPage();
    this.groupBox_Tabl = new GroupBox();
    this.dataGridView_Tabl = new DataGridView();
    this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
    this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
    this.dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
    this.button_Analiz = new Button();
    this.tabControl_Analiz = new TabControl();
    this.tabPage_Confirmity = new TabPage();
    this.groupBox_Conformity = new GroupBox();
    this.dataGridView_Conformity = new DataGridView();
    this.dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
    this.dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
    this.dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
    this.Column1 = new DataGridViewTextBoxColumn();
    this.Column2 = new DataGridViewTextBoxColumn();
    this.Column3 = new DataGridViewTextBoxColumn();
    this.Column4 = new DataGridViewTextBoxColumn();
    this.Column5 = new DataGridViewTextBoxColumn();
    this.tabPage_Arbeit = new TabPage();
    this.groupBox_Arbeit = new GroupBox();
    this.dataGridView_Arbeit = new DataGridView();
    this.dataGridViewTextBoxColumn7 = new DataGridViewTextBoxColumn();
    this.dataGridViewTextBoxColumn8 = new DataGridViewTextBoxColumn();
    this.dataGridViewTextBoxColumn9 = new DataGridViewTextBoxColumn();
    this.buttonDefault = new Button();
    this.tabControl1.SuspendLayout();
    this.tabPage_Ved.SuspendLayout();
    this.groupBox_Ved.SuspendLayout();
    ((ISupportInitialize) this.dataGridView_Ved).BeginInit();
    this.tabPage_Tabl.SuspendLayout();
    this.groupBox_Tabl.SuspendLayout();
    ((ISupportInitialize) this.dataGridView_Tabl).BeginInit();
    this.tabControl_Analiz.SuspendLayout();
    this.tabPage_Confirmity.SuspendLayout();
    this.groupBox_Conformity.SuspendLayout();
    ((ISupportInitialize) this.dataGridView_Conformity).BeginInit();
    this.tabPage_Arbeit.SuspendLayout();
    this.groupBox_Arbeit.SuspendLayout();
    ((ISupportInitialize) this.dataGridView_Arbeit).BeginInit();
    this.SuspendLayout();
    this.bCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.bCancel.DialogResult = DialogResult.Cancel;
    this.bCancel.Location = new Point(666, 411);
    this.bCancel.Name = "bCancel";
    this.bCancel.Size = new Size(109, 27);
    this.bCancel.TabIndex = 6;
    this.bCancel.Text = "Отмена";
    this.bCancel.UseVisualStyleBackColor = true;
    this.buttonSave1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.buttonSave1.Location = new Point(372, 411);
    this.buttonSave1.Name = "buttonSave1";
    this.buttonSave1.Size = new Size(109, 27);
    this.buttonSave1.TabIndex = 7;
    this.buttonSave1.Text = "Сохранить";
    this.buttonSave1.UseVisualStyleBackColor = true;
    this.buttonSave1.Click += new EventHandler(this.buttonSave1_Click);
    this.bOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.bOK.DialogResult = DialogResult.OK;
    this.bOK.Location = new Point(524, 411);
    this.bOK.Name = "bOK";
    this.bOK.Size = new Size(109, 27);
    this.bOK.TabIndex = 5;
    this.bOK.Text = "OK";
    this.bOK.UseVisualStyleBackColor = true;
    this.tabControl1.Controls.Add((Control) this.tabPage_Ved);
    this.tabControl1.Controls.Add((Control) this.tabPage_Tabl);
    this.tabControl1.Location = new Point(2, 3);
    this.tabControl1.Name = "tabControl1";
    this.tabControl1.SelectedIndex = 0;
    this.tabControl1.Size = new Size(796, 379);
    this.tabControl1.TabIndex = 17;
    this.tabPage_Ved.Controls.Add((Control) this.groupBox_Ved);
    this.tabPage_Ved.Location = new Point(4, 22);
    this.tabPage_Ved.Name = "tabPage_Ved";
    this.tabPage_Ved.Padding = new Padding(3);
    this.tabPage_Ved.Size = new Size(788, 353);
    this.tabPage_Ved.TabIndex = 0;
    this.tabPage_Ved.Text = "Ведомости";
    this.tabPage_Ved.UseVisualStyleBackColor = true;
    this.groupBox_Ved.Controls.Add((Control) this.dataGridView_Ved);
    this.groupBox_Ved.Dock = DockStyle.Fill;
    this.groupBox_Ved.Location = new Point(3, 3);
    this.groupBox_Ved.Name = "groupBox_Ved";
    this.groupBox_Ved.Size = new Size(782, 347);
    this.groupBox_Ved.TabIndex = 17;
    this.groupBox_Ved.TabStop = false;
    this.groupBox_Ved.Text = "Соответствие";
    this.dataGridView_Ved.AllowUserToResizeColumns = false;
    this.dataGridView_Ved.AllowUserToResizeRows = false;
    gridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
    gridViewCellStyle1.BackColor = SystemColors.Control;
    gridViewCellStyle1.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
    gridViewCellStyle1.ForeColor = SystemColors.WindowText;
    gridViewCellStyle1.SelectionBackColor = SystemColors.Control;
    gridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
    gridViewCellStyle1.WrapMode = DataGridViewTriState.True;
    this.dataGridView_Ved.ColumnHeadersDefaultCellStyle = gridViewCellStyle1;
    this.dataGridView_Ved.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
    this.dataGridView_Ved.Columns.AddRange((DataGridViewColumn) this.dataGridViewTextBoxColumn12, (DataGridViewColumn) this.dataGridViewTextBoxColumn13, (DataGridViewColumn) this.ColumnSystem);
    gridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
    gridViewCellStyle2.BackColor = SystemColors.Window;
    gridViewCellStyle2.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    gridViewCellStyle2.ForeColor = SystemColors.ControlText;
    gridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
    gridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
    gridViewCellStyle2.WrapMode = DataGridViewTriState.False;
    this.dataGridView_Ved.DefaultCellStyle = gridViewCellStyle2;
    this.dataGridView_Ved.Dock = DockStyle.Fill;
    this.dataGridView_Ved.EditMode = DataGridViewEditMode.EditProgrammatically;
    this.dataGridView_Ved.EnableHeadersVisualStyles = false;
    this.dataGridView_Ved.Location = new Point(3, 16 /*0x10*/);
    this.dataGridView_Ved.Name = "dataGridView_Ved";
    gridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
    gridViewCellStyle3.BackColor = SystemColors.Control;
    gridViewCellStyle3.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    gridViewCellStyle3.ForeColor = SystemColors.WindowText;
    gridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
    gridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
    gridViewCellStyle3.WrapMode = DataGridViewTriState.True;
    this.dataGridView_Ved.RowHeadersDefaultCellStyle = gridViewCellStyle3;
    this.dataGridView_Ved.RowHeadersWidth = 30;
    this.dataGridView_Ved.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
    this.dataGridView_Ved.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
    this.dataGridView_Ved.Size = new Size(776, 328);
    this.dataGridView_Ved.StandardTab = true;
    this.dataGridView_Ved.TabIndex = 2;
    this.dataGridViewTextBoxColumn12.HeaderText = "Шаблон";
    this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
    this.dataGridViewTextBoxColumn12.Resizable = DataGridViewTriState.False;
    this.dataGridViewTextBoxColumn12.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.dataGridViewTextBoxColumn12.Width = 292;
    this.dataGridViewTextBoxColumn13.HeaderText = "Документ";
    this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
    this.dataGridViewTextBoxColumn13.Resizable = DataGridViewTriState.False;
    this.dataGridViewTextBoxColumn13.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.dataGridViewTextBoxColumn13.Width = 292;
    this.ColumnSystem.FillWeight = 25f;
    this.ColumnSystem.HeaderText = "";
    this.ColumnSystem.MinimumWidth = 2;
    this.ColumnSystem.Name = "ColumnSystem";
    this.ColumnSystem.Width = 25;
    this.tabPage_Tabl.Controls.Add((Control) this.groupBox_Tabl);
    this.tabPage_Tabl.Location = new Point(4, 22);
    this.tabPage_Tabl.Name = "tabPage_Tabl";
    this.tabPage_Tabl.Padding = new Padding(3);
    this.tabPage_Tabl.Size = new Size(788, 353);
    this.tabPage_Tabl.TabIndex = 1;
    this.tabPage_Tabl.Text = "Таблицы";
    this.tabPage_Tabl.UseVisualStyleBackColor = true;
    this.groupBox_Tabl.Controls.Add((Control) this.dataGridView_Tabl);
    this.groupBox_Tabl.Dock = DockStyle.Fill;
    this.groupBox_Tabl.Location = new Point(3, 3);
    this.groupBox_Tabl.Name = "groupBox_Tabl";
    this.groupBox_Tabl.Size = new Size(782, 347);
    this.groupBox_Tabl.TabIndex = 18;
    this.groupBox_Tabl.TabStop = false;
    this.groupBox_Tabl.Text = "Соответствие";
    this.dataGridView_Tabl.AllowUserToResizeColumns = false;
    this.dataGridView_Tabl.AllowUserToResizeRows = false;
    gridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
    gridViewCellStyle4.BackColor = SystemColors.Control;
    gridViewCellStyle4.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
    gridViewCellStyle4.ForeColor = SystemColors.WindowText;
    gridViewCellStyle4.SelectionBackColor = SystemColors.Control;
    gridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
    gridViewCellStyle4.WrapMode = DataGridViewTriState.True;
    this.dataGridView_Tabl.ColumnHeadersDefaultCellStyle = gridViewCellStyle4;
    this.dataGridView_Tabl.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
    this.dataGridView_Tabl.Columns.AddRange((DataGridViewColumn) this.dataGridViewTextBoxColumn1, (DataGridViewColumn) this.dataGridViewTextBoxColumn2, (DataGridViewColumn) this.dataGridViewTextBoxColumn3);
    gridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
    gridViewCellStyle5.BackColor = SystemColors.Window;
    gridViewCellStyle5.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    gridViewCellStyle5.ForeColor = SystemColors.ControlText;
    gridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
    gridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
    gridViewCellStyle5.WrapMode = DataGridViewTriState.False;
    this.dataGridView_Tabl.DefaultCellStyle = gridViewCellStyle5;
    this.dataGridView_Tabl.Dock = DockStyle.Fill;
    this.dataGridView_Tabl.EditMode = DataGridViewEditMode.EditProgrammatically;
    this.dataGridView_Tabl.EnableHeadersVisualStyles = false;
    this.dataGridView_Tabl.Location = new Point(3, 16 /*0x10*/);
    this.dataGridView_Tabl.Name = "dataGridView_Tabl";
    gridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
    gridViewCellStyle6.BackColor = SystemColors.Control;
    gridViewCellStyle6.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    gridViewCellStyle6.ForeColor = SystemColors.WindowText;
    gridViewCellStyle6.SelectionBackColor = SystemColors.Highlight;
    gridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText;
    gridViewCellStyle6.WrapMode = DataGridViewTriState.True;
    this.dataGridView_Tabl.RowHeadersDefaultCellStyle = gridViewCellStyle6;
    this.dataGridView_Tabl.RowHeadersWidth = 30;
    this.dataGridView_Tabl.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
    this.dataGridView_Tabl.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
    this.dataGridView_Tabl.Size = new Size(776, 328);
    this.dataGridView_Tabl.StandardTab = true;
    this.dataGridView_Tabl.TabIndex = 2;
    this.dataGridViewTextBoxColumn1.HeaderText = "Шаблон";
    this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
    this.dataGridViewTextBoxColumn1.Resizable = DataGridViewTriState.False;
    this.dataGridViewTextBoxColumn1.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.dataGridViewTextBoxColumn1.Width = 292;
    this.dataGridViewTextBoxColumn2.HeaderText = "Документ";
    this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
    this.dataGridViewTextBoxColumn2.Resizable = DataGridViewTriState.False;
    this.dataGridViewTextBoxColumn2.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.dataGridViewTextBoxColumn2.Width = 292;
    this.dataGridViewTextBoxColumn3.HeaderText = "";
    this.dataGridViewTextBoxColumn3.MinimumWidth = 2;
    this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
    this.dataGridViewTextBoxColumn3.Width = 25;
    this.button_Analiz.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this.button_Analiz.Location = new Point(12, 411);
    this.button_Analiz.Name = "button_Analiz";
    this.button_Analiz.Size = new Size(109, 27);
    this.button_Analiz.TabIndex = 18;
    this.button_Analiz.Text = "Анализ";
    this.button_Analiz.UseVisualStyleBackColor = true;
    this.button_Analiz.Click += new EventHandler(this.button_Analiz_Click);
    this.tabControl_Analiz.Controls.Add((Control) this.tabPage_Confirmity);
    this.tabControl_Analiz.Controls.Add((Control) this.tabPage_Arbeit);
    this.tabControl_Analiz.Location = new Point(772, 3);
    this.tabControl_Analiz.Name = "tabControl_Analiz";
    this.tabControl_Analiz.SelectedIndex = 0;
    this.tabControl_Analiz.Size = new Size(1880, 800);
    this.tabControl_Analiz.TabIndex = 19;
    this.tabControl_Analiz.Visible = false;
    this.tabPage_Confirmity.Controls.Add((Control) this.groupBox_Conformity);
    this.tabPage_Confirmity.Location = new Point(4, 22);
    this.tabPage_Confirmity.Name = "tabPage_Confirmity";
    this.tabPage_Confirmity.Padding = new Padding(3);
    this.tabPage_Confirmity.Size = new Size(1872, 774);
    this.tabPage_Confirmity.TabIndex = 0;
    this.tabPage_Confirmity.Text = "Confirmity";
    this.tabPage_Confirmity.UseVisualStyleBackColor = true;
    this.groupBox_Conformity.Controls.Add((Control) this.dataGridView_Conformity);
    this.groupBox_Conformity.Dock = DockStyle.Fill;
    this.groupBox_Conformity.Location = new Point(3, 3);
    this.groupBox_Conformity.Name = "groupBox_Conformity";
    this.groupBox_Conformity.Size = new Size(1866, 768 /*0x0300*/);
    this.groupBox_Conformity.TabIndex = 17;
    this.groupBox_Conformity.TabStop = false;
    this.groupBox_Conformity.Text = "Confirmity";
    this.dataGridView_Conformity.AllowUserToResizeColumns = false;
    this.dataGridView_Conformity.AllowUserToResizeRows = false;
    gridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleLeft;
    gridViewCellStyle7.BackColor = SystemColors.Control;
    gridViewCellStyle7.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
    gridViewCellStyle7.ForeColor = SystemColors.WindowText;
    gridViewCellStyle7.SelectionBackColor = SystemColors.Control;
    gridViewCellStyle7.SelectionForeColor = SystemColors.HighlightText;
    gridViewCellStyle7.WrapMode = DataGridViewTriState.True;
    this.dataGridView_Conformity.ColumnHeadersDefaultCellStyle = gridViewCellStyle7;
    this.dataGridView_Conformity.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
    this.dataGridView_Conformity.Columns.AddRange((DataGridViewColumn) this.dataGridViewTextBoxColumn4, (DataGridViewColumn) this.dataGridViewTextBoxColumn5, (DataGridViewColumn) this.dataGridViewTextBoxColumn6, (DataGridViewColumn) this.Column1, (DataGridViewColumn) this.Column2, (DataGridViewColumn) this.Column3, (DataGridViewColumn) this.Column4, (DataGridViewColumn) this.Column5);
    gridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleLeft;
    gridViewCellStyle8.BackColor = SystemColors.Window;
    gridViewCellStyle8.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    gridViewCellStyle8.ForeColor = SystemColors.ControlText;
    gridViewCellStyle8.SelectionBackColor = SystemColors.Highlight;
    gridViewCellStyle8.SelectionForeColor = SystemColors.HighlightText;
    gridViewCellStyle8.WrapMode = DataGridViewTriState.False;
    this.dataGridView_Conformity.DefaultCellStyle = gridViewCellStyle8;
    this.dataGridView_Conformity.Dock = DockStyle.Fill;
    this.dataGridView_Conformity.EditMode = DataGridViewEditMode.EditProgrammatically;
    this.dataGridView_Conformity.EnableHeadersVisualStyles = false;
    this.dataGridView_Conformity.Location = new Point(3, 16 /*0x10*/);
    this.dataGridView_Conformity.Name = "dataGridView_Conformity";
    gridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleLeft;
    gridViewCellStyle9.BackColor = SystemColors.Control;
    gridViewCellStyle9.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    gridViewCellStyle9.ForeColor = SystemColors.WindowText;
    gridViewCellStyle9.SelectionBackColor = SystemColors.Highlight;
    gridViewCellStyle9.SelectionForeColor = SystemColors.HighlightText;
    gridViewCellStyle9.WrapMode = DataGridViewTriState.True;
    this.dataGridView_Conformity.RowHeadersDefaultCellStyle = gridViewCellStyle9;
    this.dataGridView_Conformity.RowHeadersWidth = 30;
    this.dataGridView_Conformity.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
    this.dataGridView_Conformity.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
    this.dataGridView_Conformity.Size = new Size(1860, 749);
    this.dataGridView_Conformity.StandardTab = true;
    this.dataGridView_Conformity.TabIndex = 2;
    this.dataGridViewTextBoxColumn4.HeaderText = "name_Ved";
    this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
    this.dataGridViewTextBoxColumn4.Resizable = DataGridViewTriState.False;
    this.dataGridViewTextBoxColumn4.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.dataGridViewTextBoxColumn4.Width = 260;
    this.dataGridViewTextBoxColumn5.HeaderText = "guid_TypeVed";
    this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
    this.dataGridViewTextBoxColumn5.Resizable = DataGridViewTriState.False;
    this.dataGridViewTextBoxColumn5.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.dataGridViewTextBoxColumn5.Width = 240 /*0xF0*/;
    this.dataGridViewTextBoxColumn6.FillWeight = 25f;
    this.dataGridViewTextBoxColumn6.HeaderText = "name_Template";
    this.dataGridViewTextBoxColumn6.MinimumWidth = 2;
    this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
    this.dataGridViewTextBoxColumn6.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.dataGridViewTextBoxColumn6.Width = 260;
    this.Column1.HeaderText = "guid_Template";
    this.Column1.Name = "Column1";
    this.Column1.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.Column1.Width = 240 /*0xF0*/;
    this.Column2.HeaderText = "name_One_Ved_Nastr";
    this.Column2.Name = "Column2";
    this.Column2.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.Column2.Width = 260;
    this.Column3.HeaderText = "guid_One_Ved_Nastr";
    this.Column3.Name = "Column3";
    this.Column3.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.Column3.Width = 240 /*0xF0*/;
    this.Column4.HeaderText = "typeDoc";
    this.Column4.MinimumWidth = 30;
    this.Column4.Name = "Column4";
    this.Column4.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.Column5.HeaderText = "Parent_name_Ved";
    this.Column5.Name = "Column5";
    this.Column5.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.Column5.Width = 220;
    this.tabPage_Arbeit.Controls.Add((Control) this.groupBox_Arbeit);
    this.tabPage_Arbeit.Location = new Point(4, 22);
    this.tabPage_Arbeit.Name = "tabPage_Arbeit";
    this.tabPage_Arbeit.Padding = new Padding(3);
    this.tabPage_Arbeit.Size = new Size(1872, 774);
    this.tabPage_Arbeit.TabIndex = 1;
    this.tabPage_Arbeit.Text = "Arbeit";
    this.tabPage_Arbeit.UseVisualStyleBackColor = true;
    this.groupBox_Arbeit.Controls.Add((Control) this.dataGridView_Arbeit);
    this.groupBox_Arbeit.Dock = DockStyle.Fill;
    this.groupBox_Arbeit.Location = new Point(3, 3);
    this.groupBox_Arbeit.Name = "groupBox_Arbeit";
    this.groupBox_Arbeit.Size = new Size(1866, 768 /*0x0300*/);
    this.groupBox_Arbeit.TabIndex = 18;
    this.groupBox_Arbeit.TabStop = false;
    this.groupBox_Arbeit.Text = "Arbeit";
    this.dataGridView_Arbeit.AllowUserToResizeColumns = false;
    this.dataGridView_Arbeit.AllowUserToResizeRows = false;
    gridViewCellStyle10.Alignment = DataGridViewContentAlignment.MiddleLeft;
    gridViewCellStyle10.BackColor = SystemColors.Control;
    gridViewCellStyle10.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
    gridViewCellStyle10.ForeColor = SystemColors.WindowText;
    gridViewCellStyle10.SelectionBackColor = SystemColors.Control;
    gridViewCellStyle10.SelectionForeColor = SystemColors.HighlightText;
    gridViewCellStyle10.WrapMode = DataGridViewTriState.True;
    this.dataGridView_Arbeit.ColumnHeadersDefaultCellStyle = gridViewCellStyle10;
    this.dataGridView_Arbeit.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
    this.dataGridView_Arbeit.Columns.AddRange((DataGridViewColumn) this.dataGridViewTextBoxColumn7, (DataGridViewColumn) this.dataGridViewTextBoxColumn8, (DataGridViewColumn) this.dataGridViewTextBoxColumn9);
    gridViewCellStyle11.Alignment = DataGridViewContentAlignment.MiddleLeft;
    gridViewCellStyle11.BackColor = SystemColors.Window;
    gridViewCellStyle11.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    gridViewCellStyle11.ForeColor = SystemColors.ControlText;
    gridViewCellStyle11.SelectionBackColor = SystemColors.Highlight;
    gridViewCellStyle11.SelectionForeColor = SystemColors.HighlightText;
    gridViewCellStyle11.WrapMode = DataGridViewTriState.False;
    this.dataGridView_Arbeit.DefaultCellStyle = gridViewCellStyle11;
    this.dataGridView_Arbeit.Dock = DockStyle.Fill;
    this.dataGridView_Arbeit.EditMode = DataGridViewEditMode.EditProgrammatically;
    this.dataGridView_Arbeit.EnableHeadersVisualStyles = false;
    this.dataGridView_Arbeit.Location = new Point(3, 16 /*0x10*/);
    this.dataGridView_Arbeit.Name = "dataGridView_Arbeit";
    gridViewCellStyle12.Alignment = DataGridViewContentAlignment.MiddleLeft;
    gridViewCellStyle12.BackColor = SystemColors.Control;
    gridViewCellStyle12.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    gridViewCellStyle12.ForeColor = SystemColors.WindowText;
    gridViewCellStyle12.SelectionBackColor = SystemColors.Highlight;
    gridViewCellStyle12.SelectionForeColor = SystemColors.HighlightText;
    gridViewCellStyle12.WrapMode = DataGridViewTriState.True;
    this.dataGridView_Arbeit.RowHeadersDefaultCellStyle = gridViewCellStyle12;
    this.dataGridView_Arbeit.RowHeadersWidth = 30;
    this.dataGridView_Arbeit.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
    this.dataGridView_Arbeit.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
    this.dataGridView_Arbeit.Size = new Size(1860, 749);
    this.dataGridView_Arbeit.StandardTab = true;
    this.dataGridView_Arbeit.TabIndex = 2;
    this.dataGridViewTextBoxColumn7.HeaderText = "Шаблон";
    this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
    this.dataGridViewTextBoxColumn7.Resizable = DataGridViewTriState.False;
    this.dataGridViewTextBoxColumn7.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.dataGridViewTextBoxColumn7.Width = 292;
    this.dataGridViewTextBoxColumn8.HeaderText = "Документ";
    this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
    this.dataGridViewTextBoxColumn8.Resizable = DataGridViewTriState.False;
    this.dataGridViewTextBoxColumn8.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.dataGridViewTextBoxColumn8.Width = 292;
    this.dataGridViewTextBoxColumn9.HeaderText = "";
    this.dataGridViewTextBoxColumn9.MinimumWidth = 2;
    this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
    this.dataGridViewTextBoxColumn9.Width = 25;
    this.buttonDefault.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.buttonDefault.Location = new Point(213, 411);
    this.buttonDefault.Name = "buttonDefault";
    this.buttonDefault.Size = new Size(121, 27);
    this.buttonDefault.TabIndex = 20;
    this.buttonDefault.Text = "По умолчанию";
    this.buttonDefault.UseVisualStyleBackColor = true;
    this.buttonDefault.Click += new EventHandler(this.buttonDefault_Click);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.BackColor = Color.LightYellow;
    this.CancelButton = (IButtonControl) this.bCancel;
    this.ClientSize = new Size(800, 450);
    this.Controls.Add((Control) this.buttonDefault);
    this.Controls.Add((Control) this.tabControl_Analiz);
    this.Controls.Add((Control) this.button_Analiz);
    this.Controls.Add((Control) this.tabControl1);
    this.Controls.Add((Control) this.bCancel);
    this.Controls.Add((Control) this.buttonSave1);
    this.Controls.Add((Control) this.bOK);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (Conformity_Template_Nastr);
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Соответствия шаблонов и настроек";
    this.Load += new EventHandler(this.Conformity_Template_Nastr_Load);
    this.tabControl1.ResumeLayout(false);
    this.tabPage_Ved.ResumeLayout(false);
    this.groupBox_Ved.ResumeLayout(false);
    ((ISupportInitialize) this.dataGridView_Ved).EndInit();
    this.tabPage_Tabl.ResumeLayout(false);
    this.groupBox_Tabl.ResumeLayout(false);
    ((ISupportInitialize) this.dataGridView_Tabl).EndInit();
    this.tabControl_Analiz.ResumeLayout(false);
    this.tabPage_Confirmity.ResumeLayout(false);
    this.groupBox_Conformity.ResumeLayout(false);
    ((ISupportInitialize) this.dataGridView_Conformity).EndInit();
    this.tabPage_Arbeit.ResumeLayout(false);
    this.groupBox_Arbeit.ResumeLayout(false);
    ((ISupportInitialize) this.dataGridView_Arbeit).EndInit();
    this.ResumeLayout(false);
  }
}
