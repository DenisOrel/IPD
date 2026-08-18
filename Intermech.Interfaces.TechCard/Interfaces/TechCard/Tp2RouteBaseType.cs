// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.TechCard.Tp2RouteBaseType
// Assembly: Intermech.Interfaces.TechCard, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B0F892EF-B72A-4A7D-8F43-9EB461AAC859
// Assembly location: D:\IPS\Client\Intermech.Interfaces.TechCard.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.TechCard.xml

using Intermech.Localization;
using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.TechCard;

/// <summary>Типы отношений ТП к расцеховке</summary>
[TypeConverter(typeof (EnumDescConverter))]
public enum Tp2RouteBaseType
{
  /// <summary>Основной ТП для расцеховки</summary>
  [CustomDescription("Attribute.Interfaces.TechCard_10")] Main,
  /// <summary>ТП-Вариант для расцеховки</summary>
  [CustomDescription("Attribute.Interfaces.TechCard_11")] Variant,
}
