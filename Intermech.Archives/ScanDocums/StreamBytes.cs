// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.ScanDocums.StreamBytes
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using System;
using System.IO;

#nullable disable
namespace Intermech.Archives.ScanDocums;

public class StreamBytes
{
  private Stream ioStream;
  private int Len = (int) ushort.MaxValue;

  public StreamBytes(Stream ioStream) => this.ioStream = ioStream;

  public byte[] ReadString()
  {
    MemoryStream memoryStream = new MemoryStream();
    for (int index = 1; index == 1; index = this.ioStream.ReadByte())
    {
      byte[] buffer = new byte[this.Len];
      int count = this.ioStream.Read(buffer, 0, this.Len);
      memoryStream.Write(buffer, 0, count);
    }
    return memoryStream.ToArray();
  }

  public void ReadString(Stream stream)
  {
    for (int index = 1; index == 1; index = this.ioStream.ReadByte())
    {
      byte[] buffer = new byte[this.Len];
      int count = this.ioStream.Read(buffer, 0, this.Len);
      stream.Write(buffer, 0, count);
    }
  }

  public void WriteString(Stream stream)
  {
    bool flag = true;
    while (flag)
    {
      byte[] buffer = new byte[this.Len];
      int count = stream.Read(buffer, 0, this.Len);
      this.ioStream.Write(buffer, 0, count);
      if (count > 0)
      {
        this.ioStream.WriteByte((byte) 1);
      }
      else
      {
        this.ioStream.WriteByte((byte) 0);
        flag = false;
      }
    }
    this.ioStream.Flush();
  }

  public void WriteString(byte[] outBuffer)
  {
    new MemoryStream(outBuffer).Position = 0L;
    int length1 = outBuffer.Length;
    int sourceIndex = 0;
    while (length1 > 0)
    {
      int length2 = length1;
      if (length2 > this.Len)
        length2 = this.Len;
      byte[] numArray = new byte[length2];
      Array.Copy((Array) outBuffer, sourceIndex, (Array) numArray, 0, length2);
      this.ioStream.Write(numArray, 0, length2);
      sourceIndex += length2;
      length1 -= length2;
      if (length1 > 0)
        this.ioStream.WriteByte((byte) 1);
      else
        this.ioStream.WriteByte((byte) 0);
    }
    this.ioStream.Flush();
  }
}
