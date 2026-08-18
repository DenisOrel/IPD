// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_7779
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_7779
{
  internal static string ssp_imbase_7780()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[7];
      byte[] numArray2 = new byte[7]
      {
        (byte) 221,
        (byte) 176 /*0xB0*/,
        (byte) 117,
        (byte) 137,
        (byte) 190,
        (byte) 34,
        (byte) 187
      };
      byte[] numArray3 = new byte[7];
      numArray3[3] = (byte) 50;
      numArray3[1] = (byte) 220;
      numArray3[2] = (byte) 242;
      numArray3[0] = (byte) 250;
      numArray3[5] = (byte) 39;
      numArray3[4] = (byte) 221;
      numArray3[6] = (byte) 47;
      key.Query(true, 343, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 7);
      for (int index = 0; index < 7; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[7];
    byte[] numArray5 = new byte[7];
    numArray5[4] = (byte) 1;
    numArray5[1] = (byte) 253;
    numArray5[2] = (byte) 26;
    numArray5[0] = (byte) 30;
    numArray5[3] = (byte) 27;
    numArray5[5] = (byte) 249;
    numArray5[6] = byte.MaxValue;
    byte[] numArray6 = new byte[7];
    numArray6[2] = (byte) 220;
    numArray6[6] = (byte) 200;
    numArray6[1] = (byte) 74;
    numArray6[5] = (byte) 79;
    numArray6[4] = (byte) 122;
    numArray6[0] = (byte) 141;
    numArray6[3] = (byte) 234;
    key.Query(true, 343, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 7);
    for (int index = 0; index < 7; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_imbase_7781()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[17];
      byte[] numArray2 = new byte[17]
      {
        (byte) 39,
        (byte) 228,
        (byte) 207,
        (byte) 163,
        (byte) 171,
        (byte) 98,
        (byte) 59,
        (byte) 32 /*0x20*/,
        (byte) 34,
        (byte) 156,
        (byte) 43,
        (byte) 113,
        (byte) 80 /*0x50*/,
        (byte) 219,
        (byte) 183,
        (byte) 118,
        (byte) 209
      };
      byte[] numArray3 = new byte[17];
      numArray3[11] = (byte) 5;
      numArray3[1] = (byte) 194;
      numArray3[2] = (byte) 104;
      numArray3[3] = (byte) 229;
      numArray3[4] = (byte) 140;
      numArray3[5] = (byte) 239;
      numArray3[0] = (byte) 88;
      numArray3[16 /*0x10*/] = (byte) 10;
      numArray3[8] = (byte) 121;
      numArray3[7] = (byte) 27;
      numArray3[13] = (byte) 123;
      numArray3[15] = (byte) 213;
      numArray3[12] = (byte) 168;
      numArray3[10] = (byte) 148;
      numArray3[14] = (byte) 15;
      numArray3[6] = (byte) 163;
      numArray3[9] = (byte) 17;
      key.Query(true, 343, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 17);
      for (int index = 0; index < 17; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[17];
    byte[] numArray5 = new byte[17];
    numArray5[4] = (byte) 164;
    numArray5[10] = (byte) 119;
    numArray5[2] = (byte) 55;
    numArray5[13] = (byte) 143;
    numArray5[11] = (byte) 230;
    numArray5[1] = (byte) 81;
    numArray5[7] = (byte) 25;
    numArray5[6] = (byte) 254;
    numArray5[0] = (byte) 21;
    numArray5[9] = (byte) 222;
    numArray5[8] = (byte) 87;
    numArray5[5] = (byte) 38;
    numArray5[12] = (byte) 167;
    numArray5[16 /*0x10*/] = (byte) 238;
    numArray5[14] = (byte) 37;
    numArray5[15] = (byte) 155;
    numArray5[3] = (byte) 172;
    byte[] numArray6 = new byte[17]
    {
      (byte) 87,
      (byte) 87,
      (byte) 236,
      (byte) 223,
      (byte) 123,
      (byte) 229,
      (byte) 14,
      (byte) 228,
      (byte) 32 /*0x20*/,
      (byte) 130,
      (byte) 160 /*0xA0*/,
      (byte) 66,
      (byte) 155,
      (byte) 216,
      (byte) 39,
      (byte) 221,
      (byte) 216
    };
    key.Query(true, 343, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 17);
    for (int index = 0; index < 17; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
