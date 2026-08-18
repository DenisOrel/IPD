// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_12998
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;


namespace ImSSP;

internal static class sc_12998
{
  internal static int ssp_appserver_12999(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[0] = (byte) 27;
    sourceArray1[8] = (byte) 3;
    sourceArray1[3] = (byte) 9;
    sourceArray1[19] = (byte) 169;
    sourceArray1[4] = (byte) 107;
    sourceArray1[23] = (byte) 15;
    sourceArray1[41] = (byte) 156;
    sourceArray1[7] = (byte) 0;
    sourceArray1[1] = (byte) 215;
    sourceArray1[34] = (byte) 37;
    sourceArray1[10] = (byte) 219;
    sourceArray1[6] = (byte) 116;
    sourceArray1[12] = (byte) 193;
    sourceArray1[13] = (byte) 194;
    sourceArray1[14] = (byte) 184;
    sourceArray1[15] = (byte) 11;
    sourceArray1[43] = (byte) 254;
    sourceArray1[30] = (byte) 28;
    sourceArray1[26] = (byte) 62;
    sourceArray1[32 /*0x20*/] = (byte) 42;
    sourceArray1[18] = (byte) 94;
    sourceArray1[21] = (byte) 136;
    sourceArray1[22] = (byte) 92;
    sourceArray1[5] = (byte) 97;
    sourceArray1[27] = (byte) 16 /*0x10*/;
    sourceArray1[17] = (byte) 243;
    sourceArray1[11] = (byte) 45;
    sourceArray1[20] = (byte) 150;
    sourceArray1[28] = (byte) 142;
    sourceArray1[29] = (byte) 112 /*0x70*/;
    sourceArray1[16 /*0x10*/] = (byte) 60;
    sourceArray1[31 /*0x1F*/] = (byte) 93;
    sourceArray1[42] = (byte) 155;
    sourceArray1[33] = (byte) 50;
    sourceArray1[39] = (byte) 61;
    sourceArray1[35] = (byte) 248;
    sourceArray1[44] = (byte) 50;
    sourceArray1[37] = (byte) 24;
    sourceArray1[38] = (byte) 217;
    sourceArray1[9] = (byte) 162;
    sourceArray1[40] = (byte) 92;
    sourceArray1[25] = (byte) 164;
    sourceArray1[36] = (byte) 59;
    sourceArray1[2] = (byte) 231;
    sourceArray1[46] = (byte) 100;
    sourceArray1[45] = (byte) 216;
    sourceArray1[24] = (byte) 124;
    sourceArray1[47] = (byte) 108;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 84,
      (byte) 12,
      (byte) 121,
      (byte) 73,
      (byte) 213,
      (byte) 207,
      (byte) 152,
      (byte) 170,
      (byte) 143,
      (byte) 103,
      (byte) 103,
      (byte) 252,
      (byte) 211,
      (byte) 150,
      (byte) 170,
      (byte) 45,
      (byte) 244,
      (byte) 98,
      (byte) 62,
      (byte) 106,
      (byte) 103,
      (byte) 43,
      (byte) 188,
      (byte) 193,
      (byte) 216,
      (byte) 220,
      (byte) 86,
      (byte) 13,
      (byte) 117,
      (byte) 0,
      (byte) 227,
      (byte) 121,
      (byte) 108,
      (byte) 132,
      (byte) 151,
      (byte) 253,
      (byte) 20,
      (byte) 223,
      (byte) 222,
      (byte) 43,
      (byte) 97,
      (byte) 170,
      (byte) 159,
      (byte) 90,
      (byte) 105,
      (byte) 134,
      (byte) 135,
      (byte) 137
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
