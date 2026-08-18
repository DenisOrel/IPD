// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Victor.VariablesList
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS.Victor;

public class VariablesList : Form
{
  public string _variable_template = "";
  public Vedomost_VB.Variables_Coordination _variables_Coordination;
  public Vedomost_VB.Variables_for_VariableDialog _variables = new Vedomost_VB.Variables_for_VariableDialog();
  public string _designationArt;
  public bool _isModified;
  private string newVariable = "";
  private string newCaption = "";
  private bool is_Draw1_Create = true;
  private bool is_Draw2_Create = true;
  private Vedomost_VB.One_Variable_for_VariableDialog one_variable_tmp;
  private int i_variable_Curr;
  private Vedomost_VB.One_Variable_for_VariableDialog one_variable_curr;
  private Vedomost_VB.One_Variable_for_VariableDialog one_variable_edit;
  private int i_template = -1;
  private Vedomost_VB.One_Variable_for_VariableDialog one_variable_template;
  private VariablesList.Mode_Draw3 mode_Draw3;
  private int number_ekrana = 1;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel panelForButtons;
  internal Button bCancel;
  internal Button bOK;
  private DataGridView dataGridView1;
  internal Button buttonAdd;
  internal Button buttonRename;
  internal Button buttonDelete;
  internal Button buttonCancel2;
  internal Button buttonOk2;
  private Label label1;
  internal Button buttonCancel3;
  internal Button buttonOk3;
  private ImageList images;
  private DataGridViewImageColumn ImgColumn;
  private DataGridViewTextBoxColumn Column1;
  private DataGridViewTextBoxColumn Column2;
  private Label label3;
  private Label label32;
  private Label label33;
  private Label label3_NameTemplate;

  public VariablesList() => this.InitializeComponent();

  private void VariablesList_Load(object sender, EventArgs e)
  {
    if (this._variables_Coordination == null)
      this.Close();
    for (int index = 0; index < this._variables_Coordination.list_Variables.Count; ++index)
    {
      string listVariable = this._variables_Coordination.list_Variables[index];
      string listCaption = this._variables_Coordination.list_Captions[index];
      if (!string.IsNullOrEmpty(listVariable) && !string.IsNullOrEmpty(listCaption))
      {
        this.one_variable_tmp = new Vedomost_VB.One_Variable_for_VariableDialog();
        this.one_variable_tmp._variable = listVariable;
        this.one_variable_tmp._caption = listCaption;
        this._variables.list_Variables.Add(this.one_variable_tmp);
      }
    }
    this.one_variable_curr = this._variables.list_Variables[this.i_variable_Curr];
    this.Draw1();
    this.is_Draw1_Create = false;
  }

  private void Draw1()
  {
    this.is_Draw1_Create = true;
    this.dataGridView1.Location = new Point(12, 0);
    this.dataGridView1.Size = new Size(554, 310);
    this.dataGridView1.Rows.Clear();
    this.dataGridView1.RowCount = this._variables.list_Variables.Count;
    this.dataGridView1.Select();
    this.Text = "Текущий список исполнений";
    for (int index = 0; index < this._variables.list_Variables.Count; ++index)
    {
      this.one_variable_tmp = this._variables.list_Variables[index];
      if (!this.one_variable_tmp._isDeleted && !this.one_variable_tmp._isNew && !this.one_variable_tmp._isRename && !this.one_variable_tmp._isTemplate)
      {
        this.dataGridView1.Rows[index].Cells[0].Value = (object) this.images.Images[4];
      }
      else
      {
        if (this.one_variable_tmp._isDeleted)
          this.dataGridView1.Rows[index].Cells[0].Value = (object) this.images.Images[6];
        if (this.one_variable_tmp._isNew)
          this.dataGridView1.Rows[index].Cells[0].Value = (object) this.images.Images[7];
        if (this.one_variable_tmp._isRename)
          this.dataGridView1.Rows[index].Cells[0].Value = (object) this.images.Images[8];
        if (this.one_variable_tmp._isTemplate)
          this.dataGridView1.Rows[index].Cells[0].Value = (object) this.images.Images[5];
        this.bOK.Enabled = true;
        this._isModified = true;
      }
      this.dataGridView1.Rows[index].Cells[1].Value = (object) this.one_variable_tmp._variable;
      this.dataGridView1.Rows[index].Cells[2].Value = (object) this.one_variable_tmp._caption;
    }
    this.dataGridView1.Columns[1].HeaderText = "Обозначение исполнения";
    this.dataGridView1.CurrentCell = this.dataGridView1.Rows[this.i_variable_Curr].Cells[0];
    this.buttonAdd.Visible = true;
    this.buttonDelete.Visible = true;
    if (this._variables.list_Variables.Count < 2)
      this.buttonDelete.Enabled = false;
    else
      this.buttonDelete.Enabled = true;
    this.buttonRename.Visible = true;
    this.buttonOk2.Visible = false;
    this.buttonCancel2.Visible = false;
    this.buttonOk3.Visible = false;
    this.buttonCancel3.Visible = false;
    this.label1.Visible = false;
    this.label3.Visible = false;
    this.label32.Visible = false;
    this.label33.Visible = false;
    this.label3_NameTemplate.Text = "";
    this.label3_NameTemplate.Visible = false;
    this.dataGridView1.Select();
    this.number_ekrana = 1;
    this.is_Draw1_Create = false;
    this.one_variable_template = (Vedomost_VB.One_Variable_for_VariableDialog) null;
    this.one_variable_edit = (Vedomost_VB.One_Variable_for_VariableDialog) null;
  }

  private void buttonAdd_Click(object sender, EventArgs e) => this.Draw2();

  /// <summary> Режим выбора прототипа исполнения </summary>
  private void Draw2()
  {
    this.is_Draw2_Create = true;
    this.number_ekrana = 2;
    this.dataGridView1.Location = new Point(12, 58);
    this.dataGridView1.Size = new Size(554, 298);
    this.dataGridView1.Rows.Clear();
    this.dataGridView1.RowCount = this._variables.list_Variables.Count + 1;
    this.dataGridView1.Rows[0].Cells[1].Value = (object) "[Без прототипа]";
    this.dataGridView1.Rows[0].Cells[2].Value = (object) "";
    this.dataGridView1.Rows[0].Cells[0].Value = (object) this.images.Images[4];
    for (int index = 0; index < this._variables.list_Variables.Count; ++index)
    {
      this.one_variable_tmp = this._variables.list_Variables[index];
      this.dataGridView1.Rows[index + 1].Cells[1].Value = (object) this.one_variable_tmp._variable;
      this.dataGridView1.Rows[index + 1].Cells[2].Value = (object) this.one_variable_tmp._caption;
      if (!this.one_variable_tmp._isDeleted && !this.one_variable_tmp._isNew && !this.one_variable_tmp._isRename && !this.one_variable_tmp._isTemplate)
      {
        this.dataGridView1.Rows[index + 1].Cells[0].Value = (object) this.images.Images[4];
      }
      else
      {
        if (this.one_variable_tmp._isDeleted)
          this.dataGridView1.Rows[index + 1].Cells[0].Value = (object) this.images.Images[6];
        if (this.one_variable_tmp._isNew)
          this.dataGridView1.Rows[index + 1].Cells[0].Value = (object) this.images.Images[7];
        if (this.one_variable_tmp._isRename)
          this.dataGridView1.Rows[index + 1].Cells[0].Value = (object) this.images.Images[8];
        if (this.one_variable_tmp._isTemplate)
          this.dataGridView1.Rows[index + 1].Cells[0].Value = (object) this.images.Images[5];
      }
    }
    this.dataGridView1.Columns[1].HeaderText = "Обозначение исполнения прототипа";
    this.dataGridView1.CurrentCell = this.dataGridView1.Rows[0].Cells[0];
    this.buttonAdd.Visible = false;
    this.buttonDelete.Visible = false;
    this.buttonRename.Visible = false;
    this.buttonOk2.Visible = true;
    this.buttonCancel2.Visible = true;
    this.buttonOk3.Visible = false;
    this.buttonCancel3.Visible = false;
    this.label1.Visible = true;
    this.label3.Visible = false;
    this.label32.Visible = false;
    this.label33.Visible = false;
    this.label3_NameTemplate.Text = "";
    this.label3_NameTemplate.Visible = false;
    this.dataGridView1.Select();
    this.Text = "Выбор прототипа";
    this.is_Draw2_Create = false;
  }

  /// <summary> Выбран пртотип или "Без прототипа" </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void buttonOk2_Click(object sender, EventArgs e)
  {
    this.mode_Draw3 = VariablesList.Mode_Draw3.New;
    this.Draw3();
  }

  private void buttonCancel2_Click(object sender, EventArgs e)
  {
    this.mode_Draw3 = VariablesList.Mode_Draw3.Undefined;
    this.number_ekrana = 1;
    this.i_template = -1;
    this.one_variable_edit = (Vedomost_VB.One_Variable_for_VariableDialog) null;
    this.Draw1();
  }

  /// <summary> Переименовать текущее исполнение </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void buttonRename_Click(object sender, EventArgs e)
  {
    this.mode_Draw3 = VariablesList.Mode_Draw3.Rename;
    this.one_variable_edit = this.one_variable_curr;
    this.Draw3();
  }

  private void Draw3()
  {
    this.number_ekrana = 3;
    this.dataGridView1.Location = new Point(12, 58);
    this.dataGridView1.Size = new Size(554, 51);
    this.dataGridView1.Rows.Clear();
    this.dataGridView1.RowCount = 1;
    this.dataGridView1.Rows[0].Cells[0].Value = (object) this.images.Images[4];
    string errorText = "";
    string variable = "";
    string caption = "";
    if (this.mode_Draw3 == VariablesList.Mode_Draw3.New)
    {
      this.dataGridView1.Columns[1].HeaderText = "Обозначение нового исполнения";
      if (Vedomost_VB_Static.Variable_Create(this._designationArt, true, this._variables.list_Variables.Count, out variable, out caption, out errorText))
      {
        this.one_variable_edit = new Vedomost_VB.One_Variable_for_VariableDialog();
        this.one_variable_edit._variable = variable;
        this.one_variable_edit._caption = caption;
        this.one_variable_edit.one_Variable_For_VariableDialog_Template = this.one_variable_template;
        this.one_variable_edit.i_variable_Template = this.i_template;
        this.mode_Draw3 = VariablesList.Mode_Draw3.New;
        if (this.one_variable_template != null)
        {
          this.label32.Visible = false;
          this.label33.Visible = true;
          this.label3_NameTemplate.Visible = true;
          this.label3_NameTemplate.Text = this.one_variable_template._variable;
        }
        else
        {
          this.label32.Visible = true;
          this.label33.Visible = false;
          this.label3_NameTemplate.Text = "";
          this.label3_NameTemplate.Visible = false;
        }
      }
      else
      {
        int num = (int) MessageBox.Show(errorText, "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        this.Draw1();
        return;
      }
    }
    if (this.mode_Draw3 == VariablesList.Mode_Draw3.Rename)
    {
      this.dataGridView1.Columns[1].HeaderText = "Обозначение изменяемого исполнения";
      this.label32.Visible = false;
      this.label33.Visible = false;
      this.label3_NameTemplate.Visible = false;
    }
    this.dataGridView1.Rows[0].Cells[1].Value = (object) this.one_variable_edit._variable;
    this.dataGridView1.Rows[0].Cells[2].Value = (object) this.one_variable_edit._caption;
    this.dataGridView1.Rows[0].Cells[1].ReadOnly = false;
    this.dataGridView1.Rows[0].Cells[2].ReadOnly = false;
    this.dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;
    this.dataGridView1.CurrentCell = this.dataGridView1.Rows[0].Cells[0];
    this.buttonAdd.Visible = false;
    this.buttonDelete.Visible = false;
    this.buttonRename.Visible = false;
    this.buttonOk2.Visible = false;
    this.buttonCancel2.Visible = false;
    this.buttonOk3.Visible = true;
    this.buttonCancel3.Visible = true;
    this.label1.Visible = false;
    this.label3.Visible = true;
    this.dataGridView1.Select();
    this.Text = "Подтверждение (Уточнение)";
    this.dataGridView1.CurrentCell = this.dataGridView1.Rows[0].Cells[1];
  }

  private void buttonDelete_Click(object sender, EventArgs e)
  {
    if (this._variables.list_Variables.Count < 2)
    {
      int num = (int) MessageBox.Show("Единственное исполнение удалить нельзя", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    else
    {
      if (this.one_variable_curr._isNew)
      {
        this._variables.list_Variables.RemoveAt(this.dataGridView1.SelectedCells[0].RowIndex);
        if (this.i_variable_Curr >= this._variables.list_Variables.Count)
          --this.i_variable_Curr;
        this.one_variable_curr = this._variables.list_Variables[this.i_variable_Curr];
        this.Draw1();
      }
      else
      {
        if (this.one_variable_curr._isDeleted)
        {
          this.one_variable_curr._isDeleted = false;
          this.dataGridView1.Rows[this.i_variable_Curr].Cells[0].Value = (object) this.images.Images[4];
          this.buttonRename.Enabled = true;
        }
        else
        {
          this.one_variable_curr._isDeleted = true;
          this.dataGridView1.Rows[this.i_variable_Curr].Cells[0].Value = (object) this.images.Images[6];
          this.buttonRename.Enabled = false;
        }
        this.one_variable_curr._isNew = false;
        this.one_variable_curr._isRename = false;
        this.one_variable_curr._isTemplate = false;
      }
      this._isModified = true;
      this.bOK.Enabled = true;
    }
  }

  /// <summary> Окончание редактирования исполнения (new или rename) </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void buttonOk3_Click(object sender, EventArgs e)
  {
    if (this.mode_Draw3 == VariablesList.Mode_Draw3.New)
    {
      string variable = this.dataGridView1.Rows[0].Cells[1].Value.ToString();
      if (!this._variables.Variable_check(variable, ""))
      {
        int num = (int) MessageBox.Show($"Исполнение\r\n\r\n{variable}\r\n\r\nуже существует", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        return;
      }
      string caption = this.dataGridView1.Rows[0].Cells[2].Value.ToString();
      if (!this._variables.Caption_check(caption, ""))
      {
        int num = (int) MessageBox.Show($"Заголовок\r\n\r\n{caption}\r\n\r\nуже существует", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        return;
      }
      this.one_variable_edit._variable = variable;
      this.one_variable_edit._caption = caption;
      this.one_variable_edit._isNew = true;
      this.one_variable_edit._isRename = false;
      this.one_variable_edit._isDeleted = false;
      this.one_variable_edit._isTemplate = false;
      this._variables.list_Variables.Add(this.one_variable_edit);
      this.one_variable_curr = this.one_variable_edit;
      this.i_variable_Curr = this._variables.list_Variables.Count - 1;
      if (this.one_variable_curr.i_variable_Template > -1)
      {
        this.one_variable_tmp = this._variables.list_Variables[this.one_variable_curr.i_variable_Template];
        this.one_variable_tmp._isTemplate = true;
      }
      this.one_variable_edit = (Vedomost_VB.One_Variable_for_VariableDialog) null;
    }
    if (this.mode_Draw3 == VariablesList.Mode_Draw3.Rename)
    {
      string variable = this.dataGridView1.Rows[0].Cells[1].Value.ToString();
      if (!this._variables.Variable_check(variable, this.one_variable_edit._variable))
      {
        int num = (int) MessageBox.Show($"Исполнение \"{variable}\" уже существует", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        return;
      }
      string caption = this.dataGridView1.Rows[0].Cells[2].Value.ToString();
      if (!this._variables.Caption_check(caption, this.one_variable_edit._caption))
      {
        int num = (int) MessageBox.Show($"Заголовок \"{caption}\" уже существует", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        return;
      }
      this.one_variable_edit._variable_old = this.one_variable_edit._variable;
      this.one_variable_edit._caption_old = this.one_variable_edit._caption;
      this.one_variable_edit._variable = variable;
      this.one_variable_edit._caption = caption;
      this.one_variable_edit._isRename = true;
      this.one_variable_edit._isNew = false;
    }
    this.dataGridView1.Rows[0].Cells[0].ReadOnly = true;
    this.dataGridView1.Rows[0].Cells[1].ReadOnly = true;
    this.dataGridView1.Rows[0].Cells[2].ReadOnly = true;
    this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
    this.Draw1();
    this.Text = "Новый список исполнений";
    this.dataGridView1_CellEnter(sender, (DataGridViewCellEventArgs) null);
    this.buttonOk2.Enabled = true;
  }

  /// <summary> Прерываем редактирование исполнения. Возвращаемся к первому экрану </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void buttonCancel3_Click(object sender, EventArgs e)
  {
    this.dataGridView1.Rows[0].Cells[0].ReadOnly = true;
    this.dataGridView1.Rows[0].Cells[1].ReadOnly = true;
    this.dataGridView1.Rows[0].Cells[2].ReadOnly = true;
    this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
    this.i_template = -1;
    this.one_variable_edit = (Vedomost_VB.One_Variable_for_VariableDialog) null;
    this.Draw1();
    this._isModified = true;
    this.bOK.Enabled = true;
  }

  private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
  {
    if (this.number_ekrana == 1 && !this.is_Draw1_Create)
    {
      this.buttonRename.Enabled = true;
      this.buttonDelete.Enabled = true;
      this.i_variable_Curr = this.dataGridView1.SelectedCells[0].RowIndex;
      this.one_variable_curr = this._variables.list_Variables[this.i_variable_Curr];
      if (this.one_variable_curr._isRename || this.one_variable_curr._isTemplate)
      {
        this.buttonRename.Enabled = false;
        this.buttonDelete.Enabled = false;
      }
      if (this.one_variable_curr._isDeleted)
        this.buttonRename.Enabled = false;
      if (this.one_variable_curr._isNew)
        this.buttonRename.Enabled = false;
    }
    if (this.number_ekrana != 2 || this.is_Draw2_Create)
      return;
    this.one_variable_tmp = this.dataGridView1.SelectedCells[0].RowIndex != 0 ? this._variables.list_Variables[this.dataGridView1.SelectedCells[0].RowIndex - 1] : (Vedomost_VB.One_Variable_for_VariableDialog) null;
    this.buttonOk2.Enabled = true;
    if (this.one_variable_tmp != null && (this.one_variable_tmp._isRename || this.one_variable_tmp._isNew || this.one_variable_tmp._isDeleted))
      this.buttonOk2.Enabled = false;
    if (this.dataGridView1.SelectedCells[0].RowIndex > 0)
    {
      this.i_template = this.dataGridView1.SelectedCells[0].RowIndex - 1;
      this.one_variable_template = this._variables.list_Variables[this.i_template];
    }
    else
    {
      this.i_template = -1;
      this.one_variable_template = (Vedomost_VB.One_Variable_for_VariableDialog) null;
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
    this.components = (IContainer) new System.ComponentModel.Container();
    DataGridViewCellStyle gridViewCellStyle = new DataGridViewCellStyle();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (VariablesList));
    this.panelForButtons = new Panel();
    this.buttonCancel3 = new Button();
    this.buttonOk3 = new Button();
    this.buttonCancel2 = new Button();
    this.buttonOk2 = new Button();
    this.bCancel = new Button();
    this.bOK = new Button();
    this.dataGridView1 = new DataGridView();
    this.ImgColumn = new DataGridViewImageColumn();
    this.Column1 = new DataGridViewTextBoxColumn();
    this.Column2 = new DataGridViewTextBoxColumn();
    this.buttonAdd = new Button();
    this.buttonRename = new Button();
    this.buttonDelete = new Button();
    this.label1 = new Label();
    this.images = new ImageList(this.components);
    this.label3 = new Label();
    this.label32 = new Label();
    this.label33 = new Label();
    this.label3_NameTemplate = new Label();
    this.panelForButtons.SuspendLayout();
    ((ISupportInitialize) this.dataGridView1).BeginInit();
    this.SuspendLayout();
    this.panelForButtons.BorderStyle = BorderStyle.Fixed3D;
    this.panelForButtons.Controls.Add((Control) this.buttonCancel3);
    this.panelForButtons.Controls.Add((Control) this.buttonOk3);
    this.panelForButtons.Controls.Add((Control) this.buttonCancel2);
    this.panelForButtons.Controls.Add((Control) this.buttonOk2);
    this.panelForButtons.Controls.Add((Control) this.bCancel);
    this.panelForButtons.Controls.Add((Control) this.bOK);
    this.panelForButtons.Dock = DockStyle.Bottom;
    this.panelForButtons.Location = new Point(0, 369);
    this.panelForButtons.Name = "panelForButtons";
    this.panelForButtons.Size = new Size(578, 42);
    this.panelForButtons.TabIndex = 13;
    this.buttonCancel3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.buttonCancel3.Location = new Point(443, 8);
    this.buttonCancel3.Name = "buttonCancel3";
    this.buttonCancel3.Size = new Size(121, 27);
    this.buttonCancel3.TabIndex = 6;
    this.buttonCancel3.Text = "Отмена";
    this.buttonCancel3.UseVisualStyleBackColor = true;
    this.buttonCancel3.Click += new EventHandler(this.buttonCancel3_Click);
    this.buttonOk3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.buttonOk3.Location = new Point(292, 8);
    this.buttonOk3.Name = "buttonOk3";
    this.buttonOk3.Size = new Size(121, 27);
    this.buttonOk3.TabIndex = 5;
    this.buttonOk3.Text = "OK";
    this.buttonOk3.UseVisualStyleBackColor = true;
    this.buttonOk3.Click += new EventHandler(this.buttonOk3_Click);
    this.buttonCancel2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.buttonCancel2.Location = new Point(443, 8);
    this.buttonCancel2.Name = "buttonCancel2";
    this.buttonCancel2.Size = new Size(121, 27);
    this.buttonCancel2.TabIndex = 4;
    this.buttonCancel2.Text = "Отмена";
    this.buttonCancel2.UseVisualStyleBackColor = true;
    this.buttonCancel2.Click += new EventHandler(this.buttonCancel2_Click);
    this.buttonOk2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.buttonOk2.Location = new Point(292, 8);
    this.buttonOk2.Name = "buttonOk2";
    this.buttonOk2.Size = new Size(121, 27);
    this.buttonOk2.TabIndex = 3;
    this.buttonOk2.Text = "OK";
    this.buttonOk2.UseVisualStyleBackColor = true;
    this.buttonOk2.Click += new EventHandler(this.buttonOk2_Click);
    this.bCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bCancel.DialogResult = DialogResult.Cancel;
    this.bCancel.Location = new Point(443, 8);
    this.bCancel.Name = "bCancel";
    this.bCancel.Size = new Size(121, 27);
    this.bCancel.TabIndex = 2;
    this.bCancel.Text = "Отмена";
    this.bCancel.UseVisualStyleBackColor = true;
    this.bOK.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bOK.DialogResult = DialogResult.OK;
    this.bOK.Enabled = false;
    this.bOK.Location = new Point(292, 8);
    this.bOK.Name = "bOK";
    this.bOK.Size = new Size(121, 27);
    this.bOK.TabIndex = 1;
    this.bOK.Text = "OK";
    this.bOK.UseVisualStyleBackColor = true;
    this.dataGridView1.AllowUserToAddRows = false;
    this.dataGridView1.AllowUserToDeleteRows = false;
    this.dataGridView1.AllowUserToResizeColumns = false;
    this.dataGridView1.AllowUserToResizeRows = false;
    this.dataGridView1.BorderStyle = BorderStyle.Fixed3D;
    gridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
    gridViewCellStyle.BackColor = SystemColors.Control;
    gridViewCellStyle.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    gridViewCellStyle.ForeColor = SystemColors.WindowText;
    gridViewCellStyle.SelectionBackColor = SystemColors.Control;
    gridViewCellStyle.SelectionForeColor = SystemColors.HighlightText;
    gridViewCellStyle.WrapMode = DataGridViewTriState.True;
    this.dataGridView1.ColumnHeadersDefaultCellStyle = gridViewCellStyle;
    this.dataGridView1.ColumnHeadersHeight = 28;
    this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
    this.dataGridView1.Columns.AddRange((DataGridViewColumn) this.ImgColumn, (DataGridViewColumn) this.Column1, (DataGridViewColumn) this.Column2);
    this.dataGridView1.EnableHeadersVisualStyles = false;
    this.dataGridView1.Location = new Point(12, 58);
    this.dataGridView1.MultiSelect = false;
    this.dataGridView1.Name = "dataGridView1";
    this.dataGridView1.RowHeadersVisible = false;
    this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
    this.dataGridView1.Size = new Size(554, 51);
    this.dataGridView1.TabIndex = 14;
    this.dataGridView1.CellEnter += new DataGridViewCellEventHandler(this.dataGridView1_CellEnter);
    this.ImgColumn.HeaderText = "";
    this.ImgColumn.Name = "ImgColumn";
    this.ImgColumn.Width = 20;
    this.Column1.HeaderText = "Обозначение исполнения";
    this.Column1.Name = "Column1";
    this.Column1.ReadOnly = true;
    this.Column1.Resizable = DataGridViewTriState.True;
    this.Column1.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.Column1.Width = 440;
    this.Column2.HeaderText = "Заголовок";
    this.Column2.Name = "Column2";
    this.Column2.ReadOnly = true;
    this.Column2.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.Column2.Width = 90;
    this.buttonAdd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.buttonAdd.Location = new Point(143, 333);
    this.buttonAdd.Name = "buttonAdd";
    this.buttonAdd.Size = new Size(121, 27);
    this.buttonAdd.TabIndex = 15;
    this.buttonAdd.Text = "Добавить";
    this.buttonAdd.UseVisualStyleBackColor = true;
    this.buttonAdd.Click += new EventHandler(this.buttonAdd_Click);
    this.buttonRename.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.buttonRename.Location = new Point(294, 333);
    this.buttonRename.Name = "buttonRename";
    this.buttonRename.Size = new Size(121, 27);
    this.buttonRename.TabIndex = 16 /*0x10*/;
    this.buttonRename.Text = "Переименовать";
    this.buttonRename.UseVisualStyleBackColor = true;
    this.buttonRename.Click += new EventHandler(this.buttonRename_Click);
    this.buttonDelete.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.buttonDelete.Location = new Point(445, 333);
    this.buttonDelete.Name = "buttonDelete";
    this.buttonDelete.Size = new Size(121, 27);
    this.buttonDelete.TabIndex = 17;
    this.buttonDelete.Text = "Удалить";
    this.buttonDelete.UseVisualStyleBackColor = true;
    this.buttonDelete.Click += new EventHandler(this.buttonDelete_Click);
    this.label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.label1.BackColor = SystemColors.Info;
    this.label1.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    this.label1.Location = new Point(12, 4);
    this.label1.Name = "label1";
    this.label1.Size = new Size(555, 53);
    this.label1.TabIndex = 18;
    this.label1.Text = "Выберите исполнение, которое выступит прототипом для создаваемого";
    this.label1.TextAlign = ContentAlignment.TopCenter;
    this.label1.Visible = false;
    this.images.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("images.ImageStream");
    this.images.TransparentColor = Color.Transparent;
    this.images.Images.SetKeyName(0, "");
    this.images.Images.SetKeyName(1, "");
    this.images.Images.SetKeyName(2, "");
    this.images.Images.SetKeyName(3, "");
    this.images.Images.SetKeyName(4, "empty.ico");
    this.images.Images.SetKeyName(5, "copy.ico");
    this.images.Images.SetKeyName(6, "del.ico");
    this.images.Images.SetKeyName(7, "new.ico");
    this.images.Images.SetKeyName(8, "edit.ico");
    this.label3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.label3.BackColor = SystemColors.Info;
    this.label3.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    this.label3.Location = new Point(12, 4);
    this.label3.Name = "label3";
    this.label3.Size = new Size(555, 53);
    this.label3.TabIndex = 19;
    this.label3.Text = "Проверьте номер исполнения";
    this.label3.TextAlign = ContentAlignment.TopCenter;
    this.label3.Visible = false;
    this.label32.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
    this.label32.Location = new Point(9, 126);
    this.label32.Name = "label32";
    this.label32.Size = new Size(557, 22);
    this.label32.TabIndex = 20;
    this.label32.Text = "Без прототипа";
    this.label32.TextAlign = ContentAlignment.TopCenter;
    this.label32.Visible = false;
    this.label33.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
    this.label33.Location = new Point(9, 126);
    this.label33.Name = "label33";
    this.label33.Size = new Size(557, 22);
    this.label33.TabIndex = 21;
    this.label33.Text = "Прототип:";
    this.label33.TextAlign = ContentAlignment.TopCenter;
    this.label33.Visible = false;
    this.label3_NameTemplate.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
    this.label3_NameTemplate.Location = new Point(9, 153);
    this.label3_NameTemplate.Name = "label3_NameTemplate";
    this.label3_NameTemplate.Size = new Size(557, 22);
    this.label3_NameTemplate.TabIndex = 22;
    this.label3_NameTemplate.TextAlign = ContentAlignment.TopCenter;
    this.label3_NameTemplate.Visible = false;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.bCancel;
    this.ClientSize = new Size(578, 411);
    this.Controls.Add((Control) this.label3_NameTemplate);
    this.Controls.Add((Control) this.label33);
    this.Controls.Add((Control) this.label32);
    this.Controls.Add((Control) this.label3);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.buttonDelete);
    this.Controls.Add((Control) this.buttonRename);
    this.Controls.Add((Control) this.buttonAdd);
    this.Controls.Add((Control) this.dataGridView1);
    this.Controls.Add((Control) this.panelForButtons);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (VariablesList);
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Список исполнений";
    this.Load += new EventHandler(this.VariablesList_Load);
    this.panelForButtons.ResumeLayout(false);
    ((ISupportInitialize) this.dataGridView1).EndInit();
    this.ResumeLayout(false);
  }

  private enum Mode_Draw3
  {
    Undefined,
    New,
    Rename,
  }
}
