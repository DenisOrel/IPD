// Decompiled with JetBrains decompiler
// Type: Intermech.Statistics.StatisticsPoint
// Assembly: Intermech.Statistics.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BE126060-F77F-4F0A-893B-FA8B66A88C31
// Assembly location: D:\IPS\Client\Intermech.Statistics.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.Statistics.Interfaces.xml

using System;

#nullable disable
namespace Intermech.Statistics;

[Serializable]
public class StatisticsPoint
{
  /// <summary>Начало периода сбора статистики</summary>
  public DateTime PeriodsStart { get; }

  public DateTime PeriodsEnd { get; }

  public int PeriodsIndex { get; }

  /// <summary>Значение У на графике.</summary>
  public object Value { get; }

  public static double ValueAsDouble(object value)
  {
    switch (value)
    {
      case DateTime dateTime:
        return Convert.ToDouble(dateTime);
      case TimeSpan timeSpan:
        return timeSpan.TotalSeconds;
      default:
        return Convert.ToDouble(value);
    }
  }

  /// <summary>Строковое выражение значения</summary>
  public string ValueAsString(string format)
  {
    if (!(this.Value is TimeSpan timeSpan))
      return this.Value.ToString();
    return format == string.Empty ? timeSpan.ToString("d\\.hh\\:mm\\:ss") : timeSpan.ToString(format);
  }

  public string ValueAsString() => this.ValueAsString(string.Empty);

  /// <summary>
  /// Разность между значением и стандартным отклонением.
  /// Может быть отрицательным (если значение меньше) и положительным числом (если больше).
  /// </summary>
  public double DifferenceFromSigma { get; set; }

  public StatisticsPoint(object value, DateTime periodsStart, DateTime periodsEnd, int index)
  {
    this.Value = value;
    this.PeriodsStart = periodsStart;
    this.PeriodsEnd = periodsEnd;
    this.PeriodsIndex = index;
  }
}
