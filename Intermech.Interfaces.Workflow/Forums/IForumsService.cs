// Decompiled with JetBrains decompiler
// Type: Intermech.Forums.IForumsService
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using Intermech.Interfaces;
using System;

#nullable disable
namespace Intermech.Forums;

/// <summary>поиск обсуждений для формирования форума</summary>
public interface IForumsService
{
  /// <summary>
  /// Формируем форум для указанного объекта в соответствии со
  ///  способом сбора обсуждений, указанным пользователем
  /// </summary>
  /// <param name="objectID">id версии объекта</param>
  /// <param name="id">id объекта</param>
  /// <param name="format">способ сбора обсуждений, выбранный пользователем </param>
  /// <param name="filtrationOwnerID">фильтрация</param>
  /// <param name="portalSearch">искать сообщения среди опубликованных обсуждений?</param>
  /// <param name="sessionID">сессия</param>
  /// <returns>Список версий объектов-обсуждений.</returns>
  Forum GenerationForum(
    long objectID,
    long id,
    ForumFormat format,
    string filtrationOwnerID,
    Guid sessionID);

  /// <summary>Добавить сообщение пользователя в обсуждение</summary>
  /// <param name="message">Сообщение</param>
  /// <param name="objectID">id версии обсуждаемого объекта</param>
  /// <param name="id">id обсуждаемого объекта</param>
  /// <param name="forum"></param>
  /// <param name="sessionID">сессия</param>
  void AddMessageToDiscussion(
    UserMessage message,
    long objectID,
    long id,
    ref Forum forum,
    Guid sessionID);

  /// <summary>Удалить сообщение из обсуждения</summary>
  /// <param name="forum"></param>
  /// <param name="message">удаляемое сообщние</param>
  /// <param name="sessionID">сессия</param>
  void DeleteMessage(ref Forum forum, UserMessage message, Guid sessionID);

  /// <summary>Изменить сообщение обсуждения</summary>
  /// <param name="forum"></param>
  /// <param name="discGuid"></param>
  /// <param name="sessionID">сессия</param>
  void ChangeMessage(Forum forum, Guid discGuid, Guid sessionID, bool sendMessage);

  /// <summary>
  /// Создать и вернуть объект-обcуждение
  /// для указанной версии объекта
  /// </summary>
  /// <param name="objectID"></param>
  /// <param name="id"></param>
  /// <param name="sessionID"></param>
  /// <returns></returns>
  IDBObject CreateDiscussion(long objectID, object sessionID);
}
