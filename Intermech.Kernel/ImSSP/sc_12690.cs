// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_12690
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_12690
{
  private static byte[] sspq = new byte[43]
  {
    (byte) 74,
    (byte) 114,
    (byte) 102,
    (byte) 148,
    (byte) 250,
    (byte) 107,
    (byte) 109,
    (byte) 185,
    (byte) 155,
    (byte) 176 /*0xB0*/,
    (byte) 49,
    (byte) 64 /*0x40*/,
    (byte) 67,
    (byte) 120,
    (byte) 181,
    (byte) 129,
    (byte) 104,
    (byte) 147,
    (byte) 66,
    (byte) 20,
    (byte) 149,
    (byte) 7,
    (byte) 124,
    (byte) 249,
    (byte) 185,
    (byte) 29,
    (byte) 153,
    (byte) 144 /*0x90*/,
    (byte) 69,
    (byte) 204,
    (byte) 141,
    (byte) 92,
    (byte) 134,
    (byte) 17,
    (byte) 208 /*0xD0*/,
    (byte) 4,
    (byte) 162,
    (byte) 63 /*0x3F*/,
    (byte) 208 /*0xD0*/,
    (byte) 116,
    (byte) 102,
    (byte) 156,
    (byte) 78
  };
  private static byte[] sspr = new byte[43]
  {
    (byte) 63 /*0x3F*/,
    (byte) 190,
    (byte) 83,
    (byte) 180,
    (byte) 9,
    (byte) 170,
    (byte) 234,
    (byte) 153,
    (byte) 10,
    (byte) 73,
    (byte) 21,
    (byte) 49,
    (byte) 19,
    (byte) 202,
    (byte) 92,
    (byte) 11,
    (byte) 121,
    (byte) 63 /*0x3F*/,
    (byte) 63 /*0x3F*/,
    (byte) 244,
    (byte) 222,
    (byte) 135,
    (byte) 174,
    (byte) 43,
    (byte) 138,
    (byte) 62,
    (byte) 233,
    (byte) 112 /*0x70*/,
    (byte) 58,
    (byte) 78,
    (byte) 201,
    (byte) 155,
    (byte) 131,
    (byte) 241,
    (byte) 90,
    (byte) 131,
    (byte) 231,
    (byte) 95,
    (byte) 112 /*0x70*/,
    (byte) 33,
    (byte) 156,
    (byte) 180,
    (byte) 122
  };

  internal static string ssp_appserver_12691()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[63 /*0x3F*/];
      byte[] numArray2 = new byte[55];
      numArray2[26] = (byte) 23;
      numArray2[1] = (byte) 4;
      numArray2[32 /*0x20*/] = (byte) 11;
      numArray2[51] = (byte) 12;
      numArray2[38] = (byte) 5;
      numArray2[5] = (byte) 254;
      numArray2[6] = (byte) 28;
      numArray2[19] = (byte) 191;
      numArray2[33] = (byte) 10;
      numArray2[45] = (byte) 153;
      numArray2[2] = (byte) 4;
      numArray2[17] = (byte) 202;
      numArray2[12] = (byte) 60;
      numArray2[28] = (byte) 28;
      numArray2[14] = (byte) 61;
      numArray2[16 /*0x10*/] = (byte) 114;
      numArray2[47] = (byte) 32 /*0x20*/;
      numArray2[41] = (byte) 92;
      numArray2[39] = (byte) 198;
      numArray2[31 /*0x1F*/] = (byte) 246;
      numArray2[20] = (byte) 237;
      numArray2[21] = (byte) 251;
      numArray2[13] = (byte) 175;
      numArray2[15] = (byte) 125;
      numArray2[49] = (byte) 168;
      numArray2[25] = (byte) 103;
      numArray2[18] = (byte) 175;
      numArray2[43] = (byte) 219;
      numArray2[10] = (byte) 70;
      numArray2[29] = (byte) 11;
      numArray2[30] = (byte) 132;
      numArray2[27] = (byte) 176 /*0xB0*/;
      numArray2[8] = (byte) 193;
      numArray2[22] = (byte) 122;
      numArray2[34] = (byte) 134;
      numArray2[35] = (byte) 50;
      numArray2[36] = (byte) 178;
      numArray2[37] = (byte) 37;
      numArray2[3] = (byte) 111;
      numArray2[9] = (byte) 128 /*0x80*/;
      numArray2[40] = (byte) 167;
      numArray2[11] = (byte) 107;
      numArray2[42] = (byte) 51;
      numArray2[23] = (byte) 174;
      numArray2[44] = (byte) 191;
      numArray2[4] = (byte) 0;
      numArray2[46] = (byte) 91;
      numArray2[7] = (byte) 248;
      numArray2[48 /*0x30*/] = (byte) 53;
      numArray2[50] = (byte) 8;
      numArray2[0] = (byte) 111;
      numArray2[24] = (byte) 122;
      numArray2[52] = (byte) 96 /*0x60*/;
      numArray2[53] = (byte) 249;
      numArray2[54] = (byte) 173;
      byte[] numArray3 = new byte[55];
      numArray3[35] = (byte) 129;
      numArray3[3] = (byte) 219;
      numArray3[2] = (byte) 46;
      numArray3[15] = (byte) 212;
      numArray3[20] = (byte) 199;
      numArray3[45] = (byte) 75;
      numArray3[6] = (byte) 41;
      numArray3[40] = (byte) 109;
      numArray3[8] = (byte) 148;
      numArray3[37] = (byte) 18;
      numArray3[10] = (byte) 156;
      numArray3[11] = (byte) 5;
      numArray3[12] = (byte) 8;
      numArray3[13] = (byte) 50;
      numArray3[14] = (byte) 40;
      numArray3[4] = (byte) 97;
      numArray3[16 /*0x10*/] = (byte) 178;
      numArray3[17] = (byte) 59;
      numArray3[18] = (byte) 212;
      numArray3[19] = (byte) 155;
      numArray3[44] = (byte) 236;
      numArray3[21] = (byte) 197;
      numArray3[22] = (byte) 250;
      numArray3[23] = (byte) 113;
      numArray3[24] = (byte) 92;
      numArray3[38] = (byte) 149;
      numArray3[9] = (byte) 123;
      numArray3[27] = (byte) 184;
      numArray3[28] = (byte) 177;
      numArray3[29] = (byte) 49;
      numArray3[30] = (byte) 105;
      numArray3[7] = (byte) 118;
      numArray3[34] = (byte) 156;
      numArray3[49] = (byte) 65;
      numArray3[41] = (byte) 132;
      numArray3[33] = (byte) 79;
      numArray3[36] = (byte) 125;
      numArray3[5] = (byte) 248;
      numArray3[53] = (byte) 174;
      numArray3[51] = (byte) 214;
      numArray3[39] = (byte) 210;
      numArray3[43] = (byte) 157;
      numArray3[26] = (byte) 29;
      numArray3[1] = (byte) 12;
      numArray3[0] = (byte) 218;
      numArray3[32 /*0x20*/] = (byte) 199;
      numArray3[46] = (byte) 33;
      numArray3[47] = (byte) 127 /*0x7F*/;
      numArray3[48 /*0x30*/] = (byte) 204;
      numArray3[42] = (byte) 143;
      numArray3[50] = (byte) 63 /*0x3F*/;
      numArray3[31 /*0x1F*/] = (byte) 5;
      numArray3[52] = (byte) 60;
      numArray3[25] = (byte) 104;
      numArray3[54] = (byte) 204;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[8];
      numArray4[0] = (byte) 232;
      numArray4[7] = (byte) 85;
      numArray4[1] = (byte) 107;
      numArray4[3] = (byte) 240 /*0xF0*/;
      numArray4[4] = (byte) 159;
      numArray4[5] = (byte) 208 /*0xD0*/;
      numArray4[6] = (byte) 108;
      numArray4[2] = (byte) 171;
      byte[] numArray5 = new byte[8]
      {
        (byte) 216,
        (byte) 72,
        (byte) 157,
        (byte) 220,
        (byte) 113,
        byte.MaxValue,
        (byte) 83,
        (byte) 203
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 8);
      for (int index = 0; index < 8; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[63 /*0x3F*/];
    byte[] numArray7 = new byte[55]
    {
      (byte) 131,
      (byte) 100,
      (byte) 201,
      (byte) 190,
      (byte) 139,
      (byte) 32 /*0x20*/,
      (byte) 138,
      (byte) 40,
      (byte) 168,
      (byte) 63 /*0x3F*/,
      (byte) 177,
      (byte) 45,
      (byte) 53,
      (byte) 4,
      (byte) 143,
      (byte) 11,
      (byte) 176 /*0xB0*/,
      (byte) 51,
      (byte) 254,
      (byte) 169,
      (byte) 71,
      (byte) 92,
      (byte) 5,
      (byte) 26,
      (byte) 204,
      (byte) 193,
      (byte) 44,
      (byte) 31 /*0x1F*/,
      (byte) 243,
      (byte) 158,
      (byte) 118,
      (byte) 82,
      (byte) 110,
      (byte) 17,
      (byte) 221,
      (byte) 200,
      (byte) 30,
      (byte) 173,
      (byte) 31 /*0x1F*/,
      (byte) 208 /*0xD0*/,
      (byte) 21,
      (byte) 168,
      (byte) 195,
      (byte) 21,
      (byte) 233,
      (byte) 239,
      (byte) 94,
      (byte) 174,
      (byte) 24,
      (byte) 111,
      (byte) 79,
      (byte) 70,
      (byte) 28,
      (byte) 234,
      (byte) 178
    };
    byte[] numArray8 = new byte[55];
    numArray8[4] = (byte) 142;
    numArray8[40] = (byte) 91;
    numArray8[2] = (byte) 11;
    numArray8[29] = (byte) 9;
    numArray8[50] = (byte) 59;
    numArray8[30] = (byte) 165;
    numArray8[18] = (byte) 123;
    numArray8[11] = (byte) 244;
    numArray8[8] = (byte) 191;
    numArray8[9] = (byte) 13;
    numArray8[7] = (byte) 241;
    numArray8[35] = (byte) 73;
    numArray8[0] = (byte) 151;
    numArray8[6] = (byte) 26;
    numArray8[41] = (byte) 222;
    numArray8[15] = (byte) 55;
    numArray8[44] = (byte) 254;
    numArray8[16 /*0x10*/] = (byte) 135;
    numArray8[38] = (byte) 167;
    numArray8[51] = (byte) 156;
    numArray8[47] = (byte) 60;
    numArray8[36] = (byte) 11;
    numArray8[48 /*0x30*/] = (byte) 162;
    numArray8[23] = (byte) 32 /*0x20*/;
    numArray8[24] = (byte) 247;
    numArray8[25] = (byte) 240 /*0xF0*/;
    numArray8[26] = (byte) 225;
    numArray8[27] = (byte) 41;
    numArray8[5] = (byte) 140;
    numArray8[21] = (byte) 35;
    numArray8[13] = (byte) 30;
    numArray8[22] = (byte) 123;
    numArray8[32 /*0x20*/] = (byte) 193;
    numArray8[31 /*0x1F*/] = (byte) 198;
    numArray8[34] = byte.MaxValue;
    numArray8[10] = (byte) 195;
    numArray8[17] = (byte) 249;
    numArray8[12] = (byte) 101;
    numArray8[1] = (byte) 163;
    numArray8[39] = (byte) 73;
    numArray8[20] = (byte) 254;
    numArray8[3] = (byte) 59;
    numArray8[42] = (byte) 68;
    numArray8[43] = (byte) 130;
    numArray8[19] = (byte) 208 /*0xD0*/;
    numArray8[45] = (byte) 49;
    numArray8[46] = (byte) 72;
    numArray8[49] = (byte) 161;
    numArray8[37] = (byte) 70;
    numArray8[28] = (byte) 253;
    numArray8[14] = (byte) 126;
    numArray8[33] = (byte) 236;
    numArray8[52] = (byte) 72;
    numArray8[53] = (byte) 184;
    numArray8[54] = (byte) 205;
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[8]
    {
      (byte) 126,
      (byte) 21,
      (byte) 104,
      (byte) 116,
      (byte) 100,
      (byte) 175,
      (byte) 187,
      (byte) 153
    };
    byte[] numArray10 = new byte[8]
    {
      (byte) 128 /*0x80*/,
      (byte) 181,
      (byte) 122,
      (byte) 134,
      (byte) 68,
      (byte) 21,
      (byte) 100,
      (byte) 7
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 8);
    for (int index = 0; index < 8; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_12692()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[195];
      byte[] numArray2 = new byte[55]
      {
        (byte) 92,
        (byte) 126,
        (byte) 71,
        (byte) 189,
        (byte) 81,
        (byte) 233,
        (byte) 42,
        (byte) 224 /*0xE0*/,
        (byte) 162,
        (byte) 165,
        (byte) 171,
        (byte) 40,
        (byte) 247,
        (byte) 180,
        (byte) 3,
        (byte) 2,
        (byte) 253,
        (byte) 72,
        (byte) 46,
        (byte) 74,
        (byte) 42,
        (byte) 251,
        (byte) 240 /*0xF0*/,
        (byte) 165,
        (byte) 237,
        (byte) 105,
        (byte) 69,
        (byte) 195,
        (byte) 253,
        (byte) 179,
        (byte) 99,
        (byte) 246,
        (byte) 115,
        (byte) 24,
        (byte) 245,
        (byte) 2,
        (byte) 4,
        (byte) 117,
        (byte) 156,
        (byte) 107,
        (byte) 232,
        (byte) 172,
        (byte) 204,
        (byte) 216,
        (byte) 106,
        (byte) 63 /*0x3F*/,
        (byte) 124,
        (byte) 175,
        (byte) 172,
        (byte) 184,
        (byte) 156,
        (byte) 223,
        (byte) 11,
        (byte) 123,
        (byte) 69
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 254,
        (byte) 212,
        (byte) 148,
        (byte) 119,
        (byte) 30,
        (byte) 22,
        (byte) 239,
        (byte) 30,
        (byte) 10,
        (byte) 67,
        (byte) 205,
        (byte) 238,
        (byte) 69,
        (byte) 174,
        (byte) 84,
        (byte) 130,
        (byte) 76,
        (byte) 150,
        (byte) 63 /*0x3F*/,
        (byte) 79,
        (byte) 197,
        (byte) 135,
        (byte) 200,
        (byte) 126,
        (byte) 1,
        (byte) 2,
        (byte) 196,
        (byte) 172,
        (byte) 3,
        (byte) 136,
        (byte) 222,
        (byte) 144 /*0x90*/,
        (byte) 106,
        (byte) 194,
        (byte) 44,
        (byte) 67,
        (byte) 23,
        (byte) 71,
        (byte) 45,
        (byte) 80 /*0x50*/,
        (byte) 210,
        (byte) 241,
        (byte) 148,
        (byte) 150,
        (byte) 75,
        (byte) 54,
        (byte) 176 /*0xB0*/,
        (byte) 221,
        (byte) 21,
        (byte) 160 /*0xA0*/,
        (byte) 127 /*0x7F*/,
        (byte) 99,
        (byte) 47,
        (byte) 180,
        (byte) 62
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 161,
        (byte) 208 /*0xD0*/,
        (byte) 232,
        (byte) 164,
        (byte) 102,
        (byte) 226,
        (byte) 239,
        (byte) 66,
        (byte) 181,
        (byte) 181,
        (byte) 223,
        (byte) 190,
        (byte) 1,
        (byte) 253,
        (byte) 135,
        (byte) 171,
        (byte) 194,
        (byte) 97,
        (byte) 125,
        (byte) 46,
        (byte) 23,
        (byte) 59,
        (byte) 173,
        (byte) 236,
        (byte) 149,
        (byte) 23,
        (byte) 69,
        (byte) 23,
        (byte) 131,
        (byte) 234,
        (byte) 248,
        (byte) 210,
        (byte) 148,
        (byte) 115,
        (byte) 98,
        (byte) 164,
        (byte) 180,
        (byte) 199,
        (byte) 130,
        (byte) 202,
        (byte) 220,
        (byte) 150,
        (byte) 181,
        (byte) 138,
        (byte) 157,
        (byte) 241,
        (byte) 171,
        (byte) 186,
        (byte) 20,
        (byte) 51,
        (byte) 73,
        (byte) 25,
        (byte) 199,
        (byte) 50,
        (byte) 118
      };
      byte[] numArray5 = new byte[55];
      numArray5[9] = (byte) 242;
      numArray5[1] = (byte) 127 /*0x7F*/;
      numArray5[27] = (byte) 42;
      numArray5[0] = (byte) 217;
      numArray5[3] = (byte) 243;
      numArray5[5] = (byte) 108;
      numArray5[15] = (byte) 186;
      numArray5[7] = (byte) 214;
      numArray5[8] = (byte) 212;
      numArray5[23] = (byte) 197;
      numArray5[10] = (byte) 102;
      numArray5[11] = (byte) 108;
      numArray5[48 /*0x30*/] = (byte) 142;
      numArray5[13] = (byte) 113;
      numArray5[6] = (byte) 114;
      numArray5[47] = (byte) 248;
      numArray5[16 /*0x10*/] = (byte) 184;
      numArray5[52] = (byte) 41;
      numArray5[18] = (byte) 60;
      numArray5[19] = (byte) 216;
      numArray5[41] = (byte) 30;
      numArray5[21] = (byte) 83;
      numArray5[49] = (byte) 117;
      numArray5[12] = (byte) 57;
      numArray5[2] = (byte) 45;
      numArray5[24] = (byte) 153;
      numArray5[4] = (byte) 253;
      numArray5[54] = (byte) 171;
      numArray5[28] = (byte) 147;
      numArray5[29] = (byte) 40;
      numArray5[53] = (byte) 185;
      numArray5[31 /*0x1F*/] = (byte) 130;
      numArray5[20] = (byte) 76;
      numArray5[32 /*0x20*/] = (byte) 112 /*0x70*/;
      numArray5[17] = (byte) 219;
      numArray5[35] = (byte) 222;
      numArray5[34] = (byte) 93;
      numArray5[37] = (byte) 156;
      numArray5[14] = (byte) 3;
      numArray5[38] = (byte) 23;
      numArray5[40] = (byte) 118;
      numArray5[44] = (byte) 197;
      numArray5[42] = (byte) 38;
      numArray5[43] = (byte) 242;
      numArray5[26] = (byte) 126;
      numArray5[45] = (byte) 161;
      numArray5[46] = (byte) 226;
      numArray5[39] = (byte) 61;
      numArray5[33] = (byte) 145;
      numArray5[25] = (byte) 191;
      numArray5[50] = (byte) 6;
      numArray5[51] = (byte) 117;
      numArray5[22] = (byte) 32 /*0x20*/;
      numArray5[30] = (byte) 220;
      numArray5[36] = (byte) 58;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[55];
      numArray6[16 /*0x10*/] = (byte) 134;
      numArray6[9] = (byte) 199;
      numArray6[22] = (byte) 220;
      numArray6[3] = (byte) 18;
      numArray6[4] = (byte) 212;
      numArray6[5] = (byte) 111;
      numArray6[6] = (byte) 40;
      numArray6[7] = (byte) 221;
      numArray6[8] = (byte) 1;
      numArray6[51] = (byte) 147;
      numArray6[19] = (byte) 155;
      numArray6[50] = (byte) 141;
      numArray6[29] = (byte) 247;
      numArray6[13] = (byte) 61;
      numArray6[14] = (byte) 65;
      numArray6[37] = (byte) 26;
      numArray6[45] = (byte) 129;
      numArray6[17] = (byte) 76;
      numArray6[18] = (byte) 170;
      numArray6[33] = (byte) 130;
      numArray6[20] = (byte) 85;
      numArray6[46] = (byte) 206;
      numArray6[38] = (byte) 49;
      numArray6[23] = (byte) 77;
      numArray6[40] = (byte) 193;
      numArray6[47] = (byte) 254;
      numArray6[26] = (byte) 18;
      numArray6[27] = (byte) 83;
      numArray6[28] = (byte) 165;
      numArray6[49] = (byte) 140;
      numArray6[30] = (byte) 129;
      numArray6[31 /*0x1F*/] = (byte) 9;
      numArray6[44] = (byte) 142;
      numArray6[41] = (byte) 240 /*0xF0*/;
      numArray6[34] = (byte) 12;
      numArray6[35] = (byte) 79;
      numArray6[36] = (byte) 79;
      numArray6[11] = (byte) 158;
      numArray6[2] = (byte) 227;
      numArray6[39] = (byte) 103;
      numArray6[0] = (byte) 172;
      numArray6[1] = (byte) 105;
      numArray6[42] = (byte) 143;
      numArray6[43] = byte.MaxValue;
      numArray6[12] = (byte) 97;
      numArray6[48 /*0x30*/] = (byte) 13;
      numArray6[25] = (byte) 210;
      numArray6[21] = (byte) 190;
      numArray6[10] = (byte) 143;
      numArray6[53] = (byte) 68;
      numArray6[24] = (byte) 106;
      numArray6[15] = (byte) 173;
      numArray6[52] = (byte) 73;
      numArray6[32 /*0x20*/] = (byte) 28;
      numArray6[54] = (byte) 169;
      byte[] numArray7 = new byte[55];
      numArray7[7] = (byte) 25;
      numArray7[24] = (byte) 45;
      numArray7[2] = (byte) 134;
      numArray7[3] = (byte) 47;
      numArray7[4] = (byte) 36;
      numArray7[19] = (byte) 60;
      numArray7[51] = (byte) 143;
      numArray7[28] = (byte) 46;
      numArray7[44] = (byte) 193;
      numArray7[9] = (byte) 84;
      numArray7[10] = (byte) 113;
      numArray7[15] = (byte) 88;
      numArray7[12] = (byte) 4;
      numArray7[13] = (byte) 194;
      numArray7[14] = (byte) 10;
      numArray7[47] = (byte) 185;
      numArray7[16 /*0x10*/] = (byte) 151;
      numArray7[17] = (byte) 142;
      numArray7[26] = (byte) 69;
      numArray7[0] = (byte) 27;
      numArray7[20] = (byte) 91;
      numArray7[11] = (byte) 74;
      numArray7[48 /*0x30*/] = (byte) 251;
      numArray7[23] = (byte) 35;
      numArray7[1] = (byte) 145;
      numArray7[25] = (byte) 176 /*0xB0*/;
      numArray7[33] = (byte) 229;
      numArray7[32 /*0x20*/] = (byte) 79;
      numArray7[39] = (byte) 140;
      numArray7[29] = (byte) 107;
      numArray7[30] = (byte) 164;
      numArray7[31 /*0x1F*/] = (byte) 157;
      numArray7[37] = (byte) 74;
      numArray7[6] = (byte) 247;
      numArray7[49] = (byte) 19;
      numArray7[35] = (byte) 30;
      numArray7[36] = (byte) 21;
      numArray7[43] = (byte) 190;
      numArray7[38] = (byte) 99;
      numArray7[18] = (byte) 22;
      numArray7[40] = (byte) 119;
      numArray7[41] = (byte) 149;
      numArray7[8] = (byte) 181;
      numArray7[42] = (byte) 30;
      numArray7[27] = (byte) 141;
      numArray7[52] = (byte) 247;
      numArray7[46] = (byte) 174;
      numArray7[22] = (byte) 90;
      numArray7[45] = (byte) 161;
      numArray7[34] = (byte) 197;
      numArray7[50] = (byte) 196;
      numArray7[54] = (byte) 252;
      numArray7[21] = (byte) 0;
      numArray7[53] = (byte) 6;
      numArray7[5] = (byte) 174;
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[30];
      numArray8[10] = (byte) 113;
      numArray8[1] = (byte) 214;
      numArray8[11] = (byte) 177;
      numArray8[3] = (byte) 3;
      numArray8[18] = (byte) 20;
      numArray8[13] = (byte) 191;
      numArray8[29] = (byte) 235;
      numArray8[28] = (byte) 232;
      numArray8[8] = (byte) 13;
      numArray8[22] = (byte) 147;
      numArray8[12] = (byte) 26;
      numArray8[19] = (byte) 194;
      numArray8[6] = (byte) 86;
      numArray8[24] = (byte) 138;
      numArray8[14] = (byte) 71;
      numArray8[15] = (byte) 184;
      numArray8[5] = (byte) 53;
      numArray8[17] = (byte) 145;
      numArray8[4] = (byte) 101;
      numArray8[21] = (byte) 118;
      numArray8[20] = (byte) 192 /*0xC0*/;
      numArray8[2] = (byte) 231;
      numArray8[9] = (byte) 59;
      numArray8[23] = (byte) 44;
      numArray8[16 /*0x10*/] = (byte) 119;
      numArray8[25] = (byte) 60;
      numArray8[0] = (byte) 30;
      numArray8[27] = (byte) 33;
      numArray8[7] = (byte) 226;
      numArray8[26] = (byte) 94;
      byte[] numArray9 = new byte[30]
      {
        (byte) 133,
        (byte) 149,
        (byte) 110,
        (byte) 148,
        (byte) 173,
        (byte) 156,
        (byte) 80 /*0x50*/,
        (byte) 164,
        (byte) 154,
        (byte) 190,
        (byte) 63 /*0x3F*/,
        (byte) 42,
        (byte) 47,
        (byte) 196,
        (byte) 133,
        (byte) 15,
        (byte) 208 /*0xD0*/,
        (byte) 3,
        (byte) 123,
        (byte) 115,
        (byte) 96 /*0x60*/,
        (byte) 200,
        (byte) 186,
        (byte) 85,
        (byte) 118,
        (byte) 237,
        (byte) 120,
        (byte) 229,
        (byte) 53,
        (byte) 71
      };
      key.Query(true, 335, numArray8, numArray8);
      Array.Copy((Array) numArray8, 0, (Array) numArray1, 165, 30);
      for (int index = 0; index < 30; ++index)
        numArray1[index + 165] ^= numArray9[index];
      byte[] numArray10 = new byte[43];
      byte[] response = new byte[43];
      Array.Copy((Array) sc_12690.sspq, 0, (Array) numArray10, 0, 43);
      key.Query(true, 335, numArray10, response);
      Array.Copy((Array) sc_12690.sspr, 0, (Array) numArray10, 0, 43);
      for (int index = 0; index < numArray10.Length; ++index)
      {
        if ((int) numArray10[index] != (int) response[index])
        {
          key.TagValue = (int) response[index];
          break;
        }
      }
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray11 = new byte[195];
    byte[] numArray12 = new byte[55];
    numArray12[15] = (byte) 218;
    numArray12[18] = (byte) 220;
    numArray12[2] = (byte) 140;
    numArray12[41] = (byte) 11;
    numArray12[4] = (byte) 202;
    numArray12[36] = (byte) 45;
    numArray12[14] = (byte) 109;
    numArray12[7] = (byte) 193;
    numArray12[8] = (byte) 66;
    numArray12[9] = (byte) 241;
    numArray12[10] = (byte) 228;
    numArray12[45] = (byte) 133;
    numArray12[29] = (byte) 194;
    numArray12[13] = (byte) 245;
    numArray12[6] = (byte) 246;
    numArray12[1] = (byte) 32 /*0x20*/;
    numArray12[16 /*0x10*/] = (byte) 220;
    numArray12[17] = (byte) 209;
    numArray12[40] = (byte) 79;
    numArray12[31 /*0x1F*/] = (byte) 174;
    numArray12[24] = (byte) 49;
    numArray12[5] = (byte) 4;
    numArray12[53] = (byte) 40;
    numArray12[23] = (byte) 186;
    numArray12[54] = (byte) 127 /*0x7F*/;
    numArray12[19] = (byte) 115;
    numArray12[32 /*0x20*/] = (byte) 249;
    numArray12[27] = (byte) 222;
    numArray12[28] = (byte) 41;
    numArray12[33] = (byte) 184;
    numArray12[42] = (byte) 74;
    numArray12[30] = (byte) 206;
    numArray12[26] = (byte) 177;
    numArray12[50] = (byte) 243;
    numArray12[3] = (byte) 100;
    numArray12[20] = (byte) 207;
    numArray12[11] = (byte) 139;
    numArray12[37] = (byte) 184;
    numArray12[38] = (byte) 206;
    numArray12[39] = (byte) 140;
    numArray12[12] = (byte) 66;
    numArray12[47] = (byte) 14;
    numArray12[21] = (byte) 35;
    numArray12[43] = (byte) 106;
    numArray12[44] = (byte) 161;
    numArray12[22] = (byte) 213;
    numArray12[46] = (byte) 141;
    numArray12[25] = (byte) 168;
    numArray12[0] = (byte) 209;
    numArray12[49] = (byte) 101;
    numArray12[34] = (byte) 171;
    numArray12[51] = (byte) 162;
    numArray12[52] = (byte) 137;
    numArray12[48 /*0x30*/] = (byte) 53;
    numArray12[35] = (byte) 216;
    byte[] numArray13 = new byte[55];
    numArray13[22] = (byte) 183;
    numArray13[17] = (byte) 228;
    numArray13[2] = (byte) 75;
    numArray13[8] = (byte) 49;
    numArray13[4] = (byte) 13;
    numArray13[43] = (byte) 107;
    numArray13[31 /*0x1F*/] = (byte) 110;
    numArray13[35] = (byte) 131;
    numArray13[7] = (byte) 248;
    numArray13[9] = (byte) 168;
    numArray13[51] = (byte) 235;
    numArray13[45] = (byte) 206;
    numArray13[0] = (byte) 217;
    numArray13[30] = (byte) 33;
    numArray13[14] = (byte) 102;
    numArray13[15] = (byte) 109;
    numArray13[3] = (byte) 169;
    numArray13[11] = (byte) 72;
    numArray13[33] = (byte) 131;
    numArray13[19] = (byte) 168;
    numArray13[20] = (byte) 53;
    numArray13[21] = (byte) 35;
    numArray13[27] = (byte) 168;
    numArray13[1] = (byte) 23;
    numArray13[24] = (byte) 102;
    numArray13[25] = (byte) 242;
    numArray13[54] = (byte) 208 /*0xD0*/;
    numArray13[44] = (byte) 116;
    numArray13[52] = (byte) 47;
    numArray13[12] = (byte) 244;
    numArray13[32 /*0x20*/] = (byte) 91;
    numArray13[53] = (byte) 134;
    numArray13[10] = (byte) 56;
    numArray13[6] = (byte) 188;
    numArray13[34] = (byte) 108;
    numArray13[18] = (byte) 38;
    numArray13[36] = (byte) 113;
    numArray13[37] = (byte) 228;
    numArray13[5] = (byte) 109;
    numArray13[39] = (byte) 209;
    numArray13[40] = (byte) 77;
    numArray13[41] = (byte) 85;
    numArray13[42] = (byte) 222;
    numArray13[29] = (byte) 235;
    numArray13[13] = (byte) 182;
    numArray13[23] = (byte) 45;
    numArray13[46] = (byte) 88;
    numArray13[47] = (byte) 198;
    numArray13[16 /*0x10*/] = (byte) 249;
    numArray13[49] = (byte) 34;
    numArray13[50] = (byte) 177;
    numArray13[28] = (byte) 254;
    numArray13[38] = (byte) 56;
    numArray13[48 /*0x30*/] = (byte) 3;
    numArray13[26] = (byte) 236;
    key.Query(true, 335, numArray12, numArray12);
    Array.Copy((Array) numArray12, 0, (Array) numArray11, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray11[index] ^= numArray13[index];
    byte[] numArray14 = new byte[55];
    numArray14[17] = (byte) 37;
    numArray14[11] = (byte) 36;
    numArray14[36] = (byte) 182;
    numArray14[14] = (byte) 158;
    numArray14[4] = (byte) 103;
    numArray14[5] = (byte) 199;
    numArray14[12] = (byte) 89;
    numArray14[7] = (byte) 107;
    numArray14[8] = (byte) 37;
    numArray14[30] = (byte) 180;
    numArray14[10] = (byte) 210;
    numArray14[13] = (byte) 174;
    numArray14[34] = (byte) 153;
    numArray14[1] = (byte) 205;
    numArray14[24] = (byte) 104;
    numArray14[47] = (byte) 184;
    numArray14[16 /*0x10*/] = (byte) 141;
    numArray14[0] = (byte) 9;
    numArray14[9] = (byte) 245;
    numArray14[41] = (byte) 226;
    numArray14[6] = (byte) 177;
    numArray14[21] = (byte) 85;
    numArray14[45] = (byte) 82;
    numArray14[46] = (byte) 65;
    numArray14[43] = (byte) 59;
    numArray14[25] = (byte) 108;
    numArray14[31 /*0x1F*/] = (byte) 17;
    numArray14[27] = (byte) 205;
    numArray14[28] = (byte) 66;
    numArray14[40] = (byte) 105;
    numArray14[35] = (byte) 49;
    numArray14[2] = (byte) 239;
    numArray14[20] = (byte) 214;
    numArray14[54] = (byte) 229;
    numArray14[32 /*0x20*/] = (byte) 96 /*0x60*/;
    numArray14[18] = (byte) 175;
    numArray14[26] = (byte) 164;
    numArray14[37] = (byte) 185;
    numArray14[38] = (byte) 96 /*0x60*/;
    numArray14[39] = (byte) 216;
    numArray14[23] = (byte) 99;
    numArray14[3] = (byte) 208 /*0xD0*/;
    numArray14[33] = (byte) 242;
    numArray14[44] = (byte) 225;
    numArray14[22] = (byte) 135;
    numArray14[51] = (byte) 242;
    numArray14[15] = (byte) 175;
    numArray14[29] = (byte) 202;
    numArray14[48 /*0x30*/] = (byte) 147;
    numArray14[49] = (byte) 154;
    numArray14[50] = (byte) 147;
    numArray14[42] = (byte) 51;
    numArray14[52] = (byte) 188;
    numArray14[53] = (byte) 212;
    numArray14[19] = (byte) 243;
    byte[] numArray15 = new byte[55];
    numArray15[18] = (byte) 238;
    numArray15[1] = (byte) 219;
    numArray15[45] = (byte) 37;
    numArray15[3] = (byte) 78;
    numArray15[54] = (byte) 245;
    numArray15[39] = (byte) 207;
    numArray15[6] = (byte) 152;
    numArray15[51] = (byte) 55;
    numArray15[8] = (byte) 223;
    numArray15[19] = (byte) 199;
    numArray15[53] = (byte) 133;
    numArray15[11] = (byte) 149;
    numArray15[12] = (byte) 230;
    numArray15[13] = (byte) 47;
    numArray15[48 /*0x30*/] = (byte) 178;
    numArray15[15] = (byte) 220;
    numArray15[16 /*0x10*/] = (byte) 240 /*0xF0*/;
    numArray15[46] = (byte) 254;
    numArray15[17] = (byte) 121;
    numArray15[4] = (byte) 190;
    numArray15[35] = (byte) 151;
    numArray15[33] = (byte) 64 /*0x40*/;
    numArray15[22] = (byte) 20;
    numArray15[26] = (byte) 49;
    numArray15[38] = (byte) 77;
    numArray15[25] = (byte) 241;
    numArray15[5] = (byte) 94;
    numArray15[27] = (byte) 95;
    numArray15[52] = (byte) 236;
    numArray15[29] = (byte) 229;
    numArray15[34] = (byte) 179;
    numArray15[31 /*0x1F*/] = (byte) 134;
    numArray15[32 /*0x20*/] = (byte) 128 /*0x80*/;
    numArray15[40] = (byte) 179;
    numArray15[9] = (byte) 47;
    numArray15[47] = (byte) 145;
    numArray15[24] = (byte) 52;
    numArray15[37] = (byte) 84;
    numArray15[14] = (byte) 139;
    numArray15[30] = (byte) 170;
    numArray15[7] = (byte) 205;
    numArray15[41] = (byte) 195;
    numArray15[21] = (byte) 109;
    numArray15[43] = (byte) 92;
    numArray15[44] = (byte) 141;
    numArray15[20] = (byte) 238;
    numArray15[36] = (byte) 246;
    numArray15[0] = (byte) 98;
    numArray15[42] = (byte) 253;
    numArray15[49] = (byte) 228;
    numArray15[50] = (byte) 28;
    numArray15[28] = (byte) 227;
    numArray15[10] = (byte) 104;
    numArray15[2] = (byte) 108;
    numArray15[23] = (byte) 74;
    key.Query(true, 335, numArray14, numArray14);
    Array.Copy((Array) numArray14, 0, (Array) numArray11, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray11[index + 55] ^= numArray15[index];
    byte[] numArray16 = new byte[55]
    {
      (byte) 168,
      (byte) 203,
      (byte) 127 /*0x7F*/,
      (byte) 238,
      (byte) 150,
      (byte) 122,
      (byte) 172,
      (byte) 233,
      (byte) 250,
      (byte) 243,
      (byte) 62,
      (byte) 218,
      (byte) 145,
      (byte) 174,
      (byte) 130,
      (byte) 203,
      (byte) 162,
      (byte) 40,
      (byte) 2,
      (byte) 150,
      (byte) 214,
      (byte) 172,
      (byte) 46,
      (byte) 207,
      (byte) 210,
      (byte) 109,
      (byte) 50,
      (byte) 59,
      (byte) 199,
      (byte) 174,
      (byte) 253,
      (byte) 100,
      (byte) 13,
      (byte) 70,
      (byte) 118,
      (byte) 52,
      (byte) 20,
      (byte) 144 /*0x90*/,
      (byte) 69,
      (byte) 190,
      (byte) 6,
      (byte) 146,
      (byte) 186,
      (byte) 100,
      (byte) 66,
      (byte) 154,
      (byte) 19,
      (byte) 37,
      (byte) 185,
      (byte) 2,
      (byte) 32 /*0x20*/,
      (byte) 28,
      (byte) 180,
      (byte) 176 /*0xB0*/,
      (byte) 211
    };
    byte[] numArray17 = new byte[55];
    numArray17[40] = (byte) 32 /*0x20*/;
    numArray17[1] = (byte) 236;
    numArray17[2] = (byte) 174;
    numArray17[3] = (byte) 101;
    numArray17[4] = (byte) 150;
    numArray17[0] = (byte) 157;
    numArray17[30] = (byte) 240 /*0xF0*/;
    numArray17[37] = (byte) 22;
    numArray17[8] = (byte) 179;
    numArray17[9] = (byte) 148;
    numArray17[49] = (byte) 187;
    numArray17[11] = (byte) 24;
    numArray17[35] = (byte) 62;
    numArray17[6] = (byte) 59;
    numArray17[14] = (byte) 48 /*0x30*/;
    numArray17[44] = (byte) 103;
    numArray17[27] = (byte) 197;
    numArray17[17] = (byte) 130;
    numArray17[36] = (byte) 49;
    numArray17[19] = (byte) 27;
    numArray17[15] = (byte) 117;
    numArray17[21] = (byte) 75;
    numArray17[22] = (byte) 139;
    numArray17[32 /*0x20*/] = (byte) 154;
    numArray17[24] = (byte) 19;
    numArray17[54] = (byte) 42;
    numArray17[26] = (byte) 177;
    numArray17[53] = (byte) 250;
    numArray17[41] = (byte) 116;
    numArray17[29] = (byte) 176 /*0xB0*/;
    numArray17[18] = (byte) 153;
    numArray17[31 /*0x1F*/] = (byte) 80 /*0x50*/;
    numArray17[47] = (byte) 137;
    numArray17[16 /*0x10*/] = (byte) 135;
    numArray17[5] = (byte) 197;
    numArray17[33] = (byte) 239;
    numArray17[23] = (byte) 121;
    numArray17[25] = (byte) 90;
    numArray17[38] = (byte) 1;
    numArray17[39] = (byte) 88;
    numArray17[10] = (byte) 156;
    numArray17[7] = (byte) 2;
    numArray17[42] = (byte) 80 /*0x50*/;
    numArray17[43] = (byte) 112 /*0x70*/;
    numArray17[34] = (byte) 162;
    numArray17[45] = (byte) 97;
    numArray17[48 /*0x30*/] = (byte) 130;
    numArray17[13] = (byte) 241;
    numArray17[46] = (byte) 32 /*0x20*/;
    numArray17[20] = (byte) 227;
    numArray17[50] = (byte) 7;
    numArray17[51] = (byte) 165;
    numArray17[52] = (byte) 214;
    numArray17[28] = (byte) 80 /*0x50*/;
    numArray17[12] = (byte) 118;
    key.Query(true, 335, numArray16, numArray16);
    Array.Copy((Array) numArray16, 0, (Array) numArray11, 110, 55);
    for (int index = 0; index < 55; ++index)
      numArray11[index + 110] ^= numArray17[index];
    byte[] numArray18 = new byte[30]
    {
      (byte) 34,
      (byte) 228,
      (byte) 206,
      (byte) 58,
      (byte) 240 /*0xF0*/,
      (byte) 92,
      (byte) 190,
      (byte) 20,
      (byte) 40,
      (byte) 113,
      (byte) 215,
      (byte) 189,
      (byte) 181,
      (byte) 81,
      (byte) 158,
      (byte) 199,
      (byte) 205,
      (byte) 251,
      (byte) 211,
      (byte) 132,
      (byte) 91,
      (byte) 35,
      (byte) 107,
      (byte) 146,
      (byte) 31 /*0x1F*/,
      (byte) 21,
      (byte) 121,
      (byte) 127 /*0x7F*/,
      (byte) 168,
      (byte) 63 /*0x3F*/
    };
    byte[] numArray19 = new byte[30]
    {
      (byte) 112 /*0x70*/,
      (byte) 234,
      (byte) 34,
      (byte) 125,
      (byte) 168,
      (byte) 215,
      (byte) 21,
      (byte) 170,
      (byte) 28,
      (byte) 51,
      (byte) 57,
      (byte) 80 /*0x50*/,
      (byte) 188,
      (byte) 31 /*0x1F*/,
      (byte) 96 /*0x60*/,
      (byte) 181,
      (byte) 0,
      (byte) 85,
      (byte) 3,
      (byte) 93,
      (byte) 217,
      (byte) 198,
      (byte) 76,
      (byte) 158,
      (byte) 223,
      (byte) 30,
      (byte) 170,
      (byte) 106,
      (byte) 227,
      (byte) 106
    };
    key.Query(true, 335, numArray18, numArray18);
    Array.Copy((Array) numArray18, 0, (Array) numArray11, 165, 30);
    for (int index = 0; index < 30; ++index)
      numArray11[index + 165] ^= numArray19[index];
    return Encoding.UTF8.GetString(numArray11);
  }
}
