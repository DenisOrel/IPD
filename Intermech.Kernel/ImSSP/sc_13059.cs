// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_13059
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;


namespace ImSSP;

internal static class sc_13059
{
  internal static int ssp_appserver_13060(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 204,
      (byte) 67,
      (byte) 109,
      (byte) 150,
      (byte) 171,
      (byte) 62,
      (byte) 130,
      (byte) 65,
      (byte) 151,
      (byte) 250,
      (byte) 143,
      (byte) 45,
      (byte) 193,
      (byte) 92,
      (byte) 143,
      (byte) 234,
      (byte) 51,
      (byte) 72,
      (byte) 40,
      (byte) 125,
      (byte) 119,
      (byte) 90,
      (byte) 161,
      (byte) 158,
      (byte) 13,
      (byte) 61,
      (byte) 2,
      (byte) 231,
      (byte) 204,
      (byte) 223,
      (byte) 181,
      (byte) 73,
      (byte) 226,
      (byte) 132,
      (byte) 243,
      (byte) 208 /*0xD0*/,
      (byte) 78,
      (byte) 53,
      (byte) 92,
      (byte) 220,
      (byte) 158,
      (byte) 212,
      (byte) 187,
      (byte) 21,
      (byte) 100,
      (byte) 133,
      (byte) 151,
      (byte) 97
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[10] = (byte) 34;
    sourceArray2[1] = (byte) 122;
    sourceArray2[14] = (byte) 44;
    sourceArray2[27] = (byte) 16 /*0x10*/;
    sourceArray2[8] = (byte) 80 /*0x50*/;
    sourceArray2[47] = (byte) 238;
    sourceArray2[20] = (byte) 146;
    sourceArray2[7] = (byte) 64 /*0x40*/;
    sourceArray2[38] = (byte) 106;
    sourceArray2[9] = (byte) 147;
    sourceArray2[35] = (byte) 209;
    sourceArray2[11] = (byte) 211;
    sourceArray2[12] = (byte) 47;
    sourceArray2[24] = (byte) 52;
    sourceArray2[29] = (byte) 145;
    sourceArray2[15] = (byte) 45;
    sourceArray2[16 /*0x10*/] = (byte) 85;
    sourceArray2[17] = (byte) 249;
    sourceArray2[18] = (byte) 118;
    sourceArray2[19] = (byte) 12;
    sourceArray2[32 /*0x20*/] = (byte) 37;
    sourceArray2[21] = (byte) 101;
    sourceArray2[6] = (byte) 206;
    sourceArray2[23] = (byte) 88;
    sourceArray2[31 /*0x1F*/] = (byte) 153;
    sourceArray2[2] = (byte) 42;
    sourceArray2[26] = (byte) 182;
    sourceArray2[4] = (byte) 52;
    sourceArray2[13] = (byte) 250;
    sourceArray2[39] = (byte) 134;
    sourceArray2[5] = (byte) 37;
    sourceArray2[33] = (byte) 59;
    sourceArray2[28] = (byte) 111;
    sourceArray2[44] = (byte) 217;
    sourceArray2[3] = (byte) 57;
    sourceArray2[43] = (byte) 51;
    sourceArray2[36] = (byte) 22;
    sourceArray2[25] = (byte) 49;
    sourceArray2[30] = (byte) 250;
    sourceArray2[46] = (byte) 79;
    sourceArray2[40] = (byte) 232;
    sourceArray2[41] = (byte) 192 /*0xC0*/;
    sourceArray2[42] = (byte) 184;
    sourceArray2[22] = (byte) 233;
    sourceArray2[37] = (byte) 224 /*0xE0*/;
    sourceArray2[45] = (byte) 101;
    sourceArray2[0] = (byte) 158;
    sourceArray2[34] = (byte) 110;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13061(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[5] = (byte) 87;
    sourceArray1[1] = (byte) 248;
    sourceArray1[2] = (byte) 64 /*0x40*/;
    sourceArray1[3] = (byte) 56;
    sourceArray1[34] = (byte) 76;
    sourceArray1[6] = (byte) 178;
    sourceArray1[31 /*0x1F*/] = (byte) 187;
    sourceArray1[29] = (byte) 68;
    sourceArray1[8] = (byte) 76;
    sourceArray1[9] = (byte) 211;
    sourceArray1[10] = (byte) 22;
    sourceArray1[0] = (byte) 89;
    sourceArray1[12] = (byte) 0;
    sourceArray1[13] = (byte) 199;
    sourceArray1[14] = (byte) 154;
    sourceArray1[22] = (byte) 231;
    sourceArray1[41] = (byte) 103;
    sourceArray1[17] = (byte) 204;
    sourceArray1[42] = (byte) 71;
    sourceArray1[46] = (byte) 171;
    sourceArray1[20] = (byte) 149;
    sourceArray1[45] = (byte) 234;
    sourceArray1[25] = (byte) 82;
    sourceArray1[23] = (byte) 143;
    sourceArray1[24] = (byte) 28;
    sourceArray1[27] = (byte) 171;
    sourceArray1[32 /*0x20*/] = (byte) 28;
    sourceArray1[18] = (byte) 211;
    sourceArray1[26] = (byte) 253;
    sourceArray1[11] = (byte) 149;
    sourceArray1[16 /*0x10*/] = (byte) 125;
    sourceArray1[19] = (byte) 252;
    sourceArray1[7] = (byte) 77;
    sourceArray1[33] = (byte) 76;
    sourceArray1[15] = (byte) 178;
    sourceArray1[21] = (byte) 169;
    sourceArray1[35] = (byte) 168;
    sourceArray1[37] = (byte) 191;
    sourceArray1[38] = (byte) 246;
    sourceArray1[39] = (byte) 25;
    sourceArray1[40] = (byte) 151;
    sourceArray1[36] = (byte) 168;
    sourceArray1[28] = (byte) 220;
    sourceArray1[43] = (byte) 173;
    sourceArray1[44] = (byte) 23;
    sourceArray1[30] = (byte) 155;
    sourceArray1[4] = (byte) 244;
    sourceArray1[47] = (byte) 38;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 197,
      (byte) 35,
      (byte) 233,
      (byte) 162,
      (byte) 177,
      (byte) 110,
      (byte) 18,
      (byte) 16 /*0x10*/,
      (byte) 65,
      (byte) 240 /*0xF0*/,
      (byte) 212,
      (byte) 46,
      (byte) 135,
      (byte) 226,
      (byte) 98,
      (byte) 66,
      (byte) 140,
      (byte) 40,
      (byte) 176 /*0xB0*/,
      (byte) 27,
      (byte) 148,
      (byte) 120,
      (byte) 134,
      (byte) 40,
      (byte) 191,
      (byte) 182,
      (byte) 211,
      (byte) 97,
      (byte) 196,
      (byte) 225,
      (byte) 93,
      (byte) 112 /*0x70*/,
      (byte) 39,
      (byte) 26,
      (byte) 229,
      (byte) 202,
      (byte) 120,
      (byte) 150,
      (byte) 179,
      (byte) 46,
      (byte) 113,
      (byte) 135,
      (byte) 103,
      (byte) 30,
      (byte) 87,
      (byte) 58,
      (byte) 24,
      (byte) 39
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
