// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_886
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_886
{
  private static byte[] sspq = new byte[63 /*0x3F*/]
  {
    (byte) 59,
    (byte) 12,
    (byte) 175,
    (byte) 173,
    (byte) 244,
    (byte) 50,
    (byte) 173,
    (byte) 21,
    (byte) 35,
    (byte) 251,
    (byte) 146,
    (byte) 103,
    (byte) 229,
    (byte) 176 /*0xB0*/,
    (byte) 8,
    (byte) 65,
    (byte) 155,
    (byte) 189,
    (byte) 51,
    (byte) 37,
    (byte) 20,
    (byte) 49,
    (byte) 27,
    (byte) 198,
    (byte) 238,
    (byte) 209,
    (byte) 236,
    (byte) 124,
    (byte) 209,
    (byte) 6,
    (byte) 206,
    (byte) 228,
    (byte) 241,
    (byte) 18,
    (byte) 225,
    (byte) 100,
    (byte) 87,
    (byte) 48 /*0x30*/,
    (byte) 246,
    (byte) 81,
    (byte) 156,
    (byte) 12,
    (byte) 136,
    (byte) 218,
    (byte) 230,
    (byte) 197,
    (byte) 229,
    (byte) 50,
    (byte) 21,
    (byte) 228,
    (byte) 67,
    (byte) 157,
    (byte) 134,
    (byte) 250,
    (byte) 50,
    (byte) 64 /*0x40*/,
    (byte) 225,
    (byte) 19,
    (byte) 99,
    (byte) 241,
    (byte) 152,
    (byte) 115,
    (byte) 76
  };
  private static byte[] sspr = new byte[63 /*0x3F*/]
  {
    (byte) 175,
    (byte) 59,
    (byte) 116,
    (byte) 128 /*0x80*/,
    (byte) 162,
    (byte) 232,
    (byte) 225,
    (byte) 40,
    (byte) 101,
    (byte) 47,
    (byte) 153,
    (byte) 181,
    (byte) 122,
    (byte) 135,
    (byte) 43,
    (byte) 158,
    (byte) 198,
    (byte) 222,
    (byte) 9,
    (byte) 184,
    (byte) 233,
    (byte) 233,
    (byte) 2,
    (byte) 16 /*0x10*/,
    (byte) 109,
    (byte) 18,
    (byte) 33,
    (byte) 164,
    (byte) 56,
    (byte) 237,
    (byte) 142,
    (byte) 173,
    (byte) 111,
    (byte) 146,
    (byte) 239,
    (byte) 22,
    (byte) 223,
    (byte) 119,
    (byte) 13,
    (byte) 42,
    (byte) 220,
    (byte) 53,
    (byte) 48 /*0x30*/,
    (byte) 170,
    (byte) 177,
    (byte) 111,
    (byte) 91,
    (byte) 216,
    (byte) 211,
    (byte) 64 /*0x40*/,
    (byte) 151,
    (byte) 130,
    (byte) 224 /*0xE0*/,
    (byte) 154,
    (byte) 129,
    (byte) 26,
    (byte) 37,
    (byte) 35,
    (byte) 129,
    (byte) 232,
    (byte) 237,
    (byte) 168,
    (byte) 7
  };

  internal static string ssp_avs_887()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[236];
      byte[] numArray2 = new byte[55];
      numArray2[38] = (byte) 94;
      numArray2[1] = (byte) 156;
      numArray2[15] = (byte) 153;
      numArray2[31 /*0x1F*/] = (byte) 188;
      numArray2[24] = (byte) 142;
      numArray2[5] = (byte) 105;
      numArray2[6] = (byte) 93;
      numArray2[36] = (byte) 103;
      numArray2[8] = (byte) 75;
      numArray2[9] = (byte) 66;
      numArray2[10] = (byte) 32 /*0x20*/;
      numArray2[11] = (byte) 51;
      numArray2[12] = (byte) 36;
      numArray2[39] = (byte) 249;
      numArray2[14] = (byte) 245;
      numArray2[53] = (byte) 227;
      numArray2[44] = (byte) 3;
      numArray2[18] = (byte) 134;
      numArray2[7] = (byte) 196;
      numArray2[35] = (byte) 218;
      numArray2[34] = (byte) 192 /*0xC0*/;
      numArray2[51] = (byte) 144 /*0x90*/;
      numArray2[22] = (byte) 128 /*0x80*/;
      numArray2[23] = (byte) 10;
      numArray2[37] = (byte) 181;
      numArray2[48 /*0x30*/] = (byte) 117;
      numArray2[54] = (byte) 197;
      numArray2[27] = (byte) 179;
      numArray2[21] = (byte) 82;
      numArray2[29] = (byte) 160 /*0xA0*/;
      numArray2[30] = (byte) 56;
      numArray2[25] = (byte) 40;
      numArray2[47] = (byte) 162;
      numArray2[41] = (byte) 81;
      numArray2[17] = (byte) 110;
      numArray2[49] = (byte) 29;
      numArray2[13] = (byte) 12;
      numArray2[33] = (byte) 230;
      numArray2[19] = (byte) 13;
      numArray2[4] = (byte) 114;
      numArray2[40] = (byte) 94;
      numArray2[3] = (byte) 208 /*0xD0*/;
      numArray2[42] = (byte) 83;
      numArray2[43] = (byte) 88;
      numArray2[20] = (byte) 42;
      numArray2[32 /*0x20*/] = (byte) 164;
      numArray2[46] = (byte) 81;
      numArray2[0] = (byte) 252;
      numArray2[52] = (byte) 183;
      numArray2[2] = (byte) 244;
      numArray2[50] = (byte) 69;
      numArray2[16 /*0x10*/] = (byte) 131;
      numArray2[28] = (byte) 159;
      numArray2[26] = (byte) 35;
      numArray2[45] = (byte) 129;
      byte[] numArray3 = new byte[55];
      numArray3[10] = (byte) 174;
      numArray3[9] = (byte) 57;
      numArray3[2] = (byte) 224 /*0xE0*/;
      numArray3[5] = (byte) 104;
      numArray3[23] = (byte) 220;
      numArray3[36] = (byte) 67;
      numArray3[33] = (byte) 18;
      numArray3[7] = (byte) 194;
      numArray3[13] = (byte) 158;
      numArray3[34] = (byte) 185;
      numArray3[31 /*0x1F*/] = (byte) 251;
      numArray3[11] = (byte) 20;
      numArray3[6] = (byte) 241;
      numArray3[17] = (byte) 119;
      numArray3[14] = (byte) 18;
      numArray3[15] = (byte) 219;
      numArray3[4] = (byte) 242;
      numArray3[35] = (byte) 10;
      numArray3[16 /*0x10*/] = (byte) 48 /*0x30*/;
      numArray3[21] = (byte) 60;
      numArray3[25] = (byte) 93;
      numArray3[50] = (byte) 4;
      numArray3[22] = (byte) 161;
      numArray3[32 /*0x20*/] = (byte) 219;
      numArray3[0] = (byte) 230;
      numArray3[20] = (byte) 60;
      numArray3[26] = (byte) 61;
      numArray3[27] = (byte) 126;
      numArray3[28] = (byte) 32 /*0x20*/;
      numArray3[37] = (byte) 172;
      numArray3[30] = (byte) 54;
      numArray3[29] = (byte) 205;
      numArray3[46] = (byte) 142;
      numArray3[18] = (byte) 54;
      numArray3[43] = (byte) 215;
      numArray3[19] = (byte) 39;
      numArray3[41] = (byte) 235;
      numArray3[24] = (byte) 166;
      numArray3[38] = (byte) 54;
      numArray3[44] = (byte) 131;
      numArray3[40] = (byte) 166;
      numArray3[53] = (byte) 218;
      numArray3[42] = (byte) 135;
      numArray3[12] = (byte) 92;
      numArray3[39] = (byte) 172;
      numArray3[45] = (byte) 246;
      numArray3[1] = (byte) 109;
      numArray3[47] = (byte) 103;
      numArray3[48 /*0x30*/] = (byte) 149;
      numArray3[49] = (byte) 225;
      numArray3[8] = (byte) 34;
      numArray3[51] = (byte) 183;
      numArray3[52] = (byte) 247;
      numArray3[3] = (byte) 24;
      numArray3[54] = (byte) 23;
      key.Query(true, 339, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55];
      numArray4[14] = (byte) 27;
      numArray4[8] = (byte) 133;
      numArray4[45] = (byte) 89;
      numArray4[3] = (byte) 153;
      numArray4[49] = (byte) 40;
      numArray4[10] = (byte) 232;
      numArray4[34] = (byte) 32 /*0x20*/;
      numArray4[1] = (byte) 25;
      numArray4[9] = (byte) 63 /*0x3F*/;
      numArray4[19] = (byte) 252;
      numArray4[29] = (byte) 136;
      numArray4[47] = (byte) 30;
      numArray4[12] = (byte) 144 /*0x90*/;
      numArray4[13] = (byte) 24;
      numArray4[4] = (byte) 136;
      numArray4[24] = (byte) 8;
      numArray4[16 /*0x10*/] = (byte) 233;
      numArray4[17] = (byte) 178;
      numArray4[28] = (byte) 164;
      numArray4[36] = (byte) 241;
      numArray4[43] = (byte) 230;
      numArray4[20] = (byte) 236;
      numArray4[22] = (byte) 244;
      numArray4[23] = (byte) 202;
      numArray4[7] = (byte) 58;
      numArray4[31 /*0x1F*/] = (byte) 242;
      numArray4[18] = (byte) 36;
      numArray4[27] = (byte) 197;
      numArray4[26] = (byte) 94;
      numArray4[40] = (byte) 236;
      numArray4[37] = (byte) 86;
      numArray4[52] = (byte) 29;
      numArray4[32 /*0x20*/] = (byte) 23;
      numArray4[33] = (byte) 177;
      numArray4[15] = (byte) 90;
      numArray4[35] = (byte) 37;
      numArray4[2] = (byte) 176 /*0xB0*/;
      numArray4[0] = (byte) 122;
      numArray4[38] = (byte) 192 /*0xC0*/;
      numArray4[39] = (byte) 121;
      numArray4[21] = (byte) 40;
      numArray4[41] = (byte) 178;
      numArray4[42] = (byte) 186;
      numArray4[30] = (byte) 82;
      numArray4[44] = (byte) 15;
      numArray4[11] = (byte) 200;
      numArray4[46] = (byte) 109;
      numArray4[53] = (byte) 87;
      numArray4[48 /*0x30*/] = (byte) 2;
      numArray4[5] = (byte) 14;
      numArray4[50] = (byte) 103;
      numArray4[51] = (byte) 136;
      numArray4[25] = (byte) 230;
      numArray4[6] = (byte) 108;
      numArray4[54] = (byte) 45;
      byte[] numArray5 = new byte[55]
      {
        (byte) 109,
        (byte) 131,
        (byte) 20,
        (byte) 200,
        (byte) 24,
        (byte) 51,
        (byte) 213,
        (byte) 132,
        (byte) 52,
        (byte) 172,
        (byte) 66,
        (byte) 251,
        (byte) 92,
        (byte) 6,
        (byte) 140,
        (byte) 249,
        (byte) 200,
        (byte) 114,
        (byte) 207,
        (byte) 146,
        (byte) 55,
        (byte) 170,
        (byte) 222,
        (byte) 49,
        (byte) 84,
        (byte) 52,
        (byte) 122,
        (byte) 247,
        (byte) 148,
        (byte) 248,
        (byte) 18,
        (byte) 18,
        (byte) 40,
        (byte) 46,
        (byte) 27,
        (byte) 76,
        (byte) 226,
        (byte) 119,
        (byte) 102,
        (byte) 28,
        (byte) 4,
        (byte) 123,
        (byte) 56,
        (byte) 238,
        (byte) 123,
        (byte) 53,
        (byte) 37,
        (byte) 134,
        (byte) 103,
        (byte) 45,
        (byte) 214,
        (byte) 171,
        (byte) 191,
        (byte) 214,
        (byte) 75
      };
      key.Query(true, 339, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[55]
      {
        (byte) 42,
        (byte) 226,
        (byte) 41,
        (byte) 111,
        (byte) 228,
        (byte) 26,
        (byte) 219,
        (byte) 34,
        (byte) 32 /*0x20*/,
        (byte) 242,
        (byte) 91,
        (byte) 42,
        (byte) 245,
        (byte) 92,
        (byte) 205,
        (byte) 211,
        (byte) 225,
        (byte) 203,
        (byte) 29,
        (byte) 176 /*0xB0*/,
        (byte) 21,
        (byte) 162,
        (byte) 7,
        (byte) 234,
        (byte) 26,
        (byte) 181,
        (byte) 187,
        (byte) 75,
        (byte) 52,
        (byte) 99,
        (byte) 119,
        (byte) 144 /*0x90*/,
        (byte) 232,
        (byte) 233,
        (byte) 225,
        (byte) 226,
        (byte) 244,
        (byte) 89,
        (byte) 96 /*0x60*/,
        (byte) 1,
        (byte) 225,
        (byte) 213,
        (byte) 248,
        (byte) 181,
        (byte) 50,
        (byte) 250,
        (byte) 23,
        (byte) 98,
        (byte) 7,
        (byte) 194,
        (byte) 144 /*0x90*/,
        (byte) 124,
        (byte) 213,
        (byte) 52,
        (byte) 1
      };
      byte[] numArray7 = new byte[55]
      {
        (byte) 19,
        (byte) 79,
        (byte) 77,
        (byte) 188,
        (byte) 117,
        (byte) 65,
        (byte) 22,
        (byte) 187,
        (byte) 130,
        (byte) 49,
        (byte) 153,
        (byte) 202,
        (byte) 189,
        (byte) 77,
        (byte) 106,
        (byte) 212,
        (byte) 220,
        (byte) 244,
        (byte) 136,
        (byte) 146,
        (byte) 180,
        (byte) 202,
        (byte) 37,
        (byte) 115,
        (byte) 200,
        (byte) 216,
        (byte) 219,
        (byte) 242,
        (byte) 236,
        (byte) 209,
        (byte) 121,
        (byte) 212,
        (byte) 45,
        (byte) 113,
        (byte) 128 /*0x80*/,
        (byte) 92,
        (byte) 250,
        (byte) 64 /*0x40*/,
        (byte) 233,
        (byte) 1,
        (byte) 39,
        (byte) 247,
        (byte) 247,
        (byte) 54,
        (byte) 87,
        (byte) 86,
        (byte) 124,
        (byte) 41,
        (byte) 209,
        (byte) 245,
        (byte) 154,
        (byte) 86,
        (byte) 107,
        (byte) 175,
        (byte) 25
      };
      key.Query(true, 339, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[55]
      {
        (byte) 63 /*0x3F*/,
        (byte) 85,
        (byte) 47,
        (byte) 94,
        (byte) 143,
        (byte) 238,
        (byte) 189,
        (byte) 122,
        (byte) 203,
        (byte) 66,
        (byte) 12,
        (byte) 123,
        (byte) 235,
        (byte) 223,
        (byte) 136,
        (byte) 106,
        (byte) 28,
        (byte) 206,
        (byte) 63 /*0x3F*/,
        (byte) 169,
        (byte) 82,
        (byte) 183,
        (byte) 31 /*0x1F*/,
        (byte) 157,
        (byte) 88,
        (byte) 67,
        (byte) 114,
        (byte) 210,
        (byte) 250,
        (byte) 187,
        (byte) 180,
        (byte) 231,
        (byte) 89,
        (byte) 7,
        (byte) 181,
        (byte) 3,
        (byte) 48 /*0x30*/,
        (byte) 163,
        (byte) 81,
        (byte) 231,
        (byte) 58,
        (byte) 58,
        (byte) 103,
        (byte) 184,
        (byte) 11,
        (byte) 231,
        (byte) 28,
        (byte) 226,
        (byte) 7,
        (byte) 17,
        (byte) 185,
        (byte) 101,
        (byte) 193,
        (byte) 222,
        (byte) 136
      };
      byte[] numArray9 = new byte[55]
      {
        (byte) 171,
        (byte) 63 /*0x3F*/,
        (byte) 122,
        (byte) 9,
        (byte) 120,
        (byte) 59,
        (byte) 202,
        (byte) 162,
        (byte) 156,
        (byte) 105,
        (byte) 252,
        (byte) 80 /*0x50*/,
        (byte) 223,
        (byte) 137,
        (byte) 196,
        (byte) 76,
        (byte) 126,
        (byte) 115,
        (byte) 143,
        (byte) 53,
        (byte) 233,
        (byte) 134,
        (byte) 216,
        (byte) 59,
        (byte) 143,
        (byte) 93,
        (byte) 173,
        (byte) 73,
        (byte) 151,
        (byte) 229,
        (byte) 247,
        (byte) 18,
        (byte) 198,
        (byte) 14,
        (byte) 57,
        (byte) 90,
        (byte) 216,
        (byte) 213,
        (byte) 125,
        (byte) 124,
        (byte) 34,
        (byte) 140,
        (byte) 146,
        (byte) 76,
        (byte) 12,
        (byte) 55,
        (byte) 164,
        (byte) 196,
        (byte) 98,
        (byte) 194,
        (byte) 161,
        (byte) 52,
        (byte) 31 /*0x1F*/,
        (byte) 227,
        (byte) 226
      };
      key.Query(true, 339, numArray8, numArray8);
      Array.Copy((Array) numArray8, 0, (Array) numArray1, 165, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 165] ^= numArray9[index];
      byte[] numArray10 = new byte[16 /*0x10*/];
      numArray10[2] = (byte) 76;
      numArray10[1] = (byte) 5;
      numArray10[8] = (byte) 36;
      numArray10[3] = (byte) 124;
      numArray10[4] = (byte) 234;
      numArray10[9] = (byte) 33;
      numArray10[0] = (byte) 145;
      numArray10[15] = (byte) 125;
      numArray10[11] = (byte) 41;
      numArray10[13] = (byte) 57;
      numArray10[5] = (byte) 127 /*0x7F*/;
      numArray10[6] = (byte) 15;
      numArray10[7] = (byte) 142;
      numArray10[12] = (byte) 132;
      numArray10[14] = (byte) 120;
      numArray10[10] = (byte) 227;
      byte[] numArray11 = new byte[16 /*0x10*/]
      {
        (byte) 253,
        (byte) 145,
        (byte) 99,
        (byte) 155,
        (byte) 64 /*0x40*/,
        (byte) 133,
        (byte) 126,
        (byte) 128 /*0x80*/,
        (byte) 108,
        (byte) 130,
        (byte) 33,
        (byte) 151,
        (byte) 208 /*0xD0*/,
        (byte) 114,
        (byte) 170,
        (byte) 150
      };
      key.Query(true, 339, numArray10, numArray10);
      Array.Copy((Array) numArray10, 0, (Array) numArray1, 220, 16 /*0x10*/);
      for (int index = 0; index < 16 /*0x10*/; ++index)
        numArray1[index + 220] ^= numArray11[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray12 = new byte[236];
    byte[] numArray13 = new byte[55]
    {
      (byte) 157,
      (byte) 5,
      (byte) 106,
      (byte) 79,
      (byte) 186,
      (byte) 87,
      (byte) 42,
      (byte) 231,
      (byte) 197,
      (byte) 56,
      (byte) 185,
      (byte) 130,
      (byte) 215,
      (byte) 64 /*0x40*/,
      (byte) 232,
      (byte) 104,
      (byte) 111,
      (byte) 113,
      (byte) 55,
      (byte) 207,
      (byte) 254,
      (byte) 148,
      (byte) 37,
      (byte) 236,
      (byte) 31 /*0x1F*/,
      (byte) 151,
      (byte) 14,
      (byte) 63 /*0x3F*/,
      (byte) 254,
      (byte) 167,
      (byte) 101,
      (byte) 251,
      (byte) 183,
      (byte) 154,
      (byte) 219,
      (byte) 119,
      (byte) 237,
      (byte) 31 /*0x1F*/,
      (byte) 93,
      (byte) 248,
      (byte) 60,
      (byte) 171,
      (byte) 171,
      (byte) 238,
      (byte) 214,
      (byte) 72,
      (byte) 16 /*0x10*/,
      (byte) 193,
      (byte) 102,
      (byte) 189,
      (byte) 11,
      (byte) 13,
      (byte) 246,
      (byte) 254,
      (byte) 160 /*0xA0*/
    };
    byte[] numArray14 = new byte[55]
    {
      (byte) 117,
      (byte) 36,
      (byte) 155,
      (byte) 113,
      (byte) 205,
      (byte) 168,
      (byte) 18,
      (byte) 117,
      (byte) 239,
      (byte) 67,
      (byte) 72,
      (byte) 175,
      (byte) 117,
      (byte) 74,
      (byte) 69,
      (byte) 70,
      (byte) 83,
      (byte) 249,
      (byte) 84,
      (byte) 146,
      (byte) 175,
      (byte) 142,
      (byte) 139,
      (byte) 208 /*0xD0*/,
      (byte) 161,
      (byte) 42,
      (byte) 232,
      (byte) 32 /*0x20*/,
      (byte) 101,
      (byte) 137,
      (byte) 88,
      (byte) 135,
      (byte) 167,
      (byte) 150,
      (byte) 192 /*0xC0*/,
      (byte) 99,
      (byte) 147,
      (byte) 141,
      (byte) 231,
      (byte) 115,
      (byte) 42,
      (byte) 103,
      (byte) 175,
      (byte) 62,
      (byte) 26,
      (byte) 249,
      (byte) 166,
      (byte) 192 /*0xC0*/,
      (byte) 10,
      byte.MaxValue,
      (byte) 152,
      (byte) 152,
      (byte) 114,
      (byte) 68,
      (byte) 112 /*0x70*/
    };
    key.Query(true, 339, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray12, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index] ^= numArray14[index];
    byte[] numArray15 = new byte[55]
    {
      (byte) 126,
      (byte) 47,
      (byte) 35,
      (byte) 73,
      (byte) 234,
      (byte) 232,
      (byte) 30,
      (byte) 211,
      (byte) 189,
      (byte) 203,
      (byte) 61,
      (byte) 100,
      (byte) 113,
      (byte) 122,
      (byte) 79,
      (byte) 19,
      (byte) 62,
      (byte) 146,
      (byte) 83,
      (byte) 233,
      (byte) 54,
      (byte) 21,
      (byte) 215,
      (byte) 134,
      (byte) 76,
      (byte) 217,
      (byte) 64 /*0x40*/,
      (byte) 155,
      (byte) 87,
      (byte) 4,
      (byte) 185,
      (byte) 249,
      (byte) 211,
      (byte) 150,
      (byte) 111,
      (byte) 240 /*0xF0*/,
      (byte) 57,
      (byte) 174,
      (byte) 81,
      (byte) 194,
      (byte) 140,
      (byte) 26,
      (byte) 25,
      (byte) 124,
      (byte) 211,
      (byte) 92,
      (byte) 245,
      (byte) 67,
      (byte) 188,
      (byte) 249,
      (byte) 184,
      (byte) 107,
      (byte) 77,
      (byte) 30,
      (byte) 169
    };
    byte[] numArray16 = new byte[55];
    numArray16[17] = (byte) 26;
    numArray16[1] = (byte) 151;
    numArray16[0] = (byte) 134;
    numArray16[53] = (byte) 170;
    numArray16[4] = (byte) 111;
    numArray16[11] = (byte) 157;
    numArray16[21] = (byte) 43;
    numArray16[16 /*0x10*/] = (byte) 46;
    numArray16[24] = (byte) 61;
    numArray16[9] = (byte) 184;
    numArray16[44] = (byte) 75;
    numArray16[47] = (byte) 72;
    numArray16[52] = (byte) 215;
    numArray16[6] = (byte) 251;
    numArray16[14] = (byte) 134;
    numArray16[15] = (byte) 224 /*0xE0*/;
    numArray16[28] = (byte) 122;
    numArray16[36] = (byte) 11;
    numArray16[38] = (byte) 179;
    numArray16[19] = (byte) 252;
    numArray16[3] = (byte) 14;
    numArray16[54] = (byte) 122;
    numArray16[22] = (byte) 118;
    numArray16[13] = (byte) 225;
    numArray16[2] = (byte) 29;
    numArray16[25] = (byte) 182;
    numArray16[50] = (byte) 252;
    numArray16[5] = (byte) 41;
    numArray16[26] = (byte) 46;
    numArray16[27] = (byte) 176 /*0xB0*/;
    numArray16[30] = (byte) 176 /*0xB0*/;
    numArray16[31 /*0x1F*/] = (byte) 76;
    numArray16[7] = (byte) 145;
    numArray16[33] = (byte) 229;
    numArray16[34] = (byte) 75;
    numArray16[35] = (byte) 17;
    numArray16[41] = (byte) 99;
    numArray16[37] = (byte) 92;
    numArray16[20] = (byte) 42;
    numArray16[29] = (byte) 29;
    numArray16[40] = (byte) 118;
    numArray16[45] = (byte) 67;
    numArray16[42] = (byte) 47;
    numArray16[48 /*0x30*/] = (byte) 15;
    numArray16[43] = (byte) 111;
    numArray16[23] = (byte) 73;
    numArray16[46] = (byte) 126;
    numArray16[18] = (byte) 25;
    numArray16[39] = (byte) 220;
    numArray16[49] = (byte) 131;
    numArray16[51] = (byte) 73;
    numArray16[12] = (byte) 47;
    numArray16[8] = (byte) 205;
    numArray16[32 /*0x20*/] = (byte) 86;
    numArray16[10] = (byte) 24;
    key.Query(true, 339, numArray15, numArray15);
    Array.Copy((Array) numArray15, 0, (Array) numArray12, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index + 55] ^= numArray16[index];
    byte[] numArray17 = new byte[55]
    {
      (byte) 233,
      (byte) 13,
      (byte) 44,
      (byte) 171,
      (byte) 163,
      (byte) 29,
      (byte) 108,
      (byte) 197,
      (byte) 152,
      (byte) 220,
      (byte) 236,
      (byte) 4,
      (byte) 198,
      (byte) 61,
      (byte) 222,
      (byte) 0,
      (byte) 119,
      (byte) 83,
      (byte) 125,
      (byte) 156,
      (byte) 241,
      (byte) 36,
      (byte) 36,
      (byte) 201,
      (byte) 197,
      (byte) 130,
      (byte) 62,
      (byte) 36,
      (byte) 34,
      (byte) 239,
      (byte) 244,
      (byte) 95,
      (byte) 155,
      (byte) 156,
      (byte) 178,
      (byte) 164,
      (byte) 19,
      (byte) 110,
      (byte) 102,
      (byte) 129,
      (byte) 243,
      (byte) 164,
      (byte) 246,
      (byte) 238,
      (byte) 46,
      (byte) 51,
      (byte) 151,
      (byte) 226,
      (byte) 74,
      (byte) 42,
      (byte) 69,
      (byte) 109,
      (byte) 16 /*0x10*/,
      (byte) 207,
      (byte) 11
    };
    byte[] numArray18 = new byte[55]
    {
      (byte) 200,
      (byte) 241,
      (byte) 109,
      (byte) 187,
      (byte) 3,
      (byte) 6,
      (byte) 180,
      (byte) 229,
      (byte) 190,
      (byte) 192 /*0xC0*/,
      (byte) 118,
      (byte) 214,
      (byte) 25,
      (byte) 120,
      (byte) 28,
      (byte) 57,
      (byte) 140,
      (byte) 7,
      (byte) 92,
      (byte) 212,
      (byte) 194,
      (byte) 108,
      (byte) 72,
      (byte) 39,
      (byte) 20,
      (byte) 182,
      (byte) 137,
      (byte) 51,
      (byte) 29,
      (byte) 177,
      (byte) 94,
      (byte) 180,
      (byte) 21,
      (byte) 72,
      (byte) 6,
      (byte) 239,
      (byte) 219,
      (byte) 250,
      (byte) 150,
      (byte) 241,
      (byte) 76,
      (byte) 160 /*0xA0*/,
      (byte) 108,
      (byte) 38,
      (byte) 249,
      (byte) 61,
      (byte) 112 /*0x70*/,
      (byte) 247,
      (byte) 18,
      byte.MaxValue,
      (byte) 20,
      (byte) 140,
      (byte) 44,
      (byte) 48 /*0x30*/,
      (byte) 183
    };
    key.Query(true, 339, numArray17, numArray17);
    Array.Copy((Array) numArray17, 0, (Array) numArray12, 110, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index + 110] ^= numArray18[index];
    byte[] numArray19 = new byte[55]
    {
      (byte) 99,
      (byte) 230,
      (byte) 33,
      (byte) 222,
      (byte) 72,
      (byte) 163,
      (byte) 168,
      (byte) 232,
      (byte) 141,
      (byte) 105,
      (byte) 41,
      (byte) 138,
      (byte) 57,
      (byte) 236,
      (byte) 8,
      (byte) 237,
      (byte) 252,
      (byte) 168,
      (byte) 76,
      (byte) 45,
      (byte) 53,
      (byte) 155,
      (byte) 145,
      (byte) 234,
      (byte) 92,
      (byte) 217,
      (byte) 247,
      (byte) 169,
      (byte) 134,
      (byte) 79,
      (byte) 204,
      (byte) 145,
      (byte) 237,
      (byte) 188,
      (byte) 175,
      (byte) 135,
      (byte) 69,
      (byte) 162,
      (byte) 171,
      (byte) 1,
      (byte) 77,
      (byte) 6,
      (byte) 160 /*0xA0*/,
      (byte) 202,
      (byte) 109,
      (byte) 87,
      (byte) 16 /*0x10*/,
      (byte) 62,
      (byte) 60,
      (byte) 127 /*0x7F*/,
      (byte) 19,
      (byte) 38,
      (byte) 190,
      (byte) 152,
      (byte) 178
    };
    byte[] numArray20 = new byte[55];
    numArray20[51] = (byte) 50;
    numArray20[1] = (byte) 115;
    numArray20[45] = (byte) 129;
    numArray20[23] = (byte) 4;
    numArray20[4] = (byte) 28;
    numArray20[5] = (byte) 3;
    numArray20[54] = (byte) 1;
    numArray20[8] = (byte) 76;
    numArray20[32 /*0x20*/] = (byte) 150;
    numArray20[21] = (byte) 117;
    numArray20[10] = (byte) 210;
    numArray20[3] = (byte) 122;
    numArray20[0] = (byte) 58;
    numArray20[13] = (byte) 154;
    numArray20[14] = (byte) 27;
    numArray20[15] = (byte) 86;
    numArray20[30] = (byte) 195;
    numArray20[2] = (byte) 10;
    numArray20[27] = (byte) 133;
    numArray20[19] = (byte) 127 /*0x7F*/;
    numArray20[22] = (byte) 134;
    numArray20[52] = (byte) 130;
    numArray20[12] = (byte) 4;
    numArray20[46] = (byte) 85;
    numArray20[24] = (byte) 86;
    numArray20[49] = (byte) 86;
    numArray20[7] = (byte) 160 /*0xA0*/;
    numArray20[38] = (byte) 78;
    numArray20[28] = (byte) 39;
    numArray20[44] = (byte) 129;
    numArray20[20] = (byte) 104;
    numArray20[31 /*0x1F*/] = (byte) 148;
    numArray20[11] = (byte) 60;
    numArray20[33] = (byte) 42;
    numArray20[34] = (byte) 145;
    numArray20[35] = (byte) 199;
    numArray20[36] = (byte) 50;
    numArray20[25] = (byte) 100;
    numArray20[9] = (byte) 65;
    numArray20[39] = (byte) 81;
    numArray20[40] = (byte) 87;
    numArray20[41] = (byte) 227;
    numArray20[42] = (byte) 73;
    numArray20[43] = (byte) 121;
    numArray20[6] = (byte) 51;
    numArray20[29] = (byte) 26;
    numArray20[48 /*0x30*/] = (byte) 244;
    numArray20[37] = (byte) 8;
    numArray20[53] = (byte) 110;
    numArray20[47] = (byte) 162;
    numArray20[17] = (byte) 3;
    numArray20[16 /*0x10*/] = (byte) 137;
    numArray20[50] = (byte) 56;
    numArray20[18] = (byte) 92;
    numArray20[26] = (byte) 132;
    key.Query(true, 339, numArray19, numArray19);
    Array.Copy((Array) numArray19, 0, (Array) numArray12, 165, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index + 165] ^= numArray20[index];
    byte[] numArray21 = new byte[16 /*0x10*/]
    {
      (byte) 185,
      (byte) 125,
      (byte) 165,
      (byte) 242,
      (byte) 145,
      (byte) 90,
      (byte) 1,
      (byte) 247,
      (byte) 197,
      (byte) 71,
      (byte) 114,
      (byte) 85,
      (byte) 78,
      (byte) 154,
      (byte) 58,
      (byte) 192 /*0xC0*/
    };
    byte[] numArray22 = new byte[16 /*0x10*/];
    numArray22[3] = (byte) 24;
    numArray22[8] = (byte) 254;
    numArray22[2] = (byte) 16 /*0x10*/;
    numArray22[10] = (byte) 239;
    numArray22[4] = (byte) 198;
    numArray22[13] = (byte) 158;
    numArray22[1] = (byte) 164;
    numArray22[7] = (byte) 222;
    numArray22[5] = (byte) 106;
    numArray22[9] = (byte) 142;
    numArray22[6] = (byte) 216;
    numArray22[11] = (byte) 4;
    numArray22[12] = (byte) 53;
    numArray22[0] = (byte) 146;
    numArray22[15] = (byte) 153;
    numArray22[14] = (byte) 166;
    key.Query(true, 339, numArray21, numArray21);
    Array.Copy((Array) numArray21, 0, (Array) numArray12, 220, 16 /*0x10*/);
    for (int index = 0; index < 16 /*0x10*/; ++index)
      numArray12[index + 220] ^= numArray22[index];
    return Encoding.UTF8.GetString(numArray12);
  }

  internal static string ssp_avs_888()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[111];
      byte[] numArray2 = new byte[55];
      numArray2[14] = (byte) 127 /*0x7F*/;
      numArray2[47] = (byte) 20;
      numArray2[51] = (byte) 228;
      numArray2[3] = (byte) 74;
      numArray2[4] = (byte) 97;
      numArray2[5] = (byte) 53;
      numArray2[39] = (byte) 100;
      numArray2[7] = (byte) 115;
      numArray2[1] = (byte) 99;
      numArray2[8] = (byte) 33;
      numArray2[10] = (byte) 109;
      numArray2[35] = (byte) 145;
      numArray2[45] = (byte) 241;
      numArray2[13] = (byte) 104;
      numArray2[44] = (byte) 68;
      numArray2[40] = (byte) 151;
      numArray2[6] = (byte) 15;
      numArray2[42] = (byte) 19;
      numArray2[31 /*0x1F*/] = (byte) 168;
      numArray2[24] = (byte) 43;
      numArray2[12] = (byte) 126;
      numArray2[21] = (byte) 112 /*0x70*/;
      numArray2[53] = (byte) 65;
      numArray2[23] = (byte) 223;
      numArray2[52] = (byte) 178;
      numArray2[25] = (byte) 127 /*0x7F*/;
      numArray2[17] = (byte) 53;
      numArray2[0] = (byte) 151;
      numArray2[11] = (byte) 61;
      numArray2[28] = (byte) 170;
      numArray2[18] = (byte) 243;
      numArray2[15] = (byte) 39;
      numArray2[32 /*0x20*/] = (byte) 69;
      numArray2[33] = (byte) 153;
      numArray2[34] = (byte) 40;
      numArray2[29] = (byte) 235;
      numArray2[36] = (byte) 211;
      numArray2[37] = (byte) 63 /*0x3F*/;
      numArray2[38] = (byte) 54;
      numArray2[46] = (byte) 4;
      numArray2[22] = (byte) 58;
      numArray2[41] = (byte) 214;
      numArray2[26] = (byte) 122;
      numArray2[43] = (byte) 12;
      numArray2[20] = (byte) 52;
      numArray2[30] = (byte) 58;
      numArray2[27] = (byte) 103;
      numArray2[9] = (byte) 172;
      numArray2[48 /*0x30*/] = (byte) 222;
      numArray2[49] = (byte) 117;
      numArray2[50] = (byte) 141;
      numArray2[16 /*0x10*/] = (byte) 249;
      numArray2[2] = (byte) 189;
      numArray2[19] = (byte) 18;
      numArray2[54] = (byte) 109;
      byte[] numArray3 = new byte[55];
      numArray3[24] = (byte) 203;
      numArray3[1] = (byte) 153;
      numArray3[20] = (byte) 117;
      numArray3[18] = (byte) 51;
      numArray3[4] = (byte) 15;
      numArray3[5] = (byte) 176 /*0xB0*/;
      numArray3[0] = (byte) 71;
      numArray3[7] = (byte) 201;
      numArray3[31 /*0x1F*/] = (byte) 184;
      numArray3[9] = (byte) 163;
      numArray3[26] = (byte) 69;
      numArray3[11] = (byte) 154;
      numArray3[12] = (byte) 198;
      numArray3[41] = (byte) 63 /*0x3F*/;
      numArray3[25] = (byte) 136;
      numArray3[53] = (byte) 207;
      numArray3[16 /*0x10*/] = (byte) 170;
      numArray3[17] = (byte) 202;
      numArray3[6] = (byte) 88;
      numArray3[49] = (byte) 19;
      numArray3[29] = (byte) 192 /*0xC0*/;
      numArray3[21] = (byte) 252;
      numArray3[2] = (byte) 178;
      numArray3[48 /*0x30*/] = (byte) 78;
      numArray3[54] = (byte) 247;
      numArray3[14] = (byte) 174;
      numArray3[19] = (byte) 211;
      numArray3[27] = (byte) 225;
      numArray3[28] = (byte) 123;
      numArray3[3] = (byte) 31 /*0x1F*/;
      numArray3[30] = (byte) 215;
      numArray3[13] = (byte) 130;
      numArray3[32 /*0x20*/] = (byte) 3;
      numArray3[33] = (byte) 103;
      numArray3[34] = (byte) 158;
      numArray3[23] = (byte) 20;
      numArray3[36] = (byte) 90;
      numArray3[43] = (byte) 52;
      numArray3[38] = (byte) 45;
      numArray3[39] = (byte) 66;
      numArray3[15] = (byte) 104;
      numArray3[8] = (byte) 39;
      numArray3[42] = (byte) 64 /*0x40*/;
      numArray3[46] = (byte) 162;
      numArray3[44] = (byte) 80 /*0x50*/;
      numArray3[45] = (byte) 198;
      numArray3[35] = (byte) 18;
      numArray3[40] = (byte) 101;
      numArray3[47] = (byte) 253;
      numArray3[22] = (byte) 247;
      numArray3[50] = (byte) 219;
      numArray3[51] = (byte) 102;
      numArray3[10] = (byte) 63 /*0x3F*/;
      numArray3[37] = (byte) 119;
      numArray3[52] = (byte) 144 /*0x90*/;
      key.Query(true, 339, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 38,
        (byte) 167,
        (byte) 11,
        (byte) 195,
        (byte) 132,
        (byte) 127 /*0x7F*/,
        (byte) 75,
        (byte) 170,
        (byte) 253,
        (byte) 183,
        (byte) 156,
        (byte) 10,
        (byte) 22,
        (byte) 169,
        (byte) 98,
        (byte) 191,
        (byte) 146,
        (byte) 184,
        (byte) 214,
        (byte) 117,
        (byte) 20,
        (byte) 107,
        (byte) 57,
        (byte) 246,
        (byte) 253,
        (byte) 47,
        (byte) 80 /*0x50*/,
        (byte) 83,
        (byte) 55,
        (byte) 147,
        (byte) 0,
        (byte) 94,
        (byte) 231,
        (byte) 8,
        (byte) 141,
        (byte) 145,
        (byte) 225,
        (byte) 143,
        (byte) 78,
        (byte) 251,
        (byte) 188,
        (byte) 212,
        (byte) 179,
        (byte) 77,
        (byte) 200,
        (byte) 138,
        (byte) 83,
        (byte) 25,
        (byte) 213,
        (byte) 122,
        (byte) 171,
        (byte) 131,
        (byte) 231,
        (byte) 215,
        (byte) 81
      };
      byte[] numArray5 = new byte[55]
      {
        (byte) 54,
        (byte) 29,
        (byte) 128 /*0x80*/,
        (byte) 8,
        (byte) 117,
        (byte) 60,
        (byte) 13,
        (byte) 218,
        (byte) 42,
        (byte) 26,
        (byte) 28,
        (byte) 178,
        (byte) 25,
        (byte) 138,
        (byte) 128 /*0x80*/,
        (byte) 158,
        (byte) 103,
        (byte) 110,
        (byte) 26,
        (byte) 18,
        (byte) 181,
        (byte) 187,
        (byte) 232,
        (byte) 227,
        (byte) 56,
        (byte) 106,
        (byte) 82,
        (byte) 42,
        (byte) 5,
        (byte) 47,
        byte.MaxValue,
        (byte) 222,
        (byte) 66,
        (byte) 213,
        (byte) 171,
        (byte) 73,
        (byte) 115,
        (byte) 75,
        (byte) 171,
        (byte) 20,
        (byte) 206,
        (byte) 130,
        (byte) 4,
        (byte) 134,
        (byte) 145,
        (byte) 203,
        (byte) 40,
        (byte) 191,
        (byte) 136,
        (byte) 41,
        (byte) 188,
        (byte) 118,
        (byte) 67,
        (byte) 26,
        (byte) 8
      };
      key.Query(true, 339, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[1]{ (byte) 20 };
      byte[] numArray7 = new byte[1]{ (byte) 16 /*0x10*/ };
      key.Query(true, 339, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 1);
      for (int index = 0; index < 1; ++index)
        numArray1[index + 110] ^= numArray7[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray8 = new byte[111];
    byte[] numArray9 = new byte[55];
    numArray9[48 /*0x30*/] = (byte) 129;
    numArray9[46] = (byte) 17;
    numArray9[2] = (byte) 61;
    numArray9[12] = (byte) 63 /*0x3F*/;
    numArray9[40] = (byte) 5;
    numArray9[9] = (byte) 106;
    numArray9[8] = (byte) 3;
    numArray9[1] = (byte) 183;
    numArray9[7] = (byte) 221;
    numArray9[21] = (byte) 232;
    numArray9[10] = (byte) 219;
    numArray9[6] = (byte) 208 /*0xD0*/;
    numArray9[26] = (byte) 211;
    numArray9[13] = (byte) 184;
    numArray9[14] = (byte) 235;
    numArray9[15] = (byte) 46;
    numArray9[42] = (byte) 167;
    numArray9[17] = (byte) 219;
    numArray9[45] = (byte) 30;
    numArray9[19] = (byte) 125;
    numArray9[20] = (byte) 188;
    numArray9[44] = (byte) 12;
    numArray9[22] = (byte) 124;
    numArray9[34] = (byte) 73;
    numArray9[24] = (byte) 16 /*0x10*/;
    numArray9[25] = (byte) 183;
    numArray9[30] = (byte) 249;
    numArray9[35] = (byte) 150;
    numArray9[28] = (byte) 204;
    numArray9[29] = (byte) 200;
    numArray9[0] = (byte) 105;
    numArray9[18] = (byte) 55;
    numArray9[32 /*0x20*/] = (byte) 196;
    numArray9[51] = (byte) 120;
    numArray9[5] = (byte) 28;
    numArray9[31 /*0x1F*/] = (byte) 204;
    numArray9[47] = (byte) 1;
    numArray9[37] = (byte) 104;
    numArray9[38] = (byte) 70;
    numArray9[39] = (byte) 253;
    numArray9[27] = (byte) 36;
    numArray9[11] = (byte) 185;
    numArray9[41] = (byte) 156;
    numArray9[43] = (byte) 24;
    numArray9[4] = (byte) 13;
    numArray9[16 /*0x10*/] = (byte) 114;
    numArray9[33] = (byte) 97;
    numArray9[36] = (byte) 139;
    numArray9[23] = (byte) 118;
    numArray9[49] = (byte) 230;
    numArray9[50] = (byte) 83;
    numArray9[53] = (byte) 122;
    numArray9[52] = (byte) 247;
    numArray9[3] = (byte) 34;
    numArray9[54] = (byte) 182;
    byte[] numArray10 = new byte[55]
    {
      (byte) 134,
      (byte) 220,
      (byte) 11,
      (byte) 129,
      (byte) 184,
      (byte) 94,
      (byte) 150,
      (byte) 174,
      (byte) 171,
      (byte) 238,
      (byte) 168,
      (byte) 195,
      (byte) 133,
      (byte) 108,
      (byte) 235,
      (byte) 239,
      (byte) 115,
      (byte) 171,
      (byte) 161,
      (byte) 64 /*0x40*/,
      (byte) 149,
      (byte) 220,
      (byte) 213,
      (byte) 83,
      (byte) 13,
      (byte) 44,
      (byte) 206,
      (byte) 157,
      (byte) 159,
      (byte) 189,
      (byte) 181,
      (byte) 88,
      (byte) 26,
      (byte) 96 /*0x60*/,
      (byte) 91,
      (byte) 25,
      (byte) 200,
      (byte) 31 /*0x1F*/,
      (byte) 36,
      (byte) 195,
      (byte) 2,
      (byte) 195,
      (byte) 70,
      (byte) 237,
      (byte) 243,
      (byte) 146,
      (byte) 54,
      (byte) 72,
      (byte) 154,
      (byte) 104,
      (byte) 201,
      (byte) 126,
      (byte) 250,
      (byte) 130,
      (byte) 187
    };
    key.Query(true, 339, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray8, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index] ^= numArray10[index];
    byte[] numArray11 = new byte[55]
    {
      (byte) 92,
      (byte) 32 /*0x20*/,
      (byte) 176 /*0xB0*/,
      (byte) 119,
      (byte) 8,
      (byte) 42,
      (byte) 198,
      (byte) 240 /*0xF0*/,
      (byte) 58,
      (byte) 171,
      (byte) 51,
      (byte) 165,
      (byte) 248,
      (byte) 222,
      (byte) 180,
      (byte) 164,
      (byte) 133,
      (byte) 29,
      (byte) 148,
      (byte) 211,
      (byte) 188,
      (byte) 109,
      (byte) 199,
      (byte) 113,
      (byte) 62,
      (byte) 119,
      (byte) 163,
      (byte) 43,
      (byte) 157,
      (byte) 23,
      (byte) 70,
      (byte) 20,
      (byte) 137,
      (byte) 134,
      (byte) 103,
      (byte) 53,
      (byte) 64 /*0x40*/,
      (byte) 60,
      (byte) 81,
      (byte) 97,
      (byte) 167,
      (byte) 183,
      (byte) 195,
      (byte) 126,
      (byte) 0,
      (byte) 237,
      (byte) 51,
      (byte) 100,
      (byte) 148,
      (byte) 63 /*0x3F*/,
      (byte) 56,
      (byte) 229,
      (byte) 2,
      (byte) 108,
      (byte) 26
    };
    byte[] numArray12 = new byte[55];
    numArray12[13] = (byte) 150;
    numArray12[28] = (byte) 212;
    numArray12[51] = (byte) 55;
    numArray12[3] = (byte) 76;
    numArray12[39] = (byte) 143;
    numArray12[10] = (byte) 242;
    numArray12[9] = (byte) 87;
    numArray12[2] = (byte) 88;
    numArray12[30] = (byte) 132;
    numArray12[14] = (byte) 63 /*0x3F*/;
    numArray12[33] = (byte) 127 /*0x7F*/;
    numArray12[11] = (byte) 221;
    numArray12[12] = (byte) 52;
    numArray12[32 /*0x20*/] = (byte) 89;
    numArray12[52] = (byte) 26;
    numArray12[15] = (byte) 107;
    numArray12[36] = (byte) 96 /*0x60*/;
    numArray12[47] = (byte) 215;
    numArray12[18] = (byte) 5;
    numArray12[19] = (byte) 19;
    numArray12[20] = (byte) 48 /*0x30*/;
    numArray12[21] = (byte) 1;
    numArray12[22] = (byte) 245;
    numArray12[6] = (byte) 191;
    numArray12[24] = (byte) 161;
    numArray12[38] = (byte) 251;
    numArray12[17] = (byte) 171;
    numArray12[27] = (byte) 237;
    numArray12[44] = (byte) 75;
    numArray12[29] = (byte) 221;
    numArray12[25] = (byte) 78;
    numArray12[31 /*0x1F*/] = (byte) 91;
    numArray12[54] = (byte) 170;
    numArray12[53] = (byte) 37;
    numArray12[34] = (byte) 18;
    numArray12[23] = (byte) 148;
    numArray12[41] = (byte) 136;
    numArray12[37] = (byte) 73;
    numArray12[42] = (byte) 163;
    numArray12[35] = (byte) 85;
    numArray12[26] = (byte) 151;
    numArray12[46] = (byte) 115;
    numArray12[1] = (byte) 185;
    numArray12[43] = (byte) 183;
    numArray12[50] = (byte) 196;
    numArray12[7] = (byte) 182;
    numArray12[16 /*0x10*/] = (byte) 215;
    numArray12[45] = (byte) 57;
    numArray12[48 /*0x30*/] = (byte) 181;
    numArray12[49] = (byte) 53;
    numArray12[4] = (byte) 79;
    numArray12[0] = (byte) 106;
    numArray12[8] = (byte) 216;
    numArray12[5] = (byte) 170;
    numArray12[40] = (byte) 193;
    key.Query(true, 339, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray8, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index + 55] ^= numArray12[index];
    byte[] numArray13 = new byte[1]{ (byte) 206 };
    byte[] numArray14 = new byte[1]{ (byte) 225 };
    key.Query(true, 339, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray8, 110, 1);
    for (int index = 0; index < 1; ++index)
      numArray8[index + 110] ^= numArray14[index];
    return Encoding.UTF8.GetString(numArray8);
  }

  internal static string ssp_avs_889()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[165];
      byte[] numArray2 = new byte[55];
      numArray2[18] = (byte) 129;
      numArray2[7] = (byte) 69;
      numArray2[50] = (byte) 202;
      numArray2[37] = (byte) 186;
      numArray2[29] = (byte) 114;
      numArray2[49] = (byte) 104;
      numArray2[6] = (byte) 99;
      numArray2[33] = (byte) 91;
      numArray2[8] = (byte) 11;
      numArray2[16 /*0x10*/] = (byte) 131;
      numArray2[10] = (byte) 31 /*0x1F*/;
      numArray2[11] = (byte) 159;
      numArray2[12] = (byte) 10;
      numArray2[22] = (byte) 65;
      numArray2[35] = (byte) 212;
      numArray2[34] = (byte) 21;
      numArray2[46] = (byte) 59;
      numArray2[27] = (byte) 51;
      numArray2[40] = (byte) 120;
      numArray2[19] = (byte) 130;
      numArray2[20] = (byte) 238;
      numArray2[21] = (byte) 71;
      numArray2[28] = (byte) 26;
      numArray2[23] = (byte) 220;
      numArray2[24] = (byte) 86;
      numArray2[5] = (byte) 162;
      numArray2[26] = (byte) 204;
      numArray2[51] = (byte) 245;
      numArray2[42] = (byte) 13;
      numArray2[4] = (byte) 254;
      numArray2[30] = (byte) 146;
      numArray2[25] = (byte) 51;
      numArray2[47] = (byte) 225;
      numArray2[54] = (byte) 85;
      numArray2[41] = (byte) 170;
      numArray2[15] = (byte) 128 /*0x80*/;
      numArray2[36] = (byte) 208 /*0xD0*/;
      numArray2[17] = (byte) 82;
      numArray2[38] = (byte) 238;
      numArray2[32 /*0x20*/] = (byte) 111;
      numArray2[31 /*0x1F*/] = (byte) 8;
      numArray2[14] = (byte) 143;
      numArray2[9] = (byte) 89;
      numArray2[43] = (byte) 118;
      numArray2[44] = (byte) 32 /*0x20*/;
      numArray2[45] = (byte) 117;
      numArray2[13] = (byte) 46;
      numArray2[1] = (byte) 13;
      numArray2[0] = (byte) 4;
      numArray2[2] = (byte) 0;
      numArray2[3] = (byte) 6;
      numArray2[48 /*0x30*/] = (byte) 63 /*0x3F*/;
      numArray2[52] = (byte) 130;
      numArray2[53] = (byte) 114;
      numArray2[39] = (byte) 115;
      byte[] numArray3 = new byte[55]
      {
        (byte) 101,
        (byte) 252,
        (byte) 108,
        (byte) 26,
        (byte) 102,
        (byte) 125,
        (byte) 238,
        (byte) 146,
        (byte) 90,
        (byte) 199,
        (byte) 195,
        (byte) 207,
        (byte) 49,
        (byte) 138,
        (byte) 200,
        (byte) 226,
        (byte) 14,
        (byte) 173,
        (byte) 35,
        (byte) 149,
        (byte) 4,
        (byte) 4,
        (byte) 213,
        (byte) 233,
        (byte) 86,
        (byte) 44,
        (byte) 148,
        (byte) 193,
        (byte) 19,
        (byte) 194,
        (byte) 232,
        (byte) 91,
        (byte) 161,
        (byte) 49,
        (byte) 123,
        (byte) 206,
        (byte) 179,
        (byte) 124,
        (byte) 237,
        (byte) 40,
        (byte) 84,
        (byte) 234,
        (byte) 172,
        (byte) 233,
        (byte) 27,
        (byte) 191,
        (byte) 207,
        (byte) 100,
        (byte) 208 /*0xD0*/,
        (byte) 91,
        (byte) 124,
        (byte) 44,
        (byte) 126,
        (byte) 83,
        (byte) 107
      };
      key.Query(true, 339, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 103,
        (byte) 207,
        (byte) 210,
        (byte) 234,
        (byte) 60,
        (byte) 120,
        (byte) 240 /*0xF0*/,
        (byte) 42,
        (byte) 144 /*0x90*/,
        (byte) 51,
        (byte) 168,
        (byte) 191,
        (byte) 178,
        (byte) 3,
        (byte) 191,
        (byte) 64 /*0x40*/,
        (byte) 111,
        (byte) 194,
        (byte) 196,
        (byte) 197,
        (byte) 28,
        (byte) 179,
        (byte) 155,
        (byte) 107,
        (byte) 34,
        (byte) 111,
        (byte) 250,
        (byte) 167,
        (byte) 11,
        (byte) 17,
        (byte) 148,
        (byte) 244,
        (byte) 214,
        (byte) 8,
        (byte) 69,
        (byte) 58,
        (byte) 80 /*0x50*/,
        (byte) 179,
        (byte) 11,
        (byte) 6,
        (byte) 131,
        (byte) 133,
        (byte) 120,
        (byte) 17,
        (byte) 227,
        (byte) 8,
        (byte) 209,
        (byte) 180,
        (byte) 66,
        (byte) 81,
        (byte) 4,
        (byte) 165,
        (byte) 76,
        (byte) 189,
        (byte) 227
      };
      byte[] numArray5 = new byte[55]
      {
        (byte) 131,
        (byte) 47,
        (byte) 57,
        (byte) 19,
        (byte) 55,
        (byte) 11,
        (byte) 210,
        (byte) 132,
        (byte) 177,
        (byte) 126,
        (byte) 142,
        (byte) 222,
        (byte) 39,
        (byte) 254,
        (byte) 195,
        (byte) 3,
        (byte) 51,
        (byte) 124,
        (byte) 233,
        (byte) 63 /*0x3F*/,
        (byte) 109,
        (byte) 61,
        (byte) 18,
        (byte) 151,
        (byte) 114,
        (byte) 234,
        (byte) 43,
        (byte) 166,
        (byte) 162,
        (byte) 174,
        (byte) 186,
        (byte) 24,
        (byte) 195,
        (byte) 101,
        (byte) 116,
        (byte) 151,
        (byte) 199,
        (byte) 170,
        (byte) 91,
        (byte) 26,
        (byte) 5,
        (byte) 237,
        (byte) 177,
        (byte) 19,
        (byte) 17,
        (byte) 29,
        (byte) 199,
        (byte) 14,
        (byte) 175,
        (byte) 160 /*0xA0*/,
        (byte) 206,
        (byte) 212,
        (byte) 180,
        (byte) 136,
        (byte) 117
      };
      key.Query(true, 339, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[55];
      numArray6[34] = (byte) 233;
      numArray6[40] = (byte) 248;
      numArray6[2] = (byte) 204;
      numArray6[39] = (byte) 102;
      numArray6[4] = (byte) 73;
      numArray6[45] = (byte) 246;
      numArray6[37] = (byte) 136;
      numArray6[16 /*0x10*/] = (byte) 189;
      numArray6[23] = (byte) 157;
      numArray6[9] = (byte) 233;
      numArray6[15] = (byte) 183;
      numArray6[31 /*0x1F*/] = (byte) 36;
      numArray6[12] = (byte) 133;
      numArray6[5] = (byte) 31 /*0x1F*/;
      numArray6[14] = (byte) 188;
      numArray6[6] = (byte) 127 /*0x7F*/;
      numArray6[25] = (byte) 101;
      numArray6[3] = (byte) 126;
      numArray6[24] = (byte) 69;
      numArray6[0] = (byte) 102;
      numArray6[48 /*0x30*/] = (byte) 72;
      numArray6[7] = (byte) 158;
      numArray6[22] = (byte) 26;
      numArray6[54] = (byte) 169;
      numArray6[32 /*0x20*/] = (byte) 144 /*0x90*/;
      numArray6[1] = (byte) 199;
      numArray6[26] = (byte) 185;
      numArray6[53] = (byte) 121;
      numArray6[28] = (byte) 239;
      numArray6[29] = (byte) 167;
      numArray6[50] = (byte) 142;
      numArray6[38] = (byte) 58;
      numArray6[27] = (byte) 12;
      numArray6[33] = (byte) 6;
      numArray6[20] = (byte) 138;
      numArray6[35] = (byte) 235;
      numArray6[36] = (byte) 63 /*0x3F*/;
      numArray6[30] = (byte) 25;
      numArray6[13] = (byte) 239;
      numArray6[21] = (byte) 136;
      numArray6[17] = (byte) 65;
      numArray6[41] = (byte) 114;
      numArray6[42] = (byte) 46;
      numArray6[46] = (byte) 188;
      numArray6[10] = (byte) 170;
      numArray6[18] = (byte) 150;
      numArray6[19] = (byte) 58;
      numArray6[47] = (byte) 97;
      numArray6[8] = (byte) 180;
      numArray6[49] = (byte) 238;
      numArray6[11] = (byte) 190;
      numArray6[51] = (byte) 123;
      numArray6[52] = (byte) 77;
      numArray6[43] = (byte) 31 /*0x1F*/;
      numArray6[44] = (byte) 39;
      byte[] numArray7 = new byte[55]
      {
        (byte) 8,
        (byte) 216,
        (byte) 9,
        (byte) 142,
        (byte) 105,
        (byte) 189,
        (byte) 180,
        (byte) 32 /*0x20*/,
        (byte) 160 /*0xA0*/,
        (byte) 183,
        (byte) 225,
        (byte) 217,
        (byte) 132,
        (byte) 16 /*0x10*/,
        (byte) 158,
        (byte) 44,
        (byte) 40,
        (byte) 149,
        (byte) 179,
        (byte) 5,
        (byte) 47,
        (byte) 132,
        (byte) 83,
        (byte) 70,
        (byte) 225,
        (byte) 167,
        (byte) 178,
        (byte) 211,
        (byte) 124,
        (byte) 115,
        (byte) 199,
        (byte) 179,
        (byte) 249,
        (byte) 225,
        (byte) 148,
        (byte) 188,
        (byte) 14,
        (byte) 117,
        (byte) 249,
        (byte) 10,
        (byte) 103,
        (byte) 90,
        (byte) 235,
        (byte) 149,
        (byte) 220,
        (byte) 163,
        (byte) 123,
        (byte) 149,
        (byte) 45,
        (byte) 185,
        (byte) 19,
        (byte) 106,
        (byte) 81,
        (byte) 128 /*0x80*/,
        (byte) 102
      };
      key.Query(true, 339, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[14];
      byte[] response = new byte[14];
      Array.Copy((Array) sc_886.sspq, 0, (Array) numArray8, 0, 14);
      key.Query(true, 339, numArray8, response);
      Array.Copy((Array) sc_886.sspr, 0, (Array) numArray8, 0, 14);
      for (int index = 0; index < numArray8.Length; ++index)
      {
        if ((int) numArray8[index] != (int) response[index])
        {
          key.TagValue = (int) response[index];
          break;
        }
      }
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray9 = new byte[165];
    byte[] numArray10 = new byte[55]
    {
      (byte) 47,
      (byte) 188,
      (byte) 84,
      (byte) 56,
      (byte) 172,
      (byte) 211,
      (byte) 134,
      (byte) 106,
      (byte) 254,
      (byte) 76,
      (byte) 139,
      (byte) 191,
      (byte) 41,
      (byte) 187,
      (byte) 224 /*0xE0*/,
      (byte) 201,
      (byte) 133,
      (byte) 140,
      (byte) 87,
      (byte) 102,
      (byte) 27,
      (byte) 244,
      (byte) 214,
      (byte) 21,
      (byte) 44,
      (byte) 28,
      (byte) 244,
      (byte) 179,
      (byte) 26,
      (byte) 95,
      (byte) 229,
      (byte) 14,
      (byte) 115,
      (byte) 195,
      (byte) 236,
      (byte) 122,
      (byte) 61,
      (byte) 76,
      (byte) 5,
      (byte) 229,
      (byte) 98,
      (byte) 208 /*0xD0*/,
      (byte) 14,
      (byte) 98,
      (byte) 34,
      (byte) 53,
      (byte) 8,
      (byte) 242,
      (byte) 53,
      (byte) 89,
      (byte) 117,
      (byte) 6,
      (byte) 16 /*0x10*/,
      (byte) 126,
      (byte) 149
    };
    byte[] numArray11 = new byte[55]
    {
      (byte) 73,
      (byte) 71,
      (byte) 173,
      (byte) 15,
      (byte) 32 /*0x20*/,
      (byte) 193,
      (byte) 157,
      (byte) 209,
      (byte) 121,
      (byte) 147,
      (byte) 213,
      (byte) 122,
      (byte) 248,
      (byte) 111,
      (byte) 43,
      (byte) 1,
      (byte) 20,
      (byte) 153,
      (byte) 198,
      (byte) 46,
      (byte) 110,
      (byte) 109,
      (byte) 131,
      (byte) 204,
      (byte) 230,
      (byte) 197,
      (byte) 245,
      (byte) 91,
      (byte) 3,
      (byte) 79,
      (byte) 216,
      (byte) 19,
      (byte) 126,
      (byte) 83,
      (byte) 187,
      (byte) 237,
      (byte) 45,
      (byte) 138,
      (byte) 65,
      (byte) 128 /*0x80*/,
      (byte) 96 /*0x60*/,
      (byte) 41,
      (byte) 151,
      (byte) 130,
      (byte) 72,
      (byte) 215,
      (byte) 40,
      (byte) 84,
      (byte) 198,
      (byte) 208 /*0xD0*/,
      (byte) 5,
      (byte) 112 /*0x70*/,
      (byte) 253,
      (byte) 33,
      (byte) 79
    };
    key.Query(true, 339, numArray10, numArray10);
    Array.Copy((Array) numArray10, 0, (Array) numArray9, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray9[index] ^= numArray11[index];
    byte[] numArray12 = new byte[55]
    {
      (byte) 63 /*0x3F*/,
      (byte) 11,
      (byte) 144 /*0x90*/,
      (byte) 223,
      (byte) 211,
      (byte) 80 /*0x50*/,
      (byte) 158,
      (byte) 103,
      (byte) 125,
      (byte) 75,
      (byte) 102,
      (byte) 206,
      (byte) 101,
      (byte) 140,
      (byte) 56,
      (byte) 134,
      (byte) 114,
      (byte) 4,
      (byte) 74,
      (byte) 230,
      (byte) 116,
      (byte) 242,
      (byte) 246,
      (byte) 19,
      (byte) 1,
      (byte) 125,
      (byte) 193,
      (byte) 233,
      (byte) 223,
      (byte) 253,
      (byte) 244,
      (byte) 46,
      (byte) 177,
      (byte) 149,
      (byte) 254,
      (byte) 64 /*0x40*/,
      (byte) 117,
      (byte) 5,
      (byte) 142,
      (byte) 128 /*0x80*/,
      (byte) 111,
      (byte) 251,
      (byte) 165,
      (byte) 234,
      (byte) 162,
      (byte) 35,
      (byte) 207,
      (byte) 205,
      (byte) 60,
      (byte) 127 /*0x7F*/,
      (byte) 158,
      (byte) 122,
      (byte) 74,
      (byte) 84,
      (byte) 113
    };
    byte[] numArray13 = new byte[55]
    {
      (byte) 49,
      (byte) 122,
      (byte) 140,
      (byte) 207,
      (byte) 19,
      (byte) 6,
      (byte) 8,
      (byte) 248,
      (byte) 33,
      (byte) 19,
      (byte) 34,
      (byte) 34,
      (byte) 155,
      (byte) 55,
      (byte) 172,
      (byte) 206,
      (byte) 117,
      (byte) 231,
      (byte) 222,
      (byte) 154,
      (byte) 141,
      (byte) 78,
      (byte) 131,
      (byte) 131,
      (byte) 199,
      (byte) 166,
      (byte) 80 /*0x50*/,
      (byte) 98,
      (byte) 56,
      (byte) 217,
      (byte) 106,
      (byte) 26,
      (byte) 171,
      (byte) 234,
      (byte) 214,
      (byte) 153,
      (byte) 248,
      (byte) 164,
      (byte) 43,
      (byte) 91,
      (byte) 90,
      (byte) 230,
      (byte) 82,
      (byte) 155,
      (byte) 222,
      (byte) 49,
      (byte) 82,
      (byte) 83,
      (byte) 253,
      (byte) 135,
      (byte) 179,
      (byte) 170,
      (byte) 197,
      (byte) 152,
      (byte) 90
    };
    key.Query(true, 339, numArray12, numArray12);
    Array.Copy((Array) numArray12, 0, (Array) numArray9, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray9[index + 55] ^= numArray13[index];
    byte[] numArray14 = new byte[55]
    {
      (byte) 124,
      (byte) 221,
      (byte) 85,
      (byte) 143,
      (byte) 181,
      (byte) 136,
      (byte) 127 /*0x7F*/,
      (byte) 86,
      (byte) 55,
      (byte) 29,
      (byte) 208 /*0xD0*/,
      (byte) 141,
      (byte) 248,
      (byte) 17,
      (byte) 108,
      (byte) 118,
      (byte) 202,
      (byte) 118,
      (byte) 120,
      (byte) 77,
      (byte) 12,
      (byte) 113,
      (byte) 221,
      (byte) 174,
      (byte) 243,
      (byte) 172,
      (byte) 95,
      (byte) 95,
      (byte) 122,
      (byte) 228,
      (byte) 92,
      (byte) 35,
      (byte) 177,
      (byte) 43,
      (byte) 248,
      (byte) 40,
      (byte) 56,
      (byte) 241,
      (byte) 186,
      (byte) 115,
      (byte) 38,
      (byte) 147,
      (byte) 107,
      (byte) 6,
      byte.MaxValue,
      (byte) 63 /*0x3F*/,
      (byte) 68,
      (byte) 110,
      (byte) 49,
      (byte) 4,
      (byte) 128 /*0x80*/,
      (byte) 222,
      (byte) 249,
      byte.MaxValue,
      (byte) 8
    };
    byte[] numArray15 = new byte[55];
    numArray15[49] = (byte) 25;
    numArray15[0] = (byte) 107;
    numArray15[19] = (byte) 83;
    numArray15[10] = (byte) 247;
    numArray15[43] = (byte) 11;
    numArray15[42] = (byte) 25;
    numArray15[6] = (byte) 59;
    numArray15[18] = (byte) 168;
    numArray15[21] = (byte) 135;
    numArray15[25] = (byte) 55;
    numArray15[1] = (byte) 244;
    numArray15[11] = (byte) 67;
    numArray15[12] = (byte) 227;
    numArray15[3] = (byte) 134;
    numArray15[4] = (byte) 223;
    numArray15[17] = (byte) 76;
    numArray15[15] = (byte) 47;
    numArray15[26] = (byte) 58;
    numArray15[44] = (byte) 175;
    numArray15[8] = (byte) 210;
    numArray15[32 /*0x20*/] = (byte) 181;
    numArray15[31 /*0x1F*/] = (byte) 60;
    numArray15[22] = (byte) 134;
    numArray15[23] = (byte) 198;
    numArray15[33] = (byte) 170;
    numArray15[38] = (byte) 236;
    numArray15[46] = (byte) 199;
    numArray15[27] = (byte) 189;
    numArray15[28] = (byte) 176 /*0xB0*/;
    numArray15[29] = (byte) 204;
    numArray15[30] = (byte) 17;
    numArray15[48 /*0x30*/] = (byte) 234;
    numArray15[14] = (byte) 13;
    numArray15[16 /*0x10*/] = (byte) 17;
    numArray15[51] = (byte) 169;
    numArray15[35] = (byte) 45;
    numArray15[36] = (byte) 241;
    numArray15[37] = (byte) 113;
    numArray15[45] = (byte) 224 /*0xE0*/;
    numArray15[39] = (byte) 172;
    numArray15[40] = (byte) 50;
    numArray15[41] = (byte) 132;
    numArray15[5] = (byte) 24;
    numArray15[47] = (byte) 97;
    numArray15[20] = (byte) 92;
    numArray15[2] = (byte) 76;
    numArray15[9] = (byte) 48 /*0x30*/;
    numArray15[7] = (byte) 187;
    numArray15[50] = (byte) 167;
    numArray15[24] = (byte) 18;
    numArray15[13] = (byte) 37;
    numArray15[34] = (byte) 59;
    numArray15[52] = (byte) 157;
    numArray15[53] = (byte) 81;
    numArray15[54] = (byte) 99;
    key.Query(true, 339, numArray14, numArray14);
    Array.Copy((Array) numArray14, 0, (Array) numArray9, 110, 55);
    for (int index = 0; index < 55; ++index)
      numArray9[index + 110] ^= numArray15[index];
    return Encoding.UTF8.GetString(numArray9);
  }

  internal static string ssp_avs_890()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[197];
      byte[] numArray2 = new byte[55]
      {
        (byte) 160 /*0xA0*/,
        (byte) 55,
        (byte) 241,
        (byte) 101,
        (byte) 238,
        (byte) 65,
        (byte) 129,
        (byte) 41,
        (byte) 169,
        (byte) 42,
        (byte) 27,
        (byte) 172,
        (byte) 130,
        (byte) 9,
        (byte) 185,
        (byte) 32 /*0x20*/,
        (byte) 193,
        (byte) 237,
        (byte) 144 /*0x90*/,
        (byte) 91,
        (byte) 80 /*0x50*/,
        (byte) 239,
        (byte) 155,
        (byte) 63 /*0x3F*/,
        (byte) 175,
        (byte) 188,
        (byte) 92,
        (byte) 54,
        (byte) 218,
        (byte) 147,
        (byte) 231,
        (byte) 0,
        (byte) 153,
        (byte) 110,
        (byte) 18,
        (byte) 19,
        (byte) 21,
        (byte) 81,
        (byte) 41,
        (byte) 4,
        (byte) 209,
        (byte) 58,
        (byte) 206,
        (byte) 43,
        (byte) 210,
        (byte) 26,
        (byte) 36,
        (byte) 24,
        (byte) 58,
        (byte) 184,
        (byte) 206,
        (byte) 24,
        (byte) 151,
        (byte) 156,
        (byte) 151
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 52,
        (byte) 218,
        (byte) 78,
        (byte) 210,
        (byte) 80 /*0x50*/,
        (byte) 214,
        (byte) 217,
        (byte) 77,
        (byte) 123,
        (byte) 216,
        (byte) 162,
        (byte) 131,
        (byte) 156,
        (byte) 61,
        (byte) 4,
        (byte) 237,
        (byte) 0,
        (byte) 149,
        (byte) 146,
        (byte) 241,
        (byte) 121,
        (byte) 137,
        (byte) 186,
        (byte) 228,
        (byte) 142,
        (byte) 88,
        (byte) 227,
        (byte) 29,
        (byte) 172,
        (byte) 103,
        (byte) 168,
        (byte) 239,
        (byte) 12,
        (byte) 232,
        (byte) 141,
        (byte) 17,
        byte.MaxValue,
        (byte) 158,
        (byte) 12,
        (byte) 252,
        (byte) 113,
        (byte) 70,
        (byte) 190,
        (byte) 116,
        (byte) 69,
        (byte) 3,
        (byte) 211,
        (byte) 28,
        (byte) 249,
        (byte) 18,
        (byte) 254,
        (byte) 108,
        (byte) 129,
        (byte) 174,
        (byte) 27
      };
      key.Query(true, 339, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 177,
        (byte) 182,
        (byte) 49,
        (byte) 229,
        (byte) 74,
        (byte) 78,
        (byte) 22,
        (byte) 67,
        (byte) 26,
        (byte) 18,
        (byte) 19,
        (byte) 247,
        (byte) 166,
        (byte) 10,
        (byte) 111,
        (byte) 195,
        (byte) 140,
        (byte) 138,
        (byte) 21,
        (byte) 33,
        (byte) 111,
        (byte) 108,
        (byte) 148,
        (byte) 152,
        (byte) 68,
        (byte) 252,
        (byte) 34,
        (byte) 196,
        (byte) 207,
        (byte) 104,
        (byte) 252,
        (byte) 215,
        (byte) 97,
        (byte) 118,
        (byte) 99,
        (byte) 154,
        (byte) 248,
        (byte) 31 /*0x1F*/,
        (byte) 135,
        (byte) 235,
        (byte) 184,
        (byte) 231,
        (byte) 184,
        (byte) 8,
        (byte) 16 /*0x10*/,
        (byte) 32 /*0x20*/,
        (byte) 147,
        (byte) 173,
        (byte) 94,
        (byte) 84,
        (byte) 11,
        (byte) 18,
        (byte) 129,
        (byte) 142,
        (byte) 44
      };
      byte[] numArray5 = new byte[55];
      numArray5[26] = (byte) 150;
      numArray5[29] = (byte) 113;
      numArray5[2] = (byte) 60;
      numArray5[3] = (byte) 154;
      numArray5[53] = (byte) 74;
      numArray5[7] = (byte) 166;
      numArray5[6] = (byte) 40;
      numArray5[10] = (byte) 48 /*0x30*/;
      numArray5[38] = (byte) 186;
      numArray5[9] = (byte) 78;
      numArray5[54] = (byte) 223;
      numArray5[11] = (byte) 98;
      numArray5[12] = (byte) 83;
      numArray5[13] = (byte) 1;
      numArray5[8] = (byte) 176 /*0xB0*/;
      numArray5[15] = (byte) 48 /*0x30*/;
      numArray5[49] = (byte) 60;
      numArray5[0] = (byte) 93;
      numArray5[52] = (byte) 58;
      numArray5[19] = (byte) 25;
      numArray5[33] = (byte) 208 /*0xD0*/;
      numArray5[34] = (byte) 236;
      numArray5[22] = (byte) 193;
      numArray5[14] = (byte) 44;
      numArray5[24] = (byte) 234;
      numArray5[35] = (byte) 119;
      numArray5[17] = (byte) 78;
      numArray5[32 /*0x20*/] = (byte) 130;
      numArray5[1] = (byte) 200;
      numArray5[48 /*0x30*/] = (byte) 21;
      numArray5[30] = (byte) 34;
      numArray5[31 /*0x1F*/] = (byte) 30;
      numArray5[51] = (byte) 253;
      numArray5[45] = (byte) 171;
      numArray5[4] = (byte) 245;
      numArray5[50] = (byte) 143;
      numArray5[5] = (byte) 128 /*0x80*/;
      numArray5[25] = (byte) 26;
      numArray5[40] = (byte) 99;
      numArray5[39] = (byte) 20;
      numArray5[18] = (byte) 147;
      numArray5[41] = (byte) 195;
      numArray5[42] = byte.MaxValue;
      numArray5[21] = (byte) 47;
      numArray5[44] = (byte) 158;
      numArray5[43] = (byte) 131;
      numArray5[28] = (byte) 238;
      numArray5[47] = (byte) 132;
      numArray5[46] = (byte) 137;
      numArray5[36] = (byte) 150;
      numArray5[37] = (byte) 50;
      numArray5[23] = (byte) 210;
      numArray5[16 /*0x10*/] = (byte) 197;
      numArray5[27] = (byte) 151;
      numArray5[20] = (byte) 75;
      key.Query(true, 339, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[55]
      {
        (byte) 18,
        (byte) 125,
        (byte) 228,
        (byte) 182,
        (byte) 236,
        (byte) 249,
        (byte) 84,
        (byte) 42,
        (byte) 229,
        (byte) 194,
        (byte) 229,
        (byte) 137,
        (byte) 92,
        (byte) 109,
        (byte) 239,
        (byte) 201,
        (byte) 106,
        (byte) 2,
        (byte) 20,
        (byte) 174,
        (byte) 154,
        (byte) 178,
        (byte) 153,
        (byte) 4,
        (byte) 111,
        (byte) 219,
        (byte) 241,
        (byte) 192 /*0xC0*/,
        (byte) 219,
        (byte) 254,
        (byte) 173,
        (byte) 165,
        (byte) 141,
        (byte) 225,
        (byte) 76,
        (byte) 0,
        (byte) 84,
        (byte) 8,
        (byte) 122,
        (byte) 122,
        (byte) 177,
        (byte) 199,
        (byte) 239,
        (byte) 199,
        (byte) 126,
        (byte) 124,
        (byte) 45,
        (byte) 28,
        (byte) 174,
        (byte) 70,
        (byte) 237,
        (byte) 35,
        (byte) 144 /*0x90*/,
        (byte) 225,
        (byte) 252
      };
      byte[] numArray7 = new byte[55];
      numArray7[33] = (byte) 82;
      numArray7[48 /*0x30*/] = (byte) 166;
      numArray7[2] = (byte) 32 /*0x20*/;
      numArray7[3] = (byte) 53;
      numArray7[4] = (byte) 90;
      numArray7[1] = (byte) 197;
      numArray7[6] = (byte) 27;
      numArray7[7] = (byte) 50;
      numArray7[9] = (byte) 21;
      numArray7[53] = (byte) 63 /*0x3F*/;
      numArray7[21] = (byte) 184;
      numArray7[11] = (byte) 101;
      numArray7[26] = (byte) 27;
      numArray7[31 /*0x1F*/] = (byte) 153;
      numArray7[32 /*0x20*/] = (byte) 56;
      numArray7[35] = (byte) 239;
      numArray7[46] = (byte) 90;
      numArray7[5] = (byte) 1;
      numArray7[49] = (byte) 57;
      numArray7[19] = (byte) 8;
      numArray7[20] = (byte) 105;
      numArray7[40] = (byte) 92;
      numArray7[14] = (byte) 144 /*0x90*/;
      numArray7[23] = (byte) 116;
      numArray7[22] = (byte) 148;
      numArray7[42] = (byte) 224 /*0xE0*/;
      numArray7[25] = (byte) 37;
      numArray7[27] = (byte) 172;
      numArray7[28] = (byte) 171;
      numArray7[29] = (byte) 236;
      numArray7[18] = (byte) 11;
      numArray7[24] = (byte) 68;
      numArray7[8] = (byte) 103;
      numArray7[15] = (byte) 231;
      numArray7[17] = (byte) 39;
      numArray7[50] = (byte) 73;
      numArray7[36] = (byte) 52;
      numArray7[37] = (byte) 89;
      numArray7[38] = (byte) 119;
      numArray7[10] = (byte) 75;
      numArray7[13] = (byte) 145;
      numArray7[41] = (byte) 102;
      numArray7[30] = (byte) 218;
      numArray7[12] = (byte) 238;
      numArray7[44] = (byte) 168;
      numArray7[45] = (byte) 27;
      numArray7[52] = (byte) 105;
      numArray7[34] = (byte) 169;
      numArray7[16 /*0x10*/] = (byte) 132;
      numArray7[39] = (byte) 217;
      numArray7[43] = (byte) 229;
      numArray7[51] = (byte) 111;
      numArray7[54] = (byte) 237;
      numArray7[0] = (byte) 181;
      numArray7[47] = (byte) 137;
      key.Query(true, 339, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[32 /*0x20*/]
      {
        (byte) 89,
        (byte) 83,
        (byte) 97,
        (byte) 50,
        (byte) 130,
        (byte) 186,
        (byte) 213,
        (byte) 139,
        (byte) 42,
        (byte) 79,
        (byte) 150,
        (byte) 52,
        (byte) 209,
        byte.MaxValue,
        (byte) 159,
        (byte) 30,
        (byte) 37,
        (byte) 223,
        (byte) 91,
        (byte) 185,
        (byte) 32 /*0x20*/,
        (byte) 71,
        (byte) 114,
        (byte) 252,
        (byte) 218,
        (byte) 82,
        (byte) 195,
        (byte) 132,
        (byte) 239,
        (byte) 29,
        (byte) 198,
        (byte) 187
      };
      byte[] numArray9 = new byte[32 /*0x20*/]
      {
        (byte) 120,
        (byte) 199,
        (byte) 178,
        (byte) 56,
        (byte) 103,
        (byte) 59,
        (byte) 20,
        (byte) 158,
        (byte) 71,
        (byte) 166,
        (byte) 199,
        (byte) 114,
        (byte) 34,
        (byte) 44,
        (byte) 8,
        (byte) 228,
        (byte) 74,
        (byte) 75,
        (byte) 3,
        (byte) 141,
        (byte) 225,
        (byte) 13,
        (byte) 101,
        (byte) 2,
        (byte) 233,
        (byte) 137,
        (byte) 88,
        (byte) 217,
        (byte) 35,
        (byte) 247,
        (byte) 67,
        (byte) 8
      };
      key.Query(true, 339, numArray8, numArray8);
      Array.Copy((Array) numArray8, 0, (Array) numArray1, 165, 32 /*0x20*/);
      for (int index = 0; index < 32 /*0x20*/; ++index)
        numArray1[index + 165] ^= numArray9[index];
      byte[] numArray10 = new byte[49];
      byte[] response = new byte[49];
      Array.Copy((Array) sc_886.sspq, 14, (Array) numArray10, 0, 49);
      key.Query(true, 339, numArray10, response);
      Array.Copy((Array) sc_886.sspr, 14, (Array) numArray10, 0, 49);
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
    byte[] numArray11 = new byte[197];
    byte[] numArray12 = new byte[55]
    {
      (byte) 192 /*0xC0*/,
      (byte) 237,
      (byte) 106,
      (byte) 26,
      (byte) 8,
      (byte) 140,
      (byte) 163,
      (byte) 233,
      (byte) 106,
      (byte) 199,
      (byte) 225,
      (byte) 137,
      (byte) 7,
      (byte) 169,
      (byte) 65,
      (byte) 224 /*0xE0*/,
      (byte) 41,
      (byte) 236,
      (byte) 186,
      (byte) 102,
      (byte) 236,
      (byte) 194,
      (byte) 48 /*0x30*/,
      (byte) 93,
      (byte) 246,
      (byte) 223,
      (byte) 160 /*0xA0*/,
      (byte) 131,
      (byte) 67,
      (byte) 204,
      (byte) 4,
      (byte) 185,
      (byte) 41,
      (byte) 192 /*0xC0*/,
      (byte) 199,
      (byte) 237,
      (byte) 123,
      (byte) 171,
      (byte) 220,
      (byte) 165,
      (byte) 113,
      (byte) 251,
      (byte) 216,
      (byte) 214,
      (byte) 164,
      (byte) 113,
      (byte) 43,
      (byte) 151,
      (byte) 159,
      (byte) 51,
      (byte) 58,
      (byte) 5,
      (byte) 221,
      (byte) 12,
      (byte) 95
    };
    byte[] numArray13 = new byte[55]
    {
      (byte) 227,
      (byte) 91,
      (byte) 218,
      (byte) 62,
      (byte) 49,
      (byte) 149,
      (byte) 61,
      (byte) 44,
      (byte) 0,
      (byte) 18,
      (byte) 57,
      (byte) 178,
      (byte) 216,
      (byte) 70,
      (byte) 49,
      (byte) 45,
      (byte) 252,
      (byte) 220,
      (byte) 179,
      (byte) 47,
      (byte) 200,
      (byte) 33,
      (byte) 161,
      (byte) 150,
      (byte) 64 /*0x40*/,
      (byte) 197,
      (byte) 169,
      (byte) 42,
      (byte) 180,
      (byte) 179,
      (byte) 112 /*0x70*/,
      (byte) 47,
      (byte) 60,
      (byte) 71,
      (byte) 32 /*0x20*/,
      (byte) 205,
      (byte) 196,
      (byte) 50,
      (byte) 98,
      (byte) 22,
      (byte) 201,
      (byte) 193,
      (byte) 193,
      (byte) 193,
      (byte) 120,
      (byte) 206,
      (byte) 116,
      (byte) 54,
      (byte) 176 /*0xB0*/,
      (byte) 0,
      (byte) 18,
      (byte) 185,
      (byte) 151,
      (byte) 22,
      (byte) 113
    };
    key.Query(true, 339, numArray12, numArray12);
    Array.Copy((Array) numArray12, 0, (Array) numArray11, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray11[index] ^= numArray13[index];
    byte[] numArray14 = new byte[55]
    {
      (byte) 12,
      (byte) 35,
      (byte) 41,
      (byte) 236,
      (byte) 32 /*0x20*/,
      (byte) 165,
      (byte) 158,
      (byte) 194,
      (byte) 40,
      (byte) 96 /*0x60*/,
      (byte) 71,
      (byte) 226,
      (byte) 83,
      (byte) 101,
      (byte) 125,
      (byte) 16 /*0x10*/,
      (byte) 21,
      (byte) 244,
      (byte) 241,
      (byte) 233,
      (byte) 90,
      (byte) 184,
      (byte) 133,
      (byte) 180,
      (byte) 9,
      (byte) 227,
      (byte) 16 /*0x10*/,
      (byte) 146,
      byte.MaxValue,
      (byte) 219,
      (byte) 28,
      (byte) 96 /*0x60*/,
      (byte) 200,
      (byte) 49,
      (byte) 110,
      (byte) 136,
      (byte) 179,
      (byte) 184,
      (byte) 81,
      (byte) 85,
      (byte) 58,
      (byte) 20,
      (byte) 124,
      (byte) 17,
      (byte) 228,
      (byte) 180,
      (byte) 76,
      (byte) 206,
      (byte) 188,
      (byte) 245,
      (byte) 200,
      (byte) 24,
      (byte) 110,
      (byte) 21,
      (byte) 97
    };
    byte[] numArray15 = new byte[55]
    {
      (byte) 155,
      (byte) 219,
      (byte) 208 /*0xD0*/,
      (byte) 110,
      (byte) 241,
      (byte) 200,
      (byte) 243,
      (byte) 152,
      (byte) 186,
      (byte) 127 /*0x7F*/,
      (byte) 103,
      (byte) 68,
      (byte) 247,
      (byte) 91,
      (byte) 34,
      (byte) 89,
      (byte) 55,
      (byte) 28,
      (byte) 101,
      (byte) 53,
      (byte) 47,
      (byte) 53,
      (byte) 102,
      (byte) 119,
      (byte) 82,
      (byte) 49,
      (byte) 181,
      (byte) 187,
      (byte) 221,
      (byte) 248,
      (byte) 104,
      (byte) 54,
      (byte) 14,
      (byte) 193,
      (byte) 193,
      (byte) 151,
      (byte) 122,
      (byte) 20,
      (byte) 84,
      (byte) 50,
      (byte) 81,
      (byte) 157,
      (byte) 46,
      (byte) 15,
      (byte) 140,
      (byte) 218,
      (byte) 130,
      (byte) 162,
      (byte) 252,
      (byte) 208 /*0xD0*/,
      (byte) 168,
      (byte) 159,
      (byte) 97,
      (byte) 34,
      (byte) 41
    };
    key.Query(true, 339, numArray14, numArray14);
    Array.Copy((Array) numArray14, 0, (Array) numArray11, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray11[index + 55] ^= numArray15[index];
    byte[] numArray16 = new byte[55];
    numArray16[31 /*0x1F*/] = (byte) 207;
    numArray16[1] = (byte) 227;
    numArray16[54] = (byte) 40;
    numArray16[32 /*0x20*/] = (byte) 26;
    numArray16[4] = (byte) 11;
    numArray16[45] = (byte) 215;
    numArray16[44] = (byte) 41;
    numArray16[7] = (byte) 67;
    numArray16[49] = (byte) 117;
    numArray16[9] = (byte) 25;
    numArray16[35] = (byte) 213;
    numArray16[5] = (byte) 195;
    numArray16[25] = (byte) 213;
    numArray16[28] = (byte) 3;
    numArray16[14] = (byte) 92;
    numArray16[15] = (byte) 163;
    numArray16[21] = (byte) 208 /*0xD0*/;
    numArray16[27] = (byte) 18;
    numArray16[18] = (byte) 70;
    numArray16[19] = (byte) 60;
    numArray16[20] = (byte) 116;
    numArray16[24] = (byte) 66;
    numArray16[41] = (byte) 205;
    numArray16[0] = (byte) 117;
    numArray16[10] = (byte) 34;
    numArray16[43] = (byte) 155;
    numArray16[26] = (byte) 10;
    numArray16[6] = (byte) 77;
    numArray16[29] = (byte) 242;
    numArray16[33] = (byte) 234;
    numArray16[38] = (byte) 47;
    numArray16[22] = (byte) 84;
    numArray16[11] = (byte) 246;
    numArray16[51] = (byte) 102;
    numArray16[34] = (byte) 62;
    numArray16[3] = (byte) 148;
    numArray16[8] = (byte) 230;
    numArray16[37] = (byte) 192 /*0xC0*/;
    numArray16[12] = (byte) 245;
    numArray16[39] = (byte) 80 /*0x50*/;
    numArray16[30] = (byte) 179;
    numArray16[40] = (byte) 47;
    numArray16[42] = (byte) 253;
    numArray16[2] = (byte) 61;
    numArray16[23] = (byte) 151;
    numArray16[53] = (byte) 23;
    numArray16[17] = (byte) 174;
    numArray16[47] = (byte) 151;
    numArray16[13] = (byte) 93;
    numArray16[46] = (byte) 199;
    numArray16[50] = (byte) 210;
    numArray16[16 /*0x10*/] = (byte) 155;
    numArray16[52] = (byte) 27;
    numArray16[36] = (byte) 20;
    numArray16[48 /*0x30*/] = (byte) 108;
    byte[] numArray17 = new byte[55]
    {
      (byte) 121,
      (byte) 186,
      (byte) 232,
      (byte) 167,
      (byte) 60,
      (byte) 64 /*0x40*/,
      (byte) 85,
      (byte) 24,
      (byte) 170,
      (byte) 4,
      (byte) 94,
      (byte) 230,
      (byte) 99,
      (byte) 60,
      (byte) 157,
      (byte) 9,
      (byte) 156,
      (byte) 109,
      (byte) 50,
      (byte) 96 /*0x60*/,
      (byte) 177,
      (byte) 181,
      (byte) 253,
      (byte) 9,
      (byte) 80 /*0x50*/,
      (byte) 244,
      (byte) 31 /*0x1F*/,
      (byte) 19,
      (byte) 120,
      (byte) 252,
      (byte) 49,
      (byte) 145,
      (byte) 26,
      (byte) 208 /*0xD0*/,
      (byte) 76,
      (byte) 10,
      (byte) 218,
      (byte) 102,
      (byte) 164,
      (byte) 120,
      (byte) 26,
      (byte) 245,
      (byte) 85,
      (byte) 22,
      (byte) 83,
      (byte) 180,
      (byte) 180,
      (byte) 39,
      (byte) 59,
      (byte) 70,
      (byte) 35,
      (byte) 152,
      (byte) 182,
      (byte) 66,
      (byte) 138
    };
    key.Query(true, 339, numArray16, numArray16);
    Array.Copy((Array) numArray16, 0, (Array) numArray11, 110, 55);
    for (int index = 0; index < 55; ++index)
      numArray11[index + 110] ^= numArray17[index];
    byte[] numArray18 = new byte[32 /*0x20*/]
    {
      (byte) 55,
      (byte) 231,
      (byte) 40,
      (byte) 71,
      (byte) 148,
      (byte) 177,
      (byte) 210,
      (byte) 24,
      (byte) 160 /*0xA0*/,
      (byte) 217,
      (byte) 72,
      (byte) 133,
      (byte) 64 /*0x40*/,
      (byte) 32 /*0x20*/,
      (byte) 108,
      (byte) 135,
      (byte) 94,
      (byte) 24,
      (byte) 25,
      (byte) 199,
      (byte) 132,
      (byte) 152,
      (byte) 111,
      (byte) 80 /*0x50*/,
      (byte) 249,
      (byte) 193,
      (byte) 167,
      (byte) 41,
      (byte) 102,
      (byte) 141,
      (byte) 247,
      (byte) 64 /*0x40*/
    };
    byte[] numArray19 = new byte[32 /*0x20*/]
    {
      (byte) 238,
      (byte) 119,
      (byte) 65,
      (byte) 250,
      (byte) 62,
      (byte) 137,
      (byte) 135,
      (byte) 97,
      (byte) 5,
      (byte) 125,
      (byte) 253,
      (byte) 208 /*0xD0*/,
      (byte) 166,
      (byte) 208 /*0xD0*/,
      (byte) 4,
      (byte) 80 /*0x50*/,
      (byte) 18,
      (byte) 16 /*0x10*/,
      (byte) 152,
      (byte) 105,
      (byte) 5,
      (byte) 184,
      (byte) 180,
      (byte) 191,
      (byte) 167,
      (byte) 193,
      (byte) 81,
      (byte) 182,
      (byte) 197,
      (byte) 142,
      (byte) 218,
      (byte) 35
    };
    key.Query(true, 339, numArray18, numArray18);
    Array.Copy((Array) numArray18, 0, (Array) numArray11, 165, 32 /*0x20*/);
    for (int index = 0; index < 32 /*0x20*/; ++index)
      numArray11[index + 165] ^= numArray19[index];
    return Encoding.UTF8.GetString(numArray11);
  }
}
