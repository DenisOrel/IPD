// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.JPEG2000.image.DataBlockFloat
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;

#nullable disable
namespace Syncfusion.Pdf.JPEG2000.image;

internal class DataBlockFloat : DataBlock
{
  private float[] data;

  public DataBlockFloat()
  {
  }

  public DataBlockFloat(DataBlockFloat src)
  {
    this.ulx = src.ulx;
    this.uly = src.uly;
    this.w = src.w;
    this.h = src.h;
    this.offset = 0;
    this.scanw = this.w;
    this.data = new float[this.w * this.h];
    for (int index = 0; index < this.h; ++index)
      Array.Copy((Array) src.data, index * src.scanw, (Array) this.data, index * this.scanw, this.w);
  }

  public DataBlockFloat(int ulx, int uly, int w, int h)
  {
    this.ulx = ulx;
    this.uly = uly;
    this.w = w;
    this.h = h;
    this.offset = 0;
    this.scanw = w;
    this.data = new float[w * h];
  }

  public override string ToString()
  {
    string str = base.ToString();
    if (this.data != null)
      str = $"{str},data={(object) this.data.Length} bytes";
    return str;
  }

  public override object Data
  {
    get => (object) this.data;
    set => this.data = (float[]) value;
  }

  public virtual float[] DataFloat
  {
    get => this.data;
    set => this.data = value;
  }

  public override int DataType => 4;
}
