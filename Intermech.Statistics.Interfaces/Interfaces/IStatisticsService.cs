// Decompiled with JetBrains decompiler
// Type: Intermech.Statistics.Interfaces.IStatisticsService
// Assembly: Intermech.Statistics.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BE126060-F77F-4F0A-893B-FA8B66A88C31
// Assembly location: D:\IPS\Client\Intermech.Statistics.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.Statistics.Interfaces.xml

using System;

#nullable disable
namespace Intermech.Statistics.Interfaces;

public interface IStatisticsService
{
  /// <summary>Получить статистику для команды сбора.</summary>
  /// <param name="sessionGuid">Гуид сессии.</param>
  /// <param name="commandSettings">Настройки команды статистики.</param>
  /// <returns>Набор статистических данных</returns>
  CollectedStatistics CollectStatistics(Guid sessionGuid, CommandSettings commandSettings);

  /// <summary>Получение настроек объекта статистики.</summary>
  /// <param name="sessionGuid">Гуид сессии.</param>
  /// <param name="statisticObjectId">ИД объекта статистики.</param>
  /// <returns>При проблемах с чтением возвращает null</returns>
  CommandSettings ReadStatisticObjectsCommandSettings(Guid sessionGuid, long statisticObjectId);
}
