// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_19635
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Protection;
using System;

#nullable disable
namespace ImSSP;

internal static class sc_19635
{
  internal static int ssp_techcard_19636(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 22,
      (byte) 89,
      (byte) 78,
      (byte) 98,
      (byte) 214,
      (byte) 175,
      (byte) 98,
      (byte) 220,
      (byte) 63 /*0x3F*/,
      (byte) 225,
      (byte) 123,
      (byte) 208 /*0xD0*/,
      (byte) 73,
      (byte) 233,
      (byte) 179,
      (byte) 9,
      (byte) 196,
      (byte) 244,
      (byte) 77,
      (byte) 216,
      (byte) 53,
      (byte) 156,
      (byte) 81,
      (byte) 164,
      (byte) 83,
      (byte) 40,
      (byte) 76,
      (byte) 61,
      (byte) 220,
      (byte) 50,
      (byte) 101,
      (byte) 13,
      (byte) 78,
      (byte) 235,
      (byte) 152,
      (byte) 153,
      (byte) 244,
      (byte) 215,
      (byte) 58,
      (byte) 129,
      (byte) 176 /*0xB0*/,
      (byte) 111,
      (byte) 196,
      (byte) 83,
      (byte) 103,
      (byte) 89,
      (byte) 60,
      (byte) 178
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 111,
      (byte) 71,
      (byte) 18,
      (byte) 58,
      (byte) 194,
      (byte) 40,
      (byte) 218,
      (byte) 243,
      (byte) 108,
      (byte) 69,
      (byte) 16 /*0x10*/,
      (byte) 246,
      (byte) 138,
      (byte) 54,
      (byte) 72,
      (byte) 86,
      (byte) 176 /*0xB0*/,
      (byte) 85,
      (byte) 87,
      (byte) 238,
      (byte) 180,
      (byte) 26,
      (byte) 130,
      (byte) 35,
      (byte) 254,
      (byte) 152,
      (byte) 24,
      (byte) 204,
      (byte) 199,
      (byte) 155,
      (byte) 136,
      (byte) 78,
      (byte) 182,
      (byte) 233,
      (byte) 147,
      (byte) 174,
      (byte) 222,
      (byte) 95,
      (byte) 78,
      (byte) 83,
      (byte) 114,
      (byte) 39,
      (byte) 254,
      (byte) 42,
      (byte) 30,
      (byte) 60,
      (byte) 73,
      (byte) 162
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 359, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_techcard_19637(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 228,
      (byte) 130,
      (byte) 122,
      (byte) 8,
      (byte) 170,
      (byte) 15,
      (byte) 174,
      (byte) 98,
      (byte) 37,
      byte.MaxValue,
      (byte) 13,
      (byte) 123,
      (byte) 110,
      (byte) 130,
      (byte) 93,
      (byte) 80 /*0x50*/,
      (byte) 25,
      (byte) 21,
      (byte) 237,
      (byte) 185,
      (byte) 203,
      (byte) 40,
      (byte) 198,
      (byte) 215,
      (byte) 253,
      (byte) 61,
      (byte) 249,
      (byte) 236,
      (byte) 153,
      (byte) 85,
      (byte) 205,
      (byte) 203,
      (byte) 238,
      (byte) 66,
      (byte) 105,
      (byte) 105,
      (byte) 30,
      (byte) 144 /*0x90*/,
      (byte) 74,
      (byte) 200,
      (byte) 140,
      (byte) 30,
      (byte) 138,
      (byte) 111,
      (byte) 182,
      (byte) 215,
      (byte) 248,
      (byte) 215
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 172,
      (byte) 100,
      (byte) 116,
      (byte) 197,
      (byte) 69,
      (byte) 203,
      (byte) 219,
      (byte) 224 /*0xE0*/,
      (byte) 230,
      (byte) 226,
      (byte) 126,
      (byte) 167,
      (byte) 244,
      (byte) 125,
      (byte) 60,
      (byte) 204,
      (byte) 232,
      (byte) 69,
      (byte) 87,
      (byte) 19,
      (byte) 22,
      (byte) 110,
      (byte) 204,
      (byte) 199,
      (byte) 194,
      (byte) 120,
      (byte) 88,
      (byte) 10,
      (byte) 60,
      (byte) 117,
      (byte) 71,
      (byte) 131,
      (byte) 64 /*0x40*/,
      (byte) 147,
      (byte) 3,
      (byte) 35,
      (byte) 124,
      (byte) 245,
      (byte) 227,
      (byte) 18,
      (byte) 74,
      (byte) 20,
      (byte) 76,
      (byte) 127 /*0x7F*/,
      (byte) 24,
      (byte) 138,
      (byte) 240 /*0xF0*/,
      (byte) 27
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 359, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_techcard_19638(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[18] = (byte) 136;
    sourceArray1[1] = (byte) 254;
    sourceArray1[5] = (byte) 10;
    sourceArray1[0] = (byte) 189;
    sourceArray1[3] = (byte) 188;
    sourceArray1[41] = (byte) 178;
    sourceArray1[23] = (byte) 157;
    sourceArray1[37] = (byte) 173;
    sourceArray1[39] = (byte) 134;
    sourceArray1[11] = (byte) 50;
    sourceArray1[10] = (byte) 208 /*0xD0*/;
    sourceArray1[17] = (byte) 50;
    sourceArray1[12] = (byte) 65;
    sourceArray1[4] = (byte) 162;
    sourceArray1[14] = (byte) 43;
    sourceArray1[15] = (byte) 25;
    sourceArray1[16 /*0x10*/] = (byte) 244;
    sourceArray1[32 /*0x20*/] = (byte) 211;
    sourceArray1[13] = (byte) 141;
    sourceArray1[8] = (byte) 194;
    sourceArray1[43] = (byte) 86;
    sourceArray1[7] = (byte) 21;
    sourceArray1[22] = (byte) 173;
    sourceArray1[24] = (byte) 86;
    sourceArray1[40] = (byte) 221;
    sourceArray1[25] = (byte) 33;
    sourceArray1[9] = (byte) 200;
    sourceArray1[27] = (byte) 68;
    sourceArray1[2] = (byte) 188;
    sourceArray1[29] = (byte) 14;
    sourceArray1[30] = (byte) 56;
    sourceArray1[31 /*0x1F*/] = (byte) 134;
    sourceArray1[35] = (byte) 68;
    sourceArray1[33] = (byte) 82;
    sourceArray1[34] = (byte) 48 /*0x30*/;
    sourceArray1[28] = (byte) 34;
    sourceArray1[36] = (byte) 172;
    sourceArray1[19] = (byte) 203;
    sourceArray1[21] = (byte) 45;
    sourceArray1[20] = (byte) 36;
    sourceArray1[38] = (byte) 123;
    sourceArray1[6] = (byte) 108;
    sourceArray1[42] = (byte) 202;
    sourceArray1[26] = (byte) 38;
    sourceArray1[44] = (byte) 186;
    sourceArray1[45] = (byte) 61;
    sourceArray1[46] = (byte) 117;
    sourceArray1[47] = (byte) 74;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 113,
      (byte) 42,
      (byte) 170,
      (byte) 120,
      (byte) 173,
      (byte) 58,
      (byte) 247,
      (byte) 34,
      (byte) 166,
      (byte) 90,
      (byte) 213,
      (byte) 246,
      (byte) 30,
      (byte) 13,
      (byte) 112 /*0x70*/,
      (byte) 196,
      (byte) 75,
      (byte) 5,
      (byte) 74,
      (byte) 211,
      (byte) 178,
      (byte) 36,
      (byte) 3,
      (byte) 103,
      (byte) 178,
      (byte) 127 /*0x7F*/,
      (byte) 238,
      (byte) 177,
      (byte) 12,
      (byte) 40,
      (byte) 252,
      (byte) 200,
      (byte) 92,
      (byte) 180,
      (byte) 58,
      (byte) 191,
      (byte) 203,
      (byte) 167,
      (byte) 34,
      (byte) 246,
      (byte) 248,
      (byte) 184,
      (byte) 131,
      (byte) 170,
      (byte) 175,
      (byte) 148,
      (byte) 230,
      (byte) 112 /*0x70*/
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 359, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
