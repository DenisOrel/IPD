
// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.PreviewHandlerViewer.ManagedIStream
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security;


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.PreviewHandlerViewer;

public class ManagedIStream : IStream
{
  private Stream _stream;

  /// <summary>Constructor</summary>
  public ManagedIStream(Stream stream)
  {
    this._stream = stream ?? throw new ArgumentNullException("ioStream");
  }

  /// <summary>
  /// Read at most bufferSize bytes into buffer and return the effective
  /// number of bytes read in bytesReadPtr (unless null).
  /// </summary>
  /// <remarks>
  /// mscorlib disassembly shows the following MarshalAs parameters
  /// void Read([Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex=1)] byte[] pv, int cb, IntPtr pcbRead);
  /// This means marshaling code will have found the size of the array buffer in the parameter bufferSize.
  /// </remarks>
  /// <securitynote>
  ///      Critical: calls Marshal.WriteInt32 which LinkDemands, takes pointers as input
  /// </securitynote>
  [SecurityCritical]
  void IStream.Read(byte[] buffer, int bufferSize, IntPtr bytesReadPtr)
  {
    int val = this._stream.Read(buffer, 0, bufferSize);
    if (!(bytesReadPtr != IntPtr.Zero))
      return;
    Marshal.WriteInt32(bytesReadPtr, val);
  }

  /// <summary>Move the stream pointer to the specified position.</summary>
  /// <remarks>
  /// System.IO.stream supports searching past the end of the stream, like
  /// OLE streams.
  /// newPositionPtr is not an out parameter because the method is required
  /// to accept NULL pointers.
  /// </remarks>
  /// <securitynote>
  ///      Critical: calls Marshal.WriteInt64 which LinkDemands, takes pointers as input
  /// </securitynote>
  [SecurityCritical]
  void IStream.Seek(long offset, int origin, IntPtr newPositionPtr)
  {
    SeekOrigin origin1;
    switch (origin)
    {
      case 0:
        origin1 = SeekOrigin.Begin;
        break;
      case 1:
        origin1 = SeekOrigin.Current;
        break;
      case 2:
        origin1 = SeekOrigin.End;
        break;
      default:
        throw new ArgumentOutOfRangeException(nameof (origin));
    }
    long val = this._stream.Seek(offset, origin1);
    if (!(newPositionPtr != IntPtr.Zero))
      return;
    Marshal.WriteInt64(newPositionPtr, val);
  }

  /// <summary>Sets stream's size.</summary>
  void IStream.SetSize(long libNewSize) => this._stream.SetLength(libNewSize);

  /// <summary>Obtain stream stats.</summary>
  /// <remarks>
  /// STATSG has to be qualified because it is defined both in System.Runtime.InteropServices and
  /// System.Runtime.InteropServices.ComTypes.
  /// The STATSTG structure is shared by streams, storages and byte arrays. Members irrelevant to streams
  /// or not available from System.IO.Stream are not returned, which leaves only cbSize and grfMode as
  /// meaningful and available pieces of information.
  /// grfStatFlag is used to indicate whether the stream name should be returned and is ignored because
  /// this information is unavailable.
  /// </remarks>
  void IStream.Stat(out System.Runtime.InteropServices.ComTypes.STATSTG streamStats, int grfStatFlag)
  {
    streamStats = new System.Runtime.InteropServices.ComTypes.STATSTG();
    streamStats.type = 2;
    streamStats.cbSize = this._stream.Length;
    streamStats.grfMode = 0;
    if (this._stream.CanRead && this._stream.CanWrite)
      streamStats.grfMode |= 2;
    else if (this._stream.CanRead)
    {
      streamStats.grfMode |= 0;
    }
    else
    {
      if (!this._stream.CanWrite)
        throw new IOException("");
      streamStats.grfMode |= 1;
    }
  }

  /// <summary>Write at most bufferSize bytes from buffer.</summary>
  /// <securitynote>
  ///      Critical: calls Marshal.WriteInt32 which LinkDemands, takes pointers as input
  /// </securitynote>
  [SecurityCritical]
  void IStream.Write(byte[] buffer, int bufferSize, IntPtr bytesWrittenPtr)
  {
    this._stream.Write(buffer, 0, bufferSize);
    if (!(bytesWrittenPtr != IntPtr.Zero))
      return;
    Marshal.WriteInt32(bytesWrittenPtr, bufferSize);
  }

  /// <summary>Create a clone.</summary>
  /// <remarks>Not implemented.</remarks>
  void IStream.Clone(out IStream streamCopy)
  {
    streamCopy = (IStream) null;
    throw new NotSupportedException();
  }

  /// <summary>
  /// Read at most bufferSize bytes from the receiver and write them to targetStream.
  /// </summary>
  /// <remarks>Not implemented.</remarks>
  void IStream.CopyTo(
    IStream targetStream,
    long bufferSize,
    IntPtr buffer,
    IntPtr bytesWrittenPtr)
  {
    throw new NotSupportedException();
  }

  /// <summary>Commit changes.</summary>
  /// <remarks>Only relevant to transacted streams.</remarks>
  void IStream.Commit(int flags)
  {
  }

  /// <summary>Lock at most byteCount bytes starting at offset.</summary>
  /// <remarks>Not supported by System.IO.Stream.</remarks>
  void IStream.LockRegion(long offset, long byteCount, int lockType)
  {
    throw new NotSupportedException();
  }

  /// <summary>Undo writes performed since last Commit.</summary>
  /// <remarks>Relevant only to transacted streams.</remarks>
  void IStream.Revert() => throw new NotSupportedException();

  /// <summary>Unlock the specified region.</summary>
  /// <remarks>Not supported by System.IO.Stream.</remarks>
  void IStream.UnlockRegion(long offset, long byteCount, int lockType)
  {
    throw new NotSupportedException();
  }
}
