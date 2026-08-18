
// Type: Intermech.Interfaces.WebPortal.IPortalConnector
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Protection;
using System;
using System.Data;


namespace Intermech.Interfaces.WebPortal
{
    /// <summary>Интерфейс на серверный сервис для работы с порталом</summary>
    public interface IPortalConnector
    {
      /// <summary>Получить версию портала</summary>
      string PortalVersion { get; }

      /// <summary>Залогиниться к порталу</summary>
      /// <param name="sessionGuid">GUID сессии, данные которой использовать для авторизации в портале</param>
      /// <returns>Глобальный идентификатор соединения</returns>
      Guid Login(Guid sessionGuid);

      /// <summary>Закончить работу с порталом</summary>
      /// <param name="connectGuid">Глобальный идентификатор соединения</param>
      void Logout(Guid connectGuid);

      /// <summary>Является ли пользователь администратором на портале</summary>
      /// <param name="sessionGuid">Глобальный идентификатор пользовательской сессии</param>
      /// <returns></returns>
      bool IsAdmin(Guid sessionGuid);

      /// <summary>
      /// Получить список типов объектов, используемых порталом (непосредственно с портала)
      /// </summary>
      /// <param name="connectGuid">Глобальный идентификатор соединения</param>
      /// <returns></returns>
      PortalObjectType[] GetObjectTypesTree(Guid connectGuid);

      /// <summary>
      /// Получить дату последней модификации метаданных на портале
      /// </summary>
      /// <param name="connectGuid">Глобальный идентификатор соединения</param>
      /// <returns></returns>
      DateTime LastModifyMetadata(Guid connectGuid);

      /// <summary>
      /// Получить список атрибутов для типа связей "Состав опубликованного объекта"
      /// </summary>
      /// <param name="connectGuid">Глобальный идентификатор соединения</param>
      /// <returns></returns>
      PortalAttributeType[] GetPublishRelationAttributes(Guid connectGuid);

      /// <summary>
      /// Получить список атрибутов и допустимых значений для атрибутов используемых в типах объектов "Опубликованные объекты"
      /// и атрибутов для типа связей "Состав опубликованного объекта"
      /// </summary>
      /// <param name="connectGuid">Глобальный идентификатор соединения</param>
      /// <returns></returns>
      AttributePossibleValues[] GetAttributePossibleValues(Guid connectGuid);

      /// <summary>Начало публикации пачки объектов/связей (задачи)</summary>
      /// <param name="connectGuid">Глобальный идентификатор соединения</param>
      /// <param name="taskName">Название задачи</param>
      /// <param name="enabledSites">Строка с кодами узлов информационной системы, на которые может быть скопирован данный объект</param>
      /// <param name="packetID"></param>
      /// <returns>Идентификатор созданной задачи публикации</returns>
      long StartPublishingTask(Guid connectGuid, string taskName, string enabledSites, long packetID);

      /// <summary>Начало публикации пачки объектов/связей (задачи)</summary>
      /// <param name="connectGuid">Глобальный идентификатор соединения</param>
      /// <param name="taskName">Название задачи</param>
      /// <param name="enabledSites">Строка с кодами узлов информационной системы, на которые может быть скопирован данный объект</param>
      /// <returns>Идентификатор созданной задачи публикации</returns>
      long StartPublishingTask(Guid connectGuid, string taskName, string enabledSites);

      /// <summary>Добавить в задачу публикации объект/связь</summary>
      /// <param name="connectGuid">Глобальный идентификатор соединения</param>
      /// <param name="taskID">Идентификатор задачи публикации</param>
      /// <param name="unit">Публикуемые объект/связь</param>
      void PublishUnit(Guid connectGuid, long taskID, TransferedObject unit);

      /// <summary>
      /// Запись на портале файла для публикуемого объекта/связи
      /// </summary>
      /// <param name="connectGuid">Глобальный идентификатор соединения</param>
      /// <param name="unitGuid">Глобальный идентификатор TransferedObject, которому принадлежит файл</param>
      /// <param name="fileName">Имя файла</param>
      /// <param name="bytes">Байты с данными</param>
      /// <param name="continuation">Флаг того, что передача файла в режипе продолжения (первый раз false, потом true)</param>
      void TransferPublishUnitFile(
        Guid connectGuid,
        string unitGuid,
        string fileName,
        byte[] bytes,
        bool continuation);

      /// <summary>Завершение сеанса публикации.</summary>
      /// <param name="connectGuid">Глобальный идентификатор соединения</param>
      /// <param name="taskID">Идентификатор задачи публикации</param>
      /// <param name="deleteTask">Удалять задачу после успешного завершения публикации</param>
      void CompletePublish(Guid connectGuid, long taskID, bool deleteTask);

      /// <summary>Удалить из списка задач завершенную задачу публикации</summary>
      /// <param name="connectGuid">Глобальный идентификатор соединения</param>
      /// <param name="taskID">Идентификатор задачи публикации</param>
      void DeletePublishTask(Guid connectGuid, long taskID);

      /// <summary>Удалить из списка задач завершенную задачу публикации</summary>
      /// <param name="sessionGuid">Глобальный идентификатор соединения</param>
      /// <param name="taskID">Идентификатор задачи публикации на портале</param>
      /// <param name="deleteMode">Режим удаления:
      /// 1 - удаляет только задачу публикации (используется для успешно завершенной задачи)
      /// 0 - удаляет также временные файлы публикуемых объектов (используется при удалении незаконченной задачи, с ошибкой)
      /// </param>
      void DeletePublishTask(Guid sessionGuid, long taskID, int deleteMode);

      /// <summary>Получить статус задачи синхронизации</summary>
      /// <param name="connectGuid">Глобальный идентификатор соединения</param>
      /// <param name="taskID">Идентификатор задачи публикации</param>
      /// <returns></returns>
      TaskStatus GetTaskStatus(Guid connectGuid, long taskID);

      /// <summary>Завершить автоматическое обновление</summary>
      /// <param name="sessionGuid">Глобальный идентификатор пользовательской сессии</param>
      /// <param name="objectsIDs">Идентификаторы опубликованных объектов</param>
      /// <param name="withComposition">Завершить получение автообновлений также и у состава объектов</param>
      /// <returns></returns>
      string[] AutoImportComplete(Guid sessionGuid, long[] objectsIDs, bool withComposition);

      /// <summary>Получить идентификаторы обновлений для текущего узла</summary>
      /// <param name="connectGuid">Глобальный идентификатор соединения</param>
      /// <param name="sessionGuid">GUID сессии</param>
      string[] GetUpdates(Guid connectGuid, Guid sessionGuid);

      /// <summary>Получить код узла, который инициировал обновление</summary>
      /// <param name="connectGuid">Глобальный идентификатор соединения</param>
      /// <param name="updateGUID">Глобальный идентификатор обновления</param>
      string GetUpdateAuthor(Guid connectGuid, string updateGUID);

      /// <summary>Начало получения изменения</summary>
      /// <param name="connectGuid">Глобальный идентификатор соединения</param>
      /// <param name="updateGUID">Глобальный идентификатор обновления</param>
      TransferedObject[] GetUpdateUnit(Guid connectGuid, string updateGUID);

      /// <summary>
      /// Установить флаг статуса для изменения "В работе", что означает, что данные по
      /// функции StartUpdateUnit приняты успешно и клиент начал обработку данных
      /// </summary>
      /// <param name="connectGuid">Глобальный идентификатор соединения</param>
      /// <param name="updateGUID">Глобальный идентификатор обновления</param>
      void StartUpdateUnit(Guid connectGuid, string updateGUID);

      /// <summary>
      /// Получить очередную порцию байт файла с атрибутами обновления
      /// </summary>
      /// <param name="connectGuid">Глобальный идентификатор соединения</param>
      /// <param name="transferedGuid">Глобальный идентификатор экземпляра TransferedObject</param>
      /// <param name="fileName">Имя файла в массиве файлов изменения</param>
      /// <param name="startPosition">Стартовая позиция в потоке с которой начинать чтение</param>
      /// <returns></returns>
      byte[] GetUpdateAttributesFile(
        Guid connectGuid,
        Guid transferedGuid,
        string fileName,
        long startPosition);

      /// <summary>Получить размер файла</summary>
      /// <param name="connectGuid">Глобальный идентификатор соединения</param>
      /// <param name="transferedGuid">Глобальный идентификатор экземпляра TransferedObject</param>
      /// <param name="fileName">Имя файла в массиве файлов изменения</param>
      /// <returns></returns>
      long GetUpdateAttributesFileLength(Guid connectGuid, Guid transferedGuid, string fileName);

      /// <summary>Окончание получения изменения</summary>
      /// <param name="connectGuid">Глобальный идентификатор соединения</param>
      /// <param name="updateGUID">Глобальный идентификатор обновления</param>
      void EndUpdateUnit(Guid connectGuid, string updateGUID);

      /// <summary>
      /// Окончание получения изменения со взятием объектов во владение
      /// </summary>
      /// <param name="connectGuid">Глобальный идентификатор соединения</param>
      /// <param name="updateGUID">Глобальный идентификатор обновления</param>
      /// <param name="guids">Глобальные идентификаторы объектов которые беруться во владение</param>
      void EndUpdateUnit(Guid connectGuid, string updateGUID, string[] guids);

      /// <summary>
      /// Получить список опубликованных объектов указанного типа
      /// </summary>
      /// <param name="connectGuid">Глобальный идентификатор соединения</param>
      /// <param name="objectType">Тип опубликованных объектов (на портале)</param>
      /// <param name="dbParams">Параметры запроса</param>
      /// <returns></returns>
      PublishObjectsTable SelectPublishObjects(
        Guid connectGuid,
        int objectType,
        DBQueryParams dbParams);

      /// <summary>Имортировать опубликованные объекты с портала</summary>
      /// <param name="sessionGuid">GUID сессии</param>
      /// <param name="priority">Приоритет</param>
      /// <param name="objectsIDs">Идентификаторы опубликованных объектов</param>
      /// <param name="filteredTypes"></param>
      /// <param name="setOwner">Получить права владения</param>
      /// <param name="autoUpdate">Автоматически получать изменения в импортируемых объектах</param>
      /// <param name="compositionType">Тип запроса состава</param>
      /// <param name="startImmediately">Начать импорт незамедлительно</param>
      void ImportObjects(
        Guid sessionGuid,
        TaskPriority priority,
        long[] objectsIDs,
        int[] filteredTypes,
        bool setOwner,
        bool autoUpdate,
        SelectCompositionType compositionType,
        bool startImmediately);

      /// <summary>Импортировать пакеты</summary>
      /// <param name="sessionGuid">GUID сессии</param>
      /// <param name="priority">Приоритет</param>
      /// <param name="packetIDs"></param>
      /// <param name="importVersionsMode"></param>
      /// <param name="startImmediately"></param>
      void ImportPackets(
        Guid sessionGuid,
        TaskPriority priority,
        long[] packetIDs,
        ImportVersionsModes importVersionsMode,
        bool startImmediately);

      /// <summary>Импорт пакета завершен</summary>
      /// <param name="connectGuid">Глобальный идентификатор соединения</param>
      /// <param name="packetID">Идентификатор пакета</param>
      void PacketImportComplete(Guid connectGuid, long packetID);

      /// <summary>Получить значения атрибутов для объекта</summary>
      /// <param name="connectGuid">Глобальный идентификатор соединения</param>
      /// <param name="objectID">Идентификатор опубликованного объекта</param>
      /// <param name="attrIDs">Массив идентификаторами атрибутов (м.б. глобальными идентификаторами, наименованиями либо для получения обязательных
      /// атрибутов объекта значения ObligatoryObjectAttributes, например "F_LC_STEP"), значения которых нужно получить</param>
      /// <returns></returns>
      PublishAttribute[] GetObjectAttributes(Guid connectGuid, long objectID, params string[] attrIDs);

      /// <summary>Получить значения атрибутов для связи</summary>
      /// <param name="connectGuid">Глобальный идентификатор соединения</param>
      /// <param name="relationID">Идентификатор опубликованной связи</param>
      /// <param name="attrIDs">Массив идентификаторами атрибутов (м.б. глобальными идентификаторами, наименованиями либо для получения обязательных
      /// атрибутов связи значения ObligatoryObjectAttributes, например "F_CREATE_DATE"), значения которых нужно получить</param>
      /// <returns></returns>
      PublishAttribute[] GetRelationAttributes(
        Guid connectGuid,
        long relationID,
        params string[] attrIDs);

      /// <summary>
      /// Получить список объектов состава импортируемого объекта, включая связанные объекты.
      /// </summary>
      /// <param name="sessionGuid">GUID сессии</param>
      /// <param name="objectIDs"></param>
      /// <param name="filteredTypes"></param>
      /// <param name="countLevels"></param>
      /// <returns></returns>
      long[] GetImportComposition(
        Guid sessionGuid,
        long[] objectIDs,
        int[] filteredTypes,
        int countLevels);

      /// <summary>Получить состав опубликованного объекта</summary>
      /// <param name="sessionGuid">GUID сессии</param>
      /// <param name="connectGuid">Глобальный идентификатор соединения</param>
      /// <param name="objectID">Идентификатор версии опубликованного объекта, для которого необходимо получить состав</param>
      /// <param name="dbParams">Параметры запроса</param>
      /// <param name="countLevels">Рекурсивный состав</param>
      /// <returns></returns>
      PublishObjectsTable SelectComposition(
        Guid sessionGuid,
        Guid connectGuid,
        long objectID,
        DBQueryParams dbParams,
        int countLevels);

      /// <summary>Получить список пользователей узла.</summary>
      /// <param name="connectGuid">Глобальный идентификатор соединения</param>
      /// <param name="siteGuid">Глобальный идентификатор версии узла</param>
      /// <param name="dbParams">Параметры запроса</param>
      /// <returns></returns>
      PublishObjectsTable GetSiteUsers(Guid connectGuid, Guid siteGuid, DBQueryParams dbParams);

      /// <summary>Импортировать пользователей в локальную базу</summary>
      /// <param name="sessionGuid">Глобальный идентификатор пользовательской сессии</param>
      /// <param name="userIDs">Список пользователей</param>
      void ImportUsers(Guid sessionGuid, long[] userIDs);

      /// <summary>
      /// Получить время последнего изменения информации об узлах информационной системы
      /// </summary>
      /// <param name="connectGuid">Глобальный идентификатор соединения</param>
      /// <returns></returns>
      DateTime GetLastSitesInfoUpdate(Guid connectGuid);

      /// <summary>Получить информацию по узлам информационной системы</summary>
      /// <param name="connectGuid">Глобальный идентификатор соединения</param>
      /// <returns></returns>
      SiteInfo[] GetSitesInfo(Guid connectGuid);

      /// <summary>Изменить пароль пользователя портала</summary>
      /// <param name="sessionGuid">GUID сессии</param>
      /// <param name="login">Логин пользователя</param>
      /// <param name="newPassword">Новый пароль</param>
      void ChangeUserPassword(Guid sessionGuid, string login, string newPassword);

      /// <summary>Добавить пользователя портала</summary>
      /// <param name="sessionGuid">Глобальный идентификатор сессии</param>
      /// <param name="userName">Выводимое имя пользователя</param>
      /// <param name="login">Логин пользователя</param>
      /// <param name="password">Пароль</param>
      /// <param name="userGuid">Глобальный идентификатор версии объекта-пользователя</param>
      void AddUser(Guid sessionGuid, string userName, string login, string password, Guid userGuid);

      /// <summary>Изменить пароль пользователя портала</summary>
      /// <param name="sessionGuid">GUID сессии</param>
      /// <param name="login">Логин пользователя</param>
      /// <param name="newPassword">Новый пароль</param>
      void ChangeUserPassword(Guid sessionGuid, string login, PswPackage newPassword);

      /// <summary>Добавить пользователя портала</summary>
      /// <param name="sessionGuid">Глобальный идентификатор сессии</param>
      /// <param name="userName">Выводимое имя пользователя</param>
      /// <param name="login">Логин пользователя</param>
      /// <param name="password">Пароль</param>
      /// <param name="userGuid">Глобальный идентификатор версии объекта-пользователя</param>
      void AddUser(
        Guid sessionGuid,
        string userName,
        string login,
        PswPackage password,
        Guid userGuid);

      /// <summary>Удалить пользователя портала</summary>
      /// <param name="sessionGuid">Глобальный идентификатор сессии</param>
      /// <param name="login">Логин пользователя</param>
      void DeleteUser(Guid sessionGuid, string login);

      /// <summary>
      /// Удаление опубликованных объектов, возвращает массив с глобальными идентификаторами удаленных объектов
      /// </summary>
      /// <param name="sessionGuid">Глобальный идентификатор сессии</param>
      /// <param name="objectIDs">Список идентификаторов объектов для удаления</param>
      long[] DeleteObjects(Guid sessionGuid, long[] objectIDs);

      /// <summary>Получить опубликованные узлом шаблоны процессов</summary>
      /// <param name="siteGuid">Глобальный идентификатор узла</param>
      /// <returns></returns>
      ProcessTemplateInfo[] GetProcessTemplates(Guid siteGuid);

      /// <summary>Завершение владением</summary>
      /// <param name="sessionGuid">Глобальный идентификатор сессии</param>
      /// <param name="objectIDs">Список идентификаторов опубликованных объектов, владение которыми завершается</param>
      /// <param name="ownerSites">Строка с кодами узлов с правами владения на эти объекты</param>
      /// <param name="applic">Состав</param>
      /// <param name="withComposition">Вместе с составом</param>
      /// <param name="autoUpdate">Получать обновления об изменениях у этих объектов</param>
      string[] OwnComplete(
        Guid sessionGuid,
        long[] objectIDs,
        string ownerSites,
        CompositionApplicabilities applic,
        bool withComposition,
        bool autoUpdate);

      /// <summary>Завершение владением</summary>
      /// <param name="connectionGuid">Глобальный идентификатор соединения</param>
      /// <param name="objectGUIDs">Список идентификаторов опубликованных объектов, владение которыми завершается</param>
      /// <param name="ownerSites">Строка с кодами узлов с правами владения на эти объекты</param>
      string[] OwnComplete(Guid connectionGuid, string[] objectGUIDs, string ownerSites);

      /// <summary>
      /// Получить список опубликованных объектов указанного типа.
      /// В параметры-массивы пишем поиндексно значения из условий запроса ConditionStructure[]
      /// </summary>
      /// <param name="sessionGuid">Глобальный идентификатор соединения</param>
      /// <param name="objectType">Тип опубликованных объектов (на портале)</param>
      /// <param name="columns">Массив с идентификаторами (число, guid или имя) колонок, которые должны быть включены в выборку</param>
      /// <param name="recordCount">Количество возвращаемых строк</param>
      /// <param name="attributes">Ид. атрибута</param>
      /// <param name="relationalOperators">Операторы отношений</param>
      /// <param name="values">Искомое значение</param>
      /// <param name="values2">Искомое значение 2, нужно например для between</param>
      /// <param name="logicalOperators">Логический операторы, которыми это условие объединяется со следующим по списку условий</param>
      /// <param name="groupIDs">Управляет группировкой условий.
      /// (если GroupID больше 0, то перед условием открываются GroupID скобок,
      ///  если GroupID меньше 0, то за условием закрываются GroupID скобок)</param>
      /// <param name="caseSensitives">Указывает на чувствительность поиска к регистру букв</param>
      /// <returns></returns>
      string[][] SelectPublishObjectsFlt(
        Guid sessionGuid,
        int objectType,
        string[] columns,
        int recordCount,
        string[] attributes,
        int[] relationalOperators,
        string[] values,
        string[] values2,
        int[] logicalOperators,
        int[] groupIDs,
        bool[] caseSensitives);

      /// <summary>Создать пакет на текущую задачу публикации</summary>
      /// <param name="connectionGuid">Глобальный идентификатор соединения</param>
      /// <param name="taskID">Идентификатор задачи публикации</param>
      /// <param name="guid">Глобальный идентификатор пакета</param>
      /// <param name="name">Наименование пакета</param>
      /// <param name="designation">Обозначение пакета</param>
      /// <param name="note">Коментарии к пакету</param>
      /// <param name="enableSites">Разрешенные узлы</param>
      /// <returns>Идентификатор пакета (IDBObject.ObjectID)</returns>
      long CreatePacket(
        Guid connectionGuid,
        long taskID,
        Guid guid,
        string name,
        string designation,
        string note,
        string enableSites);

      /// <summary>Получить содержимое пакета</summary>
      /// <param name="connectionGuid">Глобальный идентификатор соединения</param>
      /// <param name="packetID">Идентификатор пакета (IDBObject.ObjectID)</param>
      /// <returns>Содержимое ввиде таблицы</returns>
      DataTable GetPacketContent(Guid connectionGuid, long packetID);

      /// <summary>Удалить пакеты</summary>
      /// <param name="connectionGuid">Глобальный идентификатор соединения</param>
      /// <param name="packetIDs">Идентификаторы пакетов (IDBObject.ObjectID)</param>
      void DeletePackets(Guid connectionGuid, long[] packetIDs);

      /// <summary>Получить все квитанции импорта для пакета</summary>
      /// <param name="connectionGuid">Глобальный идентификатор соединения</param>
      /// <param name="packetID">Идентификатор пакета (IDBObject.ObjectID)</param>
      PublicationReceipt[] GetImportReceipts(Guid connectionGuid, long packetID);

      /// <summary>Получить содержимое квитанции</summary>
      /// <param name="connectionGuid">Глобальный идентификатор соединения</param>
      /// <param name="receiptID">Идентификатор квитанции (IDBObject.ObjectID)</param>
      /// <returns>Квитанция ввиде таблицы</returns>
      DataTable GetReceiptContent(Guid connectionGuid, long receiptID);

      /// <summary>Флаг офлайн режима</summary>
      bool IsOffline { get; }

      /// <summary>Список файлов для импорта</summary>
      string[] OfflineImportFilesList { get; }
    }
}
