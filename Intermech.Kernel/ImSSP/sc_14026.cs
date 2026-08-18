// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_14026
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;


namespace ImSSP;

internal static class sc_14026
{
  internal static int ssp_appserver_14027(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[13] = (byte) 177;
    sourceArray1[17] = (byte) 225;
    sourceArray1[2] = (byte) 145;
    sourceArray1[32 /*0x20*/] = (byte) 23;
    sourceArray1[10] = (byte) 28;
    sourceArray1[5] = (byte) 80 /*0x50*/;
    sourceArray1[6] = (byte) 62;
    sourceArray1[33] = (byte) 206;
    sourceArray1[41] = (byte) 233;
    sourceArray1[9] = (byte) 19;
    sourceArray1[8] = (byte) 239;
    sourceArray1[11] = (byte) 104;
    sourceArray1[31 /*0x1F*/] = (byte) 179;
    sourceArray1[24] = (byte) 186;
    sourceArray1[14] = (byte) 37;
    sourceArray1[30] = (byte) 100;
    sourceArray1[15] = (byte) 11;
    sourceArray1[43] = (byte) 112 /*0x70*/;
    sourceArray1[18] = (byte) 16 /*0x10*/;
    sourceArray1[19] = (byte) 177;
    sourceArray1[34] = (byte) 113;
    sourceArray1[21] = (byte) 134;
    sourceArray1[22] = (byte) 199;
    sourceArray1[23] = (byte) 142;
    sourceArray1[36] = (byte) 191;
    sourceArray1[16 /*0x10*/] = (byte) 85;
    sourceArray1[26] = (byte) 205;
    sourceArray1[27] = (byte) 253;
    sourceArray1[28] = (byte) 104;
    sourceArray1[1] = (byte) 25;
    sourceArray1[45] = (byte) 229;
    sourceArray1[39] = (byte) 254;
    sourceArray1[38] = (byte) 27;
    sourceArray1[37] = (byte) 101;
    sourceArray1[25] = (byte) 63 /*0x3F*/;
    sourceArray1[20] = (byte) 252;
    sourceArray1[12] = (byte) 137;
    sourceArray1[0] = (byte) 33;
    sourceArray1[7] = (byte) 234;
    sourceArray1[4] = (byte) 175;
    sourceArray1[29] = (byte) 60;
    sourceArray1[35] = (byte) 52;
    sourceArray1[42] = (byte) 62;
    sourceArray1[40] = (byte) 120;
    sourceArray1[44] = (byte) 217;
    sourceArray1[3] = (byte) 206;
    sourceArray1[46] = (byte) 101;
    sourceArray1[47] = (byte) 26;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[33] = (byte) 140;
    sourceArray2[0] = (byte) 166;
    sourceArray2[20] = (byte) 181;
    sourceArray2[26] = (byte) 58;
    sourceArray2[4] = (byte) 85;
    sourceArray2[10] = (byte) 220;
    sourceArray2[31 /*0x1F*/] = (byte) 237;
    sourceArray2[7] = (byte) 34;
    sourceArray2[8] = (byte) 135;
    sourceArray2[21] = (byte) 60;
    sourceArray2[35] = (byte) 231;
    sourceArray2[46] = (byte) 45;
    sourceArray2[12] = (byte) 145;
    sourceArray2[3] = (byte) 89;
    sourceArray2[16 /*0x10*/] = (byte) 209;
    sourceArray2[13] = (byte) 56;
    sourceArray2[1] = (byte) 131;
    sourceArray2[17] = (byte) 26;
    sourceArray2[18] = (byte) 204;
    sourceArray2[19] = (byte) 126;
    sourceArray2[5] = (byte) 213;
    sourceArray2[42] = (byte) 132;
    sourceArray2[22] = (byte) 102;
    sourceArray2[41] = (byte) 21;
    sourceArray2[14] = (byte) 175;
    sourceArray2[25] = (byte) 201;
    sourceArray2[24] = (byte) 225;
    sourceArray2[27] = (byte) 221;
    sourceArray2[40] = (byte) 215;
    sourceArray2[37] = (byte) 222;
    sourceArray2[30] = (byte) 82;
    sourceArray2[11] = (byte) 60;
    sourceArray2[44] = (byte) 11;
    sourceArray2[43] = (byte) 199;
    sourceArray2[32 /*0x20*/] = (byte) 221;
    sourceArray2[47] = (byte) 52;
    sourceArray2[36] = (byte) 135;
    sourceArray2[9] = (byte) 59;
    sourceArray2[38] = (byte) 138;
    sourceArray2[39] = (byte) 246;
    sourceArray2[15] = (byte) 183;
    sourceArray2[28] = (byte) 193;
    sourceArray2[34] = (byte) 97;
    sourceArray2[2] = (byte) 111;
    sourceArray2[6] = (byte) 196;
    sourceArray2[45] = (byte) 65;
    sourceArray2[23] = (byte) 15;
    sourceArray2[29] = (byte) 213;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_14028(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 173,
      (byte) 12,
      (byte) 253,
      (byte) 178,
      (byte) 135,
      (byte) 68,
      (byte) 53,
      (byte) 136,
      (byte) 114,
      (byte) 230,
      (byte) 116,
      (byte) 212,
      (byte) 16 /*0x10*/,
      (byte) 206,
      (byte) 234,
      (byte) 117,
      (byte) 150,
      (byte) 101,
      (byte) 10,
      (byte) 254,
      (byte) 103,
      (byte) 246,
      (byte) 11,
      (byte) 84,
      (byte) 115,
      (byte) 239,
      (byte) 84,
      (byte) 86,
      (byte) 168,
      (byte) 228,
      (byte) 41,
      (byte) 33,
      (byte) 140,
      (byte) 5,
      (byte) 233,
      (byte) 244,
      (byte) 173,
      (byte) 10,
      (byte) 14,
      byte.MaxValue,
      (byte) 12,
      (byte) 167,
      (byte) 102,
      (byte) 56,
      (byte) 160 /*0xA0*/,
      (byte) 145,
      (byte) 57,
      (byte) 9
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 2,
      (byte) 211,
      (byte) 194,
      (byte) 85,
      (byte) 35,
      (byte) 75,
      (byte) 40,
      (byte) 151,
      (byte) 43,
      (byte) 244,
      (byte) 247,
      (byte) 142,
      (byte) 119,
      (byte) 16 /*0x10*/,
      (byte) 96 /*0x60*/,
      (byte) 141,
      (byte) 76,
      (byte) 36,
      (byte) 8,
      (byte) 222,
      (byte) 9,
      (byte) 78,
      (byte) 93,
      (byte) 189,
      (byte) 40,
      (byte) 40,
      (byte) 0,
      (byte) 164,
      (byte) 228,
      (byte) 121,
      (byte) 224 /*0xE0*/,
      (byte) 249,
      (byte) 98,
      (byte) 33,
      (byte) 82,
      (byte) 86,
      (byte) 42,
      (byte) 254,
      (byte) 0,
      (byte) 173,
      (byte) 204,
      (byte) 86,
      (byte) 172,
      (byte) 155,
      (byte) 2,
      (byte) 202,
      (byte) 159,
      (byte) 89
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_14029(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[30] = (byte) 165;
    sourceArray1[47] = (byte) 74;
    sourceArray1[11] = (byte) 189;
    sourceArray1[3] = (byte) 0;
    sourceArray1[4] = (byte) 169;
    sourceArray1[2] = (byte) 182;
    sourceArray1[45] = (byte) 123;
    sourceArray1[26] = (byte) 225;
    sourceArray1[44] = (byte) 216;
    sourceArray1[9] = (byte) 223;
    sourceArray1[10] = (byte) 141;
    sourceArray1[15] = (byte) 229;
    sourceArray1[12] = (byte) 83;
    sourceArray1[13] = (byte) 71;
    sourceArray1[6] = (byte) 151;
    sourceArray1[16 /*0x10*/] = (byte) 245;
    sourceArray1[14] = (byte) 10;
    sourceArray1[20] = (byte) 11;
    sourceArray1[8] = (byte) 222;
    sourceArray1[19] = (byte) 86;
    sourceArray1[22] = (byte) 192 /*0xC0*/;
    sourceArray1[21] = (byte) 242;
    sourceArray1[43] = (byte) 199;
    sourceArray1[23] = (byte) 143;
    sourceArray1[35] = (byte) 88;
    sourceArray1[38] = (byte) 4;
    sourceArray1[1] = (byte) 173;
    sourceArray1[27] = (byte) 182;
    sourceArray1[28] = (byte) 75;
    sourceArray1[29] = (byte) 57;
    sourceArray1[37] = (byte) 243;
    sourceArray1[31 /*0x1F*/] = (byte) 247;
    sourceArray1[42] = (byte) 46;
    sourceArray1[33] = (byte) 189;
    sourceArray1[34] = (byte) 121;
    sourceArray1[17] = (byte) 199;
    sourceArray1[36] = (byte) 50;
    sourceArray1[18] = (byte) 139;
    sourceArray1[5] = (byte) 63 /*0x3F*/;
    sourceArray1[39] = (byte) 203;
    sourceArray1[40] = (byte) 119;
    sourceArray1[41] = (byte) 15;
    sourceArray1[25] = (byte) 179;
    sourceArray1[46] = (byte) 77;
    sourceArray1[7] = (byte) 239;
    sourceArray1[32 /*0x20*/] = (byte) 167;
    sourceArray1[0] = (byte) 19;
    sourceArray1[24] = (byte) 210;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[5] = (byte) 60;
    sourceArray2[21] = (byte) 133;
    sourceArray2[45] = (byte) 14;
    sourceArray2[9] = (byte) 125;
    sourceArray2[44] = (byte) 157;
    sourceArray2[34] = (byte) 155;
    sourceArray2[6] = (byte) 169;
    sourceArray2[7] = (byte) 224 /*0xE0*/;
    sourceArray2[28] = (byte) 81;
    sourceArray2[29] = (byte) 183;
    sourceArray2[26] = (byte) 238;
    sourceArray2[30] = (byte) 80 /*0x50*/;
    sourceArray2[12] = (byte) 124;
    sourceArray2[25] = (byte) 115;
    sourceArray2[37] = (byte) 212;
    sourceArray2[15] = (byte) 127 /*0x7F*/;
    sourceArray2[14] = (byte) 52;
    sourceArray2[17] = (byte) 186;
    sourceArray2[18] = (byte) 192 /*0xC0*/;
    sourceArray2[19] = (byte) 82;
    sourceArray2[8] = (byte) 119;
    sourceArray2[40] = (byte) 242;
    sourceArray2[32 /*0x20*/] = (byte) 60;
    sourceArray2[23] = (byte) 201;
    sourceArray2[24] = (byte) 15;
    sourceArray2[0] = (byte) 219;
    sourceArray2[38] = (byte) 223;
    sourceArray2[22] = (byte) 114;
    sourceArray2[11] = (byte) 161;
    sourceArray2[1] = (byte) 59;
    sourceArray2[27] = (byte) 229;
    sourceArray2[47] = (byte) 91;
    sourceArray2[20] = (byte) 98;
    sourceArray2[33] = (byte) 6;
    sourceArray2[10] = (byte) 64 /*0x40*/;
    sourceArray2[35] = (byte) 2;
    sourceArray2[36] = (byte) 19;
    sourceArray2[13] = (byte) 190;
    sourceArray2[31 /*0x1F*/] = (byte) 91;
    sourceArray2[39] = (byte) 81;
    sourceArray2[4] = (byte) 74;
    sourceArray2[16 /*0x10*/] = (byte) 124;
    sourceArray2[42] = (byte) 214;
    sourceArray2[41] = (byte) 163;
    sourceArray2[2] = (byte) 121;
    sourceArray2[3] = (byte) 158;
    sourceArray2[46] = (byte) 44;
    sourceArray2[43] = (byte) 137;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
