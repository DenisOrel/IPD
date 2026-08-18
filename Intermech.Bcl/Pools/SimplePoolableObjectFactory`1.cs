
// Type: Intermech.Pools.SimplePoolableObjectFactory`1
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Pools
{
    /// <summary>
    /// Реализует простейшую фабрику объектов, размещенных в пуле.
    /// </summary>
    internal sealed class SimplePoolableObjectFactory<T> : PoolableObjectFactory<T>
    {
      private Func<T> createFunction;

      /// <summary>Создает объект.</summary>
      /// <param name="createFunction">Функция для создания экземпляров объектов</param>
      /// <exception cref="T:ArgumentNullException">Параметр не должен быть равен null</exception>
      public SimplePoolableObjectFactory(Func<T> createFunction)
      {
        this.createFunction = createFunction != null ? createFunction : throw new ArgumentNullException(nameof (createFunction));
      }

      /// <summary>
      /// Создает экземпляр объект. Метод используется при недостатке объектов в пуле для пополнения пула.
      /// </summary>
      /// <returns>Экземпляр объекта</returns>
      public override T CreateObject() => this.createFunction();
    }
}
