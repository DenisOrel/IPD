
// Type: Intermech.Interfaces.IDBObjectLinkAttributeType
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>Интерфейс типа атрибута ftObjectLink</summary>
    public interface IDBObjectLinkAttributeType
    {
      /// <summary>
      /// Проверяет можно ли присваивать данный тип объекта атрибуту
      /// </summary>
      /// <param name="objectTypeID">Идентификатор типа объектов</param>
      void ValidateObjectType(int objectTypeID);

      /// <summary>
      /// Возвращает допустимые для данного атрибута типы объектов (назначенные непосредственно атрибуту без учета их дочерних типов)
      /// </summary>
      /// <returns>Массив с идентификаторами типов объектов. Если массив пустой - допустим любой тип объекта.</returns>
      int[] GetValidObjectTypes();
    }
}
