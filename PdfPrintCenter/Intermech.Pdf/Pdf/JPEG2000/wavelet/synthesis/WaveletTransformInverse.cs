// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.JPEG2000.wavelet.synthesis.WaveletTransformInverse
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.JPEG2000.decoder;
using Syncfusion.Pdf.JPEG2000.image;

#nullable disable
namespace Syncfusion.Pdf.JPEG2000.wavelet.synthesis;

public abstract class WaveletTransformInverse : InvWTAdapter, BlockImageDataSource, ImageData
{
  internal WaveletTransformInverse(MultiResImgData src, DecodeHelper decSpec)
    : base(src, decSpec)
  {
  }

  internal static WaveletTransformInverse createInstance(CBlkWTDataSrcDec src, DecodeHelper decSpec)
  {
    return (WaveletTransformInverse) new InvWTFull(src, decSpec);
  }

  public abstract DataBlock getCompData(DataBlock param1, int param2);

  public abstract int getFixedPoint(int param1);

  public abstract DataBlock getInternCompData(DataBlock param1, int param2);
}
