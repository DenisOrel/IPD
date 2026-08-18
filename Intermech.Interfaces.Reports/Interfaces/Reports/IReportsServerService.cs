// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Reports.IReportsServerService
// Assembly: Intermech.Interfaces.Reports, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3A40A7D8-A018-4590-B8F9-C63911182943
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Reports.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Reports.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Reports;

/// <summary>
/// Интерфейс серверной службы генерации комплектов документов
/// </summary>
public interface IReportsServerService
{
  /// <summary>Загрузка содержимого комплекта</summary>
  /// <remarks>Для ускорения загрузки и экономии трафика - обработка на сервере</remarks>
  /// <param name="objectId">Ид. версии объекта</param>
  /// <param name="complect">Содержимое комплекта</param>
  /// <param name="sessionGuid">Ид. сессии</param>
  /// <param name="loadMode">Режим загрузки данных</param>
  /// <returns>Результат</returns>
  [Obsolete("Use LoadComplectData instead. Will be removed in IPS 7", false)]
  bool LoadCompectData(
    long objectId,
    out ReportsDocComplect complect,
    Guid sessionGuid,
    ReportsDocModes loadMode);

  /// <summary>Загрузка содержимого комплекта</summary>
  /// <remarks>Для ускорения загрузки и экономии трафика - обработка на сервере</remarks>
  /// <param name="objectId">Ид. версии объекта</param>
  /// <param name="complect">Содержимое комплекта</param>
  /// <param name="sessionGuid">Ид. сессии</param>
  /// <param name="loadMode">Режим загрузки данных</param>
  /// <returns>Результат</returns>
  bool LoadComplectData(
    long objectId,
    out ReportsDocComplect complect,
    Guid sessionGuid,
    ReportsDocModes loadMode);
}
