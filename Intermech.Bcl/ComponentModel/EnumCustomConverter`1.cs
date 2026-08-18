
// Type: Intermech.ComponentModel.EnumCustomConverter`1
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;


namespace Intermech.ComponentModel
{
    /// <summary>EnumConverter поддерживающий DescriptionAttribute.
    /// Не поддерживает Enum с [FlagsAttribute]
    /// </summary>
    public class EnumCustomConverter<TEnum> : EnumConverter where TEnum : struct, Enum
    {
      /// <summary>Список допустимых значений, отличный от стандартного.</summary>
      [CanBeNull]
      private readonly StandardValuesCollection _customValues;

      /// <summary>Получить описание из DescriptionAttribute для типа</summary>
      /// <returns>Описание из атрибута DescriptionAttribute если есть, иначе type.ToString()</returns>
      [NotNull]
      public static string GetTypeDescription()
      {
        DescriptionAttribute customAttribute = (DescriptionAttribute) Attribute.GetCustomAttribute((MemberInfo) typeof (TEnum), typeof (DescriptionAttribute));
        return customAttribute == null ? typeof (TEnum).ToString() : customAttribute.Description ?? string.Empty;
      }

      /// <summary>Получить описание из DescriptionAttribute для значения Enum</summary>
      /// <param name="value">Значение</param>
      /// <returns>Описание из атрибута DescriptionAttribute если есть, иначе value.ToString()</returns>
      [NotNull]
      public static string GetEnumDescription([CanBeNull] Enum value)
      {
        if (value == null)
          return string.Empty;
        TEnum @enum = (TEnum) value;
        Type nullableType = value.GetType();
        Type underlyingType = Nullable.GetUnderlyingType(nullableType);
        if (underlyingType != (Type) null)
          nullableType = underlyingType;
        FieldInfo field = nullableType.GetField(value.ToString());
        if (field == (FieldInfo) null)
          return string.Empty;
        DescriptionAttribute[] customAttributes = (DescriptionAttribute[]) field.GetCustomAttributes(typeof (DescriptionAttribute), false);
        return customAttributes.Length == 0 ? value.ToString() : customAttributes[0].Description ?? string.Empty;
      }

      /// <summary>Получить описание из DescriptionAttribute для значения Enum</summary>
      /// <param name="value">Значение</param>
      /// <returns>Описание из атрибута DescriptionAttribute если есть, иначе value.ToString()</returns>
      [NotNull]
      public static string GetEnumDescription(TEnum value)
      {
        FieldInfo field = value.GetType().GetField(value.ToString());
        if (field == (FieldInfo) null)
          return string.Empty;
        DescriptionAttribute[] customAttributes = (DescriptionAttribute[]) field.GetCustomAttributes(typeof (DescriptionAttribute), false);
        return customAttributes.Length == 0 ? value.ToString() : customAttributes[0].Description ?? string.Empty;
      }

      /// <summary>Получить описание из DescriptionAttribute для значения Enum</summary>
      /// <param name="valueOrNull">Значение или null</param>
      /// <returns>Описание из атрибута DescriptionAttribute если есть, иначе value.ToString()</returns>
      [NotNull]
      public static string GetEnumDescription([CanBeNull] TEnum? valueOrNull)
      {
        if (!valueOrNull.HasValue)
          return string.Empty;
        TEnum @enum = valueOrNull.Value;
        FieldInfo field = Nullable.GetUnderlyingType(@enum.GetType()).GetField(@enum.ToString());
        if (field == (FieldInfo) null)
          return string.Empty;
        DescriptionAttribute[] customAttributes = (DescriptionAttribute[]) field.GetCustomAttributes(typeof (DescriptionAttribute), false);
        return customAttributes.Length == 0 ? @enum.ToString() : customAttributes[0].Description ?? string.Empty;
      }

      /// <summary>Получить описание из DescriptionAttribute для значение Enum</summary>
      /// <param name="valueType">Тип значения Enum</param>
      /// <param name="valueName">Имя значения</param>
      /// <returns>Описание из DescriptionAttribute для значения если есть, иначе имя значения</returns>
      [NotNull]
      public static string GetEnumDescription([NotNull] Type valueType, [NotNull, NotWhitespace] string valueName)
      {
        Type underlyingType = Nullable.GetUnderlyingType(valueType);
        if (underlyingType != (Type) null)
          valueType = underlyingType;
        FieldInfo field = valueType.GetField(valueName);
        if (field == (FieldInfo) null)
          return string.Empty;
        DescriptionAttribute[] customAttributes = (DescriptionAttribute[]) field.GetCustomAttributes(typeof (DescriptionAttribute), false);
        return customAttributes.Length == 0 ? valueName : customAttributes[0].Description ?? string.Empty;
      }

      /// <summary>Получить описание из CategoryAttribute для данного типа Enum</summary>
      /// <param name="value">Значение</param>
      /// <returns>Имя категории из CategoryAttribute для значения если есть, иначе пустую строку</returns>
      [NotNull]
      public static string GetEnumCategory([CanBeNull] Enum value)
      {
        if (value == null)
          return string.Empty;
        TEnum @enum = (TEnum) value;
        Type nullableType = value.GetType();
        Type underlyingType = Nullable.GetUnderlyingType(nullableType);
        if (underlyingType != (Type) null)
          nullableType = underlyingType;
        FieldInfo field = nullableType.GetField(value.ToString());
        if (field == (FieldInfo) null)
          return string.Empty;
        CategoryAttribute[] customAttributes = (CategoryAttribute[]) field.GetCustomAttributes(typeof (CategoryAttribute), false);
        return customAttributes.Length == 0 ? string.Empty : customAttributes[0].Category ?? string.Empty;
      }

      /// <summary>Получить описание из CategoryAttribute для данного типа Enum</summary>
      /// <param name="value">Значение</param>
      /// <returns>Имя категории из CategoryAttribute для значения если есть, иначе пустую строку</returns>
      [NotNull]
      public static string GetEnumCategory(TEnum value)
      {
        FieldInfo field = value.GetType().GetField(value.ToString());
        if (field == (FieldInfo) null)
          return string.Empty;
        CategoryAttribute[] customAttributes = (CategoryAttribute[]) field.GetCustomAttributes(typeof (CategoryAttribute), false);
        return customAttributes.Length == 0 ? string.Empty : customAttributes[0].Category ?? string.Empty;
      }

      /// <summary>Получить описание из CategoryAttribute для данного типа Enum</summary>
      /// <param name="valueOrNull">Значение или null</param>
      /// <returns>Имя категории из CategoryAttribute для значения если есть, иначе пустую строку</returns>
      [NotNull]
      public static string GetEnumCategory([CanBeNull] TEnum? valueOrNull)
      {
        if (!valueOrNull.HasValue)
          return string.Empty;
        TEnum @enum = valueOrNull.Value;
        FieldInfo field = Nullable.GetUnderlyingType(@enum.GetType()).GetField(@enum.ToString());
        if (field == (FieldInfo) null)
          return string.Empty;
        CategoryAttribute[] customAttributes = (CategoryAttribute[]) field.GetCustomAttributes(typeof (CategoryAttribute), false);
        return customAttributes.Length == 0 ? string.Empty : customAttributes[0].Category ?? string.Empty;
      }

      /// <summary>Получить значение Enum, основанное на описании из DescriptionAttribute или имени значения. Описание имеет
      /// приоритет в конфликте.</summary>
      /// <param name="descriptionOrName">Описание из DescriptionAttribute или имя значения</param>
      /// <param name="defaultValue">(Optional) Значение по умолчанию</param>
      /// <returns>Значение Enum, основанное на описании из DescriptionAttribute или имени значения</returns>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static TEnum GetEnumValue([NotNull, NotWhitespace] string descriptionOrName, TEnum defaultValue = default (TEnum))
      {
        TEnum result;
        return !EnumCustomConverter<TEnum>.TryGetEnumValue(descriptionOrName, out result) ? defaultValue : result;
      }

      /// <summary>Получить значение Enum, основанное на описании из DescriptionAttribute или имени значения. Описание имеет
      /// приоритет в конфликте.</summary>
      /// <param name="descriptionOrName">Описание из DescriptionAttribute или имя значения</param>
      /// <returns>Значение Enum, основанное на описании из DescriptionAttribute или имени значения</returns>
      [CanBeNull]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static TEnum? GetEnumValueOrNull([NotNull, NotWhitespace] string descriptionOrName)
      {
        TEnum result;
        return !EnumCustomConverter<TEnum>.TryGetEnumValue(descriptionOrName, out result) ? new TEnum?() : new TEnum?(result);
      }

      /// <summary>Получить значение Enum, основанное на описании из DescriptionAttribute или имени значения. Описание имеет
      /// приоритет в конфликте.</summary>
      /// <exception cref="T:System.Collections.Generic.KeyNotFoundException">Thrown when a Key Not Found error condition occurs</exception>
      /// <param name="descriptionOrName">Описание из DescriptionAttribute или имя значения</param>
      /// <returns>Значение Enum, основанное на описании из DescriptionAttribute или имени значения</returns>
      [TypeConverter]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static TEnum GetEnumValueOrThrowException([NotNull, NotWhitespace] string descriptionOrName)
      {
        TEnum result;
        if (!EnumCustomConverter<TEnum>.TryGetEnumValue(descriptionOrName, out result))
          throw new KeyNotFoundException($"Value \"{descriptionOrName}\" not found in {typeof (TEnum)}");
        return result;
      }

      /// <summary>Получить значение Enum, основанное на описании из DescriptionAttribute или имени значения. Описание имеет
      /// приоритет в конфликте.</summary>
      /// <param name="descriptionOrName">Описание из DescriptionAttribute или имя значения</param>
      /// <param name="result">[out] Значение Enum, основанное на описании из DescriptionAttribute или имени значения</param>
      /// <returns>True если значение найдено, иначе false</returns>
      public static bool TryGetEnumValue([NotNull, NotWhitespace] string descriptionOrName, out TEnum result)
      {
        foreach (FieldInfo field in typeof (TEnum).GetFields())
        {
          DescriptionAttribute[] customAttributes = (DescriptionAttribute[]) field.GetCustomAttributes(typeof (DescriptionAttribute), false);
          if (customAttributes.Length != 0 && string.Equals(customAttributes[0].Description, descriptionOrName, StringComparison.InvariantCulture) && field.GetValue((object) field.Name) is TEnum enum1)
          {
            result = enum1;
            return true;
          }
          if (string.Equals(field.Name, descriptionOrName, StringComparison.InvariantCulture) && field.GetValue((object) field.Name) is TEnum enum2)
          {
            result = enum2;
            return true;
          }
        }
        result = default (TEnum);
        return false;
      }

      /// <summary>Конструктор</summary>
      /// <param name="customValues">Список допустимых значений, отличный от стандартного</param>
      public EnumCustomConverter(
        [CanBeNull] StandardValuesCollection customValues)
        : base(typeof (TEnum))
      {
        this._customValues = customValues;
      }

      /// <summary>Преобразует данное значение в заданный тип, используя заданные контекстную информацию и информацию о
      /// культурной среде</summary>
      /// <param name="context">ITypeDescriptorContext, предоставляющий контекстную информацию о формате</param>
      /// <param name="culture">Объект CultureInfo. Если передается значение пустая ссылка, то предполагается использование
      /// информации о культурной среде</param>
      /// <param name="value">Объект Object, который нужно преобразовать</param>
      /// <param name="destinationType">Type, в который требуется преобразовать параметр value</param>
      /// <returns>Объект Object, представляющий преобразованное значение</returns>
      [CanBeNull]
      public override object ConvertTo(
        [CanBeNull] ITypeDescriptorContext context,
        [NotNull] CultureInfo culture,
        [CanBeNull] object value,
        [NotNull] Type destinationType)
      {
        if (value == null)
          return (object) null;
        if (destinationType == typeof (string))
        {
          switch (value)
          {
            case TEnum enum1:
              return (object) EnumCustomConverter<TEnum>.GetEnumDescription(enum1);
            case int num1 when Enum.IsDefined(this.EnumType, (object) num1):
              return (object) EnumCustomConverter<TEnum>.GetEnumDescription((TEnum) Enum.Parse(this.EnumType, num1.ToString()));
            case long num2 when Enum.IsDefined(this.EnumType, (object) num2):
              return (object) EnumCustomConverter<TEnum>.GetEnumDescription((TEnum) Enum.Parse(this.EnumType, num2.ToString()));
            case Enum enum2:
              return (object) EnumCustomConverter<TEnum>.GetEnumDescription(enum2);
            case string valueName:
              return (object) EnumCustomConverter<TEnum>.GetEnumDescription(this.EnumType, valueName);
          }
        }
        return base.ConvertTo(context, culture, value, destinationType);
      }

      /// <summary>Преобразует данное значение в заданный тип, используя заданные контекстную информацию и информацию о
      /// культурной среде</summary>
      /// <typeparam name="T">Тип, в который требуется преобразовать параметр value</typeparam>
      /// <param name="context">ITypeDescriptorContext, предоставляющий контекстную информацию о формате</param>
      /// <param name="culture">Объект CultureInfo. Если передается значение пустая ссылка, то предполагается использование
      /// информации о культурной среде</param>
      /// <param name="value">Объект Object, который нужно преобразовать</param>
      /// <returns>Объект Object, представляющий преобразованное значение</returns>
      [CanBeNull]
      public T ConvertTo<T>([CanBeNull] ITypeDescriptorContext context, [NotNull] CultureInfo culture, [CanBeNull] object value)
      {
        return (T) this.ConvertTo(context, culture, value, typeof (T));
      }

      /// <summary>Преобразует данный объект в тип этого конвертера, используя заданную контекстную информацию и информацию о
      /// культурной среде</summary>
      /// <param name="context">ITypeDescriptorContext, предоставляющий контекстную информацию о формате</param>
      /// <param name="culture">Объект CultureInfo, который нужно использовать в качестве текущей культурной среды</param>
      /// <param name="value">Объект Object, который нужно преобразовать</param>
      /// <returns>Объект Object, представляющий преобразованное значение</returns>
      [CanBeNull]
      public override object ConvertFrom(
        [NotNull] ITypeDescriptorContext context,
        [NotNull] CultureInfo culture,
        [CanBeNull] object value)
      {
        switch (value)
        {
          case null:
            return (object) null;
          case TEnum enum1:
            return (object) EnumCustomConverter<TEnum>.GetEnumValueOrNull(EnumCustomConverter<TEnum>.GetEnumDescription(enum1));
          case string descriptionOrName:
            return (object) EnumCustomConverter<TEnum>.GetEnumValueOrNull(descriptionOrName) ?? base.ConvertFrom(context, culture, value);
          case int _:
          case long _ when Enum.IsDefined(typeof (TEnum), value):
            return (object) (TEnum) Convert.ChangeType(value, typeof (TEnum));
          case Enum enum2:
            return (object) EnumCustomConverter<TEnum>.GetEnumValueOrNull(EnumCustomConverter<TEnum>.GetEnumDescription(enum2));
          default:
            return base.ConvertFrom(context, culture, value);
        }
      }

      /// <summary>Преобразует данный объект в тип этого конвертера, используя заданную контекстную информацию и информацию о
      /// культурной среде</summary>
      /// <param name="context">ITypeDescriptorContext, предоставляющий контекстную информацию о формате</param>
      /// <param name="culture">Объект CultureInfo, который нужно использовать в качестве текущей культурной среды</param>
      /// <param name="value">Объект Object, который нужно преобразовать</param>
      /// <param name="defaultValue">(Optional) Значение по умолчанию</param>
      /// <returns>Значение Enum</returns>
      public TEnum TypedConvertFrom(
        [NotNull] ITypeDescriptorContext context,
        [NotNull] CultureInfo culture,
        [CanBeNull] object value,
        TEnum defaultValue = default (TEnum))
      {
        if (value == null)
          return defaultValue;
        if (value is TEnum enum1)
          return enum1;
        if (value is string descriptionOrName)
        {
          TEnum result;
          if (EnumCustomConverter<TEnum>.TryGetEnumValue(descriptionOrName, out result))
            return result;
          if (base.ConvertFrom(context, culture, value) is TEnum enum2)
            return enum2;
        }
        if ((value is int || value is long) && Enum.IsDefined(typeof (TEnum), value))
          return (TEnum) Convert.ChangeType(value, typeof (TEnum));
        TEnum result1;
        if (value is Enum enum3 && EnumCustomConverter<TEnum>.TryGetEnumValue(EnumCustomConverter<TEnum>.GetEnumDescription(enum3), out result1))
          return result1;
        return base.ConvertFrom(context, culture, value) is TEnum enum4 ? enum4 : defaultValue;
      }

      /// <summary>Получает значение, показывающее, поддерживает ли этот объект стандартный набор значений, которые можно выбрать
      /// из списка, используя заданную контекстную информацию</summary>
      /// <param name="context">ITypeDescriptorContext, предоставляющий контекстную информацию о формате</param>
      /// <returns>true, если, чтобы найти стандартный набор значений, поддерживаемых данным объектом, следует вызвать метод
      /// GetStandardValues, false, если нет</returns>
      public override bool GetStandardValuesSupported([CanBeNull] ITypeDescriptorContext context)
      {
        return true;
      }

      /// <summary>Возвращает коллекцию стандартных значений для того типа данных, которым предназначен этот конвертер типа, если
      /// предоставлена контекстная информация о формате</summary>
      /// <param name="context">ITypeDescriptorContext, предоставляющий контекстную информацию о формате, которая может быть
      /// использована для извлечения дополнительных сведений о среде, из которой вызывается этот
      /// конвертер. Этот параметр или свойства этого параметра могут иметь значение пустая ссылка</param>
      /// <returns>TypeConverter.StandardValuesCollection, содержащий стандартный набор допустимых значений, или пустая ссылка,
      /// если этот тип данных не поддерживает стандартный набор значений</returns>
      public override StandardValuesCollection GetStandardValues(
        [NotNull] ITypeDescriptorContext context)
      {
        if (this.Values == null)
        {
          if (this._customValues != null)
          {
            this.Values = this._customValues;
            return this.Values;
          }
          Type type = TypeDescriptor.GetReflectionType(this.EnumType);
          Type underlyingType = Nullable.GetUnderlyingType(type);
          if (underlyingType != (Type) null)
            type = underlyingType;
          FieldInfo[] fields = type.GetFields(BindingFlags.Static | BindingFlags.Public);
          ArrayList arrayList = (ArrayList) null;
          if (fields.Length != 0)
          {
            arrayList = new ArrayList(fields.Length);
            foreach (FieldInfo fieldInfo in fields)
            {
              BrowsableAttribute browsableAttribute = (BrowsableAttribute) null;
              foreach (Attribute customAttribute in fieldInfo.GetCustomAttributes(typeof (BrowsableAttribute), false))
                browsableAttribute = customAttribute as BrowsableAttribute;
              if ((browsableAttribute != null ? (browsableAttribute.Browsable ? 1 : 0) : 1) != 0)
              {
                object obj = (object) null;
                try
                {
                  obj = Enum.Parse(type, fieldInfo.Name);
                }
                catch (ArgumentException ex)
                {
                }
                if (obj != null)
                  arrayList.Add(obj);
              }
            }
            IComparer comparer = this.Comparer;
            if (comparer != null)
              arrayList.Sort(comparer);
          }
          this.Values = new StandardValuesCollection((ICollection) arrayList?.ToArray());
        }
        return this.Values;
      }
    }
}
