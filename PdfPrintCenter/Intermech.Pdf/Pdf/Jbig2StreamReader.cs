// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Jbig2StreamReader
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

#nullable disable
namespace Syncfusion.Pdf;

internal class Jbig2StreamReader
{
  internal int bytePointer;
  private int m_bitPointer = 7;
  private byte[] m_data;

  internal Jbig2StreamReader(byte[] data) => this.m_data = data;

  internal void ConsumeRemainingBits()
  {
    if (this.m_bitPointer == 7)
      return;
    this.ReadBits(this.m_bitPointer + 1);
  }

  internal bool Getfinished() => this.bytePointer == this.m_data.Length;

  internal void MovePointer(int ammount) => this.bytePointer += ammount;

  internal int ReadBit()
  {
    int num = ((int) this.ReadByte() & (int) (short) (1 << this.m_bitPointer)) >> this.m_bitPointer;
    --this.m_bitPointer;
    if (this.m_bitPointer == -1)
    {
      this.m_bitPointer = 7;
      return num;
    }
    this.MovePointer(-1);
    return num;
  }

  internal int ReadBits(int num)
  {
    int num1 = 0;
    for (int index = 0; index < num; ++index)
      num1 = num1 << 1 | this.ReadBit();
    return num1;
  }

  internal short ReadByte()
  {
    return (short) ((int) this.m_data[this.bytePointer++] & (int) byte.MaxValue);
  }

  internal void ReadByte(short[] buf)
  {
    for (int index = 0; index < buf.Length; ++index)
    {
      if (this.bytePointer < this.m_data.Length)
        buf[index] = (short) ((int) this.m_data[this.bytePointer++] & (int) byte.MaxValue);
    }
  }
}
