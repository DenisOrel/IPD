// Decompiled with JetBrains decompiler
// Type: Intermech.Statistics.Interfaces.CollectPeriodsHelper
// Assembly: Intermech.Statistics.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BE126060-F77F-4F0A-893B-FA8B66A88C31
// Assembly location: D:\IPS\Client\Intermech.Statistics.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.Statistics.Interfaces.xml

using System;

#nullable disable
namespace Intermech.Statistics.Interfaces;

public class CollectPeriodsHelper
{
  /// <summary>Возвращает дату, увеличенную на указанное значение</summary>
  /// <param name="fromDate">Исходная дата</param>
  /// <param name="period">Единица измерения приращения</param>
  /// <param name="number">Величина приращения даты</param>
  /// <returns>Следующая дата</returns>
  public static DateTime NextDateTime(DateTime fromDate, CollectPeriodsEnum period, int number)
  {
    switch (period)
    {
      case CollectPeriodsEnum.Hour:
        return fromDate.AddHours((double) number);
      case CollectPeriodsEnum.Day:
        return fromDate.AddDays((double) number);
      case CollectPeriodsEnum.Week:
        return fromDate.AddDays((double) (number * 7));
      case CollectPeriodsEnum.Month:
        return fromDate.AddMonths(number);
      case CollectPeriodsEnum.Year:
        return fromDate.AddYears(number);
      default:
        throw new KernelException("Unknown CollectPeriodsEnum value");
    }
  }

  /// <summary>
  /// Возвращает дату, увеличенную на единицу в указанных единицах приращения
  /// </summary>
  /// <param name="fromDate">Исходная дата</param>
  /// <param name="period">Единица измерения приращения</param>
  /// <returns>Следующая дата</returns>
  public static DateTime NextDateTime(DateTime fromDate, CollectPeriodsEnum period)
  {
    return CollectPeriodsHelper.NextDateTime(fromDate, period, 1);
  }

  /// <summary>
  /// Получить промежуток времени, предшествующий нашему запросу.
  /// Он нужен для того, чтобы была правильная точка отсчета графика от предыдущего значения.
  /// </summary>
  /// <param name="fromDate">Исходная дата</param>
  /// <param name="period">Единица измерения приращения</param>
  /// <returns></returns>
  /// <exception cref="T:Intermech.KernelException">Unknown CollectPeriodsEnum value</exception>
  public static DateTime PreviousDateTime(DateTime fromDate, CollectPeriodsEnum period)
  {
    switch (period)
    {
      case CollectPeriodsEnum.Hour:
        return fromDate.AddHours(-1.0);
      case CollectPeriodsEnum.Day:
        return fromDate.AddDays(-1.0);
      case CollectPeriodsEnum.Week:
        return fromDate.AddDays(-7.0);
      case CollectPeriodsEnum.Month:
        return fromDate.AddMonths(-1);
      case CollectPeriodsEnum.Year:
        return fromDate.AddYears(-1);
      default:
        throw new KernelException("Unknown CollectPeriodsEnum value");
    }
  }
}
