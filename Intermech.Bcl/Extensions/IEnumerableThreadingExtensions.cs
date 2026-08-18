
// Type: Intermech.Extensions.IEnumerableThreadingExtensions
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;


namespace Intermech.Extensions
{
    /// <summary>Расширения для IEnumerable связанные с многопоточной работой без использования PLINQ.</summary>
    public static class IEnumerableThreadingExtensions
    {
      /// <summary>Enumerates with cancellation in this collection with check of Cancellation on every element.</summary>
      /// <exception cref="T:System.OperationCanceledException">Thrown when an Operation Canceled error condition occurs.</exception>
      /// <param name="enumerable">The enumerable to act on. This cannot be null.</param>
      /// <param name="token">The cancellation token.</param>
      /// <param name="messageOnCancelException">(Optional) The message on cancel exception. This may be null.</param>
      /// <returns>An enumerator that allows foreach to be used to process with cancellation in this collection. This will never be null.</returns>
      [NotNull]
      [ItemCanBeNull]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static IEnumerable<T> WithCancellation<T>(
        [NotNull] this IEnumerable<T> enumerable,
        CancellationToken token,
        [CanBeNull] string messageOnCancelException = null)
      {
        foreach (T obj in enumerable)
        {
          if (token.IsCancellationRequested)
          {
            if (!string.IsNullOrWhiteSpace(messageOnCancelException))
              throw new OperationCanceledException(messageOnCancelException, token);
            throw new OperationCanceledException(token);
          }
          yield return obj;
        }
      }

      /// <summary>Enumerates with cancellation in this collection with check of Cancellation on every element.</summary>
      /// <exception cref="T:System.OperationCanceledException">Thrown when an Operation Canceled error condition occurs.</exception>
      /// <param name="enumerable">The enumerable to act on. This cannot be null.</param>
      /// <param name="token">The cancellation token.</param>
      /// <param name="messageOnCancelException">(Optional) The message on cancel exception. This may be null.</param>
      /// <returns>An enumerator that allows foreach to be used to process with cancellation in this collection. This will never be null.</returns>
      [NotNull]
      [ItemCanBeNull]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static IEnumerable<T> WithCancellation<T>(
        [NotNull] this IEnumerable<T> enumerable,
        CancellationToken? token,
        [CanBeNull] string messageOnCancelException = null)
      {
        return !token.HasValue ? enumerable : enumerable.WithCancellation(token.Value, messageOnCancelException);
      }

      /// <summary>Enumerates with cancellation in this collection with check of Cancellation on every element.</summary>
      /// <exception cref="T:System.OperationCanceledException">Thrown when an Operation Canceled error condition occurs.</exception>
      /// <param name="enumerable">The enumerable to act on. This cannot be null.</param>
      /// <param name="cancellationTokenSource">The cancellation token.</param>
      /// <param name="messageOnCancelException">(Optional) The message on cancel exception. This may be null.</param>
      /// <returns>An enumerator that allows foreach to be used to process with cancellation in this collection. This will never be null.</returns>
      [NotNull]
      [ItemCanBeNull]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static IEnumerable<T> WithCancellation<T>(
        [NotNull] this IEnumerable<T> enumerable,
        [NotNull] CancellationTokenSource cancellationTokenSource,
        [CanBeNull] string messageOnCancelException = null)
      {
        foreach (T obj in enumerable)
        {
          if (cancellationTokenSource.IsCancellationRequested)
          {
            if (!string.IsNullOrWhiteSpace(messageOnCancelException))
              throw new OperationCanceledException(messageOnCancelException, cancellationTokenSource.Token);
            throw new OperationCanceledException(cancellationTokenSource.Token);
          }
          yield return obj;
        }
      }

      /// <summary>Enumerates with cancellation in this collection with check of Cancellation on every element.</summary>
      /// <exception cref="T:System.OperationCanceledException">Thrown when an Operation Canceled error condition occurs.</exception>
      /// <param name="enumerable">The enumerable to act on. This cannot be null.</param>
      /// <param name="token">The cancellation token.</param>
      /// <param name="messageOnCancelException">(Optional) The message on cancel exception. This may be null.</param>
      /// <returns>An enumerator that allows foreach to be used to process with cancellation in this collection. This will never be null.</returns>
      [NotNull]
      [ItemNotNull]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static IEnumerable<T> WithCancellationNotNull<T>(
        [NotNull, ItemNotNull] this IEnumerable<T> enumerable,
        CancellationToken token,
        [CanBeNull] string messageOnCancelException = null)
        where T : class
      {
        foreach (T obj in enumerable)
        {
          if (token.IsCancellationRequested)
          {
            if (!string.IsNullOrWhiteSpace(messageOnCancelException))
              throw new OperationCanceledException(messageOnCancelException, token);
            throw new OperationCanceledException(token);
          }
          yield return obj;
        }
      }

      /// <summary>Enumerates with cancellation in this collection with check of Cancellation on every element.</summary>
      /// <exception cref="T:System.OperationCanceledException">Thrown when an Operation Canceled error condition occurs.</exception>
      /// <param name="enumerable">The enumerable to act on. This cannot be null.</param>
      /// <param name="token">The cancellation token.</param>
      /// <param name="messageOnCancelException">(Optional) The message on cancel exception. This may be null.</param>
      /// <returns>An enumerator that allows foreach to be used to process with cancellation in this collection. This will never be null.</returns>
      [NotNull]
      [ItemNotNull]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static IEnumerable<T> WithCancellationNotNull<T>(
        [NotNull, ItemNotNull] this IEnumerable<T> enumerable,
        CancellationToken? token,
        [CanBeNull] string messageOnCancelException = null)
        where T : class
      {
        return !token.HasValue ? enumerable : enumerable.WithCancellationNotNull(token.Value, messageOnCancelException);
      }

      /// <summary>Enumerates with cancellation in this collection with check of Cancellation on every element.</summary>
      /// <exception cref="T:System.OperationCanceledException">Thrown when an Operation Canceled error condition occurs.</exception>
      /// <param name="enumerable">The enumerable to act on. This cannot be null.</param>
      /// <param name="cancellationTokenSource">The cancellation token.</param>
      /// <param name="messageOnCancelException">(Optional) The message on cancel exception. This may be null.</param>
      /// <returns>An enumerator that allows foreach to be used to process with cancellation in this collection. This will never be null.</returns>
      [NotNull]
      [ItemNotNull]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static IEnumerable<T> WithCancellationNotNull<T>(
        [NotNull, ItemNotNull] this IEnumerable<T> enumerable,
        [NotNull] CancellationTokenSource cancellationTokenSource,
        [CanBeNull] string messageOnCancelException = null)
        where T : class
      {
        foreach (T obj in enumerable)
        {
          if (cancellationTokenSource.IsCancellationRequested)
          {
            if (!string.IsNullOrWhiteSpace(messageOnCancelException))
              throw new OperationCanceledException(messageOnCancelException, cancellationTokenSource.Token);
            throw new OperationCanceledException(cancellationTokenSource.Token);
          }
          yield return obj;
        }
      }
    }
}
