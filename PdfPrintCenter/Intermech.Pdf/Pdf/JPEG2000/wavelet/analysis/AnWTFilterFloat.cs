// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.JPEG2000.wavelet.analysis.AnWTFilterFloat
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

#nullable disable
namespace Syncfusion.Pdf.JPEG2000.wavelet.analysis;

public abstract class AnWTFilterFloat : AnWTFilter
{
  public abstract void analyze_hpf(
    float[] inSig,
    int inOff,
    int inLen,
    int inStep,
    float[] lowSig,
    int lowOff,
    int lowStep,
    float[] highSig,
    int highOff,
    int highStep);

  public override void analyze_hpf(
    object inSig,
    int inOff,
    int inLen,
    int inStep,
    object lowSig,
    int lowOff,
    int lowStep,
    object highSig,
    int highOff,
    int highStep)
  {
    this.analyze_hpf((float[]) inSig, inOff, inLen, inStep, (float[]) lowSig, lowOff, lowStep, (float[]) highSig, highOff, highStep);
  }

  public override void analyze_lpf(
    object inSig,
    int inOff,
    int inLen,
    int inStep,
    object lowSig,
    int lowOff,
    int lowStep,
    object highSig,
    int highOff,
    int highStep)
  {
    this.analyze_lpf((float[]) inSig, inOff, inLen, inStep, (float[]) lowSig, lowOff, lowStep, (float[]) highSig, highOff, highStep);
  }

  public abstract void analyze_lpf(
    float[] inSig,
    int inOff,
    int inLen,
    int inStep,
    float[] lowSig,
    int lowOff,
    int lowStep,
    float[] highSig,
    int highOff,
    int highStep);

  public override int DataType => 4;
}
