
// Type: Intermech.Interfaces.IBlobWriter
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Remoting.Compression;


namespace Intermech.Interfaces
{
    /// <summary>Интерфейс записи BLOB-полей и файлов</summary>
    public interface IBlobWriter
    {
      /// <summary>
      /// Открывает BLOB-поле для записи и инициализирует его информацией из блока blobInfo.
      /// Если onlyInfo=true, то модифицируется только сопутствующая информация (filename
      /// и Note).
      /// Ф-ция возвращает true, если BLOB-поле ждет данные для записи, иначе BLOB-поле сразу
      /// закрывается (если потребовали записать 0 байт)
      /// </summary>
      /// <param name="blobInfo">Информация о BLOB-поле</param>
      /// <param name="onlyInfo">Если равно true, то модифицируется только сопутствующая информация</param>
      /// <returns>true, если BLOB-поле ждет данные для записи, иначе BLOB-поле сразу закрывается</returns>
      bool OpenBlob(BlobInformation blobInfo, bool onlyInfo);

      /// <summary>
      /// Дописывает блок данных в BLOB-поле. Если возвращает true, то запись BLOB-поля не завершена
      /// и ожидается продолжение данных, иначе записывает данные в BLOB-поле. Если data будет
      /// содержать данные более размера BLOB-поля, то будет выдано исключение.
      /// </summary>
      /// <param name="data">Записываемые данные</param>
      /// <returns>Если возвращает true, то запись BLOB-поля не завершена и ожидается продолжение данных</returns>
      [RemotingCompression(false)]
      bool WriteDataBlock(byte[] data);

      /// <summary>
      /// Аналогичен WriteDataBlock(byte[] data); но позволяет исользовать себя в комбинации с MemoryStream.GetBuffer()
      /// </summary>
      /// <param name="data">Записываемые данные</param>
      /// <param name="index">Индекс с которого начать запись</param>
      /// <param name="length">Длина записываемых данных</param>
      /// <returns></returns>
      [RemotingCompression(false)]
      bool WriteDataBlockEx(byte[] data, int index, int length);

      /// <summary>Закрывает BLOB-поле и отменяет запись данных.</summary>
      void CancelWrite();

      /// <summary>
      /// Вовзращает статус блоба (открыт для чтения/записи/закрыт)
      /// </summary>
      BlobAttributeStates BlobState { get; }
    }
}
