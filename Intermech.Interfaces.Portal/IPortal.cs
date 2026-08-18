// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Portal.IPortal
// Assembly: Intermech.Interfaces.Portal, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: F7558C4C-BFAF-4679-9F10-E5048F615D8F
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Portal.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Portal.xml

using Intermech.Interfaces.WebPortal;
using Intermech.Protection;
using System;

#nullable disable
namespace Intermech.Interfaces.Portal;

/// <summary>Портал</summary>
public interface IPortal
{
  /// <summary>Версия портального модуля</summary>
  string Version { get; }

  /// <summary>Изменить пароль пользователя портала</summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии</param>
  /// <param name="login">Логин пользователя</param>
  /// <param name="newPassword">Новый пароль</param>
  void ChangeUserPassword(Guid sessionGuid, string login, string newPassword);

  /// <summary>Изменить пароль пользователя портала</summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии</param>
  /// <param name="login">Логин пользователя</param>
  /// <param name="newPassword">Новый пароль</param>
  void ChangeUserPassword(Guid sessionGuid, string login, PswPackage newPassword);

  /// <summary>Добавить пользователя портала</summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии</param>
  /// <param name="userName">Полное имя пользователя</param>
  /// <param name="login">Логин пользователя</param>
  /// <param name="password">Пароль</param>
  /// <param name="userGuid">Глобальный идентификатор версии объекта пользователя</param>
  void AddUser(Guid sessionGuid, string userName, string login, string password, Guid userGuid);

  /// <summary>Добавить пользователя портала</summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии</param>
  /// <param name="userName">Полное имя пользователя</param>
  /// <param name="login">Логин пользователя</param>
  /// <param name="password">Пароль</param>
  /// <param name="userGuid">Глобальный идентификатор версии объекта пользователя</param>
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

  /// <summary>Получить список типов объектов, используемых порталом</summary>
  /// <returns></returns>
  PortalObjectType[] GetObjectTypesTree(Guid sessionGuid);

  /// <summary>Получить список типов объектов, используемых порталом</summary>
  /// <returns></returns>
  string[][] GetPublishObjectTypes(Guid sessionGuid);

  /// <summary>
  /// Получить список атрибутов для типа опубликованных объектов
  /// </summary>
  /// <param name="objectTypeID"></param>
  /// <returns></returns>
  string[][] GetAttributesForPublishObjectType(Guid sessionGuid, int objectTypeID);

  /// <summary>
  /// Получить время последнего изменения таблиц IMS_OBJECT_TYPES, IMS_ATTRIBUTES или IMS_OBJ_ATTR_TYPES в UTS
  /// </summary>
  /// <returns></returns>
  DateTime GetLasModifyMetadata(Guid sessionGuid);

  /// <summary>
  /// Получить список атрибутов для типа связей "Состав опубликованного объекта"
  /// </summary>
  /// <returns></returns>
  PortalAttributeType[] GetPublishRelationAttributes(Guid sessionGuid);

  /// <summary>
  /// Получить список атрибутов и допустимых значений для атрибутов используемых в типах объектов "Опубликованные объекты"
  /// и атрибутов для типа связей "Состав опубликованного объекта"
  /// </summary>
  /// <returns></returns>
  AttributePossibleValues[] GetAttributePossibleValues(Guid sessionGuid);

  /// <summary>Получить код узла по его глобальному идентификатору</summary>
  /// <param name="siteGuid">Глобальный идентификатор узла</param>
  /// <returns></returns>
  char GetSiteCode(Guid sessionGuid, string siteGuid);

  /// <summary>Получить информацию по узлам информационной системы</summary>
  /// <returns></returns>
  SiteInfo[] GetSitesInfo(Guid sessionGuid);

  /// <summary>
  /// Получить время последнего изменения информации об узлах информационной системы
  /// </summary>
  /// <returns></returns>
  DateTime GetLastSitesInfoUpdate();

  /// <summary>
  /// Начало публикации пачки объектов/связей (задачи)
  /// Значения с кодами узлов для прав просмотра и владения брать у родительского объекта, если же импортируемый объект никуда не входит
  /// присваивать код текущего узла
  /// </summary>
  /// <param name="connectionGuid">Глобальный идентификатор соединения</param>
  /// <param name="taskName">Название задачи</param>
  /// <returns>Идентификатор созданной задачи публикации</returns>
  long StartPublishingTask(Guid sessionGuid, string taskName);

  /// <summary>Начало публикации пачки объектов/связей (задачи)</summary>
  /// <param name="taskName">Название задачи</param>
  /// <param name="enabledSites">Строка с кодами узлов информационной системы, на которые может быть скопирован данный объект</param>
  /// <returns>Идентификатор созданной задачи публикации</returns>
  long StartPublishingTask(Guid sessionGuid, string taskName, string enabledSites);

  /// <summary>Начало публикации пачки объектов/связей (задачи)</summary>
  /// <param name="sessionGuid"></param>
  /// <param name="taskName">Название задачи</param>
  /// <param name="enabledSites">Строка с кодами узлов информационной системы, на которые может быть скопирован данный объект</param>
  /// <param name="packetID">Идентификатор пакета</param>
  /// <returns>Идентификатор созданной задачи публикации</returns>
  long StartPublishingTask(Guid sessionGuid, string taskName, string enabledSites, long packetID);

  /// <summary>Добавить в задачу публикации объект/связь</summary>
  /// <param name="taskID">Идентификатор задачи публикации</param>
  /// <param name="unit">Публикуемые объект/связь</param>
  void PublishUnit(Guid sessionGuid, long taskID, TransferedObject unit);

  /// <summary>
  /// Добавить в задачу публикации объекты и связи из опубликованного пакета
  /// </summary>
  /// <param name="taskID">Идентификатор задачи публикации</param>
  /// <param name="packetID">Идентификатор пакета</param>
  /// <param name="ownerCode">Код узла, которому отдается владение объектами из пакета</param>
  /// <returns>Количество добавленных объектов и связей</returns>
  string[][] UseGroup(Guid sessionGuid, long taskID, long packetID, string ownerCode);

  /// <summary>Удалить пакет</summary>
  /// <param name="packetID">Идентификатор пакета</param>
  /// <param name="withObjects">Удалить также объекты, входящие в его состав</param>
  void DeleteGroup(Guid sessionGuid, long packetID, bool withObjects);

  /// <summary>Создать группу публикации</summary>
  /// <param name="sessionGuid">Глобальный идентификатор текущей пользовательской сессии</param>
  /// <param name="taskID">Идентификатор задачи публикации</param>
  /// <returns>Идентификатор группы (IDBObject.ObjectID)</returns>
  long CreateGroup(Guid sessionGuid, long taskID);

  /// <summary>Добавить в задачу публикации объект</summary>
  /// <param name="sessionGuid">Глобальный идентификатор текущей пользовательской сессии</param>
  /// <param name="taskID">Идентификатор задачи публикации</param>
  /// <param name="unitGuid">Глобальный идентификатор для текущей части задачи</param>
  /// <param name="changesType">Тип изменения публикуемого (передаваемого/принимаемого) объекта</param>
  /// <param name="category">Категория передаваемого объекта</param>
  /// <param name="dataFiles">Список файлов с данными</param>
  /// <param name="inComposition">Флаг того, что объект публикуется в составе</param>
  /// <param name="withComposition">Флаг того, что объект публикуется с составом</param>
  /// <param name="creatorCode">Код узла, создавшего этот объект</param>
  /// <param name="ownerCode">Код узла, владеющего этим объектом</param>
  /// <param name="rootType">Корневой тип публикуемого объекта (документ/изделие/иное ...)</param>
  void PublishObject(
    Guid sessionGuid,
    long taskID,
    string unitGuid,
    int changesType,
    int category,
    string[] dataFiles,
    bool inComposition,
    bool withComposition,
    string creatorCode,
    string ownerCode,
    int rootType);

  /// <summary>Добавить в задачу публикации объект</summary>
  /// <param name="sessionGuid">Глобальный идентификатор текущей пользовательской сессии</param>
  /// <param name="taskID">Идентификатор задачи публикации</param>
  /// <param name="unitGuid">Глобальный идентификатор для текущей части задачи</param>
  /// <param name="changesType">Тип изменения публикуемого (передаваемого/принимаемого) объекта</param>
  /// <param name="category">Категория передаваемого объекта</param>
  /// <param name="dataFiles">Список файлов с данными</param>
  /// <param name="inComposition">Флаг того, что объект публикуется в составе</param>
  /// <param name="withComposition">Флаг того, что объект публикуется с составом</param>
  /// <param name="creatorCode">Код узла, создавшего этот объект</param>
  /// <param name="ownerCode">Код узла, владеющего этим объектом</param>
  /// <param name="compositionOwnerCode">Код узла, владеющего составом этого объектоа</param>
  /// <param name="rootType">Корневой тип публикуемого объекта (документ/изделие/иное ...)</param>
  void PublishObject(
    Guid sessionGuid,
    long taskID,
    string unitGuid,
    int changesType,
    int category,
    string[] dataFiles,
    bool inComposition,
    bool withComposition,
    string creatorCode,
    string ownerCode,
    string compositionOwnerCode,
    int rootType);

  /// <summary>Добавить в задачу публикации связь</summary>
  /// <param name="sessionGuid">Глобальный идентификатор текущей пользовательской сессии</param>
  /// <param name="taskID">Идентификатор задачи публикации</param>
  /// <param name="unitGuid">Глобальный идентификатор для текущей части задачи</param>
  /// <param name="changesType">Тип изменения публикуемого (передаваемого/принимаемого) объекта</param>
  /// <param name="category">Категория передаваемого объекта</param>
  /// <param name="dataFiles">Список файлов с данными</param>
  void PublishRelation(
    Guid sessionGuid,
    long taskID,
    string unitGuid,
    int changesType,
    int category,
    string[] dataFiles);

  /// <summary>Очистить состав опубликованного объекта</summary>
  /// <param name="sessionGuid">Глобальный идентификатор текущей пользовательской сессии</param>
  /// <param name="objectGuid">Глобальный идентификатор версии опубликованного объекта</param>
  /// <param name="relationTypes">Список наименований типов связей по которым удалять состав</param>
  void ClearComposition(Guid sessionGuid, string objectGuid, string[] relationTypes);

  /// <summary>
  /// Запись на портале файла для публикуемого объекта/связи
  /// </summary>
  /// <param name="unitGuid">Глобальный идентификатор TransferedObject, которому принадлежит файл</param>
  /// <param name="fileName">Имя файла</param>
  /// <param name="bytes">Байты с данными</param>
  /// <param name="continuation">Флаг того, что передача файла в режипе продолжения (первый раз false, потом true)</param>
  void TransferPublishUnitFile(
    Guid sessionGuid,
    string unitGuid,
    string fileName,
    byte[] bytes,
    bool continuation);

  /// <summary>
  /// Запись на портале файла для публикуемого объекта/связи
  /// </summary>
  /// <param name="sessionGuid">Глобальный идентификатор соединения</param>
  /// <param name="unitGuid">Глобальный идентификатор TransferedObject, которому принадлежит файл</param>
  /// <param name="fileName">Имя файла</param>
  /// <param name="bytes">Байты с данными</param>
  /// <param name="continuation">Флаг того, что передача файла в режипе продолжения (первый раз false, потом true)</param>
  void TransferPublishUnitFileEx(
    Guid sessionGuid,
    string unitGuid,
    string fileName,
    string bytes,
    bool continuation);

  /// <summary>Завершение сеанса публикации.</summary>
  /// <param name="sessionGuid">Глобальный идентификатор соединения</param>
  /// <param name="taskID">Идентификатор задачи публикации</param>
  /// <param name="deleteTask">Удалять задачу после завершения</param>
  void CompletePublish(Guid sessionGuid, long taskID, bool deleteTask);

  /// <summary>Удалить из списка задач завершенную задачу публикации</summary>
  /// <param name="sessionGuid">Глобальный идентификатор соединения</param>
  /// <param name="taskID">Идентификатор задачи публикации</param>
  void DeletePublishTask(Guid sessionGuid, long taskID);

  /// <summary>Удалить из списка задач завершенную задачу публикации</summary>
  /// <param name="sessionGuid">Глобальный идентификатор соединения</param>
  /// <param name="taskID">Идентификатор задачи публикации</param>
  /// <param name="deleteMode">Режим удаления:
  /// 1 - удаляет только задачу публикации (используется для успешно завершенной задачи)
  /// 0 - удаляет также временные файлы публикуемых объектов (используется при удалении незаконченной задачи, с ошибкой)
  /// </param>
  void DeletePublishTask(Guid sessionGuid, long taskID, int deleteMode);

  /// <summary>Получить статус задачи синхронизации</summary>
  /// <param name="sessionGuid">Глобальный идентификатор соединения</param>
  /// <param name="taskID">Идентификатор задачи публикации</param>
  /// <returns></returns>
  int GetTaskStatus(Guid sessionGuid, long taskID);

  /// <summary>Завершить владение.</summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии</param>
  /// <param name="objectIDs">Идентификаторы объектов, владение которыми завершается</param>
  /// <param name="recursive"></param>
  /// <param name="recursiveRelationTypes"></param>
  /// <param name="relationTypes"></param>
  /// <param name="ownerSites">Коды узлов с правами владения</param>
  /// <param name="skipNotOwned">Пропускать, если нельзя отдать права владения, иначе генериться ошибка</param>
  /// <param name="autoUpdate">Получать обновления об изменениях у этих объектов</param>
  string[] OwnCompleteEx(
    Guid sessionGuid,
    long[] objectIDs,
    string ownerSites,
    string[] relationTypes,
    string[] recursiveRelationTypes,
    bool recursive,
    bool skipNotOwned,
    bool autoUpdate);

  /// <summary>Завершить владение.</summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии</param>
  /// <param name="objectIDs">Идентификаторы объектов, владение которыми завершается</param>
  /// <param name="ownerSites">Коды узлов с правами владения</param>
  /// <param name="withComposition"></param>
  /// <param name="skipNotOwned">Пропускать, если нельзя отдать права владения, иначе генериться ошибка</param>
  /// <param name="autoUpdate">Получать обновления об изменениях у этих объектов</param>
  string[] OwnComplete(
    Guid sessionGuid,
    long[] objectIDs,
    string ownerSites,
    bool withComposition,
    bool skipNotOwned,
    bool autoUpdate);

  /// <summary>Завершить владение.</summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии</param>
  /// <param name="objectGuids">Глобальные идентификаторы объектов, владение которыми завершается</param>
  /// <param name="recursive"></param>
  /// <param name="recursiveRelationTypes"></param>
  /// <param name="relationTypes"></param>
  /// <param name="ownerSites">Коды узлов с правами владения</param>
  /// <param name="skipNotOwned">Пропускать, если нельзя отдать права владения, иначе генериться ошибка</param>
  /// <param name="autoUpdate">Получать обновления об изменениях у этих объектов</param>
  string[] OwnCompleteEx(
    Guid sessionGuid,
    string[] objectGuids,
    string ownerSites,
    string[] relationTypes,
    string[] recursiveRelationTypes,
    bool recursive,
    bool skipNotOwned,
    bool autoUpdate);

  /// <summary>Получить обновления</summary>
  /// <param name="applic">Доступные типы связей для формирования состава импортируемых объектов</param>
  /// <returns>Список глобальных идентификаторов обновлений</returns>
  string[] GetUpdates(Guid sessionGuid, CompositionApplicabilities applic);

  /// <summary>Проверить обновление по конкретному объекту</summary>
  /// <returns>Идентификатор версии опубликованного объекта или -1</returns>
  long CheckUpdate(Guid sessionGuid, Guid objectGuid);

  /// <summary>Получить обновления</summary>
  /// <param name="relationTypes">Список типов связей для формирования импортируемого состава</param>
  /// <returns></returns>
  string[] GetUpdatesEx(Guid sessionGuid, string[] relationTypes);

  /// <summary>Начало получения изменения</summary>
  /// <param name="updateGUID">Глобальный идентификатор обновления</param>
  TransferedObject[] GetUpdateUnit(Guid sessionGuid, string updateGUID);

  /// <summary>Начало получения изменения</summary>
  /// <param name="updateGUID">Глобальный идентификатор обновления</param>
  string[][] GetUpdateUnitEx(Guid sessionGuid, string updateGuid);

  /// <summary>
  /// Установить флаг статуса для изменения "В работе", что означает, что данные по
  /// функции StartUpdateUnit приняты успешно и клиент начал обработку данных
  /// </summary>
  /// <param name="updateGUID">Глобальный идентификатор обновления</param>
  void StartUpdateUnit(Guid sessionGuid, string updateGUID);

  /// <summary>
  /// Получить очередную порцию байт файла с атрибутами обновления
  /// </summary>
  /// <param name="transferedGuid">Глобальный идентификатор экземпляра TransferedObject</param>
  /// <param name="fileName">Имя файла в массиве файлов изменения</param>
  /// <param name="startPosition">Стартовая позиция в потоке с которой начинать чтение</param>
  /// <returns></returns>
  byte[] GetUpdateAttributesFile(
    Guid sessionGuid,
    Guid transferedGuid,
    string fileName,
    long startPosition);

  /// <summary>
  /// Получить очередную порцию байт файла с атрибутами объекта/связи изменения
  /// </summary>
  /// <param name="transferedGuid">Глобальный идентификатор экземпляра TransferedObject</param>
  /// <param name="fileName">Имя файла в массиве файлов изменения</param>
  /// <param name="startPosition">Стартовая позиция в потоке с которой начинать чтение</param>
  /// <returns></returns>
  string GetUpdateAttributesFileEx(
    Guid sessionGuid,
    Guid transferedGuid,
    string fileName,
    long startPosition);

  /// <summary>Установить статус обновлению.</summary>
  /// <param name="sessionGuid">Глобальный идентификатор соединения</param>
  /// <param name="updateGuid">Глобальный идентификатор обновления</param>
  /// <param name="statusID">Статус</param>
  void SetUpdateUnitStatus(Guid sessionGuid, string updateGuid, int statusID);

  /// <summary>
  /// Установить обновлению статус Ошибка и записать ему текст ошибки.
  /// </summary>
  /// <param name="sessionGuid">Глобальный идентификатор соединения</param>
  /// <param name="updateGuid">Глобальный идентификатор обновления</param>
  /// <param name="errorText">Текст ошибки</param>
  void SetUpdateUnitError(Guid sessionGuid, string updateGuid, string errorText);

  /// <summary>Получить размер файла</summary>
  /// <param name="transferedGuid">Глобальный идентификатор экземпляра TransferedObject</param>
  /// <param name="fileName">Имя файла в массиве файлов изменения</param>
  /// <returns></returns>
  long GetUpdateAttributesFileLength(Guid sessionGuid, Guid transferedGuid, string fileName);

  /// <summary>Окончание получения обновления</summary>
  /// <param name="updateGUID">Глобальный идентификатор обновления</param>
  void EndUpdateUnit(Guid sessionGuid, string updateGUID);

  /// <summary>Окончание получения изменения со взятием во владение</summary>
  /// <param name="updateGUID">Глобальный идентификатор обновления</param>
  void EndUpdateUnit(Guid sessionGuid, string updateGUID, string[] guids);

  /// <summary>
  /// Получить список объектов состава импортируемого объекта, включая связанные объекты.
  /// </summary>
  /// <param name="sessionGuid"></param>
  /// <param name="objectID"></param>
  /// <param name="filteredTypes"></param>
  /// <param name="countLevels"></param>
  /// <returns></returns>
  long[] GetImportComposition(
    Guid sessionGuid,
    long[] objectIDs,
    string[] filteredTypes,
    int countLevels);

  /// <summary>
  /// Получить список опубликованных объектов указанного типа
  /// </summary>
  /// <param name="objectType">Тип опубликованных объектов</param>
  /// <param name="dbParams">Параметры запроса</param>
  /// <returns></returns>
  PublishObjectsTable SelectPublishObjects(
    Guid sessionGuid,
    int objectType,
    DBQueryParams dbParams);

  /// <summary>
  /// Получить список опубликованных объектов указанного типа
  /// </summary>
  /// <param name="connectGuid">Глобальный идентификатор соединения</param>
  /// <param name="objectType">Тип опубликованных объектов (на портале)</param>
  /// <param name="columns">Идентификаторы колонок, которые необходимо получить</param>
  /// <param name="recordCount">Количество максимально возвращаемых строк</param>
  /// <returns></returns>
  string[][] SelectPublishObjectsEx(
    Guid sessionGuid,
    int objectType,
    string[] columns,
    int recordCount);

  /// <summary>
  /// Получить список опубликованных объектов указанного типа по условиям
  /// </summary>
  /// <param name="connectGuid">Глобальный идентификатор соединения</param>
  /// <param name="objectType">Тип опубликованных объектов (на портале)</param>
  /// <param name="columns">Идентификаторы колонок, которые необходимо получить</param>
  /// <param name="recordCount">Количество максимально возвращаемых строк</param>
  /// <param name="attributes">Идентификаторы атрибутов</param>
  /// <param name="relationalOperators">Операторы отношений</param>
  /// <param name="values">Искомые значения</param>
  /// <param name="values2">Искомые значения. Нужно например для between</param>
  /// <param name="logicalOperators">логические операторы, которыми каждое из условий объединяется со следующим по списку условием</param>
  /// <param name="groupIDs">Управляет группировкой условий. (если GroupID больше 0, то перед условием открываются GroupID скобок, если GroupID меньше 0, то за условием закрываются GroupID скобок)</param>
  /// <param name="caseSensitives">Указывает на чувствительность поиска к регистру букв текущего условия</param>
  /// <returns></returns>
  string[][] SelectPublishObjectsEx(
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

  /// <summary>Импорт пакетов</summary>
  /// <param name="sessionGuid">Идентификатор пользовательской сессии</param>
  /// <param name="updateGuid">Глобальный идентификатор создаваемого обновления</param>
  /// <param name="packetIDs">Идентификаторы импортируемых пакетов</param>
  void ImportPackets(Guid sessionGuid, Guid updateGuid, long[] packetIDs);

  /// <summary>Импорт пакета завершен</summary>
  /// <param name="sessionGuid">Идентификатор пользовательской сессии</param>
  /// <param name="packetID">Идентификатор пакета</param>
  void PacketImportComplete(Guid sessionGuid, long packetID);

  /// <summary>Завершить получение автообновлений</summary>
  /// <param name="sessionGuid">Идентификатор пользовательской сессии</param>
  /// <param name="objectsIDs">Идентификаторы опубликованных объектов</param>
  /// <param name="withComposition">Завершить получение автообновлений также и у состава объектов</param>
  /// <returns></returns>
  string[] AutoImportComplete(Guid sessionGuid, long[] objectIDs, bool withComposition);

  /// <summary>Получить информацию о задаче формировании импорта</summary>
  /// <param name="updateGuid">Глобальный идентификатор создаваемого обновления</param>
  /// <returns></returns>
  ImportInfo GetImportInfo(Guid updateGuid);

  /// <summary>Имортировать опубликованные объекты с портала</summary>
  /// <param name="sessionGuid">Идентификатор пользовательской сессии</param>
  /// <param name="updateGuid">Глобальный идентификатор создаваемого обновления</param>
  /// <param name="objectsIDs">Идентификаторы опубликованных объектов</param>
  /// <param name="filteredTypes">Типы объектов, которые необходимо отфильтровывать</param>
  /// <param name="ownBegin">Получить права владения</param>
  /// <param name="autoUpdate">Автоматически получать изменения в импортируемых объектах</param>
  /// <param name="countLevels">Количество уровней состава, -1 все</param>
  void ImportObjects(
    Guid sessionGuid,
    Guid updateGuid,
    long[] objectsIDs,
    string[] filteredTypes,
    bool ownBegin,
    bool autoUpdate,
    int countLevels);

  /// <summary>
  /// Запустить задачу формирования импорта опубликованных объектов
  /// </summary>
  /// <param name="sessionGuid">Идентификатор пользовательской сессии</param>
  /// <param name="updateGuid">Глобальный идентификатор создаваемого обновления</param>
  /// <param name="objectsIDs">Идентификаторы опубликованных объектов</param>
  /// <param name="filteredTypes">Типы объектов, которые необходимо отфильтровывать</param>
  /// <param name="ownBegin">Получить права владения</param>
  /// <param name="autoUpdate">Автоматически получать изменения в импортируемых объектах</param>
  /// <param name="countLevels">Количество уровней состава, -1 все</param>
  void CreateImportTask(
    Guid sessionGuid,
    Guid updateGuid,
    long[] objIDs,
    string[] filteredTypes,
    bool ownBegin,
    bool autoUpdate,
    int countLevels);

  /// <summary>Имортировать опубликованные объекты с портала</summary>
  /// <param name="updateGuid">Глобальный идентификатор создаваемого обновления</param>
  /// <param name="objectsIDs">Идентификаторы опубликованных объектов</param>
  /// <param name="relationTypes">Список типов связей для формирования импортируемого состава. При запросе используется флаг recursive</param>
  /// <param name="recursiveRelationTypes">Список типов связей для формирования импортируемого состава, которые независимо от флага recursive запрашиваются рекурсивно</param>
  /// <param name="ownBegin">Получить права владения</param>
  /// <param name="autoUpdate">Автоматически получать изменения в импортируемых объектах</param>
  /// <param name="withVersions">Импорт всех версий выбранных опубликованных объектов</param>
  /// <param name="recursive">Рекурсивный поиск состава</param>
  void ImportObjectsEx(
    Guid sessionGuid,
    Guid updateGuid,
    long[] objectsIDs,
    string[] relationTypes,
    string[] recursiveRelationTypes,
    bool ownBegin,
    bool autoUpdate,
    bool withVersions,
    bool recursive);

  /// <summary>Получить значения атрибута для объекта</summary>
  /// <param name="objectID">Идентификатор опубликованного объекта</param>
  /// <param name="attrIDs">Массив идентификаторами атрибутов (м.б. глобальными идентификаторами, наименованиями либо для получения обязательных
  /// атрибутов объекта значения ObligatoryObjectAttributes, например "F_LC_STEP"), значения которых нужно получить</param>
  /// <returns></returns>
  PublishAttribute[] GetObjectAttributes(Guid sessionGuid, long objectID, params string[] attrIDs);

  /// <summary>
  /// Получить все значения атрибута для связи кроме блобов, включая обязательные атрибуты опубликованной связи
  /// </summary>
  /// <param name="objectID">Идентификатор опубликованной связи</param>
  /// <param name="attrIDs">Массив идентификаторами атрибутов (м.б. глобальными идентификаторами, наименованиями либо для получения обязательных
  /// атрибутов связи значения ObligatoryObjectAttributes, например "F_CREATE_DATE"), значения которых нужно получить</param>
  /// <returns></returns>
  PublishAttribute[] GetRelationAttributes(
    Guid sessionGuid,
    long relationID,
    params string[] attrIDs);

  /// <summary>Получить значения атрибута для объекта</summary>
  /// <param name="objectID">Идентификатор опубликованного объекта</param>
  /// <param name="attrIDs">Массив идентификаторами атрибутов (м.б. глобальными идентификаторами, наименованиями либо для получения обязательных
  /// атрибутов объекта значения ObligatoryObjectAttributes, например "F_LC_STEP"), значения которых нужно получить</param>
  /// <returns></returns>
  string GetObjectAttributesEx(Guid sessionGuid, long objectID, params string[] attrIDs);

  /// <summary>
  /// Получить все значения атрибута для связи кроме блобов, включая обязательные атрибуты опубликованной связи
  /// </summary>
  /// <param name="objectID">Идентификатор опубликованной связи</param>
  /// <param name="attrIDs">Массив идентификаторами атрибутов (м.б. глобальными идентификаторами, наименованиями либо для получения обязательных
  /// атрибутов связи значения ObligatoryObjectAttributes, например "F_CREATE_DATE"), значения которых нужно получить</param>
  /// <returns></returns>
  string GetRelationAttributesEx(Guid sessionGuid, long relationID, params string[] attrIDs);

  /// <summary>Получить состав опубликованного объекта</summary>
  /// <param name="sessionGuid"></param>
  /// <param name="objectID">Идентификатор версии опубликованного объекта, для которого необходимо получить состав</param>
  /// <param name="filteredTypes"></param>
  /// <param name="dbParams">Параметры запроса</param>
  /// <param name="countLevels"></param>
  /// <returns></returns>
  PublishObjectsTable SelectComposition(
    Guid sessionGuid,
    long objectID,
    DBQueryParams dbParams,
    int countLevels);

  /// <summary>Удаление опубликованных объектов</summary>
  /// <param name="sessionGuid"></param>
  /// <param name="objectIDs">Список идентификаторов объектов на удаление</param>
  void DeleteObjects(Guid sessionGuid, long[] objectIDs);

  /// <summary>Удаление опубликованных объектов</summary>
  /// <param name="sessionGuid"></param>
  /// <param name="objectIDs">Список идентификаторов объектов на удаление</param>
  string[] DeleteObjectsEx(Guid sessionGuid, long[] objectIDs);

  /// <summary>Получить опубликованные узлом шаблоны процессов</summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии</param>
  /// <param name="siteGuid">Глобальный идентификатор узла</param>
  /// <returns></returns>
  ProcessTemplateInfo[] GetProcessTemplates(Guid sessionGuid, Guid siteGuid);

  /// <summary>Получить список пользователей узла.</summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии администратора</param>
  /// <param name="siteGuid">Глобальный дентификатор версии узла</param>
  /// <param name="dbParams">Параметры запроса</param>
  /// <returns></returns>
  PublishObjectsTable GetSiteUsers(Guid sessionGuid, string siteGuid, DBQueryParams dbParams);

  /// <summary>Импорт пользователей</summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии администратора</param>
  /// <param name="updateGuid">Глобальный идентификатор создаваемого обновления</param>
  /// <param name="userIDs">Список идентификаторов импортируемых пользователей</param>
  void ImportUsers(Guid sessionGuid, Guid updateGuid, long[] userIDs);

  /// <summary>Является ли пользователь администратором на портале</summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии</param>
  /// <returns></returns>
  bool IsAdmin(Guid sessionGuid);

  /// <summary>Получить код узла, который инициировал обновление</summary>
  /// <param name="sessionGuid">Глобальный идентификатор соединения</param>
  /// <param name="updateGuid">Глобальный идентификатор обновления</param>
  /// <returns>Код узла или пустую строку</returns>
  string GetUpdateAuthor(Guid sessionGuid, string updateGuid);

  /// <summary>Вход пользователя в систему</summary>
  /// <param name="login">Имя пользователя</param>
  /// <param name="password">Пароль</param>
  /// <param name="siteGUID">Глобальный идентификатор узла</param>
  /// <param name="computerName">Имя компьютера</param>
  /// <param name="timeZone">Временная зона</param>
  /// <returns>Guid сессии</returns>
  string Login(string login, string password, string siteGUID, string computerName, int timeZone);

  /// <summary>Вход пользователя в систему</summary>
  /// <param name="login">Имя пользователя</param>
  /// <param name="password">Пароль</param>
  /// <param name="siteGUID">Глобальный идентификатор узла</param>
  /// <param name="computerName">Имя компьютера</param>
  /// <param name="timeZone">Временная зона</param>
  /// <returns>Guid сессии</returns>
  string Login(
    string login,
    PswPackage password,
    string siteGUID,
    string computerName,
    int timeZone);

  /// <summary>Выход пользователя из системы</summary>
  /// <param name="sessionGuid">Глобальный идентификатор соединения</param>
  void Logout(Guid sessionGuid);

  /// <summary>Получить сессию</summary>
  /// <param name="sessionGuid"></param>
  /// <returns></returns>
  IUserSession GetSession(Guid sessionGuid);

  /// <summary>Создать пакет на текущую задачу публикации</summary>
  /// <param name="sessionGuid">Глобальный идентификатор текущей пользовательской сессии</param>
  /// <param name="taskID">Идентификатор задачи публикации</param>
  /// <param name="guid">Глобальный идентификатор пакета</param>
  /// <param name="name">Наименование пакета</param>
  /// <param name="designation">Обозначение пакета</param>
  /// <param name="note">Коментарии к пакету</param>
  /// <param name="enableSites">Разрешенные узлы</param>
  /// <returns>Идентификатор пакета (IDBObject.ObjectID)</returns>
  long CreatePacket(
    Guid sessionGuid,
    long taskID,
    string guid,
    string name,
    string designation,
    string note,
    string enableSites);

  /// <summary>Получить содержимое пакета</summary>
  /// <param name="sessionGuid">Глобальный идентификатор текущей пользовательской сессии</param>
  /// <param name="packetID">Идентификатор пакета (IDBObject.ObjectID)</param>
  /// <returns>Запакованная DataTable</returns>
  byte[] GetPacketContent(Guid sessionGuid, long packetID);

  /// <summary>Удалить пакеты</summary>
  /// <param name="sessionGuid">Глобальный идентификатор текущей пользовательской сессии</param>
  /// <param name="packetIDs">Идентификаторы удаляемых пакетов</param>
  void DeletePackets(Guid sessionGuid, long[] packetIDs);

  /// <summary>
  /// Получить квитанции по пакету. Возвращается описательная часть квитанций, содержимое можно получить через GetReceiptContent
  /// </summary>
  /// <param name="sessionGuid">Глобальный идентификатор текущей пользовательской сессии</param>
  /// <param name="packetID">Идентификатор пакета (IDBObject.ObjectID)</param>
  /// <returns></returns>
  PublicationReceipt[] GetImportReceipts(Guid sessionGuid, long packetID);

  /// <summary>Получить содержимое квитанции</summary>
  /// <param name="sessionGuid">Глобальный идентификатор текущей пользовательской сессии</param>
  /// <param name="receiptID">Идентификатор квитанции (IDBObject.ObjectID)</param>
  /// <returns>Запакованная DataTable</returns>
  byte[] GetReceiptContent(Guid sessionGuid, long receiptID);
}
