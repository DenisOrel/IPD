// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.ICADSettingsService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.CADInterface.Proxies;
using Intermech.Interfaces;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>
/// Сервис интегратора для получения проекции настроек интегратора, представляющей общую часть настроек всех интеграторов на основе CAD-интерфейса.
/// </summary>
public interface ICADSettingsService : 
  IIntegratorSettingsService,
  IIntegratorService,
  IDocumentAttributesSettingsService,
  IArticleAttributesSettingsService
{
  /// <summary>
  /// Возвращает проекцию настроек интегратора, представляющую общую часть настроек всех интеграторов на основе CAD-интерфейса.
  /// При первом вызове этого метода выполняется кэширование настроек интегратора. Кэш автоматически сбрасывается при их изменении в базе.
  /// </summary>
  /// <returns>Общая часть настроек интегратора</returns>
  /// <exception cref="T:System.Exception">Объект с настройками еще не создан в базе IPS, либо настройки интегратора содержат ошибки</exception>
  CADSettings GetCADSettings();

  /// <summary>
  /// Возвращает список типов файловых документов, которые пользователь может создавать в CAD-системе.
  /// </summary>
  /// <returns>Список типов файловых документов</returns>
  List<LocalId<int>> GetNewFileDocumentTypes();

  /// <summary>
  /// Отображает тип документа IPS в тип документа CAD-системы.
  /// </summary>
  /// <param name="documentType">Идентификатор типа документа IPS</param>
  /// <returns>Идентификатор типа документа CAD-системы или null</returns>
  CADDocumentType? MapDocumentTypeToCADDocumentType(int documentType);
}
