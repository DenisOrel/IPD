// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_13291
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_13291
{
  private static byte[] sspq = new byte[36]
  {
    (byte) 29,
    (byte) 151,
    (byte) 198,
    (byte) 145,
    (byte) 99,
    (byte) 63 /*0x3F*/,
    (byte) 149,
    (byte) 137,
    (byte) 26,
    (byte) 193,
    (byte) 183,
    (byte) 225,
    (byte) 185,
    (byte) 126,
    (byte) 82,
    (byte) 19,
    (byte) 109,
    (byte) 56,
    (byte) 205,
    (byte) 21,
    (byte) 200,
    (byte) 196,
    (byte) 109,
    (byte) 48 /*0x30*/,
    (byte) 184,
    (byte) 190,
    (byte) 234,
    (byte) 231,
    (byte) 2,
    (byte) 11,
    (byte) 117,
    (byte) 8,
    (byte) 123,
    (byte) 241,
    (byte) 178,
    (byte) 92
  };
  private static byte[] sspr = new byte[36]
  {
    (byte) 54,
    (byte) 49,
    (byte) 156,
    (byte) 227,
    (byte) 91,
    (byte) 199,
    (byte) 19,
    (byte) 121,
    (byte) 240 /*0xF0*/,
    (byte) 116,
    (byte) 153,
    (byte) 156,
    (byte) 33,
    (byte) 46,
    (byte) 144 /*0x90*/,
    (byte) 31 /*0x1F*/,
    (byte) 124,
    (byte) 14,
    (byte) 218,
    (byte) 176 /*0xB0*/,
    (byte) 218,
    (byte) 126,
    (byte) 155,
    (byte) 119,
    (byte) 222,
    (byte) 152,
    (byte) 130,
    (byte) 222,
    (byte) 28,
    (byte) 53,
    (byte) 8,
    (byte) 111,
    (byte) 38,
    (byte) 125,
    (byte) 135,
    (byte) 211
  };

  internal static string ssp_appserver_13292()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[251];
      byte[] numArray2 = new byte[55]
      {
        (byte) 208 /*0xD0*/,
        (byte) 124,
        (byte) 155,
        (byte) 158,
        (byte) 205,
        (byte) 241,
        (byte) 46,
        (byte) 226,
        (byte) 180,
        (byte) 9,
        (byte) 139,
        (byte) 112 /*0x70*/,
        (byte) 113,
        (byte) 167,
        (byte) 163,
        (byte) 119,
        (byte) 40,
        (byte) 163,
        (byte) 68,
        (byte) 194,
        (byte) 94,
        (byte) 216,
        (byte) 114,
        (byte) 76,
        (byte) 45,
        (byte) 49,
        (byte) 105,
        (byte) 3,
        (byte) 44,
        (byte) 249,
        (byte) 151,
        (byte) 140,
        (byte) 137,
        (byte) 167,
        (byte) 21,
        (byte) 98,
        (byte) 220,
        (byte) 219,
        (byte) 128 /*0x80*/,
        (byte) 3,
        (byte) 174,
        (byte) 211,
        byte.MaxValue,
        (byte) 144 /*0x90*/,
        (byte) 144 /*0x90*/,
        (byte) 38,
        (byte) 240 /*0xF0*/,
        (byte) 30,
        (byte) 158,
        (byte) 100,
        (byte) 179,
        (byte) 80 /*0x50*/,
        (byte) 193,
        (byte) 181,
        (byte) 128 /*0x80*/
      };
      byte[] numArray3 = new byte[55];
      numArray3[44] = (byte) 66;
      numArray3[1] = (byte) 232;
      numArray3[2] = (byte) 116;
      numArray3[33] = (byte) 19;
      numArray3[36] = (byte) 170;
      numArray3[5] = (byte) 192 /*0xC0*/;
      numArray3[0] = (byte) 172;
      numArray3[10] = (byte) 225;
      numArray3[13] = (byte) 253;
      numArray3[21] = (byte) 48 /*0x30*/;
      numArray3[49] = (byte) 214;
      numArray3[23] = (byte) 254;
      numArray3[12] = (byte) 27;
      numArray3[51] = (byte) 118;
      numArray3[34] = (byte) 51;
      numArray3[25] = (byte) 80 /*0x50*/;
      numArray3[11] = (byte) 248;
      numArray3[17] = (byte) 252;
      numArray3[32 /*0x20*/] = (byte) 171;
      numArray3[19] = (byte) 135;
      numArray3[20] = (byte) 71;
      numArray3[43] = (byte) 178;
      numArray3[41] = (byte) 168;
      numArray3[53] = (byte) 164;
      numArray3[24] = (byte) 47;
      numArray3[7] = (byte) 221;
      numArray3[16 /*0x10*/] = (byte) 197;
      numArray3[27] = (byte) 222;
      numArray3[28] = (byte) 223;
      numArray3[3] = (byte) 64 /*0x40*/;
      numArray3[30] = (byte) 211;
      numArray3[31 /*0x1F*/] = (byte) 4;
      numArray3[6] = (byte) 31 /*0x1F*/;
      numArray3[15] = (byte) 8;
      numArray3[8] = (byte) 251;
      numArray3[35] = (byte) 10;
      numArray3[46] = (byte) 92;
      numArray3[37] = (byte) 34;
      numArray3[38] = (byte) 184;
      numArray3[39] = (byte) 222;
      numArray3[22] = (byte) 150;
      numArray3[26] = (byte) 128 /*0x80*/;
      numArray3[40] = (byte) 62;
      numArray3[45] = (byte) 41;
      numArray3[50] = (byte) 13;
      numArray3[4] = (byte) 82;
      numArray3[14] = (byte) 75;
      numArray3[47] = (byte) 180;
      numArray3[48 /*0x30*/] = (byte) 113;
      numArray3[42] = (byte) 163;
      numArray3[18] = (byte) 152;
      numArray3[29] = (byte) 19;
      numArray3[52] = (byte) 116;
      numArray3[9] = (byte) 112 /*0x70*/;
      numArray3[54] = (byte) 169;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 145,
        (byte) 105,
        (byte) 90,
        (byte) 62,
        (byte) 196,
        (byte) 237,
        (byte) 117,
        (byte) 43,
        (byte) 51,
        (byte) 18,
        (byte) 162,
        (byte) 116,
        (byte) 232,
        (byte) 11,
        (byte) 226,
        (byte) 61,
        (byte) 197,
        (byte) 143,
        (byte) 109,
        (byte) 148,
        (byte) 196,
        (byte) 42,
        (byte) 198,
        (byte) 27,
        (byte) 189,
        (byte) 7,
        (byte) 253,
        (byte) 222,
        (byte) 24,
        (byte) 75,
        (byte) 49,
        (byte) 50,
        (byte) 254,
        (byte) 49,
        (byte) 252,
        (byte) 0,
        (byte) 138,
        (byte) 60,
        (byte) 191,
        (byte) 47,
        (byte) 206,
        (byte) 241,
        (byte) 94,
        (byte) 46,
        (byte) 131,
        (byte) 24,
        (byte) 117,
        (byte) 191,
        (byte) 131,
        (byte) 240 /*0xF0*/,
        (byte) 186,
        (byte) 18,
        (byte) 135,
        (byte) 138,
        (byte) 98
      };
      byte[] numArray5 = new byte[55]
      {
        (byte) 164,
        (byte) 150,
        (byte) 2,
        (byte) 238,
        (byte) 98,
        (byte) 250,
        (byte) 219,
        (byte) 100,
        (byte) 209,
        (byte) 117,
        (byte) 212,
        (byte) 220,
        (byte) 129,
        (byte) 239,
        (byte) 211,
        (byte) 38,
        (byte) 102,
        (byte) 209,
        (byte) 111,
        byte.MaxValue,
        (byte) 45,
        (byte) 9,
        (byte) 40,
        (byte) 233,
        (byte) 183,
        (byte) 247,
        (byte) 169,
        (byte) 28,
        (byte) 104,
        (byte) 177,
        (byte) 138,
        (byte) 175,
        (byte) 218,
        (byte) 14,
        (byte) 53,
        (byte) 157,
        (byte) 76,
        (byte) 124,
        (byte) 220,
        (byte) 236,
        (byte) 57,
        (byte) 125,
        (byte) 57,
        (byte) 30,
        (byte) 206,
        (byte) 59,
        (byte) 40,
        (byte) 78,
        (byte) 112 /*0x70*/,
        (byte) 112 /*0x70*/,
        (byte) 133,
        (byte) 223,
        (byte) 43,
        (byte) 20,
        (byte) 232
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[55];
      numArray6[48 /*0x30*/] = (byte) 48 /*0x30*/;
      numArray6[1] = (byte) 128 /*0x80*/;
      numArray6[2] = (byte) 188;
      numArray6[41] = (byte) 77;
      numArray6[13] = (byte) 178;
      numArray6[51] = (byte) 195;
      numArray6[38] = (byte) 55;
      numArray6[26] = (byte) 40;
      numArray6[6] = (byte) 231;
      numArray6[34] = (byte) 75;
      numArray6[15] = (byte) 208 /*0xD0*/;
      numArray6[11] = (byte) 14;
      numArray6[12] = (byte) 225;
      numArray6[53] = (byte) 155;
      numArray6[14] = (byte) 137;
      numArray6[9] = (byte) 118;
      numArray6[16 /*0x10*/] = (byte) 61;
      numArray6[45] = (byte) 176 /*0xB0*/;
      numArray6[18] = (byte) 211;
      numArray6[33] = (byte) 93;
      numArray6[20] = (byte) 152;
      numArray6[21] = (byte) 152;
      numArray6[42] = (byte) 18;
      numArray6[23] = (byte) 146;
      numArray6[24] = (byte) 62;
      numArray6[4] = (byte) 150;
      numArray6[25] = (byte) 90;
      numArray6[37] = (byte) 63 /*0x3F*/;
      numArray6[3] = (byte) 239;
      numArray6[29] = (byte) 171;
      numArray6[30] = (byte) 115;
      numArray6[17] = (byte) 198;
      numArray6[32 /*0x20*/] = (byte) 149;
      numArray6[39] = (byte) 223;
      numArray6[52] = (byte) 143;
      numArray6[35] = (byte) 188;
      numArray6[36] = (byte) 2;
      numArray6[50] = (byte) 73;
      numArray6[27] = (byte) 145;
      numArray6[7] = (byte) 224 /*0xE0*/;
      numArray6[40] = (byte) 118;
      numArray6[22] = (byte) 165;
      numArray6[8] = (byte) 112 /*0x70*/;
      numArray6[43] = (byte) 161;
      numArray6[44] = (byte) 190;
      numArray6[0] = (byte) 63 /*0x3F*/;
      numArray6[19] = (byte) 203;
      numArray6[47] = (byte) 61;
      numArray6[28] = (byte) 31 /*0x1F*/;
      numArray6[49] = (byte) 65;
      numArray6[54] = (byte) 10;
      numArray6[5] = (byte) 18;
      numArray6[46] = (byte) 162;
      numArray6[31 /*0x1F*/] = (byte) 206;
      numArray6[10] = (byte) 183;
      byte[] numArray7 = new byte[55];
      numArray7[37] = (byte) 95;
      numArray7[1] = (byte) 202;
      numArray7[18] = (byte) 30;
      numArray7[24] = (byte) 103;
      numArray7[4] = (byte) 77;
      numArray7[5] = (byte) 229;
      numArray7[26] = (byte) 119;
      numArray7[41] = (byte) 127 /*0x7F*/;
      numArray7[46] = (byte) 13;
      numArray7[9] = (byte) 133;
      numArray7[10] = (byte) 54;
      numArray7[11] = (byte) 58;
      numArray7[30] = (byte) 13;
      numArray7[13] = (byte) 30;
      numArray7[0] = (byte) 92;
      numArray7[49] = (byte) 232;
      numArray7[48 /*0x30*/] = (byte) 10;
      numArray7[47] = (byte) 249;
      numArray7[20] = (byte) 245;
      numArray7[19] = (byte) 251;
      numArray7[40] = (byte) 73;
      numArray7[12] = (byte) 213;
      numArray7[22] = (byte) 30;
      numArray7[44] = (byte) 228;
      numArray7[45] = (byte) 143;
      numArray7[2] = (byte) 168;
      numArray7[8] = (byte) 204;
      numArray7[27] = (byte) 152;
      numArray7[25] = (byte) 241;
      numArray7[29] = (byte) 229;
      numArray7[3] = (byte) 64 /*0x40*/;
      numArray7[31 /*0x1F*/] = (byte) 34;
      numArray7[32 /*0x20*/] = (byte) 205;
      numArray7[33] = (byte) 33;
      numArray7[38] = (byte) 111;
      numArray7[35] = (byte) 123;
      numArray7[15] = (byte) 139;
      numArray7[17] = (byte) 54;
      numArray7[53] = (byte) 243;
      numArray7[39] = (byte) 218;
      numArray7[28] = (byte) 119;
      numArray7[23] = (byte) 104;
      numArray7[42] = (byte) 226;
      numArray7[16 /*0x10*/] = (byte) 1;
      numArray7[6] = (byte) 4;
      numArray7[7] = (byte) 231;
      numArray7[21] = (byte) 53;
      numArray7[34] = (byte) 199;
      numArray7[14] = (byte) 153;
      numArray7[43] = (byte) 111;
      numArray7[50] = (byte) 140;
      numArray7[51] = (byte) 119;
      numArray7[52] = (byte) 170;
      numArray7[36] = (byte) 124;
      numArray7[54] = (byte) 201;
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[55];
      numArray8[31 /*0x1F*/] = (byte) 145;
      numArray8[43] = (byte) 237;
      numArray8[45] = (byte) 211;
      numArray8[44] = (byte) 79;
      numArray8[40] = (byte) 155;
      numArray8[24] = (byte) 2;
      numArray8[0] = (byte) 87;
      numArray8[47] = (byte) 9;
      numArray8[8] = (byte) 3;
      numArray8[9] = (byte) 203;
      numArray8[15] = (byte) 26;
      numArray8[3] = (byte) 233;
      numArray8[12] = (byte) 224 /*0xE0*/;
      numArray8[28] = (byte) 216;
      numArray8[14] = (byte) 3;
      numArray8[20] = (byte) 25;
      numArray8[16 /*0x10*/] = (byte) 9;
      numArray8[19] = (byte) 80 /*0x50*/;
      numArray8[21] = (byte) 208 /*0xD0*/;
      numArray8[5] = (byte) 121;
      numArray8[25] = (byte) 131;
      numArray8[54] = (byte) 87;
      numArray8[22] = (byte) 157;
      numArray8[23] = (byte) 172;
      numArray8[2] = (byte) 232;
      numArray8[27] = (byte) 58;
      numArray8[7] = (byte) 216;
      numArray8[53] = (byte) 150;
      numArray8[4] = (byte) 249;
      numArray8[41] = (byte) 114;
      numArray8[30] = (byte) 163;
      numArray8[51] = (byte) 4;
      numArray8[32 /*0x20*/] = (byte) 156;
      numArray8[33] = (byte) 152;
      numArray8[34] = (byte) 43;
      numArray8[35] = (byte) 158;
      numArray8[18] = (byte) 20;
      numArray8[37] = (byte) 12;
      numArray8[11] = (byte) 63 /*0x3F*/;
      numArray8[17] = (byte) 11;
      numArray8[13] = (byte) 115;
      numArray8[29] = (byte) 9;
      numArray8[42] = (byte) 42;
      numArray8[10] = (byte) 157;
      numArray8[6] = (byte) 76;
      numArray8[38] = (byte) 47;
      numArray8[46] = (byte) 151;
      numArray8[52] = (byte) 237;
      numArray8[48 /*0x30*/] = (byte) 215;
      numArray8[49] = (byte) 136;
      numArray8[50] = (byte) 146;
      numArray8[26] = (byte) 236;
      numArray8[36] = (byte) 144 /*0x90*/;
      numArray8[1] = (byte) 205;
      numArray8[39] = (byte) 47;
      byte[] numArray9 = new byte[55]
      {
        (byte) 4,
        (byte) 83,
        (byte) 76,
        (byte) 204,
        (byte) 96 /*0x60*/,
        (byte) 63 /*0x3F*/,
        (byte) 107,
        (byte) 125,
        (byte) 177,
        (byte) 139,
        (byte) 47,
        (byte) 118,
        (byte) 178,
        (byte) 252,
        (byte) 125,
        (byte) 20,
        byte.MaxValue,
        (byte) 156,
        (byte) 14,
        (byte) 107,
        (byte) 72,
        byte.MaxValue,
        (byte) 245,
        (byte) 14,
        (byte) 205,
        (byte) 53,
        (byte) 101,
        (byte) 179,
        (byte) 221,
        (byte) 181,
        (byte) 54,
        (byte) 205,
        (byte) 46,
        (byte) 205,
        (byte) 38,
        (byte) 134,
        (byte) 23,
        (byte) 58,
        (byte) 67,
        (byte) 78,
        (byte) 244,
        (byte) 200,
        (byte) 27,
        (byte) 194,
        (byte) 76,
        (byte) 22,
        (byte) 148,
        (byte) 39,
        (byte) 138,
        (byte) 56,
        (byte) 132,
        (byte) 203,
        (byte) 83,
        (byte) 12,
        (byte) 38
      };
      key.Query(true, 335, numArray8, numArray8);
      Array.Copy((Array) numArray8, 0, (Array) numArray1, 165, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 165] ^= numArray9[index];
      byte[] numArray10 = new byte[31 /*0x1F*/];
      numArray10[14] = (byte) 215;
      numArray10[1] = (byte) 183;
      numArray10[2] = (byte) 137;
      numArray10[11] = (byte) 163;
      numArray10[4] = (byte) 175;
      numArray10[3] = (byte) 170;
      numArray10[6] = (byte) 131;
      numArray10[25] = (byte) 240 /*0xF0*/;
      numArray10[8] = (byte) 104;
      numArray10[9] = (byte) 157;
      numArray10[22] = byte.MaxValue;
      numArray10[10] = (byte) 6;
      numArray10[16 /*0x10*/] = (byte) 66;
      numArray10[7] = (byte) 137;
      numArray10[20] = (byte) 235;
      numArray10[15] = (byte) 34;
      numArray10[17] = (byte) 43;
      numArray10[29] = (byte) 156;
      numArray10[18] = (byte) 53;
      numArray10[19] = (byte) 118;
      numArray10[12] = (byte) 246;
      numArray10[26] = (byte) 81;
      numArray10[21] = (byte) 25;
      numArray10[23] = (byte) 156;
      numArray10[24] = (byte) 33;
      numArray10[13] = (byte) 53;
      numArray10[0] = (byte) 222;
      numArray10[27] = (byte) 225;
      numArray10[28] = (byte) 117;
      numArray10[5] = (byte) 176 /*0xB0*/;
      numArray10[30] = (byte) 36;
      byte[] numArray11 = new byte[31 /*0x1F*/];
      numArray11[14] = (byte) 244;
      numArray11[1] = (byte) 76;
      numArray11[11] = (byte) 17;
      numArray11[15] = (byte) 141;
      numArray11[0] = (byte) 81;
      numArray11[5] = (byte) 9;
      numArray11[30] = (byte) 152;
      numArray11[7] = (byte) 211;
      numArray11[18] = (byte) 134;
      numArray11[8] = (byte) 80 /*0x50*/;
      numArray11[19] = (byte) 233;
      numArray11[6] = (byte) 209;
      numArray11[12] = (byte) 237;
      numArray11[24] = (byte) 62;
      numArray11[3] = (byte) 102;
      numArray11[2] = (byte) 90;
      numArray11[16 /*0x10*/] = (byte) 191;
      numArray11[17] = (byte) 205;
      numArray11[21] = (byte) 84;
      numArray11[22] = (byte) 10;
      numArray11[20] = (byte) 126;
      numArray11[10] = (byte) 72;
      numArray11[25] = (byte) 81;
      numArray11[23] = (byte) 134;
      numArray11[9] = (byte) 43;
      numArray11[4] = (byte) 150;
      numArray11[26] = (byte) 175;
      numArray11[27] = (byte) 33;
      numArray11[28] = (byte) 21;
      numArray11[29] = (byte) 216;
      numArray11[13] = (byte) 96 /*0x60*/;
      key.Query(true, 335, numArray10, numArray10);
      Array.Copy((Array) numArray10, 0, (Array) numArray1, 220, 31 /*0x1F*/);
      for (int index = 0; index < 31 /*0x1F*/; ++index)
        numArray1[index + 220] ^= numArray11[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray12 = new byte[251];
    byte[] numArray13 = new byte[55]
    {
      (byte) 39,
      (byte) 15,
      (byte) 25,
      (byte) 210,
      (byte) 61,
      (byte) 118,
      (byte) 8,
      (byte) 82,
      (byte) 20,
      (byte) 124,
      (byte) 101,
      (byte) 6,
      (byte) 159,
      (byte) 226,
      (byte) 135,
      (byte) 75,
      (byte) 152,
      (byte) 133,
      (byte) 74,
      (byte) 214,
      (byte) 125,
      (byte) 164,
      (byte) 245,
      (byte) 11,
      (byte) 96 /*0x60*/,
      (byte) 133,
      (byte) 167,
      (byte) 15,
      (byte) 180,
      (byte) 175,
      (byte) 218,
      (byte) 52,
      (byte) 19,
      (byte) 192 /*0xC0*/,
      (byte) 137,
      (byte) 46,
      (byte) 83,
      (byte) 129,
      (byte) 198,
      (byte) 253,
      (byte) 127 /*0x7F*/,
      (byte) 209,
      (byte) 91,
      (byte) 90,
      (byte) 8,
      (byte) 15,
      (byte) 186,
      (byte) 123,
      (byte) 41,
      (byte) 106,
      (byte) 170,
      (byte) 234,
      (byte) 209,
      (byte) 138,
      (byte) 140
    };
    byte[] numArray14 = new byte[55];
    numArray14[34] = (byte) 84;
    numArray14[1] = (byte) 118;
    numArray14[30] = (byte) 223;
    numArray14[40] = (byte) 254;
    numArray14[2] = (byte) 204;
    numArray14[37] = (byte) 82;
    numArray14[0] = (byte) 79;
    numArray14[8] = (byte) 91;
    numArray14[33] = (byte) 241;
    numArray14[53] = (byte) 193;
    numArray14[14] = (byte) 86;
    numArray14[6] = (byte) 35;
    numArray14[26] = (byte) 42;
    numArray14[4] = (byte) 57;
    numArray14[48 /*0x30*/] = (byte) 226;
    numArray14[15] = (byte) 217;
    numArray14[51] = (byte) 199;
    numArray14[17] = (byte) 240 /*0xF0*/;
    numArray14[18] = (byte) 167;
    numArray14[16 /*0x10*/] = (byte) 229;
    numArray14[20] = (byte) 204;
    numArray14[21] = (byte) 226;
    numArray14[9] = (byte) 123;
    numArray14[23] = (byte) 178;
    numArray14[24] = (byte) 55;
    numArray14[25] = (byte) 8;
    numArray14[11] = (byte) 174;
    numArray14[27] = (byte) 158;
    numArray14[28] = (byte) 94;
    numArray14[12] = (byte) 201;
    numArray14[29] = (byte) 138;
    numArray14[31 /*0x1F*/] = (byte) 119;
    numArray14[32 /*0x20*/] = (byte) 62;
    numArray14[38] = (byte) 204;
    numArray14[13] = (byte) 67;
    numArray14[7] = (byte) 139;
    numArray14[36] = (byte) 19;
    numArray14[41] = (byte) 82;
    numArray14[52] = (byte) 223;
    numArray14[39] = (byte) 57;
    numArray14[42] = (byte) 177;
    numArray14[10] = (byte) 61;
    numArray14[45] = (byte) 7;
    numArray14[46] = (byte) 194;
    numArray14[44] = (byte) 232;
    numArray14[49] = (byte) 115;
    numArray14[43] = (byte) 9;
    numArray14[47] = (byte) 182;
    numArray14[35] = (byte) 67;
    numArray14[22] = (byte) 187;
    numArray14[50] = (byte) 173;
    numArray14[5] = (byte) 12;
    numArray14[3] = (byte) 251;
    numArray14[19] = (byte) 105;
    numArray14[54] = (byte) 150;
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray12, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index] ^= numArray14[index];
    byte[] numArray15 = new byte[55]
    {
      (byte) 10,
      (byte) 32 /*0x20*/,
      (byte) 237,
      (byte) 163,
      (byte) 18,
      (byte) 83,
      (byte) 95,
      (byte) 178,
      (byte) 206,
      (byte) 106,
      (byte) 170,
      (byte) 82,
      (byte) 67,
      (byte) 177,
      (byte) 7,
      (byte) 177,
      (byte) 30,
      (byte) 208 /*0xD0*/,
      (byte) 120,
      (byte) 83,
      (byte) 227,
      (byte) 43,
      (byte) 52,
      (byte) 239,
      (byte) 9,
      (byte) 22,
      (byte) 215,
      (byte) 113,
      (byte) 196,
      (byte) 221,
      (byte) 84,
      (byte) 84,
      (byte) 206,
      (byte) 150,
      (byte) 59,
      (byte) 239,
      (byte) 32 /*0x20*/,
      (byte) 138,
      (byte) 116,
      (byte) 224 /*0xE0*/,
      (byte) 150,
      (byte) 6,
      (byte) 8,
      (byte) 178,
      (byte) 48 /*0x30*/,
      (byte) 208 /*0xD0*/,
      (byte) 140,
      (byte) 194,
      (byte) 139,
      (byte) 122,
      (byte) 103,
      (byte) 99,
      (byte) 103,
      (byte) 228,
      (byte) 155
    };
    byte[] numArray16 = new byte[55];
    numArray16[31 /*0x1F*/] = (byte) 233;
    numArray16[1] = (byte) 199;
    numArray16[12] = (byte) 96 /*0x60*/;
    numArray16[49] = (byte) 203;
    numArray16[26] = (byte) 157;
    numArray16[40] = (byte) 1;
    numArray16[41] = (byte) 245;
    numArray16[24] = (byte) 103;
    numArray16[38] = (byte) 253;
    numArray16[9] = (byte) 26;
    numArray16[10] = (byte) 171;
    numArray16[15] = (byte) 194;
    numArray16[6] = (byte) 240 /*0xF0*/;
    numArray16[13] = (byte) 234;
    numArray16[14] = (byte) 177;
    numArray16[0] = (byte) 40;
    numArray16[16 /*0x10*/] = (byte) 208 /*0xD0*/;
    numArray16[4] = (byte) 34;
    numArray16[18] = (byte) 10;
    numArray16[36] = (byte) 184;
    numArray16[39] = (byte) 198;
    numArray16[21] = (byte) 13;
    numArray16[32 /*0x20*/] = (byte) 13;
    numArray16[42] = (byte) 206;
    numArray16[23] = (byte) 155;
    numArray16[25] = (byte) 68;
    numArray16[5] = (byte) 233;
    numArray16[50] = (byte) 38;
    numArray16[46] = (byte) 158;
    numArray16[27] = (byte) 215;
    numArray16[30] = (byte) 141;
    numArray16[45] = (byte) 117;
    numArray16[20] = (byte) 254;
    numArray16[33] = (byte) 160 /*0xA0*/;
    numArray16[34] = (byte) 38;
    numArray16[35] = (byte) 226;
    numArray16[43] = (byte) 219;
    numArray16[8] = (byte) 188;
    numArray16[7] = (byte) 83;
    numArray16[11] = (byte) 48 /*0x30*/;
    numArray16[28] = (byte) 98;
    numArray16[17] = (byte) 147;
    numArray16[29] = (byte) 42;
    numArray16[37] = (byte) 89;
    numArray16[44] = (byte) 178;
    numArray16[22] = (byte) 207;
    numArray16[19] = (byte) 19;
    numArray16[47] = (byte) 198;
    numArray16[48 /*0x30*/] = (byte) 25;
    numArray16[2] = (byte) 209;
    numArray16[3] = (byte) 124;
    numArray16[51] = (byte) 112 /*0x70*/;
    numArray16[52] = (byte) 87;
    numArray16[53] = (byte) 216;
    numArray16[54] = (byte) 71;
    key.Query(true, 335, numArray15, numArray15);
    Array.Copy((Array) numArray15, 0, (Array) numArray12, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index + 55] ^= numArray16[index];
    byte[] numArray17 = new byte[55];
    numArray17[52] = (byte) 250;
    numArray17[6] = (byte) 123;
    numArray17[35] = (byte) 141;
    numArray17[8] = (byte) 105;
    numArray17[4] = (byte) 7;
    numArray17[5] = (byte) 162;
    numArray17[41] = (byte) 121;
    numArray17[29] = (byte) 30;
    numArray17[23] = (byte) 170;
    numArray17[31 /*0x1F*/] = (byte) 59;
    numArray17[10] = (byte) 178;
    numArray17[50] = (byte) 155;
    numArray17[2] = (byte) 114;
    numArray17[13] = (byte) 110;
    numArray17[9] = (byte) 153;
    numArray17[15] = (byte) 227;
    numArray17[16 /*0x10*/] = (byte) 51;
    numArray17[17] = (byte) 20;
    numArray17[32 /*0x20*/] = (byte) 159;
    numArray17[19] = (byte) 12;
    numArray17[28] = (byte) 234;
    numArray17[11] = (byte) 25;
    numArray17[34] = (byte) 225;
    numArray17[38] = (byte) 161;
    numArray17[24] = (byte) 201;
    numArray17[42] = (byte) 112 /*0x70*/;
    numArray17[26] = (byte) 96 /*0x60*/;
    numArray17[27] = (byte) 163;
    numArray17[33] = (byte) 151;
    numArray17[51] = (byte) 18;
    numArray17[30] = (byte) 194;
    numArray17[46] = (byte) 94;
    numArray17[3] = (byte) 101;
    numArray17[12] = (byte) 21;
    numArray17[18] = (byte) 196;
    numArray17[14] = (byte) 164;
    numArray17[36] = (byte) 5;
    numArray17[21] = (byte) 228;
    numArray17[25] = (byte) 114;
    numArray17[39] = (byte) 117;
    numArray17[40] = (byte) 155;
    numArray17[37] = (byte) 254;
    numArray17[7] = (byte) 35;
    numArray17[43] = (byte) 179;
    numArray17[44] = (byte) 44;
    numArray17[45] = (byte) 50;
    numArray17[47] = (byte) 123;
    numArray17[1] = (byte) 98;
    numArray17[48 /*0x30*/] = (byte) 46;
    numArray17[49] = (byte) 35;
    numArray17[54] = (byte) 134;
    numArray17[20] = (byte) 228;
    numArray17[22] = (byte) 235;
    numArray17[53] = (byte) 176 /*0xB0*/;
    numArray17[0] = (byte) 64 /*0x40*/;
    byte[] numArray18 = new byte[55];
    numArray18[30] = (byte) 69;
    numArray18[1] = (byte) 217;
    numArray18[2] = (byte) 136;
    numArray18[12] = (byte) 151;
    numArray18[40] = (byte) 126;
    numArray18[3] = (byte) 223;
    numArray18[6] = (byte) 154;
    numArray18[8] = (byte) 197;
    numArray18[21] = (byte) 220;
    numArray18[9] = (byte) 86;
    numArray18[15] = (byte) 237;
    numArray18[11] = (byte) 251;
    numArray18[39] = (byte) 27;
    numArray18[13] = (byte) 242;
    numArray18[33] = (byte) 204;
    numArray18[27] = (byte) 196;
    numArray18[16 /*0x10*/] = (byte) 141;
    numArray18[17] = (byte) 42;
    numArray18[5] = (byte) 235;
    numArray18[7] = (byte) 89;
    numArray18[20] = (byte) 11;
    numArray18[19] = (byte) 0;
    numArray18[22] = (byte) 7;
    numArray18[53] = (byte) 70;
    numArray18[24] = (byte) 171;
    numArray18[42] = (byte) 113;
    numArray18[26] = (byte) 71;
    numArray18[29] = (byte) 219;
    numArray18[52] = (byte) 193;
    numArray18[4] = (byte) 52;
    numArray18[0] = (byte) 78;
    numArray18[49] = (byte) 83;
    numArray18[28] = (byte) 199;
    numArray18[47] = (byte) 139;
    numArray18[34] = (byte) 172;
    numArray18[35] = (byte) 8;
    numArray18[36] = (byte) 7;
    numArray18[37] = (byte) 14;
    numArray18[18] = (byte) 129;
    numArray18[44] = (byte) 142;
    numArray18[14] = (byte) 151;
    numArray18[41] = (byte) 214;
    numArray18[43] = (byte) 246;
    numArray18[46] = (byte) 137;
    numArray18[31 /*0x1F*/] = (byte) 211;
    numArray18[45] = (byte) 240 /*0xF0*/;
    numArray18[25] = (byte) 65;
    numArray18[10] = (byte) 243;
    numArray18[48 /*0x30*/] = (byte) 45;
    numArray18[38] = (byte) 131;
    numArray18[50] = (byte) 201;
    numArray18[51] = (byte) 182;
    numArray18[23] = (byte) 108;
    numArray18[32 /*0x20*/] = (byte) 9;
    numArray18[54] = (byte) 85;
    key.Query(true, 335, numArray17, numArray17);
    Array.Copy((Array) numArray17, 0, (Array) numArray12, 110, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index + 110] ^= numArray18[index];
    byte[] numArray19 = new byte[55]
    {
      (byte) 130,
      (byte) 37,
      (byte) 77,
      (byte) 100,
      (byte) 6,
      (byte) 241,
      (byte) 170,
      (byte) 22,
      (byte) 161,
      (byte) 218,
      (byte) 119,
      (byte) 104,
      (byte) 38,
      (byte) 174,
      (byte) 117,
      (byte) 221,
      (byte) 66,
      (byte) 76,
      (byte) 92,
      (byte) 239,
      (byte) 165,
      (byte) 73,
      (byte) 177,
      (byte) 192 /*0xC0*/,
      (byte) 15,
      (byte) 69,
      (byte) 219,
      (byte) 136,
      (byte) 216,
      (byte) 2,
      (byte) 28,
      (byte) 4,
      (byte) 3,
      (byte) 247,
      (byte) 111,
      (byte) 55,
      (byte) 60,
      (byte) 114,
      (byte) 88,
      (byte) 219,
      (byte) 179,
      (byte) 160 /*0xA0*/,
      (byte) 222,
      (byte) 83,
      (byte) 252,
      (byte) 127 /*0x7F*/,
      (byte) 241,
      (byte) 246,
      (byte) 31 /*0x1F*/,
      (byte) 90,
      (byte) 78,
      (byte) 70,
      (byte) 148,
      (byte) 170,
      (byte) 61
    };
    byte[] numArray20 = new byte[55]
    {
      (byte) 118,
      (byte) 86,
      (byte) 176 /*0xB0*/,
      (byte) 221,
      (byte) 184,
      (byte) 207,
      (byte) 137,
      (byte) 54,
      (byte) 247,
      (byte) 106,
      (byte) 98,
      (byte) 10,
      (byte) 103,
      (byte) 152,
      (byte) 117,
      (byte) 225,
      (byte) 92,
      (byte) 105,
      (byte) 246,
      (byte) 16 /*0x10*/,
      (byte) 39,
      (byte) 177,
      (byte) 209,
      (byte) 84,
      (byte) 6,
      (byte) 152,
      (byte) 24,
      (byte) 170,
      (byte) 244,
      (byte) 136,
      (byte) 216,
      (byte) 172,
      (byte) 133,
      (byte) 233,
      (byte) 209,
      (byte) 129,
      (byte) 233,
      (byte) 219,
      (byte) 128 /*0x80*/,
      (byte) 195,
      (byte) 131,
      (byte) 130,
      (byte) 50,
      (byte) 168,
      (byte) 123,
      (byte) 137,
      (byte) 225,
      (byte) 110,
      (byte) 247,
      (byte) 218,
      (byte) 97,
      (byte) 82,
      (byte) 251,
      (byte) 171,
      (byte) 250
    };
    key.Query(true, 335, numArray19, numArray19);
    Array.Copy((Array) numArray19, 0, (Array) numArray12, 165, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index + 165] ^= numArray20[index];
    byte[] numArray21 = new byte[31 /*0x1F*/]
    {
      (byte) 141,
      (byte) 85,
      (byte) 193,
      (byte) 21,
      (byte) 33,
      (byte) 61,
      (byte) 158,
      (byte) 189,
      (byte) 114,
      (byte) 100,
      (byte) 47,
      (byte) 62,
      (byte) 61,
      (byte) 160 /*0xA0*/,
      (byte) 208 /*0xD0*/,
      (byte) 243,
      (byte) 128 /*0x80*/,
      (byte) 60,
      (byte) 4,
      (byte) 107,
      (byte) 94,
      (byte) 196,
      (byte) 144 /*0x90*/,
      (byte) 83,
      (byte) 229,
      (byte) 105,
      (byte) 126,
      (byte) 179,
      (byte) 214,
      (byte) 112 /*0x70*/,
      (byte) 89
    };
    byte[] numArray22 = new byte[31 /*0x1F*/]
    {
      (byte) 81,
      (byte) 138,
      (byte) 210,
      (byte) 232,
      (byte) 226,
      (byte) 168,
      (byte) 97,
      (byte) 14,
      (byte) 130,
      (byte) 232,
      (byte) 86,
      (byte) 188,
      (byte) 46,
      (byte) 212,
      (byte) 210,
      (byte) 84,
      (byte) 86,
      (byte) 143,
      (byte) 55,
      (byte) 197,
      (byte) 214,
      (byte) 155,
      (byte) 238,
      (byte) 232,
      (byte) 202,
      (byte) 218,
      (byte) 239,
      (byte) 201,
      (byte) 36,
      (byte) 207,
      (byte) 237
    };
    key.Query(true, 335, numArray21, numArray21);
    Array.Copy((Array) numArray21, 0, (Array) numArray12, 220, 31 /*0x1F*/);
    for (int index = 0; index < 31 /*0x1F*/; ++index)
      numArray12[index + 220] ^= numArray22[index];
    byte[] numArray23 = new byte[36];
    byte[] response = new byte[36];
    Array.Copy((Array) sc_13291.sspq, 0, (Array) numArray23, 0, 36);
    key.Query(true, 335, numArray23, response);
    Array.Copy((Array) sc_13291.sspr, 0, (Array) numArray23, 0, 36);
    for (int index = 0; index < numArray23.Length; ++index)
    {
      if ((int) numArray23[index] != (int) response[index])
      {
        key.TagValue = (int) response[index];
        break;
      }
    }
    return Encoding.UTF8.GetString(numArray12);
  }
}
