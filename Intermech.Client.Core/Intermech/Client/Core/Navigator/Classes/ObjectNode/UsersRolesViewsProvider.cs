
// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.UsersRolesViewsProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode;

/// <summary>
/// Провайдер для вьюхи отображения входящих в роли пользователей
/// </summary>
internal class UsersRolesViewsProvider : IViewsProvider
{
  public ViewsInfo GetViews(ISelectedItems items, IServiceProvider services)
  {
    if (items.Count != 1)
      return ViewsInfo.Empty;
    ViewsInfo views = new ViewsInfo();
    views.Add("ChildrenView", new ViewInfo(0, 1091, typeof (UsersRolesView)));
    return views;
  }
}
