
// Type: Intermech.Interfaces.IDBImporter
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces.Briefcase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Interfaces
{
    /// <summary>Для импортирования в базу всякой дряни</summary>
    public interface IDBImporter
    {
      /// <summary>
      /// Коллекция структур AttributeTypePossibleValues, формируется в процессе импорта метаданных на сервер
      /// и содержит допустимые значения типов атрибутов типа ftObjectLink
      /// </summary>
      List<AttributeTypePossibleValues> PossibleValuesAttributeType { get; }

      /// <summary>
      /// Коллекция ID атрибута (новый - уже импотрированный) -&gt; ID объекта в поле "Значение по умолчанию" (в портфеле)
      /// </summary>
      List<SaveImportValues> DefaultValueObjectLink { get; }

      /// <summary>
      /// Коллекция ID атрибута (новый - уже импотрированный) -&gt; ID объекта в поле SizeType для ftMeasured (в портфеле)
      /// </summary>
      List<SaveImportValues> MeasureValueObjectLink { get; }

      /// <summary>
      /// Коллекция структур ObjectsLinksFromImport, где хранятся ссылки на объекты встречающиеся во время
      /// импорта объектов в виде значений атрибутов типа ftObjectLink
      /// </summary>
      ArrayList ObjectLinks { get; }

      /// <summary>Импорт метаданных</summary>
      /// <param name="briefcaseID"></param>
      /// <param name="Metadata">Метаданные </param>
      /// <param name="ImportingList">Список импортируемых метаданных (пример - MetadataExportList.xml)</param>
      /// <param name="ignoringErrors"></param>
      /// <returns>Результат, все ошибки лежат в логе на сервере</returns>
      bool ImportMetadata(
        Guid briefcaseID,
        DataSet Metadata,
        DataSet ImportingList,
        IgnoringErrors ignoringErrors);

      /// <summary>
      /// Установить допустимые значения. Выполняется после импорта метаданных и всех объектов.
      /// Восстанавливаюся доп.значения (ссылки на объекты) у типов атрибутов.
      /// </summary>
      /// <param name="possibleValuesAttributeType">Коллекция AttributeTypePossibleValues</param>
      /// <param name="importingObjectIDs">Коллекция всех импортированных объектов, где ID объекта до импорта - &gt; ID объекта после импорта.
      /// Тут должны лежать все соответствия ID объекта в AttributeTypePossibleValues к новым ID базе
      /// </param>
      /// <returns>Результат, все ошибки лежат в логе на сервере</returns>
      bool SetPossibleValues(
        List<AttributeTypePossibleValues> possibleValuesAttributeType,
        Hashtable importingObjectIDs);

      /// <summary>
      /// Установить допустимые значения. Выполняется после импорта метаданных и всех объектов.
      /// Восстанавливаюся доп.значения (ссылки на объекты) у типов атрибутов.
      /// </summary>
      /// <param name="possibleValuesAttributeType">Коллекция AttributeTypePossibleValues</param>
      /// <param name="importingObjectIDs">Коллекция всех импортированных объектов, где ID объекта до импорта - &gt; ID объекта после импорта.
      /// Тут должны лежать все соответствия ID объекта в AttributeTypePossibleValues к новым ID базе
      /// </param>
      /// <returns>Результат, все ошибки лежат в логе на сервере</returns>
      bool SetPossibleValues(
        List<AttributeTypePossibleValues> possibleValuesAttributeType,
        List<Tuple<long, long>> importingObjectIDs);

      /// <summary>Добавить объект</summary>
      /// <param name="importingObject">Структура с инфой по объекту</param>
      /// <returns></returns>
      IImportedObjectInfo ImportObject(ImportingObject importingObject);

      /// <summary>Добавить объект</summary>
      /// <param name="importingObject">Структура с инфой по объекту</param>
      /// <param name="createLinksArray">Не создавать коллекцию ссылок на объекты в значениях атрибутов типа ftObject,
      /// а сразу записывать в значения атрибутов передаваемые ссылки на объекты</param>
      /// <returns></returns>
      IImportedObjectInfo ImportObject(ImportingObject importingObject, bool createLinksArray);

      /// <summary>Добавить объекты</summary>
      /// <param name="importingObjects">Коллекция структур с инфой по объектам</param>
      /// <returns></returns>
      IImportedObjectInfo[] ImportObjects(ImportingObject[] importingObjects);

      /// <summary>Добавить объекты</summary>
      /// <param name="importingObjects">Коллекция структур с инфой по объектам</param>
      /// <param name="createLinksArray">Не создавать коллекцию ссылок на объекты в значениях атрибутов типа ftObject,
      /// а сразу записывать в значения атрибутов передаваемые ссылки на объекты</param>
      /// <returns></returns>
      IImportedObjectInfo[] ImportObjects(ImportingObject[] importingObjects, bool createLinksArray);

      /// <summary>Импорт прав доступа</summary>
      /// <param name="importingRecords">Массив структур, описывающих права доступа для импорта</param>
      /// <returns></returns>
      long[] ImportSequrity(SecurityRecord[] importingRecords);

      /// <summary>Добавить связь</summary>
      /// <param name="importingRelation">Структура с инфой по объекту</param>
      /// <returns>ID связи, либо -1 при неудаче, все ошибки лежат в логе на сервере</returns>
      long ImportRelation(ImportingRelation importingRelation);

      /// <summary>Добавить связь</summary>
      /// <param name="importingRelation">Структура с инфой по объекту</param>
      /// <param name="createLinksArray">Не создавать коллекцию ссылок на объекты в значениях атрибутов типа ftObject,
      /// а сразу записывать в значения атрибутов передаваемые ссылки на объекты</param>
      /// <returns>ID связи, либо -1 при неудаче, все ошибки лежат в логе на сервере</returns>
      long ImportRelation(ImportingRelation importingRelation, bool createLinksArray);

      /// <summary>Добавить связи</summary>
      /// <param name="importingRelations">Коллекция структур с инфой по связям</param>
      /// <returns>Коллекция IDs связей, если связь не импортирована -1, все ошибки лежат в логе на сервере</returns>
      long[] ImportRelations(ImportingRelation[] importingRelations);

      /// <summary>Добавить связи</summary>
      /// <param name="importingRelations">Коллекция структур с инфой по связям</param>
      /// <param name="createLinksArray">Не создавать коллекцию ссылок на объекты в значениях атрибутов типа ftObject,
      /// а сразу записывать в значения атрибутов передаваемые ссылки на объекты</param>
      /// <returns>Коллекция IDs связей, если связь не импортирована -1, все ошибки лежат в логе на сервере</returns>
      long[] ImportRelations(ImportingRelation[] importingRelations, bool createLinksArray);

      /// <summary>
      /// Обновить ссылки на объекты у атрибутов импортированных связей / объектов
      /// </summary>
      /// <param name="objLinks">Коллекция структур ObjectsLinksFromImport, где хранятся ссылки на объекты встречающиеся во время
      /// импорта объектов в виде значений атрибутов типа ftObjectLink</param>
      /// <param name="importingObjectIDs">Коллекция всех импортированных объектов</param>
      /// <returns>Результат, все ошибки лежат в логе на сервере</returns>
      bool SetObjectLinks(ArrayList objLinks, List<IDСorresponds> importingObjectIDs);

      /// <summary>
      /// Установить значения по-умолчанию для атрибутов. Выполняется после SetPossibleValues.
      /// </summary>
      /// <param name="defaultValueObjectLink">Коллекция ID атрибута (новый - уже импотрированный) -&gt; ID объекта в поле "Значение по умолчанию" (в портфеле)</param>
      /// <param name="importingObjectIDs">Коллекция всех импортированных объектов, где ID объекта до импорта - &gt; ID объекта после импорта.
      /// Тут должны лежать все соответствия ID объекта в AttributeTypePossibleValues к новым ID базе
      /// </param>
      /// <returns>Результат, все ошибки лежат в логе на сервере</returns>
      bool SetDefaultValues(
        List<SaveImportValues> defaultValueObjectLink,
        List<Tuple<long, long>> importingObjectIDs);

      /// <summary>
      /// Установить значения по-умолчанию для атрибутов. Выполняется после SetPossibleValues.
      /// </summary>
      /// <param name="defaultValueObjectLink">Коллекция ID атрибута (новый - уже импотрированный) -&gt; ID объекта в поле "Значение по умолчанию" (в портфеле)</param>
      /// <param name="importingObjectIDs">Коллекция всех импортированных объектов, где ID объекта до импорта - &gt; ID объекта после импорта.
      /// Тут должны лежать все соответствия ID объекта в AttributeTypePossibleValues к новым ID базе
      /// </param>
      /// <returns>Результат, все ошибки лежат в логе на сервере</returns>
      bool SetDefaultValues(List<SaveImportValues> defaultValueObjectLink, Hashtable importingObjectIDs);

      /// <summary>
      /// Установить значения SizeType для атрибутов. Выполняется после SetPossibleValues.
      /// </summary>
      /// <param name="measureValueObjectLink">Коллекция ID атрибута (новый - уже импотрированный) -&gt; ID объекта в поле SizeType для ftMeasured (в портфеле)</param>
      /// <param name="importingObjectIDs">Коллекция всех импортированных объектов, где ID объекта до импорта - &gt; ID объекта после импорта.
      /// Тут должны лежать все соответствия ID объекта в AttributeTypePossibleValues к новым ID базе
      /// </param>
      /// <returns></returns>
      bool SetMeasureValues(List<SaveImportValues> measureValueObjectLink, Hashtable importingObjectIDs);

      /// <summary>
      /// Установить значения SizeType для атрибутов. Выполняется после SetPossibleValues.
      /// </summary>
      /// <param name="measureValueObjectLink">Коллекция ID атрибута (новый - уже импотрированный) -&gt; ID объекта в поле SizeType для ftMeasured (в портфеле)</param>
      /// <param name="importingObjectIDs">Коллекция всех импортированных объектов, где ID объекта до импорта - &gt; ID объекта после импорта.
      /// Тут должны лежать все соответствия ID объекта в AttributeTypePossibleValues к новым ID базе
      /// </param>
      /// <returns></returns>
      bool SetMeasureValues(
        List<SaveImportValues> measureValueObjectLink,
        List<Tuple<long, long>> importingObjectIDs);

      /// <summary>Инфа с выполнением</summary>
      BriefcaseImportProgress GetProgress(Guid briefcaseID);

      /// <summary>
      /// Закончить импорт метаданных, посылаеццо с клиенда по окончанию закачки метаданных
      /// </summary>
      /// <param name="briefcaseID"></param>
      void EndImportMetadata(Guid briefcaseID);

      /// <summary>Установить иерархию версий</summary>
      /// <param name="treeTable">IMS_VERSIONS_TREE</param>
      /// <returns>Результат</returns>
      bool SetVersionsTree(DataTable treeTable);

      /// <summary>Включить объект в ручную выборку</summary>
      /// <param name="selectionID">Идентификатор выборки</param>
      /// <param name="key">Ключ папки классификатора или пусто</param>
      /// <param name="objectID">Идентификатор версии объекта</param>
      /// <param name="id">Идентификатор объекта</param>
      /// <returns></returns>
      bool IncludeObjectIntoSelection(long selectionID, string key, long objectID, long id);

      /// <summary>Возвращает следующее значение генератора таблицы</summary>
      long GetNextID(string tableName);

      /// <summary>
      /// Установить тригера у таблицы IMS_OBJECT_ATTRS в состояние enable
      /// ****************************************************************
      /// Используется только в программе миграции данных.
      /// При INSERTах в IMS_OBJECT_ATTRS если атрибут влияет на дату модификации
      /// объекта срабатывает тригер в котором делается UPDATE для таблицы IMS_OBJECTS.
      /// Чтобы этого избежать отключаем тригера до миграции и не забываем включить после миграции
      /// </summary>
      /// <param name="enable"></param>
      void SetTriggersIMS_OBJECT_ATTRS(bool enable);

      /// <summary>Удалить индексы в таблицах</summary>
      void DropIndexes(Guid pumpGuid);

      /// <summary>Создать индексы в таблицах</summary>
      void CreateIndexes(Guid pumpGuid);

      /// <summary>Корректное завершение работы импортера.</summary>
      void CloseImporter();

      /// <summary>Присваивает признак вида записи объекту</summary>
      /// <param name="dbObject"></param>
      /// <param name="verTypeID"></param>
      void SetObjectVerType(IDBObject dbObject, ObjectRecordKind verTypeID);

      /// <summary>
      /// Получить список тип объектов и первые шаги ЖЦ для объектов этих типов
      /// </summary>
      /// <returns></returns>
      Dictionary<int, LCSchemaInfo> GetSchemaInfo4ObjTypes();

      /// <summary>
      /// Пакетный метод добавляет объектам двоичный атрибут, тело которого заранее записано в активный файловый шкаф.
      /// </summary>
      /// <param name="attributeID">Идентификатор атрибута</param>
      /// <param name="blobs">Словарь соответствия ObjectID=инфа о значениях файлового атрибута</param>
      void AddBlobAttribute(int attributeID, Dictionary<long, BlobAttributeValue[]> blobs);

      /// <summary>
      /// Пакетный метод добавляет объекту двоичный атрибут, тело которого заранее записано в активный файловый шкаф.
      /// Для рабочих копий регистрирует наличие их блобов для дальнейшей обработки командой N21 административного меню.
      /// </summary>
      /// <param name="attributeID">Идентификатор атрибута</param>
      /// <param name="objectID">Идентификатор версии объекта</param>
      /// <param name="userID">Идентификатор пользователя, взявшего объект на изменение</param>
      /// <param name="blobs">Словарь соответствия ObjectId=инфа о значениях файлового атрибута</param>
      void AddBlobAttribute(int attributeID, long objectID, long userID, BlobAttributeValue[] blobs);

      /// <summary>
      /// Последний идентификатор генератора идентификаторов объектов
      /// </summary>
      long LastObjectID { get; }

      /// <summary>IMS_IMBASE_ATTRS</summary>
      void SetImbaseTableAttributes(long tableID, List<int> attributeIDs);

      DataTable GetAttributeValues(int objectTypeID, long objectID);
    }
}
