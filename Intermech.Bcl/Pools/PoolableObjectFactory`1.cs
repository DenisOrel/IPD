
// Type: Intermech.Pools.PoolableObjectFactory`1
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml


namespace Intermech.Pools
{
    /// <summary>Реализует фабрику объектов, размещенных в пуле.</summary>
    /// <typeparam name="T">Тип объектов в пуле</typeparam>
    public abstract class PoolableObjectFactory<T>
    {
      /// <summary>
      /// Создает экземпляр объект. Метод используется при недостатке объектов в пуле для пополнения пула.
      /// </summary>
      /// <returns>Экземпляр объекта</returns>
      public abstract T CreateObject();

      /// <summary>
      /// Активирует объект после извлечения из пула перед возвратом клиенту пула.
      /// </summary>
      /// <param name="obj">Экземпляр объекта</param>
      public virtual void ActivateObject(T obj)
      {
      }

      /// <summary>Деактивирует и очищает объект перед возвратом в пул.</summary>
      /// <param name="obj">Экземпляр объекта</param>
      public virtual void DeactivateObject(T obj)
      {
      }
    }
}
