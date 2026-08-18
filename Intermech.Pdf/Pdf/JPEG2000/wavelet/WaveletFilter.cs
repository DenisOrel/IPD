// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.JPEG2000.wavelet.WaveletFilter
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml


namespace Syncfusion.Pdf.JPEG2000.wavelet
{
    internal interface WaveletFilter
    {
      bool isSameAsFullWT(int tailOvrlp, int headOvrlp, int inLen);

      int AnHighNegSupport { get; }

      int AnHighPosSupport { get; }

      int AnLowNegSupport { get; }

      int AnLowPosSupport { get; }

      int DataType { get; }

      int ImplType { get; }

      bool Reversible { get; }

      int SynHighNegSupport { get; }

      int SynHighPosSupport { get; }

      int SynLowNegSupport { get; }

      int SynLowPosSupport { get; }
    }
}
