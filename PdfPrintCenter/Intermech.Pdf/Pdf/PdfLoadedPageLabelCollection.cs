// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.PdfLoadedPageLabelCollection
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using System;
using System.Collections.Generic;

#nullable disable
namespace Syncfusion.Pdf;

public class PdfLoadedPageLabelCollection : IPdfWrapper
{
  private int m_count;
  private List<PdfPageLabel> m_pageLabel = new List<PdfPageLabel>();
  private List<PdfReferenceHolder> m_pageLabelCollection = new List<PdfReferenceHolder>();

  public void Add(PdfPageLabel pageLabel)
  {
    PdfReferenceHolder pdfReferenceHolder = pageLabel != null ? new PdfReferenceHolder((IPdfWrapper) pageLabel) : throw new ArgumentNullException("section");
    this.m_pageLabel.Add(pageLabel);
    this.m_pageLabelCollection.Add(pdfReferenceHolder);
    ++this.m_count;
  }

  public int Count => this.m_count;

  public PdfPageLabel this[int index] => this.m_pageLabel[index];

  IPdfPrimitive IPdfWrapper.Element => (IPdfPrimitive) null;
}
