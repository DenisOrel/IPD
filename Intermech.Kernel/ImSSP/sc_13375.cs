// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_13375
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_13375
{
  private static byte[] sspq = new byte[86]
  {
    (byte) 224 /*0xE0*/,
    (byte) 227,
    (byte) 164,
    (byte) 23,
    (byte) 230,
    (byte) 140,
    (byte) 119,
    (byte) 214,
    (byte) 228,
    (byte) 73,
    (byte) 93,
    (byte) 117,
    (byte) 73,
    (byte) 181,
    (byte) 188,
    (byte) 87,
    (byte) 130,
    (byte) 172,
    (byte) 148,
    (byte) 233,
    (byte) 93,
    (byte) 124,
    (byte) 195,
    (byte) 129,
    (byte) 228,
    (byte) 204,
    (byte) 242,
    (byte) 104,
    (byte) 209,
    (byte) 249,
    (byte) 43,
    (byte) 177,
    (byte) 156,
    (byte) 162,
    (byte) 246,
    (byte) 21,
    (byte) 144 /*0x90*/,
    (byte) 2,
    (byte) 138,
    (byte) 75,
    (byte) 70,
    (byte) 193,
    (byte) 128 /*0x80*/,
    (byte) 78,
    (byte) 78,
    (byte) 78,
    (byte) 176 /*0xB0*/,
    (byte) 223,
    (byte) 146,
    (byte) 249,
    (byte) 172,
    (byte) 252,
    (byte) 40,
    (byte) 216,
    (byte) 150,
    (byte) 190,
    (byte) 238,
    (byte) 58,
    (byte) 36,
    (byte) 96 /*0x60*/,
    (byte) 142,
    (byte) 149,
    (byte) 187,
    (byte) 140,
    (byte) 91,
    (byte) 137,
    (byte) 234,
    (byte) 42,
    (byte) 64 /*0x40*/,
    (byte) 141,
    (byte) 63 /*0x3F*/,
    (byte) 189,
    (byte) 34,
    (byte) 185,
    (byte) 184,
    (byte) 143,
    (byte) 152,
    (byte) 0,
    (byte) 189,
    (byte) 154,
    (byte) 112 /*0x70*/,
    (byte) 207,
    (byte) 252,
    (byte) 68,
    (byte) 211,
    (byte) 220
  };
  private static byte[] sspr = new byte[86]
  {
    (byte) 239,
    (byte) 32 /*0x20*/,
    (byte) 71,
    (byte) 113,
    (byte) 128 /*0x80*/,
    (byte) 130,
    (byte) 13,
    (byte) 219,
    (byte) 184,
    (byte) 188,
    (byte) 189,
    (byte) 120,
    (byte) 57,
    (byte) 208 /*0xD0*/,
    (byte) 10,
    (byte) 171,
    (byte) 148,
    (byte) 243,
    (byte) 87,
    (byte) 144 /*0x90*/,
    (byte) 140,
    (byte) 121,
    (byte) 209,
    (byte) 9,
    (byte) 234,
    (byte) 152,
    (byte) 78,
    (byte) 119,
    (byte) 162,
    (byte) 124,
    (byte) 188,
    (byte) 91,
    (byte) 68,
    (byte) 124,
    (byte) 0,
    (byte) 21,
    (byte) 154,
    (byte) 201,
    (byte) 111,
    (byte) 248,
    (byte) 64 /*0x40*/,
    (byte) 85,
    (byte) 66,
    (byte) 228,
    (byte) 203,
    (byte) 76,
    (byte) 63 /*0x3F*/,
    (byte) 69,
    (byte) 177,
    (byte) 55,
    (byte) 32 /*0x20*/,
    (byte) 20,
    (byte) 50,
    (byte) 47,
    (byte) 138,
    (byte) 227,
    (byte) 167,
    (byte) 92,
    (byte) 106,
    (byte) 34,
    (byte) 168,
    (byte) 80 /*0x50*/,
    (byte) 90,
    (byte) 53,
    (byte) 89,
    (byte) 29,
    (byte) 79,
    (byte) 193,
    (byte) 122,
    (byte) 189,
    (byte) 210,
    (byte) 128 /*0x80*/,
    (byte) 94,
    (byte) 247,
    (byte) 210,
    (byte) 140,
    (byte) 13,
    (byte) 153,
    (byte) 200,
    (byte) 240 /*0xF0*/,
    (byte) 125,
    (byte) 162,
    (byte) 180,
    (byte) 58,
    (byte) 165,
    (byte) 57
  };

  internal static int ssp_appserver_13376(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 189,
      (byte) 174,
      (byte) 200,
      (byte) 1,
      (byte) 24,
      (byte) 45,
      (byte) 253,
      (byte) 12,
      (byte) 153,
      (byte) 115,
      (byte) 76,
      (byte) 10,
      (byte) 245,
      (byte) 6,
      (byte) 203,
      (byte) 201,
      (byte) 132,
      (byte) 178,
      (byte) 205,
      (byte) 24,
      (byte) 48 /*0x30*/,
      (byte) 92,
      (byte) 111,
      (byte) 185,
      (byte) 96 /*0x60*/,
      (byte) 65,
      (byte) 60,
      (byte) 217,
      (byte) 91,
      (byte) 38,
      (byte) 252,
      (byte) 108,
      (byte) 50,
      (byte) 158,
      (byte) 85,
      (byte) 22,
      (byte) 252,
      (byte) 151,
      (byte) 67,
      (byte) 3,
      (byte) 90,
      (byte) 197,
      (byte) 50,
      (byte) 218,
      (byte) 111,
      (byte) 88,
      (byte) 89,
      (byte) 195
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[16 /*0x10*/] = (byte) 43;
    sourceArray2[20] = (byte) 41;
    sourceArray2[2] = (byte) 94;
    sourceArray2[41] = (byte) 169;
    sourceArray2[4] = (byte) 211;
    sourceArray2[38] = (byte) 68;
    sourceArray2[6] = (byte) 160 /*0xA0*/;
    sourceArray2[7] = (byte) 142;
    sourceArray2[8] = (byte) 246;
    sourceArray2[45] = (byte) 177;
    sourceArray2[10] = (byte) 11;
    sourceArray2[3] = (byte) 157;
    sourceArray2[12] = (byte) 140;
    sourceArray2[13] = (byte) 118;
    sourceArray2[40] = (byte) 171;
    sourceArray2[18] = (byte) 126;
    sourceArray2[39] = (byte) 75;
    sourceArray2[17] = (byte) 62;
    sourceArray2[28] = (byte) 204;
    sourceArray2[29] = (byte) 48 /*0x30*/;
    sourceArray2[35] = (byte) 215;
    sourceArray2[5] = (byte) 27;
    sourceArray2[47] = (byte) 49;
    sourceArray2[23] = (byte) 188;
    sourceArray2[43] = (byte) 116;
    sourceArray2[25] = (byte) 103;
    sourceArray2[26] = (byte) 50;
    sourceArray2[27] = (byte) 245;
    sourceArray2[21] = (byte) 77;
    sourceArray2[19] = (byte) 175;
    sourceArray2[24] = (byte) 193;
    sourceArray2[31 /*0x1F*/] = (byte) 247;
    sourceArray2[32 /*0x20*/] = (byte) 66;
    sourceArray2[30] = (byte) 245;
    sourceArray2[15] = (byte) 137;
    sourceArray2[11] = (byte) 223;
    sourceArray2[22] = (byte) 147;
    sourceArray2[36] = (byte) 117;
    sourceArray2[37] = (byte) 198;
    sourceArray2[33] = (byte) 235;
    sourceArray2[34] = (byte) 44;
    sourceArray2[42] = (byte) 228;
    sourceArray2[9] = (byte) 187;
    sourceArray2[0] = (byte) 249;
    sourceArray2[44] = (byte) 45;
    sourceArray2[1] = (byte) 202;
    sourceArray2[46] = (byte) 23;
    sourceArray2[14] = (byte) 249;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_13377()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[188];
      byte[] numArray2 = new byte[55];
      numArray2[1] = (byte) 2;
      numArray2[28] = (byte) 183;
      numArray2[2] = (byte) 18;
      numArray2[30] = (byte) 86;
      numArray2[36] = (byte) 103;
      numArray2[33] = (byte) 187;
      numArray2[6] = (byte) 235;
      numArray2[37] = (byte) 231;
      numArray2[19] = (byte) 214;
      numArray2[25] = (byte) 223;
      numArray2[31 /*0x1F*/] = (byte) 106;
      numArray2[11] = (byte) 169;
      numArray2[38] = (byte) 177;
      numArray2[13] = (byte) 151;
      numArray2[52] = (byte) 203;
      numArray2[43] = (byte) 9;
      numArray2[16 /*0x10*/] = (byte) 30;
      numArray2[17] = (byte) 17;
      numArray2[3] = (byte) 30;
      numArray2[23] = (byte) 188;
      numArray2[22] = (byte) 184;
      numArray2[45] = (byte) 46;
      numArray2[21] = (byte) 105;
      numArray2[47] = (byte) 95;
      numArray2[24] = (byte) 221;
      numArray2[8] = (byte) 6;
      numArray2[4] = (byte) 238;
      numArray2[27] = (byte) 4;
      numArray2[20] = (byte) 148;
      numArray2[29] = (byte) 136;
      numArray2[5] = (byte) 186;
      numArray2[14] = (byte) 239;
      numArray2[32 /*0x20*/] = (byte) 50;
      numArray2[7] = (byte) 86;
      numArray2[34] = (byte) 136;
      numArray2[9] = (byte) 148;
      numArray2[53] = (byte) 140;
      numArray2[51] = (byte) 3;
      numArray2[10] = (byte) 55;
      numArray2[39] = (byte) 196;
      numArray2[26] = (byte) 246;
      numArray2[41] = (byte) 245;
      numArray2[42] = (byte) 103;
      numArray2[40] = (byte) 181;
      numArray2[44] = (byte) 189;
      numArray2[15] = (byte) 119;
      numArray2[46] = (byte) 39;
      numArray2[12] = (byte) 197;
      numArray2[48 /*0x30*/] = (byte) 123;
      numArray2[49] = (byte) 155;
      numArray2[0] = (byte) 128 /*0x80*/;
      numArray2[18] = (byte) 244;
      numArray2[50] = (byte) 24;
      numArray2[35] = (byte) 230;
      numArray2[54] = (byte) 250;
      byte[] numArray3 = new byte[55];
      numArray3[8] = (byte) 200;
      numArray3[1] = (byte) 140;
      numArray3[4] = (byte) 8;
      numArray3[25] = (byte) 226;
      numArray3[22] = (byte) 137;
      numArray3[5] = (byte) 163;
      numArray3[12] = (byte) 93;
      numArray3[44] = (byte) 144 /*0x90*/;
      numArray3[47] = (byte) 123;
      numArray3[14] = (byte) 121;
      numArray3[48 /*0x30*/] = (byte) 49;
      numArray3[11] = (byte) 25;
      numArray3[43] = (byte) 243;
      numArray3[13] = (byte) 181;
      numArray3[16 /*0x10*/] = (byte) 49;
      numArray3[10] = (byte) 104;
      numArray3[2] = (byte) 95;
      numArray3[21] = (byte) 7;
      numArray3[50] = (byte) 83;
      numArray3[19] = (byte) 54;
      numArray3[20] = (byte) 6;
      numArray3[0] = (byte) 204;
      numArray3[39] = (byte) 93;
      numArray3[42] = (byte) 210;
      numArray3[15] = (byte) 186;
      numArray3[45] = (byte) 147;
      numArray3[9] = (byte) 109;
      numArray3[27] = (byte) 60;
      numArray3[28] = (byte) 72;
      numArray3[29] = (byte) 8;
      numArray3[54] = (byte) 96 /*0x60*/;
      numArray3[31 /*0x1F*/] = (byte) 158;
      numArray3[32 /*0x20*/] = (byte) 107;
      numArray3[33] = (byte) 167;
      numArray3[6] = (byte) 119;
      numArray3[35] = (byte) 246;
      numArray3[7] = (byte) 103;
      numArray3[30] = (byte) 38;
      numArray3[38] = (byte) 68;
      numArray3[52] = (byte) 136;
      numArray3[40] = (byte) 93;
      numArray3[41] = (byte) 2;
      numArray3[37] = (byte) 134;
      numArray3[34] = (byte) 246;
      numArray3[17] = (byte) 30;
      numArray3[18] = (byte) 20;
      numArray3[46] = (byte) 187;
      numArray3[24] = (byte) 174;
      numArray3[36] = (byte) 29;
      numArray3[26] = (byte) 86;
      numArray3[23] = (byte) 178;
      numArray3[51] = (byte) 174;
      numArray3[49] = (byte) 182;
      numArray3[53] = (byte) 117;
      numArray3[3] = (byte) 243;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 71,
        (byte) 152,
        (byte) 189,
        (byte) 231,
        (byte) 73,
        (byte) 241,
        (byte) 127 /*0x7F*/,
        (byte) 58,
        (byte) 44,
        (byte) 152,
        (byte) 227,
        (byte) 65,
        (byte) 107,
        (byte) 69,
        (byte) 119,
        (byte) 113,
        (byte) 92,
        (byte) 229,
        (byte) 116,
        (byte) 60,
        (byte) 0,
        (byte) 78,
        (byte) 18,
        (byte) 197,
        (byte) 147,
        (byte) 17,
        (byte) 5,
        (byte) 58,
        (byte) 199,
        (byte) 149,
        (byte) 32 /*0x20*/,
        (byte) 81,
        (byte) 95,
        (byte) 73,
        (byte) 114,
        (byte) 126,
        (byte) 141,
        (byte) 234,
        (byte) 37,
        (byte) 225,
        (byte) 91,
        (byte) 167,
        (byte) 147,
        (byte) 77,
        (byte) 240 /*0xF0*/,
        (byte) 112 /*0x70*/,
        (byte) 153,
        (byte) 70,
        (byte) 219,
        (byte) 118,
        (byte) 16 /*0x10*/,
        (byte) 51,
        (byte) 159,
        (byte) 192 /*0xC0*/,
        (byte) 184
      };
      byte[] numArray5 = new byte[55]
      {
        (byte) 110,
        (byte) 163,
        (byte) 78,
        (byte) 54,
        (byte) 6,
        (byte) 120,
        (byte) 74,
        (byte) 231,
        (byte) 145,
        (byte) 73,
        (byte) 229,
        (byte) 73,
        (byte) 19,
        (byte) 184,
        (byte) 134,
        (byte) 45,
        (byte) 75,
        (byte) 166,
        (byte) 23,
        (byte) 212,
        (byte) 211,
        (byte) 124,
        (byte) 96 /*0x60*/,
        (byte) 4,
        (byte) 239,
        (byte) 84,
        (byte) 27,
        (byte) 53,
        (byte) 35,
        (byte) 243,
        (byte) 195,
        (byte) 13,
        (byte) 191,
        (byte) 38,
        (byte) 171,
        (byte) 129,
        (byte) 254,
        (byte) 253,
        (byte) 133,
        (byte) 9,
        (byte) 97,
        (byte) 57,
        (byte) 242,
        (byte) 154,
        (byte) 228,
        (byte) 133,
        (byte) 43,
        (byte) 95,
        (byte) 212,
        (byte) 231,
        (byte) 76,
        (byte) 107,
        (byte) 62,
        (byte) 83,
        (byte) 210
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[55]
      {
        (byte) 247,
        (byte) 84,
        (byte) 55,
        (byte) 235,
        (byte) 69,
        (byte) 114,
        (byte) 151,
        (byte) 119,
        (byte) 146,
        (byte) 226,
        (byte) 33,
        (byte) 210,
        (byte) 248,
        (byte) 227,
        (byte) 113,
        (byte) 192 /*0xC0*/,
        (byte) 146,
        (byte) 224 /*0xE0*/,
        (byte) 88,
        (byte) 186,
        (byte) 197,
        (byte) 213,
        (byte) 35,
        (byte) 119,
        (byte) 191,
        (byte) 95,
        (byte) 81,
        (byte) 132,
        (byte) 237,
        (byte) 178,
        (byte) 158,
        (byte) 145,
        (byte) 121,
        (byte) 249,
        (byte) 149,
        (byte) 150,
        (byte) 237,
        (byte) 245,
        (byte) 22,
        (byte) 53,
        (byte) 252,
        (byte) 213,
        (byte) 93,
        (byte) 141,
        (byte) 119,
        (byte) 115,
        (byte) 226,
        (byte) 146,
        (byte) 159,
        (byte) 115,
        (byte) 12,
        (byte) 224 /*0xE0*/,
        (byte) 96 /*0x60*/,
        (byte) 62,
        (byte) 191
      };
      byte[] numArray7 = new byte[55];
      numArray7[53] = (byte) 115;
      numArray7[1] = (byte) 80 /*0x50*/;
      numArray7[23] = (byte) 160 /*0xA0*/;
      numArray7[3] = (byte) 242;
      numArray7[48 /*0x30*/] = (byte) 94;
      numArray7[2] = (byte) 232;
      numArray7[6] = (byte) 97;
      numArray7[35] = (byte) 179;
      numArray7[25] = (byte) 97;
      numArray7[9] = (byte) 150;
      numArray7[10] = (byte) 242;
      numArray7[7] = (byte) 108;
      numArray7[17] = (byte) 197;
      numArray7[41] = (byte) 223;
      numArray7[24] = (byte) 27;
      numArray7[49] = (byte) 124;
      numArray7[19] = (byte) 144 /*0x90*/;
      numArray7[15] = (byte) 251;
      numArray7[21] = (byte) 135;
      numArray7[51] = (byte) 64 /*0x40*/;
      numArray7[33] = (byte) 157;
      numArray7[16 /*0x10*/] = (byte) 80 /*0x50*/;
      numArray7[22] = (byte) 47;
      numArray7[4] = (byte) 189;
      numArray7[52] = (byte) 12;
      numArray7[18] = (byte) 164;
      numArray7[26] = (byte) 208 /*0xD0*/;
      numArray7[27] = (byte) 26;
      numArray7[28] = (byte) 231;
      numArray7[29] = (byte) 67;
      numArray7[30] = (byte) 229;
      numArray7[11] = (byte) 232;
      numArray7[32 /*0x20*/] = (byte) 106;
      numArray7[12] = (byte) 195;
      numArray7[14] = (byte) 116;
      numArray7[38] = (byte) 16 /*0x10*/;
      numArray7[36] = (byte) 192 /*0xC0*/;
      numArray7[0] = (byte) 250;
      numArray7[20] = (byte) 22;
      numArray7[39] = (byte) 204;
      numArray7[40] = (byte) 84;
      numArray7[34] = (byte) 150;
      numArray7[43] = (byte) 25;
      numArray7[37] = (byte) 198;
      numArray7[44] = (byte) 180;
      numArray7[45] = (byte) 249;
      numArray7[46] = (byte) 112 /*0x70*/;
      numArray7[5] = (byte) 217;
      numArray7[8] = (byte) 173;
      numArray7[42] = (byte) 207;
      numArray7[50] = (byte) 159;
      numArray7[47] = (byte) 137;
      numArray7[31 /*0x1F*/] = (byte) 125;
      numArray7[13] = (byte) 63 /*0x3F*/;
      numArray7[54] = (byte) 130;
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[23]
      {
        (byte) 192 /*0xC0*/,
        (byte) 224 /*0xE0*/,
        (byte) 104,
        (byte) 99,
        (byte) 46,
        (byte) 16 /*0x10*/,
        (byte) 57,
        (byte) 133,
        (byte) 95,
        (byte) 29,
        (byte) 196,
        (byte) 219,
        (byte) 116,
        (byte) 165,
        (byte) 211,
        (byte) 152,
        (byte) 14,
        (byte) 213,
        (byte) 165,
        (byte) 249,
        (byte) 75,
        (byte) 24,
        (byte) 49
      };
      byte[] numArray9 = new byte[23];
      numArray9[4] = (byte) 15;
      numArray9[12] = (byte) 174;
      numArray9[2] = (byte) 213;
      numArray9[3] = (byte) 227;
      numArray9[18] = (byte) 225;
      numArray9[19] = (byte) 32 /*0x20*/;
      numArray9[6] = (byte) 102;
      numArray9[17] = (byte) 7;
      numArray9[20] = (byte) 214;
      numArray9[21] = (byte) 227;
      numArray9[10] = (byte) 48 /*0x30*/;
      numArray9[11] = (byte) 96 /*0x60*/;
      numArray9[15] = (byte) 143;
      numArray9[0] = (byte) 217;
      numArray9[14] = (byte) 151;
      numArray9[13] = (byte) 76;
      numArray9[16 /*0x10*/] = (byte) 179;
      numArray9[8] = (byte) 174;
      numArray9[7] = (byte) 24;
      numArray9[1] = (byte) 166;
      numArray9[5] = (byte) 19;
      numArray9[9] = (byte) 121;
      numArray9[22] = (byte) 123;
      key.Query(true, 335, numArray8, numArray8);
      Array.Copy((Array) numArray8, 0, (Array) numArray1, 165, 23);
      for (int index = 0; index < 23; ++index)
        numArray1[index + 165] ^= numArray9[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray10 = new byte[188];
    byte[] numArray11 = new byte[55]
    {
      (byte) 126,
      (byte) 46,
      (byte) 171,
      (byte) 40,
      (byte) 176 /*0xB0*/,
      (byte) 80 /*0x50*/,
      (byte) 168,
      (byte) 175,
      (byte) 206,
      (byte) 233,
      (byte) 13,
      (byte) 55,
      (byte) 2,
      (byte) 221,
      (byte) 108,
      (byte) 99,
      (byte) 88,
      (byte) 18,
      (byte) 169,
      (byte) 56,
      (byte) 234,
      (byte) 207,
      (byte) 94,
      (byte) 26,
      (byte) 107,
      (byte) 180,
      (byte) 134,
      (byte) 145,
      (byte) 52,
      (byte) 123,
      (byte) 16 /*0x10*/,
      (byte) 209,
      (byte) 222,
      (byte) 155,
      (byte) 40,
      (byte) 196,
      (byte) 15,
      (byte) 67,
      (byte) 23,
      (byte) 122,
      (byte) 196,
      (byte) 57,
      (byte) 250,
      (byte) 199,
      (byte) 173,
      (byte) 3,
      (byte) 66,
      (byte) 230,
      (byte) 26,
      (byte) 153,
      (byte) 197,
      (byte) 87,
      (byte) 165,
      (byte) 194,
      (byte) 176 /*0xB0*/
    };
    byte[] numArray12 = new byte[55]
    {
      (byte) 64 /*0x40*/,
      (byte) 205,
      (byte) 83,
      (byte) 138,
      (byte) 79,
      (byte) 69,
      (byte) 137,
      (byte) 55,
      (byte) 71,
      (byte) 129,
      (byte) 161,
      (byte) 41,
      (byte) 197,
      (byte) 71,
      (byte) 219,
      (byte) 134,
      (byte) 3,
      (byte) 179,
      (byte) 30,
      (byte) 18,
      (byte) 248,
      (byte) 161,
      (byte) 19,
      (byte) 137,
      (byte) 75,
      (byte) 52,
      (byte) 32 /*0x20*/,
      (byte) 226,
      (byte) 11,
      (byte) 221,
      (byte) 25,
      (byte) 137,
      (byte) 110,
      (byte) 104,
      (byte) 192 /*0xC0*/,
      (byte) 207,
      (byte) 203,
      (byte) 10,
      (byte) 74,
      (byte) 147,
      (byte) 125,
      (byte) 7,
      (byte) 194,
      (byte) 193,
      (byte) 191,
      (byte) 2,
      (byte) 76,
      (byte) 236,
      (byte) 80 /*0x50*/,
      (byte) 47,
      (byte) 208 /*0xD0*/,
      (byte) 187,
      (byte) 248,
      (byte) 75,
      (byte) 132
    };
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray10, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index] ^= numArray12[index];
    byte[] numArray13 = new byte[55];
    numArray13[10] = (byte) 102;
    numArray13[11] = (byte) 181;
    numArray13[2] = (byte) 252;
    numArray13[49] = (byte) 92;
    numArray13[39] = (byte) 190;
    numArray13[22] = (byte) 104;
    numArray13[6] = (byte) 82;
    numArray13[7] = (byte) 216;
    numArray13[3] = (byte) 202;
    numArray13[25] = (byte) 118;
    numArray13[17] = (byte) 116;
    numArray13[44] = (byte) 219;
    numArray13[12] = (byte) 167;
    numArray13[38] = (byte) 173;
    numArray13[14] = (byte) 21;
    numArray13[52] = byte.MaxValue;
    numArray13[16 /*0x10*/] = (byte) 47;
    numArray13[54] = (byte) 58;
    numArray13[18] = (byte) 176 /*0xB0*/;
    numArray13[26] = (byte) 154;
    numArray13[27] = (byte) 182;
    numArray13[40] = (byte) 167;
    numArray13[21] = (byte) 108;
    numArray13[23] = (byte) 114;
    numArray13[24] = (byte) 159;
    numArray13[4] = (byte) 23;
    numArray13[15] = (byte) 250;
    numArray13[5] = (byte) 123;
    numArray13[28] = (byte) 185;
    numArray13[29] = (byte) 43;
    numArray13[47] = (byte) 69;
    numArray13[31 /*0x1F*/] = (byte) 235;
    numArray13[32 /*0x20*/] = (byte) 120;
    numArray13[33] = (byte) 244;
    numArray13[34] = (byte) 126;
    numArray13[50] = (byte) 118;
    numArray13[36] = (byte) 27;
    numArray13[37] = (byte) 176 /*0xB0*/;
    numArray13[35] = (byte) 134;
    numArray13[13] = (byte) 83;
    numArray13[48 /*0x30*/] = (byte) 175;
    numArray13[41] = (byte) 239;
    numArray13[8] = (byte) 59;
    numArray13[43] = (byte) 170;
    numArray13[9] = (byte) 88;
    numArray13[45] = (byte) 49;
    numArray13[42] = (byte) 113;
    numArray13[20] = (byte) 150;
    numArray13[30] = (byte) 27;
    numArray13[19] = (byte) 213;
    numArray13[0] = (byte) 144 /*0x90*/;
    numArray13[51] = (byte) 77;
    numArray13[46] = (byte) 115;
    numArray13[53] = (byte) 218;
    numArray13[1] = (byte) 189;
    byte[] numArray14 = new byte[55]
    {
      (byte) 199,
      (byte) 43,
      (byte) 242,
      (byte) 10,
      (byte) 152,
      (byte) 64 /*0x40*/,
      (byte) 119,
      (byte) 252,
      (byte) 242,
      (byte) 222,
      (byte) 213,
      (byte) 16 /*0x10*/,
      (byte) 165,
      (byte) 42,
      (byte) 226,
      (byte) 60,
      (byte) 49,
      (byte) 99,
      (byte) 204,
      (byte) 67,
      (byte) 195,
      (byte) 48 /*0x30*/,
      (byte) 94,
      (byte) 207,
      (byte) 95,
      (byte) 189,
      (byte) 69,
      (byte) 80 /*0x50*/,
      (byte) 109,
      (byte) 99,
      (byte) 49,
      (byte) 106,
      (byte) 252,
      (byte) 227,
      (byte) 209,
      (byte) 98,
      (byte) 162,
      (byte) 26,
      (byte) 208 /*0xD0*/,
      (byte) 212,
      (byte) 222,
      (byte) 252,
      (byte) 62,
      (byte) 14,
      (byte) 152,
      (byte) 152,
      (byte) 37,
      (byte) 196,
      (byte) 181,
      (byte) 47,
      (byte) 127 /*0x7F*/,
      (byte) 131,
      (byte) 153,
      (byte) 76,
      (byte) 57
    };
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray10, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index + 55] ^= numArray14[index];
    byte[] numArray15 = new byte[55]
    {
      (byte) 242,
      (byte) 189,
      (byte) 157,
      (byte) 139,
      (byte) 149,
      (byte) 194,
      (byte) 91,
      (byte) 110,
      (byte) 143,
      (byte) 158,
      (byte) 249,
      (byte) 121,
      (byte) 94,
      (byte) 76,
      (byte) 250,
      (byte) 186,
      (byte) 19,
      (byte) 34,
      (byte) 214,
      (byte) 153,
      (byte) 79,
      (byte) 55,
      (byte) 120,
      (byte) 67,
      (byte) 228,
      (byte) 231,
      (byte) 222,
      (byte) 189,
      (byte) 71,
      (byte) 222,
      (byte) 241,
      (byte) 38,
      (byte) 49,
      (byte) 153,
      (byte) 159,
      (byte) 200,
      (byte) 110,
      (byte) 83,
      (byte) 118,
      (byte) 14,
      (byte) 5,
      byte.MaxValue,
      (byte) 208 /*0xD0*/,
      (byte) 86,
      (byte) 198,
      (byte) 246,
      (byte) 193,
      (byte) 49,
      (byte) 252,
      (byte) 203,
      (byte) 249,
      (byte) 138,
      (byte) 189,
      (byte) 143,
      (byte) 71
    };
    byte[] numArray16 = new byte[55]
    {
      (byte) 21,
      (byte) 174,
      (byte) 48 /*0x30*/,
      (byte) 249,
      (byte) 127 /*0x7F*/,
      (byte) 44,
      (byte) 194,
      (byte) 97,
      (byte) 247,
      (byte) 29,
      (byte) 91,
      (byte) 154,
      (byte) 115,
      (byte) 169,
      (byte) 15,
      (byte) 132,
      (byte) 61,
      (byte) 166,
      (byte) 68,
      (byte) 39,
      (byte) 69,
      (byte) 223,
      (byte) 171,
      (byte) 238,
      (byte) 192 /*0xC0*/,
      (byte) 229,
      (byte) 194,
      (byte) 76,
      (byte) 219,
      (byte) 116,
      (byte) 190,
      (byte) 148,
      (byte) 199,
      (byte) 111,
      (byte) 177,
      (byte) 120,
      (byte) 23,
      (byte) 61,
      (byte) 224 /*0xE0*/,
      (byte) 158,
      (byte) 140,
      (byte) 176 /*0xB0*/,
      (byte) 105,
      (byte) 160 /*0xA0*/,
      (byte) 39,
      (byte) 149,
      (byte) 247,
      (byte) 236,
      (byte) 112 /*0x70*/,
      (byte) 149,
      (byte) 10,
      (byte) 9,
      (byte) 91,
      (byte) 185,
      (byte) 20
    };
    key.Query(true, 335, numArray15, numArray15);
    Array.Copy((Array) numArray15, 0, (Array) numArray10, 110, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index + 110] ^= numArray16[index];
    byte[] numArray17 = new byte[23]
    {
      (byte) 90,
      (byte) 167,
      (byte) 173,
      (byte) 227,
      (byte) 157,
      (byte) 38,
      (byte) 177,
      (byte) 172,
      (byte) 211,
      (byte) 71,
      (byte) 196,
      (byte) 173,
      (byte) 49,
      (byte) 75,
      (byte) 192 /*0xC0*/,
      (byte) 172,
      (byte) 200,
      (byte) 148,
      (byte) 80 /*0x50*/,
      (byte) 103,
      (byte) 33,
      (byte) 125,
      (byte) 237
    };
    byte[] numArray18 = new byte[23];
    numArray18[9] = (byte) 111;
    numArray18[13] = (byte) 12;
    numArray18[2] = (byte) 171;
    numArray18[3] = (byte) 156;
    numArray18[4] = (byte) 142;
    numArray18[12] = (byte) 199;
    numArray18[21] = (byte) 29;
    numArray18[18] = (byte) 247;
    numArray18[6] = (byte) 167;
    numArray18[17] = (byte) 159;
    numArray18[7] = (byte) 201;
    numArray18[11] = (byte) 58;
    numArray18[22] = (byte) 179;
    numArray18[5] = (byte) 252;
    numArray18[0] = (byte) 164;
    numArray18[15] = (byte) 248;
    numArray18[16 /*0x10*/] = (byte) 48 /*0x30*/;
    numArray18[14] = (byte) 128 /*0x80*/;
    numArray18[8] = (byte) 4;
    numArray18[19] = (byte) 176 /*0xB0*/;
    numArray18[10] = (byte) 129;
    numArray18[1] = (byte) 199;
    numArray18[20] = (byte) 168;
    key.Query(true, 335, numArray17, numArray17);
    Array.Copy((Array) numArray17, 0, (Array) numArray10, 165, 23);
    for (int index = 0; index < 23; ++index)
      numArray10[index + 165] ^= numArray18[index];
    return Encoding.UTF8.GetString(numArray10);
  }

  internal static string ssp_appserver_13378()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 245,
        (byte) 251,
        (byte) 246,
        (byte) 83,
        (byte) 34,
        (byte) 220,
        (byte) 185,
        (byte) 135,
        (byte) 54,
        (byte) 220
      };
      byte[] numArray3 = new byte[10]
      {
        (byte) 204,
        (byte) 130,
        (byte) 78,
        (byte) 137,
        (byte) 209,
        (byte) 31 /*0x1F*/,
        (byte) 137,
        (byte) 96 /*0x60*/,
        (byte) 228,
        (byte) 192 /*0xC0*/
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
      (byte) 105,
      (byte) 165,
      (byte) 114,
      (byte) 200,
      (byte) 173,
      (byte) 55,
      (byte) 20,
      (byte) 228,
      (byte) 157,
      (byte) 44
    };
    byte[] numArray6 = new byte[10]
    {
      (byte) 128 /*0x80*/,
      (byte) 142,
      (byte) 45,
      (byte) 226,
      (byte) 102,
      (byte) 119,
      (byte) 181,
      (byte) 175,
      (byte) 179,
      (byte) 85
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static int ssp_appserver_13379(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 41,
      (byte) 40,
      (byte) 245,
      (byte) 253,
      (byte) 171,
      (byte) 135,
      (byte) 116,
      (byte) 166,
      (byte) 92,
      (byte) 144 /*0x90*/,
      (byte) 97,
      (byte) 55,
      (byte) 242,
      (byte) 64 /*0x40*/,
      (byte) 244,
      (byte) 138,
      (byte) 46,
      (byte) 82,
      (byte) 99,
      (byte) 137,
      (byte) 9,
      (byte) 138,
      (byte) 187,
      (byte) 29,
      (byte) 72,
      (byte) 36,
      (byte) 250,
      (byte) 87,
      (byte) 206,
      (byte) 126,
      (byte) 11,
      (byte) 170,
      (byte) 83,
      (byte) 123,
      (byte) 94,
      (byte) 14,
      (byte) 231,
      (byte) 172,
      (byte) 32 /*0x20*/,
      (byte) 190,
      (byte) 245,
      (byte) 254,
      (byte) 22,
      (byte) 56,
      (byte) 75,
      byte.MaxValue,
      (byte) 34,
      (byte) 163
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[21] = (byte) 226;
    sourceArray2[7] = (byte) 251;
    sourceArray2[39] = (byte) 34;
    sourceArray2[3] = (byte) 198;
    sourceArray2[38] = (byte) 223;
    sourceArray2[5] = (byte) 100;
    sourceArray2[6] = (byte) 33;
    sourceArray2[23] = (byte) 3;
    sourceArray2[8] = (byte) 248;
    sourceArray2[27] = (byte) 234;
    sourceArray2[10] = (byte) 153;
    sourceArray2[11] = (byte) 220;
    sourceArray2[17] = (byte) 249;
    sourceArray2[31 /*0x1F*/] = (byte) 244;
    sourceArray2[14] = (byte) 20;
    sourceArray2[45] = (byte) 62;
    sourceArray2[16 /*0x10*/] = (byte) 176 /*0xB0*/;
    sourceArray2[12] = (byte) 119;
    sourceArray2[18] = (byte) 51;
    sourceArray2[19] = (byte) 175;
    sourceArray2[37] = (byte) 92;
    sourceArray2[4] = (byte) 235;
    sourceArray2[22] = (byte) 209;
    sourceArray2[47] = (byte) 238;
    sourceArray2[24] = (byte) 185;
    sourceArray2[32 /*0x20*/] = (byte) 18;
    sourceArray2[1] = (byte) 16 /*0x10*/;
    sourceArray2[25] = (byte) 146;
    sourceArray2[28] = (byte) 113;
    sourceArray2[13] = (byte) 5;
    sourceArray2[29] = (byte) 173;
    sourceArray2[20] = (byte) 167;
    sourceArray2[15] = (byte) 179;
    sourceArray2[33] = (byte) 14;
    sourceArray2[34] = (byte) 17;
    sourceArray2[35] = (byte) 230;
    sourceArray2[40] = (byte) 41;
    sourceArray2[30] = (byte) 127 /*0x7F*/;
    sourceArray2[26] = (byte) 211;
    sourceArray2[2] = (byte) 3;
    sourceArray2[9] = (byte) 226;
    sourceArray2[41] = (byte) 8;
    sourceArray2[42] = (byte) 229;
    sourceArray2[43] = (byte) 144 /*0x90*/;
    sourceArray2[44] = (byte) 1;
    sourceArray2[46] = (byte) 86;
    sourceArray2[0] = (byte) 153;
    sourceArray2[36] = (byte) 28;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13380(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 171,
      (byte) 194,
      (byte) 195,
      byte.MaxValue,
      (byte) 200,
      (byte) 196,
      (byte) 111,
      (byte) 107,
      (byte) 42,
      (byte) 42,
      (byte) 233,
      (byte) 7,
      (byte) 107,
      (byte) 132,
      (byte) 54,
      (byte) 109,
      (byte) 172,
      (byte) 169,
      (byte) 111,
      (byte) 33,
      (byte) 145,
      (byte) 47,
      (byte) 34,
      (byte) 67,
      (byte) 148,
      (byte) 137,
      (byte) 151,
      (byte) 114,
      (byte) 187,
      (byte) 228,
      (byte) 60,
      (byte) 207,
      (byte) 61,
      (byte) 46,
      (byte) 86,
      (byte) 148,
      (byte) 235,
      (byte) 44,
      (byte) 107,
      (byte) 80 /*0x50*/,
      (byte) 37,
      (byte) 230,
      (byte) 28,
      (byte) 148,
      (byte) 184,
      (byte) 7,
      (byte) 140,
      (byte) 21
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 11,
      (byte) 81,
      (byte) 143,
      (byte) 154,
      (byte) 84,
      (byte) 3,
      (byte) 140,
      (byte) 106,
      (byte) 99,
      (byte) 203,
      (byte) 83,
      (byte) 198,
      (byte) 73,
      (byte) 163,
      (byte) 107,
      (byte) 40,
      (byte) 149,
      (byte) 69,
      (byte) 62,
      (byte) 140,
      (byte) 62,
      (byte) 153,
      (byte) 93,
      (byte) 245,
      (byte) 83,
      (byte) 22,
      (byte) 200,
      (byte) 153,
      (byte) 11,
      (byte) 232,
      (byte) 96 /*0x60*/,
      (byte) 133,
      (byte) 61,
      (byte) 185,
      (byte) 86,
      (byte) 164,
      (byte) 173,
      (byte) 224 /*0xE0*/,
      byte.MaxValue,
      (byte) 11,
      (byte) 244,
      (byte) 124,
      (byte) 106,
      (byte) 160 /*0xA0*/,
      (byte) 228,
      (byte) 54,
      (byte) 161,
      (byte) 74
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13381(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 114,
      (byte) 136,
      (byte) 124,
      (byte) 159,
      (byte) 128 /*0x80*/,
      (byte) 106,
      (byte) 62,
      (byte) 44,
      (byte) 253,
      (byte) 176 /*0xB0*/,
      (byte) 69,
      (byte) 238,
      (byte) 25,
      (byte) 201,
      (byte) 60,
      (byte) 46,
      (byte) 161,
      (byte) 130,
      (byte) 128 /*0x80*/,
      (byte) 62,
      (byte) 30,
      (byte) 252,
      (byte) 170,
      (byte) 18,
      (byte) 141,
      (byte) 138,
      (byte) 141,
      (byte) 253,
      (byte) 129,
      (byte) 119,
      (byte) 80 /*0x50*/,
      (byte) 106,
      (byte) 174,
      (byte) 140,
      (byte) 202,
      (byte) 166,
      (byte) 217,
      (byte) 244,
      (byte) 106,
      (byte) 193,
      (byte) 45,
      (byte) 35,
      (byte) 40,
      (byte) 54,
      (byte) 214,
      (byte) 166,
      (byte) 159,
      (byte) 61
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 90,
      (byte) 90,
      (byte) 124,
      (byte) 125,
      (byte) 176 /*0xB0*/,
      (byte) 2,
      (byte) 173,
      (byte) 2,
      (byte) 216,
      (byte) 82,
      (byte) 215,
      (byte) 96 /*0x60*/,
      (byte) 12,
      (byte) 209,
      (byte) 126,
      (byte) 7,
      (byte) 70,
      (byte) 110,
      (byte) 80 /*0x50*/,
      (byte) 22,
      (byte) 127 /*0x7F*/,
      (byte) 158,
      (byte) 78,
      (byte) 36,
      (byte) 139,
      (byte) 63 /*0x3F*/,
      (byte) 212,
      (byte) 46,
      (byte) 222,
      (byte) 145,
      (byte) 122,
      (byte) 226,
      (byte) 65,
      (byte) 166,
      (byte) 8,
      (byte) 90,
      (byte) 114,
      (byte) 154,
      (byte) 189,
      (byte) 150,
      (byte) 210,
      (byte) 114,
      (byte) 156,
      (byte) 144 /*0x90*/,
      (byte) 55,
      (byte) 42,
      (byte) 115,
      (byte) 40
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13382(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[24] = (byte) 116;
    sourceArray1[1] = (byte) 37;
    sourceArray1[13] = (byte) 64 /*0x40*/;
    sourceArray1[3] = (byte) 221;
    sourceArray1[45] = (byte) 214;
    sourceArray1[0] = (byte) 79;
    sourceArray1[6] = (byte) 245;
    sourceArray1[7] = (byte) 221;
    sourceArray1[8] = (byte) 64 /*0x40*/;
    sourceArray1[16 /*0x10*/] = (byte) 150;
    sourceArray1[32 /*0x20*/] = (byte) 104;
    sourceArray1[4] = (byte) 105;
    sourceArray1[12] = (byte) 219;
    sourceArray1[25] = (byte) 59;
    sourceArray1[14] = (byte) 141;
    sourceArray1[15] = byte.MaxValue;
    sourceArray1[10] = (byte) 133;
    sourceArray1[27] = (byte) 235;
    sourceArray1[22] = (byte) 250;
    sourceArray1[2] = (byte) 118;
    sourceArray1[37] = (byte) 200;
    sourceArray1[47] = (byte) 102;
    sourceArray1[23] = (byte) 211;
    sourceArray1[20] = (byte) 190;
    sourceArray1[19] = (byte) 23;
    sourceArray1[43] = (byte) 120;
    sourceArray1[26] = (byte) 231;
    sourceArray1[5] = (byte) 18;
    sourceArray1[28] = (byte) 79;
    sourceArray1[29] = (byte) 174;
    sourceArray1[30] = (byte) 154;
    sourceArray1[31 /*0x1F*/] = (byte) 30;
    sourceArray1[21] = (byte) 16 /*0x10*/;
    sourceArray1[33] = (byte) 113;
    sourceArray1[34] = (byte) 94;
    sourceArray1[35] = (byte) 103;
    sourceArray1[36] = (byte) 84;
    sourceArray1[17] = (byte) 84;
    sourceArray1[38] = (byte) 112 /*0x70*/;
    sourceArray1[39] = (byte) 226;
    sourceArray1[40] = (byte) 117;
    sourceArray1[11] = (byte) 96 /*0x60*/;
    sourceArray1[42] = (byte) 236;
    sourceArray1[46] = (byte) 251;
    sourceArray1[44] = (byte) 163;
    sourceArray1[9] = (byte) 170;
    sourceArray1[41] = (byte) 228;
    sourceArray1[18] = (byte) 173;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 230,
      (byte) 103,
      (byte) 123,
      (byte) 99,
      (byte) 91,
      (byte) 192 /*0xC0*/,
      (byte) 53,
      (byte) 106,
      (byte) 239,
      (byte) 18,
      (byte) 227,
      (byte) 176 /*0xB0*/,
      (byte) 60,
      (byte) 249,
      (byte) 170,
      (byte) 113,
      (byte) 125,
      (byte) 244,
      (byte) 193,
      (byte) 107,
      (byte) 163,
      (byte) 52,
      (byte) 207,
      (byte) 231,
      (byte) 98,
      (byte) 225,
      (byte) 56,
      (byte) 60,
      (byte) 104,
      (byte) 122,
      (byte) 178,
      (byte) 104,
      (byte) 48 /*0x30*/,
      (byte) 143,
      (byte) 184,
      (byte) 159,
      (byte) 110,
      (byte) 222,
      (byte) 220,
      (byte) 159,
      (byte) 118,
      (byte) 212,
      (byte) 168,
      (byte) 202,
      (byte) 242,
      (byte) 249,
      (byte) 7,
      (byte) 45
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13383(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 145,
      (byte) 132,
      (byte) 235,
      (byte) 148,
      (byte) 234,
      (byte) 175,
      (byte) 224 /*0xE0*/,
      (byte) 46,
      (byte) 97,
      (byte) 26,
      (byte) 168,
      (byte) 114,
      (byte) 203,
      (byte) 237,
      (byte) 169,
      (byte) 48 /*0x30*/,
      (byte) 123,
      (byte) 155,
      (byte) 9,
      (byte) 245,
      (byte) 95,
      (byte) 52,
      (byte) 194,
      (byte) 233,
      (byte) 247,
      (byte) 110,
      (byte) 161,
      (byte) 20,
      (byte) 54,
      (byte) 130,
      (byte) 201,
      (byte) 25,
      (byte) 244,
      (byte) 191,
      (byte) 174,
      (byte) 252,
      (byte) 238,
      (byte) 125,
      (byte) 11,
      (byte) 88,
      (byte) 145,
      (byte) 105,
      (byte) 212,
      (byte) 4,
      (byte) 148,
      (byte) 64 /*0x40*/,
      (byte) 148,
      (byte) 84
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[23] = (byte) 175;
    sourceArray2[1] = (byte) 47;
    sourceArray2[21] = (byte) 244;
    sourceArray2[47] = (byte) 131;
    sourceArray2[0] = (byte) 217;
    sourceArray2[5] = (byte) 102;
    sourceArray2[6] = (byte) 154;
    sourceArray2[28] = (byte) 194;
    sourceArray2[3] = (byte) 243;
    sourceArray2[12] = (byte) 196;
    sourceArray2[10] = (byte) 210;
    sourceArray2[41] = (byte) 206;
    sourceArray2[13] = (byte) 168;
    sourceArray2[7] = (byte) 55;
    sourceArray2[2] = (byte) 95;
    sourceArray2[15] = (byte) 109;
    sourceArray2[24] = (byte) 22;
    sourceArray2[17] = (byte) 12;
    sourceArray2[18] = (byte) 46;
    sourceArray2[19] = (byte) 70;
    sourceArray2[25] = (byte) 235;
    sourceArray2[40] = (byte) 223;
    sourceArray2[38] = (byte) 184;
    sourceArray2[42] = (byte) 106;
    sourceArray2[46] = (byte) 57;
    sourceArray2[8] = (byte) 14;
    sourceArray2[35] = (byte) 38;
    sourceArray2[22] = (byte) 137;
    sourceArray2[11] = (byte) 56;
    sourceArray2[29] = (byte) 181;
    sourceArray2[30] = (byte) 34;
    sourceArray2[31 /*0x1F*/] = (byte) 43;
    sourceArray2[4] = (byte) 57;
    sourceArray2[20] = (byte) 227;
    sourceArray2[34] = (byte) 29;
    sourceArray2[14] = (byte) 214;
    sourceArray2[36] = (byte) 146;
    sourceArray2[37] = (byte) 119;
    sourceArray2[9] = (byte) 132;
    sourceArray2[39] = (byte) 151;
    sourceArray2[16 /*0x10*/] = (byte) 205;
    sourceArray2[27] = (byte) 236;
    sourceArray2[33] = (byte) 45;
    sourceArray2[43] = (byte) 37;
    sourceArray2[44] = (byte) 14;
    sourceArray2[32 /*0x20*/] = (byte) 54;
    sourceArray2[45] = (byte) 140;
    sourceArray2[26] = (byte) 152;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13384(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[33] = (byte) 243;
    sourceArray1[38] = (byte) 200;
    sourceArray1[0] = (byte) 136;
    sourceArray1[21] = (byte) 203;
    sourceArray1[4] = (byte) 221;
    sourceArray1[16 /*0x10*/] = (byte) 253;
    sourceArray1[44] = (byte) 56;
    sourceArray1[7] = (byte) 226;
    sourceArray1[8] = (byte) 136;
    sourceArray1[9] = (byte) 253;
    sourceArray1[10] = (byte) 3;
    sourceArray1[1] = (byte) 21;
    sourceArray1[34] = (byte) 82;
    sourceArray1[13] = (byte) 161;
    sourceArray1[45] = (byte) 62;
    sourceArray1[3] = (byte) 250;
    sourceArray1[23] = (byte) 113;
    sourceArray1[27] = (byte) 102;
    sourceArray1[20] = (byte) 206;
    sourceArray1[5] = (byte) 46;
    sourceArray1[40] = (byte) 11;
    sourceArray1[36] = (byte) 165;
    sourceArray1[22] = (byte) 237;
    sourceArray1[11] = (byte) 173;
    sourceArray1[25] = (byte) 30;
    sourceArray1[14] = (byte) 192 /*0xC0*/;
    sourceArray1[26] = byte.MaxValue;
    sourceArray1[2] = (byte) 96 /*0x60*/;
    sourceArray1[28] = (byte) 157;
    sourceArray1[6] = (byte) 174;
    sourceArray1[30] = (byte) 107;
    sourceArray1[17] = (byte) 57;
    sourceArray1[32 /*0x20*/] = (byte) 79;
    sourceArray1[39] = (byte) 220;
    sourceArray1[18] = (byte) 69;
    sourceArray1[35] = (byte) 56;
    sourceArray1[24] = (byte) 86;
    sourceArray1[15] = (byte) 197;
    sourceArray1[12] = (byte) 229;
    sourceArray1[29] = (byte) 131;
    sourceArray1[37] = (byte) 0;
    sourceArray1[41] = (byte) 203;
    sourceArray1[42] = (byte) 183;
    sourceArray1[43] = (byte) 84;
    sourceArray1[31 /*0x1F*/] = (byte) 10;
    sourceArray1[19] = (byte) 213;
    sourceArray1[46] = (byte) 250;
    sourceArray1[47] = (byte) 238;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 52,
      (byte) 184,
      (byte) 28,
      (byte) 74,
      (byte) 0,
      (byte) 64 /*0x40*/,
      (byte) 163,
      (byte) 243,
      (byte) 195,
      (byte) 100,
      (byte) 178,
      (byte) 108,
      (byte) 171,
      (byte) 75,
      (byte) 166,
      (byte) 97,
      (byte) 119,
      (byte) 107,
      (byte) 162,
      (byte) 55,
      (byte) 173,
      (byte) 12,
      (byte) 141,
      (byte) 228,
      (byte) 118,
      (byte) 158,
      (byte) 224 /*0xE0*/,
      (byte) 17,
      (byte) 137,
      (byte) 208 /*0xD0*/,
      (byte) 160 /*0xA0*/,
      (byte) 242,
      (byte) 112 /*0x70*/,
      (byte) 189,
      (byte) 36,
      (byte) 157,
      (byte) 116,
      (byte) 22,
      (byte) 80 /*0x50*/,
      (byte) 145,
      (byte) 67,
      (byte) 161,
      (byte) 78,
      (byte) 90,
      (byte) 66,
      (byte) 121,
      (byte) 74,
      (byte) 153
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13385(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 9,
      (byte) 159,
      (byte) 3,
      (byte) 224 /*0xE0*/,
      (byte) 119,
      (byte) 75,
      (byte) 4,
      (byte) 101,
      (byte) 241,
      (byte) 169,
      (byte) 141,
      (byte) 182,
      (byte) 140,
      (byte) 156,
      (byte) 29,
      (byte) 108,
      (byte) 166,
      (byte) 245,
      (byte) 225,
      (byte) 91,
      (byte) 60,
      (byte) 242,
      (byte) 185,
      (byte) 118,
      (byte) 105,
      (byte) 160 /*0xA0*/,
      (byte) 102,
      (byte) 187,
      (byte) 7,
      (byte) 124,
      (byte) 96 /*0x60*/,
      (byte) 215,
      (byte) 249,
      (byte) 53,
      (byte) 25,
      (byte) 113,
      (byte) 216,
      (byte) 189,
      (byte) 152,
      (byte) 100,
      (byte) 152,
      (byte) 187,
      (byte) 13,
      (byte) 236,
      (byte) 131,
      (byte) 0,
      (byte) 207,
      (byte) 109
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 63 /*0x3F*/,
      (byte) 19,
      (byte) 67,
      (byte) 140,
      (byte) 175,
      (byte) 138,
      (byte) 210,
      (byte) 166,
      (byte) 69,
      (byte) 158,
      (byte) 161,
      (byte) 160 /*0xA0*/,
      (byte) 18,
      (byte) 131,
      (byte) 80 /*0x50*/,
      (byte) 37,
      (byte) 7,
      (byte) 195,
      (byte) 102,
      (byte) 122,
      (byte) 163,
      (byte) 67,
      (byte) 191,
      (byte) 10,
      (byte) 60,
      (byte) 180,
      (byte) 171,
      (byte) 131,
      (byte) 176 /*0xB0*/,
      (byte) 172,
      (byte) 134,
      (byte) 17,
      (byte) 69,
      (byte) 217,
      (byte) 7,
      (byte) 233,
      (byte) 110,
      (byte) 10,
      (byte) 118,
      (byte) 169,
      (byte) 123,
      (byte) 98,
      (byte) 84,
      (byte) 251,
      (byte) 73,
      (byte) 239,
      (byte) 55,
      (byte) 236
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[44];
    byte[] response2 = new byte[44];
    Array.Copy((Array) sc_13375.sspq, 0, (Array) numArray2, 0, 44);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13375.sspr, 0, (Array) numArray2, 0, 44);
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

  internal static int ssp_appserver_13386(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[18] = (byte) 30;
    sourceArray1[1] = (byte) 205;
    sourceArray1[35] = (byte) 36;
    sourceArray1[11] = (byte) 35;
    sourceArray1[29] = (byte) 155;
    sourceArray1[5] = (byte) 227;
    sourceArray1[6] = (byte) 194;
    sourceArray1[7] = (byte) 246;
    sourceArray1[8] = (byte) 96 /*0x60*/;
    sourceArray1[42] = (byte) 66;
    sourceArray1[38] = (byte) 14;
    sourceArray1[28] = (byte) 163;
    sourceArray1[12] = (byte) 50;
    sourceArray1[40] = (byte) 239;
    sourceArray1[27] = (byte) 72;
    sourceArray1[33] = (byte) 75;
    sourceArray1[16 /*0x10*/] = (byte) 5;
    sourceArray1[4] = (byte) 234;
    sourceArray1[45] = (byte) 109;
    sourceArray1[19] = (byte) 172;
    sourceArray1[20] = (byte) 217;
    sourceArray1[21] = (byte) 166;
    sourceArray1[22] = (byte) 191;
    sourceArray1[23] = (byte) 166;
    sourceArray1[24] = (byte) 198;
    sourceArray1[0] = (byte) 162;
    sourceArray1[15] = (byte) 14;
    sourceArray1[30] = (byte) 252;
    sourceArray1[26] = (byte) 33;
    sourceArray1[14] = (byte) 154;
    sourceArray1[10] = (byte) 32 /*0x20*/;
    sourceArray1[34] = (byte) 41;
    sourceArray1[32 /*0x20*/] = (byte) 35;
    sourceArray1[31 /*0x1F*/] = (byte) 116;
    sourceArray1[37] = (byte) 88;
    sourceArray1[13] = (byte) 25;
    sourceArray1[36] = (byte) 36;
    sourceArray1[25] = (byte) 127 /*0x7F*/;
    sourceArray1[2] = (byte) 68;
    sourceArray1[39] = (byte) 71;
    sourceArray1[9] = (byte) 99;
    sourceArray1[41] = (byte) 165;
    sourceArray1[44] = (byte) 245;
    sourceArray1[43] = (byte) 146;
    sourceArray1[17] = (byte) 235;
    sourceArray1[3] = (byte) 120;
    sourceArray1[46] = (byte) 207;
    sourceArray1[47] = (byte) 112 /*0x70*/;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[14] = (byte) 98;
    sourceArray2[9] = (byte) 43;
    sourceArray2[45] = (byte) 114;
    sourceArray2[3] = (byte) 185;
    sourceArray2[4] = (byte) 2;
    sourceArray2[32 /*0x20*/] = (byte) 243;
    sourceArray2[33] = (byte) 175;
    sourceArray2[18] = (byte) 212;
    sourceArray2[20] = (byte) 244;
    sourceArray2[21] = (byte) 109;
    sourceArray2[10] = (byte) 127 /*0x7F*/;
    sourceArray2[25] = (byte) 36;
    sourceArray2[12] = (byte) 109;
    sourceArray2[1] = (byte) 70;
    sourceArray2[44] = (byte) 60;
    sourceArray2[6] = (byte) 143;
    sourceArray2[16 /*0x10*/] = (byte) 209;
    sourceArray2[2] = (byte) 71;
    sourceArray2[28] = (byte) 61;
    sourceArray2[19] = (byte) 164;
    sourceArray2[11] = (byte) 148;
    sourceArray2[42] = (byte) 73;
    sourceArray2[34] = (byte) 127 /*0x7F*/;
    sourceArray2[23] = (byte) 212;
    sourceArray2[17] = (byte) 82;
    sourceArray2[24] = (byte) 92;
    sourceArray2[13] = (byte) 4;
    sourceArray2[27] = (byte) 192 /*0xC0*/;
    sourceArray2[7] = (byte) 61;
    sourceArray2[8] = (byte) 91;
    sourceArray2[30] = (byte) 217;
    sourceArray2[31 /*0x1F*/] = (byte) 203;
    sourceArray2[41] = (byte) 181;
    sourceArray2[15] = (byte) 16 /*0x10*/;
    sourceArray2[26] = (byte) 41;
    sourceArray2[35] = (byte) 19;
    sourceArray2[36] = (byte) 87;
    sourceArray2[37] = (byte) 223;
    sourceArray2[38] = (byte) 171;
    sourceArray2[39] = (byte) 216;
    sourceArray2[43] = (byte) 244;
    sourceArray2[40] = (byte) 79;
    sourceArray2[0] = (byte) 191;
    sourceArray2[29] = (byte) 69;
    sourceArray2[22] = (byte) 194;
    sourceArray2[5] = (byte) 26;
    sourceArray2[46] = (byte) 190;
    sourceArray2[47] = (byte) 230;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13387(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[46] = (byte) 45;
    sourceArray1[1] = (byte) 144 /*0x90*/;
    sourceArray1[47] = (byte) 226;
    sourceArray1[17] = (byte) 225;
    sourceArray1[23] = (byte) 201;
    sourceArray1[5] = (byte) 227;
    sourceArray1[6] = (byte) 223;
    sourceArray1[7] = (byte) 141;
    sourceArray1[21] = (byte) 224 /*0xE0*/;
    sourceArray1[9] = (byte) 220;
    sourceArray1[10] = (byte) 233;
    sourceArray1[11] = (byte) 9;
    sourceArray1[12] = (byte) 127 /*0x7F*/;
    sourceArray1[43] = (byte) 125;
    sourceArray1[4] = (byte) 42;
    sourceArray1[30] = (byte) 44;
    sourceArray1[16 /*0x10*/] = (byte) 175;
    sourceArray1[14] = (byte) 172;
    sourceArray1[18] = (byte) 135;
    sourceArray1[19] = (byte) 108;
    sourceArray1[20] = (byte) 242;
    sourceArray1[35] = (byte) 183;
    sourceArray1[22] = (byte) 138;
    sourceArray1[31 /*0x1F*/] = (byte) 202;
    sourceArray1[41] = (byte) 58;
    sourceArray1[2] = (byte) 106;
    sourceArray1[26] = (byte) 124;
    sourceArray1[15] = (byte) 171;
    sourceArray1[25] = (byte) 83;
    sourceArray1[38] = (byte) 214;
    sourceArray1[24] = (byte) 21;
    sourceArray1[44] = (byte) 52;
    sourceArray1[32 /*0x20*/] = (byte) 184;
    sourceArray1[33] = (byte) 80 /*0x50*/;
    sourceArray1[34] = (byte) 221;
    sourceArray1[27] = (byte) 16 /*0x10*/;
    sourceArray1[36] = (byte) 118;
    sourceArray1[28] = (byte) 91;
    sourceArray1[13] = (byte) 8;
    sourceArray1[42] = (byte) 114;
    sourceArray1[40] = (byte) 13;
    sourceArray1[39] = (byte) 130;
    sourceArray1[29] = (byte) 43;
    sourceArray1[0] = (byte) 115;
    sourceArray1[37] = (byte) 78;
    sourceArray1[45] = (byte) 198;
    sourceArray1[8] = (byte) 190;
    sourceArray1[3] = (byte) 152;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[45] = (byte) 52;
    sourceArray2[26] = (byte) 56;
    sourceArray2[21] = (byte) 80 /*0x50*/;
    sourceArray2[31 /*0x1F*/] = (byte) 156;
    sourceArray2[4] = (byte) 190;
    sourceArray2[5] = (byte) 50;
    sourceArray2[1] = (byte) 183;
    sourceArray2[7] = (byte) 245;
    sourceArray2[8] = (byte) 236;
    sourceArray2[9] = (byte) 79;
    sourceArray2[10] = (byte) 155;
    sourceArray2[43] = (byte) 233;
    sourceArray2[12] = (byte) 14;
    sourceArray2[13] = (byte) 206;
    sourceArray2[14] = (byte) 234;
    sourceArray2[25] = (byte) 178;
    sourceArray2[16 /*0x10*/] = (byte) 27;
    sourceArray2[17] = (byte) 171;
    sourceArray2[18] = (byte) 242;
    sourceArray2[41] = (byte) 224 /*0xE0*/;
    sourceArray2[46] = (byte) 108;
    sourceArray2[3] = (byte) 73;
    sourceArray2[36] = (byte) 131;
    sourceArray2[20] = (byte) 97;
    sourceArray2[2] = (byte) 55;
    sourceArray2[32 /*0x20*/] = (byte) 7;
    sourceArray2[23] = (byte) 154;
    sourceArray2[27] = (byte) 234;
    sourceArray2[22] = (byte) 110;
    sourceArray2[29] = (byte) 18;
    sourceArray2[30] = (byte) 40;
    sourceArray2[24] = (byte) 146;
    sourceArray2[37] = (byte) 245;
    sourceArray2[33] = (byte) 38;
    sourceArray2[34] = (byte) 119;
    sourceArray2[35] = (byte) 69;
    sourceArray2[11] = (byte) 67;
    sourceArray2[6] = (byte) 195;
    sourceArray2[38] = (byte) 131;
    sourceArray2[39] = (byte) 50;
    sourceArray2[40] = (byte) 252;
    sourceArray2[0] = (byte) 218;
    sourceArray2[42] = (byte) 120;
    sourceArray2[28] = (byte) 153;
    sourceArray2[19] = (byte) 250;
    sourceArray2[44] = (byte) 73;
    sourceArray2[15] = (byte) 1;
    sourceArray2[47] = (byte) 175;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[42];
    byte[] response2 = new byte[42];
    Array.Copy((Array) sc_13375.sspq, 44, (Array) numArray2, 0, 42);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13375.sspr, 44, (Array) numArray2, 0, 42);
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

  internal static string ssp_appserver_13389()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10];
      numArray2[8] = (byte) 145;
      numArray2[7] = (byte) 141;
      numArray2[4] = (byte) 89;
      numArray2[3] = (byte) 208 /*0xD0*/;
      numArray2[1] = (byte) 205;
      numArray2[5] = (byte) 133;
      numArray2[6] = (byte) 246;
      numArray2[2] = (byte) 43;
      numArray2[0] = (byte) 9;
      numArray2[9] = (byte) 250;
      byte[] numArray3 = new byte[10];
      numArray3[3] = (byte) 28;
      numArray3[1] = (byte) 189;
      numArray3[2] = (byte) 15;
      numArray3[0] = (byte) 20;
      numArray3[7] = (byte) 159;
      numArray3[9] = (byte) 228;
      numArray3[6] = (byte) 131;
      numArray3[4] = (byte) 74;
      numArray3[5] = (byte) 248;
      numArray3[8] = (byte) 222;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10]
    {
      (byte) 138,
      (byte) 225,
      (byte) 92,
      (byte) 21,
      (byte) 189,
      (byte) 229,
      (byte) 56,
      (byte) 43,
      (byte) 44,
      (byte) 39
    };
    byte[] numArray6 = new byte[10]
    {
      (byte) 153,
      (byte) 236,
      (byte) 43,
      (byte) 186,
      (byte) 127 /*0x7F*/,
      (byte) 122,
      (byte) 137,
      (byte) 54,
      (byte) 159,
      (byte) 84
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static int ssp_appserver_13390(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 234,
      (byte) 47,
      (byte) 248,
      (byte) 138,
      (byte) 229,
      (byte) 2,
      (byte) 145,
      (byte) 233,
      (byte) 198,
      (byte) 5,
      (byte) 226,
      (byte) 220,
      (byte) 251,
      (byte) 6,
      (byte) 205,
      (byte) 248,
      (byte) 131,
      (byte) 131,
      (byte) 186,
      (byte) 57,
      (byte) 130,
      (byte) 42,
      (byte) 137,
      (byte) 70,
      (byte) 212,
      (byte) 182,
      (byte) 174,
      (byte) 65,
      (byte) 248,
      (byte) 114,
      (byte) 199,
      (byte) 38,
      (byte) 132,
      (byte) 83,
      (byte) 5,
      (byte) 143,
      (byte) 160 /*0xA0*/,
      (byte) 236,
      (byte) 210,
      (byte) 125,
      (byte) 249,
      (byte) 248,
      (byte) 149,
      (byte) 15,
      (byte) 218,
      (byte) 9,
      (byte) 61,
      (byte) 36
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 86,
      (byte) 71,
      (byte) 35,
      (byte) 0,
      (byte) 34,
      (byte) 122,
      (byte) 133,
      (byte) 125,
      (byte) 128 /*0x80*/,
      (byte) 177,
      (byte) 244,
      (byte) 146,
      (byte) 232,
      (byte) 61,
      (byte) 184,
      (byte) 194,
      (byte) 108,
      (byte) 102,
      (byte) 6,
      (byte) 111,
      (byte) 76,
      (byte) 56,
      (byte) 142,
      (byte) 34,
      (byte) 253,
      (byte) 26,
      (byte) 99,
      (byte) 2,
      (byte) 53,
      (byte) 134,
      (byte) 13,
      (byte) 111,
      (byte) 207,
      (byte) 130,
      (byte) 3,
      (byte) 143,
      (byte) 182,
      (byte) 247,
      (byte) 180,
      (byte) 20,
      (byte) 25,
      (byte) 95,
      (byte) 92,
      (byte) 131,
      (byte) 182,
      (byte) 204,
      (byte) 220,
      (byte) 80 /*0x50*/
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13391(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 191,
      (byte) 180,
      (byte) 191,
      (byte) 166,
      (byte) 177,
      (byte) 181,
      (byte) 46,
      (byte) 83,
      (byte) 79,
      (byte) 240 /*0xF0*/,
      (byte) 84,
      (byte) 61,
      (byte) 187,
      (byte) 87,
      (byte) 167,
      (byte) 1,
      (byte) 62,
      (byte) 87,
      (byte) 151,
      (byte) 176 /*0xB0*/,
      (byte) 61,
      (byte) 64 /*0x40*/,
      (byte) 137,
      (byte) 148,
      (byte) 122,
      (byte) 108,
      (byte) 203,
      (byte) 121,
      (byte) 137,
      (byte) 227,
      (byte) 200,
      (byte) 96 /*0x60*/,
      (byte) 238,
      (byte) 26,
      (byte) 197,
      (byte) 159,
      (byte) 69,
      (byte) 233,
      (byte) 238,
      (byte) 54,
      (byte) 164,
      (byte) 42,
      (byte) 169,
      (byte) 7,
      (byte) 65,
      (byte) 209,
      (byte) 30,
      (byte) 177
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[32 /*0x20*/] = (byte) 101;
    sourceArray2[4] = (byte) 220;
    sourceArray2[40] = (byte) 46;
    sourceArray2[37] = (byte) 35;
    sourceArray2[14] = (byte) 151;
    sourceArray2[5] = (byte) 89;
    sourceArray2[22] = (byte) 96 /*0x60*/;
    sourceArray2[47] = (byte) 117;
    sourceArray2[8] = (byte) 240 /*0xF0*/;
    sourceArray2[45] = (byte) 176 /*0xB0*/;
    sourceArray2[10] = (byte) 221;
    sourceArray2[44] = (byte) 4;
    sourceArray2[11] = (byte) 213;
    sourceArray2[13] = (byte) 15;
    sourceArray2[23] = (byte) 142;
    sourceArray2[18] = (byte) 199;
    sourceArray2[16 /*0x10*/] = (byte) 51;
    sourceArray2[25] = (byte) 77;
    sourceArray2[9] = (byte) 229;
    sourceArray2[19] = (byte) 58;
    sourceArray2[39] = (byte) 129;
    sourceArray2[35] = (byte) 176 /*0xB0*/;
    sourceArray2[15] = (byte) 216;
    sourceArray2[7] = (byte) 206;
    sourceArray2[24] = (byte) 32 /*0x20*/;
    sourceArray2[3] = (byte) 205;
    sourceArray2[2] = (byte) 191;
    sourceArray2[27] = (byte) 57;
    sourceArray2[28] = (byte) 58;
    sourceArray2[17] = (byte) 64 /*0x40*/;
    sourceArray2[1] = (byte) 143;
    sourceArray2[31 /*0x1F*/] = (byte) 240 /*0xF0*/;
    sourceArray2[21] = (byte) 126;
    sourceArray2[33] = (byte) 86;
    sourceArray2[34] = (byte) 242;
    sourceArray2[20] = (byte) 60;
    sourceArray2[36] = (byte) 79;
    sourceArray2[6] = (byte) 183;
    sourceArray2[38] = (byte) 156;
    sourceArray2[12] = (byte) 185;
    sourceArray2[26] = (byte) 215;
    sourceArray2[0] = (byte) 231;
    sourceArray2[42] = (byte) 144 /*0x90*/;
    sourceArray2[43] = (byte) 188;
    sourceArray2[29] = (byte) 159;
    sourceArray2[41] = (byte) 106;
    sourceArray2[46] = (byte) 120;
    sourceArray2[30] = (byte) 205;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
