
// Type: Intermech.Interfaces.BasicAttributeProperties
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Основные свойства атрибута для не работы некоторых ф-ций типа GetEnabledAttributes
    /// </summary>
    [Serializable]
    public class BasicAttributeProperties
    {
      /// <summary>Ид. атрибута</summary>
      public int AttributeID { get; private set; }

      /// <summary>Наименование</summary>
      public string Name { get; private set; }

      /// <summary>Реальный тип данных (с учетом системных атрибутов)</summary>
      public FieldTypes RealFieldType { get; private set; }

      public BasicAttributeProperties(int attrID, string name, FieldTypes fieldType)
      {
        this.AttributeID = attrID;
        this.Name = name;
        this.RealFieldType = fieldType;
      }
    }
}
