// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.JPEG2000.wavelet.analysis.AnWTFilterInt
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml


namespace Syncfusion.Pdf.JPEG2000.wavelet.analysis
{
    public abstract class AnWTFilterInt : AnWTFilter
    {
      public abstract void analyze_hpf(
        int[] inSig,
        int inOff,
        int inLen,
        int inStep,
        int[] lowSig,
        int lowOff,
        int lowStep,
        int[] highSig,
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
        this.analyze_hpf((int[]) inSig, inOff, inLen, inStep, (int[]) lowSig, lowOff, lowStep, (int[]) highSig, highOff, highStep);
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
        this.analyze_lpf((int[]) inSig, inOff, inLen, inStep, (int[]) lowSig, lowOff, lowStep, (int[]) highSig, highOff, highStep);
      }

      public abstract void analyze_lpf(
        int[] inSig,
        int inOff,
        int inLen,
        int inStep,
        int[] lowSig,
        int lowOff,
        int lowStep,
        int[] highSig,
        int highOff,
        int highStep);

      public override int DataType => 3;
    }
}
