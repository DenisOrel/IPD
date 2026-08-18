// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Parsing.PdfLoadedSignatureField
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Interactive;
using Syncfusion.Pdf.IO;
using Syncfusion.Pdf.Primitives;
using Syncfusion.Pdf.Security;


namespace Syncfusion.Pdf.Parsing
{
    public class PdfLoadedSignatureField : PdfLoadedStyledField
    {
      private PdfSignature m_signature;

      internal PdfLoadedSignatureField(PdfDictionary dictionary, PdfCrossTable crossTable)
        : base(dictionary, crossTable)
      {
        if (!dictionary.ContainsKey("V"))
          return;
        this.SetSignature(dictionary["V"]);
      }

      internal override void BeginSave()
      {
        base.BeginSave();
        if (this.m_signature == null)
          return;
        this.Dictionary["V"] = (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) new PdfSignatureDictionary(this.CrossTable.Document, this.m_signature, this.m_signature.Certificate));
      }

      internal new PdfField Clone(PdfDictionary dictionary, PdfPage page)
      {
        PdfCrossTable crossTable = page.Section.ParentDocument.CrossTable;
        PdfLoadedSignatureField loadedSignatureField = new PdfLoadedSignatureField(dictionary, crossTable);
        loadedSignatureField.Page = (PdfPageBase) page;
        loadedSignatureField.SetName(this.GetFieldName());
        loadedSignatureField.Widget.Dictionary = this.Widget.Dictionary.Clone(crossTable) as PdfDictionary;
        return (PdfField) loadedSignatureField;
      }

      internal override PdfLoadedFieldItem CreateLoadedItem(PdfDictionary dictionary)
      {
        return base.CreateLoadedItem(dictionary);
      }

      private void SetSignature(IPdfPrimitive signature)
      {
        if ((object) (signature as PdfReferenceHolder) == null)
          return;
        PdfDictionary pdfDictionary = (PdfDictionary) (signature as PdfReferenceHolder).Object;
        this.m_signature = new PdfSignature();
        if (pdfDictionary == null)
          return;
        if (pdfDictionary.ContainsKey("Reason"))
          this.m_signature.Reason = (pdfDictionary["Reason"] as PdfString).Value;
        if (pdfDictionary.ContainsKey("Location"))
          this.m_signature.LocationInfo = (pdfDictionary["Location"] as PdfString).Value;
        if (!pdfDictionary.ContainsKey("ContactInfo"))
          return;
        this.m_signature.ContactInfo = (pdfDictionary["ContactInfo"] as PdfString).Value;
      }

      public PdfSignature Signature
      {
        get => this.m_signature;
        set
        {
          this.m_signature = value;
          this.Changed = true;
        }
      }
    }
}
