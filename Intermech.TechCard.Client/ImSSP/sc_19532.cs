// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_19532
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_19532
{
  internal static string ssp_techcard_19533()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19];
      numArray2[14] = (byte) 119;
      numArray2[4] = (byte) 105;
      numArray2[10] = (byte) 71;
      numArray2[3] = (byte) 183;
      numArray2[0] = (byte) 7;
      numArray2[1] = (byte) 165;
      numArray2[13] = (byte) 195;
      numArray2[7] = (byte) 169;
      numArray2[5] = (byte) 9;
      numArray2[9] = (byte) 17;
      numArray2[6] = (byte) 149;
      numArray2[11] = (byte) 132;
      numArray2[8] = (byte) 231;
      numArray2[12] = (byte) 212;
      numArray2[16 /*0x10*/] = (byte) 88;
      numArray2[15] = (byte) 54;
      numArray2[2] = (byte) 218;
      numArray2[17] = (byte) 48 /*0x30*/;
      numArray2[18] = (byte) 176 /*0xB0*/;
      byte[] numArray3 = new byte[19]
      {
        (byte) 208 /*0xD0*/,
        (byte) 226,
        (byte) 132,
        (byte) 196,
        (byte) 92,
        (byte) 106,
        (byte) 90,
        (byte) 76,
        (byte) 217,
        (byte) 185,
        (byte) 175,
        (byte) 129,
        (byte) 234,
        (byte) 160 /*0xA0*/,
        (byte) 25,
        (byte) 139,
        (byte) 254,
        (byte) 204,
        (byte) 163
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
      (byte) 15,
      (byte) 52,
      (byte) 253,
      (byte) 159,
      (byte) 177,
      (byte) 201,
      (byte) 136,
      (byte) 164,
      (byte) 243,
      (byte) 39,
      (byte) 183,
      (byte) 11,
      (byte) 202,
      (byte) 84,
      (byte) 47,
      (byte) 171,
      (byte) 172,
      (byte) 5,
      (byte) 246
    };
    byte[] numArray6 = new byte[19]
    {
      (byte) 70,
      (byte) 57,
      (byte) 34,
      (byte) 171,
      (byte) 126,
      (byte) 131,
      (byte) 133,
      (byte) 19,
      (byte) 28,
      (byte) 176 /*0xB0*/,
      (byte) 20,
      (byte) 12,
      (byte) 190,
      (byte) 229,
      (byte) 62,
      (byte) 19,
      (byte) 123,
      (byte) 97,
      (byte) 220
    };
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
