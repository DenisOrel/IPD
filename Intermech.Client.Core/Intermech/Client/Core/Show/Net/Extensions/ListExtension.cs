
// Type: Intermech.Client.Core.Show.Net.Extensions.ListExtension
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;


namespace Intermech.Client.Core.Show.Net.Extensions;

public static class ListExtension
{
  /// <summary>
  /// Объединение последовательностей элементов ( если начальные или конечные элементы между собой совпадают)
  /// </summary>
  /// <remarks>Внимание! Порядок элементов в target / source после объединения может измениться !</remarks>
  /// <typeparam name="T"></typeparam>
  /// <param name="target"></param>
  /// <param name="source"></param>
  /// <returns></returns>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool AddChain<T>(this List<T> target, [NotNull] List<T> source)
  {
    int count1 = source.Count;
    if (count1 == 0)
      return false;
    int count2 = target.Count;
    if (count2 == 0)
    {
      target.AddRange((IEnumerable<T>) source);
      return true;
    }
    int[] numArray1 = new int[2]{ 0, count2 - 1 };
    foreach (int index1 in numArray1)
    {
      int[] numArray2 = new int[2]{ 0, count1 - 1 };
      foreach (int index2 in numArray2)
      {
        if (target[index1].Equals((object) source[index2]))
        {
          target.RemoveAt(index1);
          bool flag = index1 != 0;
          if (index1 == 0 && index2 == 0 || index1 > 0 && index2 > 0)
          {
            if (target.Count > count1)
            {
              source.Reverse();
              flag = index2 != 0;
            }
            else
            {
              target.Reverse();
              flag = index1 == 0;
            }
          }
          if (flag)
            target.AddRange((IEnumerable<T>) source);
          else
            target.InsertRange(0, (IEnumerable<T>) source);
          return true;
        }
      }
    }
    return false;
  }
}
