// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.PageCoorSystem
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

/// <summary>Система координат страницы</summary>
[TypeConverter(typeof (EnumCustomConverter))]
[Serializable]
public enum PageCoorSystem
{
  /// <summary>Нижний левый угол</summary>
  [CustomDescription("Attribute.Interfaces.Document_184")] BottomLeft,
  /// <summary>Верхний левый угол</summary>
  [CustomDescription("Attribute.Interfaces.Document_185")] TopLeft,
  /// <summary>Верхний правый угол</summary>
  [CustomDescription("Attribute.Interfaces.Document_186")] TopRight,
  /// <summary>Нижний правый угол</summary>
  [CustomDescription("Attribute.Interfaces.Document_187")] BottomRight,
  /// <summary>Пользовательская</summary>
  [CustomDescription("Attribute.Interfaces.Document_188")] Custom,
}
