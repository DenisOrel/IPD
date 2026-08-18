// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.Lazy2`1
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Threading;

#nullable disable
namespace Intermech.Extensions;

[ComVisible(false)]
[DebuggerTypeProxy(typeof (SystemLazy2DebugView<>))]
[DebuggerDisplay("ThreadSafetyMode={Mode}, IsValueCreated={IsValueCreated}, IsValueFaulted={IsValueFaulted}, Value={ValueForDebugDisplay}")]
[Serializable]
public class Lazy2<T>
{
  private static readonly Func<T> ALREADY_INVOKED_SENTINEL = (Func<T>) (() =>
  {
    Intermech.Diagnostics.Check.Assert(false, "ALREADY_INVOKED_SENTINEL should never be invoked.");
    return default (T);
  });
  private volatile object _boxed;
  [NonSerialized]
  private volatile Func<T> _valueFactory;
  [NonSerialized]
  private volatile object _threadSafeObj;
  internal static readonly object PublicationOnlySentinel = new object();

  public Lazy2()
    : this(LazyThreadSafetyMode.ExecutionAndPublication)
  {
  }

  public Lazy2([NotNull] Func<T> valueFactory)
    : this(valueFactory, LazyThreadSafetyMode.ExecutionAndPublication)
  {
  }

  public Lazy2(bool isThreadSafe)
    : this(isThreadSafe ? LazyThreadSafetyMode.ExecutionAndPublication : LazyThreadSafetyMode.None)
  {
  }

  public Lazy2(LazyThreadSafetyMode mode) => this._threadSafeObj = Lazy2<T>.GetObjectFromMode(mode);

  public Lazy2([NotNull] Func<T> valueFactory, bool isThreadSafe)
    : this(valueFactory, isThreadSafe ? LazyThreadSafetyMode.ExecutionAndPublication : LazyThreadSafetyMode.None)
  {
  }

  public Lazy2([NotNull] Func<T> valueFactory, LazyThreadSafetyMode mode)
  {
    this._threadSafeObj = Lazy2<T>.GetObjectFromMode(mode);
    this._valueFactory = valueFactory;
  }

  [CanBeNull]
  private static object GetObjectFromMode(LazyThreadSafetyMode mode)
  {
    if (mode == LazyThreadSafetyMode.ExecutionAndPublication)
      return new object();
    if (mode == LazyThreadSafetyMode.PublicationOnly)
      return Lazy2<T>.PublicationOnlySentinel;
    if (mode != LazyThreadSafetyMode.None)
      throw new ArgumentOutOfRangeException(nameof (mode));
    return (object) null;
  }

  [System.Runtime.Serialization.OnSerializing]
  private void OnSerializing(StreamingContext context)
  {
    T obj = this.Value;
  }

  public override string ToString()
  {
    if (!this.IsValueCreated)
      return "Value not created yet";
    T obj1 = this.Value;
    ref T local1 = ref obj1;
    string str;
    if ((object) default (T) == null)
    {
      T obj2 = local1;
      ref T local2 = ref obj2;
      if ((object) obj2 == null)
      {
        str = (string) null;
        goto label_6;
      }
      local1 = ref local2;
    }
    str = local1.ToString();
label_6:
    return str ?? string.Empty;
  }

  [CanBeNull]
  internal T ValueForDebugDisplay
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return !this.IsValueCreated ? default (T) : ((Lazy2<T>.Boxed) this._boxed)._Value;
    }
  }

  internal LazyThreadSafetyMode Mode
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      if (this._threadSafeObj == null)
        return LazyThreadSafetyMode.None;
      return this._threadSafeObj == Lazy2<T>.PublicationOnlySentinel ? LazyThreadSafetyMode.PublicationOnly : LazyThreadSafetyMode.ExecutionAndPublication;
    }
  }

  internal bool IsValueFaulted
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._boxed is Lazy2<T>.LazyInternalExceptionHolder;
    }
  }

  public bool IsValueCreated
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._boxed is Lazy2<T>.Boxed;
  }

  [CanBeNull]
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public T Value
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.GetValue();
  }

  [CanBeNull]
  public T GetValue([CanBeNull, InstantHandle] Func<T> valueFactory, bool forceThreadSafe)
  {
    if (forceThreadSafe)
    {
      object threadSafeObj = this._threadSafeObj;
      if (threadSafeObj == null || threadSafeObj == Lazy2<T>.PublicationOnlySentinel)
        Interlocked.CompareExchange(ref this._threadSafeObj, Lazy2<T>.GetObjectFromMode(LazyThreadSafetyMode.ExecutionAndPublication), threadSafeObj);
    }
    return this.GetValue(valueFactory);
  }

  [CanBeNull]
  public T GetValue([CanBeNull] Func<T> valueFactory = null, [CanBeNull] object threadSafeObj = null)
  {
    if (this._boxed != null)
    {
      if (this._boxed is Lazy2<T>.Boxed boxed)
        return boxed._Value;
      (this._boxed as Lazy2<T>.LazyInternalExceptionHolder)._ExceptionDispatchInfo.Throw();
    }
    return this.LazyInitValue(valueFactory, threadSafeObj ?? this._threadSafeObj);
  }

  [CanBeNull]
  private T LazyInitValue([CanBeNull] Func<T> valueFactory = null, [CanBeNull] object threadSafeObj = null)
  {
    LazyThreadSafetyMode mode = this.Mode;
    if (threadSafeObj == null && mode == LazyThreadSafetyMode.None)
    {
      boxed = this.CreateValue(valueFactory);
      this._boxed = (object) boxed;
    }
    else if (threadSafeObj == null && mode == LazyThreadSafetyMode.PublicationOnly)
    {
      boxed = this.CreateValue(valueFactory);
      if (boxed == null || Interlocked.CompareExchange(ref this._boxed, (object) boxed, (object) null) != null)
        boxed = (Lazy2<T>.Boxed) this._boxed;
      else
        this._valueFactory = Lazy2<T>.ALREADY_INVOKED_SENTINEL;
    }
    else
    {
      bool lockTaken = false;
      try
      {
        if (this._valueFactory != Lazy2<T>.ALREADY_INVOKED_SENTINEL)
        {
          if (threadSafeObj == null)
            threadSafeObj = this._threadSafeObj;
          Monitor.Enter(threadSafeObj, ref lockTaken);
        }
        else
          Intermech.Diagnostics.Check.NotNull<object>(this._boxed, "_boxed");
        if (this._boxed == null)
        {
          boxed = this.CreateValue(valueFactory);
          if (boxed == null || Interlocked.CompareExchange(ref this._boxed, (object) boxed, (object) null) != null)
            boxed = (Lazy2<T>.Boxed) this._boxed;
          else
            this._valueFactory = Lazy2<T>.ALREADY_INVOKED_SENTINEL;
        }
        else if (!(this._boxed is Lazy2<T>.Boxed boxed))
          (this._boxed as Lazy2<T>.LazyInternalExceptionHolder)._ExceptionDispatchInfo.Throw();
      }
      finally
      {
        if (lockTaken)
          Monitor.Exit(threadSafeObj);
      }
    }
    Intermech.Diagnostics.Check.NotNull<Lazy2<T>.Boxed>(boxed, "boxed");
    return boxed._Value;
  }

  [CanBeNull]
  private Lazy2<T>.Boxed CreateValue([CanBeNull] Func<T> valueFactory = null)
  {
    LazyThreadSafetyMode mode = this.Mode;
    Func<T> func1 = valueFactory ?? this._valueFactory;
    if (func1 != null)
    {
      try
      {
        if (mode != LazyThreadSafetyMode.PublicationOnly && this._valueFactory == Lazy2<T>.ALREADY_INVOKED_SENTINEL)
          throw new InvalidOperationException("Recursive calls to value");
        Func<T> func2 = func1;
        if (mode != LazyThreadSafetyMode.PublicationOnly)
          this._valueFactory = Lazy2<T>.ALREADY_INVOKED_SENTINEL;
        else if (func2 == Lazy2<T>.ALREADY_INVOKED_SENTINEL)
          return (Lazy2<T>.Boxed) null;
        return new Lazy2<T>.Boxed(func2());
      }
      catch (Exception ex)
      {
        if (mode != LazyThreadSafetyMode.PublicationOnly)
          this._boxed = (object) new Lazy2<T>.LazyInternalExceptionHolder(ex);
        throw;
      }
    }
    else
    {
      try
      {
        return new Lazy2<T>.Boxed((T) Activator.CreateInstance(typeof (T)));
      }
      catch (MissingMethodException ex)
      {
        Exception exception = (Exception) new MissingMemberException($"Class {typeof (T)} has not paramless constructor!");
        if (mode != LazyThreadSafetyMode.PublicationOnly)
          this._boxed = (object) new Lazy2<T>.LazyInternalExceptionHolder(exception);
        throw exception;
      }
    }
  }

  [Serializable]
  private class Boxed
  {
    [CanBeNull]
    internal readonly T _Value;

    internal Boxed([CanBeNull] T value) => this._Value = value;
  }

  private class LazyInternalExceptionHolder
  {
    [NotNull]
    internal readonly ExceptionDispatchInfo _ExceptionDispatchInfo;

    internal LazyInternalExceptionHolder([NotNull] Exception exception)
    {
      this._ExceptionDispatchInfo = ExceptionDispatchInfo.Capture(exception);
    }
  }
}
