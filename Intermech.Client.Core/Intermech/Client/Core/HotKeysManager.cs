
// Type: Intermech.Client.Core.HotKeysManager
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;


namespace Intermech.Client.Core;

/// <summary>
/// Класс, позволяющий назначать комбинациям "горячих клавиш" определённые команды
/// </summary>
[DebuggerDisplay("HotKeysManager {hot keys: [{_hotKeys.Count}]")]
public class HotKeysManager : IHotKeysManager
{
  /// <summary>
  /// Коллекция зарегистрированных "горячих клавиш" и соотвествующих команд системы.
  /// Ключ - строковое название команды.
  /// </summary>
  private Dictionary<string, HotKeysCommand> _commands = new Dictionary<string, HotKeysCommand>(0);
  /// <summary>
  /// Коллекция зарегистрированных "горячих клавиш" и соотвествующих команд системы.
  /// Ключ - комбинация "горячих клавиш".
  /// </summary>
  private Dictionary<Keys, List<IHotKeysCommand>> _hotKeys = new Dictionary<Keys, List<IHotKeysCommand>>(0);

  /// <summary>Отыскать по указанному имени описание команды</summary>
  /// <param name="command">Уникальное в пределах системы название команды</param>
  /// <returns>Описание команды</returns>
  public IHotKeysCommand this[string command]
  {
    get
    {
      return !this._commands.ContainsKey(command) ? (IHotKeysCommand) null : (IHotKeysCommand) this._commands[command];
    }
  }

  /// <summary>
  /// Отыскать по указанной комбинации "горячих клавиш" описания команд
  /// </summary>
  /// <param name="shortcut">Комбинация "горячих клавиш"</param>
  /// <returns>Описания команд</returns>
  public List<IHotKeysCommand> this[Keys shortcut]
  {
    get
    {
      return !this._hotKeys.ContainsKey(shortcut) ? (List<IHotKeysCommand>) null : this._hotKeys[shortcut];
    }
  }

  /// <summary>
  /// Зарегистрировать (или перекрыть существующую регистрацию) комбинацию "горячих клавиш" длв выполнения определённой команды
  /// </summary>
  /// <param name="shortcut">Комбинация "горячих клавиш", которой назначена определённая команда</param>
  /// <param name="command">Название команды (уникальное в пределах всей системы строковое значение)</param>
  /// <param name="handler">Обработчик указанной команды - контекстное меню или менеджер команд</param>
  /// <returns>Ссылка на интерфейс, связывающий "горячие клавиши" с определённой командой</returns>
  public virtual IHotKeysCommand RegisterHotKeysCommand(
    Keys shortcut,
    string command,
    DefaultCommandHandler handler)
  {
    HotKeysCommand hotKeysCommand = new HotKeysCommand(shortcut, command, handler);
    this._commands.Add(hotKeysCommand.Command, hotKeysCommand);
    List<IHotKeysCommand> hotKeysCommandList = this._hotKeys.ContainsKey(shortcut) ? this._hotKeys[shortcut] : (List<IHotKeysCommand>) null;
    if (hotKeysCommandList == null)
    {
      hotKeysCommandList = new List<IHotKeysCommand>();
      this._hotKeys.Add(shortcut, hotKeysCommandList);
    }
    if (!hotKeysCommandList.Contains((IHotKeysCommand) hotKeysCommand))
      hotKeysCommandList.Add((IHotKeysCommand) hotKeysCommand);
    return (IHotKeysCommand) hotKeysCommand;
  }

  /// <summary>
  /// Удалить назначенные "горячие клавиши" указанной команде
  /// </summary>
  /// <param name="command">Уникальная строковая команда</param>
  public virtual void UnregisterCommand(string command)
  {
    if (!this._commands.ContainsKey(command))
      return;
    HotKeysCommand command1 = this._commands[command];
    if (this._hotKeys.ContainsKey(command1.Shortcut))
    {
      List<IHotKeysCommand> hotKey = this._hotKeys[command1.Shortcut];
      if (hotKey.Contains((IHotKeysCommand) command1))
        hotKey.Remove((IHotKeysCommand) command1);
      if (hotKey.Count == 0)
        this._hotKeys.Remove(command1.Shortcut);
    }
    this._commands.Remove(command);
  }

  /// <summary>
  /// Удалить команду, назначенную указанным "горячим клавишам"
  /// </summary>
  /// <param name="shortcut">Комбинация "горячих клавиш"</param>
  public virtual void UnregisterHotKeys(Keys shortcut)
  {
    if (!this._hotKeys.ContainsKey(shortcut))
      return;
    List<IHotKeysCommand> hotKey = this._hotKeys[shortcut];
    for (int index = 0; index < hotKey.Count; ++index)
    {
      IHotKeysCommand hotKeysCommand = hotKey[index];
      if (this._commands.ContainsKey(hotKeysCommand.Command))
        this._commands.Remove(hotKeysCommand.Command);
    }
    this._hotKeys.Remove(shortcut);
  }

  /// <summary>Удалить все настройки горячих клавиш</summary>
  public virtual void UnregisterHotKeys()
  {
    this._hotKeys.Clear();
    this._commands.Clear();
  }
}
