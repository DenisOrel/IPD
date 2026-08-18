// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.JPEG2000.entropy.decoder.ByteInputBuffer
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.IO;


namespace Syncfusion.Pdf.JPEG2000.entropy.decoder
{
    internal class ByteInputBuffer
    {
      private byte[] buf;
      private int count;
      private int pos;

      public ByteInputBuffer(byte[] buf)
      {
        this.buf = buf;
        this.count = buf.Length;
      }

      public ByteInputBuffer(byte[] buf, int offset, int length)
      {
        this.buf = buf;
        this.pos = offset;
        this.count = offset + length;
      }

      public virtual void addByteArray(byte[] data, int off, int len)
      {
        lock (this)
        {
          if (len < 0 || off < 0 || len + off > this.buf.Length)
            throw new ArgumentException();
          if (this.count + len <= this.buf.Length)
          {
            Array.Copy((Array) data, off, (Array) this.buf, this.count, len);
            this.count += len;
          }
          else
          {
            if (this.count - this.pos + len <= this.buf.Length)
            {
              Array.Copy((Array) this.buf, this.pos, (Array) this.buf, 0, this.count - this.pos);
            }
            else
            {
              byte[] buf1 = this.buf;
              this.buf = new byte[this.count - this.pos + len];
              int count = this.count;
              byte[] buf2 = this.buf;
              int length = this.count - this.pos;
              Array.Copy((Array) buf1, count, (Array) buf2, 0, length);
            }
            this.count -= this.pos;
            this.pos = 0;
            Array.Copy((Array) data, off, (Array) this.buf, this.count, len);
            this.count += len;
          }
        }
      }

      public virtual int read()
      {
        return this.pos < this.count ? (int) this.buf[this.pos++] & (int) byte.MaxValue : -1;
      }

      public virtual int readChecked()
      {
        if (this.pos >= this.count)
          throw new EndOfStreamException();
        return (int) this.buf[this.pos++] & (int) byte.MaxValue;
      }

      public virtual void setByteArray(byte[] buf, int offset, int length)
      {
        if (buf == null)
        {
          if (length < 0 || this.count + length > this.buf.Length)
            throw new ArgumentException();
          if (offset < 0)
          {
            this.pos = this.count;
            this.count += length;
          }
          else
          {
            this.count = offset + length;
            this.pos = offset;
          }
        }
        else
        {
          if (offset < 0 || length < 0 || offset + length > buf.Length)
            throw new ArgumentException();
          this.buf = buf;
          this.count = offset + length;
          this.pos = offset;
        }
      }
    }
}
