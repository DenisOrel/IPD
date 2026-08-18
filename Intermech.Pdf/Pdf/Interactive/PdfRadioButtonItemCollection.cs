// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfRadioButtonItemCollection
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using System;
using System.Collections;


namespace Syncfusion.Pdf.Interactive
{
    public class PdfRadioButtonItemCollection : PdfCollection, IPdfWrapper
    {
      private PdfArray m_array = new PdfArray();
      private PdfRadioButtonListField m_field;

      public PdfRadioButtonItemCollection(PdfRadioButtonListField field) => this.m_field = field;

      public int Add(PdfRadioButtonListItem item)
      {
        return item != null ? this.DoAdd(item) : throw new ArgumentNullException(nameof (item));
      }

      public void Clear() => this.DoClear();

      public bool Contains(PdfRadioButtonListItem item) => this.List.Contains((object) item);

      private int DoAdd(PdfRadioButtonListItem item)
      {
        this.m_array.Add((IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) item));
        item.SetField(this.m_field);
        return this.List.Add((object) item);
      }

      private void DoClear()
      {
        foreach (PdfRadioButtonListItem radioButtonListItem in (IEnumerable) this.List)
          radioButtonListItem.SetField((PdfRadioButtonListField) null);
        this.m_array.Clear();
        this.List.Clear();
      }

      private void DoInsert(int index, PdfRadioButtonListItem item)
      {
        this.m_array.Insert(index, (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) item));
        item.SetField(this.m_field);
        this.List.Insert(index, (object) item);
      }

      private void DoRemove(PdfRadioButtonListItem item)
      {
        if (!this.List.Contains((object) item))
          return;
        int index = this.List.IndexOf((object) item);
        this.m_array.RemoveAt(index);
        item.SetField((PdfRadioButtonListField) null);
        this.List.RemoveAt(index);
      }

      public int IndexOf(PdfRadioButtonListItem item) => this.List.IndexOf((object) item);

      public void Insert(int index, PdfRadioButtonListItem item)
      {
        if (item == null)
          throw new ArgumentNullException(nameof (item));
        this.DoInsert(index, item);
      }

      public void Remove(PdfRadioButtonListItem item)
      {
        if (item == null)
          throw new ArgumentNullException(nameof (item));
        this.DoRemove(item);
      }

      public void RemoveAt(int index)
      {
        if (index < 0 || index >= this.List.Count)
          throw new ArgumentOutOfRangeException(nameof (index));
        PdfRadioButtonListItem radioButtonListItem = (PdfRadioButtonListItem) this.List[index];
        this.m_array.RemoveAt(index);
        this.List.RemoveAt(index);
      }

      public PdfRadioButtonListItem this[int index] => (PdfRadioButtonListItem) this.List[index];

      IPdfPrimitive IPdfWrapper.Element => (IPdfPrimitive) this.m_array;
    }
}
