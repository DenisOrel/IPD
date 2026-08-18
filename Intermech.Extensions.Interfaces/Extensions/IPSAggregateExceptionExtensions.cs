// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.IPSAggregateExceptionExtensions
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public static class IPSAggregateExceptionExtensions
{
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Exception FilterAbort([NotNull] this AggregateException aggregateException)
  {
    AggregateException aggregateException1 = aggregateException.Flatten();
    IReadOnlyCollection<Exception> list = (IReadOnlyCollection<Exception>) aggregateException1.InnerExceptions.Where<Exception>((Func<Exception, bool>) (exception => exception.ExtractNotAbort() == null)).ToList<Exception>();
    if (list.Count == 0)
      return (Exception) null;
    if (aggregateException1.InnerExceptions.Count == list.Count)
      return (Exception) aggregateException;
    return list.Count <= 1 ? list.First<Exception>() : (Exception) new AggregateException((IEnumerable<Exception>) list);
  }
}
