
// Type: Intermech.Navigator.MRUItem
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using System;


namespace Intermech.Navigator;

/// <summary>Элемент коллекции "Наиболее часто используемые"</summary>
[Serializable]
public class MRUItem : IMRUItem, ICloneable, IComparable, IComparable<MRUItem>
{
  /// <summary>Дата и время последнего доступа (в UTC-формате)</summary>
  protected DateTime _lastAccess = DateTime.UtcNow;
  /// <summary>Количество "попаданий" в элемент</summary>
  protected int _hintCount;
  /// <summary>Текстовое пояснение элемента</summary>
  protected string _caption = string.Empty;
  /// <summary>Основное значение элемента</summary>
  protected object _value;
  /// <summary>Дополнительное значение элемента</summary>
  protected object _tag;

  /// <summary>Базовый конструктор</summary>
  public MRUItem()
  {
  }

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="caption">Текстовое пояснение элемента</param>
  /// <param name="value">Основное значение элемента</param>
  public MRUItem(string caption, object value)
    : this(caption, value, (object) null, DateTime.UtcNow, 0)
  {
  }

  /// <summary>Создать полностью заполненный экземпляр класса</summary>
  /// <param name="caption">Текстовое пояснение элемента</param>
  /// <param name="value">Основное значение элемента</param>
  /// <param name="tag">Дополнительное значение элемента</param>
  public MRUItem(string caption, object value, object tag)
    : this(caption, value, tag, DateTime.UtcNow, 0)
  {
  }

  /// <summary>Создать полностью заполненный экземпляр класса</summary>
  /// <param name="caption">Текстовое пояснение элемента</param>
  /// <param name="value">Основное значение элемента</param>
  /// <param name="tag">Дополнительное значение элемента</param>
  /// <param name="lastAccess">Дата и время последнего доступа (в UTC-формате)</param>
  public MRUItem(string caption, object value, object tag, DateTime lastAccess)
    : this(caption, value, tag, lastAccess, 0)
  {
  }

  /// <summary>Создать полностью заполненный экземпляр класса</summary>
  /// <param name="caption">Текстовое пояснение элемента</param>
  /// <param name="value">Основное значение элемента</param>
  /// <param name="tag">Дополнительное значение элемента</param>
  /// <param name="lastAccess">Дата и время последнего доступа (в UTC-формате)</param>
  /// <param name="hitCount">Количество "попаданий" в элемент</param>
  public MRUItem(string caption, object value, object tag, DateTime lastAccess, int hitCount)
  {
    this._caption = caption;
    this._value = value;
    this._tag = tag;
    this._lastAccess = lastAccess;
    this._hintCount = hitCount;
  }

  /// <summary>
  /// Сравнить текущий экземпляр объекта с указанным объектом
  /// </summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>true, если объекты равны</returns>
  public override bool Equals(object obj)
  {
    if (!(obj is MRUItem mruItem))
      return base.Equals(obj);
    return mruItem._value == null ? base.Equals(obj) : this._value.Equals(mruItem._value);
  }

  /// <summary>Вернуть 32-битный хэш-код экземпляра объекта</summary>
  /// <returns>32-битный хэш-код экземпляра объекта</returns>
  public override int GetHashCode()
  {
    return this._value == null ? this._caption.GetHashCode() << 12 ^ this._hintCount.GetHashCode() << 8 ^ this._lastAccess.GetHashCode() : this._value.GetHashCode() << 24 ^ this._caption.GetHashCode() << 12 ^ this._hintCount.GetHashCode() << 8 ^ this._lastAccess.GetHashCode();
  }

  /// <summary>Вернуть строковое представление экземпляра объекта</summary>
  /// <returns>Строковое представление экземпляра объекта</returns>
  public override string ToString() => this._caption;

  /// <summary>Дата и время последнего доступа (в UTC-формате)</summary>
  public DateTime LastAccess
  {
    get => this._lastAccess;
    set => this._lastAccess = value;
  }

  /// <summary>Количество "попаданий" в элемент</summary>
  public int HintCount
  {
    get => this._hintCount;
    set => this._hintCount = value;
  }

  /// <summary>Текстовое пояснение элемента</summary>
  public string Caption
  {
    get => this._caption;
    set => this._caption = value;
  }

  /// <summary>Основное значение элемента</summary>
  public object Value
  {
    get => this._value;
    set => this._value = value;
  }

  /// <summary>Дополнительное значение элемента</summary>
  public object Tag
  {
    get => this._tag;
    set => this._tag = value;
  }

  /// <summary>Создать точную копию экземпляра объекта</summary>
  /// <returns>Точная копия экземпляра объекта</returns>
  public object Clone()
  {
    return (object) new MRUItem(this._caption, this._value, this._tag, this._lastAccess, this._hintCount);
  }

  /// <summary>
  /// Сравнить текущий экземпляр объекта с указанным объектом
  /// </summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>-1 - объект меньше, чем obj, 0 - объекты равны, 1 - объект больше, чем obj</returns>
  public int CompareTo(object obj) => 0;

  /// <summary>
  /// Сравнить текущий экземпляр объекта с указанным объектом
  /// </summary>
  /// <param name="other">Объект для сравнения</param>
  /// <returns>-1 - объект меньше, чем obj, 0 - объекты равны, 1 - объект больше, чем obj</returns>
  public int CompareTo(MRUItem other) => 0;
}
