// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.PdmConfigurator.PdmConfiguratorStates
// Assembly: Intermech.Interfaces.PdmConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 6A3EF664-00FF-4A8A-A8E2-24964457B937
// Assembly location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.xml

using Intermech.Localization;
using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.PdmConfigurator;

/// <summary>
/// Состояние элемента, обработанного конфигуратором составов
/// </summary>
[TypeConverter(typeof (EnumDescConverter))]
[CustomDescription("Attribute.Interfaces.PdmConfigurator_8")]
[Category("Misc")]
[Serializable]
public enum PdmConfiguratorStates
{
  /// <summary>Фильтрация не потребовалась</summary>
  [Description("")] None,
  /// <summary>Объект подобран по условиям применения</summary>
  [CustomDescription("Attribute.Interfaces.PdmConfigurator_9")] Configured,
  /// <summary>Не найдены значения опций в текущем составе</summary>
  [CustomDescription("Attribute.Interfaces.PdmConfigurator_10")] ContextNotFound,
  /// <summary>Не найдена опция конфигуратора составов</summary>
  [CustomDescription("Attribute.Interfaces.PdmConfigurator_11")] OptionNotFound,
  /// <summary>Не найдено значение опции</summary>
  [CustomDescription("Attribute.Interfaces.PdmConfigurator_12")] OptionValueNotFound,
  /// <summary>
  /// Во время вычисления произошла ошибка - не найдена опция, с которой заданы условия несовместимости
  /// </summary>
  [CustomDescription("Attribute.Interfaces.PdmConfigurator_13")] ConflictOptionNotFound,
  /// <summary>
  /// Во время вычисления произошла ошибка - не найдено значение опции, с которой заданы условия несовместимости
  /// </summary>
  [CustomDescription("Attribute.Interfaces.PdmConfigurator_14")] ConflictOptionValueNotFound,
  /// <summary>
  /// Во время вычисления произошла ошибка - не найдено значение опции из условий применения объекта
  /// </summary>
  [CustomDescription("Attribute.Interfaces.PdmConfigurator_15")] ApplOptionNotFound,
  /// <summary>
  /// Во время вычисления произошла ошибка - не найдено значение опции из условий применения объекта
  /// </summary>
  [CustomDescription("Attribute.Interfaces.PdmConfigurator_15")] ApplOptionValueNotFound,
  /// <summary>Возникли условия несовместимости значений опции</summary>
  [CustomDescription("Attribute.Interfaces.PdmConfigurator_16")] Incompatibles,
  /// <summary>Возникла исключительная ситуация</summary>
  [CustomDescription("Attribute.Interfaces.PdmConfigurator_17")] Exception,
}
