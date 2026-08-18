// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Parsing.PdfLoadedDocument
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Graphics.Fonts;
using Syncfusion.Pdf.Interactive;
using Syncfusion.Pdf.IO;
using Syncfusion.Pdf.Primitives;
using Syncfusion.Pdf.Security;
using Syncfusion.Pdf.Xmp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;

#nullable disable
namespace Syncfusion.Pdf.Parsing;

public class PdfLoadedDocument : PdfDocumentBase, IDisposable
{
  private bool isLinearized;
  private bool isPageLabel;
  private bool isPortfolio;
  private PdfAttachmentCollection m_attachments;
  private bool m_bCloseStream;
  private PdfBookmarkBase m_bookmark;
  private Dictionary<PdfPageBase, object> m_bookmarkHashtable;
  private bool m_bWasEncrypted;
  private PdfColorSpace m_colorSpace;
  private PdfConformanceLevel m_conformance;
  private PdfDocumentInformation m_documentInfo;
  private DublinCoreSchema m_dublinschema;
  private string m_fileName;
  private PdfLoadedForm m_form;
  private MemoryStream m_internalStream;
  private bool m_isDisposed;
  private bool m_isPdfViewerDocumentDisable;
  private bool m_isXFAForm;
  private static Stream m_openStream;
  private PdfPageLabel m_pageLabel;
  private PdfLoadedPageLabelCollection m_pageLabelCollection;
  private PdfLoadedPageCollection m_pages;
  private string m_password;
  private PdfPortfolioInformation m_portfolio;
  private Stream m_stream;
  private List<PdfUsedFont> m_usedFonts;
  private string password;

  public PdfLoadedDocument(Stream file)
  {
    this.m_isPdfViewerDocumentDisable = true;
    this.m_internalStream = new MemoryStream();
    Stream stream = file != null ? this.CheckIfValid(file) : throw new ArgumentNullException(nameof (file));
    byte[] buffer = new byte[stream.Length];
    MemoryStream file1 = new MemoryStream();
    if (stream.Position != 0L)
      stream.Position = 0L;
    int count;
    while ((count = stream.Read(buffer, 0, buffer.Length)) > 0)
      file1.Write(buffer, 0, count);
    this.LoadDocument((Stream) file1);
  }

  public PdfLoadedDocument(byte[] file)
    : this(PdfLoadedDocument.CreateStream(file))
  {
    this.m_bCloseStream = true;
  }

  public PdfLoadedDocument(string filename)
    : this(PdfLoadedDocument.CreateStream(filename))
  {
    this.m_bCloseStream = true;
    this.m_fileName = filename;
  }

  public PdfLoadedDocument(byte[] file, string password)
    : this(PdfLoadedDocument.CreateStream(file), password)
  {
    this.Password = password;
    this.m_bCloseStream = true;
  }

  public PdfLoadedDocument(Stream file, string password)
  {
    this.m_isPdfViewerDocumentDisable = true;
    this.m_internalStream = new MemoryStream();
    if (file == null)
      throw new ArgumentNullException(nameof (file));
    this.m_password = password != null ? password : throw new ArgumentNullException(nameof (password));
    this.LoadDocument(file);
  }

  public PdfLoadedDocument(string filename, string password)
    : this(PdfLoadedDocument.CreateStream(filename), password)
  {
    this.m_bCloseStream = true;
    this.Password = password;
    this.m_fileName = filename;
  }

  internal override void AddFields(
    PdfLoadedDocument ldDoc,
    PdfPageBase newPage,
    List<PdfField> fields)
  {
    if (fields.Count > 0 && this.Form == null)
      this.CreateForm();
    int index = 0;
    for (int count = fields.Count; index < count; ++index)
      this.Form.Fields.Add(fields[index], newPage);
  }

  private void AppendDocument(PdfWriter writer)
  {
    writer.Document = (PdfDocumentBase) this;
    if (this.isPageLabel)
      this.PageLabel();
    this.CrossTable.Save(writer);
    this.OnDocumentSaved(new DocumentSavedEventArgs(writer));
  }

  private bool CheckEncryption()
  {
    bool flag1 = false;
    PdfDictionary trailer = this.CrossTable.Trailer;
    PdfDictionary encryptorDictionary = this.CrossTable.EncryptorDictionary;
    bool flag2 = true;
    if (encryptorDictionary != null & flag2)
    {
      if (this.m_password == null)
        this.m_password = string.Empty;
      PdfString key = ((trailer["ID"] ?? throw new PdfDocumentException("Unable to decrypt document without ID.")) as PdfArray)[0] as PdfString;
      flag1 = true;
      PdfEncryptor pdfEncryptor = new PdfEncryptor();
      if (encryptorDictionary != null && encryptorDictionary.ContainsKey("EncryptMetadata"))
        pdfEncryptor.EncryptMetaData = (encryptorDictionary["EncryptMetadata"] as PdfBoolean).Value;
      pdfEncryptor.ReadFromDictionary(encryptorDictionary);
      if (!pdfEncryptor.CheckPassword(this.m_password, key))
      {
        this.Close(true);
        throw new PdfDocumentException("Can't open an encrypted document. The password is invalid.");
      }
      encryptorDictionary.Encrypt = false;
      this.SetSecurity(new PdfSecurity()
      {
        Encryptor = pdfEncryptor
      });
      this.CrossTable.Encryptor = pdfEncryptor;
    }
    return flag1;
  }

  private void CheckIfTagged()
  {
    if (!(this.CrossTable.DocumentCatalog["MarkInfo"] is PdfDictionary pdfDictionary) || !pdfDictionary.ContainsKey("Marked"))
      return;
    this.FileStructure.TaggedPdf = (pdfDictionary["Marked"] as PdfBoolean).Value;
  }

  private Stream CheckIfValid(Stream file)
  {
    file.Position = file.Length - 1L;
    if (file.ReadByte() == 0)
    {
      byte[] numArray1 = new byte[file.Length];
      file.Position = 0L;
      file.Read(numArray1, 0, numArray1.Length);
      int index = numArray1.Length - 1;
      while (numArray1[index] == (byte) 0)
        --index;
      byte[] numArray2 = new byte[index + 1];
      Array.Copy((Array) numArray1, (Array) numArray2, index + 1);
      MemoryStream memoryStream = new MemoryStream();
      memoryStream.Write(numArray2, 0, numArray2.Length);
      file.Dispose();
      return (Stream) memoryStream;
    }
    file.Position = 0L;
    return file;
  }

  private bool CheckLinearization()
  {
    bool flag = false;
    long num = 0;
    PdfReader pdfReader = new PdfReader(this.m_stream);
    try
    {
      num = pdfReader.SearchForward("Linearized");
    }
    catch (Exception ex)
    {
      if (ex.Message.Equals("Invalid/Unknown/Unsupported format"))
        return flag;
    }
    if (num != 0L)
    {
      do
        ;
      while (!pdfReader.GetNextToken().Equals("L"));
      if (pdfReader.GetNextToken().Equals(this.m_stream.Length.ToString()))
        flag = true;
    }
    return flag;
  }

  private void CheckNeedAppearence(PdfDictionary dictionary)
  {
    if (!dictionary.ContainsKey("AcroForm"))
      return;
    if ((object) (dictionary["AcroForm"] as PdfReferenceHolder) != null)
    {
      if ((dictionary["AcroForm"] as PdfReferenceHolder).Object is PdfDictionary pdfDictionary1 && pdfDictionary1.ContainsKey("XFA"))
      {
        this.IsXFAForm = true;
      }
      else
      {
        if (pdfDictionary1 == null || !pdfDictionary1.ContainsKey("NeedAppearances"))
          return;
        this.IsXFAForm = true;
      }
    }
    else
    {
      if (!(dictionary["AcroForm"] is PdfDictionary))
        return;
      PdfDictionary pdfDictionary2 = dictionary["AcroForm"] as PdfDictionary;
      if (pdfDictionary2.ContainsKey("XFA"))
      {
        this.IsXFAForm = true;
      }
      else
      {
        if (!pdfDictionary2.ContainsKey("NeedAppearances"))
          return;
        this.IsXFAForm = true;
      }
    }
  }

  public object Clone() => this.MemberwiseClone();

  internal override PdfPageBase ClonePage(
    PdfLoadedDocument ldDoc,
    PdfPageBase page,
    List<PdfArray> destinations)
  {
    return this.Pages.Add(ldDoc, page, destinations);
  }

  public override void Close(bool completely)
  {
    if (completely && this.m_pages != null && this.EnableMemoryOptimization)
      this.m_pages.Clear();
    base.Close(completely);
    if (this.EnableMemoryOptimization)
      this.Dispose(completely);
    else
      this.Dispose();
  }

  private void CopyOldStream(PdfWriter writer)
  {
    long length = this.m_stream.Length;
    byte[] numArray = new byte[length];
    this.m_stream.Position = 0L;
    this.m_stream.Read(numArray, 0, (int) length);
    writer.Write(numArray);
  }

  public PdfAttachmentCollection CreateAttachment()
  {
    this.m_attachments = new PdfAttachmentCollection();
    this.Catalog.CreateNamesIfNone();
    this.Catalog.Names.EmbeddedFiles = this.m_attachments;
    return this.m_attachments;
  }

  internal Dictionary<PdfPageBase, object> CreateBookmarkDestinationDictionary()
  {
    PdfBookmarkBase bookmarks = this.Bookmarks;
    if (this.m_bookmarkHashtable == null && bookmarks != null)
    {
      this.m_bookmarkHashtable = new Dictionary<PdfPageBase, object>();
      Stack<PdfLoadedDocument.CurrentNodeInfo> currentNodeInfoStack = new Stack<PdfLoadedDocument.CurrentNodeInfo>();
      PdfLoadedDocument.CurrentNodeInfo currentNodeInfo = new PdfLoadedDocument.CurrentNodeInfo(bookmarks.List);
      do
      {
        while (currentNodeInfo.Index < currentNodeInfo.Kids.Count)
        {
          PdfBookmarkBase kid = currentNodeInfo.Kids[currentNodeInfo.Index];
          PdfDestination destination = (kid as PdfBookmark).Destination;
          if (destination != null)
          {
            PdfPageBase page = destination.Page;
            List<object> objectList = this.m_bookmarkHashtable.ContainsKey(page) ? this.m_bookmarkHashtable[page] as List<object> : (List<object>) null;
            if (objectList == null)
            {
              objectList = new List<object>();
              this.m_bookmarkHashtable[page] = (object) objectList;
            }
            objectList.Add((object) kid);
          }
          ++currentNodeInfo.Index;
          if (kid.Count > 0)
          {
            currentNodeInfoStack.Push(currentNodeInfo);
            currentNodeInfo = new PdfLoadedDocument.CurrentNodeInfo(kid.List);
          }
        }
        if (currentNodeInfoStack.Count > 0)
        {
          currentNodeInfo = currentNodeInfoStack.Pop();
          while (currentNodeInfo.Index == currentNodeInfo.Kids.Count && currentNodeInfoStack.Count > 0)
            currentNodeInfo = currentNodeInfoStack.Pop();
        }
      }
      while (currentNodeInfo.Index < currentNodeInfo.Kids.Count);
    }
    return this.m_bookmarkHashtable;
  }

  public PdfBookmarkBase CreateBookmarkRoot()
  {
    this.m_bookmark = new PdfBookmarkBase();
    this.Catalog.SetProperty("Outlines", (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) this.m_bookmark));
    return this.m_bookmark;
  }

  public void CreateForm()
  {
    if (this.m_form != null)
      return;
    this.m_form = new PdfLoadedForm(this.CrossTable);
    this.Catalog.SetProperty("AcroForm", (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) this.m_form));
    this.Catalog.LoadedForm = this.m_form;
  }

  private static Stream CreateStream(byte[] file)
  {
    return file != null ? (Stream) new MemoryStream(file) : throw new ArgumentNullException(nameof (file));
  }

  private static Stream CreateStream(string filename)
  {
    if (filename == null)
      throw new ArgumentNullException(nameof (filename));
    FileInfo fileInfo = File.Exists(filename) ? new FileInfo(filename) : throw new ArgumentException("File doesn't exist", nameof (filename));
    byte[] buffer = new byte[0];
    if ((fileInfo.Attributes & FileAttributes.ReadOnly) != (FileAttributes) 0)
    {
      Stream stream;
      using (stream = (Stream) fileInfo.OpenRead())
      {
        buffer = new byte[stream.Length];
        stream.Read(buffer, 0, buffer.Length);
      }
    }
    else
    {
      try
      {
        Stream stream;
        using (stream = (Stream) fileInfo.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None))
        {
          buffer = new byte[stream.Length];
          stream.Read(buffer, 0, buffer.Length);
        }
      }
      catch (IOException ex)
      {
        Stream stream;
        using (stream = (Stream) fileInfo.OpenRead())
        {
          buffer = new byte[stream.Length];
          stream.Read(buffer, 0, buffer.Length);
        }
      }
      catch (SystemException ex)
      {
        Stream stream;
        using (stream = (Stream) fileInfo.OpenRead())
        {
          buffer = new byte[stream.Length];
          stream.Read(buffer, 0, buffer.Length);
        }
      }
    }
    return (Stream) new MemoryStream(buffer);
  }

  public void Dispose()
  {
    if (this.EnableMemoryOptimization)
      this.Close(true);
    else
      this.Dispose(true);
    GC.SuppressFinalize((object) this);
  }

  private void Dispose(bool dispose)
  {
    if (this.m_isDisposed)
      return;
    this.m_isDisposed = true;
    if (dispose && this.EnableMemoryOptimization)
    {
      if (this.m_bookmark != null)
        this.m_bookmark.Clear();
      if (this.m_bookmarkHashtable != null)
        this.m_bookmarkHashtable.Clear();
      this.m_documentInfo = (PdfDocumentInformation) null;
      this.m_form = (PdfLoadedForm) null;
      this.m_internalStream = (MemoryStream) null;
      PdfLoadedDocument.m_openStream = (Stream) null;
      this.m_pageLabel = (PdfPageLabel) null;
      this.m_pageLabelCollection = (PdfLoadedPageLabelCollection) null;
      if (this.m_stream != null)
        this.m_stream.Close();
      this.m_dublinschema = (DublinCoreSchema) null;
      if (this.m_usedFonts != null)
        this.m_usedFonts.Clear();
    }
    this.m_stream = (Stream) null;
    this.m_form = (PdfLoadedForm) null;
    this.m_pages = (PdfLoadedPageCollection) null;
    this.m_bookmark = (PdfBookmarkBase) null;
  }

  private static PdfArray ExtractDestination(IPdfPrimitive obj)
  {
    PdfDictionary pdfDictionary = obj as PdfDictionary;
    PdfArray destination = obj as PdfArray;
    if (pdfDictionary != null)
    {
      obj = PdfCrossTable.Dereference(pdfDictionary["D"]);
      destination = obj as PdfArray;
    }
    return destination;
  }

  private List<PdfUsedFont> ExtractFonts()
  {
    List<PdfUsedFont> fonts = new List<PdfUsedFont>();
    PdfLoadedPageCollection pages = this.Pages;
    ArrayList arrayList = new ArrayList();
    foreach (PdfLoadedPage page in pages)
    {
      foreach (PdfFont font in page.ExtractFonts())
        fonts.Add(new PdfUsedFont(font, page));
    }
    PdfFont[] pdfFontArray = new PdfFont[arrayList.Count];
    int num = 0;
    foreach (PdfFont pdfFont in arrayList)
      pdfFontArray[num++] = pdfFont;
    return fonts;
  }

  ~PdfLoadedDocument() => this.Dispose(false);

  private PdfDictionary GetAttachmentDictionary()
  {
    return PdfCrossTable.Dereference(this.Catalog["Names"]) as PdfDictionary;
  }

  private PdfCatalog GetCatalog()
  {
    PdfCatalog newObj = new PdfCatalog(this, this.CrossTable.DocumentCatalog);
    this.PdfObjects.ReregisterReference((IPdfPrimitive) this.CrossTable.DocumentCatalog, (IPdfPrimitive) newObj);
    if (!this.CrossTable.IsMerging)
      newObj.Position = -1;
    PdfDictionary dictionary = (PdfDictionary) newObj;
    if (dictionary != null)
      this.CheckNeedAppearence(dictionary);
    return newObj;
  }

  internal override PdfForm GetForm()
  {
    if (this.Form == null)
      this.CreateForm();
    return (PdfForm) this.Form;
  }

  private PdfDictionary GetFormDictionary()
  {
    return PdfCrossTable.Dereference(this.Catalog["AcroForm"]) as PdfDictionary;
  }

  internal PdfArray GetNamedDestination(PdfName name)
  {
    return PdfLoadedDocument.ExtractDestination(this.Catalog.Destinations[name]);
  }

  internal PdfArray GetNamedDestination(PdfString name)
  {
    PdfCatalogNames names = this.Catalog.Names;
    PdfArray namedDestination = (PdfArray) null;
    if (name != null)
    {
      PdfDictionary destinations = names.Destinations;
      namedDestination = PdfLoadedDocument.ExtractDestination(names.GetNamedObjectFromTree(destinations, name));
    }
    return namedDestination;
  }

  private PdfDictionary GetPortfolioDictionary()
  {
    return PdfCrossTable.Dereference(this.Catalog["Collection"]) as PdfDictionary;
  }

  private void LoadDocument(Stream file)
  {
    if (!file.CanRead || !file.CanSeek)
      throw new ArgumentException("Can't use the specified stream.", nameof (file));
    this.m_stream = file;
    this.SetMainObjectCollection(new PdfMainObjectCollection());
    PdfCrossTable cTable = new PdfCrossTable(file);
    cTable.Document = (PdfDocumentBase) this;
    if (cTable.StructureAltered)
      cTable.Document.FileStructure.IncrementalUpdate = false;
    this.SetCrossTable(cTable);
    this.m_bWasEncrypted = this.CheckEncryption();
    this.SetCatalog(this.GetCatalog());
    this.ReadDocumentInfo();
    this.ReadFileVersion();
    this.CheckIfTagged();
  }

  internal void PageLabel()
  {
    if (!(this.Catalog["PageLabels"] is PdfDictionary pdfDictionary))
    {
      pdfDictionary = new PdfDictionary();
      this.Catalog["PageLabels"] = (IPdfPrimitive) pdfDictionary;
    }
    PdfArray pdfArray1 = new PdfArray();
    pdfDictionary["Nums"] = (IPdfPrimitive) pdfArray1;
    PdfArray pdfArray2 = (this.CrossTable.GetObject(this.Catalog["Pages"]) as PdfDictionary)["Kids"] as PdfArray;
    int num = 0;
    for (int index = 0; index < pdfArray2.Count; ++index)
    {
      PdfPageLabel pdfPageLabel = this.m_pageLabelCollection[index] ?? new PdfPageLabel();
      pdfArray1.Add((IPdfPrimitive) new PdfNumber(num));
      PdfArray pdfArray3 = (this.CrossTable.GetObject((IPdfPrimitive) (pdfArray2[index] as PdfReferenceHolder)) as PdfDictionary)["Kids"] as PdfArray;
      num += pdfArray3.Count;
      pdfArray1.Add(((IPdfWrapper) pdfPageLabel).Element);
    }
  }

  private void ReadDocumentInfo()
  {
    if (PdfCrossTable.Dereference(this.CrossTable.Trailer["Info"]) is PdfDictionary pdfDictionary && this.m_bWasEncrypted && this.Catalog.Metadata != null)
    {
      XmpMetadata metadata = this.Catalog.Metadata;
      if (pdfDictionary.ContainsKey("Producer") && metadata.PDFSchema != null && metadata.PDFSchema.Producer != string.Empty && metadata.PDFSchema.Producer != (pdfDictionary["Producer"] as PdfString).Value)
        pdfDictionary["Producer"] = (IPdfPrimitive) new PdfString(metadata.PDFSchema.Producer);
      if (pdfDictionary.ContainsKey("Author") && this.DublinSchema.Creator.Items != null && this.DublinSchema.Creator.Items[0] != string.Empty && metadata.DublinCoreSchema.Creator.Items[0] != (pdfDictionary["Author"] as PdfString).Value)
        pdfDictionary["Author"] = (IPdfPrimitive) new PdfString(this.DublinSchema.Creator.Items[0]);
      if (metadata.XmlData.InnerText.Contains("Title") && pdfDictionary.ContainsKey("Title") && this.DublinSchema != null && this.DublinSchema.Title.DefaultText != string.Empty && metadata.DublinCoreSchema.Title.DefaultText != (pdfDictionary["Title"] as PdfString).Value)
        pdfDictionary["Title"] = (IPdfPrimitive) new PdfString(this.DublinSchema.Title.DefaultText);
      if (pdfDictionary.ContainsKey("Creator") && metadata.BasicSchema != null && metadata.BasicSchema.CreatorTool != string.Empty && metadata.BasicSchema.CreatorTool != (pdfDictionary["Creator"] as PdfString).Value)
        pdfDictionary["Creator"] = (IPdfPrimitive) new PdfString(metadata.BasicSchema.CreatorTool);
      if (pdfDictionary.ContainsKey("CreationDate") && metadata.BasicSchema != null && metadata.BasicSchema.CreateDate.ToString() != string.Empty && metadata.BasicSchema.CreateDate.ToString() != (pdfDictionary["CreationDate"] as PdfString).Value)
        pdfDictionary["CreationDate"] = (IPdfPrimitive) new PdfString(metadata.BasicSchema.CreateDate.ToString("yyyyMMddHHmmss"));
      if (pdfDictionary.ContainsKey("ModDate") && metadata.BasicSchema != null && metadata.BasicSchema.ModifyDate.ToString() != string.Empty && metadata.BasicSchema.ModifyDate.ToString() != (pdfDictionary["ModDate"] as PdfString).Value)
        pdfDictionary["ModDate"] = (IPdfPrimitive) new PdfString(metadata.BasicSchema.ModifyDate.ToString("yyyyMMddHHmmss"));
    }
    if (pdfDictionary == null)
      return;
    this.m_documentInfo = new PdfDocumentInformation(pdfDictionary, this.Catalog);
    this.PdfObjects.ReregisterReference((IPdfPrimitive) pdfDictionary, ((IPdfWrapper) this.m_documentInfo).Element);
    ((IPdfWrapper) this.m_documentInfo).Element.Position = -1;
  }

  private void ReadFileVersion()
  {
    PdfReader pdfReader = new PdfReader(this.m_stream);
    pdfReader.Position = 0L;
    if (!pdfReader.GetNextToken().StartsWith("%"))
      return;
    string nextToken = pdfReader.GetNextToken();
    // ISSUE: reference to a compiler-generated method
    switch (\u003CPrivateImplementationDetails\u003E.ComputeStringHash(nextToken))
    {
      case 1708395560:
        if (!(nextToken == "PDF-1.7"))
          break;
        this.FileStructure.Version = PdfVersion.Version1_7;
        break;
      case 1725173179:
        if (!(nextToken == "PDF-1.6"))
          break;
        this.FileStructure.Version = PdfVersion.Version1_6;
        break;
      case 1741950798:
        if (!(nextToken == "PDF-1.5"))
          break;
        this.FileStructure.Version = PdfVersion.Version1_5;
        break;
      case 1758728417:
        if (!(nextToken == "PDF-1.4"))
          break;
        this.FileStructure.Version = PdfVersion.Version1_4;
        break;
      case 1775506036:
        if (!(nextToken == "PDF-1.3"))
          break;
        this.FileStructure.Version = PdfVersion.Version1_3;
        this.FileStructure.IncrementalUpdate = false;
        break;
      case 1792283655:
        if (!(nextToken == "PDF-1.2"))
          break;
        this.FileStructure.Version = PdfVersion.Version1_2;
        this.FileStructure.IncrementalUpdate = false;
        break;
      case 1809061274:
        if (!(nextToken == "PDF-1.1"))
          break;
        this.FileStructure.Version = PdfVersion.Version1_1;
        this.FileStructure.IncrementalUpdate = false;
        break;
      case 1825838893:
        if (!(nextToken == "PDF-1.0"))
          break;
        this.FileStructure.Version = PdfVersion.Version1_0;
        this.FileStructure.IncrementalUpdate = false;
        break;
    }
  }

  public void Save()
  {
    this.Save((Stream) this.m_internalStream);
    if (!this.m_stream.CanWrite)
      throw new PdfException("Unable to save to the specified file or stream, because it is being used by another process. Use Save(filename) or Save(stream) instead and specify different filename or stream.");
    if (string.IsNullOrEmpty(this.m_fileName))
      return;
    using (FileStream fileStream = new FileStream(this.m_fileName, FileMode.Create, FileAccess.Write))
      this.Save((Stream) fileStream);
  }

  public override void Save(Stream stream)
  {
    using (PdfWriter writer = new PdfWriter(stream))
    {
      if (this.Security.Enabled == this.m_bWasEncrypted && (!this.m_bWasEncrypted || !this.Security.Encryptor.Changed) && this.FileStructure.IncrementalUpdate)
      {
        this.CopyOldStream(writer);
        this.AppendDocument(writer);
      }
      else
      {
        if (this.FileStructure.Version <= PdfVersion.Version1_2)
          this.FileStructure.Version = PdfVersion.Version1_4;
        PdfCrossTable crossTable = this.CrossTable;
        this.SetCrossTable(new PdfCrossTable(crossTable.Count, crossTable.EncryptorDictionary));
        this.CrossTable.Document = (PdfDocumentBase) this;
        if (this.DocumentInformation != null)
          this.CrossTable.Trailer["Info"] = (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) this.DocumentInformation);
        this.AppendDocument(writer);
      }
    }
  }

  public void Split(string destFilePattern) => this.Split(destFilePattern, 0);

  public void Split(string destFilePattern, int startNumber)
  {
    if (destFilePattern == null)
      throw new ArgumentNullException("destFileName");
    if (!new Regex("\\w*\\{0.*\\}\\w*", RegexOptions.None).Match(destFilePattern).Success)
    {
      int num = destFilePattern.LastIndexOf('.');
      destFilePattern = num >= 0 ? $"{destFilePattern.Substring(0, num)}{"{0}"}{destFilePattern.Substring(num, destFilePattern.Length - num)}" : $"{destFilePattern}{"{0}.pdf"}";
    }
    int pageIndex = 0;
    for (int count = this.Pages.Count; pageIndex < count; ++pageIndex)
    {
      PdfDocument pdfDocument = new PdfDocument();
      pdfDocument.ImportPage(this, pageIndex);
      pdfDocument.Save(string.Format(destFilePattern, (object) (pageIndex + startNumber)));
      pdfDocument.Close();
    }
  }

  public PdfAttachmentCollection Attachments
  {
    get
    {
      if (this.m_attachments == null)
      {
        PdfDictionary attachmentDictionary = this.GetAttachmentDictionary();
        if (attachmentDictionary != null)
        {
          this.m_attachments = new PdfAttachmentCollection(attachmentDictionary, this.CrossTable);
          if (this.m_attachments != null)
            this.Catalog.Attachments = this.m_attachments;
        }
      }
      return this.m_attachments;
    }
  }

  public override PdfBookmarkBase Bookmarks
  {
    get
    {
      if (this.Catalog.ContainsKey("Outlines") && this.m_bookmark == null)
      {
        this.m_bookmark = new PdfBookmarkBase(PdfCrossTable.Dereference(this.Catalog["Outlines"]) as PdfDictionary, this.CrossTable);
        this.m_bookmark.ReproduceTree();
      }
      else if (this.m_bookmark == null)
        this.m_bookmark = this.CreateBookmarkRoot();
      return this.m_bookmark;
    }
  }

  public PdfColorSpace ColorSpace
  {
    get
    {
      return this.m_colorSpace != PdfColorSpace.RGB && this.m_colorSpace != PdfColorSpace.CMYK && this.m_colorSpace != PdfColorSpace.GrayScale ? PdfColorSpace.RGB : this.m_colorSpace;
    }
    set
    {
      if (value == PdfColorSpace.RGB || value == PdfColorSpace.CMYK || value == PdfColorSpace.GrayScale)
        this.m_colorSpace = value;
      else
        this.m_colorSpace = PdfColorSpace.RGB;
    }
  }

  public PdfConformanceLevel Conformance
  {
    get
    {
      if (this.m_conformance == PdfConformanceLevel.None)
      {
        if (this.Catalog["OutputIntents"] is PdfArray pdfArray)
        {
          for (int index = 0; index < pdfArray.Count; ++index)
          {
            if (pdfArray[index] is PdfDictionary pdfDictionary)
            {
              PdfName pdfName = pdfDictionary["S"] as PdfName;
              if (pdfName.Value == "GTS_PDFA1")
              {
                this.m_conformance = PdfConformanceLevel.Pdf_A1B;
                break;
              }
              if (pdfName.Value == "GTS_PDFX" && this.DocumentInformation.Dictionary.ContainsKey("GTS_PDFXConformance") && (this.DocumentInformation.Dictionary["GTS_PDFXConformance"] as PdfString).Value == "PDF/X-1a:2001")
              {
                this.m_conformance = PdfConformanceLevel.Pdf_X1A2001;
                break;
              }
            }
          }
        }
        if (pdfArray == null | this.m_conformance == PdfConformanceLevel.Pdf_A1B)
        {
          string name1 = "pdfaid:part";
          string name2 = "pdfaid:conformance";
          XmlElement xmpmeta = this.DocumentInformation.XmpMetadata.Xmpmeta;
          bool flag = false;
          foreach (XmlNode childNode1 in xmpmeta.ChildNodes)
          {
            foreach (XmlNode childNode2 in childNode1.ChildNodes)
            {
              XmlAttribute attribute1 = childNode2.Attributes[name1];
              XmlAttribute attribute2 = childNode2.Attributes[name2];
              if (attribute1 != null && attribute2 != null && attribute1.Value == "1" && attribute2.Value == "B")
              {
                this.m_conformance = PdfConformanceLevel.Pdf_A1B;
                flag = true;
                break;
              }
              if (childNode2.InnerXml.Contains("pdfaid") && childNode2[name1].InnerText == "1" && childNode2[name2].InnerText == "B")
              {
                this.m_conformance = PdfConformanceLevel.Pdf_A1B;
                flag = true;
                break;
              }
            }
            if (flag)
              break;
          }
          if (!flag)
            this.m_conformance = PdfConformanceLevel.None;
        }
      }
      return this.m_conformance;
    }
  }

  public override PdfDocumentInformation DocumentInformation
  {
    get
    {
      if (this.m_documentInfo == null)
        this.m_documentInfo = !(PdfCrossTable.Dereference(this.CrossTable.Trailer["Info"]) is PdfDictionary dictionary) ? base.DocumentInformation : new PdfDocumentInformation(dictionary, this.Catalog);
      return this.m_documentInfo;
    }
  }

  internal DublinCoreSchema DublinSchema
  {
    get => this.m_dublinschema;
    set => this.m_dublinschema = value;
  }

  public PdfLoadedForm Form
  {
    get
    {
      if (this.m_form == null)
      {
        PdfDictionary formDictionary = this.GetFormDictionary();
        if (formDictionary != null)
        {
          this.m_form = new PdfLoadedForm(formDictionary, this.CrossTable);
          if (this.m_form != null)
          {
            this.Catalog.LoadedForm = this.m_form;
            if (!PdfLoadedPage.m_annotChanged)
            {
              for (int index = 0; index < (this.m_form.CrossTable.Document as PdfLoadedDocument).Pages.Count; ++index)
              {
                if ((this.m_form.CrossTable.Document as PdfLoadedDocument).Pages[index] is PdfLoadedPage)
                  ((this.m_form.CrossTable.Document as PdfLoadedDocument).Pages[index] as PdfLoadedPage).CreateAnnotations();
              }
            }
          }
        }
      }
      return this.m_form;
    }
  }

  public bool IsEncrypted => this.m_bWasEncrypted;

  public bool IsLinearized
  {
    get
    {
      this.isLinearized = this.CheckLinearization();
      return this.isLinearized;
    }
  }

  internal override bool IsPdfViewerDocumentDisable
  {
    get => this.m_isPdfViewerDocumentDisable;
    set => this.m_isPdfViewerDocumentDisable = value;
  }

  public bool IsPortfolio
  {
    get
    {
      if (this.GetPortfolioDictionary() != null)
      {
        this.isPortfolio = true;
        return this.isPortfolio;
      }
      this.isPortfolio = false;
      return this.isPortfolio;
    }
  }

  internal bool IsXFAForm
  {
    get => this.m_isXFAForm;
    set => this.m_isXFAForm = value;
  }

  public PdfPageLabel LoadedPageLabel
  {
    get => this.m_pageLabel;
    set
    {
      if (this.m_pageLabelCollection == null)
        this.m_pageLabelCollection = new PdfLoadedPageLabelCollection();
      this.isPageLabel = true;
      this.m_pageLabelCollection.Add(value);
    }
  }

  internal override int PageCount => this.Pages.Count;

  public PdfLoadedPageCollection Pages
  {
    get
    {
      if (this.m_pages == null)
        this.m_pages = new PdfLoadedPageCollection((PdfDocumentBase) this, this.CrossTable);
      return this.m_pages;
    }
  }

  internal string Password
  {
    get => this.password;
    set => this.password = value;
  }

  public new PdfPortfolioInformation PortfolioInformation
  {
    get
    {
      if (this.m_portfolio == null)
      {
        PdfDictionary portfolioDictionary = this.GetPortfolioDictionary();
        if (portfolioDictionary != null)
          this.m_portfolio = new PdfPortfolioInformation(portfolioDictionary);
      }
      return this.m_portfolio;
    }
    set => this.Catalog.PdfPortfolio = value;
  }

  public PdfUsedFont[] UsedFonts
  {
    get
    {
      if (this.m_usedFonts == null)
        this.m_usedFonts = this.ExtractFonts();
      return this.m_usedFonts.ToArray();
    }
  }

  internal override bool WasEncrypted => this.m_bWasEncrypted;

  private class CurrentNodeInfo
  {
    public int Index;
    public List<PdfBookmarkBase> Kids;

    public CurrentNodeInfo(List<PdfBookmarkBase> kids)
    {
      this.Kids = kids;
      this.Index = 0;
    }

    public CurrentNodeInfo(List<PdfBookmarkBase> kids, int index)
      : this(kids)
    {
      this.Index = index;
    }
  }
}
