
// Type: Intermech.Interfaces.IBlobReader
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Remoting.Compression;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Интерфейс чтения BLOB-полей и файлов на клиентской стороне IPS
    /// </summary>
    public interface IBlobReader
    {
      /// <summary>
      /// Открывает BLOB-поле (файл) и возвращает блок с информацией о нем.
      /// dataBlockSize - размер блоков, которые он будет отдавать при чтении.
      /// Если dataBlockSize меньше 0, то читается только информация, а блоб-атрибут
      /// остается закрытым для чтения. Если dataBlockSize=0, то размер блока
      /// равен размеру реально хранимых в блобе данных.
      /// </summary>
      /// <param name="dataBlockSize">Размер блоков, которые BLOB-поле будет отдавать при чтении</param>
      /// <returns>Информация о BLOB-поле</returns>
      BlobInformation OpenBlob(int dataBlockSize);

      /// <summary>
      /// Читает следующий блок данных размером dataBlockSize. Если dataBlockSize==0,
      /// то размер блока берется заданный в OpenBlob. Если возвращенный блок имеет длину 0
      /// (или меньше dataBlockSize), то данные закончились и блоб закрыт.
      /// </summary>
      /// <param name="dataBlockSize">Размер блока данных для чтения</param>
      /// <returns>Загруженные данные</returns>
      [RemotingCompression(false)]
      byte[] ReadDataBlock(int dataBlockSize);

      /// <summary>Читает следующий блок данных</summary>
      /// <returns>Загруженные данные</returns>
      [RemotingCompression(false)]
      byte[] ReadDataBlock();

      /// <summary>Закрывает BLOB-поле и освобождает занятые ресурсы</summary>
      void CloseBlob();

      /// <summary>
      /// Вовзращает статус блоба (открыт для чтения/записи/закрыт)
      /// </summary>
      BlobAttributeStates BlobState { get; }
    }
}
