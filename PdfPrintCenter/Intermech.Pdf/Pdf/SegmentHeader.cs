// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.SegmentHeader
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

#nullable disable
namespace Syncfusion.Pdf;

internal class SegmentHeader
{
  private int m_dataLength;
  private bool m_deferredNonRetainSet;
  private int m_pageAssociation;
  private bool m_pageAssociationSizeSet;
  private int m_referredToSegmentCount;
  private int[] m_referredToSegments;
  private short[] m_rententionFlags;
  private int m_segmentNumber;
  private int m_segmentType;

  public void SetSegmentHeaderFlags(short SegmentHeaderFlags)
  {
    this.m_segmentType = (int) SegmentHeaderFlags & 63 /*0x3F*/;
    this.m_pageAssociationSizeSet = ((int) SegmentHeaderFlags & 64 /*0x40*/) == 64 /*0x40*/;
    this.m_deferredNonRetainSet = ((int) SegmentHeaderFlags & 80 /*0x50*/) == 80 /*0x50*/;
  }

  internal int DataLength
  {
    get => this.m_dataLength;
    set => this.m_dataLength = value;
  }

  internal bool IsDeferredNonRetainSet => this.m_deferredNonRetainSet;

  internal bool IsPageAssociationSizeSet => this.m_pageAssociationSizeSet;

  internal int PageAssociation
  {
    get => this.m_pageAssociation;
    set => this.m_pageAssociation = value;
  }

  internal int ReferedToSegCount
  {
    get => this.m_referredToSegmentCount;
    set => this.m_referredToSegmentCount = value;
  }

  internal int[] ReferredToSegments
  {
    get => this.m_referredToSegments;
    set => this.m_referredToSegments = value;
  }

  internal short[] RententionFlags
  {
    get => this.m_rententionFlags;
    set => this.m_rententionFlags = value;
  }

  internal int SegmentNumber
  {
    get => this.m_segmentNumber;
    set => this.m_segmentNumber = value;
  }

  internal int SegmentType
  {
    get => this.m_segmentType;
    set => this.m_segmentType = value;
  }
}
