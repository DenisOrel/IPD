// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Compression.Segment
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System.Collections.Generic;

#nullable disable
namespace Syncfusion.Pdf.Compression;

internal class Segment
{
  private int deferred_non_retain;
  private uint m_length;
  private uint m_number;
  private uint m_page;
  private List<int> m_referredTo;
  private int m_retainBits;
  private int m_sType;

  internal Segment() => this.ReferredTo = new List<int>();

  internal void Write(List<byte> buf)
  {
    JBIG2Segment jbiG2Segment = new JBIG2Segment();
    jbiG2Segment.Number = JBIG2Statics.Htonl((object) this.Number);
    jbiG2Segment.SType = (byte) this.SType;
    jbiG2Segment.DeferredNonRetain = (byte) this.deferred_non_retain;
    jbiG2Segment.RetainBits = (byte) this.RetainBits;
    jbiG2Segment.SegmentCount = (byte) this.ReferredTo.Count;
    int pageSize = this.PageSize;
    int referenceSize = this.ReferenceSize;
    if (pageSize == 4)
      jbiG2Segment.PageAssocSize = (byte) 1;
    buf.AddRange((IEnumerable<byte>) jbiG2Segment.Serialize());
    foreach (uint num in this.ReferredTo)
    {
      if (referenceSize != 2 && referenceSize != 4)
        buf.Add((byte) num);
    }
    if (pageSize == 4)
      buf.AddRange((IEnumerable<byte>) JBIG2Statics.Htonl(this.Page));
    else
      buf.Add((byte) this.Page);
    buf.AddRange((IEnumerable<byte>) JBIG2Statics.Htonl(this.Length));
  }

  internal uint Length
  {
    get => this.m_length;
    set => this.m_length = value;
  }

  internal uint Number
  {
    get => this.m_number;
    set => this.m_number = value;
  }

  internal uint Page
  {
    get => this.m_page;
    set => this.m_page = value;
  }

  private int PageSize => this.Page > (uint) byte.MaxValue ? 4 : 1;

  private int ReferenceSize
  {
    get
    {
      if (this.Number <= 256U /*0x0100*/)
        return 1;
      return this.Number <= 65536U /*0x010000*/ ? 2 : 4;
    }
  }

  internal List<int> ReferredTo
  {
    get => this.m_referredTo;
    set => this.m_referredTo = value;
  }

  internal int RetainBits
  {
    get => this.m_retainBits;
    set => this.m_retainBits = value;
  }

  private uint Size
  {
    get
    {
      int referenceSize = this.ReferenceSize;
      int pageSize = this.PageSize;
      return 0;
    }
  }

  internal int SType
  {
    get => this.m_sType;
    set => this.m_sType = value;
  }
}
