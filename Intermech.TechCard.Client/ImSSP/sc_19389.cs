// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_19389
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_19389
{
  internal static string ssp_techcard_19390()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19]
      {
        (byte) 20,
        (byte) 99,
        (byte) 17,
        (byte) 119,
        (byte) 93,
        (byte) 103,
        (byte) 201,
        (byte) 148,
        (byte) 147,
        (byte) 108,
        (byte) 76,
        (byte) 219,
        (byte) 186,
        (byte) 90,
        (byte) 131,
        (byte) 84,
        (byte) 138,
        (byte) 89,
        (byte) 183
      };
      byte[] numArray3 = new byte[19];
      numArray3[6] = (byte) 35;
      numArray3[1] = (byte) 81;
      numArray3[2] = (byte) 72;
      numArray3[3] = (byte) 137;
      numArray3[9] = (byte) 229;
      numArray3[4] = (byte) 61;
      numArray3[12] = (byte) 251;
      numArray3[7] = (byte) 138;
      numArray3[14] = (byte) 155;
      numArray3[10] = (byte) 57;
      numArray3[11] = (byte) 67;
      numArray3[0] = (byte) 179;
      numArray3[5] = (byte) 193;
      numArray3[13] = (byte) 71;
      numArray3[8] = (byte) 43;
      numArray3[15] = (byte) 211;
      numArray3[16 /*0x10*/] = (byte) 190;
      numArray3[17] = (byte) 83;
      numArray3[18] = (byte) 211;
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[19];
    byte[] numArray5 = new byte[19];
    numArray5[12] = (byte) 240 /*0xF0*/;
    numArray5[7] = (byte) 111;
    numArray5[9] = (byte) 72;
    numArray5[16 /*0x10*/] = (byte) 156;
    numArray5[4] = (byte) 23;
    numArray5[5] = (byte) 228;
    numArray5[6] = (byte) 248;
    numArray5[2] = (byte) 173;
    numArray5[3] = (byte) 115;
    numArray5[1] = (byte) 150;
    numArray5[10] = (byte) 111;
    numArray5[11] = (byte) 84;
    numArray5[8] = (byte) 81;
    numArray5[13] = (byte) 141;
    numArray5[14] = (byte) 119;
    numArray5[15] = (byte) 25;
    numArray5[18] = (byte) 105;
    numArray5[17] = (byte) 155;
    numArray5[0] = (byte) 9;
    byte[] numArray6 = new byte[19]
    {
      (byte) 202,
      (byte) 218,
      (byte) 198,
      (byte) 13,
      (byte) 195,
      (byte) 185,
      (byte) 184,
      (byte) 9,
      (byte) 98,
      (byte) 94,
      (byte) 102,
      (byte) 190,
      (byte) 254,
      (byte) 102,
      (byte) 83,
      (byte) 183,
      (byte) 19,
      (byte) 93,
      (byte) 103
    };
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
