// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.ICompositionByObjectTypesFiltration
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>
/// Интерфейс для работы с текущим фильтром составов по родительским и дочерним типам объектов
/// </summary>
public interface ICompositionByObjectTypesFiltration
{
  /// <summary>Коллекция фильтров</summary>
  ICompositionByObjectTypesFilters Filters { get; set; }

  /// <summary>
  /// Guid активного фильтра по родительским и дочерним типам объектов. Если
  /// значение равно Guid.Empty, то фильтрация отключена
  /// </summary>
  Guid ActiveFilterGuid { get; set; }
}
