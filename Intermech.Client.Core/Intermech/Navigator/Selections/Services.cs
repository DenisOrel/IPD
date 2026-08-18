
// Type: Intermech.Navigator.Selections.Services
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Bars;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Configuration;
using Intermech.Localization;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.DBObjectTypes;
using Intermech.Navigator.Selections.Implementation;
using Intermech.Navigator.SelectionView;
using Intermech.Navigator.Views;
using Intermech.Search;
using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;


namespace Intermech.Navigator.Selections;

/// <summary>
/// Предоставляет методы для инициализации механизма выборок и завершения его
/// работы.
/// </summary>
public sealed class Services
{
  private static readonly string showInternalFoldersCaption = LocalizationHolder.rm.GetString("Client.Core_402");
  private static string _configShowInternalFolders = "ShowInternalFolders";
  private static string _configItemChecked = "ItemChecked";

  /// <summary>Инициализирует механизм выборок.</summary>
  /// <remarks>
  /// Этот метод получает из базы данных необходимые сведения и регистрирует
  /// все плагины для навигатора, отвечающие за работу выборок
  /// </remarks>
  public static void Start()
  {
    Services.InitUserInterface();
    Services.InitMenuTemplate();
    Services.InitEvents();
    Services.InitPlugins();
  }

  /// <summary>Останавливает механизм выборок.</summary>
  public static void Stop()
  {
    Services.DisposePlugins();
    Services.DisposeEvents();
    Services.DisposeMenuTemplate();
    Services.DisposeUserInterface();
  }

  private static void InitUserInterface() => Services.CreateMenuItemShowInternalFolders();

  private static void InitMenuTemplate()
  {
    if (Holder.Factory == null)
      return;
    MenuTemplate contextMenuTemplate = Holder.Factory.ContextMenuTemplate;
    contextMenuTemplate.BeginUpdate();
    try
    {
      INamedImageList service = (INamedImageList) ServicesManager.GetService(typeof (INamedImageList));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("IncludeToSelection", LocalizationHolder.rm.GetString("Client.Core_1635"), service.ImageIndex("imgIncludeToSelection"), 99, 10, Keys.I | Keys.Control));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("ExcludeFromSelection", LocalizationHolder.rm.GetString("Client.Core_404"), service.ImageIndex("imgExcludeFromSelection"), 99, 20, Keys.E | Keys.Control));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("PasteAsLink", LocalizationHolder.rm.GetString("Client.Core_1676"), service.ImageIndex("imgPasteAsLink"), 60, 41));
    }
    finally
    {
      contextMenuTemplate.EndUpdate();
    }
  }

  private static void InitEvents()
  {
    if (Holder.IconService == null)
      return;
    Holder.IconService.FindIcon += new FindIconEventHandler(Services.FindIcon);
  }

  private static void InitPlugins()
  {
    Intermech.Navigator.Consts.CategorySelectionsNode = Holder.GuidMapper.Register(Intermech.Navigator.Consts.CategorySelectionsNodeGuid);
    Holder.Factory.AddNodeType(Intermech.Navigator.Consts.CategorySelectionsNode, typeof (HiveNode));
    Holder.Factory.AddViewsProvider(Intermech.Navigator.Consts.CategorySelectionsNode, (IViewsProvider) new HiveViewsProvider());
    Holder.Factory.AddCommandsProvider(Intermech.Navigator.Consts.CategorySelectionsNode, (ICommandsProvider) new HiveCommandsProvider());
    Holder.Factory.AddNodeType(1, Consts.SelectionsTypeID, typeof (SelectionNode));
    Holder.Factory.AddViewsProvider(1, Consts.SelectionsTypeID, (IViewsProvider) new SelectionViewProvider());
    Holder.Factory.AddCommandsProvider(1, Consts.SelectionsTypeID, (ICommandsProvider) new SelectionCommandsProvider());
    Holder.Factory.AddCommandsProvider(1, (ICommandsProvider) new ObjectCommandsProvider());
    int objectTypeId1 = MetaDataHelper.GetObjectTypeID(new Guid("cad00156-306c-11d8-b4e9-00304f19f545"));
    if (objectTypeId1 != -1)
      Holder.Factory.AddNodeType(4, objectTypeId1, typeof (SelectionTypeNode));
    int objectTypeId2 = MetaDataHelper.GetObjectTypeID(new Guid("cad00157-306c-11d8-b4e9-00304f19f545"));
    if (objectTypeId2 != -1)
      Holder.Factory.AddNodeType(4, objectTypeId2, typeof (SelectionTypeNode));
    int objectTypeId3 = MetaDataHelper.GetObjectTypeID(new Guid("cad00119-306c-11d8-b4e9-00304f19f545"));
    if (objectTypeId3 != -1)
      Holder.Factory.AddNodeType(4, objectTypeId3, typeof (SelectionTypeNode));
    ServicesManager.AddService(typeof (ISelectionFormCustomCommandsService), (object) new SelectionFormToolBarService());
  }

  private static void DisposeUserInterface()
  {
  }

  private static void DisposeMenuTemplate()
  {
    if (Holder.Factory == null)
      return;
    MenuTemplate contextMenuTemplate = Holder.Factory.ContextMenuTemplate;
    contextMenuTemplate.BeginUpdate();
    try
    {
    }
    finally
    {
      contextMenuTemplate.EndUpdate();
    }
  }

  private static void DisposeEvents()
  {
    if (Holder.IconService == null)
      return;
    Holder.IconService.FindIcon -= new FindIconEventHandler(Services.FindIcon);
  }

  private static void DisposePlugins()
  {
  }

  /// <summary>
  /// Метод для создания пункта меню "Отображать содержимое вложенных папок классификатора"
  /// </summary>
  public static void CreateMenuItemShowInternalFolders()
  {
    INamedImageList service1 = (INamedImageList) ServicesManager.GetService(typeof (INamedImageList));
    service1.Add(Intermech.Client.Core.Properties.Resources.exclude_selection, "imgExcludeFromSelection");
    service1.Add(Intermech.Client.Core.Properties.Resources.include_selection, "imgIncludeToSelection");
    service1.Add(Intermech.Client.Core.Properties.Resources.PasteAsLink, "imgPasteAsLink");
    ((BarManager) ServicesManager.GetService(typeof (BarManager))).MenuBar.FindMenuBar("View");
    MenuButtonItem menuButtonItem = new MenuButtonItem(Services.showInternalFoldersCaption, new EventHandler(Services.ShowInternalFoldersClick));
    ISelectionsService service2 = (ISelectionsService) ServicesManager.GetService(typeof (ISelectionsService));
    if (service2 != null)
    {
      bool newValue = false;
      IConfigurationManager service3 = ServicesManager.GetService(typeof (IConfigurationManager)) as IConfigurationManager;
      IConfiguration configuration = service3.Open(Services._configShowInternalFolders);
      if (configuration == null)
        service3.Create(Services._configShowInternalFolders).SetProperty(Services._configItemChecked, newValue.ToString((IFormatProvider) CultureInfo.InvariantCulture));
      else
        newValue = Convert.ToBoolean(configuration.GetProperty(Services._configItemChecked), (IFormatProvider) CultureInfo.InvariantCulture);
      service2.SetShowInternalFolders(newValue);
      menuButtonItem.Checked = newValue;
    }
    if (!(ServicesManager.GetService(typeof (IMainMenuService)) is IMainMenuService service4))
      return;
    MenuButtonItem[] menuButtonItemArray = new MenuButtonItem[1]
    {
      menuButtonItem
    };
    service4.RegisterMenuItems(MainMenuItemSite.ViewMiddle, MainMenuItemPosition.Default, menuButtonItemArray);
  }

  /// <summary>
  /// Оработчик для пункта меню "Показывать содержимое вложенных папок классификаторов"
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private static void ShowInternalFoldersClick(object sender, EventArgs e)
  {
    if (!(sender is MenuButtonItem))
      return;
    MenuButtonItem menuButtonItem = sender as MenuButtonItem;
    ISelectionsService service1 = (ISelectionsService) ServicesManager.GetService(typeof (ISelectionsService));
    if (service1 == null)
      return;
    bool newValue = !menuButtonItem.Checked;
    (ServicesManager.GetService(typeof (IConfigurationManager)) as IConfigurationManager).Open(Services._configShowInternalFolders).SetProperty(Services._configItemChecked, newValue.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    service1.SetShowInternalFolders(newValue);
    service1.ClearCashe();
    menuButtonItem.Checked = newValue;
    if (!(ServicesManager.GetService(typeof (INotificationService)) is INotificationService service2))
      return;
    service2.FireEvent((object) null, new NotificationEventArgs("FiltrationChanged"));
  }

  /// <summary>
  /// Возвращает иконку для виртуальных элементов навигации, таких как
  /// "Выборки" и "Классификаторы".
  /// </summary>
  /// <param name="category">Идентификатор категории элемента навигации</param>
  /// <param name="type">Идентификатор типа элемента навигации</param>
  /// <param name="data">Дополнительные данные</param>
  /// <returns>Иконка</returns>
  private static Icon FindIcon(int category, int type, object data)
  {
    return category == Intermech.Navigator.Consts.CategorySelectionsNode ? Holder.IconService.GetIcon(4, type, (object) null) : (Icon) null;
  }
}
