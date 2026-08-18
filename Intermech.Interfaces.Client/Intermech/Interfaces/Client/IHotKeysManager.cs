// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IHotKeysManager
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Collections.Generic;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Интерфейс, позволяющий назначать комбинациям "горячих клавиш" определённые команды
/// </summary>
public interface IHotKeysManager
{
  /// <summary>Отыскать по указанному имени описание команды</summary>
  /// <param name="command">Уникальное в пределах системы название команды</param>
  /// <returns>Описание команды</returns>
  IHotKeysCommand this[string command] { get; }

  /// <summary>
  /// Отыскать по указанной комбинации "горячих клавиш" описания команд
  /// </summary>
  /// <param name="shortcut">Комбинация "горячих клавиш"</param>
  /// <returns>Описания команд</returns>
  List<IHotKeysCommand> this[Keys shortcut] { get; }

  /// <summary>
  /// Зарегистрировать (или перекрыть существующую регистрацию) комбинацию "горячих клавиш" длв выполнения определённой команды
  /// </summary>
  /// <param name="shortcut">Комбинация "горячих клавиш", которой назначена определённая команда</param>
  /// <param name="command">Название команды (уникальное в пределах всей системы строковое значение)</param>
  /// <param name="handler">Обработчик указанной команды - контекстное меню или менеджер команд</param>
  /// <returns>Ссылка на интерфейс, связывающий "горячие клавиши" с определённой командой</returns>
  IHotKeysCommand RegisterHotKeysCommand(
    Keys shortcut,
    string command,
    DefaultCommandHandler handler);

  /// <summary>
  /// Удалить назначенные "горячие клавиши" указанной команде
  /// </summary>
  /// <param name="command">Уникальная строковая команда</param>
  void UnregisterCommand(string command);

  /// <summary>
  /// Удалить команду, назначенную указанным "горячим клавишам"
  /// </summary>
  /// <param name="shortcut">Комбинация "горячих клавиш"</param>
  void UnregisterHotKeys(Keys shortcut);

  /// <summary>Удалить все настройки горячих клавиш</summary>
  void UnregisterHotKeys();
}
