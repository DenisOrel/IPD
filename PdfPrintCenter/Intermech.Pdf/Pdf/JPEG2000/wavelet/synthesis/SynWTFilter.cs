// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.JPEG2000.wavelet.synthesis.SynWTFilter
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

#nullable disable
namespace Syncfusion.Pdf.JPEG2000.wavelet.synthesis;

public abstract class SynWTFilter : WaveletFilter
{
  public abstract bool isSameAsFullWT(int param1, int param2, int param3);

  public abstract void synthetize_hpf(
    object lowSig,
    int lowOff,
    int lowLen,
    int lowStep,
    object highSig,
    int highOff,
    int highLen,
    int highStep,
    object outSig,
    int outOff,
    int outStep);

  public abstract void synthetize_lpf(
    object lowSig,
    int lowOff,
    int lowLen,
    int lowStep,
    object highSig,
    int highOff,
    int highLen,
    int highStep,
    object outSig,
    int outOff,
    int outStep);

  public abstract int AnHighNegSupport { get; }

  public abstract int AnHighPosSupport { get; }

  public abstract int AnLowNegSupport { get; }

  public abstract int AnLowPosSupport { get; }

  public abstract int DataType { get; }

  public abstract int ImplType { get; }

  public abstract bool Reversible { get; }

  public abstract int SynHighNegSupport { get; }

  public abstract int SynHighPosSupport { get; }

  public abstract int SynLowNegSupport { get; }

  public abstract int SynLowPosSupport { get; }
}
