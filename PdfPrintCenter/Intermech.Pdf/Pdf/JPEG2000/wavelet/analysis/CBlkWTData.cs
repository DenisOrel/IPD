// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.JPEG2000.wavelet.analysis.CBlkWTData
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

#nullable disable
namespace Syncfusion.Pdf.JPEG2000.wavelet.analysis;

public abstract class CBlkWTData
{
  public double convertFactor = 1.0;
  public int h;
  public int m;
  public int magbits;
  public int n;
  public int nROIbp;
  public int nROIcoeff;
  public int offset;
  internal SubbandAn sb;
  public int scanw;
  public double stepSize = 1.0;
  public int ulx;
  public int uly;
  public int w;
  public float wmseScaling = 1f;

  public override string ToString()
  {
    string str = "";
    switch (this.DataType)
    {
      case 0:
        str = "Unsigned Byte";
        break;
      case 1:
        str = "Short";
        break;
      case 3:
        str = "Integer";
        break;
      case 4:
        str = "Float";
        break;
    }
    return $"ulx={(object) this.ulx}, uly={(object) this.uly}, idx=({(object) this.m},{(object) this.n}), w={(object) this.w}, h={(object) this.h}, off={(object) this.offset}, scanw={(object) this.scanw}, wmseScaling={(object) this.wmseScaling}, convertFactor={(object) this.convertFactor}, stepSize={(object) this.stepSize}, type={str}, magbits={(object) this.magbits}, nROIcoeff={(object) this.nROIcoeff}, nROIbp={(object) this.nROIbp}";
  }

  public abstract object Data { get; set; }

  public abstract int DataType { get; }
}
