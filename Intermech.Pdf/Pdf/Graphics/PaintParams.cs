// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.PaintParams
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Interactive;
using System.Drawing;


namespace Syncfusion.Pdf.Graphics
{
    internal class PaintParams
    {
      private PdfBrush m_backBrush;
      private PdfPen m_borderPen;
      private PdfBorderStyle m_borderStyle;
      private int m_borderWidth;
      private RectangleF m_bounds;
      private PdfBrush m_foreBrush;
      private int m_rotationAngle;
      private PdfBrush m_shadowBrush;

      public PaintParams()
      {
        this.m_borderWidth = 1;
        this.m_bounds = RectangleF.Empty;
      }

      public PaintParams(
        RectangleF bounds,
        PdfBrush backBrush,
        PdfBrush foreBrush,
        PdfPen borderPen,
        PdfBorderStyle style,
        int borderWidth,
        PdfBrush shadowBrush,
        int rotationAngle)
      {
        this.m_borderWidth = 1;
        this.m_bounds = RectangleF.Empty;
        this.m_bounds = bounds;
        this.m_backBrush = backBrush;
        this.m_foreBrush = foreBrush;
        this.m_borderPen = borderPen;
        this.m_borderStyle = style;
        this.m_borderWidth = borderWidth;
        this.m_shadowBrush = shadowBrush;
        this.m_rotationAngle = rotationAngle;
      }

      public PdfBrush BackBrush
      {
        get => this.m_backBrush;
        set => this.m_backBrush = value;
      }

      public PdfPen BorderPen
      {
        get => this.m_borderPen;
        set => this.m_borderPen = value;
      }

      public PdfBorderStyle BorderStyle
      {
        get => this.m_borderStyle;
        set => this.m_borderStyle = value;
      }

      public int BorderWidth
      {
        get => this.m_borderWidth;
        set => this.m_borderWidth = value;
      }

      public RectangleF Bounds
      {
        get => this.m_bounds;
        set => this.m_bounds = value;
      }

      public PdfBrush ForeBrush
      {
        get => this.m_foreBrush;
        set => this.m_foreBrush = value;
      }

      public int RotationAngle
      {
        get => this.m_rotationAngle;
        set => this.m_rotationAngle = value;
      }

      public PdfBrush ShadowBrush
      {
        get => this.m_shadowBrush;
        set => this.m_shadowBrush = value;
      }
    }
}
