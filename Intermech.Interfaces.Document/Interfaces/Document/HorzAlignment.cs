// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.HorzAlignment
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

/// <summary>Горизонтальное выравнивание</summary>
[TypeConverter(typeof (EnumCustomConverter))]
[Serializable]
public enum HorzAlignment
{
  /// <summary>Слева</summary>
  [CustomDescription("Attribute.Interfaces.Document_255")] Left,
  /// <summary>По центру</summary>
  [CustomDescription("Attribute.Interfaces.Document_256")] Center,
  /// <summary>Справа</summary>
  [CustomDescription("Attribute.Interfaces.Document_257")] Right,
  /// <summary>По ширине</summary>
  [CustomDescription("Attribute.Interfaces.Document_258")] Justify,
}
