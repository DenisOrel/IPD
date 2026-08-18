
// Type: Intermech.IO.Compression.StreamPackerPool
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech.IO.Compression
{
    /// <summary>
    /// Реализует пул упаковщиков, позволяющих за одну операцию запаковать или распаковать целый поток (System.IO.Stream). Реализация пула является thread safe.
    /// </summary>
    public sealed class StreamPackerPool
    {
      private readonly LinkedList<StreamPacker> poolItems;
      private readonly int maxCapacity;
      private readonly Func<StreamPacker> createPackerFunction;

      /// <summary>Создает пул упаковщиков.</summary>
      /// <param name="initialCapacity">Начальное количество упаковщиков</param>
      /// <param name="maxCapacity">Максимальное количество упаковщиков</param>
      /// <param name="createPackerFunction">Функция создания упаковщика</param>
      public StreamPackerPool(
        int initialCapacity,
        int maxCapacity,
        Func<StreamPacker> createPackerFunction)
      {
        if (initialCapacity < 0)
          throw new ArgumentOutOfRangeException(nameof (initialCapacity));
        if (maxCapacity < initialCapacity || maxCapacity == 0)
          throw new ArgumentOutOfRangeException(nameof (maxCapacity));
        if (createPackerFunction == null)
          throw new ArgumentNullException(nameof (createPackerFunction));
        this.maxCapacity = maxCapacity;
        this.createPackerFunction = createPackerFunction;
        this.poolItems = new LinkedList<StreamPacker>();
        for (int index = 0; index < initialCapacity; ++index)
          this.poolItems.AddFirst(createPackerFunction());
      }

      /// <summary>
      /// Выделяет упаковщик из пула. После завершения использования упаковщика его следует вернуть в пул методом Release().
      /// </summary>
      /// <returns>Объект упаковщика</returns>
      public StreamPacker Allocate()
      {
        StreamPacker streamPacker = (StreamPacker) null;
        lock (this.poolItems)
        {
          if (this.poolItems.Count > 0)
          {
            streamPacker = this.poolItems.First.Value;
            this.poolItems.RemoveFirst();
          }
        }
        if (streamPacker == null)
          streamPacker = this.createPackerFunction();
        return streamPacker;
      }

      /// <summary>
      /// Возвращает упаковщик в пул. Если количество упаковщиков в пуле максимальное, то возвращаемый упаковщик будет просто отброшен.
      /// </summary>
      /// <param name="packer">Объект упаковщика</param>
      public void Release(StreamPacker packer)
      {
        if (packer == null)
          throw new ArgumentNullException(nameof (packer));
        lock (this.poolItems)
        {
          if (this.poolItems.Count >= this.maxCapacity)
            return;
          this.poolItems.AddFirst(packer);
        }
      }
    }
}
