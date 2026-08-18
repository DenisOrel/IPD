
// Type: Intermech.Interfaces.Enums
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;


namespace Intermech.Interfaces
{
    public static class Enums
    {
      /// <summary>преобразовать строку в обьект</summary>
      /// <typeparam name="T">тип обьекта</typeparam>
      /// <param name="value">строка с обьектом</param>
      /// <returns>обьект</returns>
      public static T Convert<T>(this string value)
      {
        TypeConverter converter = TypeDescriptor.GetConverter(typeof (T));
        return converter != null ? (T) converter.ConvertFromString(value) : default (T);
      }

      /// <summary>получить имя перечислителя</summary>
      /// <param name="value">Перечислитель</param>
      /// <returns>Имя перечислителя</returns>
      [Obsolete("Use GetName instead", true)]
      public static string Name(this Enum value) => Enum.GetName(value.GetType(), (object) value);

      /// <summary>
      /// Gets the string of an DescriptionAttribute of an Enum.
      /// </summary>
      /// <param name="enumValue">The Enum value for which the description is needed.</param>
      /// <returns>If a DescriptionAttribute is set it return the content of it.
      /// Otherwise just the raw name as string.</returns>
      [Obsolete("Use GetDescription instead", true)]
      public static string Description(this Enum enumValue)
      {
        DescriptionAttribute attribute = enumValue.GetAttribute<DescriptionAttribute>();
        return attribute != null ? attribute.Description : enumValue.ToString();
      }

      /// <summary>
      /// Creates an List with all keys and values of a given Enum class
      /// </summary>
      /// <typeparam name="TEnum">Must be derived from class Enum!</typeparam>
      /// <returns>A list of KeyValuePair&lt;Enum, string&gt; with all available
      /// names and values of the given Enum.</returns>
      public static IList<KeyValuePair<TEnum, string>> ToList<TEnum>() where TEnum : struct, Enum, IConvertible, IComparable, IFormattable
      {
        return (IList<KeyValuePair<TEnum, string>>) Enum.GetValues(typeof (TEnum)).OfType<TEnum>().Select<TEnum, KeyValuePair<TEnum, string>>((Func<TEnum, KeyValuePair<TEnum, string>>) (e => new KeyValuePair<TEnum, string>(e, e.GetDescription<TEnum>()))).ToArray<KeyValuePair<TEnum, string>>();
      }

      internal static TEnum GetValueFromDescription<TEnum>(string description) where TEnum : struct, Enum, IConvertible, IComparable, IFormattable
      {
        foreach (FieldInfo field in typeof (TEnum).GetFields())
        {
          if (Attribute.GetCustomAttribute((MemberInfo) field, typeof (DescriptionAttribute)) is DescriptionAttribute customAttribute)
          {
            if (customAttribute.Description == description)
              return (TEnum) field.GetValue((object) null);
          }
          else if (field.Name == description)
            return (TEnum) field.GetValue((object) null);
        }
        throw new ArgumentOutOfRangeException(nameof (description));
      }

      /// <summary>Преобразовать пиксели в миллиметры для прямоугольника</summary>
      /// <param name="pixels">Прямоугольник в пикселях</param>
      /// <param name="dpi">Точек на дюйм</param>
      /// <returns>Прямоугольник в миллиметрах</returns>
      public static RectangleF PixelsToMm(this RectangleF pixels, PointF dpi)
      {
        return new RectangleF(pixels.X * (25.4f / dpi.X), pixels.Y * (25.4f / dpi.Y), pixels.Width * (25.4f / dpi.X), pixels.Height * (25.4f / dpi.Y));
      }

      /// <summary>Преобразовать пиксели в миллиметры для точки</summary>
      /// <param name="pixels">Точка в пикселях</param>
      /// <param name="dpi">Точек на дюйм</param>
      /// <returns>Точка в миллиметрах</returns>
      public static PointF PixelsToMm(this Point pixels, PointF dpi)
      {
        return new PointF((float) pixels.X * (25.4f / dpi.X), (float) pixels.Y * (25.4f / dpi.Y));
      }

      /// <summary>преобразование строки</summary>
      /// <param name="value">строка с числом</param>
      /// <param name="result">число</param>
      /// <returns>true - преобразование успешно</returns>
      public static bool TryParse(this string value, out float result)
      {
        NumberFormatInfo provider = new NumberFormatInfo();
        provider.NumberDecimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
        if (float.TryParse(value, NumberStyles.Any, (IFormatProvider) provider, out result))
          return true;
        provider.NumberDecimalSeparator = provider.NumberDecimalSeparator.Equals(".") ? "," : ".";
        return float.TryParse(value, NumberStyles.Any, (IFormatProvider) provider, out result);
      }
    }
}
