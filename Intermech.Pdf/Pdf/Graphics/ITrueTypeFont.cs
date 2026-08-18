// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.ITrueTypeFont
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics.Fonts;
using Syncfusion.Pdf.Primitives;
using System.Drawing;


namespace Syncfusion.Pdf.Graphics
{
    internal interface ITrueTypeFont
    {
      void Close();

      void CreateInternals();

      bool EqualsToFont(PdfFont font);

      int GetCharWidth(char charCode);

      IPdfPrimitive GetInternals();

      int GetLineWidth(string line);

      Font Font { get; }

      PdfFontMetrics Metrics { get; }

      float Size { get; }
    }
}
