
// Type: Intermech.Interfaces.IDBAttribute4TypeCollection
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    public interface IDBAttribute4TypeCollection : IDBCollection
    {
      /// <summary>
      /// Возвращает массив атрибутов-типов по их идентификационным параметрам, хранящимся в
      /// idList. Идентификаторы могут быть именами (string), Guid-фми (Guid) и AttributeID (int).
      /// Если failIfNotFound=false, то не найденный атрибут будет просто пропущен.
      /// </summary>
      IDBAttributeType[] GetAttributeTypeList(object[] idList, bool failIfNotFound);

      /// <summary>
      /// Найти атрибут attributeID и вернуть соответствующий объект или null при его
      /// отсутствии. Если throwNotFoundException=true, то сгенерировать исключение
      /// при отсутствии такого атрибута.
      /// </summary>
      IDBAttributeType4 GetAttributeByID(int attributeID, bool throwNotFoundException);

      IDBAttributeType4 GetAttributeByID(int attributeID);

      IDBAttributeType4 GetAttributeByName(string attributeName, bool throwNotFoundException);

      IDBAttributeType4 GetAttributeByName(string attributeName);

      IDBAttributeType4 GetAttributeByGUID(Guid attributeName, bool throwNotFoundException);

      IDBAttributeType4 GetAttributeByGUID(Guid attributeName);

      /// <summary>
      /// Вернуть набор атрибутов, допустимых для данного типа объектов/связей
      /// </summary>
      /// <param name="includeObligatory">Включать ли обязательные атрибуты объектов/связей</param>
      /// <returns>Массив с основынми свойствами атрибутов</returns>
      BasicAttributeProperties[] GetEnabledAttributes(bool includeObligatory);

      /// <summary>
      /// Возвращает true, если данный атрибут может быть у сущностей данного типа (с учетом флага возможности добавления любого атрибута и системных атрибутов)
      /// </summary>
      /// <param name="attributeID">Ид. атрибута</param>
      /// <returns>Атрибут может быть или нет</returns>
      bool IsEnabledAttribute(int attributeID);
    }
}
