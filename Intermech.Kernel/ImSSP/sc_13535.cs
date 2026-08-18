// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_13535
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;


namespace ImSSP;

internal static class sc_13535
{
  internal static int ssp_appserver_13536(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[21] = (byte) 245;
    sourceArray1[46] = (byte) 20;
    sourceArray1[18] = (byte) 54;
    sourceArray1[3] = (byte) 233;
    sourceArray1[7] = (byte) 86;
    sourceArray1[5] = (byte) 209;
    sourceArray1[6] = (byte) 141;
    sourceArray1[35] = (byte) 254;
    sourceArray1[8] = (byte) 153;
    sourceArray1[13] = (byte) 197;
    sourceArray1[10] = (byte) 232;
    sourceArray1[1] = (byte) 245;
    sourceArray1[12] = (byte) 68;
    sourceArray1[41] = (byte) 230;
    sourceArray1[14] = (byte) 46;
    sourceArray1[39] = (byte) 68;
    sourceArray1[16 /*0x10*/] = (byte) 135;
    sourceArray1[43] = (byte) 34;
    sourceArray1[31 /*0x1F*/] = (byte) 91;
    sourceArray1[4] = (byte) 11;
    sourceArray1[20] = (byte) 138;
    sourceArray1[34] = (byte) 143;
    sourceArray1[38] = (byte) 161;
    sourceArray1[23] = (byte) 1;
    sourceArray1[24] = (byte) 76;
    sourceArray1[25] = (byte) 95;
    sourceArray1[26] = (byte) 188;
    sourceArray1[15] = (byte) 126;
    sourceArray1[33] = (byte) 51;
    sourceArray1[22] = (byte) 43;
    sourceArray1[11] = (byte) 155;
    sourceArray1[0] = (byte) 127 /*0x7F*/;
    sourceArray1[32 /*0x20*/] = (byte) 9;
    sourceArray1[28] = (byte) 14;
    sourceArray1[19] = (byte) 81;
    sourceArray1[2] = (byte) 102;
    sourceArray1[36] = (byte) 132;
    sourceArray1[37] = (byte) 153;
    sourceArray1[45] = (byte) 120;
    sourceArray1[29] = (byte) 221;
    sourceArray1[40] = (byte) 236;
    sourceArray1[9] = (byte) 25;
    sourceArray1[42] = (byte) 0;
    sourceArray1[17] = (byte) 54;
    sourceArray1[44] = (byte) 202;
    sourceArray1[27] = (byte) 26;
    sourceArray1[30] = (byte) 144 /*0x90*/;
    sourceArray1[47] = (byte) 182;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[3] = (byte) 29;
    sourceArray2[0] = (byte) 66;
    sourceArray2[1] = (byte) 115;
    sourceArray2[39] = (byte) 164;
    sourceArray2[11] = (byte) 5;
    sourceArray2[38] = (byte) 0;
    sourceArray2[6] = (byte) 186;
    sourceArray2[7] = (byte) 150;
    sourceArray2[8] = (byte) 200;
    sourceArray2[19] = (byte) 23;
    sourceArray2[42] = (byte) 3;
    sourceArray2[37] = (byte) 171;
    sourceArray2[12] = (byte) 52;
    sourceArray2[17] = (byte) 218;
    sourceArray2[14] = (byte) 175;
    sourceArray2[15] = (byte) 236;
    sourceArray2[16 /*0x10*/] = (byte) 113;
    sourceArray2[4] = (byte) 77;
    sourceArray2[36] = (byte) 46;
    sourceArray2[13] = (byte) 112 /*0x70*/;
    sourceArray2[20] = (byte) 12;
    sourceArray2[2] = (byte) 4;
    sourceArray2[45] = (byte) 175;
    sourceArray2[23] = (byte) 87;
    sourceArray2[24] = (byte) 173;
    sourceArray2[25] = (byte) 237;
    sourceArray2[22] = (byte) 43;
    sourceArray2[27] = (byte) 139;
    sourceArray2[28] = (byte) 55;
    sourceArray2[29] = (byte) 107;
    sourceArray2[9] = (byte) 123;
    sourceArray2[31 /*0x1F*/] = (byte) 252;
    sourceArray2[32 /*0x20*/] = (byte) 249;
    sourceArray2[33] = (byte) 222;
    sourceArray2[41] = (byte) 41;
    sourceArray2[35] = (byte) 36;
    sourceArray2[40] = (byte) 151;
    sourceArray2[26] = (byte) 174;
    sourceArray2[47] = (byte) 95;
    sourceArray2[18] = (byte) 122;
    sourceArray2[30] = (byte) 88;
    sourceArray2[34] = (byte) 110;
    sourceArray2[44] = (byte) 84;
    sourceArray2[43] = (byte) 184;
    sourceArray2[21] = (byte) 10;
    sourceArray2[5] = (byte) 196;
    sourceArray2[46] = (byte) 101;
    sourceArray2[10] = (byte) 97;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
