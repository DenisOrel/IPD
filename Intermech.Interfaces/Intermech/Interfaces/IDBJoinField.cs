
// Type: Intermech.Interfaces.IDBJoinField
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>
    /// Интерфейс для управления полем по которому производится присоединение таблиц
    /// </summary>
    /// <remarks>В данный момент используется только для IDBRelationColection</remarks>
    public interface IDBJoinField
    {
      /// <summary>
      /// Имя поля по которому производится присоединение таблиц
      /// </summary>
      string JoinFieldName { get; set; }
    }
}
