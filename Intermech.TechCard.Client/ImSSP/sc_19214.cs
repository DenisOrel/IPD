// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_19214
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_19214
{
  internal static string ssp_techcard_19215()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[18];
      byte[] numArray2 = new byte[18]
      {
        (byte) 112 /*0x70*/,
        (byte) 222,
        (byte) 124,
        (byte) 75,
        (byte) 125,
        (byte) 69,
        (byte) 243,
        (byte) 215,
        (byte) 50,
        (byte) 245,
        (byte) 26,
        (byte) 253,
        (byte) 57,
        (byte) 178,
        (byte) 166,
        (byte) 87,
        (byte) 162,
        (byte) 173
      };
      byte[] numArray3 = new byte[18];
      numArray3[1] = (byte) 246;
      numArray3[2] = (byte) 2;
      numArray3[8] = (byte) 238;
      numArray3[3] = (byte) 138;
      numArray3[4] = (byte) 220;
      numArray3[5] = (byte) 164;
      numArray3[9] = (byte) 186;
      numArray3[14] = (byte) 151;
      numArray3[11] = (byte) 58;
      numArray3[10] = (byte) 157;
      numArray3[0] = (byte) 116;
      numArray3[13] = (byte) 91;
      numArray3[12] = (byte) 93;
      numArray3[6] = (byte) 233;
      numArray3[16 /*0x10*/] = (byte) 173;
      numArray3[15] = byte.MaxValue;
      numArray3[7] = (byte) 4;
      numArray3[17] = (byte) 121;
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 18);
      for (int index = 0; index < 18; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[18];
    byte[] numArray5 = new byte[18]
    {
      (byte) 171,
      (byte) 89,
      (byte) 40,
      (byte) 152,
      (byte) 122,
      (byte) 52,
      (byte) 87,
      (byte) 91,
      (byte) 95,
      (byte) 183,
      (byte) 175,
      (byte) 97,
      (byte) 174,
      (byte) 147,
      (byte) 31 /*0x1F*/,
      (byte) 53,
      (byte) 19,
      (byte) 9
    };
    byte[] numArray6 = new byte[18]
    {
      (byte) 186,
      (byte) 223,
      (byte) 118,
      (byte) 142,
      (byte) 89,
      (byte) 103,
      (byte) 89,
      (byte) 139,
      (byte) 160 /*0xA0*/,
      (byte) 132,
      (byte) 94,
      (byte) 254,
      (byte) 92,
      (byte) 102,
      (byte) 214,
      (byte) 41,
      (byte) 150,
      (byte) 221
    };
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 18);
    for (int index = 0; index < 18; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_techcard_19216()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[18];
      byte[] numArray2 = new byte[18]
      {
        (byte) 80 /*0x50*/,
        (byte) 169,
        (byte) 45,
        (byte) 31 /*0x1F*/,
        (byte) 231,
        (byte) 21,
        (byte) 178,
        (byte) 164,
        (byte) 101,
        (byte) 123,
        (byte) 168,
        (byte) 170,
        (byte) 209,
        (byte) 18,
        (byte) 11,
        (byte) 196,
        (byte) 90,
        (byte) 149
      };
      byte[] numArray3 = new byte[18]
      {
        (byte) 236,
        (byte) 38,
        (byte) 242,
        (byte) 93,
        (byte) 41,
        (byte) 224 /*0xE0*/,
        (byte) 118,
        (byte) 166,
        (byte) 115,
        (byte) 115,
        (byte) 125,
        (byte) 31 /*0x1F*/,
        (byte) 119,
        (byte) 93,
        (byte) 162,
        (byte) 59,
        (byte) 68,
        (byte) 51
      };
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 18);
      for (int index = 0; index < 18; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[18];
    byte[] numArray5 = new byte[18];
    numArray5[17] = (byte) 76;
    numArray5[1] = (byte) 126;
    numArray5[9] = (byte) 40;
    numArray5[4] = (byte) 159;
    numArray5[7] = (byte) 250;
    numArray5[12] = (byte) 58;
    numArray5[6] = (byte) 81;
    numArray5[14] = (byte) 126;
    numArray5[11] = (byte) 158;
    numArray5[13] = (byte) 9;
    numArray5[2] = (byte) 163;
    numArray5[8] = (byte) 54;
    numArray5[3] = (byte) 39;
    numArray5[0] = (byte) 235;
    numArray5[5] = (byte) 199;
    numArray5[15] = (byte) 106;
    numArray5[16 /*0x10*/] = (byte) 90;
    numArray5[10] = (byte) 170;
    byte[] numArray6 = new byte[18];
    numArray6[17] = (byte) 243;
    numArray6[1] = (byte) 211;
    numArray6[9] = (byte) 218;
    numArray6[3] = (byte) 172;
    numArray6[4] = (byte) 8;
    numArray6[15] = (byte) 165;
    numArray6[6] = (byte) 240 /*0xF0*/;
    numArray6[7] = (byte) 118;
    numArray6[8] = (byte) 34;
    numArray6[13] = (byte) 212;
    numArray6[10] = (byte) 38;
    numArray6[11] = (byte) 130;
    numArray6[12] = (byte) 117;
    numArray6[16 /*0x10*/] = (byte) 64 /*0x40*/;
    numArray6[14] = (byte) 161;
    numArray6[0] = (byte) 222;
    numArray6[5] = (byte) 171;
    numArray6[2] = (byte) 25;
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 18);
    for (int index = 0; index < 18; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
