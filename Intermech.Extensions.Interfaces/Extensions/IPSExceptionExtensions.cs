// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.IPSExceptionExtensions
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using System;
using System.Linq;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public static class IPSExceptionExtensions
{
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Exception ExtractNotAbort([NotNull] this Exception exception)
  {
    if (exception is AbortException)
      return (Exception) null;
    if (exception is AggregateException aggregateException)
      return aggregateException.FilterOperationCancelled();
    return exception.InnerException != null && !Enumeration.Create<Exception>(exception.InnerException, (Enumeration.GetNextItemDelegate<Exception>) (e => e.InnerException)).All<Exception>((Func<Exception, bool>) (e => e.ExtractNotAbort() != null)) ? (Exception) null : exception;
  }

  public static bool TryProcessException([NotNull] this Exception exception)
  {
    exception = exception.ExtractNotOperationCancelled();
    if (exception == null)
      return true;
    exception = exception.ExtractNotAbort();
    return exception == null;
  }
}
