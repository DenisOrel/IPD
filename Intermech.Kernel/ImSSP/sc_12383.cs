// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_12383
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;


namespace ImSSP;

internal static class sc_12383
{
  internal static int ssp_appserver_12384(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[16 /*0x10*/] = (byte) 245;
    sourceArray1[25] = (byte) 30;
    sourceArray1[2] = (byte) 54;
    sourceArray1[11] = (byte) 210;
    sourceArray1[4] = (byte) 31 /*0x1F*/;
    sourceArray1[43] = (byte) 112 /*0x70*/;
    sourceArray1[1] = (byte) 9;
    sourceArray1[41] = (byte) 85;
    sourceArray1[8] = (byte) 131;
    sourceArray1[27] = (byte) 84;
    sourceArray1[38] = (byte) 103;
    sourceArray1[19] = (byte) 40;
    sourceArray1[12] = (byte) 14;
    sourceArray1[13] = (byte) 5;
    sourceArray1[45] = (byte) 178;
    sourceArray1[15] = (byte) 112 /*0x70*/;
    sourceArray1[35] = (byte) 99;
    sourceArray1[17] = (byte) 194;
    sourceArray1[7] = (byte) 88;
    sourceArray1[32 /*0x20*/] = (byte) 236;
    sourceArray1[20] = (byte) 21;
    sourceArray1[21] = (byte) 224 /*0xE0*/;
    sourceArray1[40] = (byte) 191;
    sourceArray1[23] = (byte) 154;
    sourceArray1[42] = (byte) 244;
    sourceArray1[3] = (byte) 193;
    sourceArray1[26] = (byte) 73;
    sourceArray1[34] = (byte) 56;
    sourceArray1[28] = (byte) 104;
    sourceArray1[29] = (byte) 191;
    sourceArray1[22] = (byte) 4;
    sourceArray1[31 /*0x1F*/] = (byte) 243;
    sourceArray1[0] = (byte) 94;
    sourceArray1[9] = (byte) 101;
    sourceArray1[30] = (byte) 156;
    sourceArray1[14] = (byte) 71;
    sourceArray1[36] = (byte) 157;
    sourceArray1[18] = (byte) 45;
    sourceArray1[37] = (byte) 198;
    sourceArray1[39] = (byte) 180;
    sourceArray1[6] = (byte) 121;
    sourceArray1[33] = (byte) 253;
    sourceArray1[10] = (byte) 25;
    sourceArray1[5] = (byte) 100;
    sourceArray1[44] = (byte) 42;
    sourceArray1[47] = (byte) 110;
    sourceArray1[46] = (byte) 109;
    sourceArray1[24] = (byte) 102;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 151,
      (byte) 252,
      (byte) 145,
      (byte) 115,
      (byte) 83,
      (byte) 189,
      (byte) 93,
      (byte) 6,
      (byte) 141,
      (byte) 23,
      (byte) 0,
      (byte) 44,
      (byte) 183,
      (byte) 91,
      (byte) 184,
      (byte) 60,
      (byte) 116,
      byte.MaxValue,
      (byte) 40,
      (byte) 249,
      (byte) 96 /*0x60*/,
      (byte) 217,
      (byte) 160 /*0xA0*/,
      (byte) 231,
      (byte) 98,
      (byte) 8,
      (byte) 246,
      (byte) 59,
      (byte) 145,
      (byte) 104,
      (byte) 11,
      (byte) 191,
      (byte) 182,
      (byte) 124,
      (byte) 176 /*0xB0*/,
      (byte) 11,
      (byte) 186,
      (byte) 25,
      (byte) 29,
      (byte) 232,
      (byte) 119,
      (byte) 3,
      (byte) 176 /*0xB0*/,
      (byte) 44,
      (byte) 119,
      (byte) 98,
      (byte) 230,
      (byte) 76
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
