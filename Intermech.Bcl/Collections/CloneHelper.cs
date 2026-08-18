
// Type: Intermech.Collections.CloneHelper
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections;


namespace Intermech.Collections
{
    /// <summary>
    /// Вспомогательный статический класс, позволяющий осуществлять клонирование объектов, списков, словариков
    /// без ограничения уровня сложности и вложенности элементов ключей и значений
    /// </summary>
    public static class CloneHelper
    {
      /// <summary>Создать точную копию объекта, если это возможно</summary>
      /// <param name="source">Объект-источник</param>
      /// <returns>Точная копия объекта или сам объект-источник</returns>
      public static object Clone(object source)
      {
        if (source == null || source == DBNull.Value)
          return source;
        if (source is IList list1)
        {
          IList list = (IList) null;
          try
          {
            list = Activator.CreateInstance(source.GetType()) as IList;
          }
          catch
          {
          }
          if (list != null)
          {
            for (int index = 0; index < list1.Count; ++index)
              list.Add(CloneHelper.Clone(list1[index]));
            return (object) list;
          }
        }
        if (source is IDictionary dictionary1)
        {
          IDictionary dictionary = (IDictionary) null;
          try
          {
            dictionary = Activator.CreateInstance(source.GetType()) as IDictionary;
          }
          catch
          {
          }
          if (dictionary != null)
          {
            IDictionaryEnumerator enumerator = dictionary1.GetEnumerator();
            enumerator.Reset();
            while (enumerator.MoveNext())
              dictionary.Add(CloneHelper.Clone(enumerator.Key), CloneHelper.Clone(enumerator.Value));
            return (object) dictionary;
          }
        }
        return source is ICloneable cloneable ? cloneable.Clone() : source;
      }
    }
}
