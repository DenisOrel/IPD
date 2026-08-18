// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Exporting.PdfImageInfo
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.IO;
using System.Drawing;


namespace Syncfusion.Pdf.Exporting
{
    public class PdfImageInfo
    {
      private bool m_bisImageExtracted;
      private RectangleF m_bounds;
      private Image m_image;
      private int m_index;
      private bool m_maskImage;
      private PdfMatrix m_matrix;
      private string m_name;

      internal PdfImageInfo()
      {
      }

      internal PdfImageInfo(RectangleF bounds, Image image, int index)
      {
        this.m_bounds = bounds;
        this.m_image = image;
        this.m_index = index;
      }

      internal PdfImageInfo(RectangleF bounds, Image image, int index, string name)
      {
        this.m_bounds = bounds;
        this.m_image = image;
        this.m_index = index;
        this.m_name = name;
      }

      public RectangleF Bounds
      {
        get => this.m_bounds;
        internal set => this.m_bounds = value;
      }

      public Image Image
      {
        get => this.m_image;
        internal set => this.m_image = value;
      }

      public int Index
      {
        get => this.m_index;
        internal set => this.m_index = value;
      }

      internal bool IsImageExtracted
      {
        get => this.m_bisImageExtracted;
        set => this.m_bisImageExtracted = value;
      }

      internal bool MaskImage
      {
        get => this.m_maskImage;
        set => this.m_maskImage = value;
      }

      internal PdfMatrix Matrix
      {
        get => this.m_matrix;
        set => this.m_matrix = value;
      }

      internal string Name
      {
        get => this.m_name;
        set => this.m_name = value;
      }
    }
}
