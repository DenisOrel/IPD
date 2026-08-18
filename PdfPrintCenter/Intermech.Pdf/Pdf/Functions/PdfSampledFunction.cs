// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Functions.PdfSampledFunction
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using System;

#nullable disable
namespace Syncfusion.Pdf.Functions;

internal class PdfSampledFunction : PdfFunction
{
  private PdfSampledFunction()
    : base((PdfDictionary) new PdfStream())
  {
    this.Dictionary.SetProperty("FunctionType", (IPdfPrimitive) new PdfNumber(0));
  }

  internal PdfSampledFunction(float[] domain, float[] range, int[] sizes, byte[] samples)
    : this()
  {
    this.CheckParams(domain, range, sizes, (Array) samples);
    this.SetDomainAndRange(domain, range);
    this.SetSizeAndValues(sizes, samples);
  }

  internal PdfSampledFunction(float[] domain, float[] range, int[] sizes, int[] samples)
    : this()
  {
    this.CheckParams(domain, range, sizes, (Array) samples);
    this.SetDomainAndRange(domain, range);
    this.SetSizeAndValues(sizes, samples);
  }

  internal PdfSampledFunction(
    float[] domain,
    float[] range,
    int[] sizes,
    float[] samples,
    int bps)
    : this()
  {
    this.CheckParams(domain, range, sizes, (Array) samples);
    PdfDictionary dictionary = this.Dictionary;
  }

  private void CheckParams(float[] domain, float[] range, int[] sizes, Array samples)
  {
    if (domain == null)
      throw new ArgumentNullException(nameof (domain));
    if (range == null)
      throw new ArgumentNullException(nameof (range));
    if (samples == null)
      throw new ArgumentNullException(nameof (samples));
    int length1 = range.Length;
    int length2 = domain.Length;
    int length3 = samples.Length;
    if (length2 <= 0)
      throw new ArgumentException("The array has no enough elements", nameof (domain));
    if (length1 <= 0)
      throw new ArgumentException("The array has no enough elements", nameof (range));
    double num = (double) (length1 * length2 / 4);
    if ((double) length3 < num)
      throw new ArgumentException("There is no enough samples", nameof (samples));
  }

  private void SetDomainAndRange(float[] domain, float[] range)
  {
    this.Domain = new PdfArray(domain);
    this.Range = new PdfArray(range);
  }

  private void SetSizeAndValues(int[] sizes, byte[] samples)
  {
    PdfStream dictionary = this.Dictionary as PdfStream;
    this.Dictionary.SetProperty("Size", (IPdfPrimitive) new PdfArray(sizes));
    this.Dictionary.SetProperty("BitsPerSample", (IPdfPrimitive) new PdfNumber(8));
    byte[] data = samples;
    dictionary.Write(data);
  }

  private void SetSizeAndValues(int[] sizes, int[] samples)
  {
    PdfStream dictionary = this.Dictionary as PdfStream;
    this.Dictionary.SetProperty("Size", (IPdfPrimitive) new PdfArray(sizes));
    this.Dictionary.SetProperty("BitsPerSample", (IPdfPrimitive) new PdfNumber(32 /*0x20*/));
    byte[] data = new byte[samples.Length * 4];
    int index = 0;
    foreach (int sample in samples)
    {
      BitConverter.GetBytes(sample).CopyTo((Array) data, index);
      index += 4;
    }
    dictionary.Write(data);
  }
}
