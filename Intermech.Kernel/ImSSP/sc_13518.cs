// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_13518
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_13518
{
  private static byte[] sspq = new byte[115]
  {
    (byte) 245,
    (byte) 121,
    (byte) 28,
    (byte) 11,
    (byte) 153,
    (byte) 74,
    (byte) 115,
    (byte) 220,
    (byte) 111,
    (byte) 13,
    (byte) 66,
    (byte) 241,
    (byte) 190,
    (byte) 229,
    (byte) 141,
    (byte) 243,
    (byte) 137,
    (byte) 232,
    (byte) 240 /*0xF0*/,
    (byte) 50,
    (byte) 124,
    (byte) 181,
    (byte) 249,
    (byte) 173,
    (byte) 74,
    (byte) 116,
    (byte) 119,
    (byte) 57,
    (byte) 180,
    (byte) 60,
    (byte) 134,
    (byte) 134,
    byte.MaxValue,
    (byte) 195,
    (byte) 220,
    (byte) 42,
    (byte) 67,
    (byte) 230,
    (byte) 115,
    (byte) 35,
    (byte) 22,
    (byte) 105,
    (byte) 208 /*0xD0*/,
    (byte) 139,
    (byte) 145,
    (byte) 204,
    (byte) 84,
    (byte) 14,
    (byte) 94,
    (byte) 168,
    (byte) 202,
    (byte) 81,
    (byte) 24,
    (byte) 194,
    (byte) 132,
    (byte) 132,
    (byte) 25,
    (byte) 118,
    (byte) 252,
    (byte) 254,
    (byte) 215,
    (byte) 98,
    (byte) 68,
    (byte) 89,
    (byte) 113,
    (byte) 175,
    (byte) 114,
    (byte) 2,
    (byte) 83,
    (byte) 41,
    (byte) 43,
    (byte) 98,
    (byte) 59,
    (byte) 183,
    (byte) 93,
    (byte) 138,
    (byte) 220,
    (byte) 158,
    (byte) 194,
    (byte) 204,
    (byte) 47,
    (byte) 62,
    (byte) 182,
    (byte) 153,
    (byte) 236,
    (byte) 142,
    (byte) 45,
    (byte) 31 /*0x1F*/,
    (byte) 73,
    (byte) 248,
    (byte) 127 /*0x7F*/,
    (byte) 137,
    (byte) 178,
    (byte) 130,
    (byte) 15,
    (byte) 171,
    (byte) 62,
    (byte) 154,
    (byte) 96 /*0x60*/,
    (byte) 110,
    (byte) 68,
    (byte) 181,
    (byte) 125,
    (byte) 48 /*0x30*/,
    (byte) 135,
    (byte) 232,
    (byte) 35,
    (byte) 157,
    (byte) 0,
    (byte) 248,
    (byte) 242,
    (byte) 177,
    (byte) 24,
    (byte) 146,
    (byte) 65
  };
  private static byte[] sspr = new byte[115]
  {
    (byte) 198,
    (byte) 12,
    (byte) 150,
    (byte) 54,
    (byte) 175,
    (byte) 82,
    (byte) 155,
    (byte) 180,
    (byte) 60,
    (byte) 164,
    (byte) 108,
    (byte) 192 /*0xC0*/,
    (byte) 93,
    (byte) 186,
    (byte) 222,
    (byte) 30,
    (byte) 154,
    (byte) 179,
    (byte) 86,
    (byte) 218,
    (byte) 41,
    (byte) 138,
    (byte) 110,
    (byte) 211,
    (byte) 127 /*0x7F*/,
    (byte) 92,
    (byte) 134,
    (byte) 101,
    (byte) 6,
    (byte) 232,
    (byte) 45,
    (byte) 170,
    (byte) 253,
    (byte) 162,
    (byte) 129,
    (byte) 159,
    (byte) 134,
    (byte) 174,
    (byte) 184,
    (byte) 205,
    (byte) 243,
    (byte) 41,
    (byte) 33,
    (byte) 80 /*0x50*/,
    (byte) 247,
    (byte) 27,
    (byte) 83,
    (byte) 212,
    (byte) 203,
    (byte) 232,
    (byte) 90,
    (byte) 229,
    (byte) 249,
    (byte) 30,
    (byte) 20,
    (byte) 178,
    (byte) 152,
    (byte) 247,
    (byte) 189,
    (byte) 239,
    (byte) 132,
    (byte) 172,
    (byte) 135,
    (byte) 98,
    (byte) 138,
    (byte) 252,
    (byte) 94,
    (byte) 182,
    (byte) 18,
    (byte) 13,
    (byte) 205,
    (byte) 21,
    (byte) 104,
    (byte) 210,
    (byte) 113,
    (byte) 88,
    (byte) 88,
    (byte) 21,
    (byte) 202,
    (byte) 133,
    (byte) 212,
    (byte) 141,
    (byte) 251,
    (byte) 153,
    (byte) 123,
    (byte) 61,
    (byte) 159,
    (byte) 5,
    (byte) 15,
    (byte) 134,
    (byte) 223,
    (byte) 203,
    (byte) 98,
    (byte) 204,
    (byte) 49,
    (byte) 249,
    (byte) 160 /*0xA0*/,
    (byte) 230,
    (byte) 94,
    (byte) 84,
    (byte) 226,
    (byte) 70,
    (byte) 42,
    (byte) 168,
    (byte) 161,
    (byte) 142,
    (byte) 9,
    (byte) 85,
    (byte) 59,
    (byte) 115,
    (byte) 58,
    (byte) 183,
    (byte) 69,
    (byte) 62,
    (byte) 124
  };

  internal static string ssp_appserver_13519()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[63 /*0x3F*/];
      byte[] numArray2 = new byte[55];
      numArray2[41] = (byte) 132;
      numArray2[35] = (byte) 244;
      numArray2[0] = (byte) 92;
      numArray2[5] = (byte) 168;
      numArray2[34] = (byte) 206;
      numArray2[39] = (byte) 178;
      numArray2[6] = (byte) 219;
      numArray2[7] = (byte) 249;
      numArray2[8] = (byte) 100;
      numArray2[50] = (byte) 82;
      numArray2[21] = (byte) 82;
      numArray2[49] = (byte) 249;
      numArray2[1] = (byte) 186;
      numArray2[13] = (byte) 201;
      numArray2[12] = (byte) 231;
      numArray2[3] = (byte) 77;
      numArray2[16 /*0x10*/] = (byte) 71;
      numArray2[30] = (byte) 59;
      numArray2[10] = (byte) 249;
      numArray2[28] = (byte) 77;
      numArray2[20] = (byte) 248;
      numArray2[9] = (byte) 24;
      numArray2[22] = (byte) 44;
      numArray2[23] = (byte) 116;
      numArray2[24] = (byte) 149;
      numArray2[25] = (byte) 198;
      numArray2[26] = (byte) 142;
      numArray2[51] = (byte) 232;
      numArray2[37] = (byte) 248;
      numArray2[29] = (byte) 131;
      numArray2[44] = (byte) 75;
      numArray2[31 /*0x1F*/] = (byte) 85;
      numArray2[32 /*0x20*/] = (byte) 77;
      numArray2[15] = (byte) 33;
      numArray2[14] = (byte) 129;
      numArray2[27] = (byte) 22;
      numArray2[36] = (byte) 190;
      numArray2[40] = (byte) 67;
      numArray2[18] = (byte) 51;
      numArray2[4] = (byte) 169;
      numArray2[54] = (byte) 190;
      numArray2[38] = (byte) 42;
      numArray2[19] = (byte) 18;
      numArray2[43] = (byte) 70;
      numArray2[17] = (byte) 253;
      numArray2[45] = (byte) 32 /*0x20*/;
      numArray2[46] = (byte) 157;
      numArray2[47] = (byte) 37;
      numArray2[48 /*0x30*/] = (byte) 77;
      numArray2[42] = (byte) 175;
      numArray2[2] = (byte) 2;
      numArray2[11] = (byte) 229;
      numArray2[52] = (byte) 107;
      numArray2[53] = (byte) 9;
      numArray2[33] = (byte) 143;
      byte[] numArray3 = new byte[55];
      numArray3[35] = (byte) 173;
      numArray3[1] = (byte) 29;
      numArray3[2] = (byte) 189;
      numArray3[9] = (byte) 87;
      numArray3[0] = (byte) 173;
      numArray3[5] = (byte) 32 /*0x20*/;
      numArray3[24] = (byte) 187;
      numArray3[12] = (byte) 239;
      numArray3[33] = (byte) 118;
      numArray3[23] = (byte) 136;
      numArray3[10] = (byte) 229;
      numArray3[43] = (byte) 249;
      numArray3[18] = (byte) 63 /*0x3F*/;
      numArray3[15] = (byte) 14;
      numArray3[6] = (byte) 246;
      numArray3[26] = (byte) 57;
      numArray3[16 /*0x10*/] = (byte) 90;
      numArray3[25] = (byte) 28;
      numArray3[8] = (byte) 50;
      numArray3[7] = (byte) 78;
      numArray3[51] = (byte) 223;
      numArray3[21] = (byte) 170;
      numArray3[22] = (byte) 155;
      numArray3[32 /*0x20*/] = (byte) 136;
      numArray3[40] = (byte) 0;
      numArray3[3] = (byte) 103;
      numArray3[47] = (byte) 241;
      numArray3[27] = (byte) 193;
      numArray3[28] = (byte) 0;
      numArray3[29] = (byte) 36;
      numArray3[30] = (byte) 111;
      numArray3[19] = (byte) 6;
      numArray3[17] = (byte) 213;
      numArray3[11] = (byte) 2;
      numArray3[34] = (byte) 110;
      numArray3[31 /*0x1F*/] = (byte) 220;
      numArray3[36] = (byte) 49;
      numArray3[37] = (byte) 70;
      numArray3[38] = (byte) 14;
      numArray3[39] = (byte) 76;
      numArray3[13] = (byte) 237;
      numArray3[53] = (byte) 18;
      numArray3[42] = (byte) 221;
      numArray3[20] = (byte) 186;
      numArray3[44] = (byte) 175;
      numArray3[45] = (byte) 39;
      numArray3[46] = (byte) 177;
      numArray3[54] = (byte) 183;
      numArray3[48 /*0x30*/] = (byte) 141;
      numArray3[41] = (byte) 129;
      numArray3[50] = (byte) 104;
      numArray3[14] = (byte) 199;
      numArray3[52] = (byte) 130;
      numArray3[4] = (byte) 183;
      numArray3[49] = (byte) 188;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[8];
      numArray4[7] = (byte) 77;
      numArray4[0] = (byte) 227;
      numArray4[2] = (byte) 213;
      numArray4[3] = (byte) 112 /*0x70*/;
      numArray4[4] = (byte) 242;
      numArray4[1] = (byte) 86;
      numArray4[6] = (byte) 129;
      numArray4[5] = (byte) 152;
      byte[] numArray5 = new byte[8];
      numArray5[7] = (byte) 45;
      numArray5[1] = (byte) 139;
      numArray5[2] = (byte) 58;
      numArray5[3] = (byte) 228;
      numArray5[5] = (byte) 226;
      numArray5[0] = (byte) 249;
      numArray5[6] = (byte) 33;
      numArray5[4] = (byte) 223;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 8);
      for (int index = 0; index < 8; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[63 /*0x3F*/];
    byte[] numArray7 = new byte[55]
    {
      (byte) 57,
      (byte) 181,
      (byte) 254,
      (byte) 31 /*0x1F*/,
      (byte) 213,
      (byte) 203,
      (byte) 147,
      (byte) 231,
      (byte) 167,
      (byte) 183,
      (byte) 119,
      (byte) 145,
      (byte) 232,
      (byte) 157,
      (byte) 143,
      (byte) 53,
      (byte) 236,
      (byte) 185,
      (byte) 27,
      (byte) 47,
      (byte) 251,
      (byte) 227,
      (byte) 136,
      (byte) 1,
      (byte) 29,
      (byte) 149,
      (byte) 221,
      (byte) 138,
      (byte) 80 /*0x50*/,
      (byte) 159,
      (byte) 174,
      (byte) 36,
      (byte) 130,
      (byte) 61,
      (byte) 71,
      (byte) 151,
      (byte) 186,
      (byte) 41,
      (byte) 45,
      (byte) 12,
      (byte) 213,
      (byte) 127 /*0x7F*/,
      (byte) 105,
      (byte) 71,
      (byte) 136,
      (byte) 59,
      (byte) 49,
      (byte) 22,
      (byte) 16 /*0x10*/,
      (byte) 93,
      (byte) 202,
      (byte) 109,
      (byte) 47,
      (byte) 64 /*0x40*/,
      (byte) 96 /*0x60*/
    };
    byte[] numArray8 = new byte[55];
    numArray8[51] = (byte) 0;
    numArray8[26] = (byte) 37;
    numArray8[10] = (byte) 233;
    numArray8[3] = (byte) 234;
    numArray8[20] = (byte) 51;
    numArray8[25] = (byte) 191;
    numArray8[7] = (byte) 36;
    numArray8[12] = (byte) 41;
    numArray8[4] = (byte) 237;
    numArray8[17] = (byte) 133;
    numArray8[2] = (byte) 76;
    numArray8[37] = (byte) 177;
    numArray8[41] = (byte) 55;
    numArray8[34] = (byte) 176 /*0xB0*/;
    numArray8[38] = (byte) 205;
    numArray8[15] = (byte) 171;
    numArray8[16 /*0x10*/] = (byte) 184;
    numArray8[8] = (byte) 249;
    numArray8[18] = (byte) 155;
    numArray8[6] = (byte) 77;
    numArray8[13] = (byte) 85;
    numArray8[46] = (byte) 169;
    numArray8[1] = (byte) 147;
    numArray8[23] = (byte) 97;
    numArray8[24] = (byte) 210;
    numArray8[19] = (byte) 28;
    numArray8[36] = (byte) 164;
    numArray8[27] = (byte) 33;
    numArray8[28] = (byte) 217;
    numArray8[29] = (byte) 205;
    numArray8[30] = (byte) 213;
    numArray8[31 /*0x1F*/] = (byte) 149;
    numArray8[32 /*0x20*/] = (byte) 101;
    numArray8[21] = (byte) 241;
    numArray8[5] = (byte) 166;
    numArray8[35] = (byte) 201;
    numArray8[22] = (byte) 84;
    numArray8[40] = (byte) 38;
    numArray8[11] = (byte) 181;
    numArray8[39] = (byte) 39;
    numArray8[0] = (byte) 159;
    numArray8[9] = (byte) 92;
    numArray8[42] = (byte) 25;
    numArray8[43] = (byte) 184;
    numArray8[44] = (byte) 175;
    numArray8[45] = (byte) 140;
    numArray8[47] = (byte) 46;
    numArray8[33] = (byte) 110;
    numArray8[14] = (byte) 158;
    numArray8[49] = (byte) 127 /*0x7F*/;
    numArray8[50] = (byte) 19;
    numArray8[48 /*0x30*/] = (byte) 243;
    numArray8[52] = (byte) 249;
    numArray8[53] = (byte) 75;
    numArray8[54] = (byte) 27;
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[8]
    {
      (byte) 131,
      (byte) 31 /*0x1F*/,
      (byte) 219,
      (byte) 38,
      (byte) 142,
      (byte) 165,
      (byte) 52,
      (byte) 199
    };
    byte[] numArray10 = new byte[8]
    {
      (byte) 240 /*0xF0*/,
      (byte) 211,
      (byte) 216,
      (byte) 231,
      (byte) 233,
      (byte) 59,
      (byte) 156,
      (byte) 232
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 8);
    for (int index = 0; index < 8; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_13520()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[56];
      byte[] numArray2 = new byte[55]
      {
        (byte) 140,
        (byte) 189,
        (byte) 154,
        (byte) 158,
        (byte) 239,
        (byte) 141,
        (byte) 147,
        (byte) 25,
        (byte) 1,
        (byte) 67,
        (byte) 180,
        (byte) 221,
        (byte) 7,
        (byte) 97,
        (byte) 15,
        (byte) 204,
        (byte) 96 /*0x60*/,
        (byte) 211,
        (byte) 92,
        (byte) 247,
        (byte) 20,
        (byte) 127 /*0x7F*/,
        (byte) 57,
        (byte) 8,
        (byte) 18,
        (byte) 249,
        (byte) 62,
        (byte) 66,
        (byte) 44,
        (byte) 120,
        (byte) 215,
        (byte) 97,
        (byte) 184,
        (byte) 90,
        (byte) 140,
        (byte) 37,
        (byte) 109,
        (byte) 66,
        (byte) 50,
        (byte) 89,
        (byte) 81,
        (byte) 194,
        (byte) 234,
        (byte) 64 /*0x40*/,
        (byte) 106,
        (byte) 253,
        (byte) 30,
        (byte) 45,
        (byte) 150,
        (byte) 104,
        (byte) 103,
        (byte) 138,
        (byte) 111,
        (byte) 214,
        (byte) 80 /*0x50*/
      };
      byte[] numArray3 = new byte[55];
      numArray3[25] = (byte) 92;
      numArray3[31 /*0x1F*/] = (byte) 38;
      numArray3[37] = (byte) 162;
      numArray3[12] = (byte) 160 /*0xA0*/;
      numArray3[32 /*0x20*/] = (byte) 137;
      numArray3[48 /*0x30*/] = (byte) 180;
      numArray3[6] = (byte) 91;
      numArray3[39] = (byte) 4;
      numArray3[8] = (byte) 215;
      numArray3[4] = (byte) 252;
      numArray3[10] = (byte) 127 /*0x7F*/;
      numArray3[29] = (byte) 164;
      numArray3[11] = (byte) 167;
      numArray3[19] = (byte) 120;
      numArray3[26] = (byte) 239;
      numArray3[35] = (byte) 151;
      numArray3[16 /*0x10*/] = (byte) 179;
      numArray3[17] = (byte) 10;
      numArray3[45] = (byte) 213;
      numArray3[34] = (byte) 63 /*0x3F*/;
      numArray3[20] = (byte) 222;
      numArray3[1] = (byte) 238;
      numArray3[22] = (byte) 196;
      numArray3[23] = (byte) 41;
      numArray3[24] = (byte) 123;
      numArray3[21] = (byte) 197;
      numArray3[42] = (byte) 77;
      numArray3[27] = (byte) 62;
      numArray3[0] = (byte) 1;
      numArray3[9] = (byte) 206;
      numArray3[18] = (byte) 26;
      numArray3[46] = (byte) 87;
      numArray3[3] = (byte) 85;
      numArray3[33] = (byte) 131;
      numArray3[49] = (byte) 3;
      numArray3[7] = (byte) 157;
      numArray3[36] = (byte) 230;
      numArray3[5] = (byte) 183;
      numArray3[38] = (byte) 71;
      numArray3[15] = (byte) 190;
      numArray3[40] = (byte) 250;
      numArray3[41] = (byte) 201;
      numArray3[28] = (byte) 118;
      numArray3[43] = (byte) 97;
      numArray3[44] = (byte) 137;
      numArray3[51] = (byte) 7;
      numArray3[2] = (byte) 120;
      numArray3[47] = (byte) 110;
      numArray3[30] = (byte) 109;
      numArray3[14] = (byte) 182;
      numArray3[50] = (byte) 48 /*0x30*/;
      numArray3[13] = (byte) 159;
      numArray3[52] = (byte) 98;
      numArray3[53] = (byte) 230;
      numArray3[54] = (byte) 3;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[1]{ (byte) 244 };
      byte[] numArray5 = new byte[1]{ (byte) 201 };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 1);
      for (int index = 0; index < 1; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[56];
    byte[] numArray7 = new byte[55]
    {
      (byte) 120,
      (byte) 32 /*0x20*/,
      (byte) 98,
      (byte) 67,
      (byte) 161,
      (byte) 86,
      (byte) 71,
      (byte) 130,
      (byte) 102,
      (byte) 100,
      (byte) 249,
      (byte) 165,
      (byte) 124,
      (byte) 51,
      (byte) 38,
      (byte) 134,
      (byte) 59,
      (byte) 103,
      (byte) 184,
      byte.MaxValue,
      (byte) 186,
      (byte) 198,
      (byte) 99,
      (byte) 132,
      (byte) 177,
      (byte) 11,
      (byte) 6,
      (byte) 23,
      (byte) 231,
      (byte) 63 /*0x3F*/,
      (byte) 33,
      (byte) 204,
      (byte) 91,
      (byte) 171,
      (byte) 232,
      (byte) 179,
      (byte) 180,
      (byte) 149,
      (byte) 208 /*0xD0*/,
      (byte) 2,
      (byte) 100,
      (byte) 135,
      (byte) 227,
      (byte) 132,
      (byte) 115,
      (byte) 138,
      (byte) 129,
      (byte) 68,
      (byte) 100,
      (byte) 110,
      (byte) 60,
      (byte) 179,
      (byte) 133,
      (byte) 165,
      (byte) 111
    };
    byte[] numArray8 = new byte[55];
    numArray8[24] = (byte) 15;
    numArray8[0] = (byte) 30;
    numArray8[11] = (byte) 16 /*0x10*/;
    numArray8[3] = (byte) 145;
    numArray8[4] = (byte) 75;
    numArray8[5] = (byte) 34;
    numArray8[20] = (byte) 113;
    numArray8[12] = (byte) 179;
    numArray8[7] = (byte) 246;
    numArray8[9] = (byte) 208 /*0xD0*/;
    numArray8[52] = (byte) 75;
    numArray8[40] = (byte) 19;
    numArray8[17] = (byte) 32 /*0x20*/;
    numArray8[42] = (byte) 133;
    numArray8[14] = (byte) 253;
    numArray8[15] = (byte) 51;
    numArray8[43] = (byte) 231;
    numArray8[25] = (byte) 233;
    numArray8[1] = (byte) 77;
    numArray8[19] = (byte) 64 /*0x40*/;
    numArray8[34] = (byte) 246;
    numArray8[33] = (byte) 19;
    numArray8[44] = (byte) 88;
    numArray8[41] = (byte) 41;
    numArray8[6] = (byte) 26;
    numArray8[16 /*0x10*/] = (byte) 125;
    numArray8[46] = (byte) 116;
    numArray8[27] = (byte) 42;
    numArray8[8] = (byte) 250;
    numArray8[23] = (byte) 16 /*0x10*/;
    numArray8[30] = (byte) 124;
    numArray8[31 /*0x1F*/] = (byte) 231;
    numArray8[32 /*0x20*/] = (byte) 21;
    numArray8[22] = (byte) 93;
    numArray8[13] = (byte) 208 /*0xD0*/;
    numArray8[29] = (byte) 219;
    numArray8[18] = (byte) 174;
    numArray8[37] = (byte) 157;
    numArray8[38] = (byte) 143;
    numArray8[39] = (byte) 24;
    numArray8[48 /*0x30*/] = (byte) 193;
    numArray8[35] = (byte) 138;
    numArray8[26] = (byte) 40;
    numArray8[21] = (byte) 170;
    numArray8[36] = (byte) 197;
    numArray8[2] = (byte) 121;
    numArray8[28] = (byte) 11;
    numArray8[47] = byte.MaxValue;
    numArray8[10] = (byte) 164;
    numArray8[49] = (byte) 202;
    numArray8[50] = (byte) 28;
    numArray8[51] = (byte) 127 /*0x7F*/;
    numArray8[45] = (byte) 191;
    numArray8[53] = (byte) 32 /*0x20*/;
    numArray8[54] = (byte) 228;
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[1]{ (byte) 204 };
    byte[] numArray10 = new byte[1]{ (byte) 194 };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 1);
    for (int index = 0; index < 1; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static int ssp_appserver_13521(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[28] = (byte) 10;
    sourceArray1[18] = (byte) 201;
    sourceArray1[2] = (byte) 229;
    sourceArray1[46] = (byte) 246;
    sourceArray1[4] = (byte) 149;
    sourceArray1[36] = (byte) 97;
    sourceArray1[23] = (byte) 120;
    sourceArray1[31 /*0x1F*/] = (byte) 37;
    sourceArray1[22] = (byte) 81;
    sourceArray1[9] = (byte) 92;
    sourceArray1[10] = (byte) 4;
    sourceArray1[11] = (byte) 102;
    sourceArray1[12] = (byte) 70;
    sourceArray1[37] = (byte) 172;
    sourceArray1[14] = (byte) 158;
    sourceArray1[15] = (byte) 202;
    sourceArray1[33] = (byte) 201;
    sourceArray1[17] = (byte) 217;
    sourceArray1[30] = (byte) 46;
    sourceArray1[45] = (byte) 165;
    sourceArray1[20] = (byte) 219;
    sourceArray1[21] = (byte) 108;
    sourceArray1[8] = (byte) 160 /*0xA0*/;
    sourceArray1[24] = (byte) 170;
    sourceArray1[0] = (byte) 36;
    sourceArray1[25] = byte.MaxValue;
    sourceArray1[19] = (byte) 193;
    sourceArray1[27] = (byte) 24;
    sourceArray1[6] = (byte) 124;
    sourceArray1[29] = (byte) 154;
    sourceArray1[3] = (byte) 148;
    sourceArray1[47] = (byte) 169;
    sourceArray1[7] = (byte) 250;
    sourceArray1[42] = (byte) 246;
    sourceArray1[34] = (byte) 152;
    sourceArray1[35] = (byte) 107;
    sourceArray1[39] = (byte) 226;
    sourceArray1[13] = (byte) 37;
    sourceArray1[38] = (byte) 26;
    sourceArray1[26] = (byte) 102;
    sourceArray1[43] = (byte) 192 /*0xC0*/;
    sourceArray1[41] = (byte) 162;
    sourceArray1[16 /*0x10*/] = (byte) 229;
    sourceArray1[5] = (byte) 22;
    sourceArray1[44] = (byte) 202;
    sourceArray1[32 /*0x20*/] = (byte) 182;
    sourceArray1[1] = (byte) 213;
    sourceArray1[40] = (byte) 116;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 201,
      (byte) 8,
      (byte) 187,
      (byte) 141,
      (byte) 179,
      (byte) 244,
      (byte) 8,
      (byte) 21,
      (byte) 36,
      (byte) 175,
      (byte) 22,
      (byte) 40,
      (byte) 51,
      (byte) 55,
      (byte) 206,
      byte.MaxValue,
      (byte) 135,
      (byte) 39,
      (byte) 110,
      (byte) 8,
      (byte) 132,
      (byte) 38,
      (byte) 23,
      (byte) 42,
      (byte) 235,
      (byte) 245,
      (byte) 172,
      (byte) 154,
      (byte) 59,
      (byte) 224 /*0xE0*/,
      (byte) 47,
      (byte) 132,
      (byte) 43,
      (byte) 215,
      (byte) 108,
      (byte) 34,
      (byte) 34,
      (byte) 77,
      (byte) 229,
      (byte) 214,
      (byte) 197,
      (byte) 90,
      (byte) 251,
      (byte) 10,
      (byte) 120,
      (byte) 4,
      (byte) 247,
      (byte) 131
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[34];
    byte[] response2 = new byte[34];
    Array.Copy((Array) sc_13518.sspq, 0, (Array) numArray2, 0, 34);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13518.sspr, 0, (Array) numArray2, 0, 34);
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

  internal static int ssp_appserver_13522(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[16 /*0x10*/] = (byte) 254;
    sourceArray1[1] = (byte) 206;
    sourceArray1[31 /*0x1F*/] = (byte) 251;
    sourceArray1[23] = (byte) 36;
    sourceArray1[9] = (byte) 247;
    sourceArray1[5] = (byte) 133;
    sourceArray1[6] = (byte) 138;
    sourceArray1[7] = (byte) 117;
    sourceArray1[29] = (byte) 187;
    sourceArray1[15] = (byte) 202;
    sourceArray1[39] = (byte) 247;
    sourceArray1[11] = (byte) 220;
    sourceArray1[12] = (byte) 247;
    sourceArray1[43] = (byte) 83;
    sourceArray1[14] = (byte) 134;
    sourceArray1[37] = (byte) 38;
    sourceArray1[13] = (byte) 62;
    sourceArray1[36] = (byte) 121;
    sourceArray1[0] = (byte) 159;
    sourceArray1[19] = (byte) 63 /*0x3F*/;
    sourceArray1[30] = (byte) 241;
    sourceArray1[21] = (byte) 111;
    sourceArray1[2] = (byte) 110;
    sourceArray1[35] = (byte) 187;
    sourceArray1[24] = (byte) 211;
    sourceArray1[8] = (byte) 157;
    sourceArray1[26] = (byte) 40;
    sourceArray1[27] = (byte) 51;
    sourceArray1[25] = (byte) 58;
    sourceArray1[4] = (byte) 135;
    sourceArray1[17] = (byte) 95;
    sourceArray1[18] = (byte) 110;
    sourceArray1[32 /*0x20*/] = (byte) 214;
    sourceArray1[33] = (byte) 253;
    sourceArray1[10] = (byte) 54;
    sourceArray1[3] = (byte) 202;
    sourceArray1[22] = (byte) 192 /*0xC0*/;
    sourceArray1[47] = (byte) 118;
    sourceArray1[38] = (byte) 171;
    sourceArray1[45] = (byte) 155;
    sourceArray1[40] = (byte) 43;
    sourceArray1[41] = (byte) 132;
    sourceArray1[42] = (byte) 15;
    sourceArray1[34] = (byte) 84;
    sourceArray1[44] = (byte) 62;
    sourceArray1[20] = (byte) 128 /*0x80*/;
    sourceArray1[46] = (byte) 65;
    sourceArray1[28] = (byte) 135;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 98,
      (byte) 194,
      (byte) 64 /*0x40*/,
      (byte) 83,
      (byte) 165,
      (byte) 105,
      (byte) 250,
      (byte) 176 /*0xB0*/,
      (byte) 64 /*0x40*/,
      (byte) 125,
      (byte) 204,
      (byte) 215,
      (byte) 212,
      (byte) 226,
      (byte) 18,
      (byte) 8,
      (byte) 22,
      (byte) 134,
      (byte) 152,
      (byte) 39,
      (byte) 150,
      (byte) 173,
      (byte) 174,
      (byte) 64 /*0x40*/,
      (byte) 219,
      (byte) 114,
      (byte) 11,
      (byte) 85,
      (byte) 138,
      (byte) 193,
      (byte) 174,
      (byte) 101,
      (byte) 110,
      (byte) 135,
      (byte) 152,
      (byte) 231,
      (byte) 95,
      (byte) 104,
      (byte) 184,
      (byte) 129,
      (byte) 69,
      (byte) 176 /*0xB0*/,
      (byte) 144 /*0x90*/,
      (byte) 203,
      (byte) 152,
      (byte) 133,
      (byte) 10,
      (byte) 54
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_13523()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[91];
      byte[] numArray2 = new byte[55]
      {
        (byte) 149,
        (byte) 71,
        (byte) 140,
        (byte) 234,
        (byte) 106,
        (byte) 175,
        (byte) 236,
        (byte) 221,
        (byte) 187,
        (byte) 19,
        (byte) 208 /*0xD0*/,
        (byte) 160 /*0xA0*/,
        (byte) 99,
        (byte) 192 /*0xC0*/,
        (byte) 85,
        (byte) 171,
        (byte) 218,
        (byte) 32 /*0x20*/,
        (byte) 174,
        (byte) 180,
        (byte) 148,
        byte.MaxValue,
        (byte) 11,
        (byte) 197,
        (byte) 28,
        (byte) 32 /*0x20*/,
        (byte) 169,
        (byte) 86,
        (byte) 220,
        (byte) 65,
        (byte) 100,
        (byte) 50,
        (byte) 31 /*0x1F*/,
        (byte) 226,
        (byte) 162,
        (byte) 176 /*0xB0*/,
        (byte) 37,
        (byte) 146,
        (byte) 97,
        (byte) 131,
        (byte) 100,
        (byte) 201,
        (byte) 74,
        (byte) 219,
        (byte) 83,
        (byte) 14,
        (byte) 204,
        (byte) 110,
        (byte) 8,
        (byte) 239,
        (byte) 248,
        (byte) 118,
        (byte) 126,
        (byte) 152,
        (byte) 111
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 115,
        (byte) 159,
        (byte) 101,
        (byte) 106,
        (byte) 20,
        (byte) 130,
        (byte) 222,
        (byte) 32 /*0x20*/,
        (byte) 140,
        byte.MaxValue,
        (byte) 80 /*0x50*/,
        (byte) 130,
        (byte) 130,
        (byte) 100,
        (byte) 166,
        (byte) 131,
        (byte) 119,
        (byte) 52,
        (byte) 204,
        (byte) 105,
        (byte) 199,
        (byte) 243,
        (byte) 150,
        (byte) 132,
        (byte) 100,
        (byte) 212,
        (byte) 251,
        (byte) 242,
        (byte) 89,
        (byte) 47,
        (byte) 48 /*0x30*/,
        (byte) 169,
        (byte) 89,
        (byte) 208 /*0xD0*/,
        (byte) 85,
        (byte) 11,
        (byte) 74,
        (byte) 128 /*0x80*/,
        (byte) 166,
        (byte) 165,
        (byte) 175,
        (byte) 209,
        (byte) 123,
        (byte) 223,
        (byte) 220,
        (byte) 40,
        (byte) 153,
        (byte) 102,
        (byte) 246,
        (byte) 134,
        (byte) 92,
        (byte) 32 /*0x20*/,
        (byte) 58,
        (byte) 170,
        (byte) 55
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[36];
      numArray4[15] = (byte) 190;
      numArray4[1] = (byte) 205;
      numArray4[0] = (byte) 198;
      numArray4[6] = (byte) 122;
      numArray4[4] = (byte) 150;
      numArray4[5] = (byte) 1;
      numArray4[23] = (byte) 83;
      numArray4[7] = (byte) 175;
      numArray4[2] = (byte) 74;
      numArray4[9] = (byte) 128 /*0x80*/;
      numArray4[10] = (byte) 129;
      numArray4[19] = (byte) 181;
      numArray4[16 /*0x10*/] = (byte) 43;
      numArray4[8] = (byte) 157;
      numArray4[14] = (byte) 72;
      numArray4[12] = (byte) 164;
      numArray4[13] = (byte) 133;
      numArray4[29] = (byte) 180;
      numArray4[31 /*0x1F*/] = (byte) 114;
      numArray4[3] = (byte) 5;
      numArray4[20] = (byte) 48 /*0x30*/;
      numArray4[21] = (byte) 114;
      numArray4[22] = (byte) 14;
      numArray4[35] = (byte) 196;
      numArray4[33] = (byte) 116;
      numArray4[18] = (byte) 203;
      numArray4[11] = (byte) 29;
      numArray4[27] = (byte) 72;
      numArray4[30] = (byte) 134;
      numArray4[26] = (byte) 236;
      numArray4[34] = (byte) 95;
      numArray4[25] = (byte) 78;
      numArray4[32 /*0x20*/] = (byte) 12;
      numArray4[28] = (byte) 237;
      numArray4[24] = (byte) 59;
      numArray4[17] = (byte) 80 /*0x50*/;
      byte[] numArray5 = new byte[36]
      {
        (byte) 181,
        (byte) 92,
        (byte) 85,
        (byte) 147,
        (byte) 238,
        (byte) 124,
        (byte) 77,
        (byte) 168,
        (byte) 182,
        (byte) 59,
        (byte) 205,
        (byte) 218,
        (byte) 115,
        (byte) 5,
        (byte) 119,
        (byte) 229,
        (byte) 232,
        (byte) 174,
        (byte) 113,
        (byte) 29,
        (byte) 21,
        (byte) 171,
        (byte) 130,
        (byte) 51,
        (byte) 108,
        (byte) 42,
        (byte) 187,
        (byte) 168,
        (byte) 201,
        (byte) 156,
        (byte) 160 /*0xA0*/,
        (byte) 15,
        (byte) 48 /*0x30*/,
        (byte) 249,
        (byte) 69,
        (byte) 104
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 36);
      for (int index = 0; index < 36; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[91];
    byte[] numArray7 = new byte[55]
    {
      (byte) 4,
      (byte) 187,
      (byte) 129,
      (byte) 8,
      (byte) 235,
      (byte) 234,
      (byte) 130,
      (byte) 151,
      (byte) 53,
      (byte) 97,
      (byte) 62,
      (byte) 206,
      (byte) 44,
      (byte) 104,
      (byte) 11,
      (byte) 5,
      (byte) 33,
      (byte) 179,
      (byte) 134,
      (byte) 178,
      (byte) 158,
      (byte) 106,
      (byte) 205,
      (byte) 148,
      (byte) 61,
      (byte) 63 /*0x3F*/,
      (byte) 50,
      (byte) 67,
      (byte) 45,
      (byte) 17,
      (byte) 165,
      (byte) 197,
      (byte) 161,
      (byte) 251,
      (byte) 7,
      (byte) 152,
      (byte) 61,
      (byte) 237,
      (byte) 222,
      (byte) 93,
      (byte) 146,
      (byte) 153,
      (byte) 140,
      (byte) 135,
      (byte) 186,
      (byte) 64 /*0x40*/,
      (byte) 228,
      (byte) 200,
      (byte) 26,
      (byte) 125,
      (byte) 132,
      (byte) 250,
      (byte) 252,
      (byte) 81,
      (byte) 69
    };
    byte[] numArray8 = new byte[55]
    {
      (byte) 9,
      (byte) 51,
      (byte) 143,
      (byte) 173,
      (byte) 212,
      (byte) 151,
      (byte) 123,
      (byte) 196,
      (byte) 240 /*0xF0*/,
      (byte) 87,
      (byte) 88,
      (byte) 21,
      (byte) 241,
      (byte) 123,
      (byte) 38,
      (byte) 0,
      (byte) 226,
      (byte) 140,
      (byte) 171,
      (byte) 122,
      (byte) 143,
      (byte) 5,
      (byte) 171,
      (byte) 42,
      (byte) 8,
      (byte) 31 /*0x1F*/,
      (byte) 221,
      (byte) 193,
      (byte) 65,
      (byte) 234,
      (byte) 42,
      (byte) 78,
      (byte) 25,
      (byte) 0,
      (byte) 42,
      (byte) 149,
      (byte) 218,
      (byte) 134,
      (byte) 244,
      (byte) 91,
      (byte) 112 /*0x70*/,
      (byte) 13,
      (byte) 159,
      (byte) 217,
      (byte) 200,
      (byte) 75,
      (byte) 86,
      (byte) 161,
      (byte) 189,
      (byte) 113,
      (byte) 238,
      (byte) 36,
      (byte) 190,
      (byte) 93,
      (byte) 57
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[36];
    numArray9[0] = (byte) 236;
    numArray9[32 /*0x20*/] = (byte) 108;
    numArray9[2] = (byte) 172;
    numArray9[3] = (byte) 183;
    numArray9[22] = (byte) 253;
    numArray9[18] = (byte) 5;
    numArray9[8] = (byte) 120;
    numArray9[7] = (byte) 187;
    numArray9[33] = (byte) 169;
    numArray9[23] = (byte) 90;
    numArray9[6] = (byte) 36;
    numArray9[5] = (byte) 106;
    numArray9[26] = (byte) 175;
    numArray9[13] = (byte) 199;
    numArray9[14] = (byte) 52;
    numArray9[10] = (byte) 48 /*0x30*/;
    numArray9[21] = (byte) 217;
    numArray9[12] = (byte) 115;
    numArray9[29] = (byte) 201;
    numArray9[19] = (byte) 222;
    numArray9[27] = (byte) 140;
    numArray9[31 /*0x1F*/] = (byte) 182;
    numArray9[9] = (byte) 45;
    numArray9[16 /*0x10*/] = (byte) 41;
    numArray9[15] = (byte) 212;
    numArray9[35] = (byte) 34;
    numArray9[25] = (byte) 41;
    numArray9[4] = (byte) 62;
    numArray9[1] = (byte) 203;
    numArray9[17] = (byte) 32 /*0x20*/;
    numArray9[30] = (byte) 80 /*0x50*/;
    numArray9[20] = (byte) 69;
    numArray9[34] = (byte) 81;
    numArray9[11] = (byte) 97;
    numArray9[24] = (byte) 106;
    numArray9[28] = (byte) 171;
    byte[] numArray10 = new byte[36];
    numArray10[24] = (byte) 120;
    numArray10[1] = (byte) 190;
    numArray10[15] = (byte) 132;
    numArray10[19] = (byte) 238;
    numArray10[30] = (byte) 55;
    numArray10[5] = (byte) 193;
    numArray10[6] = (byte) 202;
    numArray10[7] = (byte) 93;
    numArray10[8] = (byte) 21;
    numArray10[9] = (byte) 13;
    numArray10[14] = (byte) 127 /*0x7F*/;
    numArray10[11] = (byte) 110;
    numArray10[27] = (byte) 79;
    numArray10[13] = (byte) 19;
    numArray10[4] = (byte) 184;
    numArray10[23] = (byte) 128 /*0x80*/;
    numArray10[16 /*0x10*/] = (byte) 212;
    numArray10[29] = (byte) 17;
    numArray10[18] = (byte) 15;
    numArray10[3] = (byte) 64 /*0x40*/;
    numArray10[33] = (byte) 99;
    numArray10[21] = (byte) 115;
    numArray10[10] = (byte) 149;
    numArray10[22] = (byte) 234;
    numArray10[17] = (byte) 2;
    numArray10[2] = (byte) 1;
    numArray10[25] = (byte) 147;
    numArray10[26] = (byte) 184;
    numArray10[32 /*0x20*/] = (byte) 228;
    numArray10[0] = (byte) 252;
    numArray10[28] = (byte) 72;
    numArray10[31 /*0x1F*/] = (byte) 92;
    numArray10[20] = (byte) 231;
    numArray10[34] = (byte) 55;
    numArray10[12] = (byte) 193;
    numArray10[35] = (byte) 85;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 36);
    for (int index = 0; index < 36; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_13524()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[186];
      byte[] numArray2 = new byte[55]
      {
        (byte) 170,
        (byte) 72,
        (byte) 147,
        (byte) 193,
        (byte) 92,
        (byte) 137,
        (byte) 184,
        (byte) 190,
        (byte) 4,
        (byte) 67,
        (byte) 201,
        (byte) 57,
        (byte) 190,
        (byte) 192 /*0xC0*/,
        (byte) 43,
        (byte) 142,
        (byte) 97,
        (byte) 167,
        (byte) 189,
        (byte) 4,
        (byte) 180,
        (byte) 108,
        (byte) 145,
        (byte) 165,
        (byte) 185,
        (byte) 252,
        (byte) 2,
        (byte) 167,
        (byte) 186,
        (byte) 11,
        (byte) 106,
        (byte) 210,
        (byte) 193,
        (byte) 111,
        (byte) 54,
        (byte) 15,
        (byte) 170,
        (byte) 178,
        (byte) 114,
        (byte) 50,
        (byte) 218,
        (byte) 119,
        (byte) 53,
        (byte) 46,
        (byte) 19,
        (byte) 6,
        (byte) 128 /*0x80*/,
        (byte) 195,
        (byte) 36,
        (byte) 214,
        (byte) 8,
        (byte) 158,
        (byte) 112 /*0x70*/,
        (byte) 119,
        (byte) 47
      };
      byte[] numArray3 = new byte[55];
      numArray3[47] = (byte) 220;
      numArray3[7] = (byte) 21;
      numArray3[11] = (byte) 43;
      numArray3[3] = (byte) 76;
      numArray3[4] = (byte) 235;
      numArray3[27] = (byte) 112 /*0x70*/;
      numArray3[6] = (byte) 202;
      numArray3[0] = (byte) 193;
      numArray3[8] = (byte) 158;
      numArray3[9] = (byte) 110;
      numArray3[2] = (byte) 158;
      numArray3[48 /*0x30*/] = (byte) 121;
      numArray3[52] = (byte) 199;
      numArray3[13] = (byte) 141;
      numArray3[38] = (byte) 178;
      numArray3[15] = (byte) 164;
      numArray3[40] = (byte) 152;
      numArray3[43] = (byte) 27;
      numArray3[18] = (byte) 147;
      numArray3[19] = (byte) 141;
      numArray3[16 /*0x10*/] = (byte) 147;
      numArray3[42] = (byte) 136;
      numArray3[33] = (byte) 110;
      numArray3[23] = (byte) 161;
      numArray3[24] = (byte) 139;
      numArray3[20] = (byte) 115;
      numArray3[26] = (byte) 197;
      numArray3[25] = (byte) 131;
      numArray3[28] = (byte) 132;
      numArray3[10] = (byte) 197;
      numArray3[37] = (byte) 218;
      numArray3[31 /*0x1F*/] = (byte) 161;
      numArray3[1] = (byte) 8;
      numArray3[41] = (byte) 38;
      numArray3[34] = (byte) 85;
      numArray3[35] = (byte) 172;
      numArray3[45] = (byte) 6;
      numArray3[22] = (byte) 144 /*0x90*/;
      numArray3[44] = (byte) 115;
      numArray3[39] = (byte) 74;
      numArray3[30] = (byte) 216;
      numArray3[32 /*0x20*/] = (byte) 175;
      numArray3[14] = (byte) 28;
      numArray3[29] = (byte) 212;
      numArray3[17] = (byte) 24;
      numArray3[5] = (byte) 117;
      numArray3[46] = (byte) 5;
      numArray3[21] = (byte) 159;
      numArray3[12] = (byte) 72;
      numArray3[49] = (byte) 35;
      numArray3[36] = (byte) 192 /*0xC0*/;
      numArray3[51] = (byte) 212;
      numArray3[50] = (byte) 112 /*0x70*/;
      numArray3[53] = (byte) 202;
      numArray3[54] = (byte) 254;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55];
      numArray4[2] = (byte) 228;
      numArray4[1] = (byte) 84;
      numArray4[4] = (byte) 141;
      numArray4[3] = (byte) 72;
      numArray4[38] = (byte) 60;
      numArray4[36] = (byte) 45;
      numArray4[32 /*0x20*/] = (byte) 241;
      numArray4[7] = (byte) 13;
      numArray4[46] = (byte) 70;
      numArray4[10] = (byte) 14;
      numArray4[40] = (byte) 71;
      numArray4[11] = (byte) 145;
      numArray4[39] = (byte) 83;
      numArray4[45] = (byte) 167;
      numArray4[26] = (byte) 240 /*0xF0*/;
      numArray4[49] = (byte) 71;
      numArray4[15] = (byte) 34;
      numArray4[17] = (byte) 96 /*0x60*/;
      numArray4[18] = (byte) 201;
      numArray4[48 /*0x30*/] = (byte) 83;
      numArray4[20] = (byte) 14;
      numArray4[14] = (byte) 5;
      numArray4[22] = (byte) 36;
      numArray4[23] = (byte) 73;
      numArray4[53] = (byte) 12;
      numArray4[25] = (byte) 122;
      numArray4[9] = (byte) 45;
      numArray4[27] = (byte) 169;
      numArray4[28] = (byte) 97;
      numArray4[6] = (byte) 245;
      numArray4[30] = (byte) 253;
      numArray4[31 /*0x1F*/] = (byte) 76;
      numArray4[19] = (byte) 250;
      numArray4[33] = (byte) 5;
      numArray4[51] = (byte) 236;
      numArray4[21] = (byte) 116;
      numArray4[42] = (byte) 135;
      numArray4[37] = (byte) 97;
      numArray4[34] = (byte) 96 /*0x60*/;
      numArray4[24] = (byte) 5;
      numArray4[43] = (byte) 97;
      numArray4[41] = (byte) 71;
      numArray4[47] = (byte) 204;
      numArray4[12] = (byte) 194;
      numArray4[44] = (byte) 35;
      numArray4[0] = (byte) 92;
      numArray4[5] = (byte) 31 /*0x1F*/;
      numArray4[35] = (byte) 85;
      numArray4[54] = (byte) 69;
      numArray4[50] = (byte) 4;
      numArray4[8] = (byte) 149;
      numArray4[13] = (byte) 38;
      numArray4[52] = (byte) 42;
      numArray4[16 /*0x10*/] = (byte) 224 /*0xE0*/;
      numArray4[29] = (byte) 124;
      byte[] numArray5 = new byte[55];
      numArray5[42] = (byte) 131;
      numArray5[28] = (byte) 64 /*0x40*/;
      numArray5[2] = (byte) 165;
      numArray5[20] = (byte) 228;
      numArray5[47] = (byte) 94;
      numArray5[30] = (byte) 16 /*0x10*/;
      numArray5[6] = (byte) 243;
      numArray5[23] = (byte) 88;
      numArray5[34] = (byte) 238;
      numArray5[9] = (byte) 150;
      numArray5[10] = (byte) 249;
      numArray5[7] = (byte) 169;
      numArray5[12] = (byte) 194;
      numArray5[44] = (byte) 243;
      numArray5[14] = (byte) 195;
      numArray5[15] = (byte) 193;
      numArray5[1] = (byte) 246;
      numArray5[4] = (byte) 125;
      numArray5[3] = (byte) 37;
      numArray5[51] = (byte) 204;
      numArray5[33] = (byte) 213;
      numArray5[35] = (byte) 45;
      numArray5[18] = (byte) 126;
      numArray5[37] = (byte) 20;
      numArray5[24] = (byte) 53;
      numArray5[25] = (byte) 9;
      numArray5[5] = (byte) 85;
      numArray5[19] = (byte) 204;
      numArray5[41] = (byte) 3;
      numArray5[29] = (byte) 82;
      numArray5[22] = (byte) 221;
      numArray5[8] = (byte) 28;
      numArray5[27] = (byte) 122;
      numArray5[38] = (byte) 29;
      numArray5[45] = (byte) 214;
      numArray5[26] = (byte) 20;
      numArray5[36] = (byte) 113;
      numArray5[21] = (byte) 224 /*0xE0*/;
      numArray5[32 /*0x20*/] = (byte) 56;
      numArray5[39] = (byte) 177;
      numArray5[40] = (byte) 83;
      numArray5[17] = (byte) 80 /*0x50*/;
      numArray5[13] = (byte) 19;
      numArray5[43] = (byte) 146;
      numArray5[11] = (byte) 184;
      numArray5[31 /*0x1F*/] = (byte) 128 /*0x80*/;
      numArray5[46] = (byte) 69;
      numArray5[53] = (byte) 111;
      numArray5[48 /*0x30*/] = (byte) 210;
      numArray5[49] = (byte) 252;
      numArray5[50] = (byte) 129;
      numArray5[16 /*0x10*/] = (byte) 223;
      numArray5[52] = (byte) 79;
      numArray5[0] = (byte) 164;
      numArray5[54] = (byte) 220;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[55];
      numArray6[52] = (byte) 49;
      numArray6[11] = (byte) 207;
      numArray6[2] = (byte) 51;
      numArray6[3] = (byte) 238;
      numArray6[4] = (byte) 67;
      numArray6[44] = (byte) 211;
      numArray6[6] = (byte) 174;
      numArray6[43] = (byte) 28;
      numArray6[8] = (byte) 49;
      numArray6[7] = (byte) 212;
      numArray6[25] = (byte) 152;
      numArray6[38] = (byte) 194;
      numArray6[12] = (byte) 208 /*0xD0*/;
      numArray6[26] = (byte) 216;
      numArray6[30] = (byte) 235;
      numArray6[15] = (byte) 173;
      numArray6[20] = (byte) 126;
      numArray6[17] = (byte) 178;
      numArray6[34] = (byte) 109;
      numArray6[1] = (byte) 142;
      numArray6[28] = (byte) 124;
      numArray6[21] = (byte) 1;
      numArray6[22] = (byte) 11;
      numArray6[10] = (byte) 186;
      numArray6[19] = (byte) 177;
      numArray6[45] = (byte) 234;
      numArray6[35] = (byte) 170;
      numArray6[18] = (byte) 33;
      numArray6[48 /*0x30*/] = (byte) 253;
      numArray6[29] = (byte) 8;
      numArray6[13] = (byte) 68;
      numArray6[31 /*0x1F*/] = (byte) 239;
      numArray6[32 /*0x20*/] = (byte) 1;
      numArray6[46] = (byte) 73;
      numArray6[53] = (byte) 202;
      numArray6[27] = (byte) 205;
      numArray6[40] = (byte) 30;
      numArray6[37] = (byte) 43;
      numArray6[50] = (byte) 45;
      numArray6[39] = (byte) 64 /*0x40*/;
      numArray6[33] = (byte) 222;
      numArray6[41] = (byte) 62;
      numArray6[42] = (byte) 144 /*0x90*/;
      numArray6[5] = (byte) 201;
      numArray6[36] = (byte) 24;
      numArray6[16 /*0x10*/] = (byte) 54;
      numArray6[23] = (byte) 28;
      numArray6[47] = (byte) 171;
      numArray6[0] = (byte) 176 /*0xB0*/;
      numArray6[49] = (byte) 31 /*0x1F*/;
      numArray6[24] = (byte) 98;
      numArray6[51] = (byte) 237;
      numArray6[9] = (byte) 46;
      numArray6[14] = (byte) 39;
      numArray6[54] = (byte) 92;
      byte[] numArray7 = new byte[55]
      {
        (byte) 148,
        (byte) 236,
        (byte) 49,
        (byte) 184,
        (byte) 106,
        (byte) 99,
        (byte) 148,
        (byte) 222,
        (byte) 169,
        (byte) 78,
        (byte) 10,
        (byte) 158,
        (byte) 173,
        (byte) 50,
        (byte) 20,
        (byte) 241,
        (byte) 152,
        (byte) 147,
        (byte) 82,
        (byte) 26,
        (byte) 46,
        (byte) 141,
        (byte) 9,
        (byte) 160 /*0xA0*/,
        (byte) 86,
        (byte) 103,
        (byte) 74,
        (byte) 11,
        (byte) 196,
        (byte) 245,
        (byte) 180,
        (byte) 164,
        (byte) 81,
        (byte) 244,
        (byte) 66,
        (byte) 20,
        (byte) 165,
        (byte) 191,
        (byte) 131,
        (byte) 51,
        (byte) 186,
        (byte) 74,
        (byte) 150,
        (byte) 110,
        (byte) 97,
        (byte) 33,
        (byte) 168,
        (byte) 202,
        (byte) 122,
        (byte) 75,
        (byte) 132,
        (byte) 20,
        (byte) 205,
        (byte) 88,
        (byte) 13
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[21]
      {
        (byte) 205,
        (byte) 19,
        (byte) 92,
        (byte) 196,
        (byte) 17,
        (byte) 251,
        (byte) 77,
        (byte) 68,
        (byte) 12,
        (byte) 151,
        (byte) 113,
        (byte) 25,
        (byte) 85,
        (byte) 148,
        (byte) 108,
        (byte) 217,
        (byte) 145,
        (byte) 86,
        (byte) 90,
        (byte) 9,
        (byte) 20
      };
      byte[] numArray9 = new byte[21]
      {
        (byte) 218,
        (byte) 78,
        (byte) 15,
        (byte) 155,
        (byte) 186,
        (byte) 33,
        (byte) 137,
        (byte) 85,
        (byte) 10,
        (byte) 26,
        (byte) 176 /*0xB0*/,
        (byte) 36,
        (byte) 226,
        (byte) 16 /*0x10*/,
        (byte) 190,
        (byte) 132,
        (byte) 84,
        (byte) 125,
        (byte) 56,
        (byte) 157,
        (byte) 163
      };
      key.Query(true, 335, numArray8, numArray8);
      Array.Copy((Array) numArray8, 0, (Array) numArray1, 165, 21);
      for (int index = 0; index < 21; ++index)
        numArray1[index + 165] ^= numArray9[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray10 = new byte[186];
    byte[] numArray11 = new byte[55];
    numArray11[45] = (byte) 224 /*0xE0*/;
    numArray11[23] = (byte) 245;
    numArray11[2] = (byte) 210;
    numArray11[41] = (byte) 12;
    numArray11[11] = (byte) 139;
    numArray11[19] = (byte) 132;
    numArray11[44] = (byte) 96 /*0x60*/;
    numArray11[7] = (byte) 100;
    numArray11[8] = (byte) 128 /*0x80*/;
    numArray11[46] = (byte) 30;
    numArray11[10] = (byte) 51;
    numArray11[5] = (byte) 110;
    numArray11[37] = (byte) 149;
    numArray11[52] = (byte) 69;
    numArray11[13] = (byte) 138;
    numArray11[15] = (byte) 233;
    numArray11[22] = (byte) 44;
    numArray11[17] = (byte) 149;
    numArray11[39] = (byte) 158;
    numArray11[50] = (byte) 239;
    numArray11[12] = (byte) 186;
    numArray11[21] = (byte) 187;
    numArray11[43] = (byte) 195;
    numArray11[0] = (byte) 88;
    numArray11[24] = (byte) 182;
    numArray11[25] = (byte) 35;
    numArray11[14] = (byte) 43;
    numArray11[4] = (byte) 1;
    numArray11[6] = byte.MaxValue;
    numArray11[49] = (byte) 112 /*0x70*/;
    numArray11[42] = (byte) 152;
    numArray11[31 /*0x1F*/] = (byte) 140;
    numArray11[32 /*0x20*/] = (byte) 184;
    numArray11[3] = (byte) 184;
    numArray11[34] = (byte) 107;
    numArray11[35] = (byte) 195;
    numArray11[33] = (byte) 100;
    numArray11[20] = (byte) 229;
    numArray11[38] = (byte) 67;
    numArray11[28] = (byte) 193;
    numArray11[40] = (byte) 152;
    numArray11[18] = (byte) 251;
    numArray11[30] = (byte) 128 /*0x80*/;
    numArray11[16 /*0x10*/] = (byte) 25;
    numArray11[9] = (byte) 146;
    numArray11[29] = (byte) 51;
    numArray11[54] = (byte) 17;
    numArray11[47] = (byte) 6;
    numArray11[48 /*0x30*/] = (byte) 43;
    numArray11[26] = (byte) 190;
    numArray11[1] = (byte) 78;
    numArray11[51] = (byte) 127 /*0x7F*/;
    numArray11[27] = (byte) 224 /*0xE0*/;
    numArray11[53] = (byte) 214;
    numArray11[36] = (byte) 40;
    byte[] numArray12 = new byte[55];
    numArray12[5] = (byte) 72;
    numArray12[38] = (byte) 194;
    numArray12[32 /*0x20*/] = (byte) 174;
    numArray12[15] = (byte) 23;
    numArray12[53] = (byte) 176 /*0xB0*/;
    numArray12[16 /*0x10*/] = (byte) 127 /*0x7F*/;
    numArray12[6] = (byte) 198;
    numArray12[42] = (byte) 113;
    numArray12[8] = (byte) 31 /*0x1F*/;
    numArray12[9] = (byte) 183;
    numArray12[10] = (byte) 227;
    numArray12[20] = (byte) 74;
    numArray12[39] = (byte) 180;
    numArray12[2] = (byte) 13;
    numArray12[24] = (byte) 241;
    numArray12[40] = (byte) 92;
    numArray12[43] = (byte) 212;
    numArray12[34] = (byte) 57;
    numArray12[18] = (byte) 50;
    numArray12[54] = (byte) 83;
    numArray12[3] = (byte) 163;
    numArray12[21] = (byte) 55;
    numArray12[7] = (byte) 8;
    numArray12[23] = (byte) 248;
    numArray12[51] = (byte) 17;
    numArray12[17] = (byte) 68;
    numArray12[26] = (byte) 99;
    numArray12[27] = (byte) 167;
    numArray12[28] = (byte) 78;
    numArray12[52] = (byte) 35;
    numArray12[30] = (byte) 15;
    numArray12[31 /*0x1F*/] = (byte) 223;
    numArray12[12] = (byte) 129;
    numArray12[33] = (byte) 226;
    numArray12[22] = (byte) 109;
    numArray12[13] = (byte) 200;
    numArray12[36] = (byte) 227;
    numArray12[29] = (byte) 123;
    numArray12[44] = (byte) 187;
    numArray12[14] = (byte) 95;
    numArray12[19] = (byte) 118;
    numArray12[41] = (byte) 42;
    numArray12[25] = (byte) 54;
    numArray12[11] = (byte) 153;
    numArray12[0] = (byte) 136;
    numArray12[45] = (byte) 193;
    numArray12[46] = (byte) 201;
    numArray12[49] = (byte) 120;
    numArray12[48 /*0x30*/] = (byte) 222;
    numArray12[50] = (byte) 94;
    numArray12[35] = (byte) 244;
    numArray12[37] = (byte) 79;
    numArray12[1] = (byte) 151;
    numArray12[47] = (byte) 96 /*0x60*/;
    numArray12[4] = (byte) 174;
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray10, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index] ^= numArray12[index];
    byte[] numArray13 = new byte[55];
    numArray13[24] = (byte) 58;
    numArray13[1] = (byte) 99;
    numArray13[2] = (byte) 155;
    numArray13[12] = (byte) 219;
    numArray13[4] = (byte) 122;
    numArray13[30] = (byte) 129;
    numArray13[6] = (byte) 47;
    numArray13[7] = (byte) 59;
    numArray13[8] = (byte) 1;
    numArray13[13] = (byte) 114;
    numArray13[35] = (byte) 3;
    numArray13[11] = (byte) 253;
    numArray13[28] = (byte) 145;
    numArray13[54] = (byte) 234;
    numArray13[42] = (byte) 87;
    numArray13[31 /*0x1F*/] = (byte) 82;
    numArray13[15] = (byte) 93;
    numArray13[51] = (byte) 15;
    numArray13[18] = (byte) 89;
    numArray13[19] = (byte) 148;
    numArray13[32 /*0x20*/] = (byte) 176 /*0xB0*/;
    numArray13[23] = (byte) 118;
    numArray13[27] = (byte) 96 /*0x60*/;
    numArray13[9] = (byte) 249;
    numArray13[47] = (byte) 200;
    numArray13[3] = (byte) 38;
    numArray13[26] = (byte) 102;
    numArray13[46] = (byte) 34;
    numArray13[14] = (byte) 138;
    numArray13[44] = (byte) 221;
    numArray13[37] = (byte) 210;
    numArray13[36] = (byte) 243;
    numArray13[5] = (byte) 177;
    numArray13[33] = (byte) 234;
    numArray13[34] = (byte) 149;
    numArray13[22] = (byte) 171;
    numArray13[21] = (byte) 129;
    numArray13[52] = (byte) 110;
    numArray13[38] = (byte) 147;
    numArray13[29] = (byte) 227;
    numArray13[40] = (byte) 19;
    numArray13[41] = (byte) 102;
    numArray13[20] = (byte) 21;
    numArray13[45] = (byte) 152;
    numArray13[25] = (byte) 242;
    numArray13[17] = (byte) 102;
    numArray13[0] = (byte) 109;
    numArray13[43] = (byte) 35;
    numArray13[48 /*0x30*/] = (byte) 238;
    numArray13[49] = (byte) 228;
    numArray13[50] = (byte) 5;
    numArray13[16 /*0x10*/] = (byte) 117;
    numArray13[10] = (byte) 232;
    numArray13[53] = (byte) 14;
    numArray13[39] = (byte) 102;
    byte[] numArray14 = new byte[55]
    {
      (byte) 136,
      (byte) 209,
      (byte) 9,
      (byte) 67,
      (byte) 111,
      (byte) 216,
      (byte) 119,
      (byte) 222,
      (byte) 63 /*0x3F*/,
      (byte) 0,
      (byte) 19,
      (byte) 132,
      (byte) 77,
      (byte) 239,
      (byte) 89,
      (byte) 231,
      (byte) 224 /*0xE0*/,
      (byte) 254,
      (byte) 151,
      (byte) 82,
      (byte) 113,
      (byte) 245,
      (byte) 214,
      (byte) 102,
      (byte) 75,
      (byte) 82,
      (byte) 206,
      (byte) 43,
      (byte) 64 /*0x40*/,
      (byte) 63 /*0x3F*/,
      (byte) 213,
      (byte) 1,
      (byte) 109,
      (byte) 201,
      (byte) 5,
      (byte) 57,
      (byte) 166,
      (byte) 181,
      (byte) 252,
      (byte) 142,
      (byte) 131,
      (byte) 217,
      (byte) 139,
      (byte) 92,
      (byte) 40,
      (byte) 104,
      (byte) 154,
      (byte) 31 /*0x1F*/,
      (byte) 24,
      (byte) 0,
      (byte) 3,
      (byte) 7,
      (byte) 94,
      (byte) 85,
      (byte) 21
    };
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray10, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index + 55] ^= numArray14[index];
    byte[] numArray15 = new byte[55];
    numArray15[24] = (byte) 178;
    numArray15[1] = (byte) 73;
    numArray15[42] = (byte) 150;
    numArray15[3] = (byte) 173;
    numArray15[4] = (byte) 93;
    numArray15[26] = (byte) 50;
    numArray15[6] = (byte) 63 /*0x3F*/;
    numArray15[22] = (byte) 86;
    numArray15[8] = (byte) 177;
    numArray15[14] = (byte) 188;
    numArray15[10] = (byte) 241;
    numArray15[7] = (byte) 5;
    numArray15[12] = (byte) 60;
    numArray15[13] = (byte) 155;
    numArray15[47] = (byte) 222;
    numArray15[15] = (byte) 198;
    numArray15[32 /*0x20*/] = (byte) 231;
    numArray15[43] = (byte) 159;
    numArray15[23] = (byte) 159;
    numArray15[19] = (byte) 240 /*0xF0*/;
    numArray15[2] = (byte) 183;
    numArray15[21] = (byte) 80 /*0x50*/;
    numArray15[41] = (byte) 166;
    numArray15[30] = (byte) 20;
    numArray15[25] = (byte) 10;
    numArray15[9] = (byte) 172;
    numArray15[38] = (byte) 230;
    numArray15[39] = (byte) 191;
    numArray15[28] = (byte) 52;
    numArray15[29] = (byte) 103;
    numArray15[20] = (byte) 220;
    numArray15[31 /*0x1F*/] = (byte) 196;
    numArray15[18] = (byte) 242;
    numArray15[33] = (byte) 28;
    numArray15[51] = (byte) 78;
    numArray15[35] = (byte) 156;
    numArray15[53] = (byte) 208 /*0xD0*/;
    numArray15[37] = (byte) 109;
    numArray15[5] = (byte) 252;
    numArray15[16 /*0x10*/] = (byte) 215;
    numArray15[40] = (byte) 250;
    numArray15[34] = (byte) 67;
    numArray15[0] = (byte) 139;
    numArray15[54] = (byte) 82;
    numArray15[50] = (byte) 164;
    numArray15[27] = (byte) 124;
    numArray15[46] = (byte) 42;
    numArray15[44] = (byte) 63 /*0x3F*/;
    numArray15[48 /*0x30*/] = (byte) 165;
    numArray15[49] = (byte) 118;
    numArray15[36] = (byte) 85;
    numArray15[17] = (byte) 61;
    numArray15[52] = (byte) 71;
    numArray15[11] = (byte) 52;
    numArray15[45] = (byte) 182;
    byte[] numArray16 = new byte[55];
    numArray16[27] = (byte) 145;
    numArray16[18] = byte.MaxValue;
    numArray16[2] = (byte) 47;
    numArray16[5] = (byte) 15;
    numArray16[51] = (byte) 79;
    numArray16[15] = (byte) 165;
    numArray16[31 /*0x1F*/] = (byte) 104;
    numArray16[48 /*0x30*/] = (byte) 85;
    numArray16[8] = (byte) 154;
    numArray16[42] = (byte) 153;
    numArray16[9] = (byte) 142;
    numArray16[44] = (byte) 195;
    numArray16[12] = (byte) 160 /*0xA0*/;
    numArray16[13] = (byte) 243;
    numArray16[14] = (byte) 148;
    numArray16[39] = (byte) 62;
    numArray16[10] = (byte) 28;
    numArray16[50] = (byte) 185;
    numArray16[37] = (byte) 157;
    numArray16[53] = (byte) 254;
    numArray16[0] = (byte) 66;
    numArray16[19] = (byte) 191;
    numArray16[21] = (byte) 199;
    numArray16[23] = (byte) 159;
    numArray16[40] = (byte) 137;
    numArray16[25] = (byte) 147;
    numArray16[26] = (byte) 182;
    numArray16[17] = (byte) 157;
    numArray16[28] = (byte) 56;
    numArray16[29] = (byte) 215;
    numArray16[30] = (byte) 213;
    numArray16[4] = (byte) 188;
    numArray16[24] = (byte) 183;
    numArray16[33] = (byte) 13;
    numArray16[34] = (byte) 188;
    numArray16[35] = (byte) 54;
    numArray16[36] = (byte) 161;
    numArray16[16 /*0x10*/] = (byte) 218;
    numArray16[38] = (byte) 14;
    numArray16[22] = (byte) 185;
    numArray16[47] = (byte) 44;
    numArray16[41] = (byte) 176 /*0xB0*/;
    numArray16[7] = (byte) 214;
    numArray16[43] = (byte) 76;
    numArray16[3] = (byte) 50;
    numArray16[45] = (byte) 213;
    numArray16[46] = (byte) 89;
    numArray16[6] = (byte) 203;
    numArray16[54] = (byte) 232;
    numArray16[11] = (byte) 220;
    numArray16[49] = (byte) 83;
    numArray16[32 /*0x20*/] = (byte) 138;
    numArray16[52] = (byte) 19;
    numArray16[20] = (byte) 192 /*0xC0*/;
    numArray16[1] = (byte) 163;
    key.Query(true, 335, numArray15, numArray15);
    Array.Copy((Array) numArray15, 0, (Array) numArray10, 110, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index + 110] ^= numArray16[index];
    byte[] numArray17 = new byte[21];
    numArray17[13] = (byte) 175;
    numArray17[1] = (byte) 107;
    numArray17[2] = (byte) 156;
    numArray17[20] = (byte) 121;
    numArray17[4] = (byte) 156;
    numArray17[15] = (byte) 164;
    numArray17[3] = (byte) 168;
    numArray17[0] = (byte) 106;
    numArray17[8] = (byte) 72;
    numArray17[6] = (byte) 252;
    numArray17[10] = (byte) 169;
    numArray17[19] = (byte) 166;
    numArray17[9] = (byte) 247;
    numArray17[7] = (byte) 3;
    numArray17[14] = (byte) 129;
    numArray17[17] = (byte) 118;
    numArray17[12] = (byte) 8;
    numArray17[16 /*0x10*/] = (byte) 190;
    numArray17[18] = (byte) 157;
    numArray17[5] = (byte) 90;
    numArray17[11] = (byte) 200;
    byte[] numArray18 = new byte[21]
    {
      (byte) 7,
      (byte) 204,
      (byte) 235,
      (byte) 132,
      (byte) 80 /*0x50*/,
      (byte) 172,
      (byte) 202,
      (byte) 82,
      (byte) 224 /*0xE0*/,
      (byte) 246,
      (byte) 151,
      (byte) 139,
      (byte) 42,
      (byte) 160 /*0xA0*/,
      (byte) 252,
      (byte) 110,
      (byte) 210,
      (byte) 52,
      (byte) 105,
      (byte) 243,
      (byte) 98
    };
    key.Query(true, 335, numArray17, numArray17);
    Array.Copy((Array) numArray17, 0, (Array) numArray10, 165, 21);
    for (int index = 0; index < 21; ++index)
      numArray10[index + 165] ^= numArray18[index];
    return Encoding.UTF8.GetString(numArray10);
  }

  internal static int ssp_appserver_13525(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[19] = (byte) 83;
    sourceArray1[15] = (byte) 103;
    sourceArray1[0] = (byte) 34;
    sourceArray1[42] = (byte) 153;
    sourceArray1[6] = (byte) 108;
    sourceArray1[5] = (byte) 31 /*0x1F*/;
    sourceArray1[31 /*0x1F*/] = (byte) 25;
    sourceArray1[44] = (byte) 9;
    sourceArray1[25] = (byte) 201;
    sourceArray1[9] = (byte) 14;
    sourceArray1[10] = (byte) 122;
    sourceArray1[43] = (byte) 251;
    sourceArray1[12] = (byte) 93;
    sourceArray1[8] = (byte) 128 /*0x80*/;
    sourceArray1[14] = (byte) 181;
    sourceArray1[7] = (byte) 18;
    sourceArray1[4] = (byte) 163;
    sourceArray1[41] = (byte) 207;
    sourceArray1[18] = (byte) 184;
    sourceArray1[1] = (byte) 193;
    sourceArray1[20] = (byte) 38;
    sourceArray1[35] = (byte) 133;
    sourceArray1[22] = (byte) 119;
    sourceArray1[11] = (byte) 17;
    sourceArray1[38] = (byte) 128 /*0x80*/;
    sourceArray1[17] = (byte) 211;
    sourceArray1[3] = (byte) 25;
    sourceArray1[24] = (byte) 231;
    sourceArray1[21] = (byte) 149;
    sourceArray1[2] = (byte) 141;
    sourceArray1[30] = (byte) 32 /*0x20*/;
    sourceArray1[27] = (byte) 219;
    sourceArray1[32 /*0x20*/] = (byte) 41;
    sourceArray1[33] = (byte) 55;
    sourceArray1[16 /*0x10*/] = (byte) 183;
    sourceArray1[46] = (byte) 79;
    sourceArray1[36] = (byte) 14;
    sourceArray1[37] = (byte) 119;
    sourceArray1[34] = (byte) 196;
    sourceArray1[39] = (byte) 209;
    sourceArray1[40] = (byte) 8;
    sourceArray1[23] = (byte) 33;
    sourceArray1[26] = (byte) 53;
    sourceArray1[13] = (byte) 116;
    sourceArray1[28] = (byte) 70;
    sourceArray1[45] = (byte) 196;
    sourceArray1[29] = (byte) 208 /*0xD0*/;
    sourceArray1[47] = (byte) 243;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 189,
      (byte) 36,
      (byte) 87,
      (byte) 118,
      (byte) 119,
      (byte) 147,
      (byte) 91,
      (byte) 115,
      (byte) 212,
      (byte) 159,
      (byte) 246,
      (byte) 213,
      (byte) 94,
      (byte) 123,
      (byte) 13,
      (byte) 207,
      (byte) 66,
      (byte) 206,
      (byte) 165,
      (byte) 123,
      (byte) 196,
      (byte) 236,
      (byte) 41,
      (byte) 242,
      (byte) 108,
      (byte) 71,
      (byte) 66,
      (byte) 94,
      (byte) 187,
      (byte) 142,
      (byte) 194,
      (byte) 235,
      (byte) 33,
      (byte) 171,
      (byte) 172,
      (byte) 167,
      (byte) 252,
      (byte) 168,
      (byte) 19,
      (byte) 227,
      (byte) 212,
      (byte) 97,
      (byte) 48 /*0x30*/,
      (byte) 222,
      (byte) 240 /*0xF0*/,
      (byte) 118,
      (byte) 253,
      (byte) 84
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[18];
    byte[] response2 = new byte[18];
    Array.Copy((Array) sc_13518.sspq, 34, (Array) numArray2, 0, 18);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13518.sspr, 34, (Array) numArray2, 0, 18);
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

  internal static int ssp_appserver_13527(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 152,
      (byte) 217,
      (byte) 207,
      (byte) 120,
      (byte) 243,
      (byte) 183,
      (byte) 249,
      (byte) 171,
      byte.MaxValue,
      (byte) 210,
      (byte) 83,
      (byte) 16 /*0x10*/,
      (byte) 111,
      (byte) 111,
      (byte) 70,
      (byte) 80 /*0x50*/,
      (byte) 254,
      (byte) 224 /*0xE0*/,
      (byte) 7,
      (byte) 74,
      (byte) 159,
      (byte) 94,
      (byte) 62,
      (byte) 202,
      (byte) 165,
      (byte) 111,
      (byte) 77,
      (byte) 55,
      (byte) 72,
      (byte) 63 /*0x3F*/,
      (byte) 173,
      (byte) 177,
      (byte) 168,
      (byte) 213,
      (byte) 15,
      (byte) 2,
      (byte) 90,
      (byte) 202,
      (byte) 242,
      (byte) 187,
      (byte) 41,
      (byte) 106,
      (byte) 105,
      (byte) 253,
      (byte) 55,
      (byte) 129,
      (byte) 93,
      (byte) 228
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[5] = (byte) 67;
    sourceArray2[0] = (byte) 232;
    sourceArray2[2] = (byte) 31 /*0x1F*/;
    sourceArray2[37] = (byte) 52;
    sourceArray2[1] = (byte) 22;
    sourceArray2[24] = (byte) 8;
    sourceArray2[6] = (byte) 57;
    sourceArray2[7] = (byte) 84;
    sourceArray2[41] = (byte) 124;
    sourceArray2[36] = (byte) 188;
    sourceArray2[30] = (byte) 183;
    sourceArray2[11] = (byte) 223;
    sourceArray2[20] = (byte) 228;
    sourceArray2[8] = (byte) 187;
    sourceArray2[9] = (byte) 178;
    sourceArray2[26] = (byte) 53;
    sourceArray2[16 /*0x10*/] = (byte) 208 /*0xD0*/;
    sourceArray2[17] = (byte) 126;
    sourceArray2[14] = (byte) 169;
    sourceArray2[19] = (byte) 214;
    sourceArray2[23] = (byte) 114;
    sourceArray2[39] = (byte) 196;
    sourceArray2[10] = (byte) 99;
    sourceArray2[15] = (byte) 42;
    sourceArray2[18] = (byte) 182;
    sourceArray2[4] = (byte) 245;
    sourceArray2[3] = (byte) 85;
    sourceArray2[31 /*0x1F*/] = (byte) 36;
    sourceArray2[28] = (byte) 132;
    sourceArray2[29] = (byte) 161;
    sourceArray2[13] = (byte) 89;
    sourceArray2[22] = (byte) 43;
    sourceArray2[38] = (byte) 137;
    sourceArray2[33] = (byte) 252;
    sourceArray2[32 /*0x20*/] = (byte) 4;
    sourceArray2[12] = (byte) 34;
    sourceArray2[25] = (byte) 47;
    sourceArray2[35] = (byte) 196;
    sourceArray2[40] = (byte) 174;
    sourceArray2[34] = (byte) 24;
    sourceArray2[27] = (byte) 87;
    sourceArray2[21] = (byte) 27;
    sourceArray2[42] = (byte) 15;
    sourceArray2[43] = (byte) 236;
    sourceArray2[44] = (byte) 207;
    sourceArray2[45] = (byte) 190;
    sourceArray2[46] = (byte) 63 /*0x3F*/;
    sourceArray2[47] = (byte) 129;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[26];
    byte[] response2 = new byte[26];
    Array.Copy((Array) sc_13518.sspq, 52, (Array) numArray2, 0, 26);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13518.sspr, 52, (Array) numArray2, 0, 26);
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

  internal static string ssp_appserver_13528()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[63 /*0x3F*/];
      byte[] numArray2 = new byte[55];
      numArray2[21] = (byte) 251;
      numArray2[42] = (byte) 35;
      numArray2[48 /*0x30*/] = (byte) 211;
      numArray2[3] = (byte) 45;
      numArray2[46] = (byte) 13;
      numArray2[49] = (byte) 221;
      numArray2[6] = (byte) 253;
      numArray2[7] = (byte) 11;
      numArray2[28] = (byte) 97;
      numArray2[11] = (byte) 32 /*0x20*/;
      numArray2[33] = (byte) 58;
      numArray2[29] = (byte) 101;
      numArray2[19] = (byte) 229;
      numArray2[4] = (byte) 76;
      numArray2[14] = (byte) 103;
      numArray2[17] = (byte) 145;
      numArray2[15] = (byte) 217;
      numArray2[35] = (byte) 4;
      numArray2[30] = (byte) 249;
      numArray2[41] = (byte) 163;
      numArray2[20] = (byte) 71;
      numArray2[10] = (byte) 234;
      numArray2[9] = (byte) 228;
      numArray2[23] = (byte) 18;
      numArray2[2] = (byte) 186;
      numArray2[25] = (byte) 125;
      numArray2[12] = (byte) 29;
      numArray2[27] = (byte) 89;
      numArray2[50] = (byte) 130;
      numArray2[22] = (byte) 78;
      numArray2[5] = (byte) 105;
      numArray2[31 /*0x1F*/] = (byte) 167;
      numArray2[32 /*0x20*/] = (byte) 123;
      numArray2[24] = (byte) 222;
      numArray2[18] = (byte) 13;
      numArray2[51] = (byte) 249;
      numArray2[36] = (byte) 28;
      numArray2[37] = (byte) 53;
      numArray2[38] = (byte) 248;
      numArray2[39] = (byte) 240 /*0xF0*/;
      numArray2[40] = (byte) 115;
      numArray2[0] = (byte) 127 /*0x7F*/;
      numArray2[47] = (byte) 155;
      numArray2[43] = (byte) 29;
      numArray2[44] = (byte) 105;
      numArray2[45] = (byte) 9;
      numArray2[54] = (byte) 245;
      numArray2[26] = (byte) 117;
      numArray2[13] = (byte) 198;
      numArray2[34] = (byte) 124;
      numArray2[1] = (byte) 250;
      numArray2[8] = (byte) 79;
      numArray2[52] = (byte) 207;
      numArray2[53] = (byte) 185;
      numArray2[16 /*0x10*/] = (byte) 218;
      byte[] numArray3 = new byte[55]
      {
        (byte) 178,
        (byte) 171,
        (byte) 152,
        (byte) 132,
        (byte) 218,
        (byte) 12,
        (byte) 150,
        (byte) 128 /*0x80*/,
        (byte) 164,
        (byte) 165,
        (byte) 28,
        (byte) 82,
        (byte) 116,
        (byte) 12,
        (byte) 71,
        (byte) 149,
        (byte) 111,
        (byte) 151,
        (byte) 154,
        (byte) 158,
        (byte) 147,
        (byte) 189,
        (byte) 166,
        (byte) 180,
        (byte) 108,
        (byte) 206,
        (byte) 4,
        (byte) 191,
        (byte) 138,
        (byte) 238,
        (byte) 48 /*0x30*/,
        (byte) 90,
        (byte) 154,
        (byte) 25,
        (byte) 17,
        (byte) 205,
        (byte) 57,
        (byte) 57,
        (byte) 243,
        (byte) 132,
        (byte) 117,
        (byte) 154,
        (byte) 31 /*0x1F*/,
        (byte) 124,
        (byte) 213,
        (byte) 145,
        (byte) 128 /*0x80*/,
        (byte) 47,
        (byte) 32 /*0x20*/,
        (byte) 113,
        (byte) 166,
        (byte) 3,
        (byte) 53,
        (byte) 99,
        (byte) 192 /*0xC0*/
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[8];
      numArray4[4] = (byte) 29;
      numArray4[2] = (byte) 95;
      numArray4[1] = (byte) 121;
      numArray4[3] = (byte) 25;
      numArray4[5] = (byte) 37;
      numArray4[0] = (byte) 253;
      numArray4[6] = (byte) 6;
      numArray4[7] = (byte) 234;
      byte[] numArray5 = new byte[8];
      numArray5[2] = (byte) 51;
      numArray5[4] = (byte) 115;
      numArray5[7] = (byte) 173;
      numArray5[3] = (byte) 2;
      numArray5[0] = (byte) 56;
      numArray5[5] = (byte) 164;
      numArray5[1] = (byte) 156;
      numArray5[6] = (byte) 208 /*0xD0*/;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 8);
      for (int index = 0; index < 8; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[63 /*0x3F*/];
    byte[] numArray7 = new byte[55]
    {
      (byte) 5,
      (byte) 159,
      (byte) 38,
      (byte) 122,
      (byte) 243,
      (byte) 74,
      (byte) 221,
      (byte) 119,
      (byte) 108,
      (byte) 201,
      (byte) 227,
      (byte) 177,
      (byte) 212,
      (byte) 51,
      (byte) 72,
      (byte) 187,
      (byte) 129,
      (byte) 139,
      (byte) 254,
      (byte) 100,
      (byte) 217,
      (byte) 30,
      (byte) 240 /*0xF0*/,
      (byte) 145,
      (byte) 164,
      (byte) 168,
      (byte) 16 /*0x10*/,
      (byte) 124,
      (byte) 122,
      (byte) 9,
      (byte) 26,
      (byte) 144 /*0x90*/,
      (byte) 7,
      (byte) 194,
      (byte) 1,
      (byte) 75,
      (byte) 68,
      (byte) 104,
      (byte) 171,
      (byte) 8,
      (byte) 45,
      (byte) 207,
      (byte) 82,
      (byte) 165,
      (byte) 61,
      (byte) 241,
      (byte) 213,
      (byte) 105,
      (byte) 182,
      (byte) 200,
      (byte) 239,
      (byte) 185,
      (byte) 138,
      (byte) 177,
      (byte) 138
    };
    byte[] numArray8 = new byte[55]
    {
      (byte) 48 /*0x30*/,
      (byte) 72,
      (byte) 162,
      (byte) 100,
      (byte) 126,
      (byte) 244,
      (byte) 226,
      (byte) 13,
      (byte) 138,
      (byte) 41,
      (byte) 94,
      (byte) 188,
      (byte) 215,
      (byte) 125,
      (byte) 57,
      (byte) 147,
      (byte) 43,
      (byte) 151,
      (byte) 39,
      (byte) 124,
      (byte) 142,
      (byte) 133,
      (byte) 230,
      (byte) 136,
      (byte) 209,
      (byte) 63 /*0x3F*/,
      (byte) 180,
      (byte) 53,
      (byte) 89,
      (byte) 69,
      (byte) 15,
      (byte) 223,
      (byte) 159,
      (byte) 32 /*0x20*/,
      (byte) 132,
      (byte) 105,
      (byte) 192 /*0xC0*/,
      (byte) 155,
      (byte) 139,
      (byte) 47,
      (byte) 32 /*0x20*/,
      (byte) 175,
      (byte) 10,
      (byte) 75,
      (byte) 252,
      (byte) 34,
      (byte) 186,
      (byte) 10,
      (byte) 174,
      (byte) 179,
      (byte) 124,
      (byte) 56,
      (byte) 54,
      (byte) 167,
      (byte) 75
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[8]
    {
      (byte) 148,
      (byte) 214,
      (byte) 75,
      (byte) 57,
      (byte) 97,
      (byte) 253,
      (byte) 99,
      (byte) 175
    };
    byte[] numArray10 = new byte[8]
    {
      (byte) 241,
      (byte) 82,
      (byte) 94,
      (byte) 167,
      (byte) 106,
      (byte) 215,
      (byte) 87,
      (byte) 120
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 8);
    for (int index = 0; index < 8; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static int ssp_appserver_13529(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[21] = (byte) 176 /*0xB0*/;
    sourceArray1[1] = (byte) 24;
    sourceArray1[2] = (byte) 224 /*0xE0*/;
    sourceArray1[3] = (byte) 246;
    sourceArray1[11] = (byte) 166;
    sourceArray1[23] = (byte) 95;
    sourceArray1[6] = (byte) 203;
    sourceArray1[7] = (byte) 74;
    sourceArray1[46] = (byte) 155;
    sourceArray1[9] = (byte) 178;
    sourceArray1[10] = (byte) 210;
    sourceArray1[37] = (byte) 147;
    sourceArray1[15] = (byte) 96 /*0x60*/;
    sourceArray1[33] = (byte) 185;
    sourceArray1[14] = (byte) 25;
    sourceArray1[30] = (byte) 71;
    sourceArray1[28] = (byte) 246;
    sourceArray1[17] = (byte) 85;
    sourceArray1[18] = (byte) 56;
    sourceArray1[29] = (byte) 234;
    sourceArray1[20] = (byte) 155;
    sourceArray1[42] = (byte) 64 /*0x40*/;
    sourceArray1[45] = (byte) 147;
    sourceArray1[12] = (byte) 197;
    sourceArray1[31 /*0x1F*/] = (byte) 195;
    sourceArray1[5] = (byte) 9;
    sourceArray1[26] = (byte) 210;
    sourceArray1[27] = (byte) 239;
    sourceArray1[16 /*0x10*/] = (byte) 6;
    sourceArray1[22] = (byte) 145;
    sourceArray1[25] = (byte) 203;
    sourceArray1[41] = (byte) 100;
    sourceArray1[32 /*0x20*/] = (byte) 55;
    sourceArray1[0] = (byte) 118;
    sourceArray1[24] = (byte) 206;
    sourceArray1[35] = (byte) 214;
    sourceArray1[36] = (byte) 216;
    sourceArray1[47] = (byte) 74;
    sourceArray1[38] = (byte) 121;
    sourceArray1[39] = (byte) 128 /*0x80*/;
    sourceArray1[40] = (byte) 27;
    sourceArray1[34] = (byte) 81;
    sourceArray1[8] = (byte) 83;
    sourceArray1[43] = (byte) 35;
    sourceArray1[44] = (byte) 120;
    sourceArray1[4] = (byte) 55;
    sourceArray1[13] = (byte) 178;
    sourceArray1[19] = (byte) 25;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[39] = (byte) 20;
    sourceArray2[1] = (byte) 176 /*0xB0*/;
    sourceArray2[2] = (byte) 77;
    sourceArray2[33] = (byte) 167;
    sourceArray2[46] = (byte) 141;
    sourceArray2[10] = (byte) 69;
    sourceArray2[22] = (byte) 199;
    sourceArray2[7] = (byte) 248;
    sourceArray2[6] = (byte) 253;
    sourceArray2[4] = (byte) 64 /*0x40*/;
    sourceArray2[0] = (byte) 146;
    sourceArray2[23] = (byte) 221;
    sourceArray2[12] = (byte) 222;
    sourceArray2[13] = (byte) 122;
    sourceArray2[11] = (byte) 21;
    sourceArray2[15] = (byte) 170;
    sourceArray2[9] = (byte) 203;
    sourceArray2[41] = (byte) 8;
    sourceArray2[18] = (byte) 42;
    sourceArray2[16 /*0x10*/] = (byte) 164;
    sourceArray2[43] = (byte) 8;
    sourceArray2[14] = (byte) 226;
    sourceArray2[3] = (byte) 215;
    sourceArray2[32 /*0x20*/] = (byte) 167;
    sourceArray2[5] = (byte) 201;
    sourceArray2[20] = (byte) 230;
    sourceArray2[26] = (byte) 17;
    sourceArray2[35] = (byte) 184;
    sourceArray2[28] = (byte) 153;
    sourceArray2[25] = (byte) 133;
    sourceArray2[21] = (byte) 225;
    sourceArray2[31 /*0x1F*/] = (byte) 167;
    sourceArray2[8] = (byte) 66;
    sourceArray2[30] = (byte) 1;
    sourceArray2[42] = (byte) 3;
    sourceArray2[29] = (byte) 242;
    sourceArray2[36] = (byte) 5;
    sourceArray2[37] = (byte) 126;
    sourceArray2[38] = (byte) 13;
    sourceArray2[44] = (byte) 170;
    sourceArray2[40] = (byte) 192 /*0xC0*/;
    sourceArray2[27] = (byte) 158;
    sourceArray2[19] = (byte) 130;
    sourceArray2[17] = (byte) 70;
    sourceArray2[34] = (byte) 135;
    sourceArray2[45] = (byte) 198;
    sourceArray2[24] = (byte) 84;
    sourceArray2[47] = (byte) 7;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13530(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[28] = (byte) 133;
    sourceArray1[32 /*0x20*/] = (byte) 134;
    sourceArray1[1] = (byte) 9;
    sourceArray1[26] = (byte) 89;
    sourceArray1[3] = (byte) 13;
    sourceArray1[5] = (byte) 70;
    sourceArray1[6] = (byte) 211;
    sourceArray1[7] = (byte) 218;
    sourceArray1[46] = (byte) 209;
    sourceArray1[30] = (byte) 188;
    sourceArray1[2] = (byte) 97;
    sourceArray1[17] = (byte) 98;
    sourceArray1[8] = (byte) 21;
    sourceArray1[12] = (byte) 169;
    sourceArray1[13] = (byte) 177;
    sourceArray1[15] = (byte) 0;
    sourceArray1[16 /*0x10*/] = (byte) 191;
    sourceArray1[44] = (byte) 73;
    sourceArray1[36] = (byte) 198;
    sourceArray1[39] = (byte) 54;
    sourceArray1[23] = (byte) 204;
    sourceArray1[21] = (byte) 19;
    sourceArray1[31 /*0x1F*/] = (byte) 113;
    sourceArray1[19] = (byte) 212;
    sourceArray1[24] = (byte) 5;
    sourceArray1[29] = (byte) 160 /*0xA0*/;
    sourceArray1[10] = (byte) 150;
    sourceArray1[27] = (byte) 76;
    sourceArray1[11] = (byte) 211;
    sourceArray1[4] = (byte) 64 /*0x40*/;
    sourceArray1[14] = (byte) 92;
    sourceArray1[41] = (byte) 197;
    sourceArray1[18] = (byte) 28;
    sourceArray1[33] = (byte) 252;
    sourceArray1[34] = (byte) 214;
    sourceArray1[35] = (byte) 180;
    sourceArray1[25] = (byte) 252;
    sourceArray1[0] = (byte) 152;
    sourceArray1[22] = (byte) 151;
    sourceArray1[20] = (byte) 156;
    sourceArray1[37] = (byte) 138;
    sourceArray1[40] = (byte) 68;
    sourceArray1[42] = (byte) 135;
    sourceArray1[43] = (byte) 138;
    sourceArray1[38] = (byte) 118;
    sourceArray1[45] = (byte) 241;
    sourceArray1[9] = (byte) 20;
    sourceArray1[47] = (byte) 25;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 84,
      (byte) 88,
      (byte) 63 /*0x3F*/,
      (byte) 121,
      (byte) 216,
      (byte) 244,
      (byte) 56,
      (byte) 197,
      (byte) 89,
      (byte) 229,
      (byte) 31 /*0x1F*/,
      (byte) 237,
      (byte) 73,
      (byte) 30,
      (byte) 142,
      (byte) 50,
      (byte) 212,
      (byte) 211,
      (byte) 62,
      (byte) 204,
      (byte) 30,
      (byte) 80 /*0x50*/,
      (byte) 185,
      (byte) 34,
      (byte) 232,
      (byte) 124,
      (byte) 72,
      (byte) 76,
      (byte) 162,
      (byte) 44,
      (byte) 94,
      (byte) 175,
      (byte) 84,
      (byte) 159,
      (byte) 53,
      (byte) 103,
      (byte) 168,
      (byte) 10,
      (byte) 214,
      (byte) 118,
      (byte) 67,
      (byte) 229,
      (byte) 156,
      (byte) 159,
      (byte) 58,
      (byte) 115,
      (byte) 87,
      (byte) 212
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13531(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 35,
      (byte) 62,
      byte.MaxValue,
      (byte) 244,
      (byte) 64 /*0x40*/,
      (byte) 231,
      (byte) 188,
      (byte) 28,
      (byte) 94,
      (byte) 249,
      (byte) 100,
      (byte) 17,
      (byte) 243,
      (byte) 23,
      (byte) 173,
      (byte) 227,
      (byte) 253,
      (byte) 74,
      (byte) 111,
      (byte) 86,
      (byte) 115,
      (byte) 229,
      (byte) 136,
      (byte) 101,
      (byte) 7,
      (byte) 178,
      (byte) 185,
      (byte) 156,
      (byte) 210,
      (byte) 101,
      (byte) 121,
      (byte) 219,
      (byte) 95,
      (byte) 113,
      (byte) 7,
      (byte) 1,
      (byte) 164,
      (byte) 98,
      (byte) 233,
      (byte) 0,
      (byte) 18,
      (byte) 189,
      (byte) 104,
      (byte) 87,
      (byte) 222,
      (byte) 31 /*0x1F*/,
      (byte) 143,
      (byte) 27
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 254,
      (byte) 245,
      (byte) 116,
      (byte) 143,
      (byte) 104,
      (byte) 36,
      (byte) 37,
      (byte) 212,
      (byte) 111,
      (byte) 106,
      (byte) 92,
      (byte) 105,
      (byte) 16 /*0x10*/,
      (byte) 168,
      (byte) 29,
      (byte) 49,
      (byte) 233,
      (byte) 127 /*0x7F*/,
      (byte) 117,
      (byte) 140,
      (byte) 127 /*0x7F*/,
      (byte) 9,
      (byte) 189,
      (byte) 187,
      (byte) 119,
      byte.MaxValue,
      (byte) 208 /*0xD0*/,
      (byte) 143,
      (byte) 116,
      (byte) 13,
      (byte) 73,
      (byte) 32 /*0x20*/,
      (byte) 241,
      (byte) 88,
      (byte) 52,
      (byte) 34,
      (byte) 1,
      (byte) 123,
      (byte) 50,
      (byte) 253,
      (byte) 9,
      (byte) 9,
      (byte) 23,
      (byte) 167,
      (byte) 240 /*0xF0*/,
      (byte) 164,
      (byte) 207,
      (byte) 103
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[37];
    byte[] response2 = new byte[37];
    Array.Copy((Array) sc_13518.sspq, 78, (Array) numArray2, 0, 37);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13518.sspr, 78, (Array) numArray2, 0, 37);
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

  internal static int ssp_appserver_13532(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[35] = (byte) 36;
    sourceArray1[1] = (byte) 118;
    sourceArray1[22] = (byte) 4;
    sourceArray1[8] = (byte) 84;
    sourceArray1[31 /*0x1F*/] = (byte) 65;
    sourceArray1[0] = (byte) 10;
    sourceArray1[6] = (byte) 111;
    sourceArray1[47] = (byte) 92;
    sourceArray1[34] = (byte) 52;
    sourceArray1[42] = (byte) 50;
    sourceArray1[25] = (byte) 31 /*0x1F*/;
    sourceArray1[7] = (byte) 215;
    sourceArray1[23] = (byte) 107;
    sourceArray1[13] = (byte) 155;
    sourceArray1[14] = (byte) 216;
    sourceArray1[29] = (byte) 113;
    sourceArray1[16 /*0x10*/] = (byte) 84;
    sourceArray1[43] = (byte) 122;
    sourceArray1[18] = (byte) 122;
    sourceArray1[21] = (byte) 18;
    sourceArray1[20] = (byte) 133;
    sourceArray1[33] = (byte) 96 /*0x60*/;
    sourceArray1[2] = (byte) 91;
    sourceArray1[28] = (byte) 205;
    sourceArray1[24] = (byte) 143;
    sourceArray1[12] = (byte) 240 /*0xF0*/;
    sourceArray1[26] = (byte) 81;
    sourceArray1[11] = (byte) 12;
    sourceArray1[30] = (byte) 32 /*0x20*/;
    sourceArray1[38] = (byte) 80 /*0x50*/;
    sourceArray1[9] = (byte) 109;
    sourceArray1[44] = (byte) 22;
    sourceArray1[32 /*0x20*/] = (byte) 69;
    sourceArray1[37] = (byte) 155;
    sourceArray1[39] = (byte) 65;
    sourceArray1[17] = (byte) 136;
    sourceArray1[36] = (byte) 5;
    sourceArray1[46] = (byte) 15;
    sourceArray1[19] = (byte) 192 /*0xC0*/;
    sourceArray1[4] = (byte) 243;
    sourceArray1[40] = (byte) 13;
    sourceArray1[41] = (byte) 88;
    sourceArray1[15] = (byte) 102;
    sourceArray1[3] = (byte) 211;
    sourceArray1[45] = (byte) 145;
    sourceArray1[27] = (byte) 6;
    sourceArray1[5] = (byte) 116;
    sourceArray1[10] = (byte) 12;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 229,
      (byte) 206,
      (byte) 205,
      (byte) 143,
      (byte) 74,
      (byte) 59,
      (byte) 145,
      (byte) 34,
      (byte) 213,
      (byte) 134,
      (byte) 43,
      (byte) 210,
      (byte) 217,
      (byte) 108,
      (byte) 130,
      (byte) 187,
      (byte) 91,
      (byte) 96 /*0x60*/,
      (byte) 157,
      (byte) 122,
      (byte) 165,
      (byte) 166,
      (byte) 179,
      (byte) 80 /*0x50*/,
      (byte) 67,
      (byte) 196,
      (byte) 190,
      (byte) 159,
      (byte) 54,
      (byte) 94,
      (byte) 180,
      (byte) 81,
      (byte) 89,
      (byte) 40,
      (byte) 50,
      (byte) 52,
      (byte) 142,
      (byte) 118,
      (byte) 209,
      (byte) 185,
      (byte) 157,
      (byte) 204,
      (byte) 33,
      (byte) 245,
      (byte) 81,
      (byte) 187,
      (byte) 89,
      (byte) 245
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
