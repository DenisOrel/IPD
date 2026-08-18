// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.MeasuredValueComparer
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AVS;

/// <summary>Класс для сравнения MeasuredValue при сортировке</summary>
public class MeasuredValueComparer : IComparer<MeasuredValue>
{
  public int Compare(MeasuredValue x, MeasuredValue y)
  {
    switch (MeasureHelper.Compare(x, y))
    {
      case CompareResult.More:
        return 1;
      case CompareResult.Less:
        return -1;
      case CompareResult.NotCompatible:
        return string.Compare(MeasureHelper.FindDescriptor(x.MeasureID).ShortName, MeasureHelper.FindDescriptor(y.MeasureID).ShortName);
      default:
        return 0;
    }
  }
}
