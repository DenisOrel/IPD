// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Compression.PdfPngFilter
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;

#nullable disable
namespace Syncfusion.Pdf.Compression;

internal class PdfPngFilter
{
  private const byte m_zero = 0;
  private static PdfPngFilter.RowFilter s_averageFilter = new PdfPngFilter.RowFilter(PdfPngFilter.CompressAverage);
  private static PdfPngFilter.RowFilter s_decompressFilter = new PdfPngFilter.RowFilter(PdfPngFilter.Decompress);
  private static PdfPngFilter.RowFilter s_paethFilter = new PdfPngFilter.RowFilter(PdfPngFilter.CompressPaeth);
  private static PdfPngFilter.RowFilter s_subFilter = new PdfPngFilter.RowFilter(PdfPngFilter.CompressSub);
  private static PdfPngFilter.RowFilter s_upFilter = new PdfPngFilter.RowFilter(PdfPngFilter.CompressUp);

  public static byte[] Compress(byte[] data, int bpr, PdfPngFilter.Type type)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    if (bpr <= 0)
      throw new ArgumentException("There can't be less or equal to zero bytes in a line.", nameof (bpr));
    PdfPngFilter.RowFilter filter;
    switch (type)
    {
      case PdfPngFilter.Type.None:
        return data;
      case PdfPngFilter.Type.Sub:
        filter = PdfPngFilter.s_subFilter;
        break;
      case PdfPngFilter.Type.Up:
        filter = PdfPngFilter.s_upFilter;
        break;
      case PdfPngFilter.Type.Average:
        filter = PdfPngFilter.s_averageFilter;
        break;
      case PdfPngFilter.Type.Paeth:
        filter = PdfPngFilter.s_paethFilter;
        break;
      default:
        throw new ArgumentException("Unsupported PNG filter: " + type.ToString(), nameof (type));
    }
    return PdfPngFilter.Modify(data, bpr, filter, true);
  }

  private static void CompressAverage(
    byte[] data,
    long inIndex,
    int inBPR,
    byte[] result,
    long resIndex,
    int resBPR)
  {
    long index1 = inIndex - (long) inBPR;
    result[(int) (IntPtr) resIndex] = (byte) 3;
    ++resIndex;
    for (int index2 = 0; index2 < inBPR; ++index2)
    {
      result[(int) (IntPtr) resIndex] = (byte) ((int) data[(int) (IntPtr) inIndex] - ((index2 > 0 ? (int) data[(int) (IntPtr) (inIndex - 1L)] : 0) + (index1 < 0L ? 0 : (int) data[(int) (IntPtr) index1])) >> 1);
      ++resIndex;
      ++inIndex;
      ++index1;
    }
  }

  private static void CompressPaeth(
    byte[] data,
    long inIndex,
    int inBPR,
    byte[] result,
    long resIndex,
    int resBPR)
  {
    long index1 = inIndex - (long) inBPR;
    result[(int) (IntPtr) resIndex] = (byte) 3;
    ++resIndex;
    for (int index2 = 0; index2 < inBPR; ++index2)
    {
      byte a = index2 > 0 ? data[(int) (IntPtr) (inIndex - 1L)] : (byte) 0;
      byte b = index1 < 0L ? (byte) 0 : data[(int) (IntPtr) index1];
      byte c = index1 < 1L ? (byte) 0 : data[(int) (IntPtr) (index1 - 1L)];
      result[(int) (IntPtr) resIndex] = (byte) ((uint) data[(int) (IntPtr) inIndex] - (uint) PdfPngFilter.PaethPredictor(a, b, c));
      ++resIndex;
      ++inIndex;
      ++index1;
    }
  }

  private static void CompressSub(
    byte[] data,
    long inIndex,
    int inBPR,
    byte[] result,
    long resIndex,
    int resBPR)
  {
    result[(int) (IntPtr) resIndex] = (byte) 1;
    ++resIndex;
    for (int index = 0; index < resBPR; ++index)
    {
      result[(int) (IntPtr) resIndex] = (byte) ((int) data[(int) (IntPtr) inIndex] - (index > 0 ? (int) data[(int) (IntPtr) (inIndex - 1L)] : 0));
      ++resIndex;
      ++inIndex;
    }
  }

  private static void CompressUp(
    byte[] data,
    long inIndex,
    int inBPR,
    byte[] result,
    long resIndex,
    int resBPR)
  {
    long index1 = inIndex - (long) inBPR;
    result[(int) (IntPtr) resIndex] = (byte) 2;
    ++resIndex;
    for (int index2 = 0; index2 < inBPR; ++index2)
    {
      result[(int) (IntPtr) resIndex] = (byte) ((int) data[(int) (IntPtr) inIndex] - (index1 < 0L ? 0 : (int) data[(int) (IntPtr) index1]));
      ++resIndex;
      ++inIndex;
      ++index1;
    }
  }

  public static byte[] Decompress(byte[] data, int bpr)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    if (bpr <= 0)
      throw new ArgumentException("There can't be less or equal to zero bytes in a line.", nameof (bpr));
    return PdfPngFilter.Modify(data, bpr + 1, PdfPngFilter.s_decompressFilter, false);
  }

  private static void Decompress(
    byte[] data,
    long inIndex,
    int inBPR,
    byte[] result,
    long resIndex,
    int resBPR)
  {
    switch (data[(int) (IntPtr) inIndex])
    {
      case 0:
        PdfPngFilter.DecompressNone(data, inIndex + 1L, inBPR, result, resIndex, resBPR);
        break;
      case 1:
        PdfPngFilter.DeompressSub(data, inIndex + 1L, inBPR, result, resIndex, resBPR);
        break;
      case 2:
        PdfPngFilter.DecompressUp(data, inIndex + 1L, inBPR, result, resIndex, resBPR);
        break;
      case 3:
        PdfPngFilter.DecompressAverage(data, inIndex + 1L, inBPR, result, resIndex, resBPR);
        break;
      case 4:
        PdfPngFilter.DecompressPaeth(data, inIndex + 1L, inBPR, result, resIndex, resBPR);
        break;
      default:
        throw new ArgumentException("Unsupported PNG filter: " + data[(int) (IntPtr) inIndex].ToString(), "type");
    }
  }

  private static void DecompressAverage(
    byte[] data,
    long inIndex,
    int inBPR,
    byte[] result,
    long resIndex,
    int resBPR)
  {
    long index1 = inIndex - (long) inBPR;
    for (int index2 = 0; index2 < inBPR; ++index2)
    {
      result[(int) (IntPtr) resIndex] = (byte) ((int) data[(int) (IntPtr) inIndex] + ((index2 <= 0 || inIndex - 1L >= (long) result.Length ? 0 : (int) result[(int) (IntPtr) (inIndex - 1L)]) + (index1 < 0L ? 0 : (index1 < (long) result.Length ? (int) result[(int) (IntPtr) index1] : 0))) >> 1);
      ++resIndex;
      ++inIndex;
      ++index1;
    }
  }

  private static void DecompressNone(
    byte[] data,
    long inIndex,
    int inBPR,
    byte[] result,
    long resIndex,
    int resBPR)
  {
    for (int index = 1; index < inBPR; ++index)
    {
      result[(int) (IntPtr) resIndex] = data[(int) (IntPtr) inIndex];
      ++resIndex;
      ++inIndex;
    }
  }

  private static void DecompressPaeth(
    byte[] data,
    long inIndex,
    int inBPR,
    byte[] result,
    long resIndex,
    int resBPR)
  {
    long index1 = inIndex - (long) inBPR;
    for (int index2 = 0; index2 < inBPR; ++index2)
    {
      byte a = index2 <= 0 || inIndex - 1L >= (long) result.Length ? (byte) 0 : result[(int) (IntPtr) (inIndex - 1L)];
      byte b = index1 < 0L || index1 > (long) result.Length ? (byte) 0 : result[(int) (IntPtr) index1];
      byte c = index1 < 1L || index1 - 1L > (long) result.Length ? (byte) 0 : result[(int) (IntPtr) (index1 - 1L)];
      result[(int) (IntPtr) resIndex] = (byte) ((uint) data[(int) (IntPtr) inIndex] + (uint) PdfPngFilter.PaethPredictor(a, b, c));
      ++resIndex;
      ++inIndex;
      ++index1;
    }
  }

  private static byte[] DecompressUp(
    byte[] data,
    long inIndex,
    int inBPR,
    byte[] result,
    long resIndex,
    int resBPR)
  {
    long index1 = resIndex - (long) resBPR;
    for (int index2 = 0; index2 < resBPR; ++index2)
    {
      result[(int) (IntPtr) resIndex] = (byte) ((int) data[(int) (IntPtr) inIndex] + (index1 < 0L ? 0 : (int) result[(int) (IntPtr) index1]));
      ++resIndex;
      ++inIndex;
      ++index1;
    }
    return result;
  }

  private static void DeompressSub(
    byte[] data,
    long inIndex,
    int inBPR,
    byte[] result,
    long resIndex,
    int resBPR)
  {
    for (int index = 0; index < resBPR; ++index)
    {
      result[(int) (IntPtr) resIndex] = (byte) ((int) data[(int) (IntPtr) inIndex] + (index > 0 ? (int) result[(int) (IntPtr) (resIndex - 1L)] : 0));
      ++resIndex;
      ++inIndex;
    }
  }

  private static byte[] Modify(byte[] data, int bpr, PdfPngFilter.RowFilter filter, bool pack)
  {
    long inIndex = 0;
    long length = (long) data.Length;
    long num = length / (long) bpr;
    int resBPR = bpr - (pack ? -1 : 1);
    byte[] result = new byte[pack ? checked ((IntPtr) unchecked (num * (long) resBPR)) : checked ((IntPtr) unchecked (num * (long) resBPR))];
    int resIndex = 0;
    for (; inIndex + (long) bpr <= length; inIndex += (long) bpr)
    {
      filter(data, inIndex, bpr, result, (long) resIndex, resBPR);
      resIndex += resBPR;
    }
    return result;
  }

  private static byte PaethPredictor(byte a, byte b, byte c)
  {
    int num1 = (int) a + (int) b - (int) c;
    int num2 = Math.Abs(num1 - (int) a);
    int num3 = Math.Abs(num1 - (int) b);
    int num4 = Math.Abs(num1 - (int) c);
    if (num2 <= num3 && num2 <= num4)
      return a;
    return num3 <= num4 ? b : c;
  }

  private delegate void RowFilter(
    byte[] data,
    long inIndex,
    int inBPR,
    byte[] result,
    long resIndex,
    int resBPR);

  internal enum Type
  {
    None,
    Sub,
    Up,
    Average,
    Paeth,
  }
}
