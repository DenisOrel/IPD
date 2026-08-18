// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.TechCard.TechNumerationMode
// Assembly: Intermech.Interfaces.TechCard, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B0F892EF-B72A-4A7D-8F43-9EB461AAC859
// Assembly location: D:\IPS\Client\Intermech.Interfaces.TechCard.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.TechCard.xml

using Intermech.Localization;
using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.TechCard;

/// <summary>Режим нумерации</summary>
[TypeConverter(typeof (EnumDescConverter))]
public enum TechNumerationMode
{
  /// <summary>Не задан</summary>
  [CustomDescription("Attribute.Interfaces.TechCard_22"), Browsable(false)] None,
  /// <summary>Нумерация атрибутов объектов</summary>
  [CustomDescription("Attribute.Interfaces.TechCard_19")] Object,
  /// <summary>Нумерация атрибутов связей</summary>
  [CustomDescription("Attribute.Interfaces.TechCard_20")] Relation,
  /// <summary>Нумерация атрибутов объектов и связей</summary>
  [CustomDescription("Attribute.Interfaces.TechCard_21")] ObjectAndRelation,
}
