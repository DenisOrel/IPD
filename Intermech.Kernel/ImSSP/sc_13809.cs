// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_13809
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;


namespace ImSSP;

internal static class sc_13809
{
  internal static int ssp_appserver_13810(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[42] = (byte) 120;
    sourceArray1[43] = (byte) 54;
    sourceArray1[2] = (byte) 214;
    sourceArray1[3] = (byte) 66;
    sourceArray1[15] = (byte) 237;
    sourceArray1[5] = (byte) 165;
    sourceArray1[6] = (byte) 25;
    sourceArray1[18] = (byte) 189;
    sourceArray1[12] = (byte) 212;
    sourceArray1[9] = (byte) 227;
    sourceArray1[0] = (byte) 77;
    sourceArray1[11] = (byte) 206;
    sourceArray1[10] = (byte) 190;
    sourceArray1[16 /*0x10*/] = (byte) 117;
    sourceArray1[14] = (byte) 27;
    sourceArray1[7] = (byte) 112 /*0x70*/;
    sourceArray1[36] = (byte) 96 /*0x60*/;
    sourceArray1[17] = (byte) 61;
    sourceArray1[46] = (byte) 90;
    sourceArray1[19] = (byte) 130;
    sourceArray1[20] = (byte) 58;
    sourceArray1[25] = (byte) 12;
    sourceArray1[34] = (byte) 132;
    sourceArray1[23] = (byte) 62;
    sourceArray1[40] = (byte) 5;
    sourceArray1[24] = (byte) 170;
    sourceArray1[26] = byte.MaxValue;
    sourceArray1[4] = (byte) 115;
    sourceArray1[28] = (byte) 6;
    sourceArray1[39] = (byte) 92;
    sourceArray1[8] = (byte) 141;
    sourceArray1[1] = (byte) 250;
    sourceArray1[41] = (byte) 91;
    sourceArray1[33] = (byte) 15;
    sourceArray1[32 /*0x20*/] = (byte) 144 /*0x90*/;
    sourceArray1[22] = (byte) 192 /*0xC0*/;
    sourceArray1[45] = (byte) 174;
    sourceArray1[37] = (byte) 89;
    sourceArray1[38] = (byte) 37;
    sourceArray1[31 /*0x1F*/] = (byte) 186;
    sourceArray1[35] = (byte) 59;
    sourceArray1[29] = (byte) 6;
    sourceArray1[13] = (byte) 230;
    sourceArray1[30] = (byte) 185;
    sourceArray1[44] = (byte) 82;
    sourceArray1[21] = (byte) 168;
    sourceArray1[27] = (byte) 199;
    sourceArray1[47] = (byte) 148;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[44] = (byte) 252;
    sourceArray2[18] = (byte) 83;
    sourceArray2[30] = (byte) 81;
    sourceArray2[3] = (byte) 191;
    sourceArray2[36] = (byte) 44;
    sourceArray2[5] = (byte) 202;
    sourceArray2[37] = (byte) 223;
    sourceArray2[7] = (byte) 106;
    sourceArray2[14] = (byte) 32 /*0x20*/;
    sourceArray2[45] = (byte) 158;
    sourceArray2[8] = (byte) 157;
    sourceArray2[11] = (byte) 254;
    sourceArray2[15] = (byte) 18;
    sourceArray2[4] = (byte) 83;
    sourceArray2[46] = (byte) 62;
    sourceArray2[10] = (byte) 98;
    sourceArray2[16 /*0x10*/] = (byte) 51;
    sourceArray2[24] = (byte) 232;
    sourceArray2[20] = (byte) 134;
    sourceArray2[19] = (byte) 246;
    sourceArray2[26] = (byte) 81;
    sourceArray2[2] = (byte) 27;
    sourceArray2[22] = (byte) 211;
    sourceArray2[23] = (byte) 245;
    sourceArray2[33] = (byte) 119;
    sourceArray2[25] = (byte) 134;
    sourceArray2[40] = (byte) 163;
    sourceArray2[9] = (byte) 60;
    sourceArray2[12] = (byte) 248;
    sourceArray2[29] = (byte) 46;
    sourceArray2[0] = (byte) 226;
    sourceArray2[31 /*0x1F*/] = (byte) 216;
    sourceArray2[32 /*0x20*/] = (byte) 168;
    sourceArray2[1] = (byte) 54;
    sourceArray2[34] = (byte) 117;
    sourceArray2[17] = (byte) 76;
    sourceArray2[28] = (byte) 117;
    sourceArray2[27] = (byte) 237;
    sourceArray2[42] = (byte) 36;
    sourceArray2[39] = (byte) 64 /*0x40*/;
    sourceArray2[13] = (byte) 10;
    sourceArray2[41] = (byte) 180;
    sourceArray2[21] = (byte) 17;
    sourceArray2[43] = (byte) 168;
    sourceArray2[38] = (byte) 104;
    sourceArray2[35] = (byte) 202;
    sourceArray2[6] = (byte) 232;
    sourceArray2[47] = (byte) 118;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13811(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 124,
      (byte) 249,
      (byte) 7,
      (byte) 217,
      (byte) 59,
      (byte) 192 /*0xC0*/,
      (byte) 60,
      (byte) 215,
      (byte) 174,
      (byte) 109,
      (byte) 53,
      (byte) 36,
      (byte) 236,
      (byte) 56,
      (byte) 61,
      (byte) 58,
      (byte) 56,
      (byte) 51,
      (byte) 14,
      (byte) 14,
      (byte) 153,
      (byte) 0,
      (byte) 106,
      (byte) 102,
      (byte) 40,
      (byte) 190,
      (byte) 250,
      (byte) 49,
      (byte) 222,
      (byte) 27,
      (byte) 142,
      (byte) 168,
      (byte) 49,
      (byte) 83,
      (byte) 45,
      (byte) 209,
      (byte) 230,
      (byte) 100,
      (byte) 65,
      (byte) 220,
      (byte) 78,
      (byte) 68,
      (byte) 139,
      (byte) 224 /*0xE0*/,
      (byte) 78,
      (byte) 204,
      (byte) 38,
      (byte) 90
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[2] = (byte) 166;
    sourceArray2[23] = (byte) 228;
    sourceArray2[28] = (byte) 19;
    sourceArray2[5] = (byte) 84;
    sourceArray2[7] = (byte) 194;
    sourceArray2[1] = (byte) 138;
    sourceArray2[34] = (byte) 171;
    sourceArray2[8] = (byte) 201;
    sourceArray2[11] = (byte) 117;
    sourceArray2[40] = (byte) 210;
    sourceArray2[10] = (byte) 126;
    sourceArray2[35] = (byte) 174;
    sourceArray2[12] = (byte) 168;
    sourceArray2[19] = (byte) 2;
    sourceArray2[14] = (byte) 188;
    sourceArray2[18] = (byte) 126;
    sourceArray2[16 /*0x10*/] = (byte) 192 /*0xC0*/;
    sourceArray2[17] = (byte) 141;
    sourceArray2[36] = (byte) 216;
    sourceArray2[21] = (byte) 31 /*0x1F*/;
    sourceArray2[20] = (byte) 45;
    sourceArray2[29] = (byte) 34;
    sourceArray2[41] = (byte) 2;
    sourceArray2[3] = (byte) 227;
    sourceArray2[24] = (byte) 169;
    sourceArray2[6] = (byte) 227;
    sourceArray2[25] = (byte) 180;
    sourceArray2[33] = (byte) 54;
    sourceArray2[9] = (byte) 184;
    sourceArray2[32 /*0x20*/] = (byte) 5;
    sourceArray2[30] = (byte) 198;
    sourceArray2[37] = (byte) 128 /*0x80*/;
    sourceArray2[26] = (byte) 113;
    sourceArray2[27] = (byte) 13;
    sourceArray2[0] = (byte) 158;
    sourceArray2[4] = (byte) 123;
    sourceArray2[15] = (byte) 60;
    sourceArray2[22] = (byte) 125;
    sourceArray2[31 /*0x1F*/] = (byte) 166;
    sourceArray2[39] = (byte) 123;
    sourceArray2[38] = (byte) 230;
    sourceArray2[47] = (byte) 115;
    sourceArray2[42] = (byte) 38;
    sourceArray2[43] = (byte) 138;
    sourceArray2[44] = (byte) 219;
    sourceArray2[45] = (byte) 46;
    sourceArray2[46] = (byte) 23;
    sourceArray2[13] = (byte) 245;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
