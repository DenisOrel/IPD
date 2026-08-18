
// Type: Intermech.Client.Core.GridContextSearchHolder
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System.Diagnostics;


namespace Intermech.Client.Core;

/// <summary>
/// Контейнер настроек для контекстного поиска в элементе управления Навигатора, содержащем список строк
/// (для программного поиска)
/// </summary>
public class GridContextSearchHolder : IGridContextSearchHolder, IAssignable
{
  /// <summary>
  /// Шаблон для поиска (строка может содержать маски * и ?)
  /// </summary>
  private string _mask = string.Empty;
  /// <summary>Опции для поиска</summary>
  private GridContextSearchOptions _options;

  /// <summary>
  /// Создать экземпляр класса, заполненный значениями по умолчанию
  /// </summary>
  public GridContextSearchHolder()
    : this(string.Empty, GridContextSearchOptions.None)
  {
  }

  /// <summary>Создать заполненный экземпляр класса</summary>
  /// <param name="mask">Шаблон для поиска (строка может содержать маски * и ?)</param>
  /// <param name="options">Опции для поиска</param>
  public GridContextSearchHolder(string mask, GridContextSearchOptions options)
  {
    this._mask = mask;
    this._options = options;
  }

  /// <summary>Очистить поля класса</summary>
  public void Clear()
  {
    this._mask = string.Empty;
    this._options = GridContextSearchOptions.None;
  }

  /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
  /// <param name="source">Объект-источник</param>
  public void Assign(object source)
  {
    if (!(source is IGridContextSearchHolder contextSearchHolder))
      return;
    this._mask = contextSearchHolder.Mask;
    this._options = contextSearchHolder.Options;
  }

  /// <summary>
  /// Шаблон для поиска (строка может содержать маски * и ?)
  /// </summary>
  public string Mask
  {
    [DebuggerStepThrough] get => this._mask;
    set => this._mask = value;
  }

  /// <summary>Опции для поиска</summary>
  public GridContextSearchOptions Options
  {
    [DebuggerStepThrough] get => this._options;
    set => this._options = value;
  }
}
