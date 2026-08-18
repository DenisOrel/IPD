// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.ComponentSelectionConsts
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Pdm;

public static class ComponentSelectionConsts
{
  /// <summary>
  /// Глобальный идентификатор типа связей Подборный компонент
  /// </summary>
  public static readonly Guid relationTypeComponentSelection = new Guid("cadd9740-306c-11d8-b4e9-00304f19f545");
  /// <summary>
  /// Глобальный идентификатор атрибута Подбор для позиционного обозначение
  /// </summary>
  public static readonly Guid attributeSelectionForPosDesignation = new Guid("cadd9741-306c-11d8-b4e9-00304f19f545");
  /// <summary>
  /// Глобальный идентификатор атрибута Количество на регулировку
  /// </summary>
  public static readonly Guid attributeCountOnRegulation = new Guid("cad007a6-306c-11d8-b4e9-00304f19f545");
  /// <summary>
  /// Глобальный идентификатор атрибута "Предельные значения"
  /// </summary>
  public static readonly Guid attributeNominals = new Guid("cadd973c-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор атрибута "Подбор"</summary>
  public static readonly Guid attributeReplace = new Guid("cadd943a-306c-11d8-b4e9-00304f19f545");
}
