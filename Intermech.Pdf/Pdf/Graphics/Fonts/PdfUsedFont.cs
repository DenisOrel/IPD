// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.Fonts.PdfUsedFont
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using System.Collections.Generic;
using System.Drawing;


namespace Syncfusion.Pdf.Graphics.Fonts
{
    public class PdfUsedFont
    {
      private string m_actualFontName;
      private PdfFont m_internalFont;
      private PdfLoadedPage m_lpage;
      private string m_name;
      private float m_size;
      private PdfFontStyle m_style;
      private PdfFontType m_type;

      public PdfUsedFont(PdfFont font, PdfLoadedPage page) => this.InitializeInternals(font, page);

      private void CheckPreambula()
      {
        if (this.Type == PdfFontType.TrueTypeEmbedded)
          throw new PdfException("Can't replace font,  the font is already embedded");
      }

      private string GetActualFontName()
      {
        string empty = string.Empty;
        foreach (KeyValuePair<IPdfPrimitive, PdfName> name in this.m_lpage.GetResources().GetNames())
        {
          string str = (name.Key as PdfDictionary)["BaseFont"].ToString().TrimStart('/');
          if (str == this.Name || str == this.InternalFont.InternalFontName)
          {
            empty = name.Value.ToString();
            break;
          }
        }
        return empty.TrimStart('/');
      }

      private void InitializeInternals(PdfFont font, PdfLoadedPage page)
      {
        this.m_lpage = page;
        this.m_internalFont = font;
        this.m_name = font.Name;
        this.m_size = font.Size;
        this.m_style = font.Style;
        switch (font)
        {
          case PdfStandardFont _:
            this.m_type = PdfFontType.Standard;
            return;
          case PdfTrueTypeFont _:
            if (!(font as PdfTrueTypeFont).Unicode)
            {
              this.m_type = PdfFontType.TrueType;
              return;
            }
            break;
        }
        this.m_type = PdfFontType.TrueTypeEmbedded;
      }

      public void Replace(PdfFont fontToReplace)
      {
        this.CheckPreambula();
        PdfFont font1 = fontToReplace;
        PdfResources resources = this.m_lpage.GetResources();
        if (fontToReplace is PdfTrueTypeFont)
        {
          Font font2 = (fontToReplace as PdfTrueTypeFont).Font;
          string fontFile = (fontToReplace as PdfTrueTypeFont).FontFile;
          font1 = font2 != null || fontFile == null ? (PdfFont) new PdfTrueTypeFont(font2, true, true) : (PdfFont) new PdfTrueTypeFont(fontFile, fontToReplace.Size, true);
        }
        if (resources == null || font1 == null)
          return;
        PdfName name = resources.GetName(this.ActualFontName);
        resources.RemoveFont(name.Value);
        resources.Add(font1, name);
      }

      internal string ActualFontName
      {
        get
        {
          if (this.m_actualFontName == null)
            this.m_actualFontName = this.GetActualFontName();
          return this.GetActualFontName();
        }
      }

      internal PdfFont InternalFont => this.m_internalFont;

      public string Name => this.m_name;

      public float Size => this.m_size;

      public PdfFontStyle Style => this.m_style;

      public PdfFontType Type => this.m_type;
    }
}
