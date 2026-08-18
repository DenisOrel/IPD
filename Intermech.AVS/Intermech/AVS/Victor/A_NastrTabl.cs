// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Victor.A_NastrTabl
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.AVS.Properties;
using Intermech.Docking;
using Intermech.Docking.Rendering;
using Intermech.Document.Client;
using Intermech.Document.Model;
using Intermech.Document.UI;
using Intermech.Interfaces;
using Intermech.Interfaces.Attributes;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Document;
using Intermech.Kernel.Search;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;

#nullable disable
namespace Intermech.AVS.Victor;

public class A_NastrTabl : Form
{
  private long templateID_curr_Vyvod = -1;
  private long templateID_Vyvod = -1;
  private long templateID_curr_Xml = -1;
  private long templateID_Xml = -1;
  private long templateID_curr_Avs = -1;
  private long templateID_Avs = -1;
  public Guid _guidTypeTabl_Curr;
  public Guid _guidTemplateTabl_Curr;
  public Vedomost_VB_Static.One_Conformity_Template_Nastr _one_Conformity_Template_Nastr_Curr;
  public string _documentName_Curr;
  public IMSObjectType _imsObjectType_Curr;
  private string docName = "";
  private bool IsModifiedAll;
  private bool isCreate = true;
  private bool IsModified_Page_Bases;
  private bool IsModified_Page_Sbor;
  private bool IsModified_Page_Vyvod;
  private bool IsModified_Page_Service;
  private bool IsModified_Page_Xml;
  private bool IsModified_Page_Avs;
  public bool IsBylo_IsModified_Page_Vyvod;
  public bool IsBylo_IsModified;
  private bool IsModifiedFromFile;
  private bool isButtonDefault;
  private bool isByloButtonTypeVedTo_Click;
  private bool is_one_Ved_Nastr_New;
  private bool noClosing;
  public One_Ved_Nastr _one_Tabl_Nastr_Curr;
  public One_ImsObjectType_With_One_Ved_Nastr _one_ImsObjectType_With_One_Ved_Nastr_Curr;
  public One_Ved_Nastr _one_Tabl_Nastr_Tmp;
  public List<One_ImsObjectType_With_One_Ved_Nastr> _list_Tabl_Arbeit_ImsObjectType_With_One_Tabl_Nastr;
  private List<QuickObjectInfo> list_CalalogsImbaseFull;
  private List<QuickObjectInfo> list_CalalogsImbaseTmp;
  private int _indexImageList_Section;
  private ImDocument imDocument_template_Vyvod;
  private ImDocument imDocument_template_Vyvod_FromDump;
  private long templID_Vyvod;
  private DocumentControl docControl_Vyvod;
  private DockControl docKcontrol_Vyvod;
  private DocumentTreeViewDlg documentTreeViewDlg_Vyvod;
  private A_NastrTabl.OneVyvodNode oneTreeNode_Current;
  private Vedomost_VB.OneRecordToPrint oneRecordToPrint_Current;
  private Vedomost_VB.OneGrafaToPrint oneGrafaToPrint_Current;
  private int i_curr_oneGrafaToPrint_Current = -1;
  private Vedomost_VB.OneDataFieldToPrint oneDataFieldToPrint_current;
  private Vedomost_VB.AlgorithmToPrint algorithmToPrint_curr;
  private Vedomost_VB.AlgorithmToPrint algorithmToPrint;
  private ImDocument imDocument_template_Xml;
  private long templID_Xml;
  private DocumentControl docControl_Xml;
  private DockControl docKcontrol_Xml;
  private DocumentTreeViewDlg documentTreeViewDlg_Xml;
  private string text_Old = "";
  private Vedomost_VB_Static.OneXmlNode oneXmlNode_Curr;
  private ImDocument imDocument_template_Avs;
  private long templID_Avs;
  private DocumentControl docControl_Avs;
  private DockControl docKcontrol_Avs;
  private DocumentTreeViewDlg documentTreeViewDlg_Avs;
  private A_NastrTabl.OneAvsNode oneTreeNode_Avs_Current;
  private Vedomost_VB.OneRecord_Avs6_To_Ips oneRecord_Avs_Current;
  private Vedomost_VB.OneGrafa_Avs6_To_Ips oneGrafa_Avs_Current;
  private int i_curr_oneGrafa_Avs_Current = -1;
  private Vedomost_VB.OneDataField_Avs6_To_Ips oneDataField_Avs_current;
  private Vedomost_VB.Algorithm_Avs6_To_Ips algorithm_Avs_curr;
  private Vedomost_VB.Algorithm_Avs6_To_Ips algorithm_Avs;
  private string text_Old_Avs = "";
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ToolTip toolTip1;
  private ImageList imageList1;
  private ImageList imagesToolbars;
  private ImageList imageListSort;
  private Panel panelForButtons;
  private Button buttonWarnings;
  private Button buttonCopyFrom;
  private Button buttonSelectTabl;
  private Button buttonSave1;
  private Button buttonDefault;
  internal Button bCancel;
  internal Button bOK;
  private System.Windows.Forms.TabControl tabControl_Nastr;
  private System.Windows.Forms.TabPage tabPage_Sbor;
  private System.Windows.Forms.TabPage tabPage_Vyvod;
  private Button button_Sbor_Peredatha_Delete2;
  private Button button_Sbor_Peredatha_Add2;
  private GroupBox groupBox_Sbor_Peredatha_AttributeControl1;
  private SelectAvsAttributeControl select_Sbor_Peredatha_AttributeControl2;
  private GroupBox groupBox_Sbor_Peredatha_ListId;
  private ListBox listBox_Sbor_Peredatha_ListId;
  private Panel panel_Vyvod_1;
  private Button button_Vyvod_AddAttribut;
  private Button button_Vyvod_Delete;
  private Button button_Vyvod_Edit;
  private Button button_Vyvod_AddCell;
  private GroupBox groupBox_Vyvod_TextRazdelitel;
  private ComboBox comboBox_Vyvod_TextRazdelitel;
  private TreeView treeView_Vyvod;
  private DocumentContainer docContainer_Vyvod;
  private DockContainer docKcontainer_Vyvod;
  private DockManager dockMan_Vyvod;
  private System.Windows.Forms.TabPage tabPage_Service;
  private Label label_ServicesFileOpen;
  private Label label_ServiceCreateDump;
  private Label label_ServicesTypeVedTo;
  private Label label_ServicesCopyAll;
  private Label label_ServicesDefaultAll;
  private Button buttonServicesFileOpen;
  private Button buttonServiceCreateDump;
  private Button buttonServicesTypeVedTo;
  private Button buttonServicesCopyAll;
  private Button buttonServicesDefaultAll;
  private Label labelService1;
  private Label labelService2;
  private System.Windows.Forms.TabPage tabPage_Bases;
  private GroupBox groupBox_Usl_Bases_ImbaseCatalog;
  private Label label_QuickObjectInfo;
  private Label label_CatalogsImbase;
  private Button button_Delete_From_To_listBox_QuickObjectInfo;
  private Button button_Add_To_listBox_QuickObjectInfo;
  private ListBox listBox_CatalogsImbase;
  private ListBox listBox_QuickObjectInfo;
  private GroupBox groupBox_Usl_Bases_Sbor_Input;
  private CheckBox checkBox_Usl_Bases_Sbor_isInputIzd;
  private CheckBox checkBox_Usl_Bases_Sbor_isInputDoc;
  private System.Windows.Forms.TabPage tabPage_Xml;
  private DockContainer docKcontainer_Xml;
  private DockManager dockMan_Xml;
  private DocumentContainer docContainer_Xml;
  private TreeView treeView_Xml;
  private GroupBox groupBox_Xml_Text;
  private TextBox textBox_Xml_Text;
  private Button button_Xml_Delete;
  private Button button_Xml_Edit;
  private Button button_Xml_Add;
  private GroupBox groupBox_Xml_EmptyString;
  private Label label_Xml_AfterRemark;
  private NumericUpDown numeric_UpDown_Xml_AfterRemark;
  private Label label_Xml_AfterInfo;
  private NumericUpDown numeric_UpDown_Xml_AfterInfo;
  private GroupBox groupBox_Xml_Out;
  private RadioButton radioButton_Xml_PassportOutNo;
  private RadioButton radioButton_Xml_PassportOutDialog;
  private RadioButton radioButton_Xml_PassporOutAlways;
  private GroupBox groupBox_Xml_In;
  private RadioButton radioButton_Xml_PassportInNo;
  private RadioButton radioButton_Xml_PassportInDialog;
  private RadioButton radioButton_Xml_PassporInAlways;
  private GroupBox groupBox_Xml_Folder_In;
  private Button button_Xml_Folder_In;
  private TextBox textBox_Xml_Folder_In;
  private CheckBox checkBox_Services_CreateDump;
  private GroupBox groupBox_Vyvod_Additional;
  private CheckBox checkBox_Vyvod_Additional4;
  private CheckBox checkBox_Vyvod_Additional3;
  private CheckBox checkBox_Vyvod_Additional2;
  private CheckBox checkBox_Vyvod_Additional1;
  private System.Windows.Forms.TabPage tabPage_Avs6;
  private GroupBox groupBox_Avs6_Fields;
  private ListBox listBox_Avs6_Fields;
  private Panel panel_Avs_1;
  private Button button_Avs_AddAttribut;
  private Button button_Avs_Delete;
  private Button button_Avs_Edit;
  private Button button_Avs_AddCell;
  private GroupBox groupBox_Avs_TextRazdelitel;
  private ComboBox comboBox_Avs_TextRazdelitel;
  private TreeView treeView_Avs;
  private DockContainer dockcontainer_Avs;
  private DockManager dockMan_Avs;
  private DocumentContainer docContainer_Avs;
  private GroupBox groupBox_Dump;
  private GroupBox groupBox_AccessLevel;
  private RadioButton radioButton_AccessLevel2;
  private RadioButton radioButton_AccessLevel1;
  private RadioButton radioButton_AccessLevel0;
  private GroupBox groupBox_Vyvod_List_Ved_Id;
  private ListBox listBox_Vyvod_List_Ved_Id;
  private CheckBox checkBox_Usl_Bases_Sbor_isInputMat;
  private Button buttonEditTemplate;

  public A_NastrTabl() => this.InitializeComponent();

  private void A_NastrTabl_Load(object sender, EventArgs e)
  {
    if (this._one_Conformity_Template_Nastr_Curr == null)
    {
      if (this._guidTemplateTabl_Curr == Guid.Empty)
      {
        int num = (int) MessageBox.Show("Шаблон документа пустой", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        this.Close();
        return;
      }
      if (this._one_Tabl_Nastr_Curr == null)
      {
        this.Processing_Template(this._guidTemplateTabl_Curr);
        if (this._one_Conformity_Template_Nastr_Curr._one_Ved_Nastr != null)
        {
          this._one_Tabl_Nastr_Curr = this._one_Conformity_Template_Nastr_Curr._one_Ved_Nastr;
        }
        else
        {
          this._one_Tabl_Nastr_Curr = new One_Ved_Nastr(true);
          this.is_one_Ved_Nastr_New = true;
          int num = (int) MessageBox.Show("Для данного типа документа настройки отсутствуют", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        this._one_Tabl_Nastr_Curr._vedomostTemplateObjectGuid = this._guidTemplateTabl_Curr;
      }
      else
        this.Processing_Template(this._guidTemplateTabl_Curr);
    }
    else
    {
      this._guidTemplateTabl_Curr = this._one_Conformity_Template_Nastr_Curr._guid_Template;
      this._guidTypeTabl_Curr = this._one_Conformity_Template_Nastr_Curr._guid_TypeVed;
      this._documentName_Curr = this._one_Conformity_Template_Nastr_Curr._name_Ved;
      this._one_Tabl_Nastr_Curr = this._one_Conformity_Template_Nastr_Curr._one_Ved_Nastr;
      this._one_Tabl_Nastr_Curr._vedomostTemplateObjectGuid = this._guidTemplateTabl_Curr;
      this.Processing_Template(this._guidTemplateTabl_Curr);
    }
    this._one_Tabl_Nastr_Curr._imsObjectType = this._imsObjectType_Curr;
    this._one_Tabl_Nastr_Curr._nameVed = this._imsObjectType_Curr.ObjectName;
    this._one_Tabl_Nastr_Tmp = Vedomost_VB_Static.One_Ved_Nastr_Copy(this._one_Tabl_Nastr_Curr);
    if (this._one_Tabl_Nastr_Tmp._algorithmToPrint == null || this._one_Tabl_Nastr_Tmp._typeCreateNastr == TypeCreateNastr.Empty || this._one_Tabl_Nastr_Tmp._algorithmToPrint._oneRecordToPrint_Info == null || this._one_Tabl_Nastr_Tmp._algorithmToPrint._oneRecordToPrint_Info._listOneGrafaToPrint == null || this._one_Tabl_Nastr_Tmp._algorithmToPrint._oneRecordToPrint_Info._listOneGrafaToPrint.Count < 1)
    {
      this._one_Tabl_Nastr_Tmp._algorithmToPrint = Tabl_Static.AlgorithmToPrint_Based_Init();
      this._one_Tabl_Nastr_Tmp._algorithmXml = Tabl_Static.AlgorithmXml_Tabl_Based_Init();
      int num = (int) MessageBox.Show("Для данного типа документа настройки вывода и XML созданы программой\r\n\r\nПроверьте", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    this.Text = $"{this.Text} [{this._one_Tabl_Nastr_Curr._imsObjectType.ObjectName}]";
    if (this._one_Tabl_Nastr_Curr._dateIni != "")
      this.Text = $"{this.Text} {this._one_Tabl_Nastr_Curr._dateIni}";
    else
      this.Text += " новая";
    this.IsButtonDefault();
    this.IsButtonCopyFrom();
    if (this._list_Tabl_Arbeit_ImsObjectType_With_One_Tabl_Nastr.Count > 1)
      this.buttonSelectTabl.Visible = true;
    else
      this.buttonSelectTabl.Visible = false;
    this.list_CalalogsImbaseFull = Vedomost_VB_Static.FindCatalogs();
    this.list_CalalogsImbaseTmp = Vedomost_VB_Static.FindCatalogs();
    Vedomost_VB_Static.Begin_For_Avs6();
    this.Cursor = Cursors.WaitCursor;
    this.Draw_All();
    this.Cursor = Cursors.Default;
    string machineName = Environment.MachineName;
    AVS6_From_Avs6Main.Inits();
    if (!AvsConfig.General.AskAVS6 && !Vedomost_VB_Static.IsAvs6ToIps)
      this.tabControl_Nastr.TabPages.Remove(this.tabPage_Avs6);
    if (!List_Element_Accord_Avs6_Ips.isPopytkaInits)
    {
      List_Element_Accord_Avs6_Ips.Read_From_Base();
      List_Element_Accord_Avs6_Ips.Begin();
    }
    if (AVS6_From_Avs6Main._list_recordFields == null || AVS6_From_Avs6Main._list_recordFields.Count == 0)
      this.tabControl_Nastr.TabPages.Remove(this.tabPage_Avs6);
    this.isCreate = false;
  }

  private void A_NastrTabl_Shown(object sender, EventArgs e)
  {
    if (this._one_Tabl_Nastr_Tmp._algorithmToPrint != null && (this._one_Tabl_Nastr_Tmp._algorithmToPrint._list_OneRazdelToPrint != null || this._one_Tabl_Nastr_Tmp._algorithmToPrint._oneRecordToPrint_Info != null))
      return;
    int num = (int) MessageBox.Show("Для данного типа документа отсутствуют настройки вывода", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
  }

  /// <summary> Рисование ВСЕХ страниц </summary>
  private void Draw_All()
  {
    this.Draw_Page_Bases();
    this.Draw_Page_Sbor();
    this.docKcontainer_Vyvod.Width = 355;
    this.docKcontainer_Xml.Width = 355;
    this.docKcontainer_Vyvod.Location = new Point(1218, 1);
    this.docKcontainer_Xml.Location = new Point(1218, 1);
    this.dockcontainer_Avs.Width = 355;
    this.dockcontainer_Avs.Location = new Point(1218, 1);
    this.Draw_Page_Vyvod();
    this.Draw_Page_Xml();
    this.Draw_Page_Service();
    if (List_Element_Accord_Avs6_Ips.Find(this._one_Tabl_Nastr_Curr._imsObjectType.ObjectName) && AvsConfig.General.AskAVS6)
    {
      if (this.tabControl_Nastr.TabPages.Count < 6)
        this.tabControl_Nastr.TabPages.Insert(4, this.tabPage_Avs6);
    }
    else
      this.tabControl_Nastr.TabPages.Remove(this.tabPage_Avs6);
    this.Draw_Page_Avs();
    this.IsButtonDefault();
    this.IsButtonCopyFrom();
    this.isByloButtonTypeVedTo_Click = false;
  }

  /// <summary> Открыть или спрятать кнопки Save </summary>
  /// <param name="isModified"></param>
  private void ModifiedAll(bool isModifiedAll)
  {
    if (this.isCreate)
    {
      this.IsModifiedAll = false;
      this.buttonSave1.Enabled = false;
      this.IsModified_Page_Sbor = false;
      this.IsModified_Page_Vyvod = false;
      this.IsModified_Page_Xml = false;
      this.IsModified_Page_Service = false;
    }
    else
    {
      if (isModifiedAll)
      {
        this.IsModifiedAll = true;
        this.buttonSave1.Enabled = true;
        this.IsBylo_IsModified = true;
      }
      else
      {
        this.IsModifiedAll = false;
        this.buttonSave1.Enabled = false;
        this.IsModified_Page_Sbor = false;
        this.IsModified_Page_Vyvod = false;
        this.IsModified_Page_Xml = false;
        this.IsModified_Page_Service = false;
      }
      if (!this.buttonSave1.Enabled)
        return;
      if (this._one_Tabl_Nastr_Tmp._accessLevel == 0)
      {
        if (!(Vedomost_VB_Static.UserName != "Системный администратор"))
          return;
        this.buttonSave1.Enabled = false;
      }
      else
      {
        if (this._one_Tabl_Nastr_Tmp._accessLevel != 1 || !(Vedomost_VB_Static.UserName != "Системный администратор") || Vedomost_VB_Static.IsAdmin)
          return;
        this.buttonSave1.Enabled = false;
      }
    }
  }

  /// <summary> Сохранение редактирования страниц в _one_Tabl_Nastr_Tmp </summary>
  private void Saving_Pages()
  {
    this.Saving_Page_Bases();
    this.Saving_Page_Sbor();
    this.Saving_Page_Vyvod();
    this.Saving_Page_Service();
  }

  private void buttonSave1_Click(object sender, EventArgs e) => this.Save();

  /// <summary> Сохранение _one_Tabl_Nastr_Curr </summary>
  private string Save()
  {
    if (this.IsModifiedFromFile && MessageBox.Show("Параметры настройки были изменены данными из файла (Dump)\r\n\r\nСохранить?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
      return "";
    this.Saving_Pages();
    if (this._one_Tabl_Nastr_Tmp._nameVed == null || this._one_Tabl_Nastr_Tmp._nameVed == "")
      this._one_Tabl_Nastr_Tmp._nameVed = this._imsObjectType_Curr.ObjectName;
    if (!this.is_one_Ved_Nastr_New)
    {
      Vedomost_VB_Static.One_Ved_Nastr_Copy(this._one_Tabl_Nastr_Tmp, this._one_Tabl_Nastr_Curr);
      XmlDocument xmlDocument = this._one_Tabl_Nastr_Curr.XmlDocument_create();
      string str;
      try
      {
        str = Vedomost_VB_Static.WriteXmlNastrToBase(xmlDocument, this._one_Tabl_Nastr_Curr._vedomostTemplateObjectGuid);
      }
      catch
      {
        return "Неопределенная ошибка сохранения";
      }
      if (str != "")
        return str;
      if (Vedomost_VB_Static.isCreateDump_Tmp || Vedomost_VB_Static.isComputerName_Victor || Vedomost_VB_Static.isHozain)
        xmlDocument.Save(Vedomost_VB_Static.DirectoryDump + "\\onenastr.xml");
      if (Vedomost_VB_Static.IsUse_New_System_ByOneNastr)
        Vedomost_VB_Static.Write_One_Ved_Nastr_Directly_ToBase(this._one_Tabl_Nastr_Curr, true);
      this.ModifiedAll(false);
      this.IsModifiedFromFile = false;
      this.Text = "Настройка ведомости:";
      this.Text = $"{this.Text} [{this._imsObjectType_Curr.ObjectName}]";
      if (this._one_Tabl_Nastr_Curr._dateIni != "")
        this.Text = $"{this.Text} {this._one_Tabl_Nastr_Curr._dateIni}";
    }
    else
    {
      string vedByTemplateGuid = Vedomost_VB_Static.Get_NameTypeVed_By_TemplateGuid(Vedomost_VB_Static.List_Conformity_Template_Nastr_Tabl, this._guidTemplateTabl_Curr);
      string str = "Шаблон, данной настройки, уже применяется в другом типе документа";
      if (vedByTemplateGuid != "")
        str = $"{str}\r\n\r\n({vedByTemplateGuid})";
      int num = (int) MessageBox.Show(str + "\r\n\r\nПараметры настройки не могут быть сохранены", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }
    return "";
  }

  /// <summary> При ПОПЫТКЕ закрыть окно диалога </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void A_NastrTabl_FormClosing(object sender, FormClosingEventArgs e)
  {
    if (!this.noClosing)
      return;
    e.Cancel = true;
    this.noClosing = false;
  }

  /// <summary> Нажатие кнопки OK </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void bOK_Click(object sender, EventArgs e)
  {
    if (this.IsModifiedAll && this.buttonSave1.Enabled && this.Save() != "")
      this.noClosing = true;
    else
      this.Close();
  }

  /// <summary> Нажатие кнопки "По умолчанию" </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void buttonDefault_Click(object sender, EventArgs e)
  {
    switch (this.tabControl_Nastr.SelectedTab.Name)
    {
      case "tabPage_Bases":
        Vedomost_VB.Bases_Options_Ved basesOptionsVed = Tabl_Static.Bases_Options_Ved_Init(this._one_Tabl_Nastr_Curr._typeVed);
        if (basesOptionsVed == null)
          break;
        this._one_Tabl_Nastr_Tmp._bases_Options_Ved = basesOptionsVed;
        this.Draw_Page_Bases();
        this.ModifiedAll(true);
        this.IsModified_Page_Bases = true;
        break;
      case "tabPage_Sbor":
        List<Vedomost_VB.OneFieldSpForRead> oneFieldSpForReadList = Tabl_Static.List_Tabl_ID_Init(this._one_Tabl_Nastr_Curr._typeVed);
        if (oneFieldSpForReadList == null)
          break;
        this._one_Tabl_Nastr_Tmp._list_Ved_ID = oneFieldSpForReadList;
        this.Draw_Page_Sbor();
        this.ModifiedAll(true);
        this.IsModified_Page_Sbor = true;
        break;
      case "tabPage_Vyvod":
        this.Default_Vyvod();
        this.ModifiedAll(true);
        this.IsModified_Page_Vyvod = true;
        break;
      case "tabPage_Xml":
        Vedomost_VB.AlgorithmXml algorithmXml = Tabl_Static.AlgorithmXml_Init_By_TypeTabl(this._one_Tabl_Nastr_Curr._typeVed);
        if (algorithmXml == null)
          break;
        this._one_Tabl_Nastr_Tmp._algorithmXml = algorithmXml;
        this.Draw_Page_Xml();
        this.ModifiedAll(true);
        this.IsModified_Page_Xml = true;
        break;
      case "tabPage_Avs6":
        Vedomost_VB.Algorithm_Avs6_To_Ips ipsInitByTypeTabl = Tabl_Static.Algorithm_Avs6_To_Ips_Init_By_TypeTabl(this._one_Tabl_Nastr_Curr._typeVed);
        if (ipsInitByTypeTabl == null)
          break;
        this._one_Tabl_Nastr_Tmp._algorithm_Avs6_To_Ips = ipsInitByTypeTabl;
        this.Draw_Page_Avs();
        this.ModifiedAll(true);
        this.IsModified_Page_Avs = true;
        break;
      case "tabPage_Service":
        this.ModifiedAll(true);
        this.IsModified_Page_Service = true;
        break;
    }
  }

  /// <summary> Выключение и включение постраничной кнопки "По умолчанию" </summary>
  private void IsButtonDefault()
  {
    if (this._one_Tabl_Nastr_Tmp._typeCreate == Vedomost_VB.TypeCreate.System)
    {
      this.isButtonDefault = true;
      this.buttonDefault.Visible = true;
      this.buttonServicesDefaultAll.Visible = true;
      this.label_ServicesDefaultAll.Visible = true;
    }
    else
    {
      Vedomost_VB.TypeVed typeVed = this._one_Tabl_Nastr_Tmp._typeVed;
      for (int index = 0; index < Vedomost_VB_Static.List_TypeTabl_Systems.Count; ++index)
      {
        if (Vedomost_VB_Static.List_TypeTabl_Systems[index].typeVed == typeVed)
        {
          this.isButtonDefault = true;
          this.buttonDefault.Visible = true;
          this.buttonServicesDefaultAll.Visible = true;
          this.label_ServicesDefaultAll.Visible = true;
          return;
        }
      }
      this.isButtonDefault = false;
      this.buttonDefault.Visible = false;
      this.buttonServicesDefaultAll.Visible = false;
      this.label_ServicesDefaultAll.Visible = false;
    }
  }

  /// <summary> Выключение и включение нижней кнопки "Копировать из ..." </summary>
  private void IsButtonCopyFrom()
  {
    if (this._one_Tabl_Nastr_Tmp._typeCreate == Vedomost_VB.TypeCreate.System)
    {
      this.buttonCopyFrom.Visible = false;
      this.buttonServicesCopyAll.Visible = false;
      this.label_ServicesCopyAll.Visible = false;
      this.buttonServicesTypeVedTo.Visible = false;
      this.label_ServicesTypeVedTo.Visible = false;
    }
    else if (this._list_Tabl_Arbeit_ImsObjectType_With_One_Tabl_Nastr.Count > 1)
    {
      this.buttonCopyFrom.Visible = true;
      this.buttonServicesCopyAll.Visible = true;
      this.label_ServicesCopyAll.Visible = true;
      this.buttonServicesTypeVedTo.Visible = true;
      this.label_ServicesTypeVedTo.Visible = true;
    }
    else
    {
      this.buttonCopyFrom.Visible = false;
      this.buttonServicesCopyAll.Visible = false;
      this.label_ServicesCopyAll.Visible = false;
      this.buttonServicesTypeVedTo.Visible = false;
      this.label_ServicesTypeVedTo.Visible = false;
    }
  }

  /// <summary> Изменение текущей страницы </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tabControl_Nastr_SelectedIndexChanged(object sender, EventArgs e)
  {
    System.Windows.Forms.TabPage selectedTab = this.tabControl_Nastr.SelectedTab;
    this.IsButtonDefault();
    this.IsButtonCopyFrom();
    if (this._list_Tabl_Arbeit_ImsObjectType_With_One_Tabl_Nastr.Count > 1)
      this.buttonSelectTabl.Visible = true;
    else
      this.buttonSelectTabl.Visible = false;
    switch (selectedTab.Name)
    {
      case "tabPage_Vyvod":
        this.List_Ved_Id_Draw(this.listBox_Vyvod_List_Ved_Id);
        break;
      case "tabPage_Service":
        this.buttonCopyFrom.Visible = false;
        this.buttonDefault.Visible = false;
        if (this._list_Tabl_Arbeit_ImsObjectType_With_One_Tabl_Nastr.Count > 1)
        {
          if (this._one_Tabl_Nastr_Tmp._typeCreate != Vedomost_VB.TypeCreate.System)
          {
            this.buttonServicesCopyAll.Visible = true;
            this.buttonServicesTypeVedTo.Visible = true;
            this.label_ServicesTypeVedTo.Visible = true;
            this.label_ServicesCopyAll.Visible = true;
          }
          else
          {
            this.buttonServicesCopyAll.Visible = false;
            this.buttonServicesTypeVedTo.Visible = false;
            this.label_ServicesTypeVedTo.Visible = false;
            this.label_ServicesCopyAll.Visible = false;
          }
        }
        else
        {
          this.buttonServicesCopyAll.Visible = false;
          this.buttonServicesTypeVedTo.Visible = false;
          this.label_ServicesTypeVedTo.Visible = false;
          this.label_ServicesCopyAll.Visible = false;
        }
        if (this._one_Tabl_Nastr_Tmp._typeCreate != Vedomost_VB.TypeCreate.System && this._list_Tabl_Arbeit_ImsObjectType_With_One_Tabl_Nastr.Count > 1)
        {
          this.buttonServicesTypeVedTo.Visible = true;
          this.label_ServicesTypeVedTo.Visible = true;
          this.labelService1.Text = "Таблица пользовательская";
          this.labelService2.Text = "";
          string str = Vedomost_VB_Static.TypeVed_string(this._one_Tabl_Nastr_Tmp._typeVed);
          if (string.IsNullOrEmpty(str))
            break;
          this.labelService2.Text = "Аналог: " + str;
          break;
        }
        this.buttonServicesTypeVedTo.Visible = false;
        this.label_ServicesTypeVedTo.Visible = false;
        this.labelService1.Text = "Таблица системная";
        this.labelService2.Text = "";
        break;
    }
  }

  /// <summary> Выбор другого типа таблицы </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void buttonSelectTabl_Click(object sender, EventArgs e)
  {
    if (this.IsModifiedAll)
    {
      DialogResult dialogResult = MessageBox.Show("Параметры настройки изменены\r\n\r\nСохранить?", "Внимание!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
      if (dialogResult == DialogResult.Yes && this.Save() != "")
        dialogResult = DialogResult.Cancel;
      if (dialogResult == DialogResult.No)
        this.ModifiedAll(false);
      if (dialogResult == DialogResult.Cancel)
        return;
    }
    VyborVedomosti vyborVedomosti = new VyborVedomosti();
    vyborVedomosti._typeDoc = Vedomost_VB.TypeDoc.Tabl;
    using (vyborVedomosti)
    {
      vyborVedomosti._imsObjectTypeDel = this._imsObjectType_Curr;
      vyborVedomosti._caption = "Перейти к настройке таблицы ...";
      vyborVedomosti._list_ImsObjectType_With_One_Ved_Nastrs = Vedomost_VB_Static._list_Tabl_Arbeit_ImsObjectType_With_One_Ved_Nastr;
      if (vyborVedomosti.ShowDialog() != DialogResult.OK)
        return;
      if (!Vedomost_VB_Static.IsUse_New_System_ByOneNastr)
      {
        One_ImsObjectType_With_One_Ved_Nastr typeWithOneVedNastr = Vedomost_VB_Static.Checking_Use_Template(Vedomost_VB_Static._list_Tabl_Arbeit_ImsObjectType_With_One_Ved_Nastr, vyborVedomosti._guidTemplateVed_Result, vyborVedomosti._one_ImsObjectType_With_One_Ved_Nastr.imsObjectType.ObjectName);
        if (typeWithOneVedNastr != null)
        {
          int num = (int) MessageBox.Show($"{$"В документе \"{vyborVedomosti._one_ImsObjectType_With_One_Ved_Nastr.imsObjectType.ObjectName}\"" + "\r\nнастроено использование шаблона, который уже используется в другом документе"}\r\n\r\n\"{typeWithOneVedNastr.imsObjectType.ObjectName}\"" + "\r\n\r\nЭто не допускается" + "\r\n\r\nКаждому типу документа должен соответствовать свой шаблон", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
          return;
        }
      }
      this.is_one_Ved_Nastr_New = false;
      this._guidTemplateTabl_Curr = vyborVedomosti._guidTemplateVed_Result;
      this._guidTypeTabl_Curr = vyborVedomosti._guidTypeVed_Result;
      this._documentName_Curr = vyborVedomosti._documentName_Result;
      this._imsObjectType_Curr = vyborVedomosti._imsObjectType_Result;
      this._one_Tabl_Nastr_Curr = vyborVedomosti._one_Ved_Nastr_Result;
      this.Processing_Template(this._guidTemplateTabl_Curr);
      this.isCreate = true;
      if (vyborVedomosti._one_Ved_Nastr_Result != null)
      {
        this._one_Tabl_Nastr_Curr = Vedomost_VB_Static.One_Ved_Nastr_Copy(vyborVedomosti._one_Ved_Nastr_Result);
        this._one_Tabl_Nastr_Tmp = Vedomost_VB_Static.One_Ved_Nastr_Copy(vyborVedomosti._one_Ved_Nastr_Result);
      }
      else
      {
        this._one_Tabl_Nastr_Curr = new One_Ved_Nastr(true);
        this._one_Tabl_Nastr_Curr._vedomostTemplateObjectGuid = this._guidTemplateTabl_Curr;
        this._one_Tabl_Nastr_Curr._imsObjectType = this._imsObjectType_Curr;
        this._one_Tabl_Nastr_Tmp = Vedomost_VB_Static.One_Ved_Nastr_Copy(this._one_Tabl_Nastr_Curr);
        this.is_one_Ved_Nastr_New = true;
        int num = (int) MessageBox.Show("Для данного типа документа настройки отсутствуют", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
      this.Text = "Настройка таблицы:";
      this.Text = $"{this.Text} [{this._imsObjectType_Curr.ObjectName}]";
      if (this._one_Tabl_Nastr_Curr._dateIni != "")
        this.Text = $"{this.Text} {this._one_Tabl_Nastr_Curr._dateIni}";
      this.Draw_All();
      this.IsModifiedFromFile = false;
      this.tabControl_Nastr.SelectedTab = this.tabPage_Bases;
      this.isCreate = false;
      Vedomost_VB_Static.xmlProtocol_Last = (XmlDocument) null;
      Vedomost_VB_Static.xml_SborMainVed_Dump_Last = (XmlDocument) null;
      Vedomost_VB_Static.xml_SborVed_Dump_Last = (XmlDocument) null;
      Vedomost_VB_Static.imDocument = (ImDocument) null;
    }
  }

  /// <summary>Кнопка Копировать из ...</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void buttonCopyFrom_Click(object sender, EventArgs e)
  {
    VyborVedomosti vyborVedomosti = new VyborVedomosti();
    vyborVedomosti._typeDoc = Vedomost_VB.TypeDoc.Tabl;
    using (vyborVedomosti)
    {
      vyborVedomosti._list_ImsObjectType_With_One_Ved_Nastrs = Vedomost_VB_Static._list_Tabl_Arbeit_ImsObjectType_With_One_Ved_Nastr;
      vyborVedomosti._imsObjectTypeDel = this._imsObjectType_Curr;
      if (vyborVedomosti.ShowDialog() != DialogResult.OK)
        return;
      bool flag = true;
      if (this._one_Tabl_Nastr_Tmp._typeVed != vyborVedomosti._one_Ved_Nastr_Result._typeVed && MessageBox.Show("Типы таблиц не совпадают\r\n\r\nКопировать настройки?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
        flag = false;
      if (!flag)
        return;
      switch (this.tabControl_Nastr.SelectedTab.Name)
      {
        case "tabPage_Sbor":
          this._one_Tabl_Nastr_Tmp._list_Ved_ID = Vedomost_VB_Static.List_Ved_ID_Copy(vyborVedomosti._one_Ved_Nastr_Result._list_Ved_ID);
          this.Draw_Page_Sbor();
          this.ModifiedAll(true);
          this.IsModified_Page_Sbor = true;
          break;
        case "tabPage_Xml":
          this.ModifiedAll(true);
          break;
        case "tabPage_Vyvod":
          this._one_Tabl_Nastr_Tmp._algorithmToPrint = Vedomost_VB_Static.AlgorithmToPrint_Copy(vyborVedomosti._one_Ved_Nastr_Result._algorithmToPrint);
          this.algorithmToPrint = this._one_Tabl_Nastr_Tmp._algorithmToPrint;
          this._one_Tabl_Nastr_Tmp._algorithmToPrint_B = (Vedomost_VB.AlgorithmToPrint) null;
          this.algorithmToPrint_curr = this.algorithmToPrint;
          this.Draw_Page_Vyvod();
          this.ModifiedAll(true);
          this.IsModified_Page_Vyvod = true;
          break;
        case "tabPage_Service":
          this.Draw_Page_Service();
          this.ModifiedAll(true);
          this.IsModified_Page_Service = true;
          break;
      }
    }
  }

  /// <summary> Рисование списка собранных атрибутов системы  </summary>
  /// <param name="listBox"></param>
  private void List_Ved_Id_Draw(ListBox listBox)
  {
    listBox.Items.Clear();
    for (int index = 0; index < this._one_Tabl_Nastr_Tmp._list_Ved_ID.Count; ++index)
    {
      string name = this._one_Tabl_Nastr_Tmp._list_Ved_ID[index]._name;
      listBox.Items.Add((object) name);
    }
    listBox.SelectedIndex = -1;
  }

  /// <summary> Установить курсор по тексту строки </summary>
  /// <param name="listBox"></param>
  /// <param name="text"></param>
  private void List_Ved_Id_SelectedValue(ListBox listBox, string text)
  {
    if (string.IsNullOrEmpty(text))
    {
      listBox.SelectedIndex = -1;
    }
    else
    {
      for (int index = 0; index < listBox.Items.Count; ++index)
      {
        string str = listBox.Items[index].ToString();
        if (text == str)
        {
          listBox.SelectedIndex = index;
          break;
        }
      }
    }
  }

  /// <summary> Установить курсор по objType </summary>
  /// <param name="listBox"></param>
  /// <param name="list_Ved_ID"></param>
  /// <param name="objType"></param>
  private void List_Ved_Id_SelectedByObjType(
    ListBox listBox,
    List<Vedomost_VB.OneFieldSpForRead> list_Ved_ID,
    int objType)
  {
    if (objType == 0 || objType == -1)
    {
      listBox.SelectedIndex = -1;
    }
    else
    {
      for (int index = 0; index < list_Ved_ID.Count; ++index)
      {
        Vedomost_VB.OneFieldSpForRead oneFieldSpForRead = list_Ved_ID[index];
        if (objType == oneFieldSpForRead._id)
        {
          listBox.SelectedIndex = index;
          return;
        }
      }
      listBox.SelectedIndex = -1;
    }
  }

  /// <summary> AttrId по номеру </summary>
  /// <param name="list_Ved_ID"></param>
  /// <param name="index"></param>
  /// <returns></returns>
  private int Get_ObjType_By_index(List<Vedomost_VB.OneFieldSpForRead> list_Ved_ID, int index)
  {
    return index < 0 ? -1 : list_Ved_ID[index]._id;
  }

  /// <summary> СЛУШАЮ И ПОВИНУЮСЬ </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void panelForButtons_MouseClick(object sender, MouseEventArgs e)
  {
    if ((Control.ModifierKeys & Keys.Control) != Keys.Control || (Control.ModifierKeys & Keys.Alt) != Keys.Alt)
      return;
    Vedomost_VB_Static.isHozain = true;
    int num = (int) MessageBox.Show("Слушаю и повинуюсь", "ПРИВЕТ!");
  }

  private void Draw_Page_Bases()
  {
    this.checkBox_Usl_Bases_Sbor_isInputDoc.Checked = this._one_Tabl_Nastr_Tmp._bases_Options_Ved._isInputDoc;
    this.checkBox_Usl_Bases_Sbor_isInputIzd.Checked = this._one_Tabl_Nastr_Tmp._bases_Options_Ved._isInputIzd;
    this.checkBox_Usl_Bases_Sbor_isInputMat.Checked = this._one_Tabl_Nastr_Tmp._bases_Options_Ved._isInputMat;
    this.listBox_QuickObjectInfo.Items.Clear();
    if (this._one_Tabl_Nastr_Tmp._bases_Options_Ved == null || this._one_Tabl_Nastr_Tmp._bases_Options_Ved._list_quickObjectInfo == null)
      this._one_Tabl_Nastr_Tmp._bases_Options_Ved = Vedomost_VB_Static.Bases_Options_Ved_Init(this._one_Tabl_Nastr_Tmp._typeVed);
    if (this._one_Tabl_Nastr_Tmp._bases_Options_Ved._list_quickObjectInfo != null)
    {
      for (int index = 0; index < this._one_Tabl_Nastr_Tmp._bases_Options_Ved._list_quickObjectInfo.Count; ++index)
      {
        QuickObjectInfo quickObjectInfo_Del = this._one_Tabl_Nastr_Tmp._bases_Options_Ved._list_quickObjectInfo[index];
        if (!string.IsNullOrEmpty(quickObjectInfo_Del.Caption))
          this.listBox_QuickObjectInfo.Items.Add((object) quickObjectInfo_Del.Caption);
        this.Delete_From_list_CalalogsImbaseTmp(quickObjectInfo_Del);
        if (this.listBox_CatalogsImbase.Items.Count > 0)
          this.button_Add_To_listBox_QuickObjectInfo.Enabled = true;
        else
          this.button_Add_To_listBox_QuickObjectInfo.Enabled = false;
        this.button_Delete_From_To_listBox_QuickObjectInfo.Enabled = true;
      }
    }
    else
      this.button_Add_To_listBox_QuickObjectInfo.Enabled = false;
    this.Draw_listBox_CatalogsImbase();
  }

  /// <summary> Рисование listBox_CatalogsImbase в соответствии с list_CalalogsImbaseTmp </summary>
  private void Draw_listBox_CatalogsImbase()
  {
    this.listBox_CatalogsImbase.Items.Clear();
    for (int index = 0; index < this.list_CalalogsImbaseTmp.Count; ++index)
      this.listBox_CatalogsImbase.Items.Add((object) this.list_CalalogsImbaseTmp[index].Caption);
    if (this.listBox_CatalogsImbase.Items.Count > 0)
      this.button_Add_To_listBox_QuickObjectInfo.Enabled = true;
    else
      this.button_Add_To_listBox_QuickObjectInfo.Enabled = false;
    if (this._one_Tabl_Nastr_Tmp._bases_Options_Ved._list_quickObjectInfo.Count > 0)
      this.button_Delete_From_To_listBox_QuickObjectInfo.Enabled = true;
    else
      this.button_Delete_From_To_listBox_QuickObjectInfo.Enabled = false;
  }

  /// <summary> Кнопка -&gt;&gt; </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void button_Add_To_listBox_QuickObjectInfo_Click(object sender, EventArgs e)
  {
    if (this.listBox_CatalogsImbase.SelectedIndex < 0)
      return;
    int selectedIndex = this.listBox_CatalogsImbase.SelectedIndex;
    QuickObjectInfo quickObjectInfo = this.list_CalalogsImbaseTmp[selectedIndex];
    this.list_CalalogsImbaseTmp.RemoveAt(selectedIndex);
    this._one_Tabl_Nastr_Tmp._bases_Options_Ved._list_quickObjectInfo.Add(quickObjectInfo);
    this.listBox_QuickObjectInfo.Items.Add((object) quickObjectInfo.Caption);
    this.Draw_listBox_CatalogsImbase();
    if (selectedIndex > 0)
      this.listBox_CatalogsImbase.SelectedIndex = selectedIndex - 1;
    else if (this.listBox_CatalogsImbase.Items.Count == 0)
      this.listBox_CatalogsImbase.SelectedIndex = -1;
    else
      this.listBox_CatalogsImbase.SelectedIndex = 0;
    this.ModifiedAll(true);
    this.IsModified_Page_Bases = true;
  }

  /// <summary> Удаление из списка выбранных Каталогов (Кнопка  &lt;&lt;-) </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void button_Delete_From_To_listBox_QuickObjectInfo_Click(object sender, EventArgs e)
  {
    if (this.listBox_QuickObjectInfo.SelectedIndex < 0)
      return;
    int selectedIndex = this.listBox_QuickObjectInfo.SelectedIndex;
    QuickObjectInfo quickObjectInfo = this._one_Tabl_Nastr_Tmp._bases_Options_Ved._list_quickObjectInfo[selectedIndex];
    this._one_Tabl_Nastr_Tmp._bases_Options_Ved._list_quickObjectInfo.RemoveAt(selectedIndex);
    this.listBox_QuickObjectInfo.Items.RemoveAt(selectedIndex);
    this.list_CalalogsImbaseTmp.Add(quickObjectInfo);
    this.Draw_listBox_CatalogsImbase();
    if (selectedIndex > 0)
      this.listBox_QuickObjectInfo.SelectedIndex = selectedIndex - 1;
    else if (this.listBox_QuickObjectInfo.Items.Count == 0)
      this.listBox_QuickObjectInfo.SelectedIndex = -1;
    else
      this.listBox_QuickObjectInfo.SelectedIndex = 0;
    this.ModifiedAll(true);
    this.IsModified_Page_Bases = true;
  }

  /// <summary> Удаление из list_CalalogsImbaseTmp и listBox_CatalogsImbase </summary>
  /// <param name="quickObjectInfo_Del"></param>
  private void Delete_From_list_CalalogsImbaseTmp(QuickObjectInfo quickObjectInfo_Del)
  {
    if (quickObjectInfo_Del.Empty)
      return;
    for (int index = 0; index < this.list_CalalogsImbaseTmp.Count; ++index)
    {
      if (this.list_CalalogsImbaseTmp[index].Caption == quickObjectInfo_Del.Caption)
      {
        this.list_CalalogsImbaseTmp.RemoveAt(index);
        break;
      }
    }
    this.Draw_listBox_CatalogsImbase();
  }

  private void checkBox_Usl_Bases_Sbor_isInputDoc_CheckedChanged(object sender, EventArgs e)
  {
    this.ModifiedAll(true);
    this.IsModified_Page_Bases = true;
  }

  private void checkBox_Usl_Bases_Sbor_isInputIzd_CheckedChanged(object sender, EventArgs e)
  {
    this.ModifiedAll(true);
    this.IsModified_Page_Bases = true;
  }

  private void checkBox_Usl_Bases_Sbor_isInputMat_CheckedChanged(object sender, EventArgs e)
  {
    this.ModifiedAll(true);
    this.IsModified_Page_Bases = true;
  }

  /// <summary> Сохранение редактирования страницы "Основные" в _one_Tabl_Nastr_Tmp </summary>
  private void Saving_Page_Bases()
  {
    this._one_Tabl_Nastr_Tmp._bases_Options_Ved._isInputDoc = this.checkBox_Usl_Bases_Sbor_isInputDoc.Checked;
    this._one_Tabl_Nastr_Tmp._bases_Options_Ved._isInputIzd = this.checkBox_Usl_Bases_Sbor_isInputIzd.Checked;
    this._one_Tabl_Nastr_Tmp._bases_Options_Ved._isInputMat = this.checkBox_Usl_Bases_Sbor_isInputMat.Checked;
  }

  /// <summary> Рисование СТРАНИЦЫ СБОР </summary>
  private void Draw_Page_Sbor() => this.Draw_PodPage_Sbor_Peredatha();

  /// <summary> Рисование подстраницы "Передача данных" </summary>
  private void Draw_PodPage_Sbor_Peredatha()
  {
    this.select_Sbor_Peredatha_AttributeControl2.Select((NodeColumnCollection) null, (List<AVSColumnScheme>) null);
    this.listBox_Sbor_Peredatha_ListId_Draw();
  }

  /// <summary> Рисование списка передаваемых атрибутов </summary>
  private void listBox_Sbor_Peredatha_ListId_Draw()
  {
    this.listBox_Sbor_Peredatha_ListId.Items.Clear();
    if (Vedomost_VB_Static._listObligatoryId.Count == 0)
      Vedomost_VB_Static.ListObligatoryId_Filled();
    for (int index1 = 0; index1 < this._one_Tabl_Nastr_Tmp._list_Ved_ID.Count; ++index1)
    {
      Vedomost_VB.OneFieldSpForRead oneFieldSpForRead1 = this._one_Tabl_Nastr_Tmp._list_Ved_ID[index1];
      string str = oneFieldSpForRead1._name;
      bool flag = false;
      for (int index2 = 0; index2 < Vedomost_VB_Static._listObligatoryId.Count; ++index2)
      {
        Vedomost_VB.OneFieldSpForRead oneFieldSpForRead2 = Vedomost_VB_Static._listObligatoryId[index2];
        if (oneFieldSpForRead1._name == oneFieldSpForRead2._name)
        {
          flag = true;
          break;
        }
      }
      if (flag)
        str = "*" + str;
      this.listBox_Sbor_Peredatha_ListId.Items.Add((object) str);
    }
  }

  /// <summary> Добавить имя в список передаваемых атрибутов </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void button_Sbor_Peredatha_Add2_Click(object sender, EventArgs e)
  {
    int isUzeEst = -1;
    if (this.select_Sbor_Peredatha_AttributeControl2.SelectedAttributeId == -1)
    {
      int num1 = (int) MessageBox.Show("Не выбран атрибут", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    else
    {
      AvsRowAttributeInfo specRowAttributeInfo = new AvsRowAttributeInfo(this.select_Sbor_Peredatha_AttributeControl2.SelectedAttribute);
      if (specRowAttributeInfo == null || this.Add_Id_To_list_Tabl_ID(specRowAttributeInfo, out isUzeEst) || isUzeEst <= -1)
        return;
      this.listBox_Sbor_Peredatha_ListId.SelectedIndex = isUzeEst;
      int num2 = (int) MessageBox.Show("Данный атрибут в списке уже есть", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }
  }

  /// <summary> Добавление в список ВВОДИМЫХ </summary>
  /// <param name="specRowAttributeInfo"></param>
  /// <param name="isUzeEst"></param>
  /// <returns></returns>
  private bool Add_Id_To_list_Tabl_ID(AvsRowAttributeInfo specRowAttributeInfo, out int isUzeEst)
  {
    if (specRowAttributeInfo == null)
    {
      isUzeEst = -1;
      return false;
    }
    for (int index = 0; index < this._one_Tabl_Nastr_Tmp._list_Ved_ID.Count; ++index)
    {
      Vedomost_VB.OneFieldSpForRead oneFieldSpForRead = this._one_Tabl_Nastr_Tmp._list_Ved_ID[index];
      if (specRowAttributeInfo.AttributeId == oneFieldSpForRead._id)
      {
        isUzeEst = index;
        return false;
      }
    }
    Vedomost_VB.OneFieldSpForRead oneFieldSpForRead1 = this.create_OneFieldSpForRead(specRowAttributeInfo, false);
    if (oneFieldSpForRead1 == null)
    {
      isUzeEst = -1;
      return false;
    }
    this._one_Tabl_Nastr_Tmp._list_Ved_ID.Add(oneFieldSpForRead1);
    this.listBox_Sbor_Peredatha_ListId.Items.Add((object) oneFieldSpForRead1._name);
    this.ModifiedAll(true);
    this.IsModified_Page_Sbor = true;
    isUzeEst = this.listBox_Sbor_Peredatha_ListId.Items.Count - 1;
    return true;
  }

  /// <summary> Создание OneFieldSpForRead на основе выбранного атрибута </summary>
  /// <param name="specRowAttributeInfo"></param>
  /// <returns></returns>
  private Vedomost_VB.OneFieldSpForRead create_OneFieldSpForRead(
    AvsRowAttributeInfo specRowAttributeInfo,
    bool quiet)
  {
    int attributeId = specRowAttributeInfo.AttributeId;
    AttributeSourceTypes attr = AttributeSourceTypes.Auto;
    Vedomost_VB.TypeDataSel attrType = Vedomost_VB.TypeDataSel.Undefined;
    if (specRowAttributeInfo.AttrSrc == FieldSource.Object)
      attr = AttributeSourceTypes.Object;
    if (attr == AttributeSourceTypes.Auto)
    {
      if (!quiet)
      {
        int num = (int) MessageBox.Show("Не определено\r\nЭто атрибут объекта или связи", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      return (Vedomost_VB.OneFieldSpForRead) null;
    }
    switch (specRowAttributeInfo.FieldType)
    {
      case FieldTypes.ftString:
        attrType = Vedomost_VB.TypeDataSel.String;
        break;
      case FieldTypes.ftInteger:
        attrType = Vedomost_VB.TypeDataSel.Int;
        break;
      case FieldTypes.ftDouble:
        attrType = Vedomost_VB.TypeDataSel.Float;
        break;
      case FieldTypes.ftObjectLink:
        attrType = Vedomost_VB.TypeDataSel.String;
        break;
      case FieldTypes.ftMeasured:
        attrType = Vedomost_VB.TypeDataSel.String;
        break;
      case FieldTypes.ftGuid:
        attrType = Vedomost_VB.TypeDataSel.Guid;
        break;
    }
    if (attrType != Vedomost_VB.TypeDataSel.Undefined)
      return new Vedomost_VB.OneFieldSpForRead(attributeId, attr, attrType);
    if (!quiet)
    {
      int num1 = (int) MessageBox.Show("Условие с таким типом атрибута не может быть создано", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    return (Vedomost_VB.OneFieldSpForRead) null;
  }

  /// <summary> Удалить имя из списка передаваемых атрибутов </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void button_Sbor_Peredatha_Delete2_Click(object sender, EventArgs e)
  {
    int selectedIndex = this.listBox_Sbor_Peredatha_ListId.SelectedIndex;
    if (this.listBox_Sbor_Peredatha_ListId.SelectedIndex < 0)
      return;
    if (this.listBox_Sbor_Peredatha_ListId.Items[selectedIndex].ToString()[0] == '*')
    {
      int num = (int) MessageBox.Show("Системные атрибуты (помеченные знаком *) не удаляются", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }
    else
    {
      this._one_Tabl_Nastr_Tmp._list_Ved_ID.RemoveAt(selectedIndex);
      this.listBox_Sbor_Peredatha_ListId.Items.RemoveAt(selectedIndex);
      if (this.listBox_Sbor_Peredatha_ListId.Items.Count == 0)
      {
        this.listBox_Sbor_Peredatha_ListId.SelectedIndex = -1;
      }
      else
      {
        if (selectedIndex >= this.listBox_Sbor_Peredatha_ListId.Items.Count)
          --selectedIndex;
        this.listBox_Sbor_Peredatha_ListId.SelectedIndex = selectedIndex;
      }
      this.ModifiedAll(true);
      this.IsModified_Page_Sbor = true;
    }
  }

  /// <summary> Нажатие кнопки DEL </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void listBox_Sbor_Peredatha_ListId_KeyDown(object sender, KeyEventArgs e)
  {
    if (e.KeyCode != Keys.Delete)
      return;
    this.button_Sbor_Peredatha_Delete2_Click(sender, (EventArgs) e);
  }

  /// <summary> Сохранение редактирования страницы "Сбор" в _one_Tabl_Nastr_Tmp </summary>
  private void Saving_Page_Sbor()
  {
  }

  private void listBox_Sbor_Peredatha_ListId_Click(object sender, EventArgs e)
  {
    this.select_Sbor_Peredatha_AttributeControl2.SelectedAttributeId = this._one_Tabl_Nastr_Tmp._list_Ved_ID[this.listBox_Sbor_Peredatha_ListId.SelectedIndex]._id;
  }

  private void Processing_Template(Guid template_Guid)
  {
    this.imDocument_template_Vyvod = (ImDocument) null;
    if (template_Guid == Guid.Empty)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(template_Guid, false);
      this.templateID_Vyvod = dbObject == null ? -1L : dbObject.ObjectID;
      this.templateID_curr_Vyvod = this.templateID_Vyvod;
    }
    if (this.templateID_Vyvod == 0L || this.templateID_Vyvod == -1L)
      return;
    this.imDocument_template_Vyvod = this.LoadTemplateFromObject(this.templateID_Vyvod);
  }

  /// <summary> Загрузить шаблон </summary>
  /// <param name="objectId"></param>
  /// <returns></returns>
  public ImDocument LoadTemplateFromObject(long objectId)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(objectId, false);
      if (dbObject != null)
        this.docName = dbObject.Caption;
    }
    ImDocument imDocument = (ImDocument) null;
    MemoryStream aDestStream = new MemoryStream();
    try
    {
      new BlobProcReader(objectId, AttributableElements.Object, Vedomost_VB_Static.fileAttrId, 0, 0, (Stream) aDestStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).ReadData();
      aDestStream.Position = 0L;
      if (aDestStream.Length != 0L)
      {
        imDocument = ImDocument.LoadFromStream((Stream) aDestStream, true, false, true);
      }
      else
      {
        imDocument = new ImDocument(true);
        imDocument.Modified = false;
      }
    }
    finally
    {
      aDestStream.Close();
    }
    return imDocument;
  }

  public long TemplateId_Vyvod
  {
    get => this.templID_Vyvod;
    set
    {
      this.templID_Vyvod = value;
      this.CloseAllDocs_Vyvod();
      if (this.templID_Vyvod == 0L || this.templID_Vyvod == -1L)
        return;
      this.OpenTemplate_Vyvod(this.imDocument_template_Vyvod);
    }
  }

  private void OpenTemplate_Vyvod(ImDocument doc)
  {
    this.CloseAllDocs_Vyvod();
    this.docControl_Vyvod = new DocumentControl();
    this.docControl_Vyvod.Document = doc;
    double num = (double) this.docControl_Vyvod.SetZoom(DocZoomMode.FitPage, 0.0f);
    this.docKcontrol_Vyvod = new DockControl((Control) this.docControl_Vyvod, this.docName);
    this.docKcontrol_Vyvod.Show(this.dockMan_Vyvod, DockState.Document);
    this.docKcontrol_Vyvod.Closable = false;
    this.ShowDocumentTreeView_Vyvod();
    this.docKcontrol_Vyvod.Show();
    this.docKcontrol_Vyvod.Select();
    this.docControl_Vyvod.ReadOnly = true;
  }

  protected DocumentTreeViewDlg DocumentTreeViewDlg_Vyvod
  {
    get
    {
      return this.documentTreeViewDlg_Vyvod != null && !this.documentTreeViewDlg_Vyvod.IsDisposed ? this.documentTreeViewDlg_Vyvod : (DocumentTreeViewDlg) null;
    }
  }

  public void ShowDocumentTreeView_Vyvod()
  {
    if (this.DocumentTreeViewDlg_Vyvod == null)
    {
      this.documentTreeViewDlg_Vyvod = new DocumentTreeViewDlg();
      this.documentTreeViewDlg_Vyvod.Visible = true;
    }
    if (this.docControl_Vyvod != null)
    {
      DocumentTreeNode activeElement = this.docControl_Vyvod.ActiveElement;
      this.documentTreeViewDlg_Vyvod.TreeRoot = (DocumentTreeNode) this.docControl_Vyvod.Document;
      this.documentTreeViewDlg_Vyvod.DocumentControl = this.docControl_Vyvod;
      this.documentTreeViewDlg_Vyvod.UpdateSelection();
    }
    this.documentTreeViewDlg_Vyvod.Visible = true;
    this.documentTreeViewDlg_Vyvod.Show(this.dockMan_Vyvod, DockState.DockRight);
    this.documentTreeViewDlg_Vyvod.Closable = false;
  }

  /// <summary> Очистка контейнера в окне вывода </summary>
  private void CloseAllDocs_Vyvod()
  {
    for (int index = 0; index < this.docContainer_Vyvod.Documents.Length; ++index)
      this.docContainer_Vyvod.Documents[index].Close();
  }

  /// <summary> В окне шаблона установить курсор на элемент </summary>
  /// <param name="currId"></param>
  private void SetElementStr_Vyvod(string currId)
  {
    if (currId != null && currId != "" && this.imDocument_template_Vyvod != null)
    {
      DocumentTreeNode selection = this.imDocument_template_Vyvod.FindNode(currId) ?? this.imDocument_template_Xml.FindFirstNodeByName(currId);
      if (selection != null)
      {
        this.docControl_Vyvod.SetSelection(selection, false, new Point(0, 0), true, false);
        this.docControl_Vyvod.ResetTernBufer();
        this.documentTreeViewDlg_Vyvod.UpdateSelection();
      }
      else
        this.docControl_Vyvod.SetSelection(selection, false, new Point(0, 0), true, false);
    }
    else
    {
      if (this.imDocument_template_Vyvod == null)
        return;
      this.docControl_Vyvod.SetSelection((DocumentTreeNode) null, false, new Point(0, 0), true, false);
    }
  }

  /// <summary> В окне шаблона установить курсор на элемент </summary>
  /// <param name="currId"></param>
  private void SetElementInt_Vyvod(int currId) => this.SetElementStr_Vyvod(currId.ToString());

  /// <summary> Рисование страницы ВЫВОД </summary>
  private void Draw_Page_Vyvod()
  {
    this.algorithmToPrint = this._one_Tabl_Nastr_Tmp._algorithmToPrint;
    this.algorithmToPrint_curr = this.algorithmToPrint;
    this.List_Ved_Id_Draw(this.listBox_Vyvod_List_Ved_Id);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = (IDBObject) null;
      if (this._one_Tabl_Nastr_Tmp._vedomostTemplateObjectGuid != Guid.Empty)
        dbObject = sessionKeeper.Session.GetObject(this._one_Tabl_Nastr_Tmp._vedomostTemplateObjectGuid, false);
      if (dbObject != null)
        this.templateID_Vyvod = dbObject.ObjectID;
      this.templateID_curr_Vyvod = this.templateID_Vyvod;
    }
    this.TemplateId_Vyvod = this.templateID_curr_Vyvod;
    this.treeView_Vyvod_Draw();
    this.treeView_Vyvod.SelectedNode = this.treeView_Vyvod.Nodes[0];
    this.checkBox_Vyvod_Additional1.Checked = this.algorithmToPrint_curr._additional1 != 0;
    this.checkBox_Vyvod_Additional2.Checked = this.algorithmToPrint_curr._additional2 != 0;
    this.checkBox_Vyvod_Additional3.Checked = this.algorithmToPrint_curr._additional3 != 0;
    if (this.algorithmToPrint_curr._additional4 == 0)
      this.checkBox_Vyvod_Additional4.Checked = false;
    else
      this.checkBox_Vyvod_Additional4.Checked = true;
  }

  private void checkBox_Vyvod_Additional1_CheckedChanged(object sender, EventArgs e)
  {
    if (this.isCreate)
      return;
    this.ModifiedAll(true);
    this.IsModified_Page_Vyvod = true;
    if (this.checkBox_Vyvod_Additional1.Checked)
      this.algorithmToPrint_curr._additional1 = 1;
    else
      this.algorithmToPrint_curr._additional1 = 0;
  }

  private void checkBox_Vyvod_Additional2_CheckedChanged(object sender, EventArgs e)
  {
    if (this.isCreate)
      return;
    this.ModifiedAll(true);
    this.IsModified_Page_Vyvod = true;
    if (this.checkBox_Vyvod_Additional2.Checked)
      this.algorithmToPrint_curr._additional2 = 1;
    else
      this.algorithmToPrint_curr._additional2 = 0;
  }

  private void checkBox_Vyvod_Additional3_CheckedChanged(object sender, EventArgs e)
  {
    if (this.isCreate)
      return;
    this.ModifiedAll(true);
    this.IsModified_Page_Vyvod = true;
    if (this.checkBox_Vyvod_Additional3.Checked)
      this.algorithmToPrint_curr._additional3 = 1;
    else
      this.algorithmToPrint_curr._additional3 = 0;
  }

  private void checkBox_Vyvod_Additional4_CheckedChanged(object sender, EventArgs e)
  {
    if (this.isCreate)
      return;
    this.ModifiedAll(true);
    this.IsModified_Page_Vyvod = true;
    if (this.checkBox_Vyvod_Additional4.Checked)
      this.algorithmToPrint_curr._additional4 = 1;
    else
      this.algorithmToPrint_curr._additional4 = 0;
  }

  /// <summary> Рисование ДЕРЕВА выводимых полей </summary>
  private void treeView_Vyvod_Draw()
  {
    this.treeView_Vyvod.Nodes.Clear();
    bool flag1 = this.imDocument_template_Vyvod.FindNode("Подтаблица Куда входит") != null;
    bool flag2 = this.imDocument_template_Vyvod.FindNode("Строка Кол итого") != null;
    A_NastrTabl.OneVyvodNode oneVyvodNode1 = new A_NastrTabl.OneVyvodNode();
    oneVyvodNode1.Text = "Правила вывода на шаблон: " + this.algorithmToPrint_curr._tableName;
    oneVyvodNode1.ImageIndex = this._indexImageList_Section;
    oneVyvodNode1.SelectedImageIndex = this._indexImageList_Section;
    A_NastrTabl.OneVyvodNode oneVyvodNode2 = oneVyvodNode1;
    oneVyvodNode2._oneVyvodNode_Parent = (A_NastrTabl.OneVyvodNode) null;
    oneVyvodNode2._oneGrafaToPrint = (Vedomost_VB.OneGrafaToPrint) null;
    oneVyvodNode2._oneDataFieldToPrint = (Vedomost_VB.OneDataFieldToPrint) null;
    oneVyvodNode2._oneRecordToPrint = (Vedomost_VB.OneRecordToPrint) null;
    oneVyvodNode2._typeNode = Vedomost_VB_Static.TypeNode_Tree.Main;
    this.treeView_Vyvod.Nodes.Add((TreeNode) oneVyvodNode2);
    A_NastrTabl.OneVyvodNode oneVyvodNode3 = new A_NastrTabl.OneVyvodNode();
    oneVyvodNode3.Text = "Информационные записи: ";
    oneVyvodNode3.ImageIndex = this._indexImageList_Section;
    oneVyvodNode3.SelectedImageIndex = this._indexImageList_Section;
    A_NastrTabl.OneVyvodNode oneVyvodNode4 = oneVyvodNode3;
    oneVyvodNode4._oneVyvodNode_Parent = (A_NastrTabl.OneVyvodNode) null;
    oneVyvodNode4._oneGrafaToPrint = (Vedomost_VB.OneGrafaToPrint) null;
    oneVyvodNode4._oneDataFieldToPrint = (Vedomost_VB.OneDataFieldToPrint) null;
    oneVyvodNode4._oneRecordToPrint = (Vedomost_VB.OneRecordToPrint) null;
    oneVyvodNode4._typeNode = Vedomost_VB_Static.TypeNode_Tree.Info;
    oneVyvodNode2.Nodes.Add((TreeNode) oneVyvodNode4);
    if (this.algorithmToPrint_curr._list_OneRazdelToPrint != null && this.algorithmToPrint_curr._list_OneRazdelToPrint.Count > 0)
    {
      for (int index = 0; index < this.algorithmToPrint_curr._list_OneRazdelToPrint.Count; ++index)
      {
        Vedomost_VB.OneRazdelToPrint oneRazdelToPrint = this.algorithmToPrint_curr._list_OneRazdelToPrint[index];
        if (oneRazdelToPrint._oneRecordToPrint_Info != null)
        {
          A_NastrTabl.OneVyvodNode oneVyvodNode5 = this.oneRecordNode_Create(oneRazdelToPrint._oneRecordToPrint_Info, oneVyvodNode4, oneRazdelToPrint._razdelVed);
          if (oneVyvodNode5 != null)
          {
            oneVyvodNode4.Nodes.Add((TreeNode) oneVyvodNode5);
            if (flag1 && oneRazdelToPrint._oneRecordToPrint_Info._oneRecordToPrint_Vtor != null)
            {
              A_NastrTabl.OneVyvodNode node = this.oneRecordNode_Create(oneRazdelToPrint._oneRecordToPrint_Info._oneRecordToPrint_Vtor, oneVyvodNode5);
              if (node != null)
                oneVyvodNode5.Nodes.Add((TreeNode) node);
            }
            if (flag2 && oneRazdelToPrint._oneRecordToPrint_Info._oneRecordToPrint_Itogo != null)
            {
              A_NastrTabl.OneVyvodNode node = this.oneRecordNode_Create(oneRazdelToPrint._oneRecordToPrint_Info._oneRecordToPrint_Itogo, oneVyvodNode5);
              if (node != null)
                oneVyvodNode5.Nodes.Add((TreeNode) node);
            }
          }
        }
      }
    }
    else if (this.algorithmToPrint_curr._oneRecordToPrint_Info != null)
    {
      A_NastrTabl.OneVyvodNode oneVyvodNode6 = this.oneRecordNode_Create(this.algorithmToPrint_curr._oneRecordToPrint_Info, oneVyvodNode4);
      if (oneVyvodNode6 != null)
      {
        oneVyvodNode4.Nodes.Add((TreeNode) oneVyvodNode6);
        if (flag1 && this.algorithmToPrint_curr._oneRecordToPrint_Info._oneRecordToPrint_Vtor != null)
        {
          A_NastrTabl.OneVyvodNode node = this.oneRecordNode_Create(this.algorithmToPrint_curr._oneRecordToPrint_Info._oneRecordToPrint_Vtor, oneVyvodNode6);
          if (node != null)
            oneVyvodNode6.Nodes.Add((TreeNode) node);
        }
        if (flag2 && this.algorithmToPrint_curr._oneRecordToPrint_Info._oneRecordToPrint_Itogo != null)
        {
          A_NastrTabl.OneVyvodNode node = this.oneRecordNode_Create(this.algorithmToPrint_curr._oneRecordToPrint_Info._oneRecordToPrint_Itogo, oneVyvodNode6);
          if (node != null)
            oneVyvodNode6.Nodes.Add((TreeNode) node);
        }
        oneVyvodNode6.Expand();
      }
    }
    oneVyvodNode4.Expand();
    A_NastrTabl.OneVyvodNode node1 = this.oneRecordNode_Create(this.algorithmToPrint_curr._oneRecordToPrintTitle, oneVyvodNode2);
    if (node1 != null)
      oneVyvodNode2.Nodes.Add((TreeNode) node1);
    A_NastrTabl.OneVyvodNode node2 = this.oneRecordNode_Create(this.algorithmToPrint_curr._oneRecordToPrintTitlePodSection, oneVyvodNode2);
    if (node2 != null)
      oneVyvodNode2.Nodes.Add((TreeNode) node2);
    A_NastrTabl.OneVyvodNode node3 = this.oneRecordNode_Create(this.algorithmToPrint_curr._oneRecordToPrintTitleVar, oneVyvodNode2);
    if (node3 != null)
      oneVyvodNode2.Nodes.Add((TreeNode) node3);
    A_NastrTabl.OneVyvodNode node4 = this.oneRecordNode_Create(this.algorithmToPrint_curr._oneRecordToPrintTitleIsp, oneVyvodNode2);
    if (node4 != null)
      oneVyvodNode2.Nodes.Add((TreeNode) node4);
    A_NastrTabl.OneVyvodNode node5 = this.oneRecordNode_Create(this.algorithmToPrint_curr._oneRecordToPrintRemark, oneVyvodNode2);
    if (node5 != null)
      oneVyvodNode2.Nodes.Add((TreeNode) node5);
    A_NastrTabl.OneVyvodNode node6 = this.oneRecordNode_Create(this.algorithmToPrint_curr._oneRecordToPrintRemarkShort, oneVyvodNode2);
    if (node6 != null)
      oneVyvodNode2.Nodes.Add((TreeNode) node6);
    A_NastrTabl.OneVyvodNode node7 = this.oneRecordNode_Create(this.algorithmToPrint_curr._oneRecordToPrintPasport, oneVyvodNode2);
    if (node7 != null)
      oneVyvodNode2.Nodes.Add((TreeNode) node7);
    A_NastrTabl.OneVyvodNode node8 = this.oneRecordNode_Create(this.algorithmToPrint_curr._oneRecordToPrintEmpty, oneVyvodNode2);
    if (node8 != null)
      oneVyvodNode2.Nodes.Add((TreeNode) node8);
    A_NastrTabl.OneVyvodNode node9 = this.oneRecordNode_Create(this.algorithmToPrint_curr._oneRecordToPrintAdditional1, oneVyvodNode2);
    if (node9 != null)
      oneVyvodNode2.Nodes.Add((TreeNode) node9);
    A_NastrTabl.OneVyvodNode node10 = this.oneRecordNode_Create(this.algorithmToPrint_curr._oneRecordToPrintAdditional2, oneVyvodNode2);
    if (node10 != null)
      oneVyvodNode2.Nodes.Add((TreeNode) node10);
    A_NastrTabl.OneVyvodNode node11 = this.oneRecordNode_Create(this.algorithmToPrint_curr._oneRecordToPrintAdditional3, oneVyvodNode2);
    if (node11 != null)
      oneVyvodNode2.Nodes.Add((TreeNode) node11);
    A_NastrTabl.OneVyvodNode node12 = this.oneRecordNode_Create(this.algorithmToPrint_curr._oneRecordToPrintAdditional4, oneVyvodNode2);
    if (node12 != null)
      oneVyvodNode2.Nodes.Add((TreeNode) node12);
    oneVyvodNode2.Expand();
  }

  /// <summary> Ветка, описывающая одну ЗАПИСЬ </summary>
  /// <param name="oneRecordToPrint"></param>
  /// <param name="oneVyvodNode_Parent"></param>
  /// <returns></returns>
  private A_NastrTabl.OneVyvodNode oneRecordNode_Create(
    Vedomost_VB.OneRecordToPrint oneRecordToPrint,
    A_NastrTabl.OneVyvodNode oneVyvodNode_Parent,
    int razdelVed = 0)
  {
    if (oneRecordToPrint == null)
      return (A_NastrTabl.OneVyvodNode) null;
    string str1 = Vedomost_VB_Static.TypeRecName_by_TypeRec(oneRecordToPrint._nameTypeRec);
    string str2 = !(str1 != "Информационная") ? "" : str1;
    if (oneRecordToPrint._tableRowId != "")
    {
      if (str2 != "")
      {
        str2 = $"{str2}: {oneRecordToPrint._tableRowId}";
      }
      else
      {
        if (razdelVed > 0)
        {
          string nameRazdelVed = Vedomost_VB_Static.Get_NameRazdelVed(this._one_Tabl_Nastr_Tmp._list_RazdelsVed, razdelVed);
          if (nameRazdelVed != "")
            str2 = nameRazdelVed + ": ";
        }
        str2 += oneRecordToPrint._tableRowId;
      }
    }
    A_NastrTabl.OneVyvodNode oneVyvodNode = new A_NastrTabl.OneVyvodNode();
    oneVyvodNode.Text = str2;
    oneVyvodNode.ImageIndex = this._indexImageList_Section;
    oneVyvodNode.SelectedImageIndex = this._indexImageList_Section;
    A_NastrTabl.OneVyvodNode oneVyvodNode_Parent1 = oneVyvodNode;
    oneVyvodNode_Parent1._oneVyvodNode_Parent = oneVyvodNode_Parent;
    oneVyvodNode_Parent1._oneGrafaToPrint = (Vedomost_VB.OneGrafaToPrint) null;
    oneVyvodNode_Parent1._oneDataFieldToPrint = (Vedomost_VB.OneDataFieldToPrint) null;
    oneVyvodNode_Parent1._oneRecordToPrint = oneRecordToPrint;
    oneVyvodNode_Parent1._typeNode = Vedomost_VB_Static.TypeNode_Tree.Record;
    if (oneRecordToPrint._listOneGrafaToPrint != null)
    {
      for (int index = 0; index < oneRecordToPrint._listOneGrafaToPrint.Count; ++index)
      {
        A_NastrTabl.OneVyvodNode node = this.oneGrafaNode_Create(oneRecordToPrint._listOneGrafaToPrint[index], oneVyvodNode_Parent1);
        if (node != null)
          oneVyvodNode_Parent1.Nodes.Add((TreeNode) node);
      }
    }
    return oneVyvodNode_Parent1;
  }

  /// <summary> Ветка, описывающая одну ГРАФУ </summary>
  /// <param name="oneGrafaToPrint"></param>
  /// <param name="oneVyvodNode_Parent"></param>
  /// <returns></returns>
  private A_NastrTabl.OneVyvodNode oneGrafaNode_Create(
    Vedomost_VB.OneGrafaToPrint oneGrafaToPrint,
    A_NastrTabl.OneVyvodNode oneVyvodNode_Parent)
  {
    if (oneGrafaToPrint == null)
      return (A_NastrTabl.OneVyvodNode) null;
    string cellId = oneGrafaToPrint._cell_ID;
    A_NastrTabl.OneVyvodNode oneVyvodNode = new A_NastrTabl.OneVyvodNode();
    oneVyvodNode.Text = "Ячейка шаблона: " + cellId;
    oneVyvodNode.ImageIndex = this._indexImageList_Section;
    oneVyvodNode.SelectedImageIndex = this._indexImageList_Section;
    A_NastrTabl.OneVyvodNode oneVyvodNode_Parent1 = oneVyvodNode;
    oneVyvodNode_Parent1._oneVyvodNode_Parent = oneVyvodNode_Parent;
    oneVyvodNode_Parent1._oneGrafaToPrint = oneGrafaToPrint;
    oneVyvodNode_Parent1._oneDataFieldToPrint = (Vedomost_VB.OneDataFieldToPrint) null;
    oneVyvodNode_Parent1._oneRecordToPrint = (Vedomost_VB.OneRecordToPrint) null;
    oneVyvodNode_Parent1._typeNode = Vedomost_VB_Static.TypeNode_Tree.Cell;
    if (oneGrafaToPrint._listOneDataFieldToPrint != null)
    {
      for (int index = 0; index < oneGrafaToPrint._listOneDataFieldToPrint.Count; ++index)
      {
        A_NastrTabl.OneVyvodNode node = this.oneDataNode_Create(oneGrafaToPrint._listOneDataFieldToPrint[index], oneVyvodNode_Parent1, index);
        if (node != null)
        {
          node._oneVyvodNode_Parent = oneVyvodNode_Parent1;
          oneVyvodNode_Parent1.Nodes.Add((TreeNode) node);
        }
      }
    }
    return oneVyvodNode_Parent1;
  }

  /// <summary> Формирование одной конечной ветки ДАННЫх для дерева </summary>
  /// <param name="oneDataFieldToPrint"></param>
  /// <param name="oneVyvodNode_Parent"></param>
  /// <param name="iData"></param>
  /// <returns></returns>
  private A_NastrTabl.OneVyvodNode oneDataNode_Create(
    Vedomost_VB.OneDataFieldToPrint oneDataFieldToPrint,
    A_NastrTabl.OneVyvodNode oneVyvodNode_Parent,
    int iData)
  {
    if (oneDataFieldToPrint == null)
      return (A_NastrTabl.OneVyvodNode) null;
    string str = this.OneDataField_Draw(oneDataFieldToPrint, iData);
    A_NastrTabl.OneVyvodNode oneVyvodNode1 = new A_NastrTabl.OneVyvodNode();
    oneVyvodNode1.Text = str;
    oneVyvodNode1.ImageIndex = this._indexImageList_Section;
    oneVyvodNode1.SelectedImageIndex = this._indexImageList_Section;
    A_NastrTabl.OneVyvodNode oneVyvodNode2 = oneVyvodNode1;
    oneVyvodNode2._oneVyvodNode_Parent = iData <= 0 ? oneVyvodNode_Parent : oneVyvodNode_Parent._oneVyvodNode_Parent;
    oneVyvodNode2._oneGrafaToPrint = (Vedomost_VB.OneGrafaToPrint) null;
    oneVyvodNode2._oneDataFieldToPrint = oneDataFieldToPrint;
    oneVyvodNode2._oneRecordToPrint = (Vedomost_VB.OneRecordToPrint) null;
    oneVyvodNode2._typeNode = Vedomost_VB_Static.TypeNode_Tree.Data;
    oneVyvodNode2._iData = iData;
    return oneVyvodNode2;
  }

  /// <summary> Формирование одной конечной строчки ДАННЫх для дерева </summary>
  /// <param name="oneDataFieldToPrint"></param>
  /// <returns></returns>
  private string OneDataField_Draw(
    Vedomost_VB.OneDataFieldToPrint oneDataFieldToPrint,
    int iData)
  {
    if (oneDataFieldToPrint == null)
      return "";
    string str = "";
    this.comboBox_Vyvod_TextRazdelitel.Text = this.translate_text(oneDataFieldToPrint._symbolRazd, true);
    if (iData > 0)
      str = $"\"{this.translate_text(oneDataFieldToPrint._symbolRazd, false)}\" ";
    if (oneDataFieldToPrint._typeField == Vedomost_VB.TypeField.ObjectType)
    {
      string attributeTypeName = MetaDataHelper.GetAttributeTypeName(oneDataFieldToPrint._objectType);
      if (attributeTypeName != null)
        str += attributeTypeName;
    }
    return str;
  }

  /// <summary> Преобразование строк для Combobox и обратно </summary>
  /// <param name="s1"></param>
  /// <returns></returns>
  private string translate_text(string s1, bool tuda)
  {
    if (s1 == "")
      return "";
    string str;
    if (tuda)
    {
      switch (s1)
      {
        case "":
          str = "(без пробела)";
          break;
        case "\r\n":
          str = "(перенос)";
          break;
        case " ":
          str = "  (пробел)";
          break;
        case "*":
          str = "* (звездочка)";
          break;
        case ",":
          str = ", (запятая)";
          break;
        case "-":
          str = "- (минус)";
          break;
        case ".":
          str = ". (точка)";
          break;
        case " ":
          str = "(неразрывный пробел)";
          break;
        case "–":
          str = "(неразрывный дефис)";
          break;
        default:
          str = s1;
          break;
      }
    }
    else
    {
      switch (s1)
      {
        case "  (пробел)":
          str = " ";
          break;
        case "(без пробела)":
          str = "";
          break;
        case "(неразрывный дефис)":
          str = "–";
          break;
        case "(неразрывный пробел)":
          str = " ";
          break;
        case "(перенос)":
          str = "\r\n";
          break;
        case "* (звездочка)":
          str = "*";
          break;
        case ", (запятая)":
          str = ",";
          break;
        case "- (минус)":
          str = "-";
          break;
        case ". (точка)":
          str = ".";
          break;
        default:
          str = s1;
          break;
      }
    }
    return str;
  }

  /// <summary> При выделении ветки в дереве </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void treeView_Vyvod_AfterSelect(object sender, TreeViewEventArgs e)
  {
    string currId1 = "";
    this.i_curr_oneGrafaToPrint_Current = -1;
    this.oneDataFieldToPrint_current = (Vedomost_VB.OneDataFieldToPrint) null;
    this.oneGrafaToPrint_Current = (Vedomost_VB.OneGrafaToPrint) null;
    this.oneRecordToPrint_Current = (Vedomost_VB.OneRecordToPrint) null;
    this.listBox_Vyvod_List_Ved_Id.Enabled = true;
    this.comboBox_Vyvod_TextRazdelitel.Enabled = true;
    this.groupBox_Vyvod_TextRazdelitel.Enabled = true;
    this.listBox_Vyvod_List_Ved_Id.SelectedIndex = -1;
    this.comboBox_Vyvod_TextRazdelitel.Text = this.translate_text("", true);
    this.treeView_Vyvod.Enabled = true;
    this.oneTreeNode_Current = (A_NastrTabl.OneVyvodNode) this.treeView_Vyvod.SelectedNode;
    if (this.oneTreeNode_Current == null)
    {
      int num = (int) MessageBox.Show("В \"Дереве\" не выбран элемент", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    else if (this.oneTreeNode_Current._typeNode == Vedomost_VB_Static.TypeNode_Tree.Main)
    {
      this.oneDataFieldToPrint_current = (Vedomost_VB.OneDataFieldToPrint) null;
      this.oneGrafaToPrint_Current = (Vedomost_VB.OneGrafaToPrint) null;
      this.oneRecordToPrint_Current = (Vedomost_VB.OneRecordToPrint) null;
      this.listBox_Vyvod_List_Ved_Id.SelectedIndex = -1;
      this.listBox_Vyvod_List_Ved_Id.Enabled = false;
      this.comboBox_Vyvod_TextRazdelitel.Enabled = false;
      this.groupBox_Vyvod_TextRazdelitel.Enabled = false;
      string tableName = this.algorithmToPrint_curr == null ? "" : this.algorithmToPrint_curr._tableName;
      this.SetElementStr_Vyvod(tableName);
      if (tableName == "")
      {
        this.button_Vyvod_AddCell.Enabled = true;
        this.button_Vyvod_AddAttribut.Enabled = true;
      }
      else
      {
        this.button_Vyvod_AddCell.Enabled = false;
        this.button_Vyvod_AddAttribut.Enabled = false;
      }
      this.button_Vyvod_Edit.Enabled = false;
      this.button_Vyvod_Delete.Enabled = false;
    }
    else if (this.oneTreeNode_Current._typeNode == Vedomost_VB_Static.TypeNode_Tree.Info)
    {
      this.oneDataFieldToPrint_current = (Vedomost_VB.OneDataFieldToPrint) null;
      this.oneGrafaToPrint_Current = (Vedomost_VB.OneGrafaToPrint) null;
      this.oneRecordToPrint_Current = (Vedomost_VB.OneRecordToPrint) null;
      this.button_Vyvod_AddCell.Enabled = false;
      this.button_Vyvod_AddAttribut.Enabled = false;
      this.button_Vyvod_Edit.Enabled = false;
      this.button_Vyvod_Delete.Enabled = false;
      this.listBox_Vyvod_List_Ved_Id.SelectedIndex = -1;
      this.listBox_Vyvod_List_Ved_Id.Enabled = false;
      this.comboBox_Vyvod_TextRazdelitel.Enabled = false;
      this.groupBox_Vyvod_TextRazdelitel.Enabled = false;
      this.SetElementStr_Vyvod("");
    }
    else if (this.oneTreeNode_Current._typeNode == Vedomost_VB_Static.TypeNode_Tree.Record || this.oneTreeNode_Current._typeNode == Vedomost_VB_Static.TypeNode_Tree.RecordPasport)
    {
      this.oneDataFieldToPrint_current = (Vedomost_VB.OneDataFieldToPrint) null;
      this.oneGrafaToPrint_Current = (Vedomost_VB.OneGrafaToPrint) null;
      this.oneRecordToPrint_Current = this.oneTreeNode_Current._oneRecordToPrint;
      this.button_Vyvod_AddCell.Enabled = true;
      this.button_Vyvod_AddAttribut.Enabled = false;
      this.button_Vyvod_Edit.Enabled = true;
      this.button_Vyvod_Delete.Enabled = false;
      this.i_curr_oneGrafaToPrint_Current = -1;
      this.listBox_Vyvod_List_Ved_Id.SelectedIndex = -1;
      this.listBox_Vyvod_List_Ved_Id.Enabled = false;
      if (this.oneRecordToPrint_Current != null && this.oneRecordToPrint_Current._nameTypeRec == "oneRecordToPrintPasport" || this.oneTreeNode_Current.Text == "Основная надпись")
        this.button_Vyvod_Edit.Enabled = false;
      this.comboBox_Vyvod_TextRazdelitel.Enabled = false;
      this.groupBox_Vyvod_TextRazdelitel.Enabled = false;
      string currId2 = this.oneRecordToPrint_Current == null ? "" : this.oneRecordToPrint_Current._tableRowId;
      if (currId2 == "" && this.oneRecordToPrint_Current._nameTypeRec == "oneRecordToPrintPasport")
        currId2 = "Основная надпись";
      this.SetElementStr_Vyvod(currId2);
    }
    else if (this.oneTreeNode_Current._typeNode == Vedomost_VB_Static.TypeNode_Tree.Cell)
    {
      this.oneDataFieldToPrint_current = (Vedomost_VB.OneDataFieldToPrint) null;
      this.oneGrafaToPrint_Current = this.oneTreeNode_Current._oneGrafaToPrint;
      this.oneRecordToPrint_Current = this.oneTreeNode_Current._oneVyvodNode_Parent._oneRecordToPrint;
      if (this.oneRecordToPrint_Current != null && this.oneRecordToPrint_Current._listOneGrafaToPrint != null)
        this.i_curr_oneGrafaToPrint_Current = this.oneRecordToPrint_Current._listOneGrafaToPrint.IndexOf(this.oneGrafaToPrint_Current);
      this.button_Vyvod_AddCell.Enabled = true;
      this.button_Vyvod_AddAttribut.Enabled = true;
      this.button_Vyvod_Edit.Enabled = true;
      this.button_Vyvod_Delete.Enabled = true;
      this.listBox_Vyvod_List_Ved_Id.SelectedIndex = -1;
      this.listBox_Vyvod_List_Ved_Id.Enabled = true;
      this.comboBox_Vyvod_TextRazdelitel.Enabled = false;
      this.groupBox_Vyvod_TextRazdelitel.Enabled = false;
      this.SetElementStr_Vyvod(this.oneGrafaToPrint_Current == null ? "" : this.oneGrafaToPrint_Current._cell_ID);
    }
    else
    {
      if (this.oneTreeNode_Current._typeNode == Vedomost_VB_Static.TypeNode_Tree.Data)
      {
        this.oneDataFieldToPrint_current = this.oneTreeNode_Current._oneDataFieldToPrint;
        this.oneGrafaToPrint_Current = this.oneTreeNode_Current._oneVyvodNode_Parent._oneGrafaToPrint;
        this.oneRecordToPrint_Current = this.oneTreeNode_Current._oneVyvodNode_Parent._oneVyvodNode_Parent._oneRecordToPrint;
        this.button_Vyvod_AddCell.Enabled = false;
        this.button_Vyvod_AddAttribut.Enabled = true;
        this.button_Vyvod_Edit.Enabled = true;
        this.button_Vyvod_Delete.Enabled = true;
        this.listBox_Vyvod_List_Ved_Id.SelectedIndex = -1;
        this.listBox_Vyvod_List_Ved_Id.Enabled = true;
        this.comboBox_Vyvod_TextRazdelitel.Text = this.translate_text("", true);
        if (this.oneDataFieldToPrint_current._typeField == Vedomost_VB.TypeField.ObjectType)
          this.List_Ved_Id_SelectedByObjType(this.listBox_Vyvod_List_Ved_Id, this._one_Tabl_Nastr_Tmp._list_Ved_ID, this.oneDataFieldToPrint_current._objectType);
        currId1 = this.oneGrafaToPrint_Current == null ? "" : this.oneGrafaToPrint_Current._cell_ID;
        this.SetElementStr_Vyvod(currId1);
        this.comboBox_Vyvod_TextRazdelitel.Text = this.translate_text(this.oneDataFieldToPrint_current._symbolRazd, true);
      }
      this.SetElementStr_Vyvod(currId1);
    }
  }

  /// <summary> Кнопка ДОБАВИТЬ ЯЧЕЙКУ </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void button_Vyvod_AddCell_Click(object sender, EventArgs e)
  {
    DocumentTreeNode activeElement = this.docControl_Vyvod.ActiveElement;
    if (activeElement == null || activeElement.Id == "")
      return;
    if (this.oneTreeNode_Current._typeNode == Vedomost_VB_Static.TypeNode_Tree.Record)
    {
      if (activeElement.NodeClass != "TextBoxElement")
      {
        int num = (int) MessageBox.Show("На шаблоне необходимо выбрать текстовое поле", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      A_NastrTabl.OneVyvodNode oneVyvodNode = new A_NastrTabl.OneVyvodNode();
      oneVyvodNode.Text = "Ячейка шаблона: " + activeElement.Id;
      oneVyvodNode.ImageIndex = this._indexImageList_Section;
      oneVyvodNode.SelectedImageIndex = this._indexImageList_Section;
      A_NastrTabl.OneVyvodNode node = oneVyvodNode;
      node._typeNode = Vedomost_VB_Static.TypeNode_Tree.Cell;
      node._oneVyvodNode_Parent = this.oneTreeNode_Current;
      node._oneDataFieldToPrint = (Vedomost_VB.OneDataFieldToPrint) null;
      node._oneRecordToPrint = (Vedomost_VB.OneRecordToPrint) null;
      node._iData = -1;
      Vedomost_VB.OneGrafaToPrint oneGrafaToPrint = new Vedomost_VB.OneGrafaToPrint();
      node._oneGrafaToPrint = oneGrafaToPrint;
      oneGrafaToPrint._cell_ID = activeElement.Id;
      oneGrafaToPrint._listOneDataFieldToPrint = new List<Vedomost_VB.OneDataFieldToPrint>();
      this.oneTreeNode_Current.Nodes.Insert(0, (TreeNode) node);
      this.oneRecordToPrint_Current._listOneGrafaToPrint.Insert(0, oneGrafaToPrint);
      this.ModifiedAll(true);
      this.IsModified_Page_Vyvod = true;
    }
    if (this.oneTreeNode_Current._typeNode == Vedomost_VB_Static.TypeNode_Tree.Cell)
    {
      if (activeElement.NodeClass != "TextBoxElement")
      {
        int num = (int) MessageBox.Show("На шаблоне необходимо выбрать текстовое поле", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      A_NastrTabl.OneVyvodNode oneVyvodNode = new A_NastrTabl.OneVyvodNode();
      oneVyvodNode.Text = "Ячейка шаблона: " + activeElement.Id;
      oneVyvodNode.ImageIndex = this._indexImageList_Section;
      oneVyvodNode.SelectedImageIndex = this._indexImageList_Section;
      A_NastrTabl.OneVyvodNode node = oneVyvodNode;
      node._typeNode = Vedomost_VB_Static.TypeNode_Tree.Cell;
      node._oneVyvodNode_Parent = this.oneTreeNode_Current._oneVyvodNode_Parent;
      node._oneDataFieldToPrint = (Vedomost_VB.OneDataFieldToPrint) null;
      node._oneRecordToPrint = (Vedomost_VB.OneRecordToPrint) null;
      node._iData = -1;
      Vedomost_VB.OneGrafaToPrint oneGrafaToPrint = new Vedomost_VB.OneGrafaToPrint();
      node._oneGrafaToPrint = oneGrafaToPrint;
      oneGrafaToPrint._cell_ID = activeElement.Id;
      oneGrafaToPrint._listOneDataFieldToPrint = new List<Vedomost_VB.OneDataFieldToPrint>();
      this.oneTreeNode_Current._oneVyvodNode_Parent.Nodes.Insert(this.oneTreeNode_Current._oneVyvodNode_Parent.Nodes.IndexOf((TreeNode) this.oneTreeNode_Current) + 1, (TreeNode) node);
      this.oneRecordToPrint_Current._listOneGrafaToPrint.Insert(this.oneTreeNode_Current._oneVyvodNode_Parent.Nodes.IndexOf((TreeNode) this.oneTreeNode_Current) + 1, oneGrafaToPrint);
      this.ModifiedAll(true);
      this.IsModified_Page_Vyvod = true;
    }
    this.treeView_Vyvod.Select();
  }

  /// <summary> Кнопка ДОБАВИТЬ АТРИБУТ </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void button_Vyvod_AddAttribut_Click(object sender, EventArgs e)
  {
    if (this.oneTreeNode_Current._typeNode != Vedomost_VB_Static.TypeNode_Tree.Cell && this.oneTreeNode_Current._typeNode != Vedomost_VB_Static.TypeNode_Tree.Data)
      return;
    if (this.listBox_Vyvod_List_Ved_Id.SelectedIndex == -1)
    {
      int num1 = (int) MessageBox.Show("Атрибут не выбран", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    else
    {
      Vedomost_VB.OneDataFieldToPrint oneDataFieldToPrint = new Vedomost_VB.OneDataFieldToPrint();
      oneDataFieldToPrint._typeField = Vedomost_VB.TypeField.ObjectType;
      if (!(this.oneRecordToPrint_Current._nameTypeRec == "oneRecordToPrintPasport"))
      {
        oneDataFieldToPrint._objectType = this.Get_ObjType_By_index(this._one_Tabl_Nastr_Tmp._list_Ved_ID, this.listBox_Vyvod_List_Ved_Id.SelectedIndex);
        oneDataFieldToPrint._typeField = Vedomost_VB.TypeField.ObjectType;
        oneDataFieldToPrint._typeFieldVedRec = Vedomost_VB.TypeFieldVedRec.Undefined;
      }
      A_NastrTabl.OneVyvodNode node;
      if (this.oneTreeNode_Current._typeNode == Vedomost_VB_Static.TypeNode_Tree.Cell)
      {
        oneDataFieldToPrint._symbolRazd = "";
        node = this.oneDataNode_Create(oneDataFieldToPrint, this.oneTreeNode_Current, 0);
      }
      else
      {
        oneDataFieldToPrint._symbolRazd = this.translate_text(this.comboBox_Vyvod_TextRazdelitel.Text, false);
        node = this.oneDataNode_Create(oneDataFieldToPrint, this.oneTreeNode_Current, this.oneTreeNode_Current._iData + 1);
      }
      node._oneGrafaToPrint = (Vedomost_VB.OneGrafaToPrint) null;
      node._oneDataFieldToPrint = oneDataFieldToPrint;
      node._oneRecordToPrint = (Vedomost_VB.OneRecordToPrint) null;
      node._typeNode = Vedomost_VB_Static.TypeNode_Tree.Data;
      node._iData = this.oneTreeNode_Current._iData + 1;
      if (this.oneTreeNode_Current._typeNode == Vedomost_VB_Static.TypeNode_Tree.Cell)
      {
        this.oneGrafaToPrint_Current._listOneDataFieldToPrint.Insert(0, oneDataFieldToPrint);
        this.oneTreeNode_Current.Nodes.Insert(0, (TreeNode) node);
      }
      if (this.oneTreeNode_Current._typeNode == Vedomost_VB_Static.TypeNode_Tree.Data)
      {
        int num2 = this.oneGrafaToPrint_Current._listOneDataFieldToPrint.IndexOf(this.oneDataFieldToPrint_current);
        this.oneGrafaToPrint_Current._listOneDataFieldToPrint.Insert(num2 + 1, oneDataFieldToPrint);
        this.oneTreeNode_Current._oneVyvodNode_Parent.Nodes.Insert(num2 + 1, (TreeNode) node);
      }
      this.ModifiedAll(true);
      this.IsModified_Page_Vyvod = true;
      this.oneTreeNode_Current.Expand();
      this.treeView_Vyvod.Select();
    }
  }

  /// <summary> Добавление в список ВВОДИМЫХ </summary>
  /// <param name="specRowAttributeInfo"></param>
  /// <param name="isUzeEst"></param>
  /// <returns></returns>
  private bool Add_Id_To_list_Ved_ID(AvsRowAttributeInfo specRowAttributeInfo, out int isUzeEst)
  {
    if (specRowAttributeInfo == null)
    {
      isUzeEst = -1;
      return false;
    }
    for (int index = 0; index < this._one_Tabl_Nastr_Tmp._list_Ved_ID.Count; ++index)
    {
      Vedomost_VB.OneFieldSpForRead oneFieldSpForRead = this._one_Tabl_Nastr_Tmp._list_Ved_ID[index];
      if (specRowAttributeInfo.AttributeId == oneFieldSpForRead._id)
      {
        isUzeEst = index;
        return false;
      }
    }
    Vedomost_VB.OneFieldSpForRead oneFieldSpForRead1 = this.create_OneFieldSpForRead(specRowAttributeInfo, false);
    if (oneFieldSpForRead1 == null)
    {
      isUzeEst = -1;
      return false;
    }
    this._one_Tabl_Nastr_Tmp._list_Ved_ID.Add(oneFieldSpForRead1);
    this.listBox_Sbor_Peredatha_ListId.Items.Add((object) oneFieldSpForRead1._name);
    this.ModifiedAll(true);
    this.IsModified_Page_Sbor = true;
    isUzeEst = this.listBox_Sbor_Peredatha_ListId.Items.Count - 1;
    return true;
  }

  /// <summary> Кнопка ИЗМЕНИТЬ </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void button_Vyvod_Edit_Click(object sender, EventArgs e)
  {
    DocumentTreeNode activeElement = this.docControl_Vyvod.ActiveElement;
    string str = "";
    string id = activeElement.Id;
    string name1 = activeElement.Name;
    this.oneTreeNode_Current = (A_NastrTabl.OneVyvodNode) this.treeView_Vyvod.SelectedNode;
    if (this.oneTreeNode_Current == null)
    {
      int num1 = (int) MessageBox.Show("В \"Дереве\" не выбран элемент", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    else
    {
      if (this.oneTreeNode_Current._typeNode == Vedomost_VB_Static.TypeNode_Tree.Main)
      {
        if (activeElement.NodeClass != "TableElement")
        {
          int num2 = (int) MessageBox.Show("На шаблоне не выбрана таблица", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
          return;
        }
        string name2 = activeElement.Name;
        if (name2 != "")
        {
          this.algorithmToPrint_curr._tableName = name2;
          this.ModifiedAll(true);
          this.IsModified_Page_Vyvod = true;
        }
      }
      if (this.oneTreeNode_Current._typeNode == Vedomost_VB_Static.TypeNode_Tree.Record)
      {
        if (activeElement.NodeClass != "TableElement")
        {
          int num3 = (int) MessageBox.Show("На шаблоне не выбрана строка", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
          return;
        }
        if (activeElement.Id != "" && this.oneRecordToPrint_Current != null && activeElement.Id != "")
        {
          this.oneRecordToPrint_Current._tableRowId = activeElement.Id;
          this.oneTreeNode_Current.Text = $"{Vedomost_VB_Static.TypeRecName_by_TypeRec(this.oneRecordToPrint_Current._nameTypeRec)}: Строка: {this.oneRecordToPrint_Current._tableRowId}";
          this.ModifiedAll(true);
          this.IsModified_Page_Vyvod = true;
        }
      }
      if (this.oneTreeNode_Current._typeNode == Vedomost_VB_Static.TypeNode_Tree.Cell)
      {
        if (activeElement.NodeClass != "TextBoxElement" || !(activeElement.Id != ""))
        {
          int num4 = (int) MessageBox.Show("На шаблоне не выбрано текстовое поле", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
          return;
        }
        str = activeElement.Id;
        if (this.oneGrafaToPrint_Current != null)
        {
          this.oneGrafaToPrint_Current._cell_ID = activeElement.Id;
          this.oneTreeNode_Current.Text = "Ячейка шаблона: " + this.oneGrafaToPrint_Current._cell_ID;
          this.ModifiedAll(true);
          this.IsModified_Page_Vyvod = true;
        }
      }
      if (this.oneTreeNode_Current._typeNode == Vedomost_VB_Static.TypeNode_Tree.Data)
      {
        if (activeElement.NodeClass != "TextBoxElement" || !(activeElement.Id != ""))
        {
          int num5 = (int) MessageBox.Show("На шаблоне не выбрано текстовое поле", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
          return;
        }
        if (!(this.oneRecordToPrint_Current._nameTypeRec == "oneRecordToPrintPasport"))
        {
          if (this.listBox_Vyvod_List_Ved_Id.SelectedIndex > -1)
          {
            this.oneDataFieldToPrint_current._objectType = this.Get_ObjType_By_index(this._one_Tabl_Nastr_Tmp._list_Ved_ID, this.listBox_Vyvod_List_Ved_Id.SelectedIndex);
            this.oneDataFieldToPrint_current._typeField = Vedomost_VB.TypeField.ObjectType;
            this.oneDataFieldToPrint_current._typeFieldVedRec = Vedomost_VB.TypeFieldVedRec.Undefined;
            this.oneDataFieldToPrint_current._symbolRazd = this.translate_text(this.comboBox_Vyvod_TextRazdelitel.Text, false);
            this.oneTreeNode_Current.Text = this.OneDataField_Draw(this.oneDataFieldToPrint_current, this.oneTreeNode_Current._iData);
            this.ModifiedAll(true);
            this.IsModified_Page_Vyvod = true;
          }
          else
          {
            int num6 = (int) MessageBox.Show("Атрибут не выбран", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            return;
          }
        }
        if (this.oneTreeNode_Current._iData > 0)
        {
          this.oneDataFieldToPrint_current._symbolRazd = this.translate_text(this.comboBox_Vyvod_TextRazdelitel.Text, false);
          this.oneTreeNode_Current.Text = this.OneDataField_Draw(this.oneDataFieldToPrint_current, this.oneTreeNode_Current._iData);
          this.ModifiedAll(true);
          this.IsModified_Page_Vyvod = true;
        }
      }
      this.treeView_Vyvod.Select();
    }
  }

  /// <summary> Кнопка УДАЛИТЬ </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void button_Vyvod_Delete_Click(object sender, EventArgs e)
  {
    if (this.oneTreeNode_Current._typeNode != Vedomost_VB_Static.TypeNode_Tree.Cell && this.oneTreeNode_Current._typeNode != Vedomost_VB_Static.TypeNode_Tree.Data)
      return;
    A_NastrTabl.OneVyvodNode oneVyvodNode = (A_NastrTabl.OneVyvodNode) this.oneTreeNode_Current.PrevNode ?? this.oneTreeNode_Current._oneVyvodNode_Parent;
    if (this.oneTreeNode_Current._typeNode == Vedomost_VB_Static.TypeNode_Tree.Data && this.oneTreeNode_Current._iData > -1 && this.oneGrafaToPrint_Current._listOneDataFieldToPrint != null && this.oneTreeNode_Current._iData < this.oneGrafaToPrint_Current._listOneDataFieldToPrint.Count)
      this.oneGrafaToPrint_Current._listOneDataFieldToPrint.RemoveAt(this.oneTreeNode_Current._iData);
    else if (this.oneTreeNode_Current._typeNode == Vedomost_VB_Static.TypeNode_Tree.Cell && this.i_curr_oneGrafaToPrint_Current > -1 && this.oneRecordToPrint_Current._listOneGrafaToPrint != null && this.oneRecordToPrint_Current._listOneGrafaToPrint.Count > this.i_curr_oneGrafaToPrint_Current)
      this.oneRecordToPrint_Current._listOneGrafaToPrint.RemoveAt(this.i_curr_oneGrafaToPrint_Current);
    this.oneTreeNode_Current.Remove();
    if (oneVyvodNode != null)
      this.treeView_Vyvod.SelectedNode = (TreeNode) oneVyvodNode;
    this.treeView_Vyvod.Select();
    this.ModifiedAll(true);
    this.IsModified_Page_Vyvod = true;
  }

  /// <summary>Рисование подстраницы Вывод/Прочее</summary>
  public void Page_Vyvod_2_Draw()
  {
  }

  /// <summary> Default для ВЫВОДА </summary>
  private void Default_Vyvod()
  {
    if (this._one_Tabl_Nastr_Tmp._typeCreate != Vedomost_VB.TypeCreate.System)
      Tabl_Static.GuidTabl_By_TypeTabl(this._one_Tabl_Nastr_Tmp._typeVed);
    this.algorithmToPrint = Tabl_Static.AlgorithmToPrint_Init_By_TypeTabl(this._one_Tabl_Nastr_Tmp._typeVed);
    this._one_Tabl_Nastr_Tmp._algorithmToPrint = this.algorithmToPrint;
    this.algorithmToPrint_curr = this.algorithmToPrint;
    this.ModifiedAll(true);
    this.IsModified_Page_Vyvod = true;
    this.treeView_Vyvod_Draw();
    this.checkBox_Vyvod_Additional1.Checked = this.algorithmToPrint_curr._additional1 != 0;
    this.checkBox_Vyvod_Additional2.Checked = this.algorithmToPrint_curr._additional2 != 0;
    this.checkBox_Vyvod_Additional3.Checked = this.algorithmToPrint_curr._additional3 != 0;
    this.checkBox_Vyvod_Additional4.Checked = this.algorithmToPrint_curr._additional4 != 0;
    this.Page_Vyvod_2_Draw();
  }

  /// <summary> Сохранение редактирования страницы "Вывод" в _one_Tabl_Nastr_Tmp </summary>
  private void Saving_Page_Vyvod()
  {
    if (!this.IsModified_Page_Vyvod)
      return;
    this.IsBylo_IsModified_Page_Vyvod = true;
  }

  private long TemplateId_Xml
  {
    get => this.templID_Xml;
    set
    {
      this.templID_Xml = value;
      this.CloseAllDocs_Xml();
      if (this.templID_Xml == 0L || this.templID_Xml == -1L)
        return;
      this.imDocument_template_Xml = this.LoadTemplateFromObject(this.templID_Xml);
      this.OpenTemplate_Xml(this.imDocument_template_Xml);
    }
  }

  private void OpenTemplate_Xml(ImDocument doc)
  {
    this.CloseAllDocs_Xml();
    this.docControl_Xml = new DocumentControl();
    this.docControl_Xml.Document = doc;
    double num = (double) this.docControl_Xml.SetZoom(DocZoomMode.FitPage, 0.0f);
    this.docKcontrol_Xml = new DockControl((Control) this.docControl_Xml, this.docName);
    this.docKcontrol_Xml.Show(this.dockMan_Xml, DockState.Document);
    this.docKcontrol_Xml.Closable = false;
    this.ShowDocumentTreeView_Xml();
    this.docKcontrol_Xml.Select();
    this.docControl_Xml.ReadOnly = true;
  }

  protected DocumentTreeViewDlg DocumentTreeViewDlg_Xml
  {
    get
    {
      return this.documentTreeViewDlg_Xml != null && !this.documentTreeViewDlg_Xml.IsDisposed ? this.documentTreeViewDlg_Xml : (DocumentTreeViewDlg) null;
    }
  }

  public void ShowDocumentTreeView_Xml()
  {
    if (this.DocumentTreeViewDlg_Xml == null)
      this.documentTreeViewDlg_Xml = new DocumentTreeViewDlg();
    if (this.docControl_Xml != null)
    {
      DocumentTreeNode activeElement = this.docControl_Xml.ActiveElement;
      this.documentTreeViewDlg_Xml.TreeRoot = (DocumentTreeNode) this.docControl_Xml.Document;
      this.documentTreeViewDlg_Xml.DocumentControl = this.docControl_Xml;
      this.documentTreeViewDlg_Xml.UpdateSelection();
    }
    this.documentTreeViewDlg_Xml.Show(this.dockMan_Xml, DockState.DockRight);
    this.documentTreeViewDlg_Xml.Closable = false;
  }

  /// <summary> Очистка контейнера в окне Xml </summary>
  private void CloseAllDocs_Xml()
  {
    for (int index = 0; index < this.docContainer_Xml.Documents.Length; ++index)
      this.docContainer_Xml.Documents[index].Close();
  }

  /// <summary> В окне шаблона установить курсор на элемент </summary>
  /// <param name="currId"></param>
  private void SetElementStr_Xml(string currId)
  {
    if (currId != null && currId != "" && this.imDocument_template_Xml != null)
    {
      DocumentTreeNode selection = this.imDocument_template_Xml.FindNode(currId) ?? this.imDocument_template_Xml.FindFirstNodeByName(currId);
      if (selection != null)
      {
        this.docControl_Xml.SetSelection(selection, false, new Point(0, 0), true, false);
        this.docControl_Xml.ResetTernBufer();
        this.documentTreeViewDlg_Xml.UpdateSelection();
      }
      else
        this.docControl_Xml.SetSelection(selection, false, new Point(0, 0), true, false);
    }
    else
    {
      if (this.imDocument_template_Xml == null)
        return;
      this.docControl_Xml.SetSelection((DocumentTreeNode) null, false, new Point(0, 0), true, false);
    }
  }

  /// <summary> Установить на ячейку в определенной строке </summary>
  /// <param name="currId_Record"></param>
  /// <param name="currId_Field"></param>
  private void SetElementStr_Xml2(string currId_Record, string currId_Field)
  {
    if (string.IsNullOrEmpty(currId_Record) && string.IsNullOrEmpty(currId_Field))
      this.SetElementStr_Xml("");
    else if (string.IsNullOrEmpty(currId_Record))
      this.SetElementStr_Xml(currId_Field);
    else if (string.IsNullOrEmpty(currId_Field))
    {
      this.SetElementStr_Xml(currId_Record);
    }
    else
    {
      DocumentTreeNode documentTreeNode = (this.imDocument_template_Xml.FindNode(currId_Record) ?? this.imDocument_template_Xml.FindFirstNodeByName(currId_Record)) ?? (DocumentTreeNode) this.imDocument_template_Xml;
      DocumentTreeNode selection = documentTreeNode.FindNode(currId_Field) ?? documentTreeNode.FindFirstNodeByName(currId_Field);
      if (selection != null)
      {
        this.docControl_Xml.SetSelection(selection, false, new Point(0, 0), true, false);
        this.docControl_Xml.ResetTernBufer();
        this.documentTreeViewDlg_Xml.UpdateSelection();
      }
      else
        this.docControl_Xml.SetSelection((DocumentTreeNode) null, false, new Point(0, 0), true, false);
    }
  }

  /// <summary> В окне шаблона установить курсор на элемент </summary>
  /// <param name="currId"></param>
  private void SetElementInt_Xml(int currId) => this.SetElementStr_Xml(currId.ToString());

  private void Draw_Page_Xml()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = (IDBObject) null;
      if (this._one_Tabl_Nastr_Tmp._vedomostTemplateObjectGuid != Guid.Empty)
        dbObject = sessionKeeper.Session.GetObject(this._one_Tabl_Nastr_Tmp._vedomostTemplateObjectGuid, false);
      if (dbObject != null)
        this.templateID_Xml = dbObject.ObjectID;
      this.templateID_curr_Xml = this.templateID_Xml;
    }
    this.TemplateId_Xml = this.templateID_curr_Xml;
    this.treeView_Xml_Draw();
    if (this._one_Tabl_Nastr_Tmp._algorithmXml == null)
      return;
    this.numeric_UpDown_Xml_AfterInfo.Value = (Decimal) this._one_Tabl_Nastr_Tmp._algorithmXml._afterInfo;
    this.numeric_UpDown_Xml_AfterRemark.Value = (Decimal) this._one_Tabl_Nastr_Tmp._algorithmXml._afterRemark;
    switch (this._one_Tabl_Nastr_Tmp._algorithmXml._passportIn)
    {
      case 0:
        this.radioButton_Xml_PassportInNo.Checked = true;
        break;
      case 1:
        this.radioButton_Xml_PassportInDialog.Checked = true;
        break;
      case 2:
        this.radioButton_Xml_PassporInAlways.Checked = true;
        break;
    }
    switch (this._one_Tabl_Nastr_Tmp._algorithmXml._passportOut)
    {
      case 0:
        this.radioButton_Xml_PassportOutNo.Checked = true;
        break;
      case 1:
        this.radioButton_Xml_PassportOutDialog.Checked = true;
        break;
      case 2:
        this.radioButton_Xml_PassporOutAlways.Checked = true;
        break;
    }
    this.textBox_Xml_Folder_In.Text = this._one_Tabl_Nastr_Tmp._algorithmXml._folderXmlIn;
  }

  /// <summary> Рисование ДЕРЕВА выводимых полей </summary>
  private void treeView_Xml_Draw()
  {
    this.treeView_Xml.Nodes.Clear();
    Vedomost_VB_Static.OneXmlNode oneXmlNode1 = new Vedomost_VB_Static.OneXmlNode();
    oneXmlNode1.Text = "Правила связи с XML";
    oneXmlNode1.ImageIndex = this._indexImageList_Section;
    oneXmlNode1.SelectedImageIndex = this._indexImageList_Section;
    Vedomost_VB_Static.OneXmlNode oneXmlNode2 = oneXmlNode1;
    oneXmlNode2._oneXmlNode_Parent = (Vedomost_VB_Static.OneXmlNode) null;
    oneXmlNode2._oneFieldXml = (Vedomost_VB.OneFieldXml) null;
    oneXmlNode2._oneRecordXml = (Vedomost_VB.OneRecordXml) null;
    oneXmlNode2._typeNode = Vedomost_VB_Static.TypeNode_Tree.Main;
    this.treeView_Xml.Nodes.Add((TreeNode) oneXmlNode2);
    if (this._one_Tabl_Nastr_Tmp._algorithmXml == null)
      return;
    oneXmlNode2._oneXmlNode_Parent = (Vedomost_VB_Static.OneXmlNode) null;
    oneXmlNode2._oneFieldXml = (Vedomost_VB.OneFieldXml) null;
    oneXmlNode2._oneRecordXml = (Vedomost_VB.OneRecordXml) null;
    oneXmlNode2._typeNode = Vedomost_VB_Static.TypeNode_Tree.Info;
    Vedomost_VB_Static.oneRecordNode_Xml_CreateAll(this._one_Tabl_Nastr_Tmp._algorithmXml._oneRecordXmlPasport, "Основная надпись", oneXmlNode2);
    Vedomost_VB_Static.oneRecordNode_Xml_CreateAll(this._one_Tabl_Nastr_Tmp._algorithmXml._oneRecordXml_Info, "Информационная", oneXmlNode2);
    Vedomost_VB_Static.oneRecordNode_Xml_CreateAll(this._one_Tabl_Nastr_Tmp._algorithmXml._oneRecordXmlTitleIncluded, "Заголовок \"Ведомости составных частей\"", oneXmlNode2);
    Vedomost_VB_Static.oneRecordNode_Xml_CreateAll(this._one_Tabl_Nastr_Tmp._algorithmXml._oneRecordXmlIncluded, "Ведомость составных частей", oneXmlNode2);
    Vedomost_VB_Static.oneRecordNode_Xml_CreateAll(this._one_Tabl_Nastr_Tmp._algorithmXml._oneRecordXmlTitleVar, "Переменные данные для исполнений", oneXmlNode2);
    Vedomost_VB_Static.oneRecordNode_Xml_CreateAll(this._one_Tabl_Nastr_Tmp._algorithmXml._oneRecordXmlTitleIsp, "Заголовок исполнения", oneXmlNode2);
    Vedomost_VB_Static.oneRecordNode_Xml_CreateAll(this._one_Tabl_Nastr_Tmp._algorithmXml._oneRecordXmlTitle, "Заголовок", oneXmlNode2);
    Vedomost_VB_Static.oneRecordNode_Xml_CreateAll(this._one_Tabl_Nastr_Tmp._algorithmXml._oneRecordXmlTitlePodSection, "Заголовок подраздела", oneXmlNode2);
    Vedomost_VB_Static.oneRecordNode_Xml_CreateAll(this._one_Tabl_Nastr_Tmp._algorithmXml._oneRecordXmlRemark, "Примечание", oneXmlNode2);
    Vedomost_VB_Static.oneRecordNode_Xml_CreateAll(this._one_Tabl_Nastr_Tmp._algorithmXml._oneRecordXmlRemarkShort, "Примечание короткое", oneXmlNode2);
    Vedomost_VB_Static.oneRecordNode_Xml_CreateAll(this._one_Tabl_Nastr_Tmp._algorithmXml._oneRecordXmlTitlePart, "Заголовок части", oneXmlNode2);
    Vedomost_VB_Static.oneRecordNode_Xml_CreateAll(this._one_Tabl_Nastr_Tmp._algorithmXml._oneRecordXmlAdditional1, "Дополнительная1", oneXmlNode2);
    Vedomost_VB_Static.oneRecordNode_Xml_CreateAll(this._one_Tabl_Nastr_Tmp._algorithmXml._oneRecordXmlAdditional2, "Дополнительная2", oneXmlNode2);
    Vedomost_VB_Static.oneRecordNode_Xml_CreateAll(this._one_Tabl_Nastr_Tmp._algorithmXml._oneRecordXmlAdditional3, "Дополнительная3", oneXmlNode2);
    Vedomost_VB_Static.oneRecordNode_Xml_CreateAll(this._one_Tabl_Nastr_Tmp._algorithmXml._oneRecordXmlAdditional4, "Дополнительная4", oneXmlNode2);
    Vedomost_VB_Static.oneRecordNode_Xml_CreateAll(this._one_Tabl_Nastr_Tmp._algorithmXml._oneRecordXmlEmpty, "Пустая", oneXmlNode2);
    oneXmlNode2.Expand();
    this.treeView_Xml.SelectedNode = this.treeView_Xml.Nodes[0].Nodes[1];
  }

  /// <summary> Что то указали на дереве </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void treeView_Xml_AfterSelect(object sender, TreeViewEventArgs e)
  {
    string str1 = "";
    string currId_Field = "";
    string str2 = "";
    if (this.treeView_Xml.SelectedNode == this.treeView_Xml.Nodes[0])
      this.treeView_Xml.SelectedNode = this.treeView_Xml.Nodes[0].Nodes[0];
    this.oneXmlNode_Curr = (Vedomost_VB_Static.OneXmlNode) this.treeView_Xml.SelectedNode;
    if (this.oneXmlNode_Curr == null)
    {
      int num = (int) MessageBox.Show("В \"Дереве\" не выбран элемент", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    else
    {
      if (this.oneXmlNode_Curr._typeNode == Vedomost_VB_Static.TypeNode_Tree.Undefined)
      {
        this.treeView_Xml.SelectedNode = this.treeView_Xml.SelectedNode.Parent;
        this.oneXmlNode_Curr = (Vedomost_VB_Static.OneXmlNode) this.treeView_Xml.SelectedNode;
      }
      if ((this.oneXmlNode_Curr._typeNode == Vedomost_VB_Static.TypeNode_Tree.Record || this.oneXmlNode_Curr._typeNode == Vedomost_VB_Static.TypeNode_Tree.RecordPasport) && this.oneXmlNode_Curr._oneRecordXml != null)
      {
        str1 = this.oneXmlNode_Curr._oneRecordXml._tableRowId;
        this.SetElementStr_Xml(str1);
        this.textBox_Xml_Text.Enabled = true;
        this.button_Xml_Edit.Enabled = true;
        this.button_Xml_Add.Enabled = true;
        this.button_Xml_Delete.Enabled = false;
      }
      if (this.oneXmlNode_Curr._typeNode == Vedomost_VB_Static.TypeNode_Tree.Cell)
      {
        if (this.oneXmlNode_Curr._oneRecordXml != null)
          str1 = this.oneXmlNode_Curr._oneRecordXml._tableRowId;
        if (this.oneXmlNode_Curr._oneFieldXml != null)
          currId_Field = this.oneXmlNode_Curr._oneFieldXml._nameToFile;
        this.SetElementStr_Xml2(str1, currId_Field);
        str2 = this.oneXmlNode_Curr._oneFieldXml._nameToXml;
        this.textBox_Xml_Text.Enabled = true;
        this.button_Xml_Edit.Enabled = true;
        this.button_Xml_Add.Enabled = true;
        this.button_Xml_Delete.Enabled = true;
      }
      this.textBox_Xml_Text.Text = str2;
      this.text_Old = str2;
    }
  }

  private void treeView_Xml_KeyDown(object sender, KeyEventArgs e)
  {
    e.KeyCode.Equals((object) Keys.Up);
    e.KeyCode.Equals((object) Keys.Down);
    if (!e.KeyCode.Equals((object) Keys.Delete) || this.oneXmlNode_Curr._typeNode != Vedomost_VB_Static.TypeNode_Tree.Cell)
      return;
    this.button_Xml_Delete_Click(sender, (EventArgs) e);
  }

  /// <summary> Кнопка ИЗМЕНИТЬ </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void button_Xml_Edit_Click(object sender, EventArgs e)
  {
    bool flag = false;
    if (this.oneXmlNode_Curr._typeNode == Vedomost_VB_Static.TypeNode_Tree.Cell && !Vedomost_VB_Static.name_Field_Xml_Check(this.textBox_Xml_Text.Text, this.textBox_Xml_Text))
      return;
    DocumentTreeNode activeElement = this.docControl_Xml.ActiveElement;
    if (activeElement == null || string.IsNullOrEmpty(activeElement.Id))
    {
      int num1 = (int) MessageBox.Show("Не выбран элемент на шаблоне", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    else
    {
      Vedomost_VB_Static.TypeNodeElement_Shablona nodeElementShablona = Vedomost_VB_Static.Check_tableElement(activeElement);
      int num2 = (int) Vedomost_VB_Static.Check_tableElement_Parent(activeElement);
      if (this.oneXmlNode_Curr._typeNode == Vedomost_VB_Static.TypeNode_Tree.Cell && nodeElementShablona != Vedomost_VB_Static.TypeNodeElement_Shablona.Cell)
      {
        int num3 = (int) MessageBox.Show("На шаблоне необходимо выбрать ячейку", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      else if ((this.oneXmlNode_Curr._typeNode == Vedomost_VB_Static.TypeNode_Tree.Record || this.oneXmlNode_Curr._typeNode == Vedomost_VB_Static.TypeNode_Tree.RecordPasport) && nodeElementShablona != Vedomost_VB_Static.TypeNodeElement_Shablona.RecordMain && nodeElementShablona != Vedomost_VB_Static.TypeNodeElement_Shablona.RecordVtor && nodeElementShablona != Vedomost_VB_Static.TypeNodeElement_Shablona.Pasport)
      {
        int num4 = (int) MessageBox.Show("На шаблоне необходимо выбрать запись", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      else
      {
        if (nodeElementShablona == Vedomost_VB_Static.TypeNodeElement_Shablona.Cell)
        {
          int num5 = (int) Vedomost_VB_Static.Check_tableElement_Parent(activeElement);
        }
        Vedomost_VB.OneFieldXml oneFieldXml = (Vedomost_VB.OneFieldXml) null;
        if (this.oneXmlNode_Curr._typeNode == Vedomost_VB_Static.TypeNode_Tree.Cell)
          oneFieldXml = this.oneXmlNode_Curr._oneFieldXml;
        Vedomost_VB.OneRecordXml oneRecordXml = this.oneXmlNode_Curr._oneRecordXml;
        if (!Vedomost_VB_Static.control_Douplication(oneRecordXml, oneFieldXml, this.textBox_Xml_Text.Text, activeElement.Name, true))
          return;
        if (this.oneXmlNode_Curr._typeNode == Vedomost_VB_Static.TypeNode_Tree.Cell)
        {
          if (!Vedomost_VB_Static.name_Field_Xml_Check(this.textBox_Xml_Text.Text, this.textBox_Xml_Text))
            return;
          string nameToFile = oneFieldXml._nameToFile;
          if (this.text_Old != this.textBox_Xml_Text.Text)
          {
            oneFieldXml._nameToXml = this.textBox_Xml_Text.Text;
            this.oneXmlNode_Curr.Text = this.oneXmlNode_Curr.Text.Replace(this.text_Old, this.textBox_Xml_Text.Text);
            this.text_Old = this.textBox_Xml_Text.Text;
            flag = true;
          }
          if (nameToFile != activeElement.Id)
          {
            this.oneXmlNode_Curr._oneFieldXml._nameToFile = activeElement.Id;
            this.oneXmlNode_Curr.Text = Vedomost_VB_Static.Text_oneFieldXml(this.oneXmlNode_Curr._oneFieldXml._nameToFile, this.oneXmlNode_Curr._oneFieldXml._nameToXml, this.oneXmlNode_Curr._lBases);
            flag = true;
          }
        }
        if (this.oneXmlNode_Curr._typeNode == Vedomost_VB_Static.TypeNode_Tree.Record)
        {
          string tableRowId = oneRecordXml._tableRowId;
          if (tableRowId != activeElement.Id)
          {
            this.oneXmlNode_Curr._oneRecordXml._tableRowId = activeElement.Id;
            this.oneXmlNode_Curr.Text = this.oneXmlNode_Curr.Text.Replace(tableRowId, activeElement.Id);
            flag = true;
          }
        }
        if (!flag)
          return;
        this.ModifiedAll(true);
        this.IsModified_Page_Xml = true;
      }
    }
  }

  /// <summary> Нажатие ENTER в textBox_Xml_Text </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void textBox_Xml_Text_KeyDown(object sender, KeyEventArgs e)
  {
    if (!e.KeyCode.Equals((object) Keys.Return) || !Vedomost_VB_Static.name_Field_Xml_Check(this.textBox_Xml_Text.Text, this.textBox_Xml_Text))
      return;
    if (this.oneXmlNode_Curr._typeNode == Vedomost_VB_Static.TypeNode_Tree.Record)
    {
      this.button_Xml_Add_Click(sender, (EventArgs) e);
    }
    else
    {
      Vedomost_VB_Static.OneXmlNode selectedNode = (Vedomost_VB_Static.OneXmlNode) this.treeView_Xml.SelectedNode;
      Vedomost_VB.OneFieldXml oneFieldXml = selectedNode._oneFieldXml;
      if (!Vedomost_VB_Static.control_Douplication(selectedNode._oneRecordXml, oneFieldXml, this.textBox_Xml_Text.Text, (string) null, false))
        return;
      selectedNode._oneFieldXml._nameToXml = this.textBox_Xml_Text.Text;
      selectedNode.Text = selectedNode.Text.Replace(this.text_Old, this.textBox_Xml_Text.Text);
      this.text_Old = this.textBox_Xml_Text.Text;
      this.ModifiedAll(true);
      this.IsModified_Page_Xml = true;
    }
  }

  /// <summary> Проверка текста имени Xml "На лету" </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void textBox_Xml_Text_TextChanged(object sender, EventArgs e)
  {
    Vedomost_VB_Static.name_Field_Xml_Check(this.textBox_Xml_Text.Text, this.textBox_Xml_Text);
  }

  /// <summary>  Кнопка ДОБАВИТЬ </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void button_Xml_Add_Click(object sender, EventArgs e)
  {
    if (!Vedomost_VB_Static.name_Field_Xml_Check(this.textBox_Xml_Text.Text, this.textBox_Xml_Text))
      return;
    DocumentTreeNode activeElement = this.docControl_Xml.ActiveElement;
    if (activeElement == null || string.IsNullOrEmpty(activeElement.Id))
    {
      int num1 = (int) MessageBox.Show("Не выбран элемент на шаблоне", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    else if (Vedomost_VB_Static.Check_tableElement(activeElement) != Vedomost_VB_Static.TypeNodeElement_Shablona.Cell)
    {
      int num2 = (int) MessageBox.Show("На шаблоне необходимо выбрать ячейку", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    else
    {
      Vedomost_VB.OneFieldXml oneFieldXml = new Vedomost_VB.OneFieldXml();
      oneFieldXml._nameToXml = this.textBox_Xml_Text.Text;
      oneFieldXml._nameToFile = activeElement.Name;
      oneFieldXml._typeDataToXml = Vedomost_VB.TypeDataToXml.Field;
      Vedomost_VB_Static.OneXmlNode node = Vedomost_VB_Static.oneFieldNode_Xml_Create(this.oneXmlNode_Curr._oneRecordXml, oneFieldXml, this.oneXmlNode_Curr, this.oneXmlNode_Curr._lBases, this.oneXmlNode_Curr._oneRecordXml._listOneFieldXml.Count - 1);
      if (node == null)
        return;
      this.oneXmlNode_Curr._oneRecordXml._listOneFieldXml.Insert(this.oneXmlNode_Curr._iData + 1, oneFieldXml);
      this.oneXmlNode_Curr.Parent.Nodes.Insert(this.oneXmlNode_Curr.Index + 1, (TreeNode) node);
      this.treeView_Xml.SelectedNode = (TreeNode) node;
      this.ModifiedAll(true);
      this.IsModified_Page_Xml = true;
    }
  }

  /// <summary> Кнопка УДАЛИТЬ </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void button_Xml_Delete_Click(object sender, EventArgs e)
  {
    this.oneXmlNode_Curr._oneRecordXml._listOneFieldXml.Remove(this.oneXmlNode_Curr._oneFieldXml);
    this.oneXmlNode_Curr.Parent.Nodes.Remove((TreeNode) this.oneXmlNode_Curr);
    this.ModifiedAll(true);
    this.IsModified_Page_Xml = true;
  }

  /// <summary>Пропускать строк после информационной</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void numeric_UpDown_Xml_AfterInfo_ValueChanged(object sender, EventArgs e)
  {
    this._one_Tabl_Nastr_Tmp._algorithmXml._afterInfo = (int) this.numeric_UpDown_Xml_AfterInfo.Value;
    this.ModifiedAll(true);
    this.IsModified_Page_Xml = true;
  }

  /// <summary> Пропускать строк после примечания </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void numeric_UpDown_Xml_AfterRemark_ValueChanged(object sender, EventArgs e)
  {
    this._one_Tabl_Nastr_Tmp._algorithmXml._afterRemark = (int) this.numeric_UpDown_Xml_AfterRemark.Value;
    this.ModifiedAll(true);
    this.IsModified_Page_Xml = true;
  }

  /// <summary> Изменение ввода основной надписи </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void radioButton_Xml_PassporIn(object sender, MouseEventArgs e)
  {
    if (this.radioButton_Xml_PassporInAlways.Checked)
      this._one_Tabl_Nastr_Tmp._algorithmXml._passportIn = 2;
    else if (this.radioButton_Xml_PassportInDialog.Checked)
      this._one_Tabl_Nastr_Tmp._algorithmXml._passportIn = 1;
    else if (this.radioButton_Xml_PassportInNo.Checked)
      this._one_Tabl_Nastr_Tmp._algorithmXml._passportIn = 0;
    this.ModifiedAll(true);
    this.IsModified_Page_Xml = true;
  }

  /// <summary> Изменение вывода основной надписи </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void radioButton_Xml_PassporOut(object sender, MouseEventArgs e)
  {
    if (this.radioButton_Xml_PassporOutAlways.Checked)
      this._one_Tabl_Nastr_Tmp._algorithmXml._passportOut = 2;
    else if (this.radioButton_Xml_PassportOutDialog.Checked)
      this._one_Tabl_Nastr_Tmp._algorithmXml._passportOut = 1;
    else if (this.radioButton_Xml_PassportOutNo.Checked)
      this._one_Tabl_Nastr_Tmp._algorithmXml._passportOut = 0;
    this.ModifiedAll(true);
    this.IsModified_Page_Xml = true;
  }

  /// <summary> Окончание редактирования строки </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void textBox_Xml_Folder_In_Leave(object sender, EventArgs e)
  {
    if (!Vedomost_VB_Static.check_Text_For_Filename(this.textBox_Xml_Folder_In.Text, this.textBox_Xml_Folder_In) || !Vedomost_VB_Static.check_Text_For_FilenameExists(this.textBox_Xml_Folder_In.Text, this.textBox_Xml_Folder_In))
      return;
    this._one_Tabl_Nastr_Tmp._algorithmXml._folderXmlIn = this.textBox_Xml_Folder_In.Text.Trim();
    this.ModifiedAll(true);
    this.IsModified_Page_Xml = true;
  }

  /// <summary> Нажатие кнопки выбора папки </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void button_Xml_Folder_In_Click(object sender, EventArgs e)
  {
    string str = "";
    try
    {
      using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
      {
        folderBrowserDialog.SelectedPath = this._one_Tabl_Nastr_Tmp._algorithmXml._folderXmlIn;
        if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
        {
          str = folderBrowserDialog.SelectedPath;
          if (!string.IsNullOrEmpty(str))
            this.textBox_Xml_Folder_In.Text = str;
        }
      }
    }
    catch (Exception ex)
    {
      int num = (int) MessageBox.Show("Ошибка\r\n\r\n" + str, "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    this._one_Tabl_Nastr_Tmp._algorithmXml._folderXmlIn = this.textBox_Xml_Folder_In.Text.Trim();
    this.ModifiedAll(true);
    this.IsModified_Page_Xml = true;
  }

  /// <summary> Рисование страницы СЕРВИС </summary>
  private void Draw_Page_Service()
  {
    if (this._one_Tabl_Nastr_Tmp._typeCreate == Vedomost_VB.TypeCreate.System)
    {
      this.buttonServicesTypeVedTo.Visible = false;
      this.label_ServicesTypeVedTo.Visible = false;
      this.labelService1.Text = "Таблица системная";
      this.labelService2.Text = "";
    }
    else
    {
      this.buttonServicesTypeVedTo.Visible = true;
      this.label_ServicesTypeVedTo.Visible = true;
      this.labelService1.Text = "Таблица пользовательская";
      this.labelService2.Text = "";
      string str = Vedomost_VB_Static.TypeVed_string(this._one_Tabl_Nastr_Tmp._typeVed);
      if (!string.IsNullOrEmpty(str))
        this.labelService2.Text = "Аналог: " + str;
    }
    if (Vedomost_VB_Static.isCreateDump_Tmp)
    {
      this.checkBox_Services_CreateDump.Checked = true;
      this.checkBox_Services_CreateDump.Enabled = true;
    }
    if (Vedomost_VB_Static.isCreateDump_System)
    {
      Vedomost_VB_Static.isCreateDump_Tmp = true;
      this.checkBox_Services_CreateDump.Checked = true;
      this.checkBox_Services_CreateDump.Enabled = false;
      this.checkBox_Services_CreateDump.Text = "Создавать Dump в текущем сеансе работы (включено постоянно в системной переменной)";
    }
    this.checkBox_Services_CreateDump.Visible = false;
    if (this._one_Tabl_Nastr_Tmp._accessLevel == 0)
      this.radioButton_AccessLevel0.Checked = true;
    else if (this._one_Tabl_Nastr_Tmp._accessLevel == 1)
      this.radioButton_AccessLevel1.Checked = true;
    else
      this.radioButton_AccessLevel2.Checked = true;
  }

  /// <summary> Кнопка По умолчанию все </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void buttonServicesDefaultAll_Click(object sender, EventArgs e)
  {
    One_Ved_Nastr one_Ved_Nastr = Tabl_Static.Tabl_Nastr_Init(this._one_Tabl_Nastr_Tmp._typeVed, Guid.Empty, false);
    if (one_Ved_Nastr == null)
      return;
    if (this.isByloButtonTypeVedTo_Click || this._one_Tabl_Nastr_Tmp._typeCreate == Vedomost_VB.TypeCreate.User)
    {
      one_Ved_Nastr._guidParent = this._one_Tabl_Nastr_Tmp._guidParent;
      one_Ved_Nastr._guidTypeVed = this._one_Tabl_Nastr_Tmp._guidTypeVed;
      one_Ved_Nastr._idTypeVed = this._one_Tabl_Nastr_Tmp._idTypeVed;
      one_Ved_Nastr._imsObjectType = this._one_Tabl_Nastr_Tmp._imsObjectType;
      one_Ved_Nastr._typeCreate = this._one_Tabl_Nastr_Tmp._typeCreate;
      one_Ved_Nastr._vedomostTemplateObjectGuid = this._one_Tabl_Nastr_Tmp._vedomostTemplateObjectGuid;
      one_Ved_Nastr._vedomostTemplateObjectGuid_B = this._one_Tabl_Nastr_Tmp._vedomostTemplateObjectGuid_B;
      this.isByloButtonTypeVedTo_Click = false;
    }
    else
    {
      one_Ved_Nastr._typeCreate = this._one_Tabl_Nastr_Tmp._typeCreate;
      one_Ved_Nastr._vedomostTemplateObjectGuid = this._one_Tabl_Nastr_Tmp._vedomostTemplateObjectGuid;
      one_Ved_Nastr._vedomostTemplateObjectGuid_B = this._one_Tabl_Nastr_Tmp._vedomostTemplateObjectGuid_B;
    }
    this.isCreate = true;
    this._one_Tabl_Nastr_Tmp = Vedomost_VB_Static.One_Ved_Nastr_Copy(one_Ved_Nastr);
    this._one_Tabl_Nastr_Tmp._accessLevel = this._one_Tabl_Nastr_Curr._accessLevel;
    this.Draw_All();
    this.isCreate = false;
    this.ModifiedAll(true);
    this.tabControl_Nastr.SelectedTab = this.tabPage_Vyvod;
    this.isCreate = false;
  }

  /// <summary> Кнопка Копировать все из ... </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void buttonServicesCopyAll_Click(object sender, EventArgs e)
  {
    VyborVedomosti vyborVedomosti = new VyborVedomosti();
    vyborVedomosti._typeDoc = Vedomost_VB.TypeDoc.Tabl;
    using (vyborVedomosti)
    {
      vyborVedomosti._imsObjectTypeDel = this._imsObjectType_Curr;
      vyborVedomosti._list_ImsObjectType_With_One_Ved_Nastrs = Vedomost_VB_Static._list_Tabl_Arbeit_ImsObjectType_With_One_Ved_Nastr;
      if (vyborVedomosti.ShowDialog() != DialogResult.OK)
        return;
      bool flag = true;
      if (this._one_Tabl_Nastr_Tmp._typeVed != vyborVedomosti._one_Ved_Nastr_Result._typeVed && MessageBox.Show("Типы таблиц не совпадают\r\n\r\nКопировать настройки?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
        flag = false;
      if (!flag)
        return;
      this._one_Tabl_Nastr_Tmp = Vedomost_VB_Static.One_Ved_Nastr_Copy_NotFull(vyborVedomosti._one_Ved_Nastr_Result, this._one_Tabl_Nastr_Tmp);
      this.isCreate = true;
      this.Draw_All();
      this.ModifiedAll(true);
      this.tabControl_Nastr.SelectedTab = this.tabPage_Vyvod;
      this.isCreate = false;
    }
  }

  /// <summary> Пользовательской таблицы присвоить какой либо системный тип </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void buttonServicesTypeVedTo_Click(object sender, EventArgs e)
  {
    using (VyborVedomosti_withIni vedomostiWithIni = new VyborVedomosti_withIni())
    {
      vedomostiWithIni.List_Type_Systems = Vedomost_VB_Static.List_TypeTabl_Systems;
      if (vedomostiWithIni.ShowDialog() != DialogResult.OK)
        return;
      this._one_Tabl_Nastr_Tmp._typeVed = vedomostiWithIni.typeVed_result;
      this.Draw_Page_Service();
      this.isByloButtonTypeVedTo_Click = true;
      this.IsButtonDefault();
      this.buttonDefault.Visible = false;
      this.isCreate = false;
      this.ModifiedAll(true);
    }
  }

  private void buttonServiceCreateDump_Click(object sender, EventArgs e)
  {
    try
    {
      Vedomost_VB_Static.Checking_FOLDER_ForDump();
      if (string.IsNullOrEmpty(Vedomost_VB_Static.DirectoryDump) || !Directory.Exists(Vedomost_VB_Static.DirectoryDump))
        return;
      Vedomost_VB_Static.CleaningDirectoryDumpVed();
      Vedomost_VB_Static.OneVedNastrToDump(this._one_Tabl_Nastr_Tmp);
      Vedomost_VB_Static.ShablonToDump(this._one_Tabl_Nastr_Tmp);
      if (Vedomost_VB_Static.imDocument != null)
      {
        string textIn1 = Vedomost_VB_Static.DirectoryDump + "\\Tabl.pdf";
        string textIn2 = Vedomost_VB_Static.DirectoryDump + "\\Tabl.imdx";
        string fileName1 = Vedomost_VB_Static.Replace_Invalid_Char(textIn1, true);
        string fileName2 = Vedomost_VB_Static.Replace_Invalid_Char(textIn2, true);
        Vedomost_VB_Static.imDocument.SaveToPdf(fileName1);
        Vedomost_VB_Static.imDocument.SaveToXml(fileName2, false);
      }
      List<string> stringList = Vedomost_VB_Static.GelVersion_AVS();
      StreamWriter streamWriter = new StreamWriter(Vedomost_VB_Static.DirectoryDump + "\\VERSION.TXT");
      for (int index = 0; index < stringList.Count; ++index)
      {
        string str = stringList[index];
        streamWriter.WriteLine(str);
      }
      streamWriter.Close();
      if (MessageBox.Show($"Параметры настройки и шаблон сохранены в файлы.\r\n\r\nПапка\r\n{Vedomost_VB_Static.DirectoryDump}\r\n\r\nОткрыть эту папку?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
        return;
      Process.Start(Vedomost_VB_Static.DirectoryDump);
    }
    catch
    {
      int num = (int) MessageBox.Show("Создать файлы для Dump не удалось.\r\n\r\nПапка\r\n" + Vedomost_VB_Static.DirectoryDump, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
  }

  private void checkBox_Services_CreateDump_CheckedChanged(object sender, EventArgs e)
  {
    if (this.checkBox_Services_CreateDump.Checked)
      Vedomost_VB_Static.isCreateDump_Tmp = true;
    else
      Vedomost_VB_Static.isCreateDump_Tmp = false;
  }

  private void Saving_Page_Service()
  {
    if (this.radioButton_AccessLevel0.Checked)
      this._one_Tabl_Nastr_Tmp._accessLevel = 0;
    else if (this.radioButton_AccessLevel1.Checked)
      this._one_Tabl_Nastr_Tmp._accessLevel = 1;
    else if (this.radioButton_AccessLevel2.Checked)
      this._one_Tabl_Nastr_Tmp._accessLevel = 2;
    if (!this.IsModified_Page_Vyvod)
      return;
    this.IsBylo_IsModified_Page_Vyvod = true;
  }

  private void radioButton_AccessLevel0_MouseClick(object sender, MouseEventArgs e)
  {
    this.ModifiedAll(true);
    this._one_Tabl_Nastr_Curr._accessLevel = 0;
    this.IsModified_Page_Service = true;
  }

  private void radioButton_AccessLevel1_MouseClick(object sender, MouseEventArgs e)
  {
    this.ModifiedAll(true);
    this._one_Tabl_Nastr_Curr._accessLevel = 1;
    this.IsModified_Page_Service = true;
  }

  private void radioButton_AccessLevel2_MouseClick(object sender, MouseEventArgs e)
  {
    this.ModifiedAll(true);
    this._one_Tabl_Nastr_Curr._accessLevel = 2;
    this.IsModified_Page_Service = true;
  }

  /// <summary> Кнопка "Читать из файла" </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void buttonServicesFileOpen_Click(object sender, EventArgs e)
  {
    OpenFileDialog openFileDialog = new OpenFileDialog();
    openFileDialog.RestoreDirectory = true;
    openFileDialog.Filter = "Ini файлы (*.xml)|*.xml";
    openFileDialog.DefaultExt = "xml";
    if (openFileDialog.ShowDialog() != DialogResult.OK)
      return;
    string fileName1 = openFileDialog.FileName;
    string fileName2 = Vedomost_VB_Static.FileName_Template_For_FileName_Nastr(fileName1);
    this.imDocument_template_Vyvod_FromDump = (ImDocument) null;
    if (!string.IsNullOrEmpty(fileName2))
      this.imDocument_template_Vyvod_FromDump = ImDocument.LoadFromFile(fileName2, out DocumentFileType _, false) as ImDocument;
    XmlDocument xmlDocument = new XmlDocument();
    xmlDocument.Load(fileName1);
    this._one_Tabl_Nastr_Tmp = new One_Ved_Nastr();
    this._one_Tabl_Nastr_Tmp.Filled_One_Ved_Nastr_FromXml(xmlDocument);
    this._imsObjectType_Curr = MetaDataHelper.GetObjectType(this._one_Tabl_Nastr_Tmp._guidTypeVed);
    this._one_Tabl_Nastr_Tmp._imsObjectType = this._imsObjectType_Curr;
    this._guidTypeTabl_Curr = this._one_Tabl_Nastr_Tmp._guidTypeVed;
    this._guidTemplateTabl_Curr = this._one_Tabl_Nastr_Tmp._vedomostTemplateObjectGuid;
    this.Text = "Настройка ведомости:";
    this.Text = $"{this.Text} [{this._imsObjectType_Curr.ObjectName}]";
    if (this._one_Tabl_Nastr_Curr._dateIni != "")
      this.Text = $"{this.Text} {this._one_Tabl_Nastr_Curr._dateIni}";
    this.Text = $"{this.Text}: Настройки изменены данными из файла (Dump): {fileName1}";
    this.ModifiedAll(true);
    this.isCreate = true;
    this.Draw_All();
    this.tabControl_Nastr.SelectedTab = this.tabPage_Bases;
    this.isCreate = false;
    this.IsModifiedFromFile = true;
  }

  private long TemplateId_Avs
  {
    get => this.templID_Avs;
    set
    {
      this.templID_Avs = value;
      this.CloseAllDocs_Avs();
      if (this.templID_Avs == 0L || this.templID_Avs == -1L)
        return;
      this.imDocument_template_Avs = this.LoadTemplateFromObject(this.templID_Avs);
      this.OpenTemplate_Avs(this.imDocument_template_Avs);
    }
  }

  private void OpenTemplate_Avs(ImDocument doc)
  {
    this.CloseAllDocs_Avs();
    this.docControl_Avs = new DocumentControl();
    this.docControl_Avs.Document = doc;
    double num = (double) this.docControl_Avs.SetZoom(DocZoomMode.FitPage, 0.0f);
    this.docKcontrol_Avs = new DockControl((Control) this.docControl_Avs, this.docName);
    this.docKcontrol_Avs.Show(this.dockMan_Avs, DockState.Document);
    this.docKcontrol_Avs.Closable = false;
    this.ShowDocumentTreeView_Avs();
    this.docKcontrol_Avs.Select();
    this.docControl_Avs.ReadOnly = true;
  }

  protected DocumentTreeViewDlg DocumentTreeViewDlg_Avs
  {
    get
    {
      return this.documentTreeViewDlg_Avs != null && !this.documentTreeViewDlg_Avs.IsDisposed ? this.documentTreeViewDlg_Avs : (DocumentTreeViewDlg) null;
    }
  }

  public void ShowDocumentTreeView_Avs()
  {
    if (this.DocumentTreeViewDlg_Avs == null)
      this.documentTreeViewDlg_Avs = new DocumentTreeViewDlg();
    if (this.docControl_Avs != null)
    {
      DocumentTreeNode activeElement = this.docControl_Avs.ActiveElement;
      this.documentTreeViewDlg_Avs.TreeRoot = (DocumentTreeNode) this.docControl_Avs.Document;
      this.documentTreeViewDlg_Avs.DocumentControl = this.docControl_Avs;
      this.documentTreeViewDlg_Avs.UpdateSelection();
    }
    this.documentTreeViewDlg_Avs.Show(this.dockMan_Avs, DockState.DockRight);
    this.documentTreeViewDlg_Avs.Closable = false;
  }

  /// <summary> Очистка контейнера в окне Avs </summary>
  private void CloseAllDocs_Avs()
  {
    for (int index = 0; index < this.docContainer_Avs.Documents.Length; ++index)
      this.docContainer_Avs.Documents[index].Close();
  }

  /// <summary> В окне шаблона установить курсор на элемент </summary>
  /// <param name="currId"></param>
  private void SetElementStr_Avs(string currId)
  {
    if (currId != null && currId != "" && this.imDocument_template_Avs != null)
    {
      DocumentTreeNode selection = this.imDocument_template_Avs.FindNode(currId) ?? this.imDocument_template_Avs.FindFirstNodeByName(currId);
      if (selection != null)
      {
        this.docControl_Avs.SetSelection(selection, false, new Point(0, 0), true, false);
        this.docControl_Avs.ResetTernBufer();
        this.documentTreeViewDlg_Avs.UpdateSelection();
      }
      else
        this.docControl_Avs.SetSelection(selection, false, new Point(0, 0), true, false);
    }
    else
    {
      if (this.imDocument_template_Avs == null)
        return;
      this.docControl_Avs.SetSelection((DocumentTreeNode) null, false, new Point(0, 0), true, false);
    }
  }

  /// <summary> Установить на ячейку в определенной строке </summary>
  /// <param name="currId_Record"></param>
  /// <param name="currId_Field"></param>
  private void SetElementStr_Avs2(string currId_Record, string currId_Field)
  {
    if (string.IsNullOrEmpty(currId_Record) && string.IsNullOrEmpty(currId_Field))
      this.SetElementStr_Avs("");
    else if (string.IsNullOrEmpty(currId_Record))
      this.SetElementStr_Avs(currId_Field);
    else if (string.IsNullOrEmpty(currId_Field))
    {
      this.SetElementStr_Avs(currId_Record);
    }
    else
    {
      DocumentTreeNode documentTreeNode = (this.imDocument_template_Avs.FindNode(currId_Record) ?? this.imDocument_template_Avs.FindFirstNodeByName(currId_Record)) ?? (DocumentTreeNode) this.imDocument_template_Avs;
      DocumentTreeNode selection = documentTreeNode.FindFirstChildNodeByName(currId_Field) ?? documentTreeNode.FindFirstNodeByName(currId_Field);
      if (selection != null)
      {
        this.docControl_Avs.SetSelection(selection, false, new Point(0, 0), true, false);
        this.docControl_Avs.ResetTernBufer();
        this.documentTreeViewDlg_Avs.UpdateSelection();
      }
      else
        this.docControl_Avs.SetSelection((DocumentTreeNode) null, false, new Point(0, 0), true, false);
    }
  }

  /// <summary> В окне шаблона установить курсор на элемент </summary>
  /// <param name="currId"></param>
  private void SetElementInt_Avs(int currId) => this.SetElementStr_Avs(currId.ToString());

  private void Draw_listBox_Avs6_Fields()
  {
    this.listBox_Avs6_Fields.Items.Clear();
    if (AVS6_From_Avs6Main._list_recordFields == null || AVS6_From_Avs6Main._list_recordFields.Count == 0)
      return;
    for (int index = 0; index < AVS6_From_Avs6Main._list_recordFields.Count; ++index)
      this.listBox_Avs6_Fields.Items.Add((object) AVS6_From_Avs6Main._list_recordFields[index]._fieldName_Avs6);
  }

  private void Draw_Page_Avs()
  {
    this.algorithm_Avs = this._one_Tabl_Nastr_Tmp._algorithm_Avs6_To_Ips;
    this.algorithm_Avs_curr = this.algorithm_Avs;
    this.Draw_listBox_Avs6_Fields();
    if (this.listBox_Avs6_Fields.Items.Count > 0)
      this.listBox_Avs6_Fields.SelectedIndex = 0;
    else
      this.listBox_Avs6_Fields.SelectedIndex = -1;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = (IDBObject) null;
      if (this._one_Tabl_Nastr_Tmp._vedomostTemplateObjectGuid != Guid.Empty)
        dbObject = sessionKeeper.Session.GetObject(this._one_Tabl_Nastr_Tmp._vedomostTemplateObjectGuid, false);
      if (dbObject != null)
        this.templateID_Avs = dbObject.ObjectID;
      this.templateID_curr_Avs = this.templateID_Avs;
    }
    this.TemplateId_Avs = this.templateID_curr_Avs;
    this.treeView_Avs_Draw();
  }

  /// <summary> Рисование ДЕРЕВА выводимых полей </summary>
  private void treeView_Avs_Draw()
  {
    bool flag1 = false;
    bool flag2 = false;
    this.treeView_Avs.Nodes.Clear();
    if (this.imDocument_template_Avs != null)
    {
      flag1 = this.imDocument_template_Avs.FindNode("Подтаблица Куда входит") != null;
      flag2 = this.imDocument_template_Avs.FindNode("Строка Кол итого") != null;
    }
    A_NastrTabl.OneAvsNode oneAvsNode1 = new A_NastrTabl.OneAvsNode();
    oneAvsNode1.Text = "Правила вывода на шаблон: Главная таблица";
    oneAvsNode1.ImageIndex = this._indexImageList_Section;
    oneAvsNode1.SelectedImageIndex = this._indexImageList_Section;
    A_NastrTabl.OneAvsNode oneAvsNode2 = oneAvsNode1;
    oneAvsNode2._oneAvsNode_Parent = (A_NastrTabl.OneAvsNode) null;
    oneAvsNode2._oneGrafa_Avs = (Vedomost_VB.OneGrafa_Avs6_To_Ips) null;
    oneAvsNode2._oneDataField_Avs = (Vedomost_VB.OneDataField_Avs6_To_Ips) null;
    oneAvsNode2._oneRecord_Avs = (Vedomost_VB.OneRecord_Avs6_To_Ips) null;
    oneAvsNode2._typeNode = Vedomost_VB_Static.TypeNode_Tree.Main;
    if (this.imDocument_template_Avs != null)
      this.treeView_Avs.Nodes.Add((TreeNode) oneAvsNode2);
    A_NastrTabl.OneAvsNode oneAvsNode3 = new A_NastrTabl.OneAvsNode();
    oneAvsNode3.Text = "Информационные записи: ";
    oneAvsNode3.ImageIndex = this._indexImageList_Section;
    oneAvsNode3.SelectedImageIndex = this._indexImageList_Section;
    A_NastrTabl.OneAvsNode oneAvsNode4 = oneAvsNode3;
    oneAvsNode4._oneAvsNode_Parent = (A_NastrTabl.OneAvsNode) null;
    oneAvsNode4._oneGrafa_Avs = (Vedomost_VB.OneGrafa_Avs6_To_Ips) null;
    oneAvsNode4._oneDataField_Avs = (Vedomost_VB.OneDataField_Avs6_To_Ips) null;
    oneAvsNode4._oneRecord_Avs = (Vedomost_VB.OneRecord_Avs6_To_Ips) null;
    oneAvsNode4._typeNode = Vedomost_VB_Static.TypeNode_Tree.Info;
    oneAvsNode2.Nodes.Add((TreeNode) oneAvsNode4);
    if (this.algorithm_Avs_curr == null)
      return;
    if (this.algorithm_Avs_curr._list_OneRazdel_Avs6_To_Ips != null && this.algorithm_Avs_curr._list_OneRazdel_Avs6_To_Ips.Count > 0)
    {
      for (int index = 0; index < this.algorithm_Avs_curr._list_OneRazdel_Avs6_To_Ips.Count; ++index)
      {
        Vedomost_VB.OneRazdel_Avs6_To_Ips oneRazdelAvs6ToIp = this.algorithm_Avs_curr._list_OneRazdel_Avs6_To_Ips[index];
        if (oneRazdelAvs6ToIp._oneRecord_Avs6_To_Ips_Info != null)
        {
          A_NastrTabl.OneAvsNode oneAvsNode5 = this.oneRecordNode_Avs_Create(oneRazdelAvs6ToIp._oneRecord_Avs6_To_Ips_Info, oneAvsNode4, oneRazdelAvs6ToIp._razdelVed);
          if (oneAvsNode5 != null)
          {
            oneAvsNode4.Nodes.Add((TreeNode) oneAvsNode5);
            if (flag1 && oneRazdelAvs6ToIp._oneRecord_Avs6_To_Ips_Info._oneRecord_Avs6_To_Ips_Vtor != null)
            {
              A_NastrTabl.OneAvsNode node = this.oneRecordNode_Avs_Create(oneRazdelAvs6ToIp._oneRecord_Avs6_To_Ips_Info._oneRecord_Avs6_To_Ips_Vtor, oneAvsNode5);
              if (node != null)
                oneAvsNode5.Nodes.Add((TreeNode) node);
            }
            if (flag2 && oneRazdelAvs6ToIp._oneRecord_Avs6_To_Ips_Info._oneRecord_Avs6_To_Ips_Itogo != null)
            {
              A_NastrTabl.OneAvsNode node = this.oneRecordNode_Avs_Create(oneRazdelAvs6ToIp._oneRecord_Avs6_To_Ips_Info._oneRecord_Avs6_To_Ips_Itogo, oneAvsNode5);
              if (node != null)
                oneAvsNode5.Nodes.Add((TreeNode) node);
            }
          }
        }
      }
    }
    else if (this.algorithm_Avs_curr._oneRecord_Avs6_To_Ips_Info != null)
    {
      A_NastrTabl.OneAvsNode oneAvsNode6 = this.oneRecordNode_Avs_Create(this.algorithm_Avs_curr._oneRecord_Avs6_To_Ips_Info, oneAvsNode4);
      if (oneAvsNode6 != null)
      {
        oneAvsNode4.Nodes.Add((TreeNode) oneAvsNode6);
        if (flag1 && this.algorithm_Avs_curr._oneRecord_Avs6_To_Ips_Info._oneRecord_Avs6_To_Ips_Vtor != null)
        {
          A_NastrTabl.OneAvsNode node = this.oneRecordNode_Avs_Create(this.algorithm_Avs_curr._oneRecord_Avs6_To_Ips_Info._oneRecord_Avs6_To_Ips_Vtor, oneAvsNode6);
          if (node != null)
            oneAvsNode6.Nodes.Add((TreeNode) node);
        }
        if (flag2 && this.algorithm_Avs_curr._oneRecord_Avs6_To_Ips_Info._oneRecord_Avs6_To_Ips_Itogo != null)
        {
          A_NastrTabl.OneAvsNode node = this.oneRecordNode_Avs_Create(this.algorithm_Avs_curr._oneRecord_Avs6_To_Ips_Info._oneRecord_Avs6_To_Ips_Itogo, oneAvsNode6);
          if (node != null)
            oneAvsNode6.Nodes.Add((TreeNode) node);
        }
        oneAvsNode6.Expand();
      }
    }
    oneAvsNode4.Expand();
    A_NastrTabl.OneAvsNode node1 = this.oneRecordNode_Avs_Create(this.algorithm_Avs_curr._oneRecord_Avs6_To_Ips_Title, oneAvsNode2);
    if (node1 != null)
      oneAvsNode2.Nodes.Add((TreeNode) node1);
    A_NastrTabl.OneAvsNode node2 = this.oneRecordNode_Avs_Create(this.algorithm_Avs_curr._oneRecord_Avs6_To_Ips_TitleVar, oneAvsNode2);
    if (node2 != null)
      oneAvsNode2.Nodes.Add((TreeNode) node2);
    A_NastrTabl.OneAvsNode node3 = this.oneRecordNode_Avs_Create(this.algorithm_Avs_curr._oneRecord_Avs6_To_Ips_TitleIsp, oneAvsNode2);
    if (node3 != null)
      oneAvsNode2.Nodes.Add((TreeNode) node3);
    A_NastrTabl.OneAvsNode node4 = this.oneRecordNode_Avs_Create(this.algorithm_Avs_curr._oneRecord_Avs6_To_Ips_Remark, oneAvsNode2);
    if (node4 != null)
      oneAvsNode2.Nodes.Add((TreeNode) node4);
    A_NastrTabl.OneAvsNode node5 = this.oneRecordNode_Avs_Create(this.algorithm_Avs_curr._oneRecord_Avs6_To_Ips_RemarkShort, oneAvsNode2);
    if (node5 != null)
      oneAvsNode2.Nodes.Add((TreeNode) node5);
    A_NastrTabl.OneAvsNode node6 = this.oneRecordNode_Avs_Create(this.algorithm_Avs_curr._oneRecord_Avs6_To_Ips_Additional1, oneAvsNode2);
    if (node6 != null)
      oneAvsNode2.Nodes.Add((TreeNode) node6);
    A_NastrTabl.OneAvsNode node7 = this.oneRecordNode_Avs_Create(this.algorithm_Avs_curr._oneRecord_Avs6_To_Ips_Additional2, oneAvsNode2);
    if (node7 != null)
      oneAvsNode2.Nodes.Add((TreeNode) node7);
    A_NastrTabl.OneAvsNode node8 = this.oneRecordNode_Avs_Create(this.algorithm_Avs_curr._oneRecord_Avs6_To_Ips_Additional3, oneAvsNode2);
    if (node8 != null)
      oneAvsNode2.Nodes.Add((TreeNode) node8);
    A_NastrTabl.OneAvsNode node9 = this.oneRecordNode_Avs_Create(this.algorithm_Avs_curr._oneRecord_Avs6_To_Ips_Additional4, oneAvsNode2);
    if (node9 != null)
      oneAvsNode2.Nodes.Add((TreeNode) node9);
    oneAvsNode2.Expand();
  }

  /// <summary> Ветка, описывающая одну ЗАПИСЬ </summary>
  /// <param name="oneRecord_Avs"></param>
  /// <param name="oneAvsNode_Parent"></param>
  /// <returns></returns>
  private A_NastrTabl.OneAvsNode oneRecordNode_Avs_Create(
    Vedomost_VB.OneRecord_Avs6_To_Ips oneRecord_Avs,
    A_NastrTabl.OneAvsNode oneAvsNode_Parent,
    int razdelVed = 0)
  {
    if (oneRecord_Avs == null)
      return (A_NastrTabl.OneAvsNode) null;
    string str1 = Vedomost_VB_Static.TypeRecName_by_TypeRec_Avs(oneRecord_Avs._nameTypeRec);
    string str2 = !(str1 != "Информационная") ? "" : str1;
    if (oneRecord_Avs._tableRowId != "")
    {
      if (str2 != "")
      {
        str2 = $"{str2}: {oneRecord_Avs._tableRowId}";
      }
      else
      {
        if (razdelVed > 0)
        {
          string nameRazdelVed = Vedomost_VB_Static.Get_NameRazdelVed(this._one_Tabl_Nastr_Tmp._list_RazdelsVed, razdelVed);
          if (nameRazdelVed != "")
            str2 = nameRazdelVed + ": ";
        }
        str2 += oneRecord_Avs._tableRowId;
      }
    }
    A_NastrTabl.OneAvsNode oneAvsNode = new A_NastrTabl.OneAvsNode();
    oneAvsNode.Text = str2;
    oneAvsNode.ImageIndex = this._indexImageList_Section;
    oneAvsNode.SelectedImageIndex = this._indexImageList_Section;
    A_NastrTabl.OneAvsNode oneAvsNode_Parent1 = oneAvsNode;
    oneAvsNode_Parent1._oneAvsNode_Parent = oneAvsNode_Parent;
    oneAvsNode_Parent1._oneGrafa_Avs = (Vedomost_VB.OneGrafa_Avs6_To_Ips) null;
    oneAvsNode_Parent1._oneDataField_Avs = (Vedomost_VB.OneDataField_Avs6_To_Ips) null;
    oneAvsNode_Parent1._oneRecord_Avs = oneRecord_Avs;
    oneAvsNode_Parent1._typeNode = Vedomost_VB_Static.TypeNode_Tree.Record;
    if (oneRecord_Avs._listOneGrafa_Avs6_To_Ips != null)
    {
      for (int index = 0; index < oneRecord_Avs._listOneGrafa_Avs6_To_Ips.Count; ++index)
      {
        A_NastrTabl.OneAvsNode node = this.oneGrafaNode_Avs_Create(oneRecord_Avs._listOneGrafa_Avs6_To_Ips[index], oneAvsNode_Parent1);
        if (node != null)
          oneAvsNode_Parent1.Nodes.Add((TreeNode) node);
      }
    }
    return oneAvsNode_Parent1;
  }

  /// <summary> Ветка, описывающая одну ГРАФУ </summary>
  /// <param name="oneGrafa_Avs"></param>
  /// <param name="oneAvsNode_Parent"></param>
  /// <returns></returns>
  private A_NastrTabl.OneAvsNode oneGrafaNode_Avs_Create(
    Vedomost_VB.OneGrafa_Avs6_To_Ips oneGrafa_Avs,
    A_NastrTabl.OneAvsNode oneAvsNode_Parent)
  {
    if (oneGrafa_Avs == null)
      return (A_NastrTabl.OneAvsNode) null;
    string cellId = oneGrafa_Avs._cell_ID;
    A_NastrTabl.OneAvsNode oneAvsNode = new A_NastrTabl.OneAvsNode();
    oneAvsNode.Text = "Ячейка шаблона: " + cellId;
    oneAvsNode.ImageIndex = this._indexImageList_Section;
    oneAvsNode.SelectedImageIndex = this._indexImageList_Section;
    A_NastrTabl.OneAvsNode oneAvsNode_Parent1 = oneAvsNode;
    oneAvsNode_Parent1._oneAvsNode_Parent = oneAvsNode_Parent;
    oneAvsNode_Parent1._oneGrafa_Avs = oneGrafa_Avs;
    oneAvsNode_Parent1._oneDataField_Avs = (Vedomost_VB.OneDataField_Avs6_To_Ips) null;
    oneAvsNode_Parent1._oneRecord_Avs = (Vedomost_VB.OneRecord_Avs6_To_Ips) null;
    oneAvsNode_Parent1._typeNode = Vedomost_VB_Static.TypeNode_Tree.Cell;
    if (oneGrafa_Avs._listOneDataField_Avs6_To_Ips != null)
    {
      for (int index = 0; index < oneGrafa_Avs._listOneDataField_Avs6_To_Ips.Count; ++index)
      {
        A_NastrTabl.OneAvsNode node = this.oneDataNode_Avs_Create(oneGrafa_Avs._listOneDataField_Avs6_To_Ips[index], oneAvsNode_Parent1, index);
        if (node != null)
        {
          node._oneAvsNode_Parent = oneAvsNode_Parent1;
          oneAvsNode_Parent1.Nodes.Add((TreeNode) node);
        }
      }
    }
    return oneAvsNode_Parent1;
  }

  /// <summary> Формирование одной конечной ветки ДАННЫх для дерева </summary>
  /// <param name="oneDataField_Avs"></param>
  /// <param name="oneAvsNode_Parent"></param>
  /// <param name="iData"></param>
  /// <returns></returns>
  private A_NastrTabl.OneAvsNode oneDataNode_Avs_Create(
    Vedomost_VB.OneDataField_Avs6_To_Ips oneDataField_Avs,
    A_NastrTabl.OneAvsNode oneAvsNode_Parent,
    int iData)
  {
    if (oneDataField_Avs == null)
      return (A_NastrTabl.OneAvsNode) null;
    string str = this.OneDataField_Avs_Draw(oneDataField_Avs, iData);
    A_NastrTabl.OneAvsNode oneAvsNode1 = new A_NastrTabl.OneAvsNode();
    oneAvsNode1.Text = str;
    oneAvsNode1.ImageIndex = this._indexImageList_Section;
    oneAvsNode1.SelectedImageIndex = this._indexImageList_Section;
    A_NastrTabl.OneAvsNode oneAvsNode2 = oneAvsNode1;
    oneAvsNode2._oneAvsNode_Parent = iData <= 0 ? oneAvsNode_Parent : oneAvsNode_Parent._oneAvsNode_Parent;
    oneAvsNode2._oneGrafa_Avs = (Vedomost_VB.OneGrafa_Avs6_To_Ips) null;
    oneAvsNode2._oneDataField_Avs = oneDataField_Avs;
    oneAvsNode2._oneRecord_Avs = (Vedomost_VB.OneRecord_Avs6_To_Ips) null;
    oneAvsNode2._typeNode = Vedomost_VB_Static.TypeNode_Tree.Data;
    oneAvsNode2._iData = iData;
    return oneAvsNode2;
  }

  /// <summary> Формирование одной конечной строчки ДАННЫх для дерева </summary>
  /// <param name="oneDataField_Avs"></param>
  /// <returns></returns>
  private string OneDataField_Avs_Draw(
    Vedomost_VB.OneDataField_Avs6_To_Ips oneDataField_Avs,
    int iData)
  {
    if (oneDataField_Avs == null)
      return "";
    string str1 = "";
    this.comboBox_Avs_TextRazdelitel.Text = this.translate_text(oneDataField_Avs._symbolRazd, true);
    if (iData > 0)
      str1 = $"\"{this.translate_text(oneDataField_Avs._symbolRazd, false)}\" ";
    string str2 = AVS6_From_Avs6Main.FieldNameByType(AVS6_From_Avs6Main.TypeListFields.Record, (byte) oneDataField_Avs._objectType);
    if (str2 != null)
      str1 += str2;
    return str1;
  }

  /// <summary> Выбор строки на ДЕРЕВЕ </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void treeView_Avs_AfterSelect(object sender, TreeViewEventArgs e)
  {
    string currId = "";
    this.i_curr_oneGrafa_Avs_Current = -1;
    this.oneDataField_Avs_current = (Vedomost_VB.OneDataField_Avs6_To_Ips) null;
    this.oneGrafa_Avs_Current = (Vedomost_VB.OneGrafa_Avs6_To_Ips) null;
    this.oneRecord_Avs_Current = (Vedomost_VB.OneRecord_Avs6_To_Ips) null;
    this.comboBox_Avs_TextRazdelitel.Enabled = true;
    this.groupBox_Avs_TextRazdelitel.Enabled = true;
    this.comboBox_Avs_TextRazdelitel.Text = this.translate_text("", true);
    this.treeView_Avs.Enabled = true;
    this.oneTreeNode_Avs_Current = (A_NastrTabl.OneAvsNode) this.treeView_Avs.SelectedNode;
    if (this.oneTreeNode_Avs_Current == null)
    {
      int num = (int) MessageBox.Show("В \"Дереве\" не выбран элемент", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    else if (this.oneTreeNode_Avs_Current._typeNode == Vedomost_VB_Static.TypeNode_Tree.Main)
    {
      this.listBox_Avs6_Fields.SelectedIndex = -1;
      this.oneDataField_Avs_current = (Vedomost_VB.OneDataField_Avs6_To_Ips) null;
      this.oneGrafa_Avs_Current = (Vedomost_VB.OneGrafa_Avs6_To_Ips) null;
      this.oneRecord_Avs_Current = (Vedomost_VB.OneRecord_Avs6_To_Ips) null;
      this.comboBox_Avs_TextRazdelitel.Enabled = false;
      this.groupBox_Avs_TextRazdelitel.Enabled = false;
      string tableName = this.algorithm_Avs_curr == null ? "" : this.algorithm_Avs_curr._tableName;
      this.SetElementStr_Avs(tableName);
      if (tableName == "")
      {
        this.button_Avs_AddCell.Enabled = true;
        this.button_Avs_AddAttribut.Enabled = true;
      }
      else
      {
        this.button_Avs_AddCell.Enabled = false;
        this.button_Avs_AddAttribut.Enabled = false;
      }
      this.button_Avs_Edit.Enabled = false;
      this.button_Avs_Delete.Enabled = false;
    }
    else if (this.oneTreeNode_Avs_Current._typeNode == Vedomost_VB_Static.TypeNode_Tree.Info)
    {
      this.listBox_Avs6_Fields.SelectedIndex = -1;
      this.oneDataField_Avs_current = (Vedomost_VB.OneDataField_Avs6_To_Ips) null;
      this.oneGrafa_Avs_Current = (Vedomost_VB.OneGrafa_Avs6_To_Ips) null;
      this.oneRecord_Avs_Current = (Vedomost_VB.OneRecord_Avs6_To_Ips) null;
      this.button_Avs_AddCell.Enabled = false;
      this.button_Avs_AddAttribut.Enabled = false;
      this.button_Avs_Edit.Enabled = false;
      this.button_Avs_Delete.Enabled = false;
      this.comboBox_Avs_TextRazdelitel.Enabled = false;
      this.groupBox_Avs_TextRazdelitel.Enabled = false;
      this.SetElementStr_Avs("");
    }
    else if (this.oneTreeNode_Avs_Current._typeNode == Vedomost_VB_Static.TypeNode_Tree.Record)
    {
      this.listBox_Avs6_Fields.SelectedIndex = -1;
      this.oneDataField_Avs_current = (Vedomost_VB.OneDataField_Avs6_To_Ips) null;
      this.oneGrafa_Avs_Current = (Vedomost_VB.OneGrafa_Avs6_To_Ips) null;
      this.oneRecord_Avs_Current = this.oneTreeNode_Avs_Current._oneRecord_Avs;
      this.button_Avs_AddCell.Enabled = true;
      this.button_Avs_AddAttribut.Enabled = false;
      this.button_Avs_Edit.Enabled = true;
      this.button_Avs_Delete.Enabled = false;
      this.i_curr_oneGrafa_Avs_Current = -1;
      this.comboBox_Avs_TextRazdelitel.Enabled = false;
      this.groupBox_Avs_TextRazdelitel.Enabled = false;
      this.SetElementStr_Avs(this.oneRecord_Avs_Current == null ? "" : this.oneRecord_Avs_Current._tableRowId);
    }
    else if (this.oneTreeNode_Avs_Current._typeNode == Vedomost_VB_Static.TypeNode_Tree.Cell)
    {
      this.listBox_Avs6_Fields.SelectedIndex = -1;
      this.oneDataField_Avs_current = (Vedomost_VB.OneDataField_Avs6_To_Ips) null;
      this.oneGrafa_Avs_Current = this.oneTreeNode_Avs_Current._oneGrafa_Avs;
      this.oneRecord_Avs_Current = this.oneTreeNode_Avs_Current._oneAvsNode_Parent._oneRecord_Avs;
      if (this.oneRecord_Avs_Current != null && this.oneRecord_Avs_Current._listOneGrafa_Avs6_To_Ips != null)
        this.i_curr_oneGrafa_Avs_Current = this.oneRecord_Avs_Current._listOneGrafa_Avs6_To_Ips.IndexOf(this.oneGrafa_Avs_Current);
      this.button_Avs_AddCell.Enabled = true;
      this.button_Avs_AddAttribut.Enabled = true;
      this.button_Avs_Edit.Enabled = true;
      this.button_Avs_Delete.Enabled = true;
      this.comboBox_Avs_TextRazdelitel.Enabled = false;
      this.groupBox_Avs_TextRazdelitel.Enabled = false;
      this.SetElementStr_Avs(this.oneGrafa_Avs_Current == null ? "" : this.oneGrafa_Avs_Current._cell_ID);
    }
    else
    {
      if (this.oneTreeNode_Avs_Current._typeNode == Vedomost_VB_Static.TypeNode_Tree.Data)
      {
        this.listBox_Avs6_Fields.SelectedIndex = -1;
        this.oneDataField_Avs_current = this.oneTreeNode_Avs_Current._oneDataField_Avs;
        this.oneGrafa_Avs_Current = this.oneTreeNode_Avs_Current._oneAvsNode_Parent._oneGrafa_Avs;
        this.oneRecord_Avs_Current = this.oneTreeNode_Avs_Current._oneAvsNode_Parent._oneAvsNode_Parent._oneRecord_Avs;
        this.button_Avs_AddCell.Enabled = false;
        this.button_Avs_AddAttribut.Enabled = true;
        this.button_Avs_Edit.Enabled = true;
        this.button_Avs_Delete.Enabled = true;
        this.comboBox_Avs_TextRazdelitel.Enabled = true;
        this.comboBox_Avs_TextRazdelitel.Text = this.translate_text("", true);
        this.listBox_Avs6_Fields.SelectedIndex = AVS6_From_Avs6Main.IndexByType(AVS6_From_Avs6Main.TypeListFields.Record, (byte) this.oneDataField_Avs_current._objectType);
        currId = this.oneGrafa_Avs_Current == null ? "" : this.oneGrafa_Avs_Current._cell_ID;
        this.SetElementStr_Avs(currId);
        this.comboBox_Avs_TextRazdelitel.Text = this.translate_text(this.oneDataField_Avs_current._symbolRazd, true);
      }
      this.SetElementStr_Avs(currId);
    }
  }

  /// <summary> Кнопка Добавить Ячейку </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void button_Avs_AddCell_Click(object sender, EventArgs e)
  {
    DocumentTreeNode activeElement = this.docControl_Avs.ActiveElement;
    if (activeElement == null || activeElement.Id == "")
      return;
    if (this.oneTreeNode_Avs_Current._typeNode == Vedomost_VB_Static.TypeNode_Tree.Record)
    {
      if (activeElement.NodeClass != "TextBoxElement")
      {
        int num = (int) MessageBox.Show("На шаблоне необходимо выбрать текстовое поле", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      A_NastrTabl.OneAvsNode oneAvsNode = new A_NastrTabl.OneAvsNode();
      oneAvsNode.Text = "Ячейка шаблона: " + activeElement.Id;
      oneAvsNode.ImageIndex = this._indexImageList_Section;
      oneAvsNode.SelectedImageIndex = this._indexImageList_Section;
      A_NastrTabl.OneAvsNode node = oneAvsNode;
      node._typeNode = Vedomost_VB_Static.TypeNode_Tree.Cell;
      node._oneAvsNode_Parent = this.oneTreeNode_Avs_Current;
      node._oneDataField_Avs = (Vedomost_VB.OneDataField_Avs6_To_Ips) null;
      node._oneRecord_Avs = (Vedomost_VB.OneRecord_Avs6_To_Ips) null;
      node._iData = -1;
      Vedomost_VB.OneGrafa_Avs6_To_Ips oneGrafaAvs6ToIps = new Vedomost_VB.OneGrafa_Avs6_To_Ips();
      node._oneGrafa_Avs = oneGrafaAvs6ToIps;
      oneGrafaAvs6ToIps._cell_ID = activeElement.Id;
      oneGrafaAvs6ToIps._listOneDataField_Avs6_To_Ips = new List<Vedomost_VB.OneDataField_Avs6_To_Ips>();
      this.oneTreeNode_Avs_Current.Nodes.Insert(0, (TreeNode) node);
      this.oneRecord_Avs_Current._listOneGrafa_Avs6_To_Ips.Insert(0, oneGrafaAvs6ToIps);
      this.ModifiedAll(true);
      this.IsModified_Page_Avs = true;
    }
    if (this.oneTreeNode_Avs_Current._typeNode == Vedomost_VB_Static.TypeNode_Tree.Cell)
    {
      if (activeElement.NodeClass != "TextBoxElement")
      {
        int num = (int) MessageBox.Show("На шаблоне необходимо выбрать текстовое поле", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      A_NastrTabl.OneAvsNode oneAvsNode = new A_NastrTabl.OneAvsNode();
      oneAvsNode.Text = "Ячейка шаблона: " + activeElement.Id;
      oneAvsNode.ImageIndex = this._indexImageList_Section;
      oneAvsNode.SelectedImageIndex = this._indexImageList_Section;
      A_NastrTabl.OneAvsNode node = oneAvsNode;
      node._typeNode = Vedomost_VB_Static.TypeNode_Tree.Cell;
      node._oneAvsNode_Parent = this.oneTreeNode_Avs_Current._oneAvsNode_Parent;
      node._oneDataField_Avs = (Vedomost_VB.OneDataField_Avs6_To_Ips) null;
      node._oneRecord_Avs = (Vedomost_VB.OneRecord_Avs6_To_Ips) null;
      node._iData = -1;
      Vedomost_VB.OneGrafa_Avs6_To_Ips oneGrafaAvs6ToIps = new Vedomost_VB.OneGrafa_Avs6_To_Ips();
      node._oneGrafa_Avs = oneGrafaAvs6ToIps;
      oneGrafaAvs6ToIps._cell_ID = activeElement.Id;
      oneGrafaAvs6ToIps._listOneDataField_Avs6_To_Ips = new List<Vedomost_VB.OneDataField_Avs6_To_Ips>();
      this.oneTreeNode_Avs_Current._oneAvsNode_Parent.Nodes.Insert(this.oneTreeNode_Avs_Current._oneAvsNode_Parent.Nodes.IndexOf((TreeNode) this.oneTreeNode_Avs_Current) + 1, (TreeNode) node);
      this.oneRecord_Avs_Current._listOneGrafa_Avs6_To_Ips.Insert(this.oneTreeNode_Avs_Current._oneAvsNode_Parent.Nodes.IndexOf((TreeNode) this.oneTreeNode_Avs_Current) + 1, oneGrafaAvs6ToIps);
      this.ModifiedAll(true);
      this.IsModified_Page_Avs = true;
    }
    this.treeView_Avs.Select();
  }

  /// <summary> Кнопка Добавить поле Avs </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void button_Avs_AddAttribut_Click(object sender, EventArgs e)
  {
    if (this.oneTreeNode_Avs_Current._typeNode != Vedomost_VB_Static.TypeNode_Tree.Cell && this.oneTreeNode_Avs_Current._typeNode != Vedomost_VB_Static.TypeNode_Tree.Data)
      return;
    Vedomost_VB.OneDataField_Avs6_To_Ips oneDataField_Avs = new Vedomost_VB.OneDataField_Avs6_To_Ips();
    if (this.listBox_Avs6_Fields.SelectedIndex <= -1)
      return;
    byte num1 = AVS6_From_Avs6Main.FieldTypeByIndex(AVS6_From_Avs6Main.TypeListFields.Record, this.listBox_Avs6_Fields.SelectedIndex);
    oneDataField_Avs._objectType = (int) num1;
    A_NastrTabl.OneAvsNode node;
    if (this.oneTreeNode_Avs_Current._typeNode == Vedomost_VB_Static.TypeNode_Tree.Cell)
    {
      oneDataField_Avs._symbolRazd = "";
      node = this.oneDataNode_Avs_Create(oneDataField_Avs, this.oneTreeNode_Avs_Current, 0);
    }
    else
    {
      oneDataField_Avs._symbolRazd = this.translate_text(this.comboBox_Avs_TextRazdelitel.Text, false);
      node = this.oneDataNode_Avs_Create(oneDataField_Avs, this.oneTreeNode_Avs_Current, this.oneTreeNode_Avs_Current._iData + 1);
    }
    node._oneGrafa_Avs = (Vedomost_VB.OneGrafa_Avs6_To_Ips) null;
    node._oneDataField_Avs = oneDataField_Avs;
    node._oneRecord_Avs = (Vedomost_VB.OneRecord_Avs6_To_Ips) null;
    node._typeNode = Vedomost_VB_Static.TypeNode_Tree.Data;
    node._iData = this.oneTreeNode_Avs_Current._iData + 1;
    if (this.oneTreeNode_Avs_Current._typeNode == Vedomost_VB_Static.TypeNode_Tree.Cell)
    {
      this.oneGrafa_Avs_Current._listOneDataField_Avs6_To_Ips.Insert(0, oneDataField_Avs);
      this.oneTreeNode_Avs_Current.Nodes.Insert(0, (TreeNode) node);
    }
    if (this.oneTreeNode_Avs_Current._typeNode == Vedomost_VB_Static.TypeNode_Tree.Data)
    {
      int num2 = this.oneGrafa_Avs_Current._listOneDataField_Avs6_To_Ips.IndexOf(this.oneDataField_Avs_current);
      this.oneGrafa_Avs_Current._listOneDataField_Avs6_To_Ips.Insert(num2 + 1, oneDataField_Avs);
      this.oneTreeNode_Avs_Current._oneAvsNode_Parent.Nodes.Insert(num2 + 1, (TreeNode) node);
    }
    this.ModifiedAll(true);
    this.IsModified_Page_Avs = true;
    this.oneTreeNode_Avs_Current.Expand();
    this.treeView_Avs.Select();
  }

  /// <summary> Кнопка ИЗМЕНИТЬ </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void button_Avs_Edit_Click(object sender, EventArgs e)
  {
    DocumentTreeNode activeElement = this.docControl_Avs.ActiveElement;
    string str = "";
    string id = activeElement.Id;
    string name1 = activeElement.Name;
    this.oneTreeNode_Avs_Current = (A_NastrTabl.OneAvsNode) this.treeView_Avs.SelectedNode;
    if (this.oneTreeNode_Avs_Current == null)
    {
      int num1 = (int) MessageBox.Show("В \"Дереве\" не выбран элемент", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    else
    {
      if (this.oneTreeNode_Avs_Current._typeNode == Vedomost_VB_Static.TypeNode_Tree.Main)
      {
        if (activeElement.NodeClass != "TableElement")
        {
          int num2 = (int) MessageBox.Show("На шаблоне не выбрана таблица", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
          return;
        }
        string name2 = activeElement.Name;
        if (name2 != "")
        {
          this.algorithm_Avs_curr._tableName = name2;
          this.ModifiedAll(true);
          this.IsModified_Page_Avs = true;
        }
      }
      if (this.oneTreeNode_Avs_Current._typeNode == Vedomost_VB_Static.TypeNode_Tree.Record)
      {
        if (activeElement.NodeClass != "TableElement")
        {
          int num3 = (int) MessageBox.Show("На шаблоне не выбрана строка", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
          return;
        }
        if (activeElement.Id != "" && this.oneRecord_Avs_Current != null && activeElement.Id != "")
        {
          this.oneRecord_Avs_Current._tableRowId = activeElement.Id;
          this.oneTreeNode_Avs_Current.Text = $"{Vedomost_VB_Static.TypeRecName_by_TypeRec_Avs(this.oneRecord_Avs_Current._nameTypeRec)}: Строка: {this.oneRecord_Avs_Current._tableRowId}";
          this.ModifiedAll(true);
          this.IsModified_Page_Avs = true;
        }
      }
      if (this.oneTreeNode_Avs_Current._typeNode == Vedomost_VB_Static.TypeNode_Tree.Cell)
      {
        if (activeElement.NodeClass != "TextBoxElement" || !(activeElement.Id != ""))
        {
          int num4 = (int) MessageBox.Show("На шаблоне не выбрано текстовое поле", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
          return;
        }
        str = activeElement.Id;
        if (this.oneRecord_Avs_Current != null)
        {
          this.oneRecord_Avs_Current._tableRowId = activeElement.Id;
          this.oneTreeNode_Avs_Current.Text = "Ячейка шаблона: " + this.oneRecord_Avs_Current._tableRowId;
          this.ModifiedAll(true);
          this.IsModified_Page_Avs = true;
        }
      }
      if (this.oneTreeNode_Avs_Current._typeNode == Vedomost_VB_Static.TypeNode_Tree.Data)
      {
        if (activeElement.NodeClass != "TextBoxElement" || !(activeElement.Id != ""))
        {
          int num5 = (int) MessageBox.Show("На шаблоне не выбрано текстовое поле", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
          return;
        }
        if (this.listBox_Avs6_Fields.SelectedIndex > -1)
        {
          this.oneDataField_Avs_current._objectType = (int) AVS6_From_Avs6Main.FieldTypeByIndex(AVS6_From_Avs6Main.TypeListFields.Record, this.listBox_Avs6_Fields.SelectedIndex);
          this.oneDataField_Avs_current._symbolRazd = this.translate_text(this.comboBox_Avs_TextRazdelitel.Text, false);
          this.oneTreeNode_Avs_Current.Text = this.OneDataField_Avs_Draw(this.oneDataField_Avs_current, this.oneTreeNode_Avs_Current._iData);
          this.ModifiedAll(true);
          this.IsModified_Page_Avs = true;
        }
      }
      this.treeView_Avs.Select();
    }
  }

  /// <summary> Кнопка УДАЛИТЬ </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void button_Avs_Delete_Click(object sender, EventArgs e)
  {
    if (this.oneTreeNode_Avs_Current._typeNode != Vedomost_VB_Static.TypeNode_Tree.Cell && this.oneTreeNode_Avs_Current._typeNode != Vedomost_VB_Static.TypeNode_Tree.Data)
      return;
    A_NastrTabl.OneAvsNode oneAvsNode = (A_NastrTabl.OneAvsNode) this.oneTreeNode_Avs_Current.PrevNode ?? this.oneTreeNode_Avs_Current._oneAvsNode_Parent;
    if (this.oneTreeNode_Avs_Current._typeNode == Vedomost_VB_Static.TypeNode_Tree.Data && this.oneTreeNode_Avs_Current._iData > -1 && this.oneGrafa_Avs_Current._listOneDataField_Avs6_To_Ips != null && this.oneTreeNode_Avs_Current._iData < this.oneGrafaToPrint_Current._listOneDataFieldToPrint.Count)
      this.oneGrafa_Avs_Current._listOneDataField_Avs6_To_Ips.RemoveAt(this.oneTreeNode_Avs_Current._iData);
    else if (this.oneTreeNode_Avs_Current._typeNode == Vedomost_VB_Static.TypeNode_Tree.Cell && this.i_curr_oneGrafa_Avs_Current > -1 && this.oneRecord_Avs_Current._listOneGrafa_Avs6_To_Ips != null && this.oneRecord_Avs_Current._listOneGrafa_Avs6_To_Ips.Count > this.i_curr_oneGrafa_Avs_Current)
      this.oneRecord_Avs_Current._listOneGrafa_Avs6_To_Ips.RemoveAt(this.i_curr_oneGrafa_Avs_Current);
    this.oneTreeNode_Avs_Current.Remove();
    if (oneAvsNode != null)
      this.treeView_Avs.SelectedNode = (TreeNode) oneAvsNode;
    this.treeView_Avs.Select();
    this.ModifiedAll(true);
    this.IsModified_Page_Avs = true;
  }

  private void tabPage_Bases_HelpRequested(object sender, HelpEventArgs hlpevent)
  {
    Help.ShowHelp((Control) this, "D:\\AVS6\\bin\\AVS6.chm", HelpNavigator.TopicId, (object) "37800");
  }

  private void label_ServicesFileOpen_Click(object sender, EventArgs e)
  {
    ICurrentUserAndRole service = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
    string userName = service.UserName;
    int num = service.IsAdmin ? 1 : 0;
  }

  /// <summary>Редактор шаблона (бланка)</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void buttonEditTemplate_Click(object sender, EventArgs e)
  {
    DocumentEditorPlugin.Instance.OpenDocumentImDocumentObject(this.templateID_curr_Vyvod, false, true);
    int num = (int) MessageBox.Show("Редактор шаблона (бланка) открыт на общей панели\r\nДля доступа к редактору закройте окно настройки", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (A_NastrTabl));
    this.toolTip1 = new ToolTip(this.components);
    this.buttonWarnings = new Button();
    this.buttonCopyFrom = new Button();
    this.buttonSelectTabl = new Button();
    this.buttonSave1 = new Button();
    this.buttonDefault = new Button();
    this.bCancel = new Button();
    this.bOK = new Button();
    this.button_Vyvod_AddAttribut = new Button();
    this.groupBox_Vyvod_TextRazdelitel = new GroupBox();
    this.comboBox_Vyvod_TextRazdelitel = new ComboBox();
    this.buttonServicesFileOpen = new Button();
    this.buttonServiceCreateDump = new Button();
    this.buttonServicesTypeVedTo = new Button();
    this.buttonServicesCopyAll = new Button();
    this.buttonServicesDefaultAll = new Button();
    this.groupBox_Usl_Bases_ImbaseCatalog = new GroupBox();
    this.label_QuickObjectInfo = new Label();
    this.label_CatalogsImbase = new Label();
    this.button_Delete_From_To_listBox_QuickObjectInfo = new Button();
    this.button_Add_To_listBox_QuickObjectInfo = new Button();
    this.listBox_CatalogsImbase = new ListBox();
    this.listBox_QuickObjectInfo = new ListBox();
    this.groupBox_Usl_Bases_Sbor_Input = new GroupBox();
    this.checkBox_Usl_Bases_Sbor_isInputMat = new CheckBox();
    this.checkBox_Usl_Bases_Sbor_isInputIzd = new CheckBox();
    this.checkBox_Usl_Bases_Sbor_isInputDoc = new CheckBox();
    this.groupBox_Xml_Text = new GroupBox();
    this.textBox_Xml_Text = new TextBox();
    this.groupBox_Xml_EmptyString = new GroupBox();
    this.label_Xml_AfterRemark = new Label();
    this.numeric_UpDown_Xml_AfterRemark = new NumericUpDown();
    this.label_Xml_AfterInfo = new Label();
    this.numeric_UpDown_Xml_AfterInfo = new NumericUpDown();
    this.groupBox_Xml_Out = new GroupBox();
    this.radioButton_Xml_PassportOutNo = new RadioButton();
    this.radioButton_Xml_PassportOutDialog = new RadioButton();
    this.radioButton_Xml_PassporOutAlways = new RadioButton();
    this.groupBox_Xml_In = new GroupBox();
    this.radioButton_Xml_PassportInNo = new RadioButton();
    this.radioButton_Xml_PassportInDialog = new RadioButton();
    this.radioButton_Xml_PassporInAlways = new RadioButton();
    this.button_Vyvod_Delete = new Button();
    this.button_Vyvod_Edit = new Button();
    this.button_Vyvod_AddCell = new Button();
    this.button_Xml_Delete = new Button();
    this.button_Xml_Edit = new Button();
    this.button_Xml_Add = new Button();
    this.groupBox_Xml_Folder_In = new GroupBox();
    this.button_Xml_Folder_In = new Button();
    this.textBox_Xml_Folder_In = new TextBox();
    this.groupBox_Vyvod_Additional = new GroupBox();
    this.checkBox_Vyvod_Additional4 = new CheckBox();
    this.checkBox_Vyvod_Additional3 = new CheckBox();
    this.checkBox_Vyvod_Additional2 = new CheckBox();
    this.checkBox_Vyvod_Additional1 = new CheckBox();
    this.button_Avs_AddAttribut = new Button();
    this.button_Avs_Delete = new Button();
    this.button_Avs_Edit = new Button();
    this.button_Avs_AddCell = new Button();
    this.groupBox_Avs_TextRazdelitel = new GroupBox();
    this.comboBox_Avs_TextRazdelitel = new ComboBox();
    this.groupBox_Vyvod_List_Ved_Id = new GroupBox();
    this.listBox_Vyvod_List_Ved_Id = new ListBox();
    this.buttonEditTemplate = new Button();
    this.imageList1 = new ImageList(this.components);
    this.imagesToolbars = new ImageList(this.components);
    this.imageListSort = new ImageList(this.components);
    this.panelForButtons = new Panel();
    this.tabControl_Nastr = new System.Windows.Forms.TabControl();
    this.tabPage_Bases = new System.Windows.Forms.TabPage();
    this.tabPage_Sbor = new System.Windows.Forms.TabPage();
    this.button_Sbor_Peredatha_Delete2 = new Button();
    this.button_Sbor_Peredatha_Add2 = new Button();
    this.groupBox_Sbor_Peredatha_AttributeControl1 = new GroupBox();
    this.select_Sbor_Peredatha_AttributeControl2 = new SelectAvsAttributeControl();
    this.groupBox_Sbor_Peredatha_ListId = new GroupBox();
    this.listBox_Sbor_Peredatha_ListId = new ListBox();
    this.tabPage_Vyvod = new System.Windows.Forms.TabPage();
    this.docKcontainer_Vyvod = new DockContainer();
    this.dockMan_Vyvod = new DockManager();
    this.docContainer_Vyvod = new DocumentContainer();
    this.panel_Vyvod_1 = new Panel();
    this.treeView_Vyvod = new TreeView();
    this.tabPage_Xml = new System.Windows.Forms.TabPage();
    this.treeView_Xml = new TreeView();
    this.docContainer_Xml = new DocumentContainer();
    this.docKcontainer_Xml = new DockContainer();
    this.dockMan_Xml = new DockManager();
    this.tabPage_Avs6 = new System.Windows.Forms.TabPage();
    this.panel_Avs_1 = new Panel();
    this.treeView_Avs = new TreeView();
    this.groupBox_Avs6_Fields = new GroupBox();
    this.listBox_Avs6_Fields = new ListBox();
    this.docContainer_Avs = new DocumentContainer();
    this.dockcontainer_Avs = new DockContainer();
    this.dockMan_Avs = new DockManager();
    this.tabPage_Service = new System.Windows.Forms.TabPage();
    this.groupBox_AccessLevel = new GroupBox();
    this.radioButton_AccessLevel2 = new RadioButton();
    this.radioButton_AccessLevel1 = new RadioButton();
    this.radioButton_AccessLevel0 = new RadioButton();
    this.checkBox_Services_CreateDump = new CheckBox();
    this.labelService2 = new Label();
    this.labelService1 = new Label();
    this.label_ServicesFileOpen = new Label();
    this.label_ServiceCreateDump = new Label();
    this.label_ServicesTypeVedTo = new Label();
    this.label_ServicesCopyAll = new Label();
    this.label_ServicesDefaultAll = new Label();
    this.groupBox_Dump = new GroupBox();
    this.groupBox_Vyvod_TextRazdelitel.SuspendLayout();
    this.groupBox_Usl_Bases_ImbaseCatalog.SuspendLayout();
    this.groupBox_Usl_Bases_Sbor_Input.SuspendLayout();
    this.groupBox_Xml_Text.SuspendLayout();
    this.groupBox_Xml_EmptyString.SuspendLayout();
    this.numeric_UpDown_Xml_AfterRemark.BeginInit();
    this.numeric_UpDown_Xml_AfterInfo.BeginInit();
    this.groupBox_Xml_Out.SuspendLayout();
    this.groupBox_Xml_In.SuspendLayout();
    this.groupBox_Xml_Folder_In.SuspendLayout();
    this.groupBox_Vyvod_Additional.SuspendLayout();
    this.groupBox_Avs_TextRazdelitel.SuspendLayout();
    this.groupBox_Vyvod_List_Ved_Id.SuspendLayout();
    this.panelForButtons.SuspendLayout();
    this.tabControl_Nastr.SuspendLayout();
    this.tabPage_Bases.SuspendLayout();
    this.tabPage_Sbor.SuspendLayout();
    this.groupBox_Sbor_Peredatha_AttributeControl1.SuspendLayout();
    this.groupBox_Sbor_Peredatha_ListId.SuspendLayout();
    this.tabPage_Vyvod.SuspendLayout();
    this.panel_Vyvod_1.SuspendLayout();
    this.tabPage_Xml.SuspendLayout();
    this.tabPage_Avs6.SuspendLayout();
    this.panel_Avs_1.SuspendLayout();
    this.groupBox_Avs6_Fields.SuspendLayout();
    this.tabPage_Service.SuspendLayout();
    this.groupBox_AccessLevel.SuspendLayout();
    this.SuspendLayout();
    this.toolTip1.IsBalloon = true;
    this.toolTip1.ToolTipIcon = ToolTipIcon.Info;
    this.toolTip1.ToolTipTitle = "Подсказка";
    this.buttonWarnings.ForeColor = Color.Red;
    this.buttonWarnings.Location = new Point(700, 5);
    this.buttonWarnings.Name = "buttonWarnings";
    this.buttonWarnings.Size = new Size(121, 27);
    this.buttonWarnings.TabIndex = 9;
    this.buttonWarnings.Text = "Предупреждения";
    this.toolTip1.SetToolTip((Control) this.buttonWarnings, "Смотреть список предупреждений");
    this.buttonWarnings.UseVisualStyleBackColor = true;
    this.buttonWarnings.Visible = false;
    this.buttonCopyFrom.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.buttonCopyFrom.Location = new Point(850, 5);
    this.buttonCopyFrom.Name = "buttonCopyFrom";
    this.buttonCopyFrom.Size = new Size(121, 27);
    this.buttonCopyFrom.TabIndex = 7;
    this.buttonCopyFrom.Text = "Копировать из ...";
    this.toolTip1.SetToolTip((Control) this.buttonCopyFrom, "Всем значениям ТЕКУЩЕЙ страницы копировать значения из другой таблицы");
    this.buttonCopyFrom.UseVisualStyleBackColor = true;
    this.buttonCopyFrom.Click += new EventHandler(this.buttonCopyFrom_Click);
    this.buttonSelectTabl.Location = new Point(12, 5);
    this.buttonSelectTabl.Name = "buttonSelectTabl";
    this.buttonSelectTabl.Size = new Size(168, 27);
    this.buttonSelectTabl.TabIndex = 6;
    this.buttonSelectTabl.Text = "Выбрать таблицу";
    this.toolTip1.SetToolTip((Control) this.buttonSelectTabl, "Выбрать для настройки другую таблицу");
    this.buttonSelectTabl.UseVisualStyleBackColor = true;
    this.buttonSelectTabl.Click += new EventHandler(this.buttonSelectTabl_Click);
    this.buttonSave1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.buttonSave1.Enabled = false;
    this.buttonSave1.Location = new Point(1150, 5);
    this.buttonSave1.Name = "buttonSave1";
    this.buttonSave1.Size = new Size(121, 27);
    this.buttonSave1.TabIndex = 4;
    this.buttonSave1.Text = "Сохранить";
    this.toolTip1.SetToolTip((Control) this.buttonSave1, "Сохранить все произведенные изменения. Окно диалога не закрывать");
    this.buttonSave1.UseVisualStyleBackColor = true;
    this.buttonSave1.Click += new EventHandler(this.buttonSave1_Click);
    this.buttonDefault.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.buttonDefault.Location = new Point(1000, 5);
    this.buttonDefault.Name = "buttonDefault";
    this.buttonDefault.Size = new Size(121, 27);
    this.buttonDefault.TabIndex = 3;
    this.buttonDefault.Text = "По умолчанию";
    this.toolTip1.SetToolTip((Control) this.buttonDefault, "Всем значениям ТЕКУЩЕЙ страницы присвоить значения по умолчанию");
    this.buttonDefault.UseVisualStyleBackColor = true;
    this.buttonDefault.Click += new EventHandler(this.buttonDefault_Click);
    this.bCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bCancel.DialogResult = DialogResult.Cancel;
    this.bCancel.Location = new Point(1450, 5);
    this.bCancel.Name = "bCancel";
    this.bCancel.Size = new Size(121, 27);
    this.bCancel.TabIndex = 2;
    this.bCancel.Text = "Закрыть";
    this.toolTip1.SetToolTip((Control) this.bCancel, "Закрыть окно диалога");
    this.bCancel.UseVisualStyleBackColor = true;
    this.bOK.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bOK.DialogResult = DialogResult.OK;
    this.bOK.Location = new Point(1300, 5);
    this.bOK.Name = "bOK";
    this.bOK.Size = new Size(121, 27);
    this.bOK.TabIndex = 1;
    this.bOK.Text = "OK";
    this.toolTip1.SetToolTip((Control) this.bOK, "Сохранить все произведенные изменения и закрыть окно диалога");
    this.bOK.UseVisualStyleBackColor = true;
    this.bOK.Click += new EventHandler(this.bOK_Click);
    this.button_Vyvod_AddAttribut.AccessibleRole = AccessibleRole.OutlineButton;
    this.button_Vyvod_AddAttribut.Enabled = false;
    this.button_Vyvod_AddAttribut.ImageAlign = ContentAlignment.MiddleRight;
    this.button_Vyvod_AddAttribut.Location = new Point(11, 488);
    this.button_Vyvod_AddAttribut.Name = "button_Vyvod_AddAttribut";
    this.button_Vyvod_AddAttribut.Size = new Size(202, 27);
    this.button_Vyvod_AddAttribut.TabIndex = 16 /*0x10*/;
    this.button_Vyvod_AddAttribut.Text = "Добавить атрибут";
    this.toolTip1.SetToolTip((Control) this.button_Vyvod_AddAttribut, "Добавить атрибут");
    this.button_Vyvod_AddAttribut.UseVisualStyleBackColor = true;
    this.button_Vyvod_AddAttribut.Click += new EventHandler(this.button_Vyvod_AddAttribut_Click);
    this.groupBox_Vyvod_TextRazdelitel.Controls.Add((Control) this.comboBox_Vyvod_TextRazdelitel);
    this.groupBox_Vyvod_TextRazdelitel.Location = new Point(11, 402);
    this.groupBox_Vyvod_TextRazdelitel.Name = "groupBox_Vyvod_TextRazdelitel";
    this.groupBox_Vyvod_TextRazdelitel.Size = new Size(202, 41);
    this.groupBox_Vyvod_TextRazdelitel.TabIndex = 12;
    this.groupBox_Vyvod_TextRazdelitel.TabStop = false;
    this.groupBox_Vyvod_TextRazdelitel.Text = "Текст разделитель";
    this.toolTip1.SetToolTip((Control) this.groupBox_Vyvod_TextRazdelitel, "Текст (символ) разделитель, отделяющий отделяющий от предыдущего текста");
    this.comboBox_Vyvod_TextRazdelitel.Dock = DockStyle.Fill;
    this.comboBox_Vyvod_TextRazdelitel.FormattingEnabled = true;
    this.comboBox_Vyvod_TextRazdelitel.Items.AddRange(new object[9]
    {
      (object) "  (пробел)",
      (object) "(неразрывный пробел)",
      (object) "(без пробела)",
      (object) "(принудительный перенос)",
      (object) ". (точка)",
      (object) ", (запятая)",
      (object) "* (звездочка)",
      (object) "- (минус)",
      (object) "(неразрывный дефис)"
    });
    this.comboBox_Vyvod_TextRazdelitel.Location = new Point(3, 16 /*0x10*/);
    this.comboBox_Vyvod_TextRazdelitel.Name = "comboBox_Vyvod_TextRazdelitel";
    this.comboBox_Vyvod_TextRazdelitel.Size = new Size(196, 21);
    this.comboBox_Vyvod_TextRazdelitel.TabIndex = 3;
    this.buttonServicesFileOpen.Location = new Point(40, 347);
    this.buttonServicesFileOpen.Name = "buttonServicesFileOpen";
    this.buttonServicesFileOpen.Size = new Size(168, 27);
    this.buttonServicesFileOpen.TabIndex = 30;
    this.buttonServicesFileOpen.Text = "Читать из файла";
    this.toolTip1.SetToolTip((Control) this.buttonServicesFileOpen, "Прочитать параметры настроек из файла (Dump)");
    this.buttonServicesFileOpen.UseVisualStyleBackColor = true;
    this.buttonServicesFileOpen.Click += new EventHandler(this.buttonServicesFileOpen_Click);
    this.buttonServiceCreateDump.Location = new Point(40, 297);
    this.buttonServiceCreateDump.Name = "buttonServiceCreateDump";
    this.buttonServiceCreateDump.Size = new Size(168, 27);
    this.buttonServiceCreateDump.TabIndex = 29;
    this.buttonServiceCreateDump.Text = "Создать Dump";
    this.toolTip1.SetToolTip((Control) this.buttonServiceCreateDump, "Сохранить параметры настроек в файл (Dump)");
    this.buttonServiceCreateDump.UseVisualStyleBackColor = true;
    this.buttonServiceCreateDump.Click += new EventHandler(this.buttonServiceCreateDump_Click);
    this.buttonServicesTypeVedTo.Location = new Point(21, 124);
    this.buttonServicesTypeVedTo.Name = "buttonServicesTypeVedTo";
    this.buttonServicesTypeVedTo.Size = new Size(168, 27);
    this.buttonServicesTypeVedTo.TabIndex = 28;
    this.buttonServicesTypeVedTo.Text = "Тип таблицы";
    this.toolTip1.SetToolTip((Control) this.buttonServicesTypeVedTo, "Текущему виду таблицы присвоить свойства определенной системной таблицы, например \"Таблица соединений\"");
    this.buttonServicesTypeVedTo.UseVisualStyleBackColor = true;
    this.buttonServicesTypeVedTo.Click += new EventHandler(this.buttonServicesTypeVedTo_Click);
    this.buttonServicesCopyAll.Location = new Point(21, 74);
    this.buttonServicesCopyAll.Name = "buttonServicesCopyAll";
    this.buttonServicesCopyAll.Size = new Size(168, 27);
    this.buttonServicesCopyAll.TabIndex = 27;
    this.buttonServicesCopyAll.Text = "Копировать все из ...";
    this.toolTip1.SetToolTip((Control) this.buttonServicesCopyAll, "Все значениям настройки для текущего типа таблицы копировать значения из другой ведомости");
    this.buttonServicesCopyAll.UseVisualStyleBackColor = true;
    this.buttonServicesCopyAll.Click += new EventHandler(this.buttonServicesCopyAll_Click);
    this.buttonServicesDefaultAll.Location = new Point(21, 24);
    this.buttonServicesDefaultAll.Name = "buttonServicesDefaultAll";
    this.buttonServicesDefaultAll.Size = new Size(168, 27);
    this.buttonServicesDefaultAll.TabIndex = 26;
    this.buttonServicesDefaultAll.Text = "По умолчанию все";
    this.toolTip1.SetToolTip((Control) this.buttonServicesDefaultAll, "Всем значениям настройки для текущего типа тблицы присвоить значения по умолчанию");
    this.buttonServicesDefaultAll.UseVisualStyleBackColor = true;
    this.buttonServicesDefaultAll.Click += new EventHandler(this.buttonServicesDefaultAll_Click);
    this.groupBox_Usl_Bases_ImbaseCatalog.Controls.Add((Control) this.label_QuickObjectInfo);
    this.groupBox_Usl_Bases_ImbaseCatalog.Controls.Add((Control) this.label_CatalogsImbase);
    this.groupBox_Usl_Bases_ImbaseCatalog.Controls.Add((Control) this.button_Delete_From_To_listBox_QuickObjectInfo);
    this.groupBox_Usl_Bases_ImbaseCatalog.Controls.Add((Control) this.button_Add_To_listBox_QuickObjectInfo);
    this.groupBox_Usl_Bases_ImbaseCatalog.Controls.Add((Control) this.listBox_CatalogsImbase);
    this.groupBox_Usl_Bases_ImbaseCatalog.Controls.Add((Control) this.listBox_QuickObjectInfo);
    this.groupBox_Usl_Bases_ImbaseCatalog.Location = new Point(6, 125);
    this.groupBox_Usl_Bases_ImbaseCatalog.Name = "groupBox_Usl_Bases_ImbaseCatalog";
    this.groupBox_Usl_Bases_ImbaseCatalog.Size = new Size(434, 193);
    this.groupBox_Usl_Bases_ImbaseCatalog.TabIndex = 2;
    this.groupBox_Usl_Bases_ImbaseCatalog.TabStop = false;
    this.groupBox_Usl_Bases_ImbaseCatalog.Text = "Ввод в диалоге из Imbase";
    this.toolTip1.SetToolTip((Control) this.groupBox_Usl_Bases_ImbaseCatalog, "Выбор Каталогов Imbase, доступных для выбора данных");
    this.label_QuickObjectInfo.AutoSize = true;
    this.label_QuickObjectInfo.Location = new Point(263, 20);
    this.label_QuickObjectInfo.Name = "label_QuickObjectInfo";
    this.label_QuickObjectInfo.Size = new Size(115, 13);
    this.label_QuickObjectInfo.TabIndex = 8;
    this.label_QuickObjectInfo.Text = "Выбранные каталоги";
    this.label_CatalogsImbase.AutoSize = true;
    this.label_CatalogsImbase.Location = new Point(15, 20);
    this.label_CatalogsImbase.Name = "label_CatalogsImbase";
    this.label_CatalogsImbase.Size = new Size(112 /*0x70*/, 13);
    this.label_CatalogsImbase.TabIndex = 7;
    this.label_CatalogsImbase.Text = "Все каталоги Imbase";
    this.button_Delete_From_To_listBox_QuickObjectInfo.Image = (Image) Resources.arrow_left_green;
    this.button_Delete_From_To_listBox_QuickObjectInfo.Location = new Point(196, 89);
    this.button_Delete_From_To_listBox_QuickObjectInfo.Name = "button_Delete_From_To_listBox_QuickObjectInfo";
    this.button_Delete_From_To_listBox_QuickObjectInfo.Size = new Size(39, 23);
    this.button_Delete_From_To_listBox_QuickObjectInfo.TabIndex = 6;
    this.toolTip1.SetToolTip((Control) this.button_Delete_From_To_listBox_QuickObjectInfo, "Удалить из списка выбранных каталогов");
    this.button_Delete_From_To_listBox_QuickObjectInfo.UseVisualStyleBackColor = true;
    this.button_Delete_From_To_listBox_QuickObjectInfo.Click += new EventHandler(this.button_Delete_From_To_listBox_QuickObjectInfo_Click);
    this.button_Delete_From_To_listBox_QuickObjectInfo.HelpRequested += new HelpEventHandler(this.tabPage_Bases_HelpRequested);
    this.button_Add_To_listBox_QuickObjectInfo.Image = (Image) Resources.arrow_right_green;
    this.button_Add_To_listBox_QuickObjectInfo.Location = new Point(196, 56);
    this.button_Add_To_listBox_QuickObjectInfo.Name = "button_Add_To_listBox_QuickObjectInfo";
    this.button_Add_To_listBox_QuickObjectInfo.Size = new Size(39, 23);
    this.button_Add_To_listBox_QuickObjectInfo.TabIndex = 5;
    this.toolTip1.SetToolTip((Control) this.button_Add_To_listBox_QuickObjectInfo, "Внести в список выбранных каталогов");
    this.button_Add_To_listBox_QuickObjectInfo.UseVisualStyleBackColor = true;
    this.button_Add_To_listBox_QuickObjectInfo.Click += new EventHandler(this.button_Add_To_listBox_QuickObjectInfo_Click);
    this.button_Add_To_listBox_QuickObjectInfo.HelpRequested += new HelpEventHandler(this.tabPage_Bases_HelpRequested);
    this.listBox_CatalogsImbase.BackColor = Color.FloralWhite;
    this.listBox_CatalogsImbase.FormattingEnabled = true;
    this.listBox_CatalogsImbase.Location = new Point(6, 37);
    this.listBox_CatalogsImbase.Name = "listBox_CatalogsImbase";
    this.listBox_CatalogsImbase.Size = new Size(170, 147);
    this.listBox_CatalogsImbase.TabIndex = 4;
    this.toolTip1.SetToolTip((Control) this.listBox_CatalogsImbase, "Каталоги Imbase");
    this.listBox_CatalogsImbase.HelpRequested += new HelpEventHandler(this.tabPage_Bases_HelpRequested);
    this.listBox_QuickObjectInfo.BackColor = Color.FloralWhite;
    this.listBox_QuickObjectInfo.FormattingEnabled = true;
    this.listBox_QuickObjectInfo.Location = new Point(256 /*0x0100*/, 37);
    this.listBox_QuickObjectInfo.Name = "listBox_QuickObjectInfo";
    this.listBox_QuickObjectInfo.Size = new Size(170, 147);
    this.listBox_QuickObjectInfo.TabIndex = 0;
    this.toolTip1.SetToolTip((Control) this.listBox_QuickObjectInfo, "Каталоги Imbase доступные для выбора данных");
    this.groupBox_Usl_Bases_Sbor_Input.Controls.Add((Control) this.checkBox_Usl_Bases_Sbor_isInputMat);
    this.groupBox_Usl_Bases_Sbor_Input.Controls.Add((Control) this.checkBox_Usl_Bases_Sbor_isInputIzd);
    this.groupBox_Usl_Bases_Sbor_Input.Controls.Add((Control) this.checkBox_Usl_Bases_Sbor_isInputDoc);
    this.groupBox_Usl_Bases_Sbor_Input.Location = new Point(6, 6);
    this.groupBox_Usl_Bases_Sbor_Input.Name = "groupBox_Usl_Bases_Sbor_Input";
    this.groupBox_Usl_Bases_Sbor_Input.Size = new Size(434, 101);
    this.groupBox_Usl_Bases_Sbor_Input.TabIndex = 3;
    this.groupBox_Usl_Bases_Sbor_Input.TabStop = false;
    this.groupBox_Usl_Bases_Sbor_Input.Text = "Ввод в диалоге существующих объектов";
    this.toolTip1.SetToolTip((Control) this.groupBox_Usl_Bases_Sbor_Input, "При выполнении команды \"Добавить запись с существующим объектом\" давать доступ к объектам данного типа");
    this.groupBox_Usl_Bases_Sbor_Input.HelpRequested += new HelpEventHandler(this.tabPage_Bases_HelpRequested);
    this.checkBox_Usl_Bases_Sbor_isInputMat.AutoSize = true;
    this.checkBox_Usl_Bases_Sbor_isInputMat.Checked = true;
    this.checkBox_Usl_Bases_Sbor_isInputMat.CheckState = CheckState.Checked;
    this.checkBox_Usl_Bases_Sbor_isInputMat.Location = new Point(18, 70);
    this.checkBox_Usl_Bases_Sbor_isInputMat.Name = "checkBox_Usl_Bases_Sbor_isInputMat";
    this.checkBox_Usl_Bases_Sbor_isInputMat.Size = new Size(84, 17);
    this.checkBox_Usl_Bases_Sbor_isInputMat.TabIndex = 2;
    this.checkBox_Usl_Bases_Sbor_isInputMat.Text = "Материалы";
    this.toolTip1.SetToolTip((Control) this.checkBox_Usl_Bases_Sbor_isInputMat, "При выполнении команды \"Добавить существующий объект\" давать доступ к Материалы");
    this.checkBox_Usl_Bases_Sbor_isInputMat.UseVisualStyleBackColor = true;
    this.checkBox_Usl_Bases_Sbor_isInputMat.CheckedChanged += new EventHandler(this.checkBox_Usl_Bases_Sbor_isInputMat_CheckedChanged);
    this.checkBox_Usl_Bases_Sbor_isInputIzd.AutoSize = true;
    this.checkBox_Usl_Bases_Sbor_isInputIzd.Checked = true;
    this.checkBox_Usl_Bases_Sbor_isInputIzd.CheckState = CheckState.Checked;
    this.checkBox_Usl_Bases_Sbor_isInputIzd.Location = new Point(18, 47);
    this.checkBox_Usl_Bases_Sbor_isInputIzd.Name = "checkBox_Usl_Bases_Sbor_isInputIzd";
    this.checkBox_Usl_Bases_Sbor_isInputIzd.Size = new Size(70, 17);
    this.checkBox_Usl_Bases_Sbor_isInputIzd.TabIndex = 1;
    this.checkBox_Usl_Bases_Sbor_isInputIzd.Text = "Изделия";
    this.toolTip1.SetToolTip((Control) this.checkBox_Usl_Bases_Sbor_isInputIzd, "При выполнении команды \"Добавить существующий объект\" давать доступ к Изделиям");
    this.checkBox_Usl_Bases_Sbor_isInputIzd.UseVisualStyleBackColor = true;
    this.checkBox_Usl_Bases_Sbor_isInputIzd.CheckedChanged += new EventHandler(this.checkBox_Usl_Bases_Sbor_isInputIzd_CheckedChanged);
    this.checkBox_Usl_Bases_Sbor_isInputIzd.HelpRequested += new HelpEventHandler(this.tabPage_Bases_HelpRequested);
    this.checkBox_Usl_Bases_Sbor_isInputDoc.AutoSize = true;
    this.checkBox_Usl_Bases_Sbor_isInputDoc.Checked = true;
    this.checkBox_Usl_Bases_Sbor_isInputDoc.CheckState = CheckState.Checked;
    this.checkBox_Usl_Bases_Sbor_isInputDoc.Location = new Point(18, 24);
    this.checkBox_Usl_Bases_Sbor_isInputDoc.Name = "checkBox_Usl_Bases_Sbor_isInputDoc";
    this.checkBox_Usl_Bases_Sbor_isInputDoc.Size = new Size(85, 17);
    this.checkBox_Usl_Bases_Sbor_isInputDoc.TabIndex = 0;
    this.checkBox_Usl_Bases_Sbor_isInputDoc.Text = "Документы";
    this.toolTip1.SetToolTip((Control) this.checkBox_Usl_Bases_Sbor_isInputDoc, "При выполнении команды \"Добавить существующий объект\" давать доступ к Документам");
    this.checkBox_Usl_Bases_Sbor_isInputDoc.UseVisualStyleBackColor = true;
    this.checkBox_Usl_Bases_Sbor_isInputDoc.CheckedChanged += new EventHandler(this.checkBox_Usl_Bases_Sbor_isInputDoc_CheckedChanged);
    this.checkBox_Usl_Bases_Sbor_isInputDoc.HelpRequested += new HelpEventHandler(this.tabPage_Bases_HelpRequested);
    this.groupBox_Xml_Text.BackColor = Color.Transparent;
    this.groupBox_Xml_Text.Controls.Add((Control) this.textBox_Xml_Text);
    this.groupBox_Xml_Text.Location = new Point(331, 593);
    this.groupBox_Xml_Text.Name = "groupBox_Xml_Text";
    this.groupBox_Xml_Text.Size = new Size(250, 44);
    this.groupBox_Xml_Text.TabIndex = 34;
    this.groupBox_Xml_Text.TabStop = false;
    this.groupBox_Xml_Text.Text = "Имя Xml";
    this.toolTip1.SetToolTip((Control) this.groupBox_Xml_Text, "Имя поля в файде Xml");
    this.textBox_Xml_Text.Location = new Point(6, 14);
    this.textBox_Xml_Text.Name = "textBox_Xml_Text";
    this.textBox_Xml_Text.Size = new Size(238, 20);
    this.textBox_Xml_Text.TabIndex = 0;
    this.toolTip1.SetToolTip((Control) this.textBox_Xml_Text, "Имя поля в файде Xml");
    this.textBox_Xml_Text.TextChanged += new EventHandler(this.textBox_Xml_Text_TextChanged);
    this.textBox_Xml_Text.KeyDown += new KeyEventHandler(this.textBox_Xml_Text_KeyDown);
    this.groupBox_Xml_EmptyString.Controls.Add((Control) this.label_Xml_AfterRemark);
    this.groupBox_Xml_EmptyString.Controls.Add((Control) this.numeric_UpDown_Xml_AfterRemark);
    this.groupBox_Xml_EmptyString.Controls.Add((Control) this.label_Xml_AfterInfo);
    this.groupBox_Xml_EmptyString.Controls.Add((Control) this.numeric_UpDown_Xml_AfterInfo);
    this.groupBox_Xml_EmptyString.Location = new Point(612, 677);
    this.groupBox_Xml_EmptyString.Name = "groupBox_Xml_EmptyString";
    this.groupBox_Xml_EmptyString.Size = new Size(275, 80 /*0x50*/);
    this.groupBox_Xml_EmptyString.TabIndex = 38;
    this.groupBox_Xml_EmptyString.TabStop = false;
    this.groupBox_Xml_EmptyString.Text = "Пропуск строк при заполнении из Xml";
    this.toolTip1.SetToolTip((Control) this.groupBox_Xml_EmptyString, "Настроить правила вставки пустых строк");
    this.label_Xml_AfterRemark.AutoSize = true;
    this.label_Xml_AfterRemark.Location = new Point(59, 52);
    this.label_Xml_AfterRemark.Name = "label_Xml_AfterRemark";
    this.label_Xml_AfterRemark.Size = new Size(103, 13);
    this.label_Xml_AfterRemark.TabIndex = 11;
    this.label_Xml_AfterRemark.Text = "После примечания";
    this.toolTip1.SetToolTip((Control) this.label_Xml_AfterRemark, "Количество пустых строк между записями примечаний");
    this.numeric_UpDown_Xml_AfterRemark.Location = new Point(11, 50);
    this.numeric_UpDown_Xml_AfterRemark.Maximum = new Decimal(new int[4]
    {
      5,
      0,
      0,
      0
    });
    this.numeric_UpDown_Xml_AfterRemark.Name = "numeric_UpDown_Xml_AfterRemark";
    this.numeric_UpDown_Xml_AfterRemark.Size = new Size(38, 20);
    this.numeric_UpDown_Xml_AfterRemark.TabIndex = 10;
    this.toolTip1.SetToolTip((Control) this.numeric_UpDown_Xml_AfterRemark, "Количество пустых строк между записями примечаний");
    this.numeric_UpDown_Xml_AfterRemark.ValueChanged += new EventHandler(this.numeric_UpDown_Xml_AfterRemark_ValueChanged);
    this.label_Xml_AfterInfo.AutoSize = true;
    this.label_Xml_AfterInfo.Location = new Point(57, 22);
    this.label_Xml_AfterInfo.Name = "label_Xml_AfterInfo";
    this.label_Xml_AfterInfo.Size = new Size(169, 13);
    this.label_Xml_AfterInfo.TabIndex = 3;
    this.label_Xml_AfterInfo.Text = "После информационной записи";
    this.toolTip1.SetToolTip((Control) this.label_Xml_AfterInfo, "Количество пустых строк между иформационными записями");
    this.numeric_UpDown_Xml_AfterInfo.ForeColor = SystemColors.WindowFrame;
    this.numeric_UpDown_Xml_AfterInfo.Location = new Point(9, 20);
    this.numeric_UpDown_Xml_AfterInfo.Maximum = new Decimal(new int[4]
    {
      5,
      0,
      0,
      0
    });
    this.numeric_UpDown_Xml_AfterInfo.Name = "numeric_UpDown_Xml_AfterInfo";
    this.numeric_UpDown_Xml_AfterInfo.Size = new Size(38, 20);
    this.numeric_UpDown_Xml_AfterInfo.TabIndex = 2;
    this.toolTip1.SetToolTip((Control) this.numeric_UpDown_Xml_AfterInfo, "Количество пустых строк между иформационными записями");
    this.numeric_UpDown_Xml_AfterInfo.Value = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this.numeric_UpDown_Xml_AfterInfo.ValueChanged += new EventHandler(this.numeric_UpDown_Xml_AfterInfo_ValueChanged);
    this.groupBox_Xml_Out.BackColor = Color.Transparent;
    this.groupBox_Xml_Out.Controls.Add((Control) this.radioButton_Xml_PassportOutNo);
    this.groupBox_Xml_Out.Controls.Add((Control) this.radioButton_Xml_PassportOutDialog);
    this.groupBox_Xml_Out.Controls.Add((Control) this.radioButton_Xml_PassporOutAlways);
    this.groupBox_Xml_Out.Location = new Point(897, 593);
    this.groupBox_Xml_Out.Name = "groupBox_Xml_Out";
    this.groupBox_Xml_Out.Size = new Size(320, 80 /*0x50*/);
    this.groupBox_Xml_Out.TabIndex = 37;
    this.groupBox_Xml_Out.TabStop = false;
    this.groupBox_Xml_Out.Text = "Вывод основной надписи в Xml";
    this.toolTip1.SetToolTip((Control) this.groupBox_Xml_Out, "Порядок dsdjlf основной надписи в файл Xml");
    this.radioButton_Xml_PassportOutNo.AutoSize = true;
    this.radioButton_Xml_PassportOutNo.Location = new Point(14, 56);
    this.radioButton_Xml_PassportOutNo.Name = "radioButton_Xml_PassportOutNo";
    this.radioButton_Xml_PassportOutNo.Size = new Size(91, 17);
    this.radioButton_Xml_PassportOutNo.TabIndex = 2;
    this.radioButton_Xml_PassportOutNo.Text = "Не выводить";
    this.toolTip1.SetToolTip((Control) this.radioButton_Xml_PassportOutNo, "Данные основной надписи не выводить");
    this.radioButton_Xml_PassportOutNo.UseVisualStyleBackColor = true;
    this.radioButton_Xml_PassportOutNo.MouseClick += new MouseEventHandler(this.radioButton_Xml_PassporOut);
    this.radioButton_Xml_PassportOutDialog.AutoSize = true;
    this.radioButton_Xml_PassportOutDialog.Location = new Point(14, 36);
    this.radioButton_Xml_PassportOutDialog.Name = "radioButton_Xml_PassportOutDialog";
    this.radioButton_Xml_PassportOutDialog.Size = new Size(76, 17);
    this.radioButton_Xml_PassportOutDialog.TabIndex = 1;
    this.radioButton_Xml_PassportOutDialog.Text = "В диалоге";
    this.toolTip1.SetToolTip((Control) this.radioButton_Xml_PassportOutDialog, "При выводе задавать вопрос \"Выводить данные основной надписи?\"");
    this.radioButton_Xml_PassportOutDialog.UseVisualStyleBackColor = true;
    this.radioButton_Xml_PassportOutDialog.MouseClick += new MouseEventHandler(this.radioButton_Xml_PassporOut);
    this.radioButton_Xml_PassporOutAlways.AutoSize = true;
    this.radioButton_Xml_PassporOutAlways.Checked = true;
    this.radioButton_Xml_PassporOutAlways.Location = new Point(14, 16 /*0x10*/);
    this.radioButton_Xml_PassporOutAlways.Name = "radioButton_Xml_PassporOutAlways";
    this.radioButton_Xml_PassporOutAlways.Size = new Size(113, 17);
    this.radioButton_Xml_PassporOutAlways.TabIndex = 0;
    this.radioButton_Xml_PassporOutAlways.TabStop = true;
    this.radioButton_Xml_PassporOutAlways.Text = "Выводить всегда";
    this.toolTip1.SetToolTip((Control) this.radioButton_Xml_PassporOutAlways, "Данные основной надписи выводить всегда");
    this.radioButton_Xml_PassporOutAlways.UseVisualStyleBackColor = true;
    this.radioButton_Xml_PassporOutAlways.MouseClick += new MouseEventHandler(this.radioButton_Xml_PassporOut);
    this.groupBox_Xml_In.BackColor = Color.Transparent;
    this.groupBox_Xml_In.Controls.Add((Control) this.radioButton_Xml_PassportInNo);
    this.groupBox_Xml_In.Controls.Add((Control) this.radioButton_Xml_PassportInDialog);
    this.groupBox_Xml_In.Controls.Add((Control) this.radioButton_Xml_PassporInAlways);
    this.groupBox_Xml_In.Location = new Point(612, 593);
    this.groupBox_Xml_In.Name = "groupBox_Xml_In";
    this.groupBox_Xml_In.Size = new Size(275, 80 /*0x50*/);
    this.groupBox_Xml_In.TabIndex = 36;
    this.groupBox_Xml_In.TabStop = false;
    this.groupBox_Xml_In.Text = "Ввод основной надписи из Xml";
    this.toolTip1.SetToolTip((Control) this.groupBox_Xml_In, "Порядок заполнения основной надписи из файла Xml");
    this.radioButton_Xml_PassportInNo.AutoSize = true;
    this.radioButton_Xml_PassportInNo.Location = new Point(14, 56);
    this.radioButton_Xml_PassportInNo.Name = "radioButton_Xml_PassportInNo";
    this.radioButton_Xml_PassportInNo.Size = new Size(75, 17);
    this.radioButton_Xml_PassportInNo.TabIndex = 5;
    this.radioButton_Xml_PassportInNo.Text = "Не читать";
    this.toolTip1.SetToolTip((Control) this.radioButton_Xml_PassportInNo, "Основную надпись из файла Xml не читать");
    this.radioButton_Xml_PassportInNo.UseVisualStyleBackColor = true;
    this.radioButton_Xml_PassportInNo.MouseClick += new MouseEventHandler(this.radioButton_Xml_PassporIn);
    this.radioButton_Xml_PassportInDialog.AutoSize = true;
    this.radioButton_Xml_PassportInDialog.Location = new Point(14, 36);
    this.radioButton_Xml_PassportInDialog.Name = "radioButton_Xml_PassportInDialog";
    this.radioButton_Xml_PassportInDialog.Size = new Size(76, 17);
    this.radioButton_Xml_PassportInDialog.TabIndex = 4;
    this.radioButton_Xml_PassportInDialog.Text = "В диалоге";
    this.toolTip1.SetToolTip((Control) this.radioButton_Xml_PassportInDialog, "При чтении задавать вопрос \"Читать данные основной надписи?\"");
    this.radioButton_Xml_PassportInDialog.UseVisualStyleBackColor = true;
    this.radioButton_Xml_PassportInDialog.MouseClick += new MouseEventHandler(this.radioButton_Xml_PassporIn);
    this.radioButton_Xml_PassporInAlways.AutoSize = true;
    this.radioButton_Xml_PassporInAlways.Checked = true;
    this.radioButton_Xml_PassporInAlways.Location = new Point(14, 16 /*0x10*/);
    this.radioButton_Xml_PassporInAlways.Name = "radioButton_Xml_PassporInAlways";
    this.radioButton_Xml_PassporInAlways.Size = new Size(99, 17);
    this.radioButton_Xml_PassporInAlways.TabIndex = 3;
    this.radioButton_Xml_PassporInAlways.TabStop = true;
    this.radioButton_Xml_PassporInAlways.Text = "Читать всегда";
    this.toolTip1.SetToolTip((Control) this.radioButton_Xml_PassporInAlways, "Основную надпись читать из файла Xml всегда");
    this.radioButton_Xml_PassporInAlways.UseVisualStyleBackColor = true;
    this.radioButton_Xml_PassporInAlways.MouseClick += new MouseEventHandler(this.radioButton_Xml_PassporIn);
    this.button_Vyvod_Delete.Enabled = false;
    this.button_Vyvod_Delete.Image = (Image) componentResourceManager.GetObject("button_Vyvod_Delete.Image");
    this.button_Vyvod_Delete.ImageAlign = ContentAlignment.MiddleRight;
    this.button_Vyvod_Delete.Location = new Point(11, 560);
    this.button_Vyvod_Delete.Name = "button_Vyvod_Delete";
    this.button_Vyvod_Delete.Size = new Size(202, 27);
    this.button_Vyvod_Delete.TabIndex = 15;
    this.button_Vyvod_Delete.Text = "Удалить";
    this.toolTip1.SetToolTip((Control) this.button_Vyvod_Delete, "Удалить текущее условие");
    this.button_Vyvod_Delete.UseVisualStyleBackColor = true;
    this.button_Vyvod_Delete.Click += new EventHandler(this.button_Vyvod_Delete_Click);
    this.button_Vyvod_Edit.Enabled = false;
    this.button_Vyvod_Edit.Image = (Image) componentResourceManager.GetObject("button_Vyvod_Edit.Image");
    this.button_Vyvod_Edit.ImageAlign = ContentAlignment.MiddleRight;
    this.button_Vyvod_Edit.Location = new Point(11, 526);
    this.button_Vyvod_Edit.Name = "button_Vyvod_Edit";
    this.button_Vyvod_Edit.Size = new Size(202, 27);
    this.button_Vyvod_Edit.TabIndex = 14;
    this.button_Vyvod_Edit.Text = "Изменить";
    this.toolTip1.SetToolTip((Control) this.button_Vyvod_Edit, "Изменить текущее условие согласно выбранным параметрам");
    this.button_Vyvod_Edit.UseVisualStyleBackColor = true;
    this.button_Vyvod_Edit.Click += new EventHandler(this.button_Vyvod_Edit_Click);
    this.button_Vyvod_AddCell.AccessibleRole = AccessibleRole.OutlineButton;
    this.button_Vyvod_AddCell.Enabled = false;
    this.button_Vyvod_AddCell.Image = (Image) componentResourceManager.GetObject("button_Vyvod_AddCell.Image");
    this.button_Vyvod_AddCell.ImageAlign = ContentAlignment.MiddleRight;
    this.button_Vyvod_AddCell.Location = new Point(11, 450);
    this.button_Vyvod_AddCell.Name = "button_Vyvod_AddCell";
    this.button_Vyvod_AddCell.Size = new Size(202, 27);
    this.button_Vyvod_AddCell.TabIndex = 13;
    this.button_Vyvod_AddCell.Text = "Добавить ячейку";
    this.toolTip1.SetToolTip((Control) this.button_Vyvod_AddCell, "Добавить ячейку");
    this.button_Vyvod_AddCell.UseVisualStyleBackColor = true;
    this.button_Vyvod_AddCell.Click += new EventHandler(this.button_Vyvod_AddCell_Click);
    this.button_Xml_Delete.Image = (Image) componentResourceManager.GetObject("button_Xml_Delete.Image");
    this.button_Xml_Delete.ImageAlign = ContentAlignment.MiddleRight;
    this.button_Xml_Delete.Location = new Point(123, 685);
    this.button_Xml_Delete.Name = "button_Xml_Delete";
    this.button_Xml_Delete.Size = new Size(202, 27);
    this.button_Xml_Delete.TabIndex = 33;
    this.button_Xml_Delete.Text = "Удалить";
    this.toolTip1.SetToolTip((Control) this.button_Xml_Delete, "Удалить текущую строку");
    this.button_Xml_Delete.UseVisualStyleBackColor = true;
    this.button_Xml_Delete.Click += new EventHandler(this.button_Xml_Delete_Click);
    this.button_Xml_Edit.Image = (Image) componentResourceManager.GetObject("button_Xml_Edit.Image");
    this.button_Xml_Edit.ImageAlign = ContentAlignment.MiddleRight;
    this.button_Xml_Edit.Location = new Point(123, 603);
    this.button_Xml_Edit.Name = "button_Xml_Edit";
    this.button_Xml_Edit.Size = new Size(202, 27);
    this.button_Xml_Edit.TabIndex = 32 /*0x20*/;
    this.button_Xml_Edit.Text = "Изменить";
    this.toolTip1.SetToolTip((Control) this.button_Xml_Edit, "Изменить текущую строку");
    this.button_Xml_Edit.UseVisualStyleBackColor = true;
    this.button_Xml_Edit.Click += new EventHandler(this.button_Xml_Edit_Click);
    this.button_Xml_Add.AccessibleRole = AccessibleRole.OutlineButton;
    this.button_Xml_Add.Image = (Image) componentResourceManager.GetObject("button_Xml_Add.Image");
    this.button_Xml_Add.ImageAlign = ContentAlignment.MiddleRight;
    this.button_Xml_Add.Location = new Point(123, 643);
    this.button_Xml_Add.Name = "button_Xml_Add";
    this.button_Xml_Add.Size = new Size(202, 27);
    this.button_Xml_Add.TabIndex = 31 /*0x1F*/;
    this.button_Xml_Add.Text = "Добавить";
    this.toolTip1.SetToolTip((Control) this.button_Xml_Add, "Добавить");
    this.button_Xml_Add.UseVisualStyleBackColor = true;
    this.button_Xml_Add.Click += new EventHandler(this.button_Xml_Add_Click);
    this.groupBox_Xml_Folder_In.BackColor = Color.Transparent;
    this.groupBox_Xml_Folder_In.Controls.Add((Control) this.button_Xml_Folder_In);
    this.groupBox_Xml_Folder_In.Controls.Add((Control) this.textBox_Xml_Folder_In);
    this.groupBox_Xml_Folder_In.Location = new Point(897, 679);
    this.groupBox_Xml_Folder_In.Name = "groupBox_Xml_Folder_In";
    this.groupBox_Xml_Folder_In.Size = new Size(320, 44);
    this.groupBox_Xml_Folder_In.TabIndex = 39;
    this.groupBox_Xml_Folder_In.TabStop = false;
    this.groupBox_Xml_Folder_In.Text = "Папка файлов Xml";
    this.toolTip1.SetToolTip((Control) this.groupBox_Xml_Folder_In, "Папка файлов Xml");
    this.button_Xml_Folder_In.Image = (Image) Resources.Folder;
    this.button_Xml_Folder_In.Location = new Point(267, 11);
    this.button_Xml_Folder_In.Name = "button_Xml_Folder_In";
    this.button_Xml_Folder_In.Size = new Size(44, 25);
    this.button_Xml_Folder_In.TabIndex = 38;
    this.toolTip1.SetToolTip((Control) this.button_Xml_Folder_In, "Выбор папки файлов Xml");
    this.button_Xml_Folder_In.UseVisualStyleBackColor = true;
    this.button_Xml_Folder_In.Click += new EventHandler(this.button_Xml_Folder_In_Click);
    this.textBox_Xml_Folder_In.Location = new Point(6, 14);
    this.textBox_Xml_Folder_In.Name = "textBox_Xml_Folder_In";
    this.textBox_Xml_Folder_In.Size = new Size((int) byte.MaxValue, 20);
    this.textBox_Xml_Folder_In.TabIndex = 0;
    this.toolTip1.SetToolTip((Control) this.textBox_Xml_Folder_In, "Папка файлов Xml");
    this.textBox_Xml_Folder_In.Leave += new EventHandler(this.textBox_Xml_Folder_In_Leave);
    this.groupBox_Vyvod_Additional.Controls.Add((Control) this.checkBox_Vyvod_Additional4);
    this.groupBox_Vyvod_Additional.Controls.Add((Control) this.checkBox_Vyvod_Additional3);
    this.groupBox_Vyvod_Additional.Controls.Add((Control) this.checkBox_Vyvod_Additional2);
    this.groupBox_Vyvod_Additional.Controls.Add((Control) this.checkBox_Vyvod_Additional1);
    this.groupBox_Vyvod_Additional.Location = new Point(11, 593);
    this.groupBox_Vyvod_Additional.Name = "groupBox_Vyvod_Additional";
    this.groupBox_Vyvod_Additional.Size = new Size(313, 102);
    this.groupBox_Vyvod_Additional.TabIndex = 27;
    this.groupBox_Vyvod_Additional.TabStop = false;
    this.groupBox_Vyvod_Additional.Text = "Дополнительные записи";
    this.toolTip1.SetToolTip((Control) this.groupBox_Vyvod_Additional, "Использлвать ли в редакторе \"Дополнительные\" записи");
    this.checkBox_Vyvod_Additional4.AutoSize = true;
    this.checkBox_Vyvod_Additional4.Location = new Point(10, 80 /*0x50*/);
    this.checkBox_Vyvod_Additional4.Name = "checkBox_Vyvod_Additional4";
    this.checkBox_Vyvod_Additional4.Size = new Size(121, 17);
    this.checkBox_Vyvod_Additional4.TabIndex = 3;
    this.checkBox_Vyvod_Additional4.Text = "Дополнительная 4";
    this.checkBox_Vyvod_Additional4.UseVisualStyleBackColor = true;
    this.checkBox_Vyvod_Additional4.CheckedChanged += new EventHandler(this.checkBox_Vyvod_Additional4_CheckedChanged);
    this.checkBox_Vyvod_Additional3.AutoSize = true;
    this.checkBox_Vyvod_Additional3.Location = new Point(10, 60);
    this.checkBox_Vyvod_Additional3.Name = "checkBox_Vyvod_Additional3";
    this.checkBox_Vyvod_Additional3.Size = new Size(121, 17);
    this.checkBox_Vyvod_Additional3.TabIndex = 2;
    this.checkBox_Vyvod_Additional3.Text = "Дополнительная 3";
    this.checkBox_Vyvod_Additional3.UseVisualStyleBackColor = true;
    this.checkBox_Vyvod_Additional3.CheckedChanged += new EventHandler(this.checkBox_Vyvod_Additional3_CheckedChanged);
    this.checkBox_Vyvod_Additional2.AutoSize = true;
    this.checkBox_Vyvod_Additional2.Location = new Point(10, 40);
    this.checkBox_Vyvod_Additional2.Name = "checkBox_Vyvod_Additional2";
    this.checkBox_Vyvod_Additional2.Size = new Size(121, 17);
    this.checkBox_Vyvod_Additional2.TabIndex = 1;
    this.checkBox_Vyvod_Additional2.Text = "Дополнительная 2";
    this.checkBox_Vyvod_Additional2.UseVisualStyleBackColor = true;
    this.checkBox_Vyvod_Additional2.CheckedChanged += new EventHandler(this.checkBox_Vyvod_Additional2_CheckedChanged);
    this.checkBox_Vyvod_Additional1.AutoSize = true;
    this.checkBox_Vyvod_Additional1.Location = new Point(10, 20);
    this.checkBox_Vyvod_Additional1.Name = "checkBox_Vyvod_Additional1";
    this.checkBox_Vyvod_Additional1.Size = new Size(121, 17);
    this.checkBox_Vyvod_Additional1.TabIndex = 0;
    this.checkBox_Vyvod_Additional1.Text = "Дополнительная 1";
    this.checkBox_Vyvod_Additional1.UseVisualStyleBackColor = true;
    this.checkBox_Vyvod_Additional1.CheckedChanged += new EventHandler(this.checkBox_Vyvod_Additional1_CheckedChanged);
    this.button_Avs_AddAttribut.AccessibleRole = AccessibleRole.OutlineButton;
    this.button_Avs_AddAttribut.Enabled = false;
    this.button_Avs_AddAttribut.ImageAlign = ContentAlignment.MiddleRight;
    this.button_Avs_AddAttribut.Location = new Point(11, 643);
    this.button_Avs_AddAttribut.Name = "button_Avs_AddAttribut";
    this.button_Avs_AddAttribut.Size = new Size(202, 27);
    this.button_Avs_AddAttribut.TabIndex = 16 /*0x10*/;
    this.button_Avs_AddAttribut.Text = "Добавить поле Avs";
    this.toolTip1.SetToolTip((Control) this.button_Avs_AddAttribut, "Добавить поле Avs");
    this.button_Avs_AddAttribut.UseVisualStyleBackColor = true;
    this.button_Avs_AddAttribut.Click += new EventHandler(this.button_Avs_AddAttribut_Click);
    this.button_Avs_Delete.Enabled = false;
    this.button_Avs_Delete.ImageAlign = ContentAlignment.MiddleRight;
    this.button_Avs_Delete.Location = new Point(11, 715);
    this.button_Avs_Delete.Name = "button_Avs_Delete";
    this.button_Avs_Delete.Size = new Size(202, 27);
    this.button_Avs_Delete.TabIndex = 15;
    this.button_Avs_Delete.Text = "Удалить";
    this.toolTip1.SetToolTip((Control) this.button_Avs_Delete, "Удалить текущее условие");
    this.button_Avs_Delete.UseVisualStyleBackColor = true;
    this.button_Avs_Delete.Click += new EventHandler(this.button_Avs_Delete_Click);
    this.button_Avs_Edit.Enabled = false;
    this.button_Avs_Edit.ImageAlign = ContentAlignment.MiddleRight;
    this.button_Avs_Edit.Location = new Point(11, 681);
    this.button_Avs_Edit.Name = "button_Avs_Edit";
    this.button_Avs_Edit.Size = new Size(202, 27);
    this.button_Avs_Edit.TabIndex = 14;
    this.button_Avs_Edit.Text = "Изменить";
    this.toolTip1.SetToolTip((Control) this.button_Avs_Edit, "Изменить текущее условие согласно выбранным параметрам");
    this.button_Avs_Edit.UseVisualStyleBackColor = true;
    this.button_Avs_Edit.Click += new EventHandler(this.button_Avs_Edit_Click);
    this.button_Avs_AddCell.AccessibleRole = AccessibleRole.OutlineButton;
    this.button_Avs_AddCell.Enabled = false;
    this.button_Avs_AddCell.ImageAlign = ContentAlignment.MiddleRight;
    this.button_Avs_AddCell.Location = new Point(11, 605);
    this.button_Avs_AddCell.Name = "button_Avs_AddCell";
    this.button_Avs_AddCell.Size = new Size(202, 27);
    this.button_Avs_AddCell.TabIndex = 13;
    this.button_Avs_AddCell.Text = "Добавить ячейку";
    this.toolTip1.SetToolTip((Control) this.button_Avs_AddCell, "Добавить ячейку");
    this.button_Avs_AddCell.UseVisualStyleBackColor = true;
    this.button_Avs_AddCell.Click += new EventHandler(this.button_Avs_AddCell_Click);
    this.groupBox_Avs_TextRazdelitel.Controls.Add((Control) this.comboBox_Avs_TextRazdelitel);
    this.groupBox_Avs_TextRazdelitel.Location = new Point(11, 557);
    this.groupBox_Avs_TextRazdelitel.Name = "groupBox_Avs_TextRazdelitel";
    this.groupBox_Avs_TextRazdelitel.Size = new Size(202, 41);
    this.groupBox_Avs_TextRazdelitel.TabIndex = 12;
    this.groupBox_Avs_TextRazdelitel.TabStop = false;
    this.groupBox_Avs_TextRazdelitel.Text = "Текст разделитель";
    this.toolTip1.SetToolTip((Control) this.groupBox_Avs_TextRazdelitel, "Текст (символ) разделитель, отделяющий отделяющий от предыдущего текста");
    this.comboBox_Avs_TextRazdelitel.Dock = DockStyle.Fill;
    this.comboBox_Avs_TextRazdelitel.FormattingEnabled = true;
    this.comboBox_Avs_TextRazdelitel.Items.AddRange(new object[9]
    {
      (object) "  (пробел)",
      (object) "(неразрывный пробел)",
      (object) "(без пробела)",
      (object) "(принудительный перенос)",
      (object) ". (точка)",
      (object) ", (запятая)",
      (object) "* (звездочка)",
      (object) "- (минус)",
      (object) "(неразрывный дефис)"
    });
    this.comboBox_Avs_TextRazdelitel.Location = new Point(3, 16 /*0x10*/);
    this.comboBox_Avs_TextRazdelitel.Name = "comboBox_Avs_TextRazdelitel";
    this.comboBox_Avs_TextRazdelitel.Size = new Size(196, 21);
    this.comboBox_Avs_TextRazdelitel.TabIndex = 3;
    this.groupBox_Vyvod_List_Ved_Id.Controls.Add((Control) this.listBox_Vyvod_List_Ved_Id);
    this.groupBox_Vyvod_List_Ved_Id.Location = new Point(0, 7);
    this.groupBox_Vyvod_List_Ved_Id.Name = "groupBox_Vyvod_List_Ved_Id";
    this.groupBox_Vyvod_List_Ved_Id.Size = new Size(252, 750);
    this.groupBox_Vyvod_List_Ved_Id.TabIndex = 37;
    this.groupBox_Vyvod_List_Ved_Id.TabStop = false;
    this.groupBox_Vyvod_List_Ved_Id.Text = "Атрибуты";
    this.toolTip1.SetToolTip((Control) this.groupBox_Vyvod_List_Ved_Id, "Атрибуты базы данных");
    this.listBox_Vyvod_List_Ved_Id.Dock = DockStyle.Fill;
    this.listBox_Vyvod_List_Ved_Id.FormattingEnabled = true;
    this.listBox_Vyvod_List_Ved_Id.Location = new Point(3, 16 /*0x10*/);
    this.listBox_Vyvod_List_Ved_Id.Name = "listBox_Vyvod_List_Ved_Id";
    this.listBox_Vyvod_List_Ved_Id.Size = new Size(246, 731);
    this.listBox_Vyvod_List_Ved_Id.TabIndex = 0;
    this.toolTip1.SetToolTip((Control) this.listBox_Vyvod_List_Ved_Id, "Атрибуты, собираемые из спецификаций");
    this.buttonEditTemplate.Location = new Point(11, 710);
    this.buttonEditTemplate.Name = "buttonEditTemplate";
    this.buttonEditTemplate.Size = new Size(202, 27);
    this.buttonEditTemplate.TabIndex = 38;
    this.buttonEditTemplate.Text = "Редактор шаблона";
    this.toolTip1.SetToolTip((Control) this.buttonEditTemplate, "Открыть окно редактирования шаблона (бланка)");
    this.buttonEditTemplate.UseVisualStyleBackColor = true;
    this.buttonEditTemplate.Click += new EventHandler(this.buttonEditTemplate_Click);
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
    this.panelForButtons.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
    this.panelForButtons.Controls.Add((Control) this.buttonWarnings);
    this.panelForButtons.Controls.Add((Control) this.buttonCopyFrom);
    this.panelForButtons.Controls.Add((Control) this.buttonSelectTabl);
    this.panelForButtons.Controls.Add((Control) this.buttonSave1);
    this.panelForButtons.Controls.Add((Control) this.buttonDefault);
    this.panelForButtons.Controls.Add((Control) this.bCancel);
    this.panelForButtons.Controls.Add((Control) this.bOK);
    this.panelForButtons.Dock = DockStyle.Bottom;
    this.panelForButtons.Location = new Point(0, 789);
    this.panelForButtons.Name = "panelForButtons";
    this.panelForButtons.Size = new Size(1584, 42);
    this.panelForButtons.TabIndex = 32 /*0x20*/;
    this.panelForButtons.MouseClick += new MouseEventHandler(this.panelForButtons_MouseClick);
    this.tabControl_Nastr.Controls.Add((Control) this.tabPage_Bases);
    this.tabControl_Nastr.Controls.Add((Control) this.tabPage_Sbor);
    this.tabControl_Nastr.Controls.Add((Control) this.tabPage_Vyvod);
    this.tabControl_Nastr.Controls.Add((Control) this.tabPage_Xml);
    this.tabControl_Nastr.Controls.Add((Control) this.tabPage_Avs6);
    this.tabControl_Nastr.Controls.Add((Control) this.tabPage_Service);
    this.tabControl_Nastr.Dock = DockStyle.Fill;
    this.tabControl_Nastr.Location = new Point(0, 0);
    this.tabControl_Nastr.Name = "tabControl_Nastr";
    this.tabControl_Nastr.SelectedIndex = 0;
    this.tabControl_Nastr.Size = new Size(1584, 789);
    this.tabControl_Nastr.TabIndex = 33;
    this.tabControl_Nastr.SelectedIndexChanged += new EventHandler(this.tabControl_Nastr_SelectedIndexChanged);
    this.tabPage_Bases.AutoScroll = true;
    this.tabPage_Bases.BackColor = Color.LightYellow;
    this.tabPage_Bases.Controls.Add((Control) this.groupBox_Usl_Bases_Sbor_Input);
    this.tabPage_Bases.Controls.Add((Control) this.groupBox_Usl_Bases_ImbaseCatalog);
    this.tabPage_Bases.Location = new Point(4, 22);
    this.tabPage_Bases.Name = "tabPage_Bases";
    this.tabPage_Bases.Padding = new Padding(3);
    this.tabPage_Bases.Size = new Size(1576, 763);
    this.tabPage_Bases.TabIndex = 0;
    this.tabPage_Bases.Text = "Основные";
    this.tabPage_Bases.HelpRequested += new HelpEventHandler(this.tabPage_Bases_HelpRequested);
    this.tabPage_Sbor.AutoScroll = true;
    this.tabPage_Sbor.BackColor = SystemColors.Control;
    this.tabPage_Sbor.Controls.Add((Control) this.button_Sbor_Peredatha_Delete2);
    this.tabPage_Sbor.Controls.Add((Control) this.button_Sbor_Peredatha_Add2);
    this.tabPage_Sbor.Controls.Add((Control) this.groupBox_Sbor_Peredatha_AttributeControl1);
    this.tabPage_Sbor.Controls.Add((Control) this.groupBox_Sbor_Peredatha_ListId);
    this.tabPage_Sbor.Location = new Point(4, 22);
    this.tabPage_Sbor.Name = "tabPage_Sbor";
    this.tabPage_Sbor.Padding = new Padding(3);
    this.tabPage_Sbor.Size = new Size(1576, 763);
    this.tabPage_Sbor.TabIndex = 1;
    this.tabPage_Sbor.Text = "Передача данных в таблицу";
    this.button_Sbor_Peredatha_Delete2.Image = (Image) componentResourceManager.GetObject("button_Sbor_Peredatha_Delete2.Image");
    this.button_Sbor_Peredatha_Delete2.ImageAlign = ContentAlignment.MiddleRight;
    this.button_Sbor_Peredatha_Delete2.Location = new Point(559, 177);
    this.button_Sbor_Peredatha_Delete2.Name = "button_Sbor_Peredatha_Delete2";
    this.button_Sbor_Peredatha_Delete2.Size = new Size(121, 27);
    this.button_Sbor_Peredatha_Delete2.TabIndex = 17;
    this.button_Sbor_Peredatha_Delete2.Text = "Удалить";
    this.button_Sbor_Peredatha_Delete2.UseVisualStyleBackColor = true;
    this.button_Sbor_Peredatha_Delete2.Click += new EventHandler(this.button_Sbor_Peredatha_Delete2_Click);
    this.button_Sbor_Peredatha_Add2.AccessibleRole = AccessibleRole.OutlineButton;
    this.button_Sbor_Peredatha_Add2.Image = (Image) componentResourceManager.GetObject("button_Sbor_Peredatha_Add2.Image");
    this.button_Sbor_Peredatha_Add2.ImageAlign = ContentAlignment.MiddleRight;
    this.button_Sbor_Peredatha_Add2.Location = new Point(559, (int) sbyte.MaxValue);
    this.button_Sbor_Peredatha_Add2.Name = "button_Sbor_Peredatha_Add2";
    this.button_Sbor_Peredatha_Add2.Size = new Size(121, 27);
    this.button_Sbor_Peredatha_Add2.TabIndex = 16 /*0x10*/;
    this.button_Sbor_Peredatha_Add2.Text = "Добавить";
    this.button_Sbor_Peredatha_Add2.UseVisualStyleBackColor = true;
    this.button_Sbor_Peredatha_Add2.Click += new EventHandler(this.button_Sbor_Peredatha_Add2_Click);
    this.groupBox_Sbor_Peredatha_AttributeControl1.Controls.Add((Control) this.select_Sbor_Peredatha_AttributeControl2);
    this.groupBox_Sbor_Peredatha_AttributeControl1.Location = new Point(8, 6);
    this.groupBox_Sbor_Peredatha_AttributeControl1.Name = "groupBox_Sbor_Peredatha_AttributeControl1";
    this.groupBox_Sbor_Peredatha_AttributeControl1.Size = new Size(525, 720);
    this.groupBox_Sbor_Peredatha_AttributeControl1.TabIndex = 14;
    this.groupBox_Sbor_Peredatha_AttributeControl1.TabStop = false;
    this.groupBox_Sbor_Peredatha_AttributeControl1.Text = "Выбор атрибутов";
    this.select_Sbor_Peredatha_AttributeControl2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
    this.select_Sbor_Peredatha_AttributeControl2.Dock = DockStyle.Fill;
    this.select_Sbor_Peredatha_AttributeControl2.Font = new Font("Tahoma", 8.25f);
    this.select_Sbor_Peredatha_AttributeControl2.Location = new Point(3, 16 /*0x10*/);
    this.select_Sbor_Peredatha_AttributeControl2.Name = "select_Sbor_Peredatha_AttributeControl2";
    this.select_Sbor_Peredatha_AttributeControl2.Size = new Size(519, 701);
    this.select_Sbor_Peredatha_AttributeControl2.TabIndex = 1;
    this.select_Sbor_Peredatha_AttributeControl2.ViewType = ViewType.All;
    this.groupBox_Sbor_Peredatha_ListId.Controls.Add((Control) this.listBox_Sbor_Peredatha_ListId);
    this.groupBox_Sbor_Peredatha_ListId.Location = new Point(712, 6);
    this.groupBox_Sbor_Peredatha_ListId.Name = "groupBox_Sbor_Peredatha_ListId";
    this.groupBox_Sbor_Peredatha_ListId.Size = new Size(525, 720);
    this.groupBox_Sbor_Peredatha_ListId.TabIndex = 15;
    this.groupBox_Sbor_Peredatha_ListId.TabStop = false;
    this.groupBox_Sbor_Peredatha_ListId.Text = "Список передаваемых атрибутов";
    this.listBox_Sbor_Peredatha_ListId.Dock = DockStyle.Fill;
    this.listBox_Sbor_Peredatha_ListId.FormattingEnabled = true;
    this.listBox_Sbor_Peredatha_ListId.Location = new Point(3, 16 /*0x10*/);
    this.listBox_Sbor_Peredatha_ListId.Name = "listBox_Sbor_Peredatha_ListId";
    this.listBox_Sbor_Peredatha_ListId.Size = new Size(519, 701);
    this.listBox_Sbor_Peredatha_ListId.TabIndex = 1;
    this.listBox_Sbor_Peredatha_ListId.Click += new EventHandler(this.listBox_Sbor_Peredatha_ListId_Click);
    this.listBox_Sbor_Peredatha_ListId.KeyDown += new KeyEventHandler(this.listBox_Sbor_Peredatha_ListId_KeyDown);
    this.tabPage_Vyvod.AutoScroll = true;
    this.tabPage_Vyvod.BackColor = SystemColors.Control;
    this.tabPage_Vyvod.Controls.Add((Control) this.groupBox_Vyvod_List_Ved_Id);
    this.tabPage_Vyvod.Controls.Add((Control) this.docKcontainer_Vyvod);
    this.tabPage_Vyvod.Controls.Add((Control) this.panel_Vyvod_1);
    this.tabPage_Vyvod.Controls.Add((Control) this.docContainer_Vyvod);
    this.tabPage_Vyvod.Location = new Point(4, 22);
    this.tabPage_Vyvod.Name = "tabPage_Vyvod";
    this.tabPage_Vyvod.Padding = new Padding(3);
    this.tabPage_Vyvod.Size = new Size(1576, 763);
    this.tabPage_Vyvod.TabIndex = 2;
    this.tabPage_Vyvod.Text = "Вывод";
    this.docKcontainer_Vyvod.Dock = DockStyle.Right;
    this.docKcontainer_Vyvod.Guid = new Guid("6c63e3af-951f-4d98-b09e-be3ffba040c1");
    this.docKcontainer_Vyvod.LayoutSystem = new SplitLayoutSystem(250, 400);
    this.docKcontainer_Vyvod.Location = new Point(1573, 3);
    this.docKcontainer_Vyvod.Manager = this.dockMan_Vyvod;
    this.docKcontainer_Vyvod.Name = "docKcontainer_Vyvod";
    this.docKcontainer_Vyvod.Renderer = (RendererBase) null;
    this.docKcontainer_Vyvod.Size = new Size(0, 757);
    this.docKcontainer_Vyvod.TabIndex = 34;
    this.dockMan_Vyvod.DocumentContainer = this.docContainer_Vyvod;
    this.dockMan_Vyvod.OwnerForm = (Form) this;
    this.docContainer_Vyvod.Dock = DockStyle.None;
    this.docContainer_Vyvod.Guid = new Guid("adadfb01-16c4-4a32-8733-a11ec038a68c");
    this.docContainer_Vyvod.LayoutSystem = new SplitLayoutSystem(250, 400);
    this.docContainer_Vyvod.Location = new Point(612, 6);
    this.docContainer_Vyvod.Manager = (DockManager) null;
    this.docContainer_Vyvod.Name = "docContainer_Vyvod";
    this.docContainer_Vyvod.Renderer = (RendererBase) null;
    this.docContainer_Vyvod.Size = new Size(605, 751);
    this.docContainer_Vyvod.TabIndex = 32 /*0x20*/;
    this.panel_Vyvod_1.Controls.Add((Control) this.buttonEditTemplate);
    this.panel_Vyvod_1.Controls.Add((Control) this.groupBox_Vyvod_Additional);
    this.panel_Vyvod_1.Controls.Add((Control) this.button_Vyvod_AddAttribut);
    this.panel_Vyvod_1.Controls.Add((Control) this.button_Vyvod_Delete);
    this.panel_Vyvod_1.Controls.Add((Control) this.button_Vyvod_Edit);
    this.panel_Vyvod_1.Controls.Add((Control) this.button_Vyvod_AddCell);
    this.panel_Vyvod_1.Controls.Add((Control) this.groupBox_Vyvod_TextRazdelitel);
    this.panel_Vyvod_1.Controls.Add((Control) this.treeView_Vyvod);
    this.panel_Vyvod_1.Location = new Point(258, 7);
    this.panel_Vyvod_1.Name = "panel_Vyvod_1";
    this.panel_Vyvod_1.Size = new Size(348, 752);
    this.panel_Vyvod_1.TabIndex = 33;
    this.treeView_Vyvod.HideSelection = false;
    this.treeView_Vyvod.Location = new Point(6, 6);
    this.treeView_Vyvod.Name = "treeView_Vyvod";
    this.treeView_Vyvod.Size = new Size(342, 388);
    this.treeView_Vyvod.TabIndex = 11;
    this.treeView_Vyvod.AfterSelect += new TreeViewEventHandler(this.treeView_Vyvod_AfterSelect);
    this.tabPage_Xml.AutoScroll = true;
    this.tabPage_Xml.Controls.Add((Control) this.groupBox_Xml_Folder_In);
    this.tabPage_Xml.Controls.Add((Control) this.groupBox_Xml_EmptyString);
    this.tabPage_Xml.Controls.Add((Control) this.groupBox_Xml_Out);
    this.tabPage_Xml.Controls.Add((Control) this.groupBox_Xml_In);
    this.tabPage_Xml.Controls.Add((Control) this.groupBox_Xml_Text);
    this.tabPage_Xml.Controls.Add((Control) this.treeView_Xml);
    this.tabPage_Xml.Controls.Add((Control) this.docContainer_Xml);
    this.tabPage_Xml.Controls.Add((Control) this.docKcontainer_Xml);
    this.tabPage_Xml.Controls.Add((Control) this.button_Xml_Delete);
    this.tabPage_Xml.Controls.Add((Control) this.button_Xml_Edit);
    this.tabPage_Xml.Controls.Add((Control) this.button_Xml_Add);
    this.tabPage_Xml.Location = new Point(4, 22);
    this.tabPage_Xml.Name = "tabPage_Xml";
    this.tabPage_Xml.Padding = new Padding(3);
    this.tabPage_Xml.Size = new Size(1576, 763);
    this.tabPage_Xml.TabIndex = 4;
    this.tabPage_Xml.Text = "XML";
    this.tabPage_Xml.UseVisualStyleBackColor = true;
    this.treeView_Xml.Font = new Font("Courier New", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    this.treeView_Xml.HideSelection = false;
    this.treeView_Xml.Location = new Point(10, 6);
    this.treeView_Xml.Name = "treeView_Xml";
    this.treeView_Xml.Size = new Size(596, 581);
    this.treeView_Xml.TabIndex = 28;
    this.treeView_Xml.AfterSelect += new TreeViewEventHandler(this.treeView_Xml_AfterSelect);
    this.treeView_Xml.KeyDown += new KeyEventHandler(this.treeView_Xml_KeyDown);
    this.docContainer_Xml.Dock = DockStyle.None;
    this.docContainer_Xml.Guid = new Guid("adadfb01-16c4-4a32-8733-a11ec038a68c");
    this.docContainer_Xml.LayoutSystem = new SplitLayoutSystem(250, 400);
    this.docContainer_Xml.Location = new Point(612, 6);
    this.docContainer_Xml.Manager = (DockManager) null;
    this.docContainer_Xml.Name = "docContainer_Xml";
    this.docContainer_Xml.Renderer = (RendererBase) null;
    this.docContainer_Xml.Size = new Size(605, 581);
    this.docContainer_Xml.TabIndex = 27;
    this.docKcontainer_Xml.Dock = DockStyle.Right;
    this.docKcontainer_Xml.Guid = new Guid("6c63e3af-951f-4d98-b09e-be3ffba040c1");
    this.docKcontainer_Xml.LayoutSystem = new SplitLayoutSystem(250, 400);
    this.docKcontainer_Xml.Location = new Point(1573, 3);
    this.docKcontainer_Xml.Manager = this.dockMan_Xml;
    this.docKcontainer_Xml.Name = "docKcontainer_Xml";
    this.docKcontainer_Xml.Renderer = (RendererBase) null;
    this.docKcontainer_Xml.Size = new Size(0, 757);
    this.docKcontainer_Xml.TabIndex = 26;
    this.dockMan_Xml.DocumentContainer = this.docContainer_Xml;
    this.dockMan_Xml.OwnerForm = (Form) this;
    this.tabPage_Avs6.AutoScroll = true;
    this.tabPage_Avs6.Controls.Add((Control) this.panel_Avs_1);
    this.tabPage_Avs6.Controls.Add((Control) this.groupBox_Avs6_Fields);
    this.tabPage_Avs6.Controls.Add((Control) this.docContainer_Avs);
    this.tabPage_Avs6.Controls.Add((Control) this.dockcontainer_Avs);
    this.tabPage_Avs6.Location = new Point(4, 22);
    this.tabPage_Avs6.Name = "tabPage_Avs6";
    this.tabPage_Avs6.Padding = new Padding(3);
    this.tabPage_Avs6.Size = new Size(1576, 763);
    this.tabPage_Avs6.TabIndex = 5;
    this.tabPage_Avs6.Text = "Ввод документов AVS6";
    this.tabPage_Avs6.UseVisualStyleBackColor = true;
    this.panel_Avs_1.Controls.Add((Control) this.button_Avs_AddAttribut);
    this.panel_Avs_1.Controls.Add((Control) this.button_Avs_Delete);
    this.panel_Avs_1.Controls.Add((Control) this.button_Avs_Edit);
    this.panel_Avs_1.Controls.Add((Control) this.button_Avs_AddCell);
    this.panel_Avs_1.Controls.Add((Control) this.groupBox_Avs_TextRazdelitel);
    this.panel_Avs_1.Controls.Add((Control) this.treeView_Avs);
    this.panel_Avs_1.Location = new Point(266, 6);
    this.panel_Avs_1.Name = "panel_Avs_1";
    this.panel_Avs_1.Size = new Size(348, 751);
    this.panel_Avs_1.TabIndex = 20;
    this.treeView_Avs.HideSelection = false;
    this.treeView_Avs.Location = new Point(6, 6);
    this.treeView_Avs.Name = "treeView_Avs";
    this.treeView_Avs.Size = new Size(342, 545);
    this.treeView_Avs.TabIndex = 11;
    this.treeView_Avs.AfterSelect += new TreeViewEventHandler(this.treeView_Avs_AfterSelect);
    this.groupBox_Avs6_Fields.Controls.Add((Control) this.listBox_Avs6_Fields);
    this.groupBox_Avs6_Fields.Location = new Point(10, 6);
    this.groupBox_Avs6_Fields.Name = "groupBox_Avs6_Fields";
    this.groupBox_Avs6_Fields.Size = new Size(250, 751);
    this.groupBox_Avs6_Fields.TabIndex = 8;
    this.groupBox_Avs6_Fields.TabStop = false;
    this.groupBox_Avs6_Fields.Text = "Список полей записей AVS6";
    this.listBox_Avs6_Fields.Dock = DockStyle.Fill;
    this.listBox_Avs6_Fields.FormattingEnabled = true;
    this.listBox_Avs6_Fields.Location = new Point(3, 16 /*0x10*/);
    this.listBox_Avs6_Fields.Name = "listBox_Avs6_Fields";
    this.listBox_Avs6_Fields.Size = new Size(244, 732);
    this.listBox_Avs6_Fields.TabIndex = 0;
    this.docContainer_Avs.Dock = DockStyle.None;
    this.docContainer_Avs.Guid = new Guid("adadfb01-16c4-4a32-8733-a11ec038a68c");
    this.docContainer_Avs.LayoutSystem = new SplitLayoutSystem(250, 400);
    this.docContainer_Avs.Location = new Point(612, 6);
    this.docContainer_Avs.Manager = (DockManager) null;
    this.docContainer_Avs.Name = "docContainer_Avs";
    this.docContainer_Avs.Renderer = (RendererBase) null;
    this.docContainer_Avs.Size = new Size(605, 751);
    this.docContainer_Avs.TabIndex = 28;
    this.dockcontainer_Avs.Dock = DockStyle.Right;
    this.dockcontainer_Avs.Guid = new Guid("6c63e3af-951f-4d98-b09e-be3ffba040c1");
    this.dockcontainer_Avs.LayoutSystem = new SplitLayoutSystem(250, 400);
    this.dockcontainer_Avs.Location = new Point(1573, 3);
    this.dockcontainer_Avs.Manager = this.dockMan_Avs;
    this.dockcontainer_Avs.Name = "dockcontainer_Avs";
    this.dockcontainer_Avs.Renderer = (RendererBase) null;
    this.dockcontainer_Avs.Size = new Size(0, 757);
    this.dockcontainer_Avs.TabIndex = 34;
    this.dockMan_Avs.DocumentContainer = this.docContainer_Avs;
    this.dockMan_Avs.OwnerForm = (Form) this;
    this.tabPage_Service.AutoScroll = true;
    this.tabPage_Service.Controls.Add((Control) this.groupBox_AccessLevel);
    this.tabPage_Service.Controls.Add((Control) this.checkBox_Services_CreateDump);
    this.tabPage_Service.Controls.Add((Control) this.labelService2);
    this.tabPage_Service.Controls.Add((Control) this.labelService1);
    this.tabPage_Service.Controls.Add((Control) this.label_ServicesFileOpen);
    this.tabPage_Service.Controls.Add((Control) this.label_ServiceCreateDump);
    this.tabPage_Service.Controls.Add((Control) this.label_ServicesTypeVedTo);
    this.tabPage_Service.Controls.Add((Control) this.label_ServicesCopyAll);
    this.tabPage_Service.Controls.Add((Control) this.label_ServicesDefaultAll);
    this.tabPage_Service.Controls.Add((Control) this.buttonServicesFileOpen);
    this.tabPage_Service.Controls.Add((Control) this.buttonServiceCreateDump);
    this.tabPage_Service.Controls.Add((Control) this.buttonServicesTypeVedTo);
    this.tabPage_Service.Controls.Add((Control) this.buttonServicesCopyAll);
    this.tabPage_Service.Controls.Add((Control) this.buttonServicesDefaultAll);
    this.tabPage_Service.Controls.Add((Control) this.groupBox_Dump);
    this.tabPage_Service.Location = new Point(4, 22);
    this.tabPage_Service.Name = "tabPage_Service";
    this.tabPage_Service.Padding = new Padding(3);
    this.tabPage_Service.Size = new Size(1576, 763);
    this.tabPage_Service.TabIndex = 3;
    this.tabPage_Service.Text = "Сервис";
    this.tabPage_Service.UseVisualStyleBackColor = true;
    this.groupBox_AccessLevel.BackColor = Color.Transparent;
    this.groupBox_AccessLevel.Controls.Add((Control) this.radioButton_AccessLevel2);
    this.groupBox_AccessLevel.Controls.Add((Control) this.radioButton_AccessLevel1);
    this.groupBox_AccessLevel.Controls.Add((Control) this.radioButton_AccessLevel0);
    this.groupBox_AccessLevel.Location = new Point(21, 470);
    this.groupBox_AccessLevel.Name = "groupBox_AccessLevel";
    this.groupBox_AccessLevel.Size = new Size(502, 83);
    this.groupBox_AccessLevel.TabIndex = 40;
    this.groupBox_AccessLevel.TabStop = false;
    this.groupBox_AccessLevel.Text = "Уровень доступа к настройке";
    this.radioButton_AccessLevel2.AutoSize = true;
    this.radioButton_AccessLevel2.Checked = true;
    this.radioButton_AccessLevel2.Location = new Point(6, 54);
    this.radioButton_AccessLevel2.Name = "radioButton_AccessLevel2";
    this.radioButton_AccessLevel2.Size = new Size(44, 17);
    this.radioButton_AccessLevel2.TabIndex = 2;
    this.radioButton_AccessLevel2.TabStop = true;
    this.radioButton_AccessLevel2.Text = "Все";
    this.radioButton_AccessLevel2.UseVisualStyleBackColor = true;
    this.radioButton_AccessLevel2.MouseClick += new MouseEventHandler(this.radioButton_AccessLevel2_MouseClick);
    this.radioButton_AccessLevel1.AutoSize = true;
    this.radioButton_AccessLevel1.Location = new Point(6, 34);
    this.radioButton_AccessLevel1.Name = "radioButton_AccessLevel1";
    this.radioButton_AccessLevel1.Size = new Size(234, 17);
    this.radioButton_AccessLevel1.TabIndex = 1;
    this.radioButton_AccessLevel1.Text = "Пользователи с ролью \"Администратор\"";
    this.radioButton_AccessLevel1.UseVisualStyleBackColor = true;
    this.radioButton_AccessLevel1.MouseClick += new MouseEventHandler(this.radioButton_AccessLevel1_MouseClick);
    this.radioButton_AccessLevel0.AutoSize = true;
    this.radioButton_AccessLevel0.Location = new Point(6, 14);
    this.radioButton_AccessLevel0.Name = "radioButton_AccessLevel0";
    this.radioButton_AccessLevel0.Size = new Size(302, 17);
    this.radioButton_AccessLevel0.TabIndex = 0;
    this.radioButton_AccessLevel0.Text = "Пользователь с именем \"Системный администратор\"";
    this.radioButton_AccessLevel0.UseVisualStyleBackColor = true;
    this.radioButton_AccessLevel0.MouseClick += new MouseEventHandler(this.radioButton_AccessLevel0_MouseClick);
    this.checkBox_Services_CreateDump.AutoSize = true;
    this.checkBox_Services_CreateDump.Location = new Point(39, 405);
    this.checkBox_Services_CreateDump.Name = "checkBox_Services_CreateDump";
    this.checkBox_Services_CreateDump.Size = new Size(247, 17);
    this.checkBox_Services_CreateDump.TabIndex = 38;
    this.checkBox_Services_CreateDump.Text = "Создавать Dump в текущем сеансе работы";
    this.checkBox_Services_CreateDump.UseVisualStyleBackColor = true;
    this.checkBox_Services_CreateDump.CheckedChanged += new EventHandler(this.checkBox_Services_CreateDump_CheckedChanged);
    this.labelService2.AutoSize = true;
    this.labelService2.Location = new Point(17, 735);
    this.labelService2.Name = "labelService2";
    this.labelService2.Size = new Size(71, 13);
    this.labelService2.TabIndex = 37;
    this.labelService2.Text = "labelService2";
    this.labelService1.AutoSize = true;
    this.labelService1.Location = new Point(17, 707);
    this.labelService1.Name = "labelService1";
    this.labelService1.Size = new Size(71, 13);
    this.labelService1.TabIndex = 36;
    this.labelService1.Text = "labelService1";
    this.label_ServicesFileOpen.AutoSize = true;
    this.label_ServicesFileOpen.Location = new Point(250, 354);
    this.label_ServicesFileOpen.Name = "label_ServicesFileOpen";
    this.label_ServicesFileOpen.Size = new Size(257, 13);
    this.label_ServicesFileOpen.TabIndex = 35;
    this.label_ServicesFileOpen.Text = "Прочитать параметры настроек из файла (Dump)";
    this.label_ServicesFileOpen.Click += new EventHandler(this.label_ServicesFileOpen_Click);
    this.label_ServiceCreateDump.AutoSize = true;
    this.label_ServiceCreateDump.Location = new Point(250, 304);
    this.label_ServiceCreateDump.Name = "label_ServiceCreateDump";
    this.label_ServiceCreateDump.Size = new Size(256 /*0x0100*/, 13);
    this.label_ServiceCreateDump.TabIndex = 34;
    this.label_ServiceCreateDump.Text = "Создать Dump (Сохраняются настройки, шаблон)";
    this.label_ServicesTypeVedTo.Location = new Point(231, 131);
    this.label_ServicesTypeVedTo.Name = "label_ServicesTypeVedTo";
    this.label_ServicesTypeVedTo.Size = new Size(413, 36);
    this.label_ServicesTypeVedTo.TabIndex = 33;
    this.label_ServicesTypeVedTo.Text = "Текущему виду таблицы присвоить свойства определенной системной таблицы, например \"Таблица соединений\"";
    this.label_ServicesCopyAll.Location = new Point(231, 81);
    this.label_ServicesCopyAll.Name = "label_ServicesCopyAll";
    this.label_ServicesCopyAll.Size = new Size(413, 34);
    this.label_ServicesCopyAll.TabIndex = 32 /*0x20*/;
    this.label_ServicesCopyAll.Text = "Все значениям настройки для текущего типа таблицы копировать значения из другой таблицы";
    this.label_ServicesDefaultAll.Location = new Point(231, 31 /*0x1F*/);
    this.label_ServicesDefaultAll.Name = "label_ServicesDefaultAll";
    this.label_ServicesDefaultAll.Size = new Size(413, 32 /*0x20*/);
    this.label_ServicesDefaultAll.TabIndex = 31 /*0x1F*/;
    this.label_ServicesDefaultAll.Text = "Всем значениям настройки для текущего типа таблицы присвоить значения по умолчанию";
    this.groupBox_Dump.Location = new Point(24, 274);
    this.groupBox_Dump.Name = "groupBox_Dump";
    this.groupBox_Dump.Size = new Size(499, 170);
    this.groupBox_Dump.TabIndex = 39;
    this.groupBox_Dump.TabStop = false;
    this.groupBox_Dump.Text = "Dump";
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.AutoScroll = true;
    this.AutoSize = true;
    this.CancelButton = (IButtonControl) this.bCancel;
    this.ClientSize = new Size(1584, 831);
    this.Controls.Add((Control) this.tabControl_Nastr);
    this.Controls.Add((Control) this.panelForButtons);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (A_NastrTabl);
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Настройка таблицы:";
    this.FormClosing += new FormClosingEventHandler(this.A_NastrTabl_FormClosing);
    this.Load += new EventHandler(this.A_NastrTabl_Load);
    this.Shown += new EventHandler(this.A_NastrTabl_Shown);
    this.groupBox_Vyvod_TextRazdelitel.ResumeLayout(false);
    this.groupBox_Usl_Bases_ImbaseCatalog.ResumeLayout(false);
    this.groupBox_Usl_Bases_ImbaseCatalog.PerformLayout();
    this.groupBox_Usl_Bases_Sbor_Input.ResumeLayout(false);
    this.groupBox_Usl_Bases_Sbor_Input.PerformLayout();
    this.groupBox_Xml_Text.ResumeLayout(false);
    this.groupBox_Xml_Text.PerformLayout();
    this.groupBox_Xml_EmptyString.ResumeLayout(false);
    this.groupBox_Xml_EmptyString.PerformLayout();
    this.numeric_UpDown_Xml_AfterRemark.EndInit();
    this.numeric_UpDown_Xml_AfterInfo.EndInit();
    this.groupBox_Xml_Out.ResumeLayout(false);
    this.groupBox_Xml_Out.PerformLayout();
    this.groupBox_Xml_In.ResumeLayout(false);
    this.groupBox_Xml_In.PerformLayout();
    this.groupBox_Xml_Folder_In.ResumeLayout(false);
    this.groupBox_Xml_Folder_In.PerformLayout();
    this.groupBox_Vyvod_Additional.ResumeLayout(false);
    this.groupBox_Vyvod_Additional.PerformLayout();
    this.groupBox_Avs_TextRazdelitel.ResumeLayout(false);
    this.groupBox_Vyvod_List_Ved_Id.ResumeLayout(false);
    this.panelForButtons.ResumeLayout(false);
    this.tabControl_Nastr.ResumeLayout(false);
    this.tabPage_Bases.ResumeLayout(false);
    this.tabPage_Sbor.ResumeLayout(false);
    this.groupBox_Sbor_Peredatha_AttributeControl1.ResumeLayout(false);
    this.groupBox_Sbor_Peredatha_ListId.ResumeLayout(false);
    this.tabPage_Vyvod.ResumeLayout(false);
    this.panel_Vyvod_1.ResumeLayout(false);
    this.tabPage_Xml.ResumeLayout(false);
    this.tabPage_Avs6.ResumeLayout(false);
    this.panel_Avs_1.ResumeLayout(false);
    this.groupBox_Avs6_Fields.ResumeLayout(false);
    this.tabPage_Service.ResumeLayout(false);
    this.tabPage_Service.PerformLayout();
    this.groupBox_AccessLevel.ResumeLayout(false);
    this.groupBox_AccessLevel.PerformLayout();
    this.ResumeLayout(false);
  }

  public class OneVyvodNode : TreeNode
  {
    public Vedomost_VB.OneRecordToPrint _oneRecordToPrint;
    public Vedomost_VB.OneGrafaToPrint _oneGrafaToPrint;
    public Vedomost_VB.OneDataFieldToPrint _oneDataFieldToPrint;
    public A_NastrTabl.OneVyvodNode _oneVyvodNode_Parent;
    public Vedomost_VB_Static.TypeNode_Tree _typeNode;
    public int _iData = -1;
  }

  public class OneAvsNode : TreeNode
  {
    public Vedomost_VB.OneRecord_Avs6_To_Ips _oneRecord_Avs;
    public Vedomost_VB.OneGrafa_Avs6_To_Ips _oneGrafa_Avs;
    public Vedomost_VB.OneDataField_Avs6_To_Ips _oneDataField_Avs;
    public A_NastrTabl.OneAvsNode _oneAvsNode_Parent;
    public Vedomost_VB_Static.TypeNode_Tree _typeNode;
    public int _iData = -1;
  }
}
