// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfGoToAction
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using System;


namespace Syncfusion.Pdf.Interactive
{
    public class PdfGoToAction : PdfAction
    {
      private PdfDestination m_destination;

      public PdfGoToAction(PdfDestination destination)
      {
        this.m_destination = destination != null ? destination : throw new ArgumentNullException(nameof (destination));
      }

      public PdfGoToAction(PdfPage page)
      {
        this.m_destination = page != null ? new PdfDestination((PdfPageBase) page) : throw new ArgumentNullException(nameof (page));
      }

      private void Dictionary_BeginSave(object sender, SavePdfPrimitiveEventArgs ars)
      {
        this.Dictionary.SetProperty("D", (IPdfWrapper) this.m_destination);
      }

      protected override void Initialize()
      {
        base.Initialize();
        this.Dictionary.BeginSave += new SavePdfPrimitiveEventHandler(this.Dictionary_BeginSave);
        this.Dictionary.SetProperty("S", (IPdfPrimitive) new PdfName("GoTo"));
      }

      public PdfDestination Destination
      {
        get => this.m_destination;
        set
        {
          if (value == null)
            throw new ArgumentNullException(nameof (Destination));
          if (value == this.m_destination)
            return;
          this.m_destination = value;
        }
      }
    }
}
