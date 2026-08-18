// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfLoadedTextWebLinkAnnotation
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.IO;
using Syncfusion.Pdf.Primitives;
using System;


namespace Syncfusion.Pdf.Interactive
{
    public class PdfLoadedTextWebLinkAnnotation : PdfLoadedStyledAnnotation
    {
      private PdfCrossTable m_crossTable;
      private string m_url;

      internal PdfLoadedTextWebLinkAnnotation(
        PdfDictionary dictionary,
        PdfCrossTable crossTable,
        string text)
        : base(dictionary, crossTable)
      {
        if (text == null)
          throw new ArgumentNullException(nameof (text));
        this.Dictionary = dictionary;
        this.m_crossTable = crossTable;
      }

      private string GetUrl()
      {
        string empty = string.Empty;
        if (this.Dictionary.ContainsKey("A"))
          empty = ((this.m_crossTable.GetObject(this.Dictionary["A"]) as PdfDictionary)["URI"] as PdfString).Value.ToString();
        return empty;
      }

      public string Url
      {
        get => this.GetUrl();
        set
        {
          this.m_url = value;
          if (!this.Dictionary.ContainsKey("A"))
            return;
          (this.m_crossTable.GetObject(this.Dictionary["A"]) as PdfDictionary).SetString("URI", this.m_url);
          this.Dictionary.Modify();
        }
      }
    }
}
