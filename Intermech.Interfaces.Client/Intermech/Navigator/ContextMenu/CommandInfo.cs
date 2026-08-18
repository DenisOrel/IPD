// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.ContextMenu.CommandInfo
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Navigator.ContextMenu;

/// <summary>
/// Класс описывает команду контекстного меню – её приоритет, делегаты для обработчика команды и
/// для её отрисовки (если требуется). Экземпляры данного класса используются в коллекции CommandsInfo.
/// </summary>
public class CommandInfo
{
  /// <summary>
  /// Возвращает приоритет команды. Он используется сборщиком контекстного меню только в том случае,
  /// если несколько провайдеров одного уровня представляют одну и ту же команду.
  /// В этом случае будет выбрана команда с максимальным приоритетом.
  /// </summary>
  private int _priority;
  /// <summary>
  /// Возвращает делегат метода, который вызывается для выполнения команды контекстного меню.
  /// </summary>
  private ClickEventHandler _clickHandler;
  /// <summary>
  /// Возвращает делегат метода, который вызывается для отрисовки пункта контекстного меню, соответствующего команде.
  /// </summary>
  private DrawEventHandler _drawHandler;
  /// <summary>
  /// Возвращает дополнительные сведения, которые должны быть переданы методу, вызываемому для выполнения команды контекстного меню.
  /// </summary>
  private object _additionalInfo;
  /// <summary>Дополнительные настройки элемента контекстного меню</summary>
  private ContextMenuItemState _state = ContextMenuItemState.Default;

  /// <summary>
  /// Возвращает приоритет команды. Он используется сборщиком контекстного меню только в том случае,
  /// если несколько провайдеров одного уровня представляют одну и ту же команду.
  /// В этом случае будет выбрана команда с максимальным приоритетом.
  /// </summary>
  public int Priority => this._priority;

  /// <summary>
  /// Возвращает делегат метода, который вызывается для выполнения команды контекстного меню.
  /// </summary>
  public ClickEventHandler ClickHandler => this._clickHandler;

  /// <summary>
  /// Возвращает делегат метода, который вызывается для отрисовки пункта контекстного меню, соответствующего команде.
  /// </summary>
  public DrawEventHandler DrawHandler => this._drawHandler;

  /// <summary>
  /// Возвращает дополнительные сведения, которые должны быть переданы методу, вызываемому для выполнения команды контекстного меню.
  /// </summary>
  public object AdditionalInfo => this._additionalInfo;

  /// <summary>Дополнительные настройки элемента контекстного меню</summary>
  public ContextMenuItemState State => this._state;

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="priority">Приоритет команды</param>
  public CommandInfo(int priority)
    : this(priority, (ClickEventHandler) null, (DrawEventHandler) null, (object) null)
  {
  }

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="priority">Приоритет команды</param>
  /// <param name="state">Дополнительные настройки элемента контекстного меню</param>
  public CommandInfo(int priority, ContextMenuItemState state)
    : this(priority, (ClickEventHandler) null, (DrawEventHandler) null, (object) null, state)
  {
  }

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="priority">Приоритет команды</param>
  /// <param name="clickHandler">Делегат обработчика данной команды</param>
  public CommandInfo(int priority, ClickEventHandler clickHandler)
    : this(priority, clickHandler, (DrawEventHandler) null, (object) null)
  {
  }

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="priority">Приоритет команды</param>
  /// <param name="clickHandler">Делегат обработчика данной команды</param>
  /// <param name="additionalInfo">Дополнительная информация</param>
  public CommandInfo(int priority, ClickEventHandler clickHandler, object additionalInfo)
    : this(priority, clickHandler, (DrawEventHandler) null, additionalInfo)
  {
  }

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="priority">Приоритет команды</param>
  /// <param name="clickHandler">Делегат обработчика данной команды</param>
  /// <param name="drawHandler">Делегат для отрисовки данной команды в контекстном меню</param>
  public CommandInfo(int priority, ClickEventHandler clickHandler, DrawEventHandler drawHandler)
    : this(priority, clickHandler, drawHandler, (object) null)
  {
  }

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="priority">Приоритет команды</param>
  /// <param name="clickHandler">Делегат обработчика данной команды</param>
  /// <param name="state">Дополнительные настройки элемента контекстного меню</param>
  public CommandInfo(int priority, ClickEventHandler clickHandler, ContextMenuItemState state)
    : this(priority, clickHandler, (DrawEventHandler) null, (object) null, state)
  {
  }

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="priority">Приоритет команды</param>
  /// <param name="clickHandler">Делегат обработчика данной команды</param>
  /// <param name="drawHandler">Делегат для отрисовки данной команды в контекстном меню</param>
  /// <param name="additionalInfo">Дополнительная информация</param>
  public CommandInfo(
    int priority,
    ClickEventHandler clickHandler,
    DrawEventHandler drawHandler,
    object additionalInfo)
    : this(priority, clickHandler, drawHandler, additionalInfo, ContextMenuItemState.Default)
  {
  }

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="priority">Приоритет команды</param>
  /// <param name="clickHandler">Делегат обработчика данной команды</param>
  /// <param name="drawHandler">Делегат для отрисовки данной команды в контекстном меню</param>
  /// <param name="additionalInfo">Дополнительная информация</param>
  /// <param name="state">Дополнительные настройки элемента контекстного меню</param>
  public CommandInfo(
    int priority,
    ClickEventHandler clickHandler,
    DrawEventHandler drawHandler,
    object additionalInfo,
    ContextMenuItemState state)
  {
    this._priority = priority;
    this._clickHandler = clickHandler;
    this._drawHandler = drawHandler;
    this._additionalInfo = additionalInfo;
    this._state = state;
  }
}
