// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_19396
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_19396
{
  internal static string ssp_techcard_19397()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[18];
      byte[] numArray2 = new byte[18];
      numArray2[10] = (byte) 158;
      numArray2[15] = (byte) 114;
      numArray2[12] = (byte) 75;
      numArray2[5] = (byte) 227;
      numArray2[2] = (byte) 244;
      numArray2[6] = (byte) 105;
      numArray2[14] = (byte) 79;
      numArray2[0] = (byte) 58;
      numArray2[8] = (byte) 189;
      numArray2[9] = (byte) 79;
      numArray2[7] = (byte) 153;
      numArray2[11] = (byte) 121;
      numArray2[3] = (byte) 51;
      numArray2[13] = (byte) 50;
      numArray2[1] = (byte) 202;
      numArray2[4] = (byte) 89;
      numArray2[16 /*0x10*/] = (byte) 230;
      numArray2[17] = (byte) 205;
      byte[] numArray3 = new byte[18]
      {
        (byte) 97,
        (byte) 92,
        (byte) 133,
        (byte) 44,
        (byte) 31 /*0x1F*/,
        (byte) 62,
        (byte) 87,
        (byte) 42,
        (byte) 244,
        (byte) 15,
        (byte) 150,
        (byte) 159,
        (byte) 86,
        (byte) 28,
        (byte) 204,
        (byte) 199,
        (byte) 71,
        (byte) 68
      };
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 18);
      for (int index = 0; index < 18; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[18];
    byte[] numArray5 = new byte[18];
    numArray5[1] = (byte) 13;
    numArray5[8] = (byte) 138;
    numArray5[2] = (byte) 206;
    numArray5[3] = (byte) 53;
    numArray5[4] = (byte) 86;
    numArray5[9] = (byte) 51;
    numArray5[6] = (byte) 10;
    numArray5[17] = (byte) 230;
    numArray5[14] = (byte) 214;
    numArray5[7] = (byte) 0;
    numArray5[15] = (byte) 1;
    numArray5[11] = (byte) 167;
    numArray5[16 /*0x10*/] = (byte) 233;
    numArray5[13] = (byte) 117;
    numArray5[12] = (byte) 156;
    numArray5[5] = (byte) 11;
    numArray5[0] = (byte) 145;
    numArray5[10] = (byte) 181;
    byte[] numArray6 = new byte[18]
    {
      (byte) 68,
      (byte) 230,
      (byte) 198,
      (byte) 22,
      (byte) 24,
      (byte) 13,
      (byte) 118,
      (byte) 134,
      (byte) 14,
      (byte) 152,
      (byte) 110,
      (byte) 107,
      (byte) 248,
      (byte) 246,
      (byte) 66,
      (byte) 31 /*0x1F*/,
      (byte) 43,
      (byte) 194
    };
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 18);
    for (int index = 0; index < 18; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
