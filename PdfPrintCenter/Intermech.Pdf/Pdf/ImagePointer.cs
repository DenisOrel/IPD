// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.ImagePointer
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

#nullable disable
namespace Syncfusion.Pdf;

internal class ImagePointer
{
  private JBIG2Image m_bitmap;
  private int m_height;
  private int m_width;
  private int m_x;
  private int m_y;

  internal ImagePointer(JBIG2Image bitmap)
  {
    this.m_bitmap = bitmap;
    this.m_height = bitmap.Height;
    this.m_width = bitmap.Width;
  }

  internal int NextPixel()
  {
    if (this.m_y < 0 || this.m_y >= this.m_height || this.m_x >= this.m_width)
      return 0;
    if (this.m_x < 0)
    {
      ++this.m_x;
      return 0;
    }
    int pixel = this.m_bitmap.GetPixel(this.m_x, this.m_y);
    ++this.m_x;
    return pixel;
  }

  internal void SetPointer(int x, int y)
  {
    this.m_x = x;
    this.m_y = y;
  }
}
