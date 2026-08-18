// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfDocumentLinkAnnotation
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.Drawing;


namespace Syncfusion.Pdf.Interactive
{
    public class PdfDocumentLinkAnnotation : PdfLinkAnnotation
    {
      private PdfDestination m_destination;

      public PdfDocumentLinkAnnotation(RectangleF rectangle)
        : base(rectangle)
      {
      }

      public PdfDocumentLinkAnnotation(RectangleF rectangle, PdfDestination destination)
        : base(rectangle)
      {
        this.Destination = destination != null ? destination : throw new ArgumentNullException(nameof (destination));
      }

      protected override void Save()
      {
        base.Save();
        if (this.m_destination == null)
          return;
        this.Dictionary.SetProperty("Dest", (IPdfWrapper) this.m_destination);
      }

      public PdfDestination Destination
      {
        get => this.m_destination;
        set
        {
          if (value == null)
            throw new ArgumentNullException(nameof (Destination));
          if (this.m_destination == value)
            return;
          this.m_destination = value;
        }
      }
    }
}
