// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfEmbeddedFileSpecification
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using System;
using System.IO;

#nullable disable
namespace Syncfusion.Pdf.Interactive;

public class PdfEmbeddedFileSpecification : PdfFileSpecificationBase
{
  private string m_description;
  private PdfDictionary m_dictionary;
  private EmbeddedFile m_embeddedFile;
  private PdfPortfolioAttributes m_portfolioAttributes;

  public PdfEmbeddedFileSpecification(string fileName)
    : base(fileName)
  {
    this.m_description = string.Empty;
    this.m_dictionary = new PdfDictionary();
    this.m_embeddedFile = new EmbeddedFile(fileName);
    this.Description = fileName;
  }

  public PdfEmbeddedFileSpecification(string fileName, byte[] data)
    : base(fileName)
  {
    this.m_description = string.Empty;
    this.m_dictionary = new PdfDictionary();
    this.m_embeddedFile = data != null ? new EmbeddedFile(fileName, data) : throw new ArgumentNullException(nameof (data));
    this.Description = fileName;
  }

  public PdfEmbeddedFileSpecification(string fileName, Stream stream)
    : base(fileName)
  {
    this.m_description = string.Empty;
    this.m_dictionary = new PdfDictionary();
    this.m_embeddedFile = stream != null ? new EmbeddedFile(fileName, stream) : throw new ArgumentNullException(nameof (stream));
    this.Description = fileName;
  }

  protected override void Initialize()
  {
    base.Initialize();
    this.Dictionary.SetProperty("EF", (IPdfPrimitive) this.m_dictionary);
  }

  protected override void Save()
  {
    this.m_dictionary["F"] = (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) this.m_embeddedFile);
    PdfString primitive = new PdfString(this.FormatFileName(Path.GetFileName(this.FileName), false));
    this.Dictionary.SetProperty("F", (IPdfPrimitive) primitive);
    this.Dictionary.SetProperty("UF", (IPdfPrimitive) primitive);
  }

  public DateTime CreationDate
  {
    get => this.m_embeddedFile.Params.CreationDate;
    set => this.m_embeddedFile.Params.CreationDate = value;
  }

  public byte[] Data
  {
    get => this.m_embeddedFile.Data;
    set => this.m_embeddedFile.Data = value;
  }

  public string Description
  {
    get => this.m_description;
    set
    {
      if (!(this.m_description != value))
        return;
      this.m_description = value;
      this.Dictionary.SetString("Desc", this.m_description);
    }
  }

  public override string FileName
  {
    get => this.m_embeddedFile.FileName;
    set => this.m_embeddedFile.FileName = value;
  }

  public string MimeType
  {
    get => this.m_embeddedFile.MimeType;
    set => this.m_embeddedFile.MimeType = value;
  }

  public DateTime ModificationDate
  {
    get => this.m_embeddedFile.Params.ModificationDate;
    set => this.m_embeddedFile.Params.ModificationDate = value;
  }

  public PdfPortfolioAttributes PortfolioAttributes
  {
    get => this.m_portfolioAttributes;
    set
    {
      this.m_portfolioAttributes = value;
      this.Dictionary.SetProperty("CI", (IPdfWrapper) this.m_portfolioAttributes);
    }
  }
}
