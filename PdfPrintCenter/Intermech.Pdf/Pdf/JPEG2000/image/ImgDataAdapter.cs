// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.JPEG2000.image.ImgDataAdapter
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

#nullable disable
namespace Syncfusion.Pdf.JPEG2000.image;

internal abstract class ImgDataAdapter : ImageData
{
  internal ImageData imgdatasrc;
  internal int tIdx;

  internal ImgDataAdapter(ImageData src) => this.imgdatasrc = src;

  public virtual int getCompImgHeight(int c) => this.imgdatasrc.getCompImgHeight(c);

  public virtual int getCompImgWidth(int c) => this.imgdatasrc.getCompImgWidth(c);

  public virtual int getCompSubsX(int c) => this.imgdatasrc.getCompSubsX(c);

  public virtual int getCompSubsY(int c) => this.imgdatasrc.getCompSubsY(c);

  public virtual int getCompUpperLeftCornerX(int c) => this.imgdatasrc.getCompUpperLeftCornerX(c);

  public virtual int getCompUpperLeftCornerY(int c) => this.imgdatasrc.getCompUpperLeftCornerY(c);

  public virtual int getNomRangeBits(int c) => this.imgdatasrc.getNomRangeBits(c);

  public virtual int getNumTiles() => this.imgdatasrc.getNumTiles();

  public virtual JPXImageCoordinates getNumTiles(JPXImageCoordinates co)
  {
    return this.imgdatasrc.getNumTiles(co);
  }

  public virtual JPXImageCoordinates getTile(JPXImageCoordinates co) => this.imgdatasrc.getTile(co);

  public virtual int getTileComponentHeight(int t, int c)
  {
    return this.imgdatasrc.getTileComponentHeight(t, c);
  }

  public virtual int getTileComponentWidth(int t, int c)
  {
    return this.imgdatasrc.getTileComponentWidth(t, c);
  }

  public virtual void nextTile()
  {
    this.imgdatasrc.nextTile();
    this.tIdx = this.TileIdx;
  }

  public virtual void setTile(int x, int y)
  {
    this.imgdatasrc.setTile(x, y);
    this.tIdx = this.TileIdx;
  }

  public virtual int ImgHeight => this.imgdatasrc.ImgHeight;

  public virtual int ImgULX => this.imgdatasrc.ImgULX;

  public virtual int ImgULY => this.imgdatasrc.ImgULY;

  public virtual int ImgWidth => this.imgdatasrc.ImgWidth;

  public virtual int NomTileHeight => this.imgdatasrc.NomTileHeight;

  public virtual int NomTileWidth => this.imgdatasrc.NomTileWidth;

  public virtual int NumComps => this.imgdatasrc.NumComps;

  public virtual int TileHeight => this.imgdatasrc.TileHeight;

  public virtual int TileIdx => this.imgdatasrc.TileIdx;

  public virtual int TilePartULX => this.imgdatasrc.TilePartULX;

  public virtual int TilePartULY => this.imgdatasrc.TilePartULY;

  public virtual int TileWidth => this.imgdatasrc.TileWidth;
}
