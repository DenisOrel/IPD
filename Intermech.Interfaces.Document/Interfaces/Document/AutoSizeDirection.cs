// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.AutoSizeDirection
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

/// <summary>Направление авторазмера</summary>
[TypeConverter(typeof (EnumCustomConverter))]
[Serializable]
public enum AutoSizeDirection
{
  /// <summary>Нет</summary>
  [CustomDescription("Attribute.Document.Model_240")] None,
  /// <summary>Высота</summary>
  [CustomDescription("Attribute.Document.Model_241")] Height,
  /// <summary>Ширина</summary>
  [CustomDescription("Attribute.Document.Model_242")] Width,
}
