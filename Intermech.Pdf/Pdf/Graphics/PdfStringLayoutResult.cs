// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.PdfStringLayoutResult
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System.Drawing;


namespace Syncfusion.Pdf.Graphics
{
    public class PdfStringLayoutResult
    {
      internal SizeF m_actualSize;
      internal float m_lineHeight;
      internal LineInfo[] m_lines;
      internal string m_remainder;

      public SizeF ActualSize => this.m_actualSize;

      internal bool Empty => this.m_lines == null || this.m_lines.Length == 0;

      internal int LineCount => this.Empty ? 0 : this.m_lines.Length;

      public float LineHeight => this.m_lineHeight;

      public LineInfo[] Lines => this.m_lines;

      public string Remainder => this.m_remainder;
    }
}
