// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.PdfDocumentBase
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Interactive;
using Syncfusion.Pdf.IO;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf.Primitives;
using Syncfusion.Pdf.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Security.Permissions;

#nullable disable
namespace Syncfusion.Pdf;

public abstract class PdfDocumentBase
{
  private PdfCatalog m_catalog;
  private PdfCompressionLevel m_compression = PdfCompressionLevel.Normal;
  private PdfCrossTable m_crossTable;
  private PdfReference m_currentSavingObj;
  private List<IDisposable> m_disposeObjects;
  private PdfDocumentInformation m_documentInfo;
  private bool m_enableMemoryOptimization;
  private PdfFileStructure m_fileStructure;
  private PdfMainObjectCollection m_objects;
  internal PdfArray m_off = new PdfArray();
  internal int m_offpositon;
  internal PdfArray m_on = new PdfArray();
  internal int m_onpositon;
  internal PdfArray m_order = new PdfArray();
  internal int m_orderposition;
  private string m_password;
  private PdfPortfolioInformation m_portfolio;
  internal int m_positon;
  internal PdfArray m_printLayer = new PdfArray();
  private PdfSecurity m_security;
  internal PdfArray m_sublayer = new PdfArray();
  internal int m_sublayerposition;
  internal PdfArray primitive = new PdfArray();

  internal event PdfDocumentBase.DocumentSavedEventHandler DocumentSaved;

  internal abstract void AddFields(
    PdfLoadedDocument ldDoc,
    PdfPageBase newPage,
    List<PdfField> fields);

  public void Append(PdfLoadedDocument ldDoc)
  {
    if (this is PdfDocument)
      this.CrossTable.IsMerging = true;
    if (ldDoc == null)
      throw new ArgumentNullException(nameof (ldDoc));
    if (ldDoc.IsXFAForm && this is PdfDocument)
      ((PdfDocument) this).Form.IsXFA = true;
    if (!ldDoc.IsXFAForm && ldDoc.Form != null)
    {
      int num = ldDoc.Form.IsXFAForm ? 1 : 0;
    }
    int startIndex = 0;
    int endIndex = ldDoc.Pages.Count - 1;
    if (!this.EnableMemoryOptimization)
      this.EnableMemoryOptimization = true;
    this.ImportPageRange(ldDoc, startIndex, endIndex);
    this.MergeAttachments(ldDoc);
  }

  private bool CheckEncryption(PdfLoadedDocument ldoc)
  {
    bool flag1 = false;
    PdfDictionary trailer = ldoc.CrossTable.Trailer;
    PdfDictionary encryptorDictionary = ldoc.CrossTable.EncryptorDictionary;
    this.m_password = ldoc.Password;
    bool flag2 = true;
    if (encryptorDictionary != null && encryptorDictionary.ContainsKey("EncryptMetadata"))
      flag2 = (encryptorDictionary["EncryptMetadata"] as PdfBoolean).Value;
    if (encryptorDictionary != null & flag2)
    {
      if (this.m_password == null)
        this.m_password = string.Empty;
      PdfString key = ((trailer["ID"] ?? throw new PdfDocumentException("Unable to decrypt document without ID.")) as PdfArray)[0] as PdfString;
      PdfEncryptor pdfEncryptor = new PdfEncryptor();
      pdfEncryptor.ReadFromDictionary(encryptorDictionary);
      if (!pdfEncryptor.CheckPassword(this.m_password, key))
      {
        this.Close(true);
        throw new PdfDocumentException("Can't open an encrypted document. The password is invalid.");
      }
      encryptorDictionary.Encrypt = false;
      PdfSecurity security = new PdfSecurity();
      if (!this.Security.Encryptor.Encrypt)
      {
        security.Encryptor = pdfEncryptor;
        this.SetSecurity(security);
        flag1 = true;
        this.Security.Encryptor = pdfEncryptor;
      }
    }
    return flag1;
  }

  protected virtual void CheckFields(
    PdfLoadedDocument ldDoc,
    PdfPageBase page,
    List<PdfField> fields)
  {
    PdfArray annots = page.GetAnnots();
    PdfLoadedForm form = ldDoc.Form;
    PdfName key = new PdfName("Kids");
    PdfCollection pdfCollection = (PdfCollection) null;
    if (annots == null || form == null)
      return;
    int index = 0;
    for (int count = form.Fields.Count; index < count; ++index)
    {
      PdfField field = form.Fields[index];
      if (this.EnableMemoryOptimization)
      {
        bool flag = false;
        if (field is PdfLoadedSignatureField)
          flag = true;
        if (field.Dictionary.ContainsKey(key) && (field.Dictionary[key] as PdfArray).Count > 0 && !flag)
        {
          switch (field)
          {
            case PdfLoadedButtonField _ when (field as PdfLoadedButtonField).Items.Count > 0:
              pdfCollection = (PdfCollection) (field as PdfLoadedButtonField).Items;
              break;
            case PdfLoadedCheckBoxField _ when (field as PdfLoadedCheckBoxField).Items.Count > 0:
              pdfCollection = (PdfCollection) (field as PdfLoadedCheckBoxField).Items;
              break;
            case PdfLoadedComboBoxField _ when (field as PdfLoadedComboBoxField).Items.Count > 0:
              pdfCollection = (PdfCollection) (field as PdfLoadedComboBoxField).Items;
              break;
            case PdfLoadedListBoxField _ when (field as PdfLoadedListBoxField).Items.Count > 0:
              pdfCollection = (PdfCollection) (field as PdfLoadedListBoxField).Items;
              break;
            case PdfLoadedRadioButtonListField _ when (field as PdfLoadedRadioButtonListField).Items.Count > 0:
              pdfCollection = (PdfCollection) (field as PdfLoadedRadioButtonListField).Items;
              break;
            case PdfLoadedTextBoxField _ when (field as PdfLoadedTextBoxField).Items.Count > 0:
              pdfCollection = (PdfCollection) (field as PdfLoadedTextBoxField).Items;
              break;
          }
          foreach (PdfLoadedFieldItem pdfLoadedFieldItem in pdfCollection)
          {
            if (pdfLoadedFieldItem.Page == page)
            {
              fields.Add(field);
              break;
            }
          }
        }
        else if (field.Page == page)
          fields.Add(field);
      }
      else if (field.Page == page)
        fields.Add(field);
    }
  }

  internal abstract PdfPageBase ClonePage(
    PdfLoadedDocument ldDoc,
    PdfPageBase page,
    List<PdfArray> destinations);

  public void Close() => this.Close(false);

  public virtual void Close(bool completely)
  {
    this.m_security = (PdfSecurity) null;
    this.m_objects = (PdfMainObjectCollection) null;
    this.m_currentSavingObj = (PdfReference) null;
    if (this.m_catalog != null & completely && this.EnableMemoryOptimization)
    {
      this.m_catalog.Clear();
      this.m_catalog = (PdfCatalog) null;
    }
    if (this.EnableMemoryOptimization)
    {
      if (this.m_crossTable != null)
        this.m_crossTable.Close(true);
    }
    else if (completely && this.m_crossTable != null)
      this.m_crossTable.Dispose();
    this.m_crossTable = (PdfCrossTable) null;
    this.m_documentInfo = (PdfDocumentInformation) null;
    this.m_compression = PdfCompressionLevel.Normal;
    if (this.m_disposeObjects != null)
    {
      int index = 0;
      for (int count = this.m_disposeObjects.Count; index < count; ++index)
        this.m_disposeObjects[index]?.Dispose();
      this.m_disposeObjects.Clear();
      this.m_disposeObjects = (List<IDisposable>) null;
    }
    PdfDocument.Cache.Clear();
  }

  public void DisposeOnClose(IDisposable obj)
  {
    if (obj == null)
      return;
    this.DisposeObjects.Add(obj);
  }

  private void ExportBookmarks(
    PdfLoadedDocument ldDoc,
    List<PdfBookmarkBase> bookmarks,
    int pageCount,
    Dictionary<PdfPageBase, object> bookmarkshash)
  {
    PdfBookmarkBase bookmarkBase = this.Bookmarks;
    PdfBookmarkBase bookmarks1 = ldDoc.Bookmarks;
    List<string> stringList = (List<string>) null;
    if (bookmarks1 == null)
      return;
    if (bookmarkBase == null)
      bookmarkBase = (this as PdfLoadedDocument).CreateBookmarkRoot();
    Stack<PdfDocumentBase.NodeInfo> nodeInfoStack = new Stack<PdfDocumentBase.NodeInfo>();
    PdfDocumentBase.NodeInfo nodeInfo = new PdfDocumentBase.NodeInfo(bookmarkBase, bookmarks1.List);
    if (ldDoc.Pages.Count != pageCount)
    {
      nodeInfo = new PdfDocumentBase.NodeInfo(bookmarkBase, bookmarks);
      stringList = new List<string>();
    }
    do
    {
      while (nodeInfo.Index < nodeInfo.Kids.Count)
      {
        PdfBookmarkBase kid = nodeInfo.Kids[nodeInfo.Index];
        if (bookmarks.Contains(kid) && stringList != null && !stringList.Contains((kid as PdfBookmark).Title))
        {
          PdfBookmark pdfBookmark1 = kid as PdfBookmark;
          PdfBookmark pdfBookmark2 = bookmarkBase.Add(pdfBookmark1.Title);
          pdfBookmark2.TextStyle = pdfBookmark1.TextStyle;
          pdfBookmark2.Color = pdfBookmark1.Color;
          PdfDestination destination = pdfBookmark1.Destination;
          if (destination != null && this.EnableMemoryOptimization)
          {
            PdfPageBase page1 = destination.Page;
            if (ldDoc.CrossTable.PageCorrespondance.ContainsKey((IPdfPrimitive) page1.Dictionary) && ldDoc.CrossTable.PageCorrespondance[(IPdfPrimitive) page1.Dictionary] != null)
            {
              if (ldDoc.CrossTable.PageCorrespondance[(IPdfPrimitive) page1.Dictionary] is PdfPageBase page2)
              {
                PdfDestination pdfDestination = new PdfDestination(page2, destination.Location);
                pdfBookmark2.Destination = pdfDestination;
              }
            }
            else
              pdfBookmark2.Dictionary.Remove("A");
          }
          else
          {
            PdfPageBase page = destination.Page;
            PdfDestination pdfDestination = new PdfDestination(ldDoc.CrossTable.PageCorrespondance[((IPdfWrapper) page).Element] as PdfPageBase, destination.Location);
            pdfBookmark2.Destination = pdfDestination;
          }
          bookmarkBase = (PdfBookmarkBase) pdfBookmark2;
          stringList.Add(pdfBookmark2.Title);
        }
        else
        {
          PdfBookmark pdfBookmark3 = kid as PdfBookmark;
          PdfDestination destination = pdfBookmark3.Destination;
          PdfPageBase pdfPageBase = (PdfPageBase) null;
          if (ldDoc.Pages.Count == pageCount)
          {
            PdfBookmark pdfBookmark4 = bookmarkBase.Add(pdfBookmark3.Title);
            if (!this.EnableMemoryOptimization && pdfBookmark3.Dictionary.ContainsKey("A"))
              pdfBookmark4.Dictionary.SetProperty("A", pdfBookmark3.Dictionary["A"]);
            pdfBookmark4.TextStyle = pdfBookmark3.TextStyle;
            pdfBookmark4.Color = pdfBookmark3.Color;
            if (destination != null)
            {
              PdfPageBase page3 = destination.Page;
              if (ldDoc.CrossTable.PageCorrespondance.ContainsKey((IPdfPrimitive) page3.Dictionary) && ldDoc.CrossTable.PageCorrespondance[(IPdfPrimitive) page3.Dictionary] != null)
              {
                if (ldDoc.CrossTable.PageCorrespondance[(IPdfPrimitive) page3.Dictionary] is PdfPageBase page4)
                {
                  PdfDestination pdfDestination = new PdfDestination(page4, destination.Location);
                  pdfBookmark4.Destination = pdfDestination;
                }
              }
              else
                pdfBookmark4.Dictionary.Remove("A");
            }
            bookmarkBase = (PdfBookmarkBase) pdfBookmark4;
          }
          else if (destination != null && destination.Page != null && ldDoc.Pages.IndexOf(destination.Page) < pageCount && ldDoc.CrossTable.PageCorrespondance.ContainsKey((IPdfPrimitive) destination.Page.Dictionary) && ldDoc.CrossTable.PageCorrespondance[(IPdfPrimitive) destination.Page.Dictionary] != null)
          {
            pdfPageBase = destination.Page;
            PdfPageBase page = ldDoc.CrossTable.PageCorrespondance[(IPdfPrimitive) destination.Page.Dictionary] as PdfPageBase;
            PdfBookmark pdfBookmark5 = bookmarkBase.Add(pdfBookmark3.Title);
            if (pdfBookmark3.Dictionary.ContainsKey("A"))
            {
              if (this.EnableMemoryOptimization)
              {
                IPdfPrimitive primitive = pdfBookmark3.Dictionary["A"].Clone(this.m_crossTable);
                pdfBookmark5.Dictionary.SetProperty("A", primitive);
              }
              else
                pdfBookmark5.Dictionary.SetProperty("A", pdfBookmark3.Dictionary["A"]);
            }
            if (page != null)
            {
              pdfBookmark5.TextStyle = pdfBookmark3.TextStyle;
              pdfBookmark5.Color = pdfBookmark3.Color;
              PdfDestination pdfDestination = new PdfDestination(page, destination.Location);
              pdfBookmark5.Destination = pdfDestination;
              bookmarkBase = (PdfBookmarkBase) pdfBookmark5;
            }
          }
        }
        ++nodeInfo.Index;
        if (kid.Count > 0)
        {
          nodeInfoStack.Push(nodeInfo);
          nodeInfo = new PdfDocumentBase.NodeInfo(bookmarkBase, kid.List);
        }
        else
          bookmarkBase = nodeInfo.Base;
      }
      if (nodeInfoStack.Count > 0)
      {
        nodeInfo = nodeInfoStack.Pop();
        while (nodeInfo.Index == nodeInfo.Kids.Count && nodeInfoStack.Count > 0)
          nodeInfo = nodeInfoStack.Pop();
        bookmarkBase = nodeInfo.Base;
      }
    }
    while (nodeInfo.Index < nodeInfo.Kids.Count);
    stringList?.Clear();
  }

  private void FixDestinations(
    Dictionary<IPdfPrimitive, object> pageCorrespondance,
    List<PdfArray> destinations)
  {
    PdfNull element1 = new PdfNull();
    int index = 0;
    for (int count = destinations.Count; index < count; ++index)
    {
      PdfArray destination = destinations[index];
      if (destination != null)
      {
        PdfReferenceHolder pdfReferenceHolder = destination[0] as PdfReferenceHolder;
        if (pdfReferenceHolder != (PdfReferenceHolder) null)
        {
          if (pdfReferenceHolder.Object is PdfDictionary key && pageCorrespondance.ContainsKey((IPdfPrimitive) key) && pageCorrespondance[(IPdfPrimitive) key] != null)
          {
            PdfPageBase wrapper = pageCorrespondance[(IPdfPrimitive) key] as PdfPageBase;
            destination.RemoveAt(0);
            if (wrapper != null)
            {
              PdfReferenceHolder element2 = new PdfReferenceHolder((IPdfWrapper) wrapper);
              destination.Insert(0, (IPdfPrimitive) element2);
            }
            else
              destination.Insert(0, (IPdfPrimitive) element1);
          }
          else if (pageCorrespondance.ContainsKey((IPdfPrimitive) key) && pageCorrespondance[(IPdfPrimitive) key] == null)
          {
            destination.RemoveAt(0);
            destination.Insert(0, (IPdfPrimitive) element1);
          }
        }
      }
    }
  }

  internal abstract PdfForm GetForm();

  public PdfPageBase ImportPage(PdfLoadedDocument ldDoc, PdfPageBase page)
  {
    if (ldDoc == null)
      throw new ArgumentNullException(nameof (ldDoc));
    if (page == null)
      throw new ArgumentNullException(nameof (page));
    int pageIndex = ldDoc.Pages.IndexOf(page);
    return this.ImportPage(ldDoc, pageIndex);
  }

  public PdfPageBase ImportPage(PdfLoadedDocument ldDoc, int pageIndex)
  {
    if (ldDoc == null)
      throw new ArgumentNullException(nameof (ldDoc));
    if (pageIndex < 0 || pageIndex >= ldDoc.Pages.Count)
      throw new ArgumentOutOfRangeException(nameof (pageIndex));
    return this.ImportPageRange(ldDoc, pageIndex, pageIndex);
  }

  public PdfPageBase ImportPageRange(PdfLoadedDocument ldDoc, int startIndex, int endIndex)
  {
    if (ldDoc == null)
      throw new ArgumentNullException(nameof (ldDoc));
    if (startIndex > endIndex)
      throw new ArgumentException("The start index is greater then the end index, which might indicate the error in the program.");
    PdfPageBase pdfPageBase1 = (PdfPageBase) null;
    PdfLoadedPageCollection pages = ldDoc.Pages;
    if (this is PdfLoadedDocument)
    {
      PdfLoadedDocument pdfLoadedDocument = this as PdfLoadedDocument;
      foreach (PdfPageBase page in pages)
      {
        if (pdfLoadedDocument.Pages.IndexOf(page) >= 0)
          return (PdfPageBase) null;
      }
    }
    if (ldDoc.CrossTable.DocumentCatalog.ContainsKey("Pages"))
    {
      PdfDictionary pdfDictionary = (ldDoc.CrossTable.DocumentCatalog["Pages"] as PdfReferenceHolder).Object as PdfDictionary;
      PdfReferenceHolder pdfReferenceHolder = pdfDictionary["Kids"] as PdfReferenceHolder;
      if ((!(pdfReferenceHolder != (PdfReferenceHolder) null) ? pdfDictionary["Kids"] as PdfArray : pdfReferenceHolder.Object as PdfArray).Count != pages.Count)
      {
        foreach (PdfLoadedPage pdfLoadedPage in pages)
        {
          if (pdfLoadedPage.Contents.Count == 0)
          {
            PdfDictionary dictionary = pdfLoadedPage.Dictionary;
            if (dictionary.ContainsKey("Contents") && !(dictionary["Contents"] is PdfArray))
              (((dictionary["Parent"] as PdfReferenceHolder).Object as PdfDictionary)["Kids"] as PdfArray).Remove((IPdfPrimitive) new PdfReferenceHolder((IPdfPrimitive) dictionary));
          }
        }
      }
    }
    if (endIndex >= pages.Count || startIndex >= pages.Count)
      throw new ArgumentException("Either or both indices are out of range", "endIndex, startIndex");
    List<PdfField> pdfFieldList = new List<PdfField>();
    List<PdfBookmarkBase> bookmarks = new List<PdfBookmarkBase>();
    List<PdfArray> destinations = new List<PdfArray>();
    Dictionary<IPdfPrimitive, object> pageCorrespondance = ldDoc.CrossTable.PageCorrespondance;
    Dictionary<PdfPageBase, object> destinationDictionary = ldDoc.CreateBookmarkDestinationDictionary();
    bool flag = destinationDictionary != null && destinationDictionary.Count > 0;
    int pageCount = 0;
    for (int index = startIndex; index <= endIndex; ++index)
    {
      PdfPageBase pdfPageBase2 = pages[index];
      PdfPageBase pdfPageBase3 = this.ClonePage(ldDoc, pdfPageBase2, destinations);
      pdfPageBase3.Imported = true;
      pageCorrespondance[((IPdfWrapper) pdfPageBase2).Element] = (object) pdfPageBase3;
      ++pageCount;
      if (flag)
      {
        List<object> pageBookmarks = destinationDictionary.ContainsKey(pdfPageBase2) ? destinationDictionary[pdfPageBase2] as List<object> : (List<object>) null;
        if (pageBookmarks != null)
          this.MarkBookmarks(pageBookmarks, bookmarks);
      }
      if (pdfPageBase2.Dictionary.ContainsKey("Resources"))
        pdfPageBase1 = pdfPageBase3;
      else if (pdfPageBase2.Dictionary.ContainsKey("Parent"))
      {
        PdfDictionary pdfDictionary1 = (pdfPageBase2.Dictionary["Parent"] as PdfReferenceHolder).Object as PdfDictionary;
        if (pdfDictionary1.ContainsKey("Resources"))
        {
          PdfResources pdfResources = (PdfResources) null;
          if ((object) (pdfDictionary1["Resources"] as PdfReferenceHolder) != null)
          {
            if ((pdfDictionary1["Resources"] as PdfReferenceHolder).Object is PdfDictionary)
              pdfResources = new PdfResources((pdfDictionary1["Resources"] as PdfReferenceHolder).Object as PdfDictionary);
          }
          else
            pdfResources = new PdfResources(pdfDictionary1["Resources"] as PdfDictionary);
          if (pdfResources != null && (pdfPageBase3 as PdfPage).Dictionary.ContainsKey("Resources"))
          {
            (pdfPageBase3 as PdfPage).Dictionary.Remove("Resources");
            PdfDictionary pdfDictionary2 = (object) (pdfDictionary1["Resources"] as PdfReferenceHolder) == null ? pdfDictionary1["Resources"] as PdfDictionary : (pdfDictionary1["Resources"] as PdfReferenceHolder).Object as PdfDictionary;
            if (pdfDictionary2 != null)
            {
              PdfDictionary pdfDictionary3 = this.EnableMemoryOptimization ? pdfDictionary2.Clone(this.CrossTable) as PdfDictionary : pdfDictionary2;
              (pdfPageBase3 as PdfPage).Dictionary["Resources"] = (IPdfPrimitive) new PdfReferenceHolder((IPdfPrimitive) pdfDictionary3);
            }
            pdfPageBase3.Contents.Clear();
            foreach (IPdfPrimitive content in pdfPageBase2.Contents)
            {
              if (this.EnableMemoryOptimization)
                pdfPageBase3.Contents.Add(content.Clone(this.m_crossTable));
              else
                pdfPageBase3.Contents.Add(content);
            }
            (pdfPageBase3 as PdfPage).Dictionary.Modify();
          }
        }
      }
    }
    for (int index = startIndex; index <= endIndex; ++index)
    {
      List<PdfField> fields = new List<PdfField>();
      PdfPageBase page = ldDoc.Pages[index];
      PdfPageBase newPage = pageCorrespondance[((IPdfWrapper) page).Element] as PdfPageBase;
      this.CheckFields(ldDoc, page, fields);
      if (fields.Count > 0)
      {
        this.AddFields(ldDoc, newPage, fields);
        fields.Clear();
        PdfForm form = this.GetForm();
        if (form != null && !form.m_pageMap.ContainsKey(page.Dictionary))
          form.m_pageMap.Add(page.Dictionary, newPage);
      }
      if (this.EnableMemoryOptimization)
        (newPage as PdfPage).ImportAnnotations(ldDoc, page, destinations);
    }
    this.FixDestinations(pageCorrespondance, destinations);
    if (flag)
    {
      this.ExportBookmarks(ldDoc, bookmarks, pageCount, destinationDictionary);
      this.Bookmarks.CrossTable.Document = this;
    }
    bookmarks.Clear();
    destinations.Clear();
    this.CrossTable.PrevReference = (List<PdfReference>) null;
    return pdfPageBase1;
  }

  private void m_fileStructure_TaggedPdfChanged(object sender, EventArgs e)
  {
    if (!this.m_fileStructure.TaggedPdf)
      return;
    this.Catalog.InitializeStructTreeRoot();
  }

  private void MarkBookmarks(List<object> pageBookmarks, List<PdfBookmarkBase> bookmarks)
  {
    if (pageBookmarks == null)
      return;
    foreach (object pageBookmark in pageBookmarks)
    {
      if (!(pageBookmark is PdfBookmarkBase))
        throw new Exception("Type not specified properly");
      bookmarks.Add(pageBookmark as PdfBookmarkBase);
    }
  }

  private void MarkBookmarks(PdfBookmarkBase bookmarkBase, List<PdfBookmarkBase> bookmarks)
  {
    bookmarks.Add(bookmarkBase);
  }

  public static PdfDocument Merge(string[] paths)
  {
    if (paths == null)
      throw new ArgumentNullException(nameof (paths));
    PdfDocument pdfDocument = new PdfDocument(true);
    pdfDocument.EnableMemoryOptimization = true;
    bool flag = false;
    foreach (string path in paths)
    {
      PdfLoadedDocument ldDoc = path != null ? new PdfLoadedDocument(path) : throw new ArgumentNullException("path");
      if (ldDoc.IsXFAForm)
      {
        flag = true;
        pdfDocument.Form.IsXFA = true;
      }
      else if (ldDoc.Form != null && ldDoc.Form.IsXFAForm)
      {
        flag = true;
        pdfDocument.Form.IsXFA = true;
      }
      pdfDocument.Append(ldDoc);
      ldDoc.Close(true);
    }
    if (((pdfDocument == null ? 0 : (pdfDocument.Form != null ? 1 : 0)) & (flag ? 1 : 0)) != 0)
      pdfDocument.Form.NeedAppearances = true;
    return pdfDocument;
  }

  public static PdfDocumentBase Merge(PdfDocumentBase dest, PdfLoadedDocument src)
  {
    if (src == null)
      throw new ArgumentNullException(nameof (src));
    if (dest == null)
      dest = (PdfDocumentBase) new PdfDocument(true);
    else
      dest.CrossTable.IsMerging = true;
    dest.Append(src);
    if (dest is PdfDocument && (dest as PdfDocument).Form != null)
    {
      if (src.IsXFAForm)
      {
        (dest as PdfDocument).Form.NeedAppearances = true;
        (dest as PdfDocument).Form.IsXFA = true;
        return dest;
      }
      if (src.Form != null && src.Form.IsXFAForm)
      {
        (dest as PdfDocument).Form.NeedAppearances = true;
        (dest as PdfDocument).Form.IsXFA = true;
      }
    }
    return dest;
  }

  public static PdfDocumentBase Merge(PdfDocumentBase dest, params object[] sourceDocuments)
  {
    if (dest == null)
      dest = (PdfDocumentBase) new PdfDocument(true);
    else
      dest.CrossTable.IsMerging = true;
    int index = 0;
    for (int length = sourceDocuments.Length; index < length; ++index)
    {
      object sourceDocument = sourceDocuments[index];
      string filename = sourceDocument as string;
      Stream file1 = sourceDocument as Stream;
      byte[] file2 = sourceDocument as byte[];
      PdfLoadedDocument ldDoc = sourceDocument as PdfLoadedDocument;
      bool flag = true;
      if (filename != null)
        ldDoc = new PdfLoadedDocument(filename);
      else if (file1 != null)
        ldDoc = new PdfLoadedDocument(file1);
      else if (file2 != null)
      {
        ldDoc = new PdfLoadedDocument(file2);
      }
      else
      {
        if (ldDoc == null)
          throw new ArgumentException("Unsupported argument type: " + (object) sourceDocument.GetType());
        flag = false;
      }
      dest.Append(ldDoc);
      if (dest is PdfDocument && (dest as PdfDocument).Form != null && ldDoc.Form != null)
      {
        if (ldDoc.Form.IsXFAForm)
        {
          (dest as PdfDocument).Form.NeedAppearances = true;
          (dest as PdfDocument).Form.IsXFA = true;
        }
        else if (ldDoc.IsXFAForm)
        {
          (dest as PdfDocument).Form.NeedAppearances = true;
          (dest as PdfDocument).Form.IsXFA = true;
        }
      }
      if (flag && dest.EnableMemoryOptimization)
        ldDoc.Close(true);
    }
    return dest;
  }

  private void MergeAttachments(PdfLoadedDocument ldDoc)
  {
    PdfCatalogNames names = ldDoc.Catalog.Names;
    if (names == null)
      return;
    this.Catalog.CreateNamesIfNone();
    if (this.EnableMemoryOptimization)
      this.Catalog.Names.MergeEmbedded(names, this.m_crossTable);
    else
      this.Catalog.Names.MergeEmbedded(names, (PdfCrossTable) null);
  }

  internal void OnDocumentSaved(DocumentSavedEventArgs args)
  {
    if (args == null)
      throw new ArgumentNullException(nameof (args));
    if (this.DocumentSaved == null)
      return;
    this.DocumentSaved((object) this, args);
  }

  public abstract void Save(Stream stream);

  public void Save(string filename)
  {
    switch (filename)
    {
      case null:
        throw new ArgumentNullException("fileName");
      case "":
        throw new ArgumentException("fileName - string can not be empty");
      default:
        string fullPath = Path.GetFullPath(filename);
        string directoryName = Path.GetDirectoryName(fullPath);
        if (!Directory.Exists(directoryName))
          Directory.CreateDirectory(directoryName);
        if (File.Exists(fullPath) && (File.GetAttributes(fullPath) & FileAttributes.ReadOnly) != (FileAttributes) 0)
          throw new ArgumentException("File attributes set to Read-only state. File Name: " + fullPath);
        using (FileStream fileStream = new FileStream(filename, FileMode.Create, FileAccess.ReadWrite, FileShare.Read))
        {
          this.Save((Stream) fileStream);
          break;
        }
    }
  }

  internal void SetCatalog(PdfCatalog catalog)
  {
    this.m_catalog = catalog != null ? catalog : throw new ArgumentNullException(nameof (catalog));
  }

  internal void SetCrossTable(PdfCrossTable cTable)
  {
    this.m_crossTable = cTable != null ? cTable : throw new ArgumentNullException(nameof (cTable));
  }

  internal void SetMainObjectCollection(PdfMainObjectCollection moc)
  {
    this.m_objects = moc != null ? moc : throw new ArgumentNullException(nameof (moc));
  }

  internal void SetSecurity(PdfSecurity security)
  {
    this.m_security = security != null ? security : throw new ArgumentNullException(nameof (security));
  }

  public abstract PdfBookmarkBase Bookmarks { get; }

  internal PdfCatalog Catalog => this.m_catalog;

  public PdfCompressionLevel Compression
  {
    get => this.m_compression;
    set => this.m_compression = value;
  }

  internal PdfCrossTable CrossTable => this.m_crossTable;

  internal PdfReference CurrentSavingObj
  {
    get => this.m_currentSavingObj;
    set => this.m_currentSavingObj = value;
  }

  internal List<IDisposable> DisposeObjects
  {
    get
    {
      if (this.m_disposeObjects == null)
        this.m_disposeObjects = new List<IDisposable>();
      return this.m_disposeObjects;
    }
  }

  public virtual PdfDocumentInformation DocumentInformation
  {
    get
    {
      if (this.m_documentInfo == null)
      {
        this.m_documentInfo = new PdfDocumentInformation(this.Catalog);
        this.CrossTable.Trailer["Info"] = (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) this.m_documentInfo);
      }
      return this.m_documentInfo;
    }
  }

  public bool EnableMemoryOptimization
  {
    get => this.m_enableMemoryOptimization;
    set => this.m_enableMemoryOptimization = value;
  }

  public PdfFileStructure FileStructure
  {
    get
    {
      if (this.m_fileStructure == null)
      {
        this.m_fileStructure = new PdfFileStructure();
        this.m_fileStructure.TaggedPdfChanged += new EventHandler(this.m_fileStructure_TaggedPdfChanged);
      }
      return this.m_fileStructure;
    }
    set => this.m_fileStructure = value;
  }

  internal abstract bool IsPdfViewerDocumentDisable { get; set; }

  internal static bool IsSecurityGranted
  {
    get
    {
      bool isSecurityGranted = false;
      SecurityPermission securityPermission = new SecurityPermission(PermissionState.Unrestricted);
      try
      {
        securityPermission.Demand();
        isSecurityGranted = true;
      }
      catch (SecurityException ex)
      {
      }
      return isSecurityGranted;
    }
  }

  internal abstract int PageCount { get; }

  internal PdfMainObjectCollection PdfObjects => this.m_objects;

  public PdfPortfolioInformation PortfolioInformation
  {
    get => this.m_portfolio;
    set
    {
      this.m_portfolio = value;
      this.m_catalog.PdfPortfolio = this.m_portfolio;
    }
  }

  public PdfSecurity Security
  {
    get
    {
      if (this.m_security == null)
        this.m_security = new PdfSecurity();
      return this.m_security;
    }
  }

  public PdfViewerPreferences ViewerPreferences
  {
    get
    {
      if (this.m_catalog.ViewerPreferences == null)
        this.m_catalog.ViewerPreferences = new PdfViewerPreferences(this.m_catalog);
      return this.m_catalog.ViewerPreferences;
    }
    set
    {
      this.m_catalog.ViewerPreferences = value != null ? value : throw new ArgumentNullException(nameof (ViewerPreferences));
    }
  }

  internal abstract bool WasEncrypted { get; }

  internal delegate void DocumentSavedEventHandler(object sender, DocumentSavedEventArgs args);

  private class NodeInfo
  {
    public PdfBookmarkBase Base;
    public int Index;
    public List<PdfBookmarkBase> Kids;

    public NodeInfo(PdfBookmarkBase bookmarkBase, List<PdfBookmarkBase> kids)
    {
      if (bookmarkBase == null)
        throw new ArgumentNullException(nameof (bookmarkBase));
      if (kids == null)
        throw new ArgumentNullException(nameof (kids));
      this.Base = bookmarkBase;
      this.Kids = kids;
    }
  }
}
