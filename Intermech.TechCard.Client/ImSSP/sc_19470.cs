// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_19470
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_19470
{
  internal static string ssp_techcard_19471()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19]
      {
        (byte) 70,
        (byte) 80 /*0x50*/,
        (byte) 92,
        (byte) 102,
        (byte) 125,
        (byte) 247,
        (byte) 43,
        (byte) 166,
        (byte) 253,
        (byte) 182,
        (byte) 113,
        (byte) 179,
        (byte) 190,
        (byte) 111,
        (byte) 229,
        (byte) 112 /*0x70*/,
        (byte) 247,
        (byte) 72,
        (byte) 5
      };
      byte[] numArray3 = new byte[19]
      {
        (byte) 213,
        (byte) 220,
        (byte) 9,
        (byte) 10,
        (byte) 147,
        (byte) 21,
        (byte) 144 /*0x90*/,
        (byte) 201,
        (byte) 81,
        (byte) 53,
        (byte) 189,
        (byte) 234,
        (byte) 242,
        (byte) 91,
        (byte) 98,
        (byte) 241,
        (byte) 172,
        (byte) 209,
        (byte) 92
      };
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[19];
    byte[] numArray5 = new byte[19]
    {
      (byte) 252,
      (byte) 126,
      (byte) 249,
      (byte) 143,
      (byte) 94,
      (byte) 86,
      (byte) 167,
      (byte) 96 /*0x60*/,
      (byte) 7,
      (byte) 37,
      (byte) 74,
      (byte) 252,
      (byte) 145,
      (byte) 49,
      (byte) 213,
      (byte) 120,
      (byte) 102,
      (byte) 146,
      (byte) 112 /*0x70*/
    };
    byte[] numArray6 = new byte[19];
    numArray6[10] = (byte) 110;
    numArray6[1] = (byte) 100;
    numArray6[14] = (byte) 238;
    numArray6[3] = (byte) 234;
    numArray6[4] = (byte) 125;
    numArray6[5] = (byte) 27;
    numArray6[17] = (byte) 159;
    numArray6[0] = (byte) 166;
    numArray6[8] = (byte) 111;
    numArray6[6] = (byte) 127 /*0x7F*/;
    numArray6[15] = (byte) 190;
    numArray6[11] = (byte) 242;
    numArray6[12] = (byte) 194;
    numArray6[2] = (byte) 122;
    numArray6[13] = (byte) 151;
    numArray6[18] = (byte) 145;
    numArray6[16 /*0x10*/] = (byte) 220;
    numArray6[7] = (byte) 237;
    numArray6[9] = (byte) 87;
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_techcard_19472()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19];
      numArray2[15] = (byte) 174;
      numArray2[9] = (byte) 114;
      numArray2[0] = (byte) 174;
      numArray2[3] = (byte) 193;
      numArray2[4] = (byte) 221;
      numArray2[14] = (byte) 98;
      numArray2[1] = (byte) 76;
      numArray2[7] = (byte) 127 /*0x7F*/;
      numArray2[18] = (byte) 86;
      numArray2[16 /*0x10*/] = (byte) 80 /*0x50*/;
      numArray2[10] = (byte) 73;
      numArray2[17] = (byte) 63 /*0x3F*/;
      numArray2[12] = (byte) 202;
      numArray2[13] = (byte) 45;
      numArray2[11] = (byte) 120;
      numArray2[2] = (byte) 104;
      numArray2[8] = (byte) 226;
      numArray2[5] = (byte) 179;
      numArray2[6] = (byte) 154;
      byte[] numArray3 = new byte[19];
      numArray3[3] = (byte) 224 /*0xE0*/;
      numArray3[7] = (byte) 254;
      numArray3[10] = (byte) 179;
      numArray3[4] = (byte) 179;
      numArray3[12] = (byte) 115;
      numArray3[5] = (byte) 254;
      numArray3[0] = (byte) 151;
      numArray3[14] = (byte) 112 /*0x70*/;
      numArray3[8] = (byte) 7;
      numArray3[15] = (byte) 166;
      numArray3[6] = (byte) 41;
      numArray3[11] = (byte) 75;
      numArray3[1] = (byte) 86;
      numArray3[13] = (byte) 176 /*0xB0*/;
      numArray3[9] = (byte) 239;
      numArray3[16 /*0x10*/] = (byte) 2;
      numArray3[2] = (byte) 125;
      numArray3[17] = (byte) 165;
      numArray3[18] = (byte) 44;
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[19];
    byte[] numArray5 = new byte[19];
    numArray5[12] = (byte) 253;
    numArray5[10] = (byte) 203;
    numArray5[17] = (byte) 99;
    numArray5[3] = (byte) 106;
    numArray5[14] = (byte) 190;
    numArray5[5] = (byte) 173;
    numArray5[13] = (byte) 9;
    numArray5[7] = (byte) 60;
    numArray5[8] = (byte) 12;
    numArray5[9] = (byte) 147;
    numArray5[4] = (byte) 120;
    numArray5[11] = (byte) 211;
    numArray5[2] = (byte) 115;
    numArray5[0] = (byte) 213;
    numArray5[1] = (byte) 162;
    numArray5[15] = (byte) 173;
    numArray5[16 /*0x10*/] = (byte) 182;
    numArray5[6] = (byte) 248;
    numArray5[18] = (byte) 188;
    byte[] numArray6 = new byte[19]
    {
      (byte) 118,
      (byte) 187,
      (byte) 237,
      (byte) 34,
      (byte) 75,
      (byte) 69,
      (byte) 6,
      (byte) 68,
      (byte) 241,
      (byte) 84,
      (byte) 56,
      (byte) 212,
      (byte) 174,
      (byte) 182,
      (byte) 41,
      (byte) 231,
      (byte) 195,
      (byte) 200,
      (byte) 165
    };
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_techcard_19473()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19];
      numArray2[10] = (byte) 76;
      numArray2[3] = (byte) 201;
      numArray2[16 /*0x10*/] = (byte) 144 /*0x90*/;
      numArray2[6] = (byte) 218;
      numArray2[4] = (byte) 92;
      numArray2[12] = (byte) 226;
      numArray2[13] = (byte) 30;
      numArray2[9] = (byte) 221;
      numArray2[1] = (byte) 231;
      numArray2[7] = (byte) 168;
      numArray2[8] = (byte) 137;
      numArray2[0] = (byte) 26;
      numArray2[18] = (byte) 143;
      numArray2[2] = (byte) 243;
      numArray2[14] = (byte) 37;
      numArray2[11] = (byte) 151;
      numArray2[15] = (byte) 38;
      numArray2[17] = (byte) 217;
      numArray2[5] = (byte) 116;
      byte[] numArray3 = new byte[19];
      numArray3[4] = (byte) 93;
      numArray3[1] = (byte) 221;
      numArray3[10] = (byte) 79;
      numArray3[3] = (byte) 46;
      numArray3[0] = (byte) 73;
      numArray3[2] = (byte) 115;
      numArray3[6] = (byte) 29;
      numArray3[8] = (byte) 237;
      numArray3[14] = (byte) 86;
      numArray3[9] = (byte) 162;
      numArray3[5] = (byte) 40;
      numArray3[11] = (byte) 234;
      numArray3[12] = (byte) 165;
      numArray3[13] = (byte) 4;
      numArray3[18] = (byte) 97;
      numArray3[15] = (byte) 135;
      numArray3[16 /*0x10*/] = (byte) 48 /*0x30*/;
      numArray3[17] = (byte) 253;
      numArray3[7] = (byte) 180;
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[19];
    byte[] numArray5 = new byte[19]
    {
      (byte) 225,
      (byte) 32 /*0x20*/,
      (byte) 33,
      (byte) 32 /*0x20*/,
      (byte) 168,
      (byte) 132,
      (byte) 248,
      (byte) 112 /*0x70*/,
      (byte) 129,
      (byte) 144 /*0x90*/,
      (byte) 204,
      (byte) 127 /*0x7F*/,
      (byte) 151,
      (byte) 230,
      (byte) 5,
      (byte) 116,
      (byte) 213,
      (byte) 85,
      (byte) 99
    };
    byte[] numArray6 = new byte[19]
    {
      (byte) 15,
      (byte) 224 /*0xE0*/,
      (byte) 85,
      (byte) 24,
      (byte) 132,
      (byte) 138,
      (byte) 95,
      (byte) 100,
      (byte) 219,
      (byte) 63 /*0x3F*/,
      (byte) 127 /*0x7F*/,
      (byte) 34,
      (byte) 59,
      (byte) 183,
      (byte) 126,
      (byte) 205,
      (byte) 163,
      (byte) 149,
      (byte) 98
    };
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
