// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_12350
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;


namespace ImSSP;

internal static class sc_12350
{
  internal static int ssp_appserver_12351(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[23] = (byte) 244;
    sourceArray1[40] = (byte) 37;
    sourceArray1[2] = (byte) 48 /*0x30*/;
    sourceArray1[3] = (byte) 241;
    sourceArray1[4] = (byte) 226;
    sourceArray1[5] = (byte) 16 /*0x10*/;
    sourceArray1[6] = (byte) 177;
    sourceArray1[0] = (byte) 166;
    sourceArray1[25] = (byte) 95;
    sourceArray1[9] = (byte) 156;
    sourceArray1[44] = (byte) 103;
    sourceArray1[12] = (byte) 134;
    sourceArray1[22] = (byte) 42;
    sourceArray1[13] = (byte) 127 /*0x7F*/;
    sourceArray1[10] = (byte) 2;
    sourceArray1[15] = (byte) 134;
    sourceArray1[45] = (byte) 180;
    sourceArray1[27] = (byte) 146;
    sourceArray1[1] = (byte) 85;
    sourceArray1[8] = (byte) 45;
    sourceArray1[20] = (byte) 106;
    sourceArray1[14] = (byte) 108;
    sourceArray1[16 /*0x10*/] = (byte) 184;
    sourceArray1[39] = (byte) 90;
    sourceArray1[24] = (byte) 178;
    sourceArray1[46] = (byte) 161;
    sourceArray1[30] = (byte) 145;
    sourceArray1[19] = (byte) 73;
    sourceArray1[28] = (byte) 252;
    sourceArray1[21] = (byte) 75;
    sourceArray1[29] = (byte) 163;
    sourceArray1[41] = (byte) 117;
    sourceArray1[32 /*0x20*/] = (byte) 149;
    sourceArray1[47] = (byte) 70;
    sourceArray1[34] = (byte) 197;
    sourceArray1[35] = (byte) 83;
    sourceArray1[36] = (byte) 129;
    sourceArray1[37] = (byte) 47;
    sourceArray1[38] = (byte) 222;
    sourceArray1[33] = (byte) 97;
    sourceArray1[26] = (byte) 232;
    sourceArray1[7] = (byte) 141;
    sourceArray1[42] = (byte) 237;
    sourceArray1[43] = (byte) 246;
    sourceArray1[31 /*0x1F*/] = (byte) 10;
    sourceArray1[18] = (byte) 106;
    sourceArray1[17] = (byte) 191;
    sourceArray1[11] = (byte) 133;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 41,
      (byte) 230,
      (byte) 77,
      (byte) 212,
      (byte) 155,
      (byte) 66,
      (byte) 114,
      (byte) 187,
      (byte) 209,
      (byte) 83,
      (byte) 159,
      (byte) 28,
      (byte) 71,
      (byte) 171,
      (byte) 206,
      (byte) 75,
      (byte) 64 /*0x40*/,
      (byte) 101,
      (byte) 69,
      (byte) 66,
      (byte) 217,
      (byte) 247,
      (byte) 203,
      (byte) 230,
      (byte) 23,
      (byte) 10,
      (byte) 158,
      (byte) 46,
      (byte) 181,
      (byte) 224 /*0xE0*/,
      (byte) 239,
      (byte) 42,
      (byte) 11,
      (byte) 9,
      (byte) 37,
      (byte) 36,
      (byte) 196,
      (byte) 84,
      (byte) 187,
      (byte) 36,
      (byte) 40,
      (byte) 125,
      (byte) 250,
      (byte) 135,
      (byte) 42,
      (byte) 201,
      (byte) 221,
      (byte) 194
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
