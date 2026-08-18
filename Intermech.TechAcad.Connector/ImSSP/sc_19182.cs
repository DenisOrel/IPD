// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_19182
// Assembly: Intermech.TechAcad.Connector, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5A35A651-9A96-41F3-9839-2AAB5A952CB8
// Assembly location: D:\IPS\Client\Intermech.TechAcad.Connector.dll

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_19182
{
  private static byte[] sspq = new byte[372]
  {
    (byte) 82,
    (byte) 183,
    (byte) 158,
    (byte) 135,
    (byte) 246,
    (byte) 16 /*0x10*/,
    (byte) 42,
    (byte) 123,
    (byte) 228,
    (byte) 186,
    (byte) 213,
    (byte) 14,
    (byte) 100,
    (byte) 155,
    (byte) 252,
    (byte) 183,
    (byte) 206,
    (byte) 159,
    (byte) 97,
    (byte) 253,
    (byte) 54,
    (byte) 108,
    (byte) 11,
    (byte) 220,
    (byte) 73,
    (byte) 81,
    (byte) 178,
    (byte) 158,
    (byte) 72,
    (byte) 80 /*0x50*/,
    (byte) 158,
    (byte) 196,
    (byte) 251,
    (byte) 46,
    (byte) 246,
    (byte) 248,
    (byte) 72,
    (byte) 90,
    (byte) 44,
    (byte) 135,
    (byte) 17,
    (byte) 140,
    (byte) 131,
    (byte) 105,
    (byte) 144 /*0x90*/,
    (byte) 239,
    (byte) 57,
    (byte) 22,
    (byte) 137,
    (byte) 19,
    (byte) 223,
    (byte) 161,
    (byte) 176 /*0xB0*/,
    (byte) 107,
    (byte) 192 /*0xC0*/,
    (byte) 254,
    (byte) 0,
    (byte) 32 /*0x20*/,
    (byte) 114,
    (byte) 146,
    (byte) 184,
    (byte) 22,
    (byte) 169,
    (byte) 151,
    (byte) 105,
    (byte) 190,
    (byte) 46,
    (byte) 48 /*0x30*/,
    (byte) 198,
    (byte) 122,
    (byte) 133,
    (byte) 64 /*0x40*/,
    (byte) 21,
    (byte) 175,
    byte.MaxValue,
    (byte) 90,
    (byte) 130,
    (byte) 110,
    (byte) 17,
    (byte) 233,
    (byte) 7,
    (byte) 136,
    (byte) 90,
    (byte) 0,
    (byte) 2,
    (byte) 164,
    (byte) 235,
    (byte) 180,
    (byte) 37,
    (byte) 250,
    (byte) 118,
    (byte) 138,
    (byte) 33,
    (byte) 11,
    (byte) 92,
    (byte) 85,
    (byte) 88,
    (byte) 247,
    (byte) 8,
    (byte) 173,
    (byte) 82,
    (byte) 168,
    (byte) 100,
    (byte) 1,
    (byte) 206,
    (byte) 185,
    (byte) 32 /*0x20*/,
    (byte) 100,
    (byte) 17,
    (byte) 48 /*0x30*/,
    (byte) 41,
    (byte) 37,
    (byte) 13,
    (byte) 132,
    (byte) 237,
    (byte) 25,
    (byte) 64 /*0x40*/,
    (byte) 144 /*0x90*/,
    (byte) 95,
    (byte) 30,
    (byte) 28,
    (byte) 27,
    (byte) 132,
    (byte) 202,
    (byte) 173,
    (byte) 134,
    (byte) 252,
    (byte) 103,
    (byte) 149,
    (byte) 92,
    (byte) 8,
    (byte) 118,
    (byte) 14,
    (byte) 135,
    (byte) 177,
    (byte) 121,
    (byte) 24,
    (byte) 129,
    (byte) 207,
    (byte) 142,
    (byte) 211,
    (byte) 212,
    (byte) 227,
    (byte) 157,
    (byte) 17,
    (byte) 84,
    (byte) 210,
    (byte) 133,
    (byte) 253,
    (byte) 149,
    (byte) 112 /*0x70*/,
    (byte) 61,
    (byte) 121,
    (byte) 226,
    (byte) 146,
    (byte) 60,
    (byte) 51,
    (byte) 242,
    (byte) 19,
    (byte) 251,
    (byte) 24,
    (byte) 45,
    (byte) 238,
    (byte) 228,
    (byte) 49,
    (byte) 39,
    (byte) 161,
    (byte) 52,
    (byte) 178,
    (byte) 19,
    (byte) 100,
    (byte) 107,
    (byte) 222,
    (byte) 249,
    (byte) 16 /*0x10*/,
    (byte) 197,
    (byte) 125,
    (byte) 14,
    (byte) 148,
    (byte) 63 /*0x3F*/,
    (byte) 121,
    (byte) 111,
    (byte) 131,
    (byte) 13,
    (byte) 50,
    (byte) 112 /*0x70*/,
    (byte) 48 /*0x30*/,
    (byte) 32 /*0x20*/,
    (byte) 199,
    (byte) 241,
    (byte) 67,
    (byte) 116,
    (byte) 81,
    (byte) 178,
    (byte) 56,
    (byte) 158,
    (byte) 152,
    (byte) 216,
    (byte) 122,
    (byte) 138,
    (byte) 125,
    (byte) 230,
    (byte) 99,
    (byte) 126,
    (byte) 42,
    (byte) 4,
    (byte) 252,
    (byte) 223,
    (byte) 61,
    (byte) 118,
    (byte) 127 /*0x7F*/,
    (byte) 202,
    (byte) 115,
    (byte) 47,
    (byte) 97,
    (byte) 173,
    (byte) 87,
    (byte) 168,
    (byte) 156,
    (byte) 222,
    (byte) 73,
    (byte) 69,
    (byte) 31 /*0x1F*/,
    (byte) 64 /*0x40*/,
    (byte) 233,
    (byte) 24,
    (byte) 249,
    (byte) 253,
    (byte) 205,
    (byte) 110,
    (byte) 174,
    (byte) 253,
    (byte) 202,
    (byte) 150,
    (byte) 36,
    (byte) 202,
    (byte) 106,
    (byte) 124,
    (byte) 72,
    (byte) 178,
    (byte) 248,
    (byte) 141,
    (byte) 7,
    (byte) 166,
    (byte) 45,
    (byte) 130,
    (byte) 171,
    (byte) 92,
    (byte) 198,
    (byte) 182,
    (byte) 107,
    (byte) 110,
    (byte) 252,
    (byte) 39,
    (byte) 50,
    (byte) 177,
    (byte) 216,
    (byte) 85,
    (byte) 190,
    (byte) 149,
    (byte) 163,
    (byte) 175,
    (byte) 155,
    (byte) 71,
    (byte) 50,
    (byte) 231,
    (byte) 5,
    (byte) 47,
    (byte) 126,
    (byte) 139,
    (byte) 179,
    (byte) 10,
    (byte) 44,
    (byte) 201,
    (byte) 89,
    (byte) 186,
    (byte) 198,
    (byte) 22,
    (byte) 235,
    (byte) 45,
    (byte) 224 /*0xE0*/,
    (byte) 195,
    (byte) 226,
    (byte) 209,
    (byte) 247,
    (byte) 182,
    (byte) 13,
    (byte) 231,
    (byte) 137,
    (byte) 106,
    (byte) 194,
    (byte) 155,
    (byte) 51,
    (byte) 74,
    (byte) 245,
    (byte) 35,
    (byte) 109,
    (byte) 197,
    (byte) 106,
    (byte) 43,
    (byte) 154,
    (byte) 6,
    (byte) 149,
    (byte) 141,
    (byte) 35,
    (byte) 10,
    (byte) 152,
    (byte) 215,
    (byte) 66,
    (byte) 196,
    (byte) 118,
    (byte) 192 /*0xC0*/,
    (byte) 98,
    (byte) 20,
    (byte) 195,
    (byte) 167,
    (byte) 105,
    (byte) 184,
    (byte) 184,
    (byte) 218,
    (byte) 147,
    (byte) 156,
    (byte) 42,
    (byte) 44,
    (byte) 243,
    (byte) 10,
    (byte) 60,
    (byte) 39,
    (byte) 94,
    (byte) 113,
    (byte) 42,
    (byte) 49,
    (byte) 196,
    (byte) 174,
    (byte) 232,
    (byte) 45,
    (byte) 110,
    (byte) 185,
    (byte) 52,
    (byte) 40,
    (byte) 67,
    (byte) 23,
    (byte) 80 /*0x50*/,
    (byte) 173,
    (byte) 230,
    byte.MaxValue,
    (byte) 51,
    (byte) 9,
    (byte) 50,
    (byte) 68,
    (byte) 115,
    (byte) 240 /*0xF0*/,
    (byte) 127 /*0x7F*/,
    (byte) 30,
    (byte) 87,
    (byte) 69,
    (byte) 128 /*0x80*/,
    (byte) 241,
    (byte) 77,
    byte.MaxValue,
    (byte) 203,
    (byte) 111,
    (byte) 78,
    (byte) 84,
    (byte) 148,
    (byte) 18,
    (byte) 93,
    (byte) 6,
    (byte) 4,
    (byte) 92,
    (byte) 17,
    (byte) 139
  };
  private static byte[] sspr = new byte[372]
  {
    (byte) 115,
    (byte) 7,
    (byte) 31 /*0x1F*/,
    (byte) 86,
    (byte) 183,
    (byte) 152,
    (byte) 110,
    (byte) 68,
    (byte) 54,
    (byte) 67,
    (byte) 215,
    (byte) 225,
    (byte) 59,
    (byte) 65,
    (byte) 213,
    (byte) 192 /*0xC0*/,
    (byte) 94,
    (byte) 122,
    (byte) 184,
    (byte) 123,
    (byte) 73,
    (byte) 92,
    (byte) 125,
    (byte) 244,
    (byte) 161,
    (byte) 209,
    (byte) 169,
    (byte) 92,
    (byte) 196,
    (byte) 213,
    (byte) 105,
    (byte) 15,
    (byte) 95,
    (byte) 136,
    (byte) 109,
    (byte) 240 /*0xF0*/,
    (byte) 241,
    (byte) 222,
    (byte) 192 /*0xC0*/,
    (byte) 52,
    (byte) 102,
    (byte) 0,
    (byte) 219,
    (byte) 154,
    (byte) 220,
    (byte) 150,
    (byte) 250,
    (byte) 193,
    (byte) 204,
    (byte) 122,
    (byte) 27,
    (byte) 83,
    (byte) 93,
    (byte) 3,
    (byte) 28,
    (byte) 48 /*0x30*/,
    (byte) 160 /*0xA0*/,
    (byte) 190,
    (byte) 49,
    (byte) 196,
    (byte) 163,
    (byte) 195,
    (byte) 237,
    (byte) 70,
    (byte) 211,
    (byte) 164,
    (byte) 200,
    (byte) 228,
    (byte) 122,
    (byte) 157,
    (byte) 213,
    (byte) 127 /*0x7F*/,
    (byte) 115,
    (byte) 134,
    (byte) 35,
    (byte) 216,
    (byte) 225,
    (byte) 125,
    (byte) 172,
    (byte) 238,
    (byte) 26,
    (byte) 68,
    (byte) 41,
    (byte) 189,
    (byte) 176 /*0xB0*/,
    (byte) 10,
    (byte) 238,
    (byte) 228,
    (byte) 151,
    (byte) 45,
    (byte) 9,
    (byte) 253,
    (byte) 169,
    (byte) 39,
    (byte) 41,
    (byte) 107,
    (byte) 123,
    (byte) 109,
    (byte) 215,
    (byte) 73,
    (byte) 222,
    (byte) 4,
    (byte) 28,
    (byte) 133,
    (byte) 26,
    (byte) 228,
    (byte) 78,
    (byte) 200,
    (byte) 58,
    (byte) 6,
    (byte) 115,
    (byte) 65,
    (byte) 37,
    (byte) 72,
    (byte) 0,
    (byte) 205,
    (byte) 87,
    byte.MaxValue,
    (byte) 105,
    (byte) 219,
    (byte) 110,
    (byte) 64 /*0x40*/,
    (byte) 107,
    (byte) 90,
    (byte) 147,
    (byte) 30,
    (byte) 237,
    (byte) 142,
    (byte) 67,
    (byte) 127 /*0x7F*/,
    (byte) 224 /*0xE0*/,
    (byte) 136,
    (byte) 24,
    (byte) 190,
    (byte) 163,
    (byte) 121,
    (byte) 180,
    (byte) 125,
    (byte) 138,
    (byte) 178,
    (byte) 169,
    (byte) 207,
    (byte) 196,
    (byte) 18,
    (byte) 99,
    (byte) 135,
    (byte) 137,
    (byte) 157,
    (byte) 127 /*0x7F*/,
    (byte) 26,
    (byte) 80 /*0x50*/,
    (byte) 73,
    (byte) 39,
    (byte) 174,
    (byte) 117,
    (byte) 214,
    (byte) 190,
    (byte) 167,
    (byte) 198,
    (byte) 76,
    (byte) 11,
    (byte) 141,
    (byte) 121,
    (byte) 189,
    (byte) 11,
    (byte) 90,
    (byte) 153,
    (byte) 246,
    (byte) 136,
    (byte) 136,
    (byte) 45,
    (byte) 64 /*0x40*/,
    (byte) 236,
    (byte) 44,
    (byte) 71,
    (byte) 13,
    (byte) 151,
    (byte) 27,
    (byte) 240 /*0xF0*/,
    (byte) 216,
    (byte) 38,
    (byte) 136,
    (byte) 153,
    (byte) 39,
    (byte) 120,
    (byte) 49,
    (byte) 219,
    (byte) 191,
    (byte) 38,
    (byte) 224 /*0xE0*/,
    (byte) 220,
    (byte) 123,
    (byte) 40,
    (byte) 118,
    (byte) 212,
    (byte) 214,
    (byte) 252,
    (byte) 221,
    (byte) 253,
    (byte) 61,
    (byte) 139,
    (byte) 138,
    (byte) 76,
    (byte) 153,
    (byte) 88,
    (byte) 254,
    (byte) 246,
    (byte) 173,
    (byte) 189,
    (byte) 1,
    (byte) 157,
    (byte) 252,
    (byte) 36,
    (byte) 156,
    (byte) 68,
    (byte) 65,
    (byte) 226,
    (byte) 180,
    (byte) 138,
    (byte) 239,
    byte.MaxValue,
    (byte) 14,
    (byte) 56,
    (byte) 145,
    (byte) 155,
    (byte) 7,
    (byte) 18,
    (byte) 247,
    (byte) 59,
    (byte) 176 /*0xB0*/,
    (byte) 207,
    (byte) 141,
    (byte) 122,
    (byte) 236,
    (byte) 20,
    (byte) 18,
    (byte) 185,
    (byte) 8,
    (byte) 95,
    (byte) 98,
    (byte) 52,
    (byte) 56,
    (byte) 3,
    (byte) 220,
    (byte) 172,
    (byte) 207,
    (byte) 124,
    (byte) 212,
    (byte) 115,
    (byte) 241,
    (byte) 128 /*0x80*/,
    (byte) 216,
    (byte) 222,
    (byte) 221,
    (byte) 176 /*0xB0*/,
    (byte) 204,
    (byte) 196,
    (byte) 241,
    (byte) 95,
    (byte) 115,
    (byte) 149,
    (byte) 124,
    (byte) 191,
    (byte) 147,
    (byte) 82,
    (byte) 218,
    (byte) 62,
    (byte) 2,
    (byte) 70,
    (byte) 176 /*0xB0*/,
    (byte) 131,
    (byte) 140,
    (byte) 22,
    (byte) 192 /*0xC0*/,
    (byte) 2,
    (byte) 244,
    (byte) 231,
    (byte) 161,
    (byte) 214,
    (byte) 178,
    (byte) 30,
    (byte) 220,
    (byte) 9,
    (byte) 136,
    (byte) 41,
    (byte) 189,
    (byte) 114,
    (byte) 163,
    (byte) 209,
    (byte) 176 /*0xB0*/,
    (byte) 125,
    (byte) 97,
    (byte) 66,
    (byte) 5,
    (byte) 78,
    (byte) 73,
    (byte) 68,
    (byte) 82,
    (byte) 22,
    (byte) 154,
    (byte) 74,
    (byte) 213,
    (byte) 66,
    (byte) 0,
    (byte) 35,
    (byte) 244,
    (byte) 197,
    (byte) 20,
    (byte) 113,
    (byte) 246,
    (byte) 122,
    (byte) 180,
    (byte) 84,
    (byte) 139,
    (byte) 81,
    (byte) 236,
    (byte) 21,
    (byte) 177,
    (byte) 119,
    (byte) 57,
    (byte) 146,
    (byte) 89,
    (byte) 191,
    (byte) 45,
    (byte) 165,
    (byte) 74,
    (byte) 173,
    (byte) 0,
    (byte) 228,
    (byte) 46,
    (byte) 136,
    (byte) 239,
    (byte) 15,
    (byte) 180,
    (byte) 35,
    (byte) 133,
    (byte) 49,
    (byte) 119,
    (byte) 200,
    (byte) 27,
    (byte) 31 /*0x1F*/,
    (byte) 89,
    (byte) 227,
    (byte) 17,
    (byte) 179,
    (byte) 16 /*0x10*/,
    (byte) 249,
    (byte) 71,
    (byte) 80 /*0x50*/,
    (byte) 151,
    (byte) 240 /*0xF0*/,
    (byte) 48 /*0x30*/,
    (byte) 157,
    (byte) 64 /*0x40*/,
    (byte) 46,
    (byte) 64 /*0x40*/,
    (byte) 219,
    (byte) 19,
    (byte) 221,
    (byte) 197,
    (byte) 38,
    (byte) 254,
    (byte) 163,
    (byte) 82,
    (byte) 49,
    (byte) 154,
    (byte) 120,
    (byte) 206,
    (byte) 181,
    (byte) 95,
    (byte) 7,
    (byte) 55
  };

  internal static string ssp_techacad_19183()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[63 /*0x3F*/];
      byte[] numArray2 = new byte[55]
      {
        (byte) 150,
        (byte) 189,
        (byte) 198,
        (byte) 154,
        (byte) 102,
        (byte) 52,
        (byte) 171,
        (byte) 36,
        (byte) 217,
        (byte) 121,
        (byte) 34,
        (byte) 154,
        (byte) 38,
        (byte) 235,
        (byte) 230,
        (byte) 65,
        (byte) 99,
        (byte) 6,
        (byte) 25,
        (byte) 122,
        (byte) 191,
        (byte) 192 /*0xC0*/,
        (byte) 42,
        (byte) 234,
        (byte) 171,
        (byte) 16 /*0x10*/,
        (byte) 178,
        (byte) 70,
        (byte) 50,
        (byte) 160 /*0xA0*/,
        (byte) 178,
        (byte) 162,
        (byte) 24,
        (byte) 250,
        (byte) 68,
        (byte) 104,
        (byte) 35,
        (byte) 177,
        (byte) 196,
        (byte) 34,
        (byte) 191,
        (byte) 66,
        (byte) 243,
        (byte) 207,
        (byte) 89,
        (byte) 199,
        (byte) 90,
        (byte) 49,
        (byte) 217,
        (byte) 146,
        (byte) 236,
        (byte) 135,
        (byte) 239,
        (byte) 254,
        (byte) 180
      };
      byte[] numArray3 = new byte[55];
      numArray3[45] = (byte) 4;
      numArray3[38] = (byte) 51;
      numArray3[50] = (byte) 88;
      numArray3[3] = (byte) 45;
      numArray3[4] = (byte) 199;
      numArray3[29] = (byte) 97;
      numArray3[6] = (byte) 184;
      numArray3[53] = (byte) 194;
      numArray3[8] = (byte) 80 /*0x50*/;
      numArray3[13] = (byte) 92;
      numArray3[10] = (byte) 234;
      numArray3[11] = (byte) 218;
      numArray3[1] = (byte) 132;
      numArray3[7] = (byte) 16 /*0x10*/;
      numArray3[0] = (byte) 110;
      numArray3[47] = (byte) 113;
      numArray3[9] = (byte) 93;
      numArray3[46] = (byte) 45;
      numArray3[12] = (byte) 51;
      numArray3[40] = (byte) 118;
      numArray3[20] = (byte) 237;
      numArray3[21] = (byte) 203;
      numArray3[16 /*0x10*/] = (byte) 247;
      numArray3[19] = (byte) 210;
      numArray3[24] = (byte) 28;
      numArray3[25] = (byte) 62;
      numArray3[26] = (byte) 247;
      numArray3[32 /*0x20*/] = (byte) 162;
      numArray3[28] = (byte) 34;
      numArray3[15] = (byte) 232;
      numArray3[27] = (byte) 169;
      numArray3[5] = (byte) 219;
      numArray3[35] = (byte) 36;
      numArray3[33] = (byte) 163;
      numArray3[34] = (byte) 104;
      numArray3[37] = (byte) 220;
      numArray3[36] = (byte) 159;
      numArray3[14] = (byte) 95;
      numArray3[17] = (byte) 64 /*0x40*/;
      numArray3[39] = (byte) 140;
      numArray3[22] = (byte) 184;
      numArray3[41] = (byte) 149;
      numArray3[42] = (byte) 96 /*0x60*/;
      numArray3[23] = (byte) 47;
      numArray3[43] = (byte) 166;
      numArray3[49] = (byte) 161;
      numArray3[51] = (byte) 86;
      numArray3[44] = (byte) 37;
      numArray3[48 /*0x30*/] = (byte) 36;
      numArray3[18] = (byte) 121;
      numArray3[31 /*0x1F*/] = (byte) 104;
      numArray3[30] = (byte) 36;
      numArray3[52] = (byte) 251;
      numArray3[54] = (byte) 217;
      numArray3[2] = (byte) 172;
      key.Query(true, 357, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[8]
      {
        (byte) 75,
        (byte) 11,
        (byte) 159,
        (byte) 1,
        (byte) 248,
        (byte) 82,
        (byte) 132,
        (byte) 155
      };
      byte[] numArray5 = new byte[8];
      numArray5[4] = (byte) 31 /*0x1F*/;
      numArray5[1] = (byte) 200;
      numArray5[6] = (byte) 48 /*0x30*/;
      numArray5[3] = (byte) 62;
      numArray5[0] = (byte) 41;
      numArray5[5] = (byte) 203;
      numArray5[2] = (byte) 68;
      numArray5[7] = (byte) 55;
      key.Query(true, 357, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 8);
      for (int index = 0; index < 8; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[29];
      byte[] response = new byte[29];
      Array.Copy((Array) sc_19182.sspq, 0, (Array) numArray6, 0, 29);
      key.Query(true, 357, numArray6, response);
      Array.Copy((Array) sc_19182.sspr, 0, (Array) numArray6, 0, 29);
      for (int index = 0; index < numArray6.Length; ++index)
      {
        if ((int) numArray6[index] != (int) response[index])
        {
          key.TagValue = (int) response[index];
          break;
        }
      }
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray7 = new byte[63 /*0x3F*/];
    byte[] numArray8 = new byte[55]
    {
      (byte) 114,
      (byte) 160 /*0xA0*/,
      (byte) 226,
      (byte) 11,
      (byte) 248,
      (byte) 230,
      (byte) 147,
      (byte) 108,
      (byte) 24,
      (byte) 10,
      (byte) 231,
      (byte) 39,
      (byte) 9,
      (byte) 183,
      (byte) 18,
      (byte) 147,
      (byte) 214,
      (byte) 213,
      (byte) 13,
      (byte) 218,
      (byte) 57,
      (byte) 4,
      (byte) 184,
      byte.MaxValue,
      (byte) 45,
      (byte) 218,
      (byte) 230,
      (byte) 171,
      (byte) 193,
      (byte) 61,
      (byte) 165,
      (byte) 94,
      (byte) 140,
      (byte) 50,
      (byte) 114,
      (byte) 25,
      (byte) 243,
      (byte) 2,
      (byte) 107,
      (byte) 181,
      (byte) 104,
      (byte) 74,
      (byte) 182,
      (byte) 148,
      (byte) 121,
      (byte) 214,
      (byte) 45,
      (byte) 181,
      (byte) 248,
      (byte) 73,
      (byte) 114,
      (byte) 124,
      (byte) 164,
      (byte) 196,
      (byte) 14
    };
    byte[] numArray9 = new byte[55];
    numArray9[0] = (byte) 150;
    numArray9[38] = (byte) 178;
    numArray9[42] = (byte) 26;
    numArray9[9] = (byte) 61;
    numArray9[44] = (byte) 90;
    numArray9[5] = (byte) 16 /*0x10*/;
    numArray9[32 /*0x20*/] = (byte) 88;
    numArray9[7] = (byte) 24;
    numArray9[8] = (byte) 197;
    numArray9[26] = (byte) 230;
    numArray9[10] = (byte) 242;
    numArray9[6] = (byte) 165;
    numArray9[4] = (byte) 36;
    numArray9[23] = (byte) 25;
    numArray9[14] = (byte) 211;
    numArray9[15] = (byte) 86;
    numArray9[17] = (byte) 230;
    numArray9[27] = (byte) 207;
    numArray9[53] = (byte) 253;
    numArray9[19] = (byte) 22;
    numArray9[29] = (byte) 2;
    numArray9[21] = (byte) 209;
    numArray9[22] = (byte) 164;
    numArray9[1] = (byte) 54;
    numArray9[31 /*0x1F*/] = (byte) 149;
    numArray9[25] = (byte) 44;
    numArray9[18] = (byte) 1;
    numArray9[28] = (byte) 23;
    numArray9[13] = (byte) 92;
    numArray9[50] = (byte) 45;
    numArray9[30] = (byte) 208 /*0xD0*/;
    numArray9[43] = (byte) 180;
    numArray9[36] = (byte) 50;
    numArray9[33] = (byte) 88;
    numArray9[34] = (byte) 108;
    numArray9[35] = (byte) 60;
    numArray9[3] = (byte) 76;
    numArray9[37] = (byte) 89;
    numArray9[54] = (byte) 245;
    numArray9[39] = (byte) 124;
    numArray9[12] = (byte) 247;
    numArray9[41] = (byte) 18;
    numArray9[46] = (byte) 113;
    numArray9[24] = (byte) 162;
    numArray9[20] = (byte) 57;
    numArray9[45] = (byte) 107;
    numArray9[40] = (byte) 220;
    numArray9[47] = (byte) 197;
    numArray9[48 /*0x30*/] = (byte) 179;
    numArray9[49] = (byte) 82;
    numArray9[11] = (byte) 118;
    numArray9[51] = (byte) 226;
    numArray9[52] = (byte) 104;
    numArray9[16 /*0x10*/] = (byte) 140;
    numArray9[2] = (byte) 197;
    key.Query(true, 357, numArray8, numArray8);
    Array.Copy((Array) numArray8, 0, (Array) numArray7, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray7[index] ^= numArray9[index];
    byte[] numArray10 = new byte[8]
    {
      (byte) 91,
      (byte) 134,
      (byte) 113,
      (byte) 206,
      (byte) 52,
      (byte) 51,
      (byte) 15,
      (byte) 59
    };
    byte[] numArray11 = new byte[8]
    {
      (byte) 142,
      (byte) 204,
      (byte) 224 /*0xE0*/,
      (byte) 83,
      (byte) 71,
      (byte) 57,
      (byte) 113,
      (byte) 74
    };
    key.Query(true, 357, numArray10, numArray10);
    Array.Copy((Array) numArray10, 0, (Array) numArray7, 55, 8);
    for (int index = 0; index < 8; ++index)
      numArray7[index + 55] ^= numArray11[index];
    return Encoding.UTF8.GetString(numArray7);
  }

  internal static string ssp_techacad_19184()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[38];
      byte[] numArray2 = new byte[38]
      {
        (byte) 200,
        (byte) 142,
        (byte) 174,
        (byte) 27,
        (byte) 162,
        (byte) 239,
        (byte) 80 /*0x50*/,
        (byte) 114,
        (byte) 244,
        (byte) 150,
        (byte) 28,
        (byte) 84,
        (byte) 192 /*0xC0*/,
        (byte) 179,
        (byte) 205,
        (byte) 43,
        (byte) 22,
        (byte) 110,
        (byte) 116,
        (byte) 4,
        (byte) 227,
        (byte) 107,
        (byte) 181,
        (byte) 158,
        (byte) 220,
        (byte) 230,
        (byte) 76,
        (byte) 107,
        (byte) 203,
        (byte) 243,
        (byte) 33,
        (byte) 103,
        (byte) 165,
        (byte) 46,
        (byte) 55,
        (byte) 27,
        (byte) 240 /*0xF0*/,
        (byte) 247
      };
      byte[] numArray3 = new byte[38]
      {
        (byte) 14,
        (byte) 146,
        (byte) 201,
        (byte) 167,
        (byte) 249,
        (byte) 211,
        (byte) 11,
        (byte) 127 /*0x7F*/,
        (byte) 157,
        (byte) 176 /*0xB0*/,
        (byte) 86,
        (byte) 68,
        (byte) 252,
        (byte) 6,
        (byte) 69,
        (byte) 248,
        (byte) 219,
        (byte) 169,
        (byte) 185,
        (byte) 198,
        (byte) 65,
        (byte) 93,
        (byte) 97,
        (byte) 163,
        (byte) 90,
        (byte) 104,
        (byte) 174,
        (byte) 70,
        (byte) 215,
        (byte) 50,
        (byte) 164,
        (byte) 155,
        (byte) 239,
        (byte) 82,
        (byte) 227,
        (byte) 133,
        (byte) 72,
        (byte) 80 /*0x50*/
      };
      key.Query(true, 357, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 38);
      for (int index = 0; index < 38; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[38];
    byte[] numArray5 = new byte[38];
    numArray5[18] = (byte) 188;
    numArray5[1] = (byte) 209;
    numArray5[37] = (byte) 52;
    numArray5[3] = (byte) 105;
    numArray5[10] = (byte) 96 /*0x60*/;
    numArray5[28] = (byte) 177;
    numArray5[2] = (byte) 197;
    numArray5[7] = (byte) 101;
    numArray5[4] = (byte) 28;
    numArray5[9] = (byte) 37;
    numArray5[8] = (byte) 228;
    numArray5[11] = (byte) 94;
    numArray5[32 /*0x20*/] = (byte) 17;
    numArray5[14] = (byte) 92;
    numArray5[5] = (byte) 14;
    numArray5[15] = (byte) 216;
    numArray5[34] = (byte) 54;
    numArray5[17] = (byte) 63 /*0x3F*/;
    numArray5[33] = (byte) 53;
    numArray5[20] = (byte) 228;
    numArray5[35] = (byte) 154;
    numArray5[6] = (byte) 129;
    numArray5[22] = (byte) 0;
    numArray5[23] = (byte) 117;
    numArray5[24] = (byte) 180;
    numArray5[0] = (byte) 173;
    numArray5[26] = (byte) 145;
    numArray5[27] = (byte) 123;
    numArray5[13] = (byte) 93;
    numArray5[29] = (byte) 58;
    numArray5[30] = (byte) 31 /*0x1F*/;
    numArray5[31 /*0x1F*/] = (byte) 196;
    numArray5[21] = (byte) 167;
    numArray5[12] = (byte) 173;
    numArray5[25] = (byte) 175;
    numArray5[19] = (byte) 163;
    numArray5[36] = (byte) 240 /*0xF0*/;
    numArray5[16 /*0x10*/] = (byte) 15;
    byte[] numArray6 = new byte[38];
    numArray6[32 /*0x20*/] = (byte) 219;
    numArray6[1] = (byte) 193;
    numArray6[2] = (byte) 235;
    numArray6[6] = (byte) 71;
    numArray6[7] = (byte) 194;
    numArray6[5] = (byte) 215;
    numArray6[23] = (byte) 250;
    numArray6[4] = (byte) 114;
    numArray6[11] = (byte) 155;
    numArray6[18] = (byte) 219;
    numArray6[10] = (byte) 74;
    numArray6[25] = (byte) 126;
    numArray6[12] = (byte) 200;
    numArray6[13] = (byte) 138;
    numArray6[17] = (byte) 132;
    numArray6[16 /*0x10*/] = (byte) 44;
    numArray6[3] = (byte) 100;
    numArray6[36] = (byte) 22;
    numArray6[19] = (byte) 227;
    numArray6[24] = (byte) 150;
    numArray6[8] = (byte) 115;
    numArray6[21] = (byte) 150;
    numArray6[22] = (byte) 169;
    numArray6[0] = (byte) 71;
    numArray6[35] = (byte) 209;
    numArray6[14] = (byte) 231;
    numArray6[26] = (byte) 210;
    numArray6[27] = (byte) 101;
    numArray6[28] = (byte) 105;
    numArray6[9] = (byte) 144 /*0x90*/;
    numArray6[30] = (byte) 103;
    numArray6[29] = (byte) 89;
    numArray6[15] = (byte) 92;
    numArray6[33] = (byte) 235;
    numArray6[34] = (byte) 26;
    numArray6[31 /*0x1F*/] = (byte) 89;
    numArray6[20] = (byte) 58;
    numArray6[37] = (byte) 112 /*0x70*/;
    key.Query(true, 357, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 38);
    for (int index = 0; index < 38; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[21];
    byte[] response = new byte[21];
    Array.Copy((Array) sc_19182.sspq, 29, (Array) numArray7, 0, 21);
    key.Query(true, 357, numArray7, response);
    Array.Copy((Array) sc_19182.sspr, 29, (Array) numArray7, 0, 21);
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

  internal static string ssp_techacad_19185()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[34];
      byte[] numArray2 = new byte[34];
      numArray2[12] = (byte) 236;
      numArray2[8] = (byte) 158;
      numArray2[2] = (byte) 105;
      numArray2[3] = (byte) 62;
      numArray2[0] = (byte) 46;
      numArray2[5] = (byte) 43;
      numArray2[6] = (byte) 4;
      numArray2[32 /*0x20*/] = (byte) 50;
      numArray2[31 /*0x1F*/] = (byte) 218;
      numArray2[9] = (byte) 160 /*0xA0*/;
      numArray2[22] = (byte) 130;
      numArray2[11] = (byte) 12;
      numArray2[7] = (byte) 118;
      numArray2[1] = (byte) 205;
      numArray2[29] = (byte) 122;
      numArray2[15] = (byte) 192 /*0xC0*/;
      numArray2[16 /*0x10*/] = (byte) 80 /*0x50*/;
      numArray2[28] = (byte) 25;
      numArray2[18] = (byte) 31 /*0x1F*/;
      numArray2[19] = (byte) 18;
      numArray2[24] = (byte) 127 /*0x7F*/;
      numArray2[21] = (byte) 12;
      numArray2[10] = (byte) 98;
      numArray2[13] = (byte) 41;
      numArray2[27] = (byte) 10;
      numArray2[25] = (byte) 193;
      numArray2[26] = (byte) 53;
      numArray2[17] = (byte) 179;
      numArray2[20] = (byte) 171;
      numArray2[23] = (byte) 214;
      numArray2[30] = (byte) 165;
      numArray2[14] = (byte) 95;
      numArray2[33] = (byte) 253;
      numArray2[4] = (byte) 235;
      byte[] numArray3 = new byte[34]
      {
        (byte) 141,
        (byte) 199,
        (byte) 155,
        (byte) 227,
        (byte) 237,
        (byte) 250,
        (byte) 157,
        (byte) 47,
        (byte) 93,
        (byte) 7,
        (byte) 4,
        (byte) 198,
        (byte) 136,
        (byte) 117,
        (byte) 226,
        (byte) 28,
        (byte) 43,
        (byte) 179,
        (byte) 139,
        byte.MaxValue,
        (byte) 34,
        (byte) 177,
        (byte) 73,
        (byte) 52,
        (byte) 159,
        (byte) 212,
        (byte) 194,
        (byte) 53,
        (byte) 93,
        (byte) 114,
        (byte) 51,
        (byte) 194,
        (byte) 9,
        (byte) 254
      };
      key.Query(true, 357, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 34);
      for (int index = 0; index < 34; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[34];
    byte[] numArray5 = new byte[34]
    {
      (byte) 96 /*0x60*/,
      (byte) 168,
      (byte) 82,
      (byte) 206,
      (byte) 199,
      (byte) 127 /*0x7F*/,
      (byte) 155,
      (byte) 39,
      byte.MaxValue,
      (byte) 42,
      (byte) 245,
      (byte) 217,
      (byte) 234,
      (byte) 145,
      (byte) 66,
      (byte) 113,
      (byte) 49,
      (byte) 86,
      (byte) 79,
      (byte) 170,
      byte.MaxValue,
      (byte) 47,
      (byte) 225,
      (byte) 215,
      (byte) 73,
      (byte) 135,
      (byte) 106,
      (byte) 236,
      (byte) 39,
      (byte) 164,
      (byte) 168,
      (byte) 134,
      (byte) 199,
      (byte) 1
    };
    byte[] numArray6 = new byte[34]
    {
      (byte) 159,
      (byte) 100,
      (byte) 23,
      (byte) 220,
      (byte) 217,
      (byte) 101,
      (byte) 49,
      (byte) 116,
      (byte) 63 /*0x3F*/,
      (byte) 247,
      (byte) 1,
      (byte) 214,
      (byte) 4,
      (byte) 70,
      (byte) 101,
      (byte) 198,
      (byte) 55,
      (byte) 244,
      (byte) 188,
      (byte) 63 /*0x3F*/,
      (byte) 241,
      (byte) 103,
      (byte) 12,
      (byte) 244,
      (byte) 227,
      (byte) 44,
      (byte) 21,
      (byte) 10,
      (byte) 217,
      (byte) 10,
      (byte) 8,
      (byte) 73,
      (byte) 218,
      (byte) 121
    };
    key.Query(true, 357, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 34);
    for (int index = 0; index < 34; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[34];
    byte[] response = new byte[34];
    Array.Copy((Array) sc_19182.sspq, 50, (Array) numArray7, 0, 34);
    key.Query(true, 357, numArray7, response);
    Array.Copy((Array) sc_19182.sspr, 50, (Array) numArray7, 0, 34);
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

  internal static string ssp_techacad_19186()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[34];
      byte[] numArray2 = new byte[34]
      {
        (byte) 173,
        (byte) 250,
        (byte) 147,
        (byte) 111,
        (byte) 226,
        (byte) 215,
        (byte) 40,
        (byte) 20,
        (byte) 215,
        (byte) 52,
        (byte) 17,
        (byte) 251,
        (byte) 17,
        (byte) 29,
        (byte) 9,
        (byte) 210,
        (byte) 88,
        (byte) 243,
        (byte) 156,
        (byte) 172,
        (byte) 159,
        (byte) 140,
        (byte) 1,
        (byte) 108,
        (byte) 101,
        (byte) 124,
        (byte) 146,
        (byte) 15,
        (byte) 137,
        (byte) 138,
        (byte) 47,
        (byte) 187,
        (byte) 169,
        (byte) 31 /*0x1F*/
      };
      byte[] numArray3 = new byte[34]
      {
        (byte) 93,
        (byte) 96 /*0x60*/,
        (byte) 233,
        (byte) 224 /*0xE0*/,
        (byte) 119,
        (byte) 44,
        (byte) 93,
        (byte) 17,
        (byte) 217,
        (byte) 135,
        (byte) 92,
        (byte) 180,
        (byte) 44,
        (byte) 170,
        (byte) 140,
        (byte) 73,
        (byte) 74,
        (byte) 34,
        (byte) 13,
        (byte) 95,
        (byte) 102,
        (byte) 82,
        (byte) 184,
        (byte) 156,
        (byte) 228,
        (byte) 217,
        (byte) 48 /*0x30*/,
        (byte) 130,
        (byte) 245,
        (byte) 152,
        (byte) 160 /*0xA0*/,
        (byte) 144 /*0x90*/,
        (byte) 178,
        (byte) 92
      };
      key.Query(true, 357, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 34);
      for (int index = 0; index < 34; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[34];
    byte[] numArray5 = new byte[34]
    {
      (byte) 193,
      (byte) 228,
      (byte) 163,
      (byte) 92,
      (byte) 43,
      (byte) 61,
      (byte) 122,
      (byte) 238,
      (byte) 94,
      (byte) 113,
      (byte) 81,
      (byte) 7,
      (byte) 22,
      (byte) 175,
      (byte) 148,
      (byte) 190,
      (byte) 83,
      (byte) 216,
      (byte) 32 /*0x20*/,
      (byte) 248,
      (byte) 38,
      (byte) 9,
      (byte) 102,
      (byte) 68,
      (byte) 25,
      (byte) 139,
      (byte) 77,
      (byte) 4,
      (byte) 85,
      (byte) 82,
      (byte) 241,
      (byte) 96 /*0x60*/,
      (byte) 68,
      (byte) 55
    };
    byte[] numArray6 = new byte[34]
    {
      (byte) 151,
      (byte) 24,
      (byte) 229,
      (byte) 58,
      (byte) 181,
      (byte) 109,
      (byte) 134,
      (byte) 127 /*0x7F*/,
      (byte) 103,
      (byte) 145,
      (byte) 147,
      (byte) 209,
      (byte) 35,
      (byte) 6,
      (byte) 195,
      (byte) 13,
      (byte) 173,
      (byte) 14,
      (byte) 115,
      (byte) 100,
      (byte) 139,
      (byte) 206,
      (byte) 170,
      (byte) 115,
      (byte) 95,
      (byte) 17,
      (byte) 236,
      (byte) 166,
      (byte) 125,
      (byte) 109,
      (byte) 85,
      (byte) 109,
      (byte) 32 /*0x20*/,
      (byte) 131
    };
    key.Query(true, 357, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 34);
    for (int index = 0; index < 34; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_techacad_19187()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[35];
      byte[] numArray2 = new byte[35]
      {
        (byte) 116,
        (byte) 79,
        (byte) 134,
        (byte) 126,
        (byte) 6,
        (byte) 11,
        (byte) 65,
        (byte) 7,
        (byte) 114,
        (byte) 219,
        (byte) 154,
        (byte) 253,
        (byte) 85,
        (byte) 71,
        (byte) 155,
        (byte) 203,
        (byte) 116,
        (byte) 183,
        (byte) 125,
        (byte) 94,
        (byte) 66,
        (byte) 159,
        (byte) 240 /*0xF0*/,
        (byte) 118,
        (byte) 152,
        (byte) 12,
        (byte) 128 /*0x80*/,
        (byte) 240 /*0xF0*/,
        (byte) 195,
        (byte) 223,
        (byte) 23,
        (byte) 133,
        (byte) 197,
        (byte) 3,
        (byte) 99
      };
      byte[] numArray3 = new byte[35]
      {
        (byte) 7,
        (byte) 134,
        (byte) 132,
        (byte) 36,
        (byte) 87,
        (byte) 70,
        (byte) 99,
        (byte) 165,
        (byte) 181,
        (byte) 239,
        (byte) 190,
        (byte) 178,
        (byte) 17,
        (byte) 120,
        (byte) 193,
        (byte) 47,
        (byte) 137,
        (byte) 17,
        (byte) 27,
        (byte) 116,
        (byte) 120,
        (byte) 172,
        (byte) 159,
        (byte) 76,
        (byte) 121,
        (byte) 115,
        (byte) 157,
        (byte) 150,
        (byte) 77,
        (byte) 186,
        (byte) 194,
        (byte) 124,
        (byte) 40,
        (byte) 202,
        (byte) 81
      };
      key.Query(true, 357, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 35);
      for (int index = 0; index < 35; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[35];
    byte[] numArray5 = new byte[35]
    {
      (byte) 2,
      (byte) 91,
      (byte) 3,
      (byte) 17,
      (byte) 24,
      (byte) 114,
      (byte) 105,
      (byte) 88,
      (byte) 253,
      (byte) 169,
      (byte) 197,
      (byte) 121,
      (byte) 194,
      (byte) 223,
      (byte) 145,
      (byte) 153,
      (byte) 13,
      (byte) 212,
      (byte) 95,
      (byte) 15,
      (byte) 229,
      (byte) 250,
      (byte) 139,
      (byte) 80 /*0x50*/,
      (byte) 68,
      (byte) 147,
      (byte) 195,
      (byte) 21,
      (byte) 55,
      (byte) 82,
      (byte) 228,
      (byte) 202,
      (byte) 89,
      (byte) 203,
      (byte) 181
    };
    byte[] numArray6 = new byte[35];
    numArray6[20] = (byte) 49;
    numArray6[1] = (byte) 9;
    numArray6[11] = (byte) 69;
    numArray6[2] = (byte) 73;
    numArray6[6] = (byte) 127 /*0x7F*/;
    numArray6[5] = (byte) 224 /*0xE0*/;
    numArray6[24] = (byte) 101;
    numArray6[21] = (byte) 90;
    numArray6[16 /*0x10*/] = (byte) 97;
    numArray6[9] = (byte) 38;
    numArray6[28] = (byte) 39;
    numArray6[4] = (byte) 82;
    numArray6[12] = (byte) 110;
    numArray6[33] = (byte) 13;
    numArray6[14] = (byte) 141;
    numArray6[15] = (byte) 214;
    numArray6[0] = (byte) 51;
    numArray6[17] = (byte) 98;
    numArray6[18] = (byte) 50;
    numArray6[19] = (byte) 173;
    numArray6[32 /*0x20*/] = (byte) 97;
    numArray6[3] = (byte) 101;
    numArray6[22] = (byte) 250;
    numArray6[23] = (byte) 100;
    numArray6[29] = (byte) 8;
    numArray6[25] = (byte) 223;
    numArray6[7] = (byte) 25;
    numArray6[27] = (byte) 198;
    numArray6[13] = (byte) 91;
    numArray6[10] = (byte) 78;
    numArray6[8] = (byte) 129;
    numArray6[31 /*0x1F*/] = (byte) 57;
    numArray6[26] = (byte) 105;
    numArray6[30] = (byte) 171;
    numArray6[34] = (byte) 57;
    key.Query(true, 357, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 35);
    for (int index = 0; index < 35; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[38];
    byte[] response = new byte[38];
    Array.Copy((Array) sc_19182.sspq, 84, (Array) numArray7, 0, 38);
    key.Query(true, 357, numArray7, response);
    Array.Copy((Array) sc_19182.sspr, 84, (Array) numArray7, 0, 38);
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

  internal static string ssp_techacad_19188()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[35];
      byte[] numArray2 = new byte[35];
      numArray2[0] = (byte) 209;
      numArray2[1] = (byte) 14;
      numArray2[2] = (byte) 36;
      numArray2[3] = (byte) 50;
      numArray2[4] = (byte) 21;
      numArray2[28] = (byte) 200;
      numArray2[19] = (byte) 162;
      numArray2[12] = (byte) 150;
      numArray2[8] = (byte) 40;
      numArray2[9] = (byte) 238;
      numArray2[23] = (byte) 124;
      numArray2[22] = (byte) 80 /*0x50*/;
      numArray2[25] = (byte) 29;
      numArray2[13] = (byte) 119;
      numArray2[16 /*0x10*/] = (byte) 73;
      numArray2[17] = (byte) 132;
      numArray2[21] = (byte) 209;
      numArray2[29] = (byte) 151;
      numArray2[10] = (byte) 125;
      numArray2[15] = (byte) 19;
      numArray2[20] = (byte) 245;
      numArray2[14] = (byte) 76;
      numArray2[31 /*0x1F*/] = (byte) 144 /*0x90*/;
      numArray2[18] = (byte) 134;
      numArray2[5] = (byte) 78;
      numArray2[24] = (byte) 156;
      numArray2[27] = (byte) 20;
      numArray2[34] = (byte) 146;
      numArray2[32 /*0x20*/] = (byte) 54;
      numArray2[30] = (byte) 234;
      numArray2[26] = (byte) 66;
      numArray2[6] = (byte) 111;
      numArray2[7] = (byte) 161;
      numArray2[33] = (byte) 27;
      numArray2[11] = (byte) 28;
      byte[] numArray3 = new byte[35]
      {
        (byte) 14,
        (byte) 85,
        (byte) 26,
        (byte) 124,
        (byte) 230,
        (byte) 237,
        (byte) 252,
        (byte) 99,
        (byte) 114,
        (byte) 91,
        (byte) 252,
        (byte) 156,
        (byte) 27,
        (byte) 100,
        (byte) 56,
        (byte) 214,
        (byte) 40,
        (byte) 156,
        (byte) 173,
        (byte) 65,
        (byte) 217,
        (byte) 254,
        (byte) 232,
        (byte) 86,
        (byte) 161,
        (byte) 219,
        (byte) 7,
        (byte) 230,
        (byte) 176 /*0xB0*/,
        (byte) 229,
        (byte) 218,
        (byte) 209,
        (byte) 97,
        (byte) 199,
        (byte) 133
      };
      key.Query(true, 357, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 35);
      for (int index = 0; index < 35; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[35];
    byte[] numArray5 = new byte[35];
    numArray5[32 /*0x20*/] = (byte) 81;
    numArray5[1] = byte.MaxValue;
    numArray5[2] = (byte) 101;
    numArray5[4] = (byte) 25;
    numArray5[14] = (byte) 188;
    numArray5[25] = (byte) 50;
    numArray5[6] = (byte) 53;
    numArray5[27] = (byte) 129;
    numArray5[8] = (byte) 159;
    numArray5[9] = (byte) 31 /*0x1F*/;
    numArray5[10] = (byte) 75;
    numArray5[11] = (byte) 130;
    numArray5[12] = (byte) 80 /*0x50*/;
    numArray5[13] = (byte) 250;
    numArray5[5] = (byte) 236;
    numArray5[20] = (byte) 19;
    numArray5[16 /*0x10*/] = (byte) 145;
    numArray5[17] = (byte) 52;
    numArray5[26] = (byte) 164;
    numArray5[28] = (byte) 219;
    numArray5[22] = (byte) 70;
    numArray5[21] = (byte) 9;
    numArray5[3] = (byte) 207;
    numArray5[0] = (byte) 113;
    numArray5[19] = (byte) 240 /*0xF0*/;
    numArray5[24] = (byte) 112 /*0x70*/;
    numArray5[15] = (byte) 131;
    numArray5[31 /*0x1F*/] = (byte) 20;
    numArray5[18] = (byte) 3;
    numArray5[29] = (byte) 190;
    numArray5[30] = (byte) 154;
    numArray5[7] = (byte) 198;
    numArray5[33] = (byte) 31 /*0x1F*/;
    numArray5[23] = (byte) 182;
    numArray5[34] = (byte) 36;
    byte[] numArray6 = new byte[35];
    numArray6[3] = (byte) 244;
    numArray6[1] = (byte) 41;
    numArray6[8] = (byte) 200;
    numArray6[33] = (byte) 132;
    numArray6[4] = (byte) 164;
    numArray6[17] = (byte) 40;
    numArray6[31 /*0x1F*/] = (byte) 145;
    numArray6[7] = (byte) 173;
    numArray6[19] = (byte) 175;
    numArray6[16 /*0x10*/] = (byte) 228;
    numArray6[10] = (byte) 68;
    numArray6[11] = (byte) 80 /*0x50*/;
    numArray6[12] = (byte) 209;
    numArray6[27] = (byte) 173;
    numArray6[28] = (byte) 86;
    numArray6[6] = (byte) 109;
    numArray6[15] = (byte) 218;
    numArray6[20] = (byte) 183;
    numArray6[2] = (byte) 8;
    numArray6[5] = (byte) 43;
    numArray6[21] = (byte) 217;
    numArray6[29] = (byte) 114;
    numArray6[22] = (byte) 68;
    numArray6[23] = (byte) 154;
    numArray6[24] = (byte) 91;
    numArray6[25] = (byte) 231;
    numArray6[26] = (byte) 25;
    numArray6[0] = (byte) 144 /*0x90*/;
    numArray6[18] = (byte) 68;
    numArray6[13] = (byte) 90;
    numArray6[30] = (byte) 230;
    numArray6[14] = (byte) 248;
    numArray6[32 /*0x20*/] = (byte) 181;
    numArray6[9] = (byte) 151;
    numArray6[34] = (byte) 205;
    key.Query(true, 357, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 35);
    for (int index = 0; index < 35; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[38];
    byte[] response = new byte[38];
    Array.Copy((Array) sc_19182.sspq, 122, (Array) numArray7, 0, 38);
    key.Query(true, 357, numArray7, response);
    Array.Copy((Array) sc_19182.sspr, 122, (Array) numArray7, 0, 38);
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

  internal static string ssp_techacad_19189()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[43];
      byte[] numArray2 = new byte[43]
      {
        (byte) 86,
        (byte) 221,
        (byte) 80 /*0x50*/,
        (byte) 61,
        (byte) 21,
        (byte) 167,
        (byte) 207,
        (byte) 44,
        (byte) 7,
        (byte) 4,
        (byte) 101,
        (byte) 214,
        (byte) 99,
        (byte) 192 /*0xC0*/,
        (byte) 72,
        (byte) 95,
        (byte) 231,
        (byte) 185,
        (byte) 88,
        (byte) 180,
        (byte) 119,
        (byte) 105,
        byte.MaxValue,
        (byte) 236,
        (byte) 202,
        (byte) 252,
        (byte) 44,
        (byte) 244,
        (byte) 116,
        (byte) 70,
        (byte) 37,
        (byte) 110,
        (byte) 212,
        (byte) 184,
        (byte) 125,
        (byte) 246,
        (byte) 146,
        (byte) 96 /*0x60*/,
        (byte) 252,
        (byte) 172,
        (byte) 2,
        (byte) 88,
        (byte) 246
      };
      byte[] numArray3 = new byte[43];
      numArray3[31 /*0x1F*/] = (byte) 100;
      numArray3[38] = (byte) 23;
      numArray3[11] = (byte) 223;
      numArray3[3] = (byte) 0;
      numArray3[4] = (byte) 153;
      numArray3[27] = (byte) 185;
      numArray3[15] = (byte) 184;
      numArray3[23] = (byte) 162;
      numArray3[8] = (byte) 184;
      numArray3[9] = (byte) 115;
      numArray3[10] = (byte) 158;
      numArray3[16 /*0x10*/] = (byte) 95;
      numArray3[12] = (byte) 111;
      numArray3[13] = (byte) 243;
      numArray3[7] = (byte) 212;
      numArray3[26] = (byte) 151;
      numArray3[22] = (byte) 47;
      numArray3[19] = (byte) 168;
      numArray3[14] = (byte) 214;
      numArray3[1] = (byte) 224 /*0xE0*/;
      numArray3[20] = (byte) 163;
      numArray3[6] = (byte) 9;
      numArray3[17] = (byte) 63 /*0x3F*/;
      numArray3[24] = (byte) 46;
      numArray3[5] = (byte) 184;
      numArray3[42] = (byte) 52;
      numArray3[39] = (byte) 23;
      numArray3[2] = (byte) 162;
      numArray3[28] = (byte) 248;
      numArray3[29] = (byte) 246;
      numArray3[30] = (byte) 74;
      numArray3[36] = (byte) 43;
      numArray3[32 /*0x20*/] = (byte) 237;
      numArray3[33] = (byte) 49;
      numArray3[34] = (byte) 13;
      numArray3[35] = (byte) 159;
      numArray3[21] = (byte) 145;
      numArray3[37] = (byte) 66;
      numArray3[40] = (byte) 245;
      numArray3[0] = (byte) 66;
      numArray3[18] = (byte) 114;
      numArray3[41] = (byte) 122;
      numArray3[25] = (byte) 155;
      key.Query(true, 357, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 43);
      for (int index = 0; index < 43; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[41];
      byte[] response = new byte[41];
      Array.Copy((Array) sc_19182.sspq, 160 /*0xA0*/, (Array) numArray4, 0, 41);
      key.Query(true, 357, numArray4, response);
      Array.Copy((Array) sc_19182.sspr, 160 /*0xA0*/, (Array) numArray4, 0, 41);
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
    byte[] numArray5 = new byte[43];
    byte[] numArray6 = new byte[43]
    {
      (byte) 62,
      (byte) 187,
      (byte) 205,
      (byte) 0,
      (byte) 123,
      (byte) 44,
      (byte) 176 /*0xB0*/,
      (byte) 135,
      (byte) 52,
      (byte) 111,
      (byte) 165,
      (byte) 249,
      (byte) 26,
      (byte) 23,
      (byte) 169,
      (byte) 170,
      (byte) 224 /*0xE0*/,
      (byte) 177,
      (byte) 192 /*0xC0*/,
      (byte) 246,
      (byte) 128 /*0x80*/,
      (byte) 109,
      (byte) 193,
      (byte) 208 /*0xD0*/,
      (byte) 239,
      (byte) 25,
      (byte) 43,
      (byte) 193,
      (byte) 113,
      (byte) 45,
      (byte) 164,
      (byte) 147,
      (byte) 10,
      (byte) 194,
      (byte) 58,
      (byte) 179,
      (byte) 182,
      (byte) 13,
      (byte) 182,
      (byte) 68,
      (byte) 128 /*0x80*/,
      (byte) 152,
      (byte) 147
    };
    byte[] numArray7 = new byte[43];
    numArray7[25] = (byte) 219;
    numArray7[1] = (byte) 53;
    numArray7[42] = (byte) 164;
    numArray7[24] = (byte) 58;
    numArray7[10] = (byte) 52;
    numArray7[5] = (byte) 63 /*0x3F*/;
    numArray7[6] = (byte) 30;
    numArray7[7] = (byte) 233;
    numArray7[8] = (byte) 201;
    numArray7[9] = (byte) 184;
    numArray7[41] = (byte) 164;
    numArray7[11] = (byte) 217;
    numArray7[12] = (byte) 37;
    numArray7[13] = (byte) 198;
    numArray7[14] = (byte) 47;
    numArray7[27] = (byte) 58;
    numArray7[16 /*0x10*/] = (byte) 52;
    numArray7[17] = (byte) 121;
    numArray7[22] = (byte) 139;
    numArray7[23] = (byte) 108;
    numArray7[20] = (byte) 220;
    numArray7[21] = (byte) 128 /*0x80*/;
    numArray7[19] = (byte) 127 /*0x7F*/;
    numArray7[33] = (byte) 192 /*0xC0*/;
    numArray7[35] = (byte) 223;
    numArray7[0] = (byte) 212;
    numArray7[26] = (byte) 11;
    numArray7[40] = (byte) 181;
    numArray7[31 /*0x1F*/] = (byte) 195;
    numArray7[18] = (byte) 170;
    numArray7[29] = (byte) 62;
    numArray7[36] = (byte) 55;
    numArray7[28] = (byte) 177;
    numArray7[4] = (byte) 2;
    numArray7[34] = (byte) 20;
    numArray7[37] = (byte) 205;
    numArray7[2] = (byte) 91;
    numArray7[39] = (byte) 37;
    numArray7[38] = (byte) 28;
    numArray7[32 /*0x20*/] = (byte) 112 /*0x70*/;
    numArray7[3] = (byte) 98;
    numArray7[15] = (byte) 128 /*0x80*/;
    numArray7[30] = (byte) 35;
    key.Query(true, 357, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 43);
    for (int index = 0; index < 43; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_techacad_19190()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[43];
      byte[] numArray2 = new byte[43]
      {
        (byte) 185,
        (byte) 185,
        (byte) 252,
        (byte) 85,
        (byte) 67,
        (byte) 144 /*0x90*/,
        (byte) 130,
        (byte) 140,
        (byte) 183,
        (byte) 139,
        (byte) 140,
        (byte) 223,
        (byte) 189,
        (byte) 152,
        byte.MaxValue,
        (byte) 228,
        (byte) 42,
        (byte) 61,
        (byte) 28,
        (byte) 20,
        (byte) 247,
        (byte) 187,
        (byte) 201,
        (byte) 156,
        (byte) 126,
        (byte) 111,
        (byte) 188,
        (byte) 153,
        (byte) 145,
        (byte) 76,
        (byte) 56,
        (byte) 120,
        (byte) 146,
        (byte) 116,
        (byte) 60,
        (byte) 59,
        (byte) 214,
        (byte) 165,
        (byte) 57,
        (byte) 229,
        (byte) 101,
        (byte) 77,
        (byte) 134
      };
      byte[] numArray3 = new byte[43];
      numArray3[38] = (byte) 189;
      numArray3[16 /*0x10*/] = (byte) 60;
      numArray3[10] = (byte) 182;
      numArray3[41] = (byte) 31 /*0x1F*/;
      numArray3[27] = (byte) 175;
      numArray3[34] = (byte) 121;
      numArray3[6] = (byte) 153;
      numArray3[7] = (byte) 69;
      numArray3[4] = (byte) 22;
      numArray3[9] = (byte) 94;
      numArray3[33] = (byte) 189;
      numArray3[11] = (byte) 151;
      numArray3[40] = (byte) 213;
      numArray3[13] = (byte) 28;
      numArray3[0] = (byte) 98;
      numArray3[30] = (byte) 172;
      numArray3[8] = (byte) 178;
      numArray3[17] = (byte) 161;
      numArray3[18] = (byte) 124;
      numArray3[19] = (byte) 65;
      numArray3[28] = (byte) 140;
      numArray3[1] = (byte) 24;
      numArray3[20] = (byte) 36;
      numArray3[23] = (byte) 219;
      numArray3[24] = (byte) 75;
      numArray3[25] = (byte) 107;
      numArray3[22] = (byte) 122;
      numArray3[3] = (byte) 39;
      numArray3[15] = (byte) 249;
      numArray3[29] = (byte) 22;
      numArray3[5] = (byte) 157;
      numArray3[31 /*0x1F*/] = (byte) 98;
      numArray3[32 /*0x20*/] = (byte) 35;
      numArray3[26] = (byte) 22;
      numArray3[39] = (byte) 95;
      numArray3[35] = (byte) 198;
      numArray3[21] = (byte) 115;
      numArray3[37] = (byte) 144 /*0x90*/;
      numArray3[14] = (byte) 199;
      numArray3[36] = (byte) 217;
      numArray3[12] = (byte) 28;
      numArray3[2] = (byte) 242;
      numArray3[42] = (byte) 197;
      key.Query(true, 357, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 43);
      for (int index = 0; index < 43; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[43];
    byte[] numArray5 = new byte[43]
    {
      (byte) 63 /*0x3F*/,
      (byte) 76,
      (byte) 117,
      (byte) 207,
      (byte) 101,
      (byte) 17,
      (byte) 123,
      (byte) 82,
      (byte) 27,
      (byte) 83,
      (byte) 211,
      (byte) 182,
      (byte) 138,
      (byte) 61,
      (byte) 184,
      (byte) 130,
      (byte) 206,
      (byte) 151,
      (byte) 34,
      (byte) 1,
      (byte) 198,
      (byte) 200,
      (byte) 204,
      (byte) 171,
      (byte) 91,
      (byte) 54,
      (byte) 176 /*0xB0*/,
      (byte) 205,
      (byte) 250,
      (byte) 114,
      (byte) 182,
      (byte) 245,
      (byte) 108,
      (byte) 69,
      (byte) 29,
      (byte) 120,
      (byte) 152,
      (byte) 181,
      (byte) 158,
      (byte) 37,
      (byte) 51,
      (byte) 87,
      (byte) 25
    };
    byte[] numArray6 = new byte[43]
    {
      (byte) 78,
      (byte) 249,
      (byte) 49,
      (byte) 35,
      (byte) 1,
      (byte) 186,
      (byte) 186,
      (byte) 174,
      (byte) 44,
      (byte) 37,
      (byte) 253,
      (byte) 165,
      (byte) 95,
      (byte) 46,
      (byte) 170,
      (byte) 218,
      (byte) 79,
      (byte) 159,
      (byte) 78,
      (byte) 231,
      (byte) 173,
      (byte) 35,
      (byte) 77,
      (byte) 161,
      (byte) 247,
      (byte) 244,
      (byte) 42,
      (byte) 221,
      (byte) 84,
      (byte) 74,
      (byte) 144 /*0x90*/,
      (byte) 195,
      (byte) 157,
      (byte) 112 /*0x70*/,
      (byte) 3,
      (byte) 188,
      (byte) 130,
      (byte) 21,
      (byte) 249,
      (byte) 178,
      (byte) 112 /*0x70*/,
      (byte) 117,
      (byte) 44
    };
    key.Query(true, 357, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 43);
    for (int index = 0; index < 43; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[43];
    byte[] response = new byte[43];
    Array.Copy((Array) sc_19182.sspq, 201, (Array) numArray7, 0, 43);
    key.Query(true, 357, numArray7, response);
    Array.Copy((Array) sc_19182.sspr, 201, (Array) numArray7, 0, 43);
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

  internal static string ssp_techacad_19191()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[44];
      byte[] numArray2 = new byte[44]
      {
        (byte) 206,
        (byte) 66,
        (byte) 111,
        (byte) 196,
        (byte) 134,
        (byte) 0,
        (byte) 112 /*0x70*/,
        (byte) 50,
        (byte) 2,
        (byte) 123,
        (byte) 205,
        (byte) 198,
        (byte) 35,
        (byte) 148,
        (byte) 17,
        (byte) 10,
        byte.MaxValue,
        (byte) 17,
        (byte) 206,
        (byte) 199,
        (byte) 130,
        (byte) 0,
        (byte) 221,
        (byte) 11,
        (byte) 150,
        (byte) 67,
        (byte) 218,
        (byte) 96 /*0x60*/,
        (byte) 234,
        (byte) 230,
        (byte) 188,
        (byte) 61,
        (byte) 121,
        (byte) 71,
        (byte) 27,
        (byte) 214,
        (byte) 138,
        (byte) 56,
        (byte) 71,
        (byte) 110,
        (byte) 62,
        (byte) 202,
        (byte) 199,
        (byte) 27
      };
      byte[] numArray3 = new byte[44]
      {
        (byte) 17,
        (byte) 87,
        (byte) 124,
        (byte) 249,
        (byte) 116,
        (byte) 249,
        (byte) 180,
        (byte) 205,
        (byte) 103,
        (byte) 231,
        (byte) 28,
        (byte) 4,
        (byte) 95,
        (byte) 111,
        (byte) 122,
        (byte) 131,
        (byte) 112 /*0x70*/,
        (byte) 231,
        (byte) 101,
        (byte) 31 /*0x1F*/,
        (byte) 66,
        (byte) 254,
        (byte) 56,
        (byte) 8,
        (byte) 140,
        (byte) 157,
        (byte) 210,
        (byte) 17,
        (byte) 150,
        (byte) 74,
        (byte) 152,
        (byte) 139,
        (byte) 8,
        (byte) 134,
        (byte) 186,
        (byte) 109,
        (byte) 131,
        (byte) 101,
        (byte) 155,
        (byte) 75,
        (byte) 234,
        (byte) 198,
        (byte) 132,
        (byte) 169
      };
      key.Query(true, 357, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 44);
      for (int index = 0; index < 44; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[44];
    byte[] numArray5 = new byte[44]
    {
      (byte) 231,
      (byte) 55,
      (byte) 167,
      (byte) 249,
      (byte) 123,
      (byte) 56,
      (byte) 222,
      (byte) 118,
      (byte) 205,
      (byte) 101,
      (byte) 100,
      (byte) 45,
      (byte) 67,
      (byte) 37,
      (byte) 16 /*0x10*/,
      (byte) 71,
      (byte) 28,
      (byte) 190,
      (byte) 144 /*0x90*/,
      (byte) 77,
      (byte) 219,
      (byte) 121,
      (byte) 58,
      (byte) 128 /*0x80*/,
      (byte) 198,
      (byte) 202,
      (byte) 96 /*0x60*/,
      (byte) 137,
      (byte) 183,
      (byte) 56,
      (byte) 193,
      (byte) 180,
      (byte) 146,
      (byte) 251,
      (byte) 242,
      (byte) 40,
      (byte) 66,
      (byte) 236,
      (byte) 64 /*0x40*/,
      (byte) 4,
      (byte) 154,
      (byte) 209,
      (byte) 77,
      (byte) 113
    };
    byte[] numArray6 = new byte[44]
    {
      (byte) 50,
      (byte) 183,
      (byte) 183,
      (byte) 236,
      (byte) 73,
      (byte) 232,
      (byte) 216,
      (byte) 245,
      (byte) 76,
      (byte) 110,
      (byte) 140,
      (byte) 133,
      (byte) 15,
      (byte) 19,
      (byte) 14,
      (byte) 12,
      (byte) 129,
      (byte) 82,
      (byte) 205,
      (byte) 231,
      (byte) 82,
      (byte) 108,
      (byte) 11,
      (byte) 152,
      (byte) 52,
      (byte) 168,
      (byte) 64 /*0x40*/,
      (byte) 87,
      (byte) 149,
      (byte) 250,
      (byte) 81,
      (byte) 192 /*0xC0*/,
      (byte) 101,
      (byte) 148,
      (byte) 81,
      (byte) 124,
      (byte) 7,
      (byte) 37,
      (byte) 47,
      (byte) 124,
      (byte) 125,
      (byte) 218,
      (byte) 252,
      (byte) 89
    };
    key.Query(true, 357, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 44);
    for (int index = 0; index < 44; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[34];
    byte[] response = new byte[34];
    Array.Copy((Array) sc_19182.sspq, 244, (Array) numArray7, 0, 34);
    key.Query(true, 357, numArray7, response);
    Array.Copy((Array) sc_19182.sspr, 244, (Array) numArray7, 0, 34);
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

  internal static string ssp_techacad_19192()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[40];
      byte[] numArray2 = new byte[40]
      {
        (byte) 53,
        (byte) 141,
        (byte) 101,
        (byte) 238,
        (byte) 219,
        (byte) 143,
        (byte) 132,
        (byte) 44,
        (byte) 27,
        (byte) 185,
        (byte) 143,
        (byte) 44,
        (byte) 68,
        (byte) 19,
        (byte) 183,
        (byte) 206,
        (byte) 119,
        (byte) 112 /*0x70*/,
        (byte) 188,
        (byte) 23,
        (byte) 69,
        (byte) 125,
        (byte) 124,
        (byte) 222,
        (byte) 58,
        (byte) 3,
        (byte) 97,
        (byte) 65,
        (byte) 51,
        (byte) 205,
        (byte) 119,
        (byte) 235,
        (byte) 194,
        (byte) 249,
        (byte) 137,
        (byte) 8,
        (byte) 61,
        (byte) 192 /*0xC0*/,
        (byte) 191,
        (byte) 183
      };
      byte[] numArray3 = new byte[40];
      numArray3[2] = (byte) 71;
      numArray3[9] = (byte) 142;
      numArray3[38] = (byte) 23;
      numArray3[3] = (byte) 125;
      numArray3[29] = (byte) 36;
      numArray3[26] = (byte) 198;
      numArray3[6] = (byte) 63 /*0x3F*/;
      numArray3[22] = (byte) 83;
      numArray3[30] = (byte) 126;
      numArray3[33] = (byte) 157;
      numArray3[10] = (byte) 41;
      numArray3[11] = (byte) 215;
      numArray3[32 /*0x20*/] = (byte) 20;
      numArray3[13] = (byte) 15;
      numArray3[39] = (byte) 141;
      numArray3[15] = (byte) 154;
      numArray3[25] = (byte) 125;
      numArray3[17] = (byte) 239;
      numArray3[18] = (byte) 19;
      numArray3[19] = (byte) 235;
      numArray3[21] = (byte) 24;
      numArray3[14] = (byte) 125;
      numArray3[1] = (byte) 233;
      numArray3[23] = (byte) 205;
      numArray3[24] = (byte) 185;
      numArray3[20] = (byte) 208 /*0xD0*/;
      numArray3[16 /*0x10*/] = (byte) 220;
      numArray3[27] = (byte) 243;
      numArray3[31 /*0x1F*/] = (byte) 19;
      numArray3[12] = (byte) 226;
      numArray3[28] = (byte) 88;
      numArray3[5] = (byte) 213;
      numArray3[0] = (byte) 35;
      numArray3[8] = (byte) 136;
      numArray3[4] = (byte) 113;
      numArray3[35] = (byte) 107;
      numArray3[36] = (byte) 48 /*0x30*/;
      numArray3[37] = (byte) 128 /*0x80*/;
      numArray3[34] = (byte) 21;
      numArray3[7] = (byte) 209;
      key.Query(true, 357, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 40);
      for (int index = 0; index < 40; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[42];
      byte[] response = new byte[42];
      Array.Copy((Array) sc_19182.sspq, 278, (Array) numArray4, 0, 42);
      key.Query(true, 357, numArray4, response);
      Array.Copy((Array) sc_19182.sspr, 278, (Array) numArray4, 0, 42);
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
    byte[] numArray5 = new byte[40];
    byte[] numArray6 = new byte[40];
    numArray6[3] = byte.MaxValue;
    numArray6[11] = (byte) 7;
    numArray6[28] = (byte) 135;
    numArray6[2] = (byte) 123;
    numArray6[7] = (byte) 164;
    numArray6[5] = (byte) 248;
    numArray6[34] = (byte) 130;
    numArray6[16 /*0x10*/] = (byte) 45;
    numArray6[13] = (byte) 191;
    numArray6[38] = (byte) 198;
    numArray6[10] = (byte) 252;
    numArray6[33] = (byte) 144 /*0x90*/;
    numArray6[18] = (byte) 174;
    numArray6[30] = (byte) 185;
    numArray6[14] = (byte) 147;
    numArray6[15] = (byte) 218;
    numArray6[21] = (byte) 53;
    numArray6[17] = (byte) 85;
    numArray6[35] = (byte) 209;
    numArray6[19] = (byte) 236;
    numArray6[1] = (byte) 200;
    numArray6[22] = (byte) 165;
    numArray6[8] = (byte) 230;
    numArray6[9] = (byte) 113;
    numArray6[4] = (byte) 198;
    numArray6[25] = (byte) 2;
    numArray6[26] = (byte) 56;
    numArray6[27] = (byte) 30;
    numArray6[20] = (byte) 30;
    numArray6[29] = (byte) 77;
    numArray6[12] = (byte) 206;
    numArray6[31 /*0x1F*/] = (byte) 180;
    numArray6[32 /*0x20*/] = (byte) 12;
    numArray6[6] = (byte) 211;
    numArray6[0] = (byte) 169;
    numArray6[24] = (byte) 243;
    numArray6[36] = (byte) 139;
    numArray6[37] = (byte) 149;
    numArray6[23] = (byte) 196;
    numArray6[39] = (byte) 42;
    byte[] numArray7 = new byte[40]
    {
      (byte) 192 /*0xC0*/,
      (byte) 94,
      (byte) 230,
      (byte) 135,
      (byte) 243,
      (byte) 191,
      (byte) 124,
      (byte) 246,
      (byte) 110,
      (byte) 141,
      (byte) 193,
      (byte) 74,
      (byte) 35,
      (byte) 132,
      (byte) 9,
      (byte) 241,
      (byte) 74,
      (byte) 235,
      (byte) 198,
      (byte) 122,
      (byte) 26,
      (byte) 77,
      (byte) 63 /*0x3F*/,
      (byte) 27,
      (byte) 101,
      (byte) 243,
      (byte) 98,
      (byte) 166,
      (byte) 207,
      (byte) 228,
      (byte) 230,
      (byte) 106,
      (byte) 211,
      (byte) 103,
      (byte) 250,
      (byte) 251,
      (byte) 76,
      (byte) 29,
      (byte) 209,
      (byte) 190
    };
    key.Query(true, 357, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 40);
    for (int index = 0; index < 40; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_techacad_19193()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[50];
      byte[] numArray2 = new byte[50]
      {
        (byte) 212,
        (byte) 166,
        (byte) 170,
        (byte) 65,
        (byte) 212,
        (byte) 14,
        (byte) 173,
        (byte) 212,
        (byte) 137,
        (byte) 1,
        (byte) 105,
        (byte) 130,
        (byte) 214,
        (byte) 88,
        (byte) 25,
        (byte) 128 /*0x80*/,
        (byte) 115,
        (byte) 179,
        (byte) 103,
        (byte) 191,
        (byte) 154,
        (byte) 224 /*0xE0*/,
        (byte) 132,
        (byte) 134,
        (byte) 21,
        (byte) 145,
        (byte) 47,
        (byte) 79,
        (byte) 187,
        (byte) 134,
        (byte) 179,
        (byte) 44,
        (byte) 23,
        (byte) 127 /*0x7F*/,
        (byte) 196,
        (byte) 17,
        (byte) 156,
        (byte) 185,
        (byte) 106,
        (byte) 46,
        (byte) 20,
        (byte) 123,
        (byte) 15,
        (byte) 141,
        (byte) 26,
        (byte) 51,
        (byte) 83,
        (byte) 236,
        (byte) 186,
        (byte) 122
      };
      byte[] numArray3 = new byte[50];
      numArray3[48 /*0x30*/] = (byte) 128 /*0x80*/;
      numArray3[9] = (byte) 191;
      numArray3[21] = (byte) 212;
      numArray3[15] = (byte) 109;
      numArray3[46] = (byte) 195;
      numArray3[5] = (byte) 193;
      numArray3[6] = (byte) 126;
      numArray3[7] = (byte) 50;
      numArray3[8] = (byte) 57;
      numArray3[0] = (byte) 3;
      numArray3[23] = (byte) 12;
      numArray3[29] = (byte) 33;
      numArray3[41] = (byte) 196;
      numArray3[13] = (byte) 224 /*0xE0*/;
      numArray3[19] = (byte) 199;
      numArray3[3] = (byte) 228;
      numArray3[1] = (byte) 187;
      numArray3[42] = (byte) 232;
      numArray3[40] = (byte) 32 /*0x20*/;
      numArray3[2] = (byte) 78;
      numArray3[20] = (byte) 113;
      numArray3[12] = (byte) 237;
      numArray3[45] = (byte) 139;
      numArray3[4] = (byte) 152;
      numArray3[31 /*0x1F*/] = (byte) 223;
      numArray3[25] = (byte) 231;
      numArray3[35] = (byte) 48 /*0x30*/;
      numArray3[27] = (byte) 245;
      numArray3[38] = (byte) 13;
      numArray3[24] = (byte) 48 /*0x30*/;
      numArray3[30] = (byte) 142;
      numArray3[36] = (byte) 208 /*0xD0*/;
      numArray3[32 /*0x20*/] = (byte) 102;
      numArray3[28] = (byte) 168;
      numArray3[34] = (byte) 149;
      numArray3[10] = (byte) 199;
      numArray3[16 /*0x10*/] = (byte) 72;
      numArray3[37] = (byte) 57;
      numArray3[44] = (byte) 115;
      numArray3[39] = (byte) 141;
      numArray3[33] = (byte) 234;
      numArray3[18] = (byte) 184;
      numArray3[11] = (byte) 233;
      numArray3[43] = (byte) 167;
      numArray3[26] = (byte) 230;
      numArray3[14] = (byte) 106;
      numArray3[17] = (byte) 235;
      numArray3[47] = (byte) 217;
      numArray3[22] = (byte) 130;
      numArray3[49] = (byte) 60;
      key.Query(true, 357, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 50);
      for (int index = 0; index < 50; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[50];
    byte[] numArray5 = new byte[50]
    {
      (byte) 179,
      (byte) 150,
      (byte) 87,
      (byte) 20,
      (byte) 244,
      (byte) 50,
      (byte) 228,
      (byte) 34,
      (byte) 42,
      (byte) 185,
      (byte) 73,
      (byte) 5,
      (byte) 154,
      (byte) 201,
      (byte) 216,
      (byte) 140,
      (byte) 249,
      (byte) 62,
      (byte) 48 /*0x30*/,
      (byte) 219,
      (byte) 196,
      (byte) 23,
      (byte) 108,
      (byte) 43,
      (byte) 33,
      (byte) 233,
      (byte) 245,
      (byte) 188,
      (byte) 196,
      (byte) 184,
      (byte) 142,
      (byte) 24,
      (byte) 64 /*0x40*/,
      (byte) 235,
      (byte) 225,
      (byte) 170,
      (byte) 29,
      (byte) 64 /*0x40*/,
      (byte) 10,
      (byte) 7,
      (byte) 151,
      (byte) 172,
      (byte) 198,
      (byte) 190,
      (byte) 68,
      (byte) 33,
      (byte) 181,
      (byte) 236,
      (byte) 138,
      (byte) 128 /*0x80*/
    };
    byte[] numArray6 = new byte[50]
    {
      (byte) 53,
      (byte) 242,
      (byte) 250,
      (byte) 36,
      (byte) 198,
      (byte) 184,
      (byte) 218,
      (byte) 88,
      (byte) 58,
      (byte) 207,
      (byte) 192 /*0xC0*/,
      (byte) 108,
      (byte) 93,
      (byte) 126,
      (byte) 121,
      (byte) 101,
      (byte) 28,
      (byte) 115,
      (byte) 30,
      (byte) 141,
      (byte) 144 /*0x90*/,
      (byte) 224 /*0xE0*/,
      (byte) 124,
      (byte) 139,
      (byte) 213,
      (byte) 206,
      (byte) 227,
      (byte) 233,
      (byte) 203,
      (byte) 143,
      (byte) 77,
      (byte) 209,
      (byte) 76,
      (byte) 201,
      (byte) 94,
      (byte) 83,
      (byte) 174,
      (byte) 166,
      (byte) 137,
      (byte) 70,
      (byte) 9,
      (byte) 51,
      (byte) 114,
      (byte) 235,
      (byte) 194,
      (byte) 86,
      (byte) 54,
      (byte) 42,
      (byte) 5,
      (byte) 4
    };
    key.Query(true, 357, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 50);
    for (int index = 0; index < 50; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[52];
    byte[] response = new byte[52];
    Array.Copy((Array) sc_19182.sspq, 320, (Array) numArray7, 0, 52);
    key.Query(true, 357, numArray7, response);
    Array.Copy((Array) sc_19182.sspr, 320, (Array) numArray7, 0, 52);
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
}
