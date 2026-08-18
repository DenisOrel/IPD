
// Type: Intermech.Client.Core.Organizer.OrganizerStartup
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Bars;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.NavBars;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Queries;
using Intermech.Navigator.Views;
using Intermech.Search;
using System;
using System.Drawing;
using System.Reflection;


namespace Intermech.Client.Core.Organizer;

/// <summary>Загрузка органайзера.</summary>
public class OrganizerStartup
{
  /// <summary>Инициализация органайзера.</summary>
  public static void Initialize()
  {
    IServiceProvider serviceContainer = (IServiceProvider) ServicesManager.ServiceContainer;
    if (serviceContainer.GetService(typeof (IStartupService)) is IStartupService service1)
      service1.MainFormShown += new EventHandler(OrganizerStartup.OnstartupSrv_MainFormShown);
    OrganizerService serviceInstance = new OrganizerService(serviceContainer);
    ServicesManager.AddService(typeof (IOrganizerService), (object) serviceInstance);
    OrganizerReminderSettingsPage reminderSettingsPage = new OrganizerReminderSettingsPage(serviceContainer);
    IGuidMapper service2 = serviceContainer.GetService(typeof (IGuidMapper)) as IGuidMapper;
    IFactory service3 = (IFactory) serviceContainer.GetService(typeof (IFactory));
    service3.AddViewsProvider((IViewsProvider) new OrganizerRootViewProvider());
    IViewsProvider provider = (IViewsProvider) new OrganizerViewProvider();
    Intermech.Navigator.Consts.OrganizerRootNodeTypeID = service2.Register(Intermech.Navigator.Consts.OrganizerRootNodeGuid);
    service3.AddNodeType(Intermech.Navigator.Consts.OrganizerRootNodeTypeID, typeof (OrganizerRootNode));
    service3.AddViewsProvider(Intermech.Navigator.Consts.OrganizerRootNodeTypeID, provider);
    service3.AddGlobalNode(new Guid("{29910E40-D9D3-4c8b-A22F-A0A58D4DB56A}"), (IDescriptor) new OrganizerRootNodeDescriptor(), 30);
    int objectTypeId = MetaDataHelper.GetObjectTypeID("cad015bc-306c-11d8-b4e9-00304f19f545");
    service3.AddNodeType(4, objectTypeId, typeof (OrganizerTaskNode));
    service3.AddViewsProvider(4, objectTypeId, provider);
    service3.AddViewsProvider(1, objectTypeId, (IViewsProvider) new OrganizerTaskObjectsViewProvider());
    ConditionStructure conditionStructure1 = new ConditionStructure(-22, RelationalOperators.Equal, (object) (ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole).ID, LogicalOperators.AND, 0, false);
    ConditionStructure conditionStructure2 = new ConditionStructure(new Guid("cad015d5-306c-11d8-b4e9-00304f19f545"), RelationalOperators.Equal, (object) true, LogicalOperators.NONE, 0);
    serviceInstance.RegisterTypeForReminder(objectTypeId, new ConditionStructure[2]
    {
      conditionStructure1,
      conditionStructure2
    });
    if (serviceContainer.GetService(typeof (IObjectCreatorService)) is IObjectCreatorService service4)
      service4.RegisterCreatorCustomService(objectTypeId, typeof (OrganizerTaskCreator));
    INamedImageList service5 = serviceContainer.GetService(typeof (INamedImageList)) as INamedImageList;
    OrganizerStartup.LoadResources(serviceContainer, service5);
    if (ServicesManager.GetService(typeof (IMainMenuService)) is IMainMenuService service6)
    {
      MenuButtonItem menuButtonItem = new MenuButtonItem(OrganizerRootNodeDescriptor.Caption, new EventHandler(OrganizerStartup.OnViewRootNode));
      menuButtonItem.Image = service5.ImageList.Images[service5.ImageIndex("imgOrganizer")];
      MenuButtonItem[] menuButtonItemArray = new MenuButtonItem[1]
      {
        menuButtonItem
      };
      service6.RegisterMenuItems(MainMenuItemSite.Applications, MainMenuItemPosition.Third, menuButtonItemArray);
    }
    if (serviceContainer.GetService(typeof (INavigationBar)) is INavigationBar service7 && service7.FindPane("appPane") is IAppPane pane)
      pane.Add(OrganizerRootNodeDescriptor.Caption, new EventHandler(OrganizerStartup.OnViewRootNode), service5.ImageIndex("imgOrganizer"));
    QueryEvents.BeforeClientRecordsSelectEvent += new BeforeClientRecordsSelectHandler(OrganizerStartup.QueryEvents_BeforeClientRecordsSelectEvent);
    MenuTemplate contextMenuTemplate = service3.ContextMenuTemplate;
    MenuTemplateNode menuTemplateNode = contextMenuTemplate["Create"];
    if (menuTemplateNode == null)
      return;
    contextMenuTemplate.BeginUpdate();
    try
    {
      int imageIndex = service5.ImageIndex("imgOrganizerTask");
      string text = LocalizationHolder.rm.GetString("Organaizer_TaskCaption");
      menuTemplateNode.Nodes.Add(new MenuTemplateNode("CreateOrganizerTask", text, imageIndex, 10, 1));
    }
    finally
    {
      contextMenuTemplate.EndUpdate();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private static void OnstartupSrv_MainFormShown(object sender, EventArgs e)
  {
    int interval = 15;
    if (!(ServicesManager.ServiceContainer.GetService(typeof (IOrganizerService)) is OrganizerService service1))
      return;
    if (ServicesManager.GetService(typeof (IDBConfigurations)) is IDBConfigurations service2)
    {
      service1.TimeBeforeReminder = Convert.ToInt32(service2.ReadInteger("CLIENT", "ORGANIZER_REMINDER", "TIME_BEFORE", 30L, DBConfigMode.UserAndGlobal));
      if (!service2.ReadBool("CLIENT", "ORGANIZER_REMINDER", "ACTIVATE", true, DBConfigMode.UserAndGlobal))
        return;
      interval = Convert.ToInt32(service2.ReadInteger("CLIENT", "ORGANIZER_REMINDER", "TIME_SPACE", 15L, DBConfigMode.UserAndGlobal));
    }
    service1.StartTimers(interval);
  }

  /// <summary>Отображение узла "Органайзер" в новом окне.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private static void OnViewRootNode(object sender, EventArgs e)
  {
    Intermech.Navigator.Utils.OpenNewWindow((IDescriptor) new OrganizerRootNodeDescriptor(), (IServiceProvider) null, new GetSupportedColumnsEventHandler(Intermech.Navigator.Utils.DefaultSupportedColumnsObjects), (NodeIDPath) null);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="args"></param>
  private static void QueryEvents_BeforeClientRecordsSelectEvent(
    object sender,
    BeforeClientRecordsSelectEventArgs args)
  {
    if (!(sender is ObjectsQuery objectsQuery) || !(objectsQuery.Services.GetService(typeof (OrganizerChildNodePart)) is OrganizerChildNodePart service) || service.Tag == null || !service.Tag.Contains((object) "LocalTypesSelector"))
      return;
    args.NewParameters = new DBRecordSetParams?(args.OldParameters);
    args.NewParameters.Value.Tags[(object) "LocalTypesSelector"] = service.Tag[(object) "LocalTypesSelector"];
  }

  /// <summary>Загрузить ресурсы (изображения, т.п.).</summary>
  /// <param name="srvProvider">Коллекция сервисов</param>
  /// <param name="namedImgList">Именованный список картинок</param>
  private static void LoadResources(IServiceProvider srvProvider, INamedImageList namedImgList)
  {
    if (namedImgList == null)
      return;
    Assembly assembly = typeof (OrganizerStartup).Assembly;
    using (Icon resourceData = ResourceHelper.GetResourceData<Icon>(assembly, "Intermech.Client.Core.Resources.Organizer.ico"))
    {
      if (resourceData != null)
      {
        namedImgList.Add(resourceData, "imgOrganizer");
        if (srvProvider.GetService(typeof (ICategoryTypeIconService)) is ICategoryTypeIconService service)
          service.AddIcon(resourceData, Intermech.Navigator.Consts.OrganizerRootNodeTypeID);
      }
    }
    using (Bitmap resourceData = ResourceHelper.GetResourceData<Bitmap>(assembly, "Intermech.Client.Core.Resources.OrganizerCalendar.bmp"))
    {
      if (resourceData != null)
      {
        resourceData.MakeTransparent();
        namedImgList.Add((Image) resourceData, "imgOrganizerCalendar");
      }
    }
    using (Bitmap resourceData = ResourceHelper.GetResourceData<Bitmap>(assembly, "Intermech.Client.Core.Resources.OrganizerTask.bmp"))
    {
      if (resourceData == null)
        return;
      resourceData.MakeTransparent();
      namedImgList.Add((Image) resourceData, "imgOrganizerTask");
    }
  }
}
