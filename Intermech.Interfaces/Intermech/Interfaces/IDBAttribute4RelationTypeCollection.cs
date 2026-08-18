
// Type: Intermech.Interfaces.IDBAttribute4RelationTypeCollection
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>Коллекция атрибутов, допустимых для типа связей</summary>
    public interface IDBAttribute4RelationTypeCollection : IDBAttribute4TypeCollection, IDBCollection
    {
      /// <summary>Добавить атрибут к списку атрибутов типа связей.</summary>
      IDBAttributeType4Relation Create(Attribute4RelationTypeProperties attrProperties);
    }
}
