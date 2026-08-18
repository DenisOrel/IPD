// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_7671
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_7671
{
  private static byte[] sspq = new byte[84]
  {
    (byte) 16 /*0x10*/,
    (byte) 0,
    (byte) 183,
    (byte) 153,
    (byte) 35,
    (byte) 45,
    (byte) 28,
    (byte) 186,
    (byte) 68,
    (byte) 201,
    (byte) 205,
    (byte) 113,
    (byte) 138,
    (byte) 58,
    (byte) 153,
    (byte) 20,
    (byte) 51,
    (byte) 37,
    (byte) 205,
    (byte) 210,
    (byte) 11,
    (byte) 97,
    (byte) 72,
    (byte) 213,
    (byte) 115,
    (byte) 229,
    (byte) 205,
    (byte) 68,
    (byte) 56,
    (byte) 9,
    (byte) 214,
    (byte) 174,
    (byte) 28,
    (byte) 25,
    (byte) 41,
    (byte) 49,
    (byte) 206,
    (byte) 26,
    (byte) 207,
    (byte) 238,
    (byte) 70,
    (byte) 161,
    (byte) 51,
    (byte) 249,
    (byte) 130,
    (byte) 217,
    (byte) 127 /*0x7F*/,
    (byte) 250,
    (byte) 212,
    (byte) 176 /*0xB0*/,
    (byte) 156,
    (byte) 113,
    (byte) 104,
    (byte) 185,
    (byte) 2,
    (byte) 6,
    (byte) 38,
    (byte) 41,
    (byte) 119,
    (byte) 83,
    (byte) 53,
    (byte) 95,
    (byte) 82,
    (byte) 125,
    (byte) 16 /*0x10*/,
    (byte) 95,
    (byte) 86,
    (byte) 111,
    (byte) 106,
    (byte) 164,
    (byte) 75,
    (byte) 29,
    (byte) 158,
    (byte) 117,
    (byte) 129,
    (byte) 162,
    (byte) 99,
    (byte) 219,
    (byte) 230,
    (byte) 68,
    (byte) 191,
    (byte) 207,
    (byte) 218,
    (byte) 156
  };
  private static byte[] sspr = new byte[84]
  {
    (byte) 61,
    (byte) 51,
    (byte) 13,
    (byte) 199,
    (byte) 30,
    (byte) 130,
    (byte) 105,
    (byte) 49,
    (byte) 174,
    (byte) 242,
    (byte) 243,
    (byte) 12,
    (byte) 145,
    (byte) 176 /*0xB0*/,
    (byte) 160 /*0xA0*/,
    (byte) 35,
    (byte) 233,
    (byte) 64 /*0x40*/,
    (byte) 202,
    (byte) 183,
    (byte) 236,
    (byte) 67,
    (byte) 187,
    (byte) 9,
    (byte) 175,
    byte.MaxValue,
    (byte) 8,
    (byte) 93,
    (byte) 20,
    (byte) 66,
    (byte) 204,
    (byte) 102,
    (byte) 208 /*0xD0*/,
    (byte) 206,
    (byte) 155,
    (byte) 142,
    (byte) 249,
    (byte) 50,
    (byte) 19,
    (byte) 252,
    (byte) 88,
    (byte) 181,
    (byte) 95,
    (byte) 178,
    (byte) 31 /*0x1F*/,
    (byte) 200,
    (byte) 68,
    (byte) 102,
    (byte) 26,
    (byte) 31 /*0x1F*/,
    (byte) 197,
    (byte) 68,
    (byte) 73,
    (byte) 44,
    (byte) 32 /*0x20*/,
    (byte) 138,
    (byte) 56,
    (byte) 206,
    (byte) 185,
    (byte) 252,
    (byte) 173,
    (byte) 195,
    (byte) 61,
    (byte) 250,
    (byte) 47,
    (byte) 229,
    (byte) 73,
    byte.MaxValue,
    (byte) 153,
    (byte) 223,
    (byte) 7,
    (byte) 218,
    (byte) 93,
    (byte) 30,
    (byte) 128 /*0x80*/,
    (byte) 0,
    (byte) 127 /*0x7F*/,
    (byte) 196,
    (byte) 62,
    (byte) 238,
    (byte) 7,
    (byte) 149,
    (byte) 119,
    byte.MaxValue
  };

  internal static int ssp_imbase_7672(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 235,
      (byte) 14,
      (byte) 175,
      (byte) 223,
      (byte) 59,
      (byte) 207,
      (byte) 62,
      (byte) 118,
      (byte) 92,
      (byte) 3,
      (byte) 14,
      (byte) 58,
      (byte) 65,
      (byte) 242,
      (byte) 195,
      (byte) 170,
      (byte) 222,
      (byte) 242,
      (byte) 234,
      (byte) 245,
      (byte) 155,
      (byte) 155,
      (byte) 166,
      (byte) 95,
      (byte) 6,
      (byte) 163,
      (byte) 179,
      (byte) 131,
      (byte) 0,
      (byte) 150,
      (byte) 9,
      (byte) 137,
      (byte) 215,
      (byte) 16 /*0x10*/,
      (byte) 150,
      (byte) 38,
      (byte) 76,
      (byte) 31 /*0x1F*/,
      (byte) 92,
      (byte) 117,
      (byte) 33,
      (byte) 34,
      (byte) 107,
      (byte) 254,
      (byte) 232,
      (byte) 236,
      (byte) 80 /*0x50*/,
      (byte) 1
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 123,
      (byte) 155,
      (byte) 230,
      (byte) 17,
      (byte) 199,
      (byte) 90,
      (byte) 173,
      (byte) 87,
      (byte) 36,
      (byte) 205,
      (byte) 0,
      (byte) 204,
      (byte) 163,
      (byte) 248,
      (byte) 20,
      (byte) 82,
      (byte) 214,
      (byte) 228,
      (byte) 5,
      (byte) 205,
      (byte) 9,
      (byte) 15,
      (byte) 186,
      (byte) 251,
      (byte) 102,
      (byte) 17,
      (byte) 95,
      (byte) 130,
      (byte) 252,
      (byte) 130,
      (byte) 159,
      (byte) 148,
      (byte) 205,
      (byte) 208 /*0xD0*/,
      (byte) 147,
      (byte) 205,
      (byte) 129,
      (byte) 134,
      (byte) 89,
      (byte) 95,
      (byte) 127 /*0x7F*/,
      (byte) 27,
      (byte) 167,
      (byte) 51,
      (byte) 222,
      (byte) 78,
      (byte) 175,
      (byte) 34
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 343, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_imbase_7673()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[29];
      byte[] numArray2 = new byte[29]
      {
        (byte) 134,
        (byte) 21,
        (byte) 249,
        (byte) 79,
        (byte) 103,
        (byte) 79,
        (byte) 217,
        (byte) 152,
        (byte) 28,
        (byte) 186,
        (byte) 117,
        (byte) 198,
        (byte) 250,
        (byte) 133,
        (byte) 98,
        (byte) 48 /*0x30*/,
        (byte) 92,
        (byte) 157,
        (byte) 94,
        (byte) 191,
        (byte) 150,
        (byte) 116,
        (byte) 211,
        (byte) 121,
        (byte) 47,
        (byte) 168,
        (byte) 119,
        (byte) 26,
        (byte) 82
      };
      byte[] numArray3 = new byte[29];
      numArray3[23] = (byte) 242;
      numArray3[21] = (byte) 222;
      numArray3[2] = (byte) 193;
      numArray3[3] = (byte) 246;
      numArray3[4] = (byte) 82;
      numArray3[5] = (byte) 49;
      numArray3[28] = (byte) 37;
      numArray3[7] = (byte) 73;
      numArray3[26] = (byte) 178;
      numArray3[6] = (byte) 20;
      numArray3[10] = (byte) 115;
      numArray3[11] = (byte) 219;
      numArray3[12] = (byte) 205;
      numArray3[13] = (byte) 32 /*0x20*/;
      numArray3[16 /*0x10*/] = (byte) 150;
      numArray3[27] = (byte) 209;
      numArray3[1] = (byte) 228;
      numArray3[25] = (byte) 189;
      numArray3[18] = (byte) 34;
      numArray3[19] = (byte) 217;
      numArray3[20] = (byte) 92;
      numArray3[17] = (byte) 214;
      numArray3[22] = (byte) 54;
      numArray3[8] = (byte) 74;
      numArray3[15] = (byte) 65;
      numArray3[9] = (byte) 13;
      numArray3[0] = (byte) 220;
      numArray3[14] = (byte) 6;
      numArray3[24] = (byte) 124;
      key.Query(true, 343, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 29);
      for (int index = 0; index < 29; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[29];
    byte[] numArray5 = new byte[29]
    {
      (byte) 191,
      (byte) 69,
      (byte) 106,
      (byte) 78,
      (byte) 126,
      (byte) 44,
      (byte) 207,
      (byte) 130,
      (byte) 160 /*0xA0*/,
      (byte) 25,
      (byte) 22,
      (byte) 222,
      (byte) 97,
      (byte) 121,
      (byte) 209,
      (byte) 28,
      (byte) 108,
      (byte) 204,
      (byte) 138,
      (byte) 164,
      (byte) 20,
      (byte) 144 /*0x90*/,
      (byte) 51,
      (byte) 12,
      (byte) 240 /*0xF0*/,
      (byte) 74,
      (byte) 93,
      (byte) 225,
      (byte) 241
    };
    byte[] numArray6 = new byte[29];
    numArray6[7] = (byte) 220;
    numArray6[1] = (byte) 117;
    numArray6[27] = (byte) 191;
    numArray6[3] = (byte) 186;
    numArray6[28] = (byte) 120;
    numArray6[5] = (byte) 104;
    numArray6[6] = (byte) 72;
    numArray6[2] = (byte) 214;
    numArray6[8] = (byte) 151;
    numArray6[23] = (byte) 197;
    numArray6[9] = (byte) 228;
    numArray6[25] = (byte) 213;
    numArray6[11] = (byte) 105;
    numArray6[21] = (byte) 49;
    numArray6[14] = (byte) 74;
    numArray6[15] = (byte) 28;
    numArray6[16 /*0x10*/] = (byte) 99;
    numArray6[17] = (byte) 183;
    numArray6[18] = (byte) 4;
    numArray6[12] = (byte) 35;
    numArray6[20] = (byte) 62;
    numArray6[0] = (byte) 88;
    numArray6[19] = (byte) 153;
    numArray6[26] = (byte) 239;
    numArray6[4] = (byte) 108;
    numArray6[13] = (byte) 79;
    numArray6[24] = (byte) 180;
    numArray6[10] = (byte) 83;
    numArray6[22] = (byte) 244;
    key.Query(true, 343, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 29);
    for (int index = 0; index < 29; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static int ssp_imbase_7674(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[37] = (byte) 190;
    sourceArray1[12] = (byte) 52;
    sourceArray1[2] = (byte) 240 /*0xF0*/;
    sourceArray1[15] = (byte) 125;
    sourceArray1[13] = (byte) 166;
    sourceArray1[3] = (byte) 222;
    sourceArray1[20] = (byte) 251;
    sourceArray1[7] = (byte) 46;
    sourceArray1[8] = (byte) 84;
    sourceArray1[23] = (byte) 144 /*0x90*/;
    sourceArray1[31 /*0x1F*/] = (byte) 170;
    sourceArray1[0] = (byte) 32 /*0x20*/;
    sourceArray1[32 /*0x20*/] = (byte) 69;
    sourceArray1[34] = (byte) 201;
    sourceArray1[14] = (byte) 45;
    sourceArray1[27] = (byte) 109;
    sourceArray1[16 /*0x10*/] = (byte) 83;
    sourceArray1[26] = (byte) 88;
    sourceArray1[18] = (byte) 153;
    sourceArray1[19] = (byte) 106;
    sourceArray1[25] = (byte) 95;
    sourceArray1[21] = (byte) 171;
    sourceArray1[22] = (byte) 227;
    sourceArray1[10] = (byte) 17;
    sourceArray1[24] = (byte) 228;
    sourceArray1[41] = (byte) 199;
    sourceArray1[5] = (byte) 242;
    sourceArray1[4] = (byte) 61;
    sourceArray1[9] = (byte) 158;
    sourceArray1[29] = (byte) 186;
    sourceArray1[30] = (byte) 249;
    sourceArray1[42] = (byte) 128 /*0x80*/;
    sourceArray1[45] = (byte) 170;
    sourceArray1[6] = (byte) 82;
    sourceArray1[33] = (byte) 78;
    sourceArray1[35] = (byte) 130;
    sourceArray1[36] = (byte) 158;
    sourceArray1[43] = (byte) 20;
    sourceArray1[38] = (byte) 12;
    sourceArray1[39] = (byte) 209;
    sourceArray1[40] = (byte) 36;
    sourceArray1[28] = (byte) 142;
    sourceArray1[17] = (byte) 0;
    sourceArray1[47] = (byte) 136;
    sourceArray1[44] = (byte) 132;
    sourceArray1[11] = (byte) 112 /*0x70*/;
    sourceArray1[46] = (byte) 144 /*0x90*/;
    sourceArray1[1] = (byte) 130;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 211,
      (byte) 115,
      (byte) 7,
      (byte) 150,
      (byte) 230,
      (byte) 251,
      (byte) 108,
      (byte) 238,
      (byte) 184,
      (byte) 117,
      (byte) 110,
      (byte) 231,
      (byte) 29,
      (byte) 82,
      (byte) 70,
      (byte) 150,
      (byte) 240 /*0xF0*/,
      (byte) 66,
      (byte) 164,
      (byte) 190,
      (byte) 136,
      (byte) 224 /*0xE0*/,
      (byte) 30,
      (byte) 6,
      (byte) 158,
      (byte) 175,
      (byte) 247,
      (byte) 232,
      (byte) 82,
      (byte) 198,
      (byte) 194,
      (byte) 7,
      (byte) 190,
      (byte) 113,
      (byte) 128 /*0x80*/,
      (byte) 171,
      (byte) 92,
      (byte) 28,
      (byte) 46,
      (byte) 55,
      (byte) 43,
      (byte) 68,
      (byte) 149,
      (byte) 226,
      (byte) 223,
      (byte) 239,
      (byte) 7,
      (byte) 167
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 343, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_imbase_7675()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[18];
      byte[] numArray2 = new byte[18]
      {
        (byte) 127 /*0x7F*/,
        (byte) 170,
        (byte) 170,
        (byte) 58,
        (byte) 3,
        (byte) 116,
        (byte) 64 /*0x40*/,
        (byte) 109,
        (byte) 60,
        (byte) 97,
        (byte) 73,
        (byte) 208 /*0xD0*/,
        (byte) 82,
        (byte) 190,
        (byte) 15,
        (byte) 202,
        (byte) 92,
        (byte) 242
      };
      byte[] numArray3 = new byte[18];
      numArray3[12] = (byte) 133;
      numArray3[16 /*0x10*/] = (byte) 210;
      numArray3[2] = (byte) 200;
      numArray3[3] = (byte) 75;
      numArray3[0] = (byte) 199;
      numArray3[15] = (byte) 249;
      numArray3[14] = (byte) 252;
      numArray3[6] = (byte) 112 /*0x70*/;
      numArray3[8] = (byte) 63 /*0x3F*/;
      numArray3[9] = (byte) 55;
      numArray3[10] = (byte) 200;
      numArray3[1] = (byte) 1;
      numArray3[11] = (byte) 51;
      numArray3[13] = (byte) 9;
      numArray3[5] = (byte) 33;
      numArray3[4] = (byte) 209;
      numArray3[7] = (byte) 253;
      numArray3[17] = (byte) 223;
      key.Query(true, 343, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 18);
      for (int index = 0; index < 18; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[18];
    byte[] numArray5 = new byte[18];
    numArray5[15] = (byte) 42;
    numArray5[1] = (byte) 133;
    numArray5[14] = (byte) 143;
    numArray5[3] = (byte) 134;
    numArray5[16 /*0x10*/] = (byte) 22;
    numArray5[4] = (byte) 87;
    numArray5[6] = (byte) 19;
    numArray5[13] = (byte) 251;
    numArray5[8] = (byte) 6;
    numArray5[9] = (byte) 30;
    numArray5[12] = (byte) 52;
    numArray5[7] = (byte) 237;
    numArray5[0] = (byte) 59;
    numArray5[10] = (byte) 109;
    numArray5[2] = (byte) 6;
    numArray5[11] = (byte) 225;
    numArray5[5] = (byte) 10;
    numArray5[17] = (byte) 125;
    byte[] numArray6 = new byte[18]
    {
      (byte) 53,
      (byte) 173,
      (byte) 102,
      (byte) 4,
      (byte) 92,
      (byte) 199,
      (byte) 35,
      (byte) 208 /*0xD0*/,
      (byte) 252,
      (byte) 231,
      (byte) 151,
      (byte) 170,
      (byte) 114,
      (byte) 20,
      (byte) 199,
      (byte) 241,
      (byte) 137,
      (byte) 172
    };
    key.Query(true, 343, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 18);
    for (int index = 0; index < 18; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_imbase_7676()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[36];
      byte[] numArray2 = new byte[36];
      numArray2[23] = (byte) 3;
      numArray2[1] = (byte) 176 /*0xB0*/;
      numArray2[2] = (byte) 202;
      numArray2[28] = (byte) 77;
      numArray2[6] = (byte) 216;
      numArray2[26] = (byte) 211;
      numArray2[29] = (byte) 52;
      numArray2[9] = (byte) 197;
      numArray2[8] = (byte) 208 /*0xD0*/;
      numArray2[31 /*0x1F*/] = (byte) 52;
      numArray2[4] = (byte) 35;
      numArray2[11] = (byte) 167;
      numArray2[20] = (byte) 5;
      numArray2[13] = (byte) 218;
      numArray2[14] = (byte) 67;
      numArray2[0] = (byte) 179;
      numArray2[16 /*0x10*/] = (byte) 11;
      numArray2[17] = (byte) 41;
      numArray2[7] = (byte) 72;
      numArray2[19] = (byte) 3;
      numArray2[5] = (byte) 38;
      numArray2[21] = (byte) 163;
      numArray2[22] = (byte) 138;
      numArray2[32 /*0x20*/] = (byte) 167;
      numArray2[24] = (byte) 52;
      numArray2[25] = (byte) 200;
      numArray2[15] = (byte) 151;
      numArray2[18] = (byte) 82;
      numArray2[3] = (byte) 167;
      numArray2[12] = (byte) 4;
      numArray2[30] = (byte) 62;
      numArray2[27] = (byte) 234;
      numArray2[34] = (byte) 192 /*0xC0*/;
      numArray2[10] = (byte) 164;
      numArray2[33] = (byte) 162;
      numArray2[35] = (byte) 180;
      byte[] numArray3 = new byte[36];
      numArray3[4] = (byte) 96 /*0x60*/;
      numArray3[1] = (byte) 202;
      numArray3[0] = (byte) 0;
      numArray3[3] = (byte) 126;
      numArray3[6] = (byte) 116;
      numArray3[23] = (byte) 205;
      numArray3[33] = (byte) 103;
      numArray3[22] = (byte) 209;
      numArray3[16 /*0x10*/] = (byte) 31 /*0x1F*/;
      numArray3[31 /*0x1F*/] = (byte) 32 /*0x20*/;
      numArray3[10] = (byte) 75;
      numArray3[18] = (byte) 130;
      numArray3[24] = (byte) 160 /*0xA0*/;
      numArray3[13] = (byte) 65;
      numArray3[14] = (byte) 18;
      numArray3[15] = (byte) 210;
      numArray3[29] = (byte) 10;
      numArray3[12] = (byte) 130;
      numArray3[30] = (byte) 170;
      numArray3[19] = (byte) 223;
      numArray3[20] = (byte) 27;
      numArray3[21] = (byte) 6;
      numArray3[11] = (byte) 16 /*0x10*/;
      numArray3[34] = (byte) 23;
      numArray3[25] = (byte) 22;
      numArray3[8] = (byte) 51;
      numArray3[26] = (byte) 196;
      numArray3[27] = (byte) 215;
      numArray3[9] = (byte) 125;
      numArray3[17] = (byte) 90;
      numArray3[32 /*0x20*/] = (byte) 99;
      numArray3[5] = (byte) 72;
      numArray3[28] = (byte) 104;
      numArray3[2] = (byte) 179;
      numArray3[7] = (byte) 186;
      numArray3[35] = (byte) 134;
      key.Query(true, 343, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 36);
      for (int index = 0; index < 36; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[36];
    byte[] numArray5 = new byte[36];
    numArray5[17] = (byte) 137;
    numArray5[22] = (byte) 183;
    numArray5[32 /*0x20*/] = (byte) 250;
    numArray5[3] = (byte) 49;
    numArray5[25] = (byte) 134;
    numArray5[30] = (byte) 89;
    numArray5[7] = (byte) 182;
    numArray5[6] = (byte) 197;
    numArray5[2] = (byte) 129;
    numArray5[8] = (byte) 10;
    numArray5[4] = (byte) 178;
    numArray5[11] = (byte) 57;
    numArray5[5] = (byte) 8;
    numArray5[13] = (byte) 0;
    numArray5[14] = (byte) 173;
    numArray5[27] = (byte) 221;
    numArray5[16 /*0x10*/] = (byte) 18;
    numArray5[9] = (byte) 203;
    numArray5[18] = (byte) 250;
    numArray5[12] = (byte) 106;
    numArray5[10] = (byte) 42;
    numArray5[19] = (byte) 137;
    numArray5[33] = (byte) 163;
    numArray5[23] = (byte) 174;
    numArray5[24] = (byte) 236;
    numArray5[1] = (byte) 34;
    numArray5[21] = (byte) 214;
    numArray5[20] = (byte) 0;
    numArray5[26] = (byte) 109;
    numArray5[29] = (byte) 43;
    numArray5[0] = (byte) 57;
    numArray5[31 /*0x1F*/] = (byte) 139;
    numArray5[15] = (byte) 216;
    numArray5[28] = (byte) 207;
    numArray5[34] = (byte) 85;
    numArray5[35] = (byte) 238;
    byte[] numArray6 = new byte[36]
    {
      (byte) 158,
      (byte) 215,
      (byte) 128 /*0x80*/,
      (byte) 43,
      (byte) 24,
      (byte) 38,
      (byte) 164,
      (byte) 42,
      (byte) 86,
      (byte) 65,
      (byte) 78,
      (byte) 111,
      (byte) 131,
      (byte) 73,
      (byte) 182,
      (byte) 109,
      (byte) 251,
      (byte) 227,
      (byte) 60,
      (byte) 203,
      (byte) 218,
      (byte) 66,
      (byte) 137,
      (byte) 99,
      (byte) 251,
      (byte) 198,
      (byte) 51,
      (byte) 248,
      (byte) 42,
      (byte) 219,
      (byte) 216,
      (byte) 73,
      (byte) 133,
      (byte) 214,
      (byte) 54,
      (byte) 169
    };
    key.Query(true, 343, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 36);
    for (int index = 0; index < 36; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_imbase_7677()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[42];
      byte[] numArray2 = new byte[42]
      {
        (byte) 202,
        (byte) 148,
        (byte) 150,
        (byte) 233,
        (byte) 214,
        (byte) 246,
        (byte) 50,
        (byte) 146,
        (byte) 236,
        (byte) 152,
        (byte) 59,
        (byte) 85,
        (byte) 152,
        (byte) 243,
        (byte) 130,
        (byte) 12,
        (byte) 231,
        (byte) 219,
        (byte) 233,
        (byte) 17,
        (byte) 101,
        (byte) 45,
        (byte) 176 /*0xB0*/,
        (byte) 121,
        (byte) 195,
        (byte) 233,
        (byte) 204,
        (byte) 236,
        (byte) 67,
        (byte) 88,
        (byte) 63 /*0x3F*/,
        (byte) 218,
        (byte) 21,
        (byte) 86,
        (byte) 152,
        (byte) 215,
        (byte) 60,
        (byte) 91,
        (byte) 85,
        (byte) 164,
        (byte) 182,
        (byte) 84
      };
      byte[] numArray3 = new byte[42]
      {
        (byte) 36,
        byte.MaxValue,
        (byte) 69,
        (byte) 96 /*0x60*/,
        (byte) 150,
        (byte) 52,
        (byte) 101,
        (byte) 45,
        (byte) 214,
        (byte) 96 /*0x60*/,
        (byte) 205,
        (byte) 16 /*0x10*/,
        (byte) 167,
        (byte) 195,
        (byte) 139,
        (byte) 216,
        (byte) 163,
        (byte) 245,
        (byte) 12,
        (byte) 123,
        (byte) 133,
        (byte) 65,
        (byte) 35,
        (byte) 219,
        (byte) 199,
        (byte) 235,
        (byte) 10,
        (byte) 83,
        (byte) 171,
        (byte) 166,
        (byte) 43,
        byte.MaxValue,
        (byte) 54,
        (byte) 57,
        (byte) 161,
        (byte) 219,
        (byte) 195,
        (byte) 148,
        (byte) 25,
        (byte) 54,
        (byte) 117,
        (byte) 36
      };
      key.Query(true, 343, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 42);
      for (int index = 0; index < 42; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[42];
    byte[] numArray5 = new byte[42]
    {
      (byte) 234,
      (byte) 45,
      (byte) 183,
      (byte) 60,
      (byte) 237,
      (byte) 254,
      (byte) 167,
      (byte) 139,
      (byte) 126,
      (byte) 78,
      (byte) 156,
      (byte) 227,
      (byte) 28,
      (byte) 37,
      (byte) 237,
      (byte) 41,
      (byte) 70,
      (byte) 79,
      (byte) 147,
      (byte) 89,
      (byte) 97,
      (byte) 63 /*0x3F*/,
      (byte) 65,
      (byte) 90,
      (byte) 171,
      (byte) 207,
      (byte) 140,
      (byte) 133,
      (byte) 68,
      (byte) 90,
      (byte) 17,
      (byte) 58,
      (byte) 215,
      (byte) 21,
      (byte) 217,
      (byte) 187,
      (byte) 21,
      (byte) 175,
      (byte) 85,
      (byte) 90,
      (byte) 151,
      (byte) 78
    };
    byte[] numArray6 = new byte[42]
    {
      (byte) 203,
      (byte) 188,
      (byte) 207,
      (byte) 13,
      (byte) 45,
      (byte) 93,
      (byte) 190,
      (byte) 44,
      (byte) 130,
      (byte) 133,
      (byte) 9,
      (byte) 0,
      (byte) 150,
      (byte) 10,
      (byte) 159,
      (byte) 84,
      (byte) 250,
      (byte) 40,
      (byte) 182,
      (byte) 151,
      (byte) 139,
      (byte) 44,
      (byte) 2,
      (byte) 170,
      (byte) 235,
      (byte) 184,
      (byte) 113,
      (byte) 33,
      (byte) 209,
      (byte) 15,
      (byte) 115,
      (byte) 80 /*0x50*/,
      (byte) 140,
      (byte) 111,
      (byte) 104,
      (byte) 182,
      (byte) 74,
      (byte) 240 /*0xF0*/,
      (byte) 26,
      (byte) 254,
      (byte) 226,
      (byte) 140
    };
    key.Query(true, 343, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 42);
    for (int index = 0; index < 42; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_imbase_7678()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[43];
      byte[] numArray2 = new byte[43]
      {
        (byte) 31 /*0x1F*/,
        (byte) 53,
        (byte) 103,
        (byte) 123,
        (byte) 112 /*0x70*/,
        (byte) 5,
        (byte) 94,
        (byte) 127 /*0x7F*/,
        (byte) 2,
        (byte) 170,
        (byte) 235,
        (byte) 92,
        (byte) 145,
        (byte) 29,
        (byte) 113,
        (byte) 33,
        (byte) 70,
        (byte) 35,
        (byte) 61,
        (byte) 97,
        (byte) 12,
        (byte) 6,
        (byte) 75,
        (byte) 50,
        (byte) 20,
        (byte) 150,
        (byte) 249,
        (byte) 57,
        (byte) 193,
        (byte) 18,
        (byte) 168,
        (byte) 57,
        (byte) 94,
        (byte) 183,
        (byte) 176 /*0xB0*/,
        (byte) 86,
        (byte) 29,
        (byte) 201,
        (byte) 234,
        (byte) 11,
        (byte) 220,
        (byte) 87,
        (byte) 19
      };
      byte[] numArray3 = new byte[43]
      {
        (byte) 81,
        (byte) 30,
        (byte) 71,
        (byte) 200,
        (byte) 84,
        (byte) 22,
        (byte) 249,
        (byte) 48 /*0x30*/,
        (byte) 244,
        (byte) 145,
        (byte) 170,
        (byte) 239,
        (byte) 234,
        (byte) 167,
        (byte) 87,
        (byte) 147,
        (byte) 112 /*0x70*/,
        (byte) 92,
        (byte) 32 /*0x20*/,
        (byte) 232,
        (byte) 146,
        (byte) 200,
        (byte) 230,
        (byte) 118,
        (byte) 2,
        (byte) 62,
        (byte) 47,
        (byte) 185,
        (byte) 19,
        (byte) 194,
        (byte) 128 /*0x80*/,
        (byte) 100,
        (byte) 137,
        (byte) 160 /*0xA0*/,
        (byte) 132,
        (byte) 126,
        (byte) 49,
        (byte) 154,
        (byte) 205,
        (byte) 248,
        (byte) 242,
        (byte) 187,
        (byte) 195
      };
      key.Query(true, 343, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 43);
      for (int index = 0; index < 43; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[43];
    byte[] numArray5 = new byte[43]
    {
      (byte) 30,
      (byte) 55,
      (byte) 233,
      (byte) 118,
      (byte) 7,
      (byte) 115,
      (byte) 203,
      (byte) 153,
      (byte) 231,
      (byte) 25,
      (byte) 234,
      (byte) 53,
      (byte) 102,
      (byte) 218,
      (byte) 11,
      (byte) 37,
      (byte) 190,
      (byte) 239,
      (byte) 2,
      (byte) 178,
      (byte) 253,
      (byte) 217,
      (byte) 109,
      (byte) 27,
      (byte) 4,
      (byte) 124,
      (byte) 57,
      (byte) 49,
      (byte) 133,
      (byte) 180,
      (byte) 243,
      (byte) 28,
      (byte) 98,
      (byte) 91,
      (byte) 120,
      (byte) 194,
      (byte) 80 /*0x50*/,
      (byte) 75,
      (byte) 10,
      (byte) 52,
      (byte) 110,
      (byte) 197,
      (byte) 208 /*0xD0*/
    };
    byte[] numArray6 = new byte[43]
    {
      (byte) 191,
      (byte) 185,
      (byte) 53,
      (byte) 125,
      (byte) 69,
      (byte) 66,
      (byte) 217,
      (byte) 92,
      (byte) 42,
      (byte) 25,
      (byte) 161,
      (byte) 91,
      (byte) 178,
      (byte) 62,
      (byte) 118,
      (byte) 58,
      (byte) 134,
      (byte) 156,
      (byte) 70,
      (byte) 72,
      (byte) 93,
      (byte) 81,
      (byte) 158,
      (byte) 95,
      (byte) 94,
      (byte) 210,
      (byte) 198,
      (byte) 252,
      (byte) 55,
      (byte) 214,
      (byte) 124,
      (byte) 164,
      (byte) 0,
      (byte) 133,
      (byte) 92,
      (byte) 196,
      (byte) 224 /*0xE0*/,
      (byte) 71,
      (byte) 69,
      (byte) 172,
      (byte) 16 /*0x10*/,
      (byte) 61,
      (byte) 193
    };
    key.Query(true, 343, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 43);
    for (int index = 0; index < 43; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[52];
    byte[] response = new byte[52];
    Array.Copy((Array) sc_7671.sspq, 0, (Array) numArray7, 0, 52);
    key.Query(true, 343, numArray7, response);
    Array.Copy((Array) sc_7671.sspr, 0, (Array) numArray7, 0, 52);
    for (int index = 0; index < numArray7.Length; ++index)
    {
      if ((int) numArray7[index] != (int) response[index])
      {
        key.TagValue = (int) response[index];
        break;
      }
    }
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_imbase_7679()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[29];
      byte[] numArray2 = new byte[29]
      {
        (byte) 21,
        (byte) 87,
        (byte) 174,
        (byte) 183,
        (byte) 225,
        (byte) 38,
        (byte) 62,
        (byte) 95,
        (byte) 216,
        (byte) 129,
        (byte) 59,
        (byte) 243,
        (byte) 31 /*0x1F*/,
        (byte) 85,
        (byte) 112 /*0x70*/,
        (byte) 17,
        (byte) 104,
        (byte) 70,
        (byte) 24,
        (byte) 86,
        (byte) 222,
        (byte) 140,
        (byte) 105,
        (byte) 45,
        (byte) 79,
        (byte) 83,
        (byte) 154,
        (byte) 101,
        (byte) 211
      };
      byte[] numArray3 = new byte[29]
      {
        (byte) 118,
        (byte) 17,
        (byte) 104,
        (byte) 89,
        (byte) 61,
        (byte) 44,
        (byte) 131,
        (byte) 207,
        (byte) 249,
        (byte) 87,
        (byte) 59,
        (byte) 171,
        (byte) 114,
        (byte) 105,
        (byte) 170,
        (byte) 98,
        (byte) 29,
        (byte) 135,
        (byte) 45,
        (byte) 246,
        (byte) 254,
        (byte) 247,
        (byte) 95,
        (byte) 13,
        (byte) 113,
        (byte) 207,
        (byte) 117,
        (byte) 71,
        (byte) 72
      };
      key.Query(true, 343, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 29);
      for (int index = 0; index < 29; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[29];
    byte[] numArray5 = new byte[29]
    {
      (byte) 100,
      (byte) 16 /*0x10*/,
      (byte) 7,
      (byte) 194,
      (byte) 122,
      (byte) 122,
      (byte) 236,
      (byte) 244,
      (byte) 145,
      (byte) 143,
      (byte) 110,
      (byte) 31 /*0x1F*/,
      (byte) 248,
      (byte) 5,
      (byte) 98,
      (byte) 131,
      (byte) 14,
      (byte) 232,
      (byte) 107,
      (byte) 106,
      (byte) 132,
      (byte) 27,
      (byte) 191,
      (byte) 136,
      (byte) 238,
      (byte) 128 /*0x80*/,
      (byte) 103,
      (byte) 142,
      (byte) 140
    };
    byte[] numArray6 = new byte[29];
    numArray6[2] = (byte) 128 /*0x80*/;
    numArray6[14] = (byte) 167;
    numArray6[22] = (byte) 106;
    numArray6[3] = (byte) 99;
    numArray6[4] = (byte) 135;
    numArray6[26] = (byte) 170;
    numArray6[8] = (byte) 212;
    numArray6[18] = (byte) 42;
    numArray6[10] = (byte) 27;
    numArray6[9] = (byte) 99;
    numArray6[12] = (byte) 117;
    numArray6[24] = (byte) 236;
    numArray6[11] = (byte) 115;
    numArray6[5] = (byte) 87;
    numArray6[7] = (byte) 220;
    numArray6[1] = (byte) 0;
    numArray6[16 /*0x10*/] = (byte) 241;
    numArray6[17] = (byte) 83;
    numArray6[0] = (byte) 227;
    numArray6[19] = (byte) 163;
    numArray6[20] = (byte) 119;
    numArray6[21] = (byte) 205;
    numArray6[6] = (byte) 196;
    numArray6[23] = (byte) 156;
    numArray6[15] = (byte) 84;
    numArray6[25] = (byte) 25;
    numArray6[13] = (byte) 112 /*0x70*/;
    numArray6[27] = (byte) 52;
    numArray6[28] = (byte) 29;
    key.Query(true, 343, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 29);
    for (int index = 0; index < 29; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[32 /*0x20*/];
    byte[] response = new byte[32 /*0x20*/];
    Array.Copy((Array) sc_7671.sspq, 52, (Array) numArray7, 0, 32 /*0x20*/);
    key.Query(true, 343, numArray7, response);
    Array.Copy((Array) sc_7671.sspr, 52, (Array) numArray7, 0, 32 /*0x20*/);
    for (int index = 0; index < numArray7.Length; ++index)
    {
      if ((int) numArray7[index] != (int) response[index])
      {
        key.TagValue = (int) response[index];
        break;
      }
    }
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_imbase_7680()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[15];
      byte[] numArray2 = new byte[15];
      numArray2[10] = (byte) 247;
      numArray2[1] = (byte) 188;
      numArray2[2] = (byte) 11;
      numArray2[0] = (byte) 252;
      numArray2[5] = (byte) 35;
      numArray2[3] = (byte) 77;
      numArray2[6] = (byte) 11;
      numArray2[7] = (byte) 210;
      numArray2[8] = (byte) 156;
      numArray2[9] = (byte) 225;
      numArray2[12] = (byte) 135;
      numArray2[11] = (byte) 151;
      numArray2[13] = (byte) 13;
      numArray2[4] = (byte) 23;
      numArray2[14] = (byte) 136;
      byte[] numArray3 = new byte[15];
      numArray3[1] = (byte) 31 /*0x1F*/;
      numArray3[10] = (byte) 60;
      numArray3[0] = (byte) 0;
      numArray3[3] = (byte) 149;
      numArray3[6] = (byte) 20;
      numArray3[7] = (byte) 118;
      numArray3[2] = (byte) 237;
      numArray3[12] = (byte) 32 /*0x20*/;
      numArray3[8] = (byte) 59;
      numArray3[9] = (byte) 187;
      numArray3[4] = (byte) 60;
      numArray3[11] = (byte) 32 /*0x20*/;
      numArray3[5] = (byte) 22;
      numArray3[13] = (byte) 154;
      numArray3[14] = (byte) 248;
      key.Query(true, 343, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 15);
      for (int index = 0; index < 15; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[15];
    byte[] numArray5 = new byte[15]
    {
      (byte) 93,
      (byte) 82,
      (byte) 222,
      (byte) 187,
      (byte) 171,
      (byte) 237,
      (byte) 229,
      (byte) 61,
      (byte) 201,
      (byte) 210,
      (byte) 237,
      (byte) 116,
      (byte) 216,
      (byte) 200,
      (byte) 160 /*0xA0*/
    };
    byte[] numArray6 = new byte[15];
    numArray6[2] = (byte) 171;
    numArray6[1] = (byte) 121;
    numArray6[0] = (byte) 196;
    numArray6[3] = (byte) 106;
    numArray6[11] = (byte) 64 /*0x40*/;
    numArray6[7] = (byte) 162;
    numArray6[6] = (byte) 39;
    numArray6[4] = (byte) 49;
    numArray6[12] = (byte) 39;
    numArray6[8] = (byte) 183;
    numArray6[10] = (byte) 161;
    numArray6[5] = (byte) 180;
    numArray6[14] = (byte) 2;
    numArray6[13] = (byte) 44;
    numArray6[9] = (byte) 243;
    key.Query(true, 343, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 15);
    for (int index = 0; index < 15; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
