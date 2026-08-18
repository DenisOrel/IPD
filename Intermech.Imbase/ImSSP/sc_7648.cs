// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_7648
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_7648
{
  internal static string ssp_imbase_7649()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[48 /*0x30*/];
      byte[] numArray2 = new byte[48 /*0x30*/]
      {
        (byte) 94,
        (byte) 14,
        (byte) 9,
        (byte) 116,
        (byte) 188,
        (byte) 46,
        (byte) 34,
        (byte) 250,
        (byte) 131,
        (byte) 122,
        (byte) 250,
        (byte) 17,
        (byte) 120,
        (byte) 190,
        (byte) 239,
        (byte) 22,
        (byte) 202,
        (byte) 38,
        (byte) 35,
        (byte) 243,
        (byte) 45,
        (byte) 25,
        (byte) 41,
        (byte) 96 /*0x60*/,
        (byte) 138,
        (byte) 40,
        (byte) 67,
        (byte) 208 /*0xD0*/,
        (byte) 2,
        (byte) 138,
        (byte) 243,
        (byte) 162,
        (byte) 161,
        (byte) 202,
        (byte) 174,
        (byte) 186,
        (byte) 5,
        (byte) 162,
        (byte) 240 /*0xF0*/,
        (byte) 138,
        (byte) 177,
        (byte) 192 /*0xC0*/,
        (byte) 72,
        (byte) 61,
        (byte) 93,
        (byte) 126,
        (byte) 31 /*0x1F*/,
        (byte) 135
      };
      byte[] numArray3 = new byte[48 /*0x30*/];
      numArray3[24] = (byte) 123;
      numArray3[0] = (byte) 127 /*0x7F*/;
      numArray3[16 /*0x10*/] = (byte) 50;
      numArray3[19] = (byte) 5;
      numArray3[15] = (byte) 8;
      numArray3[5] = (byte) 93;
      numArray3[6] = (byte) 62;
      numArray3[12] = (byte) 15;
      numArray3[26] = (byte) 107;
      numArray3[9] = (byte) 99;
      numArray3[38] = (byte) 135;
      numArray3[11] = (byte) 123;
      numArray3[14] = (byte) 119;
      numArray3[35] = (byte) 250;
      numArray3[44] = (byte) 2;
      numArray3[7] = (byte) 189;
      numArray3[22] = (byte) 57;
      numArray3[17] = (byte) 56;
      numArray3[45] = (byte) 69;
      numArray3[47] = (byte) 146;
      numArray3[20] = (byte) 139;
      numArray3[21] = (byte) 182;
      numArray3[10] = (byte) 115;
      numArray3[3] = (byte) 134;
      numArray3[18] = (byte) 233;
      numArray3[25] = (byte) 228;
      numArray3[43] = (byte) 193;
      numArray3[29] = (byte) 114;
      numArray3[1] = (byte) 155;
      numArray3[27] = (byte) 178;
      numArray3[30] = (byte) 188;
      numArray3[31 /*0x1F*/] = (byte) 16 /*0x10*/;
      numArray3[32 /*0x20*/] = (byte) 151;
      numArray3[33] = (byte) 162;
      numArray3[34] = (byte) 62;
      numArray3[28] = (byte) 247;
      numArray3[36] = (byte) 7;
      numArray3[37] = (byte) 43;
      numArray3[2] = (byte) 160 /*0xA0*/;
      numArray3[39] = (byte) 247;
      numArray3[40] = (byte) 171;
      numArray3[41] = (byte) 77;
      numArray3[42] = (byte) 18;
      numArray3[23] = (byte) 72;
      numArray3[13] = (byte) 228;
      numArray3[46] = (byte) 81;
      numArray3[4] = (byte) 39;
      numArray3[8] = (byte) 245;
      key.Query(true, 343, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 48 /*0x30*/);
      for (int index = 0; index < 48 /*0x30*/; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[48 /*0x30*/];
    byte[] numArray5 = new byte[48 /*0x30*/]
    {
      (byte) 6,
      (byte) 13,
      (byte) 113,
      (byte) 119,
      (byte) 157,
      (byte) 44,
      (byte) 204,
      (byte) 221,
      (byte) 9,
      (byte) 28,
      (byte) 229,
      (byte) 116,
      (byte) 249,
      (byte) 166,
      (byte) 221,
      (byte) 138,
      (byte) 46,
      (byte) 84,
      (byte) 51,
      (byte) 99,
      (byte) 168,
      (byte) 86,
      (byte) 45,
      (byte) 53,
      (byte) 167,
      (byte) 95,
      (byte) 181,
      (byte) 112 /*0x70*/,
      (byte) 218,
      (byte) 250,
      (byte) 226,
      (byte) 164,
      (byte) 129,
      (byte) 53,
      (byte) 252,
      byte.MaxValue,
      (byte) 152,
      (byte) 68,
      (byte) 95,
      (byte) 152,
      (byte) 200,
      (byte) 1,
      (byte) 247,
      (byte) 38,
      (byte) 145,
      (byte) 244,
      (byte) 220,
      (byte) 180
    };
    byte[] numArray6 = new byte[48 /*0x30*/]
    {
      (byte) 33,
      (byte) 161,
      (byte) 109,
      (byte) 90,
      (byte) 130,
      (byte) 141,
      (byte) 98,
      (byte) 64 /*0x40*/,
      (byte) 146,
      (byte) 104,
      (byte) 77,
      (byte) 118,
      (byte) 241,
      (byte) 173,
      (byte) 161,
      (byte) 186,
      (byte) 39,
      (byte) 185,
      (byte) 214,
      (byte) 14,
      (byte) 103,
      (byte) 199,
      (byte) 129,
      (byte) 233,
      (byte) 17,
      (byte) 234,
      (byte) 102,
      (byte) 163,
      (byte) 232,
      (byte) 173,
      (byte) 20,
      (byte) 178,
      (byte) 76,
      (byte) 3,
      (byte) 4,
      (byte) 165,
      (byte) 234,
      (byte) 154,
      (byte) 79,
      byte.MaxValue,
      (byte) 112 /*0x70*/,
      (byte) 178,
      (byte) 246,
      (byte) 170,
      (byte) 27,
      (byte) 158,
      (byte) 224 /*0xE0*/,
      (byte) 154
    };
    key.Query(true, 343, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 48 /*0x30*/);
    for (int index = 0; index < 48 /*0x30*/; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
