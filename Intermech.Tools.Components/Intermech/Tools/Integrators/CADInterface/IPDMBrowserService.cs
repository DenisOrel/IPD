// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.IPDMBrowserService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>Сервис поддержки PDM-браузера в CAD-системе.</summary>
public interface IPDMBrowserService : IIntegratorService
{
  /// <summary>Возвращает глобальный идентификатор CAD-системы.</summary>
  Guid CADSystemId { get; }

  /// <summary>
  /// Определяет, могут ли конструкторские документы указанного типа служить источником информации о зонах для спецификации.
  /// </summary>
  /// <param name="documentType">Идентификатор типа документа</param>
  /// <returns>true - документ может содержать информацию о зонах для спецификации, false - документ не может содержать информацию о зонах</returns>
  /// <exception cref="T:ArgumentException">Параметр <param name="documentType" /> не задан</exception>
  bool CanProvideSpecificationZones(int documentType);

  /// <summary>
  /// Создает стратегию для переоткрытия в CAD-системе открытых файлов документов, подлежащих обновлению из базы данных IPS.
  /// Используется командной PDM-браузера "Синхронизировать".
  /// </summary>
  /// <returns>Объект стратегии</returns>
  ISynchronizeActionReloadStrategy CreateSynchronizeActionReloadStrategy();
}
