
// Type: Intermech.Interfaces.Objects.FileStorageInfo
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.Objects
{
    /// <summary>
    /// Структура для получения с сервера информации о файловом шкафу
    /// </summary>
    [Serializable]
    public struct FileStorageInfo(long filesCount, long realFilesSize, long packedFilesSize)
    {
      /// <summary>Количество файлов в файловом шкафу</summary>
      public long FilesCount = filesCount;
      /// <summary>Суммарный реальный размер файлов в шкафу</summary>
      public long RealFilesSize = realFilesSize;
      /// <summary>Суммарный запакованный размер файлов в шкафу</summary>
      public long PackedFilesSize = packedFilesSize;
    }
}
