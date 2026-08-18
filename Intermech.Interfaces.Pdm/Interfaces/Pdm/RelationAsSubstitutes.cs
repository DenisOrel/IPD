// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.RelationAsSubstitutes
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using Intermech.Localization;
using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.Pdm;

/// <summary>Связь как допустимый заменитель</summary>
[TypeConverter(typeof (EnumDescConverter))]
[CustomDescription("Attribute.Interfaces.Pdm_10")]
[Category("Misc")]
public enum RelationAsSubstitutes
{
  /// <summary>Связь не участвует в допустимых заменах</summary>
  [CustomDescription("Attribute.Interfaces.Pdm_11")] rsNoSubstitutes,
  /// <summary>Связь является актуальным заменителем</summary>
  [CustomDescription("Attribute.Interfaces.Pdm_12")] rsActualSubstitute,
  /// <summary>Связь является допустимым заменителем</summary>
  [CustomDescription("Attribute.Interfaces.Pdm_13")] rsSubstitute,
}
