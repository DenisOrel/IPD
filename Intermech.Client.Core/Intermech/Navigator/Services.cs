
// Type: Intermech.Navigator.Services
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.DataFormats;
using Intermech.Docking;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Configuration;
using Intermech.Navigator.Conditions;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.Drawing;
using System.IO;


namespace Intermech.Navigator;

/// <summary>Службы "Навигатора"</summary>
public sealed class Services
{
  /// <summary>Полный путь к ресурсам "Навигатора"</summary>
  private const string _resourcesNamespace = "Intermech.Client.Core.Navigator.Resources.";
  /// <summary>
  /// Значок для "Общие выборки", если значение атрибута "Ручная выборка" равно true
  /// </summary>
  private static Icon _selectionCommonManualIcon;
  /// <summary>
  /// Значок для "Персональные выборки", если значение атрибута "Ручная выборка" равно true
  /// </summary>
  private static Icon _selectionPersonalManualIcon;
  /// <summary>
  /// Значок для "Общие выборки", если значение атрибута "Ручная выборка" равно false
  /// </summary>
  private static Icon _selectionCommonIcon;
  /// <summary>
  /// Значок для "Персональные выборки", если значение атрибута "Ручная выборка" равно false
  /// </summary>
  private static Icon _selectionPersonalIcon;
  /// <summary>Тип объекта "Общие выборки"</summary>
  public static int _objectTypeIDCommonSelection = -1;
  /// <summary>Тип объекта "Персональные выборки"</summary>
  public static int _objectTypeIDPersonalSelection = -1;
  public static long IconsCount = 0;

  /// <summary>Выполнить инициализацию служб "Навигатора"</summary>
  public static void Start()
  {
    Holder.IconService = (ICategoryTypeIconService) ServicesManager.GetService(typeof (ICategoryTypeIconService));
    Holder.ImageService = (ICategoryTypeStateImageService) ServicesManager.GetService(typeof (ICategoryTypeStateImageService));
    Holder.BarManager = (BarManager) ServicesManager.GetService(typeof (BarManager));
    Holder.DockManager = (DockManager) ServicesManager.GetService(typeof (DockManager));
    Holder.HistoryManager = (INavigateManager) ServicesManager.GetService(typeof (INavigateManager));
    Holder.CommandManager = (ICommandManager) ServicesManager.GetService(typeof (ICommandManager));
    Holder.ContentProvider = (IContentProvider) ServicesManager.GetService(typeof (IContentProvider));
    Holder.NamedImageList = (INamedImageList) ServicesManager.GetService(typeof (INamedImageList));
    Holder.ConfigurationManager = (IConfigurationManager) ServicesManager.GetService(typeof (IConfigurationManager));
    Holder.NotificationService = (INotificationService) ServicesManager.GetService(typeof (INotificationService));
    Holder.ColumnSchemes = (IColumnSchemes) new ColumnSchemes();
    Holder.StringMapper = (IStringMapper) new StringMapper();
    Holder.Factory = (IFactory) new Factory();
    Holder.WellKnownNavigators = (IWellKnownNavigators) new WellKnowNavigators();
    Holder.ClientPluginsService = (IClientPluginsService) new ClientPluginsService();
    Holder.ElementStatusesClientService = (IElementStatusesClientService) new ElementStatusesClientService();
    Holder.DefaultCommands4ObjTypes = (IDefaultCommands4ObjTypes) new DefaultCommands4ObjTypes();
    Holder.NavGraphicsCache = (INavGraphicsCache) new NavGraphicsCache();
    Holder.EnableTreeMultiSelectService = (IEnableTreeMultiSelectService) new EnableTreeMultiSelectService();
    Holder.EnableTreeColumnsSortingService = (IEnableTreeColumnsSortingService) new EnableTreeColumnsSortingService();
    Holder.NavigatorTreeCollapseService = (INavigatorTreeCollapseService) new NavigatorTreeCollapseService();
    Holder.ObjectsCheckOutService = (IObjectsCheckOutService) new ObjectsCheckOutService();
    ServicesManager.AddService(typeof (INavigatorColumnsService), (object) new NavigatorColumnsService());
    ServicesManager.AddService(typeof (IColumnSchemes), (object) Holder.ColumnSchemes);
    ServicesManager.AddService(typeof (IStringMapper), (object) Holder.StringMapper);
    ServicesManager.AddService(typeof (IFactory), (object) Holder.Factory);
    ServicesManager.AddService(typeof (IWellKnownNavigators), (object) Holder.WellKnownNavigators);
    ServicesManager.AddService(typeof (IClientPluginsService), (object) Holder.ClientPluginsService);
    ServicesManager.AddService(typeof (IDefaultCommands4ObjTypes), (object) Holder.DefaultCommands4ObjTypes);
    ServicesManager.AddService(typeof (INavGraphicsCache), (object) Holder.NavGraphicsCache);
    ServicesManager.AddService(typeof (IEnableTreeMultiSelectService), (object) Holder.EnableTreeMultiSelectService);
    ServicesManager.AddService(typeof (IEnableTreeColumnsSortingService), (object) Holder.EnableTreeColumnsSortingService);
    ServicesManager.AddService(typeof (INavigatorTreeCollapseService), (object) Holder.NavigatorTreeCollapseService);
    ServicesManager.AddService(typeof (IObjectsCheckOutService), (object) Holder.ObjectsCheckOutService);
    ServicesManager.AddService(typeof (IConditionEditorAttributeService), (object) new ConditionEditorAttributeService());
    ServicesManager.AddService(typeof (IConditionsFormService), (object) new ConditionsFormService());
    ServicesManager.AddService(typeof (IConditionControllersService), (object) new ConditionControllersService());
    ServicesManager.AddService(typeof (IConditionDisplayService), (object) new ConditionDisplayService());
    ConditionDataProviderService serviceInstance = new ConditionDataProviderService();
    serviceInstance.Register(SelectionDataSource.DataBase, (IConditionDataProvider) new DBConditionDataProvider());
    ServicesManager.AddService(typeof (IConditionDataProviderService), (object) serviceInstance);
    ServicesManager.AddService(typeof (ICurrentUserAndRole), (object) new CurrentUserAndRole());
    IDefaultCommand commands4ObjType = Holder.DefaultCommands4ObjTypes[new Guid("6e9f08f2-963f-4126-baaa-2d33cd1dc10c"), true];
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IElementStatusesService customService1 = sessionKeeper.Session.GetCustomService(typeof (IElementStatusesService)) as IElementStatusesService;
      IPluginStatusesTable customService2 = sessionKeeper.Session.GetCustomService(typeof (IPluginStatusesTable)) as IPluginStatusesTable;
      Holder.ElementStatusesClientService.SyncWithServerSide(customService1, customService2);
      Services._objectTypeIDCommonSelection = sessionKeeper.Session.IdentHelper.GetObjectTypeID("cad00122-306c-11d8-b4e9-00304f19f545");
      Services._objectTypeIDPersonalSelection = sessionKeeper.Session.IdentHelper.GetObjectTypeID("cad00123-306c-11d8-b4e9-00304f19f545");
      Holder.ElementStatusesClientService.LoadUserSettings(sessionKeeper.Session);
    }
    ServicesManager.AddService(typeof (IElementStatusesClientService), (object) Holder.ElementStatusesClientService);
    Services.PreserveStandartCategories();
    Services.RegisterColumnSchemes();
    Holder.Factory.AddCommandsProvider((ICommandsProvider) new CommonCommandsProvider());
    Holder.Factory.AddViewsProvider((IViewsProvider) new CommonViewsProvider());
    Services.RegisterCustomCategories();
    Statics.InitAttributeReadonlyBlacklist();
    Holder.IconService.FindIcon += new FindIconEventHandler(Services.FindDbEntityIcon);
    Holder.IconService.FindIcon += new FindIconEventHandler(Services.GetDescriptorNodeIcon);
  }

  public static void Stop()
  {
    ServicesManager.RemoveService(typeof (IObjectsCheckOutService));
    ServicesManager.RemoveService(typeof (IEnableTreeMultiSelectService));
    ServicesManager.RemoveService(typeof (INavGraphicsCache));
    ServicesManager.RemoveService(typeof (INavigatorColumnsService));
    ServicesManager.RemoveService(typeof (IColumnSchemes));
    ServicesManager.RemoveService(typeof (IGuidMapper));
    ServicesManager.RemoveService(typeof (IStringMapper));
    ServicesManager.RemoveService(typeof (IFactory));
    ServicesManager.RemoveService(typeof (IWellKnownNavigators));
    ServicesManager.RemoveService(typeof (IClientPluginsService));
    ServicesManager.RemoveService(typeof (IDefaultCommands4ObjTypes));
    ServicesManager.RemoveService(typeof (ICreateObjByTypeMRU));
    ServicesManager.RemoveService(typeof (ICurrentUserAndRole));
    Holder.ColumnSchemes = (IColumnSchemes) null;
    Holder.GuidMapper = (IGuidMapper) null;
    Holder.StringMapper = (IStringMapper) null;
    Holder.Factory = (IFactory) null;
    Holder.WellKnownNavigators = (IWellKnownNavigators) null;
    Holder.EnableTreeMultiSelectService = (IEnableTreeMultiSelectService) null;
    Holder.NavGraphicsCache.Clear();
    Holder.NavGraphicsCache = (INavGraphicsCache) null;
    Holder.IconService = (ICategoryTypeIconService) null;
    Holder.ImageService = (ICategoryTypeStateImageService) null;
    Holder.BarManager = (BarManager) null;
    Holder.DockManager = (DockManager) null;
    Holder.HistoryManager = (INavigateManager) null;
    Holder.CommandManager = (ICommandManager) null;
    Holder.ContentProvider = (IContentProvider) null;
    Holder.NamedImageList = (INamedImageList) null;
    Holder.ConfigurationManager = (IConfigurationManager) null;
    Holder.NotificationService = (INotificationService) null;
    Holder.DefaultCommands4ObjTypes = (IDefaultCommands4ObjTypes) null;
    Holder.ElementStatusesClientService = (IElementStatusesClientService) null;
  }

  public static Stream GetResourceStream(string resourceName)
  {
    return Services.GetResourceStream("Intermech.Client.Core.Navigator.Resources.", resourceName);
  }

  public static Stream GetResourceStream(string resourcesNamespace, string resourceName)
  {
    return typeof (Services).Assembly.GetManifestResourceStream(resourcesNamespace + resourceName);
  }

  private static void MapGuids()
  {
    IGuidMapper guidMapper = (IGuidMapper) new GuidMapper();
    Holder.GuidMapper = guidMapper;
    ServicesManager.AddService(typeof (IGuidMapper), (object) Holder.GuidMapper);
    if (Statics.CategoryAttributes == 0)
      Statics.CategoryAttributes = guidMapper.Register(Statics.CategoryAttributesGUID);
    if (Statics.CategorySubjectAreas == 0)
      Statics.CategorySubjectAreas = guidMapper.Register(Statics.CategorySubjectAreasGUID);
    if (Statics.CategoryObjectTypes == 0)
      Statics.CategoryObjectTypes = guidMapper.Register(Statics.CategoryObjectTypesGUID);
    if (Statics.CategoryRelationTypes == 0)
      Statics.CategoryRelationTypes = guidMapper.Register(Statics.CategoryRelationTypesGUID);
    if (Statics.CategoryLCLevels == 0)
      Statics.CategoryLCLevels = guidMapper.Register(Statics.CategoryLCLevelsGUID);
    if (Statics.CategoryLanguages == 0)
      Statics.CategoryLanguages = guidMapper.Register(Statics.CategoryLanguagesGUID);
    if (Statics.CategoryLCSchemas != 0)
      return;
    Statics.CategoryLCSchemas = guidMapper.Register(Statics.CategoryLCSchemasGUID);
  }

  public static void LoadIconsFromResources()
  {
    Services.MapGuids();
    try
    {
      string str = "Intermech.Client.Core.Resources.";
      if (Services._selectionCommonManualIcon == null)
      {
        using (Stream resourceStream = Services.GetResourceStream(str, "SelectionsCommon_Manual.ico"))
          Services._selectionCommonManualIcon = new Icon(resourceStream);
      }
      if (Services._selectionCommonIcon == null)
      {
        using (Stream resourceStream = Services.GetResourceStream(str, "SelectionsCommon.ico"))
          Services._selectionCommonIcon = new Icon(resourceStream);
      }
      if (Services._selectionPersonalManualIcon == null)
      {
        using (Stream resourceStream = Services.GetResourceStream(str, "SelectionsPersonal_Manual.ico"))
          Services._selectionPersonalManualIcon = new Icon(resourceStream);
      }
      if (Services._selectionPersonalIcon == null)
      {
        using (Stream resourceStream = Services.GetResourceStream(str, "SelectionsPersonal.ico"))
          Services._selectionPersonalIcon = new Icon(resourceStream);
      }
      if (Statics.IconSrv == null)
        return;
      using (Stream resourceStream = Services.GetResourceStream(str, "Attributes.ico"))
      {
        Icon icon = new Icon(resourceStream);
        if (icon != null)
        {
          Statics.IconSrv.AddIcon(icon, Statics.CategoryAttributes, 0);
          Statics.IconSrv.AddIcon(icon, 3, 0);
          Statics.IconSrv.AddIcon(icon, 12, -1);
          icon.Dispose();
        }
      }
      using (Stream resourceStream = Services.GetResourceStream(str, "AttributeGroup.ico"))
      {
        Icon icon = new Icon(resourceStream);
        if (icon != null)
        {
          Statics.IconSrv.AddIcon(icon, 12, 0);
          icon.Dispose();
        }
      }
      using (Stream resourceStream = Services.GetResourceStream(str, "SubjectArea.ico"))
      {
        Icon icon = new Icon(resourceStream);
        if (icon != null)
        {
          Statics.IconSrv.AddIcon(icon, 11, 0);
          Statics.IconSrv.AddIcon(icon, Statics.CategorySubjectAreas, 0);
          icon.Dispose();
        }
      }
      using (Stream resourceStream = Services.GetResourceStream(str, "ObjectTypes.ico"))
      {
        Icon icon = new Icon(resourceStream);
        if (icon != null)
        {
          Statics.IconSrv.AddIcon(icon, 4, 0);
          Statics.IconSrv.AddIcon(icon, Statics.CategoryObjectTypes, 0);
          icon.Dispose();
        }
      }
      using (Stream resourceStream = Services.GetResourceStream(str, "RelationTypes.ico"))
      {
        Icon icon = new Icon(resourceStream);
        if (icon != null)
        {
          Statics.IconSrv.AddIcon(icon, Statics.CategoryRelationTypes, 0);
          Statics.IconSrv.AddIcon(icon, 6, 0);
          icon.Dispose();
        }
      }
      using (Stream resourceStream = Services.GetResourceStream(str, "LCLevels.ico"))
      {
        Icon icon = new Icon(resourceStream);
        if (icon != null)
        {
          Statics.IconSrv.AddIcon(icon, Statics.CategoryLCLevels, 0);
          Statics.IconSrv.AddIcon(icon, 8, 0);
          icon.Dispose();
        }
      }
      using (Stream resourceStream = Services.GetResourceStream(str, "LCSchemas.ico"))
      {
        Icon icon = new Icon(resourceStream);
        if (icon != null)
        {
          Statics.IconSrv.AddIcon(icon, Statics.CategoryLCSchemas, 0);
          Statics.IconSrv.AddIcon(icon, 16 /*0x10*/, 0);
          icon.Dispose();
        }
      }
      using (Stream resourceStream = Services.GetResourceStream(str, "Languages.ico"))
      {
        Icon icon = new Icon(resourceStream);
        if (icon != null)
        {
          Statics.IconSrv.AddIcon(icon, Statics.CategoryLanguages, 0);
          Statics.IconSrv.AddIcon(icon, 9, 0);
          icon.Dispose();
        }
      }
      using (Stream resourceStream = Services.GetResourceStream(str, "System.ico"))
      {
        Icon icon = new Icon(resourceStream);
        if (icon != null)
        {
          Statics.IconSrv.AddIcon(icon, 14, 0);
          icon.Dispose();
        }
      }
      using (Stream resourceStream = Services.GetResourceStream(str, "Snapshot.ico"))
      {
        Icon icon = new Icon(resourceStream);
        if (icon != null)
        {
          ((INamedImageList) ServicesManager.GetService(typeof (INamedImageList))).Add(icon, "imgSnapshot");
          Statics.IconSrv.AddIcon(icon, 23, 0);
          icon.Dispose();
        }
      }
      Services.LoadIcon4AttributeType(str, "ftAutoInc.ico", FieldTypes.ftAutoInc);
      Services.LoadIcon4AttributeType(str, "ftBoolean.ico", FieldTypes.ftBoolean);
      Services.LoadIcon4AttributeType(str, "ftDate.ico", FieldTypes.ftDateTime);
      Services.LoadIcon4AttributeType(str, "ftDouble.ico", FieldTypes.ftDouble);
      Services.LoadIcon4AttributeType(str, "ftExternalLink.ico", FieldTypes.ftExternalLink);
      Services.LoadIcon4AttributeType(str, "ftFile.ico", FieldTypes.ftFile);
      Services.LoadIcon4AttributeType(str, "ftGuid.ico", FieldTypes.ftGuid);
      Services.LoadIcon4AttributeType(str, "ftInteger.ico", FieldTypes.ftInteger);
      Services.LoadIcon4AttributeType(str, "ftMeasured.ico", FieldTypes.ftMeasured);
      Services.LoadIcon4AttributeType(str, "ftMemo.ico", FieldTypes.ftMemo);
      Services.LoadIcon4AttributeType(str, "ftObjectLink.ico", FieldTypes.ftObjectLink);
      Services.LoadIcon4AttributeType(str, "ftPassword.ico", FieldTypes.ftPassword);
      Services.LoadIcon4AttributeType(str, "ftShortBlob.ico", FieldTypes.ftShortBlob);
      Services.LoadIcon4AttributeType(str, "ftString.ico", FieldTypes.ftString);
      Services.LoadIcon4AttributeType(str, "ftSystem.ico", FieldTypes.ftSystem);
      Services.LoadIcon4AttributeType(str, "ftBlob.ico", FieldTypes.ftBlob);
      Services.LoadIcon4AttributeType(str, "ftObjectLinkByID.ico", FieldTypes.ftObjectLinkByID);
    }
    catch
    {
    }
  }

  private static void RegisterCustomCategories()
  {
    IGuidMapper service = ServicesManager.GetService(typeof (IGuidMapper)) as IGuidMapper;
    if (Statics.CategoryStatistics != 0)
      return;
    Statics.CategoryStatistics = service.Register(Statics.CategoryStatisticsGUID);
  }

  private static void LoadIcon4AttributeType(string resPath, string resName, FieldTypes type)
  {
    using (Stream resourceStream = Services.GetResourceStream(resPath, resName))
    {
      if (resourceStream == null)
        return;
      using (Icon icon = new Icon(resourceStream))
        Statics.IconSrv.AddIcon(icon, 3, -1, (object) type);
    }
  }

  /// <summary>
  /// Отображает глобальные идентификаторы предопределенных категорий в
  /// предопределенные целочисленные константы, чтобы предотвратить их
  /// использование при регистрации других глобальных идентификаторов
  /// </summary>
  private static void PreserveStandartCategories()
  {
    Holder.GuidMapper.Register(Intermech.Consts.CategoryObjectVersionGUID, 1);
    Holder.GuidMapper.Register(Intermech.Consts.CategoryObjectGUID, 2);
    Holder.GuidMapper.Register(Intermech.Consts.CategoryAttributeGUID, 3);
    Holder.GuidMapper.Register(Intermech.Consts.CategoryObjectTypeGUID, 4);
    Holder.GuidMapper.Register(Intermech.Consts.CategoryRelationGUID, 5);
    Holder.GuidMapper.Register(Intermech.Consts.CategoryRelationTypeGUID, 6);
    Holder.GuidMapper.Register(Intermech.Consts.CategoryLCStepGUID, 7);
    Holder.GuidMapper.Register(Intermech.Consts.CategoryLCLevelGUID, 8);
    Holder.GuidMapper.Register(Intermech.Consts.CategoryLanguageGUID, 9);
    Holder.GuidMapper.Register(Intermech.Consts.CategoryEventLogGUID, 10);
    Holder.GuidMapper.Register(Intermech.Consts.CategorySubjectAreaGUID, 11);
    Holder.GuidMapper.Register(Intermech.Consts.CategoryAttributeGroupGUID, 12);
    Holder.GuidMapper.Register(Intermech.Consts.CategorySystemGUID, 14);
    Holder.GuidMapper.Register(Intermech.Consts.CategoryFilesGUID, 15);
    Holder.GuidMapper.Register(Intermech.Consts.CategoryLCSchemaGUID, 16 /*0x10*/);
    Holder.GuidMapper.Register(Intermech.Consts.CategoryApplicabilityGUID, 19);
    Holder.GuidMapper.Register(Intermech.Consts.CategoryObjectSnapshotsGUID, 23);
    Holder.GuidMapper.Register(Intermech.Consts.CategoryHistoryFilesGUID, 21);
    Holder.GuidMapper.Register(Intermech.Consts.CategorySavedObjectGUID, 24);
    Holder.GuidMapper.Register(Intermech.Consts.CategoryImbaseRecordGUID, 25);
    Holder.GuidMapper.Register(Intermech.Consts.CategoryImbaseAttGUID, 26);
    Holder.GuidMapper.Register(Intermech.Consts.CategoryImbaseIndexGUID, 30);
  }

  /// <summary>Регистрирует основные схемы виртуальных колонок.</summary>
  private static void RegisterColumnSchemes()
  {
    Holder.ColumnSchemes.Register(Consts.NavigatorColumnSchemeGuid, (INodeColumnScheme) new NavigatorColumnScheme());
    Holder.ColumnSchemes.Register(Consts.NameColumnSchemeGuid, (INodeColumnScheme) new NameColumnScheme());
  }

  /// <summary>
  /// Получить значок для указанных категории, типа и данных
  /// </summary>
  /// <param name="category">Категория</param>
  /// <param name="type">Тип</param>
  /// <param name="data">Данные</param>
  /// <returns>Значок или null</returns>
  private static Icon FindDbEntityIcon(int category, int type, object data)
  {
    if (type != -1)
    {
      if (data is INavigatorIconInformation && (data as INavigatorIconInformation).data is IDBSelectionID data1)
      {
        if (data1.HandSelection)
        {
          if (type == Services._objectTypeIDCommonSelection)
            return Services._selectionCommonManualIcon;
          if (type == Services._objectTypeIDPersonalSelection)
            return Services._selectionPersonalManualIcon;
        }
        else
        {
          if (type == Services._objectTypeIDCommonSelection)
            return Services._selectionCommonIcon;
          if (type == Services._objectTypeIDPersonalSelection)
            return Services._selectionPersonalIcon;
        }
      }
      byte[] iconRawData = Services.GetIconRawData(category, type, data);
      if (iconRawData != null)
      {
        if (iconRawData.Length != 0)
        {
          try
          {
            ++Services.IconsCount;
            using (MemoryStream memoryStream = new MemoryStream(iconRawData))
              return new Icon((Stream) memoryStream);
          }
          catch
          {
            return (Icon) null;
          }
        }
      }
    }
    return (Icon) null;
  }

  /// <summary>
  /// Возвращает иконку для виртуальных элементов навигации связаных с объектами
  /// </summary>
  /// <param name="category">Идентификатор категории элемента навигации</param>
  /// <param name="type">Идентификатор типа элемента навигации</param>
  /// <param name="data">Дополнительные данные</param>
  /// <returns>Иконка</returns>
  private static Icon GetDescriptorNodeIcon(int category, int type, object data)
  {
    return category == Consts.CategorySelectObjectsNode || category == Consts.CategorySelectObjectListsNode ? Holder.IconService.GetIcon(4, type, (object) null) : (Icon) null;
  }

  private static byte[] GetIconRawData(int category, int type, object data)
  {
    if (category == Consts.CategoryAdvRootObjectsListNode)
    {
      using (MemoryStream outputStream = new MemoryStream())
      {
        Statics.IconSrv.GetIcon(Statics.CategoryObjectTypes, 0).Save((Stream) outputStream);
        return outputStream.ToArray();
      }
    }
    IClientMetadataCache service = ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache;
    switch (category)
    {
      case 1:
      case 4:
      case 24:
        IDBObjectTypeInfo objectType = service.GetObjectType(type, false);
        if (objectType != null)
          return objectType.Icon;
        break;
      case 6:
        IDBRelationTypeInfo relationType = service.GetRelationType(type, false);
        if (relationType != null)
          return relationType.Icon;
        break;
      case 8:
        IDBLifecycleLevelInfo lifecycleLevel = service.GetLifecycleLevel(type, false);
        if (lifecycleLevel != null)
          return lifecycleLevel.LevelIcon;
        break;
    }
    return (byte[]) null;
  }
}
