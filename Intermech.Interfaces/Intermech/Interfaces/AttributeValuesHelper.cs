
// Type: Intermech.Interfaces.AttributeValuesHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech.Interfaces
{
    /// <summary>Класс впомогательных утилит для AttributeValues</summary>
    public static class AttributeValuesHelper
    {
      /// <summary>
      /// Ф-ция ищет в массиве значений атрибутов атрибут с гуидом guid и возвращает найденный элемент
      /// </summary>
      /// <param name="values">Массив значений атрибутов</param>
      /// <param name="guid">Искомый гуид атрибута</param>
      /// <param name="attributableID">Ид. объекта или связи, которому принадлежит данный массив значений атрибутов</param>
      /// <param name="throwException">Генерить ли исключение, если атрибут в массиве не найден</param>
      /// <returns>Массив значений атрибута или null</returns>
      public static AttributeValues GetAttributeByGuid(
        AttributeValues[] values,
        Guid guid,
        long attributableID,
        bool throwException)
      {
        AttributeValues attributeValues1 = (AttributeValues) null;
        foreach (AttributeValues attributeValues2 in values)
        {
          if (attributeValues2.AttributeGuid.Equals(guid))
          {
            attributeValues1 = attributeValues2;
            break;
          }
        }
        return !(attributeValues1 == null & throwException) ? attributeValues1 : throw new AttributeNotFoundException(string.Empty, guid.ToString(), attributableID);
      }

      /// <summary>
      /// Ф-ция ищет в массиве значений атрибутов атрибут с идентификаторов attributeID и возвращает найденный элемент
      /// </summary>
      /// <param name="values">Массив значений атрибутов</param>
      /// <param name="attributeID">Ид. искомого атрибута</param>
      /// <param name="attributableID">Ид. объекта или связи, которому принадлежит данный массив значений атрибутов</param>
      /// <param name="throwException">Генерить ли исключение, если атрибут в массиве не найден</param>
      /// <returns>Массив значений атрибута или null</returns>
      public static AttributeValues GetAttributeByID(
        AttributeValues[] values,
        int attributeID,
        long attributableID,
        bool throwException)
      {
        AttributeValues attributeValues1 = (AttributeValues) null;
        foreach (AttributeValues attributeValues2 in values)
        {
          if (attributeValues2.AttributeID == attributeID)
          {
            attributeValues1 = attributeValues2;
            break;
          }
        }
        return !(attributeValues1 == null & throwException) ? attributeValues1 : throw new AttributeNotFoundException(attributeID, attributableID);
      }

      /// <summary>Сравниваем два объкта на null между собою</summary>
      /// <param name="x"></param>
      /// <param name="y"></param>
      /// <param name="result">Результат сравнения (0 - если оба не null)</param>
      /// <returns>True, если хоть один объект = null</returns>
      public static bool IsNullCompare(object x, object y, out int result)
      {
        result = 0;
        if (x != null && y != null)
          return false;
        result = x != y ? (x != null ? -1 : 1) : 0;
        return true;
      }

      /// <summary>Сравниваем два объкта на DbNull между собою</summary>
      /// <param name="x"></param>
      /// <param name="y"></param>
      /// <param name="result">Результат сравнения (0 - если оба не DbNull)</param>
      /// <returns>True, если хоть один объект = DbNull</returns>
      public static bool IsDbNullCompare(object x, object y, out int result)
      {
        result = 0;
        if (x != DBNull.Value && y != DBNull.Value)
          return false;
        result = x != y ? (x != DBNull.Value ? -1 : 1) : 0;
        return true;
      }

      /// <summary>Сравнение элементов по гуидам</summary>
      public class GuidComparer : IComparer<AttributeValues>
      {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public virtual int Compare(AttributeValues x, AttributeValues y)
        {
          int result;
          return AttributeValuesHelper.IsNullCompare((object) x, (object) y, out result) ? result : string.Compare(x.AttributeGuid.ToString(), y.AttributeGuid.ToString());
        }
      }

      /// <summary>Сравнение элементам по гуидам + значениям</summary>
      public class GuidValueComparer : AttributeValuesHelper.GuidComparer
      {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public override int Compare(AttributeValues x, AttributeValues y)
        {
          int result = base.Compare(x, y);
          if (result != 0 || x == null || AttributeValuesHelper.IsNullCompare((object) x.Values, (object) y.Values, out result))
            return result;
          result = x.Values.Length - y.Values.Length;
          if (result != 0)
            return result;
          for (int index = 0; index < x.Values.Length; ++index)
          {
            object x1 = x.Values[index];
            object y1 = y.Values[index];
            if (!AttributeValuesHelper.IsNullCompare(x1, y1, out result) && !AttributeValuesHelper.IsDbNullCompare(x1, y1, out result))
            {
              result = !(x1 is IComparable) ? x1.GetHashCode() - y1.GetHashCode() : ((IComparable) x1).CompareTo(y1);
              if (result != 0)
                break;
            }
            else
              break;
          }
          return result;
        }
      }
    }
}
