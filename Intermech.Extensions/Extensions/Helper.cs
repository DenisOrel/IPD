// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.Helper
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace Intermech.Extensions;

public abstract class Helper
{
  public const double DoubleEqualityTolerance = 1E-09;

  [NotNull]
  public static Task RunInThread(
    [NotNull] Action<CancellationToken> action,
    CancellationToken cancellationToken,
    [CanBeNull, InstantHandle] Action<Thread> initThreadAction = null)
  {
    cancellationToken.ThrowIfCancellationRequested();
    TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
    Thread thread = new Thread((ThreadStart) (() =>
    {
      try
      {
        if (cancellationToken.IsCancellationRequested)
        {
          taskCompletionSource.TrySetCanceled();
        }
        else
        {
          action(cancellationToken);
          if (cancellationToken.IsCancellationRequested)
            taskCompletionSource.TrySetCanceled();
          else
            taskCompletionSource.TrySetResult(true);
        }
      }
      catch (Exception ex)
      {
        if (cancellationToken.IsCancellationRequested)
          taskCompletionSource.TrySetCanceled();
        else
          taskCompletionSource.TrySetException(ex);
      }
    }));
    if (initThreadAction != null)
      initThreadAction(thread);
    if (cancellationToken != CancellationToken.None)
      cancellationToken.Register((Action) (() => taskCompletionSource.TrySetCanceled()));
    cancellationToken.ThrowIfCancellationRequested();
    thread.Start();
    return (Task) taskCompletionSource.Task;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Task RunInThread(
    [NotNull] Action action,
    CancellationToken cancellationToken,
    [CanBeNull, InstantHandle] Action<Thread> initThreadAction = null)
  {
    return Helper.RunInThread((Action<CancellationToken>) (_ => action()), cancellationToken, initThreadAction);
  }

  [NotNull]
  public static Task RunInThread<T>(
    [NotNull] Action<CancellationToken, T> action,
    CancellationToken cancellationToken,
    [CanBeNull] T param1,
    [CanBeNull, InstantHandle] Action<Thread> initThreadAction = null)
  {
    cancellationToken.ThrowIfCancellationRequested();
    TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
    Thread thread = new Thread((ThreadStart) (() =>
    {
      try
      {
        if (cancellationToken.IsCancellationRequested)
        {
          taskCompletionSource.TrySetCanceled();
        }
        else
        {
          action(cancellationToken, param1);
          if (cancellationToken.IsCancellationRequested)
            taskCompletionSource.TrySetCanceled();
          else
            taskCompletionSource.TrySetResult(true);
        }
      }
      catch (Exception ex)
      {
        if (cancellationToken.IsCancellationRequested)
          taskCompletionSource.TrySetCanceled();
        else
          taskCompletionSource.TrySetException(ex);
      }
    }));
    if (initThreadAction != null)
      initThreadAction(thread);
    if (cancellationToken != CancellationToken.None)
      cancellationToken.Register((Action) (() => taskCompletionSource.TrySetCanceled()));
    cancellationToken.ThrowIfCancellationRequested();
    thread.Start();
    return (Task) taskCompletionSource.Task;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Task RunInThread<T>(
    [NotNull] Action<T> action,
    CancellationToken cancellationToken,
    [CanBeNull] T param1,
    [CanBeNull, InstantHandle] Action<Thread> initThreadAction = null)
  {
    return Helper.RunInThread((Action<CancellationToken>) (_ => action(param1)), cancellationToken, initThreadAction);
  }

  [NotNull]
  public static Task RunInThread<T1, T2>(
    [NotNull] Action<CancellationToken, T1, T2> action,
    CancellationToken cancellationToken,
    [CanBeNull] T1 param1,
    [CanBeNull] T2 param2,
    [CanBeNull, InstantHandle] Action<Thread> initThreadAction = null)
  {
    cancellationToken.ThrowIfCancellationRequested();
    TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
    Thread thread = new Thread((ThreadStart) (() =>
    {
      try
      {
        if (cancellationToken.IsCancellationRequested)
        {
          taskCompletionSource.TrySetCanceled();
        }
        else
        {
          action(cancellationToken, param1, param2);
          if (cancellationToken.IsCancellationRequested)
            taskCompletionSource.TrySetCanceled();
          else
            taskCompletionSource.TrySetResult(true);
        }
      }
      catch (Exception ex)
      {
        if (cancellationToken.IsCancellationRequested)
          taskCompletionSource.TrySetCanceled();
        else
          taskCompletionSource.TrySetException(ex);
      }
    }));
    if (initThreadAction != null)
      initThreadAction(thread);
    if (cancellationToken != CancellationToken.None)
      cancellationToken.Register((Action) (() => taskCompletionSource.TrySetCanceled()));
    cancellationToken.ThrowIfCancellationRequested();
    thread.Start();
    return (Task) taskCompletionSource.Task;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Task RunInThread<T1, T2>(
    [NotNull] Action<T1, T2> action,
    CancellationToken cancellationToken,
    [CanBeNull] T1 param1,
    [CanBeNull] T2 param2,
    [CanBeNull, InstantHandle] Action<Thread> initThreadAction = null)
  {
    return Helper.RunInThread((Action<CancellationToken>) (_ => action(param1, param2)), cancellationToken, initThreadAction);
  }

  [NotNull]
  public static Task RunInThread<T1, T2, T3>(
    [NotNull] Action<CancellationToken, T1, T2, T3> action,
    CancellationToken cancellationToken,
    [CanBeNull] T1 param1,
    [CanBeNull] T2 param2,
    [CanBeNull] T3 param3,
    [CanBeNull, InstantHandle] Action<Thread> initThreadAction = null)
  {
    cancellationToken.ThrowIfCancellationRequested();
    TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
    Thread thread = new Thread((ThreadStart) (() =>
    {
      try
      {
        if (cancellationToken.IsCancellationRequested)
        {
          taskCompletionSource.TrySetCanceled();
        }
        else
        {
          action(cancellationToken, param1, param2, param3);
          if (cancellationToken.IsCancellationRequested)
            taskCompletionSource.TrySetCanceled();
          else
            taskCompletionSource.TrySetResult(true);
        }
      }
      catch (Exception ex)
      {
        if (cancellationToken.IsCancellationRequested)
          taskCompletionSource.TrySetCanceled();
        else
          taskCompletionSource.TrySetException(ex);
      }
    }));
    if (initThreadAction != null)
      initThreadAction(thread);
    if (cancellationToken != CancellationToken.None)
      cancellationToken.Register((Action) (() => taskCompletionSource.TrySetCanceled()));
    cancellationToken.ThrowIfCancellationRequested();
    thread.Start();
    return (Task) taskCompletionSource.Task;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Task RunInThread<T1, T2, T3>(
    [NotNull] Action<T1, T2, T3> action,
    CancellationToken cancellationToken,
    [CanBeNull] T1 param1,
    [CanBeNull] T2 param2,
    [CanBeNull] T3 param3,
    [CanBeNull, InstantHandle] Action<Thread> initThreadAction = null)
  {
    return Helper.RunInThread((Action<CancellationToken>) (_ => action(param1, param2, param3)), cancellationToken, initThreadAction);
  }

  [NotNull]
  public static Task RunInThread<T1, T2, T3, T4>(
    [NotNull] Action<CancellationToken, T1, T2, T3, T4> action,
    CancellationToken cancellationToken,
    [CanBeNull] T1 param1,
    [CanBeNull] T2 param2,
    [CanBeNull] T3 param3,
    [CanBeNull] T4 param4,
    [CanBeNull, InstantHandle] Action<Thread> initThreadAction = null)
  {
    cancellationToken.ThrowIfCancellationRequested();
    TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
    Thread thread = new Thread((ThreadStart) (() =>
    {
      try
      {
        if (cancellationToken.IsCancellationRequested)
        {
          taskCompletionSource.TrySetCanceled();
        }
        else
        {
          action(cancellationToken, param1, param2, param3, param4);
          if (cancellationToken.IsCancellationRequested)
            taskCompletionSource.TrySetCanceled();
          else
            taskCompletionSource.TrySetResult(true);
        }
      }
      catch (Exception ex)
      {
        if (cancellationToken.IsCancellationRequested)
          taskCompletionSource.TrySetCanceled();
        else
          taskCompletionSource.TrySetException(ex);
      }
    }));
    if (initThreadAction != null)
      initThreadAction(thread);
    if (cancellationToken != CancellationToken.None)
      cancellationToken.Register((Action) (() => taskCompletionSource.TrySetCanceled()));
    cancellationToken.ThrowIfCancellationRequested();
    thread.Start();
    return (Task) taskCompletionSource.Task;
  }

  [NotNull]
  public static Task RunInThread<T1, T2, T3, T4>(
    [NotNull] Action<T1, T2, T3, T4> action,
    CancellationToken cancellationToken,
    [CanBeNull] T1 param1,
    [CanBeNull] T2 param2,
    [CanBeNull] T3 param3,
    [CanBeNull] T4 param4,
    [CanBeNull, InstantHandle] Action<Thread> initThreadAction = null)
  {
    return Helper.RunInThread((Action<CancellationToken>) (_ => action(param1, param2, param3, param4)), cancellationToken, initThreadAction);
  }

  [NotNull]
  public static Task RunInThread<T1, T2, T3, T4, T5>(
    [NotNull] Action<CancellationToken, T1, T2, T3, T4, T5> action,
    CancellationToken cancellationToken,
    [CanBeNull] T1 param1,
    [CanBeNull] T2 param2,
    [CanBeNull] T3 param3,
    [CanBeNull] T4 param4,
    [CanBeNull] T5 param5,
    [CanBeNull, InstantHandle] Action<Thread> initThreadAction = null)
  {
    cancellationToken.ThrowIfCancellationRequested();
    TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
    Thread thread = new Thread((ThreadStart) (() =>
    {
      try
      {
        if (cancellationToken.IsCancellationRequested)
        {
          taskCompletionSource.TrySetCanceled();
        }
        else
        {
          action(cancellationToken, param1, param2, param3, param4, param5);
          if (cancellationToken.IsCancellationRequested)
            taskCompletionSource.TrySetCanceled();
          else
            taskCompletionSource.TrySetResult(true);
        }
      }
      catch (Exception ex)
      {
        if (cancellationToken.IsCancellationRequested)
          taskCompletionSource.TrySetCanceled();
        else
          taskCompletionSource.TrySetException(ex);
      }
    }));
    if (initThreadAction != null)
      initThreadAction(thread);
    if (cancellationToken != CancellationToken.None)
      cancellationToken.Register((Action) (() => taskCompletionSource.TrySetCanceled()));
    cancellationToken.ThrowIfCancellationRequested();
    thread.Start();
    return (Task) taskCompletionSource.Task;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Task RunInThread<T1, T2, T3, T4, T5>(
    [NotNull] Action<T1, T2, T3, T4, T5> action,
    CancellationToken cancellationToken,
    [CanBeNull] T1 param1,
    [CanBeNull] T2 param2,
    [CanBeNull] T3 param3,
    [CanBeNull] T4 param4,
    [CanBeNull] T5 param5,
    [CanBeNull, InstantHandle] Action<Thread> initThreadAction = null)
  {
    return Helper.RunInThread((Action<CancellationToken>) (_ => action(param1, param2, param3, param4, param5)), cancellationToken, initThreadAction);
  }

  [NotNull]
  public static Task RunInThread<T1, T2, T3, T4, T5, T6>(
    [NotNull] Action<CancellationToken, T1, T2, T3, T4, T5, T6> action,
    CancellationToken cancellationToken,
    [CanBeNull] T1 param1,
    [CanBeNull] T2 param2,
    [CanBeNull] T3 param3,
    [CanBeNull] T4 param4,
    [CanBeNull] T5 param5,
    [CanBeNull] T6 param6,
    [CanBeNull, InstantHandle] Action<Thread> initThreadAction = null)
  {
    cancellationToken.ThrowIfCancellationRequested();
    TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
    Thread thread = new Thread((ThreadStart) (() =>
    {
      try
      {
        if (cancellationToken.IsCancellationRequested)
        {
          taskCompletionSource.TrySetCanceled();
        }
        else
        {
          action(cancellationToken, param1, param2, param3, param4, param5, param6);
          if (cancellationToken.IsCancellationRequested)
            taskCompletionSource.TrySetCanceled();
          else
            taskCompletionSource.TrySetResult(true);
        }
      }
      catch (Exception ex)
      {
        if (cancellationToken.IsCancellationRequested)
          taskCompletionSource.TrySetCanceled();
        else
          taskCompletionSource.TrySetException(ex);
      }
    }));
    if (initThreadAction != null)
      initThreadAction(thread);
    if (cancellationToken != CancellationToken.None)
      cancellationToken.Register((Action) (() => taskCompletionSource.TrySetCanceled()));
    cancellationToken.ThrowIfCancellationRequested();
    thread.Start();
    return (Task) taskCompletionSource.Task;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Task RunInThread<T1, T2, T3, T4, T5, T6>(
    [NotNull] Action<T1, T2, T3, T4, T5, T6> action,
    CancellationToken cancellationToken,
    [CanBeNull] T1 param1,
    [CanBeNull] T2 param2,
    [CanBeNull] T3 param3,
    [CanBeNull] T4 param4,
    [CanBeNull] T5 param5,
    [CanBeNull] T6 param6,
    [CanBeNull, InstantHandle] Action<Thread> initThreadAction = null)
  {
    return Helper.RunInThread((Action<CancellationToken>) (_ => action(param1, param2, param3, param4, param5, param6)), cancellationToken, initThreadAction);
  }

  [NotNull]
  public static Task RunInThread<T1, T2, T3, T4, T5, T6, T7>(
    [NotNull] Action<CancellationToken, T1, T2, T3, T4, T5, T6, T7> action,
    CancellationToken cancellationToken,
    [CanBeNull] T1 param1,
    [CanBeNull] T2 param2,
    [CanBeNull] T3 param3,
    [CanBeNull] T4 param4,
    [CanBeNull] T5 param5,
    [CanBeNull] T6 param6,
    [CanBeNull] T7 param7,
    [CanBeNull, InstantHandle] Action<Thread> initThreadAction = null)
  {
    cancellationToken.ThrowIfCancellationRequested();
    TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
    Thread thread = new Thread((ThreadStart) (() =>
    {
      try
      {
        if (cancellationToken.IsCancellationRequested)
        {
          taskCompletionSource.TrySetCanceled();
        }
        else
        {
          action(cancellationToken, param1, param2, param3, param4, param5, param6, param7);
          if (cancellationToken.IsCancellationRequested)
            taskCompletionSource.TrySetCanceled();
          else
            taskCompletionSource.TrySetResult(true);
        }
      }
      catch (Exception ex)
      {
        if (cancellationToken.IsCancellationRequested)
          taskCompletionSource.TrySetCanceled();
        else
          taskCompletionSource.TrySetException(ex);
      }
    }));
    if (initThreadAction != null)
      initThreadAction(thread);
    if (cancellationToken != CancellationToken.None)
      cancellationToken.Register((Action) (() => taskCompletionSource.TrySetCanceled()));
    cancellationToken.ThrowIfCancellationRequested();
    thread.Start();
    return (Task) taskCompletionSource.Task;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Task RunInThread<T1, T2, T3, T4, T5, T6, T7>(
    [NotNull] Action<T1, T2, T3, T4, T5, T6, T7> action,
    CancellationToken cancellationToken,
    [CanBeNull] T1 param1,
    [CanBeNull] T2 param2,
    [CanBeNull] T3 param3,
    [CanBeNull] T4 param4,
    [CanBeNull] T5 param5,
    [CanBeNull] T6 param6,
    [CanBeNull] T7 param7,
    [CanBeNull, InstantHandle] Action<Thread> initThreadAction = null)
  {
    return Helper.RunInThread((Action<CancellationToken>) (_ => action(param1, param2, param3, param4, param5, param6, param7)), cancellationToken, initThreadAction);
  }

  [NotNull]
  public static Task RunInThread<T1, T2, T3, T4, T5, T6, T7, T8>(
    [NotNull] Action<CancellationToken, T1, T2, T3, T4, T5, T6, T7, T8> action,
    CancellationToken cancellationToken,
    [CanBeNull] T1 param1,
    [CanBeNull] T2 param2,
    [CanBeNull] T3 param3,
    [CanBeNull] T4 param4,
    [CanBeNull] T5 param5,
    [CanBeNull] T6 param6,
    [CanBeNull] T7 param7,
    [CanBeNull] T8 param8,
    [CanBeNull, InstantHandle] Action<Thread> initThreadAction = null)
  {
    cancellationToken.ThrowIfCancellationRequested();
    TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
    Thread thread = new Thread((ThreadStart) (() =>
    {
      try
      {
        if (cancellationToken.IsCancellationRequested)
        {
          taskCompletionSource.TrySetCanceled();
        }
        else
        {
          action(cancellationToken, param1, param2, param3, param4, param5, param6, param7, param8);
          if (cancellationToken.IsCancellationRequested)
            taskCompletionSource.TrySetCanceled();
          else
            taskCompletionSource.TrySetResult(true);
        }
      }
      catch (Exception ex)
      {
        if (cancellationToken.IsCancellationRequested)
          taskCompletionSource.TrySetCanceled();
        else
          taskCompletionSource.TrySetException(ex);
      }
    }));
    if (initThreadAction != null)
      initThreadAction(thread);
    if (cancellationToken != CancellationToken.None)
      cancellationToken.Register((Action) (() => taskCompletionSource.TrySetCanceled()));
    cancellationToken.ThrowIfCancellationRequested();
    thread.Start();
    return (Task) taskCompletionSource.Task;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Task RunInThread<T1, T2, T3, T4, T5, T6, T7, T8>(
    [NotNull] Action<T1, T2, T3, T4, T5, T6, T7, T8> action,
    CancellationToken cancellationToken,
    [CanBeNull] T1 param1,
    [CanBeNull] T2 param2,
    [CanBeNull] T3 param3,
    [CanBeNull] T4 param4,
    [CanBeNull] T5 param5,
    [CanBeNull] T6 param6,
    [CanBeNull] T7 param7,
    [CanBeNull] T8 param8,
    [CanBeNull, InstantHandle] Action<Thread> initThreadAction = null)
  {
    return Helper.RunInThread((Action<CancellationToken>) (_ => action(param1, param2, param3, param4, param5, param6, param7, param8)), cancellationToken, initThreadAction);
  }

  [NotNull]
  [ItemCanBeNull]
  public static Task<TResult> RunInThread<TResult>(
    [NotNull] Func<CancellationToken, TResult> function,
    CancellationToken cancellationToken,
    [CanBeNull, InstantHandle] Action<Thread> initThreadAction = null)
  {
    cancellationToken.ThrowIfCancellationRequested();
    TaskCompletionSource<TResult> taskCompletionSource = new TaskCompletionSource<TResult>();
    Thread thread = new Thread((ThreadStart) (() =>
    {
      try
      {
        if (cancellationToken.IsCancellationRequested)
        {
          taskCompletionSource.TrySetCanceled();
        }
        else
        {
          TResult result = function(cancellationToken);
          if (cancellationToken.IsCancellationRequested)
            taskCompletionSource.TrySetCanceled();
          else
            taskCompletionSource.TrySetResult(result);
        }
      }
      catch (Exception ex)
      {
        if (cancellationToken.IsCancellationRequested)
          taskCompletionSource.TrySetCanceled();
        else
          taskCompletionSource.TrySetException(ex);
      }
    }));
    if (initThreadAction != null)
      initThreadAction(thread);
    if (cancellationToken != CancellationToken.None)
      cancellationToken.Register((Action) (() => taskCompletionSource.TrySetCanceled()));
    cancellationToken.ThrowIfCancellationRequested();
    thread.Start();
    return taskCompletionSource.Task;
  }

  [NotNull]
  [ItemCanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Task<TResult> RunInThread<TResult>(
    [NotNull] Func<TResult> function,
    CancellationToken cancellationToken,
    [CanBeNull, InstantHandle] Action<Thread> initThreadAction = null)
  {
    return Helper.RunInThread<TResult>((Func<CancellationToken, TResult>) (_ => function()), cancellationToken, initThreadAction);
  }

  [NotNull]
  [ItemCanBeNull]
  public static Task<TResult> RunInThread<T, TResult>(
    [NotNull] Func<CancellationToken, T, TResult> function,
    CancellationToken cancellationToken,
    [CanBeNull] T param1,
    [CanBeNull, InstantHandle] Action<Thread> initThreadAction = null)
  {
    cancellationToken.ThrowIfCancellationRequested();
    TaskCompletionSource<TResult> taskCompletionSource = new TaskCompletionSource<TResult>();
    Thread thread = new Thread((ThreadStart) (() =>
    {
      try
      {
        if (cancellationToken.IsCancellationRequested)
        {
          taskCompletionSource.TrySetCanceled();
        }
        else
        {
          TResult result = function(cancellationToken, param1);
          if (cancellationToken.IsCancellationRequested)
            taskCompletionSource.TrySetCanceled();
          else
            taskCompletionSource.TrySetResult(result);
        }
      }
      catch (Exception ex)
      {
        if (cancellationToken.IsCancellationRequested)
          taskCompletionSource.TrySetCanceled();
        else
          taskCompletionSource.TrySetException(ex);
      }
    }));
    if (initThreadAction != null)
      initThreadAction(thread);
    if (cancellationToken != CancellationToken.None)
      cancellationToken.Register((Action) (() => taskCompletionSource.TrySetCanceled()));
    cancellationToken.ThrowIfCancellationRequested();
    thread.Start();
    return taskCompletionSource.Task;
  }

  [NotNull]
  [ItemCanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Task<TResult> RunInThread<T, TResult>(
    [NotNull] Func<T, TResult> function,
    CancellationToken cancellationToken,
    [CanBeNull] T param1,
    [CanBeNull, InstantHandle] Action<Thread> initThreadAction = null)
  {
    return Helper.RunInThread<TResult>((Func<CancellationToken, TResult>) (_ => function(param1)), cancellationToken, initThreadAction);
  }

  [NotNull]
  [ItemCanBeNull]
  public static Task<TResult> RunInThread<T1, T2, TResult>(
    [NotNull] Func<CancellationToken, T1, T2, TResult> function,
    CancellationToken cancellationToken,
    [CanBeNull] T1 param1,
    [CanBeNull] T2 param2,
    [CanBeNull, InstantHandle] Action<Thread> initThreadAction = null)
  {
    cancellationToken.ThrowIfCancellationRequested();
    TaskCompletionSource<TResult> taskCompletionSource = new TaskCompletionSource<TResult>();
    Thread thread = new Thread((ThreadStart) (() =>
    {
      try
      {
        if (cancellationToken.IsCancellationRequested)
        {
          taskCompletionSource.TrySetCanceled();
        }
        else
        {
          TResult result = function(cancellationToken, param1, param2);
          if (cancellationToken.IsCancellationRequested)
            taskCompletionSource.TrySetCanceled();
          else
            taskCompletionSource.TrySetResult(result);
        }
      }
      catch (Exception ex)
      {
        if (cancellationToken.IsCancellationRequested)
          taskCompletionSource.TrySetCanceled();
        else
          taskCompletionSource.TrySetException(ex);
      }
    }));
    if (initThreadAction != null)
      initThreadAction(thread);
    if (cancellationToken != CancellationToken.None)
      cancellationToken.Register((Action) (() => taskCompletionSource.TrySetCanceled()));
    cancellationToken.ThrowIfCancellationRequested();
    thread.Start();
    return taskCompletionSource.Task;
  }

  [NotNull]
  [ItemCanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Task<TResult> RunInThread<T1, T2, TResult>(
    [NotNull] Func<T1, T2, TResult> function,
    CancellationToken cancellationToken,
    [CanBeNull] T1 param1,
    [CanBeNull] T2 param2,
    [CanBeNull, InstantHandle] Action<Thread> initThreadAction = null)
  {
    return Helper.RunInThread<TResult>((Func<CancellationToken, TResult>) (_ => function(param1, param2)), cancellationToken, initThreadAction);
  }

  [NotNull]
  [ItemCanBeNull]
  public static Task<TResult> RunInThread<T1, T2, T3, TResult>(
    [NotNull] Func<CancellationToken, T1, T2, T3, TResult> function,
    CancellationToken cancellationToken,
    [CanBeNull] T1 param1,
    [CanBeNull] T2 param2,
    [CanBeNull] T3 param3,
    [CanBeNull, InstantHandle] Action<Thread> initThreadAction = null)
  {
    cancellationToken.ThrowIfCancellationRequested();
    TaskCompletionSource<TResult> taskCompletionSource = new TaskCompletionSource<TResult>();
    Thread thread = new Thread((ThreadStart) (() =>
    {
      try
      {
        if (cancellationToken.IsCancellationRequested)
        {
          taskCompletionSource.TrySetCanceled();
        }
        else
        {
          TResult result = function(cancellationToken, param1, param2, param3);
          if (cancellationToken.IsCancellationRequested)
            taskCompletionSource.TrySetCanceled();
          else
            taskCompletionSource.TrySetResult(result);
        }
      }
      catch (Exception ex)
      {
        if (cancellationToken.IsCancellationRequested)
          taskCompletionSource.TrySetCanceled();
        else
          taskCompletionSource.TrySetException(ex);
      }
    }));
    if (initThreadAction != null)
      initThreadAction(thread);
    if (cancellationToken != CancellationToken.None)
      cancellationToken.Register((Action) (() => taskCompletionSource.TrySetCanceled()));
    cancellationToken.ThrowIfCancellationRequested();
    thread.Start();
    return taskCompletionSource.Task;
  }

  [NotNull]
  [ItemCanBeNull]
  public static Task<TResult> RunInThread<T1, T2, T3, TResult>(
    [NotNull] Func<T1, T2, T3, TResult> function,
    CancellationToken cancellationToken,
    [CanBeNull] T1 param1,
    [CanBeNull] T2 param2,
    [CanBeNull] T3 param3,
    [CanBeNull, InstantHandle] Action<Thread> initThreadAction = null)
  {
    return Helper.RunInThread<TResult>((Func<CancellationToken, TResult>) (_ => function(param1, param2, param3)), cancellationToken, initThreadAction);
  }

  [NotNull]
  [ItemCanBeNull]
  public static Task<TResult> RunInThread<T1, T2, T3, T4, TResult>(
    [NotNull] Func<CancellationToken, T1, T2, T3, T4, TResult> function,
    CancellationToken cancellationToken,
    [CanBeNull] T1 param1,
    [CanBeNull] T2 param2,
    [CanBeNull] T3 param3,
    [CanBeNull] T4 param4,
    [CanBeNull, InstantHandle] Action<Thread> initThreadAction = null)
  {
    cancellationToken.ThrowIfCancellationRequested();
    TaskCompletionSource<TResult> taskCompletionSource = new TaskCompletionSource<TResult>();
    Thread thread = new Thread((ThreadStart) (() =>
    {
      try
      {
        if (cancellationToken.IsCancellationRequested)
        {
          taskCompletionSource.TrySetCanceled();
        }
        else
        {
          TResult result = function(cancellationToken, param1, param2, param3, param4);
          if (cancellationToken.IsCancellationRequested)
            taskCompletionSource.TrySetCanceled();
          else
            taskCompletionSource.TrySetResult(result);
        }
      }
      catch (Exception ex)
      {
        if (cancellationToken.IsCancellationRequested)
          taskCompletionSource.TrySetCanceled();
        else
          taskCompletionSource.TrySetException(ex);
      }
    }));
    if (initThreadAction != null)
      initThreadAction(thread);
    if (cancellationToken != CancellationToken.None)
      cancellationToken.Register((Action) (() => taskCompletionSource.TrySetCanceled()));
    cancellationToken.ThrowIfCancellationRequested();
    thread.Start();
    return taskCompletionSource.Task;
  }

  [NotNull]
  [ItemCanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Task<TResult> RunInThread<T1, T2, T3, T4, TResult>(
    [NotNull] Func<T1, T2, T3, T4, TResult> function,
    CancellationToken cancellationToken,
    [CanBeNull] T1 param1,
    [CanBeNull] T2 param2,
    [CanBeNull] T3 param3,
    [CanBeNull] T4 param4,
    [CanBeNull, InstantHandle] Action<Thread> initThreadAction = null)
  {
    return Helper.RunInThread<TResult>((Func<CancellationToken, TResult>) (_ => function(param1, param2, param3, param4)), cancellationToken, initThreadAction);
  }

  [NotNull]
  [ItemCanBeNull]
  public static Task<TResult> RunInThread<T1, T2, T3, T4, T5, TResult>(
    [NotNull] Func<CancellationToken, T1, T2, T3, T4, T5, TResult> function,
    CancellationToken cancellationToken,
    [CanBeNull] T1 param1,
    [CanBeNull] T2 param2,
    [CanBeNull] T3 param3,
    [CanBeNull] T4 param4,
    [CanBeNull] T5 param5,
    [CanBeNull, InstantHandle] Action<Thread> initThreadAction = null)
  {
    cancellationToken.ThrowIfCancellationRequested();
    TaskCompletionSource<TResult> taskCompletionSource = new TaskCompletionSource<TResult>();
    Thread thread = new Thread((ThreadStart) (() =>
    {
      try
      {
        if (cancellationToken.IsCancellationRequested)
        {
          taskCompletionSource.TrySetCanceled();
        }
        else
        {
          TResult result = function(cancellationToken, param1, param2, param3, param4, param5);
          if (cancellationToken.IsCancellationRequested)
            taskCompletionSource.TrySetCanceled();
          else
            taskCompletionSource.TrySetResult(result);
        }
      }
      catch (Exception ex)
      {
        if (cancellationToken.IsCancellationRequested)
          taskCompletionSource.TrySetCanceled();
        else
          taskCompletionSource.TrySetException(ex);
      }
    }));
    if (initThreadAction != null)
      initThreadAction(thread);
    if (cancellationToken != CancellationToken.None)
      cancellationToken.Register((Action) (() => taskCompletionSource.TrySetCanceled()));
    cancellationToken.ThrowIfCancellationRequested();
    thread.Start();
    return taskCompletionSource.Task;
  }

  [NotNull]
  [ItemCanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Task<TResult> RunInThread<T1, T2, T3, T4, T5, TResult>(
    [NotNull] Func<T1, T2, T3, T4, T5, TResult> function,
    CancellationToken cancellationToken,
    [CanBeNull] T1 param1,
    [CanBeNull] T2 param2,
    [CanBeNull] T3 param3,
    [CanBeNull] T4 param4,
    [CanBeNull] T5 param5,
    [CanBeNull, InstantHandle] Action<Thread> initThreadAction = null)
  {
    return Helper.RunInThread<TResult>((Func<CancellationToken, TResult>) (_ => function(param1, param2, param3, param4, param5)), cancellationToken, initThreadAction);
  }

  [NotNull]
  [ItemCanBeNull]
  public static Task<TResult> RunInThread<T1, T2, T3, T4, T5, T6, TResult>(
    [NotNull] Func<CancellationToken, T1, T2, T3, T4, T5, T6, TResult> function,
    CancellationToken cancellationToken,
    [CanBeNull] T1 param1,
    [CanBeNull] T2 param2,
    [CanBeNull] T3 param3,
    [CanBeNull] T4 param4,
    [CanBeNull] T5 param5,
    [CanBeNull] T6 param6,
    [CanBeNull, InstantHandle] Action<Thread> initThreadAction = null)
  {
    cancellationToken.ThrowIfCancellationRequested();
    TaskCompletionSource<TResult> taskCompletionSource = new TaskCompletionSource<TResult>();
    Thread thread = new Thread((ThreadStart) (() =>
    {
      try
      {
        if (cancellationToken.IsCancellationRequested)
        {
          taskCompletionSource.TrySetCanceled();
        }
        else
        {
          TResult result = function(cancellationToken, param1, param2, param3, param4, param5, param6);
          if (cancellationToken.IsCancellationRequested)
            taskCompletionSource.TrySetCanceled();
          else
            taskCompletionSource.TrySetResult(result);
        }
      }
      catch (Exception ex)
      {
        if (cancellationToken.IsCancellationRequested)
          taskCompletionSource.TrySetCanceled();
        else
          taskCompletionSource.TrySetException(ex);
      }
    }));
    if (initThreadAction != null)
      initThreadAction(thread);
    if (cancellationToken != CancellationToken.None)
      cancellationToken.Register((Action) (() => taskCompletionSource.TrySetCanceled()));
    cancellationToken.ThrowIfCancellationRequested();
    thread.Start();
    return taskCompletionSource.Task;
  }

  [NotNull]
  [ItemCanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Task<TResult> RunInThread<T1, T2, T3, T4, T5, T6, TResult>(
    [NotNull] Func<T1, T2, T3, T4, T5, T6, TResult> function,
    CancellationToken cancellationToken,
    [CanBeNull] T1 param1,
    [CanBeNull] T2 param2,
    [CanBeNull] T3 param3,
    [CanBeNull] T4 param4,
    [CanBeNull] T5 param5,
    [CanBeNull] T6 param6,
    [CanBeNull, InstantHandle] Action<Thread> initThreadAction = null)
  {
    return Helper.RunInThread<TResult>((Func<CancellationToken, TResult>) (_ => function(param1, param2, param3, param4, param5, param6)), cancellationToken, initThreadAction);
  }

  [NotNull]
  [ItemCanBeNull]
  public static Task<TResult> RunInThread<T1, T2, T3, T4, T5, T6, T7, TResult>(
    [NotNull] Func<CancellationToken, T1, T2, T3, T4, T5, T6, T7, TResult> function,
    CancellationToken cancellationToken,
    [CanBeNull] T1 param1,
    [CanBeNull] T2 param2,
    [CanBeNull] T3 param3,
    [CanBeNull] T4 param4,
    [CanBeNull] T5 param5,
    [CanBeNull] T6 param6,
    [CanBeNull] T7 param7,
    [CanBeNull, InstantHandle] Action<Thread> initThreadAction = null)
  {
    cancellationToken.ThrowIfCancellationRequested();
    TaskCompletionSource<TResult> taskCompletionSource = new TaskCompletionSource<TResult>();
    Thread thread = new Thread((ThreadStart) (() =>
    {
      try
      {
        if (cancellationToken.IsCancellationRequested)
        {
          taskCompletionSource.TrySetCanceled();
        }
        else
        {
          TResult result = function(cancellationToken, param1, param2, param3, param4, param5, param6, param7);
          if (cancellationToken.IsCancellationRequested)
            taskCompletionSource.TrySetCanceled();
          else
            taskCompletionSource.TrySetResult(result);
        }
      }
      catch (Exception ex)
      {
        if (cancellationToken.IsCancellationRequested)
          taskCompletionSource.TrySetCanceled();
        else
          taskCompletionSource.TrySetException(ex);
      }
    }));
    if (initThreadAction != null)
      initThreadAction(thread);
    if (cancellationToken != CancellationToken.None)
      cancellationToken.Register((Action) (() => taskCompletionSource.TrySetCanceled()));
    cancellationToken.ThrowIfCancellationRequested();
    thread.Start();
    return taskCompletionSource.Task;
  }

  [NotNull]
  [ItemCanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Task<TResult> RunInThread<T1, T2, T3, T4, T5, T6, T7, TResult>(
    [NotNull] Func<T1, T2, T3, T4, T5, T6, T7, TResult> function,
    CancellationToken cancellationToken,
    [CanBeNull] T1 param1,
    [CanBeNull] T2 param2,
    [CanBeNull] T3 param3,
    [CanBeNull] T4 param4,
    [CanBeNull] T5 param5,
    [CanBeNull] T6 param6,
    [CanBeNull] T7 param7,
    [CanBeNull, InstantHandle] Action<Thread> initThreadAction = null)
  {
    return Helper.RunInThread<TResult>((Func<CancellationToken, TResult>) (_ => function(param1, param2, param3, param4, param5, param6, param7)), cancellationToken, initThreadAction);
  }

  [NotNull]
  [ItemCanBeNull]
  public static Task<TResult> RunInThread<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(
    [NotNull] Func<CancellationToken, T1, T2, T3, T4, T5, T6, T7, T8, TResult> function,
    CancellationToken cancellationToken,
    [CanBeNull] T1 param1,
    [CanBeNull] T2 param2,
    [CanBeNull] T3 param3,
    [CanBeNull] T4 param4,
    [CanBeNull] T5 param5,
    [CanBeNull] T6 param6,
    [CanBeNull] T7 param7,
    [CanBeNull] T8 param8,
    [CanBeNull, InstantHandle] Action<Thread> initThreadAction = null)
  {
    cancellationToken.ThrowIfCancellationRequested();
    TaskCompletionSource<TResult> taskCompletionSource = new TaskCompletionSource<TResult>();
    Thread thread = new Thread((ThreadStart) (() =>
    {
      try
      {
        if (cancellationToken.IsCancellationRequested)
        {
          taskCompletionSource.TrySetCanceled();
        }
        else
        {
          TResult result = function(cancellationToken, param1, param2, param3, param4, param5, param6, param7, param8);
          if (cancellationToken.IsCancellationRequested)
            taskCompletionSource.TrySetCanceled();
          else
            taskCompletionSource.TrySetResult(result);
        }
      }
      catch (Exception ex)
      {
        if (cancellationToken.IsCancellationRequested)
          taskCompletionSource.TrySetCanceled();
        else
          taskCompletionSource.TrySetException(ex);
      }
    }));
    if (initThreadAction != null)
      initThreadAction(thread);
    if (cancellationToken != CancellationToken.None)
      cancellationToken.Register((Action) (() => taskCompletionSource.TrySetCanceled()));
    cancellationToken.ThrowIfCancellationRequested();
    thread.Start();
    return taskCompletionSource.Task;
  }

  [NotNull]
  [ItemCanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Task<TResult> RunInThread<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(
    [NotNull] Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> function,
    CancellationToken cancellationToken,
    [CanBeNull] T1 param1,
    [CanBeNull] T2 param2,
    [CanBeNull] T3 param3,
    [CanBeNull] T4 param4,
    [CanBeNull] T5 param5,
    [CanBeNull] T6 param6,
    [CanBeNull] T7 param7,
    [CanBeNull] T8 param8,
    [CanBeNull, InstantHandle] Action<Thread> initThreadAction = null)
  {
    return Helper.RunInThread<TResult>((Func<CancellationToken, TResult>) (_ => function(param1, param2, param3, param4, param5, param6, param7, param8)), cancellationToken, initThreadAction);
  }

  [ContractAnnotation("=> value:NotNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T GetOrInit<T>(ref T? value, [NotNull, InstantHandle] Func<T> initMethod) where T : struct
  {
    if (value.HasValue)
      return value.Value;
    T orInit = initMethod();
    value = new T?(orInit);
    return orInit;
  }

  [NotNull]
  [MustUseReturnValue]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IDisposable CallOnDispose([NotNull] Action finishAction)
  {
    return (IDisposable) new Intermech.Extensions.CallOnDispose(finishAction);
  }

  [NotNull]
  [MustUseReturnValue]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IDisposable StartFinish([NotNull, InstantHandle] Action startAction, [NotNull] Action finishAction)
  {
    return (IDisposable) new Intermech.Extensions.StartFinish(startAction, finishAction);
  }

  [NotNull]
  [MustUseReturnValue]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IDisposable Merge([NotNull, NotEmpty, ItemNotNull] params IDisposable[] disposables)
  {
    return (IDisposable) new DisposableExtensions.MergedDisposables((IEnumerable<IDisposable>) disposables);
  }

  [NotNull]
  [MustUseReturnValue]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyList<T> PossibleValuesOf<T>(bool includeZero = false) where T : struct, Enum
  {
    return EnumHelper.PossibleValues<T>(includeZero);
  }

  [NotNull]
  [MustUseReturnValue]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Container<T> Holder<T>([CanBeNull] T value, [CanBeNull] Action<T> finishAction) where T : class
  {
    return new Container<T>(value, finishAction);
  }

  [NotNull]
  [MustUseReturnValue]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ValueContainer<T> ValueHolder<T>(T value, [CanBeNull] Action<T> finishAction) where T : struct
  {
    return new ValueContainer<T>(value, finishAction);
  }

  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void IgnoreOperationCancelled([NotNull] Action action)
  {
    try
    {
      action();
    }
    catch (Exception ex)
    {
      Exception operationCancelled = ex.ExtractNotOperationCancelled();
      if (operationCancelled == null)
        return;
      if (operationCancelled != ex)
        throw operationCancelled;
      throw;
    }
  }

  [CanBeNull]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T IgnoreOperationCancelled<T>([NotNull] Func<T> func)
  {
    try
    {
      return func();
    }
    catch (Exception ex)
    {
      Exception operationCancelled = ex.ExtractNotOperationCancelled();
      if (operationCancelled != null)
      {
        if (operationCancelled != ex)
          throw operationCancelled;
        throw;
      }
    }
    return default (T);
  }

  [NotNull]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task IgnoreOperationCancelledAsync([NotNull] Task task, bool continueOnCapturedContext = true)
  {
    try
    {
      if (task.Status.Ended())
        return;
      await task.ConfigureAwait(continueOnCapturedContext);
    }
    catch (Exception ex)
    {
      Exception operationCancelled = ex.ExtractNotOperationCancelled();
      if (operationCancelled == null)
        return;
      if (operationCancelled != ex)
        throw operationCancelled;
      throw;
    }
  }

  [ItemCanBeNull]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T> IgnoreOperationCancelledAsync<T>(
    [NotNull] Task<T> task,
    bool continueOnCapturedContext = true)
  {
    try
    {
      if (!task.Status.Ended())
        return await task.ConfigureAwait(continueOnCapturedContext);
    }
    catch (Exception ex)
    {
      Exception operationCancelled = ex.ExtractNotOperationCancelled();
      if (operationCancelled != null)
      {
        if (operationCancelled != ex)
          throw operationCancelled;
        throw;
      }
    }
    return default (T);
  }

  [NotNull]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task IgnoreOperationCancelledAsync(
    [NotNull] Func<Task> taskConstructor,
    bool continueOnCapturedContext = true)
  {
    try
    {
      Task task = taskConstructor();
      if (task.Status.Ended())
        return;
      await task.ConfigureAwait(continueOnCapturedContext);
    }
    catch (Exception ex)
    {
      Exception operationCancelled = ex.ExtractNotOperationCancelled();
      if (operationCancelled == null)
        return;
      if (operationCancelled != ex)
        throw operationCancelled;
      throw;
    }
  }

  [ItemCanBeNull]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T> IgnoreOperationCancelledAsync<T>(
    [NotNull] Func<Task<T>> taskConstructor,
    bool continueOnCapturedContext = true)
  {
    try
    {
      Task<T> task = taskConstructor();
      if (!task.Status.Ended())
        return await task.ConfigureAwait(continueOnCapturedContext);
    }
    catch (Exception ex)
    {
      Exception operationCancelled = ex.ExtractNotOperationCancelled();
      if (operationCancelled != null)
      {
        if (operationCancelled != ex)
          throw operationCancelled;
        throw;
      }
    }
    return default (T);
  }

  [DebuggerHidden]
  public static void CancelTasksAndWait(
    [NotNull] CancellationTokenSource cancellationTokenSource,
    int millisecondsTimeout,
    [NotNull, NotEmpty] params Task[] tasks)
  {
    if (!cancellationTokenSource.IsCancellationRequested)
    {
      if (cancellationTokenSource.Token.CanBeCanceled)
      {
        try
        {
          cancellationTokenSource.Cancel();
        }
        catch (Exception ex)
        {
          Exception operationCancelled = ex.ExtractNotOperationCancelled();
          if (operationCancelled != null)
          {
            if (operationCancelled != ex)
              throw operationCancelled;
            throw;
          }
        }
      }
    }
    if (tasks.Length == 1)
    {
      Task task = tasks[0];
      if (task == null)
        return;
      if (!task.Status.NotEnded())
        return;
      try
      {
        task.Wait(millisecondsTimeout);
      }
      catch (Exception ex)
      {
        Exception operationCancelled = ex.ExtractNotOperationCancelled();
        if (operationCancelled == null)
          return;
        if (operationCancelled != ex)
          throw operationCancelled;
        throw;
      }
    }
    else
    {
      if (((IEnumerable<Task>) tasks).Where<Task>((Func<Task, bool>) (task => task != null && task.Status.NotEnded())).ToArray<Task>(tasks.Length).Length == 0)
        return;
      try
      {
        Task.WaitAll(tasks, millisecondsTimeout);
      }
      catch (Exception ex)
      {
        Exception operationCancelled = ex.ExtractNotOperationCancelled();
        if (operationCancelled == null)
          return;
        if (operationCancelled != ex)
          throw operationCancelled;
        throw;
      }
    }
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Task CancelTasksAsync(
    [NotNull] CancellationTokenSource cancellationTokenSource,
    [NotNull, NotEmpty] params Task[] tasks)
  {
    return Helper.CancelTasksAsync(cancellationTokenSource, true, tasks);
  }

  [DebuggerHidden]
  public static async Task CancelTasksAsync(
    [NotNull] CancellationTokenSource cancellationTokenSource,
    bool continueOnCapturedContext,
    [NotNull, NotEmpty] Task[] tasks)
  {
    if (!cancellationTokenSource.IsCancellationRequested)
    {
      if (cancellationTokenSource.Token.CanBeCanceled)
      {
        try
        {
          cancellationTokenSource.Cancel();
        }
        catch (Exception ex)
        {
          Exception operationCancelled = ex.ExtractNotOperationCancelled();
          if (operationCancelled != null)
          {
            if (operationCancelled != ex)
              throw operationCancelled;
            throw;
          }
        }
      }
    }
    if (tasks.Length == 1)
    {
      Task task = tasks[0];
      if (task == null)
        return;
      if (!task.Status.NotEnded())
        return;
      try
      {
        await task.ConfigureAwait(continueOnCapturedContext);
      }
      catch (Exception ex)
      {
        Exception operationCancelled = ex.ExtractNotOperationCancelled();
        if (operationCancelled == null)
          return;
        if (operationCancelled != ex)
          throw operationCancelled;
        throw;
      }
    }
    else
    {
      if (((IEnumerable<Task>) tasks).Where<Task>((Func<Task, bool>) (task => task != null && task.Status.NotEnded())).ToArray<Task>(tasks.Length).Length == 0)
        return;
      try
      {
        await Task.WhenAll(tasks).ConfigureAwait(continueOnCapturedContext);
      }
      catch (Exception ex)
      {
        Exception operationCancelled = ex.ExtractNotOperationCancelled();
        if (operationCancelled == null)
          return;
        if (operationCancelled != ex)
          throw operationCancelled;
        throw;
      }
    }
  }

  [CanBeNull]
  [DebuggerHidden]
  public static OperationCanceledException CatchOperationCancelled([NotNull] Action action)
  {
    try
    {
      action();
    }
    catch (Exception ex)
    {
      OperationCanceledException exceptionOfType = ex.GetExceptionOfType<OperationCanceledException>();
      if (exceptionOfType != null)
        return exceptionOfType;
      throw;
    }
    return (OperationCanceledException) null;
  }

  [DebuggerHidden]
  [ContractAnnotation("=> true, operationCanceledException: notnull; => false, operationCanceledException: null")]
  public static bool TryCatchOperationCancelled(
    [NotNull] Action action,
    out OperationCanceledException operationCanceledException)
  {
    try
    {
      action();
    }
    catch (Exception ex)
    {
      operationCanceledException = ex.GetExceptionOfType<OperationCanceledException>();
      if (operationCanceledException != null)
        return true;
      throw;
    }
    operationCanceledException = (OperationCanceledException) null;
    return false;
  }

  [ItemCanBeNull]
  [DebuggerHidden]
  public static async Task<OperationCanceledException> CatchOperationCancelledAsync(
    [NotNull] Task task,
    bool continueOnCapturedContext = true)
  {
    try
    {
      await task.ConfigureAwait(continueOnCapturedContext);
    }
    catch (Exception ex)
    {
      OperationCanceledException exceptionOfType = ex.GetExceptionOfType<OperationCanceledException>();
      if (exceptionOfType != null)
        return exceptionOfType;
      throw;
    }
    return (OperationCanceledException) null;
  }

  [DebuggerHidden]
  public static async Task<(T, OperationCanceledException)> CatchOperationCancelledAsync<T>(
    [NotNull] Task<T> task,
    bool continueOnCapturedContext = true)
  {
    T result = default (T);
    try
    {
      result = await task.ConfigureAwait(continueOnCapturedContext);
    }
    catch (Exception ex)
    {
      OperationCanceledException exceptionOfType = ex.GetExceptionOfType<OperationCanceledException>();
      if (exceptionOfType != null)
        return (result, exceptionOfType);
      throw;
    }
    return (result, (OperationCanceledException) null);
  }

  [ItemCanBeNull]
  [DebuggerHidden]
  public static async Task<OperationCanceledException> CatchOperationCancelledAsync(
    [NotNull] Func<Task> taskConstructor,
    bool continueOnCapturedContext = true)
  {
    try
    {
      await taskConstructor().ConfigureAwait(continueOnCapturedContext);
    }
    catch (Exception ex)
    {
      OperationCanceledException exceptionOfType = ex.GetExceptionOfType<OperationCanceledException>();
      if (exceptionOfType != null)
        return exceptionOfType;
      throw;
    }
    return (OperationCanceledException) null;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TInstance CreateInstance<TInstance>(bool onlyPublic = true)
  {
    return (TInstance) typeof (TInstance).GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, (Binder) null, Type.EmptyTypes, Array.Empty<ParameterModifier>()).Invoke(Array.Empty<object>());
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TInstance CreateInstance<TInstance, TParam1>([CanBeNull] TParam1 param1, bool onlyPublic = true)
  {
    return (TInstance) typeof (TInstance).GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, (Binder) null, new Type[1]
    {
      typeof (TParam1)
    }, Array.Empty<ParameterModifier>()).Invoke(new object[1]
    {
      (object) param1
    });
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TInstance CreateInstance<TInstance, TParam1, TParam2>(
    [CanBeNull] TParam1 param1,
    [CanBeNull] TParam2 param2,
    bool onlyPublic = true)
  {
    return (TInstance) typeof (TInstance).GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, (Binder) null, new Type[2]
    {
      typeof (TParam1),
      typeof (TParam2)
    }, Array.Empty<ParameterModifier>()).Invoke(new object[2]
    {
      (object) param1,
      (object) param2
    });
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TInstance CreateInstance<TInstance, TParam1, TParam2, TParam3>(
    [CanBeNull] TParam1 param1,
    [CanBeNull] TParam2 param2,
    [CanBeNull] TParam3 param3,
    bool onlyPublic = true)
  {
    return (TInstance) typeof (TInstance).GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, (Binder) null, new Type[3]
    {
      typeof (TParam1),
      typeof (TParam2),
      typeof (TParam3)
    }, Array.Empty<ParameterModifier>()).Invoke(new object[3]
    {
      (object) param1,
      (object) param2,
      (object) param3
    });
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TInstance CreateInstance<TInstance, TParam1, TParam2, TParam3, TParam4>(
    [CanBeNull] TParam1 param1,
    [CanBeNull] TParam2 param2,
    [CanBeNull] TParam3 param3,
    [CanBeNull] TParam4 param4,
    bool onlyPublic = true)
  {
    return (TInstance) typeof (TInstance).GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, (Binder) null, new Type[4]
    {
      typeof (TParam1),
      typeof (TParam2),
      typeof (TParam3),
      typeof (TParam4)
    }, Array.Empty<ParameterModifier>()).Invoke(new object[4]
    {
      (object) param1,
      (object) param2,
      (object) param3,
      (object) param4
    });
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TInstance CreateInstance<TInstance, TParam1, TParam2, TParam3, TParam4, TParam5>(
    [CanBeNull] TParam1 param1,
    [CanBeNull] TParam2 param2,
    [CanBeNull] TParam3 param3,
    [CanBeNull] TParam4 param4,
    [CanBeNull] TParam5 param5,
    bool onlyPublic = true)
  {
    return (TInstance) typeof (TInstance).GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, (Binder) null, new Type[5]
    {
      typeof (TParam1),
      typeof (TParam2),
      typeof (TParam3),
      typeof (TParam4),
      typeof (TParam5)
    }, Array.Empty<ParameterModifier>()).Invoke(new object[5]
    {
      (object) param1,
      (object) param2,
      (object) param3,
      (object) param4,
      (object) param5
    });
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TInstance CreateInstance<TInstance, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(
    [CanBeNull] TParam1 param1,
    [CanBeNull] TParam2 param2,
    [CanBeNull] TParam3 param3,
    [CanBeNull] TParam4 param4,
    [CanBeNull] TParam5 param5,
    [CanBeNull] TParam6 param6,
    bool onlyPublic = true)
  {
    return (TInstance) typeof (TInstance).GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, (Binder) null, new Type[6]
    {
      typeof (TParam1),
      typeof (TParam2),
      typeof (TParam3),
      typeof (TParam4),
      typeof (TParam5),
      typeof (TParam6)
    }, Array.Empty<ParameterModifier>()).Invoke(new object[6]
    {
      (object) param1,
      (object) param2,
      (object) param3,
      (object) param4,
      (object) param5,
      (object) param6
    });
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TInstance CreateInstance<TInstance, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(
    [CanBeNull] TParam1 param1,
    [CanBeNull] TParam2 param2,
    [CanBeNull] TParam3 param3,
    [CanBeNull] TParam4 param4,
    [CanBeNull] TParam5 param5,
    [CanBeNull] TParam6 param6,
    [CanBeNull] TParam7 param7,
    bool onlyPublic = true)
  {
    return (TInstance) typeof (TInstance).GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, (Binder) null, new Type[7]
    {
      typeof (TParam1),
      typeof (TParam2),
      typeof (TParam3),
      typeof (TParam4),
      typeof (TParam5),
      typeof (TParam6),
      typeof (TParam7)
    }, Array.Empty<ParameterModifier>()).Invoke(new object[7]
    {
      (object) param1,
      (object) param2,
      (object) param3,
      (object) param4,
      (object) param5,
      (object) param6,
      (object) param7
    });
  }
}
