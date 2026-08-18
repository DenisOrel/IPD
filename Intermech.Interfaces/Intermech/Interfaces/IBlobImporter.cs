
// Type: Intermech.Interfaces.IBlobImporter
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Интерфейс для импорта двоичных данных напрямую в файловые шкафы.
    /// </summary>
    public interface IBlobImporter
    {
      /// <summary>Добавляет в активный шкаф блоб</summary>
      /// <param name="sessionGuid">Гуид добавляющей сессии.</param>
      /// <param name="blobInfo">Информация о блобе.</param>
      /// <returns>Идентификатор блоба.</returns>
      long AddBlob(Guid sessionGuid, BlobInformation4Import blobInfo);
    }
}
