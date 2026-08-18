// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Victor.VyborVedomosti
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
namespace Intermech.AVS.Victor;

public class VyborVedomosti : Form
{
  public IMSObjectType _imsObjectTypeDel;
  public IMSObjectType _imsObjectTypeVydelit;
  public IMSObjectType _imsObjectType_Result;
  public One_ImsObjectType_With_One_Ved_Nastr _one_ImsObjectType_With_One_Ved_Nastr;
  private int i_Vydelit = -1;
  public Vedomost_VB.TypeDoc _typeDoc;
  public List<IMSObjectType> _listVedRedaktirovanyi = new List<IMSObjectType>();
  public Guid _guidTemplateVed_Result = Guid.Empty;
  public Guid _guidTypeVed_Result = Guid.Empty;
  public Vedomost_VB_Static.One_Conformity_Template_Nastr _one_Conformity_Template_Nastr_Result;
  public string _documentName_Result;
  public One_Ved_Nastr _one_Ved_Nastr_Result;
  public string _caption = "";
  private bool NoClosing;
  public List<One_ImsObjectType_With_One_Ved_Nastr> _list_ImsObjectType_With_One_Ved_Nastrs;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ImageList imagesToolbars;
  private ImageList imageList1;
  private ToolTip toolTip1;
  private Panel panelForButtons;
  internal Button bCancel;
  internal Button bOK;
  private ListBox listBoxVedomostName;

  public VyborVedomosti() => this.InitializeComponent();

  private void VyborVedomosti_Load(object sender, EventArgs e)
  {
    for (int index = 0; index < this._list_ImsObjectType_With_One_Ved_Nastrs.Count; ++index)
    {
      One_ImsObjectType_With_One_Ved_Nastr typeWithOneVedNastr = this._list_ImsObjectType_With_One_Ved_Nastrs[index];
      IMSObjectType imsObjectType = typeWithOneVedNastr.imsObjectType;
      if (imsObjectType.ObjectTypeID != AvsIDCache.ObjType_EspdLU)
      {
        if (this._imsObjectTypeVydelit != null && imsObjectType.ObjectTypeName == this._imsObjectTypeVydelit.ObjectTypeName)
          this.i_Vydelit = index;
        string str1 = imsObjectType.ObjectTypeName;
        string str2 = "";
        if (Vedomost_VB_Static.IsUse_New_System_ByOneNastr)
        {
          Vedomost_VB_Static.One_Conformity_Template_Nastr templateNastrByDocGuid = Vedomost_VB_Static.Find_One_Conformity_Template_Nastr_ByDocGuid(imsObjectType.Guid);
          if (templateNastrByDocGuid._guid_Template == Guid.Empty && typeWithOneVedNastr.one_Ved_Nastr != null)
            templateNastrByDocGuid._guid_Template = typeWithOneVedNastr.one_Ved_Nastr._vedomostTemplateObjectGuid;
          if (templateNastrByDocGuid == null || templateNastrByDocGuid._guid_Template == Guid.Empty)
            str2 = "нет шаблона";
          else if (templateNastrByDocGuid._one_Ved_Nastr == null || templateNastrByDocGuid._one_Ved_Nastr._list_Ved_ID.Count == 0 || templateNastrByDocGuid._one_Ved_Nastr._typeCreateNastr == TypeCreateNastr.Empty)
            str2 = "";
        }
        else if (DocumentEditorPlugin.GetDocumentTemplateIDFromIMDocSettings(imsObjectType.Guid) == Guid.Empty)
          str2 = "нет шаблона";
        if (str2 != "")
          str1 = $"{str1}    [{str2}]";
        this.listBoxVedomostName.Items.Add((object) str1);
        this._listVedRedaktirovanyi.Add(imsObjectType);
      }
    }
    if (this.i_Vydelit > -1)
      this.listBoxVedomostName.SelectedIndex = this.i_Vydelit;
    else if (this.listBoxVedomostName.Items.Count == 0)
      this.listBoxVedomostName.SelectedIndex = -1;
    else
      this.listBoxVedomostName.SelectedIndex = 0;
    if (!(this._caption != ""))
      return;
    this.Text = this._caption;
  }

  private void bOK_Click(object sender, EventArgs e)
  {
    int selectedIndex = this.listBoxVedomostName.SelectedIndex;
    if (selectedIndex < 0)
      return;
    this._one_ImsObjectType_With_One_Ved_Nastr = this._list_ImsObjectType_With_One_Ved_Nastrs[selectedIndex];
    this._imsObjectType_Result = this._one_ImsObjectType_With_One_Ved_Nastr.imsObjectType;
    Vedomost_VB_Static.One_Conformity_Template_Nastr conformityTemplateNastr = (Vedomost_VB_Static.One_Conformity_Template_Nastr) null;
    if (Vedomost_VB_Static.IsUse_New_System_ByOneNastr)
      conformityTemplateNastr = Vedomost_VB_Static.Find_One_Conformity_Template_Nastr_ByDocGuid(this._imsObjectType_Result.Guid);
    if (Vedomost_VB_Static.IsUse_New_System_ByOneNastr && conformityTemplateNastr != null && conformityTemplateNastr._guid_Template != Guid.Empty)
    {
      if (conformityTemplateNastr._guid_Template == Guid.Empty)
      {
        int num = (int) MessageBox.Show("Для такого типа документа нет назначенного шаблона" + "\r\n\r\nНастройка ведомости невозможна" + "\r\n\r\nНеобходимо настроить систему в" + "\r\n\r\nНастройка\r\n  Настройка инструментов\r\n    Интеграторы с приложениями\r\n      Интегратор с редактором документов", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        this.NoClosing = true;
      }
      else
      {
        this._one_Conformity_Template_Nastr_Result = conformityTemplateNastr;
        this._guidTemplateVed_Result = DocumentEditorPlugin.GetDocumentTemplateIDFromIMDocSettings(this._imsObjectType_Result.Guid);
        this._one_Ved_Nastr_Result = this._one_ImsObjectType_With_One_Ved_Nastr.one_Ved_Nastr;
      }
    }
    else
    {
      this._guidTemplateVed_Result = DocumentEditorPlugin.GetDocumentTemplateIDFromIMDocSettings(this._imsObjectType_Result.Guid);
      if (this._guidTemplateVed_Result == Guid.Empty)
      {
        int num = (int) MessageBox.Show("Для такого типа документа нет назначенного шаблона" + "\r\n\r\nНастройка ведомости невозможна" + "\r\n\r\nНеобходимо настроить систему в" + "\r\n\r\nНастройка\r\n  Настройка инструментов\r\n    Интеграторы с приложениями\r\n      Интегратор с редактором документов", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        this.NoClosing = true;
      }
      else
      {
        One_ImsObjectType_With_One_Ved_Nastr typeWithOneVedNastr = Vedomost_VB_Static.Checking_Use_Template(this._list_ImsObjectType_With_One_Ved_Nastrs, this._guidTemplateVed_Result, this._one_ImsObjectType_With_One_Ved_Nastr.imsObjectType.ObjectName);
        if (typeWithOneVedNastr != null && !Vedomost_VB_Static.IsUse_New_System_ByOneNastr)
        {
          int num = (int) MessageBox.Show($"{$"Для документа \"{this._one_ImsObjectType_With_One_Ved_Nastr.imsObjectType.ObjectTypeName}\"" + "\r\nнастроено использование шаблона, который уже используется в документе"}\r\n\"{typeWithOneVedNastr.imsObjectType.ObjectTypeName}\"" + "\r\n\r\nЭто не допускается" + "\r\n\r\nКаждому типу документа должен соответствовать свой шаблон", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
          this.NoClosing = true;
        }
        else
        {
          this._guidTypeVed_Result = this._imsObjectType_Result.Guid;
          this._documentName_Result = this._imsObjectType_Result.ObjectTypeName;
          this._one_Ved_Nastr_Result = this._one_ImsObjectType_With_One_Ved_Nastr.one_Ved_Nastr;
        }
      }
    }
  }

  private void listBoxVedomostName_MouseDoubleClick(object sender, MouseEventArgs e)
  {
    this.bOK_Click(sender, (EventArgs) e);
    this.DialogResult = DialogResult.OK;
    this.Close();
  }

  private void listBoxVedomostName_KeyDown(object sender, KeyEventArgs e)
  {
    if (e.KeyCode != Keys.Return)
      return;
    this.bOK_Click(sender, (EventArgs) e);
    this.DialogResult = DialogResult.OK;
    this.Close();
  }

  private void VyborVedomosti_Shown(object sender, EventArgs e)
  {
    if (this._list_ImsObjectType_With_One_Ved_Nastrs != null && this._list_ImsObjectType_With_One_Ved_Nastrs.Count != 0)
      return;
    string text = "В Конфигураторе базы данных документов данного типа нет";
    string str1 = "Список пустой т.к. для конструкторских ";
    string str2 = "ведомостей";
    string str3 = "таблиц";
    string str4 = " не назначен Редактор\r\n\r\nСмотри:\r\n\r\nНастройка/Настройка инструментов";
    string str5 = "\r\n  Объекты\r\n    Документы\r\n      Конструкторские документы\r\n        ";
    string str6 = "Ведомости";
    string str7 = "Конструкторские таблицы";
    string str8 = "\r\n\r\nДля КАЖДОГО документа должны быть настроены\r\n\r\n\"Редактор команд\" и \"Команды по умолчанию\" - AVS";
    if (this._typeDoc == Vedomost_VB.TypeDoc.Ved && Vedomost_VB_Static.isObnuliliListVed)
      text = str1 + str2 + str4 + str5 + str6 + str8;
    if (this._typeDoc == Vedomost_VB.TypeDoc.Tabl && Vedomost_VB_Static.isObnuliliListTabl)
      text = str1 + str3 + str4 + str5 + str7 + str8;
    int num = (int) MessageBox.Show(text, "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
  }

  private void VyborVedomosti_FormClosing(object sender, FormClosingEventArgs e)
  {
    if (!this.NoClosing)
      return;
    e.Cancel = true;
    this.NoClosing = false;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (VyborVedomosti));
    this.imagesToolbars = new ImageList(this.components);
    this.imageList1 = new ImageList(this.components);
    this.toolTip1 = new ToolTip(this.components);
    this.bCancel = new Button();
    this.bOK = new Button();
    this.listBoxVedomostName = new ListBox();
    this.panelForButtons = new Panel();
    this.panelForButtons.SuspendLayout();
    this.SuspendLayout();
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
    this.imageList1.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imageList1.ImageStream");
    this.imageList1.TransparentColor = Color.Transparent;
    this.imageList1.Images.SetKeyName(0, "Not.ico");
    this.bCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bCancel.DialogResult = DialogResult.Cancel;
    this.bCancel.Location = new Point(393, 5);
    this.bCancel.Name = "bCancel";
    this.bCancel.Size = new Size(121, 27);
    this.bCancel.TabIndex = 2;
    this.bCancel.Text = "Отмена";
    this.toolTip1.SetToolTip((Control) this.bCancel, "Отменить изменения и закрыть диалог");
    this.bCancel.UseVisualStyleBackColor = true;
    this.bOK.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bOK.DialogResult = DialogResult.OK;
    this.bOK.Location = new Point(262, 5);
    this.bOK.Name = "bOK";
    this.bOK.Size = new Size(121, 27);
    this.bOK.TabIndex = 1;
    this.bOK.Text = "OK";
    this.toolTip1.SetToolTip((Control) this.bOK, "Сохранить изменения и закрыть диалог");
    this.bOK.UseVisualStyleBackColor = true;
    this.bOK.Click += new EventHandler(this.bOK_Click);
    this.listBoxVedomostName.Dock = DockStyle.Fill;
    this.listBoxVedomostName.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    this.listBoxVedomostName.FormattingEnabled = true;
    this.listBoxVedomostName.ItemHeight = 16 /*0x10*/;
    this.listBoxVedomostName.Location = new Point(0, 0);
    this.listBoxVedomostName.Name = "listBoxVedomostName";
    this.listBoxVedomostName.Size = new Size(522, 342);
    this.listBoxVedomostName.TabIndex = 12;
    this.toolTip1.SetToolTip((Control) this.listBoxVedomostName, "Выбор собираемой ведомости");
    this.listBoxVedomostName.KeyDown += new KeyEventHandler(this.listBoxVedomostName_KeyDown);
    this.listBoxVedomostName.MouseDoubleClick += new MouseEventHandler(this.listBoxVedomostName_MouseDoubleClick);
    this.panelForButtons.BorderStyle = BorderStyle.Fixed3D;
    this.panelForButtons.Controls.Add((Control) this.bCancel);
    this.panelForButtons.Controls.Add((Control) this.bOK);
    this.panelForButtons.Dock = DockStyle.Bottom;
    this.panelForButtons.Location = new Point(0, 342);
    this.panelForButtons.Name = "panelForButtons";
    this.panelForButtons.Size = new Size(522, 42);
    this.panelForButtons.TabIndex = 11;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.bCancel;
    this.ClientSize = new Size(522, 384);
    this.Controls.Add((Control) this.listBoxVedomostName);
    this.Controls.Add((Control) this.panelForButtons);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (VyborVedomosti);
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "В текущие настройки копировать из настроек ...";
    this.FormClosing += new FormClosingEventHandler(this.VyborVedomosti_FormClosing);
    this.Load += new EventHandler(this.VyborVedomosti_Load);
    this.Shown += new EventHandler(this.VyborVedomosti_Shown);
    this.panelForButtons.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
