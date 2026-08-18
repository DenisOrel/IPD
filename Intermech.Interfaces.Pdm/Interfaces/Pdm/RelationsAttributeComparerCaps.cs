// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.RelationsAttributeComparerCaps
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

#nullable disable
namespace Intermech.Interfaces.Pdm;

/// <summary>Возможности интерфейса по сравнению атрибутов</summary>
public enum RelationsAttributeComparerCaps
{
  /// <summary>
  /// Интерфейс умеет выполнять сравнение по одному атрибуту
  /// (наиболее высокий приоритет при выборе интерфейса для сравнения)
  /// </summary>
  BySingleAttribute,
  /// <summary>
  /// Интерфейс умеет выполнять сравнение по нескольким атрибутам
  /// (средний приоритет при выборе интерфейса для сравнения)
  /// </summary>
  BySomeAttributes,
  /// <summary>
  /// Интерфейс умеет выполнять сравнение по любым атрибутам
  /// (самый низкий приоритет при выборе интерфейса для сравнения)
  /// </summary>
  ByAnyAttributes,
}
