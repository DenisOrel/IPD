
// Type: Intermech.Interfaces.Data.CompositeObjectLocator
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech.Interfaces.Data
{
    /// <summary>
    /// Реализует составной алгоритм поиска объекта в базе IPS, который позволяет объединить в цепочку
    /// несколько других алгоритмов.
    /// </summary>
    public sealed class CompositeObjectLocator : IObjectLocator
    {
      private readonly IEnumerable<IObjectLocator> locators;

      /// <summary>Создает объект.</summary>
      /// <param name="locators">Массив алгоритмов поиска объекта</param>
      /// <exception cref="T:System.ArgumentNullException">Ссылка на массив алгоритмов не может быть null</exception>
      public CompositeObjectLocator(params IObjectLocator[] locators)
      {
        this.locators = locators != null ? (IEnumerable<IObjectLocator>) locators : throw new ArgumentNullException(nameof (locators));
      }

      /// <summary>Создает объект.</summary>
      /// <param name="locators">Перечислитель цепочки алгоритмов поиска объекта</param>
      /// <exception cref="T:System.ArgumentNullException">Ссылка на перечислитель алгоритмов не может быть null</exception>
      public CompositeObjectLocator(IEnumerable<IObjectLocator> locators)
      {
        this.locators = locators != null ? locators : throw new ArgumentNullException(nameof (locators));
      }

      /// <summary>
      /// Последовательно выполняет цепочку алгоритмов поиска объекта.
      /// </summary>
      /// <returns>Описатель найденного объекта в базе IPS или null, если объект не был найден</returns>
      public ObjectLocatorResult LocateObject()
      {
        foreach (IObjectLocator locator in this.locators)
        {
          ObjectLocatorResult objectLocatorResult = locator.LocateObject();
          if (objectLocatorResult != null)
            return objectLocatorResult;
        }
        return (ObjectLocatorResult) null;
      }
    }
}
