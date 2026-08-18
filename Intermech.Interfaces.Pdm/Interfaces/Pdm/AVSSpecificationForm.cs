// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.AVSSpecificationForm
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using Intermech.Localization;

#nullable disable
namespace Intermech.Interfaces.Pdm;

/// <summary>Форма спецификации</summary>
public enum AVSSpecificationForm
{
  /// <summary>Единичная</summary>
  [CustomDescription("Attribute.Interfaces.Pdm_28")] Single,
  /// <summary>Групповая А</summary>
  [CustomDescription("Attribute.Interfaces.Pdm_29")] A,
  /// <summary>Групповая Б</summary>
  [CustomDescription("Attribute.Interfaces.Pdm_30")] B,
  /// <summary>Зеркальная</summary>
  [CustomDescription("Attribute.Interfaces.Pdm_31")] Mirror,
  /// <summary>Групповая В</summary>
  [CustomDescription("Attribute.Interfaces.Pdm_32")] V,
  /// <summary>Групповая Г</summary>
  [CustomDescription("Attribute.Interfaces.Pdm_33")] G,
  /// <summary>Автомобильная</summary>
  [CustomDescription("Attribute.Interfaces.Pdm_34")] AvtoProm,
}
