// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.ObjectExtensions
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public static class ObjectExtensions
{
  [Pure]
  [ContractAnnotation("obj:null => null")]
  [CanBeNull]
  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static object InvokeIfNotNull<T>([CanBeNull, NoEnumeration] this T obj, [NotNull, InstantHandle] Action<T> action) where T : class
  {
    if ((object) obj != null)
      action(obj);
    return (object) obj;
  }

  [Pure]
  [ContractAnnotation("obj:null => null")]
  [CanBeNull]
  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static object InvokeIfNotNull<T>([CanBeNull, NoEnumeration] this T obj, [NotNull, InstantHandle] Action action) where T : class
  {
    if ((object) obj != null)
      action();
    return (object) obj;
  }

  [Pure]
  [ContractAnnotation("obj:null => null")]
  [CanBeNull]
  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T If<T>([CanBeNull, NoEnumeration] this T obj, [NotNull, InstantHandle] Func<T, bool> condition) where T : class
  {
    return (object) obj == null || !condition(obj) ? default (T) : obj;
  }

  [Pure]
  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool GetIf<T>([CanBeNull, NoEnumeration] this T obj, [NotNull, InstantHandle] Func<T, bool> condition, [CanBeNull] out T value)
  {
    bool flag = condition(obj);
    value = flag ? obj : default (T);
    return flag;
  }

  [Pure]
  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void Invoke<T>([CanBeNull, NoEnumeration] this T obj, [NotNull, InstantHandle] Action<T> action)
  {
    action(obj);
  }

  [CanBeNull]
  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TResult Invoke<T, TResult>([CanBeNull, NoEnumeration] this T obj, [NotNull, InstantHandle] Func<T, TResult> func)
  {
    return func(obj);
  }

  [Pure]
  [ContractAnnotation("obj:null => null")]
  [CanBeNull]
  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static object InvokeIf<T>([CanBeNull, NoEnumeration] this T obj, [NotNull, InstantHandle] Func<T, bool> condition, [NotNull, InstantHandle] Action<T> action) where T : class
  {
    if ((object) obj != null && condition(obj))
      action(obj);
    return (object) obj;
  }

  [Pure]
  [ContractAnnotation("obj:null => null")]
  [CanBeNull]
  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static object InvokeIf<T>([CanBeNull, NoEnumeration] this T obj, [NotNull, InstantHandle] Func<T, bool> condition, [NotNull, InstantHandle] Action action) where T : class
  {
    if ((object) obj != null && condition(obj))
      action();
    return (object) obj;
  }

  [Pure]
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TResult GetValue<TSource, TResult>(
    [CanBeNull] this TSource obj,
    [NotNull, InstantHandle] Func<TSource, TResult> getResultFunc)
    where TSource : class
  {
    return getResultFunc(obj);
  }

  [Pure]
  [CanBeNull]
  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TResult GetNotNullIfOrDefault<TSource, TResult>(
    [CanBeNull] this TSource obj,
    [NotNull, InstantHandle] Func<TSource, TResult> getResultFunc)
    where TSource : class
  {
    return (object) obj == null ? default (TResult) : getResultFunc(obj);
  }

  [Pure]
  [CanBeNull]
  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TResult GetIfOrDefault<TSource, TResult>(
    [CanBeNull] this TSource obj,
    [NotNull, InstantHandle] Func<TSource, bool> condition,
    [NotNull, InstantHandle] Func<TSource, TResult> getResultFunc)
    where TSource : class
  {
    return (object) obj == null || !condition(obj) ? default (TResult) : getResultFunc(obj);
  }

  [Pure]
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TResult GetIfOrDefault<TSource, TResult>(
    [CanBeNull] this TSource obj,
    [NotNull, InstantHandle] Func<TSource, bool> condition,
    [NotNull, InstantHandle] Func<TSource, TResult> getResultFunc,
    [CanBeNull] TResult defaultValue)
    where TSource : class
  {
    return (object) obj == null || !condition(obj) ? defaultValue : getResultFunc(obj);
  }

  [Pure]
  [CanBeNull]
  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TResult GetIfOrDefault<TSource, TResult>(
    [CanBeNull] this TSource obj,
    [NotNull, InstantHandle] Func<TSource, bool> condition,
    [NotNull, InstantHandle] Func<TSource, TResult> getResultFunc,
    [NotNull, InstantHandle] Func<TResult> getDefaultValue)
    where TSource : class
  {
    return (object) obj == null || !condition(obj) ? getDefaultValue() : getResultFunc(obj);
  }

  [Pure]
  [NotNull]
  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T As<T>([NotNull] this object obj, [CanBeNull] string exceptionMessage = null)
  {
    return obj is T obj1 ? obj1 : throw new InvalidCastException(exceptionMessage ?? $"'{obj}' is not of type '{typeof (T)}'");
  }

  [NotNull]
  public static T GetRoot<T>([NotNull] this T obj, [NotNull, InstantHandle] Func<T, T> getParentFunction) where T : class
  {
    T obj1 = obj;
    while ((object) (obj1 = getParentFunction(obj1)) != null)
      obj = obj1;
    return obj;
  }

  public static T GetRoot<T>(this T obj, [NotNull, InstantHandle] Func<T, T?> getParentFunction) where T : struct
  {
    T? nullable = new T?(obj);
    while ((nullable = getParentFunction(nullable.Value)).HasValue)
      obj = nullable.Value;
    return obj;
  }

  [NotNull]
  [DebuggerStepThrough]
  [MustUseReturnValue]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IDisposable SaveValue<TObject, TValue>(
    [NotNull] this TObject obj,
    [NotNull] Expression<Func<TValue>> expression)
    where TObject : class
  {
    (object Object, MemberInfo MemberInfo) = expression.GetMember<TValue>();
    switch (MemberInfo)
    {
      case FieldInfo fieldInfo:
        return Object.SaveValue<TValue>(fieldInfo);
      case PropertyInfo propertyInfo:
        return Object.SaveValue<TValue>(propertyInfo);
      default:
        throw new InvalidOperationException($"Member {MemberInfo?.Name ?? string.Empty} is not field or property in {obj.GetType().Name} class");
    }
  }

  [NotNull]
  [DebuggerStepThrough]
  [MustUseReturnValue]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IDisposable SaveFieldValue<TObject, TValue>(
    [NotNull] this TObject obj,
    [NotNull] Expression<Func<TValue>> expression)
  {
    return Field.SaveValue<TValue>(expression);
  }

  [NotNull]
  [DebuggerStepThrough]
  [MustUseReturnValue]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IDisposable SavePropertyValue<TObject, TValue>(
    [NotNull] this TObject obj,
    [NotNull] Expression<Func<TValue>> expression)
  {
    return Property.SaveValue<TValue>(expression);
  }

  [NotNull]
  [DebuggerStepThrough]
  [MustUseReturnValue]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IDisposable SaveValue<TValue>([NotNull] this object obj, [NotNull] FieldInfo fieldInfo)
  {
    return Field.SaveValue<TValue>(obj, fieldInfo);
  }

  [NotNull]
  [DebuggerStepThrough]
  [MustUseReturnValue]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IDisposable SaveValue<TValue>([NotNull] this object obj, [NotNull] PropertyInfo propertyInfo)
  {
    return Property.SaveValue<TValue>(obj, propertyInfo);
  }

  [NotNull]
  [DebuggerStepThrough]
  [MustUseReturnValue]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IDisposable SaveFieldValue<TValue>([NotNull] this object obj, [NotNull, NotWhitespace] string fieldName)
  {
    return Field.SaveValue<TValue>(obj, fieldName);
  }

  [NotNull]
  [DebuggerStepThrough]
  [MustUseReturnValue]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IDisposable SavePropertyValue<TValue>([NotNull] this object obj, [NotNull, NotWhitespace] string propertyName)
  {
    return Property.SaveValue<TValue>(obj, propertyName);
  }

  [NotNull]
  [DebuggerStepThrough]
  [MustUseReturnValue]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IDisposable SaveValue<TValue>([NotNull] this object obj, [NotNull, NotWhitespace] string fieldOrPropertyName)
  {
    FieldInfo field = obj.GetType().GetField(fieldOrPropertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
    if (field != (FieldInfo) null)
      return obj.SaveValue<TValue>(field);
    PropertyInfo property = obj.GetType().GetProperty(fieldOrPropertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
    return property != (PropertyInfo) null ? obj.SaveValue<TValue>(property) : throw new InvalidOperationException($"Class {obj.GetType().Name} has no {fieldOrPropertyName} field or property");
  }

  [NotNull]
  [DebuggerStepThrough]
  [MustUseReturnValue]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IDisposable SetTempValue<TValue>(
    [NotNull] this object obj,
    [NotNull] Expression<Func<TValue>> expression,
    [CanBeNull] TValue tempValue)
  {
    (object Object, MemberInfo MemberInfo) = expression.GetMember<TValue>();
    switch (MemberInfo)
    {
      case FieldInfo fieldInfo:
        return Object.SetTempValue<TValue>(fieldInfo, tempValue);
      case PropertyInfo propertyInfo:
        return Object.SetTempValue<TValue>(propertyInfo, tempValue);
      default:
        throw new InvalidOperationException($"Member {MemberInfo?.Name ?? string.Empty} is not field or property in {obj.GetType().Name} class");
    }
  }

  [NotNull]
  [DebuggerStepThrough]
  [MustUseReturnValue]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IDisposable SetFieldTempValue<TValue>(
    [NotNull] this object obj,
    [NotNull] Expression<Func<TValue>> expression,
    [CanBeNull] TValue tempValue)
  {
    return Field.SetTempValue<TValue>(expression, tempValue);
  }

  [NotNull]
  [DebuggerStepThrough]
  [MustUseReturnValue]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IDisposable SetPropertyTempValue<TValue>(
    [NotNull] this object obj,
    [NotNull] Expression<Func<TValue>> expression,
    [CanBeNull] TValue tempValue)
  {
    return Property.SetTempValue<TValue>(expression, tempValue);
  }

  [NotNull]
  [DebuggerStepThrough]
  [MustUseReturnValue]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IDisposable SetTempValue<TValue>(
    [NotNull] this object obj,
    [NotNull] FieldInfo fieldInfo,
    [CanBeNull] TValue tempValue)
  {
    return Field.SetTempValue<TValue>(obj, fieldInfo, tempValue);
  }

  [NotNull]
  [DebuggerStepThrough]
  [MustUseReturnValue]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IDisposable SetTempValue<TValue>(
    [NotNull] this object obj,
    [NotNull] PropertyInfo propertyInfo,
    [CanBeNull] TValue tempValue)
  {
    return Property.SetTempValue<TValue>(obj, propertyInfo, tempValue);
  }

  [NotNull]
  [DebuggerStepThrough]
  [MustUseReturnValue]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IDisposable SetTempValue<TValue>(
    [NotNull] this object obj,
    [NotNull, NotWhitespace] string fieldOrPropertyName,
    [CanBeNull] TValue tempValue)
  {
    FieldInfo field = obj.GetType().GetField(fieldOrPropertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
    if (field != (FieldInfo) null)
      return obj.SetTempValue<TValue>(field, tempValue);
    PropertyInfo property = obj.GetType().GetProperty(fieldOrPropertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
    if (property != (PropertyInfo) null)
      return obj.SetTempValue<TValue>(property, tempValue);
    throw new InvalidOperationException($"Class {obj.GetType().Name} has no {fieldOrPropertyName} field or property");
  }

  [NotNull]
  [Pure]
  [ContractAnnotation("obj:null => halt; => NotNull")]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T CheckInitializedIn<T>(this T obj, [NotNull, NotWhitespace] string containerName) where T : class
  {
    return (object) obj != null ? obj : throw NotYetInitializedException.ForContainer(containerName, (Exception) new NullReferenceException());
  }

  [NotNull]
  [Pure]
  [ContractAnnotation("obj:null => halt; => NotNull")]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T CheckInitializedIn<T>(this T obj, [NotNull] Type staticContainerType) where T : class
  {
    return (object) obj != null ? obj : throw NotYetInitializedException.ForContainer(staticContainerType.FullName ?? string.Empty, (Exception) new NullReferenceException());
  }

  [NotNull]
  [Pure]
  [ContractAnnotation("obj:null => halt; => NotNull")]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T CheckInitializedIn<T>(this T obj, [NotNull] object container) where T : class
  {
    return (object) obj != null ? obj : throw NotYetInitializedException.ForContainer(container.GetType().Name, (Exception) new NullReferenceException());
  }

  [NotNull]
  [ContractAnnotation("obj:null => halt; => NotNull")]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TInterface CastToInterface<TInterface>([NotNull] this object obj) where TInterface : class
  {
    return obj is TInterface @interface ? @interface : throw new InvalidCastException($"Can not cast {obj.GetType()} instance to {typeof (TInterface)} interface");
  }

  [NotNull]
  [ContractAnnotation("obj:null => halt; => NotNull")]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TInterface CastToInterface<TInterface, TInvalidCastException>([NotNull] this object obj)
    where TInterface : class
    where TInvalidCastException : InvalidCastException
  {
    return obj is TInterface @interface ? @interface : throw (object) (TInvalidCastException) Activator.CreateInstance(typeof (TInvalidCastException), (object) $"Can not cast {obj.GetType()} instance to {typeof (TInterface)} interface");
  }

  [NotNull]
  [ContractAnnotation("obj:null => halt; => NotNull")]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TToInterface CastInterfaceToOtherInterface<TFromInterface, TToInterface>(
    [NotNull] this TFromInterface obj)
    where TFromInterface : class
    where TToInterface : class
  {
    return obj is TToInterface toInterface ? toInterface : throw new InvalidCastException($"Can not cast ({typeof (TFromInterface)}) {obj.GetType()} instance to {typeof (TToInterface)} interface");
  }

  [NotNull]
  [ContractAnnotation("obj:null => halt; => NotNull")]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TToInterface CastInterfaceToOtherInterface<TFromInterface, TToInterface, TInvalidCastException>(
    [NotNull] this TFromInterface obj)
    where TFromInterface : class
    where TToInterface : class
    where TInvalidCastException : InvalidCastException
  {
    return obj is TToInterface toInterface ? toInterface : throw (object) (TInvalidCastException) Activator.CreateInstance(typeof (TInvalidCastException), (object) $"Can not cast ({typeof (TFromInterface)}) {obj.GetType()} instance to {typeof (TToInterface)} interface");
  }

  [NotNull]
  [ContractAnnotation("obj:null => halt; => NotNull")]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TToClass CastInterfaceToClass<TFromInterface, TToClass>([NotNull] this TFromInterface obj)
    where TFromInterface : class
    where TToClass : class
  {
    return obj is TToClass toClass ? toClass : throw new InvalidCastException($"Can not cast ({typeof (TFromInterface)}) {obj.GetType()} instance to {typeof (TToClass)} type");
  }

  [NotNull]
  [ContractAnnotation("obj:null => halt; => NotNull")]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TToClass CastInterfaceToClass<TFromInterface, TToClass, TInvalidCastException>(
    [NotNull] this TFromInterface obj)
    where TFromInterface : class
    where TToClass : class
    where TInvalidCastException : InvalidCastException
  {
    return obj is TToClass toClass ? toClass : throw (object) (TInvalidCastException) Activator.CreateInstance(typeof (TInvalidCastException), (object) $"Can not cast ({typeof (TFromInterface)}) {obj.GetType()} instance to {typeof (TToClass)} type");
  }

  [NotNull]
  [ContractAnnotation("obj:null => halt; => NotNull")]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TToClass CastClassToClass<TToClass>([NotNull] this object obj) where TToClass : class
  {
    return obj is TToClass toClass ? toClass : throw new InvalidCastException($"Can not cast {obj.GetType()} instance to {typeof (TToClass)} type");
  }

  [NotNull]
  [ContractAnnotation("obj:null => halt; => NotNull")]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TToClass CastClassToClass<TToClass, TInvalidCastException>([NotNull] this object obj)
    where TToClass : class
    where TInvalidCastException : InvalidCastException
  {
    return obj is TToClass toClass ? toClass : throw (object) (TInvalidCastException) Activator.CreateInstance(typeof (TInvalidCastException), (object) $"Can not cast {obj.GetType()} instance to {typeof (TToClass)} type");
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static object InvokeMethod(
    [NotNull] this object obj,
    [NotNull] MethodInfo methodInfo,
    [NotNull] params object[] parameters)
  {
    return methodInfo.Invoke(obj, parameters);
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T InvokeMethod<T>(
    [NotNull] this object obj,
    [NotNull] MethodInfo methodInfo,
    [NotNull] params object[] parameters)
  {
    return (T) methodInfo.Invoke(obj, parameters);
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static object GetPropertyValue(
    [NotNull] this object obj,
    [NotNull] PropertyInfo propertyInfo,
    [NotNull] params object[] parameters)
  {
    return propertyInfo.GetValue(obj, parameters);
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T GetPropertyValue<T>(
    [NotNull] this object obj,
    [NotNull] PropertyInfo propertyInfo,
    [NotNull] params object[] parameters)
  {
    return (T) propertyInfo.GetValue(obj, parameters);
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static object GetFieldValue([NotNull] this object obj, [NotNull] FieldInfo fieldInfo)
  {
    return fieldInfo.GetValue(obj);
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T GetFieldValue<T>([NotNull] this object obj, [NotNull] FieldInfo fieldInfo)
  {
    return (T) fieldInfo.GetValue(obj);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void SetPropertyValue<T>(
    [NotNull] this object obj,
    [NotNull] PropertyInfo propertyInfo,
    [CanBeNull] T value,
    [NotNull] params object[] parameters)
  {
    propertyInfo.SetValue(obj, (object) value, parameters);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void SetFieldValue<T>([NotNull] this object obj, [NotNull] FieldInfo fieldInfo, [CanBeNull] T value)
  {
    fieldInfo.SetValue(obj, (object) value);
  }

  [NotNull]
  public static ReflectionHelper GetReflectionHelper([NotNull] this object obj, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy | BindingFlags.ExactBinding)
  {
    return new ReflectionHelper(obj, obj.GetType(), bindingFlags);
  }

  [NotNull]
  public static LazyPropertyReflection<TValue> GetLazyPropertyReflection<TValue>(
    [NotNull] this object obj,
    [NotNull, NotWhitespace] string propertyName,
    BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy | BindingFlags.GetProperty | BindingFlags.ExactBinding,
    bool cacheValue = false)
  {
    return new LazyPropertyReflection<TValue>(obj, obj.GetType(), propertyName, bindingFlags);
  }

  [NotNull]
  public static LazyPropertyReflection<TValue> GetLazyPropertyReflection<TValue>(
    [NotNull] this object obj,
    [NotNull, NotWhitespace] string propertyName,
    bool cacheValue)
  {
    return new LazyPropertyReflection<TValue>(obj, obj.GetType(), propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy | BindingFlags.GetProperty | BindingFlags.ExactBinding);
  }

  [NotNull]
  public static LazyFieldReflection<TValue> GetLazyFieldReflection<TValue>(
    [NotNull] this object obj,
    [NotNull, NotWhitespace] string fieldName,
    BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy | BindingFlags.GetField | BindingFlags.ExactBinding,
    bool cacheValue = false)
  {
    return new LazyFieldReflection<TValue>(obj, obj.GetType(), fieldName, bindingFlags);
  }

  [NotNull]
  public static LazyFieldReflection<TValue> GetLazyFieldReflection<TValue>(
    [NotNull] this object obj,
    [NotNull, NotWhitespace] string fieldName,
    bool cacheValue)
  {
    return new LazyFieldReflection<TValue>(obj, obj.GetType(), fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy | BindingFlags.GetField | BindingFlags.ExactBinding);
  }
}
