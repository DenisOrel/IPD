// Decompiled with JetBrains decompiler
// Type: Intermech.Statistics.Interfaces.IWorkflowStatisticService
// Assembly: Intermech.Statistics.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BE126060-F77F-4F0A-893B-FA8B66A88C31
// Assembly location: D:\IPS\Client\Intermech.Statistics.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.Statistics.Interfaces.xml

using System;
using System.Data;

#nullable disable
namespace Intermech.Statistics.Interfaces;

/// <summary>
/// Серверная служба содержит методы для получения статистики по процессам workflow
/// </summary>
public interface IWorkflowStatisticService
{
  /// <summary>
  /// Метод собирает статистику о времени выполнения процессов Workflow
  /// </summary>
  /// <param name="sessionGuid">Гуид сессии пользователя</param>
  /// <param name="templatesID">Массив ObjectID шаблонов, процессы по которым нужно проанализировать</param>
  /// <param name="beginDate">Начальная дата сбора статистики</param>
  /// <param name="endDate">Конечная дата сбора статистики</param>
  /// <param name="period">Период анализа данных</param>
  /// <returns>Таблица с результатами сбора данных (колонки - процессы по указанным шаблонам, строки - среднее время выполнения процессов с датой завершения в указанном интервале дат)</returns>
  DataTable GetProcessesRuntime(
    Guid sessionGuid,
    long[] templatesID,
    DateTime beginDate,
    DateTime endDate,
    CollectPeriodsEnum period);

  /// <summary>
  /// Метод собирает статистику о времени выполнения указанных задач в рамках процесса
  /// </summary>
  /// <param name="sessionGuid">Гуид сессии пользователя</param>
  /// <param name="activitiesID">Идентификаторы родительских задач</param>
  /// <param name="beginDate">Начальная дата сбора статистики</param>
  /// <param name="endDate">Конечная дата сбора статистики</param>
  /// <param name="period">Период анализа данных</param>
  /// <returns>Таблица с результатами сбора данных (колонки - задачи, строки - среднее время выполнения задач с датой завершения в указанном интервале дат)</returns>
  DataTable GetActivitiesRuntime(
    Guid sessionGuid,
    long[] activitiesID,
    DateTime beginDate,
    DateTime endDate,
    CollectPeriodsEnum period);

  /// <summary>
  /// Метод собирает статистику о том, скольно времени пользователи затратили на выполнение данной задачи
  /// </summary>
  /// <param name="sessionGuid">Гуид сессии пользователя</param>
  /// <param name="activityID">Идентификатор родительской задачи</param>
  /// <param name="beginDate">Начальная дата сбора статистики</param>
  /// <param name="endDate">Конечная дата сбора статистики</param>
  /// <param name="period">Период анализа данных</param>
  /// <returns>Таблица с результатами сбора данных (колонки - пользователи, выполнявшие данную задачу, строки - среднее время выполнения задач с датой завершения в указанном интервале дат)</returns>
  DataTable GetActivityUsersRuntime(
    Guid sessionGuid,
    long activityID,
    DateTime beginDate,
    DateTime endDate,
    CollectPeriodsEnum period);

  /// <summary>
  /// Метод собирает статистику о количестве возвратов задачи
  /// </summary>
  /// <param name="sessionGuid">Гуид сессии пользователя</param>
  /// <param name="activityID">Идентификатор задачи</param>
  /// <param name="beginDate">Начальная дата сбора статистики</param>
  /// <param name="endDate">Конечная дата сбора статистики</param>
  /// <param name="period">Период анализа данных</param>
  /// <returns>Таблица с результатами сбора данных</returns>
  DataTable GetUsersActivityReject(
    Guid sessionGuid,
    long activityID,
    DateTime beginDate,
    DateTime endDate,
    CollectPeriodsEnum period);
}
