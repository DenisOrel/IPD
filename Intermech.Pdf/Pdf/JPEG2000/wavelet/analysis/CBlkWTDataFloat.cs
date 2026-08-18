// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.JPEG2000.wavelet.analysis.CBlkWTDataFloat
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml


namespace Syncfusion.Pdf.JPEG2000.wavelet.analysis
{
    internal class CBlkWTDataFloat : CBlkWTData
    {
      private float[] data;

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
}
