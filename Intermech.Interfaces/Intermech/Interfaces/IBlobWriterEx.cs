
// Type: Intermech.Interfaces.IBlobWriterEx
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    public interface IBlobWriterEx : IBlobWriter
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
      /// <param name="fixedSize">Если равно true, то размер блоба заранее известен и блоб автоматически закрывается после записи последнего байта.
      /// Иначе блоб нужно закрывать методом CloseBlob.</param>
      bool OpenBlob(BlobInformation blobInfo, bool onlyInfo, bool fixedSize);

      /// <summary>Закрывает запись блоба и записывает данные в базу</summary>
      /// <param name="realFileSize">Реальный размер двоичных данных, записанный в блоб. Размер упакованных данных устанавливается равным количеству байт, уже записанному в блоб методами WriteData.</param>
      void CloseBlob(long realFileSize);

      /// <summary>
      /// Метод перемещает блоб из текущего значения атрибута в файловый шкаф toStorageID
      /// </summary>
      void RemoveToStorage(long toStorageID);
    }
}
