// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.JPEG2000.image.DataBlockInt
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;


namespace Syncfusion.Pdf.JPEG2000.image
{
    internal class DataBlockInt : DataBlock
    {
      public int[] data_array;

      public DataBlockInt()
      {
      }

      public DataBlockInt(DataBlockInt src)
      {
        this.ulx = src.ulx;
        this.uly = src.uly;
        this.w = src.w;
        this.h = src.h;
        this.offset = 0;
        this.scanw = this.w;
        this.data_array = new int[this.w * this.h];
        for (int index = 0; index < this.h; ++index)
          Array.Copy((Array) src.data_array, index * src.scanw, (Array) this.data_array, index * this.scanw, this.w);
      }

      public DataBlockInt(int ulx, int uly, int w, int h)
      {
        this.ulx = ulx;
        this.uly = uly;
        this.w = w;
        this.h = h;
        this.offset = 0;
        this.scanw = w;
        this.data_array = new int[w * h];
      }

      public override string ToString()
      {
        string str = base.ToString();
        if (this.data_array != null)
          str = $"{str},data={(object) this.data_array.Length} bytes";
        return str;
      }

      public override object Data
      {
        get => (object) this.data_array;
        set => this.data_array = (int[]) value;
      }

      public virtual int[] DataInt
      {
        get => this.data_array;
        set => this.data_array = value;
      }

      public override int DataType => 3;
    }
}
