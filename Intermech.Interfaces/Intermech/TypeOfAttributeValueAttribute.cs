
// Type: Intermech.TypeOfAttributeValueAttribute
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech
{
    /// <summary>
    /// Внутренний атрибут для FieldTypes: тип данных, в AttributeValues (IDBAttributable.GetAttributeValues)
    /// </summary>
    internal class TypeOfAttributeValueAttribute : Attribute
    {
      public Type TypeOfAttributeValue;

      public TypeOfAttributeValueAttribute(Type type) => this.TypeOfAttributeValue = type;
    }
}
