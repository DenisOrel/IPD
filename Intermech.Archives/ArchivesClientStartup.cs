// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.ArchivesClientStartup
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using ImSSP;
using Intermech.Archives.AutoPlaceInArchiveView;
using Intermech.Archives.BarCodes;
using Intermech.Archives.Common;
using Intermech.Archives.Copies;
using Intermech.Archives.PermittedTypesView;
using Intermech.Archives.ScanDocums;
using Intermech.Archives.StructureView;
using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.DataFormats;
using Intermech.Docking;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Configuration;
using Intermech.Interfaces.Copies;
using Intermech.Interfaces.ECO;
using Intermech.Interfaces.Plugins;
using Intermech.NavBars;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.PropertyEditors;
using Intermech.Protection;
using Intermech.Search;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Archives;

/// <summary>Класс для запуска плагина</summary>
public class ArchivesClientStartup : IPackage, IConfigurable
{
  /// <summary>Плагин проинициализирован</summary>
  private static bool _initialize;

  /// <summary>Плагин проинициализирован</summary>
  /// <value>
  ///   <c>true</c> если плагин загружен; иначе, <c>false</c>.
  /// </value>
  public static bool Initialize => ArchivesClientStartup._initialize;

  /// <summary>Выгрузка плагина</summary>
  public void Unload()
  {
    ArchivesClientStartup._initialize = false;
    if (!(ServicesManager.GetService(typeof (INotificationService)) is INotificationService service))
      return;
    service.Unsubscribe(new NotificationEventHandler(this.PlaceIntoArchive));
  }

  /// <summary>Заголовок плагина</summary>
  public string Name => ServiceHolder.rm.GetString("Archives_3");

  /// <summary>Загрузка плагина</summary>
  /// <param name="serviceProvider">провайдер сервисов</param>
  public void Load(System.IServiceProvider serviceProvider)
  {
    ArchivesClientStartup._initialize = true;
    IProtectionKey service1 = ServicesManager.GetService(typeof (IProtectionKey)) as IProtectionKey;
    if (!(serviceProvider.GetService(typeof (ILicenser)) is ILicenser service2))
      throw new ProtectionException(ServiceHolder.rm.GetString("Archives_54"));
    if (service1 != null)
    {
      service2.AllocateLicense(Consts.appId);
      int index1 = (Environment.TickCount & 15) * 2;
      byte[] queryData = Consts.Key[index1];
      byte[] numArray = Consts.Key[index1 + 1];
      byte[] response = new byte[numArray.Length];
      service1.Query(true, Consts.appId, queryData, response);
      int length = queryData.Length;
      for (int index2 = 0; index2 < length; ++index2)
      {
        if ((int) numArray[index2] != (int) response[index2])
          return;
      }
    }
    (serviceProvider.GetService(typeof (IPluginManager)) as IPluginManager).LoadComplete += new EventHandler(this.pluginManager_LoadComplete);
    BarCodeSettings.Instance.Changed += new EventHandler(this.Instance_Changed);
  }

  private void Instance_Changed(object sender, EventArgs e) => BarCodeListener.Instance.Start();

  /// <summary>Загрузка конфигурации плагина</summary>
  /// <param name="configurationManager">Интерфейс менеджера конфигурации</param>
  public void LoadConfiguration(IConfigurationManager configurationManager)
  {
    IConfiguration configuration1 = configurationManager.Open("Archives") ?? configurationManager.Create("Archives");
    if (configuration1.HasProperty(Consts.ShowInternalDocumsProperty))
      Consts.ShowInternalDocums = Convert.ToBoolean(configuration1.GetProperty(Consts.ShowInternalDocumsProperty));
    if (configuration1.HasProperty(Consts.ColumnsVisibleProperty))
    {
      string[] collection = configuration1.GetProperty(Consts.ColumnsVisibleProperty).Split(';');
      Consts.ColumnsVisible.AddRange((IEnumerable<string>) collection);
    }
    if (configuration1.HasProperty(Consts.GroupByColumnsProperty))
    {
      string[] collection = configuration1.GetProperty(Consts.GroupByColumnsProperty).Split(';');
      Consts.GroupByColumns.AddRange((IEnumerable<string>) collection);
    }
    if (configuration1.HasProperty(Consts.ColumnsWidthProperty))
    {
      string property = configuration1.GetProperty(Consts.ColumnsWidthProperty);
      char[] chArray1 = new char[1]{ ';' };
      foreach (string str in property.Split(chArray1))
      {
        char[] chArray2 = new char[1]{ '=' };
        string[] strArray = str.Split(chArray2);
        if (strArray.Length.Equals(2))
          Consts.ColumnsWidth.Add(strArray[0], Convert.ToInt32(strArray[1]));
      }
    }
    IConfiguration configuration2 = configurationManager.Open("BarCodes") ?? configurationManager.Create("BarCodes");
    string property1 = configuration2.GetProperty("BaudRate");
    if (property1 != null && property1 != "")
      BarCodeSettings.Instance.BaudRate = int.Parse(property1, (IFormatProvider) CultureInfo.InvariantCulture);
    string property2 = configuration2.GetProperty("DataBits");
    if (property2 != null && property2 != "")
      BarCodeSettings.Instance.DataBits = int.Parse(property2, (IFormatProvider) CultureInfo.InvariantCulture);
    string property3 = configuration2.GetProperty("StopBits");
    if (property3 != null && property3 != "")
      BarCodeSettings.Instance.StopBits = (StopBitsEnum) int.Parse(property3, (IFormatProvider) CultureInfo.InvariantCulture);
    string property4 = configuration2.GetProperty("Parity");
    if (property4 != null && property4 != "")
      BarCodeSettings.Instance.Parity = (ParityEnum) int.Parse(property4, (IFormatProvider) CultureInfo.InvariantCulture);
    string property5 = configuration2.GetProperty("Port");
    if (property5 != null && property5 != "")
      BarCodeSettings.Instance.Port = property5;
    string property6 = configuration2.GetProperty("Use");
    if (property6 != null && property6 != "")
      BarCodeSettings.Instance.Use = bool.Parse(property6);
    string property7 = configuration2.GetProperty("OpenMode");
    if (property7 == null || !(property7 != ""))
      return;
    BarCodeSettings.Instance.OpenMode = (OpenModeEnum) int.Parse(property7, (IFormatProvider) CultureInfo.InvariantCulture);
  }

  /// <summary>Сохранение конфигурации плагина</summary>
  /// <param name="configurationManager">Интерфейс менеджера конфигурации</param>
  public void SaveConfiguration(IConfigurationManager configurationManager)
  {
    IConfiguration configuration1 = configurationManager.Open("Archives") ?? configurationManager.Create("Archives");
    configuration1.SetProperty(Consts.ShowInternalDocumsProperty, Consts.ShowInternalDocums.ToString());
    string str1 = string.Join(";", Consts.ColumnsVisible.ToArray());
    configuration1.SetProperty(Consts.ColumnsVisibleProperty, str1);
    string str2 = string.Join(";", Consts.GroupByColumns.ToArray());
    configuration1.SetProperty(Consts.GroupByColumnsProperty, str2);
    List<string> stringList = new List<string>();
    foreach (KeyValuePair<string, int> keyValuePair in Consts.ColumnsWidth)
      stringList.Add($"{keyValuePair.Key}={keyValuePair.Value.ToString()}");
    string str3 = string.Join(";", stringList.ToArray());
    configuration1.SetProperty(Consts.ColumnsWidthProperty, str3);
    IConfiguration configuration2 = configurationManager.Open("BarCodes") ?? configurationManager.Create("BarCodes");
    IConfiguration configuration3 = configuration2;
    int num = BarCodeSettings.Instance.BaudRate;
    string str4 = num.ToString((IFormatProvider) CultureInfo.InvariantCulture);
    configuration3.SetProperty("BaudRate", str4);
    IConfiguration configuration4 = configuration2;
    num = BarCodeSettings.Instance.DataBits;
    string str5 = num.ToString((IFormatProvider) CultureInfo.InvariantCulture);
    configuration4.SetProperty("DataBits", str5);
    IConfiguration configuration5 = configuration2;
    num = (int) BarCodeSettings.Instance.Parity;
    string str6 = num.ToString((IFormatProvider) CultureInfo.InvariantCulture);
    configuration5.SetProperty("Parity", str6);
    IConfiguration configuration6 = configuration2;
    num = (int) BarCodeSettings.Instance.StopBits;
    string str7 = num.ToString((IFormatProvider) CultureInfo.InvariantCulture);
    configuration6.SetProperty("StopBits", str7);
    configuration2.SetProperty("Port", BarCodeSettings.Instance.Port.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    configuration2.SetProperty("Use", BarCodeSettings.Instance.Use.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    IConfiguration configuration7 = configuration2;
    num = (int) BarCodeSettings.Instance.OpenMode;
    string str8 = num.ToString((IFormatProvider) CultureInfo.InvariantCulture);
    configuration7.SetProperty("OpenMode", str8);
  }

  private void pluginManager_LoadComplete(object sender, EventArgs e)
  {
    ServiceHolder.BarManager = ServicesManager.GetService(typeof (BarManager)) as BarManager;
    ServiceHolder.Factory = ServicesManager.GetService(typeof (IFactory)) as IFactory;
    ServiceHolder.GuidMapper = ServicesManager.GetService(typeof (IGuidMapper)) as IGuidMapper;
    ServiceHolder.CategoryTypeIconService = ServicesManager.GetService(typeof (ICategoryTypeIconService)) as ICategoryTypeIconService;
    using (SessionKeeper sk = new SessionKeeper())
    {
      ConstsHolder.ArchiveAttrID = sk.Session.GetAttributeType(ConstsHolder.ArcAttrGuid).AttributeID;
      ConstsHolder.ArcTypeID = sk.Session.GetObjectType(ConstsHolder.ArcTypeGuid).ObjectType;
      ConstsHolder.DocTypeID = sk.Session.GetObjectType(ConstsHolder.DocTypeGuid).ObjectType;
      ConstsHolder.ArchiveStructureAttrID = sk.Session.GetAttributeType(ConstsHolder.ArchiveStructureAttrGuid).AttributeID;
      IDBAttributeType attributeType1 = sk.Session.GetAttributeType(ConstsHolder.ArchivesForSelectionGuid, false);
      if (attributeType1 != null)
        ConstsHolder.ArchivesForSelectionID = attributeType1.AttributeID;
      IDBAttributeType attributeType2 = sk.Session.GetAttributeType(ConstsHolder.CanCreateDocVersionInArchiveGuid, false);
      if (attributeType2 != null)
        ConstsHolder.CanCreateDocVersionInArchiveID = attributeType2.AttributeID;
      IDBAttributeType attributeType3 = sk.Session.GetAttributeType(ConstsHolder.AutoPlaceDocTypesAttrGuid, false);
      if (attributeType3 != null)
        ConstsHolder.AutoPlaceDocTypesAttrID = attributeType3.AttributeID;
      IDBAttributeType attributeType4 = sk.Session.GetAttributeType(ConstsHolder.UsersCanAutoPlaceDocsAttrGuid, false);
      if (attributeType4 != null)
        ConstsHolder.UsersCanAutoPlaceDocsAttrID = attributeType4.AttributeID;
      IArchiveService archiveService;
      try
      {
        archiveService = sk.Session.GetCustomService(typeof (IArchiveService)) as IArchiveService;
      }
      catch
      {
        archiveService = (IArchiveService) null;
      }
      if (archiveService == null)
      {
        int num = (int) MessageBox.Show(ServiceHolder.rm.GetString(sc_441.ssp_archives_442()), ServiceHolder.rm.GetString("Archives_57"));
        return;
      }
      if (ServiceHolder.Factory != null && ServiceHolder.BarManager != null)
      {
        Consts.CategoryArchivesNode = ServiceHolder.GuidMapper.Register(Consts.CategoryArchivesNodeGuid);
        ServiceHolder.Factory.AddNodeType(Consts.CategoryArchivesNode, typeof (ArchivesNode));
        ServiceHolder.Factory.AddCommandsProvider(Consts.CategoryArchivesNode, (ICommandsProvider) new ArchivesContextMenuProvider());
        ServiceHolder.Factory.AddViewsProvider(Consts.CategoryArchivesNode, (IViewsProvider) new AllDocumsProvider());
        ServiceHolder.Factory.AddGlobalNode(new Guid(sc_441.ssp_archives_443()), (IDescriptor) new HiveDescriptor(), 30);
        IDBObjectType objectType = sk.Session.GetObjectType(ConstsHolder.ArcTypeID);
        if (objectType.Icon.Length != 0)
        {
          using (MemoryStream memoryStream = new MemoryStream(objectType.Icon))
          {
            using (Icon icon = new Icon((Stream) memoryStream))
              ServiceHolder.CategoryTypeIconService.AddIcon(icon, Consts.CategoryArchivesNode, -1);
          }
        }
        foreach (int typeID in MetaDataHelper.GetObjectTypeChildrenIDRecursive(ConstsHolder.ArcTypeID))
        {
          ServiceHolder.Factory.AddNodeType(1, typeID, typeof (ArchiveNode));
          ServiceHolder.Factory.AddCommandsProvider(1, typeID, (ICommandsProvider) new ArchiveContextMenuProvider());
          ServiceHolder.Factory.AddViewsProvider(1, typeID, (IViewsProvider) new DocumsProvider());
          ServiceHolder.Factory.AddViewsProvider(1, typeID, (IViewsProvider) new TechDocumentViewProvider());
          ServiceHolder.Factory.AddViewsProvider(1, typeID, (IViewsProvider) new ArchiveStructureProvider());
          ServiceHolder.Factory.AddViewsProvider(1, typeID, (IViewsProvider) new ArchivePermittedTypesProvider());
          ServiceHolder.Factory.AddViewsProvider(1, typeID, (IViewsProvider) new AutoPlaceInArchiveProvider());
          ServiceHolder.Factory.AddViewsProvider(1, typeID, (IViewsProvider) new ColumnsAutoSettingsProvider());
        }
        IColumnSchemes service1 = ServicesManager.GetService(typeof (IColumnSchemes)) as IColumnSchemes;
        service1.Register(ArchivesStructureScheme.ArchivesStructureSchemeGuid, (INodeColumnScheme) new ArchivesStructureScheme());
        ServiceHolder.Factory.AddCommandsProvider(1, ConstsHolder.DocTypeID, (ICommandsProvider) new DocumentCommandsProvider());
        ServiceHolder.Factory.AddCommandsProvider(1, ConstsHolder.CopyOfDocumentID, (ICommandsProvider) new CopyCommandsProvider());
        ServiceHolder.Factory.AddViewsProvider(4, ConstsHolder.DocTypeID, (IViewsProvider) new TechDocumentViewProvider());
        ServiceHolder.Factory.AddViewsProvider(Consts.CategoryArchivesNode, (IViewsProvider) new TechDocumentViewProvider());
        ServiceHolder.Factory.AddViewsProvider(1, (IViewsProvider) new CopiesViewsProvider());
        ServiceHolder.Factory.AddViewsProvider(1, MetaDataHelper.GetObjectTypeID("cad00156-306c-11d8-b4e9-00304f19f545"), (IViewsProvider) new TechDocumentViewProvider());
        ServiceHolder.Factory.AddViewsProvider(1, MetaDataHelper.GetObjectTypeID("cad00157-306c-11d8-b4e9-00304f19f545"), (IViewsProvider) new TechDocumentViewProvider());
        service1.Register(ConstsHolder.CopySchemeName, (INodeColumnScheme) new CopiesColumnScheme());
        if (ServicesManager.GetService(typeof (IPropertyPagesService)) is IPropertyPagesService service2)
        {
          service2.AddPage(ServiceHolder.rm.GetString("Archives_144"), (IPropertyPage) new InventorySettingsControl());
          service2.AddPage("Система\\Настройка штрихкодирования", (IPropertyPage) BarCodeSettings.Instance);
        }
        MenuTemplate contextMenuTemplate = ServiceHolder.Factory.ContextMenuTemplate;
        contextMenuTemplate.BeginUpdate();
        try
        {
          ServiceHolder.Factory.ContextMenuTemplate["EngineeringChangeOrders"]?.Nodes.Add(new MenuTemplateNode("CopyDeliveryListFromECOToDoc", ServiceHolder.rm.GetString("Archives_182"), -1, 34, 6));
          contextMenuTemplate.Nodes.Add(new MenuTemplateNode("AddDocum", ServiceHolder.rm.GetString("Archives_58"), -1, 17, 31 /*0x1F*/));
          contextMenuTemplate.Nodes.Add(new MenuTemplateNode("Register", ServiceHolder.rm.GetString("Archives_59"), -1, 17, 32 /*0x20*/));
          if ((ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole).IsAdmin)
          {
            contextMenuTemplate.Nodes.Add(new MenuTemplateNode("CheckFileStorage", "Проверить размещение файлов в шкафу", -1, 17, 33));
            contextMenuTemplate.Nodes.Add(new MenuTemplateNode("RemoveFilesToStorage", "Перенести файлы в заданный шкаф", -1, 17, 34));
          }
          int imageIndex1 = !(ServicesManager.GetService(typeof (INamedImageList)) is INamedImageList service3) ? -1 : service3.ImageIndex("imgRegDocument");
          contextMenuTemplate.Nodes.Add(new MenuTemplateNode("InventoryNumber", ServiceHolder.rm.GetString("Archives_145"), imageIndex1, 80 /*0x50*/, 0));
          int imageIndex2 = service3 == null ? -1 : service3.ImageIndex("imgUnregisterDoc");
          contextMenuTemplate.Nodes.Add(new MenuTemplateNode("DeleteInventoryNumber", ServiceHolder.rm.GetString("Archives_161"), imageIndex2, 80 /*0x50*/, 1));
          int imageIndex3 = service3 == null ? -1 : service3.ImageIndex("imgCopyListFromDoc");
          contextMenuTemplate.Nodes.Add(new MenuTemplateNode("CopyDeliveryListFromDoc", ServiceHolder.rm.GetString("Archives_165"), imageIndex3, 80 /*0x50*/, 2));
          contextMenuTemplate.Nodes.Add(new MenuTemplateNode("AddSubscriber", ServiceHolder.rm.GetString("Archives_146"), -1, 80 /*0x50*/, 3));
          int imageIndex4 = service3 == null ? -1 : service3.ImageIndex("imgAddSubscrByRoute");
          contextMenuTemplate.Nodes.Add(new MenuTemplateNode("AddSubscribersByRoute", ServiceHolder.rm.GetString("Archives_171"), imageIndex4, 80 /*0x50*/, 4));
          int imageIndex5 = service3 == null ? -1 : service3.ImageIndex("imgCreateCopiesByDeliveryList");
          contextMenuTemplate.Nodes.Add(new MenuTemplateNode("CreateCopiesByDeliveryList", ServiceHolder.rm.GetString("Archives_176"), imageIndex5, 80 /*0x50*/, 5));
          contextMenuTemplate.Nodes.Add(new MenuTemplateNode("ChangeCopiesByDeliveryList", ServiceHolder.rm.GetString("Archives_ChangeCopiesByDeliveryList"), -1, 80 /*0x50*/, 6));
          contextMenuTemplate.Nodes.Add(new MenuTemplateNode("OpenDocumentInNewWindow", ServiceHolder.rm.GetString("Archives_147"), -1, 80 /*0x50*/, 6));
          MenuTemplateNode menuTemplateNode = (MenuTemplateNode) null;
          for (int index = 0; index < contextMenuTemplate.Nodes.Count; ++index)
          {
            MenuTemplateNode node = contextMenuTemplate.Nodes[index];
            if (node.Name.Equals("Create"))
            {
              menuTemplateNode = node;
              break;
            }
          }
          if (menuTemplateNode != null)
          {
            menuTemplateNode.Nodes.Add(new MenuTemplateNode("CreateDocum", ServiceHolder.rm.GetString("Archives_60"), -1, 1, 0));
            menuTemplateNode.Nodes.Add(new MenuTemplateNode(sc_441.ssp_archives_444(), ServiceHolder.rm.GetString("Archives_61"), -1, 1, 1));
            menuTemplateNode.Nodes.Add(new MenuTemplateNode("CreateArchiveProto", ServiceHolder.rm.GetString("Archives_62"), -1, 1, 2));
          }
        }
        finally
        {
          contextMenuTemplate.EndUpdate();
        }
        Icon icon1 = Statics.IconSrv != null ? Statics.IconSrv.GetIcon(Consts.CategoryArchivesNode) : (Icon) null;
        if (ServicesManager.GetService(typeof (IMainMenuService)) is IMainMenuService service4)
        {
          MenuButtonItem menuButtonItem1 = new MenuButtonItem(ServiceHolder.rm.GetString("Archives_65"), new EventHandler(this.ArchivesAndDocumentsClick));
          if (icon1 != null)
            menuButtonItem1.Image = icon1.ToBitmap().GetThumbnailImage(16 /*0x10*/, 16 /*0x10*/, (Image.GetThumbnailImageAbort) null, IntPtr.Zero);
          service4.RegisterMenuItems(MainMenuItemSite.Applications, MainMenuItemPosition.Default, menuButtonItem1);
          MenuButtonItem menuButtonItem2 = new MenuButtonItem(ServiceHolder.rm.GetString("Archives_64"), new EventHandler(this.ShowChildernArchivesEntityClick));
          menuButtonItem2.BeginGroup = false;
          menuButtonItem2.CommandName = "ShowChildrenArchives";
          menuButtonItem2.Checked = Consts.ShowInternalDocums;
          service4.RegisterMenuItems(MainMenuItemSite.ViewMiddle, MainMenuItemPosition.Default, menuButtonItem2);
        }
        if (ServicesManager.GetService(typeof (INavigationBar)) is INavigationBar service6)
        {
          if (service6.FindPane("appPane") is IAppPane pane)
            pane.Add(ServiceHolder.rm.GetString(sc_441.ssp_archives_445()), new EventHandler(this.ArchivesAndDocumentsClick), icon1);
          if (ServicesManager.GetService(typeof (IWellKnownWindowsOpenService)) is IWellKnownWindowsOpenService service5)
            service5.RegisterWindowOpeningHandler(Consts.ArchivesWindowName, new EventHandler(this.ArchivesAndDocumentsClick));
        }
        MenuBar menuBar = ((BarManager) ServicesManager.GetService(typeof (BarManager))).MenuBar;
        menuBar.FindMenuBar("File");
        MenuItemBase menuItem = menuBar.FindMenuItem("File.New");
        if (menuItem != null)
        {
          MenuButtonItem menuButtonItem = new MenuButtonItem(ServiceHolder.rm.GetString("Archives_90"));
          menuButtonItem.CommandName = "New.ScanDocument";
          menuButtonItem.Click += new EventHandler(this.CreateNewScaneDocumentClick);
          menuItem.Items.Add((ToolbarItemBase) menuButtonItem);
        }
        service1.Register(ArchiveStructureColumnScheme.ArchiveStructureShemeGuid, (INodeColumnScheme) new ArchiveStructureColumnScheme());
        if (ServicesManager.GetService(typeof (IAttributePropertyDescriberService)) is IAttributePropertyDescriberService service7 && service7.GetDescriber(ConstsHolder.ArchiveAttrID) == null)
          service7.RegisterDescriber(ConstsHolder.ArchiveAttrID, (IAttributePropertyDescriber) new ArchivesClientStartup.ArchivePropertyDescriber());
        ServicesManager.AddService(typeof (IArchivesDescriptorService), (object) new ArchivesDescriptorService());
        ServicesManager.AddService(typeof (IScanerDocumentService), (object) new ScanerDocumentService());
        ServicesManager.AddService(typeof (ICopiesClientService), (object) new CopiesClientService());
      }
      (ServicesManager.GetService(typeof (IObjectCreatorService)) as IObjectCreatorService).AfterDraftCreatedEvent += new AfterDraftCreatedEventHandler(this.ObjectCreatorDraftCreatedEvent);
      if (sk.Session.GetCustomService(typeof (IColumnCaptionsHelper)) is IColumnCaptionsHelper customService)
        ConstsHolder.ColumnCaptionsCach = customService.FillColumnCaptionsCach();
      if (ServicesManager.GetService(typeof (IContentProvider)) is IContentProvider service8)
        service8.ContentCallback += new GetContentCallback(this.ContentCallback);
      if (ServicesManager.GetService(typeof (INotificationService)) is INotificationService service9)
      {
        service9.Subscribe("ObjectsCreated", new NotificationEventHandler(this.PlaceIntoArchive));
        service9.Subscribe("ObjectsChanged", new NotificationEventHandler(this.OnObjectsChangedEvent));
      }
      ApplicationServices.Container.AddService(typeof (IArchiveColumnsSettingsCacheService), (object) new ArchiveColumnsSettingsCacheService());
      ApplicationServices.Container.AddService<ArchiveHierarchyService>(new ArchiveHierarchyService(sk));
      if (ServicesManager.GetService(typeof (IAttributePropertyDescriberService)) is IAttributePropertyDescriberService service10)
      {
        int objectTypeId = MetaDataHelper.GetObjectTypeID(new Guid("cad00070-306c-11d8-b4e9-00304f19f545"));
        foreach (DataRow dataRow in sk.Session.GetAttributeTypeCollection(-1, true).Select(string.Empty).Select($"F_ATTRIBUTE_TYPE = {8}"))
        {
          int int32 = Convert.ToInt32(dataRow["F_SIZE_TYPE"]);
          if (int32 > 0 && MetaDataHelper.IsObjectTypeChildOf(int32, objectTypeId))
            service10.RegisterDescriber(Convert.ToInt32(dataRow["F_ATTRIBUTE_ID"]), (IAttributePropertyDescriber) new ArchivesDocumentsDescriber());
        }
      }
      this.RegisterViewsAsAdjustable();
    }
    BarCodeListener.Instance.Start();
  }

  /// <summary>Обработка события изменения объекта.</summary>
  /// <param name="sender">The sender.</param>
  /// <param name="e">The <see cref="T:Intermech.Interfaces.Client.NotificationEventArgs" /> instance containing the event data.</param>
  /// <exception cref="T:System.NotImplementedException"></exception>
  private void OnObjectsChangedEvent(object sender, NotificationEventArgs e)
  {
    if (!(e is DBObjectsExtendedEventArgs extendedEventArgs) || extendedEventArgs.ObjectType != ConstsHolder.DeliveryListID)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttribute objectAttributeById = sessionKeeper.Session.GetObjectAttributeByID(extendedEventArgs.ObjectIDs[0], ConstsHolder.OriginalObjectID);
      if (objectAttributeById == null || objectAttributeById.Value == null)
        return;
      long asInteger = objectAttributeById.AsInteger;
      IDBObject objectById = sessionKeeper.Session.GetObjectByID(asInteger, false);
      if (!MetaDataHelper.IsObjectTypeChildOf(objectById.ObjectType, MetaDataHelper.GetObjectTypeID(new Guid("cad00348-306c-11d8-b4e9-00304f19f545"))))
        return;
      IECOServer customService1 = sessionKeeper.Session.GetCustomService(typeof (IECOServer)) as IECOServer;
      ICopiesService customService2 = sessionKeeper.Session.GetCustomService(typeof (ICopiesService)) as ICopiesService;
      if (customService1 == null || customService2 == null || !customService1.GetDeliveryListParam())
        return;
      long deliveryListId = customService2.GetDeliveryListID(sessionKeeper.Session.SessionGUID, objectById.ID);
      if (deliveryListId == 0L)
        return;
      foreach (KeyValuePair<long, long> keyValuePair in customService1.GetDocsIDsInfoFromECOComposition(objectById.ObjectID, sessionKeeper.Session.SessionGUID))
      {
        long docID = keyValuePair.Value;
        long key = keyValuePair.Key;
        customService2.AddSubscrsFromEcoToDoc(deliveryListId, docID, key, sessionKeeper.Session.SessionGUID);
      }
    }
  }

  /// <summary>
  /// обработка события создания объекта.
  /// необходимо для помещения в архив имортируемого документа
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void PlaceIntoArchive(object sender, NotificationEventArgs e)
  {
    if (!(e is DBObjectsEventArgs objectsEventArgs) || !(e.EventName == "ObjectsCreated") || objectsEventArgs.ObjectIDs.Count != 1)
      return;
    this.ObjectCreatorDraftCreatedEvent((object) this, new AfterDraftCreatedEventArgs(objectsEventArgs.ObjectTypeIDs[0], objectsEventArgs.ObjectIDs[0]));
  }

  private static void AttributesPatch(IUserSession session)
  {
    IDBObjectType objectType = session.GetObjectType(new Guid("cad00119-306c-11d8-b4e9-00304f19f545"), false);
    if (MetaDataHelper.GetAttribute4ObjectType(new Guid("cad00119-306c-11d8-b4e9-00304f19f545"), ConstsHolder.ArchivesForSelectionGuid) != null)
      return;
    IDBAttribute4ObjectTypeCollection attributes = objectType.Attributes as IDBAttribute4ObjectTypeCollection;
    ConstsHolder.ArchivesForSelectionID = session.GetAttributeTypeCollection(-1).Create(new AttributeTypeProperties(ServiceHolder.rm.GetString("Archives_142"), FieldTypes.ftObjectLink)
    {
      AttributeGuid = ConstsHolder.ArchivesForSelectionGuid,
      FieldType = FieldTypes.ftObjectLink,
      Note = ServiceHolder.rm.GetString("Archives_148"),
      IsContent = false,
      OptimizationMode = OptimizationModes.Seek,
      MultiValueMode = MultiValueModes.MultiValues
    });
    Attribute4ObjectTypeProperties attrProperties = new Attribute4ObjectTypeProperties(ConstsHolder.ArchivesForSelectionID, objectType.ObjectType, InheritModes.Public, RequiredModes.Manual, string.Empty, ComputeValueModes.NotComputableValue, string.Empty, UniqueValueModes.NotUnique, 0, (object) string.Empty, OptimizationModes.Seek, false, AttributeOptions.None, string.Empty, 0, 0);
    attributes.Create(attrProperties);
  }

  /// <summary>
  /// Обработка события создания заготовки объекта
  /// Если стоим на архиве и создаём документ,
  /// то в атрибут Архив документа записываем id архива
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void ObjectCreatorDraftCreatedEvent(object sender, AfterDraftCreatedEventArgs e)
  {
    if (!MetaDataHelper.IsObjectTypeChildOf(e.ObjectTypeID, ConstsHolder.DocTypeID))
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(e.ObjectID, false);
      if (dbObject == null || !(ServicesManager.GetService(typeof (ICurrentNavWindow)) is ICurrentNavWindow service) || !(service.TreeView is NavigatorTreeView treeView))
        return;
      NavigatorTreeNode node = treeView.FocusedNode;
      long objectId;
      if (node != null && node.NodeID != null && node.Handler != null && (node.Handler.GetType().Equals(typeof (ArchiveNode)) || this.OneOfParentIsArchiveNode(node) != null))
      {
        if (!node.Handler.GetType().Equals(typeof (ArchiveNode)))
          node = this.OneOfParentIsArchiveNode(node);
        if (node.Parent.Handler == null)
        {
          if (!(treeView.RootDescriptor is Intermech.Navigator.DBObjects.Descriptor rootDescriptor))
            return;
          objectId = rootDescriptor.ObjectID;
        }
        else
        {
          if (!(node.Parent.Handler.GetData(node.NodeID, typeof (IDBTypedObjectID)) is IDBTypedObjectID data))
            return;
          objectId = data.ObjectID;
        }
      }
      else
      {
        if (!(service.ViewsManagers is IViewsManager viewsManagers) || viewsManagers.ActiveViewPage == null || !(viewsManagers.ActiveViewPage.Control is ISelectedItemsHost control))
          return;
        ISelectedItems selectedItems = control.SelectedItems;
        if (selectedItems == null || selectedItems.Count != 1 || !(selectedItems.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData) || !MetaDataHelper.IsObjectTypeChildOf(itemData.ObjectType, ConstsHolder.ArcTypeID))
          return;
        objectId = itemData.ObjectID;
      }
      if (objectId == 0L)
        return;
      IDBAttribute attributeById = dbObject.GetAttributeByID(ConstsHolder.ArchiveAttrID);
      if (attributeById != null && attributeById.Value.GetType().Equals(typeof (long)))
        return;
      IDBAttribute objectAttributeById = sessionKeeper.Session.GetObjectAttributeByID(objectId, ConstsHolder.CanCreateDocVersionInArchiveID);
      if (dbObject.VersionID != 0 && (objectAttributeById == null || !objectAttributeById.AsBoolean))
        return;
      AttributeValues attributeValues = new AttributeValues(ConstsHolder.ArchiveAttrID, (object) objectId);
      dbObject.SetAttributesValues(new AttributeValues[1]
      {
        attributeValues
      });
    }
  }

  /// <summary>
  /// Проверить, есть ли среди родителей нода какой-нибудь архив
  /// </summary>
  /// <param name="node"></param>
  /// <returns>null, если среди родителей нет архива</returns>
  private NavigatorTreeNode OneOfParentIsArchiveNode(NavigatorTreeNode node)
  {
    NavigatorTreeNode navigatorTreeNode = (NavigatorTreeNode) null;
    if (node == null)
      return navigatorTreeNode;
    for (NavigatorTreeNode parent = node.Parent; parent != null; parent = parent.Parent)
    {
      if (parent.Handler != null && parent.Handler.GetType().Equals(typeof (ArchiveNode)))
      {
        navigatorTreeNode = parent;
        break;
      }
    }
    return navigatorTreeNode;
  }

  private void ShowChildernArchivesEntityClick(object sender, EventArgs e)
  {
    if (!(sender is MenuButtonItem))
      return;
    MenuButtonItem menuButtonItem = sender as MenuButtonItem;
    Consts.ShowInternalDocums = !Consts.ShowInternalDocums;
    int num = Consts.ShowInternalDocums ? 1 : 0;
    menuButtonItem.Checked = num != 0;
  }

  /// <summary>
  /// вызов команды меню
  /// Сканировать документ
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void CreateNewScaneDocumentClick(object sender, EventArgs e)
  {
    new ScanedDocumentCreator().CreateDocument();
  }

  /// <summary>восстановление окон</summary>
  /// <param name="guid"></param>
  /// <param name="persistString"></param>
  /// <returns></returns>
  internal DockControl ContentCallback(Guid guid, string persistString)
  {
    IWellKnownNavigators service = (IWellKnownNavigators) ServicesManager.GetService(typeof (IWellKnownNavigators));
    return guid == Consts.ArchivesWindowGuid ? (DockControl) service.Get(Consts.ArchivesWindowName) ?? (DockControl) this.CreateArchivesWindow() : (DockControl) null;
  }

  private void ArchivesAndDocumentsClick(object sender, EventArgs e)
  {
    WellKnownNavWindow wellKnownNavWindow = (WellKnownNavWindow) ((IWellKnownNavigators) ServicesManager.GetService(typeof (IWellKnownNavigators))).Get(Consts.ArchivesWindowName);
    DockManager service = (DockManager) ServicesManager.GetService(typeof (DockManager));
    if (wellKnownNavWindow == null)
    {
      DockControl dockControl = service.FindDockControl(Consts.ArchivesWindowGuid);
      if (dockControl != null)
      {
        dockControl.Activate();
        wellKnownNavWindow = service.FindDockControl(Consts.ArchivesWindowGuid) as WellKnownNavWindow;
      }
    }
    if (wellKnownNavWindow == null)
      wellKnownNavWindow = this.CreateArchivesWindow();
    wellKnownNavWindow.Show(service);
    wellKnownNavWindow.Activate();
  }

  private WellKnownNavWindow CreateArchivesWindow()
  {
    WellKnownNavWindow archivesWindow = new WellKnownNavWindow();
    archivesWindow.WellKnownName = Consts.ArchivesWindowName;
    archivesWindow.Guid = Consts.ArchivesWindowGuid;
    archivesWindow.Text = ServiceHolder.rm.GetString("Archives_65");
    if (ServiceHolder.CategoryTypeIconService != null)
    {
      int index = ServiceHolder.CategoryTypeIconService.IndexOf(Consts.CategoryArchivesNode, -1, (object) null);
      if (index >= 0)
        archivesWindow.TabImage = ServiceHolder.CategoryTypeIconService.ImageList.Images[index];
    }
    IDescriptor rootDescriptor = (IDescriptor) new HiveDescriptor();
    archivesWindow.TreeView.OnGetSupportedColumnsEventHandler += new GetSupportedColumnsEventHandler(Intermech.Navigator.Utils.GetNavigatorColumns);
    archivesWindow.TreeView.SetColumns(Intermech.Navigator.Utils.CaptionColumnOnly(NodeColumnSortOrder.Ascending));
    archivesWindow.TreeView.Build(rootDescriptor);
    return archivesWindow;
  }

  /// <summary>
  /// Зарегистрировать все закладки, добавляемые модулем расширения в Навигатор
  /// </summary>
  internal void RegisterViewsAsAdjustable()
  {
    AdjustableViewsHelper.RegisterView("AllDocumsObject", ServiceHolder.rm.GetString("Archives_1"), "Документы", "", "", true, 0);
    AdjustableViewsHelper.RegisterView("ArchiveStructureView", ServiceHolder.rm.GetString("Archives_74"), ServiceHolder.rm.GetString("Archives_204"), ServiceHolder.rm.GetString("Archives_142"), "imgListView", true, 23);
    AdjustableViewsHelper.RegisterView("TechDocumView", ServiceHolder.rm.GetString("Archives_140"), ServiceHolder.rm.GetString("Archives_141"), ServiceHolder.rm.GetString("Archives_142"), string.Empty, true, 24);
    AdjustableViewsHelper.RegisterView("CopiesView", ServiceHolder.rm.GetString("Archives_99"), ServiceHolder.rm.GetString("Archives_143"), ServiceHolder.rm.GetString("Archives_142"), string.Empty, true, 27);
    AdjustableViewsHelper.RegisterView("ArchivePermittedTypesView", ServiceHolder.rm.GetString("Archives_155"), ServiceHolder.rm.GetString("Archives_203"), ServiceHolder.rm.GetString("Archives_142"), "", true, 28);
    AdjustableViewsHelper.RegisterView("AutoPlaceInArchiveView", ServiceHolder.rm.GetString("Archives_184"), ServiceHolder.rm.GetString("Archives_202"), ServiceHolder.rm.GetString("Archives_142"), "", true, 29);
    AdjustableViewsHelper.RegisterView("ColumnsAutoSettingsView", ServiceHolder.rm.GetString("Archives_213"), ServiceHolder.rm.GetString("Archives_214"), ServiceHolder.rm.GetString("Archives_142"), "", true, 30);
  }

  /// <summary>
  /// 
  /// </summary>
  public class ArchiveProxy
  {
    private string _caption = ServiceHolder.rm.GetString("Archives_68");

    /// <summary>
    /// 
    /// </summary>
    public bool IsArchive { get; private set; }

    /// <summary>Идентификатор объекта.</summary>
    public long Value { get; private set; }

    /// <summary>Конструктор.</summary>
    /// <param name="id">Идентификатор объекта</param>
    public ArchiveProxy(long id)
    {
      this.Value = id;
      this.IsArchive = false;
      if (id == -1L)
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(id, false);
        if (dbObject == null)
          return;
        this._caption = dbObject.Caption;
        if (string.IsNullOrEmpty(this._caption))
          this._caption = string.Format(ServiceHolder.rm.GetString("Archives_69"), (object) this.Value);
        this.IsArchive = dbObject.isParentType(ConstsHolder.ArcTypeGuid);
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override string ToString() => this._caption;
  }

  /// <summary>
  /// 
  /// </summary>
  internal class ArchivePropertyDescriber : IAttributePropertyDescriber
  {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="attributeId"></param>
    /// <param name="baseType"></param>
    /// <returns></returns>
    public System.Type GetPropDescriptorType(int attributeId, FieldTypes baseType)
    {
      return typeof (ArchivesClientStartup.ArchiveProxy);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="attributeId"></param>
    /// <returns></returns>
    public object GetPropDescriptorEditor(int attributeId)
    {
      return (object) new ArchivesClientStartup.ArchiveEditor();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="attributeId"></param>
    /// <returns></returns>
    public TypeConverter GetPropDescriptorConverter(int attributeId) => (TypeConverter) null;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="attributeId"></param>
    /// <param name="baseReadonly"></param>
    /// <returns></returns>
    public bool GetPropDescriptorReadonly(int attributeId, bool baseReadonly) => baseReadonly;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="attributeId"></param>
    /// <param name="baseReset"></param>
    /// <returns></returns>
    public bool GetPropDescriptorReset(int attributeId, bool baseReset) => true;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="attributeId"></param>
    /// <param name="baseMask"></param>
    /// <returns></returns>
    public string GetPropDescriptorMask(int attributeId, string baseMask) => baseMask;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="elementInfo"></param>
    /// <param name="attributeId"></param>
    /// <param name="actualValue"></param>
    /// <returns></returns>
    public object GetPropDescriptorValue(
      IElementInfo elementInfo,
      int attributeId,
      object actualValue)
    {
      return actualValue == null || !(actualValue.GetType() == typeof (long)) && !(actualValue.GetType() == typeof (int)) ? (object) null : (object) new ArchivesClientStartup.ArchiveProxy(Convert.ToInt64(actualValue));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="elementInfo"></param>
    /// <param name="attributeId"></param>
    /// <param name="propertyValue"></param>
    /// <returns></returns>
    public object GetAttributeValue(
      IElementInfo elementInfo,
      int attributeId,
      object propertyValue)
    {
      object attributeValue = (object) null;
      if (propertyValue != null && propertyValue != DBNull.Value)
        attributeValue = (object) (propertyValue as ArchivesClientStartup.ArchiveProxy).Value;
      return attributeValue;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="attributeId"></param>
    /// <param name="attributeProcessor"></param>
    /// <returns></returns>
    public TypeConverter GetConverter(int attributeId, object attributeProcessor)
    {
      return (TypeConverter) null;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public class ArchiveEditor : UITypeEditor
  {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
    {
      return UITypeEditorEditStyle.Modal;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="provider"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public override object EditValue(
      ITypeDescriptorContext context,
      System.IServiceProvider provider,
      object value)
    {
      object obj = value;
      long num = value is ArchivesClientStartup.ArchiveProxy ? (value as ArchivesClientStartup.ArchiveProxy).Value : 0L;
      ServiceContainer nodesContext = new ServiceContainer();
      nodesContext.AddService(typeof (ViewArchives), (object) new ViewArchives());
      IDescriptor rootDescriptor = (IDescriptor) new HiveDescriptor(ServiceHolder.rm.GetString("Archives_3"));
      long[] numArray = Intermech.Navigator.SelectionWindow.SelectObjects(ServiceHolder.rm.GetString(sc_441.ssp_archives_446()), string.Empty, rootDescriptor, (System.IServiceProvider) nodesContext, SelectionOptions.Default);
      if (numArray != null && numArray.Length != 0 && num != numArray[0])
      {
        ArchivesClientStartup.ArchiveProxy archiveProxy = new ArchivesClientStartup.ArchiveProxy(numArray[0]);
        obj = archiveProxy.IsArchive ? (object) archiveProxy : (object) (ArchivesClientStartup.ArchiveProxy) null;
      }
      return obj;
    }
  }
}
