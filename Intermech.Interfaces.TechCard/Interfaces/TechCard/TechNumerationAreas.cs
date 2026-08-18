// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.TechCard.TechNumerationAreas
// Assembly: Intermech.Interfaces.TechCard, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B0F892EF-B72A-4A7D-8F43-9EB461AAC859
// Assembly location: D:\IPS\Client\Intermech.Interfaces.TechCard.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.TechCard.xml

using Intermech.Localization;
using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.TechCard;

/// <summary>Области нумерации</summary>
[TypeConverter(typeof (EnumDescConverter))]
public enum TechNumerationAreas
{
  /// <summary>Нумерация внутри родительского объекта</summary>
  [CustomDescription("Attribute.Interfaces.TechCard_6")] Parent,
  /// <summary>Нумерация внутри вида производства</summary>
  [CustomDescription("Attribute.Interfaces.TechCard_7")] Production,
  /// <summary>Нумерация внутри техпроцесса</summary>
  [CustomDescription("Attribute.Interfaces.TechCard_8")] TechProccess,
  /// <summary>Нумерация внутри всей базы данных</summary>
  [CustomDescription("Attribute.Interfaces.TechCard_9")] Global,
}
