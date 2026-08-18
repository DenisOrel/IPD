// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.OneOrMore
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.Extensions;

public static class OneOrMore
{
  public static OneOrMore<TOther> ConvertAll<TFrom, TOther>(
    OneOrMore<TFrom> source,
    [NotNull] Converter<TFrom, TOther> converter)
  {
    if (!source.HasValue)
      return new OneOrMore<TOther>();
    if (source.OneValue)
      return new OneOrMore<TOther>(converter(source.Value));
    IReadOnlyList<TFrom> values = source.Values;
    switch (values.Count)
    {
      case 0:
        return new OneOrMore<TOther>();
      case 1:
        return new OneOrMore<TOther>(converter(values[0]));
      default:
        switch (values)
        {
          case List<TFrom> fromList:
            return new OneOrMore<TOther>((IReadOnlyList<TOther>) fromList.ConvertAll<TOther>(converter), true);
          case TFrom[] array:
            return new OneOrMore<TOther>((IReadOnlyList<TOther>) Array.ConvertAll<TFrom, TOther>(array, converter), true);
          default:
            return new OneOrMore<TOther>(source.ConvertAll<TFrom, TOther>(converter), true);
        }
    }
  }

  public static OneOrMore<TOther> ConvertToAncestor<TFrom, TOther>(OneOrMore<TFrom> source) where TFrom : TOther
  {
    if (!source.HasValue)
      return new OneOrMore<TOther>();
    if (source.OneValue)
      return new OneOrMore<TOther>((TOther) source.Value);
    IReadOnlyList<TFrom> values = source.Values;
    int count = values.Count;
    switch (count)
    {
      case 0:
        return new OneOrMore<TOther>();
      case 1:
        return new OneOrMore<TOther>((TOther) values[0]);
      default:
        switch (values)
        {
          case List<TFrom> fromList:
            return new OneOrMore<TOther>((IReadOnlyList<TOther>) fromList.ConvertAll<TOther>((Converter<TFrom, TOther>) (item => (TOther) item)), true);
          case TFrom[] array:
            return new OneOrMore<TOther>((IReadOnlyList<TOther>) Array.ConvertAll<TFrom, TOther>(array, (Converter<TFrom, TOther>) (item => (TOther) item)), true);
          default:
            return new OneOrMore<TOther>((IReadOnlyList<TOther>) source.Cast<TOther>().ToList<TOther>(count), true);
        }
    }
  }
}
