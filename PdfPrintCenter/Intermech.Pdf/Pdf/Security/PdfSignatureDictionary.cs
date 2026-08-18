// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Security.PdfSignatureDictionary
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.IO;
using Syncfusion.Pdf.Primitives;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.Text;

#nullable disable
namespace Syncfusion.Pdf.Security;

internal class PdfSignatureDictionary : IPdfWrapper
{
  private const string c_DocMdp = "DocMDP";
  private const string c_FilterType = "adbe.pkcs7.detached";
  private const string c_TransParam = "TransformParams";
  private const string c_Type = "Sig";
  private PdfCertificate m_cert;
  private PdfDictionary m_dictionary = new PdfDictionary();
  private PdfDocumentBase m_doc;
  private int m_docDigestPosition;
  private int m_fieldsDigestPosition;
  private int m_firstRangeLength;
  private int m_secondRangeIndex;
  private PdfSignature m_sig;
  private int m_startPositionByteRange;

  internal PdfSignatureDictionary(PdfDocumentBase doc, PdfSignature sig, PdfCertificate cert)
  {
    if (doc == null)
      throw new ArgumentNullException(nameof (doc));
    if (sig == null)
      throw new ArgumentNullException(nameof (sig));
    if (cert == null)
      throw new ArgumentNullException(nameof (cert));
    this.m_doc = doc;
    this.m_sig = sig;
    this.m_cert = cert;
    doc.DocumentSaved += new PdfDocumentBase.DocumentSavedEventHandler(this.DocumentSaved);
    this.m_dictionary.BeginSave += new SavePdfPrimitiveEventHandler(this.Dictionary_BeginSave);
  }

  private void AddContactInfo()
  {
    if (this.m_sig.ContactInfo == null)
      return;
    this.m_dictionary.SetProperty("ContactInfo", (IPdfPrimitive) new PdfString(this.m_sig.ContactInfo));
  }

  private void AddContents(IPdfWriter writer)
  {
    writer.Write("/Contents ");
    this.m_firstRangeLength = (int) writer.Position;
    uint num = (uint) writer.Position + 10000U;
    byte[] data = new byte[this.m_sig.TimeStampServer == null ? (int) this.m_cert.GetSignatureLength() * 2 + 2 : (int) num * 2 + 2];
    writer.Write(data);
    this.m_secondRangeIndex = (int) writer.Position;
    writer.Write("\r\n");
  }

  private void AddDate()
  {
    this.m_dictionary.SetProperty("M", (IPdfPrimitive) new PdfString($"D:{DateTime.Now:yyyyMMddHHmmss}"));
  }

  private void AddDigest(IPdfWriter writer)
  {
    if (!this.AllowMDP())
      return;
    PdfDictionary catalog = (PdfDictionary) writer.Document.Catalog;
    writer.Write((IPdfPrimitive) new PdfName("Reference"));
    writer.Write("[");
    writer.Write("<<");
    writer.Write("/TransformParams");
    PdfDictionary pdfObject1 = new PdfDictionary();
    int documentPermissions = (int) this.m_sig.DocumentPermissions;
    pdfObject1["V"] = (IPdfPrimitive) new PdfName("1.2");
    pdfObject1["P"] = (IPdfPrimitive) new PdfNumber(documentPermissions);
    pdfObject1["Type"] = (IPdfPrimitive) new PdfName("TransformParams");
    writer.Write((IPdfPrimitive) pdfObject1);
    writer.Write((IPdfPrimitive) new PdfName("TransformMethod"));
    writer.Write((IPdfPrimitive) new PdfName("DocMDP"));
    writer.Write((IPdfPrimitive) new PdfName("Type"));
    writer.Write((IPdfPrimitive) new PdfName("SigRef"));
    writer.Write((IPdfPrimitive) new PdfName("DigestValue"));
    int position1 = (int) writer.Position;
    this.m_docDigestPosition = position1;
    writer.Write((IPdfPrimitive) new PdfString(new byte[16 /*0x10*/]));
    PdfArray pdfObject2 = new PdfArray();
    pdfObject2.Add((IPdfPrimitive) new PdfNumber(position1));
    pdfObject2.Add((IPdfPrimitive) new PdfNumber(34));
    writer.Write((IPdfPrimitive) new PdfName("DigestLocation"));
    writer.Write((IPdfPrimitive) pdfObject2);
    writer.Write((IPdfPrimitive) new PdfName("DigestMethod"));
    writer.Write((IPdfPrimitive) new PdfName("MD5"));
    writer.Write((IPdfPrimitive) new PdfName("Data"));
    PdfReferenceHolder pdfObject3 = new PdfReferenceHolder((IPdfPrimitive) catalog);
    writer.Write(" ");
    writer.Write((IPdfPrimitive) pdfObject3);
    writer.Write(">>");
    writer.Write("<<");
    writer.Write((IPdfPrimitive) new PdfName("TransformParams"));
    writer.Write((IPdfPrimitive) new PdfDictionary()
    {
      ["V"] = (IPdfPrimitive) new PdfName("1.2"),
      ["Fields"] = (IPdfPrimitive) new PdfArray()
      {
        (IPdfPrimitive) new PdfString(this.m_sig.Field.Name)
      },
      ["Type"] = (IPdfPrimitive) new PdfName("TransformParams"),
      ["Action"] = (IPdfPrimitive) new PdfName("Include")
    });
    writer.Write((IPdfPrimitive) new PdfName("TransformMethod"));
    writer.Write((IPdfPrimitive) new PdfName("FieldMDP"));
    writer.Write((IPdfPrimitive) new PdfName("Type"));
    writer.Write((IPdfPrimitive) new PdfName("SigRef"));
    writer.Write((IPdfPrimitive) new PdfName("DigestValue"));
    int position2 = (int) writer.Position;
    this.m_fieldsDigestPosition = position2;
    writer.Write((IPdfPrimitive) new PdfString(new byte[16 /*0x10*/]));
    PdfArray pdfObject4 = new PdfArray();
    pdfObject4.Add((IPdfPrimitive) new PdfNumber(position2));
    pdfObject4.Add((IPdfPrimitive) new PdfNumber(34));
    writer.Write((IPdfPrimitive) new PdfName("DigestLocation"));
    writer.Write((IPdfPrimitive) pdfObject4);
    writer.Write((IPdfPrimitive) new PdfName("DigestMethod"));
    writer.Write((IPdfPrimitive) new PdfName("MD5"));
    writer.Write((IPdfPrimitive) new PdfName("Data"));
    writer.Write(" ");
    writer.Write((IPdfPrimitive) new PdfReferenceHolder((IPdfPrimitive) catalog));
    writer.Write(">>");
    writer.Write("]");
    writer.Write(" ");
  }

  private void AddFilter() => this.m_dictionary.SetName("Filter", "Adobe.PPKMS");

  private void AddLocation()
  {
    if (this.m_sig.LocationInfo == null)
      return;
    this.m_dictionary.SetProperty("Location", (IPdfPrimitive) new PdfString(this.m_sig.LocationInfo));
  }

  private void AddName()
  {
    this.m_dictionary.SetProperty("Name", (IPdfPrimitive) new PdfString(this.m_cert.IssuerName));
  }

  private void AddOptionalItems()
  {
    this.AddReason();
    this.AddLocation();
    this.AddContactInfo();
  }

  private void AddRange(IPdfWriter writer)
  {
    writer.Write("/ByteRange [");
    this.m_startPositionByteRange = (int) writer.Position;
    for (int index = 0; index < 32 /*0x20*/; ++index)
      writer.Write(" ");
    writer.Write("]\r\n");
  }

  private void AddReason()
  {
    if (this.m_sig.Reason == null)
      return;
    this.m_dictionary.SetProperty("Reason", (IPdfPrimitive) new PdfString(this.m_sig.Reason));
  }

  private void AddReference()
  {
    PdfDictionary pdfDictionary = new PdfDictionary();
    PdfDictionary element = new PdfDictionary();
    PdfArray primitive = new PdfArray();
    int documentPermissions = (int) this.m_sig.DocumentPermissions;
    pdfDictionary["V"] = (IPdfPrimitive) new PdfName("1.2");
    pdfDictionary["P"] = (IPdfPrimitive) new PdfNumber(documentPermissions);
    pdfDictionary["Type"] = (IPdfPrimitive) new PdfName("TransformParams");
    element["TransformMethod"] = (IPdfPrimitive) new PdfName("DocMDP");
    element["Type"] = (IPdfPrimitive) new PdfName("SigRef");
    element["TransformParams"] = (IPdfPrimitive) pdfDictionary;
    primitive.Add((IPdfPrimitive) element);
    this.m_dictionary.SetProperty("Reference", (IPdfPrimitive) primitive);
  }

  private void AddRequiredItems()
  {
    if (this.m_sig.Certificated && this.AllowMDP())
      this.AddReference();
    this.AddType();
    this.AddName();
    this.AddDate();
    this.AddFilter();
    this.AddSubFilter();
  }

  private void AddSubFilter() => this.m_dictionary.SetName("SubFilter", "adbe.pkcs7.detached");

  private void AddType() => this.m_dictionary.SetName("Type", "Sig");

  private bool AllowMDP()
  {
    return this.m_dictionary.Equals((object) PdfCrossTable.Dereference((PdfCrossTable.Dereference(this.m_doc.Catalog["Perms"]) as PdfDictionary)["DocMDP"]));
  }

  private int CreateAsn1TspRequest(byte[] sha1Hash, Stream input)
  {
    byte[] buffer1 = new byte[18]
    {
      (byte) 48 /*0x30*/,
      (byte) 39,
      (byte) 2,
      (byte) 1,
      (byte) 1,
      (byte) 48 /*0x30*/,
      (byte) 31 /*0x1F*/,
      (byte) 48 /*0x30*/,
      (byte) 7,
      (byte) 6,
      (byte) 5,
      (byte) 43,
      (byte) 14,
      (byte) 3,
      (byte) 2,
      (byte) 26,
      (byte) 4,
      (byte) 20
    };
    byte[] buffer2 = new byte[3]
    {
      (byte) 1,
      (byte) 1,
      byte.MaxValue
    };
    input.Write(buffer1, 0, buffer1.Length);
    input.Write(sha1Hash, 0, sha1Hash.Length);
    input.Write(buffer2, 0, buffer2.Length);
    return buffer1.Length + sha1Hash.Length + buffer2.Length;
  }

  private void Dictionary_BeginSave(object sender, SavePdfPrimitiveEventArgs args)
  {
    bool enabled = this.m_doc.Security.Enabled;
    this.m_dictionary.Encrypt = enabled;
    this.AddRequiredItems();
    this.AddOptionalItems();
    this.m_doc.Security.Enabled = false;
    this.AddContents(args.Writer);
    this.AddRange(args.Writer);
    if (this.m_sig.Certificated)
      this.AddDigest(args.Writer);
    this.m_doc.Security.Enabled = enabled;
  }

  private void DocumentSaved(object sender, DocumentSavedEventArgs e)
  {
    if (sender == null)
      throw new ArgumentNullException(nameof (sender));
    if (e == null)
      throw new ArgumentNullException(nameof (e));
    bool enabled = this.m_doc.Security.Enabled;
    this.m_doc.Security.Enabled = false;
    PdfWriter writer = e.Writer;
    byte[] buffer1 = new byte[this.m_firstRangeLength];
    int length1 = (int) e.Writer.Length - this.m_secondRangeIndex;
    byte[] buffer2 = new byte[length1];
    string str1 = "0 ";
    string str2 = this.m_firstRangeLength.ToString() + " ";
    string str3 = this.m_secondRangeIndex.ToString() + " ";
    string str4 = length1.ToString();
    int startPosition1 = this.SaveRangeItem(writer, str1, this.m_startPositionByteRange);
    int startPosition2 = this.SaveRangeItem(writer, str2, startPosition1);
    int startPosition3 = this.SaveRangeItem(writer, str3, startPosition2);
    this.SaveRangeItem(e.Writer, str4, startPosition3);
    if (this.m_sig.Certificated && this.AllowMDP())
    {
      byte[] numArray1 = new PdfSignatureDigest().HashDocument(e.Writer.Document);
      e.Writer.Position = (long) this.m_docDigestPosition;
      e.Writer.Write((IPdfPrimitive) new PdfString(numArray1));
      byte[] numArray2 = new PdfSignatureDigest().HashSignatureFields(this.m_sig.Field.Page as PdfPage);
      e.Writer.Position = (long) this.m_fieldsDigestPosition;
      e.Writer.Write((IPdfPrimitive) new PdfString(numArray2));
    }
    Stream stream = writer.GetStream();
    writer.Position = 0L;
    stream.Read(buffer1, 0, buffer1.Length);
    writer.Position = (long) this.m_secondRangeIndex;
    stream.Read(buffer2, 0, buffer2.Length);
    PdfString pdfString = new PdfString(this.m_cert.GetSignatureValue(new byte[2][]
    {
      buffer1,
      buffer2
    }));
    e.Writer.Position = (long) this.m_firstRangeLength;
    e.Writer.Write(pdfString.PdfEncode(writer.Document));
    byte[] numArray = new byte[buffer1.Length + buffer2.Length];
    buffer1.CopyTo((Array) numArray, 0);
    buffer2.CopyTo((Array) numArray, buffer1.Length);
    if (this.m_sig.TimeStampServer != null)
    {
      TimeStampResponse timeStampResponse = new TimeStampResponse(this.m_sig.TimeStampServer.GetTimeStampResponse(new TimeStampRequest(true).GetAsnEncodedTimestampRequest(SHA1.Create().ComputeHash(numArray))));
      byte[] encoded = timeStampResponse.GetEncoded(timeStampResponse.Object);
      CmsSigner signer = new CmsSigner(SubjectIdentifierType.IssuerAndSerialNumber, this.m_sig.Certificate.X509Certificate);
      signer.IncludeOption = X509IncludeOption.EndCertOnly;
      SignedCms signedCms = new SignedCms(new ContentInfo(numArray), false);
      AsnEncodedData asnEncodedData = new AsnEncodedData(new Oid("1.2.840.113549.1.9.16.2.14"), encoded);
      signer.UnsignedAttributes.Add(asnEncodedData);
      signedCms.ComputeSignature(signer);
      byte[] bytes = signedCms.Encode();
      e.Writer.Position = (long) this.m_firstRangeLength;
      e.Writer.Write("<");
      string hex = PdfString.BytesToHex(bytes);
      e.Writer.Write(hex);
      int length2 = (this.m_secondRangeIndex - (int) e.Writer.Position) / 2;
      e.Writer.Write(PdfString.BytesToHex(new byte[length2]));
      e.Writer.Write(">");
    }
    this.m_doc.Security.Enabled = enabled;
  }

  private int SaveRangeItem(PdfWriter writer, string str, int startPosition)
  {
    byte[] bytes = Encoding.UTF8.GetBytes(str);
    writer.Position = (long) startPosition;
    writer.GetStream().Write(bytes, 0, bytes.Length);
    return startPosition + str.Length;
  }

  public bool Archive
  {
    get => this.m_dictionary.Archive;
    set => this.m_dictionary.Archive = value;
  }

  IPdfPrimitive IPdfWrapper.Element => (IPdfPrimitive) this.m_dictionary;
}
