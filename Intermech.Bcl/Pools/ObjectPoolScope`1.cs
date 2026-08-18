
// Type: Intermech.Pools.ObjectPoolScope`1
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Diagnostics;


namespace Intermech.Pools
{
    /// <summary>
    /// Динамическая область видимости, обеспечивающая захват объектов из пула и возврат объектов в пул.
    /// </summary>
    /// <typeparam name="T">Тип объектов в пуле</typeparam>
    public sealed class ObjectPoolScope<T> : IDisposable
    {
      private IObjectPool<T> pool;
      private T allocatedObject;

      /// <summary>Создает объект.</summary>
      /// <param name="pool">Пул объектов</param>
      /// <param name="allocatedObject">Экземпляр объекта, полученный из пула объектов</param>
      /// <exception cref="T:ArgumentNullException">Параметр <paramref name="pool" /> не должен быть равен null</exception>
      public ObjectPoolScope(IObjectPool<T> pool, T allocatedObject)
      {
        this.pool = pool != null ? pool : throw new ArgumentNullException(nameof (pool));
        this.allocatedObject = allocatedObject;
      }

      /// <summary>
      /// Возвращает экземпляр объекта, полученный из пула объектов.
      /// </summary>
      public T Object
      {
        [DebuggerStepThrough] get => this.allocatedObject;
      }

      /// <summary>
      /// Очищает текущий объект и освобождает все использованные ресурсы. Экземпляр объекта, полученный из пула, будет возвращен обратно в пул.
      /// </summary>
      public void Dispose()
      {
        if (this.pool == null)
          return;
        this.pool.Release(this.allocatedObject);
        this.pool = (IObjectPool<T>) null;
        this.allocatedObject = default (T);
      }

      /// <summary>
      /// Возвращает признак, что ресурсы текущего объекта были освобождены. Использовать текущий объект больше нельзя.
      /// </summary>
      public bool IsDisposed
      {
        [DebuggerStepThrough] get => this.pool == null;
      }
    }
}
