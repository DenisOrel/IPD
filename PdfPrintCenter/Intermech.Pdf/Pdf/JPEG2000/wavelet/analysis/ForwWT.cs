// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.JPEG2000.wavelet.analysis.ForwWT
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.JPEG2000.image;

#nullable disable
namespace Syncfusion.Pdf.JPEG2000.wavelet.analysis;

internal interface ForwWT : WaveletTransform, ImageData, ForwWTDataProps
{
  int getDecomp(int t, int c);

  int getDecompLevels(int t, int c);

  AnWTFilter[] getHorAnWaveletFilters(int t, int c);

  AnWTFilter[] getVertAnWaveletFilters(int t, int c);
}
