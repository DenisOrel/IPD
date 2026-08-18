
// Type: Intermech.Collections.ExtensibleList`1
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;


namespace Intermech.Collections
{
    /// <summary>
    /// Реализует generic-список объектов, позволяющих расширять или заменять стандартную реализацию методов
    /// списка.
    /// </summary>
    /// <typeparam name="T">Тип объектов в списке</typeparam>
    public class ExtensibleList<T> : 
      IList<T>,
      ICollection<T>,
      IEnumerable<T>,
      IEnumerable,
      IList,
      ICollection
    {
      private IList<T> items;

      /// <summary>Создает список.</summary>
      public ExtensibleList() => this.items = (IList<T>) new List<T>();

      /// <summary>Добавляет элемент в список.</summary>
      /// <param name="item">Добавляемый элемент</param>
      public void Add(T item) => this.InsertItem(this.items.Count, item);

      /// <summary>Очищает список.</summary>
      public void Clear() => this.ClearItems();

      /// <summary>Реализует очистку списка.</summary>
      protected virtual void ClearItems() => this.items.Clear();

      /// <summary>Проверяет, содержится ли элемент в списке.</summary>
      /// <param name="item">Искомый элемент</param>
      /// <returns>Возвращает true, если элемент присутствует в списке</returns>
      public bool Contains(T item) => this.ContainsItem(item);

      /// <summary>Реализует проверку присутствия элемента в списке</summary>
      /// <param name="item">Искомый элемент</param>
      /// <returns>Возвращает true, если элемент присутствует в списке</returns>
      protected virtual bool ContainsItem(T item) => this.items.Contains(item);

      /// <summary>Копирует содержимое списка в указанный массив.</summary>
      /// <param name="array">Массив-приемник</param>
      /// <param name="index">Индекс элемента в массиве, начиная с которого будут расположены копируемые элементы</param>
      public void CopyTo(T[] array, int index) => this.items.CopyTo(array, index);

      /// <summary>Возвращает перечислитель элементов списка.</summary>
      /// <returns>Перечислитель элементов списка</returns>
      public IEnumerator<T> GetEnumerator() => this.items.GetEnumerator();

      /// <summary>Возвращает индекс элемента в списке.</summary>
      /// <param name="item">Искомый элемент</param>
      /// <returns>Индекс элемента в списке. Если такого элемента в списке нет, то метод вернет null</returns>
      public int IndexOf(T item) => this.IndexOfItem(item);

      /// <summary>Реализует определение индекса элемента в списке.</summary>
      /// <param name="item">Искомый элемент</param>
      /// <returns>Индекс элемента в списке. Если такого элемента в списке нет, то метод вернет null</returns>
      protected virtual int IndexOfItem(T item) => this.items.IndexOf(item);

      /// <summary>Вставляет элемент в список.</summary>
      /// <param name="index">Индекс элемента в списке</param>
      /// <param name="item">Вставляемый элемент</param>
      public void Insert(int index, T item)
      {
        this.CheckIndex(index);
        this.InsertItem(index, item);
      }

      /// <summary>Реализует вставку элемента в список.</summary>
      /// <param name="index">Индекс элемента в списке</param>
      /// <param name="item">Вставляемый элемент</param>
      protected virtual void InsertItem(int index, T item) => this.items.Insert(index, item);

      /// <summary>Удаляет элемент из списка.</summary>
      /// <param name="item">Удаляемый элемент</param>
      /// <returns>Возвращает true, если элемент был успешно удален</returns>
      public bool Remove(T item)
      {
        int index = this.items.IndexOf(item);
        if (index < 0)
          return false;
        this.RemoveItem(index);
        return true;
      }

      /// <summary>Удаляет элемент из списка.</summary>
      /// <param name="index">Индекс удаляемого элемента в списке</param>
      public void RemoveAt(int index)
      {
        this.CheckIndex(index);
        this.RemoveItem(index);
      }

      /// <summary>Реализует удаление элемента из списка.</summary>
      /// <param name="index">Индекс удаляемого элемента в списке</param>
      protected virtual void RemoveItem(int index) => this.items.RemoveAt(index);

      /// <summary>
      /// Реализует установку нового значения для элемента в списке.
      /// </summary>
      /// <param name="index">Индекс элемента</param>
      /// <param name="item">Новое значение элемента</param>
      protected virtual void SetItem(int index, T item) => this.items[index] = item;

      /// <summary>Возвращает количество элементов в списке.</summary>
      public int Count => this.items.Count;

      /// <summary>Возвращает или задает значение элемента в списке.</summary>
      /// <param name="index">Индекс элемента</param>
      /// <returns>Значение элемента</returns>
      public T this[int index]
      {
        get => this.items[index];
        set
        {
          this.CheckIndex(index);
          this.SetItem(index, value);
        }
      }

      /// <summary>Возвращает внутренний контейнер элементов списка.</summary>
      protected IList<T> Items => this.items;

      void ICollection.CopyTo(Array array, int index)
      {
        ((ICollection) this.items).CopyTo(array, index);
      }

      IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.items.GetEnumerator();

      int IList.Add(object value)
      {
        ExtensibleList<T>.VerifyValueType(value);
        this.Add((T) value);
        return this.Count - 1;
      }

      bool IList.Contains(object value)
      {
        return ExtensibleList<T>.IsCompatibleObject(value) && this.Contains((T) value);
      }

      int IList.IndexOf(object value)
      {
        return ExtensibleList<T>.IsCompatibleObject(value) ? this.IndexOf((T) value) : -1;
      }

      void IList.Insert(int index, object value)
      {
        ExtensibleList<T>.VerifyValueType(value);
        this.Insert(index, (T) value);
      }

      void IList.Remove(object value)
      {
        if (!ExtensibleList<T>.IsCompatibleObject(value))
          return;
        this.Remove((T) value);
      }

      bool ICollection<T>.IsReadOnly => this.items.IsReadOnly;

      bool ICollection.IsSynchronized => false;

      object ICollection.SyncRoot => ((ICollection) this.items).SyncRoot;

      bool IList.IsFixedSize => ((IList) this.items).IsFixedSize;

      bool IList.IsReadOnly => ((IList) this.items).IsReadOnly;

      object IList.this[int index]
      {
        get => (object) this.items[index];
        set
        {
          ExtensibleList<T>.VerifyValueType(value);
          this[index] = (T) value;
        }
      }

      private void CheckIndex(int index)
      {
        if (index < 0 || index > this.items.Count)
          throw new ArgumentOutOfRangeException(nameof (index), string.Format(LocalizationHolder.rm.GetString("Interfaces_1"), (object) index));
      }

      private static void VerifyValueType(object value)
      {
        if (!ExtensibleList<T>.IsCompatibleObject(value))
          throw new ArgumentException(string.Format(LocalizationHolder.rm.GetString("Interfaces_2"), (object) value.GetType()), nameof (value));
      }

      private static bool IsCompatibleObject(object value)
      {
        return value is T || value == null && !typeof (T).IsValueType;
      }
    }
}
