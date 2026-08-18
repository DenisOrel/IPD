// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.ContextMenu.ContextMenuItemState
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Navigator.ContextMenu;

/// <summary>
/// Дополнительные настройки состояния элемента контекстного меню
/// </summary>
public class ContextMenuItemState
{
  /// <summary>Состояние переключателя у элемента контекстного меню</summary>
  private ContextMenuCheckState _state;
  /// <summary>
  /// Дополнительные настройки состояния элемента контекстного меню - по умолчанию
  /// </summary>
  private static ContextMenuItemState _default = new ContextMenuItemState(ContextMenuCheckState.Default);
  /// <summary>
  /// Дополнительные настройки состояния элемента контекстного меню - переключатель есть, состояние [ ]
  /// </summary>
  private static ContextMenuItemState _unchecked = new ContextMenuItemState(ContextMenuCheckState.Unchecked);
  /// <summary>
  /// Дополнительные настройки состояния элемента контекстного меню - переключатель есть, состояние [x]
  /// </summary>
  private static ContextMenuItemState _checked = new ContextMenuItemState(ContextMenuCheckState.Checked);

  /// <summary>Состояние переключателя у элемента контекстного меню</summary>
  public ContextMenuCheckState State => this._state;

  /// <summary>
  /// Дополнительные настройки состояния элемента контекстного меню - по умолчанию
  /// </summary>
  public static ContextMenuItemState Default => ContextMenuItemState._default;

  /// <summary>
  /// Дополнительные настройки состояния элемента контекстного меню - переключатель есть, состояние [ ]
  /// </summary>
  public static ContextMenuItemState Unchecked => ContextMenuItemState._unchecked;

  /// <summary>
  /// Дополнительные настройки состояния элемента контекстного меню - переключатель есть, состояние [x]
  /// </summary>
  public static ContextMenuItemState Checked => ContextMenuItemState._checked;

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="state">Состояние переключателя у элемента контекстного меню</param>
  public ContextMenuItemState(ContextMenuCheckState state) => this._state = state;
}
