// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfField
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf.Primitives;
using System;


namespace Syncfusion.Pdf.Interactive
{
    public abstract class PdfField : IPdfWrapper
    {
      private PdfDictionary m_dictionary;
      private bool m_disableAutoFormat;
      private bool m_export;
      private FieldFlags m_flags;
      private bool m_flatten;
      private PdfForm m_form;
      private string m_mappingName;
      private string m_name;
      private PdfPageBase m_page;
      private bool m_readOnly;
      private bool m_required;
      private int m_rotationAngle;
      private string m_toolTip;

      internal PdfField()
      {
        this.m_name = string.Empty;
        this.m_mappingName = string.Empty;
        this.m_export = true;
        this.m_toolTip = string.Empty;
        this.m_dictionary = new PdfDictionary();
        this.Initialize();
      }

      public PdfField(PdfPageBase page, string name)
      {
        this.m_name = string.Empty;
        this.m_mappingName = string.Empty;
        this.m_export = true;
        this.m_toolTip = string.Empty;
        this.m_dictionary = new PdfDictionary();
        this.Initialize();
        this.m_name = name;
        this.m_page = page;
        this.m_dictionary.SetProperty("T", (IPdfPrimitive) new PdfString(name));
      }

      internal virtual void ApplyName(string name)
      {
        this.m_name = name;
        this.Dictionary.SetProperty("T", (IPdfPrimitive) new PdfString(name));
      }

      internal virtual PdfField Clone(PdfPageBase page)
      {
        if (page == null)
          throw new ArgumentNullException(nameof (page));
        PdfField pdfField1 = (PdfField) null;
        if (!(page as PdfPage).Section.ParentDocument.EnableMemoryOptimization)
        {
          PdfField pdfField2 = this.MemberwiseClone() as PdfField;
          pdfField2.Dictionary = new PdfDictionary(this.Dictionary);
          pdfField2.m_page = page;
          pdfField2.Dictionary["P"] = (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) page);
          return pdfField2;
        }
        if (this is PdfLoadedField)
        {
          PdfDictionary pdfDictionary = new PdfDictionary(this.Dictionary);
          pdfDictionary.Remove("Parent");
          pdfDictionary.Remove("P");
          pdfDictionary.Remove("Kids");
          PdfDictionary dictionary = pdfDictionary.Clone((page as PdfPage).Section.ParentDocument.CrossTable) as PdfDictionary;
          if (this is PdfLoadedButtonField)
            pdfField1 = (this as PdfLoadedButtonField).Clone(dictionary, page as PdfPage);
          else if (this is PdfLoadedCheckBoxField)
            pdfField1 = (this as PdfLoadedCheckBoxField).Clone(dictionary, page as PdfPage);
          else if (this is PdfLoadedComboBoxField)
            pdfField1 = (this as PdfLoadedComboBoxField).Clone(dictionary, page as PdfPage);
          else if (this is PdfLoadedListBoxField)
            pdfField1 = (this as PdfLoadedListBoxField).Clone(dictionary, page as PdfPage);
          else if (this is PdfLoadedRadioButtonListField)
            pdfField1 = (this as PdfLoadedRadioButtonListField).Clone(dictionary, page as PdfPage);
          else if (this is PdfLoadedSignatureField)
            pdfField1 = (this as PdfLoadedSignatureField).Clone(dictionary, page as PdfPage);
          else if (this is PdfLoadedTextBoxField)
            pdfField1 = (this as PdfLoadedTextBoxField).Clone(dictionary, page as PdfPage);
          else if (!dictionary.ContainsKey("FT") && this is PdfLoadedStyledField)
            pdfField1 = (this as PdfLoadedStyledField).Clone(dictionary, page as PdfPage);
          PdfLoadedField pdfLoadedField = this as PdfLoadedField;
          pdfField1.DisableAutoFormat = pdfLoadedField.DisableAutoFormat;
          pdfField1.Export = pdfLoadedField.Export;
          pdfField1.Flags = pdfLoadedField.Flags;
          pdfField1.Flatten = pdfLoadedField.Flatten;
          if (pdfField1.MappingName != null)
            pdfField1.MappingName = pdfLoadedField.MappingName;
          pdfField1.Required = pdfLoadedField.Required;
          pdfField1.RotationAngle = pdfLoadedField.RotationAngle;
          if (pdfLoadedField.ToolTip != null)
            pdfField1.ToolTip = pdfLoadedField.ToolTip;
          if (pdfLoadedField is PdfLoadedTextBoxField loadedTextBoxField && loadedTextBoxField.m_font != null)
            (pdfField1 as PdfLoadedTextBoxField).Font = loadedTextBoxField.m_font;
        }
        return pdfField1;
      }

      protected virtual void DefineDefaultAppearance()
      {
      }

      private void Dictionary_BeginSave(object sender, SavePdfPrimitiveEventArgs ars) => this.Save();

      internal abstract void Draw();

      protected virtual void Initialize()
      {
        this.m_dictionary.BeginSave += new SavePdfPrimitiveEventHandler(this.Dictionary_BeginSave);
      }

      internal virtual void Save()
      {
        if (!(this.m_readOnly | (this.Form != null && this.Form.ReadOnly)))
          return;
        this.Flags |= FieldFlags.ReadOnly;
      }

      internal void SetForm(PdfForm form)
      {
        this.m_form = form;
        this.DefineDefaultAppearance();
      }

      internal PdfDictionary Dictionary
      {
        get => this.m_dictionary;
        set => this.m_dictionary = value;
      }

      public bool DisableAutoFormat
      {
        get
        {
          bool disableAutoFormat = this.m_disableAutoFormat;
          if (this.Form != null)
            disableAutoFormat |= this.Form.DisableAutoFormat;
          return disableAutoFormat;
        }
        set => this.m_disableAutoFormat = value;
      }

      public virtual bool Export
      {
        get => this.m_export;
        set
        {
          if (this.m_export == value)
            return;
          this.m_export = value;
          if (this.m_export)
            this.Flags -= FieldFlags.NoExport;
          else
            this.Flags |= FieldFlags.NoExport;
        }
      }

      internal virtual FieldFlags Flags
      {
        get => this.m_flags;
        set
        {
          if (this.m_flags == value)
            return;
          this.m_flags = value;
          this.m_dictionary.SetNumber("Ff", (int) this.m_flags);
        }
      }

      public bool Flatten
      {
        get
        {
          bool flatten = this.m_flatten;
          if (this.Form != null)
            flatten |= this.Form.Flatten;
          return flatten;
        }
        set => this.m_flatten = value;
      }

      public virtual PdfForm Form => this.m_form;

      public virtual string MappingName
      {
        get => this.m_mappingName;
        set
        {
          if (value == null)
            throw new ArgumentNullException(nameof (MappingName));
          if (!(this.m_mappingName != value))
            return;
          this.m_mappingName = value;
          this.m_dictionary.SetString("TM", this.m_mappingName);
        }
      }

      public virtual string Name => this.m_name;

      public virtual PdfPageBase Page
      {
        get => this.m_page;
        internal set => this.m_page = value;
      }

      public virtual bool ReadOnly
      {
        get => this.m_readOnly;
        set => this.m_readOnly = value;
      }

      public virtual bool Required
      {
        get => this.m_required;
        set
        {
          if (this.m_required == value)
            return;
          this.m_required = value;
          if (this.m_required)
            this.Flags |= FieldFlags.Required;
          else
            this.Flags -= FieldFlags.Required;
        }
      }

      internal int RotationAngle
      {
        get => this.m_rotationAngle;
        set => this.m_rotationAngle = value;
      }

      IPdfPrimitive IPdfWrapper.Element => (IPdfPrimitive) this.m_dictionary;

      public virtual string ToolTip
      {
        get => this.m_toolTip;
        set
        {
          if (value == null)
            throw new ArgumentNullException(nameof (ToolTip));
          if (!(this.m_toolTip != value))
            return;
          this.m_toolTip = value;
          this.m_dictionary.SetString("TU", this.m_toolTip);
        }
      }
    }
}
