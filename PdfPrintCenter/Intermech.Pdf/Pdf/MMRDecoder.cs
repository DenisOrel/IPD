// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.MMRDecoder
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

#nullable disable
namespace Syncfusion.Pdf;

internal class MMRDecoder
{
  internal const int CcittEndOfLine = -2;
  private BitOperation m_bitOperation = new BitOperation();
  private int[][] m_blackTable1;
  private int[][] m_blackTable2;
  private int[][] m_blackTable3;
  private long m_buffer;
  private long m_bufferLength;
  private long m_noOfBytesRead;
  private Jbig2StreamReader m_reader;
  private int[][] m_twoDimensionalTable1;
  private int[][] m_whiteTable1;
  private int[][] m_whiteTable2;
  internal const int TwoDimensionalHorizontal = 1;
  internal const int TwoDimensionalPass = 0;
  internal const int TwoDimensionalVertical0 = 2;
  internal const int TwoDimensionalVerticalL1 = 4;
  internal const int TwoDimensionalVerticalL2 = 6;
  internal const int TwoDimensionalVerticalL3 = 8;
  internal const int TwoDimensionalVerticalR1 = 3;
  internal const int TwoDimensionalVerticalR2 = 5;
  internal const int TwoDimensionalVerticalR3 = 7;

  internal MMRDecoder(Jbig2StreamReader reader)
  {
    int[][] numArray1 = new int[128 /*0x80*/][];
    numArray1[0] = new int[2]{ -1, -1 };
    numArray1[1] = new int[2]{ -1, -1 };
    numArray1[2] = new int[2]{ 7, 8 };
    numArray1[3] = new int[2]{ 7, 7 };
    numArray1[4] = new int[2]{ 6, 6 };
    numArray1[5] = new int[2]{ 6, 6 };
    numArray1[6] = new int[2]{ 6, 5 };
    numArray1[7] = new int[2]{ 6, 5 };
    int[] numArray2 = new int[2]{ 4, 0 };
    numArray1[8] = numArray2;
    int[] numArray3 = new int[2]{ 4, 0 };
    numArray1[9] = numArray3;
    int[] numArray4 = new int[2]{ 4, 0 };
    numArray1[10] = numArray4;
    int[] numArray5 = new int[2]{ 4, 0 };
    numArray1[11] = numArray5;
    int[] numArray6 = new int[2]{ 4, 0 };
    numArray1[12] = numArray6;
    int[] numArray7 = new int[2]{ 4, 0 };
    numArray1[13] = numArray7;
    int[] numArray8 = new int[2]{ 4, 0 };
    numArray1[14] = numArray8;
    int[] numArray9 = new int[2]{ 4, 0 };
    numArray1[15] = numArray9;
    numArray1[16 /*0x10*/] = new int[2]{ 3, 1 };
    numArray1[17] = new int[2]{ 3, 1 };
    numArray1[18] = new int[2]{ 3, 1 };
    numArray1[19] = new int[2]{ 3, 1 };
    numArray1[20] = new int[2]{ 3, 1 };
    numArray1[21] = new int[2]{ 3, 1 };
    numArray1[22] = new int[2]{ 3, 1 };
    numArray1[23] = new int[2]{ 3, 1 };
    numArray1[24] = new int[2]{ 3, 1 };
    numArray1[25] = new int[2]{ 3, 1 };
    numArray1[26] = new int[2]{ 3, 1 };
    numArray1[27] = new int[2]{ 3, 1 };
    numArray1[28] = new int[2]{ 3, 1 };
    numArray1[29] = new int[2]{ 3, 1 };
    numArray1[30] = new int[2]{ 3, 1 };
    numArray1[31 /*0x1F*/] = new int[2]{ 3, 1 };
    numArray1[32 /*0x20*/] = new int[2]{ 3, 4 };
    numArray1[33] = new int[2]{ 3, 4 };
    numArray1[34] = new int[2]{ 3, 4 };
    numArray1[35] = new int[2]{ 3, 4 };
    numArray1[36] = new int[2]{ 3, 4 };
    numArray1[37] = new int[2]{ 3, 4 };
    numArray1[38] = new int[2]{ 3, 4 };
    numArray1[39] = new int[2]{ 3, 4 };
    numArray1[40] = new int[2]{ 3, 4 };
    numArray1[41] = new int[2]{ 3, 4 };
    numArray1[42] = new int[2]{ 3, 4 };
    numArray1[43] = new int[2]{ 3, 4 };
    numArray1[44] = new int[2]{ 3, 4 };
    numArray1[45] = new int[2]{ 3, 4 };
    numArray1[46] = new int[2]{ 3, 4 };
    numArray1[47] = new int[2]{ 3, 4 };
    numArray1[48 /*0x30*/] = new int[2]{ 3, 3 };
    numArray1[49] = new int[2]{ 3, 3 };
    numArray1[50] = new int[2]{ 3, 3 };
    numArray1[51] = new int[2]{ 3, 3 };
    numArray1[52] = new int[2]{ 3, 3 };
    numArray1[53] = new int[2]{ 3, 3 };
    numArray1[54] = new int[2]{ 3, 3 };
    numArray1[55] = new int[2]{ 3, 3 };
    numArray1[56] = new int[2]{ 3, 3 };
    numArray1[57] = new int[2]{ 3, 3 };
    numArray1[58] = new int[2]{ 3, 3 };
    numArray1[59] = new int[2]{ 3, 3 };
    numArray1[60] = new int[2]{ 3, 3 };
    numArray1[61] = new int[2]{ 3, 3 };
    numArray1[62] = new int[2]{ 3, 3 };
    numArray1[63 /*0x3F*/] = new int[2]{ 3, 3 };
    numArray1[64 /*0x40*/] = new int[2]{ 1, 2 };
    numArray1[65] = new int[2]{ 1, 2 };
    numArray1[66] = new int[2]{ 1, 2 };
    numArray1[67] = new int[2]{ 1, 2 };
    numArray1[68] = new int[2]{ 1, 2 };
    numArray1[69] = new int[2]{ 1, 2 };
    numArray1[70] = new int[2]{ 1, 2 };
    numArray1[71] = new int[2]{ 1, 2 };
    numArray1[72] = new int[2]{ 1, 2 };
    numArray1[73] = new int[2]{ 1, 2 };
    numArray1[74] = new int[2]{ 1, 2 };
    numArray1[75] = new int[2]{ 1, 2 };
    numArray1[76] = new int[2]{ 1, 2 };
    numArray1[77] = new int[2]{ 1, 2 };
    numArray1[78] = new int[2]{ 1, 2 };
    numArray1[79] = new int[2]{ 1, 2 };
    numArray1[80 /*0x50*/] = new int[2]{ 1, 2 };
    numArray1[81] = new int[2]{ 1, 2 };
    numArray1[82] = new int[2]{ 1, 2 };
    numArray1[83] = new int[2]{ 1, 2 };
    numArray1[84] = new int[2]{ 1, 2 };
    numArray1[85] = new int[2]{ 1, 2 };
    numArray1[86] = new int[2]{ 1, 2 };
    numArray1[87] = new int[2]{ 1, 2 };
    numArray1[88] = new int[2]{ 1, 2 };
    numArray1[89] = new int[2]{ 1, 2 };
    numArray1[90] = new int[2]{ 1, 2 };
    numArray1[91] = new int[2]{ 1, 2 };
    numArray1[92] = new int[2]{ 1, 2 };
    numArray1[93] = new int[2]{ 1, 2 };
    numArray1[94] = new int[2]{ 1, 2 };
    numArray1[95] = new int[2]{ 1, 2 };
    numArray1[96 /*0x60*/] = new int[2]{ 1, 2 };
    numArray1[97] = new int[2]{ 1, 2 };
    numArray1[98] = new int[2]{ 1, 2 };
    numArray1[99] = new int[2]{ 1, 2 };
    numArray1[100] = new int[2]{ 1, 2 };
    numArray1[101] = new int[2]{ 1, 2 };
    numArray1[102] = new int[2]{ 1, 2 };
    numArray1[103] = new int[2]{ 1, 2 };
    numArray1[104] = new int[2]{ 1, 2 };
    numArray1[105] = new int[2]{ 1, 2 };
    numArray1[106] = new int[2]{ 1, 2 };
    numArray1[107] = new int[2]{ 1, 2 };
    numArray1[108] = new int[2]{ 1, 2 };
    numArray1[109] = new int[2]{ 1, 2 };
    numArray1[110] = new int[2]{ 1, 2 };
    numArray1[111] = new int[2]{ 1, 2 };
    numArray1[112 /*0x70*/] = new int[2]{ 1, 2 };
    numArray1[113] = new int[2]{ 1, 2 };
    numArray1[114] = new int[2]{ 1, 2 };
    numArray1[115] = new int[2]{ 1, 2 };
    numArray1[116] = new int[2]{ 1, 2 };
    numArray1[117] = new int[2]{ 1, 2 };
    numArray1[118] = new int[2]{ 1, 2 };
    numArray1[119] = new int[2]{ 1, 2 };
    numArray1[120] = new int[2]{ 1, 2 };
    numArray1[121] = new int[2]{ 1, 2 };
    numArray1[122] = new int[2]{ 1, 2 };
    numArray1[123] = new int[2]{ 1, 2 };
    numArray1[124] = new int[2]{ 1, 2 };
    numArray1[125] = new int[2]{ 1, 2 };
    numArray1[126] = new int[2]{ 1, 2 };
    numArray1[(int) sbyte.MaxValue] = new int[2]{ 1, 2 };
    this.m_twoDimensionalTable1 = numArray1;
    this.m_whiteTable1 = new int[32 /*0x20*/][]
    {
      new int[2]{ -1, -1 },
      new int[2]{ 12, -2 },
      new int[2]{ -1, -1 },
      new int[2]{ -1, -1 },
      new int[2]{ -1, -1 },
      new int[2]{ -1, -1 },
      new int[2]{ -1, -1 },
      new int[2]{ -1, -1 },
      new int[2]{ -1, -1 },
      new int[2]{ -1, -1 },
      new int[2]{ -1, -1 },
      new int[2]{ -1, -1 },
      new int[2]{ -1, -1 },
      new int[2]{ -1, -1 },
      new int[2]{ -1, -1 },
      new int[2]{ -1, -1 },
      new int[2]{ 11, 1792 /*0x0700*/ },
      new int[2]{ 11, 1792 /*0x0700*/ },
      new int[2]{ 12, 1984 },
      new int[2]{ 12, 2048 /*0x0800*/ },
      new int[2]{ 12, 2112 },
      new int[2]{ 12, 2176 },
      new int[2]{ 12, 2240 },
      new int[2]{ 12, 2304 /*0x0900*/ },
      new int[2]{ 11, 1856 },
      new int[2]{ 11, 1856 },
      new int[2]{ 11, 1920 },
      new int[2]{ 11, 1920 },
      new int[2]{ 12, 2368 },
      new int[2]{ 12, 2432 },
      new int[2]{ 12, 2496 },
      new int[2]{ 12, 2560 /*0x0A00*/ }
    };
    int[][] numArray10 = new int[512 /*0x0200*/][];
    numArray10[0] = new int[2]{ -1, -1 };
    numArray10[1] = new int[2]{ -1, -1 };
    numArray10[2] = new int[2]{ -1, -1 };
    numArray10[3] = new int[2]{ -1, -1 };
    numArray10[4] = new int[2]{ 8, 29 };
    numArray10[5] = new int[2]{ 8, 29 };
    numArray10[6] = new int[2]{ 8, 30 };
    numArray10[7] = new int[2]{ 8, 30 };
    numArray10[8] = new int[2]{ 8, 45 };
    numArray10[9] = new int[2]{ 8, 45 };
    numArray10[10] = new int[2]{ 8, 46 };
    numArray10[11] = new int[2]{ 8, 46 };
    numArray10[12] = new int[2]{ 7, 22 };
    numArray10[13] = new int[2]{ 7, 22 };
    numArray10[14] = new int[2]{ 7, 22 };
    numArray10[15] = new int[2]{ 7, 22 };
    numArray10[16 /*0x10*/] = new int[2]{ 7, 23 };
    numArray10[17] = new int[2]{ 7, 23 };
    numArray10[18] = new int[2]{ 7, 23 };
    numArray10[19] = new int[2]{ 7, 23 };
    numArray10[20] = new int[2]{ 8, 47 };
    numArray10[21] = new int[2]{ 8, 47 };
    numArray10[22] = new int[2]{ 8, 48 /*0x30*/ };
    numArray10[23] = new int[2]{ 8, 48 /*0x30*/ };
    numArray10[24] = new int[2]{ 6, 13 };
    numArray10[25] = new int[2]{ 6, 13 };
    numArray10[26] = new int[2]{ 6, 13 };
    numArray10[27] = new int[2]{ 6, 13 };
    numArray10[28] = new int[2]{ 6, 13 };
    numArray10[29] = new int[2]{ 6, 13 };
    numArray10[30] = new int[2]{ 6, 13 };
    numArray10[31 /*0x1F*/] = new int[2]{ 6, 13 };
    numArray10[32 /*0x20*/] = new int[2]{ 7, 20 };
    numArray10[33] = new int[2]{ 7, 20 };
    numArray10[34] = new int[2]{ 7, 20 };
    numArray10[35] = new int[2]{ 7, 20 };
    numArray10[36] = new int[2]{ 8, 33 };
    numArray10[37] = new int[2]{ 8, 33 };
    numArray10[38] = new int[2]{ 8, 34 };
    numArray10[39] = new int[2]{ 8, 34 };
    numArray10[40] = new int[2]{ 8, 35 };
    numArray10[41] = new int[2]{ 8, 35 };
    numArray10[42] = new int[2]{ 8, 36 };
    numArray10[43] = new int[2]{ 8, 36 };
    numArray10[44] = new int[2]{ 8, 37 };
    numArray10[45] = new int[2]{ 8, 37 };
    numArray10[46] = new int[2]{ 8, 38 };
    numArray10[47] = new int[2]{ 8, 38 };
    numArray10[48 /*0x30*/] = new int[2]{ 7, 19 };
    numArray10[49] = new int[2]{ 7, 19 };
    numArray10[50] = new int[2]{ 7, 19 };
    numArray10[51] = new int[2]{ 7, 19 };
    numArray10[52] = new int[2]{ 8, 31 /*0x1F*/ };
    numArray10[53] = new int[2]{ 8, 31 /*0x1F*/ };
    numArray10[54] = new int[2]{ 8, 32 /*0x20*/ };
    numArray10[55] = new int[2]{ 8, 32 /*0x20*/ };
    numArray10[56] = new int[2]{ 6, 1 };
    numArray10[57] = new int[2]{ 6, 1 };
    numArray10[58] = new int[2]{ 6, 1 };
    numArray10[59] = new int[2]{ 6, 1 };
    numArray10[60] = new int[2]{ 6, 1 };
    numArray10[61] = new int[2]{ 6, 1 };
    numArray10[62] = new int[2]{ 6, 1 };
    numArray10[63 /*0x3F*/] = new int[2]{ 6, 1 };
    numArray10[64 /*0x40*/] = new int[2]{ 6, 12 };
    numArray10[65] = new int[2]{ 6, 12 };
    numArray10[66] = new int[2]{ 6, 12 };
    numArray10[67] = new int[2]{ 6, 12 };
    numArray10[68] = new int[2]{ 6, 12 };
    numArray10[69] = new int[2]{ 6, 12 };
    numArray10[70] = new int[2]{ 6, 12 };
    numArray10[71] = new int[2]{ 6, 12 };
    numArray10[72] = new int[2]{ 8, 53 };
    numArray10[73] = new int[2]{ 8, 53 };
    numArray10[74] = new int[2]{ 8, 54 };
    numArray10[75] = new int[2]{ 8, 54 };
    numArray10[76] = new int[2]{ 7, 26 };
    numArray10[77] = new int[2]{ 7, 26 };
    numArray10[78] = new int[2]{ 7, 26 };
    numArray10[79] = new int[2]{ 7, 26 };
    numArray10[80 /*0x50*/] = new int[2]{ 8, 39 };
    numArray10[81] = new int[2]{ 8, 39 };
    numArray10[82] = new int[2]{ 8, 40 };
    numArray10[83] = new int[2]{ 8, 40 };
    numArray10[84] = new int[2]{ 8, 41 };
    numArray10[85] = new int[2]{ 8, 41 };
    numArray10[86] = new int[2]{ 8, 42 };
    numArray10[87] = new int[2]{ 8, 42 };
    numArray10[88] = new int[2]{ 8, 43 };
    numArray10[89] = new int[2]{ 8, 43 };
    numArray10[90] = new int[2]{ 8, 44 };
    numArray10[91] = new int[2]{ 8, 44 };
    numArray10[92] = new int[2]{ 7, 21 };
    numArray10[93] = new int[2]{ 7, 21 };
    numArray10[94] = new int[2]{ 7, 21 };
    numArray10[95] = new int[2]{ 7, 21 };
    numArray10[96 /*0x60*/] = new int[2]{ 7, 28 };
    numArray10[97] = new int[2]{ 7, 28 };
    numArray10[98] = new int[2]{ 7, 28 };
    numArray10[99] = new int[2]{ 7, 28 };
    numArray10[100] = new int[2]{ 8, 61 };
    numArray10[101] = new int[2]{ 8, 61 };
    numArray10[102] = new int[2]{ 8, 62 };
    numArray10[103] = new int[2]{ 8, 62 };
    numArray10[104] = new int[2]{ 8, 63 /*0x3F*/ };
    numArray10[105] = new int[2]{ 8, 63 /*0x3F*/ };
    int[] numArray11 = new int[2]{ 8, 0 };
    numArray10[106] = numArray11;
    int[] numArray12 = new int[2]{ 8, 0 };
    numArray10[107] = numArray12;
    numArray10[108] = new int[2]{ 8, 320 };
    numArray10[109] = new int[2]{ 8, 320 };
    numArray10[110] = new int[2]{ 8, 384 };
    numArray10[111] = new int[2]{ 8, 384 };
    numArray10[112 /*0x70*/] = new int[2]{ 5, 10 };
    numArray10[113] = new int[2]{ 5, 10 };
    numArray10[114] = new int[2]{ 5, 10 };
    numArray10[115] = new int[2]{ 5, 10 };
    numArray10[116] = new int[2]{ 5, 10 };
    numArray10[117] = new int[2]{ 5, 10 };
    numArray10[118] = new int[2]{ 5, 10 };
    numArray10[119] = new int[2]{ 5, 10 };
    numArray10[120] = new int[2]{ 5, 10 };
    numArray10[121] = new int[2]{ 5, 10 };
    numArray10[122] = new int[2]{ 5, 10 };
    numArray10[123] = new int[2]{ 5, 10 };
    numArray10[124] = new int[2]{ 5, 10 };
    numArray10[125] = new int[2]{ 5, 10 };
    numArray10[126] = new int[2]{ 5, 10 };
    numArray10[(int) sbyte.MaxValue] = new int[2]{ 5, 10 };
    numArray10[128 /*0x80*/] = new int[2]{ 5, 11 };
    numArray10[129] = new int[2]{ 5, 11 };
    numArray10[130] = new int[2]{ 5, 11 };
    numArray10[131] = new int[2]{ 5, 11 };
    numArray10[132] = new int[2]{ 5, 11 };
    numArray10[133] = new int[2]{ 5, 11 };
    numArray10[134] = new int[2]{ 5, 11 };
    numArray10[135] = new int[2]{ 5, 11 };
    numArray10[136] = new int[2]{ 5, 11 };
    numArray10[137] = new int[2]{ 5, 11 };
    numArray10[138] = new int[2]{ 5, 11 };
    numArray10[139] = new int[2]{ 5, 11 };
    numArray10[140] = new int[2]{ 5, 11 };
    numArray10[141] = new int[2]{ 5, 11 };
    numArray10[142] = new int[2]{ 5, 11 };
    numArray10[143] = new int[2]{ 5, 11 };
    numArray10[144 /*0x90*/] = new int[2]{ 7, 27 };
    numArray10[145] = new int[2]{ 7, 27 };
    numArray10[146] = new int[2]{ 7, 27 };
    numArray10[147] = new int[2]{ 7, 27 };
    numArray10[148] = new int[2]{ 8, 59 };
    numArray10[149] = new int[2]{ 8, 59 };
    numArray10[150] = new int[2]{ 8, 60 };
    numArray10[151] = new int[2]{ 8, 60 };
    numArray10[152] = new int[2]{ 9, 1472 };
    numArray10[153] = new int[2]{ 9, 1536 /*0x0600*/ };
    numArray10[154] = new int[2]{ 9, 1600 };
    numArray10[155] = new int[2]{ 9, 1728 };
    numArray10[156] = new int[2]{ 7, 18 };
    numArray10[157] = new int[2]{ 7, 18 };
    numArray10[158] = new int[2]{ 7, 18 };
    numArray10[159] = new int[2]{ 7, 18 };
    numArray10[160 /*0xA0*/] = new int[2]{ 7, 24 };
    numArray10[161] = new int[2]{ 7, 24 };
    numArray10[162] = new int[2]{ 7, 24 };
    numArray10[163] = new int[2]{ 7, 24 };
    numArray10[164] = new int[2]{ 8, 49 };
    numArray10[165] = new int[2]{ 8, 49 };
    numArray10[166] = new int[2]{ 8, 50 };
    numArray10[167] = new int[2]{ 8, 50 };
    numArray10[168] = new int[2]{ 8, 51 };
    numArray10[169] = new int[2]{ 8, 51 };
    numArray10[170] = new int[2]{ 8, 52 };
    numArray10[171] = new int[2]{ 8, 52 };
    numArray10[172] = new int[2]{ 7, 25 };
    numArray10[173] = new int[2]{ 7, 25 };
    numArray10[174] = new int[2]{ 7, 25 };
    numArray10[175] = new int[2]{ 7, 25 };
    numArray10[176 /*0xB0*/] = new int[2]{ 8, 55 };
    numArray10[177] = new int[2]{ 8, 55 };
    numArray10[178] = new int[2]{ 8, 56 };
    numArray10[179] = new int[2]{ 8, 56 };
    numArray10[180] = new int[2]{ 8, 57 };
    numArray10[181] = new int[2]{ 8, 57 };
    numArray10[182] = new int[2]{ 8, 58 };
    numArray10[183] = new int[2]{ 8, 58 };
    numArray10[184] = new int[2]{ 6, 192 /*0xC0*/ };
    numArray10[185] = new int[2]{ 6, 192 /*0xC0*/ };
    numArray10[186] = new int[2]{ 6, 192 /*0xC0*/ };
    numArray10[187] = new int[2]{ 6, 192 /*0xC0*/ };
    numArray10[188] = new int[2]{ 6, 192 /*0xC0*/ };
    numArray10[189] = new int[2]{ 6, 192 /*0xC0*/ };
    numArray10[190] = new int[2]{ 6, 192 /*0xC0*/ };
    numArray10[191] = new int[2]{ 6, 192 /*0xC0*/ };
    numArray10[192 /*0xC0*/] = new int[2]{ 6, 1664 };
    numArray10[193] = new int[2]{ 6, 1664 };
    numArray10[194] = new int[2]{ 6, 1664 };
    numArray10[195] = new int[2]{ 6, 1664 };
    numArray10[196] = new int[2]{ 6, 1664 };
    numArray10[197] = new int[2]{ 6, 1664 };
    numArray10[198] = new int[2]{ 6, 1664 };
    numArray10[199] = new int[2]{ 6, 1664 };
    numArray10[200] = new int[2]{ 8, 448 };
    numArray10[201] = new int[2]{ 8, 448 };
    numArray10[202] = new int[2]{ 8, 512 /*0x0200*/ };
    numArray10[203] = new int[2]{ 8, 512 /*0x0200*/ };
    numArray10[204] = new int[2]{ 9, 704 };
    numArray10[205] = new int[2]{ 9, 768 /*0x0300*/ };
    numArray10[206] = new int[2]{ 8, 640 };
    numArray10[207] = new int[2]{ 8, 640 };
    numArray10[208 /*0xD0*/] = new int[2]{ 8, 576 };
    numArray10[209] = new int[2]{ 8, 576 };
    numArray10[210] = new int[2]{ 9, 832 };
    numArray10[211] = new int[2]{ 9, 896 };
    numArray10[212] = new int[2]{ 9, 960 };
    numArray10[213] = new int[2]{ 9, 1024 /*0x0400*/ };
    numArray10[214] = new int[2]{ 9, 1088 };
    numArray10[215] = new int[2]{ 9, 1152 };
    numArray10[216] = new int[2]{ 9, 1216 };
    numArray10[217] = new int[2]{ 9, 1280 /*0x0500*/ };
    numArray10[218] = new int[2]{ 9, 1344 };
    numArray10[219] = new int[2]{ 9, 1408 };
    numArray10[220] = new int[2]{ 7, 256 /*0x0100*/ };
    numArray10[221] = new int[2]{ 7, 256 /*0x0100*/ };
    numArray10[222] = new int[2]{ 7, 256 /*0x0100*/ };
    numArray10[223] = new int[2]{ 7, 256 /*0x0100*/ };
    numArray10[224 /*0xE0*/] = new int[2]{ 4, 2 };
    numArray10[225] = new int[2]{ 4, 2 };
    numArray10[226] = new int[2]{ 4, 2 };
    numArray10[227] = new int[2]{ 4, 2 };
    numArray10[228] = new int[2]{ 4, 2 };
    numArray10[229] = new int[2]{ 4, 2 };
    numArray10[230] = new int[2]{ 4, 2 };
    numArray10[231] = new int[2]{ 4, 2 };
    numArray10[232] = new int[2]{ 4, 2 };
    numArray10[233] = new int[2]{ 4, 2 };
    numArray10[234] = new int[2]{ 4, 2 };
    numArray10[235] = new int[2]{ 4, 2 };
    numArray10[236] = new int[2]{ 4, 2 };
    numArray10[237] = new int[2]{ 4, 2 };
    numArray10[238] = new int[2]{ 4, 2 };
    numArray10[239] = new int[2]{ 4, 2 };
    numArray10[240 /*0xF0*/] = new int[2]{ 4, 2 };
    numArray10[241] = new int[2]{ 4, 2 };
    numArray10[242] = new int[2]{ 4, 2 };
    numArray10[243] = new int[2]{ 4, 2 };
    numArray10[244] = new int[2]{ 4, 2 };
    numArray10[245] = new int[2]{ 4, 2 };
    numArray10[246] = new int[2]{ 4, 2 };
    numArray10[247] = new int[2]{ 4, 2 };
    numArray10[248] = new int[2]{ 4, 2 };
    numArray10[249] = new int[2]{ 4, 2 };
    numArray10[250] = new int[2]{ 4, 2 };
    numArray10[251] = new int[2]{ 4, 2 };
    numArray10[252] = new int[2]{ 4, 2 };
    numArray10[253] = new int[2]{ 4, 2 };
    numArray10[254] = new int[2]{ 4, 2 };
    numArray10[(int) byte.MaxValue] = new int[2]{ 4, 2 };
    numArray10[256 /*0x0100*/] = new int[2]{ 4, 3 };
    numArray10[257] = new int[2]{ 4, 3 };
    numArray10[258] = new int[2]{ 4, 3 };
    numArray10[259] = new int[2]{ 4, 3 };
    numArray10[260] = new int[2]{ 4, 3 };
    numArray10[261] = new int[2]{ 4, 3 };
    numArray10[262] = new int[2]{ 4, 3 };
    numArray10[263] = new int[2]{ 4, 3 };
    numArray10[264] = new int[2]{ 4, 3 };
    numArray10[265] = new int[2]{ 4, 3 };
    numArray10[266] = new int[2]{ 4, 3 };
    numArray10[267] = new int[2]{ 4, 3 };
    numArray10[268] = new int[2]{ 4, 3 };
    numArray10[269] = new int[2]{ 4, 3 };
    numArray10[270] = new int[2]{ 4, 3 };
    numArray10[271] = new int[2]{ 4, 3 };
    numArray10[272] = new int[2]{ 4, 3 };
    numArray10[273] = new int[2]{ 4, 3 };
    numArray10[274] = new int[2]{ 4, 3 };
    numArray10[275] = new int[2]{ 4, 3 };
    numArray10[276] = new int[2]{ 4, 3 };
    numArray10[277] = new int[2]{ 4, 3 };
    numArray10[278] = new int[2]{ 4, 3 };
    numArray10[279] = new int[2]{ 4, 3 };
    numArray10[280] = new int[2]{ 4, 3 };
    numArray10[281] = new int[2]{ 4, 3 };
    numArray10[282] = new int[2]{ 4, 3 };
    numArray10[283] = new int[2]{ 4, 3 };
    numArray10[284] = new int[2]{ 4, 3 };
    numArray10[285] = new int[2]{ 4, 3 };
    numArray10[286] = new int[2]{ 4, 3 };
    numArray10[287] = new int[2]{ 4, 3 };
    numArray10[288] = new int[2]{ 5, 128 /*0x80*/ };
    numArray10[289] = new int[2]{ 5, 128 /*0x80*/ };
    numArray10[290] = new int[2]{ 5, 128 /*0x80*/ };
    numArray10[291] = new int[2]{ 5, 128 /*0x80*/ };
    numArray10[292] = new int[2]{ 5, 128 /*0x80*/ };
    numArray10[293] = new int[2]{ 5, 128 /*0x80*/ };
    numArray10[294] = new int[2]{ 5, 128 /*0x80*/ };
    numArray10[295] = new int[2]{ 5, 128 /*0x80*/ };
    numArray10[296] = new int[2]{ 5, 128 /*0x80*/ };
    numArray10[297] = new int[2]{ 5, 128 /*0x80*/ };
    numArray10[298] = new int[2]{ 5, 128 /*0x80*/ };
    numArray10[299] = new int[2]{ 5, 128 /*0x80*/ };
    numArray10[300] = new int[2]{ 5, 128 /*0x80*/ };
    numArray10[301] = new int[2]{ 5, 128 /*0x80*/ };
    numArray10[302] = new int[2]{ 5, 128 /*0x80*/ };
    numArray10[303] = new int[2]{ 5, 128 /*0x80*/ };
    numArray10[304] = new int[2]{ 5, 8 };
    numArray10[305] = new int[2]{ 5, 8 };
    numArray10[306] = new int[2]{ 5, 8 };
    numArray10[307] = new int[2]{ 5, 8 };
    numArray10[308] = new int[2]{ 5, 8 };
    numArray10[309] = new int[2]{ 5, 8 };
    numArray10[310] = new int[2]{ 5, 8 };
    numArray10[311] = new int[2]{ 5, 8 };
    numArray10[312] = new int[2]{ 5, 8 };
    numArray10[313] = new int[2]{ 5, 8 };
    numArray10[314] = new int[2]{ 5, 8 };
    numArray10[315] = new int[2]{ 5, 8 };
    numArray10[316] = new int[2]{ 5, 8 };
    numArray10[317] = new int[2]{ 5, 8 };
    numArray10[318] = new int[2]{ 5, 8 };
    numArray10[319] = new int[2]{ 5, 8 };
    numArray10[320] = new int[2]{ 5, 9 };
    numArray10[321] = new int[2]{ 5, 9 };
    numArray10[322] = new int[2]{ 5, 9 };
    numArray10[323] = new int[2]{ 5, 9 };
    numArray10[324] = new int[2]{ 5, 9 };
    numArray10[325] = new int[2]{ 5, 9 };
    numArray10[326] = new int[2]{ 5, 9 };
    numArray10[327] = new int[2]{ 5, 9 };
    numArray10[328] = new int[2]{ 5, 9 };
    numArray10[329] = new int[2]{ 5, 9 };
    numArray10[330] = new int[2]{ 5, 9 };
    numArray10[331] = new int[2]{ 5, 9 };
    numArray10[332] = new int[2]{ 5, 9 };
    numArray10[333] = new int[2]{ 5, 9 };
    numArray10[334] = new int[2]{ 5, 9 };
    numArray10[335] = new int[2]{ 5, 9 };
    numArray10[336] = new int[2]{ 6, 16 /*0x10*/ };
    numArray10[337] = new int[2]{ 6, 16 /*0x10*/ };
    numArray10[338] = new int[2]{ 6, 16 /*0x10*/ };
    numArray10[339] = new int[2]{ 6, 16 /*0x10*/ };
    numArray10[340] = new int[2]{ 6, 16 /*0x10*/ };
    numArray10[341] = new int[2]{ 6, 16 /*0x10*/ };
    numArray10[342] = new int[2]{ 6, 16 /*0x10*/ };
    numArray10[343] = new int[2]{ 6, 16 /*0x10*/ };
    numArray10[344] = new int[2]{ 6, 17 };
    numArray10[345] = new int[2]{ 6, 17 };
    numArray10[346] = new int[2]{ 6, 17 };
    numArray10[347] = new int[2]{ 6, 17 };
    numArray10[348] = new int[2]{ 6, 17 };
    numArray10[349] = new int[2]{ 6, 17 };
    numArray10[350] = new int[2]{ 6, 17 };
    numArray10[351] = new int[2]{ 6, 17 };
    numArray10[352] = new int[2]{ 4, 4 };
    numArray10[353] = new int[2]{ 4, 4 };
    numArray10[354] = new int[2]{ 4, 4 };
    numArray10[355] = new int[2]{ 4, 4 };
    numArray10[356] = new int[2]{ 4, 4 };
    numArray10[357] = new int[2]{ 4, 4 };
    numArray10[358] = new int[2]{ 4, 4 };
    numArray10[359] = new int[2]{ 4, 4 };
    numArray10[360] = new int[2]{ 4, 4 };
    numArray10[361] = new int[2]{ 4, 4 };
    numArray10[362] = new int[2]{ 4, 4 };
    numArray10[363] = new int[2]{ 4, 4 };
    numArray10[364] = new int[2]{ 4, 4 };
    numArray10[365] = new int[2]{ 4, 4 };
    numArray10[366] = new int[2]{ 4, 4 };
    numArray10[367] = new int[2]{ 4, 4 };
    numArray10[368] = new int[2]{ 4, 4 };
    numArray10[369] = new int[2]{ 4, 4 };
    numArray10[370] = new int[2]{ 4, 4 };
    numArray10[371] = new int[2]{ 4, 4 };
    numArray10[372] = new int[2]{ 4, 4 };
    numArray10[373] = new int[2]{ 4, 4 };
    numArray10[374] = new int[2]{ 4, 4 };
    numArray10[375] = new int[2]{ 4, 4 };
    numArray10[376] = new int[2]{ 4, 4 };
    numArray10[377] = new int[2]{ 4, 4 };
    numArray10[378] = new int[2]{ 4, 4 };
    numArray10[379] = new int[2]{ 4, 4 };
    numArray10[380] = new int[2]{ 4, 4 };
    numArray10[381] = new int[2]{ 4, 4 };
    numArray10[382] = new int[2]{ 4, 4 };
    numArray10[383] = new int[2]{ 4, 4 };
    numArray10[384] = new int[2]{ 4, 5 };
    numArray10[385] = new int[2]{ 4, 5 };
    numArray10[386] = new int[2]{ 4, 5 };
    numArray10[387] = new int[2]{ 4, 5 };
    numArray10[388] = new int[2]{ 4, 5 };
    numArray10[389] = new int[2]{ 4, 5 };
    numArray10[390] = new int[2]{ 4, 5 };
    numArray10[391] = new int[2]{ 4, 5 };
    numArray10[392] = new int[2]{ 4, 5 };
    numArray10[393] = new int[2]{ 4, 5 };
    numArray10[394] = new int[2]{ 4, 5 };
    numArray10[395] = new int[2]{ 4, 5 };
    numArray10[396] = new int[2]{ 4, 5 };
    numArray10[397] = new int[2]{ 4, 5 };
    numArray10[398] = new int[2]{ 4, 5 };
    numArray10[399] = new int[2]{ 4, 5 };
    numArray10[400] = new int[2]{ 4, 5 };
    numArray10[401] = new int[2]{ 4, 5 };
    numArray10[402] = new int[2]{ 4, 5 };
    numArray10[403] = new int[2]{ 4, 5 };
    numArray10[404] = new int[2]{ 4, 5 };
    numArray10[405] = new int[2]{ 4, 5 };
    numArray10[406] = new int[2]{ 4, 5 };
    numArray10[407] = new int[2]{ 4, 5 };
    numArray10[408] = new int[2]{ 4, 5 };
    numArray10[409] = new int[2]{ 4, 5 };
    numArray10[410] = new int[2]{ 4, 5 };
    numArray10[411] = new int[2]{ 4, 5 };
    numArray10[412] = new int[2]{ 4, 5 };
    numArray10[413] = new int[2]{ 4, 5 };
    numArray10[414] = new int[2]{ 4, 5 };
    numArray10[415] = new int[2]{ 4, 5 };
    numArray10[416] = new int[2]{ 6, 14 };
    numArray10[417] = new int[2]{ 6, 14 };
    numArray10[418] = new int[2]{ 6, 14 };
    numArray10[419] = new int[2]{ 6, 14 };
    numArray10[420] = new int[2]{ 6, 14 };
    numArray10[421] = new int[2]{ 6, 14 };
    numArray10[422] = new int[2]{ 6, 14 };
    numArray10[423] = new int[2]{ 6, 14 };
    numArray10[424] = new int[2]{ 6, 15 };
    numArray10[425] = new int[2]{ 6, 15 };
    numArray10[426] = new int[2]{ 6, 15 };
    numArray10[427] = new int[2]{ 6, 15 };
    numArray10[428] = new int[2]{ 6, 15 };
    numArray10[429] = new int[2]{ 6, 15 };
    numArray10[430] = new int[2]{ 6, 15 };
    numArray10[431] = new int[2]{ 6, 15 };
    numArray10[432] = new int[2]{ 5, 64 /*0x40*/ };
    numArray10[433] = new int[2]{ 5, 64 /*0x40*/ };
    numArray10[434] = new int[2]{ 5, 64 /*0x40*/ };
    numArray10[435] = new int[2]{ 5, 64 /*0x40*/ };
    numArray10[436] = new int[2]{ 5, 64 /*0x40*/ };
    numArray10[437] = new int[2]{ 5, 64 /*0x40*/ };
    numArray10[438] = new int[2]{ 5, 64 /*0x40*/ };
    numArray10[439] = new int[2]{ 5, 64 /*0x40*/ };
    numArray10[440] = new int[2]{ 5, 64 /*0x40*/ };
    numArray10[441] = new int[2]{ 5, 64 /*0x40*/ };
    numArray10[442] = new int[2]{ 5, 64 /*0x40*/ };
    numArray10[443] = new int[2]{ 5, 64 /*0x40*/ };
    numArray10[444] = new int[2]{ 5, 64 /*0x40*/ };
    numArray10[445] = new int[2]{ 5, 64 /*0x40*/ };
    numArray10[446] = new int[2]{ 5, 64 /*0x40*/ };
    numArray10[447] = new int[2]{ 5, 64 /*0x40*/ };
    numArray10[448] = new int[2]{ 4, 6 };
    numArray10[449] = new int[2]{ 4, 6 };
    numArray10[450] = new int[2]{ 4, 6 };
    numArray10[451] = new int[2]{ 4, 6 };
    numArray10[452] = new int[2]{ 4, 6 };
    numArray10[453] = new int[2]{ 4, 6 };
    numArray10[454] = new int[2]{ 4, 6 };
    numArray10[455] = new int[2]{ 4, 6 };
    numArray10[456] = new int[2]{ 4, 6 };
    numArray10[457] = new int[2]{ 4, 6 };
    numArray10[458] = new int[2]{ 4, 6 };
    numArray10[459] = new int[2]{ 4, 6 };
    numArray10[460] = new int[2]{ 4, 6 };
    numArray10[461] = new int[2]{ 4, 6 };
    numArray10[462] = new int[2]{ 4, 6 };
    numArray10[463] = new int[2]{ 4, 6 };
    numArray10[464] = new int[2]{ 4, 6 };
    numArray10[465] = new int[2]{ 4, 6 };
    numArray10[466] = new int[2]{ 4, 6 };
    numArray10[467] = new int[2]{ 4, 6 };
    numArray10[468] = new int[2]{ 4, 6 };
    numArray10[469] = new int[2]{ 4, 6 };
    numArray10[470] = new int[2]{ 4, 6 };
    numArray10[471] = new int[2]{ 4, 6 };
    numArray10[472] = new int[2]{ 4, 6 };
    numArray10[473] = new int[2]{ 4, 6 };
    numArray10[474] = new int[2]{ 4, 6 };
    numArray10[475] = new int[2]{ 4, 6 };
    numArray10[476] = new int[2]{ 4, 6 };
    numArray10[477] = new int[2]{ 4, 6 };
    numArray10[478] = new int[2]{ 4, 6 };
    numArray10[479] = new int[2]{ 4, 6 };
    numArray10[480] = new int[2]{ 4, 7 };
    numArray10[481] = new int[2]{ 4, 7 };
    numArray10[482] = new int[2]{ 4, 7 };
    numArray10[483] = new int[2]{ 4, 7 };
    numArray10[484] = new int[2]{ 4, 7 };
    numArray10[485] = new int[2]{ 4, 7 };
    numArray10[486] = new int[2]{ 4, 7 };
    numArray10[487] = new int[2]{ 4, 7 };
    numArray10[488] = new int[2]{ 4, 7 };
    numArray10[489] = new int[2]{ 4, 7 };
    numArray10[490] = new int[2]{ 4, 7 };
    numArray10[491] = new int[2]{ 4, 7 };
    numArray10[492] = new int[2]{ 4, 7 };
    numArray10[493] = new int[2]{ 4, 7 };
    numArray10[494] = new int[2]{ 4, 7 };
    numArray10[495] = new int[2]{ 4, 7 };
    numArray10[496] = new int[2]{ 4, 7 };
    numArray10[497] = new int[2]{ 4, 7 };
    numArray10[498] = new int[2]{ 4, 7 };
    numArray10[499] = new int[2]{ 4, 7 };
    numArray10[500] = new int[2]{ 4, 7 };
    numArray10[501] = new int[2]{ 4, 7 };
    numArray10[502] = new int[2]{ 4, 7 };
    numArray10[503] = new int[2]{ 4, 7 };
    numArray10[504] = new int[2]{ 4, 7 };
    numArray10[505] = new int[2]{ 4, 7 };
    numArray10[506] = new int[2]{ 4, 7 };
    numArray10[507] = new int[2]{ 4, 7 };
    numArray10[508] = new int[2]{ 4, 7 };
    numArray10[509] = new int[2]{ 4, 7 };
    numArray10[510] = new int[2]{ 4, 7 };
    numArray10[511 /*0x01FF*/] = new int[2]{ 4, 7 };
    this.m_whiteTable2 = numArray10;
    this.m_blackTable1 = new int[128 /*0x80*/][]
    {
      new int[2]{ -1, -1 },
      new int[2]{ -1, -1 },
      new int[2]{ 12, -2 },
      new int[2]{ 12, -2 },
      new int[2]{ -1, -1 },
      new int[2]{ -1, -1 },
      new int[2]{ -1, -1 },
      new int[2]{ -1, -1 },
      new int[2]{ -1, -1 },
      new int[2]{ -1, -1 },
      new int[2]{ -1, -1 },
      new int[2]{ -1, -1 },
      new int[2]{ -1, -1 },
      new int[2]{ -1, -1 },
      new int[2]{ -1, -1 },
      new int[2]{ -1, -1 },
      new int[2]{ -1, -1 },
      new int[2]{ -1, -1 },
      new int[2]{ -1, -1 },
      new int[2]{ -1, -1 },
      new int[2]{ -1, -1 },
      new int[2]{ -1, -1 },
      new int[2]{ -1, -1 },
      new int[2]{ -1, -1 },
      new int[2]{ -1, -1 },
      new int[2]{ -1, -1 },
      new int[2]{ -1, -1 },
      new int[2]{ -1, -1 },
      new int[2]{ -1, -1 },
      new int[2]{ -1, -1 },
      new int[2]{ -1, -1 },
      new int[2]{ -1, -1 },
      new int[2]{ 11, 1792 /*0x0700*/ },
      new int[2]{ 11, 1792 /*0x0700*/ },
      new int[2]{ 11, 1792 /*0x0700*/ },
      new int[2]{ 11, 1792 /*0x0700*/ },
      new int[2]{ 12, 1984 },
      new int[2]{ 12, 1984 },
      new int[2]{ 12, 2048 /*0x0800*/ },
      new int[2]{ 12, 2048 /*0x0800*/ },
      new int[2]{ 12, 2112 },
      new int[2]{ 12, 2112 },
      new int[2]{ 12, 2176 },
      new int[2]{ 12, 2176 },
      new int[2]{ 12, 2240 },
      new int[2]{ 12, 2240 },
      new int[2]{ 12, 2304 /*0x0900*/ },
      new int[2]{ 12, 2304 /*0x0900*/ },
      new int[2]{ 11, 1856 },
      new int[2]{ 11, 1856 },
      new int[2]{ 11, 1856 },
      new int[2]{ 11, 1856 },
      new int[2]{ 11, 1920 },
      new int[2]{ 11, 1920 },
      new int[2]{ 11, 1920 },
      new int[2]{ 11, 1920 },
      new int[2]{ 12, 2368 },
      new int[2]{ 12, 2368 },
      new int[2]{ 12, 2432 },
      new int[2]{ 12, 2432 },
      new int[2]{ 12, 2496 },
      new int[2]{ 12, 2496 },
      new int[2]{ 12, 2560 /*0x0A00*/ },
      new int[2]{ 12, 2560 /*0x0A00*/ },
      new int[2]{ 10, 18 },
      new int[2]{ 10, 18 },
      new int[2]{ 10, 18 },
      new int[2]{ 10, 18 },
      new int[2]{ 10, 18 },
      new int[2]{ 10, 18 },
      new int[2]{ 10, 18 },
      new int[2]{ 10, 18 },
      new int[2]{ 12, 52 },
      new int[2]{ 12, 52 },
      new int[2]{ 13, 640 },
      new int[2]{ 13, 704 },
      new int[2]{ 13, 768 /*0x0300*/ },
      new int[2]{ 13, 832 },
      new int[2]{ 12, 55 },
      new int[2]{ 12, 55 },
      new int[2]{ 12, 56 },
      new int[2]{ 12, 56 },
      new int[2]{ 13, 1280 /*0x0500*/ },
      new int[2]{ 13, 1344 },
      new int[2]{ 13, 1408 },
      new int[2]{ 13, 1472 },
      new int[2]{ 12, 59 },
      new int[2]{ 12, 59 },
      new int[2]{ 12, 60 },
      new int[2]{ 12, 60 },
      new int[2]{ 13, 1536 /*0x0600*/ },
      new int[2]{ 13, 1600 },
      new int[2]{ 11, 24 },
      new int[2]{ 11, 24 },
      new int[2]{ 11, 24 },
      new int[2]{ 11, 24 },
      new int[2]{ 11, 25 },
      new int[2]{ 11, 25 },
      new int[2]{ 11, 25 },
      new int[2]{ 11, 25 },
      new int[2]{ 13, 1664 },
      new int[2]{ 13, 1728 },
      new int[2]{ 12, 320 },
      new int[2]{ 12, 320 },
      new int[2]{ 12, 384 },
      new int[2]{ 12, 384 },
      new int[2]{ 12, 448 },
      new int[2]{ 12, 448 },
      new int[2]{ 13, 512 /*0x0200*/ },
      new int[2]{ 13, 576 },
      new int[2]{ 12, 53 },
      new int[2]{ 12, 53 },
      new int[2]{ 12, 54 },
      new int[2]{ 12, 54 },
      new int[2]{ 13, 896 },
      new int[2]{ 13, 960 },
      new int[2]{ 13, 1024 /*0x0400*/ },
      new int[2]{ 13, 1088 },
      new int[2]{ 13, 1152 },
      new int[2]{ 13, 1216 },
      new int[2]{ 10, 64 /*0x40*/ },
      new int[2]{ 10, 64 /*0x40*/ },
      new int[2]{ 10, 64 /*0x40*/ },
      new int[2]{ 10, 64 /*0x40*/ },
      new int[2]{ 10, 64 /*0x40*/ },
      new int[2]{ 10, 64 /*0x40*/ },
      new int[2]{ 10, 64 /*0x40*/ },
      new int[2]{ 10, 64 /*0x40*/ }
    };
    int[][] numArray13 = new int[192 /*0xC0*/][]
    {
      new int[2]{ 8, 13 },
      new int[2]{ 8, 13 },
      new int[2]{ 8, 13 },
      new int[2]{ 8, 13 },
      new int[2]{ 8, 13 },
      new int[2]{ 8, 13 },
      new int[2]{ 8, 13 },
      new int[2]{ 8, 13 },
      new int[2]{ 8, 13 },
      new int[2]{ 8, 13 },
      new int[2]{ 8, 13 },
      new int[2]{ 8, 13 },
      new int[2]{ 8, 13 },
      new int[2]{ 8, 13 },
      new int[2]{ 8, 13 },
      new int[2]{ 8, 13 },
      new int[2]{ 11, 23 },
      new int[2]{ 11, 23 },
      new int[2]{ 12, 50 },
      new int[2]{ 12, 51 },
      new int[2]{ 12, 44 },
      new int[2]{ 12, 45 },
      new int[2]{ 12, 46 },
      new int[2]{ 12, 47 },
      new int[2]{ 12, 57 },
      new int[2]{ 12, 58 },
      new int[2]{ 12, 61 },
      new int[2]{ 12, 256 /*0x0100*/ },
      new int[2]{ 10, 16 /*0x10*/ },
      new int[2]{ 10, 16 /*0x10*/ },
      new int[2]{ 10, 16 /*0x10*/ },
      new int[2]{ 10, 16 /*0x10*/ },
      new int[2]{ 10, 17 },
      new int[2]{ 10, 17 },
      new int[2]{ 10, 17 },
      new int[2]{ 10, 17 },
      new int[2]{ 12, 48 /*0x30*/ },
      new int[2]{ 12, 49 },
      new int[2]{ 12, 62 },
      new int[2]{ 12, 63 /*0x3F*/ },
      new int[2]{ 12, 30 },
      new int[2]{ 12, 31 /*0x1F*/ },
      new int[2]{ 12, 32 /*0x20*/ },
      new int[2]{ 12, 33 },
      new int[2]{ 12, 40 },
      new int[2]{ 12, 41 },
      new int[2]{ 11, 22 },
      new int[2]{ 11, 22 },
      new int[2]{ 8, 14 },
      new int[2]{ 8, 14 },
      new int[2]{ 8, 14 },
      new int[2]{ 8, 14 },
      new int[2]{ 8, 14 },
      new int[2]{ 8, 14 },
      new int[2]{ 8, 14 },
      new int[2]{ 8, 14 },
      new int[2]{ 8, 14 },
      new int[2]{ 8, 14 },
      new int[2]{ 8, 14 },
      new int[2]{ 8, 14 },
      new int[2]{ 8, 14 },
      new int[2]{ 8, 14 },
      new int[2]{ 8, 14 },
      new int[2]{ 8, 14 },
      new int[2]{ 7, 10 },
      new int[2]{ 7, 10 },
      new int[2]{ 7, 10 },
      new int[2]{ 7, 10 },
      new int[2]{ 7, 10 },
      new int[2]{ 7, 10 },
      new int[2]{ 7, 10 },
      new int[2]{ 7, 10 },
      new int[2]{ 7, 10 },
      new int[2]{ 7, 10 },
      new int[2]{ 7, 10 },
      new int[2]{ 7, 10 },
      new int[2]{ 7, 10 },
      new int[2]{ 7, 10 },
      new int[2]{ 7, 10 },
      new int[2]{ 7, 10 },
      new int[2]{ 7, 10 },
      new int[2]{ 7, 10 },
      new int[2]{ 7, 10 },
      new int[2]{ 7, 10 },
      new int[2]{ 7, 10 },
      new int[2]{ 7, 10 },
      new int[2]{ 7, 10 },
      new int[2]{ 7, 10 },
      new int[2]{ 7, 10 },
      new int[2]{ 7, 10 },
      new int[2]{ 7, 10 },
      new int[2]{ 7, 10 },
      new int[2]{ 7, 10 },
      new int[2]{ 7, 10 },
      new int[2]{ 7, 10 },
      new int[2]{ 7, 10 },
      new int[2]{ 7, 11 },
      new int[2]{ 7, 11 },
      new int[2]{ 7, 11 },
      new int[2]{ 7, 11 },
      new int[2]{ 7, 11 },
      new int[2]{ 7, 11 },
      new int[2]{ 7, 11 },
      new int[2]{ 7, 11 },
      new int[2]{ 7, 11 },
      new int[2]{ 7, 11 },
      new int[2]{ 7, 11 },
      new int[2]{ 7, 11 },
      new int[2]{ 7, 11 },
      new int[2]{ 7, 11 },
      new int[2]{ 7, 11 },
      new int[2]{ 7, 11 },
      new int[2]{ 7, 11 },
      new int[2]{ 7, 11 },
      new int[2]{ 7, 11 },
      new int[2]{ 7, 11 },
      new int[2]{ 7, 11 },
      new int[2]{ 7, 11 },
      new int[2]{ 7, 11 },
      new int[2]{ 7, 11 },
      new int[2]{ 7, 11 },
      new int[2]{ 7, 11 },
      new int[2]{ 7, 11 },
      new int[2]{ 7, 11 },
      new int[2]{ 7, 11 },
      new int[2]{ 7, 11 },
      new int[2]{ 7, 11 },
      new int[2]{ 7, 11 },
      new int[2]{ 9, 15 },
      new int[2]{ 9, 15 },
      new int[2]{ 9, 15 },
      new int[2]{ 9, 15 },
      new int[2]{ 9, 15 },
      new int[2]{ 9, 15 },
      new int[2]{ 9, 15 },
      new int[2]{ 9, 15 },
      new int[2]{ 12, 128 /*0x80*/ },
      new int[2]{ 12, 192 /*0xC0*/ },
      new int[2]{ 12, 26 },
      new int[2]{ 12, 27 },
      new int[2]{ 12, 28 },
      new int[2]{ 12, 29 },
      new int[2]{ 11, 19 },
      new int[2]{ 11, 19 },
      new int[2]{ 11, 20 },
      new int[2]{ 11, 20 },
      new int[2]{ 12, 34 },
      new int[2]{ 12, 35 },
      new int[2]{ 12, 36 },
      new int[2]{ 12, 37 },
      new int[2]{ 12, 38 },
      new int[2]{ 12, 39 },
      new int[2]{ 11, 21 },
      new int[2]{ 11, 21 },
      new int[2]{ 12, 42 },
      new int[2]{ 12, 43 },
      null,
      null,
      null,
      null,
      null,
      null,
      null,
      null,
      null,
      null,
      null,
      null,
      null,
      null,
      null,
      null,
      null,
      null,
      null,
      null,
      null,
      null,
      null,
      null,
      null,
      null,
      null,
      null,
      null,
      null,
      null,
      null,
      null,
      null,
      null,
      null
    };
    int[] numArray14 = new int[2]{ 10, 0 };
    numArray13[156] = numArray14;
    int[] numArray15 = new int[2]{ 10, 0 };
    numArray13[157] = numArray15;
    int[] numArray16 = new int[2]{ 10, 0 };
    numArray13[158] = numArray16;
    int[] numArray17 = new int[2]{ 10, 0 };
    numArray13[159] = numArray17;
    numArray13[160 /*0xA0*/] = new int[2]{ 7, 12 };
    numArray13[161] = new int[2]{ 7, 12 };
    numArray13[162] = new int[2]{ 7, 12 };
    numArray13[163] = new int[2]{ 7, 12 };
    numArray13[164] = new int[2]{ 7, 12 };
    numArray13[165] = new int[2]{ 7, 12 };
    numArray13[166] = new int[2]{ 7, 12 };
    numArray13[167] = new int[2]{ 7, 12 };
    numArray13[168] = new int[2]{ 7, 12 };
    numArray13[169] = new int[2]{ 7, 12 };
    numArray13[170] = new int[2]{ 7, 12 };
    numArray13[171] = new int[2]{ 7, 12 };
    numArray13[172] = new int[2]{ 7, 12 };
    numArray13[173] = new int[2]{ 7, 12 };
    numArray13[174] = new int[2]{ 7, 12 };
    numArray13[175] = new int[2]{ 7, 12 };
    numArray13[176 /*0xB0*/] = new int[2]{ 7, 12 };
    numArray13[177] = new int[2]{ 7, 12 };
    numArray13[178] = new int[2]{ 7, 12 };
    numArray13[179] = new int[2]{ 7, 12 };
    numArray13[180] = new int[2]{ 7, 12 };
    numArray13[181] = new int[2]{ 7, 12 };
    numArray13[182] = new int[2]{ 7, 12 };
    numArray13[183] = new int[2]{ 7, 12 };
    numArray13[184] = new int[2]{ 7, 12 };
    numArray13[185] = new int[2]{ 7, 12 };
    numArray13[186] = new int[2]{ 7, 12 };
    numArray13[187] = new int[2]{ 7, 12 };
    numArray13[188] = new int[2]{ 7, 12 };
    numArray13[189] = new int[2]{ 7, 12 };
    numArray13[190] = new int[2]{ 7, 12 };
    numArray13[191] = new int[2]{ 7, 12 };
    this.m_blackTable2 = numArray13;
    this.m_blackTable3 = new int[64 /*0x40*/][]
    {
      new int[2]{ -1, -1 },
      new int[2]{ -1, -1 },
      new int[2]{ -1, -1 },
      new int[2]{ -1, -1 },
      new int[2]{ 6, 9 },
      new int[2]{ 6, 8 },
      new int[2]{ 5, 7 },
      new int[2]{ 5, 7 },
      new int[2]{ 4, 6 },
      new int[2]{ 4, 6 },
      new int[2]{ 4, 6 },
      new int[2]{ 4, 6 },
      new int[2]{ 4, 5 },
      new int[2]{ 4, 5 },
      new int[2]{ 4, 5 },
      new int[2]{ 4, 5 },
      new int[2]{ 3, 1 },
      new int[2]{ 3, 1 },
      new int[2]{ 3, 1 },
      new int[2]{ 3, 1 },
      new int[2]{ 3, 1 },
      new int[2]{ 3, 1 },
      new int[2]{ 3, 1 },
      new int[2]{ 3, 1 },
      new int[2]{ 3, 4 },
      new int[2]{ 3, 4 },
      new int[2]{ 3, 4 },
      new int[2]{ 3, 4 },
      new int[2]{ 3, 4 },
      new int[2]{ 3, 4 },
      new int[2]{ 3, 4 },
      new int[2]{ 3, 4 },
      new int[2]{ 2, 3 },
      new int[2]{ 2, 3 },
      new int[2]{ 2, 3 },
      new int[2]{ 2, 3 },
      new int[2]{ 2, 3 },
      new int[2]{ 2, 3 },
      new int[2]{ 2, 3 },
      new int[2]{ 2, 3 },
      new int[2]{ 2, 3 },
      new int[2]{ 2, 3 },
      new int[2]{ 2, 3 },
      new int[2]{ 2, 3 },
      new int[2]{ 2, 3 },
      new int[2]{ 2, 3 },
      new int[2]{ 2, 3 },
      new int[2]{ 2, 3 },
      new int[2]{ 2, 2 },
      new int[2]{ 2, 2 },
      new int[2]{ 2, 2 },
      new int[2]{ 2, 2 },
      new int[2]{ 2, 2 },
      new int[2]{ 2, 2 },
      new int[2]{ 2, 2 },
      new int[2]{ 2, 2 },
      new int[2]{ 2, 2 },
      new int[2]{ 2, 2 },
      new int[2]{ 2, 2 },
      new int[2]{ 2, 2 },
      new int[2]{ 2, 2 },
      new int[2]{ 2, 2 },
      new int[2]{ 2, 2 },
      new int[2]{ 2, 2 }
    };
    this.m_reader = reader;
  }

  internal long Get24Bits()
  {
    while (this.m_bufferLength < 24L)
    {
      this.m_buffer = this.m_bitOperation.Bit32Shift(this.m_buffer, 8, 0) | (long) ((int) this.m_reader.ReadByte() & (int) byte.MaxValue);
      this.m_bufferLength += 8L;
      ++this.m_noOfBytesRead;
    }
    return this.m_bitOperation.Bit32Shift(this.m_buffer, (int) (this.m_bufferLength - 24L), 1) & 16777215L /*0xFFFFFF*/;
  }

  internal int Get2DCode()
  {
    int[] numArray;
    if (this.m_bufferLength == 0L)
    {
      this.m_buffer = (long) ((int) this.m_reader.ReadByte() & (int) byte.MaxValue);
      int bytePointer = this.m_reader.bytePointer;
      this.m_bufferLength = 8L;
      ++this.m_noOfBytesRead;
      numArray = this.m_twoDimensionalTable1[(int) (this.m_bitOperation.Bit32Shift(this.m_buffer, 1, 1) & (long) sbyte.MaxValue)];
    }
    else if (this.m_bufferLength == 8L)
    {
      numArray = this.m_twoDimensionalTable1[(int) (this.m_bitOperation.Bit32Shift(this.m_buffer, 1, 1) & (long) sbyte.MaxValue)];
    }
    else
    {
      numArray = this.m_twoDimensionalTable1[(int) (this.m_bitOperation.Bit32Shift(this.m_buffer, (int) (7L - this.m_bufferLength), 0) & (long) sbyte.MaxValue)];
      if (numArray[0] < 0 || numArray[0] > (int) this.m_bufferLength)
      {
        int num = (int) this.m_reader.ReadByte() & (int) byte.MaxValue;
        this.m_buffer = this.m_bitOperation.Bit32Shift(this.m_buffer, 8, 0) | (long) num;
        this.m_bufferLength += 8L;
        ++this.m_noOfBytesRead;
        numArray = this.m_twoDimensionalTable1[(int) (this.m_bitOperation.Bit32Shift(this.m_buffer, (int) (this.m_bufferLength - 7L), 1) & (long) sbyte.MaxValue)];
      }
    }
    if (numArray[0] < 0)
      return 0;
    this.m_bufferLength -= (long) numArray[0];
    return numArray[1];
  }

  internal int GetblackCode()
  {
    if (this.m_bufferLength == 0L)
    {
      this.m_buffer = (long) ((int) this.m_reader.ReadByte() & (int) byte.MaxValue);
      this.m_bufferLength = 8L;
      ++this.m_noOfBytesRead;
    }
    int[] numArray;
    while (true)
    {
      if (this.m_bufferLength >= 6L && (this.m_bitOperation.Bit32Shift(this.m_buffer, (int) (this.m_bufferLength - 6L), 1) & 63L /*0x3F*/) == 0L)
        numArray = this.m_blackTable1[(int) ((this.m_bufferLength > 13L ? this.m_bitOperation.Bit32Shift(this.m_buffer, (int) (this.m_bufferLength - 13L), 1) : this.m_bitOperation.Bit32Shift(this.m_buffer, (int) (13L - this.m_bufferLength), 0)) & (long) sbyte.MaxValue)];
      else if (this.m_bufferLength >= 4L && ((int) this.m_buffer >> (int) (this.m_bufferLength - 4L) & 15) == 0)
      {
        int index = (int) (((this.m_bufferLength > 12L ? this.m_bitOperation.Bit32Shift(this.m_buffer, (int) (this.m_bufferLength - 12L), 1) : this.m_bitOperation.Bit32Shift(this.m_buffer, (int) (12L - this.m_bufferLength), 0)) & (long) byte.MaxValue) - 64L /*0x40*/);
        numArray = index < 0 ? this.m_blackTable1[this.m_blackTable1.Length + index] : this.m_blackTable2[index];
      }
      else
      {
        int index = (int) ((this.m_bufferLength > 6L ? this.m_bitOperation.Bit32Shift(this.m_buffer, (int) (this.m_bufferLength - 6L), 1) : this.m_bitOperation.Bit32Shift(this.m_buffer, (int) (6L - this.m_bufferLength), 0)) & 63L /*0x3F*/);
        numArray = index < 0 ? this.m_blackTable2[this.m_blackTable2.Length + index] : this.m_blackTable3[index];
      }
      if (numArray[0] <= 0 || numArray[0] > (int) this.m_bufferLength)
      {
        if (this.m_bufferLength < 13L)
        {
          this.m_buffer = this.m_bitOperation.Bit32Shift(this.m_buffer, 8, 0) | (long) ((int) this.m_reader.ReadByte() & (int) byte.MaxValue);
          this.m_bufferLength += 8L;
          ++this.m_noOfBytesRead;
        }
        else
          goto label_11;
      }
      else
        break;
    }
    this.m_bufferLength -= (long) numArray[0];
    return numArray[1];
label_11:
    --this.m_bufferLength;
    return 1;
  }

  internal int GetWhiteCode()
  {
    if (this.m_bufferLength == 0L)
    {
      this.m_buffer = (long) ((int) this.m_reader.ReadByte() & (int) byte.MaxValue);
      this.m_bufferLength = 8L;
      ++this.m_noOfBytesRead;
    }
    int[] numArray;
    while (true)
    {
      if (this.m_bufferLength >= 7L && (this.m_bitOperation.Bit32Shift(this.m_buffer, (int) (this.m_bufferLength - 7L), 1) & (long) sbyte.MaxValue) == 0L)
      {
        numArray = this.m_whiteTable1[(int) ((this.m_bufferLength > 12L ? this.m_bitOperation.Bit32Shift(this.m_buffer, (int) (this.m_bufferLength - 12L), 1) : this.m_bitOperation.Bit32Shift(this.m_buffer, (int) (12L - this.m_bufferLength), 0)) & 31L /*0x1F*/)];
      }
      else
      {
        int index = (int) ((this.m_bufferLength > 9L ? this.m_bitOperation.Bit32Shift(this.m_buffer, (int) (this.m_bufferLength - 9L), 1) : this.m_bitOperation.Bit32Shift(this.m_buffer, (int) (9L - this.m_bufferLength), 0)) & 511L /*0x01FF*/);
        numArray = index < 0 ? this.m_whiteTable2[this.m_whiteTable2.Length + index] : this.m_whiteTable2[index];
      }
      if (numArray[0] <= 0 || numArray[0] > (int) this.m_bufferLength)
      {
        if (this.m_bufferLength < 12L)
        {
          this.m_buffer = this.m_bitOperation.Bit32Shift(this.m_buffer, 8, 0) | (long) ((int) this.m_reader.ReadByte() & (int) byte.MaxValue);
          this.m_bufferLength += 8L;
          ++this.m_noOfBytesRead;
        }
        else
          goto label_9;
      }
      else
        break;
    }
    this.m_bufferLength -= (long) numArray[0];
    return numArray[1];
label_9:
    --this.m_bufferLength;
    return 1;
  }

  internal void Reset()
  {
    this.m_bufferLength = 0L;
    this.m_noOfBytesRead = 0L;
    this.m_buffer = 0L;
  }

  internal void SkipTo(int length)
  {
    for (; this.m_noOfBytesRead < (long) length; ++this.m_noOfBytesRead)
    {
      int num = (int) this.m_reader.ReadByte();
    }
  }
}
