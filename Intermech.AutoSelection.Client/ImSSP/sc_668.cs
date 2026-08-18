// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_668
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_668
{
  internal static string ssp_automatch_669()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[23];
      byte[] numArray2 = new byte[23]
      {
        (byte) 212,
        (byte) 198,
        (byte) 179,
        (byte) 70,
        (byte) 61,
        (byte) 54,
        (byte) 109,
        (byte) 189,
        (byte) 149,
        (byte) 229,
        (byte) 8,
        (byte) 200,
        (byte) 215,
        (byte) 13,
        (byte) 29,
        (byte) 205,
        (byte) 217,
        (byte) 2,
        (byte) 40,
        (byte) 205,
        (byte) 47,
        (byte) 145,
        (byte) 137
      };
      byte[] numArray3 = new byte[23];
      numArray3[6] = (byte) 137;
      numArray3[9] = (byte) 136;
      numArray3[11] = (byte) 104;
      numArray3[15] = (byte) 193;
      numArray3[4] = (byte) 3;
      numArray3[10] = (byte) 89;
      numArray3[2] = (byte) 52;
      numArray3[7] = (byte) 205;
      numArray3[8] = (byte) 184;
      numArray3[21] = (byte) 2;
      numArray3[3] = (byte) 215;
      numArray3[17] = (byte) 78;
      numArray3[12] = (byte) 14;
      numArray3[13] = (byte) 26;
      numArray3[14] = (byte) 122;
      numArray3[1] = (byte) 66;
      numArray3[16 /*0x10*/] = (byte) 13;
      numArray3[5] = (byte) 167;
      numArray3[18] = (byte) 238;
      numArray3[19] = (byte) 218;
      numArray3[20] = (byte) 96 /*0x60*/;
      numArray3[0] = (byte) 201;
      numArray3[22] = (byte) 146;
      key.Query(true, 338, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 23);
      for (int index = 0; index < 23; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[23];
    byte[] numArray5 = new byte[23]
    {
      (byte) 189,
      (byte) 43,
      (byte) 213,
      (byte) 46,
      (byte) 100,
      (byte) 238,
      (byte) 67,
      (byte) 60,
      (byte) 247,
      (byte) 96 /*0x60*/,
      (byte) 108,
      (byte) 224 /*0xE0*/,
      (byte) 10,
      (byte) 220,
      (byte) 104,
      (byte) 95,
      (byte) 201,
      (byte) 127 /*0x7F*/,
      (byte) 36,
      (byte) 156,
      (byte) 202,
      (byte) 150,
      (byte) 192 /*0xC0*/
    };
    byte[] numArray6 = new byte[23]
    {
      (byte) 243,
      (byte) 219,
      (byte) 224 /*0xE0*/,
      (byte) 243,
      (byte) 104,
      (byte) 232,
      (byte) 141,
      (byte) 14,
      (byte) 201,
      (byte) 54,
      (byte) 209,
      (byte) 90,
      (byte) 4,
      (byte) 125,
      (byte) 150,
      (byte) 14,
      (byte) 9,
      (byte) 80 /*0x50*/,
      (byte) 249,
      (byte) 151,
      (byte) 253,
      byte.MaxValue,
      (byte) 158
    };
    key.Query(true, 338, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 23);
    for (int index = 0; index < 23; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
