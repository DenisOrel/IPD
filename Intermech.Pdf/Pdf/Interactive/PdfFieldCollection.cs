// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfFieldCollection
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.IO;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf.Primitives;
using System;
using System.Collections;
using System.Collections.Generic;


namespace Syncfusion.Pdf.Interactive
{
    public class PdfFieldCollection : PdfCollection, IPdfWrapper
    {
      internal string c_exisingFieldException = "The field with '{0}' name already exists";
      private PdfArray m_array = new PdfArray();
      private Dictionary<string, int> m_fieldNames;

      public int Add(PdfField field)
      {
        return field != null ? this.DoAdd(field) : throw new ArgumentNullException(nameof (field));
      }

      internal int Add(PdfField field, PdfPageBase newPage)
      {
        PdfField field1 = (PdfField) null;
        if (field is PdfLoadedField)
          field1 = this.InsertLoadedField(field as PdfLoadedField, newPage);
        int num = this.DoAdd(field1);
        if (!(field is PdfLoadedField) || field1.ReadOnly == (field as PdfLoadedField).ReadOnly)
          return num;
        field1.ReadOnly = (field as PdfLoadedField).ReadOnly;
        return num;
      }

      public void Clear() => this.DoClear();

      public bool Contains(PdfField field) => this.List.Contains((object) field);

      protected virtual int DoAdd(PdfField field)
      {
        this.m_array.Add((IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) field));
        return this.List.Add((object) field);
      }

      protected virtual void DoClear()
      {
        this.m_array.Clear();
        this.List.Clear();
      }

      protected virtual void DoInsert(int index, PdfField field)
      {
        this.m_array.Insert(index, (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) field));
        this.List.Insert(index, (object) field);
      }

      protected virtual void DoRemove(PdfField field)
      {
        this.m_array.RemoveAt(this.List.IndexOf((object) field));
        this.List.Remove((object) field);
      }

      protected virtual void DoRemoveAt(int index)
      {
        this.m_array.RemoveAt(index);
        this.List.RemoveAt(index);
      }

      private int GetFieldIndex(string name)
      {
        int num = -1;
        if (this.m_fieldNames == null)
        {
          this.m_fieldNames = new Dictionary<string, int>();
          foreach (PdfField pdfField in (IEnumerable) this.List)
          {
            ++num;
            this.m_fieldNames.Add(pdfField.Name.Split('[')[0], num);
          }
        }
        int fieldIndex = -1;
        this.m_fieldNames.TryGetValue(name, out fieldIndex);
        return fieldIndex;
      }

      public int IndexOf(PdfField field)
      {
        return field != null ? this.List.IndexOf((object) field) : throw new ArgumentNullException(nameof (field));
      }

      public void Insert(int index, PdfField field)
      {
        if (field == null)
          throw new ArgumentNullException(nameof (field));
        this.DoInsert(index, field);
      }

      private PdfField InsertLoadedField(PdfLoadedField field, PdfPageBase newPage)
      {
        if (!(newPage as PdfPage).Section.ParentDocument.EnableMemoryOptimization)
        {
          PdfDictionary dictionary1 = field.Dictionary;
          PdfDictionary dictionary2 = field.Page.Dictionary;
          PdfDictionary dictionary3 = newPage.Dictionary;
          field = field.Clone(newPage) as PdfLoadedField;
          PdfArray array1 = field.CrossTable.GetObject(dictionary1["Kids"]) as PdfArray;
          PdfArray array2 = field.CrossTable.GetObject(dictionary2["Annots"]) as PdfArray;
          PdfArray newArray = field.CrossTable.GetObject(dictionary3["Annots"]) as PdfArray;
          if (array1 != null)
          {
            PdfArray kidsArray = new PdfArray(array1);
            field.Dictionary["Kids"] = (IPdfPrimitive) kidsArray;
            this.UpdateReferences(kidsArray, array2, newArray, (PdfField) field);
            field.Dictionary.Remove("P");
            return (PdfField) field;
          }
          PdfReferenceHolder element = new PdfReferenceHolder((IPdfPrimitive) dictionary1);
          int index = array2.IndexOf((IPdfPrimitive) element);
          if (index >= 0)
            field.Dictionary = PdfCrossTable.Dereference(newArray[index]) as PdfDictionary;
          return (PdfField) field;
        }
        int num1 = 0;
        if (newPage.Dictionary.ContainsKey("Annots"))
        {
          num1 = newPage.GetAnnots().Count;
        }
        else
        {
          PdfArray primitive = new PdfArray();
          newPage.Dictionary.SetProperty("Annots", (IPdfPrimitive) primitive);
        }
        PdfField wrapper1 = field.Clone(newPage);
        if (wrapper1 is PdfLoadedTextBoxField)
        {
          PdfDictionary dictionary = (wrapper1 as PdfLoadedTextBoxField).Dictionary;
          if (dictionary.ContainsKey("V"))
            (dictionary["V"] as PdfString).IsFormField = true;
        }
        bool flag = false;
        if (wrapper1 is PdfLoadedSignatureField)
          flag = true;
        if (field.CrossTable.GetObject(field.Dictionary["Kids"]) is PdfArray pdfArray && pdfArray.Count > 0 && !flag)
        {
          PdfCrossTable crossTable = (newPage as PdfPage).Section.ParentDocument.CrossTable;
          for (int index1 = 0; index1 < pdfArray.Count; ++index1)
          {
            PdfDictionary dictionary4 = PdfCrossTable.Dereference(pdfArray[index1]) as PdfDictionary;
            PdfDictionary pdfDictionary = new PdfDictionary(dictionary4);
            PdfName key1 = new PdfName("Parent");
            PdfName key2 = new PdfName("P");
            pdfDictionary.Remove(key1);
            pdfDictionary.Remove(key2);
            PdfDictionary dictionary5 = pdfDictionary.Clone(crossTable) as PdfDictionary;
            dictionary5[key1] = (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) wrapper1);
            PdfPageBase wrapper2;
            if (dictionary4.ContainsKey(key2))
            {
              if (PdfCrossTable.Dereference(dictionary4[key2]) is PdfDictionary key3 && field.CrossTable.PageCorrespondance.ContainsKey((IPdfPrimitive) key3) && field.CrossTable.PageCorrespondance[(IPdfPrimitive) key3] != null)
              {
                wrapper2 = field.CrossTable.PageCorrespondance[(IPdfPrimitive) key3] as PdfPageBase;
                if (wrapper2 == newPage)
                  dictionary5[key2] = (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) wrapper2);
                else
                  continue;
              }
              else
                continue;
            }
            else
            {
              dictionary5[key2] = (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) newPage);
              wrapper2 = newPage;
            }
            PdfLoadedFieldItem loadedItem = (wrapper1 as PdfLoadedField).CreateLoadedItem(dictionary5);
            if (wrapper2 != null)
              loadedItem.Page = wrapper2;
            PdfArray annots = newPage.GetAnnots();
            if (num1 < annots.Count)
            {
              for (int index2 = annots.Count - 1; index2 >= num1; --index2)
                annots.RemoveAt(index2);
            }
            annots.Add((IPdfPrimitive) new PdfReferenceHolder((IPdfPrimitive) dictionary5));
            ++num1;
          }
          return wrapper1;
        }
        PdfArray annots1 = newPage.GetAnnots();
        if (num1 < annots1.Count)
        {
          for (int index = annots1.Count - 1; index >= num1; --index)
            annots1.RemoveAt(index);
        }
        annots1.Add((IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) wrapper1));
        int num2 = num1 + 1;
        return wrapper1;
      }

      public void Remove(PdfField field)
      {
        if (field == null)
          throw new ArgumentNullException(nameof (field));
        this.DoRemove(field);
      }

      public void RemoveAt(int index) => this.DoRemoveAt(index);

      private void UpdateReferences(
        PdfArray kidsArray,
        PdfArray array,
        PdfArray newArray,
        PdfField field)
      {
        if (kidsArray == null)
          return;
        int index1 = 0;
        for (int count = kidsArray.Count; index1 < count; ++index1)
        {
          PdfReferenceHolder kids = kidsArray[index1] as PdfReferenceHolder;
          if (array != null)
          {
            int index2 = array.IndexOf((IPdfPrimitive) kids);
            if (index2 >= 0)
            {
              IPdfPrimitive element = newArray[index2];
              kidsArray.RemoveAt(index1);
              kidsArray.Insert(index1, element);
              PdfDictionary pdfDictionary = PdfCrossTable.Dereference(element) as PdfDictionary;
              if (pdfDictionary.ContainsKey("Parent"))
                pdfDictionary["Parent"] = (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) field);
            }
          }
        }
      }

      public PdfField this[string name]
      {
        get
        {
          if (name == null)
            throw new ArgumentNullException(nameof (name));
          int index = !(name == string.Empty) ? this.GetFieldIndex(name) : throw new ArgumentException("Field name can't be empty");
          return index != -1 ? this[index] : throw new ArgumentException("Incorrect field name");
        }
      }

      public virtual PdfField this[int index] => (PdfField) this.List[index];

      internal PdfArray Items => this.m_array;

      IPdfPrimitive IPdfWrapper.Element => (IPdfPrimitive) this.m_array;
    }
}
