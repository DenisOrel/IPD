// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_552
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_552
{
  private static byte[] sspq = new byte[236]
  {
    (byte) 218,
    (byte) 141,
    (byte) 150,
    (byte) 22,
    (byte) 33,
    (byte) 104,
    (byte) 110,
    (byte) 129,
    (byte) 48 /*0x30*/,
    (byte) 121,
    (byte) 24,
    (byte) 124,
    (byte) 135,
    (byte) 107,
    (byte) 29,
    (byte) 25,
    (byte) 25,
    (byte) 45,
    (byte) 86,
    (byte) 115,
    (byte) 154,
    (byte) 201,
    (byte) 83,
    (byte) 176 /*0xB0*/,
    (byte) 52,
    (byte) 130,
    (byte) 86,
    (byte) 145,
    (byte) 123,
    (byte) 206,
    (byte) 216,
    (byte) 245,
    (byte) 7,
    (byte) 213,
    (byte) 219,
    (byte) 201,
    (byte) 126,
    (byte) 26,
    (byte) 12,
    (byte) 189,
    (byte) 144 /*0x90*/,
    (byte) 195,
    (byte) 191,
    (byte) 165,
    (byte) 156,
    (byte) 142,
    (byte) 170,
    (byte) 202,
    (byte) 1,
    (byte) 246,
    (byte) 72,
    (byte) 170,
    (byte) 74,
    (byte) 106,
    (byte) 218,
    (byte) 4,
    (byte) 228,
    (byte) 76,
    (byte) 10,
    (byte) 57,
    (byte) 178,
    (byte) 44,
    (byte) 76,
    (byte) 173,
    (byte) 164,
    (byte) 221,
    (byte) 144 /*0x90*/,
    (byte) 247,
    (byte) 34,
    (byte) 32 /*0x20*/,
    (byte) 190,
    (byte) 147,
    (byte) 150,
    (byte) 90,
    (byte) 152,
    (byte) 191,
    (byte) 210,
    (byte) 146,
    (byte) 93,
    (byte) 94,
    (byte) 18,
    (byte) 225,
    (byte) 28,
    (byte) 81,
    (byte) 8,
    (byte) 162,
    (byte) 175,
    (byte) 81,
    (byte) 139,
    (byte) 139,
    (byte) 245,
    (byte) 196,
    (byte) 42,
    (byte) 61,
    (byte) 72,
    (byte) 15,
    (byte) 254,
    (byte) 213,
    (byte) 63 /*0x3F*/,
    (byte) 26,
    (byte) 55,
    (byte) 128 /*0x80*/,
    (byte) 39,
    (byte) 44,
    (byte) 193,
    (byte) 81,
    (byte) 110,
    (byte) 49,
    (byte) 198,
    (byte) 68,
    (byte) 147,
    (byte) 34,
    (byte) 245,
    (byte) 188,
    (byte) 144 /*0x90*/,
    (byte) 209,
    (byte) 75,
    (byte) 188,
    (byte) 82,
    (byte) 121,
    (byte) 122,
    (byte) 133,
    (byte) 55,
    (byte) 216,
    (byte) 5,
    (byte) 208 /*0xD0*/,
    (byte) 45,
    (byte) 151,
    (byte) 178,
    (byte) 73,
    (byte) 79,
    (byte) 104,
    (byte) 4,
    (byte) 4,
    (byte) 139,
    (byte) 177,
    (byte) 11,
    (byte) 48 /*0x30*/,
    (byte) 182,
    byte.MaxValue,
    (byte) 135,
    (byte) 70,
    (byte) 73,
    (byte) 172,
    (byte) 8,
    (byte) 191,
    (byte) 209,
    (byte) 248,
    (byte) 58,
    (byte) 164,
    (byte) 0,
    (byte) 58,
    (byte) 51,
    (byte) 246,
    (byte) 198,
    (byte) 36,
    (byte) 28,
    (byte) 242,
    (byte) 88,
    (byte) 4,
    (byte) 29,
    (byte) 45,
    (byte) 151,
    (byte) 50,
    (byte) 142,
    (byte) 72,
    (byte) 99,
    (byte) 141,
    (byte) 202,
    (byte) 181,
    (byte) 53,
    (byte) 89,
    (byte) 71,
    (byte) 49,
    (byte) 11,
    (byte) 148,
    (byte) 3,
    (byte) 218,
    (byte) 149,
    (byte) 138,
    (byte) 237,
    (byte) 177,
    (byte) 4,
    (byte) 173,
    (byte) 111,
    (byte) 210,
    (byte) 216,
    (byte) 124,
    (byte) 231,
    (byte) 37,
    (byte) 249,
    (byte) 94,
    (byte) 57,
    (byte) 25,
    (byte) 178,
    (byte) 232,
    (byte) 231,
    (byte) 213,
    (byte) 19,
    (byte) 145,
    (byte) 220,
    (byte) 16 /*0x10*/,
    (byte) 23,
    (byte) 144 /*0x90*/,
    (byte) 53,
    (byte) 66,
    (byte) 225,
    (byte) 110,
    (byte) 201,
    (byte) 147,
    (byte) 11,
    (byte) 41,
    (byte) 222,
    (byte) 197,
    (byte) 97,
    (byte) 130,
    (byte) 159,
    (byte) 43,
    (byte) 55,
    (byte) 80 /*0x50*/,
    (byte) 19,
    (byte) 205,
    (byte) 109,
    (byte) 226,
    (byte) 38,
    (byte) 83,
    (byte) 167,
    (byte) 245,
    (byte) 60,
    (byte) 50,
    (byte) 202,
    (byte) 229,
    (byte) 123,
    (byte) 89,
    (byte) 122,
    (byte) 122
  };
  private static byte[] sspr = new byte[236]
  {
    (byte) 244,
    (byte) 16 /*0x10*/,
    (byte) 96 /*0x60*/,
    (byte) 154,
    (byte) 25,
    (byte) 156,
    (byte) 236,
    (byte) 247,
    (byte) 112 /*0x70*/,
    (byte) 214,
    (byte) 202,
    (byte) 217,
    (byte) 81,
    (byte) 14,
    (byte) 67,
    (byte) 253,
    (byte) 137,
    (byte) 7,
    (byte) 88,
    (byte) 114,
    (byte) 18,
    (byte) 9,
    (byte) 179,
    (byte) 157,
    (byte) 166,
    (byte) 209,
    (byte) 39,
    (byte) 151,
    (byte) 18,
    (byte) 125,
    (byte) 17,
    (byte) 181,
    (byte) 222,
    (byte) 132,
    (byte) 173,
    (byte) 14,
    (byte) 196,
    (byte) 118,
    (byte) 235,
    (byte) 167,
    (byte) 233,
    (byte) 226,
    (byte) 189,
    (byte) 138,
    (byte) 198,
    (byte) 133,
    (byte) 124,
    (byte) 232,
    byte.MaxValue,
    (byte) 57,
    (byte) 136,
    (byte) 86,
    (byte) 72,
    (byte) 34,
    (byte) 182,
    (byte) 74,
    (byte) 120,
    (byte) 43,
    (byte) 218,
    (byte) 53,
    (byte) 137,
    (byte) 245,
    (byte) 146,
    (byte) 200,
    (byte) 188,
    (byte) 175,
    (byte) 179,
    (byte) 40,
    (byte) 28,
    (byte) 110,
    (byte) 0,
    (byte) 208 /*0xD0*/,
    (byte) 89,
    (byte) 108,
    (byte) 203,
    (byte) 26,
    (byte) 50,
    (byte) 34,
    (byte) 61,
    (byte) 189,
    (byte) 148,
    (byte) 41,
    (byte) 8,
    (byte) 207,
    (byte) 240 /*0xF0*/,
    (byte) 185,
    (byte) 159,
    (byte) 52,
    (byte) 25,
    (byte) 209,
    (byte) 185,
    (byte) 171,
    (byte) 169,
    (byte) 149,
    (byte) 194,
    (byte) 129,
    (byte) 253,
    (byte) 147,
    (byte) 207,
    (byte) 225,
    (byte) 15,
    (byte) 62,
    (byte) 237,
    (byte) 110,
    (byte) 111,
    (byte) 76,
    (byte) 129,
    (byte) 88,
    (byte) 64 /*0x40*/,
    (byte) 141,
    (byte) 145,
    (byte) 152,
    (byte) 117,
    (byte) 171,
    (byte) 248,
    (byte) 160 /*0xA0*/,
    (byte) 107,
    (byte) 237,
    (byte) 75,
    (byte) 32 /*0x20*/,
    (byte) 65,
    (byte) 75,
    (byte) 238,
    (byte) 53,
    (byte) 240 /*0xF0*/,
    (byte) 58,
    (byte) 150,
    (byte) 187,
    (byte) 224 /*0xE0*/,
    (byte) 125,
    (byte) 173,
    (byte) 232,
    (byte) 135,
    (byte) 118,
    (byte) 209,
    (byte) 14,
    (byte) 186,
    (byte) 41,
    (byte) 64 /*0x40*/,
    (byte) 216,
    (byte) 250,
    (byte) 235,
    (byte) 169,
    (byte) 57,
    (byte) 218,
    (byte) 63 /*0x3F*/,
    (byte) 251,
    (byte) 169,
    (byte) 127 /*0x7F*/,
    (byte) 240 /*0xF0*/,
    (byte) 153,
    (byte) 159,
    (byte) 89,
    (byte) 90,
    (byte) 95,
    (byte) 49,
    (byte) 208 /*0xD0*/,
    (byte) 148,
    (byte) 60,
    (byte) 10,
    (byte) 22,
    (byte) 211,
    (byte) 141,
    (byte) 136,
    (byte) 139,
    (byte) 188,
    (byte) 103,
    (byte) 41,
    (byte) 169,
    (byte) 168,
    (byte) 199,
    (byte) 213,
    (byte) 65,
    (byte) 190,
    (byte) 34,
    (byte) 212,
    (byte) 240 /*0xF0*/,
    (byte) 132,
    (byte) 249,
    (byte) 209,
    (byte) 86,
    (byte) 236,
    (byte) 208 /*0xD0*/,
    (byte) 207,
    (byte) 59,
    (byte) 172,
    (byte) 25,
    (byte) 87,
    (byte) 201,
    (byte) 37,
    (byte) 173,
    (byte) 106,
    (byte) 144 /*0x90*/,
    (byte) 51,
    (byte) 238,
    (byte) 114,
    (byte) 231,
    (byte) 44,
    (byte) 130,
    (byte) 92,
    (byte) 69,
    (byte) 162,
    (byte) 110,
    (byte) 127 /*0x7F*/,
    (byte) 235,
    (byte) 249,
    (byte) 74,
    (byte) 24,
    (byte) 86,
    (byte) 148,
    (byte) 221,
    (byte) 237,
    (byte) 199,
    (byte) 20,
    (byte) 226,
    (byte) 62,
    (byte) 235,
    (byte) 94,
    (byte) 121,
    (byte) 85,
    (byte) 159,
    (byte) 128 /*0x80*/,
    (byte) 210,
    (byte) 26,
    (byte) 113,
    (byte) 196,
    (byte) 102,
    (byte) 111,
    (byte) 23,
    (byte) 18,
    (byte) 140,
    (byte) 221,
    (byte) 151,
    (byte) 97,
    (byte) 168,
    (byte) 80 /*0x50*/
  };

  internal static string ssp_archives_553()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[230];
      byte[] numArray2 = new byte[55];
      numArray2[40] = (byte) 149;
      numArray2[37] = (byte) 69;
      numArray2[34] = (byte) 210;
      numArray2[46] = (byte) 206;
      numArray2[43] = (byte) 120;
      numArray2[39] = (byte) 135;
      numArray2[6] = (byte) 240 /*0xF0*/;
      numArray2[7] = (byte) 133;
      numArray2[13] = (byte) 238;
      numArray2[9] = (byte) 75;
      numArray2[18] = (byte) 6;
      numArray2[11] = (byte) 239;
      numArray2[19] = (byte) 25;
      numArray2[32 /*0x20*/] = (byte) 215;
      numArray2[14] = (byte) 140;
      numArray2[26] = (byte) 43;
      numArray2[16 /*0x10*/] = (byte) 203;
      numArray2[12] = (byte) 226;
      numArray2[24] = (byte) 28;
      numArray2[1] = (byte) 97;
      numArray2[38] = (byte) 218;
      numArray2[21] = (byte) 225;
      numArray2[22] = (byte) 168;
      numArray2[45] = (byte) 148;
      numArray2[3] = (byte) 159;
      numArray2[25] = (byte) 101;
      numArray2[54] = (byte) 122;
      numArray2[51] = (byte) 211;
      numArray2[23] = (byte) 76;
      numArray2[48 /*0x30*/] = (byte) 181;
      numArray2[17] = (byte) 23;
      numArray2[8] = (byte) 19;
      numArray2[47] = (byte) 166;
      numArray2[33] = (byte) 145;
      numArray2[0] = (byte) 148;
      numArray2[35] = (byte) 231;
      numArray2[36] = (byte) 248;
      numArray2[27] = (byte) 88;
      numArray2[5] = (byte) 180;
      numArray2[28] = (byte) 76;
      numArray2[30] = (byte) 211;
      numArray2[41] = (byte) 24;
      numArray2[42] = (byte) 36;
      numArray2[10] = (byte) 112 /*0x70*/;
      numArray2[44] = (byte) 112 /*0x70*/;
      numArray2[52] = (byte) 36;
      numArray2[20] = (byte) 202;
      numArray2[29] = (byte) 66;
      numArray2[4] = (byte) 97;
      numArray2[49] = (byte) 78;
      numArray2[31 /*0x1F*/] = (byte) 229;
      numArray2[50] = (byte) 82;
      numArray2[15] = (byte) 199;
      numArray2[53] = (byte) 141;
      numArray2[2] = (byte) 114;
      byte[] numArray3 = new byte[55]
      {
        (byte) 238,
        (byte) 40,
        (byte) 208 /*0xD0*/,
        (byte) 74,
        (byte) 84,
        (byte) 29,
        (byte) 152,
        (byte) 159,
        (byte) 44,
        (byte) 65,
        (byte) 202,
        (byte) 139,
        (byte) 249,
        (byte) 207,
        (byte) 213,
        (byte) 18,
        (byte) 0,
        (byte) 24,
        (byte) 127 /*0x7F*/,
        (byte) 173,
        (byte) 79,
        (byte) 56,
        (byte) 123,
        (byte) 217,
        (byte) 50,
        (byte) 202,
        (byte) 75,
        (byte) 85,
        (byte) 153,
        (byte) 208 /*0xD0*/,
        (byte) 82,
        (byte) 124,
        (byte) 158,
        (byte) 153,
        (byte) 219,
        (byte) 27,
        (byte) 244,
        (byte) 75,
        (byte) 208 /*0xD0*/,
        (byte) 128 /*0x80*/,
        (byte) 142,
        (byte) 195,
        (byte) 14,
        (byte) 81,
        (byte) 209,
        (byte) 219,
        (byte) 38,
        (byte) 92,
        (byte) 203,
        (byte) 107,
        (byte) 46,
        (byte) 99,
        (byte) 58,
        (byte) 15,
        (byte) 168
      };
      key.Query(true, 336, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 72,
        (byte) 250,
        (byte) 101,
        (byte) 3,
        (byte) 243,
        (byte) 58,
        (byte) 224 /*0xE0*/,
        (byte) 120,
        (byte) 201,
        (byte) 206,
        (byte) 57,
        (byte) 252,
        (byte) 117,
        (byte) 252,
        (byte) 194,
        (byte) 93,
        (byte) 128 /*0x80*/,
        (byte) 133,
        (byte) 209,
        (byte) 105,
        (byte) 48 /*0x30*/,
        (byte) 247,
        (byte) 12,
        (byte) 26,
        (byte) 250,
        (byte) 147,
        (byte) 127 /*0x7F*/,
        (byte) 34,
        (byte) 103,
        (byte) 28,
        (byte) 64 /*0x40*/,
        (byte) 78,
        (byte) 42,
        (byte) 167,
        (byte) 138,
        (byte) 199,
        (byte) 121,
        (byte) 250,
        (byte) 10,
        (byte) 165,
        (byte) 114,
        (byte) 134,
        (byte) 28,
        (byte) 172,
        (byte) 67,
        (byte) 141,
        (byte) 155,
        (byte) 130,
        (byte) 10,
        (byte) 57,
        (byte) 37,
        (byte) 131,
        (byte) 247,
        (byte) 139,
        (byte) 87
      };
      byte[] numArray5 = new byte[55];
      numArray5[30] = (byte) 214;
      numArray5[1] = (byte) 23;
      numArray5[2] = (byte) 171;
      numArray5[54] = (byte) 47;
      numArray5[16 /*0x10*/] = (byte) 122;
      numArray5[5] = (byte) 6;
      numArray5[41] = (byte) 3;
      numArray5[34] = (byte) 174;
      numArray5[8] = (byte) 241;
      numArray5[9] = (byte) 184;
      numArray5[52] = (byte) 87;
      numArray5[11] = (byte) 36;
      numArray5[14] = (byte) 180;
      numArray5[38] = (byte) 77;
      numArray5[23] = (byte) 86;
      numArray5[15] = (byte) 125;
      numArray5[13] = (byte) 71;
      numArray5[44] = (byte) 233;
      numArray5[18] = (byte) 126;
      numArray5[4] = (byte) 53;
      numArray5[28] = (byte) 187;
      numArray5[21] = (byte) 72;
      numArray5[22] = (byte) 148;
      numArray5[7] = (byte) 229;
      numArray5[24] = (byte) 247;
      numArray5[20] = (byte) 196;
      numArray5[26] = (byte) 6;
      numArray5[32 /*0x20*/] = (byte) 8;
      numArray5[31 /*0x1F*/] = (byte) 55;
      numArray5[29] = (byte) 195;
      numArray5[17] = (byte) 117;
      numArray5[37] = (byte) 55;
      numArray5[27] = (byte) 118;
      numArray5[33] = (byte) 196;
      numArray5[53] = (byte) 68;
      numArray5[19] = (byte) 151;
      numArray5[35] = (byte) 67;
      numArray5[25] = (byte) 144 /*0x90*/;
      numArray5[46] = (byte) 125;
      numArray5[10] = (byte) 213;
      numArray5[50] = (byte) 91;
      numArray5[39] = (byte) 151;
      numArray5[40] = (byte) 49;
      numArray5[43] = (byte) 248;
      numArray5[36] = (byte) 188;
      numArray5[45] = (byte) 215;
      numArray5[3] = (byte) 215;
      numArray5[47] = (byte) 146;
      numArray5[0] = (byte) 3;
      numArray5[49] = (byte) 51;
      numArray5[48 /*0x30*/] = (byte) 226;
      numArray5[12] = (byte) 22;
      numArray5[42] = (byte) 3;
      numArray5[51] = (byte) 134;
      numArray5[6] = (byte) 213;
      key.Query(true, 336, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[55];
      numArray6[29] = (byte) 139;
      numArray6[28] = (byte) 182;
      numArray6[2] = (byte) 105;
      numArray6[12] = (byte) 32 /*0x20*/;
      numArray6[4] = (byte) 165;
      numArray6[32 /*0x20*/] = (byte) 194;
      numArray6[46] = (byte) 239;
      numArray6[14] = (byte) 147;
      numArray6[8] = (byte) 147;
      numArray6[9] = (byte) 52;
      numArray6[0] = (byte) 116;
      numArray6[36] = (byte) 64 /*0x40*/;
      numArray6[48 /*0x30*/] = (byte) 12;
      numArray6[13] = (byte) 182;
      numArray6[31 /*0x1F*/] = (byte) 162;
      numArray6[15] = (byte) 210;
      numArray6[26] = (byte) 216;
      numArray6[17] = (byte) 66;
      numArray6[18] = (byte) 51;
      numArray6[16 /*0x10*/] = (byte) 65;
      numArray6[20] = (byte) 65;
      numArray6[43] = (byte) 134;
      numArray6[22] = (byte) 178;
      numArray6[23] = (byte) 42;
      numArray6[24] = (byte) 235;
      numArray6[11] = (byte) 56;
      numArray6[6] = (byte) 181;
      numArray6[27] = (byte) 87;
      numArray6[1] = (byte) 156;
      numArray6[40] = (byte) 185;
      numArray6[41] = (byte) 78;
      numArray6[30] = (byte) 9;
      numArray6[3] = (byte) 190;
      numArray6[33] = (byte) 76;
      numArray6[34] = (byte) 185;
      numArray6[10] = (byte) 126;
      numArray6[49] = (byte) 53;
      numArray6[37] = (byte) 44;
      numArray6[38] = (byte) 15;
      numArray6[39] = (byte) 146;
      numArray6[7] = (byte) 208 /*0xD0*/;
      numArray6[5] = (byte) 227;
      numArray6[54] = (byte) 75;
      numArray6[50] = (byte) 137;
      numArray6[44] = (byte) 79;
      numArray6[45] = (byte) 217;
      numArray6[25] = (byte) 8;
      numArray6[47] = (byte) 158;
      numArray6[42] = (byte) 197;
      numArray6[51] = (byte) 81;
      numArray6[35] = (byte) 85;
      numArray6[21] = (byte) 125;
      numArray6[52] = (byte) 121;
      numArray6[53] = (byte) 11;
      numArray6[19] = (byte) 106;
      byte[] numArray7 = new byte[55]
      {
        (byte) 251,
        (byte) 39,
        (byte) 185,
        (byte) 233,
        (byte) 25,
        (byte) 101,
        (byte) 59,
        (byte) 124,
        (byte) 189,
        (byte) 35,
        (byte) 149,
        (byte) 250,
        (byte) 124,
        (byte) 153,
        (byte) 14,
        (byte) 93,
        (byte) 0,
        (byte) 42,
        (byte) 57,
        (byte) 121,
        (byte) 14,
        (byte) 89,
        (byte) 6,
        (byte) 51,
        (byte) 142,
        (byte) 221,
        (byte) 18,
        (byte) 238,
        (byte) 225,
        (byte) 149,
        (byte) 50,
        (byte) 111,
        (byte) 100,
        (byte) 43,
        (byte) 122,
        (byte) 33,
        (byte) 163,
        (byte) 122,
        (byte) 2,
        (byte) 129,
        (byte) 7,
        (byte) 209,
        (byte) 217,
        (byte) 67,
        (byte) 224 /*0xE0*/,
        (byte) 18,
        (byte) 117,
        (byte) 59,
        (byte) 222,
        (byte) 26,
        (byte) 242,
        (byte) 232,
        (byte) 121,
        (byte) 51,
        (byte) 77
      };
      key.Query(true, 336, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[55];
      numArray8[17] = (byte) 86;
      numArray8[20] = (byte) 208 /*0xD0*/;
      numArray8[8] = (byte) 179;
      numArray8[3] = (byte) 207;
      numArray8[53] = (byte) 70;
      numArray8[5] = (byte) 153;
      numArray8[47] = (byte) 18;
      numArray8[44] = (byte) 72;
      numArray8[32 /*0x20*/] = (byte) 129;
      numArray8[9] = (byte) 190;
      numArray8[54] = (byte) 236;
      numArray8[14] = (byte) 187;
      numArray8[19] = (byte) 249;
      numArray8[30] = (byte) 91;
      numArray8[1] = (byte) 193;
      numArray8[33] = (byte) 174;
      numArray8[16 /*0x10*/] = (byte) 24;
      numArray8[4] = (byte) 19;
      numArray8[18] = (byte) 191;
      numArray8[2] = (byte) 130;
      numArray8[46] = (byte) 251;
      numArray8[21] = (byte) 104;
      numArray8[22] = (byte) 173;
      numArray8[23] = (byte) 254;
      numArray8[24] = (byte) 153;
      numArray8[25] = (byte) 125;
      numArray8[10] = (byte) 210;
      numArray8[27] = (byte) 141;
      numArray8[38] = (byte) 71;
      numArray8[29] = (byte) 79;
      numArray8[0] = (byte) 99;
      numArray8[31 /*0x1F*/] = (byte) 168;
      numArray8[28] = (byte) 153;
      numArray8[40] = (byte) 184;
      numArray8[34] = (byte) 157;
      numArray8[6] = (byte) 5;
      numArray8[36] = (byte) 239;
      numArray8[37] = (byte) 184;
      numArray8[26] = (byte) 221;
      numArray8[45] = (byte) 100;
      numArray8[50] = (byte) 191;
      numArray8[41] = (byte) 232;
      numArray8[42] = (byte) 191;
      numArray8[43] = (byte) 164;
      numArray8[39] = (byte) 155;
      numArray8[13] = (byte) 144 /*0x90*/;
      numArray8[7] = (byte) 114;
      numArray8[11] = (byte) 60;
      numArray8[48 /*0x30*/] = (byte) 184;
      numArray8[35] = (byte) 151;
      numArray8[51] = (byte) 4;
      numArray8[49] = (byte) 93;
      numArray8[12] = (byte) 210;
      numArray8[52] = (byte) 153;
      numArray8[15] = (byte) 192 /*0xC0*/;
      byte[] numArray9 = new byte[55];
      numArray9[42] = (byte) 173;
      numArray9[1] = (byte) 224 /*0xE0*/;
      numArray9[27] = (byte) 59;
      numArray9[35] = (byte) 239;
      numArray9[18] = (byte) 191;
      numArray9[49] = (byte) 207;
      numArray9[6] = (byte) 54;
      numArray9[7] = (byte) 190;
      numArray9[8] = (byte) 44;
      numArray9[25] = (byte) 52;
      numArray9[10] = (byte) 49;
      numArray9[11] = (byte) 23;
      numArray9[22] = (byte) 30;
      numArray9[17] = (byte) 196;
      numArray9[50] = (byte) 159;
      numArray9[15] = (byte) 8;
      numArray9[16 /*0x10*/] = (byte) 150;
      numArray9[38] = (byte) 176 /*0xB0*/;
      numArray9[24] = (byte) 6;
      numArray9[13] = (byte) 34;
      numArray9[20] = (byte) 235;
      numArray9[47] = (byte) 55;
      numArray9[41] = (byte) 210;
      numArray9[14] = (byte) 190;
      numArray9[23] = (byte) 225;
      numArray9[26] = (byte) 244;
      numArray9[2] = (byte) 14;
      numArray9[46] = (byte) 124;
      numArray9[33] = (byte) 8;
      numArray9[5] = (byte) 224 /*0xE0*/;
      numArray9[9] = (byte) 239;
      numArray9[30] = (byte) 148;
      numArray9[12] = (byte) 210;
      numArray9[29] = (byte) 109;
      numArray9[34] = (byte) 87;
      numArray9[48 /*0x30*/] = (byte) 181;
      numArray9[36] = (byte) 185;
      numArray9[40] = (byte) 31 /*0x1F*/;
      numArray9[31 /*0x1F*/] = (byte) 66;
      numArray9[39] = (byte) 106;
      numArray9[3] = (byte) 89;
      numArray9[37] = (byte) 60;
      numArray9[19] = (byte) 12;
      numArray9[43] = (byte) 234;
      numArray9[44] = (byte) 215;
      numArray9[21] = (byte) 166;
      numArray9[0] = (byte) 56;
      numArray9[32 /*0x20*/] = (byte) 84;
      numArray9[4] = (byte) 151;
      numArray9[45] = (byte) 172;
      numArray9[28] = (byte) 44;
      numArray9[51] = (byte) 158;
      numArray9[52] = (byte) 43;
      numArray9[53] = (byte) 185;
      numArray9[54] = (byte) 197;
      key.Query(true, 336, numArray8, numArray8);
      Array.Copy((Array) numArray8, 0, (Array) numArray1, 165, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 165] ^= numArray9[index];
      byte[] numArray10 = new byte[10]
      {
        (byte) 54,
        (byte) 115,
        (byte) 108,
        (byte) 71,
        (byte) 100,
        (byte) 238,
        (byte) 163,
        (byte) 171,
        (byte) 127 /*0x7F*/,
        (byte) 76
      };
      byte[] numArray11 = new byte[10];
      numArray11[8] = (byte) 30;
      numArray11[5] = (byte) 226;
      numArray11[2] = (byte) 159;
      numArray11[3] = (byte) 63 /*0x3F*/;
      numArray11[7] = (byte) 177;
      numArray11[0] = (byte) 234;
      numArray11[1] = (byte) 222;
      numArray11[4] = (byte) 41;
      numArray11[6] = (byte) 229;
      numArray11[9] = (byte) 35;
      key.Query(true, 336, numArray10, numArray10);
      Array.Copy((Array) numArray10, 0, (Array) numArray1, 220, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index + 220] ^= numArray11[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray12 = new byte[230];
    byte[] numArray13 = new byte[55]
    {
      (byte) 49,
      (byte) 220,
      (byte) 75,
      (byte) 136,
      (byte) 175,
      (byte) 201,
      (byte) 106,
      (byte) 47,
      (byte) 148,
      (byte) 74,
      (byte) 54,
      (byte) 103,
      (byte) 14,
      (byte) 46,
      (byte) 166,
      (byte) 54,
      (byte) 130,
      (byte) 228,
      (byte) 137,
      (byte) 132,
      (byte) 219,
      (byte) 213,
      (byte) 203,
      (byte) 206,
      (byte) 94,
      (byte) 38,
      (byte) 212,
      (byte) 216,
      (byte) 159,
      (byte) 193,
      (byte) 62,
      (byte) 177,
      (byte) 162,
      (byte) 15,
      (byte) 58,
      (byte) 106,
      (byte) 25,
      (byte) 101,
      (byte) 243,
      (byte) 226,
      (byte) 21,
      (byte) 29,
      (byte) 14,
      (byte) 184,
      (byte) 240 /*0xF0*/,
      (byte) 96 /*0x60*/,
      (byte) 76,
      (byte) 148,
      (byte) 177,
      (byte) 47,
      (byte) 229,
      (byte) 226,
      (byte) 8,
      (byte) 15,
      (byte) 137
    };
    byte[] numArray14 = new byte[55]
    {
      (byte) 34,
      (byte) 208 /*0xD0*/,
      (byte) 121,
      (byte) 228,
      (byte) 50,
      (byte) 128 /*0x80*/,
      (byte) 220,
      (byte) 253,
      (byte) 65,
      (byte) 37,
      (byte) 238,
      (byte) 143,
      (byte) 134,
      (byte) 166,
      (byte) 235,
      (byte) 79,
      (byte) 19,
      (byte) 93,
      (byte) 56,
      (byte) 90,
      (byte) 85,
      (byte) 0,
      (byte) 70,
      (byte) 209,
      (byte) 116,
      (byte) 168,
      (byte) 34,
      (byte) 14,
      (byte) 30,
      (byte) 152,
      (byte) 7,
      (byte) 150,
      (byte) 97,
      (byte) 146,
      (byte) 14,
      (byte) 47,
      (byte) 16 /*0x10*/,
      (byte) 72,
      (byte) 196,
      (byte) 104,
      (byte) 15,
      (byte) 127 /*0x7F*/,
      (byte) 81,
      (byte) 217,
      (byte) 243,
      (byte) 61,
      (byte) 88,
      (byte) 152,
      (byte) 137,
      (byte) 48 /*0x30*/,
      (byte) 191,
      (byte) 173,
      (byte) 60,
      (byte) 57,
      (byte) 206
    };
    key.Query(true, 336, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray12, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index] ^= numArray14[index];
    byte[] numArray15 = new byte[55]
    {
      (byte) 125,
      (byte) 42,
      (byte) 136,
      (byte) 244,
      (byte) 145,
      (byte) 144 /*0x90*/,
      (byte) 210,
      (byte) 11,
      (byte) 132,
      (byte) 196,
      byte.MaxValue,
      (byte) 244,
      (byte) 59,
      (byte) 28,
      (byte) 29,
      (byte) 240 /*0xF0*/,
      (byte) 167,
      (byte) 110,
      (byte) 189,
      (byte) 199,
      (byte) 18,
      (byte) 221,
      (byte) 253,
      (byte) 217,
      (byte) 64 /*0x40*/,
      (byte) 39,
      (byte) 111,
      (byte) 220,
      (byte) 91,
      (byte) 168,
      (byte) 146,
      (byte) 133,
      (byte) 222,
      (byte) 89,
      (byte) 156,
      (byte) 58,
      (byte) 106,
      (byte) 237,
      (byte) 132,
      (byte) 140,
      (byte) 59,
      (byte) 172,
      (byte) 107,
      (byte) 155,
      (byte) 76,
      (byte) 160 /*0xA0*/,
      (byte) 118,
      (byte) 18,
      (byte) 200,
      (byte) 242,
      (byte) 97,
      (byte) 161,
      (byte) 203,
      (byte) 149,
      (byte) 254
    };
    byte[] numArray16 = new byte[55]
    {
      (byte) 132,
      (byte) 222,
      (byte) 105,
      (byte) 30,
      (byte) 126,
      (byte) 179,
      (byte) 63 /*0x3F*/,
      (byte) 48 /*0x30*/,
      (byte) 240 /*0xF0*/,
      (byte) 104,
      (byte) 65,
      (byte) 89,
      (byte) 68,
      (byte) 212,
      (byte) 21,
      (byte) 251,
      (byte) 58,
      (byte) 127 /*0x7F*/,
      (byte) 7,
      (byte) 75,
      (byte) 90,
      (byte) 205,
      (byte) 39,
      (byte) 206,
      (byte) 237,
      (byte) 216,
      (byte) 53,
      (byte) 234,
      (byte) 107,
      (byte) 75,
      (byte) 9,
      (byte) 92,
      (byte) 251,
      (byte) 55,
      (byte) 161,
      (byte) 72,
      (byte) 23,
      (byte) 155,
      (byte) 131,
      (byte) 6,
      (byte) 136,
      (byte) 65,
      (byte) 115,
      (byte) 162,
      (byte) 46,
      (byte) 174,
      (byte) 108,
      (byte) 41,
      (byte) 35,
      (byte) 70,
      (byte) 156,
      (byte) 198,
      (byte) 46,
      (byte) 250,
      (byte) 69
    };
    key.Query(true, 336, numArray15, numArray15);
    Array.Copy((Array) numArray15, 0, (Array) numArray12, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index + 55] ^= numArray16[index];
    byte[] numArray17 = new byte[55]
    {
      (byte) 175,
      (byte) 180,
      (byte) 106,
      (byte) 32 /*0x20*/,
      (byte) 246,
      (byte) 176 /*0xB0*/,
      (byte) 219,
      (byte) 50,
      (byte) 121,
      (byte) 21,
      (byte) 225,
      (byte) 128 /*0x80*/,
      (byte) 226,
      (byte) 133,
      (byte) 185,
      (byte) 233,
      (byte) 48 /*0x30*/,
      (byte) 139,
      (byte) 101,
      (byte) 114,
      (byte) 97,
      (byte) 140,
      (byte) 160 /*0xA0*/,
      (byte) 176 /*0xB0*/,
      (byte) 93,
      (byte) 19,
      (byte) 105,
      (byte) 71,
      (byte) 241,
      (byte) 186,
      (byte) 72,
      (byte) 91,
      (byte) 253,
      (byte) 13,
      (byte) 187,
      (byte) 57,
      (byte) 25,
      (byte) 108,
      (byte) 69,
      (byte) 252,
      (byte) 143,
      (byte) 105,
      (byte) 211,
      (byte) 138,
      (byte) 148,
      (byte) 145,
      (byte) 229,
      (byte) 108,
      (byte) 168,
      (byte) 183,
      (byte) 154,
      (byte) 10,
      (byte) 152,
      (byte) 45,
      (byte) 203
    };
    byte[] numArray18 = new byte[55]
    {
      (byte) 168,
      (byte) 206,
      byte.MaxValue,
      (byte) 112 /*0x70*/,
      (byte) 41,
      (byte) 42,
      (byte) 85,
      (byte) 111,
      (byte) 30,
      (byte) 239,
      (byte) 174,
      (byte) 130,
      (byte) 106,
      (byte) 6,
      (byte) 14,
      (byte) 0,
      (byte) 66,
      (byte) 161,
      (byte) 204,
      (byte) 153,
      (byte) 56,
      (byte) 99,
      (byte) 108,
      (byte) 28,
      (byte) 243,
      (byte) 34,
      (byte) 61,
      (byte) 239,
      (byte) 76,
      (byte) 14,
      (byte) 103,
      (byte) 61,
      (byte) 216,
      (byte) 51,
      (byte) 173,
      (byte) 27,
      (byte) 94,
      (byte) 11,
      (byte) 124,
      (byte) 217,
      (byte) 22,
      (byte) 185,
      (byte) 108,
      (byte) 214,
      (byte) 8,
      (byte) 22,
      (byte) 23,
      (byte) 18,
      (byte) 26,
      (byte) 205,
      (byte) 157,
      (byte) 8,
      (byte) 38,
      (byte) 95,
      (byte) 24
    };
    key.Query(true, 336, numArray17, numArray17);
    Array.Copy((Array) numArray17, 0, (Array) numArray12, 110, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index + 110] ^= numArray18[index];
    byte[] numArray19 = new byte[55]
    {
      (byte) 185,
      (byte) 194,
      (byte) 226,
      (byte) 70,
      (byte) 148,
      (byte) 65,
      (byte) 191,
      (byte) 205,
      (byte) 134,
      (byte) 130,
      (byte) 213,
      (byte) 201,
      (byte) 127 /*0x7F*/,
      (byte) 207,
      (byte) 196,
      (byte) 69,
      (byte) 105,
      (byte) 226,
      (byte) 247,
      (byte) 185,
      (byte) 21,
      (byte) 27,
      (byte) 203,
      (byte) 46,
      (byte) 253,
      (byte) 159,
      (byte) 57,
      (byte) 31 /*0x1F*/,
      (byte) 62,
      (byte) 194,
      (byte) 207,
      (byte) 65,
      (byte) 153,
      (byte) 37,
      (byte) 96 /*0x60*/,
      (byte) 169,
      (byte) 99,
      (byte) 181,
      (byte) 251,
      (byte) 40,
      (byte) 19,
      (byte) 189,
      (byte) 14,
      (byte) 14,
      (byte) 16 /*0x10*/,
      (byte) 162,
      (byte) 40,
      (byte) 231,
      (byte) 214,
      (byte) 197,
      (byte) 47,
      (byte) 74,
      (byte) 212,
      (byte) 104,
      (byte) 254
    };
    byte[] numArray20 = new byte[55];
    numArray20[18] = (byte) 249;
    numArray20[1] = (byte) 55;
    numArray20[54] = (byte) 43;
    numArray20[44] = (byte) 15;
    numArray20[38] = (byte) 94;
    numArray20[5] = (byte) 95;
    numArray20[37] = (byte) 252;
    numArray20[7] = (byte) 19;
    numArray20[9] = (byte) 83;
    numArray20[10] = (byte) 124;
    numArray20[46] = (byte) 33;
    numArray20[51] = (byte) 172;
    numArray20[12] = (byte) 231;
    numArray20[13] = (byte) 205;
    numArray20[11] = (byte) 227;
    numArray20[15] = (byte) 42;
    numArray20[16 /*0x10*/] = (byte) 121;
    numArray20[17] = (byte) 46;
    numArray20[50] = (byte) 207;
    numArray20[19] = (byte) 165;
    numArray20[20] = (byte) 84;
    numArray20[21] = (byte) 154;
    numArray20[22] = (byte) 20;
    numArray20[23] = (byte) 18;
    numArray20[47] = (byte) 73;
    numArray20[25] = (byte) 240 /*0xF0*/;
    numArray20[26] = (byte) 191;
    numArray20[53] = (byte) 194;
    numArray20[28] = (byte) 62;
    numArray20[40] = (byte) 1;
    numArray20[33] = (byte) 72;
    numArray20[2] = (byte) 157;
    numArray20[32 /*0x20*/] = (byte) 171;
    numArray20[0] = (byte) 215;
    numArray20[34] = (byte) 1;
    numArray20[24] = (byte) 226;
    numArray20[36] = (byte) 160 /*0xA0*/;
    numArray20[52] = (byte) 95;
    numArray20[3] = (byte) 245;
    numArray20[41] = (byte) 102;
    numArray20[35] = (byte) 153;
    numArray20[42] = (byte) 28;
    numArray20[29] = (byte) 81;
    numArray20[43] = (byte) 97;
    numArray20[14] = (byte) 220;
    numArray20[27] = (byte) 24;
    numArray20[8] = (byte) 179;
    numArray20[4] = (byte) 111;
    numArray20[30] = (byte) 42;
    numArray20[49] = (byte) 111;
    numArray20[48 /*0x30*/] = (byte) 236;
    numArray20[39] = (byte) 128 /*0x80*/;
    numArray20[6] = (byte) 216;
    numArray20[45] = (byte) 124;
    numArray20[31 /*0x1F*/] = (byte) 4;
    key.Query(true, 336, numArray19, numArray19);
    Array.Copy((Array) numArray19, 0, (Array) numArray12, 165, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index + 165] ^= numArray20[index];
    byte[] numArray21 = new byte[10]
    {
      (byte) 150,
      (byte) 239,
      (byte) 126,
      (byte) 181,
      (byte) 86,
      (byte) 21,
      (byte) 49,
      (byte) 46,
      (byte) 38,
      (byte) 254
    };
    byte[] numArray22 = new byte[10];
    numArray22[1] = (byte) 250;
    numArray22[9] = (byte) 99;
    numArray22[2] = (byte) 218;
    numArray22[3] = (byte) 168;
    numArray22[4] = (byte) 191;
    numArray22[7] = (byte) 184;
    numArray22[6] = (byte) 155;
    numArray22[0] = (byte) 154;
    numArray22[8] = (byte) 181;
    numArray22[5] = (byte) 156;
    key.Query(true, 336, numArray21, numArray21);
    Array.Copy((Array) numArray21, 0, (Array) numArray12, 220, 10);
    for (int index = 0; index < 10; ++index)
      numArray12[index + 220] ^= numArray22[index];
    byte[] numArray23 = new byte[41];
    byte[] response = new byte[41];
    Array.Copy((Array) sc_552.sspq, 0, (Array) numArray23, 0, 41);
    key.Query(true, 336, numArray23, response);
    Array.Copy((Array) sc_552.sspr, 0, (Array) numArray23, 0, 41);
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

  internal static string ssp_archives_554()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[205];
      byte[] numArray2 = new byte[55];
      numArray2[19] = (byte) 221;
      numArray2[52] = (byte) 170;
      numArray2[2] = (byte) 25;
      numArray2[48 /*0x30*/] = (byte) 143;
      numArray2[14] = (byte) 57;
      numArray2[43] = (byte) 165;
      numArray2[30] = (byte) 32 /*0x20*/;
      numArray2[6] = (byte) 19;
      numArray2[8] = (byte) 169;
      numArray2[54] = (byte) 172;
      numArray2[15] = (byte) 229;
      numArray2[11] = (byte) 116;
      numArray2[50] = (byte) 55;
      numArray2[1] = (byte) 238;
      numArray2[45] = (byte) 147;
      numArray2[0] = (byte) 125;
      numArray2[37] = (byte) 105;
      numArray2[17] = (byte) 158;
      numArray2[47] = (byte) 202;
      numArray2[39] = (byte) 18;
      numArray2[20] = (byte) 5;
      numArray2[21] = (byte) 98;
      numArray2[46] = (byte) 199;
      numArray2[12] = (byte) 13;
      numArray2[24] = (byte) 117;
      numArray2[25] = (byte) 178;
      numArray2[26] = (byte) 144 /*0x90*/;
      numArray2[27] = (byte) 84;
      numArray2[28] = (byte) 154;
      numArray2[29] = byte.MaxValue;
      numArray2[3] = (byte) 202;
      numArray2[31 /*0x1F*/] = (byte) 152;
      numArray2[23] = (byte) 35;
      numArray2[33] = (byte) 232;
      numArray2[34] = (byte) 76;
      numArray2[5] = (byte) 67;
      numArray2[36] = (byte) 151;
      numArray2[10] = (byte) 74;
      numArray2[38] = (byte) 41;
      numArray2[32 /*0x20*/] = (byte) 227;
      numArray2[40] = (byte) 125;
      numArray2[41] = (byte) 107;
      numArray2[42] = (byte) 37;
      numArray2[49] = (byte) 153;
      numArray2[44] = (byte) 170;
      numArray2[4] = (byte) 89;
      numArray2[22] = (byte) 160 /*0xA0*/;
      numArray2[13] = (byte) 16 /*0x10*/;
      numArray2[7] = (byte) 16 /*0x10*/;
      numArray2[35] = (byte) 101;
      numArray2[18] = (byte) 18;
      numArray2[51] = (byte) 110;
      numArray2[9] = (byte) 51;
      numArray2[53] = (byte) 178;
      numArray2[16 /*0x10*/] = (byte) 226;
      byte[] numArray3 = new byte[55];
      numArray3[49] = (byte) 252;
      numArray3[1] = (byte) 141;
      numArray3[2] = (byte) 105;
      numArray3[15] = (byte) 167;
      numArray3[6] = (byte) 113;
      numArray3[5] = (byte) 40;
      numArray3[31 /*0x1F*/] = (byte) 136;
      numArray3[35] = (byte) 35;
      numArray3[8] = (byte) 53;
      numArray3[29] = (byte) 213;
      numArray3[10] = (byte) 164;
      numArray3[11] = (byte) 193;
      numArray3[45] = (byte) 221;
      numArray3[34] = (byte) 84;
      numArray3[37] = (byte) 160 /*0xA0*/;
      numArray3[40] = (byte) 216;
      numArray3[0] = (byte) 179;
      numArray3[17] = (byte) 177;
      numArray3[18] = (byte) 162;
      numArray3[20] = (byte) 211;
      numArray3[7] = (byte) 152;
      numArray3[21] = (byte) 139;
      numArray3[14] = (byte) 63 /*0x3F*/;
      numArray3[16 /*0x10*/] = (byte) 111;
      numArray3[47] = (byte) 165;
      numArray3[25] = (byte) 203;
      numArray3[26] = (byte) 223;
      numArray3[32 /*0x20*/] = (byte) 176 /*0xB0*/;
      numArray3[28] = (byte) 39;
      numArray3[13] = (byte) 74;
      numArray3[30] = (byte) 206;
      numArray3[12] = (byte) 9;
      numArray3[27] = (byte) 21;
      numArray3[54] = (byte) 11;
      numArray3[19] = (byte) 152;
      numArray3[3] = (byte) 186;
      numArray3[23] = (byte) 90;
      numArray3[52] = (byte) 105;
      numArray3[38] = (byte) 154;
      numArray3[33] = (byte) 50;
      numArray3[51] = (byte) 127 /*0x7F*/;
      numArray3[50] = (byte) 31 /*0x1F*/;
      numArray3[42] = (byte) 108;
      numArray3[43] = (byte) 11;
      numArray3[44] = (byte) 0;
      numArray3[41] = (byte) 101;
      numArray3[46] = (byte) 212;
      numArray3[24] = (byte) 38;
      numArray3[48 /*0x30*/] = (byte) 149;
      numArray3[22] = (byte) 107;
      numArray3[4] = (byte) 54;
      numArray3[36] = (byte) 92;
      numArray3[39] = (byte) 213;
      numArray3[9] = (byte) 203;
      numArray3[53] = (byte) 2;
      key.Query(true, 336, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55];
      numArray4[52] = (byte) 72;
      numArray4[39] = (byte) 194;
      numArray4[2] = (byte) 115;
      numArray4[3] = (byte) 47;
      numArray4[41] = (byte) 139;
      numArray4[4] = (byte) 240 /*0xF0*/;
      numArray4[32 /*0x20*/] = (byte) 55;
      numArray4[28] = (byte) 175;
      numArray4[8] = (byte) 127 /*0x7F*/;
      numArray4[9] = (byte) 129;
      numArray4[20] = (byte) 52;
      numArray4[11] = (byte) 62;
      numArray4[33] = (byte) 215;
      numArray4[0] = (byte) 253;
      numArray4[24] = (byte) 73;
      numArray4[7] = (byte) 167;
      numArray4[16 /*0x10*/] = (byte) 46;
      numArray4[17] = (byte) 221;
      numArray4[18] = (byte) 36;
      numArray4[30] = (byte) 43;
      numArray4[45] = (byte) 53;
      numArray4[13] = (byte) 137;
      numArray4[53] = (byte) 243;
      numArray4[23] = (byte) 143;
      numArray4[10] = (byte) 191;
      numArray4[38] = (byte) 211;
      numArray4[36] = (byte) 84;
      numArray4[27] = (byte) 172;
      numArray4[14] = (byte) 195;
      numArray4[47] = (byte) 20;
      numArray4[34] = (byte) 38;
      numArray4[29] = (byte) 58;
      numArray4[5] = (byte) 190;
      numArray4[43] = (byte) 33;
      numArray4[12] = (byte) 250;
      numArray4[35] = (byte) 12;
      numArray4[19] = (byte) 41;
      numArray4[37] = (byte) 66;
      numArray4[26] = (byte) 23;
      numArray4[1] = (byte) 148;
      numArray4[40] = (byte) 188;
      numArray4[48 /*0x30*/] = (byte) 151;
      numArray4[22] = (byte) 59;
      numArray4[44] = (byte) 249;
      numArray4[15] = (byte) 41;
      numArray4[6] = (byte) 219;
      numArray4[46] = (byte) 3;
      numArray4[25] = (byte) 11;
      numArray4[42] = (byte) 181;
      numArray4[31 /*0x1F*/] = (byte) 189;
      numArray4[50] = (byte) 219;
      numArray4[51] = (byte) 212;
      numArray4[49] = (byte) 222;
      numArray4[21] = (byte) 75;
      numArray4[54] = (byte) 183;
      byte[] numArray5 = new byte[55]
      {
        (byte) 190,
        (byte) 82,
        (byte) 43,
        (byte) 45,
        (byte) 82,
        (byte) 217,
        (byte) 189,
        (byte) 24,
        (byte) 195,
        (byte) 29,
        (byte) 209,
        (byte) 210,
        (byte) 123,
        (byte) 79,
        (byte) 84,
        (byte) 82,
        (byte) 217,
        (byte) 116,
        (byte) 215,
        (byte) 117,
        (byte) 164,
        (byte) 27,
        (byte) 79,
        (byte) 150,
        (byte) 147,
        (byte) 116,
        (byte) 68,
        (byte) 164,
        (byte) 42,
        (byte) 43,
        (byte) 101,
        (byte) 110,
        (byte) 155,
        (byte) 148,
        (byte) 199,
        (byte) 190,
        (byte) 11,
        (byte) 70,
        (byte) 62,
        (byte) 106,
        (byte) 138,
        (byte) 78,
        (byte) 108,
        (byte) 186,
        (byte) 62,
        (byte) 234,
        (byte) 114,
        (byte) 229,
        (byte) 219,
        (byte) 168,
        (byte) 141,
        (byte) 45,
        (byte) 11,
        (byte) 151,
        (byte) 167
      };
      key.Query(true, 336, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[55]
      {
        (byte) 77,
        (byte) 209,
        (byte) 221,
        (byte) 163,
        (byte) 25,
        (byte) 39,
        (byte) 230,
        (byte) 254,
        (byte) 140,
        (byte) 195,
        (byte) 2,
        (byte) 238,
        (byte) 3,
        (byte) 100,
        (byte) 143,
        (byte) 27,
        (byte) 71,
        (byte) 221,
        (byte) 22,
        (byte) 37,
        (byte) 78,
        (byte) 157,
        (byte) 229,
        (byte) 38,
        (byte) 81,
        (byte) 52,
        (byte) 199,
        (byte) 111,
        (byte) 140,
        (byte) 143,
        (byte) 189,
        (byte) 18,
        (byte) 88,
        (byte) 116,
        (byte) 34,
        (byte) 139,
        (byte) 17,
        (byte) 119,
        (byte) 117,
        (byte) 107,
        (byte) 137,
        (byte) 205,
        (byte) 108,
        (byte) 107,
        (byte) 222,
        (byte) 195,
        (byte) 220,
        (byte) 164,
        (byte) 96 /*0x60*/,
        (byte) 175,
        (byte) 158,
        (byte) 168,
        (byte) 246,
        (byte) 246,
        (byte) 241
      };
      byte[] numArray7 = new byte[55]
      {
        (byte) 61,
        (byte) 55,
        (byte) 134,
        (byte) 235,
        (byte) 75,
        (byte) 23,
        (byte) 91,
        (byte) 180,
        (byte) 185,
        (byte) 123,
        (byte) 12,
        (byte) 80 /*0x50*/,
        (byte) 15,
        (byte) 157,
        (byte) 87,
        (byte) 126,
        (byte) 195,
        (byte) 204,
        (byte) 23,
        (byte) 201,
        (byte) 193,
        (byte) 135,
        (byte) 24,
        (byte) 158,
        (byte) 238,
        (byte) 26,
        (byte) 15,
        (byte) 156,
        (byte) 68,
        (byte) 208 /*0xD0*/,
        (byte) 173,
        (byte) 33,
        (byte) 249,
        (byte) 59,
        (byte) 113,
        (byte) 250,
        (byte) 24,
        (byte) 18,
        (byte) 145,
        (byte) 3,
        (byte) 147,
        (byte) 1,
        (byte) 232,
        (byte) 177,
        (byte) 109,
        (byte) 147,
        (byte) 193,
        (byte) 6,
        (byte) 253,
        (byte) 74,
        (byte) 217,
        (byte) 130,
        (byte) 129,
        (byte) 245,
        (byte) 248
      };
      key.Query(true, 336, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[40];
      numArray8[16 /*0x10*/] = (byte) 171;
      numArray8[29] = (byte) 28;
      numArray8[2] = (byte) 228;
      numArray8[7] = (byte) 5;
      numArray8[25] = (byte) 15;
      numArray8[12] = (byte) 4;
      numArray8[6] = (byte) 252;
      numArray8[28] = (byte) 20;
      numArray8[19] = (byte) 49;
      numArray8[30] = (byte) 98;
      numArray8[22] = (byte) 142;
      numArray8[39] = (byte) 91;
      numArray8[1] = (byte) 82;
      numArray8[27] = (byte) 241;
      numArray8[26] = (byte) 221;
      numArray8[15] = (byte) 104;
      numArray8[10] = (byte) 143;
      numArray8[17] = (byte) 156;
      numArray8[18] = (byte) 128 /*0x80*/;
      numArray8[23] = (byte) 88;
      numArray8[3] = (byte) 95;
      numArray8[5] = (byte) 113;
      numArray8[9] = (byte) 149;
      numArray8[4] = (byte) 119;
      numArray8[24] = (byte) 35;
      numArray8[11] = (byte) 65;
      numArray8[37] = (byte) 11;
      numArray8[32 /*0x20*/] = (byte) 114;
      numArray8[8] = (byte) 239;
      numArray8[34] = (byte) 91;
      numArray8[20] = (byte) 122;
      numArray8[31 /*0x1F*/] = (byte) 130;
      numArray8[13] = (byte) 9;
      numArray8[33] = (byte) 158;
      numArray8[14] = (byte) 207;
      numArray8[35] = (byte) 37;
      numArray8[36] = (byte) 144 /*0x90*/;
      numArray8[21] = (byte) 72;
      numArray8[38] = (byte) 18;
      numArray8[0] = (byte) 182;
      byte[] numArray9 = new byte[40];
      numArray9[37] = (byte) 38;
      numArray9[1] = (byte) 91;
      numArray9[2] = (byte) 242;
      numArray9[3] = (byte) 104;
      numArray9[24] = (byte) 251;
      numArray9[5] = (byte) 88;
      numArray9[6] = (byte) 223;
      numArray9[19] = (byte) 227;
      numArray9[8] = (byte) 92;
      numArray9[26] = (byte) 179;
      numArray9[20] = (byte) 191;
      numArray9[11] = (byte) 141;
      numArray9[35] = (byte) 114;
      numArray9[13] = (byte) 234;
      numArray9[38] = (byte) 12;
      numArray9[15] = (byte) 238;
      numArray9[29] = (byte) 112 /*0x70*/;
      numArray9[7] = (byte) 170;
      numArray9[10] = (byte) 89;
      numArray9[18] = (byte) 52;
      numArray9[12] = (byte) 165;
      numArray9[21] = (byte) 177;
      numArray9[9] = (byte) 154;
      numArray9[4] = (byte) 14;
      numArray9[0] = (byte) 89;
      numArray9[25] = (byte) 223;
      numArray9[34] = (byte) 216;
      numArray9[23] = (byte) 95;
      numArray9[16 /*0x10*/] = (byte) 131;
      numArray9[22] = (byte) 175;
      numArray9[28] = (byte) 184;
      numArray9[31 /*0x1F*/] = (byte) 136;
      numArray9[32 /*0x20*/] = (byte) 20;
      numArray9[33] = (byte) 189;
      numArray9[30] = (byte) 250;
      numArray9[17] = (byte) 0;
      numArray9[36] = (byte) 86;
      numArray9[14] = (byte) 20;
      numArray9[27] = (byte) 181;
      numArray9[39] = (byte) 87;
      key.Query(true, 336, numArray8, numArray8);
      Array.Copy((Array) numArray8, 0, (Array) numArray1, 165, 40);
      for (int index = 0; index < 40; ++index)
        numArray1[index + 165] ^= numArray9[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray10 = new byte[205];
    byte[] numArray11 = new byte[55];
    numArray11[21] = (byte) 215;
    numArray11[1] = (byte) 72;
    numArray11[2] = (byte) 211;
    numArray11[33] = (byte) 189;
    numArray11[6] = (byte) 92;
    numArray11[4] = (byte) 223;
    numArray11[32 /*0x20*/] = (byte) 183;
    numArray11[7] = (byte) 160 /*0xA0*/;
    numArray11[30] = (byte) 207;
    numArray11[19] = (byte) 54;
    numArray11[37] = (byte) 218;
    numArray11[5] = (byte) 93;
    numArray11[11] = (byte) 45;
    numArray11[50] = (byte) 221;
    numArray11[14] = byte.MaxValue;
    numArray11[15] = (byte) 108;
    numArray11[16 /*0x10*/] = (byte) 37;
    numArray11[9] = (byte) 103;
    numArray11[0] = (byte) 194;
    numArray11[31 /*0x1F*/] = (byte) 70;
    numArray11[17] = (byte) 57;
    numArray11[38] = (byte) 240 /*0xF0*/;
    numArray11[12] = (byte) 189;
    numArray11[25] = (byte) 247;
    numArray11[24] = (byte) 83;
    numArray11[8] = (byte) 62;
    numArray11[26] = (byte) 106;
    numArray11[27] = (byte) 126;
    numArray11[28] = (byte) 87;
    numArray11[29] = (byte) 24;
    numArray11[44] = (byte) 182;
    numArray11[45] = (byte) 78;
    numArray11[39] = (byte) 237;
    numArray11[42] = (byte) 215;
    numArray11[20] = (byte) 207;
    numArray11[34] = (byte) 52;
    numArray11[36] = (byte) 149;
    numArray11[13] = (byte) 64 /*0x40*/;
    numArray11[35] = (byte) 218;
    numArray11[23] = (byte) 131;
    numArray11[40] = (byte) 97;
    numArray11[41] = (byte) 188;
    numArray11[52] = (byte) 50;
    numArray11[43] = (byte) 251;
    numArray11[47] = (byte) 27;
    numArray11[18] = (byte) 199;
    numArray11[46] = (byte) 246;
    numArray11[22] = byte.MaxValue;
    numArray11[48 /*0x30*/] = (byte) 230;
    numArray11[49] = (byte) 176 /*0xB0*/;
    numArray11[3] = (byte) 208 /*0xD0*/;
    numArray11[51] = (byte) 241;
    numArray11[10] = (byte) 202;
    numArray11[53] = (byte) 165;
    numArray11[54] = (byte) 105;
    byte[] numArray12 = new byte[55];
    numArray12[3] = (byte) 69;
    numArray12[14] = (byte) 41;
    numArray12[22] = (byte) 104;
    numArray12[38] = (byte) 127 /*0x7F*/;
    numArray12[20] = (byte) 15;
    numArray12[0] = (byte) 49;
    numArray12[23] = (byte) 86;
    numArray12[53] = (byte) 240 /*0xF0*/;
    numArray12[7] = (byte) 47;
    numArray12[9] = (byte) 238;
    numArray12[10] = (byte) 248;
    numArray12[11] = byte.MaxValue;
    numArray12[12] = (byte) 160 /*0xA0*/;
    numArray12[13] = (byte) 233;
    numArray12[32 /*0x20*/] = (byte) 197;
    numArray12[1] = (byte) 95;
    numArray12[16 /*0x10*/] = (byte) 86;
    numArray12[17] = (byte) 238;
    numArray12[18] = (byte) 32 /*0x20*/;
    numArray12[8] = (byte) 117;
    numArray12[39] = (byte) 194;
    numArray12[21] = (byte) 125;
    numArray12[34] = (byte) 100;
    numArray12[4] = (byte) 0;
    numArray12[6] = (byte) 180;
    numArray12[25] = (byte) 40;
    numArray12[2] = (byte) 225;
    numArray12[27] = (byte) 18;
    numArray12[15] = (byte) 30;
    numArray12[29] = (byte) 204;
    numArray12[30] = (byte) 14;
    numArray12[31 /*0x1F*/] = (byte) 36;
    numArray12[37] = (byte) 205;
    numArray12[33] = (byte) 239;
    numArray12[51] = (byte) 196;
    numArray12[35] = (byte) 150;
    numArray12[42] = (byte) 214;
    numArray12[19] = (byte) 72;
    numArray12[41] = (byte) 139;
    numArray12[28] = (byte) 158;
    numArray12[40] = (byte) 133;
    numArray12[26] = (byte) 252;
    numArray12[52] = (byte) 209;
    numArray12[43] = (byte) 197;
    numArray12[44] = (byte) 117;
    numArray12[36] = (byte) 113;
    numArray12[46] = (byte) 158;
    numArray12[47] = (byte) 243;
    numArray12[50] = (byte) 98;
    numArray12[5] = (byte) 238;
    numArray12[45] = (byte) 1;
    numArray12[48 /*0x30*/] = (byte) 11;
    numArray12[49] = (byte) 188;
    numArray12[24] = (byte) 97;
    numArray12[54] = (byte) 156;
    key.Query(true, 336, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray10, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index] ^= numArray12[index];
    byte[] numArray13 = new byte[55];
    numArray13[37] = (byte) 242;
    numArray13[1] = (byte) 76;
    numArray13[41] = (byte) 40;
    numArray13[52] = (byte) 235;
    numArray13[49] = (byte) 72;
    numArray13[10] = (byte) 174;
    numArray13[46] = (byte) 229;
    numArray13[7] = (byte) 130;
    numArray13[8] = (byte) 178;
    numArray13[0] = (byte) 231;
    numArray13[53] = (byte) 17;
    numArray13[19] = (byte) 137;
    numArray13[27] = (byte) 0;
    numArray13[33] = (byte) 166;
    numArray13[16 /*0x10*/] = (byte) 138;
    numArray13[5] = (byte) 212;
    numArray13[39] = (byte) 253;
    numArray13[9] = (byte) 34;
    numArray13[18] = (byte) 205;
    numArray13[50] = (byte) 236;
    numArray13[6] = (byte) 68;
    numArray13[21] = (byte) 215;
    numArray13[29] = (byte) 69;
    numArray13[23] = (byte) 187;
    numArray13[24] = (byte) 242;
    numArray13[25] = (byte) 168;
    numArray13[26] = (byte) 134;
    numArray13[36] = (byte) 64 /*0x40*/;
    numArray13[28] = (byte) 206;
    numArray13[32 /*0x20*/] = (byte) 196;
    numArray13[2] = (byte) 223;
    numArray13[31 /*0x1F*/] = (byte) 67;
    numArray13[14] = (byte) 8;
    numArray13[34] = (byte) 220;
    numArray13[30] = (byte) 192 /*0xC0*/;
    numArray13[35] = (byte) 151;
    numArray13[43] = (byte) 82;
    numArray13[22] = (byte) 183;
    numArray13[38] = (byte) 208 /*0xD0*/;
    numArray13[42] = (byte) 16 /*0x10*/;
    numArray13[20] = (byte) 244;
    numArray13[11] = (byte) 98;
    numArray13[17] = (byte) 17;
    numArray13[40] = (byte) 128 /*0x80*/;
    numArray13[3] = (byte) 100;
    numArray13[45] = (byte) 235;
    numArray13[12] = (byte) 72;
    numArray13[47] = (byte) 121;
    numArray13[48 /*0x30*/] = (byte) 157;
    numArray13[15] = (byte) 31 /*0x1F*/;
    numArray13[4] = (byte) 110;
    numArray13[51] = (byte) 111;
    numArray13[44] = (byte) 158;
    numArray13[13] = (byte) 205;
    numArray13[54] = (byte) 176 /*0xB0*/;
    byte[] numArray14 = new byte[55]
    {
      (byte) 153,
      (byte) 184,
      (byte) 9,
      (byte) 217,
      (byte) 19,
      (byte) 36,
      (byte) 27,
      (byte) 134,
      (byte) 229,
      (byte) 142,
      (byte) 234,
      (byte) 244,
      (byte) 68,
      (byte) 132,
      (byte) 170,
      (byte) 52,
      (byte) 229,
      (byte) 85,
      (byte) 173,
      (byte) 18,
      (byte) 211,
      byte.MaxValue,
      (byte) 166,
      (byte) 88,
      (byte) 120,
      (byte) 172,
      (byte) 71,
      (byte) 209,
      (byte) 215,
      (byte) 102,
      (byte) 139,
      (byte) 152,
      (byte) 17,
      (byte) 77,
      (byte) 232,
      (byte) 65,
      (byte) 15,
      (byte) 45,
      (byte) 193,
      (byte) 35,
      (byte) 32 /*0x20*/,
      (byte) 84,
      (byte) 187,
      (byte) 64 /*0x40*/,
      (byte) 83,
      (byte) 175,
      (byte) 203,
      (byte) 110,
      (byte) 70,
      (byte) 200,
      (byte) 243,
      (byte) 133,
      byte.MaxValue,
      (byte) 83,
      (byte) 215
    };
    key.Query(true, 336, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray10, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index + 55] ^= numArray14[index];
    byte[] numArray15 = new byte[55]
    {
      (byte) 5,
      (byte) 107,
      (byte) 2,
      (byte) 65,
      (byte) 225,
      (byte) 46,
      (byte) 20,
      (byte) 15,
      (byte) 19,
      (byte) 234,
      (byte) 172,
      (byte) 37,
      (byte) 111,
      (byte) 193,
      (byte) 24,
      (byte) 54,
      (byte) 32 /*0x20*/,
      (byte) 204,
      (byte) 88,
      (byte) 36,
      (byte) 115,
      (byte) 234,
      (byte) 184,
      (byte) 128 /*0x80*/,
      (byte) 131,
      (byte) 54,
      (byte) 28,
      (byte) 4,
      (byte) 35,
      (byte) 59,
      (byte) 171,
      (byte) 17,
      (byte) 131,
      (byte) 208 /*0xD0*/,
      (byte) 23,
      (byte) 20,
      (byte) 8,
      (byte) 109,
      (byte) 90,
      (byte) 69,
      (byte) 142,
      (byte) 225,
      (byte) 31 /*0x1F*/,
      (byte) 99,
      (byte) 150,
      (byte) 99,
      (byte) 188,
      (byte) 167,
      (byte) 59,
      (byte) 248,
      (byte) 251,
      (byte) 59,
      (byte) 139,
      (byte) 36,
      (byte) 189
    };
    byte[] numArray16 = new byte[55]
    {
      (byte) 216,
      (byte) 39,
      (byte) 208 /*0xD0*/,
      (byte) 133,
      (byte) 166,
      (byte) 191,
      (byte) 196,
      (byte) 76,
      (byte) 68,
      (byte) 207,
      (byte) 45,
      (byte) 112 /*0x70*/,
      (byte) 34,
      (byte) 64 /*0x40*/,
      (byte) 54,
      (byte) 121,
      (byte) 42,
      (byte) 111,
      (byte) 144 /*0x90*/,
      (byte) 33,
      (byte) 155,
      (byte) 206,
      (byte) 92,
      (byte) 62,
      (byte) 190,
      (byte) 221,
      (byte) 220,
      (byte) 28,
      (byte) 50,
      (byte) 18,
      (byte) 85,
      (byte) 107,
      (byte) 53,
      (byte) 167,
      (byte) 26,
      (byte) 63 /*0x3F*/,
      (byte) 200,
      (byte) 66,
      (byte) 60,
      (byte) 123,
      (byte) 4,
      (byte) 234,
      (byte) 242,
      (byte) 24,
      (byte) 32 /*0x20*/,
      (byte) 32 /*0x20*/,
      (byte) 155,
      (byte) 108,
      (byte) 48 /*0x30*/,
      (byte) 112 /*0x70*/,
      (byte) 217,
      (byte) 114,
      (byte) 172,
      (byte) 58,
      (byte) 96 /*0x60*/
    };
    key.Query(true, 336, numArray15, numArray15);
    Array.Copy((Array) numArray15, 0, (Array) numArray10, 110, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index + 110] ^= numArray16[index];
    byte[] numArray17 = new byte[40]
    {
      (byte) 129,
      (byte) 189,
      (byte) 171,
      (byte) 18,
      (byte) 15,
      (byte) 236,
      (byte) 215,
      (byte) 63 /*0x3F*/,
      (byte) 155,
      (byte) 161,
      (byte) 158,
      (byte) 170,
      (byte) 3,
      (byte) 45,
      (byte) 179,
      (byte) 114,
      (byte) 19,
      (byte) 149,
      (byte) 146,
      (byte) 202,
      (byte) 85,
      (byte) 234,
      (byte) 32 /*0x20*/,
      (byte) 121,
      (byte) 117,
      (byte) 201,
      (byte) 43,
      (byte) 64 /*0x40*/,
      (byte) 112 /*0x70*/,
      (byte) 133,
      (byte) 95,
      (byte) 19,
      (byte) 252,
      (byte) 98,
      (byte) 134,
      (byte) 175,
      (byte) 175,
      (byte) 51,
      (byte) 127 /*0x7F*/,
      (byte) 244
    };
    byte[] numArray18 = new byte[40]
    {
      (byte) 8,
      (byte) 211,
      (byte) 93,
      (byte) 113,
      (byte) 48 /*0x30*/,
      (byte) 180,
      (byte) 208 /*0xD0*/,
      (byte) 125,
      (byte) 181,
      (byte) 96 /*0x60*/,
      (byte) 110,
      (byte) 48 /*0x30*/,
      (byte) 36,
      (byte) 124,
      (byte) 14,
      (byte) 23,
      (byte) 228,
      (byte) 173,
      (byte) 224 /*0xE0*/,
      (byte) 179,
      (byte) 17,
      (byte) 193,
      (byte) 159,
      (byte) 134,
      (byte) 146,
      (byte) 159,
      (byte) 73,
      (byte) 172,
      (byte) 138,
      (byte) 122,
      (byte) 241,
      (byte) 231,
      (byte) 42,
      (byte) 181,
      (byte) 186,
      (byte) 83,
      (byte) 253,
      (byte) 49,
      (byte) 72,
      (byte) 30
    };
    key.Query(true, 336, numArray17, numArray17);
    Array.Copy((Array) numArray17, 0, (Array) numArray10, 165, 40);
    for (int index = 0; index < 40; ++index)
      numArray10[index + 165] ^= numArray18[index];
    return Encoding.UTF8.GetString(numArray10);
  }

  internal static string ssp_archives_555()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[137];
      byte[] numArray2 = new byte[55]
      {
        (byte) 30,
        (byte) 253,
        (byte) 56,
        (byte) 202,
        (byte) 71,
        (byte) 200,
        (byte) 119,
        (byte) 142,
        (byte) 42,
        (byte) 80 /*0x50*/,
        (byte) 93,
        (byte) 151,
        (byte) 157,
        (byte) 49,
        (byte) 156,
        (byte) 70,
        (byte) 192 /*0xC0*/,
        (byte) 150,
        (byte) 206,
        (byte) 21,
        (byte) 66,
        (byte) 139,
        (byte) 173,
        (byte) 160 /*0xA0*/,
        (byte) 166,
        (byte) 3,
        (byte) 194,
        (byte) 224 /*0xE0*/,
        (byte) 22,
        (byte) 157,
        (byte) 130,
        (byte) 48 /*0x30*/,
        (byte) 239,
        (byte) 95,
        (byte) 91,
        (byte) 119,
        (byte) 144 /*0x90*/,
        (byte) 96 /*0x60*/,
        (byte) 248,
        (byte) 167,
        (byte) 14,
        (byte) 208 /*0xD0*/,
        (byte) 232,
        (byte) 172,
        (byte) 5,
        (byte) 238,
        (byte) 18,
        (byte) 126,
        (byte) 150,
        (byte) 57,
        (byte) 51,
        (byte) 177,
        (byte) 85,
        (byte) 228,
        (byte) 126
      };
      byte[] numArray3 = new byte[55];
      numArray3[26] = (byte) 228;
      numArray3[1] = (byte) 37;
      numArray3[28] = (byte) 72;
      numArray3[6] = (byte) 13;
      numArray3[2] = (byte) 216;
      numArray3[5] = (byte) 205;
      numArray3[11] = (byte) 24;
      numArray3[7] = (byte) 112 /*0x70*/;
      numArray3[8] = (byte) 67;
      numArray3[4] = (byte) 114;
      numArray3[40] = (byte) 178;
      numArray3[14] = (byte) 254;
      numArray3[12] = (byte) 226;
      numArray3[54] = (byte) 130;
      numArray3[42] = (byte) 249;
      numArray3[15] = (byte) 127 /*0x7F*/;
      numArray3[16 /*0x10*/] = (byte) 165;
      numArray3[36] = (byte) 220;
      numArray3[3] = (byte) 126;
      numArray3[19] = (byte) 75;
      numArray3[32 /*0x20*/] = (byte) 170;
      numArray3[27] = (byte) 230;
      numArray3[22] = (byte) 79;
      numArray3[53] = (byte) 175;
      numArray3[51] = (byte) 209;
      numArray3[25] = (byte) 16 /*0x10*/;
      numArray3[33] = (byte) 207;
      numArray3[48 /*0x30*/] = (byte) 190;
      numArray3[49] = (byte) 165;
      numArray3[20] = (byte) 77;
      numArray3[30] = (byte) 231;
      numArray3[31 /*0x1F*/] = (byte) 208 /*0xD0*/;
      numArray3[38] = (byte) 39;
      numArray3[46] = (byte) 228;
      numArray3[13] = (byte) 140;
      numArray3[35] = (byte) 11;
      numArray3[10] = (byte) 114;
      numArray3[45] = (byte) 129;
      numArray3[24] = (byte) 39;
      numArray3[39] = (byte) 176 /*0xB0*/;
      numArray3[44] = (byte) 95;
      numArray3[41] = (byte) 242;
      numArray3[23] = (byte) 215;
      numArray3[43] = (byte) 74;
      numArray3[18] = (byte) 180;
      numArray3[34] = (byte) 163;
      numArray3[52] = (byte) 158;
      numArray3[47] = (byte) 129;
      numArray3[37] = (byte) 41;
      numArray3[9] = (byte) 108;
      numArray3[17] = (byte) 220;
      numArray3[0] = (byte) 169;
      numArray3[29] = (byte) 112 /*0x70*/;
      numArray3[21] = (byte) 79;
      numArray3[50] = (byte) 60;
      key.Query(true, 336, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55];
      numArray4[46] = (byte) 115;
      numArray4[19] = (byte) 72;
      numArray4[2] = (byte) 82;
      numArray4[3] = (byte) 194;
      numArray4[32 /*0x20*/] = (byte) 218;
      numArray4[5] = (byte) 155;
      numArray4[6] = (byte) 35;
      numArray4[53] = (byte) 166;
      numArray4[28] = (byte) 189;
      numArray4[0] = (byte) 252;
      numArray4[31 /*0x1F*/] = (byte) 212;
      numArray4[11] = (byte) 12;
      numArray4[1] = (byte) 107;
      numArray4[13] = (byte) 189;
      numArray4[14] = (byte) 35;
      numArray4[38] = (byte) 42;
      numArray4[10] = (byte) 92;
      numArray4[17] = (byte) 144 /*0x90*/;
      numArray4[35] = (byte) 209;
      numArray4[15] = (byte) 232;
      numArray4[16 /*0x10*/] = (byte) 135;
      numArray4[27] = (byte) 150;
      numArray4[22] = (byte) 113;
      numArray4[23] = (byte) 28;
      numArray4[41] = (byte) 226;
      numArray4[25] = (byte) 217;
      numArray4[24] = (byte) 155;
      numArray4[8] = (byte) 80 /*0x50*/;
      numArray4[18] = (byte) 25;
      numArray4[29] = (byte) 173;
      numArray4[7] = (byte) 252;
      numArray4[26] = (byte) 89;
      numArray4[36] = (byte) 98;
      numArray4[12] = (byte) 253;
      numArray4[34] = (byte) 251;
      numArray4[4] = (byte) 76;
      numArray4[44] = (byte) 120;
      numArray4[37] = (byte) 221;
      numArray4[20] = (byte) 34;
      numArray4[51] = (byte) 76;
      numArray4[40] = (byte) 55;
      numArray4[30] = (byte) 45;
      numArray4[42] = (byte) 136;
      numArray4[43] = (byte) 169;
      numArray4[47] = (byte) 198;
      numArray4[49] = (byte) 42;
      numArray4[39] = (byte) 188;
      numArray4[9] = (byte) 157;
      numArray4[48 /*0x30*/] = (byte) 176 /*0xB0*/;
      numArray4[21] = (byte) 193;
      numArray4[50] = (byte) 166;
      numArray4[33] = (byte) 217;
      numArray4[52] = (byte) 242;
      numArray4[45] = (byte) 155;
      numArray4[54] = (byte) 43;
      byte[] numArray5 = new byte[55]
      {
        (byte) 121,
        (byte) 79,
        (byte) 77,
        (byte) 60,
        (byte) 206,
        (byte) 74,
        (byte) 38,
        (byte) 110,
        (byte) 78,
        (byte) 112 /*0x70*/,
        (byte) 38,
        (byte) 83,
        (byte) 55,
        (byte) 72,
        (byte) 81,
        (byte) 97,
        (byte) 105,
        (byte) 140,
        (byte) 48 /*0x30*/,
        (byte) 231,
        (byte) 47,
        (byte) 174,
        (byte) 164,
        (byte) 152,
        (byte) 125,
        (byte) 162,
        (byte) 10,
        (byte) 199,
        (byte) 12,
        (byte) 236,
        (byte) 25,
        (byte) 126,
        (byte) 208 /*0xD0*/,
        (byte) 188,
        (byte) 38,
        (byte) 232,
        (byte) 48 /*0x30*/,
        (byte) 27,
        (byte) 185,
        (byte) 136,
        (byte) 168,
        (byte) 82,
        (byte) 24,
        (byte) 168,
        (byte) 194,
        (byte) 52,
        (byte) 177,
        (byte) 230,
        (byte) 169,
        (byte) 111,
        (byte) 185,
        (byte) 160 /*0xA0*/,
        (byte) 180,
        (byte) 30,
        (byte) 120
      };
      key.Query(true, 336, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[27]
      {
        (byte) 144 /*0x90*/,
        (byte) 39,
        (byte) 13,
        (byte) 175,
        (byte) 167,
        (byte) 172,
        (byte) 156,
        (byte) 143,
        (byte) 166,
        (byte) 254,
        (byte) 117,
        (byte) 0,
        (byte) 122,
        (byte) 88,
        (byte) 243,
        (byte) 173,
        (byte) 227,
        (byte) 103,
        (byte) 157,
        (byte) 84,
        (byte) 142,
        (byte) 179,
        (byte) 150,
        (byte) 21,
        (byte) 141,
        (byte) 135,
        (byte) 237
      };
      byte[] numArray7 = new byte[27]
      {
        (byte) 27,
        (byte) 56,
        (byte) 124,
        (byte) 211,
        (byte) 211,
        (byte) 224 /*0xE0*/,
        (byte) 136,
        (byte) 148,
        (byte) 151,
        (byte) 55,
        (byte) 236,
        (byte) 209,
        (byte) 3,
        (byte) 214,
        (byte) 36,
        (byte) 122,
        (byte) 252,
        (byte) 15,
        (byte) 234,
        (byte) 116,
        (byte) 163,
        (byte) 51,
        (byte) 199,
        (byte) 77,
        (byte) 235,
        (byte) 6,
        (byte) 65
      };
      key.Query(true, 336, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 27);
      for (int index = 0; index < 27; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[33];
      byte[] response = new byte[33];
      Array.Copy((Array) sc_552.sspq, 41, (Array) numArray8, 0, 33);
      key.Query(true, 336, numArray8, response);
      Array.Copy((Array) sc_552.sspr, 41, (Array) numArray8, 0, 33);
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
    byte[] numArray9 = new byte[137];
    byte[] numArray10 = new byte[55];
    numArray10[33] = (byte) 239;
    numArray10[32 /*0x20*/] = (byte) 20;
    numArray10[2] = (byte) 65;
    numArray10[51] = (byte) 29;
    numArray10[4] = (byte) 187;
    numArray10[5] = (byte) 100;
    numArray10[22] = (byte) 119;
    numArray10[27] = (byte) 179;
    numArray10[8] = (byte) 102;
    numArray10[9] = (byte) 167;
    numArray10[12] = (byte) 105;
    numArray10[15] = (byte) 136;
    numArray10[37] = (byte) 15;
    numArray10[13] = (byte) 43;
    numArray10[3] = (byte) 213;
    numArray10[7] = (byte) 24;
    numArray10[36] = (byte) 195;
    numArray10[48 /*0x30*/] = (byte) 188;
    numArray10[10] = (byte) 11;
    numArray10[26] = (byte) 254;
    numArray10[20] = (byte) 153;
    numArray10[21] = (byte) 114;
    numArray10[17] = (byte) 160 /*0xA0*/;
    numArray10[23] = (byte) 17;
    numArray10[34] = (byte) 12;
    numArray10[25] = (byte) 191;
    numArray10[19] = (byte) 191;
    numArray10[6] = (byte) 3;
    numArray10[28] = (byte) 167;
    numArray10[47] = (byte) 100;
    numArray10[16 /*0x10*/] = (byte) 162;
    numArray10[46] = (byte) 115;
    numArray10[42] = (byte) 198;
    numArray10[11] = (byte) 227;
    numArray10[18] = (byte) 254;
    numArray10[35] = (byte) 41;
    numArray10[14] = (byte) 59;
    numArray10[0] = (byte) 94;
    numArray10[38] = (byte) 145;
    numArray10[39] = (byte) 165;
    numArray10[31 /*0x1F*/] = (byte) 180;
    numArray10[41] = (byte) 153;
    numArray10[52] = (byte) 148;
    numArray10[43] = (byte) 165;
    numArray10[44] = (byte) 161;
    numArray10[45] = (byte) 192 /*0xC0*/;
    numArray10[29] = (byte) 225;
    numArray10[30] = (byte) 55;
    numArray10[40] = (byte) 54;
    numArray10[49] = (byte) 154;
    numArray10[50] = (byte) 125;
    numArray10[1] = (byte) 177;
    numArray10[24] = (byte) 0;
    numArray10[53] = (byte) 214;
    numArray10[54] = (byte) 193;
    byte[] numArray11 = new byte[55]
    {
      (byte) 156,
      (byte) 226,
      (byte) 220,
      (byte) 208 /*0xD0*/,
      (byte) 248,
      (byte) 25,
      (byte) 52,
      (byte) 98,
      (byte) 156,
      (byte) 24,
      (byte) 139,
      (byte) 209,
      (byte) 148,
      (byte) 202,
      (byte) 32 /*0x20*/,
      (byte) 229,
      (byte) 241,
      (byte) 23,
      (byte) 4,
      (byte) 64 /*0x40*/,
      (byte) 52,
      (byte) 101,
      (byte) 92,
      (byte) 95,
      (byte) 117,
      (byte) 247,
      (byte) 102,
      (byte) 54,
      (byte) 195,
      (byte) 91,
      (byte) 254,
      (byte) 175,
      (byte) 43,
      (byte) 209,
      (byte) 113,
      (byte) 141,
      (byte) 108,
      (byte) 175,
      (byte) 36,
      (byte) 5,
      (byte) 16 /*0x10*/,
      (byte) 211,
      (byte) 7,
      (byte) 243,
      (byte) 170,
      (byte) 66,
      (byte) 244,
      (byte) 109,
      (byte) 55,
      (byte) 141,
      (byte) 201,
      (byte) 87,
      (byte) 84,
      (byte) 232,
      (byte) 125
    };
    key.Query(true, 336, numArray10, numArray10);
    Array.Copy((Array) numArray10, 0, (Array) numArray9, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray9[index] ^= numArray11[index];
    byte[] numArray12 = new byte[55]
    {
      (byte) 190,
      (byte) 147,
      (byte) 175,
      (byte) 204,
      (byte) 88,
      (byte) 99,
      (byte) 33,
      (byte) 85,
      (byte) 56,
      (byte) 145,
      (byte) 75,
      byte.MaxValue,
      (byte) 35,
      (byte) 48 /*0x30*/,
      (byte) 89,
      (byte) 121,
      (byte) 25,
      (byte) 161,
      (byte) 214,
      (byte) 107,
      (byte) 156,
      (byte) 38,
      (byte) 78,
      (byte) 83,
      (byte) 84,
      (byte) 76,
      (byte) 56,
      (byte) 121,
      (byte) 11,
      (byte) 227,
      (byte) 240 /*0xF0*/,
      (byte) 108,
      (byte) 224 /*0xE0*/,
      (byte) 134,
      (byte) 213,
      (byte) 166,
      (byte) 230,
      (byte) 114,
      (byte) 142,
      (byte) 196,
      (byte) 31 /*0x1F*/,
      (byte) 224 /*0xE0*/,
      (byte) 17,
      (byte) 96 /*0x60*/,
      (byte) 173,
      (byte) 69,
      (byte) 135,
      (byte) 165,
      (byte) 144 /*0x90*/,
      (byte) 148,
      (byte) 34,
      (byte) 172,
      (byte) 235,
      (byte) 212,
      (byte) 203
    };
    byte[] numArray13 = new byte[55]
    {
      (byte) 71,
      (byte) 167,
      (byte) 193,
      (byte) 83,
      (byte) 15,
      (byte) 178,
      (byte) 198,
      (byte) 56,
      (byte) 108,
      (byte) 167,
      (byte) 158,
      (byte) 156,
      (byte) 66,
      (byte) 102,
      (byte) 180,
      (byte) 31 /*0x1F*/,
      (byte) 195,
      (byte) 138,
      (byte) 138,
      (byte) 17,
      (byte) 0,
      (byte) 154,
      (byte) 248,
      (byte) 130,
      (byte) 58,
      (byte) 197,
      (byte) 248,
      (byte) 108,
      (byte) 152,
      (byte) 169,
      (byte) 26,
      (byte) 146,
      (byte) 131,
      (byte) 112 /*0x70*/,
      (byte) 105,
      (byte) 57,
      (byte) 49,
      (byte) 104,
      (byte) 30,
      (byte) 127 /*0x7F*/,
      (byte) 210,
      (byte) 72,
      (byte) 6,
      (byte) 5,
      (byte) 172,
      (byte) 251,
      (byte) 55,
      (byte) 162,
      (byte) 49,
      (byte) 154,
      (byte) 84,
      (byte) 78,
      (byte) 180,
      (byte) 189,
      (byte) 140
    };
    key.Query(true, 336, numArray12, numArray12);
    Array.Copy((Array) numArray12, 0, (Array) numArray9, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray9[index + 55] ^= numArray13[index];
    byte[] numArray14 = new byte[27]
    {
      (byte) 91,
      (byte) 120,
      (byte) 11,
      (byte) 43,
      (byte) 168,
      (byte) 73,
      (byte) 84,
      (byte) 161,
      (byte) 223,
      (byte) 30,
      (byte) 156,
      (byte) 90,
      (byte) 178,
      (byte) 146,
      (byte) 166,
      (byte) 19,
      (byte) 17,
      (byte) 75,
      (byte) 188,
      (byte) 20,
      (byte) 238,
      (byte) 165,
      (byte) 14,
      (byte) 197,
      (byte) 147,
      (byte) 232,
      (byte) 119
    };
    byte[] numArray15 = new byte[27];
    numArray15[6] = (byte) 154;
    numArray15[1] = (byte) 98;
    numArray15[22] = (byte) 204;
    numArray15[3] = (byte) 87;
    numArray15[21] = (byte) 0;
    numArray15[20] = (byte) 74;
    numArray15[26] = (byte) 84;
    numArray15[7] = (byte) 114;
    numArray15[8] = (byte) 34;
    numArray15[0] = (byte) 6;
    numArray15[23] = (byte) 175;
    numArray15[11] = (byte) 190;
    numArray15[14] = (byte) 215;
    numArray15[13] = (byte) 5;
    numArray15[5] = (byte) 130;
    numArray15[15] = (byte) 232;
    numArray15[2] = (byte) 166;
    numArray15[17] = (byte) 102;
    numArray15[18] = (byte) 194;
    numArray15[19] = (byte) 189;
    numArray15[16 /*0x10*/] = (byte) 25;
    numArray15[12] = (byte) 183;
    numArray15[10] = (byte) 149;
    numArray15[25] = (byte) 60;
    numArray15[24] = (byte) 80 /*0x50*/;
    numArray15[4] = (byte) 238;
    numArray15[9] = (byte) 99;
    key.Query(true, 336, numArray14, numArray14);
    Array.Copy((Array) numArray14, 0, (Array) numArray9, 110, 27);
    for (int index = 0; index < 27; ++index)
      numArray9[index + 110] ^= numArray15[index];
    byte[] numArray16 = new byte[10];
    byte[] response1 = new byte[10];
    Array.Copy((Array) sc_552.sspq, 74, (Array) numArray16, 0, 10);
    key.Query(true, 336, numArray16, response1);
    Array.Copy((Array) sc_552.sspr, 74, (Array) numArray16, 0, 10);
    for (int index = 0; index < numArray16.Length; ++index)
    {
      if ((int) numArray16[index] != (int) response1[index])
      {
        key.TagValue = (int) response1[index];
        break;
      }
    }
    return Encoding.UTF8.GetString(numArray9);
  }

  internal static string ssp_archives_556()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[116];
      byte[] numArray2 = new byte[55]
      {
        (byte) 172,
        (byte) 144 /*0x90*/,
        (byte) 120,
        (byte) 212,
        (byte) 37,
        (byte) 87,
        (byte) 46,
        (byte) 215,
        (byte) 232,
        (byte) 219,
        (byte) 72,
        (byte) 10,
        (byte) 117,
        (byte) 70,
        (byte) 170,
        (byte) 167,
        (byte) 131,
        (byte) 170,
        (byte) 186,
        (byte) 10,
        (byte) 212,
        (byte) 144 /*0x90*/,
        (byte) 60,
        (byte) 0,
        (byte) 183,
        (byte) 195,
        (byte) 42,
        (byte) 98,
        (byte) 111,
        (byte) 19,
        (byte) 8,
        (byte) 45,
        (byte) 141,
        (byte) 250,
        (byte) 206,
        (byte) 8,
        (byte) 97,
        (byte) 180,
        (byte) 234,
        (byte) 43,
        (byte) 40,
        (byte) 222,
        (byte) 129,
        (byte) 106,
        (byte) 80 /*0x50*/,
        (byte) 181,
        (byte) 93,
        (byte) 76,
        (byte) 178,
        (byte) 183,
        (byte) 143,
        (byte) 55,
        (byte) 98,
        (byte) 70,
        (byte) 51
      };
      byte[] numArray3 = new byte[55];
      numArray3[50] = (byte) 77;
      numArray3[12] = (byte) 65;
      numArray3[2] = (byte) 116;
      numArray3[28] = (byte) 81;
      numArray3[4] = (byte) 114;
      numArray3[5] = (byte) 183;
      numArray3[19] = (byte) 243;
      numArray3[42] = (byte) 13;
      numArray3[8] = (byte) 23;
      numArray3[9] = (byte) 112 /*0x70*/;
      numArray3[21] = (byte) 254;
      numArray3[40] = (byte) 25;
      numArray3[43] = (byte) 115;
      numArray3[13] = (byte) 174;
      numArray3[14] = (byte) 128 /*0x80*/;
      numArray3[18] = (byte) 66;
      numArray3[16 /*0x10*/] = (byte) 202;
      numArray3[6] = (byte) 247;
      numArray3[3] = (byte) 89;
      numArray3[10] = (byte) 23;
      numArray3[53] = (byte) 57;
      numArray3[47] = (byte) 148;
      numArray3[22] = (byte) 238;
      numArray3[23] = (byte) 22;
      numArray3[11] = (byte) 237;
      numArray3[15] = (byte) 128 /*0x80*/;
      numArray3[26] = (byte) 99;
      numArray3[27] = (byte) 187;
      numArray3[32 /*0x20*/] = (byte) 115;
      numArray3[51] = (byte) 162;
      numArray3[48 /*0x30*/] = (byte) 66;
      numArray3[38] = (byte) 26;
      numArray3[45] = (byte) 12;
      numArray3[33] = (byte) 79;
      numArray3[7] = (byte) 123;
      numArray3[35] = (byte) 216;
      numArray3[1] = (byte) 106;
      numArray3[37] = (byte) 46;
      numArray3[25] = (byte) 120;
      numArray3[39] = (byte) 54;
      numArray3[0] = (byte) 155;
      numArray3[41] = (byte) 75;
      numArray3[34] = (byte) 237;
      numArray3[17] = (byte) 117;
      numArray3[36] = (byte) 209;
      numArray3[31 /*0x1F*/] = (byte) 179;
      numArray3[46] = (byte) 19;
      numArray3[24] = (byte) 33;
      numArray3[30] = (byte) 184;
      numArray3[49] = (byte) 32 /*0x20*/;
      numArray3[29] = (byte) 56;
      numArray3[44] = (byte) 159;
      numArray3[52] = (byte) 222;
      numArray3[20] = (byte) 228;
      numArray3[54] = (byte) 134;
      key.Query(true, 336, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55];
      numArray4[17] = (byte) 134;
      numArray4[1] = (byte) 28;
      numArray4[9] = (byte) 54;
      numArray4[3] = (byte) 79;
      numArray4[10] = (byte) 180;
      numArray4[2] = (byte) 251;
      numArray4[18] = (byte) 127 /*0x7F*/;
      numArray4[7] = (byte) 0;
      numArray4[0] = (byte) 198;
      numArray4[54] = (byte) 72;
      numArray4[35] = (byte) 122;
      numArray4[37] = (byte) 174;
      numArray4[12] = (byte) 236;
      numArray4[38] = (byte) 62;
      numArray4[14] = (byte) 56;
      numArray4[53] = (byte) 172;
      numArray4[21] = (byte) 123;
      numArray4[20] = (byte) 57;
      numArray4[42] = (byte) 229;
      numArray4[30] = (byte) 226;
      numArray4[36] = (byte) 140;
      numArray4[15] = (byte) 56;
      numArray4[16 /*0x10*/] = (byte) 144 /*0x90*/;
      numArray4[22] = (byte) 201;
      numArray4[24] = (byte) 7;
      numArray4[32 /*0x20*/] = (byte) 164;
      numArray4[26] = (byte) 69;
      numArray4[28] = (byte) 91;
      numArray4[25] = (byte) 158;
      numArray4[29] = (byte) 246;
      numArray4[5] = (byte) 176 /*0xB0*/;
      numArray4[31 /*0x1F*/] = (byte) 137;
      numArray4[19] = (byte) 97;
      numArray4[33] = (byte) 51;
      numArray4[49] = (byte) 61;
      numArray4[11] = (byte) 196;
      numArray4[4] = (byte) 28;
      numArray4[34] = (byte) 35;
      numArray4[48 /*0x30*/] = (byte) 136;
      numArray4[39] = (byte) 198;
      numArray4[40] = (byte) 93;
      numArray4[41] = (byte) 151;
      numArray4[44] = (byte) 45;
      numArray4[43] = (byte) 0;
      numArray4[8] = (byte) 79;
      numArray4[45] = (byte) 16 /*0x10*/;
      numArray4[46] = (byte) 35;
      numArray4[47] = (byte) 22;
      numArray4[52] = (byte) 189;
      numArray4[13] = (byte) 91;
      numArray4[6] = (byte) 80 /*0x50*/;
      numArray4[51] = (byte) 221;
      numArray4[50] = (byte) 68;
      numArray4[27] = (byte) 195;
      numArray4[23] = (byte) 131;
      byte[] numArray5 = new byte[55]
      {
        (byte) 141,
        (byte) 3,
        (byte) 25,
        (byte) 139,
        (byte) 77,
        (byte) 75,
        (byte) 151,
        (byte) 171,
        (byte) 196,
        (byte) 238,
        (byte) 128 /*0x80*/,
        (byte) 223,
        (byte) 190,
        (byte) 245,
        (byte) 152,
        (byte) 117,
        (byte) 236,
        (byte) 186,
        (byte) 7,
        (byte) 157,
        (byte) 141,
        (byte) 190,
        (byte) 162,
        (byte) 56,
        (byte) 132,
        (byte) 128 /*0x80*/,
        (byte) 93,
        (byte) 39,
        (byte) 79,
        (byte) 105,
        (byte) 34,
        (byte) 111,
        (byte) 29,
        (byte) 90,
        (byte) 171,
        (byte) 242,
        (byte) 164,
        (byte) 3,
        (byte) 31 /*0x1F*/,
        (byte) 150,
        (byte) 130,
        (byte) 133,
        (byte) 62,
        (byte) 171,
        (byte) 135,
        (byte) 137,
        (byte) 176 /*0xB0*/,
        (byte) 235,
        (byte) 51,
        (byte) 91,
        (byte) 233,
        (byte) 147,
        (byte) 81,
        (byte) 79,
        (byte) 209
      };
      key.Query(true, 336, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[6]
      {
        (byte) 33,
        (byte) 80 /*0x50*/,
        (byte) 2,
        (byte) 59,
        (byte) 82,
        (byte) 135
      };
      byte[] numArray7 = new byte[6]
      {
        (byte) 171,
        (byte) 29,
        (byte) 226,
        (byte) 79,
        (byte) 240 /*0xF0*/,
        (byte) 142
      };
      key.Query(true, 336, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 6);
      for (int index = 0; index < 6; ++index)
        numArray1[index + 110] ^= numArray7[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray8 = new byte[116];
    byte[] numArray9 = new byte[55]
    {
      (byte) 39,
      (byte) 207,
      (byte) 94,
      (byte) 150,
      (byte) 152,
      (byte) 123,
      (byte) 134,
      (byte) 93,
      (byte) 52,
      (byte) 174,
      (byte) 10,
      (byte) 28,
      (byte) 82,
      (byte) 6,
      (byte) 16 /*0x10*/,
      (byte) 79,
      (byte) 112 /*0x70*/,
      (byte) 140,
      (byte) 79,
      (byte) 109,
      (byte) 199,
      (byte) 80 /*0x50*/,
      (byte) 67,
      (byte) 36,
      (byte) 158,
      (byte) 242,
      (byte) 21,
      (byte) 190,
      (byte) 31 /*0x1F*/,
      (byte) 125,
      (byte) 106,
      (byte) 228,
      (byte) 91,
      (byte) 164,
      (byte) 201,
      (byte) 155,
      (byte) 128 /*0x80*/,
      (byte) 52,
      (byte) 248,
      (byte) 231,
      (byte) 16 /*0x10*/,
      (byte) 166,
      (byte) 115,
      (byte) 248,
      (byte) 169,
      (byte) 108,
      (byte) 207,
      (byte) 74,
      (byte) 241,
      (byte) 235,
      (byte) 225,
      (byte) 251,
      (byte) 87,
      (byte) 226,
      (byte) 150
    };
    byte[] numArray10 = new byte[55];
    numArray10[35] = (byte) 10;
    numArray10[53] = (byte) 78;
    numArray10[51] = (byte) 232;
    numArray10[0] = (byte) 205;
    numArray10[19] = (byte) 239;
    numArray10[21] = (byte) 134;
    numArray10[6] = (byte) 43;
    numArray10[52] = (byte) 114;
    numArray10[8] = (byte) 39;
    numArray10[9] = (byte) 19;
    numArray10[10] = (byte) 147;
    numArray10[46] = (byte) 106;
    numArray10[1] = (byte) 223;
    numArray10[40] = (byte) 88;
    numArray10[14] = (byte) 231;
    numArray10[15] = (byte) 127 /*0x7F*/;
    numArray10[7] = (byte) 56;
    numArray10[54] = (byte) 42;
    numArray10[18] = (byte) 201;
    numArray10[16 /*0x10*/] = (byte) 99;
    numArray10[5] = (byte) 46;
    numArray10[20] = (byte) 3;
    numArray10[22] = (byte) 122;
    numArray10[13] = (byte) 15;
    numArray10[28] = (byte) 246;
    numArray10[31 /*0x1F*/] = (byte) 93;
    numArray10[26] = (byte) 205;
    numArray10[42] = (byte) 212;
    numArray10[43] = (byte) 211;
    numArray10[29] = (byte) 67;
    numArray10[30] = (byte) 17;
    numArray10[25] = (byte) 88;
    numArray10[48 /*0x30*/] = (byte) 142;
    numArray10[17] = (byte) 105;
    numArray10[34] = (byte) 245;
    numArray10[11] = (byte) 150;
    numArray10[36] = (byte) 121;
    numArray10[37] = (byte) 224 /*0xE0*/;
    numArray10[38] = byte.MaxValue;
    numArray10[39] = (byte) 254;
    numArray10[3] = (byte) 75;
    numArray10[41] = (byte) 25;
    numArray10[44] = (byte) 32 /*0x20*/;
    numArray10[4] = (byte) 93;
    numArray10[23] = (byte) 164;
    numArray10[45] = (byte) 206;
    numArray10[12] = (byte) 202;
    numArray10[47] = (byte) 96 /*0x60*/;
    numArray10[27] = (byte) 239;
    numArray10[49] = (byte) 128 /*0x80*/;
    numArray10[50] = (byte) 179;
    numArray10[24] = (byte) 140;
    numArray10[32 /*0x20*/] = (byte) 18;
    numArray10[33] = (byte) 156;
    numArray10[2] = (byte) 23;
    key.Query(true, 336, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray8, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index] ^= numArray10[index];
    byte[] numArray11 = new byte[55];
    numArray11[20] = (byte) 150;
    numArray11[50] = (byte) 7;
    numArray11[12] = (byte) 102;
    numArray11[15] = (byte) 65;
    numArray11[4] = (byte) 251;
    numArray11[26] = (byte) 23;
    numArray11[6] = (byte) 27;
    numArray11[38] = (byte) 76;
    numArray11[3] = (byte) 153;
    numArray11[13] = (byte) 137;
    numArray11[1] = (byte) 196;
    numArray11[11] = (byte) 74;
    numArray11[23] = (byte) 0;
    numArray11[49] = (byte) 37;
    numArray11[43] = (byte) 100;
    numArray11[48 /*0x30*/] = (byte) 165;
    numArray11[2] = (byte) 131;
    numArray11[17] = (byte) 21;
    numArray11[18] = (byte) 183;
    numArray11[19] = (byte) 33;
    numArray11[32 /*0x20*/] = (byte) 249;
    numArray11[37] = (byte) 231;
    numArray11[36] = (byte) 206;
    numArray11[28] = (byte) 7;
    numArray11[24] = (byte) 190;
    numArray11[25] = (byte) 80 /*0x50*/;
    numArray11[0] = (byte) 191;
    numArray11[27] = (byte) 246;
    numArray11[31 /*0x1F*/] = (byte) 151;
    numArray11[10] = (byte) 159;
    numArray11[30] = (byte) 244;
    numArray11[21] = (byte) 246;
    numArray11[9] = (byte) 71;
    numArray11[33] = (byte) 206;
    numArray11[34] = (byte) 135;
    numArray11[35] = (byte) 120;
    numArray11[8] = (byte) 151;
    numArray11[29] = (byte) 159;
    numArray11[14] = byte.MaxValue;
    numArray11[7] = (byte) 213;
    numArray11[40] = (byte) 160 /*0xA0*/;
    numArray11[51] = (byte) 111;
    numArray11[42] = (byte) 236;
    numArray11[16 /*0x10*/] = (byte) 231;
    numArray11[44] = (byte) 130;
    numArray11[45] = (byte) 194;
    numArray11[46] = (byte) 21;
    numArray11[47] = (byte) 147;
    numArray11[39] = (byte) 95;
    numArray11[5] = (byte) 9;
    numArray11[22] = (byte) 134;
    numArray11[41] = (byte) 222;
    numArray11[52] = (byte) 190;
    numArray11[53] = (byte) 216;
    numArray11[54] = (byte) 170;
    byte[] numArray12 = new byte[55];
    numArray12[17] = (byte) 179;
    numArray12[10] = (byte) 143;
    numArray12[1] = (byte) 120;
    numArray12[3] = (byte) 132;
    numArray12[4] = (byte) 76;
    numArray12[37] = (byte) 61;
    numArray12[29] = (byte) 249;
    numArray12[7] = (byte) 84;
    numArray12[2] = (byte) 160 /*0xA0*/;
    numArray12[9] = (byte) 50;
    numArray12[19] = (byte) 238;
    numArray12[11] = (byte) 35;
    numArray12[12] = (byte) 186;
    numArray12[13] = (byte) 25;
    numArray12[52] = (byte) 187;
    numArray12[15] = (byte) 168;
    numArray12[16 /*0x10*/] = (byte) 152;
    numArray12[22] = (byte) 196;
    numArray12[14] = (byte) 64 /*0x40*/;
    numArray12[5] = (byte) 132;
    numArray12[33] = (byte) 24;
    numArray12[21] = (byte) 141;
    numArray12[36] = (byte) 105;
    numArray12[23] = (byte) 106;
    numArray12[24] = (byte) 237;
    numArray12[25] = (byte) 141;
    numArray12[20] = (byte) 30;
    numArray12[28] = (byte) 142;
    numArray12[40] = (byte) 160 /*0xA0*/;
    numArray12[31 /*0x1F*/] = (byte) 174;
    numArray12[8] = (byte) 195;
    numArray12[51] = (byte) 219;
    numArray12[47] = (byte) 40;
    numArray12[54] = (byte) 109;
    numArray12[49] = (byte) 32 /*0x20*/;
    numArray12[35] = (byte) 116;
    numArray12[34] = (byte) 66;
    numArray12[26] = (byte) 27;
    numArray12[0] = (byte) 71;
    numArray12[39] = (byte) 180;
    numArray12[18] = (byte) 251;
    numArray12[41] = (byte) 108;
    numArray12[42] = (byte) 230;
    numArray12[6] = (byte) 57;
    numArray12[30] = (byte) 15;
    numArray12[27] = (byte) 245;
    numArray12[46] = (byte) 106;
    numArray12[32 /*0x20*/] = (byte) 145;
    numArray12[48 /*0x30*/] = (byte) 109;
    numArray12[43] = (byte) 133;
    numArray12[50] = (byte) 58;
    numArray12[38] = (byte) 252;
    numArray12[44] = (byte) 192 /*0xC0*/;
    numArray12[53] = (byte) 71;
    numArray12[45] = (byte) 88;
    key.Query(true, 336, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray8, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index + 55] ^= numArray12[index];
    byte[] numArray13 = new byte[6]
    {
      (byte) 0,
      (byte) 85,
      (byte) 0,
      (byte) 0,
      (byte) 161,
      (byte) 189
    };
    numArray13[3] = (byte) 123;
    numArray13[0] = byte.MaxValue;
    numArray13[2] = (byte) 178;
    byte[] numArray14 = new byte[6]
    {
      (byte) 238,
      (byte) 218,
      (byte) 145,
      (byte) 44,
      (byte) 214,
      (byte) 11
    };
    key.Query(true, 336, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray8, 110, 6);
    for (int index = 0; index < 6; ++index)
      numArray8[index + 110] ^= numArray14[index];
    return Encoding.UTF8.GetString(numArray8);
  }

  internal static string ssp_archives_557()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[11];
      byte[] numArray2 = new byte[11]
      {
        (byte) 237,
        (byte) 93,
        (byte) 132,
        (byte) 128 /*0x80*/,
        (byte) 205,
        (byte) 183,
        (byte) 70,
        (byte) 180,
        (byte) 165,
        (byte) 97,
        (byte) 162
      };
      byte[] numArray3 = new byte[11]
      {
        (byte) 138,
        (byte) 56,
        (byte) 96 /*0x60*/,
        (byte) 171,
        (byte) 163,
        (byte) 197,
        (byte) 166,
        (byte) 145,
        (byte) 14,
        (byte) 55,
        (byte) 27
      };
      key.Query(true, 336, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 11);
      for (int index = 0; index < 11; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[11];
    byte[] numArray5 = new byte[11]
    {
      (byte) 118,
      (byte) 234,
      (byte) 84,
      (byte) 19,
      (byte) 73,
      (byte) 216,
      (byte) 126,
      (byte) 131,
      (byte) 6,
      (byte) 102,
      (byte) 196
    };
    byte[] numArray6 = new byte[11]
    {
      (byte) 177,
      (byte) 113,
      (byte) 88,
      (byte) 6,
      (byte) 138,
      (byte) 217,
      (byte) 139,
      (byte) 26,
      (byte) 163,
      (byte) 131,
      (byte) 73
    };
    key.Query(true, 336, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 11);
    for (int index = 0; index < 11; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[16 /*0x10*/];
    byte[] response = new byte[16 /*0x10*/];
    Array.Copy((Array) sc_552.sspq, 84, (Array) numArray7, 0, 16 /*0x10*/);
    key.Query(true, 336, numArray7, response);
    Array.Copy((Array) sc_552.sspr, 84, (Array) numArray7, 0, 16 /*0x10*/);
    for (int index = 0; index < numArray7.Length; ++index)
    {
      if ((int) numArray7[index] != (int) response[index])
      {
        key.TagValue = (int) response[index];
        break;
      }
    }
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_archives_558()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[11];
      byte[] numArray2 = new byte[11];
      numArray2[2] = (byte) 238;
      numArray2[1] = (byte) 192 /*0xC0*/;
      numArray2[10] = (byte) 87;
      numArray2[0] = (byte) 147;
      numArray2[4] = (byte) 240 /*0xF0*/;
      numArray2[5] = (byte) 26;
      numArray2[6] = (byte) 148;
      numArray2[7] = (byte) 235;
      numArray2[8] = (byte) 118;
      numArray2[9] = (byte) 106;
      numArray2[3] = (byte) 87;
      byte[] numArray3 = new byte[11];
      numArray3[1] = (byte) 87;
      numArray3[9] = (byte) 3;
      numArray3[2] = (byte) 59;
      numArray3[3] = (byte) 73;
      numArray3[8] = (byte) 87;
      numArray3[5] = (byte) 138;
      numArray3[10] = (byte) 157;
      numArray3[7] = (byte) 110;
      numArray3[4] = (byte) 192 /*0xC0*/;
      numArray3[0] = (byte) 175;
      numArray3[6] = (byte) 198;
      key.Query(true, 336, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 11);
      for (int index = 0; index < 11; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[11];
    byte[] numArray5 = new byte[11]
    {
      (byte) 238,
      (byte) 113,
      (byte) 23,
      (byte) 135,
      (byte) 26,
      (byte) 101,
      (byte) 20,
      (byte) 33,
      (byte) 158,
      (byte) 166,
      (byte) 111
    };
    byte[] numArray6 = new byte[11]
    {
      (byte) 57,
      (byte) 253,
      (byte) 220,
      (byte) 156,
      (byte) 198,
      (byte) 157,
      (byte) 34,
      (byte) 7,
      (byte) 79,
      (byte) 103,
      (byte) 151
    };
    key.Query(true, 336, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 11);
    for (int index = 0; index < 11; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[24];
    byte[] response = new byte[24];
    Array.Copy((Array) sc_552.sspq, 100, (Array) numArray7, 0, 24);
    key.Query(true, 336, numArray7, response);
    Array.Copy((Array) sc_552.sspr, 100, (Array) numArray7, 0, 24);
    for (int index = 0; index < numArray7.Length; ++index)
    {
      if ((int) numArray7[index] != (int) response[index])
      {
        key.TagValue = (int) response[index];
        break;
      }
    }
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_archives_559()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[11];
      byte[] numArray2 = new byte[11];
      numArray2[10] = (byte) 70;
      numArray2[1] = (byte) 76;
      numArray2[9] = (byte) 118;
      numArray2[6] = (byte) 221;
      numArray2[8] = (byte) 110;
      numArray2[5] = (byte) 20;
      numArray2[0] = (byte) 189;
      numArray2[7] = (byte) 34;
      numArray2[3] = (byte) 127 /*0x7F*/;
      numArray2[2] = (byte) 226;
      numArray2[4] = (byte) 186;
      byte[] numArray3 = new byte[11]
      {
        (byte) 9,
        (byte) 203,
        (byte) 34,
        (byte) 251,
        (byte) 80 /*0x50*/,
        (byte) 176 /*0xB0*/,
        (byte) 39,
        (byte) 50,
        (byte) 102,
        (byte) 116,
        (byte) 211
      };
      key.Query(true, 336, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 11);
      for (int index = 0; index < 11; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[25];
      byte[] response = new byte[25];
      Array.Copy((Array) sc_552.sspq, 124, (Array) numArray4, 0, 25);
      key.Query(true, 336, numArray4, response);
      Array.Copy((Array) sc_552.sspr, 124, (Array) numArray4, 0, 25);
      for (int index = 0; index < numArray4.Length; ++index)
      {
        if ((int) numArray4[index] != (int) response[index])
        {
          key.TagValue = (int) response[index];
          break;
        }
      }
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray5 = new byte[11];
    byte[] numArray6 = new byte[11];
    numArray6[0] = (byte) 52;
    numArray6[10] = (byte) 12;
    numArray6[2] = (byte) 81;
    numArray6[3] = (byte) 128 /*0x80*/;
    numArray6[1] = (byte) 244;
    numArray6[6] = (byte) 211;
    numArray6[5] = (byte) 231;
    numArray6[7] = (byte) 170;
    numArray6[8] = (byte) 151;
    numArray6[9] = (byte) 14;
    numArray6[4] = (byte) 157;
    byte[] numArray7 = new byte[11]
    {
      (byte) 53,
      (byte) 187,
      (byte) 80 /*0x50*/,
      (byte) 99,
      (byte) 133,
      (byte) 163,
      (byte) 72,
      (byte) 162,
      (byte) 187,
      (byte) 227,
      (byte) 91
    };
    key.Query(true, 336, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 11);
    for (int index = 0; index < 11; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_archives_560()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[11];
      byte[] numArray2 = new byte[11]
      {
        (byte) 30,
        (byte) 196,
        (byte) 39,
        (byte) 27,
        (byte) 70,
        byte.MaxValue,
        (byte) 53,
        (byte) 92,
        (byte) 126,
        (byte) 84,
        (byte) 254
      };
      byte[] numArray3 = new byte[11];
      numArray3[2] = (byte) 53;
      numArray3[1] = (byte) 118;
      numArray3[10] = (byte) 184;
      numArray3[3] = (byte) 32 /*0x20*/;
      numArray3[0] = (byte) 136;
      numArray3[5] = (byte) 99;
      numArray3[6] = (byte) 4;
      numArray3[7] = (byte) 161;
      numArray3[8] = (byte) 34;
      numArray3[4] = (byte) 136;
      numArray3[9] = (byte) 163;
      key.Query(true, 336, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 11);
      for (int index = 0; index < 11; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[11];
    byte[] numArray5 = new byte[11];
    numArray5[0] = (byte) 198;
    numArray5[1] = (byte) 85;
    numArray5[2] = (byte) 3;
    numArray5[8] = (byte) 5;
    numArray5[5] = (byte) 226;
    numArray5[6] = (byte) 127 /*0x7F*/;
    numArray5[4] = (byte) 238;
    numArray5[7] = (byte) 60;
    numArray5[3] = (byte) 172;
    numArray5[9] = (byte) 201;
    numArray5[10] = (byte) 15;
    byte[] numArray6 = new byte[11]
    {
      (byte) 239,
      (byte) 172,
      (byte) 104,
      (byte) 100,
      (byte) 85,
      (byte) 151,
      (byte) 32 /*0x20*/,
      (byte) 165,
      (byte) 105,
      (byte) 108,
      (byte) 205
    };
    key.Query(true, 336, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 11);
    for (int index = 0; index < 11; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_archives_561()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[11];
      byte[] numArray2 = new byte[11];
      numArray2[5] = (byte) 120;
      numArray2[7] = (byte) 238;
      numArray2[2] = (byte) 56;
      numArray2[0] = (byte) 239;
      numArray2[4] = (byte) 155;
      numArray2[1] = (byte) 39;
      numArray2[6] = (byte) 80 /*0x50*/;
      numArray2[10] = (byte) 49;
      numArray2[8] = (byte) 128 /*0x80*/;
      numArray2[9] = (byte) 169;
      numArray2[3] = (byte) 231;
      byte[] numArray3 = new byte[11]
      {
        (byte) 85,
        (byte) 124,
        (byte) 144 /*0x90*/,
        (byte) 242,
        (byte) 57,
        (byte) 31 /*0x1F*/,
        (byte) 200,
        (byte) 151,
        (byte) 133,
        (byte) 14,
        (byte) 109
      };
      key.Query(true, 336, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 11);
      for (int index = 0; index < 11; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[33];
      byte[] response = new byte[33];
      Array.Copy((Array) sc_552.sspq, 149, (Array) numArray4, 0, 33);
      key.Query(true, 336, numArray4, response);
      Array.Copy((Array) sc_552.sspr, 149, (Array) numArray4, 0, 33);
      for (int index = 0; index < numArray4.Length; ++index)
      {
        if ((int) numArray4[index] != (int) response[index])
        {
          key.TagValue = (int) response[index];
          break;
        }
      }
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray5 = new byte[11];
    byte[] numArray6 = new byte[11]
    {
      (byte) 94,
      (byte) 211,
      (byte) 80 /*0x50*/,
      (byte) 179,
      (byte) 76,
      (byte) 178,
      (byte) 107,
      (byte) 139,
      (byte) 114,
      (byte) 128 /*0x80*/,
      (byte) 37
    };
    byte[] numArray7 = new byte[11]
    {
      (byte) 201,
      (byte) 204,
      (byte) 51,
      (byte) 77,
      (byte) 197,
      (byte) 34,
      (byte) 58,
      (byte) 242,
      (byte) 6,
      (byte) 63 /*0x3F*/,
      (byte) 225
    };
    key.Query(true, 336, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 11);
    for (int index = 0; index < 11; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_archives_562()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[11];
      byte[] numArray2 = new byte[11]
      {
        (byte) 81,
        (byte) 105,
        (byte) 17,
        (byte) 56,
        (byte) 189,
        (byte) 107,
        (byte) 135,
        (byte) 110,
        (byte) 167,
        (byte) 242,
        (byte) 2
      };
      byte[] numArray3 = new byte[11]
      {
        (byte) 5,
        (byte) 253,
        (byte) 135,
        (byte) 89,
        (byte) 76,
        (byte) 127 /*0x7F*/,
        (byte) 142,
        (byte) 85,
        (byte) 174,
        (byte) 61,
        (byte) 235
      };
      key.Query(true, 336, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 11);
      for (int index = 0; index < 11; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[11];
    byte[] numArray5 = new byte[11];
    numArray5[9] = (byte) 251;
    numArray5[3] = (byte) 23;
    numArray5[8] = (byte) 218;
    numArray5[4] = (byte) 108;
    numArray5[2] = (byte) 206;
    numArray5[0] = (byte) 66;
    numArray5[6] = (byte) 195;
    numArray5[7] = (byte) 226;
    numArray5[1] = (byte) 92;
    numArray5[5] = (byte) 207;
    numArray5[10] = (byte) 176 /*0xB0*/;
    byte[] numArray6 = new byte[11]
    {
      (byte) 155,
      (byte) 85,
      (byte) 238,
      (byte) 33,
      (byte) 73,
      (byte) 173,
      (byte) 224 /*0xE0*/,
      (byte) 87,
      (byte) 36,
      (byte) 236,
      (byte) 132
    };
    key.Query(true, 336, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 11);
    for (int index = 0; index < 11; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_archives_563()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[11];
      byte[] numArray2 = new byte[11];
      numArray2[10] = (byte) 86;
      numArray2[1] = (byte) 101;
      numArray2[9] = (byte) 36;
      numArray2[3] = (byte) 192 /*0xC0*/;
      numArray2[7] = (byte) 105;
      numArray2[5] = (byte) 200;
      numArray2[6] = (byte) 58;
      numArray2[4] = (byte) 73;
      numArray2[0] = (byte) 214;
      numArray2[8] = (byte) 47;
      numArray2[2] = (byte) 168;
      byte[] numArray3 = new byte[11]
      {
        (byte) 184,
        (byte) 101,
        (byte) 12,
        (byte) 41,
        (byte) 186,
        (byte) 67,
        (byte) 161,
        (byte) 144 /*0x90*/,
        (byte) 144 /*0x90*/,
        (byte) 156,
        (byte) 237
      };
      key.Query(true, 336, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 11);
      for (int index = 0; index < 11; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[28];
      byte[] response = new byte[28];
      Array.Copy((Array) sc_552.sspq, 182, (Array) numArray4, 0, 28);
      key.Query(true, 336, numArray4, response);
      Array.Copy((Array) sc_552.sspr, 182, (Array) numArray4, 0, 28);
      for (int index = 0; index < numArray4.Length; ++index)
      {
        if ((int) numArray4[index] != (int) response[index])
        {
          key.TagValue = (int) response[index];
          break;
        }
      }
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray5 = new byte[11];
    byte[] numArray6 = new byte[11]
    {
      (byte) 26,
      (byte) 146,
      (byte) 171,
      (byte) 53,
      (byte) 110,
      (byte) 53,
      (byte) 196,
      (byte) 140,
      (byte) 250,
      (byte) 93,
      (byte) 75
    };
    byte[] numArray7 = new byte[11];
    numArray7[3] = (byte) 29;
    numArray7[1] = (byte) 156;
    numArray7[2] = (byte) 152;
    numArray7[0] = (byte) 243;
    numArray7[4] = (byte) 196;
    numArray7[5] = (byte) 83;
    numArray7[7] = (byte) 174;
    numArray7[9] = (byte) 153;
    numArray7[8] = (byte) 50;
    numArray7[6] = (byte) 25;
    numArray7[10] = (byte) 22;
    key.Query(true, 336, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 11);
    for (int index = 0; index < 11; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_archives_564()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[11];
      byte[] numArray2 = new byte[11];
      numArray2[9] = (byte) 103;
      numArray2[3] = (byte) 44;
      numArray2[2] = (byte) 120;
      numArray2[0] = (byte) 253;
      numArray2[4] = (byte) 240 /*0xF0*/;
      numArray2[5] = (byte) 105;
      numArray2[8] = (byte) 10;
      numArray2[7] = (byte) 84;
      numArray2[1] = (byte) 115;
      numArray2[6] = (byte) 179;
      numArray2[10] = (byte) 179;
      byte[] numArray3 = new byte[11]
      {
        (byte) 229,
        (byte) 208 /*0xD0*/,
        (byte) 187,
        (byte) 247,
        (byte) 114,
        (byte) 44,
        (byte) 204,
        (byte) 157,
        (byte) 226,
        (byte) 105,
        (byte) 49
      };
      key.Query(true, 336, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 11);
      for (int index = 0; index < 11; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[26];
      byte[] response = new byte[26];
      Array.Copy((Array) sc_552.sspq, 210, (Array) numArray4, 0, 26);
      key.Query(true, 336, numArray4, response);
      Array.Copy((Array) sc_552.sspr, 210, (Array) numArray4, 0, 26);
      for (int index = 0; index < numArray4.Length; ++index)
      {
        if ((int) numArray4[index] != (int) response[index])
        {
          key.TagValue = (int) response[index];
          break;
        }
      }
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray5 = new byte[11];
    byte[] numArray6 = new byte[11]
    {
      (byte) 227,
      (byte) 147,
      (byte) 218,
      (byte) 151,
      (byte) 63 /*0x3F*/,
      (byte) 51,
      (byte) 31 /*0x1F*/,
      (byte) 131,
      (byte) 94,
      (byte) 137,
      (byte) 117
    };
    byte[] numArray7 = new byte[11];
    numArray7[7] = (byte) 72;
    numArray7[3] = (byte) 186;
    numArray7[9] = (byte) 217;
    numArray7[0] = (byte) 103;
    numArray7[10] = (byte) 233;
    numArray7[5] = (byte) 136;
    numArray7[6] = (byte) 252;
    numArray7[4] = (byte) 247;
    numArray7[8] = (byte) 47;
    numArray7[1] = (byte) 183;
    numArray7[2] = (byte) 85;
    key.Query(true, 336, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 11);
    for (int index = 0; index < 11; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_archives_565()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[11];
      byte[] numArray2 = new byte[11];
      numArray2[4] = (byte) 174;
      numArray2[1] = (byte) 218;
      numArray2[2] = (byte) 162;
      numArray2[0] = (byte) 8;
      numArray2[7] = (byte) 230;
      numArray2[3] = (byte) 37;
      numArray2[6] = (byte) 74;
      numArray2[5] = (byte) 133;
      numArray2[8] = (byte) 159;
      numArray2[9] = (byte) 11;
      numArray2[10] = (byte) 80 /*0x50*/;
      byte[] numArray3 = new byte[11]
      {
        (byte) 179,
        (byte) 206,
        (byte) 72,
        (byte) 97,
        (byte) 74,
        (byte) 176 /*0xB0*/,
        (byte) 105,
        (byte) 180,
        (byte) 89,
        (byte) 161,
        (byte) 231
      };
      key.Query(true, 336, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 11);
      for (int index = 0; index < 11; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[11];
    byte[] numArray5 = new byte[11];
    numArray5[5] = (byte) 183;
    numArray5[1] = (byte) 18;
    numArray5[4] = (byte) 128 /*0x80*/;
    numArray5[3] = (byte) 49;
    numArray5[0] = (byte) 167;
    numArray5[8] = (byte) 1;
    numArray5[6] = (byte) 197;
    numArray5[7] = (byte) 216;
    numArray5[2] = (byte) 108;
    numArray5[9] = (byte) 42;
    numArray5[10] = (byte) 69;
    byte[] numArray6 = new byte[11];
    numArray6[0] = (byte) 217;
    numArray6[10] = (byte) 14;
    numArray6[2] = (byte) 47;
    numArray6[4] = (byte) 74;
    numArray6[6] = (byte) 6;
    numArray6[5] = (byte) 211;
    numArray6[3] = (byte) 244;
    numArray6[8] = (byte) 124;
    numArray6[1] = (byte) 99;
    numArray6[9] = (byte) 191;
    numArray6[7] = (byte) 163;
    key.Query(true, 336, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 11);
    for (int index = 0; index < 11; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_archives_566()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[11];
      byte[] numArray2 = new byte[11]
      {
        (byte) 181,
        (byte) 30,
        (byte) 84,
        (byte) 138,
        (byte) 200,
        (byte) 15,
        (byte) 182,
        (byte) 104,
        (byte) 7,
        (byte) 74,
        (byte) 194
      };
      byte[] numArray3 = new byte[11]
      {
        (byte) 49,
        (byte) 175,
        (byte) 43,
        (byte) 6,
        (byte) 203,
        (byte) 190,
        (byte) 3,
        (byte) 210,
        (byte) 101,
        (byte) 181,
        (byte) 38
      };
      key.Query(true, 336, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 11);
      for (int index = 0; index < 11; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[11];
    byte[] numArray5 = new byte[11]
    {
      (byte) 123,
      (byte) 117,
      (byte) 26,
      (byte) 251,
      (byte) 227,
      (byte) 172,
      (byte) 11,
      (byte) 209,
      (byte) 160 /*0xA0*/,
      (byte) 121,
      (byte) 142
    };
    byte[] numArray6 = new byte[11]
    {
      (byte) 76,
      (byte) 50,
      (byte) 57,
      (byte) 49,
      (byte) 56,
      (byte) 198,
      (byte) 53,
      (byte) 211,
      (byte) 154,
      (byte) 253,
      (byte) 137
    };
    key.Query(true, 336, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 11);
    for (int index = 0; index < 11; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
