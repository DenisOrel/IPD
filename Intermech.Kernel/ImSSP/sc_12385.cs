// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_12385
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;


namespace ImSSP;

internal static class sc_12385
{
  internal static int ssp_appserver_12386(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[18] = (byte) 161;
    sourceArray1[0] = (byte) 92;
    sourceArray1[2] = (byte) 180;
    sourceArray1[3] = (byte) 236;
    sourceArray1[4] = (byte) 77;
    sourceArray1[5] = (byte) 25;
    sourceArray1[44] = (byte) 235;
    sourceArray1[7] = (byte) 35;
    sourceArray1[43] = (byte) 40;
    sourceArray1[12] = (byte) 167;
    sourceArray1[10] = (byte) 114;
    sourceArray1[41] = (byte) 60;
    sourceArray1[31 /*0x1F*/] = (byte) 235;
    sourceArray1[13] = (byte) 80 /*0x50*/;
    sourceArray1[14] = (byte) 63 /*0x3F*/;
    sourceArray1[1] = (byte) 161;
    sourceArray1[17] = (byte) 128 /*0x80*/;
    sourceArray1[29] = (byte) 90;
    sourceArray1[40] = (byte) 9;
    sourceArray1[19] = (byte) 227;
    sourceArray1[45] = (byte) 213;
    sourceArray1[21] = (byte) 180;
    sourceArray1[36] = (byte) 8;
    sourceArray1[23] = (byte) 133;
    sourceArray1[24] = (byte) 209;
    sourceArray1[25] = (byte) 195;
    sourceArray1[37] = (byte) 189;
    sourceArray1[6] = (byte) 56;
    sourceArray1[28] = (byte) 207;
    sourceArray1[42] = (byte) 123;
    sourceArray1[9] = (byte) 145;
    sourceArray1[26] = (byte) 245;
    sourceArray1[34] = (byte) 83;
    sourceArray1[33] = (byte) 232;
    sourceArray1[30] = (byte) 187;
    sourceArray1[39] = (byte) 13;
    sourceArray1[16 /*0x10*/] = (byte) 70;
    sourceArray1[8] = (byte) 225;
    sourceArray1[38] = (byte) 246;
    sourceArray1[15] = (byte) 104;
    sourceArray1[32 /*0x20*/] = (byte) 107;
    sourceArray1[22] = (byte) 18;
    sourceArray1[27] = (byte) 222;
    sourceArray1[20] = (byte) 134;
    sourceArray1[46] = (byte) 195;
    sourceArray1[35] = (byte) 57;
    sourceArray1[11] = (byte) 43;
    sourceArray1[47] = (byte) 21;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 139,
      (byte) 26,
      (byte) 49,
      (byte) 102,
      (byte) 121,
      (byte) 191,
      (byte) 151,
      (byte) 158,
      (byte) 156,
      (byte) 67,
      (byte) 88,
      (byte) 221,
      (byte) 53,
      (byte) 88,
      (byte) 183,
      (byte) 198,
      (byte) 226,
      (byte) 166,
      (byte) 125,
      (byte) 61,
      (byte) 144 /*0x90*/,
      (byte) 16 /*0x10*/,
      (byte) 65,
      (byte) 160 /*0xA0*/,
      (byte) 236,
      (byte) 166,
      (byte) 78,
      (byte) 18,
      (byte) 111,
      (byte) 178,
      (byte) 243,
      (byte) 45,
      (byte) 225,
      (byte) 204,
      (byte) 224 /*0xE0*/,
      (byte) 41,
      (byte) 113,
      (byte) 243,
      (byte) 200,
      (byte) 35,
      (byte) 193,
      (byte) 226,
      (byte) 106,
      (byte) 116,
      (byte) 52,
      (byte) 56,
      (byte) 188,
      (byte) 42
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
