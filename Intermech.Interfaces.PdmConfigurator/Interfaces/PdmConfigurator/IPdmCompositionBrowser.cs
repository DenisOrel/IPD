// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.PdmConfigurator.IPdmCompositionBrowser
// Assembly: Intermech.Interfaces.PdmConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 6A3EF664-00FF-4A8A-A8E2-24964457B937
// Assembly location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.xml

using Intermech.Interfaces.Compositions;
using System;

#nullable disable
namespace Intermech.Interfaces.PdmConfigurator;

/// <summary>
/// Интерфейс для получения развёрнутого сконфигурированного состава
/// </summary>
public interface IPdmCompositionBrowser
{
  /// <summary>
  /// Уникальный идентификатор анализатора (по данному идентификатору происходит регистрация и
  /// удаление анализатора в службе IPdmConfiguratorService)
  /// </summary>
  Guid Guid { get; }

  /// <summary>
  /// Выполнить раскрутку состава рекурсивно вниз по всем видимым конфигурируемым типам связей,
  /// собрать протокол подбора объектов конфигуратором составов
  /// </summary>
  /// <param name="session">Сессия, в рамках которой выполняется анализ</param>
  /// <param name="rootObject">Информация о корневом объекте конфигурируемого состава</param>
  /// <param name="rootObjectPath">Относительный путь от корневого объекта к обрабатываемым объектам</param>
  /// <param name="optionObjects">Объекты анализируемых составов</param>
  /// <param name="args">Аргументы для вызова службы</param>
  /// <returns>Протокол подбора объектов конфигуратором составов</returns>
  TraceLog Browse(
    IUserSession session,
    RelationPair rootObject,
    RelationPath rootObjectPath,
    CompositionObjects optionObjects,
    PdmCompositionBrowserEventArgs args);
}
