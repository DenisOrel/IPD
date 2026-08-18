// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_7609
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_7609
{
  internal static string ssp_imbase_7610()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[16 /*0x10*/];
      byte[] numArray2 = new byte[16 /*0x10*/];
      numArray2[9] = (byte) 168;
      numArray2[1] = (byte) 241;
      numArray2[13] = (byte) 203;
      numArray2[8] = (byte) 205;
      numArray2[0] = (byte) 68;
      numArray2[4] = (byte) 125;
      numArray2[11] = (byte) 4;
      numArray2[7] = (byte) 80 /*0x50*/;
      numArray2[5] = (byte) 185;
      numArray2[3] = (byte) 143;
      numArray2[10] = (byte) 98;
      numArray2[6] = (byte) 181;
      numArray2[12] = (byte) 195;
      numArray2[14] = (byte) 184;
      numArray2[2] = (byte) 194;
      numArray2[15] = (byte) 38;
      byte[] numArray3 = new byte[16 /*0x10*/]
      {
        (byte) 33,
        (byte) 175,
        (byte) 132,
        (byte) 83,
        (byte) 193,
        (byte) 198,
        (byte) 143,
        (byte) 54,
        (byte) 14,
        (byte) 243,
        (byte) 51,
        (byte) 5,
        (byte) 2,
        (byte) 205,
        (byte) 184,
        (byte) 41
      };
      key.Query(true, 343, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 16 /*0x10*/);
      for (int index = 0; index < 16 /*0x10*/; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[16 /*0x10*/];
    byte[] numArray5 = new byte[16 /*0x10*/]
    {
      (byte) 201,
      (byte) 15,
      (byte) 23,
      (byte) 172,
      (byte) 184,
      (byte) 212,
      (byte) 186,
      (byte) 203,
      (byte) 184,
      (byte) 87,
      (byte) 143,
      (byte) 151,
      (byte) 70,
      (byte) 194,
      (byte) 84,
      (byte) 192 /*0xC0*/
    };
    byte[] numArray6 = new byte[16 /*0x10*/]
    {
      (byte) 157,
      (byte) 132,
      (byte) 80 /*0x50*/,
      (byte) 47,
      (byte) 56,
      (byte) 57,
      (byte) 150,
      (byte) 10,
      (byte) 167,
      (byte) 138,
      (byte) 22,
      (byte) 78,
      (byte) 179,
      (byte) 136,
      (byte) 145,
      (byte) 63 /*0x3F*/
    };
    key.Query(true, 343, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 16 /*0x10*/);
    for (int index = 0; index < 16 /*0x10*/; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
