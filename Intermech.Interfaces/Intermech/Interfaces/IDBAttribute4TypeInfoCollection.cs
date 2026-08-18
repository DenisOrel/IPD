
// Type: Intermech.Interfaces.IDBAttribute4TypeInfoCollection
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    public interface IDBAttribute4TypeInfoCollection : IDBMetadataInfoCollection
    {
      /// <summary>
      /// Найти атрибут attributeID и вернуть соответствующий объект или null при его
      /// отсутствии. Если throwNotFoundException=true, то сгенерировать исключение
      /// при отсутствии такого атрибута.
      /// </summary>
      IDBAttributeTypeInfo4 GetAttributeByID(int attributeID, bool throwNotFoundException);

      IDBAttributeTypeInfo4 GetAttributeByID(int attributeID);

      IDBAttributeTypeInfo4 GetAttributeByName(string attributeName, bool throwNotFoundException);

      IDBAttributeTypeInfo4 GetAttributeByName(string attributeName);

      IDBAttributeTypeInfo4 GetAttributeByGUID(Guid attributeGuid, bool throwNotFoundException);

      IDBAttributeTypeInfo4 GetAttributeByGUID(Guid attributeGuid);

      /// <summary>
      /// Возвращает true, если данный атрибут может быть у сущностей данного типа (с учетом флага возможности добавления любого атрибута и системных атрибутов)
      /// </summary>
      /// <param name="attributeID">Ид. атрибута</param>
      /// <returns>Атрибут может быть или нет</returns>
      bool IsEnabledAttribute(int attributeID);
    }
}
