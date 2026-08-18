// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Xmp.XmpMetadata
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using System;
using System.IO;
using System.Xml;

#nullable disable
namespace Syncfusion.Pdf.Xmp;

public class XmpMetadata : IPdfWrapper
{
  protected internal const string c_dublinSchema = "http://purl.org/dc/elements/1.1/";
  private const string c_endPacket = "end=\"r\"";
  protected internal const string c_pdfschema = "http://ns.adobe.com/pdf/1.3/";
  protected internal const string c_rdfPdfa = "http://www.aiim.org/pdfa/ns/id/";
  protected internal const string c_rdfPrefix = "rdf";
  protected internal const string c_rdfUri = "http://www.w3.org/1999/02/22-rdf-syntax-ns#";
  private const string c_startPacket = "begin=\"\uFEFF\" id=\"W5M0MpCehiHzreSzNTczkc9d\"";
  protected internal const string c_xap = "http://ns.adobe.com/xap/1.0/";
  protected internal const string c_xmlnsPrefix = "xmlns";
  protected internal const string c_xmlnsUri = "http://www.w3.org/2000/xmlns/";
  protected internal const string c_xmlPefix = "xml";
  protected internal const string c_xmlUri = "http://www.w3.org/XML/1998/namespace";
  private const string c_xmpMetaUri = "adobe:ns:meta";
  protected internal const string c_xpathRdf = "/x:xmpmeta/rdf:RDF";
  internal bool isLoadedDocument;
  private BasicJobTicketSchema m_basicJobTicketSchema;
  private BasicSchema m_basicSchema;
  private DublinCoreSchema m_dublinCoreSchema;
  private XmlNamespaceManager m_nmpManager;
  private PagedTextSchema m_pagedTextSchemaSchema;
  private PDFSchema m_pdfSchema;
  private RightsManagementSchema m_rightsManagementSchema;
  private PdfStream m_stream;
  private XmlDocument m_xmlDocument;

  public XmpMetadata(PdfDocumentInformation documentInfo) => this.Init(documentInfo);

  public XmpMetadata(XmlDocument xmp)
  {
    if (xmp == null)
      throw new ArgumentNullException("xmpMetadata");
    this.Load(xmp);
  }

  public void Add(XmlElement schema)
  {
    schema = schema != null ? this.XmlData.ImportNode((XmlNode) schema, true) as XmlElement : throw new ArgumentNullException(nameof (schema));
    this.ImportNamespaces(schema, this.m_nmpManager);
    this.Rdf.AppendChild((XmlNode) schema);
  }

  internal string AddNamespace(string prefix, string namespaceURI)
  {
    if (prefix == null)
      throw new ArgumentNullException(nameof (prefix));
    string str = namespaceURI;
    if (this.NamespaceManager.HasNamespace(prefix) || !(prefix != "xml") || !(prefix != "xmlns"))
      return this.NamespaceManager.LookupNamespace(prefix);
    if (namespaceURI == null)
      throw new ArgumentNullException(nameof (namespaceURI));
    this.NamespaceManager.AddNamespace(prefix, namespaceURI);
    return str;
  }

  private void BeginSave(object sender, SavePdfPrimitiveEventArgs ars)
  {
    this.XmlData.Save((Stream) this.m_stream.InternalStream);
  }

  internal XmlAttribute CreateAttribute(string name, string value)
  {
    if (name == null)
      throw new ArgumentNullException(nameof (name));
    if (value == null)
      throw new ArgumentNullException(nameof (value));
    XmlAttribute attribute = this.XmlData.CreateAttribute(name);
    attribute.Value = value;
    return attribute;
  }

  internal XmlAttribute CreateAttribute(
    string prefix,
    string localName,
    string namespaceURI,
    string value)
  {
    if (prefix == null)
      throw new ArgumentNullException(nameof (prefix));
    if (localName == null)
      throw new ArgumentNullException(nameof (localName));
    if (value == null)
      throw new ArgumentNullException(nameof (value));
    namespaceURI = this.AddNamespace(prefix, namespaceURI);
    XmlAttribute attribute = this.XmlData.CreateAttribute(prefix, localName, namespaceURI);
    attribute.Value = value;
    return attribute;
  }

  private void CreateDublinCoreContainer(
    XmlElement rdf,
    XmlElement dublinDesc,
    string containerName,
    string value,
    bool defaultLang,
    XmpArrayType element)
  {
    if (!string.IsNullOrEmpty(value))
    {
      XmlElement element1 = this.CreateElement("dc", containerName, "http://purl.org/dc/elements/1.1/");
      XmlElement element2 = this.CreateElement(nameof (rdf), element.ToString(), "http://purl.org/dc/elements/1.1/");
      XmlElement element3 = this.CreateElement(nameof (rdf), "li", "http://purl.org/dc/elements/1.1/");
      element3.InnerText = value;
      element2.AppendChild((XmlNode) element3);
      element1.AppendChild((XmlNode) element2);
      dublinDesc.AppendChild((XmlNode) element1);
      if (defaultLang)
      {
        XmlAttribute attribute = this.CreateAttribute("xml", "lang", "http://purl.org/dc/elements/1.1/", "x-default");
        element3.Attributes.Append(attribute);
      }
    }
    rdf.AppendChild((XmlNode) dublinDesc);
  }

  internal XmlElement CreateElement(string name)
  {
    return name != null ? this.XmlData.CreateElement(name) : throw new ArgumentNullException(nameof (name));
  }

  internal XmlElement CreateElement(string prefix, string localName, string namespaceURI)
  {
    if (prefix == null)
      throw new ArgumentNullException(nameof (prefix));
    if (localName == null)
      throw new ArgumentNullException(nameof (localName));
    namespaceURI = this.AddNamespace(prefix, namespaceURI);
    return this.XmlData.CreateElement(prefix, localName, namespaceURI);
  }

  private void CreateEndPacket()
  {
    this.XmlData.AppendChild((XmlNode) this.XmlData.CreateProcessingInstruction("xpacket", "end=\"r\""));
  }

  private void CreateRdf(PdfDocumentInformation documentInfo)
  {
    XmlElement element1 = this.CreateElement("rdf", "RDF", "http://www.w3.org/1999/02/22-rdf-syntax-ns#");
    if (PdfDocument.ConformanceLevel == PdfConformanceLevel.Pdf_A1B)
    {
      if (!string.IsNullOrEmpty(documentInfo.Producer) || !string.IsNullOrEmpty(documentInfo.Keywords))
      {
        this.NamespaceManager.AddNamespace("pdf", "http://ns.adobe.com/pdf/1.3/");
        XmlElement element2 = this.CreateElement("rdf", "Description", "http://ns.adobe.com/pdf/1.3/");
        XmlAttribute attribute = this.CreateAttribute("xmlns", "pdf", "http://ns.adobe.com/pdf/1.3/", "http://ns.adobe.com/pdf/1.3/");
        element2.Attributes.Append(attribute);
        if (!string.IsNullOrEmpty(documentInfo.Producer))
        {
          XmlElement element3 = this.CreateElement("pdf", "Producer", "http://ns.adobe.com/pdf/1.3/");
          element3.InnerText = documentInfo.Producer;
          element2.AppendChild((XmlNode) element3);
        }
        if (!string.IsNullOrEmpty(documentInfo.Keywords))
        {
          XmlElement element4 = this.CreateElement("pdf", "Keywords", "http://ns.adobe.com/pdf/1.3/");
          element4.InnerText = documentInfo.Keywords;
          element2.AppendChild((XmlNode) element4);
        }
        this.Xmpmeta.AppendChild((XmlNode) element2);
        element1.AppendChild((XmlNode) element2);
      }
      XmlElement element5 = this.CreateElement("rdf", "Description", "http://ns.adobe.com/pdf/1.3/");
      XmlAttribute attribute1 = this.CreateAttribute("xmlns", "dc", "http://purl.org/dc/elements/1.1/", "http://purl.org/dc/elements/1.1/");
      element5.Attributes.Append(attribute1);
      XmlElement element6 = this.CreateElement("dc", "format", "http://purl.org/dc/elements/1.1/");
      element6.InnerText = "application/pdf";
      element5.AppendChild((XmlNode) element6);
      this.CreateDublinCoreContainer(element1, element5, "title", documentInfo.Title, true, XmpArrayType.Alt);
      this.CreateDublinCoreContainer(element1, element5, "description", documentInfo.Subject, true, XmpArrayType.Alt);
      this.CreateDublinCoreContainer(element1, element5, "subject", documentInfo.Keywords, false, XmpArrayType.Bag);
      this.CreateDublinCoreContainer(element1, element5, "creator", documentInfo.Author, false, XmpArrayType.Seq);
      this.NamespaceManager.AddNamespace("pdfaid", "http://www.aiim.org/pdfa/ns/id/");
      XmlElement element7 = this.CreateElement("rdf", "Description", "http://www.w3.org/1999/02/22-rdf-syntax-ns#");
      XmlAttribute attribute2 = this.CreateAttribute("rdf", "about", "http://www.w3.org/1999/02/22-rdf-syntax-ns#", " ");
      XmlAttribute attribute3 = this.CreateAttribute("pdfaid", "part", "http://www.aiim.org/pdfa/ns/id/", "1");
      XmlAttribute attribute4 = this.CreateAttribute("pdfaid", "conformance", "http://www.aiim.org/pdfa/ns/id/", "B");
      element7.Attributes.Append(attribute2);
      element7.Attributes.Append(attribute3);
      element7.Attributes.Append(attribute4);
      this.Xmpmeta.AppendChild((XmlNode) element7);
      element1.AppendChild((XmlNode) element7);
    }
    this.Xmpmeta.AppendChild((XmlNode) element1);
  }

  private void CreateStartPacket()
  {
    this.XmlData.AppendChild((XmlNode) this.XmlData.CreateProcessingInstruction("xpacket", "begin=\"\uFEFF\" id=\"W5M0MpCehiHzreSzNTczkc9d\""));
  }

  private void CreateXmpmeta()
  {
    this.XmlData.AppendChild((XmlNode) this.CreateElement("x", "xmpmeta", "adobe:ns:meta"));
  }

  private void EndSave(object sender, SavePdfPrimitiveEventArgs ars) => this.m_stream.Clear();

  private void ImportNamespaces(XmlElement elm, XmlNamespaceManager nsm)
  {
    if (elm == null)
      throw new ArgumentNullException(nameof (elm));
    if (nsm == null)
      throw new ArgumentNullException(nameof (nsm));
    string prefix = elm.Prefix;
    string namespaceUri = elm.NamespaceURI;
    if (prefix != null && prefix.Length > 0 && namespaceUri != null && !nsm.HasNamespace(prefix))
      nsm.AddNamespace(prefix, namespaceUri);
    if (!elm.HasChildNodes)
      return;
    for (XmlNode xmlNode = elm.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
    {
      XmlNode elm1 = xmlNode;
      if (elm1.NodeType == XmlNodeType.Element)
        this.ImportNamespaces(elm1 as XmlElement, nsm);
    }
  }

  private void Init(PdfDocumentInformation documentInfo)
  {
    this.m_xmlDocument = new XmlDocument();
    this.m_nmpManager = new XmlNamespaceManager(this.XmlData.NameTable);
    this.m_stream = new PdfStream();
    this.InitStream();
    this.CreateStartPacket();
    this.CreateXmpmeta();
    this.CreateRdf(documentInfo);
    this.CreateEndPacket();
  }

  private void InitStream()
  {
    this.m_stream.BeginSave += new SavePdfPrimitiveEventHandler(this.BeginSave);
    this.m_stream.EndSave += new SavePdfPrimitiveEventHandler(this.EndSave);
    this.m_stream["Type"] = (IPdfPrimitive) new PdfName("Metadata");
    this.m_stream["Subtype"] = (IPdfPrimitive) new PdfName("XML");
    this.m_stream.Compress = false;
  }

  public void Load(XmlDocument xmp)
  {
    if (xmp == null)
      throw new ArgumentNullException(nameof (xmp));
    this.Reset();
    this.m_xmlDocument = xmp;
    this.m_nmpManager = new XmlNamespaceManager(this.m_xmlDocument.NameTable);
    this.ImportNamespaces(this.m_xmlDocument.DocumentElement, this.m_nmpManager);
  }

  private void Reset()
  {
    this.m_xmlDocument = (XmlDocument) null;
    this.m_nmpManager = (XmlNamespaceManager) null;
    this.m_dublinCoreSchema = (DublinCoreSchema) null;
  }

  public BasicJobTicketSchema BasicJobTicketSchema
  {
    get
    {
      if (this.m_basicJobTicketSchema == null)
        this.m_basicJobTicketSchema = new BasicJobTicketSchema(this);
      return this.m_basicJobTicketSchema;
    }
  }

  public BasicSchema BasicSchema
  {
    get
    {
      if (this.m_basicSchema == null)
        this.m_basicSchema = new BasicSchema(this);
      return this.m_basicSchema;
    }
  }

  public DublinCoreSchema DublinCoreSchema
  {
    get
    {
      if (this.m_dublinCoreSchema == null)
        this.m_dublinCoreSchema = new DublinCoreSchema(this);
      return this.m_dublinCoreSchema;
    }
  }

  public XmlNamespaceManager NamespaceManager => this.m_nmpManager;

  public PagedTextSchema PagedTextSchema
  {
    get
    {
      if (this.m_pagedTextSchemaSchema == null)
        this.m_pagedTextSchemaSchema = new PagedTextSchema(this);
      return this.m_pagedTextSchemaSchema;
    }
  }

  public PDFSchema PDFSchema
  {
    get
    {
      if (this.m_pdfSchema == null)
        this.m_pdfSchema = new PDFSchema(this);
      return this.m_pdfSchema;
    }
  }

  internal XmlElement Rdf
  {
    get
    {
      string xpath = "/x:xmpmeta/rdf:RDF";
      if (!this.XmlData.DocumentElement.Prefix.Equals("x"))
        xpath = this.XmlData.DocumentElement.Name;
      XmlNode rdf = this.XmlData.SelectSingleNode(xpath, this.NamespaceManager);
      if (rdf == null)
      {
        rdf = this.XmlData.SelectSingleNode($"/{this.XmlData.DocumentElement.Name}/rdf:RDF", this.NamespaceManager);
        if (rdf == null)
          throw new ArgumentNullException("node");
      }
      return rdf as XmlElement;
    }
  }

  public RightsManagementSchema RightsManagementSchema
  {
    get
    {
      if (this.m_rightsManagementSchema == null)
        this.m_rightsManagementSchema = new RightsManagementSchema(this);
      return this.m_rightsManagementSchema;
    }
  }

  IPdfPrimitive IPdfWrapper.Element => (IPdfPrimitive) this.m_stream;

  public XmlDocument XmlData => this.m_xmlDocument;

  internal XmlElement Xmpmeta
  {
    get
    {
      return (this.XmlData.SelectSingleNode("/x:xmpmeta", this.NamespaceManager) ?? throw new ArgumentNullException("node")) as XmlElement;
    }
  }
}
