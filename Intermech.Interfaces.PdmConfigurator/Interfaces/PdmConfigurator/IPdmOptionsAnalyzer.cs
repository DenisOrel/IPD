// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.PdmConfigurator.IPdmOptionsAnalyzer
// Assembly: Intermech.Interfaces.PdmConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 6A3EF664-00FF-4A8A-A8E2-24964457B937
// Assembly location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.PdmConfigurator;

/// <summary>Анализатор объектов с опциями</summary>
public interface IPdmOptionsAnalyzer
{
  /// <summary>
  /// Уникальный идентификатор анализатора (по данному идентификатору происходит регистрация и
  /// удаление анализатора в службе IPdmConfiguratorService)
  /// </summary>
  Guid Guid { get; }

  /// <summary>
  /// Выполнить анализ опций объектов, при необходимости добавить в граф дополнительные идентификаторы
  /// версий объектов-опций
  /// </summary>
  /// <param name="session">Сессия, в рамках которой выполняется анализ</param>
  /// <param name="optionObjects">Изучаемые объекты с опциями</param>
  /// <param name="options">Параметры</param>
  /// <returns>Количество добавленных объектов-опций</returns>
  int Analyze(
    IUserSession session,
    PdmAnalyzedOptionObjects optionObjects,
    PdmAnalyzerFlags options);

  /// <summary>
  /// Выполнить анализ опций объектов, при необходимости добавить в граф дополнительные идентификаторы
  /// версий объектов-опций
  /// </summary>
  /// <param name="session">Сессия, в рамках которой выполняется анализ</param>
  /// <param name="optionObjects">Изучаемые объекты с опциями</param>
  /// <param name="options">Параметры</param>
  /// <param name="excludedObjects">Список идентификаторов версий объектов, которые должны быть проигнорированы анализатором</param>
  /// <param name="excludedOptions">Список идентификаторов версий опций, которые должны быть проигнорированы анализатором</param>
  /// <returns>Количество добавленных объектов-опций</returns>
  int Analyze(
    IUserSession session,
    PdmAnalyzedOptionObjects optionObjects,
    PdmAnalyzerFlags options,
    IList<long> excludedObjects,
    IList<long> excludedOptions);
}
