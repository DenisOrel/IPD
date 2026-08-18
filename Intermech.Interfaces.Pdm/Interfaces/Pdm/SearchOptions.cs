// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.SearchOptions
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using Intermech.Localization;
using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.Pdm;

/// <summary>Опции поиска</summary>
[TypeConverter(typeof (EnumDescConverter))]
[CustomDescription("Interfaces.Pdm_55")]
[Category("Misc")]
[Flags]
public enum SearchOptions
{
  /// <summary>Нет опций</summary>
  [CustomDescription("Interfaces.Pdm_56")] None = 0,
  /// <summary>Нет опций</summary>
  [CustomDescription("Interfaces.Pdm_57")] InSelectionProd = 1,
  /// <summary>Нет опций</summary>
  [CustomDescription("Interfaces.Pdm_58")] ObjectGrouping = 2,
  /// <summary>Нет опций</summary>
  [CustomDescription("Interfaces.Pdm_80")] ActualSubstitutesOnly = 4,
}
