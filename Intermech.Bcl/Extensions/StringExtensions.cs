
// Type: Intermech.Extensions.StringExtensions
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Diagnostics;
using System;
using System.Runtime.CompilerServices;


namespace Intermech.Extensions
{
    public static class StringExtensions
    {
      [NotNull]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static string Truncate([NotNull] this string value, int maxChars)
      {
        return value.Length > maxChars ? value.Substring(0, maxChars) + "…" : value;
      }

      /// <summary>Получить значение перечисления по его имени</summary>
      /// <typeparam name="TEnum">Тип перечисления</typeparam>
      /// <param name="enumValue">Имя значения перечисления</param>
      /// <returns>Enum</returns>
      [MustUseReturnValue]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static TEnum ToEnum<TEnum>([NotNull] this string enumValue) where TEnum : struct, Enum
      {
        return (TEnum) Enum.Parse(typeof (TEnum), enumValue, true);
      }

      /// <summary>получить значение перечисления по его имени</summary>
      /// <typeparam name="TEnum">Тип перечисления</typeparam>
      /// <param name="enumValue">Имя  значения перечисления</param>
      /// <param name="defaultValue">Значение по умолчанию</param>
      /// <returns>Enum</returns>
      [MustUseReturnValue]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static TEnum ToEnum<TEnum>([NotNull] this string enumValue, TEnum defaultValue) where TEnum : struct, Enum
      {
        TEnum result;
        return string.IsNullOrEmpty(enumValue) || !Enum.TryParse(enumValue, true, out result) ? defaultValue : result;
      }

      /// <summary>Строка пуста. Короткая форма вызова вместо string.IsNullOrEmpty(value) - value.IsEmpty()</summary>
      /// <param name="value">Значение</param>
      /// <returns>Возвращает true, если value null или пустая строка</returns>
      [MustUseReturnValue]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static bool IsEmpty([CanBeNull] this string value) => string.IsNullOrEmpty(value);
    }
}
