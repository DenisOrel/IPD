// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.JBIG2BaseSegment
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

#nullable disable
namespace Syncfusion.Pdf;

internal abstract class JBIG2BaseSegment : JBIG2Segment
{
  private BitOperation m_bitOperation;
  protected internal int regionBitmapHeight;
  protected internal int regionBitmapWidth;
  protected internal int regionBitmapXLocation;
  protected internal int regionBitmapYLocation;
  protected internal RegionFlags regionFlags;

  public JBIG2BaseSegment(JBIG2StreamDecoder streamDecoder)
    : base(streamDecoder)
  {
    this.regionFlags = new RegionFlags();
    this.m_bitOperation = new BitOperation();
  }

  public override void readSegment()
  {
    short[] numArray1 = new short[4];
    this.m_decoder.ReadByte(numArray1);
    this.regionBitmapWidth = this.m_bitOperation.GetInt32(numArray1);
    short[] numArray2 = new short[4];
    this.m_decoder.ReadByte(numArray2);
    this.regionBitmapHeight = this.m_bitOperation.GetInt32(numArray2);
    short[] numArray3 = new short[4];
    this.m_decoder.ReadByte(numArray3);
    this.regionBitmapXLocation = this.m_bitOperation.GetInt32(numArray3);
    short[] numArray4 = new short[4];
    this.m_decoder.ReadByte(numArray4);
    this.regionBitmapYLocation = this.m_bitOperation.GetInt32(numArray4);
    this.regionFlags.setFlags((int) this.m_decoder.ReadByte());
  }
}
