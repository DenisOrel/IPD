
// Type: Intermech.Streams.ImStreamReader
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using Intermech.Remoting.Compression;
using System;
using System.IO;


namespace Intermech.Streams
{
    /// <summary>
    /// 
    /// </summary>
    public class ImStreamReader : MarshalByRefObject, IBlobReader, IDisposable
    {
      /// <summary>Размер блока передаваемых данных</summary>
      protected int _dataBlockSize;
      /// <summary>
      /// 
      /// </summary>
      protected Stream _stream;
      /// <summary>
      /// 
      /// </summary>
      protected BinaryReader _reader;

      /// <summary>
      /// 
      /// </summary>
      protected virtual void CheckForClosed()
      {
        BinaryReader reader = this._reader;
      }

      /// <summary>Конструктор</summary>
      /// <param name="stream"></param>
      public ImStreamReader(Stream stream) => this._stream = stream;

      /// <summary>Чтение информации из потока.</summary>
      /// <param name="dataBlockSize">Размер блоков при чтении</param>
      /// <remarks>
      /// Если dataBlockSize меньше 0, то не читается из содержимого.
      /// Если dataBlockSize=0, то размер блока равен размеру хранимых в потоке данных.</remarks>
      /// <returns>Информация о потоке (файле)</returns>
      public virtual BlobInformation OpenBlob(int dataBlockSize)
      {
        this.CheckForClosed();
        BlobInformation blobInformation = new BlobInformation();
        blobInformation.ArcMethod = ArcMethods.NotPacked;
        blobInformation.BlobID = -1L;
        blobInformation.FileName = string.Empty;
        blobInformation.ModifyDate = DateTime.UtcNow;
        blobInformation.Note = string.Empty;
        blobInformation.PackedFileSize = 0L;
        blobInformation.RealFileSize = 0L;
        if (this._stream != null)
          blobInformation.PackedFileSize = blobInformation.RealFileSize = this._stream.Length;
        if (dataBlockSize < 0)
          return blobInformation;
        this._dataBlockSize = dataBlockSize != 0 ? dataBlockSize : (blobInformation.PackedFileSize <= (long) int.MaxValue ? Convert.ToInt32(blobInformation.PackedFileSize) : int.MaxValue);
        if (this._stream != null)
          this._reader = new BinaryReader(this._stream);
        return blobInformation;
      }

      /// <summary>
      /// Читает следующий блок данных размером dataBlockSize. Если dataBlockSize==0,
      /// то размер блока берется заданный в OpenBlob. Если возвращенный блок имеет длину 0
      /// (или меньше dataBlockSize), то данные закончились.
      /// </summary>
      /// <param name="dataBlockSize"></param>
      /// <returns></returns>
      [RemotingCompression(false)]
      public virtual byte[] ReadDataBlock(int dataBlockSize)
      {
        BinaryReader reader = this._reader;
        if (this._stream == null)
          return new byte[0];
        byte[] numArray = this._reader.ReadBytes(dataBlockSize > 0 ? dataBlockSize : this._dataBlockSize);
        if (numArray.Length != 0 && this._stream.Position != this._stream.Length)
          return numArray;
        this.CloseBlob();
        return numArray;
      }

      /// <summary>Читает следующий блок данных</summary>
      /// <returns></returns>
      [RemotingCompression(false)]
      public virtual byte[] ReadDataBlock() => this.ReadDataBlock(0);

      /// <summary>Закрывает поток и освобождает занятые ресурсы</summary>
      public virtual void CloseBlob()
      {
        if (this._reader != null)
        {
          this._reader.Close();
          this._reader = (BinaryReader) null;
        }
        else if (this._stream != null)
          this._stream.Close();
        this._stream = (Stream) null;
      }

      /// <summary>
      /// Вовзращает статус блоба (открыт для чтения/записи/закрыт)
      /// </summary>
      public BlobAttributeStates BlobState => throw new NotImplementedException();

      /// <summary>Освобождает занятые ресурсы</summary>
      public virtual void Dispose() => this.CloseBlob();
    }
}
