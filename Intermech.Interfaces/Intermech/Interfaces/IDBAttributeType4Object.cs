
// Type: Intermech.Interfaces.IDBAttributeType4Object
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>
    /// Интерфейс, описывающий свойства атрибута в контексте определенного типа объектов
    /// </summary>
    public interface IDBAttributeType4Object : IDBAttributeType4, IDBAttributeType
    {
      /// <summary>
      /// Правила передачи по наследству атрибутов от родительских типов дочерним типам
      /// </summary>
      InheritModes InheritMode { get; set; }

      Attribute4ObjectTypeProperties Attribute4ObjectPropertiesStructure { get; set; }
    }
}
