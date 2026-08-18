// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_13556
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_13556
{
  private static byte[] sspq = new byte[110]
  {
    (byte) 88,
    (byte) 174,
    (byte) 114,
    (byte) 84,
    (byte) 124,
    (byte) 5,
    (byte) 243,
    (byte) 48 /*0x30*/,
    (byte) 39,
    (byte) 13,
    (byte) 248,
    (byte) 169,
    (byte) 36,
    (byte) 252,
    (byte) 194,
    (byte) 189,
    (byte) 175,
    (byte) 247,
    (byte) 226,
    (byte) 243,
    (byte) 145,
    (byte) 191,
    (byte) 136,
    (byte) 213,
    (byte) 1,
    (byte) 235,
    (byte) 61,
    (byte) 15,
    (byte) 84,
    (byte) 133,
    (byte) 50,
    (byte) 193,
    (byte) 122,
    (byte) 75,
    (byte) 178,
    (byte) 207,
    (byte) 7,
    (byte) 95,
    (byte) 61,
    (byte) 58,
    (byte) 69,
    (byte) 113,
    (byte) 62,
    (byte) 83,
    (byte) 28,
    (byte) 120,
    (byte) 111,
    (byte) 117,
    (byte) 201,
    (byte) 166,
    (byte) 59,
    (byte) 111,
    (byte) 199,
    (byte) 159,
    (byte) 245,
    (byte) 156,
    (byte) 158,
    (byte) 244,
    (byte) 83,
    (byte) 188,
    (byte) 96 /*0x60*/,
    (byte) 218,
    (byte) 168,
    (byte) 174,
    (byte) 73,
    (byte) 55,
    (byte) 103,
    (byte) 7,
    (byte) 225,
    (byte) 173,
    (byte) 151,
    (byte) 58,
    (byte) 244,
    (byte) 130,
    (byte) 7,
    (byte) 78,
    (byte) 64 /*0x40*/,
    (byte) 232,
    (byte) 195,
    (byte) 102,
    (byte) 52,
    (byte) 34,
    (byte) 233,
    (byte) 21,
    (byte) 118,
    (byte) 253,
    (byte) 228,
    (byte) 14,
    (byte) 47,
    (byte) 79,
    (byte) 86,
    (byte) 64 /*0x40*/,
    (byte) 55,
    (byte) 156,
    (byte) 157,
    (byte) 45,
    (byte) 110,
    (byte) 150,
    (byte) 193,
    (byte) 101,
    (byte) 119,
    (byte) 61,
    (byte) 61,
    (byte) 23,
    (byte) 177,
    (byte) 251,
    (byte) 165,
    (byte) 48 /*0x30*/,
    (byte) 246,
    (byte) 47
  };
  private static byte[] sspr = new byte[110]
  {
    (byte) 125,
    (byte) 247,
    (byte) 90,
    (byte) 7,
    (byte) 228,
    (byte) 103,
    (byte) 223,
    (byte) 16 /*0x10*/,
    (byte) 210,
    (byte) 190,
    (byte) 63 /*0x3F*/,
    (byte) 238,
    (byte) 14,
    (byte) 187,
    (byte) 109,
    (byte) 216,
    byte.MaxValue,
    (byte) 40,
    (byte) 60,
    (byte) 20,
    (byte) 107,
    (byte) 27,
    (byte) 2,
    (byte) 22,
    (byte) 151,
    (byte) 95,
    (byte) 239,
    (byte) 79,
    (byte) 111,
    (byte) 129,
    (byte) 157,
    (byte) 66,
    (byte) 109,
    (byte) 141,
    (byte) 146,
    (byte) 245,
    (byte) 188,
    (byte) 22,
    (byte) 123,
    (byte) 211,
    (byte) 231,
    (byte) 5,
    (byte) 194,
    (byte) 145,
    (byte) 119,
    (byte) 190,
    (byte) 254,
    (byte) 47,
    (byte) 14,
    (byte) 36,
    (byte) 43,
    (byte) 133,
    (byte) 68,
    (byte) 22,
    (byte) 245,
    (byte) 161,
    (byte) 153,
    (byte) 77,
    (byte) 172,
    (byte) 226,
    (byte) 98,
    (byte) 251,
    (byte) 90,
    (byte) 223,
    (byte) 205,
    (byte) 209,
    (byte) 99,
    (byte) 83,
    (byte) 168,
    (byte) 19,
    (byte) 79,
    (byte) 90,
    (byte) 74,
    (byte) 7,
    (byte) 66,
    (byte) 170,
    (byte) 215,
    (byte) 172,
    (byte) 132,
    (byte) 168,
    (byte) 59,
    (byte) 66,
    (byte) 75,
    (byte) 249,
    (byte) 219,
    (byte) 206,
    (byte) 171,
    (byte) 54,
    (byte) 100,
    (byte) 104,
    (byte) 162,
    (byte) 166,
    (byte) 140,
    (byte) 96 /*0x60*/,
    (byte) 129,
    (byte) 215,
    (byte) 47,
    (byte) 215,
    (byte) 190,
    (byte) 52,
    (byte) 87,
    (byte) 94,
    (byte) 204,
    (byte) 204,
    (byte) 64 /*0x40*/,
    (byte) 150,
    (byte) 0,
    (byte) 136,
    (byte) 207,
    (byte) 103
  };

  internal static int ssp_appserver_13557(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[7] = (byte) 249;
    sourceArray1[30] = (byte) 215;
    sourceArray1[47] = (byte) 106;
    sourceArray1[3] = (byte) 167;
    sourceArray1[4] = (byte) 170;
    sourceArray1[36] = (byte) 132;
    sourceArray1[33] = (byte) 50;
    sourceArray1[28] = (byte) 232;
    sourceArray1[38] = (byte) 249;
    sourceArray1[9] = (byte) 44;
    sourceArray1[25] = (byte) 56;
    sourceArray1[2] = (byte) 101;
    sourceArray1[41] = (byte) 254;
    sourceArray1[13] = (byte) 15;
    sourceArray1[14] = byte.MaxValue;
    sourceArray1[39] = (byte) 239;
    sourceArray1[37] = (byte) 248;
    sourceArray1[16 /*0x10*/] = (byte) 51;
    sourceArray1[42] = (byte) 123;
    sourceArray1[8] = (byte) 97;
    sourceArray1[20] = (byte) 231;
    sourceArray1[44] = (byte) 7;
    sourceArray1[22] = (byte) 47;
    sourceArray1[46] = (byte) 0;
    sourceArray1[12] = (byte) 21;
    sourceArray1[27] = (byte) 250;
    sourceArray1[26] = (byte) 216;
    sourceArray1[6] = (byte) 76;
    sourceArray1[17] = (byte) 67;
    sourceArray1[45] = (byte) 101;
    sourceArray1[5] = (byte) 134;
    sourceArray1[31 /*0x1F*/] = (byte) 196;
    sourceArray1[1] = (byte) 67;
    sourceArray1[29] = (byte) 173;
    sourceArray1[34] = (byte) 221;
    sourceArray1[35] = (byte) 171;
    sourceArray1[18] = (byte) 198;
    sourceArray1[10] = (byte) 105;
    sourceArray1[32 /*0x20*/] = (byte) 58;
    sourceArray1[23] = (byte) 245;
    sourceArray1[40] = (byte) 22;
    sourceArray1[19] = (byte) 168;
    sourceArray1[24] = (byte) 75;
    sourceArray1[21] = (byte) 201;
    sourceArray1[0] = (byte) 74;
    sourceArray1[15] = (byte) 116;
    sourceArray1[11] = (byte) 128 /*0x80*/;
    sourceArray1[43] = (byte) 172;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 158,
      (byte) 135,
      (byte) 126,
      (byte) 150,
      (byte) 212,
      (byte) 70,
      (byte) 71,
      (byte) 124,
      (byte) 99,
      (byte) 183,
      (byte) 61,
      (byte) 44,
      (byte) 123,
      (byte) 198,
      (byte) 87,
      (byte) 164,
      (byte) 46,
      (byte) 1,
      (byte) 11,
      (byte) 83,
      (byte) 230,
      (byte) 160 /*0xA0*/,
      (byte) 52,
      (byte) 152,
      (byte) 72,
      (byte) 34,
      (byte) 165,
      (byte) 229,
      (byte) 142,
      (byte) 88,
      (byte) 216,
      (byte) 235,
      (byte) 69,
      (byte) 196,
      (byte) 149,
      (byte) 230,
      (byte) 59,
      (byte) 53,
      (byte) 33,
      (byte) 128 /*0x80*/,
      (byte) 156,
      (byte) 185,
      (byte) 22,
      (byte) 249,
      (byte) 246,
      (byte) 66,
      (byte) 240 /*0xF0*/,
      (byte) 101
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13558(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 29,
      (byte) 56,
      (byte) 24,
      (byte) 80 /*0x50*/,
      (byte) 98,
      (byte) 60,
      (byte) 132,
      (byte) 224 /*0xE0*/,
      (byte) 252,
      (byte) 51,
      (byte) 145,
      (byte) 129,
      (byte) 118,
      (byte) 58,
      (byte) 64 /*0x40*/,
      (byte) 195,
      (byte) 105,
      (byte) 163,
      (byte) 5,
      (byte) 58,
      (byte) 126,
      (byte) 150,
      (byte) 213,
      (byte) 187,
      (byte) 219,
      (byte) 157,
      (byte) 195,
      (byte) 148,
      (byte) 206,
      (byte) 251,
      (byte) 134,
      (byte) 43,
      (byte) 139,
      (byte) 197,
      (byte) 26,
      (byte) 53,
      (byte) 118,
      (byte) 87,
      (byte) 174,
      (byte) 18,
      (byte) 229,
      (byte) 230,
      (byte) 79,
      (byte) 41,
      (byte) 6,
      (byte) 147,
      (byte) 38,
      (byte) 76
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 115,
      (byte) 194,
      (byte) 79,
      (byte) 40,
      (byte) 48 /*0x30*/,
      (byte) 69,
      (byte) 4,
      byte.MaxValue,
      (byte) 17,
      (byte) 189,
      (byte) 187,
      (byte) 87,
      (byte) 30,
      (byte) 18,
      (byte) 29,
      (byte) 63 /*0x3F*/,
      (byte) 127 /*0x7F*/,
      (byte) 81,
      (byte) 165,
      (byte) 184,
      (byte) 248,
      (byte) 75,
      (byte) 41,
      (byte) 194,
      (byte) 68,
      (byte) 73,
      (byte) 166,
      (byte) 184,
      (byte) 72,
      (byte) 167,
      (byte) 90,
      (byte) 90,
      (byte) 170,
      (byte) 153,
      (byte) 31 /*0x1F*/,
      (byte) 22,
      (byte) 168,
      (byte) 173,
      (byte) 9,
      (byte) 213,
      (byte) 66,
      (byte) 0,
      (byte) 122,
      (byte) 187,
      (byte) 23,
      (byte) 204,
      (byte) 158,
      (byte) 117
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13559(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 40,
      (byte) 161,
      (byte) 135,
      (byte) 99,
      (byte) 92,
      (byte) 117,
      (byte) 196,
      (byte) 121,
      (byte) 240 /*0xF0*/,
      (byte) 17,
      (byte) 1,
      (byte) 172,
      (byte) 101,
      (byte) 246,
      (byte) 177,
      (byte) 31 /*0x1F*/,
      (byte) 10,
      (byte) 76,
      (byte) 18,
      (byte) 245,
      (byte) 243,
      (byte) 85,
      (byte) 151,
      (byte) 70,
      (byte) 172,
      (byte) 180,
      (byte) 71,
      (byte) 119,
      (byte) 118,
      (byte) 146,
      (byte) 206,
      (byte) 120,
      (byte) 98,
      (byte) 217,
      (byte) 98,
      (byte) 35,
      (byte) 146,
      (byte) 209,
      (byte) 25,
      (byte) 226,
      (byte) 233,
      (byte) 153,
      (byte) 245,
      (byte) 213,
      (byte) 78,
      (byte) 124,
      (byte) 160 /*0xA0*/,
      (byte) 157
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[8] = (byte) 107;
    sourceArray2[2] = (byte) 248;
    sourceArray2[32 /*0x20*/] = (byte) 197;
    sourceArray2[12] = (byte) 49;
    sourceArray2[4] = (byte) 81;
    sourceArray2[5] = (byte) 186;
    sourceArray2[23] = (byte) 135;
    sourceArray2[7] = (byte) 85;
    sourceArray2[21] = byte.MaxValue;
    sourceArray2[24] = (byte) 32 /*0x20*/;
    sourceArray2[10] = (byte) 198;
    sourceArray2[18] = (byte) 150;
    sourceArray2[20] = (byte) 213;
    sourceArray2[42] = (byte) 220;
    sourceArray2[14] = (byte) 228;
    sourceArray2[29] = (byte) 229;
    sourceArray2[16 /*0x10*/] = (byte) 16 /*0x10*/;
    sourceArray2[30] = (byte) 138;
    sourceArray2[15] = (byte) 104;
    sourceArray2[11] = (byte) 151;
    sourceArray2[17] = (byte) 14;
    sourceArray2[9] = (byte) 252;
    sourceArray2[22] = (byte) 174;
    sourceArray2[1] = (byte) 233;
    sourceArray2[3] = (byte) 115;
    sourceArray2[44] = (byte) 57;
    sourceArray2[26] = (byte) 164;
    sourceArray2[27] = (byte) 148;
    sourceArray2[19] = (byte) 187;
    sourceArray2[47] = (byte) 71;
    sourceArray2[0] = (byte) 107;
    sourceArray2[31 /*0x1F*/] = (byte) 103;
    sourceArray2[35] = (byte) 177;
    sourceArray2[33] = (byte) 119;
    sourceArray2[34] = (byte) 120;
    sourceArray2[39] = (byte) 153;
    sourceArray2[6] = (byte) 6;
    sourceArray2[37] = (byte) 228;
    sourceArray2[38] = (byte) 132;
    sourceArray2[28] = (byte) 60;
    sourceArray2[40] = (byte) 125;
    sourceArray2[41] = (byte) 102;
    sourceArray2[36] = (byte) 127 /*0x7F*/;
    sourceArray2[43] = (byte) 75;
    sourceArray2[13] = (byte) 43;
    sourceArray2[45] = (byte) 181;
    sourceArray2[46] = (byte) 222;
    sourceArray2[25] = (byte) 75;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_13560()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[100];
      byte[] numArray2 = new byte[55]
      {
        (byte) 50,
        (byte) 20,
        (byte) 9,
        (byte) 231,
        (byte) 159,
        (byte) 189,
        (byte) 129,
        (byte) 63 /*0x3F*/,
        (byte) 35,
        (byte) 5,
        (byte) 115,
        (byte) 72,
        (byte) 73,
        (byte) 136,
        (byte) 148,
        (byte) 130,
        (byte) 75,
        (byte) 184,
        (byte) 192 /*0xC0*/,
        (byte) 41,
        (byte) 128 /*0x80*/,
        (byte) 22,
        (byte) 17,
        (byte) 4,
        (byte) 116,
        (byte) 59,
        (byte) 31 /*0x1F*/,
        (byte) 192 /*0xC0*/,
        (byte) 123,
        (byte) 112 /*0x70*/,
        (byte) 194,
        (byte) 199,
        (byte) 48 /*0x30*/,
        (byte) 48 /*0x30*/,
        (byte) 253,
        byte.MaxValue,
        (byte) 161,
        (byte) 26,
        (byte) 170,
        (byte) 223,
        (byte) 152,
        (byte) 200,
        (byte) 16 /*0x10*/,
        (byte) 117,
        (byte) 27,
        (byte) 208 /*0xD0*/,
        (byte) 8,
        (byte) 145,
        (byte) 221,
        (byte) 50,
        (byte) 79,
        (byte) 223,
        (byte) 216,
        (byte) 104,
        (byte) 69
      };
      byte[] numArray3 = new byte[55];
      numArray3[52] = (byte) 228;
      numArray3[14] = (byte) 12;
      numArray3[3] = (byte) 21;
      numArray3[10] = (byte) 241;
      numArray3[4] = (byte) 147;
      numArray3[35] = (byte) 213;
      numArray3[6] = (byte) 214;
      numArray3[7] = (byte) 241;
      numArray3[39] = (byte) 65;
      numArray3[13] = (byte) 24;
      numArray3[32 /*0x20*/] = (byte) 168;
      numArray3[11] = (byte) 134;
      numArray3[53] = (byte) 193;
      numArray3[40] = (byte) 242;
      numArray3[19] = (byte) 23;
      numArray3[15] = (byte) 56;
      numArray3[8] = (byte) 175;
      numArray3[22] = (byte) 107;
      numArray3[18] = (byte) 177;
      numArray3[54] = (byte) 12;
      numArray3[20] = (byte) 236;
      numArray3[21] = (byte) 227;
      numArray3[46] = (byte) 112 /*0x70*/;
      numArray3[23] = (byte) 191;
      numArray3[24] = (byte) 252;
      numArray3[31 /*0x1F*/] = (byte) 68;
      numArray3[26] = (byte) 84;
      numArray3[27] = (byte) 199;
      numArray3[9] = (byte) 41;
      numArray3[29] = byte.MaxValue;
      numArray3[30] = (byte) 43;
      numArray3[12] = (byte) 142;
      numArray3[28] = (byte) 156;
      numArray3[33] = (byte) 228;
      numArray3[34] = (byte) 204;
      numArray3[37] = (byte) 127 /*0x7F*/;
      numArray3[2] = (byte) 176 /*0xB0*/;
      numArray3[36] = (byte) 161;
      numArray3[38] = (byte) 45;
      numArray3[45] = (byte) 86;
      numArray3[17] = (byte) 49;
      numArray3[41] = (byte) 244;
      numArray3[25] = (byte) 11;
      numArray3[43] = (byte) 20;
      numArray3[44] = (byte) 148;
      numArray3[16 /*0x10*/] = (byte) 231;
      numArray3[0] = (byte) 162;
      numArray3[47] = (byte) 136;
      numArray3[48 /*0x30*/] = (byte) 63 /*0x3F*/;
      numArray3[50] = (byte) 216;
      numArray3[49] = (byte) 66;
      numArray3[51] = (byte) 56;
      numArray3[42] = (byte) 106;
      numArray3[5] = (byte) 26;
      numArray3[1] = (byte) 178;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[45];
      numArray4[11] = (byte) 168;
      numArray4[1] = (byte) 206;
      numArray4[36] = (byte) 21;
      numArray4[37] = (byte) 49;
      numArray4[4] = (byte) 220;
      numArray4[23] = (byte) 68;
      numArray4[6] = (byte) 145;
      numArray4[7] = (byte) 96 /*0x60*/;
      numArray4[8] = (byte) 189;
      numArray4[15] = (byte) 199;
      numArray4[10] = (byte) 45;
      numArray4[20] = (byte) 105;
      numArray4[30] = (byte) 240 /*0xF0*/;
      numArray4[13] = (byte) 69;
      numArray4[31 /*0x1F*/] = (byte) 93;
      numArray4[5] = (byte) 123;
      numArray4[9] = (byte) 79;
      numArray4[17] = (byte) 185;
      numArray4[33] = (byte) 149;
      numArray4[39] = (byte) 136;
      numArray4[24] = (byte) 134;
      numArray4[21] = (byte) 122;
      numArray4[42] = (byte) 176 /*0xB0*/;
      numArray4[14] = (byte) 137;
      numArray4[12] = (byte) 152;
      numArray4[22] = (byte) 234;
      numArray4[19] = (byte) 168;
      numArray4[2] = (byte) 48 /*0x30*/;
      numArray4[28] = (byte) 17;
      numArray4[29] = (byte) 247;
      numArray4[18] = (byte) 180;
      numArray4[34] = (byte) 127 /*0x7F*/;
      numArray4[32 /*0x20*/] = (byte) 205;
      numArray4[26] = (byte) 247;
      numArray4[44] = (byte) 193;
      numArray4[35] = (byte) 217;
      numArray4[3] = (byte) 16 /*0x10*/;
      numArray4[0] = (byte) 28;
      numArray4[38] = (byte) 250;
      numArray4[16 /*0x10*/] = (byte) 159;
      numArray4[40] = (byte) 204;
      numArray4[41] = (byte) 210;
      numArray4[25] = (byte) 155;
      numArray4[43] = (byte) 105;
      numArray4[27] = (byte) 2;
      byte[] numArray5 = new byte[45]
      {
        (byte) 198,
        (byte) 47,
        (byte) 153,
        (byte) 34,
        (byte) 215,
        (byte) 154,
        (byte) 188,
        (byte) 120,
        (byte) 166,
        (byte) 139,
        (byte) 125,
        (byte) 79,
        (byte) 241,
        (byte) 73,
        (byte) 253,
        (byte) 242,
        (byte) 227,
        (byte) 39,
        (byte) 59,
        (byte) 113,
        (byte) 113,
        (byte) 212,
        (byte) 6,
        (byte) 204,
        (byte) 15,
        (byte) 15,
        (byte) 190,
        (byte) 11,
        (byte) 1,
        (byte) 80 /*0x50*/,
        (byte) 94,
        (byte) 62,
        (byte) 150,
        (byte) 149,
        (byte) 58,
        (byte) 238,
        (byte) 151,
        (byte) 194,
        (byte) 80 /*0x50*/,
        (byte) 219,
        (byte) 50,
        (byte) 243,
        (byte) 52,
        (byte) 147,
        (byte) 45
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 45);
      for (int index = 0; index < 45; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[100];
    byte[] numArray7 = new byte[55];
    numArray7[46] = (byte) 181;
    numArray7[25] = (byte) 37;
    numArray7[42] = (byte) 62;
    numArray7[3] = (byte) 131;
    numArray7[38] = (byte) 222;
    numArray7[5] = (byte) 11;
    numArray7[2] = (byte) 62;
    numArray7[35] = (byte) 38;
    numArray7[37] = (byte) 19;
    numArray7[9] = (byte) 70;
    numArray7[27] = (byte) 76;
    numArray7[11] = (byte) 87;
    numArray7[31 /*0x1F*/] = (byte) 186;
    numArray7[13] = (byte) 95;
    numArray7[34] = (byte) 5;
    numArray7[28] = (byte) 121;
    numArray7[16 /*0x10*/] = (byte) 102;
    numArray7[15] = (byte) 143;
    numArray7[18] = (byte) 99;
    numArray7[45] = (byte) 177;
    numArray7[14] = (byte) 174;
    numArray7[20] = (byte) 98;
    numArray7[23] = (byte) 33;
    numArray7[30] = (byte) 232;
    numArray7[24] = (byte) 69;
    numArray7[8] = (byte) 173;
    numArray7[26] = (byte) 139;
    numArray7[40] = (byte) 145;
    numArray7[17] = (byte) 216;
    numArray7[29] = (byte) 106;
    numArray7[0] = (byte) 185;
    numArray7[4] = (byte) 234;
    numArray7[32 /*0x20*/] = (byte) 136;
    numArray7[54] = (byte) 69;
    numArray7[12] = (byte) 234;
    numArray7[33] = (byte) 197;
    numArray7[36] = (byte) 158;
    numArray7[19] = (byte) 105;
    numArray7[22] = (byte) 152;
    numArray7[39] = (byte) 117;
    numArray7[41] = (byte) 164;
    numArray7[44] = (byte) 135;
    numArray7[53] = (byte) 191;
    numArray7[43] = (byte) 164;
    numArray7[7] = (byte) 131;
    numArray7[1] = (byte) 5;
    numArray7[51] = (byte) 5;
    numArray7[47] = (byte) 115;
    numArray7[48 /*0x30*/] = (byte) 213;
    numArray7[49] = (byte) 210;
    numArray7[50] = (byte) 50;
    numArray7[6] = (byte) 241;
    numArray7[52] = (byte) 146;
    numArray7[10] = (byte) 229;
    numArray7[21] = (byte) 85;
    byte[] numArray8 = new byte[55];
    numArray8[19] = (byte) 162;
    numArray8[41] = (byte) 218;
    numArray8[2] = (byte) 136;
    numArray8[3] = (byte) 192 /*0xC0*/;
    numArray8[1] = (byte) 140;
    numArray8[6] = (byte) 29;
    numArray8[8] = (byte) 76;
    numArray8[51] = (byte) 67;
    numArray8[4] = (byte) 91;
    numArray8[35] = (byte) 218;
    numArray8[10] = (byte) 6;
    numArray8[11] = (byte) 161;
    numArray8[40] = (byte) 38;
    numArray8[47] = (byte) 88;
    numArray8[31 /*0x1F*/] = (byte) 199;
    numArray8[49] = (byte) 56;
    numArray8[15] = (byte) 124;
    numArray8[23] = (byte) 213;
    numArray8[18] = (byte) 126;
    numArray8[21] = (byte) 122;
    numArray8[32 /*0x20*/] = (byte) 231;
    numArray8[52] = (byte) 92;
    numArray8[44] = (byte) 68;
    numArray8[37] = (byte) 122;
    numArray8[14] = (byte) 99;
    numArray8[25] = (byte) 97;
    numArray8[26] = (byte) 251;
    numArray8[27] = (byte) 217;
    numArray8[28] = (byte) 120;
    numArray8[29] = (byte) 103;
    numArray8[9] = (byte) 183;
    numArray8[45] = (byte) 14;
    numArray8[24] = (byte) 163;
    numArray8[33] = (byte) 216;
    numArray8[12] = (byte) 176 /*0xB0*/;
    numArray8[34] = (byte) 208 /*0xD0*/;
    numArray8[16 /*0x10*/] = (byte) 47;
    numArray8[48 /*0x30*/] = (byte) 5;
    numArray8[38] = (byte) 193;
    numArray8[5] = (byte) 96 /*0x60*/;
    numArray8[36] = (byte) 29;
    numArray8[0] = (byte) 1;
    numArray8[42] = (byte) 155;
    numArray8[13] = (byte) 51;
    numArray8[22] = (byte) 73;
    numArray8[54] = (byte) 15;
    numArray8[46] = (byte) 198;
    numArray8[17] = (byte) 239;
    numArray8[43] = (byte) 73;
    numArray8[7] = (byte) 186;
    numArray8[50] = (byte) 227;
    numArray8[30] = (byte) 120;
    numArray8[20] = (byte) 62;
    numArray8[53] = (byte) 254;
    numArray8[39] = (byte) 30;
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[45]
    {
      (byte) 69,
      (byte) 249,
      (byte) 119,
      (byte) 104,
      (byte) 2,
      (byte) 155,
      (byte) 178,
      (byte) 163,
      (byte) 125,
      (byte) 36,
      (byte) 169,
      (byte) 101,
      (byte) 141,
      (byte) 112 /*0x70*/,
      (byte) 83,
      (byte) 28,
      (byte) 105,
      (byte) 44,
      (byte) 236,
      (byte) 42,
      (byte) 226,
      (byte) 225,
      (byte) 211,
      (byte) 240 /*0xF0*/,
      (byte) 160 /*0xA0*/,
      (byte) 171,
      (byte) 86,
      (byte) 246,
      (byte) 172,
      (byte) 215,
      (byte) 230,
      (byte) 150,
      (byte) 34,
      (byte) 117,
      (byte) 185,
      (byte) 48 /*0x30*/,
      (byte) 76,
      (byte) 38,
      (byte) 188,
      (byte) 155,
      (byte) 62,
      (byte) 40,
      (byte) 221,
      (byte) 184,
      (byte) 67
    };
    byte[] numArray10 = new byte[45];
    numArray10[35] = (byte) 136;
    numArray10[24] = (byte) 45;
    numArray10[6] = (byte) 238;
    numArray10[34] = (byte) 218;
    numArray10[26] = (byte) 110;
    numArray10[30] = (byte) 235;
    numArray10[33] = (byte) 184;
    numArray10[0] = (byte) 25;
    numArray10[3] = (byte) 226;
    numArray10[15] = (byte) 168;
    numArray10[10] = (byte) 8;
    numArray10[11] = (byte) 104;
    numArray10[8] = (byte) 225;
    numArray10[36] = (byte) 91;
    numArray10[14] = (byte) 122;
    numArray10[40] = (byte) 102;
    numArray10[21] = (byte) 26;
    numArray10[17] = (byte) 21;
    numArray10[18] = (byte) 116;
    numArray10[19] = (byte) 209;
    numArray10[20] = (byte) 236;
    numArray10[25] = (byte) 195;
    numArray10[22] = (byte) 142;
    numArray10[12] = (byte) 13;
    numArray10[5] = (byte) 66;
    numArray10[4] = (byte) 116;
    numArray10[2] = (byte) 198;
    numArray10[27] = (byte) 54;
    numArray10[23] = (byte) 251;
    numArray10[29] = (byte) 88;
    numArray10[16 /*0x10*/] = (byte) 76;
    numArray10[31 /*0x1F*/] = (byte) 111;
    numArray10[32 /*0x20*/] = (byte) 212;
    numArray10[7] = (byte) 91;
    numArray10[13] = (byte) 180;
    numArray10[28] = (byte) 148;
    numArray10[44] = (byte) 203;
    numArray10[37] = (byte) 161;
    numArray10[38] = (byte) 3;
    numArray10[39] = (byte) 30;
    numArray10[1] = (byte) 50;
    numArray10[41] = (byte) 139;
    numArray10[42] = (byte) 163;
    numArray10[43] = (byte) 194;
    numArray10[9] = (byte) 156;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 45);
    for (int index = 0; index < 45; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static int ssp_appserver_13561(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[5] = (byte) 183;
    sourceArray1[1] = (byte) 49;
    sourceArray1[2] = (byte) 55;
    sourceArray1[4] = (byte) 48 /*0x30*/;
    sourceArray1[16 /*0x10*/] = (byte) 11;
    sourceArray1[25] = (byte) 242;
    sourceArray1[27] = (byte) 140;
    sourceArray1[21] = (byte) 212;
    sourceArray1[6] = (byte) 216;
    sourceArray1[0] = (byte) 130;
    sourceArray1[10] = byte.MaxValue;
    sourceArray1[19] = (byte) 155;
    sourceArray1[12] = (byte) 37;
    sourceArray1[13] = (byte) 25;
    sourceArray1[14] = (byte) 47;
    sourceArray1[43] = (byte) 94;
    sourceArray1[33] = (byte) 250;
    sourceArray1[17] = (byte) 218;
    sourceArray1[20] = (byte) 239;
    sourceArray1[28] = (byte) 96 /*0x60*/;
    sourceArray1[15] = (byte) 95;
    sourceArray1[18] = (byte) 33;
    sourceArray1[46] = (byte) 250;
    sourceArray1[23] = (byte) 17;
    sourceArray1[24] = (byte) 36;
    sourceArray1[31 /*0x1F*/] = (byte) 229;
    sourceArray1[8] = (byte) 236;
    sourceArray1[7] = (byte) 73;
    sourceArray1[34] = (byte) 106;
    sourceArray1[29] = (byte) 23;
    sourceArray1[30] = (byte) 9;
    sourceArray1[3] = (byte) 123;
    sourceArray1[9] = (byte) 63 /*0x3F*/;
    sourceArray1[41] = (byte) 12;
    sourceArray1[45] = (byte) 187;
    sourceArray1[35] = (byte) 136;
    sourceArray1[36] = (byte) 204;
    sourceArray1[37] = (byte) 159;
    sourceArray1[11] = (byte) 93;
    sourceArray1[39] = (byte) 238;
    sourceArray1[40] = (byte) 226;
    sourceArray1[47] = (byte) 2;
    sourceArray1[42] = (byte) 251;
    sourceArray1[22] = (byte) 88;
    sourceArray1[44] = (byte) 148;
    sourceArray1[38] = (byte) 123;
    sourceArray1[32 /*0x20*/] = (byte) 11;
    sourceArray1[26] = (byte) 37;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 216,
      (byte) 159,
      (byte) 208 /*0xD0*/,
      (byte) 76,
      (byte) 146,
      (byte) 85,
      (byte) 81,
      (byte) 252,
      (byte) 116,
      (byte) 247,
      (byte) 186,
      (byte) 207,
      (byte) 252,
      (byte) 67,
      (byte) 213,
      (byte) 218,
      (byte) 41,
      (byte) 182,
      (byte) 243,
      (byte) 243,
      (byte) 51,
      (byte) 119,
      (byte) 12,
      (byte) 144 /*0x90*/,
      (byte) 221,
      (byte) 151,
      (byte) 23,
      (byte) 241,
      (byte) 208 /*0xD0*/,
      (byte) 91,
      (byte) 164,
      (byte) 130,
      (byte) 86,
      (byte) 167,
      (byte) 195,
      (byte) 97,
      (byte) 92,
      (byte) 223,
      (byte) 189,
      (byte) 243,
      (byte) 53,
      (byte) 187,
      (byte) 146,
      (byte) 155,
      (byte) 254,
      (byte) 207,
      (byte) 104,
      (byte) 247
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13562(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 207,
      (byte) 83,
      (byte) 182,
      (byte) 98,
      (byte) 147,
      (byte) 139,
      (byte) 9,
      (byte) 16 /*0x10*/,
      (byte) 131,
      (byte) 211,
      (byte) 222,
      (byte) 169,
      (byte) 236,
      (byte) 70,
      (byte) 110,
      (byte) 29,
      (byte) 125,
      (byte) 188,
      (byte) 23,
      (byte) 114,
      (byte) 15,
      (byte) 151,
      (byte) 241,
      (byte) 246,
      (byte) 237,
      (byte) 34,
      (byte) 34,
      (byte) 215,
      (byte) 34,
      (byte) 191,
      (byte) 179,
      (byte) 210,
      (byte) 1,
      (byte) 0,
      (byte) 40,
      (byte) 208 /*0xD0*/,
      (byte) 120,
      (byte) 8,
      (byte) 132,
      (byte) 146,
      (byte) 159,
      (byte) 50,
      (byte) 16 /*0x10*/,
      (byte) 233,
      (byte) 209,
      (byte) 251,
      (byte) 92,
      (byte) 246
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 209,
      (byte) 43,
      (byte) 252,
      (byte) 129,
      (byte) 231,
      (byte) 114,
      (byte) 226,
      (byte) 77,
      (byte) 10,
      (byte) 211,
      (byte) 105,
      (byte) 73,
      (byte) 227,
      (byte) 144 /*0x90*/,
      (byte) 95,
      (byte) 198,
      (byte) 163,
      (byte) 212,
      (byte) 22,
      (byte) 156,
      (byte) 65,
      (byte) 50,
      (byte) 180,
      (byte) 11,
      (byte) 252,
      (byte) 202,
      (byte) 232,
      (byte) 239,
      (byte) 19,
      (byte) 228,
      (byte) 63 /*0x3F*/,
      (byte) 112 /*0x70*/,
      (byte) 27,
      (byte) 7,
      (byte) 73,
      (byte) 24,
      (byte) 97,
      (byte) 201,
      (byte) 164,
      (byte) 23,
      (byte) 222,
      (byte) 212,
      (byte) 217,
      (byte) 229,
      (byte) 63 /*0x3F*/,
      (byte) 45,
      (byte) 76,
      (byte) 43
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13563(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[5] = (byte) 116;
    sourceArray1[25] = (byte) 36;
    sourceArray1[12] = (byte) 12;
    sourceArray1[3] = (byte) 97;
    sourceArray1[4] = (byte) 191;
    sourceArray1[24] = (byte) 8;
    sourceArray1[6] = (byte) 112 /*0x70*/;
    sourceArray1[0] = (byte) 8;
    sourceArray1[30] = (byte) 169;
    sourceArray1[43] = (byte) 68;
    sourceArray1[10] = (byte) 196;
    sourceArray1[11] = (byte) 32 /*0x20*/;
    sourceArray1[9] = (byte) 237;
    sourceArray1[13] = (byte) 168;
    sourceArray1[14] = (byte) 235;
    sourceArray1[15] = (byte) 30;
    sourceArray1[16 /*0x10*/] = (byte) 158;
    sourceArray1[17] = (byte) 164;
    sourceArray1[38] = (byte) 184;
    sourceArray1[26] = (byte) 242;
    sourceArray1[20] = (byte) 132;
    sourceArray1[21] = (byte) 210;
    sourceArray1[32 /*0x20*/] = (byte) 72;
    sourceArray1[28] = (byte) 44;
    sourceArray1[40] = (byte) 20;
    sourceArray1[1] = (byte) 89;
    sourceArray1[42] = (byte) 109;
    sourceArray1[8] = (byte) 49;
    sourceArray1[27] = (byte) 198;
    sourceArray1[29] = (byte) 234;
    sourceArray1[35] = (byte) 167;
    sourceArray1[23] = (byte) 134;
    sourceArray1[33] = (byte) 193;
    sourceArray1[47] = (byte) 83;
    sourceArray1[34] = (byte) 249;
    sourceArray1[45] = (byte) 200;
    sourceArray1[36] = (byte) 240 /*0xF0*/;
    sourceArray1[37] = (byte) 110;
    sourceArray1[39] = (byte) 147;
    sourceArray1[44] = (byte) 76;
    sourceArray1[7] = (byte) 105;
    sourceArray1[41] = (byte) 41;
    sourceArray1[18] = (byte) 117;
    sourceArray1[19] = (byte) 55;
    sourceArray1[2] = (byte) 55;
    sourceArray1[31 /*0x1F*/] = (byte) 24;
    sourceArray1[46] = (byte) 197;
    sourceArray1[22] = (byte) 221;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[45] = (byte) 104;
    sourceArray2[36] = (byte) 41;
    sourceArray2[3] = (byte) 70;
    sourceArray2[38] = (byte) 135;
    sourceArray2[4] = (byte) 68;
    sourceArray2[5] = (byte) 31 /*0x1F*/;
    sourceArray2[10] = (byte) 248;
    sourceArray2[28] = (byte) 141;
    sourceArray2[2] = (byte) 88;
    sourceArray2[9] = (byte) 151;
    sourceArray2[33] = (byte) 210;
    sourceArray2[44] = (byte) 254;
    sourceArray2[35] = (byte) 153;
    sourceArray2[1] = (byte) 124;
    sourceArray2[14] = (byte) 150;
    sourceArray2[8] = (byte) 59;
    sourceArray2[16 /*0x10*/] = (byte) 48 /*0x30*/;
    sourceArray2[15] = (byte) 182;
    sourceArray2[11] = (byte) 108;
    sourceArray2[24] = (byte) 200;
    sourceArray2[18] = (byte) 83;
    sourceArray2[25] = (byte) 60;
    sourceArray2[22] = (byte) 103;
    sourceArray2[23] = (byte) 82;
    sourceArray2[21] = (byte) 176 /*0xB0*/;
    sourceArray2[20] = (byte) 19;
    sourceArray2[17] = (byte) 8;
    sourceArray2[27] = (byte) 202;
    sourceArray2[6] = (byte) 66;
    sourceArray2[29] = (byte) 84;
    sourceArray2[30] = (byte) 118;
    sourceArray2[31 /*0x1F*/] = (byte) 180;
    sourceArray2[7] = (byte) 10;
    sourceArray2[13] = (byte) 254;
    sourceArray2[19] = (byte) 253;
    sourceArray2[47] = (byte) 80 /*0x50*/;
    sourceArray2[32 /*0x20*/] = (byte) 15;
    sourceArray2[37] = (byte) 140;
    sourceArray2[34] = (byte) 4;
    sourceArray2[39] = (byte) 184;
    sourceArray2[40] = (byte) 223;
    sourceArray2[41] = (byte) 234;
    sourceArray2[42] = (byte) 102;
    sourceArray2[43] = (byte) 245;
    sourceArray2[12] = (byte) 106;
    sourceArray2[46] = (byte) 137;
    sourceArray2[0] = (byte) 132;
    sourceArray2[26] = (byte) 149;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_13564()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[9];
      byte[] numArray2 = new byte[9];
      numArray2[8] = (byte) 101;
      numArray2[4] = (byte) 123;
      numArray2[2] = (byte) 20;
      numArray2[6] = (byte) 221;
      numArray2[3] = (byte) 3;
      numArray2[5] = (byte) 145;
      numArray2[0] = (byte) 226;
      numArray2[7] = (byte) 46;
      numArray2[1] = (byte) 44;
      byte[] numArray3 = new byte[9];
      numArray3[0] = (byte) 28;
      numArray3[3] = (byte) 164;
      numArray3[2] = (byte) 228;
      numArray3[4] = (byte) 172;
      numArray3[1] = (byte) 119;
      numArray3[5] = (byte) 57;
      numArray3[6] = (byte) 57;
      numArray3[7] = (byte) 71;
      numArray3[8] = (byte) 155;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 9);
      for (int index = 0; index < 9; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[9];
    byte[] numArray5 = new byte[9]
    {
      (byte) 248,
      (byte) 210,
      (byte) 37,
      (byte) 28,
      (byte) 234,
      (byte) 164,
      (byte) 165,
      (byte) 107,
      (byte) 194
    };
    byte[] numArray6 = new byte[9];
    numArray6[2] = (byte) 150;
    numArray6[3] = (byte) 39;
    numArray6[0] = (byte) 20;
    numArray6[1] = (byte) 51;
    numArray6[4] = (byte) 213;
    numArray6[6] = (byte) 5;
    numArray6[5] = (byte) 106;
    numArray6[7] = (byte) 106;
    numArray6[8] = (byte) 187;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 9);
    for (int index = 0; index < 9; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static int ssp_appserver_13565(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[22] = (byte) 170;
    sourceArray1[1] = (byte) 49;
    sourceArray1[2] = (byte) 139;
    sourceArray1[32 /*0x20*/] = (byte) 151;
    sourceArray1[19] = (byte) 207;
    sourceArray1[37] = (byte) 98;
    sourceArray1[40] = (byte) 117;
    sourceArray1[7] = (byte) 14;
    sourceArray1[8] = (byte) 180;
    sourceArray1[9] = (byte) 112 /*0x70*/;
    sourceArray1[10] = (byte) 85;
    sourceArray1[13] = (byte) 150;
    sourceArray1[39] = (byte) 166;
    sourceArray1[44] = (byte) 41;
    sourceArray1[14] = (byte) 232;
    sourceArray1[15] = (byte) 48 /*0x30*/;
    sourceArray1[4] = (byte) 232;
    sourceArray1[17] = (byte) 177;
    sourceArray1[3] = (byte) 66;
    sourceArray1[34] = (byte) 57;
    sourceArray1[20] = (byte) 70;
    sourceArray1[21] = (byte) 33;
    sourceArray1[16 /*0x10*/] = (byte) 215;
    sourceArray1[23] = (byte) 216;
    sourceArray1[42] = (byte) 7;
    sourceArray1[24] = (byte) 170;
    sourceArray1[26] = (byte) 78;
    sourceArray1[27] = (byte) 183;
    sourceArray1[28] = (byte) 179;
    sourceArray1[12] = (byte) 64 /*0x40*/;
    sourceArray1[5] = (byte) 197;
    sourceArray1[31 /*0x1F*/] = (byte) 204;
    sourceArray1[41] = (byte) 246;
    sourceArray1[11] = (byte) 193;
    sourceArray1[6] = (byte) 77;
    sourceArray1[29] = (byte) 11;
    sourceArray1[36] = (byte) 95;
    sourceArray1[47] = (byte) 89;
    sourceArray1[35] = (byte) 235;
    sourceArray1[33] = (byte) 215;
    sourceArray1[25] = (byte) 21;
    sourceArray1[30] = (byte) 16 /*0x10*/;
    sourceArray1[18] = (byte) 84;
    sourceArray1[43] = (byte) 176 /*0xB0*/;
    sourceArray1[46] = (byte) 213;
    sourceArray1[45] = (byte) 125;
    sourceArray1[0] = (byte) 236;
    sourceArray1[38] = (byte) 189;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[43] = (byte) 226;
    sourceArray2[1] = (byte) 14;
    sourceArray2[15] = (byte) 102;
    sourceArray2[3] = (byte) 147;
    sourceArray2[4] = (byte) 248;
    sourceArray2[5] = (byte) 91;
    sourceArray2[25] = (byte) 117;
    sourceArray2[7] = (byte) 12;
    sourceArray2[8] = (byte) 97;
    sourceArray2[9] = (byte) 117;
    sourceArray2[14] = (byte) 29;
    sourceArray2[11] = (byte) 178;
    sourceArray2[13] = (byte) 243;
    sourceArray2[46] = (byte) 118;
    sourceArray2[31 /*0x1F*/] = (byte) 161;
    sourceArray2[10] = (byte) 149;
    sourceArray2[16 /*0x10*/] = (byte) 138;
    sourceArray2[17] = (byte) 230;
    sourceArray2[28] = (byte) 170;
    sourceArray2[45] = (byte) 178;
    sourceArray2[20] = (byte) 137;
    sourceArray2[24] = (byte) 161;
    sourceArray2[36] = (byte) 190;
    sourceArray2[23] = (byte) 228;
    sourceArray2[38] = (byte) 195;
    sourceArray2[0] = (byte) 126;
    sourceArray2[26] = (byte) 99;
    sourceArray2[35] = (byte) 23;
    sourceArray2[32 /*0x20*/] = (byte) 89;
    sourceArray2[40] = (byte) 79;
    sourceArray2[30] = (byte) 22;
    sourceArray2[41] = (byte) 160 /*0xA0*/;
    sourceArray2[18] = (byte) 203;
    sourceArray2[33] = (byte) 156;
    sourceArray2[34] = (byte) 106;
    sourceArray2[47] = (byte) 189;
    sourceArray2[19] = (byte) 80 /*0x50*/;
    sourceArray2[37] = (byte) 149;
    sourceArray2[2] = (byte) 157;
    sourceArray2[12] = (byte) 220;
    sourceArray2[27] = (byte) 38;
    sourceArray2[6] = (byte) 174;
    sourceArray2[42] = (byte) 81;
    sourceArray2[22] = (byte) 56;
    sourceArray2[44] = (byte) 52;
    sourceArray2[39] = (byte) 177;
    sourceArray2[21] = (byte) 135;
    sourceArray2[29] = (byte) 36;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[39];
    byte[] response2 = new byte[39];
    Array.Copy((Array) sc_13556.sspq, 0, (Array) numArray2, 0, 39);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13556.sspr, 0, (Array) numArray2, 0, 39);
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

  internal static string ssp_appserver_13566()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[9];
      byte[] numArray2 = new byte[9]
      {
        (byte) 28,
        (byte) 169,
        (byte) 171,
        (byte) 235,
        (byte) 158,
        (byte) 156,
        (byte) 209,
        (byte) 17,
        (byte) 218
      };
      byte[] numArray3 = new byte[9]
      {
        (byte) 187,
        (byte) 105,
        (byte) 55,
        (byte) 215,
        (byte) 115,
        (byte) 187,
        (byte) 201,
        (byte) 39,
        (byte) 18
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 9);
      for (int index = 0; index < 9; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[9];
    byte[] numArray5 = new byte[9];
    numArray5[1] = (byte) 182;
    numArray5[5] = (byte) 156;
    numArray5[4] = (byte) 38;
    numArray5[3] = (byte) 152;
    numArray5[2] = (byte) 206;
    numArray5[8] = (byte) 199;
    numArray5[6] = (byte) 82;
    numArray5[7] = (byte) 145;
    numArray5[0] = (byte) 245;
    byte[] numArray6 = new byte[9];
    numArray6[5] = (byte) 202;
    numArray6[1] = (byte) 200;
    numArray6[8] = (byte) 48 /*0x30*/;
    numArray6[7] = (byte) 77;
    numArray6[4] = (byte) 3;
    numArray6[2] = (byte) 178;
    numArray6[6] = (byte) 240 /*0xF0*/;
    numArray6[0] = (byte) 252;
    numArray6[3] = (byte) 37;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 9);
    for (int index = 0; index < 9; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static int ssp_appserver_13567(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 117,
      (byte) 108,
      (byte) 203,
      (byte) 195,
      (byte) 125,
      (byte) 26,
      (byte) 169,
      (byte) 106,
      (byte) 12,
      (byte) 172,
      (byte) 221,
      (byte) 56,
      (byte) 205,
      (byte) 9,
      (byte) 15,
      (byte) 103,
      (byte) 109,
      (byte) 248,
      (byte) 198,
      (byte) 232,
      (byte) 92,
      (byte) 192 /*0xC0*/,
      (byte) 11,
      (byte) 184,
      (byte) 223,
      (byte) 229,
      (byte) 131,
      (byte) 136,
      (byte) 101,
      (byte) 237,
      (byte) 119,
      (byte) 155,
      (byte) 174,
      (byte) 154,
      (byte) 44,
      (byte) 110,
      (byte) 107,
      (byte) 107,
      (byte) 91,
      (byte) 48 /*0x30*/,
      (byte) 72,
      (byte) 75,
      (byte) 66,
      (byte) 12,
      (byte) 95,
      (byte) 158,
      (byte) 27,
      (byte) 11
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 22,
      (byte) 124,
      (byte) 234,
      (byte) 227,
      (byte) 254,
      (byte) 156,
      (byte) 23,
      (byte) 111,
      (byte) 125,
      (byte) 82,
      (byte) 15,
      (byte) 204,
      (byte) 156,
      (byte) 66,
      (byte) 202,
      (byte) 124,
      (byte) 86,
      (byte) 158,
      (byte) 118,
      (byte) 157,
      (byte) 43,
      (byte) 199,
      (byte) 45,
      (byte) 218,
      (byte) 173,
      (byte) 56,
      (byte) 1,
      (byte) 83,
      (byte) 39,
      (byte) 159,
      (byte) 109,
      (byte) 30,
      (byte) 63 /*0x3F*/,
      (byte) 213,
      (byte) 227,
      (byte) 22,
      (byte) 64 /*0x40*/,
      (byte) 254,
      (byte) 221,
      (byte) 96 /*0x60*/,
      (byte) 216,
      (byte) 96 /*0x60*/,
      (byte) 214,
      (byte) 144 /*0x90*/,
      (byte) 2,
      (byte) 106,
      (byte) 35,
      (byte) 233
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13568(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[31 /*0x1F*/] = (byte) 245;
    sourceArray1[35] = (byte) 180;
    sourceArray1[5] = (byte) 227;
    sourceArray1[19] = (byte) 157;
    sourceArray1[20] = (byte) 150;
    sourceArray1[15] = (byte) 220;
    sourceArray1[4] = (byte) 18;
    sourceArray1[7] = (byte) 150;
    sourceArray1[0] = (byte) 77;
    sourceArray1[17] = (byte) 251;
    sourceArray1[33] = (byte) 54;
    sourceArray1[11] = (byte) 52;
    sourceArray1[12] = (byte) 65;
    sourceArray1[13] = (byte) 137;
    sourceArray1[41] = (byte) 153;
    sourceArray1[23] = (byte) 125;
    sourceArray1[16 /*0x10*/] = (byte) 160 /*0xA0*/;
    sourceArray1[2] = (byte) 180;
    sourceArray1[14] = (byte) 51;
    sourceArray1[6] = (byte) 151;
    sourceArray1[44] = (byte) 35;
    sourceArray1[3] = (byte) 96 /*0x60*/;
    sourceArray1[22] = (byte) 16 /*0x10*/;
    sourceArray1[10] = (byte) 31 /*0x1F*/;
    sourceArray1[24] = byte.MaxValue;
    sourceArray1[25] = (byte) 88;
    sourceArray1[36] = (byte) 119;
    sourceArray1[8] = (byte) 33;
    sourceArray1[28] = (byte) 119;
    sourceArray1[9] = (byte) 4;
    sourceArray1[30] = (byte) 150;
    sourceArray1[42] = (byte) 39;
    sourceArray1[38] = (byte) 89;
    sourceArray1[29] = (byte) 250;
    sourceArray1[34] = (byte) 97;
    sourceArray1[27] = (byte) 202;
    sourceArray1[18] = (byte) 1;
    sourceArray1[26] = (byte) 65;
    sourceArray1[37] = (byte) 14;
    sourceArray1[1] = (byte) 229;
    sourceArray1[45] = (byte) 216;
    sourceArray1[21] = (byte) 159;
    sourceArray1[32 /*0x20*/] = (byte) 86;
    sourceArray1[43] = (byte) 71;
    sourceArray1[40] = (byte) 195;
    sourceArray1[39] = (byte) 135;
    sourceArray1[46] = (byte) 30;
    sourceArray1[47] = (byte) 20;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[34] = (byte) 95;
    sourceArray2[21] = (byte) 44;
    sourceArray2[47] = (byte) 72;
    sourceArray2[29] = (byte) 145;
    sourceArray2[17] = (byte) 215;
    sourceArray2[5] = (byte) 75;
    sourceArray2[1] = (byte) 58;
    sourceArray2[6] = (byte) 185;
    sourceArray2[11] = (byte) 75;
    sourceArray2[9] = (byte) 12;
    sourceArray2[10] = (byte) 238;
    sourceArray2[40] = (byte) 168;
    sourceArray2[35] = (byte) 45;
    sourceArray2[18] = (byte) 136;
    sourceArray2[14] = (byte) 174;
    sourceArray2[4] = (byte) 72;
    sourceArray2[16 /*0x10*/] = (byte) 218;
    sourceArray2[30] = (byte) 216;
    sourceArray2[0] = (byte) 212;
    sourceArray2[19] = (byte) 154;
    sourceArray2[24] = (byte) 52;
    sourceArray2[20] = (byte) 176 /*0xB0*/;
    sourceArray2[22] = (byte) 98;
    sourceArray2[23] = (byte) 83;
    sourceArray2[13] = (byte) 198;
    sourceArray2[15] = (byte) 169;
    sourceArray2[26] = (byte) 66;
    sourceArray2[39] = (byte) 251;
    sourceArray2[28] = (byte) 227;
    sourceArray2[44] = (byte) 206;
    sourceArray2[38] = (byte) 250;
    sourceArray2[31 /*0x1F*/] = (byte) 112 /*0x70*/;
    sourceArray2[32 /*0x20*/] = (byte) 75;
    sourceArray2[33] = (byte) 192 /*0xC0*/;
    sourceArray2[8] = (byte) 246;
    sourceArray2[3] = (byte) 134;
    sourceArray2[36] = (byte) 118;
    sourceArray2[37] = (byte) 88;
    sourceArray2[25] = (byte) 70;
    sourceArray2[12] = (byte) 95;
    sourceArray2[7] = (byte) 250;
    sourceArray2[41] = (byte) 153;
    sourceArray2[42] = (byte) 151;
    sourceArray2[43] = (byte) 147;
    sourceArray2[27] = (byte) 218;
    sourceArray2[45] = (byte) 239;
    sourceArray2[46] = (byte) 26;
    sourceArray2[2] = (byte) 198;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13569(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 110,
      (byte) 12,
      (byte) 163,
      (byte) 231,
      (byte) 45,
      (byte) 206,
      (byte) 238,
      (byte) 110,
      (byte) 118,
      (byte) 200,
      (byte) 166,
      (byte) 116,
      (byte) 46,
      (byte) 230,
      (byte) 200,
      (byte) 81,
      (byte) 190,
      (byte) 29,
      (byte) 123,
      (byte) 14,
      (byte) 86,
      (byte) 54,
      (byte) 111,
      (byte) 88,
      (byte) 103,
      (byte) 26,
      (byte) 168,
      (byte) 227,
      (byte) 16 /*0x10*/,
      (byte) 247,
      (byte) 207,
      (byte) 191,
      (byte) 116,
      (byte) 226,
      (byte) 39,
      (byte) 213,
      (byte) 221,
      (byte) 143,
      (byte) 193,
      (byte) 146,
      (byte) 61,
      (byte) 185,
      (byte) 201,
      (byte) 178,
      (byte) 215,
      (byte) 61,
      (byte) 98,
      (byte) 155
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[29] = (byte) 152;
    sourceArray2[1] = (byte) 136;
    sourceArray2[2] = (byte) 175;
    sourceArray2[3] = (byte) 82;
    sourceArray2[4] = (byte) 142;
    sourceArray2[31 /*0x1F*/] = (byte) 166;
    sourceArray2[6] = (byte) 152;
    sourceArray2[12] = (byte) 19;
    sourceArray2[18] = (byte) 90;
    sourceArray2[23] = (byte) 186;
    sourceArray2[24] = (byte) 97;
    sourceArray2[7] = (byte) 246;
    sourceArray2[33] = (byte) 249;
    sourceArray2[41] = (byte) 178;
    sourceArray2[14] = (byte) 105;
    sourceArray2[30] = (byte) 32 /*0x20*/;
    sourceArray2[28] = (byte) 237;
    sourceArray2[16 /*0x10*/] = (byte) 102;
    sourceArray2[19] = (byte) 19;
    sourceArray2[9] = (byte) 3;
    sourceArray2[17] = (byte) 230;
    sourceArray2[21] = (byte) 151;
    sourceArray2[22] = (byte) 234;
    sourceArray2[8] = (byte) 231;
    sourceArray2[27] = (byte) 30;
    sourceArray2[25] = (byte) 137;
    sourceArray2[40] = (byte) 199;
    sourceArray2[20] = (byte) 241;
    sourceArray2[44] = (byte) 5;
    sourceArray2[43] = (byte) 111;
    sourceArray2[5] = (byte) 131;
    sourceArray2[47] = (byte) 221;
    sourceArray2[15] = (byte) 11;
    sourceArray2[34] = (byte) 210;
    sourceArray2[32 /*0x20*/] = (byte) 107;
    sourceArray2[35] = (byte) 70;
    sourceArray2[36] = (byte) 12;
    sourceArray2[37] = (byte) 95;
    sourceArray2[38] = (byte) 236;
    sourceArray2[39] = (byte) 90;
    sourceArray2[11] = (byte) 141;
    sourceArray2[13] = (byte) 74;
    sourceArray2[42] = (byte) 59;
    sourceArray2[26] = (byte) 81;
    sourceArray2[0] = (byte) 216;
    sourceArray2[45] = (byte) 200;
    sourceArray2[46] = (byte) 67;
    sourceArray2[10] = (byte) 90;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_13570()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[24];
      byte[] numArray2 = new byte[24];
      numArray2[0] = (byte) 61;
      numArray2[20] = (byte) 14;
      numArray2[2] = (byte) 219;
      numArray2[3] = (byte) 108;
      numArray2[4] = (byte) 207;
      numArray2[21] = (byte) 238;
      numArray2[5] = (byte) 144 /*0x90*/;
      numArray2[6] = (byte) 86;
      numArray2[8] = (byte) 147;
      numArray2[19] = (byte) 220;
      numArray2[10] = (byte) 202;
      numArray2[11] = (byte) 223;
      numArray2[12] = (byte) 202;
      numArray2[15] = (byte) 25;
      numArray2[18] = (byte) 70;
      numArray2[23] = (byte) 208 /*0xD0*/;
      numArray2[16 /*0x10*/] = (byte) 135;
      numArray2[17] = (byte) 144 /*0x90*/;
      numArray2[9] = (byte) 44;
      numArray2[13] = (byte) 86;
      numArray2[7] = (byte) 254;
      numArray2[14] = (byte) 91;
      numArray2[22] = (byte) 51;
      numArray2[1] = (byte) 22;
      byte[] numArray3 = new byte[24]
      {
        (byte) 68,
        (byte) 235,
        (byte) 78,
        (byte) 133,
        (byte) 173,
        (byte) 211,
        (byte) 243,
        (byte) 222,
        (byte) 209,
        (byte) 201,
        (byte) 210,
        (byte) 217,
        (byte) 116,
        (byte) 102,
        (byte) 222,
        (byte) 137,
        (byte) 205,
        (byte) 221,
        (byte) 108,
        (byte) 207,
        (byte) 192 /*0xC0*/,
        (byte) 14,
        (byte) 174,
        (byte) 190
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 24);
      for (int index = 0; index < 24; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[24];
    byte[] numArray5 = new byte[24];
    numArray5[9] = (byte) 112 /*0x70*/;
    numArray5[3] = (byte) 226;
    numArray5[20] = (byte) 112 /*0x70*/;
    numArray5[17] = (byte) 105;
    numArray5[4] = (byte) 79;
    numArray5[5] = (byte) 226;
    numArray5[12] = (byte) 245;
    numArray5[23] = (byte) 23;
    numArray5[8] = (byte) 238;
    numArray5[18] = (byte) 185;
    numArray5[6] = (byte) 11;
    numArray5[19] = (byte) 232;
    numArray5[2] = (byte) 220;
    numArray5[11] = (byte) 128 /*0x80*/;
    numArray5[10] = (byte) 168;
    numArray5[15] = (byte) 219;
    numArray5[16 /*0x10*/] = (byte) 59;
    numArray5[13] = (byte) 234;
    numArray5[1] = (byte) 4;
    numArray5[14] = (byte) 168;
    numArray5[7] = (byte) 177;
    numArray5[21] = (byte) 145;
    numArray5[22] = (byte) 157;
    numArray5[0] = (byte) 203;
    byte[] numArray6 = new byte[24]
    {
      (byte) 59,
      (byte) 201,
      (byte) 112 /*0x70*/,
      (byte) 125,
      (byte) 175,
      (byte) 211,
      (byte) 249,
      (byte) 173,
      (byte) 4,
      (byte) 160 /*0xA0*/,
      (byte) 74,
      (byte) 115,
      (byte) 1,
      (byte) 195,
      (byte) 132,
      (byte) 68,
      (byte) 45,
      (byte) 44,
      (byte) 191,
      (byte) 197,
      (byte) 143,
      (byte) 189,
      (byte) 70,
      (byte) 182
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 24);
    for (int index = 0; index < 24; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static int ssp_appserver_13571(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[12] = (byte) 76;
    sourceArray1[31 /*0x1F*/] = (byte) 139;
    sourceArray1[13] = (byte) 84;
    sourceArray1[30] = (byte) 227;
    sourceArray1[4] = (byte) 196;
    sourceArray1[46] = (byte) 208 /*0xD0*/;
    sourceArray1[6] = (byte) 223;
    sourceArray1[7] = (byte) 155;
    sourceArray1[8] = (byte) 96 /*0x60*/;
    sourceArray1[9] = (byte) 80 /*0x50*/;
    sourceArray1[36] = (byte) 60;
    sourceArray1[45] = (byte) 45;
    sourceArray1[3] = (byte) 102;
    sourceArray1[24] = (byte) 102;
    sourceArray1[34] = (byte) 194;
    sourceArray1[15] = (byte) 117;
    sourceArray1[39] = byte.MaxValue;
    sourceArray1[19] = (byte) 203;
    sourceArray1[18] = (byte) 142;
    sourceArray1[11] = (byte) 10;
    sourceArray1[20] = (byte) 249;
    sourceArray1[21] = (byte) 195;
    sourceArray1[38] = (byte) 98;
    sourceArray1[23] = (byte) 130;
    sourceArray1[44] = (byte) 58;
    sourceArray1[25] = (byte) 251;
    sourceArray1[26] = (byte) 221;
    sourceArray1[27] = (byte) 229;
    sourceArray1[2] = (byte) 57;
    sourceArray1[29] = (byte) 46;
    sourceArray1[10] = (byte) 213;
    sourceArray1[0] = (byte) 105;
    sourceArray1[32 /*0x20*/] = (byte) 22;
    sourceArray1[33] = (byte) 194;
    sourceArray1[35] = (byte) 184;
    sourceArray1[28] = (byte) 69;
    sourceArray1[22] = (byte) 10;
    sourceArray1[37] = (byte) 182;
    sourceArray1[41] = (byte) 35;
    sourceArray1[16 /*0x10*/] = (byte) 139;
    sourceArray1[17] = (byte) 179;
    sourceArray1[40] = (byte) 189;
    sourceArray1[42] = (byte) 196;
    sourceArray1[43] = (byte) 21;
    sourceArray1[5] = (byte) 173;
    sourceArray1[14] = (byte) 1;
    sourceArray1[1] = (byte) 58;
    sourceArray1[47] = (byte) 3;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 196,
      (byte) 129,
      (byte) 130,
      (byte) 79,
      (byte) 225,
      (byte) 68,
      (byte) 146,
      (byte) 134,
      (byte) 145,
      (byte) 147,
      (byte) 208 /*0xD0*/,
      (byte) 192 /*0xC0*/,
      (byte) 122,
      (byte) 6,
      (byte) 251,
      (byte) 29,
      (byte) 217,
      (byte) 220,
      (byte) 42,
      (byte) 98,
      (byte) 148,
      (byte) 2,
      (byte) 43,
      (byte) 153,
      (byte) 229,
      (byte) 130,
      (byte) 244,
      (byte) 168,
      (byte) 139,
      (byte) 219,
      (byte) 75,
      (byte) 167,
      (byte) 78,
      (byte) 30,
      (byte) 63 /*0x3F*/,
      (byte) 6,
      (byte) 124,
      (byte) 181,
      (byte) 60,
      (byte) 116,
      (byte) 126,
      (byte) 253,
      (byte) 147,
      (byte) 50,
      (byte) 68,
      (byte) 124,
      (byte) 231,
      (byte) 20
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[22];
    byte[] response2 = new byte[22];
    Array.Copy((Array) sc_13556.sspq, 39, (Array) numArray2, 0, 22);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13556.sspr, 39, (Array) numArray2, 0, 22);
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

  internal static int ssp_appserver_13572(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 192 /*0xC0*/,
      (byte) 180,
      (byte) 30,
      (byte) 185,
      (byte) 95,
      (byte) 74,
      (byte) 8,
      (byte) 108,
      (byte) 75,
      (byte) 87,
      (byte) 254,
      (byte) 228,
      (byte) 105,
      (byte) 31 /*0x1F*/,
      (byte) 100,
      (byte) 33,
      (byte) 160 /*0xA0*/,
      (byte) 30,
      (byte) 29,
      (byte) 88,
      (byte) 117,
      (byte) 76,
      (byte) 21,
      (byte) 238,
      (byte) 249,
      (byte) 58,
      (byte) 39,
      (byte) 46,
      (byte) 158,
      (byte) 85,
      (byte) 51,
      (byte) 217,
      (byte) 221,
      (byte) 251,
      (byte) 126,
      (byte) 159,
      (byte) 253,
      (byte) 190,
      (byte) 15,
      (byte) 43,
      (byte) 193,
      (byte) 58,
      (byte) 240 /*0xF0*/,
      (byte) 23,
      (byte) 200,
      (byte) 186,
      (byte) 140,
      (byte) 124
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[36] = (byte) 46;
    sourceArray2[1] = (byte) 156;
    sourceArray2[26] = (byte) 159;
    sourceArray2[20] = (byte) 113;
    sourceArray2[4] = (byte) 167;
    sourceArray2[5] = (byte) 216;
    sourceArray2[2] = (byte) 234;
    sourceArray2[7] = (byte) 101;
    sourceArray2[9] = (byte) 188;
    sourceArray2[46] = (byte) 50;
    sourceArray2[10] = (byte) 93;
    sourceArray2[15] = (byte) 80 /*0x50*/;
    sourceArray2[17] = (byte) 54;
    sourceArray2[31 /*0x1F*/] = (byte) 219;
    sourceArray2[12] = (byte) 214;
    sourceArray2[22] = (byte) 123;
    sourceArray2[16 /*0x10*/] = (byte) 6;
    sourceArray2[40] = (byte) 213;
    sourceArray2[18] = (byte) 218;
    sourceArray2[21] = (byte) 19;
    sourceArray2[43] = (byte) 190;
    sourceArray2[14] = (byte) 83;
    sourceArray2[19] = (byte) 172;
    sourceArray2[23] = (byte) 116;
    sourceArray2[28] = (byte) 179;
    sourceArray2[25] = (byte) 57;
    sourceArray2[6] = (byte) 187;
    sourceArray2[27] = (byte) 201;
    sourceArray2[13] = (byte) 241;
    sourceArray2[11] = (byte) 190;
    sourceArray2[30] = (byte) 88;
    sourceArray2[41] = (byte) 103;
    sourceArray2[39] = (byte) 121;
    sourceArray2[33] = (byte) 94;
    sourceArray2[34] = (byte) 254;
    sourceArray2[35] = (byte) 35;
    sourceArray2[24] = (byte) 132;
    sourceArray2[37] = (byte) 0;
    sourceArray2[38] = (byte) 79;
    sourceArray2[45] = (byte) 99;
    sourceArray2[0] = (byte) 56;
    sourceArray2[32 /*0x20*/] = (byte) 1;
    sourceArray2[42] = (byte) 150;
    sourceArray2[44] = (byte) 165;
    sourceArray2[29] = (byte) 125;
    sourceArray2[8] = (byte) 22;
    sourceArray2[3] = (byte) 123;
    sourceArray2[47] = (byte) 125;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13573(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 30,
      (byte) 220,
      (byte) 116,
      (byte) 174,
      (byte) 208 /*0xD0*/,
      (byte) 143,
      (byte) 81,
      (byte) 120,
      (byte) 196,
      (byte) 23,
      (byte) 160 /*0xA0*/,
      (byte) 11,
      (byte) 128 /*0x80*/,
      (byte) 118,
      (byte) 120,
      (byte) 228,
      (byte) 2,
      (byte) 83,
      (byte) 142,
      (byte) 142,
      (byte) 56,
      (byte) 113,
      (byte) 149,
      (byte) 196,
      (byte) 87,
      (byte) 227,
      (byte) 36,
      (byte) 233,
      (byte) 176 /*0xB0*/,
      (byte) 207,
      (byte) 97,
      (byte) 10,
      (byte) 129,
      (byte) 140,
      (byte) 143,
      (byte) 206,
      (byte) 129,
      (byte) 29,
      (byte) 209,
      (byte) 6,
      (byte) 69,
      (byte) 86,
      (byte) 55,
      (byte) 243,
      (byte) 95,
      (byte) 192 /*0xC0*/,
      (byte) 125,
      (byte) 116
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[27] = (byte) 78;
    sourceArray2[1] = (byte) 216;
    sourceArray2[16 /*0x10*/] = (byte) 5;
    sourceArray2[3] = (byte) 228;
    sourceArray2[6] = (byte) 80 /*0x50*/;
    sourceArray2[5] = (byte) 228;
    sourceArray2[7] = (byte) 161;
    sourceArray2[4] = (byte) 183;
    sourceArray2[8] = (byte) 54;
    sourceArray2[19] = (byte) 31 /*0x1F*/;
    sourceArray2[10] = (byte) 23;
    sourceArray2[11] = (byte) 29;
    sourceArray2[0] = (byte) 60;
    sourceArray2[31 /*0x1F*/] = (byte) 131;
    sourceArray2[2] = (byte) 2;
    sourceArray2[13] = (byte) 6;
    sourceArray2[43] = (byte) 29;
    sourceArray2[26] = (byte) 70;
    sourceArray2[14] = (byte) 115;
    sourceArray2[36] = (byte) 232;
    sourceArray2[20] = (byte) 172;
    sourceArray2[42] = (byte) 165;
    sourceArray2[12] = (byte) 217;
    sourceArray2[23] = (byte) 65;
    sourceArray2[9] = (byte) 115;
    sourceArray2[40] = (byte) 183;
    sourceArray2[38] = (byte) 69;
    sourceArray2[18] = (byte) 75;
    sourceArray2[28] = (byte) 123;
    sourceArray2[29] = (byte) 169;
    sourceArray2[30] = (byte) 2;
    sourceArray2[21] = (byte) 237;
    sourceArray2[24] = (byte) 252;
    sourceArray2[33] = (byte) 87;
    sourceArray2[47] = (byte) 131;
    sourceArray2[17] = (byte) 52;
    sourceArray2[34] = (byte) 206;
    sourceArray2[25] = (byte) 146;
    sourceArray2[22] = (byte) 252;
    sourceArray2[32 /*0x20*/] = (byte) 212;
    sourceArray2[39] = (byte) 83;
    sourceArray2[41] = (byte) 116;
    sourceArray2[37] = (byte) 186;
    sourceArray2[35] = (byte) 166;
    sourceArray2[44] = (byte) 60;
    sourceArray2[45] = (byte) 71;
    sourceArray2[46] = (byte) 98;
    sourceArray2[15] = (byte) 177;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13574(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[34] = (byte) 219;
    sourceArray1[1] = (byte) 140;
    sourceArray1[30] = (byte) 248;
    sourceArray1[23] = (byte) 34;
    sourceArray1[4] = (byte) 131;
    sourceArray1[5] = (byte) 35;
    sourceArray1[39] = (byte) 151;
    sourceArray1[44] = (byte) 227;
    sourceArray1[40] = (byte) 198;
    sourceArray1[45] = (byte) 136;
    sourceArray1[10] = (byte) 164;
    sourceArray1[9] = (byte) 114;
    sourceArray1[26] = (byte) 159;
    sourceArray1[13] = (byte) 136;
    sourceArray1[14] = (byte) 186;
    sourceArray1[22] = (byte) 4;
    sourceArray1[28] = (byte) 108;
    sourceArray1[17] = (byte) 142;
    sourceArray1[2] = (byte) 228;
    sourceArray1[19] = (byte) 241;
    sourceArray1[20] = (byte) 30;
    sourceArray1[21] = (byte) 227;
    sourceArray1[6] = (byte) 222;
    sourceArray1[0] = (byte) 137;
    sourceArray1[3] = (byte) 159;
    sourceArray1[15] = (byte) 13;
    sourceArray1[11] = (byte) 74;
    sourceArray1[27] = (byte) 235;
    sourceArray1[24] = (byte) 144 /*0x90*/;
    sourceArray1[29] = (byte) 83;
    sourceArray1[37] = (byte) 13;
    sourceArray1[36] = (byte) 117;
    sourceArray1[12] = (byte) 191;
    sourceArray1[33] = (byte) 96 /*0x60*/;
    sourceArray1[31 /*0x1F*/] = (byte) 99;
    sourceArray1[35] = (byte) 35;
    sourceArray1[46] = (byte) 7;
    sourceArray1[7] = (byte) 204;
    sourceArray1[38] = (byte) 226;
    sourceArray1[16 /*0x10*/] = (byte) 26;
    sourceArray1[41] = (byte) 159;
    sourceArray1[32 /*0x20*/] = (byte) 155;
    sourceArray1[42] = (byte) 120;
    sourceArray1[43] = (byte) 112 /*0x70*/;
    sourceArray1[8] = (byte) 152;
    sourceArray1[18] = (byte) 250;
    sourceArray1[25] = (byte) 30;
    sourceArray1[47] = (byte) 90;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 250,
      (byte) 28,
      (byte) 21,
      (byte) 205,
      (byte) 229,
      (byte) 248,
      (byte) 135,
      (byte) 66,
      (byte) 169,
      (byte) 72,
      (byte) 179,
      (byte) 231,
      (byte) 166,
      (byte) 107,
      (byte) 219,
      (byte) 117,
      (byte) 0,
      (byte) 63 /*0x3F*/,
      (byte) 54,
      (byte) 5,
      (byte) 242,
      (byte) 222,
      (byte) 18,
      (byte) 254,
      (byte) 214,
      (byte) 121,
      (byte) 213,
      (byte) 51,
      (byte) 247,
      (byte) 161,
      (byte) 156,
      (byte) 224 /*0xE0*/,
      (byte) 3,
      (byte) 15,
      (byte) 47,
      (byte) 72,
      (byte) 68,
      (byte) 49,
      (byte) 175,
      (byte) 79,
      (byte) 101,
      (byte) 104,
      (byte) 236,
      (byte) 219,
      (byte) 90,
      (byte) 251,
      (byte) 87,
      (byte) 237
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[49];
    byte[] response2 = new byte[49];
    Array.Copy((Array) sc_13556.sspq, 61, (Array) numArray2, 0, 49);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13556.sspr, 61, (Array) numArray2, 0, 49);
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

  internal static int ssp_appserver_13575(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 123,
      (byte) 204,
      (byte) 194,
      (byte) 127 /*0x7F*/,
      (byte) 240 /*0xF0*/,
      (byte) 147,
      (byte) 55,
      (byte) 76,
      (byte) 83,
      (byte) 88,
      (byte) 216,
      (byte) 146,
      (byte) 86,
      (byte) 87,
      (byte) 3,
      (byte) 88,
      (byte) 229,
      (byte) 217,
      (byte) 223,
      (byte) 170,
      (byte) 38,
      (byte) 94,
      (byte) 238,
      (byte) 247,
      (byte) 229,
      (byte) 210,
      (byte) 99,
      (byte) 122,
      (byte) 87,
      (byte) 179,
      (byte) 24,
      byte.MaxValue,
      (byte) 125,
      (byte) 106,
      (byte) 196,
      (byte) 106,
      (byte) 218,
      (byte) 29,
      (byte) 61,
      (byte) 218,
      (byte) 94,
      (byte) 117,
      (byte) 17,
      (byte) 30,
      (byte) 100,
      (byte) 171,
      (byte) 192 /*0xC0*/,
      (byte) 90
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 75,
      (byte) 130,
      (byte) 178,
      (byte) 77,
      (byte) 88,
      (byte) 84,
      (byte) 140,
      (byte) 38,
      (byte) 70,
      (byte) 92,
      (byte) 208 /*0xD0*/,
      (byte) 45,
      (byte) 14,
      (byte) 118,
      (byte) 28,
      (byte) 89,
      (byte) 137,
      (byte) 65,
      (byte) 70,
      (byte) 121,
      (byte) 180,
      (byte) 143,
      (byte) 61,
      (byte) 94,
      (byte) 172,
      (byte) 91,
      (byte) 86,
      (byte) 137,
      (byte) 229,
      (byte) 117,
      (byte) 39,
      (byte) 80 /*0x50*/,
      (byte) 7,
      (byte) 233,
      (byte) 71,
      (byte) 233,
      (byte) 163,
      (byte) 102,
      (byte) 247,
      (byte) 115,
      (byte) 124,
      (byte) 219,
      (byte) 142,
      (byte) 244,
      (byte) 109,
      (byte) 246,
      (byte) 230,
      (byte) 209
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13576(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[14] = (byte) 236;
    sourceArray1[6] = (byte) 29;
    sourceArray1[0] = (byte) 39;
    sourceArray1[7] = (byte) 48 /*0x30*/;
    sourceArray1[4] = (byte) 202;
    sourceArray1[5] = (byte) 154;
    sourceArray1[2] = (byte) 29;
    sourceArray1[36] = (byte) 166;
    sourceArray1[43] = (byte) 128 /*0x80*/;
    sourceArray1[9] = (byte) 244;
    sourceArray1[37] = (byte) 140;
    sourceArray1[11] = (byte) 64 /*0x40*/;
    sourceArray1[1] = (byte) 197;
    sourceArray1[39] = (byte) 131;
    sourceArray1[3] = (byte) 221;
    sourceArray1[35] = (byte) 165;
    sourceArray1[17] = (byte) 125;
    sourceArray1[40] = (byte) 32 /*0x20*/;
    sourceArray1[16 /*0x10*/] = (byte) 152;
    sourceArray1[19] = (byte) 155;
    sourceArray1[20] = (byte) 5;
    sourceArray1[32 /*0x20*/] = (byte) 251;
    sourceArray1[18] = (byte) 199;
    sourceArray1[23] = (byte) 76;
    sourceArray1[47] = (byte) 150;
    sourceArray1[25] = (byte) 146;
    sourceArray1[21] = (byte) 75;
    sourceArray1[27] = (byte) 18;
    sourceArray1[28] = (byte) 8;
    sourceArray1[29] = (byte) 130;
    sourceArray1[10] = (byte) 159;
    sourceArray1[12] = (byte) 216;
    sourceArray1[8] = (byte) 188;
    sourceArray1[44] = (byte) 114;
    sourceArray1[30] = (byte) 10;
    sourceArray1[15] = (byte) 146;
    sourceArray1[41] = (byte) 170;
    sourceArray1[31 /*0x1F*/] = (byte) 104;
    sourceArray1[13] = (byte) 20;
    sourceArray1[26] = (byte) 58;
    sourceArray1[24] = (byte) 208 /*0xD0*/;
    sourceArray1[22] = (byte) 177;
    sourceArray1[38] = (byte) 16 /*0x10*/;
    sourceArray1[46] = (byte) 140;
    sourceArray1[33] = (byte) 176 /*0xB0*/;
    sourceArray1[45] = (byte) 222;
    sourceArray1[42] = (byte) 162;
    sourceArray1[34] = (byte) 85;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 102,
      (byte) 12,
      (byte) 165,
      (byte) 222,
      (byte) 252,
      (byte) 211,
      (byte) 80 /*0x50*/,
      (byte) 50,
      (byte) 116,
      (byte) 125,
      (byte) 152,
      (byte) 141,
      (byte) 84,
      (byte) 162,
      (byte) 208 /*0xD0*/,
      (byte) 199,
      (byte) 200,
      (byte) 124,
      (byte) 119,
      (byte) 217,
      (byte) 54,
      (byte) 47,
      (byte) 117,
      (byte) 254,
      (byte) 135,
      (byte) 110,
      (byte) 87,
      (byte) 134,
      (byte) 102,
      (byte) 130,
      (byte) 195,
      (byte) 119,
      (byte) 140,
      (byte) 39,
      (byte) 158,
      (byte) 158,
      (byte) 164,
      (byte) 104,
      (byte) 129,
      (byte) 195,
      (byte) 152,
      (byte) 251,
      (byte) 116,
      (byte) 225,
      (byte) 241,
      (byte) 121,
      (byte) 193,
      (byte) 107
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
