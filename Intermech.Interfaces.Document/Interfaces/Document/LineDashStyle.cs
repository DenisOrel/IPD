// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.LineDashStyle
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using Intermech.ComponentModel;
using Intermech.Localization;
using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.Document;

[TypeConverter(typeof (EnumCustomConverter))]
public enum LineDashStyle
{
  /// <summary>Сплошная</summary>
  [CustomDescription("Attribute.Interfaces.Document_20")] SolidLine,
  /// <summary>Штриховая</summary>
  [CustomDescription("Attribute.Interfaces.Document_21")] Dash,
  /// <summary>Пунктирная</summary>
  [CustomDescription("Attribute.Interfaces.Document_24")] Dot,
  /// <summary>Штрих-пунктирная</summary>
  [CustomDescription("Attribute.Interfaces.Document_22")] DashDot,
  /// <summary>Штрих-точка-точка</summary>
  [CustomDescription("Attribute.Interfaces.Document_23")] DashDotDot,
}
