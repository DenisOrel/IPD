
// Type: Intermech.PropertyEditors.CollectionValueAdapter`1
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;


namespace Intermech.PropertyEditors;

/// <summary>
/// Реализует адаптер для значений коллекций, редактируемых с помощью редактора на основе CollectionEditor.
/// Адаптер добавляет конструктор по умолчанию для значений коллекций, а также улучшает отображение значений коллекций в редакторе.
/// Основное назначение адаптера - улучшение работы CollectionEditor при редактировании коллекций примитивных значений.
/// </summary>
/// <remarks>
/// Редакторы коллекций на основе CollectionEditor требуют, чтобы значения коллекции имели конструктор по умолчанию.
/// Используя этот адаптер можно добавить конструктор по умолчанию даже тем типам, чьи исходные тексты недоступны (например, тип string).
/// </remarks>
/// <typeparam name="T">Тип значения коллекции</typeparam>
[DefaultProperty("Value")]
public sealed class CollectionValueAdapter<T> : ICloneable where T : ICloneable
{
  private T value;
  private int? hashValue;
  private string strValue;

  /// <summary>
  /// Конструктор по умолчанию, требуемый редактором коллекции.
  /// </summary>
  public CollectionValueAdapter() => this.value = default (T);

  /// <summary>Создает объект.</summary>
  /// <param name="value">Значение коллекции</param>
  public CollectionValueAdapter(T value) => this.value = value;

  /// <summary>Возвращает или изменяет значение коллекции.</summary>
  [DisplayName("Значение")]
  public T Value
  {
    get => this.value;
    set
    {
      if (object.Equals((object) this.value, (object) value))
        return;
      this.value = value;
      this.hashValue = new int?();
      this.strValue = (string) null;
    }
  }

  public CollectionValueAdapter<T> Clone()
  {
    return new CollectionValueAdapter<T>((object) this.value != null ? (T) this.value.Clone() : this.value);
  }

  object ICloneable.Clone() => (object) this.Clone();

  public override int GetHashCode()
  {
    if (!this.hashValue.HasValue)
      this.hashValue = new int?((object) this.value != null ? this.value.GetHashCode() : 0);
    return this.hashValue.Value;
  }

  public override bool Equals(object obj)
  {
    return !(obj is CollectionValueAdapter<T> collectionValueAdapter) ? base.Equals(obj) : object.Equals((object) collectionValueAdapter.value, (object) this.value);
  }

  public override string ToString()
  {
    if (this.strValue == null)
    {
      if ((object) this.value != null)
        this.strValue = this.value.ToString();
      if (string.IsNullOrEmpty(this.strValue))
        this.strValue = "<пусто>";
    }
    return this.strValue;
  }
}
