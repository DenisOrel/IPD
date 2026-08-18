// Decompiled with JetBrains decompiler
// Type: Intermech.HashHelper
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech;

public static class HashHelper
{
  public static int GetHashCode<T1, T2>(T1 arg1, T2 arg2)
  {
    return 31 /*0x1F*/ * arg1.GetHashCode() + arg2.GetHashCode();
  }

  public static int GetHashCode<T1, T2, T3>(T1 arg1, T2 arg2, T3 arg3)
  {
    return 31 /*0x1F*/ * (31 /*0x1F*/ * arg1.GetHashCode() + arg2.GetHashCode()) + arg3.GetHashCode();
  }

  public static int GetHashCode<T1, T2, T3, T4>(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
  {
    return 31 /*0x1F*/ * (31 /*0x1F*/ * (31 /*0x1F*/ * arg1.GetHashCode() + arg2.GetHashCode()) + arg3.GetHashCode()) + arg4.GetHashCode();
  }

  public static int GetHashCode<T>(T[] list)
  {
    int hashCode = 0;
    foreach (T obj in list)
      hashCode = 31 /*0x1F*/ * hashCode + obj.GetHashCode();
    return hashCode;
  }

  public static int GetHashCode<T>(IEnumerable<T> list)
  {
    int hashCode = 0;
    foreach (T obj in list)
      hashCode = 31 /*0x1F*/ * hashCode + obj.GetHashCode();
    return hashCode;
  }

  /// <summary>
  /// Gets a hashcode for a collection for that the order of items
  /// does not matter.
  /// So {1, 2, 3} and {3, 2, 1} will get same hash code.
  /// </summary>
  public static int GetHashCodeForOrderNoMatterCollection<T>(IEnumerable<T> list)
  {
    int num1 = 0;
    int num2 = 0;
    foreach (T obj in list)
    {
      num1 += obj.GetHashCode();
      ++num2;
    }
    return 31 /*0x1F*/ * num1 + num2.GetHashCode();
  }

  /// <summary>
  /// Alternative way to get a hashcode is to use a fluent
  /// interface like this:<br />
  /// return 0.CombineHashCode(field1).CombineHashCode(field2).
  ///     CombineHashCode(field3);
  /// </summary>
  public static int CombineHashCode<T>(int hashCode, T arg)
  {
    return 31 /*0x1F*/ * hashCode + arg.GetHashCode();
  }
}
