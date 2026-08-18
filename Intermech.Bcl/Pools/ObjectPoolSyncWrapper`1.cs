
// Type: Intermech.Pools.ObjectPoolSyncWrapper`1
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Diagnostics;


namespace Intermech.Pools
{
    /// <summary>
    /// Обертка для объектов типа IObjectPool, позволяющая сделать их thread safe.
    /// </summary>
    public sealed class ObjectPoolSyncWrapper<T> : IObjectPool<T>
    {
      private IObjectPool<T> pool;
      private object syncRoot;

      /// <summary>Создает объект.</summary>
      /// <param name="pool">Пул объектов, который необходимо сделать thread safe</param>
      /// <exception cref="T:ArgumentNullException">Параметр <paramref name="pool" /> не должен быть равен null</exception>
      public ObjectPoolSyncWrapper(IObjectPool<T> pool)
      {
        this.pool = pool != null ? pool : throw new ArgumentNullException(nameof (pool));
        this.syncRoot = new object();
      }

      /// <summary>Возвращает объект, скрытый за оберткой.</summary>
      /// <returns>Объект, скрытый за оберткой</returns>
      public IObjectPool<T> Unwrap() => this.pool;

      /// <summary>Выделяет объект из пула.</summary>
      /// <returns>Выделенный объект</returns>
      public T Allocate()
      {
        lock (this.syncRoot)
          return this.pool.Allocate();
      }

      /// <summary>Возвращает объект обратно в пул.</summary>
      /// <param name="obj">Объект пула, выделенный ранее с помощью метода Allocate</param>
      public void Release(T obj)
      {
        lock (this.syncRoot)
          this.pool.Release(obj);
      }

      /// <summary>Количество объектов в пуле, доступных для выделения.</summary>
      public int IdleObjects
      {
        [DebuggerStepThrough] get
        {
          lock (this.syncRoot)
            return this.pool.IdleObjects;
        }
      }
    }
}
