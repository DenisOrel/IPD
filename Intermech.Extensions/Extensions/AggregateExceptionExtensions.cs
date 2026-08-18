// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.AggregateExceptionExtensions
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public static class AggregateExceptionExtensions
{
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Exception FilterOperationCancelled([NotNull] this AggregateException aggregateException)
  {
    AggregateException aggregateException1 = aggregateException.Flatten();
    IReadOnlyCollection<Exception> list = (IReadOnlyCollection<Exception>) aggregateException1.InnerExceptions.Where<Exception>((Func<Exception, bool>) (exception => exception.ExtractNotOperationCancelled() == null)).ToList<Exception>();
    if (list.Count == 0)
      return (Exception) null;
    if (aggregateException1.InnerExceptions.Count == list.Count)
      return (Exception) aggregateException;
    return list.Count <= 1 ? list.First<Exception>() : (Exception) new AggregateException((IEnumerable<Exception>) list);
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Exception FilterNotComeFrom(
    [NotNull] this AggregateException aggregateException,
    [NotNull] Exception otherException)
  {
    AggregateException aggregateException1 = aggregateException.Flatten();
    IReadOnlyCollection<Exception> list = (IReadOnlyCollection<Exception>) aggregateException1.InnerExceptions.Where<Exception>((Func<Exception, bool>) (exception => exception.ExtractNotComeFrom(otherException) == null)).ToList<Exception>();
    if (list.Count == 0)
      return (Exception) null;
    if (aggregateException1.InnerExceptions.Count == list.Count)
      return (Exception) aggregateException;
    return list.Count <= 1 ? list.First<Exception>() : (Exception) new AggregateException((IEnumerable<Exception>) list);
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TException ExtractExceptionOfType<TException>(
    [NotNull] this AggregateException aggregateException)
    where TException : Exception
  {
    return aggregateException.Flatten().InnerExceptions.SelectFirstNotNull<Exception, TException>((Func<Exception, TException>) (exception => exception.GetExceptionOfType<TException>()));
  }

  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryExtractExceptionOfType<TException>(
    [NotNull] this AggregateException aggregateException,
    out TException result)
    where TException : Exception
  {
    result = aggregateException.ExtractExceptionOfType<TException>();
    return (object) result != null;
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static OperationCanceledException ExtractOperationCanceled(
    [NotNull] this AggregateException aggregateException)
  {
    return aggregateException.ExtractExceptionOfType<OperationCanceledException>();
  }

  [ContractAnnotation("=> true, operationCanceledException: notnull; => false, operationCanceledException: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryExtractOperationCanceled(
    [NotNull] this AggregateException aggregateException,
    out OperationCanceledException operationCanceledException)
  {
    operationCanceledException = aggregateException.ExtractExceptionOfType<OperationCanceledException>();
    return operationCanceledException != null;
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ErrorMessageException ExtractErrorMessageException(
    [NotNull] this AggregateException aggregateException)
  {
    return aggregateException.ExtractExceptionOfType<ErrorMessageException>();
  }

  [ContractAnnotation("=> true, errorMessageException: notnull; => false, errorMessageException: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryExtractErrorMessageException(
    [NotNull] this AggregateException aggregateException,
    out ErrorMessageException errorMessageException)
  {
    errorMessageException = aggregateException.ExtractExceptionOfType<ErrorMessageException>();
    return errorMessageException != null;
  }
}
