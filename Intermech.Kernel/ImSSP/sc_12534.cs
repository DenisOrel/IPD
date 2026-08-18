// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_12534
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_12534
{
  internal static int ssp_appserver_12535(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 204,
      (byte) 69,
      (byte) 221,
      (byte) 190,
      (byte) 80 /*0x50*/,
      (byte) 237,
      (byte) 252,
      (byte) 56,
      (byte) 196,
      (byte) 131,
      (byte) 55,
      (byte) 39,
      (byte) 29,
      (byte) 250,
      (byte) 249,
      (byte) 217,
      (byte) 119,
      (byte) 13,
      (byte) 115,
      (byte) 231,
      (byte) 197,
      (byte) 46,
      (byte) 87,
      (byte) 28,
      (byte) 33,
      (byte) 130,
      (byte) 113,
      (byte) 237,
      (byte) 216,
      (byte) 18,
      (byte) 70,
      (byte) 100,
      (byte) 53,
      (byte) 55,
      (byte) 131,
      (byte) 187,
      (byte) 112 /*0x70*/,
      (byte) 165,
      (byte) 118,
      (byte) 61,
      (byte) 243,
      (byte) 118,
      (byte) 179,
      (byte) 126,
      (byte) 40,
      (byte) 178,
      (byte) 147,
      (byte) 193
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[10] = (byte) 23;
    sourceArray2[37] = (byte) 99;
    sourceArray2[2] = (byte) 227;
    sourceArray2[3] = (byte) 141;
    sourceArray2[28] = (byte) 53;
    sourceArray2[22] = (byte) 157;
    sourceArray2[30] = (byte) 213;
    sourceArray2[7] = (byte) 30;
    sourceArray2[41] = (byte) 51;
    sourceArray2[9] = (byte) 57;
    sourceArray2[40] = (byte) 96 /*0x60*/;
    sourceArray2[47] = (byte) 81;
    sourceArray2[12] = (byte) 205;
    sourceArray2[19] = (byte) 35;
    sourceArray2[14] = (byte) 237;
    sourceArray2[18] = (byte) 182;
    sourceArray2[26] = (byte) 157;
    sourceArray2[17] = (byte) 157;
    sourceArray2[25] = (byte) 82;
    sourceArray2[23] = (byte) 158;
    sourceArray2[20] = (byte) 69;
    sourceArray2[21] = (byte) 174;
    sourceArray2[36] = (byte) 117;
    sourceArray2[32 /*0x20*/] = (byte) 197;
    sourceArray2[11] = (byte) 8;
    sourceArray2[4] = (byte) 114;
    sourceArray2[8] = (byte) 13;
    sourceArray2[27] = (byte) 123;
    sourceArray2[43] = (byte) 145;
    sourceArray2[29] = (byte) 170;
    sourceArray2[6] = (byte) 9;
    sourceArray2[46] = (byte) 212;
    sourceArray2[44] = (byte) 241;
    sourceArray2[13] = (byte) 162;
    sourceArray2[16 /*0x10*/] = (byte) 165;
    sourceArray2[35] = (byte) 108;
    sourceArray2[0] = (byte) 2;
    sourceArray2[33] = (byte) 117;
    sourceArray2[15] = (byte) 175;
    sourceArray2[39] = (byte) 235;
    sourceArray2[31 /*0x1F*/] = (byte) 110;
    sourceArray2[34] = (byte) 221;
    sourceArray2[42] = (byte) 189;
    sourceArray2[5] = (byte) 116;
    sourceArray2[1] = (byte) 73;
    sourceArray2[45] = (byte) 86;
    sourceArray2[24] = (byte) 20;
    sourceArray2[38] = (byte) 200;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_12536()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[32 /*0x20*/];
      byte[] numArray2 = new byte[32 /*0x20*/];
      numArray2[14] = (byte) 222;
      numArray2[11] = (byte) 20;
      numArray2[9] = (byte) 67;
      numArray2[28] = (byte) 116;
      numArray2[0] = (byte) 192 /*0xC0*/;
      numArray2[5] = (byte) 181;
      numArray2[3] = (byte) 49;
      numArray2[1] = (byte) 173;
      numArray2[4] = (byte) 36;
      numArray2[25] = (byte) 186;
      numArray2[8] = (byte) 244;
      numArray2[26] = (byte) 201;
      numArray2[2] = (byte) 74;
      numArray2[13] = (byte) 46;
      numArray2[20] = (byte) 165;
      numArray2[15] = (byte) 203;
      numArray2[16 /*0x10*/] = (byte) 145;
      numArray2[17] = (byte) 21;
      numArray2[18] = (byte) 38;
      numArray2[19] = (byte) 138;
      numArray2[22] = (byte) 64 /*0x40*/;
      numArray2[21] = (byte) 165;
      numArray2[10] = (byte) 35;
      numArray2[23] = (byte) 146;
      numArray2[24] = (byte) 111;
      numArray2[6] = (byte) 253;
      numArray2[31 /*0x1F*/] = (byte) 121;
      numArray2[27] = (byte) 237;
      numArray2[12] = (byte) 205;
      numArray2[29] = (byte) 15;
      numArray2[30] = (byte) 56;
      numArray2[7] = (byte) 0;
      byte[] numArray3 = new byte[32 /*0x20*/];
      numArray3[14] = (byte) 226;
      numArray3[1] = (byte) 142;
      numArray3[24] = (byte) 57;
      numArray3[3] = (byte) 204;
      numArray3[13] = (byte) 133;
      numArray3[5] = (byte) 100;
      numArray3[6] = (byte) 190;
      numArray3[0] = (byte) 226;
      numArray3[10] = (byte) 187;
      numArray3[31 /*0x1F*/] = (byte) 226;
      numArray3[22] = (byte) 83;
      numArray3[19] = (byte) 204;
      numArray3[12] = (byte) 240 /*0xF0*/;
      numArray3[7] = (byte) 177;
      numArray3[18] = (byte) 64 /*0x40*/;
      numArray3[15] = (byte) 130;
      numArray3[2] = (byte) 74;
      numArray3[17] = (byte) 90;
      numArray3[29] = (byte) 111;
      numArray3[26] = (byte) 172;
      numArray3[20] = (byte) 251;
      numArray3[11] = (byte) 96 /*0x60*/;
      numArray3[30] = (byte) 252;
      numArray3[23] = (byte) 191;
      numArray3[8] = (byte) 68;
      numArray3[21] = (byte) 29;
      numArray3[9] = (byte) 116;
      numArray3[27] = (byte) 79;
      numArray3[28] = (byte) 246;
      numArray3[4] = (byte) 199;
      numArray3[16 /*0x10*/] = (byte) 110;
      numArray3[25] = (byte) 249;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 32 /*0x20*/);
      for (int index = 0; index < 32 /*0x20*/; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[32 /*0x20*/];
    byte[] numArray5 = new byte[32 /*0x20*/]
    {
      (byte) 22,
      (byte) 60,
      (byte) 177,
      (byte) 91,
      (byte) 44,
      (byte) 144 /*0x90*/,
      (byte) 14,
      (byte) 17,
      (byte) 137,
      (byte) 224 /*0xE0*/,
      (byte) 53,
      (byte) 205,
      (byte) 140,
      (byte) 230,
      (byte) 11,
      (byte) 143,
      (byte) 253,
      (byte) 236,
      (byte) 14,
      (byte) 41,
      (byte) 211,
      (byte) 12,
      (byte) 155,
      (byte) 64 /*0x40*/,
      (byte) 235,
      (byte) 200,
      (byte) 184,
      (byte) 185,
      (byte) 50,
      (byte) 104,
      (byte) 48 /*0x30*/,
      (byte) 217
    };
    byte[] numArray6 = new byte[32 /*0x20*/]
    {
      (byte) 202,
      (byte) 180,
      (byte) 138,
      (byte) 196,
      (byte) 231,
      (byte) 202,
      (byte) 44,
      (byte) 18,
      (byte) 67,
      (byte) 78,
      (byte) 97,
      (byte) 194,
      (byte) 45,
      (byte) 183,
      (byte) 104,
      (byte) 48 /*0x30*/,
      (byte) 70,
      (byte) 23,
      (byte) 54,
      (byte) 243,
      (byte) 45,
      (byte) 24,
      (byte) 138,
      (byte) 125,
      (byte) 53,
      (byte) 176 /*0xB0*/,
      (byte) 72,
      (byte) 36,
      (byte) 116,
      (byte) 96 /*0x60*/,
      (byte) 205,
      (byte) 151
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 32 /*0x20*/);
    for (int index = 0; index < 32 /*0x20*/; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static int ssp_appserver_12537(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 147,
      (byte) 246,
      (byte) 237,
      (byte) 173,
      (byte) 39,
      (byte) 19,
      (byte) 1,
      (byte) 185,
      (byte) 197,
      (byte) 72,
      (byte) 198,
      (byte) 127 /*0x7F*/,
      (byte) 45,
      (byte) 235,
      (byte) 185,
      (byte) 144 /*0x90*/,
      (byte) 72,
      (byte) 130,
      (byte) 9,
      (byte) 160 /*0xA0*/,
      (byte) 113,
      (byte) 111,
      (byte) 100,
      (byte) 215,
      (byte) 84,
      (byte) 222,
      (byte) 33,
      (byte) 176 /*0xB0*/,
      (byte) 91,
      (byte) 242,
      (byte) 2,
      (byte) 44,
      (byte) 144 /*0x90*/,
      (byte) 104,
      (byte) 106,
      (byte) 245,
      (byte) 33,
      (byte) 124,
      (byte) 171,
      (byte) 249,
      (byte) 154,
      (byte) 135,
      (byte) 37,
      (byte) 128 /*0x80*/,
      (byte) 241,
      (byte) 58,
      (byte) 4,
      (byte) 124
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 135,
      (byte) 240 /*0xF0*/,
      (byte) 28,
      (byte) 247,
      (byte) 207,
      (byte) 170,
      (byte) 13,
      (byte) 13,
      (byte) 163,
      (byte) 86,
      (byte) 95,
      (byte) 5,
      (byte) 134,
      (byte) 139,
      (byte) 205,
      (byte) 112 /*0x70*/,
      (byte) 237,
      (byte) 138,
      (byte) 165,
      (byte) 234,
      (byte) 239,
      (byte) 198,
      (byte) 52,
      (byte) 192 /*0xC0*/,
      (byte) 24,
      (byte) 113,
      (byte) 246,
      (byte) 19,
      (byte) 163,
      (byte) 82,
      (byte) 164,
      (byte) 236,
      (byte) 15,
      (byte) 120,
      (byte) 29,
      (byte) 24,
      (byte) 138,
      (byte) 94,
      (byte) 21,
      (byte) 110,
      (byte) 231,
      (byte) 222,
      (byte) 182,
      (byte) 213,
      (byte) 122,
      (byte) 158,
      (byte) 178,
      (byte) 58
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12538(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[29] = (byte) 57;
    sourceArray1[28] = (byte) 164;
    sourceArray1[2] = (byte) 36;
    sourceArray1[3] = (byte) 146;
    sourceArray1[46] = (byte) 87;
    sourceArray1[7] = (byte) 242;
    sourceArray1[31 /*0x1F*/] = (byte) 95;
    sourceArray1[19] = (byte) 215;
    sourceArray1[45] = (byte) 58;
    sourceArray1[32 /*0x20*/] = (byte) 251;
    sourceArray1[4] = (byte) 21;
    sourceArray1[11] = (byte) 69;
    sourceArray1[8] = (byte) 80 /*0x50*/;
    sourceArray1[25] = (byte) 230;
    sourceArray1[14] = (byte) 197;
    sourceArray1[15] = (byte) 129;
    sourceArray1[16 /*0x10*/] = (byte) 196;
    sourceArray1[17] = (byte) 111;
    sourceArray1[9] = (byte) 93;
    sourceArray1[10] = (byte) 211;
    sourceArray1[20] = (byte) 16 /*0x10*/;
    sourceArray1[21] = (byte) 6;
    sourceArray1[42] = (byte) 73;
    sourceArray1[23] = (byte) 73;
    sourceArray1[47] = (byte) 75;
    sourceArray1[26] = (byte) 26;
    sourceArray1[24] = (byte) 228;
    sourceArray1[27] = (byte) 217;
    sourceArray1[22] = (byte) 234;
    sourceArray1[40] = (byte) 99;
    sourceArray1[30] = (byte) 18;
    sourceArray1[0] = (byte) 217;
    sourceArray1[6] = (byte) 72;
    sourceArray1[33] = (byte) 123;
    sourceArray1[34] = (byte) 183;
    sourceArray1[35] = (byte) 178;
    sourceArray1[13] = (byte) 238;
    sourceArray1[12] = (byte) 154;
    sourceArray1[36] = (byte) 70;
    sourceArray1[39] = (byte) 35;
    sourceArray1[38] = (byte) 11;
    sourceArray1[41] = (byte) 6;
    sourceArray1[18] = (byte) 203;
    sourceArray1[43] = (byte) 41;
    sourceArray1[44] = (byte) 82;
    sourceArray1[37] = (byte) 99;
    sourceArray1[5] = (byte) 89;
    sourceArray1[1] = (byte) 181;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 19,
      (byte) 120,
      (byte) 194,
      (byte) 110,
      (byte) 192 /*0xC0*/,
      (byte) 221,
      (byte) 116,
      (byte) 1,
      (byte) 78,
      (byte) 253,
      (byte) 223,
      (byte) 169,
      (byte) 220,
      (byte) 235,
      (byte) 213,
      (byte) 88,
      (byte) 228,
      (byte) 204,
      (byte) 53,
      (byte) 244,
      (byte) 40,
      (byte) 15,
      (byte) 36,
      (byte) 13,
      (byte) 19,
      (byte) 151,
      (byte) 245,
      (byte) 28,
      (byte) 66,
      (byte) 115,
      (byte) 113,
      (byte) 134,
      (byte) 20,
      (byte) 185,
      (byte) 253,
      (byte) 110,
      (byte) 168,
      (byte) 159,
      (byte) 26,
      (byte) 176 /*0xB0*/,
      (byte) 189,
      (byte) 143,
      (byte) 110,
      (byte) 221,
      (byte) 104,
      (byte) 210,
      (byte) 172,
      (byte) 247
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_12539()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[27];
      byte[] numArray2 = new byte[27];
      numArray2[25] = (byte) 207;
      numArray2[1] = (byte) 55;
      numArray2[21] = (byte) 126;
      numArray2[4] = (byte) 192 /*0xC0*/;
      numArray2[6] = (byte) 116;
      numArray2[26] = (byte) 171;
      numArray2[14] = (byte) 159;
      numArray2[7] = (byte) 30;
      numArray2[8] = (byte) 188;
      numArray2[9] = (byte) 150;
      numArray2[0] = (byte) 241;
      numArray2[11] = (byte) 10;
      numArray2[12] = (byte) 227;
      numArray2[13] = (byte) 3;
      numArray2[5] = (byte) 81;
      numArray2[15] = (byte) 112 /*0x70*/;
      numArray2[18] = (byte) 145;
      numArray2[10] = (byte) 22;
      numArray2[20] = (byte) 220;
      numArray2[3] = (byte) 45;
      numArray2[19] = (byte) 16 /*0x10*/;
      numArray2[2] = (byte) 20;
      numArray2[22] = (byte) 68;
      numArray2[23] = (byte) 83;
      numArray2[24] = (byte) 43;
      numArray2[17] = (byte) 164;
      numArray2[16 /*0x10*/] = (byte) 212;
      byte[] numArray3 = new byte[27];
      numArray3[14] = (byte) 9;
      numArray3[25] = (byte) 102;
      numArray3[5] = (byte) 232;
      numArray3[7] = (byte) 18;
      numArray3[0] = (byte) 183;
      numArray3[18] = (byte) 98;
      numArray3[6] = (byte) 56;
      numArray3[22] = (byte) 105;
      numArray3[11] = (byte) 86;
      numArray3[20] = (byte) 236;
      numArray3[10] = (byte) 2;
      numArray3[2] = (byte) 154;
      numArray3[12] = (byte) 112 /*0x70*/;
      numArray3[13] = (byte) 9;
      numArray3[17] = (byte) 127 /*0x7F*/;
      numArray3[15] = (byte) 131;
      numArray3[4] = (byte) 235;
      numArray3[23] = (byte) 243;
      numArray3[9] = (byte) 127 /*0x7F*/;
      numArray3[19] = (byte) 136;
      numArray3[1] = (byte) 248;
      numArray3[21] = (byte) 211;
      numArray3[24] = (byte) 59;
      numArray3[26] = (byte) 148;
      numArray3[8] = (byte) 155;
      numArray3[16 /*0x10*/] = (byte) 108;
      numArray3[3] = (byte) 92;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 27);
      for (int index = 0; index < 27; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[27];
    byte[] numArray5 = new byte[27];
    numArray5[23] = (byte) 66;
    numArray5[1] = (byte) 59;
    numArray5[2] = (byte) 235;
    numArray5[3] = (byte) 19;
    numArray5[4] = (byte) 24;
    numArray5[5] = (byte) 69;
    numArray5[6] = (byte) 99;
    numArray5[8] = (byte) 107;
    numArray5[13] = (byte) 209;
    numArray5[14] = (byte) 253;
    numArray5[24] = (byte) 130;
    numArray5[9] = (byte) 246;
    numArray5[21] = (byte) 183;
    numArray5[22] = (byte) 88;
    numArray5[12] = (byte) 31 /*0x1F*/;
    numArray5[15] = (byte) 67;
    numArray5[19] = (byte) 244;
    numArray5[17] = (byte) 190;
    numArray5[7] = (byte) 247;
    numArray5[0] = (byte) 95;
    numArray5[20] = (byte) 188;
    numArray5[11] = (byte) 215;
    numArray5[18] = (byte) 102;
    numArray5[26] = (byte) 3;
    numArray5[10] = (byte) 142;
    numArray5[16 /*0x10*/] = (byte) 180;
    numArray5[25] = (byte) 157;
    byte[] numArray6 = new byte[27]
    {
      (byte) 188,
      (byte) 30,
      (byte) 67,
      (byte) 18,
      (byte) 31 /*0x1F*/,
      (byte) 216,
      (byte) 90,
      (byte) 189,
      (byte) 216,
      (byte) 5,
      (byte) 253,
      (byte) 213,
      (byte) 227,
      (byte) 132,
      (byte) 102,
      (byte) 84,
      (byte) 154,
      (byte) 148,
      (byte) 190,
      (byte) 79,
      (byte) 225,
      (byte) 77,
      (byte) 2,
      (byte) 248,
      (byte) 12,
      (byte) 67,
      (byte) 111
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 27);
    for (int index = 0; index < 27; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static int ssp_appserver_12540(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 122,
      (byte) 66,
      (byte) 111,
      (byte) 187,
      (byte) 183,
      (byte) 77,
      (byte) 226,
      (byte) 54,
      (byte) 22,
      (byte) 244,
      (byte) 241,
      (byte) 165,
      (byte) 159,
      (byte) 13,
      (byte) 97,
      (byte) 15,
      (byte) 252,
      (byte) 186,
      (byte) 147,
      (byte) 115,
      (byte) 13,
      (byte) 90,
      (byte) 102,
      (byte) 241,
      (byte) 251,
      (byte) 25,
      (byte) 33,
      (byte) 25,
      (byte) 73,
      (byte) 115,
      (byte) 54,
      (byte) 41,
      (byte) 245,
      (byte) 131,
      (byte) 54,
      (byte) 195,
      (byte) 117,
      (byte) 4,
      (byte) 69,
      (byte) 120,
      (byte) 236,
      (byte) 62,
      (byte) 44,
      (byte) 169,
      (byte) 16 /*0x10*/,
      (byte) 155,
      (byte) 249,
      (byte) 123
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 35,
      (byte) 188,
      (byte) 8,
      (byte) 109,
      (byte) 84,
      (byte) 3,
      (byte) 239,
      (byte) 208 /*0xD0*/,
      (byte) 149,
      (byte) 28,
      (byte) 129,
      (byte) 184,
      (byte) 131,
      (byte) 3,
      (byte) 47,
      (byte) 12,
      (byte) 91,
      (byte) 171,
      (byte) 74,
      (byte) 146,
      (byte) 77,
      (byte) 92,
      (byte) 9,
      (byte) 215,
      (byte) 226,
      (byte) 60,
      (byte) 139,
      (byte) 9,
      (byte) 174,
      (byte) 165,
      (byte) 12,
      (byte) 247,
      (byte) 251,
      (byte) 67,
      (byte) 4,
      (byte) 200,
      (byte) 165,
      (byte) 216,
      (byte) 209,
      (byte) 124,
      (byte) 86,
      (byte) 181,
      (byte) 51,
      (byte) 145,
      (byte) 157,
      (byte) 93,
      (byte) 170,
      (byte) 50
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
