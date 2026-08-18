// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.PdfPortfolioInformation
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Interactive;
using Syncfusion.Pdf.Primitives;

#nullable disable
namespace Syncfusion.Pdf;

public class PdfPortfolioInformation : IPdfWrapper
{
  private PdfCatalog m_catalog;
  private PdfDictionary m_dictionary;
  private PdfPortfolioSchema m_Schema;
  private PdfAttachment m_startupDocument;
  private PdfPortfolioViewMode m_viewMode;

  public PdfPortfolioInformation()
  {
    this.m_dictionary = new PdfDictionary();
    this.Initialize();
  }

  internal PdfPortfolioInformation(PdfDictionary portfolioDictionary)
  {
    this.m_dictionary = new PdfDictionary();
    if (portfolioDictionary == null)
      return;
    this.m_dictionary = portfolioDictionary;
    if (this.m_dictionary[nameof (Schema)] is PdfDictionary schemaDictionary)
      this.m_Schema = new PdfPortfolioSchema(schemaDictionary);
    PdfName pdfName = this.m_dictionary["View"] as PdfName;
    if (!(pdfName != (PdfName) null))
      return;
    if (pdfName.Value.Equals("D"))
      this.ViewMode = PdfPortfolioViewMode.Details;
    else if (pdfName.Value.Equals("T"))
    {
      this.ViewMode = PdfPortfolioViewMode.Tile;
    }
    else
    {
      if (!pdfName.Value.Equals("H"))
        return;
      this.ViewMode = PdfPortfolioViewMode.Hidden;
    }
  }

  private void Initialize()
  {
    this.m_dictionary.SetProperty("Type", (IPdfPrimitive) new PdfName("Collection"));
  }

  public PdfPortfolioSchema Schema
  {
    get => this.m_Schema;
    set
    {
      this.m_Schema = value;
      this.m_dictionary.SetProperty(nameof (Schema), (IPdfWrapper) this.m_Schema);
    }
  }

  public PdfAttachment StartupDocument
  {
    get => this.m_startupDocument;
    set
    {
      this.m_startupDocument = value;
      this.m_dictionary.SetProperty("D", (IPdfPrimitive) new PdfString(this.m_startupDocument.FileName));
    }
  }

  IPdfPrimitive IPdfWrapper.Element => (IPdfPrimitive) this.m_dictionary;

  public PdfPortfolioViewMode ViewMode
  {
    get => this.m_viewMode;
    set
    {
      this.m_viewMode = value;
      if (this.m_viewMode == PdfPortfolioViewMode.Details)
        this.m_dictionary.SetProperty("View", (IPdfPrimitive) new PdfName("D"));
      else if (this.m_viewMode == PdfPortfolioViewMode.Hidden)
      {
        this.m_dictionary.SetProperty("View", (IPdfPrimitive) new PdfName("H"));
      }
      else
      {
        if (this.m_viewMode != PdfPortfolioViewMode.Tile)
          return;
        this.m_dictionary.SetProperty("View", (IPdfPrimitive) new PdfName("T"));
      }
    }
  }
}
