// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.ScanDocums.StreamString
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using System.IO;
using System.Text;

#nullable disable
namespace Intermech.Archives.ScanDocums;

public class StreamString
{
  private Stream ioStream;
  private UnicodeEncoding streamEncoding;

  public StreamString(Stream ioStream)
  {
    this.ioStream = ioStream;
    this.streamEncoding = new UnicodeEncoding();
  }

  public string ReadString()
  {
    int count = this.ioStream.ReadByte() * 256 /*0x0100*/ + this.ioStream.ReadByte();
    byte[] numArray = new byte[count];
    this.ioStream.Read(numArray, 0, count);
    return this.streamEncoding.GetString(numArray);
  }

  public int WriteString(string outString)
  {
    byte[] bytes = this.streamEncoding.GetBytes(outString);
    int count = bytes.Length;
    if (count > (int) ushort.MaxValue)
      count = (int) ushort.MaxValue;
    this.ioStream.WriteByte((byte) (count / 256 /*0x0100*/));
    this.ioStream.WriteByte((byte) (count & (int) byte.MaxValue));
    this.ioStream.Write(bytes, 0, count);
    this.ioStream.Flush();
    return bytes.Length + 2;
  }
}
