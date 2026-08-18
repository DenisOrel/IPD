
// Type: Intermech.Pools.ListPool`1
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.Pools
{
    /// <summary>
    /// Реализует пул объектов на основе класса List. Реализация не является thread safe.
    /// </summary>
    /// <typeparam name="T">Тип объектов в пуле</typeparam>
    public sealed class ListPool<T> : ObjectPoolBase<T>
    {
      private List<T> container;

      /// <summary>Создает объект.</summary>
      /// <param name="minCapacity">Начальая емкость пула объектов. Значение параметра может быть равно 0</param>
      /// <param name="objectFactory">Фабрика для создания и обслуживания объектов в пуле</param>
      /// <exception cref="T:ArgumentOutOfRangeException">Параметр <paramref name="minCapacity" /> не должен быть отрицательным числом</exception>
      /// <exception cref="T:ArgumentNullException">Параметр <paramref name="objectFactory" /> не должен быть равен null</exception>
      public ListPool(int minCapacity, PoolableObjectFactory<T> objectFactory)
        : base(minCapacity, objectFactory)
      {
      }

      /// <summary>Создает объект.</summary>
      /// <param name="minCapacity">Начальая емкость пула объектов. Значение параметра может быть равно 0</param>
      /// <param name="createFunction">Функция создания экземпляров объектов для пула</param>
      /// <exception cref="T:ArgumentOutOfRangeException">Параметр <paramref name="minCapacity" /> не должен быть отрицательным числом</exception>
      /// <exception cref="T:ArgumentNullException">Параметр <paramref name="createFunction" /> не должен быть равен null</exception>
      public ListPool(int minCapacity, Func<T> createFunction)
        : base(minCapacity, createFunction)
      {
      }

      /// <summary>Создает пустой контейнер для элементов пула.</summary>
      /// <param name="minCapacity">Затребованная клиентом минимальная емкость пула</param>
      protected override void CreateObjectContainer(int minCapacity)
      {
        this.container = new List<T>(minCapacity != 0 ? minCapacity * 2 : 8);
      }

      /// <summary>Извлекает объект из пула.</summary>
      /// <param name="obj">Извлеченный объект</param>
      /// <returns>Признак успешного или неуспешного извлечения в случае пустого пула</returns>
      protected override bool TryGetObject(out T obj)
      {
        int index = this.container.Count - 1;
        if (index >= 0)
        {
          obj = this.container[index];
          this.container.RemoveAt(index);
          return true;
        }
        obj = default (T);
        return false;
      }

      /// <summary>
      /// Помещает указанный объект в контейнер для элементов пула.
      /// </summary>
      /// <param name="obj">Добавляемый объект</param>
      protected override void PutObject(T obj) => this.container.Add(obj);

      /// <summary>Количество объектов в пуле, доступных для выделения.</summary>
      public override int IdleObjects
      {
        [DebuggerStepThrough] get => this.container.Count;
      }
    }
}
