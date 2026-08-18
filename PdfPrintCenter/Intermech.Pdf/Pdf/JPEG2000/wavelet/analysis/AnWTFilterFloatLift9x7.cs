// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.JPEG2000.wavelet.analysis.AnWTFilterFloatLift9x7
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

#nullable disable
namespace Syncfusion.Pdf.JPEG2000.wavelet.analysis;

internal class AnWTFilterFloatLift9x7 : AnWTFilterFloat
{
  public const float ALPHA = -1.586134f;
  public const float BETA = -0.05298012f;
  public const float DELTA = 0.4435069f;
  public const float GAMMA = 0.8829111f;
  private static readonly float[] HPSynthesisFilter = new float[9]
  {
    0.026749f,
    0.016864f,
    -0.078223f,
    -0.266864f,
    0.602949f,
    -0.266864f,
    -0.078223f,
    0.016864f,
    0.026749f
  };
  public const float KH = 1.230174f;
  public const float KL = 0.8128931f;
  private static readonly float[] LPSynthesisFilter = new float[7]
  {
    -0.091272f,
    -0.057544f,
    0.591272f,
    1.115087f,
    0.591272f,
    -0.057544f,
    -0.091272f
  };

  public override void analyze_hpf(
    float[] inSig,
    int inOff,
    int inLen,
    int inStep,
    float[] lowSig,
    int lowOff,
    int lowStep,
    float[] highSig,
    int highOff,
    int highStep)
  {
    int num = 2 * inStep;
    int index1 = inOff;
    int index2 = highOff;
    highSig[index2] = inLen <= 1 ? inSig[index1] * 2f : inSig[index1] + -3.172269f * inSig[index1 + inStep];
    int index3 = index1 + num;
    int index4 = index2 + highStep;
    for (int index5 = 2; index5 < inLen - 1; index5 += 2)
    {
      highSig[index4] = inSig[index3] + (float) (-1.5861339569091797 * ((double) inSig[index3 - inStep] + (double) inSig[index3 + inStep]));
      index3 += num;
      index4 += highStep;
    }
    if (inLen % 2 == 1 && inLen > 1)
      highSig[index4] = inSig[index3] + -3.172269f * inSig[index3 - inStep];
    int index6 = inOff + inStep;
    int index7 = lowOff;
    int index8 = highOff;
    for (int index9 = 1; index9 < inLen - 1; index9 += 2)
    {
      lowSig[index7] = inSig[index6] + (float) (-0.052980121225118637 * ((double) highSig[index8] + (double) highSig[index8 + highStep]));
      index6 += num;
      index7 += lowStep;
      index8 += highStep;
    }
    if (inLen > 1 && inLen % 2 == 0)
      lowSig[index7] = inSig[index6] + -0.1059602f * highSig[index8];
    int index10 = lowOff;
    int index11 = highOff;
    if (inLen > 1)
      highSig[index11] += 1.765822f * lowSig[index10];
    int index12 = index11 + highStep;
    for (int index13 = 2; index13 < inLen - 1; index13 += 2)
    {
      highSig[index12] += (float) (0.8829110860824585 * ((double) lowSig[index10] + (double) lowSig[index10 + lowStep]));
      index10 += lowStep;
      index12 += highStep;
    }
    if (inLen > 1 && inLen % 2 == 1)
      highSig[index12] += 1.765822f * lowSig[index10];
    int index14 = lowOff;
    int index15 = highOff;
    for (int index16 = 1; index16 < inLen - 1; index16 += 2)
    {
      lowSig[index14] += (float) (0.44350689649581909 * ((double) highSig[index15] + (double) highSig[index15 + highStep]));
      index14 += lowStep;
      index15 += highStep;
    }
    if (inLen > 1 && inLen % 2 == 0)
      lowSig[index14] += 0.8870137f * highSig[index15];
    int index17 = lowOff;
    int index18 = highOff;
    for (int index19 = 0; index19 < inLen >> 1; ++index19)
    {
      lowSig[index17] *= 0.8128931f;
      highSig[index18] *= 1.230174f;
      index17 += lowStep;
      index18 += highStep;
    }
    if (inLen % 2 != 1 || inLen == 1)
      return;
    highSig[index18] *= 1.230174f;
  }

  public override void analyze_lpf(
    float[] inSig,
    int inOff,
    int inLen,
    int inStep,
    float[] lowSig,
    int lowOff,
    int lowStep,
    float[] highSig,
    int highOff,
    int highStep)
  {
    int num1 = 2 * inStep;
    int index1 = inOff + inStep;
    int index2 = highOff;
    int num2 = 1;
    for (int index3 = inLen - 1; num2 < index3; num2 += 2)
    {
      highSig[index2] = inSig[index1] + (float) (-1.5861339569091797 * ((double) inSig[index1 - inStep] + (double) inSig[index1 + inStep]));
      index1 += num1;
      index2 += highStep;
    }
    if (inLen % 2 == 0)
      highSig[index2] = inSig[index1] + -3.172269f * inSig[index1 - inStep];
    int index4 = inOff;
    int index5 = lowOff;
    int index6 = highOff;
    lowSig[index5] = inLen <= 1 ? inSig[index4] : inSig[index4] + -0.1059602f * highSig[index6];
    int index7 = index4 + num1;
    int index8 = index5 + lowStep;
    int index9 = index6 + highStep;
    int num3 = 2;
    for (int index10 = inLen - 1; num3 < index10; num3 += 2)
    {
      lowSig[index8] = inSig[index7] + (float) (-0.052980121225118637 * ((double) highSig[index9 - highStep] + (double) highSig[index9]));
      index7 += num1;
      index8 += lowStep;
      index9 += highStep;
    }
    if (inLen % 2 == 1 && inLen > 2)
      lowSig[index8] = inSig[index7] + -0.1059602f * highSig[index9 - highStep];
    int index11 = lowOff;
    int index12 = highOff;
    int num4 = 1;
    for (int index13 = inLen - 1; num4 < index13; num4 += 2)
    {
      highSig[index12] += (float) (0.8829110860824585 * ((double) lowSig[index11] + (double) lowSig[index11 + lowStep]));
      index11 += lowStep;
      index12 += highStep;
    }
    if (inLen % 2 == 0)
      highSig[index12] += 1.765822f * lowSig[index11];
    int index14 = lowOff;
    int index15 = highOff;
    if (inLen > 1)
      lowSig[index14] += 0.8870137f * highSig[index15];
    int index16 = index14 + lowStep;
    int index17 = index15 + highStep;
    int num5 = 2;
    for (int index18 = inLen - 1; num5 < index18; num5 += 2)
    {
      lowSig[index16] += (float) (0.44350689649581909 * ((double) highSig[index17 - highStep] + (double) highSig[index17]));
      index16 += lowStep;
      index17 += highStep;
    }
    if (inLen % 2 == 1 && inLen > 2)
      lowSig[index16] += 0.8870137f * highSig[index17 - highStep];
    int index19 = lowOff;
    int index20 = highOff;
    for (int index21 = 0; index21 < inLen >> 1; ++index21)
    {
      lowSig[index19] *= 0.8128931f;
      highSig[index20] *= 1.230174f;
      index19 += lowStep;
      index20 += highStep;
    }
    if (inLen % 2 != 1 || inLen == 1)
      return;
    lowSig[index19] *= 0.8128931f;
  }

  public override bool Equals(object obj) => obj == this || obj is AnWTFilterFloatLift9x7;

  public override int GetHashCode() => base.GetHashCode();

  public override float[] getHPSynthesisFilter() => AnWTFilterFloatLift9x7.HPSynthesisFilter;

  public override float[] getLPSynthesisFilter() => AnWTFilterFloatLift9x7.LPSynthesisFilter;

  public override bool isSameAsFullWT(int tailOvrlp, int headOvrlp, int inLen)
  {
    return inLen % 2 == 0 ? tailOvrlp >= 4 && headOvrlp >= 3 : tailOvrlp >= 4 && headOvrlp >= 4;
  }

  public override string ToString() => "w9x7";

  public override int AnHighNegSupport => 3;

  public override int AnHighPosSupport => 3;

  public override int AnLowNegSupport => 4;

  public override int AnLowPosSupport => 4;

  public override int FilterType => 0;

  public override int ImplType => WaveletFilter_Fields.WT_FILTER_FLOAT_LIFT;

  public override bool Reversible => false;

  public override int SynHighNegSupport => 4;

  public override int SynHighPosSupport => 4;

  public override int SynLowNegSupport => 3;

  public override int SynLowPosSupport => 3;
}
