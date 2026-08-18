// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Workflow.IAutoNotificationsService
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using Intermech.Interfaces.Workflow.AutoNotification;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Workflow;

/// <summary>Интерфейс службы автоматических уведомлений</summary>
public interface IAutoNotificationsService
{
  /// <summary>Формирует настройки объекта автоуведомления.</summary>
  /// <param name="objectId">ИД объекта автоуведомления.</param>
  /// <param name="sessionGuid">Guid сессии.</param>
  /// <returns>
  /// Настройки автоуведомления, либо null, если были проблемы с чтением
  /// </returns>
  AutoNotificationSettings FormSettingsFromObjectsBlobAttr(long objectId, Guid sessionGuid);

  /// <summary>Сохраняет настройки в блоб-атрибут объекта.</summary>
  /// <param name="settings">Настройки автоуведомления.</param>
  /// <param name="objectId">Ид объекта,в который сохраняются настройки</param>
  /// 
  ///             Нельзя использовать поле из сеттингс, потому что в настройках значение хранится по модулю, а при создании объекта ИД отрицателен
  ///             <param name="sessionGuid">Guid сессии.</param>
  void SaveSettingsToObjectsBlobAttr(
    AutoNotificationSettings settings,
    long objectId,
    Guid sessionGuid);

  /// <summary>Удаляет настройку из кэша.</summary>
  /// <param name="objectId">Идентификатор объекта настроек автоуведомления.</param>
  void DeleteSettingsFromCashe(long objectId);

  /// <summary>Получить исполнения изделия.</summary>
  /// <param name="initiatorId">The initiator identifier.</param>
  /// <returns>Список исполнений изделия</returns>
  List<long> GetArticles(long initiatorId);

  /// <summary>
  /// Получает дочерний объект связи.
  /// Либо список версий дочернего объекта для случая удаления связи, когда версия не известна.
  /// </summary>
  /// <param name="partId">Ид. дочернего объекта.</param>
  /// <param name="partObjectId">Ид. версии дочернего объекта (если не известна, то 0).</param>
  /// <returns>Дочерний объект связи</returns>
  List<long> GetRelationPartIds(long partId, long partObjectId);

  /// <summary>Получает состав объекта.</summary>
  /// <param name="initiatorId">Ид объекта-инициатора.</param>
  /// <param name="childTypesIDs">Возможные дочерние типы объектов</param>
  /// <param name="relTypesIDs">Типы связей</param>
  /// <param name="versionRuleID">Ид правила подбора версий</param>
  /// <returns>Состав объекта</returns>
  List<long> GetObjectComposition(
    long initiatorId,
    List<int> childTypesIDs,
    List<int> relTypesIDs,
    long versionRuleID);

  /// <summary>Получает применяемость объекта.</summary>
  /// <param name="initiatorId">Ид объекта-инициатора.</param>
  /// <param name="parentTypesIDs">Возможные родительские типы.</param>
  /// <param name="relTypesIDs">Связи, которыми входит.</param>
  /// <param name="versionRuleID">Ид правила подбора версий.</param>
  /// <returns>Применяемость объекта</returns>
  List<long> GetObjectApplicability(
    long initiatorId,
    List<int> parentTypesIDs,
    List<int> relTypesIDs,
    long versionRuleID);

  /// <summary>Получить набор объектов по схеме поиска.</summary>
  /// <param name="initiatorId">Ид объекта-инициатора.</param>
  /// <param name="searchSchemeId">Ид используемой схемы поиска</param>
  /// <returns>Набор объектов, полученных схемой поиска</returns>
  List<long> GetObjectsWithSearchScheme(long initiatorId, long searchSchemeId);

  /// <summary>Проверяет атрибуты на соответствие формуле.</summary>
  /// <param name="attrValues">Атрибуты объекта</param>
  /// <param name="formula">Строка формулы.</param>
  /// <returns>True, если значения атрибутов соответствуют формуле</returns>
  bool CheckAttrsWithFormula(AttributeValues[] attrValues, string formula);

  /// <summary>Получает список пользователей с указанными ролями.</summary>
  /// <param name="roles">Роли</param>
  /// <returns>Список пользователей с указанными ролями.</returns>
  List<long> GetUserIdsFromRoles(List<long> roles);

  /// <summary>Получает список пользователей с указанными группами.</summary>
  /// <param name="groups">Роли</param>
  /// <returns>Список пользователей с указанными группами.</returns>
  List<long> GetUserIdsFromGroups(List<long> groups);

  /// <summary>Получить список адресатов из атрибута.</summary>
  /// <param name="collectedObjects">Набор объектов.</param>
  /// <param name="attrId">Id атрибута</param>
  /// <returns>Список адресатов</returns>
  List<long> GetAdresseesFromAttribute(List<long> collectedObjects, int attrId);

  /// <summary>Получить автора связи.</summary>
  /// <param name="relationId">Ид связи.</param>
  List<long> GetRelationAuthor(long relationId);

  /// <summary>Получить авторов объектов.</summary>
  /// <param name="collectedObjects">Набор объектов.</param>
  /// <returns>Авторы объектов</returns>
  List<long> GetAuthors(List<long> collectedObjects);

  /// <summary>Получить владельцев объектов.</summary>
  /// <param name="collectedObjects">Набор объектов.</param>
  /// <returns>Владельцы объектов</returns>
  List<long> GetOwners(List<long> collectedObjects);

  /// <summary>Получить менеджеров проектов для объектов.</summary>
  /// <param name="collectedObjects">Набор объектов.</param>
  /// <returns>Менеджеры проектов</returns>
  List<long> GetProjectManagers(List<long> collectedObjects);

  /// <summary>
  /// Получить руководителей подразделений, которым принадлежат авторы объектов.
  /// </summary>
  /// <param name="collectedObjects">Набор объектов.</param>
  /// <returns>Руководители подразделений, которым принадлежат авторы объектов.</returns>
  List<long> GetAuthorsOrganizationUnitsChiefs(List<long> collectedObjects);

  /// <summary>
  /// Получить руководителей подразделений, которым принадлежат владельцы объектов.
  /// </summary>
  /// <param name="collectedObjects">Набор объектов.</param>
  /// <returns>Руководители подразделений, которым принадлежат владельцы объектов.</returns>
  List<long> GetOwnersDeparmentChiefs(List<long> collectedObjects);

  /// <summary>Разослать сообщение по конкретным адресам.</summary>
  /// <param name="emails">Список адресов</param>
  /// <param name="subject">Тема письма</param>
  /// <param name="message">Сообщение</param>
  /// <param name="session">Сессия</param>
  void SendToSpecificEmails(
    List<string> emails,
    string subject,
    string message,
    IUserSession session);

  /// <summary>Разослать уведомление на внешнюю почту пользователей.</summary>
  /// <param name="session">Сессия.</param>
  /// <param name="ToUserIDs">Пользователи</param>
  /// <param name="subject">Предмет сообщения.</param>
  /// <param name="message">Текст сообщения</param>
  /// <returns>Информация о пользователях, которым не удалось отправить сообщения</returns>
  List<MyElement> EmailProcessing(
    IUserSession session,
    long[] ToUserIDs,
    string subject,
    string message);

  /// <summary>
  /// Разослать уведомления на внутреннюю почту пользователей.
  /// </summary>
  /// <param name="session">Сессия.</param>
  /// <param name="users">Пользователи, которым надо отослать сообщения.</param>
  /// <param name="subject">Предмет сообщения.</param>
  /// <param name="message">Текст сообщения</param>
  void InternalMailProcessing(
    IUserSession session,
    List<long> users,
    string subject,
    string message);

  /// <summary>Запись сообщения в лог автоуведомлений.</summary>
  /// <param name="message">Текст сообщения</param>
  void AddMessageToLog(string message);
}
