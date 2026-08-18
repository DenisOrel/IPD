
// Type: Intermech.Client.Core.Show.Net.Stylus.StylusTable
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;


namespace Intermech.Client.Core.Show.Net.Stylus;

/// <summary>список типов линий(по цвету примитива)</summary>
[DebuggerDisplay("Count = {_dictionary.Count}")]
internal class StylusTable : IEnumerable<KeyValuePair<DwgColor, IStylus>>, IEnumerable
{
  /// <summary>коллекция перьев</summary>
  private readonly IDictionary<DwgColor, IStylus> _dictionary = (IDictionary<DwgColor, IStylus>) new Dictionary<DwgColor, IStylus>();

  /// <summary>Вернуть ссылку на интерфейс IEnumerator</summary>
  /// <returns>Ссылка на интерфейс IEnumerator</returns>
  public IEnumerator<KeyValuePair<DwgColor, IStylus>> GetEnumerator()
  {
    return this._dictionary.GetEnumerator();
  }

  /// <summary>Вернуть ссылку на интерфейс IEnumerator</summary>
  /// <returns>Ссылка на интерфейс IEnumerator</returns>
  IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this._dictionary.GetEnumerator();

  /// <summary>добавить пару (цвет примитива, перо)</summary>
  /// <param name="key">цвет примитива</param>
  /// <param name="value">перо</param>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private void Add(DwgColor key, IStylus value)
  {
    if (value == null)
      throw new ArgumentNullException(nameof (value));
    this._dictionary.Add(key, value);
  }

  /// <summary>получить по цвету примитива само перо</summary>
  /// <param name="key">цвет примитива</param>
  /// <returns>перо</returns>
  internal IStylus this[DwgColor key]
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._dictionary[key];
  }

  /// <summary>коллекция цветов перьев</summary>
  internal ICollection<DwgColor> Keys
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._dictionary.Keys;
  }

  /// <summary>проверка : есть ли перо для указанного цвета</summary>
  /// <param name="key">цвет примитива</param>
  /// <returns>true - есть перо</returns>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  internal bool ContainsKey(DwgColor key) => this._dictionary.ContainsKey(key);

  /// <summary>для цвета найти перо(если его нет то создать)</summary>
  /// <param name="key">цвет примитива</param>
  /// <returns>перо для цвета</returns>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  internal IStylus Generate(DwgColor key)
  {
    IStylus stylus1;
    if (this._dictionary.TryGetValue(key, out stylus1))
      return stylus1;
    IStylus stylus2;
    this.Add(key, stylus2 = (IStylus) new StylusObject(key));
    return stylus2;
  }
}
