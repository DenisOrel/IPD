// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_13827
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;


namespace ImSSP;

internal static class sc_13827
{
  internal static int ssp_appserver_13828(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[27] = (byte) 214;
    sourceArray1[1] = (byte) 168;
    sourceArray1[25] = (byte) 245;
    sourceArray1[4] = (byte) 77;
    sourceArray1[14] = (byte) 243;
    sourceArray1[5] = (byte) 233;
    sourceArray1[15] = (byte) 32 /*0x20*/;
    sourceArray1[31 /*0x1F*/] = (byte) 38;
    sourceArray1[8] = (byte) 55;
    sourceArray1[9] = (byte) 16 /*0x10*/;
    sourceArray1[37] = (byte) 183;
    sourceArray1[47] = (byte) 137;
    sourceArray1[12] = (byte) 10;
    sourceArray1[10] = (byte) 147;
    sourceArray1[16 /*0x10*/] = (byte) 73;
    sourceArray1[23] = (byte) 213;
    sourceArray1[28] = (byte) 60;
    sourceArray1[2] = (byte) 37;
    sourceArray1[18] = (byte) 52;
    sourceArray1[19] = (byte) 100;
    sourceArray1[0] = (byte) 79;
    sourceArray1[44] = (byte) 195;
    sourceArray1[7] = (byte) 91;
    sourceArray1[46] = (byte) 9;
    sourceArray1[13] = (byte) 60;
    sourceArray1[29] = (byte) 251;
    sourceArray1[26] = (byte) 59;
    sourceArray1[45] = (byte) 218;
    sourceArray1[21] = (byte) 202;
    sourceArray1[36] = (byte) 214;
    sourceArray1[30] = (byte) 41;
    sourceArray1[33] = (byte) 77;
    sourceArray1[32 /*0x20*/] = (byte) 84;
    sourceArray1[6] = (byte) 146;
    sourceArray1[34] = (byte) 59;
    sourceArray1[24] = (byte) 137;
    sourceArray1[20] = (byte) 109;
    sourceArray1[11] = (byte) 96 /*0x60*/;
    sourceArray1[38] = (byte) 87;
    sourceArray1[39] = (byte) 64 /*0x40*/;
    sourceArray1[41] = (byte) 86;
    sourceArray1[35] = (byte) 1;
    sourceArray1[42] = (byte) 247;
    sourceArray1[43] = (byte) 12;
    sourceArray1[3] = (byte) 161;
    sourceArray1[22] = (byte) 79;
    sourceArray1[40] = (byte) 183;
    sourceArray1[17] = (byte) 58;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 210,
      (byte) 150,
      (byte) 131,
      (byte) 247,
      (byte) 75,
      (byte) 136,
      (byte) 127 /*0x7F*/,
      (byte) 111,
      (byte) 141,
      (byte) 253,
      (byte) 180,
      (byte) 103,
      (byte) 38,
      (byte) 88,
      (byte) 209,
      (byte) 168,
      (byte) 112 /*0x70*/,
      (byte) 23,
      (byte) 219,
      (byte) 242,
      (byte) 154,
      (byte) 69,
      (byte) 160 /*0xA0*/,
      (byte) 179,
      (byte) 117,
      (byte) 216,
      (byte) 156,
      (byte) 81,
      (byte) 72,
      (byte) 224 /*0xE0*/,
      (byte) 25,
      (byte) 227,
      (byte) 238,
      (byte) 74,
      (byte) 239,
      (byte) 159,
      (byte) 231,
      (byte) 17,
      (byte) 186,
      (byte) 186,
      (byte) 7,
      (byte) 54,
      (byte) 144 /*0x90*/,
      (byte) 46,
      (byte) 191,
      (byte) 154,
      (byte) 99,
      (byte) 104
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
