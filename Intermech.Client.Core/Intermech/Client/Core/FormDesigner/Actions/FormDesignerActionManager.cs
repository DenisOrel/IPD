
// Type: Intermech.Client.Core.FormDesigner.Actions.FormDesignerActionManager
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
internal class FormDesignerActionManager : 
  IFormDesignerActionManager,
  IEnumerable<FormDesignerAction>,
  IEnumerable
{
  private Dictionary<Guid, FormDesignerAction> _actionsInfo = new Dictionary<Guid, FormDesignerAction>();
  private Dictionary<Guid, IFormDesignerActionHandler> _actionsHash = new Dictionary<Guid, IFormDesignerActionHandler>();

  /// <summary>Регистрация события.</summary>
  /// <param name="actionGuid">Глобальный идентификатор события</param>
  /// <param name="actionName">Наименование события</param>
  /// <param name="handler"></param>
  public void RegisterAction(
    Guid actionGuid,
    string actionName,
    IFormDesignerActionHandler handler)
  {
    FormDesignerAction formDesignerAction = new FormDesignerAction(actionGuid, actionName);
    this._actionsInfo[actionGuid] = formDesignerAction;
    this._actionsHash[actionGuid] = handler;
  }

  /// <summary>Регистрация события.</summary>
  /// <param name="action">Событие</param>
  /// <param name="handler"></param>
  public void RegisterAction(FormDesignerAction action, IFormDesignerActionHandler handler)
  {
    this._actionsInfo[action.ActionGuid] = action;
    this._actionsHash[action.ActionGuid] = handler;
  }

  /// <summary>Выгрузить событие.</summary>
  /// <param name="actionGuid">Глобальный идентификатор события</param>
  public void UnregisterAction(Guid actionGuid)
  {
    this._actionsInfo.Remove(actionGuid);
    this._actionsHash.Remove(actionGuid);
  }

  /// <summary>Получить событие.</summary>
  /// <param name="actionGuid">Глобальный идентификатор события</param>
  /// <returns>Событие</returns>
  public FormDesignerAction GetInfo(Guid actionGuid)
  {
    return !this._actionsInfo.ContainsKey(actionGuid) ? (FormDesignerAction) null : this._actionsInfo[actionGuid];
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="actionInfo"></param>
  /// <returns></returns>
  public IFormDesignerActionHandler GetAction(object actionInfo)
  {
    IFormDesignerActionHandler action = (IFormDesignerActionHandler) null;
    switch (actionInfo)
    {
      case FormDesignerAction formDesignerAction:
        action = this._actionsHash.ContainsKey(formDesignerAction.ActionGuid) ? this._actionsHash[formDesignerAction.ActionGuid] : (IFormDesignerActionHandler) null;
        break;
      case Guid key:
        action = this._actionsHash.ContainsKey(key) ? this._actionsHash[key] : (IFormDesignerActionHandler) null;
        break;
    }
    return action;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public IEnumerator<FormDesignerAction> GetEnumerator()
  {
    return (IEnumerator<FormDesignerAction>) this._actionsInfo.Values.GetEnumerator();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();
}
