// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.JPEG2000.quantization.dequantizer.StdDequantizer
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.JPEG2000.decoder;
using Syncfusion.Pdf.JPEG2000.image;
using Syncfusion.Pdf.JPEG2000.wavelet.synthesis;
using System;


namespace Syncfusion.Pdf.JPEG2000.quantization.dequantizer
{
    internal class StdDequantizer : Dequantizer
    {
      private GuardBitsSpec gbs;
      private DataBlockInt inblk;
      private int outdtype;
      private QuantStepSizeSpec qsss;
      private QuantTypeSpec qts;

      internal StdDequantizer(CBlkQuantDataSrcDec src, int[] utrb, DecodeHelper decSpec)
        : base(src, utrb, decSpec)
      {
        if (utrb.Length != src.NumComps)
          throw new ArgumentException("Invalid rb argument");
        this.qsss = decSpec.qsss;
        this.qts = decSpec.qts;
        this.gbs = decSpec.gbs;
      }

      public override DataBlock getCodeBlock(int c, int m, int n, SubbandSyn sb, DataBlock cblk)
      {
        return this.getInternCodeBlock(c, m, n, sb, cblk);
      }

      public override int getFixedPoint(int c) => 0;

      public override DataBlock getInternCodeBlock(
        int c,
        int m,
        int n,
        SubbandSyn sb,
        DataBlock cblk)
      {
        bool flag1 = this.qts.isReversible(this.tIdx, c);
        bool flag2 = this.qts.isDerived(this.tIdx, c);
        StdDequantizerParams tileCompVal1 = (StdDequantizerParams) this.qsss.getTileCompVal(this.tIdx, c);
        int tileCompVal2 = (int) this.gbs.getTileCompVal(this.tIdx, c);
        this.outdtype = cblk.DataType;
        if (flag1 && this.outdtype != 3)
          throw new ArgumentException("Reversible quantizations must use int data");
        int[] numArray1 = (int[]) null;
        float[] numArray2 = (float[]) null;
        int[] numArray3 = (int[]) null;
        switch (this.outdtype)
        {
          case 3:
            cblk = this.src.getCodeBlock(c, m, n, sb, cblk);
            numArray1 = (int[]) cblk.Data;
            break;
          case 4:
            this.inblk = (DataBlockInt) this.src.getInternCodeBlock(c, m, n, sb, (DataBlock) this.inblk);
            numArray3 = this.inblk.DataInt;
            if (cblk == null)
              cblk = (DataBlock) new DataBlockFloat();
            cblk.ulx = this.inblk.ulx;
            cblk.uly = this.inblk.uly;
            cblk.w = this.inblk.w;
            cblk.h = this.inblk.h;
            cblk.offset = 0;
            cblk.scanw = cblk.w;
            cblk.progressive = this.inblk.progressive;
            numArray2 = (float[]) cblk.Data;
            if (numArray2 == null || numArray2.Length < cblk.w * cblk.h)
            {
              numArray2 = new float[cblk.w * cblk.h];
              cblk.Data = (object) numArray2;
              break;
            }
            break;
        }
        int magbits = sb.magbits;
        if (flag1)
        {
          int num1 = 31 /*0x1F*/ - magbits;
          for (int index = numArray1.Length - 1; index >= 0; --index)
          {
            int num2 = numArray1[index];
            numArray1[index] = num2 >= 0 ? num2 >> num1 : -((num2 & int.MaxValue) >> num1);
          }
          return cblk;
        }
        float num3;
        if (flag2)
        {
          int resLvl = ((InvWTData) this.src).getSynSubbandTree(this.TileIdx, c).resLvl;
          num3 = tileCompVal1.nStep[0][0] * (float) (1L << this.rb[c] + sb.anGainExp + resLvl - sb.level);
        }
        else
          num3 = tileCompVal1.nStep[sb.resLvl][sb.sbandIdx] * (float) (1L << this.rb[c] + sb.anGainExp);
        int num4 = 31 /*0x1F*/ - magbits;
        float num5 = num3 / (float) (1 << num4);
        switch (this.outdtype)
        {
          case 3:
            for (int index = numArray1.Length - 1; index >= 0; --index)
            {
              int num6 = numArray1[index];
              numArray1[index] = (int) ((num6 >= 0 ? (double) num6 : (double) -(num6 & int.MaxValue)) * (double) num5);
            }
            return cblk;
          case 4:
            int w = cblk.w;
            int h = cblk.h;
            int index1 = w * h - 1;
            int index2 = this.inblk.offset + (h - 1) * this.inblk.scanw + w - 1;
            int num7 = w * (h - 1);
            while (index1 >= 0)
            {
              for (; index1 >= num7; --index1)
              {
                int num8 = numArray3[index2];
                numArray2[index1] = (num8 >= 0 ? (float) num8 : (float) -(num8 & int.MaxValue)) * num5;
                --index2;
              }
              index2 -= this.inblk.scanw - w;
              num7 -= w;
            }
            return cblk;
          default:
            return cblk;
        }
      }
    }
}
