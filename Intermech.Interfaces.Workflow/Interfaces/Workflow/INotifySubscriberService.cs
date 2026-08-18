// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Workflow.INotifySubscriberService
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using Intermech.Workflow;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Workflow;

public interface INotifySubscriberService
{
  /// <summary>Добавить уведомление на объект.</summary>
  /// <param name="sessionGuid">Гуид сессии.</param>
  /// <param name="notifiedObjectId">Идентификатор объекта для которого создается уведомление (не ид версии).</param>
  /// <param name="newNotify">Новое уведомление.</param>
  void AddNotificationForObject(Guid sessionGuid, long notifiedObjectId, Notify newNotify);

  /// <summary>Удаляет уведомление на объект.</summary>
  /// <param name="sessionGuid">Гуид сессии.</param>
  /// <param name="notifiedObjectId">Идентификатор объекта, для которого надо удалить уведомление(не ид версии).</param>
  /// <param name="deletingNotify">Уведомление, которое надо удалить.</param>
  void DeleteNotificationForObject(Guid sessionGuid, long notifiedObjectId, Notify deletingNotify);

  List<Notify> GetNotifications(
    Guid sessionGuid,
    long senderID,
    ref long notifyID,
    out string errorMessage);

  List<Notify> GetNotifications(Guid sessionGuid, long senderID, out string errorMessage);

  /// <summary>
  /// Формирует список общих уведомлений для группы объектов.
  /// </summary>
  /// <param name="sessionGuid">Сессия</param>
  /// <param name="notificationsForObjects">Таблица соответствий объектов и их уведомлений</param>
  /// <returns>
  /// Список общих уведомлений для отображения во вьюшке. Если где-то не подобрались какие-то данные - возвращает пустой список
  /// </returns>
  List<Notify> GetCommonNotifies(Guid sessionGuid, Dictionary<long, long> notificationsForObjects);

  /// <summary>
  /// Получает табличку соответствий объектов и их уведомлений.
  /// </summary>
  /// <param name="sessionGuid">Сессия</param>
  /// <param name="ids">Список ИД объектов</param>
  /// <returns>Таблица соответствий объектов и их уведомлений</returns>
  Dictionary<long, long> GetNotificationsForObjects(Guid sessionGuid, List<long> ids);

  /// <summary>
  /// Событие вызываемое маршрутизатором для получения вложенных документов Eco.
  /// </summary>
  event GetEcoDocumentsHandler GetEcoDocumentsListEvent;

  /// <summary>Метод вызываемый для обработки события</summary>
  /// <param name="attachmentsDoc">Документ ECO, со списком требуемых переводов на шаги жц/уровни продвижения и соответствующих типов объектов для которых нужны эти переводы</param>
  /// <returns>Список документов которым можно менять уровень продвижения/шаг ЖЦ маршрутизатором</returns>
  List<ResultEcoDocumentsInformation> GetResultEcos(EcoDocumentsInAttachments attachmentsDoc);

  /// <summary>
  /// Добавляем уведомления к одному объекту уведомлений и возвращаем его ИД
  /// Все переданные уведомления перезаписываются
  /// (Объект уведомлений может быть создан в этом методе с нуля)
  /// </summary>
  /// <param name="sessionGuid">Гуид сессии</param>
  /// <param name="Id">ИД объекта, на которое подписывается уведомление</param>
  /// <param name="notifyId"> ИД уведомления. Const.UnknownObjectId, если уведомление еще не создано в базе</param>
  /// <param name="notifies"> ИД уведомления</param>
  long AddNotify(Guid sessionGuid, long Id, long notifyId, List<Notify> notifies);

  /// <summary>
  /// Подписывает на уведомление несколько объектов. Возвращает список ошибок на клиент
  /// </summary>
  /// <param name="sessionGuid">Гуид сессии</param>
  /// <param name="ids">Идентификаторы объектов уведомлений</param>
  /// <param name="notificationsForObjects">Словарь соответствий объектов уведомлений и объектов, для которых создаются уведомления</param>
  /// <param name="notifies">Уведомления, которые надо подписать</param>
  List<string> AddNotifies(
    Guid sessionGuid,
    List<long> ids,
    Dictionary<long, long> notificationsForObjects,
    List<Notify> notifies);
}
