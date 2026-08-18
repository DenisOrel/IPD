
// Type: Intermech.Navigator.Engine
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Calendars;
using Intermech.Calendars.Editor;
using Intermech.Client.Core;
using Intermech.Client.Core.Commands.CommandCache;
using Intermech.Client.Core.CompositionView;
using Intermech.Client.Core.FormDesigner;
using Intermech.Client.Core.FormDesigner.Controls;
using Intermech.Client.Core.Navigator.Classes.ObjectNode;
using Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost;
using Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.AxViewers.AxHostWrappers;
using Intermech.Client.Core.Organizer;
using Intermech.Client.Core.Thumbnail;
using Intermech.Client.Core.Visualizers;
using Intermech.Commands;
using Intermech.DocumentView;
using Intermech.Expressions;
using Intermech.Interfaces;
using Intermech.Interfaces.Calendars;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Configuration;
using Intermech.Interfaces.Plugins;
using Intermech.Navigator.Classifiers;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Controls;
using Intermech.Navigator.CustomNode;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.InformationCreator;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.LifeCycle;
using Intermech.Navigator.Selections;
using Intermech.Navigator.Selections.Implementation;
using Intermech.Navigator.Views;
using Intermech.Office.Interfaces;
using Intermech.PropertyEditors;
using Intermech.Search;
using Intermech.Search.AttributeChangeHistory;
using Intermech.Search.AutoConcretization;
using Intermech.Search.ButtonBars;
using Intermech.Search.CompositionByObjectTypesFilters;
using Intermech.Search.Concretization;
using Intermech.Search.ContextMenus;
using Intermech.Search.EditingContexts;
using Intermech.Search.EventLogFilters;
using Intermech.Search.GlobalNodes;
using Intermech.Search.GroupAttributesChanging;
using Intermech.Search.Navigator.FindInTree;
using Intermech.Search.ObjectGroups;
using Intermech.Search.ObjectListFilters;
using Intermech.Search.RecentObjects;
using Intermech.Security;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace Intermech.Navigator;

public sealed class Engine
{
  /// <summary>Признак окончания загрузки IPS (плагинов)</summary>
  public static bool LoadCompleted;

  /// <summary>
  /// 
  /// </summary>
  public static void Start()
  {
    BasicCommandsProvider.Init();
    ObjectsInfoCache serviceInstance = new ObjectsInfoCache();
    ServicesManager.AddService(typeof (IObjectsInfoCache), (object) serviceInstance);
    CacheManager.Register("ObjectsInfoCache", (ICache) serviceInstance);
    CacheManager.Register("ObjectTypeInheritanceCache", (ICache) new ObjectTypesInheritanceCache(new TimeSpan(0, 5, 0)));
    CacheManager.Register("ComputerNamesCache", (ICache) new ComputerNamesCache());
    CacheManager.Register("UserNamesCache", (ICache) new UserNamesCache());
    CacheManager.Register("ProjectNamesCache", (ICache) new ProjectNamesCache());
    CacheManager.Register("ObjectTypeNamesCache", (ICache) new ObjectTypeNamesCache());
    CacheManager.Register("ObjectLCStepsCache", (ICache) new ObjectLCStepsCache());
    CacheManager.Register("ObjectLevelIDsCache", (ICache) new ObjectLevelIDsCache());
    Intermech.CacheServices.Services.Start();
    Services.Start();
    Intermech.Navigator.ContextMenu.Services.Start();
    Intermech.Navigator.Parts.Services.Start();
    Intermech.Navigator.GlobalNode.Services.Start();
    ServicesManager.AddService(typeof (IGlobalNodeRegistry), (object) new StandardGlobalNodeRegistry());
    Intermech.Navigator.CustomNode.Services.Start();
    Intermech.Navigator.EventLog.Services.Start();
    Intermech.Navigator.Controls.Services.Start();
    Intermech.Navigator.DBObjects.Services.Start();
    Intermech.Navigator.DBObjectTypes.Services.Start();
    Intermech.Navigator.Selections.Services.Start();
    Intermech.Navigator.Classifiers.Services.Start();
    Intermech.Navigator.Snapshots.Services.Start();
    ApplicationServices.Container.AddService(typeof (ICompareFilesService), (object) new CompareFilesService());
    CommandCacheService.RegisterService();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      Engine.StartAddons(sessionKeeper.Session);
    ((INotificationService) ServicesManager.GetService(typeof (INotificationService))).Subscribe("ProjectChanged", (NotificationEventHandler) ((s, e) =>
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        MetaDataHelper.SyncMetadata((sessionKeeper.Session as IUserSessionCacheDataSet).CacheDataSet, true);
    }));
    if (ServicesManager.GetService(typeof (IConfigurationManager)) is IConfigurationManager service)
    {
      IConfiguration configuration = service.Open("UISettings");
      if (configuration != null)
      {
        Guid result1;
        if (Guid.TryParse(configuration.GetProperty("SelectedChildrenViewObjectFilter"), out result1))
          UISettings.SelectedChildrenViewObjectFilter = new Guid?(result1);
        bool result2;
        if (bool.TryParse(configuration.GetProperty("DisableChildrenViewGrouping"), out result2))
          UISettings.DisableChildrenViewGrouping = result2;
        bool result3;
        if (bool.TryParse(configuration.GetProperty("SearchInIndexSubstring"), out result3))
          UISettings.SearchInIndexSubstring = result3;
      }
    }
    ServicesManager.AddService(typeof (INavigatorTreeViewClientService), (object) new NavigatorTreeViewClientService());
    new ObjectGroupClientModule().Load();
    new CompositionByObjectTypesFiltersClientModule(ServiceLocator.Get<IFactory>()).Load();
    new ObjectListFiltersClientModule().Load();
    new ContextMenuClientModule().Load();
    (ServicesManager.GetService(typeof (INotificationService)) as INotificationService).Subscribe("ApplicationClosing", (NotificationEventHandler) ((sender, e) => SelectionWindow.SaveMementos()));
    new GroupAttributesChangingClientModule(ServiceLocator.Get<IFactory>()).Load();
    new RecentObjectsClientModule(ServiceLocator.Get<ICategoryTypeIconService>(), ServiceLocator.Get<IFactory>(), ServiceLocator.Get<IGuidMapper>(), ServiceLocator.Get<INamedImageList>(), ServiceLocator.Get<INotificationService>()).Load();
    new EventLogFiltersClientModule().Load();
    Intermech.Interfaces.EventLog.Helper.Init();
    new AttributeChangeHistoryClientModule().Load();
    new ConcretizationClientModule().Load();
    new AutoConcretizationClientModule(ServiceLocator.Get<IFactory>(), ServiceLocator.Get<IConcretizationClientService>()).Load();
    new FindInTreeClientModule(ServiceLocator.Get<IFactory>()).Load();
    Consts.NotificationsAndContextsCategoryID = Holder.GuidMapper.Register(Consts.NotificationsAndContextsCategoryGuid);
    Holder.Factory.AddNodeType(Consts.NotificationsAndContextsCategoryID, typeof (ObjectsListNode));
    Holder.Factory.AddViewsProvider(Consts.NotificationsAndContextsCategoryID, (IViewsProvider) new AdvObjectsPropertiesProvider());
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  private static void StartAddons(IUserSession session)
  {
    (ServicesManager.GetService(typeof (IPluginManager)) as IPluginManager).LoadComplete += new EventHandler(Engine.pluginManager_LoadComplete);
    ClientConsts.UsersGroupsCategoryID = Holder.GuidMapper.Register(ClientConsts.CategoryUsersGroupsGuid);
    Holder.Factory.AddNodeType(ClientConsts.UsersGroupsCategoryID, typeof (TopObjectsNode));
    Holder.Factory.AddViewsProvider(ClientConsts.UsersGroupsCategoryID, (IViewsProvider) new UsersGroupsViewsProvider());
    Consts.CategoryOrganizationalUnitsNode = Holder.GuidMapper.Register(Consts.CategoryOrganizationalUnitsNodeGuid);
    Holder.Factory.AddNodeType(Consts.CategoryOrganizationalUnitsNode, typeof (OrganizationalUnitsNode));
    Holder.Factory.AddViewsProvider(Consts.CategoryOrganizationalUnitsNode, (IViewsProvider) new OrganizationalUnitsViewsProvider());
    Icon objTypeIcon = UIHelper.GetObjTypeIcon(MetaDataHelper.GetObjectTypeID("cadd9235-306c-11d8-b4e9-00304f19f545"));
    if (objTypeIcon != null)
    {
      Statics.IconSrv.AddIcon(objTypeIcon, Consts.CategoryOrganizationalUnitsNode);
      objTypeIcon.Dispose();
    }
    ClientConsts.UsersRolesCategoryID = Holder.GuidMapper.Register(ClientConsts.CategoryUsersRolesGuid);
    Holder.Factory.AddNodeType(ClientConsts.UsersRolesCategoryID, typeof (TopObjectsNode));
    Holder.Factory.AddViewsProvider(ClientConsts.UsersRolesCategoryID, (IViewsProvider) new UsersRolesViewsProvider());
    ClientConsts.MeasuresCategoryID = Holder.GuidMapper.Register(ClientConsts.MeasuresGuid);
    Holder.Factory.AddNodeType(ClientConsts.MeasuresCategoryID, typeof (TopObjectsNode));
    Holder.Factory.AddViewsProvider(ClientConsts.MeasuresCategoryID, (IViewsProvider) new MeasuresViewsProvider());
    Consts.CategoryVersionsObjectNode = Holder.GuidMapper.Register(Consts.CategoryVersionsObjectNodeGuid);
    Holder.Factory.AddNodeType(Consts.CategoryVersionsObjectNode, typeof (VersionsNode));
    Holder.Factory.AddViewsProvider(Consts.CategoryVersionsObjectNode, (IViewsProvider) new VersionsViewsProvider());
    Consts.CategoryAdvRelationsNode = Holder.GuidMapper.Register(Consts.CategoryAdvRelationsNodeGuid);
    Holder.Factory.AddNodeType(Consts.CategoryAdvRelationsNode, typeof (AdvRelationsNode));
    Consts.CategoryAdvRootObjectsListNode = Holder.GuidMapper.Register(Consts.CategoryAdvRootObjectsListNodeGuid);
    Holder.Factory.AddNodeType(Consts.CategoryAdvRootObjectsListNode, typeof (AdvRootObjectsListNode));
    Holder.Factory.AddViewsProvider(Consts.CategoryAdvRootObjectsListNode, (IViewsProvider) new AdvObjectsPropertiesProvider());
    Consts.CategoryDesktopNode = Holder.GuidMapper.Register(Consts.CategoryDesktopNodeGuid);
    Holder.Factory.AddNodeType(1, session.IdentHelper.GetObjectTypeID("cad0004a-306c-11d8-b4e9-00304f19f545"), typeof (DesktopObjectNode));
    Consts.CategoryFavoritesNode = Holder.GuidMapper.Register(Consts.CategoryFavoritesNavigatorNodeGuid);
    Holder.Factory.AddNodeType(Consts.CategoryFavoritesNode, typeof (FavoritesRootNode));
    Holder.Factory.AddViewsProvider(Consts.CategoryFavoritesNode, (IViewsProvider) new FavoritesNodeViewProvider());
    Holder.Factory.AddCommandsProvider(Consts.CategoryFavoritesNode, (ICommandsProvider) new FavoritesCommandProvider());
    Holder.Factory.AddCommandsProvider(1, (ICommandsProvider) new FavoritesCommandProvider());
    Holder.Factory.AddCommandsProvider(4, (ICommandsProvider) new FavoritesCommandProvider());
    using (Stream resourceStream = Services.GetResourceStream("Favorites.ico"))
    {
      using (Icon icon = new Icon(resourceStream))
      {
        Holder.NamedImageList.Add(icon, "imgFavorites");
        Statics.IconSrv.AddIcon(icon, Consts.CategoryFavoritesNode);
      }
    }
    if (UISettings.ShowFavoritesFolder)
      Holder.Factory.AddGlobalNode(new Guid("{D9AB85B5-BF1F-4346-91B3-80DDE7229C0A}"), (IDescriptor) new FavoritesRootNodeDescriptor(), 20);
    Consts.CategoryCurrentProjectNode = Holder.GuidMapper.Register(Consts.CategoryCurrentProjectNodeGuid);
    int objectTypeId1 = session.IdentHelper.GetObjectTypeID("cad00812-306c-11d8-b4e9-00304f19f545");
    Holder.Factory.AddNodeType(1, objectTypeId1, typeof (ProjectObjectNode));
    Holder.Factory.AddViewsProvider(Consts.CategoryCurrentProjectNode, (IViewsProvider) new ProjectsViewProvider());
    Icon icon1 = Statics.IconSrv.GetIcon(4, objectTypeId1);
    if (icon1 != null)
    {
      Statics.IconSrv.AddIcon(icon1, Consts.CategoryCurrentProjectNode);
      Holder.NamedImageList.Add(icon1, "imgProjects");
    }
    Consts.CategoryContextSelectionsNodeID = Holder.GuidMapper.Register(Consts.CategoryContexSelectionsNodeGuid);
    int objectTypeId2 = session.IdentHelper.GetObjectTypeID("cad00156-306c-11d8-b4e9-00304f19f545");
    Holder.Factory.AddNodeType(1, objectTypeId2, typeof (SelectionNode));
    Holder.Factory.AddViewsProvider(Consts.CategoryContextSelectionsNodeID, (IViewsProvider) new ContextSelectionsViewProvider());
    Icon icon2 = Statics.IconSrv.GetIcon(4, objectTypeId2);
    if (icon2 != null)
    {
      Statics.IconSrv.AddIcon(icon2, Consts.CategoryContextSelectionsNodeID);
      Holder.NamedImageList.Add(icon2, "imgContextSelection");
    }
    Consts.CategoryCurrentContextNode = Holder.GuidMapper.Register(Consts.CategoryCurrentContextNodeGuid);
    Holder.Factory.AddNodeType(1, typeof (Intermech.Navigator.DBObjects.ObjectNode));
    Consts.CategorySelectObjectsNode = Holder.GuidMapper.Register(Consts.CategorySelectObjectsNodeGuid);
    Holder.Factory.AddNodeType(Consts.CategorySelectObjectsNode, typeof (SelectObjectsNode));
    Holder.Factory.AddViewsProvider(Consts.CategorySelectObjectsNode, (IViewsProvider) new SelectObjectsProvider());
    Consts.CategorySelectObjectListsNode = Holder.GuidMapper.Register(Consts.CategorySelectObjectListsNodeGuid);
    Holder.Factory.AddNodeType(Consts.CategorySelectObjectListsNode, typeof (Intermech.Navigator.CustomNode.Node));
    Holder.Factory.AddViewsProvider(Consts.CategorySelectObjectListsNode, (IViewsProvider) new Intermech.Navigator.CustomNode.ViewsProvider());
    using (Stream resourceStream = Services.GetResourceStream("ObjectTypes.ico"))
    {
      using (Icon icon3 = new Icon(resourceStream))
      {
        Holder.NamedImageList.Add(icon3, "imgSelectObjects");
        Statics.IconSrv.AddIcon(icon3, Consts.CategorySelectObjectsNode, 0);
      }
    }
    Consts.CategoryVirtualObjectNode = Holder.GuidMapper.Register(Consts.CategoryVirtualObjectNodeGuid);
    Holder.Factory.AddNodeType(Consts.CategoryVirtualObjectNode, typeof (VirtualObjectNode));
    using (Stream resourceStream = Services.GetResourceStream("ObjectTypesInvalid.ico"))
    {
      using (Icon icon4 = new Icon(resourceStream))
      {
        Holder.NamedImageList.Add(icon4, "imgInvalidObject");
        Statics.IconSrv.AddIcon(icon4, Consts.CategoryVirtualObjectNode, 0);
      }
    }
    ServicesManager.AddService(typeof (IGroupingObjectsCache), (object) new GroupingObjectsCache());
    Consts.CategoryGroupingObjectsNode = Holder.GuidMapper.Register(Consts.CategoryGroupingObjectsNodeGuid);
    Holder.Factory.AddNodeType(Consts.CategoryGroupingObjectsNode, typeof (VirtualGrouingObjectsNode));
    Holder.Factory.AddViewsProvider(Consts.CategoryGroupingObjectsNode, (IViewsProvider) new VirtualGrouingObjectsProvider());
    using (Stream resourceStream = Services.GetResourceStream("GroupingObjects.ico"))
    {
      using (Icon icon5 = new Icon(resourceStream))
      {
        Holder.NamedImageList.Add(icon5, "imgGroupingObjects");
        Statics.IconSrv.AddIcon(icon5, Consts.CategoryGroupingObjectsNode, 0);
      }
    }
    Consts.CategoryAllProjectObjectsNode = Holder.GuidMapper.Register(Consts.CategoryAllProjectObjectsNodeGuid);
    Holder.Factory.AddNodeType(Consts.CategoryAllProjectObjectsNode, typeof (AllProjectObjectsNode));
    Holder.Factory.AddViewsProvider(Consts.CategoryAllProjectObjectsNode, (IViewsProvider) new AllProjectObjectsViewsProvider());
    using (Stream resourceStream = Services.GetResourceStream("AllProjectObjects.ico"))
    {
      using (Icon icon6 = new Icon(resourceStream))
      {
        Holder.NamedImageList.Add(icon6, "imgAllProjectObjects");
        Statics.IconSrv.AddIcon(icon6, Consts.CategoryAllProjectObjectsNode, 0);
      }
    }
    Consts.CategoryMultipleObjectsNode = Holder.GuidMapper.Register(Consts.CategoryMultipleObjectsGuid);
    Holder.Factory.AddNodeType(Consts.CategoryMultipleObjectsNode, typeof (MultipleObjectsNode));
    Holder.Factory.AddViewsProvider(Consts.CategoryMultipleObjectsNode, (IViewsProvider) new MultipleObjectsViewsProvider());
    Consts.NotificationSelectionsCategoryID = Holder.GuidMapper.Register(Consts.NotificationSelectionsCategoryGuid);
    Holder.Factory.AddViewsProvider(Consts.NotificationSelectionsCategoryID, 2, (IViewsProvider) new MultipleObjectsViewsProvider());
    Holder.Factory.AddViewsProvider(Consts.NotificationSelectionsCategoryID, 1, (IViewsProvider) new MultipleObjectsViewsProvider());
    using (Stream resourceStream = Services.GetResourceStream("ObjectTypes.ico"))
    {
      using (Icon icon7 = new Icon(resourceStream))
      {
        Holder.NamedImageList.Add(icon7, "imgMultipleObjects");
        Statics.IconSrv.AddIcon(icon7, Consts.CategoryMultipleObjectsNode, 0);
      }
    }
    using (Stream resourceStream = Services.GetResourceStream("BaseVersion.ico"))
    {
      using (Icon icon8 = new Icon(resourceStream))
        Holder.NamedImageList.Add(icon8, "imgBaseVersion");
    }
    using (Stream resourceStream = Services.GetResourceStream("BaseVersionEmpty.ico"))
    {
      using (Icon icon9 = new Icon(resourceStream))
        Holder.NamedImageList.Add(icon9, "imgBaseVersionEmpty");
    }
    using (Stream resourceStream = Services.GetResourceStream("NonBaseVersion.ico"))
    {
      using (Icon icon10 = new Icon(resourceStream))
        Holder.NamedImageList.Add(icon10, "imgNonBaseVersion");
    }
    Consts.CategoryLCSchemesObjTypesNode = Holder.GuidMapper.Register(Consts.CategoryLCSchemesObjTypesNodeGuid);
    Holder.Factory.AddNodeType(Consts.CategoryLCSchemesObjTypesNode, typeof (LCSchemesObjTypesNode));
    Holder.Factory.AddViewsProvider(Consts.CategoryLCSchemesObjTypesNode, (IViewsProvider) new LCSchemesObjTypesNodeProvider());
    Consts.CategoryLifeCycleSchemesNode = Holder.GuidMapper.Register(Consts.CategoryLifeCycleSchemesNodeGuid);
    Holder.Factory.AddNodeType(Consts.CategoryLifeCycleSchemesNode, typeof (LifeCycleSchemesNode));
    Holder.Factory.AddViewsProvider(Consts.CategoryLifeCycleSchemesNode, (IViewsProvider) new LifeCycleSchemesProvider());
    Consts.CategoryLifeCycleSchemeNode = Holder.GuidMapper.Register(Consts.CategoryLifeCycleSchemeNodeGuid);
    Holder.Factory.AddNodeType(Consts.CategoryLifeCycleSchemeNode, typeof (LifeCycleSchemeNode));
    Holder.Factory.AddViewsProvider(Consts.CategoryLifeCycleSchemeNode, (IViewsProvider) new LifeCycleSchemeStepsProvider());
    Consts.CategoryLifeCycleLevelNode = Holder.GuidMapper.Register(Consts.CategoryLifeCycleLevelNodeGuid);
    Consts.CategoryLifeCycleStepNode = Holder.GuidMapper.Register(Consts.CategoryLifeCycleStepNodeGuid);
    Holder.Factory.AddNodeType(Consts.CategoryLifeCycleStepNode, typeof (LifeCycleStepNode));
    ObjectsListConsts.ObjectsNodeID = Holder.GuidMapper.Register(ObjectsListConsts.ObjectsNodeGuid);
    Holder.Factory.AddNodeType(ObjectsListConsts.ObjectsNodeID, typeof (ObjectsListObjectsNode));
    ObjectsListConsts.CompositionNodeID = Holder.GuidMapper.Register(ObjectsListConsts.CompositionNodeGuid);
    Holder.Factory.AddNodeType(ObjectsListConsts.CompositionNodeID, typeof (ObjectsListCompositionApplicabilityNode));
    ObjectsListConsts.ApplicabilityNodeID = Holder.GuidMapper.Register(ObjectsListConsts.ApplicabilityNodeGuid);
    Holder.Factory.AddNodeType(ObjectsListConsts.ApplicabilityNodeID, typeof (ObjectsListCompositionApplicabilityNode));
    Icon icon11 = Statics.IconSrv.GetIcon(16 /*0x10*/, 0);
    INamedImageList service1 = ServicesManager.GetService(typeof (INamedImageList)) as INamedImageList;
    if (icon11 != null)
    {
      Statics.IconSrv.AddIcon(icon11, Consts.CategoryLifeCycleSchemesNode, 0);
      Statics.IconSrv.AddIcon(icon11, Consts.CategoryLifeCycleSchemeNode, 0);
      service1?.Add(icon11, "imgLifeSteps");
      List<IMSLifeCycleScheme> lcSchemesList = MetaDataHelper.GetLCSchemesList();
      for (int index = 0; index < lcSchemesList.Count; ++index)
        Statics.IconSrv.AddIcon(icon11, Consts.CategoryLifeCycleSchemeNode, lcSchemesList[index].SchemaID);
    }
    if (service1 != null)
    {
      Image image1 = service1.ImageList.Images[service1.ImageIndex("imgVersionsTree")];
      Image image2 = service1.ImageList.Images[service1.ImageIndex("imgVersionsList")];
      Image image3 = service1.ImageList.Images[service1.ImageIndex("imgEntersTo")];
      Image image4 = service1.ImageList.Images[service1.ImageIndex("imgTreeView")];
      using (Icon iconFromImage = ImagesResizeHelper.GetIconFromImage(image1))
        Statics.IconSrv.AddIcon(iconFromImage, Consts.CategoryVersionsObjectNode, 0);
      using (Icon iconFromImage = ImagesResizeHelper.GetIconFromImage(image2))
        Statics.IconSrv.AddIcon(iconFromImage, Consts.CategoryVersionsObjectNode, 1);
      using (Icon iconFromImage = ImagesResizeHelper.GetIconFromImage(image3))
        Statics.IconSrv.AddIcon(iconFromImage, Consts.CategoryCustomNode, 1);
      using (Icon iconFromImage = ImagesResizeHelper.GetIconFromImage(image4))
      {
        Statics.IconSrv.AddIcon(iconFromImage, Consts.CategoryLCSchemesObjTypesNode, 0);
        Statics.IconSrv.AddIcon(iconFromImage, Consts.CategoryAllObjectTypes, 0);
      }
    }
    FormsProviders.RegisterFormProviders(Holder.Factory);
    Engine.StartFiltrationAddon(session);
    UserToRolesProvider provider = new UserToRolesProvider();
    Holder.Factory.AddViewsProvider(1, session.IdentHelper.UsersTypeID, (IViewsProvider) provider);
    Holder.Factory.AddViewsProvider(1, session.IdentHelper.GroupsTypeID, (IViewsProvider) provider);
    Holder.Factory.AddViewsProvider(1, MetaDataHelper.GetObjectTypeID(new Guid("cad00812-306c-11d8-b4e9-00304f19f545")), (IViewsProvider) new ProjectTeamsViewProvider());
    new EditingContextsClientModule().Load();
    Holder.Factory.AddViewsProvider(1, (IViewsProvider) new ButtonBarViewsProvider());
    Holder.Factory.AddViewsProvider(1, (IViewsProvider) new GropingObjectsSearchViewProvider());
    Holder.Factory.AddViewsProvider(1, (IViewsProvider) new ObjectsVisibilityViewProvider());
    ServicesManager.AddService(typeof (ICalendarsService), (object) new CalendarsService(session));
    Intermech.Project.Library.Init((System.IServiceProvider) ServicesManager.ServiceContainer, session);
    OfficeConsts.Init(session);
    Holder.Factory.AddCommandsProvider(1, (ICommandsProvider) new AdditionalCommandProvider());
    Holder.Factory.AddViewsProvider(10, (IViewsProvider) new EventLogPropertiesProvider());
    ServicesManager.AddService(typeof (IAttributePropertyDescriberService), (object) new AttributePropertyDescriberService());
    ServicesManager.AddService(typeof (IVisualizerService), (object) new VisualizerService());
    ServicesManager.AddService(typeof (IPreviewExtender), (object) new PreviewExtender());
    AxHostProviders.Register();
    ViewerFactoryProvider.Register();
    Intermech.Client.Core.Thumbnail.Consts.Initialize();
    OrganizerStartup.Initialize();
    EditorHelper.Initialize();
    DwgVisualizer.Initialize((System.IServiceProvider) ServicesManager.ServiceContainer);
    IObjectCreatorService service2 = (IObjectCreatorService) ServicesManager.GetService(typeof (IObjectCreatorService));
    if (service2 != null)
    {
      RegVersionRulesCreatorForm.Attach(service2);
      ClassiffCreator.Attach(service2);
      SelectionCreator.Attach(service2);
      ObjectTemplateCreator.Attach(service2);
      ProjectCreator.Attach(service2);
      UserCreator.Attach(service2);
      SiteCreator.Attach(service2);
      BitmapCreator.Attach(service2);
    }
    Engine.StartObjectPropertyGridAddons(session);
    ServicesManager.AddService(typeof (ISelectionDialogTabsService), (object) new SelectionDialogTabsService());
    ServicesManager.AddService(typeof (ISelectObjectDialogService), (object) new SelectObjectDialogService());
    CalendarsEditor.Init((System.IServiceProvider) ServicesManager.ServiceContainer, session);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private static void pluginManager_LoadComplete(object sender, EventArgs e)
  {
    Engine.LoadCompleted = true;
    CompositionViewHolder.Register((System.IServiceProvider) ServicesManager.ServiceContainer);
    SelectionWindow.RestoreMementos();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  private static void StartFiltrationAddon(IUserSession session)
  {
    try
    {
      int objectType = session.GetObjectType(new Guid("cad001b3-306c-11d8-b4e9-00304f19f545")).ObjectType;
      Holder.Factory.AddViewsProvider(1, objectType, (IViewsProvider) new VersionRulesEditorProvider());
      int rolesTypeId = session.IdentHelper.RolesTypeID;
      Holder.Factory.AddViewsProvider(1, rolesTypeId, (IViewsProvider) new RolesSettingsProvider());
    }
    catch (Exception ex)
    {
      Trace.WriteLine(ex.Message);
    }
    using (Stream resourceStream = Services.GetResourceStream("VersionRulesView.ico"))
    {
      using (Icon icon = new Icon(resourceStream))
        Holder.NamedImageList.Add(icon, "imgVersionRule");
    }
    using (Stream resourceStream = Services.GetResourceStream("User_Current.ico"))
    {
      using (Icon icon = new Icon(resourceStream))
        Holder.NamedImageList.Add(icon, "imgUserCurrent");
    }
    using (Stream resourceStream = Services.GetResourceStream("User_Other.ico"))
    {
      using (Icon icon = new Icon(resourceStream))
        Holder.NamedImageList.Add(icon, "imgUserOther");
    }
    using (Stream resourceStream = Services.GetResourceStream("User_Other.ico"))
    {
      using (Icon icon = new Icon(resourceStream))
        Holder.NamedImageList.Add(icon, "imgUserOther");
    }
    using (Stream resourceStream = Services.GetResourceStream("AddStandart.ico"))
    {
      using (Icon icon = new Icon(resourceStream))
        Holder.NamedImageList.Add(icon, "imgAdd");
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  private static void StartObjectPropertyGridAddons(IUserSession session)
  {
    if (!(ServicesManager.GetService(typeof (IAttributePropertyDescriberService)) is IAttributePropertyDescriberService service))
      return;
    int attributeId1 = session.GetAttributeType(new Guid("cad00149-306c-11d8-b4e9-00304f19f545")).AttributeID;
    if (service.GetDescriber(attributeId1) == null)
      service.RegisterDescriber(attributeId1, (IAttributePropertyDescriber) new ObjectTypeAttDescriber());
    int attributeId2 = session.GetAttributeType(new Guid("cad001a0-306c-11d8-b4e9-00304f19f545")).AttributeID;
    if (service.GetDescriber(attributeId2) == null)
      service.RegisterDescriber(attributeId2, (IAttributePropertyDescriber) new ObjectTypeAttDescriber());
    int attributeId3 = session.GetAttributeType(new Guid("cadd9c3a-306c-11d8-b4e9-00304f19f545")).AttributeID;
    if (service.GetDescriber(attributeId3) == null)
      service.RegisterDescriber(attributeId3, (IAttributePropertyDescriber) new ObjectTypeAttDescriber());
    int attributeId4 = session.GetAttributeType(new Guid("cad001af-306c-11d8-b4e9-00304f19f545")).AttributeID;
    if (service.GetDescriber(attributeId4) == null)
      service.RegisterDescriber(attributeId4, (IAttributePropertyDescriber) new AreasAttDescriber());
    int attributeId5 = session.GetAttributeType(new Guid("cad001d0-306c-11d8-b4e9-00304f19f545")).AttributeID;
    if (service.GetDescriber(attributeId5) == null)
      service.RegisterDescriber(attributeId5, (IAttributePropertyDescriber) new AttributeTypeAttrDescriber());
    int attributeId6 = session.GetAttributeType(new Guid("cad0014a-306c-11d8-b4e9-00304f19f545")).AttributeID;
    if (service.GetDescriber(attributeId6) == null)
      service.RegisterDescriber(attributeId6, (IAttributePropertyDescriber) new RelationTypeAttDescriber());
    int attributeId7 = session.GetAttributeType(new Guid("cad001a9-306c-11d8-b4e9-00304f19f545")).AttributeID;
    if (service.GetDescriber(attributeId7) == null)
      service.RegisterDescriber(attributeId7, (IAttributePropertyDescriber) new RelationTypeAttDescriber());
    int attributeId8 = session.GetAttributeType(new Guid("cad00620-306c-11d8-b4e9-00304f19f545")).AttributeID;
    if (service.GetDescriber(attributeId8) == null)
      service.RegisterDescriber(attributeId8, (IAttributePropertyDescriber) new ColumnSchemeAttrDescriber());
    int attributeId9 = session.GetAttributeType(new Guid("cad0038c-306c-11d8-b4e9-00304f19f545")).AttributeID;
    if (service.GetDescriber(attributeId9) == null)
      service.RegisterDescriber(attributeId9, (IAttributePropertyDescriber) new MaterialPropertyDescriber());
    int attributeId10 = session.GetAttributeType(new Guid("cadd94c2-306c-11d8-b4e9-00304f19f545")).AttributeID;
    if (service.GetDescriber(attributeId10) == null)
      service.RegisterDescriber(attributeId10, (IAttributePropertyDescriber) new MaterialPropertyDescriber());
    int attributeId11 = session.GetAttributeType(new Guid("cadd94c3-306c-11d8-b4e9-00304f19f545")).AttributeID;
    if (service.GetDescriber(attributeId11) == null)
      service.RegisterDescriber(attributeId11, (IAttributePropertyDescriber) new MaterialPropertyDescriber());
    int attributeId12 = session.GetAttributeType(new Guid("cad014ab-306c-11d8-b4e9-00304f19f545")).AttributeID;
    if (service.GetDescriber(attributeId12) == null)
      service.RegisterDescriber(attributeId12, (IAttributePropertyDescriber) new AttributeTypeAttrDescriber());
    int attributeId13 = session.GetAttributeType(new Guid("cad014c6-306c-11d8-b4e9-00304f19f545")).AttributeID;
    if (service.GetDescriber(attributeId13) == null)
      service.RegisterDescriber(attributeId13, (IAttributePropertyDescriber) new ObjectTypeAttDescriber());
    int attributeId14 = session.GetAttributeType(new Guid("cad00127-306c-11d8-b4e9-00304f19f545")).AttributeID;
    if (service.GetDescriber(attributeId14) == null)
      service.RegisterDescriber(attributeId14, (IAttributePropertyDescriber) new PluginFileDescriber());
    int attributeId15 = session.GetAttributeType(new Guid("cad01579-306c-11d8-b4e9-00304f19f545")).AttributeID;
    if (service.GetDescriber(attributeId15) == null)
      service.RegisterDescriber(attributeId15, (IAttributePropertyDescriber) new DVSPasswordDescriber());
    int attributeId16 = session.GetAttributeType(new Guid("cadd9c3f-306c-11d8-b4e9-00304f19f545")).AttributeID;
    if (service.GetDescriber(attributeId16) == null)
      service.RegisterDescriber(attributeId16, (IAttributePropertyDescriber) new ObjectTypeAttDescriber());
    int attributeId17 = session.GetAttributeType(new Guid("cad015d1-306c-11d8-b4e9-00304f19f545")).AttributeID;
    if (service.GetDescriber(attributeId17) == null)
      service.RegisterDescriber(attributeId17, (IAttributePropertyDescriber) new OrganizerChildNodeCategoryDescriber());
    int attributeId18 = session.GetAttributeType(new Guid("cad014af-306c-11d8-b4e9-00304f19f545")).AttributeID;
    if (service.GetDescriber(attributeId18) != null)
      return;
    service.RegisterDescriber(attributeId18, (IAttributePropertyDescriber) new CheckSumPropertyDescriber());
  }

  private static void RemoveNotVersionedObjectsFromAllEditingContexts(object sender, EventArgs e)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      (sessionKeeper.Session.GetCustomService(typeof (IEditingContextServerService)) as IEditingContextServerService).RemoveNotVersionedObjectsFromAllEditingContexts(sessionKeeper.Session.SessionGUID);
    int num = (int) MessageBox.Show("Команда выполнена успешно", "Intermech Professional Solution", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
  }
}
