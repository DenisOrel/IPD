
// Type: Intermech.Navigator.ContextMenu.Filter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using ImSSP;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Specialized;


namespace Intermech.Navigator.ContextMenu;

/// <summary>
/// Реализует простейший алгоритм фильтрации команд,
/// попадающих в таблицу при ее построении. Разрешенные
/// и отфильтровываемые команды указываются в виде массив
/// имен команд.
/// </summary>
public class Filter : IFilter
{
  private FilterActionType _actionType;
  private IDictionary _commandNames;

  public Filter(FilterActionType actionType, params string[] commandNames)
  {
    if (commandNames == null)
      throw new ArgumentNullException(sc_3792.ssp_imclient_3793(), LocalizationHolder.rm.GetString("Client.Core_443"));
    this._actionType = actionType;
    this._commandNames = (IDictionary) new HybridDictionary();
    for (int index = 0; index < commandNames.Length; ++index)
    {
      Services.Check(commandNames[index]);
      this._commandNames.Add((object) commandNames[index], (object) null);
    }
  }

  /// <summary>
  /// Возвращает true, если команда с таким именем может быть
  /// помещена в таблицу команд.
  /// </summary>
  /// <param name="commandName">Имя команды</param>
  /// <returns>Признак прохождения условий фильтра.</returns>
  bool IFilter.PassCommand(string commandName)
  {
    bool flag = this._commandNames.Contains((object) commandName);
    if (this._actionType == FilterActionType.Suppress)
      flag = !flag;
    return flag;
  }
}
