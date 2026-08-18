
// Type: Intermech.Pools.IObjectPool`1
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml


namespace Intermech.Pools
{
    /// <summary>Интерфейс пула объектов.</summary>
    /// <typeparam name="T">Тип объектов в пуле</typeparam>
    public interface IObjectPool<T>
    {
      /// <summary>Выделяет объект из пула.</summary>
      /// <returns>Выделенный объект</returns>
      T Allocate();

      /// <summary>Возвращает объект обратно в пул.</summary>
      /// <param name="obj">Объект пула, выделенный ранее с помощью метода Allocate</param>
      void Release(T obj);

      /// <summary>Количество объектов в пуле, доступных для выделения.</summary>
      int IdleObjects { get; }
    }
}
