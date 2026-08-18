// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.CellType
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using Intermech.ComponentModel;
using Intermech.Localization;
using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Тип ячейки</summary>
[TypeConverter(typeof (EnumCustomConverter))]
public enum CellType
{
  /// <summary>Ячейка данных родительской таблицы</summary>
  [CustomDescription("Attribute.Interfaces.Document_479")] DataCell,
  /// <summary>Заголовок таблицы</summary>
  [CustomDescription("Attribute.Interfaces.Document_480")] Header,
}
