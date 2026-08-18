// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.IObjectListFiltration
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Kernel.Search;
using System;

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>
/// Сервис позволяет управлять фильтрацией списков объектов на основании
/// специальным образом подготовленных выборок
/// </summary>
public interface IObjectListFiltration
{
  /// <summary>
  /// Разрешено ли использовать дополнительно основные критерии от текущего правила подбора версий объектов
  /// </summary>
  bool FilterByCurrentVersionsRule { get; }

  /// <summary>Активен ли поиск по индексу</summary>
  bool IsGlobalIndexSearchActived { get; }

  /// <summary>
  /// Guid выборки, по которой будет выполняться фильтрация.
  /// Guid.Empty - фильтрация отключена
  /// </summary>
  Guid SelectedFilterGuid { get; }

  /// <summary>Значение для поиска по индексу</summary>
  GlobalIndexSearchValue GlobalIndexSearchValue { get; }
}
