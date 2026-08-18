// Decompiled with JetBrains decompiler
// Type: Intermech.Statistics.Interfaces.Limits
// Assembly: Intermech.Statistics.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BE126060-F77F-4F0A-893B-FA8B66A88C31
// Assembly location: D:\IPS\Client\Intermech.Statistics.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.Statistics.Interfaces.xml

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.Statistics.Interfaces;

/// <summary>
/// Границы, в которое должно попадать значение
/// https://basegroup.ru/community/bank/mean-square-deviation
/// </summary>
[Serializable]
public class Limits
{
  /// <summary>Нижняя граница</summary>
  private double _lowerLimit;
  /// <summary>
  /// 
  /// </summary>
  private double _higherLimit;

  public Limits()
  {
  }

  public Limits(double lowerLimit, double higherLimit)
  {
    this._lowerLimit = lowerLimit;
    this._higherLimit = higherLimit;
  }

  /// <summary>Попадает ли значение в границы разумного.</summary>
  /// <param name="value">Значение.</param>
  /// <returns>true - если попадает</returns>
  public bool Fits(double value) => value >= this._lowerLimit && value <= this._higherLimit;

  /// <summary>
  /// Посчитать максимально допустимое отклонение от среднеквадратичного.
  /// </summary>
  /// <param name="values">Значения, на основании которых считается отклонение.</param>
  /// <param name="percent">Допустимый процент отклонения. Не может быть 0. Положительное целое число.</param>
  /// <returns></returns>
  public static Limits CountLimits(List<double> values, uint percent)
  {
    double arithmeticAverage = values.Average();
    double num = Math.Sqrt(Limits.CountSumSquareDiff(values, arithmeticAverage) / (double) values.Count);
    double higherLimit = arithmeticAverage + num * (double) percent / 100.0;
    return new Limits(arithmeticAverage - num * (double) percent / 100.0, higherLimit);
  }

  /// <summary>Посчитать сумму квадратов разностей</summary>
  /// <param name="allValues">All values.</param>
  /// <param name="arithmeticAverage">The arithmetic average.</param>
  /// <returns></returns>
  private static double CountSumSquareDiff(List<double> allValues, double arithmeticAverage)
  {
    double num1 = 0.0;
    foreach (double allValue in allValues)
    {
      double num2 = Math.Pow(allValue - arithmeticAverage, 2.0);
      num1 += num2;
    }
    return num1;
  }
}
