// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.StrikeoutLineStyle
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

/// <summary> Стиль линии перечеркивания </summary>
[TypeConverter(typeof (EnumCustomConverter))]
[Flags]
public enum StrikeoutLineStyle
{
  /// <summary> Нет перечёркивания </summary>
  [CustomDescription("Attribute.Interfaces.Document_601")] None = 0,
  /// <summary> Перечёркивание </summary>
  [CustomDescription("Attribute.Interfaces.Document_602")] SingleLine = 8,
  /// <summary> Двойное перечёркивание </summary>
  [CustomDescription("Attribute.Interfaces.Document_603")] DoubleLine = 524288, // 0x00080000
  /// <summary>Все</summary>
  [CustomDescription("Attribute.Interfaces.Document_604"), Browsable(false)] All = DoubleLine | SingleLine, // 0x00080008
}
