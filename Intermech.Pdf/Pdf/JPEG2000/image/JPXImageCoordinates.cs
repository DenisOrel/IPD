// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.JPEG2000.image.JPXImageCoordinates
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml


namespace Syncfusion.Pdf.JPEG2000.image
{
    public class JPXImageCoordinates
    {
      public int x;
      public int y;

      public JPXImageCoordinates()
      {
      }

      public JPXImageCoordinates(JPXImageCoordinates c)
      {
        this.x = c.x;
        this.y = c.y;
      }

      public JPXImageCoordinates(int x, int y)
      {
        this.x = x;
        this.y = y;
      }

      public override string ToString() => $"({(object) this.x},{(object) this.y})";
    }
}
