// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.JPEG2000.wavelet.analysis.CBlkWTDataInt
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

#nullable disable
namespace Syncfusion.Pdf.JPEG2000.wavelet.analysis;

internal class CBlkWTDataInt : CBlkWTData
{
  public int[] data_array;

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
