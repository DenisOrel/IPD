// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_19718
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_19718
{
  private static byte[] sspq = new byte[39]
  {
    (byte) 2,
    (byte) 179,
    (byte) 25,
    (byte) 214,
    (byte) 44,
    (byte) 80 /*0x50*/,
    (byte) 40,
    (byte) 116,
    (byte) 104,
    (byte) 191,
    (byte) 52,
    (byte) 83,
    (byte) 47,
    (byte) 14,
    (byte) 83,
    (byte) 239,
    (byte) 68,
    (byte) 231,
    (byte) 157,
    (byte) 235,
    (byte) 16 /*0x10*/,
    (byte) 66,
    (byte) 7,
    (byte) 102,
    (byte) 139,
    (byte) 234,
    (byte) 103,
    (byte) 52,
    (byte) 97,
    (byte) 241,
    (byte) 12,
    (byte) 47,
    (byte) 167,
    (byte) 247,
    (byte) 32 /*0x20*/,
    (byte) 32 /*0x20*/,
    (byte) 40,
    (byte) 21,
    (byte) 173
  };
  private static byte[] sspr = new byte[39]
  {
    (byte) 53,
    (byte) 49,
    (byte) 97,
    (byte) 212,
    (byte) 227,
    (byte) 38,
    (byte) 87,
    (byte) 116,
    (byte) 76,
    (byte) 21,
    (byte) 28,
    (byte) 194,
    (byte) 98,
    (byte) 9,
    (byte) 140,
    (byte) 131,
    (byte) 124,
    (byte) 213,
    (byte) 35,
    (byte) 146,
    (byte) 221,
    (byte) 64 /*0x40*/,
    (byte) 188,
    (byte) 89,
    (byte) 1,
    (byte) 142,
    (byte) 228,
    (byte) 234,
    (byte) 80 /*0x50*/,
    (byte) 236,
    (byte) 107,
    (byte) 173,
    (byte) 24,
    (byte) 252,
    (byte) 61,
    (byte) 190,
    (byte) 62,
    (byte) 165,
    (byte) 188
  };

  internal static string ssp_techcard_19719()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19];
      numArray2[12] = (byte) 173;
      numArray2[1] = (byte) 235;
      numArray2[14] = (byte) 37;
      numArray2[15] = (byte) 165;
      numArray2[4] = (byte) 91;
      numArray2[10] = (byte) 60;
      numArray2[6] = (byte) 145;
      numArray2[16 /*0x10*/] = (byte) 42;
      numArray2[18] = (byte) 203;
      numArray2[9] = (byte) 208 /*0xD0*/;
      numArray2[7] = (byte) 80 /*0x50*/;
      numArray2[11] = (byte) 52;
      numArray2[13] = (byte) 50;
      numArray2[17] = (byte) 20;
      numArray2[3] = (byte) 100;
      numArray2[5] = (byte) 153;
      numArray2[2] = (byte) 219;
      numArray2[0] = (byte) 131;
      numArray2[8] = (byte) 231;
      byte[] numArray3 = new byte[19];
      numArray3[7] = (byte) 90;
      numArray3[0] = (byte) 13;
      numArray3[15] = (byte) 252;
      numArray3[13] = (byte) 93;
      numArray3[2] = (byte) 111;
      numArray3[18] = (byte) 58;
      numArray3[1] = (byte) 1;
      numArray3[9] = (byte) 6;
      numArray3[8] = (byte) 45;
      numArray3[11] = (byte) 190;
      numArray3[10] = (byte) 85;
      numArray3[4] = (byte) 10;
      numArray3[12] = (byte) 97;
      numArray3[3] = (byte) 76;
      numArray3[14] = (byte) 229;
      numArray3[16 /*0x10*/] = (byte) 81;
      numArray3[6] = (byte) 214;
      numArray3[17] = (byte) 12;
      numArray3[5] = (byte) 230;
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[19];
    byte[] numArray5 = new byte[19];
    numArray5[8] = (byte) 161;
    numArray5[2] = (byte) 90;
    numArray5[6] = (byte) 80 /*0x50*/;
    numArray5[0] = (byte) 48 /*0x30*/;
    numArray5[17] = (byte) 222;
    numArray5[10] = (byte) 123;
    numArray5[3] = (byte) 250;
    numArray5[7] = (byte) 168;
    numArray5[18] = (byte) 196;
    numArray5[12] = (byte) 161;
    numArray5[4] = (byte) 130;
    numArray5[11] = (byte) 14;
    numArray5[16 /*0x10*/] = (byte) 116;
    numArray5[9] = (byte) 191;
    numArray5[1] = (byte) 7;
    numArray5[5] = (byte) 216;
    numArray5[14] = (byte) 99;
    numArray5[13] = (byte) 90;
    numArray5[15] = (byte) 88;
    byte[] numArray6 = new byte[19];
    numArray6[14] = (byte) 148;
    numArray6[1] = (byte) 76;
    numArray6[7] = (byte) 216;
    numArray6[3] = (byte) 183;
    numArray6[4] = (byte) 151;
    numArray6[2] = (byte) 234;
    numArray6[6] = (byte) 206;
    numArray6[18] = (byte) 224 /*0xE0*/;
    numArray6[8] = (byte) 251;
    numArray6[9] = (byte) 223;
    numArray6[10] = (byte) 34;
    numArray6[0] = (byte) 44;
    numArray6[12] = (byte) 112 /*0x70*/;
    numArray6[13] = (byte) 207;
    numArray6[16 /*0x10*/] = (byte) 191;
    numArray6[15] = (byte) 131;
    numArray6[5] = (byte) 168;
    numArray6[17] = (byte) 201;
    numArray6[11] = (byte) 19;
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_techcard_19720()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19];
      numArray2[13] = (byte) 191;
      numArray2[1] = (byte) 14;
      numArray2[0] = (byte) 225;
      numArray2[3] = (byte) 202;
      numArray2[17] = (byte) 153;
      numArray2[5] = (byte) 1;
      numArray2[6] = (byte) 159;
      numArray2[7] = (byte) 26;
      numArray2[14] = (byte) 102;
      numArray2[11] = (byte) 198;
      numArray2[10] = (byte) 219;
      numArray2[8] = (byte) 253;
      numArray2[4] = (byte) 147;
      numArray2[9] = (byte) 50;
      numArray2[12] = (byte) 200;
      numArray2[2] = (byte) 139;
      numArray2[16 /*0x10*/] = (byte) 202;
      numArray2[15] = (byte) 72;
      numArray2[18] = (byte) 0;
      byte[] numArray3 = new byte[19]
      {
        (byte) 118,
        (byte) 180,
        (byte) 224 /*0xE0*/,
        (byte) 49,
        (byte) 120,
        (byte) 136,
        (byte) 8,
        (byte) 157,
        (byte) 104,
        (byte) 103,
        (byte) 7,
        (byte) 29,
        (byte) 188,
        (byte) 117,
        (byte) 30,
        (byte) 207,
        (byte) 102,
        (byte) 30,
        (byte) 81
      };
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[19];
    byte[] numArray5 = new byte[19];
    numArray5[3] = (byte) 43;
    numArray5[1] = (byte) 19;
    numArray5[14] = (byte) 132;
    numArray5[11] = (byte) 138;
    numArray5[10] = (byte) 182;
    numArray5[5] = (byte) 180;
    numArray5[4] = (byte) 204;
    numArray5[7] = (byte) 53;
    numArray5[8] = (byte) 224 /*0xE0*/;
    numArray5[2] = (byte) 224 /*0xE0*/;
    numArray5[13] = (byte) 234;
    numArray5[12] = (byte) 23;
    numArray5[15] = (byte) 136;
    numArray5[0] = (byte) 71;
    numArray5[9] = (byte) 114;
    numArray5[6] = (byte) 235;
    numArray5[16 /*0x10*/] = (byte) 124;
    numArray5[17] = (byte) 103;
    numArray5[18] = (byte) 40;
    byte[] numArray6 = new byte[19];
    numArray6[12] = (byte) 195;
    numArray6[16 /*0x10*/] = (byte) 45;
    numArray6[2] = (byte) 9;
    numArray6[14] = (byte) 247;
    numArray6[8] = (byte) 160 /*0xA0*/;
    numArray6[11] = (byte) 143;
    numArray6[6] = (byte) 147;
    numArray6[7] = (byte) 240 /*0xF0*/;
    numArray6[5] = (byte) 47;
    numArray6[9] = (byte) 181;
    numArray6[10] = (byte) 174;
    numArray6[1] = (byte) 170;
    numArray6[18] = (byte) 73;
    numArray6[17] = (byte) 101;
    numArray6[3] = (byte) 76;
    numArray6[13] = (byte) 206;
    numArray6[15] = (byte) 63 /*0x3F*/;
    numArray6[4] = (byte) 75;
    numArray6[0] = (byte) 91;
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_techcard_19721()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19];
      numArray2[16 /*0x10*/] = (byte) 31 /*0x1F*/;
      numArray2[14] = (byte) 11;
      numArray2[10] = (byte) 153;
      numArray2[15] = (byte) 203;
      numArray2[4] = (byte) 251;
      numArray2[0] = (byte) 236;
      numArray2[6] = (byte) 186;
      numArray2[7] = (byte) 235;
      numArray2[8] = (byte) 59;
      numArray2[17] = (byte) 61;
      numArray2[1] = (byte) 46;
      numArray2[5] = (byte) 247;
      numArray2[12] = (byte) 114;
      numArray2[9] = (byte) 227;
      numArray2[2] = (byte) 204;
      numArray2[13] = (byte) 41;
      numArray2[3] = (byte) 223;
      numArray2[11] = (byte) 208 /*0xD0*/;
      numArray2[18] = (byte) 75;
      byte[] numArray3 = new byte[19];
      numArray3[5] = (byte) 82;
      numArray3[10] = (byte) 96 /*0x60*/;
      numArray3[0] = (byte) 148;
      numArray3[4] = (byte) 153;
      numArray3[3] = (byte) 181;
      numArray3[2] = (byte) 89;
      numArray3[6] = (byte) 47;
      numArray3[7] = (byte) 86;
      numArray3[8] = (byte) 55;
      numArray3[9] = (byte) 83;
      numArray3[16 /*0x10*/] = (byte) 183;
      numArray3[11] = (byte) 167;
      numArray3[14] = (byte) 217;
      numArray3[13] = (byte) 46;
      numArray3[1] = (byte) 235;
      numArray3[18] = (byte) 28;
      numArray3[12] = (byte) 76;
      numArray3[17] = (byte) 83;
      numArray3[15] = (byte) 132;
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[19];
    byte[] numArray5 = new byte[19]
    {
      (byte) 49,
      (byte) 39,
      (byte) 48 /*0x30*/,
      (byte) 48 /*0x30*/,
      (byte) 194,
      (byte) 18,
      (byte) 221,
      (byte) 116,
      (byte) 160 /*0xA0*/,
      (byte) 11,
      (byte) 96 /*0x60*/,
      (byte) 72,
      (byte) 25,
      (byte) 192 /*0xC0*/,
      (byte) 215,
      (byte) 38,
      (byte) 193,
      (byte) 61,
      (byte) 60
    };
    byte[] numArray6 = new byte[19];
    numArray6[15] = (byte) 107;
    numArray6[1] = (byte) 50;
    numArray6[0] = (byte) 138;
    numArray6[3] = (byte) 6;
    numArray6[12] = (byte) 35;
    numArray6[4] = (byte) 231;
    numArray6[6] = (byte) 65;
    numArray6[7] = (byte) 164;
    numArray6[18] = (byte) 137;
    numArray6[8] = (byte) 161;
    numArray6[13] = (byte) 166;
    numArray6[11] = (byte) 209;
    numArray6[17] = (byte) 141;
    numArray6[2] = (byte) 194;
    numArray6[14] = (byte) 253;
    numArray6[9] = (byte) 35;
    numArray6[16 /*0x10*/] = (byte) 16 /*0x10*/;
    numArray6[10] = (byte) 128 /*0x80*/;
    numArray6[5] = (byte) 18;
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[39];
    byte[] response = new byte[39];
    Array.Copy((Array) sc_19718.sspq, 0, (Array) numArray7, 0, 39);
    key.Query(true, 359, numArray7, response);
    Array.Copy((Array) sc_19718.sspr, 0, (Array) numArray7, 0, 39);
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
}
