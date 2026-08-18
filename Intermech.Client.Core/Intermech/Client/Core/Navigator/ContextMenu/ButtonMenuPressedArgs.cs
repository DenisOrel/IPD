
// Type: Intermech.Client.Core.Navigator.ContextMenu.ButtonMenuPressedArgs
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Navigator.Interfaces;
using System;


namespace Intermech.Client.Core.Navigator.ContextMenu;

/// <summary>
/// Аргументы события при выборе объекта в меню привязаанной выборки
/// </summary>
public class ButtonMenuPressedArgs
{
  /// <summary>
  /// Информация по текущему объекту к которому привязанна выборка
  /// </summary>
  public AttachedSelObjectInfo ObjectInfo;
  /// <summary>Контейнер сервисов из ChildrenView</summary>
  public IServiceProvider Services;
  /// <summary>Запрос для текущей выборки</summary>
  public INodeQuery Query;

  public ButtonMenuPressedArgs(
    AttachedSelObjectInfo objectInfo,
    IServiceProvider services,
    INodeQuery query)
  {
    this.ObjectInfo = objectInfo;
    this.Services = services;
    this.Query = query;
  }
}
