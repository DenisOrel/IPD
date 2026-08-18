// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_10482
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Protection;
using System;

#nullable disable
namespace ImSSP;

internal static class sc_10482
{
  internal static int ssp_appserver_10483(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[34] = (byte) 145;
    sourceArray1[1] = (byte) 252;
    sourceArray1[29] = (byte) 168;
    sourceArray1[3] = (byte) 226;
    sourceArray1[6] = (byte) 251;
    sourceArray1[5] = (byte) 129;
    sourceArray1[13] = (byte) 182;
    sourceArray1[28] = (byte) 253;
    sourceArray1[8] = (byte) 140;
    sourceArray1[9] = (byte) 227;
    sourceArray1[10] = (byte) 137;
    sourceArray1[11] = (byte) 6;
    sourceArray1[12] = (byte) 105;
    sourceArray1[17] = (byte) 144 /*0x90*/;
    sourceArray1[31 /*0x1F*/] = (byte) 143;
    sourceArray1[40] = (byte) 221;
    sourceArray1[16 /*0x10*/] = (byte) 229;
    sourceArray1[0] = (byte) 232;
    sourceArray1[2] = (byte) 247;
    sourceArray1[19] = (byte) 210;
    sourceArray1[20] = (byte) 56;
    sourceArray1[21] = (byte) 212;
    sourceArray1[22] = (byte) 217;
    sourceArray1[23] = (byte) 143;
    sourceArray1[42] = (byte) 249;
    sourceArray1[4] = (byte) 19;
    sourceArray1[26] = (byte) 31 /*0x1F*/;
    sourceArray1[45] = (byte) 83;
    sourceArray1[36] = (byte) 33;
    sourceArray1[14] = (byte) 219;
    sourceArray1[39] = (byte) 67;
    sourceArray1[25] = (byte) 216;
    sourceArray1[15] = (byte) 225;
    sourceArray1[24] = (byte) 0;
    sourceArray1[18] = (byte) 233;
    sourceArray1[35] = (byte) 79;
    sourceArray1[43] = (byte) 114;
    sourceArray1[37] = (byte) 93;
    sourceArray1[38] = (byte) 221;
    sourceArray1[33] = (byte) 86;
    sourceArray1[41] = (byte) 9;
    sourceArray1[27] = (byte) 144 /*0x90*/;
    sourceArray1[30] = (byte) 200;
    sourceArray1[44] = (byte) 187;
    sourceArray1[32 /*0x20*/] = (byte) 0;
    sourceArray1[47] = (byte) 8;
    sourceArray1[46] = (byte) 170;
    sourceArray1[7] = (byte) 153;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 120,
      (byte) 97,
      (byte) 150,
      (byte) 31 /*0x1F*/,
      (byte) 184,
      (byte) 23,
      (byte) 35,
      (byte) 250,
      (byte) 195,
      (byte) 227,
      (byte) 164,
      (byte) 234,
      (byte) 42,
      (byte) 77,
      (byte) 173,
      (byte) 216,
      (byte) 215,
      (byte) 102,
      (byte) 197,
      (byte) 58,
      (byte) 21,
      (byte) 34,
      (byte) 121,
      (byte) 54,
      (byte) 223,
      (byte) 79,
      (byte) 238,
      (byte) 18,
      (byte) 158,
      (byte) 2,
      (byte) 10,
      (byte) 26,
      (byte) 171,
      (byte) 177,
      (byte) 26,
      (byte) 121,
      (byte) 96 /*0x60*/,
      (byte) 135,
      (byte) 246,
      (byte) 97,
      (byte) 57,
      (byte) 247,
      (byte) 251,
      (byte) 55,
      (byte) 69,
      (byte) 13,
      (byte) 174,
      (byte) 29
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
