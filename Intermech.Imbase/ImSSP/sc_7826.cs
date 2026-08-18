// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_7826
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Protection;
using System;

#nullable disable
namespace ImSSP;

internal static class sc_7826
{
  internal static int ssp_imbase_7827(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[30] = (byte) 72;
    sourceArray1[24] = (byte) 31 /*0x1F*/;
    sourceArray1[39] = (byte) 38;
    sourceArray1[37] = (byte) 215;
    sourceArray1[36] = (byte) 156;
    sourceArray1[29] = (byte) 205;
    sourceArray1[6] = (byte) 17;
    sourceArray1[27] = (byte) 77;
    sourceArray1[3] = (byte) 180;
    sourceArray1[9] = (byte) 121;
    sourceArray1[25] = (byte) 17;
    sourceArray1[26] = (byte) 31 /*0x1F*/;
    sourceArray1[12] = (byte) 205;
    sourceArray1[13] = (byte) 133;
    sourceArray1[46] = (byte) 233;
    sourceArray1[35] = (byte) 149;
    sourceArray1[20] = (byte) 16 /*0x10*/;
    sourceArray1[17] = (byte) 212;
    sourceArray1[18] = (byte) 140;
    sourceArray1[19] = (byte) 222;
    sourceArray1[1] = (byte) 194;
    sourceArray1[21] = (byte) 162;
    sourceArray1[0] = (byte) 189;
    sourceArray1[23] = (byte) 139;
    sourceArray1[15] = (byte) 121;
    sourceArray1[14] = (byte) 221;
    sourceArray1[7] = (byte) 236;
    sourceArray1[47] = (byte) 151;
    sourceArray1[38] = (byte) 231;
    sourceArray1[11] = (byte) 205;
    sourceArray1[33] = (byte) 98;
    sourceArray1[31 /*0x1F*/] = (byte) 148;
    sourceArray1[4] = (byte) 119;
    sourceArray1[32 /*0x20*/] = (byte) 215;
    sourceArray1[34] = (byte) 241;
    sourceArray1[5] = (byte) 124;
    sourceArray1[8] = (byte) 117;
    sourceArray1[16 /*0x10*/] = (byte) 93;
    sourceArray1[22] = (byte) 218;
    sourceArray1[2] = (byte) 64 /*0x40*/;
    sourceArray1[40] = (byte) 214;
    sourceArray1[41] = (byte) 52;
    sourceArray1[42] = (byte) 131;
    sourceArray1[43] = (byte) 231;
    sourceArray1[44] = (byte) 172;
    sourceArray1[45] = (byte) 65;
    sourceArray1[10] = (byte) 131;
    sourceArray1[28] = (byte) 89;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 95,
      (byte) 102,
      (byte) 100,
      (byte) 7,
      (byte) 203,
      (byte) 187,
      (byte) 118,
      (byte) 110,
      (byte) 46,
      (byte) 218,
      (byte) 225,
      (byte) 254,
      (byte) 84,
      (byte) 47,
      (byte) 224 /*0xE0*/,
      (byte) 100,
      (byte) 140,
      (byte) 36,
      (byte) 16 /*0x10*/,
      (byte) 70,
      (byte) 60,
      (byte) 106,
      (byte) 157,
      (byte) 157,
      (byte) 21,
      (byte) 63 /*0x3F*/,
      (byte) 204,
      (byte) 217,
      (byte) 91,
      (byte) 185,
      (byte) 157,
      (byte) 111,
      (byte) 68,
      (byte) 156,
      (byte) 64 /*0x40*/,
      (byte) 25,
      (byte) 153,
      (byte) 172,
      (byte) 200,
      (byte) 182,
      (byte) 103,
      (byte) 86,
      (byte) 73,
      (byte) 38,
      (byte) 250,
      (byte) 151,
      (byte) 31 /*0x1F*/,
      (byte) 60
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 343, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
