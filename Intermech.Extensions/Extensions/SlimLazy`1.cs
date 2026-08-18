// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.SlimLazy`1
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

#nullable disable
namespace Intermech.Extensions;

[ComVisible(false)]
[DebuggerTypeProxy(typeof (SystemSlimLazyDebugView<>))]
[DebuggerDisplay("IsValueCreated={IsValueCreated}, IsValueFaulted={IsValueFaulted}, Value={ValueForDebugDisplay}")]
[Serializable]
public class SlimLazy<T>
{
  private static readonly Func<T> ALREADY_INVOKED_SENTINEL = (Func<T>) (() =>
  {
    Intermech.Diagnostics.Check.Assert(false, "ALREADY_INVOKED_SENTINEL should never be invoked.");
    return default (T);
  });
  private T _value;
  private bool _valueCreated;
  private ExceptionDispatchInfo _exceptionDispatchInfo;
  [NonSerialized]
  private Func<T> _valueFactory;

  public SlimLazy()
  {
  }

  public SlimLazy([NotNull] Func<T> valueFactory) => this._valueFactory = valueFactory;

  [CanBeNull]
  [ContractAnnotation("=> lazy: NotNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T GetValue([CanBeNull] ref SlimLazy<T> lazy, [NotNull, InstantHandle] Func<T> valueFactory)
  {
    if (lazy == null)
      lazy = new SlimLazy<T>(valueFactory);
    return lazy.Value;
  }

  [System.Runtime.Serialization.OnSerializing]
  private void OnSerializing(StreamingContext context)
  {
    T obj = this.Value;
  }

  public override string ToString()
  {
    if (!this._valueCreated)
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
      return !this._valueCreated ? default (T) : this._value;
    }
  }

  public bool IsValueFaulted
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._exceptionDispatchInfo != null;
  }

  public bool IsValueCreated
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._valueCreated;
  }

  [CanBeNull]
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public T Value
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.GetValue();
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public T GetValue([CanBeNull, InstantHandle] Func<T> valueFactory = null)
  {
    if (this._exceptionDispatchInfo != null)
      this._exceptionDispatchInfo.Throw();
    if (!this._valueCreated)
    {
      this._valueFactory = !(this._valueFactory == SlimLazy<T>.ALREADY_INVOKED_SENTINEL) ? SlimLazy<T>.ALREADY_INVOKED_SENTINEL : throw new InvalidOperationException("Recursive calls to value");
      if (valueFactory == null)
        valueFactory = this._valueFactory;
      if (valueFactory != null)
      {
        try
        {
          this._value = valueFactory();
        }
        catch (Exception ex)
        {
          this._exceptionDispatchInfo = ExceptionDispatchInfo.Capture(ex);
          throw;
        }
      }
      else
      {
        try
        {
          this._value = (T) Activator.CreateInstance(typeof (T));
        }
        catch (MissingMethodException ex)
        {
          Exception source = (Exception) new MissingMemberException($"Class {typeof (T)} has not paramless constructor!");
          this._exceptionDispatchInfo = ExceptionDispatchInfo.Capture(source);
          throw source;
        }
      }
      this._valueCreated = true;
    }
    return this._value;
  }
}
