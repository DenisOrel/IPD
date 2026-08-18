
// Type: Intermech.Interfaces.IDeletable
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>Реализуется объектами, которые можно удалять</summary>
    public interface IDeletable
    {
      /// <summary>Удалить реализующий объект</summary>
      /// <param name="DeleteMode">Параметр для указания доп. информации по удалению.
      /// Если не нужен в конкретной реализации, то туда будут передавать 0.</param>
      /// <returns>Зарезервировано</returns>
      int Delete(long DeleteMode);
    }
}
