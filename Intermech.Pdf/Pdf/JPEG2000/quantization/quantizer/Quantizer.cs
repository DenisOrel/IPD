// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.JPEG2000.quantization.quantizer.Quantizer
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.JPEG2000.encoder;
using Syncfusion.Pdf.JPEG2000.image;
using Syncfusion.Pdf.JPEG2000.wavelet.analysis;


namespace Syncfusion.Pdf.JPEG2000.quantization.quantizer
{
    internal abstract class Quantizer : ImgDataAdapter, CBlkQuantDataSrcEnc, ForwWTDataProps, ImageData
    {
      public const char OPT_PREFIX = 'Q';
      private static readonly string[][] pinfo;
      internal CBlkWTDataSrc src;

      static Quantizer()
      {
        string[][] strArray1 = new string[3][];
        string[] strArray2 = new string[4]
        {
          "Qtype",
          "[<tile-component idx>] <id> [ [<tile-component idx>] <id> ...]",
          "Specifies which quantization type to use for specified tile-component. The default type is either 'reversible' or 'expounded' depending on whether or not the '-lossless' option  is specified.\n<tile-component idx> : see general note.\n<id>: Supported quantization types specification are : 'reversible' (no quantization), 'derived' (derived quantization step size) and 'expounded'.\nExample: -Qtype reversible or -Qtype t2,4-8 c2 reversible t9 derived.",
          null
        };
        strArray1[0] = strArray2;
        strArray1[1] = new string[4]
        {
          "Qstep",
          "[<tile-component idx>] <bnss> [ [<tile-component idx>] <bnss> ...]",
          "This option specifies the base normalized quantization step size (bnss) for tile-components. It is normalized to a dynamic range of 1 in the image domain. This parameter is ignored in reversible coding. The default value is '1/128' (i.e. 0.0078125).",
          "0.0078125"
        };
        strArray1[2] = new string[4]
        {
          "Qguard_bits",
          "[<tile-component idx>] <gb> [ [<tile-component idx>] <gb> ...]",
          "The number of bits used for each tile-component in the quantizer to avoid overflow (gb).",
          "2"
        };
        Quantizer.pinfo = strArray1;
      }

      internal Quantizer(CBlkWTDataSrc src)
        : base((ImageData) src)
      {
        this.src = src;
      }

      internal abstract void calcSbParams(SubbandAn sb, int n);

      internal static Quantizer createInstance(CBlkWTDataSrc src, EncoderSpecs encSpec)
      {
        return (Quantizer) new StdQuantizer(src, encSpec);
      }

      public virtual SubbandAn getAnSubbandTree(int t, int c)
      {
        SubbandAn anSubbandTree = this.src.getAnSubbandTree(t, c);
        this.calcSbParams(anSubbandTree, c);
        return anSubbandTree;
      }

      public abstract int getMaxMagBits(int c);

      public abstract CBlkWTData getNextCodeBlock(int param1, CBlkWTData param2);

      public abstract CBlkWTData getNextInternCodeBlock(int param1, CBlkWTData param2);

      public abstract int getNumGuardBits(int t, int c);

      public abstract bool isDerived(int t, int c);

      public abstract bool isReversible(int param1, int param2);

      public virtual int CbULX => this.src.CbULX;

      public virtual int CbULY => this.src.CbULY;

      public static string[][] ParameterInfo => Quantizer.pinfo;
    }
}
