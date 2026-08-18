// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfForm
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf.Primitives;
using Syncfusion.Pdf.Security;
using System;
using System.Collections.Generic;


namespace Syncfusion.Pdf.Interactive
{
    public class PdfForm : IPdfWrapper
    {
      private bool m_changeName = true;
      private PdfDictionary m_dictionary = new PdfDictionary();
      private bool m_disableAutoFormat;
      private List<string> m_fieldName = new List<string>();
      private List<string> m_fieldNames = new List<string>();
      private PdfFormFieldCollection m_fields = new PdfFormFieldCollection();
      private bool m_flatten;
      private bool m_isXFA;
      private bool m_needAppearances = true;
      internal System.Collections.Generic.Dictionary<PdfDictionary, PdfPageBase> m_pageMap = new System.Collections.Generic.Dictionary<PdfDictionary, PdfPageBase>();
      private bool m_readOnly;
      private PdfResources m_resources;
      private bool m_setAppearanceDictionary;
      private SignatureFlags m_signatureFlags;

      public PdfForm()
      {
        this.m_fields.Form = this;
        this.m_dictionary.SetProperty(nameof (Fields), (IPdfWrapper) this.m_fields);
        this.m_dictionary.BeginSave += new SavePdfPrimitiveEventHandler(this.Dictionary_BeginSave);
        this.m_setAppearanceDictionary = true;
      }

      private void CheckFlatten()
      {
        for (int index = 0; index < this.m_fields.Count; ++index)
        {
          PdfField field = this.m_fields[index];
          if (field.DisableAutoFormat && field.Dictionary.ContainsKey("AA"))
            field.Dictionary.Remove("AA");
          if (field.Flatten)
          {
            field.Draw();
            this.m_fields.Remove(field);
            this.DeleteFromPages(field);
            this.DeleteAnnotation(field);
            --index;
          }
          else if (field is PdfLoadedField)
          {
            if (field is PdfLoadedTextBoxField && (field.Dictionary.ContainsKey("AP") || (field as PdfLoadedTextBoxField).Items.Count > 0))
            {
              if (this.IsXFA && field.Dictionary.ContainsKey("MK"))
              {
                PdfDictionary pdfDictionary = field.Dictionary["MK"] as PdfDictionary;
                if (pdfDictionary.ContainsKey("BG"))
                  pdfDictionary.Remove("BG");
              }
              if (!this.IsXFA)
                (field as PdfLoadedField).BeginSave();
            }
            if (field is PdfLoadedField && this.SignatureFlags == SignatureFlags.None && field is PdfLoadedSignatureField)
              field.Save();
            else if (field is PdfLoadedField && !this.ReadOnly)
              (field as PdfLoadedField).BeginSave();
            else
              field.Save();
          }
        }
      }

      internal virtual void Clear()
      {
        if (this.m_fields != null)
        {
          this.m_fields.Clear();
          this.m_fields = (PdfFormFieldCollection) null;
        }
        if (this.m_dictionary != null)
        {
          this.m_dictionary.Clear();
          this.m_dictionary = (PdfDictionary) null;
        }
        this.m_fieldName.Clear();
        this.m_fieldNames.Clear();
        this.m_pageMap.Clear();
      }

      internal void DeleteAnnotation(PdfField field)
      {
        PdfDictionary dictionary = field.Dictionary;
        if (!dictionary.ContainsKey("Kids"))
          return;
        PdfArray primitive = dictionary["Kids"] as PdfArray;
        primitive.Clear();
        dictionary.SetProperty("Kids", (IPdfPrimitive) primitive);
      }

      internal void DeleteFromPages(PdfField field)
      {
        PdfDictionary dictionary = field.Dictionary;
        if (dictionary.ContainsKey("Kids"))
        {
          PdfArray pdfArray = dictionary["Kids"] as PdfArray;
          int index = 0;
          for (int count = pdfArray.Count; index < count; ++index)
          {
            PdfReferenceHolder element = pdfArray[index] as PdfReferenceHolder;
            PdfDictionary pdfDictionary = ((element.Object as PdfDictionary)["P"] as PdfReferenceHolder).Object as PdfDictionary;
            if (pdfDictionary.ContainsKey("Annots"))
            {
              if ((object) (pdfDictionary["Annots"] as PdfReferenceHolder) != null)
              {
                PdfArray primitive = (pdfDictionary["Annots"] as PdfReferenceHolder).Object as PdfArray;
                primitive.Remove((IPdfPrimitive) element);
                primitive.MarkChanged();
                pdfDictionary.SetProperty("Annots", (IPdfPrimitive) primitive);
              }
              else if (pdfDictionary["Annots"] is PdfArray)
              {
                PdfArray primitive = pdfDictionary["Annots"] as PdfArray;
                primitive.Remove((IPdfPrimitive) element);
                primitive.MarkChanged();
                pdfDictionary.SetProperty("Annots", (IPdfPrimitive) primitive);
              }
            }
          }
        }
        else
        {
          PdfDictionary pdfDictionary = (!dictionary.ContainsKey("P") ? new PdfReferenceHolder((IPdfPrimitive) field.Page.Dictionary) : dictionary["P"] as PdfReferenceHolder).Object as PdfDictionary;
          if (!pdfDictionary.ContainsKey("Annots"))
            return;
          PdfArray primitive = pdfDictionary["Annots"] as PdfArray;
          primitive.Remove((IPdfPrimitive) new PdfReferenceHolder((IPdfPrimitive) dictionary));
          primitive.MarkChanged();
          pdfDictionary.SetProperty("Annots", (IPdfPrimitive) primitive);
        }
      }

      internal virtual void Dictionary_BeginSave(object sender, SavePdfPrimitiveEventArgs ars)
      {
        if (this.m_signatureFlags != SignatureFlags.None)
          this.NeedAppearances = false;
        this.CheckFlatten();
        if (this.m_fields.Count <= 0 || !this.SetAppearanceDictionary)
          return;
        this.m_dictionary.SetBoolean("NeedAppearances", this.m_needAppearances);
      }

      internal virtual string GetCorrectName(string name)
      {
        string correctName = name;
        this.m_fieldNames.Add(correctName);
        if (this.m_fieldName.Contains(name))
        {
          int num1 = this.m_fieldName.IndexOf(name);
          int index = this.m_fieldName.LastIndexOf(name);
          int num2 = index;
          if (num1 != num2)
          {
            string[] strArray = Guid.NewGuid().ToString().Split('-');
            correctName = $"{name}_{strArray[4]}";
            this.m_fieldName.RemoveAt(index);
            this.m_fieldName.Add(correctName);
          }
        }
        return correctName;
      }

      public void SetDefaultAppearance(bool applyDefault)
      {
        this.NeedAppearances = applyDefault;
        this.SetAppearanceDictionary = true;
      }

      internal virtual PdfDictionary Dictionary
      {
        get => this.m_dictionary;
        set
        {
          this.m_dictionary = value != null ? value : throw new ArgumentNullException(nameof (Dictionary));
        }
      }

      public bool DisableAutoFormat
      {
        get => this.m_disableAutoFormat;
        set => this.m_disableAutoFormat = value;
      }

      public bool FieldAutoNaming
      {
        get => this.m_changeName;
        set => this.m_changeName = value;
      }

      internal List<string> FieldNames => this.m_fieldName;

      public PdfFormFieldCollection Fields => this.m_fields;

      public bool Flatten
      {
        get => this.m_flatten;
        set => this.m_flatten = value;
      }

      internal bool IsXFA
      {
        get => this.m_isXFA;
        set => this.m_isXFA = true;
      }

      internal virtual bool NeedAppearances
      {
        get => this.m_needAppearances;
        set
        {
          if (this.m_needAppearances == value)
            return;
          this.m_needAppearances = value;
        }
      }

      public virtual bool ReadOnly
      {
        get => this.m_readOnly;
        set => this.m_readOnly = value;
      }

      internal virtual PdfResources Resources
      {
        get
        {
          if (this.m_resources == null)
          {
            this.m_resources = new PdfResources();
            this.m_dictionary.SetProperty("DR", (IPdfPrimitive) this.m_resources);
          }
          return this.m_resources;
        }
        set => this.m_resources = value != null ? value : throw new ArgumentNullException("resources");
      }

      internal bool SetAppearanceDictionary
      {
        get => this.m_setAppearanceDictionary;
        set => this.m_setAppearanceDictionary = value;
      }

      internal virtual SignatureFlags SignatureFlags
      {
        get => this.m_signatureFlags;
        set
        {
          if (this.m_signatureFlags == value)
            return;
          this.m_signatureFlags = value;
          this.m_dictionary.SetNumber("SigFlags", (int) this.m_signatureFlags);
        }
      }

      IPdfPrimitive IPdfWrapper.Element => (IPdfPrimitive) this.m_dictionary;
    }
}
