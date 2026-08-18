// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.PdmConfigurator.Operator
// Assembly: Intermech.Interfaces.PdmConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 6A3EF664-00FF-4A8A-A8E2-24964457B937
// Assembly location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.xml

using Intermech.Localization;

#nullable disable
namespace Intermech.Interfaces.PdmConfigurator;

/// <summary>
/// Оператор для сравнения значений опции конфигуратора составов IPS
/// </summary>
public enum Operator
{
  /// <summary>Не задан</summary>
  [CustomDescription("Attribute.Interfaces.PdmConfigurator_1")] Undefined,
  /// <summary>Меньше</summary>
  [CustomDescription("Attribute.Interfaces.PdmConfigurator_2")] Less,
  /// <summary>Меньше либо равно</summary>
  [CustomDescription("Attribute.Interfaces.PdmConfigurator_3")] LessEquals,
  /// <summary>Равно</summary>
  [CustomDescription("Attribute.Interfaces.PdmConfigurator_4")] Equals,
  /// <summary>Больше либо равно</summary>
  [CustomDescription("Attribute.Interfaces.PdmConfigurator_5")] GreaterEquals,
  /// <summary>Больше</summary>
  [CustomDescription("Attribute.Interfaces.PdmConfigurator_6")] Greater,
  /// <summary>Не равно</summary>
  [CustomDescription("Attribute.Interfaces.PdmConfigurator_7")] NotEquals,
}
