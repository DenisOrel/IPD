// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.PdfPage
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Interactive;
using Syncfusion.Pdf.IO;
using Syncfusion.Pdf.Primitives;
using System;
using System.Drawing;

#nullable disable
namespace Syncfusion.Pdf;

public class PdfPage : PdfPageBase
{
  private PdfAnnotationCollection m_annotations;
  private bool m_isProgressOn;
  private PdfSection m_section;

  public event EventHandler BeginSave;

  public PdfPage()
    : base(new PdfDictionary())
  {
    this.Initialize();
  }

  internal override void Clear()
  {
    base.Clear();
    if (this.m_annotations != null)
      this.m_annotations.Clear();
    this.m_section = (PdfSection) null;
  }

  private void DrawPageTemplates(PdfDocument document)
  {
    if (document == null)
      return;
    if (this.Section.ContainsTemplates(document, this, false))
    {
      PdfPageLayer layer = new PdfPageLayer((PdfPageBase) this, false);
      this.Layers.Insert(0, layer);
      this.Section.DrawTemplates(this, layer, document, false);
    }
    if (!this.Section.ContainsTemplates(document, this, true))
      return;
    PdfPageLayer layer1 = new PdfPageLayer((PdfPageBase) this, false);
    this.Layers.Add(layer1);
    this.Section.DrawTemplates(this, layer1, document, true);
  }

  public SizeF GetClientSize() => this.Section.GetActualBounds(this, true).Size;

  private void Initialize()
  {
    this.Dictionary["Type"] = (IPdfPrimitive) new PdfName("Page");
    this.Dictionary.BeginSave += new SavePdfPrimitiveEventHandler(this.PageBeginSave);
    this.Dictionary.EndSave += new SavePdfPrimitiveEventHandler(this.PageEndSave);
  }

  protected virtual void OnBeginSave(EventArgs e)
  {
    if (this.BeginSave == null)
      return;
    this.BeginSave((object) this, e);
  }

  private void PageBeginSave(object sender, SavePdfPrimitiveEventArgs args)
  {
    if (args.Writer.Document is PdfDocument document)
    {
      this.DrawPageTemplates(document);
      if (this.m_isProgressOn)
        this.Section.OnPageSaving(this);
      if (PdfDocument.ConformanceLevel == PdfConformanceLevel.Pdf_X1A2001)
      {
        this.Dictionary["MediaBox"] = (IPdfPrimitive) PdfArray.FromRectangle(new RectangleF(PointF.Empty, this.Size));
        this.Dictionary["TrimBox"] = (IPdfPrimitive) PdfArray.FromRectangle(new RectangleF(PointF.Empty, this.Size));
      }
      PdfPageTransition transitionSettings = this.Section.GetTransitionSettings();
      if (transitionSettings != null)
      {
        this.Dictionary.SetProperty("Dur", (IPdfPrimitive) new PdfNumber(transitionSettings.PageDuration));
        this.Dictionary.SetProperty("Trans", ((IPdfWrapper) transitionSettings).Element);
      }
      if (document.FileStructure.TaggedPdf && PdfCrossTable.Dereference(document.Catalog["StructTreeRoot"]) is PdfStructTreeRoot)
        this.Dictionary["StructParents"] = (IPdfPrimitive) new PdfNumber(0);
    }
    this.OnBeginSave(new EventArgs());
  }

  private void PageEndSave(object sender, SavePdfPrimitiveEventArgs args)
  {
    if (!(args.Writer.Document is PdfDocument document))
      return;
    this.RemoveTemplateLayers(document);
  }

  private void RemoveTemplateLayers(PdfDocument document)
  {
    if (document == null)
      throw new ArgumentNullException(nameof (document));
    int num = this.Section.ContainsTemplates(document, this, false) ? 1 : 0;
    bool flag = this.Section.ContainsTemplates(document, this, true);
    if (num != 0)
      this.Layers.RemoveAt(0);
    if (!flag)
      return;
    this.Layers.RemoveAt(this.Layers.Count - 1);
  }

  internal void ResetProgress() => this.m_isProgressOn = false;

  internal void SetProgress() => this.m_isProgressOn = true;

  internal void SetSection(PdfSection section)
  {
    this.m_section = this.m_section == null ? section : throw new PdfException("The page already exists in some section, it can't be contained by several sections");
    this.Dictionary["Parent"] = (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) section);
  }

  public PdfAnnotationCollection Annotations
  {
    get
    {
      if (this.m_annotations == null)
      {
        this.m_annotations = new PdfAnnotationCollection(this);
        this.Dictionary["Annots"] = ((IPdfWrapper) this.m_annotations).Element;
      }
      return this.m_annotations;
    }
  }

  internal PdfCrossTable CrossTable
  {
    get
    {
      return this.m_section.Parent != null ? this.m_section.Parent.Document.CrossTable : this.m_section.ParentDocument.CrossTable;
    }
  }

  internal PdfDocument Document
  {
    get => this.m_section != null ? this.m_section.Parent.Document : (PdfDocument) null;
  }

  internal override PointF Origin => this.Section.PageSettings.Origin;

  public PdfSection Section
  {
    get
    {
      return this.m_section != null ? this.m_section : throw new PdfException("Page must be added to some section before using.");
    }
    internal set => this.m_section = value;
  }

  public override SizeF Size => this.Section.PageSettings.Size;
}
