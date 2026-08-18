// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.JPEG2000.wavelet.synthesis.SynWTFilterInt
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

#nullable disable
namespace Syncfusion.Pdf.JPEG2000.wavelet.synthesis;

public abstract class SynWTFilterInt : SynWTFilter
{
  public abstract void synthetize_hpf(
    int[] lowSig,
    int lowOff,
    int lowLen,
    int lowStep,
    int[] highSig,
    int highOff,
    int highLen,
    int highStep,
    int[] outSig,
    int outOff,
    int outStep);

  public override void synthetize_hpf(
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
    int outStep)
  {
    this.synthetize_hpf((int[]) lowSig, lowOff, lowLen, lowStep, (int[]) highSig, highOff, highLen, highStep, (int[]) outSig, outOff, outStep);
  }

  public override void synthetize_lpf(
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
    int outStep)
  {
    this.synthetize_lpf((int[]) lowSig, lowOff, lowLen, lowStep, (int[]) highSig, highOff, highLen, highStep, (int[]) outSig, outOff, outStep);
  }

  public abstract void synthetize_lpf(
    int[] lowSig,
    int lowOff,
    int lowLen,
    int lowStep,
    int[] highSig,
    int highOff,
    int highLen,
    int highStep,
    int[] outSig,
    int outOff,
    int outStep);

  public override int DataType => 3;
}
