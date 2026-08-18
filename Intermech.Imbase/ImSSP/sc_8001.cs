// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_8001
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_8001
{
  internal static string ssp_imbase_8002()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[17];
      byte[] numArray2 = new byte[17]
      {
        (byte) 251,
        (byte) 183,
        (byte) 169,
        (byte) 78,
        (byte) 190,
        (byte) 204,
        (byte) 153,
        (byte) 45,
        (byte) 149,
        (byte) 172,
        (byte) 81,
        (byte) 208 /*0xD0*/,
        (byte) 116,
        (byte) 112 /*0x70*/,
        (byte) 246,
        (byte) 78,
        (byte) 139
      };
      byte[] numArray3 = new byte[17]
      {
        (byte) 175,
        (byte) 85,
        (byte) 70,
        (byte) 58,
        (byte) 236,
        (byte) 159,
        (byte) 228,
        (byte) 222,
        (byte) 149,
        (byte) 154,
        (byte) 54,
        (byte) 232,
        (byte) 95,
        (byte) 162,
        (byte) 63 /*0x3F*/,
        (byte) 14,
        (byte) 0
      };
      key.Query(true, 343, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 17);
      for (int index = 0; index < 17; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[17];
    byte[] numArray5 = new byte[17]
    {
      (byte) 1,
      (byte) 50,
      (byte) 43,
      (byte) 252,
      (byte) 101,
      (byte) 236,
      (byte) 149,
      (byte) 3,
      (byte) 62,
      (byte) 67,
      (byte) 22,
      (byte) 30,
      (byte) 202,
      (byte) 221,
      (byte) 207,
      (byte) 165,
      (byte) 179
    };
    byte[] numArray6 = new byte[17]
    {
      (byte) 122,
      (byte) 153,
      (byte) 247,
      (byte) 229,
      (byte) 241,
      (byte) 6,
      (byte) 177,
      (byte) 30,
      (byte) 242,
      (byte) 75,
      (byte) 166,
      (byte) 180,
      (byte) 88,
      (byte) 104,
      (byte) 153,
      (byte) 204,
      (byte) 168
    };
    key.Query(true, 343, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 17);
    for (int index = 0; index < 17; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
