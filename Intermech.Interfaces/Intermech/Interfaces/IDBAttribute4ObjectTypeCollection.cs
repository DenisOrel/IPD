
// Type: Intermech.Interfaces.IDBAttribute4ObjectTypeCollection
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>Коллекция атрибутов, допустимых для типа объектов</summary>
    public interface IDBAttribute4ObjectTypeCollection : IDBAttribute4TypeCollection, IDBCollection
    {
      /// <summary>Добавить атрибут к списку атрибутов типа объектов.</summary>
      IDBAttributeType4Object Create(Attribute4ObjectTypeProperties attrProperties);

      /// <summary>
      /// Возвращает структуру со свойствами по умолчанию для атрибута attributeID применительно к данному типу объектов.
      /// </summary>
      Attribute4ObjectTypeProperties GetDefaultProperties(int attributeID);
    }
}
