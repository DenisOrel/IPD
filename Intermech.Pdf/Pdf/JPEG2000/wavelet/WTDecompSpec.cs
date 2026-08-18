// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.JPEG2000.wavelet.WTDecompSpec
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;


namespace Syncfusion.Pdf.JPEG2000.wavelet
{
    internal class WTDecompSpec
    {
      private int[] compMainDefDecompType;
      private int[] compMainDefLevels;
      public const byte DEC_SPEC_COMP_DEF = 1;
      public const byte DEC_SPEC_MAIN_DEF = 0;
      public const byte DEC_SPEC_TILE_COMP = 3;
      public const byte DEC_SPEC_TILE_DEF = 2;
      private int mainDefDecompType;
      private int mainDefLevels;
      private byte[] specValType;
      public const int WT_DECOMP_DYADIC = 0;
      public const int WT_DECOMP_PACKET = 1;
      public const int WT_DECOMP_SPACL = 2;

      public WTDecompSpec(int nc, int dec, int lev)
      {
        this.mainDefDecompType = dec;
        this.mainDefLevels = lev;
        this.specValType = new byte[nc];
      }

      public virtual int getDecompType(int n)
      {
        switch (this.specValType[n])
        {
          case 0:
            return this.mainDefDecompType;
          case 1:
            return this.compMainDefDecompType[n];
          case 2:
            throw new ArgumentException("The Tile elemet is not supported in JPX");
          case 3:
            throw new ArgumentException("The Componet elemet is not supported in JPX");
          default:
            throw new ArgumentException();
        }
      }

      public virtual byte getDecSpecType(int n) => this.specValType[n];

      public virtual int getLevels(int n)
      {
        switch (this.specValType[n])
        {
          case 0:
            return this.mainDefLevels;
          case 1:
            return this.compMainDefLevels[n];
          case 2:
            throw new ArgumentException();
          case 3:
            throw new ArgumentException();
          default:
            throw new ArgumentException();
        }
      }

      public virtual void setMainCompDefDecompType(int n, int dec, int lev)
      {
        if (dec < 0 && lev < 0)
          throw new ArgumentException();
        this.specValType[n] = (byte) 1;
        if (this.compMainDefDecompType == null)
        {
          this.compMainDefDecompType = new int[this.specValType.Length];
          this.compMainDefLevels = new int[this.specValType.Length];
        }
        this.compMainDefDecompType[n] = dec >= 0 ? dec : this.mainDefDecompType;
        this.compMainDefLevels[n] = lev >= 0 ? lev : this.mainDefLevels;
        throw new ArgumentException("Components and tiles are having difffrent decomposition type and levels");
      }

      public virtual int MainDefDecompType => this.mainDefDecompType;

      public virtual int MainDefLevels => this.mainDefLevels;
    }
}
