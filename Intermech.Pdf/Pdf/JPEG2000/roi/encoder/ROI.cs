// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.JPEG2000.roi.encoder.ROI
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.JPEG2000.image.input;


namespace Syncfusion.Pdf.JPEG2000.roi.encoder
{
    internal class ROI
    {
      public bool arbShape;
      public int comp;
      public int h;
      public ImgReaderPGM maskPGM;
      public int r;
      public bool rect;
      public int ulx;
      public int uly;
      public int w;
      public int x;
      public int y;

      public ROI(int comp, ImgReaderPGM maskPGM)
      {
        this.arbShape = true;
        this.rect = false;
        this.comp = comp;
        this.maskPGM = maskPGM;
      }

      public ROI(int comp, int x, int y, int rad)
      {
        this.arbShape = false;
        this.comp = comp;
        this.x = x;
        this.y = y;
        this.r = rad;
      }

      public ROI(int comp, int ulx, int uly, int w, int h)
      {
        this.arbShape = false;
        this.comp = comp;
        this.ulx = ulx;
        this.uly = uly;
        this.w = w;
        this.h = h;
        this.rect = true;
      }

      public override string ToString()
      {
        if (this.arbShape)
          return "ROI with arbitrary shape, PGM file= " + (object) this.maskPGM;
        return this.rect ? $"Rectangular ROI, comp={(object) this.comp} ulx={(object) this.ulx} uly={(object) this.uly} w={(object) this.w} h={(object) this.h}" : $"Circular ROI,  comp={(object) this.comp} x={(object) this.x} y={(object) this.y} radius={(object) this.r}";
      }
    }
}
