// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.TypeExtensions
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public static class TypeExtensions
{
  [NotNull]
  [ItemNotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<PropertyInfo> GetPropWithAttribute<TAttribute>([NotNull] this Type type) where TAttribute : Attribute
  {
    return ((IEnumerable<PropertyInfo>) type.GetProperties()).Where<PropertyInfo>((Func<PropertyInfo, bool>) (prop => prop.HasAttribute<TAttribute>()));
  }

  [NotNull]
  [ItemNotEmpty]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<(PropertyInfo Property, TAttribute Attribute)> GetPropAndAttribute<TAttribute>(
    [NotNull] this Type type)
    where TAttribute : Attribute
  {
    TAttribute attribute;
    return ((IEnumerable<PropertyInfo>) type.GetProperties()).Select<PropertyInfo, (PropertyInfo, TAttribute)?>((Func<PropertyInfo, (PropertyInfo, TAttribute)?>) (prop => !prop.TryGetAttribute<TAttribute>(out attribute) ? new (PropertyInfo, TAttribute)?() : new (PropertyInfo, TAttribute)?((prop, attribute)))).NotNull<(PropertyInfo, TAttribute)>();
  }

  [NotNull]
  [ItemNotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<FieldInfo> GetFieldWithAttribute<TAttribute>(
    [NotNull] this Type type,
    BindingFlags bindingFlags = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.ExactBinding)
    where TAttribute : Attribute
  {
    return ((IEnumerable<FieldInfo>) type.GetFields(bindingFlags)).Where<FieldInfo>((Func<FieldInfo, bool>) (prop => prop.HasAttribute<TAttribute>()));
  }

  [NotNull]
  [ItemNotEmpty]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<(FieldInfo Field, TAttribute Attribute)> GetFieldAndAttribute<TAttribute>(
    [NotNull] this Type type,
    BindingFlags bindingFlags = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.ExactBinding)
    where TAttribute : Attribute
  {
    TAttribute attribute;
    return ((IEnumerable<FieldInfo>) type.GetFields(bindingFlags)).Select<FieldInfo, (FieldInfo, TAttribute)?>((Func<FieldInfo, (FieldInfo, TAttribute)?>) (field => !field.TryGetAttribute<TAttribute>(out attribute) ? new (FieldInfo, TAttribute)?() : new (FieldInfo, TAttribute)?((field, attribute)))).NotNull<(FieldInfo, TAttribute)>();
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static object GetDefault([NotNull] this Type type)
  {
    return !type.IsValueType ? (object) null : Activator.CreateInstance(type);
  }

  [NotNull]
  public static ReflectionHelper GetReflectionHelper([NotNull] this Type type, BindingFlags bindingFlags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy | BindingFlags.ExactBinding)
  {
    return new ReflectionHelper((object) null, type, bindingFlags);
  }

  [NotNull]
  public static LazyPropertyReflection<TValue> GetLazyPropertyReflection<TValue>(
    [NotNull] this Type type,
    [NotNull, NotWhitespace] string propertyName,
    BindingFlags bindingFlags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy | BindingFlags.GetProperty | BindingFlags.ExactBinding,
    bool cacheValue = false)
  {
    return new LazyPropertyReflection<TValue>((object) null, type, propertyName, bindingFlags);
  }

  [NotNull]
  public static LazyPropertyReflection<TValue> GetLazyPropertyReflection<TValue>(
    [NotNull] this Type type,
    [NotNull, NotWhitespace] string propertyName,
    bool cacheValue)
  {
    return new LazyPropertyReflection<TValue>((object) null, type, propertyName, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy | BindingFlags.GetProperty | BindingFlags.ExactBinding);
  }

  [NotNull]
  public static LazyFieldReflection<TValue> GetLazyFieldReflection<TValue>(
    [NotNull] this Type type,
    [NotNull, NotWhitespace] string fieldName,
    BindingFlags bindingFlags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy | BindingFlags.GetField | BindingFlags.ExactBinding,
    bool cacheValue = false)
  {
    return new LazyFieldReflection<TValue>((object) null, type, fieldName, bindingFlags);
  }

  [NotNull]
  public static LazyFieldReflection<TValue> GetLazyFieldReflection<TValue>(
    [NotNull] this Type type,
    [NotNull, NotWhitespace] string fieldName,
    bool cacheValue)
  {
    return new LazyFieldReflection<TValue>((object) null, type, fieldName, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy | BindingFlags.GetField | BindingFlags.ExactBinding);
  }
}
