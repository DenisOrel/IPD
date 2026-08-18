
// Type: Intermech.Search.RecentObjects.RecentObjectsClientModule
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.Collections;
using System.Drawing;
using System.IO;


namespace Intermech.Search.RecentObjects;

public sealed class RecentObjectsClientModule
{
  private ICategoryTypeIconService _categoryTypeIconService;
  private IFactory _factory;
  private IGuidMapper _guidMapper;
  private INamedImageList _namedImageList;
  private INotificationService _notificationService;
  private RecentObjectsClientService _recentObjectsClientService;
  private RecentObjectsCommandsProvider _recentObjectsCommandsProvider;
  private RecentObjectsViewsProvider _recentObjectsViewsProvider = new RecentObjectsViewsProvider();
  private MenuTemplateNode _changeRecentObjectsAccessSettingsMenuTemplateNode = new MenuTemplateNode("ChangeRecentObjectsAccessSettings", "Предоставить доступ", -1, -1, -1);
  private MenuTemplateNode _openOtherUserRecentObjectsMenuTemplateNode = new MenuTemplateNode("OpenOtherUserRecentObjects", "Открыть недавние объекты другого пользователя", -1, -1, -1);

  public RecentObjectsClientModule(
    ICategoryTypeIconService categoryTypeIconService,
    IFactory factory,
    IGuidMapper guidMapper,
    INamedImageList namedImageList,
    INotificationService notificationService)
  {
    if (categoryTypeIconService == null)
      throw new ArgumentNullException(nameof (categoryTypeIconService));
    if (factory == null)
      throw new ArgumentNullException(nameof (factory));
    if (guidMapper == null)
      throw new ArgumentNullException(nameof (guidMapper));
    if (namedImageList == null)
      throw new ArgumentNullException(nameof (namedImageList));
    if (notificationService == null)
      throw new ArgumentNullException(nameof (notificationService));
    this._categoryTypeIconService = categoryTypeIconService;
    this._factory = factory;
    this._guidMapper = guidMapper;
    this._namedImageList = namedImageList;
    this._notificationService = notificationService;
  }

  public void Load()
  {
    this._recentObjectsClientService = new RecentObjectsClientService(this._notificationService);
    ServiceLocator.Register<IRecentObjectsClientService>((IRecentObjectsClientService) this._recentObjectsClientService);
    Intermech.Navigator.Consts.CategoryRecentObjectsNode = this._guidMapper.Register(Intermech.Navigator.Consts.CategoryRecentObjectsNodeGuid);
    this._factory.ContextMenuTemplate.Nodes.AddRange((ICollection) new MenuTemplateNode[2]
    {
      this._changeRecentObjectsAccessSettingsMenuTemplateNode,
      this._openOtherUserRecentObjectsMenuTemplateNode
    });
    this._recentObjectsCommandsProvider = new RecentObjectsCommandsProvider((IRecentObjectsClientService) this._recentObjectsClientService);
    this._factory.AddCommandsProvider((ICommandsProvider) this._recentObjectsCommandsProvider);
    this._factory.AddViewsProvider((IViewsProvider) this._recentObjectsViewsProvider);
    this._factory.AddNodeType(Intermech.Navigator.Consts.CategoryRecentObjectsNode, typeof (CurrentUserRecentObjectsNode));
    using (Stream resourceStream = Intermech.Navigator.Services.GetResourceStream("ObjectTypes.ico"))
    {
      using (Icon icon = new Icon(resourceStream))
      {
        this._namedImageList.Add(icon, "imgRecentObjects");
        this._categoryTypeIconService.AddIcon(icon, Intermech.Navigator.Consts.CategoryRecentObjectsNode, 0);
      }
    }
    this._factory.AddGlobalNode(new Guid("58412DFC-D3FD-4D9F-A904-AD7528774EF5"), (IDescriptor) new CurrentUserRecentObjectsDescriptor(), 10);
  }

  public void Unload()
  {
    ServiceLocator.Unregister<IRecentObjectsClientService>();
    this._factory.ContextMenuTemplate.Nodes.Remove(this._changeRecentObjectsAccessSettingsMenuTemplateNode);
    this._factory.ContextMenuTemplate.Nodes.Remove(this._openOtherUserRecentObjectsMenuTemplateNode);
    this._factory.RemoveCommandsProvider((ICommandsProvider) this._recentObjectsCommandsProvider);
  }
}
