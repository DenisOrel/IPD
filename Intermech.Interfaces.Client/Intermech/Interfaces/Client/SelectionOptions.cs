// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.SelectionOptions
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Набор флажков, позволяющий фильтровать выбираемые в окне SelectionWindow узлы
/// (позволяет выбирать в окне узлы, содержащие определённые данные),
/// скрывать и показывать дерево или панель закладок, управлять множественным выделением
/// </summary>
[Flags]
public enum SelectionOptions : long
{
  /// <summary>
  /// Позволяет выбирать любые узлы из дерева и списка, в окне доступны
  /// как дерево "Навигатора", так и панель закладок
  /// </summary>
  Default = 4352, // 0x0000000000001100
  /// <summary>Скрывать панель закладок (правая часть окна)</summary>
  HideViews = 1,
  /// <summary>Скрывать дерево "Навигатора" (левая часть окна)</summary>
  HideTree = 2,
  /// <summary>Скрывать панели управления в закладках</summary>
  HideViewsToolbar = 16, // 0x0000000000000010
  /// <summary>Скрывать панель группирования в закладках</summary>
  HideViewsGroupingBox = 32, // 0x0000000000000020
  /// <summary>Скрывать статусные строки в закладках</summary>
  HideViewsStatusBar = 64, // 0x0000000000000040
  /// <summary>Выбирать в окне можно узлы, содержащие объекты</summary>
  SelectObjects = 256, // 0x0000000000000100
  /// <summary>Выбирать в окне можно узлы, содержащие типы объектов</summary>
  SelectObjectTypes = 512, // 0x0000000000000200
  /// <summary>Выбирать в окне можно узлы, содержащие связи</summary>
  SelectRelations = 4096, // 0x0000000000001000
  /// <summary>Выбирать в окне можно узлы, содержащие типы связей</summary>
  SelectRelationTypes = 8192, // 0x0000000000002000
  /// <summary>Выбирать в окне можно любые другие типы узлов</summary>
  SelectOtherNodes = 65536, // 0x0000000000010000
  /// <summary>
  /// Запретить выбирать в окне из дерева "Навигатора" (левая часть окна)
  /// </summary>
  DisableSelectFromTree = 1048576, // 0x0000000000100000
  /// <summary>
  /// Запретить выбирать в окне из закладок (правая часть окна)
  /// </summary>
  DisableSelectFromViews = 2097152, // 0x0000000000200000
  /// <summary>Запретить фильтр списков объектов в окне</summary>
  DisableObjectListFilter = 4194304, // 0x0000000000400000
  /// <summary>Запретить в окне выбирать абстрактные типы объектов</summary>
  DisableSelectAbstractTypes = 8388608, // 0x0000000000800000
  /// <summary>
  /// Запретить в гриде и дереве множественный выбор элементов,
  /// допускается выбор только одного элемента
  /// </summary>
  DisableMultiselect = 16777216, // 0x0000000001000000
  /// <summary>Запретить сортировку колонок в дереве Навигатора</summary>
  DisableTreeSorting = 33554432, // 0x0000000002000000
  /// <summary>
  /// Принудительно вызвать перестроение дерева, даже если форма
  /// найдена в кэше
  /// </summary>
  ForceRebuildNavTree = 4294967296, // 0x0000000100000000
  /// <summary>
  /// Принудительно включить кнопку "Фильтровать списки объектов по текущему правилу подбора версий"
  /// </summary>
  ForceFilterObjectsByRule = 8589934592, // 0x0000000200000000
}
