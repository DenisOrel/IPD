// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.ReflectionHelper
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Reflection;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public sealed class ReflectionHelper : IEquatable<ReflectionHelper>
{
  public const BindingFlags BaseFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy | BindingFlags.ExactBinding;
  public BindingFlags BindingFlags;

  [NotNull]
  public Type ClassType { get; }

  [CanBeNull]
  public object Obj { get; }

  [NotNull]
  private static ReflectionHelper GetForInstance<TType>([NotNull] TType obj, BindingFlags bindingFlags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy | BindingFlags.ExactBinding)
  {
    return new ReflectionHelper((object) obj, typeof (TType), bindingFlags);
  }

  [NotNull]
  private static ReflectionHelper GetForStatic<TType>(BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy | BindingFlags.ExactBinding)
  {
    return new ReflectionHelper((object) null, typeof (TType), bindingFlags);
  }

  internal ReflectionHelper([CanBeNull] object obj, [NotNull] Type type, BindingFlags bindingFlags)
  {
    string errorText;
    this.BindingFlags = bindingFlags.TryValidateForUse(out errorText) ? bindingFlags : throw new ArgumentException(errorText, nameof (bindingFlags));
    this.Obj = obj;
    this.ClassType = type;
  }

  public bool Equals(ReflectionHelper other)
  {
    if (other == null)
      return false;
    if (this == other)
      return true;
    return this.BindingFlags == other.BindingFlags && this.ClassType == other.ClassType && object.Equals(this.Obj, other.Obj);
  }

  public override bool Equals(object obj)
  {
    if (obj == null)
      return false;
    if (this == obj)
      return true;
    return !(obj.GetType() != this.GetType()) && this.Equals((ReflectionHelper) obj);
  }

  public override int GetHashCode()
  {
    return (this.BindingFlags.GetHashCode() * 397 ^ this.ClassType.GetHashCode()) * 397 ^ (this.Obj != null ? this.Obj.GetHashCode() : 0);
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public TValue GetPropertyValue<TValue>([NotNull, NotWhitespace] string propertyName)
  {
    PropertyInfo property = this.ClassType.GetProperty(propertyName, this.BindingFlags & BindingFlags.GetProperty, (Binder) null, typeof (TValue), Array.Empty<Type>(), (ParameterModifier[]) null);
    return !(property == (PropertyInfo) null) ? (TValue) property.GetValue(this.Obj) : throw new InvalidOperationException($"{((this.BindingFlags & BindingFlags.Static) != BindingFlags.Default ? "Static" : "Instance")} property {propertyName} with type {typeof (TValue).Name} not found in {this.ClassType.Name}!");
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public TValue GetFieldValue<TValue>([NotNull, NotWhitespace] string fieldName)
  {
    FieldInfo field = this.ClassType.GetField(fieldName, this.BindingFlags & BindingFlags.GetField);
    return !(field == (FieldInfo) null) ? (TValue) field.GetValue(this.Obj) : throw new InvalidOperationException($"{((this.BindingFlags & BindingFlags.Static) != BindingFlags.Default ? "Static" : "Instance")} field {fieldName} with type {typeof (TValue).Name} not found in {this.ClassType.Name}!");
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public LazyPropertyReflection<TValue> GetLazyPropertyValue<TValue>(
    [NotNull, NotWhitespace] string propertyName,
    bool cacheValue = false)
  {
    return new LazyPropertyReflection<TValue>(this.Obj, this.ClassType, propertyName, this.BindingFlags & BindingFlags.GetProperty, cacheValue);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public LazyFieldReflection<TValue> GetLazyFieldValue<TValue>([NotNull, NotWhitespace] string fieldName, bool cacheValue = false)
  {
    return new LazyFieldReflection<TValue>(this.Obj, this.ClassType, fieldName, this.BindingFlags & BindingFlags.GetField, cacheValue);
  }
}
