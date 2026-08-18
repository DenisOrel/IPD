// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.TextOrientation
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using Intermech.ComponentModel;
using Intermech.Localization;
using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Ориентация текста</summary>
[TypeConverter(typeof (EnumCustomConverter))]
public enum TextOrientation
{
  /// <summary>Обычный текст без поворотов, слева-направо (0 градусов)</summary>
  [CustomDescription("Attribute.Interfaces.Document_505")] Normal = 0,
  /// <summary>Вертикальный текст, снизу вверх (90 градусов)</summary>
  [CustomDescription("Attribute.Interfaces.Document_506")] DownTop = 90, // 0x0000005A
  /// <summary>Перевернутый горизонтальный текст (180 градусов)</summary>
  [CustomDescription("Attribute.Interfaces.Document_507")] UpsideDown = 180, // 0x000000B4
  /// <summary>Вертикальный текст, сверху вниз (270 градусов)</summary>
  [CustomDescription("Attribute.Interfaces.Document_508")] TopDown = 270, // 0x0000010E
}
