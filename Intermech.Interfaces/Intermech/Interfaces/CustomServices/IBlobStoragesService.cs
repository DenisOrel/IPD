
// Type: Intermech.Interfaces.CustomServices.IBlobStoragesService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces.CustomServices
{
    /// <summary>Интерфейс для работы со списком файловых шкафов</summary>
    public interface IBlobStoragesService
    {
      /// <summary>Вернуть массив описателей файловых шкафов</summary>
      /// <returns></returns>
      BlobStorageInfo[] GetStorages();
    }
}
