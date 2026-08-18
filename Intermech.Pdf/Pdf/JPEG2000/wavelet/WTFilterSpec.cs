// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.JPEG2000.wavelet.WTFilterSpec
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml


namespace Syncfusion.Pdf.JPEG2000.wavelet
{
    internal abstract class WTFilterSpec
    {
      public const byte FILTER_SPEC_COMP_DEF = 1;
      public const byte FILTER_SPEC_MAIN_DEF = 0;
      public const byte FILTER_SPEC_TILE_COMP = 3;
      public const byte FILTER_SPEC_TILE_DEF = 2;
      internal byte[] specValType;

      internal WTFilterSpec(int nc) => this.specValType = new byte[nc];

      public virtual byte getKerSpecType(int n) => this.specValType[n];

      public abstract int WTDataType { get; }
    }
}
