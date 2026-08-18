// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.DispatcherExtensions
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Threading;

#nullable disable
namespace Intermech.Extensions;

public static class DispatcherExtensions
{
  public static void TryInvoke([CanBeNull] this Dispatcher dispatcher, [NotNull] Action callback)
  {
    if (dispatcher != null)
      dispatcher.Invoke(callback);
    else
      callback();
  }

  public static void TryInvoke(
    [CanBeNull] this Dispatcher dispatcher,
    [NotNull] Action callback,
    DispatcherPriority priority)
  {
    if (dispatcher != null)
      dispatcher.Invoke(callback, priority);
    else
      callback();
  }

  public static void TryInvoke(
    [CanBeNull] this Dispatcher dispatcher,
    [NotNull] Action callback,
    DispatcherPriority priority,
    CancellationToken cancellationToken)
  {
    if (dispatcher != null)
      dispatcher.Invoke(callback, priority, cancellationToken);
    else
      callback();
  }

  public static void TryInvoke(
    [CanBeNull] this Dispatcher dispatcher,
    [NotNull] Action callback,
    DispatcherPriority priority,
    CancellationToken cancellationToken,
    TimeSpan timeout)
  {
    if (dispatcher != null)
      dispatcher.Invoke(callback, priority, cancellationToken, timeout);
    else
      callback();
  }

  [CanBeNull]
  public static TResult TryInvoke<TResult>([CanBeNull] this Dispatcher dispatcher, [NotNull] Func<TResult> callback)
  {
    return dispatcher == null ? callback() : dispatcher.Invoke<TResult>(callback);
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TResult TryInvoke<TResult>(
    [CanBeNull] this Dispatcher dispatcher,
    [NotNull] Func<TResult> callback,
    DispatcherPriority priority)
  {
    return dispatcher == null ? callback() : dispatcher.Invoke<TResult>(callback, priority);
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TResult TryInvoke<TResult>(
    [CanBeNull] this Dispatcher dispatcher,
    [NotNull] Func<TResult> callback,
    DispatcherPriority priority,
    CancellationToken cancellationToken)
  {
    return dispatcher == null ? callback() : dispatcher.Invoke<TResult>(callback, priority, cancellationToken);
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TResult TryInvoke<TResult>(
    [CanBeNull] this Dispatcher dispatcher,
    [NotNull] Func<TResult> callback,
    DispatcherPriority priority,
    CancellationToken cancellationToken,
    TimeSpan timeout)
  {
    return dispatcher == null ? callback() : dispatcher.Invoke<TResult>(callback, priority, cancellationToken, timeout);
  }
}
