
// Type: Intermech.Interfaces.AttributeTypePossibleValues
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Структура для хранения допустимых значений типов атрибутов
    /// </summary>
    [Serializable]
    public class AttributeTypePossibleValues
    {
      /// <summary>Идентификатор (новый)</summary>
      public int AttributeID;
      /// <summary>Тип</summary>
      public FieldTypes FieldType;
      /// <summary>Значения</summary>
      public Hashtable Values;

      public AttributeTypePossibleValues(int attributeID, FieldTypes fieldType)
      {
        this.AttributeID = attributeID;
        this.FieldType = fieldType;
        this.Values = new Hashtable();
      }

      /// <summary>Добавить значение</summary>
      /// <param name="key">InlistID</param>
      /// <param name="val">Значение</param>
      /// <param name="descr">Описание</param>
      public void AddValue(object key, object val, object descr)
      {
        if (this.Values.Contains(key))
          this.Values[key] = val;
        else
          this.Values.Add(key, (object) new object[2]
          {
            val,
            descr
          });
      }
    }
}
