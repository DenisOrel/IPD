// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.Field
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

public static class Field
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<T> Get<T>(int fieldIndex, [CanBeNull] T defaultValue = null)
  {
    return Field<T>.Get(fieldIndex, defaultValue);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<T> Get<T>([NotNull, NotWhitespace] string fieldName, [CanBeNull] T defaultValue = null)
  {
    return Field<T>.Get(fieldName, defaultValue);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<bool> Bool(int fieldIndex, bool defaultValue = false)
  {
    return new Field<bool>(fieldIndex, (string) null, (IFormatProvider) null, new Field<bool>.NotNullValueParser(Convert.ToBoolean), defaultValue, (Field<bool>.GetDefaultDelegate) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<bool> Bool([NotNull, NotWhitespace] string fieldName, bool defaultValue = false)
  {
    return new Field<bool>(0, fieldName, (IFormatProvider) null, new Field<bool>.NotNullValueParser(Convert.ToBoolean), defaultValue, (Field<bool>.GetDefaultDelegate) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<bool?> BoolOrNull(int fieldIndex)
  {
    return new Field<bool?>(fieldIndex, (string) null, (IFormatProvider) null, (Field<bool?>.NotNullValueParser) (obj => new bool?(Convert.ToBoolean(obj))), new bool?(), (Field<bool?>.GetDefaultDelegate) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<bool?> BoolOrNull([NotNull, NotWhitespace] string fieldName)
  {
    return new Field<bool?>(0, fieldName, (IFormatProvider) null, (Field<bool?>.NotNullValueParser) (obj => new bool?(Convert.ToBoolean(obj))), new bool?(), (Field<bool?>.GetDefaultDelegate) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<char> Char(int fieldIndex, char defaultValue = '\0')
  {
    return new Field<char>(fieldIndex, (string) null, (IFormatProvider) null, new Field<char>.NotNullValueParser(Convert.ToChar), defaultValue, (Field<char>.GetDefaultDelegate) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<char> Char([NotNull, NotWhitespace] string fieldName, char defaultValue = '\0')
  {
    return new Field<char>(0, fieldName, (IFormatProvider) null, new Field<char>.NotNullValueParser(Convert.ToChar), defaultValue, (Field<char>.GetDefaultDelegate) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<char> Char(int fieldIndex, [NotNull] IFormatProvider formatProvider, char defaultValue = '\0')
  {
    return new Field<char>(fieldIndex, (string) null, (IFormatProvider) null, (Field<char>.NotNullValueParser) (obj => Convert.ToChar(obj, formatProvider)), defaultValue, (Field<char>.GetDefaultDelegate) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<char> Char(
    [NotNull, NotWhitespace] string fieldName,
    [NotNull] IFormatProvider formatProvider,
    char defaultValue = '\0')
  {
    return new Field<char>(0, fieldName, (IFormatProvider) null, (Field<char>.NotNullValueParser) (obj => Convert.ToChar(obj, formatProvider)), defaultValue, (Field<char>.GetDefaultDelegate) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<char?> CharOrNull(int fieldIndex)
  {
    return new Field<char?>(fieldIndex, (string) null, (IFormatProvider) null, (Field<char?>.NotNullValueParser) (obj => new char?(Convert.ToChar(obj))), new char?(), (Field<char?>.GetDefaultDelegate) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<char?> CharOrNull([NotNull, NotWhitespace] string fieldName)
  {
    return new Field<char?>(0, fieldName, (IFormatProvider) null, (Field<char?>.NotNullValueParser) (obj => new char?(Convert.ToChar(obj))), new char?(), (Field<char?>.GetDefaultDelegate) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<char?> CharOrNull(int fieldIndex, [NotNull] IFormatProvider formatProvider)
  {
    return new Field<char?>(fieldIndex, (string) null, (IFormatProvider) null, (Field<char?>.NotNullValueParser) (obj => new char?(Convert.ToChar(obj, formatProvider))), new char?(), (Field<char?>.GetDefaultDelegate) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<char?> CharOrNull([NotNull, NotWhitespace] string fieldName, [NotNull] IFormatProvider formatProvider)
  {
    return new Field<char?>(0, fieldName, (IFormatProvider) null, (Field<char?>.NotNullValueParser) (obj => new char?(Convert.ToChar(obj, formatProvider))), new char?(), (Field<char?>.GetDefaultDelegate) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<int> Integer(int fieldIndex, int defaultValue = 0)
  {
    return new Field<int>(fieldIndex, (string) null, (IFormatProvider) null, new Field<int>.NotNullValueParser(Convert.ToInt32), defaultValue, (Field<int>.GetDefaultDelegate) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<int> Integer([NotNull, NotWhitespace] string fieldName, int defaultValue = 0)
  {
    return new Field<int>(0, fieldName, (IFormatProvider) null, new Field<int>.NotNullValueParser(Convert.ToInt32), defaultValue, (Field<int>.GetDefaultDelegate) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<int?> IntegerOrNull(int fieldIndex)
  {
    return new Field<int?>(fieldIndex, (string) null, (IFormatProvider) null, (Field<int?>.NotNullValueParser) (obj => new int?(Convert.ToInt32(obj))), new int?(), (Field<int?>.GetDefaultDelegate) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<int?> IntegerOrNull([NotNull, NotWhitespace] string fieldName)
  {
    return new Field<int?>(0, fieldName, (IFormatProvider) null, (Field<int?>.NotNullValueParser) (obj => new int?(Convert.ToInt32(obj))), new int?(), (Field<int?>.GetDefaultDelegate) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<long> Long(int fieldIndex, long defaultValue = 0)
  {
    return new Field<long>(fieldIndex, (string) null, (IFormatProvider) null, new Field<long>.NotNullValueParser(Convert.ToInt64), defaultValue, (Field<long>.GetDefaultDelegate) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<long> Long([NotNull, NotWhitespace] string fieldName, long defaultValue = 0)
  {
    return new Field<long>(0, fieldName, (IFormatProvider) null, new Field<long>.NotNullValueParser(Convert.ToInt64), defaultValue, (Field<long>.GetDefaultDelegate) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<long?> LongOrNull(int fieldIndex)
  {
    return new Field<long?>(fieldIndex, (string) null, (IFormatProvider) null, (Field<long?>.NotNullValueParser) (obj => new long?(Convert.ToInt64(obj))), new long?(), (Field<long?>.GetDefaultDelegate) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<long?> LongOrNull([NotNull, NotWhitespace] string fieldName)
  {
    return new Field<long?>(0, fieldName, (IFormatProvider) null, (Field<long?>.NotNullValueParser) (obj => new long?(Convert.ToInt64(obj))), new long?(), (Field<long?>.GetDefaultDelegate) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<string> String(int fieldIndex, [CanBeNull] string defaultValue = null)
  {
    return new Field<string>(fieldIndex, (string) null, (IFormatProvider) null, new Field<string>.NotNullValueParser(Convert.ToString), defaultValue, (Field<string>.GetDefaultDelegate) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<string> String([NotNull, NotWhitespace] string fieldName, [CanBeNull] string defaultValue = null)
  {
    return new Field<string>(0, fieldName, (IFormatProvider) null, new Field<string>.NotNullValueParser(Convert.ToString), defaultValue, (Field<string>.GetDefaultDelegate) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<string> String(
    int fieldIndex,
    [NotNull] IFormatProvider formatProvider,
    [CanBeNull] string defaultValue = null)
  {
    return new Field<string>(fieldIndex, (string) null, formatProvider, (Field<string>.NotNullValueParser) (obj => Convert.ToString(obj, formatProvider)), defaultValue, (Field<string>.GetDefaultDelegate) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<string> String(
    [NotNull, NotWhitespace] string fieldName,
    [NotNull] IFormatProvider formatProvider,
    [CanBeNull] string defaultValue = null)
  {
    return new Field<string>(0, fieldName, formatProvider, (Field<string>.NotNullValueParser) (obj => Convert.ToString(obj, formatProvider)), defaultValue, (Field<string>.GetDefaultDelegate) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<string> String(int fieldIndex, [NotNull] Field<string>.GetDefaultDelegate getDefault)
  {
    return new Field<string>(fieldIndex, (string) null, (IFormatProvider) null, new Field<string>.NotNullValueParser(Convert.ToString), (string) null, getDefault);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<string> String([NotNull, NotWhitespace] string fieldName, [NotNull] Field<string>.GetDefaultDelegate getDefault)
  {
    return new Field<string>(0, fieldName, (IFormatProvider) null, new Field<string>.NotNullValueParser(Convert.ToString), (string) null, getDefault);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<string> String(
    int fieldIndex,
    [NotNull] IFormatProvider formatProvider,
    [NotNull] Field<string>.GetDefaultDelegate getDefault)
  {
    return new Field<string>(fieldIndex, (string) null, formatProvider, (Field<string>.NotNullValueParser) (obj => Convert.ToString(obj, formatProvider)), (string) null, getDefault);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<string> String(
    [NotNull, NotWhitespace] string fieldName,
    [NotNull] IFormatProvider formatProvider,
    [NotNull] Field<string>.GetDefaultDelegate getDefault)
  {
    return new Field<string>(0, fieldName, formatProvider, (Field<string>.NotNullValueParser) (obj => Convert.ToString(obj, formatProvider)), (string) null, getDefault);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<Maybe<string>> MaybeString(int fieldIndex)
  {
    return new Field<Maybe<string>>(fieldIndex, (string) null, (IFormatProvider) null, (Field<Maybe<string>>.NotNullValueParser) (obj => new Maybe<string>(Convert.ToString(obj))), Maybe<string>.Empty, (Field<Maybe<string>>.GetDefaultDelegate) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<Maybe<string>> MaybeString([NotNull, NotWhitespace] string fieldName)
  {
    return new Field<Maybe<string>>(0, fieldName, (IFormatProvider) null, (Field<Maybe<string>>.NotNullValueParser) (obj => new Maybe<string>(Convert.ToString(obj))), Maybe<string>.Empty, (Field<Maybe<string>>.GetDefaultDelegate) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<Maybe<string>> MaybeString(
    int fieldIndex,
    [NotNull] IFormatProvider formatProvider,
    [CanBeNull] string defaultValue = null)
  {
    return new Field<Maybe<string>>(fieldIndex, (string) null, formatProvider, (Field<Maybe<string>>.NotNullValueParser) (obj => new Maybe<string>(Convert.ToString(obj, formatProvider))), Maybe<string>.Empty, (Field<Maybe<string>>.GetDefaultDelegate) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<Maybe<string>> MaybeString(
    [NotNull, NotWhitespace] string fieldName,
    [NotNull] IFormatProvider formatProvider,
    [CanBeNull] string defaultValue = null)
  {
    return new Field<Maybe<string>>(0, fieldName, formatProvider, (Field<Maybe<string>>.NotNullValueParser) (obj => new Maybe<string>(Convert.ToString(obj, formatProvider))), Maybe<string>.Empty, (Field<Maybe<string>>.GetDefaultDelegate) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<string> NotNullString(int fieldIndex, [NotNull] string defaultValue = "")
  {
    return new Field<string>(fieldIndex, (string) null, (IFormatProvider) null, new Field<string>.NotNullValueParser(Convert.ToString), defaultValue, (Field<string>.GetDefaultDelegate) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<string> NotNullString([NotNull, NotWhitespace] string fieldName, [NotNull] string defaultValue = "")
  {
    return new Field<string>(0, fieldName, (IFormatProvider) null, new Field<string>.NotNullValueParser(Convert.ToString), defaultValue, (Field<string>.GetDefaultDelegate) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<string> NotNullString(
    int fieldIndex,
    [NotNull] IFormatProvider formatProvider,
    [NotNull] string defaultValue = "")
  {
    return new Field<string>(fieldIndex, (string) null, formatProvider, (Field<string>.NotNullValueParser) (obj => Convert.ToString(obj, formatProvider)), defaultValue, (Field<string>.GetDefaultDelegate) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<string> NotNullString(
    [NotNull, NotWhitespace] string fieldName,
    [NotNull] IFormatProvider formatProvider,
    [NotNull] string defaultValue = "")
  {
    return new Field<string>(0, fieldName, formatProvider, (Field<string>.NotNullValueParser) (obj => Convert.ToString(obj, formatProvider)), defaultValue, (Field<string>.GetDefaultDelegate) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<float> Float(int fieldIndex, float defaultValue = 0.0f)
  {
    return new Field<float>(fieldIndex, (string) null, (IFormatProvider) null, new Field<float>.NotNullValueParser(Convert.ToSingle), defaultValue, (Field<float>.GetDefaultDelegate) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<float> Float([NotNull, NotWhitespace] string fieldName, float defaultValue = 0.0f)
  {
    return new Field<float>(0, fieldName, (IFormatProvider) null, new Field<float>.NotNullValueParser(Convert.ToSingle), defaultValue, (Field<float>.GetDefaultDelegate) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<float?> FloatOrNull(int fieldIndex)
  {
    return new Field<float?>(fieldIndex, (string) null, (IFormatProvider) null, (Field<float?>.NotNullValueParser) (obj => new float?(Convert.ToSingle(obj))), new float?(), (Field<float?>.GetDefaultDelegate) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<float?> FloatOrNull([NotNull, NotWhitespace] string fieldName)
  {
    return new Field<float?>(0, fieldName, (IFormatProvider) null, (Field<float?>.NotNullValueParser) (obj => new float?(Convert.ToSingle(obj))), new float?(), (Field<float?>.GetDefaultDelegate) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<double> Double(int fieldIndex, double defaultValue = 0.0)
  {
    return new Field<double>(fieldIndex, (string) null, (IFormatProvider) null, new Field<double>.NotNullValueParser(Convert.ToDouble), defaultValue, (Field<double>.GetDefaultDelegate) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<double> Double([NotNull, NotWhitespace] string fieldName, double defaultValue = 0.0)
  {
    return new Field<double>(0, fieldName, (IFormatProvider) null, new Field<double>.NotNullValueParser(Convert.ToDouble), defaultValue, (Field<double>.GetDefaultDelegate) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<double?> DoubleOrNull(int fieldIndex)
  {
    return new Field<double?>(fieldIndex, (string) null, (IFormatProvider) null, (Field<double?>.NotNullValueParser) (obj => new double?(Convert.ToDouble(obj))), new double?(), (Field<double?>.GetDefaultDelegate) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<double?> DoubleOrNull([NotNull, NotWhitespace] string fieldName)
  {
    return new Field<double?>(0, fieldName, (IFormatProvider) null, (Field<double?>.NotNullValueParser) (obj => new double?((double) Convert.ToSingle(obj))), new double?(), (Field<double?>.GetDefaultDelegate) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<System.DateTime> DateTime(int fieldIndex, System.DateTime defaultValue = default (System.DateTime))
  {
    return new Field<System.DateTime>(fieldIndex, (string) null, (IFormatProvider) null, new Field<System.DateTime>.NotNullValueParser(Convert.ToDateTime), defaultValue, (Field<System.DateTime>.GetDefaultDelegate) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<System.DateTime> DateTime([NotNull, NotWhitespace] string fieldName, System.DateTime defaultValue = default (System.DateTime))
  {
    return new Field<System.DateTime>(0, fieldName, (IFormatProvider) null, new Field<System.DateTime>.NotNullValueParser(Convert.ToDateTime), defaultValue, (Field<System.DateTime>.GetDefaultDelegate) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<System.DateTime> DateTime(
    int fieldIndex,
    [NotNull] IFormatProvider formatProvider,
    System.DateTime defaultValue = default (System.DateTime))
  {
    return new Field<System.DateTime>(fieldIndex, (string) null, (IFormatProvider) null, (Field<System.DateTime>.NotNullValueParser) (obj => Convert.ToDateTime(obj, formatProvider)), defaultValue, (Field<System.DateTime>.GetDefaultDelegate) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<System.DateTime> DateTime(
    [NotNull, NotWhitespace] string fieldName,
    [NotNull] IFormatProvider formatProvider,
    System.DateTime defaultValue = default (System.DateTime))
  {
    return new Field<System.DateTime>(0, fieldName, (IFormatProvider) null, (Field<System.DateTime>.NotNullValueParser) (obj => Convert.ToDateTime(obj, formatProvider)), defaultValue, (Field<System.DateTime>.GetDefaultDelegate) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<System.DateTime?> DateTimeOrNull(int fieldIndex)
  {
    return new Field<System.DateTime?>(fieldIndex, (string) null, (IFormatProvider) null, (Field<System.DateTime?>.NotNullValueParser) (obj => new System.DateTime?(Convert.ToDateTime(obj))), new System.DateTime?(), (Field<System.DateTime?>.GetDefaultDelegate) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<System.DateTime?> DateTimeOrNull([NotNull, NotWhitespace] string fieldName)
  {
    return new Field<System.DateTime?>(0, fieldName, (IFormatProvider) null, (Field<System.DateTime?>.NotNullValueParser) (obj => new System.DateTime?(Convert.ToDateTime(obj))), new System.DateTime?(), (Field<System.DateTime?>.GetDefaultDelegate) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<System.DateTime?> DateTimeOrNull(
    int fieldIndex,
    [NotNull] IFormatProvider formatProvider)
  {
    return new Field<System.DateTime?>(fieldIndex, (string) null, (IFormatProvider) null, (Field<System.DateTime?>.NotNullValueParser) (obj => new System.DateTime?(Convert.ToDateTime(obj, formatProvider))), new System.DateTime?(), (Field<System.DateTime?>.GetDefaultDelegate) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<System.DateTime?> DateTimeOrNull(
    [NotNull, NotWhitespace] string fieldName,
    [NotNull] IFormatProvider formatProvider)
  {
    return new Field<System.DateTime?>(0, fieldName, (IFormatProvider) null, (Field<System.DateTime?>.NotNullValueParser) (obj => new System.DateTime?(Convert.ToDateTime(obj, formatProvider))), new System.DateTime?(), (Field<System.DateTime?>.GetDefaultDelegate) null);
  }

  private static bool TryConvertToGuid([NotNull] object obj, out System.Guid result)
  {
    switch (obj)
    {
      case System.Guid guid:
        result = guid;
        return true;
      case string input:
        System.Guid result1;
        if (!string.IsNullOrWhiteSpace(input) && System.Guid.TryParse(input, out result1))
        {
          result = result1;
          return true;
        }
        result = System.Guid.Empty;
        return false;
      case byte[] b:
        if (b.Length == 16 /*0x10*/)
        {
          result = new System.Guid(b);
          return true;
        }
        result = System.Guid.Empty;
        return false;
      default:
        throw new FormatException($"Can`t convert object of type {obj.GetType()} to Guid");
    }
  }

  private static System.Guid? TryConvertToGuid([NotNull] object obj)
  {
    switch (obj)
    {
      case System.Guid guid:
        return new System.Guid?(guid);
      case string input:
        System.Guid result;
        return string.IsNullOrWhiteSpace(input) || !System.Guid.TryParse(input, out result) ? new System.Guid?() : new System.Guid?(result);
      case byte[] b:
        return b.Length != 16 /*0x10*/ ? new System.Guid?() : new System.Guid?(new System.Guid(b));
      default:
        throw new FormatException($"Can`t convert object of type {obj.GetType()} to Guid");
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<System.Guid> Guid(int fieldIndex, System.Guid defaultValue = default (System.Guid))
  {
    System.Guid result;
    return new Field<System.Guid>(fieldIndex, (string) null, (IFormatProvider) null, (Field<System.Guid>.NotNullValueParser) (obj => !Field.TryConvertToGuid(obj, out result) ? defaultValue : result), defaultValue, (Field<System.Guid>.GetDefaultDelegate) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<System.Guid> Guid([NotNull, NotWhitespace] string fieldName, System.Guid defaultValue = default (System.Guid))
  {
    System.Guid result;
    return new Field<System.Guid>(0, fieldName, (IFormatProvider) null, (Field<System.Guid>.NotNullValueParser) (obj => !Field.TryConvertToGuid(obj, out result) ? defaultValue : result), defaultValue, (Field<System.Guid>.GetDefaultDelegate) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<System.Guid?> GuidOrNull(int fieldIndex)
  {
    return new Field<System.Guid?>(fieldIndex, (string) null, (IFormatProvider) null, new Field<System.Guid?>.NotNullValueParser(Field.TryConvertToGuid), new System.Guid?(), (Field<System.Guid?>.GetDefaultDelegate) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<System.Guid?> GuidOrNull([NotNull, NotWhitespace] string fieldName)
  {
    return new Field<System.Guid?>(0, fieldName, (IFormatProvider) null, new Field<System.Guid?>.NotNullValueParser(Field.TryConvertToGuid), new System.Guid?(), (Field<System.Guid?>.GetDefaultDelegate) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<T> Custom<T>(int fieldIndex, [CanBeNull] T defaultValue = null)
  {
    return new Field<T>(fieldIndex, (string) null, (IFormatProvider) null, (Field<T>.NotNullValueParser) null, defaultValue, (Field<T>.GetDefaultDelegate) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<T> Custom<T>([NotNull, NotWhitespace] string fieldName, [CanBeNull] T defaultValue = null)
  {
    return new Field<T>(0, fieldName, (IFormatProvider) null, (Field<T>.NotNullValueParser) null, defaultValue, (Field<T>.GetDefaultDelegate) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<T> Custom<T>(int fieldIndex, [NotNull] IFormatProvider formatProvider, [CanBeNull] T defaultValue = null)
  {
    return new Field<T>(fieldIndex, (string) null, formatProvider, (Field<T>.NotNullValueParser) null, defaultValue, (Field<T>.GetDefaultDelegate) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<T> Custom<T>(
    [NotNull, NotWhitespace] string fieldName,
    [NotNull] IFormatProvider formatProvider,
    [CanBeNull] T defaultValue = null)
  {
    return new Field<T>(0, fieldName, formatProvider, (Field<T>.NotNullValueParser) null, defaultValue, (Field<T>.GetDefaultDelegate) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<T> Custom<T>(
    int fieldIndex,
    [NotNull] Field<T>.NotNullValueParser parser,
    [CanBeNull] T defaultValue = null)
  {
    return new Field<T>(fieldIndex, (string) null, (IFormatProvider) null, parser, defaultValue, (Field<T>.GetDefaultDelegate) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<T> Custom<T>(
    [NotNull, NotWhitespace] string fieldName,
    [NotNull] Field<T>.NotNullValueParser parser,
    [CanBeNull] T defaultValue = null)
  {
    return new Field<T>(0, fieldName, (IFormatProvider) null, parser, defaultValue, (Field<T>.GetDefaultDelegate) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<T> Custom<T>(
    int fieldIndex,
    [NotNull] Field<T>.NotNullValueParser parser,
    [NotNull] Field<T>.GetDefaultDelegate getDefault)
  {
    return new Field<T>(fieldIndex, (string) null, (IFormatProvider) null, parser, default (T), getDefault);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<T> Custom<T>(
    [NotNull, NotWhitespace] string fieldName,
    [NotNull] Field<T>.NotNullValueParser parser,
    [NotNull] Field<T>.GetDefaultDelegate getDefault)
  {
    return new Field<T>(0, fieldName, (IFormatProvider) null, parser, default (T), getDefault);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<T> Custom<T>(int fieldIndex, [NotNull] Field<T>.GetDefaultDelegate getDefault)
  {
    return new Field<T>(fieldIndex, (string) null, (IFormatProvider) null, (Field<T>.NotNullValueParser) null, default (T), getDefault);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<T> Custom<T>([NotNull, NotWhitespace] string fieldName, [NotNull] Field<T>.GetDefaultDelegate getDefault)
  {
    return new Field<T>(0, fieldName, (IFormatProvider) null, (Field<T>.NotNullValueParser) null, default (T), getDefault);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<T> Custom<T>(
    int fieldIndex,
    [NotNull] IFormatProvider formatProvider,
    [NotNull] Field<T>.GetDefaultDelegate getDefault)
  {
    return new Field<T>(fieldIndex, (string) null, (IFormatProvider) null, (Field<T>.NotNullValueParser) null, default (T), getDefault);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<T> Custom<T>(
    [NotNull, NotWhitespace] string fieldName,
    [NotNull] IFormatProvider formatProvider,
    [NotNull] Field<T>.GetDefaultDelegate getDefault)
  {
    return new Field<T>(0, fieldName, (IFormatProvider) null, (Field<T>.NotNullValueParser) null, default (T), getDefault);
  }

  [NotNull]
  [DebuggerStepThrough]
  [MustUseReturnValue]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IDisposable SaveValue<TValue>([NotNull] Expression<Func<TValue>> expression)
  {
    (object Object, FieldInfo FieldInfo) objectField = expression.GetObjectField<TValue>();
    return (IDisposable) new Field.FieldSavedValue<TValue>(objectField.Object, objectField.FieldInfo);
  }

  [NotNull]
  [DebuggerStepThrough]
  [MustUseReturnValue]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IDisposable SaveValue<TValue>([NotNull] object owner, [NotNull] FieldInfo fieldInfo)
  {
    return (IDisposable) new Field.FieldSavedValue<TValue>(owner, fieldInfo);
  }

  [NotNull]
  [DebuggerStepThrough]
  [MustUseReturnValue]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IDisposable SaveValue<TValue>([NotNull] object owner, [NotNull, NotWhitespace] string fieldName)
  {
    FieldInfo field = owner.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
    return (IDisposable) new Field.FieldSavedValue<TValue>(owner, field);
  }

  [NotNull]
  [DebuggerStepThrough]
  [MustUseReturnValue]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IDisposable SetTempValue<TValue>(
    [NotNull] Expression<Func<TValue>> expression,
    [CanBeNull] TValue tempValue)
  {
    (object Object, FieldInfo FieldInfo) objectField = expression.GetObjectField<TValue>();
    return (IDisposable) new Field.FieldSavedValue<TValue>(objectField.Object, objectField.FieldInfo, tempValue);
  }

  [NotNull]
  [DebuggerStepThrough]
  [MustUseReturnValue]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IDisposable SetTempValue<TValue>(
    [NotNull] object owner,
    [NotNull] FieldInfo fieldInfo,
    [CanBeNull] TValue tempValue)
  {
    return (IDisposable) new Field.FieldSavedValue<TValue>(owner, fieldInfo, tempValue);
  }

  [NotNull]
  [DebuggerStepThrough]
  [MustUseReturnValue]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IDisposable SetTempValue<TValue>([NotNull] object owner, [NotNull, NotWhitespace] string fieldName, [CanBeNull] TValue tempValue)
  {
    FieldInfo field = owner.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
    return (IDisposable) new Field.FieldSavedValue<TValue>(owner, field);
  }

  private class FieldSavedValue<TValue> : IDisposable
  {
    [NotNull]
    private readonly object _owner;
    [NotNull]
    private readonly FieldInfo _fieldInfo;
    [CanBeNull]
    private readonly TValue _savedValue;
    [CanBeNull]
    private readonly ISynchronizeInvoke _ownerSynchronizeInvoke;
    [CanBeNull]
    private readonly SynchronizationContext _synchronizationContext;
    [CanBeNull]
    private readonly Dispatcher _dispatcher;

    internal FieldSavedValue([NotNull] object owner, [NotNull] FieldInfo fieldInfo)
    {
      this._ownerSynchronizeInvoke = owner as ISynchronizeInvoke;
      if (this._ownerSynchronizeInvoke == null)
      {
        this._synchronizationContext = SynchronizationContext.Current;
        if (this._synchronizationContext == null)
          this._dispatcher = Dispatcher.CurrentDispatcher;
      }
      this._owner = owner;
      this._fieldInfo = fieldInfo;
      this._savedValue = this._ownerSynchronizeInvoke.Invoke<TValue>((Func<TValue>) (() => (TValue) this._fieldInfo.GetValue(this._owner)));
    }

    internal FieldSavedValue([NotNull] object owner, [NotNull] FieldInfo fieldInfo, [CanBeNull] TValue tempValue)
      : this(owner, fieldInfo)
    {
      if (object.Equals((object) this._savedValue, (object) tempValue))
        return;
      this._fieldInfo.SetValue(this._owner, (object) tempValue);
    }

    private void Restore()
    {
      if (this._owner == null || object.Equals((object) (TValue) this._fieldInfo.GetValue(this._owner), (object) this._savedValue))
        return;
      this._fieldInfo.SetValue(this._owner, (object) this._savedValue);
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
