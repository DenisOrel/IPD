// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.ContainerHorzAlignment
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

/// <summary>Горизонтальное выравнивание изображения</summary>
[TypeConverter(typeof (EnumCustomConverter))]
[Serializable]
public enum ContainerHorzAlignment
{
  /// <summary>Слева</summary>
  [CustomDescription("Attr ibute.Interfaces.Document_255")] Left = 1,
  /// <summary>По центру</summary>
  [CustomDescription("Attribute.Interfaces.Document_256")] Center = 2,
  /// <summary>Справа</summary>
  [CustomDescription("Attribute.Interfaces.Document_257")] Right = 3,
}
