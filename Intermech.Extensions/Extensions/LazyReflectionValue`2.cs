// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.LazyReflectionValue`2
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Reflection;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public abstract class LazyReflectionValue<TMemberInfo, TValue> : 
  IEquatable<LazyReflectionValue<TMemberInfo, TValue>>
  where TMemberInfo : System.Reflection.MemberInfo
{
  public readonly bool CacheValue;
  private TMemberInfo _memberInfo;
  [CanBeNull]
  private TValue _cachedValue;

  private static bool ValueIsReference { get; } = typeof (TValue).IsByRef;

  [CanBeNull]
  public object Obj { get; }

  [NotNull]
  protected Type ClassType { get; }

  protected LazyReflectionValue(
    [CanBeNull] object obj,
    [NotNull] Type classType,
    [NotNull, NotWhitespace] string valueName,
    BindingFlags bindingFlags,
    bool cacheValue = false)
  {
    string errorText;
    if (!bindingFlags.TryValidateForUse(out errorText))
      throw new ArgumentException(errorText, nameof (valueName));
    this.Obj = obj;
    this.ClassType = classType;
    this.ValueName = valueName;
    this.CacheValue = cacheValue;
    this.BindingFlags = bindingFlags;
  }

  public BindingFlags BindingFlags { get; }

  protected bool ValueLoaded { get; private set; }

  public void RefreshValue() => this.ValueLoaded = false;

  [NotNull]
  [NotWhitespace]
  public string ValueName { get; }

  [NotNull]
  public TMemberInfo MemberInfo
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      if ((System.Reflection.MemberInfo) this._memberInfo != (System.Reflection.MemberInfo) null)
        return this._memberInfo;
      this._memberInfo = this.GetMemberInfo();
      return this._memberInfo;
    }
  }

  [NotNull]
  protected abstract TMemberInfo GetMemberInfo();

  [CanBeNull]
  public TValue Value
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      if (!this.CacheValue)
        return this.GetValue();
      if (!this.ValueLoaded)
      {
        this._cachedValue = this.GetValue();
        this.ValueLoaded = true;
      }
      return this._cachedValue;
    }
  }

  protected abstract TValue GetValue();

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static explicit operator TValue(
    [NotNull] LazyReflectionValue<TMemberInfo, TValue> reflectionValue)
  {
    return reflectionValue.Value;
  }

  public override bool Equals([CanBeNull] object other)
  {
    if (this == other)
      return true;
    if (other == null)
      return LazyReflectionValue<TMemberInfo, TValue>.ValueIsReference && (object) this.Value == null;
    if (other.GetType() == this.GetType())
      return this.Equals((LazyReflectionValue<TMemberInfo, TValue>) other);
    return other is TValue objB && object.Equals((object) this.Value, (object) objB);
  }

  public override int GetHashCode()
  {
    return (((this.Obj != null ? this.Obj.GetHashCode() : 0) * 397 ^ this.CacheValue.GetHashCode()) * 397 ^ this.BindingFlags.GetHashCode()) * 397 ^ StringComparer.InvariantCulture.GetHashCode(this.ValueName);
  }

  public override string ToString()
  {
    TValue obj = this.Value;
    return LazyReflectionValue<TMemberInfo, TValue>.ValueIsReference && (object) obj == null ? "NULL" : obj.ToString();
  }

  public bool Equals([CanBeNull] LazyReflectionValue<TMemberInfo, TValue> other)
  {
    if (other == null)
      return false;
    if (this == other)
      return true;
    return object.Equals(this.Obj, other.Obj) && this.BindingFlags == other.BindingFlags && this.CacheValue == other.CacheValue && string.Equals(this.ValueName, other.ValueName, StringComparison.Ordinal);
  }
}
