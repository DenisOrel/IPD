
// Type: Intermech.Extensions.EnumerationExtension
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;


namespace Intermech.Extensions
{
    /// <summary>Расширение системного класса <see cref="T:System.Enum" /></summary>
    public static class EnumerationExtension
    {
      /// <summary>Получить первый атрибут заданного типа</summary>
      /// <returns>Первый атрибут заданного типа, либо null</returns>
      [MustUseReturnValue]
      [CanBeNull]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static TAttribute GetAttribute<TAttribute>([NotNull] this Enum value) where TAttribute : Attribute
      {
        MemberInfo[] member = value.GetType().GetMember(value.ToString());
        if (member.Length == 0)
          return default (TAttribute);
        object[] customAttributes = member[0].GetCustomAttributes(typeof (TAttribute), false);
        return customAttributes.Length == 0 ? default (TAttribute) : (TAttribute) ((IEnumerable<object>) customAttributes).FirstOrDefault();
      }

      /// <summary>Получить описание значения enum-а из атрибута <see cref="T:System.ComponentModel.DescriptionAttribute" /> или текстовое представление значения если атрибута нет</summary>
      /// <returns>Описание значения enum-а взятое из атрибута <see cref="T:System.ComponentModel.DescriptionAttribute" /> или текстовое представление значения если атрибута нет</returns>
      [MustUseReturnValue]
      [NotNull]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static string GetDescription([NotNull] this Enum value)
      {
        value.GetType();
        return value.GetAttribute<DescriptionAttribute>()?.Description ?? value.ToString();
      }

      /// <summary>Получить описание значения enum-а из атрибута <see cref="T:System.ComponentModel.DescriptionAttribute" /> или текстовое представление значения если атрибута нет</summary>
      /// <returns>Описание значения enum-а взятое из атрибута <see cref="T:System.ComponentModel.DescriptionAttribute" /> или текстовое представление значения если атрибута нет</returns>
      [MustUseReturnValue]
      [NotNull]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static string GetDescription<TEnum>(this TEnum value) where TEnum : struct, Enum
      {
        value.GetType();
        return value.GetAttribute<DescriptionAttribute>()?.Description ?? value.ToString();
      }

      /// <summary>Получить описание значения enum-а из атрибута <see cref="T:System.ComponentModel.DescriptionAttribute" /> или null если атрибута нет</summary>
      /// <returns>Описание значения enum-а взятое из атрибута <see cref="T:System.ComponentModel.DescriptionAttribute" /> или null если атрибута нет</returns>
      [MustUseReturnValue]
      [CanBeNull]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static string FindDescription([NotNull] this Enum value)
      {
        value.GetType();
        return value.GetAttribute<DescriptionAttribute>()?.Description;
      }

      /// <summary>Получить описание значения enum-а из атрибута <see cref="T:System.ComponentModel.DescriptionAttribute" /></summary>
      /// <returns>Описание значения enum-а взятое из атрибута <see cref="T:System.ComponentModel.DescriptionAttribute" /> или null если атрибута нет</returns>
      [MustUseReturnValue]
      [CanBeNull]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static string FindDescription<TEnum>(this TEnum value) where TEnum : struct, Enum
      {
        value.GetType();
        return value.GetAttribute<DescriptionAttribute>()?.Description;
      }

      /// <summary>Получить наименование значения Enum, либо текстовое представление значения, если оно выходит за рамки значений этого типа</summary>
      /// <returns>Наименование значения, либо текстовое представление значения, если оно выходит за рамки значений этого типа</returns>
      [MustUseReturnValue]
      [NotNull]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static string GetName([NotNull] this Enum value)
      {
        return Enum.GetName(value.GetType(), (object) value) ?? value.ToString();
      }

      /// <summary>Получить наименование значения Enum, либо текстовое представление значения, если оно выходит за рамки значений этого типа</summary>
      /// <returns>Наименование значения, либо текстовое представление значения, если оно выходит за рамки значений этого типа</returns>
      [MustUseReturnValue]
      [NotNull]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static string GetName<TEnum>(this TEnum value) where TEnum : struct, Enum
      {
        return Enum.GetName(value.GetType(), (object) value) ?? value.ToString();
      }

      /// <summary>Получить наименование значения Enum, либо null, если оно выходит за рамки значений этого типа</summary>
      /// <returns>Наименование значения, либо null, если оно выходит за рамки значений этого типа</returns>
      [MustUseReturnValue]
      [CanBeNull]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static string FindName([NotNull] this Enum value)
      {
        return Enum.GetName(value.GetType(), (object) value);
      }

      /// <summary>Получить наименование значения Enum, либо null, если оно выходит за рамки значений этого типа</summary>
      /// <returns>Наименование значения, либо null, если оно выходит за рамки значений этого типа</returns>
      [MustUseReturnValue]
      [CanBeNull]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static string FindName<TEnum>(this TEnum value) where TEnum : struct, Enum
      {
        return Enum.GetName(value.GetType(), (object) value);
      }

      /// <summary>Проверка, что значение Enum содержится в переданном перечислении его значений</summary>
      [MustUseReturnValue]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static bool EnumInRange<TEnum>(this TEnum value, [NotNull] IEnumerable<TEnum> checkValues) where TEnum : struct, Enum
      {
        return checkValues.Any((Func<TEnum, bool>) (checkValue => value.Equals((object) checkValue)));
      }
    }
}
