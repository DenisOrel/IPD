// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_7955
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_7955
{
  internal static string ssp_imbase_7956()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[6];
      byte[] numArray2 = new byte[6]
      {
        (byte) 75,
        (byte) 222,
        (byte) 131,
        (byte) 80 /*0x50*/,
        (byte) 195,
        (byte) 222
      };
      byte[] numArray3 = new byte[6]
      {
        (byte) 223,
        (byte) 44,
        (byte) 236,
        (byte) 19,
        (byte) 99,
        (byte) 248
      };
      key.Query(true, 343, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 6);
      for (int index = 0; index < 6; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[6];
    byte[] numArray5 = new byte[6];
    numArray5[4] = (byte) 95;
    numArray5[0] = (byte) 29;
    numArray5[2] = (byte) 144 /*0x90*/;
    numArray5[3] = (byte) 220;
    numArray5[5] = (byte) 172;
    numArray5[1] = (byte) 76;
    byte[] numArray6 = new byte[6];
    numArray6[1] = (byte) 220;
    numArray6[0] = (byte) 14;
    numArray6[2] = (byte) 161;
    numArray6[3] = (byte) 251;
    numArray6[4] = (byte) 174;
    numArray6[5] = (byte) 128 /*0x80*/;
    key.Query(true, 343, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 6);
    for (int index = 0; index < 6; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
