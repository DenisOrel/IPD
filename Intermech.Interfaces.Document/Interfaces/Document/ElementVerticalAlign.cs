// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.ElementVerticalAlign
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

/// <summary>Выравнивание элементов страницы</summary>
[TypeConverter(typeof (EnumCustomConverter))]
[Serializable]
public enum ElementVerticalAlign
{
  /// <summary>Нет</summary>
  [CustomDescription("Attribute.Interfaces.Document_517")] None,
  /// <summary>Вверх</summary>
  [CustomDescription("Attribute.Interfaces.Document_541")] Top,
  /// <summary>По центру</summary>
  [CustomDescription("Attribute.Interfaces.Document_540")] Center,
  /// <summary>Вниз</summary>
  [CustomDescription("Attribute.Interfaces.Document_542")] Bottom,
}
