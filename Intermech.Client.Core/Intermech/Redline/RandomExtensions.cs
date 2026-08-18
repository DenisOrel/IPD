
// Type: Intermech.Redline.RandomExtensions
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Redline;

public static class RandomExtensions
{
  /// <summary>Рандомизация UInt64 чисел в промежутке между ((UInt64)min,(UInt64)max)</summary>
  /// <param name="rnd">генератор случайнных чисел</param>
  /// <param name="min">нижняя граница</param>
  /// <param name="max">верхняя граница</param>
  /// <returns>случайное число</returns>
  public static ulong NextULong(this Random rnd, ulong min, ulong max)
  {
    ulong num1 = (ulong) rnd.Next();
    ulong num2 = (ulong) rnd.Next();
    ulong num3 = max - min;
    return min + (num1 << 32 /*0x20*/ | num2) % num3;
  }
}
