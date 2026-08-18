// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.JPEG2000.codestream.PrecInfo
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml


namespace Syncfusion.Pdf.JPEG2000.codestream
{
    internal class PrecInfo
    {
      public CBlkCoordInfo[][][] cblk;
      public int h;
      public int[] nblk;
      public int r;
      public int rgh;
      public int rgulx;
      public int rguly;
      public int rgw;
      public int ulx;
      public int uly;
      public int w;

      public PrecInfo(
        int r,
        int ulx,
        int uly,
        int w,
        int h,
        int rgulx,
        int rguly,
        int rgw,
        int rgh)
      {
        this.r = r;
        this.ulx = ulx;
        this.uly = uly;
        this.w = w;
        this.h = h;
        this.rgulx = rgulx;
        this.rguly = rguly;
        this.rgw = rgw;
        this.rgh = rgh;
        if (r == 0)
        {
          this.cblk = new CBlkCoordInfo[1][][];
          this.nblk = new int[1];
        }
        else
        {
          this.cblk = new CBlkCoordInfo[4][][];
          this.nblk = new int[4];
        }
      }

      public override string ToString()
      {
        return $"ulx={(object) this.ulx},uly={(object) this.uly},w={(object) this.w},h={(object) this.h},rgulx={(object) this.rgulx},rguly={(object) this.rguly},rgw={(object) this.rgw},rgh={(object) this.rgh}";
      }
    }
}
