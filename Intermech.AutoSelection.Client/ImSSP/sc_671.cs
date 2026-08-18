// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_671
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.Protection;
using System;

#nullable disable
namespace ImSSP;

internal static class sc_671
{
  internal static int ssp_automatch_672(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 193,
      (byte) 75,
      (byte) 124,
      (byte) 230,
      (byte) 142,
      (byte) 249,
      (byte) 35,
      (byte) 1,
      (byte) 177,
      (byte) 17,
      (byte) 227,
      (byte) 195,
      (byte) 46,
      (byte) 22,
      (byte) 62,
      (byte) 184,
      (byte) 67,
      (byte) 67,
      (byte) 105,
      (byte) 241,
      (byte) 0,
      (byte) 104,
      (byte) 166,
      (byte) 233,
      (byte) 197,
      (byte) 43,
      (byte) 159,
      (byte) 38,
      (byte) 107,
      (byte) 131,
      (byte) 41,
      (byte) 11,
      (byte) 140,
      (byte) 26,
      (byte) 123,
      (byte) 171,
      (byte) 228,
      (byte) 134,
      (byte) 82,
      (byte) 17,
      (byte) 184,
      (byte) 103,
      (byte) 174,
      (byte) 184,
      (byte) 155,
      (byte) 96 /*0x60*/,
      (byte) 110,
      (byte) 179
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 39,
      (byte) 95,
      (byte) 78,
      (byte) 89,
      (byte) 196,
      (byte) 245,
      (byte) 178,
      (byte) 242,
      (byte) 6,
      (byte) 204,
      (byte) 90,
      (byte) 135,
      (byte) 251,
      (byte) 70,
      (byte) 91,
      (byte) 136,
      (byte) 111,
      (byte) 87,
      (byte) 66,
      (byte) 61,
      (byte) 111,
      (byte) 85,
      (byte) 163,
      (byte) 196,
      (byte) 222,
      (byte) 123,
      (byte) 203,
      (byte) 125,
      (byte) 123,
      (byte) 107,
      (byte) 142,
      (byte) 17,
      (byte) 47,
      (byte) 85,
      (byte) 103,
      (byte) 184,
      (byte) 181,
      (byte) 143,
      (byte) 127 /*0x7F*/,
      (byte) 80 /*0x50*/,
      (byte) 84,
      (byte) 15,
      (byte) 213,
      (byte) 211,
      (byte) 185,
      (byte) 174,
      (byte) 30,
      (byte) 199
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 338, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_automatch_673(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[36] = (byte) 34;
    sourceArray1[1] = (byte) 40;
    sourceArray1[47] = (byte) 41;
    sourceArray1[5] = (byte) 32 /*0x20*/;
    sourceArray1[17] = (byte) 212;
    sourceArray1[8] = (byte) 253;
    sourceArray1[6] = (byte) 98;
    sourceArray1[7] = (byte) 89;
    sourceArray1[46] = (byte) 195;
    sourceArray1[42] = (byte) 187;
    sourceArray1[9] = (byte) 149;
    sourceArray1[11] = (byte) 231;
    sourceArray1[12] = (byte) 216;
    sourceArray1[30] = (byte) 100;
    sourceArray1[34] = (byte) 28;
    sourceArray1[14] = (byte) 19;
    sourceArray1[16 /*0x10*/] = (byte) 147;
    sourceArray1[13] = (byte) 55;
    sourceArray1[24] = (byte) 238;
    sourceArray1[19] = (byte) 241;
    sourceArray1[15] = (byte) 154;
    sourceArray1[21] = (byte) 57;
    sourceArray1[22] = (byte) 152;
    sourceArray1[26] = (byte) 101;
    sourceArray1[40] = (byte) 239;
    sourceArray1[28] = (byte) 235;
    sourceArray1[20] = (byte) 88;
    sourceArray1[27] = (byte) 188;
    sourceArray1[10] = (byte) 46;
    sourceArray1[29] = (byte) 206;
    sourceArray1[0] = (byte) 112 /*0x70*/;
    sourceArray1[31 /*0x1F*/] = (byte) 115;
    sourceArray1[32 /*0x20*/] = (byte) 104;
    sourceArray1[33] = (byte) 27;
    sourceArray1[3] = (byte) 164;
    sourceArray1[35] = (byte) 241;
    sourceArray1[41] = (byte) 183;
    sourceArray1[37] = (byte) 6;
    sourceArray1[38] = (byte) 81;
    sourceArray1[39] = (byte) 101;
    sourceArray1[4] = (byte) 146;
    sourceArray1[2] = (byte) 88;
    sourceArray1[18] = (byte) 188;
    sourceArray1[43] = (byte) 209;
    sourceArray1[44] = (byte) 128 /*0x80*/;
    sourceArray1[25] = (byte) 77;
    sourceArray1[23] = (byte) 186;
    sourceArray1[45] = (byte) 208 /*0xD0*/;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 122,
      (byte) 220,
      (byte) 10,
      (byte) 254,
      (byte) 242,
      (byte) 85,
      (byte) 112 /*0x70*/,
      (byte) 39,
      (byte) 20,
      (byte) 153,
      (byte) 131,
      (byte) 236,
      (byte) 138,
      (byte) 132,
      (byte) 136,
      (byte) 53,
      (byte) 131,
      (byte) 249,
      (byte) 186,
      (byte) 83,
      (byte) 164,
      (byte) 150,
      (byte) 203,
      (byte) 10,
      (byte) 129,
      (byte) 73,
      (byte) 70,
      byte.MaxValue,
      (byte) 20,
      (byte) 72,
      (byte) 236,
      (byte) 105,
      (byte) 43,
      (byte) 204,
      (byte) 192 /*0xC0*/,
      (byte) 103,
      (byte) 210,
      (byte) 241,
      (byte) 67,
      (byte) 174,
      (byte) 167,
      (byte) 59,
      (byte) 29,
      (byte) 216,
      byte.MaxValue,
      (byte) 24,
      (byte) 69,
      (byte) 52
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 338, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
