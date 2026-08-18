
// Type: Intermech.Client.Core.HotKeysCommand
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using System.Diagnostics;
using System.Windows.Forms;


namespace Intermech.Client.Core;

/// <summary>
/// Класс, позволяющий определить, какая команда назначена комбинации "горячих клавиш"
/// </summary>
[DebuggerDisplay("Shortcut: [{_shortcut}]; command: \"{_command}\"; handler: {_handler}")]
public class HotKeysCommand : IHotKeysCommand
{
  /// <summary>
  /// Комбинация "горячих клавиш", которой назначена определённая команда
  /// </summary>
  private Keys _shortcut;
  /// <summary>
  /// Название команды (уникальное в пределах всей системы строковое значение)
  /// </summary>
  private string _command = string.Empty;
  /// <summary>
  /// Обработчик указанной команды - контекстное меню или менеджер команд
  /// </summary>
  private DefaultCommandHandler _handler;

  /// <summary>Базовый конструктор</summary>
  public HotKeysCommand()
  {
  }

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="shortcut">Комбинация "горячих клавиш", которой назначена определённая команда</param>
  /// <param name="command">Название команды (уникальное в пределах всей системы строковое значение)</param>
  /// <param name="handler">Обработчик указанной команды - контекстное меню или менеджер команд</param>
  public HotKeysCommand(Keys shortcut, string command, DefaultCommandHandler handler)
  {
    this._shortcut = shortcut;
    this._command = command;
    this._handler = handler;
  }

  /// <summary>
  /// Комбинация "горячих клавиш", которой назначена определённая команда
  /// </summary>
  public Keys Shortcut => this._shortcut;

  /// <summary>
  /// Название команды (уникальное в пределах всей системы строковое значение)
  /// </summary>
  public string Command => this._command;

  /// <summary>
  /// Обработчик указанной команды - контекстное меню или менеджер команд
  /// </summary>
  public DefaultCommandHandler Handler => this._handler;

  /// <summary>Сравнить экземпляр класса с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>true, если объекты равны</returns>
  public override bool Equals(object obj)
  {
    if (!(obj is HotKeysCommand hotKeysCommand))
      return base.Equals(obj);
    return this._command == hotKeysCommand._command && this._handler == hotKeysCommand._handler && this._shortcut.Equals((object) hotKeysCommand._shortcut);
  }

  /// <summary>Вернуть 32-битный хэш-код экземпляра класса</summary>
  /// <returns>32-битный хэш-код экземпляра класса</returns>
  public override int GetHashCode()
  {
    return this._handler.GetHashCode() << 31 /*0x1F*/ ^ this._shortcut.GetHashCode() << 24 ^ this._command.GetHashCode();
  }
}
