// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Lists.PdfOrderedList
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using System;


namespace Syncfusion.Pdf.Lists
{
    public class PdfOrderedList : PdfList
    {
      private PdfOrderedMarker m_marker;
      private bool m_useHierarchy;

      public PdfOrderedList()
        : this(PdfOrderedList.CreateMarker(PdfNumberStyle.Numeric))
      {
      }

      public PdfOrderedList(PdfFont font)
        : base(font)
      {
        PdfOrderedList.CreateMarker(PdfNumberStyle.Numeric);
      }

      public PdfOrderedList(PdfListItemCollection items)
        : this(items, PdfOrderedList.CreateMarker(PdfNumberStyle.Numeric))
      {
      }

      public PdfOrderedList(PdfOrderedMarker marker) => this.Marker = marker;

      public PdfOrderedList(PdfNumberStyle style) => this.Marker = PdfOrderedList.CreateMarker(style);

      public PdfOrderedList(string text)
        : this(text, PdfOrderedList.CreateMarker(PdfNumberStyle.Numeric))
      {
      }

      public PdfOrderedList(PdfListItemCollection items, PdfOrderedMarker marker)
        : base(items)
      {
        this.Marker = marker;
      }

      public PdfOrderedList(string text, PdfOrderedMarker marker)
        : this(PdfList.CreateItems(text), marker)
      {
      }

      private static PdfOrderedMarker CreateMarker(PdfNumberStyle style)
      {
        return new PdfOrderedMarker(style, (PdfFont) null);
      }

      public PdfOrderedMarker Marker
      {
        get => this.m_marker;
        set => this.m_marker = value != null ? value : throw new ArgumentNullException("marker");
      }

      public bool MarkerHierarchy
      {
        get => this.m_useHierarchy;
        set => this.m_useHierarchy = value;
      }
    }
}
