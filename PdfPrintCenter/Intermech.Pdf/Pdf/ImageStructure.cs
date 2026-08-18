// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.ImageStructure
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Compression;
using Syncfusion.Pdf.IO;
using Syncfusion.Pdf.Primitives;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Runtime.InteropServices;
using System.Text;

#nullable disable
namespace Syncfusion.Pdf;

internal class ImageStructure
{
  private Dictionary<string, MemoryStream> colorSpaceResourceDict;
  internal StringBuilder exceptions;
  private bool isDeviceN;
  private bool isIndexedImage;
  private bool IsTransparent;
  private float m_bitsPerComponent;
  private ColorPalette m_colorPalette;
  private string m_colorspace;
  private string m_colorspaceBase;
  private int m_colorspaceHival;
  private MemoryStream m_colorspaceStream;
  private PdfDictionary[] m_decodeParam;
  private Image m_embeddedImage;
  private float m_height;
  private PdfDictionary m_imageDictionary;
  private string[] m_imageFilter;
  private PdfMatrix m_imageInfo;
  private Stream m_imageStream;
  private bool m_isImageMask;
  private bool m_isImageStreamParsed;
  private float m_maskBitsPerComponent;
  private string m_maskFilter;
  private float m_maskHeight;
  private Stream m_maskStream;
  private float m_maskWidth;
  private PixelFormat m_pixelFormat;
  private float m_width;
  private Dictionary<string, PdfStream> nonIndexedImageColorResource;

  public static event ImageStructure.ImagePreRenderEventHandler ImagePreRender;

  public ImageStructure()
  {
    this.m_pixelFormat = PixelFormat.Format24bppRgb;
    this.exceptions = new StringBuilder();
    this.colorSpaceResourceDict = new Dictionary<string, MemoryStream>();
    this.nonIndexedImageColorResource = new Dictionary<string, PdfStream>();
  }

  public ImageStructure(IPdfPrimitive fontDictionary, PdfMatrix tm)
  {
    this.m_pixelFormat = PixelFormat.Format24bppRgb;
    this.exceptions = new StringBuilder();
    this.colorSpaceResourceDict = new Dictionary<string, MemoryStream>();
    this.nonIndexedImageColorResource = new Dictionary<string, PdfStream>();
    this.m_imageDictionary = fontDictionary as PdfDictionary;
    this.ImageInfo = tm;
  }

  private float[] ConvertCMYKToRGB(float[] values)
  {
    float num1 = values[0];
    float num2 = values[1];
    float num3 = values[2];
    float num4 = values[3];
    return new float[3]
    {
      (float) ((double) byte.MaxValue * (1.0 - (double) num1) * (1.0 - (double) num4)),
      (float) ((double) byte.MaxValue * (1.0 - (double) num2) * (1.0 - (double) num4)),
      (float) ((double) byte.MaxValue * (1.0 - (double) num3) * (1.0 - (double) num4))
    };
  }

  private byte[] ConvertIndexCMYKToRGB(byte[] data)
  {
    int length = data.Length;
    byte[] rgb1 = new byte[length * 3 / 4];
    int index1 = 0;
    for (int index2 = 0; index2 < length; index2 += 4)
    {
      float[] values = new float[4];
      for (int index3 = 0; index3 < 4; ++index3)
        values[index3] = (float) ((int) data[index2 + index3] & (int) byte.MaxValue) / (float) byte.MaxValue;
      float[] rgb2 = this.ConvertCMYKToRGB(values);
      rgb1[index1] = (byte) (int) rgb2[0];
      rgb1[index1 + 1] = (byte) (int) rgb2[1];
      rgb1[index1 + 2] = (byte) (int) rgb2[2];
      index1 += 3;
      if (length - 4 - index2 < 4)
        index2 = length;
    }
    return rgb1;
  }

  private byte[] ConvertIndexedStreamToFlat(
    int d,
    int w,
    int h,
    byte[] data,
    byte[] index,
    bool isARGB,
    bool isDownsampled)
  {
    int[] numArray = new int[3]{ 0, 1, 2 };
    new int[4]{ 0, 1, 2, 3 };
    int components = 3;
    int indexLength = 0;
    if (index != null)
      indexLength = index.Length;
    if (isARGB)
      components = 4;
    return this.ConvertIndexedStreamToFlat(d, w, h, data, index, isARGB, isDownsampled, components, indexLength);
  }

  private byte[] ConvertIndexedStreamToFlat(
    int d,
    int w,
    int h,
    byte[] data,
    byte[] index,
    bool isARGB,
    bool isDownsampled,
    int components,
    int indexLength)
  {
    int num1 = 0;
    int length = w * h * components;
    byte[] flat = new byte[length];
    int index1 = 0;
    float num2 = 0.0f;
    switch (d)
    {
      case 1:
        int num3 = 0;
        for (int index2 = 0; index2 < data.Length; ++index2)
        {
          for (int index3 = 0; index3 < 8; ++index3)
          {
            int index4 = ((int) data[index2] >> 7 - index3 & 1) * 3;
            if (num1 < length)
            {
              if (isARGB)
              {
                if (index4 == 0)
                {
                  byte[] numArray1 = flat;
                  int index5 = num1;
                  int num4 = index5 + 1;
                  int num5 = (int) index[index4];
                  numArray1[index5] = (byte) num5;
                  byte[] numArray2 = flat;
                  int index6 = num4;
                  int num6 = index6 + 1;
                  int num7 = (int) index[index4 + 1];
                  numArray2[index6] = (byte) num7;
                  byte[] numArray3 = flat;
                  int index7 = num6;
                  int num8 = index7 + 1;
                  int num9 = (int) index[index4 + 2];
                  numArray3[index7] = (byte) num9;
                  byte[] numArray4 = flat;
                  int index8 = num8;
                  num1 = index8 + 1;
                  numArray4[index8] = byte.MaxValue;
                }
                else
                {
                  byte[] numArray5 = flat;
                  int index9 = num1;
                  int num10 = index9 + 1;
                  int num11 = (int) index[index4];
                  numArray5[index9] = (byte) num11;
                  byte[] numArray6 = flat;
                  int index10 = num10;
                  int num12 = index10 + 1;
                  int num13 = (int) index[index4 + 1];
                  numArray6[index10] = (byte) num13;
                  byte[] numArray7 = flat;
                  int index11 = num12;
                  int num14 = index11 + 1;
                  int num15 = (int) index[index4 + 2];
                  numArray7[index11] = (byte) num15;
                  byte[] numArray8 = flat;
                  int index12 = num14;
                  num1 = index12 + 1;
                  numArray8[index12] = (byte) 0;
                }
              }
              else
              {
                byte[] numArray9 = flat;
                int index13 = num1;
                int num16 = index13 + 1;
                int num17 = (int) index[index4];
                numArray9[index13] = (byte) num17;
                byte[] numArray10 = flat;
                int index14 = num16;
                int num18 = index14 + 1;
                int num19 = (int) index[index4 + 1];
                numArray10[index14] = (byte) num19;
                byte[] numArray11 = flat;
                int index15 = num18;
                num1 = index15 + 1;
                int num20 = (int) index[index4 + 2];
                numArray11[index15] = (byte) num20;
              }
              ++num3;
              if (num3 == w)
              {
                num3 = 0;
                index3 = 8;
              }
            }
            else
              break;
          }
        }
        break;
      case 2:
        int[] numArray12 = new int[4]{ 6, 4, 2, 0 };
        int num21 = 0;
        for (int index16 = 0; index16 < data.Length; ++index16)
        {
          for (int index17 = 0; index17 < 4; ++index17)
          {
            int index18 = ((int) data[index16] >> numArray12[index17] & 3) * 3;
            if (num1 < length)
            {
              byte[] numArray13 = flat;
              int index19 = num1;
              int num22 = index19 + 1;
              int num23 = (int) index[index18];
              numArray13[index19] = (byte) num23;
              byte[] numArray14 = flat;
              int index20 = num22;
              int num24 = index20 + 1;
              int num25 = (int) index[index18 + 1];
              numArray14[index20] = (byte) num25;
              byte[] numArray15 = flat;
              int index21 = num24;
              num1 = index21 + 1;
              int num26 = (int) index[index18 + 2];
              numArray15[index21] = (byte) num26;
              if (isARGB)
                flat[num1++] = index18 != 0 ? (byte) 0 : (byte) 0;
              ++num21;
              if (num21 == w)
              {
                num21 = 0;
                index17 = 8;
              }
            }
            else
              break;
          }
        }
        return flat;
      case 4:
        int[] numArray16 = new int[2]{ 4, 0 };
        int num27 = 0;
        for (int index22 = 0; index22 < data.Length; ++index22)
        {
          for (int index23 = 0; index23 < 2; ++index23)
          {
            int index24 = ((int) data[index22] >> numArray16[index23] & 15) * 3;
            if (num1 < length)
            {
              byte[] numArray17 = flat;
              int index25 = num1;
              int num28 = index25 + 1;
              int num29 = (int) index[index24];
              numArray17[index25] = (byte) num29;
              byte[] numArray18 = flat;
              int index26 = num28;
              int num30 = index26 + 1;
              int num31 = (int) index[index24 + 1];
              numArray18[index26] = (byte) num31;
              byte[] numArray19 = flat;
              int index27 = num30;
              num1 = index27 + 1;
              int num32 = (int) index[index24 + 2];
              numArray19[index27] = (byte) num32;
              if (isARGB)
                flat[num1++] = index24 != 0 ? (byte) 0 : (byte) 0;
              ++num27;
              if (num27 == w)
              {
                num27 = 0;
                index23 = 8;
              }
            }
            else
              break;
          }
        }
        return flat;
      case 8:
        for (int index28 = 0; index28 < data.Length - 1; ++index28)
        {
          if (isDownsampled)
            num2 = (float) ((int) data[index28] & (int) byte.MaxValue) / (float) byte.MaxValue;
          else
            index1 = ((int) data[index28] & (int) byte.MaxValue) * 3;
          if (num1 >= length)
            return flat;
          if (isDownsampled)
          {
            if ((double) num2 > 0.0)
            {
              byte[] numArray20 = flat;
              int index29 = num1;
              int num33 = index29 + 1;
              int num34 = (int) (byte) ((double) ((int) byte.MaxValue - (int) index[0]) * (double) num2);
              numArray20[index29] = (byte) num34;
              byte[] numArray21 = flat;
              int index30 = num33;
              int num35 = index30 + 1;
              int num36 = (int) (byte) ((double) ((int) byte.MaxValue - (int) index[1]) * (double) num2);
              numArray21[index30] = (byte) num36;
              byte[] numArray22 = flat;
              int index31 = num35;
              num1 = index31 + 1;
              int num37 = (int) (byte) ((double) ((int) byte.MaxValue - (int) index[2]) * (double) num2);
              numArray22[index31] = (byte) num37;
            }
            else
              num1 += 3;
          }
          else if (index1 < indexLength)
          {
            byte[] numArray23 = flat;
            int index32 = num1;
            int num38 = index32 + 1;
            int num39 = (int) index[index1];
            numArray23[index32] = (byte) num39;
            byte[] numArray24 = flat;
            int index33 = num38;
            int num40 = index33 + 1;
            int num41 = (int) index[index1 + 1];
            numArray24[index33] = (byte) num41;
            byte[] numArray25 = flat;
            int index34 = num40;
            num1 = index34 + 1;
            int num42 = (int) index[index1 + 2];
            numArray25[index34] = (byte) num42;
          }
          if (isARGB)
            flat[num1++] = index1 != 0 || (double) num2 != 0.0 ? (byte) 0 : byte.MaxValue;
        }
        return flat;
    }
    return flat;
  }

  private byte ConvertToByte(float value)
  {
    if ((double) value <= 0.0)
      return 0;
    if ((double) value <= 0.0031308)
      return (byte) ((double) byte.MaxValue * (double) value * 12.920000076293945 + 0.5);
    return (double) value < 1.0 ? (byte) ((double) byte.MaxValue * (1.0549999475479126 * Math.Pow((double) value, 5.0 / 12.0) - 0.054999999701976776) + 0.5) : byte.MaxValue;
  }

  private float ConvertToFloat(byte byteValue)
  {
    float num = (float) byteValue / (float) byte.MaxValue;
    if ((double) num <= 0.0)
      return 0.0f;
    if ((double) num <= 0.04045)
      return num / 12.92f;
    return (double) num < 1.0 ? (float) Math.Pow(((double) num + 0.055) / 1.055, 2.4) : 1f;
  }

  public Image Decode()
  {
    try
    {
      ImagePreRenderEventArgs args = new ImagePreRenderEventArgs(this);
      if (ImageStructure.ImagePreRender == null)
        return Image.FromStream(this.GetImageStream());
      ImageStructure.ImagePreRender((object) null, args);
      return Image.FromStream(args.ImageStream);
    }
    catch (Exception ex)
    {
      return (Image) null;
    }
  }

  private MemoryStream DecodeASCII85Stream(MemoryStream encodedStream)
  {
    byte[] buffer = new ASCII85().decode(encodedStream.GetBuffer());
    MemoryStream memoryStream = new MemoryStream(buffer, 0, buffer.Length, true, true);
    memoryStream.Position = 0L;
    return memoryStream;
  }

  private MemoryStream DecodeDeviceGrayImage(MemoryStream imageStr)
  {
    imageStr.GetBuffer();
    Bitmap bitmap = new Bitmap((int) this.Width, (int) this.Height, PixelFormat.Format32bppArgb);
    int bitsPerComponent = (int) this.BitsPerComponent;
    long length = imageStr.Length;
    for (int y = 0; y < (int) this.Height; ++y)
    {
      int num1 = 0;
      int num2 = 0;
      for (int x = 0; x < (int) this.Width; ++x)
      {
        if (num2 < bitsPerComponent)
        {
          num1 = num1 << 8 | imageStr.ReadByte();
          num2 += 8;
        }
        byte num3 = (byte) (num1 >> num2 - bitsPerComponent);
        num1 ^= (int) num3 << num2 - bitsPerComponent;
        num2 -= bitsPerComponent;
        Color color = Color.FromArgb((int) byte.MaxValue, (int) num3, (int) num3, (int) num3);
        bitmap.SetPixel(x, y, color);
      }
    }
    MemoryStream memoryStream = new MemoryStream();
    bitmap.Save((Stream) memoryStream, ImageFormat.Jpeg);
    return memoryStream;
  }

  private MemoryStream DecodeFlateStream(MemoryStream encodedStream)
  {
    encodedStream.Position = 0L;
    encodedStream.ReadByte();
    encodedStream.ReadByte();
    DeflateStream deflateStream = new DeflateStream((Stream) encodedStream, CompressionMode.Decompress, true);
    byte[] buffer = new byte[4096 /*0x1000*/];
    MemoryStream memoryStream = new MemoryStream();
    while (true)
    {
      int count = deflateStream.Read(buffer, 0, 4096 /*0x1000*/);
      if (count > 0)
        memoryStream.Write(buffer, 0, count);
      else
        break;
    }
    return memoryStream;
  }

  private Bitmap DecodeMaskImage(MemoryStream mask)
  {
    PixelFormat format1 = PixelFormat.Format8bppIndexed;
    if ((double) this.m_maskBitsPerComponent == 1.0)
    {
      PixelFormat format2 = PixelFormat.Format1bppIndexed;
      byte[] buffer = mask.GetBuffer();
      Bitmap bitmap = new Bitmap((int) this.m_maskWidth, (int) this.m_maskHeight, format2);
      BitmapData bitmapdata = bitmap.LockBits(new Rectangle(0, 0, (int) this.m_maskWidth, (int) this.m_maskHeight), ImageLockMode.ReadWrite, bitmap.PixelFormat);
      int stride = bitmapdata.Stride;
      IntPtr scan0 = bitmapdata.Scan0;
      int pixelFormatSize = Image.GetPixelFormatSize(bitmap.PixelFormat);
      int length = (int) this.m_maskWidth * pixelFormatSize / 8;
      if ((int) this.m_maskWidth * pixelFormatSize % 8 != 0)
        ++length;
      int startIndex = 0;
      long int64 = bitmapdata.Scan0.ToInt64();
      for (int index = 0; (double) index < (double) this.m_maskHeight; ++index)
      {
        Marshal.Copy(buffer, startIndex, new IntPtr(int64), length);
        startIndex += length;
        int64 += (long) bitmapdata.Stride;
      }
      bitmap.UnlockBits(bitmapdata);
      return bitmap;
    }
    byte[] buffer1 = mask.GetBuffer();
    ColorPalette palette = new Bitmap((int) this.m_maskWidth, (int) this.m_maskHeight, format1).Palette;
    for (int index = 0; index < palette.Entries.Length; ++index)
      palette.Entries[index] = Color.FromArgb(index, index, index);
    Bitmap bitmap1 = new Bitmap((int) this.m_maskWidth, (int) this.m_maskHeight, format1);
    BitmapData bitmapdata1 = bitmap1.LockBits(new Rectangle(0, 0, (int) this.m_maskWidth, (int) this.m_maskHeight), ImageLockMode.ReadWrite, format1);
    Math.Abs(bitmapdata1.Stride);
    int height = bitmap1.Height;
    if (format1 == PixelFormat.Format8bppIndexed)
    {
      int startIndex = 0;
      long int64 = bitmapdata1.Scan0.ToInt64();
      for (int index = 0; (double) index < (double) this.m_maskHeight; ++index)
      {
        Marshal.Copy(buffer1, startIndex, new IntPtr(int64), (int) this.m_maskWidth);
        startIndex += (int) this.m_maskWidth;
        int64 += (long) bitmapdata1.Stride;
      }
    }
    else
      Marshal.Copy(buffer1, 0, bitmapdata1.Scan0, buffer1.Length);
    bitmap1.Palette = palette;
    bitmap1.UnlockBits(bitmapdata1);
    return bitmap1;
  }

  private MemoryStream DecodePredictor(int predictor, int colors, int columns, MemoryStream data)
  {
    MemoryStream memoryStream = new MemoryStream();
    if (predictor == 1)
      return data;
    int offset1 = (int) (((double) colors * (double) this.BitsPerComponent + 7.0) / 8.0);
    int length = (int) (((double) (columns * colors) * (double) this.BitsPerComponent + 7.0) / 8.0) + offset1;
    byte[] buffer1 = new byte[length];
    byte[] numArray = new byte[length];
    int num1 = predictor;
    bool flag = false;
    data.Position = 0L;
    while (!flag && data.Position < data.Length)
    {
      if (predictor >= 10)
      {
        byte[] buffer2 = new byte[1];
        data.Read(buffer2, 0, 1);
        int num2 = (int) buffer2[0];
        if (num2 == -1)
          return memoryStream;
        num1 = num2 + 10;
      }
      int offset2 = offset1;
      int num3;
      while (offset2 < length && (num3 = data.Read(buffer1, offset2, length - offset2)) != -1)
        offset2 += num3;
      switch (num1)
      {
        case 2:
          for (int index = offset1; index < length; ++index)
          {
            int num4 = (int) buffer1[index] & (int) byte.MaxValue;
            int num5 = (int) buffer1[index - offset1] & (int) byte.MaxValue;
            buffer1[index] = (byte) (num4 + num5);
          }
          break;
        case 11:
          for (int index = offset1; index < length; ++index)
          {
            int num6 = (int) buffer1[index] & (int) byte.MaxValue;
            int num7 = (int) buffer1[index - offset1] & (int) byte.MaxValue;
            buffer1[index] = (byte) (num6 + num7);
          }
          break;
        case 12:
          for (int index = offset1; index < length; ++index)
          {
            int num8 = (int) buffer1[index] & (int) byte.MaxValue;
            int num9 = (int) numArray[index] & (int) byte.MaxValue;
            buffer1[index] = (byte) (num8 + num9);
          }
          break;
        case 13:
          for (int index = offset1; index < length; ++index)
          {
            int num10 = (int) buffer1[index] & (int) byte.MaxValue;
            int num11 = (int) buffer1[index - offset1] & (int) byte.MaxValue;
            int num12 = (int) numArray[index] & (int) byte.MaxValue;
            buffer1[index] = (byte) (num10 + (num11 + num12) / 2);
          }
          break;
        case 14:
          for (int index = offset1; index < length; ++index)
          {
            int num13 = (int) buffer1[index] & (int) byte.MaxValue;
            int num14 = (int) buffer1[index - offset1] & (int) byte.MaxValue;
            int num15 = (int) numArray[index] & (int) byte.MaxValue;
            int num16 = (int) numArray[index - offset1] & (int) byte.MaxValue;
            int num17 = num14 + num15 - num16;
            int num18 = Math.Abs(num17 - num14);
            int num19 = Math.Abs(num17 - num15);
            int num20 = Math.Abs(num17 - num16);
            buffer1[index] = num18 > num19 || num18 > num20 ? (num19 > num20 ? (byte) (num13 + num16) : (byte) (num13 + num15)) : (byte) (num13 + num14);
          }
          break;
      }
      numArray = (byte[]) buffer1.Clone();
      memoryStream.Write(buffer1, offset1, buffer1.Length - offset1);
    }
    return memoryStream;
  }

  private static byte[] GetAsciiBytes(string value)
  {
    byte[] asciiBytes = value != null ? new byte[value.Length] : throw new ArgumentNullException(nameof (value));
    int index = 0;
    for (int length = value.Length; index < length; ++index)
      asciiBytes[index] = (byte) value[index];
    return asciiBytes;
  }

  private float GetBitsPerComponent()
  {
    float bitsPerComponent = 0.0f;
    if (this.m_imageDictionary != null)
      bitsPerComponent = (this.m_imageDictionary["BitsPerComponent"] as PdfNumber).FloatValue;
    return bitsPerComponent;
  }

  private void GetColorSpace()
  {
    if (!this.m_imageDictionary.ContainsKey("ColorSpace"))
      return;
    string[] filter = (string[]) null;
    string internalColorSpace = (string) null;
    PdfDictionary colorspaceDictionary = (PdfDictionary) null;
    PdfArray pdfArray1 = (PdfArray) null;
    if (this.m_imageDictionary["ColorSpace"] is PdfArray)
      pdfArray1 = this.m_imageDictionary["ColorSpace"] as PdfArray;
    if ((object) (this.m_imageDictionary["ColorSpace"] as PdfReferenceHolder) != null)
      pdfArray1 = (this.m_imageDictionary["ColorSpace"] as PdfReferenceHolder).Object as PdfArray;
    if ((object) (this.m_imageDictionary["ColorSpace"] as PdfName) != null)
      this.m_colorspace = (this.m_imageDictionary["ColorSpace"] as PdfName).Value;
    if (pdfArray1 == null)
      return;
    this.m_colorspace = (pdfArray1[0] as PdfName).Value;
    PdfArray pdfArray2 = pdfArray1;
    if (pdfArray1.Count == 4)
    {
      foreach (string str in this.ImageFilter)
      {
        int num = str == "RunLengthDecode" ? 1 : 0;
      }
      if (!((pdfArray2[0] as PdfName).Value != "Indexed"))
      {
        if (!(pdfArray2[pdfArray2.Count - 1].GetType().Name != "PdfReferenceHolder"))
        {
          try
          {
            if (((pdfArray2[pdfArray2.Count - 1] as PdfReferenceHolder).Object as PdfDictionary).Values.Count > 1)
            {
              this.colorSpaceResourceDict.Add("Indexed", this.DecodeFlateStream(((pdfArray2[pdfArray2.Count - 1] as PdfReferenceHolder).Object as PdfStream).InternalStream));
              this.isIndexedImage = true;
            }
            else
            {
              this.colorSpaceResourceDict.Add("Indexed", ((pdfArray2[pdfArray2.Count - 1] as PdfReferenceHolder).Object as PdfStream).InternalStream);
              if (this.ImageDictionary.ContainsKey("DecodeParms"))
                this.GetIndexedColorSpace(pdfArray1, internalColorSpace, colorspaceDictionary, filter);
              this.isIndexedImage = true;
            }
            if (pdfArray2[pdfArray2.Count - 3].GetType().Name == "PdfName")
            {
              this.colorSpaceResourceDict.Add((pdfArray2[pdfArray2.Count - 3] as PdfName).Value, new MemoryStream());
              return;
            }
            if ((object) (pdfArray2[pdfArray2.Count - 3] as PdfReferenceHolder) == null)
              return;
            PdfArray pdfArray3 = (pdfArray2[pdfArray2.Count - 3] as PdfReferenceHolder).Object as PdfArray;
            if ((object) (pdfArray3[0] as PdfName) == null || !((pdfArray3[0] as PdfName).Value == "DeviceN"))
              return;
            this.isDeviceN = true;
            return;
          }
          catch
          {
            this.isIndexedImage = false;
            this.GetIndexedColorSpace(pdfArray1, internalColorSpace, colorspaceDictionary, filter);
            return;
          }
        }
      }
      this.isIndexedImage = false;
      this.GetIndexedColorSpace(pdfArray1, internalColorSpace, colorspaceDictionary, filter);
    }
    else
    {
      this.isIndexedImage = false;
      this.GetIndexedColorSpace(pdfArray1, internalColorSpace, colorspaceDictionary, filter);
    }
  }

  private PdfDictionary[] GetDecodeParam()
  {
    PdfDictionary[] decodeParam = (PdfDictionary[]) null;
    if (this.m_imageDictionary != null && this.m_imageDictionary.ContainsKey("DecodeParms"))
    {
      if (this.m_imageDictionary["DecodeParms"] is PdfDictionary)
        return new PdfDictionary[1]
        {
          this.m_imageDictionary["DecodeParms"] as PdfDictionary
        };
      if (!(this.m_imageDictionary["DecodeParms"] is PdfArray))
        return decodeParam;
      PdfArray image = this.m_imageDictionary["DecodeParms"] as PdfArray;
      decodeParam = new PdfDictionary[image.Count];
      for (int index = 0; index < image.Count; ++index)
        decodeParam[index] = image[index] as PdfDictionary;
    }
    return decodeParam;
  }

  private byte[] GetDeviceNData(byte[] data)
  {
    byte[] numArray1 = data;
    byte[] deviceNdata = new byte[3 * (int) this.Width * (int) this.Height];
    int index1 = 0;
    int length1 = numArray1.Length;
    int length2 = 1;
    float[] numArray2 = new float[length2];
    for (int index2 = 0; index2 < length1; index2 += length2)
    {
      for (int index3 = 0; index3 < length2; ++index3)
        numArray2[index3] = (float) ((int) numArray1[index2 + index3] & (int) byte.MaxValue) / (float) byte.MaxValue;
      float num1 = 0.0f;
      float num2 = 0.0f;
      float num3 = 0.0f;
      float num4 = numArray2[0];
      float num5 = (float) ((double) byte.MaxValue * (1.0 - (double) num1) * (1.0 - (double) num4));
      float num6 = (float) ((double) byte.MaxValue * (1.0 - (double) num2) * (1.0 - (double) num4));
      float num7 = (float) ((double) byte.MaxValue * (1.0 - (double) num3) * (1.0 - (double) num4));
      deviceNdata[index1] = (byte) num5;
      int index4 = index1 + 1;
      deviceNdata[index4] = (byte) num6;
      int index5 = index4 + 1;
      deviceNdata[index5] = (byte) num7;
      index1 = index5 + 1;
    }
    return deviceNdata;
  }

  private string[] GetImageFilter()
  {
    string[] imageFilter1 = (string[]) null;
    if (this.m_imageDictionary != null && this.m_imageDictionary.ContainsKey("Filter"))
    {
      if ((object) (this.m_imageDictionary["Filter"] as PdfName) != null)
        return new string[1]
        {
          (this.m_imageDictionary["Filter"] as PdfName).Value
        };
      if (this.m_imageDictionary["Filter"] is PdfArray)
      {
        PdfArray image = this.m_imageDictionary["Filter"] as PdfArray;
        string[] imageFilter2 = new string[image.Count];
        for (int index = 0; index < image.Count; ++index)
          imageFilter2[index] = (image[index] as PdfName).Value;
        return imageFilter2;
      }
      if ((object) (this.m_imageDictionary["Filter"] as PdfReferenceHolder) == null)
        return imageFilter1;
      PdfArray pdfArray = (this.m_imageDictionary["Filter"] as PdfReferenceHolder).Object as PdfArray;
      imageFilter1 = new string[pdfArray.Count];
      for (int index = 0; index < pdfArray.Count; ++index)
        imageFilter1[index] = (pdfArray[index] as PdfName).Value;
    }
    return imageFilter1;
  }

  private float GetImageHeight()
  {
    float imageHeight = 0.0f;
    if (this.m_imageDictionary == null || !this.m_imageDictionary.ContainsKey("Height"))
      return imageHeight;
    return (object) (this.m_imageDictionary["Height"] as PdfReferenceHolder) != null ? ((this.m_imageDictionary["Height"] as PdfReferenceHolder).Object as PdfNumber).FloatValue : (this.m_imageDictionary["Height"] as PdfNumber).FloatValue;
  }

  public Stream GetImageStream()
  {
    this.m_isImageStreamParsed = true;
    bool flag1 = true;
    if (this.ImageFilter == null)
    {
      this.m_imageFilter = new string[1]{ "FlateDecode" };
      flag1 = false;
    }
    if (this.ImageFilter == null)
      return (Stream) null;
    IntPtr scan0_1;
    for (int index1 = 0; index1 < this.ImageFilter.Length; ++index1)
    {
      switch (this.ImageFilter[index1])
      {
        case "A85":
        case "ASCII85Decode":
          this.ImageStream = (Stream) this.DecodeASCII85Stream(this.ImageStream as MemoryStream);
          this.ImageStream.Position = 0L;
          break;
        case "ASCIIHex":
          this.ImageStream = (Stream) new MemoryStream(new ASCIIHex().Decode((this.ImageStream as MemoryStream).GetBuffer()));
          this.ImageStream.Position = 0L;
          break;
        case "CCITTFaxDecode":
          PdfDictionary pdfDictionary1 = new PdfDictionary();
          if (this.m_imageDictionary.ContainsKey("DecodeParms"))
            pdfDictionary1 = this.DecodeParam[index1];
          TiffDecode tiffDecode = new TiffDecode();
          tiffDecode.m_tiffHeader.m_byteOrder = (short) 18761;
          tiffDecode.m_tiffHeader.m_version = (short) 42;
          tiffDecode.m_tiffHeader.m_dirOffset = (uint) ((ulong) (this.ImageStream.Length + 8L) + 1UL);
          tiffDecode.WriteHeader(tiffDecode.m_tiffHeader);
          tiffDecode.m_stream.Seek(8L, SeekOrigin.Begin);
          tiffDecode.m_stream.Write((this.ImageStream as MemoryStream).GetBuffer(), 0, (int) this.ImageStream.Length);
          tiffDecode.SetField(1, (int) this.Width, TiffTag.ImageWidth, TiffType.Short);
          tiffDecode.SetField(1, (int) this.Height, TiffTag.ImageLength, TiffType.Short);
          tiffDecode.SetField(1, 1, TiffTag.BitsPerSample, TiffType.Short);
          if (pdfDictionary1 != null && pdfDictionary1.ContainsKey("K"))
          {
            if ((pdfDictionary1["K"] as PdfNumber).IntValue < 0)
              tiffDecode.SetField(1, 4, TiffTag.Compression, TiffType.Short);
            else if ((pdfDictionary1["K"] as PdfNumber).IntValue == 0)
            {
              if (pdfDictionary1.ContainsKey("EndOfBlock"))
              {
                if (!(pdfDictionary1["EndOfBlock"] as PdfBoolean).Value)
                  tiffDecode.SetField(1, 2, TiffTag.Compression, TiffType.Short);
              }
              else
                tiffDecode.SetField(1, 3, TiffTag.Compression, TiffType.Short);
            }
            else
              tiffDecode.SetField(1, 3, TiffTag.Compression, TiffType.Short);
            if (pdfDictionary1.ContainsKey("BlackIs1"))
            {
              if ((pdfDictionary1["BlackIs1"] as PdfBoolean).Value)
                tiffDecode.SetField(1, 1, TiffTag.Photometric, TiffType.Short);
              else
                tiffDecode.SetField(1, 0, TiffTag.Photometric, TiffType.Short);
            }
          }
          else
            tiffDecode.SetField(1, 3, TiffTag.Compression, TiffType.Short);
          tiffDecode.SetField(1, 8, TiffTag.StripOffset, TiffType.Long);
          tiffDecode.SetField(1, 1, TiffTag.SamplesPerPixel, TiffType.Short);
          tiffDecode.SetField(1, (int) this.ImageStream.Length, TiffTag.StripByteCounts, TiffType.Long);
          tiffDecode.m_stream.Seek(9L + this.ImageStream.Length, SeekOrigin.Begin);
          tiffDecode.WriteDirEntry(tiffDecode.directoryEntries);
          tiffDecode.m_stream.Position = 0L;
          tiffDecode.m_stream.Capacity = (int) tiffDecode.m_stream.Length;
          if (!this.ImageDictionary.ContainsKey("ImageMask"))
          {
            this.ImageStream = (Stream) tiffDecode.m_stream;
            this.ImageStream.Position = 0L;
            break;
          }
          this.m_isImageMask = (this.ImageDictionary["ImageMask"] as PdfBoolean).Value;
          if (this.m_isImageMask)
          {
            Bitmap bitmap = new Bitmap((Stream) tiffDecode.m_stream);
            bitmap.MakeTransparent(Color.White);
            MemoryStream memoryStream = new MemoryStream();
            bitmap.Save((Stream) memoryStream, ImageFormat.Png);
            this.ImageStream = (Stream) memoryStream;
            this.ImageStream.Position = 0L;
            break;
          }
          this.ImageStream = (Stream) tiffDecode.m_stream;
          this.ImageStream.Position = 0L;
          break;
        case "DCTDecode":
          if (!this.m_imageDictionary.ContainsKey("SMask"))
          {
            this.ImageStream.Position = 0L;
            if (this.ColorSpace == "DeviceCMYK")
            {
              if (this.m_imageDictionary.ContainsKey("Decode"))
              {
                PdfArray image = this.m_imageDictionary["Decode"] as PdfArray;
                PdfArray pdfArray = new PdfArray(new double[8]
                {
                  1.0,
                  0.0,
                  1.0,
                  0.0,
                  1.0,
                  0.0,
                  1.0,
                  0.0
                });
                bool flag2 = true;
                for (int index2 = 0; index2 < pdfArray.Count; ++index2)
                {
                  if ((double) (image[index2] as PdfNumber).FloatValue != (double) (pdfArray[index2] as PdfNumber).FloatValue)
                    flag2 = false;
                }
                if (flag2)
                  break;
              }
              Bitmap bitmap1 = Image.FromStream(this.ImageStream) as Bitmap;
              BitmapData bitmapdata = bitmap1.LockBits(new Rectangle(0, 0, bitmap1.Width, bitmap1.Height), ImageLockMode.ReadWrite, bitmap1.PixelFormat);
              IntPtr scan0_2 = bitmapdata.Scan0;
              int length = Math.Abs(bitmapdata.Stride) * bitmap1.Height;
              byte[] numArray = new byte[length];
              Marshal.Copy(scan0_2, numArray, 0, length);
              byte[] source = this.YCCKtoRGB(numArray);
              Marshal.Copy(source, 0, scan0_2, source.Length);
              bitmap1.UnlockBits(bitmapdata);
              Bitmap bitmap2 = bitmap1;
              this.ImageStream = (Stream) new MemoryStream();
              Stream imageStream = this.ImageStream;
              ImageFormat jpeg = ImageFormat.Jpeg;
              bitmap2.Save(imageStream, jpeg);
              this.ImageStream.Position = 0L;
              break;
            }
            break;
          }
          try
          {
            Bitmap input;
            try
            {
              this.ImageStream.Position = 0L;
              if (this.ColorSpace == "DeviceCMYK")
              {
                if (this.m_imageDictionary.ContainsKey("Decode"))
                {
                  PdfArray image = this.m_imageDictionary["Decode"] as PdfArray;
                  PdfArray pdfArray = new PdfArray(new double[8]
                  {
                    1.0,
                    0.0,
                    1.0,
                    0.0,
                    1.0,
                    0.0,
                    1.0,
                    0.0
                  });
                  bool flag3 = true;
                  for (int index3 = 0; index3 < pdfArray.Count; ++index3)
                  {
                    if ((double) (image[index3] as PdfNumber).FloatValue != (double) (pdfArray[index3] as PdfNumber).FloatValue)
                      flag3 = false;
                  }
                  if (flag3)
                    break;
                }
                Bitmap bitmap3 = Image.FromStream(this.ImageStream) as Bitmap;
                BitmapData bitmapdata = bitmap3.LockBits(new Rectangle(0, 0, bitmap3.Width, bitmap3.Height), ImageLockMode.ReadWrite, bitmap3.PixelFormat);
                IntPtr scan0_3 = bitmapdata.Scan0;
                int length = Math.Abs(bitmapdata.Stride) * bitmap3.Height;
                byte[] numArray = new byte[length];
                Marshal.Copy(scan0_3, numArray, 0, length);
                byte[] source = this.YCCKtoRGB(numArray);
                Marshal.Copy(source, 0, scan0_3, source.Length);
                bitmap3.UnlockBits(bitmapdata);
                Bitmap bitmap4 = bitmap3;
                this.ImageStream = (Stream) new MemoryStream();
                Stream imageStream = this.ImageStream;
                ImageFormat jpeg = ImageFormat.Jpeg;
                bitmap4.Save(imageStream, jpeg);
                this.ImageStream.Position = 0L;
              }
              this.ImageStream.Position = 0L;
              input = Image.FromStream(this.ImageStream) as Bitmap;
            }
            catch
            {
              input = (Bitmap) null;
            }
            this.MaskStream.Position = 0L;
            this.ImageStream = (Stream) this.MergeImages(input, this.MaskStream as MemoryStream);
            this.ImageStream.Position = 0L;
            break;
          }
          catch
          {
            this.ImageStream.Position = 0L;
            break;
          }
        case "FlateDecode":
          int predictor = 0;
          int colors = 1;
          int columns = 1;
          MemoryStream memoryStream1 = !flag1 ? this.ImageStream as MemoryStream : this.DecodeFlateStream(this.ImageStream as MemoryStream);
          string colorSpace = this.ColorSpace;
          byte[] numArray1 = (byte[]) null;
          if (this.colorSpaceResourceDict.Count > 0)
          {
            int d = 0;
            int w = 0;
            int h = 0;
            this.isIndexedImage = true;
            if (this.m_imageDictionary.ContainsKey("BitsPerComponent"))
              d = (this.m_imageDictionary["BitsPerComponent"] as PdfNumber).IntValue;
            if (this.m_imageDictionary.ContainsKey("Width"))
              w = (this.m_imageDictionary["Width"] as PdfNumber).IntValue;
            if (this.m_imageDictionary.ContainsKey("Height"))
              h = (this.m_imageDictionary["Height"] as PdfNumber).IntValue;
            if (this.colorSpaceResourceDict.ContainsKey("DeviceCMYK"))
            {
              byte[] rgb = this.ConvertIndexCMYKToRGB(this.colorSpaceResourceDict["Indexed"].GetBuffer());
              numArray1 = this.ConvertIndexedStreamToFlat(d, w, h, memoryStream1.GetBuffer(), rgb, false, false);
            }
            else if (this.isDeviceN)
            {
              byte[] buffer = this.colorSpaceResourceDict["Indexed"].GetBuffer();
              byte[] index4 = new byte[768 /*0x0300*/];
              int index5 = 0;
              int length1 = buffer.Length;
              int length2 = 2;
              float[] numArray2 = new float[length2];
              for (int index6 = 0; index6 < length1; index6 += length2)
              {
                for (int index7 = 0; index7 < length2; ++index7)
                  numArray2[index7] = (float) ((int) buffer[index6 + index7] & (int) byte.MaxValue) / (float) byte.MaxValue;
                float num1 = numArray2[0];
                float num2 = 0.0f;
                float num3 = 0.0f;
                float num4 = numArray2[1];
                float num5 = (float) ((double) byte.MaxValue * (1.0 - (double) num1) * (1.0 - (double) num4));
                float num6 = (float) ((double) byte.MaxValue * (1.0 - (double) num2) * (1.0 - (double) num4));
                float num7 = (float) ((double) byte.MaxValue * (1.0 - (double) num3) * (1.0 - (double) num4));
                index4[index5] = (byte) num5;
                int index8 = index5 + 1;
                index4[index8] = (byte) num6;
                int index9 = index8 + 1;
                index4[index9] = (byte) num7;
                index5 = index9 + 1;
              }
              numArray1 = this.ConvertIndexedStreamToFlat(d, w, h, memoryStream1.GetBuffer(), index4, false, false);
            }
            else
              numArray1 = this.ConvertIndexedStreamToFlat(d, w, h, memoryStream1.GetBuffer(), this.colorSpaceResourceDict["Indexed"].GetBuffer(), false, false);
          }
          if (this.ColorSpace == "DeviceGray")
          {
            memoryStream1.Position = 0L;
            if (this.ImageFilter.Length > 1 && index1 == 0 && (this.ImageFilter[index1 + 1] == "DCTDecode" || this.ImageFilter[index1 + 1] == "RunLengthDecode"))
            {
              this.ImageStream = (Stream) memoryStream1;
              this.ImageStream.Position = 0L;
              break;
            }
            this.ImageStream = (Stream) this.DecodeDeviceGrayImage(memoryStream1);
            if (this.m_imageDictionary.ContainsKey("SMask"))
            {
              Bitmap input = new Bitmap(this.ImageStream);
              try
              {
                this.ImageStream = (Stream) this.MergeImages(input, this.MaskStream as MemoryStream);
              }
              catch (Exception ex)
              {
                this.ImageStream = (Stream) memoryStream1;
              }
            }
            this.ImageStream.Position = 0L;
            break;
          }
          if (this.ImageFilter.Length > 1 && index1 == 0 && (this.ImageFilter[index1 + 1] == "DCTDecode" || this.ImageFilter[index1 + 1] == "RunLengthDecode"))
          {
            this.ImageStream = (Stream) new MemoryStream();
            this.ImageStream = (Stream) memoryStream1;
            this.ImageStream.Position = 0L;
            break;
          }
          if (!this.isIndexedImage)
          {
            if (this.nonIndexedImageColorResource != null && this.nonIndexedImageColorResource.Count > 0)
            {
              PdfDictionary pdfDictionary2 = (PdfDictionary) this.nonIndexedImageColorResource["ICCBased"];
              if (pdfDictionary2["N"] is PdfNumber)
              {
                if ((pdfDictionary2["N"] as PdfNumber).IntValue == 1)
                {
                  int num = 0;
                  if (this.m_imageDictionary.ContainsKey("BitsPerComponent"))
                    num = (this.m_imageDictionary["BitsPerComponent"] as PdfNumber).IntValue;
                  if (num == 8)
                    this.m_pixelFormat = PixelFormat.Format8bppIndexed;
                  numArray1 = memoryStream1.GetBuffer();
                  for (int index10 = 0; index10 < numArray1.Length; ++index10)
                  {
                    if (numArray1[index10] != (byte) 0 && numArray1[index10] != byte.MaxValue)
                      numArray1[index10] = (byte) 0;
                  }
                }
                else
                  numArray1 = memoryStream1.GetBuffer();
              }
              else
                numArray1 = memoryStream1.GetBuffer();
            }
            else if (this.ColorSpace == "DeviceCMYK")
            {
              numArray1 = this.YCCToRGB(memoryStream1.GetBuffer());
              memoryStream1 = new MemoryStream(numArray1);
            }
            else
              numArray1 = memoryStream1.GetBuffer();
          }
          if (this.ImageDictionary.ContainsKey("Mask"))
          {
            this.IsTransparent = true;
            if (this.ImageDictionary["Mask"] is PdfArray)
            {
              PdfArray image = this.ImageDictionary["Mask"] as PdfArray;
              int index11 = 0;
              while (index11 < image.Count)
              {
                int num8 = 0;
                int num9 = 0;
                if (image[index11] is PdfNumber)
                {
                  num9 = (image[index11] as PdfNumber).IntValue;
                  ++index11;
                }
                if (image[index11] is PdfNumber)
                {
                  num8 = (image[index11] as PdfNumber).IntValue;
                  ++index11;
                }
                for (int index12 = 0; index12 < numArray1.Length; ++index12)
                {
                  if ((int) numArray1[index12] >= num9 && (int) numArray1[index12] <= num8)
                    numArray1[index12] = byte.MaxValue;
                }
              }
            }
          }
          if (this.ColorSpace == "DeviceN")
            numArray1 = this.GetDeviceNData(numArray1);
          PdfDictionary pdfDictionary3 = new PdfDictionary();
          Bitmap input1 = new Bitmap((int) this.Width, (int) this.Height, this.m_pixelFormat);
          BitmapData bitmapdata1 = input1.LockBits(new Rectangle(0, 0, input1.Width, input1.Height), ImageLockMode.ReadWrite, input1.PixelFormat);
          if ((this.m_pixelFormat == PixelFormat.Format8bppIndexed || this.m_pixelFormat == PixelFormat.Format4bppIndexed) && this.m_colorPalette != null)
            input1.Palette = this.m_colorPalette;
          int num10 = Image.GetPixelFormatSize(input1.PixelFormat) / 8;
          if (this.m_imageDictionary.ContainsKey("DecodeParms"))
          {
            PdfDictionary pdfDictionary4 = this.DecodeParam[index1];
            if (pdfDictionary4.ContainsKey("Predictor"))
              predictor = (pdfDictionary4["Predictor"] as PdfNumber).IntValue;
            if (pdfDictionary4.ContainsKey("Columns"))
              columns = (pdfDictionary4["Columns"] as PdfNumber).IntValue;
            if (pdfDictionary4.ContainsKey("Colors"))
              colors = (pdfDictionary4["Colors"] as PdfNumber).IntValue;
            if (pdfDictionary4.Count > 0)
              numArray1 = this.DecodePredictor(predictor, colors, columns, memoryStream1).GetBuffer();
          }
          switch (num10)
          {
            case 3:
              for (int index13 = 0; index13 + 3 < numArray1.Length; index13 += 3)
              {
                int index14 = index13 + 2;
                byte num11 = numArray1[index14];
                numArray1[index14] = numArray1[index13];
                numArray1[index13] = num11;
              }
              break;
            case 4:
              byte[] numArray3 = new byte[(int) ((double) this.Width * (double) this.Height * 3.0)];
              int num12 = (int) ((double) this.Width * (double) this.Height * 4.0);
              byte[] numArray4 = numArray1;
              int index15 = 0;
              int index16 = 0;
              for (; index15 < num12 + 4; index15 += 4)
              {
                numArray3[index16 + 2] = numArray4[index15];
                numArray3[index16 + 1] = numArray4[index15 + 1];
                numArray3[index16] = numArray4[index15 + 2];
                index16 += 3;
              }
              numArray1 = numArray3;
              break;
          }
          int startIndex1 = 0;
          scan0_1 = bitmapdata1.Scan0;
          long int64_1 = scan0_1.ToInt64();
          int length3 = (int) this.Width;
          if (num10 == 3)
            length3 = (int) this.Width * 3;
          if (this.m_pixelFormat == PixelFormat.Format4bppIndexed)
          {
            for (int index17 = 0; (double) index17 < (double) this.Height / 2.0; ++index17)
            {
              if (index17 % 2 == 0)
              {
                Marshal.Copy(numArray1, startIndex1, new IntPtr(int64_1), length3);
                int64_1 += (long) bitmapdata1.Stride;
              }
              if (index17 % 3 == 0)
              {
                Marshal.Copy(numArray1, startIndex1, new IntPtr(int64_1), length3);
                int64_1 += (long) bitmapdata1.Stride;
              }
              if (index17 % 7 == 0)
              {
                Marshal.Copy(numArray1, startIndex1, new IntPtr(int64_1), length3);
                int64_1 += (long) bitmapdata1.Stride;
              }
              Marshal.Copy(numArray1, startIndex1, new IntPtr(int64_1), length3);
              int64_1 += (long) bitmapdata1.Stride;
              startIndex1 += length3;
            }
          }
          else
          {
            for (int index18 = 0; (double) index18 < (double) this.Height; ++index18)
            {
              Marshal.Copy(numArray1, startIndex1, new IntPtr(int64_1), length3);
              startIndex1 += length3;
              int64_1 += (long) bitmapdata1.Stride;
            }
          }
          input1.UnlockBits(bitmapdata1);
          if (this.IsTransparent)
            input1.MakeTransparent();
          MemoryStream memoryStream2 = new MemoryStream();
          input1.Save((Stream) memoryStream2, ImageFormat.Png);
          if (!this.m_imageDictionary.ContainsKey("SMask"))
          {
            this.ImageStream = (Stream) memoryStream2;
            this.ImageStream.Position = 0L;
            break;
          }
          try
          {
            this.ImageStream = (Stream) this.MergeImages(input1, this.MaskStream as MemoryStream);
            this.ImageStream.Position = 0L;
            break;
          }
          catch (Exception ex)
          {
            this.ImageStream = (Stream) memoryStream2;
            this.ImageStream.Position = 0L;
            break;
          }
        case "JBIG2Decode":
          MemoryStream imageStream1 = this.ImageStream as MemoryStream;
          JBIG2StreamDecoder jbiG2StreamDecoder = new JBIG2StreamDecoder();
          MemoryStream encodedStream = new MemoryStream();
          if (this.m_imageDictionary.ContainsKey("DecodeParms"))
          {
            if (this.m_imageDictionary["DecodeParms"] is PdfDictionary image)
            {
              if (image.ContainsKey("JBIG2Globals"))
              {
                encodedStream = ((image["JBIG2Globals"] as PdfReferenceHolder).Object as PdfStream).InternalStream;
                if ((image["JBIG2Globals"] as PdfReferenceHolder).Object is PdfDictionary)
                {
                  PdfDictionary pdfDictionary5 = (image["JBIG2Globals"] as PdfReferenceHolder).Object as PdfDictionary;
                  if (pdfDictionary5.ContainsKey("Filter") && (pdfDictionary5["Filter"] as PdfName).Value == "FlateDecode")
                    encodedStream = this.DecodeFlateStream(encodedStream);
                }
              }
            }
            else
            {
              string str = "";
              PdfDictionary pdfDictionary6 = (this.m_imageDictionary["DecodeParms"] as PdfArray)[0] as PdfDictionary;
              if (pdfDictionary6.ContainsKey("JBIG2Globals"))
              {
                if ((pdfDictionary6["JBIG2Globals"] as PdfReferenceHolder).Object is PdfDictionary pdfDictionary7 && pdfDictionary7.ContainsKey("Filter"))
                  str = (pdfDictionary7["Filter"] as PdfName).Value.ToString();
                encodedStream = ((pdfDictionary6["JBIG2Globals"] as PdfReferenceHolder).Object as PdfStream).InternalStream;
                if (str == "FlateDecode")
                {
                  encodedStream.Position = 0L;
                  encodedStream.ReadByte();
                  encodedStream.ReadByte();
                  DeflateStream deflateStream = new DeflateStream((Stream) encodedStream, CompressionMode.Decompress, true);
                  byte[] buffer = new byte[4096 /*0x1000*/];
                  MemoryStream memoryStream3 = new MemoryStream();
                  while (true)
                  {
                    int count = deflateStream.Read(buffer, 0, 4096 /*0x1000*/);
                    if (count > 0)
                      memoryStream3.Write(buffer, 0, count);
                    else
                      break;
                  }
                  encodedStream = memoryStream3;
                }
              }
            }
            if (encodedStream.Length > 0L)
            {
              encodedStream.Capacity = (int) encodedStream.Length;
              jbiG2StreamDecoder.GlobalData = encodedStream.GetBuffer();
            }
          }
          jbiG2StreamDecoder.DecodeJBIG2(imageStream1.GetBuffer());
          byte[] data = jbiG2StreamDecoder.GetPageAsJBIG2Bitmap(0).GetData(true);
          Bitmap bitmap5 = new Bitmap((int) this.Width, (int) this.Height, PixelFormat.Format1bppIndexed);
          BitmapData bitmapdata2 = bitmap5.LockBits(new Rectangle(0, 0, (int) this.Width, (int) this.Height), ImageLockMode.ReadWrite, bitmap5.PixelFormat);
          double height1 = (double) this.Height;
          int stride = bitmapdata2.Stride;
          IntPtr scan0_4 = bitmapdata2.Scan0;
          int pixelFormatSize = Image.GetPixelFormatSize(bitmap5.PixelFormat);
          int num13 = pixelFormatSize / 8;
          int length4 = num13 * (int) this.Width;
          switch (num13)
          {
            case 0:
              length4 = (int) this.Width * pixelFormatSize / 8;
              if ((int) this.Width * pixelFormatSize % 8 != 0)
              {
                ++length4;
                break;
              }
              break;
            case 1:
              length4 = bitmap5.Width;
              break;
            case 3:
              length4 = num13 * (int) this.Width;
              break;
          }
          int startIndex2 = 0;
          scan0_1 = bitmapdata2.Scan0;
          long int64_2 = scan0_1.ToInt64();
          for (int index19 = 0; (double) index19 < (double) this.Height; ++index19)
          {
            Marshal.Copy(data, startIndex2, new IntPtr(int64_2), length4);
            startIndex2 += length4;
            int64_2 += (long) bitmapdata2.Stride;
          }
          bitmap5.UnlockBits(bitmapdata2);
          Bitmap bitmap6 = bitmap5;
          MemoryStream memoryStream4 = new MemoryStream();
          MemoryStream memoryStream5 = memoryStream4;
          ImageFormat jpeg1 = ImageFormat.Jpeg;
          bitmap6.Save((Stream) memoryStream5, jpeg1);
          memoryStream4.Position = 0L;
          this.ImageStream = (Stream) memoryStream4;
          this.ImageStream.Position = 0L;
          break;
        case "JPXDecode":
          this.ImageStream.Position = 0L;
          Bitmap bitmap7 = (Bitmap) new JPXImage().FromStream(this.ImageStream);
          MemoryStream imageStream2 = new MemoryStream();
          MemoryStream memoryStream6 = imageStream2;
          ImageFormat jpeg2 = ImageFormat.Jpeg;
          bitmap7.Save((Stream) memoryStream6, jpeg2);
          return (Stream) imageStream2;
        case "LZWDecode":
          PdfLzwCompressor pdfLzwCompressor = new PdfLzwCompressor();
          Stream stream = (Stream) new MemoryStream();
          byte[] array1 = (this.ImageStream as MemoryStream).ToArray();
          Stream outputData = stream;
          pdfLzwCompressor.Decompress(array1, outputData);
          this.ImageStream = (Stream) new MemoryStream();
          this.ImageStream = stream;
          break;
        case "RunLengthDecode":
          this.ImageStream.Position = 0L;
          MemoryStream memoryStream7 = new MemoryStream();
          byte[] array2 = (this.ImageStream as MemoryStream).ToArray();
          int length5 = array2.Length;
          int index20;
          for (int index21 = 0; index21 < length5; index21 = index20 + 1)
          {
            int num14 = (int) array2[index21];
            if (num14 < 0)
              num14 = 256 /*0x0100*/ + num14;
            if (num14 == 128 /*0x80*/)
              index20 = length5;
            else if (num14 > 128 /*0x80*/)
            {
              index20 = index21 + 1;
              int length6 = 257 - num14;
              int num15 = (int) array2[index20];
              byte[] buffer = new byte[length6];
              for (int index22 = 0; index22 < length6; ++index22)
                buffer[index22] = (byte) num15;
              memoryStream7.Write(buffer, 0, buffer.Length);
            }
            else
            {
              int num16 = index21 + 1;
              int length7 = num14 + 1;
              byte[] buffer = new byte[length7];
              for (int index23 = 0; index23 < length7; ++index23)
              {
                int num17 = (int) array2[num16 + index23];
                buffer[index23] = (byte) num17;
              }
              memoryStream7.Write(buffer, 0, buffer.Length);
              index20 = num16 + length7 - 1;
            }
          }
          byte[] array3 = memoryStream7.ToArray();
          byte[] source1 = array3;
          if (this.ColorSpace == "DeviceGray")
          {
            byte[] numArray5 = array3;
            int height2 = (int) this.Height;
            int width1 = (int) this.Width;
            byte[] numArray6 = new byte[width1 * height2];
            int[] numArray7 = new int[8]
            {
              1,
              2,
              4,
              8,
              16 /*0x10*/,
              32 /*0x20*/,
              64 /*0x40*/,
              128 /*0x80*/
            };
            int num18 = (int) this.Width + 7 >> 3;
            int num19 = 1;
            try
            {
              for (int index24 = 0; index24 < height2; ++index24)
              {
                for (int index25 = 0; index25 < width1; ++index25)
                {
                  int num20 = 0;
                  int num21 = 0;
                  int num22 = num19;
                  int num23 = num19;
                  int num24 = (int) this.Width - index25;
                  int num25 = (int) this.Height - index24;
                  if (num22 > num24)
                    num22 = num24;
                  if (num23 > num25)
                    num23 = num25;
                  for (int index26 = 0; index26 < num23; ++index26)
                  {
                    for (int index27 = 0; index27 < num22; ++index27)
                    {
                      if (((int) numArray5[(index26 + index24 * num19) * num18 + (index25 * num19 + index27 >> 3)] & numArray7[7 - (index25 * num19 + index27 & 7)]) != 0)
                        ++num20;
                      ++num21;
                    }
                  }
                  int index28 = index25 + width1 * index24;
                  numArray6[index28] = (byte) ((int) byte.MaxValue * num20 / num21);
                }
              }
            }
            catch
            {
            }
            byte[] source2 = numArray6;
            this.m_pixelFormat = PixelFormat.Format8bppIndexed;
            Bitmap bitmap8 = new Bitmap((int) this.Width, (int) this.Height, this.m_pixelFormat);
            BitmapData bitmapdata3 = bitmap8.LockBits(new Rectangle(0, 0, bitmap8.Width, bitmap8.Height), ImageLockMode.ReadWrite, bitmap8.PixelFormat);
            int startIndex3 = 0;
            scan0_1 = bitmapdata3.Scan0;
            long int64_3 = scan0_1.ToInt64();
            int width2 = (int) this.Width;
            for (int index29 = 0; (double) index29 < (double) this.Height; ++index29)
            {
              Marshal.Copy(source2, startIndex3, new IntPtr(int64_3), width2);
              startIndex3 += width2;
              int64_3 += (long) bitmapdata3.Stride;
            }
            bitmap8.UnlockBits(bitmapdata3);
            MemoryStream memoryStream8 = new MemoryStream();
            bitmap8.Save((Stream) memoryStream8, ImageFormat.Jpeg);
            this.ImageStream = (Stream) memoryStream8;
            this.ImageStream.Position = 0L;
            break;
          }
          Bitmap input2 = new Bitmap((int) this.Width, (int) this.Height, this.m_pixelFormat);
          BitmapData bitmapdata4 = input2.LockBits(new Rectangle(0, 0, input2.Width, input2.Height), ImageLockMode.ReadWrite, input2.PixelFormat);
          if (this.m_pixelFormat == PixelFormat.Format8bppIndexed)
            input2.Palette = this.m_colorPalette;
          int num26 = Image.GetPixelFormatSize(input2.PixelFormat) / 8;
          switch (num26)
          {
            case 3:
              for (int index30 = 0; index30 + 3 < source1.Length; index30 += 3)
              {
                int index31 = index30 + 2;
                byte num27 = source1[index31];
                source1[index31] = source1[index30];
                source1[index30] = num27;
              }
              break;
            case 4:
              byte[] numArray8 = new byte[(int) ((double) this.Width * (double) this.Height * 3.0)];
              int num28 = (int) ((double) this.Width * (double) this.Height * 4.0);
              byte[] numArray9 = source1;
              int index32 = 0;
              int index33 = 0;
              for (; index32 < num28 + 4; index32 += 4)
              {
                numArray8[index33 + 2] = numArray9[index32];
                numArray8[index33 + 1] = numArray9[index32 + 1];
                numArray8[index33] = numArray9[index32 + 2];
                index33 += 3;
              }
              source1 = numArray8;
              break;
          }
          if (Math.Abs(bitmapdata4.Stride) * input2.Height < source1.Length)
          {
            int startIndex4 = 0;
            scan0_1 = bitmapdata4.Scan0;
            long int64_4 = scan0_1.ToInt64();
            int length8 = (int) this.Width;
            if (num26 == 3)
              length8 = (int) this.Width * 3;
            for (int index34 = 0; (double) index34 < (double) this.Height; ++index34)
            {
              Marshal.Copy(source1, startIndex4, new IntPtr(int64_4), length8);
              startIndex4 += length8;
              int64_4 += (long) bitmapdata4.Stride;
            }
          }
          else
            Marshal.Copy(source1, 0, bitmapdata4.Scan0, source1.Length);
          input2.UnlockBits(bitmapdata4);
          MemoryStream memoryStream9 = new MemoryStream();
          input2.Save((Stream) memoryStream9, ImageFormat.Jpeg);
          if (!this.m_imageDictionary.ContainsKey("SMask"))
          {
            this.ImageStream = (Stream) memoryStream9;
            this.ImageStream.Position = 0L;
            break;
          }
          try
          {
            this.ImageStream = (Stream) this.MergeImages(input2, this.MaskStream as MemoryStream);
            this.ImageStream.Position = 0L;
            break;
          }
          catch (Exception ex)
          {
            this.ImageStream = (Stream) memoryStream9;
            this.ImageStream.Position = 0L;
            break;
          }
        default:
          if (string.IsNullOrEmpty(this.ImageFilter[index1]))
            throw new Exception("Error in identifying ImageFilter");
          throw new Exception(this.ImageFilter.ToString() + " does not supported");
      }
    }
    return this.ImageStream;
  }

  private float GetImageWidth()
  {
    float imageWidth = 0.0f;
    if (this.m_imageDictionary != null && this.m_imageDictionary.ContainsKey("Width"))
      imageWidth = (this.m_imageDictionary["Width"] as PdfNumber).FloatValue;
    return imageWidth;
  }

  private void GetIndexedColorSpace(
    PdfArray value,
    string internalColorSpace,
    PdfDictionary colorspaceDictionary,
    string[] filter)
  {
    if (this.m_colorspace == "ICCBased")
    {
      if ((value[1] as PdfReferenceHolder).Object is PdfStream)
      {
        PdfStream pdfStream = (value[1] as PdfReferenceHolder).Object as PdfStream;
        this.nonIndexedImageColorResource = new Dictionary<string, PdfStream>();
        this.nonIndexedImageColorResource.Add(this.m_colorspace, pdfStream);
      }
    }
    else if (this.m_colorspace == "Indexed")
    {
      if ((object) (value[1] as PdfName) != null)
        this.m_colorspaceBase = (value[1] as PdfName).Value;
      else if ((object) (value[1] as PdfReferenceHolder) != null)
      {
        if ((value[1] as PdfReferenceHolder).Object is PdfArray)
        {
          PdfArray pdfArray = (value[1] as PdfReferenceHolder).Object as PdfArray;
          if ((object) (pdfArray[0] as PdfName) != null)
            internalColorSpace = (pdfArray[0] as PdfName).Value;
          if ((object) (pdfArray[1] as PdfReferenceHolder) != null)
          {
            PdfDictionary pdfDictionary = (pdfArray[1] as PdfReferenceHolder).Object as PdfDictionary;
            if (pdfDictionary.ContainsKey("Alternate"))
              this.m_colorspaceBase = (pdfDictionary["Alternate"] as PdfName).Value;
          }
        }
      }
      else if (value[1] is PdfArray)
      {
        PdfArray pdfArray = value[1] as PdfArray;
        if ((object) (pdfArray[0] as PdfName) != null)
          internalColorSpace = (pdfArray[0] as PdfName).Value;
        if ((object) (pdfArray[1] as PdfReferenceHolder) != null)
        {
          PdfDictionary pdfDictionary = (pdfArray[1] as PdfReferenceHolder).Object as PdfDictionary;
          if (pdfDictionary.ContainsKey("Alternate"))
            this.m_colorspaceBase = (pdfDictionary["Alternate"] as PdfName).Value;
        }
      }
      if (this.m_colorspaceBase == "DeviceRGB" || this.m_colorspaceBase == "DeviceGray")
      {
        if (this.m_colorspaceBase == "DeviceGray")
        {
          this.ColorSpace = "IndexedDeviceGray";
          this.IsTransparent = true;
        }
        this.m_colorspaceHival = (value[2] as PdfNumber).IntValue;
        if ((object) (value[3] as PdfReferenceHolder) != null)
        {
          this.m_colorspaceStream = ((value[3] as PdfReferenceHolder).Object as PdfStream).InternalStream;
          colorspaceDictionary = (value[3] as PdfReferenceHolder).Object as PdfDictionary;
        }
        else if (value[3] is PdfString)
        {
          string encodedText = (value[3] as PdfString).Value;
          if (encodedText.Contains("ColorFound") && encodedText.IndexOf("ColorFound") == 0)
            encodedText = encodedText.Remove(0, 10);
          byte[] asciiBytes = ImageStructure.GetAsciiBytes(this.SkipEscapeSequence(this.GetLiteralString(encodedText)));
          this.m_colorspaceStream = new MemoryStream(asciiBytes, 0, asciiBytes.Length, true, true);
        }
        if ((double) this.BitsPerComponent == 4.0 && internalColorSpace != "ICCBased")
        {
          this.m_pixelFormat = PixelFormat.Format4bppIndexed;
          this.IsTransparent = true;
        }
        else if ((double) this.BitsPerComponent == 8.0)
          this.m_pixelFormat = PixelFormat.Format8bppIndexed;
      }
    }
    if (colorspaceDictionary != null && colorspaceDictionary.ContainsKey("Filter"))
    {
      if ((object) (colorspaceDictionary["Filter"] as PdfName) != null)
        filter = new string[1]
        {
          (colorspaceDictionary["Filter"] as PdfName).Value
        };
      else if (colorspaceDictionary["Filter"] is PdfArray)
      {
        int count = (colorspaceDictionary["Filter"] as PdfArray).Count;
        filter = new string[count];
        for (int index = 0; index < count; ++index)
          filter[index] = ((colorspaceDictionary["Filter"] as PdfArray)[index] as PdfName).Value;
      }
      else if ((object) (colorspaceDictionary["Filter"] as PdfReferenceHolder) != null)
      {
        PdfArray pdfArray = (colorspaceDictionary["Filter"] as PdfReferenceHolder).Object as PdfArray;
        filter = new string[pdfArray.Count];
        for (int index = 0; index < pdfArray.Count; ++index)
          filter[index] = (pdfArray[0] as PdfName).Value;
      }
    }
    if (filter != null)
    {
      for (int index = 0; index < filter.Length; ++index)
      {
        switch (filter[index])
        {
          case "FlateDecode":
            this.m_colorspaceStream = this.DecodeFlateStream(this.m_colorspaceStream);
            break;
          case "ASCII85":
          case "ASCII85Decode":
            this.m_colorspaceStream = this.DecodeASCII85Stream(this.m_colorspaceStream);
            break;
          default:
            throw new Exception("Filter to decode colorspace not implemented.");
        }
      }
    }
    if (!(this.m_colorspace == "Indexed") && !(this.m_colorspace == "IndexedDeviceGray"))
      return;
    byte[] buffer = this.m_colorspaceStream.GetBuffer();
    byte[] destinationArray = new byte[786];
    Array.Copy((Array) buffer, (Array) destinationArray, buffer.Length);
    this.m_colorPalette = new Bitmap((int) this.Width, (int) this.Height, PixelFormat.Format8bppIndexed).Palette;
    int num1 = 0;
    for (int index1 = 0; index1 < this.m_colorPalette.Entries.Length; ++index1)
    {
      Color[] entries = this.m_colorPalette.Entries;
      int index2 = index1;
      byte[] numArray1 = destinationArray;
      int index3 = num1;
      int num2 = index3 + 1;
      int red = (int) numArray1[index3];
      byte[] numArray2 = destinationArray;
      int index4 = num2;
      int num3 = index4 + 1;
      int green = (int) numArray2[index4];
      byte[] numArray3 = destinationArray;
      int index5 = num3;
      num1 = index5 + 1;
      int blue = (int) numArray3[index5];
      Color color = Color.FromArgb(red, green, blue);
      entries[index2] = color;
    }
  }

  private void GetIsImageMask()
  {
    if (!this.m_imageDictionary.ContainsKey("ImageMask"))
      return;
    this.m_isImageMask = (this.m_imageDictionary["ImageMask"] as PdfBoolean).Value;
  }

  private string GetLiteralString(string encodedText)
  {
    string literalString = encodedText;
    int startIndex = -1;
    int num = 3;
    while (literalString.Contains("\\") || literalString.Contains("\0"))
    {
      string empty = string.Empty;
      if (literalString.IndexOf('\\', startIndex + 1) >= 0)
      {
        startIndex = literalString.IndexOf('\\', startIndex + 1);
      }
      else
      {
        startIndex = literalString.IndexOf(char.MinValue, startIndex + 1);
        if (startIndex < 0)
          return literalString;
        num = 2;
      }
      for (int index = startIndex + 1; index <= startIndex + num; ++index)
      {
        if (index < literalString.Length)
        {
          int result = 0;
          if (int.TryParse(literalString[index].ToString(), out result))
          {
            if (result <= 8)
              empty += literalString[index].ToString();
          }
          else
          {
            empty = string.Empty;
            break;
          }
        }
        else
          empty = string.Empty;
      }
      if (empty != string.Empty)
      {
        int uint64 = (int) Convert.ToUInt64(empty, 8);
        string str = Encoding.GetEncoding(1252).GetString(new byte[1]
        {
          Convert.ToByte(uint64)
        });
        literalString = literalString.Remove(startIndex, num + 1).Insert(startIndex, str);
      }
    }
    return literalString;
  }

  private MemoryStream MergeImages(Bitmap input, MemoryStream maskStream)
  {
    Color transparentColor;
    if (input == null)
    {
      input = new Bitmap((int) this.m_maskWidth, (int) this.m_maskHeight);
      transparentColor = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
    }
    else
      transparentColor = input.GetPixel(0, 0);
    Bitmap bitmap1;
    if (this.m_maskFilter == "DCTDecode")
      bitmap1 = Image.FromStream((Stream) maskStream) as Bitmap;
    else if (this.m_maskFilter == "FlateDecode")
    {
      maskStream = this.DecodeFlateStream(maskStream);
      bitmap1 = this.DecodeMaskImage(maskStream);
    }
    else
      bitmap1 = Image.FromStream((Stream) maskStream) as Bitmap;
    Bitmap bitmap2 = new Bitmap(input.Width, input.Height, PixelFormat.Format32bppArgb);
    Rectangle rect = new Rectangle(0, 0, input.Width, input.Height);
    BitmapData bitmapdata1 = input.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppRgb);
    BitmapData bitmapdata2 = bitmap1.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppRgb);
    BitmapData bitmapdata3 = bitmap2.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppRgb);
    int length1 = Math.Abs(bitmapdata1.Stride) * input.Height;
    byte[] destination1 = new byte[length1];
    Marshal.Copy(bitmapdata1.Scan0, destination1, 0, length1);
    int length2 = Math.Abs(bitmapdata2.Stride) * bitmap1.Height;
    byte[] destination2 = new byte[length2];
    Marshal.Copy(bitmapdata2.Scan0, destination2, 0, length2);
    int length3 = Math.Abs(bitmapdata3.Stride) * bitmap2.Height;
    byte[] numArray = new byte[length3];
    Marshal.Copy(bitmapdata3.Scan0, numArray, 0, length3);
    byte maxValue = byte.MaxValue;
    for (int index = 0; index < length3; index += 4)
    {
      if (destination2[index] != (byte) 0 || destination2[index + 1] != (byte) 0 || destination2[index + 2] != (byte) 0 || destination2[index + 3] != byte.MaxValue)
      {
        Color color1 = Color.FromArgb((int) destination1[index], (int) destination1[index + 1], (int) destination1[index + 2], (int) destination1[index + 3]);
        Color color2 = Color.FromArgb((int) (byte) ((uint) maxValue - (uint) destination2[index]), (int) (byte) ((uint) maxValue - (uint) destination2[index + 1]), (int) (byte) ((uint) maxValue - (uint) destination2[index + 2]), (int) (byte) ((uint) maxValue - (uint) destination2[index + 3]));
        float num1 = this.ConvertToFloat(color1.A);
        float num2 = this.ConvertToFloat(color1.R);
        float num3 = this.ConvertToFloat(color1.G);
        double num4 = (double) this.ConvertToFloat(color1.B);
        float num5 = this.ConvertToFloat(color2.A);
        float num6 = this.ConvertToFloat(color2.R);
        float num7 = this.ConvertToFloat(color2.G);
        float num8 = this.ConvertToFloat(color2.B);
        float num9 = num1 + num5;
        float num10 = num2 + num6;
        float num11 = num3 + num7;
        double num12 = (double) num8;
        float num13 = (float) (num4 + num12);
        byte num14 = this.ConvertToByte(num9);
        byte num15 = this.ConvertToByte(num10);
        byte num16 = this.ConvertToByte(num11);
        byte num17 = this.ConvertToByte(num13);
        numArray[index] = num14;
        numArray[index + 1] = num15;
        numArray[index + 2] = num16;
        numArray[index + 3] = num17;
      }
      else if (transparentColor == Color.FromArgb((int) byte.MaxValue, 0, 0, 0) || transparentColor == Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue))
      {
        numArray[index] = transparentColor.R;
        numArray[index + 1] = transparentColor.G;
        numArray[index + 2] = transparentColor.B;
        numArray[index + 3] = transparentColor.A;
      }
      else
      {
        numArray[index] = (byte) 0;
        numArray[index + 1] = (byte) 0;
        numArray[index + 2] = (byte) 0;
        numArray[index + 3] = (byte) 0;
      }
    }
    Marshal.Copy(numArray, 0, bitmapdata3.Scan0, length3);
    bitmap1.UnlockBits(bitmapdata2);
    input.UnlockBits(bitmapdata1);
    bitmap2.UnlockBits(bitmapdata3);
    if (transparentColor == Color.FromArgb((int) byte.MaxValue, 0, 0, 0) || transparentColor == Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue))
      bitmap2.MakeTransparent(transparentColor);
    else
      bitmap2.MakeTransparent();
    MemoryStream memoryStream = new MemoryStream();
    bitmap2.Save((Stream) memoryStream, ImageFormat.Png);
    memoryStream.Position = 0L;
    input.Dispose();
    bitmap1.Dispose();
    bitmap2.Dispose();
    return memoryStream;
  }

  private double RoundOff(double value)
  {
    if (value < 0.0)
      value = 0.0;
    if (value > 1.0)
      value = 1.0;
    return value;
  }

  public static Image SetOpacity(Image image, float opacity)
  {
    ColorMatrix newColorMatrix = new ColorMatrix();
    newColorMatrix.Matrix33 = opacity;
    ImageAttributes imageAttr = new ImageAttributes();
    imageAttr.SetColorMatrix(newColorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
    Bitmap bitmap = new Bitmap(image.Width, image.Height);
    using (Graphics graphics = Graphics.FromImage((Image) bitmap))
    {
      graphics.SmoothingMode = SmoothingMode.AntiAlias;
      graphics.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttr);
    }
    return (Image) bitmap;
  }

  private string SkipEscapeSequence(string text)
  {
    text = text.Replace("\\r", "\r");
    text = text.Replace("\\(", "(");
    text = text.Replace("\\)", ")");
    text = text.Replace("\\n", "\n");
    text = text.Replace("\\t", "\t");
    return text;
  }

  private byte[] YCCKtoRGB(byte[] encodedData)
  {
    byte[] numArray1 = new byte[encodedData.Length];
    double width = (double) this.Width;
    double height = (double) this.Height;
    int num1 = 0;
    for (int index1 = 0; index1 + 3 < encodedData.Length; index1 += 3)
    {
      double num2 = (double) ((int) encodedData[index1] & (int) byte.MaxValue);
      double num3 = (double) ((int) encodedData[index1 + 1] & (int) byte.MaxValue);
      double num4 = (double) ((int) encodedData[index1 + 2] & (int) byte.MaxValue);
      double num5 = (double) byte.MaxValue - num2;
      double num6 = (double) byte.MaxValue - num3;
      double num7 = (double) byte.MaxValue - num4;
      byte[] numArray2 = numArray1;
      int index2 = num1;
      int num8 = index2 + 1;
      int num9 = (int) (byte) num5;
      numArray2[index2] = (byte) num9;
      byte[] numArray3 = numArray1;
      int index3 = num8;
      int num10 = index3 + 1;
      int num11 = (int) (byte) num6;
      numArray3[index3] = (byte) num11;
      byte[] numArray4 = numArray1;
      int index4 = num10;
      num1 = index4 + 1;
      int num12 = (int) (byte) num7;
      numArray4[index4] = (byte) num12;
    }
    return numArray1;
  }

  private byte[] YCCToRGB(byte[] encodedData)
  {
    byte[] rgb = new byte[(int) this.Width * (int) this.Height * 3];
    int num1 = (int) this.Width * (int) this.Height * 4;
    double num2 = -1.0;
    double num3 = -1.12;
    double num4 = -1.12;
    double num5 = -1.21;
    double maxValue = (double) byte.MaxValue;
    int num6 = 0;
    for (int index1 = 0; index1 < num1 && index1 <= encodedData.Length; index1 += 4)
    {
      double num7 = (double) ((int) encodedData[index1] & (int) byte.MaxValue) / maxValue;
      double num8 = (double) ((int) encodedData[index1 + 1] & (int) byte.MaxValue) / maxValue;
      double num9 = (double) ((int) encodedData[index1 + 2] & (int) byte.MaxValue) / maxValue;
      double num10 = (double) ((int) encodedData[index1 + 3] & (int) byte.MaxValue) / maxValue;
      double num11 = 0.0;
      double num12 = 0.0;
      double num13 = 0.0;
      if (num2 != num7 || num3 != num8 || num4 != num9 || num5 != num10)
      {
        double num14 = num7;
        double num15 = num8;
        double num16 = num9;
        double num17 = num10;
        num11 = (double) byte.MaxValue * (1.0 - num14) * (1.0 - num17);
        num12 = (double) byte.MaxValue * (1.0 - num15) * (1.0 - num17);
        num13 = (double) byte.MaxValue * (1.0 - num16) * (1.0 - num17);
      }
      byte[] numArray1 = rgb;
      int index2 = num6;
      int num18 = index2 + 1;
      int num19 = (int) (byte) num11;
      numArray1[index2] = (byte) num19;
      byte[] numArray2 = rgb;
      int index3 = num18;
      int num20 = index3 + 1;
      int num21 = (int) (byte) num12;
      numArray2[index3] = (byte) num21;
      byte[] numArray3 = rgb;
      int index4 = num20;
      num6 = index4 + 1;
      int num22 = (int) (byte) num13;
      numArray3[index4] = (byte) num22;
    }
    return rgb;
  }

  internal float BitsPerComponent
  {
    get
    {
      if ((double) this.m_bitsPerComponent == 0.0)
        this.m_bitsPerComponent = this.GetBitsPerComponent();
      return this.m_bitsPerComponent;
    }
  }

  internal string ColorSpace
  {
    get
    {
      if (this.m_colorspace == null)
        this.GetColorSpace();
      return this.m_colorspace;
    }
    set => this.m_colorspace = value;
  }

  internal PdfDictionary[] DecodeParam
  {
    get
    {
      if (this.m_decodeParam == null)
        this.m_decodeParam = this.GetDecodeParam();
      return this.m_decodeParam;
    }
  }

  internal Image EmbeddedImage
  {
    get
    {
      if (this.m_embeddedImage == null)
      {
        if (!this.m_isImageStreamParsed)
        {
          try
          {
            this.m_embeddedImage = Image.FromStream((Stream) (this.GetImageStream() as MemoryStream));
            return this.m_embeddedImage;
          }
          catch
          {
            return (Image) null;
          }
        }
      }
      return this.m_embeddedImage;
    }
  }

  internal float Height
  {
    get
    {
      if ((double) this.m_height == 0.0)
        this.m_height = this.GetImageHeight();
      return this.m_height;
    }
  }

  internal PdfDictionary ImageDictionary => this.m_imageDictionary;

  internal string[] ImageFilter
  {
    get
    {
      if (this.m_imageFilter == null)
        this.m_imageFilter = this.GetImageFilter();
      return this.m_imageFilter;
    }
  }

  internal PdfMatrix ImageInfo
  {
    get => this.m_imageInfo;
    set => this.m_imageInfo = value;
  }

  public Stream ImageStream
  {
    get
    {
      if (this.m_imageStream == null)
        this.m_imageStream = (Stream) (this.m_imageDictionary as PdfStream).InternalStream;
      return this.m_imageStream;
    }
    set => this.m_imageStream = value;
  }

  internal bool IsImageMask
  {
    get
    {
      this.GetIsImageMask();
      return this.m_isImageMask;
    }
    set => this.m_isImageMask = value;
  }

  public Stream MaskStream
  {
    get
    {
      if (this.m_maskStream == null)
      {
        this.m_maskStream = (Stream) ((this.m_imageDictionary["SMask"] as PdfReferenceHolder).Object as PdfStream).InternalStream;
        PdfDictionary pdfDictionary = (this.m_imageDictionary["SMask"] as PdfReferenceHolder).Object as PdfDictionary;
        if (pdfDictionary.ContainsKey("Width"))
          this.m_maskWidth = (float) (pdfDictionary["Width"] as PdfNumber).IntValue;
        if (pdfDictionary.ContainsKey("Height"))
          this.m_maskHeight = (float) (pdfDictionary["Height"] as PdfNumber).IntValue;
        if (pdfDictionary.ContainsKey("BitsPerComponent"))
          this.m_maskBitsPerComponent = (float) (pdfDictionary["BitsPerComponent"] as PdfNumber).IntValue;
        if (pdfDictionary.ContainsKey("Filter"))
        {
          if (pdfDictionary["Filter"] is PdfArray)
          {
            if (pdfDictionary["Filter"] is PdfArray pdfArray)
              this.m_maskFilter = (pdfArray[0] as PdfName).Value;
          }
          else
            this.m_maskFilter = (pdfDictionary["Filter"] as PdfName).Value;
        }
      }
      return this.m_maskStream;
    }
    set => this.m_maskStream = value;
  }

  internal float Width
  {
    get
    {
      if ((double) this.m_width == 0.0)
        this.m_width = this.GetImageWidth();
      return this.m_width;
    }
  }

  public delegate void ImagePreRenderEventHandler(object sender, ImagePreRenderEventArgs args);
}
