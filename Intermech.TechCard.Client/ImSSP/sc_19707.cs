// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_19707
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_19707
{
  internal static string ssp_techcard_19708()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19]
      {
        (byte) 98,
        (byte) 246,
        (byte) 97,
        (byte) 16 /*0x10*/,
        (byte) 153,
        (byte) 93,
        (byte) 17,
        (byte) 10,
        (byte) 128 /*0x80*/,
        (byte) 73,
        (byte) 231,
        (byte) 145,
        (byte) 63 /*0x3F*/,
        (byte) 79,
        (byte) 153,
        (byte) 190,
        (byte) 0,
        (byte) 95,
        (byte) 125
      };
      byte[] numArray3 = new byte[19];
      numArray3[14] = (byte) 67;
      numArray3[11] = (byte) 218;
      numArray3[10] = (byte) 243;
      numArray3[18] = (byte) 42;
      numArray3[4] = (byte) 88;
      numArray3[5] = (byte) 198;
      numArray3[6] = (byte) 55;
      numArray3[8] = (byte) 38;
      numArray3[13] = (byte) 117;
      numArray3[2] = (byte) 197;
      numArray3[3] = (byte) 252;
      numArray3[16 /*0x10*/] = (byte) 82;
      numArray3[7] = (byte) 128 /*0x80*/;
      numArray3[1] = (byte) 6;
      numArray3[0] = (byte) 217;
      numArray3[15] = (byte) 211;
      numArray3[9] = (byte) 127 /*0x7F*/;
      numArray3[17] = (byte) 26;
      numArray3[12] = (byte) 160 /*0xA0*/;
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[19];
    byte[] numArray5 = new byte[19];
    numArray5[9] = (byte) 223;
    numArray5[0] = (byte) 212;
    numArray5[2] = (byte) 150;
    numArray5[17] = (byte) 75;
    numArray5[3] = (byte) 43;
    numArray5[6] = (byte) 196;
    numArray5[15] = (byte) 244;
    numArray5[7] = (byte) 51;
    numArray5[8] = (byte) 74;
    numArray5[12] = (byte) 120;
    numArray5[10] = (byte) 34;
    numArray5[11] = (byte) 35;
    numArray5[4] = (byte) 82;
    numArray5[13] = (byte) 155;
    numArray5[5] = (byte) 24;
    numArray5[1] = (byte) 11;
    numArray5[16 /*0x10*/] = (byte) 76;
    numArray5[14] = (byte) 188;
    numArray5[18] = (byte) 17;
    byte[] numArray6 = new byte[19];
    numArray6[17] = (byte) 103;
    numArray6[12] = (byte) 217;
    numArray6[0] = (byte) 186;
    numArray6[3] = (byte) 174;
    numArray6[4] = (byte) 56;
    numArray6[1] = (byte) 179;
    numArray6[6] = (byte) 19;
    numArray6[5] = (byte) 2;
    numArray6[8] = (byte) 25;
    numArray6[9] = (byte) 249;
    numArray6[14] = (byte) 176 /*0xB0*/;
    numArray6[13] = (byte) 164;
    numArray6[7] = (byte) 61;
    numArray6[10] = (byte) 98;
    numArray6[2] = (byte) 160 /*0xA0*/;
    numArray6[15] = (byte) 178;
    numArray6[11] = (byte) 22;
    numArray6[16 /*0x10*/] = (byte) 178;
    numArray6[18] = (byte) 125;
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_techcard_19709()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19];
      numArray2[18] = (byte) 135;
      numArray2[8] = (byte) 34;
      numArray2[2] = (byte) 232;
      numArray2[0] = (byte) 204;
      numArray2[12] = (byte) 161;
      numArray2[5] = (byte) 63 /*0x3F*/;
      numArray2[6] = (byte) 56;
      numArray2[11] = (byte) 216;
      numArray2[1] = (byte) 195;
      numArray2[9] = (byte) 138;
      numArray2[16 /*0x10*/] = (byte) 228;
      numArray2[7] = (byte) 27;
      numArray2[17] = (byte) 59;
      numArray2[3] = (byte) 229;
      numArray2[10] = (byte) 99;
      numArray2[15] = (byte) 246;
      numArray2[14] = (byte) 168;
      numArray2[4] = (byte) 75;
      numArray2[13] = (byte) 254;
      byte[] numArray3 = new byte[19];
      numArray3[14] = (byte) 250;
      numArray3[1] = (byte) 168;
      numArray3[8] = (byte) 215;
      numArray3[7] = (byte) 199;
      numArray3[9] = (byte) 103;
      numArray3[5] = (byte) 149;
      numArray3[6] = (byte) 165;
      numArray3[4] = (byte) 145;
      numArray3[2] = (byte) 171;
      numArray3[0] = (byte) 78;
      numArray3[10] = (byte) 232;
      numArray3[3] = (byte) 83;
      numArray3[12] = (byte) 54;
      numArray3[13] = (byte) 139;
      numArray3[11] = (byte) 202;
      numArray3[15] = (byte) 126;
      numArray3[16 /*0x10*/] = (byte) 238;
      numArray3[17] = (byte) 214;
      numArray3[18] = (byte) 5;
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[19];
    byte[] numArray5 = new byte[19]
    {
      (byte) 42,
      (byte) 74,
      (byte) 21,
      (byte) 82,
      (byte) 5,
      (byte) 128 /*0x80*/,
      (byte) 207,
      (byte) 10,
      (byte) 254,
      (byte) 208 /*0xD0*/,
      (byte) 122,
      (byte) 107,
      (byte) 17,
      (byte) 40,
      (byte) 195,
      (byte) 147,
      (byte) 229,
      (byte) 145,
      (byte) 168
    };
    byte[] numArray6 = new byte[19]
    {
      (byte) 188,
      (byte) 104,
      (byte) 234,
      (byte) 12,
      (byte) 108,
      (byte) 52,
      (byte) 52,
      (byte) 90,
      (byte) 64 /*0x40*/,
      (byte) 133,
      (byte) 108,
      (byte) 95,
      (byte) 8,
      (byte) 9,
      (byte) 233,
      (byte) 43,
      (byte) 93,
      (byte) 110,
      (byte) 3
    };
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
