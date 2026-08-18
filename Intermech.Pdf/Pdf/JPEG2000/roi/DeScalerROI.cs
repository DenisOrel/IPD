// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.JPEG2000.roi.DeScalerROI
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.JPEG2000.decoder;
using Syncfusion.Pdf.JPEG2000.image;
using Syncfusion.Pdf.JPEG2000.quantization.dequantizer;
using Syncfusion.Pdf.JPEG2000.util;
using Syncfusion.Pdf.JPEG2000.wavelet.synthesis;


namespace Syncfusion.Pdf.JPEG2000.roi
{
    internal class DeScalerROI : MultiResImgDataAdapter, CBlkQuantDataSrcDec, InvWTData, MultiResImgData
    {
      private MaxShiftSpec mss;
      public const char OPT_PREFIX = 'R';
      private static readonly string[][] pinfo = new string[1][]
      {
        new string[4]
        {
          "Rno_roi",
          null,
          "This argument makes sure that the no ROI de-scaling is performed. Decompression is done like there is no ROI in the image",
          null
        }
      };
      private CBlkQuantDataSrcDec src;

      internal DeScalerROI(CBlkQuantDataSrcDec src, MaxShiftSpec mss)
        : base((MultiResImgData) src)
      {
        this.src = src;
        this.mss = mss;
      }

      internal static DeScalerROI createInstance(
        CBlkQuantDataSrcDec src,
        JPXParameters pl,
        DecodeHelper decSpec)
      {
        pl.checkList('R', JPXParameters.toNameArray(DeScalerROI.pinfo));
        return pl.getParameter("Rno_roi") == null && decSpec.rois != null ? new DeScalerROI(src, decSpec.rois) : new DeScalerROI(src, (MaxShiftSpec) null);
      }

      public virtual DataBlock getCodeBlock(int c, int m, int n, SubbandSyn sb, DataBlock cblk)
      {
        return this.getInternCodeBlock(c, m, n, sb, cblk);
      }

      public virtual DataBlock getInternCodeBlock(int c, int m, int n, SubbandSyn sb, DataBlock cblk)
      {
        cblk = this.src.getInternCodeBlock(c, m, n, sb, cblk);
        bool flag = false;
        if (this.mss == null || this.mss.getTileCompVal(this.TileIdx, c) == null)
          flag = true;
        if (!flag && cblk != null)
        {
          int[] data = (int[]) cblk.Data;
          int ulx = cblk.ulx;
          int uly = cblk.uly;
          int w = cblk.w;
          int h = cblk.h;
          int tileCompVal = (int) this.mss.getTileCompVal(this.TileIdx, c);
          int num1 = (1 << sb.magbits) - 1 << 31 /*0x1F*/ - sb.magbits;
          int num2 = ~num1 & int.MaxValue;
          int num3 = cblk.scanw - w;
          int index1 = cblk.offset + cblk.scanw * (h - 1) + w - 1;
          for (int index2 = h; index2 > 0; --index2)
          {
            int num4 = w;
            while (num4 > 0)
            {
              int num5 = data[index1];
              if ((num5 & num1) == 0)
                data[index1] = num5 & int.MinValue | num5 << tileCompVal;
              else if ((num5 & num2) != 0)
                data[index1] = num5 & ~num2 | 1 << 30 - sb.magbits;
              --num4;
              --index1;
            }
            index1 -= num3;
          }
        }
        return cblk;
      }

      public override SubbandSyn getSynSubbandTree(int t, int c)
      {
        return ((InvWTData) this.src).getSynSubbandTree(t, c);
      }

      public virtual int CbULX => this.src.CbULX;

      public virtual int CbULY => this.src.CbULY;

      public static string[][] ParameterInfo => DeScalerROI.pinfo;
    }
}
