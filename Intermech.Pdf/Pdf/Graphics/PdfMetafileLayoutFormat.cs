// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.PdfMetafileLayoutFormat
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml


namespace Syncfusion.Pdf.Graphics
{
    public class PdfMetafileLayoutFormat : PdfLayoutFormat
    {
      private bool m_htmlPageBreak;
      private bool m_splitImages;
      private bool m_splitLines;
      private float m_trackHeight;

      internal bool IsHTMLPageBreak
      {
        get => this.m_htmlPageBreak;
        set => this.m_htmlPageBreak = value;
      }

      public bool SplitImages
      {
        get => this.m_splitImages;
        set => this.m_splitImages = value;
      }

      public bool SplitTextLines
      {
        get => this.m_splitLines;
        set => this.m_splitLines = value;
      }

      internal float TrackHeight
      {
        get => this.m_trackHeight;
        set => this.m_trackHeight = value;
      }
    }
}
