// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Primitives.PdfReferenceHolder
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.IO;
using System;


namespace Syncfusion.Pdf.Primitives
{
    internal class PdfReferenceHolder : IPdfPrimitive
    {
      private PdfCrossTable m_crossTable;
      private int m_index;
      private bool m_isSaving;
      private IPdfPrimitive m_object;
      private int m_objectIndex;
      private int m_position;
      private PdfReference m_reference;
      private ObjectStatus m_status;

      public PdfReferenceHolder(IPdfWrapper wrapper)
        : this(wrapper.Element)
      {
      }

      public PdfReferenceHolder(IPdfPrimitive obj)
      {
        this.m_objectIndex = -1;
        this.m_position = -1;
        this.m_object = obj != null ? obj : throw new ArgumentNullException(nameof (obj));
      }

      internal PdfReferenceHolder(PdfReference reference, PdfCrossTable crossTable)
      {
        this.m_objectIndex = -1;
        this.m_position = -1;
        if (crossTable == null)
          throw new ArgumentNullException(nameof (crossTable));
        if (reference == (PdfReference) null)
          throw new ArgumentNullException(nameof (reference));
        this.m_crossTable = crossTable;
        this.m_reference = reference;
      }

      public IPdfPrimitive Clone(PdfCrossTable crossTable)
      {
        if (this.Reference != (PdfReference) null && this.m_crossTable != null && this.m_crossTable.PageCorrespondance.ContainsKey((IPdfPrimitive) this.Reference))
          return (IPdfPrimitive) new PdfReferenceHolder(this.m_crossTable.PageCorrespondance[(IPdfPrimitive) this.Reference] as PdfReference, crossTable);
        IPdfPrimitive pdfPrimitive;
        if (this.m_crossTable != null && this.m_crossTable.PageCorrespondance.ContainsKey(this.Object))
        {
          if (!(this.m_crossTable.PageCorrespondance[this.Object] is PdfPageBase pdfPageBase))
            return (IPdfPrimitive) new PdfNull();
          pdfPrimitive = (IPdfPrimitive) pdfPageBase.Dictionary;
        }
        else
        {
          if (this.Object is PdfNumber)
            return (IPdfPrimitive) new PdfNumber((this.Object as PdfNumber).FloatValue);
          if (this.Object is PdfDictionary)
          {
            PdfName key = new PdfName("Type");
            PdfDictionary pdfDictionary = this.Object as PdfDictionary;
            if (pdfDictionary.ContainsKey(key) && (pdfDictionary[key] as PdfName).Value == "Page")
              return (IPdfPrimitive) new PdfNull();
          }
          if (crossTable.PrevReference != null && crossTable.PrevReference.Contains(this.Reference))
          {
            IPdfPrimitive clonedObject = this.m_crossTable.GetObject((IPdfPrimitive) this.Reference).ClonedObject;
            return clonedObject != null ? (IPdfPrimitive) new PdfReferenceHolder(crossTable.GetReference(clonedObject), crossTable) : (IPdfPrimitive) new PdfNull();
          }
          if (this.Reference != (PdfReference) null)
            crossTable.PrevReference.Add(this.Reference);
          pdfPrimitive = !(this.Object is PdfCatalog) ? this.Object.Clone(crossTable) : (IPdfPrimitive) crossTable.Document.Catalog;
        }
        return (IPdfPrimitive) new PdfReferenceHolder(crossTable.GetReference(pdfPrimitive), crossTable);
      }

      public override bool Equals(object obj)
      {
        PdfReferenceHolder pdfReferenceHolder = obj as PdfReferenceHolder;
        bool flag = pdfReferenceHolder != (PdfReferenceHolder) null;
        if (!flag)
          return flag;
        return this.m_reference != (PdfReference) null && pdfReferenceHolder.m_reference != (PdfReference) null ? flag & pdfReferenceHolder.m_reference == this.m_reference : flag & pdfReferenceHolder.Object == this.Object;
      }

      public override int GetHashCode() => this.Object.GetHashCode();

      private IPdfPrimitive GetObject()
      {
        IPdfPrimitive pdfPrimitive = (IPdfPrimitive) null;
        if (this.m_reference != (PdfReference) null)
          return this.m_crossTable.PdfObjects.GetObject(this.Index);
        if (this.m_object != null)
          pdfPrimitive = this.m_object;
        return pdfPrimitive;
      }

      public static bool operator ==(PdfReferenceHolder rh1, PdfReferenceHolder rh2)
      {
        object obj1 = (object) rh1;
        object obj2 = (object) rh2;
        return obj1 != null && obj2 != null ? rh1.Equals((object) rh2) : obj1 == obj2;
      }

      public static bool operator !=(PdfReferenceHolder rh1, PdfReferenceHolder rh2) => !(rh1 == rh2);

      public void Save(IPdfWriter writer)
      {
        long num = writer != null ? writer.Position : throw new ArgumentNullException(nameof (writer));
        PdfCrossTable crossTable = writer.Document.CrossTable;
        if (crossTable.Document is PdfDocument)
          this.Object.IsSaving = true;
        PdfReference reference = crossTable.GetReference(this.Object);
        if (writer.Position != num)
          writer.Position = num;
        IPdfWriter writer1 = writer;
        reference.Save(writer1);
      }

      public IPdfPrimitive ClonedObject => (IPdfPrimitive) null;

      internal int Index
      {
        get
        {
          PdfMainObjectCollection pdfObjects = this.m_crossTable.PdfObjects;
          this.m_objectIndex = pdfObjects.GetObjectIndex(this.m_reference);
          if (this.m_objectIndex < 0)
          {
            this.m_crossTable.GetObject((IPdfPrimitive) this.m_reference);
            this.m_objectIndex = pdfObjects.Count - 1;
          }
          return this.m_objectIndex;
        }
      }

      public bool IsSaving
      {
        get => this.m_isSaving;
        set => this.m_isSaving = value;
      }

      internal IPdfPrimitive Object
      {
        get
        {
          if (this.m_reference != (PdfReference) null || this.m_object == null)
            this.m_object = this.GetObject();
          return this.m_object;
        }
      }

      public int ObjectCollectionIndex
      {
        get => this.m_index;
        set => this.m_index = value;
      }

      public int Position
      {
        get => this.m_position;
        set => this.m_position = value;
      }

      public PdfReference Reference => this.m_reference;

      public ObjectStatus Status
      {
        get => this.m_status;
        set => this.m_status = value;
      }
    }
}
