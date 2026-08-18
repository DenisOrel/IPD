// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.JPEG2000.roi.encoder.ROIScaler
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.JPEG2000.encoder;
using Syncfusion.Pdf.JPEG2000.image;
using Syncfusion.Pdf.JPEG2000.quantization.quantizer;
using Syncfusion.Pdf.JPEG2000.wavelet.analysis;

#nullable disable
namespace Syncfusion.Pdf.JPEG2000.roi.encoder;

internal class ROIScaler
{
  private bool blockAligned;
  private int[][] maxMagBits;
  private ROIMaskGenerator mg;
  public const char OPT_PREFIX = 'R';
  private static readonly string[][] pinfo;
  private bool roi;
  private DataBlockInt roiMask;
  private Quantizer src;
  private int useStartLevel;

  static ROIScaler()
  {
    string[][] strArray1 = new string[4][];
    string[] strArray2 = new string[4]
    {
      "Rroi",
      "[<component idx>] R <left> <top> <width> <height> or [<component idx>] C <centre column> <centre row> <radius> or [<component idx>] A <filename>",
      "Specifies ROIs shape and location. The shape can be either rectangular 'R', or circular 'C' or arbitrary 'A'. Each new occurrence of an 'R', a 'C' or an 'A' is a new ROI. For circular and rectangular ROIs, all values are given as their pixel values relative to the canvas origin. Arbitrary shapes must be included in a PGM file where non 0 values correspond to ROI coefficients. The PGM file must have the size as the image. The component idx specifies which components contain the ROI. The component index is specified as described by points 3 and 4 in the general comment on tile-component idx. If this option is used, the codestream is layer progressive by default unless it is overridden by the 'Aptype' option.",
      null
    };
    strArray1[0] = strArray2;
    strArray1[1] = new string[4]
    {
      "Ralign",
      "[on|off]",
      "By specifying this argument, the ROI mask will be limited to covering only entire code-blocks. The ROI coding can then be performed without any actual scaling of the coefficients but by instead scaling the distortion estimates.",
      "off"
    };
    strArray1[2] = new string[4]
    {
      "Rstart_level",
      "<level>",
      "This argument forces the lowest <level> resolution levels to belong to the ROI. By doing this, it is possible to avoid only getting information for the ROI at an early stage of transmission.<level> = 0 means the lowest resolution level belongs to the ROI, 1 means the two lowest etc. (-1 deactivates the option)",
      "-1"
    };
    strArray1[3] = new string[4]
    {
      "Rno_rect",
      "[on|off]",
      "This argument makes sure that the ROI mask generation is not done using the fast ROI mask generation for rectangular ROIs regardless of whether the specified ROIs are rectangular or not",
      "off"
    };
    ROIScaler.pinfo = strArray1;
  }

  private void calcMaxMagBits(EncoderSpecs encSpec)
  {
    MaxShiftSpec rois = encSpec.rois;
    int numTiles = this.src.getNumTiles();
    int numComps = this.src.NumComps;
    this.maxMagBits = new int[numTiles][];
    for (int index = 0; index < numTiles; ++index)
      this.maxMagBits[index] = new int[numComps];
    this.src.setTile(0, 0);
    for (int t = 0; t < numTiles; ++t)
    {
      for (int c = numComps - 1; c >= 0; --c)
      {
        int maxMagBits = this.src.getMaxMagBits(c);
        this.maxMagBits[t][c] = maxMagBits;
        rois.setTileCompVal(t, c, (object) maxMagBits);
      }
      if (t < numTiles - 1)
        this.src.nextTile();
    }
    this.src.setTile(0, 0);
  }

  public virtual SubbandAn getAnSubbandTree(int t, int c) => this.src.getAnSubbandTree(t, c);

  public virtual bool isReversible(int t, int c) => this.src.isReversible(t, c);

  public virtual bool useRoi() => this.roi;

  public virtual bool BlockAligned => this.blockAligned;

  public virtual int CbULX => this.src.CbULX;

  public virtual int CbULY => this.src.CbULY;

  public static string[][] ParameterInfo => ROIScaler.pinfo;

  public virtual ROIMaskGenerator ROIMaskGenerator => this.mg;
}
