// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Comparers.IntegerComparer
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System.Collections;

#nullable disable
namespace Intermech.Imbase.Comparers;

internal class IntegerComparer : IComparer
{
  public int Compare(object x, object y)
  {
    int result1 = 0;
    int result2 = 0;
    int.TryParse(x as string, out result1);
    int.TryParse(y as string, out result2);
    return result1 - result2;
  }
}
