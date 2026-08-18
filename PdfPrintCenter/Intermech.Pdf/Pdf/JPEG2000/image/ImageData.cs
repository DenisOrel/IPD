// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.JPEG2000.image.ImageData
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

#nullable disable
namespace Syncfusion.Pdf.JPEG2000.image;

public interface ImageData
{
  int getCompImgHeight(int c);

  int getCompImgWidth(int c);

  int getCompSubsX(int c);

  int getCompSubsY(int c);

  int getCompUpperLeftCornerX(int c);

  int getCompUpperLeftCornerY(int c);

  int getNomRangeBits(int c);

  int getNumTiles();

  JPXImageCoordinates getNumTiles(JPXImageCoordinates co);

  JPXImageCoordinates getTile(JPXImageCoordinates co);

  int getTileComponentHeight(int t, int c);

  int getTileComponentWidth(int t, int c);

  void nextTile();

  void setTile(int x, int y);

  int ImgHeight { get; }

  int ImgULX { get; }

  int ImgULY { get; }

  int ImgWidth { get; }

  int NomTileHeight { get; }

  int NomTileWidth { get; }

  int NumComps { get; }

  int TileHeight { get; }

  int TileIdx { get; }

  int TilePartULX { get; }

  int TilePartULY { get; }

  int TileWidth { get; }
}
