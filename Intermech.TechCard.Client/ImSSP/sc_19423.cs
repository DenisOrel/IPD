// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_19423
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_19423
{
  private static byte[] sspq = new byte[10]
  {
    (byte) 76,
    (byte) 183,
    (byte) 42,
    (byte) 118,
    (byte) 152,
    (byte) 24,
    (byte) 45,
    (byte) 73,
    (byte) 38,
    (byte) 54
  };
  private static byte[] sspr = new byte[10]
  {
    (byte) 144 /*0x90*/,
    (byte) 53,
    (byte) 165,
    (byte) 218,
    (byte) 78,
    (byte) 190,
    (byte) 24,
    (byte) 127 /*0x7F*/,
    (byte) 70,
    (byte) 245
  };

  internal static string ssp_techcard_19424()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[18];
      byte[] numArray2 = new byte[18];
      numArray2[16 /*0x10*/] = (byte) 229;
      numArray2[5] = (byte) 61;
      numArray2[15] = (byte) 251;
      numArray2[3] = (byte) 13;
      numArray2[13] = (byte) 29;
      numArray2[0] = (byte) 199;
      numArray2[14] = (byte) 155;
      numArray2[7] = (byte) 56;
      numArray2[8] = (byte) 60;
      numArray2[9] = (byte) 234;
      numArray2[10] = (byte) 216;
      numArray2[11] = (byte) 40;
      numArray2[12] = (byte) 107;
      numArray2[1] = (byte) 233;
      numArray2[4] = (byte) 55;
      numArray2[6] = (byte) 51;
      numArray2[2] = (byte) 195;
      numArray2[17] = (byte) 33;
      byte[] numArray3 = new byte[18]
      {
        (byte) 37,
        (byte) 65,
        (byte) 188,
        (byte) 148,
        (byte) 251,
        (byte) 148,
        (byte) 134,
        (byte) 18,
        (byte) 35,
        (byte) 122,
        (byte) 236,
        (byte) 72,
        (byte) 53,
        (byte) 210,
        (byte) 65,
        (byte) 72,
        (byte) 7,
        (byte) 86
      };
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 18);
      for (int index = 0; index < 18; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[10];
      byte[] response = new byte[10];
      Array.Copy((Array) sc_19423.sspq, 0, (Array) numArray4, 0, 10);
      key.Query(true, 359, numArray4, response);
      Array.Copy((Array) sc_19423.sspr, 0, (Array) numArray4, 0, 10);
      for (int index = 0; index < numArray4.Length; ++index)
      {
        if ((int) numArray4[index] != (int) response[index])
        {
          key.TagValue = (int) response[index];
          break;
        }
      }
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray5 = new byte[18];
    byte[] numArray6 = new byte[18];
    numArray6[3] = (byte) 181;
    numArray6[1] = (byte) 191;
    numArray6[14] = (byte) 202;
    numArray6[11] = (byte) 88;
    numArray6[5] = (byte) 35;
    numArray6[7] = (byte) 170;
    numArray6[16 /*0x10*/] = (byte) 206;
    numArray6[6] = (byte) 73;
    numArray6[8] = (byte) 26;
    numArray6[9] = (byte) 27;
    numArray6[10] = (byte) 252;
    numArray6[4] = (byte) 97;
    numArray6[12] = (byte) 244;
    numArray6[2] = (byte) 192 /*0xC0*/;
    numArray6[17] = (byte) 122;
    numArray6[15] = (byte) 10;
    numArray6[0] = (byte) 166;
    numArray6[13] = (byte) 240 /*0xF0*/;
    byte[] numArray7 = new byte[18]
    {
      (byte) 133,
      (byte) 61,
      (byte) 245,
      (byte) 198,
      (byte) 109,
      (byte) 178,
      (byte) 190,
      (byte) 213,
      (byte) 211,
      (byte) 130,
      (byte) 26,
      (byte) 131,
      (byte) 136,
      (byte) 122,
      (byte) 49,
      (byte) 130,
      (byte) 69,
      (byte) 174
    };
    key.Query(true, 359, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 18);
    for (int index = 0; index < 18; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }
}
