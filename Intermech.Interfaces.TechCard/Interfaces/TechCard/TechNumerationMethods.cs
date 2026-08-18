// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.TechCard.TechNumerationMethods
// Assembly: Intermech.Interfaces.TechCard, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B0F892EF-B72A-4A7D-8F43-9EB461AAC859
// Assembly location: D:\IPS\Client\Intermech.Interfaces.TechCard.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.TechCard.xml

using Intermech.Localization;
using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.TechCard;

/// <summary>Методы нумерации</summary>
[TypeConverter(typeof (EnumDescConverter))]
public enum TechNumerationMethods
{
  /// <summary>Автоматический метод нумерации</summary>
  [CustomDescription("Attribute.Interfaces.TechCard_1")] Auto,
  /// <summary>Ручной метод нумерации</summary>
  [CustomDescription("Attribute.Interfaces.TechCard_2")] Manual,
}
