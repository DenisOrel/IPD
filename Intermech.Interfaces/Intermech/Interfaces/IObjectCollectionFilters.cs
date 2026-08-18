
// Type: Intermech.Interfaces.IObjectCollectionFilters
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Collections.Specialized;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Интерфейс позволяет передавать в запросе к коллекциям объектов
    /// какие-то дополнительные поля
    /// </summary>
    public interface IObjectCollectionFilters
    {
      /// <summary>
      /// В коллекцию PluginsData можно сохранять свою информацию в виде сериализуемых пар
      /// значений [Ключ] = [Значение]. Данная коллекция будет доступна на серверной стороне
      /// в Select у коллекции объектов.
      /// </summary>
      HybridDictionary PluginsData { get; }
    }
}
