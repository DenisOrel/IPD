
// Type: Intermech.Extensions.FlagsExtensions
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;


namespace Intermech.Extensions
{
    /// <summary>
    /// Расширение системного класса Enum для упрощения обработки флагов
    /// </summary>
    public static class FlagsExtensions
    {
      /// <summary>Безопасное преобразование Enum в ulong (Int64), не глядя на то, в каком именно типе он хранится</summary>
      /// <exception cref="T:System.InvalidOperationException">Thrown when the requested operation is invalid</exception>
      /// <param name="value">Базовый набор флагов</param>
      /// <param name="typeCode"></param>
      /// <returns>The given data converted to an ulong</returns>
      /// <exception cref="T:System.FormatException">
      /// <paramref name="value" /> is not in an appropriate format</exception>
      /// <exception cref="T:System.InvalidCastException">
      /// <paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface.-or-The conversion is not supported. </exception>
      /// <exception cref="T:System.OverflowException">
      /// <paramref name="value" /> represents a number that is less than <see cref="F:System.Int64.MinValue" /> or greater than <see cref="F:System.Int64.MaxValue" /></exception>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      private static ulong ToULong([NotNull] object value, TypeCode typeCode)
      {
        switch (typeCode)
        {
          case TypeCode.Boolean:
          case TypeCode.Char:
          case TypeCode.Byte:
          case TypeCode.UInt16:
          case TypeCode.UInt32:
          case TypeCode.UInt64:
            return Convert.ToUInt64(value, (IFormatProvider) CultureInfo.InvariantCulture);
          case TypeCode.SByte:
          case TypeCode.Int16:
          case TypeCode.Int32:
          case TypeCode.Int64:
            return (ulong) Convert.ToInt64(value, (IFormatProvider) CultureInfo.InvariantCulture);
          default:
            throw new InvalidOperationException("Unknown enum Type");
        }
      }

      /// <summary>Безопасное преобразование Enum в ulong (Int64), не глядя на то, в каком именно типе он хранится</summary>
      /// <exception cref="T:System.InvalidOperationException">Thrown when the requested operation is invalid.</exception>
      /// <param name="value">Базовый набор флагов</param>
      /// <param name="typeCode"></param>
      /// <returns></returns>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      internal static ulong ToULong<TEnum>(TEnum value, TypeCode typeCode) where TEnum : struct, Enum
      {
        switch (typeCode)
        {
          case TypeCode.Boolean:
          case TypeCode.Char:
          case TypeCode.Byte:
          case TypeCode.UInt16:
          case TypeCode.UInt32:
          case TypeCode.UInt64:
            return Convert.ToUInt64((object) value);
          case TypeCode.SByte:
          case TypeCode.Int16:
          case TypeCode.Int32:
          case TypeCode.Int64:
            return (ulong) Convert.ToInt64((object) value);
          default:
            throw new InvalidOperationException("Unknown enum Type");
        }
      }

      /// <summary>Сравнение набора флагов с некоторым базовым набором, проверка, есть в нём новые, отсутствующие в базовом наборе</summary>
      /// <typeparam name="T">Generic type parameter</typeparam>
      /// <param name="type"></param>
      /// <param name="value">Базовый набор флагов</param>
      /// <returns>True, если новые флаги присутствуют</returns>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static bool HasNewFlags<T>([NotNull] this Enum type, [NotNull] T value)
      {
        TypeCode typeCode = type.GetTypeCode();
        return (FlagsExtensions.ToULong((object) type, typeCode) & ~FlagsExtensions.ToULong((object) value, typeCode)) > 0UL;
      }

      /// <summary>Сравнение набора флагов с некоторым базовым набором, проверка, есть в нём новые, отсутствующие в базовом наборе</summary>
      /// <param name="value"></param>
      /// <param name="compareWith">Базовый набор флагов</param>
      /// <returns>True, если новые флаги присутствуют</returns>
      [MustUseReturnValue]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static bool HasNewFlags<TEnum>(this TEnum value, TEnum compareWith) where TEnum : struct, Enum
      {
        TypeCode typeCode = Convert.GetTypeCode((object) default (TEnum));
        return (FlagsExtensions.ToULong<TEnum>(value, typeCode) & ~FlagsExtensions.ToULong<TEnum>(compareWith, typeCode)) > 0UL;
      }

      /// <summary>Добавить флаги в набор флагов</summary>
      /// <exception cref="T:System.ArgumentException">Thrown when one or more arguments have unsupported or illegal values</exception>
      /// <typeparam name="T">Generic type parameter</typeparam>
      /// <param name="type"></param>
      /// <param name="value">Флаги, которые требуется добавить в набор</param>
      /// <returns>Первоначальный набор флагов, в который были удалены переданные флаги</returns>
      [NotNull]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static T AddFlags<T>([NotNull] this Enum type, [NotNull] T value)
      {
        TypeCode typeCode = type.GetTypeCode();
        try
        {
          return (T) Convert.ChangeType((object) (ulong) ((long) FlagsExtensions.ToULong((object) type, typeCode) | (long) FlagsExtensions.ToULong((object) value, typeCode)), typeCode);
        }
        catch (Exception ex)
        {
          throw new ArgumentException($"Could not append value from enumerated type '{typeof (T).Name}'.", ex);
        }
      }

      /// <summary>Добавить флаги в набор флагов</summary>
      /// <param name="value"></param>
      /// <param name="newFlags">Флаги, которые требуется добавить в набор</param>
      /// <returns>Первоначальный набор флагов, в который были удалены переданные флаги</returns>
      [MustUseReturnValue]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static TEnum AddFlags<TEnum>(this TEnum value, TEnum newFlags) where TEnum : struct, Enum
      {
        TypeCode typeCode = Convert.GetTypeCode((object) default (TEnum));
        try
        {
          return (TEnum) Convert.ChangeType((object) (ulong) ((long) FlagsExtensions.ToULong<TEnum>(value, typeCode) | (long) FlagsExtensions.ToULong<TEnum>(newFlags, typeCode)), typeCode);
        }
        catch (Exception ex)
        {
          throw new ArgumentException($"Could not append value from enumerated type '{typeof (TEnum).Name}'.", ex);
        }
      }

      /// <summary>Удалить флаги из набора флагов</summary>
      /// <exception cref="T:System.ArgumentException">Thrown when one or more arguments have unsupported or illegal values</exception>
      /// <typeparam name="T">Generic type parameter</typeparam>
      /// <param name="type"></param>
      /// <param name="value">Флаги, которые требуется удалить</param>
      /// <returns>Первоначальный набор флагов, из которого были удалены переданные флаги</returns>
      [NotNull]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static T RemoveFlags<T>([NotNull] this Enum type, [NotNull] T value)
      {
        TypeCode typeCode = type.GetTypeCode();
        try
        {
          return (T) Convert.ChangeType((object) (ulong) ((long) FlagsExtensions.ToULong((object) type, typeCode) & ~(long) FlagsExtensions.ToULong((object) value, typeCode)), typeCode);
        }
        catch (Exception ex)
        {
          throw new ArgumentException($"Could not remove value from enumerated type '{typeof (T).Name}'.", ex);
        }
      }

      /// <summary>Удалить флаги из набора флагов</summary>
      /// <exception cref="T:System.ArgumentException">Thrown when one or more arguments have unsupported or illegal values.</exception>
      /// <typeparam name="TEnum">Generic type parameter</typeparam>
      /// <param name="value"></param>
      /// <param name="flagsToRemove">Флаги, которые требуется удалить</param>
      /// <returns>Первоначальный набор флагов, из которого были удалены переданные флаги</returns>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static TEnum RemoveFlags<TEnum>(this TEnum value, TEnum flagsToRemove) where TEnum : struct, Enum
      {
        TypeCode typeCode = Convert.GetTypeCode((object) default (TEnum));
        try
        {
          return (TEnum) Convert.ChangeType((object) (ulong) ((long) FlagsExtensions.ToULong<TEnum>(value, typeCode) & ~(long) FlagsExtensions.ToULong<TEnum>(flagsToRemove, typeCode)), typeCode);
        }
        catch (Exception ex)
        {
          throw new ArgumentException($"Could not remove value from enumerated type '{typeof (TEnum).Name}'.", ex);
        }
      }

      /// <summary>Перечисление всех "поднятых" флагам enum-а
      /// При этом из результата исключается 0,
      /// у перечислений помеченных атрибутом Flags возвращаются только индивидуальные значения, комбинации флагов отфильтровываются</summary>
      /// <exception cref="T:System.ArgumentException">Если тип перечисления не имеет атрибута Flags</exception>
      [NotNull]
      [MustUseReturnValue]
      public static IEnumerable<TEnum> Values<TEnum>(this TEnum value) where TEnum : struct, Enum
      {
        Type type = typeof (TEnum);
        TypeCode typeCode = Convert.GetTypeCode((object) default (TEnum));
        ulong num1 = FlagsExtensions.ToULong<TEnum>(value, typeCode);
        TEnum[] values = (TEnum[]) Enum.GetValues(type);
        if (values.Length == 0)
          return (IEnumerable<TEnum>) Array.Empty<TEnum>();
        bool flag1 = type.HasAttribute<FlagsAttribute>();
        bool flag2 = false;
        List<TEnum> source = new List<TEnum>(values.Length);
        if (num1 != 0UL)
        {
          foreach (TEnum @enum in values)
          {
            ulong num2 = FlagsExtensions.ToULong<TEnum>(@enum, typeCode);
            if (num2 != 0UL && ((long) num1 & (long) num2) == (long) num2)
            {
              source.Add(@enum);
              if (flag1 && !flag2)
                flag2 = (num2 & num2 - 1UL) > 0UL;
            }
          }
        }
        if (source.Count == 0)
          return (IEnumerable<TEnum>) Array.Empty<TEnum>();
        if (!flag1 || !flag2)
          return (IEnumerable<TEnum>) source.Distinct().ToList();
        List<TEnum> enumList = new List<TEnum>(source.Count);
        HashSet<TEnum> enumSet = new HashSet<TEnum>();
        foreach (TEnum @enum in source)
        {
          TEnum enumValue = @enum;
          if (enumSet.Add(enumValue) && !source.Any((Func<TEnum, bool>) (compareVal => enumValue.HasNewFlags(compareVal) && enumValue.HasFlag((Enum) compareVal))))
            enumList.Add(enumValue);
        }
        return (IEnumerable<TEnum>) enumList;
      }

      /// <summary>Итератор по всем возможным флагам типа enum
      /// При этом из результата исключается 0 и наборы флагов, возвращаются только индивидуальные битовые флаги</summary>
      /// <typeparam name="T">Generic type parameter</typeparam>
      /// <param name="type"></param>
      /// <returns>A list of</returns>
      [NotNull]
      [ItemNotNull]
      public static IEnumerable ForEachPossibleFlag<T>([NotNull] this Enum type)
      {
        TypeCode typeCode = type.GetTypeCode();
        foreach (T obj in Enum.GetValues(typeof (T)))
        {
          ulong num = FlagsExtensions.ToULong((object) obj, typeCode);
          if (num != 0UL && ((long) num & (long) num - 1L) != 0L)
            yield return (object) obj;
        }
      }

      /// <summary>Итератор по всем "поднятым" флагам enum-а</summary>
      /// <typeparam name="T">Generic type parameter</typeparam>
      /// <param name="type"></param>
      /// <returns>A list of</returns>
      [NotNull]
      [ItemNotNull]
      public static IEnumerable ForEachFlag<T>([NotNull] this Enum type)
      {
        TypeCode typeCode = type.GetTypeCode();
        ulong typeUInt64 = FlagsExtensions.ToULong((object) type, typeCode);
        if (typeUInt64 != 0UL)
        {
          foreach (T obj in Enum.GetValues(typeof (T)))
          {
            ulong num = FlagsExtensions.ToULong((object) obj, typeCode);
            if (num != 0UL && ((long) num & (long) num - 1L) != 0L && ((long) typeUInt64 & (long) num) == (long) num)
              yield return (object) obj;
          }
        }
      }

      /// <summary>Для всех новых флагов (относительно другого набора флага) вызвать лямбда-функцию</summary>
      /// <typeparam name="T">Generic type parameter</typeparam>
      /// <param name="type"></param>
      /// <param name="value">Базовый набор флагов</param>
      /// <returns>A list of</returns>
      [NotNull]
      [ItemNotNull]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static IEnumerable ForEachNewFlag<T>([NotNull] this Enum type, T value) where T : struct
      {
        TypeCode typeCode = type.GetTypeCode();
        if (FlagsExtensions.ToULong((object) type, typeCode) != 0UL)
        {
          ulong onlyNewFlagsUInt64 = FlagsExtensions.ToULong((object) type.RemoveFlags(value), typeCode);
          if (onlyNewFlagsUInt64 != 0UL)
          {
            foreach (T obj in Enum.GetValues(typeof (T)))
            {
              ulong num = FlagsExtensions.ToULong((object) obj, typeCode);
              if (num != 0UL && ((long) num & (long) num - 1L) == 0L && ((long) onlyNewFlagsUInt64 & (long) num) == (long) num)
                yield return (object) obj;
            }
          }
        }
      }
    }
}
