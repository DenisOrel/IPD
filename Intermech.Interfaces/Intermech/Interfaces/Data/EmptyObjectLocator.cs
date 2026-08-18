
// Type: Intermech.Interfaces.Data.EmptyObjectLocator
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces.Data
{
    /// <summary>
    /// Реализует т.н. пустой алгоритм поиска объекта в базе IPS, он никогда ничего не находит.
    /// </summary>
    public sealed class EmptyObjectLocator : IObjectLocator
    {
      /// <summary>
      /// Игнорирует требование найти объект в базе IPS и возвращает признак неудачного поиска.
      /// </summary>
      /// <returns>Всегда содержит null</returns>
      public ObjectLocatorResult LocateObject() => (ObjectLocatorResult) null;
    }
}
