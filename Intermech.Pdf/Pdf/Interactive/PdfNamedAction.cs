// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfNamedAction
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;


namespace Syncfusion.Pdf.Interactive
{
    public class PdfNamedAction : PdfAction
    {
      private PdfActionDestination m_destination = PdfActionDestination.NextPage;

      public PdfNamedAction(PdfActionDestination destination) => this.Destination = destination;

      protected override void Initialize()
      {
        base.Initialize();
        this.Dictionary.SetProperty("S", (IPdfPrimitive) new PdfName("Named"));
        this.Dictionary.SetProperty("N", (IPdfPrimitive) new PdfName(this.m_destination.ToString()));
      }

      public PdfActionDestination Destination
      {
        get => this.m_destination;
        set
        {
          if (this.m_destination == value)
            return;
          this.m_destination = value;
          this.Dictionary.SetName("N", this.m_destination.ToString());
        }
      }
    }
}
