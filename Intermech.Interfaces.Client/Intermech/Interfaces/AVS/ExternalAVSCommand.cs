// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.AVS.ExternalAVSCommand
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Bars;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.AVS;

/// <summary>Встраиваемая в AVS внешняя команда</summary>
public class ExternalAVSCommand
{
  private readonly string commandName;
  private readonly string caption;
  private readonly string hint;
  private readonly EventHandler commandHandler;
  private MenuItemBase menuItem;

  /// <summary>Конструктор</summary>
  /// <param name="commandName">Имя команды. Идентифицирует команду в методах QueryStatus() и Execute() класса CommandManager</param>
  /// <param name="caption">Текст пункта меню</param>
  /// <param name="hint">Подсказка пункта меню</param>
  /// <param name="commandHandler">Делегат обработчика команды</param>
  public ExternalAVSCommand(
    string commandName,
    string caption,
    string hint,
    EventHandler commandHandler)
  {
    this.commandName = commandName != null ? commandName : throw new ArgumentNullException(nameof (commandName));
    this.caption = caption;
    this.hint = hint;
    this.commandHandler = commandHandler;
  }

  /// <summary>Конструктор</summary>
  /// <param name="menuItem">Пункт меню. Он должен иметь CommandName, который идентифицирует команду в методах QueryStatus() и Execute() класса CommandManager</param>
  /// <param name="commandHandler">Делегат обработчика команды</param>
  public ExternalAVSCommand(MenuItemBase menuItem, EventHandler commandHandler)
  {
    if (menuItem == null)
      throw new ArgumentNullException(nameof (menuItem));
    this.menuItem = menuItem.CommandName != null ? menuItem : throw new ArgumentException("Пункт меню должен иметь заданное свойство CommandName.", nameof (menuItem));
    this.commandName = menuItem.CommandName;
    this.caption = menuItem.Text;
    this.hint = menuItem.ToolTipText;
    this.commandHandler = commandHandler;
  }

  /// <summary>
  /// Имя команды. Идентифицирует команду в методах QueryStatus() и Execute() класса CommandManager
  /// </summary>
  public string CommandName
  {
    [DebuggerStepThrough] get => this.commandName;
  }

  /// <summary>Текст пункта меню</summary>
  public string Caption
  {
    [DebuggerStepThrough] get => this.caption;
  }

  /// <summary>Подсказка пункта меню</summary>
  public string Hint
  {
    [DebuggerStepThrough] get => this.hint;
  }

  /// <summary>
  /// Делегат обработчика команды (sender - это окно AVSWindow)
  /// </summary>
  public EventHandler CommandHandler
  {
    [DebuggerStepThrough] get => this.commandHandler;
  }

  /// <summary>
  /// Пункт меню. Может быть null, в этом случае AVS сам создаст пункт в меню "Документ"
  /// </summary>
  public MenuItemBase MenuItem
  {
    [DebuggerStepThrough] get => this.menuItem;
    [DebuggerStepThrough] set => this.menuItem = value;
  }
}
