// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Victor.VedomostEditorWindow
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Bars;
using Intermech.Document.Client;
using Intermech.Document.DBCore;
using Intermech.Document.Model;
using Intermech.Document.UI;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Document;
using Intermech.Navigator.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS.Victor;

public class VedomostEditorWindow : ImDocumentEditorForm
{
  public Vedomost_VB _vedomost_VB_new;
  private bool _isVveliOsnovnyeDannye;
  private Guid _guidTemplateDoc = Guid.Empty;
  private Guid _guidTypeDoc = Guid.Empty;
  public One_Ved_Nastr _one_Ved_Nastr_Curr;
  public ListError_OneError _listError = new ListError_OneError();
  private long _templateID = -1;
  private ImDocument _docTemplate;
  public ImDocument _document;
  public Vedomost_VB.TypeDoc _typeDoc;
  public string _formaGroup_Doc = "Ed";
  private Vedomost_VB.Variables_Coordination _variables_Coordination = new Vedomost_VB.Variables_Coordination();
  private string _designationArticle = "";
  private string _nameArticle = "";
  private string _designationDoc = "";
  private string _nameTypeDoc = "";
  private Vedomost_VB_Static.TypeRow _typeRowCurr;
  private TableData _docRowCurrent;
  private DocumentTreeNode _pageCurrent;
  private TableData _mainTableCurrent;
  private string _variableRowCurrent = "";
  public bool _document_readOnly;
  private int _kudaVhodit_Skolko = -1;
  private int _kudaVhodit_Index = -1;
  private bool _isItogo;
  private TableData _currKudaVhoditRow;
  private TableData _contextLRIRow;
  private TableData _lriTable;
  private List<string> list_Names_Pages_Temlate;
  private List<string> list_Names_Pages_Document;
  private bool is_Extended_List_Names_Pages_ByTemplate;
  public bool is_TitList_In_Document;
  public bool is_LIZM_In_Document;
  public bool is_RemarkPage_In_Document;
  private int n_TitList_In_Template;
  private int n_LIZM_In_Template;
  private int n_RemarkPage_In_Template;
  private int number_Page_Current;
  private int number_Page_First_Info;
  private int number_Page_End_Info;
  private int quanty_Info_Pages;
  private int number_Page_First_CurrentName;
  private int number_Page_End_CurrentName;
  private string name_Page_Current;
  private string name_Page_Next;
  public Vedomost_VB_Static.TypePageVedom typePageVedom;
  private string group_razdel_Ved = "1";
  private ProductInfo prodInfo_reCreate;
  private long _idSP = -1;
  private string metodCreate = "";
  private string metodFrom = "";
  public string isDeleteIdenticalTexts = "";
  private int variablesCount;
  private List<ProductInfo> listAll_IspolneniySp_prodInfo_ReCreate;
  private int startPageNumber = 1;
  private bool isListZagolovki;
  public bool isAddRowKudaVhodit;
  public List<One_ImsObjectType_With_One_Ved_Nastr> list_Ims_of_this_type = new List<One_ImsObjectType_With_One_Ved_Nastr>();
  private bool isLU;
  /// <summary>Команды меню окна редактора ведомостей</summary>
  internal List<string> VedomostContexMenuList = new List<string>();
  /// <summary>Команды меню окна редактора ведомостей, которые обрабатываются внутри окна</summary>
  internal Dictionary<string, AVSPluginExecuteCommand> VedomostWindowMenu = new Dictionary<string, AVSPluginExecuteCommand>();
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  public VedomostEditorWindow() => this.InitializeComponent();

  /// <summary>Метод для создания формы через делегат</summary>
  /// <param name_From_Oglavlenie="documentManager">Менеджер документов</param>
  /// <param name_From_Oglavlenie="document">Документ</param>
  /// <param name_From_Oglavlenie="readOnly">Только для чтения</param>
  /// <returns>Окно редактора документов</returns>
  public static ImDocumentEditorForm VedomostEditorWindowCreator(
    IImDocumentManager documentManager,
    ImDocument document,
    bool readOnly)
  {
    return (ImDocumentEditorForm) new VedomostEditorWindow(documentManager, document, readOnly);
  }

  /// <summary>Конструктор</summary>
  /// <param name_From_Oglavlenie="documentManager">Менеджер документов</param>
  /// <param name_From_Oglavlenie="document">Документ</param>
  /// <param name_From_Oglavlenie="readOnly">Только для чтения</param>
  public VedomostEditorWindow(
    IImDocumentManager documentManager,
    ImDocument document,
    bool readOnly)
    : base(documentManager, document, readOnly)
  {
    if (this.LoadOsnovnyeDannye())
      this._isVveliOsnovnyeDannye = true;
    if (document.MaterialKeyWords != null)
      return;
    document.SetMaterialKeyWords(FormSetupKeyWords.GetKeywords(this._idSP));
  }

  protected override void Init()
  {
    AVSPlugin.AllocateAVSLicense();
    base.Init();
    this.SetBaseEditCommandsEnabled(false, false);
    this.SetTableEditCommandsEnabled(false, false);
    this.DocumentControl.GetCustomElementContextMenu += new GetCustomElementContextMenu_EventHandler(this.DocumentControl_GetCustomElementContextMenu);
    this.Guid = DocumentEditorPlugin.VedomostWindowGuid;
    this.CreateMenuCommands();
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    if (disposing)
      AVSPlugin.ReleaseAVSLicense();
    base.Dispose(disposing);
  }

  /// <summary> Ввод всех основных данных для документа </summary>
  /// <returns></returns>
  protected bool LoadOsnovnyeDannye()
  {
    this._guidTemplateDoc = Vedomost_VB_Static.Get_GuidTemplateVedomosty_ByObjTypeVed(this.DocumentType, this._formaGroup_Doc);
    if (MetaDataHelper.IsObjectTypeChildOf(this.DocumentType, AvsIDCache.ObjType_ConstrTabl))
      this._typeDoc = Vedomost_VB.TypeDoc.Tabl;
    if (MetaDataHelper.IsObjectTypeChildOf(this.DocumentType, AvsIDCache.ObjType_Vedomost))
      this._typeDoc = Vedomost_VB.TypeDoc.Ved;
    if (MetaDataHelper.IsObjectTypeChildOf(this.DocumentType, AvsIDCache.ObjType_DocumsExpluat))
      this._typeDoc = Vedomost_VB.TypeDoc.Ved;
    if (MetaDataHelper.IsObjectTypeChildOf(this.DocumentType, AvsIDCache.ObjType_Espd))
      this._typeDoc = Vedomost_VB.TypeDoc.Espd;
    if (MetaDataHelper.IsObjectTypeChildOf(this.DocumentType, AvsIDCache.ObjType_DocumsProg))
      this._typeDoc = Vedomost_VB.TypeDoc.Espd;
    if (MetaDataHelper.IsObjectTypeChildOf(this.DocumentType, AvsIDCache.ObjType_EspdLU))
      this._typeDoc = Vedomost_VB.TypeDoc.EspdLU;
    this._formaGroup_Doc = this.Document.GetAttributeValue("GroupForm", true);
    if (this._formaGroup_Doc == "A" || this._formaGroup_Doc == "B")
      Vedomost_VB_Static.Create_Variables_Coordination_From_Document(this.Document, out this._variables_Coordination);
    if (this._formaGroup_Doc == "B")
    {
      Guid guid = Vedomost_VB_Static.Association_A_B(this._guidTemplateDoc);
      if (guid != Guid.Empty)
        this._guidTemplateDoc = guid;
    }
    this._designationArticle = this.Document.GetAttributeValue("_designationArticle", true);
    this._designationDoc = this.Document.GetAttributeValue("_designationDoc", true);
    if (this._designationDoc == "")
    {
      this._designationDoc = this.DocumentDesignation;
      this.Document.SetAttributeValue("_designationDoc", this._designationDoc);
    }
    this._nameArticle = this.Document.GetAttributeValue("_nameArticle", true);
    if (this._nameArticle == "")
    {
      this._nameArticle = this.DocumentName;
      this.Document.SetAttributeValue("_nameArticle", this._nameArticle);
    }
    this.metodCreate = this.Document.GetAttributeValue("metodCreate", true);
    this.metodFrom = this.Document.GetAttributeValue("metodFrom", true);
    string attributeValue1 = this.Document.GetAttributeValue("iDSP", true);
    if (attributeValue1 != "" && attributeValue1 != "0")
      this._idSP = (long) int.Parse(attributeValue1);
    string attributeValue2 = this.Document.GetAttributeValue("prodInfo_Id", true);
    if (attributeValue2 != "" && attributeValue2 != "0")
    {
      this.prodInfo_reCreate = new ProductInfo();
      this.prodInfo_reCreate.Id = (long) int.Parse(attributeValue2);
      this.prodInfo_reCreate.ObjectType = int.Parse(this.Document.GetAttributeValue("prodInfo_ObjectType", true));
      this.prodInfo_reCreate.Designation = this.Document.GetAttributeValue("prodInfo_Designation", true);
    }
    string attributeValue3 = this.Document.GetAttributeValue("VariablesCount", true);
    if (attributeValue3 != "" && attributeValue3 != "0")
      this.variablesCount = int.Parse(attributeValue3);
    if (this.variablesCount > 0)
    {
      this.listAll_IspolneniySp_prodInfo_ReCreate = new List<ProductInfo>();
      for (int index = 0; index < this.variablesCount; ++index)
      {
        ProductInfo productInfo = new ProductInfo();
        string attributeValue4 = this.Document.GetAttributeValue("prodInfo_Id_" + index.ToString(), true);
        if (attributeValue4 != "" && attributeValue4 != "0")
        {
          productInfo.Id = long.Parse(attributeValue4);
          string attributeValue5 = this.Document.GetAttributeValue("prodInfo_ObjectType_" + index.ToString(), true);
          if (attributeValue5 != "" && attributeValue5 != "0")
          {
            productInfo.ObjectType = int.Parse(attributeValue5);
            string attributeValue6 = this.Document.GetAttributeValue("prodInfo_Designation_" + index.ToString(), true);
            if (attributeValue6 != "" && attributeValue6 != "0")
            {
              productInfo.Designation = attributeValue6;
              this.listAll_IspolneniySp_prodInfo_ReCreate.Add(productInfo);
            }
          }
        }
      }
    }
    else if (this._formaGroup_Doc == "A" || this._formaGroup_Doc == "B")
    {
      if (this._variables_Coordination == null)
        Vedomost_VB_Static.Create_Variables_Coordination_From_Document(this.Document, out this._variables_Coordination);
      if (this._variables_Coordination.list_Variables.Count == 0)
      {
        this._variables_Coordination.list_Variables.Add(this._designationArticle);
        this._variables_Coordination.list_Captions.Add("-");
        if (this._formaGroup_Doc == "B")
          Vedomost_VB_Static.FillProductHeadersOnPages(this.Document, this._designationArticle, this._variables_Coordination);
      }
      this.variablesCount = 1;
      this.Document.SetAttributeValue("VariablesCount", this.variablesCount.ToString());
      this.Document.SetAttributeValue("Variable_0", this._designationArticle);
      this.Document.SetAttributeValue("Caption_0", "-");
    }
    this.isDeleteIdenticalTexts = this.Document.GetAttributeValue("DeleteIdenticalTexts", true);
    string attributeValue7 = this.Document.GetAttributeValue("_guidTypeDoc", true);
    if (attributeValue7 == "")
    {
      this._guidTypeDoc = Vedomost_VB_Static.Get_GuidTypeVed_ByObjTypeVed(this.DocumentType);
      if (this._guidTypeDoc != Guid.Empty)
        this.Document.SetAttributeValue("_guidTypeDoc", this._guidTypeDoc.ToString());
    }
    else
    {
      this._guidTypeDoc = Guid.Parse(attributeValue7);
      this.Document.SetAttributeValue("_guidTypeDoc", attributeValue7);
    }
    if (this._guidTypeDoc == Guid.Empty)
      return false;
    if (this._guidTemplateDoc != Guid.Empty)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        this._templateID = sessionKeeper.Session.GetObject(this._guidTemplateDoc).ObjectID;
      if (this._templateID == -1L)
        return false;
      this._docTemplate = DocumentEditorPlugin.LoadDocumentFromDBObject(this._templateID);
      if (this.Document.GetAttributeValue("guidTemplate", true) == "")
        this.Document.SetAttributeValue("guidTemplate", this._guidTemplateDoc.ToString());
    }
    else
    {
      int num = (int) MessageBox.Show($"Шаблон документа\r\n{this.Document.Designation}\r\nне задан", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    this._vedomost_VB_new = new Vedomost_VB(this._typeDoc);
    this._vedomost_VB_new._one_Ved_Nastr_RazrabatyvaemoiVed = !Vedomost_VB_Static.IsUse_New_System_ByOneNastr ? (!(this._guidTemplateDoc == Guid.Empty) ? Vedomost_VB_Static.Read_One_Ved_Nastr_byDocTemplateGuid(this._guidTemplateDoc, this._guidTypeDoc, this._typeDoc) : Vedomost_VB_Static.Read_One_Ved_Nastr_byDocTypeGuid(this._guidTypeDoc, this._typeDoc)) : Vedomost_VB_Static.Read_One_Ved_Nastr_byDocTypeGuid_From_Conformity(this._guidTypeDoc);
    if (this._vedomost_VB_new._one_Ved_Nastr_RazrabatyvaemoiVed == null)
      this._vedomost_VB_new._one_Ved_Nastr_RazrabatyvaemoiVed = new One_Ved_Nastr();
    this._one_Ved_Nastr_Curr = this._vedomost_VB_new._one_Ved_Nastr_RazrabatyvaemoiVed;
    this._nameTypeDoc = this.Document.GetAttributeValue("_nameTypeDoc", true);
    if (this._nameTypeDoc == "")
    {
      this._nameTypeDoc = this._one_Ved_Nastr_Curr._nameVed;
      this.Document.SetAttributeValue("_nameTypeDoc", this._nameTypeDoc);
    }
    if (this.Document.GetAttributeValue("_typeVed", true) == "")
      this.Document.SetAttributeValue("_typeVed", this._one_Ved_Nastr_Curr._typeVed.ToString());
    if (this.Document.GetAttributeValue("_kodDoc", true) == "")
      this.Document.SetAttributeValue("_kodDoc", Vedomost_VB_Static.Get_DocumentTypeSuffix_ForObjectTypeId(this._one_Ved_Nastr_Curr._idTypeVed));
    if (this._designationArticle == "")
    {
      string attributeValue8 = this.Document.GetAttributeValue("_kodDoc", true);
      if (!string.IsNullOrEmpty(attributeValue8))
      {
        int startIndex = this.Document.Designation.LastIndexOf(attributeValue8);
        if (startIndex > -1)
        {
          this._designationArticle = this.Document.Designation.Remove(startIndex);
          this._designationArticle = this._designationArticle.Trim();
          if (!string.IsNullOrEmpty(this._designationArticle))
            this.Document.SetAttributeValue("_nameArticle", this._designationArticle);
        }
      }
    }
    this.isListZagolovki = Vedomost_VB_Static.CheckListZagolovki(this.Document);
    this._vedomost_VB_new.ListCommonId_Filled(this._vedomost_VB_new.listCommonId);
    if (this._vedomost_VB_new.listCommonId == null)
      return false;
    Vedomost_VB_Static.List_Ved_imsObjectType_Filled(false);
    Vedomost_VB_Static.List_Tabl_imsObjectType_Filled(false);
    if (Vedomost_VB_Static.List_Ved_OpisanieVed == null)
      Vedomost_VB_Static.List_Ved_OpisanieVed_Create();
    if (Vedomost_VB_Static.List_Tabl_OpisanieTabl == null)
      Vedomost_VB_Static.List_Tabl_OpisanieTabl_Create();
    if (AVS6_From_Avs6Main._list_recordFields == null || AVS6_From_Avs6Main._list_recordFields.Count == 0)
      Vedomost_VB_Static.IsAvs6ToIps = false;
    string nodeId = "Следующая страница";
    if (this.Document.Nodes[0].Name == "Титульный лист" && this.Document.Nodes.Count == 1 && this.Document.Template.FindNode(nodeId) is PageData node)
    {
      PageData child = (PageData) node.CloneFromTemplate(true, true);
      if (child != null)
        this.Document.AddChildNode((DocumentTreeNode) child, false, false);
    }
    if (this._typeDoc == Vedomost_VB.TypeDoc.Espd && this._one_Ved_Nastr_Curr._espd._isAddLU)
    {
      this.isLU = !Vedomost_VB_Static.Check_LU_By_Document(this.Document).IsUndefinedId();
      if (!this.isLU && this._one_Ved_Nastr_Curr._espd._isCreateLU)
      {
        if (!new Espd_VB()
        {
          _nameProg = this._nameArticle,
          _nameDoc = this._nameTypeDoc,
          _designationDocLU = (this._designationDoc + "-ЛУ"),
          _iDSP = 0L
        }.CreateAndOpenLU(this._one_Ved_Nastr_Curr._espd._isCreateLU).IsUndefinedId())
          this.isLU = true;
      }
      if (this.isLU)
      {
        if (!Vedomost_VB_Static.Is_LU_InTitList(this.Document, "Обозначение ЛУ"))
          Vedomost_VB_Static.Add_LU_ToTitList(this.Document, this._designationDoc + "-ЛУ", "Обозначение ЛУ");
        if (!this.Check_DocRowLU_ESPD())
        {
          if (!this.Check_FirstZagol_ESPD())
            this.Add_FirstZagol_ESPD();
          this.DocumentControl.SetSelection((DocumentTreeNode) Vedomost_VB_Static.FindFirstMainTable(this.Document), true, Point.Empty, true, false);
          long objectIdByDesignation = Vedomost_VB_Static.Get_ObjectID_By_Designation(this._designationDoc + "-ЛУ");
          if (!objectIdByDesignation.IsUndefinedId() && this._one_Ved_Nastr_Curr._espd._isAddRemark)
            this.AddVedRow_ByObjectID(objectIdByDesignation, "Auto", "1", false, this._one_Ved_Nastr_Curr._espd._textRemark, false);
        }
      }
    }
    this.list_Names_Pages_Temlate = Vedomost_VB_Static.Get_List_Names_Pages_Temlate(this.Document.Template);
    this.list_Names_Pages_Document = Vedomost_VB_Static.Get_List_Names_Pages_Document((DocumentTreeNode) this.Document);
    this.is_Extended_List_Names_Pages_ByTemplate = Vedomost_VB_Static.Is_Extended_List_Names_Pages_ByTemplate(this.list_Names_Pages_Temlate);
    this.is_TitList_In_Document = Vedomost_VB_Static.Is_TitList_In_Document((DocumentTreeNode) this.Document);
    this.is_LIZM_In_Document = Vedomost_VB_Static.Is_LIZM_In_Document((DocumentTreeNode) this.Document);
    this.is_RemarkPage_In_Document = Vedomost_VB_Static.Is_RemarkPage_In_Document((DocumentTreeNode) this.Document);
    this.n_TitList_In_Template = Vedomost_VB_Static.N_TitList_In_Template((DocumentTreeNode) this._docTemplate);
    this.n_LIZM_In_Template = Vedomost_VB_Static.N_LIZM_In_Template((DocumentTreeNode) this._docTemplate);
    this.n_RemarkPage_In_Template = Vedomost_VB_Static.N_RemarkPage_In_Template((DocumentTreeNode) this._docTemplate);
    this.Filled_list_Ims_of_this_type();
    this.ClearArticleVed();
    this._document = this.Document;
    return true;
  }

  /// <summary> Удаление атрибутов, если это создано по прототипу </summary>
  private void ClearArticleVed()
  {
    if (!(this.DocumentDesignation != this.Document.GetAttributeValue("_designationDoc", false)))
      return;
    this.Document.SetAttributeValue("_designationDoc", this.DocumentDesignation);
    string attributeValue = this.Document.GetAttributeValue("metodCreate", true);
    if (attributeValue != "" && attributeValue != "ChangeType")
    {
      this.Document.SetAttributeValue("metodCreate", "");
      this.metodCreate = "";
    }
    if (this.Document.GetAttributeValue("metodFrom", true) != "" && attributeValue != "ChangeType")
      this.Document.SetAttributeValue("metodFrom", "");
    if (this.Document.GetAttributeValue("prodInfo_Id", true) != "")
      this.Document.SetAttributeValue("prodInfo_Id", "");
    if (this.Document.GetAttributeValue("prodInfo_ObjectType", true) != "")
      this.Document.SetAttributeValue("prodInfo_ObjectType", "");
    if (!(this.Document.GetAttributeValue("prodInfo_Designation", true) != ""))
      return;
    this.Document.SetAttributeValue("prodInfo_Designation", "");
  }

  /// <summary>Получить текущую строку ведомости. Если фокус не на строке, то возвращает null</summary>
  /// <returns></returns>
  public TableData GetSelectedVedomostRow()
  {
    DocumentTreeNode[] selectedNodes = this.DocumentControl.GetSelectedNodes();
    return selectedNodes.Length == 1 ? VedomostEditorWindow.FindParentVedomostRowDocNode(selectedNodes[0]) : (TableData) null;
  }

  /// <summary>Найти узел строки ведомости, который является родительским для заданного узла</summary>
  /// <param name_From_Oglavlenie="docNode">Узел документа, принадлежащий строке ведомости</param>
  /// <returns>Строка ведомости, которой пренадлежит заданный узел</returns>
  public static TableData FindParentVedomostRowDocNode(DocumentTreeNode docNode)
  {
    TableData firstTable;
    while (true)
    {
      switch (docNode)
      {
        case null:
          goto label_6;
        case Page _:
        case ImDocument _:
          goto label_1;
        case TableData tableData:
          firstTable = tableData.FindFirstTable();
          if (!VedomostEditorWindow.IsVedomostRowDocNode(firstTable))
            break;
          goto label_3;
      }
      docNode = docNode.Parent;
    }
label_1:
    return (TableData) null;
label_3:
    return firstTable;
label_6:
    return (TableData) null;
  }

  /// <summary>Найти владельца строки которой принадлежит элемент.
  /// Если элемент преставление, то ищет и ее строку данных.</summary>
  /// <param name_From_Oglavlenie="context">Элемент в строке или строка</param>
  /// <param name_From_Oglavlenie="viewRowParent">Родитель строки представления</param>
  /// <param name_From_Oglavlenie="viewRowIndex">Индекс строки преставления в Nodes</param>
  private static void FindRowParent(
    DocumentTreeNode context,
    out TableElement viewRowParent,
    out int viewRowIndex)
  {
    viewRowParent = (TableElement) null;
    viewRowIndex = -1;
    if (!(context is RectangleElement rectangleElement))
      return;
    TableData rowParent;
    viewRowIndex = rectangleElement.GetRowIndex(out rowParent);
    viewRowParent = rowParent as TableElement;
  }

  /// <summary>Узел документа является строкой ведомости</summary>
  /// <param name_From_Oglavlenie="docNode">Узел документа</param>
  /// <returns>true, если заданный узел документа является строкой спецификации</returns>
  public static bool IsVedomostRowDocNode(TableData docRow)
  {
    if (docRow != null)
      docRow = docRow.FindFirstTable();
    return true;
  }

  private void DocumentControl_GetCustomElementContextMenu(
    object sender,
    GetCustomElementContextMenu_EventArgs e)
  {
    ICommandManager commandManager = this.DocumentControl.DocumentManager.CommandManager;
    e.ContextMenuItems.Clear();
    foreach (string vedomostContexMenu in AVSPlugin.Instance.VedomostContexMenuList)
      this.AddEnabledContextMenu(vedomostContexMenu, e.ContextMenuItems, commandManager);
  }

  /// <summary>Добавить команду в статический список команд контекстного меню для ведомостей</summary>
  /// <param name_From_Oglavlenie="commandName">Имя команды</param>
  private static void AddContextMenu(string commandName)
  {
    if (string.IsNullOrEmpty(commandName))
      throw new ArgumentNullException(nameof (commandName));
    if (AVSPlugin.Instance.VedomostContexMenuList.Contains(commandName))
      return;
    AVSPlugin.Instance.VedomostContexMenuList.Add(commandName);
  }

  /// <summary>Общий обработчик команд</summary>
  /// <param name_From_Oglavlenie="commandState">Данные команды</param>
  /// <returns>true, если команда найдена и обработана</returns>
  public override bool Execute(ICommandState commandState)
  {
    AVSPluginExecuteCommand pluginExecuteCommand;
    return this.VedomostWindowMenu.TryGetValue(commandState.CommandName, out pluginExecuteCommand) && pluginExecuteCommand != null && pluginExecuteCommand(commandState) || AVSPlugin.Instance.VedomostEditorVBMenu.TryGetValue(commandState.CommandName, out pluginExecuteCommand) && pluginExecuteCommand != null && pluginExecuteCommand(commandState) || base.Execute(commandState);
  }

  /// <summary>Создать меню ДОКУМЕНТ и добавить в контекстное меню базовые команды</summary>
  /// 
  ///             Вызывается в AVSPlugin.Load
  public static void CreateMenuAndBaseContextCommands()
  {
    MenuBarItem menuBarItem = new MenuBarItem("Документ");
    menuBarItem.CommandName = "AVS.VB.Menu";
    menuBarItem.ToolTipText = "Команды редактирования документа";
    menuBarItem.Visible = false;
    ((BarManager) ServicesManager.GetService(typeof (BarManager))).MenuBar.Items.Insert(0, (ToolbarItemBase) menuBarItem);
    AVSPlugin.Instance.CommandManager.Add((ButtonItemBase) menuBarItem);
    VedomostEditorWindow.AddContextMenu("Cut");
    VedomostEditorWindow.AddContextMenu("Copy");
    VedomostEditorWindow.AddContextMenu("Paste");
    VedomostEditorWindow.AddContextMenu("Delete");
  }

  /// <summary> Обращение к ПОДМЕНЮ "Добавить запись..." из КОНКРЕТНОГО окна ведомости </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private static void miAddRecordVBMenu_BeforePopup333(object sender, MenuPopupEventArgs e)
  {
    if (!(sender is MenuButtonItem menuButtonItem1))
      return;
    menuButtonItem1.Items.Clear();
    menuButtonItem1.Items.Add("Дополнительная 1");
    MenuButtonItem menuButtonItem2 = menuButtonItem1.Items[menuButtonItem1.Items.Count - 1];
    menuButtonItem2.Tag = (object) "AVS.VB.AddVedRow_Additional1";
    menuButtonItem2.Click += new EventHandler(VedomostEditorWindow.mi_CreateRecordVB);
    menuButtonItem1.Items.Add("Дополнительная 2");
    MenuButtonItem menuButtonItem3 = menuButtonItem1.Items[menuButtonItem1.Items.Count - 1];
    menuButtonItem3.Tag = (object) "AVS.VB.AddVedRow_Additional2";
    menuButtonItem3.Click += new EventHandler(VedomostEditorWindow.mi_CreateRecordVB);
    menuButtonItem1.Items.Add("Дополнительная 3");
    MenuButtonItem menuButtonItem4 = menuButtonItem1.Items[menuButtonItem1.Items.Count - 1];
    menuButtonItem4.Tag = (object) "AVS.VB.AddVedRow_Additional3";
    menuButtonItem4.Click += new EventHandler(VedomostEditorWindow.mi_CreateRecordVB);
    menuButtonItem1.Items.Add("Дополнительная 4");
    MenuButtonItem menuButtonItem5 = menuButtonItem1.Items[menuButtonItem1.Items.Count - 1];
    menuButtonItem5.Tag = (object) "AVS.VB.AddVedRow_Additional4";
    menuButtonItem5.Click += new EventHandler(VedomostEditorWindow.mi_CreateRecordVB);
    if (menuButtonItem1.Items.Count != 0)
      return;
    menuButtonItem1.Items.Add("[Нет записей]");
    menuButtonItem1.Items[0].Enabled = false;
  }

  /// <summary> Выполнение Конкрктной каманды "Вставить Дополнительная 1"  или 2 или 3 В КОНКРЕТНОМ ДОКУМЕНТЕ </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private static void mi_CreateRecordVB(object sender, EventArgs e)
  {
    if (sender == null || !(sender is MenuButtonItem menuButtonItem))
      return;
    string tag = menuButtonItem.Tag as string;
  }

  /// <summary>Добавить пункт меню для окна ведомости</summary>
  /// <param name_From_Oglavlenie="menu">Пукт меню</param>
  /// <param name_From_Oglavlenie="commandName">Имя команды</param>
  /// <param name_From_Oglavlenie="commandCaption">Заголовок команды в меню</param>
  /// <param name_From_Oglavlenie="commandHint">Посказка для команды</param>
  /// <param name_From_Oglavlenie="beginGroup">Команда является началом группы команд</param>
  /// <param name_From_Oglavlenie="useInContextMenu">Команда будет использоваться в контекстном меню</param>
  /// <param name_From_Oglavlenie="commandHandler">Функция Обработчик команды</param>
  private void AddMenuItem(
    MenuItemBase menu,
    string commandName,
    string commandCaption,
    string commandHint,
    bool beginGroup,
    bool useInContextMenu,
    AVSPluginExecuteCommand commandHandler)
  {
    AVSPlugin.Instance.AddNewMenuItem(menu, commandName, commandCaption, commandHint, beginGroup, useInContextMenu);
    if (!AVSPlugin.Instance.VedomostEditorVBMenu.ContainsKey(commandName))
      AVSPlugin.Instance.VedomostEditorVBMenu.Add(commandName, (AVSPluginExecuteCommand) null);
    this.VedomostWindowMenu.Add(commandName, commandHandler);
    VedomostEditorWindow.AddContextMenu(commandName);
  }

  /// <summary> Добавить пункт меню для окна ведомости с заданием Клавиши </summary>
  /// <param name_From_Oglavlenie="menu">Пукт меню</param>
  /// <param name_From_Oglavlenie="commandName">Имя команды</param>
  /// <param name_From_Oglavlenie="commandCaption">Заголовок команды в меню</param>
  /// <param name_From_Oglavlenie="commandHint">Посказка для команды</param>
  /// <param name_From_Oglavlenie="beginGroup">Команда является началом группы команд</param>
  /// <param name_From_Oglavlenie="useInContextMenu">Команда будет использоваться в контекстном меню</param>
  /// <param name_From_Oglavlenie="commandHandler">Функция Обработчик команды</param>
  /// <param name="keys"></param>
  private void AddMenuItem_WithKeys(
    MenuItemBase menu,
    string commandName,
    string commandCaption,
    string commandHint,
    bool beginGroup,
    bool useInContextMenu,
    AVSPluginExecuteCommand commandHandler,
    Keys keys)
  {
    MenuButtonItem menuButtonItem = AVSPlugin.Instance.AddNewMenuItem(menu, commandName, commandCaption, commandHint, beginGroup, useInContextMenu);
    if (!AVSPlugin.Instance.VedomostEditorVBMenu.ContainsKey(commandName))
      AVSPlugin.Instance.VedomostEditorVBMenu.Add(commandName, (AVSPluginExecuteCommand) null);
    int num = (int) keys;
    menuButtonItem.Shortcut = (Shortcut) num;
    this.VedomostWindowMenu.Add(commandName, commandHandler);
    VedomostEditorWindow.AddContextMenu(commandName);
  }

  /// <summary> Добавить пункт меню для окна ведомости с ИКОНКОЙ </summary>
  /// <param name_From_Oglavlenie="menu">Пукт меню</param>
  /// <param name_From_Oglavlenie="commandName">Имя команды</param>
  /// <param name_From_Oglavlenie="commandCaption">Заголовок команды в меню</param>
  /// <param name_From_Oglavlenie="commandHint">Посказка для команды</param>
  /// <param name="iconName"></param>
  /// <param name_From_Oglavlenie="beginGroup">Команда является началом группы команд</param>
  /// <param name_From_Oglavlenie="useInContextMenu">Команда будет использоваться в контекстном меню</param>
  /// <param name_From_Oglavlenie="commandHandler">Функция Обработчик команды</param>
  private void AddMenuItem_WithIcon(
    MenuItemBase menu,
    string commandName,
    string commandCaption,
    string commandHint,
    string iconName,
    bool beginGroup,
    bool useInContextMenu,
    AVSPluginExecuteCommand commandHandler)
  {
    AVSPlugin.Instance.AddNewMenuItemIcon(menu, commandName, commandCaption, iconName, commandHint, beginGroup, useInContextMenu);
    if (!AVSPlugin.Instance.VedomostEditorVBMenu.ContainsKey(commandName))
      AVSPlugin.Instance.VedomostEditorVBMenu.Add(commandName, (AVSPluginExecuteCommand) null);
    this.VedomostWindowMenu.Add(commandName, commandHandler);
    VedomostEditorWindow.AddContextMenu(commandName);
  }

  /// <summary>Разрешено показывать дерево документа</summary>
  protected override bool CanShowStructureTree() => true;

  /// <summary>Создать команды меню, которые обрабатываются в этом окне</summary>
  public void CreateMenuCommands()
  {
    string str = "Intermech.Document.Model.Resources.";
    ServicesManager.GetService(typeof (ICategoryTypeIconService));
    MenuBarItem menuBar = ((BarManager) ServicesManager.GetService(typeof (BarManager))).MenuBar.FindMenuBar("AVS.VB.Menu");
    if (menuBar == null)
      return;
    Vedomost_VB_Static.Check_For_VB();
    string machineName = Environment.MachineName;
    this.AddMenuItem((MenuItemBase) menuBar, "AVS.VB.EditGroupForm", "Форма документа", "Изменить групповую форму документа", false, false, new AVSPluginExecuteCommand(this.EditGroupForm));
    this.AddMenuItem((MenuItemBase) menuBar, "AVS.VB.ListVariableToDocument", "Список исполнений", "Работа со списком исполнений документа", false, false, new AVSPluginExecuteCommand(this.ListVariableToDocument));
    this.AddMenuItem((MenuItemBase) menuBar, "AVS.VB.CreateXmlFile_FromDocument", "Создать файл XML", "Сохранить данные в файл формата XML", true, false, new AVSPluginExecuteCommand(this.CreateXmlFile_FromDocument));
    this.AddMenuItem((MenuItemBase) menuBar, "AVS.VB.Filled_Data_From_XmlFile_To_Document", "Читать файл XML", "Читать данные из файла формата XML", false, false, new AVSPluginExecuteCommand(this.Read_From_XmlFile_To_Document));
    if (!List_Element_Accord_Avs6_Ips.isPopytkaInits)
    {
      List_Element_Accord_Avs6_Ips.Read_From_Base();
      List_Element_Accord_Avs6_Ips.Begin();
    }
    if (AvsConfig.General.AskAVS6 && Vedomost_VB_Static.IsAvs6ToIps)
    {
      this.AddMenuItem((MenuItemBase) menuBar, "AVS.VB.Filled_Data_From_Avs6File_To_Document", "Читать данные из файла AVS6", "Читать данные из файла AVS6 в текущий документ", true, false, new AVSPluginExecuteCommand(this.Filled_Data_From_File_Avs6));
      this.AddMenuItem((MenuItemBase) menuBar, "AVS.VB.Create_Document_From_Avs6File", "Создать документ из файла AVS6", "Создать новый документ на основе данных файла AVS6", false, false, new AVSPluginExecuteCommand(this.Create_Document_From_Avs6File));
    }
    this.AddMenuItem((MenuItemBase) menuBar, "AVS.VB.ReDrawing", "Отобразить документ заново", "Оформить документ (ведомость) заново, например, после изменения шаблона", true, false, new AVSPluginExecuteCommand(Vedomost_VB_Static.AVSPluginExecuteCommand_ReDrawing));
    this.AddMenuItem((MenuItemBase) menuBar, "AVS.VB.ReCreate", "Создать документ заново", "Создать данный документ (ведомость) автоматически заново", false, false, new AVSPluginExecuteCommand(Vedomost_VB_Static.AVSPluginExecuteCommand_ReCreate));
    this.AddMenuItem((MenuItemBase) menuBar, "AVS.VB.ChangeTyp", "Изменить тип документа", "Изменить тип документа", false, false, new AVSPluginExecuteCommand(Vedomost_VB_Static.AVSPluginExecuteCommand_ChangeTyp));
    this.AddMenuItem((MenuItemBase) menuBar, "AVS.VB.CreateListZagol", "Создать список заголовков", "", true, false, new AVSPluginExecuteCommand(this.CreateListZagol));
    this.AddMenuItem((MenuItemBase) menuBar, "AVS.VB.UpdateListZagol", "Обновить список заголовков", "", true, false, new AVSPluginExecuteCommand(this.CreateListZagol));
    this.AddMenuItem((MenuItemBase) menuBar, "AVS.VB.DeleteListZagol", "Удалить список заголовков", "", false, false, new AVSPluginExecuteCommand(this.DeleteListZagol));
    this.AddMenuItem((MenuItemBase) menuBar, "AVS.VB.FilledNumbersIspolneniy", "Расположение исполнений по листам", "Вывод расположения исполнений по листам", false, false, new AVSPluginExecuteCommand(this.FilledNumbersIspolneniy));
    MenuButtonItem menuItem = DocumentMenuHelper.CreateMenuItem("AVS.AddRecordsVB", "&Добавить строку...", "Команды добавления различных строк", true, true, (ICommandManager) null);
    menuItem.Items.Add("[Нет записей]");
    menuItem.BeforePopup += new MenuItemBase.BeforePopupEventHandler(this.miAddRecordVBMenu_BeforePopup2);
    MenuButtonItem contextMenuItem = NodeContextMenu.GetContextMenuItem("AVS.AddRecordsVB");
    contextMenuItem.Items.Add("[Нет записей]");
    contextMenuItem.BeforePopup += new MenuItemBase.BeforePopupEventHandler(this.miAddRecordVBMenu_BeforePopup2);
    this.AddMenuItem((MenuItemBase) menuBar, "AVS.AddRecordsVB", "&Добавить строку...", "Команды добавления различных строк", true, true, (AVSPluginExecuteCommand) null);
    this.AddMenuItem((MenuItemBase) menuBar, "AVS.VB.AddRowKudaVhodit", "Добавить подстроку \"Куда входит\"", "Добавить подстроку \"Куда входит\"", true, true, new AVSPluginExecuteCommand(this.AddVedRow_KudaVhodit));
    this.AddMenuItem((MenuItemBase) menuBar, "AVS.VB.AddNewKudaVhoditWithDBObject", "Выбрать существующий объект", "В строку \"Куда водит\" выбрать существующий объект", false, true, new AVSPluginExecuteCommand(this.AddNewKudaVhoditWithDBObject));
    this.AddMenuItem((MenuItemBase) menuBar, "AVS.VB.AddRowItogo", "Добавить подстроку \"Итого\"", "Добавить подстроку \"Итого\"", false, true, new AVSPluginExecuteCommand(this.AddVedRow_Itogo));
    this.AddMenuItem((MenuItemBase) menuBar, "AVS.VB.AddCopyRow", "Добавить копию текущей строки", "Добавить копию текущей строки", true, true, new AVSPluginExecuteCommand(this.AddCopyRow));
    this.AddMenuItem_WithKeys((MenuItemBase) menuBar, "AVS.VB.DocRowUp", "Строку переместить вверх", "Строку переместить вверх", true, true, new AVSPluginExecuteCommand(this.DocRowUp), Keys.Up | Keys.Control);
    this.AddMenuItem_WithKeys((MenuItemBase) menuBar, "AVS.VB.DocRowDown", "Строку переместить вниз", "Строку переместить вниз", false, true, new AVSPluginExecuteCommand(this.DocRowDown), Keys.Down | Keys.Control);
    this.AddMenuItem((MenuItemBase) menuBar, "AVS.VB.SortingDoc", "Сортировать", "Сортировать документ", true, false, new AVSPluginExecuteCommand(this.SortingDoc));
    this.AddMenuItem_WithIcon((MenuItemBase) menuBar, "AVS.VB.AddRowFromImbase", "Добавить запись из Imbase...", "В документ добавить строку с изделием из IMBASE", str + "InsertFromImbase.png", true, true, new AVSPluginExecuteCommand(this.AddVedRowFromImbase));
    this.AddMenuItem((MenuItemBase) menuBar, "AVS.VB.AddRowGroupFromImbase", "Групповой ввод записей из ImBase...", "В документ добавить строки с изделиями из IMBASE", false, true, new AVSPluginExecuteCommand(this.AddRowsGroupFromImbase));
    this.AddMenuItem_WithIcon((MenuItemBase) menuBar, "AVS.VB.AddNewRowWithDBObject", "Добавить запись с существующим объектом...", "В документ добавить строку с существующим объектом IPS", str + "Insert-Object.png", false, true, new AVSPluginExecuteCommand(this.AddNewRowWithDBObject));
    this.AddMenuItem((MenuItemBase) menuBar, "AVS.VB.FillingExistingImbase", "В текущую строку добавить данные из Imbase", "В ТЕКУЩУЮ СТРОКУ добавить данные из Imbase", true, true, new AVSPluginExecuteCommand(this.FillingExistingFromImbase));
    this.AddMenuItem((MenuItemBase) menuBar, "AVS.VB.FillingExistingObject", "В текущую строку добавить данные из существующего объекта", "В ТЕКУЩУЮ СТРОКУ добавить данные из существующего объекта", false, true, new AVSPluginExecuteCommand(this.FillingExistingFromDBObject));
    this.AddMenuItem((MenuItemBase) menuBar, "AVS.VB.FillingExistingImbaseSelected", "В выделенные строки добавить данные из Imbase", "В выделенные строки добавить данные из Imbase", false, false, new AVSPluginExecuteCommand(this.FillingExistingFromImbaseSelected));
    this.AddMenuItem((MenuItemBase) menuBar, "AVS.VB.FillingExistingObjectSelected", "В выделенные строки добавить данные из существующего объекта", "В выделенные строки добавить данные из существующего объекта", false, false, new AVSPluginExecuteCommand(this.FillingExistingFromDBObjectSelected));
    this.AddMenuItem((MenuItemBase) menuBar, "AVS.VB.RemoveDocRow", "Удалить текущую строку", "Удалить текущую строку", true, true, new AVSPluginExecuteCommand(this.RemoveDocRow));
    this.AddMenuItem((MenuItemBase) menuBar, "AVS.VB.RemoveDocPodRow", "Удалить подстроку", "Удалить текущую подстроку", true, true, new AVSPluginExecuteCommand(this.RemoveDocPodRow));
    this.AddMenuItem((MenuItemBase) menuBar, "AVS.VB.RemovePage_Curr", "Удалить текущую страницу", "Удалить текущую страницу", true, false, new AVSPluginExecuteCommand(this.RemovePage_Curr));
    this.AddMenuItem_WithIcon((MenuItemBase) menuBar, "AVS.VB.FromNewPage", "Текущую запись выводить с новой страницы", "Текущую запись выводить, начиная с новой страницы", str + "Page.png", true, true, new AVSPluginExecuteCommand(this.FromNewPage));
    this.AddMenuItem((MenuItemBase) menuBar, "AVS.VB.DeleteFromNewPage", "Отменить \"С новой страницы\"", "Отменить вывод этой записи с новой страницы", true, true, new AVSPluginExecuteCommand(this.DeleteFromNewPage));
    this.AddMenuItem((MenuItemBase) menuBar, "AVS.VB.Insert_Next_Page", "Добавить страницу после текущей страницы", "Добавить страницу после текущей страницы", true, false, new AVSPluginExecuteCommand(this.Insert_Next_Page));
    this.AddMenuItem((MenuItemBase) menuBar, "AVS.VB.AddTitList", "Добавить \"Титульный лист\"", "Добавить \"Титульный лист\"", false, false, new AVSPluginExecuteCommand(this.AddTitList));
    string ipsVersion = Vedomost_VB_Static.AssemblyAttributes.IPSVersion;
    if (ipsVersion.StartsWith("8") || ipsVersion.StartsWith("9"))
      this.AddMenuItem((MenuItemBase) menuBar, "AVS.VB.Create_LUESPD", "Создать \"Лист утверждения\"", "Создать Лист утверждения на текущую спецификацию", true, false, new AVSPluginExecuteCommand(this.Create_LUESPD));
    this.AddMenuItem((MenuItemBase) menuBar, "AVS.VB.Create_RemarkPage", "Добавить \"Лист примечаний\"", "Добавить лист для примечаний", true, false, new AVSPluginExecuteCommand(this.Create_RemarkPage));
    this.AddMenuItem((MenuItemBase) menuBar, "AVS.VB.AddLRIPage", "Добавить \"Лист регистрации изменений\"", "Добавить \"Лист регистрации изменений\"", false, false, new AVSPluginExecuteCommand(this.AddLRIPage));
    this.AddMenuItem_WithIcon((MenuItemBase) menuBar, "AVS.VB.AddLRIRecord_Before", "Вставить запись в ЛРИ перед", "Вставить запись в лист регистрации изменений перед текущей записью", str + "InsertRowAbove.png", false, true, new AVSPluginExecuteCommand(this.AddLRIRecord_Before));
    this.AddMenuItem_WithIcon((MenuItemBase) menuBar, "AVS.VB.AddLRIRecord_After", "Вставить запись в ЛРИ после", "Вставить запись в лист регистрации изменений  после текущей записи", str + "InsertRowBelow.png", false, true, new AVSPluginExecuteCommand(this.AddLRIRecord_After));
    this.AddMenuItem((MenuItemBase) menuBar, "AVS.VB.DeleteIdenticalTexts", "Удалить одинаковые тексты", "Удалить одинаковые тексты в графах документа", true, false, new AVSPluginExecuteCommand(this.DeleteIdenticalTexts));
    this.AddMenuItem((MenuItemBase) menuBar, "AVS.VB.RecoverIdenticalTexts", "Восстановить одинаковые тексты", "Восстановить одинаковые тексты в графах документа", false, false, new AVSPluginExecuteCommand(this.RecoverIdenticalTexts));
    this.AddMenuItem_WithKeys((MenuItemBase) menuBar, "AVS.VB.Propertias", "Свойства объекта (Карточка)", "Смотреть карточку объекта", true, true, new AVSPluginExecuteCommand(this.Propertias), Keys.F4 | Keys.Shift);
    this.AddMenuItem((MenuItemBase) menuBar, "AVS.VB.EditTemplate", "Открыть редактор шаблона (бланка)", "Открыть редактор шаблона (бланка)", true, false, new AVSPluginExecuteCommand(this.EditTemplate));
    if (Vedomost_VB_Static.isComputerName_Victor || Vedomost_VB_Static.isHozain)
    {
      this.AddMenuItem_WithKeys((MenuItemBase) menuBar, "AVS.VB.AboutDocRow_Function", "Свойства строки", "Свойства строки", true, true, new AVSPluginExecuteCommand(this.AboutDocRow), Keys.F4 | Keys.Alt);
      this.AddMenuItem_WithKeys((MenuItemBase) menuBar, "AVS.VB.AboutDocument_Function", "Свойства документа", "Свойства документа", false, false, new AVSPluginExecuteCommand(this.AboutDocument), Keys.F4 | Keys.Control);
      this.AddMenuItem((MenuItemBase) menuBar, "AVS.VB.Check_Ved_Or_Tabl", "Проверить", "Проверка строк на связи с базой", true, false, new AVSPluginExecuteCommand(this.Check_Ved_Or_Tabl));
      this.AddMenuItem((MenuItemBase) menuBar, "AVS.VB.Experiment", "Эксперимент", "", true, false, new AVSPluginExecuteCommand(this.Experiment));
    }
    this.AddMenuItem((MenuItemBase) menuBar, "AVS.VB.Protection_docRow", "Строке присвоить \"Только для чтения\"", "Строке присвоить \"Только для чтения\"", false, true, new AVSPluginExecuteCommand(this.Protection_docRow));
    this.AddMenuItem((MenuItemBase) menuBar, "AVS.VB.DeProtection_docRow", "Разрешить редактирование строки", "", false, true, new AVSPluginExecuteCommand(this.DeProtection_docRow));
    this.AddMenuItem((MenuItemBase) menuBar, "AVS.VB.Protection_mainTabls", "Содержимому документа присвоить \"Только для чтения\"", "Содержимому текущего документа присвоить \"Только для чтения\"", false, false, new AVSPluginExecuteCommand(this.Protection_mainTabls));
    this.AddMenuItem((MenuItemBase) menuBar, "AVS.VB.DeProtection_mainTabls", "Разрешить редактирование документа", "Разрешить редактирование текущего документа", false, false, new AVSPluginExecuteCommand(this.DeProtection_mainTabls));
  }

  /// <summary>Проверка контекста команды, чтобы определить её допустимость</summary>
  /// <param name_From_Oglavlenie="commandState">Данные команды</param>
  /// <returns>true, если команда найдена и обработана</returns>
  public override bool QueryStatus(ICommandState commandState)
  {
    this._typeRowCurr = Vedomost_VB_Static.TypeRow.Undefined;
    int num = (int) this.CheckTypeDocRow(commandState);
    this._docRowCurrent = this.DocRowCurrent();
    bool flag1 = false;
    if (this._docRowCurrent != null && !string.IsNullOrEmpty(this._docRowCurrent.GetAttributeValue("ReadOnly", false)))
      flag1 = true;
    bool flag2 = false;
    string str = "";
    if (this.Document != null)
      str = this.Document.GetAttributeValue("ReadOnly", false);
    if (!string.IsNullOrEmpty(str))
      flag2 = true;
    this._document_readOnly = flag2;
    this.Check_PageCurrent();
    if (this._typeRowCurr == Vedomost_VB_Static.TypeRow.Main)
      this._typeRowCurr = Vedomost_VB_Static.TypeRow.Main;
    DocumentTreeNode[] selectedNodes = this.DocumentControl.GetSelectedNodes();
    switch (commandState.CommandName)
    {
      case "AVS.AddRecordsVB":
        if (!this.ReadOnly && !this._document_readOnly && this._typeRowCurr != Vedomost_VB_Static.TypeRow.LRI && this.name_Page_Current != "Титульный лист" && this.name_Page_Current != "Лист регистрации изменений" && this._typeDoc != Vedomost_VB.TypeDoc.EspdLU)
        {
          commandState.Visible = true;
          commandState.Enabled = true;
        }
        else
        {
          commandState.Visible = false;
          commandState.Enabled = false;
        }
        return true;
      case "AVS.VB.AboutDocRow_Function":
        if (this._docRowCurrent != null && this._typeRowCurr != Vedomost_VB_Static.TypeRow.Main && this._typeRowCurr != Vedomost_VB_Static.TypeRow.MainTab && this.typePageVedom == Vedomost_VB_Static.TypePageVedom.Info && this._typeRowCurr != Vedomost_VB_Static.TypeRow.Main && (Vedomost_VB_Static.isComputerName_Victor || Vedomost_VB_Static.isHozain))
        {
          commandState.Visible = true;
          commandState.Enabled = true;
        }
        else if (this._typeRowCurr == Vedomost_VB_Static.TypeRow.Dlinaia || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Info)
        {
          commandState.Visible = true;
          commandState.Enabled = true;
        }
        else
        {
          commandState.Visible = false;
          commandState.Enabled = false;
        }
        return true;
      case "AVS.VB.AboutDocument_Function":
        if (Vedomost_VB_Static.isComputerName_Victor || Vedomost_VB_Static.isHozain)
        {
          commandState.Visible = true;
          commandState.Enabled = true;
        }
        else
        {
          commandState.Visible = false;
          commandState.Enabled = false;
        }
        return true;
      case "AVS.VB.AddCopyRow":
        if (!this.ReadOnly && !this._document_readOnly && this._docRowCurrent != null && this._typeRowCurr != Vedomost_VB_Static.TypeRow.Undefined && this._typeRowCurr != Vedomost_VB_Static.TypeRow.MainTab && this._typeRowCurr != Vedomost_VB_Static.TypeRow.LRI && this._typeRowCurr != Vedomost_VB_Static.TypeRow.TextBoxNoEdit)
        {
          commandState.Visible = true;
          commandState.Enabled = true;
        }
        else
        {
          commandState.Visible = false;
          commandState.Enabled = false;
        }
        return true;
      case "AVS.VB.AddLRIPage":
        if (!this.ReadOnly && this._typeDoc != Vedomost_VB.TypeDoc.EspdLU)
        {
          commandState.Visible = true;
          commandState.Enabled = true;
        }
        else
        {
          commandState.Visible = false;
          commandState.Enabled = false;
        }
        return true;
      case "AVS.VB.AddLRIRecord_After":
      case "AVS.VB.AddLRIRecord_Before":
        if (!this.ReadOnly && this._typeRowCurr == Vedomost_VB_Static.TypeRow.LRI && selectedNodes.Length < 2)
        {
          commandState.Visible = true;
          commandState.Enabled = true;
        }
        else
        {
          commandState.Visible = false;
          commandState.Enabled = false;
        }
        return true;
      case "AVS.VB.AddNewKudaVhoditWithDBObject":
        if (!this.ReadOnly && !this._document_readOnly && !flag1 && this._typeRowCurr == Vedomost_VB_Static.TypeRow.KudaVhodit && selectedNodes.Length < 2)
        {
          commandState.Visible = true;
          commandState.Enabled = true;
        }
        else
        {
          commandState.Visible = false;
          commandState.Enabled = false;
        }
        return true;
      case "AVS.VB.AddNewRowWithDBObject":
        if (!this.ReadOnly && !this._document_readOnly && this.typePageVedom != Vedomost_VB_Static.TypePageVedom.REMARK && (this._typeRowCurr == Vedomost_VB_Static.TypeRow.Info || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Empty || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Zagolovok || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Additional1 || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Additional2 || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Additional3 || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Additional4 || this._typeRowCurr == Vedomost_VB_Static.TypeRow.TitlePart || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Podzagolovok || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Dlinaia || this._typeRowCurr == Vedomost_VB_Static.TypeRow.RemarkShort || this._typeRowCurr == Vedomost_VB_Static.TypeRow.EmptyBezOtryva || this._typeRowCurr == Vedomost_VB_Static.TypeRow.ZagolovokBezPustoi || this._typeRowCurr == Vedomost_VB_Static.TypeRow.ZagolovikNeZhirny || this._typeRowCurr == Vedomost_VB_Static.TypeRow.MainTab) && selectedNodes.Length < 2)
        {
          commandState.Visible = true;
          commandState.Enabled = true;
        }
        else
        {
          commandState.Visible = false;
          commandState.Enabled = false;
        }
        return true;
      case "AVS.VB.AddPage_ToEndDoc":
        if (this.ReadOnly || this._document_readOnly || this._typeRowCurr == Vedomost_VB_Static.TypeRow.KudaVhodit || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Itogo)
        {
          commandState.Visible = false;
          commandState.Enabled = false;
        }
        else
        {
          commandState.Visible = true;
          commandState.Enabled = true;
        }
        return true;
      case "AVS.VB.AddRowAdditional1":
        if (!this.ReadOnly && !this._document_readOnly && this._docRowCurrent != null && (this._typeRowCurr == Vedomost_VB_Static.TypeRow.Info || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Empty || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Zagolovok || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Additional1 || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Additional2 || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Additional3 || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Additional4 || this._typeRowCurr == Vedomost_VB_Static.TypeRow.TitlePart || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Podzagolovok || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Dlinaia || this._typeRowCurr == Vedomost_VB_Static.TypeRow.RemarkShort || this._typeRowCurr == Vedomost_VB_Static.TypeRow.EmptyBezOtryva || this._typeRowCurr == Vedomost_VB_Static.TypeRow.ZagolovokBezPustoi || this._typeRowCurr == Vedomost_VB_Static.TypeRow.ZagolovikNeZhirny || this._typeRowCurr == Vedomost_VB_Static.TypeRow.MainTab) && selectedNodes.Length < 2)
        {
          if (this._vedomost_VB_new != null && this._one_Ved_Nastr_Curr._algorithmToPrint != null && this._one_Ved_Nastr_Curr._algorithmToPrint._additional1 == 1)
          {
            commandState.Visible = true;
            commandState.Enabled = true;
          }
          else
          {
            commandState.Visible = false;
            commandState.Enabled = false;
          }
        }
        else
        {
          commandState.Visible = false;
          commandState.Enabled = false;
        }
        return true;
      case "AVS.VB.AddRowAdditional2":
        if (!this.ReadOnly && !this._document_readOnly && this._docRowCurrent != null && (this._typeRowCurr == Vedomost_VB_Static.TypeRow.Info || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Empty || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Zagolovok || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Additional1 || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Additional2 || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Additional3 || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Additional4 || this._typeRowCurr == Vedomost_VB_Static.TypeRow.TitlePart || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Podzagolovok || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Dlinaia || this._typeRowCurr == Vedomost_VB_Static.TypeRow.RemarkShort || this._typeRowCurr == Vedomost_VB_Static.TypeRow.EmptyBezOtryva || this._typeRowCurr == Vedomost_VB_Static.TypeRow.ZagolovokBezPustoi || this._typeRowCurr == Vedomost_VB_Static.TypeRow.ZagolovikNeZhirny || this._typeRowCurr == Vedomost_VB_Static.TypeRow.MainTab) && selectedNodes.Length < 2)
        {
          if (this._vedomost_VB_new != null && this._one_Ved_Nastr_Curr._algorithmToPrint != null && this._one_Ved_Nastr_Curr._algorithmToPrint._additional2 == 1)
          {
            commandState.Visible = true;
            commandState.Enabled = true;
          }
          else
          {
            commandState.Visible = false;
            commandState.Enabled = false;
          }
        }
        else
        {
          commandState.Visible = false;
          commandState.Enabled = false;
        }
        return true;
      case "AVS.VB.AddRowAdditional3":
        if (!this.ReadOnly && !this._document_readOnly && this._docRowCurrent != null && (this._typeRowCurr == Vedomost_VB_Static.TypeRow.Info || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Empty || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Zagolovok || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Additional1 || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Additional2 || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Additional3 || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Additional4 || this._typeRowCurr == Vedomost_VB_Static.TypeRow.TitlePart || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Podzagolovok || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Dlinaia || this._typeRowCurr == Vedomost_VB_Static.TypeRow.RemarkShort || this._typeRowCurr == Vedomost_VB_Static.TypeRow.EmptyBezOtryva || this._typeRowCurr == Vedomost_VB_Static.TypeRow.ZagolovokBezPustoi || this._typeRowCurr == Vedomost_VB_Static.TypeRow.ZagolovikNeZhirny || this._typeRowCurr == Vedomost_VB_Static.TypeRow.MainTab) && selectedNodes.Length < 2)
        {
          if (this._vedomost_VB_new != null && this._one_Ved_Nastr_Curr._algorithmToPrint._oneRecordToPrintAdditional3 != null && this._one_Ved_Nastr_Curr._algorithmToPrint._additional3 == 1)
          {
            commandState.Visible = true;
            commandState.Enabled = true;
          }
          else
          {
            commandState.Visible = false;
            commandState.Enabled = false;
          }
        }
        else
        {
          commandState.Visible = false;
          commandState.Enabled = false;
        }
        return true;
      case "AVS.VB.AddRowAdditional4":
        if (!this.ReadOnly && !this._document_readOnly && this._docRowCurrent != null && (this._typeRowCurr == Vedomost_VB_Static.TypeRow.Info || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Empty || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Zagolovok || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Additional1 || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Additional2 || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Additional3 || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Additional4 || this._typeRowCurr == Vedomost_VB_Static.TypeRow.TitlePart || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Podzagolovok || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Dlinaia || this._typeRowCurr == Vedomost_VB_Static.TypeRow.RemarkShort || this._typeRowCurr == Vedomost_VB_Static.TypeRow.EmptyBezOtryva || this._typeRowCurr == Vedomost_VB_Static.TypeRow.ZagolovokBezPustoi || this._typeRowCurr == Vedomost_VB_Static.TypeRow.ZagolovikNeZhirny || this._typeRowCurr == Vedomost_VB_Static.TypeRow.MainTab) && selectedNodes.Length < 2)
        {
          if (this._vedomost_VB_new != null && this._one_Ved_Nastr_Curr._algorithmToPrint._oneRecordToPrintAdditional4 != null && this._one_Ved_Nastr_Curr._algorithmToPrint._additional4 == 1)
          {
            commandState.Visible = true;
            commandState.Enabled = true;
          }
          else
          {
            commandState.Visible = false;
            commandState.Enabled = false;
          }
        }
        else
        {
          commandState.Visible = false;
          commandState.Enabled = false;
        }
        return true;
      case "AVS.VB.AddRowEmpty":
      case "AVS.VB.AddRowInfo":
        if (!this.ReadOnly && !this._document_readOnly && this._docRowCurrent != null && (this._typeRowCurr == Vedomost_VB_Static.TypeRow.Info || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Empty || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Zagolovok || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Additional1 || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Additional2 || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Additional3 || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Additional4 || this._typeRowCurr == Vedomost_VB_Static.TypeRow.TitlePart || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Podzagolovok || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Dlinaia || this._typeRowCurr == Vedomost_VB_Static.TypeRow.RemarkShort || this._typeRowCurr == Vedomost_VB_Static.TypeRow.EmptyBezOtryva || this._typeRowCurr == Vedomost_VB_Static.TypeRow.ZagolovokBezPustoi || this._typeRowCurr == Vedomost_VB_Static.TypeRow.ZagolovikNeZhirny || this._typeRowCurr == Vedomost_VB_Static.TypeRow.MainTab) && selectedNodes.Length < 2)
        {
          commandState.Visible = true;
          commandState.Enabled = true;
        }
        else
        {
          commandState.Visible = false;
          commandState.Enabled = false;
        }
        return true;
      case "AVS.VB.AddRowFromImbase":
      case "AVS.VB.AddRowGroupFromImbase":
        if (!this.ReadOnly && !this._document_readOnly && this._typeDoc != Vedomost_VB.TypeDoc.Espd && this._typeDoc != Vedomost_VB.TypeDoc.EspdLU && (this._typeRowCurr == Vedomost_VB_Static.TypeRow.Info || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Empty || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Zagolovok || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Additional1 || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Additional2 || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Additional3 || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Additional4 || this._typeRowCurr == Vedomost_VB_Static.TypeRow.TitlePart || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Podzagolovok || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Dlinaia || this._typeRowCurr == Vedomost_VB_Static.TypeRow.RemarkShort || this._typeRowCurr == Vedomost_VB_Static.TypeRow.EmptyBezOtryva || this._typeRowCurr == Vedomost_VB_Static.TypeRow.ZagolovokBezPustoi || this._typeRowCurr == Vedomost_VB_Static.TypeRow.ZagolovikNeZhirny || this._typeRowCurr == Vedomost_VB_Static.TypeRow.MainTab) && selectedNodes.Length < 2)
        {
          commandState.Visible = true;
          commandState.Enabled = true;
        }
        else
        {
          commandState.Visible = false;
          commandState.Enabled = false;
        }
        return true;
      case "AVS.VB.AddRowItogo":
        if (!this.ReadOnly && !this._document_readOnly && this._typeRowCurr == Vedomost_VB_Static.TypeRow.KudaVhodit && this._kudaVhodit_Skolko > 1 && this._kudaVhodit_Index == this._kudaVhodit_Skolko - 1 && !this._isItogo && selectedNodes.Length < 2)
        {
          commandState.Visible = true;
          commandState.Enabled = true;
        }
        else
        {
          commandState.Visible = false;
          commandState.Enabled = false;
        }
        return true;
      case "AVS.VB.AddRowKudaVhodit":
        if (!this.ReadOnly && !this._document_readOnly && !flag1 && this._typeRowCurr == Vedomost_VB_Static.TypeRow.KudaVhodit && selectedNodes.Length < 2)
        {
          commandState.Visible = true;
          commandState.Enabled = true;
          this.isAddRowKudaVhodit = true;
        }
        else
        {
          commandState.Visible = false;
          commandState.Enabled = false;
          this.isAddRowKudaVhodit = false;
        }
        return true;
      case "AVS.VB.AddRowPodZagolovok":
        if (!this.ReadOnly && !this._document_readOnly && this._docRowCurrent != null && (this._typeRowCurr == Vedomost_VB_Static.TypeRow.Info || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Empty || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Zagolovok || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Additional1 || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Additional2 || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Additional3 || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Additional4 || this._typeRowCurr == Vedomost_VB_Static.TypeRow.TitlePart || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Podzagolovok || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Dlinaia || this._typeRowCurr == Vedomost_VB_Static.TypeRow.RemarkShort || this._typeRowCurr == Vedomost_VB_Static.TypeRow.EmptyBezOtryva || this._typeRowCurr == Vedomost_VB_Static.TypeRow.ZagolovokBezPustoi || this._typeRowCurr == Vedomost_VB_Static.TypeRow.ZagolovikNeZhirny || this._typeRowCurr == Vedomost_VB_Static.TypeRow.MainTab) && selectedNodes.Length < 2)
        {
          if (this._vedomost_VB_new != null && this._one_Ved_Nastr_Curr != null && this._one_Ved_Nastr_Curr._algorithmToPrint._oneRecordToPrintTitlePodSection != null)
          {
            commandState.Visible = true;
            commandState.Enabled = true;
          }
          else
          {
            commandState.Visible = false;
            commandState.Enabled = false;
          }
        }
        else
        {
          commandState.Visible = false;
          commandState.Enabled = false;
        }
        return true;
      case "AVS.VB.AddRowRemark":
        if (!this.ReadOnly && !this._document_readOnly && this._docRowCurrent != null && (this._typeRowCurr == Vedomost_VB_Static.TypeRow.Info || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Empty || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Zagolovok || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Additional1 || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Additional2 || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Additional3 || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Additional4 || this._typeRowCurr == Vedomost_VB_Static.TypeRow.TitlePart || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Podzagolovok || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Dlinaia || this._typeRowCurr == Vedomost_VB_Static.TypeRow.RemarkShort || this._typeRowCurr == Vedomost_VB_Static.TypeRow.EmptyBezOtryva || this._typeRowCurr == Vedomost_VB_Static.TypeRow.ZagolovokBezPustoi || this._typeRowCurr == Vedomost_VB_Static.TypeRow.ZagolovikNeZhirny || this._typeRowCurr == Vedomost_VB_Static.TypeRow.MainTab) && selectedNodes.Length < 2)
        {
          commandState.Visible = true;
          commandState.Enabled = true;
        }
        else
        {
          commandState.Visible = false;
          commandState.Enabled = false;
        }
        return true;
      case "AVS.VB.AddRowRemarkShort":
        if (!this.ReadOnly && !this._document_readOnly && this._docRowCurrent != null && (this._typeRowCurr == Vedomost_VB_Static.TypeRow.Info || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Empty || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Zagolovok || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Additional1 || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Additional2 || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Additional3 || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Additional4 || this._typeRowCurr == Vedomost_VB_Static.TypeRow.TitlePart || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Podzagolovok || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Dlinaia || this._typeRowCurr == Vedomost_VB_Static.TypeRow.RemarkShort || this._typeRowCurr == Vedomost_VB_Static.TypeRow.EmptyBezOtryva || this._typeRowCurr == Vedomost_VB_Static.TypeRow.ZagolovokBezPustoi || this._typeRowCurr == Vedomost_VB_Static.TypeRow.ZagolovikNeZhirny || this._typeRowCurr == Vedomost_VB_Static.TypeRow.MainTab) && selectedNodes.Length < 2)
        {
          commandState.Visible = true;
          commandState.Enabled = true;
        }
        else
        {
          commandState.Visible = false;
          commandState.Enabled = false;
        }
        return true;
      case "AVS.VB.AddRowTitlePart":
        if (!this.ReadOnly && !this._document_readOnly && this._docRowCurrent != null && (this._typeRowCurr == Vedomost_VB_Static.TypeRow.Info || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Empty || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Zagolovok || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Additional1 || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Additional2 || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Additional3 || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Additional4 || this._typeRowCurr == Vedomost_VB_Static.TypeRow.TitlePart || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Podzagolovok || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Dlinaia || this._typeRowCurr == Vedomost_VB_Static.TypeRow.RemarkShort || this._typeRowCurr == Vedomost_VB_Static.TypeRow.EmptyBezOtryva || this._typeRowCurr == Vedomost_VB_Static.TypeRow.ZagolovokBezPustoi || this._typeRowCurr == Vedomost_VB_Static.TypeRow.ZagolovikNeZhirny || this._typeRowCurr == Vedomost_VB_Static.TypeRow.MainTab) && selectedNodes.Length < 2)
        {
          if (this._vedomost_VB_new != null && this._one_Ved_Nastr_Curr != null && this._one_Ved_Nastr_Curr._algorithmToPrint._oneRecordToPrintTitlePart != null)
          {
            commandState.Visible = true;
            commandState.Enabled = true;
          }
          else
          {
            commandState.Visible = false;
            commandState.Enabled = false;
          }
        }
        else
        {
          commandState.Visible = false;
          commandState.Enabled = false;
        }
        return true;
      case "AVS.VB.AddRowZagolovok":
        if (!this.ReadOnly && !this._document_readOnly && this._docRowCurrent != null && (this._typeRowCurr == Vedomost_VB_Static.TypeRow.Info || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Empty || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Zagolovok || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Additional1 || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Additional2 || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Additional3 || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Additional4 || this._typeRowCurr == Vedomost_VB_Static.TypeRow.TitlePart || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Podzagolovok || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Dlinaia || this._typeRowCurr == Vedomost_VB_Static.TypeRow.RemarkShort || this._typeRowCurr == Vedomost_VB_Static.TypeRow.EmptyBezOtryva || this._typeRowCurr == Vedomost_VB_Static.TypeRow.ZagolovokBezPustoi || this._typeRowCurr == Vedomost_VB_Static.TypeRow.ZagolovikNeZhirny || this._typeRowCurr == Vedomost_VB_Static.TypeRow.MainTab) && selectedNodes.Length < 2)
        {
          commandState.Visible = true;
          commandState.Enabled = true;
        }
        else
        {
          commandState.Visible = false;
          commandState.Enabled = false;
        }
        return true;
      case "AVS.VB.AddTitList":
        if (this.ReadOnly)
        {
          commandState.Visible = false;
          commandState.Enabled = false;
        }
        else if (this.n_TitList_In_Template > -1 && !this.is_TitList_In_Document)
        {
          commandState.Visible = true;
          commandState.Enabled = true;
        }
        else
        {
          commandState.Visible = true;
          commandState.Enabled = false;
        }
        return true;
      case "AVS.VB.AddVariableToDocument_Old":
      case "AVS.VB.ListVariableToDocument":
        if (this.ReadOnly || this._formaGroup_Doc == "" || this._formaGroup_Doc == "Ed" || this.metodCreate.StartsWith("Auto") || this._document_readOnly)
        {
          commandState.Visible = false;
          commandState.Enabled = false;
        }
        else
        {
          commandState.Visible = true;
          commandState.Enabled = true;
        }
        return true;
      case "AVS.VB.AddVedRow_Additional1":
      case "AVS.VB.AddVedRow_Additional2":
      case "AVS.VB.AddVedRow_Additional3":
      case "AVS.VB.AddVedRow_Additional4":
      case "AVS.VB.AddVedRow_Empty":
      case "AVS.VB.AddVedRow_PodZagolovok":
      case "AVS.VB.AddVedRow_Remark":
      case "AVS.VB.AddVedRow_RemarkShort":
      case "AVS.VB.AddVedRow_TitlePart":
      case "AVS.VB.AddVedRow_Zagolovok":
        if (this._typeDoc != Vedomost_VB.TypeDoc.Ved || this._typeDoc == Vedomost_VB.TypeDoc.EspdLU || this.typePageVedom != Vedomost_VB_Static.TypePageVedom.Info || this.ReadOnly || this._document_readOnly)
        {
          commandState.Visible = false;
          commandState.Enabled = false;
        }
        else
        {
          commandState.Visible = true;
          commandState.Enabled = true;
        }
        return true;
      case "AVS.VB.ChangeTyp":
        if (!this.ReadOnly && !this._document_readOnly && this.list_Ims_of_this_type != null && this.list_Ims_of_this_type.Count > 0 && !this.is_TitList_In_Document && !this.is_LIZM_In_Document && this._typeDoc != Vedomost_VB.TypeDoc.EspdLU)
        {
          commandState.Visible = true;
          commandState.Enabled = true;
        }
        else
        {
          commandState.Visible = false;
          commandState.Enabled = false;
        }
        return true;
      case "AVS.VB.CreateListZagol":
        if (!this.ReadOnly && !this._document_readOnly && this._vedomost_VB_new != null && this._one_Ved_Nastr_Curr != null && this._one_Ved_Nastr_Curr._typeVed == Vedomost_VB.TypeVed.VP && !this.isListZagolovki)
        {
          commandState.Visible = true;
          commandState.Enabled = true;
        }
        else
        {
          commandState.Visible = false;
          commandState.Enabled = false;
        }
        return true;
      case "AVS.VB.CreateRazdel":
        if (!this.ReadOnly && this._typeRowCurr != Vedomost_VB_Static.TypeRow.Undefined && this._typeRowCurr != Vedomost_VB_Static.TypeRow.Main && !this._document_readOnly && this._one_Ved_Nastr_Curr._list_RazdelsVed != null && this._one_Ved_Nastr_Curr._list_RazdelsVed.Count > 1)
        {
          commandState.Visible = true;
          commandState.Enabled = true;
        }
        else
        {
          commandState.Visible = false;
          commandState.Enabled = false;
        }
        return true;
      case "AVS.VB.CreateXmlFile_FromDocument":
        if (this._typeDoc != Vedomost_VB.TypeDoc.EspdLU)
        {
          commandState.Visible = true;
          commandState.Enabled = true;
        }
        else
        {
          commandState.Visible = false;
          commandState.Enabled = false;
        }
        return true;
      case "AVS.VB.Create_LUESPD":
        if (this._typeDoc == Vedomost_VB.TypeDoc.Espd && !this.isLU)
        {
          commandState.Visible = true;
          commandState.Enabled = true;
        }
        else
        {
          commandState.Visible = false;
          commandState.Enabled = false;
        }
        return true;
      case "AVS.VB.Create_RemarkPage":
        if (!this.ReadOnly && this._typeDoc == Vedomost_VB.TypeDoc.Espd && !this.is_RemarkPage_In_Document)
        {
          commandState.Visible = true;
          commandState.Enabled = true;
        }
        else
        {
          commandState.Visible = false;
          commandState.Enabled = false;
        }
        return true;
      case "AVS.VB.DeProtection_docRow":
        if (((this.ReadOnly || this._document_readOnly ? 0 : (this._docRowCurrent != null ? 1 : 0)) & (flag1 ? 1 : 0)) != 0 && this._one_Ved_Nastr_Curr._protection_From_Editing._isProtectionCommand)
        {
          commandState.Visible = true;
          commandState.Enabled = true;
        }
        else
        {
          commandState.Visible = false;
          commandState.Enabled = false;
        }
        return true;
      case "AVS.VB.DeProtection_mainTabls":
        if (!this.ReadOnly & flag2 && this._one_Ved_Nastr_Curr._protection_From_Editing._isProtectionCommand)
        {
          commandState.Visible = true;
          commandState.Enabled = true;
        }
        else
        {
          commandState.Visible = false;
          commandState.Enabled = false;
        }
        return true;
      case "AVS.VB.DeleteFromNewPage":
        if (this.ReadOnly || this._document_readOnly || this._docRowCurrent == null && selectedNodes.Length < 2)
        {
          commandState.Visible = false;
          commandState.Enabled = false;
          return true;
        }
        if (this._docRowCurrent != null && this._docRowCurrent.FromNewPage)
        {
          commandState.Visible = true;
          commandState.Enabled = true;
        }
        else
        {
          commandState.Visible = false;
          commandState.Enabled = false;
        }
        return true;
      case "AVS.VB.DeleteIdenticalTexts":
        if (!this.ReadOnly && !this._document_readOnly && this._typeDoc != Vedomost_VB.TypeDoc.Espd && this._typeDoc != Vedomost_VB.TypeDoc.EspdLU)
        {
          commandState.Visible = true;
          commandState.Enabled = true;
        }
        else
        {
          commandState.Visible = false;
          commandState.Enabled = false;
        }
        return true;
      case "AVS.VB.DeleteListZagol":
        if (!this.ReadOnly && !this._document_readOnly && this._vedomost_VB_new != null && this._one_Ved_Nastr_Curr != null && this._one_Ved_Nastr_Curr._typeVed == Vedomost_VB.TypeVed.VP && this.isListZagolovki)
        {
          commandState.Visible = true;
          commandState.Enabled = true;
        }
        else
        {
          commandState.Visible = false;
          commandState.Enabled = false;
        }
        return true;
      case "AVS.VB.DocRowDown":
      case "AVS.VB.DocRowUp":
        if (!this.ReadOnly && !this._document_readOnly && this._docRowCurrent != null && this._typeRowCurr != Vedomost_VB_Static.TypeRow.Undefined && this._typeRowCurr != Vedomost_VB_Static.TypeRow.MainTab && this._typeRowCurr != Vedomost_VB_Static.TypeRow.LRI && this._typeRowCurr != Vedomost_VB_Static.TypeRow.TextBoxNoEdit)
        {
          commandState.Visible = true;
          commandState.Enabled = true;
        }
        else
        {
          commandState.Visible = false;
          commandState.Enabled = false;
        }
        return true;
      case "AVS.VB.EditGroupForm":
        if (this.ReadOnly || this._document_readOnly || this._typeDoc == Vedomost_VB.TypeDoc.Espd || this._typeDoc == Vedomost_VB.TypeDoc.EspdLU)
        {
          commandState.Visible = false;
          commandState.Enabled = false;
        }
        else if (this.metodCreate.StartsWith("Auto"))
        {
          if (this._formaGroup_Doc == "Ed" || this._formaGroup_Doc == "")
          {
            commandState.Visible = false;
            commandState.Enabled = false;
          }
          else if (this._one_Ved_Nastr_Curr._typeVed == Vedomost_VB.TypeVed.VS || this._one_Ved_Nastr_Curr._typeVed == Vedomost_VB.TypeVed.VP)
          {
            commandState.Visible = true;
            commandState.Enabled = true;
          }
          else
          {
            commandState.Visible = false;
            commandState.Enabled = false;
          }
        }
        else
        {
          commandState.Visible = true;
          commandState.Enabled = true;
        }
        return true;
      case "AVS.VB.EditTemplate":
        commandState.Visible = true;
        commandState.Enabled = true;
        return true;
      case "AVS.VB.FilledNumbersIspolneniy":
        if (!this.ReadOnly && !this._document_readOnly && this._vedomost_VB_new != null && this._formaGroup_Doc == "B" && this.variablesCount > this._one_Ved_Nastr_Curr._algorithmToPrint._kolGraf)
        {
          commandState.Visible = true;
          commandState.Enabled = true;
        }
        else
        {
          commandState.Visible = false;
          commandState.Enabled = false;
        }
        return true;
      case "AVS.VB.Filled_Data_From_XmlFile_To_Document":
        if (this.ReadOnly || this._document_readOnly || this._typeDoc == Vedomost_VB.TypeDoc.EspdLU)
        {
          commandState.Visible = false;
          commandState.Enabled = false;
        }
        else if (this.metodCreate.StartsWith("Auto"))
        {
          commandState.Visible = false;
          commandState.Enabled = false;
        }
        else
        {
          commandState.Visible = true;
          commandState.Enabled = true;
        }
        return true;
      case "AVS.VB.FillingExistingImbase":
      case "AVS.VB.FillingExistingObject":
        if (!this.ReadOnly && !this._document_readOnly && this._typeRowCurr == Vedomost_VB_Static.TypeRow.Info && selectedNodes.Length < 2)
        {
          commandState.Visible = true;
          commandState.Enabled = true;
        }
        else
        {
          commandState.Visible = false;
          commandState.Enabled = false;
        }
        return true;
      case "AVS.VB.FillingExistingImbaseSelected":
      case "AVS.VB.FillingExistingObjectSelected":
        if (!this.ReadOnly && !this._document_readOnly && this._typeRowCurr == Vedomost_VB_Static.TypeRow.Info && selectedNodes.Length > 1)
        {
          commandState.Visible = true;
          commandState.Enabled = true;
        }
        else
        {
          commandState.Visible = false;
          commandState.Enabled = false;
        }
        return true;
      case "AVS.VB.FromNewPage":
        if (this.ReadOnly || this._document_readOnly || this._docRowCurrent == null || this.typePageVedom == Vedomost_VB_Static.TypePageVedom.REMARK && selectedNodes.Length < 2)
        {
          commandState.Visible = false;
          commandState.Enabled = false;
          return true;
        }
        if ((this._typeRowCurr == Vedomost_VB_Static.TypeRow.Info || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Dlinaia || this._typeRowCurr == Vedomost_VB_Static.TypeRow.RemarkShort || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Empty || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Zagolovok) && this._docRowCurrent != null && selectedNodes.Length < 2)
        {
          commandState.Visible = true;
          commandState.Enabled = true;
        }
        else
        {
          commandState.Visible = false;
          commandState.Enabled = false;
        }
        return true;
      case "AVS.VB.Insert_Next_Page":
        if (this.name_Page_Next == "Заглавный лист")
        {
          commandState.Visible = false;
          commandState.Enabled = false;
        }
        else if (this.ReadOnly || this._document_readOnly)
        {
          commandState.Visible = false;
          commandState.Enabled = false;
        }
        else
        {
          commandState.Visible = true;
          commandState.Enabled = true;
        }
        return true;
      case "AVS.VB.Propertias":
        if ((this._typeRowCurr == Vedomost_VB_Static.TypeRow.Info || this._typeRowCurr == Vedomost_VB_Static.TypeRow.KudaVhodit) && selectedNodes.Length < 2)
        {
          commandState.Visible = true;
          commandState.Enabled = true;
        }
        else
        {
          commandState.Visible = false;
          commandState.Enabled = false;
        }
        return true;
      case "AVS.VB.Protection_docRow":
        if (!this.ReadOnly && !this._document_readOnly && this._docRowCurrent != null && !flag1 && this._one_Ved_Nastr_Curr._protection_From_Editing._isProtectionCommand)
        {
          commandState.Visible = true;
          commandState.Enabled = true;
        }
        else
        {
          commandState.Visible = false;
          commandState.Enabled = false;
        }
        return true;
      case "AVS.VB.Protection_mainTabls":
        if (!this.ReadOnly && !flag2 && this._one_Ved_Nastr_Curr._protection_From_Editing._isProtectionCommand)
        {
          commandState.Visible = true;
          commandState.Enabled = true;
        }
        else
        {
          commandState.Visible = false;
          commandState.Enabled = false;
        }
        return true;
      case "AVS.VB.ReCreate":
        if (!this.ReadOnly && this.Check_ReCreate_Mozno() && this._typeDoc != Vedomost_VB.TypeDoc.EspdLU)
        {
          commandState.Visible = true;
          commandState.Enabled = true;
        }
        else
        {
          commandState.Visible = false;
          commandState.Enabled = false;
        }
        return true;
      case "AVS.VB.ReDrawing":
        if (!this.ReadOnly && this._typeDoc != Vedomost_VB.TypeDoc.EspdLU)
        {
          commandState.Visible = true;
          commandState.Enabled = true;
        }
        else
        {
          commandState.Visible = false;
          commandState.Enabled = false;
        }
        return true;
      case "AVS.VB.RecoverIdenticalTexts":
        if (!this.ReadOnly && !this._document_readOnly && !string.IsNullOrEmpty(this.isDeleteIdenticalTexts))
        {
          commandState.Visible = true;
          commandState.Enabled = true;
        }
        else
        {
          commandState.Visible = false;
          commandState.Enabled = false;
        }
        return true;
      case "AVS.VB.RemoveDocPodRow":
        if (!this.ReadOnly && !this._document_readOnly && !flag1 && (this._typeRowCurr == Vedomost_VB_Static.TypeRow.KudaVhodit && this._kudaVhodit_Skolko > 1 || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Itogo) && selectedNodes.Length < 2)
        {
          commandState.Visible = true;
          commandState.Enabled = true;
        }
        else
        {
          commandState.Visible = false;
          commandState.Enabled = false;
        }
        return true;
      case "AVS.VB.RemoveDocRow":
        if (!this.ReadOnly && !this._document_readOnly && this._docRowCurrent != null && this._typeRowCurr != Vedomost_VB_Static.TypeRow.Undefined && this._typeRowCurr != Vedomost_VB_Static.TypeRow.MainTab && this._typeRowCurr != Vedomost_VB_Static.TypeRow.LRI && this._typeRowCurr != Vedomost_VB_Static.TypeRow.TextBoxNoEdit)
        {
          commandState.Visible = true;
          commandState.Enabled = true;
        }
        else
        {
          commandState.Visible = false;
          commandState.Enabled = false;
        }
        return true;
      case "AVS.VB.RemovePage_Curr":
        if (!this.ReadOnly && !this._document_readOnly && this.Document != null && this.Document.NodesCount > 1 && this.name_Page_Current != "Заглавный лист")
        {
          commandState.Visible = true;
          commandState.Enabled = true;
        }
        else
        {
          commandState.Visible = false;
          commandState.Enabled = false;
        }
        return true;
      case "AVS.VB.SortingDoc":
        if (!this.ReadOnly && !this._document_readOnly && this._typeDoc == Vedomost_VB.TypeDoc.Espd && Vedomost_VB_Static.isOnEspd)
        {
          commandState.Visible = true;
          commandState.Enabled = true;
        }
        else
        {
          commandState.Visible = false;
          commandState.Enabled = false;
        }
        return true;
      case "AVS.VB.UpdateListZagol":
        if (!this.ReadOnly && !this._document_readOnly && this._vedomost_VB_new != null && this._one_Ved_Nastr_Curr != null && this._one_Ved_Nastr_Curr._typeVed == Vedomost_VB.TypeVed.VP && this.isListZagolovki)
        {
          commandState.Visible = true;
          commandState.Enabled = true;
        }
        else
        {
          commandState.Visible = false;
          commandState.Enabled = false;
        }
        return true;
      case "Copy":
      case "Cut":
      case "Delete":
      case "Paste":
        if (!this.ReadOnly && !this._document_readOnly)
        {
          commandState.Visible = true;
          commandState.Enabled = true;
        }
        else
        {
          commandState.Visible = false;
          commandState.Enabled = false;
        }
        return true;
      case "FormatMenuItem":
        commandState.Visible = false;
        return true;
      default:
        if (!AVSPlugin.Instance.VedomostEditorVBMenu.ContainsKey(commandState.CommandName))
          return base.QueryStatus(commandState);
        commandState.Visible = !this.ReadOnly;
        commandState.Enabled = true;
        return true;
    }
  }

  /// <summary> Команда Создать список заголовков (Оглавление) </summary>
  /// <param name_From_Oglavlenie="commandState"></param>
  /// <returns></returns>
  private bool CreateListZagol(ICommandState commandState)
  {
    if (AVSPlugin.Instance.ActiveImDocumentEditorForm == null)
      return false;
    ImDocument document = AVSPlugin.Instance.ActiveImDocumentEditorForm.Document;
    if (document == null || document.NodesCount < 1)
      return false;
    this.Update_ListZagolovki();
    return true;
  }

  /// <summary> Удаление списка заголовков </summary>
  /// <param name="commandState"></param>
  /// <returns></returns>
  private bool DeleteListZagol(ICommandState commandState)
  {
    if (AVSPlugin.Instance.ActiveImDocumentEditorForm == null)
      return false;
    ImDocument document = AVSPlugin.Instance.ActiveImDocumentEditorForm.Document;
    if (document == null || document.NodesCount < 1)
      return false;
    this.Delete_ListZagolovki();
    this.isListZagolovki = false;
    return true;
  }

  /// <summary> Вывод расположения списка исполнений формы Б </summary>
  /// <param name="commandState"></param>
  /// <returns></returns>
  private bool FilledNumbersIspolneniy(ICommandState commandState)
  {
    Vedomost_VB_Static.FilledNumbersIspolneniyFormaB(this.Document);
    return true;
  }

  /// <summary> Пересоздание списка заголовков </summary>
  private void Update_ListZagolovki()
  {
    this.startPageNumber = this.Document.StartPageNumber;
    this.isListZagolovki = false;
    int nodesCount = this.Document.NodesCount;
    this.Delete_Zagolovki_FromDocument(this.Document);
    List<Vedomost_VB.RecordForVed_New> zagolStep1OnlyNames = this.CreateListZagol_Step1_Only_Names(this.Document);
    if (zagolStep1OnlyNames == null)
      return;
    this.Add_Zagolovki_ToDocument(this.Document, zagolStep1OnlyNames);
    this.Filled_NumberPages(this.Document, zagolStep1OnlyNames);
    this.NumbersListToOglavlenie(this.Document, zagolStep1OnlyNames);
    this.isListZagolovki = true;
    if (!(this._formaGroup_Doc == "B"))
      return;
    this.Filled_Tabl_Isp_B(nodesCount);
  }

  /// <summary> Удаление старого списка заголовков </summary>
  private void Delete_ListZagolovki()
  {
    this.startPageNumber = this.Document.StartPageNumber;
    this.Delete_Zagolovki_FromDocument(this.Document);
  }

  private bool CreateRazdel(ICommandState commandState)
  {
    VyborRazdela vyborRazdela = new VyborRazdela();
    vyborRazdela._one_Ved_Nastr = this._one_Ved_Nastr_Curr;
    if (vyborRazdela.ShowDialog() != DialogResult.OK)
      return false;
    Vedomost_VB.OneRazdelVed oneRazdelVed = this._one_Ved_Nastr_Curr._list_RazdelsVed[vyborRazdela.ListZagolovkov.SelectedIndex];
    Vedomost_VB.RecordForVed_New recordForVed = new Vedomost_VB.RecordForVed_New();
    recordForVed.TypeRec = Vedomost_VB.TypeRec.Title;
    recordForVed.Set_Name(oneRazdelVed._name);
    TableData docRowZagolovok = this._vedomost_VB_new.Create_DocRow_Zagolovok(this._docTemplate, recordForVed, this._variableRowCurrent, this.name_Page_Current);
    string attributeValue1 = oneRazdelVed._razdelVed.ToString();
    if (docRowZagolovok != null)
    {
      docRowZagolovok.SetAttributeValue("Razdel_Ved", attributeValue1);
      if (this.DocRowCurrent() != null)
      {
        string attributeValue2 = this.Razdel_Ved_Curr();
        if (string.IsNullOrEmpty(attributeValue2) || attributeValue2 == "0" || attributeValue2 == "-1")
          attributeValue2 = "1";
        TableData docRowEmpty = this._vedomost_VB_new.Create_DocRow_Empty(this._docTemplate, this._variableRowCurrent, this.name_Page_Current);
        docRowEmpty.SetAttributeValue("Razdel_Ved", attributeValue2);
        this._vedomost_VB_new.Add_AttributeTypeRec_To_DocRow(docRowEmpty, "Empty", "UserNewZagolovok");
        this.AddRowToDoc(docRowEmpty);
      }
      this.AddRowToDoc(docRowZagolovok);
      this._vedomost_VB_new.Add_AttributeTypeRec_To_DocRow(docRowZagolovok, "Title", "UserNewZagolovok");
    }
    return true;
  }

  /// <summary>Команда "Добавить из IMBASE..."</summary>
  /// <param name_From_Oglavlenie="commandState">Выполняемая команда</param>
  /// <returns>Возвращает true, если команда обработана. False, если требуется другой обработчик</returns>
  private bool AddVedRowFromImbase(ICommandState commandState)
  {
    if (this._one_Ved_Nastr_Curr._bases_Options_Ved._list_quickObjectInfo == null || this._one_Ved_Nastr_Curr._bases_Options_Ved._list_quickObjectInfo.Count == 0)
    {
      int num = (int) MessageBox.Show("Настройки ввода из Imbase отсутствуют", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      return false;
    }
    if (this._one_Ved_Nastr_Curr._bases_Options_Ved._list_quickObjectInfo != null && this._one_Ved_Nastr_Curr._bases_Options_Ved._list_quickObjectInfo.Count > 0 && string.IsNullOrEmpty(this._one_Ved_Nastr_Curr._bases_Options_Ved._list_quickObjectInfo[0].Caption))
    {
      int num = (int) MessageBox.Show("Настройки ввода из Imbase отсутствуют", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      return false;
    }
    long ObjId_Current = -1;
    string razdel_Ved = this.Razdel_Ved_Curr();
    TableData tableData = this.DocRowCurrent();
    if (tableData != null)
    {
      string attributeValue = tableData.GetAttributeValue("ObjectIdIzd", false);
      if (attributeValue != null && attributeValue != "")
        ObjId_Current = Convert.ToInt64(attributeValue);
    }
    long objectID = Vedomost_VB_Static.VyborFromImbase(ObjId_Current, this._one_Ved_Nastr_Curr._bases_Options_Ved._list_quickObjectInfo);
    switch (objectID)
    {
      case -1:
      case 0:
        return false;
      default:
        if (this._one_Ved_Nastr_Curr._algorithmToPrint._oneRecordToPrint_Info != null)
          razdel_Ved = "1";
        else if (string.IsNullOrEmpty(razdel_Ved) || razdel_Ved == "0")
        {
          razdel_Ved = this.Vybor_Razdel_Ved();
          if (razdel_Ved == "-1")
            return false;
        }
        return this.AddVedRow_ByObjectID(objectID, "UserFromImbase", razdel_Ved, true);
    }
  }

  /// <summary> Раздел ТЕКУЩЕЙ строки </summary>
  /// <returns></returns>
  private string Razdel_Ved_Curr()
  {
    string str = "";
    TableData tableData = this.DocRowCurrent();
    if (tableData != null)
      str = tableData.GetAttributeValue("Razdel_Ved", false);
    return str;
  }

  /// <summary>Команда "Добавить существующий объект..."</summary>
  /// <param name_From_Oglavlenie="commandState">Выполняемая команда</param>
  /// <returns>Возвращает true, если команда обработана. False, если требуется другой обработчик</returns>
  private bool AddNewRowWithDBObject(ICommandState commandState)
  {
    long ObjId_Current = -1;
    string razdel_Ved = this.Razdel_Ved_Curr();
    TableData tableData = this.DocRowCurrent();
    if (tableData != null)
    {
      string attributeValue = tableData.GetAttributeValue("ObjectIdIzd", false);
      razdel_Ved = tableData.GetAttributeValue("Razdel_Ved", false);
      if (attributeValue != null && attributeValue != "")
        ObjId_Current = Convert.ToInt64(attributeValue);
    }
    long objectID = Vedomost_VB_Static.VyborFromObject(ObjId_Current, this._one_Ved_Nastr_Curr);
    switch (objectID)
    {
      case -1:
      case 0:
        return false;
      default:
        if (string.IsNullOrEmpty(razdel_Ved) || razdel_Ved == "0")
        {
          razdel_Ved = this.Vybor_Razdel_Ved();
          if (razdel_Ved == "-1")
            return false;
        }
        if (this.AddVedRow_ByObjectID(objectID, "UserDBObject", razdel_Ved, false) && this._typeDoc == Vedomost_VB.TypeDoc.Espd && this._one_Ved_Nastr_Curr._espd._isAddToSpLU)
        {
          string designationByObjectId = Vedomost_VB_Static.Get_Designation_By_ObjectId(objectID);
          if (!string.IsNullOrEmpty(designationByObjectId))
          {
            long num = Vedomost_VB_Static.Check_LU_By_DesignationDoc(designationByObjectId);
            if (!num.IsUndefinedId() && this._one_Ved_Nastr_Curr._espd._isAddRemark)
              this.AddVedRow_ByObjectID(num, "UserDBObject", razdel_Ved, false, this._one_Ved_Nastr_Curr._espd._textRemark, false);
          }
        }
        return true;
    }
  }

  private string Vybor_Razdel_Ved()
  {
    if (this._one_Ved_Nastr_Curr._list_RazdelsVed == null || this._one_Ved_Nastr_Curr._list_RazdelsVed.Count == 1 || this._one_Ved_Nastr_Curr._list_RazdelsVed.Count == 2 && this._one_Ved_Nastr_Curr._list_RazdelsVed[1]._razdelVed == 1000)
      return "1";
    List<Vedomost_VB.OneRazdelVed> oneRazdelVedList = new List<Vedomost_VB.OneRazdelVed>();
    for (int index = 0; index < this._one_Ved_Nastr_Curr._list_RazdelsVed.Count; ++index)
    {
      Vedomost_VB.OneRazdelVed oneRazdelVed = this._one_Ved_Nastr_Curr._list_RazdelsVed[index];
      if (oneRazdelVed._razdelVed != 1000)
        oneRazdelVedList.Add(oneRazdelVed);
      else
        break;
    }
    bool flag1 = false;
    if (this._one_Ved_Nastr_Curr._algorithmToPrint._list_OneRazdelToPrint != null)
    {
      for (int index = 0; index < this._one_Ved_Nastr_Curr._algorithmToPrint._list_OneRazdelToPrint.Count; ++index)
      {
        if (string.IsNullOrEmpty(this._one_Ved_Nastr_Curr._algorithmToPrint._list_OneRazdelToPrint[index]._namePage_First))
        {
          flag1 = true;
          break;
        }
      }
    }
    if (!flag1)
    {
      for (int index1 = oneRazdelVedList.Count - 1; index1 > -1; --index1)
      {
        bool flag2 = true;
        int razdelVed = oneRazdelVedList[index1]._razdelVed;
        if (this._one_Ved_Nastr_Curr._algorithmToPrint._list_OneRazdelToPrint != null)
        {
          for (int index2 = 0; index2 < this._one_Ved_Nastr_Curr._algorithmToPrint._list_OneRazdelToPrint.Count; ++index2)
          {
            Vedomost_VB.OneRazdelToPrint oneRazdelToPrint = this._one_Ved_Nastr_Curr._algorithmToPrint._list_OneRazdelToPrint[index2];
            if (oneRazdelToPrint._razdelVed == razdelVed)
            {
              if (!string.IsNullOrEmpty(oneRazdelToPrint._namePage_First) && oneRazdelToPrint._namePage_First == this.name_Page_Current)
              {
                flag2 = false;
                break;
              }
              if (!string.IsNullOrEmpty(oneRazdelToPrint._namePage_Next) && oneRazdelToPrint._namePage_Next == this.name_Page_Current)
              {
                flag2 = false;
                break;
              }
            }
          }
        }
        if (flag2)
          oneRazdelVedList.RemoveAt(index1);
      }
    }
    if (oneRazdelVedList.Count == 0)
      return "1";
    if (oneRazdelVedList.Count == 1)
      return oneRazdelVedList[0]._razdelVed.ToString();
    List<string> stringList = new List<string>();
    for (int index = 0; index < oneRazdelVedList.Count; ++index)
    {
      Vedomost_VB.OneRazdelVed oneRazdelVed = oneRazdelVedList[index];
      string name = oneRazdelVed._name;
      if (oneRazdelVed._razdelVed != 1000)
        stringList.Add(name);
      else
        break;
    }
    using (VyborFromStringList vyborFromStringList = new VyborFromStringList())
    {
      vyborFromStringList.Text = "Выбор раздела ведомости";
      vyborFromStringList.stringlist = stringList;
      return vyborFromStringList.ShowDialog() == DialogResult.OK ? oneRazdelVedList[vyborFromStringList.indexResult]._razdelVed.ToString() : "-1";
    }
  }

  /// <summary> По obiectID создать docRowNew и вставить в документ </summary>
  /// <param name="objectID"></param>
  /// <param name="from"></param>
  /// <param name="razdel_Ved"></param>
  /// <returns></returns>
  private bool AddVedRow_ByObjectID(
    long objectID,
    string from,
    string razdel_Ved,
    bool isEtoImbase,
    string remark = "",
    bool check = true)
  {
    if (objectID == 0L || objectID == -1L || check && !this._isVveliOsnovnyeDannye)
      return false;
    Vedomost_VB.RecordForVed_New recordForVed_New = this._one_Ved_Nastr_Curr._typeVed == Vedomost_VB.TypeVed.DP || this._one_Ved_Nastr_Curr._typeVed == Vedomost_VB.TypeVed.VD ? this._vedomost_VB_new.Create_recordForVed_New_From_ObjectID_DP_VD_Dialog(objectID) : this._vedomost_VB_new.Create_recordForVed_New_From_ObjectID(objectID);
    if (recordForVed_New == null)
      return false;
    if (recordForVed_New.Razdel_Ved < 1L)
    {
      if (string.IsNullOrEmpty(razdel_Ved))
      {
        razdel_Ved = this.Razdel_Ved_Curr();
        if (string.IsNullOrEmpty(razdel_Ved) || razdel_Ved == "0" || razdel_Ved == "-1")
          razdel_Ved = "1";
        try
        {
          if (!string.IsNullOrEmpty(razdel_Ved))
            recordForVed_New.Razdel_Ved = (long) Convert.ToInt32(razdel_Ved);
        }
        catch (Exception ex)
        {
          recordForVed_New.Razdel_Ved = 1L;
        }
      }
      else
        recordForVed_New.Razdel_Ved = (long) Convert.ToInt32(razdel_Ved);
    }
    if (!string.IsNullOrEmpty(remark))
      recordForVed_New.Set_Note(remark);
    if (isEtoImbase && Vedomost_VB_Static.IsUse_Input_From_Imbase)
    {
      List<Vedomost_VB_Static.AttributeValuesVed> list_AttributeValuesVed = (List<Vedomost_VB_Static.AttributeValuesVed>) null;
      try
      {
        list_AttributeValuesVed = Vedomost_VB_Static.Get_List_AttributeValuesVed_FromImbase(objectID);
      }
      catch
      {
      }
      if (list_AttributeValuesVed != null && list_AttributeValuesVed.Count > 0)
        Vedomost_VB_Static.Addition_recordForVed_New_From_Imbase(recordForVed_New, list_AttributeValuesVed, this._vedomost_VB_new.listCommonId);
    }
    TableData docRowInfo = this._vedomost_VB_new.Create_DocRow_Info(recordForVed_New, this._docTemplate, this.Document, from, false, this.name_Page_Current);
    if (docRowInfo != null)
    {
      docRowInfo.SetAttributeValue("TypeRow", "Info");
      if (!string.IsNullOrEmpty(this._variableRowCurrent))
        docRowInfo.SetAttributeValue("Variable", this._variableRowCurrent);
      this.AddRowToDoc(docRowInfo);
      this._vedomost_VB_new.Filled_Record_DocRow_Ved_Attributes(docRowInfo, recordForVed_New, from);
      if (!string.IsNullOrEmpty(this._variableRowCurrent))
        docRowInfo.SetAttributeValue("Variable", this._variableRowCurrent);
      if (this._one_Ved_Nastr_Curr._algorithmToPrint._afterInfo > 0)
      {
        TableData docRowEmpty = this._vedomost_VB_new.Create_DocRow_Empty(this._docTemplate, "", this.name_Page_Current);
        if (docRowEmpty != null)
        {
          razdel_Ved = docRowInfo.GetAttributeValue("Razdel_Ved", true);
          if (!string.IsNullOrEmpty(razdel_Ved))
            docRowEmpty.SetAttributeValue("Razdel_Ved", razdel_Ved);
          this.AddRowToDoc(docRowEmpty);
        }
      }
    }
    return true;
  }

  /// <summary> Команда "Добавить существующий объект... как строку "Куда входит" </summary>
  /// <param name="commandState"></param>
  /// <returns></returns>
  private bool AddNewKudaVhoditWithDBObject(ICommandState commandState)
  {
    long objectID = Vedomost_VB_Static.VyborFromObjectKudaVhodit();
    switch (objectID)
    {
      case -1:
      case 0:
        return false;
      default:
        return this.AddKudaVhodit_ByObjectID(commandState, objectID);
    }
  }

  /// <summary> Текущую _currKudaVhoditRow заполняем на основе objectID </summary>
  /// <param name="commandState"></param>
  /// <param name="objectID"></param>
  /// <returns></returns>
  private bool AddKudaVhodit_ByObjectID(ICommandState commandState, long objectID)
  {
    if (objectID == 0L || objectID == -1L || this._currKudaVhoditRow == null || !this._isVveliOsnovnyeDannye)
      return false;
    Vedomost_VB.RecordForVed_Vtor vtorFromObjectId = this._vedomost_VB_new.Create_recordForVed_Vtor_From_ObjectID(objectID);
    if (vtorFromObjectId == null || !this._vedomost_VB_new.Filled_DocRowVtor_Info(vtorFromObjectId, this._docTemplate, this._currKudaVhoditRow))
      return false;
    this._currKudaVhoditRow.UpdateLayout(false);
    return true;
  }

  /// <summary> Изменить групповую форму документа </summary>
  /// <param name="commandState"></param>
  /// <returns></returns>
  private bool EditGroupForm(ICommandState commandState)
  {
    ImDocument document = AVSPlugin.Instance.ActiveImDocumentEditorForm.Document;
    if (document == null || document.NodesCount < 1)
      return false;
    if (this._formaGroup_Doc == "B")
    {
      int num = (int) MessageBox.Show("Ведомость формы Б не может быть преобразована в групповую А или единичную", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      return false;
    }
    Intermech.AVS.Victor.EditGroupForm editGroupForm = new Intermech.AVS.Victor.EditGroupForm();
    int num1 = 12;
    if (this._one_Ved_Nastr_Curr._typeVed == Vedomost_VB.TypeVed.VS || this._one_Ved_Nastr_Curr._typeVed == Vedomost_VB.TypeVed.VP)
      num1 = !this.metodCreate.StartsWith("Auto") ? 123 : 23;
    editGroupForm._nGroupForm = num1;
    editGroupForm._formaGroup_Doc = this._formaGroup_Doc;
    if (editGroupForm.ShowDialog() != DialogResult.OK || !editGroupForm._isModifiedGroup || (editGroupForm._change == "AB" || editGroupForm._change == "EB") && MessageBox.Show("Ведомость формы Б в дальнейшем не преобразовать в групповую А или единичную\r\n\r\nПродолжить?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
      return false;
    string variable = "";
    string caption = "";
    string errorText = "";
    if (editGroupForm._change == "EA")
    {
      Vedomost_VB_Static.Convert_Ed_A(document);
      this._formaGroup_Doc = "A";
      if (this._variables_Coordination == null)
        this._variables_Coordination = new Vedomost_VB.Variables_Coordination();
      if (this._designationArticle != "")
      {
        if (!Vedomost_VB_Static.Variable_Create(this._designationArticle, true, 0, out variable, out caption, out errorText))
        {
          int num2 = (int) MessageBox.Show(errorText, "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
          return false;
        }
      }
      else if (!Vedomost_VB_Static.Variable_Create(document.Designation, true, 0, out variable, out caption, out errorText))
      {
        int num3 = (int) MessageBox.Show(errorText, "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        return false;
      }
      this._variables_Coordination.Add_Variable_WithCaption(variable, caption);
      Vedomost_VB_Static.SetAttributeValue_Variable(document, this._variables_Coordination);
      TableData endMainTable = this.GetEndMainTable();
      if (endMainTable != null)
      {
        TableData docRowEmpty = this._vedomost_VB_new.Create_DocRow_Empty(this._docTemplate, "", this.name_Page_Current);
        TableData docRow = Vedomost_VB_Static.Create_DocRow(document, this._one_Ved_Nastr_Curr._algorithmToPrint._oneRecordToPrintTitleVar, Vedomost_VB_Static._text_For_TitleVar, "TitleVar", "");
        if (docRow != null)
        {
          if (docRowEmpty != null)
            endMainTable.AddChildNode((DocumentTreeNode) docRowEmpty, true, true);
          endMainTable.AddChildNode((DocumentTreeNode) docRow, true, true);
        }
        TableData child = (TableData) null;
        if (!string.IsNullOrEmpty(variable))
          child = Vedomost_VB_Static.Create_DocRow(document, this._one_Ved_Nastr_Curr._algorithmToPrint._oneRecordToPrintTitleIsp, variable, "TitleIsp", variable);
        if (child != null)
          endMainTable.AddChildNode((DocumentTreeNode) child, true, true);
        int num4 = (int) MessageBox.Show("Документ преобразован в групповой\r\nФорма А", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      }
      return true;
    }
    if (editGroupForm._change == "AE")
    {
      Vedomost_VB_Static.Convert_A_Ed(document);
      this._variables_Coordination = (Vedomost_VB.Variables_Coordination) null;
      this._formaGroup_Doc = "Ed";
      int num5 = (int) MessageBox.Show("Документ преобразован в единичный.\r\n\r\nУдалены все данные, кроме общих\r\nи относящихся к первому исполнению", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      return true;
    }
    if (editGroupForm._change == "EB")
      Vedomost_VB_Static.Convert_Ed_B(document);
    if (editGroupForm._change == "AB")
    {
      this._formaGroup_Doc = "B";
      Vedomost_VB_Static.Convert_A_B(document);
    }
    return true;
  }

  /// <summary> Команда СПИСОК ИСПОЛНЕНИЙ </summary>
  /// <param name="commandState"></param>
  /// <returns></returns>
  private bool ListVariableToDocument(ICommandState commandState)
  {
    ImDocument document = AVSPlugin.Instance.ActiveImDocumentEditorForm.Document;
    if (document == null || document.NodesCount < 1 || this._formaGroup_Doc == "" || this._formaGroup_Doc == "Ed" || this._variables_Coordination == null)
      return false;
    VariablesList variablesList = new VariablesList();
    variablesList._variables_Coordination = this._variables_Coordination;
    variablesList._designationArt = this._designationArticle;
    if (variablesList.ShowDialog() != DialogResult.OK || !variablesList._isModified)
      return true;
    if (this._formaGroup_Doc == "A")
    {
      Vedomost_VB_Static.Processing_Group_A(document, this._variables_Coordination, variablesList._variables, this._vedomost_VB_new, this._docTemplate, this.name_Page_Current);
      document.UpdateLayout(true);
    }
    else if (this._formaGroup_Doc == "B")
      Vedomost_VB_Static.Processing_Group_B(document, this._variables_Coordination, variablesList._variables);
    int num = (int) MessageBox.Show("Операция завершена", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    return true;
  }

  /// <summary> Заполнение атрибута типа записи при ручном добавлении записи /// </summary>
  /// <param name="docRow"></param>
  /// <param name="typeRec"></param>
  /// <param name="from"></param>
  /// <returns></returns>
  public bool Add_AttributeTypeRec_To_DocRow(TableData docRow, string typeRec, string from)
  {
    if (docRow == null)
      return false;
    if (!string.IsNullOrEmpty(typeRec))
      docRow.SetAttributeValue("TypeRec", typeRec);
    if (!string.IsNullOrEmpty(typeRec))
      docRow.SetAttributeValue("From", from);
    if (typeRec == "Oglavlenie")
      docRow.SetAttributeValue("TypeRow", "Info");
    else
      docRow.SetAttributeValue("TypeRow", typeRec);
    return true;
  }

  /// <summary> Команда "Добавить основную строку" </summary>
  /// <param name="commandState"></param>
  /// <returns></returns>
  private bool AddVedRow_Info(ICommandState commandState)
  {
    if (!this._isVveliOsnovnyeDannye)
      return false;
    string attributeValue = this.Razdel_Ved_Curr();
    if (string.IsNullOrEmpty(attributeValue) || attributeValue == "0" || attributeValue == "-1")
      attributeValue = "1";
    TableData docRowInfo = this._vedomost_VB_new.Create_DocRow_Info((Vedomost_VB.RecordForVed_New) null, this._docTemplate, this.Document, "UserNewRowInfo", true, this.name_Page_Current);
    if (docRowInfo != null)
    {
      if (!string.IsNullOrEmpty(this._variableRowCurrent))
        docRowInfo.SetAttributeValue("Variable", this._variableRowCurrent);
      docRowInfo.SetAttributeValue("TypeRow", "Info");
      docRowInfo.SetAttributeValue("Razdel_Ved", attributeValue);
      this.AddRowToDoc(docRowInfo);
      this._vedomost_VB_new.Add_AttributeTypeRec_To_DocRow(docRowInfo, "Info", "UserNewRowInfo");
    }
    return true;
  }

  /// <summary> Команда "Добавить строку Дополнительная 1" </summary>
  /// <param name="commandState"></param>
  /// <returns></returns>
  private bool AddVedRow_Additional1(ICommandState commandState)
  {
    if (!this._isVveliOsnovnyeDannye)
      return false;
    TableData docRowAdditional = this._vedomost_VB_new.Create_DocRow_Additional(this._docTemplate, this.Document, "UserNewRowAdditional", 1, this.name_Page_Current);
    if (docRowAdditional != null)
    {
      if (!string.IsNullOrEmpty(this._variableRowCurrent))
        docRowAdditional.SetAttributeValue("Variable", this._variableRowCurrent);
      docRowAdditional.SetAttributeValue("TypeRow", "Additional1");
      string attributeValue = this.Razdel_Ved_Curr();
      if (string.IsNullOrEmpty(attributeValue) || attributeValue == "0" || attributeValue == "-1")
        attributeValue = "1";
      docRowAdditional.SetAttributeValue("Razdel_Ved", attributeValue);
      this.AddRowToDoc(docRowAdditional);
      this._vedomost_VB_new.Add_AttributeTypeRec_To_DocRow(docRowAdditional, "Additional1", "UserNewRowAdditional");
    }
    return true;
  }

  /// <summary> Команда "Добавить строку Дополнительная 2" </summary>
  /// <param name="commandState"></param>
  /// <returns></returns>
  private bool AddVedRow_Additional2(ICommandState commandState)
  {
    if (!this._isVveliOsnovnyeDannye)
      return false;
    TableData docRowAdditional = this._vedomost_VB_new.Create_DocRow_Additional(this._docTemplate, this.Document, "UserNewRowAdditional", 2, this.name_Page_Current);
    if (docRowAdditional != null)
    {
      if (!string.IsNullOrEmpty(this._variableRowCurrent))
        docRowAdditional.SetAttributeValue("Variable", this._variableRowCurrent);
      docRowAdditional.SetAttributeValue("TypeRow", "Additional2");
      string attributeValue = this.Razdel_Ved_Curr();
      if (string.IsNullOrEmpty(attributeValue) || attributeValue == "0" || attributeValue == "-1")
        attributeValue = "1";
      docRowAdditional.SetAttributeValue("Razdel_Ved", attributeValue);
      this.AddRowToDoc(docRowAdditional);
      this._vedomost_VB_new.Add_AttributeTypeRec_To_DocRow(docRowAdditional, "Additional2", "UserNewRowAdditional");
    }
    return true;
  }

  /// <summary> Команда "Добавить строку Дополнительная 3" </summary>
  /// <param name="commandState"></param>
  /// <returns></returns>
  private bool AddVedRow_Additional3(ICommandState commandState)
  {
    if (!this._isVveliOsnovnyeDannye)
      return false;
    TableData docRowAdditional = this._vedomost_VB_new.Create_DocRow_Additional(this._docTemplate, this.Document, "UserNewRowAdditional", 3, this.name_Page_Current);
    if (docRowAdditional != null)
    {
      if (!string.IsNullOrEmpty(this._variableRowCurrent))
        docRowAdditional.SetAttributeValue("Variable", this._variableRowCurrent);
      docRowAdditional.SetAttributeValue("TypeRow", "Additional3");
      string attributeValue = this.Razdel_Ved_Curr();
      if (string.IsNullOrEmpty(attributeValue) || attributeValue == "0" || attributeValue == "-1")
        attributeValue = "1";
      docRowAdditional.SetAttributeValue("Razdel_Ved", attributeValue);
      this.AddRowToDoc(docRowAdditional);
      this._vedomost_VB_new.Add_AttributeTypeRec_To_DocRow(docRowAdditional, "Additional3", "UserNewRowAdditional");
    }
    return true;
  }

  /// <summary> Команда "Добавить строку Дополнительная 4" </summary>
  /// <param name="commandState"></param>
  /// <returns></returns>
  private bool AddVedRow_Additional4(ICommandState commandState)
  {
    if (!this._isVveliOsnovnyeDannye)
      return false;
    TableData docRowAdditional = this._vedomost_VB_new.Create_DocRow_Additional(this._docTemplate, this.Document, "UserNewRowAdditional", 4, this.name_Page_Current);
    if (docRowAdditional != null)
    {
      if (!string.IsNullOrEmpty(this._variableRowCurrent))
        docRowAdditional.SetAttributeValue("Variable", this._variableRowCurrent);
      docRowAdditional.SetAttributeValue("TypeRow", "Additional4");
      string attributeValue = this.Razdel_Ved_Curr();
      if (string.IsNullOrEmpty(attributeValue) || attributeValue == "0" || attributeValue == "-1")
        attributeValue = "1";
      docRowAdditional.SetAttributeValue("Razdel_Ved", attributeValue);
      this.AddRowToDoc(docRowAdditional);
      this._vedomost_VB_new.Add_AttributeTypeRec_To_DocRow(docRowAdditional, "Additional4", "UserNewRowAdditional");
    }
    return true;
  }

  /// <summary> Команда "Добавить пустую строку" </summary>
  /// <param name_From_Oglavlenie="commandState"></param>
  /// <returns></returns>
  private bool AddVedRow_Empty(ICommandState commandState)
  {
    if (!this._isVveliOsnovnyeDannye)
      return false;
    TableData docRowEmpty = this._vedomost_VB_new.Create_DocRow_Empty(this._docTemplate, this._variableRowCurrent, this.name_Page_Current);
    if (docRowEmpty != null)
    {
      string attributeValue = this.Razdel_Ved_Curr();
      if (string.IsNullOrEmpty(attributeValue) || attributeValue == "0" || attributeValue == "-1")
        attributeValue = "1";
      docRowEmpty.SetAttributeValue("Razdel_Ved", attributeValue);
      this.AddRowToDoc(docRowEmpty);
      this._vedomost_VB_new.Add_AttributeTypeRec_To_DocRow(docRowEmpty, "Empty", "UserNewEmpty");
    }
    return true;
  }

  /// <summary> Команда "Добавить строку ПРИМЕЧАНИЕ" </summary>
  /// <param name_From_Oglavlenie="commandState"></param>
  /// <returns></returns>
  private bool AddVedRow_Remark(ICommandState commandState)
  {
    if (!this._isVveliOsnovnyeDannye)
      return false;
    TableData docRowRemark = this._vedomost_VB_new.Create_DocRow_Remark(this._docTemplate, this._variableRowCurrent, this.name_Page_Current);
    if (docRowRemark != null)
    {
      string attributeValue = this.Razdel_Ved_Curr();
      if (string.IsNullOrEmpty(attributeValue) || attributeValue == "0" || attributeValue == "-1")
        attributeValue = "1";
      docRowRemark.SetAttributeValue("Razdel_Ved", attributeValue);
      this.AddRowToDoc(docRowRemark);
      this._vedomost_VB_new.Add_AttributeTypeRec_To_DocRow(docRowRemark, "Remark", "UserNewRemark");
    }
    return true;
  }

  /// <summary> Добавить строку - короткое примечание </summary>
  /// <param name="commandState"></param>
  /// <returns></returns>
  private bool AddVedRow_RemarkShort(ICommandState commandState)
  {
    if (!this._isVveliOsnovnyeDannye)
      return false;
    TableData docRowRemarkShort = this._vedomost_VB_new.Create_DocRow_RemarkShort(this._docTemplate, this._variableRowCurrent, this.name_Page_Current);
    if (docRowRemarkShort != null)
    {
      string attributeValue = this.Razdel_Ved_Curr();
      if (string.IsNullOrEmpty(attributeValue) || attributeValue == "0" || attributeValue == "-1")
        attributeValue = "1";
      docRowRemarkShort.SetAttributeValue("Razdel_Ved", attributeValue);
      this.AddRowToDoc(docRowRemarkShort);
      this._vedomost_VB_new.Add_AttributeTypeRec_To_DocRow(docRowRemarkShort, "RemarkShort", "UserNewRemarkShort");
    }
    return true;
  }

  /// <summary> Создать копию текущей записи </summary>
  /// <param name="commandState"></param>
  /// <returns></returns>
  private bool AddCopyRow(ICommandState commandState)
  {
    if (this.DocRowCurrent().Clone(true, true) is TableData docRowNew)
      this.AddRowToDoc(docRowNew);
    return true;
  }

  /// <summary> Строку ВВЕРХ </summary>
  /// <param name="commandState"></param>
  /// <returns></returns>
  private bool DocRowUp(ICommandState commandState)
  {
    this.DocRowChange(this.DocRowCurrent(), true);
    return true;
  }

  /// <summary> Строку ВНИЗ </summary>
  /// <param name="commandState"></param>
  /// <returns></returns>
  private bool DocRowDown(ICommandState commandState)
  {
    this.DocRowChange(this.DocRowCurrent(), false);
    return true;
  }

  /// <summary> Перемещение строк </summary>
  /// <param name="docRowCurr"></param>
  /// <param name="up">Вверх или вниз</param>
  private void DocRowChange(TableData docRowCurr, bool up)
  {
    if (docRowCurr == null)
      return;
    this.Calculate_Pages();
    if (this.number_Page_Current < this.number_Page_First_Info || this.number_Page_Current > this.number_Page_End_Info || this.number_Page_Current < this.number_Page_First_CurrentName || this.number_Page_Current > this.number_Page_End_CurrentName)
      return;
    TableElement viewRowParent;
    int viewRowIndex;
    VedomostEditorWindow.FindRowParent(this.DocumentControl.GetSelectedNodes()[0], out viewRowParent, out viewRowIndex);
    if (viewRowIndex < 0 || ((viewRowIndex != 0 ? 0 : (this.number_Page_Current == this.number_Page_First_Info ? 1 : 0)) & (up ? 1 : 0)) != 0 || ((viewRowIndex != 0 ? 0 : (this.number_Page_Current == this.number_Page_First_CurrentName ? 1 : 0)) & (up ? 1 : 0)) != 0 || viewRowIndex == viewRowParent.NodesCount - 1 && this.number_Page_Current == this.number_Page_End_Info && !up || viewRowIndex == viewRowParent.NodesCount - 1 && this.number_Page_Current == this.number_Page_End_CurrentName && !up)
      return;
    TableData child = docRowCurr.Clone(true, true) as TableData;
    viewRowParent.RemoveChildNodeAt(viewRowIndex, false, false);
    if (up)
    {
      if (viewRowIndex > 0)
        viewRowParent.InsertChildNode(viewRowIndex - 1, (DocumentTreeNode) child, false, true, false, true, false);
      else if (this.Document.Nodes[this.number_Page_Current - 1].FindFirstChildNodeByName("Главная таблица") is TableData firstChildNodeByName1)
        firstChildNodeByName1.InsertChildNode(firstChildNodeByName1.NodesCount - 2, (DocumentTreeNode) child, false, true, true, true, false);
    }
    else if (viewRowIndex <= viewRowParent.NodesCount - 1)
      viewRowParent.InsertChildNode(viewRowIndex + 1, (DocumentTreeNode) child, false, true, false, true, false);
    else if (this.Document.Nodes[this.number_Page_Current + 1].FindFirstChildNodeByName("Главная таблица") is TableData firstChildNodeByName2)
      firstChildNodeByName2.InsertChildNode(0, (DocumentTreeNode) child, false, true, true, true, false);
    this.DocumentControl.SetSelection(child.Nodes[0], true, Point.Empty, true, false);
  }

  /// <summary> Окно Свойства (Карточка) </summary>
  /// <param name="commandState"></param>
  /// <returns></returns>
  private bool Propertias(ICommandState commandState)
  {
    TableData tableData = this.DocRowCurrent() ?? this.DocPodRowCurrent();
    if (tableData == null)
      return false;
    string attributeValue = tableData.GetAttributeValue("ObjectIdIzd", true);
    if (string.IsNullOrEmpty(attributeValue) || attributeValue == "0")
    {
      int num = (int) MessageBox.Show("В строке отсутствуют данные об объекте", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Question);
      return false;
    }
    if (!string.IsNullOrEmpty(attributeValue))
    {
      int num1 = (int) PropertiesWindow.Execute("Свойства объекта", "", Convert.ToInt64(attributeValue), "ObjectProperties");
    }
    return true;
  }

  /// <summary> Удалить одинаковые тексты </summary>
  /// <param name="commandState"></param>
  /// <returns></returns>
  private bool DeleteIdenticalTexts(ICommandState commandState)
  {
    if (Processing_Ved_Static.DeleteIdenticalTexts(this.Document))
    {
      int num1 = (int) MessageBox.Show("Команда выполнена\r\n\r\nЕсть замененные тексты", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }
    else
    {
      int num2 = (int) MessageBox.Show("Одинаковые тексты не обнаружены", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }
    this.isDeleteIdenticalTexts = "Yes";
    return true;
  }

  /// <summary> Восстановить одинаковые тексты </summary>
  /// <param name="commandState"></param>
  /// <returns></returns>
  private bool RecoverIdenticalTexts(ICommandState commandState)
  {
    if (Processing_Ved_Static.RecoverIdenticalTexts(this.Document))
    {
      int num1 = (int) MessageBox.Show("Команда выполнена\r\n\r\nЕсть замененные тексты", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }
    else
    {
      int num2 = (int) MessageBox.Show("Тексты для восстановления не обнаружены", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }
    this.isDeleteIdenticalTexts = "";
    return true;
  }

  /// <summary> Свойства строки </summary>
  /// <param name="commandState"></param>
  /// <returns></returns>
  private bool AboutDocRow(ICommandState commandState)
  {
    TableData docRow = this.DocRowCurrent() ?? this.DocPodRowCurrent();
    if (docRow != null)
      Vedomost_VB_Static.AboutDocRow_Function(docRow);
    return true;
  }

  /// <summary> Свойства ДОКУМЕНТА </summary>
  /// <param name="commandState"></param>
  /// <returns></returns>
  private bool AboutDocument(ICommandState commandState)
  {
    Vedomost_VB_Static.AboutDocument(this.Document);
    return true;
  }

  /// <summary> Открыть редактор шаблона </summary>
  /// <param name="commandState"></param>
  /// <returns></returns>
  private bool EditTemplate(ICommandState commandState)
  {
    DocumentEditorPlugin.Instance.OpenDocumentImDocumentObject(this._templateID, false, true);
    return true;
  }

  /// <summary> Проверка документа </summary>
  /// <param name="commandState"></param>
  /// <returns></returns>
  private bool Check_Ved_Or_Tabl(ICommandState commandState)
  {
    Processing_Ved_Static.Check_Ved_Or_Tabl(this.Document, this._listError);
    if (this._listError != null)
      this.ErrorsUserControl.Show(this._listError.CreateErrorMessage());
    return true;
  }

  /// <summary> Сортировка готового документа (пока ЕСПД) </summary>
  /// <param name="commandState"></param>
  /// <returns></returns>
  private bool SortingDoc(ICommandState commandState)
  {
    Processing_Ved_Static.Sorting_Document(this.Document, this._one_Ved_Nastr_Curr._sorting_Usl_Doc, this._docTemplate);
    return true;
  }

  private bool Experiment(ICommandState commandState)
  {
    Processing_Ved_Static.Sorting_Document(this.Document, this._one_Ved_Nastr_Curr._sorting_Usl_Doc, this._docTemplate);
    return true;
  }

  /// <summary> Создать Лист утверждения на текущую спецификацию ЕСПД </summary>
  /// <param name="commandState"></param>
  /// <returns></returns>
  private bool Create_LUESPD(ICommandState commandState)
  {
    this.Begin_Espd(true);
    return true;
  }

  /// <summary> Удалить строку </summary>
  /// <param name="commandState"></param>
  /// <returns></returns>
  private bool RemoveDocRow(ICommandState commandState)
  {
    Vedomost_VB_Static.RemoveDocRow(this.DocRowCurrent());
    return true;
  }

  /// <summary> Удалить ПОДСтроку </summary>
  /// <param name="commandState"></param>
  /// <returns></returns>
  private bool RemoveDocPodRow(ICommandState commandState)
  {
    Vedomost_VB_Static.RemoveDocPodRow(this.DocPodRowCurrent());
    return true;
  }

  /// <summary> Команда "Добавить строку заголовка" </summary>
  /// <param name_From_Oglavlenie="commandState"></param>
  /// <returns></returns>
  private bool AddVedRow_Zagolovok(ICommandState commandState)
  {
    if (!this._isVveliOsnovnyeDannye)
      return false;
    Vedomost_VB.RecordForVed_New recordForVed = new Vedomost_VB.RecordForVed_New();
    recordForVed.TypeRec = Vedomost_VB.TypeRec.Title;
    string attributeValue1 = "1";
    if (this._one_Ved_Nastr_Curr._zagolovki_Ved != null && this._one_Ved_Nastr_Curr._zagolovki_Ved._list_One_Zagolovok != null && this._one_Ved_Nastr_Curr._zagolovki_Ved._list_One_Zagolovok.Count != 0)
    {
      VyborZagolovka vyborZagolovka = new VyborZagolovka();
      if (this._guidTemplateDoc == Guid.Empty)
      {
        this._guidTemplateDoc = this.DocumentGuid;
        vyborZagolovka._guidTypeVed = Guid.Empty;
      }
      else
        vyborZagolovka._guidTypeVed = this.DocumentGuid;
      vyborZagolovka._guidTemplateVed = this._guidTemplateDoc;
      vyborZagolovka._one_Ved_Nastr = this._one_Ved_Nastr_Curr;
      if (vyborZagolovka.ShowDialog() != DialogResult.OK)
        return false;
      int selectedIndex = vyborZagolovka.ListZagolovkov.SelectedIndex;
      if (selectedIndex > 0)
      {
        string name = vyborZagolovka.ListZagolovkov.Items[selectedIndex].ToString();
        recordForVed.Set_Name(name);
        attributeValue1 = this._one_Ved_Nastr_Curr._zagolovki_Ved._list_One_Zagolovok[selectedIndex - 1]._granicaPriznaka;
      }
    }
    string attributeValue2 = this.Razdel_Ved_Curr();
    if (string.IsNullOrEmpty(attributeValue2) || attributeValue2 == "0" || attributeValue2 == "-1")
      attributeValue2 = "1";
    TableData docRowEmpty = this._vedomost_VB_new.Create_DocRow_Empty(this._docTemplate, this._variableRowCurrent, this.name_Page_Current);
    docRowEmpty.SetAttributeValue("Razdel_Ved", attributeValue2);
    TableData docRowZagolovok = this._vedomost_VB_new.Create_DocRow_Zagolovok(this._docTemplate, recordForVed, this._variableRowCurrent, this.name_Page_Current);
    docRowZagolovok.SetAttributeValue("Razdel_Ved", attributeValue1);
    if (docRowZagolovok != null)
    {
      this._vedomost_VB_new.Add_AttributeTypeRec_To_DocRow(docRowEmpty, "Empty", "UserNewZagolovok");
      this.AddRowToDoc(docRowEmpty);
      this.AddRowToDoc(docRowZagolovok);
      this._vedomost_VB_new.Add_AttributeTypeRec_To_DocRow(docRowZagolovok, "Title", "UserNewZagolovok");
    }
    return true;
  }

  /// <summary> Команда "Добавить пподзаголовок" </summary>
  /// <param name_From_Oglavlenie="commandState"></param>
  /// <returns></returns>
  private bool AddVedRow_PodZagolovok(ICommandState commandState)
  {
    if (!this._isVveliOsnovnyeDannye)
      return false;
    Vedomost_VB.RecordForVed_New recordForVed = new Vedomost_VB.RecordForVed_New();
    recordForVed.TypeRec = Vedomost_VB.TypeRec.Title2;
    recordForVed.Set_Name("");
    TableData docRowPodZagolovok = this._vedomost_VB_new.Create_DocRow_PodZagolovok(this._docTemplate, recordForVed, this._variableRowCurrent, this.name_Page_Current);
    TableData docRowEmpty = this._vedomost_VB_new.Create_DocRow_Empty(this._docTemplate, this._variableRowCurrent, this.name_Page_Current);
    if (docRowPodZagolovok != null)
    {
      this._vedomost_VB_new.Add_AttributeTypeRec_To_DocRow(docRowEmpty, "Empty", "UserNewPodZagolovok");
      this.AddRowToDoc(docRowEmpty);
      this.AddRowToDoc(docRowPodZagolovok);
      this._vedomost_VB_new.Add_AttributeTypeRec_To_DocRow(docRowPodZagolovok, "Title2", "UserNewPodZagolovok");
    }
    return true;
  }

  /// <summary> Команда "Добавить строку заголовка части" </summary>
  /// <param name_From_Oglavlenie="commandState"></param>
  /// <returns></returns>
  private bool AddVedRow_TitlePart(ICommandState commandState)
  {
    if (!this._isVveliOsnovnyeDannye)
      return false;
    TableData docRowTitlePart = this._vedomost_VB_new.Create_DocRow_TitlePart(this._docTemplate, this._variableRowCurrent, this.name_Page_Current);
    TableData docRowEmpty = this._vedomost_VB_new.Create_DocRow_Empty(this._docTemplate, this._variableRowCurrent, this.name_Page_Current);
    if (docRowTitlePart != null)
    {
      this._vedomost_VB_new.Add_AttributeTypeRec_To_DocRow(docRowEmpty, "Empty", "UserNewTitlePart");
      this.AddRowToDoc(docRowEmpty);
      this.AddRowToDoc(docRowTitlePart);
      this._vedomost_VB_new.Add_AttributeTypeRec_To_DocRow(docRowTitlePart, "TitlePart", "UserNewTitlePart");
    }
    return true;
  }

  /// <summary> Команда "Добавить подстроку "Куда входит"" </summary>
  /// <param name_From_Oglavlenie="commandState"></param>
  /// <returns></returns>
  private bool AddVedRow_KudaVhodit(ICommandState commandState)
  {
    if (!this._isVveliOsnovnyeDannye)
      return false;
    TableData docRowKudaVhodit = this._vedomost_VB_new.Create_DocRow_KudaVhodit(this._docTemplate);
    if (docRowKudaVhodit != null)
      this.AddRowVtor(docRowKudaVhodit);
    return true;
  }

  /// <summary> Команда "Добавить подстроку "Итого"" </summary>
  /// <param name_From_Oglavlenie="commandState"></param>
  /// <returns></returns>
  private bool AddVedRow_Itogo(ICommandState commandState)
  {
    if (!this._isVveliOsnovnyeDannye)
      return false;
    TableData docRowItogo = this._vedomost_VB_new.Create_DocRow_Itogo(this._docTemplate);
    if (docRowItogo != null)
      this.AddRowVtor(docRowItogo);
    return true;
  }

  /// <summary> В ведомость вставить готовую "СТРОКУ" после текущей </summary>
  /// <param name_From_Oglavlenie="docRowNew"></param>
  private void AddRowToDoc(TableData docRowNew)
  {
    if (docRowNew == null)
      return;
    if (this._formaGroup_Doc == "B")
    {
      Guid guid = Guid.NewGuid();
      docRowNew.SetAttributeValue("Guid_RecB", guid.ToString());
    }
    DocumentTreeNode[] selectedNodes = this.DocumentControl.GetSelectedNodes();
    if (selectedNodes.Length == 0)
      return;
    TableElement viewRowParent1;
    int viewRowIndex1;
    VedomostEditorWindow.FindRowParent(selectedNodes[0], out viewRowParent1, out viewRowIndex1);
    string name1 = viewRowParent1.Name;
    int num = 0;
    TableElement viewRowParent2 = viewRowParent1;
    for (; !name1.StartsWith("Главная таблица") && num < 5; ++num)
    {
      TableElement parentRow = (TableElement) viewRowParent2.FindParentRow(false);
      int viewRowIndex2;
      VedomostEditorWindow.FindRowParent((DocumentTreeNode) parentRow, out viewRowParent2, out viewRowIndex2);
      if (viewRowParent2 == null)
        viewRowParent2 = (TableElement) this.Get_PageCurrent().FindFirstNodeByName("Главная таблица");
      name1 = viewRowParent2.Name;
      if (name1.StartsWith("Главная таблица"))
      {
        viewRowParent1 = viewRowParent2;
        viewRowIndex1 = viewRowIndex2;
        break;
      }
      viewRowParent2 = parentRow;
    }
    if (name1.StartsWith("Главная таблица") && viewRowIndex1 == -1)
      viewRowIndex1 = viewRowParent1.NodesCount - 1;
    int index = viewRowIndex1 + 1;
    int nodesCount = this.Document.NodesCount;
    string name2 = docRowNew.Name;
    viewRowParent1.InsertChildNode(index, (DocumentTreeNode) docRowNew, true, true, true, true, false);
    docRowNew.Name = name2;
    if (this._formaGroup_Doc == "B")
      this.Filled_Tabl_Isp_B(nodesCount);
    this.DocumentControl.SetSelection(docRowNew.Nodes[0], true, Point.Empty, true, false);
  }

  /// <summary> Проверка. Есть ли ПЕРВЫЙ заголовок в ЕСПД </summary>
  /// <returns></returns>
  private bool Check_FirstZagol_ESPD()
  {
    TableData firstMainTable = Vedomost_VB_Static.FindFirstMainTable(this.Document);
    if (firstMainTable == null || firstMainTable.NodesCount < 1)
      return false;
    TableData node = (TableData) firstMainTable.Nodes[0];
    return node != null && !(node.Name != "Заголовок") && node.FindFirstNodeByName("Наименование") is TextData firstNodeByName && !(firstNodeByName.Text != "Документация");
  }

  /// <summary> Есть ли запись ОСНОВНОГО ЛИСТА УТВЕРЖДЕНИЯ </summary>
  /// <returns></returns>
  private bool Check_DocRowLU_ESPD()
  {
    TableData firstMainTable = Vedomost_VB_Static.FindFirstMainTable(this.Document);
    if (firstMainTable == null || firstMainTable.NodesCount < 2)
      return false;
    TableData node = (TableData) firstMainTable.Nodes[1];
    return node != null && !(node.Name != "Основная строка") && node.FindFirstNodeByName("Обозначение") is TextData firstNodeByName && !(firstNodeByName.Text != this._designationDoc + "-ЛУ");
  }

  /// <summary> На ПЕРВУЮ страницу SP ESPD добавить заголовок "Документация" </summary>
  private void Add_FirstZagol_ESPD()
  {
    Vedomost_VB.One_Zagolovok oneZagolovok = this._one_Ved_Nastr_Curr._zagolovki_Ved._list_One_Zagolovok[0];
    Vedomost_VB.RecordForVed_New recordForVed = new Vedomost_VB.RecordForVed_New();
    recordForVed.TypeRec = Vedomost_VB.TypeRec.Title;
    recordForVed.Set_Name(oneZagolovok._name);
    this.DocumentControl.SetSelection((DocumentTreeNode) Vedomost_VB_Static.FindFirstMainTable(this.Document), true, Point.Empty, true, false);
    TableData docRowZagolovok = this._vedomost_VB_new.Create_DocRow_Zagolovok(this._docTemplate, recordForVed, "", this.name_Page_Current);
    docRowZagolovok.SetAttributeValue("Razdel_Ved", "1");
    docRowZagolovok.SetAttributeValue("TypeRec", "Title");
    docRowZagolovok.SetAttributeValue("From", "Auto");
    this.AddRowToDoc(docRowZagolovok);
    this.DocumentControl.SetSelection(docRowZagolovok.Nodes[0], true, Point.Empty, true, false);
  }

  /// <summary> Анализ и заполнение номеров исполнений в форме Б Если добавилась страница </summary>
  private void Filled_Tabl_Isp_B(int listovBylo)
  {
    if (this._formaGroup_Doc != "B" || this.Document.NodesCount == 1 || this.Document.NodesCount == listovBylo || this.Document.NodesCount <= listovBylo)
      return;
    this.Filled_Tabl_Isp_Next_Like_Previous();
    Vedomost_VB_Static.FilledNumbersIspolneniyFormaB(this.Document);
  }

  /// <summary> Анализ и заполнение ПУСТЫХ номеров исполнений в форме Б  как на предыдущей странице </summary>
  private void Filled_Tabl_Isp_Next_Like_Previous()
  {
    string nodeName1 = "Заголовок таблицы исполнений";
    textData2 = (TextData) null;
    string nodeName2 = "Номера исполнений";
    tableData2 = (TableData) null;
    for (int index1 = 0; index1 < this.Document.NodesCount; ++index1)
    {
      DocumentTreeNode node1 = this.Document.Nodes[index1];
      if (!(node1.NodeClass != "Page") && !(node1.Id == "TL") && !(node1.Id == "Титульный лист") && !(node1.Name == "Титульный лист") && !(node1.Id == "LRI") && !(node1.Name == "Лист регистрации изменений"))
      {
        TextData textData1 = textData2;
        TableData tableData1 = tableData2;
        if (node1.FindFirstNodeByName(nodeName1) is TextData textData2 && node1.FindFirstNodeByName(nodeName2) is TableData tableData2 && textData1 != null && tableData1 != null && string.IsNullOrEmpty((tableData2.Nodes[0] as TextData).Text))
        {
          textData2.AssignText(textData1.Text, true, true, false, false, false);
          for (int index2 = 0; index2 < tableData1.NodesCount; ++index2)
          {
            TextData node2 = tableData1.Nodes[index2] as TextData;
            if (!string.IsNullOrEmpty(node2.Text))
              (tableData2.Nodes[index2] as TextData).AssignText(node2.Text, true, true, false, false, false);
            else
              break;
          }
        }
      }
    }
  }

  /// <summary> В основную строку вставить готовую "ВТОРИЧНУЮ СТРОКУ" или "ИТОГО" после текущей  </summary>
  /// <param name_From_Oglavlenie="docRowVtorNew"></param>
  private void AddRowVtor(TableData docRowVtorNew)
  {
    DocumentTreeNode[] selectedNodes = this.DocumentControl.GetSelectedNodes();
    if (selectedNodes.Length == 0)
      return;
    TableElement viewRowParent1;
    int viewRowIndex1;
    VedomostEditorWindow.FindRowParent(selectedNodes[0], out viewRowParent1, out viewRowIndex1);
    string name = viewRowParent1.Name;
    int num = 0;
    TableElement viewRowParent2 = viewRowParent1;
    for (; !name.StartsWith("Подтаблица Куда входит") && num < 5; ++num)
    {
      TableElement parentRow = (TableElement) viewRowParent2.FindParentRow(false);
      int viewRowIndex2;
      VedomostEditorWindow.FindRowParent((DocumentTreeNode) parentRow, out viewRowParent2, out viewRowIndex2);
      name = viewRowParent2.Name;
      if (name.StartsWith("Подтаблица Куда входит"))
      {
        viewRowParent1 = viewRowParent2;
        viewRowIndex1 = viewRowIndex2;
        break;
      }
      viewRowParent2 = parentRow;
    }
    int index = viewRowIndex1 + 1;
    viewRowParent1.InsertChildNode(index, (DocumentTreeNode) docRowVtorNew, false, true, false, true, false);
    this.DocumentControl.SetSelection(docRowVtorNew.Nodes[0], true, Point.Empty, true, false);
  }

  /// <summary> Заполнение существующей записи из Imbase </summary>
  /// <param name_From_Oglavlenie="commandState"></param>
  /// <returns></returns>
  private bool FillingExistingFromImbase(ICommandState commandState)
  {
    TableData docRowCurrent = this.DocRowCurrent();
    if (docRowCurrent == null)
      return false;
    long ObjId_Current = -1;
    string attributeValue = docRowCurrent.GetAttributeValue("ObjectIdIzd", false);
    if (attributeValue != null && attributeValue != "")
      ObjId_Current = Convert.ToInt64(attributeValue);
    long objectID = Vedomost_VB_Static.VyborFromImbase(ObjId_Current, this._one_Ved_Nastr_Curr._bases_Options_Ved._list_quickObjectInfo);
    return Math.Abs(objectID) >= 2L && this.FillingDocRow_By_ObjectID(docRowCurrent, objectID, "UserFromImbase");
  }

  /// <summary> Заполнение существующей записи из Search </summary>
  /// <param name_From_Oglavlenie="commandState"></param>
  /// <returns></returns>
  private bool FillingExistingFromDBObject(ICommandState commandState)
  {
    TableData docRowCurrent = this.DocRowCurrent();
    if (docRowCurrent == null)
      return false;
    long ObjId_Current = -1;
    string attributeValue = docRowCurrent.GetAttributeValue("ObjectIdIzd", false);
    if (attributeValue != null && attributeValue != "")
      ObjId_Current = Convert.ToInt64(attributeValue);
    long objectID = Vedomost_VB_Static.VyborFromObject(ObjId_Current, this._one_Ved_Nastr_Curr);
    return Math.Abs(objectID) >= 2L && this.FillingDocRow_By_ObjectID(docRowCurrent, objectID, "UserFromDbObject");
  }

  /// <summary> Заполнение группы выделенных записей из Imbase </summary>
  /// <param name="commandState"></param>
  /// <returns></returns>
  private bool FillingExistingFromImbaseSelected(ICommandState commandState)
  {
    DocumentTreeNode[] selectedNodes = this.DocumentControl.GetSelectedNodes();
    if (selectedNodes.Length < 1)
      return false;
    long ObjId_Current = -1;
    TableData tableData = this.DocRowCurrent();
    if (tableData != null)
    {
      string attributeValue = tableData.GetAttributeValue("ObjectIdIzd", false);
      if (attributeValue != null && attributeValue != "")
        ObjId_Current = Convert.ToInt64(attributeValue);
    }
    long objectID = Vedomost_VB_Static.VyborFromImbase(ObjId_Current, this._one_Ved_Nastr_Curr._bases_Options_Ved._list_quickObjectInfo);
    if (Math.Abs(objectID) < 2L)
      return false;
    int index = 0;
    while (index < selectedNodes.Length && this.FillingDocRow_By_ObjectID((TableData) selectedNodes[index], objectID, "UserFromImbase"))
      ++index;
    return true;
  }

  /// <summary> Заполнение группы выделенных записей из Search </summary>
  /// <param name="commandState"></param>
  /// <returns></returns>
  private bool FillingExistingFromDBObjectSelected(ICommandState commandState)
  {
    DocumentTreeNode[] selectedNodes = this.DocumentControl.GetSelectedNodes();
    if (selectedNodes.Length < 1)
      return false;
    long ObjId_Current = -1;
    TableData tableData = this.DocRowCurrent();
    if (tableData != null)
    {
      string attributeValue = tableData.GetAttributeValue("ObjectIdIzd", false);
      if (attributeValue != null && attributeValue != "")
        ObjId_Current = Convert.ToInt64(attributeValue);
    }
    long objectID = Vedomost_VB_Static.VyborFromObject(ObjId_Current, this._one_Ved_Nastr_Curr);
    if (Math.Abs(objectID) < 2L)
      return false;
    int index = 0;
    while (index < selectedNodes.Length && this.FillingDocRow_By_ObjectID((TableData) selectedNodes[index], objectID, "UserFromDbObject"))
      ++index;
    return true;
  }

  /// <summary> Заполнение существующей записи </summary>
  /// <param name="docRowCurrent"></param>
  /// <param name="objectID"></param>
  /// <param name="label"></param>
  /// <returns></returns>
  private bool FillingDocRow_By_ObjectID(TableData docRowCurrent, long objectID, string label)
  {
    if (docRowCurrent == null || Math.Abs(objectID) < 2L)
      return false;
    Vedomost_VB.RecordForVed_New vedNewFromObjectId = this._vedomost_VB_new.Create_recordForVed_New_From_ObjectID(objectID);
    if (vedNewFromObjectId == null)
      return false;
    int nodesCount = this.Document.NodesCount;
    this._vedomost_VB_new.FillingExisting_DocRow_Info(vedNewFromObjectId, this._docTemplate, this.Document, docRowCurrent);
    docRowCurrent.UpdateLayout(true);
    this._vedomost_VB_new.Filled_Record_DocRow_Ved_Attributes(docRowCurrent, vedNewFromObjectId, label);
    if (this._formaGroup_Doc == "B")
      this.Filled_Tabl_Isp_B(nodesCount);
    return true;
  }

  /// <summary> Текущую запись выводить, начиная с новой страницы </summary>
  /// <param name="commandState"></param>
  /// <returns></returns>
  private bool FromNewPage(ICommandState commandState)
  {
    this.Get_Number_PageCurrent();
    TableData tableData = this.DocRowCurrent();
    if (tableData == null)
      return false;
    int nodesCount = this.Document.NodesCount;
    tableData.FromNewPage = true;
    if (this._formaGroup_Doc == "B")
      this.Filled_Tabl_Isp_B(nodesCount);
    return true;
  }

  /// <summary> Добавить страницу в конец документа </summary>
  /// <param name="commandState"></param>
  /// <returns></returns>
  private bool AddPage_ToEndDoc(ICommandState commandState)
  {
    TableData endMainTable = this.GetEndMainTable();
    if (endMainTable != null)
    {
      TableData docRowInfo = this._vedomost_VB_new.Create_DocRow_Info((Vedomost_VB.RecordForVed_New) null, this._docTemplate, this.Document, "UserNewRowInfo", true, this.name_Page_Current);
      if (docRowInfo != null)
      {
        DocumentTreeNode endDocRow = this.GetEndDocRow();
        if (endDocRow != null)
          this.DocumentControl?.SetSelection(endDocRow, true, false);
        if (!string.IsNullOrEmpty(this._variableRowCurrent))
          docRowInfo.SetAttributeValue("Variable", this._variableRowCurrent);
        docRowInfo.SetAttributeValue("TypeRow", "Info");
        this._vedomost_VB_new.Add_AttributeTypeRec_To_DocRow(docRowInfo, "Info", "UserNewRowInfo");
        docRowInfo.FromNewPage = true;
        endMainTable.AddChildNode((DocumentTreeNode) docRowInfo, true, true);
        this.DocumentControl?.SetSelection((DocumentTreeNode) docRowInfo, true, false);
      }
      else if (this.Document.Template.FindNode("Следующая страница") is PageData node)
      {
        PageData pageData = (PageData) node.CloneFromTemplate(true, true);
        if (pageData != null)
        {
          this.Document.AddChildNode((DocumentTreeNode) pageData, false, false);
          this.DocumentControl?.SetSelection((DocumentTreeNode) pageData, true, false);
        }
      }
    }
    return true;
  }

  /// <summary> Вставить пустую страницу ПОСЛЕ текущей </summary>
  /// <param name="commandState"></param>
  /// <returns></returns>
  private bool Insert_Next_Page(ICommandState commandState) => this.Insert_Page(1);

  /// <summary> Вставить пустую страницу ПЕРЕД текущей </summary>
  /// <param name="commandState"></param>
  /// <returns></returns>
  private bool Insert_Prev_Page(ICommandState commandState) => this.Insert_Page(0);

  /// <summary> Вставка страницы </summary>
  /// <param name="n"> 0-Prev 1-Next</param>
  /// <returns></returns>
  private bool Insert_Page(int n)
  {
    if (n != 0 && n != 1 || string.IsNullOrEmpty(this.name_Page_Current) || this.number_Page_Current < 0 || this.name_Page_Next == "Заглавный лист")
      return false;
    string nodeName = "Следующая страница";
    if (this.is_Extended_List_Names_Pages_ByTemplate)
    {
      List<string> stringList = new List<string>();
      for (int index = 0; index < this.list_Names_Pages_Temlate.Count; ++index)
      {
        string str = this.list_Names_Pages_Temlate[index];
        stringList.Add(str);
      }
      if (stringList.Count > 1)
      {
        this.Removal_Excess(stringList);
        if (stringList.Count > 0)
        {
          if (stringList.Count > 1)
          {
            using (VyborFromStringList vyborFromStringList = new VyborFromStringList())
            {
              vyborFromStringList.Text = "Выбор страницы бланка";
              vyborFromStringList.stringlist = stringList;
              if (vyborFromStringList.ShowDialog() != DialogResult.OK && string.IsNullOrEmpty(vyborFromStringList.nameResult))
                return false;
              nodeName = vyborFromStringList.nameResult;
            }
          }
          else
            nodeName = stringList[0];
        }
      }
    }
    if (this.Document.Template.FindFirstChildNodeByName(nodeName) is PageData firstChildNodeByName)
    {
      PageData pageData = (PageData) firstChildNodeByName.CloneFromTemplate(true, true);
      if (pageData != null)
      {
        this.Document.InsertChildNode(this.number_Page_Current + n, (DocumentTreeNode) pageData, false, false, false, false, false);
        this.DocumentControl?.SetSelection((DocumentTreeNode) pageData, true, false);
        if (this._formaGroup_Doc == "B")
        {
          this.FillProductHeaders_OnePage(pageData, 0);
          if (this.variablesCount > this._one_Ved_Nastr_Curr._algorithmToPrint._kolGraf)
            Vedomost_VB_Static.FilledNumbersIspolneniyFormaB(this.Document);
        }
      }
    }
    return true;
  }

  /// <summary> Удалить текущую страницу </summary>
  /// <param name="commandState"></param>
  /// <returns></returns>
  private bool RemovePage_Curr(ICommandState commandState)
  {
    int numberPageCurrent = this.Get_Number_PageCurrent();
    string name1 = this.Document.Nodes[numberPageCurrent].Name;
    this.Document.RemoveChildNodeAt(numberPageCurrent, true, true);
    if (name1 == "Титульный лист")
    {
      this.is_TitList_In_Document = false;
      PageData node = (PageData) this.Document.Nodes[0];
      string name2 = node.Name;
      this.name_Page_Current = node.Name;
      if (this.isListZagolovki)
        this.Update_ListZagolovki();
    }
    else if (numberPageCurrent < this.Document.Nodes.Count)
    {
      PageData node = (PageData) this.Document.Nodes[numberPageCurrent];
      string name3 = node.Name;
      this.name_Page_Current = node.Name;
    }
    this.is_LIZM_In_Document = Vedomost_VB_Static.Is_LIZM_In_Document((DocumentTreeNode) this.Document);
    this.is_RemarkPage_In_Document = Vedomost_VB_Static.Is_RemarkPage_In_Document((DocumentTreeNode) this.Document);
    return true;
  }

  /// <summary> Добавить Лист регистрации изменений </summary>
  /// <param name="commandState"></param>
  /// <returns></returns>
  private bool AddLRIPage(ICommandState commandState)
  {
    ImDocument imDocument = DocumentEditorPlugin.LoadDocumentFromDBObject(this._templateID);
    if (this.n_LIZM_In_Template <= -1)
      return false;
    PageData node = (PageData) imDocument.Nodes[this.n_LIZM_In_Template];
    AVSPlugin.Instance.ActiveImDocumentEditorForm.Document.AddChildNode((DocumentTreeNode) node, true, true);
    this.DocumentControl.SetSelection((DocumentTreeNode) node, true, Point.Empty, true, false);
    this.is_LIZM_In_Document = true;
    return true;
  }

  /// <summary> Добавить Титульный лист </summary>
  /// <param name="commandState"></param>
  /// <returns></returns>
  private bool AddTitList(ICommandState commandState)
  {
    ImDocument imDocument = DocumentEditorPlugin.LoadDocumentFromDBObject(this._templateID);
    if (this.n_TitList_In_Template <= -1)
      return false;
    AVSPlugin.Instance.ActiveImDocumentEditorForm.Document.InsertChildNode(0, imDocument.Nodes[this.n_TitList_In_Template], true, true, true, true, false);
    this.is_TitList_In_Document = true;
    if (this.isListZagolovki)
      this.Update_ListZagolovki();
    return true;
  }

  private bool Create_RemarkPage(ICommandState commandState)
  {
    ImDocument imDocument = DocumentEditorPlugin.LoadDocumentFromDBObject(this._templateID);
    if (this.n_RemarkPage_In_Template > -1)
    {
      PageData node = (PageData) imDocument.Nodes[this.n_RemarkPage_In_Template];
      ImDocument document = AVSPlugin.Instance.ActiveImDocumentEditorForm.Document;
      document.InsertChildNode(Vedomost_VB_Static.FindNumberEndInfoPage(document) + 1, (DocumentTreeNode) node, true, true, true, true, false);
      this.DocumentControl.SetSelection((DocumentTreeNode) node, true, Point.Empty, true, false);
      this.is_RemarkPage_In_Document = true;
      return true;
    }
    int num = (int) MessageBox.Show("В шаблоне нет листа \"Примечания\"", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    return false;
  }

  /// <summary> Убрать признак "С новой страницы" </summary>
  /// <param name="commandState"></param>
  /// <returns></returns>
  private bool DeleteFromNewPage(ICommandState commandState)
  {
    TableData tableData = this.DocRowCurrent();
    if (tableData == null)
      return false;
    tableData.FromNewPage = false;
    return true;
  }

  /// <summary>Групповой ввод из Imbase</summary>
  /// <param name_From_Oglavlenie="commandState">Выполняемая команда</param>
  /// <returns>Возвращает true, если команда обработана. False, если требуется другой обработчик</returns>
  private bool AddRowsGroupFromImbase(ICommandState commandState)
  {
    this.group_razdel_Ved = "1";
    if (this._one_Ved_Nastr_Curr._bases_Options_Ved._list_quickObjectInfo == null || this._one_Ved_Nastr_Curr._bases_Options_Ved._list_quickObjectInfo.Count == 0)
    {
      int num = (int) MessageBox.Show("Настройки ввода из Imbase отсутствуют", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      return false;
    }
    if (this._one_Ved_Nastr_Curr._bases_Options_Ved._list_quickObjectInfo != null && this._one_Ved_Nastr_Curr._bases_Options_Ved._list_quickObjectInfo.Count > 0 && string.IsNullOrEmpty(this._one_Ved_Nastr_Curr._bases_Options_Ved._list_quickObjectInfo[this._one_Ved_Nastr_Curr._bases_Options_Ved._list_quickObjectInfo.Count - 1].Caption))
    {
      int num = (int) MessageBox.Show("Настройки ввода из Imbase отсутствуют", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      return false;
    }
    if (this._one_Ved_Nastr_Curr._algorithmToPrint._oneRecordToPrint_Info != null)
    {
      this.group_razdel_Ved = "1";
    }
    else
    {
      this.group_razdel_Ved = this.Razdel_Ved_Curr();
      if (string.IsNullOrEmpty(this.group_razdel_Ved) || this.group_razdel_Ved == "0")
      {
        this.group_razdel_Ved = this.Vybor_Razdel_Ved();
        if (this.group_razdel_Ved == "-1")
          return false;
      }
    }
    if (AVSPlugin.ImbaseSelector != null)
    {
      List<object> catalogId = new List<object>();
      long contextObjsID = -1;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (this._one_Ved_Nastr_Curr._bases_Options_Ved._list_quickObjectInfo != null && this._one_Ved_Nastr_Curr._bases_Options_Ved._list_quickObjectInfo.Count > 0)
        {
          for (int index = 0; index < this._one_Ved_Nastr_Curr._bases_Options_Ved._list_quickObjectInfo.Count; ++index)
          {
            QuickObjectInfo quickObjectInfo = this._one_Ved_Nastr_Curr._bases_Options_Ved._list_quickObjectInfo[index];
            if (quickObjectInfo.VersionGuid != Guid.Empty)
            {
              IDBObject dbObject = sessionKeeper.Session.GetObject(quickObjectInfo.VersionGuid, false);
              if (dbObject != null)
                catalogId.Add((object) dbObject.ObjectID);
            }
          }
        }
        else
        {
          IDBObject dbObject1 = sessionKeeper.Session.GetObject(Vedomost_VB_Static.GuidImbaseConctructorsky, false);
          if (dbObject1 != null)
            catalogId.Add((object) dbObject1.ObjectID);
          IDBObject dbObject2 = sessionKeeper.Session.GetObject(Vedomost_VB_Static.GuidImbaseMaterialy, false);
          if (dbObject2 != null)
            catalogId.Add((object) dbObject2.ObjectID);
        }
      }
      AVSPlugin.ImbaseSelector.DynamicSelection("Выберите изделия", "Выберите изделия, которые необходимо добавить в документ", (object) catalogId, false, true, -1, new DynamicSelectionEventHandler(this.ImBaseSelectionHandler), contextObjsID);
    }
    return true;
  }

  /// <summary> Определяем тип текущей "записи"  </summary>
  private Vedomost_VB_Static.TypeRow CheckTypeDocRow(ICommandState commandState)
  {
    TextBoxElement textBoxElement = (TextBoxElement) null;
    this._isItogo = false;
    DocumentTreeNode[] contextForContextMenu = NodeContextMenu.ContextForContextMenu;
    if (contextForContextMenu == null || !NodeContextMenu.ContextMenuCommand)
    {
      DocumentTreeNode[] selectedNodes = this.DocumentControl.GetSelectedNodes();
      if (selectedNodes != null && selectedNodes.Length != 0)
        textBoxElement = selectedNodes[0] as TextBoxElement;
    }
    else
      textBoxElement = contextForContextMenu[0] as TextBoxElement;
    if (textBoxElement != null)
    {
      int num = textBoxElement.InPlaceEditorActive ? 1 : 0;
    }
    if (this.DocumentControl.GetSelectedNodes().Length > 1)
      return Vedomost_VB_Static.TypeRow.Undefined;
    DocumentTreeNode[] selectedNodes1 = this.DocumentControl.GetSelectedNodes();
    if (selectedNodes1.Length == 0)
      return Vedomost_VB_Static.TypeRow.Undefined;
    DocumentTreeNode documentTreeNode = selectedNodes1[0];
    DocumentTreeNode parent1 = documentTreeNode.Parent;
    System.Type type1 = documentTreeNode.GetType();
    string name1 = documentTreeNode.Name;
    string name2 = type1.Name;
    string str = (string) null;
    if (parent1 != null)
    {
      System.Type type2 = parent1.GetType();
      str = parent1.Name;
      if (string.IsNullOrEmpty(str))
        str = parent1.Id;
      string name3 = type2.Name;
    }
    switch (name1)
    {
      case "Главная таблица":
        this._typeRowCurr = Vedomost_VB_Static.TypeRow.MainTab;
        this._kudaVhodit_Skolko = -1;
        this._kudaVhodit_Index = -1;
        this._isItogo = false;
        this._currKudaVhoditRow = (TableData) null;
        return this._typeRowCurr;
      case "Кол итого":
      case "Подтаблица для количества":
label_17:
        this._typeRowCurr = Vedomost_VB_Static.TypeRow.Itogo;
        this._currKudaVhoditRow = (TableData) parent1;
        return this._typeRowCurr;
      default:
        switch (str)
        {
          case "Подтаблица 'всего'":
          case "Строка Кол итого":
            goto label_17;
          case "Строка Куда входит":
label_19:
            this._typeRowCurr = Vedomost_VB_Static.TypeRow.KudaVhodit;
            this._currKudaVhoditRow = (TableData) parent1;
            TableData rowParent;
            int rowIndex = (documentTreeNode as RectangleElement).GetRowIndex(out rowParent);
            this._kudaVhodit_Skolko = rowParent.Nodes.Count;
            if (rowParent.Nodes.Count > 1)
            {
              if (rowParent.Nodes[rowParent.Nodes.Count - 1].Name == "Строка Кол итого")
              {
                this._isItogo = true;
                --this._kudaVhodit_Skolko;
                this._kudaVhodit_Index = rowIndex;
              }
              else if (this._kudaVhodit_Skolko > 1)
                this._kudaVhodit_Index = rowIndex;
            }
            else
            {
              if (rowParent.Nodes.Count == 0)
                this._kudaVhodit_Index = -1;
              if (rowParent.Nodes.Count == 1)
                this._kudaVhodit_Index = 0;
            }
            return this._typeRowCurr;
          default:
            switch (name1)
            {
              case "Подтаблица Куда входит":
                goto label_19;
              case "Основная строка":
                this._typeRowCurr = Vedomost_VB_Static.TypeRow.Info;
                return this._typeRowCurr;
              default:
                if (!(str == "Основная строка"))
                {
                  if (name1 == "Заголовок" || str == "Заголовок")
                  {
                    this._typeRowCurr = Vedomost_VB_Static.TypeRow.Zagolovok;
                    return this._typeRowCurr;
                  }
                  if (name1 == "Подзаголовок" || str == "Подзаголовок")
                  {
                    this._typeRowCurr = Vedomost_VB_Static.TypeRow.Podzagolovok;
                    return this._typeRowCurr;
                  }
                  if (name1 == "Часть" || str == "Часть")
                  {
                    this._typeRowCurr = Vedomost_VB_Static.TypeRow.TitlePart;
                    return this._typeRowCurr;
                  }
                  if (name1 == "Дополнительная 1" || str == "Дополнительная 1")
                  {
                    this._typeRowCurr = Vedomost_VB_Static.TypeRow.Additional1;
                    return this._typeRowCurr;
                  }
                  if (name1 == "Дополнительная 2" || str == "Дополнительная 2")
                  {
                    this._typeRowCurr = Vedomost_VB_Static.TypeRow.Additional2;
                    return this._typeRowCurr;
                  }
                  if (name1 == "Дополнительная 3" || str == "Дополнительная 3")
                  {
                    this._typeRowCurr = Vedomost_VB_Static.TypeRow.Additional3;
                    return this._typeRowCurr;
                  }
                  if (name1 == "Дополнительная 4" || str == "Дополнительная 4")
                  {
                    this._typeRowCurr = Vedomost_VB_Static.TypeRow.Additional4;
                    return this._typeRowCurr;
                  }
                  if (name1 == "Пустая строка" || str == "Пустая строка")
                  {
                    this._typeRowCurr = Vedomost_VB_Static.TypeRow.Empty;
                    return this._typeRowCurr;
                  }
                  if (name1 == "Длинная строка" || str == "Длинная строка")
                  {
                    this._typeRowCurr = Vedomost_VB_Static.TypeRow.Dlinaia;
                    return this._typeRowCurr;
                  }
                  if (!(name1 == "Примечание короткое"))
                  {
                    switch (str)
                    {
                      case "Примечание короткое":
                        break;
                      case "Заголовок таблицы":
                        this._typeRowCurr = Vedomost_VB_Static.TypeRow.Main;
                        return this._typeRowCurr;
                      default:
                        if (parent1 != null && (parent1.NodeClass == "ImDocument" || parent1.NodeClass == "Page"))
                        {
                          this._typeRowCurr = Vedomost_VB_Static.TypeRow.Main;
                          return this._typeRowCurr;
                        }
                        if (parent1 != null && parent1.Parent != null)
                        {
                          DocumentTreeNode parent2 = parent1.Parent;
                          string name4 = parent2.Name;
                          if (name4 == "Таблица изменений" || str == "Таблица изменений" || name1 == "Таблица изменений")
                          {
                            this._typeRowCurr = Vedomost_VB_Static.TypeRow.LRI;
                            if (name4 == "Таблица изменений")
                              this._lriTable = (TableData) parent2;
                            if (str == "Таблица изменений")
                              this._lriTable = (TableData) parent1;
                            if (name1 == "Таблица изменений")
                              this._lriTable = (TableData) documentTreeNode;
                            if (str != "Таблица изменений" && str != "Лист регистрации изменений")
                              this._contextLRIRow = (TableData) parent1;
                            return this._typeRowCurr;
                          }
                        }
                        if (parent1 != null && parent1.Parent != null)
                        {
                          for (int index = 0; index < 12; ++index)
                          {
                            if (parent1.Parent != null)
                            {
                              parent1 = parent1.Parent;
                              if (parent1 != null && (parent1.NodeClass == "ImDocument" || parent1.NodeClass == "Page"))
                              {
                                this._typeRowCurr = Vedomost_VB_Static.TypeRow.Main;
                                return this._typeRowCurr;
                              }
                            }
                          }
                        }
                        return Vedomost_VB_Static.TypeRow.Undefined;
                    }
                  }
                  this._typeRowCurr = Vedomost_VB_Static.TypeRow.RemarkShort;
                  return this._typeRowCurr;
                }
                goto case "Основная строка";
            }
        }
    }
  }

  /// <summary> Получение текущей строки документа (таблицы) </summary>
  /// <returns></returns>
  private TableData DocRowCurrent()
  {
    TableData tableData = (TableData) null;
    DocumentTreeNode[] selectedNodes = this.DocumentControl.GetSelectedNodes();
    if (selectedNodes != null && selectedNodes.Length != 0)
    {
      DocumentTreeNode documentTreeNode = selectedNodes[0];
      if (documentTreeNode.Name == "Главная таблица")
      {
        tableData = (TableData) null;
      }
      else
      {
        if (documentTreeNode.Parent != null && documentTreeNode.Parent.Name == "Главная таблица" && documentTreeNode.NodeClass != "VirtualColumn" && documentTreeNode.NodeClass != "TextBoxElement")
          tableData = (TableData) documentTreeNode;
        if (documentTreeNode.Parent != null && documentTreeNode.Parent.Parent != null && documentTreeNode.Parent.Parent.Name == "Главная таблица" && documentTreeNode.Parent.NodeClass != "VirtualColumn")
          tableData = (TableData) documentTreeNode.Parent;
      }
    }
    this._variableRowCurrent = tableData == null ? "" : tableData.GetAttributeValue("Variable", true);
    return tableData;
  }

  private void Check_PageCurrent()
  {
    this.Get_Number_PageCurrent();
    this._pageCurrent = this.Get_PageCurrent();
    if (this._pageCurrent != null)
      this._mainTableCurrent = (TableData) this._pageCurrent.FindFirstChildNodeByName("Главная таблица");
    else
      this._mainTableCurrent = (TableData) null;
  }

  /// <summary> Текущая страница </summary>
  /// <returns></returns>
  private DocumentTreeNode Get_PageCurrent()
  {
    if (this.Document == null)
      return (DocumentTreeNode) null;
    DocumentTreeNode pageCurrent = (DocumentTreeNode) null;
    DocumentTreeNode[] selectedNodes = this.DocumentControl.GetSelectedNodes();
    if (selectedNodes != null && selectedNodes.Length != 0)
    {
      for (DocumentTreeNode parent = selectedNodes[0]; parent != null; parent = parent.Parent)
      {
        if (parent.NodeClass == "Page")
        {
          pageCurrent = parent;
          this.name_Page_Current = pageCurrent.Name;
          this.typePageVedom = Vedomost_VB_Static.TypePageVedom.Info;
          switch (this.name_Page_Current)
          {
            case "Титульный лист":
              this.typePageVedom = Vedomost_VB_Static.TypePageVedom.TitList;
              break;
            case "Лист регистрации изменений":
              this.typePageVedom = Vedomost_VB_Static.TypePageVedom.LRI;
              break;
            case "Примечания":
              this.typePageVedom = Vedomost_VB_Static.TypePageVedom.REMARK;
              break;
          }
          this.name_Page_Next = "";
          int afterCurrentPage = this.DocumentControl.GetIndexAfterCurrentPage();
          if (afterCurrentPage < this.Document.NodesCount)
          {
            this.name_Page_Next = this.Document.Nodes[afterCurrentPage].Name;
            break;
          }
          break;
        }
      }
    }
    return pageCurrent;
  }

  /// <summary> Номер текущей страницы </summary>
  /// <returns></returns>
  private int Get_Number_PageCurrent()
  {
    if (this.Document == null)
      return 0;
    int numberPageCurrent = this.DocumentControl.GetIndexAfterCurrentPage() - 1;
    this.number_Page_Current = numberPageCurrent;
    return numberPageCurrent;
  }

  /// <summary> Определяем данные страниц </summary>
  private void Calculate_Pages()
  {
    this.number_Page_Current = this.Get_Number_PageCurrent();
    string name = this.Document.Nodes[this.number_Page_Current].Name;
    this.number_Page_First_Info = -1;
    this.number_Page_End_Info = -1;
    this.quanty_Info_Pages = 0;
    this.number_Page_First_CurrentName = -1;
    this.number_Page_End_CurrentName = -1;
    string str1 = "";
    for (int index = 0; index < this.Document.NodesCount; ++index)
    {
      string str2 = str1;
      DocumentTreeNode node = this.Document.Nodes[index];
      str1 = node.Name;
      string nodeClass = node.NodeClass;
      if (!(node.Id == "TL") && !(node.Id == "Титульный лист"))
      {
        if (node.Id == "LRI" || node.Id == "Лист регистрации изменений")
        {
          this.number_Page_End_Info = index - 1;
          if (this.number_Page_End_CurrentName == -1)
          {
            this.number_Page_End_CurrentName = this.number_Page_End_Info;
            break;
          }
          break;
        }
        if (nodeClass == "Page")
        {
          if (this.number_Page_First_Info == -1)
            this.number_Page_First_Info = index;
          if (str2 == "Титульный лист")
          {
            if (name == "Заглавный лист" || name == "Следующая страница")
              this.number_Page_First_CurrentName = index;
          }
          else if (!(str2 == str1) && (!(str2 == "Заглавный лист") || !(str1 == "Следующая страница")))
          {
            if (this.number_Page_First_CurrentName == -1)
            {
              if (name == str1)
                this.number_Page_First_CurrentName = index;
            }
            else if (this.number_Page_End_CurrentName == -1)
              this.number_Page_End_CurrentName = index - 1;
          }
        }
      }
    }
    if (this.number_Page_End_Info == -1)
    {
      this.number_Page_End_Info = this.Document.NodesCount - 1;
      if (this.number_Page_End_CurrentName == -1)
        this.number_Page_End_CurrentName = this.number_Page_End_Info;
    }
    this.quanty_Info_Pages = this.number_Page_End_Info - this.number_Page_First_Info + 1;
  }

  /// <summary> Получение текущей подстроки документа (таблицы) </summary>
  /// <returns></returns>
  private TableData DocPodRowCurrent()
  {
    TableData tableData = (TableData) null;
    DocumentTreeNode[] selectedNodes = this.DocumentControl.GetSelectedNodes();
    if (selectedNodes == null || selectedNodes.Length == 0)
      return (TableData) null;
    DocumentTreeNode documentTreeNode = selectedNodes[0];
    System.Type type1 = documentTreeNode.GetType();
    string name1 = documentTreeNode.Name;
    string name2 = type1.Name;
    switch (name1)
    {
      case "Строка Куда входит":
        return (TableData) documentTreeNode;
      case "Строка Кол итого":
        return (TableData) documentTreeNode;
      default:
        DocumentTreeNode parent = documentTreeNode.Parent;
        string str = (string) null;
        if (parent != null)
        {
          System.Type type2 = parent.GetType();
          str = parent.Name;
          string name3 = type2.Name;
        }
        if (str == "Подтаблица 'всего'" || str == "Подтаблица для количества")
        {
          parent = parent.Parent;
          System.Type type3 = parent.GetType();
          str = parent.Name;
          string name4 = type3.Name;
        }
        switch (str)
        {
          case "Строка Куда входит":
            return (TableData) parent;
          case "Строка Кол итого":
            return (TableData) parent;
          default:
            return tableData;
        }
    }
  }

  /// <summary> Команда "Вставить запись в ЛРИ перед" </summary>
  /// <param name_From_Oglavlenie="commandState"></param>
  /// <returns></returns>
  private bool AddLRIRecord_Before(ICommandState commandState) => this.AddLRIRecord(false);

  /// <summary> Команда "Вставить запись в ЛРИ после" </summary>
  /// <param name_From_Oglavlenie="commandState"></param>
  /// <returns></returns>
  private bool AddLRIRecord_After(ICommandState commandState) => this.AddLRIRecord(true);

  /// <summary> Команда "Вставить запись в ЛРИ" </summary>
  /// <param name_From_Oglavlenie="commandState"></param>
  /// <returns></returns>
  private bool AddLRIRecord(bool after)
  {
    if (!this._isVveliOsnovnyeDannye)
      return false;
    TableData lriRow = this._vedomost_VB_new.Create_LriRow(this._docTemplate);
    DocumentTreeNode[] selectedNodes = this.DocumentControl.GetSelectedNodes();
    if (selectedNodes.Length != 0)
    {
      TableElement viewRowParent1;
      int viewRowIndex1;
      VedomostEditorWindow.FindRowParent(selectedNodes[0], out viewRowParent1, out viewRowIndex1);
      string name = viewRowParent1.Name;
      int num = 0;
      TableElement viewRowParent2 = viewRowParent1;
      for (; !name.StartsWith("Таблица изменений") && num < 5; ++num)
      {
        TableElement parentRow = (TableElement) viewRowParent2.FindParentRow(false);
        int viewRowIndex2;
        VedomostEditorWindow.FindRowParent((DocumentTreeNode) parentRow, out viewRowParent2, out viewRowIndex2);
        name = viewRowParent2.Name;
        if (name.StartsWith("Таблица изменений"))
        {
          viewRowIndex1 = viewRowIndex2;
          break;
        }
        viewRowParent2 = parentRow;
      }
      if (after)
        ++viewRowIndex1;
      if (viewRowIndex1 < 0)
        viewRowIndex1 = 0;
      this._lriTable.InsertChildNode(viewRowIndex1, (DocumentTreeNode) lriRow, false, true, false, true, false);
      this.DocumentControl.SetSelection(lriRow.Nodes[1], true, Point.Empty, true, false);
    }
    return true;
  }

  /// <summary> Вставка при групповом вводе </summary>
  /// <param name_From_Oglavlenie="objectId"></param>
  /// <param name_From_Oglavlenie="mode"></param>
  /// <returns></returns>
  private bool ImBaseSelectionHandler(long objectId, DynamicSelectionMode mode)
  {
    ImDocument document = this.Document;
    try
    {
      if (mode == DynamicSelectionMode.Select)
      {
        if (objectId == -1L || objectId == 0L)
          return false;
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          if (sessionKeeper.Session.GetObject(objectId) == null)
            return false;
        }
        this.AddVedRow_ByObjectID(objectId, "Imbase", this.group_razdel_Ved, true);
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
    return true;
  }

  /// <summary> Создание списка заголовков (оглавления). Шаг 1. Только тексты заголовков </summary>
  /// <param name_From_Oglavlenie="document"></param>
  /// <returns></returns>
  private List<Vedomost_VB.RecordForVed_New> CreateListZagol_Step1_Only_Names(ImDocument document)
  {
    List<Vedomost_VB.RecordForVed_New> zagolStep1OnlyNames = new List<Vedomost_VB.RecordForVed_New>();
    string attributeValue = document.GetAttributeValue("_nameArticle", true);
    for (int index1 = 0; index1 < document.NodesCount; ++index1)
    {
      TableData firstNodeByName = (TableData) (document.Nodes[index1] as PageData).FindFirstNodeByName("Главная таблица");
      if (firstNodeByName != null)
      {
        for (int index2 = 0; index2 < firstNodeByName.NodesCount; ++index2)
        {
          DocumentTreeNode node1 = firstNodeByName.Nodes[index2];
          if (node1 != null && node1.TemplateId == "Заголовок")
          {
            for (int index3 = 0; index3 < node1.NodesCount; ++index3)
            {
              DocumentTreeNode node2 = node1.Nodes[index3];
              if (node2 != null && node2.TemplateId == "Текст заголовка")
              {
                string text = ((TextData) node2).Text;
                if (!string.IsNullOrEmpty(text))
                {
                  Vedomost_VB.RecordForVed_New recordForVedNew = new Vedomost_VB.RecordForVed_New();
                  recordForVedNew.TypeRec = !string.IsNullOrEmpty(attributeValue) && text.StartsWith(attributeValue) || text.StartsWith("Переменные") ? Vedomost_VB.TypeRec.TitleIsp : Vedomost_VB.TypeRec.Oglavlenie;
                  recordForVedNew.Set_Name(text);
                  zagolStep1OnlyNames.Add(recordForVedNew);
                }
              }
            }
          }
        }
      }
    }
    return zagolStep1OnlyNames;
  }

  /// <summary> Занесение текстов заголовков в ДОКУМЕНТ. Шаг 2. </summary>
  /// 
  ///             При этом строки на страницах перемещаются
  ///             но номеров сраниц еще нет
  ///             <param name_From_Oglavlenie="document"></param>
  /// <param name_From_Oglavlenie="listOglavlenie"></param>
  private void Add_Zagolovki_ToDocument(
    ImDocument document,
    List<Vedomost_VB.RecordForVed_New> listOglavlenie)
  {
    if (document == null || listOglavlenie == null || listOglavlenie.Count == 0)
      return;
    int index1 = !this.is_TitList_In_Document ? 0 : 1;
    if (document.NodesCount < index1 - 1)
      return;
    TableData firstNodeByName1 = (TableData) (document.Nodes[index1] as PageData).FindFirstNodeByName("Главная таблица");
    if (firstNodeByName1 == null)
      return;
    TableData docRowEmpty1 = this._vedomost_VB_new.Create_DocRow_Empty(this._docTemplate, "", this.name_Page_Current);
    docRowEmpty1.SetAttributeValue("Oglav", "Empty");
    this._vedomost_VB_new.Add_AttributeTypeRec_To_DocRow(docRowEmpty1, "Empty", "UserOglav");
    firstNodeByName1.InsertChildNode(0, (DocumentTreeNode) docRowEmpty1, false, true, false, true, false);
    for (int index2 = listOglavlenie.Count - 1; index2 > -1; --index2)
    {
      Vedomost_VB.RecordForVed_New recordForVed_New = listOglavlenie[index2];
      TableData docRowInfo = this._vedomost_VB_new.Create_DocRow_Info(recordForVed_New, this._docTemplate, this.Document, "UserOglav", false, this.name_Page_Current);
      if (docRowInfo != null)
      {
        if (recordForVed_New.TypeRec == Vedomost_VB.TypeRec.TitleIsp)
        {
          TextData firstNodeByName2 = (TextData) docRowInfo.FindFirstNodeByName("Наименование");
          if (firstNodeByName2 != null)
          {
            CharFormat charFormat = firstNodeByName2.CharFormat.Clone();
            charFormat.Underline = new UnderlineStyle?(UnderlineStyle.Underline);
            firstNodeByName2.SetCharFormat(charFormat, false, false);
            TableData docRowEmpty2 = this._vedomost_VB_new.Create_DocRow_Empty(this._docTemplate, "", this.name_Page_Current);
            docRowEmpty2.SetAttributeValue("Oglav", "Empty");
            this._vedomost_VB_new.Add_AttributeTypeRec_To_DocRow(docRowEmpty2, "Empty", "UserOglav");
            firstNodeByName1.InsertChildNode(0, (DocumentTreeNode) docRowEmpty2, false, true, false, true, false);
          }
        }
        docRowInfo.SetAttributeValue("Oglav", "Zagolovok");
        this._vedomost_VB_new.Add_AttributeTypeRec_To_DocRow(docRowInfo, "Oglavlenie", "UserOglav");
        firstNodeByName1.InsertChildNode(0, (DocumentTreeNode) docRowInfo, false, true, false, true, false);
      }
    }
  }

  /// <summary> Удаление списка заголовков </summary>
  /// <param name="document"></param>
  private void Delete_Zagolovki_FromDocument(ImDocument document)
  {
    if (document == null)
      return;
    int index = !this.is_TitList_In_Document ? 0 : 1;
    if (document.NodesCount < index - 1)
      return;
    while (true)
    {
      TableData firstNodeByName = (TableData) (document.Nodes[index] as PageData).FindFirstNodeByName("Главная таблица");
      DocumentTreeNode documentTreeNode = (DocumentTreeNode) firstNodeByName;
      if (firstNodeByName != null)
      {
        if (firstNodeByName.NodesCount != 0)
        {
          DocumentTreeNode node = firstNodeByName.Nodes[0];
          if (node != null && !string.IsNullOrEmpty(node.GetAttributeValue("Oglav", true)))
            documentTreeNode.RemoveChildNodeAt(0, true, true);
          else
            goto label_5;
        }
        else
          goto label_7;
      }
      else
        break;
    }
    return;
label_7:
    int num = (int) MessageBox.Show("Документ не имеет записей", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    return;
label_5:;
  }

  /// <summary> Теперь страницы переформированы. Поэтому можем определить ЛИСТЫ и занести их в список заголовков </summary>
  /// <param name="document"></param>
  /// <param name="listOglavlenie"></param>
  private void Filled_NumberPages(
    ImDocument document,
    List<Vedomost_VB.RecordForVed_New> listOglavlenie)
  {
    if (document == null || listOglavlenie == null || listOglavlenie.Count == 0)
      return;
    int count = listOglavlenie.Count;
    int index1 = 0;
    Vedomost_VB.RecordForVed_New recordForVedNew1 = listOglavlenie[index1];
    Vedomost_VB.RecordForVed_New recordForVedNew2 = recordForVedNew1;
    string name1 = recordForVedNew1.Get_Name();
    int num1 = this.startPageNumber;
    int num2 = num1;
    string name2 = "";
    int num3 = 0;
    if (this.is_TitList_In_Document)
      num3 = 1;
    for (int index2 = num3; index2 < document.NodesCount; ++index2)
    {
      name2 = "";
      TableData firstNodeByName = (TableData) (document.Nodes[index2] as PageData).FindFirstNodeByName("Главная таблица");
      if (firstNodeByName != null)
      {
        for (int index3 = 0; index3 < firstNodeByName.NodesCount; ++index3)
        {
          DocumentTreeNode node1 = firstNodeByName.Nodes[index3];
          if (node1 != null)
          {
            if (node1.TemplateId == "Заголовок")
            {
              for (int index4 = 0; index4 < node1.NodesCount; ++index4)
              {
                DocumentTreeNode node2 = node1.Nodes[index4];
                if (node2 != null && node2.TemplateId == "Текст заголовка")
                {
                  string text = ((TextData) node2).Text;
                  if (!string.IsNullOrEmpty(text) && name1 == text)
                  {
                    if (index1 == 0)
                    {
                      ++index1;
                      if (index1 != listOglavlenie.Count)
                      {
                        recordForVedNew2 = recordForVedNew1;
                        recordForVedNew1 = listOglavlenie[index1];
                        name1 = recordForVedNew1.Get_Name();
                        num1 = index2 + this.startPageNumber;
                        break;
                      }
                      break;
                    }
                    name2 = "";
                    if (num1 == num2)
                      name2 = "Лист " + num1.ToString();
                    if (num2 > num1)
                      name2 = $"Листы {num1.ToString()}-{num2.ToString()}";
                    recordForVedNew2.Set_Gost(name2);
                    recordForVedNew2.Get_Name();
                    num1 = index2 + this.startPageNumber;
                    ++index1;
                    if (index1 != listOglavlenie.Count)
                    {
                      recordForVedNew2 = recordForVedNew1;
                      recordForVedNew1 = listOglavlenie[index1];
                      name1 = recordForVedNew1.Get_Name();
                    }
                    else
                      break;
                  }
                }
              }
            }
            else if (node1.TemplateId == "Основная строка")
              num2 = index2 + this.startPageNumber;
          }
        }
      }
    }
    if (num1 == num2)
      name2 = "Лист " + num1.ToString();
    if (num2 > num1)
      name2 = $"Листы {num1.ToString()}-{num2.ToString()}";
    recordForVedNew1.Set_Gost(name2);
  }

  /// <summary> Из списка заголовков номера листов выводим в оглавлениях </summary>
  /// <param name="document"></param>
  /// <param name="listOglavlenie"></param>
  private void NumbersListToOglavlenie(
    ImDocument document,
    List<Vedomost_VB.RecordForVed_New> listOglavlenie)
  {
    if (document == null || listOglavlenie == null || listOglavlenie.Count == 0)
      return;
    int index1 = 0;
    Vedomost_VB.RecordForVed_New recordForVedNew = listOglavlenie[index1];
    for (int index2 = 0; index2 < document.NodesCount; ++index2)
    {
      TableData firstNodeByName = (TableData) (document.Nodes[index2] as PageData).FindFirstNodeByName("Главная таблица");
      if (firstNodeByName != null)
      {
        for (int index3 = 0; index3 < firstNodeByName.NodesCount; ++index3)
        {
          DocumentTreeNode node1 = firstNodeByName.Nodes[index3];
          if (node1 != null && node1.TemplateId == "Основная строка")
          {
            for (int index4 = 0; index4 < node1.NodesCount; ++index4)
            {
              DocumentTreeNode node2 = node1.Nodes[index4];
              if (node2 != null && node2.TemplateId == "Обозначение документа на поставку")
              {
                ((TextData) node2).Text = recordForVedNew.Get_Gost();
                break;
              }
            }
            ++index1;
            if (index1 == listOglavlenie.Count || index1 > listOglavlenie.Count)
              return;
            recordForVedNew = listOglavlenie[index1];
          }
        }
      }
    }
  }

  /// <summary> Команда создания файла XML </summary>
  /// <param name="commandState"></param>
  /// <returns></returns>
  private bool CreateXmlFile_FromDocument(ICommandState commandState)
  {
    ImDocument document = AVSPlugin.Instance.ActiveImDocumentEditorForm.Document;
    return document != null && document.NodesCount >= 1 && Vedomost_VB_Static.CreateXmlFile_FromDocument(document, this._vedomost_VB_new, this.is_Extended_List_Names_Pages_ByTemplate);
  }

  /// <summary> Команда чтения файла XML </summary>
  /// <param name="commandState"></param>
  /// <returns></returns>
  private bool Read_From_XmlFile_To_Document(ICommandState commandState)
  {
    ImDocument document = AVSPlugin.Instance.ActiveImDocumentEditorForm.Document;
    return document != null && Vedomost_VB_Static.Filled_Data_From_XmlFile_To_Document(document, this._vedomost_VB_new, this.is_Extended_List_Names_Pages_ByTemplate);
  }

  /// <summary> Команда "Читать данные из файла AVS6" </summary>
  /// <param name="commandState"></param>
  /// <returns></returns>
  public bool Filled_Data_From_File_Avs6(ICommandState commandState)
  {
    ImDocument document = AVSPlugin.Instance.ActiveImDocumentEditorForm.Document;
    if (document == null || List_Element_Accord_Avs6_Ips.list_Element_Accord_Avs6_Ips == null || List_Element_Accord_Avs6_Ips.list_Element_Accord_Avs6_Ips.Count == 0)
      return false;
    Element_Accord_Avs6_Ips element_Accord_Avs6_Ips = Vedomost_VB_Static.Classification_Element_Accord_Avs6_Ips_By_Document(document);
    string attributeValue = document.GetAttributeValue("GroupForm", false);
    ElDocList elDocList = Vedomost_VB_Static.Classification_ElDocList_By_Element_Accord_Avs6_Ips(element_Accord_Avs6_Ips);
    string str1 = "*";
    string str2 = "";
    AVS6_File aVS6File = (AVS6_File) null;
    if (elDocList == null)
    {
      if (MessageBox.Show("Текущему документу не найдено соответствующего типа документа AVS6\r\n\r\n" + "Произвести выбор файла в диалоге?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
        return false;
    }
    else
      str1 = elDocList._fileType;
    try
    {
      using (OpenFileDialog openFileDialog = new OpenFileDialog())
      {
        openFileDialog.InitialDirectory = "D:\\IM\\IMWORK";
        openFileDialog.Filter = $"Файлы {str1} (*.{str1})|*.{str1}";
        openFileDialog.FilterIndex = 1;
        openFileDialog.RestoreDirectory = true;
        if (openFileDialog.ShowDialog() != DialogResult.OK)
          return false;
        str2 = openFileDialog.FileName;
        if (!string.IsNullOrEmpty(str2))
        {
          aVS6File = new AVS6_File();
          if (aVS6File == null || !aVS6File.Read(str2))
            return false;
          aVS6File.Synchronization_With_IPS();
          AVSDocumentForm groupForm = aVS6File.GroupForm;
          string str3 = "Не совпадает форма групповых документов";
          string text = "";
          string str4 = "Ввод данных невозможен";
          if (attributeValue == "B" && groupForm != AVSDocumentForm.B)
            text = $"{str3}\r\n\r\nТекущий документ имеет форму Б\r\nФайл Avs6 не формы Б\r\n\r\n{str4}";
          if (attributeValue != "B" && groupForm == AVSDocumentForm.B)
            text = $"{str3}\r\n\r\nФайл Avs6 имеет форму Б\r\nТекущий докуменнт не формы Б\r\n\r\n{str4}";
          if (text != "")
          {
            int num = (int) MessageBox.Show(text, "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            return false;
          }
          if (aVS6File.Classification_By_Avs6Main() == null)
          {
            int num = (int) MessageBox.Show(!(Path.GetExtension(str2).ToUpper() == "SP") ? $"Файл\r\n\r\n{str2}\r\n\r\nне найден в списке конвертируемых из AVS6 в IPS" : $"Файл\r\n\r\n{str2}\r\n\r\nэто спецификация\r\nСпецификации - не конвертируются из AVS6 в IPS", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            return false;
          }
          if (aVS6File.Classification_By_list_Element_Accord_Avs6_Ips() == null)
          {
            int num = (int) MessageBox.Show($"Для файла\r\n\r\n{str2}\r\n\r\nне найдена настройка в списке конверттации", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            return false;
          }
        }
      }
    }
    catch (Exception ex)
    {
      int num = (int) MessageBox.Show("Ошибка чтения файла\r\n\r\n" + str2, "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    if (aVS6File == null)
      return false;
    return Vedomost_VB_Static.Filled_Data_From_Avs6File_To_Document(document, this._vedomost_VB_new, aVS6File);
  }

  /// <summary> Создать новый документ на основе данных файла AVS6 </summary>
  /// <param name="commandState"></param>
  /// <returns></returns>
  public bool Create_Document_From_Avs6File(ICommandState commandState)
  {
    Vedomost_VB_Static.Create_Document_From_Avs6File();
    return true;
  }

  /// <summary> Поиск последней ИНФОРМАЦИОННОЙ страницы </summary>
  /// <returns></returns>
  private DocumentTreeNode GetEndInfoPage()
  {
    DocumentTreeNode endInfoPage = (DocumentTreeNode) null;
    int index = this.Document.NodesCount - 1;
    while (index >= 0)
    {
      DocumentTreeNode node = this.Document.Nodes[index];
      if (node.NodeClass != "Page")
        --index;
      else if (node.Id == "LRI" || node.Id == "Лист регистрации изменений")
      {
        --index;
      }
      else
      {
        endInfoPage = node;
        break;
      }
    }
    return endInfoPage;
  }

  /// <summary> На последней странице ищем Главную таблицу </summary>
  /// <returns></returns>
  private TableData GetEndMainTable()
  {
    DocumentTreeNode endInfoPage = this.GetEndInfoPage();
    if (endInfoPage == null)
      return (TableData) null;
    string nodeTemplateId1 = "Главная таблица";
    if (!(endInfoPage.FindFirstNodeFromTemplate_Recursive(nodeTemplateId1) is TableData templateRecursive))
    {
      string nodeTemplateId2 = nodeTemplateId1 + " 2";
      templateRecursive = endInfoPage.FindFirstNodeFromTemplate_Recursive(nodeTemplateId2) as TableData;
    }
    return templateRecursive;
  }

  /// <summary> Найти последнюю запись ДОКУМЕНТА (не ЛИЗМ) </summary>
  /// <returns></returns>
  private DocumentTreeNode GetEndDocRow()
  {
    if (this.GetEndInfoPage() == null)
      return (DocumentTreeNode) null;
    TableData endMainTable = this.GetEndMainTable();
    if (endMainTable == null)
      return (DocumentTreeNode) null;
    return endMainTable.NodesCount == 0 ? (DocumentTreeNode) null : endMainTable.Nodes[endMainTable.NodesCount - 1];
  }

  /// <summary> Для формы Б заполнение номеров исполнений (Полная копия VedomostVB.FillProductHeadersOnPages) </summary>
  /// <param name="document"></param>
  public void FillProductHeadersOnPages2(ImDocument document)
  {
    int index1 = 0;
    int num1 = 0;
    foreach (PageData pageData in (ImDocumentData) document)
    {
      string attributeValue = (document.Nodes[index1] as PageData).GetAttributeValue("iCikl", true);
      if (!string.IsNullOrEmpty(attributeValue))
        num1 = int.Parse(attributeValue) * 10;
      ++index1;
      if (pageData.FindFirstNodeByName("Заголовок таблицы исполнений") is TextData firstNodeByName1)
      {
        string str = firstNodeByName1.Text;
        if (string.IsNullOrEmpty(str))
          str = Vedomost_VB_Static.Kol_na_ispoln_Template;
        firstNodeByName1.AssignText(string.Format(str + " {0} -", (object) this._designationArticle), false, true, false, false, false);
      }
      if (pageData.FindFirstNodeByName("Номера исполнений") is TableData firstNodeByName2)
      {
        int count = this._variables_Coordination.list_Captions.Count;
        int num2 = num1 + 10;
        if (this._variables_Coordination != null && this._variables_Coordination.list_Variables.Count <= count)
        {
          int index2 = -1;
          int index3 = num1;
          while (true)
          {
            if (index3 < num2 && index3 < this._variables_Coordination.list_Captions.Count)
            {
              ++index2;
              if (firstNodeByName2.Nodes[index2] is TextData node)
              {
                string listVariable = this._variables_Coordination.list_Variables[index3];
                string listCaption = this._variables_Coordination.list_Captions[index3];
                node.AssignText(listCaption, false, true, false, false, false);
              }
              ++index3;
            }
            else
              goto label_21;
          }
        }
        else
        {
          for (int index4 = 0; index4 < this._variables_Coordination.list_Captions.Count; ++index4)
          {
            if (firstNodeByName2.Nodes[index4] is TextData node)
            {
              string listCaption = this._variables_Coordination.list_Captions[index4];
              if (!string.IsNullOrEmpty(listCaption))
                node.AssignText(listCaption, false, true, false, false, false);
              else
                break;
            }
          }
          continue;
        }
      }
      else
        continue;
label_21:;
    }
  }

  /// <summary> Удаление из list недопустимых для вставки </summary>
  /// <param name="name_Page_Current"></param>
  public void Removal_Excess(List<string> stringList)
  {
    if (string.IsNullOrEmpty(this.name_Page_Current))
      return;
    bool flag = false;
    for (int index = stringList.Count - 1; index > -1; --index)
    {
      string str = stringList[index];
      if (str == "Титульный лист")
        stringList.RemoveAt(index);
      else if (this.name_Page_Current == str)
      {
        flag = true;
        if (str == "Заглавный лист")
          stringList.RemoveAt(index);
      }
      else if (this.name_Page_Current == this.name_Page_Next && str != this.name_Page_Current)
        stringList.RemoveAt(index);
      else if (str == "Лист регистрации изменений" && this.number_Page_Current < this.Document.NodesCount - 1)
        stringList.RemoveAt(index);
      else if (flag)
        stringList.RemoveAt(index);
    }
    if (stringList.Count <= 0 || string.IsNullOrEmpty(this.name_Page_Next) || !(this.name_Page_Next != "Лист регистрации изменений") || !(stringList[0] == this.name_Page_Next) || stringList.Count <= 1)
      return;
    for (int index = stringList.Count - 1; index > 0; --index)
      stringList.RemoveAt(index);
  }

  /// <summary> Проверка открыть ли команду "Создать документ заново" </summary>
  /// <returns></returns>
  private bool Check_ReCreate_Mozno() => this._typeDoc == Vedomost_VB.TypeDoc.Ved;

  /// <summary>  Заполнение списка list_of_this_type. Это Список пожих типов документа Для команды "Изменить тип документа" </summary>
  private void Filled_list_Ims_of_this_type()
  {
    this.list_Ims_of_this_type.Clear();
    if (this._one_Ved_Nastr_Curr._typeVed == Vedomost_VB.TypeVed.Undefined)
      return;
    List<One_ImsObjectType_With_One_Ved_Nastr> typeWithOneVedNastr1;
    List<Vedomost_VB_Static.One_Conformity_Template_Nastr> conformityTemplateNastrList;
    if (this._one_Ved_Nastr_Curr._typeDoc == Vedomost_VB.TypeDoc.Ved)
    {
      typeWithOneVedNastr1 = Vedomost_VB_Static._list_Ved_Arbeit_ImsObjectType_With_One_Ved_Nastr;
      conformityTemplateNastrList = Vedomost_VB_Static.List_Conformity_Template_Nastr_Ved;
    }
    else
    {
      if (this._one_Ved_Nastr_Curr._typeDoc != Vedomost_VB.TypeDoc.Tabl)
        return;
      typeWithOneVedNastr1 = Vedomost_VB_Static._list_Tabl_Arbeit_ImsObjectType_With_One_Ved_Nastr;
      conformityTemplateNastrList = Vedomost_VB_Static.List_Conformity_Template_Nastr_Tabl;
    }
    if (Vedomost_VB_Static.IsUse_New_System_ByOneNastr)
    {
      for (int index1 = 0; index1 < conformityTemplateNastrList.Count; ++index1)
      {
        Vedomost_VB_Static.One_Conformity_Template_Nastr conformityTemplateNastr = conformityTemplateNastrList[index1];
        if (conformityTemplateNastr._one_Ved_Nastr != null && conformityTemplateNastr._one_Ved_Nastr._typeVed == this._one_Ved_Nastr_Curr._typeVed && this._one_Ved_Nastr_Curr._guidTypeVed != conformityTemplateNastr._one_Ved_Nastr._guidTypeVed)
        {
          for (int index2 = 0; index2 < typeWithOneVedNastr1.Count; ++index2)
          {
            One_ImsObjectType_With_One_Ved_Nastr typeWithOneVedNastr2 = typeWithOneVedNastr1[index2];
            if (typeWithOneVedNastr2.one_Ved_Nastr._nameVed == conformityTemplateNastr._one_Ved_Nastr._nameVed)
              this.list_Ims_of_this_type.Add(typeWithOneVedNastr2);
          }
        }
      }
    }
    else
    {
      for (int index = 0; index < typeWithOneVedNastr1.Count; ++index)
      {
        One_ImsObjectType_With_One_Ved_Nastr typeWithOneVedNastr3 = typeWithOneVedNastr1[index];
        if (typeWithOneVedNastr3.one_Ved_Nastr != null && this._one_Ved_Nastr_Curr._typeVed == typeWithOneVedNastr3.one_Ved_Nastr._typeVed && this._one_Ved_Nastr_Curr._guidTypeVed != typeWithOneVedNastr3.one_Ved_Nastr._guidTypeVed)
          this.list_Ims_of_this_type.Add(typeWithOneVedNastr3);
      }
    }
  }

  /// <summary> В подменю "Добавить строку..." вставляются команды </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void miAddRecordVBMenu_BeforePopup2(object sender, MenuPopupEventArgs e)
  {
    if (!(sender is MenuButtonItem menuButtonItem1))
      return;
    menuButtonItem1.Items.Clear();
    Vedomost_VB.AlgorithmToPrint algorithmToPrint = this._one_Ved_Nastr_Curr._algorithmToPrint;
    if (this._formaGroup_Doc == "B")
      algorithmToPrint = this._one_Ved_Nastr_Curr._algorithmToPrint_B;
    menuButtonItem1.Items.Add("Основную строку");
    MenuButtonItem menuButtonItem2 = menuButtonItem1.Items[menuButtonItem1.Items.Count - 1];
    menuButtonItem2.Tag = (object) "AVS.VB.AddVedRow_Info";
    menuButtonItem2.Click += new EventHandler(this.mi_CreateRecord_Info);
    if (this.typePageVedom == Vedomost_VB_Static.TypePageVedom.TitList || this.typePageVedom == Vedomost_VB_Static.TypePageVedom.REMARK || this.typePageVedom == Vedomost_VB_Static.TypePageVedom.LRI || this.ReadOnly || this._document_readOnly || this.typePageVedom == Vedomost_VB_Static.TypePageVedom.Undefined || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Main)
      menuButtonItem2.Enabled = false;
    menuButtonItem1.Items.Add("Строку заголовка");
    MenuButtonItem menuButtonItem3 = menuButtonItem1.Items[menuButtonItem1.Items.Count - 1];
    menuButtonItem3.Tag = (object) "AVS.VB.AddVedRow_Zagolovok";
    menuButtonItem3.Click += new EventHandler(this.mi_CreateRecord_Zagolovok);
    if (this.typePageVedom == Vedomost_VB_Static.TypePageVedom.TitList || this.typePageVedom == Vedomost_VB_Static.TypePageVedom.REMARK || this.typePageVedom == Vedomost_VB_Static.TypePageVedom.LRI || this.ReadOnly || this._document_readOnly || this.typePageVedom == Vedomost_VB_Static.TypePageVedom.Undefined || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Main)
      menuButtonItem3.Enabled = false;
    if (this._vedomost_VB_new != null && this._one_Ved_Nastr_Curr != null && algorithmToPrint._oneRecordToPrintTitlePodSection != null)
    {
      menuButtonItem1.Items.Add("Строку подзаголовка");
      MenuButtonItem menuButtonItem4 = menuButtonItem1.Items[menuButtonItem1.Items.Count - 1];
      menuButtonItem4.Tag = (object) "AVS.VB.AddVedRow_PodZagolovok";
      menuButtonItem4.Click += new EventHandler(this.mi_CreateRecord_PodZagolovok);
      if (this.typePageVedom == Vedomost_VB_Static.TypePageVedom.TitList || this.typePageVedom == Vedomost_VB_Static.TypePageVedom.REMARK || this.typePageVedom == Vedomost_VB_Static.TypePageVedom.LRI || this.ReadOnly || this._document_readOnly || this.typePageVedom == Vedomost_VB_Static.TypePageVedom.Undefined || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Main)
        menuButtonItem4.Enabled = false;
    }
    if (this._vedomost_VB_new != null && this._one_Ved_Nastr_Curr != null && algorithmToPrint._oneRecordToPrintTitlePart != null)
    {
      menuButtonItem1.Items.Add("Строку заголовка \"части\"");
      MenuButtonItem menuButtonItem5 = menuButtonItem1.Items[menuButtonItem1.Items.Count - 1];
      menuButtonItem5.Tag = (object) "AVS.VB.AddVedRow_TitlePart";
      menuButtonItem5.Click += new EventHandler(this.mi_CreateRecord_TitlePart);
      if (this.typePageVedom == Vedomost_VB_Static.TypePageVedom.TitList || this.typePageVedom == Vedomost_VB_Static.TypePageVedom.REMARK || this.typePageVedom == Vedomost_VB_Static.TypePageVedom.LRI || this.ReadOnly || this._document_readOnly || this.typePageVedom == Vedomost_VB_Static.TypePageVedom.Undefined || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Main)
        menuButtonItem5.Enabled = false;
    }
    menuButtonItem1.Items.Add("Строку примечание");
    MenuButtonItem menuButtonItem6 = menuButtonItem1.Items[menuButtonItem1.Items.Count - 1];
    menuButtonItem6.Tag = (object) "AVS.VB.AddVedRow_Remark";
    menuButtonItem6.Click += new EventHandler(this.mi_CreateRecord_Remark);
    if (this.typePageVedom == Vedomost_VB_Static.TypePageVedom.TitList || this.typePageVedom == Vedomost_VB_Static.TypePageVedom.LRI || this.ReadOnly || this._document_readOnly || this.typePageVedom == Vedomost_VB_Static.TypePageVedom.Undefined || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Main)
      menuButtonItem6.Enabled = false;
    menuButtonItem1.Items.Add("Строку короткое примечание");
    MenuButtonItem menuButtonItem7 = menuButtonItem1.Items[menuButtonItem1.Items.Count - 1];
    menuButtonItem7.Tag = (object) "AVS.VB.AddVedRow_RemarkShort";
    menuButtonItem7.Click += new EventHandler(this.mi_CreateRecord_RemarkShort);
    if (this.typePageVedom == Vedomost_VB_Static.TypePageVedom.TitList || this.typePageVedom == Vedomost_VB_Static.TypePageVedom.REMARK || this.typePageVedom == Vedomost_VB_Static.TypePageVedom.LRI || this.ReadOnly || this._document_readOnly || this.typePageVedom == Vedomost_VB_Static.TypePageVedom.Undefined || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Main)
      menuButtonItem7.Enabled = false;
    if (this._vedomost_VB_new != null && algorithmToPrint != null && algorithmToPrint._additional1 == 1)
    {
      menuButtonItem1.Items.Add("Строку Дополнительная 1");
      MenuButtonItem menuButtonItem8 = menuButtonItem1.Items[menuButtonItem1.Items.Count - 1];
      menuButtonItem8.Tag = (object) "AVS.VB.AddVedRow_Additional1";
      menuButtonItem8.Click += new EventHandler(this.mi_CreateRecord_Additional);
      if (this.typePageVedom == Vedomost_VB_Static.TypePageVedom.TitList || this.typePageVedom == Vedomost_VB_Static.TypePageVedom.REMARK || this.typePageVedom == Vedomost_VB_Static.TypePageVedom.LRI || this.ReadOnly || this._document_readOnly || this.typePageVedom == Vedomost_VB_Static.TypePageVedom.Undefined || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Main)
        menuButtonItem8.Enabled = false;
    }
    if (this._vedomost_VB_new != null && algorithmToPrint != null && algorithmToPrint._additional2 == 1)
    {
      menuButtonItem1.Items.Add("Строку Дополнительная 2");
      MenuButtonItem menuButtonItem9 = menuButtonItem1.Items[menuButtonItem1.Items.Count - 1];
      menuButtonItem9.Tag = (object) "AVS.VB.AddVedRow_Additional2";
      menuButtonItem9.Click += new EventHandler(this.mi_CreateRecord_Additional);
      if (this.typePageVedom == Vedomost_VB_Static.TypePageVedom.TitList || this.typePageVedom == Vedomost_VB_Static.TypePageVedom.REMARK || this.typePageVedom == Vedomost_VB_Static.TypePageVedom.LRI || this.ReadOnly || this._document_readOnly || this.typePageVedom == Vedomost_VB_Static.TypePageVedom.Undefined || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Main)
        menuButtonItem9.Enabled = false;
    }
    if (this._vedomost_VB_new != null && algorithmToPrint != null && algorithmToPrint._additional3 == 1)
    {
      menuButtonItem1.Items.Add("Строку Дополнительная 3");
      MenuButtonItem menuButtonItem10 = menuButtonItem1.Items[menuButtonItem1.Items.Count - 1];
      menuButtonItem10.Tag = (object) "AVS.VB.AddVedRow_Additional3";
      menuButtonItem10.Click += new EventHandler(this.mi_CreateRecord_Additional);
      if (this.typePageVedom == Vedomost_VB_Static.TypePageVedom.TitList || this.typePageVedom == Vedomost_VB_Static.TypePageVedom.REMARK || this.typePageVedom == Vedomost_VB_Static.TypePageVedom.LRI || this.ReadOnly || this._document_readOnly || this.typePageVedom == Vedomost_VB_Static.TypePageVedom.Undefined || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Main)
        menuButtonItem10.Enabled = false;
    }
    if (this._vedomost_VB_new != null && algorithmToPrint != null && algorithmToPrint._additional4 == 1)
    {
      menuButtonItem1.Items.Add("Строку Дополнительная 4");
      MenuButtonItem menuButtonItem11 = menuButtonItem1.Items[menuButtonItem1.Items.Count - 1];
      menuButtonItem11.Tag = (object) "AVS.VB.AddVedRow_Additional4";
      menuButtonItem11.Click += new EventHandler(this.mi_CreateRecord_Additional);
      if (this.typePageVedom == Vedomost_VB_Static.TypePageVedom.TitList || this.typePageVedom == Vedomost_VB_Static.TypePageVedom.REMARK || this.typePageVedom == Vedomost_VB_Static.TypePageVedom.LRI || this.ReadOnly || this._document_readOnly || this.typePageVedom == Vedomost_VB_Static.TypePageVedom.Undefined || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Main)
        menuButtonItem11.Enabled = false;
    }
    menuButtonItem1.Items.Add("Пустую строку");
    MenuButtonItem menuButtonItem12 = menuButtonItem1.Items[menuButtonItem1.Items.Count - 1];
    menuButtonItem12.Tag = (object) "AVS.VB.c";
    menuButtonItem12.Click += new EventHandler(this.mi_CreateRecord_Empty);
    if (this.typePageVedom == Vedomost_VB_Static.TypePageVedom.TitList || this.typePageVedom == Vedomost_VB_Static.TypePageVedom.LRI || this.ReadOnly || this._document_readOnly || this.typePageVedom == Vedomost_VB_Static.TypePageVedom.Undefined || this._typeRowCurr == Vedomost_VB_Static.TypeRow.Main)
      menuButtonItem12.Enabled = false;
    if (menuButtonItem1.Items.Count != 0)
      return;
    menuButtonItem1.Items.Add("[Пусто]");
    menuButtonItem1.Items[0].Enabled = false;
  }

  /// <summary> Выполнение Конкретной ПОДкоманды "(Добавить) Основную строку" </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void mi_CreateRecord_Info(object sender, EventArgs e)
  {
    if (sender == null || !(sender is MenuButtonItem menuButtonItem) || !(menuButtonItem.Tag is string _) || !this._isVveliOsnovnyeDannye)
      return;
    string attributeValue = this.Razdel_Ved_Curr();
    if (string.IsNullOrEmpty(attributeValue) || attributeValue == "0" || attributeValue == "-1")
      attributeValue = "1";
    VedomostEditorWindow documentEditorForm = (VedomostEditorWindow) AVSPlugin.Instance.ActiveImDocumentEditorForm;
    One_Ved_Nastr oneVedNastrCurr = documentEditorForm._one_Ved_Nastr_Curr;
    Vedomost_VB vedomostVbNew = documentEditorForm._vedomost_VB_new;
    TableData docRowInfo = vedomostVbNew.Create_DocRow_Info((Vedomost_VB.RecordForVed_New) null, this._docTemplate, this.Document, "UserNewRowInfo", true, this.name_Page_Current);
    if (docRowInfo == null)
      return;
    if (!string.IsNullOrEmpty(this._variableRowCurrent))
      docRowInfo.SetAttributeValue("Variable", this._variableRowCurrent);
    docRowInfo.SetAttributeValue("TypeRow", "Info");
    docRowInfo.SetAttributeValue("Razdel_Ved", attributeValue);
    this.AddRowToDoc(docRowInfo);
    vedomostVbNew.Add_AttributeTypeRec_To_DocRow(docRowInfo, "Info", "UserNewRowInfo");
  }

  /// <summary> Выполнение Конкретной ПОДкоманды "(Добавить) Строку заголовка" </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void mi_CreateRecord_Zagolovok(object sender, EventArgs e)
  {
    if (sender == null || !(sender is MenuButtonItem menuButtonItem) || !(menuButtonItem.Tag is string _) || !this._isVveliOsnovnyeDannye)
      return;
    string str = "";
    string attributeValue = "1";
    Vedomost_VB.One_Zagolovok one_Zagolovok_Selected = (Vedomost_VB.One_Zagolovok) null;
    VedomostEditorWindow documentEditorForm = (VedomostEditorWindow) AVSPlugin.Instance.ActiveImDocumentEditorForm;
    One_Ved_Nastr oneVedNastrCurr = documentEditorForm._one_Ved_Nastr_Curr;
    Vedomost_VB vedomostVbNew = documentEditorForm._vedomost_VB_new;
    this._vedomost_VB_new = vedomostVbNew;
    this._one_Ved_Nastr_Curr = oneVedNastrCurr;
    Vedomost_VB.RecordForVed_New recordForVed;
    if (oneVedNastrCurr._zagolovki_Ved == null || oneVedNastrCurr._zagolovki_Ved._list_One_Zagolovok == null || oneVedNastrCurr._zagolovki_Ved._list_One_Zagolovok.Count == 0)
    {
      recordForVed = new Vedomost_VB.RecordForVed_New();
      recordForVed.TypeRec = Vedomost_VB.TypeRec.Title;
    }
    else
    {
      VyborZagolovka vyborZagolovka = new VyborZagolovka();
      if (this._guidTemplateDoc == Guid.Empty)
      {
        this._guidTemplateDoc = this.DocumentGuid;
        vyborZagolovka._guidTypeVed = Guid.Empty;
      }
      else
        vyborZagolovka._guidTypeVed = this.DocumentGuid;
      vyborZagolovka._guidTemplateVed = this._guidTemplateDoc;
      vyborZagolovka._one_Ved_Nastr = oneVedNastrCurr;
      if (vyborZagolovka.ShowDialog() != DialogResult.OK)
        return;
      recordForVed = new Vedomost_VB.RecordForVed_New();
      recordForVed.TypeRec = Vedomost_VB.TypeRec.Title;
      int selectedIndex = vyborZagolovka.ListZagolovkov.SelectedIndex;
      if (oneVedNastrCurr._zagolovki_Ved._userZagolovki)
      {
        if (selectedIndex > 0)
        {
          one_Zagolovok_Selected = oneVedNastrCurr._zagolovki_Ved._list_One_Zagolovok[selectedIndex - 1];
          recordForVed.Set_Name(one_Zagolovok_Selected._name);
          attributeValue = one_Zagolovok_Selected._granicaPriznaka;
          str = one_Zagolovok_Selected._name;
        }
      }
      else
      {
        one_Zagolovok_Selected = oneVedNastrCurr._zagolovki_Ved._list_One_Zagolovok[selectedIndex];
        recordForVed.Set_Name(one_Zagolovok_Selected._name);
        attributeValue = one_Zagolovok_Selected._granicaPriznaka;
        str = one_Zagolovok_Selected._name;
      }
    }
    if (recordForVed == null)
      return;
    TableData docRowTarget = (TableData) null;
    TableData mainTableTarget = (TableData) null;
    if (oneVedNastrCurr._zagolovki_Ved._locationZagolovki && str != "" && one_Zagolovok_Selected != null && (this.FindDocRowZagolovok(one_Zagolovok_Selected, this._variableRowCurrent) || !this.SearchRowForZagolovok2(one_Zagolovok_Selected, this._variableRowCurrent, out mainTableTarget, out docRowTarget)))
      return;
    if (!oneVedNastrCurr._zagolovki_Ved._locationZagolovki || one_Zagolovok_Selected == null)
      docRowTarget = this._docRowCurrent;
    TableData docRowZagolovok = this._vedomost_VB_new.Create_DocRow_Zagolovok(this._docTemplate, recordForVed, this._variableRowCurrent, this.name_Page_Current);
    if (docRowZagolovok == null)
      return;
    int index = docRowZagolovok.NodesCount <= 2 ? (docRowZagolovok.NodesCount <= 1 ? 0 : 1) : 2;
    docRowZagolovok.SetAttributeValue("Razdel_Ved", attributeValue);
    this.Razdel_Ved_Curr();
    if (docRowTarget != null)
    {
      this.DocumentControl.SetSelection(docRowTarget.Nodes[index], true, Point.Empty, true, false);
      if (docRowTarget.Name != "Пустая строка")
      {
        TableData docRowEmpty = vedomostVbNew.Create_DocRow_Empty(this._docTemplate, this._variableRowCurrent, this.name_Page_Current);
        vedomostVbNew.Add_AttributeTypeRec_To_DocRow(docRowEmpty, "Empty", "UserNewZagolovok");
        this.AddRowToDoc(docRowEmpty);
      }
      this.AddRowToDoc(docRowZagolovok);
      this.DocumentControl.SetSelection(docRowZagolovok.Nodes[index], true, Point.Empty, true, false);
    }
    else if (mainTableTarget != null)
    {
      this.DocumentControl.SetSelection((DocumentTreeNode) mainTableTarget, true, Point.Empty, true, false);
      mainTableTarget.Nodes.Insert(0, (DocumentTreeNode) docRowZagolovok);
      this.DocumentControl.SetSelection(docRowZagolovok.Nodes[index], true, Point.Empty, true, false);
    }
    else
    {
      this.Get_PageCurrent().FindFirstChildNodeByName("Главная таблица").Nodes.Insert(0, (DocumentTreeNode) docRowZagolovok);
      this.DocumentControl.SetSelection(docRowZagolovok.Nodes[index], true, Point.Empty, true, false);
    }
    vedomostVbNew.Add_AttributeTypeRec_To_DocRow(docRowZagolovok, "Title", "UserNewZagolovok");
  }

  /// <summary> Выполнение Конкретной ПОДкоманды "(Добавить) Строку заголовка" </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void mi_CreateRecord_PodZagolovok(object sender, EventArgs e)
  {
    if (sender == null || !(sender is MenuButtonItem menuButtonItem) || !(menuButtonItem.Tag is string _) || !this._isVveliOsnovnyeDannye)
      return;
    VedomostEditorWindow documentEditorForm = (VedomostEditorWindow) AVSPlugin.Instance.ActiveImDocumentEditorForm;
    One_Ved_Nastr oneVedNastrCurr = documentEditorForm._one_Ved_Nastr_Curr;
    Vedomost_VB vedomostVbNew = documentEditorForm._vedomost_VB_new;
    Vedomost_VB.RecordForVed_New recordForVed = new Vedomost_VB.RecordForVed_New();
    recordForVed.TypeRec = Vedomost_VB.TypeRec.Title2;
    recordForVed.Set_Name("");
    TableData docRowPodZagolovok = vedomostVbNew.Create_DocRow_PodZagolovok(this._docTemplate, recordForVed, this._variableRowCurrent, this.name_Page_Current);
    TableData docRowEmpty = vedomostVbNew.Create_DocRow_Empty(this._docTemplate, this._variableRowCurrent, this.name_Page_Current);
    if (docRowPodZagolovok == null)
      return;
    vedomostVbNew.Add_AttributeTypeRec_To_DocRow(docRowEmpty, "Empty", "UserNewPodZagolovok");
    this.AddRowToDoc(docRowEmpty);
    this.AddRowToDoc(docRowPodZagolovok);
    vedomostVbNew.Add_AttributeTypeRec_To_DocRow(docRowPodZagolovok, "Title2", "UserNewPodZagolovok");
  }

  /// <summary> Выполнение Конкретной ПОДкоманды "(Добавить) Строку заголовка" </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void mi_CreateRecord_TitlePart(object sender, EventArgs e)
  {
    if (sender == null || !(sender is MenuButtonItem menuButtonItem) || !(menuButtonItem.Tag is string _) || !this._isVveliOsnovnyeDannye)
      return;
    VedomostEditorWindow documentEditorForm = (VedomostEditorWindow) AVSPlugin.Instance.ActiveImDocumentEditorForm;
    One_Ved_Nastr oneVedNastrCurr = documentEditorForm._one_Ved_Nastr_Curr;
    Vedomost_VB vedomostVbNew = documentEditorForm._vedomost_VB_new;
    TableData docRowTitlePart = vedomostVbNew.Create_DocRow_TitlePart(this._docTemplate, this._variableRowCurrent, this.name_Page_Current);
    TableData docRowEmpty = vedomostVbNew.Create_DocRow_Empty(this._docTemplate, this._variableRowCurrent, this.name_Page_Current);
    if (docRowTitlePart == null)
      return;
    vedomostVbNew.Add_AttributeTypeRec_To_DocRow(docRowEmpty, "Empty", "UserNewTitlePart");
    this.AddRowToDoc(docRowEmpty);
    this.AddRowToDoc(docRowTitlePart);
    vedomostVbNew.Add_AttributeTypeRec_To_DocRow(docRowTitlePart, "TitlePart", "UserNewTitlePart");
  }

  /// <summary> Выполнение Конкретной ПОДкоманды "(Добавить) Строку примечания" </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void mi_CreateRecord_Remark(object sender, EventArgs e)
  {
    if (sender == null || !(sender is MenuButtonItem menuButtonItem) || !(menuButtonItem.Tag is string _) || !this._isVveliOsnovnyeDannye)
      return;
    VedomostEditorWindow documentEditorForm = (VedomostEditorWindow) AVSPlugin.Instance.ActiveImDocumentEditorForm;
    One_Ved_Nastr oneVedNastrCurr = documentEditorForm._one_Ved_Nastr_Curr;
    Vedomost_VB vedomostVbNew = documentEditorForm._vedomost_VB_new;
    TableData docRowRemark = vedomostVbNew.Create_DocRow_Remark(this._docTemplate, this._variableRowCurrent, this.name_Page_Current);
    if (docRowRemark == null)
      return;
    string attributeValue = this.Razdel_Ved_Curr();
    if (string.IsNullOrEmpty(attributeValue) || attributeValue == "0" || attributeValue == "-1")
      attributeValue = !(this.name_Page_Current == "Примечания") ? "1" : "9999";
    docRowRemark.SetAttributeValue("Razdel_Ved", attributeValue);
    this.AddRowToDoc(docRowRemark);
    vedomostVbNew.Add_AttributeTypeRec_To_DocRow(docRowRemark, "Remark", "UserNewRemark");
  }

  /// <summary> Выполнение Конкретной ПОДкоманды "(Добавить) Строку короткого примечания" </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void mi_CreateRecord_RemarkShort(object sender, EventArgs e)
  {
    if (sender == null || !(sender is MenuButtonItem menuButtonItem) || !(menuButtonItem.Tag is string _) || !this._isVveliOsnovnyeDannye)
      return;
    VedomostEditorWindow documentEditorForm = (VedomostEditorWindow) AVSPlugin.Instance.ActiveImDocumentEditorForm;
    One_Ved_Nastr oneVedNastrCurr = documentEditorForm._one_Ved_Nastr_Curr;
    Vedomost_VB vedomostVbNew = documentEditorForm._vedomost_VB_new;
    TableData docRowRemarkShort = vedomostVbNew.Create_DocRow_RemarkShort(this._docTemplate, this._variableRowCurrent, this.name_Page_Current);
    if (docRowRemarkShort == null)
      return;
    string attributeValue = this.Razdel_Ved_Curr();
    if (string.IsNullOrEmpty(attributeValue) || attributeValue == "0" || attributeValue == "-1")
      attributeValue = "1";
    docRowRemarkShort.SetAttributeValue("Razdel_Ved", attributeValue);
    this.AddRowToDoc(docRowRemarkShort);
    vedomostVbNew.Add_AttributeTypeRec_To_DocRow(docRowRemarkShort, "RemarkShort", "UserNewRemarkShort");
  }

  /// <summary> Выполнение Конкретной ПОДкоманды "(Добавить) Строку Дополнительная ... </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void mi_CreateRecord_Additional(object sender, EventArgs e)
  {
    if (sender == null || !(sender is MenuButtonItem menuButtonItem) || !(menuButtonItem.Tag is string tag) || !this._isVveliOsnovnyeDannye)
      return;
    VedomostEditorWindow documentEditorForm = (VedomostEditorWindow) AVSPlugin.Instance.ActiveImDocumentEditorForm;
    One_Ved_Nastr oneVedNastrCurr = documentEditorForm._one_Ved_Nastr_Curr;
    Vedomost_VB vedomostVbNew = documentEditorForm._vedomost_VB_new;
    string str = "Additional1";
    switch (tag)
    {
      case "AVS.VB.AddVedRow_Additional1":
        str = "Additional1";
        break;
      case "AVS.VB.AddVedRow_Additional2":
        str = "Additional2";
        break;
      case "AVS.VB.AddVedRow_Additional3":
        str = "Additional3";
        break;
      case "AVS.VB.AddVedRow_Additional4":
        str = "Additional4";
        break;
    }
    TableData docRowAdditional = vedomostVbNew.Create_DocRow_Additional(this._docTemplate, this.Document, "UserNewRowAdditional", 1, this.name_Page_Current);
    if (docRowAdditional == null)
      return;
    if (!string.IsNullOrEmpty(this._variableRowCurrent))
      docRowAdditional.SetAttributeValue("Variable", this._variableRowCurrent);
    docRowAdditional.SetAttributeValue("TypeRow", str);
    string attributeValue = this.Razdel_Ved_Curr();
    if (string.IsNullOrEmpty(attributeValue) || attributeValue == "0" || attributeValue == "-1")
      attributeValue = "1";
    docRowAdditional.SetAttributeValue("Razdel_Ved", attributeValue);
    this.AddRowToDoc(docRowAdditional);
    vedomostVbNew.Add_AttributeTypeRec_To_DocRow(docRowAdditional, str, "UserNewRowAdditional");
  }

  /// <summary> Выполнение Конкретной ПОДкоманды "(Добавить) Пустую строку" </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void mi_CreateRecord_Empty(object sender, EventArgs e)
  {
    if (sender == null || !(sender is MenuButtonItem menuButtonItem) || !(menuButtonItem.Tag is string _) || !this._isVveliOsnovnyeDannye)
      return;
    VedomostEditorWindow documentEditorForm = (VedomostEditorWindow) AVSPlugin.Instance.ActiveImDocumentEditorForm;
    One_Ved_Nastr oneVedNastrCurr = documentEditorForm._one_Ved_Nastr_Curr;
    Vedomost_VB vedomostVbNew = documentEditorForm._vedomost_VB_new;
    TableData docRowEmpty = vedomostVbNew.Create_DocRow_Empty(this._docTemplate, this._variableRowCurrent, this.name_Page_Current);
    if (docRowEmpty == null)
      return;
    string attributeValue = this.Razdel_Ved_Curr();
    if (string.IsNullOrEmpty(attributeValue) || attributeValue == "0" || attributeValue == "-1")
      attributeValue = "1";
    docRowEmpty.SetAttributeValue("Razdel_Ved", attributeValue);
    this.AddRowToDoc(docRowEmpty);
    vedomostVbNew.Add_AttributeTypeRec_To_DocRow(docRowEmpty, "Empty", "UserNewEmpty");
  }

  /// <summary> Для формы Б заполнение Заголовка ОБОЗНАЧЕНИЯ и номеров исполнений для СТРАНИЦЫ </summary>
  /// <param name="page"></param>
  /// <param name="number_Cikla"></param>
  public void FillProductHeaders_OnePage(PageData page, int number_Cikla)
  {
    string nodeName1 = "Заголовок таблицы исполнений";
    if (page.FindFirstNodeByName(nodeName1) is TextData firstNodeByName1)
    {
      string str = firstNodeByName1.Text;
      if (string.IsNullOrEmpty(str))
        str = Vedomost_VB_Static.Kol_na_ispoln_Template;
      firstNodeByName1.AssignText(string.Format(str + " {0} -", (object) this._designationArticle), false, true, false, true, false);
    }
    string nodeName2 = "Номера исполнений";
    TableData firstNodeByName2 = page.FindFirstNodeByName(nodeName2) as TableData;
    int count = this._variables_Coordination.list_Variables.Count;
    int num = number_Cikla + 10;
    if (this._variables_Coordination != null && this._variables_Coordination.list_Variables.Count <= count)
    {
      int index1 = -1;
      for (int index2 = number_Cikla; index2 < num && index2 < this._variables_Coordination.list_Variables.Count; ++index2)
      {
        ++index1;
        if (firstNodeByName2.Nodes[index1] is TextData node)
        {
          string listVariable = this._variables_Coordination.list_Variables[index2];
          string listCaption = this._variables_Coordination.list_Captions[index2];
          node.AssignText(listCaption, false, true, false, true, false);
        }
      }
    }
    else
    {
      for (int index = 0; index < this._variables_Coordination.list_Captions.Count && index < firstNodeByName2.Nodes.Count; ++index)
      {
        if (firstNodeByName2.Nodes[index] is TextData node)
        {
          string listCaption = this._variables_Coordination.list_Captions[index];
          if (string.IsNullOrEmpty(listCaption))
            break;
          node.AssignText(listCaption, false, true, false, false, false);
        }
      }
    }
  }

  /// <summary> Строке присвоить \"Только для чтения\" </summary>
  /// <param name="commandState"></param>
  /// <returns></returns>
  private bool Protection_docRow(ICommandState commandState)
  {
    TableData docRow = this.DocRowCurrent() ?? this.DocPodRowCurrent();
    if (docRow != null)
      Vedomost_VB_Static.DocRow_ReadOnly((DocumentTreeNode) docRow, true, true);
    return true;
  }

  /// <summary> Разрешить редактирование строки </summary>
  /// <param name="commandState"></param>
  /// <returns></returns>
  private bool DeProtection_docRow(ICommandState commandState)
  {
    TableData docRow = this.DocRowCurrent() ?? this.DocPodRowCurrent();
    if (docRow != null)
      Vedomost_VB_Static.DocRow_ReadOnly((DocumentTreeNode) docRow, false, false);
    return true;
  }

  /// <summary> Содержимому текущего документа присвоить \"Только для чтения\" </summary>
  /// <param name="commandState"></param>
  /// <returns></returns>
  private bool Protection_mainTabls(ICommandState commandState)
  {
    Vedomost_VB_Static.MainTables_ReadOnly((DocumentTreeNode) this.Document, true, false);
    return true;
  }

  /// <summary> Разрешить редактирование текущего документа </summary>
  /// <param name="commandState"></param>
  /// <returns></returns>
  private bool DeProtection_mainTabls(ICommandState commandState)
  {
    Vedomost_VB_Static.MainTables_ReadOnly((DocumentTreeNode) this.Document, false, false);
    return true;
  }

  /// <summary> Проверка ЭТО ЕСПД?, если ДА, то создать раздел, сам ЛУ и запись о ЛУ </summary>
  /// <param name="quet">Если true? то создавать ЛУ не спрашивая</param>
  private void Begin_Espd(bool quet)
  {
    if (this._typeDoc != Vedomost_VB.TypeDoc.Espd || this._nameTypeDoc != "Cпецификация")
      return;
    bool flag = false;
    long num1 = Vedomost_VB_Static.Check_LU_By_Document(this.Document);
    this.isLU = !num1.IsUndefinedId();
    if (!this.isLU)
    {
      if (quet)
        flag = true;
      else if (MessageBox.Show("Создать \"Лист утверждения\" на текущий документ?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
        flag = true;
      if (flag)
      {
        num1 = new Espd_VB()
        {
          _nameProg = this._nameArticle,
          _nameDoc = this._nameTypeDoc,
          _designationDocLU = (this._designationDoc + "-ЛУ"),
          _iDSP = 0L
        }.CreateAndOpenLU(false);
        if (!num1.IsUndefinedId())
        {
          if (!quet)
          {
            int num2 = (int) MessageBox.Show($"Лист утверждения\r\n\r\n{this._designationDoc}-ЛУ\r\n\r\nсоздан", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
          }
          this.isLU = true;
        }
      }
      flag = false;
    }
    else
      num1 = Vedomost_VB_Static.Get_ObjectID_By_Designation(this._designationDoc + "-ЛУ");
    if (!this.isLU)
      return;
    if (this.is_TitList_In_Document && !Vedomost_VB_Static.Is_LU_InTitList(this.Document, "Обозначение ЛУ"))
    {
      if (quet)
        flag = true;
      else if (MessageBox.Show("Занести ссылку о \"Листе утверждения\" в \"Титульный лист\"?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
        flag = true;
      if (flag)
        Vedomost_VB_Static.Add_LU_ToTitList(this.Document, this._designationDoc + "-ЛУ", "Обозначение ЛУ");
      flag = false;
    }
    if (this.Check_DocRowLU_ESPD())
      return;
    if (quet)
      flag = true;
    else if (MessageBox.Show("Занести в спецификацию запись о \"Листе утверждения\"?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
      flag = true;
    if (flag)
    {
      if (!this.Check_FirstZagol_ESPD())
        this.Add_FirstZagol_ESPD();
      this.DocumentControl.SetSelection((DocumentTreeNode) Vedomost_VB_Static.FindFirstMainTable(this.Document), true, Point.Empty, true, false);
      this.AddVedRow_ByObjectID(num1, "Auto", "1", false, "Размножать по указанию");
    }
  }

  /// <summary> Поиск существующего заголовка </summary>
  /// <param name="one_Zagolovok_Selected"></param>
  /// <param name="_variableRowCurrent"></param>
  /// <returns></returns>
  private bool FindDocRowZagolovok(
    Vedomost_VB.One_Zagolovok one_Zagolovok_Selected,
    string variableRowCurrent)
  {
    if (one_Zagolovok_Selected == null || string.IsNullOrEmpty(one_Zagolovok_Selected._name))
      return false;
    return this.FindDocRowZagolovok_UpDown(one_Zagolovok_Selected, variableRowCurrent, false) || this.FindDocRowZagolovok_UpDown(one_Zagolovok_Selected, variableRowCurrent, true);
  }

  private bool FindDocRowZagolovok_UpDown(
    Vedomost_VB.One_Zagolovok one_Zagolovok_Selected,
    string variableRowCurrent,
    bool UpDown)
  {
    int numberPageCurrent = this.number_Page_Current;
    string name = one_Zagolovok_Selected._name;
    string granicaPriznaka = one_Zagolovok_Selected._granicaPriznaka;
    while (numberPageCurrent >= 0 && numberPageCurrent < this.Document.NodesCount)
    {
      DocumentTreeNode node1 = this.Document.Nodes[numberPageCurrent];
      string nodeClass = node1.NodeClass;
      TableData docRowCurrent = this._docRowCurrent;
      if (nodeClass != "Page")
        return false;
      string nodeName = "Главная таблица";
      TableData firstChildNodeByName = (TableData) node1.FindFirstChildNodeByName(nodeName);
      if (firstChildNodeByName != null && firstChildNodeByName.Nodes != null && firstChildNodeByName.NodesCount > 0)
      {
        int index1;
        if (numberPageCurrent == this.number_Page_Current)
        {
          int num = Vedomost_VB_Static.Get_Number_By_Node(firstChildNodeByName, docRowCurrent);
          if (num < 0)
          {
            if (docRowCurrent == null)
              num = 0;
            else
              break;
          }
          index1 = num;
        }
        else
          index1 = !UpDown ? firstChildNodeByName.NodesCount - 1 : 0;
        while (index1 >= 0 && index1 < firstChildNodeByName.NodesCount)
        {
          TableData node2 = firstChildNodeByName.Nodes[index1] as TableData;
          if (node2.GetAttributeValue("TypeRow", true) != "Title")
          {
            if (UpDown)
              ++index1;
            else
              --index1;
          }
          else
          {
            if (string.Compare(node2.GetAttributeValue("Variable", true), variableRowCurrent, StringComparison.Ordinal) != 0)
              return false;
            for (int index2 = 0; index2 < node2.NodesCount; ++index2)
            {
              DocumentTreeNode node3 = node2.Nodes[index2];
              if (node3 != null && node3.NodeClass == "TextBoxElement" && string.Compare((node3 as TextBoxElement).Text, name, StringComparison.Ordinal) == 0)
              {
                this.DocumentControl.SetSelection(node2.Nodes[index2], true, Point.Empty, true, false);
                return true;
              }
            }
            string razdel = Vedomost_VB_Static.GetRazdel(node2, this._one_Ved_Nastr_Curr._zagolovki_Ved._objectType);
            if (Vedomost_VB_Static.Compare_StrongOrInt(granicaPriznaka, razdel, this._one_Ved_Nastr_Curr._zagolovki_Ved._typeCompare) == 0)
            {
              this.DocumentControl.SetSelection(node2.Nodes[1], true, Point.Empty, true, false);
              return true;
            }
            if (UpDown)
              ++index1;
            else
              --index1;
          }
        }
      }
      if (UpDown)
        ++numberPageCurrent;
      else
        --numberPageCurrent;
    }
    return false;
  }

  /// <summary> Поиск. Куда вставить строку заголовка </summary>
  /// <param name="one_Zagolovok_Selected"></param>
  /// <param name="variableRowCurrent"></param>
  /// <param name="mainTableTarget"></param>
  /// <param name="docRowTarget"></param>
  /// <returns></returns>
  private bool SearchRowForZagolovok2(
    Vedomost_VB.One_Zagolovok one_Zagolovok_Selected,
    string variableRowCurrent,
    out TableData mainTableTarget,
    out TableData docRowTarget)
  {
    mainTableTarget = (TableData) null;
    docRowTarget = (TableData) null;
    if (one_Zagolovok_Selected == null)
      return false;
    DocumentTreeNode node = this.Document.Nodes[this.number_Page_Current];
    if (node.NodeClass != "Page")
      return false;
    TableData firstChildNodeByName = (TableData) node.FindFirstChildNodeByName("Главная таблица");
    if (firstChildNodeByName == null || firstChildNodeByName.Nodes == null)
      return false;
    int num = 0;
    if (this._docRowCurrent != null)
    {
      if (this._docRowCurrent.GetAttributeValue("TypeRow", true) == "Main" && one_Zagolovok_Selected._granicaPriznaka == "1")
      {
        docRowTarget = this._docRowCurrent;
        return true;
      }
      string razdel = Vedomost_VB_Static.GetRazdel(this._docRowCurrent, this._one_Ved_Nastr_Curr._zagolovki_Ved._objectType);
      if (!string.IsNullOrEmpty(razdel))
        num = Vedomost_VB_Static.Compare_StrongOrInt(one_Zagolovok_Selected._granicaPriznaka, razdel, this._one_Ved_Nastr_Curr._zagolovki_Ved._typeCompare);
    }
    bool flag = false;
    if (num <= 0)
      flag = this.SearchRowForZagolovok_Prevision(one_Zagolovok_Selected, variableRowCurrent, out mainTableTarget, out docRowTarget);
    return flag || this.SearchRowForZagolovok_Next(one_Zagolovok_Selected, variableRowCurrent, out mainTableTarget, out docRowTarget);
  }

  /// <summary> Поиск вверх. Куда вставить строку заголовка </summary>
  /// <param name="one_Zagolovok_Selected"></param>
  /// <param name="variableRowCurrent"></param>
  /// <param name="mainTableTarget"></param>
  /// <param name="docRowTarget"></param>
  /// <returns></returns>
  private bool SearchRowForZagolovok_Prevision(
    Vedomost_VB.One_Zagolovok one_Zagolovok_Selected,
    string variableRowCurrent,
    out TableData mainTableTarget,
    out TableData docRowTarget)
  {
    mainTableTarget = this._mainTableCurrent;
    docRowTarget = (TableData) null;
    TableData docRow1 = this._docRowCurrent;
    TableData mainTable_In = this._mainTableCurrent;
    TableData mainTable_out = mainTable_In;
    int iPage_in = this.number_Page_Current;
    int iPage_out = iPage_in;
    bool flag = false;
    TableData tableData = (TableData) null;
    TableData docRow2;
    while (true)
    {
      docRow2 = this.SearchDocRow_Prevision_Easy(iPage_in, docRow1, mainTable_In, out iPage_out, out mainTable_out);
      if (!string.IsNullOrEmpty(Vedomost_VB_Static.GetRazdel(docRow1, this._one_Ved_Nastr_Curr._zagolovki_Ved._objectType)))
        tableData = docRow1;
      if (docRow2 != null)
      {
        if (docRow2.GetAttributeValue("TypeRow", true) == "Main")
          flag = true;
        string razdel = Vedomost_VB_Static.GetRazdel(docRow2, this._one_Ved_Nastr_Curr._zagolovki_Ved._objectType);
        if (string.IsNullOrEmpty(razdel) || Vedomost_VB_Static.Compare_StrongOrInt(one_Zagolovok_Selected._granicaPriznaka, razdel, this._one_Ved_Nastr_Curr._zagolovki_Ved._typeCompare) <= 0)
        {
          if (!flag)
          {
            iPage_in = iPage_out;
            mainTable_In = mainTable_out;
            docRow1 = docRow2;
          }
          else
            goto label_14;
        }
        else
          goto label_10;
      }
      else
        break;
    }
    return mainTable_out != null;
label_10:
    if (tableData == null)
      return false;
    docRowTarget = docRow2;
    return true;
label_14:
    mainTableTarget = mainTable_out;
    docRowTarget = docRow2;
    return true;
  }

  /// <summary> Поиск вниз. Куда вставить строку заголовка </summary>
  /// <param name="one_Zagolovok_Selected"></param>
  /// <param name="variableRowCurrent"></param>
  /// <param name="mainTableTarget"></param>
  /// <param name="docRowTarget"></param>
  /// <returns></returns>
  private bool SearchRowForZagolovok_Next(
    Vedomost_VB.One_Zagolovok one_Zagolovok_Selected,
    string variableRowCurrent,
    out TableData mainTableTarget,
    out TableData docRowTarget)
  {
    mainTableTarget = this._mainTableCurrent;
    docRowTarget = (TableData) null;
    TableData docRow1 = this._docRowCurrent;
    TableData mainTable_In = this._mainTableCurrent;
    TableData mainTable_out = mainTable_In;
    int iPage_in = this.number_Page_Current;
    int iPage_out = iPage_in;
    bool flag = false;
    TableData tableData1 = (TableData) null;
    TableData tableData2;
    while (true)
    {
      TableData docRow2 = docRow1 != null || mainTable_In == null || mainTable_In.NodesCount <= 0 ? this.SearchDocRow_Next_Easy(iPage_in, docRow1, mainTable_In, out iPage_out, out mainTable_out) : (TableData) mainTable_In.Nodes[0];
      tableData2 = docRow1;
      if (!string.IsNullOrEmpty(Vedomost_VB_Static.GetRazdel(docRow1, this._one_Ved_Nastr_Curr._zagolovki_Ved._objectType)))
        tableData1 = docRow1;
      if (docRow2 != null)
      {
        docRow2.GetAttributeValue("TypeRow", true);
        string razdel = Vedomost_VB_Static.GetRazdel(docRow2, this._one_Ved_Nastr_Curr._zagolovki_Ved._objectType);
        if (!string.IsNullOrEmpty(razdel) && Vedomost_VB_Static.Compare_StrongOrInt(one_Zagolovok_Selected._granicaPriznaka, razdel, this._one_Ved_Nastr_Curr._zagolovki_Ved._typeCompare) <= 0)
          flag = true;
        if (!flag)
        {
          iPage_in = iPage_out;
          mainTable_In = mainTable_out;
          docRow1 = docRow2;
        }
        else
          goto label_8;
      }
      else
        break;
    }
    docRowTarget = tableData2;
    return true;
label_8:
    mainTableTarget = mainTable_out;
    docRowTarget = tableData1 == null ? tableData2 : tableData1;
    return true;
  }

  /// <summary> Предыдущая строка даже если она на предыдущей странице </summary>
  /// <param name="iPage_in"></param>
  /// <param name="docRow"></param>
  /// <returns></returns>
  private TableData SearchDocRow_Prevision_Easy(
    int iPage_in,
    TableData docRow,
    TableData mainTable_In,
    out int iPage_out,
    out TableData mainTable_out)
  {
    mainTable_out = (TableData) null;
    iPage_out = iPage_in;
    if (iPage_in < 0 || docRow == null)
      return (TableData) null;
    if (iPage_in >= this.Document.NodesCount)
      return (TableData) null;
    DocumentTreeNode node1 = this.Document.Nodes[iPage_in];
    TableData mainTable = mainTable_In;
    mainTable_out = mainTable;
    int numberByNode = Vedomost_VB_Static.Get_Number_By_Node(mainTable, docRow);
    string attributeValue1 = docRow.GetAttributeValue("Variable", true);
    if (numberByNode > 0)
    {
      int index = numberByNode - 1;
      TableData node2 = (TableData) mainTable.Nodes[index];
      string attributeValue2 = node2.GetAttributeValue("Variable", true);
      return attributeValue1 != attributeValue2 ? (TableData) null : node2;
    }
    if (iPage_in == 0)
      return (TableData) null;
    TableData firstChildNodeByName;
    do
    {
      --iPage_in;
      DocumentTreeNode node3 = this.Document.Nodes[iPage_in];
      string name = node3.Name;
      firstChildNodeByName = (TableData) node3.FindFirstChildNodeByName("Главная таблица");
      if (firstChildNodeByName == null || firstChildNodeByName.Nodes == null)
        return (TableData) null;
      mainTable_out = firstChildNodeByName;
      iPage_out = iPage_in;
      if (firstChildNodeByName.NodesCount != 0)
        goto label_15;
    }
    while (iPage_in != 0);
    return (TableData) null;
label_15:
    int index1 = firstChildNodeByName.NodesCount - 1;
    return (TableData) firstChildNodeByName.Nodes[index1];
  }

  /// <summary> Следующая строка даже если она на следующей странице </summary>
  /// <param name="iPage_in"></param>
  /// <param name="docRow"></param>
  /// <returns></returns>
  private TableData SearchDocRow_Next_Easy(
    int iPage_in,
    TableData docRow,
    TableData mainTable_In,
    out int iPage_out,
    out TableData mainTable_out)
  {
    mainTable_out = (TableData) null;
    iPage_out = iPage_in;
    if (iPage_in < 0 || docRow == null)
      return (TableData) null;
    if (iPage_in >= this.Document.NodesCount)
      return (TableData) null;
    DocumentTreeNode node1 = this.Document.Nodes[iPage_in];
    TableData mainTable = mainTable_In;
    mainTable_out = mainTable;
    int numberByNode = Vedomost_VB_Static.Get_Number_By_Node(mainTable, docRow);
    string attributeValue1 = docRow.GetAttributeValue("Variable", true);
    if (numberByNode < mainTable.NodesCount - 1)
    {
      int index = numberByNode + 1;
      TableData node2 = (TableData) mainTable.Nodes[index];
      string attributeValue2 = node2.GetAttributeValue("Variable", true);
      return attributeValue1 != attributeValue2 ? (TableData) null : node2;
    }
    TableData firstChildNodeByName;
    while (true)
    {
      ++iPage_in;
      if (iPage_in < this.Document.NodesCount)
      {
        DocumentTreeNode node3 = this.Document.Nodes[iPage_in];
        if (!(node3.Name != this.name_Page_Current))
        {
          firstChildNodeByName = (TableData) node3.FindFirstChildNodeByName("Главная таблица");
          if (firstChildNodeByName != null && firstChildNodeByName.Nodes != null)
          {
            mainTable_out = firstChildNodeByName;
            iPage_out = iPage_in;
            if (firstChildNodeByName.NodesCount == 0)
            {
              ++iPage_in;
              iPage_out = iPage_in;
            }
            else
              goto label_16;
          }
          else
            goto label_13;
        }
        else
          goto label_11;
      }
      else
        break;
    }
    return (TableData) null;
label_11:
    return (TableData) null;
label_13:
    return (TableData) null;
label_16:
    int index1 = 0;
    return (TableData) firstChildNodeByName.Nodes[index1];
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.SuspendLayout();
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Name = nameof (VedomostEditorWindow);
    this.ResumeLayout(false);
  }

  public enum TypeDocWindow
  {
    Undefined,
    Ved,
    Tabl,
  }
}
