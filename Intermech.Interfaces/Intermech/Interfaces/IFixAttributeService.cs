
// Type: Intermech.Interfaces.IFixAttributeService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>для удаления нечитаемых файлов</summary>
    public interface IFixAttributeService
    {
      /// <summary>
      ///  удалить нечитаемый файл, если это последнее значение - очистить его
      /// </summary>
      /// <param name="blobInfo">описание нечитаемого файла</param>
      /// <param name="sessionGuid">Гуид сессии пользователя</param>
      void DeleteBlob(InvalidBlobInfo blobInfo, Guid sessionGuid);

      /// <summary>Уничтожить версию объекта</summary>
      /// <param name="objectID"></param>
      /// <param name="sessionGuid">Гуид сессии пользователя</param>
      void PugreObject(long objectID, Guid sessionGuid);
    }
}
