
// Type: Intermech.Client.Core.NavigatorContextSearchHolder
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;


namespace Intermech.Client.Core;

/// <summary>
/// Контейнер настроек для контекстного поиска в элементе управления Навигатора, содержащем список строк
/// </summary>
public class NavigatorContextSearchHolder : INavigatorContextSearchHolder, IAssignable
{
  /// <summary>
  /// Шаблон для поиска (строка может содержать маски * и ?)
  /// </summary>
  private string _mask = string.Empty;
  /// <summary>История значений для поиска</summary>
  private List<string> _history = new List<string>(64 /*0x40*/);
  /// <summary>Опции для поиска</summary>
  private NavigatorContextSearchOptions _options;
  /// <summary>
  /// Номер строки (y) и столбца (x), которые были найдены при последнем поиске.
  /// Данное поле является точкой отсчёта для дальнейшего поиска
  /// </summary>
  private Point _lastFoundItem = Point.Empty;

  /// <summary>
  /// Создать экземпляр класса, заполненный значениями по умолчанию
  /// </summary>
  public NavigatorContextSearchHolder()
    : this(string.Empty, NavigatorContextSearchOptions.None, Point.Empty)
  {
  }

  /// <summary>Создать заполненный экземпляр класса</summary>
  /// <param name="mask">Шаблон для поиска (строка может содержать маски * и ?)</param>
  /// <param name="options">Опции для поиска</param>
  /// <param name="lastFoundItem">Номер строки (y) и столбца (x), которые были найдены при последнем поиске.
  /// Данное поле является точкой отсчёта для дальнейшего поиска</param>
  public NavigatorContextSearchHolder(
    string mask,
    NavigatorContextSearchOptions options,
    Point lastFoundItem)
  {
    this._mask = mask;
    this._options = options;
    this._lastFoundItem = lastFoundItem;
  }

  /// <summary>Очистить поля класса</summary>
  public void Clear()
  {
    this._mask = string.Empty;
    this._options = NavigatorContextSearchOptions.None;
    this._lastFoundItem = Point.Empty;
    this._history.Clear();
  }

  /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
  /// <param name="source">Объект-источник</param>
  public void Assign(object source)
  {
    if (!(source is INavigatorContextSearchHolder contextSearchHolder))
      return;
    this._mask = contextSearchHolder.Mask;
    this._options = contextSearchHolder.Options;
    this._lastFoundItem = contextSearchHolder.LastFoundItem;
    this._history = new List<string>((IEnumerable<string>) contextSearchHolder.History);
  }

  /// <summary>
  /// Шаблон для поиска (строка может содержать маски * и ?)
  /// </summary>
  public string Mask
  {
    [DebuggerStepThrough] get => this._mask;
    set
    {
      this._mask = value;
      this._history.Remove(value);
      this._history.Insert(0, value);
    }
  }

  /// <summary>История значений для поиска</summary>
  public List<string> History
  {
    [DebuggerStepThrough] get => this._history;
  }

  /// <summary>Опции для поиска</summary>
  public NavigatorContextSearchOptions Options
  {
    [DebuggerStepThrough] get => this._options;
    set => this._options = value;
  }

  /// <summary>
  /// Номер строки (y) и столбца (x), которые были найдены при последнем поиске.
  /// Данное поле является точкой отсчёта для дальнейшего поиска
  /// </summary>
  public Point LastFoundItem
  {
    [DebuggerStepThrough] get => this._lastFoundItem;
    set => this._lastFoundItem = value;
  }
}
