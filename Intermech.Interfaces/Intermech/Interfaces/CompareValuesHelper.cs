
// Type: Intermech.Interfaces.CompareValuesHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Collections;
using Intermech.Interfaces.CompareValues;
using System;
using System.Collections.Generic;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Класс поддержки сравнения значений, в методы подается
    /// всякий мусор и тут мы пытаемся его нормализовать и сравнить
    /// </summary>
    public class CompareValuesHelper
    {
      /// <summary>Сравниваем объекты</summary>
      public static bool CompareObjectValues(object val1, object val2)
      {
        return new ObjectComparer().Compare(val1, val2);
      }

      /// <summary>Сравниваем целые</summary>
      public static bool CompareIntValues(object val1, object val2)
      {
        return new IntegerComparer().Compare(val1, val2);
      }

      /// <summary>Сравниваем плавающие</summary>
      public static bool CompareFloatValues(object val1, object val2)
      {
        return new FloatComparer().Compare(val1, val2);
      }

      /// <summary>Сравниваем строки</summary>
      public static bool CompareStringValues(object val1, object val2)
      {
        return new Intermech.Interfaces.CompareValues.StringComparer().Compare(val1, val2);
      }

      /// <summary>Сравниваем логические</summary>
      public static bool CompareBoolValues(object val1, object val2)
      {
        return new BooleanComparer().Compare(val1, val2);
      }

      /// <summary>Сравниваем время</summary>
      public static bool CompareDateTimeValues(object val1, object val2)
      {
        return new DateTimeComparer().Compare(val1, val2);
      }

      public static bool CompareMeasuredValues(object val1, object val2)
      {
        return new MeasureComparer().Compare(val1, val2);
      }

      /// <summary>Нормализация значения</summary>
      public static object NormalizedValue(object value)
      {
        return value == null || value == DBNull.Value || !(value.ToString() != string.Empty) ? (object) null : value;
      }

      /// <summary>Сравнение массивов</summary>
      public static bool CompareCollections<T>(ICollection<T> value1, ICollection<T> value2)
      {
        if (value1 == null && value2 == null)
          return true;
        return (value1 == null || value2 != null) && (value1 != null || value2 == null) && value1.Count == value2.Count && CollectionUtils.ContentEqual<T>(value1, value2);
      }
    }
}
