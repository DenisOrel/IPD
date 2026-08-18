
// Type: Intermech.Runtime.ComInterop.StreamLockBytes
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Runtime.ComInterop.ComTypes;
using System;
using System.IO;
using System.Runtime.InteropServices;


namespace Intermech.Runtime.ComInterop
{
    internal sealed class StreamLockBytes : ILockBytes
    {
      private static readonly System.Runtime.InteropServices.ComTypes.STATSTG statTemplate = StreamLockBytes.CreateStatTemplate();
      private readonly FileInfo fileInfo;
      private readonly Stream fileStream;

      public StreamLockBytes(FileInfo fileInfo, Stream st)
      {
        if (fileInfo == null)
          throw new ArgumentNullException(nameof (fileInfo));
        if (st == null)
          throw new ArgumentNullException(nameof (st));
        this.fileInfo = fileInfo;
        this.fileStream = st;
      }

      public void Stat(out System.Runtime.InteropServices.ComTypes.STATSTG stat, STATFLAG grfStatFlag)
      {
        stat = StreamLockBytes.statTemplate;
        if ((grfStatFlag & STATFLAG.STATFLAG_NONAME) == STATFLAG.STATFLAG_DEFAULT)
          stat.pwcsName = this.fileInfo.FullName;
        this.fileInfo.Refresh();
        stat.cbSize = this.fileStream.Length;
        stat.ctime = StreamLockBytes.DateTimeToFileTime(this.fileInfo.CreationTime);
        stat.mtime = StreamLockBytes.DateTimeToFileTime(this.fileInfo.LastWriteTime);
        stat.atime = StreamLockBytes.DateTimeToFileTime(this.fileInfo.LastAccessTime);
      }

      private static System.Runtime.InteropServices.ComTypes.FILETIME DateTimeToFileTime(DateTime dt)
      {
        long fileTime = dt.ToFileTime();
        return new System.Runtime.InteropServices.ComTypes.FILETIME()
        {
          dwHighDateTime = (int) (fileTime >> 32 /*0x20*/),
          dwLowDateTime = (int) (fileTime & (long) uint.MaxValue)
        };
      }

      public void ReadAt(long offset, IntPtr unmanagedBuffer, int bufferLength, IntPtr readResult)
      {
        byte[] numArray = new byte[bufferLength];
        this.fileStream.Position = offset;
        int num = this.fileStream.Read(numArray, 0, bufferLength);
        Marshal.Copy(numArray, 0, unmanagedBuffer, num);
        if (!(readResult != IntPtr.Zero))
          return;
        Marshal.WriteInt64(readResult, (long) num);
      }

      public void WriteAt(long offset, IntPtr unmanagedBuffer, int bufferLength, IntPtr writeResult)
      {
        byte[] numArray = new byte[bufferLength];
        Marshal.Copy(unmanagedBuffer, numArray, 0, bufferLength);
        this.fileStream.Position = offset;
        this.fileStream.Write(numArray, 0, bufferLength);
        if (!(writeResult != IntPtr.Zero))
          return;
        Marshal.WriteInt64(writeResult, (long) bufferLength);
      }

      public void Flush() => this.fileStream.Flush();

      public void SetSize(long newStreamLength) => this.fileStream.SetLength(newStreamLength);

      public void LockRegion(long offset, long lockLength, int lockType)
      {
        throw Marshal.GetExceptionForHR(-2147287039 /*0x80030001*/);
      }

      public void UnlockRegion(long offset, long lockLength, int lockType)
      {
        throw Marshal.GetExceptionForHR(-2147287039 /*0x80030001*/);
      }

      private static System.Runtime.InteropServices.ComTypes.STATSTG CreateStatTemplate()
      {
        return new System.Runtime.InteropServices.ComTypes.STATSTG()
        {
          pwcsName = string.Empty,
          type = 3,
          grfMode = 18,
          grfLocksSupported = 0,
          grfStateBits = 0,
          reserved = 0,
          clsid = Guid.Empty
        };
      }
    }
}
