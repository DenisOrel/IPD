
// Type: Intermech.Interfaces.CustomServices.BlobStorageInfo
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.CustomServices
{
    /// <summary>Класс с описание файлового шкафа</summary>
    [Serializable]
    public class BlobStorageInfo
    {
      /// <summary>Имя шкафа</summary>
      public string StorageName { get; private set; }

      /// <summary>Идентификатор шкафа</summary>
      public long StorageID { get; private set; }

      /// <summary>Тип шкафа</summary>
      public string StorageType { get; private set; }

      public BlobStorageInfo(long storageID, string storageName, string storageType)
      {
        this.StorageID = storageID;
        this.StorageName = storageName;
        this.StorageType = storageType;
      }
    }
}
