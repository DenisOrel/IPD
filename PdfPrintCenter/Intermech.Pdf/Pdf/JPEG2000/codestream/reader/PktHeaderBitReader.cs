// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.JPEG2000.codestream.reader.PktHeaderBitReader
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.JPEG2000.io;
using System.IO;

#nullable disable
namespace Syncfusion.Pdf.JPEG2000.codestream.reader;

internal class PktHeaderBitReader
{
  internal MemoryStream bais;
  internal int bbuf;
  internal int bpos;
  internal JPXRandomAccessStream in_Renamed;
  internal int nextbbuf;
  internal bool usebais;

  internal PktHeaderBitReader(JPXRandomAccessStream in_Renamed)
  {
    this.in_Renamed = in_Renamed;
    this.usebais = false;
  }

  internal PktHeaderBitReader(MemoryStream bais)
  {
    this.bais = bais;
    this.usebais = true;
  }

  internal int readBit()
  {
    if (this.bpos == 0)
    {
      if (this.bbuf != (int) byte.MaxValue)
      {
        this.bbuf = !this.usebais ? (int) this.in_Renamed.read() : this.bais.ReadByte();
        this.bpos = 8;
        if (this.bbuf == (int) byte.MaxValue)
          this.nextbbuf = !this.usebais ? (int) this.in_Renamed.read() : this.bais.ReadByte();
      }
      else
      {
        this.bbuf = this.nextbbuf;
        this.bpos = 7;
      }
    }
    return this.bbuf >> --this.bpos & 1;
  }

  internal int readBits(int n)
  {
    if (n <= this.bpos)
      return this.bbuf >> (this.bpos -= n) & (1 << n) - 1;
    int num1 = 0;
    do
    {
      int num2 = num1 << this.bpos;
      n -= this.bpos;
      num1 = num2 | this.readBits(this.bpos);
      if (this.bbuf != (int) byte.MaxValue)
      {
        this.bbuf = !this.usebais ? (int) this.in_Renamed.read() : this.bais.ReadByte();
        this.bpos = 8;
        if (this.bbuf == (int) byte.MaxValue)
          this.nextbbuf = !this.usebais ? (int) this.in_Renamed.read() : this.bais.ReadByte();
      }
      else
      {
        this.bbuf = this.nextbbuf;
        this.bpos = 7;
      }
    }
    while (n > this.bpos);
    return num1 << n | this.bbuf >> (this.bpos -= n) & (1 << n) - 1;
  }

  internal virtual void setInput(JPXRandomAccessStream in_Renamed)
  {
    this.in_Renamed = in_Renamed;
    this.bbuf = 0;
    this.bpos = 0;
  }

  internal virtual void setInput(MemoryStream bais)
  {
    this.bais = bais;
    this.bbuf = 0;
    this.bpos = 0;
  }

  internal virtual void sync()
  {
    this.bbuf = 0;
    this.bpos = 0;
  }
}
