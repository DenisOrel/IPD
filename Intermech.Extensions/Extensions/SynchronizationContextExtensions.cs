// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.SynchronizationContextExtensions
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Async;
using Intermech.Diagnostics;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

#nullable disable
namespace Intermech.Extensions;

public static class SynchronizationContextExtensions
{
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static SynchronizationContextAwaiter GetAwaiter([NotNull] this SynchronizationContext context)
  {
    return new SynchronizationContextAwaiter(context);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void Post([CanBeNull] this SynchronizationContext context, [NotNull] Action action)
  {
    if (context == null)
      action();
    else
      context.Post((SendOrPostCallback) (_ => action()), (object) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void Post<T1>([CanBeNull] this SynchronizationContext context, [NotNull] Action<T1> action, [CanBeNull] T1 param1)
  {
    if (context == null)
      action(param1);
    else
      context.Post((SendOrPostCallback) (state => action(((Tuple<T1>) state).Item1)), (object) new Tuple<T1>(param1));
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void Post<T1, T2>(
    [CanBeNull] this SynchronizationContext context,
    [NotNull] Action<T1, T2> action,
    [CanBeNull] T1 param1,
    [CanBeNull] T2 param2)
  {
    if (context == null)
      action(param1, param2);
    else
      context.Post((SendOrPostCallback) (state =>
      {
        Tuple<T1, T2> tuple = (Tuple<T1, T2>) state;
        action(tuple.Item1, tuple.Item2);
      }), (object) new Tuple<T1, T2>(param1, param2));
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void Post<T1, T2, T3>(
    [CanBeNull] this SynchronizationContext context,
    [NotNull] Action<T1, T2, T3> action,
    [CanBeNull] T1 param1,
    [CanBeNull] T2 param2,
    [CanBeNull] T3 param3)
  {
    if (context == null)
      action(param1, param2, param3);
    else
      context.Post((SendOrPostCallback) (state =>
      {
        Tuple<T1, T2, T3> tuple = (Tuple<T1, T2, T3>) state;
        action(tuple.Item1, tuple.Item2, tuple.Item3);
      }), (object) new Tuple<T1, T2, T3>(param1, param2, param3));
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void Post<T1, T2, T3, T4>(
    [CanBeNull] this SynchronizationContext context,
    [NotNull] Action<T1, T2, T3, T4> action,
    [CanBeNull] T1 param1,
    [CanBeNull] T2 param2,
    [CanBeNull] T3 param3,
    [CanBeNull] T4 param4)
  {
    if (context == null)
      action(param1, param2, param3, param4);
    else
      context.Post((SendOrPostCallback) (state =>
      {
        Tuple<T1, T2, T3, T4> tuple = (Tuple<T1, T2, T3, T4>) state;
        action(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4);
      }), (object) new Tuple<T1, T2, T3, T4>(param1, param2, param3, param4));
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void Post<T1, T2, T3, T4, T5>(
    [CanBeNull] this SynchronizationContext context,
    [NotNull] Action<T1, T2, T3, T4, T5> action,
    [CanBeNull] T1 param1,
    [CanBeNull] T2 param2,
    [CanBeNull] T3 param3,
    [CanBeNull] T4 param4,
    [CanBeNull] T5 param5)
  {
    if (context == null)
      action(param1, param2, param3, param4, param5);
    else
      context.Post((SendOrPostCallback) (state =>
      {
        Tuple<T1, T2, T3, T4, T5> tuple = (Tuple<T1, T2, T3, T4, T5>) state;
        action(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5);
      }), (object) new Tuple<T1, T2, T3, T4, T5>(param1, param2, param3, param4, param5));
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void Post<T1, T2, T3, T4, T5, T6>(
    [CanBeNull] this SynchronizationContext context,
    [NotNull] Action<T1, T2, T3, T4, T5, T6> action,
    [CanBeNull] T1 param1,
    [CanBeNull] T2 param2,
    [CanBeNull] T3 param3,
    [CanBeNull] T4 param4,
    [CanBeNull] T5 param5,
    [CanBeNull] T6 param6)
  {
    if (context == null)
      action(param1, param2, param3, param4, param5, param6);
    else
      context.Post((SendOrPostCallback) (state =>
      {
        Tuple<T1, T2, T3, T4, T5, T6> tuple = (Tuple<T1, T2, T3, T4, T5, T6>) state;
        action(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5, tuple.Item6);
      }), (object) new Tuple<T1, T2, T3, T4, T5, T6>(param1, param2, param3, param4, param5, param6));
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void Post<T1, T2, T3, T4, T5, T6, T7>(
    [CanBeNull] this SynchronizationContext context,
    [NotNull] Action<T1, T2, T3, T4, T5, T6, T7> action,
    [CanBeNull] T1 param1,
    [CanBeNull] T2 param2,
    [CanBeNull] T3 param3,
    [CanBeNull] T4 param4,
    [CanBeNull] T5 param5,
    [CanBeNull] T6 param6,
    [CanBeNull] T7 param7)
  {
    if (context == null)
      action(param1, param2, param3, param4, param5, param6, param7);
    else
      context.Post((SendOrPostCallback) (state =>
      {
        Tuple<T1, T2, T3, T4, T5, T6, T7> tuple = (Tuple<T1, T2, T3, T4, T5, T6, T7>) state;
        action(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5, tuple.Item6, tuple.Item7);
      }), (object) new Tuple<T1, T2, T3, T4, T5, T6, T7>(param1, param2, param3, param4, param5, param6, param7));
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void Post<T1, T2, T3, T4, T5, T6, T7, T8>(
    [CanBeNull] this SynchronizationContext context,
    [NotNull] Action<T1, T2, T3, T4, T5, T6, T7, T8> action,
    [CanBeNull] T1 param1,
    [CanBeNull] T2 param2,
    [CanBeNull] T3 param3,
    [CanBeNull] T4 param4,
    [CanBeNull] T5 param5,
    [CanBeNull] T6 param6,
    [CanBeNull] T7 param7,
    [CanBeNull] T8 param8)
  {
    if (context == null)
      action(param1, param2, param3, param4, param5, param6, param7, param8);
    else
      context.Post((SendOrPostCallback) (state =>
      {
        Tuple<T1, T2, T3, T4, T5, T6, T7, T8> tuple = (Tuple<T1, T2, T3, T4, T5, T6, T7, T8>) state;
        action(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5, tuple.Item6, tuple.Item7, tuple.Rest);
      }), (object) new Tuple<T1, T2, T3, T4, T5, T6, T7, T8>(param1, param2, param3, param4, param5, param6, param7, param8));
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void Send([CanBeNull] this SynchronizationContext context, [NotNull, InstantHandle] Action action)
  {
    if (context == null)
      action();
    else
      context.Send((SendOrPostCallback) (_ => action()), (object) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void Send<T1>([CanBeNull] this SynchronizationContext context, [NotNull, InstantHandle] Action<T1> action, [CanBeNull] T1 param1)
  {
    if (context == null)
      action(param1);
    else
      context.Send((SendOrPostCallback) (state => action(((Tuple<T1>) state).Item1)), (object) new Tuple<T1>(param1));
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void Send<T1, T2>(
    [CanBeNull] this SynchronizationContext context,
    [NotNull, InstantHandle] Action<T1, T2> action,
    [CanBeNull] T1 param1,
    [CanBeNull] T2 param2)
  {
    if (context == null)
      action(param1, param2);
    else
      context.Send((SendOrPostCallback) (state =>
      {
        Tuple<T1, T2> tuple = (Tuple<T1, T2>) state;
        action(tuple.Item1, tuple.Item2);
      }), (object) new Tuple<T1, T2>(param1, param2));
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void Send<T1, T2, T3>(
    [CanBeNull] this SynchronizationContext context,
    [NotNull, InstantHandle] Action<T1, T2, T3> action,
    [CanBeNull] T1 param1,
    [CanBeNull] T2 param2,
    [CanBeNull] T3 param3)
  {
    if (context == null)
      action(param1, param2, param3);
    else
      context.Send((SendOrPostCallback) (state =>
      {
        Tuple<T1, T2, T3> tuple = (Tuple<T1, T2, T3>) state;
        action(tuple.Item1, tuple.Item2, tuple.Item3);
      }), (object) new Tuple<T1, T2, T3>(param1, param2, param3));
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void Send<T1, T2, T3, T4>(
    [CanBeNull] this SynchronizationContext context,
    [NotNull, InstantHandle] Action<T1, T2, T3, T4> action,
    [CanBeNull] T1 param1,
    [CanBeNull] T2 param2,
    [CanBeNull] T3 param3,
    [CanBeNull] T4 param4)
  {
    if (context == null)
      action(param1, param2, param3, param4);
    else
      context.Send((SendOrPostCallback) (state =>
      {
        Tuple<T1, T2, T3, T4> tuple = (Tuple<T1, T2, T3, T4>) state;
        action(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4);
      }), (object) new Tuple<T1, T2, T3, T4>(param1, param2, param3, param4));
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void Send<T1, T2, T3, T4, T5>(
    [CanBeNull] this SynchronizationContext context,
    [NotNull, InstantHandle] Action<T1, T2, T3, T4, T5> action,
    [CanBeNull] T1 param1,
    [CanBeNull] T2 param2,
    [CanBeNull] T3 param3,
    [CanBeNull] T4 param4,
    [CanBeNull] T5 param5)
  {
    if (context == null)
      action(param1, param2, param3, param4, param5);
    else
      context.Send((SendOrPostCallback) (state =>
      {
        Tuple<T1, T2, T3, T4, T5> tuple = (Tuple<T1, T2, T3, T4, T5>) state;
        action(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5);
      }), (object) new Tuple<T1, T2, T3, T4, T5>(param1, param2, param3, param4, param5));
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void Send<T1, T2, T3, T4, T5, T6>(
    [CanBeNull] this SynchronizationContext context,
    [NotNull, InstantHandle] Action<T1, T2, T3, T4, T5, T6> action,
    [CanBeNull] T1 param1,
    [CanBeNull] T2 param2,
    [CanBeNull] T3 param3,
    [CanBeNull] T4 param4,
    [CanBeNull] T5 param5,
    [CanBeNull] T6 param6)
  {
    if (context == null)
      action(param1, param2, param3, param4, param5, param6);
    else
      context.Send((SendOrPostCallback) (state =>
      {
        Tuple<T1, T2, T3, T4, T5, T6> tuple = (Tuple<T1, T2, T3, T4, T5, T6>) state;
        action(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5, tuple.Item6);
      }), (object) new Tuple<T1, T2, T3, T4, T5, T6>(param1, param2, param3, param4, param5, param6));
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void Send<T1, T2, T3, T4, T5, T6, T7>(
    [CanBeNull] this SynchronizationContext context,
    [NotNull, InstantHandle] Action<T1, T2, T3, T4, T5, T6, T7> action,
    [CanBeNull] T1 param1,
    [CanBeNull] T2 param2,
    [CanBeNull] T3 param3,
    [CanBeNull] T4 param4,
    [CanBeNull] T5 param5,
    [CanBeNull] T6 param6,
    [CanBeNull] T7 param7)
  {
    if (context == null)
      action(param1, param2, param3, param4, param5, param6, param7);
    else
      context.Send((SendOrPostCallback) (state =>
      {
        Tuple<T1, T2, T3, T4, T5, T6, T7> tuple = (Tuple<T1, T2, T3, T4, T5, T6, T7>) state;
        action(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5, tuple.Item6, tuple.Item7);
      }), (object) new Tuple<T1, T2, T3, T4, T5, T6, T7>(param1, param2, param3, param4, param5, param6, param7));
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void Send<T1, T2, T3, T4, T5, T6, T7, T8>(
    [CanBeNull] this SynchronizationContext context,
    [NotNull, InstantHandle] Action<T1, T2, T3, T4, T5, T6, T7, T8> action,
    [CanBeNull] T1 param1,
    [CanBeNull] T2 param2,
    [CanBeNull] T3 param3,
    [CanBeNull] T4 param4,
    [CanBeNull] T5 param5,
    [CanBeNull] T6 param6,
    [CanBeNull] T7 param7,
    [CanBeNull] T8 param8)
  {
    if (context == null)
      action(param1, param2, param3, param4, param5, param6, param7, param8);
    else
      context.Send((SendOrPostCallback) (state =>
      {
        Tuple<T1, T2, T3, T4, T5, T6, T7, T8> tuple = (Tuple<T1, T2, T3, T4, T5, T6, T7, T8>) state;
        action(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5, tuple.Item6, tuple.Item7, tuple.Rest);
      }), (object) new Tuple<T1, T2, T3, T4, T5, T6, T7, T8>(param1, param2, param3, param4, param5, param6, param7, param8));
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TResult Send<TResult>([CanBeNull] this SynchronizationContext context, [NotNull, InstantHandle] Func<TResult> function)
  {
    if (context == null)
      return function();
    TResult result = default (TResult);
    context.Send((SendOrPostCallback) (_ => result = function()), (object) null);
    return result;
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TResult Send<TResult, T1>(
    [CanBeNull] this SynchronizationContext context,
    [NotNull, InstantHandle] Func<T1, TResult> function,
    [CanBeNull] T1 param1)
  {
    if (context == null)
      return function(param1);
    TResult result = default (TResult);
    context.Send((SendOrPostCallback) (state => result = function(((Tuple<T1>) state).Item1)), (object) new Tuple<T1>(param1));
    return result;
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TResult Send<TResult, T1, T2>(
    [CanBeNull] this SynchronizationContext context,
    [NotNull, InstantHandle] Func<T1, T2, TResult> function,
    [CanBeNull] T1 param1,
    [CanBeNull] T2 param2)
  {
    if (context == null)
      return function(param1, param2);
    TResult result = default (TResult);
    context.Send((SendOrPostCallback) (state =>
    {
      Tuple<T1, T2> tuple = (Tuple<T1, T2>) state;
      result = function(tuple.Item1, tuple.Item2);
    }), (object) new Tuple<T1, T2>(param1, param2));
    return result;
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TResult Send<TResult, T1, T2, T3>(
    [CanBeNull] this SynchronizationContext context,
    [NotNull, InstantHandle] Func<T1, T2, T3, TResult> function,
    [CanBeNull] T1 param1,
    [CanBeNull] T2 param2,
    [CanBeNull] T3 param3)
  {
    if (context == null)
      return function(param1, param2, param3);
    TResult result = default (TResult);
    context.Send((SendOrPostCallback) (state =>
    {
      Tuple<T1, T2, T3> tuple = (Tuple<T1, T2, T3>) state;
      result = function(tuple.Item1, tuple.Item2, tuple.Item3);
    }), (object) new Tuple<T1, T2, T3>(param1, param2, param3));
    return result;
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TResult Send<TResult, T1, T2, T3, T4>(
    [CanBeNull] this SynchronizationContext context,
    [NotNull, InstantHandle] Func<T1, T2, T3, T4, TResult> function,
    [CanBeNull] T1 param1,
    [CanBeNull] T2 param2,
    [CanBeNull] T3 param3,
    [CanBeNull] T4 param4)
  {
    if (context == null)
      return function(param1, param2, param3, param4);
    TResult result = default (TResult);
    context.Send((SendOrPostCallback) (state =>
    {
      Tuple<T1, T2, T3, T4> tuple = (Tuple<T1, T2, T3, T4>) state;
      result = function(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4);
    }), (object) new Tuple<T1, T2, T3, T4>(param1, param2, param3, param4));
    return result;
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TResult Send<TResult, T1, T2, T3, T4, T5>(
    [CanBeNull] this SynchronizationContext context,
    [NotNull, InstantHandle] Func<T1, T2, T3, T4, T5, TResult> function,
    [CanBeNull] T1 param1,
    [CanBeNull] T2 param2,
    [CanBeNull] T3 param3,
    [CanBeNull] T4 param4,
    [CanBeNull] T5 param5)
  {
    if (context == null)
      return function(param1, param2, param3, param4, param5);
    TResult result = default (TResult);
    context.Send((SendOrPostCallback) (state =>
    {
      Tuple<T1, T2, T3, T4, T5> tuple = (Tuple<T1, T2, T3, T4, T5>) state;
      result = function(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5);
    }), (object) new Tuple<T1, T2, T3, T4, T5>(param1, param2, param3, param4, param5));
    return result;
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TResult Send<TResult, T1, T2, T3, T4, T5, T6>(
    [CanBeNull] this SynchronizationContext context,
    [NotNull, InstantHandle] Func<T1, T2, T3, T4, T5, T6, TResult> function,
    [CanBeNull] T1 param1,
    [CanBeNull] T2 param2,
    [CanBeNull] T3 param3,
    [CanBeNull] T4 param4,
    [CanBeNull] T5 param5,
    [CanBeNull] T6 param6)
  {
    if (context == null)
      return function(param1, param2, param3, param4, param5, param6);
    TResult result = default (TResult);
    context.Send((SendOrPostCallback) (state =>
    {
      Tuple<T1, T2, T3, T4, T5, T6> tuple = (Tuple<T1, T2, T3, T4, T5, T6>) state;
      result = function(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5, tuple.Item6);
    }), (object) new Tuple<T1, T2, T3, T4, T5, T6>(param1, param2, param3, param4, param5, param6));
    return result;
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TResult Send<TResult, T1, T2, T3, T4, T5, T6, T7>(
    [CanBeNull] this SynchronizationContext context,
    [NotNull, InstantHandle] Func<T1, T2, T3, T4, T5, T6, T7, TResult> function,
    [CanBeNull] T1 param1,
    [CanBeNull] T2 param2,
    [CanBeNull] T3 param3,
    [CanBeNull] T4 param4,
    [CanBeNull] T5 param5,
    [CanBeNull] T6 param6,
    [CanBeNull] T7 param7)
  {
    if (context == null)
      return function(param1, param2, param3, param4, param5, param6, param7);
    TResult result = default (TResult);
    context.Send((SendOrPostCallback) (state =>
    {
      Tuple<T1, T2, T3, T4, T5, T6, T7> tuple = (Tuple<T1, T2, T3, T4, T5, T6, T7>) state;
      result = function(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5, tuple.Item6, tuple.Item7);
    }), (object) new Tuple<T1, T2, T3, T4, T5, T6, T7>(param1, param2, param3, param4, param5, param6, param7));
    return result;
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TResult Send<TResult, T1, T2, T3, T4, T5, T6, T7, T8>(
    [CanBeNull] this SynchronizationContext context,
    [NotNull, InstantHandle] Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> function,
    [CanBeNull] T1 param1,
    [CanBeNull] T2 param2,
    [CanBeNull] T3 param3,
    [CanBeNull] T4 param4,
    [CanBeNull] T5 param5,
    [CanBeNull] T6 param6,
    [CanBeNull] T7 param7,
    [CanBeNull] T8 param8)
  {
    if (context == null)
      return function(param1, param2, param3, param4, param5, param6, param7, param8);
    TResult result = default (TResult);
    context.Send((SendOrPostCallback) (state =>
    {
      Tuple<T1, T2, T3, T4, T5, T6, T7, T8> tuple = (Tuple<T1, T2, T3, T4, T5, T6, T7, T8>) state;
      result = function(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5, tuple.Item6, tuple.Item7, tuple.Rest);
    }), (object) new Tuple<T1, T2, T3, T4, T5, T6, T7, T8>(param1, param2, param3, param4, param5, param6, param7, param8));
    return result;
  }
}
