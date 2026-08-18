// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.Field`1
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Data;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public readonly struct Field<T>
{
  private static readonly TypeCode _typeCode = Type.GetTypeCode(typeof (T));
  [CanBeNull]
  private static readonly T _defaultExample = default (T);
  [CanBeEmpty]
  public readonly int FieldIndex;
  [CanBeNull]
  [NotWhitespace]
  public readonly string FieldName;
  [CanBeNull]
  public readonly IFormatProvider FormatProvider;
  [CanBeNull]
  public readonly Field<T>.NotNullValueParser Parser;
  [CanBeNull]
  [CanBeEmpty]
  public readonly T DefaultValue;
  [CanBeNull]
  public readonly Field<T>.GetDefaultDelegate GetDefault;

  public T GetValue([NotNull] DataRow row)
  {
    object obj1 = this.FieldName != null ? row[this.FieldName] : row[this.FieldIndex];
    if (obj1 != null && !(obj1 is DBNull))
    {
      if (this.Parser != null)
        return this.Parser(obj1);
      switch (Field<T>._typeCode)
      {
        case TypeCode.Empty:
          T defaultExample = Field<T>._defaultExample;
          if ((object) defaultExample != null && defaultExample is Guid _)
          {
            switch (obj1)
            {
              case Guid guid:
                return (T) (System.ValueType) guid;
              case string input:
                Guid result;
                if (!string.IsNullOrWhiteSpace(input) && Guid.TryParse(input, out result))
                  return (T) (System.ValueType) result;
                break;
              case byte[] b:
                if (b.Length == 16 /*0x10*/)
                  return (T) (System.ValueType) new Guid(b);
                break;
            }
          }
          else
            break;
          break;
        case TypeCode.Boolean:
          object obj2;
          if ((obj2 = obj1) is bool)
            return (T) (System.ValueType) (bool) obj2;
          return this.FormatProvider == null ? (T) (System.ValueType) Convert.ToBoolean(obj1) : (T) (System.ValueType) Convert.ToBoolean(obj1, this.FormatProvider);
        case TypeCode.Char:
          object obj3;
          if ((obj3 = obj1) is char)
            return (T) (System.ValueType) (char) obj3;
          return this.FormatProvider == null ? (T) (System.ValueType) Convert.ToBoolean(obj1) : (T) (System.ValueType) Convert.ToBoolean(obj1, this.FormatProvider);
        case TypeCode.Int32:
          return obj1 is int num1 ? (T) (System.ValueType) num1 : (T) (System.ValueType) Convert.ToInt32(obj1);
        case TypeCode.Int64:
          return obj1 is long num2 ? (T) (System.ValueType) num2 : (T) (System.ValueType) Convert.ToInt64(obj1);
        case TypeCode.Single:
          object obj4;
          if ((obj4 = obj1) is float)
            return (T) (System.ValueType) (float) obj4;
          return this.FormatProvider == null ? (T) (System.ValueType) Convert.ToBoolean(obj1) : (T) (System.ValueType) Convert.ToBoolean(obj1, this.FormatProvider);
        case TypeCode.Double:
          if (obj1 is double num3)
            return (T) (System.ValueType) num3;
          return this.FormatProvider == null ? (T) (System.ValueType) Convert.ToBoolean(obj1) : (T) (System.ValueType) Convert.ToBoolean(obj1, this.FormatProvider);
        case TypeCode.DateTime:
          if (obj1 is DateTime dateTime)
            return (T) (System.ValueType) dateTime;
          return this.FormatProvider == null ? (T) (System.ValueType) Convert.ToBoolean(obj1) : (T) (System.ValueType) Convert.ToBoolean(obj1, this.FormatProvider);
        case TypeCode.String:
          if (obj1 is string str)
            return (T) str;
          return this.FormatProvider == null ? (T) (System.ValueType) Convert.ToBoolean(obj1) : (T) (System.ValueType) Convert.ToBoolean(obj1, this.FormatProvider);
      }
    }
    return this.GetDefault != null ? this.GetDefault(row) : this.DefaultValue;
  }

  internal Field(
    int fieldIndex,
    [CanBeNull, NotWhitespace] string fieldName,
    [CanBeNull] IFormatProvider formatProvider,
    [CanBeNull] Field<T>.NotNullValueParser parser,
    [CanBeNull, CanBeEmpty] T defaultValue,
    [CanBeNull] Field<T>.GetDefaultDelegate getDefault)
  {
    this.FieldIndex = fieldIndex;
    this.FieldName = fieldName;
    this.FormatProvider = formatProvider;
    this.Parser = parser;
    this.DefaultValue = defaultValue;
    this.GetDefault = getDefault;
  }

  public Field<T> WithIndex(int fieldIndex)
  {
    return new Field<T>(fieldIndex, (string) null, this.FormatProvider, this.Parser, this.DefaultValue, this.GetDefault);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<T> Get(int fieldIndex, [CanBeNull] T defaultValue = null)
  {
    return new Field<T>(fieldIndex, (string) null, (IFormatProvider) null, (Field<T>.NotNullValueParser) null, defaultValue, (Field<T>.GetDefaultDelegate) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<T> Get([NotNull, NotWhitespace] string fieldName, [CanBeNull] T defaultValue = null)
  {
    return new Field<T>(0, fieldName, (IFormatProvider) null, (Field<T>.NotNullValueParser) null, defaultValue, (Field<T>.GetDefaultDelegate) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<T> Get(int fieldIndex, [NotNull] Field<T>.NotNullValueParser parser, [CanBeNull] T defaultValue = null)
  {
    return new Field<T>(fieldIndex, (string) null, (IFormatProvider) null, parser, defaultValue, (Field<T>.GetDefaultDelegate) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<T> Get([NotNull, NotWhitespace] string fieldName, [NotNull] Field<T>.NotNullValueParser parser, [CanBeNull] T defaultValue = null)
  {
    return new Field<T>(0, fieldName, (IFormatProvider) null, parser, defaultValue, (Field<T>.GetDefaultDelegate) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<T> Get(
    int fieldIndex,
    [NotNull] Field<T>.NotNullValueParser parser,
    [NotNull] Field<T>.GetDefaultDelegate getDefault)
  {
    return new Field<T>(fieldIndex, (string) null, (IFormatProvider) null, parser, default (T), getDefault);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Field<T> Get(
    [NotNull, NotWhitespace] string fieldName,
    [NotNull] Field<T>.NotNullValueParser parser,
    [NotNull] Field<T>.GetDefaultDelegate getDefault)
  {
    return new Field<T>(0, fieldName, (IFormatProvider) null, parser, default (T), getDefault);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static implicit operator Field<T>(in (int FieldIndex, T DefaultValue) tuple)
  {
    return new Field<T>(tuple.Item1, (string) null, (IFormatProvider) null, (Field<T>.NotNullValueParser) null, tuple.Item2, (Field<T>.GetDefaultDelegate) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static implicit operator Field<T>(in (string FieldName, T DefaultValue) tuple)
  {
    return new Field<T>(0, tuple.Item1, (IFormatProvider) null, (Field<T>.NotNullValueParser) null, tuple.Item2, (Field<T>.GetDefaultDelegate) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static implicit operator Field<T>(
    in (int FieldIndex, Field<T>.NotNullValueParser Parser) tuple)
  {
    return new Field<T>(tuple.Item1, (string) null, (IFormatProvider) null, tuple.Item2, default (T), (Field<T>.GetDefaultDelegate) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static implicit operator Field<T>(
    in (string FieldName, Field<T>.NotNullValueParser Parser) tuple)
  {
    return new Field<T>(0, tuple.Item1, (IFormatProvider) null, tuple.Item2, default (T), (Field<T>.GetDefaultDelegate) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static implicit operator Field<T>(
    in (int FieldIndex, Field<T>.NotNullValueParser Parser, T DefaultValue) tuple)
  {
    return new Field<T>(tuple.Item1, (string) null, (IFormatProvider) null, tuple.Item2, tuple.Item3, (Field<T>.GetDefaultDelegate) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static implicit operator Field<T>(
    in (string FieldName, Field<T>.NotNullValueParser Parser, T DefaultValue) tuple)
  {
    return new Field<T>(0, tuple.Item1, (IFormatProvider) null, tuple.Item2, tuple.Item3, (Field<T>.GetDefaultDelegate) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static implicit operator Field<T>(
    in (int FieldIndex, Field<T>.NotNullValueParser Parser, Field<T>.GetDefaultDelegate GetDefault) tuple)
  {
    return new Field<T>(tuple.Item1, (string) null, (IFormatProvider) null, tuple.Item2, default (T), tuple.Item3);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static implicit operator Field<T>(
    in (string FieldName, Field<T>.NotNullValueParser Parser, Field<T>.GetDefaultDelegate GetDefault) tuple)
  {
    return new Field<T>(0, tuple.Item1, (IFormatProvider) null, tuple.Item2, default (T), tuple.Item3);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static implicit operator Field<T>(
    in (int FieldIndex, Field<T>.GetDefaultDelegate GetDefault) tuple)
  {
    return new Field<T>(tuple.Item1, (string) null, (IFormatProvider) null, (Field<T>.NotNullValueParser) null, default (T), tuple.Item2);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static implicit operator Field<T>(
    in (string FieldName, Field<T>.GetDefaultDelegate GetDefault) tuple)
  {
    return new Field<T>(0, tuple.Item1, (IFormatProvider) null, (Field<T>.NotNullValueParser) null, default (T), tuple.Item2);
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static explicit operator string(in Field<T> field) => field.FieldName;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static explicit operator int(in Field<T> field) => field.FieldIndex;

  public override string ToString() => this.FieldName ?? $"Field with index {this.FieldIndex}";

  public override int GetHashCode()
  {
    return this.FieldName == null ? this.FieldIndex.GetHashCode() : this.FieldName.GetHashCode();
  }

  public bool Equals(Field<T> other)
  {
    return object.Equals((object) this.FieldName, (object) other.FieldName) && object.Equals((object) this.FieldIndex, (object) other.FieldIndex);
  }

  [CanBeNull]
  public delegate T NotNullValueParser([NotNull] object value);

  [CanBeNull]
  public delegate T NotNullValueFormatParser([NotNull] object value, [CanBeNull] IFormatProvider formatProvider);

  [CanBeNull]
  public delegate T GetDefaultDelegate([NotNull] DataRow row);
}
