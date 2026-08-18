// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_13302
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_13302
{
  private static byte[] sspq = new byte[322]
  {
    (byte) 160 /*0xA0*/,
    (byte) 117,
    (byte) 109,
    (byte) 194,
    (byte) 19,
    (byte) 219,
    (byte) 27,
    (byte) 4,
    (byte) 253,
    (byte) 184,
    (byte) 74,
    (byte) 222,
    (byte) 247,
    (byte) 129,
    (byte) 16 /*0x10*/,
    (byte) 153,
    (byte) 42,
    (byte) 87,
    (byte) 129,
    (byte) 243,
    (byte) 243,
    (byte) 39,
    (byte) 96 /*0x60*/,
    (byte) 164,
    (byte) 242,
    (byte) 120,
    (byte) 77,
    (byte) 136,
    (byte) 253,
    (byte) 66,
    (byte) 50,
    (byte) 116,
    (byte) 158,
    (byte) 12,
    (byte) 61,
    (byte) 119,
    (byte) 179,
    (byte) 199,
    (byte) 75,
    (byte) 207,
    (byte) 227,
    (byte) 94,
    (byte) 49,
    (byte) 197,
    (byte) 144 /*0x90*/,
    (byte) 103,
    (byte) 161,
    (byte) 114,
    (byte) 201,
    (byte) 217,
    (byte) 57,
    (byte) 70,
    (byte) 38,
    (byte) 104,
    (byte) 252,
    (byte) 37,
    (byte) 60,
    (byte) 19,
    (byte) 116,
    (byte) 251,
    (byte) 81,
    (byte) 18,
    (byte) 81,
    (byte) 24,
    (byte) 233,
    (byte) 170,
    (byte) 220,
    (byte) 82,
    (byte) 137,
    (byte) 29,
    (byte) 82,
    (byte) 18,
    (byte) 36,
    (byte) 217,
    (byte) 96 /*0x60*/,
    (byte) 61,
    (byte) 124,
    (byte) 147,
    (byte) 225,
    (byte) 202,
    (byte) 80 /*0x50*/,
    (byte) 209,
    (byte) 217,
    (byte) 232,
    (byte) 190,
    (byte) 189,
    (byte) 169,
    (byte) 26,
    (byte) 234,
    (byte) 47,
    (byte) 220,
    (byte) 220,
    (byte) 94,
    (byte) 96 /*0x60*/,
    (byte) 208 /*0xD0*/,
    (byte) 22,
    (byte) 4,
    (byte) 59,
    (byte) 208 /*0xD0*/,
    (byte) 29,
    (byte) 131,
    (byte) 178,
    (byte) 104,
    (byte) 141,
    (byte) 73,
    (byte) 216,
    (byte) 34,
    (byte) 101,
    (byte) 58,
    (byte) 33,
    (byte) 132,
    (byte) 106,
    (byte) 160 /*0xA0*/,
    (byte) 192 /*0xC0*/,
    (byte) 1,
    (byte) 198,
    (byte) 148,
    (byte) 14,
    (byte) 25,
    (byte) 88,
    (byte) 59,
    (byte) 130,
    (byte) 197,
    (byte) 133,
    (byte) 247,
    (byte) 202,
    (byte) 138,
    (byte) 36,
    (byte) 71,
    (byte) 34,
    (byte) 199,
    (byte) 238,
    (byte) 192 /*0xC0*/,
    (byte) 50,
    (byte) 237,
    (byte) 237,
    (byte) 183,
    (byte) 155,
    (byte) 81,
    (byte) 63 /*0x3F*/,
    (byte) 112 /*0x70*/,
    (byte) 171,
    (byte) 12,
    (byte) 44,
    (byte) 213,
    (byte) 192 /*0xC0*/,
    (byte) 142,
    (byte) 139,
    (byte) 66,
    (byte) 149,
    (byte) 70,
    (byte) 121,
    (byte) 197,
    (byte) 80 /*0x50*/,
    (byte) 148,
    (byte) 124,
    (byte) 50,
    (byte) 18,
    byte.MaxValue,
    (byte) 157,
    (byte) 209,
    (byte) 213,
    (byte) 130,
    (byte) 147,
    (byte) 124,
    (byte) 20,
    (byte) 4,
    (byte) 133,
    (byte) 1,
    (byte) 56,
    (byte) 40,
    (byte) 253,
    (byte) 238,
    (byte) 6,
    (byte) 231,
    (byte) 172,
    (byte) 126,
    (byte) 38,
    (byte) 219,
    (byte) 226,
    (byte) 139,
    (byte) 180,
    (byte) 126,
    (byte) 204,
    (byte) 77,
    (byte) 6,
    (byte) 140,
    (byte) 165,
    (byte) 248,
    (byte) 12,
    (byte) 224 /*0xE0*/,
    (byte) 18,
    (byte) 221,
    (byte) 165,
    (byte) 94,
    (byte) 63 /*0x3F*/,
    (byte) 185,
    (byte) 62,
    (byte) 223,
    (byte) 162,
    (byte) 45,
    (byte) 227,
    (byte) 113,
    (byte) 29,
    (byte) 48 /*0x30*/,
    (byte) 70,
    (byte) 194,
    (byte) 195,
    (byte) 81,
    (byte) 238,
    (byte) 196,
    (byte) 244,
    (byte) 153,
    (byte) 47,
    (byte) 247,
    (byte) 195,
    (byte) 230,
    (byte) 37,
    (byte) 237,
    (byte) 9,
    (byte) 123,
    (byte) 73,
    (byte) 42,
    (byte) 110,
    (byte) 99,
    (byte) 88,
    (byte) 182,
    (byte) 101,
    (byte) 50,
    (byte) 146,
    (byte) 247,
    (byte) 130,
    (byte) 169,
    (byte) 240 /*0xF0*/,
    (byte) 73,
    (byte) 112 /*0x70*/,
    (byte) 121,
    (byte) 96 /*0x60*/,
    (byte) 170,
    (byte) 182,
    (byte) 6,
    (byte) 137,
    (byte) 91,
    (byte) 23,
    (byte) 51,
    (byte) 152,
    (byte) 210,
    (byte) 216,
    (byte) 93,
    (byte) 89,
    (byte) 10,
    (byte) 237,
    (byte) 184,
    (byte) 169,
    (byte) 47,
    (byte) 205,
    (byte) 47,
    (byte) 17,
    (byte) 86,
    (byte) 245,
    (byte) 207,
    (byte) 117,
    (byte) 97,
    (byte) 103,
    (byte) 237,
    (byte) 225,
    (byte) 212,
    (byte) 93,
    (byte) 45,
    (byte) 218,
    (byte) 189,
    (byte) 65,
    (byte) 61,
    (byte) 188,
    (byte) 107,
    (byte) 247,
    (byte) 184,
    (byte) 0,
    (byte) 243,
    (byte) 129,
    (byte) 3,
    (byte) 64 /*0x40*/,
    (byte) 252,
    (byte) 15,
    (byte) 137,
    (byte) 217,
    (byte) 33,
    (byte) 226,
    (byte) 131,
    (byte) 129,
    (byte) 4,
    (byte) 242,
    (byte) 160 /*0xA0*/,
    (byte) 68,
    (byte) 82,
    (byte) 181,
    (byte) 254,
    (byte) 204,
    (byte) 54,
    (byte) 132,
    (byte) 50,
    (byte) 16 /*0x10*/,
    (byte) 206,
    (byte) 19,
    (byte) 196,
    (byte) 197,
    (byte) 152,
    (byte) 91,
    (byte) 49,
    (byte) 87,
    (byte) 210,
    (byte) 204,
    (byte) 77,
    (byte) 188,
    (byte) 98,
    (byte) 78,
    (byte) 201,
    (byte) 159,
    (byte) 132,
    (byte) 44,
    (byte) 13,
    (byte) 237
  };
  private static byte[] sspr = new byte[322]
  {
    (byte) 29,
    (byte) 125,
    (byte) 164,
    (byte) 166,
    (byte) 206,
    (byte) 200,
    (byte) 35,
    (byte) 120,
    (byte) 175,
    (byte) 87,
    (byte) 192 /*0xC0*/,
    (byte) 30,
    (byte) 56,
    (byte) 225,
    (byte) 225,
    (byte) 124,
    (byte) 175,
    (byte) 4,
    (byte) 130,
    (byte) 3,
    (byte) 133,
    (byte) 174,
    (byte) 235,
    (byte) 9,
    (byte) 148,
    (byte) 22,
    (byte) 190,
    (byte) 248,
    (byte) 87,
    (byte) 143,
    (byte) 63 /*0x3F*/,
    (byte) 159,
    (byte) 55,
    (byte) 146,
    (byte) 191,
    (byte) 150,
    (byte) 76,
    (byte) 20,
    (byte) 79,
    (byte) 184,
    (byte) 86,
    (byte) 16 /*0x10*/,
    (byte) 68,
    (byte) 231,
    (byte) 91,
    (byte) 189,
    (byte) 163,
    (byte) 240 /*0xF0*/,
    (byte) 121,
    (byte) 55,
    (byte) 249,
    (byte) 228,
    (byte) 39,
    (byte) 198,
    (byte) 59,
    (byte) 18,
    (byte) 19,
    (byte) 89,
    (byte) 227,
    (byte) 35,
    (byte) 223,
    (byte) 79,
    (byte) 246,
    (byte) 69,
    (byte) 28,
    (byte) 66,
    (byte) 246,
    (byte) 203,
    (byte) 233,
    (byte) 26,
    (byte) 207,
    (byte) 14,
    (byte) 85,
    (byte) 9,
    (byte) 224 /*0xE0*/,
    (byte) 136,
    (byte) 50,
    (byte) 176 /*0xB0*/,
    (byte) 75,
    (byte) 155,
    (byte) 242,
    (byte) 62,
    (byte) 220,
    (byte) 161,
    (byte) 248,
    (byte) 126,
    (byte) 246,
    (byte) 79,
    (byte) 34,
    (byte) 178,
    (byte) 229,
    (byte) 154,
    (byte) 81,
    (byte) 147,
    (byte) 204,
    (byte) 77,
    (byte) 199,
    (byte) 215,
    (byte) 126,
    (byte) 237,
    (byte) 6,
    (byte) 75,
    (byte) 130,
    (byte) 235,
    (byte) 65,
    (byte) 167,
    (byte) 100,
    (byte) 129,
    (byte) 73,
    (byte) 137,
    (byte) 162,
    (byte) 98,
    (byte) 127 /*0x7F*/,
    (byte) 35,
    (byte) 185,
    (byte) 229,
    (byte) 33,
    (byte) 40,
    (byte) 154,
    (byte) 97,
    (byte) 145,
    (byte) 225,
    (byte) 33,
    (byte) 241,
    (byte) 210,
    (byte) 3,
    (byte) 249,
    (byte) 238,
    (byte) 6,
    (byte) 220,
    (byte) 87,
    (byte) 100,
    (byte) 91,
    (byte) 152,
    (byte) 110,
    (byte) 148,
    (byte) 154,
    (byte) 47,
    (byte) 62,
    (byte) 97,
    (byte) 224 /*0xE0*/,
    (byte) 54,
    (byte) 68,
    (byte) 156,
    (byte) 110,
    (byte) 26,
    (byte) 217,
    (byte) 149,
    (byte) 5,
    (byte) 124,
    (byte) 194,
    (byte) 174,
    (byte) 148,
    (byte) 142,
    (byte) 39,
    (byte) 240 /*0xF0*/,
    (byte) 203,
    (byte) 252,
    (byte) 224 /*0xE0*/,
    (byte) 67,
    (byte) 43,
    (byte) 45,
    (byte) 140,
    (byte) 102,
    (byte) 209,
    (byte) 91,
    (byte) 187,
    (byte) 221,
    (byte) 186,
    (byte) 193,
    (byte) 212,
    (byte) 117,
    (byte) 114,
    (byte) 76,
    (byte) 80 /*0x50*/,
    (byte) 114,
    (byte) 214,
    (byte) 83,
    (byte) 39,
    (byte) 194,
    (byte) 238,
    (byte) 95,
    (byte) 117,
    (byte) 199,
    (byte) 109,
    (byte) 159,
    (byte) 20,
    (byte) 178,
    (byte) 229,
    (byte) 161,
    (byte) 112 /*0x70*/,
    (byte) 63 /*0x3F*/,
    (byte) 105,
    (byte) 68,
    (byte) 102,
    (byte) 136,
    (byte) 58,
    (byte) 224 /*0xE0*/,
    (byte) 163,
    (byte) 206,
    (byte) 45,
    (byte) 81,
    (byte) 232,
    (byte) 56,
    (byte) 208 /*0xD0*/,
    (byte) 40,
    (byte) 177,
    (byte) 2,
    (byte) 30,
    (byte) 72,
    (byte) 9,
    (byte) 203,
    (byte) 53,
    (byte) 148,
    (byte) 106,
    (byte) 148,
    (byte) 125,
    (byte) 23,
    (byte) 180,
    (byte) 231,
    (byte) 2,
    (byte) 157,
    (byte) 89,
    (byte) 145,
    (byte) 106,
    (byte) 36,
    (byte) 89,
    (byte) 34,
    (byte) 75,
    (byte) 24,
    (byte) 226,
    (byte) 0,
    (byte) 130,
    (byte) 24,
    (byte) 133,
    (byte) 13,
    (byte) 155,
    (byte) 166,
    (byte) 174,
    (byte) 164,
    (byte) 250,
    (byte) 144 /*0x90*/,
    (byte) 139,
    (byte) 213,
    (byte) 14,
    (byte) 22,
    (byte) 236,
    (byte) 150,
    (byte) 81,
    (byte) 25,
    (byte) 243,
    (byte) 220,
    (byte) 184,
    (byte) 196,
    (byte) 180,
    (byte) 234,
    (byte) 14,
    (byte) 185,
    (byte) 232,
    (byte) 17,
    (byte) 38,
    (byte) 236,
    (byte) 250,
    (byte) 134,
    (byte) 67,
    (byte) 23,
    (byte) 75,
    (byte) 40,
    (byte) 197,
    (byte) 126,
    (byte) 150,
    (byte) 110,
    (byte) 29,
    (byte) 12,
    (byte) 120,
    (byte) 107,
    (byte) 32 /*0x20*/,
    (byte) 4,
    (byte) 4,
    (byte) 99,
    (byte) 228,
    (byte) 113,
    (byte) 215,
    (byte) 249,
    (byte) 239,
    (byte) 94,
    (byte) 176 /*0xB0*/,
    (byte) 28,
    (byte) 154,
    (byte) 123,
    (byte) 205,
    (byte) 27,
    (byte) 11,
    (byte) 143,
    (byte) 224 /*0xE0*/,
    (byte) 227,
    (byte) 97,
    (byte) 50,
    byte.MaxValue,
    (byte) 19,
    (byte) 75,
    (byte) 73,
    (byte) 80 /*0x50*/,
    (byte) 0,
    (byte) 121,
    (byte) 140,
    (byte) 56,
    (byte) 43,
    (byte) 3,
    (byte) 205,
    (byte) 40,
    (byte) 192 /*0xC0*/,
    (byte) 28,
    (byte) 54,
    (byte) 29,
    (byte) 31 /*0x1F*/,
    (byte) 132,
    (byte) 100,
    (byte) 45,
    (byte) 4,
    (byte) 182,
    (byte) 170
  };

  internal static string ssp_appserver_13303()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[44];
      byte[] numArray2 = new byte[44];
      numArray2[40] = (byte) 249;
      numArray2[1] = (byte) 158;
      numArray2[8] = (byte) 35;
      numArray2[6] = (byte) 17;
      numArray2[11] = (byte) 23;
      numArray2[5] = (byte) 238;
      numArray2[34] = (byte) 43;
      numArray2[24] = (byte) 243;
      numArray2[22] = (byte) 185;
      numArray2[0] = (byte) 65;
      numArray2[30] = (byte) 85;
      numArray2[10] = (byte) 137;
      numArray2[15] = (byte) 125;
      numArray2[13] = (byte) 82;
      numArray2[14] = (byte) 165;
      numArray2[3] = byte.MaxValue;
      numArray2[19] = (byte) 35;
      numArray2[23] = (byte) 229;
      numArray2[12] = (byte) 165;
      numArray2[9] = (byte) 142;
      numArray2[20] = (byte) 122;
      numArray2[21] = (byte) 110;
      numArray2[4] = (byte) 113;
      numArray2[35] = (byte) 111;
      numArray2[16 /*0x10*/] = (byte) 104;
      numArray2[25] = (byte) 38;
      numArray2[26] = (byte) 68;
      numArray2[27] = (byte) 250;
      numArray2[28] = (byte) 229;
      numArray2[29] = (byte) 110;
      numArray2[17] = (byte) 184;
      numArray2[18] = (byte) 112 /*0x70*/;
      numArray2[32 /*0x20*/] = (byte) 69;
      numArray2[33] = (byte) 5;
      numArray2[31 /*0x1F*/] = (byte) 207;
      numArray2[7] = (byte) 213;
      numArray2[2] = (byte) 180;
      numArray2[37] = (byte) 211;
      numArray2[38] = (byte) 7;
      numArray2[39] = (byte) 201;
      numArray2[41] = (byte) 44;
      numArray2[36] = (byte) 173;
      numArray2[42] = (byte) 247;
      numArray2[43] = (byte) 94;
      byte[] numArray3 = new byte[44];
      numArray3[20] = (byte) 55;
      numArray3[1] = (byte) 120;
      numArray3[11] = (byte) 91;
      numArray3[12] = (byte) 2;
      numArray3[38] = (byte) 69;
      numArray3[21] = (byte) 5;
      numArray3[6] = (byte) 38;
      numArray3[7] = (byte) 177;
      numArray3[8] = (byte) 225;
      numArray3[31 /*0x1F*/] = (byte) 58;
      numArray3[26] = (byte) 235;
      numArray3[16 /*0x10*/] = (byte) 186;
      numArray3[41] = (byte) 156;
      numArray3[34] = (byte) 69;
      numArray3[37] = (byte) 219;
      numArray3[15] = (byte) 178;
      numArray3[29] = (byte) 32 /*0x20*/;
      numArray3[17] = (byte) 52;
      numArray3[18] = (byte) 244;
      numArray3[19] = (byte) 223;
      numArray3[4] = (byte) 97;
      numArray3[32 /*0x20*/] = (byte) 166;
      numArray3[0] = (byte) 11;
      numArray3[25] = (byte) 143;
      numArray3[24] = (byte) 218;
      numArray3[22] = (byte) 216;
      numArray3[42] = (byte) 49;
      numArray3[27] = (byte) 85;
      numArray3[23] = (byte) 233;
      numArray3[3] = (byte) 26;
      numArray3[30] = (byte) 245;
      numArray3[39] = (byte) 77;
      numArray3[13] = (byte) 199;
      numArray3[33] = (byte) 22;
      numArray3[9] = (byte) 143;
      numArray3[35] = (byte) 237;
      numArray3[36] = (byte) 92;
      numArray3[2] = (byte) 94;
      numArray3[14] = (byte) 179;
      numArray3[10] = (byte) 242;
      numArray3[40] = (byte) 188;
      numArray3[5] = (byte) 16 /*0x10*/;
      numArray3[28] = (byte) 147;
      numArray3[43] = (byte) 216;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 44);
      for (int index = 0; index < 44; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[44];
    byte[] numArray5 = new byte[44];
    numArray5[7] = (byte) 96 /*0x60*/;
    numArray5[14] = (byte) 210;
    numArray5[28] = (byte) 21;
    numArray5[3] = (byte) 5;
    numArray5[12] = (byte) 89;
    numArray5[5] = (byte) 192 /*0xC0*/;
    numArray5[6] = (byte) 90;
    numArray5[17] = (byte) 117;
    numArray5[26] = (byte) 126;
    numArray5[41] = (byte) 179;
    numArray5[19] = (byte) 65;
    numArray5[13] = (byte) 205;
    numArray5[9] = (byte) 253;
    numArray5[2] = (byte) 62;
    numArray5[37] = (byte) 220;
    numArray5[15] = (byte) 203;
    numArray5[16 /*0x10*/] = (byte) 91;
    numArray5[33] = (byte) 78;
    numArray5[22] = (byte) 241;
    numArray5[35] = (byte) 175;
    numArray5[21] = (byte) 225;
    numArray5[10] = byte.MaxValue;
    numArray5[1] = (byte) 157;
    numArray5[18] = (byte) 31 /*0x1F*/;
    numArray5[24] = (byte) 23;
    numArray5[25] = (byte) 35;
    numArray5[36] = (byte) 216;
    numArray5[27] = (byte) 95;
    numArray5[0] = (byte) 52;
    numArray5[38] = (byte) 215;
    numArray5[30] = (byte) 38;
    numArray5[40] = (byte) 29;
    numArray5[32 /*0x20*/] = (byte) 163;
    numArray5[42] = (byte) 76;
    numArray5[34] = (byte) 186;
    numArray5[11] = (byte) 130;
    numArray5[4] = (byte) 245;
    numArray5[20] = (byte) 109;
    numArray5[29] = (byte) 76;
    numArray5[39] = (byte) 69;
    numArray5[31 /*0x1F*/] = (byte) 83;
    numArray5[8] = (byte) 250;
    numArray5[23] = (byte) 21;
    numArray5[43] = (byte) 208 /*0xD0*/;
    byte[] numArray6 = new byte[44]
    {
      (byte) 240 /*0xF0*/,
      (byte) 50,
      (byte) 44,
      (byte) 215,
      (byte) 144 /*0x90*/,
      (byte) 160 /*0xA0*/,
      (byte) 133,
      (byte) 84,
      (byte) 197,
      (byte) 99,
      (byte) 244,
      (byte) 155,
      (byte) 234,
      (byte) 153,
      (byte) 126,
      (byte) 156,
      (byte) 78,
      (byte) 220,
      (byte) 173,
      (byte) 19,
      (byte) 251,
      (byte) 119,
      (byte) 233,
      (byte) 73,
      (byte) 233,
      (byte) 43,
      (byte) 0,
      (byte) 180,
      (byte) 104,
      (byte) 250,
      (byte) 97,
      (byte) 9,
      (byte) 213,
      (byte) 242,
      (byte) 16 /*0x10*/,
      (byte) 128 /*0x80*/,
      (byte) 20,
      (byte) 67,
      (byte) 242,
      (byte) 145,
      (byte) 69,
      (byte) 34,
      (byte) 171,
      (byte) 236
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 44);
    for (int index = 0; index < 44; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static int ssp_appserver_13304(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 6,
      (byte) 2,
      (byte) 34,
      (byte) 103,
      (byte) 157,
      (byte) 248,
      (byte) 2,
      (byte) 143,
      (byte) 105,
      (byte) 20,
      (byte) 144 /*0x90*/,
      (byte) 138,
      (byte) 54,
      (byte) 33,
      (byte) 61,
      (byte) 82,
      (byte) 247,
      (byte) 188,
      (byte) 138,
      (byte) 192 /*0xC0*/,
      (byte) 69,
      (byte) 35,
      (byte) 235,
      (byte) 80 /*0x50*/,
      (byte) 178,
      (byte) 23,
      (byte) 113,
      (byte) 110,
      (byte) 179,
      (byte) 110,
      (byte) 170,
      (byte) 32 /*0x20*/,
      (byte) 65,
      (byte) 145,
      (byte) 151,
      (byte) 215,
      (byte) 1,
      (byte) 82,
      (byte) 247,
      (byte) 40,
      (byte) 101,
      (byte) 25,
      (byte) 68,
      (byte) 66,
      (byte) 25,
      (byte) 13,
      (byte) 96 /*0x60*/,
      (byte) 68
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 153,
      (byte) 25,
      (byte) 161,
      (byte) 139,
      (byte) 4,
      (byte) 61,
      (byte) 121,
      (byte) 206,
      (byte) 92,
      (byte) 15,
      (byte) 218,
      (byte) 148,
      (byte) 79,
      (byte) 229,
      (byte) 231,
      (byte) 137,
      (byte) 172,
      (byte) 143,
      (byte) 4,
      (byte) 197,
      (byte) 179,
      (byte) 51,
      (byte) 4,
      (byte) 129,
      (byte) 235,
      (byte) 33,
      (byte) 69,
      (byte) 212,
      (byte) 83,
      (byte) 94,
      (byte) 142,
      (byte) 125,
      (byte) 33,
      (byte) 148,
      (byte) 106,
      (byte) 75,
      (byte) 219,
      (byte) 103,
      (byte) 49,
      (byte) 124,
      (byte) 149,
      (byte) 116,
      (byte) 234,
      (byte) 30,
      (byte) 128 /*0x80*/,
      (byte) 152,
      (byte) 194,
      (byte) 251
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13305(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 239,
      (byte) 169,
      (byte) 226,
      (byte) 22,
      (byte) 210,
      (byte) 181,
      (byte) 134,
      (byte) 135,
      (byte) 147,
      (byte) 219,
      (byte) 250,
      (byte) 105,
      (byte) 112 /*0x70*/,
      (byte) 72,
      (byte) 165,
      (byte) 138,
      (byte) 101,
      (byte) 80 /*0x50*/,
      (byte) 165,
      (byte) 196,
      (byte) 97,
      (byte) 90,
      (byte) 43,
      (byte) 222,
      (byte) 168,
      (byte) 54,
      (byte) 164,
      (byte) 182,
      (byte) 44,
      (byte) 111,
      (byte) 218,
      (byte) 201,
      (byte) 254,
      (byte) 167,
      (byte) 81,
      (byte) 231,
      (byte) 181,
      (byte) 192 /*0xC0*/,
      (byte) 47,
      (byte) 157,
      (byte) 113,
      (byte) 160 /*0xA0*/,
      (byte) 210,
      (byte) 61,
      (byte) 74,
      (byte) 216,
      (byte) 168,
      (byte) 196
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 199,
      (byte) 6,
      (byte) 174,
      (byte) 228,
      (byte) 154,
      (byte) 143,
      (byte) 184,
      (byte) 139,
      (byte) 229,
      (byte) 123,
      (byte) 131,
      (byte) 141,
      (byte) 86,
      (byte) 239,
      (byte) 186,
      (byte) 229,
      (byte) 131,
      (byte) 211,
      (byte) 144 /*0x90*/,
      (byte) 228,
      (byte) 114,
      (byte) 73,
      (byte) 8,
      (byte) 182,
      (byte) 49,
      (byte) 54,
      (byte) 159,
      (byte) 248,
      (byte) 240 /*0xF0*/,
      (byte) 171,
      (byte) 14,
      (byte) 102,
      (byte) 164,
      (byte) 218,
      (byte) 184,
      (byte) 144 /*0x90*/,
      (byte) 85,
      (byte) 57,
      (byte) 200,
      (byte) 167,
      (byte) 171,
      (byte) 14,
      (byte) 130,
      (byte) 209,
      (byte) 216,
      (byte) 93,
      (byte) 199,
      (byte) 218
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13306(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 231,
      (byte) 23,
      (byte) 149,
      (byte) 125,
      (byte) 3,
      (byte) 110,
      (byte) 155,
      (byte) 16 /*0x10*/,
      (byte) 207,
      (byte) 91,
      (byte) 36,
      (byte) 1,
      (byte) 48 /*0x30*/,
      (byte) 107,
      (byte) 201,
      (byte) 247,
      (byte) 189,
      (byte) 213,
      (byte) 165,
      (byte) 33,
      (byte) 7,
      (byte) 83,
      (byte) 193,
      (byte) 136,
      (byte) 238,
      (byte) 148,
      (byte) 208 /*0xD0*/,
      (byte) 141,
      (byte) 224 /*0xE0*/,
      (byte) 251,
      (byte) 113,
      (byte) 213,
      (byte) 82,
      (byte) 201,
      (byte) 23,
      (byte) 143,
      (byte) 188,
      (byte) 106,
      (byte) 177,
      (byte) 82,
      (byte) 148,
      (byte) 141,
      (byte) 161,
      (byte) 69,
      (byte) 173,
      (byte) 59,
      (byte) 25,
      (byte) 109
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[42] = (byte) 108;
    sourceArray2[46] = (byte) 137;
    sourceArray2[20] = (byte) 147;
    sourceArray2[47] = (byte) 38;
    sourceArray2[2] = (byte) 37;
    sourceArray2[5] = (byte) 136;
    sourceArray2[6] = (byte) 105;
    sourceArray2[16 /*0x10*/] = (byte) 86;
    sourceArray2[8] = (byte) 99;
    sourceArray2[38] = (byte) 49;
    sourceArray2[9] = (byte) 72;
    sourceArray2[11] = (byte) 19;
    sourceArray2[12] = (byte) 169;
    sourceArray2[13] = (byte) 125;
    sourceArray2[14] = (byte) 84;
    sourceArray2[30] = (byte) 145;
    sourceArray2[19] = (byte) 57;
    sourceArray2[22] = (byte) 238;
    sourceArray2[39] = (byte) 127 /*0x7F*/;
    sourceArray2[15] = (byte) 21;
    sourceArray2[41] = (byte) 179;
    sourceArray2[21] = (byte) 132;
    sourceArray2[4] = (byte) 61;
    sourceArray2[23] = (byte) 36;
    sourceArray2[24] = (byte) 56;
    sourceArray2[25] = (byte) 111;
    sourceArray2[29] = (byte) 107;
    sourceArray2[32 /*0x20*/] = (byte) 105;
    sourceArray2[28] = (byte) 195;
    sourceArray2[1] = (byte) 27;
    sourceArray2[18] = (byte) 27;
    sourceArray2[31 /*0x1F*/] = (byte) 116;
    sourceArray2[44] = (byte) 65;
    sourceArray2[37] = (byte) 242;
    sourceArray2[34] = (byte) 90;
    sourceArray2[35] = (byte) 93;
    sourceArray2[36] = (byte) 115;
    sourceArray2[3] = (byte) 87;
    sourceArray2[33] = (byte) 115;
    sourceArray2[7] = (byte) 29;
    sourceArray2[40] = (byte) 236;
    sourceArray2[0] = (byte) 73;
    sourceArray2[45] = (byte) 60;
    sourceArray2[43] = (byte) 122;
    sourceArray2[10] = (byte) 133;
    sourceArray2[17] = (byte) 43;
    sourceArray2[26] = (byte) 236;
    sourceArray2[27] = (byte) 141;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13307(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 178,
      (byte) 25,
      (byte) 165,
      (byte) 147,
      (byte) 170,
      (byte) 192 /*0xC0*/,
      (byte) 161,
      (byte) 218,
      (byte) 125,
      (byte) 225,
      (byte) 218,
      (byte) 49,
      (byte) 220,
      (byte) 244,
      (byte) 59,
      (byte) 94,
      (byte) 29,
      (byte) 213,
      (byte) 92,
      (byte) 109,
      (byte) 196,
      (byte) 149,
      (byte) 207,
      (byte) 0,
      (byte) 138,
      (byte) 120,
      (byte) 226,
      (byte) 158,
      (byte) 5,
      (byte) 29,
      (byte) 228,
      (byte) 31 /*0x1F*/,
      (byte) 124,
      (byte) 38,
      (byte) 200,
      (byte) 72,
      (byte) 8,
      (byte) 127 /*0x7F*/,
      (byte) 83,
      (byte) 203,
      (byte) 63 /*0x3F*/,
      (byte) 16 /*0x10*/,
      (byte) 84,
      (byte) 162,
      (byte) 94,
      (byte) 126,
      (byte) 254,
      (byte) 77
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 112 /*0x70*/,
      (byte) 115,
      (byte) 163,
      (byte) 27,
      (byte) 236,
      (byte) 67,
      (byte) 138,
      (byte) 224 /*0xE0*/,
      (byte) 235,
      (byte) 192 /*0xC0*/,
      (byte) 38,
      (byte) 184,
      (byte) 44,
      (byte) 187,
      (byte) 252,
      (byte) 37,
      (byte) 61,
      (byte) 185,
      (byte) 120,
      (byte) 205,
      (byte) 122,
      (byte) 253,
      (byte) 90,
      (byte) 169,
      (byte) 167,
      (byte) 134,
      (byte) 3,
      (byte) 251,
      (byte) 197,
      (byte) 177,
      (byte) 121,
      (byte) 106,
      (byte) 46,
      (byte) 220,
      (byte) 100,
      (byte) 246,
      (byte) 148,
      (byte) 121,
      (byte) 225,
      (byte) 86,
      (byte) 247,
      (byte) 214,
      (byte) 24,
      (byte) 52,
      (byte) 80 /*0x50*/,
      (byte) 54,
      (byte) 237,
      (byte) 68
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13308(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 241,
      (byte) 199,
      (byte) 28,
      (byte) 73,
      (byte) 165,
      (byte) 8,
      (byte) 50,
      (byte) 222,
      (byte) 3,
      (byte) 212,
      (byte) 95,
      (byte) 253,
      (byte) 186,
      (byte) 181,
      (byte) 182,
      (byte) 44,
      (byte) 242,
      (byte) 86,
      (byte) 158,
      (byte) 130,
      (byte) 144 /*0x90*/,
      (byte) 46,
      (byte) 180,
      (byte) 65,
      (byte) 45,
      (byte) 161,
      (byte) 11,
      (byte) 121,
      (byte) 148,
      (byte) 37,
      (byte) 221,
      (byte) 222,
      (byte) 236,
      (byte) 33,
      (byte) 138,
      (byte) 94,
      (byte) 171,
      byte.MaxValue,
      (byte) 99,
      (byte) 118,
      (byte) 33,
      (byte) 236,
      (byte) 60,
      (byte) 59,
      (byte) 244,
      (byte) 164,
      (byte) 164,
      (byte) 201
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 171,
      (byte) 105,
      (byte) 4,
      (byte) 113,
      (byte) 110,
      (byte) 92,
      (byte) 148,
      (byte) 54,
      (byte) 240 /*0xF0*/,
      (byte) 48 /*0x30*/,
      (byte) 104,
      (byte) 196,
      (byte) 158,
      (byte) 81,
      (byte) 103,
      (byte) 117,
      (byte) 254,
      (byte) 175,
      (byte) 68,
      (byte) 206,
      (byte) 57,
      (byte) 186,
      (byte) 4,
      (byte) 12,
      (byte) 25,
      (byte) 128 /*0x80*/,
      (byte) 243,
      (byte) 147,
      (byte) 236,
      (byte) 3,
      (byte) 72,
      (byte) 120,
      (byte) 49,
      (byte) 126,
      (byte) 18,
      (byte) 155,
      (byte) 74,
      (byte) 201,
      (byte) 93,
      (byte) 101,
      (byte) 118,
      (byte) 229,
      (byte) 74,
      (byte) 21,
      (byte) 174,
      (byte) 201,
      (byte) 154,
      (byte) 250
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[42];
    byte[] response2 = new byte[42];
    Array.Copy((Array) sc_13302.sspq, 0, (Array) numArray2, 0, 42);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13302.sspr, 0, (Array) numArray2, 0, 42);
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

  internal static string ssp_appserver_13309()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[92];
      byte[] numArray2 = new byte[55];
      numArray2[44] = (byte) 10;
      numArray2[1] = (byte) 162;
      numArray2[37] = (byte) 52;
      numArray2[33] = (byte) 174;
      numArray2[4] = (byte) 115;
      numArray2[5] = (byte) 148;
      numArray2[34] = (byte) 248;
      numArray2[7] = (byte) 159;
      numArray2[8] = (byte) 239;
      numArray2[30] = (byte) 178;
      numArray2[41] = (byte) 163;
      numArray2[11] = (byte) 222;
      numArray2[53] = (byte) 87;
      numArray2[31 /*0x1F*/] = (byte) 15;
      numArray2[26] = (byte) 152;
      numArray2[15] = (byte) 208 /*0xD0*/;
      numArray2[19] = (byte) 6;
      numArray2[17] = (byte) 142;
      numArray2[48 /*0x30*/] = (byte) 233;
      numArray2[40] = (byte) 242;
      numArray2[9] = (byte) 158;
      numArray2[51] = (byte) 65;
      numArray2[22] = (byte) 214;
      numArray2[23] = (byte) 64 /*0x40*/;
      numArray2[2] = (byte) 93;
      numArray2[25] = (byte) 67;
      numArray2[18] = (byte) 147;
      numArray2[46] = (byte) 196;
      numArray2[28] = (byte) 98;
      numArray2[29] = (byte) 12;
      numArray2[35] = (byte) 1;
      numArray2[36] = (byte) 148;
      numArray2[43] = (byte) 213;
      numArray2[14] = (byte) 201;
      numArray2[6] = (byte) 10;
      numArray2[32 /*0x20*/] = (byte) 148;
      numArray2[10] = (byte) 147;
      numArray2[20] = (byte) 187;
      numArray2[38] = (byte) 217;
      numArray2[39] = (byte) 244;
      numArray2[27] = (byte) 230;
      numArray2[24] = (byte) 235;
      numArray2[12] = (byte) 120;
      numArray2[21] = (byte) 133;
      numArray2[42] = (byte) 67;
      numArray2[45] = (byte) 45;
      numArray2[0] = (byte) 104;
      numArray2[16 /*0x10*/] = (byte) 154;
      numArray2[52] = (byte) 80 /*0x50*/;
      numArray2[49] = (byte) 247;
      numArray2[50] = (byte) 177;
      numArray2[3] = (byte) 68;
      numArray2[13] = (byte) 20;
      numArray2[47] = (byte) 99;
      numArray2[54] = (byte) 190;
      byte[] numArray3 = new byte[55];
      numArray3[49] = (byte) 247;
      numArray3[1] = (byte) 169;
      numArray3[41] = (byte) 108;
      numArray3[50] = (byte) 236;
      numArray3[5] = (byte) 193;
      numArray3[54] = (byte) 189;
      numArray3[29] = (byte) 132;
      numArray3[7] = (byte) 40;
      numArray3[8] = (byte) 62;
      numArray3[46] = (byte) 243;
      numArray3[10] = (byte) 173;
      numArray3[44] = (byte) 109;
      numArray3[39] = (byte) 20;
      numArray3[13] = (byte) 202;
      numArray3[2] = (byte) 233;
      numArray3[33] = (byte) 189;
      numArray3[51] = (byte) 98;
      numArray3[17] = (byte) 116;
      numArray3[18] = (byte) 231;
      numArray3[19] = (byte) 244;
      numArray3[3] = (byte) 212;
      numArray3[21] = (byte) 238;
      numArray3[22] = (byte) 179;
      numArray3[12] = (byte) 66;
      numArray3[6] = (byte) 124;
      numArray3[25] = (byte) 54;
      numArray3[26] = (byte) 65;
      numArray3[27] = (byte) 71;
      numArray3[42] = (byte) 130;
      numArray3[9] = (byte) 100;
      numArray3[30] = (byte) 79;
      numArray3[31 /*0x1F*/] = (byte) 160 /*0xA0*/;
      numArray3[20] = (byte) 38;
      numArray3[11] = (byte) 124;
      numArray3[0] = (byte) 167;
      numArray3[35] = (byte) 148;
      numArray3[45] = (byte) 58;
      numArray3[37] = (byte) 127 /*0x7F*/;
      numArray3[38] = (byte) 36;
      numArray3[32 /*0x20*/] = (byte) 100;
      numArray3[40] = (byte) 50;
      numArray3[47] = (byte) 63 /*0x3F*/;
      numArray3[16 /*0x10*/] = (byte) 202;
      numArray3[43] = (byte) 59;
      numArray3[14] = (byte) 72;
      numArray3[53] = (byte) 184;
      numArray3[23] = (byte) 59;
      numArray3[34] = (byte) 206;
      numArray3[4] = (byte) 231;
      numArray3[15] = (byte) 181;
      numArray3[48 /*0x30*/] = (byte) 40;
      numArray3[28] = (byte) 110;
      numArray3[52] = (byte) 16 /*0x10*/;
      numArray3[36] = byte.MaxValue;
      numArray3[24] = (byte) 185;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[37];
      numArray4[7] = (byte) 152;
      numArray4[1] = (byte) 193;
      numArray4[24] = (byte) 175;
      numArray4[3] = (byte) 214;
      numArray4[4] = (byte) 74;
      numArray4[10] = (byte) 143;
      numArray4[33] = (byte) 181;
      numArray4[25] = (byte) 80 /*0x50*/;
      numArray4[2] = (byte) 44;
      numArray4[9] = (byte) 21;
      numArray4[29] = (byte) 3;
      numArray4[11] = (byte) 146;
      numArray4[12] = (byte) 43;
      numArray4[8] = (byte) 182;
      numArray4[30] = (byte) 250;
      numArray4[14] = (byte) 76;
      numArray4[16 /*0x10*/] = (byte) 115;
      numArray4[17] = (byte) 157;
      numArray4[18] = (byte) 27;
      numArray4[19] = (byte) 11;
      numArray4[36] = (byte) 204;
      numArray4[0] = (byte) 56;
      numArray4[22] = (byte) 4;
      numArray4[23] = (byte) 99;
      numArray4[6] = (byte) 121;
      numArray4[13] = (byte) 53;
      numArray4[26] = (byte) 185;
      numArray4[31 /*0x1F*/] = (byte) 84;
      numArray4[28] = (byte) 96 /*0x60*/;
      numArray4[35] = (byte) 125;
      numArray4[15] = (byte) 16 /*0x10*/;
      numArray4[5] = (byte) 32 /*0x20*/;
      numArray4[32 /*0x20*/] = (byte) 183;
      numArray4[34] = (byte) 87;
      numArray4[20] = (byte) 172;
      numArray4[21] = (byte) 237;
      numArray4[27] = (byte) 78;
      byte[] numArray5 = new byte[37];
      numArray5[31 /*0x1F*/] = (byte) 192 /*0xC0*/;
      numArray5[29] = (byte) 183;
      numArray5[2] = (byte) 73;
      numArray5[24] = (byte) 144 /*0x90*/;
      numArray5[34] = (byte) 165;
      numArray5[5] = (byte) 47;
      numArray5[6] = (byte) 92;
      numArray5[7] = (byte) 25;
      numArray5[18] = (byte) 93;
      numArray5[4] = (byte) 168;
      numArray5[8] = (byte) 72;
      numArray5[11] = (byte) 93;
      numArray5[12] = (byte) 80 /*0x50*/;
      numArray5[0] = (byte) 223;
      numArray5[3] = (byte) 107;
      numArray5[20] = (byte) 135;
      numArray5[15] = (byte) 41;
      numArray5[23] = (byte) 150;
      numArray5[16 /*0x10*/] = (byte) 174;
      numArray5[19] = (byte) 128 /*0x80*/;
      numArray5[14] = (byte) 253;
      numArray5[21] = (byte) 119;
      numArray5[13] = (byte) 242;
      numArray5[9] = (byte) 2;
      numArray5[33] = (byte) 165;
      numArray5[25] = (byte) 198;
      numArray5[32 /*0x20*/] = (byte) 82;
      numArray5[27] = (byte) 134;
      numArray5[28] = (byte) 236;
      numArray5[17] = (byte) 108;
      numArray5[30] = (byte) 163;
      numArray5[22] = (byte) 82;
      numArray5[26] = (byte) 170;
      numArray5[10] = (byte) 139;
      numArray5[35] = (byte) 242;
      numArray5[1] = byte.MaxValue;
      numArray5[36] = (byte) 181;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 37);
      for (int index = 0; index < 37; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[92];
    byte[] numArray7 = new byte[55];
    numArray7[9] = (byte) 209;
    numArray7[43] = (byte) 17;
    numArray7[54] = (byte) 246;
    numArray7[3] = (byte) 73;
    numArray7[52] = (byte) 216;
    numArray7[42] = (byte) 37;
    numArray7[6] = (byte) 191;
    numArray7[0] = (byte) 11;
    numArray7[44] = (byte) 150;
    numArray7[46] = (byte) 156;
    numArray7[40] = (byte) 150;
    numArray7[8] = (byte) 198;
    numArray7[13] = (byte) 219;
    numArray7[29] = (byte) 128 /*0x80*/;
    numArray7[14] = (byte) 152;
    numArray7[15] = (byte) 124;
    numArray7[11] = (byte) 64 /*0x40*/;
    numArray7[1] = (byte) 109;
    numArray7[18] = (byte) 159;
    numArray7[16 /*0x10*/] = (byte) 61;
    numArray7[20] = (byte) 47;
    numArray7[21] = (byte) 2;
    numArray7[17] = (byte) 136;
    numArray7[47] = (byte) 68;
    numArray7[24] = (byte) 192 /*0xC0*/;
    numArray7[41] = (byte) 253;
    numArray7[26] = (byte) 121;
    numArray7[27] = (byte) 73;
    numArray7[37] = (byte) 81;
    numArray7[51] = (byte) 225;
    numArray7[28] = (byte) 1;
    numArray7[10] = (byte) 14;
    numArray7[32 /*0x20*/] = (byte) 13;
    numArray7[23] = (byte) 53;
    numArray7[34] = (byte) 88;
    numArray7[35] = (byte) 160 /*0xA0*/;
    numArray7[7] = (byte) 213;
    numArray7[45] = (byte) 236;
    numArray7[2] = (byte) 80 /*0x50*/;
    numArray7[39] = (byte) 37;
    numArray7[33] = (byte) 14;
    numArray7[38] = (byte) 16 /*0x10*/;
    numArray7[19] = (byte) 125;
    numArray7[5] = (byte) 128 /*0x80*/;
    numArray7[30] = (byte) 162;
    numArray7[25] = (byte) 235;
    numArray7[22] = (byte) 21;
    numArray7[50] = (byte) 240 /*0xF0*/;
    numArray7[48 /*0x30*/] = (byte) 153;
    numArray7[49] = (byte) 135;
    numArray7[31 /*0x1F*/] = (byte) 72;
    numArray7[4] = (byte) 71;
    numArray7[12] = (byte) 227;
    numArray7[53] = (byte) 120;
    numArray7[36] = (byte) 225;
    byte[] numArray8 = new byte[55];
    numArray8[27] = (byte) 240 /*0xF0*/;
    numArray8[1] = (byte) 165;
    numArray8[51] = (byte) 65;
    numArray8[4] = (byte) 206;
    numArray8[34] = (byte) 196;
    numArray8[5] = (byte) 18;
    numArray8[6] = (byte) 150;
    numArray8[43] = (byte) 94;
    numArray8[16 /*0x10*/] = (byte) 37;
    numArray8[46] = (byte) 29;
    numArray8[20] = (byte) 183;
    numArray8[11] = (byte) 38;
    numArray8[12] = (byte) 113;
    numArray8[44] = (byte) 250;
    numArray8[14] = (byte) 65;
    numArray8[15] = (byte) 217;
    numArray8[24] = (byte) 175;
    numArray8[17] = (byte) 0;
    numArray8[35] = (byte) 172;
    numArray8[9] = (byte) 138;
    numArray8[10] = (byte) 155;
    numArray8[13] = (byte) 137;
    numArray8[22] = (byte) 165;
    numArray8[23] = (byte) 189;
    numArray8[0] = (byte) 144 /*0x90*/;
    numArray8[25] = (byte) 51;
    numArray8[21] = (byte) 65;
    numArray8[7] = (byte) 58;
    numArray8[48 /*0x30*/] = (byte) 86;
    numArray8[3] = (byte) 8;
    numArray8[30] = (byte) 43;
    numArray8[52] = (byte) 38;
    numArray8[32 /*0x20*/] = (byte) 34;
    numArray8[33] = (byte) 7;
    numArray8[18] = (byte) 156;
    numArray8[49] = (byte) 120;
    numArray8[26] = (byte) 53;
    numArray8[37] = (byte) 6;
    numArray8[38] = (byte) 192 /*0xC0*/;
    numArray8[39] = (byte) 102;
    numArray8[40] = (byte) 44;
    numArray8[31 /*0x1F*/] = (byte) 106;
    numArray8[29] = (byte) 84;
    numArray8[42] = (byte) 237;
    numArray8[19] = (byte) 40;
    numArray8[47] = (byte) 138;
    numArray8[53] = (byte) 210;
    numArray8[2] = (byte) 116;
    numArray8[8] = (byte) 45;
    numArray8[45] = (byte) 5;
    numArray8[50] = (byte) 127 /*0x7F*/;
    numArray8[28] = (byte) 215;
    numArray8[36] = (byte) 141;
    numArray8[41] = (byte) 245;
    numArray8[54] = (byte) 105;
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[37]
    {
      (byte) 171,
      (byte) 155,
      (byte) 133,
      (byte) 93,
      (byte) 184,
      (byte) 204,
      (byte) 41,
      (byte) 22,
      (byte) 69,
      (byte) 154,
      (byte) 105,
      (byte) 14,
      (byte) 188,
      (byte) 138,
      (byte) 16 /*0x10*/,
      (byte) 26,
      (byte) 15,
      (byte) 110,
      (byte) 56,
      (byte) 213,
      (byte) 175,
      (byte) 86,
      (byte) 254,
      (byte) 124,
      (byte) 230,
      (byte) 30,
      (byte) 117,
      (byte) 172,
      (byte) 177,
      (byte) 14,
      (byte) 57,
      (byte) 151,
      (byte) 248,
      (byte) 195,
      (byte) 158,
      (byte) 120,
      (byte) 34
    };
    byte[] numArray10 = new byte[37]
    {
      (byte) 157,
      (byte) 59,
      (byte) 71,
      (byte) 112 /*0x70*/,
      (byte) 1,
      (byte) 151,
      (byte) 134,
      (byte) 111,
      (byte) 63 /*0x3F*/,
      (byte) 123,
      (byte) 166,
      (byte) 32 /*0x20*/,
      (byte) 149,
      (byte) 216,
      (byte) 249,
      (byte) 94,
      (byte) 115,
      (byte) 241,
      (byte) 153,
      (byte) 45,
      (byte) 141,
      (byte) 36,
      (byte) 218,
      (byte) 42,
      (byte) 196,
      (byte) 111,
      (byte) 102,
      (byte) 155,
      (byte) 33,
      (byte) 75,
      (byte) 228,
      (byte) 58,
      (byte) 169,
      (byte) 95,
      (byte) 14,
      (byte) 112 /*0x70*/,
      (byte) 109
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 37);
    for (int index = 0; index < 37; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_13310()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 92,
        (byte) 51,
        (byte) 129,
        (byte) 160 /*0xA0*/,
        (byte) 126,
        (byte) 51,
        (byte) 141,
        (byte) 208 /*0xD0*/,
        (byte) 139,
        (byte) 243
      };
      byte[] numArray3 = new byte[10]
      {
        (byte) 213,
        (byte) 173,
        (byte) 5,
        (byte) 77,
        (byte) 28,
        (byte) 47,
        (byte) 228,
        (byte) 158,
        (byte) 62,
        (byte) 149
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10]
    {
      (byte) 129,
      (byte) 84,
      (byte) 156,
      (byte) 8,
      (byte) 118,
      (byte) 154,
      (byte) 86,
      (byte) 125,
      (byte) 100,
      (byte) 54
    };
    byte[] numArray6 = new byte[10];
    numArray6[3] = (byte) 140;
    numArray6[1] = (byte) 62;
    numArray6[4] = (byte) 143;
    numArray6[7] = (byte) 233;
    numArray6[5] = (byte) 236;
    numArray6[6] = (byte) 203;
    numArray6[2] = (byte) 222;
    numArray6[9] = (byte) 13;
    numArray6[8] = (byte) 14;
    numArray6[0] = (byte) 84;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13311()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[122];
      byte[] numArray2 = new byte[55]
      {
        (byte) 219,
        (byte) 103,
        (byte) 234,
        (byte) 232,
        (byte) 220,
        (byte) 37,
        (byte) 27,
        (byte) 222,
        (byte) 19,
        (byte) 162,
        (byte) 66,
        (byte) 73,
        (byte) 219,
        (byte) 26,
        (byte) 120,
        (byte) 6,
        (byte) 172,
        (byte) 131,
        (byte) 248,
        (byte) 253,
        (byte) 33,
        (byte) 206,
        (byte) 104,
        (byte) 59,
        (byte) 160 /*0xA0*/,
        (byte) 56,
        (byte) 147,
        (byte) 211,
        (byte) 140,
        (byte) 78,
        (byte) 128 /*0x80*/,
        (byte) 46,
        (byte) 49,
        (byte) 31 /*0x1F*/,
        (byte) 44,
        (byte) 14,
        (byte) 175,
        (byte) 110,
        (byte) 160 /*0xA0*/,
        (byte) 63 /*0x3F*/,
        (byte) 27,
        (byte) 129,
        (byte) 190,
        (byte) 115,
        (byte) 182,
        (byte) 162,
        (byte) 188,
        (byte) 221,
        (byte) 166,
        (byte) 200,
        (byte) 136,
        (byte) 82,
        (byte) 90,
        (byte) 37,
        (byte) 91
      };
      byte[] numArray3 = new byte[55];
      numArray3[36] = (byte) 10;
      numArray3[19] = (byte) 198;
      numArray3[2] = (byte) 163;
      numArray3[39] = (byte) 96 /*0x60*/;
      numArray3[53] = (byte) 35;
      numArray3[48 /*0x30*/] = (byte) 235;
      numArray3[31 /*0x1F*/] = (byte) 81;
      numArray3[45] = (byte) 232;
      numArray3[8] = (byte) 138;
      numArray3[44] = (byte) 86;
      numArray3[10] = (byte) 244;
      numArray3[9] = (byte) 25;
      numArray3[12] = (byte) 11;
      numArray3[30] = (byte) 231;
      numArray3[0] = (byte) 201;
      numArray3[15] = (byte) 217;
      numArray3[16 /*0x10*/] = (byte) 133;
      numArray3[17] = (byte) 48 /*0x30*/;
      numArray3[18] = (byte) 227;
      numArray3[28] = (byte) 185;
      numArray3[22] = (byte) 102;
      numArray3[3] = (byte) 96 /*0x60*/;
      numArray3[25] = (byte) 63 /*0x3F*/;
      numArray3[1] = (byte) 243;
      numArray3[4] = (byte) 7;
      numArray3[46] = (byte) 105;
      numArray3[26] = (byte) 58;
      numArray3[27] = (byte) 200;
      numArray3[42] = (byte) 173;
      numArray3[14] = (byte) 6;
      numArray3[13] = (byte) 117;
      numArray3[6] = (byte) 170;
      numArray3[7] = (byte) 233;
      numArray3[33] = (byte) 166;
      numArray3[5] = (byte) 31 /*0x1F*/;
      numArray3[35] = (byte) 123;
      numArray3[11] = (byte) 80 /*0x50*/;
      numArray3[37] = (byte) 150;
      numArray3[32 /*0x20*/] = (byte) 80 /*0x50*/;
      numArray3[40] = (byte) 83;
      numArray3[34] = (byte) 92;
      numArray3[50] = (byte) 237;
      numArray3[24] = (byte) 180;
      numArray3[43] = (byte) 147;
      numArray3[23] = (byte) 22;
      numArray3[29] = (byte) 132;
      numArray3[38] = (byte) 59;
      numArray3[47] = (byte) 56;
      numArray3[20] = (byte) 41;
      numArray3[49] = (byte) 25;
      numArray3[21] = (byte) 113;
      numArray3[51] = (byte) 2;
      numArray3[52] = (byte) 63 /*0x3F*/;
      numArray3[41] = (byte) 226;
      numArray3[54] = (byte) 76;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 42,
        (byte) 73,
        (byte) 40,
        (byte) 220,
        (byte) 26,
        (byte) 141,
        (byte) 0,
        (byte) 243,
        (byte) 63 /*0x3F*/,
        (byte) 12,
        (byte) 248,
        (byte) 206,
        (byte) 89,
        (byte) 86,
        (byte) 22,
        (byte) 229,
        (byte) 11,
        (byte) 14,
        (byte) 112 /*0x70*/,
        (byte) 21,
        (byte) 163,
        (byte) 225,
        (byte) 115,
        (byte) 122,
        (byte) 197,
        (byte) 240 /*0xF0*/,
        (byte) 82,
        (byte) 122,
        (byte) 189,
        (byte) 50,
        (byte) 199,
        (byte) 7,
        (byte) 32 /*0x20*/,
        (byte) 6,
        (byte) 213,
        (byte) 222,
        (byte) 212,
        (byte) 208 /*0xD0*/,
        (byte) 185,
        (byte) 161,
        (byte) 249,
        (byte) 94,
        (byte) 99,
        (byte) 126,
        (byte) 8,
        (byte) 33,
        (byte) 227,
        (byte) 27,
        (byte) 52,
        (byte) 139,
        (byte) 150,
        (byte) 53,
        (byte) 76,
        (byte) 232,
        (byte) 102
      };
      byte[] numArray5 = new byte[55];
      numArray5[11] = (byte) 250;
      numArray5[22] = (byte) 24;
      numArray5[6] = (byte) 53;
      numArray5[2] = (byte) 203;
      numArray5[4] = (byte) 234;
      numArray5[20] = (byte) 131;
      numArray5[13] = (byte) 48 /*0x30*/;
      numArray5[18] = (byte) 65;
      numArray5[35] = (byte) 102;
      numArray5[50] = (byte) 122;
      numArray5[10] = (byte) 165;
      numArray5[1] = (byte) 246;
      numArray5[12] = (byte) 238;
      numArray5[39] = (byte) 53;
      numArray5[5] = (byte) 20;
      numArray5[37] = (byte) 182;
      numArray5[16 /*0x10*/] = (byte) 75;
      numArray5[40] = (byte) 115;
      numArray5[25] = (byte) 217;
      numArray5[33] = (byte) 169;
      numArray5[49] = (byte) 175;
      numArray5[21] = (byte) 154;
      numArray5[48 /*0x30*/] = (byte) 13;
      numArray5[23] = (byte) 35;
      numArray5[24] = (byte) 110;
      numArray5[32 /*0x20*/] = (byte) 178;
      numArray5[47] = (byte) 84;
      numArray5[27] = (byte) 157;
      numArray5[28] = (byte) 39;
      numArray5[41] = (byte) 72;
      numArray5[30] = (byte) 146;
      numArray5[31 /*0x1F*/] = (byte) 102;
      numArray5[34] = (byte) 190;
      numArray5[8] = (byte) 55;
      numArray5[0] = (byte) 223;
      numArray5[26] = (byte) 15;
      numArray5[36] = (byte) 113;
      numArray5[19] = (byte) 15;
      numArray5[46] = (byte) 128 /*0x80*/;
      numArray5[3] = (byte) 57;
      numArray5[29] = (byte) 33;
      numArray5[52] = (byte) 37;
      numArray5[42] = (byte) 46;
      numArray5[9] = (byte) 147;
      numArray5[38] = (byte) 65;
      numArray5[14] = (byte) 204;
      numArray5[7] = (byte) 205;
      numArray5[43] = (byte) 233;
      numArray5[17] = (byte) 93;
      numArray5[44] = (byte) 244;
      numArray5[15] = (byte) 35;
      numArray5[51] = (byte) 174;
      numArray5[45] = (byte) 209;
      numArray5[53] = (byte) 123;
      numArray5[54] = (byte) 39;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[12];
      numArray6[10] = (byte) 179;
      numArray6[7] = (byte) 177;
      numArray6[5] = (byte) 4;
      numArray6[1] = (byte) 114;
      numArray6[2] = (byte) 28;
      numArray6[9] = (byte) 117;
      numArray6[3] = (byte) 4;
      numArray6[0] = (byte) 59;
      numArray6[8] = (byte) 53;
      numArray6[4] = (byte) 64 /*0x40*/;
      numArray6[6] = (byte) 112 /*0x70*/;
      numArray6[11] = (byte) 245;
      byte[] numArray7 = new byte[12];
      numArray7[9] = (byte) 6;
      numArray7[0] = (byte) 31 /*0x1F*/;
      numArray7[2] = (byte) 15;
      numArray7[11] = (byte) 202;
      numArray7[1] = (byte) 61;
      numArray7[5] = (byte) 49;
      numArray7[6] = (byte) 181;
      numArray7[7] = (byte) 149;
      numArray7[8] = (byte) 153;
      numArray7[3] = (byte) 35;
      numArray7[10] = (byte) 146;
      numArray7[4] = (byte) 25;
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 12);
      for (int index = 0; index < 12; ++index)
        numArray1[index + 110] ^= numArray7[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray8 = new byte[122];
    byte[] numArray9 = new byte[55]
    {
      (byte) 36,
      (byte) 230,
      (byte) 136,
      (byte) 226,
      (byte) 253,
      (byte) 219,
      (byte) 164,
      (byte) 204,
      (byte) 145,
      (byte) 218,
      (byte) 25,
      (byte) 23,
      (byte) 35,
      (byte) 6,
      (byte) 164,
      (byte) 37,
      (byte) 225,
      (byte) 65,
      (byte) 159,
      (byte) 103,
      (byte) 247,
      (byte) 77,
      (byte) 47,
      (byte) 254,
      (byte) 249,
      (byte) 9,
      (byte) 53,
      (byte) 53,
      (byte) 8,
      (byte) 254,
      (byte) 56,
      (byte) 83,
      (byte) 4,
      (byte) 117,
      (byte) 246,
      (byte) 64 /*0x40*/,
      (byte) 51,
      (byte) 120,
      (byte) 50,
      (byte) 161,
      (byte) 248,
      (byte) 124,
      (byte) 37,
      (byte) 102,
      (byte) 153,
      (byte) 37,
      (byte) 72,
      (byte) 119,
      (byte) 28,
      (byte) 1,
      (byte) 10,
      (byte) 6,
      (byte) 210,
      (byte) 251,
      (byte) 59
    };
    byte[] numArray10 = new byte[55]
    {
      (byte) 231,
      (byte) 47,
      (byte) 230,
      (byte) 18,
      (byte) 113,
      (byte) 109,
      (byte) 9,
      (byte) 98,
      (byte) 102,
      (byte) 175,
      (byte) 229,
      byte.MaxValue,
      (byte) 238,
      (byte) 4,
      (byte) 104,
      (byte) 102,
      (byte) 135,
      (byte) 15,
      (byte) 109,
      (byte) 3,
      (byte) 94,
      (byte) 11,
      (byte) 25,
      (byte) 62,
      (byte) 206,
      (byte) 85,
      (byte) 183,
      (byte) 140,
      (byte) 41,
      (byte) 222,
      (byte) 170,
      (byte) 11,
      (byte) 226,
      (byte) 45,
      (byte) 77,
      (byte) 243,
      (byte) 134,
      (byte) 121,
      (byte) 211,
      (byte) 146,
      (byte) 196,
      (byte) 49,
      (byte) 49,
      (byte) 110,
      (byte) 14,
      (byte) 144 /*0x90*/,
      (byte) 18,
      (byte) 101,
      (byte) 205,
      (byte) 96 /*0x60*/,
      (byte) 227,
      (byte) 68,
      (byte) 19,
      (byte) 68,
      (byte) 238
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray8, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index] ^= numArray10[index];
    byte[] numArray11 = new byte[55]
    {
      (byte) 137,
      (byte) 232,
      (byte) 244,
      (byte) 166,
      (byte) 111,
      (byte) 196,
      (byte) 147,
      (byte) 162,
      (byte) 197,
      (byte) 18,
      (byte) 174,
      (byte) 15,
      (byte) 99,
      (byte) 242,
      (byte) 104,
      (byte) 14,
      (byte) 253,
      (byte) 110,
      (byte) 123,
      (byte) 40,
      (byte) 201,
      (byte) 100,
      (byte) 211,
      (byte) 192 /*0xC0*/,
      (byte) 190,
      (byte) 25,
      (byte) 6,
      (byte) 243,
      (byte) 50,
      (byte) 128 /*0x80*/,
      (byte) 8,
      (byte) 57,
      (byte) 31 /*0x1F*/,
      (byte) 137,
      (byte) 169,
      (byte) 143,
      (byte) 62,
      (byte) 251,
      (byte) 137,
      (byte) 183,
      (byte) 145,
      (byte) 195,
      (byte) 211,
      (byte) 19,
      (byte) 154,
      (byte) 103,
      (byte) 184,
      (byte) 15,
      (byte) 161,
      (byte) 247,
      (byte) 212,
      (byte) 140,
      (byte) 192 /*0xC0*/,
      (byte) 175,
      (byte) 238
    };
    byte[] numArray12 = new byte[55]
    {
      (byte) 181,
      (byte) 90,
      (byte) 172,
      (byte) 143,
      (byte) 252,
      (byte) 140,
      (byte) 58,
      (byte) 210,
      (byte) 201,
      (byte) 71,
      (byte) 58,
      (byte) 231,
      (byte) 221,
      (byte) 64 /*0x40*/,
      (byte) 107,
      byte.MaxValue,
      (byte) 134,
      (byte) 50,
      (byte) 229,
      (byte) 195,
      (byte) 119,
      (byte) 100,
      (byte) 61,
      (byte) 127 /*0x7F*/,
      (byte) 171,
      (byte) 128 /*0x80*/,
      (byte) 163,
      (byte) 37,
      (byte) 236,
      (byte) 208 /*0xD0*/,
      (byte) 90,
      (byte) 29,
      (byte) 206,
      (byte) 157,
      (byte) 108,
      (byte) 115,
      (byte) 187,
      (byte) 235,
      (byte) 8,
      (byte) 48 /*0x30*/,
      (byte) 40,
      (byte) 67,
      (byte) 220,
      (byte) 156,
      (byte) 32 /*0x20*/,
      (byte) 165,
      (byte) 100,
      (byte) 16 /*0x10*/,
      (byte) 229,
      (byte) 155,
      (byte) 192 /*0xC0*/,
      (byte) 99,
      (byte) 224 /*0xE0*/,
      (byte) 92,
      (byte) 46
    };
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray8, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index + 55] ^= numArray12[index];
    byte[] numArray13 = new byte[12]
    {
      (byte) 51,
      (byte) 231,
      (byte) 86,
      (byte) 189,
      (byte) 159,
      (byte) 112 /*0x70*/,
      (byte) 68,
      (byte) 9,
      (byte) 117,
      (byte) 240 /*0xF0*/,
      (byte) 218,
      (byte) 72
    };
    byte[] numArray14 = new byte[12]
    {
      (byte) 93,
      (byte) 86,
      (byte) 246,
      (byte) 209,
      (byte) 142,
      (byte) 223,
      (byte) 33,
      (byte) 91,
      (byte) 56,
      (byte) 24,
      (byte) 105,
      (byte) 215
    };
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray8, 110, 12);
    for (int index = 0; index < 12; ++index)
      numArray8[index + 110] ^= numArray14[index];
    return Encoding.UTF8.GetString(numArray8);
  }

  internal static int ssp_appserver_13312(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[18] = (byte) 20;
    sourceArray1[42] = (byte) 77;
    sourceArray1[2] = (byte) 145;
    sourceArray1[3] = (byte) 192 /*0xC0*/;
    sourceArray1[16 /*0x10*/] = (byte) 121;
    sourceArray1[26] = (byte) 119;
    sourceArray1[32 /*0x20*/] = (byte) 167;
    sourceArray1[46] = (byte) 252;
    sourceArray1[1] = (byte) 5;
    sourceArray1[9] = (byte) 58;
    sourceArray1[10] = (byte) 171;
    sourceArray1[4] = (byte) 38;
    sourceArray1[41] = (byte) 198;
    sourceArray1[13] = (byte) 111;
    sourceArray1[14] = (byte) 155;
    sourceArray1[15] = (byte) 133;
    sourceArray1[38] = (byte) 12;
    sourceArray1[8] = (byte) 82;
    sourceArray1[19] = (byte) 50;
    sourceArray1[23] = (byte) 142;
    sourceArray1[20] = (byte) 250;
    sourceArray1[31 /*0x1F*/] = (byte) 210;
    sourceArray1[22] = (byte) 83;
    sourceArray1[11] = (byte) 123;
    sourceArray1[39] = (byte) 143;
    sourceArray1[25] = (byte) 37;
    sourceArray1[24] = (byte) 9;
    sourceArray1[27] = (byte) 222;
    sourceArray1[28] = (byte) 198;
    sourceArray1[29] = (byte) 200;
    sourceArray1[6] = (byte) 96 /*0x60*/;
    sourceArray1[0] = (byte) 150;
    sourceArray1[5] = (byte) 87;
    sourceArray1[33] = (byte) 253;
    sourceArray1[34] = (byte) 24;
    sourceArray1[35] = (byte) 232;
    sourceArray1[36] = (byte) 147;
    sourceArray1[12] = (byte) 120;
    sourceArray1[21] = (byte) 241;
    sourceArray1[47] = (byte) 128 /*0x80*/;
    sourceArray1[40] = (byte) 245;
    sourceArray1[17] = (byte) 11;
    sourceArray1[7] = (byte) 247;
    sourceArray1[43] = (byte) 53;
    sourceArray1[44] = (byte) 152;
    sourceArray1[45] = (byte) 142;
    sourceArray1[37] = (byte) 85;
    sourceArray1[30] = (byte) 39;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[31 /*0x1F*/] = (byte) 201;
    sourceArray2[21] = (byte) 238;
    sourceArray2[28] = (byte) 122;
    sourceArray2[3] = (byte) 191;
    sourceArray2[36] = (byte) 162;
    sourceArray2[47] = (byte) 156;
    sourceArray2[41] = (byte) 56;
    sourceArray2[37] = (byte) 218;
    sourceArray2[4] = (byte) 189;
    sourceArray2[43] = (byte) 73;
    sourceArray2[5] = (byte) 175;
    sourceArray2[11] = (byte) 115;
    sourceArray2[12] = (byte) 22;
    sourceArray2[24] = (byte) 117;
    sourceArray2[8] = byte.MaxValue;
    sourceArray2[40] = (byte) 107;
    sourceArray2[16 /*0x10*/] = (byte) 231;
    sourceArray2[17] = (byte) 60;
    sourceArray2[18] = (byte) 42;
    sourceArray2[19] = (byte) 127 /*0x7F*/;
    sourceArray2[20] = (byte) 139;
    sourceArray2[7] = (byte) 123;
    sourceArray2[1] = (byte) 211;
    sourceArray2[6] = (byte) 72;
    sourceArray2[15] = (byte) 141;
    sourceArray2[39] = (byte) 192 /*0xC0*/;
    sourceArray2[14] = (byte) 197;
    sourceArray2[27] = (byte) 234;
    sourceArray2[45] = (byte) 170;
    sourceArray2[46] = (byte) 116;
    sourceArray2[30] = (byte) 229;
    sourceArray2[2] = (byte) 101;
    sourceArray2[32 /*0x20*/] = (byte) 178;
    sourceArray2[33] = (byte) 153;
    sourceArray2[22] = (byte) 15;
    sourceArray2[10] = (byte) 49;
    sourceArray2[29] = (byte) 110;
    sourceArray2[9] = (byte) 125;
    sourceArray2[38] = (byte) 143;
    sourceArray2[23] = (byte) 160 /*0xA0*/;
    sourceArray2[26] = (byte) 57;
    sourceArray2[13] = (byte) 86;
    sourceArray2[35] = (byte) 109;
    sourceArray2[25] = (byte) 184;
    sourceArray2[44] = (byte) 71;
    sourceArray2[42] = (byte) 76;
    sourceArray2[0] = (byte) 3;
    sourceArray2[34] = (byte) 30;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[10];
    byte[] response2 = new byte[10];
    Array.Copy((Array) sc_13302.sspq, 42, (Array) numArray2, 0, 10);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13302.sspr, 42, (Array) numArray2, 0, 10);
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

  internal static int ssp_appserver_13314(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[46] = (byte) 106;
    sourceArray1[1] = (byte) 210;
    sourceArray1[43] = (byte) 0;
    sourceArray1[36] = (byte) 229;
    sourceArray1[12] = (byte) 252;
    sourceArray1[19] = (byte) 215;
    sourceArray1[5] = (byte) 32 /*0x20*/;
    sourceArray1[30] = (byte) 110;
    sourceArray1[8] = (byte) 122;
    sourceArray1[9] = (byte) 49;
    sourceArray1[38] = (byte) 169;
    sourceArray1[16 /*0x10*/] = (byte) 97;
    sourceArray1[29] = (byte) 232;
    sourceArray1[45] = (byte) 192 /*0xC0*/;
    sourceArray1[34] = (byte) 120;
    sourceArray1[15] = (byte) 123;
    sourceArray1[26] = (byte) 62;
    sourceArray1[17] = (byte) 168;
    sourceArray1[14] = (byte) 19;
    sourceArray1[4] = (byte) 246;
    sourceArray1[18] = (byte) 112 /*0x70*/;
    sourceArray1[13] = (byte) 244;
    sourceArray1[22] = (byte) 194;
    sourceArray1[23] = (byte) 15;
    sourceArray1[24] = (byte) 57;
    sourceArray1[25] = (byte) 55;
    sourceArray1[10] = (byte) 28;
    sourceArray1[41] = (byte) 175;
    sourceArray1[28] = (byte) 108;
    sourceArray1[2] = (byte) 148;
    sourceArray1[7] = (byte) 213;
    sourceArray1[31 /*0x1F*/] = (byte) 135;
    sourceArray1[3] = (byte) 171;
    sourceArray1[0] = (byte) 180;
    sourceArray1[6] = (byte) 152;
    sourceArray1[35] = (byte) 201;
    sourceArray1[37] = (byte) 211;
    sourceArray1[33] = (byte) 191;
    sourceArray1[32 /*0x20*/] = (byte) 1;
    sourceArray1[39] = (byte) 82;
    sourceArray1[21] = (byte) 36;
    sourceArray1[44] = (byte) 169;
    sourceArray1[42] = (byte) 231;
    sourceArray1[27] = (byte) 136;
    sourceArray1[11] = (byte) 208 /*0xD0*/;
    sourceArray1[40] = (byte) 11;
    sourceArray1[20] = (byte) 219;
    sourceArray1[47] = (byte) 173;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 131,
      (byte) 93,
      (byte) 77,
      (byte) 184,
      (byte) 71,
      (byte) 211,
      (byte) 131,
      (byte) 225,
      (byte) 21,
      (byte) 196,
      (byte) 48 /*0x30*/,
      (byte) 237,
      (byte) 144 /*0x90*/,
      (byte) 211,
      (byte) 212,
      (byte) 47,
      (byte) 206,
      (byte) 59,
      (byte) 112 /*0x70*/,
      (byte) 187,
      (byte) 182,
      (byte) 207,
      (byte) 219,
      (byte) 236,
      (byte) 7,
      (byte) 117,
      (byte) 80 /*0x50*/,
      (byte) 241,
      (byte) 203,
      (byte) 80 /*0x50*/,
      (byte) 137,
      (byte) 113,
      (byte) 6,
      (byte) 220,
      (byte) 216,
      (byte) 105,
      (byte) 136,
      (byte) 159,
      (byte) 54,
      (byte) 13,
      (byte) 36,
      (byte) 28,
      (byte) 38,
      (byte) 164,
      (byte) 164,
      (byte) 230,
      (byte) 238,
      (byte) 7
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13315(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 45,
      (byte) 49,
      (byte) 20,
      (byte) 185,
      (byte) 234,
      (byte) 145,
      (byte) 184,
      (byte) 109,
      (byte) 104,
      (byte) 82,
      (byte) 78,
      (byte) 31 /*0x1F*/,
      (byte) 249,
      (byte) 241,
      (byte) 81,
      (byte) 230,
      (byte) 74,
      (byte) 34,
      (byte) 229,
      (byte) 228,
      (byte) 6,
      (byte) 18,
      (byte) 147,
      (byte) 146,
      (byte) 53,
      (byte) 44,
      (byte) 20,
      (byte) 174,
      (byte) 209,
      (byte) 229,
      (byte) 231,
      (byte) 132,
      (byte) 82,
      (byte) 134,
      (byte) 159,
      (byte) 95,
      (byte) 21,
      (byte) 101,
      (byte) 132,
      (byte) 75,
      (byte) 248,
      (byte) 90,
      (byte) 193,
      (byte) 149,
      (byte) 105,
      (byte) 64 /*0x40*/,
      (byte) 103,
      (byte) 248
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[13] = (byte) 42;
    sourceArray2[9] = (byte) 211;
    sourceArray2[2] = (byte) 219;
    sourceArray2[39] = (byte) 148;
    sourceArray2[4] = (byte) 232;
    sourceArray2[5] = (byte) 188;
    sourceArray2[6] = (byte) 247;
    sourceArray2[7] = (byte) 48 /*0x30*/;
    sourceArray2[41] = (byte) 15;
    sourceArray2[38] = (byte) 161;
    sourceArray2[10] = (byte) 202;
    sourceArray2[46] = (byte) 99;
    sourceArray2[3] = (byte) 220;
    sourceArray2[32 /*0x20*/] = (byte) 193;
    sourceArray2[1] = (byte) 122;
    sourceArray2[28] = (byte) 130;
    sourceArray2[16 /*0x10*/] = (byte) 22;
    sourceArray2[8] = (byte) 132;
    sourceArray2[37] = (byte) 4;
    sourceArray2[26] = (byte) 53;
    sourceArray2[42] = byte.MaxValue;
    sourceArray2[22] = (byte) 178;
    sourceArray2[12] = (byte) 208 /*0xD0*/;
    sourceArray2[23] = (byte) 63 /*0x3F*/;
    sourceArray2[19] = (byte) 13;
    sourceArray2[25] = (byte) 222;
    sourceArray2[21] = (byte) 192 /*0xC0*/;
    sourceArray2[27] = (byte) 105;
    sourceArray2[15] = (byte) 116;
    sourceArray2[29] = (byte) 246;
    sourceArray2[0] = (byte) 109;
    sourceArray2[35] = (byte) 196;
    sourceArray2[14] = (byte) 252;
    sourceArray2[33] = (byte) 52;
    sourceArray2[34] = (byte) 247;
    sourceArray2[45] = (byte) 67;
    sourceArray2[36] = (byte) 75;
    sourceArray2[20] = (byte) 56;
    sourceArray2[18] = (byte) 14;
    sourceArray2[24] = (byte) 0;
    sourceArray2[31 /*0x1F*/] = (byte) 157;
    sourceArray2[11] = (byte) 89;
    sourceArray2[40] = (byte) 45;
    sourceArray2[43] = (byte) 72;
    sourceArray2[44] = (byte) 28;
    sourceArray2[17] = (byte) 185;
    sourceArray2[30] = (byte) 90;
    sourceArray2[47] = (byte) 55;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13316(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[38] = (byte) 210;
    sourceArray1[28] = (byte) 253;
    sourceArray1[2] = (byte) 43;
    sourceArray1[37] = (byte) 120;
    sourceArray1[13] = (byte) 248;
    sourceArray1[46] = (byte) 83;
    sourceArray1[6] = (byte) 188;
    sourceArray1[19] = (byte) 156;
    sourceArray1[4] = (byte) 23;
    sourceArray1[9] = (byte) 201;
    sourceArray1[10] = (byte) 160 /*0xA0*/;
    sourceArray1[11] = (byte) 89;
    sourceArray1[22] = (byte) 205;
    sourceArray1[7] = (byte) 67;
    sourceArray1[14] = (byte) 96 /*0x60*/;
    sourceArray1[15] = (byte) 120;
    sourceArray1[16 /*0x10*/] = (byte) 223;
    sourceArray1[17] = (byte) 153;
    sourceArray1[18] = (byte) 182;
    sourceArray1[1] = (byte) 166;
    sourceArray1[30] = (byte) 18;
    sourceArray1[29] = (byte) 237;
    sourceArray1[36] = (byte) 101;
    sourceArray1[39] = (byte) 31 /*0x1F*/;
    sourceArray1[8] = (byte) 63 /*0x3F*/;
    sourceArray1[34] = (byte) 208 /*0xD0*/;
    sourceArray1[26] = (byte) 214;
    sourceArray1[44] = (byte) 32 /*0x20*/;
    sourceArray1[27] = (byte) 204;
    sourceArray1[5] = (byte) 149;
    sourceArray1[21] = (byte) 215;
    sourceArray1[23] = (byte) 48 /*0x30*/;
    sourceArray1[32 /*0x20*/] = (byte) 136;
    sourceArray1[33] = (byte) 130;
    sourceArray1[41] = (byte) 223;
    sourceArray1[35] = (byte) 118;
    sourceArray1[12] = (byte) 141;
    sourceArray1[25] = (byte) 3;
    sourceArray1[20] = (byte) 170;
    sourceArray1[3] = (byte) 67;
    sourceArray1[40] = (byte) 112 /*0x70*/;
    sourceArray1[47] = (byte) 206;
    sourceArray1[42] = (byte) 92;
    sourceArray1[24] = (byte) 61;
    sourceArray1[0] = (byte) 244;
    sourceArray1[45] = (byte) 57;
    sourceArray1[31 /*0x1F*/] = (byte) 210;
    sourceArray1[43] = (byte) 186;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 116,
      (byte) 224 /*0xE0*/,
      (byte) 201,
      (byte) 56,
      (byte) 228,
      (byte) 60,
      (byte) 121,
      (byte) 81,
      (byte) 219,
      (byte) 223,
      (byte) 19,
      (byte) 86,
      (byte) 90,
      (byte) 105,
      (byte) 152,
      (byte) 243,
      (byte) 161,
      (byte) 175,
      (byte) 223,
      (byte) 9,
      (byte) 236,
      (byte) 176 /*0xB0*/,
      (byte) 197,
      (byte) 166,
      (byte) 160 /*0xA0*/,
      (byte) 50,
      (byte) 75,
      (byte) 20,
      (byte) 220,
      (byte) 56,
      (byte) 129,
      (byte) 55,
      (byte) 209,
      (byte) 186,
      (byte) 216,
      (byte) 184,
      (byte) 246,
      (byte) 186,
      (byte) 109,
      (byte) 240 /*0xF0*/,
      (byte) 217,
      (byte) 206,
      (byte) 74,
      (byte) 0,
      (byte) 234,
      (byte) 0,
      (byte) 224 /*0xE0*/,
      (byte) 110
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13317(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[4] = (byte) 120;
    sourceArray1[1] = (byte) 167;
    sourceArray1[2] = (byte) 131;
    sourceArray1[3] = (byte) 98;
    sourceArray1[23] = (byte) 76;
    sourceArray1[20] = (byte) 75;
    sourceArray1[6] = (byte) 220;
    sourceArray1[42] = (byte) 188;
    sourceArray1[8] = (byte) 253;
    sourceArray1[0] = (byte) 72;
    sourceArray1[10] = (byte) 198;
    sourceArray1[25] = (byte) 243;
    sourceArray1[12] = (byte) 2;
    sourceArray1[13] = (byte) 100;
    sourceArray1[14] = (byte) 183;
    sourceArray1[16 /*0x10*/] = (byte) 191;
    sourceArray1[39] = (byte) 145;
    sourceArray1[26] = (byte) 8;
    sourceArray1[43] = (byte) 16 /*0x10*/;
    sourceArray1[19] = (byte) 110;
    sourceArray1[28] = (byte) 170;
    sourceArray1[21] = (byte) 81;
    sourceArray1[22] = (byte) 116;
    sourceArray1[33] = (byte) 155;
    sourceArray1[15] = (byte) 146;
    sourceArray1[44] = (byte) 215;
    sourceArray1[37] = (byte) 36;
    sourceArray1[27] = (byte) 54;
    sourceArray1[38] = (byte) 150;
    sourceArray1[29] = (byte) 50;
    sourceArray1[30] = (byte) 113;
    sourceArray1[5] = (byte) 220;
    sourceArray1[41] = (byte) 173;
    sourceArray1[7] = (byte) 215;
    sourceArray1[34] = (byte) 103;
    sourceArray1[35] = (byte) 96 /*0x60*/;
    sourceArray1[36] = (byte) 218;
    sourceArray1[40] = (byte) 130;
    sourceArray1[32 /*0x20*/] = (byte) 188;
    sourceArray1[11] = (byte) 234;
    sourceArray1[17] = (byte) 67;
    sourceArray1[18] = (byte) 73;
    sourceArray1[45] = (byte) 129;
    sourceArray1[9] = (byte) 162;
    sourceArray1[31 /*0x1F*/] = (byte) 15;
    sourceArray1[24] = (byte) 167;
    sourceArray1[46] = (byte) 112 /*0x70*/;
    sourceArray1[47] = (byte) 90;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 119,
      (byte) 225,
      (byte) 58,
      (byte) 86,
      (byte) 130,
      (byte) 38,
      (byte) 217,
      (byte) 135,
      (byte) 155,
      (byte) 252,
      (byte) 105,
      (byte) 74,
      (byte) 111,
      (byte) 115,
      (byte) 24,
      (byte) 226,
      (byte) 93,
      (byte) 126,
      (byte) 55,
      (byte) 48 /*0x30*/,
      (byte) 30,
      (byte) 132,
      (byte) 198,
      (byte) 34,
      (byte) 83,
      (byte) 185,
      (byte) 219,
      (byte) 228,
      (byte) 152,
      (byte) 87,
      (byte) 96 /*0x60*/,
      (byte) 252,
      (byte) 9,
      (byte) 150,
      (byte) 222,
      (byte) 80 /*0x50*/,
      (byte) 58,
      (byte) 143,
      (byte) 168,
      (byte) 37,
      (byte) 121,
      (byte) 166,
      (byte) 148,
      (byte) 225,
      (byte) 82,
      (byte) 66,
      (byte) 72,
      (byte) 159
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13319(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[4] = (byte) 62;
    sourceArray1[1] = (byte) 93;
    sourceArray1[17] = (byte) 114;
    sourceArray1[11] = (byte) 194;
    sourceArray1[33] = (byte) 209;
    sourceArray1[5] = (byte) 26;
    sourceArray1[6] = (byte) 1;
    sourceArray1[7] = (byte) 248;
    sourceArray1[8] = (byte) 127 /*0x7F*/;
    sourceArray1[14] = (byte) 37;
    sourceArray1[36] = (byte) 56;
    sourceArray1[44] = (byte) 95;
    sourceArray1[12] = (byte) 150;
    sourceArray1[13] = (byte) 1;
    sourceArray1[3] = (byte) 54;
    sourceArray1[38] = (byte) 26;
    sourceArray1[16 /*0x10*/] = (byte) 112 /*0x70*/;
    sourceArray1[18] = (byte) 135;
    sourceArray1[25] = (byte) 51;
    sourceArray1[27] = (byte) 251;
    sourceArray1[20] = (byte) 85;
    sourceArray1[19] = (byte) 97;
    sourceArray1[22] = (byte) 140;
    sourceArray1[21] = (byte) 233;
    sourceArray1[43] = (byte) 16 /*0x10*/;
    sourceArray1[23] = (byte) 40;
    sourceArray1[26] = (byte) 155;
    sourceArray1[32 /*0x20*/] = (byte) 83;
    sourceArray1[47] = (byte) 16 /*0x10*/;
    sourceArray1[29] = (byte) 29;
    sourceArray1[30] = (byte) 164;
    sourceArray1[15] = (byte) 26;
    sourceArray1[2] = (byte) 83;
    sourceArray1[0] = (byte) 9;
    sourceArray1[46] = (byte) 209;
    sourceArray1[35] = (byte) 80 /*0x50*/;
    sourceArray1[34] = (byte) 122;
    sourceArray1[37] = (byte) 134;
    sourceArray1[24] = (byte) 20;
    sourceArray1[39] = (byte) 143;
    sourceArray1[40] = (byte) 84;
    sourceArray1[41] = (byte) 223;
    sourceArray1[42] = (byte) 254;
    sourceArray1[9] = (byte) 96 /*0x60*/;
    sourceArray1[28] = (byte) 17;
    sourceArray1[45] = (byte) 223;
    sourceArray1[31 /*0x1F*/] = (byte) 132;
    sourceArray1[10] = (byte) 206;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[30] = (byte) 154;
    sourceArray2[3] = (byte) 126;
    sourceArray2[2] = (byte) 75;
    sourceArray2[15] = (byte) 235;
    sourceArray2[4] = (byte) 35;
    sourceArray2[29] = (byte) 118;
    sourceArray2[13] = (byte) 104;
    sourceArray2[42] = (byte) 131;
    sourceArray2[33] = (byte) 28;
    sourceArray2[9] = (byte) 101;
    sourceArray2[10] = (byte) 52;
    sourceArray2[39] = (byte) 84;
    sourceArray2[12] = (byte) 38;
    sourceArray2[25] = (byte) 37;
    sourceArray2[6] = (byte) 193;
    sourceArray2[19] = (byte) 46;
    sourceArray2[16 /*0x10*/] = (byte) 239;
    sourceArray2[36] = (byte) 60;
    sourceArray2[18] = (byte) 193;
    sourceArray2[23] = (byte) 220;
    sourceArray2[20] = (byte) 184;
    sourceArray2[21] = (byte) 179;
    sourceArray2[22] = (byte) 56;
    sourceArray2[37] = (byte) 227;
    sourceArray2[24] = (byte) 174;
    sourceArray2[44] = (byte) 112 /*0x70*/;
    sourceArray2[26] = (byte) 29;
    sourceArray2[27] = (byte) 155;
    sourceArray2[41] = (byte) 37;
    sourceArray2[5] = (byte) 155;
    sourceArray2[0] = (byte) 163;
    sourceArray2[31 /*0x1F*/] = (byte) 115;
    sourceArray2[32 /*0x20*/] = (byte) 117;
    sourceArray2[7] = (byte) 142;
    sourceArray2[34] = (byte) 163;
    sourceArray2[35] = (byte) 134;
    sourceArray2[43] = (byte) 197;
    sourceArray2[38] = (byte) 76;
    sourceArray2[28] = (byte) 120;
    sourceArray2[1] = (byte) 198;
    sourceArray2[14] = (byte) 191;
    sourceArray2[40] = (byte) 138;
    sourceArray2[17] = (byte) 211;
    sourceArray2[8] = (byte) 91;
    sourceArray2[11] = (byte) 100;
    sourceArray2[45] = (byte) 199;
    sourceArray2[46] = (byte) 204;
    sourceArray2[47] = (byte) 78;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13320(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[24] = (byte) 112 /*0x70*/;
    sourceArray1[1] = (byte) 173;
    sourceArray1[44] = (byte) 100;
    sourceArray1[15] = (byte) 11;
    sourceArray1[22] = (byte) 253;
    sourceArray1[35] = (byte) 17;
    sourceArray1[6] = (byte) 149;
    sourceArray1[19] = (byte) 39;
    sourceArray1[13] = (byte) 122;
    sourceArray1[45] = (byte) 66;
    sourceArray1[34] = (byte) 202;
    sourceArray1[20] = (byte) 85;
    sourceArray1[12] = (byte) 223;
    sourceArray1[43] = (byte) 57;
    sourceArray1[14] = (byte) 243;
    sourceArray1[9] = (byte) 105;
    sourceArray1[10] = (byte) 11;
    sourceArray1[17] = (byte) 46;
    sourceArray1[18] = (byte) 176 /*0xB0*/;
    sourceArray1[47] = (byte) 80 /*0x50*/;
    sourceArray1[38] = (byte) 104;
    sourceArray1[21] = (byte) 243;
    sourceArray1[11] = (byte) 162;
    sourceArray1[23] = (byte) 38;
    sourceArray1[25] = (byte) 150;
    sourceArray1[8] = (byte) 93;
    sourceArray1[26] = (byte) 83;
    sourceArray1[41] = (byte) 58;
    sourceArray1[28] = (byte) 167;
    sourceArray1[29] = (byte) 6;
    sourceArray1[2] = (byte) 97;
    sourceArray1[31 /*0x1F*/] = (byte) 41;
    sourceArray1[16 /*0x10*/] = (byte) 231;
    sourceArray1[33] = (byte) 226;
    sourceArray1[40] = (byte) 47;
    sourceArray1[27] = (byte) 249;
    sourceArray1[36] = (byte) 27;
    sourceArray1[37] = (byte) 6;
    sourceArray1[0] = (byte) 28;
    sourceArray1[7] = (byte) 26;
    sourceArray1[5] = (byte) 246;
    sourceArray1[30] = (byte) 46;
    sourceArray1[42] = (byte) 163;
    sourceArray1[39] = (byte) 188;
    sourceArray1[3] = (byte) 71;
    sourceArray1[4] = (byte) 191;
    sourceArray1[46] = (byte) 31 /*0x1F*/;
    sourceArray1[32 /*0x20*/] = (byte) 11;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[42] = (byte) 209;
    sourceArray2[33] = (byte) 109;
    sourceArray2[0] = (byte) 196;
    sourceArray2[3] = (byte) 58;
    sourceArray2[23] = (byte) 7;
    sourceArray2[29] = (byte) 243;
    sourceArray2[8] = (byte) 43;
    sourceArray2[10] = (byte) 145;
    sourceArray2[24] = (byte) 95;
    sourceArray2[9] = (byte) 123;
    sourceArray2[6] = (byte) 180;
    sourceArray2[11] = (byte) 1;
    sourceArray2[26] = (byte) 247;
    sourceArray2[1] = (byte) 209;
    sourceArray2[14] = (byte) 71;
    sourceArray2[5] = (byte) 225;
    sourceArray2[21] = (byte) 33;
    sourceArray2[43] = (byte) 244;
    sourceArray2[18] = (byte) 90;
    sourceArray2[19] = (byte) 152;
    sourceArray2[47] = (byte) 59;
    sourceArray2[20] = (byte) 227;
    sourceArray2[22] = (byte) 128 /*0x80*/;
    sourceArray2[46] = (byte) 6;
    sourceArray2[12] = (byte) 224 /*0xE0*/;
    sourceArray2[25] = (byte) 150;
    sourceArray2[4] = (byte) 94;
    sourceArray2[27] = (byte) 89;
    sourceArray2[28] = (byte) 235;
    sourceArray2[40] = (byte) 103;
    sourceArray2[30] = (byte) 2;
    sourceArray2[15] = (byte) 230;
    sourceArray2[32 /*0x20*/] = (byte) 145;
    sourceArray2[44] = (byte) 187;
    sourceArray2[13] = (byte) 117;
    sourceArray2[16 /*0x10*/] = (byte) 227;
    sourceArray2[45] = (byte) 66;
    sourceArray2[17] = (byte) 179;
    sourceArray2[41] = (byte) 154;
    sourceArray2[39] = (byte) 111;
    sourceArray2[31 /*0x1F*/] = (byte) 0;
    sourceArray2[34] = (byte) 29;
    sourceArray2[36] = (byte) 122;
    sourceArray2[35] = (byte) 8;
    sourceArray2[38] = (byte) 42;
    sourceArray2[37] = (byte) 246;
    sourceArray2[2] = (byte) 121;
    sourceArray2[7] = (byte) 20;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13321(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 122,
      (byte) 2,
      (byte) 189,
      (byte) 189,
      (byte) 249,
      (byte) 206,
      (byte) 205,
      (byte) 247,
      (byte) 146,
      (byte) 142,
      (byte) 65,
      (byte) 173,
      (byte) 171,
      (byte) 43,
      (byte) 244,
      (byte) 163,
      (byte) 212,
      (byte) 38,
      (byte) 55,
      (byte) 39,
      (byte) 92,
      (byte) 61,
      (byte) 148,
      (byte) 241,
      (byte) 37,
      (byte) 2,
      (byte) 138,
      (byte) 125,
      (byte) 79,
      (byte) 70,
      (byte) 9,
      (byte) 165,
      (byte) 42,
      (byte) 248,
      (byte) 96 /*0x60*/,
      (byte) 97,
      (byte) 148,
      (byte) 71,
      (byte) 203,
      (byte) 114,
      (byte) 181,
      (byte) 92,
      (byte) 208 /*0xD0*/,
      (byte) 110,
      (byte) 214,
      (byte) 188,
      (byte) 87,
      (byte) 253
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 184,
      (byte) 82,
      (byte) 197,
      (byte) 21,
      (byte) 104,
      (byte) 211,
      (byte) 59,
      (byte) 134,
      (byte) 158,
      (byte) 124,
      (byte) 127 /*0x7F*/,
      (byte) 168,
      (byte) 64 /*0x40*/,
      (byte) 5,
      (byte) 65,
      (byte) 122,
      (byte) 124,
      (byte) 182,
      (byte) 7,
      (byte) 231,
      (byte) 31 /*0x1F*/,
      (byte) 194,
      (byte) 34,
      (byte) 218,
      (byte) 169,
      (byte) 95,
      (byte) 83,
      (byte) 134,
      (byte) 91,
      (byte) 199,
      (byte) 66,
      (byte) 15,
      (byte) 131,
      (byte) 162,
      (byte) 223,
      (byte) 174,
      (byte) 136,
      (byte) 56,
      (byte) 74,
      (byte) 252,
      (byte) 180,
      (byte) 66,
      (byte) 65,
      (byte) 77,
      (byte) 34,
      (byte) 138,
      (byte) 153,
      (byte) 72
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13322(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 94,
      (byte) 50,
      (byte) 60,
      (byte) 110,
      (byte) 204,
      (byte) 151,
      (byte) 246,
      (byte) 68,
      (byte) 79,
      (byte) 168,
      (byte) 75,
      (byte) 132,
      (byte) 156,
      (byte) 131,
      (byte) 179,
      (byte) 203,
      (byte) 146,
      (byte) 15,
      (byte) 140,
      (byte) 91,
      (byte) 197,
      (byte) 113,
      (byte) 0,
      (byte) 108,
      (byte) 37,
      (byte) 27,
      (byte) 105,
      (byte) 171,
      (byte) 141,
      (byte) 17,
      (byte) 22,
      (byte) 82,
      (byte) 186,
      (byte) 116,
      (byte) 203,
      (byte) 197,
      (byte) 35,
      (byte) 243,
      (byte) 200,
      (byte) 175,
      (byte) 213,
      (byte) 45,
      (byte) 101,
      (byte) 179,
      (byte) 132,
      (byte) 233,
      (byte) 57,
      (byte) 56
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[8] = byte.MaxValue;
    sourceArray2[1] = (byte) 82;
    sourceArray2[26] = (byte) 6;
    sourceArray2[3] = (byte) 126;
    sourceArray2[20] = (byte) 70;
    sourceArray2[46] = (byte) 236;
    sourceArray2[6] = (byte) 75;
    sourceArray2[43] = (byte) 197;
    sourceArray2[27] = (byte) 203;
    sourceArray2[9] = (byte) 33;
    sourceArray2[33] = (byte) 50;
    sourceArray2[21] = (byte) 203;
    sourceArray2[12] = (byte) 26;
    sourceArray2[13] = (byte) 72;
    sourceArray2[14] = (byte) 165;
    sourceArray2[15] = (byte) 220;
    sourceArray2[16 /*0x10*/] = (byte) 49;
    sourceArray2[17] = (byte) 126;
    sourceArray2[45] = (byte) 158;
    sourceArray2[19] = (byte) 43;
    sourceArray2[11] = (byte) 141;
    sourceArray2[38] = (byte) 79;
    sourceArray2[5] = (byte) 147;
    sourceArray2[30] = (byte) 16 /*0x10*/;
    sourceArray2[24] = (byte) 194;
    sourceArray2[25] = (byte) 13;
    sourceArray2[42] = (byte) 28;
    sourceArray2[32 /*0x20*/] = (byte) 166;
    sourceArray2[28] = (byte) 106;
    sourceArray2[29] = (byte) 51;
    sourceArray2[44] = (byte) 37;
    sourceArray2[2] = (byte) 111;
    sourceArray2[34] = (byte) 138;
    sourceArray2[0] = (byte) 224 /*0xE0*/;
    sourceArray2[18] = (byte) 31 /*0x1F*/;
    sourceArray2[31 /*0x1F*/] = (byte) 162;
    sourceArray2[36] = (byte) 38;
    sourceArray2[37] = (byte) 248;
    sourceArray2[4] = (byte) 11;
    sourceArray2[39] = (byte) 105;
    sourceArray2[23] = (byte) 93;
    sourceArray2[41] = (byte) 14;
    sourceArray2[7] = (byte) 195;
    sourceArray2[22] = (byte) 212;
    sourceArray2[10] = (byte) 218;
    sourceArray2[40] = (byte) 207;
    sourceArray2[35] = (byte) 191;
    sourceArray2[47] = (byte) 136;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13323(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[7] = (byte) 110;
    sourceArray1[12] = (byte) 13;
    sourceArray1[5] = (byte) 226;
    sourceArray1[3] = (byte) 137;
    sourceArray1[4] = (byte) 57;
    sourceArray1[30] = (byte) 228;
    sourceArray1[18] = (byte) 164;
    sourceArray1[22] = (byte) 217;
    sourceArray1[8] = (byte) 211;
    sourceArray1[17] = (byte) 7;
    sourceArray1[37] = (byte) 209;
    sourceArray1[10] = (byte) 229;
    sourceArray1[19] = (byte) 14;
    sourceArray1[43] = (byte) 252;
    sourceArray1[1] = (byte) 129;
    sourceArray1[15] = (byte) 66;
    sourceArray1[46] = (byte) 157;
    sourceArray1[31 /*0x1F*/] = (byte) 42;
    sourceArray1[47] = (byte) 73;
    sourceArray1[6] = (byte) 138;
    sourceArray1[20] = (byte) 3;
    sourceArray1[21] = (byte) 30;
    sourceArray1[0] = (byte) 18;
    sourceArray1[23] = (byte) 89;
    sourceArray1[24] = (byte) 84;
    sourceArray1[2] = (byte) 182;
    sourceArray1[26] = (byte) 46;
    sourceArray1[36] = (byte) 82;
    sourceArray1[44] = (byte) 173;
    sourceArray1[33] = (byte) 27;
    sourceArray1[14] = (byte) 254;
    sourceArray1[9] = (byte) 225;
    sourceArray1[32 /*0x20*/] = (byte) 92;
    sourceArray1[28] = (byte) 250;
    sourceArray1[34] = (byte) 207;
    sourceArray1[35] = (byte) 175;
    sourceArray1[16 /*0x10*/] = (byte) 91;
    sourceArray1[27] = (byte) 92;
    sourceArray1[38] = (byte) 253;
    sourceArray1[39] = (byte) 10;
    sourceArray1[40] = (byte) 96 /*0x60*/;
    sourceArray1[25] = (byte) 244;
    sourceArray1[42] = (byte) 53;
    sourceArray1[29] = (byte) 32 /*0x20*/;
    sourceArray1[41] = (byte) 88;
    sourceArray1[45] = (byte) 107;
    sourceArray1[11] = (byte) 231;
    sourceArray1[13] = (byte) 206;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 25,
      (byte) 39,
      (byte) 23,
      (byte) 103,
      (byte) 20,
      (byte) 152,
      (byte) 93,
      (byte) 2,
      (byte) 142,
      (byte) 57,
      (byte) 173,
      (byte) 103,
      (byte) 144 /*0x90*/,
      (byte) 246,
      (byte) 226,
      (byte) 29,
      (byte) 89,
      (byte) 19,
      (byte) 6,
      (byte) 207,
      (byte) 9,
      (byte) 72,
      (byte) 45,
      (byte) 155,
      (byte) 157,
      (byte) 123,
      (byte) 151,
      (byte) 224 /*0xE0*/,
      (byte) 109,
      (byte) 70,
      (byte) 13,
      (byte) 195,
      (byte) 246,
      (byte) 243,
      (byte) 127 /*0x7F*/,
      (byte) 135,
      (byte) 86,
      (byte) 181,
      (byte) 195,
      (byte) 249,
      (byte) 132,
      (byte) 169,
      (byte) 57,
      (byte) 40,
      (byte) 7,
      (byte) 206,
      (byte) 81,
      (byte) 50
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13324(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 172,
      (byte) 73,
      (byte) 50,
      (byte) 88,
      (byte) 146,
      (byte) 22,
      (byte) 227,
      (byte) 207,
      (byte) 112 /*0x70*/,
      (byte) 9,
      (byte) 202,
      (byte) 176 /*0xB0*/,
      (byte) 76,
      (byte) 238,
      (byte) 206,
      (byte) 83,
      (byte) 193,
      (byte) 174,
      (byte) 55,
      (byte) 13,
      (byte) 172,
      (byte) 241,
      (byte) 35,
      (byte) 72,
      (byte) 250,
      (byte) 236,
      (byte) 81,
      (byte) 66,
      (byte) 122,
      (byte) 199,
      (byte) 103,
      (byte) 222,
      (byte) 185,
      (byte) 8,
      (byte) 169,
      (byte) 156,
      (byte) 168,
      (byte) 90,
      (byte) 113,
      (byte) 175,
      (byte) 202,
      (byte) 15,
      (byte) 143,
      (byte) 210,
      (byte) 60,
      (byte) 69,
      (byte) 80 /*0x50*/,
      (byte) 201
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 101,
      (byte) 57,
      (byte) 57,
      (byte) 170,
      (byte) 243,
      (byte) 67,
      (byte) 44,
      (byte) 114,
      (byte) 165,
      (byte) 228,
      (byte) 82,
      (byte) 13,
      (byte) 93,
      (byte) 133,
      (byte) 72,
      (byte) 155,
      (byte) 125,
      (byte) 252,
      (byte) 183,
      (byte) 102,
      (byte) 79,
      (byte) 164,
      (byte) 198,
      (byte) 225,
      (byte) 148,
      (byte) 214,
      (byte) 60,
      (byte) 184,
      (byte) 254,
      (byte) 0,
      (byte) 201,
      (byte) 113,
      (byte) 55,
      (byte) 55,
      (byte) 29,
      (byte) 30,
      (byte) 170,
      (byte) 72,
      (byte) 96 /*0x60*/,
      (byte) 78,
      (byte) 86,
      (byte) 156,
      (byte) 7,
      (byte) 179,
      (byte) 116,
      (byte) 170,
      (byte) 146,
      (byte) 29
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13325(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[34] = (byte) 212;
    sourceArray1[9] = (byte) 84;
    sourceArray1[45] = (byte) 48 /*0x30*/;
    sourceArray1[3] = (byte) 38;
    sourceArray1[4] = (byte) 233;
    sourceArray1[0] = (byte) 228;
    sourceArray1[6] = (byte) 166;
    sourceArray1[7] = (byte) 197;
    sourceArray1[5] = (byte) 93;
    sourceArray1[23] = (byte) 97;
    sourceArray1[10] = (byte) 168;
    sourceArray1[11] = (byte) 162;
    sourceArray1[12] = (byte) 236;
    sourceArray1[37] = (byte) 117;
    sourceArray1[27] = (byte) 156;
    sourceArray1[24] = (byte) 80 /*0x50*/;
    sourceArray1[42] = (byte) 41;
    sourceArray1[2] = (byte) 187;
    sourceArray1[18] = (byte) 103;
    sourceArray1[19] = (byte) 61;
    sourceArray1[1] = (byte) 51;
    sourceArray1[15] = (byte) 69;
    sourceArray1[29] = (byte) 185;
    sourceArray1[13] = (byte) 247;
    sourceArray1[44] = (byte) 230;
    sourceArray1[25] = (byte) 11;
    sourceArray1[22] = (byte) 156;
    sourceArray1[26] = (byte) 181;
    sourceArray1[28] = (byte) 135;
    sourceArray1[8] = (byte) 160 /*0xA0*/;
    sourceArray1[30] = (byte) 154;
    sourceArray1[31 /*0x1F*/] = (byte) 165;
    sourceArray1[16 /*0x10*/] = (byte) 50;
    sourceArray1[32 /*0x20*/] = (byte) 148;
    sourceArray1[46] = (byte) 80 /*0x50*/;
    sourceArray1[35] = (byte) 213;
    sourceArray1[36] = (byte) 238;
    sourceArray1[40] = (byte) 203;
    sourceArray1[21] = (byte) 67;
    sourceArray1[39] = (byte) 252;
    sourceArray1[33] = (byte) 22;
    sourceArray1[20] = (byte) 136;
    sourceArray1[41] = (byte) 234;
    sourceArray1[43] = (byte) 186;
    sourceArray1[14] = (byte) 67;
    sourceArray1[17] = (byte) 170;
    sourceArray1[38] = (byte) 190;
    sourceArray1[47] = (byte) 78;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[7] = (byte) 10;
    sourceArray2[1] = (byte) 82;
    sourceArray2[22] = (byte) 231;
    sourceArray2[25] = (byte) 222;
    sourceArray2[4] = (byte) 187;
    sourceArray2[27] = (byte) 243;
    sourceArray2[3] = (byte) 216;
    sourceArray2[26] = (byte) 81;
    sourceArray2[8] = (byte) 204;
    sourceArray2[9] = (byte) 219;
    sourceArray2[24] = (byte) 106;
    sourceArray2[11] = (byte) 184;
    sourceArray2[36] = (byte) 179;
    sourceArray2[17] = (byte) 91;
    sourceArray2[14] = (byte) 170;
    sourceArray2[20] = (byte) 203;
    sourceArray2[16 /*0x10*/] = (byte) 246;
    sourceArray2[6] = (byte) 13;
    sourceArray2[18] = (byte) 83;
    sourceArray2[19] = (byte) 132;
    sourceArray2[13] = (byte) 84;
    sourceArray2[46] = (byte) 244;
    sourceArray2[23] = (byte) 253;
    sourceArray2[38] = (byte) 80 /*0x50*/;
    sourceArray2[15] = (byte) 252;
    sourceArray2[32 /*0x20*/] = (byte) 58;
    sourceArray2[21] = (byte) 3;
    sourceArray2[45] = (byte) 83;
    sourceArray2[29] = (byte) 248;
    sourceArray2[0] = (byte) 174;
    sourceArray2[30] = (byte) 116;
    sourceArray2[31 /*0x1F*/] = (byte) 166;
    sourceArray2[10] = (byte) 219;
    sourceArray2[33] = (byte) 70;
    sourceArray2[34] = (byte) 2;
    sourceArray2[35] = (byte) 203;
    sourceArray2[42] = (byte) 3;
    sourceArray2[37] = (byte) 104;
    sourceArray2[12] = (byte) 56;
    sourceArray2[39] = (byte) 153;
    sourceArray2[40] = (byte) 250;
    sourceArray2[41] = (byte) 176 /*0xB0*/;
    sourceArray2[2] = (byte) 169;
    sourceArray2[44] = (byte) 220;
    sourceArray2[5] = (byte) 12;
    sourceArray2[43] = (byte) 34;
    sourceArray2[28] = (byte) 223;
    sourceArray2[47] = (byte) 218;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13326(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[12] = (byte) 60;
    sourceArray1[29] = (byte) 66;
    sourceArray1[2] = (byte) 114;
    sourceArray1[23] = (byte) 99;
    sourceArray1[4] = (byte) 171;
    sourceArray1[5] = (byte) 201;
    sourceArray1[0] = (byte) 18;
    sourceArray1[34] = (byte) 4;
    sourceArray1[8] = (byte) 61;
    sourceArray1[13] = (byte) 57;
    sourceArray1[9] = (byte) 109;
    sourceArray1[11] = (byte) 125;
    sourceArray1[1] = (byte) 70;
    sourceArray1[45] = (byte) 74;
    sourceArray1[30] = (byte) 168;
    sourceArray1[25] = (byte) 57;
    sourceArray1[15] = (byte) 175;
    sourceArray1[32 /*0x20*/] = (byte) 51;
    sourceArray1[43] = (byte) 131;
    sourceArray1[19] = (byte) 184;
    sourceArray1[20] = (byte) 226;
    sourceArray1[14] = (byte) 113;
    sourceArray1[22] = (byte) 137;
    sourceArray1[47] = (byte) 96 /*0x60*/;
    sourceArray1[24] = (byte) 188;
    sourceArray1[40] = (byte) 233;
    sourceArray1[38] = (byte) 54;
    sourceArray1[27] = (byte) 50;
    sourceArray1[28] = (byte) 199;
    sourceArray1[3] = (byte) 190;
    sourceArray1[17] = (byte) 212;
    sourceArray1[26] = (byte) 127 /*0x7F*/;
    sourceArray1[21] = (byte) 7;
    sourceArray1[33] = (byte) 79;
    sourceArray1[6] = (byte) 19;
    sourceArray1[18] = (byte) 72;
    sourceArray1[36] = (byte) 126;
    sourceArray1[37] = (byte) 25;
    sourceArray1[39] = (byte) 206;
    sourceArray1[44] = (byte) 108;
    sourceArray1[31 /*0x1F*/] = (byte) 14;
    sourceArray1[16 /*0x10*/] = (byte) 175;
    sourceArray1[42] = (byte) 252;
    sourceArray1[41] = (byte) 196;
    sourceArray1[35] = (byte) 107;
    sourceArray1[7] = (byte) 124;
    sourceArray1[46] = (byte) 223;
    sourceArray1[10] = (byte) 106;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 76,
      (byte) 56,
      (byte) 74,
      (byte) 18,
      (byte) 117,
      (byte) 58,
      (byte) 205,
      (byte) 161,
      (byte) 21,
      (byte) 138,
      (byte) 34,
      (byte) 59,
      (byte) 196,
      (byte) 188,
      (byte) 45,
      (byte) 16 /*0x10*/,
      (byte) 13,
      (byte) 207,
      (byte) 109,
      (byte) 166,
      (byte) 217,
      (byte) 77,
      (byte) 126,
      (byte) 197,
      (byte) 13,
      (byte) 211,
      (byte) 156,
      (byte) 156,
      (byte) 98,
      (byte) 157,
      (byte) 70,
      (byte) 177,
      (byte) 250,
      (byte) 51,
      (byte) 236,
      (byte) 28,
      (byte) 30,
      (byte) 233,
      (byte) 192 /*0xC0*/,
      (byte) 95,
      (byte) 79,
      (byte) 95,
      (byte) 165,
      (byte) 137,
      (byte) 80 /*0x50*/,
      (byte) 107,
      (byte) 213,
      (byte) 128 /*0x80*/
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13327(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 241,
      (byte) 43,
      (byte) 100,
      (byte) 86,
      (byte) 102,
      (byte) 22,
      (byte) 242,
      (byte) 98,
      (byte) 187,
      (byte) 0,
      (byte) 182,
      (byte) 23,
      (byte) 143,
      (byte) 24,
      (byte) 41,
      (byte) 24,
      (byte) 115,
      (byte) 182,
      (byte) 208 /*0xD0*/,
      (byte) 184,
      (byte) 71,
      (byte) 120,
      (byte) 59,
      (byte) 87,
      (byte) 193,
      (byte) 127 /*0x7F*/,
      (byte) 216,
      (byte) 244,
      (byte) 197,
      (byte) 14,
      (byte) 9,
      (byte) 71,
      (byte) 230,
      (byte) 113,
      (byte) 104,
      (byte) 243,
      (byte) 99,
      (byte) 234,
      (byte) 223,
      (byte) 221,
      (byte) 176 /*0xB0*/,
      (byte) 160 /*0xA0*/,
      (byte) 184,
      (byte) 249,
      (byte) 247,
      (byte) 87,
      (byte) 135,
      (byte) 172
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[17] = (byte) 208 /*0xD0*/;
    sourceArray2[1] = (byte) 89;
    sourceArray2[26] = (byte) 209;
    sourceArray2[3] = (byte) 214;
    sourceArray2[4] = (byte) 221;
    sourceArray2[5] = (byte) 42;
    sourceArray2[2] = (byte) 214;
    sourceArray2[30] = (byte) 40;
    sourceArray2[18] = (byte) 136;
    sourceArray2[27] = (byte) 93;
    sourceArray2[0] = (byte) 216;
    sourceArray2[11] = (byte) 188;
    sourceArray2[12] = (byte) 216;
    sourceArray2[28] = (byte) 191;
    sourceArray2[13] = (byte) 174;
    sourceArray2[15] = (byte) 57;
    sourceArray2[16 /*0x10*/] = (byte) 200;
    sourceArray2[43] = (byte) 182;
    sourceArray2[14] = (byte) 85;
    sourceArray2[19] = (byte) 208 /*0xD0*/;
    sourceArray2[20] = (byte) 111;
    sourceArray2[21] = (byte) 51;
    sourceArray2[22] = (byte) 116;
    sourceArray2[10] = (byte) 160 /*0xA0*/;
    sourceArray2[24] = (byte) 4;
    sourceArray2[36] = (byte) 128 /*0x80*/;
    sourceArray2[31 /*0x1F*/] = (byte) 31 /*0x1F*/;
    sourceArray2[7] = (byte) 217;
    sourceArray2[41] = (byte) 236;
    sourceArray2[39] = (byte) 71;
    sourceArray2[25] = (byte) 238;
    sourceArray2[9] = (byte) 8;
    sourceArray2[32 /*0x20*/] = (byte) 149;
    sourceArray2[33] = (byte) 159;
    sourceArray2[34] = (byte) 41;
    sourceArray2[35] = (byte) 182;
    sourceArray2[6] = (byte) 47;
    sourceArray2[23] = (byte) 2;
    sourceArray2[8] = (byte) 229;
    sourceArray2[47] = (byte) 178;
    sourceArray2[40] = (byte) 185;
    sourceArray2[42] = (byte) 226;
    sourceArray2[44] = (byte) 108;
    sourceArray2[29] = (byte) 27;
    sourceArray2[37] = (byte) 220;
    sourceArray2[45] = (byte) 38;
    sourceArray2[46] = (byte) 30;
    sourceArray2[38] = (byte) 244;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[10];
    byte[] response2 = new byte[10];
    Array.Copy((Array) sc_13302.sspq, 52, (Array) numArray2, 0, 10);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13302.sspr, 52, (Array) numArray2, 0, 10);
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

  internal static int ssp_appserver_13328(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[8] = (byte) 21;
    sourceArray1[34] = (byte) 170;
    sourceArray1[2] = (byte) 239;
    sourceArray1[3] = (byte) 17;
    sourceArray1[4] = (byte) 100;
    sourceArray1[24] = (byte) 48 /*0x30*/;
    sourceArray1[6] = (byte) 59;
    sourceArray1[37] = (byte) 143;
    sourceArray1[41] = (byte) 200;
    sourceArray1[13] = (byte) 93;
    sourceArray1[7] = (byte) 57;
    sourceArray1[5] = (byte) 200;
    sourceArray1[36] = (byte) 9;
    sourceArray1[21] = (byte) 216;
    sourceArray1[14] = (byte) 51;
    sourceArray1[31 /*0x1F*/] = (byte) 164;
    sourceArray1[16 /*0x10*/] = (byte) 82;
    sourceArray1[28] = (byte) 73;
    sourceArray1[18] = (byte) 163;
    sourceArray1[43] = (byte) 131;
    sourceArray1[17] = (byte) 84;
    sourceArray1[1] = (byte) 43;
    sourceArray1[22] = (byte) 67;
    sourceArray1[23] = (byte) 142;
    sourceArray1[46] = (byte) 242;
    sourceArray1[25] = (byte) 116;
    sourceArray1[29] = (byte) 94;
    sourceArray1[27] = (byte) 94;
    sourceArray1[11] = (byte) 192 /*0xC0*/;
    sourceArray1[44] = (byte) 1;
    sourceArray1[30] = (byte) 61;
    sourceArray1[10] = (byte) 103;
    sourceArray1[32 /*0x20*/] = (byte) 54;
    sourceArray1[33] = (byte) 86;
    sourceArray1[26] = (byte) 152;
    sourceArray1[35] = (byte) 41;
    sourceArray1[20] = (byte) 64 /*0x40*/;
    sourceArray1[40] = (byte) 207;
    sourceArray1[38] = (byte) 78;
    sourceArray1[39] = (byte) 59;
    sourceArray1[0] = (byte) 172;
    sourceArray1[15] = (byte) 222;
    sourceArray1[42] = (byte) 114;
    sourceArray1[9] = (byte) 163;
    sourceArray1[19] = (byte) 180;
    sourceArray1[12] = (byte) 50;
    sourceArray1[45] = (byte) 59;
    sourceArray1[47] = (byte) 222;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 200,
      (byte) 234,
      (byte) 39,
      (byte) 29,
      (byte) 38,
      (byte) 165,
      (byte) 155,
      (byte) 30,
      (byte) 57,
      (byte) 33,
      (byte) 88,
      (byte) 247,
      (byte) 77,
      (byte) 86,
      (byte) 95,
      (byte) 44,
      (byte) 97,
      (byte) 20,
      (byte) 171,
      (byte) 82,
      (byte) 242,
      (byte) 197,
      (byte) 203,
      (byte) 149,
      (byte) 141,
      (byte) 223,
      (byte) 37,
      (byte) 248,
      (byte) 204,
      (byte) 210,
      (byte) 0,
      (byte) 37,
      (byte) 67,
      (byte) 129,
      (byte) 205,
      (byte) 123,
      (byte) 75,
      (byte) 2,
      (byte) 201,
      (byte) 225,
      (byte) 88,
      (byte) 211,
      (byte) 162,
      (byte) 142,
      (byte) 133,
      (byte) 53,
      (byte) 60,
      (byte) 189
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13331(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 22,
      (byte) 43,
      (byte) 213,
      (byte) 192 /*0xC0*/,
      (byte) 229,
      (byte) 245,
      (byte) 7,
      (byte) 30,
      (byte) 245,
      (byte) 110,
      (byte) 39,
      (byte) 210,
      (byte) 236,
      (byte) 111,
      (byte) 118,
      (byte) 94,
      (byte) 60,
      (byte) 1,
      (byte) 37,
      (byte) 141,
      (byte) 88,
      (byte) 243,
      (byte) 221,
      (byte) 199,
      (byte) 44,
      (byte) 21,
      (byte) 168,
      (byte) 92,
      (byte) 243,
      (byte) 159,
      (byte) 170,
      (byte) 163,
      (byte) 78,
      (byte) 67,
      (byte) 106,
      (byte) 141,
      (byte) 169,
      (byte) 243,
      (byte) 208 /*0xD0*/,
      (byte) 74,
      (byte) 45,
      (byte) 18,
      (byte) 243,
      (byte) 103,
      (byte) 26,
      (byte) 214,
      (byte) 193,
      (byte) 238
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[39] = (byte) 89;
    sourceArray2[8] = (byte) 225;
    sourceArray2[2] = (byte) 239;
    sourceArray2[20] = (byte) 136;
    sourceArray2[45] = (byte) 219;
    sourceArray2[5] = (byte) 152;
    sourceArray2[31 /*0x1F*/] = (byte) 29;
    sourceArray2[35] = (byte) 89;
    sourceArray2[21] = (byte) 195;
    sourceArray2[28] = (byte) 88;
    sourceArray2[10] = (byte) 232;
    sourceArray2[47] = (byte) 196;
    sourceArray2[27] = (byte) 43;
    sourceArray2[13] = (byte) 150;
    sourceArray2[23] = (byte) 31 /*0x1F*/;
    sourceArray2[4] = (byte) 185;
    sourceArray2[16 /*0x10*/] = (byte) 30;
    sourceArray2[17] = (byte) 190;
    sourceArray2[18] = (byte) 3;
    sourceArray2[12] = (byte) 21;
    sourceArray2[26] = (byte) 141;
    sourceArray2[34] = (byte) 31 /*0x1F*/;
    sourceArray2[22] = (byte) 5;
    sourceArray2[1] = (byte) 16 /*0x10*/;
    sourceArray2[44] = (byte) 37;
    sourceArray2[25] = (byte) 246;
    sourceArray2[42] = (byte) 73;
    sourceArray2[24] = (byte) 131;
    sourceArray2[11] = (byte) 208 /*0xD0*/;
    sourceArray2[29] = (byte) 245;
    sourceArray2[30] = (byte) 162;
    sourceArray2[41] = (byte) 148;
    sourceArray2[32 /*0x20*/] = (byte) 182;
    sourceArray2[33] = (byte) 8;
    sourceArray2[36] = (byte) 17;
    sourceArray2[3] = (byte) 163;
    sourceArray2[9] = (byte) 165;
    sourceArray2[37] = (byte) 127 /*0x7F*/;
    sourceArray2[38] = (byte) 139;
    sourceArray2[15] = (byte) 40;
    sourceArray2[19] = (byte) 92;
    sourceArray2[6] = (byte) 169;
    sourceArray2[14] = (byte) 125;
    sourceArray2[43] = (byte) 164;
    sourceArray2[0] = (byte) 229;
    sourceArray2[7] = (byte) 8;
    sourceArray2[46] = (byte) 77;
    sourceArray2[40] = (byte) 160 /*0xA0*/;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[51];
    byte[] response2 = new byte[51];
    Array.Copy((Array) sc_13302.sspq, 62, (Array) numArray2, 0, 51);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13302.sspr, 62, (Array) numArray2, 0, 51);
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

  internal static int ssp_appserver_13334(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 210,
      (byte) 189,
      (byte) 105,
      (byte) 89,
      (byte) 44,
      (byte) 1,
      (byte) 194,
      (byte) 103,
      (byte) 89,
      (byte) 65,
      (byte) 117,
      (byte) 180,
      (byte) 195,
      (byte) 9,
      (byte) 74,
      (byte) 56,
      (byte) 65,
      (byte) 194,
      (byte) 196,
      (byte) 70,
      (byte) 158,
      (byte) 105,
      (byte) 75,
      (byte) 158,
      (byte) 230,
      (byte) 154,
      (byte) 67,
      (byte) 223,
      (byte) 200,
      (byte) 171,
      (byte) 114,
      (byte) 59,
      (byte) 65,
      (byte) 18,
      (byte) 253,
      (byte) 88,
      (byte) 155,
      (byte) 225,
      (byte) 164,
      (byte) 28,
      (byte) 193,
      (byte) 74,
      (byte) 14,
      (byte) 76,
      (byte) 212,
      (byte) 35,
      (byte) 94,
      (byte) 92
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 47,
      (byte) 172,
      (byte) 1,
      (byte) 227,
      (byte) 102,
      (byte) 249,
      (byte) 173,
      (byte) 86,
      (byte) 6,
      (byte) 134,
      (byte) 224 /*0xE0*/,
      (byte) 146,
      (byte) 30,
      (byte) 74,
      (byte) 198,
      (byte) 78,
      (byte) 39,
      (byte) 115,
      (byte) 243,
      (byte) 42,
      (byte) 174,
      (byte) 209,
      (byte) 237,
      (byte) 152,
      (byte) 208 /*0xD0*/,
      (byte) 101,
      byte.MaxValue,
      (byte) 252,
      (byte) 183,
      (byte) 40,
      (byte) 154,
      (byte) 45,
      (byte) 139,
      (byte) 138,
      (byte) 145,
      (byte) 43,
      (byte) 28,
      (byte) 112 /*0x70*/,
      (byte) 135,
      (byte) 136,
      (byte) 50,
      (byte) 109,
      (byte) 232,
      (byte) 113,
      (byte) 159,
      (byte) 38,
      (byte) 72,
      (byte) 225
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13336(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 213,
      (byte) 175,
      (byte) 105,
      (byte) 114,
      (byte) 203,
      (byte) 115,
      (byte) 145,
      (byte) 189,
      (byte) 227,
      (byte) 159,
      (byte) 173,
      (byte) 207,
      (byte) 58,
      (byte) 115,
      (byte) 177,
      (byte) 11,
      (byte) 242,
      (byte) 156,
      (byte) 95,
      (byte) 30,
      (byte) 167,
      (byte) 133,
      (byte) 83,
      (byte) 91,
      byte.MaxValue,
      (byte) 201,
      (byte) 195,
      (byte) 60,
      (byte) 230,
      (byte) 238,
      (byte) 223,
      (byte) 104,
      (byte) 202,
      (byte) 95,
      (byte) 107,
      (byte) 98,
      (byte) 148,
      (byte) 230,
      (byte) 208 /*0xD0*/,
      (byte) 109,
      (byte) 18,
      (byte) 95,
      (byte) 221,
      (byte) 171,
      (byte) 96 /*0x60*/,
      (byte) 124,
      (byte) 154,
      (byte) 175
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 128 /*0x80*/,
      (byte) 222,
      (byte) 125,
      (byte) 240 /*0xF0*/,
      (byte) 88,
      (byte) 161,
      (byte) 39,
      (byte) 248,
      (byte) 14,
      (byte) 192 /*0xC0*/,
      (byte) 127 /*0x7F*/,
      (byte) 70,
      (byte) 202,
      (byte) 54,
      (byte) 116,
      (byte) 184,
      (byte) 170,
      (byte) 98,
      (byte) 132,
      (byte) 81,
      (byte) 32 /*0x20*/,
      (byte) 140,
      (byte) 219,
      (byte) 250,
      (byte) 155,
      (byte) 50,
      (byte) 62,
      (byte) 206,
      (byte) 107,
      (byte) 30,
      (byte) 227,
      (byte) 15,
      (byte) 129,
      (byte) 60,
      (byte) 20,
      (byte) 34,
      (byte) 144 /*0x90*/,
      (byte) 134,
      (byte) 225,
      (byte) 165,
      (byte) 41,
      (byte) 53,
      (byte) 96 /*0x60*/,
      (byte) 223,
      (byte) 122,
      (byte) 102,
      (byte) 153,
      (byte) 191
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13337(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[2] = (byte) 80 /*0x50*/;
    sourceArray1[3] = (byte) 92;
    sourceArray1[45] = (byte) 235;
    sourceArray1[40] = (byte) 206;
    sourceArray1[18] = (byte) 12;
    sourceArray1[5] = (byte) 114;
    sourceArray1[42] = (byte) 190;
    sourceArray1[7] = (byte) 176 /*0xB0*/;
    sourceArray1[8] = (byte) 84;
    sourceArray1[16 /*0x10*/] = (byte) 79;
    sourceArray1[9] = (byte) 214;
    sourceArray1[33] = (byte) 133;
    sourceArray1[4] = (byte) 13;
    sourceArray1[13] = (byte) 4;
    sourceArray1[14] = (byte) 219;
    sourceArray1[11] = (byte) 191;
    sourceArray1[10] = (byte) 244;
    sourceArray1[17] = (byte) 119;
    sourceArray1[43] = (byte) 136;
    sourceArray1[19] = (byte) 204;
    sourceArray1[20] = (byte) 242;
    sourceArray1[21] = (byte) 11;
    sourceArray1[22] = (byte) 72;
    sourceArray1[34] = (byte) 64 /*0x40*/;
    sourceArray1[24] = (byte) 168;
    sourceArray1[29] = (byte) 143;
    sourceArray1[26] = (byte) 130;
    sourceArray1[28] = (byte) 19;
    sourceArray1[25] = (byte) 250;
    sourceArray1[44] = (byte) 233;
    sourceArray1[30] = (byte) 149;
    sourceArray1[31 /*0x1F*/] = (byte) 240 /*0xF0*/;
    sourceArray1[32 /*0x20*/] = (byte) 146;
    sourceArray1[23] = (byte) 167;
    sourceArray1[0] = (byte) 137;
    sourceArray1[35] = (byte) 41;
    sourceArray1[36] = (byte) 27;
    sourceArray1[37] = (byte) 5;
    sourceArray1[15] = (byte) 8;
    sourceArray1[39] = (byte) 116;
    sourceArray1[6] = (byte) 1;
    sourceArray1[41] = (byte) 189;
    sourceArray1[12] = (byte) 69;
    sourceArray1[1] = (byte) 53;
    sourceArray1[27] = (byte) 47;
    sourceArray1[38] = (byte) 134;
    sourceArray1[46] = (byte) 27;
    sourceArray1[47] = (byte) 120;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 38,
      (byte) 160 /*0xA0*/,
      (byte) 214,
      (byte) 172,
      (byte) 130,
      (byte) 53,
      (byte) 138,
      (byte) 188,
      (byte) 95,
      (byte) 84,
      (byte) 86,
      (byte) 223,
      (byte) 6,
      (byte) 230,
      (byte) 178,
      (byte) 62,
      (byte) 128 /*0x80*/,
      (byte) 146,
      (byte) 72,
      (byte) 10,
      (byte) 138,
      (byte) 98,
      (byte) 100,
      (byte) 82,
      (byte) 184,
      (byte) 217,
      (byte) 79,
      (byte) 178,
      (byte) 27,
      (byte) 198,
      (byte) 197,
      (byte) 59,
      (byte) 120,
      (byte) 43,
      (byte) 198,
      (byte) 63 /*0x3F*/,
      (byte) 37,
      (byte) 47,
      (byte) 12,
      (byte) 65,
      (byte) 239,
      (byte) 160 /*0xA0*/,
      (byte) 22,
      (byte) 37,
      (byte) 156,
      (byte) 48 /*0x30*/,
      (byte) 2,
      (byte) 77
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13338(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 91,
      (byte) 180,
      (byte) 13,
      (byte) 186,
      (byte) 19,
      (byte) 232,
      (byte) 90,
      (byte) 130,
      (byte) 187,
      (byte) 48 /*0x30*/,
      (byte) 45,
      (byte) 233,
      (byte) 247,
      (byte) 53,
      (byte) 228,
      (byte) 96 /*0x60*/,
      (byte) 49,
      (byte) 147,
      (byte) 132,
      (byte) 5,
      (byte) 26,
      (byte) 43,
      (byte) 0,
      (byte) 115,
      (byte) 121,
      (byte) 248,
      (byte) 126,
      (byte) 19,
      (byte) 0,
      (byte) 21,
      (byte) 85,
      (byte) 119,
      (byte) 217,
      (byte) 254,
      (byte) 131,
      (byte) 86,
      (byte) 99,
      (byte) 176 /*0xB0*/,
      (byte) 29,
      (byte) 211,
      (byte) 164,
      (byte) 144 /*0x90*/,
      (byte) 216,
      (byte) 146,
      (byte) 16 /*0x10*/,
      (byte) 137,
      (byte) 89,
      (byte) 61
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 100,
      (byte) 59,
      (byte) 131,
      (byte) 206,
      (byte) 208 /*0xD0*/,
      (byte) 118,
      (byte) 0,
      (byte) 179,
      (byte) 243,
      (byte) 200,
      (byte) 60,
      (byte) 196,
      (byte) 80 /*0x50*/,
      (byte) 30,
      (byte) 9,
      (byte) 119,
      (byte) 242,
      (byte) 102,
      (byte) 101,
      (byte) 126,
      (byte) 123,
      (byte) 63 /*0x3F*/,
      (byte) 250,
      (byte) 205,
      (byte) 169,
      (byte) 58,
      (byte) 167,
      (byte) 117,
      (byte) 188,
      (byte) 102,
      (byte) 190,
      (byte) 184,
      (byte) 116,
      (byte) 207,
      (byte) 79,
      (byte) 162,
      (byte) 232,
      (byte) 195,
      (byte) 22,
      (byte) 87,
      (byte) 64 /*0x40*/,
      (byte) 58,
      (byte) 95,
      (byte) 63 /*0x3F*/,
      (byte) 235,
      (byte) 61,
      (byte) 17,
      (byte) 109
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13339(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 224 /*0xE0*/,
      (byte) 164,
      (byte) 47,
      (byte) 179,
      (byte) 153,
      (byte) 64 /*0x40*/,
      (byte) 27,
      (byte) 106,
      (byte) 70,
      (byte) 129,
      (byte) 166,
      (byte) 219,
      (byte) 182,
      (byte) 16 /*0x10*/,
      (byte) 249,
      (byte) 177,
      (byte) 142,
      (byte) 253,
      (byte) 128 /*0x80*/,
      (byte) 118,
      (byte) 177,
      (byte) 96 /*0x60*/,
      (byte) 137,
      (byte) 83,
      (byte) 109,
      (byte) 99,
      (byte) 240 /*0xF0*/,
      (byte) 158,
      (byte) 64 /*0x40*/,
      (byte) 114,
      (byte) 98,
      (byte) 38,
      (byte) 240 /*0xF0*/,
      (byte) 179,
      (byte) 248,
      (byte) 90,
      (byte) 84,
      (byte) 162,
      (byte) 201,
      (byte) 111,
      (byte) 1,
      (byte) 104,
      (byte) 93,
      (byte) 14,
      (byte) 50,
      (byte) 183,
      (byte) 90,
      (byte) 120
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[38] = (byte) 21;
    sourceArray2[1] = byte.MaxValue;
    sourceArray2[2] = (byte) 30;
    sourceArray2[3] = (byte) 19;
    sourceArray2[27] = (byte) 78;
    sourceArray2[42] = (byte) 64 /*0x40*/;
    sourceArray2[6] = (byte) 172;
    sourceArray2[22] = (byte) 37;
    sourceArray2[45] = (byte) 154;
    sourceArray2[11] = (byte) 222;
    sourceArray2[10] = (byte) 16 /*0x10*/;
    sourceArray2[47] = (byte) 246;
    sourceArray2[12] = (byte) 149;
    sourceArray2[31 /*0x1F*/] = (byte) 245;
    sourceArray2[21] = (byte) 101;
    sourceArray2[15] = (byte) 32 /*0x20*/;
    sourceArray2[16 /*0x10*/] = (byte) 41;
    sourceArray2[28] = (byte) 96 /*0x60*/;
    sourceArray2[18] = (byte) 113;
    sourceArray2[34] = (byte) 14;
    sourceArray2[20] = (byte) 83;
    sourceArray2[8] = (byte) 15;
    sourceArray2[25] = (byte) 253;
    sourceArray2[23] = (byte) 140;
    sourceArray2[7] = (byte) 79;
    sourceArray2[33] = (byte) 180;
    sourceArray2[26] = (byte) 90;
    sourceArray2[14] = (byte) 146;
    sourceArray2[4] = (byte) 134;
    sourceArray2[24] = (byte) 50;
    sourceArray2[30] = (byte) 165;
    sourceArray2[17] = (byte) 172;
    sourceArray2[32 /*0x20*/] = (byte) 140;
    sourceArray2[13] = (byte) 15;
    sourceArray2[43] = (byte) 132;
    sourceArray2[35] = (byte) 119;
    sourceArray2[37] = (byte) 35;
    sourceArray2[9] = (byte) 188;
    sourceArray2[19] = (byte) 157;
    sourceArray2[39] = (byte) 242;
    sourceArray2[40] = (byte) 186;
    sourceArray2[41] = (byte) 87;
    sourceArray2[29] = (byte) 240 /*0xF0*/;
    sourceArray2[36] = (byte) 79;
    sourceArray2[44] = (byte) 41;
    sourceArray2[5] = (byte) 40;
    sourceArray2[46] = (byte) 11;
    sourceArray2[0] = (byte) 152;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13340(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[2] = (byte) 192 /*0xC0*/;
    sourceArray1[1] = (byte) 96 /*0x60*/;
    sourceArray1[7] = (byte) 121;
    sourceArray1[10] = (byte) 159;
    sourceArray1[4] = (byte) 38;
    sourceArray1[5] = (byte) 108;
    sourceArray1[6] = (byte) 92;
    sourceArray1[18] = (byte) 92;
    sourceArray1[8] = (byte) 200;
    sourceArray1[9] = (byte) 124;
    sourceArray1[36] = (byte) 125;
    sourceArray1[3] = (byte) 46;
    sourceArray1[14] = (byte) 216;
    sourceArray1[13] = (byte) 44;
    sourceArray1[42] = (byte) 216;
    sourceArray1[35] = (byte) 23;
    sourceArray1[0] = (byte) 87;
    sourceArray1[17] = (byte) 115;
    sourceArray1[31 /*0x1F*/] = (byte) 139;
    sourceArray1[43] = (byte) 252;
    sourceArray1[45] = (byte) 188;
    sourceArray1[21] = (byte) 96 /*0x60*/;
    sourceArray1[38] = (byte) 58;
    sourceArray1[22] = (byte) 158;
    sourceArray1[32 /*0x20*/] = (byte) 109;
    sourceArray1[25] = (byte) 142;
    sourceArray1[23] = (byte) 95;
    sourceArray1[41] = (byte) 136;
    sourceArray1[28] = (byte) 239;
    sourceArray1[29] = (byte) 10;
    sourceArray1[40] = (byte) 92;
    sourceArray1[30] = (byte) 121;
    sourceArray1[12] = (byte) 143;
    sourceArray1[33] = (byte) 232;
    sourceArray1[34] = (byte) 178;
    sourceArray1[46] = (byte) 205;
    sourceArray1[20] = (byte) 231;
    sourceArray1[37] = (byte) 135;
    sourceArray1[24] = (byte) 229;
    sourceArray1[39] = (byte) 86;
    sourceArray1[26] = (byte) 160 /*0xA0*/;
    sourceArray1[16 /*0x10*/] = (byte) 116;
    sourceArray1[44] = (byte) 160 /*0xA0*/;
    sourceArray1[11] = (byte) 187;
    sourceArray1[15] = (byte) 63 /*0x3F*/;
    sourceArray1[27] = (byte) 241;
    sourceArray1[19] = (byte) 197;
    sourceArray1[47] = (byte) 102;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 209,
      (byte) 155,
      (byte) 202,
      (byte) 126,
      (byte) 122,
      (byte) 126,
      (byte) 130,
      (byte) 163,
      (byte) 3,
      (byte) 138,
      (byte) 205,
      (byte) 81,
      (byte) 172,
      (byte) 165,
      (byte) 154,
      (byte) 113,
      (byte) 205,
      (byte) 85,
      (byte) 246,
      (byte) 31 /*0x1F*/,
      (byte) 9,
      (byte) 79,
      (byte) 180,
      (byte) 170,
      (byte) 178,
      (byte) 180,
      (byte) 158,
      (byte) 62,
      (byte) 162,
      (byte) 123,
      (byte) 215,
      (byte) 231,
      (byte) 178,
      (byte) 226,
      (byte) 40,
      (byte) 86,
      (byte) 17,
      (byte) 200,
      (byte) 72,
      (byte) 31 /*0x1F*/,
      (byte) 40,
      (byte) 15,
      (byte) 183,
      (byte) 108,
      (byte) 219,
      (byte) 22,
      (byte) 30,
      (byte) 146
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13342(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 38,
      (byte) 236,
      (byte) 134,
      (byte) 131,
      (byte) 221,
      (byte) 221,
      (byte) 141,
      (byte) 132,
      (byte) 135,
      (byte) 111,
      (byte) 32 /*0x20*/,
      (byte) 161,
      (byte) 30,
      (byte) 216,
      (byte) 146,
      (byte) 193,
      (byte) 223,
      (byte) 126,
      (byte) 161,
      (byte) 28,
      (byte) 206,
      (byte) 164,
      (byte) 190,
      (byte) 108,
      (byte) 165,
      (byte) 174,
      (byte) 151,
      (byte) 123,
      (byte) 119,
      (byte) 95,
      (byte) 109,
      (byte) 168,
      (byte) 52,
      (byte) 188,
      (byte) 153,
      (byte) 76,
      (byte) 34,
      (byte) 39,
      (byte) 98,
      (byte) 132,
      (byte) 106,
      (byte) 251,
      (byte) 122,
      (byte) 76,
      (byte) 52,
      (byte) 122,
      (byte) 32 /*0x20*/,
      (byte) 201
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[4] = (byte) 208 /*0xD0*/;
    sourceArray2[15] = (byte) 66;
    sourceArray2[41] = (byte) 249;
    sourceArray2[3] = (byte) 171;
    sourceArray2[32 /*0x20*/] = (byte) 1;
    sourceArray2[2] = (byte) 105;
    sourceArray2[6] = (byte) 211;
    sourceArray2[39] = (byte) 151;
    sourceArray2[8] = (byte) 208 /*0xD0*/;
    sourceArray2[9] = (byte) 128 /*0x80*/;
    sourceArray2[10] = (byte) 180;
    sourceArray2[24] = (byte) 247;
    sourceArray2[36] = (byte) 210;
    sourceArray2[13] = (byte) 117;
    sourceArray2[0] = (byte) 77;
    sourceArray2[35] = (byte) 201;
    sourceArray2[16 /*0x10*/] = (byte) 104;
    sourceArray2[17] = (byte) 189;
    sourceArray2[18] = (byte) 142;
    sourceArray2[14] = (byte) 236;
    sourceArray2[20] = (byte) 165;
    sourceArray2[45] = (byte) 6;
    sourceArray2[22] = (byte) 135;
    sourceArray2[46] = (byte) 158;
    sourceArray2[12] = (byte) 33;
    sourceArray2[33] = (byte) 100;
    sourceArray2[25] = (byte) 102;
    sourceArray2[23] = (byte) 223;
    sourceArray2[28] = (byte) 32 /*0x20*/;
    sourceArray2[29] = (byte) 2;
    sourceArray2[30] = (byte) 242;
    sourceArray2[1] = (byte) 95;
    sourceArray2[5] = (byte) 190;
    sourceArray2[31 /*0x1F*/] = (byte) 195;
    sourceArray2[7] = (byte) 236;
    sourceArray2[34] = (byte) 50;
    sourceArray2[26] = (byte) 228;
    sourceArray2[37] = (byte) 235;
    sourceArray2[38] = (byte) 36;
    sourceArray2[21] = (byte) 114;
    sourceArray2[47] = (byte) 149;
    sourceArray2[27] = (byte) 242;
    sourceArray2[42] = (byte) 109;
    sourceArray2[43] = (byte) 93;
    sourceArray2[44] = (byte) 8;
    sourceArray2[40] = (byte) 141;
    sourceArray2[19] = (byte) 16 /*0x10*/;
    sourceArray2[11] = (byte) 244;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13343(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 189,
      (byte) 137,
      (byte) 178,
      (byte) 144 /*0x90*/,
      (byte) 104,
      (byte) 128 /*0x80*/,
      (byte) 59,
      (byte) 189,
      (byte) 203,
      (byte) 88,
      (byte) 187,
      (byte) 226,
      (byte) 178,
      (byte) 159,
      (byte) 119,
      (byte) 141,
      (byte) 197,
      (byte) 232,
      (byte) 48 /*0x30*/,
      (byte) 39,
      (byte) 167,
      (byte) 68,
      (byte) 40,
      (byte) 38,
      (byte) 150,
      (byte) 109,
      (byte) 231,
      (byte) 221,
      (byte) 88,
      (byte) 229,
      (byte) 215,
      (byte) 40,
      (byte) 124,
      (byte) 29,
      (byte) 208 /*0xD0*/,
      (byte) 68,
      (byte) 208 /*0xD0*/,
      (byte) 182,
      (byte) 215,
      (byte) 81,
      (byte) 21,
      (byte) 8,
      (byte) 81,
      (byte) 190,
      (byte) 100,
      (byte) 38,
      (byte) 151,
      (byte) 149
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[24] = (byte) 107;
    sourceArray2[21] = (byte) 160 /*0xA0*/;
    sourceArray2[44] = (byte) 98;
    sourceArray2[42] = (byte) 94;
    sourceArray2[4] = (byte) 61;
    sourceArray2[30] = (byte) 88;
    sourceArray2[6] = (byte) 229;
    sourceArray2[45] = (byte) 210;
    sourceArray2[43] = (byte) 123;
    sourceArray2[23] = (byte) 32 /*0x20*/;
    sourceArray2[10] = (byte) 87;
    sourceArray2[11] = (byte) 118;
    sourceArray2[12] = (byte) 169;
    sourceArray2[1] = (byte) 26;
    sourceArray2[14] = (byte) 68;
    sourceArray2[22] = (byte) 7;
    sourceArray2[47] = (byte) 149;
    sourceArray2[40] = (byte) 42;
    sourceArray2[26] = (byte) 62;
    sourceArray2[19] = (byte) 192 /*0xC0*/;
    sourceArray2[16 /*0x10*/] = (byte) 186;
    sourceArray2[18] = (byte) 84;
    sourceArray2[0] = (byte) 179;
    sourceArray2[36] = (byte) 81;
    sourceArray2[15] = (byte) 241;
    sourceArray2[25] = (byte) 198;
    sourceArray2[35] = (byte) 40;
    sourceArray2[27] = (byte) 37;
    sourceArray2[2] = (byte) 202;
    sourceArray2[8] = (byte) 254;
    sourceArray2[41] = (byte) 113;
    sourceArray2[31 /*0x1F*/] = (byte) 176 /*0xB0*/;
    sourceArray2[32 /*0x20*/] = (byte) 135;
    sourceArray2[5] = (byte) 61;
    sourceArray2[3] = (byte) 84;
    sourceArray2[7] = (byte) 47;
    sourceArray2[34] = (byte) 31 /*0x1F*/;
    sourceArray2[37] = (byte) 167;
    sourceArray2[38] = (byte) 202;
    sourceArray2[13] = (byte) 12;
    sourceArray2[9] = (byte) 189;
    sourceArray2[17] = (byte) 77;
    sourceArray2[29] = (byte) 68;
    sourceArray2[28] = (byte) 19;
    sourceArray2[20] = (byte) 211;
    sourceArray2[39] = (byte) 220;
    sourceArray2[46] = (byte) 96 /*0x60*/;
    sourceArray2[33] = (byte) 192 /*0xC0*/;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[16 /*0x10*/];
    byte[] response2 = new byte[16 /*0x10*/];
    Array.Copy((Array) sc_13302.sspq, 113, (Array) numArray2, 0, 16 /*0x10*/);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13302.sspr, 113, (Array) numArray2, 0, 16 /*0x10*/);
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

  internal static int ssp_appserver_13344(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[35] = (byte) 43;
    sourceArray1[25] = (byte) 241;
    sourceArray1[2] = (byte) 133;
    sourceArray1[38] = (byte) 35;
    sourceArray1[24] = (byte) 101;
    sourceArray1[5] = (byte) 118;
    sourceArray1[6] = (byte) 79;
    sourceArray1[12] = (byte) 252;
    sourceArray1[9] = (byte) 52;
    sourceArray1[1] = (byte) 18;
    sourceArray1[13] = (byte) 135;
    sourceArray1[11] = (byte) 91;
    sourceArray1[36] = (byte) 76;
    sourceArray1[44] = (byte) 21;
    sourceArray1[28] = (byte) 121;
    sourceArray1[15] = (byte) 240 /*0xF0*/;
    sourceArray1[16 /*0x10*/] = (byte) 175;
    sourceArray1[14] = (byte) 44;
    sourceArray1[18] = (byte) 139;
    sourceArray1[20] = (byte) 125;
    sourceArray1[19] = (byte) 129;
    sourceArray1[21] = (byte) 164;
    sourceArray1[22] = (byte) 21;
    sourceArray1[46] = (byte) 201;
    sourceArray1[10] = (byte) 38;
    sourceArray1[8] = (byte) 82;
    sourceArray1[26] = (byte) 210;
    sourceArray1[39] = (byte) 79;
    sourceArray1[31 /*0x1F*/] = (byte) 113;
    sourceArray1[29] = (byte) 49;
    sourceArray1[30] = (byte) 183;
    sourceArray1[3] = (byte) 126;
    sourceArray1[32 /*0x20*/] = (byte) 112 /*0x70*/;
    sourceArray1[33] = (byte) 202;
    sourceArray1[34] = (byte) 66;
    sourceArray1[43] = (byte) 36;
    sourceArray1[17] = (byte) 212;
    sourceArray1[37] = (byte) 65;
    sourceArray1[47] = (byte) 253;
    sourceArray1[0] = (byte) 40;
    sourceArray1[40] = (byte) 69;
    sourceArray1[27] = (byte) 235;
    sourceArray1[42] = (byte) 198;
    sourceArray1[41] = (byte) 149;
    sourceArray1[23] = (byte) 210;
    sourceArray1[45] = (byte) 39;
    sourceArray1[4] = (byte) 222;
    sourceArray1[7] = (byte) 121;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 10,
      (byte) 95,
      (byte) 181,
      (byte) 85,
      (byte) 128 /*0x80*/,
      (byte) 22,
      (byte) 123,
      (byte) 124,
      (byte) 249,
      (byte) 74,
      (byte) 120,
      (byte) 145,
      (byte) 242,
      (byte) 252,
      (byte) 219,
      (byte) 222,
      (byte) 49,
      (byte) 165,
      (byte) 122,
      (byte) 117,
      (byte) 209,
      (byte) 249,
      (byte) 102,
      (byte) 236,
      (byte) 103,
      (byte) 99,
      (byte) 91,
      (byte) 61,
      (byte) 39,
      (byte) 26,
      (byte) 11,
      (byte) 131,
      (byte) 184,
      (byte) 8,
      (byte) 249,
      (byte) 95,
      (byte) 71,
      (byte) 167,
      (byte) 211,
      (byte) 184,
      (byte) 8,
      (byte) 22,
      (byte) 9,
      (byte) 156,
      (byte) 254,
      (byte) 231,
      (byte) 100,
      (byte) 108
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13345(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 151,
      (byte) 184,
      (byte) 203,
      (byte) 52,
      (byte) 110,
      (byte) 159,
      (byte) 127 /*0x7F*/,
      (byte) 137,
      (byte) 114,
      (byte) 166,
      (byte) 12,
      (byte) 245,
      (byte) 219,
      (byte) 196,
      (byte) 207,
      (byte) 68,
      (byte) 97,
      (byte) 202,
      (byte) 21,
      byte.MaxValue,
      (byte) 63 /*0x3F*/,
      (byte) 193,
      (byte) 134,
      (byte) 210,
      (byte) 11,
      (byte) 69,
      (byte) 122,
      (byte) 145,
      (byte) 90,
      (byte) 83,
      (byte) 56,
      (byte) 73,
      (byte) 92,
      (byte) 1,
      (byte) 144 /*0x90*/,
      (byte) 27,
      (byte) 84,
      (byte) 106,
      (byte) 232,
      (byte) 199,
      (byte) 54,
      (byte) 33,
      (byte) 253,
      (byte) 24,
      (byte) 33,
      (byte) 163,
      (byte) 76,
      (byte) 113
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 216,
      (byte) 174,
      (byte) 48 /*0x30*/,
      (byte) 158,
      (byte) 52,
      (byte) 111,
      (byte) 61,
      (byte) 237,
      (byte) 17,
      (byte) 106,
      (byte) 57,
      (byte) 27,
      (byte) 46,
      (byte) 241,
      (byte) 239,
      (byte) 224 /*0xE0*/,
      (byte) 134,
      (byte) 182,
      (byte) 85,
      (byte) 154,
      (byte) 169,
      (byte) 112 /*0x70*/,
      (byte) 162,
      (byte) 14,
      (byte) 228,
      (byte) 143,
      (byte) 218,
      (byte) 98,
      (byte) 114,
      (byte) 208 /*0xD0*/,
      (byte) 66,
      (byte) 226,
      (byte) 71,
      (byte) 108,
      (byte) 81,
      (byte) 183,
      (byte) 217,
      (byte) 212,
      (byte) 172,
      (byte) 228,
      (byte) 235,
      (byte) 103,
      (byte) 104,
      (byte) 213,
      (byte) 247,
      (byte) 108,
      (byte) 40,
      (byte) 3
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13346(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 97,
      (byte) 252,
      (byte) 214,
      (byte) 50,
      (byte) 248,
      (byte) 41,
      (byte) 40,
      (byte) 37,
      (byte) 237,
      (byte) 46,
      (byte) 30,
      (byte) 109,
      (byte) 194,
      (byte) 152,
      (byte) 218,
      (byte) 52,
      (byte) 180,
      (byte) 239,
      (byte) 246,
      (byte) 226,
      (byte) 78,
      (byte) 200,
      (byte) 29,
      (byte) 195,
      (byte) 110,
      (byte) 177,
      (byte) 104,
      (byte) 248,
      (byte) 8,
      (byte) 181,
      (byte) 189,
      (byte) 32 /*0x20*/,
      (byte) 93,
      (byte) 184,
      (byte) 251,
      (byte) 4,
      (byte) 185,
      (byte) 233,
      (byte) 92,
      (byte) 65,
      (byte) 191,
      (byte) 162,
      (byte) 72,
      (byte) 243,
      (byte) 223,
      (byte) 170,
      (byte) 95,
      (byte) 10
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[40] = (byte) 170;
    sourceArray2[39] = (byte) 240 /*0xF0*/;
    sourceArray2[12] = (byte) 18;
    sourceArray2[1] = (byte) 157;
    sourceArray2[43] = (byte) 89;
    sourceArray2[21] = (byte) 191;
    sourceArray2[6] = (byte) 165;
    sourceArray2[35] = (byte) 182;
    sourceArray2[36] = (byte) 222;
    sourceArray2[5] = (byte) 124;
    sourceArray2[10] = (byte) 128 /*0x80*/;
    sourceArray2[18] = (byte) 186;
    sourceArray2[2] = (byte) 80 /*0x50*/;
    sourceArray2[13] = (byte) 147;
    sourceArray2[14] = (byte) 62;
    sourceArray2[15] = (byte) 144 /*0x90*/;
    sourceArray2[16 /*0x10*/] = (byte) 76;
    sourceArray2[3] = (byte) 147;
    sourceArray2[9] = (byte) 52;
    sourceArray2[29] = (byte) 180;
    sourceArray2[20] = (byte) 50;
    sourceArray2[0] = (byte) 72;
    sourceArray2[22] = (byte) 166;
    sourceArray2[42] = (byte) 172;
    sourceArray2[24] = (byte) 124;
    sourceArray2[7] = (byte) 75;
    sourceArray2[25] = (byte) 176 /*0xB0*/;
    sourceArray2[44] = (byte) 156;
    sourceArray2[28] = (byte) 209;
    sourceArray2[27] = (byte) 93;
    sourceArray2[30] = (byte) 53;
    sourceArray2[41] = (byte) 235;
    sourceArray2[32 /*0x20*/] = (byte) 139;
    sourceArray2[33] = (byte) 40;
    sourceArray2[34] = (byte) 105;
    sourceArray2[23] = (byte) 161;
    sourceArray2[26] = (byte) 251;
    sourceArray2[4] = (byte) 98;
    sourceArray2[38] = (byte) 237;
    sourceArray2[8] = (byte) 14;
    sourceArray2[19] = (byte) 168;
    sourceArray2[47] = (byte) 231;
    sourceArray2[17] = (byte) 93;
    sourceArray2[46] = (byte) 207;
    sourceArray2[11] = (byte) 1;
    sourceArray2[45] = (byte) 146;
    sourceArray2[37] = (byte) 18;
    sourceArray2[31 /*0x1F*/] = (byte) 131;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13347(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 85,
      (byte) 109,
      (byte) 157,
      (byte) 193,
      (byte) 92,
      (byte) 25,
      (byte) 134,
      (byte) 212,
      (byte) 150,
      (byte) 25,
      (byte) 180,
      (byte) 46,
      byte.MaxValue,
      (byte) 245,
      (byte) 99,
      (byte) 170,
      (byte) 80 /*0x50*/,
      (byte) 169,
      (byte) 96 /*0x60*/,
      (byte) 26,
      (byte) 171,
      (byte) 148,
      (byte) 58,
      (byte) 176 /*0xB0*/,
      (byte) 212,
      (byte) 164,
      (byte) 126,
      (byte) 53,
      (byte) 148,
      (byte) 37,
      (byte) 110,
      (byte) 167,
      (byte) 2,
      (byte) 42,
      (byte) 252,
      (byte) 240 /*0xF0*/,
      (byte) 232,
      (byte) 10,
      (byte) 49,
      (byte) 247,
      (byte) 92,
      (byte) 144 /*0x90*/,
      (byte) 222,
      (byte) 73,
      (byte) 112 /*0x70*/,
      (byte) 120,
      (byte) 37,
      (byte) 32 /*0x20*/
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 101,
      (byte) 169,
      (byte) 49,
      (byte) 46,
      (byte) 137,
      (byte) 225,
      (byte) 94,
      (byte) 239,
      (byte) 202,
      (byte) 144 /*0x90*/,
      (byte) 31 /*0x1F*/,
      (byte) 148,
      (byte) 142,
      (byte) 193,
      (byte) 254,
      (byte) 227,
      (byte) 165,
      (byte) 226,
      (byte) 210,
      (byte) 133,
      (byte) 200,
      (byte) 23,
      byte.MaxValue,
      (byte) 106,
      (byte) 49,
      (byte) 54,
      (byte) 192 /*0xC0*/,
      (byte) 22,
      (byte) 80 /*0x50*/,
      (byte) 110,
      (byte) 28,
      (byte) 163,
      (byte) 74,
      (byte) 231,
      (byte) 205,
      (byte) 99,
      (byte) 74,
      (byte) 148,
      (byte) 185,
      (byte) 133,
      (byte) 216,
      (byte) 55,
      (byte) 242,
      (byte) 213,
      (byte) 57,
      (byte) 231,
      (byte) 50,
      (byte) 99
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[16 /*0x10*/];
    byte[] response2 = new byte[16 /*0x10*/];
    Array.Copy((Array) sc_13302.sspq, 129, (Array) numArray2, 0, 16 /*0x10*/);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13302.sspr, 129, (Array) numArray2, 0, 16 /*0x10*/);
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

  internal static int ssp_appserver_13349(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[43] = (byte) 145;
    sourceArray1[37] = (byte) 185;
    sourceArray1[45] = (byte) 61;
    sourceArray1[3] = (byte) 105;
    sourceArray1[34] = (byte) 34;
    sourceArray1[5] = (byte) 26;
    sourceArray1[6] = (byte) 182;
    sourceArray1[32 /*0x20*/] = (byte) 39;
    sourceArray1[23] = (byte) 122;
    sourceArray1[9] = (byte) 92;
    sourceArray1[10] = (byte) 127 /*0x7F*/;
    sourceArray1[0] = (byte) 193;
    sourceArray1[12] = (byte) 25;
    sourceArray1[18] = (byte) 59;
    sourceArray1[14] = (byte) 240 /*0xF0*/;
    sourceArray1[15] = (byte) 7;
    sourceArray1[22] = (byte) 28;
    sourceArray1[19] = (byte) 60;
    sourceArray1[25] = (byte) 11;
    sourceArray1[7] = (byte) 241;
    sourceArray1[2] = (byte) 119;
    sourceArray1[16 /*0x10*/] = (byte) 133;
    sourceArray1[46] = (byte) 55;
    sourceArray1[17] = (byte) 59;
    sourceArray1[24] = (byte) 91;
    sourceArray1[21] = (byte) 251;
    sourceArray1[26] = (byte) 80 /*0x50*/;
    sourceArray1[33] = (byte) 169;
    sourceArray1[28] = (byte) 158;
    sourceArray1[27] = (byte) 109;
    sourceArray1[30] = (byte) 75;
    sourceArray1[20] = (byte) 189;
    sourceArray1[40] = (byte) 10;
    sourceArray1[8] = (byte) 63 /*0x3F*/;
    sourceArray1[13] = (byte) 176 /*0xB0*/;
    sourceArray1[35] = (byte) 239;
    sourceArray1[4] = (byte) 189;
    sourceArray1[1] = (byte) 70;
    sourceArray1[38] = (byte) 9;
    sourceArray1[39] = (byte) 211;
    sourceArray1[47] = (byte) 143;
    sourceArray1[41] = (byte) 65;
    sourceArray1[42] = (byte) 173;
    sourceArray1[31 /*0x1F*/] = (byte) 80 /*0x50*/;
    sourceArray1[44] = (byte) 56;
    sourceArray1[36] = (byte) 239;
    sourceArray1[11] = (byte) 93;
    sourceArray1[29] = (byte) 100;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[41] = (byte) 41;
    sourceArray2[26] = (byte) 218;
    sourceArray2[24] = (byte) 175;
    sourceArray2[3] = (byte) 210;
    sourceArray2[8] = (byte) 6;
    sourceArray2[5] = (byte) 35;
    sourceArray2[2] = (byte) 206;
    sourceArray2[33] = (byte) 65;
    sourceArray2[46] = (byte) 169;
    sourceArray2[9] = (byte) 131;
    sourceArray2[11] = (byte) 112 /*0x70*/;
    sourceArray2[0] = (byte) 202;
    sourceArray2[12] = (byte) 8;
    sourceArray2[37] = (byte) 82;
    sourceArray2[36] = (byte) 164;
    sourceArray2[29] = (byte) 210;
    sourceArray2[15] = (byte) 196;
    sourceArray2[6] = (byte) 105;
    sourceArray2[45] = (byte) 67;
    sourceArray2[19] = (byte) 55;
    sourceArray2[20] = (byte) 70;
    sourceArray2[21] = (byte) 220;
    sourceArray2[28] = (byte) 52;
    sourceArray2[23] = (byte) 17;
    sourceArray2[1] = (byte) 232;
    sourceArray2[7] = (byte) 75;
    sourceArray2[14] = (byte) 229;
    sourceArray2[4] = (byte) 215;
    sourceArray2[13] = (byte) 76;
    sourceArray2[43] = (byte) 194;
    sourceArray2[30] = (byte) 124;
    sourceArray2[22] = (byte) 167;
    sourceArray2[10] = (byte) 166;
    sourceArray2[38] = (byte) 153;
    sourceArray2[32 /*0x20*/] = (byte) 13;
    sourceArray2[44] = (byte) 137;
    sourceArray2[18] = (byte) 190;
    sourceArray2[25] = byte.MaxValue;
    sourceArray2[35] = (byte) 230;
    sourceArray2[39] = (byte) 141;
    sourceArray2[40] = (byte) 78;
    sourceArray2[31 /*0x1F*/] = (byte) 50;
    sourceArray2[42] = (byte) 57;
    sourceArray2[34] = (byte) 251;
    sourceArray2[17] = (byte) 127 /*0x7F*/;
    sourceArray2[47] = (byte) 250;
    sourceArray2[16 /*0x10*/] = (byte) 68;
    sourceArray2[27] = (byte) 48 /*0x30*/;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13350(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 1,
      (byte) 101,
      (byte) 150,
      (byte) 212,
      (byte) 203,
      (byte) 143,
      (byte) 61,
      (byte) 74,
      (byte) 209,
      (byte) 70,
      (byte) 101,
      (byte) 154,
      (byte) 227,
      (byte) 212,
      (byte) 25,
      (byte) 173,
      (byte) 252,
      (byte) 147,
      (byte) 131,
      (byte) 216,
      (byte) 209,
      (byte) 73,
      (byte) 239,
      (byte) 233,
      (byte) 219,
      (byte) 25,
      (byte) 245,
      (byte) 232,
      (byte) 195,
      (byte) 74,
      (byte) 17,
      (byte) 240 /*0xF0*/,
      (byte) 99,
      (byte) 99,
      (byte) 92,
      (byte) 11,
      (byte) 246,
      (byte) 166,
      (byte) 63 /*0x3F*/,
      (byte) 168,
      (byte) 217,
      (byte) 194,
      (byte) 237,
      (byte) 37,
      (byte) 9,
      (byte) 136,
      (byte) 184,
      (byte) 175
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[36] = (byte) 56;
    sourceArray2[33] = (byte) 229;
    sourceArray2[1] = (byte) 199;
    sourceArray2[16 /*0x10*/] = (byte) 67;
    sourceArray2[37] = (byte) 252;
    sourceArray2[39] = (byte) 207;
    sourceArray2[28] = (byte) 32 /*0x20*/;
    sourceArray2[17] = (byte) 27;
    sourceArray2[8] = (byte) 75;
    sourceArray2[2] = (byte) 34;
    sourceArray2[42] = (byte) 57;
    sourceArray2[9] = (byte) 91;
    sourceArray2[23] = (byte) 89;
    sourceArray2[11] = (byte) 131;
    sourceArray2[14] = (byte) 138;
    sourceArray2[25] = (byte) 233;
    sourceArray2[19] = (byte) 45;
    sourceArray2[29] = (byte) 221;
    sourceArray2[18] = (byte) 221;
    sourceArray2[31 /*0x1F*/] = (byte) 157;
    sourceArray2[20] = (byte) 172;
    sourceArray2[21] = (byte) 194;
    sourceArray2[7] = (byte) 221;
    sourceArray2[22] = (byte) 229;
    sourceArray2[24] = (byte) 187;
    sourceArray2[15] = (byte) 84;
    sourceArray2[45] = (byte) 18;
    sourceArray2[27] = (byte) 173;
    sourceArray2[6] = (byte) 59;
    sourceArray2[12] = (byte) 205;
    sourceArray2[30] = (byte) 99;
    sourceArray2[10] = (byte) 114;
    sourceArray2[32 /*0x20*/] = (byte) 220;
    sourceArray2[46] = (byte) 251;
    sourceArray2[34] = (byte) 83;
    sourceArray2[35] = (byte) 46;
    sourceArray2[38] = (byte) 91;
    sourceArray2[40] = (byte) 59;
    sourceArray2[0] = (byte) 61;
    sourceArray2[4] = (byte) 222;
    sourceArray2[13] = (byte) 188;
    sourceArray2[41] = (byte) 103;
    sourceArray2[5] = (byte) 168;
    sourceArray2[43] = (byte) 69;
    sourceArray2[44] = (byte) 139;
    sourceArray2[47] = (byte) 5;
    sourceArray2[26] = (byte) 48 /*0x30*/;
    sourceArray2[3] = (byte) 63 /*0x3F*/;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13352(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 56,
      (byte) 54,
      (byte) 186,
      (byte) 84,
      (byte) 147,
      (byte) 64 /*0x40*/,
      (byte) 216,
      (byte) 229,
      (byte) 151,
      (byte) 91,
      (byte) 171,
      (byte) 203,
      (byte) 165,
      (byte) 80 /*0x50*/,
      (byte) 83,
      (byte) 52,
      (byte) 206,
      (byte) 38,
      (byte) 183,
      (byte) 55,
      (byte) 182,
      (byte) 195,
      (byte) 211,
      (byte) 119,
      (byte) 109,
      (byte) 168,
      (byte) 198,
      (byte) 203,
      (byte) 121,
      (byte) 210,
      (byte) 245,
      (byte) 216,
      (byte) 76,
      (byte) 1,
      (byte) 40,
      (byte) 15,
      (byte) 101,
      (byte) 98,
      (byte) 205,
      (byte) 60,
      (byte) 101,
      (byte) 154,
      (byte) 37,
      (byte) 73,
      (byte) 234,
      (byte) 126,
      (byte) 7,
      (byte) 61
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[40] = (byte) 120;
    sourceArray2[1] = (byte) 215;
    sourceArray2[2] = (byte) 218;
    sourceArray2[3] = (byte) 74;
    sourceArray2[18] = (byte) 125;
    sourceArray2[44] = (byte) 95;
    sourceArray2[10] = (byte) 225;
    sourceArray2[25] = (byte) 4;
    sourceArray2[14] = (byte) 48 /*0x30*/;
    sourceArray2[9] = (byte) 132;
    sourceArray2[34] = (byte) 90;
    sourceArray2[19] = (byte) 218;
    sourceArray2[22] = (byte) 27;
    sourceArray2[13] = (byte) 177;
    sourceArray2[21] = (byte) 53;
    sourceArray2[11] = (byte) 108;
    sourceArray2[16 /*0x10*/] = (byte) 98;
    sourceArray2[17] = (byte) 76;
    sourceArray2[7] = (byte) 105;
    sourceArray2[43] = (byte) 37;
    sourceArray2[20] = (byte) 103;
    sourceArray2[38] = (byte) 232;
    sourceArray2[12] = (byte) 65;
    sourceArray2[36] = (byte) 19;
    sourceArray2[24] = (byte) 53;
    sourceArray2[32 /*0x20*/] = (byte) 199;
    sourceArray2[26] = (byte) 210;
    sourceArray2[27] = (byte) 233;
    sourceArray2[28] = (byte) 167;
    sourceArray2[15] = (byte) 180;
    sourceArray2[30] = (byte) 75;
    sourceArray2[31 /*0x1F*/] = (byte) 121;
    sourceArray2[29] = (byte) 209;
    sourceArray2[33] = (byte) 217;
    sourceArray2[4] = (byte) 106;
    sourceArray2[35] = (byte) 246;
    sourceArray2[6] = (byte) 47;
    sourceArray2[37] = (byte) 198;
    sourceArray2[0] = (byte) 112 /*0x70*/;
    sourceArray2[8] = (byte) 196;
    sourceArray2[39] = (byte) 251;
    sourceArray2[41] = (byte) 8;
    sourceArray2[42] = (byte) 107;
    sourceArray2[23] = (byte) 97;
    sourceArray2[45] = (byte) 28;
    sourceArray2[5] = (byte) 195;
    sourceArray2[46] = (byte) 174;
    sourceArray2[47] = (byte) 68;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13353(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[21] = (byte) 62;
    sourceArray1[22] = (byte) 116;
    sourceArray1[2] = (byte) 119;
    sourceArray1[17] = (byte) 96 /*0x60*/;
    sourceArray1[4] = (byte) 1;
    sourceArray1[1] = (byte) 21;
    sourceArray1[6] = (byte) 190;
    sourceArray1[7] = (byte) 139;
    sourceArray1[42] = (byte) 230;
    sourceArray1[9] = (byte) 215;
    sourceArray1[44] = (byte) 210;
    sourceArray1[10] = (byte) 217;
    sourceArray1[12] = (byte) 248;
    sourceArray1[13] = (byte) 73;
    sourceArray1[20] = (byte) 144 /*0x90*/;
    sourceArray1[3] = (byte) 130;
    sourceArray1[16 /*0x10*/] = (byte) 66;
    sourceArray1[32 /*0x20*/] = (byte) 116;
    sourceArray1[35] = (byte) 115;
    sourceArray1[0] = (byte) 98;
    sourceArray1[15] = (byte) 252;
    sourceArray1[14] = (byte) 240 /*0xF0*/;
    sourceArray1[28] = (byte) 120;
    sourceArray1[27] = (byte) 184;
    sourceArray1[24] = (byte) 172;
    sourceArray1[47] = (byte) 154;
    sourceArray1[26] = (byte) 117;
    sourceArray1[33] = (byte) 240 /*0xF0*/;
    sourceArray1[45] = (byte) 108;
    sourceArray1[29] = (byte) 238;
    sourceArray1[19] = (byte) 51;
    sourceArray1[31 /*0x1F*/] = (byte) 166;
    sourceArray1[11] = (byte) 182;
    sourceArray1[38] = (byte) 99;
    sourceArray1[43] = (byte) 220;
    sourceArray1[34] = (byte) 62;
    sourceArray1[36] = (byte) 153;
    sourceArray1[25] = (byte) 241;
    sourceArray1[23] = (byte) 152;
    sourceArray1[8] = (byte) 126;
    sourceArray1[40] = (byte) 224 /*0xE0*/;
    sourceArray1[41] = (byte) 99;
    sourceArray1[37] = (byte) 166;
    sourceArray1[39] = (byte) 206;
    sourceArray1[30] = (byte) 62;
    sourceArray1[18] = (byte) 175;
    sourceArray1[46] = (byte) 130;
    sourceArray1[5] = (byte) 231;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 62,
      (byte) 92,
      (byte) 228,
      (byte) 196,
      (byte) 135,
      (byte) 165,
      (byte) 94,
      (byte) 13,
      (byte) 16 /*0x10*/,
      (byte) 241,
      (byte) 228,
      (byte) 112 /*0x70*/,
      (byte) 29,
      (byte) 94,
      (byte) 58,
      (byte) 182,
      (byte) 132,
      (byte) 84,
      (byte) 218,
      (byte) 154,
      (byte) 238,
      (byte) 6,
      (byte) 217,
      (byte) 217,
      (byte) 227,
      (byte) 55,
      (byte) 93,
      (byte) 45,
      (byte) 138,
      (byte) 109,
      (byte) 112 /*0x70*/,
      (byte) 170,
      (byte) 46,
      (byte) 52,
      (byte) 75,
      (byte) 7,
      (byte) 200,
      (byte) 13,
      (byte) 171,
      (byte) 107,
      (byte) 47,
      (byte) 155,
      (byte) 142,
      (byte) 200,
      (byte) 16 /*0x10*/,
      (byte) 133,
      (byte) 31 /*0x1F*/,
      (byte) 250
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13354(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 121,
      (byte) 143,
      (byte) 85,
      (byte) 249,
      (byte) 79,
      (byte) 2,
      (byte) 83,
      (byte) 144 /*0x90*/,
      (byte) 65,
      (byte) 204,
      (byte) 8,
      (byte) 25,
      (byte) 149,
      (byte) 160 /*0xA0*/,
      (byte) 151,
      (byte) 30,
      (byte) 247,
      (byte) 142,
      (byte) 89,
      (byte) 191,
      (byte) 177,
      (byte) 134,
      (byte) 240 /*0xF0*/,
      (byte) 169,
      (byte) 169,
      (byte) 202,
      (byte) 168,
      (byte) 132,
      (byte) 120,
      (byte) 221,
      (byte) 1,
      (byte) 84,
      (byte) 36,
      (byte) 21,
      (byte) 245,
      (byte) 75,
      (byte) 145,
      (byte) 34,
      (byte) 178,
      (byte) 60,
      (byte) 31 /*0x1F*/,
      byte.MaxValue,
      (byte) 64 /*0x40*/,
      (byte) 77,
      (byte) 202,
      (byte) 160 /*0xA0*/,
      (byte) 59,
      (byte) 179
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[11] = (byte) 4;
    sourceArray2[1] = (byte) 245;
    sourceArray2[25] = (byte) 207;
    sourceArray2[3] = (byte) 233;
    sourceArray2[4] = (byte) 42;
    sourceArray2[9] = (byte) 50;
    sourceArray2[17] = (byte) 54;
    sourceArray2[22] = (byte) 16 /*0x10*/;
    sourceArray2[8] = (byte) 6;
    sourceArray2[33] = (byte) 156;
    sourceArray2[7] = (byte) 110;
    sourceArray2[39] = (byte) 137;
    sourceArray2[12] = (byte) 117;
    sourceArray2[13] = (byte) 123;
    sourceArray2[35] = (byte) 46;
    sourceArray2[5] = (byte) 87;
    sourceArray2[23] = (byte) 41;
    sourceArray2[34] = (byte) 66;
    sourceArray2[18] = (byte) 131;
    sourceArray2[19] = (byte) 20;
    sourceArray2[16 /*0x10*/] = (byte) 17;
    sourceArray2[21] = (byte) 173;
    sourceArray2[32 /*0x20*/] = (byte) 249;
    sourceArray2[14] = (byte) 8;
    sourceArray2[24] = (byte) 207;
    sourceArray2[27] = (byte) 107;
    sourceArray2[26] = byte.MaxValue;
    sourceArray2[2] = (byte) 151;
    sourceArray2[15] = (byte) 73;
    sourceArray2[29] = (byte) 114;
    sourceArray2[30] = (byte) 145;
    sourceArray2[20] = (byte) 142;
    sourceArray2[28] = (byte) 203;
    sourceArray2[44] = (byte) 70;
    sourceArray2[10] = (byte) 247;
    sourceArray2[46] = (byte) 245;
    sourceArray2[36] = (byte) 52;
    sourceArray2[37] = (byte) 151;
    sourceArray2[38] = (byte) 228;
    sourceArray2[45] = (byte) 92;
    sourceArray2[40] = (byte) 132;
    sourceArray2[6] = (byte) 126;
    sourceArray2[42] = (byte) 84;
    sourceArray2[0] = (byte) 81;
    sourceArray2[43] = (byte) 122;
    sourceArray2[41] = (byte) 47;
    sourceArray2[31 /*0x1F*/] = (byte) 134;
    sourceArray2[47] = (byte) 180;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_13355()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[221];
      byte[] numArray2 = new byte[55];
      numArray2[6] = (byte) 236;
      numArray2[47] = (byte) 139;
      numArray2[51] = (byte) 140;
      numArray2[3] = (byte) 121;
      numArray2[35] = (byte) 211;
      numArray2[0] = (byte) 171;
      numArray2[26] = (byte) 2;
      numArray2[7] = (byte) 204;
      numArray2[44] = (byte) 83;
      numArray2[33] = (byte) 201;
      numArray2[10] = (byte) 174;
      numArray2[34] = (byte) 93;
      numArray2[19] = (byte) 144 /*0x90*/;
      numArray2[32 /*0x20*/] = (byte) 32 /*0x20*/;
      numArray2[18] = (byte) 223;
      numArray2[15] = (byte) 99;
      numArray2[16 /*0x10*/] = (byte) 16 /*0x10*/;
      numArray2[17] = (byte) 90;
      numArray2[27] = (byte) 173;
      numArray2[1] = (byte) 187;
      numArray2[20] = (byte) 73;
      numArray2[21] = (byte) 176 /*0xB0*/;
      numArray2[22] = (byte) 227;
      numArray2[23] = (byte) 169;
      numArray2[24] = (byte) 159;
      numArray2[48 /*0x30*/] = (byte) 56;
      numArray2[14] = (byte) 245;
      numArray2[5] = (byte) 131;
      numArray2[28] = (byte) 58;
      numArray2[29] = (byte) 80 /*0x50*/;
      numArray2[30] = (byte) 202;
      numArray2[31 /*0x1F*/] = (byte) 105;
      numArray2[12] = (byte) 203;
      numArray2[49] = (byte) 126;
      numArray2[11] = (byte) 252;
      numArray2[4] = (byte) 46;
      numArray2[36] = (byte) 237;
      numArray2[37] = (byte) 69;
      numArray2[38] = (byte) 142;
      numArray2[52] = (byte) 252;
      numArray2[40] = (byte) 77;
      numArray2[41] = (byte) 157;
      numArray2[42] = (byte) 4;
      numArray2[13] = (byte) 71;
      numArray2[2] = (byte) 7;
      numArray2[45] = (byte) 176 /*0xB0*/;
      numArray2[46] = (byte) 26;
      numArray2[43] = (byte) 211;
      numArray2[9] = (byte) 200;
      numArray2[39] = (byte) 1;
      numArray2[50] = (byte) 63 /*0x3F*/;
      numArray2[25] = (byte) 166;
      numArray2[8] = (byte) 63 /*0x3F*/;
      numArray2[53] = (byte) 241;
      numArray2[54] = (byte) 204;
      byte[] numArray3 = new byte[55];
      numArray3[53] = (byte) 163;
      numArray3[14] = (byte) 52;
      numArray3[44] = (byte) 179;
      numArray3[50] = (byte) 41;
      numArray3[32 /*0x20*/] = (byte) 78;
      numArray3[27] = (byte) 199;
      numArray3[6] = (byte) 57;
      numArray3[7] = (byte) 63 /*0x3F*/;
      numArray3[5] = (byte) 136;
      numArray3[9] = (byte) 193;
      numArray3[22] = (byte) 99;
      numArray3[38] = (byte) 28;
      numArray3[29] = (byte) 30;
      numArray3[34] = (byte) 74;
      numArray3[4] = (byte) 114;
      numArray3[15] = (byte) 208 /*0xD0*/;
      numArray3[28] = (byte) 129;
      numArray3[17] = (byte) 11;
      numArray3[12] = (byte) 1;
      numArray3[19] = (byte) 76;
      numArray3[20] = (byte) 223;
      numArray3[21] = (byte) 68;
      numArray3[24] = (byte) 145;
      numArray3[11] = (byte) 141;
      numArray3[18] = (byte) 69;
      numArray3[25] = (byte) 81;
      numArray3[26] = (byte) 158;
      numArray3[2] = (byte) 23;
      numArray3[37] = (byte) 132;
      numArray3[45] = (byte) 126;
      numArray3[30] = (byte) 125;
      numArray3[31 /*0x1F*/] = (byte) 50;
      numArray3[40] = (byte) 224 /*0xE0*/;
      numArray3[33] = (byte) 78;
      numArray3[1] = (byte) 202;
      numArray3[8] = (byte) 135;
      numArray3[36] = (byte) 86;
      numArray3[10] = (byte) 51;
      numArray3[16 /*0x10*/] = (byte) 126;
      numArray3[39] = (byte) 97;
      numArray3[35] = (byte) 169;
      numArray3[54] = (byte) 207;
      numArray3[42] = (byte) 48 /*0x30*/;
      numArray3[43] = (byte) 25;
      numArray3[46] = (byte) 156;
      numArray3[13] = (byte) 4;
      numArray3[23] = (byte) 19;
      numArray3[41] = (byte) 182;
      numArray3[48 /*0x30*/] = (byte) 57;
      numArray3[49] = (byte) 10;
      numArray3[47] = (byte) 171;
      numArray3[51] = (byte) 37;
      numArray3[52] = (byte) 169;
      numArray3[3] = (byte) 175;
      numArray3[0] = (byte) 216;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 193,
        (byte) 207,
        (byte) 136,
        (byte) 53,
        (byte) 105,
        (byte) 35,
        (byte) 65,
        (byte) 66,
        (byte) 137,
        (byte) 45,
        (byte) 38,
        (byte) 115,
        (byte) 240 /*0xF0*/,
        (byte) 94,
        (byte) 222,
        (byte) 188,
        (byte) 233,
        (byte) 235,
        (byte) 107,
        (byte) 93,
        (byte) 36,
        (byte) 15,
        (byte) 243,
        (byte) 95,
        (byte) 187,
        (byte) 110,
        (byte) 143,
        (byte) 135,
        (byte) 132,
        (byte) 142,
        (byte) 3,
        (byte) 93,
        (byte) 90,
        (byte) 167,
        (byte) 198,
        (byte) 176 /*0xB0*/,
        (byte) 129,
        (byte) 63 /*0x3F*/,
        (byte) 222,
        (byte) 17,
        (byte) 246,
        (byte) 210,
        (byte) 173,
        (byte) 249,
        (byte) 237,
        (byte) 166,
        (byte) 0,
        (byte) 196,
        (byte) 54,
        (byte) 60,
        (byte) 214,
        (byte) 143,
        (byte) 20,
        (byte) 92,
        (byte) 104
      };
      byte[] numArray5 = new byte[55]
      {
        (byte) 216,
        (byte) 248,
        (byte) 86,
        (byte) 75,
        (byte) 5,
        (byte) 97,
        (byte) 68,
        (byte) 139,
        (byte) 134,
        (byte) 232,
        (byte) 54,
        (byte) 142,
        (byte) 200,
        (byte) 101,
        (byte) 99,
        (byte) 42,
        (byte) 81,
        (byte) 127 /*0x7F*/,
        (byte) 57,
        (byte) 194,
        (byte) 111,
        (byte) 108,
        (byte) 159,
        (byte) 28,
        (byte) 71,
        (byte) 200,
        (byte) 108,
        (byte) 38,
        (byte) 29,
        (byte) 33,
        (byte) 236,
        (byte) 26,
        (byte) 223,
        (byte) 81,
        (byte) 141,
        (byte) 129,
        (byte) 138,
        (byte) 214,
        (byte) 175,
        (byte) 77,
        (byte) 74,
        (byte) 210,
        (byte) 125,
        (byte) 91,
        (byte) 134,
        (byte) 40,
        (byte) 146,
        (byte) 116,
        (byte) 115,
        (byte) 78,
        (byte) 65,
        (byte) 188,
        (byte) 112 /*0x70*/,
        (byte) 216,
        (byte) 85
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[55]
      {
        (byte) 40,
        (byte) 121,
        (byte) 250,
        (byte) 148,
        (byte) 186,
        (byte) 190,
        (byte) 251,
        (byte) 42,
        (byte) 200,
        (byte) 25,
        (byte) 73,
        (byte) 151,
        (byte) 45,
        (byte) 59,
        (byte) 170,
        (byte) 13,
        (byte) 90,
        (byte) 102,
        (byte) 81,
        (byte) 98,
        (byte) 250,
        (byte) 114,
        (byte) 20,
        (byte) 110,
        (byte) 203,
        (byte) 80 /*0x50*/,
        (byte) 72,
        (byte) 183,
        (byte) 116,
        (byte) 73,
        (byte) 253,
        (byte) 62,
        (byte) 21,
        (byte) 212,
        (byte) 88,
        (byte) 197,
        (byte) 227,
        (byte) 125,
        (byte) 60,
        (byte) 20,
        (byte) 177,
        (byte) 207,
        (byte) 36,
        (byte) 141,
        (byte) 182,
        (byte) 44,
        (byte) 8,
        (byte) 1,
        (byte) 44,
        (byte) 130,
        (byte) 186,
        (byte) 245,
        (byte) 22,
        (byte) 78,
        (byte) 225
      };
      byte[] numArray7 = new byte[55];
      numArray7[38] = (byte) 2;
      numArray7[10] = (byte) 214;
      numArray7[52] = (byte) 235;
      numArray7[6] = (byte) 188;
      numArray7[18] = (byte) 169;
      numArray7[41] = (byte) 55;
      numArray7[47] = (byte) 229;
      numArray7[40] = (byte) 101;
      numArray7[8] = (byte) 162;
      numArray7[15] = (byte) 225;
      numArray7[26] = (byte) 198;
      numArray7[11] = (byte) 248;
      numArray7[45] = (byte) 19;
      numArray7[16 /*0x10*/] = (byte) 94;
      numArray7[1] = (byte) 18;
      numArray7[29] = (byte) 155;
      numArray7[7] = (byte) 215;
      numArray7[17] = (byte) 158;
      numArray7[9] = (byte) 54;
      numArray7[19] = (byte) 116;
      numArray7[20] = (byte) 211;
      numArray7[21] = (byte) 69;
      numArray7[22] = (byte) 55;
      numArray7[23] = (byte) 136;
      numArray7[24] = (byte) 171;
      numArray7[25] = (byte) 230;
      numArray7[0] = (byte) 210;
      numArray7[27] = (byte) 32 /*0x20*/;
      numArray7[28] = (byte) 161;
      numArray7[48 /*0x30*/] = (byte) 157;
      numArray7[3] = (byte) 92;
      numArray7[2] = (byte) 81;
      numArray7[30] = (byte) 184;
      numArray7[33] = (byte) 123;
      numArray7[34] = (byte) 135;
      numArray7[32 /*0x20*/] = (byte) 117;
      numArray7[31 /*0x1F*/] = (byte) 210;
      numArray7[37] = (byte) 206;
      numArray7[39] = (byte) 31 /*0x1F*/;
      numArray7[4] = (byte) 173;
      numArray7[5] = (byte) 76;
      numArray7[12] = (byte) 94;
      numArray7[42] = (byte) 244;
      numArray7[43] = (byte) 165;
      numArray7[44] = (byte) 119;
      numArray7[53] = (byte) 133;
      numArray7[51] = (byte) 158;
      numArray7[35] = (byte) 123;
      numArray7[36] = (byte) 168;
      numArray7[49] = (byte) 200;
      numArray7[50] = (byte) 146;
      numArray7[13] = (byte) 32 /*0x20*/;
      numArray7[14] = (byte) 140;
      numArray7[46] = (byte) 223;
      numArray7[54] = (byte) 68;
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[55];
      numArray8[38] = (byte) 180;
      numArray8[47] = (byte) 134;
      numArray8[16 /*0x10*/] = (byte) 158;
      numArray8[29] = (byte) 78;
      numArray8[1] = (byte) 41;
      numArray8[27] = (byte) 178;
      numArray8[9] = (byte) 187;
      numArray8[7] = (byte) 19;
      numArray8[50] = (byte) 58;
      numArray8[45] = (byte) 221;
      numArray8[5] = (byte) 196;
      numArray8[11] = (byte) 64 /*0x40*/;
      numArray8[30] = (byte) 41;
      numArray8[52] = (byte) 163;
      numArray8[14] = (byte) 40;
      numArray8[15] = (byte) 86;
      numArray8[25] = (byte) 68;
      numArray8[17] = (byte) 68;
      numArray8[6] = (byte) 239;
      numArray8[19] = (byte) 169;
      numArray8[53] = (byte) 190;
      numArray8[21] = (byte) 12;
      numArray8[22] = (byte) 245;
      numArray8[23] = (byte) 152;
      numArray8[4] = (byte) 184;
      numArray8[34] = (byte) 130;
      numArray8[0] = (byte) 212;
      numArray8[54] = (byte) 198;
      numArray8[28] = (byte) 60;
      numArray8[12] = (byte) 131;
      numArray8[31 /*0x1F*/] = (byte) 61;
      numArray8[20] = (byte) 22;
      numArray8[32 /*0x20*/] = (byte) 58;
      numArray8[33] = (byte) 131;
      numArray8[26] = (byte) 152;
      numArray8[35] = (byte) 139;
      numArray8[36] = (byte) 178;
      numArray8[37] = (byte) 111;
      numArray8[46] = (byte) 195;
      numArray8[39] = (byte) 84;
      numArray8[2] = (byte) 32 /*0x20*/;
      numArray8[13] = (byte) 210;
      numArray8[42] = (byte) 199;
      numArray8[43] = (byte) 237;
      numArray8[10] = (byte) 101;
      numArray8[41] = (byte) 93;
      numArray8[40] = (byte) 181;
      numArray8[3] = (byte) 47;
      numArray8[48 /*0x30*/] = (byte) 180;
      numArray8[49] = (byte) 117;
      numArray8[24] = (byte) 40;
      numArray8[51] = (byte) 88;
      numArray8[44] = (byte) 74;
      numArray8[18] = (byte) 18;
      numArray8[8] = (byte) 227;
      byte[] numArray9 = new byte[55]
      {
        (byte) 21,
        (byte) 204,
        (byte) 219,
        (byte) 139,
        (byte) 41,
        (byte) 32 /*0x20*/,
        (byte) 132,
        (byte) 235,
        (byte) 46,
        (byte) 44,
        (byte) 253,
        (byte) 151,
        (byte) 198,
        (byte) 59,
        (byte) 115,
        (byte) 148,
        (byte) 215,
        (byte) 92,
        (byte) 224 /*0xE0*/,
        (byte) 163,
        (byte) 209,
        (byte) 199,
        (byte) 117,
        (byte) 3,
        (byte) 86,
        (byte) 249,
        (byte) 253,
        (byte) 72,
        (byte) 49,
        (byte) 128 /*0x80*/,
        (byte) 170,
        (byte) 46,
        (byte) 115,
        (byte) 7,
        (byte) 77,
        (byte) 149,
        (byte) 157,
        (byte) 105,
        (byte) 29,
        (byte) 84,
        (byte) 57,
        (byte) 149,
        (byte) 4,
        (byte) 156,
        (byte) 147,
        (byte) 16 /*0x10*/,
        (byte) 215,
        (byte) 145,
        (byte) 156,
        (byte) 97,
        (byte) 92,
        (byte) 195,
        (byte) 171,
        (byte) 114,
        (byte) 107
      };
      key.Query(true, 335, numArray8, numArray8);
      Array.Copy((Array) numArray8, 0, (Array) numArray1, 165, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 165] ^= numArray9[index];
      byte[] numArray10 = new byte[1]{ (byte) 58 };
      byte[] numArray11 = new byte[1]{ (byte) 55 };
      key.Query(true, 335, numArray10, numArray10);
      Array.Copy((Array) numArray10, 0, (Array) numArray1, 220, 1);
      for (int index = 0; index < 1; ++index)
        numArray1[index + 220] ^= numArray11[index];
      byte[] numArray12 = new byte[13];
      byte[] response = new byte[13];
      Array.Copy((Array) sc_13302.sspq, 145, (Array) numArray12, 0, 13);
      key.Query(true, 335, numArray12, response);
      Array.Copy((Array) sc_13302.sspr, 145, (Array) numArray12, 0, 13);
      for (int index = 0; index < numArray12.Length; ++index)
      {
        if ((int) numArray12[index] != (int) response[index])
        {
          key.TagValue = (int) response[index];
          break;
        }
      }
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray13 = new byte[221];
    byte[] numArray14 = new byte[55]
    {
      (byte) 171,
      (byte) 44,
      (byte) 116,
      (byte) 188,
      (byte) 220,
      (byte) 230,
      (byte) 244,
      (byte) 7,
      (byte) 9,
      (byte) 180,
      (byte) 176 /*0xB0*/,
      (byte) 176 /*0xB0*/,
      (byte) 55,
      (byte) 42,
      (byte) 239,
      (byte) 174,
      (byte) 75,
      (byte) 16 /*0x10*/,
      (byte) 55,
      (byte) 117,
      (byte) 176 /*0xB0*/,
      (byte) 155,
      (byte) 147,
      (byte) 75,
      (byte) 129,
      (byte) 160 /*0xA0*/,
      (byte) 208 /*0xD0*/,
      (byte) 88,
      (byte) 70,
      (byte) 120,
      (byte) 80 /*0x50*/,
      (byte) 78,
      (byte) 218,
      (byte) 112 /*0x70*/,
      (byte) 128 /*0x80*/,
      (byte) 14,
      (byte) 70,
      (byte) 55,
      (byte) 60,
      (byte) 88,
      (byte) 28,
      (byte) 122,
      (byte) 182,
      (byte) 217,
      (byte) 219,
      (byte) 146,
      (byte) 93,
      (byte) 27,
      (byte) 137,
      (byte) 23,
      (byte) 4,
      (byte) 132,
      (byte) 68,
      (byte) 189,
      (byte) 98
    };
    byte[] numArray15 = new byte[55]
    {
      (byte) 161,
      (byte) 250,
      (byte) 57,
      (byte) 133,
      (byte) 27,
      (byte) 152,
      (byte) 225,
      (byte) 82,
      (byte) 235,
      (byte) 54,
      (byte) 26,
      (byte) 132,
      (byte) 1,
      (byte) 124,
      (byte) 216,
      (byte) 222,
      (byte) 181,
      (byte) 83,
      (byte) 180,
      (byte) 251,
      (byte) 140,
      (byte) 34,
      (byte) 160 /*0xA0*/,
      (byte) 153,
      (byte) 133,
      (byte) 213,
      (byte) 22,
      (byte) 9,
      (byte) 154,
      (byte) 225,
      (byte) 217,
      (byte) 36,
      (byte) 206,
      (byte) 1,
      (byte) 222,
      (byte) 76,
      (byte) 168,
      (byte) 19,
      (byte) 23,
      (byte) 208 /*0xD0*/,
      (byte) 107,
      (byte) 238,
      (byte) 98,
      (byte) 134,
      (byte) 141,
      (byte) 254,
      (byte) 35,
      (byte) 176 /*0xB0*/,
      (byte) 60,
      (byte) 209,
      (byte) 188,
      (byte) 158,
      (byte) 144 /*0x90*/,
      (byte) 147,
      (byte) 195
    };
    key.Query(true, 335, numArray14, numArray14);
    Array.Copy((Array) numArray14, 0, (Array) numArray13, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray13[index] ^= numArray15[index];
    byte[] numArray16 = new byte[55];
    numArray16[34] = (byte) 29;
    numArray16[54] = (byte) 221;
    numArray16[2] = (byte) 59;
    numArray16[28] = (byte) 203;
    numArray16[10] = (byte) 199;
    numArray16[50] = (byte) 101;
    numArray16[6] = (byte) 145;
    numArray16[27] = (byte) 115;
    numArray16[0] = (byte) 41;
    numArray16[9] = (byte) 213;
    numArray16[23] = (byte) 66;
    numArray16[52] = (byte) 44;
    numArray16[8] = (byte) 97;
    numArray16[24] = (byte) 181;
    numArray16[38] = (byte) 128 /*0x80*/;
    numArray16[17] = (byte) 88;
    numArray16[16 /*0x10*/] = (byte) 222;
    numArray16[13] = (byte) 104;
    numArray16[46] = (byte) 168;
    numArray16[19] = (byte) 228;
    numArray16[3] = (byte) 22;
    numArray16[21] = (byte) 112 /*0x70*/;
    numArray16[22] = (byte) 238;
    numArray16[37] = (byte) 66;
    numArray16[14] = (byte) 19;
    numArray16[25] = (byte) 207;
    numArray16[26] = (byte) 46;
    numArray16[40] = (byte) 37;
    numArray16[33] = (byte) 118;
    numArray16[42] = (byte) 122;
    numArray16[11] = (byte) 170;
    numArray16[31 /*0x1F*/] = (byte) 14;
    numArray16[32 /*0x20*/] = (byte) 111;
    numArray16[53] = (byte) 43;
    numArray16[5] = (byte) 193;
    numArray16[35] = (byte) 252;
    numArray16[1] = (byte) 59;
    numArray16[49] = (byte) 176 /*0xB0*/;
    numArray16[18] = (byte) 53;
    numArray16[39] = (byte) 185;
    numArray16[20] = (byte) 186;
    numArray16[41] = (byte) 117;
    numArray16[15] = (byte) 212;
    numArray16[43] = (byte) 189;
    numArray16[44] = (byte) 150;
    numArray16[45] = (byte) 113;
    numArray16[36] = (byte) 0;
    numArray16[47] = (byte) 175;
    numArray16[48 /*0x30*/] = (byte) 240 /*0xF0*/;
    numArray16[30] = (byte) 28;
    numArray16[7] = (byte) 100;
    numArray16[51] = (byte) 100;
    numArray16[29] = (byte) 10;
    numArray16[12] = (byte) 225;
    numArray16[4] = (byte) 155;
    byte[] numArray17 = new byte[55];
    numArray17[11] = (byte) 153;
    numArray17[1] = (byte) 212;
    numArray17[2] = byte.MaxValue;
    numArray17[50] = (byte) 225;
    numArray17[4] = (byte) 199;
    numArray17[5] = (byte) 104;
    numArray17[45] = (byte) 123;
    numArray17[7] = (byte) 87;
    numArray17[8] = (byte) 19;
    numArray17[9] = (byte) 242;
    numArray17[10] = (byte) 229;
    numArray17[26] = (byte) 120;
    numArray17[28] = (byte) 13;
    numArray17[22] = (byte) 197;
    numArray17[14] = (byte) 43;
    numArray17[16 /*0x10*/] = (byte) 193;
    numArray17[0] = (byte) 126;
    numArray17[17] = (byte) 89;
    numArray17[18] = (byte) 55;
    numArray17[19] = (byte) 123;
    numArray17[47] = (byte) 46;
    numArray17[15] = (byte) 55;
    numArray17[40] = (byte) 70;
    numArray17[13] = (byte) 225;
    numArray17[24] = (byte) 13;
    numArray17[6] = (byte) 219;
    numArray17[12] = (byte) 58;
    numArray17[27] = (byte) 196;
    numArray17[36] = (byte) 22;
    numArray17[29] = (byte) 45;
    numArray17[30] = (byte) 158;
    numArray17[33] = (byte) 142;
    numArray17[32 /*0x20*/] = (byte) 116;
    numArray17[31 /*0x1F*/] = (byte) 29;
    numArray17[51] = (byte) 132;
    numArray17[21] = byte.MaxValue;
    numArray17[35] = (byte) 37;
    numArray17[53] = (byte) 247;
    numArray17[38] = (byte) 106;
    numArray17[39] = (byte) 21;
    numArray17[37] = (byte) 125;
    numArray17[41] = (byte) 4;
    numArray17[42] = (byte) 220;
    numArray17[43] = (byte) 195;
    numArray17[49] = (byte) 1;
    numArray17[23] = (byte) 106;
    numArray17[46] = (byte) 248;
    numArray17[44] = (byte) 43;
    numArray17[3] = (byte) 30;
    numArray17[34] = (byte) 88;
    numArray17[25] = (byte) 115;
    numArray17[48 /*0x30*/] = (byte) 144 /*0x90*/;
    numArray17[52] = (byte) 161;
    numArray17[20] = (byte) 167;
    numArray17[54] = (byte) 237;
    key.Query(true, 335, numArray16, numArray16);
    Array.Copy((Array) numArray16, 0, (Array) numArray13, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray13[index + 55] ^= numArray17[index];
    byte[] numArray18 = new byte[55]
    {
      (byte) 125,
      (byte) 15,
      (byte) 187,
      (byte) 247,
      (byte) 241,
      (byte) 61,
      (byte) 215,
      (byte) 212,
      (byte) 167,
      (byte) 29,
      (byte) 211,
      (byte) 68,
      (byte) 213,
      (byte) 18,
      (byte) 246,
      (byte) 21,
      (byte) 63 /*0x3F*/,
      (byte) 74,
      (byte) 6,
      (byte) 98,
      (byte) 40,
      (byte) 201,
      (byte) 204,
      (byte) 127 /*0x7F*/,
      (byte) 157,
      (byte) 57,
      (byte) 47,
      (byte) 203,
      (byte) 195,
      (byte) 211,
      (byte) 35,
      (byte) 205,
      (byte) 178,
      (byte) 164,
      (byte) 60,
      (byte) 180,
      (byte) 82,
      (byte) 29,
      (byte) 123,
      (byte) 35,
      (byte) 152,
      (byte) 47,
      (byte) 93,
      (byte) 34,
      (byte) 150,
      (byte) 56,
      (byte) 155,
      (byte) 253,
      (byte) 25,
      (byte) 82,
      (byte) 28,
      (byte) 105,
      (byte) 43,
      (byte) 140,
      (byte) 198
    };
    byte[] numArray19 = new byte[55];
    numArray19[17] = (byte) 172;
    numArray19[30] = (byte) 228;
    numArray19[35] = (byte) 203;
    numArray19[3] = (byte) 151;
    numArray19[32 /*0x20*/] = (byte) 83;
    numArray19[7] = (byte) 156;
    numArray19[2] = (byte) 134;
    numArray19[21] = (byte) 69;
    numArray19[31 /*0x1F*/] = (byte) 35;
    numArray19[28] = (byte) 238;
    numArray19[53] = (byte) 74;
    numArray19[11] = (byte) 254;
    numArray19[5] = (byte) 107;
    numArray19[13] = (byte) 90;
    numArray19[44] = (byte) 225;
    numArray19[15] = (byte) 242;
    numArray19[10] = (byte) 168;
    numArray19[8] = (byte) 51;
    numArray19[18] = (byte) 205;
    numArray19[25] = (byte) 20;
    numArray19[4] = (byte) 228;
    numArray19[38] = (byte) 55;
    numArray19[22] = (byte) 161;
    numArray19[48 /*0x30*/] = (byte) 4;
    numArray19[24] = (byte) 174;
    numArray19[23] = (byte) 186;
    numArray19[26] = (byte) 205;
    numArray19[27] = (byte) 130;
    numArray19[19] = (byte) 67;
    numArray19[20] = (byte) 92;
    numArray19[16 /*0x10*/] = (byte) 174;
    numArray19[34] = (byte) 149;
    numArray19[47] = (byte) 90;
    numArray19[33] = (byte) 10;
    numArray19[29] = (byte) 169;
    numArray19[9] = (byte) 234;
    numArray19[36] = (byte) 67;
    numArray19[37] = (byte) 111;
    numArray19[6] = (byte) 207;
    numArray19[39] = (byte) 154;
    numArray19[40] = (byte) 61;
    numArray19[43] = (byte) 199;
    numArray19[42] = (byte) 103;
    numArray19[12] = (byte) 124;
    numArray19[46] = (byte) 226;
    numArray19[45] = (byte) 251;
    numArray19[54] = (byte) 0;
    numArray19[41] = (byte) 212;
    numArray19[14] = (byte) 64 /*0x40*/;
    numArray19[49] = (byte) 223;
    numArray19[50] = (byte) 0;
    numArray19[51] = (byte) 35;
    numArray19[52] = (byte) 187;
    numArray19[1] = (byte) 159;
    numArray19[0] = (byte) 110;
    key.Query(true, 335, numArray18, numArray18);
    Array.Copy((Array) numArray18, 0, (Array) numArray13, 110, 55);
    for (int index = 0; index < 55; ++index)
      numArray13[index + 110] ^= numArray19[index];
    byte[] numArray20 = new byte[55]
    {
      (byte) 82,
      (byte) 244,
      (byte) 25,
      (byte) 219,
      (byte) 90,
      (byte) 161,
      (byte) 28,
      (byte) 64 /*0x40*/,
      (byte) 100,
      (byte) 108,
      (byte) 82,
      (byte) 76,
      (byte) 208 /*0xD0*/,
      (byte) 172,
      (byte) 95,
      (byte) 10,
      (byte) 204,
      (byte) 62,
      (byte) 220,
      (byte) 225,
      (byte) 129,
      (byte) 89,
      (byte) 191,
      (byte) 51,
      (byte) 9,
      (byte) 218,
      (byte) 225,
      (byte) 189,
      (byte) 30,
      (byte) 89,
      (byte) 99,
      (byte) 17,
      (byte) 63 /*0x3F*/,
      (byte) 217,
      (byte) 195,
      (byte) 206,
      (byte) 170,
      (byte) 59,
      (byte) 49,
      (byte) 124,
      (byte) 109,
      (byte) 231,
      (byte) 42,
      (byte) 57,
      (byte) 23,
      (byte) 219,
      (byte) 7,
      (byte) 226,
      (byte) 154,
      (byte) 144 /*0x90*/,
      (byte) 123,
      (byte) 131,
      (byte) 240 /*0xF0*/,
      (byte) 103,
      (byte) 22
    };
    byte[] numArray21 = new byte[55];
    numArray21[28] = (byte) 216;
    numArray21[34] = (byte) 156;
    numArray21[0] = (byte) 245;
    numArray21[3] = (byte) 142;
    numArray21[4] = (byte) 60;
    numArray21[14] = (byte) 130;
    numArray21[6] = (byte) 68;
    numArray21[7] = (byte) 147;
    numArray21[27] = (byte) 63 /*0x3F*/;
    numArray21[30] = (byte) 31 /*0x1F*/;
    numArray21[22] = (byte) 185;
    numArray21[15] = (byte) 243;
    numArray21[12] = (byte) 50;
    numArray21[13] = (byte) 168;
    numArray21[48 /*0x30*/] = (byte) 13;
    numArray21[11] = (byte) 199;
    numArray21[50] = (byte) 100;
    numArray21[17] = (byte) 65;
    numArray21[45] = (byte) 0;
    numArray21[20] = (byte) 180;
    numArray21[35] = (byte) 6;
    numArray21[2] = (byte) 99;
    numArray21[33] = (byte) 90;
    numArray21[23] = (byte) 147;
    numArray21[24] = (byte) 86;
    numArray21[25] = (byte) 66;
    numArray21[31 /*0x1F*/] = (byte) 9;
    numArray21[49] = (byte) 110;
    numArray21[54] = (byte) 157;
    numArray21[29] = (byte) 133;
    numArray21[52] = (byte) 230;
    numArray21[38] = (byte) 173;
    numArray21[9] = (byte) 216;
    numArray21[8] = (byte) 84;
    numArray21[53] = (byte) 174;
    numArray21[46] = (byte) 245;
    numArray21[36] = (byte) 126;
    numArray21[37] = (byte) 16 /*0x10*/;
    numArray21[16 /*0x10*/] = (byte) 222;
    numArray21[39] = (byte) 47;
    numArray21[40] = (byte) 67;
    numArray21[41] = (byte) 171;
    numArray21[42] = (byte) 82;
    numArray21[43] = (byte) 173;
    numArray21[44] = (byte) 89;
    numArray21[26] = (byte) 208 /*0xD0*/;
    numArray21[21] = (byte) 132;
    numArray21[47] = (byte) 234;
    numArray21[18] = (byte) 178;
    numArray21[32 /*0x20*/] = (byte) 10;
    numArray21[10] = (byte) 122;
    numArray21[51] = (byte) 149;
    numArray21[19] = (byte) 89;
    numArray21[1] = (byte) 203;
    numArray21[5] = (byte) 111;
    key.Query(true, 335, numArray20, numArray20);
    Array.Copy((Array) numArray20, 0, (Array) numArray13, 165, 55);
    for (int index = 0; index < 55; ++index)
      numArray13[index + 165] ^= numArray21[index];
    byte[] numArray22 = new byte[1]{ (byte) 201 };
    byte[] numArray23 = new byte[1]{ (byte) 121 };
    key.Query(true, 335, numArray22, numArray22);
    Array.Copy((Array) numArray22, 0, (Array) numArray13, 220, 1);
    for (int index = 0; index < 1; ++index)
      numArray13[index + 220] ^= numArray23[index];
    return Encoding.UTF8.GetString(numArray13);
  }

  internal static int ssp_appserver_13356(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[2] = (byte) 52;
    sourceArray1[1] = (byte) 30;
    sourceArray1[6] = (byte) 155;
    sourceArray1[43] = (byte) 237;
    sourceArray1[22] = (byte) 145;
    sourceArray1[10] = (byte) 102;
    sourceArray1[46] = (byte) 185;
    sourceArray1[7] = (byte) 39;
    sourceArray1[12] = (byte) 16 /*0x10*/;
    sourceArray1[28] = (byte) 198;
    sourceArray1[18] = (byte) 24;
    sourceArray1[13] = (byte) 85;
    sourceArray1[23] = (byte) 228;
    sourceArray1[35] = (byte) 183;
    sourceArray1[14] = (byte) 69;
    sourceArray1[0] = (byte) 40;
    sourceArray1[16 /*0x10*/] = (byte) 195;
    sourceArray1[17] = (byte) 5;
    sourceArray1[4] = (byte) 31 /*0x1F*/;
    sourceArray1[39] = (byte) 18;
    sourceArray1[15] = (byte) 94;
    sourceArray1[21] = (byte) 105;
    sourceArray1[47] = (byte) 89;
    sourceArray1[24] = (byte) 69;
    sourceArray1[34] = (byte) 7;
    sourceArray1[30] = (byte) 250;
    sourceArray1[11] = (byte) 212;
    sourceArray1[27] = (byte) 250;
    sourceArray1[5] = (byte) 247;
    sourceArray1[8] = (byte) 90;
    sourceArray1[3] = (byte) 52;
    sourceArray1[31 /*0x1F*/] = (byte) 208 /*0xD0*/;
    sourceArray1[29] = (byte) 6;
    sourceArray1[25] = (byte) 0;
    sourceArray1[32 /*0x20*/] = (byte) 49;
    sourceArray1[26] = (byte) 233;
    sourceArray1[33] = (byte) 241;
    sourceArray1[37] = (byte) 219;
    sourceArray1[38] = (byte) 217;
    sourceArray1[44] = (byte) 141;
    sourceArray1[40] = (byte) 148;
    sourceArray1[41] = (byte) 195;
    sourceArray1[36] = (byte) 154;
    sourceArray1[20] = (byte) 242;
    sourceArray1[9] = (byte) 163;
    sourceArray1[45] = (byte) 167;
    sourceArray1[19] = (byte) 93;
    sourceArray1[42] = (byte) 27;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[23] = (byte) 204;
    sourceArray2[33] = (byte) 153;
    sourceArray2[2] = (byte) 117;
    sourceArray2[40] = (byte) 71;
    sourceArray2[31 /*0x1F*/] = (byte) 47;
    sourceArray2[5] = (byte) 13;
    sourceArray2[6] = (byte) 29;
    sourceArray2[8] = (byte) 188;
    sourceArray2[47] = (byte) 87;
    sourceArray2[9] = (byte) 14;
    sourceArray2[10] = (byte) 139;
    sourceArray2[15] = (byte) 58;
    sourceArray2[12] = (byte) 95;
    sourceArray2[0] = (byte) 92;
    sourceArray2[37] = (byte) 56;
    sourceArray2[4] = (byte) 101;
    sourceArray2[16 /*0x10*/] = (byte) 103;
    sourceArray2[17] = (byte) 187;
    sourceArray2[18] = (byte) 62;
    sourceArray2[13] = (byte) 72;
    sourceArray2[20] = (byte) 132;
    sourceArray2[11] = (byte) 72;
    sourceArray2[46] = (byte) 40;
    sourceArray2[45] = (byte) 199;
    sourceArray2[42] = (byte) 25;
    sourceArray2[25] = (byte) 139;
    sourceArray2[26] = (byte) 144 /*0x90*/;
    sourceArray2[24] = (byte) 8;
    sourceArray2[22] = (byte) 95;
    sourceArray2[7] = (byte) 194;
    sourceArray2[29] = (byte) 209;
    sourceArray2[38] = (byte) 113;
    sourceArray2[32 /*0x20*/] = (byte) 102;
    sourceArray2[39] = (byte) 46;
    sourceArray2[34] = (byte) 51;
    sourceArray2[35] = (byte) 202;
    sourceArray2[36] = (byte) 220;
    sourceArray2[19] = (byte) 54;
    sourceArray2[21] = (byte) 99;
    sourceArray2[27] = (byte) 247;
    sourceArray2[3] = (byte) 166;
    sourceArray2[41] = (byte) 198;
    sourceArray2[14] = (byte) 86;
    sourceArray2[43] = (byte) 200;
    sourceArray2[44] = (byte) 114;
    sourceArray2[30] = (byte) 4;
    sourceArray2[28] = (byte) 167;
    sourceArray2[1] = (byte) 14;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13357(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[15] = (byte) 62;
    sourceArray1[1] = (byte) 200;
    sourceArray1[2] = (byte) 131;
    sourceArray1[43] = (byte) 238;
    sourceArray1[11] = (byte) 6;
    sourceArray1[17] = (byte) 137;
    sourceArray1[6] = (byte) 92;
    sourceArray1[7] = (byte) 50;
    sourceArray1[47] = (byte) 111;
    sourceArray1[40] = (byte) 60;
    sourceArray1[14] = (byte) 150;
    sourceArray1[33] = (byte) 142;
    sourceArray1[12] = (byte) 32 /*0x20*/;
    sourceArray1[8] = (byte) 3;
    sourceArray1[25] = (byte) 188;
    sourceArray1[38] = (byte) 131;
    sourceArray1[16 /*0x10*/] = (byte) 235;
    sourceArray1[42] = (byte) 128 /*0x80*/;
    sourceArray1[44] = (byte) 251;
    sourceArray1[19] = (byte) 183;
    sourceArray1[36] = (byte) 73;
    sourceArray1[28] = (byte) 190;
    sourceArray1[22] = (byte) 170;
    sourceArray1[18] = (byte) 152;
    sourceArray1[21] = (byte) 251;
    sourceArray1[13] = (byte) 29;
    sourceArray1[27] = (byte) 26;
    sourceArray1[5] = (byte) 14;
    sourceArray1[9] = (byte) 146;
    sourceArray1[29] = (byte) 49;
    sourceArray1[30] = (byte) 33;
    sourceArray1[3] = (byte) 145;
    sourceArray1[32 /*0x20*/] = (byte) 94;
    sourceArray1[26] = (byte) 93;
    sourceArray1[20] = (byte) 45;
    sourceArray1[31 /*0x1F*/] = (byte) 39;
    sourceArray1[4] = (byte) 219;
    sourceArray1[41] = (byte) 121;
    sourceArray1[37] = (byte) 244;
    sourceArray1[39] = (byte) 81;
    sourceArray1[23] = (byte) 221;
    sourceArray1[34] = (byte) 125;
    sourceArray1[10] = (byte) 6;
    sourceArray1[24] = (byte) 24;
    sourceArray1[0] = (byte) 132;
    sourceArray1[45] = (byte) 23;
    sourceArray1[46] = (byte) 140;
    sourceArray1[35] = (byte) 14;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[47] = (byte) 251;
    sourceArray2[45] = (byte) 232;
    sourceArray2[2] = (byte) 16 /*0x10*/;
    sourceArray2[30] = (byte) 224 /*0xE0*/;
    sourceArray2[41] = (byte) 69;
    sourceArray2[5] = (byte) 19;
    sourceArray2[6] = (byte) 51;
    sourceArray2[18] = (byte) 234;
    sourceArray2[8] = (byte) 153;
    sourceArray2[28] = (byte) 220;
    sourceArray2[36] = (byte) 229;
    sourceArray2[11] = (byte) 104;
    sourceArray2[4] = (byte) 182;
    sourceArray2[42] = (byte) 243;
    sourceArray2[23] = (byte) 194;
    sourceArray2[37] = (byte) 198;
    sourceArray2[16 /*0x10*/] = (byte) 9;
    sourceArray2[17] = (byte) 139;
    sourceArray2[7] = (byte) 89;
    sourceArray2[19] = (byte) 213;
    sourceArray2[20] = (byte) 203;
    sourceArray2[13] = (byte) 12;
    sourceArray2[22] = (byte) 2;
    sourceArray2[21] = (byte) 171;
    sourceArray2[24] = (byte) 58;
    sourceArray2[25] = (byte) 60;
    sourceArray2[26] = (byte) 66;
    sourceArray2[12] = (byte) 247;
    sourceArray2[3] = (byte) 44;
    sourceArray2[29] = (byte) 69;
    sourceArray2[31 /*0x1F*/] = (byte) 50;
    sourceArray2[15] = (byte) 61;
    sourceArray2[32 /*0x20*/] = (byte) 107;
    sourceArray2[1] = (byte) 132;
    sourceArray2[34] = (byte) 136;
    sourceArray2[35] = (byte) 25;
    sourceArray2[40] = (byte) 124;
    sourceArray2[38] = (byte) 72;
    sourceArray2[10] = (byte) 222;
    sourceArray2[14] = (byte) 13;
    sourceArray2[27] = (byte) 210;
    sourceArray2[39] = (byte) 86;
    sourceArray2[9] = (byte) 165;
    sourceArray2[43] = (byte) 111;
    sourceArray2[44] = (byte) 30;
    sourceArray2[46] = (byte) 67;
    sourceArray2[0] = (byte) 6;
    sourceArray2[33] = (byte) 219;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13358(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[46] = (byte) 171;
    sourceArray1[1] = (byte) 219;
    sourceArray1[25] = (byte) 77;
    sourceArray1[3] = (byte) 34;
    sourceArray1[30] = (byte) 7;
    sourceArray1[5] = (byte) 169;
    sourceArray1[33] = (byte) 48 /*0x30*/;
    sourceArray1[29] = (byte) 198;
    sourceArray1[8] = (byte) 34;
    sourceArray1[17] = (byte) 35;
    sourceArray1[23] = (byte) 70;
    sourceArray1[11] = (byte) 118;
    sourceArray1[18] = (byte) 251;
    sourceArray1[38] = (byte) 199;
    sourceArray1[19] = (byte) 231;
    sourceArray1[37] = (byte) 170;
    sourceArray1[42] = (byte) 252;
    sourceArray1[13] = (byte) 206;
    sourceArray1[9] = (byte) 217;
    sourceArray1[2] = (byte) 228;
    sourceArray1[10] = (byte) 39;
    sourceArray1[21] = (byte) 163;
    sourceArray1[22] = (byte) 109;
    sourceArray1[43] = (byte) 217;
    sourceArray1[24] = (byte) 55;
    sourceArray1[32 /*0x20*/] = (byte) 122;
    sourceArray1[0] = (byte) 221;
    sourceArray1[7] = (byte) 67;
    sourceArray1[28] = (byte) 220;
    sourceArray1[15] = (byte) 80 /*0x50*/;
    sourceArray1[47] = (byte) 42;
    sourceArray1[14] = (byte) 234;
    sourceArray1[16 /*0x10*/] = (byte) 65;
    sourceArray1[6] = (byte) 65;
    sourceArray1[34] = (byte) 199;
    sourceArray1[12] = (byte) 13;
    sourceArray1[36] = (byte) 203;
    sourceArray1[20] = (byte) 188;
    sourceArray1[27] = (byte) 31 /*0x1F*/;
    sourceArray1[39] = (byte) 29;
    sourceArray1[40] = (byte) 79;
    sourceArray1[41] = (byte) 240 /*0xF0*/;
    sourceArray1[26] = (byte) 122;
    sourceArray1[31 /*0x1F*/] = (byte) 109;
    sourceArray1[44] = (byte) 179;
    sourceArray1[45] = (byte) 221;
    sourceArray1[4] = (byte) 118;
    sourceArray1[35] = (byte) 164;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[10] = (byte) 217;
    sourceArray2[7] = (byte) 186;
    sourceArray2[2] = (byte) 44;
    sourceArray2[14] = (byte) 164;
    sourceArray2[4] = (byte) 207;
    sourceArray2[5] = (byte) 11;
    sourceArray2[1] = (byte) 144 /*0x90*/;
    sourceArray2[27] = (byte) 77;
    sourceArray2[28] = (byte) 156;
    sourceArray2[12] = (byte) 58;
    sourceArray2[31 /*0x1F*/] = (byte) 46;
    sourceArray2[29] = (byte) 241;
    sourceArray2[3] = (byte) 246;
    sourceArray2[47] = (byte) 53;
    sourceArray2[41] = (byte) 101;
    sourceArray2[15] = (byte) 34;
    sourceArray2[19] = (byte) 161;
    sourceArray2[23] = (byte) 43;
    sourceArray2[18] = (byte) 246;
    sourceArray2[34] = (byte) 129;
    sourceArray2[13] = (byte) 202;
    sourceArray2[17] = (byte) 94;
    sourceArray2[22] = (byte) 14;
    sourceArray2[33] = (byte) 189;
    sourceArray2[0] = (byte) 5;
    sourceArray2[35] = (byte) 17;
    sourceArray2[26] = (byte) 167;
    sourceArray2[32 /*0x20*/] = (byte) 129;
    sourceArray2[20] = (byte) 182;
    sourceArray2[6] = (byte) 195;
    sourceArray2[30] = (byte) 211;
    sourceArray2[37] = (byte) 32 /*0x20*/;
    sourceArray2[8] = (byte) 181;
    sourceArray2[36] = (byte) 85;
    sourceArray2[25] = (byte) 171;
    sourceArray2[11] = (byte) 105;
    sourceArray2[16 /*0x10*/] = (byte) 161;
    sourceArray2[9] = (byte) 146;
    sourceArray2[38] = (byte) 79;
    sourceArray2[39] = (byte) 129;
    sourceArray2[40] = (byte) 141;
    sourceArray2[24] = (byte) 180;
    sourceArray2[42] = (byte) 199;
    sourceArray2[43] = (byte) 190;
    sourceArray2[44] = (byte) 210;
    sourceArray2[45] = (byte) 111;
    sourceArray2[46] = (byte) 59;
    sourceArray2[21] = (byte) 172;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13359(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[30] = (byte) 180;
    sourceArray1[16 /*0x10*/] = (byte) 139;
    sourceArray1[2] = (byte) 117;
    sourceArray1[3] = (byte) 100;
    sourceArray1[40] = (byte) 47;
    sourceArray1[29] = (byte) 126;
    sourceArray1[19] = (byte) 24;
    sourceArray1[7] = (byte) 106;
    sourceArray1[24] = (byte) 39;
    sourceArray1[34] = (byte) 224 /*0xE0*/;
    sourceArray1[15] = (byte) 167;
    sourceArray1[32 /*0x20*/] = (byte) 165;
    sourceArray1[12] = (byte) 83;
    sourceArray1[21] = (byte) 179;
    sourceArray1[14] = (byte) 22;
    sourceArray1[47] = (byte) 102;
    sourceArray1[37] = (byte) 162;
    sourceArray1[17] = (byte) 77;
    sourceArray1[18] = (byte) 114;
    sourceArray1[11] = (byte) 56;
    sourceArray1[43] = (byte) 82;
    sourceArray1[10] = (byte) 172;
    sourceArray1[8] = (byte) 171;
    sourceArray1[4] = (byte) 209;
    sourceArray1[1] = (byte) 8;
    sourceArray1[25] = (byte) 170;
    sourceArray1[26] = (byte) 179;
    sourceArray1[27] = (byte) 186;
    sourceArray1[28] = (byte) 211;
    sourceArray1[23] = (byte) 197;
    sourceArray1[5] = (byte) 199;
    sourceArray1[31 /*0x1F*/] = (byte) 53;
    sourceArray1[0] = (byte) 15;
    sourceArray1[44] = (byte) 161;
    sourceArray1[38] = (byte) 186;
    sourceArray1[35] = (byte) 98;
    sourceArray1[33] = (byte) 167;
    sourceArray1[13] = (byte) 24;
    sourceArray1[9] = (byte) 204;
    sourceArray1[39] = (byte) 209;
    sourceArray1[46] = (byte) 55;
    sourceArray1[41] = (byte) 104;
    sourceArray1[42] = (byte) 128 /*0x80*/;
    sourceArray1[20] = (byte) 180;
    sourceArray1[22] = (byte) 6;
    sourceArray1[45] = (byte) 218;
    sourceArray1[36] = (byte) 204;
    sourceArray1[6] = (byte) 72;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[27] = (byte) 166;
    sourceArray2[40] = (byte) 144 /*0x90*/;
    sourceArray2[1] = (byte) 149;
    sourceArray2[3] = (byte) 167;
    sourceArray2[33] = (byte) 248;
    sourceArray2[5] = (byte) 146;
    sourceArray2[44] = (byte) 50;
    sourceArray2[7] = (byte) 241;
    sourceArray2[35] = (byte) 73;
    sourceArray2[39] = (byte) 182;
    sourceArray2[47] = (byte) 72;
    sourceArray2[10] = (byte) 209;
    sourceArray2[9] = (byte) 65;
    sourceArray2[13] = (byte) 101;
    sourceArray2[26] = (byte) 249;
    sourceArray2[25] = (byte) 15;
    sourceArray2[16 /*0x10*/] = (byte) 126;
    sourceArray2[4] = (byte) 210;
    sourceArray2[18] = (byte) 193;
    sourceArray2[34] = (byte) 42;
    sourceArray2[20] = (byte) 65;
    sourceArray2[38] = (byte) 104;
    sourceArray2[32 /*0x20*/] = (byte) 109;
    sourceArray2[17] = (byte) 25;
    sourceArray2[24] = (byte) 191;
    sourceArray2[0] = (byte) 63 /*0x3F*/;
    sourceArray2[15] = (byte) 99;
    sourceArray2[37] = (byte) 102;
    sourceArray2[11] = (byte) 18;
    sourceArray2[29] = (byte) 127 /*0x7F*/;
    sourceArray2[30] = (byte) 100;
    sourceArray2[2] = (byte) 247;
    sourceArray2[43] = (byte) 165;
    sourceArray2[14] = (byte) 250;
    sourceArray2[45] = (byte) 20;
    sourceArray2[12] = (byte) 93;
    sourceArray2[36] = (byte) 136;
    sourceArray2[46] = (byte) 29;
    sourceArray2[19] = (byte) 65;
    sourceArray2[31 /*0x1F*/] = (byte) 10;
    sourceArray2[22] = (byte) 225;
    sourceArray2[41] = (byte) 188;
    sourceArray2[42] = (byte) 47;
    sourceArray2[8] = (byte) 98;
    sourceArray2[28] = (byte) 93;
    sourceArray2[6] = (byte) 50;
    sourceArray2[23] = (byte) 167;
    sourceArray2[21] = (byte) 186;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13360(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 245,
      (byte) 6,
      (byte) 35,
      (byte) 55,
      (byte) 201,
      (byte) 57,
      (byte) 65,
      (byte) 37,
      (byte) 125,
      (byte) 193,
      (byte) 78,
      (byte) 1,
      (byte) 122,
      (byte) 44,
      (byte) 232,
      (byte) 154,
      (byte) 72,
      (byte) 210,
      (byte) 147,
      (byte) 235,
      (byte) 206,
      (byte) 170,
      (byte) 190,
      (byte) 208 /*0xD0*/,
      (byte) 145,
      (byte) 68,
      (byte) 123,
      (byte) 168,
      (byte) 54,
      (byte) 84,
      (byte) 62,
      (byte) 47,
      (byte) 15,
      (byte) 34,
      (byte) 237,
      (byte) 64 /*0x40*/,
      (byte) 88,
      (byte) 168,
      (byte) 176 /*0xB0*/,
      (byte) 191,
      (byte) 196,
      (byte) 193,
      (byte) 159,
      (byte) 27,
      (byte) 141,
      (byte) 138,
      (byte) 177,
      (byte) 72
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 105,
      (byte) 46,
      (byte) 36,
      (byte) 206,
      (byte) 69,
      (byte) 197,
      (byte) 22,
      (byte) 60,
      (byte) 44,
      (byte) 114,
      (byte) 123,
      (byte) 198,
      (byte) 189,
      (byte) 5,
      (byte) 11,
      (byte) 67,
      (byte) 78,
      (byte) 175,
      (byte) 175,
      (byte) 129,
      (byte) 110,
      (byte) 66,
      (byte) 156,
      (byte) 114,
      (byte) 144 /*0x90*/,
      (byte) 187,
      (byte) 5,
      byte.MaxValue,
      (byte) 2,
      (byte) 113,
      (byte) 172,
      (byte) 120,
      (byte) 129,
      (byte) 247,
      (byte) 226,
      (byte) 222,
      (byte) 81,
      (byte) 167,
      (byte) 42,
      (byte) 231,
      (byte) 183,
      (byte) 132,
      (byte) 81,
      (byte) 86,
      (byte) 179,
      (byte) 136,
      (byte) 188,
      (byte) 65
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_13361()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[53];
      byte[] numArray2 = new byte[53]
      {
        (byte) 251,
        (byte) 137,
        (byte) 15,
        (byte) 74,
        (byte) 71,
        (byte) 82,
        (byte) 165,
        (byte) 133,
        (byte) 189,
        (byte) 152,
        (byte) 238,
        (byte) 40,
        (byte) 131,
        (byte) 31 /*0x1F*/,
        (byte) 242,
        (byte) 88,
        (byte) 63 /*0x3F*/,
        (byte) 168,
        (byte) 66,
        (byte) 160 /*0xA0*/,
        (byte) 34,
        (byte) 211,
        (byte) 38,
        (byte) 13,
        (byte) 10,
        (byte) 163,
        (byte) 66,
        (byte) 7,
        (byte) 146,
        (byte) 51,
        (byte) 50,
        (byte) 161,
        (byte) 64 /*0x40*/,
        (byte) 252,
        (byte) 51,
        (byte) 133,
        (byte) 18,
        (byte) 162,
        (byte) 248,
        (byte) 6,
        (byte) 2,
        (byte) 43,
        (byte) 2,
        (byte) 43,
        (byte) 211,
        (byte) 119,
        (byte) 151,
        (byte) 110,
        (byte) 152,
        (byte) 89,
        (byte) 9,
        (byte) 205,
        (byte) 83
      };
      byte[] numArray3 = new byte[53]
      {
        (byte) 135,
        (byte) 159,
        (byte) 8,
        (byte) 99,
        (byte) 116,
        (byte) 158,
        (byte) 100,
        (byte) 75,
        (byte) 197,
        (byte) 79,
        (byte) 96 /*0x60*/,
        (byte) 214,
        (byte) 49,
        (byte) 41,
        (byte) 5,
        (byte) 229,
        (byte) 48 /*0x30*/,
        (byte) 162,
        (byte) 138,
        (byte) 41,
        (byte) 225,
        (byte) 8,
        (byte) 91,
        (byte) 94,
        (byte) 44,
        (byte) 88,
        (byte) 219,
        (byte) 126,
        (byte) 192 /*0xC0*/,
        (byte) 1,
        (byte) 33,
        (byte) 224 /*0xE0*/,
        (byte) 250,
        (byte) 46,
        (byte) 51,
        (byte) 4,
        (byte) 216,
        (byte) 58,
        (byte) 216,
        (byte) 216,
        (byte) 125,
        (byte) 126,
        (byte) 232,
        (byte) 243,
        (byte) 207,
        (byte) 150,
        (byte) 111,
        (byte) 20,
        (byte) 174,
        (byte) 211,
        (byte) 44,
        (byte) 102,
        (byte) 106
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 53);
      for (int index = 0; index < 53; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[53];
    byte[] numArray5 = new byte[53];
    numArray5[37] = (byte) 84;
    numArray5[1] = (byte) 2;
    numArray5[36] = (byte) 98;
    numArray5[3] = (byte) 164;
    numArray5[39] = (byte) 99;
    numArray5[5] = (byte) 158;
    numArray5[6] = (byte) 242;
    numArray5[4] = (byte) 15;
    numArray5[9] = (byte) 76;
    numArray5[32 /*0x20*/] = (byte) 221;
    numArray5[10] = (byte) 232;
    numArray5[40] = (byte) 57;
    numArray5[24] = (byte) 11;
    numArray5[13] = (byte) 108;
    numArray5[45] = (byte) 16 /*0x10*/;
    numArray5[15] = (byte) 156;
    numArray5[43] = (byte) 134;
    numArray5[17] = (byte) 60;
    numArray5[14] = (byte) 69;
    numArray5[30] = (byte) 78;
    numArray5[20] = (byte) 208 /*0xD0*/;
    numArray5[7] = (byte) 137;
    numArray5[49] = (byte) 108;
    numArray5[23] = (byte) 147;
    numArray5[42] = (byte) 197;
    numArray5[26] = (byte) 111;
    numArray5[0] = (byte) 98;
    numArray5[2] = (byte) 63 /*0x3F*/;
    numArray5[16 /*0x10*/] = (byte) 175;
    numArray5[27] = (byte) 57;
    numArray5[50] = (byte) 29;
    numArray5[31 /*0x1F*/] = (byte) 203;
    numArray5[48 /*0x30*/] = (byte) 19;
    numArray5[33] = (byte) 11;
    numArray5[21] = (byte) 251;
    numArray5[22] = (byte) 57;
    numArray5[47] = (byte) 43;
    numArray5[41] = (byte) 120;
    numArray5[38] = (byte) 246;
    numArray5[8] = (byte) 101;
    numArray5[19] = (byte) 247;
    numArray5[51] = (byte) 22;
    numArray5[29] = (byte) 101;
    numArray5[11] = (byte) 134;
    numArray5[44] = (byte) 143;
    numArray5[52] = (byte) 213;
    numArray5[46] = (byte) 131;
    numArray5[28] = (byte) 41;
    numArray5[12] = (byte) 235;
    numArray5[18] = (byte) 158;
    numArray5[34] = (byte) 34;
    numArray5[25] = (byte) 212;
    numArray5[35] = (byte) 16 /*0x10*/;
    byte[] numArray6 = new byte[53]
    {
      (byte) 244,
      (byte) 32 /*0x20*/,
      (byte) 254,
      (byte) 178,
      (byte) 15,
      (byte) 82,
      (byte) 47,
      (byte) 104,
      (byte) 111,
      (byte) 160 /*0xA0*/,
      (byte) 42,
      (byte) 215,
      (byte) 32 /*0x20*/,
      (byte) 199,
      (byte) 42,
      (byte) 176 /*0xB0*/,
      (byte) 33,
      (byte) 72,
      (byte) 135,
      (byte) 224 /*0xE0*/,
      (byte) 68,
      (byte) 228,
      (byte) 40,
      (byte) 113,
      (byte) 133,
      (byte) 141,
      (byte) 151,
      (byte) 70,
      (byte) 40,
      (byte) 56,
      (byte) 86,
      (byte) 222,
      (byte) 184,
      (byte) 207,
      (byte) 183,
      (byte) 186,
      (byte) 176 /*0xB0*/,
      (byte) 225,
      (byte) 92,
      (byte) 132,
      (byte) 0,
      (byte) 158,
      (byte) 231,
      (byte) 184,
      (byte) 149,
      (byte) 119,
      (byte) 204,
      (byte) 39,
      (byte) 24,
      (byte) 3,
      (byte) 181,
      (byte) 175,
      (byte) 125
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 53);
    for (int index = 0; index < 53; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static int ssp_appserver_13362(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 20,
      (byte) 168,
      (byte) 157,
      (byte) 234,
      (byte) 235,
      (byte) 147,
      (byte) 162,
      (byte) 184,
      (byte) 198,
      (byte) 38,
      (byte) 243,
      (byte) 42,
      (byte) 143,
      (byte) 236,
      (byte) 131,
      (byte) 159,
      (byte) 137,
      (byte) 7,
      (byte) 61,
      (byte) 63 /*0x3F*/,
      (byte) 112 /*0x70*/,
      (byte) 42,
      (byte) 144 /*0x90*/,
      (byte) 147,
      (byte) 172,
      (byte) 98,
      (byte) 70,
      (byte) 139,
      (byte) 134,
      (byte) 101,
      (byte) 90,
      (byte) 75,
      (byte) 181,
      (byte) 137,
      (byte) 51,
      (byte) 13,
      (byte) 30,
      (byte) 209,
      (byte) 71,
      (byte) 237,
      (byte) 118,
      (byte) 13,
      (byte) 193,
      (byte) 76,
      (byte) 115,
      (byte) 121,
      (byte) 81,
      (byte) 243
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[3] = (byte) 1;
    sourceArray2[38] = (byte) 10;
    sourceArray2[35] = (byte) 194;
    sourceArray2[30] = (byte) 151;
    sourceArray2[4] = (byte) 93;
    sourceArray2[5] = (byte) 132;
    sourceArray2[10] = (byte) 66;
    sourceArray2[2] = (byte) 75;
    sourceArray2[6] = (byte) 152;
    sourceArray2[9] = (byte) 97;
    sourceArray2[17] = (byte) 98;
    sourceArray2[11] = (byte) 74;
    sourceArray2[31 /*0x1F*/] = (byte) 139;
    sourceArray2[13] = (byte) 84;
    sourceArray2[47] = (byte) 140;
    sourceArray2[15] = (byte) 149;
    sourceArray2[16 /*0x10*/] = (byte) 100;
    sourceArray2[14] = (byte) 24;
    sourceArray2[18] = (byte) 170;
    sourceArray2[19] = (byte) 179;
    sourceArray2[45] = (byte) 8;
    sourceArray2[21] = (byte) 177;
    sourceArray2[22] = (byte) 193;
    sourceArray2[8] = (byte) 246;
    sourceArray2[24] = (byte) 219;
    sourceArray2[25] = (byte) 199;
    sourceArray2[37] = (byte) 248;
    sourceArray2[39] = (byte) 109;
    sourceArray2[28] = (byte) 120;
    sourceArray2[43] = (byte) 42;
    sourceArray2[36] = (byte) 116;
    sourceArray2[20] = (byte) 115;
    sourceArray2[32 /*0x20*/] = (byte) 12;
    sourceArray2[33] = (byte) 15;
    sourceArray2[26] = (byte) 145;
    sourceArray2[34] = (byte) 29;
    sourceArray2[12] = (byte) 224 /*0xE0*/;
    sourceArray2[0] = (byte) 107;
    sourceArray2[23] = (byte) 166;
    sourceArray2[27] = (byte) 59;
    sourceArray2[40] = (byte) 130;
    sourceArray2[1] = (byte) 251;
    sourceArray2[42] = (byte) 71;
    sourceArray2[7] = (byte) 228;
    sourceArray2[44] = (byte) 124;
    sourceArray2[41] = (byte) 177;
    sourceArray2[46] = (byte) 106;
    sourceArray2[29] = (byte) 44;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13363(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 75,
      (byte) 58,
      (byte) 57,
      (byte) 160 /*0xA0*/,
      (byte) 144 /*0x90*/,
      (byte) 211,
      (byte) 219,
      (byte) 246,
      (byte) 94,
      (byte) 129,
      (byte) 212,
      (byte) 233,
      (byte) 11,
      (byte) 119,
      (byte) 249,
      (byte) 59,
      (byte) 205,
      (byte) 67,
      (byte) 4,
      (byte) 135,
      (byte) 126,
      (byte) 61,
      (byte) 111,
      (byte) 128 /*0x80*/,
      (byte) 109,
      (byte) 36,
      (byte) 1,
      (byte) 176 /*0xB0*/,
      (byte) 106,
      (byte) 135,
      (byte) 8,
      (byte) 194,
      (byte) 84,
      (byte) 46,
      (byte) 197,
      (byte) 24,
      (byte) 162,
      (byte) 196,
      (byte) 140,
      (byte) 243,
      (byte) 68,
      byte.MaxValue,
      (byte) 106,
      (byte) 200,
      (byte) 32 /*0x20*/,
      (byte) 42,
      (byte) 136,
      (byte) 71
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 120,
      (byte) 117,
      (byte) 176 /*0xB0*/,
      (byte) 177,
      (byte) 189,
      (byte) 56,
      (byte) 178,
      (byte) 147,
      (byte) 1,
      (byte) 99,
      (byte) 231,
      (byte) 110,
      (byte) 133,
      (byte) 124,
      (byte) 108,
      (byte) 85,
      (byte) 245,
      (byte) 97,
      (byte) 156,
      (byte) 245,
      (byte) 89,
      (byte) 22,
      (byte) 76,
      (byte) 61,
      (byte) 18,
      (byte) 52,
      (byte) 60,
      (byte) 12,
      (byte) 173,
      (byte) 102,
      (byte) 79,
      (byte) 233,
      (byte) 21,
      (byte) 169,
      (byte) 217,
      (byte) 230,
      (byte) 178,
      (byte) 83,
      (byte) 249,
      (byte) 201,
      (byte) 245,
      (byte) 244,
      (byte) 103,
      (byte) 193,
      (byte) 205,
      (byte) 72,
      (byte) 137,
      (byte) 241
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13364(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 68,
      (byte) 133,
      (byte) 14,
      (byte) 203,
      (byte) 184,
      (byte) 51,
      (byte) 108,
      (byte) 210,
      (byte) 42,
      (byte) 140,
      (byte) 214,
      (byte) 121,
      (byte) 18,
      (byte) 149,
      (byte) 220,
      (byte) 177,
      (byte) 225,
      (byte) 152,
      (byte) 9,
      (byte) 182,
      (byte) 16 /*0x10*/,
      (byte) 66,
      (byte) 127 /*0x7F*/,
      (byte) 210,
      (byte) 166,
      (byte) 95,
      (byte) 66,
      (byte) 155,
      (byte) 185,
      (byte) 10,
      (byte) 70,
      (byte) 121,
      (byte) 234,
      (byte) 69,
      (byte) 15,
      (byte) 168,
      (byte) 183,
      (byte) 76,
      (byte) 233,
      (byte) 145,
      (byte) 32 /*0x20*/,
      (byte) 32 /*0x20*/,
      (byte) 26,
      (byte) 115,
      (byte) 147,
      (byte) 79,
      (byte) 14,
      (byte) 149
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[15] = (byte) 153;
    sourceArray2[31 /*0x1F*/] = (byte) 191;
    sourceArray2[2] = (byte) 174;
    sourceArray2[3] = (byte) 138;
    sourceArray2[4] = (byte) 36;
    sourceArray2[5] = (byte) 5;
    sourceArray2[6] = (byte) 241;
    sourceArray2[22] = (byte) 237;
    sourceArray2[34] = (byte) 251;
    sourceArray2[12] = (byte) 135;
    sourceArray2[10] = (byte) 14;
    sourceArray2[11] = (byte) 206;
    sourceArray2[9] = (byte) 237;
    sourceArray2[13] = (byte) 216;
    sourceArray2[7] = (byte) 45;
    sourceArray2[41] = (byte) 67;
    sourceArray2[8] = (byte) 218;
    sourceArray2[43] = (byte) 238;
    sourceArray2[1] = (byte) 203;
    sourceArray2[19] = (byte) 182;
    sourceArray2[20] = (byte) 178;
    sourceArray2[21] = (byte) 205;
    sourceArray2[27] = (byte) 110;
    sourceArray2[32 /*0x20*/] = (byte) 30;
    sourceArray2[18] = (byte) 132;
    sourceArray2[17] = (byte) 250;
    sourceArray2[25] = (byte) 217;
    sourceArray2[29] = (byte) 42;
    sourceArray2[28] = (byte) 131;
    sourceArray2[37] = (byte) 121;
    sourceArray2[30] = (byte) 56;
    sourceArray2[35] = (byte) 194;
    sourceArray2[40] = (byte) 41;
    sourceArray2[33] = (byte) 213;
    sourceArray2[16 /*0x10*/] = (byte) 46;
    sourceArray2[42] = (byte) 91;
    sourceArray2[36] = (byte) 85;
    sourceArray2[23] = (byte) 88;
    sourceArray2[38] = (byte) 58;
    sourceArray2[24] = (byte) 201;
    sourceArray2[0] = (byte) 84;
    sourceArray2[26] = (byte) 241;
    sourceArray2[44] = (byte) 35;
    sourceArray2[39] = (byte) 9;
    sourceArray2[14] = (byte) 128 /*0x80*/;
    sourceArray2[45] = (byte) 61;
    sourceArray2[46] = (byte) 58;
    sourceArray2[47] = (byte) 93;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13365(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 64 /*0x40*/,
      (byte) 15,
      (byte) 119,
      (byte) 155,
      (byte) 48 /*0x30*/,
      (byte) 219,
      (byte) 140,
      (byte) 208 /*0xD0*/,
      (byte) 123,
      (byte) 217,
      (byte) 250,
      (byte) 252,
      (byte) 204,
      (byte) 7,
      (byte) 67,
      (byte) 223,
      (byte) 244,
      (byte) 219,
      (byte) 137,
      (byte) 232,
      (byte) 10,
      (byte) 244,
      (byte) 119,
      (byte) 146,
      (byte) 125,
      (byte) 107,
      (byte) 30,
      (byte) 215,
      (byte) 70,
      (byte) 232,
      (byte) 47,
      (byte) 222,
      (byte) 8,
      (byte) 151,
      (byte) 16 /*0x10*/,
      (byte) 157,
      (byte) 121,
      (byte) 136,
      (byte) 20,
      (byte) 224 /*0xE0*/,
      (byte) 232,
      (byte) 179,
      (byte) 93,
      (byte) 53,
      (byte) 167,
      (byte) 127 /*0x7F*/,
      (byte) 91,
      (byte) 136
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[44] = (byte) 138;
    sourceArray2[34] = (byte) 147;
    sourceArray2[9] = (byte) 195;
    sourceArray2[42] = (byte) 68;
    sourceArray2[4] = (byte) 80 /*0x50*/;
    sourceArray2[17] = (byte) 254;
    sourceArray2[6] = (byte) 2;
    sourceArray2[13] = (byte) 80 /*0x50*/;
    sourceArray2[0] = (byte) 218;
    sourceArray2[21] = (byte) 73;
    sourceArray2[10] = (byte) 154;
    sourceArray2[11] = (byte) 213;
    sourceArray2[5] = (byte) 50;
    sourceArray2[27] = (byte) 169;
    sourceArray2[22] = (byte) 89;
    sourceArray2[15] = (byte) 155;
    sourceArray2[16 /*0x10*/] = (byte) 79;
    sourceArray2[7] = (byte) 169;
    sourceArray2[3] = (byte) 121;
    sourceArray2[19] = (byte) 148;
    sourceArray2[20] = (byte) 162;
    sourceArray2[2] = (byte) 29;
    sourceArray2[46] = (byte) 82;
    sourceArray2[23] = (byte) 119;
    sourceArray2[24] = (byte) 134;
    sourceArray2[41] = (byte) 54;
    sourceArray2[26] = (byte) 183;
    sourceArray2[33] = (byte) 239;
    sourceArray2[28] = (byte) 99;
    sourceArray2[12] = (byte) 251;
    sourceArray2[30] = (byte) 253;
    sourceArray2[18] = (byte) 117;
    sourceArray2[32 /*0x20*/] = (byte) 233;
    sourceArray2[25] = (byte) 29;
    sourceArray2[1] = (byte) 77;
    sourceArray2[35] = (byte) 202;
    sourceArray2[36] = (byte) 201;
    sourceArray2[37] = (byte) 132;
    sourceArray2[31 /*0x1F*/] = (byte) 228;
    sourceArray2[8] = (byte) 186;
    sourceArray2[40] = (byte) 177;
    sourceArray2[38] = (byte) 238;
    sourceArray2[14] = (byte) 12;
    sourceArray2[39] = (byte) 7;
    sourceArray2[43] = (byte) 100;
    sourceArray2[45] = (byte) 79;
    sourceArray2[29] = (byte) 220;
    sourceArray2[47] = (byte) 173;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13366(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 38,
      (byte) 145,
      (byte) 183,
      (byte) 9,
      (byte) 167,
      (byte) 87,
      (byte) 250,
      (byte) 77,
      (byte) 95,
      (byte) 181,
      (byte) 86,
      (byte) 250,
      (byte) 31 /*0x1F*/,
      (byte) 180,
      (byte) 40,
      (byte) 57,
      (byte) 178,
      (byte) 189,
      (byte) 152,
      (byte) 226,
      (byte) 57,
      (byte) 83,
      (byte) 51,
      (byte) 159,
      (byte) 210,
      (byte) 1,
      (byte) 118,
      (byte) 134,
      (byte) 205,
      (byte) 44,
      (byte) 205,
      (byte) 182,
      (byte) 96 /*0x60*/,
      (byte) 115,
      (byte) 233,
      (byte) 3,
      (byte) 194,
      (byte) 205,
      (byte) 46,
      (byte) 186,
      (byte) 63 /*0x3F*/,
      (byte) 109,
      (byte) 130,
      (byte) 14,
      (byte) 175,
      (byte) 153,
      (byte) 74,
      (byte) 156
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[44] = (byte) 5;
    sourceArray2[23] = (byte) 8;
    sourceArray2[5] = (byte) 112 /*0x70*/;
    sourceArray2[33] = (byte) 32 /*0x20*/;
    sourceArray2[25] = (byte) 59;
    sourceArray2[1] = (byte) 179;
    sourceArray2[6] = (byte) 130;
    sourceArray2[7] = (byte) 216;
    sourceArray2[8] = (byte) 2;
    sourceArray2[32 /*0x20*/] = (byte) 89;
    sourceArray2[10] = (byte) 80 /*0x50*/;
    sourceArray2[24] = (byte) 232;
    sourceArray2[12] = (byte) 106;
    sourceArray2[22] = (byte) 219;
    sourceArray2[35] = (byte) 63 /*0x3F*/;
    sourceArray2[11] = (byte) 76;
    sourceArray2[34] = (byte) 175;
    sourceArray2[17] = (byte) 8;
    sourceArray2[13] = (byte) 61;
    sourceArray2[19] = (byte) 226;
    sourceArray2[39] = (byte) 102;
    sourceArray2[21] = (byte) 90;
    sourceArray2[14] = (byte) 192 /*0xC0*/;
    sourceArray2[41] = (byte) 164;
    sourceArray2[16 /*0x10*/] = (byte) 205;
    sourceArray2[38] = (byte) 83;
    sourceArray2[26] = (byte) 14;
    sourceArray2[27] = (byte) 141;
    sourceArray2[28] = (byte) 221;
    sourceArray2[29] = (byte) 215;
    sourceArray2[30] = (byte) 0;
    sourceArray2[31 /*0x1F*/] = (byte) 213;
    sourceArray2[37] = (byte) 59;
    sourceArray2[20] = (byte) 173;
    sourceArray2[40] = (byte) 251;
    sourceArray2[9] = (byte) 225;
    sourceArray2[36] = (byte) 76;
    sourceArray2[3] = (byte) 149;
    sourceArray2[2] = (byte) 135;
    sourceArray2[42] = (byte) 20;
    sourceArray2[0] = (byte) 73;
    sourceArray2[15] = (byte) 245;
    sourceArray2[4] = byte.MaxValue;
    sourceArray2[43] = (byte) 55;
    sourceArray2[18] = (byte) 193;
    sourceArray2[45] = (byte) 73;
    sourceArray2[46] = (byte) 38;
    sourceArray2[47] = (byte) 105;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[28];
    byte[] response2 = new byte[28];
    Array.Copy((Array) sc_13302.sspq, 158, (Array) numArray2, 0, 28);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13302.sspr, 158, (Array) numArray2, 0, 28);
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

  internal static int ssp_appserver_13367(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[41] = (byte) 47;
    sourceArray1[1] = (byte) 129;
    sourceArray1[2] = (byte) 253;
    sourceArray1[3] = (byte) 223;
    sourceArray1[0] = (byte) 223;
    sourceArray1[5] = (byte) 25;
    sourceArray1[36] = (byte) 59;
    sourceArray1[7] = (byte) 183;
    sourceArray1[26] = (byte) 18;
    sourceArray1[47] = (byte) 239;
    sourceArray1[19] = (byte) 210;
    sourceArray1[11] = (byte) 175;
    sourceArray1[16 /*0x10*/] = (byte) 129;
    sourceArray1[46] = (byte) 45;
    sourceArray1[14] = (byte) 20;
    sourceArray1[15] = (byte) 108;
    sourceArray1[34] = (byte) 166;
    sourceArray1[18] = (byte) 154;
    sourceArray1[37] = (byte) 73;
    sourceArray1[10] = (byte) 68;
    sourceArray1[20] = (byte) 228;
    sourceArray1[21] = (byte) 64 /*0x40*/;
    sourceArray1[27] = (byte) 28;
    sourceArray1[23] = (byte) 0;
    sourceArray1[31 /*0x1F*/] = (byte) 118;
    sourceArray1[12] = (byte) 180;
    sourceArray1[43] = (byte) 94;
    sourceArray1[44] = (byte) 226;
    sourceArray1[28] = (byte) 86;
    sourceArray1[17] = (byte) 153;
    sourceArray1[30] = (byte) 70;
    sourceArray1[13] = (byte) 156;
    sourceArray1[25] = (byte) 29;
    sourceArray1[33] = (byte) 43;
    sourceArray1[8] = (byte) 81;
    sourceArray1[32 /*0x20*/] = (byte) 15;
    sourceArray1[35] = (byte) 128 /*0x80*/;
    sourceArray1[6] = (byte) 8;
    sourceArray1[38] = (byte) 195;
    sourceArray1[39] = (byte) 212;
    sourceArray1[24] = (byte) 52;
    sourceArray1[22] = (byte) 208 /*0xD0*/;
    sourceArray1[42] = (byte) 91;
    sourceArray1[40] = (byte) 253;
    sourceArray1[9] = (byte) 32 /*0x20*/;
    sourceArray1[45] = (byte) 44;
    sourceArray1[4] = (byte) 57;
    sourceArray1[29] = (byte) 181;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 81,
      (byte) 83,
      (byte) 244,
      (byte) 8,
      (byte) 9,
      (byte) 64 /*0x40*/,
      (byte) 55,
      (byte) 226,
      (byte) 107,
      (byte) 177,
      (byte) 86,
      (byte) 226,
      (byte) 1,
      (byte) 71,
      (byte) 206,
      (byte) 148,
      (byte) 3,
      (byte) 14,
      (byte) 72,
      (byte) 14,
      (byte) 149,
      (byte) 196,
      (byte) 177,
      (byte) 52,
      (byte) 198,
      (byte) 97,
      (byte) 217,
      (byte) 37,
      (byte) 75,
      (byte) 89,
      (byte) 213,
      (byte) 195,
      (byte) 235,
      (byte) 136,
      (byte) 188,
      (byte) 192 /*0xC0*/,
      (byte) 246,
      (byte) 106,
      (byte) 93,
      (byte) 21,
      (byte) 39,
      (byte) 0,
      (byte) 47,
      (byte) 235,
      (byte) 163,
      (byte) 48 /*0x30*/,
      (byte) 125,
      (byte) 56
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[42];
    byte[] response2 = new byte[42];
    Array.Copy((Array) sc_13302.sspq, 186, (Array) numArray2, 0, 42);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13302.sspr, 186, (Array) numArray2, 0, 42);
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

  internal static int ssp_appserver_13368(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[0] = (byte) 78;
    sourceArray1[16 /*0x10*/] = (byte) 121;
    sourceArray1[22] = (byte) 80 /*0x50*/;
    sourceArray1[45] = (byte) 178;
    sourceArray1[4] = (byte) 121;
    sourceArray1[15] = (byte) 235;
    sourceArray1[6] = (byte) 159;
    sourceArray1[11] = (byte) 201;
    sourceArray1[46] = (byte) 245;
    sourceArray1[9] = (byte) 62;
    sourceArray1[10] = (byte) 82;
    sourceArray1[21] = (byte) 147;
    sourceArray1[5] = (byte) 234;
    sourceArray1[24] = (byte) 38;
    sourceArray1[14] = (byte) 72;
    sourceArray1[3] = (byte) 252;
    sourceArray1[25] = (byte) 227;
    sourceArray1[27] = (byte) 2;
    sourceArray1[7] = (byte) 176 /*0xB0*/;
    sourceArray1[13] = (byte) 10;
    sourceArray1[20] = (byte) 88;
    sourceArray1[12] = (byte) 84;
    sourceArray1[41] = (byte) 54;
    sourceArray1[26] = (byte) 184;
    sourceArray1[40] = (byte) 35;
    sourceArray1[29] = (byte) 216;
    sourceArray1[19] = (byte) 154;
    sourceArray1[2] = (byte) 94;
    sourceArray1[23] = (byte) 213;
    sourceArray1[1] = (byte) 145;
    sourceArray1[30] = (byte) 144 /*0x90*/;
    sourceArray1[31 /*0x1F*/] = (byte) 92;
    sourceArray1[17] = (byte) 173;
    sourceArray1[33] = (byte) 89;
    sourceArray1[34] = (byte) 159;
    sourceArray1[35] = (byte) 50;
    sourceArray1[36] = (byte) 149;
    sourceArray1[37] = (byte) 156;
    sourceArray1[38] = (byte) 181;
    sourceArray1[28] = (byte) 185;
    sourceArray1[8] = (byte) 143;
    sourceArray1[39] = (byte) 12;
    sourceArray1[42] = (byte) 149;
    sourceArray1[43] = (byte) 175;
    sourceArray1[44] = (byte) 182;
    sourceArray1[18] = (byte) 173;
    sourceArray1[32 /*0x20*/] = (byte) 68;
    sourceArray1[47] = (byte) 32 /*0x20*/;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 110,
      (byte) 26,
      (byte) 45,
      (byte) 241,
      (byte) 212,
      (byte) 47,
      (byte) 248,
      (byte) 59,
      (byte) 138,
      (byte) 249,
      (byte) 156,
      (byte) 185,
      (byte) 149,
      (byte) 176 /*0xB0*/,
      (byte) 35,
      (byte) 131,
      (byte) 247,
      (byte) 140,
      (byte) 12,
      (byte) 58,
      (byte) 44,
      (byte) 27,
      (byte) 81,
      (byte) 147,
      (byte) 54,
      (byte) 48 /*0x30*/,
      (byte) 3,
      (byte) 66,
      (byte) 135,
      (byte) 236,
      (byte) 203,
      (byte) 210,
      (byte) 133,
      (byte) 210,
      (byte) 10,
      (byte) 63 /*0x3F*/,
      (byte) 156,
      (byte) 96 /*0x60*/,
      (byte) 214,
      (byte) 240 /*0xF0*/,
      (byte) 229,
      (byte) 83,
      (byte) 106,
      (byte) 56,
      (byte) 212,
      (byte) 46,
      (byte) 198,
      (byte) 13
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13369(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 250,
      (byte) 180,
      (byte) 15,
      (byte) 34,
      (byte) 2,
      (byte) 16 /*0x10*/,
      (byte) 23,
      (byte) 109,
      (byte) 124,
      (byte) 188,
      (byte) 159,
      (byte) 105,
      (byte) 98,
      (byte) 168,
      (byte) 153,
      (byte) 192 /*0xC0*/,
      (byte) 182,
      (byte) 189,
      (byte) 38,
      (byte) 82,
      (byte) 36,
      (byte) 65,
      (byte) 134,
      (byte) 37,
      (byte) 72,
      (byte) 21,
      (byte) 216,
      (byte) 198,
      (byte) 241,
      (byte) 227,
      (byte) 32 /*0x20*/,
      (byte) 161,
      (byte) 94,
      (byte) 70,
      (byte) 242,
      (byte) 227,
      (byte) 204,
      (byte) 31 /*0x1F*/,
      (byte) 246,
      (byte) 119,
      (byte) 140,
      (byte) 243,
      (byte) 60,
      (byte) 40,
      (byte) 81,
      (byte) 163,
      (byte) 100,
      (byte) 119
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 47,
      (byte) 130,
      (byte) 66,
      (byte) 5,
      (byte) 83,
      (byte) 111,
      (byte) 237,
      (byte) 106,
      (byte) 163,
      (byte) 38,
      (byte) 43,
      (byte) 3,
      (byte) 62,
      (byte) 14,
      (byte) 190,
      (byte) 135,
      (byte) 157,
      (byte) 248,
      (byte) 245,
      (byte) 197,
      (byte) 90,
      (byte) 208 /*0xD0*/,
      (byte) 14,
      (byte) 2,
      (byte) 15,
      (byte) 78,
      (byte) 195,
      (byte) 69,
      (byte) 102,
      (byte) 240 /*0xF0*/,
      (byte) 238,
      (byte) 37,
      (byte) 83,
      (byte) 233,
      (byte) 147,
      (byte) 102,
      (byte) 30,
      (byte) 205,
      (byte) 209,
      (byte) 62,
      (byte) 226,
      (byte) 131,
      (byte) 40,
      (byte) 49,
      (byte) 85,
      (byte) 65,
      (byte) 209,
      (byte) 54
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13370(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[13] = (byte) 11;
    sourceArray1[1] = (byte) 8;
    sourceArray1[21] = (byte) 197;
    sourceArray1[3] = (byte) 45;
    sourceArray1[8] = (byte) 80 /*0x50*/;
    sourceArray1[5] = (byte) 217;
    sourceArray1[46] = (byte) 83;
    sourceArray1[30] = (byte) 44;
    sourceArray1[0] = (byte) 110;
    sourceArray1[9] = (byte) 124;
    sourceArray1[17] = (byte) 244;
    sourceArray1[11] = (byte) 156;
    sourceArray1[12] = (byte) 254;
    sourceArray1[31 /*0x1F*/] = (byte) 91;
    sourceArray1[41] = (byte) 64 /*0x40*/;
    sourceArray1[22] = (byte) 49;
    sourceArray1[16 /*0x10*/] = (byte) 182;
    sourceArray1[34] = (byte) 240 /*0xF0*/;
    sourceArray1[18] = (byte) 150;
    sourceArray1[19] = (byte) 199;
    sourceArray1[20] = (byte) 175;
    sourceArray1[26] = (byte) 164;
    sourceArray1[25] = (byte) 211;
    sourceArray1[23] = (byte) 4;
    sourceArray1[24] = (byte) 94;
    sourceArray1[10] = (byte) 153;
    sourceArray1[2] = (byte) 185;
    sourceArray1[47] = (byte) 152;
    sourceArray1[29] = (byte) 134;
    sourceArray1[44] = (byte) 163;
    sourceArray1[28] = (byte) 97;
    sourceArray1[27] = (byte) 177;
    sourceArray1[38] = (byte) 226;
    sourceArray1[33] = (byte) 199;
    sourceArray1[7] = (byte) 55;
    sourceArray1[35] = (byte) 61;
    sourceArray1[36] = (byte) 56;
    sourceArray1[37] = (byte) 181;
    sourceArray1[4] = (byte) 234;
    sourceArray1[39] = (byte) 69;
    sourceArray1[6] = (byte) 237;
    sourceArray1[14] = (byte) 28;
    sourceArray1[42] = (byte) 60;
    sourceArray1[43] = (byte) 119;
    sourceArray1[40] = (byte) 9;
    sourceArray1[45] = (byte) 139;
    sourceArray1[32 /*0x20*/] = byte.MaxValue;
    sourceArray1[15] = (byte) 181;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 47,
      (byte) 193,
      (byte) 37,
      (byte) 205,
      (byte) 45,
      (byte) 193,
      (byte) 186,
      (byte) 225,
      (byte) 162,
      (byte) 44,
      (byte) 167,
      (byte) 64 /*0x40*/,
      (byte) 30,
      (byte) 207,
      (byte) 225,
      (byte) 136,
      (byte) 37,
      (byte) 59,
      (byte) 234,
      (byte) 252,
      (byte) 231,
      (byte) 191,
      (byte) 176 /*0xB0*/,
      (byte) 194,
      (byte) 154,
      (byte) 108,
      (byte) 178,
      (byte) 242,
      (byte) 253,
      (byte) 90,
      (byte) 99,
      (byte) 167,
      (byte) 132,
      (byte) 218,
      (byte) 173,
      (byte) 160 /*0xA0*/,
      (byte) 193,
      (byte) 115,
      (byte) 63 /*0x3F*/,
      (byte) 69,
      (byte) 13,
      (byte) 101,
      (byte) 127 /*0x7F*/,
      (byte) 227,
      (byte) 4,
      (byte) 229,
      (byte) 253,
      (byte) 223
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13372(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 28,
      (byte) 125,
      (byte) 105,
      (byte) 68,
      (byte) 138,
      (byte) 191,
      (byte) 160 /*0xA0*/,
      (byte) 155,
      (byte) 187,
      (byte) 30,
      (byte) 28,
      (byte) 85,
      (byte) 252,
      (byte) 151,
      (byte) 39,
      (byte) 210,
      (byte) 53,
      (byte) 206,
      (byte) 245,
      (byte) 211,
      (byte) 247,
      (byte) 154,
      (byte) 211,
      (byte) 120,
      (byte) 250,
      (byte) 25,
      (byte) 69,
      (byte) 63 /*0x3F*/,
      (byte) 240 /*0xF0*/,
      (byte) 4,
      (byte) 24,
      (byte) 147,
      (byte) 249,
      (byte) 228,
      (byte) 99,
      (byte) 143,
      (byte) 0,
      (byte) 92,
      (byte) 113,
      (byte) 155,
      (byte) 201,
      (byte) 10,
      (byte) 225,
      (byte) 41,
      (byte) 73,
      (byte) 45,
      (byte) 234,
      (byte) 52
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 251,
      (byte) 169,
      (byte) 93,
      (byte) 90,
      (byte) 63 /*0x3F*/,
      (byte) 0,
      (byte) 62,
      (byte) 84,
      (byte) 57,
      (byte) 239,
      (byte) 127 /*0x7F*/,
      (byte) 127 /*0x7F*/,
      (byte) 244,
      (byte) 126,
      (byte) 180,
      (byte) 135,
      (byte) 34,
      (byte) 134,
      (byte) 240 /*0xF0*/,
      (byte) 115,
      (byte) 82,
      (byte) 148,
      (byte) 7,
      (byte) 192 /*0xC0*/,
      (byte) 12,
      (byte) 187,
      (byte) 83,
      (byte) 215,
      (byte) 133,
      (byte) 186,
      (byte) 39,
      (byte) 71,
      (byte) 214,
      (byte) 47,
      (byte) 219,
      (byte) 33,
      (byte) 217,
      (byte) 159,
      (byte) 137,
      (byte) 158,
      (byte) 164,
      (byte) 242,
      (byte) 175,
      (byte) 83,
      (byte) 0,
      (byte) 194,
      (byte) 148,
      (byte) 238
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[38];
    byte[] response2 = new byte[38];
    Array.Copy((Array) sc_13302.sspq, 228, (Array) numArray2, 0, 38);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13302.sspr, 228, (Array) numArray2, 0, 38);
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

  internal static string ssp_appserver_13373()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[120];
      byte[] numArray2 = new byte[55]
      {
        (byte) 15,
        (byte) 18,
        (byte) 0,
        (byte) 33,
        (byte) 104,
        (byte) 95,
        (byte) 43,
        (byte) 34,
        (byte) 191,
        (byte) 209,
        (byte) 210,
        (byte) 1,
        (byte) 80 /*0x50*/,
        (byte) 41,
        (byte) 92,
        (byte) 69,
        (byte) 216,
        (byte) 135,
        (byte) 9,
        (byte) 201,
        byte.MaxValue,
        (byte) 193,
        (byte) 124,
        (byte) 65,
        (byte) 104,
        (byte) 10,
        (byte) 180,
        (byte) 230,
        (byte) 144 /*0x90*/,
        (byte) 40,
        (byte) 244,
        (byte) 42,
        (byte) 81,
        (byte) 178,
        (byte) 194,
        (byte) 198,
        (byte) 92,
        (byte) 56,
        (byte) 162,
        (byte) 141,
        (byte) 141,
        (byte) 135,
        (byte) 111,
        (byte) 251,
        (byte) 93,
        (byte) 16 /*0x10*/,
        (byte) 5,
        (byte) 204,
        (byte) 115,
        (byte) 48 /*0x30*/,
        (byte) 136,
        (byte) 223,
        (byte) 227,
        (byte) 24,
        (byte) 229
      };
      byte[] numArray3 = new byte[55];
      numArray3[29] = (byte) 93;
      numArray3[37] = (byte) 77;
      numArray3[2] = (byte) 231;
      numArray3[5] = (byte) 137;
      numArray3[52] = (byte) 118;
      numArray3[9] = (byte) 149;
      numArray3[6] = (byte) 83;
      numArray3[7] = (byte) 38;
      numArray3[46] = (byte) 104;
      numArray3[40] = (byte) 249;
      numArray3[1] = (byte) 251;
      numArray3[11] = (byte) 203;
      numArray3[19] = (byte) 126;
      numArray3[21] = (byte) 27;
      numArray3[12] = (byte) 238;
      numArray3[15] = (byte) 216;
      numArray3[8] = (byte) 109;
      numArray3[17] = (byte) 210;
      numArray3[32 /*0x20*/] = (byte) 100;
      numArray3[42] = (byte) 231;
      numArray3[16 /*0x10*/] = (byte) 173;
      numArray3[47] = (byte) 142;
      numArray3[3] = (byte) 190;
      numArray3[23] = (byte) 226;
      numArray3[44] = (byte) 60;
      numArray3[25] = (byte) 202;
      numArray3[45] = (byte) 10;
      numArray3[22] = (byte) 250;
      numArray3[28] = (byte) 139;
      numArray3[24] = (byte) 99;
      numArray3[10] = (byte) 47;
      numArray3[20] = (byte) 225;
      numArray3[14] = (byte) 120;
      numArray3[33] = (byte) 146;
      numArray3[34] = (byte) 226;
      numArray3[35] = (byte) 119;
      numArray3[13] = (byte) 235;
      numArray3[43] = (byte) 123;
      numArray3[30] = (byte) 98;
      numArray3[39] = (byte) 58;
      numArray3[31 /*0x1F*/] = (byte) 204;
      numArray3[4] = (byte) 234;
      numArray3[36] = (byte) 3;
      numArray3[41] = (byte) 205;
      numArray3[0] = (byte) 115;
      numArray3[50] = (byte) 72;
      numArray3[26] = (byte) 204;
      numArray3[51] = (byte) 93;
      numArray3[48 /*0x30*/] = (byte) 237;
      numArray3[49] = (byte) 129;
      numArray3[27] = (byte) 233;
      numArray3[18] = (byte) 160 /*0xA0*/;
      numArray3[38] = (byte) 52;
      numArray3[53] = (byte) 251;
      numArray3[54] = (byte) 78;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 219,
        (byte) 95,
        (byte) 88,
        (byte) 194,
        (byte) 209,
        (byte) 49,
        (byte) 247,
        (byte) 199,
        (byte) 97,
        (byte) 117,
        (byte) 209,
        (byte) 241,
        (byte) 101,
        (byte) 76,
        (byte) 31 /*0x1F*/,
        (byte) 160 /*0xA0*/,
        (byte) 153,
        (byte) 242,
        (byte) 131,
        (byte) 121,
        (byte) 97,
        (byte) 46,
        (byte) 248,
        (byte) 157,
        (byte) 130,
        (byte) 148,
        (byte) 24,
        (byte) 182,
        (byte) 27,
        (byte) 223,
        (byte) 93,
        (byte) 96 /*0x60*/,
        (byte) 121,
        (byte) 27,
        (byte) 192 /*0xC0*/,
        (byte) 32 /*0x20*/,
        (byte) 236,
        (byte) 32 /*0x20*/,
        (byte) 119,
        (byte) 195,
        (byte) 225,
        (byte) 87,
        (byte) 84,
        (byte) 235,
        (byte) 150,
        (byte) 117,
        (byte) 121,
        (byte) 251,
        (byte) 201,
        (byte) 89,
        (byte) 205,
        (byte) 234,
        (byte) 232,
        (byte) 129,
        (byte) 179
      };
      byte[] numArray5 = new byte[55];
      numArray5[2] = (byte) 58;
      numArray5[1] = (byte) 143;
      numArray5[12] = (byte) 124;
      numArray5[3] = (byte) 60;
      numArray5[4] = (byte) 98;
      numArray5[39] = (byte) 234;
      numArray5[6] = (byte) 183;
      numArray5[23] = (byte) 56;
      numArray5[46] = (byte) 228;
      numArray5[9] = (byte) 242;
      numArray5[10] = (byte) 72;
      numArray5[25] = (byte) 121;
      numArray5[19] = (byte) 14;
      numArray5[13] = (byte) 40;
      numArray5[18] = (byte) 248;
      numArray5[15] = (byte) 15;
      numArray5[16 /*0x10*/] = (byte) 211;
      numArray5[5] = (byte) 206;
      numArray5[41] = (byte) 190;
      numArray5[42] = (byte) 167;
      numArray5[44] = (byte) 51;
      numArray5[35] = (byte) 232;
      numArray5[22] = (byte) 228;
      numArray5[20] = (byte) 49;
      numArray5[11] = (byte) 14;
      numArray5[17] = (byte) 81;
      numArray5[33] = (byte) 69;
      numArray5[27] = (byte) 29;
      numArray5[30] = (byte) 227;
      numArray5[29] = (byte) 13;
      numArray5[40] = (byte) 214;
      numArray5[31 /*0x1F*/] = (byte) 6;
      numArray5[28] = (byte) 9;
      numArray5[26] = (byte) 35;
      numArray5[48 /*0x30*/] = (byte) 114;
      numArray5[53] = (byte) 148;
      numArray5[36] = (byte) 138;
      numArray5[37] = (byte) 110;
      numArray5[7] = (byte) 166;
      numArray5[52] = (byte) 151;
      numArray5[14] = (byte) 140;
      numArray5[47] = (byte) 105;
      numArray5[8] = (byte) 97;
      numArray5[34] = (byte) 171;
      numArray5[38] = (byte) 185;
      numArray5[45] = (byte) 176 /*0xB0*/;
      numArray5[24] = (byte) 136;
      numArray5[50] = (byte) 21;
      numArray5[0] = (byte) 0;
      numArray5[49] = (byte) 168;
      numArray5[54] = (byte) 240 /*0xF0*/;
      numArray5[51] = (byte) 24;
      numArray5[21] = byte.MaxValue;
      numArray5[32 /*0x20*/] = (byte) 108;
      numArray5[43] = (byte) 125;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[10]
      {
        (byte) 179,
        (byte) 51,
        (byte) 33,
        (byte) 251,
        (byte) 82,
        (byte) 235,
        (byte) 112 /*0x70*/,
        (byte) 164,
        (byte) 249,
        (byte) 127 /*0x7F*/
      };
      byte[] numArray7 = new byte[10]
      {
        (byte) 14,
        (byte) 248,
        (byte) 197,
        (byte) 123,
        (byte) 82,
        (byte) 224 /*0xE0*/,
        (byte) 80 /*0x50*/,
        (byte) 207,
        (byte) 236,
        (byte) 127 /*0x7F*/
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index + 110] ^= numArray7[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray8 = new byte[120];
    byte[] numArray9 = new byte[55];
    numArray9[28] = (byte) 46;
    numArray9[1] = (byte) 153;
    numArray9[2] = (byte) 63 /*0x3F*/;
    numArray9[50] = (byte) 171;
    numArray9[43] = (byte) 36;
    numArray9[21] = (byte) 126;
    numArray9[6] = (byte) 8;
    numArray9[49] = (byte) 161;
    numArray9[47] = (byte) 207;
    numArray9[30] = (byte) 126;
    numArray9[40] = (byte) 201;
    numArray9[19] = (byte) 65;
    numArray9[12] = (byte) 36;
    numArray9[11] = (byte) 173;
    numArray9[14] = (byte) 188;
    numArray9[5] = (byte) 95;
    numArray9[16 /*0x10*/] = (byte) 78;
    numArray9[17] = (byte) 14;
    numArray9[18] = (byte) 191;
    numArray9[8] = (byte) 226;
    numArray9[20] = (byte) 93;
    numArray9[0] = (byte) 129;
    numArray9[22] = (byte) 172;
    numArray9[15] = (byte) 3;
    numArray9[36] = (byte) 47;
    numArray9[25] = (byte) 79;
    numArray9[26] = (byte) 191;
    numArray9[27] = (byte) 97;
    numArray9[7] = (byte) 57;
    numArray9[29] = (byte) 43;
    numArray9[48 /*0x30*/] = (byte) 179;
    numArray9[4] = (byte) 246;
    numArray9[32 /*0x20*/] = (byte) 248;
    numArray9[33] = (byte) 130;
    numArray9[10] = (byte) 235;
    numArray9[35] = (byte) 187;
    numArray9[13] = (byte) 27;
    numArray9[37] = (byte) 18;
    numArray9[24] = (byte) 175;
    numArray9[39] = (byte) 72;
    numArray9[23] = (byte) 25;
    numArray9[41] = (byte) 33;
    numArray9[42] = (byte) 208 /*0xD0*/;
    numArray9[9] = (byte) 15;
    numArray9[44] = (byte) 54;
    numArray9[3] = (byte) 21;
    numArray9[46] = (byte) 46;
    numArray9[31 /*0x1F*/] = (byte) 78;
    numArray9[38] = (byte) 243;
    numArray9[45] = (byte) 78;
    numArray9[52] = (byte) 33;
    numArray9[51] = (byte) 113;
    numArray9[54] = (byte) 232;
    numArray9[53] = (byte) 54;
    numArray9[34] = (byte) 35;
    byte[] numArray10 = new byte[55]
    {
      (byte) 185,
      (byte) 37,
      (byte) 77,
      (byte) 179,
      (byte) 76,
      (byte) 173,
      (byte) 2,
      (byte) 110,
      (byte) 236,
      (byte) 188,
      (byte) 169,
      (byte) 145,
      (byte) 68,
      (byte) 211,
      (byte) 239,
      (byte) 76,
      (byte) 192 /*0xC0*/,
      (byte) 34,
      (byte) 42,
      (byte) 126,
      (byte) 9,
      (byte) 109,
      (byte) 21,
      (byte) 121,
      (byte) 202,
      (byte) 144 /*0x90*/,
      (byte) 21,
      (byte) 84,
      (byte) 164,
      (byte) 102,
      (byte) 224 /*0xE0*/,
      (byte) 39,
      (byte) 52,
      (byte) 5,
      (byte) 243,
      (byte) 40,
      (byte) 29,
      (byte) 158,
      (byte) 190,
      (byte) 151,
      (byte) 140,
      (byte) 215,
      (byte) 211,
      (byte) 70,
      (byte) 149,
      (byte) 166,
      (byte) 184,
      (byte) 244,
      (byte) 202,
      (byte) 125,
      (byte) 15,
      (byte) 42,
      (byte) 243,
      (byte) 58,
      (byte) 120
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray8, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index] ^= numArray10[index];
    byte[] numArray11 = new byte[55]
    {
      (byte) 67,
      (byte) 5,
      (byte) 134,
      (byte) 177,
      (byte) 248,
      (byte) 125,
      (byte) 54,
      (byte) 131,
      (byte) 111,
      (byte) 241,
      (byte) 7,
      (byte) 214,
      (byte) 50,
      (byte) 201,
      (byte) 144 /*0x90*/,
      (byte) 206,
      (byte) 206,
      (byte) 84,
      (byte) 36,
      (byte) 148,
      (byte) 245,
      (byte) 73,
      (byte) 198,
      (byte) 148,
      (byte) 23,
      (byte) 251,
      (byte) 190,
      (byte) 17,
      (byte) 182,
      (byte) 131,
      (byte) 4,
      (byte) 237,
      (byte) 204,
      (byte) 106,
      (byte) 198,
      (byte) 165,
      (byte) 198,
      (byte) 247,
      (byte) 42,
      (byte) 153,
      (byte) 227,
      (byte) 15,
      (byte) 41,
      (byte) 169,
      (byte) 133,
      (byte) 1,
      (byte) 128 /*0x80*/,
      (byte) 3,
      (byte) 222,
      (byte) 136,
      (byte) 219,
      (byte) 106,
      (byte) 136,
      (byte) 192 /*0xC0*/,
      (byte) 111
    };
    byte[] numArray12 = new byte[55]
    {
      (byte) 88,
      (byte) 121,
      (byte) 242,
      (byte) 64 /*0x40*/,
      (byte) 123,
      (byte) 119,
      (byte) 112 /*0x70*/,
      (byte) 125,
      (byte) 43,
      (byte) 48 /*0x30*/,
      (byte) 247,
      (byte) 50,
      (byte) 10,
      (byte) 236,
      (byte) 103,
      (byte) 96 /*0x60*/,
      (byte) 198,
      (byte) 11,
      (byte) 218,
      (byte) 36,
      (byte) 59,
      (byte) 201,
      (byte) 175,
      (byte) 86,
      (byte) 154,
      (byte) 245,
      (byte) 183,
      (byte) 82,
      (byte) 207,
      (byte) 239,
      (byte) 130,
      (byte) 108,
      (byte) 161,
      (byte) 245,
      (byte) 225,
      (byte) 182,
      (byte) 132,
      (byte) 235,
      (byte) 176 /*0xB0*/,
      (byte) 200,
      (byte) 113,
      (byte) 10,
      (byte) 152,
      (byte) 46,
      (byte) 214,
      (byte) 214,
      (byte) 27,
      (byte) 211,
      (byte) 241,
      (byte) 36,
      (byte) 219,
      (byte) 58,
      (byte) 33,
      (byte) 200,
      (byte) 67
    };
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray8, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index + 55] ^= numArray12[index];
    byte[] numArray13 = new byte[10];
    numArray13[5] = (byte) 205;
    numArray13[8] = (byte) 26;
    numArray13[2] = (byte) 252;
    numArray13[3] = (byte) 190;
    numArray13[4] = (byte) 37;
    numArray13[0] = (byte) 241;
    numArray13[7] = (byte) 236;
    numArray13[1] = (byte) 153;
    numArray13[6] = (byte) 236;
    numArray13[9] = (byte) 10;
    byte[] numArray14 = new byte[10];
    numArray14[4] = (byte) 195;
    numArray14[1] = (byte) 1;
    numArray14[2] = (byte) 230;
    numArray14[5] = (byte) 88;
    numArray14[8] = (byte) 113;
    numArray14[7] = byte.MaxValue;
    numArray14[6] = (byte) 72;
    numArray14[0] = (byte) 211;
    numArray14[9] = (byte) 116;
    numArray14[3] = (byte) 52;
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray8, 110, 10);
    for (int index = 0; index < 10; ++index)
      numArray8[index + 110] ^= numArray14[index];
    byte[] numArray15 = new byte[35];
    byte[] response = new byte[35];
    Array.Copy((Array) sc_13302.sspq, 266, (Array) numArray15, 0, 35);
    key.Query(true, 335, numArray15, response);
    Array.Copy((Array) sc_13302.sspr, 266, (Array) numArray15, 0, 35);
    for (int index = 0; index < numArray15.Length; ++index)
    {
      if ((int) numArray15[index] != (int) response[index])
      {
        key.TagValue = (int) response[index];
        break;
      }
    }
    return Encoding.UTF8.GetString(numArray8);
  }

  internal static string ssp_appserver_13374()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[118];
      byte[] numArray2 = new byte[55]
      {
        (byte) 252,
        (byte) 177,
        (byte) 243,
        (byte) 163,
        (byte) 226,
        (byte) 97,
        (byte) 163,
        (byte) 30,
        (byte) 194,
        (byte) 186,
        (byte) 63 /*0x3F*/,
        (byte) 172,
        (byte) 163,
        (byte) 90,
        (byte) 104,
        (byte) 252,
        (byte) 114,
        (byte) 243,
        (byte) 68,
        (byte) 241,
        (byte) 217,
        (byte) 171,
        (byte) 214,
        (byte) 202,
        (byte) 161,
        (byte) 248,
        (byte) 105,
        (byte) 182,
        (byte) 248,
        (byte) 236,
        (byte) 201,
        (byte) 111,
        (byte) 78,
        (byte) 137,
        (byte) 68,
        (byte) 74,
        (byte) 27,
        (byte) 229,
        (byte) 246,
        (byte) 192 /*0xC0*/,
        (byte) 63 /*0x3F*/,
        (byte) 100,
        (byte) 190,
        (byte) 73,
        (byte) 179,
        (byte) 243,
        (byte) 160 /*0xA0*/,
        (byte) 248,
        (byte) 76,
        (byte) 2,
        (byte) 89,
        (byte) 67,
        (byte) 63 /*0x3F*/,
        (byte) 65,
        (byte) 160 /*0xA0*/
      };
      byte[] numArray3 = new byte[55];
      numArray3[48 /*0x30*/] = (byte) 210;
      numArray3[24] = byte.MaxValue;
      numArray3[49] = (byte) 21;
      numArray3[41] = (byte) 99;
      numArray3[31 /*0x1F*/] = (byte) 117;
      numArray3[50] = (byte) 31 /*0x1F*/;
      numArray3[18] = (byte) 15;
      numArray3[21] = (byte) 221;
      numArray3[8] = (byte) 67;
      numArray3[9] = (byte) 62;
      numArray3[25] = (byte) 73;
      numArray3[6] = (byte) 69;
      numArray3[19] = (byte) 175;
      numArray3[13] = (byte) 119;
      numArray3[11] = (byte) 161;
      numArray3[15] = (byte) 54;
      numArray3[16 /*0x10*/] = (byte) 243;
      numArray3[2] = (byte) 254;
      numArray3[12] = (byte) 133;
      numArray3[22] = (byte) 228;
      numArray3[17] = (byte) 77;
      numArray3[35] = (byte) 13;
      numArray3[53] = (byte) 222;
      numArray3[23] = (byte) 60;
      numArray3[4] = (byte) 79;
      numArray3[1] = (byte) 227;
      numArray3[42] = (byte) 219;
      numArray3[27] = (byte) 147;
      numArray3[28] = (byte) 235;
      numArray3[29] = (byte) 138;
      numArray3[30] = (byte) 189;
      numArray3[54] = (byte) 119;
      numArray3[32 /*0x20*/] = (byte) 62;
      numArray3[33] = (byte) 106;
      numArray3[34] = (byte) 25;
      numArray3[44] = (byte) 3;
      numArray3[3] = (byte) 229;
      numArray3[37] = (byte) 251;
      numArray3[38] = (byte) 185;
      numArray3[39] = (byte) 178;
      numArray3[40] = (byte) 65;
      numArray3[7] = (byte) 246;
      numArray3[52] = (byte) 74;
      numArray3[43] = (byte) 45;
      numArray3[5] = (byte) 184;
      numArray3[51] = (byte) 86;
      numArray3[36] = (byte) 199;
      numArray3[47] = (byte) 203;
      numArray3[14] = (byte) 251;
      numArray3[26] = (byte) 132;
      numArray3[10] = (byte) 32 /*0x20*/;
      numArray3[46] = (byte) 57;
      numArray3[0] = (byte) 151;
      numArray3[20] = (byte) 18;
      numArray3[45] = (byte) 209;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 17,
        (byte) 84,
        (byte) 146,
        (byte) 133,
        (byte) 130,
        (byte) 48 /*0x30*/,
        (byte) 67,
        (byte) 106,
        (byte) 159,
        (byte) 81,
        (byte) 50,
        (byte) 34,
        (byte) 175,
        (byte) 237,
        (byte) 169,
        (byte) 241,
        (byte) 187,
        (byte) 95,
        (byte) 55,
        (byte) 4,
        (byte) 52,
        (byte) 52,
        (byte) 24,
        (byte) 209,
        (byte) 143,
        (byte) 60,
        (byte) 212,
        (byte) 197,
        (byte) 64 /*0x40*/,
        (byte) 116,
        (byte) 125,
        (byte) 189,
        (byte) 144 /*0x90*/,
        (byte) 31 /*0x1F*/,
        (byte) 225,
        (byte) 143,
        (byte) 121,
        (byte) 67,
        (byte) 208 /*0xD0*/,
        (byte) 13,
        (byte) 164,
        (byte) 237,
        (byte) 67,
        (byte) 68,
        (byte) 83,
        (byte) 192 /*0xC0*/,
        (byte) 126,
        (byte) 52,
        (byte) 34,
        (byte) 84,
        byte.MaxValue,
        (byte) 47,
        (byte) 244,
        (byte) 94,
        (byte) 26
      };
      byte[] numArray5 = new byte[55];
      numArray5[49] = (byte) 48 /*0x30*/;
      numArray5[39] = (byte) 218;
      numArray5[2] = (byte) 121;
      numArray5[9] = (byte) 211;
      numArray5[6] = (byte) 88;
      numArray5[29] = (byte) 246;
      numArray5[42] = (byte) 13;
      numArray5[7] = (byte) 143;
      numArray5[32 /*0x20*/] = (byte) 231;
      numArray5[31 /*0x1F*/] = (byte) 150;
      numArray5[40] = (byte) 172;
      numArray5[17] = (byte) 155;
      numArray5[54] = (byte) 115;
      numArray5[13] = (byte) 116;
      numArray5[51] = (byte) 218;
      numArray5[23] = (byte) 118;
      numArray5[16 /*0x10*/] = (byte) 113;
      numArray5[37] = (byte) 179;
      numArray5[3] = (byte) 66;
      numArray5[19] = (byte) 106;
      numArray5[20] = (byte) 121;
      numArray5[15] = (byte) 21;
      numArray5[30] = (byte) 158;
      numArray5[45] = (byte) 164;
      numArray5[12] = (byte) 181;
      numArray5[25] = (byte) 162;
      numArray5[26] = (byte) 6;
      numArray5[10] = (byte) 12;
      numArray5[22] = (byte) 184;
      numArray5[21] = (byte) 140;
      numArray5[11] = (byte) 107;
      numArray5[27] = (byte) 111;
      numArray5[47] = (byte) 197;
      numArray5[5] = (byte) 183;
      numArray5[34] = (byte) 232;
      numArray5[35] = (byte) 171;
      numArray5[36] = (byte) 185;
      numArray5[14] = (byte) 5;
      numArray5[33] = (byte) 60;
      numArray5[0] = (byte) 107;
      numArray5[8] = (byte) 189;
      numArray5[41] = (byte) 55;
      numArray5[46] = (byte) 234;
      numArray5[43] = (byte) 171;
      numArray5[44] = (byte) 7;
      numArray5[50] = (byte) 148;
      numArray5[1] = (byte) 95;
      numArray5[28] = (byte) 180;
      numArray5[48 /*0x30*/] = (byte) 139;
      numArray5[4] = (byte) 197;
      numArray5[24] = (byte) 49;
      numArray5[18] = (byte) 125;
      numArray5[52] = (byte) 125;
      numArray5[53] = (byte) 157;
      numArray5[38] = (byte) 59;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[8]
      {
        (byte) 68,
        (byte) 38,
        (byte) 136,
        (byte) 188,
        (byte) 219,
        (byte) 62,
        (byte) 181,
        (byte) 133
      };
      byte[] numArray7 = new byte[8];
      numArray7[2] = (byte) 18;
      numArray7[0] = (byte) 196;
      numArray7[3] = (byte) 180;
      numArray7[6] = (byte) 172;
      numArray7[4] = (byte) 127 /*0x7F*/;
      numArray7[5] = (byte) 54;
      numArray7[1] = (byte) 41;
      numArray7[7] = (byte) 11;
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 8);
      for (int index = 0; index < 8; ++index)
        numArray1[index + 110] ^= numArray7[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray8 = new byte[118];
    byte[] numArray9 = new byte[55]
    {
      (byte) 215,
      (byte) 98,
      (byte) 158,
      (byte) 160 /*0xA0*/,
      (byte) 206,
      (byte) 119,
      (byte) 154,
      (byte) 15,
      (byte) 237,
      (byte) 173,
      (byte) 254,
      (byte) 52,
      (byte) 50,
      (byte) 23,
      (byte) 234,
      (byte) 115,
      (byte) 38,
      (byte) 0,
      (byte) 244,
      (byte) 75,
      (byte) 132,
      (byte) 98,
      (byte) 236,
      (byte) 50,
      (byte) 208 /*0xD0*/,
      (byte) 101,
      (byte) 32 /*0x20*/,
      (byte) 65,
      (byte) 27,
      (byte) 93,
      (byte) 155,
      (byte) 81,
      (byte) 196,
      (byte) 78,
      (byte) 21,
      (byte) 6,
      (byte) 165,
      (byte) 134,
      (byte) 185,
      (byte) 68,
      (byte) 64 /*0x40*/,
      (byte) 47,
      (byte) 200,
      (byte) 215,
      (byte) 173,
      (byte) 42,
      (byte) 87,
      (byte) 239,
      (byte) 220,
      (byte) 77,
      (byte) 3,
      (byte) 4,
      (byte) 132,
      (byte) 238,
      byte.MaxValue
    };
    byte[] numArray10 = new byte[55];
    numArray10[54] = (byte) 155;
    numArray10[1] = (byte) 171;
    numArray10[2] = (byte) 203;
    numArray10[43] = (byte) 68;
    numArray10[4] = (byte) 79;
    numArray10[23] = (byte) 237;
    numArray10[20] = (byte) 21;
    numArray10[48 /*0x30*/] = (byte) 212;
    numArray10[8] = (byte) 163;
    numArray10[6] = (byte) 221;
    numArray10[10] = (byte) 4;
    numArray10[46] = (byte) 188;
    numArray10[26] = (byte) 209;
    numArray10[41] = (byte) 191;
    numArray10[14] = (byte) 136;
    numArray10[15] = (byte) 110;
    numArray10[33] = (byte) 243;
    numArray10[25] = (byte) 165;
    numArray10[18] = (byte) 221;
    numArray10[19] = (byte) 252;
    numArray10[29] = (byte) 45;
    numArray10[51] = (byte) 77;
    numArray10[22] = (byte) 8;
    numArray10[36] = (byte) 152;
    numArray10[17] = (byte) 152;
    numArray10[39] = (byte) 19;
    numArray10[42] = (byte) 251;
    numArray10[27] = (byte) 44;
    numArray10[28] = (byte) 33;
    numArray10[53] = (byte) 61;
    numArray10[12] = (byte) 157;
    numArray10[31 /*0x1F*/] = (byte) 181;
    numArray10[11] = (byte) 33;
    numArray10[0] = (byte) 234;
    numArray10[34] = (byte) 250;
    numArray10[13] = (byte) 252;
    numArray10[16 /*0x10*/] = (byte) 167;
    numArray10[35] = (byte) 96 /*0x60*/;
    numArray10[38] = (byte) 186;
    numArray10[24] = (byte) 49;
    numArray10[40] = (byte) 75;
    numArray10[37] = (byte) 199;
    numArray10[3] = (byte) 181;
    numArray10[21] = (byte) 230;
    numArray10[9] = (byte) 144 /*0x90*/;
    numArray10[45] = (byte) 229;
    numArray10[32 /*0x20*/] = (byte) 133;
    numArray10[44] = (byte) 56;
    numArray10[30] = (byte) 60;
    numArray10[49] = (byte) 18;
    numArray10[50] = (byte) 84;
    numArray10[7] = (byte) 160 /*0xA0*/;
    numArray10[52] = (byte) 140;
    numArray10[47] = (byte) 92;
    numArray10[5] = (byte) 57;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray8, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index] ^= numArray10[index];
    byte[] numArray11 = new byte[55]
    {
      (byte) 232,
      (byte) 42,
      (byte) 91,
      (byte) 37,
      (byte) 244,
      (byte) 37,
      (byte) 53,
      (byte) 123,
      (byte) 225,
      (byte) 154,
      (byte) 149,
      (byte) 98,
      (byte) 84,
      (byte) 149,
      (byte) 234,
      (byte) 246,
      (byte) 193,
      (byte) 35,
      (byte) 25,
      (byte) 96 /*0x60*/,
      (byte) 78,
      (byte) 132,
      (byte) 236,
      (byte) 155,
      (byte) 67,
      (byte) 109,
      (byte) 77,
      (byte) 246,
      (byte) 217,
      (byte) 247,
      (byte) 241,
      (byte) 163,
      (byte) 196,
      (byte) 46,
      (byte) 80 /*0x50*/,
      (byte) 139,
      (byte) 244,
      (byte) 200,
      (byte) 242,
      (byte) 22,
      (byte) 3,
      (byte) 74,
      (byte) 30,
      (byte) 31 /*0x1F*/,
      (byte) 221,
      (byte) 138,
      (byte) 162,
      (byte) 235,
      (byte) 121,
      (byte) 198,
      (byte) 19,
      (byte) 193,
      (byte) 145,
      (byte) 122,
      (byte) 18
    };
    byte[] numArray12 = new byte[55];
    numArray12[43] = (byte) 248;
    numArray12[47] = (byte) 183;
    numArray12[22] = (byte) 199;
    numArray12[4] = (byte) 34;
    numArray12[19] = (byte) 35;
    numArray12[5] = (byte) 204;
    numArray12[18] = (byte) 172;
    numArray12[49] = (byte) 149;
    numArray12[17] = (byte) 164;
    numArray12[9] = (byte) 145;
    numArray12[1] = (byte) 87;
    numArray12[42] = (byte) 28;
    numArray12[12] = (byte) 3;
    numArray12[13] = (byte) 163;
    numArray12[33] = (byte) 173;
    numArray12[34] = (byte) 107;
    numArray12[46] = (byte) 19;
    numArray12[6] = (byte) 178;
    numArray12[8] = (byte) 73;
    numArray12[40] = (byte) 84;
    numArray12[50] = (byte) 206;
    numArray12[0] = (byte) 186;
    numArray12[11] = (byte) 10;
    numArray12[23] = (byte) 139;
    numArray12[24] = (byte) 206;
    numArray12[21] = (byte) 4;
    numArray12[26] = (byte) 195;
    numArray12[27] = (byte) 159;
    numArray12[28] = (byte) 145;
    numArray12[41] = (byte) 89;
    numArray12[30] = (byte) 149;
    numArray12[31 /*0x1F*/] = (byte) 148;
    numArray12[32 /*0x20*/] = (byte) 164;
    numArray12[3] = (byte) 254;
    numArray12[7] = (byte) 100;
    numArray12[20] = (byte) 7;
    numArray12[36] = (byte) 223;
    numArray12[2] = (byte) 166;
    numArray12[52] = (byte) 24;
    numArray12[14] = (byte) 50;
    numArray12[29] = (byte) 14;
    numArray12[25] = (byte) 197;
    numArray12[37] = (byte) 123;
    numArray12[10] = (byte) 113;
    numArray12[44] = (byte) 101;
    numArray12[45] = (byte) 99;
    numArray12[38] = (byte) 37;
    numArray12[15] = (byte) 29;
    numArray12[48 /*0x30*/] = (byte) 118;
    numArray12[35] = (byte) 111;
    numArray12[39] = (byte) 182;
    numArray12[51] = (byte) 211;
    numArray12[16 /*0x10*/] = (byte) 22;
    numArray12[53] = (byte) 71;
    numArray12[54] = (byte) 140;
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray8, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index + 55] ^= numArray12[index];
    byte[] numArray13 = new byte[8]
    {
      (byte) 2,
      (byte) 40,
      (byte) 188,
      (byte) 117,
      (byte) 194,
      (byte) 143,
      (byte) 6,
      (byte) 120
    };
    byte[] numArray14 = new byte[8]
    {
      (byte) 31 /*0x1F*/,
      (byte) 111,
      (byte) 103,
      (byte) 60,
      (byte) 117,
      (byte) 220,
      (byte) 105,
      (byte) 51
    };
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray8, 110, 8);
    for (int index = 0; index < 8; ++index)
      numArray8[index + 110] ^= numArray14[index];
    byte[] numArray15 = new byte[21];
    byte[] response = new byte[21];
    Array.Copy((Array) sc_13302.sspq, 301, (Array) numArray15, 0, 21);
    key.Query(true, 335, numArray15, response);
    Array.Copy((Array) sc_13302.sspr, 301, (Array) numArray15, 0, 21);
    for (int index = 0; index < numArray15.Length; ++index)
    {
      if ((int) numArray15[index] != (int) response[index])
      {
        key.TagValue = (int) response[index];
        break;
      }
    }
    return Encoding.UTF8.GetString(numArray8);
  }
}
