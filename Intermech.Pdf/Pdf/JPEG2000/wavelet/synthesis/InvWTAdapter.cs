// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.JPEG2000.wavelet.synthesis.InvWTAdapter
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.JPEG2000.decoder;
using Syncfusion.Pdf.JPEG2000.image;
using System;


namespace Syncfusion.Pdf.JPEG2000.wavelet.synthesis
{
    public abstract class InvWTAdapter : InvWT, WaveletTransform, ImageData
    {
      internal DecodeHelper decSpec;
      internal int maxImgRes;
      internal MultiResImgData mressrc;
      internal int reslvl;

      internal InvWTAdapter(MultiResImgData src, DecodeHelper decSpec)
      {
        this.mressrc = src;
        this.decSpec = decSpec;
        this.maxImgRes = decSpec.dls.Min;
      }

      public virtual int getCompImgHeight(int c)
      {
        int minInComp = this.decSpec.dls.getMinInComp(c);
        return this.mressrc.getCompImgHeight(c, minInComp);
      }

      public virtual int getCompImgWidth(int c)
      {
        int minInComp = this.decSpec.dls.getMinInComp(c);
        return this.mressrc.getCompImgWidth(c, minInComp);
      }

      public virtual int getCompSubsX(int c) => this.mressrc.getCompSubsX(c);

      public virtual int getCompSubsY(int c) => this.mressrc.getCompSubsY(c);

      public virtual int getCompUpperLeftCornerX(int c)
      {
        int resLvl = this.mressrc.getSynSubbandTree(this.TileIdx, c).resLvl;
        return this.mressrc.getResULX(c, resLvl);
      }

      public virtual int getCompUpperLeftCornerY(int c)
      {
        int resLvl = this.mressrc.getSynSubbandTree(this.TileIdx, c).resLvl;
        return this.mressrc.getResULY(c, resLvl);
      }

      public abstract int getImplementationType(int param1);

      public abstract int getNomRangeBits(int param1);

      public virtual int getNumTiles() => this.mressrc.getNumTiles();

      public virtual JPXImageCoordinates getNumTiles(JPXImageCoordinates co)
      {
        return this.mressrc.getNumTiles(co);
      }

      internal virtual SubbandSyn getSynSubbandTree(int t, int c)
      {
        return this.mressrc.getSynSubbandTree(t, c);
      }

      public virtual JPXImageCoordinates getTile(JPXImageCoordinates co) => this.mressrc.getTile(co);

      public virtual int getTileComponentHeight(int t, int c)
      {
        int resLvl = this.mressrc.getSynSubbandTree(t, c).resLvl;
        return this.mressrc.getTileCompHeight(t, c, resLvl);
      }

      public virtual int getTileComponentWidth(int t, int c)
      {
        int resLvl = this.mressrc.getSynSubbandTree(t, c).resLvl;
        return this.mressrc.getTileCompWidth(t, c, resLvl);
      }

      public abstract bool isReversible(int param1, int param2);

      public virtual void nextTile() => this.mressrc.nextTile();

      public virtual void setTile(int x, int y) => this.mressrc.setTile(x, y);

      public virtual int ImgHeight => this.mressrc.getImgHeight(this.reslvl);

      public virtual int ImgResLevel
      {
        set
        {
          this.reslvl = value >= 0 ? value : throw new ArgumentException("Resolution level index cannot be negative.");
        }
      }

      public virtual int ImgULX => this.mressrc.getImgULX(this.reslvl);

      public virtual int ImgULY => this.mressrc.getImgULY(this.reslvl);

      public virtual int ImgWidth => this.mressrc.getImgWidth(this.reslvl);

      public virtual int NomTileHeight => this.mressrc.NomTileHeight;

      public virtual int NomTileWidth => this.mressrc.NomTileWidth;

      public virtual int NumComps => this.mressrc.NumComps;

      public virtual int TileHeight
      {
        get
        {
          int tileIdx = this.TileIdx;
          int rl = 10000;
          int numComps = this.mressrc.NumComps;
          for (int c = 0; c < numComps; ++c)
          {
            int resLvl = this.mressrc.getSynSubbandTree(tileIdx, c).resLvl;
            if (resLvl < rl)
              rl = resLvl;
          }
          return this.mressrc.getTileHeight(rl);
        }
      }

      public virtual int TileIdx => this.mressrc.TileIdx;

      public virtual int TilePartULX => this.mressrc.TilePartULX;

      public virtual int TilePartULY => this.mressrc.TilePartULY;

      public virtual int TileWidth
      {
        get
        {
          int tileIdx = this.TileIdx;
          int rl = 10000;
          int numComps = this.mressrc.NumComps;
          for (int c = 0; c < numComps; ++c)
          {
            int resLvl = this.mressrc.getSynSubbandTree(tileIdx, c).resLvl;
            if (resLvl < rl)
              rl = resLvl;
          }
          return this.mressrc.getTileWidth(rl);
        }
      }
    }
}
