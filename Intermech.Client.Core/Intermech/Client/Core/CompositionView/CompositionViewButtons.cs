
// Type: Intermech.Client.Core.CompositionView.CompositionViewButtons
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections;
using System.Collections.Generic;


namespace Intermech.Client.Core.CompositionView;

/// <summary>
/// Сервис хранящий типы кнопок доступные для панели "Состав"
/// </summary>
public class CompositionViewButtons : IEnumerable<KeyValuePair<Type, string>>, IEnumerable
{
  /// <summary>
  /// 
  /// </summary>
  protected Dictionary<Type, string> _cache = new Dictionary<Type, string>();

  /// <summary>Добавить тип кнопок</summary>
  /// <param name="buttonType">тип кнопки (должен наследоваться от cvButtonBase)</param>
  /// <param name="menuItem">строковое описание</param>
  public void Add(Type buttonType, string menuItem)
  {
    if (!buttonType.IsSubclassOf(typeof (CVButtonBase)))
      return;
    this._cache[buttonType] = menuItem;
  }

  /// <summary>Количество доступных кнопок</summary>
  public int Count => this._cache.Count;

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public IEnumerator<KeyValuePair<Type, string>> GetEnumerator()
  {
    return (IEnumerator<KeyValuePair<Type, string>>) this._cache.GetEnumerator();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();
}
