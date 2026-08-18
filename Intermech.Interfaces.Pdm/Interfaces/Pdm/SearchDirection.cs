// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.SearchDirection
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using Intermech.Localization;
using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.Pdm;

/// <summary>Направление поиска</summary>
[TypeConverter(typeof (EnumDescConverter))]
[CustomDescription("Attribute.Interfaces.Pdm_5")]
[Category("Misc")]
public enum SearchDirection
{
  /// <summary>Состав</summary>
  [CustomDescription("Attribute.Interfaces.Pdm_6")] Contains,
  /// <summary>Применяемость</summary>
  [CustomDescription("Attribute.Interfaces.Pdm_7")] EntersTo,
  /// <summary>Развёрнутый состав</summary>
  [CustomDescription("Attribute.Interfaces.Pdm_8")] RecursiveContains,
  /// <summary>Развёрнутая применяемость</summary>
  [CustomDescription("Attribute.Interfaces.Pdm_9")] RecursiveEntersTo,
}
