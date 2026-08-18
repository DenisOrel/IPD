// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Table.eCellSymbol
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Localization;
using System.ComponentModel;

#nullable disable
namespace Intermech.Expert.Table;

/// <summary>Символ-условие для ячейки</summary>
[TypeConverter(typeof (EnumDescConverter))]
public enum eCellSymbol
{
  /// <summary>Нет символа</summary>
  [Symbol("Attribute.Expert_7"), CustomDescription("Attribute.Expert_8")] None,
  /// <summary>Любое значение</summary>
  [Symbol("*"), CustomDescription("Attribute.Expert_9")] Other,
  /// <summary>Набор значений</summary>
  [Symbol("{}"), CustomDescription("Attribute.Expert_10")] Set,
  /// <summary>Равно</summary>
  [Symbol("="), CustomDescription("Attribute.Expert_11")] Equal,
  /// <summary>Не равно</summary>
  [Symbol("!="), CustomDescription("Attribute.Expert_12")] NotEqual,
  /// <summary>Больше</summary>
  [Symbol(">"), CustomDescription("Attribute.Expert_13")] More,
  /// <summary>Больше или равно</summary>
  [Symbol(">="), CustomDescription("Attribute.Expert_14")] MoreOrEqual,
  /// <summary>Меньше</summary>
  [Symbol("<"), CustomDescription("Attribute.Expert_15")] Less,
  /// <summary>Меньше или равно</summary>
  [Symbol("<="), CustomDescription("Attribute.Expert_16")] LessOrEqual,
}
