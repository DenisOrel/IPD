// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.JPEG2000.codestream.CoordInfo
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml


namespace Syncfusion.Pdf.JPEG2000.codestream
{
    public abstract class CoordInfo
    {
      public int h;
      public int ulx;
      public int uly;
      public int w;

      public CoordInfo()
      {
      }

      public CoordInfo(int ulx, int uly, int w, int h)
      {
        this.ulx = ulx;
        this.uly = uly;
        this.w = w;
        this.h = h;
      }

      public override string ToString()
      {
        return $"ulx={(object) this.ulx},uly={(object) this.uly},w={(object) this.w},h={(object) this.h}";
      }
    }
}
