// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.JPEG2000.quantization.quantizer.CBlkQuantDataSrcEnc
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.JPEG2000.image;
using Syncfusion.Pdf.JPEG2000.wavelet.analysis;


namespace Syncfusion.Pdf.JPEG2000.quantization.quantizer
{
    internal interface CBlkQuantDataSrcEnc : ForwWTDataProps, ImageData
    {
      CBlkWTData getNextCodeBlock(int c, CBlkWTData cblk);

      CBlkWTData getNextInternCodeBlock(int c, CBlkWTData cblk);
    }
}
