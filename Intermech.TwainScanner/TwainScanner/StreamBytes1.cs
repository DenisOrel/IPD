// Decompiled with JetBrains decompiler
// Type: Intermech.TwainScanner.StreamBytes1
// Assembly: Intermech.TwainScanner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0CEE3C76-D3AF-4F98-AB07-F18794839283
// Assembly location: D:\IPS\Client\Intermech.TwainScanner.exe
// XML documentation location: D:\IPS\Client\Intermech.TwainScanner.xml

using System.IO;

#nullable disable
namespace Intermech.TwainScanner;

public class StreamBytes1
{
  private Stream ioStream;

  public StreamBytes1(Stream ioStream) => this.ioStream = ioStream;

  public byte[] ReadString()
  {
    int count = this.ioStream.ReadByte() * 256 /*0x0100*/ + this.ioStream.ReadByte();
    byte[] buffer = new byte[count];
    this.ioStream.Read(buffer, 0, count);
    return buffer;
  }

  public int WriteString(byte[] outBuffer)
  {
    int count = outBuffer.Length;
    if (count > (int) ushort.MaxValue)
      count = (int) ushort.MaxValue;
    this.ioStream.WriteByte((byte) (count / 256 /*0x0100*/));
    this.ioStream.WriteByte((byte) (count & (int) byte.MaxValue));
    this.ioStream.Write(outBuffer, 0, count);
    this.ioStream.Flush();
    return outBuffer.Length + 2;
  }
}
