// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.ISearchSchemeSettingsService
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Pdm;

/// <summary>Настройки схем поиска</summary>
public interface ISearchSchemeSettingsService
{
  /// <summary>
  /// Получить значение параметра Фильтровать список объектов по атрибуту "Видимость объекта"
  /// </summary>
  bool VisibilityFilter { get; }

  /// <summary>
  /// Установить значение параметра Фильтровать список объектов по атрибуту "Видимость объекта"
  /// </summary>
  void SetVisibilityFilter(Guid sessionGuid, bool value);
}
