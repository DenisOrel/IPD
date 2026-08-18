// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.WidgetAppearance
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Primitives;


namespace Syncfusion.Pdf.Interactive
{
    internal class WidgetAppearance : IPdfWrapper
    {
      private PdfColor m_backColor = new PdfColor(byte.MaxValue, byte.MaxValue, byte.MaxValue);
      private PdfColor m_borderColor = new PdfColor((byte) 0, (byte) 0, (byte) 0);
      private PdfDictionary m_dictionary = new PdfDictionary();
      private string m_normalCaption = string.Empty;

      public WidgetAppearance()
      {
        this.m_dictionary.SetProperty("BC", (IPdfPrimitive) this.m_borderColor.ToArray());
        this.m_dictionary.SetProperty("BG", (IPdfPrimitive) this.m_backColor.ToArray());
      }

      public PdfColor BackColor
      {
        get => this.m_backColor;
        set
        {
          if (!(this.m_backColor != value))
            return;
          this.m_backColor = value;
          if (this.m_backColor.A == (byte) 0)
          {
            this.m_dictionary.SetProperty("BC", (IPdfPrimitive) new PdfArray(new float[3]));
            this.m_dictionary.Remove("BG");
          }
          else
            this.m_dictionary.SetProperty("BG", (IPdfPrimitive) this.m_backColor.ToArray());
        }
      }

      public PdfColor BorderColor
      {
        get => this.m_borderColor;
        set
        {
          if (!(this.m_borderColor != value))
            return;
          this.m_borderColor = value;
          this.m_dictionary.SetProperty("BC", (IPdfPrimitive) this.m_borderColor.ToArray());
        }
      }

      public string NormalCaption
      {
        get => this.m_normalCaption;
        set
        {
          if (!(this.m_normalCaption != value))
            return;
          this.m_normalCaption = value;
          this.m_dictionary.SetString("CA", this.m_normalCaption);
        }
      }

      IPdfPrimitive IPdfWrapper.Element => (IPdfPrimitive) this.m_dictionary;
    }
}
