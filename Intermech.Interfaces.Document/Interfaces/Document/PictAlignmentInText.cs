// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.PictAlignmentInText
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

/// <summary>Вертикальное выравнивание</summary>
[TypeConverter(typeof (EnumCustomConverter))]
[Serializable]
public enum PictAlignmentInText
{
  /// <summary>Снизу</summary>
  [CustomDescription("Attribute.Interfaces.Document_261")] Bottom,
  /// <summary>По центру</summary>
  [CustomDescription("Attribute.Interfaces.Document_260")] Center,
  /// <summary>Сверху</summary>
  [CustomDescription("Attribute.Interfaces.Document_259")] Top,
  /// <summary>По базовой линии текста</summary>
  [CustomDescription("Attribute.Interfaces.Document_631")] CustomBaseLine,
}
