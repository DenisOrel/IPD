// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.TechCard.TechNumerationTypes
// Assembly: Intermech.Interfaces.TechCard, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B0F892EF-B72A-4A7D-8F43-9EB461AAC859
// Assembly location: D:\IPS\Client\Intermech.Interfaces.TechCard.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.TechCard.xml

using Intermech.Localization;
using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.TechCard;

/// <summary>Типы нумерации</summary>
[TypeConverter(typeof (EnumDescConverter))]
public enum TechNumerationTypes
{
  /// <summary>Нумерация цифрами</summary>
  [CustomDescription("Attribute.Interfaces.TechCard_3")] Number,
  /// <summary>Нумерация буквами</summary>
  [CustomDescription("Attribute.Interfaces.TechCard_4")] Literal,
  /// <summary>Не нумеровать</summary>
  [CustomDescription("Attribute.Interfaces.TechCard_5")] None,
}
