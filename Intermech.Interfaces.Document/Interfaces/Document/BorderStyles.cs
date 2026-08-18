// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.BorderStyles
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

/// <summary>Перечисление перечисление стилей линии</summary>
[TypeConverter(typeof (EnumCustomConverter))]
[Serializable]
public enum BorderStyles
{
  /// <summary>Нет линии</summary>
  [CustomDescription("Attribute.Interfaces.Document_19")] None,
  /// <summary>Сплошная</summary>
  [CustomDescription("Attribute.Interfaces.Document_20")] SolidLine,
  /// <summary>Штриховая</summary>
  [CustomDescription("Attribute.Interfaces.Document_21")] Dash,
  /// <summary>Штрих-пунктирная</summary>
  [CustomDescription("Attribute.Interfaces.Document_22")] DashDot,
  /// <summary>Штрих-точка-точка</summary>
  [CustomDescription("Attribute.Interfaces.Document_23")] DashDotDot,
  /// <summary>Пунктирная</summary>
  [CustomDescription("Attribute.Interfaces.Document_24")] Dot,
  /// <summary>Один штрих в начале</summary>
  [CustomDescription("Attribute.Interfaces.Document_25")] Serif,
}
