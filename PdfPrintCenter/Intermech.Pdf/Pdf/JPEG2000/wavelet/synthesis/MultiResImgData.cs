// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.JPEG2000.wavelet.synthesis.MultiResImgData
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.JPEG2000.image;

#nullable disable
namespace Syncfusion.Pdf.JPEG2000.wavelet.synthesis;

public interface MultiResImgData
{
  int getCompImgHeight(int n, int rl);

  int getCompImgWidth(int c, int rl);

  int getCompSubsX(int c);

  int getCompSubsY(int c);

  int getImgHeight(int rl);

  int getImgULX(int rl);

  int getImgULY(int rl);

  int getImgWidth(int rl);

  int getNumTiles();

  JPXImageCoordinates getNumTiles(JPXImageCoordinates co);

  int getResULX(int c, int rl);

  int getResULY(int c, int rl);

  SubbandSyn getSynSubbandTree(int t, int c);

  JPXImageCoordinates getTile(JPXImageCoordinates co);

  int getTileCompHeight(int t, int c, int rl);

  int getTileCompWidth(int t, int c, int rl);

  int getTileHeight(int rl);

  int getTileWidth(int rl);

  void nextTile();

  void setTile(int x, int y);

  int NomTileHeight { get; }

  int NomTileWidth { get; }

  int NumComps { get; }

  int TileIdx { get; }

  int TilePartULX { get; }

  int TilePartULY { get; }
}
