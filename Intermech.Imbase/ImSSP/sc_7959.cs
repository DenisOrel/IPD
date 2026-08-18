// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_7959
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Protection;
using System;

#nullable disable
namespace ImSSP;

internal static class sc_7959
{
  internal static int ssp_imbase_7960(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 1,
      (byte) 234,
      (byte) 15,
      (byte) 245,
      (byte) 70,
      (byte) 110,
      (byte) 244,
      (byte) 233,
      (byte) 65,
      (byte) 37,
      (byte) 163,
      (byte) 44,
      (byte) 189,
      (byte) 45,
      (byte) 196,
      (byte) 173,
      (byte) 201,
      (byte) 57,
      (byte) 140,
      (byte) 254,
      (byte) 254,
      (byte) 70,
      (byte) 221,
      (byte) 8,
      (byte) 205,
      (byte) 86,
      (byte) 186,
      (byte) 13,
      (byte) 147,
      (byte) 187,
      (byte) 181,
      (byte) 232,
      (byte) 16 /*0x10*/,
      (byte) 245,
      (byte) 143,
      (byte) 241,
      (byte) 80 /*0x50*/,
      (byte) 182,
      (byte) 202,
      (byte) 140,
      (byte) 35,
      (byte) 169,
      (byte) 50,
      (byte) 32 /*0x20*/,
      (byte) 158,
      (byte) 228,
      (byte) 111,
      (byte) 105
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[5] = (byte) 234;
    sourceArray2[3] = (byte) 115;
    sourceArray2[31 /*0x1F*/] = (byte) 112 /*0x70*/;
    sourceArray2[1] = (byte) 95;
    sourceArray2[41] = (byte) 116;
    sourceArray2[35] = (byte) 3;
    sourceArray2[6] = (byte) 73;
    sourceArray2[44] = (byte) 25;
    sourceArray2[8] = (byte) 246;
    sourceArray2[9] = (byte) 15;
    sourceArray2[10] = (byte) 48 /*0x30*/;
    sourceArray2[11] = (byte) 17;
    sourceArray2[12] = (byte) 21;
    sourceArray2[13] = (byte) 149;
    sourceArray2[14] = (byte) 128 /*0x80*/;
    sourceArray2[15] = (byte) 254;
    sourceArray2[43] = (byte) 18;
    sourceArray2[17] = (byte) 145;
    sourceArray2[18] = (byte) 213;
    sourceArray2[19] = (byte) 7;
    sourceArray2[26] = (byte) 18;
    sourceArray2[2] = (byte) 121;
    sourceArray2[39] = (byte) 151;
    sourceArray2[23] = (byte) 195;
    sourceArray2[24] = (byte) 171;
    sourceArray2[25] = (byte) 20;
    sourceArray2[30] = (byte) 15;
    sourceArray2[27] = (byte) 125;
    sourceArray2[4] = (byte) 165;
    sourceArray2[29] = (byte) 26;
    sourceArray2[34] = (byte) 43;
    sourceArray2[21] = (byte) 101;
    sourceArray2[32 /*0x20*/] = (byte) 131;
    sourceArray2[45] = (byte) 159;
    sourceArray2[22] = (byte) 13;
    sourceArray2[20] = (byte) 1;
    sourceArray2[36] = (byte) 47;
    sourceArray2[37] = (byte) 97;
    sourceArray2[7] = (byte) 128 /*0x80*/;
    sourceArray2[0] = (byte) 7;
    sourceArray2[40] = (byte) 24;
    sourceArray2[38] = (byte) 137;
    sourceArray2[42] = (byte) 1;
    sourceArray2[33] = (byte) 0;
    sourceArray2[47] = (byte) 222;
    sourceArray2[28] = (byte) 120;
    sourceArray2[46] = (byte) 115;
    sourceArray2[16 /*0x10*/] = (byte) 59;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 343, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
