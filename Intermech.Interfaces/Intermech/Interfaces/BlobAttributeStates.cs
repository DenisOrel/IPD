
// Type: Intermech.Interfaces.BlobAttributeStates
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>
    /// Перечисления состояний, которые может принимать атрибут с двоичными данными.
    /// (закрыт, открыт для чтения данных, открыт для записи данных).
    /// </summary>
    public enum BlobAttributeStates
    {
      /// <summary>BLOB-поле закрыто</summary>
      Closed,
      /// <summary>BLOB-поле открыто для чтения</summary>
      OpenedForRead,
      /// <summary>BLOB-поле открыто для записи</summary>
      OpenedForWrite,
    }
}
