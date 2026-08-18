// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.ElementHorizontalAlign
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
public enum ElementHorizontalAlign
{
  /// <summary>Нет</summary>
  [CustomDescription("Attribute.Interfaces.Document_517")] None,
  /// <summary>Влево</summary>
  [CustomDescription("Attribute.Interfaces.Document_518")] Left,
  /// <summary>Вправо</summary>
  [CustomDescription("Attribute.Interfaces.Document_519")] Right,
  /// <summary>По центру</summary>
  [CustomDescription("Attribute.Interfaces.Document_540")] Center,
}
