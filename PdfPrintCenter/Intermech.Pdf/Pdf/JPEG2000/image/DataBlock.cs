// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.JPEG2000.image.DataBlock
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;

#nullable disable
namespace Syncfusion.Pdf.JPEG2000.image;

public abstract class DataBlock
{
  public int h;
  public int offset;
  public bool progressive;
  public int scanw;
  public const int TYPE_BYTE = 0;
  public const int TYPE_FLOAT = 4;
  public const int TYPE_INT = 3;
  public const int TYPE_SHORT = 1;
  public int ulx;
  public int uly;
  public int w;

  public static int getSize(int type)
  {
    switch (type)
    {
      case 0:
        return 8;
      case 1:
        return 16 /*0x10*/;
      case 3:
      case 4:
        return 32 /*0x20*/;
      default:
        throw new ArgumentException();
    }
  }

  public override string ToString()
  {
    string str = "";
    switch (this.DataType)
    {
      case 0:
        str = "Unsigned Byte";
        break;
      case 1:
        str = "Short";
        break;
      case 3:
        str = "Integer";
        break;
      case 4:
        str = "Float";
        break;
    }
    return $"DataBlk: upper-left({(object) this.ulx},{(object) this.uly}), width={(object) this.w}, height={(object) this.h}, progressive={(object) this.progressive}, offset={(object) this.offset}, scanw={(object) this.scanw}, type={str}";
  }

  public abstract object Data { get; set; }

  public abstract int DataType { get; }
}
