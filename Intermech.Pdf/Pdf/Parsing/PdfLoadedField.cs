// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Parsing.PdfLoadedField
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Interactive;
using Syncfusion.Pdf.IO;
using Syncfusion.Pdf.Primitives;
using System;
using System.IO;
using System.Text;
using System.Xml;


namespace Syncfusion.Pdf.Parsing
{
    public abstract class PdfLoadedField : PdfField
    {
      private bool m_Changed;
      private PdfCrossTable m_crossTable;
      private int m_defaultIndex;
      private PdfLoadedForm m_form;
      private string m_name;
      private PdfPageBase m_page;
      public int ObjectID;

      internal event PdfLoadedField.BeforeNameChangesEventHandler BeforeNameChanges;

      internal PdfLoadedField(PdfDictionary dictionary, PdfCrossTable crossTable)
      {
        if (dictionary == null)
          throw new ArgumentNullException(nameof (dictionary));
        if (crossTable == null)
          throw new ArgumentNullException(nameof (crossTable));
        this.Dictionary = dictionary;
        this.m_crossTable = crossTable;
      }

      internal override void ApplyName(string name) => this.SetName(name);

      internal virtual void BeginSave()
      {
      }

      internal abstract PdfLoadedFieldItem CreateLoadedItem(PdfDictionary dictionary);

      internal abstract override void Draw();

      internal void ExportField(XmlTextWriter textWriter)
      {
        switch ((PdfLoadedField.GetValue(this.Dictionary, this.CrossTable, "FT", true) as PdfName).Value)
        {
          case "Tx":
            if (!(PdfLoadedField.GetValue(this.Dictionary, this.CrossTable, "V", true) is PdfString pdfString))
              break;
            textWriter.WriteStartElement(this.Name, "");
            textWriter.WriteString(pdfString.Value);
            textWriter.WriteEndElement();
            break;
          case "Ch":
            PdfName pdfName1 = PdfLoadedField.GetValue(this.Dictionary, this.CrossTable, "V", true) as PdfName;
            if (!(pdfName1 != (PdfName) null))
              break;
            textWriter.WriteStartElement(this.Name, "");
            textWriter.WriteString(pdfName1.Value);
            textWriter.WriteEndElement();
            break;
          case "Btn":
            PdfName pdfName2 = PdfLoadedField.GetValue(this.Dictionary, this.CrossTable, "V", true) as PdfName;
            if (pdfName2 != (PdfName) null)
            {
              textWriter.WriteStartElement(this.Name, "");
              textWriter.WriteString(pdfName2.Value);
              textWriter.WriteEndElement();
              break;
            }
            PdfDictionary widgetAnnotation = this.GetWidgetAnnotation(this.Dictionary, this.CrossTable);
            if ((object) (widgetAnnotation["AS"] as PdfName) == null)
              break;
            textWriter.WriteStartElement(this.Name, "");
            textWriter.WriteString((widgetAnnotation["AS"] as PdfName).Value);
            textWriter.WriteEndElement();
            break;
        }
      }

      internal void ExportField(Stream stream, ref int objectid)
      {
        bool flag = false;
        pdfArray = (PdfArray) null;
        if (this.Dictionary.ContainsKey("Kids") && this.CrossTable.GetObject(this.Dictionary["Kids"]) is PdfArray pdfArray)
        {
          for (int index = 0; index < pdfArray.Count; ++index)
            flag = flag || pdfArray[index] is PdfLoadedField;
        }
        PdfName pdfName1 = PdfLoadedField.GetValue(this.Dictionary, this.CrossTable, "FT", true) as PdfName;
        string text1 = "";
        switch (pdfName1.Value)
        {
          case "Tx":
            if (PdfLoadedField.GetValue(this.Dictionary, this.CrossTable, "V", true) is PdfString pdfString)
            {
              text1 = pdfString.Value;
              break;
            }
            break;
          case "Ch":
            PdfName pdfName2 = PdfLoadedField.GetValue(this.Dictionary, this.CrossTable, "V", true) as PdfName;
            if (pdfName2 != (PdfName) null)
            {
              text1 = pdfName2.Value;
              break;
            }
            break;
          case "Btn":
            PdfName pdfName3 = PdfLoadedField.GetValue(this.Dictionary, this.CrossTable, "V", true) as PdfName;
            if (pdfName3 != (PdfName) null)
            {
              text1 = pdfName3.Value;
              break;
            }
            PdfDictionary widgetAnnotation = this.GetWidgetAnnotation(this.Dictionary, this.CrossTable);
            if ((object) (widgetAnnotation["AS"] as PdfName) != null)
            {
              text1 = (widgetAnnotation["AS"] as PdfName).Value;
              break;
            }
            break;
        }
        if (!(!PdfLoadedField.validateString(text1) | flag))
          return;
        if (flag)
        {
          for (int index = 0; index < pdfArray.Count; ++index)
          {
            if (pdfArray[index] is PdfLoadedField pdfLoadedField && pdfLoadedField.Export)
              pdfLoadedField.ExportField(stream, ref objectid);
          }
          this.ObjectID = objectid;
          ++objectid;
          StringBuilder stringBuilder = new StringBuilder();
          byte[] bytes1 = Encoding.GetEncoding("windows-1252").GetBytes(new PdfString(text1)
          {
            Encode = PdfString.ForceEncoding.ASCII
          }.Value);
          stringBuilder.AppendFormat("{0} 0 obj<</T <{1}> /Kids [", (object) this.ObjectID, (object) PdfString.BytesToHex(bytes1));
          for (int index = 0; index < pdfArray.Count; ++index)
          {
            if (pdfArray[index] is PdfLoadedField pdfLoadedField && pdfLoadedField.Export && pdfLoadedField.ObjectID != 0)
              stringBuilder.AppendFormat("{0} 0 R ", (object) pdfLoadedField.ObjectID);
          }
          stringBuilder.Append("]>>endobj\n");
          byte[] bytes2 = Encoding.GetEncoding("windows-1252").GetBytes(new PdfString(stringBuilder.ToString())
          {
            Encode = PdfString.ForceEncoding.ASCII
          }.Value);
          stream.Write(bytes2, 0, bytes2.Length);
        }
        else
        {
          this.ObjectID = objectid;
          ++objectid;
          string str;
          if (this.GetType().Name == "PdfLoadedCheckBoxField" || this.GetType().Name == "PdfLoadedRadioButtonListField")
            str = "/" + text1;
          else
            str = $"<{PdfString.BytesToHex(Encoding.GetEncoding("windows-1252").GetBytes(new PdfString(text1)
            {
              Encode = PdfString.ForceEncoding.ASCII
            }.Value))}>";
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.AppendFormat("{0} 0 obj<</T <{1}> /V {2} >>endobj\n", (object) this.ObjectID, (object) PdfString.BytesToHex(Encoding.GetEncoding("windows-1252").GetBytes(new PdfString(this.Name)
          {
            Encode = PdfString.ForceEncoding.ASCII
          }.Value)), (object) str);
          byte[] bytes = Encoding.GetEncoding("windows-1252").GetBytes(new PdfString(stringBuilder.ToString())
          {
            Encode = PdfString.ForceEncoding.ASCII
          }.Value);
          stream.Write(bytes, 0, bytes.Length);
        }
      }

      internal string GetFieldName()
      {
        string fieldName = (string) null;
        PdfString pdfString = (PdfString) null;
        if (!this.Dictionary.ContainsKey("Parent"))
        {
          pdfString = PdfLoadedField.GetValue(this.Dictionary, this.m_crossTable, "T", false) as PdfString;
        }
        else
        {
          PdfDictionary dictionary;
          for (dictionary = this.m_crossTable.GetObject(this.Dictionary["Parent"]) as PdfDictionary; dictionary.ContainsKey("Parent"); dictionary = this.m_crossTable.GetObject(dictionary["Parent"]) as PdfDictionary)
          {
            if (dictionary.ContainsKey("T"))
              fieldName = fieldName == null ? (PdfLoadedField.GetValue(dictionary, this.m_crossTable, "T", false) as PdfString).Value : $"{(PdfLoadedField.GetValue(dictionary, this.m_crossTable, "T", false) as PdfString).Value}.{fieldName}";
          }
          if (dictionary.ContainsKey("T"))
            fieldName = (fieldName == null ? (PdfLoadedField.GetValue(dictionary, this.m_crossTable, "T", false) as PdfString).Value : $"{(PdfLoadedField.GetValue(dictionary, this.m_crossTable, "T", false) as PdfString).Value}.{fieldName}") + $".{(PdfLoadedField.GetValue(this.Dictionary, this.m_crossTable, "T", false) as PdfString).Value}";
          else if (this.Dictionary.ContainsKey("T"))
            pdfString = PdfLoadedField.GetValue(this.Dictionary, this.m_crossTable, "T", false) as PdfString;
        }
        if (pdfString != null)
          fieldName = pdfString.Value;
        return fieldName;
      }

      internal PdfHighlightMode GetHighLight(PdfDictionary dictionary, PdfCrossTable crossTable)
      {
        PdfName pdfName = (PdfName) null;
        if (dictionary.ContainsKey("Kids"))
        {
          PdfDictionary widgetAnnotation = this.GetWidgetAnnotation(dictionary, crossTable);
          if (widgetAnnotation.ContainsKey("H"))
            pdfName = crossTable.GetObject(widgetAnnotation["H"]) as PdfName;
        }
        else if (dictionary.ContainsKey("H"))
          pdfName = crossTable.GetObject(dictionary["H"]) as PdfName;
        PdfHighlightMode highLight = PdfHighlightMode.NoHighlighting;
        if (!(pdfName == (PdfName) null))
        {
          string str;
          switch (str = pdfName.Value)
          {
            case null:
              break;
            case "I":
              return PdfHighlightMode.Invert;
            default:
              if (!(str != "O"))
                return PdfHighlightMode.Outline;
              return str != "P" ? highLight : PdfHighlightMode.Push;
          }
        }
        return highLight;
      }

      private PdfPageBase GetLoadedPage()
      {
        PdfPageBase page1 = base.Page;
        if (page1 == null)
        {
          PdfLoadedDocument document = this.CrossTable.Document as PdfLoadedDocument;
          PdfDictionary pdfDictionary = this.GetWidgetAnnotation(this.Dictionary, this.CrossTable) ?? this.Dictionary;
          if (pdfDictionary.ContainsKey("P"))
          {
            if (this.CrossTable.GetObject(pdfDictionary["P"]) is PdfDictionary dic)
              page1 = document.Pages.GetPage(dic);
            return page1;
          }
          PdfReference reference = this.CrossTable.GetReference((IPdfPrimitive) pdfDictionary);
          foreach (PdfLoadedPage page2 in document.Pages)
          {
            PdfArray annots = page2.GetAnnots();
            if (annots != null)
            {
              for (int index = 0; index < annots.Count; ++index)
              {
                if ((annots[index] as PdfReferenceHolder).Reference == reference)
                  return (PdfPageBase) page2;
              }
            }
          }
        }
        return page1;
      }

      internal static IPdfPrimitive GetValue(
        PdfDictionary dictionary,
        PdfCrossTable crossTable,
        string value,
        bool inheritable)
      {
        IPdfPrimitive pdfPrimitive = (IPdfPrimitive) null;
        if (dictionary.ContainsKey(value))
          return crossTable.GetObject(dictionary[value]);
        if (inheritable)
          pdfPrimitive = PdfLoadedField.SearchInParents(dictionary, crossTable, value);
        return pdfPrimitive;
      }

      internal PdfDictionary GetWidgetAnnotation(PdfDictionary dictionary, PdfCrossTable crossTable)
      {
        PdfDictionary widgetAnnotation = (PdfDictionary) null;
        if (dictionary.ContainsKey("Kids"))
        {
          PdfArray pdfArray = crossTable.GetObject(dictionary["Kids"]) as PdfArray;
          if (pdfArray.Count > 0)
          {
            PdfReference reference = crossTable.GetReference(pdfArray[this.m_defaultIndex]);
            widgetAnnotation = crossTable.GetObject((IPdfPrimitive) reference) as PdfDictionary;
          }
        }
        if (dictionary.ContainsKey("Subtype") && (this.CrossTable.GetObject(dictionary["Subtype"]) as PdfName).Value == "Widget")
          widgetAnnotation = dictionary;
        if (widgetAnnotation == null)
          widgetAnnotation = dictionary;
        return widgetAnnotation;
      }

      internal void ImportFieldValue(string FieldValue)
      {
        switch ((PdfLoadedField.GetValue(this.Dictionary, this.CrossTable, "FT", true) as PdfName).Value)
        {
          case "Tx":
            if (FieldValue == null)
              break;
            (this as PdfLoadedTextBoxField).Text = FieldValue;
            break;
          case "Ch":
            if (this.GetType().Name == "PdfLoadedListBoxField")
            {
              (this as PdfLoadedListBoxField).SelectedValue = new string[1]
              {
                FieldValue
              };
              break;
            }
            if (!(this.GetType().Name == "PdfLoadedComboBoxField"))
              break;
            (this as PdfLoadedComboBoxField).SelectedValue = FieldValue;
            break;
          case "Btn":
            if (this is PdfLoadedCheckBoxField loadedCheckBoxField)
            {
              if (FieldValue.ToUpper() == "off".ToUpper() || FieldValue.ToUpper() == "no".ToUpper())
              {
                loadedCheckBoxField.Checked = false;
                break;
              }
              loadedCheckBoxField.Checked = true;
              break;
            }
            if (!(this.GetType().Name == "PdfLoadedRadioButtonListField"))
              break;
            (this as PdfLoadedRadioButtonListField).SelectedValue = FieldValue;
            break;
        }
      }

      internal static IPdfPrimitive SearchInParents(
        PdfDictionary dictionary,
        PdfCrossTable crossTable,
        string value)
      {
        IPdfPrimitive pdfPrimitive = (IPdfPrimitive) null;
        PdfDictionary pdfDictionary = dictionary;
        while (pdfPrimitive == null && pdfDictionary != null)
        {
          if (pdfDictionary.ContainsKey(value))
            pdfPrimitive = crossTable.GetObject(pdfDictionary[value]);
          else
            pdfDictionary = !pdfDictionary.ContainsKey("Parent") ? (PdfDictionary) null : crossTable.GetObject(pdfDictionary["Parent"]) as PdfDictionary;
        }
        return pdfPrimitive;
      }

      public void SetName(string name)
      {
        if (name == null)
          throw new ArgumentNullException(nameof (name));
        if (name == string.Empty)
          throw new ArgumentException("The name can't be empty");
        if (this.Name == null || !(this.Name != name))
          return;
        string[] strArray = this.Name.Split('.');
        if (!(strArray[strArray.Length - 1] != name))
          return;
        PdfString primitive = new PdfString(name);
        if (this.m_form != null)
          this.BeforeNameChanges(name);
        this.Dictionary.SetProperty("T", (IPdfPrimitive) primitive);
        this.Changed = true;
      }

      internal static bool validateString(string text1) => text1 == null || text1.Length == 0;

      internal string ActualFieldName
      {
        get
        {
          string actualFieldName = (string) null;
          if (PdfLoadedField.GetValue(this.Dictionary, this.m_crossTable, "T", false) is PdfString pdfString)
            actualFieldName = pdfString.Value;
          return actualFieldName;
        }
      }

      internal bool Changed
      {
        get => this.m_Changed;
        set => this.m_Changed = value;
      }

      internal PdfCrossTable CrossTable
      {
        get => this.m_crossTable;
        set
        {
          if (value == null)
            throw new ArgumentNullException(nameof (CrossTable));
          if (this.m_crossTable == value)
            return;
          this.m_crossTable = value;
        }
      }

      internal int DefaultIndex
      {
        get => this.m_defaultIndex;
        set => this.m_defaultIndex = value;
      }

      public override bool Export
      {
        get => (FieldFlags.NoExport & this.Flags) == FieldFlags.Default;
        set
        {
          if (value)
            this.Flags &= ~FieldFlags.NoExport;
          else
            this.Flags |= FieldFlags.NoExport;
        }
      }

      internal override FieldFlags Flags
      {
        get
        {
          FieldFlags flags = base.Flags;
          if (flags == FieldFlags.Default && PdfLoadedField.GetValue(this.Dictionary, this.m_crossTable, "Ff", true) is PdfNumber pdfNumber)
            flags = (FieldFlags) pdfNumber.IntValue;
          return flags;
        }
        set
        {
          base.Flags = value;
          this.Changed = true;
        }
      }

      public new PdfForm Form => this.m_form != null ? (PdfForm) this.m_form : base.Form;

      public override string MappingName
      {
        get
        {
          string mappingName = base.MappingName;
          if ((mappingName == null || mappingName != null && mappingName.Length == 0) && PdfLoadedField.GetValue(this.Dictionary, this.m_crossTable, "TM", false) is PdfString pdfString)
            mappingName = pdfString.Value;
          return mappingName;
        }
        set
        {
          base.MappingName = value;
          this.Changed = true;
        }
      }

      public override string Name
      {
        get
        {
          this.m_name = this.GetFieldName();
          return this.m_name;
        }
      }

      public override PdfPageBase Page
      {
        get
        {
          if (this.m_page == null)
            this.m_page = this.GetLoadedPage();
          else if (this.m_page != null && this.m_page is PdfLoadedPage && (this.Changed || this.Form.Flatten || this.Flatten))
            this.m_page = this.GetLoadedPage();
          return this.m_page;
        }
        internal set => this.m_page = value;
      }

      internal PdfDictionary Parent
      {
        get
        {
          PdfDictionary parent = (PdfDictionary) null;
          if (this.Dictionary.ContainsKey(nameof (Parent)))
            parent = this.m_crossTable.GetObject(this.Dictionary[nameof (Parent)]) as PdfDictionary;
          return parent;
        }
      }

      public override bool ReadOnly
      {
        get => (FieldFlags.ReadOnly & this.Flags) != FieldFlags.Default || this.Form.ReadOnly;
        set
        {
          if (value || this.Form.ReadOnly)
            this.Flags |= FieldFlags.ReadOnly;
          else
            this.Flags = FieldFlags.Edit;
        }
      }

      public override bool Required
      {
        get => (FieldFlags.Required & this.Flags) != 0;
        set
        {
          if (value)
            this.Flags |= FieldFlags.Required;
          else
            this.Flags &= ~FieldFlags.Required;
        }
      }

      public override string ToolTip
      {
        get
        {
          PdfString pdfString = PdfLoadedField.GetValue(this.Dictionary, this.m_crossTable, "TU", false) as PdfString;
          string toolTip = (string) null;
          if (pdfString != null)
            toolTip = pdfString.Value;
          return toolTip;
        }
        set
        {
          base.ToolTip = value;
          this.Changed = true;
        }
      }

      internal delegate void BeforeNameChangesEventHandler(string name);
    }
}
