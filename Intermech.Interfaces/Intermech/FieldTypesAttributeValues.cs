
// Type: Intermech.FieldTypesAttributeValues
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech
{
    /// <summary>Класс со значениями атрибутов для FieldTypes</summary>
    internal class FieldTypesAttributeValues
    {
      /// <summary>Значение атрибута TypeOfAttributeValue</summary>
      public Type Type { get; set; }

      /// <summary>Значение атрибута RDBMSTypeOfAttributeValueAttribute</summary>
      public Type RDbMsType { get; set; }

      /// <summary>Значение атрибута CustomDescription</summary>
      public string Description { get; set; }
    }
}
