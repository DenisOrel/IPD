// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.FormulaEditPlugin
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using ImSSP;
using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.DataFormats;
using Intermech.Docking;
using Intermech.Expert.Editor.Table;
using Intermech.Expert.Scenarios;
using Intermech.Imbase;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Configuration;
using Intermech.Interfaces.Expert;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.Plugins;
using Intermech.Interfaces.SelectionService;
using Intermech.Localization;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.PropertyEditors;
using Intermech.Protection;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Expert.Editor;

/// <summary>Main plugin class</summary>
internal class FormulaEditPlugin : 
  IPackage,
  IConfigurable,
  ICommandsProvider,
  IExpertEditor,
  ICommandTarget
{
  public static System.IServiceProvider _serviceProvider;
  private static DockManager dockManager;
  private FormEditor fEd;
  private ExpertTableColorEditor _colorEditor;
  private ExpertTablePropertiesEditor _propertiesEditor;
  public bool IsAdmin;

  public FormulaEditPlugin() => this.fEd = new FormEditor();

  public System.IServiceProvider provider => FormulaEditPlugin._serviceProvider;

  public string Name => LocalizationHolder.rm.GetString("Expert.Editor_183");

  /// <summary>
  /// Зарегистрировать все закладки, добавляемые модулем расширения в Навигатор
  /// </summary>
  internal void RegisterViews()
  {
    AdjustableViewsHelper.RegisterView(LocalizationHolder.rm.GetString("Expert.Editor_202"), LocalizationHolder.rm.GetString("Expert.Editor_27"), "", "", "", true, 0);
  }

  public void Load(System.IServiceProvider serviceProvider)
  {
    ((ILicenser) ServicesManager.GetService(typeof (ILicenser))).AllocateLicense(342);
    FormulaEditPlugin._serviceProvider = serviceProvider;
    this.fEd._serviceProvider = serviceProvider;
    BarManager service1 = (BarManager) serviceProvider.GetService(typeof (BarManager));
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ExpertConsts.Init(sessionKeeper.Session);
      if (ServicesManager.GetService(typeof (IAttributePropertyDescriberService)) is IAttributePropertyDescriberService service2)
      {
        int attributeId1 = sessionKeeper.Session.GetAttributeType(ScenarioGUIDs.attributeCreateType).AttributeID;
        if (service2.GetDescriber(attributeId1) == null)
          service2.RegisterDescriber(attributeId1, (IAttributePropertyDescriber) new ObjectTypeAttDescriber());
        int attributeId2 = sessionKeeper.Session.GetAttributeType(ScenarioGUIDs.attributeDestObjTypes).AttributeID;
        if (service2.GetDescriber(attributeId2) == null)
          service2.RegisterDescriber(attributeId2, (IAttributePropertyDescriber) new ObjectTypeAttDescriber());
        int attributeId3 = sessionKeeper.Session.GetAttributeType(ScenarioGUIDs.attributeСompositionRelType).AttributeID;
        if (service2.GetDescriber(attributeId3) == null)
          service2.RegisterDescriber(attributeId3, (IAttributePropertyDescriber) new RelationTypeAttDescriber());
      }
    }
    IFactory service3 = FormulaEditPlugin._serviceProvider.GetService(typeof (IFactory)) as IFactory;
    IViewsProvider provider = (IViewsProvider) new ExpFormViewProvider();
    service3.AddViewsProvider(1, ExpertConsts.Consts.objFormula, provider);
    service3.AddViewsProvider(1, ExpertConsts.Consts.objCond, provider);
    service3.AddViewsProvider(1, ExpertConsts.Consts.objSimpleFormula, provider);
    service3.AddViewsProvider(1, ExpertConsts.Consts.objESFolder, provider);
    service3.AddViewsProvider(1, ExpertConsts.Consts.objTable, (IViewsProvider) new TableEditViewProvider());
    service3.AddCommandsProvider(1, ExpertConsts.Consts.objTable, (ICommandsProvider) new ExpertTableCommands());
    service3.AddCommandsProvider(1, ExpertConsts.Consts.objTemplate, (ICommandsProvider) new DocScriptForTemplateMenuProvider());
    service3.AddCommandsProvider(1, ExpertConsts.Consts.objFormula, (ICommandsProvider) this);
    service3.AddCommandsProvider(1, ExpertConsts.Consts.objCond, (ICommandsProvider) this);
    service3.AddCommandsProvider(1, ExpertConsts.Consts.objSimpleFormula, (ICommandsProvider) this);
    service3.AddCommandsProvider(1, ExpertConsts.Consts.objESFolder, (ICommandsProvider) this);
    service3.AddCommandsProvider(1, ExpertConsts.Consts.objVisScheme, (ICommandsProvider) this);
    service3.AddCommandsProvider(1, ExpertConsts.Consts.objVisStyles, (ICommandsProvider) this);
    service3.AddCommandsProvider(1, ExpertConsts.Consts.objCommandScript, (ICommandsProvider) this);
    service3.AddCommandsProvider(1, ExpertConsts.Consts.objObject, (ICommandsProvider) this);
    MenuTemplate contextMenuTemplate = service3.ContextMenuTemplate;
    if (FormulaEditPlugin.dockManager == null)
      FormulaEditPlugin.dockManager = (DockManager) serviceProvider.GetService(typeof (DockManager));
    IObjectCreatorService service4 = (IObjectCreatorService) serviceProvider.GetService(typeof (IObjectCreatorService));
    if (service4 != null)
    {
      service4.RegisterCreatorCustomService(ExpertConsts.Consts.objFormula, typeof (ExpObjectCreator));
      service4.RegisterCreatorCustomService(ExpertConsts.Consts.objCond, typeof (ExpObjectCreator));
      service4.RegisterCreatorCustomService(ExpertConsts.Consts.objSimpleFormula, typeof (ExpObjectCreator));
      service4.RegisterCreatorCustomService(ExpertConsts.Consts.objESFolder, typeof (ExpObjectCreator));
      service4.RegisterCreatorCustomService(ExpertConsts.Consts.objScript, typeof (ExpObjectCreator));
      service4.RegisterCreatorCustomService(ExpertConsts.Consts.objFunction, typeof (ExpObjectCreator));
      service4.RegisterCreatorCustomService(ExpertConsts.Consts.objDocScript, typeof (ExpObjectCreator));
      service4.RegisterCreatorCustomService(ExpertConsts.Consts.objAttrRules, typeof (ExpObjectCreator));
      service4.RegisterCreatorCustomService(ExpertConsts.Consts.objObjRules, typeof (ExpObjectCreator));
      service4.RegisterCreatorCustomService(ExpertConsts.Consts.objRecalcScript, typeof (ExpObjectCreator));
      service4.RegisterCreatorCustomService(ExpertConsts.Consts.objComplectTemplate, typeof (ExpObjectCreator));
      service4.RegisterCreatorCustomService(ExpertConsts.Consts.objCommandScript, typeof (ExpObjectCreator));
      eTableObjectCreator.Attach(service4);
    }
    AdjustableViewsHelper.RegisterView(sc_6469.ssp_expert_6470(), LocalizationHolder.rm.GetString("Expert.Editor_193"), LocalizationHolder.rm.GetString("Expert.Editor_194"), "Intermech.Expert.Editor", "", true, 7);
    AdjustableViewsHelper.RegisterView("Expert.FormulaView", LocalizationHolder.rm.GetString("Expert.Editor_195"), LocalizationHolder.rm.GetString("Expert.Editor_196"), "Intermech.Expert.Editor", "", true, 17);
    ServicesManager.AddService(typeof (IExpertEditor), (object) this);
    ServicesManager.AddService(typeof (IExpertTableColorsService), (object) new ExpertTableColorsService());
    ServicesManager.AddService(typeof (IExpertTablePropertiesService), (object) new ExpertTablePropertiesService());
    this._colorEditor = new ExpertTableColorEditor();
    this._propertiesEditor = new ExpertTablePropertiesEditor();
    ImLink.il.Init(serviceProvider);
    IDefaultCommands4ObjTypes service5 = ServicesManager.GetService(typeof (IDefaultCommands4ObjTypes)) as IDefaultCommands4ObjTypes;
    service5.AddDefaultCommand(ExpertConsts.Consts.objDocScript, "EditDocument", DefaultCommandHandler.ContectMenu);
    service5.AddDefaultCommand(ExpertConsts.Consts.objAttrRules, "EditDocument", DefaultCommandHandler.ContectMenu);
    service5.AddDefaultCommand(ExpertConsts.Consts.objComplectTemplate, "EditDocument", DefaultCommandHandler.ContectMenu);
    service5.AddDefaultCommand(ExpertConsts.Consts.objRecalcScript, "EditDocument", DefaultCommandHandler.ContectMenu);
    service5.AddDefaultCommand(ExpertConsts.Consts.objObjRules, "EditDocument", DefaultCommandHandler.ContectMenu);
    service5.AddDefaultCommand(ExpertConsts.Consts.objScript, "EditDocument", DefaultCommandHandler.ContectMenu);
    service5.AddDefaultCommand(ExpertConsts.Consts.objFunction, "EditDocument", DefaultCommandHandler.ContectMenu);
    service5.AddDefaultCommand(ExpertConsts.Consts.objVisStyles, "EditDocument", DefaultCommandHandler.ContectMenu);
    service5.AddDefaultCommand(ExpertConsts.Consts.objVisScheme, "EditDocument", DefaultCommandHandler.ContectMenu);
    service5.AddDefaultCommand(ExpertConsts.Consts.objCommandScript, "EditDocument", DefaultCommandHandler.ContectMenu);
    IPluginManager service6 = serviceProvider.GetService(typeof (IPluginManager)) as IPluginManager;
    IContentProvider service7 = (IContentProvider) serviceProvider.GetService(typeof (IContentProvider));
    if (service7 != null)
      service7.ContentCallback += new GetContentCallback(this.RestoreDocumentWindow);
    EventHandler eventHandler = new EventHandler(this.pluginManager_LoadComplete);
    service6.LoadComplete += eventHandler;
    ((ISelectionDialogTabsService) ServicesManager.GetService(typeof (ISelectionDialogTabsService))).SelectionDialogTabEvent += new SelectionDialogTabCreateHandler(this.sdTabService_SelectionDialogTabEvent);
    this.RegisterViews();
  }

  private ISelectionDialogTab sdTabService_SelectionDialogTabEvent(
    object sender,
    SelectionDialogTabEventArgs e)
  {
    return (ISelectionDialogTab) new SelectionTabControl();
  }

  private void pluginManager_LoadComplete(object sender, EventArgs e)
  {
    BarManager service1 = (BarManager) FormulaEditPlugin._serviceProvider.GetService(typeof (BarManager));
    ServicesManager.GetService(typeof (ICategoryTypeIconService));
    ((ICommandManager) FormulaEditPlugin._serviceProvider.GetService(typeof (ICommandManager))).AddTarget((ICommandTarget) this);
    if ((ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole).IsAdmin)
      this.IsAdmin = true;
    Token.BeautifyEvent += new EventHandler(this.Token_BeautifyEvent);
    INotificationService service2 = ServicesManager.GetService<INotificationService>();
    if (service2 == null)
      return;
    NotificationEventHandler eventHandler = new NotificationEventHandler(this.OnObjectDeleted);
    service2.Subscribe("ObjectsRemoved", eventHandler);
  }

  private void Token_BeautifyEvent(object sender, EventArgs e)
  {
    if (!(sender is Token token))
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      switch (token.spt)
      {
        case SelectionParameterTypes.sptObject:
          IDBObject dbObject = sessionKeeper.Session.GetObject(token.iValue, false);
          if (dbObject == null)
            break;
          token.text = dbObject.Caption;
          break;
        case SelectionParameterTypes.sptObjectType:
          IDBObjectType objectType = sessionKeeper.Session.GetObjectType(Convert.ToInt32(token.iValue), false);
          if (objectType == null)
            break;
          token.text = objectType.ObjectTypeName;
          break;
        case SelectionParameterTypes.sptLinkType:
          IDBRelationType relationType = sessionKeeper.Session.GetRelationType(Convert.ToInt32(token.iValue), false);
          if (relationType == null)
            break;
          token.text = relationType.Description;
          break;
      }
    }
  }

  private void OnObjectDeleted(object sender, NotificationEventArgs e)
  {
    if (!(e is DBObjectsEventArgs objectsEventArgs) || objectsEventArgs.ObjectIDs == null)
      return;
    MetaDataHelper.GetObjectTypeChildrenIDRecursive(ExpertConsts.Consts.objObject);
    HashSet<long> delObjs = new HashSet<long>(objectsEventArgs.ItemsCount);
    for (int index = 0; index < objectsEventArgs.ItemsCount && objectsEventArgs.ObjectIDs.Count > index; ++index)
    {
      long objectId = objectsEventArgs.ObjectIDs[index];
      delObjs.Add(objectsEventArgs.ObjectIDs[index]);
    }
    if (delObjs.Count <= 0)
      return;
    this.CloseViewsForDeletedObjects(delObjs);
  }

  internal void CloseViewsForDeletedObjects(HashSet<long> delObjs)
  {
    if (FormulaEditPlugin.dockManager == null || FormulaEditPlugin.dockManager.DocumentContainer == null || FormulaEditPlugin.dockManager.DocumentContainer.Documents == null)
      return;
    foreach (DockControl document in FormulaEditPlugin.dockManager.DocumentContainer.Documents)
    {
      if (document != null && document is ScriptEditCon)
      {
        long scriptId = (document as ScriptEditCon).scriptEditor.scriptID;
        if (delObjs.Contains(scriptId) || delObjs.Contains(-scriptId))
          document.Close();
      }
    }
  }

  public void Unload()
  {
    ((ILicenser) ServicesManager.GetService(typeof (ILicenser))).ReleaseLicense(341);
    if (FormulaEditPlugin._serviceProvider == null)
      return;
    IObjectCreatorService service = (IObjectCreatorService) FormulaEditPlugin._serviceProvider.GetService(typeof (IObjectCreatorService));
    if (service == null)
      return;
    service.UnregisterCreatorCustomService(ExpertConsts.Consts.objFormula, typeof (ExpObjectCreator));
    service.UnregisterCreatorCustomService(ExpertConsts.Consts.objCond, typeof (ExpObjectCreator));
    service.UnregisterCreatorCustomService(ExpertConsts.Consts.objScript, typeof (ExpObjectCreator));
    service.UnregisterCreatorCustomService(ExpertConsts.Consts.objFunction, typeof (ExpObjectCreator));
    service.UnregisterCreatorCustomService(ExpertConsts.Consts.objESFolder, typeof (ExpObjectCreator));
    eTableObjectCreator.Detach(service);
  }

  /// <summary>Восстановить окно</summary>
  /// <param name="guid">Guid окна</param>
  /// <param name="persistString">Строка данных окна</param>
  /// <returns>Окно</returns>
  public DockControl RestoreDocumentWindow(Guid guid, string persistString)
  {
    if (guid == ScriptEditCon.ScriptWindowGuid)
    {
      flag = false;
      long result = -1;
      long scrId;
      if (long.TryParse(persistString, out result))
      {
        scrId = DBHelper.GetObjIDByGuid(DBHelper.GetObjGuidByID(result));
      }
      else
      {
        scrId = -1L;
        string empty = string.Empty;
        using (MemoryStream serializationStream = new MemoryStream(Convert.FromBase64String(persistString)))
        {
          object obj1 = new BinaryFormatter().Deserialize((Stream) serializationStream);
          if (obj1 is HybridDictionary)
          {
            HybridDictionary hybridDictionary = obj1 as HybridDictionary;
            object obj2 = hybridDictionary[(object) "ScriptId"];
            if (obj2 != null && obj2 is string && Convert.ToString(obj2) != "")
            {
              using (SessionKeeper sessionKeeper = new SessionKeeper())
              {
                IDBObject dbObject = sessionKeeper.Session.GetObject(Convert.ToInt64(obj2), false);
                if (dbObject != null)
                  scrId = dbObject.ObjectID;
              }
            }
            object obj3 = hybridDictionary[(object) "ReadOnly"];
            if (obj3 != null)
            {
              if (!(obj3 is bool flag))
                ;
            }
          }
        }
      }
      if (scrId != -1L)
      {
        ScriptEditCon scriptEditCon = new ScriptEditCon(scrId);
        scriptEditCon.ReadOnly = flag;
        scriptEditCon.Show(FormulaEditPlugin.dockManager, DockState.Document);
        scriptEditCon.Select();
        scriptEditCon.Closed += new EventHandler(this.sec_Closed);
        this.UpdateDocumentCaption(FormulaEditPlugin.dockManager);
        return (DockControl) scriptEditCon;
      }
    }
    return (DockControl) null;
  }

  public void LoadConfiguration(IConfigurationManager configurationManager)
  {
    configurationManager.Open("FormulaEditor");
  }

  public void SaveConfiguration(IConfigurationManager configurationManager)
  {
    configurationManager.Create("FormulaEditor");
  }

  public static FormulaEditPlugin FindPlugin()
  {
    return (FormulaEditPlugin) FormulaEditPlugin.FindPlugin(typeof (FormulaEditPlugin));
  }

  public static IPackage FindPlugin(System.Type pluginType)
  {
    IPackage plugin1 = (IPackage) null;
    IPluginManager service = (IPluginManager) ServicesManager.GetService(typeof (IPluginManager));
    if (service != null)
    {
      foreach (IPlugin plugin2 in (IEnumerable<IPlugin>) service.Plugins)
      {
        foreach (IPackage package in (IEnumerable<IPackage>) plugin2.Packages)
        {
          if (package.GetType() == pluginType)
          {
            plugin1 = package;
            break;
          }
        }
        if (plugin1 != null)
          break;
      }
    }
    return plugin1;
  }

  public CommandsInfo GetMergedCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    if (items.Count != 1)
      return CommandsInfo.Empty;
    CommandsInfo mergedCommands = new CommandsInfo();
    ViewStateFlags viewStateFlags = viewServices.GetService(typeof (IViewState)) is IViewState service ? service.ViewState : ViewStateFlags.None;
    if ((viewStateFlags & ViewStateFlags.InDialog) == ViewStateFlags.None && (viewStateFlags & ViewStateFlags.ReadOnly) == ViewStateFlags.None)
    {
      mergedCommands.Add("EditDocument", new CommandInfo(4096 /*0x1000*/, new ClickEventHandler(this.ContextOpenExpertEditor)));
      mergedCommands.Add("ViewDocument", new CommandInfo(4096 /*0x1000*/, new ClickEventHandler(this.ContextViewExpertObject)));
    }
    return mergedCommands;
  }

  public CommandsInfo GetGroupCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    return new CommandsInfo();
  }

  /// <summary>Открыть объекты экспертной системы для редактирования</summary>
  /// <param name="items">Выбранные в навигаторе объекты</param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  private void ContextOpenExpertEditor(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    for (int index = 0; index < items.Count; ++index)
    {
      IDBTypedObjectID itemData = (IDBTypedObjectID) items.GetItemData(index, typeof (IDBTypedObjectID));
      if (itemData != null)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IUserSession session = sessionKeeper.Session;
          int objectType = itemData.ObjectType;
          IDBObject idbO = session.GetObject(itemData.ObjectID);
          long objectId = idbO.ObjectID;
          IDBObject dbObject = FormulaEditPlugin.PrepareForEdit(session, idbO);
          if (dbObject != null)
          {
            if (dbObject is IExpertScriptable)
            {
              ScriptEditCon scriptEditCon = new ScriptEditCon(dbObject.ObjectID);
              scriptEditCon.Show(FormulaEditPlugin.dockManager, DockState.Document);
              scriptEditCon.Select();
              scriptEditCon.Closed += new EventHandler(this.sec_Closed);
              this.UpdateDocumentCaption(FormulaEditPlugin.dockManager);
            }
            else
              Services.InvokeCommand("OpenInNewWindow", Services.GetCommandsTable(Services.GetItems(dbObject.ObjectID), viewServices, false), viewServices);
          }
        }
      }
    }
  }

  private void sec_Closed(object sender, EventArgs e)
  {
    ((Component) sender).Dispose();
    GC.Collect(100, GCCollectionMode.Forced);
    GC.WaitForPendingFinalizers();
    int num = (int) GC.WaitForFullGCComplete();
  }

  /// <summary>Открыть извещения для просмотра</summary>
  /// <param name="items">Выбранные в навигаторе объекты</param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  private void ContextViewExpertObject(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (int index = 0; index < items.Count; ++index)
      {
        IDBTypedObjectID itemData = (IDBTypedObjectID) items.GetItemData(index, typeof (IDBTypedObjectID));
        if (itemData != null)
        {
          int objectType = itemData.ObjectType;
          long objectId = itemData.ObjectID;
          IDBObject dbObject = sessionKeeper.Session.GetObject(objectId);
          if (dbObject != null && dbObject is IExpertScriptable)
          {
            ScriptEditCon scriptEditCon = new ScriptEditCon(dbObject.ObjectID);
            scriptEditCon.Show(FormulaEditPlugin.dockManager, DockState.Document);
            scriptEditCon.Select();
            scriptEditCon.ReadOnly = true;
            this.UpdateDocumentCaption(FormulaEditPlugin.dockManager);
          }
          else
            Services.InvokeCommand("OpenInNewWindow", Services.GetCommandsTable(Services.GetItems(objectId), viewServices, false), viewServices);
        }
      }
    }
  }

  internal void UpdateDocumentCaption(DockManager dockManager)
  {
    for (int index = 0; index < dockManager.DocumentContainer.Documents.Length; ++index)
    {
      if (dockManager.DocumentContainer.Documents[index] is ScriptEditCon document)
        document.UpdateDocumentWindowCaption();
    }
  }

  public bool EditCondition(ref object cond, string title)
  {
    TempFormula tf;
    if (cond != null)
    {
      if (cond.GetType() != typeof (TempFormula))
        throw new Exception(LocalizationHolder.rm.GetString("Expert.Editor_197"));
      tf = ((TempFormula) cond).Clone() as TempFormula;
    }
    else
      tf = new TempFormula(true);
    int num = new FormEditor().Execute(ref tf, title) ? 1 : 0;
    if (num == 0)
      return num != 0;
    cond = (object) tf;
    return num != 0;
  }

  /// <summary>Это родительский или тот же тип что и данный</summary>
  /// <param name="parent">Родительский тип</param>
  /// <param name="child">Дочерний тип</param>
  /// <returns></returns>
  public static bool IsParentOrEqualObjectType(int parent, int child)
  {
    return parent == child || FormulaEditPlugin.IsParentObjectType(parent, child);
  }

  /// <summary>Проверить является ли тип child дочерним для типа parent</summary>
  /// <param name="parent">Родительский тип</param>
  /// <param name="child">Дочерний тип</param>
  /// <returns>true, если parent является родительским типом для child</returns>
  public static bool IsParentObjectType(int parent, int child)
  {
    return MetaDataHelper.IsObjectTypeChildOf(child, parent);
  }

  public static IDBObject PrepareForEdit(IUserSession ius, IDBObject idbO)
  {
    switch (idbO.ObjectModifyMode)
    {
      case ObjectModifyModes.InBase:
        return idbO;
      case ObjectModifyModes.Checkout:
        IDBObject dbObject = idbO;
        long checkoutBy = idbO.CheckoutBy;
        if (!checkoutBy.Equals(0L))
        {
          checkoutBy = idbO.CheckoutBy;
          if (!checkoutBy.Equals(ius.UserID))
            throw new ArgumentException(LocalizationHolder.rm.GetString(sc_6469.ssp_imclient_6471()));
        }
        else
        {
          dbObject = idbO.CheckOut();
          if (ServicesManager.GetService(typeof (INotificationService)) is INotificationService service)
          {
            DBObjectsEventArgs e = (DBObjectsEventArgs) new DBObjectsCheckOutEventArgs("ObjectsCheckedOut", (IList<long>) new long[1]
            {
              idbO.ObjectID
            }, (IList<long>) new long[1]
            {
              dbObject.ObjectID
            });
            service.FireEvent((object) null, (NotificationEventArgs) e);
          }
        }
        return dbObject;
      case ObjectModifyModes.CreateVersion:
        throw new Exception(LocalizationHolder.rm.GetString("Expert.Editor_200"));
      case ObjectModifyModes.CantModify:
        throw new Exception(LocalizationHolder.rm.GetString("Expert.Editor_198"));
      default:
        return idbO;
    }
  }

  public static void CopyToClipboard(TempFormula tf)
  {
    MemoryStream output = new MemoryStream();
    BinaryWriter bw = new BinaryWriter((Stream) output);
    tf.Save(bw);
    byte[] array = output.ToArray();
    Clipboard.SetData(TempFormula.FormulaFormat, (object) array);
  }

  public static void PasteFromClipboard(TempFormula tf)
  {
    if (!Clipboard.ContainsData(TempFormula.FormulaFormat))
      return;
    BinaryReader br = new BinaryReader((Stream) new MemoryStream((byte[]) Clipboard.GetData(TempFormula.FormulaFormat)));
    tf.Clear();
    tf.Load(br, ExpertConsts.FormulaVersion);
  }

  public static bool IsAttrForSprav(IDBAttributeType idbAT)
  {
    if (idbAT.PropertiesStructure.FieldType != FieldTypes.ftObjectLink)
      return false;
    return idbAT.SizeType == -1L || idbAT.SizeType == (long) ExpertConsts.Consts.baseIMBASEObject;
  }

  public static bool IsAttrForSpravochnik(Guid attrGuid)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return FormulaEditPlugin.IsAttrForSprav(sessionKeeper.Session.GetAttributeType(attrGuid));
  }

  public static bool IsAttrForSpravochnik(int attrTypeId)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return sessionKeeper.Session.GetAttributeType(attrTypeId).PropertiesStructure.FieldType == FieldTypes.ftObjectLink;
  }

  public static List<long> GetImbaseCatalog(int objTypeId, int attrTypeId)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ImbaseExtendedItem imbaseExtendedItem = ExtendedServiceHelper.GetObjTypeData(objTypeId, sessionKeeper.Session)?.GetValue(attrTypeId, sessionKeeper.Session);
      return imbaseExtendedItem != null ? imbaseExtendedItem.CatalogIDs : new List<long>();
    }
  }

  public bool Execute(ICommandState commandState)
  {
    switch (commandState.CommandName)
    {
      case "Expert.Export":
        new ExpertExport().Execute();
        return true;
      case "Expert.Import":
        new ExpertImport().Execute();
        return true;
      default:
        return false;
    }
  }

  public bool QueryStatus(ICommandState commandState)
  {
    switch (commandState.CommandName)
    {
      case "Expert.Export":
        commandState.Visible = this.IsAdmin;
        commandState.Enabled = true;
        return true;
      case "Expert.Import":
        commandState.Visible = this.IsAdmin;
        commandState.Enabled = true;
        return true;
      default:
        return false;
    }
  }
}
