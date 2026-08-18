
// Type: Intermech.EnumDescConverter
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;


namespace Intermech
{
    /// <summary>
    /// EnumConverter supporting System.ComponentModel.DescriptionAttribute
    /// </summary>
    public class EnumDescConverter : EnumConverter
    {
      protected Type myVal;

      /// <summary>Gets Enum Class Description Attribute</summary>
      /// <param name="value">The value you want the description attribute for</param>
      /// <returns>The description, if any, else it's .ToString()</returns>
      public static string GetEnumClassDescription(Enum value)
      {
        return EnumDescConverter.GetTypeDescription(value.GetType());
      }

      public static string GetTypeDescription(Type type)
      {
        DescriptionAttribute customAttribute = (DescriptionAttribute) Attribute.GetCustomAttribute((MemberInfo) type, typeof (DescriptionAttribute));
        return customAttribute != null ? customAttribute.Description : type.ToString();
      }

      /// <summary>Gets Enum Value's Description Attribute</summary>
      /// <param name="value">The value you want the description attribute for</param>
      /// <returns>The description, if any, else it's .ToString()</returns>
      public static string GetEnumDescription(Enum value)
      {
        return EnumDescConverter.GetEnumDescription(value.GetType(), value.ToString());
      }

      /// <summary>
      /// Gets the description for certaing named value in an Enumeration
      /// </summary>
      /// <param name="value">The type of the Enumeration</param>
      /// <param name="name">The name of the Enumeration value</param>
      /// <returns>The description, if any, else the passed name</returns>
      public static string GetEnumDescription(Type value, string name)
      {
        FieldInfo field = value.GetField(name);
        if (!(field != (FieldInfo) null))
          return name;
        DescriptionAttribute[] customAttributes = (DescriptionAttribute[]) field.GetCustomAttributes(typeof (DescriptionAttribute), false);
        return customAttributes.Length == 0 ? name : customAttributes[0].Description;
      }

      /// <summary>
      /// Gets the value of an Enum, based on it's Description Attribute or named value
      /// </summary>
      /// <param name="value">The Enum type</param>
      /// <param name="description">The description or name of the element</param>
      /// <param name="defaultValue"></param>
      /// <returns>The value, or the passed in description, if it was not found</returns>
      public static object GetEnumValue(Type value, string description, object defaultValue)
      {
        FieldInfo[] fields = value.GetFields();
        foreach (FieldInfo fieldInfo in fields)
        {
          if (fieldInfo.Name == description)
            return fieldInfo.GetValue((object) fieldInfo.Name);
        }
        foreach (FieldInfo fieldInfo in fields)
        {
          DescriptionAttribute[] customAttributes = (DescriptionAttribute[]) fieldInfo.GetCustomAttributes(typeof (DescriptionAttribute), false);
          if (customAttributes.Length != 0 && customAttributes[0].Description == description)
            return fieldInfo.GetValue((object) fieldInfo.Name);
        }
        return defaultValue;
      }

      public static object GetEnumValue(Type value, string description)
      {
        return EnumDescConverter.GetEnumValue(value, description, (object) description);
      }

      public EnumDescConverter(Type type)
        : base(type)
      {
        this.myVal = type;
      }

      public override object ConvertTo(
        ITypeDescriptorContext context,
        CultureInfo culture,
        object value,
        Type destinationType)
      {
        switch (value)
        {
          case Enum _ when destinationType == typeof (string):
            return (object) EnumDescConverter.GetEnumDescription((Enum) value);
          case string _ when destinationType == typeof (string):
            return (object) EnumDescConverter.GetEnumDescription(this.myVal, (string) value);
          default:
            return base.ConvertTo(context, culture, value, destinationType);
        }
      }

      public override object ConvertFrom(
        ITypeDescriptorContext context,
        CultureInfo culture,
        object value)
      {
        switch (value)
        {
          case string _:
            return EnumDescConverter.GetEnumValue(this.myVal, (string) value);
          case Enum _:
            return (object) EnumDescConverter.GetEnumDescription((Enum) value);
          default:
            return base.ConvertFrom(context, culture, value);
        }
      }
    }
}
