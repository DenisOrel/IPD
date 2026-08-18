// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.JPEG2000.roi.encoder.ROIMaskGenerator
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.JPEG2000.image;
using Syncfusion.Pdf.JPEG2000.wavelet;

#nullable disable
namespace Syncfusion.Pdf.JPEG2000.roi.encoder;

public abstract class ROIMaskGenerator
{
  internal int nrc;
  internal ROI[] roi_array;
  internal bool roiInTile;
  internal bool[] tileMaskMade;

  internal ROIMaskGenerator(ROI[] rois, int nrc)
  {
    this.roi_array = rois;
    this.nrc = nrc;
    this.tileMaskMade = new bool[nrc];
  }

  internal abstract bool getROIMask(DataBlockInt db, Subband sb, int magbits, int c);

  public abstract void makeMask(Subband sb, int magbits, int n);

  public virtual void tileChanged()
  {
    for (int index = 0; index < this.nrc; ++index)
      this.tileMaskMade[index] = false;
  }

  internal virtual ROI[] ROIs => this.roi_array;
}
