// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Compression.JBIG2FileHeader
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

#nullable disable
namespace Syncfusion.Pdf.Compression;

internal struct JBIG2FileHeader
{
  private byte[] m_id;
  private byte m_organisationType;
  private byte m_unknownNPages;
  private byte m_reserved;
  private uint m_nPages;

  internal byte[] Id
  {
    get => this.m_id;
    set => this.m_id = value;
  }

  internal byte OrganisationType
  {
    get => this.m_organisationType;
    set => this.m_organisationType = value;
  }

  internal byte UnknownNPages
  {
    get => this.m_unknownNPages;
    set => this.m_unknownNPages = value;
  }

  internal byte Reserved
  {
    get => this.m_reserved;
    set => this.m_reserved = value;
  }

  internal uint NPages
  {
    get => this.m_nPages;
    set => this.m_nPages = value;
  }
}
