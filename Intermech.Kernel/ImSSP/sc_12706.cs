// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_12706
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;


namespace ImSSP;

internal static class sc_12706
{
  internal static int ssp_appserver_12707(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[16 /*0x10*/] = (byte) 68;
    sourceArray1[19] = (byte) 24;
    sourceArray1[2] = (byte) 25;
    sourceArray1[6] = (byte) 185;
    sourceArray1[4] = (byte) 2;
    sourceArray1[42] = (byte) 32 /*0x20*/;
    sourceArray1[45] = (byte) 180;
    sourceArray1[7] = (byte) 220;
    sourceArray1[8] = (byte) 245;
    sourceArray1[1] = (byte) 175;
    sourceArray1[36] = (byte) 13;
    sourceArray1[11] = (byte) 73;
    sourceArray1[15] = (byte) 23;
    sourceArray1[13] = (byte) 34;
    sourceArray1[14] = (byte) 215;
    sourceArray1[33] = (byte) 231;
    sourceArray1[12] = (byte) 172;
    sourceArray1[0] = (byte) 100;
    sourceArray1[18] = (byte) 148;
    sourceArray1[44] = (byte) 87;
    sourceArray1[20] = (byte) 86;
    sourceArray1[21] = (byte) 109;
    sourceArray1[22] = (byte) 96 /*0x60*/;
    sourceArray1[39] = (byte) 76;
    sourceArray1[47] = (byte) 63 /*0x3F*/;
    sourceArray1[25] = (byte) 81;
    sourceArray1[26] = (byte) 253;
    sourceArray1[27] = (byte) 242;
    sourceArray1[28] = (byte) 31 /*0x1F*/;
    sourceArray1[29] = (byte) 58;
    sourceArray1[37] = (byte) 196;
    sourceArray1[31 /*0x1F*/] = (byte) 252;
    sourceArray1[3] = (byte) 43;
    sourceArray1[34] = (byte) 83;
    sourceArray1[10] = (byte) 153;
    sourceArray1[23] = (byte) 132;
    sourceArray1[9] = (byte) 134;
    sourceArray1[35] = (byte) 60;
    sourceArray1[17] = (byte) 234;
    sourceArray1[24] = (byte) 157;
    sourceArray1[38] = (byte) 165;
    sourceArray1[41] = (byte) 64 /*0x40*/;
    sourceArray1[40] = (byte) 24;
    sourceArray1[43] = (byte) 76;
    sourceArray1[32 /*0x20*/] = (byte) 140;
    sourceArray1[5] = (byte) 0;
    sourceArray1[46] = (byte) 243;
    sourceArray1[30] = (byte) 128 /*0x80*/;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 37,
      (byte) 145,
      (byte) 128 /*0x80*/,
      (byte) 52,
      (byte) 106,
      (byte) 254,
      (byte) 233,
      (byte) 191,
      (byte) 105,
      (byte) 78,
      (byte) 230,
      (byte) 140,
      (byte) 49,
      (byte) 134,
      (byte) 20,
      (byte) 87,
      (byte) 254,
      (byte) 242,
      (byte) 15,
      (byte) 186,
      (byte) 3,
      (byte) 48 /*0x30*/,
      (byte) 48 /*0x30*/,
      (byte) 90,
      (byte) 48 /*0x30*/,
      (byte) 9,
      (byte) 24,
      (byte) 248,
      (byte) 4,
      (byte) 212,
      (byte) 189,
      (byte) 167,
      (byte) 145,
      (byte) 192 /*0xC0*/,
      (byte) 148,
      (byte) 222,
      (byte) 194,
      (byte) 131,
      (byte) 138,
      byte.MaxValue,
      (byte) 1,
      (byte) 251,
      (byte) 209,
      (byte) 126,
      (byte) 56,
      (byte) 100,
      (byte) 20,
      (byte) 13
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12708(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 45,
      (byte) 112 /*0x70*/,
      (byte) 185,
      (byte) 154,
      (byte) 250,
      (byte) 64 /*0x40*/,
      (byte) 225,
      (byte) 66,
      (byte) 127 /*0x7F*/,
      (byte) 171,
      (byte) 59,
      (byte) 103,
      (byte) 164,
      (byte) 83,
      (byte) 67,
      (byte) 40,
      (byte) 66,
      (byte) 251,
      (byte) 11,
      (byte) 202,
      (byte) 115,
      (byte) 116,
      (byte) 115,
      (byte) 173,
      (byte) 146,
      (byte) 74,
      (byte) 81,
      (byte) 252,
      (byte) 173,
      (byte) 146,
      (byte) 231,
      (byte) 205,
      (byte) 115,
      (byte) 242,
      (byte) 47,
      (byte) 78,
      (byte) 229,
      (byte) 223,
      (byte) 197,
      (byte) 185,
      (byte) 242,
      (byte) 7,
      (byte) 209,
      (byte) 176 /*0xB0*/,
      (byte) 177,
      (byte) 16 /*0x10*/,
      (byte) 145,
      (byte) 160 /*0xA0*/
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[21] = (byte) 204;
    sourceArray2[1] = (byte) 61;
    sourceArray2[10] = (byte) 74;
    sourceArray2[28] = (byte) 125;
    sourceArray2[26] = (byte) 218;
    sourceArray2[5] = (byte) 2;
    sourceArray2[9] = (byte) 168;
    sourceArray2[45] = (byte) 179;
    sourceArray2[8] = (byte) 89;
    sourceArray2[3] = (byte) 241;
    sourceArray2[36] = (byte) 212;
    sourceArray2[11] = (byte) 198;
    sourceArray2[12] = (byte) 97;
    sourceArray2[14] = (byte) 244;
    sourceArray2[43] = (byte) 218;
    sourceArray2[15] = (byte) 164;
    sourceArray2[20] = (byte) 20;
    sourceArray2[17] = (byte) 164;
    sourceArray2[0] = (byte) 109;
    sourceArray2[19] = (byte) 82;
    sourceArray2[16 /*0x10*/] = (byte) 168;
    sourceArray2[27] = (byte) 122;
    sourceArray2[22] = (byte) 6;
    sourceArray2[18] = (byte) 131;
    sourceArray2[2] = (byte) 59;
    sourceArray2[38] = (byte) 254;
    sourceArray2[23] = (byte) 29;
    sourceArray2[6] = (byte) 10;
    sourceArray2[7] = (byte) 217;
    sourceArray2[44] = (byte) 201;
    sourceArray2[29] = (byte) 53;
    sourceArray2[47] = (byte) 103;
    sourceArray2[32 /*0x20*/] = (byte) 233;
    sourceArray2[33] = (byte) 196;
    sourceArray2[34] = (byte) 4;
    sourceArray2[35] = (byte) 194;
    sourceArray2[31 /*0x1F*/] = (byte) 26;
    sourceArray2[37] = (byte) 181;
    sourceArray2[13] = (byte) 248;
    sourceArray2[39] = (byte) 222;
    sourceArray2[40] = (byte) 72;
    sourceArray2[41] = (byte) 11;
    sourceArray2[42] = (byte) 228;
    sourceArray2[4] = (byte) 214;
    sourceArray2[24] = (byte) 152;
    sourceArray2[46] = (byte) 252;
    sourceArray2[25] = (byte) 109;
    sourceArray2[30] = (byte) 159;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
