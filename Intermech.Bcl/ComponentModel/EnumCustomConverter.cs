
// Type: Intermech.ComponentModel.EnumCustomConverter
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Diagnostics;
using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;


namespace Intermech.ComponentModel
{
    /// <summary>EnumConverter поддерживающий DescriptionAttribute.
    /// Не поддерживает Enum с [FlagsAttribute]
    /// </summary>
    public class EnumCustomConverter : EnumConverter
    {
      /// <summary>Тип перечисления с которым был создан конвертер</summary>
      private readonly Type _enumType;
      /// <summary>Список допустимых значений, отличный от стандартного.</summary>
      private readonly StandardValuesCollection _customValues;

      /// <summary>Получить описание из DescriptionAttribute для типа Enum</summary>
      /// <param name="value">Значение Enum</param>
      /// <returns>Описание из атрибута DescriptionAttribute если есть у типа,
      /// иначе value.GetType().ToString()</returns>
      public static string GetEnumClassDescription([NotNull] Enum value)
      {
        return EnumCustomConverter.GetTypeDescription(value.GetType());
      }

      /// <summary>Получить описание из DescriptionAttribute для типа</summary>
      /// <param name="type">Тип</param>
      /// <returns>Описание из атрибута DescriptionAttribute если есть,
      /// иначе type.ToString()</returns>
      public static string GetTypeDescription(Type type)
      {
        Type underlyingType = Nullable.GetUnderlyingType(type);
        if (underlyingType != (Type) null)
          type = underlyingType;
        DescriptionAttribute customAttribute = (DescriptionAttribute) Attribute.GetCustomAttribute((MemberInfo) type, typeof (DescriptionAttribute));
        return customAttribute == null ? type.ToString() : customAttribute.Description;
      }

      /// <summary>Получить описание из DescriptionAttribute для значения Enum</summary>
      /// <param name="value">Значение</param>
      /// <returns>Описание из атрибута DescriptionAttribute если есть,
      /// иначе value.ToString()</returns>
      public static string GetEnumDescription([NotNull] Enum value)
      {
        Type nullableType = value.GetType();
        Type underlyingType = Nullable.GetUnderlyingType(nullableType);
        if (underlyingType != (Type) null)
          nullableType = underlyingType;
        DescriptionAttribute[] customAttributes = (DescriptionAttribute[]) nullableType.GetField(value.ToString()).GetCustomAttributes(typeof (DescriptionAttribute), false);
        return customAttributes.Length == 0 ? value.ToString() : customAttributes[0].Description;
      }

      /// <summary>Получить описание из DescriptionAttribute для значение Enum</summary>
      /// <param name="valueType">Тип значения Enum</param>
      /// <param name="valueName">Имя значения</param>
      /// <returns>Описание из DescriptionAttribute для значения если есть, иначе имя значения</returns>
      public static string GetEnumDescription(Type valueType, string valueName)
      {
        Type underlyingType = Nullable.GetUnderlyingType(valueType);
        if (underlyingType != (Type) null)
          valueType = underlyingType;
        DescriptionAttribute[] customAttributes = (DescriptionAttribute[]) valueType.GetField(valueName).GetCustomAttributes(typeof (DescriptionAttribute), false);
        return customAttributes.Length == 0 ? valueName : customAttributes[0].Description;
      }

      /// <summary>Получить описание из CategoryAttribute для данного типа Enum</summary>
      /// <param name="valueType">Тип значения Enum</param>
      /// <param name="valueName">Имя значения</param>
      /// <returns>Имя категории из CategoryAttribute для значения если есть, иначе пустую строку</returns>
      public static string GetEnumCategory([NotNull] Enum value)
      {
        Type nullableType = value.GetType();
        Type underlyingType = Nullable.GetUnderlyingType(nullableType);
        if (underlyingType != (Type) null)
          nullableType = underlyingType;
        CategoryAttribute[] customAttributes = (CategoryAttribute[]) nullableType.GetField(value.ToString()).GetCustomAttributes(typeof (CategoryAttribute), false);
        return customAttributes.Length == 0 ? string.Empty : customAttributes[0].Category;
      }

      /// <summary>Получить значение Enum, основанное на описании из DescriptionAttribute или имени значения.
      /// Описание имеет приоритет в конфликте.</summary>
      /// <param name="valueType">Тип Enum</param>
      /// <param name="descriptionOrName">Описание из DescriptionAttribute или имя значения</param>
      /// <param name="defaultValue">Значение по умолчанию</param>
      /// <returns>Значение Enum, основанное на описании из DescriptionAttribute или имени значения</returns>
      public static object GetEnumValue(Type valueType, string descriptionOrName, object defaultValue)
      {
        Type underlyingType = Nullable.GetUnderlyingType(valueType);
        if (underlyingType != (Type) null)
          valueType = underlyingType;
        FieldInfo[] fields = valueType.GetFields();
        object obj = (object) null;
        foreach (FieldInfo fieldInfo in fields)
        {
          DescriptionAttribute[] customAttributes = (DescriptionAttribute[]) fieldInfo.GetCustomAttributes(typeof (DescriptionAttribute), false);
          if (customAttributes.Length != 0 && customAttributes[0].Description == descriptionOrName)
            return fieldInfo.GetValue((object) fieldInfo.Name);
          if (fieldInfo.Name == descriptionOrName)
            obj = fieldInfo.GetValue((object) fieldInfo.Name);
        }
        return obj ?? defaultValue;
      }

      /// <summary>Получить значение Enum, основанное на описании из DescriptionAttribute или имени значения.
      /// Описание имеет приоритет в конфликте.</summary>
      /// <param name="valueType">Тип Enum</param>
      /// <param name="descriptionOrName">Описание из DescriptionAttribute или имя значения</param>
      /// <returns>Значение Enum, основанное на описании из DescriptionAttribute или имени значения</returns>
      public static object GetEnumValue(Type valueType, string descriptionOrName)
      {
        return EnumCustomConverter.GetEnumValue(valueType, descriptionOrName, (object) null);
      }

      /// <summary>Конструктор</summary>
      /// <param name="type">Тип Enum</param>
      public EnumCustomConverter(Type type)
        : this(type, (StandardValuesCollection) null)
      {
      }

      /// <summary>Конструктор</summary>
      /// <param name="type">Тип Enum</param>
      /// <param name="customValues">Список допустимых значений, отличный от стандартного</param>
      public EnumCustomConverter(
        Type type,
        StandardValuesCollection customValues)
        : base(type)
      {
        this._enumType = type;
        this._customValues = customValues;
      }

      /// <summary>Преобразует данное значение в заданный тип, используя заданные
      /// контекстную информацию и информацию о культурной среде</summary>
      /// <param name="context">ITypeDescriptorContext, предоставляющий контекстную информацию о формате</param>
      /// <param name="culture">Объект CultureInfo. Если передается значение пустая ссылка,
      /// то предполагается использование информации о культурной среде</param>
      /// <param name="value">Объект Object, который нужно преобразовать</param>
      /// <param name="destinationType">Type, в который требуется преобразовать параметр value</param>
      /// <returns>Объект Object, представляющий преобразованное значение</returns>
      public override object ConvertTo(
        ITypeDescriptorContext context,
        CultureInfo culture,
        object value,
        Type destinationType)
      {
        if (destinationType == typeof (string))
        {
          switch (value)
          {
            case int num when this._enumType != (Type) null && Enum.IsDefined(this._enumType, (object) num):
              return (object) EnumCustomConverter.GetEnumDescription(Enum.Parse(this._enumType, num.ToString()) as Enum);
            case Enum @enum:
              return (object) EnumCustomConverter.GetEnumDescription(@enum);
            case string valueName when this._enumType != (Type) null:
              return (object) EnumCustomConverter.GetEnumDescription(this._enumType, valueName);
          }
        }
        return base.ConvertTo(context, culture, value, destinationType);
      }

      /// <summary>Преобразует данный объект в тип этого конвертера,
      /// используя заданную контекстную информацию и информацию о культурной среде</summary>
      /// <param name="context">ITypeDescriptorContext, предоставляющий контекстную информацию о формате</param>
      /// <param name="culture">Объект CultureInfo, который нужно использовать в качестве текущей культурной среды</param>
      /// <param name="value">Объект Object, который нужно преобразовать</param>
      /// <returns>Объект Object, представляющий преобразованное значение</returns>
      public override object ConvertFrom(
        ITypeDescriptorContext context,
        CultureInfo culture,
        object value)
      {
        object obj = (object) null;
        if (this._enumType != (Type) null)
        {
          switch (value)
          {
            case string descriptionOrName:
              obj = EnumCustomConverter.GetEnumValue(this._enumType, descriptionOrName);
              break;
            case Enum @enum:
              obj = EnumCustomConverter.GetEnumValue(this._enumType, EnumCustomConverter.GetEnumDescription(@enum));
              break;
          }
        }
        return obj ?? base.ConvertFrom(context, culture, value);
      }

      /// <summary>Получает значение, показывающее, поддерживает ли этот объект стандартный
      /// набор значений, которые можно выбрать из списка, используя заданную
      /// контекстную информацию</summary>
      /// <param name="context">ITypeDescriptorContext, предоставляющий контекстную информацию о формате</param>
      /// <returns>true, если, чтобы найти стандартный набор значений, поддерживаемых данным объектом, следует
      /// вызвать метод GetStandardValues, false, если нет</returns>
      public override bool GetStandardValuesSupported(ITypeDescriptorContext context) => true;

      /// <summary>Возвращает коллекцию стандартных значений для того типа данных,
      /// которым предназначен этот конвертер типа, если предоставлена контекстная
      /// информация о формате</summary>
      /// <param name="context">ITypeDescriptorContext, предоставляющий контекстную информацию
      /// о формате, которая может быть использована для извлечения дополнительных сведений о среде,
      ///  из которой вызывается этот конвертер. Этот параметр или свойства этого параметра
      ///  могут иметь значение пустая ссылка</param>
      /// <returns>TypeConverter.StandardValuesCollection, содержащий стандартный
      /// набор допустимых значений, или пустая ссылка, если этот тип данных не поддерживает
      /// стандартный набор значений</returns>
      public override StandardValuesCollection GetStandardValues(
        ITypeDescriptorContext context)
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
            arrayList = new ArrayList(fields.Length);
          if (arrayList != null)
          {
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
