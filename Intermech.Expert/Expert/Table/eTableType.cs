// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Table.eTableType
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Localization;
using System.ComponentModel;

#nullable disable
namespace Intermech.Expert.Table;

/// <summary>Типы таблиц</summary>
[TypeConverter(typeof (EnumDescConverter))]
public enum eTableType
{
  /// <summary>Без входов</summary>
  [CustomDescription("Attribute.Expert_17")] NoEntry,
  /// <summary>Один вход</summary>
  [CustomDescription("Attribute.Expert_18")] SingleEntry,
  /// <summary>Два входа</summary>
  [CustomDescription("Attribute.Expert_19")] DoubleEntry,
}
