// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.PdmConfigurator.CodePartType
// Assembly: Intermech.Interfaces.PdmConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 6A3EF664-00FF-4A8A-A8E2-24964457B937
// Assembly location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.xml

using Intermech.Localization;

#nullable disable
namespace Intermech.Interfaces.PdmConfigurator;

/// <summary>тип части шифра</summary>
public enum CodePartType
{
  /// <summary>Не задан</summary>
  Undefined = -1, // 0xFFFFFFFF
  /// <summary>Атрибут объекта</summary>
  [CustomDescription("Attribute.Interfaces.PdmConfigurator_18")] ObjectAttribute = 0,
  /// <summary>Код значения опции</summary>
  [CustomDescription("Attribute.Interfaces.PdmConfigurator_19")] OptionValueCode = 1,
  /// <summary>Код опции</summary>
  [CustomDescription("Attribute.Interfaces.PdmConfigurator_20")] OptionCode = 2,
  /// <summary>Фиксированный текст</summary>
  [CustomDescription("Attribute.Interfaces.PdmConfigurator_21")] FixedText = 3,
}
