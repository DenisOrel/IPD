
// Type: Intermech.PropertyEditors.ChangeHighlighting.ChangeTrackingListAdapter`1
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections;
using System.Collections.Generic;


namespace Intermech.PropertyEditors.ChangeHighlighting;

/// <summary>
/// Реализует адаптер для списков, обеспечивающий подсветку изменений в списке при его редактировании в PropertyGrid.
/// Данный адаптер используется в паре с <see cref="T:Intermech.PropertyEditors.ChangeHighlighting.EditableObjectChangeHighlighter" />.
/// Функция адаптера - это предоставление операций клонирования и сравнения списков.
/// </summary>
/// <typeparam name="T">Тип значений в списке</typeparam>
public class ChangeTrackingListAdapter<T> : IEnumerable<T>, IEnumerable, ICloneable where T : ICloneable
{
  private readonly List<T> list;

  /// <summary>Создает объект.</summary>
  public ChangeTrackingListAdapter()
    : this(4)
  {
  }

  /// <summary>Создает объект.</summary>
  /// <param name="capacity">Начальная емкость списока</param>
  public ChangeTrackingListAdapter(int capacity) => this.list = new List<T>(capacity);

  /// <summary>Создает объект.</summary>
  /// <param name="collection">Начальное содержимое списка</param>
  public ChangeTrackingListAdapter(IEnumerable<T> collection)
  {
    this.list = new List<T>(collection);
  }

  /// <summary>Возвращает необернутый список.</summary>
  public List<T> Items => this.list;

  /// <summary>Клонирует список.</summary>
  /// <returns>Клон списка</returns>
  public ChangeTrackingListAdapter<T> Clone()
  {
    List<T> collection = new List<T>(this.list.Count);
    foreach (T obj in this.list)
      collection.Add((T) obj.Clone());
    return new ChangeTrackingListAdapter<T>((IEnumerable<T>) collection);
  }

  /// <summary>Клонирует объект</summary>
  /// <returns>Клон объекта</returns>
  object ICloneable.Clone() => (object) this.Clone();

  public IEnumerator<T> GetEnumerator() => (IEnumerator<T>) this.list.GetEnumerator();

  IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.list.GetEnumerator();

  public override int GetHashCode() => this.list.Count.GetHashCode();

  public override bool Equals(object obj)
  {
    if (!(obj is ChangeTrackingListAdapter<T> trackingListAdapter))
      return base.Equals(obj);
    if (trackingListAdapter.list.Count != this.list.Count)
      return false;
    for (int index = 0; index < trackingListAdapter.list.Count; ++index)
    {
      if (!object.Equals((object) trackingListAdapter.list[index], (object) this.list[index]))
        return false;
    }
    return true;
  }

  public override string ToString() => "(Список)";
}
