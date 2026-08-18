// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_13895
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;


namespace ImSSP;

internal static class sc_13895
{
  private static byte[] sspq = new byte[124]
  {
    (byte) 189,
    (byte) 160 /*0xA0*/,
    (byte) 92,
    (byte) 159,
    (byte) 189,
    (byte) 123,
    (byte) 219,
    (byte) 123,
    (byte) 224 /*0xE0*/,
    (byte) 145,
    (byte) 180,
    (byte) 148,
    (byte) 20,
    (byte) 151,
    (byte) 181,
    (byte) 251,
    (byte) 115,
    (byte) 2,
    (byte) 106,
    (byte) 44,
    (byte) 203,
    (byte) 243,
    (byte) 184,
    (byte) 224 /*0xE0*/,
    (byte) 158,
    (byte) 189,
    (byte) 32 /*0x20*/,
    (byte) 82,
    (byte) 27,
    (byte) 18,
    (byte) 205,
    (byte) 40,
    (byte) 14,
    (byte) 129,
    (byte) 206,
    (byte) 215,
    (byte) 33,
    (byte) 58,
    (byte) 70,
    (byte) 113,
    (byte) 123,
    (byte) 170,
    (byte) 116,
    (byte) 62,
    (byte) 213,
    (byte) 72,
    (byte) 243,
    (byte) 13,
    (byte) 18,
    (byte) 188,
    (byte) 82,
    (byte) 103,
    (byte) 230,
    (byte) 207,
    (byte) 176 /*0xB0*/,
    (byte) 130,
    (byte) 68,
    (byte) 161,
    (byte) 107,
    (byte) 56,
    (byte) 82,
    (byte) 29,
    (byte) 87,
    (byte) 243,
    (byte) 243,
    (byte) 146,
    (byte) 70,
    (byte) 193,
    (byte) 47,
    (byte) 201,
    (byte) 19,
    (byte) 161,
    (byte) 230,
    (byte) 132,
    (byte) 228,
    (byte) 38,
    (byte) 65,
    (byte) 104,
    (byte) 138,
    (byte) 221,
    (byte) 246,
    (byte) 73,
    (byte) 70,
    (byte) 226,
    (byte) 216,
    (byte) 86,
    (byte) 232,
    (byte) 97,
    (byte) 153,
    (byte) 140,
    (byte) 12,
    (byte) 89,
    (byte) 179,
    (byte) 108,
    (byte) 143,
    (byte) 25,
    (byte) 71,
    (byte) 141,
    (byte) 72,
    (byte) 224 /*0xE0*/,
    (byte) 202,
    (byte) 5,
    (byte) 212,
    (byte) 219,
    (byte) 204,
    (byte) 21,
    (byte) 146,
    (byte) 172,
    (byte) 135,
    (byte) 78,
    (byte) 150,
    (byte) 76,
    (byte) 193,
    (byte) 2,
    (byte) 10,
    (byte) 236,
    (byte) 221,
    (byte) 212,
    (byte) 18,
    (byte) 105,
    (byte) 61,
    (byte) 118,
    (byte) 237,
    (byte) 71
  };
  private static byte[] sspr = new byte[124]
  {
    (byte) 230,
    (byte) 161,
    (byte) 50,
    (byte) 196,
    (byte) 199,
    (byte) 6,
    (byte) 168,
    (byte) 116,
    (byte) 138,
    (byte) 247,
    (byte) 88,
    (byte) 9,
    (byte) 128 /*0x80*/,
    (byte) 49,
    (byte) 124,
    (byte) 66,
    (byte) 99,
    (byte) 143,
    (byte) 231,
    (byte) 47,
    (byte) 96 /*0x60*/,
    (byte) 112 /*0x70*/,
    (byte) 0,
    (byte) 135,
    (byte) 10,
    (byte) 195,
    (byte) 238,
    (byte) 189,
    (byte) 1,
    (byte) 155,
    (byte) 8,
    (byte) 64 /*0x40*/,
    (byte) 153,
    (byte) 185,
    (byte) 4,
    (byte) 159,
    (byte) 187,
    (byte) 50,
    (byte) 241,
    (byte) 32 /*0x20*/,
    (byte) 114,
    (byte) 41,
    (byte) 122,
    (byte) 45,
    (byte) 213,
    (byte) 221,
    (byte) 85,
    (byte) 29,
    (byte) 216,
    (byte) 102,
    (byte) 195,
    (byte) 186,
    (byte) 230,
    (byte) 161,
    (byte) 190,
    (byte) 144 /*0x90*/,
    (byte) 59,
    (byte) 225,
    (byte) 66,
    (byte) 160 /*0xA0*/,
    (byte) 173,
    (byte) 35,
    (byte) 75,
    (byte) 175,
    (byte) 169,
    (byte) 114,
    (byte) 84,
    (byte) 162,
    (byte) 113,
    (byte) 202,
    (byte) 38,
    (byte) 210,
    (byte) 58,
    (byte) 145,
    (byte) 214,
    (byte) 82,
    (byte) 151,
    (byte) 100,
    (byte) 113,
    (byte) 107,
    (byte) 200,
    (byte) 3,
    (byte) 201,
    (byte) 19,
    (byte) 204,
    (byte) 194,
    (byte) 233,
    (byte) 5,
    (byte) 41,
    (byte) 39,
    (byte) 159,
    (byte) 137,
    (byte) 244,
    (byte) 92,
    (byte) 155,
    (byte) 150,
    (byte) 204,
    (byte) 214,
    (byte) 85,
    (byte) 99,
    (byte) 106,
    (byte) 253,
    (byte) 154,
    (byte) 80 /*0x50*/,
    (byte) 105,
    (byte) 120,
    (byte) 59,
    (byte) 123,
    (byte) 234,
    (byte) 113,
    (byte) 203,
    (byte) 90,
    (byte) 211,
    (byte) 148,
    (byte) 105,
    (byte) 17,
    (byte) 111,
    (byte) 48 /*0x30*/,
    (byte) 214,
    (byte) 116,
    (byte) 12,
    (byte) 208 /*0xD0*/,
    (byte) 237,
    (byte) 56
  };

  internal static int ssp_appserver_13896(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[9] = (byte) 58;
    sourceArray1[3] = (byte) 232;
    sourceArray1[2] = (byte) 56;
    sourceArray1[41] = (byte) 22;
    sourceArray1[31 /*0x1F*/] = (byte) 101;
    sourceArray1[5] = (byte) 175;
    sourceArray1[6] = (byte) 41;
    sourceArray1[0] = (byte) 183;
    sourceArray1[1] = (byte) 234;
    sourceArray1[4] = (byte) 211;
    sourceArray1[18] = (byte) 109;
    sourceArray1[22] = (byte) 68;
    sourceArray1[12] = (byte) 19;
    sourceArray1[13] = (byte) 99;
    sourceArray1[46] = (byte) 18;
    sourceArray1[14] = (byte) 45;
    sourceArray1[16 /*0x10*/] = (byte) 143;
    sourceArray1[17] = (byte) 193;
    sourceArray1[27] = (byte) 48 /*0x30*/;
    sourceArray1[35] = (byte) 73;
    sourceArray1[20] = (byte) 188;
    sourceArray1[21] = (byte) 179;
    sourceArray1[39] = (byte) 208 /*0xD0*/;
    sourceArray1[23] = (byte) 184;
    sourceArray1[24] = (byte) 217;
    sourceArray1[25] = (byte) 74;
    sourceArray1[26] = (byte) 26;
    sourceArray1[10] = (byte) 21;
    sourceArray1[7] = (byte) 138;
    sourceArray1[29] = (byte) 205;
    sourceArray1[30] = (byte) 14;
    sourceArray1[36] = (byte) 200;
    sourceArray1[32 /*0x20*/] = (byte) 7;
    sourceArray1[28] = (byte) 112 /*0x70*/;
    sourceArray1[34] = (byte) 112 /*0x70*/;
    sourceArray1[33] = (byte) 140;
    sourceArray1[40] = (byte) 219;
    sourceArray1[43] = (byte) 224 /*0xE0*/;
    sourceArray1[38] = (byte) 118;
    sourceArray1[45] = (byte) 36;
    sourceArray1[8] = (byte) 182;
    sourceArray1[42] = (byte) 97;
    sourceArray1[19] = (byte) 4;
    sourceArray1[37] = (byte) 120;
    sourceArray1[44] = (byte) 201;
    sourceArray1[15] = (byte) 57;
    sourceArray1[11] = (byte) 119;
    sourceArray1[47] = (byte) 50;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 234,
      (byte) 166,
      (byte) 17,
      (byte) 125,
      (byte) 78,
      (byte) 173,
      (byte) 225,
      (byte) 161,
      (byte) 217,
      (byte) 242,
      (byte) 28,
      (byte) 223,
      (byte) 50,
      (byte) 3,
      (byte) 208 /*0xD0*/,
      (byte) 121,
      (byte) 1,
      (byte) 100,
      (byte) 206,
      (byte) 42,
      (byte) 180,
      (byte) 180,
      (byte) 159,
      (byte) 232,
      (byte) 13,
      (byte) 191,
      (byte) 194,
      (byte) 99,
      (byte) 243,
      (byte) 139,
      (byte) 217,
      (byte) 113,
      (byte) 248,
      (byte) 123,
      (byte) 48 /*0x30*/,
      (byte) 142,
      (byte) 254,
      (byte) 81,
      (byte) 147,
      (byte) 251,
      (byte) 178,
      (byte) 103,
      (byte) 23,
      (byte) 223,
      (byte) 52,
      (byte) 236,
      (byte) 180,
      (byte) 118
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13897(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[44] = (byte) 97;
    sourceArray1[1] = (byte) 63 /*0x3F*/;
    sourceArray1[8] = (byte) 97;
    sourceArray1[3] = (byte) 168;
    sourceArray1[45] = (byte) 184;
    sourceArray1[28] = (byte) 70;
    sourceArray1[6] = (byte) 16 /*0x10*/;
    sourceArray1[9] = (byte) 178;
    sourceArray1[36] = (byte) 16 /*0x10*/;
    sourceArray1[46] = (byte) 16 /*0x10*/;
    sourceArray1[10] = (byte) 66;
    sourceArray1[14] = (byte) 173;
    sourceArray1[12] = (byte) 89;
    sourceArray1[13] = (byte) 80 /*0x50*/;
    sourceArray1[17] = (byte) 15;
    sourceArray1[15] = (byte) 201;
    sourceArray1[16 /*0x10*/] = (byte) 194;
    sourceArray1[39] = (byte) 118;
    sourceArray1[24] = (byte) 39;
    sourceArray1[19] = (byte) 248;
    sourceArray1[20] = (byte) 162;
    sourceArray1[42] = (byte) 35;
    sourceArray1[22] = (byte) 135;
    sourceArray1[18] = (byte) 142;
    sourceArray1[30] = (byte) 217;
    sourceArray1[25] = (byte) 173;
    sourceArray1[26] = (byte) 93;
    sourceArray1[27] = (byte) 99;
    sourceArray1[37] = (byte) 175;
    sourceArray1[29] = (byte) 169;
    sourceArray1[40] = (byte) 167;
    sourceArray1[31 /*0x1F*/] = (byte) 173;
    sourceArray1[32 /*0x20*/] = (byte) 214;
    sourceArray1[33] = (byte) 1;
    sourceArray1[34] = (byte) 173;
    sourceArray1[23] = (byte) 63 /*0x3F*/;
    sourceArray1[4] = (byte) 221;
    sourceArray1[7] = (byte) 17;
    sourceArray1[41] = (byte) 187;
    sourceArray1[38] = (byte) 252;
    sourceArray1[35] = (byte) 167;
    sourceArray1[5] = (byte) 38;
    sourceArray1[11] = (byte) 144 /*0x90*/;
    sourceArray1[43] = (byte) 184;
    sourceArray1[2] = (byte) 30;
    sourceArray1[21] = (byte) 2;
    sourceArray1[0] = (byte) 139;
    sourceArray1[47] = (byte) 157;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[42] = (byte) 124;
    sourceArray2[1] = (byte) 227;
    sourceArray2[2] = (byte) 147;
    sourceArray2[35] = (byte) 38;
    sourceArray2[24] = (byte) 63 /*0x3F*/;
    sourceArray2[3] = (byte) 63 /*0x3F*/;
    sourceArray2[21] = (byte) 225;
    sourceArray2[40] = (byte) 242;
    sourceArray2[13] = (byte) 17;
    sourceArray2[15] = (byte) 77;
    sourceArray2[7] = (byte) 40;
    sourceArray2[11] = (byte) 179;
    sourceArray2[16 /*0x10*/] = (byte) 137;
    sourceArray2[31 /*0x1F*/] = (byte) 53;
    sourceArray2[44] = (byte) 234;
    sourceArray2[20] = (byte) 216;
    sourceArray2[0] = (byte) 23;
    sourceArray2[17] = (byte) 43;
    sourceArray2[33] = (byte) 252;
    sourceArray2[19] = (byte) 97;
    sourceArray2[8] = (byte) 48 /*0x30*/;
    sourceArray2[4] = (byte) 146;
    sourceArray2[22] = (byte) 239;
    sourceArray2[23] = (byte) 219;
    sourceArray2[30] = (byte) 74;
    sourceArray2[25] = (byte) 223;
    sourceArray2[12] = (byte) 253;
    sourceArray2[5] = (byte) 45;
    sourceArray2[10] = (byte) 228;
    sourceArray2[29] = (byte) 13;
    sourceArray2[34] = (byte) 212;
    sourceArray2[27] = (byte) 221;
    sourceArray2[32 /*0x20*/] = (byte) 254;
    sourceArray2[46] = (byte) 111;
    sourceArray2[9] = (byte) 160 /*0xA0*/;
    sourceArray2[38] = (byte) 188;
    sourceArray2[36] = (byte) 58;
    sourceArray2[37] = (byte) 195;
    sourceArray2[14] = (byte) 168;
    sourceArray2[39] = (byte) 119;
    sourceArray2[18] = (byte) 105;
    sourceArray2[41] = (byte) 222;
    sourceArray2[26] = (byte) 97;
    sourceArray2[43] = (byte) 145;
    sourceArray2[45] = (byte) 213;
    sourceArray2[28] = (byte) 145;
    sourceArray2[6] = (byte) 41;
    sourceArray2[47] = (byte) 226;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13898(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 165,
      (byte) 34,
      (byte) 80 /*0x50*/,
      (byte) 34,
      (byte) 117,
      (byte) 190,
      (byte) 250,
      (byte) 78,
      (byte) 123,
      (byte) 112 /*0x70*/,
      (byte) 215,
      (byte) 67,
      (byte) 14,
      (byte) 165,
      (byte) 118,
      (byte) 50,
      (byte) 53,
      (byte) 249,
      (byte) 237,
      (byte) 161,
      (byte) 174,
      (byte) 66,
      (byte) 136,
      (byte) 209,
      (byte) 0,
      (byte) 78,
      (byte) 150,
      (byte) 27,
      (byte) 165,
      (byte) 4,
      (byte) 16 /*0x10*/,
      (byte) 86,
      (byte) 199,
      (byte) 13,
      (byte) 196,
      (byte) 53,
      (byte) 166,
      (byte) 81,
      (byte) 122,
      (byte) 83,
      (byte) 117,
      (byte) 149,
      (byte) 74,
      (byte) 222,
      (byte) 245,
      (byte) 4,
      (byte) 12,
      (byte) 210
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 163,
      (byte) 125,
      (byte) 44,
      (byte) 118,
      (byte) 40,
      (byte) 177,
      byte.MaxValue,
      (byte) 73,
      (byte) 28,
      (byte) 135,
      (byte) 173,
      (byte) 145,
      (byte) 122,
      (byte) 1,
      (byte) 182,
      (byte) 191,
      (byte) 29,
      (byte) 161,
      (byte) 119,
      (byte) 96 /*0x60*/,
      (byte) 177,
      (byte) 125,
      (byte) 105,
      (byte) 235,
      (byte) 77,
      (byte) 142,
      (byte) 184,
      (byte) 211,
      (byte) 186,
      (byte) 94,
      (byte) 77,
      (byte) 63 /*0x3F*/,
      (byte) 105,
      (byte) 89,
      (byte) 228,
      (byte) 78,
      (byte) 190,
      (byte) 80 /*0x50*/,
      (byte) 181,
      (byte) 191,
      (byte) 52,
      (byte) 57,
      (byte) 133,
      (byte) 27,
      (byte) 233,
      (byte) 30,
      (byte) 241,
      (byte) 226
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13899(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[15] = (byte) 224 /*0xE0*/;
    sourceArray1[34] = (byte) 77;
    sourceArray1[2] = (byte) 98;
    sourceArray1[23] = (byte) 247;
    sourceArray1[4] = (byte) 127 /*0x7F*/;
    sourceArray1[5] = (byte) 33;
    sourceArray1[44] = (byte) 212;
    sourceArray1[9] = (byte) 169;
    sourceArray1[26] = (byte) 252;
    sourceArray1[35] = (byte) 131;
    sourceArray1[46] = (byte) 117;
    sourceArray1[11] = (byte) 96 /*0x60*/;
    sourceArray1[12] = (byte) 128 /*0x80*/;
    sourceArray1[18] = (byte) 227;
    sourceArray1[7] = (byte) 40;
    sourceArray1[24] = (byte) 169;
    sourceArray1[16 /*0x10*/] = (byte) 65;
    sourceArray1[17] = (byte) 140;
    sourceArray1[13] = (byte) 123;
    sourceArray1[33] = (byte) 19;
    sourceArray1[20] = (byte) 153;
    sourceArray1[36] = (byte) 98;
    sourceArray1[47] = (byte) 136;
    sourceArray1[10] = (byte) 118;
    sourceArray1[38] = (byte) 39;
    sourceArray1[25] = (byte) 219;
    sourceArray1[0] = (byte) 122;
    sourceArray1[27] = (byte) 65;
    sourceArray1[28] = (byte) 195;
    sourceArray1[29] = (byte) 170;
    sourceArray1[30] = (byte) 19;
    sourceArray1[6] = (byte) 39;
    sourceArray1[32 /*0x20*/] = (byte) 4;
    sourceArray1[40] = (byte) 83;
    sourceArray1[21] = (byte) 44;
    sourceArray1[45] = (byte) 126;
    sourceArray1[8] = (byte) 222;
    sourceArray1[31 /*0x1F*/] = (byte) 191;
    sourceArray1[1] = (byte) 138;
    sourceArray1[3] = (byte) 154;
    sourceArray1[14] = (byte) 0;
    sourceArray1[41] = (byte) 43;
    sourceArray1[42] = (byte) 150;
    sourceArray1[43] = (byte) 236;
    sourceArray1[39] = (byte) 48 /*0x30*/;
    sourceArray1[37] = (byte) 154;
    sourceArray1[19] = (byte) 126;
    sourceArray1[22] = (byte) 24;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 66,
      (byte) 116,
      (byte) 211,
      (byte) 62,
      (byte) 168,
      (byte) 63 /*0x3F*/,
      (byte) 17,
      (byte) 222,
      (byte) 71,
      (byte) 75,
      (byte) 54,
      (byte) 214,
      (byte) 83,
      (byte) 3,
      (byte) 122,
      (byte) 57,
      (byte) 188,
      (byte) 73,
      (byte) 176 /*0xB0*/,
      (byte) 1,
      (byte) 155,
      (byte) 40,
      (byte) 33,
      (byte) 133,
      (byte) 244,
      (byte) 122,
      (byte) 164,
      (byte) 201,
      (byte) 101,
      (byte) 223,
      (byte) 123,
      (byte) 149,
      (byte) 122,
      (byte) 224 /*0xE0*/,
      (byte) 222,
      (byte) 129,
      (byte) 9,
      (byte) 84,
      (byte) 210,
      (byte) 30,
      (byte) 125,
      (byte) 55,
      (byte) 9,
      (byte) 60,
      (byte) 170,
      (byte) 139,
      (byte) 62,
      (byte) 179
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13900(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 7,
      (byte) 90,
      (byte) 143,
      (byte) 116,
      (byte) 137,
      (byte) 160 /*0xA0*/,
      (byte) 174,
      (byte) 98,
      (byte) 59,
      (byte) 183,
      (byte) 69,
      (byte) 71,
      (byte) 196,
      (byte) 61,
      (byte) 177,
      (byte) 177,
      (byte) 148,
      (byte) 185,
      (byte) 135,
      (byte) 89,
      (byte) 230,
      (byte) 4,
      (byte) 46,
      (byte) 108,
      (byte) 133,
      (byte) 251,
      (byte) 225,
      (byte) 167,
      (byte) 189,
      (byte) 167,
      (byte) 51,
      (byte) 214,
      (byte) 5,
      (byte) 9,
      (byte) 95,
      (byte) 25,
      (byte) 165,
      (byte) 91,
      (byte) 240 /*0xF0*/,
      (byte) 110,
      (byte) 174,
      (byte) 19,
      (byte) 45,
      (byte) 72,
      (byte) 203,
      (byte) 176 /*0xB0*/,
      (byte) 229,
      (byte) 69
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[36] = (byte) 85;
    sourceArray2[11] = (byte) 19;
    sourceArray2[20] = (byte) 18;
    sourceArray2[3] = (byte) 137;
    sourceArray2[4] = (byte) 86;
    sourceArray2[5] = (byte) 81;
    sourceArray2[6] = (byte) 125;
    sourceArray2[21] = (byte) 91;
    sourceArray2[7] = (byte) 21;
    sourceArray2[9] = (byte) 121;
    sourceArray2[30] = (byte) 116;
    sourceArray2[8] = (byte) 210;
    sourceArray2[10] = (byte) 36;
    sourceArray2[13] = (byte) 15;
    sourceArray2[14] = (byte) 238;
    sourceArray2[15] = (byte) 200;
    sourceArray2[16 /*0x10*/] = (byte) 99;
    sourceArray2[27] = (byte) 118;
    sourceArray2[12] = (byte) 156;
    sourceArray2[19] = (byte) 100;
    sourceArray2[45] = (byte) 61;
    sourceArray2[34] = (byte) 243;
    sourceArray2[22] = (byte) 144 /*0x90*/;
    sourceArray2[46] = (byte) 5;
    sourceArray2[24] = (byte) 84;
    sourceArray2[25] = (byte) 46;
    sourceArray2[26] = (byte) 29;
    sourceArray2[2] = (byte) 178;
    sourceArray2[28] = (byte) 39;
    sourceArray2[18] = (byte) 173;
    sourceArray2[33] = (byte) 190;
    sourceArray2[23] = (byte) 236;
    sourceArray2[17] = (byte) 82;
    sourceArray2[44] = (byte) 71;
    sourceArray2[1] = (byte) 201;
    sourceArray2[42] = (byte) 109;
    sourceArray2[31 /*0x1F*/] = (byte) 153;
    sourceArray2[37] = (byte) 69;
    sourceArray2[29] = (byte) 204;
    sourceArray2[39] = (byte) 211;
    sourceArray2[40] = (byte) 142;
    sourceArray2[41] = (byte) 29;
    sourceArray2[32 /*0x20*/] = (byte) 193;
    sourceArray2[43] = (byte) 198;
    sourceArray2[38] = (byte) 37;
    sourceArray2[35] = (byte) 237;
    sourceArray2[0] = (byte) 213;
    sourceArray2[47] = (byte) 113;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13901(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[8] = (byte) 105;
    sourceArray1[1] = (byte) 103;
    sourceArray1[2] = (byte) 194;
    sourceArray1[3] = (byte) 174;
    sourceArray1[25] = (byte) 203;
    sourceArray1[5] = (byte) 223;
    sourceArray1[6] = (byte) 182;
    sourceArray1[7] = (byte) 26;
    sourceArray1[44] = (byte) 166;
    sourceArray1[21] = (byte) 237;
    sourceArray1[12] = (byte) 25;
    sourceArray1[4] = (byte) 249;
    sourceArray1[26] = (byte) 103;
    sourceArray1[38] = (byte) 124;
    sourceArray1[14] = (byte) 34;
    sourceArray1[24] = (byte) 139;
    sourceArray1[20] = (byte) 1;
    sourceArray1[15] = (byte) 186;
    sourceArray1[45] = (byte) 254;
    sourceArray1[19] = (byte) 102;
    sourceArray1[43] = (byte) 48 /*0x30*/;
    sourceArray1[33] = (byte) 103;
    sourceArray1[22] = (byte) 130;
    sourceArray1[17] = (byte) 29;
    sourceArray1[30] = (byte) 61;
    sourceArray1[23] = (byte) 170;
    sourceArray1[32 /*0x20*/] = (byte) 66;
    sourceArray1[27] = (byte) 104;
    sourceArray1[28] = (byte) 2;
    sourceArray1[16 /*0x10*/] = (byte) 162;
    sourceArray1[46] = (byte) 83;
    sourceArray1[31 /*0x1F*/] = (byte) 34;
    sourceArray1[35] = (byte) 52;
    sourceArray1[18] = (byte) 249;
    sourceArray1[34] = (byte) 234;
    sourceArray1[0] = (byte) 42;
    sourceArray1[36] = (byte) 138;
    sourceArray1[42] = (byte) 100;
    sourceArray1[11] = (byte) 131;
    sourceArray1[13] = (byte) 120;
    sourceArray1[40] = (byte) 81;
    sourceArray1[41] = (byte) 38;
    sourceArray1[29] = (byte) 100;
    sourceArray1[39] = (byte) 171;
    sourceArray1[47] = (byte) 233;
    sourceArray1[37] = (byte) 83;
    sourceArray1[10] = (byte) 70;
    sourceArray1[9] = (byte) 43;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 49,
      (byte) 54,
      (byte) 92,
      (byte) 210,
      (byte) 70,
      (byte) 7,
      (byte) 144 /*0x90*/,
      (byte) 82,
      (byte) 247,
      (byte) 215,
      (byte) 64 /*0x40*/,
      (byte) 69,
      (byte) 252,
      (byte) 53,
      (byte) 41,
      (byte) 200,
      (byte) 122,
      (byte) 34,
      (byte) 30,
      (byte) 137,
      (byte) 144 /*0x90*/,
      (byte) 209,
      (byte) 75,
      (byte) 224 /*0xE0*/,
      (byte) 99,
      (byte) 17,
      (byte) 119,
      (byte) 252,
      (byte) 209,
      (byte) 54,
      (byte) 17,
      (byte) 85,
      (byte) 21,
      (byte) 101,
      (byte) 220,
      (byte) 216,
      (byte) 234,
      (byte) 51,
      (byte) 163,
      (byte) 157,
      (byte) 225,
      (byte) 223,
      (byte) 10,
      (byte) 237,
      (byte) 60,
      (byte) 99,
      (byte) 184,
      (byte) 114
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[38];
    byte[] response2 = new byte[38];
    Array.Copy((Array) sc_13895.sspq, 0, (Array) numArray2, 0, 38);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13895.sspr, 0, (Array) numArray2, 0, 38);
    for (int index = 0; index < numArray2.Length; ++index)
    {
      if ((int) numArray2[index] != (int) response2[index])
      {
        key.TagValue = (int) response2[index];
        break;
      }
    }
    return BitConverter.ToInt32(response1, 0) ^ BitConverter.ToInt32(numArray1, 0) ^ k;
  }

  internal static int ssp_appserver_13902(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[0] = (byte) 230;
    sourceArray1[28] = (byte) 200;
    sourceArray1[2] = (byte) 107;
    sourceArray1[37] = (byte) 79;
    sourceArray1[21] = (byte) 136;
    sourceArray1[32 /*0x20*/] = (byte) 170;
    sourceArray1[6] = (byte) 93;
    sourceArray1[40] = (byte) 95;
    sourceArray1[8] = (byte) 153;
    sourceArray1[9] = (byte) 55;
    sourceArray1[14] = (byte) 183;
    sourceArray1[11] = (byte) 11;
    sourceArray1[16 /*0x10*/] = (byte) 232;
    sourceArray1[46] = (byte) 51;
    sourceArray1[33] = (byte) 77;
    sourceArray1[15] = (byte) 202;
    sourceArray1[44] = (byte) 130;
    sourceArray1[17] = (byte) 123;
    sourceArray1[18] = (byte) 1;
    sourceArray1[19] = byte.MaxValue;
    sourceArray1[20] = (byte) 91;
    sourceArray1[7] = (byte) 152;
    sourceArray1[45] = (byte) 193;
    sourceArray1[43] = (byte) 227;
    sourceArray1[24] = (byte) 196;
    sourceArray1[38] = (byte) 232;
    sourceArray1[4] = (byte) 248;
    sourceArray1[27] = (byte) 146;
    sourceArray1[31 /*0x1F*/] = (byte) 56;
    sourceArray1[23] = (byte) 206;
    sourceArray1[3] = (byte) 104;
    sourceArray1[1] = (byte) 221;
    sourceArray1[30] = (byte) 218;
    sourceArray1[42] = (byte) 184;
    sourceArray1[25] = (byte) 140;
    sourceArray1[35] = (byte) 237;
    sourceArray1[13] = (byte) 22;
    sourceArray1[12] = (byte) 245;
    sourceArray1[26] = (byte) 133;
    sourceArray1[39] = (byte) 227;
    sourceArray1[5] = (byte) 187;
    sourceArray1[34] = (byte) 70;
    sourceArray1[29] = (byte) 221;
    sourceArray1[10] = (byte) 143;
    sourceArray1[41] = (byte) 136;
    sourceArray1[36] = (byte) 136;
    sourceArray1[22] = (byte) 95;
    sourceArray1[47] = (byte) 248;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[12] = (byte) 165;
    sourceArray2[0] = (byte) 45;
    sourceArray2[29] = (byte) 184;
    sourceArray2[3] = (byte) 50;
    sourceArray2[27] = (byte) 5;
    sourceArray2[5] = (byte) 231;
    sourceArray2[22] = (byte) 73;
    sourceArray2[7] = (byte) 246;
    sourceArray2[15] = (byte) 51;
    sourceArray2[40] = (byte) 131;
    sourceArray2[10] = (byte) 239;
    sourceArray2[36] = (byte) 245;
    sourceArray2[2] = (byte) 128 /*0x80*/;
    sourceArray2[13] = (byte) 178;
    sourceArray2[14] = (byte) 20;
    sourceArray2[34] = byte.MaxValue;
    sourceArray2[11] = (byte) 9;
    sourceArray2[21] = (byte) 37;
    sourceArray2[18] = (byte) 197;
    sourceArray2[41] = (byte) 115;
    sourceArray2[38] = (byte) 240 /*0xF0*/;
    sourceArray2[42] = (byte) 146;
    sourceArray2[6] = (byte) 104;
    sourceArray2[19] = (byte) 200;
    sourceArray2[8] = (byte) 16 /*0x10*/;
    sourceArray2[25] = (byte) 201;
    sourceArray2[28] = (byte) 114;
    sourceArray2[23] = (byte) 233;
    sourceArray2[20] = (byte) 218;
    sourceArray2[26] = (byte) 227;
    sourceArray2[37] = (byte) 120;
    sourceArray2[31 /*0x1F*/] = (byte) 241;
    sourceArray2[32 /*0x20*/] = (byte) 240 /*0xF0*/;
    sourceArray2[33] = (byte) 232;
    sourceArray2[16 /*0x10*/] = (byte) 206;
    sourceArray2[35] = (byte) 127 /*0x7F*/;
    sourceArray2[30] = (byte) 121;
    sourceArray2[4] = (byte) 227;
    sourceArray2[47] = (byte) 221;
    sourceArray2[39] = (byte) 166;
    sourceArray2[9] = (byte) 117;
    sourceArray2[1] = (byte) 186;
    sourceArray2[17] = (byte) 9;
    sourceArray2[43] = (byte) 238;
    sourceArray2[44] = (byte) 75;
    sourceArray2[45] = (byte) 163;
    sourceArray2[46] = (byte) 228;
    sourceArray2[24] = (byte) 145;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13903(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[46] = (byte) 41;
    sourceArray1[1] = (byte) 146;
    sourceArray1[2] = (byte) 172;
    sourceArray1[3] = (byte) 48 /*0x30*/;
    sourceArray1[37] = (byte) 162;
    sourceArray1[47] = (byte) 56;
    sourceArray1[6] = (byte) 82;
    sourceArray1[7] = (byte) 110;
    sourceArray1[8] = (byte) 64 /*0x40*/;
    sourceArray1[18] = (byte) 47;
    sourceArray1[15] = (byte) 212;
    sourceArray1[11] = (byte) 103;
    sourceArray1[0] = (byte) 6;
    sourceArray1[34] = (byte) 105;
    sourceArray1[14] = (byte) 171;
    sourceArray1[22] = (byte) 57;
    sourceArray1[16 /*0x10*/] = (byte) 186;
    sourceArray1[17] = (byte) 11;
    sourceArray1[20] = (byte) 69;
    sourceArray1[23] = (byte) 10;
    sourceArray1[33] = (byte) 175;
    sourceArray1[45] = (byte) 49;
    sourceArray1[39] = (byte) 77;
    sourceArray1[9] = (byte) 167;
    sourceArray1[24] = (byte) 51;
    sourceArray1[27] = (byte) 114;
    sourceArray1[12] = (byte) 249;
    sourceArray1[13] = (byte) 171;
    sourceArray1[28] = (byte) 128 /*0x80*/;
    sourceArray1[29] = (byte) 140;
    sourceArray1[30] = (byte) 234;
    sourceArray1[31 /*0x1F*/] = (byte) 188;
    sourceArray1[25] = (byte) 6;
    sourceArray1[5] = (byte) 211;
    sourceArray1[40] = (byte) 79;
    sourceArray1[35] = (byte) 121;
    sourceArray1[36] = (byte) 109;
    sourceArray1[21] = (byte) 66;
    sourceArray1[38] = (byte) 152;
    sourceArray1[42] = (byte) 36;
    sourceArray1[32 /*0x20*/] = (byte) 226;
    sourceArray1[41] = (byte) 95;
    sourceArray1[4] = (byte) 92;
    sourceArray1[43] = (byte) 33;
    sourceArray1[44] = (byte) 149;
    sourceArray1[26] = (byte) 115;
    sourceArray1[10] = (byte) 107;
    sourceArray1[19] = (byte) 254;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 164,
      (byte) 241,
      (byte) 134,
      (byte) 182,
      (byte) 172,
      (byte) 73,
      (byte) 122,
      (byte) 98,
      (byte) 187,
      (byte) 52,
      (byte) 154,
      (byte) 26,
      (byte) 166,
      (byte) 220,
      (byte) 129,
      (byte) 98,
      (byte) 218,
      (byte) 118,
      (byte) 121,
      (byte) 18,
      (byte) 5,
      (byte) 230,
      (byte) 235,
      (byte) 38,
      (byte) 132,
      (byte) 159,
      (byte) 145,
      (byte) 233,
      (byte) 171,
      (byte) 143,
      (byte) 166,
      (byte) 47,
      (byte) 150,
      (byte) 208 /*0xD0*/,
      (byte) 188,
      (byte) 216,
      (byte) 191,
      (byte) 237,
      (byte) 116,
      (byte) 57,
      (byte) 174,
      (byte) 4,
      (byte) 171,
      (byte) 155,
      (byte) 209,
      (byte) 220,
      (byte) 110,
      (byte) 131
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13904(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 222,
      (byte) 247,
      (byte) 189,
      (byte) 97,
      (byte) 136,
      (byte) 239,
      (byte) 137,
      (byte) 94,
      (byte) 60,
      (byte) 165,
      (byte) 63 /*0x3F*/,
      (byte) 44,
      (byte) 251,
      (byte) 122,
      (byte) 38,
      (byte) 94,
      (byte) 54,
      (byte) 200,
      (byte) 52,
      (byte) 93,
      (byte) 175,
      (byte) 190,
      (byte) 155,
      (byte) 17,
      (byte) 154,
      (byte) 113,
      (byte) 48 /*0x30*/,
      (byte) 244,
      (byte) 63 /*0x3F*/,
      (byte) 117,
      (byte) 173,
      (byte) 180,
      (byte) 143,
      (byte) 85,
      (byte) 253,
      (byte) 66,
      (byte) 196,
      (byte) 86,
      (byte) 98,
      (byte) 152,
      (byte) 199,
      (byte) 119,
      (byte) 79,
      (byte) 0,
      (byte) 225,
      (byte) 159,
      (byte) 170,
      (byte) 200
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 22,
      (byte) 120,
      (byte) 191,
      (byte) 229,
      (byte) 110,
      (byte) 145,
      (byte) 42,
      (byte) 164,
      (byte) 116,
      (byte) 135,
      (byte) 89,
      (byte) 250,
      (byte) 103,
      (byte) 18,
      (byte) 155,
      (byte) 108,
      (byte) 77,
      (byte) 139,
      (byte) 219,
      (byte) 226,
      (byte) 63 /*0x3F*/,
      (byte) 73,
      (byte) 58,
      (byte) 75,
      (byte) 192 /*0xC0*/,
      (byte) 159,
      (byte) 148,
      (byte) 188,
      (byte) 87,
      (byte) 64 /*0x40*/,
      (byte) 69,
      (byte) 134,
      (byte) 244,
      (byte) 230,
      (byte) 120,
      (byte) 96 /*0x60*/,
      (byte) 241,
      (byte) 11,
      (byte) 119,
      (byte) 18,
      (byte) 87,
      (byte) 233,
      (byte) 120,
      (byte) 156,
      (byte) 143,
      (byte) 65,
      (byte) 44,
      (byte) 8
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13905(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[46] = (byte) 85;
    sourceArray1[44] = (byte) 137;
    sourceArray1[2] = (byte) 32 /*0x20*/;
    sourceArray1[3] = (byte) 92;
    sourceArray1[21] = (byte) 149;
    sourceArray1[5] = (byte) 199;
    sourceArray1[6] = (byte) 23;
    sourceArray1[31 /*0x1F*/] = (byte) 59;
    sourceArray1[13] = (byte) 156;
    sourceArray1[7] = (byte) 190;
    sourceArray1[10] = (byte) 198;
    sourceArray1[9] = (byte) 30;
    sourceArray1[12] = (byte) 139;
    sourceArray1[22] = (byte) 56;
    sourceArray1[14] = (byte) 166;
    sourceArray1[15] = (byte) 124;
    sourceArray1[27] = (byte) 199;
    sourceArray1[28] = (byte) 104;
    sourceArray1[18] = (byte) 223;
    sourceArray1[19] = (byte) 101;
    sourceArray1[20] = (byte) 155;
    sourceArray1[23] = (byte) 110;
    sourceArray1[17] = (byte) 153;
    sourceArray1[26] = (byte) 250;
    sourceArray1[24] = (byte) 154;
    sourceArray1[25] = (byte) 48 /*0x30*/;
    sourceArray1[34] = (byte) 7;
    sourceArray1[37] = (byte) 230;
    sourceArray1[32 /*0x20*/] = (byte) 6;
    sourceArray1[41] = (byte) 217;
    sourceArray1[11] = (byte) 248;
    sourceArray1[8] = (byte) 149;
    sourceArray1[0] = (byte) 136;
    sourceArray1[16 /*0x10*/] = (byte) 227;
    sourceArray1[38] = (byte) 24;
    sourceArray1[35] = (byte) 88;
    sourceArray1[36] = (byte) 220;
    sourceArray1[1] = (byte) 252;
    sourceArray1[45] = (byte) 8;
    sourceArray1[30] = (byte) 47;
    sourceArray1[40] = (byte) 75;
    sourceArray1[39] = (byte) 143;
    sourceArray1[42] = (byte) 50;
    sourceArray1[33] = (byte) 131;
    sourceArray1[29] = (byte) 44;
    sourceArray1[43] = (byte) 88;
    sourceArray1[4] = (byte) 172;
    sourceArray1[47] = (byte) 3;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 24,
      (byte) 136,
      (byte) 168,
      (byte) 105,
      (byte) 222,
      (byte) 190,
      (byte) 33,
      (byte) 221,
      (byte) 185,
      (byte) 7,
      (byte) 55,
      (byte) 235,
      (byte) 246,
      (byte) 112 /*0x70*/,
      (byte) 182,
      (byte) 249,
      (byte) 93,
      (byte) 231,
      (byte) 113,
      (byte) 48 /*0x30*/,
      (byte) 168,
      (byte) 222,
      (byte) 117,
      (byte) 244,
      (byte) 192 /*0xC0*/,
      (byte) 230,
      (byte) 227,
      (byte) 199,
      (byte) 209,
      (byte) 141,
      (byte) 149,
      (byte) 99,
      (byte) 106,
      (byte) 88,
      (byte) 92,
      (byte) 129,
      (byte) 51,
      (byte) 145,
      (byte) 39,
      (byte) 157,
      (byte) 99,
      (byte) 215,
      (byte) 140,
      (byte) 45,
      (byte) 208 /*0xD0*/,
      (byte) 186,
      (byte) 207,
      (byte) 182
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13906(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[31 /*0x1F*/] = (byte) 117;
    sourceArray1[1] = (byte) 111;
    sourceArray1[42] = (byte) 27;
    sourceArray1[47] = (byte) 249;
    sourceArray1[29] = (byte) 61;
    sourceArray1[5] = (byte) 244;
    sourceArray1[6] = (byte) 10;
    sourceArray1[30] = (byte) 153;
    sourceArray1[26] = (byte) 230;
    sourceArray1[23] = (byte) 185;
    sourceArray1[20] = (byte) 45;
    sourceArray1[11] = (byte) 206;
    sourceArray1[43] = (byte) 133;
    sourceArray1[12] = (byte) 46;
    sourceArray1[14] = (byte) 213;
    sourceArray1[15] = (byte) 140;
    sourceArray1[16 /*0x10*/] = (byte) 56;
    sourceArray1[17] = (byte) 31 /*0x1F*/;
    sourceArray1[9] = (byte) 143;
    sourceArray1[19] = (byte) 171;
    sourceArray1[8] = (byte) 211;
    sourceArray1[45] = (byte) 92;
    sourceArray1[33] = (byte) 124;
    sourceArray1[18] = (byte) 52;
    sourceArray1[24] = (byte) 117;
    sourceArray1[25] = (byte) 215;
    sourceArray1[28] = (byte) 30;
    sourceArray1[7] = (byte) 39;
    sourceArray1[34] = (byte) 141;
    sourceArray1[3] = (byte) 172;
    sourceArray1[32 /*0x20*/] = (byte) 230;
    sourceArray1[13] = (byte) 161;
    sourceArray1[22] = (byte) 127 /*0x7F*/;
    sourceArray1[0] = (byte) 77;
    sourceArray1[27] = (byte) 141;
    sourceArray1[38] = (byte) 97;
    sourceArray1[36] = (byte) 13;
    sourceArray1[35] = (byte) 136;
    sourceArray1[44] = (byte) 176 /*0xB0*/;
    sourceArray1[39] = (byte) 197;
    sourceArray1[40] = (byte) 213;
    sourceArray1[41] = (byte) 156;
    sourceArray1[37] = (byte) 112 /*0x70*/;
    sourceArray1[2] = (byte) 181;
    sourceArray1[21] = (byte) 25;
    sourceArray1[4] = (byte) 231;
    sourceArray1[46] = (byte) 246;
    sourceArray1[10] = (byte) 228;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 11,
      (byte) 167,
      (byte) 43,
      (byte) 101,
      (byte) 118,
      (byte) 76,
      (byte) 221,
      (byte) 80 /*0x50*/,
      (byte) 227,
      (byte) 77,
      (byte) 111,
      (byte) 130,
      (byte) 13,
      (byte) 249,
      (byte) 162,
      (byte) 40,
      (byte) 180,
      (byte) 114,
      (byte) 112 /*0x70*/,
      (byte) 167,
      (byte) 78,
      (byte) 60,
      (byte) 149,
      (byte) 247,
      (byte) 65,
      (byte) 59,
      (byte) 225,
      (byte) 70,
      (byte) 58,
      (byte) 131,
      (byte) 145,
      (byte) 67,
      (byte) 150,
      (byte) 110,
      (byte) 153,
      (byte) 197,
      (byte) 117,
      (byte) 160 /*0xA0*/,
      (byte) 197,
      (byte) 116,
      (byte) 253,
      (byte) 60,
      (byte) 242,
      (byte) 113,
      (byte) 165,
      (byte) 185,
      (byte) 159,
      (byte) 5
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13907(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[26] = (byte) 107;
    sourceArray1[24] = (byte) 110;
    sourceArray1[2] = (byte) 160 /*0xA0*/;
    sourceArray1[3] = (byte) 238;
    sourceArray1[39] = (byte) 4;
    sourceArray1[5] = (byte) 235;
    sourceArray1[8] = (byte) 86;
    sourceArray1[21] = (byte) 28;
    sourceArray1[43] = (byte) 36;
    sourceArray1[9] = (byte) 52;
    sourceArray1[7] = (byte) 125;
    sourceArray1[31 /*0x1F*/] = (byte) 106;
    sourceArray1[33] = (byte) 164;
    sourceArray1[11] = (byte) 243;
    sourceArray1[14] = (byte) 33;
    sourceArray1[4] = (byte) 131;
    sourceArray1[16 /*0x10*/] = (byte) 89;
    sourceArray1[17] = (byte) 163;
    sourceArray1[18] = (byte) 5;
    sourceArray1[19] = (byte) 47;
    sourceArray1[32 /*0x20*/] = (byte) 199;
    sourceArray1[27] = (byte) 184;
    sourceArray1[22] = (byte) 186;
    sourceArray1[45] = (byte) 29;
    sourceArray1[25] = (byte) 142;
    sourceArray1[23] = (byte) 180;
    sourceArray1[34] = (byte) 156;
    sourceArray1[44] = (byte) 164;
    sourceArray1[28] = (byte) 124;
    sourceArray1[29] = byte.MaxValue;
    sourceArray1[10] = (byte) 244;
    sourceArray1[42] = (byte) 62;
    sourceArray1[0] = (byte) 68;
    sourceArray1[1] = (byte) 229;
    sourceArray1[37] = (byte) 192 /*0xC0*/;
    sourceArray1[35] = (byte) 7;
    sourceArray1[15] = (byte) 220;
    sourceArray1[20] = (byte) 83;
    sourceArray1[36] = byte.MaxValue;
    sourceArray1[13] = (byte) 46;
    sourceArray1[40] = (byte) 121;
    sourceArray1[41] = (byte) 22;
    sourceArray1[30] = (byte) 121;
    sourceArray1[38] = (byte) 111;
    sourceArray1[6] = (byte) 198;
    sourceArray1[12] = (byte) 30;
    sourceArray1[46] = (byte) 27;
    sourceArray1[47] = (byte) 36;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 190,
      (byte) 89,
      (byte) 236,
      (byte) 149,
      (byte) 161,
      (byte) 50,
      (byte) 70,
      (byte) 227,
      (byte) 177,
      (byte) 100,
      (byte) 2,
      (byte) 45,
      (byte) 135,
      (byte) 184,
      (byte) 55,
      (byte) 225,
      (byte) 165,
      (byte) 136,
      (byte) 194,
      (byte) 155,
      (byte) 242,
      (byte) 36,
      (byte) 0,
      (byte) 171,
      (byte) 159,
      (byte) 93,
      (byte) 238,
      (byte) 83,
      (byte) 214,
      (byte) 95,
      (byte) 73,
      (byte) 204,
      (byte) 225,
      (byte) 58,
      (byte) 51,
      (byte) 77,
      (byte) 40,
      (byte) 71,
      (byte) 129,
      (byte) 118,
      (byte) 221,
      (byte) 25,
      (byte) 137,
      (byte) 97,
      (byte) 37,
      (byte) 227,
      (byte) 85,
      (byte) 98
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13908(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 161,
      (byte) 146,
      (byte) 123,
      (byte) 159,
      (byte) 160 /*0xA0*/,
      (byte) 173,
      (byte) 137,
      (byte) 92,
      (byte) 228,
      (byte) 73,
      (byte) 148,
      (byte) 220,
      (byte) 47,
      (byte) 129,
      (byte) 56,
      (byte) 235,
      (byte) 1,
      (byte) 145,
      (byte) 35,
      (byte) 169,
      (byte) 118,
      (byte) 246,
      (byte) 29,
      (byte) 60,
      (byte) 202,
      (byte) 88,
      (byte) 185,
      (byte) 123,
      (byte) 73,
      (byte) 39,
      (byte) 247,
      (byte) 44,
      (byte) 245,
      (byte) 95,
      (byte) 209,
      (byte) 181,
      (byte) 167,
      (byte) 96 /*0x60*/,
      (byte) 37,
      (byte) 234,
      (byte) 86,
      (byte) 88,
      (byte) 158,
      (byte) 42,
      (byte) 73,
      (byte) 55,
      (byte) 17,
      (byte) 248
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 47,
      (byte) 38,
      (byte) 81,
      (byte) 239,
      (byte) 157,
      (byte) 70,
      (byte) 201,
      (byte) 127 /*0x7F*/,
      (byte) 232,
      (byte) 122,
      (byte) 4,
      (byte) 123,
      (byte) 90,
      (byte) 11,
      (byte) 104,
      (byte) 167,
      (byte) 224 /*0xE0*/,
      (byte) 238,
      (byte) 149,
      (byte) 210,
      (byte) 117,
      (byte) 175,
      (byte) 243,
      (byte) 208 /*0xD0*/,
      (byte) 171,
      (byte) 223,
      (byte) 183,
      (byte) 35,
      (byte) 130,
      (byte) 139,
      (byte) 7,
      (byte) 24,
      (byte) 218,
      (byte) 58,
      (byte) 253,
      (byte) 202,
      (byte) 77,
      (byte) 212,
      (byte) 91,
      (byte) 98,
      (byte) 4,
      (byte) 17,
      (byte) 114,
      (byte) 120,
      (byte) 159,
      (byte) 37,
      (byte) 74,
      (byte) 138
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[47];
    byte[] response2 = new byte[47];
    Array.Copy((Array) sc_13895.sspq, 38, (Array) numArray2, 0, 47);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13895.sspr, 38, (Array) numArray2, 0, 47);
    for (int index = 0; index < numArray2.Length; ++index)
    {
      if ((int) numArray2[index] != (int) response2[index])
      {
        key.TagValue = (int) response2[index];
        break;
      }
    }
    return BitConverter.ToInt32(response1, 0) ^ BitConverter.ToInt32(numArray1, 0) ^ k;
  }

  internal static int ssp_appserver_13909(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 66,
      (byte) 72,
      (byte) 201,
      (byte) 60,
      (byte) 114,
      (byte) 234,
      (byte) 13,
      (byte) 30,
      (byte) 93,
      (byte) 83,
      (byte) 128 /*0x80*/,
      (byte) 74,
      (byte) 66,
      (byte) 1,
      (byte) 50,
      (byte) 222,
      (byte) 72,
      (byte) 169,
      (byte) 176 /*0xB0*/,
      (byte) 129,
      (byte) 97,
      (byte) 235,
      (byte) 137,
      (byte) 65,
      (byte) 219,
      (byte) 22,
      (byte) 142,
      (byte) 65,
      (byte) 9,
      (byte) 215,
      (byte) 117,
      (byte) 27,
      (byte) 18,
      (byte) 65,
      (byte) 4,
      (byte) 121,
      (byte) 102,
      (byte) 141,
      (byte) 160 /*0xA0*/,
      (byte) 183,
      (byte) 216,
      (byte) 101,
      (byte) 134,
      (byte) 211,
      (byte) 192 /*0xC0*/,
      (byte) 29,
      (byte) 72,
      (byte) 219
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 65,
      (byte) 181,
      (byte) 89,
      (byte) 100,
      (byte) 133,
      (byte) 180,
      (byte) 15,
      (byte) 229,
      (byte) 185,
      (byte) 157,
      (byte) 113,
      (byte) 75,
      (byte) 44,
      (byte) 90,
      (byte) 230,
      (byte) 139,
      (byte) 208 /*0xD0*/,
      (byte) 248,
      (byte) 134,
      (byte) 11,
      (byte) 13,
      (byte) 141,
      (byte) 74,
      (byte) 232,
      (byte) 76,
      (byte) 150,
      (byte) 98,
      (byte) 158,
      (byte) 234,
      (byte) 174,
      (byte) 79,
      (byte) 226,
      (byte) 202,
      (byte) 111,
      (byte) 199,
      (byte) 108,
      (byte) 138,
      (byte) 230,
      (byte) 243,
      (byte) 252,
      (byte) 17,
      (byte) 78,
      (byte) 170,
      (byte) 47,
      (byte) 216,
      (byte) 214,
      (byte) 216,
      (byte) 141
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13910(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 95,
      (byte) 239,
      (byte) 87,
      (byte) 190,
      (byte) 135,
      (byte) 96 /*0x60*/,
      (byte) 91,
      (byte) 92,
      (byte) 203,
      (byte) 21,
      (byte) 134,
      (byte) 221,
      (byte) 101,
      (byte) 55,
      (byte) 0,
      (byte) 253,
      (byte) 185,
      (byte) 120,
      (byte) 187,
      (byte) 8,
      (byte) 249,
      (byte) 169,
      (byte) 251,
      (byte) 100,
      (byte) 182,
      (byte) 129,
      (byte) 189,
      (byte) 206,
      (byte) 178,
      (byte) 125,
      (byte) 158,
      (byte) 217,
      (byte) 61,
      (byte) 243,
      (byte) 179,
      (byte) 82,
      (byte) 172,
      (byte) 186,
      (byte) 222,
      (byte) 45,
      (byte) 69,
      (byte) 91,
      (byte) 12,
      (byte) 79,
      byte.MaxValue,
      (byte) 246,
      (byte) 90,
      (byte) 229
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[29] = (byte) 121;
    sourceArray2[11] = (byte) 99;
    sourceArray2[41] = (byte) 212;
    sourceArray2[26] = (byte) 232;
    sourceArray2[4] = (byte) 48 /*0x30*/;
    sourceArray2[9] = (byte) 187;
    sourceArray2[24] = (byte) 218;
    sourceArray2[43] = (byte) 30;
    sourceArray2[6] = (byte) 112 /*0x70*/;
    sourceArray2[8] = (byte) 131;
    sourceArray2[30] = (byte) 20;
    sourceArray2[45] = (byte) 12;
    sourceArray2[12] = (byte) 240 /*0xF0*/;
    sourceArray2[21] = (byte) 122;
    sourceArray2[14] = (byte) 67;
    sourceArray2[15] = (byte) 32 /*0x20*/;
    sourceArray2[16 /*0x10*/] = (byte) 100;
    sourceArray2[17] = (byte) 46;
    sourceArray2[18] = (byte) 77;
    sourceArray2[19] = (byte) 75;
    sourceArray2[5] = (byte) 215;
    sourceArray2[38] = (byte) 20;
    sourceArray2[3] = (byte) 174;
    sourceArray2[23] = (byte) 116;
    sourceArray2[1] = (byte) 152;
    sourceArray2[25] = (byte) 95;
    sourceArray2[27] = (byte) 208 /*0xD0*/;
    sourceArray2[10] = (byte) 0;
    sourceArray2[0] = (byte) 221;
    sourceArray2[33] = (byte) 133;
    sourceArray2[44] = (byte) 163;
    sourceArray2[31 /*0x1F*/] = (byte) 1;
    sourceArray2[32 /*0x20*/] = (byte) 103;
    sourceArray2[13] = (byte) 23;
    sourceArray2[34] = (byte) 187;
    sourceArray2[35] = (byte) 207;
    sourceArray2[20] = (byte) 85;
    sourceArray2[2] = (byte) 229;
    sourceArray2[22] = (byte) 68;
    sourceArray2[39] = (byte) 147;
    sourceArray2[40] = (byte) 99;
    sourceArray2[36] = (byte) 226;
    sourceArray2[28] = (byte) 230;
    sourceArray2[37] = (byte) 162;
    sourceArray2[7] = (byte) 16 /*0x10*/;
    sourceArray2[42] = (byte) 216;
    sourceArray2[46] = (byte) 122;
    sourceArray2[47] = (byte) 205;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[39];
    byte[] response2 = new byte[39];
    Array.Copy((Array) sc_13895.sspq, 85, (Array) numArray2, 0, 39);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13895.sspr, 85, (Array) numArray2, 0, 39);
    for (int index = 0; index < numArray2.Length; ++index)
    {
      if ((int) numArray2[index] != (int) response2[index])
      {
        key.TagValue = (int) response2[index];
        break;
      }
    }
    return BitConverter.ToInt32(response1, 0) ^ BitConverter.ToInt32(numArray1, 0) ^ k;
  }

  internal static int ssp_appserver_13911(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 227,
      (byte) 160 /*0xA0*/,
      (byte) 53,
      (byte) 227,
      (byte) 23,
      (byte) 44,
      (byte) 106,
      (byte) 247,
      (byte) 192 /*0xC0*/,
      (byte) 179,
      (byte) 220,
      (byte) 226,
      (byte) 11,
      (byte) 61,
      (byte) 195,
      (byte) 59,
      (byte) 218,
      (byte) 75,
      (byte) 245,
      (byte) 192 /*0xC0*/,
      (byte) 151,
      (byte) 176 /*0xB0*/,
      (byte) 98,
      (byte) 162,
      (byte) 59,
      (byte) 115,
      (byte) 123,
      (byte) 222,
      (byte) 163,
      (byte) 83,
      (byte) 23,
      (byte) 140,
      (byte) 118,
      (byte) 123,
      (byte) 49,
      (byte) 242,
      (byte) 118,
      (byte) 163,
      (byte) 54,
      (byte) 106,
      (byte) 225,
      (byte) 202,
      (byte) 66,
      (byte) 87,
      (byte) 16 /*0x10*/,
      (byte) 251,
      (byte) 201,
      (byte) 212
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 252,
      (byte) 132,
      (byte) 79,
      (byte) 114,
      (byte) 125,
      (byte) 106,
      (byte) 173,
      (byte) 34,
      (byte) 157,
      (byte) 130,
      (byte) 16 /*0x10*/,
      (byte) 159,
      (byte) 37,
      (byte) 3,
      (byte) 138,
      (byte) 49,
      (byte) 97,
      (byte) 162,
      (byte) 135,
      (byte) 205,
      (byte) 187,
      (byte) 56,
      (byte) 130,
      (byte) 230,
      (byte) 47,
      (byte) 200,
      (byte) 182,
      (byte) 71,
      (byte) 47,
      (byte) 231,
      (byte) 179,
      (byte) 109,
      (byte) 239,
      (byte) 190,
      (byte) 149,
      (byte) 86,
      (byte) 234,
      (byte) 68,
      (byte) 246,
      (byte) 172,
      (byte) 233,
      (byte) 199,
      (byte) 63 /*0x3F*/,
      (byte) 77,
      (byte) 149,
      (byte) 118,
      (byte) 160 /*0xA0*/,
      (byte) 254
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13912(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 165,
      (byte) 185,
      (byte) 50,
      (byte) 61,
      (byte) 147,
      (byte) 168,
      (byte) 164,
      (byte) 176 /*0xB0*/,
      (byte) 139,
      (byte) 84,
      (byte) 108,
      (byte) 155,
      (byte) 80 /*0x50*/,
      (byte) 108,
      (byte) 143,
      (byte) 12,
      (byte) 208 /*0xD0*/,
      (byte) 197,
      (byte) 54,
      (byte) 20,
      (byte) 139,
      (byte) 222,
      (byte) 205,
      (byte) 110,
      (byte) 11,
      (byte) 146,
      (byte) 64 /*0x40*/,
      (byte) 169,
      (byte) 167,
      (byte) 55,
      (byte) 161,
      (byte) 183,
      (byte) 117,
      (byte) 58,
      (byte) 209,
      (byte) 193,
      (byte) 254,
      (byte) 180,
      (byte) 227,
      (byte) 251,
      (byte) 145,
      (byte) 186,
      (byte) 86,
      (byte) 118,
      (byte) 213,
      (byte) 187,
      (byte) 192 /*0xC0*/,
      (byte) 142
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[36] = (byte) 33;
    sourceArray2[31 /*0x1F*/] = (byte) 118;
    sourceArray2[2] = (byte) 8;
    sourceArray2[3] = (byte) 89;
    sourceArray2[4] = (byte) 54;
    sourceArray2[12] = (byte) 72;
    sourceArray2[5] = (byte) 41;
    sourceArray2[13] = (byte) 9;
    sourceArray2[41] = (byte) 146;
    sourceArray2[9] = (byte) 27;
    sourceArray2[10] = (byte) 225;
    sourceArray2[11] = (byte) 191;
    sourceArray2[7] = (byte) 88;
    sourceArray2[40] = (byte) 153;
    sourceArray2[29] = (byte) 192 /*0xC0*/;
    sourceArray2[15] = (byte) 111;
    sourceArray2[16 /*0x10*/] = (byte) 215;
    sourceArray2[46] = (byte) 29;
    sourceArray2[43] = (byte) 94;
    sourceArray2[18] = (byte) 185;
    sourceArray2[20] = (byte) 131;
    sourceArray2[14] = (byte) 246;
    sourceArray2[22] = (byte) 159;
    sourceArray2[23] = (byte) 161;
    sourceArray2[27] = (byte) 2;
    sourceArray2[25] = (byte) 200;
    sourceArray2[6] = (byte) 230;
    sourceArray2[32 /*0x20*/] = (byte) 177;
    sourceArray2[28] = (byte) 16 /*0x10*/;
    sourceArray2[34] = (byte) 62;
    sourceArray2[30] = (byte) 73;
    sourceArray2[35] = (byte) 195;
    sourceArray2[19] = (byte) 143;
    sourceArray2[33] = (byte) 118;
    sourceArray2[26] = (byte) 47;
    sourceArray2[21] = (byte) 6;
    sourceArray2[24] = (byte) 182;
    sourceArray2[37] = (byte) 85;
    sourceArray2[38] = (byte) 110;
    sourceArray2[0] = (byte) 76;
    sourceArray2[8] = (byte) 235;
    sourceArray2[17] = (byte) 8;
    sourceArray2[42] = (byte) 179;
    sourceArray2[39] = (byte) 88;
    sourceArray2[44] = (byte) 165;
    sourceArray2[45] = (byte) 32 /*0x20*/;
    sourceArray2[1] = (byte) 18;
    sourceArray2[47] = (byte) 40;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
