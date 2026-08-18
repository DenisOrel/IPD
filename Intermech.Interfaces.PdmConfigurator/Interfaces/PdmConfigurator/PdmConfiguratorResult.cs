// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.PdmConfigurator.PdmConfiguratorResult
// Assembly: Intermech.Interfaces.PdmConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 6A3EF664-00FF-4A8A-A8E2-24964457B937
// Assembly location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.xml

using System;

#nullable disable
namespace Intermech.Interfaces.PdmConfigurator;

/// <summary>Результат вычисления какого-то оператора</summary>
[Serializable]
public enum PdmConfiguratorResult
{
  /// <summary>Вычисление не было выполнено</summary>
  Unknown = -1, // 0xFFFFFFFF
  /// <summary>"Ложь"</summary>
  False = 0,
  /// <summary>Истина</summary>
  True = 1,
  /// <summary>
  /// Во время вычисления произошла ошибка - не найдены значения опций в текущем составе
  /// </summary>
  ContextNotFound = 2,
  /// <summary>
  /// Во время вычисления произошла ошибка - не найдена опция
  /// </summary>
  OptionNotFound = 3,
  /// <summary>
  /// Во время вычисления произошла ошибка - не найдено значение опции
  /// </summary>
  OptionValueNotFound = 4,
  /// <summary>
  /// Во время вычисления произошла ошибка - не найдена опция, с которой заданы условия несовместимости
  /// </summary>
  ConflictOptionNotFound = 5,
  /// <summary>
  /// Во время вычисления произошла ошибка - не найдено значение опции, с которой заданы условия несовместимости
  /// </summary>
  ConflictOptionValueNotFound = 6,
  /// <summary>
  /// Во время вычисления произошла ошибка - не найдена опция из условий применения объекта
  /// </summary>
  ApplOptionNotFound = 7,
  /// <summary>
  /// Во время вычисления произошла ошибка - не найдено значение опции из условий применения объекта
  /// </summary>
  ApplOptionValueNotFound = 8,
  /// <summary>Возникли условия несовместимости значений опции</summary>
  Incompatibles = 9,
  /// <summary>Возникла исключительная ситуация</summary>
  Exception = 10, // 0x0000000A
}
