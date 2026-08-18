// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.JPEG2000.image.output.ImgWriter
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

#nullable disable
namespace Syncfusion.Pdf.JPEG2000.image.output;

public abstract class ImgWriter
{
  public const int DEF_STRIP_HEIGHT = 64 /*0x40*/;
  internal int h;
  internal BlockImageDataSource src;
  internal int w;

  public abstract void close();

  ~ImgWriter() => this.flush();

  public abstract void flush();

  public abstract void write();

  public abstract void write(int ulx, int uly, int w, int h);

  public virtual void writeAll()
  {
    JPXImageCoordinates numTiles = this.src.getNumTiles((JPXImageCoordinates) null);
    for (int y = 0; y < numTiles.y; ++y)
    {
      for (int x = 0; x < numTiles.x; ++x)
      {
        this.src.setTile(x, y);
        this.write();
      }
    }
  }
}
