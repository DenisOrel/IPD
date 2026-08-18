
// Type: Intermech.Navigator.Queries.BeforeClientRecordsSelectEventArgs
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using System;


namespace Intermech.Navigator.Queries;

/// <summary>Аргументы события BeforeClientRecordsSelectHandler</summary>
public class BeforeClientRecordsSelectEventArgs : EventArgs
{
  /// <summary>Параметры, переданные в запросе GetDataTable</summary>
  public DBRecordSetParams OldParameters;
  /// <summary>
  /// Измененные параметры запроса (присваиваются обработчиками события)
  /// </summary>
  public DBRecordSetParams? NewParameters;
  /// <summary>Сессия, в рамках которой выполняется запрос</summary>
  public IUserSession Session;
  /// <summary>
  /// Контейнер сервисов, в рамках которых выполняется запрос
  /// </summary>
  public IServiceProvider Services;

  /// <summary>
  /// Создать аргументы события BeforeClientRecordsSelectEventArgs
  /// </summary>
  /// <param name="parameters">Параметры, переданные в запросе GetDataTable</param>
  /// <param name="session">Сессия, в рамках которой выполняется запрос</param>
  /// <param name="services">Контейнер сервисов, в рамках которых выполняется запрос</param>
  public BeforeClientRecordsSelectEventArgs(
    DBRecordSetParams parameters,
    IUserSession session,
    IServiceProvider services)
  {
    this.OldParameters = parameters;
    this.Session = session;
    this.Services = services;
  }
}
