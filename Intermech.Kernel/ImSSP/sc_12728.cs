// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_12728
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;


namespace ImSSP;

internal static class sc_12728
{
  internal static int ssp_appserver_12729(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[30] = (byte) 153;
    sourceArray1[1] = (byte) 172;
    sourceArray1[7] = (byte) 178;
    sourceArray1[40] = (byte) 50;
    sourceArray1[4] = (byte) 79;
    sourceArray1[12] = (byte) 89;
    sourceArray1[44] = (byte) 69;
    sourceArray1[20] = (byte) 90;
    sourceArray1[8] = (byte) 52;
    sourceArray1[9] = (byte) 230;
    sourceArray1[32 /*0x20*/] = (byte) 80 /*0x50*/;
    sourceArray1[41] = (byte) 235;
    sourceArray1[39] = (byte) 124;
    sourceArray1[34] = (byte) 7;
    sourceArray1[14] = (byte) 225;
    sourceArray1[15] = (byte) 222;
    sourceArray1[16 /*0x10*/] = (byte) 139;
    sourceArray1[6] = (byte) 240 /*0xF0*/;
    sourceArray1[47] = (byte) 72;
    sourceArray1[19] = (byte) 158;
    sourceArray1[0] = (byte) 168;
    sourceArray1[21] = (byte) 225;
    sourceArray1[22] = (byte) 91;
    sourceArray1[23] = (byte) 205;
    sourceArray1[24] = (byte) 202;
    sourceArray1[25] = (byte) 30;
    sourceArray1[26] = (byte) 192 /*0xC0*/;
    sourceArray1[27] = (byte) 16 /*0x10*/;
    sourceArray1[28] = (byte) 86;
    sourceArray1[31 /*0x1F*/] = (byte) 30;
    sourceArray1[10] = (byte) 239;
    sourceArray1[46] = (byte) 170;
    sourceArray1[3] = (byte) 236;
    sourceArray1[33] = (byte) 165;
    sourceArray1[35] = (byte) 232;
    sourceArray1[17] = (byte) 116;
    sourceArray1[11] = (byte) 97;
    sourceArray1[37] = (byte) 76;
    sourceArray1[38] = (byte) 64 /*0x40*/;
    sourceArray1[5] = (byte) 0;
    sourceArray1[29] = (byte) 234;
    sourceArray1[13] = (byte) 187;
    sourceArray1[42] = (byte) 48 /*0x30*/;
    sourceArray1[43] = (byte) 128 /*0x80*/;
    sourceArray1[45] = (byte) 95;
    sourceArray1[18] = (byte) 82;
    sourceArray1[36] = (byte) 181;
    sourceArray1[2] = (byte) 84;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 17,
      (byte) 9,
      (byte) 154,
      (byte) 163,
      (byte) 19,
      (byte) 210,
      (byte) 179,
      (byte) 123,
      (byte) 249,
      (byte) 165,
      (byte) 226,
      (byte) 52,
      (byte) 168,
      (byte) 231,
      (byte) 113,
      (byte) 254,
      (byte) 241,
      (byte) 99,
      (byte) 94,
      (byte) 126,
      (byte) 36,
      (byte) 169,
      (byte) 85,
      (byte) 226,
      (byte) 133,
      (byte) 190,
      (byte) 228,
      (byte) 125,
      (byte) 129,
      (byte) 9,
      (byte) 7,
      (byte) 23,
      (byte) 62,
      (byte) 91,
      (byte) 217,
      (byte) 198,
      (byte) 157,
      (byte) 37,
      (byte) 91,
      (byte) 86,
      (byte) 227,
      (byte) 139,
      (byte) 227,
      (byte) 40,
      (byte) 231,
      (byte) 147,
      (byte) 5,
      (byte) 100
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
