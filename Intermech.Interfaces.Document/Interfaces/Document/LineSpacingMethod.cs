// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.LineSpacingMethod
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using Intermech.ComponentModel;
using Intermech.Localization;
using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary> Способ задания междустрочного пространства </summary>
[TypeConverter(typeof (EnumCustomConverter))]
[Serializable]
public enum LineSpacingMethod
{
  /// <summary>В процентах от нормального междустрочного интервала</summary>
  [CustomDescription("Attribute.Interfaces.Document_246"), Browsable(false)] InPercents,
  /// <summary>Одинарный</summary>
  [CustomDescription("Attribute.Interfaces.Document_247")] Ratio_1,
  /// <summary>Полуторный</summary>
  [CustomDescription("Attribute.Interfaces.Document_248")] Ratio_1_5,
  /// <summary>Двойной</summary>
  [CustomDescription("Attribute.Interfaces.Document_249")] Ratio_2,
  /// <summary>Минимум</summary>
  [CustomDescription("Attribute.Interfaces.Document_250")] AtLeast,
  /// <summary>Минимум</summary>
  [CustomDescription("Attribute.Interfaces.Document_251")] AtLeastMM,
  /// <summary>Точно, точек</summary>
  [CustomDescription("Attribute.Interfaces.Document_252")] Exact,
  /// <summary>Точно, мм</summary>
  [CustomDescription("Attribute.Interfaces.Document_253")] ExactMM,
  /// <summary>Множитель</summary>
  [CustomDescription("Attribute.Interfaces.Document_254")] Ratio,
}
