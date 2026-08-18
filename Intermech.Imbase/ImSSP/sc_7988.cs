// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_7988
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_7988
{
  internal static string ssp_imbase_7989()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[6];
      byte[] numArray2 = new byte[6]
      {
        (byte) 200,
        (byte) 168,
        (byte) 203,
        (byte) 31 /*0x1F*/,
        (byte) 63 /*0x3F*/,
        (byte) 127 /*0x7F*/
      };
      byte[] numArray3 = new byte[6]
      {
        (byte) 0,
        (byte) 87,
        (byte) 0,
        (byte) 0,
        (byte) 120,
        (byte) 0
      };
      numArray3[0] = (byte) 229;
      numArray3[3] = (byte) 231;
      numArray3[2] = (byte) 214;
      numArray3[5] = (byte) 121;
      key.Query(true, 343, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 6);
      for (int index = 0; index < 6; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[6];
    byte[] numArray5 = new byte[6];
    numArray5[1] = (byte) 203;
    numArray5[0] = (byte) 99;
    numArray5[2] = (byte) 93;
    numArray5[3] = (byte) 93;
    numArray5[4] = (byte) 37;
    numArray5[5] = (byte) 231;
    byte[] numArray6 = new byte[6]
    {
      (byte) 0,
      (byte) 0,
      (byte) 174,
      (byte) 0,
      (byte) 213,
      (byte) 0
    };
    numArray6[0] = (byte) 101;
    numArray6[3] = (byte) 236;
    numArray6[1] = (byte) 246;
    numArray6[5] = (byte) 203;
    key.Query(true, 343, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 6);
    for (int index = 0; index < 6; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
