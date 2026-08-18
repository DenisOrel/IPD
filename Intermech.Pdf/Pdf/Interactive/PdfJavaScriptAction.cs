// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfJavaScriptAction
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using System;


namespace Syncfusion.Pdf.Interactive
{
    public class PdfJavaScriptAction : PdfAction
    {
      private string m_javaScript = string.Empty;

      public PdfJavaScriptAction(string javaScript)
      {
        this.JavaScript = javaScript != null ? javaScript : throw new ArgumentNullException(nameof (javaScript));
      }

      protected override void Initialize()
      {
        base.Initialize();
        this.Dictionary.SetProperty("S", (IPdfPrimitive) new PdfName("JavaScript"));
        this.Dictionary.SetProperty("JS", (IPdfPrimitive) new PdfString(this.m_javaScript));
      }

      public string JavaScript
      {
        get => this.m_javaScript;
        set
        {
          if (!(this.m_javaScript != value))
            return;
          this.m_javaScript = value;
          this.Dictionary.SetString("JS", this.m_javaScript);
        }
      }
    }
}
