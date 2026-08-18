// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.JPEG2000.wavelet.synthesis.SynWTFilterFloatLift9x7
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

#nullable disable
namespace Syncfusion.Pdf.JPEG2000.wavelet.synthesis;

internal class SynWTFilterFloatLift9x7 : SynWTFilterFloat
{
  public const float ALPHA = -1.586134f;
  public const float BETA = -0.05298012f;
  public const float DELTA = 0.4435069f;
  public const float GAMMA = 0.8829111f;
  public const float KH = 1.230174f;
  public const float KL = 0.8128931f;

  public override bool isSameAsFullWT(int tailOvrlp, int headOvrlp, int inLen)
  {
    return inLen % 2 == 0 ? tailOvrlp >= 2 && headOvrlp >= 1 : tailOvrlp >= 2 && headOvrlp >= 2;
  }

  public override void synthetize_hpf(
    float[] lowSig,
    int lowOff,
    int lowLen,
    int lowStep,
    float[] highSig,
    int highOff,
    int highLen,
    int highStep,
    float[] outSig,
    int outOff,
    int outStep)
  {
    int num1 = lowLen + highLen;
    int num2 = 2 * outStep;
    int index1 = lowOff;
    int index2 = highOff;
    if (num1 != 1)
    {
      int num3 = num1 >> 1;
      for (int index3 = 0; index3 < num3; ++index3)
      {
        lowSig[index1] /= 0.8128931f;
        highSig[index2] /= 1.230174f;
        index1 += lowStep;
        index2 += highStep;
      }
      if (num1 % 2 == 1)
        highSig[index2] /= 1.230174f;
    }
    else
      highSig[highOff] /= 2f;
    int index4 = lowOff;
    int index5 = highOff;
    int index6 = outOff + outStep;
    for (int index7 = 1; index7 < num1 - 1; index7 += 2)
    {
      outSig[index6] = lowSig[index4] - (float) (0.44350689649581909 * ((double) highSig[index5] + (double) highSig[index5 + highStep]));
      index6 += num2;
      index4 += lowStep;
      index5 += highStep;
    }
    if (num1 % 2 == 0 && num1 > 1)
      outSig[index6] = lowSig[index4] - 0.8870137f * highSig[index5];
    int index8 = highOff;
    int index9 = outOff;
    outSig[index9] = num1 <= 1 ? highSig[index8] : highSig[index8] - 1.765822f * outSig[index9 + outStep];
    int index10 = index9 + num2;
    int index11 = index8 + highStep;
    for (int index12 = 2; index12 < num1 - 1; index12 += 2)
    {
      outSig[index10] = highSig[index11] - (float) (0.8829110860824585 * ((double) outSig[index10 - outStep] + (double) outSig[index10 + outStep]));
      index10 += num2;
      index11 += highStep;
    }
    if (num1 % 2 == 1 && num1 > 1)
      outSig[index10] = highSig[index11] - 1.765822f * outSig[index10 - outStep];
    int index13 = outOff + outStep;
    for (int index14 = 1; index14 < num1 - 1; index14 += 2)
    {
      outSig[index13] -= (float) (-0.052980121225118637 * ((double) outSig[index13 - outStep] + (double) outSig[index13 + outStep]));
      index13 += num2;
    }
    if (num1 % 2 == 0 && num1 > 1)
      outSig[index13] -= -0.1059602f * outSig[index13 - outStep];
    int index15 = outOff;
    if (num1 > 1)
      outSig[index15] -= -3.172269f * outSig[index15 + outStep];
    int index16 = index15 + num2;
    for (int index17 = 2; index17 < num1 - 1; index17 += 2)
    {
      outSig[index16] -= (float) (-1.5861339569091797 * ((double) outSig[index16 - outStep] + (double) outSig[index16 + outStep]));
      index16 += num2;
    }
    if (num1 % 2 != 1 || num1 <= 1)
      return;
    outSig[index16] -= -3.172269f * outSig[index16 - outStep];
  }

  public override void synthetize_lpf(
    float[] lowSig,
    int lowOff,
    int lowLen,
    int lowStep,
    float[] highSig,
    int highOff,
    int highLen,
    int highStep,
    float[] outSig,
    int outOff,
    int outStep)
  {
    int num1 = lowLen + highLen;
    int num2 = 2 * outStep;
    int index1 = lowOff;
    int index2 = highOff;
    int index3 = outOff;
    outSig[index3] = num1 <= 1 ? lowSig[index1] : (float) ((double) lowSig[index1] / 0.8128930926322937 - 0.88701367378234863 * (double) highSig[index2] / 1.2301739454269409);
    int index4 = index1 + lowStep;
    int index5 = index2 + highStep;
    int index6 = index3 + num2;
    int num3 = 2;
    while (num3 < num1 - 1)
    {
      outSig[index6] = (float) ((double) lowSig[index4] / 0.8128930926322937 - 0.44350689649581909 * ((double) highSig[index5 - highStep] + (double) highSig[index5]) / 1.2301739454269409);
      num3 += 2;
      index6 += num2;
      index4 += lowStep;
      index5 += highStep;
    }
    if (num1 % 2 == 1 && num1 > 2)
      outSig[index6] = (float) ((double) lowSig[index4] / 0.8128930926322937 - 0.88701367378234863 * (double) highSig[index5 - highStep] / 1.2301739454269409);
    int num4 = lowOff;
    int index7 = highOff;
    int index8 = outOff + outStep;
    int num5 = 1;
    while (num5 < num1 - 1)
    {
      outSig[index8] = (float) ((double) highSig[index7] / 1.2301739454269409 - 0.8829110860824585 * ((double) outSig[index8 - outStep] + (double) outSig[index8 + outStep]));
      num5 += 2;
      index8 += num2;
      index7 += highStep;
      num4 += lowStep;
    }
    if (num1 % 2 == 0)
      outSig[index8] = (float) ((double) highSig[index7] / 1.2301739454269409 - 1.7658220529556274 * (double) outSig[index8 - outStep]);
    int index9 = outOff;
    if (num1 > 1)
      outSig[index9] -= -0.1059602f * outSig[index9 + outStep];
    int index10 = index9 + num2;
    int num6 = 2;
    while (num6 < num1 - 1)
    {
      outSig[index10] -= (float) (-0.052980121225118637 * ((double) outSig[index10 - outStep] + (double) outSig[index10 + outStep]));
      num6 += 2;
      index10 += num2;
    }
    if (num1 % 2 == 1 && num1 > 2)
      outSig[index10] -= -0.1059602f * outSig[index10 - outStep];
    int index11 = outOff + outStep;
    int num7 = 1;
    while (num7 < num1 - 1)
    {
      outSig[index11] -= (float) (-1.5861339569091797 * ((double) outSig[index11 - outStep] + (double) outSig[index11 + outStep]));
      num7 += 2;
      index11 += num2;
    }
    if (num1 % 2 != 0)
      return;
    outSig[index11] -= -3.172269f * outSig[index11 - outStep];
  }

  public override string ToString() => "w9x7 (lifting)";

  public override int AnHighNegSupport => 3;

  public override int AnHighPosSupport => 3;

  public override int AnLowNegSupport => 4;

  public override int AnLowPosSupport => 4;

  public override int ImplType => WaveletFilter_Fields.WT_FILTER_FLOAT_LIFT;

  public override bool Reversible => false;

  public override int SynHighNegSupport => 4;

  public override int SynHighPosSupport => 4;

  public override int SynLowNegSupport => 3;

  public override int SynLowPosSupport => 3;
}
