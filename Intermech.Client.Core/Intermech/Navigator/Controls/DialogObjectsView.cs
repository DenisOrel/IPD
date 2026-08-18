
// Type: Intermech.Navigator.Controls.DialogObjectsView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.DBObjects;
using System.ComponentModel.Design;


namespace Intermech.Navigator.Controls;

public class DialogObjectsView : ObjectsViewBase
{
  /// <summary>Контейнер сервисов контекстного меню закладки</summary>
  /// <returns>Контейнер сервисов контекстного меню закладки</returns>
  protected override IServiceContainer GetMenuServiceContainer()
  {
    return DialogChildrenView.DisableGlobalCommandProviders((object) this, base.GetMenuServiceContainer());
  }
}
