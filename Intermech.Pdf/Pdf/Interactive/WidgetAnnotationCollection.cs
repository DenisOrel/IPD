// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.WidgetAnnotationCollection
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using System;


namespace Syncfusion.Pdf.Interactive
{
    internal class WidgetAnnotationCollection : PdfCollection, IPdfWrapper
    {
      private PdfArray m_array = new PdfArray();

      public int Add(WidgetAnnotation annotation)
      {
        return annotation != null ? this.DoAdd(annotation) : throw new ArgumentNullException(nameof (annotation));
      }

      public void Clear() => this.DoClear();

      public bool Contains(WidgetAnnotation annotation)
      {
        return annotation != null ? this.List.Contains((object) annotation) : throw new ArgumentNullException(nameof (annotation));
      }

      private int DoAdd(WidgetAnnotation annotation)
      {
        this.m_array.Add((IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) annotation));
        return this.List.Add((object) annotation);
      }

      private void DoClear()
      {
        this.m_array.Clear();
        this.List.Clear();
      }

      private void DoInsert(int index, WidgetAnnotation annotation)
      {
        this.m_array.Insert(index, (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) annotation));
        this.List.Insert(index, (object) annotation);
      }

      private void DoRemove(WidgetAnnotation annotation)
      {
        int index = this.List.IndexOf((object) annotation);
        this.m_array.RemoveAt(index);
        this.List.RemoveAt(index);
      }

      private void DoRemoveAt(int index)
      {
        this.m_array.RemoveAt(index);
        this.List.RemoveAt(index);
      }

      public int IndexOf(WidgetAnnotation annotation)
      {
        return annotation != null ? this.List.IndexOf((object) annotation) : throw new ArgumentNullException(nameof (annotation));
      }

      public void Insert(int index, WidgetAnnotation annotation)
      {
        if (annotation == null)
          throw new ArgumentNullException(nameof (annotation));
        this.DoInsert(index, annotation);
      }

      public void Remove(WidgetAnnotation annotation)
      {
        if (annotation == null)
          throw new ArgumentNullException(nameof (annotation));
        this.DoRemove(annotation);
      }

      public void RemoveAt(int index) => this.DoRemoveAt(index);

      public WidgetAnnotation this[int index] => (WidgetAnnotation) this.List[index];

      IPdfPrimitive IPdfWrapper.Element => (IPdfPrimitive) this.m_array;
    }
}
