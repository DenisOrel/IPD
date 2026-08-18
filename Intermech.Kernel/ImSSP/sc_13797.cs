// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_13797
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;


namespace ImSSP;

internal static class sc_13797
{
  internal static int ssp_appserver_13798(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[5] = (byte) 137;
    sourceArray1[18] = (byte) 110;
    sourceArray1[2] = (byte) 92;
    sourceArray1[17] = (byte) 134;
    sourceArray1[32 /*0x20*/] = (byte) 243;
    sourceArray1[29] = (byte) 145;
    sourceArray1[6] = (byte) 194;
    sourceArray1[7] = (byte) 125;
    sourceArray1[37] = (byte) 177;
    sourceArray1[40] = (byte) 108;
    sourceArray1[10] = (byte) 201;
    sourceArray1[11] = (byte) 147;
    sourceArray1[43] = (byte) 176 /*0xB0*/;
    sourceArray1[12] = (byte) 16 /*0x10*/;
    sourceArray1[14] = (byte) 44;
    sourceArray1[20] = (byte) 227;
    sourceArray1[33] = (byte) 109;
    sourceArray1[28] = (byte) 17;
    sourceArray1[36] = (byte) 85;
    sourceArray1[0] = (byte) 64 /*0x40*/;
    sourceArray1[27] = (byte) 236;
    sourceArray1[21] = (byte) 131;
    sourceArray1[22] = (byte) 109;
    sourceArray1[44] = (byte) 29;
    sourceArray1[39] = (byte) 19;
    sourceArray1[25] = (byte) 2;
    sourceArray1[26] = (byte) 114;
    sourceArray1[24] = (byte) 6;
    sourceArray1[13] = (byte) 26;
    sourceArray1[16 /*0x10*/] = (byte) 83;
    sourceArray1[30] = (byte) 22;
    sourceArray1[31 /*0x1F*/] = (byte) 233;
    sourceArray1[46] = (byte) 201;
    sourceArray1[45] = (byte) 104;
    sourceArray1[34] = (byte) 195;
    sourceArray1[35] = (byte) 115;
    sourceArray1[8] = (byte) 138;
    sourceArray1[15] = (byte) 147;
    sourceArray1[9] = (byte) 207;
    sourceArray1[47] = (byte) 107;
    sourceArray1[1] = (byte) 134;
    sourceArray1[4] = (byte) 197;
    sourceArray1[42] = (byte) 218;
    sourceArray1[19] = (byte) 48 /*0x30*/;
    sourceArray1[41] = (byte) 46;
    sourceArray1[38] = (byte) 114;
    sourceArray1[3] = (byte) 21;
    sourceArray1[23] = (byte) 179;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[1] = (byte) 129;
    sourceArray2[12] = (byte) 233;
    sourceArray2[2] = (byte) 232;
    sourceArray2[23] = (byte) 124;
    sourceArray2[4] = (byte) 63 /*0x3F*/;
    sourceArray2[5] = (byte) 0;
    sourceArray2[6] = (byte) 188;
    sourceArray2[21] = (byte) 222;
    sourceArray2[8] = (byte) 246;
    sourceArray2[35] = (byte) 41;
    sourceArray2[29] = (byte) 55;
    sourceArray2[7] = (byte) 25;
    sourceArray2[45] = (byte) 79;
    sourceArray2[41] = (byte) 241;
    sourceArray2[14] = (byte) 81;
    sourceArray2[32 /*0x20*/] = (byte) 44;
    sourceArray2[20] = (byte) 33;
    sourceArray2[17] = (byte) 65;
    sourceArray2[18] = (byte) 161;
    sourceArray2[19] = (byte) 119;
    sourceArray2[36] = (byte) 250;
    sourceArray2[46] = (byte) 203;
    sourceArray2[15] = (byte) 80 /*0x50*/;
    sourceArray2[33] = (byte) 58;
    sourceArray2[24] = (byte) 88;
    sourceArray2[47] = (byte) 200;
    sourceArray2[39] = (byte) 98;
    sourceArray2[27] = (byte) 126;
    sourceArray2[28] = (byte) 32 /*0x20*/;
    sourceArray2[11] = (byte) 147;
    sourceArray2[30] = (byte) 32 /*0x20*/;
    sourceArray2[31 /*0x1F*/] = (byte) 26;
    sourceArray2[10] = (byte) 84;
    sourceArray2[26] = (byte) 66;
    sourceArray2[34] = (byte) 253;
    sourceArray2[25] = (byte) 240 /*0xF0*/;
    sourceArray2[13] = (byte) 214;
    sourceArray2[16 /*0x10*/] = (byte) 67;
    sourceArray2[38] = (byte) 23;
    sourceArray2[9] = (byte) 21;
    sourceArray2[3] = (byte) 244;
    sourceArray2[22] = (byte) 89;
    sourceArray2[42] = (byte) 171;
    sourceArray2[43] = (byte) 14;
    sourceArray2[44] = (byte) 137;
    sourceArray2[0] = (byte) 100;
    sourceArray2[40] = (byte) 177;
    sourceArray2[37] = (byte) 155;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13799(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[35] = (byte) 152;
    sourceArray1[42] = (byte) 47;
    sourceArray1[2] = (byte) 111;
    sourceArray1[28] = (byte) 122;
    sourceArray1[1] = (byte) 147;
    sourceArray1[6] = (byte) 186;
    sourceArray1[8] = (byte) 195;
    sourceArray1[20] = (byte) 121;
    sourceArray1[30] = (byte) 96 /*0x60*/;
    sourceArray1[44] = (byte) 54;
    sourceArray1[4] = (byte) 62;
    sourceArray1[9] = (byte) 0;
    sourceArray1[15] = (byte) 37;
    sourceArray1[13] = (byte) 146;
    sourceArray1[14] = (byte) 112 /*0x70*/;
    sourceArray1[33] = (byte) 63 /*0x3F*/;
    sourceArray1[22] = (byte) 142;
    sourceArray1[17] = (byte) 149;
    sourceArray1[18] = (byte) 188;
    sourceArray1[11] = (byte) 57;
    sourceArray1[46] = (byte) 97;
    sourceArray1[21] = (byte) 29;
    sourceArray1[25] = (byte) 185;
    sourceArray1[23] = (byte) 243;
    sourceArray1[24] = (byte) 197;
    sourceArray1[47] = (byte) 23;
    sourceArray1[26] = (byte) 154;
    sourceArray1[27] = (byte) 24;
    sourceArray1[38] = (byte) 135;
    sourceArray1[29] = (byte) 94;
    sourceArray1[36] = (byte) 131;
    sourceArray1[31 /*0x1F*/] = (byte) 247;
    sourceArray1[32 /*0x20*/] = (byte) 205;
    sourceArray1[41] = (byte) 237;
    sourceArray1[5] = (byte) 248;
    sourceArray1[3] = (byte) 157;
    sourceArray1[7] = (byte) 36;
    sourceArray1[37] = (byte) 42;
    sourceArray1[0] = (byte) 236;
    sourceArray1[39] = (byte) 95;
    sourceArray1[40] = (byte) 27;
    sourceArray1[45] = (byte) 68;
    sourceArray1[16 /*0x10*/] = (byte) 75;
    sourceArray1[43] = (byte) 115;
    sourceArray1[19] = (byte) 155;
    sourceArray1[12] = (byte) 157;
    sourceArray1[34] = (byte) 114;
    sourceArray1[10] = (byte) 41;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 195,
      (byte) 109,
      (byte) 163,
      (byte) 228,
      (byte) 201,
      (byte) 42,
      (byte) 40,
      (byte) 22,
      (byte) 189,
      (byte) 200,
      (byte) 172,
      (byte) 153,
      (byte) 93,
      (byte) 185,
      (byte) 100,
      (byte) 73,
      (byte) 4,
      (byte) 240 /*0xF0*/,
      (byte) 33,
      (byte) 59,
      (byte) 148,
      (byte) 191,
      (byte) 221,
      (byte) 54,
      (byte) 83,
      (byte) 155,
      (byte) 207,
      (byte) 54,
      (byte) 160 /*0xA0*/,
      (byte) 212,
      (byte) 126,
      (byte) 192 /*0xC0*/,
      (byte) 1,
      (byte) 127 /*0x7F*/,
      (byte) 84,
      (byte) 117,
      byte.MaxValue,
      (byte) 116,
      (byte) 156,
      (byte) 16 /*0x10*/,
      (byte) 139,
      (byte) 7,
      (byte) 173,
      (byte) 222,
      (byte) 148,
      (byte) 181,
      (byte) 200,
      (byte) 181
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
