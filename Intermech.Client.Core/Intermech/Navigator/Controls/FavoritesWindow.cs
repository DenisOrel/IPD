
// Type: Intermech.Navigator.Controls.FavoritesWindow
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;
using System;


namespace Intermech.Navigator.Controls;

/// <summary>Окно Избранное</summary>
public class FavoritesWindow : WellKnownNavWindow, IFavoritesWindow
{
  /// <summary>Guid окна "Избранное"</summary>
  public static readonly Guid _persistStateGuidNew = new Guid("{2F5CCFA2-4183-4770-8C84-A35077ED00FC}");

  public FavoritesWindow()
  {
    this.Guid = FavoritesWindow._persistStateGuidNew;
    if (ServicesManager.GetService(typeof (IFavoritesWindow)) is IFavoritesWindow)
      return;
    ServicesManager.AddService(typeof (IFavoritesWindow), (object) this);
  }

  /// <summary>Форма активирована</summary>
  public override void Activated()
  {
    if (!(ServicesManager.GetService(typeof (IFavoritesWindow)) is IFavoritesWindow))
      ServicesManager.AddService(typeof (IFavoritesWindow), (object) this);
    base.Activated();
    this.TreeView.SupportedColumns = Utils.CaptionColumnOnly(NodeColumnSortOrder.Ascending);
    this.TreeView.SetColumns(Utils.CaptionColumnOnly(NodeColumnSortOrder.Ascending));
  }

  /// <summary>Обновить содержимое окна</summary>
  void IFavoritesWindow.Update()
  {
    if (this.Visible)
      return;
    this.FullWindowRefresh();
  }

  /// <summary>Открыто ли окно в данный момент на экране</summary>
  /// <returns></returns>
  public bool IsVisible() => this.Visible;
}
