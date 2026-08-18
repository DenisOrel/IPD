// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_19541
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_19541
{
  internal static string ssp_techcard_19542()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19];
      numArray2[12] = (byte) 156;
      numArray2[1] = (byte) 41;
      numArray2[5] = (byte) 172;
      numArray2[7] = (byte) 91;
      numArray2[6] = (byte) 161;
      numArray2[16 /*0x10*/] = (byte) 76;
      numArray2[8] = (byte) 75;
      numArray2[15] = (byte) 6;
      numArray2[3] = (byte) 131;
      numArray2[9] = (byte) 206;
      numArray2[18] = (byte) 243;
      numArray2[11] = (byte) 127 /*0x7F*/;
      numArray2[0] = (byte) 238;
      numArray2[4] = (byte) 97;
      numArray2[14] = (byte) 236;
      numArray2[2] = (byte) 64 /*0x40*/;
      numArray2[10] = (byte) 39;
      numArray2[13] = (byte) 98;
      numArray2[17] = (byte) 214;
      byte[] numArray3 = new byte[19]
      {
        (byte) 74,
        (byte) 115,
        (byte) 74,
        (byte) 227,
        (byte) 218,
        (byte) 211,
        (byte) 159,
        (byte) 173,
        (byte) 26,
        (byte) 7,
        (byte) 82,
        (byte) 15,
        (byte) 102,
        (byte) 165,
        (byte) 93,
        (byte) 157,
        (byte) 69,
        (byte) 76,
        (byte) 62
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
      (byte) 152,
      (byte) 18,
      (byte) 138,
      (byte) 67,
      (byte) 145,
      (byte) 217,
      (byte) 82,
      (byte) 250,
      (byte) 62,
      (byte) 173,
      (byte) 193,
      (byte) 213,
      (byte) 71,
      (byte) 243,
      (byte) 236,
      (byte) 170,
      (byte) 22,
      (byte) 179,
      (byte) 63 /*0x3F*/
    };
    byte[] numArray6 = new byte[19]
    {
      (byte) 14,
      (byte) 119,
      (byte) 211,
      (byte) 162,
      (byte) 178,
      (byte) 151,
      (byte) 250,
      (byte) 73,
      (byte) 38,
      (byte) 196,
      (byte) 85,
      (byte) 128 /*0x80*/,
      (byte) 54,
      (byte) 204,
      (byte) 169,
      (byte) 214,
      (byte) 99,
      (byte) 4,
      (byte) 85
    };
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_techcard_19543()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19]
      {
        (byte) 189,
        (byte) 93,
        (byte) 111,
        (byte) 164,
        (byte) 92,
        (byte) 95,
        (byte) 148,
        (byte) 62,
        (byte) 235,
        (byte) 136,
        (byte) 69,
        (byte) 53,
        (byte) 239,
        (byte) 244,
        (byte) 229,
        (byte) 62,
        (byte) 72,
        (byte) 44,
        (byte) 173
      };
      byte[] numArray3 = new byte[19];
      numArray3[3] = (byte) 92;
      numArray3[13] = (byte) 114;
      numArray3[1] = (byte) 57;
      numArray3[9] = (byte) 113;
      numArray3[4] = (byte) 16 /*0x10*/;
      numArray3[5] = (byte) 49;
      numArray3[0] = (byte) 31 /*0x1F*/;
      numArray3[6] = (byte) 3;
      numArray3[8] = (byte) 153;
      numArray3[15] = (byte) 181;
      numArray3[10] = (byte) 82;
      numArray3[11] = (byte) 73;
      numArray3[12] = (byte) 212;
      numArray3[7] = (byte) 109;
      numArray3[14] = (byte) 24;
      numArray3[2] = (byte) 51;
      numArray3[16 /*0x10*/] = (byte) 254;
      numArray3[17] = (byte) 177;
      numArray3[18] = (byte) 32 /*0x20*/;
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[19];
    byte[] numArray5 = new byte[19]
    {
      (byte) 111,
      (byte) 213,
      (byte) 77,
      (byte) 2,
      (byte) 189,
      (byte) 253,
      (byte) 181,
      (byte) 49,
      (byte) 116,
      (byte) 246,
      (byte) 0,
      (byte) 77,
      (byte) 10,
      (byte) 128 /*0x80*/,
      (byte) 109,
      (byte) 104,
      (byte) 167,
      (byte) 159,
      (byte) 66
    };
    byte[] numArray6 = new byte[19];
    numArray6[4] = (byte) 74;
    numArray6[3] = (byte) 214;
    numArray6[18] = (byte) 155;
    numArray6[1] = (byte) 55;
    numArray6[5] = (byte) 55;
    numArray6[12] = (byte) 249;
    numArray6[2] = (byte) 233;
    numArray6[7] = (byte) 40;
    numArray6[8] = (byte) 235;
    numArray6[16 /*0x10*/] = (byte) 163;
    numArray6[9] = (byte) 7;
    numArray6[11] = (byte) 98;
    numArray6[6] = (byte) 145;
    numArray6[14] = (byte) 18;
    numArray6[13] = (byte) 130;
    numArray6[15] = (byte) 107;
    numArray6[0] = (byte) 139;
    numArray6[17] = (byte) 243;
    numArray6[10] = (byte) 213;
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
