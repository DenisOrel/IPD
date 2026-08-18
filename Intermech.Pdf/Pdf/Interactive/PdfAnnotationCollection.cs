// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfAnnotationCollection
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using System;


namespace Syncfusion.Pdf.Interactive
{
    public class PdfAnnotationCollection : PdfCollection, IPdfWrapper
    {
      private string AlreadyExistsAnnotationError;
      private PdfArray m_annotations;
      private PdfPage m_page;
      private string MissingAnnotationException;

      public PdfAnnotationCollection()
      {
        this.AlreadyExistsAnnotationError = "This annotatation had been already added to page";
        this.MissingAnnotationException = "Annotation is not contained in collection.";
        this.m_annotations = new PdfArray();
      }

      public PdfAnnotationCollection(PdfPage page)
      {
        this.AlreadyExistsAnnotationError = "This annotatation had been already added to page";
        this.MissingAnnotationException = "Annotation is not contained in collection.";
        this.m_annotations = new PdfArray();
        this.m_page = page != null ? page : throw new ArgumentNullException(nameof (page));
      }

      public virtual int Add(PdfAnnotation annotation)
      {
        if (annotation == null)
          throw new ArgumentNullException(nameof (annotation));
        if (annotation is PdfTextMarkupAnnotation)
          (annotation as PdfTextMarkupAnnotation).SetQuadPoints(this.m_page.Size);
        this.SetPrint(annotation);
        return this.DoAdd(annotation);
      }

      private int AddAnnotation(PdfAnnotation annotation)
      {
        annotation.SetPage((PdfPageBase) this.m_page);
        int num = this.List.Add((object) annotation);
        this.m_annotations.Add((IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) annotation));
        return num;
      }

      public void Clear() => this.DoClear();

      public bool Contains(PdfAnnotation annotation)
      {
        return annotation != null ? this.List.Contains((object) annotation) : throw new ArgumentNullException(nameof (annotation));
      }

      protected virtual int DoAdd(PdfAnnotation annot)
      {
        annot.SetPage((PdfPageBase) this.m_page);
        this.m_annotations.Add((IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) annot));
        return this.List.Add((object) annot);
      }

      protected virtual void DoClear()
      {
        this.m_annotations.Clear();
        this.List.Clear();
      }

      protected virtual void DoInsert(int index, PdfAnnotation annot)
      {
        this.m_annotations.Insert(index, (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) annot));
        this.List.Insert(index, (object) annot);
      }

      protected virtual void DoRemove(PdfAnnotation annot)
      {
        int index = this.List.IndexOf((object) annot);
        this.m_annotations.RemoveAt(index);
        this.List.RemoveAt(index);
      }

      protected virtual void DoRemoveAt(int index)
      {
        this.m_annotations.RemoveAt(index);
        this.List.RemoveAt(index);
      }

      public int IndexOf(PdfAnnotation annotation)
      {
        return annotation != null ? this.List.IndexOf((object) annotation) : throw new ArgumentNullException(nameof (annotation));
      }

      public void Insert(int index, PdfAnnotation annotation) => this.DoInsert(index, annotation);

      private void InsertAnnotation(int index, PdfAnnotation annotation)
      {
        annotation.SetPage((PdfPageBase) this.m_page);
        this.List.Insert(index, (object) annotation);
        this.m_annotations.Insert(index, (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) annotation));
      }

      public void Remove(PdfAnnotation annot)
      {
        if (annot == null)
          throw new ArgumentNullException("annotation");
        this.DoRemove(annot);
      }

      private void RemoveAnnotation(PdfAnnotation annotation)
      {
        int index = this.List.IndexOf((object) annotation);
        annotation.SetPage((PdfPageBase) null);
        this.List.Remove((object) annotation);
        this.m_annotations.RemoveAt(index);
      }

      private void RemoveAnnotationAt(int index) => this.DoRemoveAt(index);

      public void RemoveAt(int index)
      {
        if (index < 0 || index > this.Count - 1)
          throw new ArgumentOutOfRangeException(nameof (index), "Index is out of range.");
        this.RemoveAnnotationAt(index);
      }

      public void SetPrint(PdfAnnotation annot)
      {
        if (this.m_page.Document.Conformance != PdfConformanceLevel.Pdf_A1B)
          return;
        annot.Dictionary.SetNumber("F", 4);
      }

      internal PdfArray Annotations
      {
        get => this.m_annotations;
        set => this.m_annotations = value;
      }

      public virtual PdfAnnotation this[int index]
      {
        get
        {
          return index >= 0 && index <= this.Count - 1 ? (PdfAnnotation) this.List[index] : throw new ArgumentOutOfRangeException(nameof (index), "Index is out of range.");
        }
      }

      IPdfPrimitive IPdfWrapper.Element => (IPdfPrimitive) this.m_annotations;
    }
}
