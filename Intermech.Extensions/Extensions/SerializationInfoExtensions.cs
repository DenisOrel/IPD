// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.SerializationInfoExtensions
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Extensions;

public static class SerializationInfoExtensions
{
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T GetValue<T>([NotNull] this SerializationInfo serializationInfo, [NotNull, NotWhitespace] string valueName)
  {
    return (T) serializationInfo.GetValue(valueName, typeof (T));
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Guid GetGuid([NotNull] this SerializationInfo serializationInfo, [NotNull, NotWhitespace] string valueName)
  {
    return (Guid) serializationInfo.GetValue(valueName, typeof (Guid));
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T GetValueNotNull<T>([NotNull] this SerializationInfo serializationInfo, [NotNull, NotWhitespace] string valueName)
  {
    return (T) serializationInfo.GetValue(valueName, typeof (T));
  }

  public static void LoadOptionalValues(
    [NotNull] this SerializationInfo serializationInfo,
    [NotNull, NotEmpty] params (string valueName, Type valueType, Action<object> handler)[] loadHandlers)
  {
    foreach (SerializationEntry serializationEntry1 in serializationInfo)
    {
      SerializationEntry serializationEntry = serializationEntry1;
      (string, Type, Action<object>) result;
      if (((IEnumerable<(string, Type, Action<object>)>) loadHandlers).TryGetFirst<(string, Type, Action<object>)>((Func<(string, Type, Action<object>), bool>) (handler => string.Equals(serializationEntry.Name, handler.valueName, StringComparison.InvariantCulture)), out result) && (serializationEntry.ObjectType == result.Item2 || serializationEntry.ObjectType.IsSubclassOf(result.Item2)))
        result.Item3(serializationInfo.GetValue(result.Item1, result.Item2));
    }
  }

  [NotNull]
  public static IEnumerable<(string name, object value)> OptionalValuesEnumeration(
    [NotNull] this SerializationInfo serializationInfo,
    [NotNull, NotEmpty] params (string name, Type type)[] seekValues)
  {
    List<(string, object)> valueTupleList = (List<(string, object)>) null;
    foreach (SerializationEntry serializationEntry1 in serializationInfo)
    {
      SerializationEntry serializationEntry = serializationEntry1;
      (string, Type) result;
      if (((IEnumerable<(string, Type)>) seekValues).TryGetFirst<(string, Type)>((Func<(string, Type), bool>) (seekValue => string.Equals(serializationEntry.Name, seekValue.name, StringComparison.InvariantCulture)), out result) && (serializationEntry.ObjectType == result.Item2 || serializationEntry.ObjectType.IsSubclassOf(result.Item2)))
        (valueTupleList ?? (valueTupleList = new List<(string, object)>(seekValues.Length))).Add((result.Item1, serializationInfo.GetValue(result.Item1, result.Item2)));
    }
    return (IEnumerable<(string, object)>) valueTupleList ?? (IEnumerable<(string, object)>) Array.Empty<(string, object)>();
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string GetNotNullString([NotNull] this SerializationInfo serializationInfo, [NotNull, NotWhitespace] string valueName)
  {
    return serializationInfo.GetString(valueName) ?? throw new NullReferenceException($"String \"{valueName}\" cannot be null");
  }

  [NotNull]
  [NotEmpty]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string GetNotEmptyString([NotNull] this SerializationInfo serializationInfo, [NotNull, NotWhitespace] string valueName)
  {
    string str = serializationInfo.GetString(valueName);
    if (str == null)
      throw new NullReferenceException($"String \"{valueName}\" cannot be null");
    return !(str == string.Empty) ? str : throw new EmptyStringNotAllowedException(valueName);
  }

  [NotNull]
  [NotWhitespace]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string GetNotWhitespaceString(
    [NotNull] this SerializationInfo serializationInfo,
    [NotNull, NotWhitespace] string valueName)
  {
    string str = serializationInfo.GetString(valueName);
    if (str == null)
      throw new NullReferenceException($"String \"{valueName}\" cannot be null");
    if (str == string.Empty)
      throw new EmptyStringNotAllowedException(valueName);
    return !string.IsNullOrWhiteSpace(str) ? str : throw new WhitespaceNotAllowedException(valueName);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static object GetNotNullValue(
    [NotNull] this SerializationInfo serializationInfo,
    [NotNull, NotWhitespace] string valueName,
    [NotNull] Type type)
  {
    return serializationInfo.GetValue(valueName, type) ?? throw new NullReferenceException($"Value \"{valueName}\" cannot be null");
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T GetNotNullValue<T>([NotNull] this SerializationInfo serializationInfo, [NotNull, NotWhitespace] string valueName) where T : class
  {
    return (T) serializationInfo.GetNotNullValue(valueName, typeof (T));
  }

  [NotEmpty]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T GetNotEmptyValue<T>([NotNull] this SerializationInfo serializationInfo, [NotNull, NotWhitespace] string valueName) where T : struct
  {
    Type type = typeof (T);
    T objA = (T) (serializationInfo.GetValue(valueName, type) ?? throw new ValueEmptyException(valueName));
    return !object.Equals((object) objA, (object) null) ? objA : throw new ValueEmptyException(valueName);
  }

  [ContractAnnotation("throwExceptionIfNotFound:false => CanBeNull; => NotNull")]
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Type GetType(
    [NotNull] this SerializationInfo serializationInfo,
    [NotNull, NotWhitespace] string valueName,
    bool throwExceptionIfNotFound = true)
  {
    string whitespaceString = serializationInfo.GetNotWhitespaceString(valueName);
    Type type = !string.IsNullOrWhiteSpace(whitespaceString) ? Type.GetType(whitespaceString) : (Type) null;
    return !(type == (Type) null & throwExceptionIfNotFound) ? type : throw new TypeLoadException(whitespaceString);
  }
}
