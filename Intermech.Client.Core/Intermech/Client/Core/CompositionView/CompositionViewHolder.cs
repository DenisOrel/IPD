
// Type: Intermech.Client.Core.CompositionView.CompositionViewHolder
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Bars;
using Intermech.Docking;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Search;
using System;
using System.ComponentModel.Design;


namespace Intermech.Client.Core.CompositionView;

/// <summary>Статический класс для управления панелью состава</summary>
public class CompositionViewHolder
{
  /// <summary>Сервисы для работы с панелью</summary>
  public static IServiceContainer Services = (IServiceContainer) new ServiceContainer();
  /// <summary>Сервис событий для CompositionView</summary>
  public static CompositionViewEvents CompositionViewEvents = new CompositionViewEvents();
  /// <summary>
  /// Является ли залогиневшийся пользователь администратором
  /// </summary>
  public static bool IsAdmin = false;
  /// <summary>Текущий пользователь и роль</summary>
  [NonSerialized]
  protected static ICurrentUserAndRole _userRole;
  /// <summary>CompositionView</summary>
  private static Intermech.Client.Core.CompositionView.CompositionView _cView = (Intermech.Client.Core.CompositionView.CompositionView) null;

  /// <summary>Текущий пользователь и роль</summary>
  protected internal static ICurrentUserAndRole UserRole
  {
    get
    {
      if (CompositionViewHolder._userRole == null)
        CompositionViewHolder._userRole = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
      return CompositionViewHolder._userRole;
    }
  }

  /// <summary>Регистрировать пункт меню</summary>
  /// <param name="serviceProvder"></param>
  public static void Register(IServiceProvider serviceProvder)
  {
    BarManager service1 = serviceProvder.GetService(typeof (BarManager)) as BarManager;
    INamedImageList service2 = serviceProvder.GetService(typeof (INamedImageList)) as INamedImageList;
    if (service1 != null)
    {
      MenuButtonItem menuButtonItem = new MenuButtonItem(LocalizationHolder.rm.GetString("Client.Core_28"), new EventHandler(CompositionViewHolder.CompositionItemClick));
      if (service2 != null)
        menuButtonItem.ImageIndex = service2.ImageIndex("imgTreeView");
      ServiceLocator.Get<IMainMenuService>().RegisterMenuItems(MainMenuItemSite.ViewBottom, MainMenuItemPosition.Default, menuButtonItem);
      (serviceProvder.GetService(typeof (IContentProvider)) as IContentProvider).ContentCallback += new GetContentCallback(CompositionViewHolder.cp_ContentCallback);
    }
    CompositionViewHolder.Services.AddService(typeof (CommonButtonService), (object) new CommonButtonService());
    CompositionViewHolder.Services.AddService(typeof (CustomButtonService), (object) new CustomButtonService());
    CompositionViewHolder.Services.AddService(typeof (INamedImageList), (object) service2);
    CompositionViewHolder.Services.AddService(typeof (CompositionCacheServices), (object) new CompositionCacheServices());
    ServicesManager.ServiceContainer.AddService(typeof (CompositionViewButtons), (object) new CompositionViewButtons()
    {
      {
        typeof (cvTypeButton),
        LocalizationHolder.rm.GetString("Client.Core_29")
      },
      {
        typeof (cvCompositionButton),
        LocalizationHolder.rm.GetString("Client.Core_30")
      }
    });
    CompositionViewHolder.IsAdmin = (ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole).IsAdmin;
  }

  /// <summary>Открытие редактора состава</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private static void CompositionItemClick(object sender, EventArgs e)
  {
    DockManager service = ServicesManager.GetService(typeof (DockManager)) as DockManager;
    if (CompositionViewHolder._cView == null)
      CompositionViewHolder._cView = new Intermech.Client.Core.CompositionView.CompositionView(service);
    CompositionViewHolder._cView.Show(service);
    CompositionViewHolder._cView.CompositionView_Open((object) null, EventArgs.Empty);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="guid"></param>
  /// <param name="persistString"></param>
  /// <returns></returns>
  public static DockControl cp_ContentCallback(Guid guid, string persistString)
  {
    if (!guid.Equals(Intermech.Client.Core.CompositionView.CompositionView.CompositionViewGuid))
      return (DockControl) null;
    if (CompositionViewHolder._cView == null)
    {
      CompositionViewHolder._cView = new Intermech.Client.Core.CompositionView.CompositionView(ServicesManager.GetService(typeof (DockManager)) as DockManager);
      CompositionViewHolder._cView.CompositionView_Open((object) null, EventArgs.Empty);
    }
    return (DockControl) CompositionViewHolder._cView;
  }
}
