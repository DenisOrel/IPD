// Decompiled with JetBrains decompiler
// Type: Intermech.TwainScanner.StreamString
// Assembly: Intermech.TwainScanner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0CEE3C76-D3AF-4F98-AB07-F18794839283
// Assembly location: D:\IPS\Client\Intermech.TwainScanner.exe
// XML documentation location: D:\IPS\Client\Intermech.TwainScanner.xml

using System.IO;
using System.Text;

#nullable disable
namespace Intermech.TwainScanner;

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
