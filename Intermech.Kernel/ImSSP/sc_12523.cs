// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_12523
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;


namespace ImSSP;

internal static class sc_12523
{
  internal static int ssp_appserver_12524(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[30] = (byte) 48 /*0x30*/;
    sourceArray1[46] = (byte) 233;
    sourceArray1[2] = (byte) 207;
    sourceArray1[3] = (byte) 144 /*0x90*/;
    sourceArray1[4] = (byte) 77;
    sourceArray1[5] = (byte) 189;
    sourceArray1[6] = (byte) 173;
    sourceArray1[18] = (byte) 53;
    sourceArray1[0] = (byte) 241;
    sourceArray1[27] = (byte) 91;
    sourceArray1[25] = (byte) 203;
    sourceArray1[22] = (byte) 253;
    sourceArray1[12] = (byte) 6;
    sourceArray1[11] = (byte) 157;
    sourceArray1[14] = (byte) 34;
    sourceArray1[37] = (byte) 70;
    sourceArray1[16 /*0x10*/] = (byte) 146;
    sourceArray1[8] = (byte) 48 /*0x30*/;
    sourceArray1[32 /*0x20*/] = (byte) 243;
    sourceArray1[15] = (byte) 203;
    sourceArray1[41] = (byte) 187;
    sourceArray1[10] = (byte) 103;
    sourceArray1[40] = (byte) 212;
    sourceArray1[21] = (byte) 211;
    sourceArray1[19] = (byte) 63 /*0x3F*/;
    sourceArray1[17] = (byte) 206;
    sourceArray1[26] = (byte) 38;
    sourceArray1[39] = (byte) 133;
    sourceArray1[28] = (byte) 160 /*0xA0*/;
    sourceArray1[29] = (byte) 87;
    sourceArray1[44] = (byte) 60;
    sourceArray1[31 /*0x1F*/] = (byte) 186;
    sourceArray1[45] = (byte) 66;
    sourceArray1[9] = (byte) 64 /*0x40*/;
    sourceArray1[24] = (byte) 27;
    sourceArray1[35] = (byte) 106;
    sourceArray1[36] = (byte) 93;
    sourceArray1[38] = (byte) 14;
    sourceArray1[33] = (byte) 184;
    sourceArray1[1] = (byte) 211;
    sourceArray1[20] = (byte) 108;
    sourceArray1[34] = (byte) 63 /*0x3F*/;
    sourceArray1[42] = (byte) 168;
    sourceArray1[43] = (byte) 12;
    sourceArray1[13] = (byte) 210;
    sourceArray1[7] = (byte) 113;
    sourceArray1[23] = (byte) 243;
    sourceArray1[47] = (byte) 80 /*0x50*/;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 163,
      (byte) 26,
      (byte) 69,
      (byte) 119,
      (byte) 33,
      (byte) 65,
      (byte) 79,
      (byte) 63 /*0x3F*/,
      (byte) 48 /*0x30*/,
      (byte) 216,
      (byte) 66,
      (byte) 206,
      (byte) 79,
      (byte) 248,
      (byte) 85,
      (byte) 246,
      (byte) 94,
      (byte) 7,
      (byte) 70,
      (byte) 10,
      (byte) 43,
      (byte) 160 /*0xA0*/,
      (byte) 73,
      (byte) 48 /*0x30*/,
      (byte) 86,
      (byte) 201,
      (byte) 96 /*0x60*/,
      (byte) 143,
      (byte) 38,
      (byte) 183,
      (byte) 254,
      (byte) 162,
      (byte) 180,
      (byte) 94,
      (byte) 156,
      (byte) 15,
      (byte) 35,
      (byte) 217,
      (byte) 100,
      (byte) 166,
      (byte) 144 /*0x90*/,
      (byte) 152,
      (byte) 144 /*0x90*/,
      (byte) 6,
      (byte) 56,
      (byte) 17,
      (byte) 43,
      (byte) 219
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
