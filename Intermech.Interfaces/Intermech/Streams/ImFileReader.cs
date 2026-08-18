
// Type: Intermech.Streams.ImFileReader
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using System.IO;


namespace Intermech.Streams
{
    /// <summary>
    /// 
    /// </summary>
    public class ImFileReader : ImStreamReader
    {
      /// <summary>Конструктор</summary>
      /// <param name="fileStream"></param>
      public ImFileReader(FileStream fileStream)
        : base((Stream) fileStream)
      {
      }

      /// <summary>Конструктор</summary>
      /// <param name="fileName"></param>
      public ImFileReader(string fileName)
        : base((Stream) null)
      {
        this._stream = (Stream) new FileStream(fileName, FileMode.Open, FileAccess.Read);
      }

      /// <summary>Сохранить в файл</summary>
      /// <param name="filename"></param>
      /// <param name="aDataBlockSize">размер блока для чтения</param>
      public static void SaveToFile(ImStreamReader imStreamReader, string filename, int aDataBlockSize)
      {
        if (imStreamReader == null)
          return;
        long packedFileSize = imStreamReader.OpenBlob(aDataBlockSize).PackedFileSize;
        using (FileStream fileStream = new FileStream(filename, FileMode.Create))
        {
          long num = packedFileSize % (long) aDataBlockSize == 0L ? packedFileSize / (long) aDataBlockSize : packedFileSize / (long) aDataBlockSize + 1L;
          for (long index = 0; index < num; ++index)
          {
            byte[] buffer = imStreamReader.ReadDataBlock(aDataBlockSize);
            fileStream.Write(buffer, 0, buffer.Length);
          }
        }
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="dataBlockSize"></param>
      /// <returns></returns>
      public override BlobInformation OpenBlob(int dataBlockSize)
      {
        BlobInformation blobInformation = base.OpenBlob(dataBlockSize);
        if (this._stream is FileStream stream)
          blobInformation.FileName = stream.Name;
        return blobInformation;
      }
    }
}
