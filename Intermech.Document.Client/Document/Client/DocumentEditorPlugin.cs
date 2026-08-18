// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.DocumentEditorPlugin
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.Client.Core.Navigator.ContextMenu;
using Intermech.Client.Core.Visualizers;
using Intermech.Commands;
using Intermech.Controls;
using Intermech.DataFormats;
using Intermech.Docking;
using Intermech.Document.Client.Comparison;
using Intermech.Document.Client.Report;
using Intermech.Document.Client.Reports;
using Intermech.Document.DBCore;
using Intermech.Document.Model;
using Intermech.Document.Model.ExternalDocuments;
using Intermech.Document.Model.UI;
using Intermech.Document.Model.Undo;
using Intermech.Document.UI;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.BlobStream;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Configuration;
using Intermech.Interfaces.Document;
using Intermech.Interfaces.Plugins;
using Intermech.Interfaces.Show;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.PropertyEditors;
using Intermech.PropertyEditors.AttrProcessor;
using Intermech.Search;
using Intermech.Search.Interfaces.Signs;
using Intermech.Tools;
using Intermech.Tools.Integrators;
using Intermech.Tools.LaunchActions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

#nullable disable
namespace Intermech.Document.Client;

/// <summary>Редактор документов. Плагин клиента</summary>
public class DocumentEditorPlugin : 
  DocumentEditorPluginBase,
  IPackage,
  IImDocumentManager,
  ICommandTarget,
  ICommandsProvider,
  IConfigurable,
  IDocumentPlugin,
  IDocumentNotifyService
{
  public static readonly Guid InsertSymbolPanelGuid = new Guid("{4857B19C-2F32-4943-A88D-3CB5A0A99B9D}");
  public static readonly Guid DocDataTreeViewGuid = new Guid("{6BB1482A-0014-4cb6-8311-FE6198040238}");
  /// <summary>GUID окна для восстановления</summary>
  public static readonly Guid AVSWindowGuid = new Guid("{8C09B1C6-4F81-44c5-8A3C-2F39BC189582}");
  /// <summary>GUID окна для восстановления</summary>
  public static readonly Guid ImDocumentEditorFormGuid = new Guid("{9F53AD16-2DF4-4a0e-8427-58B613DA9646}");
  /// <summary>GUID окна для восстановления</summary>
  public static readonly Guid VedomostWindowGuid = new Guid("{6EC02BAD-F550-42A8-964B-04CECE716ECF}");
  public static readonly string OpenDocumentCommand = "OpenDocument";
  public const string ContextParamAttributeID = "FileAttributeID";
  public const string ContextParamFileIndex = "FileIndex";
  private AttachedSelMenuService _asMenuService;
  private static DocumentEditorPlugin instance;
  private static IClientMetadataCache _metadataCache = (IClientMetadataCache) null;
  /// <summary>Служба для вызова методов из других потоков</summary>
  private static IInvokeService _iInvokeService = (IInvokeService) null;
  /// <summary>Сервис работы с правилами подбора версий</summary>
  private static IFiltrationService _iFiltrationService = (IFiltrationService) null;
  private static ImDocumentFilesComparisonPlugin _compPlugin = (ImDocumentFilesComparisonPlugin) null;
  private static ICompareFilesService _iCompFilesService = (ICompareFilesService) null;
  protected IVisualizer DwgVisualizer;
  private IConfigurationManager configManager;
  private static INotificationService notificationService = (INotificationService) null;
  public static bool IsLoaded = false;
  private SaveFileDialog saveToFileDialog;
  private string recentlySaveAsPath;
  private static bool docPluginInitialized = false;
  private System.IServiceProvider serviceProvider;
  private static DockManager dockManager = (DockManager) null;
  private static ICommandManager commandManager = (ICommandManager) null;
  private IStatusBar statusBar;
  private bool isElementSelecting = true;
  private bool isElementCreating;
  private PageElementCreator selectedElementCreator;
  private ArrayList elementCreators = new ArrayList();
  private ArrayList elementCreatorCommands = new ArrayList();
  private MenuBarItem pageElementsMenu;
  private MenuButtonItem selectElementMenuItem;
  private ButtonItem selectElementButton;
  private ICommandState selectElementCommand;
  public static ImageList imageList = new ImageList();
  private static bool useImDocEditorSettingsCache = false;
  private static IMDocEditorToolSettings imDocEditorSettingsCache = (IMDocEditorToolSettings) null;
  public List<Guid> SpecialDocumentLaunchHandlers = new List<Guid>();

  public static int[] GetAttributesForDBRelationType(int relationTypeID)
  {
    DataTable dataTable = DocumentEditorPlugin.MDCache.GetRelationType(relationTypeID).Attributes.Select("F_ATTRIBUTE_ID");
    int[] forDbRelationType = new int[dataTable.Rows.Count];
    for (int index = 0; index < dataTable.Rows.Count; ++index)
      forDbRelationType[index] = Convert.ToInt32(dataTable.Rows[index]["F_ATTRIBUTE_ID"]);
    dataTable.Dispose();
    return forDbRelationType;
  }

  public static int[] GetAttributesForDBObjectType(int objectType)
  {
    IDictionary dictionary = (IDictionary) new HybridDictionary();
    for (; objectType != -1; objectType = MetaDataHelper.GetObjectTypeParentID(objectType))
    {
      DataTable dataTable = DocumentEditorPlugin.MDCache.GetObjectType(objectType).Attributes.Select("F_ATTRIBUTE_ID");
      for (int index = 0; index < dataTable.Rows.Count; ++index)
        dictionary[(object) Convert.ToInt32(dataTable.Rows[index]["F_ATTRIBUTE_ID"])] = (object) null;
      dataTable.Dispose();
    }
    int[] attributesForDbObjectType = new int[dictionary.Count];
    dictionary.Keys.CopyTo((Array) attributesForDbObjectType, 0);
    return attributesForDbObjectType;
  }

  /// <summary>Получить информацию о строке кода</summary>
  /// <param name="sf">StackFrame</param>
  /// <returns></returns>
  public static string GetCodeLine(StackFrame sf)
  {
    return sf != null ? $"{sf.GetFileName()}: {sf.GetMethod()}: {sf.GetFileLineNumber()}" : "";
  }

  /// <summary>Статический экземпляр плагина</summary>
  public static DocumentEditorPlugin Instance
  {
    get
    {
      if (DocumentEditorPlugin.instance == null)
      {
        DocumentEditorPlugin.instance = new DocumentEditorPlugin();
        DocumentEditorPlugin.InitDocumentPlugin();
      }
      return DocumentEditorPlugin.instance;
    }
  }

  /// <summary>Конструктор плагина</summary>
  public DocumentEditorPlugin()
  {
    if (DocumentEditorPlugin.instance != null)
      return;
    DocumentEditorPlugin.instance = this;
  }

  /// <summary>Загрузить создатели элементов страницы</summary>
  /// <param name="assembly">Сборка</param>
  public void LoadPageElementCreators(Assembly assembly)
  {
    System.Type[] types = assembly.GetTypes();
    System.Type c = typeof (PageElementCreator);
    List<System.Type> typeList1 = new List<System.Type>();
    foreach (System.Type type in types)
    {
      if (type.IsSubclassOf(c) && !type.IsAbstract)
        typeList1.Add(type);
    }
    List<System.Type> typeList2 = new List<System.Type>();
    if (typeList1.Contains(typeof (TextBoxCreator)))
      typeList2.Add(typeof (TextBoxCreator));
    if (typeList1.Contains(typeof (LabelCreator)))
      typeList2.Add(typeof (LabelCreator));
    if (typeList1.Contains(typeof (TableCreator)))
      typeList2.Add(typeof (TableCreator));
    if (typeList1.Contains(typeof (PolylineCreator)))
      typeList2.Add(typeof (PolylineCreator));
    if (typeList1.Contains(typeof (ContainerCreator)))
      typeList2.Add(typeof (ContainerCreator));
    foreach (System.Type type in typeList1)
    {
      if (type.Name != "TextBoxCreator" && type.Name != "LabelCreator" && type.Name != "TableCreator" && type.Name != "PolylineCreator" && type.Name != "ContainerCreator")
        typeList2.Add(type);
    }
    foreach (System.Type type in typeList2)
      this.AddPageElementCreator((PageElementCreator) Activator.CreateInstance(type));
  }

  public Intermech.Bars.ToolBar CreatePageElementsToolBar()
  {
    if (this.elementCreators == null || this.CommandManager == null)
      return (Intermech.Bars.ToolBar) null;
    Intermech.Bars.ToolBar toolBar = new Intermech.Bars.ToolBar();
    toolBar.Visible = true;
    toolBar.Guid = new Guid("6cb8f8f2-0dd1-4f8a-b642-ece847e92228");
    toolBar.Text = LocalizationHolder.rm.GetString("Document.Client_94");
    toolBar.ImageList = DocumentEditorPlugin.imageList;
    toolBar.DockLine = 0;
    toolBar.DockOffset = 0;
    toolBar.Tearable = false;
    Intermech.Bars.ToolBar pageElementsToolBar = toolBar;
    ButtonItem buttonItem1 = new ButtonItem();
    buttonItem1.CommandName = "SelectPageElement";
    buttonItem1.Text = LocalizationHolder.rm.GetString("Document.Client_95");
    buttonItem1.ToolTipText = LocalizationHolder.rm.GetString("Document.Client_96");
    this.selectElementButton = buttonItem1;
    pageElementsToolBar.Items.Add((ToolbarItemBase) this.selectElementButton);
    List<ButtonItemBase> buttonItemBaseList = new List<ButtonItemBase>();
    if (this.selectElementMenuItem != null)
      buttonItemBaseList.Add((ButtonItemBase) this.selectElementMenuItem);
    buttonItemBaseList.Add((ButtonItemBase) this.selectElementButton);
    this.selectElementCommand = this.CommandManager.Add(buttonItemBaseList.ToArray());
    Stream manifestResourceStream = typeof (ImDocument).Assembly.GetManifestResourceStream("Intermech.Document.Model.Resources.SelectArrow.bmp");
    if (manifestResourceStream != null)
    {
      Bitmap bitmap = new Bitmap(manifestResourceStream);
      bitmap.MakeTransparent();
      DocumentEditorPlugin.imageList.Images.Add((Image) bitmap);
      this.selectElementCommand.ImageIndex = DocumentEditorPlugin.imageList.Images.Count - 1;
      this.selectElementButton.ShowText = false;
    }
    else
      this.selectElementButton.ShowText = true;
    this.selectElementCommand.Enabled = true;
    this.selectElementCommand.Checked = true;
    foreach (PageElementCreator elementCreator in this.elementCreators)
    {
      ButtonItem buttonItem2 = new ButtonItem();
      buttonItem2.CommandName = elementCreator.Name;
      if (elementCreator.Image == null)
        buttonItem2.ShowText = true;
      pageElementsToolBar.Items.Add((ToolbarItemBase) buttonItem2);
      ICommandState commandState = this.CommandManager.Add((ButtonItemBase) buttonItem2);
      commandState.Text = elementCreator.Name;
      commandState.Enabled = true;
      if (elementCreator.Image != null)
      {
        DocumentEditorPlugin.imageList.Images.Add(elementCreator.Image);
        commandState.ImageIndex = DocumentEditorPlugin.imageList.Images.Count - 1;
      }
      commandState.ToolTipText = elementCreator.Name;
    }
    return pageElementsToolBar;
  }

  public void AddPageElementCreator(PageElementCreator elementCreator)
  {
    if (elementCreator == null)
      throw new ArgumentNullException(nameof (elementCreator));
    MenuButtonItem menuButtonItem = new MenuButtonItem();
    menuButtonItem.CommandName = elementCreator.Name;
    if (this.pageElementsMenu != null)
      this.pageElementsMenu.Items.Add((ToolbarItemBase) menuButtonItem);
    if (this.CommandManager == null)
      return;
    ICommandState commandState = this.CommandManager.Add((ButtonItemBase) menuButtonItem);
    commandState.Text = elementCreator.Name;
    commandState.Enabled = true;
    if (elementCreator.Image != null)
    {
      DocumentEditorPlugin.imageList.Images.Add(elementCreator.Image);
      commandState.ImageIndex = DocumentEditorPlugin.imageList.Images.Count - 1;
    }
    commandState.ToolTipText = elementCreator.Name;
    this.elementCreators.Add((object) elementCreator);
    this.elementCreatorCommands.Add((object) commandState);
  }

  internal static IClientMetadataCache MDCache
  {
    get
    {
      if (DocumentEditorPlugin._metadataCache == null)
        DocumentEditorPlugin._metadataCache = ServicesManager.GetService<IClientMetadataCache>();
      return DocumentEditorPlugin._metadataCache;
    }
  }

  /// <summary>Служба для вызова методов из других потоков</summary>
  public static IInvokeService InvokeService
  {
    [DebuggerStepThrough] get
    {
      if (DocumentEditorPlugin._iInvokeService == null)
        DocumentEditorPlugin._iInvokeService = (IInvokeService) ServicesManager.GetService(typeof (IInvokeService));
      return DocumentEditorPlugin._iInvokeService;
    }
  }

  /// <summary> Сервис событий </summary>
  public static INotificationService NotificationService
  {
    [DebuggerStepThrough] get
    {
      if (DocumentEditorPlugin.notificationService == null)
        DocumentEditorPlugin.notificationService = (INotificationService) ServicesManager.GetService(typeof (INotificationService));
      return DocumentEditorPlugin.notificationService;
    }
  }

  /// <summary> Сервис работы с правилами подбора версий </summary>
  public static IFiltrationService IFiltrationService
  {
    [DebuggerStepThrough] get
    {
      if (DocumentEditorPlugin._iFiltrationService == null)
        DocumentEditorPlugin._iFiltrationService = (IFiltrationService) ServicesManager.GetService(typeof (IFiltrationService));
      return DocumentEditorPlugin._iFiltrationService;
    }
  }

  /// <summary> Сервис сравнения файлов объектов </summary>
  public static ICompareFilesService ICompareFilesService
  {
    [DebuggerStepThrough] get
    {
      if (DocumentEditorPlugin._iCompFilesService == null)
        DocumentEditorPlugin._iCompFilesService = (ICompareFilesService) ServicesManager.GetService(typeof (ICompareFilesService));
      return DocumentEditorPlugin._iCompFilesService;
    }
  }

  /// <summary>Взять объект системы на изменение</summary>
  /// <param name="dbObjectID">Идентификатор объекта системы</param>
  /// <returns>Идентификатор объекта системы взятого на изменение (рабочей копии)</returns>
  public static long CheckOutWithNotification(long dbObjectID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return DocumentEditorPlugin.CheckOutWithNotification(sessionKeeper.Session.GetObject(dbObjectID)).ObjectID;
  }

  /// <summary>Взять объект системы на изменение и послать сообщение в систему</summary>
  /// <param name="dbObject">Объект системы</param>
  /// <returns>Объект системы взятый на изменение (рабочая копия)</returns>
  public static IDBObject CheckOutWithNotification(IDBObject dbObject)
  {
    INotificationService service = (INotificationService) ServicesManager.GetService(typeof (INotificationService));
    long objectId = dbObject.ObjectID;
    dbObject = dbObject.CheckOut();
    if (service != null && objectId != dbObject.ObjectID)
      service.FireEvent((object) null, (NotificationEventArgs) new DBObjectsCheckOutEventArgs("ObjectsCheckedOut", (IList<long>) new List<long>(1)
      {
        objectId
      }, (IList<long>) new List<long>(1)
      {
        dbObject.ObjectID
      }));
    return dbObject;
  }

  /// <summary>Является ли данный тип объекта типом "Шаблон документа"</summary>
  /// <param name="objType">Тип объекта</param>
  /// <returns>Тип объекта является типом "Шаблон документа"</returns>
  public static bool IsDocumentTemplateType(int objType)
  {
    return MetaDataHelper.IsObjectTypeChildOf(objType, DocIDCache.ObjType_ImDocTemplate);
  }

  /// <summary>Является ли данный тип объекта типом "Документ Интермех"</summary>
  /// <param name="objType">Тип объекта</param>
  /// <returns>Тип объекта является типом "Документ Интермех"</returns>
  public static bool IsDocumentType(int objType)
  {
    return MetaDataHelper.IsObjectTypeChildOf(objType, DocIDCache.ObjType_ImDocument) || MetaDataHelper.IsObjectTypeChildOf(objType, DocIDCache.ObjType_ImDocTemplate) || MetaDataHelper.IsObjectTypeChildOf(objType, DocIDCache.ObjType_Specification) || MetaDataHelper.IsObjectTypeChildOf(objType, DocIDCache.ObjType_ECO);
  }

  public static string GenerateDefaultFileNameForDB(ImDocumentData document)
  {
    if (document == null)
      throw new ArgumentNullException(nameof (document));
    return DocumentEditorPlugin.GenerateDefaultFileNameForDB(document, document.DBObjectID, document.DBObjectCaption);
  }

  public static string GenerateDefaultFileNameForDB(
    ImDocumentData document,
    long documentObjectID,
    string documentObjectCaption)
  {
    if (document == null)
      throw new ArgumentNullException(nameof (document));
    string filename;
    if (ImDocumentEditorConfig.Instance.DefaultFileNameSource == DefaultFileNameSource.ObjectVersionID && documentObjectID.IsDefinedId())
    {
      filename = Math.Abs(documentObjectID).ToString();
    }
    else
    {
      filename = documentObjectCaption;
      if (string.IsNullOrWhiteSpace(filename))
        filename = document.GetDefautCaption();
      if (string.IsNullOrWhiteSpace(filename))
        filename = document.FileName;
      if (string.IsNullOrWhiteSpace(filename))
        filename = nameof (document);
    }
    return FileNameHelper.ReplaceInvalidFileNameChars(filename);
  }

  /// <summary>Создать новый объект БД и сохранить в него документ</summary>
  /// <param name="document">Документ</param>
  /// <param name="documentObjectType">Тип документа. Если -1, то будет вызван диалог выбора типа</param>
  /// <param name="callDialogWithObjectParamsBeforeSave">Вызывать ли карточку объекта при создании</param>
  /// <returns></returns>
  public static bool SaveDocumentInNewDBObject(
    ImDocument document,
    int documentObjectType = -1,
    bool callDialogWithObjectParamsBeforeSave = false)
  {
    int num = DocumentEditorPlugin.CreateNewDBObjectForDocument(document, documentObjectType, callDialogWithObjectParamsBeforeSave) ? 1 : 0;
    if (num == 0)
      return num != 0;
    DocumentEditorPlugin.SaveImDocumentObjectFile(document.DBObjectID, document, document.FileName, -1, true);
    return num != 0;
  }

  /// <summary>Создать новый объект БД для сохранения документа</summary>
  /// <param name="document">Документ</param>
  /// <param name="documentObjectType">Тип документа. Если -1, то будет вызван диалог выбора типа</param>
  /// <param name="callDialogWithObjectParamsBeforeSave">Вызывать ли карточку объекта при создании</param>
  /// <returns></returns>
  internal static bool CreateNewDBObjectForDocument(
    ImDocument document,
    int documentObjectType,
    bool callDialogWithObjectParamsBeforeSave)
  {
    if (documentObjectType == -1 && !DocumentEditorPlugin.SelectDocumentDBObjectType(out documentObjectType, out string _))
      return false;
    if (callDialogWithObjectParamsBeforeSave || string.IsNullOrEmpty(document.DocumentName) && string.IsNullOrEmpty(document.Designation))
      DocumentEditorPlugin.ShowObjectCreatorDialog(document, documentObjectType);
    else
      DocumentEditorPlugin.CreateDBObjectForDocumentType(document, documentObjectType);
    return document.DBObjectID.IsDefinedId();
  }

  private static void CreateDBObjectForDocumentType(ImDocument document, int documentObjectType)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObjectCollection(documentObjectType).Create();
      dbObject.SetAttributesValues(DBObjectHelper.Filter(dbObject, new AttributeValues[2]
      {
        new AttributeValues(DocIDCache.Attr_Designation, (object) document.Designation),
        new AttributeValues(DocIDCache.Attr_Name, (object) document.DocumentName)
      }), false, true);
      if (dbObject.IsCreationMode)
        dbObject.CommitCreation(true, true);
      DocumentEditorPlugin.Instance.SetDocumentDBObject((ImDocumentData) document, dbObject);
    }
  }

  private static void ShowObjectCreatorDialog(ImDocument document, int documentObjectType)
  {
    IObjectCreatorService service = ServicesManager.GetService<IObjectCreatorService>(false);
    if (service == null)
      return;
    AfterDraftCreatedEventHandler createdEventHandler = (AfterDraftCreatedEventHandler) ((sender, e) =>
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(e.ObjectID, false);
        if (dbObject == null)
          return;
        List<AttributeValues> list = new List<AttributeValues>();
        if (!string.IsNullOrEmpty(document.DocumentName) && AttributeCacheHelper.IsEnabledObjectTypeAttribute(DocIDCache.Attr_Name, documentObjectType))
          list.Add(new AttributeValues(DocIDCache.Attr_Name, (object) document.DocumentName));
        if (!string.IsNullOrEmpty(document.Designation) && AttributeCacheHelper.IsEnabledObjectTypeAttribute(DocIDCache.Attr_Designation, documentObjectType))
          list.Add(new AttributeValues(DocIDCache.Attr_Designation, (object) document.Designation));
        if (list.IsEmpty<AttributeValues>())
          return;
        dbObject.SetAttributesValues(list.ToArray());
      }
    });
    service.AfterDraftCreatedEvent += createdEventHandler;
    try
    {
      long objectByTypeDialog = service.CreateObjectByTypeDialog(documentObjectType);
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(objectByTypeDialog, false);
        if (dbObject == null)
          return;
        DocumentEditorPlugin.Instance.SetDocumentDBObject((ImDocumentData) document, dbObject);
      }
    }
    finally
    {
      service.AfterDraftCreatedEvent -= createdEventHandler;
    }
  }

  internal static bool SelectDocumentDBObjectType(
    out int documentObjectTypeId,
    out string documentObjectTypeName)
  {
    SelectorForm selectorForm = new SelectorForm(typeof (ObjectTypesFolder), LocalizationHolder.rm.GetString("Document.Client_140"), typeof (ObjectTypeFolder), false);
    selectorForm.ExpandLevelsOnLoad = 2;
    selectorForm.SelectorFilter = (ISelectorFilter) new TypeSelectorFilter(new int[1]
    {
      DocIDCache.ObjType_Document
    }, true, true);
    selectorForm.NodeSelectorFilter = (INodeSelectorFilter) new NodeSelectorFilter();
    if (selectorForm.ShowDialog() != DialogResult.OK || selectorForm.IDList.Count == 0)
    {
      documentObjectTypeId = -1;
      documentObjectTypeName = string.Empty;
      return false;
    }
    documentObjectTypeId = (int) selectorForm.IDList[0];
    documentObjectTypeName = (string) selectorForm.NameList[0];
    return true;
  }

  /// <summary>Сохранить документ в файловый атрибут объекта</summary>
  /// <param name="docObjectID">Идентификатор объекта</param>
  /// <param name="document">Документ</param>
  /// <param name="fileName">Имя файла</param>
  /// <param name="fileIndex">Индекс файлового атрибута объекта</param>
  /// <param name="isNewDocument">Новый документ. Используется для игнорирования флага SaveModificationDate</param>
  public static void SaveImDocumentObjectFile(
    long docObjectID,
    ImDocument document,
    string fileName,
    int fileIndex,
    bool isNewDocument)
  {
    if (document.DocumentControl != null)
      document.DocumentControl.EditorValidating();
    if (document.Reference != null)
      document.Reference.UpdateLink(true, true);
    DocumentEditorPlugin.SaveDBObjectsAttributesFromReference((ImDocumentData) document);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      DocumentEditorPluginBase.SaveImDocumentObjectFile(sessionKeeper.Session.GetObject(docObjectID), (ImDocumentData) document, fileName, fileIndex, isNewDocument);
    ServicesManager.GetService<INotificationService>(false)?.FireEvent((object) document, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsChanged", docObjectID));
  }

  /// <summary>Загрузить документ из файлового атрибута объекта</summary>
  /// <param name="docObjectID">Идентификатор объекта</param>
  /// <param name="fileIndex">Индекс файлового атрибута объекта. -1 - если неизвестно в каком файле хранится документ.
  /// В этом случае будет выбран первый документ нового формата, или, если его нет, то старого формата.</param>
  /// <param name="createIfNotFound">Создать пустой документ, если нет файла</param>
  /// <param name="updateDoc">Обновить документ после загрузки</param>
  /// <param name="loadInThread">Загружать в фоновом потоке</param>
  /// <returns>Документ. Если файловый атрибут пустой, то создается пустой документ!</returns>
  public static ImDocument LoadDocumentFromDBObject(
    long docObjectID,
    int fileIndex = -1,
    bool createIfNotFound = false,
    bool updateDoc = true,
    bool loadInThread = false)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return DocumentEditorPlugin.LoadDocumentFromDBObject(sessionKeeper.Session.GetObjectActual(docObjectID, true), fileIndex, Guid.Empty, createIfNotFound, false, updateDoc, loadInThread) as ImDocument;
  }

  /// <summary>Загрузить документ формата IMDX из файлового атрибута объекта. Не поддерживает старые форматы!</summary>
  /// <param name="docObjectID">Идентификатор объекта</param>
  /// <param name="fileIndex">Индекс файлового атрибута объекта. -1 - если неизвестно в каком файле хранится документ.
  /// В этом случае будет выбран первый документ нового формата, или, если его нет, то старого формата.</param>
  /// <param name="createIfNotFound">Создать пустой документ, если нет файла</param>
  /// <param name="updateDoc">Обновить документ после загрузки</param>
  /// <param name="loadInThread">Загружать в фоновом потоке</param>
  /// <returns>Документ. Если файловый атрибут пустой, то создается пустой документ!</returns>
  public static ImDocument LoadIPSImDocumentFromDBObject(
    long docObjectID,
    int fileIndex = -1,
    bool createIfNotFound = false,
    bool updateDoc = true,
    bool loadInThread = false)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return DocumentEditorPlugin.LoadDocumentFromDBObject(sessionKeeper.Session.GetObjectActual(docObjectID, true), fileIndex, Guid.Empty, createIfNotFound, false, updateDoc, loadInThread, true) as ImDocument;
  }

  /// <summary>Загрузить документ из файлового атрибута объекта. Если файловый атрибут пустой, то создается пустой документ!</summary>
  /// <param name="docObjectGuid">Глобальный идентификатор версии объекта</param>
  /// <param name="fileIndex">Индекс файлового атрибута объекта. -1 - если неизвестно в каком файле хранится документ.
  /// В этом случае будет выбран первый документ нового формата, или, если его нет, то старого формата.</param>
  /// <param name="createIfNotFound">Создать пустой документ, если нет файла</param>
  /// <param name="updateDoc">Обновить документ после загрузки</param>
  /// <param name="loadInThread">Загружать в фоновом потоке</param>
  /// <returns>Документ. Если файловый атрибут пустой, то создается пустой документ!</returns>
  public static ImDocument LoadDocumentFromDBObject(
    Guid docObjectGuid,
    int fileIndex = -1,
    bool createIfNotFound = false,
    bool updateDoc = true,
    bool loadInThread = false)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return DocumentEditorPlugin.LoadDocumentFromDBObject(sessionKeeper.Session.GetObject(docObjectGuid, true), fileIndex, Guid.Empty, createIfNotFound, false, updateDoc, loadInThread) as ImDocument;
  }

  /// <summary>Метод для делегата в Document.DBCore</summary>
  /// <param name="docObject"></param>
  /// <param name="fileIndex"></param>
  /// <returns></returns>
  private static ImDocumentData LoadDocumentFromDBObjectCore(
    IDBObject docObject,
    int fileIndex = -1,
    bool failIfNotFound = false)
  {
    return DocumentEditorPlugin.LoadDocumentFromDBObject(docObject, fileIndex, Guid.Empty, false, failIfNotFound, true, false) as ImDocumentData;
  }

  /// <summary>Загрузить документ из файлового атрибута объекта. Если файловый атрибут пустой, то создается пустой документ!</summary>
  /// <param name="docObject">Объект</param>
  /// <param name="fileIndex">Индекс файлового атрибута объекта</param>
  /// <param name="createIfNotFound">Создать пустой документ, если нет файла</param>
  /// <param name="updateDoc">Обновить документ после загрузки</param>
  /// <param name="loadInThread">Загружать документ в фоновом процессе</param>
  /// <returns>Документ. Если файловый атрибут пустой, то создается пустой документ!</returns>
  public static ImDocument LoadDocumentFromDBObject(
    IDBObject docObject,
    int fileIndex = -1,
    bool createIfNotFound = false,
    bool updateDoc = true,
    bool loadInThread = false)
  {
    return DocumentEditorPlugin.LoadDocumentFromDBObject(docObject, fileIndex, Guid.Empty, createIfNotFound, false, updateDoc, loadInThread) as ImDocument;
  }

  /// <summary>Загрузить документ из файлового атрибута объекта.</summary>
  /// <param name="docObject">Объект</param>
  /// <param name="fileIndex">Индекс файлового атрибута объекта</param>
  /// <param name="documentComplectGuid">Guid комплекта документов</param>
  /// <param name="createIfNotFound">Создать пустой документ, если нет файла</param>
  /// <param name="failIfUnknownFormat">Генерировать исключение, если формат документа не известен</param>
  /// <param name="updateDoc">Обновить документ после загрузки</param>
  /// <param name="loadInThread">Загружать в фоновом потоке</param>
  /// <param name="imdxForIpsOnly">Игнорировать файлы старого формата и внешние документы, искать только IMDX для IPS</param>
  /// <returns>Документ. Если файловый атрибут пустой, то создается пустой документ!</returns>
  public static DocumentTreeNode LoadDocumentFromDBObject(
    IDBObject docObject,
    int fileIndex,
    Guid documentComplectGuid,
    bool createIfNotFound,
    bool failIfUnknownFormat,
    bool updateDoc,
    bool loadInThread,
    bool imdxForIpsOnly = false)
  {
    return DocumentEditorPlugin.LoadDocumentFromDBObject(docObject, -1, fileIndex, documentComplectGuid, createIfNotFound, failIfUnknownFormat, updateDoc, loadInThread, imdxForIpsOnly);
  }

  /// <summary>Загрузить документ из файлового атрибута объекта.</summary>
  /// <param name="docObject">Объект</param>
  /// <param name="fileAttributeID">Идентификатор файлового атрибута объекта</param>
  /// <param name="fileIndex">Индекс файлового атрибута объекта</param>
  /// <param name="documentComplectGuid">Guid комплекта документов</param>
  /// <param name="createIfNotFound">Создать пустой документ, если нет файла</param>
  /// <param name="failIfUnknownFormat">Генерировать исключение, если формат документа не известен</param>
  /// <param name="updateDoc">Обновить документ после загрузки</param>
  /// <param name="loadInThread">Загружать в фоновом потоке</param>
  /// <param name="imdxForIpsOnly">Игнорировать файлы старого формата и внешние документы, искать только IMDX для IPS</param>
  /// <returns>Документ. Если файловый атрибут пустой, то создается пустой документ!</returns>
  public static DocumentTreeNode LoadDocumentFromDBObject(
    IDBObject docObject,
    int fileAttributeID,
    int fileIndex,
    Guid documentComplectGuid,
    bool createIfNotFound,
    bool failIfUnknownFormat,
    bool updateDoc,
    bool loadInThread,
    bool imdxForIpsOnly = false)
  {
    if (docObject == null)
      throw new ArgumentNullException(nameof (docObject));
    string objDesignation = "";
    IDBAttribute attributeById1 = docObject.GetAttributeByID(DocIDCache.Attr_Designation);
    if (attributeById1 != null)
      objDesignation = attributeById1.Description;
    string objName = "";
    IDBAttribute attributeById2 = docObject.GetAttributeByID(DocIDCache.Attr_Name);
    if (attributeById2 != null)
      objName = attributeById2.Description;
    return DocumentEditorPlugin.LoadDocumentFromDBObject(docObject, objDesignation, objName, fileAttributeID, fileIndex, documentComplectGuid, createIfNotFound, !createIfNotFound, failIfUnknownFormat, updateDoc, loadInThread, imdxForIpsOnly);
  }

  /// <summary>Загрузить документ из файлового атрибута объекта</summary>
  /// <param name="docObject">Объект</param>
  /// <param name="objGuid">Глобальный идентификатор версии документа</param>
  /// <param name="objID">Идентификатор версии</param>
  /// <param name="objType">Тип объекта</param>
  /// <param name="objDesignation">Обозначение</param>
  /// <param name="objName">Наименование</param>
  /// <param name="objCaption">Заголовок</param>
  /// <param name="fileIndex">Индекс файлового атрибута объекта</param>
  /// <param name="documentComplectGuid">Guid комплекта документов</param>
  /// <param name="createIfNotFound">Создать пустой документ, если нет файла</param>
  /// <param name="failIfEmptyFile"></param>
  /// <param name="failIfUnknownFormat"></param>
  /// <param name="updateDoc">Обновить документ после загрузки</param>
  /// <param name="loadInThread">Загружать в фоновом потоке</param>
  /// <param name="imdxForIpsOnly">Игнорировать файлы старого формата и внешние документы, искать только IMDX для IPS</param>
  /// <returns>Документ. Если файловый атрибут пустой, то создается пустой документ!</returns>
  public static DocumentTreeNode LoadDocumentFromDBObject(
    IDBObject docObject,
    string objDesignation,
    string objName,
    int fileIndex,
    Guid documentComplectGuid,
    bool createIfNotFound,
    bool failIfEmptyFile,
    bool failIfUnknownFormat,
    bool updateDoc,
    bool loadInThread,
    bool imdxForIpsOnly = false)
  {
    return DocumentEditorPlugin.LoadDocumentFromDBObject(docObject, objDesignation, objName, -1, fileIndex, documentComplectGuid, createIfNotFound, failIfEmptyFile, failIfUnknownFormat, updateDoc, loadInThread, imdxForIpsOnly);
  }

  /// <summary>Загрузить документ из файлового атрибута объекта</summary>
  /// <param name="docObject">Объект</param>
  /// <param name="objGuid">Глобальный идентификатор версии документа</param>
  /// <param name="objID">Идентификатор версии</param>
  /// <param name="objType">Тип объекта</param>
  /// <param name="objDesignation">Обозначение</param>
  /// <param name="objName">Наименование</param>
  /// <param name="objCaption">Заголовок</param>
  /// <param name="fileAttributeID">Идентификатор файлового атрибута объекта</param>
  /// <param name="fileIndex">Индекс файлового атрибута объекта</param>
  /// <param name="documentComplectGuid">Guid комплекта документов</param>
  /// <param name="createIfNotFound">Создать пустой документ, если нет файла</param>
  /// <param name="failIfEmptyFile"></param>
  /// <param name="failIfUnknownFormat"></param>
  /// <param name="updateDoc">Обновить документ после загрузки</param>
  /// <param name="loadInThread">Загружать в фоновом потоке</param>
  /// <param name="imdxForIpsOnly">Игнорировать файлы старого формата и внешние документы, искать только IMDX для IPS</param>
  /// <returns>Документ. Если файловый атрибут пустой, то создается пустой документ!</returns>
  public static DocumentTreeNode LoadDocumentFromDBObject(
    IDBObject docObject,
    string objDesignation,
    string objName,
    int fileAttributeID,
    int fileIndex,
    Guid documentComplectGuid,
    bool createIfNotFound,
    bool failIfEmptyFile,
    bool failIfUnknownFormat,
    bool updateDoc,
    bool loadInThread,
    bool imdxForIpsOnly = false)
  {
    if (docObject == null)
      throw new ArgumentNullException(nameof (docObject));
    DocumentEditorPlugin.ThreadParams threadParams = new DocumentEditorPlugin.ThreadParams()
    {
      DBObject = docObject,
      ObjectID = docObject.ObjectID,
      ObjectCaption = docObject.Caption,
      ObjectGuid = docObject.ObjectGUID,
      ObjectType = docObject.ObjectType,
      LoadInThread = loadInThread,
      FailIfUnknownFormat = failIfUnknownFormat,
      FailIfEmptyFile = failIfEmptyFile,
      UpdateDoc = updateDoc
    };
    threadParams.IsTemplate = DocumentEditorPlugin.IsDocumentTemplateType(threadParams.ObjectType);
    threadParams.IsFormulaLib = !threadParams.IsTemplate && MetaDataHelper.IsObjectTypeChildOf(threadParams.ObjectType, DocIDCache.ObjType_FormulaLib);
    threadParams.DocEnter = false;
    threadParams.FileIndex = fileIndex;
    int fileAttributeIndex = -1;
    int fileAttributeID1 = -1;
    try
    {
      try
      {
        bool flag1 = false;
        bool flag2 = false;
        IDBAttribute documentFileAttribute = DocumentEditorPluginBase.FindDocumentFileAttribute(docObject, fileAttributeID);
        if (documentFileAttribute != null)
        {
          if (threadParams.FileIndex == -1)
            threadParams.FileIndex = imdxForIpsOnly ? DocumentEditorPluginBase.FindImDocumentInAttribute(documentFileAttribute) : DocumentEditorPluginBase.FindAnyImDocumentInAttribute(documentFileAttribute);
          if (!imdxForIpsOnly)
          {
            flag2 = DocumentEditorPluginBase.FindExternalImDocumentInAttribute(documentFileAttribute) != -1;
            if (flag2 && docObject.GetAttributeByID(DocIDCache.Attr_SourceLink) == null)
              flag2 = false;
          }
        }
        threadParams.FileName = (string) null;
        long num = 0;
        DateTime? nullable = new DateTime?();
        if (documentFileAttribute != null && threadParams.FileIndex != -1 && threadParams.FileIndex < documentFileAttribute.Values.Length)
        {
          flag1 = true;
          fileAttributeIndex = threadParams.FileIndex;
          documentFileAttribute.Index = threadParams.FileIndex;
          threadParams.FileAttribute = documentFileAttribute;
          threadParams.AttributeId = documentFileAttribute.AttributeID;
          fileAttributeID1 = documentFileAttribute.AttributeID;
          threadParams.FileName = documentFileAttribute.Descriptions[threadParams.FileIndex];
          if (documentFileAttribute is IBlobReader blobReader)
          {
            BlobInformation blobInformation = blobReader.OpenBlob(-1);
            nullable = new DateTime?(blobInformation.ModifyDate);
            num = blobInformation.RealFileSize;
            blobReader.CloseBlob();
          }
          if (!ImDocumentData.IsImDocumentExtension(ImDocumentData.GetFileExtensionWithoutDot(threadParams.FileName)))
          {
            threadParams.LoadInThread = loadInThread = false;
            fileAttributeIndex = -1;
            fileAttributeID1 = -1;
          }
        }
        else
        {
          threadParams.LoadInThread = loadInThread = false;
          threadParams.FileIndex = -1;
        }
        if (flag2)
        {
          ExternalDocumentCreator externalDocumentCreator = new ExternalDocumentCreator();
          threadParams.RootNode = (DocumentTreeNode) externalDocumentCreator.CreateDocument(threadParams.ObjectID, true);
          flag1 = false;
        }
        if (flag1)
        {
          threadParams.readArgs = new XmlReadArgs()
          {
            FileName = threadParams.FileName,
            FileSize = num,
            FileModifyDate = nullable,
            IsTemplate = threadParams.IsTemplate,
            IsFormulaLib = threadParams.IsFormulaLib
          };
          threadParams.readArgs.DocumentDBReference = (ReferenceBase) new ReferenceToDBObject((DocumentTreeNode) null, RefToDBObjectType.rtSelectedObject, (DBObjectInfoBase) new DBObjectInfo(threadParams.ObjectGuid, threadParams.ObjectID, threadParams.ObjectType, docObject.Caption), false);
          if (loadInThread)
          {
            threadParams.readArgs.ThreadIsExternal = true;
            Monitor.Enter(threadParams.readArgs.LockedObjectByLoadThread = (object) threadParams.readArgs);
            try
            {
              threadParams.readArgs.LoadFromStreamThread = new Thread(new ParameterizedThreadStart(DocumentEditorPlugin.LoadDocumentFromInThread), 2000000);
              threadParams.readArgs.LoadFromStreamThread.SetApartmentState(ApartmentState.STA);
              threadParams.readArgs.LoadFromStreamThread.Name = "LoadImDocumentFromStreamThread";
              try
              {
                threadParams.readArgs.LoadFromStreamThread.Start((object) threadParams);
              }
              catch
              {
                if (threadParams.readArgs.LoadFromStreamThread.ThreadState != System.Threading.ThreadState.Running)
                  threadParams.readArgs.LoadFromStreamThread.Start((object) threadParams);
              }
              Monitor.Wait(threadParams.readArgs.LockedObjectByLoadThread, 12000);
              threadParams.RootNode = threadParams.readArgs.RootDocNode as DocumentTreeNode;
              if (threadParams.Exception != null)
                throw threadParams.Exception;
            }
            finally
            {
              Monitor.Exit(threadParams.readArgs.LockedObjectByLoadThread);
            }
          }
          else
            DocumentEditorPlugin.LoadDocumentFromInThread((object) threadParams);
        }
      }
      finally
      {
        if (threadParams.RootNode == null & createIfNotFound)
        {
          if (!threadParams.IsTemplate && !threadParams.IsFormulaLib)
          {
            long fromImDocSettings = DocumentEditorPlugin.GetDocumentTemplateIDFromIMDocSettings(docObject.ObjectType);
            switch (fromImDocSettings)
            {
              case -1:
              case 0:
                break;
              default:
                ImDocument template = DocumentEditorPlugin.LoadDocumentFromDBObject(fromImDocSettings);
                if (template != null)
                {
                  threadParams.RootNode = (DocumentTreeNode) new ImDocument(template, true, true);
                  break;
                }
                break;
            }
          }
          if (threadParams.RootNode == null)
            threadParams.RootNode = (DocumentTreeNode) new ImDocument(!threadParams.IsTemplate && !threadParams.IsFormulaLib);
          if (threadParams.Document != null)
          {
            if (threadParams.IsTemplate)
              threadParams.Document.SetIsTemplate(threadParams.IsTemplate);
            if (threadParams.IsFormulaLib)
              threadParams.Document.AssignIsFormulaLib(true);
            if (threadParams.Document.Nodes.Count == 0)
            {
              IDBAttribute attributeById = docObject.GetAttributeByID(DocIDCache.Attr_Format);
              string pageFormat = "A4";
              if (attributeById != null && attributeById.Value != null && !(attributeById.Value is DBNull))
                pageFormat = attributeById.Value.ToString();
              SizeF sizeF = new SizeF(210f, 297f);
              if (pageFormat != null && pageFormat != "")
                sizeF = PageData.GetSizeForPageFormat(pageFormat);
              PageData pageData = threadParams.Document.NewPage();
              if (pageData != null)
                pageData.Size = sizeF;
            }
          }
        }
        if (threadParams.Document != null)
        {
          DocumentEditorPlugin.Instance.SetDocumentDBObject((ImDocumentData) threadParams.Document, threadParams.ObjectGuid, threadParams.ObjectID, threadParams.ObjectType, docObject.Caption, fileAttributeID1, fileAttributeIndex);
          threadParams.Document.DocumentName = objName;
          threadParams.Document.Designation = objDesignation;
          threadParams.Document.SetAttributeValue(DocumentTreeNode.AttributeName_VersionId, Math.Abs(docObject.ObjectID).ToString());
          threadParams.Document.DocumentComplectObjectGuid = documentComplectGuid;
          threadParams.Document.DBObjectModifyMode = new ObjectModifyModes?(docObject.ObjectModifyMode);
          if (updateDoc)
            DocumentEditorPlugin.Instance.UpdateDocumentLinks((DocumentTreeNode) threadParams.Document, docObject.Session, true, true, false, false, !loadInThread);
          if (!threadParams.ModifiedByPatch)
            threadParams.Document.Modified = false;
          if (threadParams.DocEnter)
            threadParams.DocEnter = false;
        }
        if (threadParams.Complect != null)
          threadParams.Complect.Name = docObject.Caption;
      }
    }
    catch (Exception ex)
    {
      throw new ImDocumentException($"При загрузке файла документа из атрибута '{threadParams.AttributeName}': [{threadParams.FileIndex}] '{threadParams.FileName}' " + $"объекта [{threadParams.ObjectID}] '{threadParams.ObjectCaption}' возникла ошибка!" + Environment.NewLine + ex.Message, ex);
    }
    return threadParams.RootNode;
  }

  /// <summary>Метод загрузки документа или комплекта из XML для фонового потока</summary>
  /// <param name="args">Аргументы загрузки из XML. Должны быть типа XmlReadArgs</param>
  private static void LoadDocumentFromInThread(object args)
  {
    DocumentEditorPlugin.ThreadParams tP = (DocumentEditorPlugin.ThreadParams) args;
    SessionKeeper sessionKeeper = (SessionKeeper) null;
    try
    {
      if (args == null)
        throw new ArgumentNullException(nameof (args));
      if (tP.LoadInThread)
      {
        sessionKeeper = new SessionKeeper();
        tP.DBObject = sessionKeeper.Session.GetObject(tP.ObjectID, true);
        tP.FileAttribute = tP.DBObject.GetAttributeByID(tP.AttributeId);
        tP.FileAttribute.Index = tP.FileIndex;
      }
      BlobReaderStream blobReaderStream = new BlobReaderStream(tP.FileAttribute, 0, tP.FileAttribute.Session);
      DateTime modifyDate = blobReaderStream.BlobInformation.ModifyDate;
      if (blobReaderStream.Length != 0L)
      {
        tP.RootNode = ImDocument.LoadFromStream((Stream) blobReaderStream, (string) null, out DocumentFileType _, tP.UpdateDoc, tP.LoadInThread, tP.FailIfUnknownFormat, tP.readArgs);
        if (tP.Document != null)
        {
          tP.Document.SavedDateTime = new DateTime?(new DateTime(modifyDate.Year, modifyDate.Month, modifyDate.Day, modifyDate.Hour, modifyDate.Minute, 0));
          DocumentEditorPlugin.PreparePartlyLoadedDoc(tP);
          if (new CheckSumService().CanSetChecksum() && !tP.Document.IsTemplate)
            DocumentEditorPlugin.Instance.UpdateCheckSum((IUserSession) null, (ImDocumentData) tP.Document, tP.ObjectID, tP.AttributeId, tP.FileIndex, tP.LoadInThread);
        }
        else if (tP.Complect != null)
          DocumentEditorPlugin.PreparePartlyLoadedDoc(tP);
      }
      else
      {
        if (tP.LoadInThread && tP.readArgs.LockedObjectByLoadThread != null)
        {
          Monitor.Enter(tP.readArgs.LockedObjectByLoadThread);
          Monitor.Pulse(tP.readArgs.LockedObjectByLoadThread);
          Monitor.Exit(tP.readArgs.LockedObjectByLoadThread);
        }
        if (tP.FailIfEmptyFile)
          throw new Exception(LocalizationHolder.rm.GetString("Document.Client_164"));
      }
      if (tP.Document == null)
        return;
      bool modified = tP.Document.Modified;
      tP.Document.Modified = false;
      DocumentEditorPlugin.OnAfterLoadDocument((object) DocumentEditorPlugin.Instance, new AfterLoadDocumentEventHandlerArgs(tP.ObjectID, tP.ObjectGuid, tP.ObjectType, tP.Document));
      if (tP.Document.Modified)
        tP.ModifiedByPatch = true;
      else
        tP.Document.Modified = modified;
    }
    catch (Exception ex)
    {
      if (tP.LoadInThread)
      {
        if (tP.readArgs.LockedObjectByLoadThread != null)
        {
          tP.Exception = ex;
          Monitor.Enter(tP.readArgs.LockedObjectByLoadThread);
          Monitor.Pulse(tP.readArgs.LockedObjectByLoadThread);
          Monitor.Exit(tP.readArgs.LockedObjectByLoadThread);
        }
        ImDocumentException docException = new ImDocumentException($"При загрузке файла документа из атрибута '{tP.AttributeName}': [{tP.FileIndex}] '{tP.FileName}' " + $"объекта [{tP.ObjectID}] '{tP.ObjectCaption}' возникла ошибка!" + Environment.NewLine + ex.Message, ex);
        DocumentEditorPlugin.InvokeService.InvokeAction(-1, (Action) (() => ExceptionHelper.ExceptionService.ShowException((Exception) docException)));
      }
      else
        throw;
    }
    finally
    {
      sessionKeeper?.Dispose();
    }
  }

  /// <summary>Создать на основе заданного шаблона документа новый документ</summary>
  /// <param name="templateID">Идентификатор версии шаблона документа</param>
  /// <returns></returns>
  public static ImDocument CreateDocumentFromTemplate(long templateID)
  {
    return new ImDocument(DocumentEditorPlugin.LoadDocumentFromDBObject(templateID), true, true);
  }

  /// <summary>Создать на основе заданного шаблона документа новый документ</summary>
  /// <param name="templateGuid">Глобальный идентификатор версии шаблона документа</param>
  /// <returns></returns>
  public static ImDocument CreateDocumentFromTemplate(Guid templateGuid)
  {
    return new ImDocument(DocumentEditorPlugin.LoadDocumentFromDBObject(templateGuid), true, true);
  }

  protected override ReferenceToDBObjectCore CreateSetDocumentDBObjectReference(
    ImDocumentData document,
    DBObjectInfo info)
  {
    return (ReferenceToDBObjectCore) new ReferenceToDBObject((DocumentTreeNode) document, RefToDBObjectType.rtSelectedObject, (DBObjectInfoBase) info, false);
  }

  public static long GetDocumentTemplateIDFromIMDocSettings(int documentTypeID)
  {
    Guid fromImDocSettings = DocumentEditorPlugin.GetDocumentTemplateIDFromIMDocSettings(MetaDataHelper.GetObjectTypeGuid(documentTypeID));
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return sessionKeeper.Session.GetObjectInfo(fromImDocSettings).ObjectID;
  }

  /// <summary>Получить идентификатор шаблона привязанный к типу документа через настройки инструмента "Редактор документов"</summary>
  /// <returns>Идентификатор типа документа</returns>
  public static Guid GetDocumentTemplateIDFromIMDocSettings(Guid documentType)
  {
    Guid fromImDocSettings = Guid.Empty;
    if (!DocumentEditorPlugin.UseImDocEditorSettingsCache || DocumentEditorPlugin.imDocEditorSettingsCache == null)
      DocumentEditorPlugin.imDocEditorSettingsCache = DocumentEditorPlugin.GetDocumentEditorToolSettings();
    int objectTypeId = MetaDataHelper.GetObjectTypeID(documentType);
    IMDocObjectTypeSettings objectTypeSettings = DocumentEditorPlugin.imDocEditorSettingsCache[objectTypeId];
    if (objectTypeSettings != null)
      fromImDocSettings = objectTypeSettings.TemplateGuid;
    if (fromImDocSettings == Guid.Empty)
      fromImDocSettings = IMDocEditorToolSettings.GetDefaultVedomostTemplateGuid(documentType);
    if (!DocumentEditorPlugin.UseImDocEditorSettingsCache)
      DocumentEditorPlugin.imDocEditorSettingsCache = (IMDocEditorToolSettings) null;
    return fromImDocSettings;
  }

  /// <summary>
  /// Получить экземпляр настройки инструмента "Редактор документов"
  /// </summary>
  private static IMDocEditorToolSettings GetDocumentEditorToolSettings()
  {
    IMDocEditorToolSettings editorToolSettings = (IMDocEditorToolSettings) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IIntegratorServer service = ServiceUtils.GetService<IIntegratorServer>((object) sessionKeeper.Session, true);
      if (service.IsIntegratorExists(DocumentEditorIntegrator.IntegratorId))
      {
        XmlDocument data = new XmlDocument();
        data.LoadXml(service.GetIntegratorData(DocumentEditorIntegrator.IntegratorId));
        editorToolSettings = IMDocEditorToolSettingsCodec.Decode(data);
      }
    }
    return editorToolSettings;
  }

  /// <summary>Служебный метод, только для внутреннего пользования.
  /// Готовит документ к показу, пока он догружается в фоновом потоке</summary>
  /// <param name="readArgs">Параметры загрузки документа</param>
  /// <param name="updateDoc">Обновлять документ</param>
  /// <returns>Возвращает ссылку на загружаемый документ или комплект</returns>
  private static void PreparePartlyLoadedDoc(DocumentEditorPlugin.ThreadParams tP)
  {
    if (tP.LoadInThread)
      ImDocument.PreparePartlyLoadedDoc(tP.readArgs, tP.UpdateDoc);
    ImDocument document = tP.Document;
    if (document == null)
      return;
    if (document.BackThreadIsActive)
    {
      if (document.LoadFromStreamThread != null)
        document.LoadFromStreamThread.Priority = ThreadPriority.BelowNormal;
      tP.DocEnter = true;
    }
    if (tP.IsTemplate && !document.IsTemplate)
      document.SetIsTemplate(true);
    if (!tP.IsFormulaLib || document.IsFormulaLib)
      return;
    document.AssignIsFormulaLib(true);
  }

  protected override bool CanUpdateChecksum(ImDocumentData doc)
  {
    switch (doc)
    {
      case ImExternalDocument _:
        return false;
      case ImDocument imDocument:
        if (imDocument.DocumentControl != null && !imDocument.DocumentControl.DocumentViewMode.HasFlag((Enum) DocumentViewMode.ShowCRC))
          return false;
        break;
    }
    return true;
  }

  /// <summary>Найти первый атрибут с расширением старого формата AVS</summary>
  /// <param name="fileAttribute">Файловый атрибут</param>
  public static bool DBObjectHasOldSPFileOnly(IDBObject dbObject)
  {
    IDBAttribute attributeById = dbObject.GetAttributeByID(DocIDCache.Attr_File);
    return attributeById != null && DocumentEditorPlugin.FindImDocFileExtensionInAttribute(attributeById) == -1 && DocumentEditorPlugin.FindOldSPFileExtensionInAttribute(attributeById) != -1;
  }

  /// <summary>Найти первый атрибут с расширением старого формата AVS</summary>
  /// <param name="fileAttribute">Файловый атрибут</param>
  public static bool DBObjectHasOldImDocFileOnly(IDBObject dbObject)
  {
    IDBAttribute attributeById = dbObject.GetAttributeByID(DocIDCache.Attr_File);
    return attributeById != null && DocumentEditorPlugin.FindImDocFileExtensionInAttribute(attributeById) == -1 && DocumentEditorPlugin.FindOldFormatFileExtensionInAttribute(attributeById) != -1;
  }

  /// <summary>Найти первый атрибут с расширением старого формата AVS</summary>
  /// <param name="fileAttribute">Файловый атрибут</param>
  public static bool DBObjectHasOldFormatFileOnly(IDBObject dbObject)
  {
    IDBAttribute attributeById = dbObject.GetAttributeByID(DocIDCache.Attr_File);
    return attributeById != null && DocumentEditorPlugin.FindImDocFileExtensionInAttribute(attributeById) == -1 && DocumentEditorPlugin.FindOldFormatFileExtensionInAttribute(attributeById) != -1;
  }

  /// <summary>Найти первый атрибут с расширением старого формата AVS</summary>
  /// <param name="fileAttribute">Файловый атрибут</param>
  public static int FindOldSPFileExtensionInAttribute(IDBAttribute fileAttribute)
  {
    for (int extensionInAttribute = 0; extensionInAttribute < fileAttribute.Values.Length; ++extensionInAttribute)
    {
      if (ImDocumentData.IsOldAVSExtension(ImDocumentData.GetFileExtensionWithoutDot(fileAttribute.Descriptions[extensionInAttribute])))
        return extensionInAttribute;
    }
    return -1;
  }

  /// <summary>Найти первый атрибут с расширением старого формата AVS</summary>
  /// <param name="fileAttribute">Файловый атрибут</param>
  internal static int FindImDocFileExtensionInAttribute(IDBAttribute fileAttribute)
  {
    for (int extensionInAttribute = 0; extensionInAttribute < fileAttribute.Values.Length; ++extensionInAttribute)
    {
      if (ImDocumentData.IsImDocumentExtension(ImDocumentData.GetFileExtensionWithoutDot(fileAttribute.Descriptions[extensionInAttribute])))
        return extensionInAttribute;
    }
    return -1;
  }

  /// <summary>Найти первый атрибут с расширением старых форматов Редакторов документов или бланков</summary>
  /// <param name="fileAttribute">Файловый атрибут</param>
  internal static int FindOldFormatFileExtensionInAttribute(IDBAttribute fileAttribute)
  {
    for (int extensionInAttribute = 0; extensionInAttribute < fileAttribute.Values.Length; ++extensionInAttribute)
    {
      string extensionWithoutDot = ImDocumentData.GetFileExtensionWithoutDot(fileAttribute.Descriptions[extensionInAttribute]);
      if (ImDocumentData.IsOldImDocumentExtension(extensionWithoutDot) || ImDocumentData.IsOldBlankExtension(extensionWithoutDot) || ImDocumentData.IsOldAVSExtension(extensionWithoutDot))
        return extensionInAttribute;
    }
    return -1;
  }

  /// <summary>Обновить идентификаторы и атрибуты объектов БД для документа</summary>
  /// <param name="document">Документ</param>
  /// <param name="docObjID">Идентификатор версии объекта владеющего документом</param>
  /// <param name="updateDocumentLinks">Обновить все ссылки на атрибуты объектов БД в документе</param>
  /// <param name="updateLayout">Обновить разбивку документа на страницы после обновления данных</param>
  public static void UpdateDocumentDBObject(
    ImDocument document,
    long docObjID,
    bool updateDocumentLinks,
    bool updateLayout)
  {
    if (document == null)
      throw new ArgumentNullException(nameof (document));
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!docObjID.IsDefinedId())
        return;
      IDBObject objectActual = sessionKeeper.Session.GetObjectActual(docObjID, false);
      if (objectActual == null)
        return;
      DocumentEditorPlugin.Instance.SetDocumentDBObject((ImDocumentData) document, objectActual);
      if (!updateDocumentLinks)
        return;
      DocumentEditorPlugin.Instance.UpdateDocumentLinks((DocumentTreeNode) document, sessionKeeper.Session, true, true, false, false, updateLayout);
    }
  }

  /// <summary>Попытаться взять на редактирование документ с запросом пользователю на открытие в режиме для чтения</summary>
  /// <param name="documentID">Идентификатор документа в БД</param>
  /// <param name="readOnly">Только для чтения</param>
  /// <param name="cancel">Возвращает true, если в диалоге пользователь выбрал отмену</param>
  /// <param name="messageCaption">Заголовок для сообщений</param>
  /// <returns></returns>
  public static long TryCheckOutDocumentWhithDialog(
    long documentID,
    ref bool readOnly,
    out bool cancel,
    string messageCaption)
  {
    cancel = false;
    if (readOnly || documentID < 0L)
      return documentID;
    ObjectModifyModes objectModifyMode;
    long checkoutBy;
    long userId;
    string caption;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(documentID);
      objectModifyMode = dbObject.ObjectModifyMode;
      checkoutBy = dbObject.CheckoutBy;
      userId = sessionKeeper.Session.UserID;
      caption = dbObject.Caption;
    }
    switch (objectModifyMode)
    {
      case ObjectModifyModes.Checkout:
        if (checkoutBy == 0L)
        {
          DialogResult dialogResult = IMMessageBox.Show(messageCaption, string.Format(LocalizationHolder.rm.GetString("Document.Client_61"), (object) caption, (object) Environment.NewLine), new IMMessageBoxButton[3]
          {
            new IMMessageBoxButton(LocalizationHolder.rm.GetString("Document.Client_110"), DialogResult.Yes),
            new IMMessageBoxButton(LocalizationHolder.rm.GetString("Document.Client_111"), DialogResult.No),
            new IMMessageBoxButton(LocalizationHolder.rm.GetString("Document.Client_112"), DialogResult.Cancel)
          });
          if (dialogResult == DialogResult.Yes)
          {
            documentID = DocumentEditorPlugin.CheckOutWithNotification(documentID);
            break;
          }
          readOnly = true;
          cancel = dialogResult == DialogResult.Cancel;
          break;
        }
        if (checkoutBy == userId)
        {
          documentID = -documentID;
          break;
        }
        DialogResult dialogResult1 = MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("Document.Client_64"), (object) caption), messageCaption, MessageBoxButtons.YesNo);
        readOnly = true;
        cancel = dialogResult1 == DialogResult.No;
        break;
      case ObjectModifyModes.CreateVersion:
        DialogResult dialogResult2 = MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("Document.Client_66"), (object) caption), messageCaption, MessageBoxButtons.YesNo);
        readOnly = true;
        cancel = dialogResult2 == DialogResult.No;
        break;
      case ObjectModifyModes.CantModify:
        readOnly = true;
        break;
    }
    return documentID;
  }

  public static long TryCheckOutDocument(long objectID, ref bool readOnly)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject objectActual = sessionKeeper.Session.GetObjectActual(objectID, true);
      DocumentEditorPlugin.TryCheckOutDocument(objectActual, ref readOnly);
      return objectActual.ObjectID;
    }
  }

  /// <summary>Попытаться взять на редактирование документ</summary>
  /// <param name="documentObject">Объект базы данных</param>
  /// <param name="readOnly">Только для чтения. Если на входе readOnly - true, то объект не берётся на изменение.
  /// Если объект нельзя брать на изменение, то readOnly изменяется на true</param>
  public static IDBObject TryCheckOutDocument(IDBObject documentObject, ref bool readOnly)
  {
    if (readOnly || documentObject.ObjectID < 0L)
      return documentObject;
    long objectId = documentObject.ObjectID;
    switch (documentObject.ObjectModifyMode)
    {
      case ObjectModifyModes.Checkout:
        if (documentObject.CheckoutBy == 0L)
        {
          documentObject = DocumentEditorPlugin.CheckOutWithNotification(documentObject);
          break;
        }
        if (documentObject.CheckoutBy == documentObject.Session.UserID)
        {
          documentObject = documentObject.Session.GetObject(-documentObject.ObjectID);
          break;
        }
        readOnly = true;
        break;
      case ObjectModifyModes.CreateVersion:
        readOnly = true;
        break;
      case ObjectModifyModes.CantModify:
        readOnly = true;
        break;
    }
    return documentObject;
  }

  public static bool SaveDBObjectsAttributesFromReference(ImDocumentData document)
  {
    AttributeProcessorDictionary processorDictionary1 = document != null ? (AttributeProcessorDictionary) document.DBAttributeProcessorDictionary : throw new ArgumentNullException(nameof (document));
    if (processorDictionary1 != null)
    {
      AttributeProcessorDictionary processorDictionary2 = new AttributeProcessorDictionary((IDictionary<long, AttributeProcessor>) processorDictionary1);
      DialogResultAdv dialogResultAdv = DialogResultAdv.None;
      foreach (KeyValuePair<long, AttributeProcessor> keyValuePair in (Dictionary<long, AttributeProcessor>) processorDictionary2)
      {
        if (keyValuePair.Value != null)
        {
          if (keyValuePair.Value.Modified)
          {
            try
            {
              keyValuePair.Value.Save();
            }
            catch (Exception ex)
            {
              if (dialogResultAdv != DialogResultAdv.IgnoreAll)
              {
                dialogResultAdv = IMMessageBox.Show(LocalizationHolder.rm.GetString("Document.Client_68"), $"{LocalizationHolder.rm.GetString("Document.Client_69")}{ex.Message}\"", MessageBoxButtonsAdv.Ignore_IgnoreAll_Abort, IMMessageBoxImage.Error);
                if (dialogResultAdv == DialogResultAdv.Abort)
                  return false;
              }
            }
          }
        }
      }
    }
    return true;
  }

  /// <summary>Вспомогательный метод получения объекта IShowDwg. Нужен для поддержки DWG в ContainerElement</summary>
  /// <param name="objectId">Идентификатор объекта</param>
  /// <param name="valueIndex">Индекс файла в файловом атрибуте с множеством значений</param>
  /// <param name="fileName">Имя файла</param>
  /// <param name="data">Данные</param>
  /// <returns></returns>
  public IShowDwg GetShowDwgObject(long objectId, int valueIndex, string fileName, byte[] data)
  {
    if (this.DwgVisualizer == null)
    {
      IVisualizerService visualizerService = this.serviceProvider == null ? (IVisualizerService) ServicesManager.GetService(typeof (IVisualizerService)) : (IVisualizerService) this.serviceProvider.GetService(typeof (IVisualizerService));
      if (visualizerService != null)
        this.DwgVisualizer = visualizerService.GetVisualizer("dwg");
    }
    IShowDwg showDwgObject = (IShowDwg) null;
    if (this.DwgVisualizer != null)
      showDwgObject = this.DwgVisualizer.GetViewObject(objectId, valueIndex, fileName, data) as IShowDwg;
    return showDwgObject;
  }

  protected static void UpdateDocumentTreeLinksOld(
    DocumentTreeNode parentNode,
    SessionKeeper sessionKeeper,
    Dictionary<Guid, Dictionary<Guid, AttributeValueCache>> objAttrCache,
    Dictionary<Guid, Dictionary<Guid, AttributeValueCache>> relAttrCache,
    bool updateInTemplate,
    bool updateDBLink,
    bool updateDocLink,
    bool updateUI,
    bool updateLayout,
    bool resetCache)
  {
    ImDocument imDocument = parentNode != null ? parentNode as ImDocument : throw new ArgumentNullException(nameof (parentNode));
    if (objAttrCache == null && imDocument != null)
    {
      if (resetCache)
      {
        lock (imDocument.ObjAttrCache)
          imDocument.ObjAttrCache.Clear();
      }
      objAttrCache = imDocument.ObjAttrCache;
      if (resetCache && imDocument.DBAttributeProcessorDictionary != null)
      {
        lock (imDocument.DBAttributeProcessorDictionary)
          ((Dictionary<long, AttributeProcessor>) imDocument.DBAttributeProcessorDictionary).Clear();
      }
    }
    if (relAttrCache == null && imDocument != null)
    {
      if (resetCache)
      {
        lock (imDocument.RelAttrCache)
          imDocument.RelAttrCache.Clear();
      }
      relAttrCache = imDocument.RelAttrCache;
    }
    if (parentNode is INodeWithReference nodeWithReference)
    {
      ReferenceToDBObjectBase reference = nodeWithReference.Reference as ReferenceToDBObjectBase;
      if (updateDBLink && reference != null && sessionKeeper != null)
        reference.UpdateLink((object) sessionKeeper.Session, objAttrCache, relAttrCache, false, updateUI, updateLayout);
      else if (updateDocLink && nodeWithReference.Reference != null && reference == null)
        nodeWithReference.Reference.UpdateLink(updateUI, updateLayout);
    }
    if (parentNode.Nodes == null)
      return;
    if (imDocument != null & updateInTemplate && imDocument.DocumentTemplate != null)
      DocumentEditorPlugin.UpdateDocumentTreeLinksOld((DocumentTreeNode) imDocument.DocumentTemplate, sessionKeeper, objAttrCache, relAttrCache, updateInTemplate, updateDBLink, updateDocLink, updateUI, updateLayout, false);
    for (int index = 0; index < parentNode.Nodes.Count; ++index)
    {
      DocumentTreeNode node = parentNode.Nodes[index];
      if (node != null)
        DocumentEditorPlugin.UpdateDocumentTreeLinksOld(node, sessionKeeper, objAttrCache, relAttrCache, updateInTemplate, updateDBLink, updateDocLink, updateUI, updateLayout, false);
    }
  }

  protected override void CheckAttributeProcessorDictionary(
    ImDocumentData document,
    bool resetCache)
  {
    if (document.DBAttributeProcessorDictionary != null)
    {
      if (!resetCache)
        return;
      lock (document.DBAttributeProcessorDictionary)
        ((Dictionary<long, AttributeProcessor>) document.DBAttributeProcessorDictionary).Clear();
    }
    else
      document.DBAttributeProcessorDictionary = (object) new AttributeProcessorDictionary();
  }

  private static void UpdateTest(SessionKeeper sessionKeeper, Dictionary<Guid, List<long>> attrDict)
  {
    foreach (KeyValuePair<Guid, List<long>> keyValuePair in attrDict)
    {
      Guid key = keyValuePair.Key;
      int attributeTypeId = MetaDataHelper.GetAttributeTypeID(key);
      MetaDataHelper.GetAttributeTypeName(key);
      List<long> longList = keyValuePair.Value;
      foreach (DataRow row in (InternalDataCollectionBase) sessionKeeper.Session.GetObjectCollection(-1).SelectWithLocalObjects(new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(-2, RelationalOperators.In, (object) longList.ToArray(), LogicalOperators.NONE, 0, true)
      }, new ColumnDescriptor[3]
      {
        new ColumnDescriptor((object) -2, ColumnContents.String, ColumnNameMapping.ID, SortOrders.NONE, 0),
        new ColumnDescriptor((object) -12, ColumnContents.String, ColumnNameMapping.ID, SortOrders.NONE, 0),
        new ColumnDescriptor((object) attributeTypeId, ColumnContents.String, ColumnNameMapping.ID, SortOrders.NONE, 0)
      })).Rows)
      {
        int num1 = -12;
        Guid guid = (Guid) row[num1.ToString()];
        num1 = -2;
        long num2 = (long) row[num1.ToString()];
        object obj = row[attributeTypeId.ToString()];
        DBNull dbNull = DBNull.Value;
      }
    }
  }

  private static void UpdateTest1(
    SessionKeeper sessionKeeper,
    Dictionary<Guid, List<long>> attrDict)
  {
    foreach (KeyValuePair<Guid, List<long>> keyValuePair in attrDict)
    {
      Guid key = keyValuePair.Key;
      int attributeID = MetaDataHelper.GetAttributeTypeID(key);
      MetaDataHelper.GetAttributeTypeName(key);
      foreach (long objectID in keyValuePair.Value)
      {
        IDBObject objectActual = sessionKeeper.Session.GetObjectActual(objectID, false);
        if (objectActual != null)
        {
          AttributeValues attributeValues = ((IEnumerable<AttributeValues>) objectActual.GetAttributesValues(GetAttributeValuesModes.IncludeObligatoryAttributes)).Where<AttributeValues>((System.Func<AttributeValues, bool>) (x => x.AttributeID == attributeID)).FirstOrDefault<AttributeValues>();
          if (attributeValues != null)
          {
            object obj = attributeValues.Value;
          }
        }
      }
    }
  }

  public void UpdateObjAttrCache(SessionKeeper sessionKeeper, ImDocument document)
  {
    List<Guid> guidList = new List<Guid>();
    Dictionary<Guid, ObjInfoItem> dictionary = new Dictionary<Guid, ObjInfoItem>();
    foreach (DocumentTreeNode childNode in DocumentTreeNode.GetChildNodes((DocumentTreeNode) document))
    {
      if (childNode is INodeWithReference nodeWithReference && nodeWithReference.Reference is ReferenceToDBObjectAttributeBase reference && !guidList.Contains(reference.AttributeGuid))
        guidList.Add(reference.AttributeGuid);
    }
  }

  /// <summary>Событие перед сохранением файла</summary>
  public static event SaveAsEventHandler BeforeFileSaveAs;

  /// <summary>Вызов обработчика перед сохранением файла</summary>
  internal static void OnBeforeSaveAs(object sender, SaveAsEventHandlerArgs eventArgs)
  {
    SaveAsEventHandler beforeFileSaveAs = DocumentEditorPlugin.BeforeFileSaveAs;
    if (beforeFileSaveAs == null)
      return;
    beforeFileSaveAs(sender, eventArgs);
  }

  /// <summary>Событие перед сохранением файла</summary>
  public static event SaveAsEventHandler AfterFileSaveAs;

  /// <summary>Вызов обработчика после сохранения файла</summary>
  internal static void OnAfterFileSaveAs(object sender, SaveAsEventHandlerArgs eventArgs)
  {
    SaveAsEventHandler afterFileSaveAs = DocumentEditorPlugin.AfterFileSaveAs;
    if (afterFileSaveAs == null)
      return;
    afterFileSaveAs(sender, eventArgs);
  }

  /// <summary>Событие перед сохранением файла</summary>
  public static event AfterLoadDocumentEventHandler AfterLoadDocument;

  /// <summary>Вызов обработчика перед сохранением файла</summary>
  internal static void OnAfterLoadDocument(
    object sender,
    AfterLoadDocumentEventHandlerArgs eventArgs)
  {
    AfterLoadDocumentEventHandler afterLoadDocument = DocumentEditorPlugin.AfterLoadDocument;
    if (afterLoadDocument == null)
      return;
    afterLoadDocument(sender, eventArgs);
  }

  internal static Guid GetContextDocumentComplect(long documentID)
  {
    if (Intermech.Consts.IsUndefinedObjectId(documentID))
      return Guid.Empty;
    Guid empty = Guid.Empty;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IFiltrationService service = ServicesManager.GetService(typeof (IFiltrationService)) as IFiltrationService;
      long compositionParentObject = (sessionKeeper.Session.GetCustomService(typeof (ICompositionLoadService)) as ICompositionLoadService).FindCompositionParentObject((object) sessionKeeper.Session.SessionGUID, documentID, MetaDataHelper.GetRelationTypeID(new Guid("cad00151-306c-11d8-b4e9-00304f19f545")), service.FiltrationServiceOwnerID);
      return sessionKeeper.Session.GetObjectInfo(compositionParentObject).VersionGuid;
    }
  }

  /// <summary>Активное окно редактора документов</summary>
  [Browsable(false)]
  public static ImDocumentEditorForm ActiveImDocumentEditorForm
  {
    [DebuggerStepThrough] get
    {
      return DocumentEditorPlugin.DockManager.ActiveDocument as ImDocumentEditorForm;
    }
  }

  /// <summary>DocumentControl активного окна редактора документов</summary>
  [Browsable(false)]
  public static DocumentControl ActiveImDocumentControl
  {
    [DebuggerStepThrough] get
    {
      return DocumentEditorPlugin.DockManager.ActiveDocument != null && DocumentEditorPlugin.DockManager.ActiveDocument is ImDocumentEditorForm ? (DocumentEditorPlugin.DockManager.ActiveDocument as ImDocumentEditorForm).DocumentControl : (DocumentControl) null;
    }
  }

  /// <summary>Восстановить окно</summary>
  /// <param name="guid">Guid окна</param>
  /// <param name="persistString">Строка данных окна</param>
  /// <returns>Окно</returns>
  public DockControl RestoreDocumentWindow(Guid guid, string persistString)
  {
    try
    {
      if (guid == DocumentEditorPlugin.ImDocumentEditorFormGuid)
      {
        DocumentWindowData persistString1 = DocumentEditorPlugin.ParsePersistString(persistString);
        if (!persistString1.IsEmpty)
          return (DockControl) this.OpenDocumentImDocumentObject(persistString1.DocumentObjectID, persistString1.ReadOnly, false);
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
    return (DockControl) null;
  }

  /// <summary>Разобрать строку с сохранённым состоянием окна</summary>
  /// <param name="persistString">Текстовая строка содержащая данные для восстановления</param>
  /// <returns></returns>
  public static DocumentWindowData ParsePersistString(string persistString)
  {
    DocumentWindowData persistString1 = (DocumentWindowData) null;
    readOnly = false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = (IDBObject) null;
      long result = -1;
      if (long.TryParse(persistString, out result))
        dbObject = sessionKeeper.Session.GetObjectActual(result, false);
      else if (persistString != null && persistString.Length != 36)
      {
        HybridDictionary hybridDictionary = DocumentEditorPlugin.ReadConfigDictionaryFromPersistString(persistString);
        object objectGUID = hybridDictionary[(object) "DocumentGuid"];
        if (objectGUID != null && objectGUID is Guid guid && guid != Guid.Empty)
          dbObject = sessionKeeper.Session.GetObject((Guid) objectGUID, false);
        object obj = hybridDictionary[(object) "ReadOnly"];
        if (obj == null || !(obj is bool readOnly))
          ;
      }
      else
      {
        Guid objectGUID = new Guid(persistString);
        if (objectGUID != Guid.Empty)
          dbObject = sessionKeeper.Session.GetObject(objectGUID, false);
      }
      if (dbObject != null)
      {
        if (dbObject.AccessLevel <= sessionKeeper.Session.SecurityLevel)
          persistString1 = new DocumentWindowData(dbObject.ObjectGUID, dbObject.ObjectID, dbObject.ObjectType, readOnly);
      }
    }
    if (persistString1 == null)
      persistString1 = new DocumentWindowData(Guid.Empty, -1L, -1, false);
    return persistString1;
  }

  /// <summary>Загрузить словарь с параметрами окна из сохранённой строки</summary>
  /// <param name="persistString">Текстовая строка содержащая данные для восстановления</param>
  /// <returns></returns>
  public static HybridDictionary ReadConfigDictionaryFromPersistString(string persistString)
  {
    HybridDictionary hybridDictionary = (HybridDictionary) null;
    try
    {
      using (MemoryStream serializationStream = new MemoryStream(Convert.FromBase64String(persistString)))
        hybridDictionary = new BinaryFormatter().Deserialize((Stream) serializationStream) as HybridDictionary;
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
    if (hybridDictionary == null)
      hybridDictionary = new HybridDictionary();
    return hybridDictionary;
  }

  /// <summary>Преобразовать значение в Guid</summary>
  /// <param name="value">Значение</param>
  /// <returns></returns>
  public static Guid ConvertToGuid(object value)
  {
    switch (value)
    {
      case null:
      case DBNull _:
        return Guid.Empty;
      case Guid guid:
        return guid;
      default:
        string g = value as string;
        return !string.IsNullOrWhiteSpace(g) ? new Guid(g) : Guid.Empty;
    }
  }

  private void DockManagerActiveDocumentChanged(object sender, ActiveDocumentEventArgs e)
  {
    try
    {
      if (this.statusBar != null && e.PreviousActiveDocument is ImDocumentEditorFormBase previousActiveDocument)
        previousActiveDocument.RestoreStatusBar(this.statusBar.StatusBar);
      if (!(e.NewActiveDocument is ImDocumentEditorFormBase newActiveDocument))
        return;
      if (this.statusBar != null)
        newActiveDocument.SetStatusBar(this.statusBar.StatusBar);
      this.SelectionChanged();
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  /// <summary>Создать окно для документа</summary>
  /// <param name="document">Документ</param>
  /// <param name="readOnly">Только для чтения</param>
  /// <param name="documentWindowCreator">Делегат для конструктора окна документа</param>
  private ImDocumentEditorForm CreateDocumentWindow(
    ImDocument document,
    bool readOnly,
    DocumentWindowCreatorDelegate documentWindowCreator = null)
  {
    if (document == null)
      throw new ArgumentNullException(nameof (document));
    if (documentWindowCreator == null)
      documentWindowCreator = new DocumentWindowCreatorDelegate(ImDocumentEditorForm.DocumentWindowCreator);
    ImDocumentEditorForm form = documentWindowCreator((IImDocumentManager) this, document, readOnly);
    if (documentWindowCreator == new DocumentWindowCreatorDelegate(ImDocumentEditorForm.DocumentWindowCreator))
      form.UndoManager = (IUndoManager) new UndoManager((ImDocumentEditorFormBase) form);
    form.Text = document.DBObjectCaption;
    form.Closed += new EventHandler(this.doc_Closed);
    if (document.IsFormulaLib && document.DocumentControl?.PageControl != null)
      form.DocumentControl.PageControl.OnePage = true;
    if (readOnly)
      document.Modified = false;
    if (form.DocumentControl != null)
      form.DocumentControl.DocumentModifiedChanged += new ModifiedChanged_EventHandler(this.DocumentModifiedChanged);
    return form;
  }

  /// <summary>Создать окно для комплекта документов</summary>
  /// <param name="documentsComplect">Комплект документов</param>
  /// <param name="readOnly">Только для чтения</param>
  private ImDocumentEditorForm CreateComplectWindow(
    DocumentsComplect documentsComplect,
    bool readOnly)
  {
    ImDocumentEditorForm form = new ImDocumentEditorForm((IImDocumentManager) this, documentsComplect, readOnly);
    form.Text = documentsComplect.Name;
    form.Closed += new EventHandler(this.doc_Closed);
    form.UndoManager = (IUndoManager) new UndoManager((ImDocumentEditorFormBase) form);
    return form;
  }

  private void doc_Closed(object sender, EventArgs e)
  {
    try
    {
      if (!(sender is ImDocumentEditorForm documentEditorForm))
        return;
      if (documentEditorForm.DocumentControl != null)
      {
        documentEditorForm.DocumentControl.ActivePage = (Page) null;
        documentEditorForm.DocumentControl.SetSelection((List<DocumentTreeNode>) null, false, false);
        documentEditorForm.DocumentControl.DocumentModifiedChanged -= new ModifiedChanged_EventHandler(this.DocumentModifiedChanged);
      }
      documentEditorForm.Closed -= new EventHandler(this.doc_Closed);
      if (documentEditorForm.InternalDocumentTemplateWindow == null)
        return;
      documentEditorForm.InternalDocumentTemplateWindow.Close();
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  /// <summary>Создать новый документ</summary>
  public void CreateNewDocument()
  {
    try
    {
      long objectByTypeDialog = (this.serviceProvider == null ? ServicesManager.GetService(typeof (IObjectCreatorService)) as IObjectCreatorService : this.serviceProvider.GetService(typeof (IObjectCreatorService)) as IObjectCreatorService).CreateObjectByTypeDialog(DocIDCache.ObjType_ImDocTemplate);
      if (objectByTypeDialog == -1L)
        return;
      (this.serviceProvider == null ? ServicesManager.GetService(typeof (INotificationService)) as INotificationService : this.serviceProvider.GetService(typeof (INotificationService)) as INotificationService)?.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", objectByTypeDialog));
      this.OpenDocumentImDocumentObject(objectByTypeDialog, false, true);
      RecentObjectsNode.MRUObjects.Add(objectByTypeDialog, ObjectAction.Open, DateTime.UtcNow);
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  public void DocumentModifiedChanged(object sender, ModifiedChanged_EventArgs e)
  {
    try
    {
      if (sender is ImDocument document)
        DocumentEditorPlugin.UpdateDocumentCaption(DocumentEditorPlugin.DockManager, document);
      if (this.CommandManager == null)
        return;
      this.CommandManager.QueryStatus();
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  public ImDocumentEditorForm OpenImDocument(
    ImDocument document,
    bool readOnly = false,
    bool showWindow = true,
    bool callDialogWithObjectParamsBeforeSave = true,
    int defaultDocumentDbObjectType = -1)
  {
    ImDocumentEditorForm documentEditorForm = this.OpenDocument((DocumentTreeNode) document, readOnly, showWindow);
    documentEditorForm.CallDialogWithObjectParamsBeforeSave = callDialogWithObjectParamsBeforeSave;
    documentEditorForm.DefaultDocumentDbObjectType = defaultDocumentDbObjectType;
    return documentEditorForm;
  }

  /// <summary>Создать окно документа</summary>
  /// <param name="documentOrComplect">Документ</param>
  /// <param name="readOnly">Только для чтения</param>
  /// <param name="show">Показать окно после создания</param>
  /// <param name="documentWindowCreator">Делегат для конструктора окна документа</param>
  /// <returns>Окно документа</returns>
  public ImDocumentEditorForm OpenDocument(
    DocumentTreeNode documentOrComplect,
    bool readOnly,
    bool show,
    DocumentWindowCreatorDelegate documentWindowCreator = null)
  {
    try
    {
      if (documentOrComplect == null)
        throw new ArgumentNullException(nameof (documentOrComplect));
      if (DocumentEditorPlugin.DockManager == null)
        throw new Exception("DockManager == null");
      ImDocumentEditorForm docWin = (ImDocumentEditorForm) null;
      switch (documentOrComplect)
      {
        case ImDocument document:
          docWin = this.CreateDocumentWindow(document, readOnly, documentWindowCreator);
          break;
        case DocumentsComplect documentsComplect:
          docWin = this.CreateComplectWindow(documentsComplect, readOnly);
          break;
      }
      DocumentEditorPlugin.UpdateDocumentCaption(DocumentEditorPlugin.DockManager, docWin);
      if (show && docWin != null)
      {
        docWin.Show(DocumentEditorPlugin.DockManager, DockState.Document);
        docWin.Select();
      }
      return docWin;
    }
    catch (Exception ex)
    {
      LogManager.AddLine($"DocumentEditorPlugin.OpenDocument. Exception\r\n{ex.Message}\r\n{ex.StackTrace}", true);
      throw;
    }
  }

  /// <summary>Открыть шаблон документа в отдельном окне</summary>
  /// <param name="docWindow">Окно документа</param>
  /// <param name="show">Открыть окно</param>
  /// <returns>Окно</returns>
  private ImDocumentEditorForm ShowDocumentTemplate(ImDocumentEditorForm docWindow, bool show)
  {
    docWin = (ImDocumentEditorForm) null;
    try
    {
      if (docWindow == null)
        throw new ArgumentNullException(nameof (docWindow));
      if (docWindow.Document.Template != null)
      {
        bool readOnly = false;
        if (docWindow.Document.DocumentControl != null)
          readOnly = docWindow.Document.DocumentControl.ReadOnly;
        ImDocument documentTemplate = docWindow.Document.DocumentTemplate as ImDocument;
        documentTemplate.SetNeedUIRecursive(true, true);
        if (!(docWindow.InternalDocumentTemplateWindow is ImDocumentEditorForm docWin))
        {
          docWin = this.CreateDocumentWindow(documentTemplate, readOnly);
          docWin.DisposeDocumentOnClose = false;
          DocumentEditorPlugin.UpdateDocumentCaption(DocumentEditorPlugin.DockManager, docWin);
        }
        if (show)
        {
          docWin.Show(DocumentEditorPlugin.DockManager, DockState.Document);
          docWin.Select();
        }
      }
    }
    catch (Exception ex)
    {
      docWin = (ImDocumentEditorForm) null;
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
    return docWin;
  }

  /// <summary>Открыть документ, загрузив его из объекта</summary>
  /// <param name="docID">Идентификатор объекта</param>
  /// <param name="readOnly">Только для чтения</param>
  /// <param name="show">Отобразить созданное окно</param>
  /// <param name="documentWindowCreator">Делегат для конструктора окна документа</param>
  /// <returns>Окно документа</returns>
  public ImDocumentEditorForm OpenDocumentImDocumentObject(
    long docID,
    bool readOnly,
    bool show,
    DocumentWindowCreatorDelegate documentWindowCreator = null)
  {
    return this.OpenDocumentImDocumentObject(docID, -1, -1, readOnly, show, documentWindowCreator);
  }

  /// <summary>Открыть документ, загрузив его из объекта</summary>
  /// <param name="docID">Идентификатор объекта</param>
  /// <param name="fileIndex">Индекс файла в файловом атрибуте</param>
  /// <param name="readOnly">Только для чтения</param>
  /// <param name="show">Отобразить созданное окно</param>
  /// <param name="documentWindowCreator">Делегат для конструктора окна документа</param>
  /// <returns>Окно документа</returns>
  public ImDocumentEditorForm OpenDocumentImDocumentObject(
    long docID,
    int fileAttributeID,
    int fileIndex,
    bool readOnly,
    bool show,
    DocumentWindowCreatorDelegate documentWindowCreator = null)
  {
    if (Intermech.Consts.IsUndefinedObjectId(docID))
      return (ImDocumentEditorForm) null;
    DockControl dockControl = this.FindOpenedDocument(DBHelper.GetObjGuidByID(docID));
    if (dockControl is ImDocumentEditorForm target && target.ReadOnly && !readOnly)
    {
      target.Close();
      target = (ImDocumentEditorForm) null;
      dockControl = (DockControl) null;
    }
    if (target == null)
    {
      DocumentTreeNode documentOrComplect = (DocumentTreeNode) null;
      Guid documentComplect = DocumentEditorPlugin.GetContextDocumentComplect(docID);
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        documentOrComplect = DocumentEditorPlugin.LoadDocumentFromDBObject(sessionKeeper.Session.GetObject(docID), fileAttributeID, fileIndex, documentComplect, true, true, true, true);
      if (documentOrComplect != null)
      {
        target = this.OpenDocument(documentOrComplect, readOnly, show, documentWindowCreator);
        if (documentOrComplect is ImDocument imDocument)
          target.DefaultFileName = imDocument.FileName;
      }
      if (show && dockControl != null && dockControl != target)
      {
        dockControl.ReplaceTo((DockControl) target);
        target.Select();
      }
    }
    else
    {
      target.ReadOnly = readOnly;
      target.Activate();
    }
    if (readOnly)
      RecentObjectsNode.MRUObjects.Add(docID, ObjectAction.View, DateTime.UtcNow);
    else
      RecentObjectsNode.MRUObjects.Add(docID, ObjectAction.Open, DateTime.UtcNow);
    return target;
  }

  /// <summary> Получить список всех открытых окон редактора документов </summary>
  public DockControl FindOpenedDocument(Guid docGuid)
  {
    if (docGuid == Guid.Empty)
      throw new ArgumentException("Недопустимое значение Guid.Empty для аргумента", nameof (docGuid));
    if (DocumentEditorPlugin.DockManager != null && DocumentEditorPlugin.DockManager.DocumentContainer != null && DocumentEditorPlugin.DockManager.DocumentContainer.Documents != null)
    {
      foreach (DockControl document in DocumentEditorPlugin.DockManager.DocumentContainer.Documents)
      {
        if (document is ImDocumentEditorForm openedDocument && openedDocument.DocumentGuid == docGuid)
          return (DockControl) openedDocument;
        if ((document.Guid == DocumentEditorPlugin.ImDocumentEditorFormGuid || document.Guid == DocumentEditorPlugin.VedomostWindowGuid) && DocumentEditorPlugin.ConvertToGuid(DocumentEditorPlugin.ReadConfigDictionaryFromPersistString(document.PersistString)[(object) "DocumentGuid"]) == docGuid)
          return document;
      }
    }
    return (DockControl) null;
  }

  /// <summary>Отправить на печать документ</summary>
  /// <param name="docID">Идентификатор версии объекта документа</param>
  public void PrintImDocumentObject(long docID) => this.PrintImDocumentObject(docID, -1, -1);

  /// <summary>Отправить на печать документ</summary>
  /// <param name="docID">Идентификатор версии объекта документа</param>
  /// <param name="fileAttributeID">Идентификатор файлового атрибута объекта</param>
  /// <param name="fileIndex">Индекс файлового атрибута объекта</param>
  public void PrintImDocumentObject(long docID, int fileAttributeID, int fileIndex)
  {
    Guid documentComplect = DocumentEditorPlugin.GetContextDocumentComplect(docID);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject docObject = sessionKeeper.Session.GetObject(docID);
      ImDocument imDoc = (ImDocument) null;
      if (docObject != null)
        imDoc = DocumentEditorPlugin.LoadDocumentFromDBObject(docObject, fileAttributeID, fileIndex, documentComplect, false, false, true, false) as ImDocument;
      if (imDoc != null)
      {
        imDoc.InitPrintDocument();
        int num = 1;
        if (imDoc.NodesCount == 0)
          num = 0;
        imDoc.PrintDocument.PrinterSettings.MinimumPage = num;
        imDoc.PrintDocument.PrinterSettings.FromPage = num;
        imDoc.PrintDocument.PrinterSettings.MaximumPage = imDoc.Nodes.Count;
        imDoc.PrintDocument.PrinterSettings.ToPage = imDoc.Nodes.Count;
        if (new PrintDocumentDialog(imDoc.PrintDocument, (ImDocumentData) imDoc).ShowDialog() == DialogResult.OK)
          imDoc.PrintDocument.Print();
      }
      RecentObjectsNode.MRUObjects.Add(docID, ObjectAction.Print, DateTime.UtcNow);
    }
  }

  /// <summary>Сформировать заголовок окна документа</summary>
  /// <param name="docCaption">Заголовок документа</param>
  /// <param name="isTemplate">Является ли документ шаблоном</param>
  /// <param name="readOnly">Документ открыт только для чтения</param>
  /// <param name="modified">Документ был изменён</param>
  /// <returns></returns>
  public static string FormatDocWindowCaption(
    string docCaption,
    bool isTemplate,
    bool readOnly,
    bool modified)
  {
    return docCaption + (isTemplate ? LocalizationHolder.rm.GetString("Document.Client_76") : "") + (readOnly ? LocalizationHolder.rm.GetString("Document.Client_77") : "") + (modified ? DocumentEditorPlugin.ModifiedSign(modified) : "");
  }

  /// <summary>Обновить заголовок окна</summary>
  /// <param name="dockManager">Менеджер докинга</param>
  /// <param name="docWin">Окно</param>
  public static void UpdateDocumentCaption(DockManager dockManager, ImDocumentEditorForm docWin)
  {
    if (docWin == null)
      throw new ArgumentNullException(nameof (docWin));
    if (docWin.Document == null)
      return;
    docWin.UpdateDocumentWindowCaption();
    DocumentEditorPlugin.UpdateDocumentCaption(dockManager, docWin.Document);
  }

  /// <summary>Обновить заголовки окон документа</summary>
  /// <param name="dockManager">Менеджер докинга</param>
  /// <param name="document">Документ</param>
  public static void UpdateDocumentCaption(DockManager dockManager, ImDocument document)
  {
    if (document == null)
      throw new ArgumentNullException(nameof (document));
    if (dockManager == null)
      throw new ArgumentNullException(nameof (dockManager));
    if (document.IsTemplate && document.TemplateOwner != null)
      document = document.TemplateOwner as ImDocument;
    ImDocumentData documentTemplate = document.DocumentTemplate;
    if (document.IsTemplate)
      document = document.TemplateOwner as ImDocument;
    for (int index = 0; index < dockManager.DocumentContainer.Documents.Length; ++index)
    {
      if (dockManager.DocumentContainer.Documents[index] is ImDocumentEditorForm document1)
        document1.UpdateDocumentWindowCaption();
    }
  }

  /// <summary>Обновить заголовоки окон</summary>
  /// <param name="dockManager">Менеджер докинга</param>
  public static void UpdateDocumentCaptions(DockManager dockManager)
  {
    for (int index = 0; index < dockManager.DocumentContainer.Documents.Length; ++index)
    {
      if (dockManager.DocumentContainer.Documents[index] is ImDocumentEditorForm document)
        document.UpdateDocumentWindowCaption();
    }
  }

  /// <summary>Возвращает символ (метку) для изменённых документов. Если документ не изменялся, то пустую строку</summary>
  /// <param name="modified">Изменялся ли документ</param>
  /// <returns></returns>
  public static string ModifiedSign(bool modified) => modified ? "*" : "";

  /// <summary>Имя плагина клиента</summary>
  public string Name => LocalizationHolder.rm.GetString("Document.Client_78");

  private void AddDocCommandToMenu(string commandName, MenuItemBase menu)
  {
    MenuButtonItem menuItem = DocumentMenuHelper.GetMenuItem(commandName);
    if (menuItem == null)
      return;
    menu.Items.Add((ToolbarItemBase) menuItem);
  }

  /// <summary>
  /// Зарегистрировать все закладки, добавляемые модулем расширения в Навигатор
  /// </summary>
  internal void RegisterViews()
  {
    AdjustableViewsHelper.RegisterView("TableReportView", LocalizationHolder.rm.GetString("Document.Client_44"), "", "", "imgTableReportEdit", true, 0);
  }

  /// <summary>Загрузить плагин</summary>
  /// <param name="serviceProvider">Провайдер сервисов клиента</param>
  public void Load(System.IServiceProvider serviceProvider)
  {
    DocumentEditorPlugin.InitDocumentPlugin();
    this.serviceProvider = serviceProvider;
    if (serviceProvider == null)
      return;
    ContainerElement.GetShowDwgObject = new GetShowDwgObjectDelegate(this.GetShowDwgObject);
    ImDocumentVisualizer.Initialize(serviceProvider);
    DocumentEditorPlugin.dockManager = (DockManager) serviceProvider.GetService(typeof (DockManager));
    DocumentMenuHelper.DockManager = DocumentEditorPlugin.dockManager;
    DocumentEditorPlugin.dockManager.DocumentContainer.ActiveDocumentChanged += new ActiveDocumentEventHandler(this.DockManagerActiveDocumentChanged);
    DocumentEditorPlugin.dockManager.DockControlActivated += new DockControlEventHandler(this.dockManager_DockControlActivated);
    IFactory service1 = (IFactory) serviceProvider.GetService(typeof (IFactory));
    IPropertyPagesService service2 = (IPropertyPagesService) serviceProvider.GetService(typeof (IPropertyPagesService));
    if (service2 != null)
    {
      service2.AddPage(LocalizationHolder.rm.GetString("Document.Client_79"), (IPropertyPage) ImDocumentEditorConfig.Instance);
      service2.AddPage(LocalizationHolder.rm.GetString("Document.Client_175"), (IPropertyPage) ImDocumentClientConfig.Instance);
    }
    IProcessFileService service3 = serviceProvider.GetService<IProcessFileService>(false);
    if (service3 != null)
      service3.FileProcessEvent += new FileProcessEventHandler(this.FileService_FileProcessEvent);
    ObjectCommandEvents.SaveChanges.After += new EventHandler<AfterObjectCommandArgs>(this.SaveChanges_After);
    OldDocumentImportProvider.Init(serviceProvider);
    DocumentEditorIntegrator integrator = new DocumentEditorIntegrator();
    integrator.Initialize();
    ClientContext.Integrators.RegisterIntegrator((IIntegrator) integrator);
    ClientContext.LaunchActions.RegisterHandler((ILaunchHandler) new DocumentEditorLaunchHandler(integrator));
    IObjectCreatorService service4 = ServiceUtils.GetService<IObjectCreatorService>((object) ServicesManager.ServiceContainer, true);
    if (service4 != null)
      service4.AfterDraftCreatedEvent += new AfterDraftCreatedEventHandler(this.ObjectCreatorDraftCreated);
    service1?.AddCommandsProvider(1, (ICommandsProvider) this);
    this.statusBar = (IStatusBar) serviceProvider.GetService(typeof (IStatusBar));
    BarManager service5 = (BarManager) serviceProvider.GetService(typeof (BarManager));
    DocumentEditorPlugin.commandManager = (ICommandManager) serviceProvider.GetService(typeof (ICommandManager));
    if (DocumentEditorPlugin.commandManager == null)
      DocumentEditorPlugin.commandManager = (ICommandManager) new Intermech.Bars.CommandManager();
    INamedImageList service6 = (INamedImageList) ServicesManager.GetService(typeof (INamedImageList));
    int imageIndex1 = service6 != null ? service6.ImageIndex("imgReport") : -1;
    int imageIndex2 = service6 != null ? service6.ImageIndex("imgTableReport") : -1;
    if (service1 != null)
    {
      MenuTemplate contextMenuTemplate = service1.ContextMenuTemplate;
      if (contextMenuTemplate != null)
      {
        contextMenuTemplate.BeginUpdate();
        MenuTemplateNode node = new MenuTemplateNode("Reports", LocalizationHolder.rm.GetString("Document.Client_80"), imageIndex1, 40, 30);
        contextMenuTemplate.Nodes.Add(node);
        node.Nodes.Add(new MenuTemplateNode("TableReports", LocalizationHolder.rm.GetString("Document.Client_81"), imageIndex2, 10, 30));
        contextMenuTemplate.EndUpdate();
      }
    }
    this._asMenuService = new AttachedSelMenuService(new List<int>((IEnumerable<int>) new int[2]
    {
      MetaDataHelper.GetObjectTypeID("cad00289-306c-11d8-b4e9-00304f19f545"),
      MetaDataHelper.GetObjectTypeID("cad0028a-306c-11d8-b4e9-00304f19f545")
    }));
    this._asMenuService.ButtonMenuPressedEvent += new ButtonMenuPressedEventHandler(this.ReportButtonMenuPressed);
    Intermech.Navigator.ContextMenu.Services.AfterCreateMenu += new AfterCreateMenuHandler(this._asMenuService.AfterCreateMenu);
    ISelectionDialogTabsService service7 = (ISelectionDialogTabsService) ServicesManager.GetService(typeof (ISelectionDialogTabsService));
    if (service7 != null)
      service7.SelectionDialogTabEvent += new SelectionDialogTabCreateHandler(this.sdTabService_SelectionDialogTabEvent);
    if (service1 != null)
    {
      TableReportProvider provider = new TableReportProvider();
      service1.AddViewsProvider(1, ObjectTypesHelper.GetObjTypeID("cad00289-306c-11d8-b4e9-00304f19f545"), (IViewsProvider) provider);
      service1.AddViewsProvider(1, ObjectTypesHelper.GetObjTypeID("cad0028a-306c-11d8-b4e9-00304f19f545"), (IViewsProvider) provider);
    }
    IObjectCreatorService service8 = (IObjectCreatorService) ServicesManager.GetService(typeof (IObjectCreatorService));
    if (service8 != null)
      TableReportCreatorForm.Attach(service8);
    MenuBar menuBar1 = (MenuBar) null;
    if (service5 != null)
      menuBar1 = service5.MenuBar;
    if (this.CommandManager != null && menuBar1 != null)
    {
      DocumentEditorPlugin.imageList = menuBar1.ImageList;
      DocumentMenuHelper.CreateMenuCommands(this.CommandManager);
      MenuItemBase menuBar2 = (MenuItemBase) menuBar1.FindMenuBar("File");
      MenuItemBase menuItem1 = menuBar1.FindMenuItem("File.New");
      if (menuItem1 != null)
      {
        MenuButtonItem menuButtonItem1 = new MenuButtonItem(LocalizationHolder.rm.GetString("Document.Client_82"));
        menuButtonItem1.CommandName = "New.ImDocument";
        if (ServicesManager.GetService(typeof (ICategoryTypeIconService)) is ICategoryTypeIconService service9 && service9.IndexOf(4, DocIDCache.ObjType_ImDocTemplate) >= 0)
          menuButtonItem1.Icon = service9.GetIcon(4, DocIDCache.ObjType_ImDocTemplate);
        menuItem1.Items.Add((ToolbarItemBase) menuButtonItem1);
        this.CommandManager.Add((ButtonItemBase) menuButtonItem1);
        MenuButtonItem menuButtonItem2 = new MenuButtonItem("Тестовый документ");
        menuButtonItem2.CommandName = "New.TestDocument";
        menuItem1.Items.Add((ToolbarItemBase) menuButtonItem2);
        this.CommandManager.Add((ButtonItemBase) menuButtonItem2);
      }
      MenuItemBase menuItem2 = menuBar1.FindMenuItem("File.SaveAs");
      MenuButtonItem menuItem3 = DocumentMenuHelper.CreateMenuItem("ExportToWMF", LocalizationHolder.rm.GetString("Document.Client_83"), "", true, false, this.CommandManager);
      menuItem3.BeginGroup = false;
      menuBar2.Items.Insert(menuItem2.Index + 1, (ToolbarItemBase) menuItem3);
      MenuItemBase menuBar3 = (MenuItemBase) menuBar1.FindMenuBar("Edit");
      MenuButtonItem menuItem4 = DocumentMenuHelper.GetMenuItem("SelectAll");
      this.AddDocCommandToMenu("FindId", menuBar3);
      this.AddDocCommandToMenu("SelectAll", menuBar3);
      menuItem4.Shortcut = Shortcut.CtrlA;
      menuItem4.ShortcutActive = true;
      this.AddDocCommandToMenu("ClearFormat", menuBar3);
      this.AddDocCommandToMenu("CopySelectedToImage", menuBar3);
      this.AddDocCommandToMenu("CopySelectedToExcel", menuBar3);
      this.AddDocCommandToMenu("LoadOleFile", menuBar3);
      this.AddDocCommandToMenu("CreateOleObject", menuBar3);
      this.AddDocCommandToMenu("CallEditor", menuBar3);
      MenuItemBase menuItem5 = menuBar1.FindMenuItem("Edit.Find");
      if (menuItem5 != null)
      {
        int num1 = menuItem5.Index + 1;
        MenuItemBase.MenuItemCollection items1 = menuBar3.Items;
        int index1 = num1;
        int num2 = index1 + 1;
        MenuButtonItem menuItem6 = DocumentMenuHelper.GetMenuItem("FindNext");
        items1.Insert(index1, (ToolbarItemBase) menuItem6);
        MenuItemBase.MenuItemCollection items2 = menuBar3.Items;
        int index2 = num2;
        int num3 = index2 + 1;
        MenuButtonItem menuItem7 = DocumentMenuHelper.GetMenuItem("Replace");
        items2.Insert(index2, (ToolbarItemBase) menuItem7);
      }
      menuBar3.Items.Add((ToolbarItemBase) DocumentMenuHelper.CreateMenuItem("DocEditor.InsertFormula", LocalizationHolder.rm.GetString("Document.Client_114"), LocalizationHolder.rm.GetString("Document.Client_115"), false, true, this.CommandManager));
      menuBar3.Items.Add((ToolbarItemBase) DocumentMenuHelper.CreateMenuItem("ShowInsertSymbolView", LocalizationHolder.rm.GetString("Document.Client_117"), LocalizationHolder.rm.GetString("Document.Client_118"), false, true, this.CommandManager));
      this.AddDocCommandToMenu("CreateDataField", menuBar3);
      menuBar3.Items.Add((ToolbarItemBase) DocumentMenuHelper.CreateMenuItem("DocEditor.UpdateDocumentLinks", LocalizationHolder.rm.GetString("Document.Client_84"), "", false, false, this.CommandManager));
      this.AddDocCommandToMenu("UpdateFormulas", menuBar3);
      menuBar3.Items.Add((ToolbarItemBase) DocumentMenuHelper.CreateMenuItem("DocEditor.ReplaceTemplate", LocalizationHolder.rm.GetString("Document.Client_85"), "", false, false, this.CommandManager));
      MenuItemBase menuBar4 = (MenuItemBase) menuBar1.FindMenuBar("View");
      if (menuBar4 != null)
      {
        if (menuBar4.Items.Count > 0)
          menuBar4.Items[0].BeginGroup = true;
        int num4 = 0;
        MenuItemBase.MenuItemCollection items3 = menuBar4.Items;
        int index3 = num4;
        int num5 = index3 + 1;
        MenuButtonItem menuItem8 = DocumentMenuHelper.CreateMenuItem("DocEditor.ShowTemplate", LocalizationHolder.rm.GetString("Document.Client_86"), "", true, false, this.CommandManager);
        items3.Insert(index3, (ToolbarItemBase) menuItem8);
        MenuItemBase.MenuItemCollection items4 = menuBar4.Items;
        int index4 = num5;
        int num6 = index4 + 1;
        MenuButtonItem menuItem9 = DocumentMenuHelper.CreateMenuItem("ShowDocumentTreeView", LocalizationHolder.rm.GetString("Document.Client_87"), "", false, false, this.CommandManager);
        items4.Insert(index4, (ToolbarItemBase) menuItem9);
        MenuButtonItem menu1 = new MenuButtonItem(LocalizationHolder.rm.GetString("Document.Client_88"));
        menu1.CommandName = "DocEditor.Zoom";
        this.CommandManager.Add((ButtonItemBase) menu1);
        MenuItemBase.MenuItemCollection items5 = menuBar4.Items;
        int index5 = num6;
        int num7 = index5 + 1;
        MenuButtonItem menuButtonItem3 = menu1;
        items5.Insert(index5, (ToolbarItemBase) menuButtonItem3);
        this.AddDocCommandToMenu("Zoom200", (MenuItemBase) menu1);
        this.AddDocCommandToMenu("Zoom100", (MenuItemBase) menu1);
        this.AddDocCommandToMenu("Zoom75", (MenuItemBase) menu1);
        this.AddDocCommandToMenu("Zoom50", (MenuItemBase) menu1);
        this.AddDocCommandToMenu("ZoomFitWidth", (MenuItemBase) menu1);
        this.AddDocCommandToMenu("ZoomFitPage", (MenuItemBase) menu1);
        MenuButtonItem menu2 = new MenuButtonItem(LocalizationHolder.rm.GetString("Document.Client_89"));
        menu2.CommandName = "DocEditor.GridSize";
        this.CommandManager.Add((ButtonItemBase) menu2);
        MenuItemBase.MenuItemCollection items6 = menuBar4.Items;
        int index6 = num7;
        int num8 = index6 + 1;
        MenuButtonItem menuButtonItem4 = menu2;
        items6.Insert(index6, (ToolbarItemBase) menuButtonItem4);
        this.AddDocCommandToMenu("Doc.GridSize_1", (MenuItemBase) menu2);
        this.AddDocCommandToMenu("Doc.GridSize_0.5", (MenuItemBase) menu2);
        this.AddDocCommandToMenu("Doc.GridSize_0.1", (MenuItemBase) menu2);
        this.AddDocCommandToMenu("Doc.GridSize_0.05", (MenuItemBase) menu2);
        MenuButtonItem menu3 = new MenuButtonItem(LocalizationHolder.rm.GetString("Document.Client_90"));
        menu3.CommandName = "DocEditor.CoorSystem";
        this.CommandManager.Add((ButtonItemBase) menu3);
        MenuItemBase.MenuItemCollection items7 = menuBar4.Items;
        int index7 = num8;
        int num9 = index7 + 1;
        MenuButtonItem menuButtonItem5 = menu3;
        items7.Insert(index7, (ToolbarItemBase) menuButtonItem5);
        this.AddDocCommandToMenu("Doc.CoorSystem_BottomLeft", (MenuItemBase) menu3);
        this.AddDocCommandToMenu("Doc.CoorSystem_TopLeft", (MenuItemBase) menu3);
        this.AddDocCommandToMenu("Doc.CoorSystem_TopRight", (MenuItemBase) menu3);
        this.AddDocCommandToMenu("Doc.CoorSystem_BottomRight", (MenuItemBase) menu3);
        this.AddDocCommandToMenu("Doc.CoorSystem_Custom", (MenuItemBase) menu3);
      }
      if (ServicesManager.GetService(typeof (IMainMenuService)) is IMainMenuService service10)
      {
        MenuButtonItem[] menuButtonItemArray = new MenuButtonItem[1]
        {
          DocumentMenuHelper.CreateMenuItem("DocElementProperty", LocalizationHolder.rm.GetString("Document.Client_91"), "", false, true, this.CommandManager)
        };
        service10.RegisterMenuItems(MainMenuItemSite.ViewBottom, MainMenuItemPosition.Default, menuButtonItemArray);
      }
      this.pageElementsMenu = new MenuBarItem(LocalizationHolder.rm.GetString("Document.Client_92"));
      this.pageElementsMenu.CommandName = "DocEditor.PageElementsMenu";
      this.pageElementsMenu.Visible = false;
      this.CommandManager.Add((ButtonItemBase) this.pageElementsMenu);
      int index8 = menuBar1.Items.Count;
      MenuItemBase menuBar5 = (MenuItemBase) menuBar1.FindMenuBar("Windows");
      if (menuBar5 != null)
        index8 = menuBar5.Index;
      menuBar1.Items.Insert(index8, (ToolbarItemBase) this.pageElementsMenu);
      this.selectElementMenuItem = new MenuButtonItem(LocalizationHolder.rm.GetString("Document.Client_93"));
      this.selectElementMenuItem.CommandName = "SelectPageElement";
      this.pageElementsMenu.Items.Add((ToolbarItemBase) this.selectElementMenuItem);
      this.LoadPageElementCreators(typeof (ImDocument).Assembly);
      this.AddDocCommandToMenu("ConvertToLabel", (MenuItemBase) this.pageElementsMenu);
      this.AddDocCommandToMenu("ConvertToTextBox", (MenuItemBase) this.pageElementsMenu);
      this.AddDocCommandToMenu("ConvertToContainer", (MenuItemBase) this.pageElementsMenu);
      this.AddDocCommandToMenu("ConvertToArea", (MenuItemBase) this.pageElementsMenu);
      MenuBarItem menu4 = new MenuBarItem(LocalizationHolder.rm.GetString("Document.Client_97"));
      menu4.CommandName = "DocEditor.Table";
      this.CommandManager.Add((ButtonItemBase) menu4);
      int index9 = menuBar1.Items.Count;
      if (menuBar5 != null)
        index9 = menuBar5.Index;
      menuBar1.Items.Insert(index9, (ToolbarItemBase) menu4);
      MenuBarItem menu5 = new MenuBarItem(LocalizationHolder.rm.GetString("Document.Client_98"));
      menu5.CommandName = "FormatMenuItem";
      this.CommandManager.Add((ButtonItemBase) menu5);
      int index10 = menuBar5 == null ? menuBar1.Items.Count : menuBar4.Index + 1;
      menuBar1.Items.Insert(index10, (ToolbarItemBase) menu5);
      this.AddDocCommandToMenu("RemoveRow", (MenuItemBase) menu4);
      this.AddDocCommandToMenu("RemoveColumn", (MenuItemBase) menu4);
      this.AddDocCommandToMenu("RemoveCell", (MenuItemBase) menu4);
      this.AddDocCommandToMenu("Format.Font.SetupFont", (MenuItemBase) menu5);
      this.AddDocCommandToMenu("Format.SetupParagraph", (MenuItemBase) menu5);
      this.AddDocCommandToMenu("Format.SetupTextDirrection", (MenuItemBase) menu5);
      this.AddDocCommandToMenu("AddTableRowAbove", (MenuItemBase) menu4);
      this.AddDocCommandToMenu("AddTableRowBelow", (MenuItemBase) menu4);
      this.AddDocCommandToMenu("AddRowFromTemplateAbove", (MenuItemBase) menu4);
      this.AddDocCommandToMenu("AddRowFromTemplateBelow", (MenuItemBase) menu4);
      this.AddDocCommandToMenu("AddTableColumnLeft", (MenuItemBase) menu4);
      this.AddDocCommandToMenu("AddTableColumnRight", (MenuItemBase) menu4);
      this.AddDocCommandToMenu("AddTableCell", (MenuItemBase) menu4);
      this.AddDocCommandToMenu("SplitCell", (MenuItemBase) menu4);
      this.AddDocCommandToMenu("MergeCells", (MenuItemBase) menu4);
      this.AddDocCommandToMenu("ConvertToHeader", (MenuItemBase) menu4);
      this.AddDocCommandToMenu("UpdateTable", (MenuItemBase) menu4);
      this.AddDocCommandToMenu("SelectContinuationTable", (MenuItemBase) menu4);
      this.AddDocCommandToMenu("ApplyPreviousTable", (MenuItemBase) menu4);
      MenuBarItem menu6 = new MenuBarItem(LocalizationHolder.rm.GetString("Document.Client_99"));
      menu6.CommandName = "DocEditor.Page";
      this.CommandManager.Add((ButtonItemBase) menu6);
      menuBar1.Items.Insert(index10 + 1, (ToolbarItemBase) menu6);
      this.AddDocCommandToMenu("NewPageBefore", (MenuItemBase) menu6);
      this.AddDocCommandToMenu("NewPageAfter", (MenuItemBase) menu6);
      this.AddDocCommandToMenu("CreateNextPageTemplate", (MenuItemBase) menu6);
      this.AddDocCommandToMenu("RemovePage", (MenuItemBase) menu6);
      this.AddDocCommandToMenu("PrevPage", (MenuItemBase) menu6);
      this.AddDocCommandToMenu("NextPage", (MenuItemBase) menu6);
      menu6.Items.Add((ToolbarItemBase) DocumentMenuHelper.CreateMenuItem("AVS.ChangePageNumberingStyle", LocalizationHolder.rm.GetString("Document.Client_170"), LocalizationHolder.rm.GetString("Document.Client_170"), false, true, this.CommandManager));
      MenuItemBase menuItem10 = menuBar1.FindMenuItem("File.ParametersCard");
      if (menuItem10 != null)
        DocumentMenuHelper.CreateMenuItem("ParametersCard1", menuItem10.Text, menuItem10.ToolTipText, menuItem10.Image, false, true, this.CommandManager);
      if (service1 != null)
      {
        MenuTemplateNode menuTemplateNode = service1.ContextMenuTemplate["OpenInNewWindow"];
        if (menuTemplateNode != null)
        {
          Image img = (Image) null;
          if (menuTemplateNode.ImageIndex >= 0)
            img = DocumentEditorPlugin.imageList.Images[menuTemplateNode.ImageIndex];
          MenuButtonItem menuItem11 = DocumentMenuHelper.CreateMenuItem("DocEditor.OpenInNewWindow", menuTemplateNode.Text + "        Ctrl+Enter", "", img, false, true, DocumentEditorPlugin.commandManager);
          if (menuItem11 != null)
          {
            menuItem11.Shortcut = (Shortcut) 131085 /*0x02000D*/;
            menuBar3.Items.Add((ToolbarItemBase) menuItem11);
          }
        }
      }
      this.CommandManager.AddTarget((ICommandTarget) this);
    }
    if (DocumentEditorPlugin.NotificationService != null)
    {
      DocumentEditorPlugin.NotificationService.Subscribe("ObjectsRemoved", new NotificationEventHandler(this.ObjectsWasRemovedHandler));
      DocumentEditorPlugin.NotificationService.Subscribe("ObjectsChanged", new NotificationEventHandler(this.ObjectsWasChangedHandler));
      DocumentEditorPlugin.NotificationService.Subscribe("ObjectsChangesCancelled", new NotificationEventHandler(this.ObjectChangesWasCanceledHandler));
      DocumentEditorPlugin.NotificationService.Subscribe("RelationsChanged", new NotificationEventHandler(this.RelationsWasChangedHandler));
      DocumentEditorPlugin.NotificationService.Subscribe("RelationsRemoved", new NotificationEventHandler(this.RelationsWasRemovedHandler));
      DocumentEditorPlugin.NotificationService.Subscribe("ApplicationClosed", new NotificationEventHandler(this.ApplicationClosedHandler));
    }
    IContentProvider service11 = serviceProvider.GetService<IContentProvider>(false);
    if (service11 != null)
      service11.ContentCallback += new GetContentCallback(this.RestoreDocumentWindow);
    ServicesManager.AddService(typeof (IDocumentConverter), (object) new DocumentConverterService());
    ServicesManager.AddService(typeof (IIMDocumentEditorService), (object) new IMDocumentEditorService());
    ServicesManager.AddService(typeof (IImRtfViewService), (object) new ImRtfViewService());
    if (ServicesManager.GetService(typeof (IAuthFilesService)) is IAuthFilesService service12)
    {
      service12.AuthFileAssignEvent += new AuthFileAssignEventHandler(this.iAuthFilesService_AuthFileAssignEvent);
      service12.AuthFileNeedGenerate += new AuthFileNeedGenerateEventHandler(this.IAuthFilesService_AuthFileNeedGenerate);
    }
    this.RegisterViews();
    FilesEditor.Instance = (FilesEditor) new ImClientFilesEditor(serviceProvider);
    this.GetAllDocumentSuffixs();
    AdditionalPropertiesManager.Instance.GetAdditionalProperties += new GetAdditionalProperties_EventHandler(AdditionalPropertiesManagerHelper.Instance_GetAdditionalProperties);
    ApplicationServices.Container.AddService(typeof (IDocumentEditorPluginScriptService), (object) new DocumentEditorPluginScriptService());
    ImDocumentData.NotifyService = (IDocumentNotifyService) this;
    if (!(DocumentEditorPlugin.ICompareFilesService is CompareFilesService icompareFilesService))
      return;
    DocumentEditorPlugin._compPlugin = new ImDocumentFilesComparisonPlugin()
    {
      CompareService = icompareFilesService
    };
    DocumentEditorPlugin.imDocEditorSettingsCache = DocumentEditorPlugin.GetDocumentEditorToolSettings();
    List<int> typeIds = new List<int>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IMDocEditorToolSettings editorSettingsCache = DocumentEditorPlugin.imDocEditorSettingsCache;
      typeIds = (editorSettingsCache != null ? editorSettingsCache.SupportedTypeIDs.Concat<int>(AVSDocumentsSettings.Instance.GetDBObjectTypesForAllAVSDocuments(sessionKeeper.Session).Select<Guid, int>(new System.Func<Guid, int>(MetaDataHelper.GetObjectTypeID))).Distinct<int>().ToList<int>() : (List<int>) null) ?? new List<int>(0);
    }
    DocumentEditorPlugin._compPlugin.SetTypeIds(typeIds);
    icompareFilesService.AddPluginToCompareFilesService((ICanCompareObjectsFiles) DocumentEditorPlugin._compPlugin);
  }

  public void FileService_FileProcessEvent(object sender, FileProcessEventArgs eventArgs)
  {
    if (string.IsNullOrEmpty(eventArgs.BlobInformation.FileName) || !ImDocumentData.IsImDocumentExtension(ImDocumentData.GetFileExtensionWithoutDot(eventArgs.BlobInformation.FileName)))
      return;
    VersionsRulePackage versionsRule = eventArgs.LaunchType == LaunchType.Edit ? VersionsRuleSources.GetEditorRule() : VersionsRuleSources.GetCurrentWindowRule();
    LaunchParams launchParams = new LaunchParams(eventArgs.LaunchType, eventArgs.ObjectId, eventArgs.ObjectType, versionsRule);
    launchParams.LaunchContext.Put<int>("FileAttributeID", eventArgs.AttributeId);
    launchParams.LaunchContext.Put<int>("FileIndex", eventArgs.ValueIndex);
    ILaunchHandler launchHandler = (ILaunchHandler) null;
    LaunchActionInfo actionInfo = (LaunchActionInfo) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttribute documentFileAttribute = DocumentEditorPluginBase.FindDocumentFileAttribute(sessionKeeper.Session.GetObject(eventArgs.ObjectId, true), -1);
      if (documentFileAttribute != null)
      {
        if (documentFileAttribute.AttributeID == eventArgs.AttributeId)
        {
          if (documentFileAttribute.Index == eventArgs.ValueIndex)
            launchHandler = DocumentEditorPlugin.FindSpecialDocumentEditorLaunchHandler(launchParams, sessionKeeper.Session, out actionInfo);
        }
      }
    }
    if (launchHandler != null)
    {
      ILaunchActionService service = ServiceUtils.GetService<ILaunchActionService>((object) ApplicationServices.Container, false);
      if (service != null)
      {
        service.Launch(launchParams, actionInfo);
        eventArgs.IsHandled = true;
        return;
      }
    }
    if (eventArgs.LaunchType == LaunchType.Print)
    {
      this.PrintImDocumentObject(eventArgs.ObjectId, eventArgs.AttributeId, eventArgs.ValueIndex);
      eventArgs.IsHandled = true;
    }
    else
    {
      if (eventArgs.LaunchType != LaunchType.Edit && eventArgs.LaunchType != LaunchType.View)
        return;
      bool readOnly = eventArgs.LaunchType != 0;
      ImDocumentEditorForm documentEditorForm = this.OpenDocumentImDocumentObject(DocumentEditorPlugin.TryCheckOutDocument(eventArgs.ObjectId, ref readOnly), eventArgs.AttributeId, eventArgs.ValueIndex, readOnly, true);
      if (documentEditorForm == null)
        return;
      documentEditorForm.DefaultFileName = eventArgs.BlobInformation.FileName;
      eventArgs.IsHandled = true;
    }
  }

  public static ILaunchHandler FindSpecialDocumentEditorLaunchHandler(
    LaunchParams launchParams,
    IUserSession session,
    out LaunchActionInfo actionInfo)
  {
    actionInfo = (LaunchActionInfo) null;
    if (DocumentEditorPlugin.Instance.SpecialDocumentLaunchHandlers.IsEmpty<Guid>())
      return (ILaunchHandler) null;
    ILaunchActionServer service1 = ServiceUtils.GetService<ILaunchActionServer>((object) session, true);
    ICurrentUserAndRole service2 = ServicesManager.GetService<ICurrentUserAndRole>();
    Guid objectTypeGuid = MetaDataHelper.GetObjectTypeGuid(launchParams.ObjectTypeId);
    UserTarget userTarget = new UserTarget(service2.UserID, service2.UserGuid);
    int launchType = (int) launchParams.LaunchType;
    List<LaunchActionInfo> source = service1.LookupActionList(objectTypeGuid, (ITarget) userTarget, (LaunchType) launchType);
    actionInfo = source.FirstOrDefault<LaunchActionInfo>((System.Func<LaunchActionInfo, bool>) (a => DocumentEditorPlugin.Instance.SpecialDocumentLaunchHandlers.Contains(a.HandlerId)));
    return actionInfo != null ? ClientContext.LaunchActions.GetHandler(actionInfo.HandlerId, false) : (ILaunchHandler) null;
  }

  /// <summary>
  /// Получить список суффиксов всех документов для составных обозначений
  /// </summary>
  private void GetAllDocumentSuffixs()
  {
    ImDocumentData.ComplexDesignationSuffixs = ((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IDocumentTypeSettingsService)) as IDocumentTypeSettingsService).GetDocSuffixes();
  }

  private void IAuthFilesService_AuthFileNeedGenerate(
    object sender,
    AuthFileNeedGenerateEventArgs eventArgs)
  {
    int existIndex = -1;
    int mainIndex = -1;
    new ImDocumentAuthFileGenerator().NeedGenerate(eventArgs, out mainIndex, out existIndex);
  }

  private void iAuthFilesService_AuthFileAssignEvent(
    object sender,
    AuthFileAssignEventArgs eventArgs)
  {
    IAVSClientService service = ServicesManager.GetService<IAVSClientService>(false);
    if (service != null && service.IsAVSDocumentSupportedType(eventArgs.ObjectType))
      return;
    new ImDocumentAuthFileGenerator().Generate(eventArgs);
  }

  private void dockManager_DockControlActivated(object sender, DockControlEventArgs e)
  {
    try
    {
      if (this.CommandManager == null)
        return;
      this.CommandManager.QueryStatus();
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  public ISelectionDialogTab sdTabService_SelectionDialogTabEvent(
    object sender,
    SelectionDialogTabEventArgs e)
  {
    return (ISelectionDialogTab) new SelectionTabControl();
  }

  private void SaveChanges_After(object sender, AfterObjectCommandArgs e)
  {
    try
    {
      foreach (DockControl document in DocumentEditorPlugin.DockManager.DocumentContainer.Documents)
      {
        if (document is ImDocumentEditorForm && (document as ImDocumentEditorForm).DocumentID == e.ObjectId)
          (document as ImDocumentEditorForm).SaveDocument();
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  /// <summary>Выгрузить плагин. Пока не востребована</summary>
  public void Unload()
  {
    ServiceUtils.GetService<IObjectCreatorService>((object) ServicesManager.ServiceContainer, true).AfterDraftCreatedEvent -= new AfterDraftCreatedEventHandler(this.ObjectCreatorDraftCreated);
    if (!(DocumentEditorPlugin.ICompareFilesService is CompareFilesService icompareFilesService))
      return;
    icompareFilesService.DeletePluginFromCompareFilesService((ICanCompareObjectsFiles) DocumentEditorPlugin._compPlugin);
  }

  /// <summary>Обработчик события, возникающего после создания заготовки нового объекта</summary>
  /// <param name="e">Аргументы события</param>
  private void ObjectCreatorDraftCreated(object sender, AfterDraftCreatedEventArgs e)
  {
    if (e is AfterPrototypedDraftCreatedEventArgs createdEventArgs && createdEventArgs.PrototypeID != -1L)
      return;
    try
    {
      long fromImDocSettings = DocumentEditorPlugin.GetDocumentTemplateIDFromIMDocSettings(e.ObjectTypeID);
      switch (fromImDocSettings)
      {
        case -1:
          break;
        case 0:
          break;
        default:
          ImDocument document = new ImDocument(DocumentEditorPlugin.LoadDocumentFromDBObject(fromImDocSettings), true, true);
          long objectId = e.ObjectID;
          DocumentEditorPlugin.SaveImDocumentObjectFile(objectId, document, "document.imdx", 0, true);
          e.ObjectID = objectId;
          break;
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  /// <summary>Обработка события обновления объектов </summary>
  public void ObjectsWasChangedHandler(object sender, NotificationEventArgs e)
  {
    try
    {
      if (!(e is DBObjectsEventArgs objectsEventArgs) || objectsEventArgs.ObjectIDs == null || objectsEventArgs.ObjectIDs.Count <= 0)
        return;
      List<Guid> changedObjectsGuids = new List<Guid>();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        foreach (long objectId in (IEnumerable<long>) objectsEventArgs.ObjectIDs)
        {
          QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(objectId);
          changedObjectsGuids.Add(objectInfo.VersionGuid);
        }
      }
      foreach (DockControl document1 in DocumentEditorPlugin.DockManager.DocumentContainer.Documents)
      {
        if (document1 is ImDocumentEditorForm documentEditorForm && documentEditorForm.UpdateReferenceByNotificationService)
        {
          if (documentEditorForm.DocumentsComplect != null)
          {
            foreach (ImDocumentData document2 in documentEditorForm.DocumentsComplect)
              this.UpdateLinksInDocument(document2, (IList<Guid>) changedObjectsGuids);
          }
          else if (documentEditorForm.Document != null)
            this.UpdateLinksInDocument((ImDocumentData) documentEditorForm.Document, (IList<Guid>) changedObjectsGuids);
        }
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  private void UpdateLinksInDocument(ImDocumentData document, IList<Guid> changedObjectsGuids)
  {
    if (document == null)
      throw new ArgumentNullException(nameof (document));
    bool flag1 = false;
    foreach (Guid changedObjectsGuid in (IEnumerable<Guid>) changedObjectsGuids)
    {
      Dictionary<Guid, AttributeValueCache> dictionary;
      if (document.ObjAttrCache != null && document.ObjAttrCache.TryGetValue(changedObjectsGuid, out dictionary))
      {
        foreach (KeyValuePair<Guid, AttributeValueCache> keyValuePair in dictionary)
        {
          if (keyValuePair.Value != null)
          {
            bool flag2 = true;
            foreach (INodeWithReference referenceOwner in keyValuePair.Value.ReferenceOwnerList)
            {
              if (referenceOwner != null && referenceOwner.Reference != null)
              {
                if (referenceOwner.Reference is ReferenceToDBObjectAttribute reference)
                {
                  flag1 = true;
                  if (flag2)
                  {
                    using (SessionKeeper sessionKeeper = new SessionKeeper())
                    {
                      if (reference.UseLinkAttribute)
                        reference.GetParentDBObjectInfo(sessionKeeper.Session);
                      reference.UpdateAttributeValue(sessionKeeper.Session, false, true, false, false);
                    }
                    keyValuePair.Value.Value = (object) reference.Text;
                  }
                  else
                    reference.SetText(keyValuePair.Value.Value != null ? keyValuePair.Value.Value.ToString() : (string) null, false, true, false, false, false);
                }
                flag2 = false;
              }
            }
          }
        }
      }
    }
    if (!flag1 || !document.NeedUpdateLayoutFlag)
      return;
    document.UpdateLayout(0, false, true, true, false);
  }

  /// <summary>Изменения объекта были отменены </summary>
  public void ObjectChangesWasCanceledHandler(object sender, NotificationEventArgs e)
  {
    this.ObjectsWasChangedHandler(sender, e);
  }

  /// <summary>Обработка события обновления связей </summary>
  public void RelationsWasChangedHandler(object sender, NotificationEventArgs e)
  {
  }

  /// <summary>Обработка события удаления связей </summary>
  public void RelationsWasRemovedHandler(object sender, NotificationEventArgs e)
  {
  }

  /// <summary> Обработка события удаления объектов </summary>
  public void ObjectsWasRemovedHandler(object sender, NotificationEventArgs e)
  {
    try
    {
      if (DocumentEditorPlugin.DockManager == null || DocumentEditorPlugin.DockManager.DocumentContainer == null || !(e is DBObjectsEventArgs objectsEventArgs) || objectsEventArgs.ObjectIDs == null || objectsEventArgs.ObjectIDs.Count <= 0)
        return;
      for (int index1 = 0; index1 < DocumentEditorPlugin.DockManager.DocumentContainer.Documents.Length; ++index1)
      {
        if (DocumentEditorPlugin.DockManager.DocumentContainer.Documents[index1] is ImDocumentEditorForm document && document.UpdateReferenceByNotificationService)
        {
          if (objectsEventArgs.ObjectIDs.IndexOf(document.DocumentID) != -1)
          {
            document.Close();
          }
          else
          {
            using (SessionKeeper sessionKeeper = new SessionKeeper())
            {
              for (int index2 = 0; index2 < objectsEventArgs.ObjectIDs.Count; ++index2)
              {
                QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(objectsEventArgs.ObjectIDs[index2]);
                if (document.Document != null)
                {
                  Dictionary<Guid, AttributeValueCache> dictionary;
                  if (document.Document.ObjAttrCache.TryGetValue(objectInfo.VersionGuid, out dictionary) && dictionary != null)
                  {
                    foreach (KeyValuePair<Guid, AttributeValueCache> keyValuePair in dictionary)
                    {
                      for (int index3 = 0; index3 < keyValuePair.Value.ReferenceOwnerList.Count; ++index3)
                      {
                        ReferenceBase reference = keyValuePair.Value.ReferenceOwnerList[index3].Reference;
                        if (reference != null)
                        {
                          reference.DisconnectLink();
                          if (reference is ITextSource textSource)
                            textSource.SetText((string) null, false, false, false);
                        }
                      }
                      keyValuePair.Value.ReferenceOwnerList.Clear();
                    }
                  }
                  lock (document.Document.ObjAttrCache)
                    document.Document.ObjAttrCache.Remove(objectInfo.VersionGuid);
                }
              }
            }
            document.Document?.UpdateLayout(0, false, true);
          }
        }
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  /// <summary> Обработка события завершения работы плагина </summary>
  private void ApplicationClosedHandler(object sender, NotificationEventArgs e)
  {
  }

  /// <summary>Загрузить конфигурацию плагина</summary>
  /// <param name="configurationManager">Менеджер конфигураций</param>
  public void LoadConfiguration(IConfigurationManager configurationManager)
  {
    try
    {
      this.configManager = configurationManager;
      this.EditorConfig.LoadConfiguration(configurationManager);
      this.EditorConfig.LoadFromBase();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        this.ImDocumentConfig.LoadConfiguration(sessionKeeper.Session);
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  private Intermech.Bars.ToolBar GetToolbarByGuid(Guid guid) => (Intermech.Bars.ToolBar) null;

  /// <summary>Сохранить конфигурацию плагина</summary>
  /// <param name="configurationManager">Менеджер конфигураций</param>
  public void SaveConfiguration(IConfigurationManager configurationManager)
  {
    try
    {
      this.EditorConfig.SaveConfiguration(configurationManager);
      this.EditorConfig.SaveToBase();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        this.ImDocumentConfig.SaveConfiguration(sessionKeeper.Session);
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  /// <summary>Менеджер команд клиента</summary>
  [Browsable(false)]
  public ICommandManager CommandManager
  {
    get
    {
      if (DocumentEditorPlugin.commandManager == null)
        DocumentEditorPlugin.commandManager = (ICommandManager) ServicesManager.GetService(typeof (ICommandManager));
      return DocumentEditorPlugin.commandManager;
    }
  }

  /// <summary>Обработчик изменения выделения в документе</summary>
  public void SelectionChanged()
  {
    PropertyGridForm propertyGridForm = (PropertyGridForm) null;
    if (DocumentEditorPlugin.ActiveImDocumentEditorForm != null)
      propertyGridForm = DocumentEditorPlugin.ActiveImDocumentEditorForm.PropertyGridDlg;
    if (this.CommandManager != null)
      this.CommandManager.QueryStatus();
    if (propertyGridForm == null)
      return;
    DocumentControl imDocumentControl = DocumentEditorPlugin.ActiveImDocumentControl;
    if (imDocumentControl == null)
      return;
    propertyGridForm.SelectedObjects = (object[]) imDocumentControl.SelectedNodes.ToArray();
  }

  /// <summary>Включен режим выбора элементов. Если имеет значение true,
  /// то IsElementCreating не может иметь значение true</summary>
  [Browsable(false)]
  public bool IsElementSelecting
  {
    get => this.isElementSelecting;
    set
    {
      if (this.isElementSelecting == value)
        return;
      this.AssignIsElementSelecting(value);
      if (this.CommandManager == null)
        return;
      this.CommandManager.QueryStatus();
    }
  }

  private void AssignIsElementSelecting(bool value)
  {
    if (this.isElementSelecting == value)
      return;
    this.isElementSelecting = value;
    this.selectElementCommand.Checked = value;
    this.AssignIsElementCreating(!value);
  }

  /// <summary>Включен режим создания элементов. Если имеет значение true,
  /// то IsElementSelecting не может иметь значение true</summary>
  [Browsable(false)]
  public bool IsElementCreating
  {
    get => this.isElementCreating;
    set
    {
      if (this.isElementCreating == value)
        return;
      this.AssignIsElementCreating(value);
      if (this.CommandManager == null)
        return;
      this.CommandManager.QueryStatus();
    }
  }

  private void AssignIsElementCreating(bool value)
  {
    if (this.isElementCreating == value)
      return;
    if (DocumentEditorPlugin.ActiveImDocumentControl != null && DocumentEditorPlugin.ActiveImDocumentControl.Document != null && DocumentEditorPlugin.ActiveImDocumentControl.Document.UndoManager != null)
    {
      if (value)
        DocumentEditorPlugin.ActiveImDocumentControl.Document.UndoManager.LockUndo();
      else
        DocumentEditorPlugin.ActiveImDocumentControl.Document.UndoManager.UnlockUndo();
    }
    this.isElementCreating = value;
    this.AssignIsElementSelecting(!value);
    if (this.isElementCreating)
      return;
    this.SelectedElementCreator = (PageElementCreator) null;
  }

  /// <summary>Объект управляющий созданием элемента</summary>
  [Browsable(false)]
  public PageElementCreator SelectedElementCreator
  {
    [DebuggerStepThrough] get => this.selectedElementCreator;
    set
    {
      if (this.selectedElementCreator == value)
        return;
      if (this.selectedElementCreator != null)
        this.selectedElementCreator.Reset();
      this.selectedElementCreator = value;
      this.IsElementCreating = this.selectedElementCreator != null;
      for (int index = 0; index < this.elementCreatorCommands.Count; ++index)
        (this.elementCreatorCommands[index] as ICommandState).Checked = this.selectedElementCreator != null && this.elementCreators[index] == this.selectedElementCreator;
    }
  }

  /// <summary>Обновить информаци об выбранных элементах</summary>
  public void UpdateSelectedElementInfo()
  {
    PropertyGridForm propertyGridForm = (PropertyGridForm) null;
    if (DocumentEditorPlugin.ActiveImDocumentEditorForm != null)
      propertyGridForm = DocumentEditorPlugin.ActiveImDocumentEditorForm.PropertyGridDlg;
    if (propertyGridForm == null)
      return;
    propertyGridForm.SelectedObjects = propertyGridForm.SelectedObjects;
  }

  /// <summary>Установить строку сообщения (например в строке статуса)</summary>
  /// <param name="text">Текст сообщения</param>
  public void SetMessageText(string text)
  {
    if (this.statusBar == null || this.statusBar.StatusBar == null || this.statusBar.StatusBar.Panels.Count <= 0)
      return;
    this.statusBar.StatusBar.Panels[0].Text = text;
  }

  /// <summary>Обновить информацию о количестве страниц и текущей странице</summary>
  public void UpdatePagesInfo()
  {
    if (DocumentEditorPlugin.ActiveImDocumentEditorForm == null)
      return;
    DocumentEditorPlugin.ActiveImDocumentEditorForm.UpdateSBPagePanel();
  }

  /// <summary>Диалог сохранения документа в файл на диске</summary>
  [Browsable(false)]
  public SaveFileDialog SaveToFileDialog
  {
    [DebuggerStepThrough] get
    {
      if (this.saveToFileDialog == null)
        this.saveToFileDialog = ImDocumentEditorFormBase.CreateSaveFileDialog();
      return this.saveToFileDialog;
    }
  }

  /// <summary>Последнее путь использовавшийся при сохранении как</summary>
  [Browsable(false)]
  public string RecentlySaveAsPath
  {
    get => this.recentlySaveAsPath;
    set => this.recentlySaveAsPath = value;
  }

  /// <summary>Обновить меню и инструменты форматирования</summary>
  public void UpdateFormatCommands()
  {
    if (DocumentEditorPlugin.ActiveImDocumentEditorForm == null)
      return;
    DocumentEditorPlugin.ActiveImDocumentEditorForm.UpdateFormatCommands();
  }

  internal System.IServiceProvider ServiceProvider
  {
    get => this.serviceProvider;
    set => this.serviceProvider = value;
  }

  /// <summary>Конфигурация редактора</summary>
  public ImDocumentEditorConfig EditorConfig
  {
    [DebuggerStepThrough] get => ImDocumentEditorConfig.Instance;
  }

  /// <summary>Конфигурация редактора</summary>
  public ImDocumentClientConfig ImDocumentConfig
  {
    [DebuggerStepThrough] get => ImDocumentClientConfig.Instance;
  }

  /// <summary>Отобразить информацию о возникшей исключительной ситуации (Exception)</summary>
  /// <param name="e">Возникшее исключение</param>
  /// <returns>Тип нажатой в окне кнопки</returns>
  public void ShowExceptionDialog(Exception e) => ExceptionHelper.ExceptionService.ShowException(e);

  public IConfigurationManager ConfigurationManager => this.configManager;

  /// <summary>Выполнить комманду</summary>
  /// <param name="commandState">Данные команды</param>
  /// <returns>true, если команда найдена</returns>
  public bool Execute(ICommandState commandState)
  {
    if (commandState == null)
      return false;
    try
    {
      switch (commandState.CommandName)
      {
        case "New.ImDocument":
          this.CreateNewDocument();
          return true;
        case "New.TestDocument":
          DocGenerateSample.GenerateAndSaveNewTestDocument();
          return true;
        default:
          if (DocumentEditorPlugin.ActiveImDocumentEditorForm == null)
            return false;
          int index = this.elementCreatorCommands.IndexOf((object) commandState);
          if (index >= 0)
          {
            this.SelectedElementCreator = this.elementCreators[index] as PageElementCreator;
            return true;
          }
          switch (commandState.CommandName)
          {
            case "SelectPageElement":
              this.IsElementSelecting = true;
              return true;
            case "DocEditor.ShowTemplate":
              this.ShowDocumentTemplate(DocumentEditorPlugin.ActiveImDocumentEditorForm, true);
              return true;
          }
          break;
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
      return true;
    }
    return false;
  }

  /// <summary>Проверить статус комманды</summary>
  /// <param name="commandState">Состояние комманды</param>
  /// <returns>true, если команда найдена</returns>
  public bool QueryStatus(ICommandState commandState)
  {
    if (commandState == null)
      return false;
    try
    {
      bool flag1 = DocumentEditorPlugin.ActiveImDocumentEditorForm != null && DocumentEditorPlugin.DockManager.ActiveDockControl == DocumentEditorPlugin.ActiveImDocumentEditorForm;
      bool flag2 = DocumentEditorPlugin.ActiveImDocumentEditorForm != null && DocumentEditorPlugin.ActiveImDocumentEditorForm.GetType().Name == "ImDocumentEditorForm";
      bool flag3 = false;
      if (DocumentEditorPlugin.ActiveImDocumentEditorForm != null && DocumentEditorPlugin.ActiveImDocumentEditorForm.DocumentControl != null)
        flag3 = DocumentEditorPlugin.ActiveImDocumentEditorForm.DocumentControl.ReadOnly;
      int num = -1;
      if (this.elementCreatorCommands != null)
        num = this.elementCreatorCommands.IndexOf((object) commandState);
      if (num >= 0)
      {
        commandState.Enabled = !flag3 & flag1 && DocumentEditorPlugin.ActiveImDocumentEditorForm.BaseEditCommandsEnabled;
        return true;
      }
      string commandName = commandState.CommandName;
      // ISSUE: reference to a compiler-generated method
      switch (\u003CPrivateImplementationDetails\u003E.ComputeStringHash(commandName))
      {
        case 189178071:
          if (commandName == "UpdateFormulas")
            goto label_52;
          goto label_57;
        case 433041371:
          if (commandName == "DocEditor.InsertFormula")
            goto label_52;
          goto label_57;
        case 779179766:
          if (commandName == "FormatMenuItem")
            goto label_52;
          goto label_57;
        case 786084307:
          if (commandName == "DocEditor.GridSize")
          {
            commandState.Visible = flag1 && (flag2 || ImDocumentData.ShowDebugInfo);
            commandState.Enabled = true;
            return true;
          }
          goto label_57;
        case 787014169:
          if (commandName == "DocEditor.Page")
            break;
          goto label_57;
        case 811250246:
          if (commandName == "New.ImDocument")
          {
            commandState.Enabled = true;
            return true;
          }
          goto label_57;
        case 830761660:
          if (commandName == "ExportToWMF")
            goto label_52;
          goto label_57;
        case 983916780:
          if (commandName == "New.TestDocument")
          {
            commandState.Enabled = true;
            commandState.Visible = true;
            return true;
          }
          goto label_57;
        case 1054987743:
          if (commandName == "DocEditor.PageElementsMenu")
            break;
          goto label_57;
        case 1130138585:
          if (commandName == "CopySelectedToImage")
            goto label_53;
          goto label_57;
        case 1307176919:
          if (commandName == "FindId")
            goto label_52;
          goto label_57;
        case 1365312854:
          if (commandName == "SelectPageElement")
            goto label_52;
          goto label_57;
        case 1412139749:
          if (commandName == "DocEditor.ShowTemplate")
          {
            commandState.Visible = flag1 && (DocumentEditorPlugin.ActiveImDocumentEditorForm.BaseEditCommandsEnabled || ImDocumentData.ShowDebugInfo);
            commandState.Enabled = flag1 && DocumentEditorPlugin.ActiveImDocumentEditorForm.Document != null && !DocumentEditorPlugin.ActiveImDocumentEditorForm.Document.IsTemplate;
            return true;
          }
          goto label_57;
        case 1766802964:
          if (commandName == "UnblockGeometryChanging")
            break;
          goto label_57;
        case 1794417565:
          if (commandName == "ChangeVisibility")
            goto label_52;
          goto label_57;
        case 1846442223:
          if (commandName == "CreateDataField")
            break;
          goto label_57;
        case 2108504613:
          if (commandName == "BlockGeometryChanging")
            break;
          goto label_57;
        case 2172974414:
          if (commandName == "CreateOleObject")
            goto label_52;
          goto label_57;
        case 2179741120:
          if (commandName == "DocEditor.ReplaceTemplate")
            break;
          goto label_57;
        case 2200802204:
          if (commandName == "SelectAll")
            goto label_53;
          goto label_57;
        case 2274244545:
          if (commandName == "ClearFormat")
            goto label_52;
          goto label_57;
        case 2321908792:
          if (commandName == "DocEditor.CoorSystem")
          {
            commandState.Visible = flag1 && (flag2 || ImDocumentData.ShowDebugInfo);
            commandState.Enabled = true;
            return true;
          }
          goto label_57;
        case 2349720624:
          if (commandName == "ShowInsertSymbolView")
            goto label_52;
          goto label_57;
        case 2531153527:
          if (commandName == "FindNext")
            goto label_53;
          goto label_57;
        case 2624912623:
          if (commandName == "DocEditor.UpdateDocumentLinks")
            goto label_52;
          goto label_57;
        case 2637794311:
          if (commandName == "CopySelectedToExcel")
            goto label_53;
          goto label_57;
        case 2783175676:
          if (commandName == "CallEditor")
          {
            commandState.Visible = flag1;
            if (!commandState.Visible)
              commandState.Enabled = false;
            return true;
          }
          goto label_57;
        case 3113183494:
          if (commandName == "DocEditor.Table")
            break;
          goto label_57;
        case 3530133579:
          if (commandName == "LoadOleFile")
            goto label_52;
          goto label_57;
        case 3794582656:
          if (commandName == "ShowDocumentTreeView")
            goto label_52;
          goto label_57;
        case 3839184739:
          if (commandName == "Replace")
            goto label_53;
          goto label_57;
        case 4091959513:
          if (commandName == "DocEditor.Zoom")
            goto label_52;
          goto label_57;
        default:
          goto label_57;
      }
      commandState.Visible = flag1 && DocumentEditorPlugin.ActiveImDocumentEditorForm.BaseEditCommandsEnabled;
      if (!commandState.Visible)
        commandState.Enabled = false;
      return true;
label_52:
      commandState.Visible = flag1;
      commandState.Enabled = true;
      return true;
label_53:
      commandState.Visible = flag1;
      if (!commandState.Visible)
        commandState.Enabled = false;
      return !flag1;
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
      commandState.Enabled = false;
      return true;
    }
label_57:
    return false;
  }

  public CommandsInfo GetMergedCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    return CommandsInfo.Empty;
  }

  public CommandsInfo GetGroupCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    CommandsInfo groupCommands = new CommandsInfo();
    try
    {
      items.GetItemData(0, typeof (IDBTypedObjectID));
      groupCommands.Add("OrderReportTest", new CommandInfo(4, new ClickEventHandler(DocumentEditorPlugin.OrderReportTestCommand)));
      if ((IReportView) viewServices.GetService(typeof (IReportView)) == null || items.Count <= 0)
        return groupCommands;
      for (int index = 0; index < items.Count; ++index)
      {
        if (items.GetItemData(index, typeof (IDBTypedObjectID)) is IDBTypedObjectID)
        {
          groupCommands.Add("TableReports", new CommandInfo(4, new ClickEventHandler(DocumentEditorPlugin.TableReportsCommand)));
          break;
        }
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
    return groupCommands;
  }

  public static void InitDocumentPlugin(ShowExceptionDialogDelegate showExceptionDialog = null)
  {
    DocumentPlugin.InitDocumentPlugin();
    if (DocumentEditorPlugin.docPluginInitialized)
      return;
    ImDocumentEditorConfig.Instance.IsClientPluginConfig = true;
    ImDocumentData.ShowExceptionDialog = showExceptionDialog ?? new ShowExceptionDialogDelegate(ExceptionHelper.ExceptionService.ShowException);
    TemplateHolderBase.Instance = (TemplateHolderBase) new TemplateHolder();
    System.Type type1 = typeof (ReferenceToDBObject);
    DocumentTreeNode.TypeNameDictionary[(object) type1.Name] = (object) type1;
    DocumentTreeNode.TypeNameDictionary[(object) ReferenceToDBObjectBase.XmlTypeName] = (object) type1;
    int index1 = ReferenceBase.ReferenceClassList.IndexOf(typeof (ReferenceToDBObjectBase));
    if (index1 == -1)
      ReferenceBase.ReferenceClassList.Add(type1);
    else
      ReferenceBase.ReferenceClassList[index1] = type1;
    DocumentTreeNode.TypeConstructorDictionary[(object) type1.Name] = (object) new EmptyConstructorDelegate(ReferenceToDBObject.EmptyConstructor);
    DocumentTreeNode.TypeConstructorDictionary[(object) ReferenceToDBObjectBase.XmlTypeName] = (object) new EmptyConstructorDelegate(ReferenceToDBObject.EmptyConstructor);
    System.Type type2 = typeof (ReferenceToDBObjectBase);
    DocumentTreeNode.TypeNameDictionary[(object) type2.Name] = (object) typeof (ReferenceToDBObject);
    DocumentTreeNode.TypeConstructorDictionary[(object) type2.Name] = (object) new EmptyConstructorDelegate(ReferenceToDBObject.EmptyConstructor);
    System.Type type3 = typeof (ReferenceToDBObjectAttribute);
    DocumentTreeNode.TypeNameDictionary[(object) type3.Name] = (object) type3;
    DocumentTreeNode.TypeNameDictionary[(object) ReferenceToDBObjectAttributeBase.XmlTypeName] = (object) type3;
    int index2 = ReferenceBase.ReferenceClassList.IndexOf(typeof (ReferenceToDBObjectAttributeBase));
    if (index2 == -1)
      ReferenceBase.ReferenceClassList.Add(type3);
    else
      ReferenceBase.ReferenceClassList[index2] = type3;
    DocumentTreeNode.TypeConstructorDictionary[(object) type3.Name] = (object) new EmptyConstructorDelegate(ReferenceToDBObjectAttribute.EmptyConstructor);
    DocumentTreeNode.TypeConstructorDictionary[(object) ReferenceToDBObjectAttributeBase.XmlTypeName] = (object) new EmptyConstructorDelegate(ReferenceToDBObjectAttribute.EmptyConstructor);
    System.Type type4 = typeof (ReferenceToDBObjectAttributeBase);
    DocumentTreeNode.TypeNameDictionary[(object) type4.Name] = (object) typeof (ReferenceToDBObjectAttribute);
    DocumentTreeNode.TypeConstructorDictionary[(object) type4.Name] = (object) new EmptyConstructorDelegate(ReferenceToDBObjectAttribute.EmptyConstructorActiveLink);
    System.Type type5 = typeof (ReferenceToSign);
    DocumentTreeNode.TypeNameDictionary[(object) type5.Name] = (object) type5;
    DocumentTreeNode.TypeNameDictionary[(object) ReferenceToSignBase.XmlTypeName] = (object) type5;
    int index3 = ReferenceBase.ReferenceClassList.IndexOf(typeof (ReferenceToSignBase));
    if (index3 == -1)
      ReferenceBase.ReferenceClassList.Add(type5);
    else
      ReferenceBase.ReferenceClassList[index3] = type5;
    DocumentTreeNode.TypeConstructorDictionary[(object) type5.Name] = (object) new EmptyConstructorDelegate(ReferenceToSign.EmptyConstructor);
    DocumentTreeNode.TypeConstructorDictionary[(object) ReferenceToSignBase.XmlTypeName] = (object) new EmptyConstructorDelegate(ReferenceToSign.EmptyConstructor);
    System.Type type6 = typeof (ReferenceToSignBase);
    DocumentTreeNode.TypeNameDictionary[(object) type6.Name] = (object) typeof (ReferenceToSign);
    DocumentTreeNode.TypeConstructorDictionary[(object) type6.Name] = (object) new EmptyConstructorDelegate(ReferenceToSign.EmptyConstructorActiveLink);
    System.Type type7 = typeof (ReferenceToGraphics);
    DocumentTreeNode.TypeNameDictionary[(object) type7.Name] = (object) type7;
    DocumentTreeNode.TypeNameDictionary[(object) ReferenceToGraphicsBase.XmlTypeName] = (object) type7;
    int index4 = ReferenceBase.ReferenceClassList.IndexOf(typeof (ReferenceToGraphicsBase));
    if (index4 == -1)
      ReferenceBase.ReferenceClassList.Add(type7);
    else
      ReferenceBase.ReferenceClassList[index4] = type7;
    DocumentTreeNode.TypeConstructorDictionary[(object) type7.Name] = (object) new EmptyConstructorDelegate(ReferenceToGraphics.EmptyConstructor);
    DocumentTreeNode.TypeConstructorDictionary[(object) ReferenceToGraphicsBase.XmlTypeName] = (object) new EmptyConstructorDelegate(ReferenceToGraphics.EmptyConstructor);
    DocumentEditorPluginBase.ImDocumentLoader = new LoadDocumentFromDBObjectDelegate(DocumentEditorPlugin.LoadDocumentFromDBObjectCore);
    DocumentEditorPlugin.docPluginInitialized = true;
  }

  void IDocumentPlugin.Init() => DocumentEditorPlugin.InitDocumentPlugin();

  public static DockManager DockManager
  {
    get
    {
      if (DocumentEditorPlugin.dockManager == null)
        DocumentEditorPlugin.dockManager = (DockManager) ServicesManager.GetService(typeof (DockManager));
      return DocumentEditorPlugin.dockManager;
    }
  }

  public static bool UseImDocEditorSettingsCache
  {
    get => DocumentEditorPlugin.useImDocEditorSettingsCache;
    set
    {
      if (DocumentEditorPlugin.useImDocEditorSettingsCache == value)
        return;
      DocumentEditorPlugin.useImDocEditorSettingsCache = value;
      if (DocumentEditorPlugin.useImDocEditorSettingsCache)
        return;
      DocumentEditorPlugin.imDocEditorSettingsCache?.Clear();
      DocumentEditorPlugin.imDocEditorSettingsCache = (IMDocEditorToolSettings) null;
    }
  }

  private static void OrderReportTestCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    OrderReportTest.Generate((items.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID).ObjectID, 991523L);
  }

  /// <summary>Обработчик команды меню "Табличный отчет"</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  private static void TableReportsCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    try
    {
      int num = (int) new SelectReportForm(viewServices, items).Execute();
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  private void ReportButtonMenuPressed(object sender, ButtonMenuPressedArgs args)
  {
    try
    {
      new ReportFormer(new ReportParameters(args.ObjectInfo.ObjectID, args.Query, args.Services)).Execute(ShowReport.InPreviewWindow);
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  public event BeforePrintDocumentEventHandler BeforePrint;

  public event AfterPrintDocumentEventHandler AfterPrint;

  public void FireBeforePrint(object sender, BeforePrintDocumentEventArgs e)
  {
    List<ImDocumentData> imDocumentDataList = new List<ImDocumentData>();
    if (e.Document is ImDocumentData)
      imDocumentDataList.Add(e.Document as ImDocumentData);
    if (e.Document is DocumentsComplect)
      imDocumentDataList.AddRange((IEnumerable<ImDocumentData>) (e.Document as DocumentsComplect).GetAllDocuments());
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      string caption = sessionKeeper.Session.GetObject(sessionKeeper.Session.UserID).Caption;
      string attributeValue = DateTime.Now.ToString();
      foreach (ImDocumentData imDocumentData in imDocumentDataList)
      {
        imDocumentData.SetAttributeValue(DocumentTreeNode.AttributeName_PrintUser, caption, false, false, false);
        imDocumentData.SetAttributeValue(DocumentTreeNode.AttributeName_PrintDate, attributeValue, false, false, false);
      }
    }
    BeforePrintDocumentEventHandler beforePrint = this.BeforePrint;
    if (beforePrint == null)
      return;
    beforePrint(sender, e);
  }

  public void FireAfterPrint(object sender, AfterPrintDocumentEventArgs e)
  {
    List<ImDocumentData> imDocumentDataList = new List<ImDocumentData>();
    if (e.Document is ImDocumentData)
      imDocumentDataList.Add(e.Document as ImDocumentData);
    if (e.Document is DocumentsComplect)
      imDocumentDataList.AddRange((IEnumerable<ImDocumentData>) (e.Document as DocumentsComplect).GetAllDocuments());
    foreach (ImDocumentData imDocumentData in imDocumentDataList)
    {
      imDocumentData.SetAttributeValue(DocumentTreeNode.AttributeName_PrintUser, "", false, false, false);
      imDocumentData.SetAttributeValue(DocumentTreeNode.AttributeName_PrintDate, "", false, false, false);
    }
    AfterPrintDocumentEventHandler afterPrint = this.AfterPrint;
    if (afterPrint == null)
      return;
    afterPrint(sender, e);
  }

  /// <summary>Локальный класс для аргументов старта фонового потока</summary>
  private class ThreadParams
  {
    public int FileIndex;
    public string FileName;
    public IDBObject DBObject;
    public long ObjectID;
    public string ObjectCaption;
    public Guid ObjectGuid;
    public int ObjectType;
    public IDBAttribute FileAttribute;
    public int AttributeId;
    public bool LoadInThread;
    public bool FailIfUnknownFormat;
    public bool FailIfEmptyFile;
    public bool UpdateDoc;
    public bool IsTemplate;
    public bool IsFormulaLib;
    public bool DocEnter;
    public Exception Exception;
    public DocumentTreeNode RootNode;
    public XmlReadArgs readArgs;
    public bool ModifiedByPatch;

    public string AttributeName => MetaDataHelper.GetAttributeTypeName(this.AttributeId);

    public ImDocument Document => this.RootNode as ImDocument;

    public DocumentsComplect Complect => this.RootNode as DocumentsComplect;
  }
}
