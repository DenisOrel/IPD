// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.HeaderShowType
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using Intermech.ComponentModel;
using Intermech.Localization;
using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Тип отображения заголовка</summary>
[TypeConverter(typeof (EnumCustomConverter))]
public enum HeaderShowType
{
  /// <summary>Показывать всегда</summary>
  [CustomDescription("Attribute.Interfaces.Document_476")] All,
  /// <summary>Показывать только в первой таблице</summary>
  [CustomDescription("Attribute.Interfaces.Document_477")] FirstOnly,
  /// <summary>Показывать только в следующей таблице</summary>
  [CustomDescription("Attribute.Interfaces.Document_478")] NextOnly,
}
