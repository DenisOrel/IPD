// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.JPEG2000.roi.encoder.SubbandROIMask
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;


namespace Syncfusion.Pdf.JPEG2000.roi.encoder
{
    public abstract class SubbandROIMask
    {
      public int h;
      internal SubbandROIMask hh;
      internal SubbandROIMask hl;
      internal bool isNode;
      internal SubbandROIMask lh;
      internal SubbandROIMask ll;
      public int ulx;
      public int uly;
      public int w;

      public SubbandROIMask(int ulx, int uly, int w, int h)
      {
        this.ulx = ulx;
        this.uly = uly;
        this.w = w;
        this.h = h;
      }

      public virtual SubbandROIMask getSubbandRectROIMask(int x, int y)
      {
        if (x < this.ulx || y < this.uly || x >= this.ulx + this.w || y >= this.uly + this.h)
          throw new ArgumentException();
        SubbandROIMask subbandRectRoiMask;
        SubbandROIMask hh;
        for (subbandRectRoiMask = this; subbandRectRoiMask.isNode; subbandRectRoiMask = x >= hh.ulx ? (y >= hh.uly ? subbandRectRoiMask.hh : subbandRectRoiMask.hl) : (y >= hh.uly ? subbandRectRoiMask.lh : subbandRectRoiMask.ll))
          hh = subbandRectRoiMask.hh;
        return subbandRectRoiMask;
      }
    }
}
