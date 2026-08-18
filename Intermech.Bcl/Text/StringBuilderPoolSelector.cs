
// Type: Intermech.Text.StringBuilderPoolSelector
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Pools;
using System;
using System.Text;


namespace Intermech.Text
{
    /// <summary>
    /// Обеспечивает выбора пула StringBuilder в зависимости от затребованной емкости. Реализация не является thread safe.
    /// </summary>
    public class StringBuilderPoolSelector
    {
      private IObjectPool<StringBuilder> builderPool_016;
      private IObjectPool<StringBuilder> builderPool_128;
      private IObjectPool<StringBuilder> builderPool_512;
      private IObjectPool<StringBuilder> builderPool_big;

      /// <summary>Создает объект.</summary>
      public StringBuilderPoolSelector()
      {
        this.builderPool_016 = StringBuilderPoolSelector.CreatePool(4, 16 /*0x10*/, true);
        this.builderPool_128 = StringBuilderPoolSelector.CreatePool(4, 128 /*0x80*/, true);
        this.builderPool_512 = StringBuilderPoolSelector.CreatePool(2, 512 /*0x0200*/, true);
        this.builderPool_big = StringBuilderPoolSelector.CreatePool(2, 2048 /*0x0800*/, false);
      }

      private static IObjectPool<StringBuilder> CreatePool(
        int minPoolCapacity,
        int textCapacity,
        bool limitTextCapacity)
      {
        return (IObjectPool<StringBuilder>) new StackPool<StringBuilder>(minPoolCapacity, (PoolableObjectFactory<StringBuilder>) new StringBuilderPoolableFactory(textCapacity, limitTextCapacity));
      }

      /// <summary>Выделяет StringBuilder из пула.</summary>
      /// <returns>Динамическая область видимости, содержащая выделенный объект</returns>
      public ObjectPoolScope<StringBuilder> Allocate()
      {
        return this.builderPool_128.AllocateInScope();
      }

      /// <summary>Выделяет StringBuilder из пула.</summary>
      /// <param name="capacity">Начальная емкость для StringBuilder</param>
      /// <returns>Динамическая область видимости, содержащая выделенный объект</returns>
      /// <exception cref="T:ArgumentOutOfRangeException">Параметр <paramref name="capacity" /> не должен быть отрицательным числом</exception>
      public ObjectPoolScope<StringBuilder> Allocate(int capacity)
      {
        return capacity >= 0 ? this.SelectPool(capacity).AllocateInScope() : throw new ArgumentOutOfRangeException(nameof (capacity));
      }

      private IObjectPool<StringBuilder> SelectPool(int capacity)
      {
        if (capacity <= 16 /*0x10*/)
          return this.builderPool_016;
        if (capacity <= 128 /*0x80*/)
          return this.builderPool_128;
        return capacity <= 512 /*0x0200*/ ? this.builderPool_512 : this.builderPool_big;
      }
    }
}
