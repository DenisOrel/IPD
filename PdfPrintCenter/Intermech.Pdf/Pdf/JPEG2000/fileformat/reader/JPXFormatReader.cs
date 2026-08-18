// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.JPEG2000.fileformat.reader.JPXFormatReader
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.JPEG2000.io;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace Syncfusion.Pdf.JPEG2000.fileformat.reader;

internal class JPXFormatReader
{
  private List<object> codeStreamLength;
  private List<object> codeStreamPos;
  private JPXRandomAccessStream in_Renamed;
  public bool JP2FFUsed;

  internal JPXFormatReader(JPXRandomAccessStream in_Renamed) => this.in_Renamed = in_Renamed;

  public virtual bool readContiguousCodeStreamBox(long pos, int length, long longLength)
  {
    int pos1 = this.in_Renamed.Pos;
    if (this.codeStreamPos == null)
      this.codeStreamPos = new List<object>(10);
    this.codeStreamPos.Add((object) pos1);
    if (this.codeStreamLength == null)
      this.codeStreamLength = new List<object>(10);
    this.codeStreamLength.Add((object) length);
    return true;
  }

  public virtual void readFileFormat()
  {
    long longLength = 0;
    bool flag1 = false;
    bool flag2 = false;
    try
    {
      if (this.in_Renamed.readInt() != 12 || this.in_Renamed.readInt() != 1783636000 || this.in_Renamed.readInt() != 218793738)
      {
        this.in_Renamed.seek(0);
        if (this.in_Renamed.readShort() != (short) -177)
          this.JP2FFUsed = false;
        this.in_Renamed.seek(0);
        return;
      }
      this.JP2FFUsed = true;
      if (this.readFileTypeBox())
        ;
      while (!flag2)
      {
        int pos = this.in_Renamed.Pos;
        int length = this.in_Renamed.readInt();
        if (pos + length == this.in_Renamed.length())
          flag2 = true;
        int num = this.in_Renamed.readInt();
        switch (length)
        {
          case 0:
            flag2 = true;
            length = this.in_Renamed.length() - this.in_Renamed.Pos;
            break;
          case 1:
            this.in_Renamed.readLong();
            throw new IOException("File too long.");
          default:
            longLength = 0L;
            break;
        }
        switch (num)
        {
          case 1785737827:
            this.readContiguousCodeStreamBox((long) pos, length, longLength);
            break;
          case 1785737832:
            if (flag1)
              this.readJP2HeaderBox((long) pos, length, longLength);
            flag1 = true;
            break;
        }
        if (!flag2)
          this.in_Renamed.seek(pos + length);
      }
    }
    catch (EndOfStreamException ex)
    {
    }
    int count = this.codeStreamPos.Count;
  }

  public virtual bool readFileTypeBox()
  {
    bool flag = false;
    int pos = this.in_Renamed.Pos;
    int num = this.in_Renamed.readInt();
    if (this.in_Renamed.readInt() != 1718909296)
      return false;
    if (num == 1)
    {
      this.in_Renamed.readLong();
      throw new IOException("File too long.");
    }
    this.in_Renamed.readInt();
    this.in_Renamed.readInt();
    for (int index = (num - 16 /*0x10*/) / 4; index > 0; --index)
    {
      if (this.in_Renamed.readInt() == 1785737760)
        flag = true;
    }
    return flag;
  }

  public virtual bool readJP2HeaderBox(long pos, int length, long longLength) => true;

  public virtual long[] CodeStreamPos
  {
    get
    {
      int count = this.codeStreamPos.Count;
      long[] codeStreamPos = new long[count];
      for (int index = 0; index < count; ++index)
        codeStreamPos[index] = (long) (int) this.codeStreamPos[index];
      return codeStreamPos;
    }
  }

  public virtual int FirstCodeStreamLength => (int) this.codeStreamLength[0];

  public virtual int FirstCodeStreamPos => (int) this.codeStreamPos[0];
}
