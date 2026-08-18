// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.PdfPortfolioAttributes
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using System.Collections.Generic;


namespace Syncfusion.Pdf
{
    public class PdfPortfolioAttributes : IPdfWrapper
    {
      private string[] m_attributeKeys;
      private Dictionary<string, string> m_attributes;
      private PdfDictionary m_dictionary;

      public PdfPortfolioAttributes()
      {
        this.m_attributes = new Dictionary<string, string>();
        this.Initialize();
      }

      internal PdfPortfolioAttributes(PdfDictionary dictionary)
      {
        this.m_attributes = new Dictionary<string, string>();
        if (this.m_dictionary != null)
          return;
        this.m_dictionary = dictionary;
        foreach (KeyValuePair<PdfName, IPdfPrimitive> keyValuePair in dictionary.Items)
        {
          if (!(keyValuePair.Key.Value == "Type"))
            this.m_attributes.Add(keyValuePair.Key.Value, (keyValuePair.Value as PdfString).Value);
        }
      }

      public void AddAttributes(string key, string value)
      {
        if (this.m_attributes.ContainsKey(key) || this.m_dictionary.ContainsKey(key))
          return;
        this.m_attributes.Add(key, value);
        this.m_dictionary.SetProperty(key, (IPdfPrimitive) new PdfString(value));
      }

      private void Initialize()
      {
        this.m_dictionary = new PdfDictionary();
        this.m_dictionary.SetProperty("Type", (IPdfPrimitive) new PdfName("CollectionItem"));
      }

      public void RemoveAttributes(string key)
      {
        if (!this.m_attributes.ContainsKey(key) || !this.m_dictionary.ContainsKey(key))
          return;
        this.m_attributes.Remove(key);
        this.m_dictionary.Remove(key);
      }

      public string[] AttributesKey
      {
        get
        {
          string[] array = new string[this.m_attributes.Count];
          this.m_attributes.Keys.CopyTo(array, 0);
          return array;
        }
      }

      IPdfPrimitive IPdfWrapper.Element => (IPdfPrimitive) this.m_dictionary;
    }
}
