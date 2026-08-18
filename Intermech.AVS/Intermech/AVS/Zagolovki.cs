// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Zagolovki
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.AVS.Properties;
using Intermech.AVS.Victor;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

#nullable disable
namespace Intermech.AVS;

public class Zagolovki : Form
{
  public Guid _guidTemplateVed;
  public Guid _guidTypeVed;
  public string _documentName;
  public IMSObjectType imsObjectTypeCurr;
  public One_Ved_Nastr _one_Ved_Nastr;
  private Vedomost_VB.Zagolovki_Ved zagolovki_Ved_Curr;
  private bool _isModified;
  private bool secondModified;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel panelButtons;
  private Button bCancel;
  private Button bOK;
  private Button buttonSave;
  private Button buttonDefault;
  private GroupBox groupBox_ListZagolovkov;
  private DataGridView dataGridView_ListZagolovkov;
  private Label label_Zagolovki_SpravaVnizu;
  private Button button_Zagolovki_Add;
  private Button buttonDelete_Zagolovki;
  private ToolTip toolTip1;
  private DataGridViewTextBoxColumn Column1;
  private DataGridViewTextBoxColumn Column2;
  private Label label_Zagolovki_SlevaVverhu;
  private Label label_Zagolovki_Attribut;
  private GroupBox groupBox_Zagolovki_AtributeControl1;
  private SelectAvsAttributeControl select_Zagolovki_AttributeControl1;
  private ImageList imageList1;
  private Button button_Zagolovki_EditKeyAtribut;
  private GroupBox groupBox_Zagolovki_AttribVedRec1;
  private ListBox listBox_Zagolovki_AttribVedRec;
  private Label label_NoZgolovki;
  private Button button_Zagolovki_FromList;
  private Button buttonCopyFrom;

  public Zagolovki() => this.InitializeComponent();

  /// <summary> Первоначальная загрузка окна </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void Zagolovki_Load(object sender, EventArgs e)
  {
    XmlDocument xmlDocument = Vedomost_VB_Static.ReadXmlNastrFromBase(this._guidTemplateVed);
    if (this._guidTemplateVed == Guid.Empty)
    {
      this.Close();
    }
    else
    {
      if (xmlDocument == null)
      {
        if (this._guidTemplateVed != Vedomost_VB_Static.GuidTemplateVS && this._guidTemplateVed != Vedomost_VB_Static.GuidTemplateVP && this._guidTemplateVed != Vedomost_VB_Static.GuidTemplateRS && this._guidTemplateVed != Vedomost_VB_Static.GuidTemplateVSI && this._guidTemplateVed != Vedomost_VB_Static.GuidTemplateVD && this._guidTemplateVed != Vedomost_VB_Static.GuidTemplateVDE && this._guidTemplateVed != Vedomost_VB_Static.GuidTemplateDP && this._guidTemplateVed != Vedomost_VB_Static.GuidTemplateDPE)
        {
          this.Close();
          return;
        }
        this._one_Ved_Nastr = Vedomost_VB_Static.Ved_Nastr_Init(Guid.Empty, this._guidTemplateVed, false);
        if (this._one_Ved_Nastr == null)
        {
          this.Close();
          return;
        }
        this.secondModified = true;
      }
      else
      {
        this._one_Ved_Nastr = new One_Ved_Nastr();
        this._one_Ved_Nastr.Filled_One_Ved_Nastr_FromXml(xmlDocument);
      }
      if (this._one_Ved_Nastr._typeCreate == Vedomost_VB.TypeCreate.Undefined)
        this.buttonDefault.Visible = false;
      this.imsObjectTypeCurr = MetaDataHelper.GetObjectType(this._one_Ved_Nastr._guidTypeVed);
      this._one_Ved_Nastr._imsObjectType = this.imsObjectTypeCurr;
      this._one_Ved_Nastr._vedomostTemplateObjectGuid = this._guidTemplateVed;
      this.Text = $"{this.Text} [{this._one_Ved_Nastr._imsObjectType.ObjectName}]";
      if (this._one_Ved_Nastr._dateIni != "")
        this.Text = $"{this.Text} {this._one_Ved_Nastr._dateIni}";
      this.zagolovki_Ved_Curr = Vedomost_VB_Static.Zagolovki_Ved_Copy(this._one_Ved_Nastr._zagolovki_Ved);
      this.select_Zagolovki_AttributeControl1.Select((NodeColumnCollection) null, (List<AVSColumnScheme>) null);
      this.listBox_Zagolovki_AttribVedRec.SelectedIndex = -1;
      this.ListBoxAttribVedRec_Filled();
      this.ListZagolovkov_draw();
      if (Vedomost_VB_Static.List_Ved_OpisanieVed == null)
        Vedomost_VB_Static.List_Ved_OpisanieVed_Create();
      this.Modified(this.secondModified);
    }
  }

  /// <summary> Отрисовка списка заголовков </summary>
  private void ListZagolovkov_draw()
  {
    string attributeTypeName;
    if (this.zagolovki_Ved_Curr._typeField == Vedomost_VB.TypeField.ObjectType)
    {
      this.select_Zagolovki_AttributeControl1.SelectedAttributeId = this.zagolovki_Ved_Curr._objectType;
      attributeTypeName = MetaDataHelper.GetAttributeTypeName(this.zagolovki_Ved_Curr._objectType);
      this.listBox_Zagolovki_AttribVedRec.Visible = true;
    }
    else
    {
      int index = -1;
      Vedomost_VB_Static.oneAttribVed_by_TypeFieldVedRec(this.zagolovki_Ved_Curr._typeFieldVedRec, out index);
      this.listBox_Zagolovki_AttribVedRec.SelectedIndex = index;
      attributeTypeName = this.listBox_Zagolovki_AttribVedRec.Items[index].ToString();
      if (this.zagolovki_Ved_Curr._typeFieldVedRec == Vedomost_VB.TypeFieldVedRec.Razdel_Ved)
        this.button_Zagolovki_FromList.Visible = true;
      else
        this.button_Zagolovki_FromList.Visible = false;
    }
    this.label_Zagolovki_Attribut.Text = attributeTypeName;
    for (int index = 0; index < this.zagolovki_Ved_Curr._list_One_Zagolovok.Count; ++index)
    {
      Vedomost_VB.One_Zagolovok oneZagolovok = this.zagolovki_Ved_Curr._list_One_Zagolovok[index];
      this.dataGridView_ListZagolovkov.Rows.Add((object[]) new string[2]
      {
        oneZagolovok._granicaPriznaka,
        oneZagolovok._name
      });
    }
    this.SelectDataGridViewRow(0);
    this.dataGridViewListZagolovkov_CellEnter((object) null, (DataGridViewCellEventArgs) null);
    if (this.zagolovki_Ved_Curr._list_One_Zagolovok.Count == 0)
      this.label_NoZgolovki.Visible = true;
    else
      this.label_NoZgolovki.Visible = false;
  }

  /// <summary> Отрисовка комментария текущей строки </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void dataGridViewListZagolovkov_CellEnter(object sender, DataGridViewCellEventArgs e)
  {
    string str1 = "";
    this.button_Zagolovki_Add.Enabled = true;
    this.buttonDelete_Zagolovki.Enabled = true;
    string[] currentRow = this.getCurrentRow();
    if (currentRow[0] == "" && currentRow[1] == "")
    {
      str1 = "Введите данные новой строки";
      this.button_Zagolovki_Add.Enabled = false;
      if (this.dataGridView_ListZagolovkov.CurrentCell.RowIndex == this.dataGridView_ListZagolovkov.RowCount - 1)
        this.buttonDelete_Zagolovki.Enabled = false;
    }
    if (currentRow[0] == "" && currentRow[1] != "")
    {
      str1 = "Введите текст заголовка";
      this.button_Zagolovki_Add.Enabled = false;
    }
    if (currentRow[0] != "" && currentRow[1] == "")
    {
      str1 = "Введите значение ключевого атрибута";
      this.button_Zagolovki_Add.Enabled = false;
    }
    if (currentRow[0] != "" && currentRow[1] != "")
    {
      string str2 = $"{$"{"Записи ведомости, у которых значение атрибута" + "\n"}\"{this.label_Zagolovki_Attribut.Text}\"" + "\n" + "Равно или более"} {currentRow[0]}";
      string[] nextRow = this.getNextRow();
      if (nextRow[0] != "")
        str2 = $"{str2 + " и менее"} {nextRow[0]}";
      str1 = $"{str2 + "\n" + "Будет иметь заголовок" + "\n"}\"{currentRow[1]}\"";
    }
    this.label_Zagolovki_SpravaVnizu.Text = str1;
    string str3 = "";
    string str4 = "";
    int rowIndex = this.dataGridView_ListZagolovkov.CurrentCell.RowIndex;
    if (this.dataGridView_ListZagolovkov.Rows[rowIndex].Cells[0].Value != null)
      str3 = this.dataGridView_ListZagolovkov.Rows[rowIndex].Cells[0].Value.ToString();
    if (this.dataGridView_ListZagolovkov.Rows[rowIndex].Cells[1].Value != null)
      str4 = this.dataGridView_ListZagolovkov.Rows[rowIndex].Cells[1].Value.ToString();
    if (!(str3 != "") && !(str4 != ""))
      return;
    this.label_NoZgolovki.Visible = false;
  }

  /// <summary> Текущая СТРОКА </summary>
  /// <returns></returns>
  private string[] getCurrentRow()
  {
    string[] currentRow = new string[2]{ "", "" };
    int rowIndex = this.dataGridView_ListZagolovkov.CurrentCell.RowIndex;
    string str1 = "";
    string str2 = "";
    if (this.dataGridView_ListZagolovkov.Rows[rowIndex].Cells[0].Value != null)
      str1 = this.dataGridView_ListZagolovkov.Rows[rowIndex].Cells[0].Value.ToString();
    if (this.dataGridView_ListZagolovkov.Rows[rowIndex].Cells[1].Value != null)
      str2 = this.dataGridView_ListZagolovkov.Rows[rowIndex].Cells[1].Value.ToString();
    currentRow[0] = str1;
    currentRow[1] = str2;
    return currentRow;
  }

  /// <summary> Следующая СТРОКА </summary>
  /// <returns></returns>
  private string[] getNextRow()
  {
    string[] nextRow = new string[2]{ "", "" };
    int index = this.dataGridView_ListZagolovkov.CurrentCell.RowIndex + 1;
    string str1 = "";
    string str2 = "";
    if (this.dataGridView_ListZagolovkov.Rows[index].Cells[0].Value != null)
      str1 = this.dataGridView_ListZagolovkov.Rows[index].Cells[0].Value.ToString();
    if (this.dataGridView_ListZagolovkov.Rows[index].Cells[1].Value != null)
      str2 = this.dataGridView_ListZagolovkov.Rows[index].Cells[1].Value.ToString();
    nextRow[0] = str1;
    nextRow[1] = str2;
    return nextRow;
  }

  /// <summary> Выделить ячейку </summary>
  /// <param name="rowIndex"></param>
  /// <param name="cellIndex"></param>
  public void SelectDataGridViewCell(int rowIndex, int cellIndex)
  {
    this.dataGridView_ListZagolovkov.CurrentCell = this.dataGridView_ListZagolovkov.Rows[rowIndex].Cells[0];
    this.dataGridView_ListZagolovkov.CurrentCell.Selected = true;
  }

  /// <summary> Выделить строку rowNum </summary>
  /// <param name="rowNum"></param>
  public void SelectDataGridViewRow(int rowNum) => this.SelectDataGridViewCell(rowNum, 0);

  /// <summary> Открыть или спрятать кнопки Save </summary>
  /// <param name="isModified"></param>
  private void Modified(bool isModified)
  {
    if (isModified)
    {
      this._isModified = true;
      this.buttonSave.Enabled = true;
    }
    else
    {
      this._isModified = false;
      this.buttonSave.Enabled = false;
    }
  }

  /// <summary> Кнопка Добавить </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void buttonAdd_Click(object sender, EventArgs e)
  {
    int y = this.dataGridView_ListZagolovkov.CurrentCellAddress.Y;
    string[] strArray = new string[2];
    this.dataGridView_ListZagolovkov.Rows.Insert(y, (object[]) strArray);
    this.dataGridView_ListZagolovkov.CurrentCell = this.dataGridView_ListZagolovkov.Rows[y].Cells[0];
    this.label_NoZgolovki.Visible = false;
    this.Modified(true);
  }

  private void buttonDelete_Click(object sender, EventArgs e)
  {
    int y = this.dataGridView_ListZagolovkov.CurrentCellAddress.Y;
    this.dataGridView_ListZagolovkov.Rows.Remove(this.dataGridView_ListZagolovkov.CurrentRow);
    this.dataGridView_ListZagolovkov.CurrentCell = this.dataGridView_ListZagolovkov.Rows[y].Cells[0];
    if (this.dataGridView_ListZagolovkov.Rows.Count == 1)
      this.label_NoZgolovki.Visible = true;
    this.Modified(true);
  }

  private void button1_Click(object sender, EventArgs e) => this.clear();

  /// <summary> Изменение ключевого поля </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void buttonEdit1_Click(object sender, EventArgs e)
  {
    string attributeTypeName;
    if (this.select_Zagolovki_AttributeControl1.SelectedAttributeId > -1)
    {
      if (new AvsRowAttributeInfo(this.select_Zagolovki_AttributeControl1.SelectedAttribute).FieldType != FieldTypes.ftString)
      {
        int num = (int) MessageBox.Show("Для создания заголовков можно выбирать атрибуты только строчного типа", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        return;
      }
      this.zagolovki_Ved_Curr._objectType = this.select_Zagolovki_AttributeControl1.SelectedAttributeId;
      this.zagolovki_Ved_Curr._typeField = Vedomost_VB.TypeField.ObjectType;
      this.zagolovki_Ved_Curr._typeFieldVedRec = Vedomost_VB.TypeFieldVedRec.Undefined;
      attributeTypeName = MetaDataHelper.GetAttributeTypeName(this.zagolovki_Ved_Curr._objectType);
    }
    else
    {
      this.zagolovki_Ved_Curr._objectType = -1;
      this.zagolovki_Ved_Curr._typeField = Vedomost_VB.TypeField.TypeFieldVedRec;
      this.zagolovki_Ved_Curr._typeFieldVedRec = Vedomost_VB_Static._listOneAttribVedRec[this.listBox_Zagolovki_AttribVedRec.SelectedIndex]._typeFieldVedRec;
      attributeTypeName = this.listBox_Zagolovki_AttribVedRec.Items[this.listBox_Zagolovki_AttribVedRec.SelectedIndex].ToString();
    }
    this.label_Zagolovki_Attribut.Text = attributeTypeName;
    this.dataGridViewListZagolovkov_CellEnter((object) null, (DataGridViewCellEventArgs) null);
    this.Modified(true);
  }

  /// <summary> Кнопка ПО УМОЛЧАНИЮ </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void buttonDefault_Click(object sender, EventArgs e)
  {
    Guid guidVed = Vedomost_VB_Static.GuidRS;
    if (this._one_Ved_Nastr._typeCreate == Vedomost_VB.TypeCreate.System)
    {
      guidVed = this._one_Ved_Nastr._guidTypeVed;
    }
    else
    {
      Guid guid = Vedomost_VB_Static.GuidTypeVedByTypeVed(this._one_Ved_Nastr._typeVed);
      if (guid != Guid.Empty)
        guidVed = guid;
    }
    this.zagolovki_Ved_Curr = Vedomost_VB_Static.Zagolovki_Ved_Init(guidVed);
    this.Draw(this.zagolovki_Ved_Curr);
    this.Modified(true);
  }

  /// <summary> Кнопка СОХРАНИТЬ </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void buttonSave_Click(object sender, EventArgs e) => this.save();

  /// <summary> Очистить таблицу </summary>
  private void dataGridViewListZagolovkov_Clear()
  {
    for (int index = 0; index < this.dataGridView_ListZagolovkov.RowCount - 1; ++index)
    {
      this.dataGridView_ListZagolovkov.Rows[index].Cells[0].Value = (object) "";
      this.dataGridView_ListZagolovkov.Rows[index].Cells[1].Value = (object) "";
    }
    this.dataGridView_ListZagolovkov.RowCount = 1;
  }

  /// <summary> При ПОПЫТКЕ закрыть окно диалога </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void Zagolovki_FormClosing(object sender, FormClosingEventArgs e)
  {
    if (this._isModified)
    {
      int num = (int) MessageBox.Show("Параметры настройки изменены\r\n\r\nСохранить?", "Внимание!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
      if (num == 6)
      {
        if (this.save())
          e.Cancel = false;
        else
          e.Cancel = true;
      }
      if (num == 7)
      {
        this.Modified(false);
        e.Cancel = false;
      }
      if (num != 2)
        return;
      e.Cancel = true;
    }
    else
      e.Cancel = false;
  }

  /// <summary> Метод Сохранить </summary>
  private bool save()
  {
    this.dataGridViewListZagolovkov_CleanFromEmpty();
    if (!this.dataGridViewListZagolovkov_Control())
      return false;
    this._one_Ved_Nastr._zagolovki_Ved = Vedomost_VB_Static.Zagolovki_Ved_Copy(this.zagolovki_Ved_Curr);
    Vedomost_VB_Static.WriteXmlNastrToBase(this._one_Ved_Nastr.XmlDocument_create(), this._one_Ved_Nastr._vedomostTemplateObjectGuid);
    this.Modified(false);
    this.buttonSave.Enabled = false;
    return true;
  }

  /// <summary> Удаление пустых строк </summary>
  private void dataGridViewListZagolovkov_CleanFromEmpty()
  {
    for (int index = this.dataGridView_ListZagolovkov.RowCount - 2; index > -1; --index)
    {
      if (this.dataGridView_ListZagolovkov.Rows[index].Cells[0].Value == null)
        this.dataGridView_ListZagolovkov.Rows.RemoveAt(index);
    }
  }

  /// <summary> Контролировать данные таблицы </summary>
  /// <returns></returns>
  private bool dataGridViewListZagolovkov_Control()
  {
    string strB = "";
    int num1 = 0;
    int num2 = 0;
    bool flag = true;
    for (int index = 0; index < this.dataGridView_ListZagolovkov.RowCount - 1; ++index)
    {
      if (this.dataGridView_ListZagolovkov.Rows[index].Cells[0].Value == null)
      {
        this.SelectDataGridViewCell(index, 0);
        int num3 = (int) MessageBox.Show("Значение ключевого атрибута не задано", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        flag = false;
        break;
      }
      string strA = this.dataGridView_ListZagolovkov.Rows[index].Cells[0].Value.ToString();
      if (strB != "")
      {
        try
        {
          num1 = Convert.ToInt32(strA);
          num2 = Convert.ToInt32(strB);
        }
        catch
        {
        }
        if (num1 > 0 && num2 > 0)
        {
          if (num2 > num1 || num1 == num2)
          {
            this.SelectDataGridViewCell(index, 0);
            int num4 = (int) MessageBox.Show("Значения ключевого атрибута не в порядке возрастания", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            flag = false;
            break;
          }
        }
        else if (string.Compare(strA, strB, StringComparison.Ordinal) < 0)
        {
          this.SelectDataGridViewCell(index, 0);
          int num5 = (int) MessageBox.Show("Значения ключевого атрибута не в порядке возрастания", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
          flag = false;
          break;
        }
      }
      strB = strA;
    }
    return flag;
  }

  /// <summary> Данные из таблицы перенести в zagolovki_Ved_Curr </summary>
  /// <returns></returns>
  private bool dataGridViewListZagolovkov_To_zagolovki_Ved()
  {
    this.zagolovki_Ved_Curr._list_One_Zagolovok.Clear();
    for (int index = 0; index < this.dataGridView_ListZagolovkov.RowCount - 1; ++index)
    {
      string str1 = "";
      string str2 = "";
      if (this.dataGridView_ListZagolovkov.Rows[index].Cells[0].Value != null)
        str1 = this.dataGridView_ListZagolovkov.Rows[index].Cells[0].Value.ToString();
      if (this.dataGridView_ListZagolovkov.Rows[index].Cells[1].Value != null)
        str2 = this.dataGridView_ListZagolovkov.Rows[index].Cells[1].Value.ToString();
      if (!(str1 == ""))
        this.zagolovki_Ved_Curr._list_One_Zagolovok.Add(new Vedomost_VB.One_Zagolovok()
        {
          _granicaPriznaka = str1,
          _name = str2
        });
    }
    return true;
  }

  /// <summary> Заполнение списка атрибутов ведомостей </summary>
  private void ListBoxAttribVedRec_Filled()
  {
    for (int index = 0; index < Vedomost_VB_Static._listOneAttribVedRec.Count; ++index)
      this.listBox_Zagolovki_AttribVedRec.Items.Add((object) Vedomost_VB_Static._listOneAttribVedRec[index]._name);
  }

  private void listBoxAttribVedRec_MouseClick(object sender, MouseEventArgs e)
  {
    this.select_Zagolovki_AttributeControl1.SelectedAttributeId = -1;
    this.listBox_Zagolovki_AttribVedRec.Items[this.listBox_Zagolovki_AttribVedRec.SelectedIndex].ToString();
    if (Vedomost_VB_Static._listOneAttribVedRec[this.listBox_Zagolovki_AttribVedRec.SelectedIndex]._typeFieldVedRec == Vedomost_VB.TypeFieldVedRec.Razdel_Ved)
      this.button_Zagolovki_FromList.Visible = true;
    else
      this.button_Zagolovki_FromList.Visible = false;
  }

  private void bCancel_Click(object sender, EventArgs e)
  {
    this.Modified(false);
    this.Close();
  }

  /// <summary> Полная очистка списка </summary>
  private void clear()
  {
    this.dataGridView_ListZagolovkov.Rows.Clear();
    this.zagolovki_Ved_Curr._list_One_Zagolovok.Clear();
    this.ListZagolovkov_draw();
  }

  /// <summary> Событие - редактировние ячейки </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void dataGridViewListZagolovkov_CellBeginEdit(
    object sender,
    DataGridViewCellCancelEventArgs e)
  {
    this.Modified(true);
  }

  private void create_Zagolovki_Ved_From_list_RazdelsVed()
  {
    this.dataGridView_ListZagolovkov.Rows.Clear();
    this.zagolovki_Ved_Curr.Clear();
    this.zagolovki_Ved_Curr._typeField = Vedomost_VB.TypeField.TypeFieldVedRec;
    this.zagolovki_Ved_Curr._typeFieldVedRec = Vedomost_VB.TypeFieldVedRec.Razdel_Ved;
    for (int index = 0; index < this._one_Ved_Nastr._list_RazdelsVed.Count; ++index)
    {
      Vedomost_VB.OneRazdelVed oneRazdelVed = this._one_Ved_Nastr._list_RazdelsVed[index];
      this.zagolovki_Ved_Curr._list_One_Zagolovok.Add(new Vedomost_VB.One_Zagolovok()
      {
        _granicaPriznaka = oneRazdelVed._razdelVed.ToString(),
        _name = oneRazdelVed._name
      });
    }
    this.ListZagolovkov_draw();
    this.listBox_Zagolovki_AttribVedRec.SelectedIndex = 0;
    this.zagolovki_Ved_Curr._typeFieldVedRec = Vedomost_VB_Static._listOneAttribVedRec[this.listBox_Zagolovki_AttribVedRec.SelectedIndex]._typeFieldVedRec;
    this.label_Zagolovki_Attribut.Text = this.listBox_Zagolovki_AttribVedRec.Items[this.listBox_Zagolovki_AttribVedRec.SelectedIndex].ToString();
    this.dataGridViewListZagolovkov_CellEnter((object) null, (DataGridViewCellEventArgs) null);
    this.Modified(true);
  }

  private void buttonFromList_Click(object sender, EventArgs e)
  {
    this.create_Zagolovki_Ved_From_list_RazdelsVed();
  }

  private void buttonCopyFrom_Click(object sender, EventArgs e)
  {
    using (VyborVedomosti vyborVedomosti = new VyborVedomosti())
    {
      vyborVedomosti._imsObjectTypeDel = this.imsObjectTypeCurr;
      if (vyborVedomosti.ShowDialog() != DialogResult.OK)
        return;
      this.Draw(vyborVedomosti._one_Ved_Nastr_Result._zagolovki_Ved);
      this.Modified(true);
    }
  }

  private void Draw(Vedomost_VB.Zagolovki_Ved zagolovki_Ved)
  {
    this.zagolovki_Ved_Curr = Vedomost_VB_Static.Zagolovki_Ved_Copy(zagolovki_Ved);
    this.select_Zagolovki_AttributeControl1.Select((NodeColumnCollection) null, (List<AVSColumnScheme>) null);
    this.listBox_Zagolovki_AttribVedRec.SelectedIndex = -1;
    if (this.zagolovki_Ved_Curr._typeField == Vedomost_VB.TypeField.ObjectType)
    {
      this.select_Zagolovki_AttributeControl1.SelectedAttributeId = this.zagolovki_Ved_Curr._objectType;
      MetaDataHelper.GetAttributeTypeName(this.zagolovki_Ved_Curr._objectType);
    }
    else
    {
      int index = -1;
      Vedomost_VB_Static.oneAttribVed_by_TypeFieldVedRec(this.zagolovki_Ved_Curr._typeFieldVedRec, out index);
      this.listBox_Zagolovki_AttribVedRec.SelectedIndex = index;
      this.listBox_Zagolovki_AttribVedRec.Items[index].ToString();
    }
    if (this.listBox_Zagolovki_AttribVedRec.SelectedIndex > -1)
      this.listBox_Zagolovki_AttribVedRec.Items[this.listBox_Zagolovki_AttribVedRec.SelectedIndex].ToString();
    this.dataGridViewListZagolovkov_Clear();
    this.ListZagolovkov_draw();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Zagolovki));
    this.panelButtons = new Panel();
    this.buttonCopyFrom = new Button();
    this.bCancel = new Button();
    this.bOK = new Button();
    this.buttonSave = new Button();
    this.buttonDefault = new Button();
    this.groupBox_ListZagolovkov = new GroupBox();
    this.dataGridView_ListZagolovkov = new DataGridView();
    this.Column1 = new DataGridViewTextBoxColumn();
    this.Column2 = new DataGridViewTextBoxColumn();
    this.label_Zagolovki_SpravaVnizu = new Label();
    this.button_Zagolovki_Add = new Button();
    this.buttonDelete_Zagolovki = new Button();
    this.toolTip1 = new ToolTip(this.components);
    this.button_Zagolovki_EditKeyAtribut = new Button();
    this.groupBox_Zagolovki_AttribVedRec1 = new GroupBox();
    this.listBox_Zagolovki_AttribVedRec = new ListBox();
    this.button_Zagolovki_FromList = new Button();
    this.select_Zagolovki_AttributeControl1 = new SelectAvsAttributeControl();
    this.label_Zagolovki_SlevaVverhu = new Label();
    this.label_Zagolovki_Attribut = new Label();
    this.groupBox_Zagolovki_AtributeControl1 = new GroupBox();
    this.imageList1 = new ImageList(this.components);
    this.label_NoZgolovki = new Label();
    this.panelButtons.SuspendLayout();
    this.groupBox_ListZagolovkov.SuspendLayout();
    ((ISupportInitialize) this.dataGridView_ListZagolovkov).BeginInit();
    this.groupBox_Zagolovki_AttribVedRec1.SuspendLayout();
    this.groupBox_Zagolovki_AtributeControl1.SuspendLayout();
    this.SuspendLayout();
    this.panelButtons.BorderStyle = BorderStyle.Fixed3D;
    this.panelButtons.Controls.Add((Control) this.buttonCopyFrom);
    this.panelButtons.Controls.Add((Control) this.bCancel);
    this.panelButtons.Controls.Add((Control) this.bOK);
    this.panelButtons.Controls.Add((Control) this.buttonSave);
    this.panelButtons.Controls.Add((Control) this.buttonDefault);
    this.panelButtons.Dock = DockStyle.Bottom;
    this.panelButtons.Location = new Point(0, 640);
    this.panelButtons.Name = "panelButtons";
    this.panelButtons.Size = new Size(976, 42);
    this.panelButtons.TabIndex = 1;
    this.buttonCopyFrom.Location = new Point(171, 5);
    this.buttonCopyFrom.Name = "buttonCopyFrom";
    this.buttonCopyFrom.Size = new Size(121, 27);
    this.buttonCopyFrom.TabIndex = 4;
    this.buttonCopyFrom.Text = "Копировать";
    this.toolTip1.SetToolTip((Control) this.buttonCopyFrom, "Копировать данные настройки из настроек другой ведомости");
    this.buttonCopyFrom.UseVisualStyleBackColor = true;
    this.buttonCopyFrom.Click += new EventHandler(this.buttonCopyFrom_Click);
    this.bCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bCancel.DialogResult = DialogResult.Cancel;
    this.bCancel.Location = new Point(837, 5);
    this.bCancel.Name = "bCancel";
    this.bCancel.Size = new Size(121, 27);
    this.bCancel.TabIndex = 3;
    this.bCancel.Text = "Отмена";
    this.toolTip1.SetToolTip((Control) this.bCancel, "Отменить изменения и закрыть диалог");
    this.bCancel.UseVisualStyleBackColor = true;
    this.bCancel.Click += new EventHandler(this.bCancel_Click);
    this.bOK.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bOK.DialogResult = DialogResult.OK;
    this.bOK.Location = new Point(697, 5);
    this.bOK.Name = "bOK";
    this.bOK.Size = new Size(121, 27);
    this.bOK.TabIndex = 2;
    this.bOK.Text = "OK";
    this.toolTip1.SetToolTip((Control) this.bOK, "Сохранить изменения и закрыть диалог");
    this.bOK.UseVisualStyleBackColor = true;
    this.buttonSave.Enabled = false;
    this.buttonSave.Location = new Point(490, 5);
    this.buttonSave.Name = "buttonSave";
    this.buttonSave.Size = new Size(167, 27);
    this.buttonSave.TabIndex = 1;
    this.buttonSave.Text = "Сохранить";
    this.toolTip1.SetToolTip((Control) this.buttonSave, "Сохранить изменения. Диалог не закрывать.");
    this.buttonSave.UseVisualStyleBackColor = true;
    this.buttonSave.Click += new EventHandler(this.buttonSave_Click);
    this.buttonDefault.Location = new Point(12, 5);
    this.buttonDefault.Name = "buttonDefault";
    this.buttonDefault.Size = new Size(121, 27);
    this.buttonDefault.TabIndex = 0;
    this.buttonDefault.Text = "По умолчанию";
    this.toolTip1.SetToolTip((Control) this.buttonDefault, "Всем значениям настроек присвоить значения по умолчанию");
    this.buttonDefault.UseVisualStyleBackColor = true;
    this.buttonDefault.Click += new EventHandler(this.buttonDefault_Click);
    this.groupBox_ListZagolovkov.Controls.Add((Control) this.dataGridView_ListZagolovkov);
    this.groupBox_ListZagolovkov.Location = new Point(492, 6);
    this.groupBox_ListZagolovkov.Name = "groupBox_ListZagolovkov";
    this.groupBox_ListZagolovkov.Size = new Size(470, 380);
    this.groupBox_ListZagolovkov.TabIndex = 3;
    this.groupBox_ListZagolovkov.TabStop = false;
    this.groupBox_ListZagolovkov.Text = "Список заголовков";
    this.dataGridView_ListZagolovkov.AllowUserToResizeColumns = false;
    this.dataGridView_ListZagolovkov.AllowUserToResizeRows = false;
    this.dataGridView_ListZagolovkov.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
    this.dataGridView_ListZagolovkov.Columns.AddRange((DataGridViewColumn) this.Column1, (DataGridViewColumn) this.Column2);
    this.dataGridView_ListZagolovkov.Dock = DockStyle.Fill;
    this.dataGridView_ListZagolovkov.Location = new Point(3, 16 /*0x10*/);
    this.dataGridView_ListZagolovkov.Name = "dataGridView_ListZagolovkov";
    this.dataGridView_ListZagolovkov.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
    this.dataGridView_ListZagolovkov.Size = new Size(464, 361);
    this.dataGridView_ListZagolovkov.TabIndex = 0;
    this.dataGridView_ListZagolovkov.CellBeginEdit += new DataGridViewCellCancelEventHandler(this.dataGridViewListZagolovkov_CellBeginEdit);
    this.dataGridView_ListZagolovkov.CellEnter += new DataGridViewCellEventHandler(this.dataGridViewListZagolovkov_CellEnter);
    this.Column1.HeaderText = "Значение";
    this.Column1.Name = "Column1";
    this.Column1.Resizable = DataGridViewTriState.False;
    this.Column1.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.Column1.Width = 90;
    this.Column2.HeaderText = "Заголовок";
    this.Column2.Name = "Column2";
    this.Column2.Resizable = DataGridViewTriState.False;
    this.Column2.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.Column2.Width = 310;
    this.label_Zagolovki_SpravaVnizu.BorderStyle = BorderStyle.Fixed3D;
    this.label_Zagolovki_SpravaVnizu.Location = new Point(492, 419);
    this.label_Zagolovki_SpravaVnizu.Name = "label_Zagolovki_SpravaVnizu";
    this.label_Zagolovki_SpravaVnizu.Size = new Size(464, 165);
    this.label_Zagolovki_SpravaVnizu.TabIndex = 4;
    this.button_Zagolovki_Add.Image = (Image) Resources.AddStandart;
    this.button_Zagolovki_Add.ImageAlign = ContentAlignment.MiddleRight;
    this.button_Zagolovki_Add.Location = new Point(699, 598);
    this.button_Zagolovki_Add.Name = "button_Zagolovki_Add";
    this.button_Zagolovki_Add.Size = new Size(121, 27);
    this.button_Zagolovki_Add.TabIndex = 5;
    this.button_Zagolovki_Add.Text = "Добавить";
    this.toolTip1.SetToolTip((Control) this.button_Zagolovki_Add, "Добавить строку выше текущей");
    this.button_Zagolovki_Add.UseVisualStyleBackColor = true;
    this.button_Zagolovki_Add.Click += new EventHandler(this.buttonAdd_Click);
    this.buttonDelete_Zagolovki.Image = (Image) Resources.DeleteStandart;
    this.buttonDelete_Zagolovki.ImageAlign = ContentAlignment.MiddleRight;
    this.buttonDelete_Zagolovki.Location = new Point(838, 598);
    this.buttonDelete_Zagolovki.Name = "buttonDelete_Zagolovki";
    this.buttonDelete_Zagolovki.Size = new Size(121, 27);
    this.buttonDelete_Zagolovki.TabIndex = 6;
    this.buttonDelete_Zagolovki.Text = "Удалить";
    this.toolTip1.SetToolTip((Control) this.buttonDelete_Zagolovki, "Удалить текущую строку");
    this.buttonDelete_Zagolovki.UseVisualStyleBackColor = true;
    this.buttonDelete_Zagolovki.Click += new EventHandler(this.buttonDelete_Click);
    this.button_Zagolovki_EditKeyAtribut.Image = (Image) Resources.EditStandart;
    this.button_Zagolovki_EditKeyAtribut.ImageAlign = ContentAlignment.MiddleRight;
    this.button_Zagolovki_EditKeyAtribut.Location = new Point(125, 598);
    this.button_Zagolovki_EditKeyAtribut.Name = "button_Zagolovki_EditKeyAttribut";
    this.button_Zagolovki_EditKeyAtribut.Size = new Size(231, 27);
    this.button_Zagolovki_EditKeyAtribut.TabIndex = 15;
    this.button_Zagolovki_EditKeyAtribut.Text = "Изменить ключевой атрибут";
    this.toolTip1.SetToolTip((Control) this.button_Zagolovki_EditKeyAtribut, "Изменить ключевой атрибут");
    this.button_Zagolovki_EditKeyAtribut.UseVisualStyleBackColor = true;
    this.button_Zagolovki_EditKeyAtribut.Click += new EventHandler(this.buttonEdit1_Click);
    this.groupBox_Zagolovki_AttribVedRec1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
    this.groupBox_Zagolovki_AttribVedRec1.Controls.Add((Control) this.listBox_Zagolovki_AttribVedRec);
    this.groupBox_Zagolovki_AttribVedRec1.Location = new Point(15, 393);
    this.groupBox_Zagolovki_AttribVedRec1.Name = "groupBox_Zagolovki_AttribVedRec1";
    this.groupBox_Zagolovki_AttribVedRec1.Size = new Size(454, 194);
    this.groupBox_Zagolovki_AttribVedRec1.TabIndex = 16 /*0x10*/;
    this.groupBox_Zagolovki_AttribVedRec1.TabStop = false;
    this.groupBox_Zagolovki_AttribVedRec1.Text = "Атрибуты записей ведомостей";
    this.toolTip1.SetToolTip((Control) this.groupBox_Zagolovki_AttribVedRec1, "Атрибуты (имена данных) записей характерные для ведомостей");
    this.listBox_Zagolovki_AttribVedRec.Dock = DockStyle.Fill;
    this.listBox_Zagolovki_AttribVedRec.FormattingEnabled = true;
    this.listBox_Zagolovki_AttribVedRec.Location = new Point(3, 16 /*0x10*/);
    this.listBox_Zagolovki_AttribVedRec.Name = "listBox_Zagolovki_AttribVedRec";
    this.listBox_Zagolovki_AttribVedRec.Size = new Size(448, 175);
    this.listBox_Zagolovki_AttribVedRec.TabIndex = 0;
    this.toolTip1.SetToolTip((Control) this.listBox_Zagolovki_AttribVedRec, "Атрибуты (имена данных) записей характерные для ведомостей");
    this.listBox_Zagolovki_AttribVedRec.MouseClick += new MouseEventHandler(this.listBoxAttribVedRec_MouseClick);
    this.listBox_Zagolovki_AttribVedRec.DoubleClick += new EventHandler(this.buttonEdit1_Click);
    this.button_Zagolovki_FromList.ImageAlign = ContentAlignment.MiddleRight;
    this.button_Zagolovki_FromList.Location = new Point(492, 598);
    this.button_Zagolovki_FromList.Name = "button_Zagolovki_FromList";
    this.button_Zagolovki_FromList.Size = new Size(167, 27);
    this.button_Zagolovki_FromList.TabIndex = 19;
    this.button_Zagolovki_FromList.Text = "По списку разделов";
    this.toolTip1.SetToolTip((Control) this.button_Zagolovki_FromList, "За основу списка заголовков взять список разделов");
    this.button_Zagolovki_FromList.UseVisualStyleBackColor = true;
    this.button_Zagolovki_FromList.Visible = false;
    this.button_Zagolovki_FromList.Click += new EventHandler(this.buttonFromList_Click);
    this.select_Zagolovki_AttributeControl1.Dock = DockStyle.Fill;
    this.select_Zagolovki_AttributeControl1.Font = new Font("Tahoma", 8.25f);
    this.select_Zagolovki_AttributeControl1.Location = new Point(3, 16 /*0x10*/);
    this.select_Zagolovki_AttributeControl1.Name = "select_Zagolovki_AttributeControl1";
    this.select_Zagolovki_AttributeControl1.Size = new Size(454, 295);
    this.select_Zagolovki_AttributeControl1.TabIndex = 3;
    this.toolTip1.SetToolTip((Control) this.select_Zagolovki_AttributeControl1, "Общие (системные) атрибуты системы IPS");
    this.select_Zagolovki_AttributeControl1.DoubleClick += new EventHandler(this.buttonEdit1_Click);
    this.label_Zagolovki_SlevaVverhu.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.label_Zagolovki_SlevaVverhu.AutoEllipsis = true;
    this.label_Zagolovki_SlevaVverhu.Location = new Point(12, 6);
    this.label_Zagolovki_SlevaVverhu.Name = "label_Zagolovki_SlevaVverhu";
    this.label_Zagolovki_SlevaVverhu.RightToLeft = RightToLeft.Yes;
    this.label_Zagolovki_SlevaVverhu.Size = new Size(463, 33);
    this.label_Zagolovki_SlevaVverhu.TabIndex = 7;
    this.label_Zagolovki_SlevaVverhu.Text = "Атрибут, по которому производится создание заголовков";
    this.label_Zagolovki_SlevaVverhu.TextAlign = ContentAlignment.TopCenter;
    this.label_Zagolovki_Attribut.BorderStyle = BorderStyle.Fixed3D;
    this.label_Zagolovki_Attribut.Location = new Point(9, 39);
    this.label_Zagolovki_Attribut.Name = "label_Zagolovki_Attribut";
    this.label_Zagolovki_Attribut.Size = new Size(464, 24);
    this.label_Zagolovki_Attribut.TabIndex = 8;
    this.label_Zagolovki_Attribut.TextAlign = ContentAlignment.TopCenter;
    this.groupBox_Zagolovki_AtributeControl1.BackColor = SystemColors.Control;
    this.groupBox_Zagolovki_AtributeControl1.Controls.Add((Control) this.select_Zagolovki_AttributeControl1);
    this.groupBox_Zagolovki_AtributeControl1.Location = new Point(12, 72);
    this.groupBox_Zagolovki_AtributeControl1.Name = "groupBox_Zagolovki_AttributeControl1";
    this.groupBox_Zagolovki_AtributeControl1.Size = new Size(460, 314);
    this.groupBox_Zagolovki_AtributeControl1.TabIndex = 14;
    this.groupBox_Zagolovki_AtributeControl1.TabStop = false;
    this.groupBox_Zagolovki_AtributeControl1.Text = "Системные атрибуты";
    this.imageList1.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imageList1.ImageStream");
    this.imageList1.TransparentColor = Color.Transparent;
    this.imageList1.Images.SetKeyName(0, "Not.ico");
    this.label_NoZgolovki.AutoSize = true;
    this.label_NoZgolovki.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
    this.label_NoZgolovki.Location = new Point(641, 396);
    this.label_NoZgolovki.Name = "label_NoZgolovki";
    this.label_NoZgolovki.Size = new Size(147, 13);
    this.label_NoZgolovki.TabIndex = 18;
    this.label_NoZgolovki.Text = "Заголовки отсутствуют";
    this.label_NoZgolovki.Visible = false;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.bCancel;
    this.ClientSize = new Size(976, 682);
    this.Controls.Add((Control) this.button_Zagolovki_FromList);
    this.Controls.Add((Control) this.label_NoZgolovki);
    this.Controls.Add((Control) this.groupBox_Zagolovki_AttribVedRec1);
    this.Controls.Add((Control) this.button_Zagolovki_EditKeyAtribut);
    this.Controls.Add((Control) this.groupBox_Zagolovki_AtributeControl1);
    this.Controls.Add((Control) this.label_Zagolovki_Attribut);
    this.Controls.Add((Control) this.label_Zagolovki_SlevaVverhu);
    this.Controls.Add((Control) this.buttonDelete_Zagolovki);
    this.Controls.Add((Control) this.button_Zagolovki_Add);
    this.Controls.Add((Control) this.label_Zagolovki_SpravaVnizu);
    this.Controls.Add((Control) this.groupBox_ListZagolovkov);
    this.Controls.Add((Control) this.panelButtons);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (Zagolovki);
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Настройка создания заголовков ведомости";
    this.FormClosing += new FormClosingEventHandler(this.Zagolovki_FormClosing);
    this.Load += new EventHandler(this.Zagolovki_Load);
    this.panelButtons.ResumeLayout(false);
    this.groupBox_ListZagolovkov.ResumeLayout(false);
    ((ISupportInitialize) this.dataGridView_ListZagolovkov).EndInit();
    this.groupBox_Zagolovki_AttribVedRec1.ResumeLayout(false);
    this.groupBox_Zagolovki_AtributeControl1.ResumeLayout(false);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
