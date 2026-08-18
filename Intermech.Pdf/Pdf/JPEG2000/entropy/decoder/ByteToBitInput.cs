// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.JPEG2000.entropy.decoder.ByteToBitInput
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml


namespace Syncfusion.Pdf.JPEG2000.entropy.decoder
{
    internal class ByteToBitInput
    {
      internal int bbuf;
      internal int bpos = -1;
      internal ByteInputBuffer in_Renamed;

      public ByteToBitInput(ByteInputBuffer in_Renamed) => this.in_Renamed = in_Renamed;

      public virtual bool checkBytePadding()
      {
        if (this.bpos < 0 && (this.bbuf & (int) byte.MaxValue) == (int) byte.MaxValue)
        {
          this.bbuf = this.in_Renamed.read();
          this.bpos = 6;
        }
        if (this.bpos >= 0 && (this.bbuf & (1 << this.bpos + 1) - 1) != 85 >> 7 - this.bpos)
          return true;
        if (this.bbuf != -1)
        {
          if (this.bbuf == (int) byte.MaxValue && this.bpos == 0)
          {
            if ((this.in_Renamed.read() & (int) byte.MaxValue) >= 128 /*0x80*/)
              return true;
          }
          else if (this.in_Renamed.read() != -1)
            return true;
        }
        return false;
      }

      internal void flush()
      {
        this.bbuf = 0;
        this.bpos = -1;
      }

      public int readBit()
      {
        if (this.bpos < 0)
        {
          if ((this.bbuf & (int) byte.MaxValue) != (int) byte.MaxValue)
          {
            this.bbuf = this.in_Renamed.read();
            this.bpos = 7;
          }
          else
          {
            this.bbuf = this.in_Renamed.read();
            this.bpos = 6;
          }
        }
        return this.bbuf >> this.bpos-- & 1;
      }

      internal void setByteArray(byte[] buf, int off, int len)
      {
        this.in_Renamed.setByteArray(buf, off, len);
        this.bbuf = 0;
        this.bpos = -1;
      }
    }
}
