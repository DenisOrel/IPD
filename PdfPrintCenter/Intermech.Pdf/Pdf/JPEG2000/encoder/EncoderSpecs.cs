// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.JPEG2000.encoder.EncoderSpecs
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.JPEG2000.entropy;
using Syncfusion.Pdf.JPEG2000.image;
using Syncfusion.Pdf.JPEG2000.quantization;
using Syncfusion.Pdf.JPEG2000.roi;
using Syncfusion.Pdf.JPEG2000.wavelet.analysis;

#nullable disable
namespace Syncfusion.Pdf.JPEG2000.encoder;

internal class EncoderSpecs
{
  public StringSpec bms;
  public CBlkSizeSpec cblks;
  public StringSpec css;
  public CompTransfSpec cts;
  public IntegerSpec dls;
  public StringSpec ephs;
  public GuardBitsSpec gbs;
  public StringSpec lcs;
  public StringSpec mqrs;
  public int nComp;
  public int nTiles;
  public PrecinctSizeSpec pss;
  public QuantStepSizeSpec qsss;
  public QuantTypeSpec qts;
  public MaxShiftSpec rois;
  public StringSpec rts;
  public StringSpec sops;
  public StringSpec sss;
  public StringSpec tts;
  public AnWTFilterSpec wfs;
}
