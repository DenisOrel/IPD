// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Table.eCellType
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Localization;
using System.ComponentModel;

#nullable disable
namespace Intermech.Expert.Table;

/// <summary>Тип ячейки</summary>
[TypeConverter(typeof (EnumDescConverter))]
public enum eCellType
{
  /// <summary>Значение</summary>
  [CustomDescription("Attribute.Expert_5")] Value,
  /// <summary>Текст</summary>
  [CustomDescription("Attribute.Expert_6")] Text,
}
