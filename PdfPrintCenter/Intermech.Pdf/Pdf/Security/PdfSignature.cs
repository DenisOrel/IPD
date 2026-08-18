// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Security.PdfSignature
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Interactive;
using Syncfusion.Pdf.IO;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf.Primitives;
using System;
using System.Drawing;

#nullable disable
namespace Syncfusion.Pdf.Security;

public class PdfSignature
{
  private bool m_certeficated;
  private string m_contactInfo;
  private PdfDocumentBase m_doc;
  private PdfCertificationFlags m_docPermission;
  private bool m_drawSignatureAppearance;
  private PdfSignatureField m_field;
  private string m_location;
  private PdfPageBase m_page;
  private PdfCertificate m_pdfCert;
  private string m_reason;
  private PdfLoadedSignatureField m_sigField;
  private PdfSignatureDictionary m_signatureDictionary;
  private TimeStampServer m_tsrsrv;

  public PdfSignature()
  {
    this.m_docPermission = PdfCertificationFlags.ForbidChanges;
    this.m_drawSignatureAppearance = true;
  }

  public PdfSignature(PdfPage page, PdfCertificate cert, string signatureName)
  {
    this.m_docPermission = PdfCertificationFlags.ForbidChanges;
    if (page == null)
      throw new ArgumentNullException(nameof (page));
    if (cert == null)
      throw new ArgumentNullException(nameof (cert));
    this.m_page = (PdfPageBase) page;
    this.m_pdfCert = cert;
    this.m_field = new PdfSignatureField((PdfPageBase) page, signatureName);
    PdfDocument document = page.Document;
    this.m_doc = (PdfDocumentBase) document;
    document.Form.Fields.Add((PdfField) this.m_field);
    document.Form.SignatureFlags = SignatureFlags.SignaturesExists | SignatureFlags.AppendOnly;
    document.Catalog.BeginSave += new SavePdfPrimitiveEventHandler(this.Catalog_BeginSave);
    this.m_field.Dictionary.BeginSave += new SavePdfPrimitiveEventHandler(this.Dictionary_BeginSave);
    this.m_signatureDictionary = new PdfSignatureDictionary((PdfDocumentBase) document, this, cert);
    document.PdfObjects.Add(((IPdfWrapper) this.m_signatureDictionary).Element);
    if (!document.CrossTable.IsMerging)
      ((IPdfWrapper) this.m_signatureDictionary).Element.Position = -1;
    this.m_field.Dictionary.SetProperty("V", (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) this.m_signatureDictionary));
    this.m_field.Dictionary.SetProperty("Ff", (IPdfPrimitive) new PdfNumber(0));
    this.m_signatureDictionary.Archive = false;
  }

  public PdfSignature(
    PdfDocumentBase document,
    PdfPageBase page,
    PdfCertificate certificate,
    string signatureName)
  {
    this.m_docPermission = PdfCertificationFlags.ForbidChanges;
    if (document == null)
      throw new ArgumentNullException(nameof (document));
    if (page == null)
      throw new ArgumentNullException(nameof (page));
    if (certificate == null)
      throw new ArgumentNullException(nameof (certificate));
    this.m_page = page;
    this.m_pdfCert = certificate;
    this.m_doc = document;
    this.m_field = new PdfSignatureField(page, signatureName);
    PdfForm form = document.GetForm();
    if (form is PdfLoadedForm pdfLoadedForm)
      pdfLoadedForm.Fields.Add((PdfField) this.m_field);
    else
      form.Fields.Add((PdfField) this.m_field);
    form.SignatureFlags = SignatureFlags.SignaturesExists | SignatureFlags.AppendOnly;
    document.Catalog.BeginSave += new SavePdfPrimitiveEventHandler(this.Catalog_BeginSave);
    this.m_field.Dictionary.BeginSave += new SavePdfPrimitiveEventHandler(this.Dictionary_BeginSave);
    this.m_signatureDictionary = new PdfSignatureDictionary(document, this, certificate);
    document.PdfObjects.Add(((IPdfWrapper) this.m_signatureDictionary).Element);
    if (!document.CrossTable.IsMerging)
      ((IPdfWrapper) this.m_signatureDictionary).Element.Position = -1;
    this.m_field.Dictionary.SetProperty("V", (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) this.m_signatureDictionary));
    this.m_field.Dictionary.SetProperty("Ff", (IPdfPrimitive) new PdfNumber(0));
    this.m_signatureDictionary.Archive = false;
  }

  public PdfSignature(
    PdfDocumentBase document,
    PdfPageBase page,
    PdfCertificate certificate,
    string signatureName,
    PdfLoadedSignatureField loadedField)
  {
    this.m_docPermission = PdfCertificationFlags.ForbidChanges;
    if (document == null)
      throw new ArgumentNullException(nameof (document));
    if (page == null)
      throw new ArgumentNullException(nameof (page));
    if (certificate == null)
      throw new ArgumentNullException(nameof (certificate));
    this.m_page = page;
    this.m_pdfCert = certificate;
    this.m_doc = document;
    this.m_sigField = loadedField;
    if (document.GetForm() is PdfLoadedForm form && this.m_sigField.Form == null)
      form.Fields.Add((PdfField) this.m_sigField);
    form.SignatureFlags = SignatureFlags.SignaturesExists | SignatureFlags.AppendOnly;
    document.Catalog.BeginSave += new SavePdfPrimitiveEventHandler(this.Catalog_BeginSave);
    this.m_sigField.Dictionary.BeginSave += new SavePdfPrimitiveEventHandler(this.Dictionary_BeginSave);
    this.m_signatureDictionary = new PdfSignatureDictionary(document, this, certificate);
    document.PdfObjects.Add(((IPdfWrapper) this.m_signatureDictionary).Element);
    if (!document.CrossTable.IsMerging)
      ((IPdfWrapper) this.m_signatureDictionary).Element.Position = -1;
    this.m_sigField.Dictionary.SetProperty("V", (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) this.m_signatureDictionary));
    this.m_sigField.Dictionary.SetProperty("Ff", (IPdfPrimitive) new PdfNumber(0));
    this.m_signatureDictionary.Archive = false;
  }

  private void Catalog_BeginSave(object sender, SavePdfPrimitiveEventArgs ars)
  {
    if (!this.m_certeficated)
      return;
    if (!(PdfCrossTable.Dereference(this.m_doc.Catalog["Perms"]) is PdfDictionary pdfDictionary))
    {
      this.m_doc.Catalog["Perms"] = (IPdfPrimitive) new PdfDictionary()
      {
        ["DocMDP"] = (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) this.m_signatureDictionary)
      };
    }
    else
    {
      if (pdfDictionary.ContainsKey("DocMDP"))
        return;
      pdfDictionary.SetProperty("DocMDP", (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) this.m_signatureDictionary));
    }
  }

  private void Dictionary_BeginSave(object sender, SavePdfPrimitiveEventArgs ars)
  {
    if (this.m_field != null)
      this.m_field.Dictionary.Encrypt = this.m_doc.Security.Enabled;
    else
      this.m_sigField.Dictionary.Encrypt = this.m_doc.Security.Enabled;
  }

  public PdfAppearance Appearence => this.m_field.Appearance;

  public RectangleF Bounds
  {
    get => this.m_field.Bounds;
    set => this.m_field.Bounds = value;
  }

  public PdfCertificate Certificate
  {
    get => this.m_pdfCert;
    set => this.m_pdfCert = value;
  }

  public bool Certificated
  {
    get => this.m_certeficated;
    set
    {
      if (PdfCrossTable.Dereference(this.m_doc.Catalog["Perms"]) is PdfDictionary pdfDictionary && pdfDictionary.ContainsKey("DocMDP"))
        throw new ArgumentException("The document may contain at most one author signature!");
      this.m_certeficated = value;
    }
  }

  public string ContactInfo
  {
    get => this.m_contactInfo;
    set => this.m_contactInfo = value;
  }

  public PdfCertificationFlags DocumentPermissions
  {
    get => this.m_docPermission;
    set => this.m_docPermission = value;
  }

  internal bool DrawFieldAppearance => this.m_drawSignatureAppearance;

  internal PdfField Field
  {
    get => this.m_field == null ? (PdfField) this.m_sigField : (PdfField) this.m_field;
  }

  public PointF Location
  {
    get => this.m_field.Location;
    set => this.m_field.Location = value;
  }

  public string LocationInfo
  {
    get => this.m_location;
    set => this.m_location = value;
  }

  public string Reason
  {
    get => this.m_reason;
    set => this.m_reason = value;
  }

  public TimeStampServer TimeStampServer
  {
    get => this.m_tsrsrv;
    set => this.m_tsrsrv = value;
  }

  public bool Visible
  {
    get
    {
      SizeF size = this.m_field.Size;
      return (double) size.Height != 0.0 || (double) size.Width != 0.0;
    }
  }
}
