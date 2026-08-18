// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_19556
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_19556
{
  internal static string ssp_techcard_19557()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19];
      numArray2[3] = (byte) 101;
      numArray2[5] = (byte) 214;
      numArray2[2] = (byte) 145;
      numArray2[13] = (byte) 134;
      numArray2[14] = (byte) 181;
      numArray2[10] = (byte) 198;
      numArray2[6] = (byte) 149;
      numArray2[15] = (byte) 145;
      numArray2[17] = (byte) 29;
      numArray2[9] = (byte) 238;
      numArray2[4] = (byte) 70;
      numArray2[11] = (byte) 19;
      numArray2[12] = (byte) 29;
      numArray2[0] = (byte) 76;
      numArray2[18] = (byte) 162;
      numArray2[1] = (byte) 100;
      numArray2[16 /*0x10*/] = (byte) 24;
      numArray2[7] = byte.MaxValue;
      numArray2[8] = (byte) 152;
      byte[] numArray3 = new byte[19]
      {
        (byte) 60,
        (byte) 47,
        (byte) 198,
        (byte) 124,
        (byte) 173,
        (byte) 25,
        (byte) 89,
        (byte) 206,
        (byte) 135,
        (byte) 69,
        (byte) 47,
        (byte) 114,
        (byte) 128 /*0x80*/,
        (byte) 53,
        (byte) 4,
        (byte) 153,
        (byte) 110,
        (byte) 113,
        (byte) 219
      };
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[19];
    byte[] numArray5 = new byte[19];
    numArray5[9] = (byte) 220;
    numArray5[11] = (byte) 151;
    numArray5[13] = (byte) 241;
    numArray5[6] = (byte) 95;
    numArray5[4] = (byte) 124;
    numArray5[2] = (byte) 121;
    numArray5[12] = (byte) 201;
    numArray5[7] = (byte) 172;
    numArray5[8] = (byte) 56;
    numArray5[15] = (byte) 69;
    numArray5[10] = (byte) 59;
    numArray5[16 /*0x10*/] = (byte) 152;
    numArray5[5] = (byte) 31 /*0x1F*/;
    numArray5[17] = (byte) 7;
    numArray5[14] = (byte) 69;
    numArray5[0] = (byte) 106;
    numArray5[3] = (byte) 63 /*0x3F*/;
    numArray5[1] = (byte) 217;
    numArray5[18] = (byte) 44;
    byte[] numArray6 = new byte[19]
    {
      (byte) 201,
      (byte) 72,
      (byte) 133,
      (byte) 247,
      (byte) 125,
      (byte) 37,
      (byte) 148,
      (byte) 230,
      (byte) 165,
      (byte) 81,
      (byte) 59,
      (byte) 49,
      (byte) 168,
      (byte) 113,
      (byte) 163,
      (byte) 212,
      (byte) 2,
      (byte) 253,
      (byte) 5
    };
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
