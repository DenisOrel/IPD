
// Type: Intermech.Interfaces.IDBAttributeTypeCollection
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>Коллекция атрибутов, зарегистрированных в системе</summary>
    public interface IDBAttributeTypeCollection : IDBCollection
    {
      /// <summary>Создает атрибут и возвращает его идентификатор</summary>
      int Create(AttributeTypeProperties attrProperties);

      /// <summary>
      /// Возвращает структуру валидации допустимых значений для типа fldtype
      /// </summary>
      AttributeTypePropertiesValidator GetValidator(FieldTypes fldtype);

      /// <summary>
      /// Получить валидатор для заполнения параметров атрибута номер attributeID,
      /// добавляемого к типу объектов.
      /// </summary>
      AttributeTypePropertiesValidator GetValidatorForObjectType(int attributeID);

      /// <summary>
      /// Получить валидатор для заполнения параметров атрибута номер attributeID,
      /// добавляемого к типу объектов.
      /// </summary>
      AttributeTypePropertiesValidator GetValidatorForRelationType(int attributeID);

      /// <summary>
      /// Возвращает массив атрибутов-типов по их идентификационным параметрам, хранящимся в
      /// idList. Идентификаторы могут быть именами (string), Guid-фми (Guid) и AttributeID (int).
      /// Если failIfNotFound=false, то не найденный атрибут будет просто пропущен.
      /// </summary>
      IDBAttributeType[] GetAttributeTypeList(object[] idList, bool failIfNotFound);

      IDBAttributeType GetAttributeType(object objID, bool failIfNotFound);
    }
}
