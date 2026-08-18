// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfActionAnnotation
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System.Drawing;


namespace Syncfusion.Pdf.Interactive
{
    public class PdfActionAnnotation(RectangleF rectangle, PdfAction action) : PdfActionLinkAnnotation(rectangle, action)
    {
      protected override void Save()
      {
        base.Save();
        this.Dictionary.SetProperty("A", (IPdfWrapper) this.Action);
      }
    }
}
