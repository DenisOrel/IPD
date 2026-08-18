// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_10484
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Protection;
using System;

#nullable disable
namespace ImSSP;

internal static class sc_10484
{
  internal static int ssp_appserver_10485(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 151,
      (byte) 227,
      (byte) 37,
      (byte) 13,
      (byte) 20,
      (byte) 198,
      (byte) 118,
      (byte) 10,
      (byte) 205,
      (byte) 56,
      (byte) 188,
      (byte) 147,
      (byte) 52,
      (byte) 13,
      (byte) 75,
      (byte) 138,
      (byte) 109,
      (byte) 216,
      (byte) 203,
      (byte) 133,
      (byte) 4,
      (byte) 184,
      (byte) 166,
      (byte) 95,
      (byte) 5,
      (byte) 12,
      (byte) 17,
      (byte) 195,
      (byte) 130,
      (byte) 194,
      (byte) 196,
      (byte) 14,
      (byte) 17,
      (byte) 96 /*0x60*/,
      (byte) 149,
      (byte) 50,
      (byte) 186,
      (byte) 33,
      (byte) 247,
      (byte) 137,
      (byte) 254,
      (byte) 247,
      (byte) 140,
      (byte) 69,
      (byte) 111,
      (byte) 38,
      (byte) 70,
      (byte) 148
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[39] = (byte) 117;
    sourceArray2[21] = (byte) 11;
    sourceArray2[4] = (byte) 177;
    sourceArray2[3] = (byte) 219;
    sourceArray2[41] = (byte) 208 /*0xD0*/;
    sourceArray2[27] = (byte) 238;
    sourceArray2[6] = (byte) 76;
    sourceArray2[32 /*0x20*/] = (byte) 185;
    sourceArray2[10] = (byte) 66;
    sourceArray2[46] = (byte) 192 /*0xC0*/;
    sourceArray2[13] = (byte) 144 /*0x90*/;
    sourceArray2[11] = (byte) 231;
    sourceArray2[29] = (byte) 154;
    sourceArray2[9] = (byte) 139;
    sourceArray2[14] = (byte) 113;
    sourceArray2[33] = (byte) 15;
    sourceArray2[45] = (byte) 131;
    sourceArray2[42] = (byte) 70;
    sourceArray2[2] = (byte) 112 /*0x70*/;
    sourceArray2[19] = (byte) 113;
    sourceArray2[0] = (byte) 145;
    sourceArray2[17] = (byte) 163;
    sourceArray2[22] = (byte) 221;
    sourceArray2[23] = (byte) 97;
    sourceArray2[31 /*0x1F*/] = (byte) 135;
    sourceArray2[44] = (byte) 30;
    sourceArray2[26] = (byte) 54;
    sourceArray2[1] = (byte) 241;
    sourceArray2[34] = (byte) 108;
    sourceArray2[16 /*0x10*/] = (byte) 1;
    sourceArray2[30] = (byte) 184;
    sourceArray2[5] = (byte) 223;
    sourceArray2[15] = (byte) 85;
    sourceArray2[28] = (byte) 120;
    sourceArray2[12] = (byte) 43;
    sourceArray2[35] = (byte) 160 /*0xA0*/;
    sourceArray2[24] = (byte) 175;
    sourceArray2[37] = (byte) 40;
    sourceArray2[8] = (byte) 86;
    sourceArray2[25] = (byte) 227;
    sourceArray2[40] = (byte) 246;
    sourceArray2[38] = (byte) 180;
    sourceArray2[43] = (byte) 36;
    sourceArray2[18] = (byte) 16 /*0x10*/;
    sourceArray2[20] = (byte) 65;
    sourceArray2[36] = (byte) 213;
    sourceArray2[7] = (byte) 93;
    sourceArray2[47] = (byte) 199;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
