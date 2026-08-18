// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_6342
// Assembly: Intermech.ECO.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BF6FF14F-986B-44C3-A04A-31D571D76B17
// Assembly location: D:\IPS\Client\Intermech.ECO.Client.dll

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_6342
{
  internal static string ssp_eco_6343()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[31 /*0x1F*/];
      byte[] numArray2 = new byte[31 /*0x1F*/]
      {
        (byte) 138,
        (byte) 27,
        (byte) 81,
        (byte) 223,
        (byte) 129,
        (byte) 252,
        (byte) 157,
        (byte) 75,
        (byte) 211,
        (byte) 120,
        (byte) 167,
        (byte) 196,
        (byte) 76,
        (byte) 115,
        (byte) 61,
        byte.MaxValue,
        (byte) 26,
        (byte) 221,
        (byte) 232,
        (byte) 99,
        (byte) 32 /*0x20*/,
        (byte) 61,
        (byte) 53,
        (byte) 138,
        (byte) 70,
        (byte) 114,
        (byte) 241,
        (byte) 203,
        (byte) 195,
        (byte) 143,
        (byte) 229
      };
      byte[] numArray3 = new byte[31 /*0x1F*/]
      {
        (byte) 143,
        (byte) 223,
        (byte) 37,
        (byte) 62,
        (byte) 221,
        (byte) 21,
        (byte) 239,
        (byte) 45,
        (byte) 135,
        (byte) 152,
        (byte) 71,
        (byte) 8,
        (byte) 119,
        (byte) 135,
        (byte) 221,
        (byte) 61,
        byte.MaxValue,
        (byte) 23,
        (byte) 84,
        (byte) 40,
        (byte) 136,
        (byte) 69,
        (byte) 243,
        (byte) 9,
        (byte) 247,
        (byte) 100,
        (byte) 123,
        (byte) 190,
        (byte) 184,
        (byte) 117,
        (byte) 96 /*0x60*/
      };
      key.Query(true, 340, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 31 /*0x1F*/);
      for (int index = 0; index < 31 /*0x1F*/; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[31 /*0x1F*/];
    byte[] numArray5 = new byte[31 /*0x1F*/];
    numArray5[4] = (byte) 165;
    numArray5[1] = (byte) 1;
    numArray5[6] = (byte) 202;
    numArray5[24] = (byte) 152;
    numArray5[11] = (byte) 140;
    numArray5[5] = (byte) 29;
    numArray5[12] = (byte) 151;
    numArray5[20] = (byte) 88;
    numArray5[0] = (byte) 183;
    numArray5[9] = (byte) 237;
    numArray5[14] = (byte) 3;
    numArray5[10] = (byte) 205;
    numArray5[3] = (byte) 152;
    numArray5[13] = (byte) 100;
    numArray5[21] = (byte) 77;
    numArray5[29] = (byte) 67;
    numArray5[16 /*0x10*/] = (byte) 65;
    numArray5[7] = (byte) 193;
    numArray5[2] = (byte) 174;
    numArray5[19] = (byte) 140;
    numArray5[18] = (byte) 216;
    numArray5[17] = (byte) 246;
    numArray5[22] = (byte) 229;
    numArray5[23] = (byte) 198;
    numArray5[8] = (byte) 105;
    numArray5[25] = (byte) 14;
    numArray5[15] = (byte) 34;
    numArray5[27] = (byte) 210;
    numArray5[28] = (byte) 136;
    numArray5[26] = (byte) 10;
    numArray5[30] = (byte) 146;
    byte[] numArray6 = new byte[31 /*0x1F*/]
    {
      (byte) 129,
      (byte) 131,
      (byte) 216,
      (byte) 236,
      (byte) 157,
      (byte) 210,
      (byte) 62,
      (byte) 234,
      (byte) 81,
      (byte) 153,
      (byte) 97,
      (byte) 229,
      (byte) 124,
      (byte) 24,
      (byte) 16 /*0x10*/,
      (byte) 35,
      (byte) 87,
      (byte) 26,
      (byte) 179,
      (byte) 47,
      (byte) 109,
      (byte) 28,
      (byte) 224 /*0xE0*/,
      (byte) 235,
      (byte) 142,
      (byte) 72,
      (byte) 5,
      (byte) 241,
      (byte) 192 /*0xC0*/,
      (byte) 38,
      (byte) 111
    };
    key.Query(true, 340, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 31 /*0x1F*/);
    for (int index = 0; index < 31 /*0x1F*/; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static int ssp_eco_6344(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 94,
      (byte) 169,
      (byte) 199,
      (byte) 208 /*0xD0*/,
      (byte) 191,
      (byte) 189,
      (byte) 13,
      (byte) 244,
      (byte) 250,
      (byte) 121,
      (byte) 157,
      (byte) 8,
      (byte) 222,
      (byte) 51,
      (byte) 124,
      (byte) 10,
      (byte) 231,
      (byte) 245,
      (byte) 163,
      (byte) 91,
      (byte) 146,
      (byte) 126,
      (byte) 80 /*0x50*/,
      (byte) 32 /*0x20*/,
      (byte) 105,
      (byte) 130,
      (byte) 161,
      (byte) 184,
      (byte) 159,
      (byte) 36,
      (byte) 174,
      (byte) 237,
      (byte) 132,
      (byte) 18,
      (byte) 124,
      (byte) 228,
      (byte) 0,
      (byte) 208 /*0xD0*/,
      (byte) 143,
      (byte) 49,
      (byte) 44,
      (byte) 94,
      (byte) 191,
      (byte) 241,
      (byte) 125,
      (byte) 222,
      (byte) 33,
      (byte) 99
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 244,
      (byte) 167,
      (byte) 109,
      (byte) 203,
      (byte) 225,
      (byte) 216,
      (byte) 49,
      (byte) 190,
      (byte) 74,
      (byte) 91,
      (byte) 182,
      (byte) 222,
      (byte) 94,
      (byte) 44,
      (byte) 113,
      (byte) 10,
      (byte) 193,
      (byte) 15,
      (byte) 160 /*0xA0*/,
      (byte) 57,
      (byte) 88,
      (byte) 146,
      (byte) 198,
      (byte) 254,
      (byte) 42,
      (byte) 176 /*0xB0*/,
      (byte) 253,
      (byte) 227,
      (byte) 145,
      (byte) 8,
      (byte) 58,
      (byte) 226,
      (byte) 254,
      (byte) 183,
      (byte) 46,
      (byte) 96 /*0x60*/,
      (byte) 199,
      (byte) 15,
      (byte) 56,
      (byte) 84,
      (byte) 133,
      (byte) 18,
      (byte) 251,
      (byte) 149,
      (byte) 223,
      (byte) 75,
      (byte) 128 /*0x80*/,
      (byte) 224 /*0xE0*/
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 340, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_eco_6345(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 221,
      (byte) 21,
      (byte) 223,
      (byte) 88,
      (byte) 166,
      (byte) 102,
      (byte) 60,
      (byte) 28,
      (byte) 84,
      (byte) 84,
      (byte) 214,
      (byte) 202,
      (byte) 176 /*0xB0*/,
      (byte) 131,
      (byte) 204,
      (byte) 182,
      (byte) 152,
      (byte) 9,
      (byte) 22,
      (byte) 102,
      (byte) 114,
      (byte) 47,
      (byte) 52,
      (byte) 190,
      (byte) 95,
      (byte) 163,
      (byte) 138,
      (byte) 96 /*0x60*/,
      (byte) 62,
      (byte) 34,
      (byte) 177,
      (byte) 209,
      (byte) 127 /*0x7F*/,
      (byte) 54,
      (byte) 167,
      (byte) 252,
      (byte) 157,
      (byte) 139,
      (byte) 90,
      (byte) 105,
      (byte) 82,
      (byte) 20,
      (byte) 215,
      (byte) 9,
      (byte) 233,
      (byte) 39,
      (byte) 204,
      (byte) 91
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 54,
      (byte) 158,
      (byte) 227,
      (byte) 108,
      (byte) 57,
      (byte) 46,
      (byte) 64 /*0x40*/,
      (byte) 95,
      (byte) 194,
      (byte) 69,
      (byte) 152,
      (byte) 127 /*0x7F*/,
      (byte) 46,
      (byte) 181,
      (byte) 225,
      (byte) 82,
      (byte) 233,
      (byte) 250,
      (byte) 11,
      (byte) 87,
      (byte) 118,
      (byte) 25,
      (byte) 111,
      (byte) 114,
      (byte) 187,
      (byte) 133,
      (byte) 30,
      (byte) 6,
      (byte) 178,
      (byte) 74,
      (byte) 124,
      (byte) 68,
      (byte) 87,
      (byte) 237,
      (byte) 80 /*0x50*/,
      (byte) 110,
      (byte) 151,
      (byte) 56,
      (byte) 245,
      (byte) 229,
      (byte) 39,
      (byte) 174,
      (byte) 35,
      (byte) 146,
      (byte) 231,
      (byte) 251,
      (byte) 210,
      (byte) 207
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 340, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_eco_6346(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[32 /*0x20*/] = (byte) 204;
    sourceArray1[8] = (byte) 219;
    sourceArray1[1] = (byte) 69;
    sourceArray1[3] = (byte) 225;
    sourceArray1[36] = (byte) 249;
    sourceArray1[31 /*0x1F*/] = (byte) 37;
    sourceArray1[28] = (byte) 47;
    sourceArray1[16 /*0x10*/] = (byte) 207;
    sourceArray1[6] = (byte) 2;
    sourceArray1[45] = (byte) 102;
    sourceArray1[5] = (byte) 249;
    sourceArray1[39] = (byte) 146;
    sourceArray1[12] = (byte) 4;
    sourceArray1[13] = (byte) 75;
    sourceArray1[34] = (byte) 181;
    sourceArray1[15] = (byte) 220;
    sourceArray1[27] = (byte) 214;
    sourceArray1[4] = (byte) 32 /*0x20*/;
    sourceArray1[18] = (byte) 13;
    sourceArray1[20] = (byte) 172;
    sourceArray1[7] = (byte) 219;
    sourceArray1[29] = (byte) 11;
    sourceArray1[22] = (byte) 93;
    sourceArray1[2] = (byte) 173;
    sourceArray1[24] = (byte) 42;
    sourceArray1[25] = (byte) 212;
    sourceArray1[26] = (byte) 55;
    sourceArray1[10] = (byte) 50;
    sourceArray1[42] = (byte) 118;
    sourceArray1[35] = (byte) 241;
    sourceArray1[30] = (byte) 145;
    sourceArray1[11] = (byte) 162;
    sourceArray1[47] = (byte) 220;
    sourceArray1[33] = (byte) 150;
    sourceArray1[41] = (byte) 141;
    sourceArray1[0] = (byte) 201;
    sourceArray1[21] = (byte) 19;
    sourceArray1[37] = (byte) 27;
    sourceArray1[38] = (byte) 124;
    sourceArray1[9] = (byte) 222;
    sourceArray1[17] = (byte) 25;
    sourceArray1[19] = (byte) 175;
    sourceArray1[23] = (byte) 88;
    sourceArray1[43] = (byte) 54;
    sourceArray1[44] = (byte) 164;
    sourceArray1[40] = (byte) 87;
    sourceArray1[46] = (byte) 229;
    sourceArray1[14] = (byte) 122;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 194,
      (byte) 50,
      (byte) 77,
      (byte) 135,
      (byte) 200,
      (byte) 17,
      (byte) 201,
      (byte) 231,
      (byte) 77,
      (byte) 55,
      (byte) 139,
      (byte) 188,
      (byte) 30,
      (byte) 112 /*0x70*/,
      (byte) 44,
      (byte) 200,
      (byte) 18,
      (byte) 252,
      (byte) 152,
      (byte) 150,
      (byte) 83,
      (byte) 211,
      byte.MaxValue,
      (byte) 192 /*0xC0*/,
      (byte) 146,
      (byte) 116,
      (byte) 233,
      (byte) 97,
      (byte) 228,
      (byte) 173,
      (byte) 143,
      (byte) 208 /*0xD0*/,
      (byte) 28,
      (byte) 206,
      (byte) 212,
      (byte) 141,
      (byte) 195,
      (byte) 237,
      (byte) 164,
      (byte) 172,
      (byte) 250,
      (byte) 25,
      (byte) 126,
      (byte) 194,
      (byte) 199,
      (byte) 202,
      (byte) 63 /*0x3F*/,
      (byte) 121
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 340, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_eco_6347(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 119,
      (byte) 70,
      (byte) 202,
      (byte) 156,
      (byte) 209,
      (byte) 246,
      (byte) 116,
      (byte) 221,
      (byte) 111,
      (byte) 25,
      (byte) 192 /*0xC0*/,
      (byte) 162,
      (byte) 243,
      (byte) 92,
      (byte) 101,
      (byte) 167,
      (byte) 251,
      (byte) 74,
      (byte) 77,
      (byte) 215,
      (byte) 62,
      (byte) 102,
      (byte) 150,
      (byte) 126,
      (byte) 61,
      (byte) 79,
      (byte) 107,
      (byte) 170,
      (byte) 44,
      (byte) 61,
      (byte) 246,
      (byte) 61,
      (byte) 27,
      (byte) 50,
      (byte) 119,
      (byte) 21,
      (byte) 186,
      (byte) 20,
      (byte) 180,
      (byte) 8,
      (byte) 192 /*0xC0*/,
      (byte) 186,
      (byte) 151,
      (byte) 229,
      (byte) 73,
      (byte) 1,
      (byte) 113,
      (byte) 212
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 23,
      (byte) 173,
      (byte) 0,
      (byte) 44,
      (byte) 151,
      (byte) 42,
      (byte) 209,
      (byte) 93,
      (byte) 84,
      (byte) 185,
      (byte) 178,
      (byte) 144 /*0x90*/,
      (byte) 13,
      (byte) 165,
      (byte) 100,
      (byte) 132,
      (byte) 217,
      (byte) 123,
      (byte) 153,
      (byte) 254,
      (byte) 95,
      (byte) 55,
      byte.MaxValue,
      (byte) 15,
      (byte) 52,
      (byte) 168,
      (byte) 174,
      (byte) 39,
      (byte) 219,
      (byte) 234,
      (byte) 155,
      (byte) 40,
      (byte) 54,
      (byte) 212,
      (byte) 179,
      (byte) 37,
      (byte) 10,
      (byte) 224 /*0xE0*/,
      (byte) 251,
      (byte) 226,
      (byte) 119,
      (byte) 89,
      (byte) 199,
      (byte) 181,
      (byte) 52,
      (byte) 177,
      (byte) 171,
      (byte) 217
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 340, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_eco_6348(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 191,
      (byte) 82,
      (byte) 16 /*0x10*/,
      (byte) 175,
      (byte) 75,
      (byte) 94,
      (byte) 130,
      (byte) 138,
      (byte) 202,
      (byte) 177,
      (byte) 50,
      (byte) 35,
      (byte) 130,
      (byte) 135,
      (byte) 192 /*0xC0*/,
      (byte) 228,
      (byte) 79,
      (byte) 237,
      (byte) 231,
      (byte) 150,
      (byte) 68,
      (byte) 23,
      (byte) 131,
      (byte) 207,
      (byte) 177,
      (byte) 53,
      (byte) 60,
      (byte) 222,
      (byte) 150,
      (byte) 63 /*0x3F*/,
      (byte) 167,
      (byte) 87,
      (byte) 154,
      (byte) 161,
      (byte) 91,
      (byte) 224 /*0xE0*/,
      (byte) 103,
      (byte) 102,
      (byte) 110,
      (byte) 181,
      (byte) 49,
      (byte) 125,
      (byte) 246,
      (byte) 9,
      (byte) 101,
      (byte) 193,
      (byte) 199,
      (byte) 122
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 68,
      (byte) 135,
      (byte) 32 /*0x20*/,
      (byte) 225,
      (byte) 82,
      (byte) 186,
      (byte) 58,
      (byte) 168,
      (byte) 249,
      (byte) 102,
      (byte) 13,
      (byte) 196,
      (byte) 88,
      (byte) 106,
      (byte) 158,
      (byte) 68,
      (byte) 148,
      (byte) 151,
      (byte) 189,
      (byte) 184,
      (byte) 149,
      (byte) 97,
      (byte) 224 /*0xE0*/,
      (byte) 209,
      (byte) 153,
      (byte) 104,
      (byte) 218,
      (byte) 208 /*0xD0*/,
      (byte) 236,
      (byte) 187,
      (byte) 26,
      (byte) 166,
      (byte) 112 /*0x70*/,
      (byte) 3,
      (byte) 121,
      (byte) 224 /*0xE0*/,
      (byte) 111,
      (byte) 214,
      (byte) 55,
      (byte) 161,
      (byte) 60,
      (byte) 201,
      (byte) 103,
      (byte) 167,
      (byte) 45,
      (byte) 41,
      (byte) 206,
      (byte) 209
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 340, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_eco_6349(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[25] = (byte) 105;
    sourceArray1[21] = (byte) 101;
    sourceArray1[2] = (byte) 168;
    sourceArray1[10] = (byte) 139;
    sourceArray1[18] = (byte) 233;
    sourceArray1[5] = (byte) 253;
    sourceArray1[34] = (byte) 212;
    sourceArray1[7] = (byte) 34;
    sourceArray1[8] = (byte) 164;
    sourceArray1[40] = (byte) 88;
    sourceArray1[27] = (byte) 110;
    sourceArray1[16 /*0x10*/] = (byte) 224 /*0xE0*/;
    sourceArray1[15] = (byte) 43;
    sourceArray1[13] = (byte) 137;
    sourceArray1[14] = (byte) 224 /*0xE0*/;
    sourceArray1[32 /*0x20*/] = (byte) 17;
    sourceArray1[33] = (byte) 87;
    sourceArray1[17] = (byte) 125;
    sourceArray1[35] = (byte) 74;
    sourceArray1[19] = (byte) 126;
    sourceArray1[41] = (byte) 146;
    sourceArray1[47] = (byte) 56;
    sourceArray1[30] = (byte) 166;
    sourceArray1[23] = (byte) 33;
    sourceArray1[42] = (byte) 140;
    sourceArray1[9] = (byte) 244;
    sourceArray1[26] = (byte) 240 /*0xF0*/;
    sourceArray1[22] = (byte) 198;
    sourceArray1[6] = (byte) 22;
    sourceArray1[29] = (byte) 45;
    sourceArray1[28] = (byte) 193;
    sourceArray1[31 /*0x1F*/] = (byte) 35;
    sourceArray1[20] = (byte) 49;
    sourceArray1[45] = byte.MaxValue;
    sourceArray1[43] = (byte) 137;
    sourceArray1[11] = (byte) 9;
    sourceArray1[36] = (byte) 179;
    sourceArray1[37] = (byte) 17;
    sourceArray1[38] = (byte) 37;
    sourceArray1[39] = (byte) 104;
    sourceArray1[1] = (byte) 87;
    sourceArray1[0] = (byte) 197;
    sourceArray1[3] = (byte) 206;
    sourceArray1[12] = (byte) 219;
    sourceArray1[44] = (byte) 107;
    sourceArray1[4] = (byte) 65;
    sourceArray1[46] = (byte) 138;
    sourceArray1[24] = (byte) 122;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 82,
      (byte) 200,
      (byte) 73,
      (byte) 223,
      (byte) 229,
      (byte) 192 /*0xC0*/,
      (byte) 185,
      (byte) 28,
      (byte) 88,
      (byte) 27,
      (byte) 118,
      (byte) 81,
      (byte) 26,
      (byte) 94,
      (byte) 235,
      (byte) 29,
      (byte) 121,
      (byte) 135,
      (byte) 120,
      (byte) 6,
      (byte) 74,
      (byte) 97,
      (byte) 41,
      (byte) 87,
      (byte) 240 /*0xF0*/,
      (byte) 117,
      (byte) 24,
      (byte) 215,
      (byte) 133,
      (byte) 167,
      (byte) 30,
      (byte) 9,
      (byte) 106,
      (byte) 254,
      (byte) 156,
      (byte) 78,
      (byte) 223,
      (byte) 33,
      (byte) 158,
      (byte) 45,
      (byte) 6,
      (byte) 146,
      (byte) 2,
      (byte) 222,
      (byte) 226,
      (byte) 16 /*0x10*/,
      (byte) 227,
      (byte) 69
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 340, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_eco_6350(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 7,
      (byte) 131,
      (byte) 81,
      (byte) 214,
      byte.MaxValue,
      (byte) 27,
      (byte) 177,
      (byte) 241,
      (byte) 45,
      (byte) 136,
      (byte) 82,
      (byte) 208 /*0xD0*/,
      (byte) 216,
      (byte) 249,
      (byte) 85,
      (byte) 32 /*0x20*/,
      (byte) 238,
      (byte) 24,
      (byte) 27,
      (byte) 117,
      byte.MaxValue,
      (byte) 249,
      (byte) 64 /*0x40*/,
      (byte) 219,
      (byte) 75,
      (byte) 17,
      (byte) 10,
      (byte) 36,
      (byte) 117,
      (byte) 109,
      (byte) 194,
      (byte) 2,
      (byte) 178,
      (byte) 116,
      (byte) 215,
      (byte) 52,
      (byte) 54,
      (byte) 156,
      (byte) 94,
      (byte) 23,
      (byte) 64 /*0x40*/,
      (byte) 7,
      (byte) 92,
      (byte) 236,
      (byte) 103,
      (byte) 91,
      (byte) 229,
      (byte) 84
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[38] = (byte) 244;
    sourceArray2[16 /*0x10*/] = (byte) 224 /*0xE0*/;
    sourceArray2[19] = (byte) 244;
    sourceArray2[1] = (byte) 130;
    sourceArray2[47] = (byte) 116;
    sourceArray2[21] = (byte) 66;
    sourceArray2[40] = (byte) 250;
    sourceArray2[7] = (byte) 59;
    sourceArray2[22] = (byte) 250;
    sourceArray2[28] = (byte) 174;
    sourceArray2[10] = (byte) 11;
    sourceArray2[39] = (byte) 234;
    sourceArray2[12] = (byte) 148;
    sourceArray2[26] = (byte) 206;
    sourceArray2[23] = (byte) 194;
    sourceArray2[15] = (byte) 130;
    sourceArray2[46] = (byte) 44;
    sourceArray2[17] = (byte) 54;
    sourceArray2[18] = (byte) 55;
    sourceArray2[13] = (byte) 2;
    sourceArray2[20] = (byte) 148;
    sourceArray2[11] = (byte) 99;
    sourceArray2[0] = (byte) 152;
    sourceArray2[5] = (byte) 172;
    sourceArray2[24] = (byte) 207;
    sourceArray2[25] = (byte) 179;
    sourceArray2[14] = (byte) 176 /*0xB0*/;
    sourceArray2[27] = (byte) 37;
    sourceArray2[3] = (byte) 198;
    sourceArray2[44] = (byte) 116;
    sourceArray2[30] = (byte) 218;
    sourceArray2[31 /*0x1F*/] = (byte) 177;
    sourceArray2[32 /*0x20*/] = (byte) 34;
    sourceArray2[6] = (byte) 93;
    sourceArray2[34] = (byte) 214;
    sourceArray2[35] = (byte) 51;
    sourceArray2[29] = (byte) 212;
    sourceArray2[4] = (byte) 73;
    sourceArray2[43] = (byte) 192 /*0xC0*/;
    sourceArray2[2] = (byte) 129;
    sourceArray2[37] = (byte) 107;
    sourceArray2[41] = (byte) 207;
    sourceArray2[42] = (byte) 239;
    sourceArray2[33] = (byte) 191;
    sourceArray2[36] = (byte) 217;
    sourceArray2[45] = (byte) 190;
    sourceArray2[8] = (byte) 213;
    sourceArray2[9] = (byte) 55;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 340, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_eco_6351(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 129,
      (byte) 51,
      (byte) 229,
      (byte) 164,
      (byte) 241,
      (byte) 68,
      (byte) 28,
      (byte) 89,
      (byte) 16 /*0x10*/,
      (byte) 113,
      (byte) 239,
      (byte) 2,
      (byte) 154,
      (byte) 193,
      (byte) 91,
      (byte) 110,
      (byte) 149,
      (byte) 149,
      (byte) 1,
      (byte) 109,
      (byte) 251,
      (byte) 247,
      (byte) 87,
      (byte) 47,
      (byte) 42,
      (byte) 64 /*0x40*/,
      (byte) 73,
      (byte) 253,
      (byte) 130,
      (byte) 107,
      (byte) 0,
      (byte) 45,
      (byte) 91,
      (byte) 178,
      (byte) 171,
      (byte) 130,
      (byte) 57,
      (byte) 65,
      (byte) 56,
      (byte) 127 /*0x7F*/,
      (byte) 198,
      (byte) 201,
      (byte) 92,
      (byte) 88,
      (byte) 1,
      (byte) 189,
      (byte) 118,
      (byte) 174
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[1] = (byte) 230;
    sourceArray2[11] = (byte) 98;
    sourceArray2[2] = (byte) 206;
    sourceArray2[3] = (byte) 71;
    sourceArray2[31 /*0x1F*/] = (byte) 50;
    sourceArray2[13] = (byte) 59;
    sourceArray2[6] = (byte) 222;
    sourceArray2[46] = (byte) 121;
    sourceArray2[36] = (byte) 41;
    sourceArray2[9] = (byte) 149;
    sourceArray2[10] = (byte) 113;
    sourceArray2[12] = (byte) 224 /*0xE0*/;
    sourceArray2[37] = (byte) 74;
    sourceArray2[0] = (byte) 125;
    sourceArray2[14] = (byte) 146;
    sourceArray2[15] = (byte) 248;
    sourceArray2[27] = (byte) 90;
    sourceArray2[17] = (byte) 176 /*0xB0*/;
    sourceArray2[43] = (byte) 17;
    sourceArray2[19] = (byte) 18;
    sourceArray2[20] = (byte) 51;
    sourceArray2[21] = (byte) 62;
    sourceArray2[25] = (byte) 96 /*0x60*/;
    sourceArray2[23] = (byte) 138;
    sourceArray2[24] = (byte) 179;
    sourceArray2[45] = (byte) 127 /*0x7F*/;
    sourceArray2[26] = (byte) 246;
    sourceArray2[7] = (byte) 161;
    sourceArray2[28] = (byte) 80 /*0x50*/;
    sourceArray2[29] = (byte) 167;
    sourceArray2[30] = (byte) 152;
    sourceArray2[22] = (byte) 150;
    sourceArray2[5] = (byte) 105;
    sourceArray2[33] = (byte) 15;
    sourceArray2[34] = (byte) 173;
    sourceArray2[18] = (byte) 149;
    sourceArray2[16 /*0x10*/] = (byte) 207;
    sourceArray2[35] = (byte) 229;
    sourceArray2[4] = (byte) 139;
    sourceArray2[39] = (byte) 71;
    sourceArray2[38] = (byte) 27;
    sourceArray2[40] = (byte) 51;
    sourceArray2[42] = (byte) 175;
    sourceArray2[32 /*0x20*/] = (byte) 104;
    sourceArray2[44] = (byte) 47;
    sourceArray2[47] = (byte) 17;
    sourceArray2[41] = (byte) 2;
    sourceArray2[8] = (byte) 85;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 340, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_eco_6352(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 142,
      (byte) 151,
      (byte) 210,
      (byte) 15,
      (byte) 161,
      (byte) 169,
      (byte) 162,
      (byte) 2,
      (byte) 16 /*0x10*/,
      (byte) 238,
      (byte) 221,
      (byte) 134,
      (byte) 12,
      (byte) 231,
      (byte) 96 /*0x60*/,
      (byte) 204,
      (byte) 154,
      (byte) 46,
      (byte) 118,
      (byte) 185,
      (byte) 245,
      (byte) 118,
      (byte) 245,
      (byte) 49,
      (byte) 176 /*0xB0*/,
      (byte) 140,
      (byte) 213,
      (byte) 253,
      byte.MaxValue,
      (byte) 63 /*0x3F*/,
      (byte) 226,
      (byte) 21,
      (byte) 239,
      (byte) 87,
      (byte) 58,
      (byte) 3,
      (byte) 127 /*0x7F*/,
      (byte) 45,
      (byte) 211,
      (byte) 80 /*0x50*/,
      (byte) 102,
      (byte) 175,
      (byte) 96 /*0x60*/,
      (byte) 50,
      (byte) 201,
      (byte) 253,
      (byte) 186,
      (byte) 160 /*0xA0*/
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 110,
      (byte) 157,
      (byte) 70,
      (byte) 158,
      (byte) 111,
      (byte) 115,
      (byte) 64 /*0x40*/,
      (byte) 188,
      (byte) 18,
      (byte) 174,
      (byte) 195,
      (byte) 102,
      (byte) 108,
      (byte) 152,
      (byte) 94,
      (byte) 187,
      (byte) 89,
      (byte) 71,
      (byte) 6,
      (byte) 140,
      (byte) 69,
      (byte) 72,
      (byte) 110,
      (byte) 71,
      (byte) 181,
      (byte) 190,
      (byte) 154,
      (byte) 250,
      (byte) 20,
      (byte) 186,
      (byte) 80 /*0x50*/,
      (byte) 212,
      (byte) 215,
      (byte) 42,
      (byte) 68,
      (byte) 20,
      (byte) 70,
      (byte) 138,
      (byte) 146,
      (byte) 169,
      (byte) 105,
      (byte) 69,
      (byte) 91,
      (byte) 25,
      (byte) 28,
      (byte) 128 /*0x80*/,
      (byte) 147,
      (byte) 78
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 340, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_eco_6353(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 134,
      (byte) 33,
      (byte) 23,
      (byte) 161,
      (byte) 161,
      (byte) 94,
      (byte) 20,
      (byte) 212,
      (byte) 164,
      (byte) 2,
      (byte) 208 /*0xD0*/,
      (byte) 12,
      (byte) 200,
      (byte) 151,
      (byte) 181,
      (byte) 172,
      (byte) 92,
      (byte) 76,
      (byte) 108,
      (byte) 90,
      (byte) 37,
      (byte) 82,
      (byte) 69,
      (byte) 22,
      (byte) 195,
      (byte) 104,
      (byte) 179,
      (byte) 210,
      (byte) 52,
      (byte) 217,
      (byte) 36,
      (byte) 232,
      (byte) 88,
      (byte) 162,
      (byte) 104,
      (byte) 54,
      (byte) 35,
      (byte) 97,
      (byte) 222,
      (byte) 206,
      (byte) 190,
      (byte) 123,
      (byte) 194,
      (byte) 173,
      (byte) 144 /*0x90*/,
      (byte) 38,
      (byte) 0,
      (byte) 101
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[26] = (byte) 195;
    sourceArray2[28] = (byte) 175;
    sourceArray2[15] = (byte) 11;
    sourceArray2[44] = (byte) 96 /*0x60*/;
    sourceArray2[14] = (byte) 254;
    sourceArray2[5] = (byte) 212;
    sourceArray2[4] = (byte) 160 /*0xA0*/;
    sourceArray2[7] = (byte) 197;
    sourceArray2[10] = (byte) 10;
    sourceArray2[40] = (byte) 187;
    sourceArray2[34] = (byte) 9;
    sourceArray2[11] = (byte) 205;
    sourceArray2[12] = (byte) 231;
    sourceArray2[13] = (byte) 185;
    sourceArray2[2] = (byte) 108;
    sourceArray2[35] = (byte) 157;
    sourceArray2[29] = (byte) 28;
    sourceArray2[17] = (byte) 176 /*0xB0*/;
    sourceArray2[6] = (byte) 46;
    sourceArray2[19] = (byte) 173;
    sourceArray2[18] = (byte) 175;
    sourceArray2[21] = (byte) 27;
    sourceArray2[0] = (byte) 62;
    sourceArray2[23] = (byte) 131;
    sourceArray2[24] = (byte) 119;
    sourceArray2[33] = (byte) 146;
    sourceArray2[20] = (byte) 29;
    sourceArray2[27] = (byte) 176 /*0xB0*/;
    sourceArray2[37] = (byte) 70;
    sourceArray2[42] = (byte) 221;
    sourceArray2[38] = (byte) 126;
    sourceArray2[31 /*0x1F*/] = (byte) 77;
    sourceArray2[22] = (byte) 223;
    sourceArray2[30] = (byte) 154;
    sourceArray2[1] = (byte) 24;
    sourceArray2[9] = (byte) 32 /*0x20*/;
    sourceArray2[36] = (byte) 197;
    sourceArray2[8] = (byte) 86;
    sourceArray2[16 /*0x10*/] = (byte) 216;
    sourceArray2[39] = (byte) 194;
    sourceArray2[25] = (byte) 77;
    sourceArray2[32 /*0x20*/] = (byte) 12;
    sourceArray2[47] = (byte) 78;
    sourceArray2[43] = (byte) 151;
    sourceArray2[3] = (byte) 56;
    sourceArray2[45] = (byte) 229;
    sourceArray2[46] = (byte) 247;
    sourceArray2[41] = (byte) 162;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 340, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_eco_6354(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[20] = (byte) 138;
    sourceArray1[1] = (byte) 55;
    sourceArray1[15] = (byte) 117;
    sourceArray1[3] = (byte) 106;
    sourceArray1[4] = (byte) 203;
    sourceArray1[16 /*0x10*/] = (byte) 94;
    sourceArray1[6] = (byte) 77;
    sourceArray1[43] = (byte) 144 /*0x90*/;
    sourceArray1[32 /*0x20*/] = (byte) 222;
    sourceArray1[34] = (byte) 139;
    sourceArray1[10] = (byte) 67;
    sourceArray1[11] = (byte) 123;
    sourceArray1[12] = (byte) 66;
    sourceArray1[13] = (byte) 210;
    sourceArray1[39] = (byte) 117;
    sourceArray1[46] = (byte) 64 /*0x40*/;
    sourceArray1[42] = (byte) 179;
    sourceArray1[17] = (byte) 185;
    sourceArray1[18] = (byte) 72;
    sourceArray1[2] = (byte) 133;
    sourceArray1[22] = (byte) 249;
    sourceArray1[5] = (byte) 31 /*0x1F*/;
    sourceArray1[23] = (byte) 37;
    sourceArray1[27] = (byte) 122;
    sourceArray1[24] = (byte) 103;
    sourceArray1[44] = (byte) 22;
    sourceArray1[9] = (byte) 164;
    sourceArray1[19] = (byte) 235;
    sourceArray1[21] = (byte) 207;
    sourceArray1[36] = (byte) 71;
    sourceArray1[30] = (byte) 153;
    sourceArray1[37] = (byte) 214;
    sourceArray1[35] = (byte) 22;
    sourceArray1[33] = (byte) 41;
    sourceArray1[26] = (byte) 202;
    sourceArray1[0] = (byte) 219;
    sourceArray1[25] = (byte) 121;
    sourceArray1[41] = (byte) 149;
    sourceArray1[38] = (byte) 69;
    sourceArray1[31 /*0x1F*/] = (byte) 200;
    sourceArray1[40] = (byte) 176 /*0xB0*/;
    sourceArray1[29] = (byte) 206;
    sourceArray1[28] = (byte) 31 /*0x1F*/;
    sourceArray1[14] = (byte) 93;
    sourceArray1[8] = (byte) 162;
    sourceArray1[45] = (byte) 165;
    sourceArray1[47] = (byte) 210;
    sourceArray1[7] = (byte) 147;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[42] = (byte) 247;
    sourceArray2[33] = (byte) 24;
    sourceArray2[2] = (byte) 219;
    sourceArray2[39] = (byte) 201;
    sourceArray2[24] = (byte) 170;
    sourceArray2[30] = (byte) 228;
    sourceArray2[6] = (byte) 141;
    sourceArray2[7] = (byte) 248;
    sourceArray2[16 /*0x10*/] = (byte) 75;
    sourceArray2[17] = (byte) 230;
    sourceArray2[10] = (byte) 202;
    sourceArray2[11] = (byte) 111;
    sourceArray2[12] = (byte) 157;
    sourceArray2[13] = (byte) 81;
    sourceArray2[28] = (byte) 134;
    sourceArray2[15] = (byte) 118;
    sourceArray2[9] = (byte) 59;
    sourceArray2[18] = (byte) 95;
    sourceArray2[3] = (byte) 48 /*0x30*/;
    sourceArray2[19] = (byte) 150;
    sourceArray2[8] = (byte) 6;
    sourceArray2[38] = (byte) 6;
    sourceArray2[22] = (byte) 150;
    sourceArray2[23] = (byte) 44;
    sourceArray2[40] = (byte) 62;
    sourceArray2[14] = (byte) 133;
    sourceArray2[47] = (byte) 60;
    sourceArray2[25] = (byte) 86;
    sourceArray2[1] = (byte) 31 /*0x1F*/;
    sourceArray2[29] = (byte) 12;
    sourceArray2[0] = (byte) 33;
    sourceArray2[31 /*0x1F*/] = (byte) 58;
    sourceArray2[32 /*0x20*/] = (byte) 149;
    sourceArray2[26] = (byte) 56;
    sourceArray2[35] = (byte) 207;
    sourceArray2[37] = (byte) 231;
    sourceArray2[34] = (byte) 147;
    sourceArray2[27] = (byte) 214;
    sourceArray2[4] = (byte) 166;
    sourceArray2[5] = (byte) 78;
    sourceArray2[21] = (byte) 84;
    sourceArray2[41] = (byte) 171;
    sourceArray2[20] = (byte) 21;
    sourceArray2[43] = (byte) 248;
    sourceArray2[36] = (byte) 252;
    sourceArray2[45] = (byte) 90;
    sourceArray2[46] = (byte) 79;
    sourceArray2[44] = (byte) 51;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 340, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_eco_6355(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[5] = (byte) 98;
    sourceArray1[45] = (byte) 30;
    sourceArray1[19] = (byte) 134;
    sourceArray1[32 /*0x20*/] = (byte) 33;
    sourceArray1[4] = (byte) 215;
    sourceArray1[47] = (byte) 139;
    sourceArray1[15] = (byte) 30;
    sourceArray1[41] = (byte) 191;
    sourceArray1[26] = (byte) 171;
    sourceArray1[0] = (byte) 193;
    sourceArray1[42] = (byte) 166;
    sourceArray1[11] = (byte) 62;
    sourceArray1[2] = (byte) 197;
    sourceArray1[31 /*0x1F*/] = (byte) 179;
    sourceArray1[7] = (byte) 81;
    sourceArray1[3] = (byte) 91;
    sourceArray1[16 /*0x10*/] = (byte) 178;
    sourceArray1[17] = (byte) 240 /*0xF0*/;
    sourceArray1[18] = (byte) 222;
    sourceArray1[1] = (byte) 191;
    sourceArray1[25] = (byte) 25;
    sourceArray1[21] = (byte) 242;
    sourceArray1[10] = (byte) 143;
    sourceArray1[12] = (byte) 71;
    sourceArray1[24] = (byte) 37;
    sourceArray1[13] = (byte) 1;
    sourceArray1[22] = (byte) 231;
    sourceArray1[27] = (byte) 220;
    sourceArray1[23] = (byte) 26;
    sourceArray1[9] = (byte) 15;
    sourceArray1[30] = (byte) 105;
    sourceArray1[34] = (byte) 240 /*0xF0*/;
    sourceArray1[36] = (byte) 15;
    sourceArray1[33] = (byte) 167;
    sourceArray1[37] = (byte) 225;
    sourceArray1[35] = (byte) 158;
    sourceArray1[46] = (byte) 76;
    sourceArray1[29] = (byte) 54;
    sourceArray1[38] = (byte) 12;
    sourceArray1[39] = (byte) 17;
    sourceArray1[40] = (byte) 45;
    sourceArray1[28] = (byte) 136;
    sourceArray1[6] = (byte) 140;
    sourceArray1[43] = (byte) 39;
    sourceArray1[44] = (byte) 177;
    sourceArray1[14] = (byte) 36;
    sourceArray1[20] = (byte) 8;
    sourceArray1[8] = (byte) 197;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 201,
      (byte) 85,
      (byte) 57,
      (byte) 101,
      (byte) 219,
      (byte) 191,
      (byte) 222,
      (byte) 228,
      (byte) 42,
      (byte) 135,
      (byte) 235,
      (byte) 15,
      (byte) 168,
      (byte) 248,
      (byte) 184,
      (byte) 244,
      (byte) 237,
      (byte) 104,
      (byte) 180,
      (byte) 77,
      (byte) 108,
      (byte) 140,
      (byte) 133,
      (byte) 148,
      (byte) 18,
      (byte) 202,
      (byte) 120,
      (byte) 235,
      (byte) 34,
      (byte) 58,
      (byte) 36,
      (byte) 5,
      (byte) 134,
      (byte) 193,
      (byte) 79,
      (byte) 244,
      (byte) 40,
      (byte) 140,
      (byte) 252,
      (byte) 2,
      (byte) 154,
      (byte) 17,
      (byte) 155,
      (byte) 149,
      (byte) 157,
      (byte) 195,
      (byte) 78,
      (byte) 162
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 340, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_expert_6356(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[36] = (byte) 109;
    sourceArray1[1] = (byte) 122;
    sourceArray1[2] = (byte) 244;
    sourceArray1[3] = (byte) 33;
    sourceArray1[4] = (byte) 12;
    sourceArray1[6] = (byte) 233;
    sourceArray1[41] = (byte) 99;
    sourceArray1[38] = (byte) 77;
    sourceArray1[27] = (byte) 114;
    sourceArray1[18] = (byte) 80 /*0x50*/;
    sourceArray1[43] = (byte) 157;
    sourceArray1[30] = (byte) 51;
    sourceArray1[47] = (byte) 94;
    sourceArray1[24] = (byte) 170;
    sourceArray1[25] = (byte) 37;
    sourceArray1[15] = (byte) 78;
    sourceArray1[22] = (byte) 239;
    sourceArray1[17] = (byte) 52;
    sourceArray1[10] = (byte) 22;
    sourceArray1[19] = (byte) 221;
    sourceArray1[9] = (byte) 131;
    sourceArray1[12] = (byte) 47;
    sourceArray1[8] = (byte) 136;
    sourceArray1[13] = (byte) 240 /*0xF0*/;
    sourceArray1[33] = (byte) 5;
    sourceArray1[45] = (byte) 33;
    sourceArray1[16 /*0x10*/] = (byte) 113;
    sourceArray1[14] = (byte) 151;
    sourceArray1[28] = (byte) 221;
    sourceArray1[37] = (byte) 54;
    sourceArray1[0] = (byte) 173;
    sourceArray1[31 /*0x1F*/] = (byte) 63 /*0x3F*/;
    sourceArray1[32 /*0x20*/] = (byte) 146;
    sourceArray1[21] = (byte) 201;
    sourceArray1[34] = (byte) 31 /*0x1F*/;
    sourceArray1[35] = (byte) 164;
    sourceArray1[26] = (byte) 148;
    sourceArray1[39] = (byte) 35;
    sourceArray1[20] = (byte) 7;
    sourceArray1[5] = (byte) 49;
    sourceArray1[40] = (byte) 20;
    sourceArray1[11] = (byte) 66;
    sourceArray1[42] = (byte) 78;
    sourceArray1[23] = (byte) 192 /*0xC0*/;
    sourceArray1[44] = (byte) 175;
    sourceArray1[29] = (byte) 81;
    sourceArray1[46] = (byte) 124;
    sourceArray1[7] = (byte) 178;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 125,
      (byte) 39,
      (byte) 251,
      (byte) 66,
      (byte) 161,
      (byte) 129,
      (byte) 80 /*0x50*/,
      (byte) 204,
      (byte) 240 /*0xF0*/,
      (byte) 52,
      (byte) 130,
      (byte) 46,
      (byte) 22,
      (byte) 50,
      (byte) 105,
      (byte) 164,
      (byte) 240 /*0xF0*/,
      (byte) 235,
      (byte) 140,
      (byte) 243,
      (byte) 52,
      (byte) 101,
      (byte) 53,
      (byte) 92,
      (byte) 180,
      (byte) 128 /*0x80*/,
      (byte) 204,
      (byte) 140,
      (byte) 44,
      (byte) 14,
      (byte) 235,
      (byte) 229,
      (byte) 201,
      (byte) 85,
      (byte) 150,
      (byte) 17,
      (byte) 60,
      (byte) 39,
      (byte) 218,
      (byte) 161,
      (byte) 226,
      (byte) 205,
      (byte) 155,
      (byte) 130,
      (byte) 32 /*0x20*/,
      (byte) 42,
      (byte) 217,
      (byte) 191
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 342, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
