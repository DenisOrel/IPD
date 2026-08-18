// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.PdfPaddings
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml


namespace Syncfusion.Pdf
{
    public class PdfPaddings
    {
      private float m_bottom;
      private float m_left;
      private float m_right;
      private float m_top;

      public PdfPaddings() => this.m_left = this.m_right = this.m_top = this.m_bottom = 0.5f;

      public PdfPaddings(float left, float right, float top, float bottom)
      {
        this.m_left = left;
        this.m_right = right;
        this.m_top = top;
        this.m_bottom = bottom;
      }

      public float All
      {
        set => this.m_left = this.m_right = this.m_top = this.m_bottom = value;
      }

      public float Bottom
      {
        get => this.m_bottom;
        set => this.m_bottom = value;
      }

      public float Left
      {
        get => this.m_left;
        set => this.m_left = value;
      }

      public float Right
      {
        get => this.m_right;
        set => this.m_right = value;
      }

      public float Top
      {
        get => this.m_top;
        set => this.m_top = value;
      }
    }
}
