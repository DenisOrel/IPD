// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.AttributeRoles
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Localization;
using System.ComponentModel;

#nullable disable
namespace Intermech.Expert;

/// <summary>Роли атрибутов в таблице</summary>
[TypeConverter(typeof (EnumDescConverter))]
public enum AttributeRoles
{
  /// <summary>Аргумент по вертикали</summary>
  [CustomDescription("Attribute.Expert_20")] argVert,
  /// <summary>Аргумент по горизонтали</summary>
  [CustomDescription("Attribute.Expert_21")] argHorz,
  /// <summary>Аргумент-результат</summary>
  [CustomDescription("Attribute.Expert_22")] argResult,
  /// <summary>Результат</summary>
  [CustomDescription("Attribute.Expert_23")] Result,
}
