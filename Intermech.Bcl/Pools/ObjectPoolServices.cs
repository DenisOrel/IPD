
// Type: Intermech.Pools.ObjectPoolServices
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Pools
{
    public static class ObjectPoolServices
    {
      public static PoolableObjectFactory<T> CreateObjectFactory<T>(Func<T> createFunction)
      {
        return (PoolableObjectFactory<T>) new SimplePoolableObjectFactory<T>(createFunction);
      }

      public static IObjectPool<T> Synchronized<T>(this IObjectPool<T> pool)
      {
        return pool != null ? (IObjectPool<T>) new ObjectPoolSyncWrapper<T>(pool) : throw new ArgumentNullException(nameof (pool));
      }

      public static ObjectPoolScope<T> AllocateInScope<T>(this IObjectPool<T> pool)
      {
        T allocatedObject = pool != null ? pool.Allocate() : throw new ArgumentNullException(nameof (pool));
        return new ObjectPoolScope<T>(pool, allocatedObject);
      }
    }
}
