// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_12545
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;


namespace ImSSP;

internal static class sc_12545
{
  internal static int ssp_appserver_12546(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[29] = (byte) 130;
    sourceArray1[1] = (byte) 225;
    sourceArray1[30] = (byte) 177;
    sourceArray1[3] = (byte) 82;
    sourceArray1[34] = (byte) 32 /*0x20*/;
    sourceArray1[19] = (byte) 38;
    sourceArray1[31 /*0x1F*/] = (byte) 217;
    sourceArray1[7] = (byte) 18;
    sourceArray1[5] = (byte) 28;
    sourceArray1[9] = (byte) 129;
    sourceArray1[43] = (byte) 191;
    sourceArray1[11] = (byte) 83;
    sourceArray1[24] = (byte) 172;
    sourceArray1[23] = (byte) 210;
    sourceArray1[20] = (byte) 126;
    sourceArray1[15] = (byte) 9;
    sourceArray1[16 /*0x10*/] = (byte) 247;
    sourceArray1[17] = (byte) 138;
    sourceArray1[18] = (byte) 116;
    sourceArray1[27] = (byte) 13;
    sourceArray1[14] = (byte) 241;
    sourceArray1[47] = (byte) 26;
    sourceArray1[22] = (byte) 14;
    sourceArray1[42] = (byte) 101;
    sourceArray1[21] = (byte) 117;
    sourceArray1[25] = (byte) 247;
    sourceArray1[26] = (byte) 16 /*0x10*/;
    sourceArray1[44] = (byte) 120;
    sourceArray1[28] = (byte) 73;
    sourceArray1[10] = (byte) 7;
    sourceArray1[13] = (byte) 174;
    sourceArray1[4] = (byte) 222;
    sourceArray1[37] = (byte) 199;
    sourceArray1[33] = (byte) 106;
    sourceArray1[39] = (byte) 168;
    sourceArray1[35] = (byte) 41;
    sourceArray1[36] = (byte) 129;
    sourceArray1[8] = (byte) 248;
    sourceArray1[12] = (byte) 28;
    sourceArray1[0] = (byte) 174;
    sourceArray1[40] = (byte) 169;
    sourceArray1[41] = (byte) 225;
    sourceArray1[32 /*0x20*/] = (byte) 47;
    sourceArray1[6] = (byte) 175;
    sourceArray1[38] = (byte) 155;
    sourceArray1[45] = (byte) 149;
    sourceArray1[46] = (byte) 170;
    sourceArray1[2] = (byte) 210;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[29] = (byte) 193;
    sourceArray2[43] = (byte) 192 /*0xC0*/;
    sourceArray2[19] = (byte) 141;
    sourceArray2[17] = (byte) 238;
    sourceArray2[2] = (byte) 24;
    sourceArray2[5] = (byte) 93;
    sourceArray2[6] = (byte) 40;
    sourceArray2[24] = (byte) 181;
    sourceArray2[8] = (byte) 48 /*0x30*/;
    sourceArray2[3] = (byte) 38;
    sourceArray2[21] = (byte) 91;
    sourceArray2[47] = (byte) 101;
    sourceArray2[10] = (byte) 167;
    sourceArray2[13] = (byte) 16 /*0x10*/;
    sourceArray2[23] = (byte) 110;
    sourceArray2[42] = (byte) 199;
    sourceArray2[39] = (byte) 158;
    sourceArray2[20] = (byte) 240 /*0xF0*/;
    sourceArray2[18] = (byte) 172;
    sourceArray2[28] = (byte) 99;
    sourceArray2[31 /*0x1F*/] = (byte) 4;
    sourceArray2[0] = (byte) 78;
    sourceArray2[22] = (byte) 211;
    sourceArray2[12] = (byte) 123;
    sourceArray2[14] = (byte) 146;
    sourceArray2[25] = (byte) 136;
    sourceArray2[37] = (byte) 147;
    sourceArray2[27] = (byte) 111;
    sourceArray2[7] = (byte) 10;
    sourceArray2[34] = (byte) 119;
    sourceArray2[30] = (byte) 53;
    sourceArray2[16 /*0x10*/] = (byte) 140;
    sourceArray2[32 /*0x20*/] = (byte) 226;
    sourceArray2[33] = (byte) 127 /*0x7F*/;
    sourceArray2[46] = (byte) 167;
    sourceArray2[35] = (byte) 207;
    sourceArray2[36] = (byte) 248;
    sourceArray2[45] = (byte) 54;
    sourceArray2[38] = (byte) 12;
    sourceArray2[41] = (byte) 235;
    sourceArray2[40] = (byte) 117;
    sourceArray2[11] = (byte) 202;
    sourceArray2[1] = (byte) 99;
    sourceArray2[9] = (byte) 239;
    sourceArray2[44] = (byte) 134;
    sourceArray2[15] = (byte) 94;
    sourceArray2[26] = (byte) 190;
    sourceArray2[4] = (byte) 159;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12547(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 134,
      (byte) 181,
      (byte) 123,
      (byte) 219,
      (byte) 1,
      (byte) 21,
      (byte) 73,
      (byte) 21,
      (byte) 229,
      (byte) 131,
      (byte) 144 /*0x90*/,
      (byte) 225,
      (byte) 169,
      (byte) 226,
      (byte) 65,
      (byte) 122,
      (byte) 9,
      (byte) 37,
      (byte) 150,
      (byte) 234,
      (byte) 70,
      (byte) 172,
      (byte) 249,
      (byte) 7,
      (byte) 161,
      (byte) 242,
      (byte) 77,
      (byte) 229,
      (byte) 19,
      (byte) 232,
      (byte) 160 /*0xA0*/,
      (byte) 42,
      (byte) 124,
      (byte) 31 /*0x1F*/,
      (byte) 169,
      (byte) 192 /*0xC0*/,
      (byte) 9,
      (byte) 68,
      (byte) 93,
      (byte) 250,
      (byte) 91,
      (byte) 62,
      (byte) 132,
      (byte) 212,
      (byte) 5,
      (byte) 144 /*0x90*/,
      (byte) 83,
      (byte) 3
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[21] = (byte) 144 /*0x90*/;
    sourceArray2[26] = (byte) 18;
    sourceArray2[35] = (byte) 208 /*0xD0*/;
    sourceArray2[3] = (byte) 243;
    sourceArray2[4] = (byte) 109;
    sourceArray2[5] = (byte) 98;
    sourceArray2[0] = (byte) 90;
    sourceArray2[7] = (byte) 124;
    sourceArray2[8] = (byte) 218;
    sourceArray2[23] = (byte) 233;
    sourceArray2[10] = (byte) 135;
    sourceArray2[11] = (byte) 164;
    sourceArray2[12] = (byte) 27;
    sourceArray2[22] = (byte) 16 /*0x10*/;
    sourceArray2[25] = (byte) 205;
    sourceArray2[2] = (byte) 238;
    sourceArray2[16 /*0x10*/] = (byte) 164;
    sourceArray2[45] = (byte) 36;
    sourceArray2[15] = (byte) 73;
    sourceArray2[19] = (byte) 199;
    sourceArray2[30] = (byte) 191;
    sourceArray2[28] = (byte) 113;
    sourceArray2[38] = (byte) 226;
    sourceArray2[37] = (byte) 130;
    sourceArray2[44] = (byte) 15;
    sourceArray2[31 /*0x1F*/] = (byte) 9;
    sourceArray2[17] = (byte) 17;
    sourceArray2[14] = (byte) 104;
    sourceArray2[13] = byte.MaxValue;
    sourceArray2[6] = (byte) 174;
    sourceArray2[29] = (byte) 154;
    sourceArray2[1] = (byte) 138;
    sourceArray2[32 /*0x20*/] = (byte) 47;
    sourceArray2[33] = (byte) 161;
    sourceArray2[34] = (byte) 99;
    sourceArray2[46] = (byte) 66;
    sourceArray2[18] = (byte) 94;
    sourceArray2[20] = (byte) 101;
    sourceArray2[9] = (byte) 64 /*0x40*/;
    sourceArray2[39] = (byte) 167;
    sourceArray2[40] = (byte) 148;
    sourceArray2[41] = (byte) 165;
    sourceArray2[24] = (byte) 97;
    sourceArray2[43] = (byte) 67;
    sourceArray2[42] = (byte) 72;
    sourceArray2[27] = (byte) 147;
    sourceArray2[36] = (byte) 31 /*0x1F*/;
    sourceArray2[47] = (byte) 208 /*0xD0*/;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
