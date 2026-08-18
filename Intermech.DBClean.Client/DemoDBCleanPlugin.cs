// Decompiled with JetBrains decompiler
// Type: Intermech.DBClean.Client.DemoDBCleanPlugin
// Assembly: Intermech.DBClean.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 973F13FD-72F3-4555-9BF9-74AC5C606885
// Assembly location: D:\IPS\Client\Intermech.DBClean.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.DBClean.Client.xml

using Intermech.Bars;
using Intermech.Docking;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Plugins;
using Intermech.Search;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.DBClean.Client;

public class DemoDBCleanPlugin : IPackage, ICommandTarget
{
  private IServiceProvider serviceProvider;

  void IPackage.Load(IServiceProvider serviceProvider)
  {
    this.serviceProvider = serviceProvider;
    ICommandManager service1 = (ICommandManager) serviceProvider.GetService(typeof (ICommandManager));
    service1.AddTarget((ICommandTarget) this);
    if (!(ServicesManager.GetService(typeof (IMainMenuService)) is IMainMenuService service2))
      return;
    MenuButtonItem menuButtonItem = new MenuButtonItem("Очистить демонстрационную БД");
    menuButtonItem.CommandName = "ClearDemoDB";
    menuButtonItem.ToolTipText = "Очистка демонстрационной БД";
    menuButtonItem.BeginGroup = true;
    MenuButtonItem[] menuButtonItemArray = new MenuButtonItem[1]
    {
      menuButtonItem
    };
    service2.RegisterMenuItems(MainMenuItemSite.AdministratorUtilities, MainMenuItemPosition.Default, menuButtonItemArray);
    service1.Add((ButtonItemBase) menuButtonItem);
  }

  /// <summary>Провайдер сервисов клиента</summary>
  public IServiceProvider ServiceProvider
  {
    [DebuggerStepThrough] get => this.serviceProvider;
  }

  void IPackage.Unload()
  {
  }

  public string Name => "Плагин очистки демонстрационной БД";

  bool ICommandTarget.Execute(ICommandState commandState)
  {
    if (!(commandState.CommandName == "ClearDemoDB"))
      return false;
    DockManager service = (DockManager) this.serviceProvider.GetService(typeof (DockManager));
    DemoDbCleanForm control = new DemoDbCleanForm();
    service.DocumentContainer.AddDocument((DockControl) control);
    control.Activate();
    return true;
  }

  bool ICommandTarget.QueryStatus(ICommandState commandState)
  {
    if (!(commandState.CommandName == "ClearDemoDB"))
      return false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      commandState.Visible = sessionKeeper.Session.RoleID == sessionKeeper.Session.IdentHelper.AdminRoleID && sessionKeeper.Session.DeveloperMode;
    return true;
  }
}
