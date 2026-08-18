
// Type: Intermech.IO.IByteBufferPool
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml


namespace Intermech.IO
{
    /// <summary>Buffer's pool interface</summary>
    public interface IByteBufferPool
    {
      /// <summary>Support buffer pool's cache</summary>
      bool SupportCache { get; }

      /// <summary>Get buffer length</summary>
      int BufferLength { get; }

      /// <summary>Get buffer from pool</summary>
      /// <returns></returns>
      byte[] GetBuffer();

      /// <summary>Return buffer to bool</summary>
      /// <param name="buffer"></param>
      void ReturnBuffer(byte[] buffer);
    }
}
