// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.ExceptionExtensions
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Linq;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public static class ExceptionExtensions
{
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Exception ExtractNotOperationCancelled([NotNull] this Exception exception)
  {
    if (exception is OperationCanceledException)
      return (Exception) null;
    if (exception is AggregateException aggregateException)
      return aggregateException.FilterOperationCancelled();
    return exception.InnerException != null && !Enumeration.Create<Exception>(exception.InnerException, (Enumeration.GetNextItemDelegate<Exception>) (e => e.InnerException)).All<Exception>((Func<Exception, bool>) (e => e.ExtractNotOperationCancelled() != null)) ? (Exception) null : exception;
  }

  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryExtractNotOperationCancelled([NotNull] this Exception exception, out Exception result)
  {
    result = exception.ExtractNotOperationCancelled();
    return result != null;
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Exception ExtractNotComeFrom([NotNull] this Exception exception, [NotNull] Exception otherException)
  {
    if (exception == otherException)
      return (Exception) null;
    if (exception is AggregateException aggregateException)
      return aggregateException.FilterNotComeFrom(otherException);
    return exception.InnerException != null && !Enumeration.Create<Exception>(exception.InnerException, (Enumeration.GetNextItemDelegate<Exception>) (e => e.InnerException)).All<Exception>((Func<Exception, bool>) (e => e.ExtractNotComeFrom(otherException) != null)) ? (Exception) null : exception;
  }

  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryExtractNotComeFrom(
    [NotNull] this Exception exception,
    [NotNull] Exception otherException,
    out Exception result)
  {
    result = exception.ExtractNotComeFrom(otherException);
    return result != null;
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TException GetExceptionOfType<TException>([NotNull] this Exception exception) where TException : Exception
  {
    Type type = typeof (TException);
    if (type != typeof (OperationCanceledException) && !type.IsSubclassOf(typeof (OperationCanceledException)))
    {
      exception = exception.ExtractNotOperationCancelled();
      if (exception == null)
        return default (TException);
    }
    if (exception is TException exceptionOfType)
      return exceptionOfType;
    if (exception is AggregateException aggregateException)
      return aggregateException.ExtractExceptionOfType<TException>();
    return exception.InnerException == null ? default (TException) : Enumeration.Create<Exception>(exception.InnerException, (Enumeration.GetNextItemDelegate<Exception>) (e => e.InnerException)).SelectFirstNotNull<Exception, TException>((Func<Exception, TException>) (e => e.GetExceptionOfType<TException>()));
  }

  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetExceptionOfType<TException>(
    [NotNull] this Exception exception,
    out TException result)
    where TException : Exception
  {
    result = exception.GetExceptionOfType<TException>();
    return (object) result != null;
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static OperationCanceledException GetOperationCancelled([NotNull] this Exception exception)
  {
    return exception.GetExceptionOfType<OperationCanceledException>();
  }

  [ContractAnnotation("=> true, operationCanceledException: notnull; => false, operationCanceledException: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetOperationCancelled(
    [NotNull] this Exception exception,
    out OperationCanceledException operationCanceledException)
  {
    operationCanceledException = exception.GetExceptionOfType<OperationCanceledException>();
    return operationCanceledException != null;
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ErrorMessageException GetErrorMessageException([NotNull] this Exception exception)
  {
    return exception.GetExceptionOfType<ErrorMessageException>();
  }

  [ContractAnnotation("=> true, errorMessageException: notnull; => false, errorMessageException: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetErrorMessageException(
    [NotNull] this Exception exception,
    out ErrorMessageException errorMessageException)
  {
    errorMessageException = exception.GetExceptionOfType<ErrorMessageException>();
    return errorMessageException != null;
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T TryGetExceptionOfType<T>([NotNull] this Exception exception) where T : Exception
  {
    if (exception is T exceptionOfType)
      return exceptionOfType;
    if (exception is AggregateException aggregateException)
      return aggregateException.ExtractExceptionOfType<T>();
    return exception.InnerException == null ? default (T) : Enumeration.Create<Exception>(exception.InnerException, (Enumeration.GetNextItemDelegate<Exception>) (e => e.InnerException)).SelectFirstNotNull<Exception, T>((Func<Exception, T>) (e => e.TryGetExceptionOfType<T>()));
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static OperationCanceledException TryGetOperationCancelled([NotNull] this Exception exception)
  {
    return exception.TryGetExceptionOfType<OperationCanceledException>();
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ErrorMessageException TryGetErrorMessageException([NotNull] this Exception exception)
  {
    return exception.TryGetExceptionOfType<ErrorMessageException>();
  }

  public delegate void ExceptionAction([NotNull] Exception exception);
}
