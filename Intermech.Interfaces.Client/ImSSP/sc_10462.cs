// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_10462
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Protection;
using System;

#nullable disable
namespace ImSSP;

internal static class sc_10462
{
  internal static int ssp_appserver_10463(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 20,
      (byte) 103,
      (byte) 39,
      (byte) 145,
      (byte) 239,
      (byte) 159,
      (byte) 224 /*0xE0*/,
      (byte) 162,
      (byte) 115,
      (byte) 211,
      (byte) 155,
      (byte) 198,
      (byte) 154,
      (byte) 60,
      (byte) 56,
      (byte) 141,
      (byte) 246,
      (byte) 206,
      (byte) 207,
      (byte) 88,
      (byte) 212,
      (byte) 94,
      (byte) 73,
      (byte) 78,
      (byte) 156,
      (byte) 190,
      (byte) 120,
      (byte) 229,
      (byte) 124,
      (byte) 241,
      (byte) 36,
      (byte) 240 /*0xF0*/,
      (byte) 234,
      (byte) 62,
      (byte) 13,
      (byte) 99,
      (byte) 50,
      (byte) 9,
      (byte) 240 /*0xF0*/,
      (byte) 179,
      (byte) 170,
      (byte) 168,
      (byte) 139,
      (byte) 179,
      (byte) 124,
      (byte) 16 /*0x10*/,
      (byte) 67,
      (byte) 44
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[42] = (byte) 247;
    sourceArray2[19] = (byte) 144 /*0x90*/;
    sourceArray2[21] = (byte) 223;
    sourceArray2[41] = (byte) 116;
    sourceArray2[2] = (byte) 13;
    sourceArray2[5] = (byte) 208 /*0xD0*/;
    sourceArray2[39] = (byte) 195;
    sourceArray2[7] = (byte) 62;
    sourceArray2[38] = (byte) 165;
    sourceArray2[0] = (byte) 33;
    sourceArray2[10] = (byte) 18;
    sourceArray2[11] = (byte) 70;
    sourceArray2[32 /*0x20*/] = (byte) 8;
    sourceArray2[13] = (byte) 214;
    sourceArray2[31 /*0x1F*/] = (byte) 203;
    sourceArray2[15] = (byte) 212;
    sourceArray2[16 /*0x10*/] = (byte) 133;
    sourceArray2[17] = (byte) 45;
    sourceArray2[26] = (byte) 6;
    sourceArray2[27] = (byte) 113;
    sourceArray2[20] = (byte) 128 /*0x80*/;
    sourceArray2[33] = (byte) 130;
    sourceArray2[12] = (byte) 128 /*0x80*/;
    sourceArray2[4] = (byte) 213;
    sourceArray2[24] = (byte) 49;
    sourceArray2[25] = (byte) 61;
    sourceArray2[14] = (byte) 84;
    sourceArray2[37] = (byte) 80 /*0x50*/;
    sourceArray2[3] = (byte) 245;
    sourceArray2[28] = (byte) 5;
    sourceArray2[30] = byte.MaxValue;
    sourceArray2[8] = (byte) 40;
    sourceArray2[9] = (byte) 88;
    sourceArray2[43] = (byte) 211;
    sourceArray2[34] = (byte) 126;
    sourceArray2[35] = (byte) 98;
    sourceArray2[36] = (byte) 115;
    sourceArray2[46] = (byte) 128 /*0x80*/;
    sourceArray2[6] = (byte) 110;
    sourceArray2[23] = (byte) 187;
    sourceArray2[40] = (byte) 95;
    sourceArray2[18] = (byte) 238;
    sourceArray2[22] = (byte) 251;
    sourceArray2[29] = (byte) 77;
    sourceArray2[44] = (byte) 54;
    sourceArray2[45] = (byte) 253;
    sourceArray2[1] = (byte) 203;
    sourceArray2[47] = (byte) 219;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
