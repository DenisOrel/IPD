
// Type: Intermech.Interfaces.ObjectsCompareHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections;
using System.Collections.Generic;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Вспомогательный класс для сравнения объектов, списков и словариков.
    /// Допускается сравнение списков и словариков, элементы которых также
    /// содержат списки и(или) словарики, без ограничения уровней вложенности
    /// </summary>
    public static class ObjectsCompareHelper
    {
      /// <summary>
      /// Преобразовать (если возможно) указанное значение к .NET-типу данных, соответствующему
      /// указанному типу из списка FieldTypes
      /// </summary>
      /// <param name="value">Преобразуемое значение</param>
      /// <param name="valueType">Требуемый тип данных</param>
      /// <returns>Преобразованное значение или оригинальное, если преобразование невозможно</returns>
      public static object Convert(object value, FieldTypes valueType)
      {
        if (value != null && value != DBNull.Value)
        {
          switch (valueType)
          {
            case FieldTypes.ftUnknown:
              break;
            case FieldTypes.ftString:
              return value is string ? (object) (string) value : (object) value.ToString();
            case FieldTypes.ftInteger:
            case FieldTypes.ftAutoInc:
              if (value is long num1)
                return (object) num1;
              long result1;
              return long.TryParse(value.ToString(), out result1) ? (object) result1 : value;
            case FieldTypes.ftDouble:
              if (value is double num2)
                return (object) num2;
              double result2;
              return double.TryParse(value.ToString(), out result2) ? (object) result2 : value;
            case FieldTypes.ftDateTime:
              if (value is DateTime dateTime)
                return (object) dateTime;
              DateTime result3;
              return DateTime.TryParse(value.ToString(), out result3) ? (object) result3 : value;
            case FieldTypes.ftBoolean:
              if (value is bool flag)
                return (object) flag;
              bool result4;
              return bool.TryParse(value.ToString(), out result4) ? (object) result4 : value;
            default:
              return value;
          }
        }
        return value;
      }

      /// <summary>Сравнить два объекта</summary>
      /// <param name="obj1">Первый объект</param>
      /// <param name="obj2">Второй объект</param>
      /// <param name="valueType">Ожидаемый тип данных у значений</param>
      /// <returns>true - объекты равны</returns>
      public static bool CompareValues(object obj1, object obj2, FieldTypes valueType)
      {
        return ObjectsCompareHelper.Convert(obj1, valueType).Equals(ObjectsCompareHelper.Convert(obj2, valueType));
      }

      /// <summary>Сравнить два объекта</summary>
      /// <param name="obj1">Первый объект</param>
      /// <param name="obj2">Второй объект</param>
      /// <returns>true - объекты равны</returns>
      public static bool CompareObjects(object obj1, object obj2)
      {
        if (obj1 == obj2)
          return true;
        if (obj1 == null && obj2 != null || obj1 != null && obj2 == null || !ObjectsCompareHelper.CompareLists(obj1 as IList, obj2 as IList) || !ObjectsCompareHelper.CompareDictionaries(obj1 as IDictionary, obj2 as IDictionary))
          return false;
        switch (obj1)
        {
          case IList _:
          case IDictionary _:
            return true;
          case string _:
            if (obj2 is string)
              return StringComparer.CurrentCultureIgnoreCase.Compare(obj1, obj2) == 0;
            break;
        }
        return object.Equals(obj1, obj2);
      }

      /// <summary>Сравнить поэлементно два списка</summary>
      /// <param name="list1">Первый список</param>
      /// <param name="list2">Второй список</param>
      /// <returns>true - списки идентичны</returns>
      public static bool CompareLists(IList list1, IList list2)
      {
        if (list1 == list2)
          return true;
        if (list1 == null || list2 == null || list1.Count != list2.Count)
          return false;
        if (list1.Count == list2.Count && list1.Count == 0)
          return true;
        for (int index = 0; index < list1.Count; ++index)
        {
          if (!ObjectsCompareHelper.CompareObjects(list1[index], list2[index]))
            return false;
        }
        return true;
      }

      /// <summary>Сравнить поэлементно два списка</summary>
      /// <param name="list1">Первый список</param>
      /// <param name="list2">Второй список</param>
      /// <returns>true - списки идентичны</returns>
      public static bool CompareLists<T>(IList<T> list1, IList<T> list2)
      {
        if (list1 == list2)
          return true;
        if (list1 == null || list2 == null || list1.Count != list2.Count)
          return false;
        if (list1.Count == list2.Count && list1.Count == 0)
          return true;
        for (int index = 0; index < list1.Count; ++index)
        {
          if (!ObjectsCompareHelper.CompareObjects((object) list1[index], (object) list2[index]))
            return false;
        }
        return true;
      }

      /// <summary>Сравнить поэлементно два словарика</summary>
      /// <param name="dict1">Первый словарик</param>
      /// <param name="dict2">Второй словарик</param>
      /// <returns>true - словарики идентичны</returns>
      public static bool CompareDictionaries(IDictionary dict1, IDictionary dict2)
      {
        if (dict1 == dict2)
          return true;
        if (dict1 == null || dict2 == null || dict1.Count != dict2.Count)
          return false;
        if (dict1.Count == dict2.Count && dict1.Count == 0)
          return true;
        foreach (DictionaryEntry dictionaryEntry in dict1)
        {
          object key = dict2.Contains(dictionaryEntry.Key) ? dictionaryEntry.Key : (object) null;
          if (dictionaryEntry.Key != null && key == null || !ObjectsCompareHelper.CompareObjects(dictionaryEntry.Value, dict2[key]))
            return false;
        }
        return true;
      }

      /// <summary>Сравнить два объекта</summary>
      /// <param name="obj1">Первый объект</param>
      /// <param name="obj2">Второй объект</param>
      /// <param name="valueType">Ожидаемый тип данных у значений</param>
      /// <returns>-1, 0, 1</returns>
      public static int CompareTo(object obj1, object obj2, FieldTypes valueType)
      {
        return ObjectsCompareHelper.CompareTo(ObjectsCompareHelper.Convert(obj1, valueType), ObjectsCompareHelper.Convert(obj2, valueType));
      }

      /// <summary>Сравнить два объекта</summary>
      /// <param name="value1">Первый объект</param>
      /// <param name="value2">Второй объект</param>
      /// <returns>-1, 0, 1</returns>
      public static int CompareTo(object value1, object value2)
      {
        if (value1 == null && value2 == null || value1 == DBNull.Value && value2 == DBNull.Value)
          return 0;
        if ((value1 == null || value1 == DBNull.Value) && value2 != null && value2 != DBNull.Value)
          return -1;
        if (value1 != null && value1 != DBNull.Value && (value2 == null || value2 == DBNull.Value))
          return 1;
        if (!value1.GetType().IsAssignableFrom(value2.GetType()))
          return 0;
        IComparable comparable1 = value1 as IComparable;
        IComparable comparable2 = value2 as IComparable;
        return comparable1 == null || comparable2 == null ? 0 : comparable1.CompareTo((object) comparable2);
      }
    }
}
