// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.LineInfo
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml


namespace Syncfusion.Pdf.Graphics
{
    public class LineInfo
    {
      internal LineType m_lineType;
      internal string m_text;
      internal float m_width;

      public LineType LineType
      {
        get => this.m_lineType;
        internal set => this.m_lineType = value;
      }

      public string Text
      {
        get => this.m_text;
        internal set => this.m_text = value;
      }

      public float Width
      {
        get => this.m_width;
        internal set => this.m_width = value;
      }
    }
}
