
// Type: Intermech.Interfaces.IAdminUtilsService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Интерфейс класса, который содержит разные админские методы для управления базой
    /// </summary>
    public interface IAdminUtilsService
    {
      /// <summary>Перечитывает кэш из базы данных</summary>
      void ReloadCache(Guid sessionGUID);

      /// <summary>
      /// Перечитывает некоторые серверные настройки из базы данных
      /// </summary>
      void ReloadServerSwitches(Guid sessionGUID);

      /// <summary>
      /// Осуществляет проверку и исправление целостности данных. Возвращает отчет о проделанной работе.
      /// </summary>
      string[] RepairData(Guid sessionGUID);

      /// <summary>
      /// Перегенерирует основную таблицу представления объектов IMS_OBJECTS_VIEW
      /// </summary>
      void RebuildObjectsView(Guid sessionGUID);

      /// <summary>
      /// Чистка базы данных от мусора. Возвращает лог с результатами чистки.
      /// </summary>
      string[] ClearTrash(Guid sessionGUID);

      /// <summary>
      /// Возвращает информацию о состоянии процесса чистки базы данных
      /// </summary>
      OperationStateInfo ClearingStateInfo { get; }

      /// <summary>Останавливает процесс чистки мусора в базе</summary>
      void StopClearTrash(Guid sessionGUID);

      /// <summary>
      /// Перестраивает нормализованные индексы и возвращает лог с информацией о данной операции
      /// </summary>
      string[] RebuildIndexes(Guid sessionGUID);

      /// <summary>
      /// Возвращает информацию о состоянии процесса перестройки индексов
      /// </summary>
      OperationStateInfo IndexingStateInfo { get; }

      /// <summary>Останавливает процесс перестройки индексов</summary>
      void StopRebuildIndexes(Guid sessionGUID);

      /// <summary>Перечитывает из базы настройки индексирования</summary>
      void ReloadIndexSettings();

      /// <summary>
      /// Функция выгружает сервер приложений. Сессия sessionGUID должна иметь админские права. Юзать крайне осторожно!!!
      /// </summary>
      void CloseApplicationServer(Guid sessionGUID);

      /// <summary>
      /// Возвращает true, если включен сбор статистики оптимизатора запросов
      /// </summary>
      bool GetOptimizerStatisticsFlag();

      /// <summary>
      /// Включает/выключает сбор статистики оптимизатором запросов
      /// </summary>
      void SetOptimizerStatisticsFlag(bool flag, Guid sessionGUID);

      /// <summary>
      /// Возвращает таблицу со статистикой оптимизатора запросов
      /// </summary>
      DataTable GetOptimizerStatistics(Guid sessionGUID);

      /// <summary>Сбрасывает статистику оптимизатора запросов</summary>
      void ClearStatistics(Guid sessionGUID);

      /// <summary>
      /// Перечитывает из базы данных кэш со списком единиц измерения
      /// </summary>
      void ReloadMeasuresList(Guid sessionGUID);

      /// <summary>Получаем конфигурацию сервера</summary>
      string[] GetServerConfigInfo();

      /// <summary>
      /// Ф-ция подсчитывает количество версий объектов типа objectTypeID, имеющих в наличии атрибуты, которые не описаны у данного типа объектов.
      /// Рабочие копии тоже учитываются.
      /// Возвращает таблицу с идентификаторами атрибутов и количеством объектов с данным атрибутом, а также общее количество объектов данного типа.
      /// </summary>
      int FindInvalidObjectAttributes(int objectTypeID, Guid sessionGUID, out DataTable tbl);

      /// <summary>Останавливает процесс поиска недопустимых связей</summary>
      IDBRelationCollection GetRelationCollection(Guid sessionGUID, int relationTypeID);

      /// <summary>
      /// Возвращает отчет о назначенных правах доступа для пользователя/группы/роли с идентификатором userID
      /// </summary>
      string[] GetAccessReport(Guid sessionGUID, long userID);

      /// <summary>
      /// Метод объединяет указанные версии объектов в одну версию toObjectID
      /// </summary>
      /// <param name="sessionGUID">Гуид сессии</param>
      /// <param name="objectIDs">Массив объединяемых версий объектов</param>
      /// <param name="toObjectID">Ид. версии объекта, в которую нужно перекинуть все ссылки и связи указанных объектов</param>
      void CombineObjects(Guid sessionGUID, long[] objectIDs, long toObjectID);

      /// <summary>
      /// Метод объединяет указанные атрибуты в один атрибут toAttributeID
      /// </summary>
      /// <param name="sessionGUID">Гуид сессии</param>
      /// <param name="attributeIDs">Массив объединяемых атрибутов</param>
      /// <param name="toAttributeID">Ид. атрибута, в который нужно перекинуть данные из объединяемых атрибутов</param>
      /// <param name="combineMode">Режим объединения данных</param>
      string[] CombineAttributes(
        Guid sessionGUID,
        int[] attributeIDs,
        int toAttributeID,
        CombineAttributeMode combineMode);

      /// <summary>
      /// Метод исправляет шаги ЖЦ для объектов типа objectTypeID (и дочерних типов) в случае, если они не относятся к схеме, указанной для данного типа объектов.
      /// Возвращает лог с результатами работы метода.
      /// </summary>
      string[] FixLCSteps(Guid sessionGUID, int objectTypeID);

      /// <summary>
      /// Чистит каталог IMBASE, удаляя его содержимое и таблицы, оставшиеся без ссылок
      /// </summary>
      /// <param name="sessionGUID">Сессия пользователя</param>
      /// <param name="catalogID">Ид. зачищаемого каталога</param>
      /// <param name="deleteSelf">Удалять ли из базы сам каталог</param>
      /// <returns>Возвращает текстовый лог удаления объектов IMBASE.</returns>
      string[] PurgeIMBASECatalog(Guid sessionGUID, long catalogID, bool deleteSelf);

      /// <summary>
      /// Удаляет из базы данных объекты указанных типов (не взирая на зависимости, права доступа и пр.). Требует прав доступа администратора.
      /// </summary>
      /// <param name="sessionGUID">Сессия пользователя</param>
      /// <param name="objectTypeIDs">Массив с идентификаторами типов объектов, экземпляры которых нужно удалить</param>
      /// <returns>Возвращает текстовый лог удаления объектов.</returns>
      string[] PurgeObjectsByType(Guid sessionGUID, int[] objectTypeIDs);

      /// <summary>
      /// Возвращает список объектов (включая заготовки), у которых присутствует атрибут attributeID
      /// </summary>
      /// <param name="sessionGUID">Сессия пользователя</param>
      /// <param name="attributeID">Идентификатор атрибута</param>
      /// <returns>Таблица объектов с атрибутом attributeID</returns>
      DataTable GetAttributeApplicability(Guid sessionGUID, int attributeID);

      [Obsolete("Следует использовать int SynchronizeDirectoryReadConfig(Guid sessionGUID, out bool multiDomainSyncEnabled...", true)]
      int SynchronizeDirectoryReadConfig(
        Guid sessionGUID,
        out string catalogName,
        out List<string> exclusionUserSIDsout);

      /// <summary>
      /// Чтение конфигурации синхронизации со службой каталогов.
      /// </summary>
      /// <param name="sessionGUID"></param>
      /// <param name="catalogName">имя домена из списка, полученного по SynchronizeDirectoryReadCatalogs</param>
      /// <param name="exclusionUserSIDsout">список SID пользователей, исключенных их синхронизации</param>
      /// <returns></returns>
      int SynchronizeDirectoryReadConfig(
        Guid sessionGUID,
        out string defaultCatalog,
        out HybridDictionary catalogsAndExclusionUsers);

      /// <summary>
      /// Запись конфигурации синхронизации со службой каталогов
      /// Предварительно список разрешенных доменов должен быть оформлен через SynchronizeDirectoryWriteCatalogs
      /// </summary>
      /// <param name="sessionGUID"></param>
      /// <param name="catalogName">имя домена</param>
      /// <param name="exclusionUsers">список SID пользователей, исключенных их синхронизации</param>
      /// <param name="withSync">с синхронизацией</param>
      /// <returns></returns>
      [Obsolete("Следует использовать int SynchronizeDirectoryWriteConfig(Guid sessionGUID, bool multiDomainSyncEnabled...", true)]
      int SynchronizeDirectoryWriteConfig(
        Guid sessionGUID,
        string catalogName,
        List<string> exclusionUsers,
        bool withSync);

      /// <summary>
      /// 
      /// </summary>
      /// <param name="sessionGUID"></param>
      /// <param name="defaultCatalog"></param>
      /// <param name="catalogsAndExclusionUsers"></param>
      /// <param name="withSync"></param>
      /// <returns></returns>
      int SynchronizeDirectoryWriteConfig(
        Guid sessionGUID,
        string defaultCatalog,
        HybridDictionary catalogsAndExclusionUsers,
        bool withSync);

      /// <summary>
      /// Синхронизация пользователей IPS с каталогом Active Directory (полная по всем настроенным каталогам)
      /// </summary>
      /// <param name="sessionGUID"></param>
      /// <returns>0 - нет ошибок</returns>
      int SynchronizeDirectoryProcess(Guid sessionGUID);

      /// <summary>
      /// Синхронизация пользователей IPS с каталогом Active Directory
      /// </summary>
      /// <param name="sessionGUID"></param>
      /// <param name="domainName"></param>
      /// <returns></returns>
      int SynchronizeDirectoryProcess(Guid sessionGUID, string domainName);

      /// <summary>
      /// Чтение списка пользователей IPS. Вынесено в интерфейс, т.к. вызывается как службой на сервере, так и настройкой на клиенте.
      /// </summary>
      /// <param name="sessionGUID"></param>
      /// <param name="users">key - user name, value - HybridDictionary свойств, см. LdapConsts.ADxxx</param>
      /// <returns></returns>
      int ReadDBUsers(Guid sessionGUID, out HybridDictionary users);

      /// <summary>Метод очищает SiteID у указанных версий объектов</summary>
      /// <param name="sessionGUID">Гуид сессии</param>
      /// <param name="objectIDs">Идентификаторы версий объектов</param>
      void ClearSiteIDs(Guid sessionGUID, long[] objectIDs);

      /// <summary>
      /// Возвращает массив строк со списком пользовательских сессий, открытых на сервере
      /// </summary>
      /// <param name="sessionGUID">Гуид сессии, которая запрашивает список</param>
      /// <returns>Массив строк со списком пользовательских сессий, открытых на сервере</returns>
      string[] GetSessionsList(Guid sessionGUID);

      /// <summary>
      /// Устанавливает новое значение времени жизни кэша проверок прав доступа в памяти сервера приложений
      /// </summary>
      /// <param name="sessionGUID">Гуид админской сессии</param>
      /// <param name="aclf">Время в минутах</param>
      void SetAccessCacheLifetime(Guid sessionGUID, int aclf);

      /// <summary>
      /// Возвращает список атрибутов, которые нигде не применяется (по мнению ядра)
      /// </summary>
      /// <param name="sessionGUID">Гуид сессии</param>
      /// <returns></returns>
      DataTable GetIdleAttributes(Guid sessionGUID);

      /// <summary>
      /// Метод обновляет представления данных для указанных версий объектов
      /// </summary>
      /// <param name="sessionGUID">Ссессия пользователя</param>
      /// <param name="objectIDs">Массив ObjectID</param>
      void RepairViews4Objects(Guid sessionGUID, long[] objectIDs);

      /// <summary>
      /// Метод обновляет представления данных для указанных связей
      /// </summary>
      /// <param name="sessionGUID">Ссессия пользователя</param>
      /// <param name="relationIDs">Массив RelationID</param>
      void RepairViews4Relations(Guid sessionGUID, long[] relationIDs);

      /// <summary>
      /// Метод ищет петли в составе всех версий указанных объектов по всем допустимым для них типам связей
      /// </summary>
      /// <param name="sessionGUID">Ссессия пользователя</param>
      /// <param name="IDs">Массив ID объектов</param>
      string[] FindCycleRelations(Guid sessionGUID, long[] IDs);

      /// <summary>
      /// Ф-ция преобразует набор версий одного объекта в другой объект. Тип версий объектов остается неизменным.
      /// </summary>
      /// <param name="sessionGUID">Ссессия пользователя</param>
      /// <param name="objectIDs">Массив ObjectID объектов</param>
      /// <returns>Возвращает ид. нового объекта (IDBObject.ID)</returns>
      long ConvertVersions2Object(Guid sessionGUID, long[] objectIDs);

      /// <summary>
      /// Изменить дату и время создания объекта (для разных задач закачки данных)
      /// </summary>
      /// <param name="sessionGUID">Гуид сессии</param>
      /// <param name="objectID">Ид. версии объекта</param>
      /// <param name="createDate">Новая дата в локальном времени сессии</param>
      void ChangeObjectCreateDate(Guid sessionGUID, long objectID, DateTime createDate);

      /// <summary>
      /// Проверяет права выполнения административных процедур и записывает событие в журнал
      /// </summary>
      /// <param name="sessionGUID">Гуид сессии</param>
      /// <param name="procName">Название процедуры</param>
      void CheckAdminProcedureAccess(Guid sessionGUID, string procName);
    }
}
