// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.IDBConfigurationsExtensions
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using System;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public static class IDBConfigurationsExtensions
{
  [ContractAnnotation("defaultValue:null => CanBeNull; => NotNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string ReadString(
    [NotNull] this IDBConfigurations configuration,
    [NotNull, NotWhitespace] string moduleName,
    [NotNull, NotWhitespace] string section,
    [NotNull, NotWhitespace] string param,
    [CanBeNull] string defaultValue = null,
    DBConfigMode configMode = DBConfigMode.UserAndGlobal)
  {
    return configuration.ReadString(moduleName, section, param, defaultValue, configMode);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static long ReadInteger(
    [NotNull] this IDBConfigurations configuration,
    [NotNull, NotWhitespace] string moduleName,
    [NotNull, NotWhitespace] string section,
    [NotNull, NotWhitespace] string param,
    long defaultValue = 0,
    DBConfigMode configMode = DBConfigMode.UserAndGlobal)
  {
    return configuration.ReadInteger(moduleName, section, param, defaultValue, configMode);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static double ReadDouble(
    [NotNull] this IDBConfigurations configuration,
    [NotNull, NotWhitespace] string moduleName,
    [NotNull, NotWhitespace] string section,
    [NotNull, NotWhitespace] string param,
    double defaultValue = 0.0,
    DBConfigMode configMode = DBConfigMode.UserAndGlobal)
  {
    return configuration.ReadDouble(moduleName, section, param, defaultValue, configMode);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool ReadBool(
    [NotNull] this IDBConfigurations configuration,
    [NotNull, NotWhitespace] string moduleName,
    [NotNull, NotWhitespace] string section,
    [NotNull, NotWhitespace] string param,
    bool defaultValue = false,
    DBConfigMode configMode = DBConfigMode.UserAndGlobal)
  {
    return configuration.ReadBool(moduleName, section, param, defaultValue, configMode);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DateTime ReadDateTime(
    [NotNull] this IDBConfigurations configuration,
    [NotNull, NotWhitespace] string moduleName,
    [NotNull, NotWhitespace] string section,
    [NotNull, NotWhitespace] string param,
    DateTime defaultValue,
    DBConfigMode configMode = DBConfigMode.UserAndGlobal)
  {
    return configuration.ReadDateTime(moduleName, section, param, defaultValue, configMode);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DateTime ReadDateTime(
    [NotNull] this IDBConfigurations configuration,
    [NotNull, NotWhitespace] string moduleName,
    [NotNull, NotWhitespace] string section,
    [NotNull, NotWhitespace] string param,
    DBConfigMode configMode = DBConfigMode.UserAndGlobal)
  {
    return configuration.ReadDateTime(moduleName, section, param, DateTime.MinValue, configMode);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TEnum ReadEnum<TEnum>(
    [NotNull] this IDBConfigurations configuration,
    [NotNull, NotWhitespace] string moduleName,
    [NotNull, NotWhitespace] string section,
    [NotNull, NotWhitespace] string param,
    TEnum defaultValue = default (TEnum),
    DBConfigMode configMode = DBConfigMode.UserAndGlobal)
    where TEnum : struct, Enum
  {
    Type underlyingType = Enum.GetUnderlyingType(typeof (TEnum));
    if (underlyingType == typeof (int))
      return (TEnum) (ValueType) (int) configuration.ReadInteger(moduleName, section, param, (long) (int) (ValueType) defaultValue, configMode);
    if (underlyingType == typeof (long))
      return (TEnum) (ValueType) configuration.ReadInteger(moduleName, section, param, (long) (ValueType) defaultValue, configMode);
    throw new Exception($"Unsupported Enum underlyingType {underlyingType}");
  }
}
