// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.ViewStateFlags
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>Флажки состояния узла</summary>
[Flags]
public enum ViewStateFlags : long
{
  /// <summary>Никаких флажков нет</summary>
  None = 0,
  /// <summary>Вьюшка располагается в диалоговом окне</summary>
  InDialog = 536870913, // 0x0000000020000001
  /// <summary>Вьюшка должна работать в режиме "только чтение"</summary>
  ReadOnly = 2,
  /// <summary>Запретить показываться вьюшке "Состоит из"</summary>
  NoCompositionView = 4,
  /// <summary>Запретить показываться вьюшке "Состав\Применяемость"</summary>
  NoContainsInView = 8,
  /// <summary>Запретить показываться вьюшке "Действия над объектом"</summary>
  NoEventsView = 16, // 0x0000000000000010
  /// <summary>
  /// Запретить показываться вьюшкам плагинов (плагин сам должен решать, игнорировать флажок или нет)
  /// </summary>
  NoPluginsViews = 32, // 0x0000000000000020
  /// <summary>
  /// Запретить показываться закладке "Группирующие объекты"
  /// </summary>
  NoGroupingObjectsViews = 64, // 0x0000000000000040
  /// <summary>Узел находится в составе дерева</summary>
  NodeInTree = 128, // 0x0000000000000080
  /// <summary>Узел находится в составе закладок</summary>
  NodeInViews = 256, // 0x0000000000000100
  /// <summary>
  /// Узел находится в составе закладок, расположенных под деревом Навигатора
  /// </summary>
  NodeUnderTree = 512, // 0x0000000000000200
  /// <summary>
  /// Узел находится в элементе управления, размещённом в окне "Карточка"
  /// </summary>
  InParametersCard = 4096, // 0x0000000000001000
  /// <summary>
  /// Узел находится в мастере по созданию объектов и связей
  /// </summary>
  InObjectCreatorDialog = 8192, // 0x0000000000002000
  /// <summary>Узел находится в окне по выбору элементов Навигатора</summary>
  InSelectionWindow = 16384, // 0x0000000000004000
  /// <summary>
  /// Запретить загрузку глобальных комманд для категории/типа в данном контексте
  /// </summary>
  DisableGlobalCommandProviders = 32768, // 0x0000000000008000
}
