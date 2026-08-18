// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_685
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_685
{
  internal static string ssp_automatch_686()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[23];
      byte[] numArray2 = new byte[23]
      {
        (byte) 81,
        (byte) 6,
        (byte) 13,
        (byte) 56,
        (byte) 160 /*0xA0*/,
        (byte) 55,
        (byte) 175,
        (byte) 251,
        (byte) 205,
        (byte) 246,
        (byte) 184,
        (byte) 200,
        (byte) 99,
        (byte) 152,
        (byte) 186,
        (byte) 127 /*0x7F*/,
        (byte) 68,
        (byte) 155,
        (byte) 36,
        (byte) 0,
        (byte) 153,
        (byte) 230,
        (byte) 57
      };
      byte[] numArray3 = new byte[23];
      numArray3[12] = (byte) 7;
      numArray3[1] = (byte) 99;
      numArray3[2] = (byte) 99;
      numArray3[20] = (byte) 137;
      numArray3[14] = (byte) 52;
      numArray3[5] = (byte) 254;
      numArray3[4] = (byte) 87;
      numArray3[15] = (byte) 196;
      numArray3[8] = (byte) 1;
      numArray3[22] = (byte) 45;
      numArray3[17] = (byte) 224 /*0xE0*/;
      numArray3[9] = (byte) 213;
      numArray3[18] = (byte) 233;
      numArray3[13] = (byte) 239;
      numArray3[16 /*0x10*/] = (byte) 158;
      numArray3[10] = (byte) 88;
      numArray3[7] = (byte) 232;
      numArray3[21] = (byte) 36;
      numArray3[3] = (byte) 103;
      numArray3[19] = (byte) 242;
      numArray3[11] = (byte) 223;
      numArray3[0] = (byte) 35;
      numArray3[6] = (byte) 187;
      key.Query(true, 338, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 23);
      for (int index = 0; index < 23; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[23];
    byte[] numArray5 = new byte[23]
    {
      (byte) 42,
      (byte) 4,
      (byte) 172,
      (byte) 103,
      (byte) 160 /*0xA0*/,
      (byte) 44,
      (byte) 106,
      (byte) 205,
      (byte) 215,
      (byte) 120,
      (byte) 173,
      (byte) 251,
      (byte) 56,
      (byte) 78,
      (byte) 19,
      (byte) 157,
      (byte) 198,
      (byte) 162,
      (byte) 238,
      (byte) 203,
      (byte) 54,
      (byte) 81,
      (byte) 27
    };
    byte[] numArray6 = new byte[23]
    {
      (byte) 149,
      (byte) 17,
      (byte) 69,
      (byte) 170,
      (byte) 251,
      (byte) 145,
      (byte) 21,
      (byte) 149,
      (byte) 81,
      (byte) 239,
      (byte) 195,
      (byte) 114,
      (byte) 206,
      (byte) 238,
      (byte) 16 /*0x10*/,
      (byte) 209,
      (byte) 211,
      (byte) 47,
      (byte) 36,
      (byte) 72,
      (byte) 37,
      (byte) 234,
      (byte) 79
    };
    key.Query(true, 338, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 23);
    for (int index = 0; index < 23; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
