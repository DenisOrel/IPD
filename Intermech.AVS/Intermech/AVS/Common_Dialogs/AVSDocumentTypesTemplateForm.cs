// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Common_Dialogs.AVSDocumentTypesTemplateForm
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Infralution.Controls.VirtualTree;
using Intermech.AVS.Properties;
using Intermech.AVS.Tool;
using Intermech.Bars;
using Intermech.ComponentModel;
using Intermech.DataFormats;
using Intermech.Document.DBCore;
using Intermech.Document.Model;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Client;
using Intermech.Navigator;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Tools;
using Intermech.Tools.LaunchActions;
using Intermech.VirtualTreeView;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS.Common_Dialogs;

/// <summary>Диалог выбора шаблона для типа документа</summary>
public class AVSDocumentTypesTemplateForm : Form
{
  private const string MSG_TEXT_READONLY = " (редактирование шаблонов разрешено только администраторам)";
  private const string FRM_CAPTION = "Настройка шаблонов";
  private bool isNewNode;
  /// <summary>Словарик хранения шаблонов</summary>
  private List<DocumentType> DocumentTemplates = new List<DocumentType>();
  private Image editImage;
  private bool isReadOnly;
  private List<DocumentType> removed = new List<DocumentType>();
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private SplitContainer splitContainer1;
  private Label _labelSelectProduct;
  public PageViewsManager PageViewsManager;
  private Panel panel1;
  private Button bCancel;
  private Button bOk;
  private Intermech.VirtualTreeView.VirtualTreeView treeList;
  private Column colName;
  private Panel panel2;
  private Button bSetTemplate;
  private Label labelName;
  private Label label1;
  private CellEditor cellEditor1;
  private Button bRemove;
  private Button bAdd;
  private TextBox textBox1;
  private TreeView treeView;
  private ImageList imageList1;
  private ContextMenuStrip menuDocument;
  private ToolStripMenuItem itemDocument;
  private ToolStripMenuItem itemSpecification;
  private Panel panelLeftBottom;
  private Panel panelRightTop;
  private ToolTip addRemoveTooltip;

  public AVSDocumentTypesTemplateForm()
  {
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 2884);
    this.PageViewsManager.ActiveViewPageChanged += new EventHandler(this.PageViewsManagerActiveViewPageChanged);
    this.treeView.NodeMouseClick += new TreeNodeMouseClickEventHandler(this.treeView_NodeMouseClick);
  }

  public long ContextID { get; set; } = -1;

  public AVSDocumentForm? ContextDocumentForm { get; set; }

  protected override void OnLoad(EventArgs e)
  {
    base.OnLoad(e);
    this.UpdateDocumentTemplates(false);
    this.editImage = DocumentMenuHelper.LoadImageFromResurces(this.GetType().Assembly, "Intermech.AVS.Resources.Edit.png");
    this.imageList1.Images.Add((Image) new Bitmap(16 /*0x10*/, 16 /*0x10*/));
    this.imageList1.Images.Add(this.editImage);
    this.UpdateTree();
    this.PointTo(this.ContextID, this.ContextDocumentForm);
    this.UpdateControls();
  }

  public void RestoreSize()
  {
    if (string.IsNullOrWhiteSpace(AvsConfig.General.AvsDocTypesTemplateFormSize))
      return;
    string[] strArray = AvsConfig.General.AvsDocTypesTemplateFormSize.Split(';');
    int result1;
    int result2;
    if (strArray.Length != 2 || !int.TryParse(strArray[0], out result1) || !int.TryParse(strArray[1], out result2))
      return;
    this.Size = new Size(result1, result2);
  }

  protected override void OnClosed(EventArgs e)
  {
    base.OnClosed(e);
    this.treeView.NodeMouseClick -= new TreeNodeMouseClickEventHandler(this.treeView_NodeMouseClick);
  }

  protected override void OnClosing(CancelEventArgs e)
  {
    AvsConfig.General.AvsDocTypesTemplateFormSize = $"{this.Width};{this.Height}";
    if (this.DialogResult == DialogResult.OK && !this.Save())
      e.Cancel = true;
    if (!e.Cancel)
      this.PageViewsManager.CloseViews();
    base.OnClosing(e);
  }

  /// <summary>Заполнение словарика</summary>
  private void UpdateDocumentTemplates(bool defaultMode)
  {
    this.DocumentTemplates.Clear();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.isReadOnly = !sessionKeeper.Session.IsAdmin;
      foreach (AVSDocumentTypeSettings documentTypeSettings in !defaultMode ? AVSDocumentsSettings.GetAvsDocumentTypes(sessionKeeper.Session) : AVSDocumentsSettings.GetDefault())
      {
        DocumentType documentType = new DocumentType(documentTypeSettings.TypeGuid, new AVSDocumentType?(documentTypeSettings.AVSDocType), new AVSDocumentForm?(), documentTypeSettings.TypeName, this);
        documentType.DBObjectTypeList = documentTypeSettings.DBObjectTypeList;
        if (defaultMode)
          documentType.Changed = true;
        AVSDocumentForm[] allowableDocumentForm = AVSDocumentsSettings.GetAllowableDocumentForm(documentTypeSettings.AVSDocType);
        if (allowableDocumentForm != null)
        {
          foreach (AVSDocumentForm avsDocumentForm in allowableDocumentForm)
            documentType.Childs.Add(new DocumentType(documentTypeSettings.TypeGuid, new AVSDocumentType?(documentTypeSettings.AVSDocType), new AVSDocumentForm?(avsDocumentForm), string.Empty, this)
            {
              Parent = documentType
            });
        }
        this.DocumentTemplates.Add(documentType);
      }
    }
  }

  private void UpdateControls()
  {
    this.Text = "Настройка шаблонов" + (this.isReadOnly ? " (редактирование шаблонов разрешено только администраторам)" : string.Empty);
    bool flag = false;
    AVSDocumentForm? specForm;
    AVSDocumentType? type;
    if (this.Selected != null)
    {
      specForm = this.Selected.SpecForm;
      if (!specForm.HasValue)
      {
        type = this.Selected.Type;
        flag = AVSDocumentsSettings.IsSpecificationDocType(type.Value) || this.Selected.Childs.Count == 1;
      }
      else
        flag = true;
    }
    this.bSetTemplate.Enabled = flag && !this.isReadOnly;
    Button bRemove = this.bRemove;
    int num;
    if (this.Selected != null)
    {
      specForm = this.Selected.SpecForm;
      if (!specForm.HasValue)
      {
        type = this.Selected.Type;
        AVSDocumentType avsDocumentType1 = AVSDocumentType.UserAVSDocument;
        if (!(type.GetValueOrDefault() == avsDocumentType1 & type.HasValue))
        {
          type = this.Selected.Type;
          AVSDocumentType avsDocumentType2 = AVSDocumentType.UserSpecification;
          if (!(type.GetValueOrDefault() == avsDocumentType2 & type.HasValue))
            goto label_9;
        }
        num = !this.isReadOnly ? 1 : 0;
        goto label_10;
      }
    }
label_9:
    num = 0;
label_10:
    bRemove.Enabled = num != 0;
    this.bAdd.Enabled = !this.isReadOnly;
    this.bOk.Enabled = !this.isReadOnly;
    this.bCancel.Enabled = !this.isReadOnly;
  }

  /// <summary>Обновление вида с текущим шаблоном</summary>
  /// <param name="id"></param>
  private void UpdateView(long id, DocumentType docType)
  {
    if (id.IsDefinedId())
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        this.labelName.Text = sessionKeeper.Session.GetObjectActual(id, true).Caption;
    }
    else
      this.labelName.Text = "Не назначен шаблон";
    bool flag = id == -1L;
    if (id == -1L)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        id = AvsIDCache.GetCommonSpecificationTemplateId(sessionKeeper.Session, out Guid _);
    }
    ISelectedItems items = Intermech.Navigator.ContextMenu.ObjectExtensions.GetItems(id);
    if (items.Count == 0)
      items = Intermech.Navigator.ContextMenu.ObjectExtensions.GetItems(-1L * id);
    if (!this.PageViewsManager.Visible)
    {
      this._labelSelectProduct.Visible = false;
      this._labelSelectProduct.Dock = DockStyle.Top;
      this.PageViewsManager.Dock = DockStyle.Fill;
      this.PageViewsManager.Visible = true;
      this.PageViewsManager.BringToFront();
    }
    if (this.PageViewsManager.Services == null)
    {
      ServiceContainer serviceContainer = new ServiceContainer();
      ViewStateService serviceInstance1 = new ViewStateService(ViewStateFlags.NoCompositionView | ViewStateFlags.NoContainsInView | ViewStateFlags.NoEventsView | ViewStateFlags.NoGroupingObjectsViews | ViewStateFlags.InParametersCard);
      serviceContainer.AddService(typeof (IViewState), (object) serviceInstance1);
      IAVSTemplatesViewsService serviceInstance2 = (IAVSTemplatesViewsService) new AVSTemplatesViewsService();
      serviceContainer.AddService(typeof (IAVSTemplatesViewsService), (object) serviceInstance2);
      serviceContainer.AddService(typeof (ICommandManager), (object) (ICommandManager) ServicesManager.GetService(typeof (ICommandManager)));
      serviceContainer.AddService(typeof (INotificationService), (object) (INotificationService) ServicesManager.GetService(typeof (INotificationService)));
      this.PageViewsManager.Services = (System.IServiceProvider) serviceContainer;
      this.PageViewsManager.AllowedViews = new string[9]
      {
        "ObjectFiles",
        "ObjectProperties",
        "ObjectVisualizer",
        "SetupKeyWordsView",
        "SetupNumberingView",
        "SetupSkipLinesView",
        "SetupSortingView",
        "SetupObjectTypesView",
        "SetupOutputView"
      };
    }
    IAVSTemplatesViewsService service = this.PageViewsManager.Services.GetService(typeof (IAVSTemplatesViewsService)) as IAVSTemplatesViewsService;
    service.ShowCommonTemplate = !this.Selected.SpecForm.HasValue;
    service.DocumentType = docType;
    this.PageViewsManager.CloseViews();
    if (flag)
      this.PageViewsManager.AllowedViews = new string[1]
      {
        "SetupObjectTypesView"
      };
    else
      this.PageViewsManager.AllowedViews = new string[9]
      {
        "ObjectFiles",
        "ObjectProperties",
        "ObjectVisualizer",
        "SetupKeyWordsView",
        "SetupNumberingView",
        "SetupSkipLinesView",
        "SetupSortingView",
        "SetupObjectTypesView",
        "SetupOutputView"
      };
    this.PageViewsManager.UpdateViews(items, true);
  }

  /// <summary>Внесение шаблона в словарь</summary>
  /// <param name="docType">Тип документа</param>
  /// <param name="docForm">Форма группового или единичного документа</param>
  /// <param name="templateID">Идентификатор шаблона</param>
  /// <param name="templateGuid">Глобальный идентификатор шаблона</param>
  private void SetTemplate(DocumentType docType, long templateID, Guid templateGuid)
  {
    if (!docType.SpecForm.HasValue && docType.Childs.Count == 1)
      docType = docType.Childs[0];
    docType.Template = new Template(templateID, templateGuid, true);
    docType.Changed = true;
    this.UpdateTree();
  }

  private long GetCommonTemplate(DocumentType docType, out Guid templateGuid)
  {
    if (docType.Template != null)
    {
      templateGuid = docType.Template.Guid;
      return docType.Template.Id;
    }
    AVSDocumentType? type = docType.Type;
    if (type.HasValue)
    {
      type = docType.Type;
      if (AVSDocumentsSettings.IsSpecificationDocType(type.Value))
      {
        long specificationTemplateId;
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          specificationTemplateId = AvsIDCache.GetCommonSpecificationTemplateId(sessionKeeper.Session, out templateGuid);
        docType.Template = new Template(specificationTemplateId, templateGuid);
        return specificationTemplateId;
      }
    }
    templateGuid = Guid.Empty;
    return -1;
  }

  internal static Template GetTemplate(DocumentType docType, Guid objType)
  {
    Guid templateGuid;
    long template;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      template = AVSDocumentsSettings.Instance.GetTemplate(docType.Guid, docType.SpecForm, out templateGuid, sessionKeeper.Session, false);
    return new Template(template, templateGuid);
  }

  /// <summary>Получение шаблона для типа</summary>
  /// <param name="docType">Тип документа</param>
  /// <param name="docForm">Форма группового или единичного документа</param>
  /// <param name="templateGuid">Глобальный идентификатор шаблона</param>
  /// <param name="userSession">Пользовательская сессия</param>
  /// <param name="failIfNotFound">Выдать исключение, если шаблон не найден</param>
  /// <returns></returns>
  private long GetTemplate(DocumentType docType, out Guid templateGuid, bool failIfNotFound)
  {
    if (docType.Template != null)
    {
      templateGuid = docType.Template.Guid;
      return docType.Template.Id;
    }
    long template;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      template = AVSDocumentsSettings.Instance.GetTemplate(docType.Guid, docType.SpecForm, out templateGuid, sessionKeeper.Session, false);
    docType.Template = new Template(template, templateGuid);
    return template;
  }

  private DocumentType Selected
  {
    get
    {
      return this.treeView.SelectedNode != null ? this.treeView.SelectedNode.Tag as DocumentType : (DocumentType) null;
    }
    set => this.SetSelected(value, this.treeView.Nodes);
  }

  private void SetSelected(DocumentType type, TreeNodeCollection col)
  {
    foreach (TreeNode treeNode in col)
    {
      if (treeNode.Tag == type)
      {
        this.treeView.SelectedNode = treeNode;
        break;
      }
      this.SetSelected(type, treeNode.Nodes);
    }
  }

  private void bSetTemplate_Click(object sender, EventArgs e)
  {
    if (this.Selected == null)
      return;
    object[] objArray = Intermech.Navigator.SelectionWindow.Select("Выберите объект", new DescriptorCollection()
    {
      (IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(AvsIDCache.ObjType_ConstructorDocumentTemplate)
    }[0], typeof (IDBTypedObjectID), SelectionOptions.SelectObjects | SelectionOptions.DisableMultiselect | SelectionOptions.ForceFilterObjectsByRule);
    if (objArray == null || objArray.Length == 0 || !(objArray[0] is IDBTypedObjectID))
      return;
    long objectId = ((IDBTypedObjectID) objArray[0]).ObjectID;
    Guid templateGuid = Guid.Empty;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(objectId);
      if (dbObject != null)
        templateGuid = dbObject.ObjectGUID;
    }
    if (!(templateGuid != Guid.Empty))
      return;
    this.SetTemplate(this.Selected, objectId, templateGuid);
  }

  private void UpdateTree()
  {
    DocumentType selected = this.Selected;
    this.treeView.BeginUpdate();
    this.treeView.Nodes.Clear();
    try
    {
      Dictionary<string, TreeNode> dictionary = new Dictionary<string, TreeNode>();
      foreach (DocumentType documentTemplate in this.DocumentTemplates)
      {
        string name = documentTemplate.Name;
        string str = documentTemplate.Type.HasValue ? EnumCustomConverter.GetEnumCategory((Enum) (ValueType) documentTemplate.Type) : string.Empty;
        TreeNode node1 = new TreeNode(name)
        {
          Tag = (object) documentTemplate,
          ImageIndex = documentTemplate.Changed ? 1 : -1
        };
        node1.SelectedImageIndex = node1.ImageIndex;
        if (str.Equals(string.Empty))
        {
          this.treeView.Nodes.Add(node1);
        }
        else
        {
          TreeNode treeNode;
          if (dictionary.TryGetValue(str, out treeNode))
          {
            treeNode.Nodes.Add(node1);
            treeNode.ImageIndex = documentTemplate.Changed ? 1 : -1;
            treeNode.SelectedImageIndex = treeNode.ImageIndex;
          }
          else
          {
            TreeNode node2 = new TreeNode(str)
            {
              Tag = (object) documentTemplate,
              ImageIndex = documentTemplate.Changed ? 1 : -1
            };
            node2.SelectedImageIndex = node2.ImageIndex;
            dictionary[str] = node2;
            this.treeView.Nodes.Add(node2);
            node2.Nodes.Add(node1);
            node2.Expand();
          }
        }
        foreach (DocumentType child in documentTemplate.Childs)
        {
          TreeNode node3 = new TreeNode(child.Name)
          {
            Tag = (object) child,
            ImageIndex = child.Changed ? 1 : -1
          };
          node3.SelectedImageIndex = node3.ImageIndex;
          node1.Nodes.Add(node3);
        }
        node1.Expand();
      }
      this.Selected = selected;
    }
    finally
    {
      this.treeView.EndUpdate();
    }
  }

  public void UpdateCaptions(bool suspend)
  {
    if (this.treeView.IsDisposed)
      return;
    if (suspend)
      this.treeView.BeginUpdate();
    try
    {
      foreach (TreeNode node1 in this.treeView.Nodes)
      {
        DocumentType tag1 = node1.Tag as DocumentType;
        node1.Text = tag1.Name;
        node1.ImageIndex = tag1.Changed ? 1 : -1;
        node1.SelectedImageIndex = node1.ImageIndex;
        foreach (TreeNode node2 in node1.Nodes)
        {
          DocumentType tag2 = node2.Tag as DocumentType;
          node2.Text = tag2.Name;
          node2.ImageIndex = tag2.Changed ? 1 : -1;
          node2.SelectedImageIndex = node2.ImageIndex;
        }
      }
    }
    finally
    {
      if (suspend)
        this.treeView.EndUpdate();
    }
  }

  private void treeList_GetCellData(object sender, GetCellDataEventArgs e)
  {
    if (e.Row.Item is AVSDocumentType)
    {
      string enumDescription = EnumCustomConverter.GetEnumDescription((Enum) (AVSDocumentType) e.Row.Item);
      e.CellData.Value = (object) enumDescription;
    }
    if (!(e.Row.Item is AVSDocumentForm))
      return;
    int num = (int) e.Row.ParentRow.Item;
    string enumDescription1 = EnumCustomConverter.GetEnumDescription((Enum) (AVSDocumentForm) e.Row.Item);
    e.CellData.Value = (object) enumDescription1;
  }

  private AVSDocumentTypeSettings FindSet(Guid g, IUserSession session)
  {
    AVSDocumentTypeSettings set = (AVSDocumentTypeSettings) null;
    foreach (AVSDocumentTypeSettings avsDocumentType in AVSDocumentsSettings.GetAvsDocumentTypes(session))
    {
      if (avsDocumentType.TypeGuid == g)
      {
        set = avsDocumentType;
        break;
      }
    }
    return set;
  }

  private bool SaveToItem(DocumentType t, IUserSession session)
  {
    AVSDocumentTypeSettings settings = this.FindSet(t.Guid, session);
    if (t.Type.HasValue)
    {
      AVSDocumentType? type;
      if (settings == null)
      {
        Guid guid = t.Guid;
        type = t.Type;
        int avsDocType = (int) type.Value;
        List<Guid> dbObjectTypeList = new List<Guid>();
        settings = new AVSDocumentTypeSettings(guid, (AVSDocumentType) avsDocType, "", dbObjectTypeList);
        AVSDocumentsSettings.AddAVSDocumentTypeSettings(settings);
      }
      type = t.Type;
      if (type.Value != AVSDocumentType.UserAVSDocument)
      {
        type = t.Type;
        if (type.Value != AVSDocumentType.UserSpecification)
          goto label_6;
      }
      settings.TypeName = t.Name;
    }
label_6:
    settings.DBObjectTypeList = t.DBObjectTypeList;
    return t.Changed;
  }

  private bool DeleteDeleted(IUserSession session)
  {
    bool flag = false;
    foreach (DocumentType documentType in this.removed)
    {
      AVSDocumentTypeSettings set = this.FindSet(documentType.Guid, session);
      if (set != null)
      {
        AVSDocumentsSettings.RemoveAVSDocumentTypeSettings(set);
        flag = true;
      }
    }
    return flag;
  }

  /// <summary>Сохранение данных</summary>
  public bool Save()
  {
    bool flag = false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (DocumentType documentTemplate in this.DocumentTemplates)
      {
        if (documentTemplate.TypeGuid == Guid.Empty)
        {
          int num = (int) MessageBox.Show($"Для документа '{documentTemplate.Name}' не назначено ни одного типа", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Hand);
          return false;
        }
        if (documentTemplate.Template != null && documentTemplate.Template.Changed && documentTemplate.Type.HasValue)
        {
          AVSDocumentsSettings.Instance.SetTemplate(documentTemplate.Guid, new AVSDocumentForm?(), documentTemplate.Template.Guid, false, sessionKeeper.Session);
          flag = true;
        }
        flag |= this.SaveToItem(documentTemplate, sessionKeeper.Session);
        foreach (DocumentType child in documentTemplate.Childs)
        {
          if (child.Template != null && child.Template.Changed)
          {
            foreach (Guid dbObjectType in documentTemplate.DBObjectTypeList)
            {
              AVSDocumentsSettings.Instance.SetTemplate(documentTemplate.Guid, child.SpecForm, child.Template.Guid, false, sessionKeeper.Session);
              flag = true;
            }
          }
          foreach (Guid dbObjectType in documentTemplate.DBObjectTypeList)
            this.SetIntegrator(dbObjectType, true, sessionKeeper.Session);
        }
      }
      this.DeleteDeleted(sessionKeeper.Session);
      if (flag)
        AVSDocumentsSettings.SaveSettingsToDB(sessionKeeper.Session);
    }
    return true;
  }

  private void SetIntegrator(Guid objType, bool set, IUserSession session)
  {
    ILaunchActionServer service1 = ServiceUtils.GetService<ILaunchActionServer>((object) session, true);
    ILaunchActionServer service2 = ServiceUtils.GetService<ILaunchActionServer>((object) session, true);
    ILaunchHandler handler = ClientContext.LaunchActions.GetHandler(AVSIntegrator.IntegratorId, false);
    if (handler == null)
      return;
    if (set)
    {
      string serverObjectTemplate = handler.GetServerObjectTemplate();
      if (this.GetInfo(service2, objType, LaunchType.Edit) == null)
      {
        LaunchActionInfo action = service2.CreateAction(objType, (ITarget) AllUsersTarget.Value, LaunchType.Edit, handler.Id, serverObjectTemplate);
        service1.SetDefaultAction(objType, (ITarget) AllUsersTarget.Value, action.ActionId);
      }
      if (this.GetInfo(service2, objType, LaunchType.Print) == null)
      {
        LaunchActionInfo action = service2.CreateAction(objType, (ITarget) AllUsersTarget.Value, LaunchType.Print, handler.Id, serverObjectTemplate);
        service1.SetDefaultAction(objType, (ITarget) AllUsersTarget.Value, action.ActionId);
      }
      if (this.GetInfo(service2, objType, LaunchType.View) != null)
        return;
      LaunchActionInfo action1 = service2.CreateAction(objType, (ITarget) AllUsersTarget.Value, LaunchType.View, handler.Id, serverObjectTemplate);
      service1.SetDefaultAction(objType, (ITarget) AllUsersTarget.Value, action1.ActionId);
    }
    else
    {
      LaunchActionInfo info1 = this.GetInfo(service2, objType, LaunchType.Edit);
      if (info1 != null)
        service2.RemoveAction(info1.ActionId);
      LaunchActionInfo info2 = this.GetInfo(service2, objType, LaunchType.Print);
      if (info2 != null)
        service2.RemoveAction(info2.ActionId);
      LaunchActionInfo info3 = this.GetInfo(service2, objType, LaunchType.View);
      if (info3 == null)
        return;
      service2.RemoveAction(info3.ActionId);
    }
  }

  private LaunchActionInfo GetInfo(
    ILaunchActionServer launchActions,
    Guid objType,
    LaunchType type)
  {
    foreach (LaunchActionInfo action in launchActions.GetActionList(objType, (ITarget) AllUsersTarget.Value, type))
    {
      if (action.HandlerId == AVSIntegrator.IntegratorId)
        return action;
    }
    return (LaunchActionInfo) null;
  }

  private void treeList_GetRowData(object sender, GetRowDataEventArgs e)
  {
  }

  private void treeList_GetChildren(object sender, GetChildrenEventArgs e)
  {
    if (e.Row.Item is string)
    {
      Array values = Enum.GetValues(typeof (AVSDocumentType));
      e.Children = (IList) values;
    }
    if (!(e.Row.Item is AVSDocumentType))
      return;
    AVSDocumentForm[] allowableDocumentForm = AVSDocumentsSettings.GetAllowableDocumentForm((AVSDocumentType) e.Row.Item);
    e.Children = (IList) allowableDocumentForm;
  }

  private void treeList_SelectionChanged(object sender, EventArgs e)
  {
  }

  private void bAdd_Click(object sender, EventArgs e)
  {
    this.menuDocument.Show((Control) this.bAdd, new Point(0, this.bAdd.Height));
  }

  private void bRemove_Click(object sender, EventArgs e)
  {
    if (this.Selected == null)
      return;
    AVSDocumentType? type = this.Selected.Type;
    AVSDocumentType avsDocumentType1 = AVSDocumentType.UserAVSDocument;
    if (!(type.GetValueOrDefault() == avsDocumentType1 & type.HasValue))
    {
      type = this.Selected.Type;
      AVSDocumentType avsDocumentType2 = AVSDocumentType.UserSpecification;
      if (!(type.GetValueOrDefault() == avsDocumentType2 & type.HasValue))
        return;
    }
    if (this.Selected.SpecForm.HasValue)
      return;
    this.DocumentTemplates.Remove(this.Selected);
    this.removed.Add(this.Selected);
    this.UpdateTree();
  }

  private void treeList_BeforeShowCellEdit(object sender, BeforeShowCellEditEventArgs e)
  {
  }

  private void treeList_SetCellValue(object sender, SetCellValueEventArgs e)
  {
  }

  private void treeList_TextChanged(object sender, EventArgs e)
  {
  }

  private void cellEditor1_SetControlValue(object sender, CellEditorSetValueEventArgs e)
  {
    this.textBox1.Text = (string) e.Value;
  }

  private void cellEditor1_GetControlValue(object sender, CellEditorGetValueEventArgs e)
  {
  }

  private void treeView_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
  {
    if (e.Node == null || e.Node.Tag is DocumentType tag && tag.CanChangeName)
      return;
    e.CancelEdit = true;
    this.treeView.LabelEdit = false;
  }

  private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
  {
    this.treeView.Enabled = false;
    try
    {
      Guid templateGuid;
      this.UpdateView(this.Selected == null ? long.MinValue : (!this.Selected.SpecForm.HasValue ? (this.Selected.Childs.Count != 1 ? this.GetCommonTemplate(this.Selected, out templateGuid) : this.GetTemplate(this.Selected.Childs[0], out templateGuid, false)) : this.GetTemplate(this.Selected, out templateGuid, false)), this.Selected);
      this.UpdateControls();
    }
    finally
    {
      this.treeView.Enabled = true;
    }
  }

  private void treeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
  {
    if (e.Node == null)
      return;
    if (AVSDocumentTypesTemplateForm.IsValidNewNodeLabel(e))
    {
      if (!(e.Node.Tag is DocumentType tag))
        return;
      tag.Name = e.Label.Trim();
      tag.Changed = true;
      e.Node.EndEdit(false);
      this.treeView.LabelEdit = false;
    }
    else if (e.Label == null && !this.isNewNode)
    {
      e.Node.EndEdit(true);
      this.treeView.LabelEdit = false;
    }
    else
    {
      this.isNewNode = false;
      e.CancelEdit = true;
      e.Node.BeginEdit();
    }
  }

  private void treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
  {
    if (e.Button != MouseButtons.Right || this.treeView.LabelEdit || e.Node.IsEditing || (!(e.Node.Tag is DocumentType tag) ? 0 : (tag.CanChangeName ? 1 : 0)) == 0)
      return;
    this.treeView.LabelEdit = true;
    this.isNewNode = false;
    e.Node.BeginEdit();
  }

  private static bool IsValidNewNodeLabel(NodeLabelEditEventArgs e)
  {
    if (string.IsNullOrWhiteSpace(e.Label))
      return false;
    TreeNode parent = e.Node.Parent;
    return (parent != null ? (parent.Nodes.OfType<TreeNode>().Any<TreeNode>((Func<TreeNode, bool>) (n => n.Text.Equals(e.Label, StringComparison.CurrentCultureIgnoreCase))) ? 1 : 0) : 0) == 0;
  }

  private void menuDocument_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
  {
    DocumentType documentType = (DocumentType) null;
    if (e.ClickedItem == this.itemDocument)
    {
      string userDocumentType = this.GenerateNodeNameForUserDocumentType(AVSDocumentType.UserAVSDocument);
      documentType = new DocumentType(Guid.NewGuid(), new AVSDocumentType?(AVSDocumentType.UserAVSDocument), new AVSDocumentForm?(), userDocumentType, this);
      documentType.Childs.Add(new DocumentType(Guid.NewGuid(), new AVSDocumentType?(AVSDocumentType.UserAVSDocument), new AVSDocumentForm?(AVSDocumentForm.Single), (string) null, this)
      {
        Parent = documentType
      });
    }
    if (e.ClickedItem == this.itemSpecification)
    {
      string userDocumentType = this.GenerateNodeNameForUserDocumentType(AVSDocumentType.UserSpecification);
      documentType = new DocumentType(Guid.NewGuid(), new AVSDocumentType?(AVSDocumentType.UserSpecification), new AVSDocumentForm?(), userDocumentType, this);
      documentType.Childs.Add(new DocumentType(Guid.NewGuid(), new AVSDocumentType?(AVSDocumentType.UserSpecification), new AVSDocumentForm?(AVSDocumentForm.Single), (string) null, this)
      {
        Parent = documentType
      });
      documentType.Childs.Add(new DocumentType(Guid.NewGuid(), new AVSDocumentType?(AVSDocumentType.UserSpecification), new AVSDocumentForm?(AVSDocumentForm.A), (string) null, this)
      {
        Parent = documentType
      });
      documentType.Childs.Add(new DocumentType(Guid.NewGuid(), new AVSDocumentType?(AVSDocumentType.UserSpecification), new AVSDocumentForm?(AVSDocumentForm.B), (string) null, this)
      {
        Parent = documentType
      });
    }
    if (documentType == null)
      return;
    documentType.Changed = true;
    this.DocumentTemplates.Add(documentType);
    this.UpdateTree();
    this.Selected = documentType;
    this.treeView.LabelEdit = true;
    this.isNewNode = true;
    this.treeView.SelectedNode.BeginEdit();
  }

  private string GenerateNodeNameForUserDocumentType(AVSDocumentType userDocType)
  {
    string userDocumentType = userDocType == AVSDocumentType.UserSpecification ? "Пользовательская спецификация" : "Пользовательский конструкторский документ";
    List<string> list = this.treeView.Nodes.OfType<TreeNode>().SelectMany<TreeNode, TreeNode>((Func<TreeNode, IEnumerable<TreeNode>>) (mn => mn.Nodes.OfType<TreeNode>())).Where<TreeNode>((Func<TreeNode, bool>) (sn =>
    {
      if (!(sn.Tag is DocumentType tag2))
        return false;
      AVSDocumentType? type = tag2.Type;
      AVSDocumentType avsDocumentType = userDocType;
      return type.GetValueOrDefault() == avsDocumentType & type.HasValue;
    })).Select<TreeNode, string>((Func<TreeNode, string>) (n => n.Text)).ToList<string>();
    if (list.Count > 0)
    {
      for (int index = 0; index < list.Count + 100; ++index)
      {
        string str1 = index == 0 ? "" : $" #{index}";
        string str2 = userDocumentType + str1;
        if (!list.Contains(str2))
        {
          userDocumentType = str2;
          break;
        }
      }
    }
    return userDocumentType;
  }

  private void PageViewsManagerActiveViewPageChanged(object sender, EventArgs eventArgs)
  {
    Control control = this.PageViewsManager.ActiveViewPage?.Control;
    if (control == null)
      return;
    int activeFormCancelButtonRightEdge = -1;
    if (control.Controls.OfType<object>().FirstOrDefault<object>((Func<object, bool>) (c => c is ExtForm)) is ExtForm extForm)
      activeFormCancelButtonRightEdge = extForm.CancelButtonRightEdge;
    else if (((IEnumerable<Control>) control.Controls.Find("_BtnCancel", true)).FirstOrDefault<Control>() is Button button)
      activeFormCancelButtonRightEdge = button.Parent.Width - (button.Left + button.Width);
    this.AdjustOkCancelButtonsLocations(activeFormCancelButtonRightEdge);
  }

  private void AdjustOkCancelButtonsLocations(int activeFormCancelButtonRightEdge)
  {
    if (activeFormCancelButtonRightEdge == -1)
      return;
    this.bCancel.Left = this.panel1.Width - this.bCancel.Width - activeFormCancelButtonRightEdge;
    this.bOk.Left = this.bCancel.Left - this.bCancel.Width - 5;
  }

  /// <summary>Установить текущими настройки шаблона с заданным ID</summary>
  /// <param name="templateId"></param>
  internal void PointTo(long templateId, AVSDocumentForm? form)
  {
    if (templateId.IsUndefinedId())
      return;
    Guid docTypeGuid = Guid.Empty;
    AVSDocumentForm docFormType = AVSDocumentForm.G;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach ((Guid docType, AVSDocumentForm docForm) tuple in AVSDocumentsSettings.Instance.FindTypeAndFormForTemplate(templateId, sessionKeeper.Session, false))
      {
        if (docFormType >= tuple.docForm && tuple.docForm >= AVSDocumentForm.Single)
        {
          docFormType = tuple.docForm;
          docTypeGuid = tuple.docType;
        }
      }
    }
    if (form.HasValue)
      docFormType = form.Value;
    if (docTypeGuid == Guid.Empty)
      return;
    TreeNode node = this.treeView.Nodes.FindNode((Predicate<TreeNode>) (n =>
    {
      if (!(n.Tag is DocumentType tag2) || !(tag2.Guid == docTypeGuid))
        return false;
      AVSDocumentForm? specForm = tag2.SpecForm;
      AVSDocumentForm avsDocumentForm = docFormType;
      return specForm.GetValueOrDefault() == avsDocumentForm & specForm.HasValue;
    }));
    if (node == null)
      return;
    this.treeView.SelectedNode = node;
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
    this.splitContainer1 = new SplitContainer();
    this.panelLeftBottom = new Panel();
    this.bRemove = new Button();
    this.bAdd = new Button();
    this.treeView = new TreeView();
    this.imageList1 = new ImageList(this.components);
    this.treeList = new Intermech.VirtualTreeView.VirtualTreeView();
    this.colName = new Column();
    this.cellEditor1 = new CellEditor();
    this.textBox1 = new TextBox();
    this.panelRightTop = new Panel();
    this.panel2 = new Panel();
    this.label1 = new Label();
    this.bSetTemplate = new Button();
    this.labelName = new Label();
    this._labelSelectProduct = new Label();
    this.PageViewsManager = new PageViewsManager();
    this.panel1 = new Panel();
    this.bCancel = new Button();
    this.bOk = new Button();
    this.menuDocument = new ContextMenuStrip(this.components);
    this.itemDocument = new ToolStripMenuItem();
    this.itemSpecification = new ToolStripMenuItem();
    this.addRemoveTooltip = new ToolTip(this.components);
    this.splitContainer1.BeginInit();
    this.splitContainer1.Panel1.SuspendLayout();
    this.splitContainer1.Panel2.SuspendLayout();
    this.splitContainer1.SuspendLayout();
    this.panelLeftBottom.SuspendLayout();
    this.treeList.BeginInit();
    this.panelRightTop.SuspendLayout();
    this.panel2.SuspendLayout();
    this.panel1.SuspendLayout();
    this.menuDocument.SuspendLayout();
    this.SuspendLayout();
    this.splitContainer1.Dock = DockStyle.Fill;
    this.splitContainer1.Location = new Point(0, 0);
    this.splitContainer1.Name = "splitContainer1";
    this.splitContainer1.Panel1.Controls.Add((Control) this.panelLeftBottom);
    this.splitContainer1.Panel1.Controls.Add((Control) this.treeView);
    this.splitContainer1.Panel1.Controls.Add((Control) this.treeList);
    this.splitContainer1.Panel2.Controls.Add((Control) this.panelRightTop);
    this.splitContainer1.Panel2.Controls.Add((Control) this.textBox1);
    this.splitContainer1.Panel2.Controls.Add((Control) this.panel1);
    this.splitContainer1.Size = new Size(984, 682);
    this.splitContainer1.SplitterDistance = 283;
    this.splitContainer1.SplitterWidth = 5;
    this.splitContainer1.TabIndex = 0;
    this.panelLeftBottom.Controls.Add((Control) this.bRemove);
    this.panelLeftBottom.Controls.Add((Control) this.bAdd);
    this.panelLeftBottom.Dock = DockStyle.Bottom;
    this.panelLeftBottom.Location = new Point(0, 635);
    this.panelLeftBottom.Name = "panelLeftBottom";
    this.panelLeftBottom.Size = new Size(283, 47);
    this.panelLeftBottom.TabIndex = 5;
    this.bRemove.Image = (Image) Resources.deleteLarge;
    this.bRemove.Location = new Point(45, 9);
    this.bRemove.Name = "bRemove";
    this.bRemove.Size = new Size(27, 27);
    this.bRemove.TabIndex = 4;
    this.addRemoveTooltip.SetToolTip((Control) this.bRemove, "Удалить");
    this.bRemove.UseVisualStyleBackColor = true;
    this.bRemove.Click += new EventHandler(this.bRemove_Click);
    this.bAdd.Image = (Image) Resources.addLarge;
    this.bAdd.Location = new Point(12, 9);
    this.bAdd.Name = "bAdd";
    this.bAdd.Size = new Size(27, 27);
    this.bAdd.TabIndex = 3;
    this.addRemoveTooltip.SetToolTip((Control) this.bAdd, "Добавить");
    this.bAdd.UseVisualStyleBackColor = true;
    this.bAdd.Click += new EventHandler(this.bAdd_Click);
    this.treeView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.treeView.HideSelection = false;
    this.treeView.ImageIndex = 0;
    this.treeView.ImageList = this.imageList1;
    this.treeView.Location = new Point(0, 0);
    this.treeView.Name = "treeView";
    this.treeView.SelectedImageIndex = 0;
    this.treeView.Size = new Size(283, 630);
    this.treeView.TabIndex = 4;
    this.treeView.BeforeLabelEdit += new NodeLabelEditEventHandler(this.treeView_BeforeLabelEdit);
    this.treeView.AfterLabelEdit += new NodeLabelEditEventHandler(this.treeView_AfterLabelEdit);
    this.treeView.AfterSelect += new TreeViewEventHandler(this.treeView_AfterSelect);
    this.imageList1.ColorDepth = ColorDepth.Depth32Bit;
    this.imageList1.ImageSize = new Size(16 /*0x10*/, 16 /*0x10*/);
    this.imageList1.TransparentColor = Color.Transparent;
    this.treeList.AllowDrop = true;
    this.treeList.AllowIndividualRowResize = false;
    this.treeList.AutoFitColumns = true;
    this.treeList.Columns.Add(this.colName);
    this.treeList.DisableHeaderContextMenu = false;
    this.treeList.Dock = DockStyle.Fill;
    this.treeList.Editors.Add(this.cellEditor1);
    this.treeList.ImageList = (ImageList) null;
    this.treeList.Location = new Point(0, 0);
    this.treeList.Name = "treeList";
    this.treeList.RowStyle.ForeColor = SystemColors.WindowText;
    this.treeList.SelectBeforeEdit = true;
    this.treeList.ShowColumnHeaders = false;
    this.treeList.ShowRootRow = false;
    this.treeList.Size = new Size(283, 682);
    this.treeList.TabIndex = 3;
    this.treeList.Visible = false;
    this.treeList.BeforeShowCellEdit += new BeforeShowCellEditHandler(this.treeList_BeforeShowCellEdit);
    this.treeList.GetCellData += new GetCellDataHandler(this.treeList_GetCellData);
    this.treeList.GetChildren += new GetChildrenHandler(this.treeList_GetChildren);
    this.treeList.GetRowData += new GetRowDataHandler(this.treeList_GetRowData);
    this.treeList.SelectionChanged += new EventHandler(this.treeList_SelectionChanged);
    this.treeList.SetCellValue += new SetCellValueHandler(this.treeList_SetCellValue);
    this.treeList.TextChanged += new EventHandler(this.treeList_TextChanged);
    this.colName.AutoSizePolicy = ColumnAutoSizePolicy.AutoSize;
    this.colName.Caption = (string) null;
    this.colName.CellEditor = this.cellEditor1;
    this.colName.Name = "colName";
    this.colName.Width = 30;
    this.cellEditor1.Control = (Control) this.textBox1;
    this.cellEditor1.GetControlValue += new CellEditorGetValueHandler(this.cellEditor1_GetControlValue);
    this.cellEditor1.SetControlValue += new CellEditorSetValueHandler(this.cellEditor1_SetControlValue);
    this.textBox1.Location = new Point(327, 475);
    this.textBox1.Name = "textBox1";
    this.textBox1.Size = new Size(199, 20);
    this.textBox1.TabIndex = 5;
    this.textBox1.Visible = false;
    this.panelRightTop.Controls.Add((Control) this.panel2);
    this.panelRightTop.Controls.Add((Control) this._labelSelectProduct);
    this.panelRightTop.Controls.Add((Control) this.PageViewsManager);
    this.panelRightTop.Dock = DockStyle.Fill;
    this.panelRightTop.Location = new Point(0, 0);
    this.panelRightTop.Name = "panelRightTop";
    this.panelRightTop.Size = new Size(696, 630);
    this.panelRightTop.TabIndex = 6;
    this.panel2.Controls.Add((Control) this.label1);
    this.panel2.Controls.Add((Control) this.bSetTemplate);
    this.panel2.Controls.Add((Control) this.labelName);
    this.panel2.Dock = DockStyle.Top;
    this.panel2.Location = new Point(0, 0);
    this.panel2.Name = "panel2";
    this.panel2.Size = new Size(696, 37);
    this.panel2.TabIndex = 4;
    this.label1.AutoSize = true;
    this.label1.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    this.label1.Location = new Point(16 /*0x10*/, 12);
    this.label1.Name = "label1";
    this.label1.Size = new Size(158, 20);
    this.label1.TabIndex = 2;
    this.label1.Text = "Шаблон документа:";
    this.label1.TextAlign = ContentAlignment.TopRight;
    this.bSetTemplate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bSetTemplate.Location = new Point(572, 3);
    this.bSetTemplate.Name = "bSetTemplate";
    this.bSetTemplate.Size = new Size(121, 27);
    this.bSetTemplate.TabIndex = 1;
    this.bSetTemplate.Text = "Выбрать...";
    this.bSetTemplate.UseVisualStyleBackColor = true;
    this.bSetTemplate.Click += new EventHandler(this.bSetTemplate_Click);
    this.labelName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.labelName.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
    this.labelName.Location = new Point(175, 12);
    this.labelName.Name = "labelName";
    this.labelName.Size = new Size(429, 18);
    this.labelName.TabIndex = 0;
    this.labelName.Text = "label1";
    this._labelSelectProduct.Dock = DockStyle.Fill;
    this._labelSelectProduct.Font = new Font("Verdana", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    this._labelSelectProduct.Location = new Point(0, 0);
    this._labelSelectProduct.Name = "_labelSelectProduct";
    this._labelSelectProduct.Size = new Size(696, 418);
    this._labelSelectProduct.TabIndex = 3;
    this._labelSelectProduct.Text = "Выберите документ";
    this._labelSelectProduct.TextAlign = ContentAlignment.MiddleCenter;
    this.PageViewsManager.ActiveViewPage = (IViewPage) null;
    this.PageViewsManager.BorderStyle = BorderStyle.FixedSingle;
    this.PageViewsManager.CausesValidation = false;
    this.PageViewsManager.Dock = DockStyle.Bottom;
    this.PageViewsManager.Font = new Font("Tahoma", 8.25f);
    this.PageViewsManager.Location = new Point(0, 418);
    this.PageViewsManager.Name = "PageViewsManager";
    this.PageViewsManager.Padding = new Padding(10, 0, 0, 0);
    this.PageViewsManager.Size = new Size(696, 212);
    this.PageViewsManager.TabIndex = 2;
    this.PageViewsManager.Visible = false;
    this.panel1.Controls.Add((Control) this.bCancel);
    this.panel1.Controls.Add((Control) this.bOk);
    this.panel1.Dock = DockStyle.Bottom;
    this.panel1.Location = new Point(0, 630);
    this.panel1.Name = "panel1";
    this.panel1.Size = new Size(696, 52);
    this.panel1.TabIndex = 2;
    this.bCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bCancel.DialogResult = DialogResult.Cancel;
    this.bCancel.Location = new Point(550, 14);
    this.bCancel.Name = "bCancel";
    this.bCancel.Size = new Size(121, 27);
    this.bCancel.TabIndex = 2;
    this.bCancel.Text = "Закрыть";
    this.bCancel.UseVisualStyleBackColor = true;
    this.bOk.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bOk.DialogResult = DialogResult.OK;
    this.bOk.Location = new Point(423, 14);
    this.bOk.Name = "bOk";
    this.bOk.Size = new Size(121, 27);
    this.bOk.TabIndex = 1;
    this.bOk.Text = "OK";
    this.bOk.UseVisualStyleBackColor = true;
    this.menuDocument.Items.AddRange(new ToolStripItem[2]
    {
      (ToolStripItem) this.itemDocument,
      (ToolStripItem) this.itemSpecification
    });
    this.menuDocument.Name = "menuDocument";
    this.menuDocument.Size = new Size(233, 48 /*0x30*/);
    this.menuDocument.Text = "Пользовательский документ";
    this.menuDocument.ItemClicked += new ToolStripItemClickedEventHandler(this.menuDocument_ItemClicked);
    this.itemDocument.Name = "itemDocument";
    this.itemDocument.Size = new Size(232, 22);
    this.itemDocument.Text = "Пользовательский документ";
    this.itemSpecification.Name = "itemSpecification";
    this.itemSpecification.Size = new Size(232, 22);
    this.itemSpecification.Text = "Спецификация";
    this.AcceptButton = (IButtonControl) this.bOk;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.bCancel;
    this.ClientSize = new Size(984, 682);
    this.Controls.Add((Control) this.splitContainer1);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.MinimumSize = new Size(1000, 720);
    this.Name = nameof (AVSDocumentTypesTemplateForm);
    this.ShowInTaskbar = false;
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Настройка шаблонов";
    this.splitContainer1.Panel1.ResumeLayout(false);
    this.splitContainer1.Panel2.ResumeLayout(false);
    this.splitContainer1.Panel2.PerformLayout();
    this.splitContainer1.EndInit();
    this.splitContainer1.ResumeLayout(false);
    this.panelLeftBottom.ResumeLayout(false);
    this.treeList.EndInit();
    this.panelRightTop.ResumeLayout(false);
    this.panel2.ResumeLayout(false);
    this.panel2.PerformLayout();
    this.panel1.ResumeLayout(false);
    this.menuDocument.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
