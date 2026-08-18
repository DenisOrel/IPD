// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.ECO.IECOServer
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.ECO;

public interface IECOServer
{
  void SaveProps(
    Guid sessionGuid,
    bool AutoMoveObjects,
    bool WarnInstead,
    bool WriteComplect,
    string kiTemplate,
    bool DesOnReplace,
    bool leaveOTD,
    bool autoCO,
    int daysBefore,
    bool placeInvNum,
    string invNumAttr,
    bool hideHidden,
    bool alwaysOrigSize,
    bool createLiteraVersion,
    bool setLiteraFullSostav,
    bool moveAuthFiles,
    int maxDocsAllowed,
    bool replaceEmptyDesign,
    bool hideOnCreation,
    bool prohibitCustomReason,
    bool askOnNewOrganizations,
    bool checkObjectCreation,
    bool noSlashInDPI);

  void LoadProps(
    Guid sessionGuid,
    out bool AutoMove,
    out bool WarnInstead,
    out bool WriteComplect,
    out string kiTemplate,
    out bool DesOnReplace,
    out bool leaveOTD,
    out bool autoCO,
    out int daysBefore,
    out bool placeInvNum,
    out string invNumAttr,
    out bool HideHidden,
    out bool AutoOrigSize,
    out bool createLiteraVersion,
    out bool setLiteraFullSostav,
    out bool moveAuthFiles,
    out int maxDocsAllowed,
    out bool replaceEmptyDesign,
    out bool hideOnCreation,
    out bool prohibitCustomReason,
    out bool askOnNewOrganizations,
    out bool checkObjectCreation,
    out bool noSlashInDPI);

  void SetLitera(Guid sessionGuid, long objId, string Litera);

  long GetNewChangeNo(long Id, long objId);

  bool IsChangeNumUnique(long objId, string sNum);

  List<string> AssignChangeNumbers(List<IdLinkPair> objRevList);

  void ClearChangeNumbers(List<IdLinkPair> objRevList);

  /// <summary>Подписаться на включение объекта в извещение</summary>
  /// <param name="objType">Тип объекта, для которого надо вызывать Action</param>
  /// <param name="code">Функция, принимающая три параметра: ИД извещения, ИД связи и ИД вставляемого объекта</param>
  /// <returns>true, если функция была добавлена (то есть, этого типа еще не было)</returns>
  bool SubscribeToIncludeIntoECO(int objType, Action<IUserSession, long, long, long> code);

  /// <summary>Имеет ли объект ID или это только заготовка?</summary>
  /// <param name="sessionGuid">Guid сессии</param>
  /// <param name="objId">Ид версии объекта</param>
  /// <returns>true, если объект имеет ID</returns>
  bool ObjectHasID(Guid sessionGuid, long objId);

  bool SetStartDate(long objId, DateTime date);

  bool SetEndDate(long objId, DateTime date);

  void DeleteStartEndAttrs(List<long> objId);

  /// <summary>
  /// Удалить номера изменений со связи и объекта, на который она указывает
  /// </summary>
  /// <param name="sessionGuid">Guid сессии</param>
  /// <param name="relId">Ид связи</param>
  /// <param name="objId">Ид версии объекта или Consts.UnknownObjectId (в последнем случае берется конкретизация со связи)</param>
  void RemoveChangeNums(Guid sessionGuid, long relId, long objId);

  /// <summary>Связать набор извещений с контекстом редактирования</summary>
  /// <param name="sessionGuid">GUID сессии</param>
  /// <param name="revList">Набор ИД извещений</param>
  /// <param name="newLinkedContextNumber">ИД взаимосвязанного контекста</param>
  void LinkRevisionsToOther(
    Guid sessionGuid,
    IEnumerable<long> revList,
    long newLinkedContextNumber);

  /// <summary>Отвязать извещение от контекста</summary>
  /// <param name="sessionGuid">GUID сессии</param>
  /// <param name="revId">ИД извещения, которое надо отвязать</param>
  void UnlinkToOther(Guid sessionGuid, long revId);

  /// <summary>Выдать сообщение о привязке извещения к контексту</summary>
  /// <param name="sessionGuid">GUID сессии</param>
  /// <param name="revId">ИД извещения</param>
  /// <param name="newContext">ИД контекста, к которому привязываем</param>
  void RecordLinkMessage(Guid sessionGuid, long revId, long newContext);

  /// <summary>Начало создания связи "Изменяется по извещению"</summary>
  /// <param name="rootId">ИД объекта извещения или записи ЖИ</param>
  /// <param name="childId">ИД включаемого объекта</param>
  void StartLinkCreation(long rootId, long childId);

  /// <summary>Конец создания связи "Изменяется по извещению"</summary>
  /// <param name="rootId">ИД объекта извещения или записи ЖИ</param>
  /// <param name="childId">ИД включаемого объекта</param>
  void EndLinkCreation(long rootId, long childId);

  /// <summary>Начало удаления связи</summary>
  /// <param name="relId">ИД связи</param>
  void StartLinkDeletion(long relId);

  /// <summary>Конец удаления связи</summary>
  /// <param name="relId">ИД связи</param>
  void EndLinkDeletion(long relId);

  /// <summary>Удалить связь типа "Изменяется по извещению"</summary>
  /// <param name="rel">Связь, которую нужно удалить</param>
  void DoDeleteRelation(IDBRelation rel);

  /// <summary>
  /// Сохраняет параметры работы с листом рассылки на сервере
  /// </summary>
  /// <param name="sessionGuid">ГУИД сессии</param>
  void SaveDeliveryListParams(Guid sessionGuid);

  /// <summary>
  /// Получает режим копирования листа рассылки извещения в документы
  /// </summary>
  /// <returns>true - если надо копировать ЛР извещения в документы</returns>
  bool GetDeliveryListParam();

  /// <summary>
  /// Получает словарь с информацией об ИД документов, входящих в извещение связью "Изменяется по извещению"
  /// Dict(ObjectID,ID)
  /// </summary>
  /// <param name="ecoObjectID">ObjectID извещения</param>
  /// <param name="sessionGuid">Guid сессии</param>
  /// <returns>Dict(ObjectID,ID) документов, входящих в извещение связью "Изменяется по извещению"</returns>
  Dictionary<long, long> GetDocsIDsInfoFromECOComposition(long ecoObjectID, Guid sessionGuid);

  /// <summary>Вернуть список удаленных объектов</summary>
  /// <param name="sessionGuid">Guid сессии, для которой надо получить список удаленных объектов</param>
  /// <returns>null, если еще не готово, и набор объектов (возможно, пустой), если уже удалено</returns>
  HashSet<long> GetDeletedObjects(Guid sessionGuid);

  /// <summary>Создать связь между извещением и объектом</summary>
  /// <param name="revId">ИД извещения</param>
  /// <param name="objVerId">ИД объекта</param>
  /// <param name="delOnExclude">Удалять при исключении из извещения?</param>
  /// <param name="futureStepId">ИД шага ЖЦ, на который объект надо перевести при актуализации</param>
  /// <param name="changeNum">Номер изменения</param>
  /// <param name="goal">Цель включения</param>
  /// <param name="hType">Возможность скрытия</param>
  /// <param name="auxObjects">Список дополнительных объектов</param>
  /// <returns>Созданная связь</returns>
  IDBRelation CreateRevLink(
    IUserSession session,
    long revId,
    long objVerId,
    bool delOnExclude = true,
    long futureStepId = 0,
    string changeNum = "",
    ECOGoal goal = ECOGoal.Change,
    HidingType hType = HidingType.CanBeHidden,
    IEnumerable<long> auxObjects = null);

  /// <summary>
  /// Очистить у объекта атрибуты Инвентарный номер (ОТД), Дата (ОТД) и Рег (ОТД)
  /// </summary>
  /// <param name="objId">ИД объекта</param>
  void ClearOTDAttrs(long objId);

  /// <summary>Начало запрета добавления контекста</summary>
  /// <param name="ecoObjId"></param>
  void StartDisableAddContext(long ecoObjId);

  /// <summary>Конец запрета добавления контекста</summary>
  /// <param name="ecoObjId"></param>
  void StopDisableAddContext(long ecoObjId);
}
