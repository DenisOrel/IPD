
// Type: Intermech.Interfaces.IDBSessionable
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>
    /// Интерфейс объекта, работающего в рамках пользовательской или системной сессии
    /// </summary>
    public interface IDBSessionable
    {
      /// <summary>Интерфейс пользовательской или системной сессии</summary>
      IUserSession Session { get; }
    }
}
