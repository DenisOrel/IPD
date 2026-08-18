// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Parsing.PdfLoadedPageCollection
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Interactive;
using Syncfusion.Pdf.IO;
using Syncfusion.Pdf.Primitives;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

#nullable disable
namespace Syncfusion.Pdf.Parsing;

public class PdfLoadedPageCollection : IEnumerable
{
  private PdfCrossTable m_crossTable;
  private PdfDocumentBase m_document;
  private PdfLoadedDocument m_loadedDocument;
  internal static int m_nestedPages;
  private int m_pageDuplicaton;
  private Dictionary<PdfDictionary, PdfPageBase> m_pagesCash;
  internal static int m_parentKidsCount;
  internal static int m_parentKidsCounttemp;
  internal static int m_repeatIndex;
  private int m_sectionCount;

  internal PdfLoadedPageCollection(PdfDocumentBase document, PdfCrossTable crossTable)
  {
    if (document == null)
      throw new ArgumentNullException(nameof (document));
    if (crossTable == null)
      throw new ArgumentNullException(nameof (crossTable));
    if (this.m_document == null)
      this.m_document = (PdfDocumentBase) new PdfDocument();
    this.m_document = document;
    this.m_crossTable = crossTable;
  }

  public PdfPageBase Add() => this.Insert(this.Count);

  public PdfPageBase Add(SizeF size) => this.Insert(this.Count, size);

  internal PdfPageBase Add(PdfLoadedDocument ldDoc, PdfPageBase page)
  {
    if (ldDoc == null)
      throw new ArgumentNullException(nameof (ldDoc));
    PdfTemplate template = page != null ? page.GetContent() : throw new ArgumentNullException(nameof (page));
    PdfPage pdfPage = this.Add(page.Size, new PdfMargins(), page.Rotation) as PdfPage;
    if (template != null)
      pdfPage.Graphics.DrawPdfTemplate(template, PointF.Empty);
    if (pdfPage.Document != null && !pdfPage.Document.EnableMemoryOptimization)
      pdfPage.ImportAnnotations(ldDoc, page);
    return (PdfPageBase) pdfPage;
  }

  public PdfPageBase Add(SizeF size, PdfMargins margins) => this.Insert(this.Count, size, margins);

  internal PdfPageBase Add(PdfLoadedDocument ldDoc, PdfPageBase page, List<PdfArray> destinations)
  {
    if (ldDoc == null)
      throw new ArgumentNullException(nameof (ldDoc));
    PdfTemplate template = page != null ? page.ContentTemplate : throw new ArgumentNullException(nameof (page));
    PdfPage pdfPage = this.Add(page.Size, new PdfMargins(), page.Rotation) as PdfPage;
    if (template != null)
      pdfPage.Graphics.DrawPdfTemplate(template, PointF.Empty);
    pdfPage.ImportAnnotations(ldDoc, page, destinations);
    return (PdfPageBase) pdfPage;
  }

  public PdfPageBase Add(SizeF size, PdfMargins margins, PdfPageRotateAngle rotation)
  {
    return this.Insert(this.Count, size, margins, rotation);
  }

  internal PdfPageBase Add(
    SizeF size,
    PdfMargins margins,
    PdfPageRotateAngle rotation,
    int location)
  {
    return this.Insert(location, size, margins, rotation);
  }

  internal void Clear()
  {
    if (this.m_pagesCash != null)
    {
      for (IEnumerator enumerator = (IEnumerator) this.m_pagesCash.Keys.GetEnumerator(); enumerator.MoveNext(); enumerator = (IEnumerator) this.m_pagesCash.Keys.GetEnumerator())
      {
        PdfDictionary current = enumerator.Current as PdfDictionary;
        this.Remove(this.m_pagesCash[current]);
        current.Clear();
        this.m_pagesCash.Remove(current);
      }
      this.m_pagesCash.Clear();
    }
    this.m_crossTable = (PdfCrossTable) null;
    this.m_document = (PdfDocumentBase) null;
    this.m_loadedDocument = (PdfLoadedDocument) null;
  }

  public IEnumerator GetEnumerator() => (IEnumerator) new PdfLoadedPageEnumerator(this);

  private int GetNodeCount(PdfDictionary node)
  {
    return this.m_crossTable.GetObject(node["Count"]) is PdfNumber pdfNumber ? pdfNumber.IntValue : 0;
  }

  private PdfArray GetNodeKids(PdfDictionary node)
  {
    return this.m_crossTable.GetObject(node["Kids"]) as PdfArray;
  }

  internal PdfPageBase GetPage(PdfDictionary dic)
  {
    Dictionary<PdfDictionary, PdfPageBase> pageCache = this.PageCache;
    PdfPageBase page = (PdfPageBase) null;
    if (pageCache.ContainsKey(dic))
      page = pageCache[dic];
    if (page == null)
    {
      page = (PdfPageBase) new PdfLoadedPage(this.m_document, this.m_crossTable, dic);
      pageCache[dic] = page;
    }
    return page;
  }

  private PdfPageBase GetPage(int index)
  {
    int localIndex;
    PdfArray nodeKids1 = this.GetNodeKids(this.GetParent(index, out localIndex, true));
    int index1 = localIndex;
    int index2 = 0;
    PdfDictionary pdfDictionary;
    PdfArray nodeKids2;
    do
    {
      pdfDictionary = this.m_crossTable.GetObject(nodeKids1[localIndex]) as PdfDictionary;
      if ((pdfDictionary["Type"] as PdfName).Value == "Pages")
      {
        ++index1;
        pdfDictionary = this.m_crossTable.GetObject(nodeKids1[index1]) as PdfDictionary;
        nodeKids2 = this.GetNodeKids(pdfDictionary);
        if (nodeKids2 == null)
          goto label_5;
      }
      else
        goto label_5;
    }
    while (nodeKids2.Count <= 0);
    pdfDictionary = this.m_crossTable.GetObject(nodeKids2[index2]) as PdfDictionary;
    int num = index2 + 1;
label_5:
    return this.GetPage(pdfDictionary);
  }

  private PdfDictionary GetParent(int index, out int localIndex, bool zeroValid)
  {
    if (index < 0 && index > this.Count)
      throw new ArgumentOutOfRangeException(nameof (index), "The index should be within this range: [0; Count]");
    PdfDictionary node1 = this.m_crossTable.GetObject(this.m_document.Catalog["Pages"]) as PdfDictionary;
    int num = 0;
    localIndex = this.GetNodeCount(node1);
    if (index == 0 && !zeroValid)
    {
      localIndex = 0;
      return node1;
    }
    if (index < this.Count)
    {
      PdfArray nodeKids = this.GetNodeKids(node1);
      int index1 = 0;
      for (int count = nodeKids.Count; index1 < count; ++index1)
      {
        PdfDictionary node2 = this.m_crossTable.GetObject(nodeKids[index1]) as PdfDictionary;
        if (this.IsNodeLeaf(node2))
        {
          if (num + index1 == index)
          {
            localIndex = index1;
            return node1;
          }
        }
        else
        {
          int nodeCount = this.GetNodeCount(node2);
          if (index < num + nodeCount + index1)
          {
            num += index1;
            node1 = node2;
            nodeKids = this.GetNodeKids(node1);
            index1 = -1;
            count = nodeKids.Count;
          }
          else
            num += nodeCount - 1;
        }
      }
      return node1;
    }
    localIndex = this.GetNodeKids(node1).Count;
    return node1;
  }

  internal int IndexOf(PdfPageBase page)
  {
    if (page == null)
      throw new ArgumentNullException(nameof (page));
    int index = 0;
    for (int count = this.Count; index < count; ++index)
    {
      if (this.GetPage(index) == page)
        return index;
    }
    return -1;
  }

  public PdfPageBase Insert(int index) => this.Insert(index, SizeF.Empty);

  public PdfPageBase Insert(int index, SizeF size) => this.Insert(index, size, (PdfMargins) null);

  public PdfPageBase Insert(int index, SizeF size, PdfMargins margins)
  {
    PdfPageRotateAngle rotation = PdfPageRotateAngle.RotateAngle0;
    return this.Insert(index, size, margins, rotation);
  }

  public PdfPageBase Insert(
    int index,
    SizeF size,
    PdfMargins margins,
    PdfPageRotateAngle rotation)
  {
    PdfPageOrientation orientation = (double) size.Width > (double) size.Height ? PdfPageOrientation.Landscape : PdfPageOrientation.Portrait;
    return this.Insert(index, size, margins, rotation, orientation);
  }

  public PdfPageBase Insert(
    int index,
    SizeF size,
    PdfMargins margins,
    PdfPageRotateAngle rotation,
    PdfPageOrientation orientation)
  {
    if (size == SizeF.Empty)
      size = PdfPageSize.A4;
    PdfPage page = new PdfPage();
    PdfPageSettings pageSettings = new PdfPageSettings(size, orientation, 0.0f);
    pageSettings.Size = size;
    if (margins == null)
    {
      margins = new PdfMargins();
      margins.All = 40f;
    }
    pageSettings.Margins = margins;
    pageSettings.Rotate = rotation;
    PdfSection pdfSection = new PdfSection(this.m_document, pageSettings);
    pdfSection.DropCropBox();
    pdfSection.Add(page);
    PdfDictionary element = ((IPdfWrapper) pdfSection).Element as PdfDictionary;
    int localIndex;
    PdfDictionary parent = this.GetParent(index, out localIndex, false);
    element["Parent"] = (IPdfPrimitive) new PdfReferenceHolder((IPdfPrimitive) parent);
    this.GetNodeKids(parent).Insert(localIndex, (IPdfPrimitive) new PdfReferenceHolder((IPdfPrimitive) element));
    this.UpdateCount(parent);
    this.PageCache[page.Dictionary] = (PdfPageBase) page;
    page.Graphics.ColorSpace = (this.m_document as PdfLoadedDocument).ColorSpace;
    page.Graphics.Layer.Colorspace = (this.m_document as PdfLoadedDocument).ColorSpace;
    return (PdfPageBase) page;
  }

  private bool IsNodeLeaf(PdfDictionary node) => this.GetNodeCount(node) == 0;

  public void ReArrange(int[] orderArray)
  {
    int[] numArray1 = new int[orderArray.Length];
    int[] numArray2 = new int[orderArray.Length];
    int[] numArray3 = new int[orderArray.Length];
    int index1 = 0;
    int length = orderArray.Length;
    int num1 = length;
    int count1 = this.Count;
    int num2 = 0;
    for (int index2 = 0; index2 < orderArray.Length; ++index2)
    {
      if (orderArray[index2] >= this.Count)
        throw new ArgumentException("The page Index is not Valid");
    }
    PdfDictionary pdfDictionary1 = this.m_crossTable.GetObject(this.m_document.Catalog["Pages"]) as PdfDictionary;
    PdfArray pdfArray1 = pdfDictionary1["Kids"] as PdfArray;
    int count2 = pdfArray1.Count;
    this.m_loadedDocument = this.m_document as PdfLoadedDocument;
    PdfLoadedPageCollection.m_parentKidsCount = this.Count;
    for (int index3 = 0; index3 < length; ++index3)
    {
      for (int index4 = index3 + 1; index4 < length; ++index4)
      {
        if (orderArray[index3] == orderArray[index4])
        {
          this.m_pageDuplicaton = 1;
          if (numArray1[index4] == 0)
          {
            ++num2;
            numArray1[index4] = 1;
            numArray2[index1] = index4;
            ++index1;
          }
        }
      }
    }
    if (this.m_pageDuplicaton == 1)
    {
      for (int index5 = 0; index5 < numArray2.Length; ++index5)
      {
        for (int index6 = index5 + 1; index6 < numArray2.Length; ++index6)
        {
          if (numArray2[index6] != 0 && numArray2[index5] > numArray2[index6])
          {
            int num3 = numArray2[index5];
            numArray2[index5] = numArray2[index6];
            numArray2[index6] = num3;
          }
        }
      }
      int num4 = numArray2[0];
      PdfLoadedPageCollection.m_repeatIndex = num4;
      int num5 = num2;
      if (length > count1)
      {
        int index7 = num4;
        for (int index8 = 0; index8 < num5; ++index8)
        {
          int count3 = this.Count;
          this.m_loadedDocument.Pages.Add(this.m_loadedDocument, this.GetPage(orderArray[index7]));
          numArray3[index7] = 1;
          ++index7;
        }
      }
      else
      {
        int index9 = num4;
        for (int index10 = 0; index10 < num5; ++index10)
        {
          int count4 = this.Count;
          this.m_loadedDocument.Pages.Add(this.m_loadedDocument, this.GetPage(orderArray[index9]));
          numArray3[index9] = 1;
          ++index9;
          int count5 = this.Count;
        }
      }
      for (int index11 = num4; index11 < num1; ++index11)
      {
        if (numArray3[index11] == 0)
        {
          int count6 = this.Count;
          this.m_loadedDocument.Pages.Add(this.m_loadedDocument, this.GetPage(orderArray[index11]));
        }
      }
    }
    List<long> longList = new List<long>();
    int localIndex;
    PdfDictionary parent1 = this.GetParent(0, out localIndex, true);
    PdfArray nodeKids1 = this.GetNodeKids(parent1);
    PdfLoadedPageCollection.m_parentKidsCounttemp = nodeKids1.Count;
    int count7 = nodeKids1.Count;
    for (int index12 = 0; index12 < nodeKids1.Count; ++index12)
    {
      PdfReference reference = (nodeKids1[index12] as PdfReferenceHolder).Reference;
      longList.Add(reference.ObjNum);
    }
    int num6 = 0;
    while (count7 < this.Count)
    {
      PdfLoadedPageCollection.m_nestedPages = 1;
      PdfDictionary parent2 = this.GetParent(count7, out localIndex, true);
      PdfArray pdfArray2 = parent2["Kids"] as PdfArray;
      for (int index13 = 0; index13 < this.GetNodeKids(parent2).Count; ++index13)
      {
        PdfDictionary node = (pdfArray2[index13] as PdfReferenceHolder).Object as PdfDictionary;
        PdfReference reference = (pdfArray2[index13] as PdfReferenceHolder).Reference;
        if (node["Type"].ToString() == "/Pages")
        {
          PdfArray nodeKids2 = this.GetNodeKids(node);
          for (int index14 = 0; index14 < nodeKids2.Count; ++index14)
          {
            reference = (nodeKids2[index14] as PdfReferenceHolder).Reference;
            if (!longList.Contains(reference.ObjNum))
            {
              longList.Add(reference.ObjNum);
              nodeKids1.Insert(count7, this.GetNodeKids(parent2)[index13]);
              ++count7;
            }
          }
        }
        if (node["Type"].ToString() == "/Page" && !longList.Contains(reference.ObjNum))
        {
          longList.Add(reference.ObjNum);
          nodeKids1.Insert(count7, this.GetNodeKids(parent2)[index13]);
          ++count7;
        }
      }
      ++num6;
    }
    PdfLoadedPageCollection.m_parentKidsCounttemp = nodeKids1.Count;
    nodeKids1.ReArrange(orderArray);
    int num7 = count7 - orderArray.Length;
    if (num7 != 0)
    {
      for (int index15 = 0; index15 < num7; ++index15)
        this.UpdateCountDecrement(parent1);
    }
    if (PdfLoadedPageCollection.m_nestedPages != 1)
      return;
    PdfReferenceHolder pdfReferenceHolder = pdfArray1[0] as PdfReferenceHolder;
    long objNum = pdfReferenceHolder.Reference.ObjNum;
    PdfArray pdfArray3 = parent1["Kids"] as PdfArray;
    PdfReferenceHolder[] pdfReferenceHolderArray1 = new PdfReferenceHolder[pdfArray3.Count];
    PdfReferenceHolder[] pdfReferenceHolderArray2 = new PdfReferenceHolder[pdfArray3.Count];
    PdfReferenceHolder primitive1 = (pdfReferenceHolder.Object as PdfDictionary)["Parent"] as PdfReferenceHolder;
    PdfArray primitive2 = new PdfArray();
    for (int index16 = 0; index16 < pdfArray3.Count; ++index16)
    {
      PdfArray pdfArray4 = new PdfArray();
      PdfArray pdfArray5 = new PdfArray();
      pdfReferenceHolderArray1[index16] = pdfArray3[index16] as PdfReferenceHolder;
      PdfDictionary pdfDictionary2 = pdfReferenceHolderArray1[index16].Object as PdfDictionary;
      PdfDictionary pdfDictionary3 = (pdfDictionary2["Parent"] as PdfReferenceHolder).Object as PdfDictionary;
      if (pdfDictionary2["Type"].ToString() == "/Pages")
      {
        if (pdfDictionary2.ContainsKey("CropBox"))
        {
          PdfArray primitive3 = pdfDictionary2["CropBox"] as PdfArray;
          pdfDictionary2.SetProperty("CropBox", (IPdfPrimitive) primitive3);
        }
        if (pdfDictionary2.ContainsKey("MediaBox"))
        {
          PdfArray primitive4 = pdfDictionary2["MediaBox"] as PdfArray;
          pdfDictionary2.SetProperty("MediaBox", (IPdfPrimitive) primitive4);
        }
      }
      else
      {
        if (pdfDictionary3.ContainsKey("CropBox"))
        {
          PdfArray primitive5 = pdfDictionary3["CropBox"] as PdfArray;
          pdfDictionary2.SetProperty("CropBox", (IPdfPrimitive) primitive5);
        }
        if (pdfDictionary3.ContainsKey("MediaBox"))
        {
          PdfArray primitive6 = pdfDictionary3["MediaBox"] as PdfArray;
          pdfDictionary2.SetProperty("MediaBox", (IPdfPrimitive) primitive6);
        }
      }
      pdfDictionary2.SetProperty("Parent", (IPdfPrimitive) primitive1);
      pdfReferenceHolderArray2[index16] = pdfDictionary2["Parent"] as PdfReferenceHolder;
      primitive2.Add((IPdfPrimitive) pdfReferenceHolderArray1[index16]);
    }
    PdfLoadedPageCollection.m_parentKidsCounttemp = primitive2.Count;
    pdfDictionary1.SetProperty("Kids", (IPdfPrimitive) primitive2);
    pdfDictionary1.SetNumber("Count", orderArray.Length);
    if (primitive2.Count != 0)
      return;
    parent1.SetNumber("Count", 0);
  }

  public void Remove(PdfPageBase page)
  {
    int index1 = this.IndexOf(page);
    if (index1 <= -1)
      return;
    Dictionary<PdfPageBase, object> destinationDictionary = (this.m_document as PdfLoadedDocument).CreateBookmarkDestinationDictionary();
    if (destinationDictionary != null)
    {
      List<object> objectList = (List<object>) null;
      if (destinationDictionary.ContainsKey(page))
        objectList = destinationDictionary[page] as List<object>;
      if (objectList != null)
      {
        for (int index2 = 0; index2 < objectList.Count; ++index2)
        {
          PdfBookmarkBase pdfBookmarkBase = objectList[index2] as PdfBookmarkBase;
          PdfDestination wrapper = (PdfDestination) null;
          if (pdfBookmarkBase.Dictionary["A"] != null)
            pdfBookmarkBase.Dictionary.SetProperty("A", (IPdfWrapper) wrapper);
          pdfBookmarkBase.Dictionary.SetProperty("Dest", (IPdfWrapper) wrapper);
        }
      }
    }
    PdfDictionary element1 = ((IPdfWrapper) page).Element as PdfDictionary;
    PdfDictionary parent = this.GetParent(index1, out int _, true);
    element1["Parent"] = (IPdfPrimitive) new PdfReferenceHolder((IPdfPrimitive) parent);
    PdfArray nodeKids = this.GetNodeKids(parent);
    if (index1 == 0)
    {
      PdfCrossTable crossTable = this.m_document.CrossTable;
      if (crossTable.DocumentCatalog != null)
      {
        if (crossTable.DocumentCatalog["OpenAction"] is PdfArray pdfArray1)
        {
          pdfArray1.Remove((IPdfPrimitive) new PdfReferenceHolder((IPdfPrimitive) element1));
        }
        else
        {
          PdfReferenceHolder pdfReferenceHolder = crossTable.DocumentCatalog["OpenAction"] as PdfReferenceHolder;
          if (pdfReferenceHolder != (PdfReferenceHolder) null && (pdfReferenceHolder.Object as PdfDictionary)["D"] is PdfArray pdfArray)
            pdfArray.Remove((IPdfPrimitive) new PdfReferenceHolder((IPdfPrimitive) element1));
        }
      }
    }
    PdfReferenceHolder element2 = (PdfReferenceHolder) null;
    foreach (PdfReferenceHolder pdfReferenceHolder in nodeKids)
    {
      if (pdfReferenceHolder.Object == element1)
      {
        element2 = pdfReferenceHolder;
        break;
      }
    }
    if (element2 != (PdfReferenceHolder) null)
      nodeKids.Remove((IPdfPrimitive) element2);
    this.UpdateCountDecrement(parent);
  }

  public void RemoveAt(int index) => this.Remove(this.GetPage(index));

  internal void UpdateCount(PdfDictionary parent)
  {
    for (; parent != null; parent = PdfCrossTable.Dereference(parent["Parent"]) as PdfDictionary)
    {
      int num = this.GetNodeCount(parent) + 1;
      parent.SetNumber("Count", num);
    }
  }

  private void UpdateCountDecrement(PdfDictionary parent)
  {
    for (; parent != null; parent = PdfCrossTable.Dereference(parent["Parent"]) as PdfDictionary)
    {
      if (this.GetNodeCount(parent) - 1 == 0)
      {
        PdfDictionary pdfDictionary1 = parent;
        if (PdfCrossTable.Dereference(parent["Parent"]) is PdfDictionary pdfDictionary2 && pdfDictionary2["Kids"] is PdfArray pdfArray)
          pdfArray.Remove((IPdfPrimitive) new PdfReferenceHolder((IPdfPrimitive) pdfDictionary1));
      }
      int num = this.GetNodeCount(parent) - 1;
      parent.SetNumber("Count", num);
    }
  }

  public int Count
  {
    get
    {
      return this.GetNodeCount(this.m_crossTable.GetObject(this.m_document.Catalog["Pages"]) as PdfDictionary);
    }
  }

  public PdfPageBase this[int index] => this.GetPage(index);

  private PdfLoadedDocument LoadedDocument => this.m_loadedDocument;

  private Dictionary<PdfDictionary, PdfPageBase> PageCache
  {
    get
    {
      if (this.m_pagesCash == null)
        this.m_pagesCash = new Dictionary<PdfDictionary, PdfPageBase>();
      return this.m_pagesCash;
    }
  }

  public int SectionCount
  {
    get
    {
      return ((this.m_crossTable.GetObject(this.m_document.Catalog["Pages"]) as PdfDictionary)["Kids"] as PdfArray).Count;
    }
  }
}
