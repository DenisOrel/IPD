// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Compression.PdfCcittEncoder
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Syncfusion.Pdf.Compression;

internal class PdfCcittEncoder
{
  private const int c_Code = 1;
  private const int c_Eol = 1;
  private const int c_G3code_Eof = -3;
  private const int c_G3code_Eol = -1;
  private const int c_G3code_Incomp = -4;
  private const int c_G3code_Invalid = -2;
  private const int c_Length = 0;
  private const int c_Runlen = 2;
  private int m_countBit = 8;
  private int m_data;
  private byte[] m_imageData;
  private int m_offsetData;
  private List<byte> m_outBuf = new List<byte>();
  private byte[] m_refline;
  private int m_rowbytes;
  private int m_rowPixels;
  private static int[] s_horizontalTabel = new int[3]
  {
    3,
    1,
    0
  };
  private static int[] s_maskTabel;
  private static int[] s_passcode = new int[3]{ 4, 1, 0 };
  private static byte[] s_tableOneSpan;
  private static byte[] s_tableZeroSpan;
  private static int[][] s_terminatingBlackCodes;
  private static int[][] s_terminatingWhiteCodes;
  private static int[][] s_verticalTable;

  static PdfCcittEncoder()
  {
    PdfCcittEncoder.s_maskTabel = new int[9]
    {
      0,
      1,
      3,
      7,
      15,
      31 /*0x1F*/,
      63 /*0x3F*/,
      (int) sbyte.MaxValue,
      (int) byte.MaxValue
    };
    PdfCcittEncoder.CreteTableZeroSpan();
    PdfCcittEncoder.CreteTableOneSpan();
    PdfCcittEncoder.CreateTerminatingWhiteCodes();
    PdfCcittEncoder.CreateTerminatingBlackCodes();
    PdfCcittEncoder.CreateVerticalTable();
  }

  private static void CreateTerminatingBlackCodes()
  {
    int[][] numArray1 = new int[109][];
    int[] numArray2 = new int[3]{ 10, 55, 0 };
    numArray1[0] = numArray2;
    numArray1[1] = new int[3]{ 3, 2, 1 };
    numArray1[2] = new int[3]{ 2, 3, 2 };
    numArray1[3] = new int[3]{ 2, 2, 3 };
    numArray1[4] = new int[3]{ 3, 3, 4 };
    numArray1[5] = new int[3]{ 4, 3, 5 };
    numArray1[6] = new int[3]{ 4, 2, 6 };
    numArray1[7] = new int[3]{ 5, 3, 7 };
    numArray1[8] = new int[3]{ 6, 5, 8 };
    numArray1[9] = new int[3]{ 6, 4, 9 };
    numArray1[10] = new int[3]{ 7, 4, 10 };
    numArray1[11] = new int[3]{ 7, 5, 11 };
    numArray1[12] = new int[3]{ 7, 7, 12 };
    numArray1[13] = new int[3]{ 8, 4, 13 };
    numArray1[14] = new int[3]{ 8, 7, 14 };
    numArray1[15] = new int[3]{ 9, 24, 15 };
    numArray1[16 /*0x10*/] = new int[3]
    {
      10,
      23,
      16 /*0x10*/
    };
    numArray1[17] = new int[3]{ 10, 24, 17 };
    numArray1[18] = new int[3]{ 10, 8, 18 };
    numArray1[19] = new int[3]{ 11, 103, 19 };
    numArray1[20] = new int[3]{ 11, 104, 20 };
    numArray1[21] = new int[3]{ 11, 108, 21 };
    numArray1[22] = new int[3]{ 11, 55, 22 };
    numArray1[23] = new int[3]{ 11, 40, 23 };
    numArray1[24] = new int[3]{ 11, 23, 24 };
    numArray1[25] = new int[3]{ 11, 24, 25 };
    numArray1[26] = new int[3]{ 12, 202, 26 };
    numArray1[27] = new int[3]{ 12, 203, 27 };
    numArray1[28] = new int[3]{ 12, 204, 28 };
    numArray1[29] = new int[3]{ 12, 205, 29 };
    numArray1[30] = new int[3]{ 12, 104, 30 };
    numArray1[31 /*0x1F*/] = new int[3]
    {
      12,
      105,
      31 /*0x1F*/
    };
    numArray1[32 /*0x20*/] = new int[3]
    {
      12,
      106,
      32 /*0x20*/
    };
    numArray1[33] = new int[3]{ 12, 107, 33 };
    numArray1[34] = new int[3]{ 12, 210, 34 };
    numArray1[35] = new int[3]{ 12, 211, 35 };
    numArray1[36] = new int[3]{ 12, 212, 36 };
    numArray1[37] = new int[3]{ 12, 213, 37 };
    numArray1[38] = new int[3]{ 12, 214, 38 };
    numArray1[39] = new int[3]{ 12, 215, 39 };
    numArray1[40] = new int[3]{ 12, 108, 40 };
    numArray1[41] = new int[3]{ 12, 109, 41 };
    numArray1[42] = new int[3]{ 12, 218, 42 };
    numArray1[43] = new int[3]{ 12, 219, 43 };
    numArray1[44] = new int[3]{ 12, 84, 44 };
    numArray1[45] = new int[3]{ 12, 85, 45 };
    numArray1[46] = new int[3]{ 12, 86, 46 };
    numArray1[47] = new int[3]{ 12, 87, 47 };
    numArray1[48 /*0x30*/] = new int[3]
    {
      12,
      100,
      48 /*0x30*/
    };
    numArray1[49] = new int[3]{ 12, 101, 49 };
    numArray1[50] = new int[3]{ 12, 82, 50 };
    numArray1[51] = new int[3]{ 12, 83, 51 };
    numArray1[52] = new int[3]{ 12, 36, 52 };
    numArray1[53] = new int[3]{ 12, 55, 53 };
    numArray1[54] = new int[3]{ 12, 56, 54 };
    numArray1[55] = new int[3]{ 12, 39, 55 };
    numArray1[56] = new int[3]{ 12, 40, 56 };
    numArray1[57] = new int[3]{ 12, 88, 57 };
    numArray1[58] = new int[3]{ 12, 89, 58 };
    numArray1[59] = new int[3]{ 12, 43, 59 };
    numArray1[60] = new int[3]{ 12, 44, 60 };
    numArray1[61] = new int[3]{ 12, 90, 61 };
    numArray1[62] = new int[3]{ 12, 102, 62 };
    numArray1[63 /*0x3F*/] = new int[3]
    {
      12,
      103,
      63 /*0x3F*/
    };
    numArray1[64 /*0x40*/] = new int[3]
    {
      10,
      15,
      64 /*0x40*/
    };
    numArray1[65] = new int[3]{ 12, 200, 128 /*0x80*/ };
    numArray1[66] = new int[3]{ 12, 201, 192 /*0xC0*/ };
    numArray1[67] = new int[3]{ 12, 91, 256 /*0x0100*/ };
    numArray1[68] = new int[3]{ 12, 51, 320 };
    numArray1[69] = new int[3]{ 12, 52, 384 };
    numArray1[70] = new int[3]{ 12, 53, 448 };
    numArray1[71] = new int[3]{ 13, 108, 512 /*0x0200*/ };
    numArray1[72] = new int[3]{ 13, 109, 576 };
    numArray1[73] = new int[3]{ 13, 74, 640 };
    numArray1[74] = new int[3]{ 13, 75, 704 };
    numArray1[75] = new int[3]{ 13, 76, 768 /*0x0300*/ };
    numArray1[76] = new int[3]{ 13, 77, 832 };
    numArray1[77] = new int[3]{ 13, 114, 896 };
    numArray1[78] = new int[3]{ 13, 115, 960 };
    numArray1[79] = new int[3]{ 13, 116, 1024 /*0x0400*/ };
    numArray1[80 /*0x50*/] = new int[3]{ 13, 117, 1088 };
    numArray1[81] = new int[3]{ 13, 118, 1152 };
    numArray1[82] = new int[3]{ 13, 119, 1216 };
    numArray1[83] = new int[3]{ 13, 82, 1280 /*0x0500*/ };
    numArray1[84] = new int[3]{ 13, 83, 1344 };
    numArray1[85] = new int[3]{ 13, 84, 1408 };
    numArray1[86] = new int[3]{ 13, 85, 1472 };
    numArray1[87] = new int[3]{ 13, 90, 1536 /*0x0600*/ };
    numArray1[88] = new int[3]{ 13, 91, 1600 };
    numArray1[89] = new int[3]{ 13, 100, 1664 };
    numArray1[90] = new int[3]{ 13, 101, 1728 };
    numArray1[91] = new int[3]{ 11, 8, 1792 /*0x0700*/ };
    numArray1[92] = new int[3]{ 11, 12, 1856 };
    numArray1[93] = new int[3]{ 11, 13, 1920 };
    numArray1[94] = new int[3]{ 12, 18, 1984 };
    numArray1[95] = new int[3]{ 12, 19, 2048 /*0x0800*/ };
    numArray1[96 /*0x60*/] = new int[3]{ 12, 20, 2112 };
    numArray1[97] = new int[3]{ 12, 21, 2176 };
    numArray1[98] = new int[3]{ 12, 22, 2240 };
    numArray1[99] = new int[3]{ 12, 23, 2304 /*0x0900*/ };
    numArray1[100] = new int[3]{ 12, 28, 2368 };
    numArray1[101] = new int[3]{ 12, 29, 2432 };
    numArray1[102] = new int[3]{ 12, 30, 2496 };
    numArray1[103] = new int[3]
    {
      12,
      31 /*0x1F*/,
      2560 /*0x0A00*/
    };
    numArray1[104] = new int[3]{ 12, 1, -1 };
    numArray1[105] = new int[3]{ 9, 1, -2 };
    numArray1[106] = new int[3]{ 10, 1, -2 };
    numArray1[107] = new int[3]{ 11, 1, -2 };
    int[] numArray3 = new int[3]{ 12, 0, -2 };
    numArray1[108] = numArray3;
    PdfCcittEncoder.s_terminatingBlackCodes = numArray1;
  }

  private static void CreateTerminatingWhiteCodes()
  {
    int[][] numArray1 = new int[109][];
    int[] numArray2 = new int[3]{ 8, 53, 0 };
    numArray1[0] = numArray2;
    numArray1[1] = new int[3]{ 6, 7, 1 };
    numArray1[2] = new int[3]{ 4, 7, 2 };
    numArray1[3] = new int[3]{ 4, 8, 3 };
    numArray1[4] = new int[3]{ 4, 11, 4 };
    numArray1[5] = new int[3]{ 4, 12, 5 };
    numArray1[6] = new int[3]{ 4, 14, 6 };
    numArray1[7] = new int[3]{ 4, 15, 7 };
    numArray1[8] = new int[3]{ 5, 19, 8 };
    numArray1[9] = new int[3]{ 5, 20, 9 };
    numArray1[10] = new int[3]{ 5, 7, 10 };
    numArray1[11] = new int[3]{ 5, 8, 11 };
    numArray1[12] = new int[3]{ 6, 8, 12 };
    numArray1[13] = new int[3]{ 6, 3, 13 };
    numArray1[14] = new int[3]{ 6, 52, 14 };
    numArray1[15] = new int[3]{ 6, 53, 15 };
    numArray1[16 /*0x10*/] = new int[3]
    {
      6,
      42,
      16 /*0x10*/
    };
    numArray1[17] = new int[3]{ 6, 43, 17 };
    numArray1[18] = new int[3]{ 7, 39, 18 };
    numArray1[19] = new int[3]{ 7, 12, 19 };
    numArray1[20] = new int[3]{ 7, 8, 20 };
    numArray1[21] = new int[3]{ 7, 23, 21 };
    numArray1[22] = new int[3]{ 7, 3, 22 };
    numArray1[23] = new int[3]{ 7, 4, 23 };
    numArray1[24] = new int[3]{ 7, 40, 24 };
    numArray1[25] = new int[3]{ 7, 43, 25 };
    numArray1[26] = new int[3]{ 7, 19, 26 };
    numArray1[27] = new int[3]{ 7, 36, 27 };
    numArray1[28] = new int[3]{ 7, 24, 28 };
    numArray1[29] = new int[3]{ 8, 2, 29 };
    numArray1[30] = new int[3]{ 8, 3, 30 };
    numArray1[31 /*0x1F*/] = new int[3]
    {
      8,
      26,
      31 /*0x1F*/
    };
    numArray1[32 /*0x20*/] = new int[3]
    {
      8,
      27,
      32 /*0x20*/
    };
    numArray1[33] = new int[3]{ 8, 18, 33 };
    numArray1[34] = new int[3]{ 8, 19, 34 };
    numArray1[35] = new int[3]{ 8, 20, 35 };
    numArray1[36] = new int[3]{ 8, 21, 36 };
    numArray1[37] = new int[3]{ 8, 22, 37 };
    numArray1[38] = new int[3]{ 8, 23, 38 };
    numArray1[39] = new int[3]{ 8, 40, 39 };
    numArray1[40] = new int[3]{ 8, 41, 40 };
    numArray1[41] = new int[3]{ 8, 42, 41 };
    numArray1[42] = new int[3]{ 8, 43, 42 };
    numArray1[43] = new int[3]{ 8, 44, 43 };
    numArray1[44] = new int[3]{ 8, 45, 44 };
    numArray1[45] = new int[3]{ 8, 4, 45 };
    numArray1[46] = new int[3]{ 8, 5, 46 };
    numArray1[47] = new int[3]{ 8, 10, 47 };
    numArray1[48 /*0x30*/] = new int[3]
    {
      8,
      11,
      48 /*0x30*/
    };
    numArray1[49] = new int[3]{ 8, 82, 49 };
    numArray1[50] = new int[3]{ 8, 83, 50 };
    numArray1[51] = new int[3]{ 8, 84, 51 };
    numArray1[52] = new int[3]{ 8, 85, 52 };
    numArray1[53] = new int[3]{ 8, 36, 53 };
    numArray1[54] = new int[3]{ 8, 37, 54 };
    numArray1[55] = new int[3]{ 8, 88, 55 };
    numArray1[56] = new int[3]{ 8, 89, 56 };
    numArray1[57] = new int[3]{ 8, 90, 57 };
    numArray1[58] = new int[3]{ 8, 91, 58 };
    numArray1[59] = new int[3]{ 8, 74, 59 };
    numArray1[60] = new int[3]{ 8, 75, 60 };
    numArray1[61] = new int[3]{ 8, 50, 61 };
    numArray1[62] = new int[3]{ 8, 51, 62 };
    numArray1[63 /*0x3F*/] = new int[3]
    {
      8,
      52,
      63 /*0x3F*/
    };
    numArray1[64 /*0x40*/] = new int[3]
    {
      5,
      27,
      64 /*0x40*/
    };
    numArray1[65] = new int[3]{ 5, 18, 128 /*0x80*/ };
    numArray1[66] = new int[3]{ 6, 23, 192 /*0xC0*/ };
    numArray1[67] = new int[3]{ 7, 55, 256 /*0x0100*/ };
    numArray1[68] = new int[3]{ 8, 54, 320 };
    numArray1[69] = new int[3]{ 8, 55, 384 };
    numArray1[70] = new int[3]{ 8, 100, 448 };
    numArray1[71] = new int[3]{ 8, 101, 512 /*0x0200*/ };
    numArray1[72] = new int[3]{ 8, 104, 576 };
    numArray1[73] = new int[3]{ 8, 103, 640 };
    numArray1[74] = new int[3]{ 9, 204, 704 };
    numArray1[75] = new int[3]{ 9, 205, 768 /*0x0300*/ };
    numArray1[76] = new int[3]{ 9, 210, 832 };
    numArray1[77] = new int[3]{ 9, 211, 896 };
    numArray1[78] = new int[3]{ 9, 212, 960 };
    numArray1[79] = new int[3]{ 9, 213, 1024 /*0x0400*/ };
    numArray1[80 /*0x50*/] = new int[3]{ 9, 214, 1088 };
    numArray1[81] = new int[3]{ 9, 215, 1152 };
    numArray1[82] = new int[3]{ 9, 216, 1216 };
    numArray1[83] = new int[3]{ 9, 217, 1280 /*0x0500*/ };
    numArray1[84] = new int[3]{ 9, 218, 1344 };
    numArray1[85] = new int[3]{ 9, 219, 1408 };
    numArray1[86] = new int[3]{ 9, 152, 1472 };
    numArray1[87] = new int[3]{ 9, 153, 1536 /*0x0600*/ };
    numArray1[88] = new int[3]{ 9, 154, 1600 };
    numArray1[89] = new int[3]{ 6, 24, 1664 };
    numArray1[90] = new int[3]{ 9, 155, 1728 };
    numArray1[91] = new int[3]{ 11, 8, 1792 /*0x0700*/ };
    numArray1[92] = new int[3]{ 11, 12, 1856 };
    numArray1[93] = new int[3]{ 11, 13, 1920 };
    numArray1[94] = new int[3]{ 12, 18, 1984 };
    numArray1[95] = new int[3]{ 12, 19, 2048 /*0x0800*/ };
    numArray1[96 /*0x60*/] = new int[3]{ 12, 20, 2112 };
    numArray1[97] = new int[3]{ 12, 21, 2176 };
    numArray1[98] = new int[3]{ 12, 22, 2240 };
    numArray1[99] = new int[3]{ 12, 23, 2304 /*0x0900*/ };
    numArray1[100] = new int[3]{ 12, 28, 2368 };
    numArray1[101] = new int[3]{ 12, 29, 2432 };
    numArray1[102] = new int[3]{ 12, 30, 2496 };
    numArray1[103] = new int[3]
    {
      12,
      31 /*0x1F*/,
      2560 /*0x0A00*/
    };
    numArray1[104] = new int[3]{ 12, 1, -1 };
    numArray1[105] = new int[3]{ 9, 1, -2 };
    numArray1[106] = new int[3]{ 10, 1, -2 };
    numArray1[107] = new int[3]{ 11, 1, -2 };
    int[] numArray3 = new int[3]{ 12, 0, -2 };
    numArray1[108] = numArray3;
    PdfCcittEncoder.s_terminatingWhiteCodes = numArray1;
  }

  private static void CreateVerticalTable()
  {
    PdfCcittEncoder.s_verticalTable = new int[7][]
    {
      new int[3]{ 7, 3, 0 },
      new int[3]{ 6, 3, 0 },
      new int[3]{ 3, 3, 0 },
      new int[3]{ 1, 1, 0 },
      new int[3]{ 3, 2, 0 },
      new int[3]{ 6, 2, 0 },
      new int[3]{ 7, 2, 0 }
    };
  }

  private static void CreteTableOneSpan()
  {
    PdfCcittEncoder.s_tableOneSpan = new byte[256 /*0x0100*/]
    {
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 2,
      (byte) 2,
      (byte) 2,
      (byte) 2,
      (byte) 2,
      (byte) 2,
      (byte) 2,
      (byte) 2,
      (byte) 2,
      (byte) 2,
      (byte) 2,
      (byte) 2,
      (byte) 2,
      (byte) 2,
      (byte) 2,
      (byte) 2,
      (byte) 2,
      (byte) 2,
      (byte) 2,
      (byte) 2,
      (byte) 2,
      (byte) 2,
      (byte) 2,
      (byte) 2,
      (byte) 2,
      (byte) 2,
      (byte) 2,
      (byte) 2,
      (byte) 2,
      (byte) 2,
      (byte) 2,
      (byte) 2,
      (byte) 3,
      (byte) 3,
      (byte) 3,
      (byte) 3,
      (byte) 3,
      (byte) 3,
      (byte) 3,
      (byte) 3,
      (byte) 3,
      (byte) 3,
      (byte) 3,
      (byte) 3,
      (byte) 3,
      (byte) 3,
      (byte) 3,
      (byte) 3,
      (byte) 4,
      (byte) 4,
      (byte) 4,
      (byte) 4,
      (byte) 4,
      (byte) 4,
      (byte) 4,
      (byte) 4,
      (byte) 5,
      (byte) 5,
      (byte) 5,
      (byte) 5,
      (byte) 6,
      (byte) 6,
      (byte) 7,
      (byte) 8
    };
  }

  private static void CreteTableZeroSpan()
  {
    PdfCcittEncoder.s_tableZeroSpan = new byte[256 /*0x0100*/]
    {
      (byte) 8,
      (byte) 7,
      (byte) 6,
      (byte) 6,
      (byte) 5,
      (byte) 5,
      (byte) 5,
      (byte) 5,
      (byte) 4,
      (byte) 4,
      (byte) 4,
      (byte) 4,
      (byte) 4,
      (byte) 4,
      (byte) 4,
      (byte) 4,
      (byte) 3,
      (byte) 3,
      (byte) 3,
      (byte) 3,
      (byte) 3,
      (byte) 3,
      (byte) 3,
      (byte) 3,
      (byte) 3,
      (byte) 3,
      (byte) 3,
      (byte) 3,
      (byte) 3,
      (byte) 3,
      (byte) 3,
      (byte) 3,
      (byte) 2,
      (byte) 2,
      (byte) 2,
      (byte) 2,
      (byte) 2,
      (byte) 2,
      (byte) 2,
      (byte) 2,
      (byte) 2,
      (byte) 2,
      (byte) 2,
      (byte) 2,
      (byte) 2,
      (byte) 2,
      (byte) 2,
      (byte) 2,
      (byte) 2,
      (byte) 2,
      (byte) 2,
      (byte) 2,
      (byte) 2,
      (byte) 2,
      (byte) 2,
      (byte) 2,
      (byte) 2,
      (byte) 2,
      (byte) 2,
      (byte) 2,
      (byte) 2,
      (byte) 2,
      (byte) 2,
      (byte) 2,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0
    };
  }

  public byte[] EncodeData(byte[] data, int width, int height)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    this.m_rowPixels = width;
    this.m_rowbytes = (int) Math.Ceiling((double) this.m_rowPixels / 8.0);
    this.m_refline = new byte[this.m_rowbytes];
    this.m_imageData = data;
    this.m_offsetData = 0;
    for (int index = this.m_rowbytes * height; index > 0; index -= this.m_rowbytes)
    {
      this.Fax3Encode();
      Array.Copy((Array) this.m_imageData, this.m_offsetData, (Array) this.m_refline, 0, this.m_rowbytes);
      this.m_offsetData += this.m_rowbytes;
    }
    this.Fax4Encode();
    byte[] numArray = new byte[this.m_outBuf.Count];
    int index1 = 0;
    for (int count = this.m_outBuf.Count; index1 < count; ++index1)
      numArray[index1] = this.m_outBuf[index1];
    return numArray;
  }

  private void Fax3Encode()
  {
    int num1 = 0;
    int num2 = this.Pixel(this.m_imageData, this.m_offsetData, 0) != 0 ? 0 : this.Finddiff(this.m_imageData, this.m_offsetData, 0, this.m_rowPixels, 0);
    int num3 = this.Pixel(this.m_refline, 0, 0) != 0 ? 0 : this.Finddiff(this.m_refline, 0, 0, this.m_rowPixels, 0);
    while (true)
    {
      int num4 = this.Finddiff2(this.m_refline, 0, num3, this.m_rowPixels, this.Pixel(this.m_refline, 0, num3));
      if (num4 >= num2)
      {
        int num5 = num3 - num2;
        if (-3 > num5 || num5 > 3)
        {
          int num6 = this.Finddiff2(this.m_imageData, this.m_offsetData, num2, this.m_rowPixels, this.Pixel(this.m_imageData, this.m_offsetData, num2));
          this.Putcode(PdfCcittEncoder.s_horizontalTabel);
          if (num1 + num2 == 0 || this.Pixel(this.m_imageData, this.m_offsetData, num1) == 0)
          {
            this.PutSpan(num2 - num1, PdfCcittEncoder.s_terminatingWhiteCodes);
            this.PutSpan(num6 - num2, PdfCcittEncoder.s_terminatingBlackCodes);
          }
          else
          {
            this.PutSpan(num2 - num1, PdfCcittEncoder.s_terminatingBlackCodes);
            this.PutSpan(num6 - num2, PdfCcittEncoder.s_terminatingWhiteCodes);
          }
          num1 = num6;
        }
        else
        {
          this.Putcode(PdfCcittEncoder.s_verticalTable[num5 + 3]);
          num1 = num2;
        }
      }
      else
      {
        this.Putcode(PdfCcittEncoder.s_passcode);
        num1 = num4;
      }
      if (num1 < this.m_rowPixels)
      {
        num2 = this.Finddiff(this.m_imageData, this.m_offsetData, num1, this.m_rowPixels, this.Pixel(this.m_imageData, this.m_offsetData, num1));
        num3 = this.Finddiff(this.m_refline, 0, this.Finddiff(this.m_refline, 0, num1, this.m_rowPixels, this.Pixel(this.m_imageData, this.m_offsetData, num1) ^ 1), this.m_rowPixels, this.Pixel(this.m_imageData, this.m_offsetData, num1));
      }
      else
        break;
    }
  }

  private void Fax4Encode()
  {
    this.PutBits(1, 12);
    this.PutBits(1, 12);
    if (this.m_countBit == 8)
      return;
    this.m_outBuf.Add((byte) this.m_data);
    this.m_data = 0;
    this.m_countBit = 8;
  }

  private int Finddiff(byte[] bp, int offset, int bs, int be, int color)
  {
    return bs + (color != 0 ? this.FindFirstSpan(bp, offset, bs, be) : this.FindZeroSpan(bp, offset, bs, be));
  }

  private int Finddiff2(byte[] bp, int offset, int bs, int be, int color)
  {
    return bs >= be ? be : this.Finddiff(bp, offset, bs, be, color);
  }

  private int FindFirstSpan(byte[] bp, int offset, int bs, int be)
  {
    int num1 = be - bs;
    int index = offset + (bs >> 3);
    int num2;
    int firstSpan;
    if (num1 > 0 && (num2 = bs & 7) != 0)
    {
      firstSpan = (int) PdfCcittEncoder.s_tableOneSpan[(int) bp[index] << num2 & (int) byte.MaxValue];
      if (firstSpan > 8 - num2)
        firstSpan = 8 - num2;
      if (firstSpan > num1)
        firstSpan = num1;
      if (num2 + firstSpan < 8)
        return firstSpan;
      num1 -= firstSpan;
      ++index;
    }
    else
      firstSpan = 0;
    while (num1 >= 8)
    {
      if (bp[index] != byte.MaxValue)
        return firstSpan + (int) PdfCcittEncoder.s_tableOneSpan[(int) bp[index] & (int) byte.MaxValue];
      firstSpan += 8;
      num1 -= 8;
      ++index;
    }
    if (num1 > 0)
    {
      int num3 = (int) PdfCcittEncoder.s_tableOneSpan[(int) bp[index] & (int) byte.MaxValue];
      firstSpan += num3 > num1 ? num1 : num3;
    }
    return firstSpan;
  }

  private int FindZeroSpan(byte[] bp, int offset, int bs, int be)
  {
    int num1 = be - bs;
    int index = offset + (bs >> 3);
    int num2;
    int zeroSpan;
    if (num1 > 0 && (num2 = bs & 7) != 0)
    {
      zeroSpan = (int) PdfCcittEncoder.s_tableZeroSpan[(int) bp[index] << num2 & (int) byte.MaxValue];
      if (zeroSpan > 8 - num2)
        zeroSpan = 8 - num2;
      if (zeroSpan > num1)
        zeroSpan = num1;
      if (num2 + zeroSpan < 8)
        return zeroSpan;
      num1 -= zeroSpan;
      ++index;
    }
    else
      zeroSpan = 0;
    while (num1 >= 8)
    {
      if (bp[index] != (byte) 0)
        return zeroSpan + (int) PdfCcittEncoder.s_tableZeroSpan[(int) bp[index] & (int) byte.MaxValue];
      zeroSpan += 8;
      num1 -= 8;
      ++index;
    }
    if (num1 > 0)
    {
      int num3 = (int) PdfCcittEncoder.s_tableZeroSpan[(int) bp[index] & (int) byte.MaxValue];
      zeroSpan += num3 > num1 ? num1 : num3;
    }
    return zeroSpan;
  }

  private int Pixel(byte[] data, int offset, int bit)
  {
    int num = 0;
    if (bit < this.m_rowPixels)
      num = ((int) data[offset + (bit >> 3)] & (int) byte.MaxValue) >> 7 - (bit & 7) & 1;
    return num;
  }

  private void PutBits(int bits, int length)
  {
    for (; length > this.m_countBit; this.m_countBit = 8)
    {
      this.m_data |= bits >> length - this.m_countBit;
      length -= this.m_countBit;
      this.m_outBuf.Add((byte) this.m_data);
      this.m_data = 0;
    }
    this.m_data |= (bits & PdfCcittEncoder.s_maskTabel[length]) << this.m_countBit - length;
    this.m_countBit -= length;
    if (this.m_countBit != 0)
      return;
    this.m_outBuf.Add((byte) this.m_data);
    this.m_data = 0;
    this.m_countBit = 8;
  }

  private void Putcode(int[] table) => this.PutBits(table[1], table[0]);

  private void PutSpan(int span, int[][] tab)
  {
    int[] numArray1;
    for (; span >= 2624; span -= numArray1[2])
    {
      numArray1 = tab[103];
      this.PutBits(numArray1[1], numArray1[0]);
    }
    if (span >= 64 /*0x40*/)
    {
      int[] numArray2 = tab[63 /*0x3F*/ + (span >> 6)];
      this.PutBits(numArray2[1], numArray2[0]);
      span -= numArray2[2];
    }
    this.PutBits(tab[span][1], tab[span][0]);
  }
}
