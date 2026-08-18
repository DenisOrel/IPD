
// Type: Intermech.Interfaces.Data.IObjectLocator
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces.Data
{
    /// <summary>
    /// Позволяет реализовать алгоритм поиска объекта в базе IPS.
    /// </summary>
    public interface IObjectLocator
    {
      /// <summary>Выполняет поиск объекта в базе IPS.</summary>
      /// <returns>Описатель найденного объекта в базе IPS или null, если объект не был найден</returns>
      ObjectLocatorResult LocateObject();
    }
}
