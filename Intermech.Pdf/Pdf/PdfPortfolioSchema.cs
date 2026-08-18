// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.PdfPortfolioSchema
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using System.Collections.Generic;


namespace Syncfusion.Pdf
{
    public class PdfPortfolioSchema : IPdfWrapper
    {
      private string[] fieldkeys;
      private PdfDictionary m_dictionary;
      private Dictionary<string, PdfPortfolioSchemaField> m_fieldCollections;
      private PdfPortfolioSchemaField m_schemaField;

      public PdfPortfolioSchema()
      {
        this.m_dictionary = new PdfDictionary();
        this.m_fieldCollections = new Dictionary<string, PdfPortfolioSchemaField>();
        this.Initialize();
      }

      internal PdfPortfolioSchema(PdfDictionary schemaDictionary)
      {
        this.m_dictionary = new PdfDictionary();
        this.m_fieldCollections = new Dictionary<string, PdfPortfolioSchemaField>();
        this.m_dictionary = schemaDictionary;
        if (this.m_dictionary == null)
          return;
        foreach (KeyValuePair<PdfName, IPdfPrimitive> keyValuePair in this.m_dictionary.Items)
        {
          if (!(keyValuePair.Key.Value == "Type") && this.m_dictionary[keyValuePair.Key] is PdfDictionary schemaField)
          {
            this.m_schemaField = new PdfPortfolioSchemaField(schemaField);
            if (this.m_schemaField != null)
              this.m_fieldCollections.Add(this.m_schemaField.Name, this.m_schemaField);
          }
        }
      }

      public void AddSchemaField(PdfPortfolioSchemaField field)
      {
        if (this.m_fieldCollections.ContainsKey(field.Name) || this.m_dictionary.ContainsKey(field.Name))
          return;
        this.m_fieldCollections.Add(field.Name, field);
        this.m_dictionary.SetProperty(field.Name, (IPdfWrapper) field);
      }

      private void Initialize()
      {
        this.m_dictionary.SetProperty("Type", (IPdfPrimitive) new PdfName("CollectionSchema"));
      }

      public void RemoveField(string key)
      {
        if (!this.m_fieldCollections.ContainsKey(key) || !this.m_dictionary.ContainsKey(key))
          return;
        this.m_fieldCollections.Remove(key);
        this.m_dictionary.Remove(key);
      }

      public string[] FieldKeys
      {
        get
        {
          string[] array = new string[this.m_fieldCollections.Count];
          this.m_fieldCollections.Keys.CopyTo(array, 0);
          return array;
        }
      }

      IPdfPrimitive IPdfWrapper.Element => (IPdfPrimitive) this.m_dictionary;
    }
}
