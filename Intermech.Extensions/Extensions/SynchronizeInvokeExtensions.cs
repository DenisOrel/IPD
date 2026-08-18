// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.SynchronizeInvokeExtensions
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public static class SynchronizeInvokeExtensions
{
  [Pure]
  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void Invoke([CanBeNull] this ISynchronizeInvoke invoker, [NotNull, InstantHandle] Action method)
  {
    if (invoker != null && invoker.InvokeRequired)
      invoker.Invoke((Delegate) method, Array.Empty<object>());
    else
      method();
  }

  [Pure]
  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void Invoke<T>([CanBeNull] this ISynchronizeInvoke invoker, [NotNull, InstantHandle] Action<T> method, [CanBeNull] T arg1)
  {
    if (invoker != null && invoker.InvokeRequired)
      invoker.Invoke((Delegate) method, new object[1]
      {
        (object) arg1
      });
    else
      method(arg1);
  }

  [Pure]
  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void Invoke<T1, T2>(
    [CanBeNull] this ISynchronizeInvoke invoker,
    [NotNull, InstantHandle] Action<T1, T2> method,
    [CanBeNull] T1 arg1,
    [CanBeNull] T2 arg2)
  {
    if (invoker != null && invoker.InvokeRequired)
      invoker.Invoke((Delegate) method, new object[2]
      {
        (object) arg1,
        (object) arg2
      });
    else
      method(arg1, arg2);
  }

  [Pure]
  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void Invoke<T1, T2, T3>(
    [CanBeNull] this ISynchronizeInvoke invoker,
    [NotNull, InstantHandle] Action<T1, T2, T3> method,
    [CanBeNull] T1 arg1,
    [CanBeNull] T2 arg2,
    [CanBeNull] T3 arg3)
  {
    if (invoker != null && invoker.InvokeRequired)
      invoker.Invoke((Delegate) method, new object[3]
      {
        (object) arg1,
        (object) arg2,
        (object) arg3
      });
    else
      method(arg1, arg2, arg3);
  }

  [Pure]
  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void Invoke<T1, T2, T3, T4>(
    [CanBeNull] this ISynchronizeInvoke invoker,
    [NotNull, InstantHandle] Action<T1, T2, T3, T4> method,
    [CanBeNull] T1 arg1,
    [CanBeNull] T2 arg2,
    [CanBeNull] T3 arg3,
    [CanBeNull] T4 arg4)
  {
    if (invoker != null && invoker.InvokeRequired)
      invoker.Invoke((Delegate) method, new object[4]
      {
        (object) arg1,
        (object) arg2,
        (object) arg3,
        (object) arg4
      });
    else
      method(arg1, arg2, arg3, arg4);
  }

  [Pure]
  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void Invoke<T1, T2, T3, T4, T5>(
    [CanBeNull] this ISynchronizeInvoke invoker,
    [NotNull, InstantHandle] Action<T1, T2, T3, T4, T5> method,
    [CanBeNull] T1 arg1,
    [CanBeNull] T2 arg2,
    [CanBeNull] T3 arg3,
    [CanBeNull] T4 arg4,
    [CanBeNull] T5 arg5)
  {
    if (invoker != null && invoker.InvokeRequired)
      invoker.Invoke((Delegate) method, new object[5]
      {
        (object) arg1,
        (object) arg2,
        (object) arg3,
        (object) arg4,
        (object) arg5
      });
    else
      method(arg1, arg2, arg3, arg4, arg5);
  }

  [Pure]
  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void Invoke<T1, T2, T3, T4, T5, T6>(
    [CanBeNull] this ISynchronizeInvoke invoker,
    [NotNull, InstantHandle] Action<T1, T2, T3, T4, T5, T6> method,
    [CanBeNull] T1 arg1,
    [CanBeNull] T2 arg2,
    [CanBeNull] T3 arg3,
    [CanBeNull] T4 arg4,
    [CanBeNull] T5 arg5,
    [CanBeNull] T6 arg6)
  {
    if (invoker != null && invoker.InvokeRequired)
      invoker.Invoke((Delegate) method, new object[6]
      {
        (object) arg1,
        (object) arg2,
        (object) arg3,
        (object) arg4,
        (object) arg5,
        (object) arg6
      });
    else
      method(arg1, arg2, arg3, arg4, arg5, arg6);
  }

  [Pure]
  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void Invoke<T1, T2, T3, T4, T5, T6, T7>(
    [CanBeNull] this ISynchronizeInvoke invoker,
    [NotNull, InstantHandle] Action<T1, T2, T3, T4, T5, T6, T7> method,
    [CanBeNull] T1 arg1,
    [CanBeNull] T2 arg2,
    [CanBeNull] T3 arg3,
    [CanBeNull] T4 arg4,
    [CanBeNull] T5 arg5,
    [CanBeNull] T6 arg6,
    [CanBeNull] T7 arg7)
  {
    if (invoker != null && invoker.InvokeRequired)
      invoker.Invoke((Delegate) method, new object[7]
      {
        (object) arg1,
        (object) arg2,
        (object) arg3,
        (object) arg4,
        (object) arg5,
        (object) arg6,
        (object) arg7
      });
    else
      method(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
  }

  [Pure]
  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void Invoke<T1, T2, T3, T4, T5, T6, T7, T8>(
    [CanBeNull] this ISynchronizeInvoke invoker,
    [NotNull, InstantHandle] Action<T1, T2, T3, T4, T5, T6, T7, T8> method,
    [CanBeNull] T1 arg1,
    [CanBeNull] T2 arg2,
    [CanBeNull] T3 arg3,
    [CanBeNull] T4 arg4,
    [CanBeNull] T5 arg5,
    [CanBeNull] T6 arg6,
    [CanBeNull] T7 arg7,
    [CanBeNull] T8 arg8)
  {
    if (invoker != null && invoker.InvokeRequired)
      invoker.Invoke((Delegate) method, new object[8]
      {
        (object) arg1,
        (object) arg2,
        (object) arg3,
        (object) arg4,
        (object) arg5,
        (object) arg6,
        (object) arg7,
        (object) arg8
      });
    else
      method(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
  }

  [Pure]
  [DebuggerStepThrough]
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TResult Invoke<TResult>([CanBeNull] this ISynchronizeInvoke invoker, [NotNull, InstantHandle] Func<TResult> method)
  {
    return invoker != null && invoker.InvokeRequired ? (TResult) invoker.Invoke((Delegate) method, Array.Empty<object>()) : method();
  }

  [Pure]
  [DebuggerStepThrough]
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TResult Invoke<TResult, T>(
    [CanBeNull] this ISynchronizeInvoke invoker,
    [NotNull, InstantHandle] Func<T, TResult> method,
    [CanBeNull] T arg1)
  {
    if (invoker == null || !invoker.InvokeRequired)
      return method(arg1);
    return (TResult) invoker.Invoke((Delegate) method, new object[1]
    {
      (object) arg1
    });
  }

  [Pure]
  [DebuggerStepThrough]
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TResult Invoke<TResult, T1, T2>(
    [CanBeNull] this ISynchronizeInvoke invoker,
    [NotNull, InstantHandle] Func<T1, T2, TResult> method,
    [CanBeNull] T1 arg1,
    [CanBeNull] T2 arg2)
  {
    if (invoker == null || !invoker.InvokeRequired)
      return method(arg1, arg2);
    return (TResult) invoker.Invoke((Delegate) method, new object[2]
    {
      (object) arg1,
      (object) arg2
    });
  }

  [Pure]
  [DebuggerStepThrough]
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TResult Invoke<TResult, T1, T2, T3>(
    [CanBeNull] this ISynchronizeInvoke invoker,
    [NotNull, InstantHandle] Func<T1, T2, T3, TResult> method,
    [CanBeNull] T1 arg1,
    [CanBeNull] T2 arg2,
    [CanBeNull] T3 arg3)
  {
    if (invoker == null || !invoker.InvokeRequired)
      return method(arg1, arg2, arg3);
    return (TResult) invoker.Invoke((Delegate) method, new object[3]
    {
      (object) arg1,
      (object) arg2,
      (object) arg3
    });
  }

  [Pure]
  [DebuggerStepThrough]
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TResult Invoke<TResult, T1, T2, T3, T4>(
    [CanBeNull] this ISynchronizeInvoke invoker,
    [NotNull, InstantHandle] Func<T1, T2, T3, T4, TResult> method,
    [CanBeNull] T1 arg1,
    [CanBeNull] T2 arg2,
    [CanBeNull] T3 arg3,
    [CanBeNull] T4 arg4)
  {
    if (invoker == null || !invoker.InvokeRequired)
      return method(arg1, arg2, arg3, arg4);
    return (TResult) invoker.Invoke((Delegate) method, new object[4]
    {
      (object) arg1,
      (object) arg2,
      (object) arg3,
      (object) arg4
    });
  }

  [Pure]
  [DebuggerStepThrough]
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TResult Invoke<TResult, T1, T2, T3, T4, T5>(
    [CanBeNull] this ISynchronizeInvoke invoker,
    [NotNull, InstantHandle] Func<T1, T2, T3, T4, T5, TResult> method,
    [CanBeNull] T1 arg1,
    [CanBeNull] T2 arg2,
    [CanBeNull] T3 arg3,
    [CanBeNull] T4 arg4,
    [CanBeNull] T5 arg5)
  {
    if (invoker == null || !invoker.InvokeRequired)
      return method(arg1, arg2, arg3, arg4, arg5);
    return (TResult) invoker.Invoke((Delegate) method, new object[5]
    {
      (object) arg1,
      (object) arg2,
      (object) arg3,
      (object) arg4,
      (object) arg5
    });
  }

  [Pure]
  [DebuggerStepThrough]
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TResult Invoke<TResult, T1, T2, T3, T4, T5, T6>(
    [CanBeNull] this ISynchronizeInvoke invoker,
    [NotNull, InstantHandle] Func<T1, T2, T3, T4, T5, T6, TResult> method,
    [CanBeNull] T1 arg1,
    [CanBeNull] T2 arg2,
    [CanBeNull] T3 arg3,
    [CanBeNull] T4 arg4,
    [CanBeNull] T5 arg5,
    [CanBeNull] T6 arg6)
  {
    if (invoker == null || !invoker.InvokeRequired)
      return method(arg1, arg2, arg3, arg4, arg5, arg6);
    return (TResult) invoker.Invoke((Delegate) method, new object[6]
    {
      (object) arg1,
      (object) arg2,
      (object) arg3,
      (object) arg4,
      (object) arg5,
      (object) arg6
    });
  }

  [Pure]
  [DebuggerStepThrough]
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TResult Invoke<TResult, T1, T2, T3, T4, T5, T6, T7>(
    [CanBeNull] this ISynchronizeInvoke invoker,
    [NotNull, InstantHandle] Func<T1, T2, T3, T4, T5, T6, T7, TResult> method,
    [CanBeNull] T1 arg1,
    [CanBeNull] T2 arg2,
    [CanBeNull] T3 arg3,
    [CanBeNull] T4 arg4,
    [CanBeNull] T5 arg5,
    [CanBeNull] T6 arg6,
    [CanBeNull] T7 arg7)
  {
    if (invoker == null || !invoker.InvokeRequired)
      return method(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
    return (TResult) invoker.Invoke((Delegate) method, new object[7]
    {
      (object) arg1,
      (object) arg2,
      (object) arg3,
      (object) arg4,
      (object) arg5,
      (object) arg6,
      (object) arg7
    });
  }

  [Pure]
  [DebuggerStepThrough]
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TResult Invoke<TResult, T1, T2, T3, T4, T5, T6, T7, T8>(
    [CanBeNull] this ISynchronizeInvoke invoker,
    [NotNull, InstantHandle] Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> method,
    [CanBeNull] T1 arg1,
    [CanBeNull] T2 arg2,
    [CanBeNull] T3 arg3,
    [CanBeNull] T4 arg4,
    [CanBeNull] T5 arg5,
    [CanBeNull] T6 arg6,
    [CanBeNull] T7 arg7,
    [CanBeNull] T8 arg8)
  {
    if (invoker == null || !invoker.InvokeRequired)
      return method(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
    return (TResult) invoker.Invoke((Delegate) method, new object[8]
    {
      (object) arg1,
      (object) arg2,
      (object) arg3,
      (object) arg4,
      (object) arg5,
      (object) arg6,
      (object) arg7,
      (object) arg8
    });
  }
}
