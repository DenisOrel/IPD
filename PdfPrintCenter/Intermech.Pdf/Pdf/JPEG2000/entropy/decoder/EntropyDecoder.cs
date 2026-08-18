// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.JPEG2000.entropy.decoder.EntropyDecoder
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.JPEG2000.image;
using Syncfusion.Pdf.JPEG2000.quantization.dequantizer;
using Syncfusion.Pdf.JPEG2000.wavelet.synthesis;

#nullable disable
namespace Syncfusion.Pdf.JPEG2000.entropy.decoder;

public abstract class EntropyDecoder : 
  MultiResImgDataAdapter,
  CBlkQuantDataSrcDec,
  InvWTData,
  MultiResImgData
{
  public const char OPT_PREFIX = 'C';
  private static readonly string[][] pinfo = new string[2][]
  {
    new string[4]
    {
      "Cverber",
      "[on|off]",
      "Specifies if the entropy decoder should be verbose about detected errors. If 'on' a message is printed whenever an error is detected.",
      "on"
    },
    new string[4]
    {
      "Cer",
      "[on|off]",
      "Specifies if error detection should be performed by the entropy decoder engine. If errors are detected they will be concealed and the resulting distortion will be less important. Note that errors can only be detected if the encoder that generated the data included error resilience information.",
      "on"
    }
  };
  internal CodedCBlkDataSrcDec src;

  internal EntropyDecoder(CodedCBlkDataSrcDec src)
    : base((MultiResImgData) src)
  {
    this.src = src;
  }

  public abstract DataBlock getCodeBlock(
    int param1,
    int param2,
    int param3,
    SubbandSyn param4,
    DataBlock param5);

  public abstract DataBlock getInternCodeBlock(
    int param1,
    int param2,
    int param3,
    SubbandSyn param4,
    DataBlock param5);

  public override SubbandSyn getSynSubbandTree(int t, int c)
  {
    return ((InvWTData) this.src).getSynSubbandTree(t, c);
  }

  public virtual int CbULX => this.src.CbULX;

  public virtual int CbULY => this.src.CbULY;

  public static string[][] ParameterInfo => EntropyDecoder.pinfo;
}
