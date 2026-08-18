// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_12583
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_12583
{
  internal static string ssp_appserver_12584()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[71];
      byte[] numArray2 = new byte[55];
      numArray2[14] = (byte) 116;
      numArray2[24] = (byte) 100;
      numArray2[2] = (byte) 218;
      numArray2[37] = (byte) 30;
      numArray2[4] = (byte) 225;
      numArray2[5] = (byte) 42;
      numArray2[6] = (byte) 121;
      numArray2[7] = (byte) 87;
      numArray2[3] = (byte) 139;
      numArray2[29] = (byte) 147;
      numArray2[25] = (byte) 81;
      numArray2[44] = (byte) 148;
      numArray2[12] = (byte) 109;
      numArray2[41] = (byte) 148;
      numArray2[46] = (byte) 157;
      numArray2[19] = (byte) 247;
      numArray2[16 /*0x10*/] = (byte) 124;
      numArray2[17] = (byte) 224 /*0xE0*/;
      numArray2[18] = (byte) 151;
      numArray2[43] = (byte) 234;
      numArray2[20] = (byte) 54;
      numArray2[13] = (byte) 179;
      numArray2[22] = (byte) 232;
      numArray2[30] = (byte) 192 /*0xC0*/;
      numArray2[21] = (byte) 188;
      numArray2[9] = (byte) 238;
      numArray2[26] = (byte) 151;
      numArray2[23] = (byte) 196;
      numArray2[35] = (byte) 3;
      numArray2[28] = (byte) 230;
      numArray2[8] = (byte) 249;
      numArray2[15] = (byte) 107;
      numArray2[32 /*0x20*/] = (byte) 102;
      numArray2[33] = (byte) 27;
      numArray2[47] = (byte) 140;
      numArray2[10] = (byte) 145;
      numArray2[27] = (byte) 178;
      numArray2[0] = (byte) 147;
      numArray2[38] = (byte) 219;
      numArray2[39] = (byte) 217;
      numArray2[40] = (byte) 208 /*0xD0*/;
      numArray2[36] = (byte) 90;
      numArray2[31 /*0x1F*/] = (byte) 33;
      numArray2[1] = (byte) 186;
      numArray2[45] = (byte) 210;
      numArray2[42] = (byte) 100;
      numArray2[11] = (byte) 251;
      numArray2[34] = (byte) 163;
      numArray2[48 /*0x30*/] = (byte) 224 /*0xE0*/;
      numArray2[49] = (byte) 31 /*0x1F*/;
      numArray2[50] = (byte) 200;
      numArray2[51] = (byte) 36;
      numArray2[52] = (byte) 252;
      numArray2[53] = (byte) 28;
      numArray2[54] = (byte) 123;
      byte[] numArray3 = new byte[55]
      {
        (byte) 126,
        (byte) 97,
        (byte) 134,
        (byte) 80 /*0x50*/,
        (byte) 69,
        (byte) 113,
        (byte) 122,
        (byte) 147,
        (byte) 91,
        (byte) 195,
        (byte) 234,
        (byte) 52,
        (byte) 68,
        (byte) 253,
        (byte) 13,
        (byte) 124,
        (byte) 12,
        (byte) 11,
        (byte) 102,
        (byte) 125,
        (byte) 64 /*0x40*/,
        (byte) 194,
        (byte) 68,
        (byte) 24,
        (byte) 246,
        (byte) 189,
        (byte) 96 /*0x60*/,
        (byte) 184,
        (byte) 251,
        (byte) 81,
        (byte) 23,
        (byte) 97,
        (byte) 171,
        (byte) 149,
        (byte) 204,
        (byte) 252,
        (byte) 180,
        (byte) 98,
        (byte) 82,
        (byte) 253,
        (byte) 252,
        (byte) 219,
        (byte) 35,
        (byte) 39,
        (byte) 34,
        (byte) 143,
        (byte) 65,
        (byte) 230,
        (byte) 176 /*0xB0*/,
        (byte) 30,
        (byte) 71,
        (byte) 208 /*0xD0*/,
        (byte) 21,
        (byte) 228,
        (byte) 174
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[16 /*0x10*/]
      {
        (byte) 231,
        (byte) 149,
        (byte) 221,
        (byte) 135,
        (byte) 244,
        (byte) 170,
        (byte) 155,
        (byte) 146,
        (byte) 75,
        (byte) 74,
        (byte) 177,
        (byte) 67,
        (byte) 63 /*0x3F*/,
        (byte) 206,
        (byte) 102,
        (byte) 186
      };
      byte[] numArray5 = new byte[16 /*0x10*/]
      {
        (byte) 90,
        (byte) 147,
        (byte) 203,
        (byte) 43,
        (byte) 86,
        (byte) 28,
        (byte) 252,
        (byte) 92,
        (byte) 37,
        (byte) 151,
        (byte) 216,
        (byte) 177,
        (byte) 95,
        (byte) 249,
        (byte) 211,
        (byte) 163
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 16 /*0x10*/);
      for (int index = 0; index < 16 /*0x10*/; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[71];
    byte[] numArray7 = new byte[55]
    {
      (byte) 90,
      (byte) 65,
      (byte) 10,
      (byte) 84,
      (byte) 103,
      (byte) 142,
      (byte) 98,
      (byte) 185,
      (byte) 95,
      (byte) 123,
      (byte) 229,
      (byte) 85,
      (byte) 199,
      (byte) 78,
      (byte) 92,
      (byte) 92,
      (byte) 98,
      (byte) 109,
      (byte) 191,
      (byte) 20,
      (byte) 120,
      (byte) 98,
      (byte) 42,
      (byte) 217,
      (byte) 242,
      (byte) 154,
      (byte) 57,
      (byte) 48 /*0x30*/,
      (byte) 63 /*0x3F*/,
      (byte) 4,
      (byte) 101,
      (byte) 200,
      (byte) 214,
      (byte) 89,
      (byte) 36,
      (byte) 146,
      (byte) 220,
      (byte) 64 /*0x40*/,
      (byte) 117,
      (byte) 248,
      (byte) 146,
      (byte) 240 /*0xF0*/,
      (byte) 59,
      (byte) 22,
      (byte) 102,
      (byte) 245,
      (byte) 234,
      (byte) 244,
      (byte) 226,
      (byte) 113,
      (byte) 4,
      (byte) 76,
      (byte) 183,
      (byte) 9,
      (byte) 6
    };
    byte[] numArray8 = new byte[55];
    numArray8[0] = (byte) 71;
    numArray8[1] = (byte) 98;
    numArray8[4] = (byte) 55;
    numArray8[3] = (byte) 34;
    numArray8[10] = (byte) 79;
    numArray8[45] = (byte) 123;
    numArray8[40] = (byte) 29;
    numArray8[22] = (byte) 225;
    numArray8[43] = (byte) 224 /*0xE0*/;
    numArray8[50] = (byte) 213;
    numArray8[12] = (byte) 248;
    numArray8[25] = (byte) 69;
    numArray8[9] = (byte) 50;
    numArray8[28] = (byte) 112 /*0x70*/;
    numArray8[51] = (byte) 202;
    numArray8[37] = (byte) 248;
    numArray8[16 /*0x10*/] = (byte) 13;
    numArray8[15] = (byte) 143;
    numArray8[18] = (byte) 112 /*0x70*/;
    numArray8[19] = (byte) 129;
    numArray8[20] = (byte) 181;
    numArray8[44] = (byte) 52;
    numArray8[13] = (byte) 192 /*0xC0*/;
    numArray8[5] = (byte) 13;
    numArray8[24] = (byte) 114;
    numArray8[47] = (byte) 105;
    numArray8[26] = (byte) 4;
    numArray8[27] = (byte) 15;
    numArray8[49] = (byte) 137;
    numArray8[29] = (byte) 99;
    numArray8[21] = (byte) 240 /*0xF0*/;
    numArray8[11] = (byte) 183;
    numArray8[7] = (byte) 200;
    numArray8[14] = (byte) 172;
    numArray8[34] = (byte) 180;
    numArray8[35] = (byte) 77;
    numArray8[36] = (byte) 38;
    numArray8[2] = (byte) 78;
    numArray8[38] = (byte) 77;
    numArray8[39] = (byte) 147;
    numArray8[6] = (byte) 94;
    numArray8[41] = (byte) 125;
    numArray8[42] = (byte) 55;
    numArray8[48 /*0x30*/] = (byte) 134;
    numArray8[23] = (byte) 73;
    numArray8[32 /*0x20*/] = (byte) 133;
    numArray8[46] = (byte) 161;
    numArray8[30] = (byte) 213;
    numArray8[8] = (byte) 59;
    numArray8[31 /*0x1F*/] = (byte) 38;
    numArray8[33] = (byte) 2;
    numArray8[17] = (byte) 72;
    numArray8[52] = (byte) 164;
    numArray8[53] = (byte) 103;
    numArray8[54] = (byte) 56;
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[16 /*0x10*/];
    numArray9[8] = (byte) 12;
    numArray9[1] = (byte) 101;
    numArray9[2] = (byte) 28;
    numArray9[3] = (byte) 16 /*0x10*/;
    numArray9[4] = (byte) 228;
    numArray9[11] = (byte) 253;
    numArray9[6] = (byte) 91;
    numArray9[7] = (byte) 254;
    numArray9[9] = (byte) 178;
    numArray9[0] = (byte) 190;
    numArray9[5] = (byte) 172;
    numArray9[12] = (byte) 78;
    numArray9[10] = (byte) 26;
    numArray9[13] = (byte) 213;
    numArray9[14] = (byte) 67;
    numArray9[15] = (byte) 78;
    byte[] numArray10 = new byte[16 /*0x10*/]
    {
      (byte) 15,
      (byte) 67,
      (byte) 136,
      (byte) 165,
      (byte) 143,
      (byte) 44,
      (byte) 121,
      (byte) 99,
      (byte) 228,
      (byte) 7,
      (byte) 204,
      (byte) 198,
      (byte) 180,
      (byte) 31 /*0x1F*/,
      (byte) 5,
      (byte) 170
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 16 /*0x10*/);
    for (int index = 0; index < 16 /*0x10*/; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_12585()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[160 /*0xA0*/];
      byte[] numArray2 = new byte[55];
      numArray2[1] = (byte) 147;
      numArray2[41] = (byte) 194;
      numArray2[8] = (byte) 98;
      numArray2[28] = (byte) 109;
      numArray2[51] = (byte) 211;
      numArray2[42] = (byte) 209;
      numArray2[6] = (byte) 60;
      numArray2[7] = (byte) 196;
      numArray2[2] = (byte) 123;
      numArray2[9] = (byte) 204;
      numArray2[33] = (byte) 202;
      numArray2[11] = (byte) 28;
      numArray2[24] = (byte) 208 /*0xD0*/;
      numArray2[13] = (byte) 6;
      numArray2[38] = (byte) 194;
      numArray2[15] = (byte) 102;
      numArray2[49] = (byte) 222;
      numArray2[17] = (byte) 137;
      numArray2[18] = (byte) 239;
      numArray2[36] = (byte) 166;
      numArray2[39] = (byte) 18;
      numArray2[21] = (byte) 233;
      numArray2[46] = (byte) 127 /*0x7F*/;
      numArray2[23] = (byte) 176 /*0xB0*/;
      numArray2[5] = (byte) 1;
      numArray2[3] = (byte) 202;
      numArray2[22] = (byte) 149;
      numArray2[27] = (byte) 237;
      numArray2[14] = (byte) 206;
      numArray2[29] = (byte) 69;
      numArray2[30] = (byte) 161;
      numArray2[31 /*0x1F*/] = (byte) 215;
      numArray2[34] = (byte) 193;
      numArray2[12] = (byte) 158;
      numArray2[26] = (byte) 58;
      numArray2[20] = (byte) 222;
      numArray2[25] = (byte) 61;
      numArray2[19] = (byte) 148;
      numArray2[43] = (byte) 173;
      numArray2[0] = (byte) 140;
      numArray2[40] = (byte) 238;
      numArray2[50] = (byte) 104;
      numArray2[16 /*0x10*/] = (byte) 108;
      numArray2[52] = (byte) 135;
      numArray2[44] = (byte) 105;
      numArray2[45] = (byte) 192 /*0xC0*/;
      numArray2[10] = (byte) 202;
      numArray2[47] = (byte) 189;
      numArray2[48 /*0x30*/] = (byte) 80 /*0x50*/;
      numArray2[32 /*0x20*/] = (byte) 233;
      numArray2[4] = (byte) 132;
      numArray2[35] = (byte) 142;
      numArray2[54] = (byte) 154;
      numArray2[53] = (byte) 88;
      numArray2[37] = (byte) 71;
      byte[] numArray3 = new byte[55]
      {
        (byte) 248,
        (byte) 113,
        (byte) 32 /*0x20*/,
        (byte) 80 /*0x50*/,
        (byte) 119,
        (byte) 56,
        (byte) 53,
        (byte) 10,
        (byte) 22,
        (byte) 26,
        (byte) 240 /*0xF0*/,
        (byte) 209,
        (byte) 128 /*0x80*/,
        (byte) 236,
        (byte) 235,
        (byte) 186,
        (byte) 154,
        (byte) 208 /*0xD0*/,
        (byte) 170,
        (byte) 200,
        (byte) 4,
        (byte) 104,
        (byte) 76,
        (byte) 32 /*0x20*/,
        (byte) 27,
        (byte) 248,
        (byte) 189,
        (byte) 87,
        byte.MaxValue,
        (byte) 118,
        (byte) 81,
        (byte) 109,
        (byte) 24,
        (byte) 117,
        (byte) 194,
        (byte) 169,
        (byte) 86,
        (byte) 111,
        (byte) 245,
        (byte) 253,
        (byte) 222,
        (byte) 85,
        (byte) 49,
        (byte) 146,
        (byte) 243,
        (byte) 128 /*0x80*/,
        (byte) 172,
        (byte) 162,
        (byte) 218,
        (byte) 177,
        (byte) 168,
        (byte) 187,
        (byte) 25,
        (byte) 74,
        (byte) 33
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55];
      numArray4[10] = (byte) 164;
      numArray4[1] = (byte) 234;
      numArray4[2] = (byte) 137;
      numArray4[52] = (byte) 206;
      numArray4[5] = (byte) 199;
      numArray4[47] = (byte) 56;
      numArray4[51] = (byte) 171;
      numArray4[50] = (byte) 25;
      numArray4[8] = (byte) 163;
      numArray4[9] = (byte) 162;
      numArray4[24] = (byte) 4;
      numArray4[36] = (byte) 241;
      numArray4[12] = (byte) 175;
      numArray4[13] = (byte) 229;
      numArray4[33] = (byte) 122;
      numArray4[15] = (byte) 76;
      numArray4[16 /*0x10*/] = (byte) 145;
      numArray4[20] = (byte) 102;
      numArray4[7] = (byte) 94;
      numArray4[11] = (byte) 39;
      numArray4[54] = (byte) 175;
      numArray4[21] = (byte) 207;
      numArray4[22] = (byte) 130;
      numArray4[30] = (byte) 222;
      numArray4[23] = (byte) 19;
      numArray4[25] = (byte) 139;
      numArray4[19] = (byte) 99;
      numArray4[27] = (byte) 154;
      numArray4[28] = (byte) 193;
      numArray4[29] = (byte) 164;
      numArray4[14] = (byte) 215;
      numArray4[31 /*0x1F*/] = (byte) 177;
      numArray4[32 /*0x20*/] = (byte) 145;
      numArray4[6] = (byte) 62;
      numArray4[26] = (byte) 113;
      numArray4[35] = (byte) 99;
      numArray4[0] = (byte) 132;
      numArray4[3] = (byte) 74;
      numArray4[38] = (byte) 237;
      numArray4[34] = (byte) 86;
      numArray4[40] = (byte) 117;
      numArray4[41] = (byte) 35;
      numArray4[42] = (byte) 211;
      numArray4[43] = (byte) 235;
      numArray4[46] = (byte) 98;
      numArray4[49] = (byte) 192 /*0xC0*/;
      numArray4[44] = (byte) 7;
      numArray4[18] = (byte) 176 /*0xB0*/;
      numArray4[48 /*0x30*/] = (byte) 108;
      numArray4[37] = (byte) 1;
      numArray4[17] = (byte) 6;
      numArray4[39] = (byte) 163;
      numArray4[4] = (byte) 177;
      numArray4[53] = (byte) 40;
      numArray4[45] = (byte) 81;
      byte[] numArray5 = new byte[55];
      numArray5[21] = (byte) 83;
      numArray5[4] = (byte) 66;
      numArray5[2] = (byte) 21;
      numArray5[3] = (byte) 212;
      numArray5[46] = (byte) 145;
      numArray5[5] = (byte) 100;
      numArray5[6] = (byte) 155;
      numArray5[53] = (byte) 177;
      numArray5[0] = (byte) 73;
      numArray5[9] = (byte) 13;
      numArray5[10] = (byte) 231;
      numArray5[11] = (byte) 39;
      numArray5[45] = (byte) 222;
      numArray5[13] = (byte) 56;
      numArray5[35] = (byte) 205;
      numArray5[54] = (byte) 203;
      numArray5[20] = (byte) 41;
      numArray5[14] = (byte) 100;
      numArray5[18] = (byte) 125;
      numArray5[19] = (byte) 198;
      numArray5[26] = (byte) 234;
      numArray5[49] = (byte) 232;
      numArray5[52] = (byte) 205;
      numArray5[23] = (byte) 57;
      numArray5[32 /*0x20*/] = (byte) 137;
      numArray5[25] = (byte) 209;
      numArray5[36] = (byte) 183;
      numArray5[27] = (byte) 74;
      numArray5[28] = (byte) 104;
      numArray5[29] = (byte) 167;
      numArray5[30] = (byte) 13;
      numArray5[31 /*0x1F*/] = (byte) 13;
      numArray5[41] = (byte) 196;
      numArray5[33] = (byte) 76;
      numArray5[34] = (byte) 155;
      numArray5[16 /*0x10*/] = (byte) 129;
      numArray5[47] = (byte) 210;
      numArray5[7] = (byte) 135;
      numArray5[38] = (byte) 252;
      numArray5[24] = (byte) 56;
      numArray5[37] = (byte) 226;
      numArray5[12] = (byte) 107;
      numArray5[42] = (byte) 209;
      numArray5[43] = (byte) 190;
      numArray5[44] = (byte) 78;
      numArray5[15] = (byte) 30;
      numArray5[22] = (byte) 118;
      numArray5[39] = (byte) 201;
      numArray5[48 /*0x30*/] = (byte) 104;
      numArray5[40] = (byte) 125;
      numArray5[50] = (byte) 244;
      numArray5[51] = (byte) 159;
      numArray5[1] = (byte) 161;
      numArray5[8] = (byte) 198;
      numArray5[17] = (byte) 246;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[50]
      {
        (byte) 114,
        (byte) 156,
        (byte) 1,
        (byte) 142,
        (byte) 64 /*0x40*/,
        (byte) 0,
        (byte) 116,
        (byte) 240 /*0xF0*/,
        (byte) 238,
        (byte) 254,
        (byte) 12,
        (byte) 2,
        (byte) 76,
        (byte) 206,
        (byte) 171,
        (byte) 133,
        (byte) 143,
        (byte) 194,
        (byte) 208 /*0xD0*/,
        (byte) 200,
        (byte) 215,
        (byte) 243,
        (byte) 102,
        (byte) 55,
        (byte) 209,
        (byte) 182,
        (byte) 21,
        (byte) 73,
        (byte) 92,
        (byte) 32 /*0x20*/,
        (byte) 15,
        (byte) 170,
        (byte) 90,
        (byte) 203,
        (byte) 158,
        (byte) 140,
        (byte) 201,
        (byte) 161,
        (byte) 25,
        (byte) 234,
        (byte) 235,
        (byte) 167,
        (byte) 21,
        (byte) 145,
        (byte) 182,
        (byte) 186,
        (byte) 50,
        (byte) 183,
        (byte) 11,
        (byte) 189
      };
      byte[] numArray7 = new byte[50];
      numArray7[11] = (byte) 131;
      numArray7[1] = (byte) 170;
      numArray7[6] = (byte) 27;
      numArray7[3] = (byte) 231;
      numArray7[4] = (byte) 241;
      numArray7[5] = (byte) 93;
      numArray7[17] = (byte) 9;
      numArray7[31 /*0x1F*/] = (byte) 74;
      numArray7[44] = (byte) 150;
      numArray7[33] = (byte) 226;
      numArray7[21] = (byte) 60;
      numArray7[30] = (byte) 211;
      numArray7[47] = (byte) 19;
      numArray7[0] = (byte) 26;
      numArray7[14] = (byte) 125;
      numArray7[15] = (byte) 113;
      numArray7[48 /*0x30*/] = (byte) 22;
      numArray7[16 /*0x10*/] = (byte) 111;
      numArray7[18] = (byte) 89;
      numArray7[19] = (byte) 3;
      numArray7[26] = (byte) 232;
      numArray7[35] = (byte) 25;
      numArray7[13] = (byte) 25;
      numArray7[38] = (byte) 74;
      numArray7[24] = (byte) 108;
      numArray7[2] = (byte) 10;
      numArray7[8] = (byte) 229;
      numArray7[27] = (byte) 51;
      numArray7[25] = (byte) 102;
      numArray7[29] = (byte) 226;
      numArray7[32 /*0x20*/] = (byte) 188;
      numArray7[20] = (byte) 220;
      numArray7[12] = (byte) 222;
      numArray7[36] = (byte) 56;
      numArray7[7] = (byte) 125;
      numArray7[40] = (byte) 65;
      numArray7[22] = (byte) 187;
      numArray7[9] = (byte) 241;
      numArray7[28] = (byte) 175;
      numArray7[39] = (byte) 49;
      numArray7[37] = (byte) 85;
      numArray7[41] = (byte) 167;
      numArray7[42] = (byte) 216;
      numArray7[43] = (byte) 3;
      numArray7[34] = (byte) 107;
      numArray7[45] = (byte) 124;
      numArray7[46] = (byte) 157;
      numArray7[23] = (byte) 131;
      numArray7[10] = (byte) 120;
      numArray7[49] = (byte) 74;
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 50);
      for (int index = 0; index < 50; ++index)
        numArray1[index + 110] ^= numArray7[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray8 = new byte[160 /*0xA0*/];
    byte[] numArray9 = new byte[55]
    {
      (byte) 237,
      (byte) 0,
      (byte) 132,
      (byte) 178,
      (byte) 252,
      (byte) 235,
      (byte) 88,
      (byte) 160 /*0xA0*/,
      (byte) 96 /*0x60*/,
      (byte) 72,
      (byte) 165,
      (byte) 5,
      (byte) 140,
      (byte) 237,
      (byte) 243,
      (byte) 32 /*0x20*/,
      (byte) 58,
      (byte) 154,
      (byte) 22,
      (byte) 60,
      (byte) 14,
      (byte) 199,
      (byte) 187,
      (byte) 8,
      (byte) 24,
      (byte) 229,
      (byte) 27,
      (byte) 27,
      (byte) 150,
      (byte) 50,
      (byte) 44,
      (byte) 5,
      (byte) 210,
      (byte) 213,
      (byte) 80 /*0x50*/,
      (byte) 158,
      (byte) 40,
      (byte) 1,
      (byte) 203,
      (byte) 167,
      (byte) 235,
      (byte) 127 /*0x7F*/,
      (byte) 246,
      (byte) 183,
      (byte) 63 /*0x3F*/,
      (byte) 139,
      (byte) 115,
      (byte) 212,
      (byte) 32 /*0x20*/,
      (byte) 39,
      (byte) 104,
      (byte) 186,
      (byte) 251,
      (byte) 117,
      (byte) 121
    };
    byte[] numArray10 = new byte[55];
    numArray10[9] = (byte) 146;
    numArray10[31 /*0x1F*/] = (byte) 10;
    numArray10[44] = (byte) 38;
    numArray10[27] = (byte) 98;
    numArray10[40] = (byte) 60;
    numArray10[39] = (byte) 108;
    numArray10[6] = (byte) 82;
    numArray10[47] = (byte) 132;
    numArray10[14] = (byte) 29;
    numArray10[8] = (byte) 227;
    numArray10[10] = (byte) 69;
    numArray10[26] = (byte) 206;
    numArray10[12] = (byte) 186;
    numArray10[13] = (byte) 240 /*0xF0*/;
    numArray10[30] = (byte) 221;
    numArray10[18] = (byte) 52;
    numArray10[7] = (byte) 0;
    numArray10[38] = (byte) 139;
    numArray10[4] = (byte) 210;
    numArray10[19] = (byte) 137;
    numArray10[20] = (byte) 136;
    numArray10[21] = (byte) 169;
    numArray10[22] = (byte) 146;
    numArray10[23] = (byte) 51;
    numArray10[24] = (byte) 60;
    numArray10[0] = (byte) 161;
    numArray10[11] = (byte) 191;
    numArray10[34] = (byte) 100;
    numArray10[17] = (byte) 187;
    numArray10[53] = (byte) 64 /*0x40*/;
    numArray10[48 /*0x30*/] = (byte) 39;
    numArray10[41] = (byte) 205;
    numArray10[32 /*0x20*/] = (byte) 136;
    numArray10[29] = (byte) 228;
    numArray10[16 /*0x10*/] = (byte) 212;
    numArray10[35] = (byte) 216;
    numArray10[36] = (byte) 34;
    numArray10[37] = (byte) 209;
    numArray10[15] = (byte) 44;
    numArray10[1] = (byte) 8;
    numArray10[33] = (byte) 31 /*0x1F*/;
    numArray10[2] = (byte) 216;
    numArray10[3] = (byte) 167;
    numArray10[43] = (byte) 77;
    numArray10[54] = (byte) 123;
    numArray10[45] = (byte) 145;
    numArray10[46] = (byte) 167;
    numArray10[28] = (byte) 132;
    numArray10[52] = (byte) 9;
    numArray10[49] = (byte) 113;
    numArray10[50] = (byte) 14;
    numArray10[5] = (byte) 134;
    numArray10[51] = (byte) 62;
    numArray10[25] = (byte) 235;
    numArray10[42] = (byte) 236;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray8, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index] ^= numArray10[index];
    byte[] numArray11 = new byte[55];
    numArray11[11] = (byte) 113;
    numArray11[21] = (byte) 41;
    numArray11[13] = (byte) 74;
    numArray11[6] = (byte) 100;
    numArray11[53] = (byte) 75;
    numArray11[5] = (byte) 156;
    numArray11[22] = (byte) 53;
    numArray11[32 /*0x20*/] = (byte) 214;
    numArray11[36] = (byte) 13;
    numArray11[16 /*0x10*/] = (byte) 151;
    numArray11[10] = (byte) 92;
    numArray11[1] = (byte) 12;
    numArray11[12] = (byte) 251;
    numArray11[43] = (byte) 129;
    numArray11[20] = (byte) 104;
    numArray11[4] = (byte) 64 /*0x40*/;
    numArray11[30] = (byte) 56;
    numArray11[17] = (byte) 161;
    numArray11[18] = (byte) 27;
    numArray11[19] = (byte) 8;
    numArray11[42] = (byte) 8;
    numArray11[34] = (byte) 250;
    numArray11[52] = (byte) 206;
    numArray11[44] = (byte) 95;
    numArray11[3] = (byte) 136;
    numArray11[9] = (byte) 214;
    numArray11[26] = (byte) 63 /*0x3F*/;
    numArray11[27] = (byte) 154;
    numArray11[28] = (byte) 134;
    numArray11[15] = (byte) 27;
    numArray11[8] = (byte) 150;
    numArray11[31 /*0x1F*/] = (byte) 113;
    numArray11[0] = (byte) 178;
    numArray11[23] = (byte) 181;
    numArray11[2] = (byte) 166;
    numArray11[37] = (byte) 78;
    numArray11[33] = (byte) 25;
    numArray11[25] = (byte) 48 /*0x30*/;
    numArray11[38] = (byte) 110;
    numArray11[39] = (byte) 216;
    numArray11[40] = (byte) 100;
    numArray11[41] = (byte) 241;
    numArray11[35] = (byte) 91;
    numArray11[29] = (byte) 124;
    numArray11[24] = (byte) 15;
    numArray11[45] = (byte) 198;
    numArray11[46] = (byte) 208 /*0xD0*/;
    numArray11[47] = (byte) 229;
    numArray11[48 /*0x30*/] = (byte) 93;
    numArray11[49] = (byte) 208 /*0xD0*/;
    numArray11[50] = (byte) 244;
    numArray11[51] = (byte) 130;
    numArray11[14] = (byte) 209;
    numArray11[7] = (byte) 170;
    numArray11[54] = (byte) 215;
    byte[] numArray12 = new byte[55]
    {
      (byte) 110,
      (byte) 232,
      (byte) 117,
      (byte) 120,
      (byte) 113,
      (byte) 124,
      (byte) 233,
      (byte) 195,
      (byte) 180,
      (byte) 127 /*0x7F*/,
      (byte) 120,
      (byte) 66,
      (byte) 206,
      (byte) 56,
      (byte) 25,
      (byte) 44,
      (byte) 73,
      (byte) 190,
      (byte) 85,
      (byte) 182,
      (byte) 160 /*0xA0*/,
      (byte) 174,
      (byte) 166,
      (byte) 182,
      (byte) 107,
      (byte) 176 /*0xB0*/,
      (byte) 203,
      (byte) 195,
      (byte) 238,
      (byte) 209,
      (byte) 213,
      (byte) 243,
      (byte) 10,
      (byte) 65,
      (byte) 236,
      (byte) 168,
      (byte) 233,
      (byte) 86,
      (byte) 120,
      (byte) 5,
      (byte) 113,
      (byte) 143,
      (byte) 105,
      (byte) 109,
      (byte) 48 /*0x30*/,
      (byte) 176 /*0xB0*/,
      (byte) 238,
      (byte) 125,
      (byte) 176 /*0xB0*/,
      (byte) 232,
      (byte) 104,
      (byte) 117,
      (byte) 62,
      (byte) 160 /*0xA0*/,
      (byte) 189
    };
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray8, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index + 55] ^= numArray12[index];
    byte[] numArray13 = new byte[50];
    numArray13[27] = (byte) 220;
    numArray13[1] = (byte) 47;
    numArray13[9] = (byte) 15;
    numArray13[3] = (byte) 3;
    numArray13[4] = (byte) 142;
    numArray13[21] = (byte) 224 /*0xE0*/;
    numArray13[6] = (byte) 145;
    numArray13[7] = (byte) 231;
    numArray13[41] = (byte) 252;
    numArray13[17] = (byte) 11;
    numArray13[10] = (byte) 160 /*0xA0*/;
    numArray13[29] = (byte) 23;
    numArray13[11] = (byte) 222;
    numArray13[43] = (byte) 28;
    numArray13[14] = (byte) 116;
    numArray13[26] = (byte) 243;
    numArray13[19] = (byte) 50;
    numArray13[25] = (byte) 84;
    numArray13[18] = (byte) 169;
    numArray13[8] = (byte) 71;
    numArray13[20] = (byte) 208 /*0xD0*/;
    numArray13[30] = (byte) 227;
    numArray13[22] = (byte) 29;
    numArray13[23] = (byte) 113;
    numArray13[16 /*0x10*/] = (byte) 200;
    numArray13[0] = (byte) 154;
    numArray13[15] = (byte) 212;
    numArray13[47] = (byte) 208 /*0xD0*/;
    numArray13[28] = (byte) 102;
    numArray13[12] = (byte) 241;
    numArray13[36] = (byte) 173;
    numArray13[31 /*0x1F*/] = (byte) 224 /*0xE0*/;
    numArray13[33] = (byte) 98;
    numArray13[37] = (byte) 119;
    numArray13[34] = (byte) 129;
    numArray13[35] = (byte) 165;
    numArray13[38] = (byte) 11;
    numArray13[44] = (byte) 77;
    numArray13[13] = (byte) 241;
    numArray13[48 /*0x30*/] = (byte) 55;
    numArray13[40] = (byte) 93;
    numArray13[5] = (byte) 109;
    numArray13[39] = (byte) 96 /*0x60*/;
    numArray13[24] = (byte) 196;
    numArray13[42] = (byte) 45;
    numArray13[45] = (byte) 182;
    numArray13[46] = (byte) 245;
    numArray13[32 /*0x20*/] = (byte) 10;
    numArray13[2] = (byte) 176 /*0xB0*/;
    numArray13[49] = (byte) 123;
    byte[] numArray14 = new byte[50]
    {
      (byte) 4,
      (byte) 175,
      (byte) 134,
      (byte) 246,
      (byte) 221,
      (byte) 41,
      (byte) 23,
      (byte) 24,
      (byte) 171,
      (byte) 231,
      (byte) 67,
      (byte) 91,
      (byte) 252,
      (byte) 150,
      (byte) 226,
      (byte) 19,
      (byte) 45,
      (byte) 148,
      (byte) 73,
      (byte) 41,
      (byte) 190,
      (byte) 147,
      (byte) 222,
      (byte) 212,
      (byte) 96 /*0x60*/,
      (byte) 193,
      (byte) 137,
      (byte) 236,
      (byte) 3,
      (byte) 204,
      (byte) 124,
      (byte) 175,
      (byte) 198,
      (byte) 139,
      (byte) 143,
      (byte) 39,
      (byte) 48 /*0x30*/,
      (byte) 152,
      (byte) 51,
      (byte) 206,
      (byte) 235,
      (byte) 233,
      (byte) 250,
      (byte) 248,
      (byte) 176 /*0xB0*/,
      (byte) 126,
      (byte) 213,
      (byte) 152,
      (byte) 222,
      (byte) 20
    };
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray8, 110, 50);
    for (int index = 0; index < 50; ++index)
      numArray8[index + 110] ^= numArray14[index];
    return Encoding.UTF8.GetString(numArray8);
  }
}
