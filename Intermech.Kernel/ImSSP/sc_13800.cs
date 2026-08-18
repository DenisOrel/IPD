// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_13800
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;


namespace ImSSP;

internal static class sc_13800
{
  internal static int ssp_appserver_13801(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[16 /*0x10*/] = (byte) 146;
    sourceArray1[1] = (byte) 247;
    sourceArray1[23] = (byte) 68;
    sourceArray1[29] = (byte) 73;
    sourceArray1[4] = (byte) 134;
    sourceArray1[24] = (byte) 154;
    sourceArray1[6] = (byte) 128 /*0x80*/;
    sourceArray1[7] = (byte) 23;
    sourceArray1[17] = (byte) 171;
    sourceArray1[37] = (byte) 86;
    sourceArray1[10] = (byte) 233;
    sourceArray1[18] = (byte) 169;
    sourceArray1[12] = (byte) 86;
    sourceArray1[13] = (byte) 45;
    sourceArray1[42] = (byte) 115;
    sourceArray1[15] = (byte) 14;
    sourceArray1[9] = (byte) 225;
    sourceArray1[26] = (byte) 10;
    sourceArray1[11] = (byte) 190;
    sourceArray1[19] = (byte) 23;
    sourceArray1[8] = (byte) 31 /*0x1F*/;
    sourceArray1[21] = (byte) 202;
    sourceArray1[22] = (byte) 125;
    sourceArray1[38] = (byte) 50;
    sourceArray1[30] = (byte) 33;
    sourceArray1[25] = (byte) 205;
    sourceArray1[46] = (byte) 251;
    sourceArray1[41] = (byte) 31 /*0x1F*/;
    sourceArray1[34] = (byte) 83;
    sourceArray1[45] = (byte) 72;
    sourceArray1[5] = (byte) 92;
    sourceArray1[31 /*0x1F*/] = (byte) 72;
    sourceArray1[32 /*0x20*/] = (byte) 184;
    sourceArray1[20] = (byte) 53;
    sourceArray1[39] = (byte) 154;
    sourceArray1[35] = (byte) 139;
    sourceArray1[36] = (byte) 230;
    sourceArray1[14] = (byte) 219;
    sourceArray1[44] = (byte) 170;
    sourceArray1[33] = (byte) 168;
    sourceArray1[3] = (byte) 192 /*0xC0*/;
    sourceArray1[2] = (byte) 172;
    sourceArray1[0] = (byte) 3;
    sourceArray1[43] = (byte) 193;
    sourceArray1[40] = (byte) 154;
    sourceArray1[28] = (byte) 6;
    sourceArray1[27] = (byte) 91;
    sourceArray1[47] = (byte) 212;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 83,
      (byte) 209,
      (byte) 130,
      (byte) 250,
      (byte) 59,
      (byte) 231,
      (byte) 176 /*0xB0*/,
      (byte) 143,
      (byte) 237,
      (byte) 184,
      (byte) 197,
      (byte) 8,
      (byte) 79,
      (byte) 68,
      (byte) 27,
      (byte) 35,
      (byte) 242,
      (byte) 42,
      (byte) 211,
      (byte) 22,
      (byte) 251,
      (byte) 19,
      (byte) 93,
      (byte) 200,
      (byte) 12,
      (byte) 98,
      (byte) 110,
      (byte) 213,
      (byte) 57,
      (byte) 246,
      (byte) 113,
      (byte) 167,
      (byte) 83,
      (byte) 91,
      (byte) 128 /*0x80*/,
      (byte) 176 /*0xB0*/,
      (byte) 123,
      (byte) 105,
      (byte) 32 /*0x20*/,
      (byte) 83,
      (byte) 162,
      (byte) 128 /*0x80*/,
      (byte) 168,
      (byte) 122,
      (byte) 46,
      (byte) 82,
      (byte) 101,
      (byte) 24
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
