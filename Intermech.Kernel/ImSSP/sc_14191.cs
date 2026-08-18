// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_14191
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;


namespace ImSSP;

internal static class sc_14191
{
  internal static int ssp_appserver_14192(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[31 /*0x1F*/] = (byte) 145;
    sourceArray1[14] = (byte) 91;
    sourceArray1[2] = (byte) 16 /*0x10*/;
    sourceArray1[36] = (byte) 37;
    sourceArray1[4] = (byte) 13;
    sourceArray1[5] = (byte) 251;
    sourceArray1[21] = (byte) 142;
    sourceArray1[7] = (byte) 158;
    sourceArray1[8] = (byte) 1;
    sourceArray1[26] = (byte) 230;
    sourceArray1[3] = (byte) 74;
    sourceArray1[34] = (byte) 173;
    sourceArray1[35] = (byte) 25;
    sourceArray1[16 /*0x10*/] = (byte) 172;
    sourceArray1[13] = (byte) 10;
    sourceArray1[15] = (byte) 88;
    sourceArray1[9] = (byte) 4;
    sourceArray1[10] = (byte) 116;
    sourceArray1[0] = (byte) 4;
    sourceArray1[42] = (byte) 223;
    sourceArray1[20] = (byte) 247;
    sourceArray1[37] = (byte) 58;
    sourceArray1[22] = (byte) 169;
    sourceArray1[11] = (byte) 152;
    sourceArray1[24] = (byte) 95;
    sourceArray1[6] = (byte) 182;
    sourceArray1[23] = (byte) 213;
    sourceArray1[18] = (byte) 214;
    sourceArray1[28] = (byte) 32 /*0x20*/;
    sourceArray1[29] = (byte) 129;
    sourceArray1[27] = (byte) 130;
    sourceArray1[25] = (byte) 126;
    sourceArray1[33] = (byte) 109;
    sourceArray1[12] = (byte) 234;
    sourceArray1[19] = (byte) 32 /*0x20*/;
    sourceArray1[17] = (byte) 158;
    sourceArray1[1] = (byte) 161;
    sourceArray1[47] = (byte) 28;
    sourceArray1[38] = (byte) 0;
    sourceArray1[39] = (byte) 190;
    sourceArray1[46] = (byte) 66;
    sourceArray1[41] = (byte) 136;
    sourceArray1[30] = (byte) 73;
    sourceArray1[43] = (byte) 203;
    sourceArray1[44] = (byte) 157;
    sourceArray1[45] = (byte) 214;
    sourceArray1[32 /*0x20*/] = (byte) 223;
    sourceArray1[40] = (byte) 254;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 119,
      (byte) 112 /*0x70*/,
      (byte) 56,
      (byte) 168,
      (byte) 72,
      (byte) 110,
      (byte) 249,
      (byte) 199,
      (byte) 1,
      (byte) 143,
      (byte) 40,
      (byte) 109,
      (byte) 52,
      (byte) 244,
      (byte) 39,
      (byte) 113,
      (byte) 93,
      (byte) 70,
      (byte) 121,
      (byte) 55,
      (byte) 252,
      (byte) 102,
      (byte) 218,
      (byte) 162,
      (byte) 9,
      byte.MaxValue,
      (byte) 17,
      (byte) 107,
      (byte) 175,
      (byte) 203,
      (byte) 199,
      (byte) 9,
      (byte) 186,
      (byte) 18,
      (byte) 221,
      (byte) 68,
      (byte) 78,
      (byte) 125,
      (byte) 173,
      (byte) 120,
      (byte) 40,
      (byte) 57,
      (byte) 67,
      (byte) 161,
      (byte) 167,
      (byte) 35,
      (byte) 31 /*0x1F*/,
      (byte) 67
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
