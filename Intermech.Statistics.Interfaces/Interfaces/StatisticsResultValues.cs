// Decompiled with JetBrains decompiler
// Type: Intermech.Statistics.Interfaces.StatisticsResultValues
// Assembly: Intermech.Statistics.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BE126060-F77F-4F0A-893B-FA8B66A88C31
// Assembly location: D:\IPS\Client\Intermech.Statistics.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.Statistics.Interfaces.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Statistics.Interfaces;

[Serializable]
public class StatisticsResultValues : ICloneable
{
  private string _caption;

  /// <summary>Список поинтов для построение графика</summary>
  public List<StatisticsPoint> Points { get; set; }

  /// <summary>
  /// Название для легенды графика (является либо названием колонки от пользователя, либо название данных по которым собиралась статистики, имя пользователя, шаг ЖЦ, уровень продвижения и прочее)
  /// </summary>
  public string Caption
  {
    get
    {
      return !string.IsNullOrEmpty(this.TypeCaption) ? $"{this._caption} ({this.TypeCaption})" : this._caption;
    }
    set => this._caption = value;
  }

  /// <summary>
  /// Для сбора статистики по дате создания и по дате подписания
  /// Наименование типа, для которого подсчитана статистика
  /// </summary>
  public string TypeCaption { get; set; }

  public StatisticsResultValues()
  {
  }

  public StatisticsResultValues(string caption, List<StatisticsPoint> statisticsPoints)
  {
    this.Caption = caption;
    this.Points = statisticsPoints;
  }

  public object Clone()
  {
    StatisticsResultValues newResultValues = new StatisticsResultValues()
    {
      Caption = this.Caption,
      Points = new List<StatisticsPoint>(this.Points.Count)
    };
    this.Points.ForEach((Action<StatisticsPoint>) (item => newResultValues.Points.Add(new StatisticsPoint(item.Value, item.PeriodsStart, item.PeriodsEnd, item.PeriodsIndex))));
    return (object) newResultValues;
  }
}
