
// Type: Intermech.Client.Core.FormDesigner.Actions.FormDesignerEventsManager
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;


namespace Intermech.Client.Core.FormDesigner.Actions;

/// <summary>
/// 
/// </summary>
internal class FormDesignerEventsManager : 
  IFormDesignerEventsManager,
  IEnumerable<FormDesignerAction>,
  IEnumerable
{
  /// <summary>
  /// События разделяются по типам (например события формы, события кнопки и т.д.)
  /// </summary>
  private Dictionary<Type, Dictionary<Guid, FormDesignerAction>> _typesHash = new Dictionary<Type, Dictionary<Guid, FormDesignerAction>>();
  /// <summary>Hash, в котором хранится информация для событий</summary>
  private Dictionary<Guid, FormDesignerAction> _eventsInfo = new Dictionary<Guid, FormDesignerAction>();
  /// <summary>Hash, в котором хранятсы handler'ы событий</summary>
  private Dictionary<Guid, IFormDesignerEventHandlerBase> _handlersHash = new Dictionary<Guid, IFormDesignerEventHandlerBase>();

  /// <summary>Получить обработчик.</summary>
  /// <param name="eventGuid">Глобальный идентификатор события</param>
  /// <returns>Обработчик события (если такое есть), либо null - если нет</returns>
  public IFormDesignerEventHandlerBase GetEvent(Guid eventGuid)
  {
    return !this._handlersHash.ContainsKey(eventGuid) ? (IFormDesignerEventHandlerBase) null : this._handlersHash[eventGuid];
  }

  /// <summary>Список событий по его типу.</summary>
  /// <param name="eventType">Тип события</param>
  /// <returns>Список событий на для конкретного типа</returns>
  public Dictionary<Guid, FormDesignerAction> GetEvents(Type eventType)
  {
    return !this._typesHash.ContainsKey(eventType) ? (Dictionary<Guid, FormDesignerAction>) null : this._typesHash[eventType];
  }

  /// <summary>Получить описание события.</summary>
  /// <param name="eventGuid">Глобальный идентификатор события</param>
  /// <returns>Описание события</returns>
  public FormDesignerAction GetInfo(Guid eventGuid)
  {
    return !this._eventsInfo.ContainsKey(eventGuid) ? (FormDesignerAction) null : this._eventsInfo[eventGuid];
  }

  /// <summary>Регистрация события.</summary>
  /// <param name="eventType">Тип события</param>
  /// <param name="eventGuid">Глобальный идентификатор события</param>
  /// <param name="eventName">Наименование события</param>
  /// <param name="eventHandler">Обработчик события</param>
  public void RegisterEvent(
    Type eventType,
    Guid eventGuid,
    string eventName,
    IFormDesignerEventHandlerBase eventHandler)
  {
    if (!(eventType != (Type) null) || !(eventGuid != Guid.Empty) || eventHandler == null)
      return;
    this.RegisterEvent(eventType, new FormDesignerAction(eventGuid, eventName), eventHandler);
  }

  /// <summary>Регистрация события.</summary>
  /// <param name="eventType">Тип события</param>
  /// <param name="action">Действие</param>
  /// <param name="eventHandler">Обработчик события</param>
  public void RegisterEvent(
    Type eventType,
    FormDesignerAction action,
    IFormDesignerEventHandlerBase eventHandler)
  {
    if (!(eventType != (Type) null) || action == null || action == FormDesignerAction.Empty || eventHandler == null)
      return;
    Guid actionGuid = action.ActionGuid;
    if (this._typesHash.ContainsKey(eventType))
    {
      if (!this._typesHash[eventType].ContainsKey(actionGuid))
        this._typesHash[eventType].Add(actionGuid, action);
    }
    else
      this._typesHash[eventType] = new Dictionary<Guid, FormDesignerAction>(1)
      {
        {
          actionGuid,
          action
        }
      };
    if (this._eventsInfo.ContainsKey(actionGuid) || this._handlersHash.ContainsKey(actionGuid))
      return;
    this._eventsInfo.Add(actionGuid, action);
    this._handlersHash.Add(actionGuid, eventHandler);
  }

  /// <summary>Разрегистрация события.</summary>
  /// <param name="eventType">Тип события</param>
  /// <param name="eventGuid">Глобальный идентификатор события</param>
  public void UnregisterEvent(Type eventType, Guid eventGuid)
  {
    this._eventsInfo.Remove(eventGuid);
    this._handlersHash.Remove(eventGuid);
    if (!this._typesHash.ContainsKey(eventType))
      return;
    this._typesHash[eventType].Remove(eventGuid);
    if (this._typesHash[eventType].Count != 0)
      return;
    this._typesHash.Remove(eventType);
  }

  /// <summary>
  /// 
  /// </summary>
  public event FormDesignerEventHandler DataLoadCompleted;

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public IEnumerator<FormDesignerAction> GetEnumerator()
  {
    return (IEnumerator<FormDesignerAction>) this._eventsInfo.Values.GetEnumerator();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="seneder"></param>
  /// <param name="items"></param>
  public void DataLoaded(object seneder, EventArgs args)
  {
    if (this.DataLoadCompleted == null)
      return;
    this.DataLoadCompleted(seneder, args);
  }
}
