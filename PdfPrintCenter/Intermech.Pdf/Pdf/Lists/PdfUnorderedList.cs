// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Lists.PdfUnorderedList
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using System;

#nullable disable
namespace Syncfusion.Pdf.Lists;

public class PdfUnorderedList : PdfList
{
  private PdfUnorderedMarker m_marker;

  public PdfUnorderedList()
    : this(PdfUnorderedList.CreateMarker(PdfUnorderedMarkerStyle.Disk))
  {
  }

  public PdfUnorderedList(PdfFont font)
    : base(font)
  {
    PdfUnorderedList.CreateMarker(PdfUnorderedMarkerStyle.Disk);
  }

  public PdfUnorderedList(PdfListItemCollection items)
    : this(items, PdfUnorderedList.CreateMarker(PdfUnorderedMarkerStyle.Disk))
  {
  }

  public PdfUnorderedList(PdfUnorderedMarker marker) => this.Marker = marker;

  public PdfUnorderedList(string text)
    : this(text, PdfUnorderedList.CreateMarker(PdfUnorderedMarkerStyle.Disk))
  {
  }

  public PdfUnorderedList(PdfListItemCollection items, PdfUnorderedMarker marker)
    : base(items)
  {
    this.Marker = marker;
  }

  public PdfUnorderedList(string text, PdfUnorderedMarker marker)
    : this(PdfList.CreateItems(text), marker)
  {
  }

  private static PdfUnorderedMarker CreateMarker(PdfUnorderedMarkerStyle style)
  {
    return new PdfUnorderedMarker(style);
  }

  public PdfUnorderedMarker Marker
  {
    get => this.m_marker;
    set => this.m_marker = value != null ? value : throw new ArgumentNullException("marker");
  }
}
