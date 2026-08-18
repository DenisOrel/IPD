// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.TimedEventsSheduler.DateStringWithPeriod
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.DatabaseConfigurator.TimedEventsSheduler;

public static class DateStringWithPeriod
{
  public static string ConvertToString(string[] lst2)
  {
    List<string> stringList = new List<string>(lst2.Length - 1);
    for (int index = 1; index < lst2.Length; ++index)
    {
      int int32_1 = Convert.ToInt32(lst2[index]);
      int num1 = -1;
      int num2 = int32_1;
      while (index < lst2.Length - 1)
      {
        ++index;
        int int32_2 = Convert.ToInt32(lst2[index]);
        if (num2 + 1 == int32_2)
        {
          ++num1;
          num2 = int32_2;
        }
        else
        {
          --index;
          break;
        }
      }
      if (num1 >= 1)
        stringList.Add($"{(object) int32_1}-{(object) num2}");
      else if (num1 == 0)
      {
        stringList.Add(int32_1.ToString());
        stringList.Add(num2.ToString());
      }
      else
        stringList.Add(int32_1.ToString());
    }
    return string.Join(",", stringList.ToArray());
  }
}
