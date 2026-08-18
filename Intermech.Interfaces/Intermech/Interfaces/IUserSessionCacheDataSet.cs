
// Type: Intermech.Interfaces.IUserSessionCacheDataSet
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Data;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Интерфейс пользовательской сессии для доступа к копии кэша метаданных.
    /// </summary>
    public interface IUserSessionCacheDataSet
    {
      /// <summary>Таблицы кэша метаданных</summary>
      DataSet CacheDataSet { get; }
    }
}
