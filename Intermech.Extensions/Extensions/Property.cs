// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.Property
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Threading;

#nullable disable
namespace Intermech.Extensions;

public static class Property
{
  [NotNull]
  [DebuggerStepThrough]
  [MustUseReturnValue]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IDisposable SaveValue<TValue>([NotNull] Expression<Func<TValue>> expression)
  {
    (object Object, PropertyInfo PropertyInfo) objectProperty = expression.GetObjectProperty<TValue>();
    return (IDisposable) new Property.PropertySavedValue<TValue>(objectProperty.Object, objectProperty.PropertyInfo);
  }

  [NotNull]
  [DebuggerStepThrough]
  [MustUseReturnValue]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IDisposable SaveValue<TValue>([NotNull] object owner, [NotNull] PropertyInfo propertyInfo)
  {
    return (IDisposable) new Property.PropertySavedValue<TValue>(owner, propertyInfo);
  }

  [NotNull]
  [DebuggerStepThrough]
  [MustUseReturnValue]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IDisposable SaveValue<TValue>([NotNull] object owner, [NotNull, NotWhitespace] string propertyName)
  {
    PropertyInfo property = owner.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
    return (IDisposable) new Property.PropertySavedValue<TValue>(owner, property);
  }

  [NotNull]
  [DebuggerStepThrough]
  [MustUseReturnValue]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IDisposable SetTempValue<TValue>(
    [NotNull] Expression<Func<TValue>> expression,
    [CanBeNull] TValue tempValue)
  {
    (object Object, PropertyInfo PropertyInfo) objectProperty = expression.GetObjectProperty<TValue>();
    return (IDisposable) new Property.PropertySavedValue<TValue>(objectProperty.Object, objectProperty.PropertyInfo, tempValue);
  }

  [NotNull]
  [DebuggerStepThrough]
  [MustUseReturnValue]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IDisposable SetTempValue<TValue>(
    [NotNull] object owner,
    [NotNull] PropertyInfo propertyInfo,
    [CanBeNull] TValue tempValue)
  {
    return (IDisposable) new Property.PropertySavedValue<TValue>(owner, propertyInfo, tempValue);
  }

  [NotNull]
  [DebuggerStepThrough]
  [MustUseReturnValue]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IDisposable SetTempValue<TValue>(
    [NotNull] object owner,
    [NotNull, NotWhitespace] string propertyName,
    [CanBeNull] TValue tempValue)
  {
    PropertyInfo property = owner.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
    return (IDisposable) new Property.PropertySavedValue<TValue>(owner, property, tempValue);
  }

  private class PropertySavedValue<TValue> : IDisposable
  {
    [NotNull]
    private readonly object _owner;
    [CanBeNull]
    private readonly TValue _savedValue;
    [CanBeNull]
    private readonly ISynchronizeInvoke _ownerSynchronizeInvoke;
    [CanBeNull]
    private readonly SynchronizationContext _synchronizationContext;
    [CanBeNull]
    private readonly Dispatcher _dispatcher;
    [NotNull]
    private readonly MethodInfo _getPropertyInfo;
    [NotNull]
    private readonly MethodInfo _setPropertyInfo;

    internal PropertySavedValue([NotNull] object owner, [NotNull] PropertyInfo propertyInfo)
    {
      this._owner = owner;
      this._ownerSynchronizeInvoke = owner as ISynchronizeInvoke;
      if (this._ownerSynchronizeInvoke == null)
      {
        this._synchronizationContext = SynchronizationContext.Current;
        if (this._synchronizationContext == null)
          this._dispatcher = Dispatcher.CurrentDispatcher;
      }
      this._getPropertyInfo = Intermech.Diagnostics.Check.NotNull<MethodInfo>(propertyInfo.GetGetMethod(true), message: "Property must have getter!");
      this._setPropertyInfo = Intermech.Diagnostics.Check.NotNull<MethodInfo>(propertyInfo.GetSetMethod(true), message: "Property must have setter!");
      this._savedValue = this._ownerSynchronizeInvoke.Invoke<TValue>((Func<TValue>) (() => (TValue) this._getPropertyInfo.Invoke(this._owner, Array.Empty<object>())));
    }

    internal PropertySavedValue([NotNull] object owner, [NotNull] PropertyInfo propertyInfo, [CanBeNull] TValue tempValue)
      : this(owner, propertyInfo)
    {
      if (object.Equals((object) this._savedValue, (object) tempValue))
        return;
      this._setPropertyInfo.Invoke(this._owner, new object[1]
      {
        (object) tempValue
      });
    }

    private void Restore()
    {
      if (this._owner == null || object.Equals((object) (TValue) this._getPropertyInfo.Invoke(this._owner, Array.Empty<object>()), (object) this._savedValue))
        return;
      this._setPropertyInfo.Invoke(this._owner, new object[1]
      {
        (object) this._savedValue
      });
    }

    public void Dispose()
    {
      if (this._ownerSynchronizeInvoke != null)
        this._ownerSynchronizeInvoke.Invoke(new Action(this.Restore));
      else if (this._synchronizationContext != null)
        this._synchronizationContext.Send(new Action(this.Restore));
      else
        this._dispatcher.TryInvoke(new Action(this.Restore));
    }
  }
}
