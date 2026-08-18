// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.ImageScaleMode
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

/// <summary>Режим масштабирования изображения и автоподбора размера контейнера</summary>
[TypeConverter(typeof (EnumCustomConverter))]
[Serializable]
public enum ImageScaleMode
{
  /// <summary>Масштаб 1:1, обрезать лишнее</summary>
  [CustomDescription("Attribute.Interfaces.Document_93")] OriginalClip,
  /// <summary>Масштаб 1:1, размер контейнера по изображению</summary>
  [CustomDescription("Attribute.Interfaces.Document_94")] OriginalAutoSize,
  /// <summary>Вписать по ширине и высоте</summary>
  [CustomDescription("Attribute.Interfaces.Document_95")] FitWidthHeight,
}
