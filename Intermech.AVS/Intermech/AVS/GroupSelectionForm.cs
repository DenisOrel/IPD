// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.GroupSelectionForm
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Document.Client;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS;

/// <summary>
/// Выбор разрабытываемой ведомости
/// а для групповой - выбор исполнения ведомости
/// </summary>
public class GroupSelectionForm : Form
{
  public List<ProductInfo> _allProducts;
  public Vedomost_VB.FormaGroup _formaGroup;
  public int _itemIndex = -1;
  public bool _isGroupSp;
  public bool _isGroupVed = true;
  public AVSDocumentForm _aVSDocumentForm;
  public One_ImsObjectType_With_One_Ved_Nastr _one_ImsObjectType_With_One_Ved_Nastr_Result;
  public Vedomost_VB_Static.One_Conformity_Template_Nastr _one_Conformity_Template_Nastr_Result;
  public ProductInfo _productInfo_Result;
  public string _designationVed_Result = "";
  public int _iIsp_Result;
  public string _designation_Article;
  public bool isError;
  private bool NoClosing;
  private ListBox listBoxVedomostName;
  private Panel panelForButtons;
  internal Button bCancel;
  internal Button bOK;
  private Panel panelForGroup;
  private GroupBox groupBoxGroupOrEdin;
  private RadioButton radioButtonGroup2;
  private RadioButton radioButtonGroup1;
  private ListBox listBoxIspolnenia;
  private ToolTip toolTip1;
  private IContainer components;
  private GroupBox groupBoxForma;
  private RadioButton radioButtonB;
  private RadioButton radioButtonA;

  public GroupSelectionForm() => this.InitializeComponent();

  private void GroupSelectionForm_Load(object sender, EventArgs e)
  {
    for (int index = 0; index < Vedomost_VB_Static._list_Ved_Arbeit_ImsObjectType_With_One_Ved_Nastr.Count; ++index)
    {
      IMSObjectType imsObjectType = Vedomost_VB_Static._list_Ved_Arbeit_ImsObjectType_With_One_Ved_Nastr[index].imsObjectType;
      string str1 = imsObjectType.ObjectTypeName;
      string str2 = "";
      if (Vedomost_VB_Static.IsUse_New_System_ByOneNastr)
      {
        Vedomost_VB_Static.One_Conformity_Template_Nastr templateNastrByDocGuid = Vedomost_VB_Static.Find_One_Conformity_Template_Nastr_ByDocGuid(imsObjectType.Guid);
        if (templateNastrByDocGuid == null || templateNastrByDocGuid._guid_Template == Guid.Empty)
          str2 = "нет шаблона";
        else if (templateNastrByDocGuid._one_Ved_Nastr == null || templateNastrByDocGuid._one_Ved_Nastr._typeCreateNastr == TypeCreateNastr.Empty)
          str2 = "не настроено";
        else if (templateNastrByDocGuid._one_Ved_Nastr._list_Ved_ID.Count == 0)
          str2 = "сбор не настроен";
      }
      else if (DocumentEditorPlugin.GetDocumentTemplateIDFromIMDocSettings(imsObjectType.Guid) == Guid.Empty)
        str2 = "нет шаблона";
      if (str2 != "")
        str1 = $"{str1}    [{str2}]";
      this.listBoxVedomostName.Items.Add((object) str1);
    }
    this.listBoxVedomostName.SelectedIndex = 0;
    if (this._allProducts.Count > 1)
    {
      this._isGroupSp = true;
      this.panelForGroup.Visible = true;
      this.groupBoxForma.Visible = true;
      for (int index = 0; index < this._allProducts.Count; ++index)
        this.listBoxIspolnenia.Items.Add((object) this._allProducts[index].Designation);
    }
    else
      this.listBoxVedomostName.Height = 450;
    One_ImsObjectType_With_One_Ved_Nastr typeWithOneVedNastr = Vedomost_VB_Static._list_Ved_Arbeit_ImsObjectType_With_One_Ved_Nastr[0];
    if (!this._isGroupSp)
      return;
    this.drawElements();
  }

  private void radioButtonGroup1_Click(object sender, EventArgs e)
  {
    this.listBoxIspolnenia.Visible = false;
    this._isGroupVed = true;
    this.groupBoxForma.Visible = true;
    this.drawElements();
  }

  private void radioButtonGroup2_Click(object sender, EventArgs e)
  {
    this.listBoxIspolnenia.Visible = true;
    this.listBoxIspolnenia.SelectedIndex = 0;
    this._isGroupVed = false;
    this.groupBoxForma.Visible = false;
  }

  /// <summary> Прорисовка области ИСПОЛНЕНИЙ </summary>
  private void drawElements()
  {
    One_Ved_Nastr oneVedNastr = Vedomost_VB_Static._list_Ved_Arbeit_ImsObjectType_With_One_Ved_Nastr[this.listBoxVedomostName.SelectedIndex].one_Ved_Nastr;
    if (oneVedNastr != null && (oneVedNastr._typeVed == Vedomost_VB.TypeVed.VS || oneVedNastr._typeVed == Vedomost_VB.TypeVed.VP))
    {
      if (!this.groupBoxForma.Enabled)
        this.radioButtonA.Checked = true;
      if (this.radioButtonGroup1.Checked)
        this.groupBoxForma.Visible = true;
      else
        this.groupBoxForma.Visible = false;
      this.groupBoxForma.Enabled = true;
    }
    else
    {
      this.radioButtonA.Checked = true;
      this.groupBoxForma.Enabled = false;
      this.groupBoxForma.Visible = false;
    }
  }

  private void listBoxVedomostName_SelectedIndexChanged(object sender, EventArgs e)
  {
    this.drawElements();
  }

  private void bOK_Click(object sender, EventArgs e)
  {
    string str1 = (string) this.listBoxVedomostName.Items[this.listBoxVedomostName.SelectedIndex];
    if (str1.StartsWith("Ведомость ссылочных документов") || str1.StartsWith("Ведомость держателей подлинников"))
    {
      string str2 = "";
      string str3 = "Для сбора данного типа ведомости в настройках конфигуратора базы данных не найдены атрибуты:";
      if (AvsIDCache.Attr_TypeNTD == -10000)
        str2 = str3 + "\r\n\r\n" + "Тип НТД";
      if (AvsIDCache.Attr_VidNTD == -10000 && str1.StartsWith("Ведомость ссылочных документов"))
      {
        string str4;
        str2 = (!(str2 == "") ? str3 + "\r\n" : (str4 = str3 + "\r\n\r\n")) + "Вид НТД";
      }
      if (str2 != "")
      {
        int num = (int) MessageBox.Show(str2 + "\r\n\r\nНеобходима настройка конфигуратора", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        this.isError = true;
        return;
      }
    }
    this._one_ImsObjectType_With_One_Ved_Nastr_Result = Vedomost_VB_Static._list_Ved_Arbeit_ImsObjectType_With_One_Ved_Nastr[this.listBoxVedomostName.SelectedIndex];
    if (Vedomost_VB_Static.IsUse_New_System_ByOneNastr)
      this._one_Conformity_Template_Nastr_Result = Vedomost_VB_Static.Find_One_Conformity_Template_Nastr_ByDocGuid(this._one_ImsObjectType_With_One_Ved_Nastr_Result.imsObjectType.Guid);
    if (Vedomost_VB_Static.IsUse_New_System_ByOneNastr && this._one_Conformity_Template_Nastr_Result != null && this._one_Conformity_Template_Nastr_Result._guid_Template == Guid.Empty && this._one_Conformity_Template_Nastr_Result._guid_Template == Guid.Empty)
    {
      int num = (int) MessageBox.Show("Для такого типа документа нет назначенного шаблона", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      this.NoClosing = true;
    }
    else
    {
      if (Vedomost_VB_Static.IsUse_New_System_ByOneNastr)
      {
        if (!this._one_Conformity_Template_Nastr_Result._one_Ved_Nastr.IsAutoSbor())
        {
          int num = (int) MessageBox.Show("Для такого типа ведомости нет настроек автоматического сбора", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
          this.NoClosing = true;
          return;
        }
      }
      else
      {
        if (this._one_ImsObjectType_With_One_Ved_Nastr_Result.one_Ved_Nastr._autoSbor == 0)
        {
          int num = (int) MessageBox.Show("Для такого типа ведомости выключена возможность автоматического сбора", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
          this.NoClosing = true;
          return;
        }
        if (!this._one_ImsObjectType_With_One_Ved_Nastr_Result.one_Ved_Nastr.IsAutoSbor())
        {
          int num = (int) MessageBox.Show("Для такого типа ведомости нет настроек автоматического сбора", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
          this.NoClosing = true;
          return;
        }
      }
      if (this._isGroupSp && this.radioButtonGroup2.Checked)
      {
        int selectedIndex = this.listBoxIspolnenia.SelectedIndex;
        this._productInfo_Result = this._allProducts[selectedIndex];
        this._designationVed_Result = this._productInfo_Result.Designation;
        this._iIsp_Result = selectedIndex;
      }
      else
      {
        this._productInfo_Result = this._allProducts[0];
        this._designationVed_Result = this._designation_Article;
      }
      if (!this._isGroupVed)
        return;
      if (this.radioButtonA.Checked)
        this._formaGroup = Vedomost_VB.FormaGroup.A;
      if (!this.radioButtonB.Checked)
        return;
      this._formaGroup = Vedomost_VB.FormaGroup.B;
    }
  }

  /// <summary> DoubleClick мышкой </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void listBoxVedomostName_DoubleClick(object sender, EventArgs e)
  {
    this.bOK_Click(sender, e);
    this.DialogResult = DialogResult.OK;
    this.Close();
  }

  /// <summary> Нажали на ENTER </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void listBoxVedomostName_KeyDown(object sender, KeyEventArgs e)
  {
    if (e.KeyCode != Keys.Return)
      return;
    this.bOK_Click(sender, (EventArgs) e);
    this.DialogResult = DialogResult.OK;
    this.Close();
  }

  private void radioButtonB_CheckedChanged(object sender, EventArgs e)
  {
    if (Vedomost_VB_Static.IsUse_New_System_ByOneNastr)
    {
      Vedomost_VB_Static.One_Conformity_Template_Nastr templateNastrByDocGuid = Vedomost_VB_Static.Find_One_Conformity_Template_Nastr_ByDocGuid(Vedomost_VB_Static._list_Ved_Arbeit_ImsObjectType_With_One_Ved_Nastr[this.listBoxVedomostName.SelectedIndex].imsObjectType.Guid);
      if (this.radioButtonB.Checked && templateNastrByDocGuid._one_Ved_Nastr._vedomostTemplateObjectGuid_B == Guid.Empty)
      {
        int num = (int) MessageBox.Show("Бланк (шаблон) для формы Б не назначен\r\n\r\nСмотри Настройка, Закладка \"Сервис\" кнопка \"Шаблон Б\"", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        this.radioButtonB.Checked = false;
        this.radioButtonA.Checked = true;
        return;
      }
    }
    if (!this.radioButtonB.Checked || MessageBox.Show("Ведомость формы Б в дальнейшем не преобразовать в групповую А или единичную\r\n\r\nПродолжить?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
      return;
    this.radioButtonA.Checked = true;
  }

  /// <summary> Попытка закрытия диалога </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void GroupSelectionForm_FormClosing(object sender, FormClosingEventArgs e)
  {
    if (!this.NoClosing)
      return;
    e.Cancel = true;
    this.NoClosing = false;
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    this.listBoxVedomostName = new ListBox();
    this.panelForButtons = new Panel();
    this.bCancel = new Button();
    this.bOK = new Button();
    this.panelForGroup = new Panel();
    this.listBoxIspolnenia = new ListBox();
    this.groupBoxGroupOrEdin = new GroupBox();
    this.radioButtonGroup2 = new RadioButton();
    this.radioButtonGroup1 = new RadioButton();
    this.groupBoxForma = new GroupBox();
    this.radioButtonB = new RadioButton();
    this.radioButtonA = new RadioButton();
    this.toolTip1 = new ToolTip(this.components);
    this.panelForButtons.SuspendLayout();
    this.panelForGroup.SuspendLayout();
    this.groupBoxGroupOrEdin.SuspendLayout();
    this.groupBoxForma.SuspendLayout();
    this.SuspendLayout();
    this.listBoxVedomostName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.listBoxVedomostName.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    this.listBoxVedomostName.FormattingEnabled = true;
    this.listBoxVedomostName.ItemHeight = 16 /*0x10*/;
    this.listBoxVedomostName.Location = new Point(12, 12);
    this.listBoxVedomostName.Name = "listBoxVedomostName";
    this.listBoxVedomostName.Size = new Size(579, 244);
    this.listBoxVedomostName.TabIndex = 0;
    this.toolTip1.SetToolTip((Control) this.listBoxVedomostName, "Выбор собираемой ведомости");
    this.listBoxVedomostName.SelectedIndexChanged += new EventHandler(this.listBoxVedomostName_SelectedIndexChanged);
    this.listBoxVedomostName.DoubleClick += new EventHandler(this.listBoxVedomostName_DoubleClick);
    this.listBoxVedomostName.KeyDown += new KeyEventHandler(this.listBoxVedomostName_KeyDown);
    this.panelForButtons.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.panelForButtons.Controls.Add((Control) this.bCancel);
    this.panelForButtons.Controls.Add((Control) this.bOK);
    this.panelForButtons.Location = new Point(12, 491);
    this.panelForButtons.Name = "panelForButtons";
    this.panelForButtons.Size = new Size(579, 42);
    this.panelForButtons.TabIndex = 3;
    this.bCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.bCancel.DialogResult = DialogResult.Cancel;
    this.bCancel.Location = new Point(454, 5);
    this.bCancel.Name = "bCancel";
    this.bCancel.Size = new Size(121, 27);
    this.bCancel.TabIndex = 2;
    this.bCancel.Text = "Отмена";
    this.toolTip1.SetToolTip((Control) this.bCancel, "Прервать процесс сбора ведомости");
    this.bCancel.UseVisualStyleBackColor = true;
    this.bOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.bOK.DialogResult = DialogResult.OK;
    this.bOK.Location = new Point(327, 5);
    this.bOK.Name = "bOK";
    this.bOK.Size = new Size(121, 27);
    this.bOK.TabIndex = 1;
    this.bOK.Text = "OK";
    this.toolTip1.SetToolTip((Control) this.bOK, "Продолжить процесс сбора ведомости");
    this.bOK.UseVisualStyleBackColor = true;
    this.bOK.Click += new EventHandler(this.bOK_Click);
    this.panelForGroup.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.panelForGroup.BackColor = SystemColors.Info;
    this.panelForGroup.Controls.Add((Control) this.listBoxIspolnenia);
    this.panelForGroup.Controls.Add((Control) this.groupBoxGroupOrEdin);
    this.panelForGroup.Controls.Add((Control) this.groupBoxForma);
    this.panelForGroup.Location = new Point(12, 265);
    this.panelForGroup.Name = "panelForGroup";
    this.panelForGroup.Size = new Size(579, 222);
    this.panelForGroup.TabIndex = 4;
    this.panelForGroup.Visible = false;
    this.listBoxIspolnenia.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.listBoxIspolnenia.FormattingEnabled = true;
    this.listBoxIspolnenia.Location = new Point(3, 83);
    this.listBoxIspolnenia.Name = "listBoxIspolnenia";
    this.listBoxIspolnenia.Size = new Size(569, 134);
    this.listBoxIspolnenia.TabIndex = 1;
    this.toolTip1.SetToolTip((Control) this.listBoxIspolnenia, "Выбор одного исполнения, на которое создать ведомость");
    this.listBoxIspolnenia.Visible = false;
    this.groupBoxGroupOrEdin.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.groupBoxGroupOrEdin.Controls.Add((Control) this.radioButtonGroup2);
    this.groupBoxGroupOrEdin.Controls.Add((Control) this.radioButtonGroup1);
    this.groupBoxGroupOrEdin.Location = new Point(3, 3);
    this.groupBoxGroupOrEdin.Name = "groupBoxGroupOrEdin";
    this.groupBoxGroupOrEdin.Size = new Size(419, 68);
    this.groupBoxGroupOrEdin.TabIndex = 0;
    this.groupBoxGroupOrEdin.TabStop = false;
    this.groupBoxGroupOrEdin.Text = "Создать ведомость: групповую или единичную";
    this.radioButtonGroup2.AutoSize = true;
    this.radioButtonGroup2.Location = new Point(6, 42);
    this.radioButtonGroup2.Name = "radioButtonGroup2";
    this.radioButtonGroup2.Size = new Size(186, 17);
    this.radioButtonGroup2.TabIndex = 1;
    this.radioButtonGroup2.Text = "Ведомость на одно исполнение";
    this.radioButtonGroup2.UseVisualStyleBackColor = true;
    this.radioButtonGroup2.Click += new EventHandler(this.radioButtonGroup2_Click);
    this.radioButtonGroup1.AutoSize = true;
    this.radioButtonGroup1.Checked = true;
    this.radioButtonGroup1.Location = new Point(6, 19);
    this.radioButtonGroup1.Name = "radioButtonGroup1";
    this.radioButtonGroup1.Size = new Size(136, 17);
    this.radioButtonGroup1.TabIndex = 0;
    this.radioButtonGroup1.TabStop = true;
    this.radioButtonGroup1.Text = "Групповая ведомость";
    this.radioButtonGroup1.UseVisualStyleBackColor = true;
    this.radioButtonGroup1.Click += new EventHandler(this.radioButtonGroup1_Click);
    this.groupBoxForma.Controls.Add((Control) this.radioButtonB);
    this.groupBoxForma.Controls.Add((Control) this.radioButtonA);
    this.groupBoxForma.Enabled = false;
    this.groupBoxForma.Location = new Point(428, 3);
    this.groupBoxForma.Name = "groupBoxForma";
    this.groupBoxForma.Size = new Size(146, 68);
    this.groupBoxForma.TabIndex = 2;
    this.groupBoxForma.TabStop = false;
    this.groupBoxForma.Text = "Форма";
    this.groupBoxForma.Visible = false;
    this.radioButtonB.AutoSize = true;
    this.radioButtonB.Location = new Point(6, 42);
    this.radioButtonB.Name = "radioButtonB";
    this.radioButtonB.Size = new Size(72, 17);
    this.radioButtonB.TabIndex = 1;
    this.radioButtonB.Text = "Форма Б";
    this.radioButtonB.UseVisualStyleBackColor = true;
    this.radioButtonB.CheckedChanged += new EventHandler(this.radioButtonB_CheckedChanged);
    this.radioButtonA.AutoSize = true;
    this.radioButtonA.Checked = true;
    this.radioButtonA.Location = new Point(6, 19);
    this.radioButtonA.Name = "radioButtonA";
    this.radioButtonA.Size = new Size(72, 17);
    this.radioButtonA.TabIndex = 0;
    this.radioButtonA.TabStop = true;
    this.radioButtonA.Text = "Форма А";
    this.radioButtonA.UseVisualStyleBackColor = true;
    this.toolTip1.ShowAlways = true;
    this.CancelButton = (IButtonControl) this.bCancel;
    this.ClientSize = new Size(603, 536);
    this.Controls.Add((Control) this.panelForGroup);
    this.Controls.Add((Control) this.panelForButtons);
    this.Controls.Add((Control) this.listBoxVedomostName);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (GroupSelectionForm);
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Выбор разрабатываемой ведомости";
    this.FormClosing += new FormClosingEventHandler(this.GroupSelectionForm_FormClosing);
    this.Load += new EventHandler(this.GroupSelectionForm_Load);
    this.panelForButtons.ResumeLayout(false);
    this.panelForGroup.ResumeLayout(false);
    this.groupBoxGroupOrEdin.ResumeLayout(false);
    this.groupBoxGroupOrEdin.PerformLayout();
    this.groupBoxForma.ResumeLayout(false);
    this.groupBoxForma.PerformLayout();
    this.ResumeLayout(false);
  }
}
