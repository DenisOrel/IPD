// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_903
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_903
{
  internal static string ssp_avs_904()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[129];
      byte[] numArray2 = new byte[55];
      numArray2[38] = (byte) 82;
      numArray2[1] = (byte) 90;
      numArray2[7] = (byte) 131;
      numArray2[28] = (byte) 17;
      numArray2[46] = (byte) 163;
      numArray2[50] = (byte) 115;
      numArray2[6] = (byte) 170;
      numArray2[5] = (byte) 216;
      numArray2[8] = (byte) 190;
      numArray2[9] = (byte) 213;
      numArray2[14] = (byte) 138;
      numArray2[3] = (byte) 253;
      numArray2[12] = (byte) 47;
      numArray2[33] = (byte) 195;
      numArray2[22] = (byte) 252;
      numArray2[36] = (byte) 239;
      numArray2[39] = (byte) 81;
      numArray2[26] = (byte) 108;
      numArray2[18] = (byte) 15;
      numArray2[19] = (byte) 193;
      numArray2[20] = (byte) 120;
      numArray2[21] = (byte) 235;
      numArray2[30] = (byte) 64 /*0x40*/;
      numArray2[23] = (byte) 27;
      numArray2[4] = (byte) 186;
      numArray2[25] = (byte) 117;
      numArray2[53] = (byte) 234;
      numArray2[27] = (byte) 34;
      numArray2[44] = (byte) 171;
      numArray2[45] = (byte) 54;
      numArray2[0] = (byte) 55;
      numArray2[31 /*0x1F*/] = (byte) 17;
      numArray2[10] = (byte) 98;
      numArray2[29] = (byte) 78;
      numArray2[15] = (byte) 208 /*0xD0*/;
      numArray2[35] = (byte) 247;
      numArray2[17] = (byte) 173;
      numArray2[37] = (byte) 193;
      numArray2[2] = (byte) 229;
      numArray2[11] = (byte) 58;
      numArray2[40] = (byte) 191;
      numArray2[41] = (byte) 126;
      numArray2[42] = (byte) 62;
      numArray2[43] = (byte) 106;
      numArray2[24] = (byte) 137;
      numArray2[32 /*0x20*/] = (byte) 132;
      numArray2[13] = (byte) 38;
      numArray2[47] = (byte) 34;
      numArray2[48 /*0x30*/] = (byte) 123;
      numArray2[52] = (byte) 187;
      numArray2[16 /*0x10*/] = (byte) 233;
      numArray2[49] = (byte) 40;
      numArray2[34] = (byte) 119;
      numArray2[51] = (byte) 95;
      numArray2[54] = (byte) 199;
      byte[] numArray3 = new byte[55]
      {
        (byte) 1,
        (byte) 114,
        (byte) 34,
        (byte) 96 /*0x60*/,
        (byte) 45,
        (byte) 249,
        (byte) 94,
        (byte) 100,
        (byte) 187,
        (byte) 81,
        (byte) 80 /*0x50*/,
        (byte) 52,
        (byte) 153,
        (byte) 121,
        (byte) 144 /*0x90*/,
        (byte) 15,
        (byte) 18,
        (byte) 81,
        (byte) 223,
        (byte) 155,
        (byte) 220,
        (byte) 45,
        (byte) 250,
        (byte) 118,
        (byte) 55,
        (byte) 103,
        (byte) 218,
        (byte) 250,
        (byte) 26,
        (byte) 181,
        (byte) 236,
        (byte) 218,
        (byte) 210,
        (byte) 227,
        (byte) 187,
        (byte) 65,
        (byte) 65,
        (byte) 151,
        (byte) 35,
        (byte) 32 /*0x20*/,
        (byte) 192 /*0xC0*/,
        (byte) 73,
        (byte) 197,
        (byte) 232,
        (byte) 45,
        (byte) 246,
        (byte) 45,
        (byte) 92,
        (byte) 132,
        (byte) 13,
        (byte) 181,
        (byte) 244,
        (byte) 172,
        (byte) 13,
        (byte) 247
      };
      key.Query(true, 339, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55];
      numArray4[46] = (byte) 238;
      numArray4[1] = (byte) 33;
      numArray4[2] = (byte) 115;
      numArray4[6] = (byte) 148;
      numArray4[19] = (byte) 221;
      numArray4[5] = (byte) 136;
      numArray4[22] = (byte) 209;
      numArray4[4] = (byte) 224 /*0xE0*/;
      numArray4[45] = (byte) 239;
      numArray4[9] = (byte) 27;
      numArray4[10] = (byte) 112 /*0x70*/;
      numArray4[11] = (byte) 131;
      numArray4[14] = (byte) 218;
      numArray4[50] = (byte) 38;
      numArray4[29] = (byte) 88;
      numArray4[15] = (byte) 78;
      numArray4[16 /*0x10*/] = (byte) 215;
      numArray4[17] = (byte) 203;
      numArray4[18] = (byte) 236;
      numArray4[38] = (byte) 86;
      numArray4[21] = (byte) 20;
      numArray4[7] = (byte) 178;
      numArray4[52] = (byte) 225;
      numArray4[23] = (byte) 154;
      numArray4[39] = (byte) 132;
      numArray4[47] = (byte) 139;
      numArray4[12] = (byte) 250;
      numArray4[27] = (byte) 143;
      numArray4[28] = (byte) 222;
      numArray4[25] = (byte) 191;
      numArray4[32 /*0x20*/] = (byte) 140;
      numArray4[26] = (byte) 161;
      numArray4[20] = (byte) 187;
      numArray4[33] = (byte) 203;
      numArray4[34] = (byte) 202;
      numArray4[24] = (byte) 215;
      numArray4[36] = (byte) 153;
      numArray4[37] = (byte) 197;
      numArray4[43] = (byte) 100;
      numArray4[13] = (byte) 187;
      numArray4[0] = (byte) 90;
      numArray4[53] = (byte) 193;
      numArray4[42] = (byte) 96 /*0x60*/;
      numArray4[40] = (byte) 58;
      numArray4[44] = (byte) 198;
      numArray4[41] = (byte) 35;
      numArray4[35] = (byte) 44;
      numArray4[49] = (byte) 181;
      numArray4[30] = (byte) 177;
      numArray4[54] = (byte) 17;
      numArray4[48 /*0x30*/] = (byte) 112 /*0x70*/;
      numArray4[51] = (byte) 28;
      numArray4[31 /*0x1F*/] = (byte) 45;
      numArray4[3] = (byte) 32 /*0x20*/;
      numArray4[8] = (byte) 250;
      byte[] numArray5 = new byte[55];
      numArray5[37] = (byte) 96 /*0x60*/;
      numArray5[31 /*0x1F*/] = (byte) 27;
      numArray5[2] = (byte) 184;
      numArray5[3] = (byte) 230;
      numArray5[30] = (byte) 216;
      numArray5[45] = (byte) 161;
      numArray5[6] = (byte) 168;
      numArray5[24] = (byte) 28;
      numArray5[8] = (byte) 119;
      numArray5[34] = (byte) 64 /*0x40*/;
      numArray5[14] = (byte) 187;
      numArray5[11] = (byte) 26;
      numArray5[54] = (byte) 42;
      numArray5[18] = (byte) 192 /*0xC0*/;
      numArray5[51] = (byte) 129;
      numArray5[0] = (byte) 46;
      numArray5[16 /*0x10*/] = (byte) 74;
      numArray5[17] = (byte) 200;
      numArray5[13] = (byte) 72;
      numArray5[7] = (byte) 187;
      numArray5[20] = (byte) 87;
      numArray5[19] = (byte) 66;
      numArray5[4] = (byte) 164;
      numArray5[23] = (byte) 68;
      numArray5[22] = (byte) 237;
      numArray5[47] = (byte) 96 /*0x60*/;
      numArray5[38] = (byte) 199;
      numArray5[27] = (byte) 178;
      numArray5[12] = (byte) 13;
      numArray5[29] = (byte) 234;
      numArray5[53] = (byte) 236;
      numArray5[9] = (byte) 253;
      numArray5[32 /*0x20*/] = (byte) 41;
      numArray5[25] = (byte) 33;
      numArray5[15] = (byte) 216;
      numArray5[35] = (byte) 44;
      numArray5[49] = (byte) 36;
      numArray5[36] = (byte) 203;
      numArray5[10] = (byte) 82;
      numArray5[39] = (byte) 162;
      numArray5[40] = (byte) 10;
      numArray5[41] = (byte) 28;
      numArray5[26] = (byte) 249;
      numArray5[43] = (byte) 103;
      numArray5[44] = (byte) 20;
      numArray5[28] = (byte) 65;
      numArray5[1] = (byte) 253;
      numArray5[50] = (byte) 123;
      numArray5[48 /*0x30*/] = (byte) 119;
      numArray5[21] = (byte) 81;
      numArray5[46] = (byte) 25;
      numArray5[33] = (byte) 176 /*0xB0*/;
      numArray5[52] = (byte) 105;
      numArray5[5] = (byte) 119;
      numArray5[42] = (byte) 170;
      key.Query(true, 339, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[19];
      numArray6[10] = (byte) 53;
      numArray6[1] = (byte) 113;
      numArray6[2] = (byte) 63 /*0x3F*/;
      numArray6[3] = (byte) 86;
      numArray6[12] = (byte) 216;
      numArray6[0] = (byte) 178;
      numArray6[5] = (byte) 132;
      numArray6[7] = (byte) 232;
      numArray6[8] = (byte) 37;
      numArray6[13] = (byte) 52;
      numArray6[15] = (byte) 91;
      numArray6[17] = (byte) 32 /*0x20*/;
      numArray6[18] = (byte) 165;
      numArray6[6] = (byte) 82;
      numArray6[14] = (byte) 123;
      numArray6[9] = (byte) 128 /*0x80*/;
      numArray6[16 /*0x10*/] = (byte) 253;
      numArray6[11] = (byte) 121;
      numArray6[4] = (byte) 138;
      byte[] numArray7 = new byte[19];
      numArray7[17] = (byte) 31 /*0x1F*/;
      numArray7[14] = (byte) 70;
      numArray7[3] = (byte) 10;
      numArray7[18] = (byte) 20;
      numArray7[11] = (byte) 51;
      numArray7[5] = (byte) 126;
      numArray7[0] = (byte) 247;
      numArray7[8] = (byte) 253;
      numArray7[9] = (byte) 225;
      numArray7[7] = (byte) 58;
      numArray7[4] = (byte) 69;
      numArray7[10] = (byte) 253;
      numArray7[12] = (byte) 56;
      numArray7[2] = (byte) 48 /*0x30*/;
      numArray7[13] = (byte) 197;
      numArray7[15] = (byte) 34;
      numArray7[16 /*0x10*/] = (byte) 81;
      numArray7[6] = (byte) 210;
      numArray7[1] = (byte) 124;
      key.Query(true, 339, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index + 110] ^= numArray7[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray8 = new byte[129];
    byte[] numArray9 = new byte[55]
    {
      (byte) 107,
      (byte) 179,
      (byte) 98,
      (byte) 2,
      (byte) 132,
      (byte) 39,
      (byte) 34,
      (byte) 79,
      (byte) 40,
      (byte) 210,
      (byte) 182,
      (byte) 135,
      (byte) 93,
      (byte) 134,
      (byte) 174,
      (byte) 15,
      (byte) 94,
      (byte) 218,
      (byte) 176 /*0xB0*/,
      (byte) 150,
      (byte) 83,
      (byte) 62,
      (byte) 156,
      (byte) 122,
      (byte) 112 /*0x70*/,
      (byte) 108,
      (byte) 129,
      (byte) 99,
      (byte) 101,
      (byte) 30,
      (byte) 91,
      (byte) 43,
      (byte) 108,
      (byte) 164,
      (byte) 178,
      (byte) 70,
      (byte) 218,
      (byte) 99,
      (byte) 198,
      (byte) 238,
      (byte) 146,
      (byte) 200,
      (byte) 177,
      (byte) 48 /*0x30*/,
      (byte) 85,
      (byte) 42,
      (byte) 50,
      (byte) 5,
      (byte) 69,
      (byte) 19,
      (byte) 142,
      (byte) 114,
      (byte) 200,
      (byte) 114,
      (byte) 203
    };
    byte[] numArray10 = new byte[55];
    numArray10[15] = (byte) 89;
    numArray10[30] = (byte) 90;
    numArray10[2] = (byte) 212;
    numArray10[3] = (byte) 214;
    numArray10[4] = (byte) 148;
    numArray10[5] = (byte) 235;
    numArray10[53] = (byte) 153;
    numArray10[42] = (byte) 21;
    numArray10[22] = (byte) 84;
    numArray10[9] = (byte) 183;
    numArray10[0] = (byte) 61;
    numArray10[40] = (byte) 63 /*0x3F*/;
    numArray10[12] = (byte) 243;
    numArray10[13] = (byte) 80 /*0x50*/;
    numArray10[14] = (byte) 197;
    numArray10[10] = (byte) 161;
    numArray10[16 /*0x10*/] = (byte) 27;
    numArray10[17] = (byte) 152;
    numArray10[32 /*0x20*/] = (byte) 140;
    numArray10[19] = (byte) 190;
    numArray10[20] = (byte) 167;
    numArray10[21] = (byte) 82;
    numArray10[44] = (byte) 231;
    numArray10[8] = (byte) 197;
    numArray10[47] = (byte) 60;
    numArray10[25] = (byte) 218;
    numArray10[6] = (byte) 138;
    numArray10[33] = (byte) 143;
    numArray10[28] = (byte) 22;
    numArray10[29] = (byte) 184;
    numArray10[26] = (byte) 55;
    numArray10[45] = (byte) 196;
    numArray10[24] = (byte) 184;
    numArray10[23] = (byte) 17;
    numArray10[34] = (byte) 246;
    numArray10[35] = (byte) 137;
    numArray10[36] = (byte) 85;
    numArray10[37] = (byte) 139;
    numArray10[31 /*0x1F*/] = (byte) 245;
    numArray10[39] = (byte) 223;
    numArray10[38] = (byte) 127 /*0x7F*/;
    numArray10[18] = (byte) 172;
    numArray10[1] = (byte) 104;
    numArray10[43] = (byte) 15;
    numArray10[50] = (byte) 166;
    numArray10[7] = (byte) 0;
    numArray10[46] = (byte) 72;
    numArray10[41] = (byte) 82;
    numArray10[48 /*0x30*/] = (byte) 228;
    numArray10[49] = (byte) 239;
    numArray10[27] = (byte) 235;
    numArray10[51] = (byte) 154;
    numArray10[52] = (byte) 13;
    numArray10[11] = (byte) 207;
    numArray10[54] = (byte) 163;
    key.Query(true, 339, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray8, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index] ^= numArray10[index];
    byte[] numArray11 = new byte[55]
    {
      (byte) 195,
      (byte) 76,
      (byte) 221,
      (byte) 41,
      (byte) 161,
      (byte) 152,
      (byte) 122,
      (byte) 141,
      (byte) 39,
      (byte) 33,
      (byte) 224 /*0xE0*/,
      (byte) 78,
      (byte) 55,
      (byte) 66,
      (byte) 41,
      (byte) 101,
      (byte) 122,
      (byte) 49,
      (byte) 172,
      (byte) 230,
      (byte) 178,
      (byte) 90,
      (byte) 53,
      (byte) 193,
      (byte) 74,
      (byte) 63 /*0x3F*/,
      (byte) 114,
      (byte) 252,
      (byte) 138,
      (byte) 93,
      (byte) 138,
      (byte) 152,
      (byte) 229,
      (byte) 133,
      (byte) 47,
      (byte) 132,
      (byte) 209,
      (byte) 30,
      (byte) 49,
      (byte) 69,
      (byte) 44,
      (byte) 44,
      (byte) 162,
      (byte) 238,
      (byte) 153,
      (byte) 227,
      (byte) 166,
      (byte) 156,
      (byte) 106,
      (byte) 249,
      (byte) 139,
      (byte) 61,
      (byte) 147,
      (byte) 185,
      (byte) 247
    };
    byte[] numArray12 = new byte[55]
    {
      (byte) 46,
      (byte) 187,
      (byte) 240 /*0xF0*/,
      (byte) 52,
      (byte) 97,
      (byte) 0,
      (byte) 96 /*0x60*/,
      (byte) 122,
      (byte) 127 /*0x7F*/,
      (byte) 143,
      (byte) 188,
      (byte) 143,
      (byte) 173,
      (byte) 146,
      (byte) 56,
      (byte) 143,
      (byte) 184,
      (byte) 86,
      (byte) 96 /*0x60*/,
      (byte) 209,
      (byte) 171,
      (byte) 1,
      (byte) 72,
      (byte) 253,
      (byte) 191,
      (byte) 129,
      (byte) 84,
      byte.MaxValue,
      (byte) 106,
      (byte) 15,
      (byte) 57,
      (byte) 162,
      (byte) 62,
      (byte) 101,
      (byte) 230,
      (byte) 226,
      (byte) 16 /*0x10*/,
      (byte) 250,
      (byte) 166,
      (byte) 32 /*0x20*/,
      (byte) 251,
      (byte) 233,
      (byte) 182,
      (byte) 2,
      (byte) 145,
      (byte) 56,
      (byte) 78,
      (byte) 103,
      (byte) 47,
      (byte) 205,
      (byte) 187,
      (byte) 109,
      (byte) 164,
      (byte) 23,
      (byte) 28
    };
    key.Query(true, 339, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray8, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index + 55] ^= numArray12[index];
    byte[] numArray13 = new byte[19];
    numArray13[14] = (byte) 91;
    numArray13[1] = (byte) 223;
    numArray13[7] = (byte) 150;
    numArray13[3] = (byte) 37;
    numArray13[15] = byte.MaxValue;
    numArray13[0] = (byte) 28;
    numArray13[6] = (byte) 201;
    numArray13[17] = (byte) 186;
    numArray13[8] = (byte) 71;
    numArray13[9] = (byte) 104;
    numArray13[10] = (byte) 177;
    numArray13[4] = (byte) 125;
    numArray13[12] = (byte) 164;
    numArray13[2] = (byte) 97;
    numArray13[13] = (byte) 148;
    numArray13[18] = (byte) 134;
    numArray13[16 /*0x10*/] = (byte) 102;
    numArray13[5] = (byte) 3;
    numArray13[11] = (byte) 18;
    byte[] numArray14 = new byte[19]
    {
      (byte) 111,
      (byte) 250,
      (byte) 230,
      (byte) 86,
      (byte) 51,
      (byte) 177,
      (byte) 70,
      (byte) 102,
      (byte) 253,
      (byte) 188,
      (byte) 20,
      (byte) 28,
      (byte) 142,
      (byte) 60,
      (byte) 103,
      (byte) 93,
      (byte) 94,
      (byte) 119,
      (byte) 149
    };
    key.Query(true, 339, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray8, 110, 19);
    for (int index = 0; index < 19; ++index)
      numArray8[index + 110] ^= numArray14[index];
    return Encoding.UTF8.GetString(numArray8);
  }

  internal static string ssp_avs_905()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[130];
      byte[] numArray2 = new byte[55];
      numArray2[39] = (byte) 43;
      numArray2[1] = (byte) 123;
      numArray2[23] = (byte) 34;
      numArray2[41] = (byte) 14;
      numArray2[4] = (byte) 197;
      numArray2[5] = (byte) 101;
      numArray2[19] = (byte) 246;
      numArray2[7] = (byte) 61;
      numArray2[0] = (byte) 81;
      numArray2[9] = byte.MaxValue;
      numArray2[38] = (byte) 204;
      numArray2[11] = (byte) 176 /*0xB0*/;
      numArray2[34] = (byte) 214;
      numArray2[13] = (byte) 244;
      numArray2[14] = (byte) 120;
      numArray2[15] = (byte) 200;
      numArray2[49] = (byte) 7;
      numArray2[2] = (byte) 245;
      numArray2[18] = (byte) 114;
      numArray2[10] = (byte) 112 /*0x70*/;
      numArray2[20] = (byte) 73;
      numArray2[33] = (byte) 238;
      numArray2[22] = (byte) 60;
      numArray2[16 /*0x10*/] = (byte) 79;
      numArray2[24] = (byte) 75;
      numArray2[25] = (byte) 54;
      numArray2[26] = (byte) 222;
      numArray2[27] = (byte) 28;
      numArray2[17] = (byte) 235;
      numArray2[31 /*0x1F*/] = (byte) 93;
      numArray2[48 /*0x30*/] = (byte) 199;
      numArray2[12] = (byte) 208 /*0xD0*/;
      numArray2[32 /*0x20*/] = (byte) 222;
      numArray2[47] = (byte) 192 /*0xC0*/;
      numArray2[40] = (byte) 163;
      numArray2[35] = (byte) 235;
      numArray2[36] = (byte) 164;
      numArray2[37] = (byte) 75;
      numArray2[42] = (byte) 214;
      numArray2[44] = (byte) 30;
      numArray2[29] = (byte) 70;
      numArray2[3] = (byte) 138;
      numArray2[30] = (byte) 11;
      numArray2[43] = (byte) 45;
      numArray2[46] = (byte) 156;
      numArray2[45] = (byte) 22;
      numArray2[53] = (byte) 226;
      numArray2[50] = (byte) 208 /*0xD0*/;
      numArray2[21] = (byte) 67;
      numArray2[28] = (byte) 48 /*0x30*/;
      numArray2[51] = (byte) 194;
      numArray2[8] = (byte) 45;
      numArray2[52] = (byte) 182;
      numArray2[6] = (byte) 47;
      numArray2[54] = (byte) 14;
      byte[] numArray3 = new byte[55];
      numArray3[17] = (byte) 65;
      numArray3[2] = (byte) 123;
      numArray3[33] = (byte) 109;
      numArray3[3] = (byte) 75;
      numArray3[45] = (byte) 217;
      numArray3[37] = (byte) 217;
      numArray3[18] = (byte) 115;
      numArray3[40] = (byte) 107;
      numArray3[8] = (byte) 90;
      numArray3[34] = (byte) 41;
      numArray3[36] = (byte) 190;
      numArray3[11] = (byte) 254;
      numArray3[19] = (byte) 250;
      numArray3[48 /*0x30*/] = (byte) 27;
      numArray3[9] = (byte) 35;
      numArray3[15] = (byte) 47;
      numArray3[22] = (byte) 196;
      numArray3[28] = (byte) 153;
      numArray3[10] = (byte) 223;
      numArray3[13] = (byte) 220;
      numArray3[16 /*0x10*/] = (byte) 193;
      numArray3[14] = (byte) 109;
      numArray3[35] = (byte) 79;
      numArray3[23] = (byte) 75;
      numArray3[24] = (byte) 31 /*0x1F*/;
      numArray3[25] = (byte) 60;
      numArray3[26] = (byte) 219;
      numArray3[20] = (byte) 39;
      numArray3[41] = (byte) 37;
      numArray3[29] = (byte) 176 /*0xB0*/;
      numArray3[30] = (byte) 236;
      numArray3[31 /*0x1F*/] = (byte) 245;
      numArray3[52] = (byte) 88;
      numArray3[0] = (byte) 92;
      numArray3[27] = byte.MaxValue;
      numArray3[5] = (byte) 32 /*0x20*/;
      numArray3[6] = (byte) 117;
      numArray3[1] = (byte) 24;
      numArray3[38] = (byte) 156;
      numArray3[39] = (byte) 0;
      numArray3[21] = (byte) 158;
      numArray3[12] = (byte) 29;
      numArray3[42] = (byte) 148;
      numArray3[43] = (byte) 69;
      numArray3[44] = (byte) 126;
      numArray3[53] = (byte) 199;
      numArray3[46] = (byte) 186;
      numArray3[47] = (byte) 188;
      numArray3[50] = (byte) 253;
      numArray3[49] = (byte) 105;
      numArray3[7] = (byte) 60;
      numArray3[51] = (byte) 98;
      numArray3[32 /*0x20*/] = (byte) 33;
      numArray3[4] = (byte) 175;
      numArray3[54] = (byte) 35;
      key.Query(true, 339, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55];
      numArray4[7] = (byte) 3;
      numArray4[1] = (byte) 26;
      numArray4[30] = (byte) 161;
      numArray4[3] = (byte) 237;
      numArray4[4] = (byte) 164;
      numArray4[27] = (byte) 213;
      numArray4[54] = (byte) 20;
      numArray4[41] = (byte) 139;
      numArray4[20] = (byte) 69;
      numArray4[23] = (byte) 38;
      numArray4[35] = (byte) 232;
      numArray4[10] = (byte) 246;
      numArray4[12] = (byte) 98;
      numArray4[13] = (byte) 80 /*0x50*/;
      numArray4[14] = (byte) 243;
      numArray4[53] = (byte) 41;
      numArray4[16 /*0x10*/] = (byte) 100;
      numArray4[0] = (byte) 214;
      numArray4[18] = (byte) 29;
      numArray4[9] = (byte) 105;
      numArray4[11] = (byte) 46;
      numArray4[21] = (byte) 87;
      numArray4[22] = (byte) 128 /*0x80*/;
      numArray4[2] = (byte) 22;
      numArray4[8] = (byte) 53;
      numArray4[48 /*0x30*/] = (byte) 225;
      numArray4[45] = (byte) 31 /*0x1F*/;
      numArray4[5] = (byte) 158;
      numArray4[28] = (byte) 229;
      numArray4[29] = (byte) 103;
      numArray4[26] = (byte) 182;
      numArray4[31 /*0x1F*/] = (byte) 114;
      numArray4[32 /*0x20*/] = (byte) 2;
      numArray4[25] = (byte) 191;
      numArray4[50] = (byte) 106;
      numArray4[6] = (byte) 197;
      numArray4[19] = (byte) 251;
      numArray4[33] = (byte) 35;
      numArray4[43] = (byte) 5;
      numArray4[39] = (byte) 125;
      numArray4[40] = (byte) 187;
      numArray4[15] = (byte) 154;
      numArray4[42] = (byte) 107;
      numArray4[17] = (byte) 242;
      numArray4[44] = (byte) 194;
      numArray4[36] = (byte) 49;
      numArray4[46] = (byte) 248;
      numArray4[47] = (byte) 63 /*0x3F*/;
      numArray4[51] = (byte) 216;
      numArray4[49] = (byte) 37;
      numArray4[34] = (byte) 43;
      numArray4[38] = (byte) 45;
      numArray4[52] = (byte) 124;
      numArray4[37] = (byte) 78;
      numArray4[24] = (byte) 45;
      byte[] numArray5 = new byte[55]
      {
        (byte) 224 /*0xE0*/,
        (byte) 54,
        (byte) 133,
        (byte) 143,
        (byte) 82,
        (byte) 185,
        (byte) 69,
        (byte) 53,
        (byte) 98,
        (byte) 113,
        (byte) 179,
        (byte) 57,
        (byte) 21,
        (byte) 103,
        (byte) 158,
        (byte) 54,
        (byte) 172,
        (byte) 123,
        (byte) 90,
        (byte) 71,
        (byte) 8,
        (byte) 12,
        (byte) 118,
        (byte) 233,
        (byte) 51,
        (byte) 203,
        (byte) 54,
        (byte) 177,
        (byte) 41,
        (byte) 39,
        (byte) 148,
        (byte) 152,
        (byte) 176 /*0xB0*/,
        (byte) 250,
        (byte) 223,
        (byte) 147,
        (byte) 13,
        (byte) 106,
        (byte) 90,
        (byte) 80 /*0x50*/,
        (byte) 221,
        (byte) 45,
        (byte) 152,
        (byte) 236,
        (byte) 190,
        (byte) 101,
        (byte) 39,
        (byte) 223,
        (byte) 153,
        (byte) 36,
        (byte) 174,
        (byte) 156,
        (byte) 89,
        (byte) 37,
        (byte) 166
      };
      key.Query(true, 339, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[20]
      {
        (byte) 138,
        (byte) 177,
        (byte) 29,
        (byte) 114,
        (byte) 252,
        (byte) 93,
        (byte) 209,
        byte.MaxValue,
        (byte) 142,
        (byte) 82,
        (byte) 48 /*0x30*/,
        (byte) 119,
        (byte) 188,
        (byte) 39,
        (byte) 135,
        (byte) 124,
        (byte) 12,
        (byte) 116,
        (byte) 76,
        (byte) 79
      };
      byte[] numArray7 = new byte[20]
      {
        (byte) 173,
        (byte) 46,
        (byte) 39,
        (byte) 166,
        (byte) 159,
        (byte) 200,
        (byte) 246,
        (byte) 134,
        byte.MaxValue,
        (byte) 65,
        (byte) 16 /*0x10*/,
        (byte) 35,
        (byte) 195,
        (byte) 198,
        (byte) 93,
        (byte) 246,
        (byte) 186,
        (byte) 133,
        (byte) 62,
        (byte) 216
      };
      key.Query(true, 339, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 20);
      for (int index = 0; index < 20; ++index)
        numArray1[index + 110] ^= numArray7[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray8 = new byte[130];
    byte[] numArray9 = new byte[55]
    {
      (byte) 13,
      (byte) 91,
      (byte) 28,
      (byte) 4,
      (byte) 168,
      (byte) 180,
      (byte) 25,
      (byte) 239,
      (byte) 179,
      (byte) 27,
      (byte) 48 /*0x30*/,
      (byte) 184,
      (byte) 220,
      (byte) 42,
      (byte) 34,
      (byte) 207,
      (byte) 16 /*0x10*/,
      (byte) 203,
      (byte) 62,
      (byte) 175,
      (byte) 161,
      (byte) 136,
      (byte) 163,
      (byte) 26,
      (byte) 148,
      (byte) 204,
      (byte) 150,
      (byte) 208 /*0xD0*/,
      (byte) 116,
      (byte) 14,
      (byte) 66,
      (byte) 224 /*0xE0*/,
      (byte) 99,
      (byte) 151,
      (byte) 16 /*0x10*/,
      (byte) 22,
      (byte) 223,
      (byte) 89,
      (byte) 41,
      (byte) 74,
      (byte) 117,
      (byte) 98,
      (byte) 125,
      (byte) 92,
      (byte) 139,
      (byte) 110,
      (byte) 171,
      (byte) 82,
      (byte) 233,
      (byte) 164,
      (byte) 59,
      (byte) 132,
      byte.MaxValue,
      (byte) 119,
      (byte) 121
    };
    byte[] numArray10 = new byte[55];
    numArray10[4] = (byte) 199;
    numArray10[1] = (byte) 196;
    numArray10[2] = (byte) 110;
    numArray10[30] = (byte) 125;
    numArray10[39] = (byte) 173;
    numArray10[24] = (byte) 67;
    numArray10[6] = (byte) 56;
    numArray10[7] = (byte) 204;
    numArray10[8] = (byte) 79;
    numArray10[13] = (byte) 220;
    numArray10[26] = (byte) 157;
    numArray10[11] = (byte) 129;
    numArray10[12] = (byte) 102;
    numArray10[10] = (byte) 190;
    numArray10[14] = (byte) 44;
    numArray10[15] = (byte) 209;
    numArray10[50] = (byte) 178;
    numArray10[17] = (byte) 41;
    numArray10[18] = (byte) 244;
    numArray10[19] = (byte) 120;
    numArray10[0] = (byte) 185;
    numArray10[21] = (byte) 64 /*0x40*/;
    numArray10[37] = (byte) 162;
    numArray10[48 /*0x30*/] = (byte) 29;
    numArray10[22] = (byte) 173;
    numArray10[25] = (byte) 110;
    numArray10[36] = (byte) 167;
    numArray10[51] = (byte) 77;
    numArray10[40] = (byte) 173;
    numArray10[16 /*0x10*/] = (byte) 206;
    numArray10[53] = (byte) 216;
    numArray10[31 /*0x1F*/] = (byte) 134;
    numArray10[32 /*0x20*/] = (byte) 136;
    numArray10[33] = (byte) 179;
    numArray10[34] = (byte) 244;
    numArray10[9] = (byte) 146;
    numArray10[3] = (byte) 53;
    numArray10[28] = (byte) 4;
    numArray10[52] = (byte) 162;
    numArray10[5] = (byte) 27;
    numArray10[35] = (byte) 223;
    numArray10[41] = (byte) 201;
    numArray10[42] = (byte) 109;
    numArray10[43] = (byte) 42;
    numArray10[44] = (byte) 134;
    numArray10[45] = (byte) 246;
    numArray10[46] = (byte) 89;
    numArray10[27] = (byte) 70;
    numArray10[47] = (byte) 19;
    numArray10[49] = (byte) 198;
    numArray10[54] = (byte) 189;
    numArray10[20] = (byte) 10;
    numArray10[38] = (byte) 36;
    numArray10[23] = (byte) 215;
    numArray10[29] = (byte) 48 /*0x30*/;
    key.Query(true, 339, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray8, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index] ^= numArray10[index];
    byte[] numArray11 = new byte[55]
    {
      (byte) 115,
      (byte) 73,
      (byte) 186,
      (byte) 218,
      (byte) 99,
      (byte) 170,
      (byte) 210,
      (byte) 26,
      (byte) 110,
      (byte) 105,
      (byte) 39,
      (byte) 141,
      (byte) 173,
      (byte) 19,
      (byte) 205,
      (byte) 84,
      (byte) 199,
      (byte) 14,
      (byte) 86,
      (byte) 51,
      (byte) 195,
      (byte) 82,
      (byte) 12,
      (byte) 187,
      (byte) 11,
      (byte) 8,
      (byte) 27,
      (byte) 78,
      (byte) 220,
      (byte) 213,
      (byte) 174,
      (byte) 226,
      (byte) 19,
      (byte) 237,
      (byte) 105,
      (byte) 160 /*0xA0*/,
      (byte) 147,
      (byte) 81,
      (byte) 200,
      (byte) 60,
      (byte) 6,
      (byte) 105,
      (byte) 206,
      (byte) 176 /*0xB0*/,
      (byte) 61,
      (byte) 170,
      (byte) 48 /*0x30*/,
      (byte) 160 /*0xA0*/,
      (byte) 182,
      (byte) 141,
      (byte) 68,
      (byte) 46,
      (byte) 229,
      (byte) 69,
      (byte) 172
    };
    byte[] numArray12 = new byte[55]
    {
      (byte) 49,
      (byte) 105,
      (byte) 207,
      (byte) 109,
      (byte) 106,
      (byte) 226,
      (byte) 114,
      (byte) 106,
      (byte) 114,
      (byte) 187,
      (byte) 20,
      (byte) 93,
      (byte) 146,
      (byte) 83,
      (byte) 30,
      (byte) 192 /*0xC0*/,
      (byte) 198,
      (byte) 65,
      (byte) 184,
      (byte) 186,
      (byte) 124,
      (byte) 195,
      (byte) 179,
      (byte) 30,
      (byte) 56,
      (byte) 165,
      (byte) 33,
      (byte) 240 /*0xF0*/,
      (byte) 76,
      (byte) 81,
      (byte) 20,
      (byte) 181,
      (byte) 10,
      (byte) 8,
      (byte) 87,
      (byte) 198,
      (byte) 26,
      (byte) 97,
      (byte) 166,
      (byte) 188,
      (byte) 113,
      (byte) 187,
      (byte) 61,
      (byte) 107,
      (byte) 187,
      (byte) 172,
      (byte) 156,
      (byte) 32 /*0x20*/,
      (byte) 38,
      (byte) 185,
      (byte) 120,
      (byte) 109,
      (byte) 221,
      (byte) 26,
      (byte) 76
    };
    key.Query(true, 339, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray8, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index + 55] ^= numArray12[index];
    byte[] numArray13 = new byte[20];
    numArray13[16 /*0x10*/] = (byte) 25;
    numArray13[1] = (byte) 206;
    numArray13[2] = (byte) 90;
    numArray13[13] = (byte) 193;
    numArray13[14] = (byte) 117;
    numArray13[19] = (byte) 132;
    numArray13[6] = (byte) 152;
    numArray13[3] = (byte) 143;
    numArray13[8] = (byte) 69;
    numArray13[9] = (byte) 122;
    numArray13[11] = (byte) 170;
    numArray13[10] = (byte) 191;
    numArray13[12] = (byte) 45;
    numArray13[17] = (byte) 45;
    numArray13[7] = (byte) 187;
    numArray13[4] = (byte) 131;
    numArray13[15] = (byte) 70;
    numArray13[18] = (byte) 90;
    numArray13[5] = (byte) 61;
    numArray13[0] = (byte) 254;
    byte[] numArray14 = new byte[20];
    numArray14[9] = (byte) 182;
    numArray14[1] = (byte) 5;
    numArray14[2] = (byte) 149;
    numArray14[3] = (byte) 83;
    numArray14[11] = (byte) 131;
    numArray14[13] = (byte) 160 /*0xA0*/;
    numArray14[19] = (byte) 10;
    numArray14[7] = (byte) 124;
    numArray14[4] = (byte) 99;
    numArray14[8] = (byte) 73;
    numArray14[12] = (byte) 144 /*0x90*/;
    numArray14[10] = (byte) 135;
    numArray14[17] = (byte) 193;
    numArray14[5] = (byte) 192 /*0xC0*/;
    numArray14[14] = (byte) 120;
    numArray14[15] = (byte) 152;
    numArray14[16 /*0x10*/] = (byte) 240 /*0xF0*/;
    numArray14[0] = (byte) 39;
    numArray14[18] = (byte) 246;
    numArray14[6] = (byte) 99;
    key.Query(true, 339, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray8, 110, 20);
    for (int index = 0; index < 20; ++index)
      numArray8[index + 110] ^= numArray14[index];
    return Encoding.UTF8.GetString(numArray8);
  }

  internal static string ssp_avs_906()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[168];
      byte[] numArray2 = new byte[55]
      {
        (byte) 173,
        (byte) 126,
        (byte) 97,
        (byte) 202,
        (byte) 44,
        (byte) 242,
        (byte) 216,
        (byte) 107,
        (byte) 170,
        (byte) 46,
        (byte) 79,
        (byte) 102,
        (byte) 134,
        (byte) 49,
        (byte) 102,
        (byte) 132,
        (byte) 14,
        (byte) 131,
        (byte) 195,
        (byte) 150,
        (byte) 48 /*0x30*/,
        (byte) 181,
        (byte) 238,
        (byte) 173,
        (byte) 129,
        (byte) 20,
        (byte) 22,
        (byte) 234,
        (byte) 152,
        (byte) 150,
        (byte) 61,
        (byte) 47,
        (byte) 184,
        (byte) 174,
        (byte) 130,
        (byte) 242,
        (byte) 148,
        (byte) 190,
        (byte) 159,
        (byte) 219,
        (byte) 16 /*0x10*/,
        (byte) 96 /*0x60*/,
        (byte) 109,
        (byte) 35,
        (byte) 76,
        (byte) 122,
        (byte) 250,
        (byte) 59,
        (byte) 28,
        (byte) 144 /*0x90*/,
        (byte) 220,
        (byte) 106,
        (byte) 66,
        (byte) 165,
        (byte) 24
      };
      byte[] numArray3 = new byte[55];
      numArray3[50] = (byte) 34;
      numArray3[13] = (byte) 3;
      numArray3[2] = (byte) 169;
      numArray3[29] = (byte) 32 /*0x20*/;
      numArray3[0] = (byte) 47;
      numArray3[5] = (byte) 5;
      numArray3[31 /*0x1F*/] = (byte) 8;
      numArray3[7] = (byte) 189;
      numArray3[16 /*0x10*/] = (byte) 173;
      numArray3[49] = (byte) 139;
      numArray3[44] = (byte) 49;
      numArray3[11] = (byte) 207;
      numArray3[36] = (byte) 170;
      numArray3[21] = (byte) 45;
      numArray3[3] = (byte) 32 /*0x20*/;
      numArray3[47] = (byte) 114;
      numArray3[12] = (byte) 187;
      numArray3[30] = (byte) 168;
      numArray3[6] = (byte) 134;
      numArray3[43] = (byte) 116;
      numArray3[20] = (byte) 236;
      numArray3[27] = (byte) 24;
      numArray3[22] = (byte) 199;
      numArray3[23] = (byte) 13;
      numArray3[1] = (byte) 62;
      numArray3[25] = (byte) 21;
      numArray3[38] = (byte) 105;
      numArray3[8] = (byte) 235;
      numArray3[28] = (byte) 159;
      numArray3[15] = (byte) 211;
      numArray3[17] = (byte) 218;
      numArray3[9] = (byte) 8;
      numArray3[10] = (byte) 144 /*0x90*/;
      numArray3[33] = (byte) 37;
      numArray3[26] = (byte) 216;
      numArray3[35] = (byte) 246;
      numArray3[18] = (byte) 4;
      numArray3[37] = (byte) 53;
      numArray3[32 /*0x20*/] = (byte) 19;
      numArray3[39] = (byte) 222;
      numArray3[40] = (byte) 105;
      numArray3[52] = (byte) 39;
      numArray3[42] = (byte) 97;
      numArray3[19] = (byte) 204;
      numArray3[54] = (byte) 45;
      numArray3[41] = (byte) 43;
      numArray3[14] = (byte) 97;
      numArray3[51] = (byte) 140;
      numArray3[48 /*0x30*/] = (byte) 216;
      numArray3[46] = (byte) 141;
      numArray3[4] = (byte) 131;
      numArray3[45] = (byte) 197;
      numArray3[34] = (byte) 54;
      numArray3[53] = (byte) 58;
      numArray3[24] = (byte) 98;
      key.Query(true, 339, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 83,
        (byte) 66,
        (byte) 253,
        (byte) 1,
        (byte) 31 /*0x1F*/,
        (byte) 202,
        (byte) 89,
        (byte) 30,
        (byte) 51,
        (byte) 155,
        (byte) 64 /*0x40*/,
        (byte) 36,
        (byte) 17,
        (byte) 14,
        (byte) 219,
        (byte) 93,
        (byte) 232,
        (byte) 100,
        (byte) 67,
        (byte) 239,
        (byte) 152,
        (byte) 51,
        (byte) 58,
        (byte) 247,
        (byte) 78,
        (byte) 38,
        (byte) 104,
        (byte) 46,
        (byte) 76,
        (byte) 145,
        (byte) 243,
        (byte) 79,
        (byte) 220,
        (byte) 158,
        (byte) 141,
        (byte) 50,
        (byte) 137,
        (byte) 253,
        (byte) 2,
        (byte) 195,
        byte.MaxValue,
        (byte) 19,
        (byte) 249,
        (byte) 49,
        (byte) 226,
        (byte) 104,
        (byte) 150,
        (byte) 145,
        (byte) 176 /*0xB0*/,
        (byte) 41,
        (byte) 115,
        (byte) 187,
        (byte) 4,
        (byte) 191,
        (byte) 44
      };
      byte[] numArray5 = new byte[55];
      numArray5[53] = (byte) 27;
      numArray5[35] = (byte) 144 /*0x90*/;
      numArray5[45] = (byte) 230;
      numArray5[3] = (byte) 52;
      numArray5[4] = (byte) 112 /*0x70*/;
      numArray5[5] = (byte) 171;
      numArray5[23] = (byte) 98;
      numArray5[46] = (byte) 187;
      numArray5[7] = (byte) 111;
      numArray5[50] = (byte) 201;
      numArray5[13] = (byte) 94;
      numArray5[11] = (byte) 253;
      numArray5[14] = (byte) 24;
      numArray5[18] = (byte) 221;
      numArray5[49] = (byte) 194;
      numArray5[15] = (byte) 251;
      numArray5[33] = (byte) 65;
      numArray5[17] = (byte) 213;
      numArray5[41] = (byte) 47;
      numArray5[19] = (byte) 225;
      numArray5[2] = (byte) 100;
      numArray5[21] = (byte) 173;
      numArray5[22] = (byte) 241;
      numArray5[43] = (byte) 226;
      numArray5[0] = (byte) 128 /*0x80*/;
      numArray5[26] = (byte) 148;
      numArray5[25] = (byte) 166;
      numArray5[27] = (byte) 15;
      numArray5[20] = (byte) 38;
      numArray5[8] = (byte) 10;
      numArray5[30] = (byte) 59;
      numArray5[24] = (byte) 165;
      numArray5[39] = (byte) 166;
      numArray5[31 /*0x1F*/] = (byte) 200;
      numArray5[32 /*0x20*/] = (byte) 39;
      numArray5[1] = (byte) 145;
      numArray5[36] = (byte) 6;
      numArray5[37] = (byte) 28;
      numArray5[38] = (byte) 56;
      numArray5[9] = (byte) 8;
      numArray5[40] = (byte) 237;
      numArray5[34] = (byte) 30;
      numArray5[42] = (byte) 17;
      numArray5[12] = (byte) 64 /*0x40*/;
      numArray5[44] = (byte) 147;
      numArray5[10] = (byte) 10;
      numArray5[16 /*0x10*/] = (byte) 14;
      numArray5[47] = (byte) 181;
      numArray5[48 /*0x30*/] = (byte) 92;
      numArray5[29] = (byte) 69;
      numArray5[6] = (byte) 47;
      numArray5[51] = (byte) 141;
      numArray5[52] = (byte) 161;
      numArray5[28] = (byte) 246;
      numArray5[54] = (byte) 247;
      key.Query(true, 339, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[55];
      numArray6[24] = (byte) 197;
      numArray6[1] = (byte) 205;
      numArray6[4] = (byte) 173;
      numArray6[37] = (byte) 214;
      numArray6[17] = (byte) 28;
      numArray6[22] = (byte) 197;
      numArray6[6] = (byte) 132;
      numArray6[33] = (byte) 44;
      numArray6[8] = (byte) 166;
      numArray6[9] = (byte) 42;
      numArray6[10] = (byte) 1;
      numArray6[2] = (byte) 105;
      numArray6[25] = (byte) 119;
      numArray6[34] = (byte) 145;
      numArray6[14] = (byte) 159;
      numArray6[15] = (byte) 88;
      numArray6[16 /*0x10*/] = (byte) 30;
      numArray6[38] = (byte) 38;
      numArray6[40] = (byte) 210;
      numArray6[19] = (byte) 227;
      numArray6[20] = (byte) 128 /*0x80*/;
      numArray6[21] = (byte) 36;
      numArray6[13] = (byte) 80 /*0x50*/;
      numArray6[23] = (byte) 107;
      numArray6[18] = (byte) 67;
      numArray6[45] = (byte) 253;
      numArray6[0] = (byte) 241;
      numArray6[27] = (byte) 148;
      numArray6[28] = (byte) 223;
      numArray6[29] = (byte) 198;
      numArray6[30] = (byte) 77;
      numArray6[31 /*0x1F*/] = (byte) 201;
      numArray6[32 /*0x20*/] = (byte) 137;
      numArray6[12] = (byte) 127 /*0x7F*/;
      numArray6[3] = (byte) 139;
      numArray6[26] = (byte) 205;
      numArray6[53] = (byte) 1;
      numArray6[54] = (byte) 125;
      numArray6[50] = (byte) 101;
      numArray6[35] = (byte) 124;
      numArray6[5] = (byte) 175;
      numArray6[7] = (byte) 111;
      numArray6[42] = (byte) 73;
      numArray6[49] = (byte) 0;
      numArray6[44] = (byte) 33;
      numArray6[48 /*0x30*/] = (byte) 233;
      numArray6[46] = (byte) 135;
      numArray6[47] = (byte) 4;
      numArray6[39] = (byte) 145;
      numArray6[11] = (byte) 18;
      numArray6[36] = (byte) 159;
      numArray6[51] = (byte) 179;
      numArray6[43] = (byte) 245;
      numArray6[41] = (byte) 26;
      numArray6[52] = (byte) 100;
      byte[] numArray7 = new byte[55];
      numArray7[0] = (byte) 201;
      numArray7[27] = (byte) 254;
      numArray7[2] = (byte) 244;
      numArray7[13] = (byte) 194;
      numArray7[43] = (byte) 225;
      numArray7[30] = (byte) 240 /*0xF0*/;
      numArray7[6] = (byte) 254;
      numArray7[7] = (byte) 28;
      numArray7[33] = (byte) 183;
      numArray7[17] = (byte) 217;
      numArray7[10] = (byte) 174;
      numArray7[42] = (byte) 28;
      numArray7[51] = (byte) 221;
      numArray7[1] = (byte) 149;
      numArray7[14] = (byte) 170;
      numArray7[26] = (byte) 86;
      numArray7[16 /*0x10*/] = (byte) 215;
      numArray7[44] = (byte) 62;
      numArray7[50] = (byte) 51;
      numArray7[32 /*0x20*/] = (byte) 119;
      numArray7[11] = (byte) 63 /*0x3F*/;
      numArray7[21] = (byte) 223;
      numArray7[22] = (byte) 45;
      numArray7[23] = (byte) 158;
      numArray7[37] = (byte) 6;
      numArray7[25] = (byte) 242;
      numArray7[3] = (byte) 195;
      numArray7[45] = (byte) 216;
      numArray7[19] = (byte) 164;
      numArray7[29] = (byte) 102;
      numArray7[15] = (byte) 51;
      numArray7[9] = (byte) 79;
      numArray7[40] = (byte) 95;
      numArray7[41] = (byte) 164;
      numArray7[34] = (byte) 76;
      numArray7[35] = (byte) 79;
      numArray7[36] = (byte) 146;
      numArray7[8] = (byte) 115;
      numArray7[28] = (byte) 95;
      numArray7[20] = (byte) 249;
      numArray7[5] = (byte) 168;
      numArray7[46] = (byte) 58;
      numArray7[12] = (byte) 168;
      numArray7[18] = (byte) 109;
      numArray7[39] = (byte) 53;
      numArray7[24] = (byte) 48 /*0x30*/;
      numArray7[38] = (byte) 225;
      numArray7[47] = (byte) 169;
      numArray7[48 /*0x30*/] = (byte) 70;
      numArray7[49] = (byte) 137;
      numArray7[4] = (byte) 93;
      numArray7[31 /*0x1F*/] = (byte) 171;
      numArray7[52] = (byte) 19;
      numArray7[53] = (byte) 23;
      numArray7[54] = (byte) 23;
      key.Query(true, 339, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[3]
      {
        (byte) 82,
        (byte) 168,
        (byte) 139
      };
      byte[] numArray9 = new byte[3]
      {
        (byte) 0,
        (byte) 87,
        (byte) 0
      };
      numArray9[0] = (byte) 101;
      numArray9[2] = (byte) 236;
      key.Query(true, 339, numArray8, numArray8);
      Array.Copy((Array) numArray8, 0, (Array) numArray1, 165, 3);
      for (int index = 0; index < 3; ++index)
        numArray1[index + 165] ^= numArray9[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray10 = new byte[168];
    byte[] numArray11 = new byte[55];
    numArray11[20] = byte.MaxValue;
    numArray11[1] = (byte) 13;
    numArray11[2] = (byte) 39;
    numArray11[39] = (byte) 195;
    numArray11[4] = (byte) 41;
    numArray11[5] = (byte) 34;
    numArray11[6] = (byte) 91;
    numArray11[7] = (byte) 106;
    numArray11[49] = (byte) 95;
    numArray11[9] = (byte) 1;
    numArray11[10] = (byte) 135;
    numArray11[23] = (byte) 82;
    numArray11[54] = (byte) 36;
    numArray11[47] = (byte) 197;
    numArray11[43] = (byte) 58;
    numArray11[33] = (byte) 240 /*0xF0*/;
    numArray11[15] = (byte) 54;
    numArray11[17] = (byte) 103;
    numArray11[18] = (byte) 93;
    numArray11[19] = (byte) 37;
    numArray11[52] = (byte) 222;
    numArray11[21] = (byte) 209;
    numArray11[50] = (byte) 143;
    numArray11[46] = (byte) 6;
    numArray11[24] = (byte) 185;
    numArray11[8] = (byte) 142;
    numArray11[28] = (byte) 1;
    numArray11[44] = (byte) 248;
    numArray11[32 /*0x20*/] = (byte) 113;
    numArray11[38] = (byte) 6;
    numArray11[30] = (byte) 192 /*0xC0*/;
    numArray11[25] = (byte) 155;
    numArray11[22] = (byte) 89;
    numArray11[48 /*0x30*/] = (byte) 96 /*0x60*/;
    numArray11[12] = (byte) 76;
    numArray11[35] = (byte) 130;
    numArray11[36] = (byte) 36;
    numArray11[37] = (byte) 161;
    numArray11[27] = (byte) 30;
    numArray11[45] = (byte) 160 /*0xA0*/;
    numArray11[11] = (byte) 224 /*0xE0*/;
    numArray11[41] = (byte) 71;
    numArray11[42] = (byte) 53;
    numArray11[13] = (byte) 226;
    numArray11[51] = (byte) 177;
    numArray11[0] = (byte) 108;
    numArray11[31 /*0x1F*/] = (byte) 174;
    numArray11[29] = (byte) 63 /*0x3F*/;
    numArray11[3] = (byte) 116;
    numArray11[26] = (byte) 107;
    numArray11[16 /*0x10*/] = (byte) 207;
    numArray11[14] = (byte) 42;
    numArray11[40] = (byte) 91;
    numArray11[53] = (byte) 63 /*0x3F*/;
    numArray11[34] = (byte) 5;
    byte[] numArray12 = new byte[55];
    numArray12[34] = (byte) 27;
    numArray12[1] = (byte) 91;
    numArray12[2] = (byte) 54;
    numArray12[47] = (byte) 46;
    numArray12[25] = (byte) 183;
    numArray12[0] = (byte) 211;
    numArray12[6] = (byte) 234;
    numArray12[41] = (byte) 24;
    numArray12[8] = (byte) 70;
    numArray12[16 /*0x10*/] = (byte) 29;
    numArray12[10] = (byte) 73;
    numArray12[11] = (byte) 176 /*0xB0*/;
    numArray12[27] = (byte) 96 /*0x60*/;
    numArray12[13] = (byte) 9;
    numArray12[14] = (byte) 135;
    numArray12[12] = (byte) 25;
    numArray12[21] = (byte) 79;
    numArray12[17] = (byte) 105;
    numArray12[18] = (byte) 163;
    numArray12[19] = (byte) 62;
    numArray12[20] = (byte) 125;
    numArray12[40] = (byte) 51;
    numArray12[37] = (byte) 72;
    numArray12[7] = (byte) 155;
    numArray12[24] = (byte) 148;
    numArray12[54] = (byte) 135;
    numArray12[26] = (byte) 67;
    numArray12[5] = (byte) 40;
    numArray12[32 /*0x20*/] = (byte) 213;
    numArray12[29] = (byte) 138;
    numArray12[30] = (byte) 196;
    numArray12[31 /*0x1F*/] = (byte) 207;
    numArray12[22] = (byte) 44;
    numArray12[42] = (byte) 157;
    numArray12[3] = (byte) 213;
    numArray12[43] = (byte) 0;
    numArray12[36] = (byte) 87;
    numArray12[9] = (byte) 180;
    numArray12[33] = (byte) 238;
    numArray12[39] = (byte) 146;
    numArray12[4] = (byte) 76;
    numArray12[44] = (byte) 121;
    numArray12[38] = (byte) 85;
    numArray12[35] = (byte) 125;
    numArray12[45] = (byte) 120;
    numArray12[15] = (byte) 182;
    numArray12[46] = (byte) 76;
    numArray12[50] = (byte) 89;
    numArray12[48 /*0x30*/] = (byte) 4;
    numArray12[49] = (byte) 211;
    numArray12[28] = (byte) 106;
    numArray12[51] = (byte) 235;
    numArray12[52] = (byte) 135;
    numArray12[53] = (byte) 1;
    numArray12[23] = (byte) 247;
    key.Query(true, 339, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray10, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index] ^= numArray12[index];
    byte[] numArray13 = new byte[55]
    {
      (byte) 61,
      (byte) 187,
      (byte) 16 /*0x10*/,
      (byte) 81,
      (byte) 122,
      (byte) 115,
      (byte) 81,
      (byte) 116,
      (byte) 127 /*0x7F*/,
      (byte) 128 /*0x80*/,
      (byte) 47,
      (byte) 6,
      (byte) 158,
      (byte) 235,
      (byte) 1,
      (byte) 108,
      (byte) 145,
      (byte) 13,
      (byte) 195,
      (byte) 111,
      (byte) 94,
      (byte) 40,
      (byte) 243,
      (byte) 206,
      (byte) 73,
      (byte) 36,
      (byte) 200,
      (byte) 50,
      (byte) 88,
      (byte) 211,
      (byte) 244,
      (byte) 204,
      (byte) 36,
      (byte) 202,
      (byte) 152,
      (byte) 137,
      (byte) 109,
      (byte) 170,
      (byte) 35,
      (byte) 163,
      (byte) 245,
      (byte) 82,
      (byte) 147,
      (byte) 115,
      (byte) 235,
      (byte) 110,
      (byte) 65,
      (byte) 160 /*0xA0*/,
      (byte) 25,
      (byte) 250,
      (byte) 37,
      (byte) 187,
      (byte) 207,
      (byte) 216,
      (byte) 17
    };
    byte[] numArray14 = new byte[55]
    {
      (byte) 248,
      (byte) 152,
      (byte) 120,
      (byte) 98,
      (byte) 172,
      (byte) 17,
      (byte) 113,
      (byte) 63 /*0x3F*/,
      (byte) 8,
      (byte) 37,
      (byte) 118,
      (byte) 217,
      (byte) 246,
      (byte) 238,
      (byte) 62,
      (byte) 97,
      (byte) 201,
      (byte) 252,
      (byte) 241,
      (byte) 26,
      (byte) 103,
      (byte) 12,
      (byte) 155,
      (byte) 226,
      (byte) 172,
      (byte) 134,
      (byte) 197,
      (byte) 200,
      (byte) 252,
      (byte) 20,
      (byte) 93,
      (byte) 212,
      (byte) 105,
      (byte) 140,
      (byte) 51,
      (byte) 38,
      (byte) 253,
      (byte) 188,
      (byte) 218,
      (byte) 56,
      (byte) 23,
      (byte) 133,
      (byte) 73,
      (byte) 78,
      (byte) 24,
      (byte) 37,
      (byte) 33,
      (byte) 35,
      (byte) 6,
      (byte) 125,
      (byte) 240 /*0xF0*/,
      (byte) 142,
      (byte) 116,
      (byte) 79,
      (byte) 212
    };
    key.Query(true, 339, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray10, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index + 55] ^= numArray14[index];
    byte[] numArray15 = new byte[55];
    numArray15[32 /*0x20*/] = (byte) 130;
    numArray15[22] = (byte) 218;
    numArray15[2] = (byte) 85;
    numArray15[35] = (byte) 161;
    numArray15[4] = (byte) 30;
    numArray15[31 /*0x1F*/] = (byte) 185;
    numArray15[6] = (byte) 172;
    numArray15[7] = (byte) 140;
    numArray15[0] = (byte) 65;
    numArray15[30] = (byte) 75;
    numArray15[34] = (byte) 43;
    numArray15[11] = (byte) 83;
    numArray15[12] = (byte) 119;
    numArray15[13] = (byte) 254;
    numArray15[39] = (byte) 53;
    numArray15[15] = (byte) 233;
    numArray15[16 /*0x10*/] = (byte) 32 /*0x20*/;
    numArray15[36] = (byte) 206;
    numArray15[18] = (byte) 12;
    numArray15[50] = (byte) 175;
    numArray15[20] = (byte) 128 /*0x80*/;
    numArray15[21] = (byte) 226;
    numArray15[42] = (byte) 136;
    numArray15[23] = (byte) 227;
    numArray15[24] = (byte) 40;
    numArray15[25] = (byte) 24;
    numArray15[26] = (byte) 94;
    numArray15[38] = (byte) 215;
    numArray15[10] = (byte) 212;
    numArray15[29] = (byte) 252;
    numArray15[41] = (byte) 231;
    numArray15[37] = (byte) 248;
    numArray15[14] = (byte) 231;
    numArray15[33] = (byte) 194;
    numArray15[40] = (byte) 231;
    numArray15[48 /*0x30*/] = (byte) 62;
    numArray15[43] = (byte) 139;
    numArray15[47] = (byte) 251;
    numArray15[3] = (byte) 170;
    numArray15[8] = (byte) 99;
    numArray15[46] = (byte) 162;
    numArray15[53] = (byte) 129;
    numArray15[19] = (byte) 148;
    numArray15[28] = (byte) 56;
    numArray15[44] = (byte) 109;
    numArray15[45] = (byte) 76;
    numArray15[17] = (byte) 122;
    numArray15[1] = (byte) 8;
    numArray15[27] = (byte) 173;
    numArray15[49] = (byte) 209;
    numArray15[51] = (byte) 94;
    numArray15[9] = (byte) 226;
    numArray15[52] = (byte) 4;
    numArray15[5] = (byte) 28;
    numArray15[54] = (byte) 71;
    byte[] numArray16 = new byte[55];
    numArray16[34] = (byte) 232;
    numArray16[53] = (byte) 237;
    numArray16[43] = (byte) 141;
    numArray16[22] = (byte) 96 /*0x60*/;
    numArray16[38] = (byte) 20;
    numArray16[36] = (byte) 161;
    numArray16[6] = (byte) 227;
    numArray16[32 /*0x20*/] = (byte) 3;
    numArray16[28] = (byte) 231;
    numArray16[23] = (byte) 91;
    numArray16[10] = (byte) 153;
    numArray16[11] = (byte) 163;
    numArray16[49] = (byte) 226;
    numArray16[54] = (byte) 207;
    numArray16[8] = (byte) 56;
    numArray16[5] = (byte) 81;
    numArray16[25] = (byte) 186;
    numArray16[12] = (byte) 167;
    numArray16[18] = (byte) 249;
    numArray16[2] = (byte) 212;
    numArray16[20] = (byte) 158;
    numArray16[21] = (byte) 141;
    numArray16[45] = (byte) 136;
    numArray16[7] = (byte) 104;
    numArray16[19] = (byte) 179;
    numArray16[24] = (byte) 237;
    numArray16[9] = (byte) 114;
    numArray16[4] = (byte) 199;
    numArray16[41] = (byte) 240 /*0xF0*/;
    numArray16[29] = (byte) 152;
    numArray16[3] = byte.MaxValue;
    numArray16[17] = (byte) 44;
    numArray16[26] = (byte) 18;
    numArray16[44] = (byte) 125;
    numArray16[15] = (byte) 246;
    numArray16[35] = (byte) 191;
    numArray16[1] = (byte) 209;
    numArray16[37] = (byte) 52;
    numArray16[31 /*0x1F*/] = (byte) 125;
    numArray16[39] = (byte) 210;
    numArray16[47] = (byte) 92;
    numArray16[33] = (byte) 198;
    numArray16[42] = (byte) 131;
    numArray16[48 /*0x30*/] = (byte) 86;
    numArray16[14] = (byte) 61;
    numArray16[0] = (byte) 118;
    numArray16[16 /*0x10*/] = (byte) 249;
    numArray16[30] = (byte) 141;
    numArray16[40] = (byte) 36;
    numArray16[50] = (byte) 198;
    numArray16[13] = (byte) 112 /*0x70*/;
    numArray16[51] = (byte) 89;
    numArray16[52] = (byte) 96 /*0x60*/;
    numArray16[27] = (byte) 169;
    numArray16[46] = (byte) 99;
    key.Query(true, 339, numArray15, numArray15);
    Array.Copy((Array) numArray15, 0, (Array) numArray10, 110, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index + 110] ^= numArray16[index];
    byte[] numArray17 = new byte[3]
    {
      (byte) 217,
      (byte) 72,
      (byte) 4
    };
    byte[] numArray18 = new byte[3]
    {
      (byte) 196,
      (byte) 39,
      (byte) 225
    };
    key.Query(true, 339, numArray17, numArray17);
    Array.Copy((Array) numArray17, 0, (Array) numArray10, 165, 3);
    for (int index = 0; index < 3; ++index)
      numArray10[index + 165] ^= numArray18[index];
    return Encoding.UTF8.GetString(numArray10);
  }
}
