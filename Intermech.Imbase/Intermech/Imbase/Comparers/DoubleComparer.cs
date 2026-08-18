// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Comparers.DoubleComparer
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System.Collections;

#nullable disable
namespace Intermech.Imbase.Comparers;

internal class DoubleComparer : IComparer
{
  public int Compare(object x, object y)
  {
    double result1 = 0.0;
    double result2 = 0.0;
    double.TryParse(x as string, out result1);
    double.TryParse(y as string, out result2);
    if (result1 > result2)
      return 1;
    return result1 < result2 ? -1 : 0;
  }
}
