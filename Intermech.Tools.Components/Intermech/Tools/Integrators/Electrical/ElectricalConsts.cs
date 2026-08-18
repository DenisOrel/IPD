// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Electrical.ElectricalConsts
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;

#nullable disable
namespace Intermech.Tools.Integrators.Electrical;

public static class ElectricalConsts
{
  /// <summary>
  /// Глобальный идентификатор атрибута "Данные проекта ECAD"
  /// </summary>
  public static readonly Guid attributeProjectData = new Guid("cadd973a-306c-11d8-b4e9-00304f19f545");
  /// <summary>
  /// Глобальный идентификатор атрибута "Элемент перечня элементов"
  /// </summary>
  public static readonly Guid attributeElementEL = new Guid("cadd973b-306c-11d8-b4e9-00304f19f545");
  /// <summary>
  /// Глобальный идентификатор атрибута "Позиционное обозначение функциональной группы"
  /// </summary>
  public static readonly Guid attributeFGPosDesignation = new Guid("cadd973d-306c-11d8-b4e9-00304f19f545");
  /// <summary>
  /// Глобальный идентификатор атрибута "Обозначение функциональной группы"
  /// </summary>
  public static readonly Guid attributeFGDesignation = new Guid("cadd973f-306c-11d8-b4e9-00304f19f545");
  /// <summary>
  /// Глобальный идентификатор атрибута "Наименование функциональной группы"
  /// </summary>
  public static readonly Guid attributeFGName = new Guid("cadd973e-306c-11d8-b4e9-00304f19f545");
  /// <summary>
  /// Глобальный идентификатор атрибута "Предельные значения"
  /// </summary>
  public static readonly Guid attributeNominals = new Guid("cadd973c-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор атрибута "Подбор"</summary>
  public static readonly Guid attributeReplace = new Guid("cadd943a-306c-11d8-b4e9-00304f19f545");
  /// <summary>
  /// Глобальный идентификатор атрибута "Позиционное обозначение ДС"
  /// </summary>
  public static readonly Guid attributeASPosDesignation = new Guid("cadd98d4-306c-11d8-b4e9-00304f19f545");
  /// <summary>
  /// Название параметра у компонента с позиционным глобальным идентификатором
  /// </summary>
  public static readonly string PosGuidAttribute = "PosGuid";
}
