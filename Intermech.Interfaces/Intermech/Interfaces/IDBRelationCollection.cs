
// Type: Intermech.Interfaces.IDBRelationCollection
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Interfaces
{
    /// <summary>Интерфейс для получения и работы со списками связей</summary>
    public interface IDBRelationCollection : IDBRecords, IDBSessionable, IDBAttributableCollection
    {
      /// <summary>
      /// Получить таблицу со списком связей, соответствующую paramSet. Тип связей указывается
      /// при получении объекта, реализующего IDBRelationCollection (если при получении указано
      /// значение типа меньшее 0, то в запрос включаются связи всех типов).
      /// projectID - ид. версии родительского объекта, чей состав нужно получить. Если =-1, то по этому
      /// параметру связи не фильтруются.
      /// partID - ид. дочернего объекта (IDBObject.ID), чью применяемость нужно получить. Если = -1, то по этому
      /// параметру связи не фильтруются.
      /// actualDate - дата, на которую нужно получить связи (должна быть локальной).
      /// </summary>
      /// <param name="paramSet">Параметры запроса</param>
      /// <param name="projectID">ID версии родительского объекта (F_OBJECT_ID), чей состав нужно получить. Если =-1, то по этому
      /// параметру связи не фильтруются</param>
      /// <param name="partID">ID дочернего объекта (F_ID), чью применяемость нужно получить. Если = -1, то по этому
      /// параметру связи не фильтруются</param>
      /// <param name="actualDate">дата, на которую нужно получить связи (должна быть локальной)</param>
      /// <returns>Результаты запроса</returns>
      DataTable Select(DBRecordSetParams paramSet, long projectID, long partID, DateTime actualDate);

      /// <summary>
      /// Получить таблицу со списком связей, соответствующую paramSet. Тип связей указывается
      /// при получении объекта, реализующего IDBRelationCollection (если при получении указано
      /// значение типа меньшее 0, то в запрос включаются связи всех типов).
      /// projectID - ид. версии родительского объекта, чей состав нужно получить. Если =-1, то по этому
      /// параметру связи не фильтруются.
      /// partID - ид. дочернего объекта (IDBObject.ID), чью применяемость нужно получить. Если = -1, то по этому
      /// параметру связи не фильтруются.
      /// actualDate - дата, на которую нужно получить связи (должна быть локальной).
      /// </summary>
      /// <param name="paramSet">Параметры запроса</param>
      /// <param name="projectID">ID версии родительского объекта (F_OBJECT_ID), чей состав нужно получить. Если =-1, то по этому
      /// параметру связи не фильтруются</param>
      /// <param name="partID">ID дочернего объекта (F_ID), чью применяемость нужно получить. Если = -1, то по этому
      /// параметру связи не фильтруются</param>
      /// <param name="actualDate">дата, на которую нужно получить связи (должна быть локальной)</param>
      /// <param name="childObjectTypes">Список дочерних типов объектов. Для каждого типа будет выполнен отдельный запрос,
      /// затем все результаты запросов будут объединены в одну общую таблицу. Данный параметр имеет более высокий
      /// приоритет перед свойствами ObjectTypeID и ChildObjectTypes</param>
      /// <returns>Результаты запроса</returns>
      DataTable Select(
        DBRecordSetParams paramSet,
        long projectID,
        long partID,
        DateTime actualDate,
        IList<int> childObjectTypes);

      /// <summary>
      /// Получить применяемость объекта номер partID (IDBObject.ID) на текущую дату в соответствии с
      /// условиями paramSet
      /// </summary>
      DataTable EntersIn(DBRecordSetParams paramSet, long partID);

      DataTable EntersIn(DBRecordSetParams paramSet, long partID, bool recursive);

      /// <summary>
      /// Получить применяемость объекта номер partID (IDBObject.ID) в соответствии с условиями paramSet.
      /// Если recursive == true, то ф-ция получает развернутый список применяемости во всем дереве связей.
      /// actualDate - дата, на которую нужно получить применяемость (должна быть локальной).
      /// </summary>
      DataTable EntersIn(DBRecordSetParams paramSet, long partID, bool recursive, DateTime actualDate);

      /// <summary>
      /// Получить применяемость версии объекта номер (IDBObject.ObjectID) на текущую дату в соответствии с
      /// условиями paramSet
      /// </summary>
      DataTable EntersInVersion(DBRecordSetParams paramSet, long objectID);

      /// <summary>Применяемость в версии объекта</summary>
      /// <param name="paramSet">Параметры запроса</param>
      /// <param name="objectID">Ид. версии объекта</param>
      /// <param name="id">Ид. объекта (для ускорения работы)</param>
      /// <returns></returns>
      DataTable EntersInVersion(DBRecordSetParams paramSet, long objectID, long id);

      /// <summary>
      /// Получить применяемость версии объекта номер partID (IDBObject.ObjectID) в соответствии с условиями paramSet.
      /// Если recursive == true, то ф-ция получает развернутый список применяемости во всем дереве связей.
      /// actualDate - дата, на которую нужно получить применяемость (должна быть локальной).
      /// </summary>
      DataTable EntersInVersion(
        DBRecordSetParams paramSet,
        long objectID,
        bool recursive,
        DateTime actualDate);

      /// <summary>
      /// Получить состав версии объекта номер projectID на текущую дату в соответствии с условиями paramSet
      /// </summary>
      DataTable ConsistFrom(DBRecordSetParams paramSet, long projectID);

      DataTable ConsistFrom(DBRecordSetParams paramSet, long projectID, bool recursive);

      /// <summary>
      /// Получить состав версии объекта номер projectID в соответствии с условиями paramSet.
      /// Если recursive == true, то ф-ция получает развернутый состав со всем деревом связей.
      /// actualDate - дата, на которую нужно получить состав (должна быть локальной).
      /// </summary>
      DataTable ConsistFrom(
        DBRecordSetParams paramSet,
        long projectID,
        bool recursive,
        DateTime actualDate);

      /// <summary>
      /// Возвращает состав версии объекта projectID с возможностью определить есть ли в составе скрытые правилами безопасности объекты.
      /// </summary>
      /// <param name="paramSet">Настройки получения состава</param>
      /// <param name="projectID">ObjectID объекта, состав которого нужно получить.</param>
      /// <param name="invisibleExists">Возвращает true, если в составе кроме возвращенных объектов есть еще и скрытые.</param>
      /// <returns>Таблица с видимым составом объекта.</returns>
      DataTable ConsistFrom(DBRecordSetParams paramSet, long projectID, out bool invisibleExists);

      /// <summary>
      /// Создает связь между объектами projectID (ид. версии родительского объекта) и partObjectID (ид. версии
      /// дочернего объекта), которая начнет действовать с даты beginDate (в локальном времени). Тип связи
      /// задается при получении объекта IDBRelationCollection (если при создании тип связи меньше 0,
      /// то функция Create работать не будет).
      /// </summary>
      IDBRelation Create(long projectID, long partObjectID, DateTime beginDate);

      /// <summary>
      /// То же, но связь начнет действовать с момента ее создания.
      /// </summary>
      IDBRelation Create(long projectID, long partObjectID, AttributeValues[] vals = null);

      /// <summary>
      /// Создает связь между объектами properties.ProjectObjectID (ид. версии родительского объекта) и properties.PartID (ид. версии
      /// дочернего объекта), которая начнет действовать с даты properties.BeginDate (в локальном времени).
      /// Если properties.BeginDate == DateTime.MinValue, то связь начинает действовать с даты ее создания.
      /// Если properties.EndDate == DateTime.MaxValue, то время действия связи не ограничено. Тип связи
      /// задается при получении объекта IDBRelationCollection (если при создании тип связи меньше 0,
      /// то функция Create работать не будет). Если properties.PrototypeRelationID &gt; 0, то связь инициализируется атрибутами
      /// от связи прототипа с номером properties.PrototypeRelationID.
      /// </summary>
      IDBRelation Create(NewRelationProperties properties);

      /// <summary>Тип связей</summary>
      int RelationTypeID { get; set; }

      /// <summary>
      /// Ид. типа объектов, которые нужно получить по списку связей (по умолчанию = -1, т.е. все типы)
      /// </summary>
      int ObjectTypeID { get; set; }

      /// <summary>
      /// Режим, позволяющий строить нетипизированные запросы по всем типам объектов, включая в них локальный типы.
      /// Не допускает использование в запросах необязательных атрибутов объектов, а также атрибутов F_GUID и CAPTION
      /// </summary>
      bool LocalTypesMode { get; set; }

      /// <summary>
      /// Список дочерних типов объектов. Для каждого типа будет выполнен отдельный запрос,
      /// затем все результаты запросов будут объединены в одну общую таблицу.
      /// 1) Свойство имеет более высокий приоритет перед свойством ObjectTypeID;
      /// 2) Если вызывается метод Select, в параметрах которого указан иной непустой список childObjectTypes,
      /// то будет применён именно этот список
      /// </summary>
      IList<int> ChildObjectTypes { get; set; }

      /// <summary>
      /// Возвращает true, если объект с ид. id (IDBObject.ID) входит в любой из объектов parentsObjectID (IDBObject.ObjectID)
      /// Тип связи берётся из текущей коллекции связей.
      /// </summary>
      bool IsObjectInFolders(long id, long[] parentsObjectID);

      /// <summary>
      /// Получает массив идентификаторов объектов-заготовок, которые в данный момент входят в состав
      /// версии объекта projectID текущим типом связи.
      /// </summary>
      long[] ConsistFromBlanks(long projectID);

      /// <summary>Удаляет набор указанных связей</summary>
      /// <param name="projID">Идентификатор версии объекта, у которого будут удалены нижележащие связи.</param>
      /// <param name="relationsGUID">Глобальные идентификаторы удаляемых связей.</param>
      /// <param name="transactionMode">Если содержит true, то удаление производится в одной транзакции,
      /// т.е. в случае ошибки будут отменены все удаления</param>
      /// <param name="deleteMode">Параметр передается в функцию удаления связей (рекомендуемое значение 0).</param>
      /// <returns>Возвращает количество удаленных связей</returns>
      int DeleteRelations(
        long projID,
        IList<Guid> relationsGUID,
        bool transactionMode,
        long deleteMode);

      /// <summary>
      /// Уникальный ключ, по которому сервер (сервис IVersionRulesCacheService) определяет настройки
      /// фильтрации состава. Вместо указания этого ключа можно явно задать правило подбора
      /// версий (поле FiltrationRule, приоритет которого выше, чем у поля FiltrationOwnerID).
      /// </summary>
      string FiltrationOwnerID { get; set; }

      /// <summary>
      /// Вместо указания ключа с настройками фильтрации (FiltrationOwnerID) можно явно задать
      /// правило подбора версий, по которому будет осуществляться фильтрация состава
      /// (приоритет поля FiltrationRule выше, чем у поля FiltrationOwnerID)
      /// </summary>
      VersionsRule FiltrationRule { get; set; }

      /// <summary>
      /// Отыскать версию объекта, которая существована на указанную дату
      /// на указанном уровне продвижения или на указанном шаге ЖЦ
      /// (значения даты и уровня/шага ЖЦ находятся в правиле подбора версий).
      /// Если версия не найдена, вернётся значение -1
      /// </summary>
      /// <param name="objectID">Идентификатор версии объекта</param>
      /// <param name="rule">Правило подбора версий, в которое включены:
      /// - дата актуального состава,
      /// - уровень продвижения или шаг жизненного цикла</param>
      /// <param name="state">Статус найденной версии</param>
      /// <returns>Найденный идентификатор версии объекта или -1</returns>
      long ActualDateObjectVersion(long objectID, VersionsRule rule, out ObjectFiltrationState state);

      /// <summary>
      /// Получить статус версии объекта согласно указанному правилу подбора версий
      /// </summary>
      /// <param name="objectID">Идентификатор версии объекта</param>
      /// <param name="rule">Правило подбора версий</param>
      /// <returns>Статус версии объекта согласно указанному правилу подбора версий</returns>
      ObjectFiltrationState GetObjectVersionFiltrationState(long objectID, VersionsRule rule);

      /// <summary>
      /// Получает развернутый состав объектов rootIDs по всем типам связей, возвращая в result идентификаторы объектов типа targetObjectTypeIDs (или всех типов, если массив targetObjectTypeIDs пустой)
      /// Юзать осторожно! Не проверяет НИЧЕГО! Работает без учета дат и истории связей! Работает ТОЛЬКО с неверсионными типами объектов! Но зато ОЧЕНЬ БЫСТРО!
      /// </summary>
      /// <param name="rootIDs">ObjectID объектов, состав которых нужно получить</param>
      /// <param name="targetObjectTypeIDs">Список типов объектов, которые нужно включать в результат</param>
      /// <returns>Список ObjectID результата</returns>
      List<long> QuickConsistFrom(long[] rootIDs, List<int> targetObjectTypeIDs);

      /// <summary>
      /// Имя поля для объединения связей и объектов (F_PROJ_ID или F_PART_ID)
      /// </summary>
      string JoinFieldName { get; set; }
    }
}
