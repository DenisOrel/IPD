// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.CompositionItemFlags
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Pdm;

/// <summary>
/// Флаги для позиции в составе, описывают результаты сравнения позиций
/// </summary>
[Flags]
public enum CompositionItemFlags
{
  /// <summary>Позиция в составе аналогична со сравниваемой</summary>
  Equal = 0,
  /// <summary>
  /// Атрибуты позиции в составе (объекта или связи) не равны с атрибутами сравниваемой позиции
  /// </summary>
  AttributesChanged = 1,
  /// <summary>Позиция была добавлена в состав</summary>
  Added = 2,
  /// <summary>Позиция была удалена из состава</summary>
  Removed = 4,
  /// <summary>Изменен объект в составе позиции</summary>
  ChangedInComposition = 8,
  /// <summary>Изменилась версия</summary>
  AnotherVersion = 16, // 0x00000010
  /// <summary>Изменились атрибуты у объекта в составе</summary>
  AttributesChangedInCompositionObject = 32, // 0x00000020
  /// <summary>
  /// Флаг для создания новой производственной копии в составе ПВ
  /// </summary>
  CreateNewCopy = 64, // 0x00000040
}
