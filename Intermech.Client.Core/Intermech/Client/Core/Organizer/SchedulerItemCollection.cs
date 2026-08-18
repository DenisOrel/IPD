
// Type: Intermech.Client.Core.Organizer.SchedulerItemCollection
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections.Generic;


namespace Intermech.Client.Core.Organizer;

/// <summary>
/// 
/// </summary>
public class SchedulerItemCollection : List<CalendarItem>
{
  private Scheduler _scheduler;

  /// <summary>Планировщик.</summary>
  public Scheduler Scheduler => this._scheduler;

  /// <summary>Конструктор.</summary>
  /// <param name="scheduler">Calendar this collection will belong to.</param>
  internal SchedulerItemCollection(Scheduler scheduler) => this._scheduler = scheduler;

  /// <summary>Изменение списка элементов.</summary>
  private void CollectionChanged()
  {
    this.Scheduler.Renderer.PerformItemsLayout();
    this.Scheduler.Invalidate();
  }

  /// <summary>Добавление элемента вконец списка элементов.</summary>
  /// <param name="item">Элемент, добавляемый в колекцию элементов</param>
  public new void Add(CalendarItem item)
  {
    base.Add(item);
    this.CollectionChanged();
  }

  /// <summary>Добавление элементов вконец списка элементов.</summary>
  /// <param name="items">Список элементов, добавляемых в коллекцию элементов</param>
  public new void AddRange(IEnumerable<CalendarItem> items)
  {
    base.AddRange(items);
    this.CollectionChanged();
  }

  /// <summary>Очистить список элементов.</summary>
  public new void Clear()
  {
    base.Clear();
    this.CollectionChanged();
  }

  /// <summary>
  /// Добавление элемента в указанное место в списоке элементов.
  /// </summary>
  /// <param name="index">Индекс позиции</param>
  /// <param name="item">Элемент, добавляемый в колекцию элементов</param>
  public new void Insert(int index, CalendarItem item)
  {
    base.Insert(index, item);
    this.CollectionChanged();
  }

  /// <summary>
  /// Добавление элементов в указанное место в списоке элементов.
  /// </summary>
  /// <param name="index">Индекс позиции</param>
  /// <param name="items">Список элементов, добавляемых в коллекцию элементов</param>
  public new void InsertRange(int index, IEnumerable<CalendarItem> items)
  {
    base.InsertRange(index, items);
    this.CollectionChanged();
  }

  /// <summary>Удаление элемента из списка элементов.</summary>
  /// <param name="item">Эелемент, удаляемый из списка элементов</param>
  /// <returns><c>true</c> если элемент удачно удален; иначе, <c>false</c>.
  /// Метод также возвращает false если элемент небыл найден в коллекции элементов</returns>
  public new bool Remove(CalendarItem item)
  {
    int num = base.Remove(item) ? 1 : 0;
    this.CollectionChanged();
    return num != 0;
  }

  /// <summary>
  /// Удаление элемента по идентификатору объекта, по данным которого создавался элемент планировщика.
  /// </summary>
  /// <param name="objID">Идентификатор объекта</param>
  public void Remove(long objID)
  {
    for (int index = 0; index < this.Count; ++index)
    {
      CalendarItem calendarItem = this[index];
      if (calendarItem.ObjectID == objID)
      {
        if (!this.Remove(calendarItem))
          break;
        this.CollectionChanged();
        break;
      }
    }
  }

  /// <summary>
  /// Удаление элемента с указанным индексом из списка элементов.
  /// </summary>
  /// <param name="index">Индекс элемента предназначенного для удаления</param>
  /// <returns></returns>
  public new void RemoveAt(int index)
  {
    base.RemoveAt(index);
    this.CollectionChanged();
  }

  /// <summary>
  /// Removes the all the items that match the conditions defined by the specified predicate.
  /// </summary>
  /// <param name="match">The Predicate delegate that defines the conditions of the items to remove.</param>
  /// <returns>Количество элементов, удаленных из коллекции</returns>
  public new int RemoveAll(Predicate<CalendarItem> match)
  {
    int num = base.RemoveAll(match);
    this.CollectionChanged();
    return num;
  }

  /// <summary>Удаление набора элементов из списка элементов.</summary>
  /// <param name="index">Индек первого элемента предназначенного для удаления</param>
  /// <param name="count">Количество элементов предназначенных для удаления</param>
  public new void RemoveRange(int index, int count)
  {
    base.RemoveRange(index, count);
    this.CollectionChanged();
  }
}
