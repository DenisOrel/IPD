// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_7950
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_7950
{
  internal static string ssp_imbase_7951()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[11];
      byte[] numArray2 = new byte[11]
      {
        (byte) 42,
        (byte) 228,
        (byte) 176 /*0xB0*/,
        (byte) 107,
        (byte) 5,
        (byte) 204,
        (byte) 132,
        (byte) 59,
        (byte) 158,
        (byte) 233,
        (byte) 253
      };
      byte[] numArray3 = new byte[11]
      {
        (byte) 217,
        (byte) 209,
        (byte) 83,
        (byte) 32 /*0x20*/,
        (byte) 26,
        (byte) 128 /*0x80*/,
        (byte) 153,
        (byte) 65,
        (byte) 140,
        (byte) 127 /*0x7F*/,
        (byte) 105
      };
      key.Query(true, 343, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 11);
      for (int index = 0; index < 11; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[11];
    byte[] numArray5 = new byte[11];
    numArray5[4] = (byte) 80 /*0x50*/;
    numArray5[3] = (byte) 0;
    numArray5[2] = (byte) 243;
    numArray5[10] = (byte) 34;
    numArray5[5] = (byte) 16 /*0x10*/;
    numArray5[1] = (byte) 213;
    numArray5[6] = (byte) 246;
    numArray5[7] = (byte) 253;
    numArray5[8] = (byte) 233;
    numArray5[9] = (byte) 221;
    numArray5[0] = (byte) 79;
    byte[] numArray6 = new byte[11];
    numArray6[6] = (byte) 102;
    numArray6[1] = (byte) 247;
    numArray6[0] = (byte) 227;
    numArray6[4] = (byte) 213;
    numArray6[9] = (byte) 122;
    numArray6[5] = (byte) 68;
    numArray6[2] = (byte) 11;
    numArray6[7] = (byte) 60;
    numArray6[8] = (byte) 217;
    numArray6[3] = (byte) 12;
    numArray6[10] = (byte) 235;
    key.Query(true, 343, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 11);
    for (int index = 0; index < 11; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
