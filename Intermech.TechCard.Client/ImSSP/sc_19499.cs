// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_19499
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Protection;
using System;

#nullable disable
namespace ImSSP;

internal static class sc_19499
{
  internal static int ssp_techcard_19500(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 178,
      (byte) 242,
      (byte) 94,
      (byte) 230,
      (byte) 114,
      (byte) 94,
      (byte) 192 /*0xC0*/,
      (byte) 85,
      (byte) 208 /*0xD0*/,
      (byte) 50,
      (byte) 9,
      (byte) 31 /*0x1F*/,
      (byte) 126,
      (byte) 2,
      (byte) 169,
      (byte) 106,
      (byte) 111,
      (byte) 143,
      (byte) 7,
      (byte) 245,
      (byte) 235,
      (byte) 231,
      (byte) 165,
      (byte) 202,
      (byte) 76,
      (byte) 35,
      (byte) 251,
      (byte) 125,
      (byte) 161,
      (byte) 162,
      (byte) 182,
      (byte) 195,
      (byte) 6,
      (byte) 69,
      (byte) 218,
      (byte) 116,
      (byte) 63 /*0x3F*/,
      (byte) 28,
      (byte) 92,
      (byte) 120,
      (byte) 194,
      (byte) 102,
      (byte) 12,
      (byte) 195,
      (byte) 253,
      (byte) 163,
      (byte) 57,
      (byte) 114
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 50,
      (byte) 114,
      (byte) 239,
      (byte) 96 /*0x60*/,
      (byte) 98,
      (byte) 133,
      (byte) 243,
      (byte) 143,
      (byte) 52,
      (byte) 16 /*0x10*/,
      (byte) 126,
      (byte) 175,
      (byte) 127 /*0x7F*/,
      (byte) 210,
      (byte) 115,
      (byte) 242,
      (byte) 113,
      (byte) 123,
      (byte) 242,
      (byte) 15,
      (byte) 226,
      (byte) 137,
      (byte) 135,
      (byte) 145,
      (byte) 199,
      (byte) 32 /*0x20*/,
      (byte) 120,
      (byte) 70,
      (byte) 121,
      (byte) 82,
      (byte) 254,
      (byte) 105,
      (byte) 124,
      (byte) 64 /*0x40*/,
      (byte) 223,
      (byte) 198,
      (byte) 2,
      (byte) 51,
      (byte) 93,
      (byte) 126,
      (byte) 28,
      (byte) 76,
      (byte) 247,
      (byte) 114,
      (byte) 133,
      (byte) 137,
      (byte) 52,
      (byte) 211
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 359, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_techcard_19501(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[4] = (byte) 147;
    sourceArray1[33] = (byte) 158;
    sourceArray1[2] = (byte) 202;
    sourceArray1[20] = (byte) 143;
    sourceArray1[17] = (byte) 42;
    sourceArray1[5] = (byte) 154;
    sourceArray1[31 /*0x1F*/] = (byte) 207;
    sourceArray1[7] = (byte) 99;
    sourceArray1[1] = (byte) 1;
    sourceArray1[44] = (byte) 221;
    sourceArray1[9] = (byte) 179;
    sourceArray1[11] = (byte) 123;
    sourceArray1[45] = (byte) 71;
    sourceArray1[13] = (byte) 24;
    sourceArray1[14] = (byte) 56;
    sourceArray1[19] = (byte) 51;
    sourceArray1[36] = (byte) 15;
    sourceArray1[41] = (byte) 20;
    sourceArray1[47] = (byte) 138;
    sourceArray1[34] = (byte) 50;
    sourceArray1[10] = (byte) 46;
    sourceArray1[21] = (byte) 50;
    sourceArray1[22] = (byte) 133;
    sourceArray1[29] = (byte) 118;
    sourceArray1[24] = (byte) 225;
    sourceArray1[18] = (byte) 83;
    sourceArray1[0] = (byte) 211;
    sourceArray1[27] = (byte) 234;
    sourceArray1[28] = (byte) 136;
    sourceArray1[40] = (byte) 13;
    sourceArray1[6] = (byte) 78;
    sourceArray1[30] = (byte) 9;
    sourceArray1[32 /*0x20*/] = (byte) 57;
    sourceArray1[8] = (byte) 67;
    sourceArray1[15] = (byte) 39;
    sourceArray1[23] = (byte) 219;
    sourceArray1[3] = (byte) 77;
    sourceArray1[37] = (byte) 38;
    sourceArray1[38] = (byte) 172;
    sourceArray1[39] = (byte) 129;
    sourceArray1[42] = (byte) 127 /*0x7F*/;
    sourceArray1[35] = (byte) 68;
    sourceArray1[16 /*0x10*/] = (byte) 53;
    sourceArray1[43] = (byte) 178;
    sourceArray1[25] = (byte) 204;
    sourceArray1[26] = (byte) 168;
    sourceArray1[46] = (byte) 183;
    sourceArray1[12] = (byte) 69;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 179,
      (byte) 208 /*0xD0*/,
      (byte) 20,
      (byte) 140,
      (byte) 56,
      (byte) 225,
      (byte) 64 /*0x40*/,
      (byte) 143,
      (byte) 77,
      (byte) 21,
      (byte) 253,
      (byte) 57,
      (byte) 174,
      (byte) 46,
      (byte) 148,
      (byte) 111,
      (byte) 247,
      (byte) 243,
      (byte) 80 /*0x50*/,
      (byte) 234,
      (byte) 134,
      (byte) 188,
      (byte) 244,
      (byte) 84,
      (byte) 101,
      (byte) 1,
      (byte) 238,
      (byte) 152,
      (byte) 231,
      (byte) 148,
      (byte) 246,
      (byte) 7,
      (byte) 230,
      (byte) 84,
      (byte) 70,
      byte.MaxValue,
      (byte) 61,
      (byte) 250,
      (byte) 178,
      (byte) 181,
      (byte) 18,
      (byte) 50,
      (byte) 252,
      (byte) 149,
      (byte) 22,
      (byte) 12,
      (byte) 97,
      (byte) 225
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 359, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_techcard_19502(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 17,
      (byte) 164,
      (byte) 13,
      (byte) 50,
      (byte) 195,
      (byte) 240 /*0xF0*/,
      (byte) 173,
      (byte) 35,
      (byte) 27,
      (byte) 237,
      (byte) 168,
      (byte) 242,
      (byte) 209,
      (byte) 177,
      (byte) 211,
      (byte) 122,
      (byte) 156,
      (byte) 40,
      (byte) 15,
      (byte) 14,
      (byte) 82,
      (byte) 157,
      (byte) 84,
      (byte) 42,
      (byte) 248,
      (byte) 83,
      (byte) 152,
      (byte) 65,
      (byte) 124,
      (byte) 64 /*0x40*/,
      (byte) 1,
      (byte) 202,
      (byte) 126,
      (byte) 149,
      (byte) 3,
      (byte) 121,
      (byte) 220,
      (byte) 11,
      (byte) 224 /*0xE0*/,
      (byte) 170,
      (byte) 163,
      (byte) 203,
      (byte) 237,
      (byte) 164,
      (byte) 32 /*0x20*/,
      (byte) 45,
      (byte) 233,
      (byte) 51
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 84,
      (byte) 132,
      (byte) 249,
      (byte) 206,
      (byte) 240 /*0xF0*/,
      (byte) 50,
      (byte) 202,
      (byte) 185,
      (byte) 64 /*0x40*/,
      (byte) 123,
      (byte) 244,
      (byte) 164,
      (byte) 11,
      (byte) 70,
      (byte) 200,
      (byte) 144 /*0x90*/,
      (byte) 1,
      (byte) 151,
      (byte) 205,
      (byte) 172,
      (byte) 238,
      (byte) 21,
      (byte) 98,
      (byte) 252,
      (byte) 10,
      (byte) 104,
      (byte) 182,
      (byte) 142,
      (byte) 2,
      (byte) 101,
      (byte) 91,
      (byte) 181,
      (byte) 217,
      (byte) 32 /*0x20*/,
      (byte) 248,
      (byte) 33,
      (byte) 118,
      (byte) 77,
      (byte) 146,
      (byte) 208 /*0xD0*/,
      (byte) 163,
      (byte) 36,
      (byte) 134,
      (byte) 203,
      (byte) 37,
      (byte) 62,
      (byte) 140,
      (byte) 229
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 359, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
