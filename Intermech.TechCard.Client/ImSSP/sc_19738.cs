// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_19738
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_19738
{
  internal static string ssp_techcard_19739()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19];
      numArray2[13] = (byte) 190;
      numArray2[0] = (byte) 140;
      numArray2[2] = (byte) 80 /*0x50*/;
      numArray2[7] = (byte) 69;
      numArray2[14] = (byte) 201;
      numArray2[5] = (byte) 62;
      numArray2[6] = (byte) 75;
      numArray2[4] = (byte) 207;
      numArray2[8] = (byte) 205;
      numArray2[9] = (byte) 51;
      numArray2[3] = (byte) 100;
      numArray2[11] = (byte) 100;
      numArray2[17] = (byte) 180;
      numArray2[10] = (byte) 225;
      numArray2[1] = (byte) 191;
      numArray2[18] = (byte) 201;
      numArray2[16 /*0x10*/] = (byte) 7;
      numArray2[12] = (byte) 34;
      numArray2[15] = (byte) 124;
      byte[] numArray3 = new byte[19]
      {
        (byte) 23,
        (byte) 119,
        (byte) 164,
        (byte) 173,
        (byte) 72,
        (byte) 199,
        (byte) 241,
        (byte) 174,
        (byte) 213,
        (byte) 118,
        (byte) 119,
        (byte) 209,
        (byte) 193,
        (byte) 156,
        (byte) 139,
        (byte) 153,
        (byte) 30,
        (byte) 192 /*0xC0*/,
        (byte) 141
      };
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[19];
    byte[] numArray5 = new byte[19];
    numArray5[4] = (byte) 195;
    numArray5[6] = (byte) 15;
    numArray5[1] = (byte) 167;
    numArray5[3] = (byte) 48 /*0x30*/;
    numArray5[14] = (byte) 160 /*0xA0*/;
    numArray5[12] = (byte) 218;
    numArray5[18] = (byte) 189;
    numArray5[0] = (byte) 85;
    numArray5[2] = (byte) 181;
    numArray5[8] = (byte) 10;
    numArray5[10] = (byte) 60;
    numArray5[17] = (byte) 194;
    numArray5[13] = (byte) 94;
    numArray5[5] = (byte) 53;
    numArray5[11] = (byte) 123;
    numArray5[15] = (byte) 52;
    numArray5[16 /*0x10*/] = (byte) 235;
    numArray5[9] = (byte) 11;
    numArray5[7] = (byte) 69;
    byte[] numArray6 = new byte[19];
    numArray6[12] = (byte) 191;
    numArray6[1] = (byte) 96 /*0x60*/;
    numArray6[2] = (byte) 230;
    numArray6[3] = (byte) 28;
    numArray6[4] = (byte) 90;
    numArray6[5] = (byte) 101;
    numArray6[9] = (byte) 3;
    numArray6[0] = (byte) 24;
    numArray6[8] = (byte) 46;
    numArray6[7] = (byte) 48 /*0x30*/;
    numArray6[18] = (byte) 220;
    numArray6[13] = (byte) 22;
    numArray6[14] = (byte) 194;
    numArray6[11] = (byte) 222;
    numArray6[6] = (byte) 254;
    numArray6[15] = (byte) 153;
    numArray6[16 /*0x10*/] = (byte) 162;
    numArray6[17] = (byte) 222;
    numArray6[10] = (byte) 6;
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
