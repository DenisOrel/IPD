// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Victor.A_Nastr_Avs6Compatibility
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.AVS.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS.Victor;

public class A_Nastr_Avs6Compatibility : Form
{
  private bool isModified;
  private bool isCreate = true;
  private List<Element_Accord_Avs6_Ips> list_Element_Accord_Avs6_Ips_Curr;
  private List<ElDocList> list_ElDocList_Processed_Curc = new List<ElDocList>();
  public int _isAvs6;
  public string _fileIni6 = "";
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button buttonSave1;
  internal Button bCancel;
  internal Button bOK;
  private ToolTip toolTip1;
  private ImageList imageList1;
  private ImageList imagesToolbars;
  private ImageList imageListSort;
  private GroupBox groupBox_Avs6_List_ElDocList;
  private DataGridView dataGridView_Avs6_List_ElDocList;
  private GroupBox groupBox_Avs6_Avs6Docs;
  private ListBox listBox_Avs6_Avs6Docs;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
  private Button button_Delete;
  private Button button_Add;
  private Button button_Copy;
  private Button buttonDefault;
  private Button button_Open;
  private Label label1;
  private Button buttonAvs6_Default;

  public A_Nastr_Avs6Compatibility() => this.InitializeComponent();

  private void A_Nastr_Avs6Compatibility_Load(object sender, EventArgs e)
  {
    bool flag = false;
    if (List_Element_Accord_Avs6_Ips.list_Element_Accord_Avs6_Ips == null || List_Element_Accord_Avs6_Ips.list_Element_Accord_Avs6_Ips.Count == 0)
      flag = List_Element_Accord_Avs6_Ips.Read_From_Base();
    Vedomost_VB_Static.Begin_For_Avs6();
    if (AVS6_From_Avs6Main._isAvs6 == 1)
    {
      this._isAvs6 = 1;
      this._fileIni6 = AVS6_From_Avs6Main._fileIni6;
      List_Element_Accord_Avs6_Ips.fileIni6 = this._fileIni6;
      List_Element_Accord_Avs6_Ips.isAvs6 = this._isAvs6;
    }
    else
    {
      this._fileIni6 = List_Element_Accord_Avs6_Ips.fileIni6;
      this._isAvs6 = List_Element_Accord_Avs6_Ips.isAvs6;
      if (this._isAvs6 == 0)
      {
        Vedomost_VB_Static.Begin_For_Avs6();
      }
      else
      {
        if (this._isAvs6 == 2)
          AVS6_From_Avs6Main.Read_Avs6Main(this._fileIni6);
        if (this._isAvs6 == 3)
          AVS6_From_Avs6Main.Read_Avs6Main("Default");
      }
    }
    this.list_Element_Accord_Avs6_Ips_Curr = !flag ? List_Element_Accord_Avs6_Ips.Default() : List_Element_Accord_Avs6_Ips.List_Element_Accord_Avs6_Ips_Copy();
    this.Draw_All();
    this.Current_Row_dataGriedView(0);
    this.isCreate = false;
  }

  private void Draw_All()
  {
    this.Draw_Buttons();
    this.Draw_Page_Avs6();
  }

  private void Draw_Buttons()
  {
    string str1 = "";
    string str2 = "\r\n";
    switch (this._isAvs6)
    {
      case 0:
        this.buttonAvs6_Default.Visible = true;
        this.button_Open.Visible = true;
        str1 = AVS6_From_Avs6Main.sTextError;
        break;
      case 1:
        this.buttonAvs6_Default.Visible = false;
        this.button_Open.Visible = false;
        str1 = this._fileIni6;
        if (AVS6_From_Avs6Main.sTextError != "")
        {
          this.buttonAvs6_Default.Visible = true;
          this.button_Open.Visible = true;
          str1 = str1 + str2 + AVS6_From_Avs6Main.sTextError;
          break;
        }
        break;
      case 2:
        this.buttonAvs6_Default.Visible = true;
        this.button_Open.Visible = true;
        str1 = this._fileIni6;
        if (AVS6_From_Avs6Main.sTextError != "")
        {
          str1 = str1 + str2 + AVS6_From_Avs6Main.sTextError;
          break;
        }
        break;
      case 3:
        this.buttonAvs6_Default.Visible = true;
        this.button_Open.Visible = true;
        str1 = "По умолчанию";
        break;
    }
    this.label1.Text = str1;
  }

  /// <summary>
  /// Рисуется страница
  /// Выделил на случай если появятся страницы
  /// </summary>
  private void Draw_Page_Avs6()
  {
    this.Draw_ListBox_Avs6_Avs6Docs();
    this.Draw_dataGridView_Avs6_List_ElDocList();
  }

  /// <summary> Левый верхний/ </summary>
  private void Draw_ListBox_Avs6_Avs6Docs()
  {
    this.list_ElDocList_Processed_Curc.Clear();
    this.listBox_Avs6_Avs6Docs.Items.Clear();
    if (AVS6_From_Avs6Main._list_ElDocList_Processed != null)
    {
      for (int index1 = 0; index1 < AVS6_From_Avs6Main._list_ElDocList_Processed.Count; ++index1)
      {
        bool flag = false;
        ElDocList elDocList = AVS6_From_Avs6Main._list_ElDocList_Processed[index1];
        for (int index2 = 0; index2 < this.list_Element_Accord_Avs6_Ips_Curr.Count; ++index2)
        {
          if (this.list_Element_Accord_Avs6_Ips_Curr[index2].Avs6_Comment == elDocList.Title())
          {
            flag = true;
            break;
          }
        }
        if (!flag)
          this.list_ElDocList_Processed_Curc.Add(elDocList);
      }
    }
    this.listBox_Avs6_Avs6Docs.Items.Clear();
    if (this.list_ElDocList_Processed_Curc == null)
      return;
    for (int index = 0; index < this.list_ElDocList_Processed_Curc.Count; ++index)
      this.listBox_Avs6_Avs6Docs.Items.Add((object) this.list_ElDocList_Processed_Curc[index]._title);
    if (this.listBox_Avs6_Avs6Docs.Items.Count <= 0)
      return;
    this.listBox_Avs6_Avs6Docs.SelectedIndex = 0;
  }

  /// <summary> Отображение связей СПРАВА </summary>
  private void Draw_dataGridView_Avs6_List_ElDocList()
  {
    this.dataGridView_Avs6_List_ElDocList.Rows.Clear();
    if (this.list_Element_Accord_Avs6_Ips_Curr == null || this.list_Element_Accord_Avs6_Ips_Curr.Count <= 0 || this.list_Element_Accord_Avs6_Ips_Curr == null)
      return;
    for (int index = 0; index < this.list_Element_Accord_Avs6_Ips_Curr.Count; ++index)
    {
      Element_Accord_Avs6_Ips elementAccordAvs6Ips = this.list_Element_Accord_Avs6_Ips_Curr[index];
      DataGridViewRow dataGridViewRow = (DataGridViewRow) this.dataGridView_Avs6_List_ElDocList.Rows[0].Clone();
      dataGridViewRow.Cells[0].Value = (object) elementAccordAvs6Ips.Avs6_Comment;
      dataGridViewRow.Cells[1].Value = (object) elementAccordAvs6Ips.Ips_Ims_ObjectName;
      this.dataGridView_Avs6_List_ElDocList.Rows.Add(dataGridViewRow);
    }
    if (this.dataGridView_Avs6_List_ElDocList.Rows.Count <= 1)
      return;
    this.dataGridView_Avs6_List_ElDocList.Rows[0].Selected = true;
    this.dataGridView_Avs6_List_ElDocList.RowsDefaultCellStyle.SelectionForeColor = Color.White;
    if (this.dataGridView_Avs6_List_ElDocList.Rows[0].Cells[0].Value.ToString() == "")
    {
      this.button_Add.Enabled = true;
      this.button_Delete.Enabled = false;
    }
    else
    {
      this.button_Add.Enabled = false;
      this.button_Delete.Enabled = true;
    }
  }

  /// <summary> При выборе строки </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void dataGridView_Avs6_List_ElDocList_CellMouseClick(
    object sender,
    DataGridViewCellMouseEventArgs e)
  {
    int rowIndex = this.dataGridView_Avs6_List_ElDocList.CurrentCell.RowIndex;
    this.dataGridView_Avs6_List_ElDocList.Rows[rowIndex].Selected = true;
    this.dataGridView_Avs6_List_ElDocList.RowsDefaultCellStyle.SelectionForeColor = Color.White;
    this.Current_Row_dataGriedView(rowIndex);
  }

  /// <summary> Действия для конкретной строки таблицы </summary>
  /// <param name="index"></param>
  private void Current_Row_dataGriedView(int index)
  {
    if (this.dataGridView_Avs6_List_ElDocList.Rows.Count <= 1)
      return;
    if (this.dataGridView_Avs6_List_ElDocList.Rows[index].Cells[0].Value == null || this.dataGridView_Avs6_List_ElDocList.Rows[index].Cells[0].Value.ToString() == "")
    {
      this.button_Add.Enabled = true;
      this.button_Delete.Enabled = false;
      if (this.Compare_TypeDoc())
        return;
      this.button_Add.Enabled = false;
    }
    else
    {
      this.button_Add.Enabled = false;
      this.button_Delete.Enabled = true;
    }
  }

  /// <summary> При изменении текущей строки слева </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void dataGridView_Avs6_List_ElDocList_SelectionChanged(object sender, EventArgs e)
  {
    if (this.isCreate || this.dataGridView_Avs6_List_ElDocList.Rows.Count <= 1)
      return;
    this.Current_Row_dataGriedView(this.dataGridView_Avs6_List_ElDocList.CurrentCell.RowIndex);
  }

  /// <summary> При изменении текущей строки справа </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void listBox_Avs6_Avs6Docs_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this.isCreate)
      return;
    this.Current_Row_dataGriedView(this.dataGridView_Avs6_List_ElDocList.CurrentCell.RowIndex);
  }

  /// <summary> Сравнение двух строк на совпадение типов документов </summary>
  /// <returns></returns>
  private bool Compare_TypeDoc()
  {
    int rowIndex = this.dataGridView_Avs6_List_ElDocList.CurrentCell.RowIndex;
    if (rowIndex >= this.list_Element_Accord_Avs6_Ips_Curr.Count)
      return false;
    Element_Accord_Avs6_Ips elementAccordAvs6Ips = this.list_Element_Accord_Avs6_Ips_Curr[rowIndex];
    int selectedIndex = this.listBox_Avs6_Avs6Docs.SelectedIndex;
    if (selectedIndex <= -1)
      return false;
    ElDocList elDocList = this.list_ElDocList_Processed_Curc[selectedIndex];
    return elementAccordAvs6Ips.TypeDoc == elDocList.typeDoc;
  }

  /// <summary> Открыть или спрятать кнопки Save </summary>
  private void ModifiedAll()
  {
    if (this.isCreate)
      this.buttonSave1.Enabled = false;
    else if (this.isModified)
      this.buttonSave1.Enabled = true;
    else
      this.buttonSave1.Enabled = false;
  }

  /// <summary> Попытка закрытия диалога </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void A_Nastr_Avs6Compatibility_FormClosing(object sender, FormClosingEventArgs e)
  {
    if (this.isModified)
    {
      int num = (int) MessageBox.Show("Параметры настройки изменены\r\n\r\nСохранить?", "Внимание!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
      if (num == 6)
      {
        this.Save();
        e.Cancel = false;
      }
      if (num == 7)
      {
        this.ModifiedAll();
        e.Cancel = false;
      }
      if (num != 2)
        return;
      e.Cancel = true;
    }
    else
      e.Cancel = false;
  }

  /// <summary> Кнопка OK </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void bOK_Click(object sender, EventArgs e)
  {
    if (this.isModified)
      this.Save();
    this.Close();
  }

  /// <summary> Кнопка По умолчанию ПРАВАЯ </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void buttonDefault_Click(object sender, EventArgs e)
  {
    this.isCreate = true;
    this.list_Element_Accord_Avs6_Ips_Curr.Clear();
    this.list_Element_Accord_Avs6_Ips_Curr = List_Element_Accord_Avs6_Ips.Default();
    this.Draw_All();
    this.isCreate = false;
    this.isModified = true;
    this.ModifiedAll();
  }

  /// <summary> Кнопка Save </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void buttonSave1_Click(object sender, EventArgs e) => this.Save();

  /// <summary> Сохранение </summary>
  private void Save()
  {
    List_Element_Accord_Avs6_Ips.list_Element_Accord_Avs6_Ips.Clear();
    for (int index = 0; index < this.list_Element_Accord_Avs6_Ips_Curr.Count; ++index)
    {
      Element_Accord_Avs6_Ips elementAccordAvs6Ips = this.list_Element_Accord_Avs6_Ips_Curr[index];
      List_Element_Accord_Avs6_Ips.list_Element_Accord_Avs6_Ips.Add(new Element_Accord_Avs6_Ips()
      {
        Avs6_Comment = elementAccordAvs6Ips.Avs6_Comment,
        Avs6_FileType = elementAccordAvs6Ips.Avs6_FileType,
        Avs6_GuidSysnumber = elementAccordAvs6Ips.Avs6_GuidSysnumber,
        Avs6_Sysnumber = elementAccordAvs6Ips.Avs6_Sysnumber,
        Ips_Ims_Guid = elementAccordAvs6Ips.Ips_Ims_Guid,
        Ips_Ims_ObjectName = elementAccordAvs6Ips.Ips_Ims_ObjectName,
        TypeDoc = elementAccordAvs6Ips.TypeDoc
      });
    }
    List_Element_Accord_Avs6_Ips.isAvs6 = this._isAvs6;
    List_Element_Accord_Avs6_Ips.fileIni6 = this._fileIni6;
    List_Element_Accord_Avs6_Ips.Write_To_Base();
    this.isModified = false;
    this.ModifiedAll();
  }

  /// <summary> Кнопка ЗАНЕСТИ вправо </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void button_Add_Click(object sender, EventArgs e)
  {
    if (this.listBox_Avs6_Avs6Docs.SelectedIndex < 0)
      return;
    int selectedIndex = this.listBox_Avs6_Avs6Docs.SelectedIndex;
    ElDocList elDocList = this.list_ElDocList_Processed_Curc[selectedIndex];
    int rowIndex = this.dataGridView_Avs6_List_ElDocList.CurrentCell.RowIndex;
    Element_Accord_Avs6_Ips elementAccordAvs6Ips = this.list_Element_Accord_Avs6_Ips_Curr[rowIndex];
    elementAccordAvs6Ips.Avs6_Comment = elDocList._title;
    elementAccordAvs6Ips.Avs6_FileType = elDocList._fileType;
    elementAccordAvs6Ips.Avs6_GuidSysnumber = elDocList._guidSysNumber;
    elementAccordAvs6Ips.Avs6_Sysnumber = elDocList._sysNumber;
    this.dataGridView_Avs6_List_ElDocList.Rows[rowIndex].Cells[0].Value = (object) elementAccordAvs6Ips.Avs6_Comment;
    this.list_ElDocList_Processed_Curc.RemoveAt(selectedIndex);
    this.listBox_Avs6_Avs6Docs.Items.RemoveAt(selectedIndex);
    this.Current_Row_dataGriedView(rowIndex);
    if (selectedIndex > 0)
      --selectedIndex;
    if (selectedIndex > -1)
    {
      if (this.listBox_Avs6_Avs6Docs.Items.Count > selectedIndex)
        this.listBox_Avs6_Avs6Docs.SelectedIndex = selectedIndex;
      else
        this.listBox_Avs6_Avs6Docs.SelectedIndex = -1;
    }
    this.isModified = true;
    this.ModifiedAll();
  }

  private void button_Copy_Click(object sender, EventArgs e)
  {
    int rowIndex = this.dataGridView_Avs6_List_ElDocList.CurrentCell.RowIndex;
    Element_Accord_Avs6_Ips elementAccordAvs6Ips = this.list_Element_Accord_Avs6_Ips_Curr[rowIndex];
    this.list_Element_Accord_Avs6_Ips_Curr.Insert(rowIndex + 1, new Element_Accord_Avs6_Ips()
    {
      Ips_Ims_Guid = elementAccordAvs6Ips.Ips_Ims_Guid,
      Ips_Ims_ObjectName = elementAccordAvs6Ips.Ips_Ims_ObjectName,
      TypeDoc = elementAccordAvs6Ips.TypeDoc,
      Avs6_Comment = "",
      Avs6_FileType = "",
      Avs6_GuidSysnumber = "",
      Avs6_Sysnumber = -1
    });
    this.Draw_dataGridView_Avs6_List_ElDocList();
    int index = rowIndex + 1;
    this.dataGridView_Avs6_List_ElDocList.CurrentCell = this.dataGridView_Avs6_List_ElDocList.Rows[index].Cells[0];
    this.dataGridView_Avs6_List_ElDocList.Rows[0].Selected = false;
    this.dataGridView_Avs6_List_ElDocList.Rows[index].Selected = true;
    this.isModified = true;
    this.ModifiedAll();
  }

  /// <summary> Кнопка УДАЛИТЬ влево </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void button_Delete_Click(object sender, EventArgs e)
  {
    int rowIndex = this.dataGridView_Avs6_List_ElDocList.CurrentCell.RowIndex;
    this.dataGridView_Avs6_List_ElDocList.Rows[rowIndex].Cells[0].Value = (object) "";
    Element_Accord_Avs6_Ips elementAccordAvs6Ips = this.list_Element_Accord_Avs6_Ips_Curr[rowIndex];
    for (int index = 0; index < AVS6_From_Avs6Main._list_ElDocList_Processed.Count; ++index)
    {
      ElDocList elDocList = AVS6_From_Avs6Main._list_ElDocList_Processed[index];
      if (elDocList.Title() == elementAccordAvs6Ips.Avs6_Comment)
      {
        this.list_ElDocList_Processed_Curc.Add(elDocList.Copy());
        this.listBox_Avs6_Avs6Docs.Items.Add((object) elDocList.Title());
        break;
      }
    }
    elementAccordAvs6Ips.Avs6_Comment = "";
    elementAccordAvs6Ips.Avs6_FileType = "";
    elementAccordAvs6Ips.Avs6_GuidSysnumber = "";
    elementAccordAvs6Ips.Avs6_Sysnumber = -1;
    this.Current_Row_dataGriedView(rowIndex);
    this.isModified = true;
    this.ModifiedAll();
  }

  private void panelForButtons_MouseClick(object sender, MouseEventArgs e)
  {
    if ((Control.ModifierKeys & Keys.Control) != Keys.Control || (Control.ModifierKeys & Keys.Alt) != Keys.Alt)
      return;
    Vedomost_VB_Static.isHozain = true;
    int num = (int) MessageBox.Show("Слушаю и повинуюсь", "ПРИВЕТ!");
  }

  private void button_Open_Click(object sender, EventArgs e)
  {
    OpenFileDialog openFileDialog = new OpenFileDialog();
    openFileDialog.RestoreDirectory = true;
    openFileDialog.Filter = "Ini файлы (*.ini)|*.ini";
    openFileDialog.DefaultExt = "ini";
    if (openFileDialog.ShowDialog() == DialogResult.OK)
    {
      this._fileIni6 = openFileDialog.FileName;
      this.listBox_Avs6_Avs6Docs.Items.Clear();
      this.list_ElDocList_Processed_Curc.Clear();
      AVS6_From_Avs6Main._list_ElDocList_Processed.Clear();
      AVS6_From_Avs6Main._list_ElDocList.Clear();
      AVS6_From_Avs6Main._list_recordFields.Clear();
      AVS6_From_Avs6Main._isAvs6 = 0;
      this._isAvs6 = 0;
      AVS6_From_Avs6Main._fileIni6 = this._fileIni6;
      if (AVS6_From_Avs6Main.Read_Avs6Main(this._fileIni6))
      {
        this._isAvs6 = 2;
        AVS6_From_Avs6Main._isAvs6 = 2;
        this.Draw_All();
        this.Draw_Buttons();
      }
      else
      {
        this.Draw_Buttons();
        this.label1.Text = "Ошибка";
      }
    }
    this.isModified = true;
    this.ModifiedAll();
  }

  private void buttonAvs6_Default_Click(object sender, EventArgs e)
  {
    AVS6_From_Avs6Main.Read_Avs6Main("Default");
    this._isAvs6 = 3;
    AVS6_From_Avs6Main._isAvs6 = 3;
    this.isModified = true;
    this.ModifiedAll();
    this.Draw_All();
  }

  private void A_Nastr_Avs6Compatibility_Click(object sender, EventArgs e)
  {
    if ((Control.ModifierKeys & Keys.Control) != Keys.Control || (Control.ModifierKeys & Keys.Alt) != Keys.Alt)
      return;
    Vedomost_VB_Static.isHozain = true;
    int num = (int) MessageBox.Show("Слушаю и повинуюсь", "ПРИВЕТ!");
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (A_Nastr_Avs6Compatibility));
    DataGridViewCellStyle gridViewCellStyle1 = new DataGridViewCellStyle();
    DataGridViewCellStyle gridViewCellStyle2 = new DataGridViewCellStyle();
    DataGridViewCellStyle gridViewCellStyle3 = new DataGridViewCellStyle();
    this.buttonSave1 = new Button();
    this.bCancel = new Button();
    this.bOK = new Button();
    this.toolTip1 = new ToolTip(this.components);
    this.listBox_Avs6_Avs6Docs = new ListBox();
    this.buttonDefault = new Button();
    this.button_Open = new Button();
    this.buttonAvs6_Default = new Button();
    this.button_Copy = new Button();
    this.button_Delete = new Button();
    this.button_Add = new Button();
    this.dataGridView_Avs6_List_ElDocList = new DataGridView();
    this.dataGridViewTextBoxColumn12 = new DataGridViewTextBoxColumn();
    this.dataGridViewTextBoxColumn13 = new DataGridViewTextBoxColumn();
    this.imageList1 = new ImageList(this.components);
    this.imagesToolbars = new ImageList(this.components);
    this.imageListSort = new ImageList(this.components);
    this.groupBox_Avs6_List_ElDocList = new GroupBox();
    this.groupBox_Avs6_Avs6Docs = new GroupBox();
    this.label1 = new Label();
    ((ISupportInitialize) this.dataGridView_Avs6_List_ElDocList).BeginInit();
    this.groupBox_Avs6_List_ElDocList.SuspendLayout();
    this.groupBox_Avs6_Avs6Docs.SuspendLayout();
    this.SuspendLayout();
    this.buttonSave1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.buttonSave1.Enabled = false;
    this.buttonSave1.Location = new Point(709, 386);
    this.buttonSave1.Name = "buttonSave1";
    this.buttonSave1.Size = new Size(109, 27);
    this.buttonSave1.TabIndex = 4;
    this.buttonSave1.Text = "Сохранить";
    this.toolTip1.SetToolTip((Control) this.buttonSave1, "Сохранить результаты настройки. Диалог настройки не закрывать");
    this.buttonSave1.UseVisualStyleBackColor = true;
    this.buttonSave1.Click += new EventHandler(this.buttonSave1_Click);
    this.bCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bCancel.DialogResult = DialogResult.Cancel;
    this.bCancel.Location = new Point(1003, 386);
    this.bCancel.Name = "bCancel";
    this.bCancel.Size = new Size(109, 27);
    this.bCancel.TabIndex = 2;
    this.bCancel.Text = "Закрыть";
    this.toolTip1.SetToolTip((Control) this.bCancel, "Закрыть диалог настройки без сохранения результатов настройки");
    this.bCancel.UseVisualStyleBackColor = true;
    this.bOK.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bOK.DialogResult = DialogResult.OK;
    this.bOK.Location = new Point(861, 386);
    this.bOK.Name = "bOK";
    this.bOK.Size = new Size(109, 27);
    this.bOK.TabIndex = 1;
    this.bOK.Text = "OK";
    this.toolTip1.SetToolTip((Control) this.bOK, "Сохранить результаты настройки и закрыть диалог настройки");
    this.bOK.UseVisualStyleBackColor = true;
    this.bOK.Click += new EventHandler(this.bOK_Click);
    this.toolTip1.AutomaticDelay = 2000;
    this.toolTip1.AutoPopDelay = 2000;
    this.toolTip1.InitialDelay = 2000;
    this.toolTip1.IsBalloon = true;
    this.toolTip1.OwnerDraw = true;
    this.toolTip1.ReshowDelay = 1000;
    this.toolTip1.ToolTipIcon = ToolTipIcon.Info;
    this.toolTip1.ToolTipTitle = "Подсказка";
    this.listBox_Avs6_Avs6Docs.Dock = DockStyle.Fill;
    this.listBox_Avs6_Avs6Docs.FormattingEnabled = true;
    this.listBox_Avs6_Avs6Docs.Location = new Point(3, 16 /*0x10*/);
    this.listBox_Avs6_Avs6Docs.Name = "listBox_Avs6_Avs6Docs";
    this.listBox_Avs6_Avs6Docs.Size = new Size(319, 305);
    this.listBox_Avs6_Avs6Docs.TabIndex = 0;
    this.toolTip1.SetToolTip((Control) this.listBox_Avs6_Avs6Docs, "Список документов Avs6, для которых не настроено соответствие в Ips");
    this.listBox_Avs6_Avs6Docs.SelectedIndexChanged += new EventHandler(this.listBox_Avs6_Avs6Docs_SelectedIndexChanged);
    this.buttonDefault.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.buttonDefault.Location = new Point(459, 386);
    this.buttonDefault.Name = "buttonDefault";
    this.buttonDefault.Size = new Size(109, 27);
    this.buttonDefault.TabIndex = 19;
    this.buttonDefault.Text = "По умолчанию";
    this.toolTip1.SetToolTip((Control) this.buttonDefault, "Параметры соответствия документов настроить \"по умолчанию\"");
    this.buttonDefault.UseVisualStyleBackColor = true;
    this.buttonDefault.Click += new EventHandler(this.buttonDefault_Click);
    this.button_Open.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.button_Open.Location = new Point(222, 386);
    this.button_Open.Name = "button_Open";
    this.button_Open.Size = new Size(109, 27);
    this.button_Open.TabIndex = 21;
    this.button_Open.Text = "Открыть";
    this.toolTip1.SetToolTip((Control) this.button_Open, "Настройки Avs6 читать из файла");
    this.button_Open.UseVisualStyleBackColor = true;
    this.button_Open.Visible = false;
    this.button_Open.Click += new EventHandler(this.button_Open_Click);
    this.buttonAvs6_Default.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.buttonAvs6_Default.Location = new Point(40, 386);
    this.buttonAvs6_Default.Name = "buttonAvs6_Default";
    this.buttonAvs6_Default.Size = new Size(109, 27);
    this.buttonAvs6_Default.TabIndex = 24;
    this.buttonAvs6_Default.Text = "Программа";
    this.toolTip1.SetToolTip((Control) this.buttonAvs6_Default, "Настройки Avs6 читать из файла");
    this.buttonAvs6_Default.UseVisualStyleBackColor = true;
    this.buttonAvs6_Default.Visible = false;
    this.buttonAvs6_Default.Click += new EventHandler(this.buttonAvs6_Default_Click);
    this.button_Copy.Image = (Image) componentResourceManager.GetObject("button_Copy.Image");
    this.button_Copy.Location = new Point(379, 153);
    this.button_Copy.Name = "button_Copy";
    this.button_Copy.Size = new Size(39, 23);
    this.button_Copy.TabIndex = 18;
    this.toolTip1.SetToolTip((Control) this.button_Copy, "Для документа Ips (справа) создать копию строки");
    this.button_Copy.UseVisualStyleBackColor = true;
    this.button_Copy.Click += new EventHandler(this.button_Copy_Click);
    this.button_Delete.Image = (Image) Resources.arrow_left_green;
    this.button_Delete.Location = new Point(379, 61);
    this.button_Delete.Name = "button_Delete";
    this.button_Delete.Size = new Size(39, 23);
    this.button_Delete.TabIndex = 17;
    this.toolTip1.SetToolTip((Control) this.button_Delete, "Для документа Ips (справа) удалить связь с документом Avs6");
    this.button_Delete.UseVisualStyleBackColor = true;
    this.button_Delete.Click += new EventHandler(this.button_Delete_Click);
    this.button_Add.Image = (Image) Resources.arrow_right_green;
    this.button_Add.Location = new Point(379, 28);
    this.button_Add.Name = "button_Add";
    this.button_Add.Size = new Size(39, 23);
    this.button_Add.TabIndex = 16 /*0x10*/;
    this.toolTip1.SetToolTip((Control) this.button_Add, "Текущий документ Avs6 связать с текущим документом Ips");
    this.button_Add.UseVisualStyleBackColor = true;
    this.button_Add.Click += new EventHandler(this.button_Add_Click);
    this.dataGridView_Avs6_List_ElDocList.AllowUserToResizeColumns = false;
    this.dataGridView_Avs6_List_ElDocList.AllowUserToResizeRows = false;
    gridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
    gridViewCellStyle1.BackColor = SystemColors.Control;
    gridViewCellStyle1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    gridViewCellStyle1.ForeColor = SystemColors.WindowText;
    gridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
    gridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
    gridViewCellStyle1.WrapMode = DataGridViewTriState.True;
    this.dataGridView_Avs6_List_ElDocList.ColumnHeadersDefaultCellStyle = gridViewCellStyle1;
    this.dataGridView_Avs6_List_ElDocList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
    this.dataGridView_Avs6_List_ElDocList.Columns.AddRange((DataGridViewColumn) this.dataGridViewTextBoxColumn12, (DataGridViewColumn) this.dataGridViewTextBoxColumn13);
    gridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
    gridViewCellStyle2.BackColor = SystemColors.Window;
    gridViewCellStyle2.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    gridViewCellStyle2.ForeColor = SystemColors.ControlText;
    gridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
    gridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
    gridViewCellStyle2.WrapMode = DataGridViewTriState.False;
    this.dataGridView_Avs6_List_ElDocList.DefaultCellStyle = gridViewCellStyle2;
    this.dataGridView_Avs6_List_ElDocList.Dock = DockStyle.Fill;
    this.dataGridView_Avs6_List_ElDocList.EditMode = DataGridViewEditMode.EditProgrammatically;
    this.dataGridView_Avs6_List_ElDocList.EnableHeadersVisualStyles = false;
    this.dataGridView_Avs6_List_ElDocList.Location = new Point(3, 16 /*0x10*/);
    this.dataGridView_Avs6_List_ElDocList.Name = "dataGridView_Avs6_List_ElDocList";
    gridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
    gridViewCellStyle3.BackColor = SystemColors.Control;
    gridViewCellStyle3.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    gridViewCellStyle3.ForeColor = SystemColors.WindowText;
    gridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
    gridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
    gridViewCellStyle3.WrapMode = DataGridViewTriState.True;
    this.dataGridView_Avs6_List_ElDocList.RowHeadersDefaultCellStyle = gridViewCellStyle3;
    this.dataGridView_Avs6_List_ElDocList.RowHeadersWidth = 30;
    this.dataGridView_Avs6_List_ElDocList.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
    this.dataGridView_Avs6_List_ElDocList.Size = new Size(664, 339);
    this.dataGridView_Avs6_List_ElDocList.StandardTab = true;
    this.dataGridView_Avs6_List_ElDocList.TabIndex = 2;
    this.dataGridView_Avs6_List_ElDocList.CellMouseClick += new DataGridViewCellMouseEventHandler(this.dataGridView_Avs6_List_ElDocList_CellMouseClick);
    this.dataGridView_Avs6_List_ElDocList.SelectionChanged += new EventHandler(this.dataGridView_Avs6_List_ElDocList_SelectionChanged);
    this.dataGridViewTextBoxColumn12.HeaderText = "Документ Avs6";
    this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
    this.dataGridViewTextBoxColumn12.Resizable = DataGridViewTriState.False;
    this.dataGridViewTextBoxColumn12.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.dataGridViewTextBoxColumn12.Width = 292;
    this.dataGridViewTextBoxColumn13.HeaderText = "Документ Ips";
    this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
    this.dataGridViewTextBoxColumn13.Resizable = DataGridViewTriState.False;
    this.dataGridViewTextBoxColumn13.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.dataGridViewTextBoxColumn13.Width = 292;
    this.imageList1.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imageList1.ImageStream");
    this.imageList1.TransparentColor = Color.Transparent;
    this.imageList1.Images.SetKeyName(0, "Not.ico");
    this.imagesToolbars.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imagesToolbars.ImageStream");
    this.imagesToolbars.TransparentColor = Color.Transparent;
    this.imagesToolbars.Images.SetKeyName(0, "arrow_right_blue.ico");
    this.imagesToolbars.Images.SetKeyName(1, "");
    this.imagesToolbars.Images.SetKeyName(2, "");
    this.imagesToolbars.Images.SetKeyName(3, "");
    this.imagesToolbars.Images.SetKeyName(4, "");
    this.imagesToolbars.Images.SetKeyName(5, "");
    this.imagesToolbars.Images.SetKeyName(6, "");
    this.imagesToolbars.Images.SetKeyName(7, "Связь.ico");
    this.imagesToolbars.Images.SetKeyName(8, "object_16x16.ico");
    this.imagesToolbars.Images.SetKeyName(9, "WithoutDrawing.ico");
    this.imageListSort.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imageListSort.ImageStream");
    this.imageListSort.TransparentColor = Color.Transparent;
    this.imageListSort.Images.SetKeyName(0, "");
    this.imageListSort.Images.SetKeyName(1, "");
    this.imageListSort.Images.SetKeyName(2, "");
    this.groupBox_Avs6_List_ElDocList.Controls.Add((Control) this.dataGridView_Avs6_List_ElDocList);
    this.groupBox_Avs6_List_ElDocList.Location = new Point(442, 12);
    this.groupBox_Avs6_List_ElDocList.Name = "groupBox_Avs6_List_ElDocList";
    this.groupBox_Avs6_List_ElDocList.Size = new Size(670, 358);
    this.groupBox_Avs6_List_ElDocList.TabIndex = 15;
    this.groupBox_Avs6_List_ElDocList.TabStop = false;
    this.groupBox_Avs6_List_ElDocList.Text = "Соответствие документов";
    this.groupBox_Avs6_Avs6Docs.Controls.Add((Control) this.listBox_Avs6_Avs6Docs);
    this.groupBox_Avs6_Avs6Docs.Location = new Point(28, 12);
    this.groupBox_Avs6_Avs6Docs.Name = "groupBox_Avs6_Avs6Docs";
    this.groupBox_Avs6_Avs6Docs.Size = new Size(325, 324);
    this.groupBox_Avs6_Avs6Docs.TabIndex = 13;
    this.groupBox_Avs6_Avs6Docs.TabStop = false;
    this.groupBox_Avs6_Avs6Docs.Text = "Документы Avs6";
    this.label1.AllowDrop = true;
    this.label1.Location = new Point(34, 348);
    this.label1.Name = "label1";
    this.label1.Size = new Size(316, 35);
    this.label1.TabIndex = 23;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.BackColor = Color.LightYellow;
    this.CancelButton = (IButtonControl) this.bCancel;
    this.ClientSize = new Size(1135, 425);
    this.Controls.Add((Control) this.buttonAvs6_Default);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.button_Open);
    this.Controls.Add((Control) this.bCancel);
    this.Controls.Add((Control) this.buttonSave1);
    this.Controls.Add((Control) this.bOK);
    this.Controls.Add((Control) this.buttonDefault);
    this.Controls.Add((Control) this.button_Copy);
    this.Controls.Add((Control) this.button_Delete);
    this.Controls.Add((Control) this.button_Add);
    this.Controls.Add((Control) this.groupBox_Avs6_List_ElDocList);
    this.Controls.Add((Control) this.groupBox_Avs6_Avs6Docs);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (A_Nastr_Avs6Compatibility);
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Настройка чтения документов Avs6";
    this.FormClosing += new FormClosingEventHandler(this.A_Nastr_Avs6Compatibility_FormClosing);
    this.Load += new EventHandler(this.A_Nastr_Avs6Compatibility_Load);
    this.Click += new EventHandler(this.A_Nastr_Avs6Compatibility_Click);
    ((ISupportInitialize) this.dataGridView_Avs6_List_ElDocList).EndInit();
    this.groupBox_Avs6_List_ElDocList.ResumeLayout(false);
    this.groupBox_Avs6_Avs6Docs.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
