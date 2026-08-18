// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.PdfPortfolioSchemaField
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;


namespace Syncfusion.Pdf
{
    public class PdfPortfolioSchemaField : IPdfWrapper
    {
      private PdfDictionary m_dictionary;
      private bool m_editable;
      private string m_name;
      private int m_order;
      private PdfPortfolioSchemaFieldType m_type;
      private bool m_visible;

      public PdfPortfolioSchemaField()
      {
        this.m_visible = true;
        this.m_dictionary = new PdfDictionary();
        this.Initialize();
      }

      internal PdfPortfolioSchemaField(PdfDictionary schemaField)
      {
        this.m_visible = true;
        this.m_dictionary = new PdfDictionary();
        this.m_dictionary = schemaField;
        if (this.m_dictionary.ContainsKey("N"))
          this.Name = (this.m_dictionary["N"] as PdfString).Value;
        if (this.m_dictionary.ContainsKey("O"))
          this.Order = (this.m_dictionary["O"] as PdfNumber).IntValue;
        if (this.m_dictionary.ContainsKey("V"))
          this.Visible = (this.m_dictionary["V"] as PdfBoolean).Value;
        if (this.m_dictionary.ContainsKey("E"))
          this.Editable = (this.m_dictionary["E"] as PdfBoolean).Value;
        if (!this.m_dictionary.ContainsKey("Subtype"))
          return;
        switch ((this.m_dictionary["Subtype"] as PdfName).Value)
        {
          case "S":
            this.Type = PdfPortfolioSchemaFieldType.String;
            break;
          case "Size":
            this.Type = PdfPortfolioSchemaFieldType.Size;
            break;
          case "N":
            this.Type = PdfPortfolioSchemaFieldType.Number;
            break;
          case "D":
            this.Type = PdfPortfolioSchemaFieldType.Date;
            break;
        }
      }

      private void Initialize()
      {
        this.m_dictionary.SetProperty("Type", (IPdfPrimitive) new PdfName("CollectionField"));
        this.m_dictionary.SetBoolean("V", true);
      }

      public bool Editable
      {
        get => this.m_editable;
        set
        {
          this.m_editable = value;
          this.m_dictionary.SetBoolean("E", this.m_editable);
        }
      }

      public string Name
      {
        get => this.m_name;
        set
        {
          this.m_name = value;
          this.m_dictionary.SetProperty("N", (IPdfPrimitive) new PdfString(this.m_name));
        }
      }

      public int Order
      {
        get => this.m_order;
        set
        {
          this.m_order = value;
          this.m_dictionary.SetNumber("O", this.m_order);
        }
      }

      IPdfPrimitive IPdfWrapper.Element => (IPdfPrimitive) this.m_dictionary;

      public PdfPortfolioSchemaFieldType Type
      {
        get => this.m_type;
        set
        {
          this.m_type = value;
          if (this.m_type == PdfPortfolioSchemaFieldType.String)
            this.m_dictionary.SetName("Subtype", "S");
          else if (this.m_type == PdfPortfolioSchemaFieldType.Date)
          {
            this.m_dictionary.SetName("Subtype", "D");
          }
          else
          {
            if (this.m_type != PdfPortfolioSchemaFieldType.Number)
              return;
            this.m_dictionary.SetName("Subtype", "N");
          }
        }
      }

      public bool Visible
      {
        get => this.m_visible;
        set
        {
          this.m_visible = value;
          this.m_dictionary.SetBoolean("V", this.m_visible);
        }
      }
    }
}
