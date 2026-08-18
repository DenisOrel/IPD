// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_19291
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_19291
{
  internal static string ssp_techcard_19292()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19]
      {
        (byte) 7,
        (byte) 104,
        (byte) 56,
        (byte) 64 /*0x40*/,
        (byte) 21,
        (byte) 118,
        (byte) 122,
        (byte) 236,
        (byte) 34,
        (byte) 24,
        (byte) 9,
        (byte) 157,
        (byte) 54,
        (byte) 46,
        (byte) 46,
        (byte) 36,
        (byte) 198,
        (byte) 252,
        (byte) 46
      };
      byte[] numArray3 = new byte[19]
      {
        (byte) 102,
        (byte) 247,
        (byte) 28,
        (byte) 34,
        (byte) 106,
        (byte) 96 /*0x60*/,
        (byte) 180,
        (byte) 59,
        (byte) 19,
        (byte) 188,
        (byte) 108,
        (byte) 115,
        (byte) 96 /*0x60*/,
        (byte) 228,
        (byte) 147,
        (byte) 197,
        (byte) 139,
        (byte) 11,
        byte.MaxValue
      };
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[19];
    byte[] numArray5 = new byte[19];
    numArray5[4] = (byte) 42;
    numArray5[5] = (byte) 75;
    numArray5[12] = (byte) 95;
    numArray5[2] = (byte) 176 /*0xB0*/;
    numArray5[0] = (byte) 64 /*0x40*/;
    numArray5[3] = (byte) 46;
    numArray5[6] = (byte) 37;
    numArray5[7] = (byte) 216;
    numArray5[18] = (byte) 12;
    numArray5[1] = (byte) 238;
    numArray5[10] = (byte) 97;
    numArray5[11] = (byte) 214;
    numArray5[17] = (byte) 117;
    numArray5[13] = (byte) 208 /*0xD0*/;
    numArray5[14] = (byte) 168;
    numArray5[15] = (byte) 136;
    numArray5[16 /*0x10*/] = (byte) 219;
    numArray5[8] = (byte) 69;
    numArray5[9] = (byte) 249;
    byte[] numArray6 = new byte[19];
    numArray6[8] = (byte) 108;
    numArray6[1] = (byte) 233;
    numArray6[4] = (byte) 9;
    numArray6[3] = (byte) 147;
    numArray6[5] = (byte) 203;
    numArray6[15] = (byte) 233;
    numArray6[6] = (byte) 159;
    numArray6[7] = (byte) 144 /*0x90*/;
    numArray6[10] = (byte) 28;
    numArray6[9] = (byte) 206;
    numArray6[11] = (byte) 54;
    numArray6[17] = (byte) 104;
    numArray6[18] = (byte) 233;
    numArray6[0] = (byte) 12;
    numArray6[14] = (byte) 125;
    numArray6[2] = (byte) 23;
    numArray6[16 /*0x10*/] = (byte) 79;
    numArray6[13] = (byte) 100;
    numArray6[12] = (byte) 132;
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_techcard_19293()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[77];
      byte[] numArray2 = new byte[55]
      {
        (byte) 123,
        (byte) 52,
        (byte) 56,
        (byte) 157,
        (byte) 31 /*0x1F*/,
        (byte) 44,
        (byte) 45,
        (byte) 168,
        (byte) 94,
        (byte) 248,
        (byte) 244,
        (byte) 41,
        (byte) 221,
        (byte) 225,
        (byte) 1,
        (byte) 198,
        (byte) 208 /*0xD0*/,
        (byte) 16 /*0x10*/,
        (byte) 186,
        (byte) 180,
        (byte) 179,
        (byte) 45,
        (byte) 101,
        (byte) 234,
        (byte) 162,
        (byte) 195,
        (byte) 97,
        (byte) 168,
        (byte) 129,
        (byte) 59,
        (byte) 44,
        (byte) 210,
        (byte) 165,
        (byte) 244,
        (byte) 75,
        (byte) 22,
        (byte) 154,
        (byte) 114,
        (byte) 28,
        (byte) 51,
        (byte) 162,
        (byte) 181,
        (byte) 29,
        (byte) 69,
        (byte) 45,
        (byte) 143,
        (byte) 160 /*0xA0*/,
        (byte) 102,
        (byte) 136,
        (byte) 224 /*0xE0*/,
        (byte) 62,
        (byte) 82,
        (byte) 89,
        (byte) 241,
        (byte) 135
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 198,
        (byte) 241,
        (byte) 39,
        (byte) 189,
        (byte) 103,
        (byte) 121,
        (byte) 125,
        (byte) 106,
        (byte) 103,
        (byte) 5,
        (byte) 165,
        (byte) 123,
        (byte) 5,
        (byte) 87,
        (byte) 56,
        (byte) 59,
        (byte) 22,
        (byte) 244,
        (byte) 250,
        (byte) 252,
        (byte) 135,
        (byte) 43,
        (byte) 241,
        (byte) 209,
        (byte) 60,
        (byte) 138,
        (byte) 182,
        (byte) 231,
        (byte) 249,
        (byte) 221,
        (byte) 248,
        (byte) 44,
        (byte) 51,
        (byte) 12,
        (byte) 56,
        (byte) 184,
        (byte) 133,
        (byte) 239,
        (byte) 185,
        (byte) 131,
        (byte) 29,
        (byte) 79,
        (byte) 81,
        (byte) 172,
        (byte) 102,
        (byte) 208 /*0xD0*/,
        (byte) 1,
        (byte) 3,
        (byte) 91,
        (byte) 232,
        (byte) 112 /*0x70*/,
        (byte) 238,
        (byte) 199,
        (byte) 62,
        (byte) 85
      };
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[22]
      {
        (byte) 77,
        (byte) 251,
        (byte) 92,
        (byte) 202,
        (byte) 132,
        (byte) 39,
        (byte) 35,
        (byte) 204,
        (byte) 33,
        (byte) 132,
        (byte) 233,
        (byte) 149,
        (byte) 235,
        (byte) 44,
        (byte) 93,
        (byte) 243,
        (byte) 134,
        (byte) 18,
        (byte) 254,
        (byte) 16 /*0x10*/,
        (byte) 32 /*0x20*/,
        (byte) 189
      };
      byte[] numArray5 = new byte[22]
      {
        (byte) 151,
        (byte) 44,
        (byte) 150,
        (byte) 137,
        (byte) 196,
        (byte) 224 /*0xE0*/,
        (byte) 216,
        (byte) 73,
        (byte) 171,
        (byte) 134,
        (byte) 99,
        (byte) 254,
        (byte) 193,
        (byte) 248,
        (byte) 242,
        (byte) 9,
        (byte) 30,
        (byte) 111,
        (byte) 250,
        (byte) 113,
        (byte) 45,
        (byte) 140
      };
      key.Query(true, 359, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 22);
      for (int index = 0; index < 22; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[77];
    byte[] numArray7 = new byte[55]
    {
      (byte) 32 /*0x20*/,
      (byte) 160 /*0xA0*/,
      (byte) 252,
      (byte) 217,
      (byte) 121,
      (byte) 179,
      (byte) 109,
      (byte) 253,
      (byte) 71,
      (byte) 76,
      (byte) 119,
      (byte) 192 /*0xC0*/,
      (byte) 105,
      (byte) 29,
      (byte) 141,
      (byte) 250,
      (byte) 120,
      (byte) 11,
      (byte) 11,
      (byte) 183,
      (byte) 209,
      (byte) 157,
      (byte) 180,
      (byte) 224 /*0xE0*/,
      (byte) 94,
      (byte) 211,
      (byte) 223,
      (byte) 136,
      (byte) 61,
      (byte) 174,
      (byte) 144 /*0x90*/,
      (byte) 231,
      (byte) 58,
      (byte) 207,
      (byte) 100,
      (byte) 135,
      (byte) 38,
      (byte) 243,
      (byte) 168,
      (byte) 208 /*0xD0*/,
      (byte) 124,
      (byte) 151,
      (byte) 164,
      (byte) 224 /*0xE0*/,
      (byte) 230,
      (byte) 51,
      (byte) 29,
      (byte) 245,
      (byte) 112 /*0x70*/,
      (byte) 21,
      (byte) 167,
      (byte) 177,
      (byte) 8,
      (byte) 106,
      (byte) 41
    };
    byte[] numArray8 = new byte[55];
    numArray8[22] = (byte) 235;
    numArray8[37] = (byte) 86;
    numArray8[35] = (byte) 103;
    numArray8[2] = (byte) 172;
    numArray8[4] = (byte) 253;
    numArray8[24] = (byte) 40;
    numArray8[47] = (byte) 109;
    numArray8[6] = (byte) 146;
    numArray8[8] = (byte) 53;
    numArray8[9] = (byte) 221;
    numArray8[51] = (byte) 23;
    numArray8[31 /*0x1F*/] = (byte) 173;
    numArray8[3] = (byte) 54;
    numArray8[17] = (byte) 51;
    numArray8[14] = (byte) 114;
    numArray8[15] = (byte) 73;
    numArray8[30] = (byte) 176 /*0xB0*/;
    numArray8[36] = (byte) 205;
    numArray8[18] = (byte) 254;
    numArray8[1] = (byte) 21;
    numArray8[20] = (byte) 178;
    numArray8[21] = (byte) 248;
    numArray8[7] = (byte) 94;
    numArray8[39] = (byte) 39;
    numArray8[10] = (byte) 208 /*0xD0*/;
    numArray8[25] = (byte) 217;
    numArray8[26] = (byte) 124;
    numArray8[27] = (byte) 82;
    numArray8[28] = (byte) 230;
    numArray8[46] = (byte) 221;
    numArray8[5] = (byte) 234;
    numArray8[45] = (byte) 32 /*0x20*/;
    numArray8[32 /*0x20*/] = (byte) 241;
    numArray8[16 /*0x10*/] = (byte) 72;
    numArray8[34] = (byte) 137;
    numArray8[13] = (byte) 2;
    numArray8[52] = (byte) 117;
    numArray8[42] = (byte) 41;
    numArray8[38] = (byte) 241;
    numArray8[54] = (byte) 50;
    numArray8[40] = (byte) 56;
    numArray8[41] = (byte) 247;
    numArray8[23] = (byte) 73;
    numArray8[43] = (byte) 245;
    numArray8[44] = (byte) 138;
    numArray8[50] = (byte) 111;
    numArray8[29] = (byte) 89;
    numArray8[12] = (byte) 11;
    numArray8[48 /*0x30*/] = (byte) 10;
    numArray8[49] = (byte) 199;
    numArray8[11] = (byte) 112 /*0x70*/;
    numArray8[33] = (byte) 245;
    numArray8[19] = (byte) 127 /*0x7F*/;
    numArray8[53] = (byte) 254;
    numArray8[0] = (byte) 45;
    key.Query(true, 359, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[22]
    {
      (byte) 34,
      (byte) 79,
      (byte) 238,
      (byte) 233,
      (byte) 204,
      (byte) 153,
      (byte) 145,
      (byte) 221,
      (byte) 175,
      (byte) 183,
      (byte) 243,
      (byte) 214,
      (byte) 106,
      (byte) 201,
      (byte) 11,
      (byte) 97,
      (byte) 61,
      (byte) 163,
      (byte) 98,
      (byte) 236,
      (byte) 176 /*0xB0*/,
      (byte) 162
    };
    byte[] numArray10 = new byte[22]
    {
      (byte) 211,
      (byte) 232,
      (byte) 35,
      (byte) 187,
      (byte) 196,
      (byte) 150,
      (byte) 43,
      (byte) 114,
      (byte) 236,
      (byte) 177,
      (byte) 241,
      (byte) 28,
      (byte) 132,
      (byte) 130,
      (byte) 21,
      (byte) 230,
      (byte) 196,
      (byte) 20,
      (byte) 72,
      (byte) 26,
      (byte) 164,
      (byte) 218
    };
    key.Query(true, 359, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 22);
    for (int index = 0; index < 22; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }
}
