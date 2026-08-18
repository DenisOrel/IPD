// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_12542
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;


namespace ImSSP;

internal static class sc_12542
{
  internal static int ssp_appserver_12543(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[1] = (byte) 131;
    sourceArray1[22] = (byte) 207;
    sourceArray1[2] = (byte) 32 /*0x20*/;
    sourceArray1[45] = (byte) 235;
    sourceArray1[4] = (byte) 125;
    sourceArray1[5] = (byte) 191;
    sourceArray1[6] = (byte) 157;
    sourceArray1[7] = (byte) 43;
    sourceArray1[8] = (byte) 200;
    sourceArray1[41] = (byte) 76;
    sourceArray1[10] = (byte) 163;
    sourceArray1[37] = (byte) 230;
    sourceArray1[39] = (byte) 69;
    sourceArray1[13] = (byte) 176 /*0xB0*/;
    sourceArray1[11] = (byte) 189;
    sourceArray1[31 /*0x1F*/] = (byte) 219;
    sourceArray1[26] = (byte) 96 /*0x60*/;
    sourceArray1[19] = (byte) 117;
    sourceArray1[16 /*0x10*/] = (byte) 158;
    sourceArray1[44] = (byte) 137;
    sourceArray1[20] = (byte) 27;
    sourceArray1[21] = (byte) 135;
    sourceArray1[24] = (byte) 188;
    sourceArray1[23] = (byte) 46;
    sourceArray1[15] = (byte) 215;
    sourceArray1[42] = (byte) 35;
    sourceArray1[12] = (byte) 200;
    sourceArray1[27] = (byte) 32 /*0x20*/;
    sourceArray1[33] = (byte) 218;
    sourceArray1[0] = (byte) 53;
    sourceArray1[46] = (byte) 71;
    sourceArray1[9] = (byte) 236;
    sourceArray1[32 /*0x20*/] = (byte) 73;
    sourceArray1[25] = (byte) 225;
    sourceArray1[34] = (byte) 191;
    sourceArray1[35] = (byte) 0;
    sourceArray1[30] = (byte) 1;
    sourceArray1[18] = (byte) 248;
    sourceArray1[38] = (byte) 247;
    sourceArray1[36] = (byte) 21;
    sourceArray1[17] = (byte) 153;
    sourceArray1[14] = (byte) 27;
    sourceArray1[3] = (byte) 220;
    sourceArray1[43] = (byte) 81;
    sourceArray1[40] = (byte) 167;
    sourceArray1[28] = (byte) 165;
    sourceArray1[29] = (byte) 231;
    sourceArray1[47] = (byte) 234;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[41] = (byte) 200;
    sourceArray2[40] = (byte) 244;
    sourceArray2[44] = (byte) 22;
    sourceArray2[3] = (byte) 222;
    sourceArray2[4] = (byte) 134;
    sourceArray2[46] = (byte) 76;
    sourceArray2[6] = (byte) 134;
    sourceArray2[7] = (byte) 83;
    sourceArray2[1] = (byte) 122;
    sourceArray2[9] = (byte) 133;
    sourceArray2[14] = (byte) 17;
    sourceArray2[28] = (byte) 210;
    sourceArray2[22] = (byte) 243;
    sourceArray2[33] = (byte) 222;
    sourceArray2[45] = (byte) 15;
    sourceArray2[15] = (byte) 18;
    sourceArray2[29] = (byte) 76;
    sourceArray2[10] = (byte) 102;
    sourceArray2[18] = (byte) 71;
    sourceArray2[24] = (byte) 40;
    sourceArray2[11] = (byte) 29;
    sourceArray2[17] = (byte) 3;
    sourceArray2[8] = (byte) 75;
    sourceArray2[13] = (byte) 251;
    sourceArray2[12] = (byte) 17;
    sourceArray2[21] = (byte) 120;
    sourceArray2[26] = (byte) 181;
    sourceArray2[27] = (byte) 165;
    sourceArray2[31 /*0x1F*/] = (byte) 192 /*0xC0*/;
    sourceArray2[25] = (byte) 104;
    sourceArray2[30] = (byte) 252;
    sourceArray2[19] = (byte) 37;
    sourceArray2[2] = (byte) 21;
    sourceArray2[23] = (byte) 95;
    sourceArray2[34] = (byte) 36;
    sourceArray2[35] = (byte) 254;
    sourceArray2[32 /*0x20*/] = (byte) 242;
    sourceArray2[37] = (byte) 232;
    sourceArray2[5] = (byte) 192 /*0xC0*/;
    sourceArray2[39] = (byte) 29;
    sourceArray2[0] = (byte) 97;
    sourceArray2[36] = (byte) 167;
    sourceArray2[42] = (byte) 52;
    sourceArray2[43] = (byte) 4;
    sourceArray2[20] = (byte) 26;
    sourceArray2[38] = (byte) 146;
    sourceArray2[16 /*0x10*/] = (byte) 186;
    sourceArray2[47] = (byte) 138;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12544(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[8] = (byte) 27;
    sourceArray1[9] = (byte) 242;
    sourceArray1[2] = (byte) 66;
    sourceArray1[3] = (byte) 182;
    sourceArray1[4] = (byte) 207;
    sourceArray1[35] = (byte) 180;
    sourceArray1[32 /*0x20*/] = (byte) 120;
    sourceArray1[42] = (byte) 95;
    sourceArray1[1] = (byte) 239;
    sourceArray1[23] = (byte) 100;
    sourceArray1[10] = (byte) 69;
    sourceArray1[19] = (byte) 244;
    sourceArray1[12] = (byte) 196;
    sourceArray1[30] = (byte) 185;
    sourceArray1[13] = (byte) 31 /*0x1F*/;
    sourceArray1[15] = (byte) 91;
    sourceArray1[38] = (byte) 71;
    sourceArray1[47] = (byte) 48 /*0x30*/;
    sourceArray1[18] = (byte) 105;
    sourceArray1[11] = (byte) 235;
    sourceArray1[5] = (byte) 54;
    sourceArray1[21] = (byte) 109;
    sourceArray1[22] = (byte) 186;
    sourceArray1[40] = (byte) 132;
    sourceArray1[24] = (byte) 19;
    sourceArray1[25] = (byte) 81;
    sourceArray1[14] = (byte) 5;
    sourceArray1[27] = (byte) 120;
    sourceArray1[28] = (byte) 2;
    sourceArray1[29] = (byte) 23;
    sourceArray1[43] = (byte) 141;
    sourceArray1[6] = (byte) 213;
    sourceArray1[20] = (byte) 196;
    sourceArray1[26] = (byte) 59;
    sourceArray1[34] = (byte) 207;
    sourceArray1[0] = (byte) 253;
    sourceArray1[36] = (byte) 198;
    sourceArray1[37] = (byte) 106;
    sourceArray1[7] = (byte) 20;
    sourceArray1[41] = (byte) 42;
    sourceArray1[17] = (byte) 32 /*0x20*/;
    sourceArray1[44] = (byte) 218;
    sourceArray1[16 /*0x10*/] = (byte) 41;
    sourceArray1[31 /*0x1F*/] = (byte) 198;
    sourceArray1[33] = (byte) 240 /*0xF0*/;
    sourceArray1[45] = (byte) 141;
    sourceArray1[46] = (byte) 130;
    sourceArray1[39] = (byte) 171;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 155,
      (byte) 92,
      (byte) 90,
      (byte) 114,
      (byte) 147,
      (byte) 24,
      (byte) 116,
      (byte) 96 /*0x60*/,
      (byte) 249,
      (byte) 199,
      (byte) 248,
      (byte) 62,
      (byte) 40,
      (byte) 253,
      (byte) 128 /*0x80*/,
      (byte) 168,
      (byte) 184,
      (byte) 60,
      (byte) 238,
      (byte) 90,
      (byte) 120,
      (byte) 155,
      (byte) 108,
      (byte) 50,
      (byte) 2,
      (byte) 66,
      (byte) 254,
      (byte) 155,
      (byte) 205,
      (byte) 141,
      (byte) 220,
      (byte) 159,
      (byte) 160 /*0xA0*/,
      (byte) 187,
      (byte) 238,
      (byte) 94,
      (byte) 175,
      (byte) 197,
      (byte) 143,
      (byte) 248,
      byte.MaxValue,
      (byte) 171,
      (byte) 84,
      (byte) 41,
      (byte) 168,
      (byte) 6,
      (byte) 23,
      (byte) 125
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
