// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.IO.PdfMainObjectCollection
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using System;
using System.Collections.Generic;


namespace Syncfusion.Pdf.IO
{
    internal class PdfMainObjectCollection
    {
      private int m_index;
      private List<PdfMainObjectCollection.ObjectInfo> m_objectCollection = new List<PdfMainObjectCollection.ObjectInfo>();

      internal PdfMainObjectCollection()
      {
      }

      internal void Add(IPdfPrimitive element)
      {
        if (element == null)
          throw new ArgumentNullException(nameof (element));
        if (element is IPdfWrapper)
          element = (element as IPdfWrapper).Element;
        this.m_objectCollection.Add(new PdfMainObjectCollection.ObjectInfo(element));
        element.Position = this.m_index = this.m_objectCollection.Count - 1;
        element.Status = ObjectStatus.Registered;
      }

      internal void Add(IPdfPrimitive obj, PdfReference reference)
      {
        if (obj == null)
          throw new ArgumentNullException("element");
        if (reference == (PdfReference) null)
          throw new ArgumentNullException(nameof (reference));
        if (obj is IPdfWrapper)
          obj = (obj as IPdfWrapper).Element;
        this.m_objectCollection.Add(new PdfMainObjectCollection.ObjectInfo(obj, reference));
        obj.Position = reference.Position = this.m_objectCollection.Count - 1;
      }

      internal bool Contains(IPdfPrimitive element) => this.LookFor(element) >= 0;

      internal bool ContainsReference(PdfReference reference) => this.LookForReference(reference) >= 0;

      internal IPdfPrimitive GetObject(int index) => this.m_objectCollection[index].Object;

      internal int GetObjectIndex(PdfReference reference)
      {
        if (reference.Position != -1)
          return reference.Position;
        for (int index = this.m_objectCollection.Count - 1; index >= 0; --index)
        {
          PdfMainObjectCollection.ObjectInfo objectInfo = this.m_objectCollection[index];
          if (objectInfo.Reference != (PdfReference) null && objectInfo.Reference.ObjNum == reference.ObjNum && objectInfo.Reference.GenNum == reference.GenNum)
            return index;
        }
        return -1;
      }

      internal PdfReference GetReference(int index) => this.m_objectCollection[index].Reference;

      internal PdfReference GetReference(IPdfPrimitive obj, out bool isNew)
      {
        this.m_index = this.LookFor(obj);
        if (this.m_index < 0 || this.m_index > this.Count)
        {
          isNew = true;
          return (PdfReference) null;
        }
        isNew = false;
        return this.m_objectCollection[this.m_index].Reference;
      }

      internal int IndexOf(IPdfPrimitive element) => this.LookFor(element);

      private int LookFor(IPdfPrimitive obj)
      {
        if (obj.Position != -1)
          return obj.Position;
        for (int index = this.Count - 1; index >= 0; --index)
        {
          if (this.m_objectCollection[index].Object == obj)
            return index;
        }
        return -1;
      }

      private int LookForReference(PdfReference reference)
      {
        if (reference.Position != -1)
          return reference.Position;
        for (int index = this.Count - 1; index >= 0; --index)
        {
          if (this.m_objectCollection[index].Reference == reference)
            return index;
        }
        return -1;
      }

      internal void Remove(int index) => this.m_objectCollection.RemoveAt(index);

      internal void ReregisterReference(IPdfPrimitive oldObj, IPdfPrimitive newObj)
      {
        if (oldObj == null)
          throw new ArgumentNullException(nameof (oldObj));
        if (newObj == null)
          throw new ArgumentNullException(nameof (newObj));
        int oldObjIndex = this.IndexOf(oldObj);
        if (oldObjIndex < 0)
          throw new ArgumentException("Can't reregister an object.", nameof (oldObj));
        this.ReregisterReference(oldObjIndex, newObj);
      }

      internal void ReregisterReference(int oldObjIndex, IPdfPrimitive newObj)
      {
        if (newObj == null)
          throw new ArgumentNullException(nameof (newObj));
        if (oldObjIndex < 0 || oldObjIndex > this.Count)
          throw new ArgumentOutOfRangeException("oldObjectIndex");
        this.m_objectCollection[oldObjIndex].Object = newObj;
        newObj.Position = oldObjIndex;
      }

      internal bool TrySetReference(IPdfPrimitive obj, PdfReference reference, out bool found)
      {
        if (obj == null)
          throw new ArgumentNullException(nameof (obj));
        if (reference == (PdfReference) null)
          throw new ArgumentNullException(nameof (reference));
        bool flag1 = true;
        found = true;
        this.m_index = this.LookFor(obj);
        if (this.m_index < 0 || this.m_index >= this.m_objectCollection.Count)
        {
          bool flag2 = false;
          found = false;
          return flag2;
        }
        PdfMainObjectCollection.ObjectInfo objectInfo = this.m_objectCollection[this.m_index];
        if (objectInfo.Reference != (PdfReference) null)
          return false;
        objectInfo.SetReference(reference);
        return flag1;
      }

      internal int Count => this.m_objectCollection.Count;

      internal PdfMainObjectCollection.ObjectInfo this[int index]
      {
        get
        {
          if (index < 0 || index > this.m_objectCollection.Count)
            throw new ArgumentOutOfRangeException(nameof (index));
          return this.m_objectCollection[index];
        }
      }

      internal class ObjectInfo
      {
        private bool m_bModified;
        private IPdfPrimitive m_object;
        private PdfReference m_reference;

        internal ObjectInfo(IPdfPrimitive obj)
        {
          this.m_object = obj != null ? obj : throw new ArgumentNullException(nameof (obj));
          this.m_bModified = true;
        }

        internal ObjectInfo(IPdfPrimitive obj, PdfReference reference)
        {
          if (obj == null)
            throw new ArgumentNullException(nameof (obj));
          if (reference == (PdfReference) null)
            throw new ArgumentNullException(nameof (reference));
          this.m_object = obj;
          this.m_reference = reference;
        }

        public override bool Equals(object obj)
        {
          bool flag = false;
          if (obj != null)
          {
            IPdfPrimitive pdfPrimitive = obj as IPdfPrimitive;
            PdfMainObjectCollection.ObjectInfo objectInfo = obj as PdfMainObjectCollection.ObjectInfo;
            if (pdfPrimitive != null)
              return this.Object == pdfPrimitive;
            if (objectInfo != (object) null)
              flag = objectInfo.Object == this.Object;
          }
          return flag;
        }

        public static bool operator ==(PdfMainObjectCollection.ObjectInfo oi, object obj)
        {
          bool flag = false;
          if (oi != (object) null)
            flag = oi.Equals(obj);
          return flag;
        }

        public static bool operator !=(PdfMainObjectCollection.ObjectInfo oi, object obj) => oi != obj;

        public void SetModified() => this.m_bModified = true;

        internal void SetReference(PdfReference reference)
        {
          if (reference == (PdfReference) null)
            throw new ArgumentNullException(nameof (reference));
          this.m_reference = !(this.m_reference != (PdfReference) null) ? reference : throw new ArgumentException("The object has the reference bound to it.", nameof (reference));
        }

        public override string ToString()
        {
          return $"{(this.m_reference != (PdfReference) null ? this.m_reference.ToString() : string.Empty)} : {this.Object.GetType().Name}";
        }

        internal bool Modified
        {
          get
          {
            bool bModified = this.m_bModified;
            if (this.Object is IPdfChangable pdfChangable)
              bModified |= pdfChangable.Changed;
            return bModified;
          }
        }

        internal IPdfPrimitive Object
        {
          get => this.m_object;
          set
          {
            this.m_object = value != null ? value : throw new ArgumentNullException(nameof (Object));
          }
        }

        internal PdfReference Reference => this.m_reference;
      }
    }
}
