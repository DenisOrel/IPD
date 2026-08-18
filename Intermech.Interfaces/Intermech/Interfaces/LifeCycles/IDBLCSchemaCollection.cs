
// Type: Intermech.Interfaces.LifeCycles.IDBLCSchemaCollection
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces.LifeCycles
{
    /// <summary>Список схем жизненных циклов объектов</summary>
    public interface IDBLCSchemaCollection
    {
      /// <summary>Создает новую схему ЖЦ и возвращает ее идентификатор</summary>
      int Create(DBLCSchemaProperties properties);

      /// <summary>
      /// Возвращает идентификатор схемы ЖЦ, установленной в системе по умолчанию
      /// </summary>
      int GetDefaultSchemaID();
    }
}
