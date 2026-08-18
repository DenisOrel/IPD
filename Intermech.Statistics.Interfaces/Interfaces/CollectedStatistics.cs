// Decompiled with JetBrains decompiler
// Type: Intermech.Statistics.Interfaces.CollectedStatistics
// Assembly: Intermech.Statistics.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BE126060-F77F-4F0A-893B-FA8B66A88C31
// Assembly location: D:\IPS\Client\Intermech.Statistics.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.Statistics.Interfaces.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Statistics.Interfaces;

/// <summary>Собранная по настройке статистика</summary>
[Serializable]
public class CollectedStatistics
{
  /// <summary>Настройки, по которым была собрана статистика</summary>
  private readonly CommandSettings _commandSettings;

  /// <summary>
  /// Собранные статистические значения по каждому пользователю, типу и пр..
  /// </summary>
  public List<Intermech.Statistics.Interfaces.StatisticsResultValues> StatisticsResultValues { get; }

  public ExcludeAbnormalValuesSettings ExcludeAbnormalValuesSettings
  {
    get => this._commandSettings.ExcludeAbnormalValuesSettings;
  }

  /// <summary>Подпериоды сбора статистики.</summary>
  public List<Period> Periods => this._commandSettings.Periods;

  public CollectPeriodsEnum CollectPeriod => this._commandSettings.CollectPeriod;

  /// <summary>Анализируемые типы объектов</summary>
  public List<ObjectTypesListItem> AnalizedObjectTypes
  {
    get => this._commandSettings.AnalizedObjectsTypes;
  }

  /// <summary>Наименование</summary>
  public string Caption { get; }

  /// <summary>Тип сбора статистики</summary>
  public CommandStatisticsTypesEnum StatisticsType => this._commandSettings.CommandType;

  /// <summary>Время начала подсчета статистики</summary>
  public DateTime StartDateTime => this._commandSettings.StartDateTime;

  /// <summary>Время окончания подсчета статистики</summary>
  public DateTime EndDateTime => this._commandSettings.EndDateTime;

  public CollectedStatistics(
    string caption,
    List<Intermech.Statistics.Interfaces.StatisticsResultValues> statisticsResultValues,
    CommandSettings commandSettings)
  {
    this._commandSettings = commandSettings;
    this.StatisticsResultValues = statisticsResultValues;
    this.Caption = caption;
  }

  /// <summary>
  /// В собранной статистике есть какие-либо значения, отличные от нуля
  /// </summary>
  public bool HasValuePoints()
  {
    foreach (Intermech.Statistics.Interfaces.StatisticsResultValues statisticsResultValue in this.StatisticsResultValues)
    {
      foreach (StatisticsPoint point in statisticsResultValue.Points)
      {
        if (StatisticsPoint.ValueAsDouble(point.Value) != 0.0)
          return true;
      }
    }
    return false;
  }
}
