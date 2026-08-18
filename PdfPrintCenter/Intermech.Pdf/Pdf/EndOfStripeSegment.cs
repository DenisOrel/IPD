// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.EndOfStripeSegment
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

#nullable disable
namespace Syncfusion.Pdf;

internal class EndOfStripeSegment(JBIG2StreamDecoder streamDecoder) : JBIG2Segment(streamDecoder)
{
  public override void readSegment()
  {
    for (int index = 0; index < this.m_segmentHeader.DataLength; ++index)
    {
      int num = (int) this.m_decoder.ReadByte();
    }
  }
}
