// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Table.eCellDestination
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Localization;
using System.ComponentModel;

#nullable disable
namespace Intermech.Expert.Table;

/// <summary>Назначение ячейки</summary>
[TypeConverter(typeof (EnumDescConverter))]
public enum eCellDestination
{
  /// <summary>
  /// Ячейка с данными,
  /// содержит данные и ссылки, без eCellSymbol
  /// </summary>
  [CustomDescription("Attribute.Expert_1")] Data,
  /// <summary>
  /// Ячейка-заголовок,
  /// содержит заголовок атрибута, с eCellSymbol
  /// </summary>
  [CustomDescription("Attribute.Expert_2")] Header,
  /// <summary>
  /// Ячейка-заголовок с данными,
  /// содержит данные и ссылки, с eCellSymbol
  /// </summary>
  [CustomDescription("Attribute.Expert_3")] HeaderData,
  /// <summary>
  /// Ячейка-результат,
  /// заголовок атрибута, с eCellSymbol
  /// </summary>
  [CustomDescription("Attribute.Expert_4")] Result,
}
