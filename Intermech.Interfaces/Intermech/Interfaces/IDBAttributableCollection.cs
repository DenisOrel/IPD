
// Type: Intermech.Interfaces.IDBAttributableCollection
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Collections.Generic;
using System.Data;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Интерфейс для работы с атрибутами списка объектов или связей
    /// </summary>
    public interface IDBAttributableCollection
    {
      /// <summary>
      /// Добавляет атрибут attributeID объектам objectIDs и присваивает им значение values (если values != null).
      /// Если ignoreExceptions == true, то игнорирует ошибки добавления атрибута и пытается
      /// добавить атрибут к следующему объекту в списке. Если атрибут у объекта существует, то ему
      /// просто присваивается значение values.
      /// </summary>
      CommandResult AddAttribute(
        long[] objectIDs,
        object attributeID,
        object[] values,
        bool ignoreExceptions);

      /// <summary>
      /// Изменяет значение атрибута attributeID у объектов objectIDs на значение values
      /// (или очищает значение если values == null). Если ignoreExceptions == true, то игнорирует
      /// ошибки изменения значений и пытается происвоить значение атрибуту следующего объекта в списке.
      /// Если атрибут у объекта не существует, то объект игнорируется.
      /// </summary>
      CommandResult EditAttribute(
        long[] objectIDs,
        object attributeID,
        object[] values,
        bool ignoreExceptions);

      /// <summary>
      /// Удаляет атрибут attributeID у объектов objectIDs (если таковой у объекта имеется).
      /// Если ignoreExceptions == true, то игнорирует ошибки удаления атрибута и переходит
      /// к удалению атрибута у следующего объекта в списке.
      /// </summary>
      CommandResult DeleteAttribute(long[] objectIDs, object attributeID, bool ignoreExceptions);

      /// <summary>
      /// Присваивает объекту (связи) с идентификаторами idList значения атрибутов valuesList.
      /// Если throwException == false, то давит исключения и продолжает присваивать значения атрибутам.
      /// Если throwException == true, то присвоение идет в одной транзакции.
      /// Если addIfNotExists == true, то добавляет несуществующие атрибуты, иначе выдает исключение.
      /// Метод не поддерживает присвоение значений системным, двоичным и файловым атрибутам!
      /// Соответствующие объекты уже должны допускать изменение себя (быть взяты на изменение и пр.)
      /// Возвращает количество объектов (связей), которым успешно присвоились значения атрибутов.
      /// </summary>
      int SetAttributesValues(
        long[] idList,
        AttributeValues[] valuesList,
        bool addIfNotExists,
        bool throwException);

      /// <summary>
      /// Ф-ция читает все значения атрибута attrID для версий объектов/связей с идентификаторами idList.
      /// Проверяет права чтения атрибута, но не проверяет видимость/наличие таких объектов/связей.
      /// </summary>
      /// <param name="idList">Список ObjectID для объектов или RelationID для связей</param>
      /// <param name="attrID">Ид. атрибута</param>
      /// <param name="allFields">Если true, то возвращает все составляющие значений атрибутов, а иначе только поле, значимое для этого типа данных</param>
      /// <returns>Таблица со значениями атрибута</returns>
      DataTable GetAttributeValues(ICollection<long> idList, int attrID, bool allFields);

      /// <summary>
      /// Метод проверяет правильность новых значений атрибутов при создании объектов/связей по прототипу от других объектов/связей
      /// </summary>
      /// <param name="ckeckedValues">Словарь ид. версии объекта/связи=набор новых значений атрибутов</param>
      /// <returns>Массив ошибок при проверке (если ошибок нет массив пусто)</returns>
      CheckAttributeValueResult[] CheckAttributesValues(
        Dictionary<long, AttributeValues[]> ckeckedValues);
    }
}
