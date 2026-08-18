// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.JPEG2000.image.input.ImgReader
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;


namespace Syncfusion.Pdf.JPEG2000.image.input
{
    public abstract class ImgReader : BlockImageDataSource, ImageData
    {
      internal int h;
      internal int nc;
      internal int w;

      public abstract void close();

      public abstract DataBlock getCompData(DataBlock param1, int param2);

      public virtual int getCompImgHeight(int c) => this.h;

      public virtual int getCompImgWidth(int c) => this.w;

      public virtual int getCompSubsX(int c) => 1;

      public virtual int getCompSubsY(int c) => 1;

      public virtual int getCompUpperLeftCornerX(int c) => 0;

      public virtual int getCompUpperLeftCornerY(int c) => 0;

      public abstract int getFixedPoint(int param1);

      public abstract DataBlock getInternCompData(DataBlock param1, int param2);

      public abstract int getNomRangeBits(int param1);

      public virtual int getNumTiles() => 1;

      public virtual JPXImageCoordinates getNumTiles(JPXImageCoordinates co)
      {
        if (co == null)
          return new JPXImageCoordinates(1, 1);
        co.x = 1;
        co.y = 1;
        return co;
      }

      public virtual JPXImageCoordinates getTile(JPXImageCoordinates co)
      {
        if (co == null)
          return new JPXImageCoordinates(0, 0);
        co.x = 0;
        co.y = 0;
        return co;
      }

      public virtual int getTileComponentHeight(int t, int c) => this.h;

      public virtual int getTileComponentWidth(int t, int c) => this.w;

      public abstract bool isOrigSigned(int c);

      public virtual void nextTile() => throw new Exception();

      public virtual void setTile(int x, int y)
      {
        if (x != 0 || y != 0)
          throw new ArgumentException();
      }

      public virtual int ImgHeight => this.h;

      public virtual int ImgULX => 0;

      public virtual int ImgULY => 0;

      public virtual int ImgWidth => this.w;

      public virtual int NomTileHeight => this.h;

      public virtual int NomTileWidth => this.w;

      public virtual int NumComps => this.nc;

      public virtual int TileHeight => this.h;

      public virtual int TileIdx => 0;

      public virtual int TilePartULX => 0;

      public virtual int TilePartULY => 0;

      public virtual int TileWidth => this.w;
    }
}
