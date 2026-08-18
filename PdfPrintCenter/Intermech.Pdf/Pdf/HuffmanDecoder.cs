// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.HuffmanDecoder
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System.Globalization;

#nullable disable
namespace Syncfusion.Pdf;

internal class HuffmanDecoder
{
  internal int[][] huffmanTableA;
  internal int[][] huffmanTableB;
  internal int[][] huffmanTableC;
  internal int[][] huffmanTableD;
  internal int[][] huffmanTableE;
  internal int[][] huffmanTableF;
  internal int[][] huffmanTableG;
  internal int[][] huffmanTableH;
  internal int[][] huffmanTableI;
  internal int[][] huffmanTableJ;
  internal int[][] huffmanTableK;
  internal int[][] huffmanTableL;
  internal int[][] huffmanTableM;
  internal int[][] huffmanTableN;
  internal int[][] huffmanTableO;
  internal int jbig2HuffmanEOT;
  internal int jbig2HuffmanLOW;
  internal int jbig2HuffmanOOB;
  private Jbig2StreamReader reader;

  internal HuffmanDecoder()
  {
    this.jbig2HuffmanLOW = int.Parse("fffffffd", NumberStyles.HexNumber);
    this.jbig2HuffmanOOB = int.Parse("fffffffe", NumberStyles.HexNumber);
    this.jbig2HuffmanEOT = int.Parse("ffffffff", NumberStyles.HexNumber);
    this.Initialize();
  }

  internal HuffmanDecoder(Jbig2StreamReader reader)
  {
    this.jbig2HuffmanLOW = int.Parse("fffffffd", NumberStyles.HexNumber);
    this.jbig2HuffmanOOB = int.Parse("fffffffe", NumberStyles.HexNumber);
    this.jbig2HuffmanEOT = int.Parse("ffffffff", NumberStyles.HexNumber);
    this.reader = reader;
    this.Initialize();
  }

  internal int[][] BuildTable(int[][] table, int length)
  {
    int index1;
    for (index1 = 0; index1 < length; ++index1)
    {
      int index2 = index1;
      while (index2 < length && table[index2][1] == 0)
        ++index2;
      if (index2 != length)
      {
        for (int index3 = index2 + 1; index3 < length; ++index3)
        {
          if (table[index3][1] > 0 && table[index3][1] < table[index2][1])
            index2 = index3;
        }
        if (index2 != index1)
        {
          int[] numArray = table[index2];
          for (int index4 = index2; index4 > index1; --index4)
            table[index4] = table[index4 - 1];
          table[index1] = numArray;
        }
      }
      else
        break;
    }
    table[index1] = table[length];
    int num1 = 0;
    int num2 = 0;
    int[][] numArray1 = table;
    int index5 = num1;
    int index6 = index5 + 1;
    int[] numArray2 = numArray1[index5];
    int num3 = num2;
    int num4 = num3 + 1;
    numArray2[3] = num3;
    for (; table[index6][2] != this.jbig2HuffmanEOT; ++index6)
    {
      int num5 = num4 << table[index6][1] - table[index6 - 1][1];
      int[] numArray3 = table[index6];
      int num6 = num5;
      num4 = num6 + 1;
      numArray3[3] = num6;
    }
    return table;
  }

  internal DecodeIntResult DecodeInt(int[][] table)
  {
    int num1 = 0;
    int num2 = 0;
    for (int index = 0; table[index][2] != this.jbig2HuffmanEOT; ++index)
    {
      for (; num1 < table[index][1]; ++num1)
      {
        int num3 = this.reader.ReadBit();
        num2 = num2 << 1 | num3;
      }
      if (num2 == table[index][3])
      {
        if (table[index][2] == this.jbig2HuffmanOOB)
          return new DecodeIntResult(-1, false);
        int intResult;
        if (table[index][2] == this.jbig2HuffmanLOW)
        {
          int num4 = this.reader.ReadBits(32 /*0x20*/);
          intResult = table[index][0] - num4;
        }
        else if (table[index][2] > 0)
        {
          int num5 = this.reader.ReadBits(table[index][2]);
          intResult = table[index][0] + num5;
        }
        else
          intResult = table[index][0];
        return new DecodeIntResult(intResult, true);
      }
    }
    return new DecodeIntResult(-1, false);
  }

  internal void Initialize()
  {
    int[][] numArray1 = new int[5][];
    int[] numArray2 = new int[4]{ 0, 1, 4, 0 };
    numArray1[0] = numArray2;
    numArray1[1] = new int[4]{ 16 /*0x10*/, 2, 8, 2 };
    numArray1[2] = new int[4]{ 272, 3, 16 /*0x10*/, 6 };
    numArray1[3] = new int[4]{ 65808, 3, 32 /*0x20*/, 7 };
    int[] numArray3 = new int[4]
    {
      0,
      0,
      this.jbig2HuffmanEOT,
      0
    };
    numArray1[4] = numArray3;
    this.huffmanTableA = numArray1;
    int[][] numArray4 = new int[8][];
    int[] numArray5 = new int[4]{ 0, 1, 0, 0 };
    numArray4[0] = numArray5;
    numArray4[1] = new int[4]{ 1, 2, 0, 2 };
    numArray4[2] = new int[4]{ 2, 3, 0, 6 };
    numArray4[3] = new int[4]{ 3, 4, 3, 14 };
    numArray4[4] = new int[4]{ 11, 5, 6, 30 };
    numArray4[5] = new int[4]{ 75, 6, 32 /*0x20*/, 62 };
    int[] numArray6 = new int[4]
    {
      0,
      6,
      this.jbig2HuffmanOOB,
      63 /*0x3F*/
    };
    numArray4[6] = numArray6;
    int[] numArray7 = new int[4]
    {
      0,
      0,
      this.jbig2HuffmanEOT,
      0
    };
    numArray4[7] = numArray7;
    this.huffmanTableB = numArray4;
    int[][] numArray8 = new int[10][];
    int[] numArray9 = new int[4]{ 0, 1, 0, 0 };
    numArray8[0] = numArray9;
    numArray8[1] = new int[4]{ 1, 2, 0, 2 };
    numArray8[2] = new int[4]{ 2, 3, 0, 6 };
    numArray8[3] = new int[4]{ 3, 4, 3, 14 };
    numArray8[4] = new int[4]{ 11, 5, 6, 30 };
    int[] numArray10 = new int[4]
    {
      0,
      6,
      this.jbig2HuffmanOOB,
      62
    };
    numArray8[5] = numArray10;
    numArray8[6] = new int[4]{ 75, 7, 32 /*0x20*/, 254 };
    numArray8[7] = new int[4]{ -256, 8, 8, 254 };
    int[] numArray11 = new int[4]
    {
      -257,
      8,
      this.jbig2HuffmanLOW,
      (int) byte.MaxValue
    };
    numArray8[8] = numArray11;
    int[] numArray12 = new int[4]
    {
      0,
      0,
      this.jbig2HuffmanEOT,
      0
    };
    numArray8[9] = numArray12;
    this.huffmanTableC = numArray8;
    int[][] numArray13 = new int[7][];
    int[] numArray14 = new int[4]{ 1, 1, 0, 0 };
    numArray13[0] = numArray14;
    numArray13[1] = new int[4]{ 2, 2, 0, 2 };
    numArray13[2] = new int[4]{ 3, 3, 0, 6 };
    numArray13[3] = new int[4]{ 4, 4, 3, 14 };
    numArray13[4] = new int[4]{ 12, 5, 6, 30 };
    numArray13[5] = new int[4]
    {
      76,
      5,
      32 /*0x20*/,
      31 /*0x1F*/
    };
    int[] numArray15 = new int[4]
    {
      0,
      0,
      this.jbig2HuffmanEOT,
      0
    };
    numArray13[6] = numArray15;
    this.huffmanTableD = numArray13;
    int[][] numArray16 = new int[9][];
    int[] numArray17 = new int[4]{ 1, 1, 0, 0 };
    numArray16[0] = numArray17;
    numArray16[1] = new int[4]{ 2, 2, 0, 2 };
    numArray16[2] = new int[4]{ 3, 3, 0, 6 };
    numArray16[3] = new int[4]{ 4, 4, 3, 14 };
    numArray16[4] = new int[4]{ 12, 5, 6, 30 };
    numArray16[5] = new int[4]{ 76, 6, 32 /*0x20*/, 62 };
    numArray16[6] = new int[4]{ -255, 7, 8, 126 };
    int[] numArray18 = new int[4]
    {
      -256,
      7,
      this.jbig2HuffmanLOW,
      (int) sbyte.MaxValue
    };
    numArray16[7] = numArray18;
    int[] numArray19 = new int[4]
    {
      0,
      0,
      this.jbig2HuffmanEOT,
      0
    };
    numArray16[8] = numArray19;
    this.huffmanTableE = numArray16;
    int[][] numArray20 = new int[15][];
    int[] numArray21 = new int[4]{ 0, 2, 7, 0 };
    numArray20[0] = numArray21;
    numArray20[1] = new int[4]{ 128 /*0x80*/, 3, 7, 2 };
    numArray20[2] = new int[4]{ 256 /*0x0100*/, 3, 8, 3 };
    numArray20[3] = new int[4]{ -1024, 4, 9, 8 };
    numArray20[4] = new int[4]{ -512, 4, 8, 9 };
    numArray20[5] = new int[4]{ -256, 4, 7, 10 };
    numArray20[6] = new int[4]{ -32, 4, 5, 11 };
    numArray20[7] = new int[4]{ 512 /*0x0200*/, 4, 9, 12 };
    numArray20[8] = new int[4]{ 1024 /*0x0400*/, 4, 10, 13 };
    numArray20[9] = new int[4]{ -2048, 5, 10, 28 };
    numArray20[10] = new int[4]
    {
      (int) sbyte.MinValue,
      5,
      6,
      29
    };
    numArray20[11] = new int[4]{ -64, 5, 5, 30 };
    int[] numArray22 = new int[4]
    {
      -2049,
      6,
      this.jbig2HuffmanLOW,
      62
    };
    numArray20[12] = numArray22;
    numArray20[13] = new int[4]
    {
      2048 /*0x0800*/,
      6,
      32 /*0x20*/,
      63 /*0x3F*/
    };
    int[] numArray23 = new int[4]
    {
      0,
      0,
      this.jbig2HuffmanEOT,
      0
    };
    numArray20[14] = numArray23;
    this.huffmanTableF = numArray20;
    int[][] numArray24 = new int[16 /*0x10*/][]
    {
      new int[4]{ -512, 3, 8, 0 },
      new int[4]{ 256 /*0x0100*/, 3, 8, 1 },
      new int[4]{ 512 /*0x0200*/, 3, 9, 2 },
      new int[4]{ 1024 /*0x0400*/, 3, 10, 3 },
      new int[4]{ -1024, 4, 9, 8 },
      new int[4]{ -256, 4, 7, 9 },
      new int[4]{ -32, 4, 5, 10 },
      new int[4]{ 0, 4, 5, 11 },
      new int[4]{ 128 /*0x80*/, 4, 7, 12 },
      new int[4]{ (int) sbyte.MinValue, 5, 6, 26 },
      new int[4]{ -64, 5, 5, 27 },
      new int[4]{ 32 /*0x20*/, 5, 5, 28 },
      new int[4]{ 64 /*0x40*/, 5, 6, 29 },
      null,
      null,
      null
    };
    int[] numArray25 = new int[4]
    {
      -1025,
      5,
      this.jbig2HuffmanLOW,
      30
    };
    numArray24[13] = numArray25;
    numArray24[14] = new int[4]
    {
      2048 /*0x0800*/,
      5,
      32 /*0x20*/,
      31 /*0x1F*/
    };
    int[] numArray26 = new int[4]
    {
      0,
      0,
      this.jbig2HuffmanEOT,
      0
    };
    numArray24[15] = numArray26;
    this.huffmanTableG = numArray24;
    int[][] numArray27 = new int[22][];
    int[] numArray28 = new int[4]{ 0, 2, 1, 0 };
    numArray27[0] = numArray28;
    int[] numArray29 = new int[4]
    {
      0,
      2,
      this.jbig2HuffmanOOB,
      1
    };
    numArray27[1] = numArray29;
    numArray27[2] = new int[4]{ 4, 3, 4, 4 };
    numArray27[3] = new int[4]{ -1, 4, 0, 10 };
    numArray27[4] = new int[4]{ 22, 4, 4, 11 };
    numArray27[5] = new int[4]{ 38, 4, 5, 12 };
    numArray27[6] = new int[4]{ 2, 5, 0, 26 };
    numArray27[7] = new int[4]{ 70, 5, 6, 27 };
    numArray27[8] = new int[4]{ 134, 5, 7, 28 };
    numArray27[9] = new int[4]{ 3, 6, 0, 58 };
    numArray27[10] = new int[4]{ 20, 6, 1, 59 };
    numArray27[11] = new int[4]{ 262, 6, 7, 60 };
    numArray27[12] = new int[4]{ 646, 6, 10, 61 };
    numArray27[13] = new int[4]{ -2, 7, 0, 124 };
    numArray27[14] = new int[4]{ 390, 7, 8, 125 };
    numArray27[15] = new int[4]{ -15, 8, 3, 252 };
    numArray27[16 /*0x10*/] = new int[4]{ -5, 8, 1, 253 };
    numArray27[17] = new int[4]{ -7, 9, 1, 508 };
    numArray27[18] = new int[4]{ -3, 9, 0, 509 };
    int[] numArray30 = new int[4]
    {
      -16,
      9,
      this.jbig2HuffmanLOW,
      510
    };
    numArray27[19] = numArray30;
    numArray27[20] = new int[4]
    {
      1670,
      9,
      32 /*0x20*/,
      511 /*0x01FF*/
    };
    int[] numArray31 = new int[4]
    {
      0,
      0,
      this.jbig2HuffmanEOT,
      0
    };
    numArray27[21] = numArray31;
    this.huffmanTableH = numArray27;
    int[][] numArray32 = new int[23][];
    int[] numArray33 = new int[4]
    {
      0,
      2,
      this.jbig2HuffmanOOB,
      0
    };
    numArray32[0] = numArray33;
    numArray32[1] = new int[4]{ -1, 3, 1, 2 };
    numArray32[2] = new int[4]{ 1, 3, 1, 3 };
    numArray32[3] = new int[4]{ 7, 3, 5, 4 };
    numArray32[4] = new int[4]{ -3, 4, 1, 10 };
    numArray32[5] = new int[4]{ 43, 4, 5, 11 };
    numArray32[6] = new int[4]{ 75, 4, 6, 12 };
    numArray32[7] = new int[4]{ 3, 5, 1, 26 };
    numArray32[8] = new int[4]{ 139, 5, 7, 27 };
    numArray32[9] = new int[4]{ 267, 5, 8, 28 };
    numArray32[10] = new int[4]{ 5, 6, 1, 58 };
    numArray32[11] = new int[4]{ 39, 6, 2, 59 };
    numArray32[12] = new int[4]{ 523, 6, 8, 60 };
    numArray32[13] = new int[4]{ 1291, 6, 11, 61 };
    numArray32[14] = new int[4]{ -5, 7, 1, 124 };
    numArray32[15] = new int[4]{ 779, 7, 9, 125 };
    numArray32[16 /*0x10*/] = new int[4]{ -31, 8, 4, 252 };
    numArray32[17] = new int[4]{ -11, 8, 2, 253 };
    numArray32[18] = new int[4]{ -15, 9, 2, 508 };
    numArray32[19] = new int[4]{ -7, 9, 1, 509 };
    int[] numArray34 = new int[4]
    {
      -32,
      9,
      this.jbig2HuffmanLOW,
      510
    };
    numArray32[20] = numArray34;
    numArray32[21] = new int[4]
    {
      3339,
      9,
      32 /*0x20*/,
      511 /*0x01FF*/
    };
    int[] numArray35 = new int[4]
    {
      0,
      0,
      this.jbig2HuffmanEOT,
      0
    };
    numArray32[22] = numArray35;
    this.huffmanTableI = numArray32;
    int[][] numArray36 = new int[22][];
    numArray36[0] = new int[4]{ -2, 2, 2, 0 };
    numArray36[1] = new int[4]{ 6, 2, 6, 1 };
    int[] numArray37 = new int[4]
    {
      0,
      2,
      this.jbig2HuffmanOOB,
      2
    };
    numArray36[2] = numArray37;
    numArray36[3] = new int[4]{ -3, 5, 0, 24 };
    numArray36[4] = new int[4]{ 2, 5, 0, 25 };
    numArray36[5] = new int[4]{ 70, 5, 5, 26 };
    numArray36[6] = new int[4]{ 3, 6, 0, 54 };
    numArray36[7] = new int[4]{ 102, 6, 5, 55 };
    numArray36[8] = new int[4]{ 134, 6, 6, 56 };
    numArray36[9] = new int[4]{ 198, 6, 7, 57 };
    numArray36[10] = new int[4]{ 326, 6, 8, 58 };
    numArray36[11] = new int[4]{ 582, 6, 9, 59 };
    numArray36[12] = new int[4]{ 1094, 6, 10, 60 };
    numArray36[13] = new int[4]{ -21, 7, 4, 122 };
    numArray36[14] = new int[4]{ -4, 7, 0, 123 };
    numArray36[15] = new int[4]{ 4, 7, 0, 124 };
    numArray36[16 /*0x10*/] = new int[4]{ 2118, 7, 11, 125 };
    numArray36[17] = new int[4]{ -5, 8, 0, 252 };
    numArray36[18] = new int[4]{ 5, 8, 0, 253 };
    int[] numArray38 = new int[4]
    {
      -22,
      8,
      this.jbig2HuffmanLOW,
      254
    };
    numArray36[19] = numArray38;
    numArray36[20] = new int[4]
    {
      4166,
      8,
      32 /*0x20*/,
      (int) byte.MaxValue
    };
    int[] numArray39 = new int[4]
    {
      0,
      0,
      this.jbig2HuffmanEOT,
      0
    };
    numArray36[21] = numArray39;
    this.huffmanTableJ = numArray36;
    int[][] numArray40 = new int[14][];
    int[] numArray41 = new int[4]{ 1, 1, 0, 0 };
    numArray40[0] = numArray41;
    numArray40[1] = new int[4]{ 2, 2, 1, 2 };
    numArray40[2] = new int[4]{ 4, 4, 0, 12 };
    numArray40[3] = new int[4]{ 5, 4, 1, 13 };
    numArray40[4] = new int[4]{ 7, 5, 1, 28 };
    numArray40[5] = new int[4]{ 9, 5, 2, 29 };
    numArray40[6] = new int[4]{ 13, 6, 2, 60 };
    numArray40[7] = new int[4]{ 17, 7, 2, 122 };
    numArray40[8] = new int[4]{ 21, 7, 3, 123 };
    numArray40[9] = new int[4]{ 29, 7, 4, 124 };
    numArray40[10] = new int[4]{ 45, 7, 5, 125 };
    numArray40[11] = new int[4]{ 77, 7, 6, 126 };
    numArray40[12] = new int[4]
    {
      141,
      7,
      32 /*0x20*/,
      (int) sbyte.MaxValue
    };
    int[] numArray42 = new int[4]
    {
      0,
      0,
      this.jbig2HuffmanEOT,
      0
    };
    numArray40[13] = numArray42;
    this.huffmanTableK = numArray40;
    int[][] numArray43 = new int[14][];
    int[] numArray44 = new int[4]{ 1, 1, 0, 0 };
    numArray43[0] = numArray44;
    numArray43[1] = new int[4]{ 2, 2, 0, 2 };
    numArray43[2] = new int[4]{ 3, 3, 1, 6 };
    numArray43[3] = new int[4]{ 5, 5, 0, 28 };
    numArray43[4] = new int[4]{ 6, 5, 1, 29 };
    numArray43[5] = new int[4]{ 8, 6, 1, 60 };
    numArray43[6] = new int[4]{ 10, 7, 0, 122 };
    numArray43[7] = new int[4]{ 11, 7, 1, 123 };
    numArray43[8] = new int[4]{ 13, 7, 2, 124 };
    numArray43[9] = new int[4]{ 17, 7, 3, 125 };
    numArray43[10] = new int[4]{ 25, 7, 4, 126 };
    numArray43[11] = new int[4]{ 41, 8, 5, 254 };
    numArray43[12] = new int[4]
    {
      73,
      8,
      32 /*0x20*/,
      (int) byte.MaxValue
    };
    int[] numArray45 = new int[4]
    {
      0,
      0,
      this.jbig2HuffmanEOT,
      0
    };
    numArray43[13] = numArray45;
    this.huffmanTableL = numArray43;
    int[][] numArray46 = new int[14][];
    int[] numArray47 = new int[4]{ 1, 1, 0, 0 };
    numArray46[0] = numArray47;
    numArray46[1] = new int[4]{ 2, 3, 0, 4 };
    numArray46[2] = new int[4]{ 7, 3, 3, 5 };
    numArray46[3] = new int[4]{ 3, 4, 0, 12 };
    numArray46[4] = new int[4]{ 5, 4, 1, 13 };
    numArray46[5] = new int[4]{ 4, 5, 0, 28 };
    numArray46[6] = new int[4]{ 15, 6, 1, 58 };
    numArray46[7] = new int[4]{ 17, 6, 2, 59 };
    numArray46[8] = new int[4]{ 21, 6, 3, 60 };
    numArray46[9] = new int[4]{ 29, 6, 4, 61 };
    numArray46[10] = new int[4]{ 45, 6, 5, 62 };
    numArray46[11] = new int[4]{ 77, 7, 6, 126 };
    numArray46[12] = new int[4]
    {
      141,
      7,
      32 /*0x20*/,
      (int) sbyte.MaxValue
    };
    int[] numArray48 = new int[4]
    {
      0,
      0,
      this.jbig2HuffmanEOT,
      0
    };
    numArray46[13] = numArray48;
    this.huffmanTableM = numArray46;
    int[][] numArray49 = new int[6][];
    int[] numArray50 = new int[4]{ 0, 1, 0, 0 };
    numArray49[0] = numArray50;
    numArray49[1] = new int[4]{ -2, 3, 0, 4 };
    numArray49[2] = new int[4]{ -1, 3, 0, 5 };
    numArray49[3] = new int[4]{ 1, 3, 0, 6 };
    numArray49[4] = new int[4]{ 2, 3, 0, 7 };
    int[] numArray51 = new int[4]
    {
      0,
      0,
      this.jbig2HuffmanEOT,
      0
    };
    numArray49[5] = numArray51;
    this.huffmanTableN = numArray49;
    int[][] numArray52 = new int[14][];
    int[] numArray53 = new int[4]{ 0, 1, 0, 0 };
    numArray52[0] = numArray53;
    numArray52[1] = new int[4]{ -1, 3, 0, 4 };
    numArray52[2] = new int[4]{ 1, 3, 0, 5 };
    numArray52[3] = new int[4]{ -2, 4, 0, 12 };
    numArray52[4] = new int[4]{ 2, 4, 0, 13 };
    numArray52[5] = new int[4]{ -4, 5, 1, 28 };
    numArray52[6] = new int[4]{ 3, 5, 1, 29 };
    numArray52[7] = new int[4]{ -8, 6, 2, 60 };
    numArray52[8] = new int[4]{ 5, 6, 2, 61 };
    numArray52[9] = new int[4]{ -24, 7, 4, 124 };
    numArray52[10] = new int[4]{ 9, 7, 4, 125 };
    int[] numArray54 = new int[4]
    {
      -25,
      7,
      this.jbig2HuffmanLOW,
      126
    };
    numArray52[11] = numArray54;
    numArray52[12] = new int[4]
    {
      25,
      7,
      32 /*0x20*/,
      (int) sbyte.MaxValue
    };
    int[] numArray55 = new int[4]
    {
      0,
      0,
      this.jbig2HuffmanEOT,
      0
    };
    numArray52[13] = numArray55;
    this.huffmanTableO = numArray52;
  }
}
