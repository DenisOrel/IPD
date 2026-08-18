
// Type: Intermech.Interfaces.IUserSession
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces.Briefcase;
using Intermech.Interfaces.Contexts;
using Intermech.Interfaces.LifeCycles;
using Intermech.Interfaces.NotifySamples;
using Intermech.Interfaces.Projects;
using Intermech.Interfaces.Snapshots;
using Intermech.Kernel.Search;
using Intermech.Protection;
using Intermech.Remoting;
using Intermech.Remoting.Compression;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Interfaces
{
    /// <summary>Интерфейс пользовательской сессии</summary>
    [SessionGuard(SessionGuardMode.Normal)]
    public interface IUserSession
    {
      /// <summary>
      /// Идентификатор подключенного пользователя (только для чтения).
      /// </summary>
      long UserID { get; }

      /// <summary>
      /// Сетевое имя клиентского компьютера (только для чтения).
      /// </summary>
      string ComputerName { get; }

      /// <summary>Имя залогиненного пользователя (только для чтения).</summary>
      string UserName { get; }

      /// <summary>
      /// Дата и время последнего обращения к интерфейсу (в глобальном времени).
      /// </summary>
      DateTime LastCallTime { get; }

      /// <summary>
      /// Идентификатор(ы) предметной области, в которой работает эта сессия. Если пусто,
      /// то доступ к объектам из всех областей.
      /// </summary>
      string AreaID { get; set; }

      /// <summary>
      /// Идентификатор(ы) языков, в которых работает эта сессия. Если пусто,
      /// то доступ к атрибутам на всех языках.
      /// </summary>
      string LanguageID { get; set; }

      /// <summary>
      /// Максимальное количество строк, которых можно возвращать клиенту в одной выборке
      /// данных.
      /// </summary>
      int MaxRows { get; set; }

      /// <summary>
      /// Смещение времени текущей временнОй зоны рабочей
      /// станции пользователя относительно универсального времени по Гринвичу
      /// </summary>
      TimeSpan TimeZoneOffset { get; }

      /// <summary>
      /// Идентификатор роли, с которой пользователь подключился
      /// к системе
      /// </summary>
      long RoleID { get; }

      /// <summary>Время сервера</summary>
      DateTime UTCTime { get; }

      /// <summary>
      /// Уникальный идентификатор клиентского подключения к серверу приложений.
      /// Идентификатор присваивается сервером приложений при создании первой сессии клиента.
      /// Все сессии одного клиента будут иметь один и тот же идентификатор клиентского подключения;
      /// два разных клиента, вошедших под одним и тем же пользователем IPS, будут иметь разные идентификаторы.
      /// </summary>
      long ClientConnectionID { get; }

      /// <summary>Вход пользователя в систему</summary>
      /// <param name="aLoginName">Имя пользователя для входа в систему</param>
      /// <param name="aPassword">Пароль пользователя</param>
      /// <param name="aComputerName">Сетевое имя компьютера пользователя</param>
      /// <param name="aTimeZoneOffset">Смещение времени текущей временнОй зоны рабочей
      /// станции пользователя относительно универсального времени по Гринвичу (можно
      /// получить вызовом функции GetUtcOffset)</param>
      /// <param name="aRoleID">Идентификатор роли, с которой пользователь подключается
      /// к системе</param>
      [RemotingCompression(false)]
      long Login(
        string aLoginName,
        PswPackage aPassword,
        string aComputerName,
        TimeSpan aTimeZoneOffset,
        long aRoleID);

      /// <summary>Вход пользователя в систему</summary>
      /// <param name="aLoginName">Имя пользователя для входа в систему</param>
      /// <param name="aPassword">Пароль пользователя</param>
      /// <param name="aComputerName">Сетевое имя компьютера пользователя</param>
      /// <param name="aTimeZoneOffset">Смещение времени текущей временнОй зоны рабочей
      /// станции пользователя относительно универсального времени по Гринвичу (можно
      /// получить вызовом функции GetUtcOffset)</param>
      /// <param name="aRoleID">Идентификатор роли, с которой пользователь подключается
      /// к системе</param>
      /// <param name="accessLevel">Уровень доступа данной сессии (должен быть не выше уровня доступа самого юзера)</param>
      [RemotingCompression(false)]
      long Login(
        string aLoginName,
        PswPackage aPassword,
        string aComputerName,
        TimeSpan aTimeZoneOffset,
        long aRoleID,
        int accessLevel);

      /// <summary>Вход пользователя в систему</summary>
      /// <param name="aLoginName">Имя пользователя для входа в систему</param>
      /// <param name="aPassword">Пароль пользователя</param>
      /// <param name="aComputerName">Сетевое имя компьютера пользователя</param>
      /// <param name="aTimeZoneOffset">Смещение времени текущей временнОй зоны рабочей
      /// станции пользователя относительно универсального времени по Гринвичу (можно
      /// получить вызовом функции GetUtcOffset)</param>
      /// <param name="aRoleID">Идентификатор роли, с которой пользователь подключается
      /// к системе</param>
      /// <param name="sessionName">Имя сессии.</param>
      [RemotingCompression(false)]
      long Login(
        string aLoginName,
        PswPackage aPassword,
        string aComputerName,
        TimeSpan aTimeZoneOffset,
        long aRoleID,
        string sessionName);

      /// <summary>Вход пользователя в систему</summary>
      /// <param name="aLoginName">Имя пользователя для входа в систему</param>
      /// <param name="aPassword">Пароль пользователя</param>
      /// <param name="aComputerName">Сетевое имя компьютера пользователя</param>
      /// <param name="aTimeZoneOffset">Смещение времени текущей временнОй зоны рабочей
      /// станции пользователя относительно универсального времени по Гринвичу (можно
      /// получить вызовом функции GetUtcOffset)</param>
      /// <param name="aRoleID">Идентификатор роли, с которой пользователь подключается
      /// к системе</param>
      /// <param name="accessLevel">Уровень доступа сессии.</param>
      /// <param name="sessionName">Имя сессии.</param>
      [RemotingCompression(false)]
      long Login(
        string aLoginName,
        PswPackage aPassword,
        string aComputerName,
        TimeSpan aTimeZoneOffset,
        long aRoleID,
        int accessLevel,
        string sessionName);

      [RemotingCompression(false)]
      long LoginAsActingUser(ActingUserLoginParameters loginParameters);

      /// <summary>Выход пользователя из системы</summary>
      [SessionGuard(SessionGuardMode.Disabled)]
      [ClientSideDisconnectionProtection(false)]
      int Logout(string sessionName);

      /// <summary>
      /// Создает копию текущей сессии для фоновых операций и выполняет Login
      /// с текущими параметрами.
      /// </summary>
      /// <returns>Копия сессии.</returns>
      IUserSession Clone(string sessionName);

      /// <summary>Получить интерфейс для записи в журнал событий</summary>
      IEventLog EventLog { get; }

      /// <summary>Интерфейс для работы с архивным журналом событий</summary>
      IEventLog EventLogArchive { get; }

      /// <summary>Идентификатор сессии в списке сессий.</summary>
      [Obsolete("Use the property SessionGUID instead of this.", true)]
      int SessionID { get; }

      /// <summary>Глобальный идентификатор сессии в списке сессий</summary>
      Guid SessionGUID { get; }

      /// <summary>
      /// Глобальный идентификатор мастер-сессии в списке сессий
      /// </summary>
      Guid MasterSessionGUID { get; }

      /// <summary>
      /// Идентификатор пользователя, который на самом деле работает от имени текущего пользователя (исполняет его обязанности)
      /// </summary>
      long ActingUserID { get; }

      /// <summary>
      /// Имя пользователя, который на самом деле работает от имени текущего пользователя (исполняет его обязанности)
      /// </summary>
      string ActingUserName { get; }

      /// <summary>
      /// Включение данного режима позволяет включать в списки удаленные объекты. Требует
      /// админских прав.
      /// </summary>
      bool ShowDeletedObjects { get; set; }

      /// <summary>Язык по-умолчанию</summary>
      IDBLanguageType DefaultLanguage { get; }

      /// <summary>
      /// Получить массив обработчиков объектов по массиву идентификаторов версий.
      /// </summary>
      /// <param name="objectIDs">Массив идентификаторов версий объектов</param>
      /// <param name="failIfNotFound">Если true, то генерит эксепшен при отсутствии любого из запрошенных объектов</param>
      /// <returns>Массив обработчиков объектов</returns>
      IDBObject[] GetObjects(long[] objectIDs, bool failIfNotFound);

      /// <summary>
      /// Получить объект базы данных по его идентификатору objectID (F_OBJECT_ID)
      /// </summary>
      IDBObject GetObject(long objectID);

      /// <summary>
      /// Возвращает обработчик рабочей копии объекта для данного пользователя (если таковая в базе имеется).
      /// Если рабочей копии у объекта нет (или объект взят на изменение другим пользователем), то метод
      /// возвращает обработчик архивной копии объекта.
      /// </summary>
      IDBObject GetObjectActualCopy(long objectID, bool failIfNotFound);

      /// <summary>
      /// Возвращает обработчит актуальной копии объекта для данного пользователя (рабочую или архивную копию). Также возвращает заготовку, если сам объект еще не создан.
      /// </summary>
      /// <param name="objectID">Идентификатор версии объекта (знак не имеет значения)</param>
      /// <param name="failIfNotFound">Сбрасыват исключение если такой версии нет вообще</param>
      /// <returns>Обработчик объекта или null</returns>
      IDBObject GetObjectActual(long objectID, bool failIfNotFound);

      /// <summary>
      /// Получить объект базы данных по его идентификатору objectID (F_OBJECT_ID). Если throwNotFoundException == false,
      /// то возвращает null если объект не найден, а иначе при отсутствии объекта objectID генерит исключение.
      /// </summary>
      IDBObject GetObject(long objectID, bool throwNotFoundException);

      /// <summary>
      /// Получить объект базы данных по его идентификатору GUID-у. Если объект взят данным пользователем на изменение,
      /// то возвращается обработчик рабочей копии объекта.
      /// </summary>
      IDBObject GetObject(Guid objectGUID);

      /// <summary>
      /// Получить объект базы данных по его идентификатору GUID-у
      /// </summary>
      IDBObject GetObject(Guid objectGUID, bool throwNotFoundException);

      /// <summary>
      /// Возвращает схему жизненного цикла с локальным идентификатором schemaID
      /// </summary>
      IDBLCSchema GetLCSchema(int schemaID);

      /// <summary>
      /// Возвращает схему жизненного цикла с глобальным идентификатором schemaGuid
      /// </summary>
      IDBLCSchema GetLCSchema(Guid schemaGuid);

      /// <summary>
      /// Возвращает схему жизненного цикла с наименованием schemaName
      /// </summary>
      IDBLCSchema GetLCSchema(string schemaName);

      /// <summary>
      /// Возвращает схему жизненного цикла с наименованием schemaName
      /// </summary>
      IDBLCSchema GetLCSchema(string schemaName, bool throwNotFoundException);

      /// <summary>
      /// Возвращает схему жизненного цикла с локальным идентификатором schemaID
      /// </summary>
      IDBLCSchema GetLCSchema(int schemaID, bool throwNotFoundException);

      /// <summary>
      /// Возвращает схему жизненного цикла с глобальным идентификатором schemaGuid
      /// </summary>
      IDBLCSchema GetLCSchema(Guid schemaGuid, bool throwNotFoundException);

      /// <summary>
      /// Возвращает коллекцию схем жизненных циклов объектов. Если filterRecs == true, то в коллекции будут только схемы,
      /// видимые для данной сессии.
      /// </summary>
      IDBLCSchemaCollection GetLCSchemaCollection(bool filterRecs);

      /// <summary>
      /// Возвращает полную коллекцию схем жизненных циклов объектов
      /// </summary>
      IDBLCSchemaCollection GetLCSchemaCollection();

      /// <summary>Получить уровень продвижения номер aLevelID</summary>
      IDBLifecycleLevelType GetLifecycleLevel(int aLevelID);

      /// <summary>Получить уровень продвижения номер aLevelID</summary>
      IDBLifecycleLevelType GetLifecycleLevel(int aLevelID, bool throwException);

      /// <summary>Получить уровень продвижения с именем levelName</summary>
      IDBLifecycleLevelType GetLifecycleLevel(string levelName);

      /// <summary>Получить уровень продвижения с именем levelName</summary>
      IDBLifecycleLevelType GetLifecycleLevel(string levelName, bool throwException);

      /// <summary>
      /// Получить уровень продвижения с глобальным идентификатором levelGuid
      /// </summary>
      IDBLifecycleLevelType GetLifecycleLevel(Guid levelGuid);

      /// <summary>
      /// Получить уровень продвижения с глобальным идентификатором levelGuid
      /// </summary>
      IDBLifecycleLevelType GetLifecycleLevel(Guid levelGuid, bool throwException);

      /// <summary>Получить список всех уровеней продвижения</summary>
      IDBLifecycleLevelCollection GetLifecycleLevelCollection();

      /// <summary>Получить список всех уровеней продвижения</summary>
      /// <param name="filterRecs">Если == true, то список фильтруется по предметным областям и правам доступа.</param>
      IDBLifecycleLevelCollection GetLifecycleLevelCollection(bool filterRecs);

      /// <summary>
      /// Получить языковой вариант (aLanguageID - буква-идентификатор)
      /// </summary>
      IDBLanguageType GetLanguage(string aLanguageID);

      /// <summary>
      /// Получить языковой вариант (aLanguageID - буква-идентификатор)
      /// </summary>
      IDBLanguageType GetLanguage(string aLanguageID, bool throwException);

      /// <summary>Получить языковой вариант по guid-у</summary>
      IDBLanguageType GetLanguage(Guid guid);

      /// <summary>Получить языковой вариант по guid-у</summary>
      IDBLanguageType GetLanguage(Guid guid, bool throwException);

      /// <summary>Получить группу атрибутов номер aGroupID</summary>
      IDBAttributesGroup GetAttributesGroup(int aGroupID);

      /// <summary>Получить группу атрибутов номер aGroupID</summary>
      IDBAttributesGroup GetAttributesGroup(int aGroupID, bool failIfNotFound);

      /// <summary>Получить группу атрибутов по названию</summary>
      IDBAttributesGroup GetAttributesGroup(string groupName);

      /// <summary>Получить группу атрибутов по названию</summary>
      IDBAttributesGroup GetAttributesGroup(string groupName, bool failIfNotFound);

      /// <summary>Получить группу атрибутов по guid-у</summary>
      IDBAttributesGroup GetAttributesGroup(Guid guid);

      /// <summary>Получить группу атрибутов по guid-у</summary>
      IDBAttributesGroup GetAttributesGroup(Guid guid, bool failIfNotFound);

      /// <summary>Получить полный список групп атрибутов</summary>
      IDBAttributesGroupCollection GetAttributesGroupCollection();

      /// <summary>Получить полный список групп атрибутов</summary>
      /// <param name="filterRecs">Если == true, то список фильтруется по предметным областям и правам доступа.</param>
      IDBAttributesGroupCollection GetAttributesGroupCollection(bool filterRecs);

      /// <summary>
      /// Получить список групп атрибутов, входящих в состав группы parentGroupID. Если parentGroupID == 0, то возвращается верний уровень иерархии групп атрибутов.
      /// Если parentGroupID меньше 0, то возвращается полный список групп.
      /// </summary>
      IDBAttributesGroupCollection GetAttributesGroupCollection(int parentGroupID);

      /// <summary>
      /// Получить список групп атрибутов, входящих в состав группы parentGroupID. Если parentGroupID == 0, то возвращается верний уровень иерархии групп атрибутов.
      /// Если parentGroupID меньше 0, то возвращается полный список всех групп атрибутов.
      /// </summary>
      /// <param name="parentGroupID">Ид. родительской группы атрибутов. Если parentGroupID == 0, то возвращается верний уровень иерархии групп атрибутов. Если parentGroupID меньше 0, то возвращается полный список всех групп атрибутов.</param>
      /// <param name="filterRecs">Если == true, то список фильтруется по предметным областям и правам доступа.</param>
      IDBAttributesGroupCollection GetAttributesGroupCollection(int parentGroupID, bool filterRecs);

      /// <summary>Получить атрибут-тип номер anAttributeType</summary>
      IDBAttributeType GetAttributeType(int anAttributeType);

      IDBAttributeType GetAttributeType(int anAttributeType, bool failIfNotFound);

      /// <summary>Получить атрибут-тип с именем anAttributeName</summary>
      IDBAttributeType GetAttributeType(string anAttributeName);

      IDBAttributeType GetAttributeType(string anAttributeName, bool failIfNotFound);

      /// <summary>Получить атрибут-тип с гуидом anAttributeGuid</summary>
      IDBAttributeType GetAttributeType(Guid anAttributeGuid);

      IDBAttributeType GetAttributeType(Guid anAttributeGuid, bool failIfNotFound);

      /// <summary>Получить объект для работы с конфигурациями</summary>
      IDBConfigurations Configurations { get; }

      /// <summary>Получить шаг жизненного цикла номер aLCStepID</summary>
      IDBLifecycleStep GetLifecycleStep(int aLCStepID);

      IDBLifecycleStep GetLifecycleStep(int aLCStepID, bool failIfNotFound);

      /// <summary>
      /// Получить шаг жизненного цикла номер aLCStepID для объектов типа objectTypeID
      /// </summary>
      IDBLifecycleStep GetLifecycleStep(int aLCStepID, int objectTypeID);

      IDBLifecycleStep GetLifecycleStep(int aLCStepID, bool failIfNotFound, int objectTypeID);

      /// <summary>
      /// Получить шаг жизненного цикла c глобальным идентификатором anLCGuid.
      /// </summary>
      IDBLifecycleStep GetLifecycleStep(Guid anLCGuid);

      IDBLifecycleStep GetLifecycleStep(Guid anLCGuid, bool throwException);

      /// <summary>
      /// Получить шаг жизненного цикла c глобальным идентификатором anLCGuid для объектов типа objectTypeID.
      /// </summary>
      IDBLifecycleStep GetLifecycleStep(Guid anLCGuid, int objectTypeID);

      IDBLifecycleStep GetLifecycleStep(Guid anLCGuid, bool throwException, int objectTypeID);

      /// <summary>
      /// Получить коллекцию шагов жизненного цикла для объектов типа anObjectTypeID.
      /// Результатом может быть схема родительского типа в случае, если у объекта нет своей.
      /// </summary>
      IDBLifecycleStepCollection GetLifecycleStepCollection(int anObjectTypeID);

      /// <summary>
      /// Получить коллекцию шагов ЖЦ для схемы schemaID применительно к типу объектов anObjectTypeID.
      /// Если anObjectTypeID == 0, то рассматривается сама схема ЖЦ.
      /// </summary>
      IDBLifecycleStepCollection GetLifecycleStepCollection(int schemaID, int anObjectTypeID);

      /// <summary>Получить тип объектов номер anObjectType</summary>
      IDBObjectType GetObjectType(int anObjectType);

      /// <summary>
      /// Получить тип объектов номер anObjectType. Если failIfNotFound == false, то возвращает
      /// null если указанный тип не найден, а иначе при отсутствии типа генерит исключение.
      /// </summary>
      IDBObjectType GetObjectType(int anObjectType, bool failIfNotFound);

      IDBObjectType GetObjectType(string anObjectTypeName);

      IDBObjectType GetObjectType(string anObjectTypeName, bool throwException);

      IDBObjectType GetObjectType(Guid anObjectTypeGuid);

      IDBObjectType GetObjectType(Guid anObjectTypeGuid, bool throwException);

      /// <summary>
      /// Получить тип объекта по имени объекта (например, "Деталь")
      /// </summary>
      IDBObjectType GetObjectTypeByObjectName(string anObjectName, bool throwException);

      /// <summary>
      /// Метод возвращает массив обработчиков связей по их идентификаторам
      /// </summary>
      /// <param name="relationIDs">Идентификаторы связей RelationID</param>
      /// <param name="failIfNotFound">Генерить ли ошибку если любая из связей не найдена</param>
      /// <returns>Возвращает массив обработчиков связей (порядок может не совпадать с порядком в массиве идентификаторов!)</returns>
      IDBRelation[] GetRelations(long[] relationIDs, bool failIfNotFound);

      /// <summary>
      /// Получить связь между объектами по ее идентификатору aRelationID
      /// </summary>
      IDBRelation GetRelation(long aRelationID);

      /// <summary>
      /// Получить связь между объектами по ее идентификатору aRelationID. Если failIfNotFound == false, то возвращает
      /// null если связь не найдена, а иначе при отсутствии связи генерит исключение.
      /// </summary>
      IDBRelation GetRelation(long aRelationID, bool failIfNotFound);

      /// <summary>
      /// Получить связь между объектами по ее глобальному идентификатору guid и идентификатору версии родительского объекта
      /// </summary>
      IDBRelation GetRelation(Guid guid, long prjID);

      IDBRelation GetRelation(Guid guid, long prjID, bool failIfNotFound);

      /// <summary>
      /// Получить связь между объектами по ее глобальному идентификатору guid. Если родительский объект взят на изменение, то связь берётся от актуальной для данного юзера копии объекта.
      /// </summary>
      /// <param name="guid">Guid связи.</param>
      /// <param name="failIfNotFound">Нужно ли выдавать эксепшен если связь не найдена или возвращать null.</param>
      /// <returns>Возвращает обработчик связи.</returns>
      IDBRelation GetRelation(Guid guid, bool failIfNotFound);

      /// <summary>
      /// Получить связь между, обозначающую вхождение объекта partID (если versionMode==true, то partID это ObjectID, иначе ID Объекта)
      /// в объект projectID связью типа
      /// relationType
      /// </summary>
      IDBRelation GetRelation(long projectID, long partID, int relationType, bool versionMode);

      /// <summary>
      /// Получить связь между, обозначающую вхождение объекта partID (если versionMode==true, то partID это ObjectID, иначе ID Объекта)
      /// в объект projectID связью любого типа
      /// </summary>
      IDBRelation GetRelation(long projectID, long partID, bool versionMode);

      /// <summary>
      /// Получить связь между, обозначающую вхождение объекта partID (IDBObject.ID) в объект projectID (IDBObject.ObjectID) связью типа
      /// relationType
      /// </summary>
      IDBRelation GetRelation(long projectID, long partID, int relationType);

      /// <summary>
      /// Получить связь между, обозначающую вхождение объекта partID (IDBObject.ID) в объект projectID (IDBObject.ObjectID) связью любого типа
      /// </summary>
      IDBRelation GetRelation(long projectID, long partID);

      /// <summary>
      /// Получить связь с ид. aRelationID и версией дочернего объекта partObjectID. Позволяет серверному ядру сразу установить версию дочернего объекта без процедур подбора версий.
      /// </summary>
      IDBRelation GetRelationByPartObjectID(long aRelationID, long partObjectID, bool failIfNotFound);

      /// <summary>Получить тип связей номер aRelationTypeID</summary>
      IDBRelationType GetRelationType(int aRelationTypeID);

      IDBRelationType GetRelationType(int aRelationTypeID, bool throwException);

      IDBRelationType GetRelationType(Guid relationTypeGUID, bool throwException);

      IDBRelationType GetRelationType(Guid relationTypeGUID);

      IDBRelationType GetRelationType(string rtypeDescription);

      IDBRelationType GetRelationType(string rtypeDescription, bool throwException);

      /// <summary>
      /// Получить предметную область с идентификатором aSubjectAreaTypeID
      /// </summary>
      IDBSubjectAreaType GetSubjectAreaType(char aSubjectAreaTypeID);

      /// <summary>
      /// Получить предметную область с идентификатором aSubjectAreaTypeID
      /// </summary>
      IDBSubjectAreaType GetSubjectAreaType(char aSubjectAreaTypeID, bool throwException);

      /// <summary>Получить предметную область с guid</summary>
      IDBSubjectAreaType GetSubjectAreaType(Guid guid);

      /// <summary>Получить предметную область с guid</summary>
      IDBSubjectAreaType GetSubjectAreaType(Guid guid, bool throwException);

      /// <summary>Получить список предметных областей</summary>
      IDBSubjectAreaCollection GetSubjectAreaCollection();

      /// <summary>Получить список языковых вариантов</summary>
      /// <returns></returns>
      IDBLanguageCollection GetLanguageCollection();

      /// <summary>
      /// Получить коллекцию объектов типа objectType. Если objectType=-1, то
      /// получается коллекция всех объектов. Под коллекцией объектов в данном
      /// случае понимаем объект, управляющий списком объектов. Никаких данных
      /// с СУБД эта операция не получае.
      /// </summary>
      /// <param name="objectType"></param>
      /// <returns></returns>
      IDBObjectCollection GetObjectCollection(int objectType);

      IDBObjectCollection GetObjectCollection(Guid objectTypeGuid);

      /// <summary>
      /// Получить список атрибутов в группе groupID. Если groupID = -1, то получается
      /// список всех атрибутов, зарегистрированных в системе.
      /// </summary>
      /// <param name="groupID">Идентификатор группы атрибутов.</param>
      /// <param name="filterRecs">Если == true, то список фильтруется по предметным областям и правам доступа.</param>
      IDBAttributeTypeCollection GetAttributeTypeCollection(int groupID, bool filterRecs);

      /// <summary>
      /// Получить список атрибутов в группе groupID. Если groupID = -1, то получается
      /// список всех атрибутов, зарегистрированных в системе.
      /// </summary>
      IDBAttributeTypeCollection GetAttributeTypeCollection(int groupID);

      /// <summary>
      /// Возвращает коллекцию типов объектов, входящих в состав типа parentTypeID.
      /// </summary>
      /// <param name="parentTypeID">Идентификатор родительского типа объектов. Если == -1,
      /// то возвращает корневые типы объектво. Если == -2, то возвращает ВСЕ типы объектов</param>
      IDBObjectTypeCollection GetObjectTypeCollection(int parentTypeID);

      /// <summary>
      /// Возвращает коллекцию типов объектов, входящих в состав типа parentTypeID.
      /// </summary>
      /// <param name="parentTypeID">Идентификатор родительского типа объектов. Если == -1,
      /// то возвращает корневые типы объектво. Если == -2, то возвращает ВСЕ типы объектов</param>
      /// <param name="filterRecs">Если == true, то список фильтруется по предметным областям и правам доступа.</param>
      IDBObjectTypeCollection GetObjectTypeCollection(int parentTypeID, bool filterRecs);

      /// <summary>Возвращает полный список типов связей.</summary>
      IDBRelationTypeCollection GetRelationTypeCollection();

      /// <summary>Возвращает список типов связей.</summary>
      /// <param name="filterRecs">Если == true, то список фильтруется по предметным областям и правам доступа.</param>
      IDBRelationTypeCollection GetRelationTypeCollection(bool filterRecs);

      /// <summary>
      /// Возвращает интерфейс на объект, управляющий входимостями типов объектов друг в друга
      /// </summary>
      IDBRelationsApplicabilityCollection GetRelationsApplicabilityCollection();

      /// <summary>
      /// Возвращает объект-получатель списка связей типа relationType
      /// (если relationType меньше 0, то связей всех типов).
      /// </summary>
      IDBRelationCollection GetRelationCollection(int relationType);

      /// <summary>
      /// Возвращает объект-получатель списка связей типа relationType (если relationType меньше 0, то связей всех типов),
      /// при этом будет использоваться фильтрация состава на основе указанных настроек фильтрации.
      /// </summary>
      /// <param name="relationType">Тип связи</param>
      /// <param name="FiltrationOwnerID">Уникальный ID настроек фильтрации, по которым будет проводиться фильтрация состава</param>
      IDBRelationCollection GetRelationCollection(int relationType, string FiltrationOwnerID);

      /// <summary>
      /// Возвращает объект-получатель списка связей типа relationType (если relationType меньше 0, то связей всех типов),
      /// при этом будет использоваться фильтрация состава на основе указанных настроек фильтрации.
      /// </summary>
      /// <param name="relationType">Тип связи</param>
      /// <param name="rule">Набор правил фильтрации</param>
      IDBRelationCollection GetRelationCollection(int relationType, VersionsRule rule);

      /// <summary>Возвращает коллекцию для работы со списком итераций</summary>
      IDBSnapshotCollection GetSnapshotCollection();

      /// <summary>
      /// Возвращает коллекцию для работы с итерацией номер snapshotID
      /// </summary>
      IDBObjectSnapshot GetSnapshot(long snapshotID);

      /// <summary>
      /// Возвращает коллекцию для работы с итерацией номер snapshotID. Если throwException==true, то в случае отсутствия такой итерации генерирует исключение.
      /// </summary>
      IDBObjectSnapshot GetSnapshot(long snapshotID, bool throwException);

      /// <summary>Возвращает интерфейс серверной части портфеля</summary>
      IServerBriefcase GetBriefcase();

      /// <summary>Возвращает интерфейс на импортер</summary>
      /// <param name="logFileName"></param>
      /// <returns></returns>
      IDBImporter GetImporter(string logFileName);

      /// <summary>Интерфейс получателя идентификаторов</summary>
      IIDHelper IdentHelper { get; }

      /// <summary>
      /// Возвращает список ролей, которыми обладает пользователь номер userID. Если userID = -1,
      /// то возвращает список всех ролей, зарегистрированных в системе. Если userID = 0,
      /// то возвращает список ролей, которые имеются у пользователя данной сессии.
      /// </summary>
      RoleProperties[] GetRolesList(long userID);

      /// <summary>
      /// Возвращает список ролей, которыми обладает пользователь с логином loginName.
      /// Если такого юзера в системе нет, то возвращает полный список ролей.
      /// </summary>
      RoleProperties[] GetRolesList(string loginName);

      /// <summary>
      /// Получает интерфейс, зарегистрированный не сервере службой ICustomServices
      /// </summary>
      /// <param name="serviceType">Тип зарегистрированного итнерфейса</param>
      /// <returns>Требуемый итнерфейс или null</returns>
      /// <exception cref="T:System.Runtime.Serialization.SerializationException">
      /// Возникает при попытке получения сервиса, тип которого не определен не стороне сервера.
      /// Возможно из-за того, что на серверной машине нет сборки с запрошенным типом.
      /// Поэтому обращение к этому методу надо заключать в try-catch с перехватом только этого
      /// типа исключения.
      /// </exception>
      /// <example>Пример перхвата ...
      ///  <code lang="C#">try
      ///  {
      ///   ...
      ///  }
      ///  catch(System.Runtime.Serialization.SerializationException)
      ///  {
      ///  // реакция на отсутствие сборки
      ///  }
      /// </code>
      /// </example>
      object GetCustomService(Type serviceType);

      /// <summary>
      /// Проверяет работоспособность подключения к сессии сервера приложений.
      /// </summary>
      /// <exception cref="T:System.Exception">Подключение к сессии сервера приложений нарушено</exception>
      [ClientSideDisconnectionProtection(false)]
      void Test();

      /// <summary>Возвращает true, если это администратор</summary>
      bool IsAdmin { get; }

      /// <summary>Если true, то это системная сессия</summary>
      bool IsSystemSession { get; }

      /// <summary>Показывать персональные объекты других пользователей</summary>
      bool ShowPersonalObjects { get; set; }

      /// <summary>Если true, то режим разработчика</summary>
      bool DeveloperMode { get; }

      /// <summary>Является ли база эталонной</summary>
      bool EtalonBase { get; }

      /// <summary>
      /// Возвращает количество дней, оставшихся до истечения срока действия пароля. Если 0, то
      /// пароль постоянный.
      /// </summary>
      int GetExpirationDays();

      /// <summary>
      /// Возвращает список плагинов, которых нужно грузить на клиента
      /// </summary>
      DataTable GetClientPlugins();

      /// <summary>Интерфейс, позволяющий получить инфу о кэше сервера</summary>
      IServerCache ServerCache { get; }

      /// <summary>
      /// Считать из кэша таблицы. Для серверной сессии используется серверный кэш,
      /// для клиентской - локальный кэш клиента
      /// </summary>
      /// <param name="tableNames">Имена таблицы</param>
      /// <returns>Таблицы или null</returns>
      DataTable[] GetCacheTables(params string[] tableNames);

      /// <summary>
      /// Возвращает краткую информацию об объекте по идентификатору его версии
      /// </summary>
      QuickObjectInfo GetObjectInfo(long objectID);

      /// <summary>
      /// Возвращает краткую информацию об объекте по глобальному идентификатору его версии
      /// </summary>
      /// <remarks>Внимание! При поиске описаний объекта по гуиду - в кеше может быть неактуальнальная информация для ид. версии объекта!</remarks>
      QuickObjectInfo GetObjectInfo(Guid objectGUID);

      /// <summary>
      /// Возвращает версию объекта, соответствующую переданному правилу подбору версий (сделано Гинзбургом на базе версии Бобко)
      /// </summary>
      /// <param name="id">Идентификатор объекта (IDBObject.ID)</param>
      /// <param name="RuleClass">Правило подбора версий</param>
      /// <param name="throwNotFoundException">Если throwNotFoundException == false, то при отсутствии такого объекта возвращает null</param>
      /// <returns>Ссылка на интерфейс указанного объекта или null</returns>
      IDBObject GetObjectByVersionsRule(long id, VersionsRule RuleClass, bool throwNotFoundException);

      /// <summary>
      /// Возвращает версию объекта, соответствующую текущим правилам подбора версий или null
      /// </summary>
      /// <param name="id">Идентификатор объекта (IDBObject.ID)</param>
      /// <param name="FiltrationSettings">Идентификатор настроек фильтрации состава</param>
      /// <param name="throwNotFoundException">Если throwNotFoundException == false, то при отсутствии такого объекта возвращает null</param>
      IDBObject GetObjectByVersionsRule(
        long id,
        string FiltrationSettings,
        bool throwNotFoundException);

      /// <summary>
      /// Возвращает версию объекта, соответствующую текущим правилам подбора версий.
      /// guid - GUID объекта (не версии !!!!)
      /// Если throwNotFoundException == false, то при отсутствии такого объекта возвращает null
      /// </summary>
      IDBObject GetObjectByVersionsRule(
        Guid guid,
        string FiltrationSettings,
        bool throwNotFoundException);

      /// <summary>
      /// Возвращает базовую версию объекта по идентификатору объекта id
      /// </summary>
      IDBObject GetObjectBaseVersionByID(long id, bool throwNotFoundException);

      /// <summary>
      /// Получить статус версии объекта согласно указанному правилу подбора версий
      /// </summary>
      /// <param name="objectID">Идентификатор версии объекта</param>
      /// <param name="rule">Правило подбора версий</param>
      /// <returns>Статус версии объекта согласно указанному правилу подбора версий</returns>
      ObjectFiltrationState GetObjectVersionFiltrationState(long objectID, VersionsRule rule);

      /// <summary>
      /// Возвращает первую попавшуюся версию объекта с идентификатором объекта id (IDBObject.ID)
      /// Если throwNotFoundException == false, то при отсутствии такого объекта возвращает null.
      /// </summary>
      IDBObject GetObjectByID(long id, bool throwNotFoundException);

      /// <summary>
      /// Возвращает первую попавшуюся версию объекта с глобальным идентификатором объекта guid (IDBObject.GUID)
      /// Если throwNotFoundException == false, то при отсутствии такого объекта возвращает null.
      /// </summary>
      IDBObject GetObjectByID(Guid guid, bool throwNotFoundException);

      /// <summary>
      /// Возвращает массив описателей единиц измерения, зарегистрированных в БД
      /// </summary>
      MeasureDescriptor[] GetMeasuresList();

      /// <summary>
      /// Возвращает обработчик истории значений атрибута attributeID
      /// </summary>
      IDBAHistoryCollection GetHistoryCollection(int attributeID);

      /// <summary>Возвращает обработчик истории значений атрибутов</summary>
      IDBHistoryCollection GetHistoryCollection();

      /// <summary>
      /// Возвращает уровень продвижения объекта номер objectID. Если объекта в базе нет, то возвращает -1.
      /// </summary>
      int GetObjectLevel(long objectID);

      /// <summary>
      /// Проверяет есть ли у данной версии объекта рабочая копия, принадлежащая данному пользователю.
      /// </summary>
      /// <param name="objectID">Ид. версии объекта.</param>
      /// <returns>Возвращает true если рабочая копия есть.</returns>
      bool HasMyWorkCopy(long objectID);

      /// <summary>Возвращает текущие настройки нормализатора строк</summary>
      NormalizerSettings GetStringNormalizerSettings();

      /// <summary>
      /// Ф-ция возвращает текстовый отчет о проверках прав доступа, выполняемых в текущей сессии
      /// </summary>
      /// <param name="mode">Если mode == GetAccessModes.AllRecords, то возвращает отчет о всех проверках,
      /// выполненных за время жизни этой сессии. Если mode == GetAccessModes.LastCheck, то возвращает отчет
      /// о последней проверке.</param>
      string[] GetCheckAccessLog(GetAccessModes mode);

      /// <summary>
      /// Проверить, включен ли режим запоминания списка изменений, сделанных в БД сервером
      /// </summary>
      bool IsStartedLogHistory { get; }

      /// <summary>
      /// Включает режим запоминания списка изменений, сделанных в БД сервером
      /// </summary>
      void StartLogHistory();

      /// <summary>
      /// Отключает режим запоминания списка изменений, сделанных в БД сервером
      /// </summary>
      void StopLogHistory();

      /// <summary>
      /// Возвращает список изменений, сделанных в БД сервером с момента вызова ф-ции StartLogHistory
      /// </summary>
      List<CategoryValue> GetModificationsHistoryList();

      /// <summary>
      /// Возвращает массив изменений, сделанных в БД сервером с момента вызова ф-ции StartLogHistory
      /// </summary>
      CategoryValue[] GetModificationsHistoryArray();

      /// <summary>
      /// Возвращает информацию о состоянии текущей операции, выполняемой на сервере
      /// </summary>
      [Obsolete("Do not use this method anymore", true)]
      OperationStateInfo GetOperationInfo();

      /// <summary>
      /// Режим, при включении которого пользователю будет разрешено редактировать общие выборки при
      /// отсутствии такого права с сохранинием результатов в текущем сеансе работы
      /// </summary>
      bool EnableEditOwnSelections { get; set; }

      /// <summary>
      /// Режим, при включении которого разрешается работа конфигуратора составов
      /// </summary>
      bool EnabledPdmConfigurator { get; set; }

      /// <summary>
      /// Режим, при включении которого разрешается подбор версий по сериям/датам
      /// </summary>
      bool EnabledSeriesDates { get; set; }

      /// <summary>
      /// Режим, при включении которого разрешается фильтрация списков и составов объектов по атрибуту "Видимость"
      /// </summary>
      bool EnabledVisibilityFiltration { get; set; }

      /// <summary>
      /// Режим Автоматическая мягкая конкретизация создаваемых связей
      /// </summary>
      bool EnabledAutoSoftInstantiation { get; set; }

      /// <summary>
      /// Максимальное количество одновременно работающих фоновых потоков,
      /// которое может использоваться распараллеливаемыми заданиями
      /// </summary>
      int MaxTaskThreadsCount { get; set; }

      /// <summary>
      /// Зафиксирован ли контекст редактирования в контексте вызова потока, в котором работает сессия
      /// </summary>
      bool IsEditingContextFixed { get; }

      /// <summary>
      /// Идентификатор текущего контекста редактирования. Если в контексте вызова сессии есть
      /// информация о контексте редактирования, будет возвращена именно она, т.к. имеет наивысший приоритет.
      /// </summary>
      long EditingContextID { get; set; }

      /// <summary>
      /// Режим работы текущего контекста редактирования. Если в контексте вызова сессии есть
      /// информация о контексте редактирования, будет возвращена именно она, т.к. имеет наивысший приоритет
      /// </summary>
      EditingContextMode EditingContextMode { get; set; }

      /// <summary>
      /// Номер группы изменений текущего контекста редактирования. Если в контексте вызова сессии есть
      /// информация о контексте редактирования, будет возвращена именно она, т.к. имеет наивысший приоритет
      /// </summary>
      long EditingContextModificationID { get; }

      /// <summary>
      /// Источник информации о текущем контексте редактирования - глобальный, оконный (используется кэширование)
      /// </summary>
      EditingContextSource EditingContextSource { get; set; }

      /// <summary>
      /// Содержимое текущего контекста редактирования (включая связанные контексты, а также описания)
      /// Свойство кэшируется!
      /// </summary>
      /// <param name="withDescriptions">true - загружать описания каждой версии и контекстов, иначе только содержимое контекста</param>
      EditingContextsObjectContainer GetEditingContext(bool withDescriptions);

      /// <summary>
      /// Разрешено ли использовать кэш контекстов редактирования. Рекомендуется
      /// включать кэширование перед длительными операциями, которые работают с объектами, меняя их состояние,
      /// выпуском версий, т.п. Изменение данного флага попутно очищает старый кэш контекстов редактирования
      /// </summary>
      bool EnabledEditingContextsCache { get; set; }

      /// <summary>
      /// Получить информацию о текущем контексте редактирования (включая режим его работы, номер группы изменений),
      /// привязанную к Guid мастер-сессии или другим уникальным Guid-ключам. Если в контексте вызова сессии есть
      /// информация о контексте редактирования, будет возвращена именно она, т.к. имеет наивысший приоритет
      /// </summary>
      /// <param name="key">Guid мастер-сессии или другой уникальный Guid-ключ</param>
      /// <returns>Информация о текущем контексте редактирования, режиме его работы, номеру группы изменений или null, если информации нет</returns>
      CurrentEditingContext EditingContextGetData(Guid key);

      /// <summary>
      /// Установить или очистить информацию о текущем контексте редактирования, режиме его работы, номеру группы изменений
      /// </summary>
      /// <param name="key">Guid мастер-сессии или другой уникальный Guid-ключ</param>
      /// <param name="data">Информация о текущем контексте редактирования, режиме его работы, номеру группы изменений.
      /// Если указать значение null, информация будет удалена из коллекции у сессии</param>
      void EditingContextSetData(Guid key, CurrentEditingContext data);

      /// <summary>
      /// Идентификатор текущего проекта, в рамках которого работает сессия
      /// </summary>
      long CurrentProjectID { get; set; }

      /// <summary>Уровень допуска текущего пользователя</summary>
      int SecurityLevel { get; }

      /// <summary>
      /// Способ фильтрации объектов по принадлежности к проектам
      /// </summary>
      ProjectFiltrationModes ProjectFiltrationMode { get; set; }

      /// <summary>
      /// Проверяет версию базы данных для модуля moduleName на соответствие версии needVersion.
      /// Возвращает true если версия корректна. Если версия неверна, то при throwVersionException==true
      /// генерирует исключение 253, иначе возвращает false.
      /// </summary>
      /// <param name="moduleName"></param>
      /// <param name="needVersion"></param>
      /// <param name="throwVersionException"></param>
      /// <returns></returns>
      bool CheckDBVersion(string moduleName, int needVersion, bool throwVersionException);

      /// <summary>Получить идентификатор объекта (F_ID)</summary>
      /// <param name="objectID">Идентификатор версии объекта (F_OBJECT_ID)</param>
      /// <returns>Идентификатор объекта (F_ID)</returns>
      long GetObjectF_ID(long objectID);

      /// <summary>
      /// Получить список всех версий для указанного объекта (F_ID)
      /// </summary>
      /// <param name="ID">Идентификатор объекта (F_ID)</param>
      /// <returns>Cписок всех версий для указанного объекта или null</returns>
      List<long> GetObjectVersions(long ID);

      /// <summary>
      /// Получить список всех версий для указанного объекта (F_ID)
      /// </summary>
      /// <param name="ID">Идентификатор объекта (F_ID)</param>
      /// <param name="includeF_ID">Если указать true, то нулевым элементом в результирующий
      /// список будет добавлено значение идентификатора объекта (F_ID)</param>
      /// <returns>Cписок всех версий для указанного объекта или null</returns>
      List<long> GetObjectVersions(long ID, bool includeF_ID);

      /// <summary>
      /// Получить список всех версий для указанной версии объекта (F_OBJECT_ID)
      /// </summary>
      /// <param name="objectID">Идентификатор любой из версий объекта (F_OBJECT_ID)</param>
      /// <returns>Cписок всех версий для указанной версии объекта или null</returns>
      List<long> GetObjectIDVersions(long objectID);

      /// <summary>
      /// Получить список всех версий для указанной версии объекта (F_OBJECT_ID)
      /// </summary>
      /// <param name="objectID">Идентификатор любой из версий объекта (F_OBJECT_ID)</param>
      /// <param name="includeF_ID">Если указать true, то нулевым элементом в результирующий
      /// список будет добавлено значение идентификатора объекта (F_ID)</param>
      /// <returns>Cписок всех версий для указанной версии объекта или null</returns>
      List<long> GetObjectIDVersions(long objectID, bool includeF_ID);

      /// <summary>
      /// Получить версии объекта (фрагмент таблицы IMS_OBJECTS), без фильтрации по контекстам редактирования и т.п.
      /// </summary>
      /// <param name="id">Идентификатор объекта (F_ID) либо идентификатор версии объекта (в зависимости от флажка isF_ID)</param>
      /// <param name="isF_ID">false - параметр id содержит идентификатор любой версии объекта (F_OBJECT_ID),
      /// true - параметр id содержит идентификатор объекта (F_ID)</param>
      /// <param name="showBlanks">true - показывать также заготовки версий</param>
      /// <param name="showDeleted">true - показывать также удалённые версии</param>
      /// <param name="columns">Список запрашиваемых колонок. Если значение пустое, будут возвращены все колонки</param>
      /// <returns>Найденные версии объектов (фрагмент таблицы IMS_OBJECTS) либо null</returns>
      DataTable GetAllObjectVersions(
        long id,
        bool isF_ID,
        bool showBlanks,
        bool showDeleted,
        params string[] columns);

      /// <summary>
      /// Получить список версий объекта, без фильтрации по контекстам редактирования и т.п.
      /// </summary>
      /// <param name="id">Идентификатор объекта (F_ID) либо идентификатор версии объекта (в зависимости от флажка isF_ID)</param>
      /// <param name="isF_ID">false - параметр id содержит идентификатор любой версии объекта (F_OBJECT_ID),
      /// true - параметр id содержит идентификатор объекта (F_ID)</param>
      /// <param name="showBlanks">true - показывать также заготовки версий</param>
      /// <param name="showDeleted">true - показывать также удалённые версии</param>
      /// <returns>Список версий объекта или пустой список</returns>
      List<long> GetAllObjectVersionsList(long id, bool isF_ID, bool showBlanks, bool showDeleted);

      /// <summary>
      /// Возвращает интерфейс для проверки прав доступа к системе
      /// </summary>
      IDBSecurity GetSystemSecurity();

      /// <summary>Устанавливаем культуру и возвращаем её</summary>
      [Obsolete("This method is deprecated", true)]
      void GetCulture(string clientCulture);

      /// <summary>
      /// Начато ли кэширование обработчиков объектов (IDBObject)
      /// </summary>
      bool DBObjectsCacheStarted { get; }

      /// <summary>Начать кэширование обработчиков объектов (IDBObject)</summary>
      void DBObjectsCacheStart();

      /// <summary>
      /// Завершить кэширование обработчиков объектов (IDBObject)
      /// </summary>
      void DBObjectsCacheStop();

      /// <summary>Очистить кэш обработчиков объектов (IDBObject)</summary>
      void DBObjectsCacheClear();

      /// <summary>
      /// Удалить из кэша обработчиков объектов (IDBObject) объект с указанным идентификатором версии
      /// </summary>
      /// <param name="fObjectID">Идентификатор версии объекта, обработчик которой надо удалить из кэша</param>
      void DBObjectsCacheRemoveVersion(long fObjectID);

      /// <summary>
      /// Свойство (только для записи), позволяющее выполнять замену пароля у пользователя,
      /// выполняющего подключение к системе. Если изменение пароля пользователю запрещено,
      /// будет сгенерировано исключение
      /// </summary>
      PswPackage NewPassword { set; }

      /// <summary>
      /// версия алгоритма подписания объектов
      ///  ( 0 - непереносимые подписи, &gt;0 - переносимые подписи)
      /// 0 от &gt;0 отличается тем, как в кэш поступает информация
      /// о пользователе. если в виде id-ка, то подпись непереносима.
      /// если в виде guid, переносима.
      /// </summary>
      int AlgorithmVersion { get; }

      /// <summary>Поколение метаданных для текущего сервера приложений</summary>
      long MetaDataGeneration { get; }

      /// <summary>
      /// Получает список типов объектов, идентификаторы версий которых указаны в массиве objectIDs.
      /// В результате присутствуют только неудалённые объекты.
      /// </summary>
      /// <param name="objectIDs">Идентификаторы версий объектов</param>
      /// <returns>Список элементов Tuple, в котором хранятся идентификаторы версий и типа объектов.</returns>
      List<Tuple<long, int>> GetObjectTypes(ICollection<long> objectIDs);

      /// <summary>
      /// Ф-ция возвращает информацию из журнала событий о двух последних логинах текущего пользователя с компьютера, имя которого записано в данной сессии.
      /// </summary>
      UserLoginEvents GetUserLoginEvents();

      /// <summary>
      /// Метод записывает сообщение в лог-файл сервера приложений.
      /// </summary>
      /// <param name="text">Текст клиентского сообщения</param>
      /// <param name="traceFileName">Имя файла трассировки</param>
      [Obsolete("Use the method AddToTrace instead of this.", true)]
      void AddToServerTrace(string text, string traceFileName = null);

      /// <summary>
      /// Метод записывает сообщение в лог-файл сервера приложений.
      /// </summary>
      /// <param name="text">Текст клиентского сообщения</param>
      /// <param name="traceLevel">Уровень трассировки, при котором сообщение будет записано в файл</param>
      /// <param name="traceFileName">Имя файла трассировки</param>
      void AddToTrace(string text, int traceLevel, string traceFileName = null);

      /// <summary>
      /// Метод возвращает список объектов с информацией о пользователях, обязанности которых в данный момент может исполнять пользователь actingUserID.
      /// </summary>
      /// <param name="actingUserID">Ид. юзера, для которого нужно получить инфу о возможном исполнении обязанностей.</param>
      /// <returns>Если ничьи обязанности исполнять не может, то список пустой.</returns>
      List<ActingUserLoginSettings> GetActingUserLoginSettings(long actingUserID);

      /// <summary>Получить из сессии информацию модуля расширения</summary>
      /// <param name="key">Ключ</param>
      /// <returns>Значение или null</returns>
      object GetSessionPluginsData(object key);

      /// <summary>
      /// Записывает в сессию информацию модуля расширения. Следует учитывать, что записанная этим методом информация копируется при клонировании сессии.
      /// Чтобы избежать копирования, записываемый объект должен реализовывать интерфейс ISessionInstanceData.
      /// </summary>
      /// <param name="key">Ключ</param>
      /// <param name="value">Значение</param>
      void SetSessionPluginsData(object key, object value);

      /// <summary>Удалить из сессии информацию модуля расширения</summary>
      /// <param name="key">Ключ</param>
      void RemoveSessionPluginsData(object key);

      /// <summary>
      /// Включает для сессии и всех ее объектов защиту от использования вне SessionKeeper. После выхода сессии за пределы SessionKeeper любые обращения к ней или
      /// ее объектам будут приводить к возникновению исключения. Выключить режим защиты нельзя. По умолчанию, режим защиты выключен.
      /// </summary>
      [SessionGuard(SessionGuardMode.Disabled)]
      [ClientSideDisconnectionProtection(false)]
      void ActivateSessionGuard();

      /// <summary>
      /// Возвращает true, если для сессии и всех ее объектов активирована защита от использования вне SessionKeeper.
      /// </summary>
      bool IsSessionGuardActive { [SessionGuard(SessionGuardMode.Disabled), ClientSideDisconnectionProtection(false)] get; }

      /// <summary>Режим отложенной записи истории значений атрибутов</summary>
      bool IsDelayedAttrHistory { get; set; }

      /// <summary>Режим отложенной записи в журнал регистрации доступа</summary>
      bool IsDelayedEventlog { get; set; }

      /// <summary>
      /// Метод возвращает словарь с уровнями доступа, которые могут быть у юзера с логином loginName (либо все уровни, если такого логина в системе нет).
      /// Если пользователю уровень не назначен, то возвращает одну запись с минимальным уровнем доступа.
      /// </summary>
      /// <param name="loginName">Имя входа пользователя.</param>
      /// <returns>Возвращает структуру ид.уровня=наименование уровня доступа</returns>
      Dictionary<int, string> GetSecurityLevels(string loginName);

      /// <summary>
      /// Метод возвращает словарь с уровнями доступа, которые могут быть у юзера с идентификатором id (либо все уровни, если такого логина в системе нет).
      /// Если пользователю уровень не назначен, то возвращает одну запись с минимальным уровнем доступа.
      /// </summary>
      /// <param name="id">Идентификатор юзера ObjectID.</param>
      /// <returns>Возвращает структуру ид.уровня=наименование уровня доступа</returns>
      Dictionary<int, string> GetSecurityLevels(long id);

      /// <summary>
      /// Возвращает ид. объекта IDBObject.ID по ид. его версии IDBObject.ObjectID.
      /// Если такой версии объекта нет, то генерит исключение ObjectNotFoundException
      /// </summary>
      long GetIDByObjectID(long objectID);

      /// <summary>
      /// Метод возвращает интерфейс для работы с уведомляющими выборками данного пользователя
      /// </summary>
      /// <returns>Обработчик уведомляющих выборок пользователя</returns>
      INotifySamplesProcessor GetNotifySamplesProcessor();

      /// <summary>
      /// Разрешена ли передача значений атрибутов в службу автоматических уведомлений
      /// </summary>
      bool SendAttrs2DelayedNotificationMode { get; set; }

      /// <summary>
      /// Режим аннулирования всех версий объекта при аннулировании одной версии
      /// </summary>
      bool AllVersionsAnnulmentMode { get; set; }

      /// <summary>
      /// Чистит умный кэш (юзать при необъяснимом состоянии вроде бы измененных объектов)
      /// </summary>
      void ClearObjectSmartCache();

      /// <summary>
      /// Массив идентификаторов групп, в которые входит данный пользователь, а также его текущей роли и самого пользователя
      /// </summary>
      /// <returns>массив ObjectID</returns>
      long[] GetUserGroupsAndRoleID();

      /// <summary>
      /// Возвращает интерфейс для проверки или назначения прав доступа к атрибуту на шаге ЖЦ применительно к типу объектов
      /// </summary>
      /// <param name="attributeID">Ид. атрибута</param>
      /// <param name="lcStepID">Ид. шага ЖЦ</param>
      /// <param name="objectTypeID">Ид. типа объектов</param>
      /// <returns>Интерфейс для проверки или назначения прав доступа либо null, если режим расширенной проверки прав атрибутов выключен</returns>
      IDBSecurity GetAttributeLCSecurity(int attributeID, int lcStepID, int objectTypeID);

      /// <summary>
      /// Проверяет возможность выполнения обратных вызовов от сервера приложений к клиенту.
      /// Метод используется для контроля работоспособности спонсоров Remoting.
      /// </summary>
      /// <param name="testObject">Клиентский объект, используемый для проверки</param>
      /// <exception cref="T:Intermech.KernelException">Обратные вызовы невозможны</exception>
      void CheckClientBackwardConnectivity(IMClientLiveStatus testObject);

      /// <summary>
      /// Возвращает таблицу связей между версиями объекта номер id
      /// </summary>
      /// <param name="id">Ид. объекта IDBObject.ID</param>
      /// <returns>Таблица связей F_PARENT_ID -- F_OBJECT_ID</returns>
      DataTable GetObjectVersionsTree(long id);

      /// <summary>
      /// Возвращает дополнительные данные пользовательской сессии
      /// </summary>
      /// <returns></returns>
      UserAndRoleInfo GetUserAndRoleInfo();

      /// <summary>
      /// Возвращает информацию о ролях и уровнях доступа юзера по его логину
      /// </summary>
      /// <param name="loginName">логин юзера</param>
      /// <returns></returns>
      LoginInformation GetLoginInformation(string loginName);

      /// <summary>Возвращает обработчик атрибута для объекта</summary>
      /// <param name="objectID">Ид. версии объекта</param>
      /// <param name="attributeID">Ид. атрибута (локальный ид., глобальный ид. или наименование)</param>
      /// <param name="failIfNotFound">Сгенерить эксепшен если чего-то не нашлось</param>
      /// <param name="getActualCopy">Получить указанный объект или его актуальную копию</param>
      /// <returns>Обработчик атрибута</returns>
      IDBAttribute GetObjectAttribute(
        long objectID,
        object attributeID,
        bool failIfNotFound,
        bool getActualCopy);

      /// <summary>Возвращает обработчик атрибута для объекта</summary>
      /// <param name="objectID">Идентификатор версии объекта</param>
      /// <param name="attributeID">Ид. атрибута</param>
      /// <returns>Обработчик атрибута или null, если что-то пошло не так</returns>
      IDBAttribute GetObjectAttributeByID(long objectID, int attributeID);

      /// <summary>Возвращает обработчик атрибута для объекта</summary>
      /// <param name="objectID">Идентификатор версии объекта</param>
      /// <param name="attributeGUID">Глобальный ид. атрибута</param>
      /// <returns>Обработчик атрибута или null, если что-то пошло не так</returns>
      IDBAttribute GetObjectAttributeByGuid(long objectID, Guid attributeGUID);

      /// <summary>Возвращает массив значений атрибутов объекта</summary>
      /// <param name="objectID">Ид. версии объекта</param>
      /// <param name="modes">Флаги управления</param>
      /// <param name="failIfNotFound">Сгенерить эксепшен если чего-то не нашлось</param>
      /// <param name="getActualCopy">Получить указанный объект или его актуальную копию</param>
      /// <returns>Массив значений (пустой, если объекта нет)</returns>
      AttributeValues[] GetObjectAttributesValues(
        long objectID,
        GetAttributeValuesModes modes,
        bool failIfNotFound,
        bool getActualCopy);

      /// <summary>Получить таблицу со списком объектов</summary>
      /// <param name="objectTypeGuid">Гуид типа объектов</param>
      /// <param name="dbRecordSetParams">Параметры запроса</param>
      /// <returns>Таблица с объектами</returns>
      DataTable ObjectsSelect(Guid objectTypeGuid, DBRecordSetParams dbRecordSetParams);

      /// <summary>Получить таблицу со списком объектов</summary>
      /// <param name="objectTypeID">ид типа объектов</param>
      /// <param name="dbRecordSetParams">Параметры запроса</param>
      /// <returns>Таблица с объектами</returns>
      DataTable ObjectsSelect(int objectTypeID, DBRecordSetParams dbRecordSetParams);

      /// <summary>Возвращает обработчик атрибута для связи</summary>
      /// <param name="relationID">Ид. связи</param>
      /// <param name="attributeID">Ид. атрибута (локальный ид., глобальный ид. или наименование)</param>
      /// <param name="failIfNotFound">Сгенерить эксепшен если чего-то не нашлось</param>
      /// <returns>Обработчик атрибута</returns>
      IDBAttribute GetRelationAttribute(long relationID, object attributeID, bool failIfNotFound);

      /// <summary>Возвращает обработчик атрибута для связи</summary>
      /// <param name="relationID">Идентификатор связи</param>
      /// <param name="attributeID">Ид. атрибута</param>
      /// <returns>Обработчик атрибута или null, если что-то пошло не так</returns>
      IDBAttribute GetRelationAttributeByID(long relationID, int attributeID);

      /// <summary>Возвращает обработчик атрибута для связи</summary>
      /// <param name="relationID">Идентификатор связи</param>
      /// <param name="attributeGUID">Глобальный ид. атрибута</param>
      /// <returns>Обработчик атрибута или null, если что-то пошло не так</returns>
      IDBAttribute GetRelationAttributeByGuid(long relationID, Guid attributeGUID);

      /// <summary>Возвращает массив значений атрибутов связи</summary>
      /// <param name="relationID">Ид. связи</param>
      /// <param name="modes">Флаги управления</param>
      /// <param name="failIfNotFound">Сгенерить эксепшен если чего-то не нашлось</param>
      /// <returns>Массив значений (пустой, если связи нет)</returns>
      AttributeValues[] GetRelationAttributesValues(
        long relationID,
        GetAttributeValuesModes modes,
        bool failIfNotFound);

      /// <summary>Получить таблицу со списком связей</summary>
      /// <param name="relationTypeID">Ид. типа связей</param>
      /// <param name="dbRecordSetParams">Параметры запроса</param>
      /// <returns>Таблица связей</returns>
      DataTable RelationsSelect(int relationTypeID, DBRecordSetParams dbRecordSetParams);

      /// <summary>Получить системные свойства объекта</summary>
      /// <param name="objectID">Ид. версии объекта</param>
      /// <param name="failIfNotFound">Сбрасывать эксепшен если объект не найден</param>
      /// <param name="getActualCopy">Получать ли акутальную копию объекта</param>
      /// <returns></returns>
      ObjectSystemProperties GetObjectSystemProperties(
        long objectID,
        bool failIfNotFound,
        bool getActualCopy);

      /// <summary>Получить системные свойства объекта</summary>
      /// <param name="objectGuid">Гуид версии объекта</param>
      /// <param name="failIfNotFound">Сбрасывать эксепшен если объект не найден</param>
      /// <returns></returns>
      ObjectSystemProperties GetObjectSystemProperties(Guid objectGuid, bool failIfNotFound);

      /// <summary>Получить расширенные системные свойства объекта</summary>
      /// <param name="objectID">Ид версии объекта</param>
      /// <param name="failIfNotFound">Сбрасывать эксепшен если объект не найден</param>
      /// <returns></returns>
      ObjectSystemPropertiesEx GetObjectSystemPropertiesEx(long objectID, bool failIfNotFound);

      /// <summary>Получить расширенные системные свойства объекта</summary>
      /// <param name="objectGuid">Гуид версии объекта</param>
      /// <param name="failIfNotFound">Сбрасывать эксепшен если объект не найден</param>
      /// <returns></returns>
      ObjectSystemPropertiesEx GetObjectSystemPropertiesEx(Guid objectGuid, bool failIfNotFound);

      /// <summary>Возвращает значения атрибута для объекта</summary>
      /// <param name="objectID">Идентификатор версии объекта</param>
      /// <param name="attributeGUID">Глобальный ид. атрибута</param>
      /// <returns>Массив значений атрибута или null, если что-то не нашлось</returns>
      object[] GetObjectAttributeValuesByGuid(long objectID, Guid attributeGUID);

      /// <summary>Возвращает первое значение атрибута для объекта</summary>
      /// <param name="objectID">Идентификатор версии объекта</param>
      /// <param name="attributeGUID">Глобальный ид. атрибута</param>
      /// <returns>Первое значение атрибута или null, если что-то не нашлось</returns>
      object GetObjectAttributeValueByGuid(long objectID, Guid attributeGUID);

      /// <summary>
      /// Добавляет атрибут номер attributeID к объекту objectID и инициализирует его значениями initValues.
      /// </summary>
      /// <param name="objectID">Ид. версии объекта</param>
      /// <param name="attributeID">Ид. атрибута</param>
      /// <param name="failIfNotFound">Генерировать ли исключение если объекта нет</param>
      /// <param name="failIfExists">Если failIfExists==true и атрибут уже существует, то генерируется исключение. Иначе присваивает атрибуту значения nitValues</param>
      /// <param name="initValues">Значения, которыми нужно проинициализировать атрибут</param>
      /// <returns>Возвращает обработчик добавленного атрибута либо null</returns>
      IDBAttribute AddObjectAttribute(
        long objectID,
        int attributeID,
        bool failIfNotFound,
        bool failIfExists,
        object[] initValues);

      /// <summary>
      /// Добавляет атрибут номер attributeID к связи relationID и инициализирует его значениями initValues.
      /// </summary>
      /// <param name="relationID">Ид. связи</param>
      /// <param name="attributeID">Ид. атрибута</param>
      /// <param name="failIfNotFound">Генерировать ли исключение если связи нет</param>
      /// <param name="failIfExists">Если failIfExists==true и атрибут уже существует, то генерируется исключение. Иначе присваивает атрибуту значения nitValues</param>
      /// <param name="initValues">Значения, которыми нужно проинициализировать атрибут</param>
      /// <returns>Возвращает обработчик добавленного атрибута либо null</returns>
      IDBAttribute AddRelationAttribute(
        long relationID,
        int attributeID,
        bool failIfNotFound,
        bool failIfExists,
        object[] initValues);

      /// <summary>
      /// Присваивает объекту objectID атрибуты attributeValues
      /// </summary>
      /// <param name="objectID">Ид. версии объекта</param>
      /// <param name="failIfNotFound">Генерировать ли исключение если объекта нет</param>
      /// <param name="attributeValues">Набор атрибутов и их значений, которые нужно присвоить объекту. Другие атрибуты у объекта не удаляются.</param>
      void SetObjectAttributesValues(
        long objectID,
        bool failIfNotFound,
        AttributeValues[] attributeValues);

      /// <summary>
      /// Присваивает связи relationID атрибуты attributeValues
      /// </summary>
      /// <param name="relationID">Ид. связи</param>
      /// <param name="failIfNotFound">Генерировать ли исключение если связи нет</param>
      /// <param name="attributeValues">Набор атрибутов и их значений, которые нужно присвоить связи. Другие атрибуты у связи не удаляются.</param>
      void SetRelationAttributesValues(
        long relationID,
        bool failIfNotFound,
        AttributeValues[] attributeValues);

      /// <summary>
      /// Берет объект на изменение и возвращает идентификатор рабочей копии
      /// </summary>
      /// <param name="objectID">Ид. архивной копии</param>
      /// <returns>Ид. рабочей копии</returns>
      long CheckOutCommand(long objectID);

      /// <summary>
      /// Завершает изменение объекта либо сохраняет изменения в его архивную копию
      /// </summary>
      /// <param name="objectID">Идентификатор рабочей копии объекта</param>
      /// <param name="preserveWorkingCopies">Нужно ли оставлять объект взятым на изменение</param>
      /// <returns>Ид. объекта после выполнения команды</returns>
      long CheckInCommand(long objectID, bool preserveWorkingCopies);

      /// <summary>Возвращает массив значений атрибутов объекта</summary>
      /// <param name="objectID">Идентификатор версии объекта</param>
      /// <param name="attributesID">Массив идентификаторов атрибутов, которые нужно получить</param>
      /// <param name="modes">Флаги для запроса списка атрибутов</param>
      /// <param name="failIfNotFound">Выдавать ли эксепшен если не найден объект</param>
      /// <returns>Массив со значениями атрибутов. Если атрибута у объекта нет или флаги modes не позволили его увидеть, то соотв. элемент массива будет null.
      /// Если объекта нет и failIfNotFound=false, то ф-ция вернет null.</returns>
      AttributeValues[] GetObjectAttributesValues(
        long objectID,
        int[] attributesID,
        GetAttributeValuesModes modes,
        bool failIfNotFound);

      /// <summary>Возвращает массив значений атрибутов связи</summary>
      /// <param name="relationID">Идентификатор связи</param>
      /// <param name="attributesID">Массив идентификаторов атрибутов, которые нужно получить</param>
      /// <param name="modes">Флаги для запроса списка атрибутов</param>
      /// <param name="failIfNotFound">Выдавать ли эксепшен если не найдена связь</param>
      /// <returns>Массив со значениями атрибутов. Если атрибута у связи нет или флаги modes не позволили его увидеть, то соотв. элемент массива будет null.
      /// Если связи нет и failIfNotFound=false, то ф-ция вернет null.</returns>
      AttributeValues[] GetRelationAttributesValues(
        long relationID,
        int[] attributesID,
        GetAttributeValuesModes modes,
        bool failIfNotFound);

      /// <summary>
      /// Устанавливает максимальный уровень доступа, с которым данная сессия может логиниться с указанного клиента
      /// </summary>
      /// <param name="clientAccessLevel">Уровень доступа</param>
      /// <param name="machineMame">Имя компьютера, с которого устанавливают доступ</param>
      void SetClientAccessLevel(int clientAccessLevel, string machineMame);

      /// <summary>
      /// Указывает системе на начало процесса удаления указанных объектов
      /// </summary>
      /// <param name="objectIDs">Список ObjectID удаляемых объектов</param>
      void BeginDeleteObjects(IEnumerable<long> objectIDs);

      /// <summary>
      /// Указывает системе на завершение процесса удаления объектов
      /// </summary>
      void EndDeleteObjects();
    }
}
