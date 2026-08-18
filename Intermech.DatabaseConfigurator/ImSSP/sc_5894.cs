// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_5894
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_5894
{
  internal static string ssp_imclient_5895()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[24];
      byte[] numArray2 = new byte[24];
      numArray2[2] = (byte) 113;
      numArray2[20] = (byte) 6;
      numArray2[7] = (byte) 153;
      numArray2[11] = (byte) 7;
      numArray2[23] = (byte) 48 /*0x30*/;
      numArray2[5] = (byte) 184;
      numArray2[6] = (byte) 240 /*0xF0*/;
      numArray2[19] = (byte) 123;
      numArray2[8] = (byte) 108;
      numArray2[3] = (byte) 180;
      numArray2[10] = (byte) 42;
      numArray2[14] = (byte) 118;
      numArray2[21] = (byte) 99;
      numArray2[1] = (byte) 98;
      numArray2[16 /*0x10*/] = (byte) 202;
      numArray2[15] = (byte) 53;
      numArray2[4] = (byte) 89;
      numArray2[13] = (byte) 133;
      numArray2[0] = (byte) 236;
      numArray2[18] = (byte) 208 /*0xD0*/;
      numArray2[12] = (byte) 14;
      numArray2[9] = (byte) 94;
      numArray2[22] = (byte) 15;
      numArray2[17] = (byte) 48 /*0x30*/;
      byte[] numArray3 = new byte[24]
      {
        (byte) 231,
        (byte) 28,
        (byte) 244,
        (byte) 70,
        (byte) 141,
        (byte) 73,
        (byte) 56,
        (byte) 60,
        (byte) 133,
        (byte) 5,
        (byte) 246,
        (byte) 16 /*0x10*/,
        (byte) 106,
        (byte) 181,
        (byte) 204,
        (byte) 223,
        (byte) 111,
        (byte) 26,
        (byte) 127 /*0x7F*/,
        (byte) 234,
        (byte) 26,
        (byte) 166,
        (byte) 109,
        (byte) 190
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 24);
      for (int index = 0; index < 24; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[24];
    byte[] numArray5 = new byte[24]
    {
      (byte) 253,
      (byte) 27,
      (byte) 41,
      (byte) 121,
      (byte) 136,
      (byte) 83,
      (byte) 64 /*0x40*/,
      (byte) 31 /*0x1F*/,
      (byte) 76,
      (byte) 169,
      (byte) 80 /*0x50*/,
      (byte) 215,
      (byte) 200,
      (byte) 130,
      (byte) 210,
      (byte) 248,
      (byte) 136,
      (byte) 113,
      (byte) 49,
      (byte) 117,
      (byte) 203,
      (byte) 0,
      (byte) 187,
      (byte) 177
    };
    byte[] numArray6 = new byte[24]
    {
      (byte) 19,
      (byte) 164,
      (byte) 241,
      (byte) 212,
      (byte) 184,
      (byte) 194,
      (byte) 15,
      (byte) 113,
      (byte) 172,
      (byte) 104,
      (byte) 156,
      (byte) 72,
      (byte) 243,
      (byte) 66,
      (byte) 32 /*0x20*/,
      (byte) 67,
      (byte) 112 /*0x70*/,
      (byte) 77,
      (byte) 207,
      (byte) 49,
      (byte) 39,
      (byte) 80 /*0x50*/,
      (byte) 250,
      (byte) 73
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 24);
    for (int index = 0; index < 24; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
