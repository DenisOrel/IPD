// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_12465
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_12465
{
  private static byte[] sspq = new byte[211]
  {
    (byte) 92,
    (byte) 178,
    (byte) 87,
    (byte) 190,
    (byte) 36,
    (byte) 187,
    (byte) 93,
    (byte) 175,
    (byte) 144 /*0x90*/,
    (byte) 143,
    (byte) 181,
    (byte) 49,
    (byte) 216,
    (byte) 228,
    (byte) 113,
    (byte) 200,
    (byte) 130,
    (byte) 16 /*0x10*/,
    (byte) 25,
    (byte) 225,
    (byte) 230,
    (byte) 165,
    (byte) 4,
    (byte) 235,
    (byte) 75,
    (byte) 136,
    (byte) 115,
    (byte) 87,
    (byte) 88,
    (byte) 102,
    (byte) 108,
    (byte) 21,
    (byte) 210,
    (byte) 228,
    (byte) 134,
    (byte) 109,
    (byte) 127 /*0x7F*/,
    (byte) 207,
    (byte) 42,
    (byte) 129,
    (byte) 212,
    (byte) 187,
    (byte) 155,
    (byte) 100,
    (byte) 211,
    (byte) 12,
    (byte) 245,
    (byte) 170,
    (byte) 100,
    (byte) 186,
    (byte) 176 /*0xB0*/,
    (byte) 63 /*0x3F*/,
    (byte) 9,
    (byte) 149,
    (byte) 175,
    (byte) 87,
    (byte) 232,
    (byte) 221,
    (byte) 111,
    (byte) 152,
    (byte) 234,
    (byte) 104,
    (byte) 59,
    (byte) 134,
    (byte) 121,
    (byte) 191,
    (byte) 135,
    (byte) 145,
    (byte) 25,
    (byte) 161,
    (byte) 126,
    (byte) 0,
    (byte) 197,
    (byte) 100,
    (byte) 17,
    (byte) 165,
    (byte) 44,
    (byte) 131,
    (byte) 222,
    (byte) 28,
    (byte) 211,
    (byte) 149,
    (byte) 191,
    (byte) 83,
    (byte) 79,
    (byte) 137,
    (byte) 142,
    (byte) 13,
    (byte) 198,
    (byte) 108,
    (byte) 89,
    (byte) 84,
    (byte) 232,
    (byte) 43,
    (byte) 244,
    (byte) 70,
    (byte) 113,
    (byte) 20,
    (byte) 69,
    (byte) 60,
    (byte) 177,
    (byte) 107,
    (byte) 63 /*0x3F*/,
    (byte) 198,
    (byte) 205,
    (byte) 4,
    (byte) 197,
    (byte) 179,
    (byte) 26,
    (byte) 162,
    (byte) 37,
    (byte) 143,
    (byte) 138,
    (byte) 248,
    (byte) 106,
    (byte) 3,
    (byte) 26,
    (byte) 1,
    (byte) 10,
    (byte) 91,
    (byte) 69,
    (byte) 42,
    (byte) 192 /*0xC0*/,
    (byte) 61,
    (byte) 190,
    (byte) 111,
    (byte) 219,
    (byte) 137,
    (byte) 139,
    (byte) 227,
    (byte) 154,
    (byte) 98,
    (byte) 64 /*0x40*/,
    (byte) 34,
    (byte) 160 /*0xA0*/,
    (byte) 147,
    (byte) 91,
    (byte) 191,
    (byte) 96 /*0x60*/,
    (byte) 235,
    (byte) 254,
    (byte) 164,
    (byte) 175,
    (byte) 213,
    (byte) 53,
    (byte) 157,
    (byte) 250,
    (byte) 145,
    (byte) 83,
    (byte) 109,
    (byte) 54,
    (byte) 254,
    (byte) 246,
    (byte) 126,
    (byte) 189,
    (byte) 128 /*0x80*/,
    (byte) 197,
    (byte) 88,
    (byte) 12,
    (byte) 23,
    (byte) 60,
    (byte) 46,
    (byte) 242,
    (byte) 146,
    (byte) 37,
    (byte) 142,
    (byte) 198,
    (byte) 126,
    (byte) 170,
    (byte) 231,
    (byte) 182,
    (byte) 82,
    (byte) 81,
    (byte) 168,
    (byte) 213,
    (byte) 183,
    (byte) 18,
    (byte) 189,
    (byte) 208 /*0xD0*/,
    (byte) 202,
    (byte) 123,
    (byte) 167,
    (byte) 39,
    (byte) 193,
    (byte) 56,
    (byte) 1,
    (byte) 152,
    (byte) 231,
    (byte) 51,
    (byte) 50,
    (byte) 64 /*0x40*/,
    (byte) 108,
    (byte) 101,
    (byte) 250,
    (byte) 93,
    (byte) 107,
    (byte) 25,
    (byte) 127 /*0x7F*/,
    (byte) 36,
    (byte) 250,
    (byte) 59,
    (byte) 45,
    (byte) 24,
    (byte) 167,
    (byte) 194,
    (byte) 169,
    (byte) 79,
    (byte) 131,
    (byte) 114,
    (byte) 35,
    (byte) 44
  };
  private static byte[] sspr = new byte[211]
  {
    (byte) 51,
    (byte) 24,
    (byte) 78,
    (byte) 156,
    (byte) 224 /*0xE0*/,
    (byte) 30,
    (byte) 231,
    (byte) 134,
    (byte) 204,
    (byte) 194,
    (byte) 144 /*0x90*/,
    (byte) 85,
    (byte) 222,
    (byte) 114,
    (byte) 46,
    (byte) 138,
    (byte) 224 /*0xE0*/,
    (byte) 140,
    (byte) 82,
    (byte) 63 /*0x3F*/,
    (byte) 106,
    (byte) 193,
    (byte) 127 /*0x7F*/,
    (byte) 201,
    (byte) 187,
    (byte) 237,
    (byte) 15,
    (byte) 91,
    (byte) 227,
    (byte) 184,
    (byte) 117,
    (byte) 156,
    (byte) 119,
    (byte) 82,
    (byte) 151,
    (byte) 183,
    (byte) 65,
    (byte) 10,
    (byte) 80 /*0x50*/,
    (byte) 149,
    (byte) 136,
    (byte) 124,
    (byte) 65,
    (byte) 66,
    (byte) 252,
    (byte) 48 /*0x30*/,
    (byte) 171,
    (byte) 210,
    (byte) 226,
    (byte) 148,
    (byte) 109,
    (byte) 163,
    (byte) 89,
    (byte) 101,
    (byte) 185,
    (byte) 52,
    (byte) 28,
    (byte) 164,
    (byte) 252,
    (byte) 8,
    (byte) 72,
    (byte) 145,
    (byte) 25,
    (byte) 221,
    (byte) 35,
    (byte) 130,
    (byte) 20,
    (byte) 103,
    (byte) 131,
    (byte) 135,
    (byte) 142,
    (byte) 222,
    (byte) 5,
    (byte) 198,
    (byte) 242,
    (byte) 248,
    (byte) 62,
    (byte) 197,
    (byte) 140,
    (byte) 218,
    (byte) 99,
    (byte) 209,
    (byte) 128 /*0x80*/,
    (byte) 94,
    (byte) 56,
    (byte) 221,
    (byte) 17,
    (byte) 132,
    (byte) 149,
    (byte) 13,
    (byte) 107,
    (byte) 205,
    (byte) 141,
    (byte) 91,
    (byte) 159,
    (byte) 19,
    (byte) 30,
    (byte) 101,
    (byte) 64 /*0x40*/,
    (byte) 49,
    (byte) 189,
    (byte) 47,
    (byte) 247,
    (byte) 0,
    (byte) 164,
    (byte) 148,
    (byte) 38,
    (byte) 8,
    (byte) 5,
    (byte) 91,
    (byte) 151,
    (byte) 85,
    (byte) 37,
    (byte) 1,
    (byte) 81,
    (byte) 112 /*0x70*/,
    (byte) 34,
    (byte) 46,
    (byte) 41,
    (byte) 76,
    (byte) 52,
    (byte) 55,
    (byte) 9,
    (byte) 247,
    (byte) 95,
    (byte) 61,
    (byte) 105,
    (byte) 20,
    (byte) 215,
    (byte) 75,
    (byte) 197,
    (byte) 111,
    (byte) 5,
    (byte) 211,
    (byte) 100,
    (byte) 134,
    (byte) 189,
    (byte) 128 /*0x80*/,
    (byte) 139,
    (byte) 115,
    (byte) 99,
    (byte) 78,
    (byte) 130,
    (byte) 133,
    (byte) 24,
    (byte) 11,
    (byte) 236,
    (byte) 214,
    (byte) 97,
    (byte) 118,
    (byte) 95,
    (byte) 113,
    (byte) 23,
    (byte) 157,
    (byte) 182,
    (byte) 172,
    (byte) 188,
    (byte) 73,
    (byte) 148,
    (byte) 207,
    (byte) 110,
    (byte) 176 /*0xB0*/,
    (byte) 252,
    (byte) 184,
    (byte) 52,
    (byte) 30,
    (byte) 170,
    (byte) 84,
    (byte) 12,
    (byte) 156,
    (byte) 187,
    (byte) 101,
    (byte) 159,
    (byte) 73,
    (byte) 16 /*0x10*/,
    (byte) 171,
    (byte) 116,
    (byte) 114,
    (byte) 201,
    (byte) 226,
    (byte) 28,
    (byte) 185,
    (byte) 82,
    (byte) 116,
    (byte) 141,
    (byte) 106,
    (byte) 78,
    (byte) 160 /*0xA0*/,
    (byte) 38,
    (byte) 212,
    (byte) 252,
    (byte) 47,
    (byte) 252,
    (byte) 144 /*0x90*/,
    (byte) 113,
    (byte) 91,
    (byte) 160 /*0xA0*/,
    (byte) 36,
    (byte) 186,
    (byte) 142,
    (byte) 39,
    (byte) 14,
    (byte) 80 /*0x50*/,
    (byte) 199,
    (byte) 46,
    (byte) 239,
    (byte) 157,
    (byte) 111,
    (byte) 183,
    (byte) 63 /*0x3F*/,
    (byte) 230
  };

  internal static string ssp_appserver_12466()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[95];
      byte[] numArray2 = new byte[55];
      numArray2[36] = (byte) 253;
      numArray2[19] = (byte) 168;
      numArray2[2] = (byte) 77;
      numArray2[7] = (byte) 166;
      numArray2[4] = (byte) 208 /*0xD0*/;
      numArray2[32 /*0x20*/] = (byte) 3;
      numArray2[35] = (byte) 36;
      numArray2[37] = (byte) 222;
      numArray2[25] = (byte) 79;
      numArray2[51] = (byte) 114;
      numArray2[10] = (byte) 38;
      numArray2[43] = (byte) 2;
      numArray2[12] = (byte) 88;
      numArray2[13] = (byte) 156;
      numArray2[33] = (byte) 129;
      numArray2[50] = (byte) 66;
      numArray2[1] = (byte) 94;
      numArray2[15] = (byte) 196;
      numArray2[18] = (byte) 63 /*0x3F*/;
      numArray2[22] = (byte) 62;
      numArray2[3] = (byte) 4;
      numArray2[21] = (byte) 64 /*0x40*/;
      numArray2[14] = (byte) 50;
      numArray2[23] = (byte) 0;
      numArray2[5] = (byte) 54;
      numArray2[11] = (byte) 166;
      numArray2[26] = (byte) 174;
      numArray2[27] = (byte) 240 /*0xF0*/;
      numArray2[28] = (byte) 209;
      numArray2[29] = (byte) 227;
      numArray2[30] = (byte) 186;
      numArray2[31 /*0x1F*/] = (byte) 236;
      numArray2[41] = (byte) 22;
      numArray2[39] = (byte) 234;
      numArray2[34] = (byte) 114;
      numArray2[53] = (byte) 104;
      numArray2[8] = (byte) 58;
      numArray2[16 /*0x10*/] = (byte) 146;
      numArray2[38] = (byte) 173;
      numArray2[0] = (byte) 196;
      numArray2[40] = (byte) 134;
      numArray2[24] = (byte) 182;
      numArray2[42] = (byte) 114;
      numArray2[9] = (byte) 76;
      numArray2[17] = (byte) 212;
      numArray2[45] = (byte) 128 /*0x80*/;
      numArray2[6] = (byte) 80 /*0x50*/;
      numArray2[47] = (byte) 140;
      numArray2[48 /*0x30*/] = (byte) 63 /*0x3F*/;
      numArray2[49] = (byte) 142;
      numArray2[20] = (byte) 106;
      numArray2[46] = (byte) 206;
      numArray2[52] = (byte) 128 /*0x80*/;
      numArray2[44] = (byte) 37;
      numArray2[54] = (byte) 107;
      byte[] numArray3 = new byte[55]
      {
        (byte) 9,
        (byte) 236,
        (byte) 217,
        (byte) 244,
        (byte) 158,
        (byte) 39,
        (byte) 179,
        (byte) 115,
        (byte) 53,
        (byte) 228,
        (byte) 232,
        (byte) 240 /*0xF0*/,
        (byte) 43,
        (byte) 65,
        (byte) 128 /*0x80*/,
        (byte) 223,
        (byte) 40,
        (byte) 87,
        (byte) 237,
        (byte) 128 /*0x80*/,
        (byte) 70,
        (byte) 138,
        (byte) 197,
        (byte) 219,
        (byte) 205,
        (byte) 156,
        (byte) 47,
        (byte) 67,
        (byte) 45,
        (byte) 136,
        (byte) 34,
        (byte) 186,
        (byte) 188,
        (byte) 211,
        (byte) 248,
        (byte) 4,
        (byte) 16 /*0x10*/,
        (byte) 122,
        (byte) 207,
        (byte) 169,
        (byte) 244,
        (byte) 120,
        (byte) 96 /*0x60*/,
        (byte) 178,
        (byte) 128 /*0x80*/,
        (byte) 174,
        (byte) 70,
        (byte) 0,
        (byte) 87,
        (byte) 72,
        (byte) 208 /*0xD0*/,
        (byte) 141,
        (byte) 37,
        (byte) 128 /*0x80*/,
        (byte) 126
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[40];
      numArray4[31 /*0x1F*/] = (byte) 253;
      numArray4[18] = (byte) 18;
      numArray4[2] = (byte) 0;
      numArray4[20] = (byte) 202;
      numArray4[4] = (byte) 174;
      numArray4[5] = (byte) 145;
      numArray4[6] = (byte) 181;
      numArray4[14] = (byte) 35;
      numArray4[7] = (byte) 73;
      numArray4[9] = (byte) 8;
      numArray4[10] = (byte) 193;
      numArray4[11] = (byte) 59;
      numArray4[34] = (byte) 61;
      numArray4[19] = (byte) 177;
      numArray4[27] = (byte) 18;
      numArray4[15] = (byte) 249;
      numArray4[28] = (byte) 251;
      numArray4[17] = (byte) 250;
      numArray4[8] = (byte) 205;
      numArray4[16 /*0x10*/] = (byte) 242;
      numArray4[37] = (byte) 252;
      numArray4[36] = (byte) 44;
      numArray4[3] = (byte) 101;
      numArray4[24] = (byte) 191;
      numArray4[29] = (byte) 65;
      numArray4[25] = (byte) 127 /*0x7F*/;
      numArray4[26] = (byte) 124;
      numArray4[21] = (byte) 239;
      numArray4[13] = (byte) 23;
      numArray4[39] = (byte) 176 /*0xB0*/;
      numArray4[30] = (byte) 84;
      numArray4[0] = (byte) 114;
      numArray4[32 /*0x20*/] = (byte) 56;
      numArray4[33] = (byte) 30;
      numArray4[23] = (byte) 137;
      numArray4[35] = (byte) 120;
      numArray4[22] = (byte) 88;
      numArray4[12] = (byte) 9;
      numArray4[38] = (byte) 120;
      numArray4[1] = (byte) 23;
      byte[] numArray5 = new byte[40]
      {
        (byte) 246,
        (byte) 25,
        (byte) 227,
        (byte) 11,
        (byte) 155,
        (byte) 146,
        (byte) 51,
        (byte) 141,
        (byte) 249,
        (byte) 108,
        (byte) 39,
        (byte) 117,
        (byte) 242,
        (byte) 75,
        (byte) 180,
        (byte) 178,
        (byte) 166,
        (byte) 67,
        (byte) 131,
        (byte) 24,
        (byte) 237,
        (byte) 71,
        (byte) 180,
        (byte) 245,
        (byte) 47,
        (byte) 133,
        (byte) 29,
        (byte) 8,
        (byte) 86,
        (byte) 106,
        (byte) 35,
        (byte) 199,
        (byte) 167,
        (byte) 2,
        (byte) 66,
        (byte) 204,
        (byte) 19,
        (byte) 89,
        (byte) 143,
        (byte) 101
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 40);
      for (int index = 0; index < 40; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[95];
    byte[] numArray7 = new byte[55];
    numArray7[39] = (byte) 231;
    numArray7[48 /*0x30*/] = (byte) 64 /*0x40*/;
    numArray7[18] = (byte) 60;
    numArray7[5] = (byte) 3;
    numArray7[19] = (byte) 214;
    numArray7[2] = (byte) 8;
    numArray7[6] = (byte) 186;
    numArray7[7] = (byte) 185;
    numArray7[28] = (byte) 59;
    numArray7[44] = (byte) 55;
    numArray7[3] = (byte) 247;
    numArray7[20] = (byte) 193;
    numArray7[12] = (byte) 194;
    numArray7[8] = (byte) 182;
    numArray7[53] = (byte) 245;
    numArray7[22] = (byte) 66;
    numArray7[27] = (byte) 177;
    numArray7[17] = (byte) 35;
    numArray7[25] = (byte) 184;
    numArray7[45] = (byte) 198;
    numArray7[16 /*0x10*/] = (byte) 112 /*0x70*/;
    numArray7[14] = (byte) 144 /*0x90*/;
    numArray7[13] = (byte) 218;
    numArray7[1] = (byte) 8;
    numArray7[24] = (byte) 164;
    numArray7[34] = (byte) 164;
    numArray7[26] = (byte) 121;
    numArray7[15] = (byte) 33;
    numArray7[40] = (byte) 31 /*0x1F*/;
    numArray7[29] = (byte) 33;
    numArray7[21] = (byte) 133;
    numArray7[10] = (byte) 74;
    numArray7[32 /*0x20*/] = (byte) 72;
    numArray7[51] = (byte) 117;
    numArray7[50] = (byte) 227;
    numArray7[35] = (byte) 131;
    numArray7[36] = (byte) 171;
    numArray7[33] = (byte) 252;
    numArray7[38] = (byte) 207;
    numArray7[9] = (byte) 86;
    numArray7[23] = (byte) 202;
    numArray7[47] = (byte) 200;
    numArray7[42] = (byte) 22;
    numArray7[43] = (byte) 159;
    numArray7[37] = (byte) 205;
    numArray7[31 /*0x1F*/] = (byte) 214;
    numArray7[46] = (byte) 137;
    numArray7[4] = (byte) 51;
    numArray7[0] = (byte) 169;
    numArray7[49] = (byte) 98;
    numArray7[30] = (byte) 13;
    numArray7[52] = (byte) 27;
    numArray7[41] = (byte) 241;
    numArray7[11] = (byte) 155;
    numArray7[54] = (byte) 229;
    byte[] numArray8 = new byte[55]
    {
      (byte) 244,
      (byte) 105,
      (byte) 181,
      (byte) 147,
      (byte) 80 /*0x50*/,
      (byte) 80 /*0x50*/,
      (byte) 144 /*0x90*/,
      (byte) 29,
      (byte) 240 /*0xF0*/,
      (byte) 59,
      (byte) 44,
      (byte) 64 /*0x40*/,
      (byte) 191,
      (byte) 78,
      (byte) 27,
      (byte) 169,
      (byte) 150,
      (byte) 7,
      (byte) 193,
      (byte) 100,
      (byte) 246,
      (byte) 223,
      (byte) 143,
      (byte) 126,
      (byte) 121,
      (byte) 224 /*0xE0*/,
      (byte) 191,
      (byte) 142,
      (byte) 148,
      (byte) 247,
      (byte) 202,
      (byte) 11,
      (byte) 254,
      (byte) 167,
      (byte) 158,
      (byte) 114,
      (byte) 57,
      (byte) 9,
      (byte) 202,
      (byte) 236,
      (byte) 71,
      (byte) 163,
      (byte) 84,
      (byte) 26,
      (byte) 121,
      (byte) 230,
      (byte) 204,
      (byte) 240 /*0xF0*/,
      (byte) 248,
      (byte) 24,
      (byte) 109,
      (byte) 243,
      (byte) 11,
      (byte) 20,
      (byte) 25
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[40];
    numArray9[26] = (byte) 236;
    numArray9[17] = (byte) 159;
    numArray9[35] = (byte) 4;
    numArray9[3] = (byte) 217;
    numArray9[31 /*0x1F*/] = (byte) 2;
    numArray9[2] = (byte) 75;
    numArray9[32 /*0x20*/] = (byte) 203;
    numArray9[7] = (byte) 176 /*0xB0*/;
    numArray9[8] = (byte) 185;
    numArray9[30] = (byte) 20;
    numArray9[10] = (byte) 174;
    numArray9[19] = (byte) 237;
    numArray9[12] = (byte) 79;
    numArray9[0] = (byte) 169;
    numArray9[22] = (byte) 106;
    numArray9[15] = byte.MaxValue;
    numArray9[4] = (byte) 151;
    numArray9[18] = (byte) 92;
    numArray9[27] = (byte) 254;
    numArray9[23] = (byte) 107;
    numArray9[13] = (byte) 7;
    numArray9[21] = (byte) 208 /*0xD0*/;
    numArray9[20] = (byte) 76;
    numArray9[38] = (byte) 127 /*0x7F*/;
    numArray9[28] = (byte) 118;
    numArray9[25] = (byte) 61;
    numArray9[39] = (byte) 207;
    numArray9[14] = (byte) 41;
    numArray9[5] = (byte) 164;
    numArray9[29] = (byte) 243;
    numArray9[24] = (byte) 30;
    numArray9[6] = (byte) 86;
    numArray9[1] = (byte) 117;
    numArray9[9] = byte.MaxValue;
    numArray9[34] = (byte) 196;
    numArray9[16 /*0x10*/] = (byte) 247;
    numArray9[33] = (byte) 154;
    numArray9[37] = (byte) 131;
    numArray9[11] = (byte) 181;
    numArray9[36] = (byte) 8;
    byte[] numArray10 = new byte[40]
    {
      (byte) 171,
      (byte) 180,
      (byte) 99,
      (byte) 118,
      (byte) 82,
      (byte) 135,
      (byte) 157,
      (byte) 230,
      (byte) 137,
      (byte) 199,
      (byte) 69,
      (byte) 27,
      (byte) 242,
      (byte) 227,
      (byte) 162,
      (byte) 61,
      (byte) 192 /*0xC0*/,
      (byte) 231,
      (byte) 196,
      (byte) 38,
      (byte) 154,
      (byte) 81,
      (byte) 244,
      (byte) 134,
      (byte) 56,
      (byte) 209,
      (byte) 235,
      (byte) 217,
      (byte) 218,
      (byte) 117,
      (byte) 167,
      (byte) 234,
      (byte) 172,
      (byte) 31 /*0x1F*/,
      (byte) 70,
      (byte) 101,
      (byte) 28,
      (byte) 250,
      (byte) 177,
      (byte) 237
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 40);
    for (int index = 0; index < 40; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_12467()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 35,
        (byte) 117,
        (byte) 73,
        (byte) 4,
        (byte) 196,
        (byte) 235,
        (byte) 249,
        (byte) 40,
        (byte) 200,
        (byte) 37
      };
      byte[] numArray3 = new byte[10];
      numArray3[7] = (byte) 36;
      numArray3[0] = (byte) 125;
      numArray3[1] = (byte) 1;
      numArray3[3] = (byte) 237;
      numArray3[5] = (byte) 179;
      numArray3[2] = (byte) 48 /*0x30*/;
      numArray3[6] = (byte) 61;
      numArray3[9] = (byte) 199;
      numArray3[8] = (byte) 159;
      numArray3[4] = (byte) 34;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10]
    {
      (byte) 43,
      (byte) 4,
      (byte) 46,
      (byte) 17,
      (byte) 20,
      (byte) 137,
      (byte) 120,
      (byte) 1,
      (byte) 176 /*0xB0*/,
      (byte) 112 /*0x70*/
    };
    byte[] numArray6 = new byte[10]
    {
      (byte) 6,
      (byte) 126,
      (byte) 123,
      (byte) 55,
      (byte) 235,
      (byte) 53,
      (byte) 176 /*0xB0*/,
      (byte) 111,
      (byte) 137,
      (byte) 179
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12468()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[250];
      byte[] numArray2 = new byte[55];
      numArray2[49] = (byte) 75;
      numArray2[46] = (byte) 154;
      numArray2[2] = (byte) 87;
      numArray2[3] = (byte) 42;
      numArray2[4] = (byte) 207;
      numArray2[5] = (byte) 170;
      numArray2[6] = (byte) 250;
      numArray2[7] = (byte) 116;
      numArray2[8] = (byte) 241;
      numArray2[9] = (byte) 60;
      numArray2[10] = (byte) 133;
      numArray2[19] = (byte) 245;
      numArray2[39] = (byte) 177;
      numArray2[13] = (byte) 79;
      numArray2[25] = (byte) 158;
      numArray2[15] = (byte) 100;
      numArray2[11] = (byte) 109;
      numArray2[17] = (byte) 43;
      numArray2[18] = (byte) 199;
      numArray2[16 /*0x10*/] = (byte) 246;
      numArray2[20] = (byte) 29;
      numArray2[21] = (byte) 0;
      numArray2[35] = (byte) 62;
      numArray2[47] = (byte) 41;
      numArray2[24] = (byte) 228;
      numArray2[52] = (byte) 219;
      numArray2[50] = (byte) 122;
      numArray2[12] = (byte) 238;
      numArray2[28] = (byte) 104;
      numArray2[29] = (byte) 96 /*0x60*/;
      numArray2[43] = (byte) 242;
      numArray2[48 /*0x30*/] = (byte) 216;
      numArray2[26] = (byte) 80 /*0x50*/;
      numArray2[33] = (byte) 5;
      numArray2[34] = (byte) 13;
      numArray2[31 /*0x1F*/] = (byte) 158;
      numArray2[37] = (byte) 198;
      numArray2[27] = (byte) 36;
      numArray2[38] = (byte) 90;
      numArray2[23] = (byte) 217;
      numArray2[0] = (byte) 229;
      numArray2[40] = (byte) 133;
      numArray2[54] = (byte) 69;
      numArray2[1] = (byte) 240 /*0xF0*/;
      numArray2[41] = (byte) 182;
      numArray2[45] = (byte) 167;
      numArray2[14] = (byte) 41;
      numArray2[44] = (byte) 140;
      numArray2[30] = (byte) 134;
      numArray2[36] = (byte) 74;
      numArray2[22] = (byte) 149;
      numArray2[51] = (byte) 185;
      numArray2[32 /*0x20*/] = (byte) 18;
      numArray2[53] = (byte) 128 /*0x80*/;
      numArray2[42] = (byte) 18;
      byte[] numArray3 = new byte[55]
      {
        (byte) 74,
        (byte) 5,
        (byte) 21,
        (byte) 217,
        (byte) 134,
        (byte) 184,
        (byte) 164,
        (byte) 212,
        (byte) 65,
        (byte) 43,
        (byte) 167,
        (byte) 21,
        (byte) 221,
        (byte) 19,
        (byte) 220,
        (byte) 241,
        (byte) 27,
        (byte) 169,
        (byte) 175,
        (byte) 111,
        (byte) 141,
        (byte) 77,
        (byte) 97,
        (byte) 92,
        (byte) 78,
        (byte) 8,
        (byte) 191,
        (byte) 61,
        (byte) 229,
        (byte) 79,
        (byte) 31 /*0x1F*/,
        (byte) 251,
        (byte) 160 /*0xA0*/,
        (byte) 123,
        (byte) 148,
        (byte) 83,
        (byte) 214,
        (byte) 244,
        (byte) 72,
        (byte) 62,
        (byte) 79,
        (byte) 213,
        (byte) 2,
        (byte) 198,
        (byte) 105,
        (byte) 117,
        (byte) 94,
        (byte) 201,
        (byte) 19,
        (byte) 81,
        (byte) 51,
        (byte) 211,
        (byte) 18,
        (byte) 219,
        (byte) 31 /*0x1F*/
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55];
      numArray4[10] = (byte) 41;
      numArray4[53] = (byte) 139;
      numArray4[2] = (byte) 45;
      numArray4[3] = (byte) 51;
      numArray4[41] = (byte) 82;
      numArray4[22] = (byte) 241;
      numArray4[5] = (byte) 240 /*0xF0*/;
      numArray4[27] = (byte) 154;
      numArray4[17] = (byte) 193;
      numArray4[47] = (byte) 226;
      numArray4[24] = (byte) 64 /*0x40*/;
      numArray4[21] = (byte) 166;
      numArray4[16 /*0x10*/] = (byte) 192 /*0xC0*/;
      numArray4[38] = (byte) 37;
      numArray4[35] = (byte) 120;
      numArray4[15] = (byte) 167;
      numArray4[44] = (byte) 136;
      numArray4[40] = (byte) 216;
      numArray4[48 /*0x30*/] = (byte) 157;
      numArray4[19] = (byte) 107;
      numArray4[20] = (byte) 102;
      numArray4[0] = (byte) 49;
      numArray4[4] = (byte) 137;
      numArray4[11] = (byte) 25;
      numArray4[54] = (byte) 83;
      numArray4[25] = (byte) 94;
      numArray4[26] = (byte) 235;
      numArray4[36] = (byte) 252;
      numArray4[28] = (byte) 227;
      numArray4[6] = (byte) 170;
      numArray4[1] = (byte) 8;
      numArray4[31 /*0x1F*/] = (byte) 225;
      numArray4[30] = (byte) 129;
      numArray4[13] = (byte) 78;
      numArray4[12] = (byte) 7;
      numArray4[18] = (byte) 188;
      numArray4[52] = (byte) 33;
      numArray4[37] = (byte) 35;
      numArray4[32 /*0x20*/] = (byte) 49;
      numArray4[39] = (byte) 185;
      numArray4[29] = (byte) 254;
      numArray4[34] = (byte) 154;
      numArray4[9] = (byte) 201;
      numArray4[43] = (byte) 130;
      numArray4[8] = (byte) 34;
      numArray4[45] = (byte) 205;
      numArray4[46] = (byte) 229;
      numArray4[33] = (byte) 41;
      numArray4[7] = (byte) 233;
      numArray4[49] = (byte) 65;
      numArray4[50] = (byte) 4;
      numArray4[51] = (byte) 78;
      numArray4[23] = (byte) 81;
      numArray4[42] = (byte) 131;
      numArray4[14] = (byte) 174;
      byte[] numArray5 = new byte[55];
      numArray5[12] = (byte) 169;
      numArray5[19] = (byte) 229;
      numArray5[17] = (byte) 186;
      numArray5[22] = (byte) 62;
      numArray5[4] = (byte) 197;
      numArray5[45] = (byte) 34;
      numArray5[6] = (byte) 143;
      numArray5[7] = (byte) 128 /*0x80*/;
      numArray5[8] = (byte) 1;
      numArray5[50] = (byte) 169;
      numArray5[10] = (byte) 241;
      numArray5[0] = (byte) 186;
      numArray5[36] = (byte) 186;
      numArray5[23] = (byte) 97;
      numArray5[14] = (byte) 228;
      numArray5[15] = (byte) 34;
      numArray5[33] = (byte) 167;
      numArray5[47] = (byte) 110;
      numArray5[24] = (byte) 223;
      numArray5[54] = (byte) 181;
      numArray5[18] = (byte) 221;
      numArray5[16 /*0x10*/] = (byte) 179;
      numArray5[21] = (byte) 89;
      numArray5[53] = (byte) 187;
      numArray5[49] = (byte) 43;
      numArray5[25] = (byte) 95;
      numArray5[11] = (byte) 82;
      numArray5[27] = (byte) 194;
      numArray5[28] = (byte) 19;
      numArray5[29] = (byte) 47;
      numArray5[46] = (byte) 132;
      numArray5[31 /*0x1F*/] = (byte) 203;
      numArray5[32 /*0x20*/] = (byte) 141;
      numArray5[1] = (byte) 153;
      numArray5[9] = (byte) 222;
      numArray5[35] = (byte) 74;
      numArray5[38] = (byte) 46;
      numArray5[37] = (byte) 211;
      numArray5[40] = (byte) 36;
      numArray5[2] = (byte) 155;
      numArray5[34] = (byte) 150;
      numArray5[41] = (byte) 34;
      numArray5[42] = (byte) 183;
      numArray5[43] = (byte) 220;
      numArray5[44] = (byte) 213;
      numArray5[39] = (byte) 232;
      numArray5[52] = (byte) 195;
      numArray5[5] = (byte) 96 /*0x60*/;
      numArray5[48 /*0x30*/] = (byte) 23;
      numArray5[26] = (byte) 40;
      numArray5[30] = (byte) 239;
      numArray5[51] = (byte) 198;
      numArray5[13] = (byte) 121;
      numArray5[3] = (byte) 82;
      numArray5[20] = (byte) 248;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[55]
      {
        (byte) 136,
        (byte) 243,
        (byte) 66,
        (byte) 32 /*0x20*/,
        (byte) 124,
        (byte) 86,
        (byte) 151,
        (byte) 147,
        (byte) 232,
        (byte) 147,
        (byte) 112 /*0x70*/,
        (byte) 0,
        (byte) 28,
        (byte) 122,
        (byte) 188,
        (byte) 90,
        (byte) 146,
        (byte) 21,
        (byte) 221,
        (byte) 141,
        (byte) 13,
        (byte) 210,
        (byte) 232,
        (byte) 112 /*0x70*/,
        (byte) 81,
        (byte) 4,
        (byte) 0,
        (byte) 150,
        (byte) 112 /*0x70*/,
        (byte) 207,
        (byte) 128 /*0x80*/,
        (byte) 30,
        (byte) 0,
        (byte) 67,
        (byte) 134,
        (byte) 188,
        (byte) 4,
        (byte) 154,
        (byte) 57,
        (byte) 146,
        (byte) 157,
        (byte) 147,
        (byte) 206,
        (byte) 17,
        (byte) 143,
        (byte) 136,
        (byte) 117,
        (byte) 145,
        (byte) 63 /*0x3F*/,
        (byte) 181,
        (byte) 62,
        (byte) 24,
        (byte) 154,
        (byte) 23,
        (byte) 103
      };
      byte[] numArray7 = new byte[55];
      numArray7[6] = (byte) 42;
      numArray7[10] = (byte) 101;
      numArray7[20] = (byte) 200;
      numArray7[3] = (byte) 34;
      numArray7[52] = (byte) 215;
      numArray7[7] = (byte) 89;
      numArray7[8] = (byte) 188;
      numArray7[16 /*0x10*/] = (byte) 152;
      numArray7[5] = (byte) 52;
      numArray7[36] = (byte) 85;
      numArray7[0] = (byte) 253;
      numArray7[11] = (byte) 152;
      numArray7[29] = (byte) 57;
      numArray7[13] = (byte) 199;
      numArray7[18] = (byte) 41;
      numArray7[48 /*0x30*/] = (byte) 117;
      numArray7[4] = (byte) 187;
      numArray7[12] = (byte) 115;
      numArray7[43] = (byte) 223;
      numArray7[19] = (byte) 19;
      numArray7[31 /*0x1F*/] = (byte) 103;
      numArray7[15] = (byte) 125;
      numArray7[9] = (byte) 91;
      numArray7[22] = (byte) 116;
      numArray7[24] = (byte) 83;
      numArray7[25] = byte.MaxValue;
      numArray7[26] = (byte) 72;
      numArray7[42] = (byte) 67;
      numArray7[28] = (byte) 52;
      numArray7[23] = (byte) 231;
      numArray7[30] = (byte) 57;
      numArray7[14] = (byte) 52;
      numArray7[32 /*0x20*/] = (byte) 121;
      numArray7[33] = (byte) 113;
      numArray7[34] = (byte) 124;
      numArray7[27] = (byte) 182;
      numArray7[21] = (byte) 204;
      numArray7[50] = (byte) 127 /*0x7F*/;
      numArray7[38] = (byte) 68;
      numArray7[39] = (byte) 94;
      numArray7[35] = (byte) 242;
      numArray7[41] = (byte) 55;
      numArray7[2] = (byte) 47;
      numArray7[1] = (byte) 78;
      numArray7[51] = (byte) 40;
      numArray7[45] = (byte) 10;
      numArray7[46] = byte.MaxValue;
      numArray7[47] = (byte) 196;
      numArray7[40] = (byte) 150;
      numArray7[49] = (byte) 224 /*0xE0*/;
      numArray7[17] = (byte) 83;
      numArray7[44] = (byte) 208 /*0xD0*/;
      numArray7[37] = (byte) 40;
      numArray7[53] = (byte) 133;
      numArray7[54] = (byte) 1;
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[55]
      {
        (byte) 240 /*0xF0*/,
        (byte) 5,
        (byte) 233,
        (byte) 230,
        (byte) 169,
        (byte) 103,
        (byte) 136,
        (byte) 170,
        (byte) 205,
        (byte) 23,
        (byte) 252,
        (byte) 35,
        (byte) 79,
        (byte) 34,
        (byte) 44,
        (byte) 25,
        (byte) 15,
        (byte) 83,
        (byte) 9,
        (byte) 78,
        (byte) 216,
        (byte) 163,
        (byte) 64 /*0x40*/,
        (byte) 241,
        (byte) 210,
        (byte) 24,
        (byte) 91,
        (byte) 73,
        (byte) 43,
        (byte) 53,
        (byte) 193,
        (byte) 238,
        (byte) 29,
        (byte) 123,
        (byte) 67,
        (byte) 198,
        (byte) 216,
        (byte) 85,
        (byte) 243,
        (byte) 9,
        (byte) 44,
        (byte) 83,
        (byte) 112 /*0x70*/,
        (byte) 113,
        (byte) 159,
        (byte) 121,
        (byte) 239,
        (byte) 60,
        (byte) 26,
        (byte) 35,
        (byte) 64 /*0x40*/,
        (byte) 211,
        (byte) 227,
        (byte) 100,
        (byte) 91
      };
      byte[] numArray9 = new byte[55]
      {
        (byte) 69,
        (byte) 162,
        (byte) 234,
        (byte) 31 /*0x1F*/,
        (byte) 233,
        (byte) 7,
        (byte) 204,
        (byte) 125,
        (byte) 49,
        (byte) 205,
        (byte) 75,
        (byte) 20,
        (byte) 173,
        (byte) 248,
        (byte) 127 /*0x7F*/,
        (byte) 219,
        (byte) 153,
        (byte) 106,
        (byte) 125,
        (byte) 200,
        (byte) 2,
        (byte) 16 /*0x10*/,
        (byte) 211,
        (byte) 33,
        (byte) 232,
        (byte) 37,
        (byte) 194,
        (byte) 97,
        (byte) 246,
        (byte) 53,
        (byte) 62,
        (byte) 1,
        (byte) 83,
        (byte) 46,
        (byte) 209,
        (byte) 134,
        (byte) 234,
        (byte) 87,
        (byte) 21,
        (byte) 36,
        (byte) 221,
        (byte) 189,
        (byte) 143,
        (byte) 110,
        (byte) 167,
        (byte) 19,
        (byte) 253,
        (byte) 82,
        (byte) 41,
        (byte) 165,
        (byte) 213,
        (byte) 123,
        (byte) 62,
        (byte) 208 /*0xD0*/,
        (byte) 88
      };
      key.Query(true, 335, numArray8, numArray8);
      Array.Copy((Array) numArray8, 0, (Array) numArray1, 165, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 165] ^= numArray9[index];
      byte[] numArray10 = new byte[30]
      {
        (byte) 248,
        (byte) 19,
        (byte) 144 /*0x90*/,
        (byte) 11,
        (byte) 63 /*0x3F*/,
        (byte) 126,
        (byte) 152,
        (byte) 85,
        (byte) 13,
        (byte) 6,
        (byte) 211,
        (byte) 223,
        (byte) 101,
        (byte) 65,
        (byte) 185,
        (byte) 95,
        byte.MaxValue,
        (byte) 33,
        (byte) 133,
        (byte) 51,
        (byte) 206,
        (byte) 81,
        (byte) 89,
        (byte) 41,
        (byte) 31 /*0x1F*/,
        (byte) 47,
        (byte) 8,
        (byte) 244,
        (byte) 237,
        (byte) 11
      };
      byte[] numArray11 = new byte[30]
      {
        (byte) 30,
        (byte) 97,
        (byte) 56,
        (byte) 201,
        (byte) 209,
        (byte) 112 /*0x70*/,
        (byte) 144 /*0x90*/,
        (byte) 238,
        (byte) 216,
        (byte) 102,
        (byte) 219,
        (byte) 240 /*0xF0*/,
        (byte) 176 /*0xB0*/,
        (byte) 245,
        (byte) 11,
        (byte) 244,
        (byte) 198,
        (byte) 214,
        (byte) 46,
        (byte) 70,
        (byte) 130,
        (byte) 209,
        (byte) 172,
        (byte) 179,
        (byte) 190,
        (byte) 170,
        (byte) 173,
        (byte) 80 /*0x50*/,
        (byte) 17,
        (byte) 225
      };
      key.Query(true, 335, numArray10, numArray10);
      Array.Copy((Array) numArray10, 0, (Array) numArray1, 220, 30);
      for (int index = 0; index < 30; ++index)
        numArray1[index + 220] ^= numArray11[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray12 = new byte[250];
    byte[] numArray13 = new byte[55]
    {
      (byte) 1,
      (byte) 224 /*0xE0*/,
      (byte) 66,
      (byte) 99,
      (byte) 254,
      (byte) 236,
      (byte) 212,
      (byte) 215,
      (byte) 27,
      (byte) 146,
      (byte) 115,
      (byte) 27,
      (byte) 92,
      (byte) 181,
      (byte) 225,
      (byte) 135,
      (byte) 115,
      (byte) 228,
      (byte) 66,
      (byte) 214,
      (byte) 24,
      (byte) 246,
      (byte) 166,
      (byte) 186,
      (byte) 102,
      (byte) 236,
      (byte) 16 /*0x10*/,
      (byte) 118,
      (byte) 163,
      (byte) 103,
      (byte) 2,
      (byte) 76,
      (byte) 179,
      (byte) 233,
      (byte) 100,
      (byte) 97,
      (byte) 118,
      (byte) 252,
      (byte) 0,
      (byte) 52,
      (byte) 176 /*0xB0*/,
      (byte) 91,
      (byte) 178,
      (byte) 191,
      (byte) 230,
      (byte) 13,
      (byte) 194,
      (byte) 122,
      (byte) 38,
      (byte) 109,
      (byte) 122,
      (byte) 38,
      (byte) 118,
      (byte) 73,
      (byte) 221
    };
    byte[] numArray14 = new byte[55]
    {
      (byte) 247,
      (byte) 242,
      (byte) 84,
      (byte) 253,
      (byte) 184,
      (byte) 85,
      (byte) 175,
      (byte) 171,
      (byte) 232,
      (byte) 219,
      (byte) 3,
      (byte) 216,
      (byte) 195,
      (byte) 23,
      (byte) 193,
      (byte) 253,
      (byte) 237,
      (byte) 98,
      (byte) 103,
      (byte) 103,
      (byte) 212,
      (byte) 234,
      (byte) 174,
      (byte) 100,
      (byte) 250,
      (byte) 2,
      (byte) 119,
      (byte) 97,
      (byte) 168,
      (byte) 102,
      (byte) 99,
      (byte) 112 /*0x70*/,
      (byte) 125,
      (byte) 53,
      (byte) 252,
      (byte) 106,
      (byte) 25,
      (byte) 214,
      (byte) 120,
      (byte) 82,
      (byte) 240 /*0xF0*/,
      (byte) 196,
      (byte) 164,
      (byte) 234,
      (byte) 24,
      (byte) 33,
      (byte) 173,
      (byte) 231,
      (byte) 154,
      (byte) 95,
      (byte) 40,
      (byte) 231,
      (byte) 101,
      (byte) 71,
      (byte) 212
    };
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray12, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index] ^= numArray14[index];
    byte[] numArray15 = new byte[55]
    {
      (byte) 136,
      (byte) 252,
      (byte) 18,
      (byte) 220,
      (byte) 94,
      (byte) 52,
      (byte) 180,
      (byte) 143,
      (byte) 38,
      (byte) 104,
      (byte) 115,
      (byte) 80 /*0x50*/,
      (byte) 128 /*0x80*/,
      (byte) 16 /*0x10*/,
      (byte) 119,
      (byte) 227,
      (byte) 13,
      (byte) 38,
      (byte) 189,
      (byte) 68,
      (byte) 231,
      (byte) 211,
      (byte) 89,
      (byte) 41,
      (byte) 150,
      (byte) 79,
      (byte) 53,
      (byte) 237,
      (byte) 219,
      (byte) 214,
      (byte) 106,
      (byte) 11,
      (byte) 89,
      (byte) 39,
      (byte) 238,
      (byte) 0,
      (byte) 238,
      (byte) 88,
      (byte) 82,
      (byte) 39,
      (byte) 254,
      (byte) 47,
      (byte) 191,
      (byte) 165,
      (byte) 79,
      (byte) 41,
      (byte) 166,
      (byte) 93,
      (byte) 3,
      (byte) 67,
      (byte) 105,
      (byte) 35,
      (byte) 32 /*0x20*/,
      (byte) 198,
      (byte) 82
    };
    byte[] numArray16 = new byte[55]
    {
      (byte) 217,
      (byte) 100,
      (byte) 136,
      (byte) 170,
      (byte) 186,
      (byte) 14,
      (byte) 143,
      (byte) 225,
      (byte) 76,
      (byte) 149,
      (byte) 20,
      (byte) 142,
      (byte) 189,
      (byte) 34,
      (byte) 177,
      (byte) 254,
      (byte) 50,
      (byte) 192 /*0xC0*/,
      (byte) 96 /*0x60*/,
      (byte) 177,
      (byte) 179,
      (byte) 186,
      (byte) 79,
      byte.MaxValue,
      (byte) 137,
      (byte) 113,
      (byte) 93,
      (byte) 208 /*0xD0*/,
      (byte) 113,
      (byte) 244,
      (byte) 164,
      (byte) 139,
      (byte) 21,
      (byte) 138,
      (byte) 224 /*0xE0*/,
      (byte) 127 /*0x7F*/,
      (byte) 27,
      (byte) 244,
      (byte) 188,
      (byte) 249,
      (byte) 166,
      (byte) 55,
      (byte) 233,
      (byte) 206,
      (byte) 160 /*0xA0*/,
      (byte) 113,
      (byte) 197,
      (byte) 143,
      (byte) 80 /*0x50*/,
      (byte) 231,
      (byte) 53,
      (byte) 74,
      (byte) 253,
      (byte) 54,
      (byte) 166
    };
    key.Query(true, 335, numArray15, numArray15);
    Array.Copy((Array) numArray15, 0, (Array) numArray12, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index + 55] ^= numArray16[index];
    byte[] numArray17 = new byte[55]
    {
      (byte) 232,
      (byte) 70,
      (byte) 14,
      (byte) 254,
      (byte) 198,
      (byte) 179,
      (byte) 80 /*0x50*/,
      (byte) 253,
      (byte) 103,
      (byte) 247,
      (byte) 88,
      (byte) 33,
      (byte) 119,
      (byte) 245,
      (byte) 181,
      (byte) 211,
      (byte) 149,
      (byte) 70,
      (byte) 183,
      (byte) 20,
      (byte) 179,
      (byte) 218,
      (byte) 108,
      (byte) 169,
      (byte) 215,
      (byte) 234,
      (byte) 152,
      (byte) 108,
      (byte) 70,
      (byte) 235,
      (byte) 147,
      (byte) 213,
      (byte) 58,
      (byte) 92,
      (byte) 5,
      (byte) 17,
      (byte) 67,
      (byte) 41,
      (byte) 55,
      (byte) 124,
      (byte) 111,
      (byte) 167,
      (byte) 231,
      (byte) 49,
      (byte) 77,
      (byte) 60,
      (byte) 140,
      (byte) 77,
      (byte) 179,
      (byte) 79,
      (byte) 138,
      (byte) 127 /*0x7F*/,
      (byte) 155,
      (byte) 159,
      (byte) 240 /*0xF0*/
    };
    byte[] numArray18 = new byte[55];
    numArray18[0] = (byte) 227;
    numArray18[16 /*0x10*/] = (byte) 30;
    numArray18[2] = (byte) 134;
    numArray18[3] = (byte) 48 /*0x30*/;
    numArray18[4] = (byte) 80 /*0x50*/;
    numArray18[1] = (byte) 246;
    numArray18[39] = (byte) 54;
    numArray18[7] = (byte) 192 /*0xC0*/;
    numArray18[29] = (byte) 196;
    numArray18[43] = (byte) 3;
    numArray18[11] = (byte) 160 /*0xA0*/;
    numArray18[44] = (byte) 17;
    numArray18[12] = (byte) 219;
    numArray18[48 /*0x30*/] = (byte) 61;
    numArray18[14] = (byte) 224 /*0xE0*/;
    numArray18[23] = (byte) 158;
    numArray18[13] = (byte) 78;
    numArray18[17] = (byte) 85;
    numArray18[41] = (byte) 117;
    numArray18[19] = (byte) 246;
    numArray18[5] = (byte) 169;
    numArray18[36] = (byte) 53;
    numArray18[22] = (byte) 90;
    numArray18[27] = (byte) 41;
    numArray18[26] = (byte) 30;
    numArray18[6] = (byte) 195;
    numArray18[49] = (byte) 39;
    numArray18[20] = (byte) 4;
    numArray18[28] = (byte) 106;
    numArray18[46] = (byte) 156;
    numArray18[8] = (byte) 187;
    numArray18[10] = byte.MaxValue;
    numArray18[18] = (byte) 176 /*0xB0*/;
    numArray18[33] = (byte) 150;
    numArray18[53] = (byte) 246;
    numArray18[35] = (byte) 146;
    numArray18[34] = (byte) 64 /*0x40*/;
    numArray18[25] = (byte) 148;
    numArray18[51] = (byte) 116;
    numArray18[24] = (byte) 203;
    numArray18[40] = (byte) 177;
    numArray18[37] = (byte) 6;
    numArray18[47] = (byte) 157;
    numArray18[42] = (byte) 205;
    numArray18[21] = (byte) 190;
    numArray18[15] = (byte) 175;
    numArray18[45] = (byte) 87;
    numArray18[31 /*0x1F*/] = (byte) 159;
    numArray18[30] = (byte) 59;
    numArray18[32 /*0x20*/] = (byte) 125;
    numArray18[50] = (byte) 242;
    numArray18[9] = (byte) 113;
    numArray18[52] = (byte) 103;
    numArray18[38] = (byte) 233;
    numArray18[54] = (byte) 145;
    key.Query(true, 335, numArray17, numArray17);
    Array.Copy((Array) numArray17, 0, (Array) numArray12, 110, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index + 110] ^= numArray18[index];
    byte[] numArray19 = new byte[55]
    {
      (byte) 10,
      (byte) 139,
      (byte) 125,
      (byte) 208 /*0xD0*/,
      (byte) 42,
      (byte) 191,
      (byte) 28,
      (byte) 80 /*0x50*/,
      (byte) 73,
      (byte) 75,
      (byte) 173,
      (byte) 248,
      (byte) 108,
      (byte) 179,
      (byte) 106,
      (byte) 178,
      (byte) 217,
      (byte) 24,
      (byte) 26,
      (byte) 136,
      (byte) 109,
      (byte) 144 /*0x90*/,
      (byte) 69,
      (byte) 236,
      (byte) 236,
      (byte) 169,
      (byte) 56,
      (byte) 65,
      (byte) 162,
      (byte) 217,
      (byte) 159,
      (byte) 135,
      (byte) 220,
      (byte) 152,
      (byte) 210,
      (byte) 118,
      (byte) 221,
      (byte) 145,
      (byte) 97,
      (byte) 48 /*0x30*/,
      (byte) 78,
      (byte) 125,
      (byte) 129,
      (byte) 10,
      (byte) 195,
      (byte) 102,
      (byte) 163,
      (byte) 11,
      (byte) 180,
      (byte) 190,
      (byte) 135,
      (byte) 226,
      (byte) 202,
      (byte) 146,
      (byte) 49
    };
    byte[] numArray20 = new byte[55];
    numArray20[49] = (byte) 60;
    numArray20[1] = (byte) 245;
    numArray20[20] = (byte) 138;
    numArray20[53] = (byte) 70;
    numArray20[4] = (byte) 208 /*0xD0*/;
    numArray20[25] = (byte) 82;
    numArray20[6] = (byte) 249;
    numArray20[7] = (byte) 174;
    numArray20[52] = (byte) 239;
    numArray20[9] = (byte) 254;
    numArray20[8] = (byte) 17;
    numArray20[21] = (byte) 133;
    numArray20[32 /*0x20*/] = (byte) 176 /*0xB0*/;
    numArray20[13] = (byte) 68;
    numArray20[14] = (byte) 94;
    numArray20[15] = (byte) 129;
    numArray20[16 /*0x10*/] = (byte) 36;
    numArray20[50] = (byte) 99;
    numArray20[37] = (byte) 43;
    numArray20[24] = (byte) 218;
    numArray20[30] = (byte) 137;
    numArray20[19] = (byte) 193;
    numArray20[22] = (byte) 129;
    numArray20[23] = (byte) 80 /*0x50*/;
    numArray20[17] = (byte) 233;
    numArray20[5] = (byte) 166;
    numArray20[40] = (byte) 57;
    numArray20[11] = (byte) 96 /*0x60*/;
    numArray20[34] = (byte) 89;
    numArray20[41] = (byte) 134;
    numArray20[29] = (byte) 5;
    numArray20[31 /*0x1F*/] = (byte) 31 /*0x1F*/;
    numArray20[39] = (byte) 14;
    numArray20[33] = (byte) 52;
    numArray20[26] = (byte) 105;
    numArray20[35] = (byte) 46;
    numArray20[36] = (byte) 198;
    numArray20[0] = (byte) 118;
    numArray20[38] = (byte) 78;
    numArray20[18] = (byte) 30;
    numArray20[2] = (byte) 248;
    numArray20[54] = (byte) 242;
    numArray20[42] = (byte) 54;
    numArray20[43] = (byte) 69;
    numArray20[44] = (byte) 56;
    numArray20[45] = (byte) 88;
    numArray20[46] = (byte) 83;
    numArray20[47] = (byte) 46;
    numArray20[48 /*0x30*/] = (byte) 189;
    numArray20[27] = (byte) 252;
    numArray20[12] = (byte) 217;
    numArray20[51] = (byte) 197;
    numArray20[3] = (byte) 142;
    numArray20[10] = (byte) 133;
    numArray20[28] = (byte) 209;
    key.Query(true, 335, numArray19, numArray19);
    Array.Copy((Array) numArray19, 0, (Array) numArray12, 165, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index + 165] ^= numArray20[index];
    byte[] numArray21 = new byte[30]
    {
      (byte) 103,
      (byte) 93,
      (byte) 123,
      (byte) 90,
      (byte) 77,
      (byte) 202,
      (byte) 30,
      (byte) 238,
      (byte) 104,
      (byte) 209,
      (byte) 109,
      (byte) 135,
      (byte) 194,
      (byte) 136,
      (byte) 153,
      (byte) 215,
      (byte) 64 /*0x40*/,
      (byte) 198,
      (byte) 31 /*0x1F*/,
      (byte) 253,
      (byte) 220,
      (byte) 139,
      (byte) 59,
      (byte) 54,
      (byte) 1,
      (byte) 204,
      (byte) 249,
      (byte) 17,
      (byte) 146,
      (byte) 220
    };
    byte[] numArray22 = new byte[30]
    {
      (byte) 245,
      (byte) 210,
      (byte) 138,
      (byte) 83,
      (byte) 198,
      (byte) 80 /*0x50*/,
      (byte) 234,
      (byte) 184,
      (byte) 191,
      (byte) 106,
      (byte) 97,
      (byte) 233,
      (byte) 249,
      (byte) 88,
      (byte) 156,
      (byte) 147,
      (byte) 184,
      (byte) 124,
      (byte) 94,
      (byte) 39,
      byte.MaxValue,
      (byte) 222,
      (byte) 124,
      (byte) 139,
      (byte) 126,
      (byte) 73,
      (byte) 20,
      (byte) 35,
      (byte) 253,
      (byte) 29
    };
    key.Query(true, 335, numArray21, numArray21);
    Array.Copy((Array) numArray21, 0, (Array) numArray12, 220, 30);
    for (int index = 0; index < 30; ++index)
      numArray12[index + 220] ^= numArray22[index];
    return Encoding.UTF8.GetString(numArray12);
  }

  internal static string ssp_appserver_12469()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[219];
      byte[] numArray2 = new byte[55]
      {
        (byte) 229,
        (byte) 80 /*0x50*/,
        (byte) 100,
        (byte) 78,
        (byte) 38,
        (byte) 4,
        (byte) 242,
        (byte) 111,
        (byte) 52,
        (byte) 121,
        (byte) 97,
        (byte) 163,
        (byte) 254,
        (byte) 97,
        (byte) 166,
        (byte) 83,
        (byte) 159,
        (byte) 145,
        (byte) 34,
        (byte) 109,
        (byte) 46,
        (byte) 220,
        (byte) 33,
        (byte) 68,
        (byte) 89,
        (byte) 128 /*0x80*/,
        (byte) 208 /*0xD0*/,
        (byte) 13,
        (byte) 91,
        (byte) 140,
        (byte) 222,
        (byte) 73,
        (byte) 154,
        (byte) 156,
        (byte) 162,
        (byte) 72,
        (byte) 115,
        (byte) 242,
        (byte) 159,
        (byte) 27,
        (byte) 11,
        (byte) 108,
        (byte) 87,
        (byte) 194,
        (byte) 213,
        (byte) 93,
        (byte) 205,
        (byte) 152,
        (byte) 106,
        (byte) 63 /*0x3F*/,
        (byte) 60,
        (byte) 96 /*0x60*/,
        (byte) 1,
        (byte) 127 /*0x7F*/,
        (byte) 247
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 129,
        (byte) 17,
        (byte) 210,
        (byte) 53,
        (byte) 102,
        (byte) 247,
        (byte) 161,
        (byte) 90,
        (byte) 76,
        (byte) 105,
        (byte) 22,
        (byte) 56,
        (byte) 224 /*0xE0*/,
        (byte) 74,
        (byte) 163,
        (byte) 171,
        (byte) 140,
        (byte) 190,
        (byte) 9,
        (byte) 39,
        (byte) 84,
        (byte) 34,
        (byte) 132,
        (byte) 145,
        (byte) 154,
        (byte) 80 /*0x50*/,
        (byte) 0,
        (byte) 36,
        (byte) 8,
        (byte) 232,
        (byte) 173,
        (byte) 51,
        (byte) 46,
        (byte) 216,
        (byte) 244,
        (byte) 166,
        (byte) 185,
        (byte) 211,
        (byte) 204,
        (byte) 55,
        (byte) 214,
        (byte) 171,
        (byte) 140,
        (byte) 63 /*0x3F*/,
        (byte) 228,
        (byte) 1,
        (byte) 1,
        (byte) 107,
        (byte) 184,
        (byte) 61,
        (byte) 189,
        (byte) 186,
        (byte) 37,
        (byte) 79,
        (byte) 98
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55];
      numArray4[40] = (byte) 175;
      numArray4[12] = (byte) 94;
      numArray4[53] = (byte) 132;
      numArray4[3] = (byte) 52;
      numArray4[24] = (byte) 56;
      numArray4[0] = (byte) 32 /*0x20*/;
      numArray4[39] = (byte) 89;
      numArray4[7] = (byte) 154;
      numArray4[11] = (byte) 190;
      numArray4[9] = (byte) 8;
      numArray4[41] = (byte) 39;
      numArray4[52] = (byte) 54;
      numArray4[5] = (byte) 19;
      numArray4[22] = (byte) 251;
      numArray4[6] = (byte) 168;
      numArray4[19] = (byte) 132;
      numArray4[44] = (byte) 106;
      numArray4[17] = (byte) 97;
      numArray4[25] = (byte) 162;
      numArray4[10] = (byte) 98;
      numArray4[13] = (byte) 224 /*0xE0*/;
      numArray4[45] = (byte) 217;
      numArray4[34] = (byte) 83;
      numArray4[23] = (byte) 179;
      numArray4[21] = (byte) 111;
      numArray4[15] = (byte) 241;
      numArray4[26] = (byte) 28;
      numArray4[27] = (byte) 79;
      numArray4[28] = (byte) 126;
      numArray4[29] = (byte) 71;
      numArray4[30] = (byte) 26;
      numArray4[33] = (byte) 24;
      numArray4[32 /*0x20*/] = (byte) 153;
      numArray4[48 /*0x30*/] = (byte) 143;
      numArray4[38] = (byte) 37;
      numArray4[35] = (byte) 253;
      numArray4[14] = (byte) 173;
      numArray4[4] = (byte) 229;
      numArray4[37] = (byte) 230;
      numArray4[1] = (byte) 244;
      numArray4[31 /*0x1F*/] = (byte) 164;
      numArray4[36] = (byte) 250;
      numArray4[42] = (byte) 189;
      numArray4[43] = (byte) 84;
      numArray4[8] = (byte) 24;
      numArray4[2] = (byte) 165;
      numArray4[46] = (byte) 12;
      numArray4[47] = (byte) 95;
      numArray4[20] = (byte) 95;
      numArray4[16 /*0x10*/] = (byte) 7;
      numArray4[49] = (byte) 110;
      numArray4[51] = (byte) 30;
      numArray4[54] = (byte) 208 /*0xD0*/;
      numArray4[18] = (byte) 193;
      numArray4[50] = (byte) 250;
      byte[] numArray5 = new byte[55]
      {
        (byte) 83,
        (byte) 68,
        (byte) 62,
        (byte) 61,
        (byte) 200,
        (byte) 17,
        (byte) 114,
        (byte) 137,
        (byte) 69,
        (byte) 102,
        (byte) 116,
        (byte) 229,
        (byte) 62,
        (byte) 117,
        (byte) 129,
        (byte) 141,
        (byte) 134,
        (byte) 107,
        (byte) 202,
        (byte) 155,
        (byte) 193,
        (byte) 253,
        (byte) 16 /*0x10*/,
        (byte) 76,
        (byte) 169,
        (byte) 139,
        (byte) 109,
        (byte) 84,
        (byte) 49,
        (byte) 17,
        (byte) 238,
        (byte) 179,
        (byte) 124,
        (byte) 239,
        (byte) 240 /*0xF0*/,
        (byte) 118,
        (byte) 251,
        (byte) 68,
        (byte) 251,
        (byte) 189,
        (byte) 167,
        (byte) 250,
        (byte) 212,
        (byte) 6,
        (byte) 178,
        (byte) 28,
        (byte) 17,
        (byte) 205,
        (byte) 134,
        (byte) 95,
        (byte) 239,
        (byte) 201,
        (byte) 238,
        (byte) 150,
        (byte) 129
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[55]
      {
        (byte) 169,
        (byte) 157,
        (byte) 173,
        (byte) 58,
        (byte) 153,
        (byte) 11,
        (byte) 131,
        (byte) 241,
        (byte) 80 /*0x50*/,
        (byte) 14,
        (byte) 252,
        (byte) 234,
        (byte) 171,
        (byte) 239,
        (byte) 53,
        (byte) 89,
        (byte) 6,
        (byte) 127 /*0x7F*/,
        (byte) 44,
        (byte) 197,
        (byte) 64 /*0x40*/,
        (byte) 156,
        (byte) 169,
        (byte) 205,
        (byte) 234,
        (byte) 225,
        (byte) 153,
        (byte) 114,
        (byte) 26,
        (byte) 158,
        (byte) 103,
        (byte) 176 /*0xB0*/,
        (byte) 163,
        (byte) 177,
        (byte) 95,
        (byte) 77,
        (byte) 52,
        (byte) 39,
        (byte) 188,
        byte.MaxValue,
        (byte) 104,
        (byte) 116,
        (byte) 91,
        (byte) 166,
        (byte) 203,
        (byte) 231,
        (byte) 50,
        (byte) 135,
        (byte) 164,
        (byte) 136,
        (byte) 200,
        (byte) 46,
        (byte) 107,
        (byte) 56,
        (byte) 18
      };
      byte[] numArray7 = new byte[55]
      {
        (byte) 52,
        (byte) 37,
        (byte) 64 /*0x40*/,
        (byte) 21,
        (byte) 35,
        (byte) 253,
        (byte) 152,
        (byte) 130,
        (byte) 26,
        (byte) 130,
        (byte) 186,
        (byte) 243,
        (byte) 197,
        (byte) 55,
        (byte) 22,
        (byte) 6,
        (byte) 62,
        (byte) 49,
        (byte) 66,
        (byte) 56,
        (byte) 121,
        (byte) 183,
        (byte) 81,
        (byte) 168,
        (byte) 174,
        (byte) 18,
        (byte) 77,
        (byte) 211,
        (byte) 148,
        (byte) 211,
        (byte) 148,
        (byte) 127 /*0x7F*/,
        (byte) 20,
        (byte) 71,
        (byte) 225,
        (byte) 160 /*0xA0*/,
        (byte) 88,
        (byte) 81,
        (byte) 253,
        (byte) 236,
        (byte) 160 /*0xA0*/,
        (byte) 82,
        (byte) 254,
        (byte) 23,
        (byte) 143,
        (byte) 252,
        (byte) 38,
        (byte) 124,
        (byte) 186,
        (byte) 180,
        (byte) 141,
        (byte) 72,
        (byte) 119,
        (byte) 79,
        (byte) 238
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[54];
      numArray8[43] = (byte) 160 /*0xA0*/;
      numArray8[1] = (byte) 79;
      numArray8[15] = (byte) 195;
      numArray8[28] = (byte) 56;
      numArray8[11] = (byte) 143;
      numArray8[2] = (byte) 184;
      numArray8[6] = (byte) 177;
      numArray8[7] = (byte) 167;
      numArray8[8] = (byte) 75;
      numArray8[9] = (byte) 71;
      numArray8[21] = (byte) 1;
      numArray8[49] = (byte) 107;
      numArray8[27] = (byte) 214;
      numArray8[39] = (byte) 157;
      numArray8[30] = (byte) 106;
      numArray8[50] = (byte) 61;
      numArray8[51] = (byte) 219;
      numArray8[14] = (byte) 222;
      numArray8[36] = (byte) 4;
      numArray8[19] = (byte) 64 /*0x40*/;
      numArray8[20] = (byte) 2;
      numArray8[16 /*0x10*/] = (byte) 45;
      numArray8[45] = (byte) 247;
      numArray8[5] = (byte) 215;
      numArray8[37] = (byte) 140;
      numArray8[25] = (byte) 205;
      numArray8[53] = (byte) 204;
      numArray8[12] = (byte) 231;
      numArray8[34] = (byte) 84;
      numArray8[26] = (byte) 145;
      numArray8[17] = (byte) 93;
      numArray8[31 /*0x1F*/] = (byte) 159;
      numArray8[32 /*0x20*/] = (byte) 81;
      numArray8[0] = (byte) 224 /*0xE0*/;
      numArray8[24] = (byte) 17;
      numArray8[35] = (byte) 215;
      numArray8[13] = (byte) 36;
      numArray8[33] = (byte) 89;
      numArray8[38] = (byte) 87;
      numArray8[18] = (byte) 203;
      numArray8[40] = (byte) 169;
      numArray8[41] = (byte) 254;
      numArray8[42] = (byte) 127 /*0x7F*/;
      numArray8[22] = (byte) 3;
      numArray8[44] = (byte) 57;
      numArray8[10] = (byte) 249;
      numArray8[46] = (byte) 189;
      numArray8[3] = (byte) 114;
      numArray8[48 /*0x30*/] = (byte) 233;
      numArray8[47] = (byte) 100;
      numArray8[23] = (byte) 174;
      numArray8[29] = (byte) 69;
      numArray8[52] = (byte) 71;
      numArray8[4] = (byte) 17;
      byte[] numArray9 = new byte[54]
      {
        (byte) 122,
        (byte) 105,
        (byte) 46,
        (byte) 154,
        (byte) 96 /*0x60*/,
        (byte) 31 /*0x1F*/,
        (byte) 176 /*0xB0*/,
        (byte) 178,
        (byte) 24,
        (byte) 210,
        (byte) 99,
        (byte) 169,
        (byte) 218,
        (byte) 113,
        (byte) 149,
        (byte) 167,
        (byte) 245,
        (byte) 100,
        (byte) 101,
        (byte) 27,
        (byte) 211,
        (byte) 239,
        (byte) 100,
        (byte) 63 /*0x3F*/,
        (byte) 116,
        (byte) 116,
        (byte) 250,
        (byte) 216,
        (byte) 217,
        (byte) 50,
        (byte) 104,
        (byte) 110,
        (byte) 147,
        (byte) 246,
        (byte) 45,
        (byte) 203,
        (byte) 18,
        (byte) 78,
        (byte) 233,
        (byte) 163,
        (byte) 193,
        (byte) 93,
        (byte) 240 /*0xF0*/,
        (byte) 221,
        (byte) 27,
        (byte) 18,
        (byte) 232,
        (byte) 24,
        (byte) 222,
        (byte) 226,
        (byte) 136,
        (byte) 66,
        (byte) 132,
        (byte) 154
      };
      key.Query(true, 335, numArray8, numArray8);
      Array.Copy((Array) numArray8, 0, (Array) numArray1, 165, 54);
      for (int index = 0; index < 54; ++index)
        numArray1[index + 165] ^= numArray9[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray10 = new byte[219];
    byte[] numArray11 = new byte[55]
    {
      (byte) 233,
      (byte) 149,
      (byte) 71,
      (byte) 50,
      (byte) 23,
      (byte) 238,
      (byte) 162,
      (byte) 253,
      (byte) 77,
      (byte) 2,
      (byte) 184,
      (byte) 39,
      (byte) 90,
      (byte) 124,
      (byte) 196,
      (byte) 117,
      (byte) 242,
      (byte) 212,
      (byte) 200,
      (byte) 117,
      (byte) 8,
      (byte) 159,
      (byte) 150,
      (byte) 95,
      (byte) 226,
      (byte) 153,
      (byte) 10,
      (byte) 32 /*0x20*/,
      (byte) 108,
      (byte) 186,
      (byte) 62,
      (byte) 79,
      (byte) 122,
      (byte) 25,
      (byte) 161,
      (byte) 101,
      (byte) 227,
      (byte) 224 /*0xE0*/,
      (byte) 64 /*0x40*/,
      (byte) 224 /*0xE0*/,
      (byte) 236,
      (byte) 123,
      (byte) 117,
      (byte) 220,
      (byte) 1,
      (byte) 7,
      (byte) 57,
      (byte) 102,
      (byte) 231,
      (byte) 38,
      (byte) 226,
      (byte) 69,
      (byte) 109,
      (byte) 119,
      (byte) 84
    };
    byte[] numArray12 = new byte[55]
    {
      (byte) 1,
      (byte) 117,
      (byte) 106,
      (byte) 194,
      (byte) 60,
      (byte) 108,
      (byte) 193,
      (byte) 124,
      (byte) 60,
      (byte) 77,
      (byte) 213,
      (byte) 170,
      (byte) 21,
      (byte) 143,
      (byte) 93,
      (byte) 187,
      (byte) 124,
      (byte) 31 /*0x1F*/,
      (byte) 167,
      (byte) 95,
      (byte) 229,
      (byte) 125,
      (byte) 3,
      (byte) 253,
      (byte) 106,
      (byte) 71,
      (byte) 117,
      (byte) 73,
      byte.MaxValue,
      (byte) 2,
      (byte) 195,
      (byte) 17,
      (byte) 22,
      (byte) 76,
      (byte) 28,
      (byte) 75,
      (byte) 123,
      (byte) 218,
      (byte) 68,
      (byte) 49,
      (byte) 57,
      (byte) 97,
      (byte) 72,
      (byte) 132,
      (byte) 215,
      (byte) 239,
      (byte) 224 /*0xE0*/,
      (byte) 66,
      (byte) 134,
      (byte) 175,
      (byte) 164,
      (byte) 48 /*0x30*/,
      (byte) 226,
      (byte) 111,
      (byte) 141
    };
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray10, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index] ^= numArray12[index];
    byte[] numArray13 = new byte[55];
    numArray13[45] = (byte) 138;
    numArray13[1] = (byte) 80 /*0x50*/;
    numArray13[42] = (byte) 151;
    numArray13[22] = (byte) 218;
    numArray13[23] = (byte) 96 /*0x60*/;
    numArray13[52] = (byte) 199;
    numArray13[40] = (byte) 225;
    numArray13[7] = (byte) 195;
    numArray13[8] = (byte) 105;
    numArray13[9] = (byte) 173;
    numArray13[48 /*0x30*/] = (byte) 208 /*0xD0*/;
    numArray13[11] = (byte) 219;
    numArray13[54] = (byte) 68;
    numArray13[13] = (byte) 146;
    numArray13[14] = (byte) 18;
    numArray13[15] = (byte) 147;
    numArray13[53] = (byte) 31 /*0x1F*/;
    numArray13[0] = (byte) 232;
    numArray13[5] = (byte) 145;
    numArray13[19] = (byte) 42;
    numArray13[33] = (byte) 102;
    numArray13[21] = (byte) 94;
    numArray13[41] = (byte) 24;
    numArray13[10] = (byte) 65;
    numArray13[24] = (byte) 164;
    numArray13[25] = (byte) 56;
    numArray13[3] = (byte) 227;
    numArray13[27] = (byte) 215;
    numArray13[28] = (byte) 248;
    numArray13[29] = (byte) 226;
    numArray13[30] = (byte) 2;
    numArray13[38] = (byte) 106;
    numArray13[32 /*0x20*/] = (byte) 87;
    numArray13[18] = (byte) 140;
    numArray13[34] = (byte) 251;
    numArray13[16 /*0x10*/] = (byte) 232;
    numArray13[36] = (byte) 194;
    numArray13[37] = (byte) 252;
    numArray13[4] = (byte) 130;
    numArray13[51] = (byte) 238;
    numArray13[12] = (byte) 95;
    numArray13[39] = (byte) 234;
    numArray13[46] = (byte) 136;
    numArray13[26] = (byte) 69;
    numArray13[17] = (byte) 137;
    numArray13[6] = (byte) 147;
    numArray13[2] = (byte) 126;
    numArray13[20] = (byte) 185;
    numArray13[35] = (byte) 144 /*0x90*/;
    numArray13[49] = (byte) 177;
    numArray13[50] = (byte) 133;
    numArray13[44] = (byte) 247;
    numArray13[47] = (byte) 147;
    numArray13[43] = (byte) 90;
    numArray13[31 /*0x1F*/] = (byte) 196;
    byte[] numArray14 = new byte[55];
    numArray14[26] = (byte) 126;
    numArray14[11] = (byte) 126;
    numArray14[2] = (byte) 161;
    numArray14[13] = (byte) 201;
    numArray14[10] = (byte) 1;
    numArray14[47] = (byte) 5;
    numArray14[6] = (byte) 233;
    numArray14[7] = (byte) 25;
    numArray14[31 /*0x1F*/] = (byte) 216;
    numArray14[9] = (byte) 57;
    numArray14[16 /*0x10*/] = (byte) 90;
    numArray14[43] = (byte) 215;
    numArray14[38] = (byte) 18;
    numArray14[52] = (byte) 112 /*0x70*/;
    numArray14[32 /*0x20*/] = (byte) 10;
    numArray14[5] = (byte) 98;
    numArray14[39] = (byte) 62;
    numArray14[17] = (byte) 153;
    numArray14[1] = (byte) 133;
    numArray14[15] = (byte) 90;
    numArray14[20] = (byte) 40;
    numArray14[21] = (byte) 183;
    numArray14[22] = (byte) 211;
    numArray14[23] = (byte) 154;
    numArray14[4] = (byte) 114;
    numArray14[25] = (byte) 51;
    numArray14[44] = (byte) 27;
    numArray14[27] = (byte) 123;
    numArray14[53] = (byte) 17;
    numArray14[18] = (byte) 1;
    numArray14[30] = (byte) 249;
    numArray14[33] = (byte) 188;
    numArray14[45] = (byte) 73;
    numArray14[35] = (byte) 175;
    numArray14[0] = (byte) 19;
    numArray14[14] = (byte) 52;
    numArray14[36] = (byte) 36;
    numArray14[37] = (byte) 182;
    numArray14[41] = (byte) 203;
    numArray14[28] = (byte) 39;
    numArray14[40] = (byte) 65;
    numArray14[46] = (byte) 63 /*0x3F*/;
    numArray14[3] = (byte) 127 /*0x7F*/;
    numArray14[42] = (byte) 102;
    numArray14[8] = (byte) 201;
    numArray14[29] = (byte) 226;
    numArray14[12] = (byte) 117;
    numArray14[34] = (byte) 242;
    numArray14[48 /*0x30*/] = (byte) 149;
    numArray14[24] = (byte) 153;
    numArray14[50] = (byte) 189;
    numArray14[51] = (byte) 31 /*0x1F*/;
    numArray14[49] = (byte) 203;
    numArray14[19] = (byte) 197;
    numArray14[54] = (byte) 10;
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray10, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index + 55] ^= numArray14[index];
    byte[] numArray15 = new byte[55];
    numArray15[34] = (byte) 42;
    numArray15[1] = (byte) 18;
    numArray15[15] = (byte) 118;
    numArray15[22] = (byte) 88;
    numArray15[4] = (byte) 250;
    numArray15[54] = (byte) 106;
    numArray15[6] = (byte) 102;
    numArray15[7] = (byte) 84;
    numArray15[46] = (byte) 163;
    numArray15[17] = (byte) 177;
    numArray15[10] = (byte) 88;
    numArray15[21] = (byte) 244;
    numArray15[41] = (byte) 195;
    numArray15[13] = (byte) 60;
    numArray15[43] = (byte) 206;
    numArray15[45] = (byte) 96 /*0x60*/;
    numArray15[47] = (byte) 119;
    numArray15[16 /*0x10*/] = byte.MaxValue;
    numArray15[52] = (byte) 198;
    numArray15[30] = (byte) 171;
    numArray15[44] = (byte) 95;
    numArray15[18] = (byte) 137;
    numArray15[3] = (byte) 228;
    numArray15[32 /*0x20*/] = (byte) 162;
    numArray15[24] = (byte) 221;
    numArray15[42] = (byte) 82;
    numArray15[11] = (byte) 118;
    numArray15[23] = (byte) 104;
    numArray15[9] = (byte) 236;
    numArray15[29] = (byte) 182;
    numArray15[8] = (byte) 167;
    numArray15[49] = (byte) 91;
    numArray15[5] = (byte) 29;
    numArray15[33] = (byte) 1;
    numArray15[28] = (byte) 113;
    numArray15[2] = (byte) 119;
    numArray15[36] = (byte) 16 /*0x10*/;
    numArray15[20] = (byte) 185;
    numArray15[38] = (byte) 119;
    numArray15[39] = (byte) 172;
    numArray15[25] = (byte) 211;
    numArray15[19] = (byte) 208 /*0xD0*/;
    numArray15[12] = (byte) 66;
    numArray15[53] = (byte) 59;
    numArray15[27] = (byte) 87;
    numArray15[31 /*0x1F*/] = (byte) 45;
    numArray15[50] = (byte) 33;
    numArray15[35] = (byte) 134;
    numArray15[48 /*0x30*/] = (byte) 215;
    numArray15[37] = (byte) 31 /*0x1F*/;
    numArray15[0] = (byte) 197;
    numArray15[51] = (byte) 3;
    numArray15[40] = (byte) 127 /*0x7F*/;
    numArray15[14] = (byte) 148;
    numArray15[26] = (byte) 179;
    byte[] numArray16 = new byte[55]
    {
      (byte) 190,
      (byte) 3,
      (byte) 161,
      (byte) 77,
      (byte) 86,
      (byte) 68,
      (byte) 77,
      (byte) 133,
      (byte) 161,
      (byte) 220,
      (byte) 120,
      (byte) 102,
      (byte) 229,
      (byte) 89,
      (byte) 65,
      (byte) 88,
      (byte) 174,
      (byte) 101,
      (byte) 28,
      (byte) 234,
      (byte) 199,
      (byte) 192 /*0xC0*/,
      (byte) 109,
      (byte) 59,
      (byte) 87,
      (byte) 120,
      (byte) 237,
      (byte) 88,
      (byte) 29,
      (byte) 147,
      (byte) 180,
      (byte) 52,
      (byte) 22,
      (byte) 236,
      (byte) 31 /*0x1F*/,
      (byte) 200,
      (byte) 65,
      (byte) 171,
      (byte) 148,
      (byte) 170,
      (byte) 246,
      (byte) 183,
      (byte) 28,
      (byte) 84,
      (byte) 199,
      (byte) 151,
      (byte) 57,
      (byte) 200,
      (byte) 159,
      (byte) 254,
      (byte) 23,
      (byte) 99,
      (byte) 225,
      (byte) 51,
      (byte) 60
    };
    key.Query(true, 335, numArray15, numArray15);
    Array.Copy((Array) numArray15, 0, (Array) numArray10, 110, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index + 110] ^= numArray16[index];
    byte[] numArray17 = new byte[54]
    {
      (byte) 6,
      (byte) 137,
      (byte) 246,
      (byte) 46,
      (byte) 142,
      (byte) 203,
      (byte) 91,
      (byte) 151,
      (byte) 196,
      (byte) 109,
      (byte) 169,
      (byte) 185,
      (byte) 64 /*0x40*/,
      (byte) 100,
      (byte) 198,
      (byte) 234,
      (byte) 154,
      (byte) 224 /*0xE0*/,
      (byte) 20,
      (byte) 63 /*0x3F*/,
      (byte) 251,
      (byte) 212,
      (byte) 240 /*0xF0*/,
      (byte) 205,
      (byte) 214,
      (byte) 183,
      (byte) 32 /*0x20*/,
      (byte) 84,
      (byte) 224 /*0xE0*/,
      (byte) 124,
      (byte) 124,
      (byte) 35,
      (byte) 39,
      (byte) 156,
      (byte) 250,
      (byte) 144 /*0x90*/,
      (byte) 89,
      (byte) 19,
      (byte) 226,
      (byte) 21,
      (byte) 6,
      (byte) 246,
      (byte) 111,
      (byte) 31 /*0x1F*/,
      (byte) 185,
      (byte) 171,
      (byte) 136,
      (byte) 144 /*0x90*/,
      (byte) 122,
      (byte) 222,
      (byte) 79,
      (byte) 9,
      (byte) 96 /*0x60*/,
      (byte) 241
    };
    byte[] numArray18 = new byte[54];
    numArray18[26] = (byte) 246;
    numArray18[1] = (byte) 117;
    numArray18[2] = (byte) 230;
    numArray18[3] = (byte) 194;
    numArray18[4] = (byte) 174;
    numArray18[12] = (byte) 80 /*0x50*/;
    numArray18[8] = (byte) 158;
    numArray18[35] = (byte) 237;
    numArray18[7] = (byte) 53;
    numArray18[34] = (byte) 188;
    numArray18[10] = (byte) 238;
    numArray18[16 /*0x10*/] = (byte) 110;
    numArray18[40] = (byte) 103;
    numArray18[45] = (byte) 195;
    numArray18[27] = (byte) 61;
    numArray18[15] = (byte) 169;
    numArray18[31 /*0x1F*/] = (byte) 226;
    numArray18[17] = (byte) 246;
    numArray18[43] = byte.MaxValue;
    numArray18[19] = (byte) 226;
    numArray18[23] = byte.MaxValue;
    numArray18[21] = (byte) 247;
    numArray18[20] = (byte) 86;
    numArray18[42] = (byte) 168;
    numArray18[0] = (byte) 59;
    numArray18[25] = (byte) 163;
    numArray18[11] = (byte) 192 /*0xC0*/;
    numArray18[13] = (byte) 99;
    numArray18[46] = (byte) 163;
    numArray18[50] = (byte) 168;
    numArray18[47] = (byte) 50;
    numArray18[24] = (byte) 142;
    numArray18[37] = (byte) 102;
    numArray18[33] = (byte) 184;
    numArray18[30] = (byte) 123;
    numArray18[14] = (byte) 148;
    numArray18[5] = (byte) 214;
    numArray18[9] = (byte) 100;
    numArray18[38] = (byte) 199;
    numArray18[18] = (byte) 173;
    numArray18[36] = (byte) 43;
    numArray18[41] = (byte) 213;
    numArray18[29] = (byte) 96 /*0x60*/;
    numArray18[39] = (byte) 188;
    numArray18[44] = (byte) 23;
    numArray18[28] = (byte) 88;
    numArray18[51] = (byte) 60;
    numArray18[22] = (byte) 238;
    numArray18[48 /*0x30*/] = (byte) 33;
    numArray18[49] = (byte) 138;
    numArray18[32 /*0x20*/] = (byte) 132;
    numArray18[53] = (byte) 45;
    numArray18[52] = (byte) 59;
    numArray18[6] = (byte) 69;
    key.Query(true, 335, numArray17, numArray17);
    Array.Copy((Array) numArray17, 0, (Array) numArray10, 165, 54);
    for (int index = 0; index < 54; ++index)
      numArray10[index + 165] ^= numArray18[index];
    return Encoding.UTF8.GetString(numArray10);
  }

  internal static string ssp_appserver_12470()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[250];
      byte[] numArray2 = new byte[55]
      {
        (byte) 21,
        (byte) 46,
        (byte) 168,
        (byte) 27,
        (byte) 45,
        (byte) 181,
        (byte) 26,
        (byte) 125,
        (byte) 195,
        (byte) 91,
        (byte) 59,
        (byte) 160 /*0xA0*/,
        (byte) 62,
        (byte) 97,
        (byte) 203,
        (byte) 242,
        (byte) 148,
        (byte) 56,
        (byte) 197,
        (byte) 61,
        (byte) 163,
        (byte) 224 /*0xE0*/,
        (byte) 16 /*0x10*/,
        (byte) 73,
        (byte) 104,
        (byte) 148,
        (byte) 179,
        (byte) 100,
        (byte) 104,
        (byte) 97,
        (byte) 125,
        (byte) 56,
        (byte) 251,
        (byte) 48 /*0x30*/,
        (byte) 25,
        (byte) 140,
        (byte) 99,
        (byte) 79,
        (byte) 210,
        (byte) 137,
        (byte) 12,
        (byte) 176 /*0xB0*/,
        (byte) 111,
        (byte) 190,
        (byte) 169,
        (byte) 134,
        (byte) 5,
        (byte) 236,
        (byte) 252,
        (byte) 23,
        (byte) 205,
        (byte) 128 /*0x80*/,
        (byte) 105,
        (byte) 144 /*0x90*/,
        (byte) 35
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 10,
        (byte) 185,
        (byte) 41,
        (byte) 164,
        (byte) 32 /*0x20*/,
        (byte) 158,
        (byte) 66,
        (byte) 175,
        (byte) 77,
        (byte) 130,
        (byte) 2,
        (byte) 46,
        (byte) 88,
        (byte) 66,
        (byte) 24,
        (byte) 147,
        (byte) 127 /*0x7F*/,
        (byte) 57,
        (byte) 83,
        (byte) 111,
        (byte) 47,
        (byte) 134,
        (byte) 59,
        (byte) 160 /*0xA0*/,
        (byte) 113,
        (byte) 32 /*0x20*/,
        (byte) 110,
        (byte) 231,
        (byte) 126,
        (byte) 74,
        (byte) 84,
        (byte) 53,
        (byte) 128 /*0x80*/,
        (byte) 23,
        (byte) 122,
        (byte) 147,
        (byte) 130,
        (byte) 108,
        (byte) 99,
        (byte) 104,
        (byte) 5,
        (byte) 206,
        (byte) 172,
        (byte) 225,
        (byte) 146,
        (byte) 56,
        (byte) 21,
        (byte) 140,
        (byte) 52,
        (byte) 117,
        (byte) 129,
        (byte) 37,
        (byte) 53,
        (byte) 71,
        (byte) 120
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 94,
        (byte) 178,
        (byte) 153,
        (byte) 2,
        (byte) 182,
        (byte) 21,
        (byte) 97,
        (byte) 222,
        (byte) 2,
        (byte) 165,
        (byte) 14,
        (byte) 71,
        (byte) 62,
        (byte) 143,
        (byte) 69,
        (byte) 102,
        (byte) 59,
        (byte) 48 /*0x30*/,
        (byte) 242,
        (byte) 112 /*0x70*/,
        (byte) 81,
        (byte) 159,
        (byte) 226,
        (byte) 142,
        (byte) 198,
        (byte) 103,
        (byte) 80 /*0x50*/,
        (byte) 148,
        (byte) 253,
        (byte) 206,
        (byte) 107,
        (byte) 13,
        (byte) 228,
        (byte) 110,
        (byte) 4,
        (byte) 156,
        (byte) 57,
        (byte) 135,
        (byte) 86,
        (byte) 155,
        (byte) 14,
        (byte) 224 /*0xE0*/,
        (byte) 167,
        (byte) 224 /*0xE0*/,
        (byte) 247,
        (byte) 165,
        (byte) 190,
        (byte) 136,
        (byte) 136,
        (byte) 25,
        (byte) 46,
        (byte) 95,
        (byte) 48 /*0x30*/,
        (byte) 196,
        (byte) 204
      };
      byte[] numArray5 = new byte[55]
      {
        (byte) 187,
        (byte) 194,
        (byte) 208 /*0xD0*/,
        (byte) 254,
        (byte) 122,
        (byte) 136,
        (byte) 223,
        (byte) 91,
        (byte) 218,
        (byte) 28,
        (byte) 51,
        (byte) 143,
        (byte) 109,
        (byte) 226,
        (byte) 22,
        (byte) 233,
        (byte) 54,
        (byte) 155,
        (byte) 153,
        (byte) 123,
        (byte) 166,
        (byte) 58,
        (byte) 30,
        (byte) 116,
        (byte) 235,
        (byte) 237,
        (byte) 164,
        (byte) 155,
        (byte) 254,
        (byte) 109,
        (byte) 29,
        (byte) 139,
        (byte) 252,
        (byte) 194,
        (byte) 103,
        (byte) 208 /*0xD0*/,
        (byte) 118,
        (byte) 117,
        (byte) 58,
        (byte) 200,
        (byte) 211,
        (byte) 13,
        (byte) 158,
        (byte) 100,
        (byte) 56,
        (byte) 94,
        (byte) 233,
        (byte) 195,
        (byte) 72,
        (byte) 113,
        (byte) 171,
        (byte) 50,
        (byte) 67,
        (byte) 209,
        (byte) 163
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[55]
      {
        (byte) 10,
        (byte) 59,
        (byte) 78,
        (byte) 196,
        (byte) 205,
        (byte) 225,
        (byte) 51,
        (byte) 57,
        (byte) 1,
        (byte) 99,
        (byte) 208 /*0xD0*/,
        (byte) 138,
        (byte) 168,
        (byte) 45,
        (byte) 180,
        (byte) 217,
        (byte) 87,
        (byte) 98,
        (byte) 113,
        (byte) 247,
        (byte) 1,
        (byte) 150,
        (byte) 33,
        (byte) 169,
        (byte) 199,
        (byte) 12,
        (byte) 227,
        (byte) 160 /*0xA0*/,
        (byte) 11,
        (byte) 220,
        (byte) 31 /*0x1F*/,
        (byte) 162,
        (byte) 69,
        (byte) 204,
        (byte) 46,
        (byte) 76,
        (byte) 8,
        (byte) 214,
        (byte) 64 /*0x40*/,
        byte.MaxValue,
        (byte) 116,
        (byte) 166,
        (byte) 246,
        (byte) 66,
        (byte) 238,
        (byte) 253,
        (byte) 223,
        (byte) 236,
        (byte) 121,
        (byte) 134,
        (byte) 217,
        (byte) 98,
        (byte) 90,
        (byte) 199,
        (byte) 206
      };
      byte[] numArray7 = new byte[55]
      {
        (byte) 77,
        (byte) 249,
        (byte) 112 /*0x70*/,
        (byte) 112 /*0x70*/,
        (byte) 123,
        (byte) 43,
        (byte) 174,
        (byte) 191,
        (byte) 68,
        (byte) 58,
        (byte) 234,
        (byte) 218,
        (byte) 166,
        (byte) 42,
        (byte) 6,
        (byte) 194,
        (byte) 165,
        (byte) 221,
        (byte) 117,
        (byte) 76,
        (byte) 152,
        (byte) 137,
        (byte) 129,
        (byte) 201,
        (byte) 7,
        (byte) 231,
        (byte) 182,
        (byte) 62,
        (byte) 217,
        (byte) 22,
        (byte) 26,
        (byte) 106,
        (byte) 243,
        (byte) 37,
        (byte) 194,
        (byte) 46,
        (byte) 226,
        (byte) 104,
        (byte) 119,
        (byte) 187,
        (byte) 0,
        (byte) 82,
        (byte) 218,
        (byte) 145,
        (byte) 29,
        (byte) 112 /*0x70*/,
        (byte) 60,
        (byte) 171,
        (byte) 63 /*0x3F*/,
        (byte) 188,
        (byte) 80 /*0x50*/,
        (byte) 104,
        (byte) 103,
        (byte) 161,
        (byte) 170
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[55];
      numArray8[25] = (byte) 38;
      numArray8[28] = (byte) 188;
      numArray8[40] = (byte) 209;
      numArray8[31 /*0x1F*/] = (byte) 254;
      numArray8[13] = (byte) 209;
      numArray8[5] = (byte) 46;
      numArray8[6] = (byte) 36;
      numArray8[7] = (byte) 67;
      numArray8[44] = (byte) 3;
      numArray8[9] = (byte) 69;
      numArray8[19] = (byte) 160 /*0xA0*/;
      numArray8[52] = (byte) 115;
      numArray8[12] = (byte) 104;
      numArray8[22] = (byte) 202;
      numArray8[2] = (byte) 192 /*0xC0*/;
      numArray8[27] = (byte) 2;
      numArray8[15] = (byte) 33;
      numArray8[17] = (byte) 131;
      numArray8[47] = (byte) 180;
      numArray8[3] = (byte) 253;
      numArray8[26] = (byte) 133;
      numArray8[21] = (byte) 187;
      numArray8[1] = (byte) 252;
      numArray8[14] = (byte) 129;
      numArray8[45] = (byte) 237;
      numArray8[10] = (byte) 219;
      numArray8[11] = (byte) 254;
      numArray8[24] = (byte) 246;
      numArray8[51] = (byte) 67;
      numArray8[50] = (byte) 232;
      numArray8[30] = (byte) 126;
      numArray8[48 /*0x30*/] = (byte) 87;
      numArray8[32 /*0x20*/] = (byte) 187;
      numArray8[54] = (byte) 58;
      numArray8[34] = (byte) 99;
      numArray8[35] = (byte) 217;
      numArray8[36] = (byte) 128 /*0x80*/;
      numArray8[29] = (byte) 71;
      numArray8[38] = (byte) 225;
      numArray8[39] = (byte) 120;
      numArray8[23] = (byte) 44;
      numArray8[41] = (byte) 254;
      numArray8[42] = (byte) 192 /*0xC0*/;
      numArray8[43] = (byte) 184;
      numArray8[4] = (byte) 146;
      numArray8[33] = (byte) 50;
      numArray8[46] = (byte) 17;
      numArray8[20] = (byte) 223;
      numArray8[18] = (byte) 90;
      numArray8[49] = (byte) 65;
      numArray8[37] = (byte) 1;
      numArray8[8] = (byte) 45;
      numArray8[16 /*0x10*/] = (byte) 113;
      numArray8[53] = (byte) 211;
      numArray8[0] = (byte) 164;
      byte[] numArray9 = new byte[55];
      numArray9[9] = (byte) 142;
      numArray9[54] = (byte) 9;
      numArray9[43] = (byte) 133;
      numArray9[3] = (byte) 192 /*0xC0*/;
      numArray9[42] = (byte) 128 /*0x80*/;
      numArray9[45] = (byte) 200;
      numArray9[31 /*0x1F*/] = (byte) 107;
      numArray9[7] = (byte) 154;
      numArray9[13] = (byte) 74;
      numArray9[22] = (byte) 51;
      numArray9[23] = (byte) 237;
      numArray9[11] = (byte) 132;
      numArray9[38] = (byte) 108;
      numArray9[8] = (byte) 203;
      numArray9[14] = (byte) 92;
      numArray9[15] = (byte) 3;
      numArray9[26] = (byte) 22;
      numArray9[25] = (byte) 182;
      numArray9[18] = (byte) 192 /*0xC0*/;
      numArray9[19] = (byte) 226;
      numArray9[20] = (byte) 229;
      numArray9[47] = (byte) 173;
      numArray9[36] = (byte) 167;
      numArray9[35] = (byte) 239;
      numArray9[24] = (byte) 171;
      numArray9[27] = (byte) 187;
      numArray9[33] = (byte) 201;
      numArray9[12] = (byte) 154;
      numArray9[28] = (byte) 179;
      numArray9[29] = (byte) 24;
      numArray9[30] = (byte) 177;
      numArray9[1] = (byte) 144 /*0x90*/;
      numArray9[32 /*0x20*/] = (byte) 156;
      numArray9[53] = (byte) 8;
      numArray9[34] = (byte) 31 /*0x1F*/;
      numArray9[48 /*0x30*/] = (byte) 248;
      numArray9[40] = (byte) 121;
      numArray9[37] = (byte) 120;
      numArray9[5] = (byte) 149;
      numArray9[39] = (byte) 9;
      numArray9[41] = (byte) 10;
      numArray9[51] = (byte) 231;
      numArray9[16 /*0x10*/] = (byte) 223;
      numArray9[2] = (byte) 163;
      numArray9[44] = (byte) 249;
      numArray9[6] = (byte) 32 /*0x20*/;
      numArray9[46] = (byte) 179;
      numArray9[17] = (byte) 96 /*0x60*/;
      numArray9[0] = (byte) 99;
      numArray9[49] = (byte) 240 /*0xF0*/;
      numArray9[50] = (byte) 182;
      numArray9[4] = (byte) 236;
      numArray9[52] = (byte) 75;
      numArray9[21] = (byte) 121;
      numArray9[10] = (byte) 194;
      key.Query(true, 335, numArray8, numArray8);
      Array.Copy((Array) numArray8, 0, (Array) numArray1, 165, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 165] ^= numArray9[index];
      byte[] numArray10 = new byte[30];
      numArray10[10] = (byte) 161;
      numArray10[3] = (byte) 79;
      numArray10[7] = (byte) 244;
      numArray10[13] = (byte) 52;
      numArray10[4] = (byte) 124;
      numArray10[0] = (byte) 103;
      numArray10[9] = (byte) 119;
      numArray10[1] = (byte) 159;
      numArray10[8] = (byte) 89;
      numArray10[20] = (byte) 186;
      numArray10[14] = (byte) 122;
      numArray10[5] = (byte) 48 /*0x30*/;
      numArray10[23] = (byte) 155;
      numArray10[27] = (byte) 227;
      numArray10[6] = (byte) 161;
      numArray10[17] = (byte) 231;
      numArray10[15] = (byte) 13;
      numArray10[11] = (byte) 101;
      numArray10[2] = (byte) 29;
      numArray10[19] = (byte) 207;
      numArray10[18] = (byte) 253;
      numArray10[21] = (byte) 48 /*0x30*/;
      numArray10[16 /*0x10*/] = (byte) 27;
      numArray10[22] = (byte) 16 /*0x10*/;
      numArray10[24] = (byte) 52;
      numArray10[25] = (byte) 76;
      numArray10[26] = (byte) 240 /*0xF0*/;
      numArray10[12] = (byte) 124;
      numArray10[28] = (byte) 35;
      numArray10[29] = (byte) 122;
      byte[] numArray11 = new byte[30]
      {
        (byte) 184,
        (byte) 105,
        (byte) 160 /*0xA0*/,
        (byte) 67,
        (byte) 154,
        (byte) 99,
        (byte) 80 /*0x50*/,
        (byte) 101,
        (byte) 5,
        (byte) 153,
        (byte) 79,
        (byte) 114,
        (byte) 31 /*0x1F*/,
        (byte) 25,
        (byte) 134,
        (byte) 237,
        (byte) 52,
        (byte) 214,
        (byte) 143,
        (byte) 130,
        (byte) 117,
        (byte) 66,
        (byte) 124,
        (byte) 18,
        (byte) 213,
        (byte) 114,
        (byte) 108,
        (byte) 15,
        (byte) 193,
        (byte) 236
      };
      key.Query(true, 335, numArray10, numArray10);
      Array.Copy((Array) numArray10, 0, (Array) numArray1, 220, 30);
      for (int index = 0; index < 30; ++index)
        numArray1[index + 220] ^= numArray11[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray12 = new byte[250];
    byte[] numArray13 = new byte[55]
    {
      (byte) 222,
      (byte) 120,
      (byte) 245,
      (byte) 163,
      (byte) 250,
      (byte) 26,
      (byte) 29,
      (byte) 11,
      (byte) 115,
      (byte) 43,
      (byte) 194,
      (byte) 30,
      (byte) 73,
      (byte) 183,
      (byte) 56,
      (byte) 160 /*0xA0*/,
      (byte) 69,
      (byte) 27,
      (byte) 58,
      (byte) 91,
      (byte) 186,
      (byte) 23,
      (byte) 0,
      (byte) 184,
      (byte) 196,
      (byte) 58,
      (byte) 226,
      (byte) 205,
      (byte) 127 /*0x7F*/,
      (byte) 141,
      (byte) 139,
      (byte) 252,
      (byte) 242,
      (byte) 159,
      (byte) 154,
      (byte) 94,
      (byte) 5,
      (byte) 75,
      (byte) 54,
      (byte) 155,
      (byte) 106,
      (byte) 92,
      (byte) 239,
      (byte) 210,
      (byte) 49,
      (byte) 222,
      (byte) 253,
      (byte) 127 /*0x7F*/,
      (byte) 86,
      (byte) 225,
      (byte) 213,
      (byte) 20,
      (byte) 4,
      (byte) 99,
      (byte) 22
    };
    byte[] numArray14 = new byte[55]
    {
      (byte) 51,
      (byte) 193,
      (byte) 21,
      (byte) 42,
      (byte) 164,
      (byte) 128 /*0x80*/,
      (byte) 248,
      (byte) 40,
      (byte) 75,
      (byte) 129,
      (byte) 58,
      (byte) 174,
      (byte) 218,
      (byte) 244,
      (byte) 138,
      (byte) 126,
      (byte) 239,
      (byte) 254,
      (byte) 113,
      (byte) 133,
      (byte) 62,
      (byte) 150,
      (byte) 216,
      (byte) 47,
      (byte) 36,
      (byte) 102,
      (byte) 13,
      (byte) 37,
      (byte) 203,
      (byte) 160 /*0xA0*/,
      (byte) 70,
      (byte) 3,
      (byte) 86,
      (byte) 249,
      (byte) 11,
      (byte) 177,
      (byte) 34,
      (byte) 32 /*0x20*/,
      (byte) 248,
      (byte) 18,
      (byte) 96 /*0x60*/,
      (byte) 135,
      (byte) 210,
      (byte) 75,
      (byte) 20,
      (byte) 198,
      (byte) 116,
      (byte) 90,
      (byte) 174,
      (byte) 214,
      (byte) 119,
      (byte) 13,
      (byte) 109,
      (byte) 36,
      (byte) 195
    };
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray12, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index] ^= numArray14[index];
    byte[] numArray15 = new byte[55];
    numArray15[5] = (byte) 139;
    numArray15[41] = (byte) 51;
    numArray15[2] = (byte) 191;
    numArray15[3] = (byte) 55;
    numArray15[51] = (byte) 114;
    numArray15[7] = (byte) 159;
    numArray15[30] = (byte) 143;
    numArray15[22] = (byte) 229;
    numArray15[8] = (byte) 197;
    numArray15[16 /*0x10*/] = (byte) 159;
    numArray15[10] = (byte) 170;
    numArray15[11] = (byte) 29;
    numArray15[12] = (byte) 218;
    numArray15[13] = (byte) 154;
    numArray15[19] = (byte) 39;
    numArray15[44] = (byte) 83;
    numArray15[20] = (byte) 45;
    numArray15[18] = (byte) 93;
    numArray15[37] = (byte) 99;
    numArray15[15] = (byte) 228;
    numArray15[54] = (byte) 74;
    numArray15[40] = (byte) 10;
    numArray15[14] = (byte) 249;
    numArray15[50] = (byte) 101;
    numArray15[1] = (byte) 223;
    numArray15[26] = (byte) 158;
    numArray15[31 /*0x1F*/] = (byte) 137;
    numArray15[24] = (byte) 99;
    numArray15[33] = (byte) 229;
    numArray15[21] = (byte) 135;
    numArray15[23] = (byte) 61;
    numArray15[9] = (byte) 44;
    numArray15[4] = (byte) 80 /*0x50*/;
    numArray15[25] = (byte) 118;
    numArray15[34] = (byte) 201;
    numArray15[35] = (byte) 108;
    numArray15[36] = (byte) 119;
    numArray15[42] = (byte) 107;
    numArray15[38] = (byte) 26;
    numArray15[6] = (byte) 229;
    numArray15[17] = (byte) 240 /*0xF0*/;
    numArray15[29] = (byte) 247;
    numArray15[27] = (byte) 184;
    numArray15[43] = (byte) 178;
    numArray15[0] = (byte) 60;
    numArray15[45] = (byte) 97;
    numArray15[46] = (byte) 123;
    numArray15[48 /*0x30*/] = (byte) 220;
    numArray15[53] = (byte) 159;
    numArray15[49] = (byte) 48 /*0x30*/;
    numArray15[39] = (byte) 209;
    numArray15[32 /*0x20*/] = (byte) 211;
    numArray15[52] = (byte) 184;
    numArray15[28] = (byte) 119;
    numArray15[47] = (byte) 70;
    byte[] numArray16 = new byte[55]
    {
      (byte) 120,
      (byte) 165,
      (byte) 121,
      (byte) 171,
      (byte) 209,
      (byte) 50,
      (byte) 60,
      (byte) 90,
      (byte) 241,
      (byte) 25,
      (byte) 61,
      (byte) 54,
      (byte) 115,
      (byte) 128 /*0x80*/,
      (byte) 110,
      (byte) 173,
      (byte) 184,
      (byte) 215,
      (byte) 189,
      (byte) 3,
      (byte) 183,
      (byte) 196,
      (byte) 196,
      (byte) 110,
      (byte) 90,
      (byte) 22,
      (byte) 0,
      (byte) 52,
      (byte) 247,
      (byte) 151,
      (byte) 45,
      (byte) 181,
      (byte) 63 /*0x3F*/,
      (byte) 71,
      (byte) 163,
      (byte) 38,
      (byte) 91,
      (byte) 227,
      (byte) 144 /*0x90*/,
      (byte) 33,
      (byte) 89,
      (byte) 224 /*0xE0*/,
      (byte) 162,
      (byte) 78,
      (byte) 4,
      (byte) 23,
      (byte) 105,
      (byte) 239,
      (byte) 95,
      (byte) 34,
      (byte) 132,
      (byte) 115,
      (byte) 116,
      (byte) 55,
      (byte) 177
    };
    key.Query(true, 335, numArray15, numArray15);
    Array.Copy((Array) numArray15, 0, (Array) numArray12, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index + 55] ^= numArray16[index];
    byte[] numArray17 = new byte[55]
    {
      (byte) 153,
      (byte) 210,
      (byte) 223,
      (byte) 150,
      (byte) 80 /*0x50*/,
      (byte) 225,
      (byte) 61,
      (byte) 128 /*0x80*/,
      (byte) 79,
      (byte) 22,
      (byte) 89,
      (byte) 188,
      (byte) 26,
      (byte) 120,
      (byte) 172,
      (byte) 149,
      (byte) 169,
      (byte) 63 /*0x3F*/,
      (byte) 240 /*0xF0*/,
      (byte) 127 /*0x7F*/,
      (byte) 29,
      (byte) 182,
      (byte) 234,
      (byte) 115,
      (byte) 225,
      (byte) 141,
      (byte) 131,
      (byte) 103,
      (byte) 127 /*0x7F*/,
      (byte) 164,
      (byte) 178,
      (byte) 75,
      (byte) 124,
      (byte) 172,
      (byte) 91,
      (byte) 189,
      (byte) 96 /*0x60*/,
      (byte) 77,
      (byte) 140,
      (byte) 40,
      (byte) 95,
      (byte) 149,
      (byte) 226,
      (byte) 1,
      (byte) 45,
      (byte) 185,
      (byte) 53,
      (byte) 75,
      (byte) 125,
      (byte) 6,
      (byte) 130,
      (byte) 65,
      (byte) 94,
      (byte) 24,
      (byte) 168
    };
    byte[] numArray18 = new byte[55];
    numArray18[33] = (byte) 240 /*0xF0*/;
    numArray18[41] = (byte) 227;
    numArray18[2] = (byte) 248;
    numArray18[40] = (byte) 178;
    numArray18[1] = (byte) 193;
    numArray18[8] = (byte) 42;
    numArray18[9] = (byte) 176 /*0xB0*/;
    numArray18[7] = (byte) 177;
    numArray18[15] = (byte) 24;
    numArray18[50] = (byte) 149;
    numArray18[45] = (byte) 193;
    numArray18[10] = (byte) 136;
    numArray18[11] = (byte) 195;
    numArray18[13] = (byte) 210;
    numArray18[14] = (byte) 69;
    numArray18[29] = (byte) 201;
    numArray18[24] = (byte) 106;
    numArray18[20] = (byte) 237;
    numArray18[18] = (byte) 4;
    numArray18[19] = (byte) 32 /*0x20*/;
    numArray18[17] = (byte) 215;
    numArray18[21] = (byte) 130;
    numArray18[22] = (byte) 117;
    numArray18[16 /*0x10*/] = (byte) 178;
    numArray18[5] = (byte) 45;
    numArray18[25] = (byte) 160 /*0xA0*/;
    numArray18[26] = (byte) 150;
    numArray18[27] = (byte) 34;
    numArray18[28] = (byte) 46;
    numArray18[47] = (byte) 127 /*0x7F*/;
    numArray18[0] = (byte) 239;
    numArray18[31 /*0x1F*/] = (byte) 148;
    numArray18[12] = (byte) 150;
    numArray18[54] = (byte) 132;
    numArray18[34] = (byte) 124;
    numArray18[35] = (byte) 122;
    numArray18[4] = (byte) 44;
    numArray18[36] = (byte) 237;
    numArray18[38] = (byte) 210;
    numArray18[39] = (byte) 60;
    numArray18[49] = (byte) 238;
    numArray18[32 /*0x20*/] = (byte) 244;
    numArray18[42] = (byte) 28;
    numArray18[43] = (byte) 153;
    numArray18[3] = (byte) 93;
    numArray18[44] = (byte) 76;
    numArray18[46] = (byte) 80 /*0x50*/;
    numArray18[37] = (byte) 192 /*0xC0*/;
    numArray18[48 /*0x30*/] = (byte) 91;
    numArray18[6] = (byte) 194;
    numArray18[23] = (byte) 154;
    numArray18[51] = (byte) 183;
    numArray18[52] = (byte) 219;
    numArray18[53] = (byte) 176 /*0xB0*/;
    numArray18[30] = (byte) 192 /*0xC0*/;
    key.Query(true, 335, numArray17, numArray17);
    Array.Copy((Array) numArray17, 0, (Array) numArray12, 110, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index + 110] ^= numArray18[index];
    byte[] numArray19 = new byte[55]
    {
      (byte) 36,
      (byte) 141,
      (byte) 107,
      (byte) 38,
      (byte) 236,
      (byte) 135,
      (byte) 136,
      (byte) 243,
      (byte) 32 /*0x20*/,
      (byte) 221,
      (byte) 106,
      (byte) 162,
      (byte) 25,
      (byte) 64 /*0x40*/,
      (byte) 195,
      (byte) 233,
      (byte) 253,
      (byte) 151,
      (byte) 243,
      (byte) 155,
      (byte) 74,
      (byte) 38,
      (byte) 174,
      (byte) 145,
      (byte) 8,
      (byte) 91,
      (byte) 167,
      (byte) 251,
      (byte) 225,
      (byte) 85,
      (byte) 106,
      (byte) 227,
      (byte) 106,
      (byte) 213,
      (byte) 208 /*0xD0*/,
      (byte) 11,
      (byte) 93,
      (byte) 163,
      (byte) 156,
      (byte) 13,
      (byte) 174,
      (byte) 170,
      byte.MaxValue,
      (byte) 156,
      (byte) 91,
      (byte) 140,
      (byte) 116,
      (byte) 145,
      (byte) 99,
      (byte) 165,
      (byte) 153,
      (byte) 97,
      (byte) 44,
      (byte) 115,
      (byte) 180
    };
    byte[] numArray20 = new byte[55]
    {
      (byte) 209,
      (byte) 79,
      (byte) 77,
      (byte) 126,
      (byte) 50,
      (byte) 87,
      (byte) 164,
      (byte) 215,
      (byte) 7,
      (byte) 227,
      (byte) 92,
      (byte) 86,
      (byte) 23,
      (byte) 16 /*0x10*/,
      (byte) 199,
      (byte) 151,
      (byte) 22,
      (byte) 196,
      (byte) 105,
      (byte) 197,
      (byte) 200,
      (byte) 185,
      (byte) 221,
      (byte) 250,
      (byte) 43,
      (byte) 15,
      (byte) 85,
      (byte) 84,
      (byte) 173,
      (byte) 63 /*0x3F*/,
      (byte) 131,
      (byte) 241,
      (byte) 122,
      (byte) 181,
      (byte) 166,
      (byte) 161,
      (byte) 76,
      (byte) 45,
      (byte) 122,
      (byte) 136,
      (byte) 180,
      (byte) 0,
      (byte) 48 /*0x30*/,
      (byte) 211,
      (byte) 248,
      (byte) 152,
      (byte) 236,
      (byte) 95,
      (byte) 104,
      (byte) 9,
      (byte) 85,
      (byte) 123,
      (byte) 89,
      (byte) 125,
      (byte) 44
    };
    key.Query(true, 335, numArray19, numArray19);
    Array.Copy((Array) numArray19, 0, (Array) numArray12, 165, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index + 165] ^= numArray20[index];
    byte[] numArray21 = new byte[30];
    numArray21[9] = (byte) 56;
    numArray21[7] = (byte) 114;
    numArray21[2] = (byte) 145;
    numArray21[1] = (byte) 29;
    numArray21[3] = (byte) 223;
    numArray21[5] = (byte) 18;
    numArray21[6] = (byte) 202;
    numArray21[20] = (byte) 18;
    numArray21[8] = (byte) 135;
    numArray21[15] = (byte) 250;
    numArray21[0] = (byte) 218;
    numArray21[26] = (byte) 60;
    numArray21[12] = (byte) 183;
    numArray21[14] = (byte) 89;
    numArray21[24] = (byte) 22;
    numArray21[21] = (byte) 53;
    numArray21[16 /*0x10*/] = (byte) 230;
    numArray21[17] = (byte) 236;
    numArray21[18] = (byte) 160 /*0xA0*/;
    numArray21[19] = (byte) 39;
    numArray21[10] = (byte) 68;
    numArray21[28] = (byte) 9;
    numArray21[22] = (byte) 124;
    numArray21[23] = (byte) 230;
    numArray21[13] = (byte) 140;
    numArray21[25] = (byte) 151;
    numArray21[11] = (byte) 111;
    numArray21[27] = (byte) 188;
    numArray21[4] = (byte) 141;
    numArray21[29] = (byte) 182;
    byte[] numArray22 = new byte[30]
    {
      (byte) 21,
      (byte) 114,
      (byte) 139,
      (byte) 162,
      (byte) 216,
      (byte) 228,
      (byte) 145,
      (byte) 8,
      (byte) 31 /*0x1F*/,
      (byte) 162,
      (byte) 69,
      (byte) 44,
      (byte) 151,
      (byte) 231,
      (byte) 15,
      (byte) 114,
      (byte) 105,
      (byte) 235,
      (byte) 70,
      (byte) 4,
      (byte) 146,
      (byte) 119,
      (byte) 186,
      (byte) 85,
      (byte) 20,
      (byte) 221,
      (byte) 245,
      (byte) 126,
      (byte) 187,
      (byte) 75
    };
    key.Query(true, 335, numArray21, numArray21);
    Array.Copy((Array) numArray21, 0, (Array) numArray12, 220, 30);
    for (int index = 0; index < 30; ++index)
      numArray12[index + 220] ^= numArray22[index];
    return Encoding.UTF8.GetString(numArray12);
  }

  internal static string ssp_appserver_12471()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[284];
      byte[] numArray2 = new byte[55];
      numArray2[23] = (byte) 251;
      numArray2[1] = (byte) 35;
      numArray2[27] = (byte) 97;
      numArray2[20] = (byte) 113;
      numArray2[4] = (byte) 8;
      numArray2[34] = (byte) 18;
      numArray2[28] = (byte) 101;
      numArray2[7] = (byte) 184;
      numArray2[8] = (byte) 203;
      numArray2[25] = (byte) 222;
      numArray2[18] = (byte) 124;
      numArray2[29] = (byte) 42;
      numArray2[12] = (byte) 96 /*0x60*/;
      numArray2[50] = (byte) 11;
      numArray2[3] = (byte) 138;
      numArray2[24] = (byte) 96 /*0x60*/;
      numArray2[16 /*0x10*/] = (byte) 235;
      numArray2[17] = (byte) 35;
      numArray2[11] = (byte) 232;
      numArray2[35] = (byte) 13;
      numArray2[42] = (byte) 187;
      numArray2[19] = (byte) 162;
      numArray2[22] = (byte) 12;
      numArray2[10] = (byte) 243;
      numArray2[52] = (byte) 146;
      numArray2[15] = (byte) 244;
      numArray2[26] = (byte) 229;
      numArray2[45] = (byte) 227;
      numArray2[13] = (byte) 40;
      numArray2[48 /*0x30*/] = (byte) 244;
      numArray2[53] = (byte) 197;
      numArray2[31 /*0x1F*/] = (byte) 38;
      numArray2[32 /*0x20*/] = (byte) 193;
      numArray2[33] = (byte) 3;
      numArray2[21] = (byte) 111;
      numArray2[0] = (byte) 65;
      numArray2[36] = (byte) 132;
      numArray2[9] = (byte) 8;
      numArray2[30] = (byte) 193;
      numArray2[39] = (byte) 108;
      numArray2[2] = (byte) 157;
      numArray2[40] = (byte) 120;
      numArray2[14] = (byte) 182;
      numArray2[49] = (byte) 212;
      numArray2[44] = (byte) 227;
      numArray2[5] = (byte) 211;
      numArray2[46] = (byte) 68;
      numArray2[47] = (byte) 120;
      numArray2[38] = (byte) 15;
      numArray2[41] = (byte) 141;
      numArray2[37] = (byte) 33;
      numArray2[51] = (byte) 178;
      numArray2[6] = (byte) 238;
      numArray2[43] = (byte) 11;
      numArray2[54] = (byte) 95;
      byte[] numArray3 = new byte[55]
      {
        (byte) 155,
        (byte) 42,
        (byte) 137,
        (byte) 24,
        (byte) 240 /*0xF0*/,
        (byte) 25,
        (byte) 119,
        (byte) 20,
        (byte) 194,
        (byte) 208 /*0xD0*/,
        (byte) 150,
        (byte) 241,
        (byte) 136,
        (byte) 183,
        (byte) 180,
        (byte) 215,
        (byte) 10,
        (byte) 89,
        (byte) 151,
        (byte) 164,
        (byte) 191,
        (byte) 123,
        (byte) 186,
        (byte) 155,
        (byte) 91,
        (byte) 109,
        (byte) 249,
        (byte) 28,
        (byte) 224 /*0xE0*/,
        (byte) 60,
        (byte) 220,
        (byte) 5,
        (byte) 233,
        (byte) 139,
        (byte) 118,
        (byte) 133,
        (byte) 164,
        (byte) 62,
        (byte) 109,
        (byte) 65,
        (byte) 217,
        (byte) 222,
        (byte) 35,
        (byte) 118,
        (byte) 230,
        (byte) 242,
        (byte) 190,
        (byte) 60,
        (byte) 230,
        (byte) 48 /*0x30*/,
        (byte) 164,
        (byte) 75,
        (byte) 110,
        (byte) 147,
        (byte) 62
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 137,
        (byte) 172,
        (byte) 144 /*0x90*/,
        (byte) 128 /*0x80*/,
        (byte) 31 /*0x1F*/,
        (byte) 193,
        (byte) 129,
        (byte) 56,
        (byte) 56,
        (byte) 42,
        (byte) 185,
        (byte) 213,
        (byte) 178,
        (byte) 142,
        (byte) 209,
        (byte) 46,
        (byte) 144 /*0x90*/,
        (byte) 62,
        (byte) 168,
        (byte) 154,
        (byte) 60,
        (byte) 2,
        (byte) 192 /*0xC0*/,
        (byte) 179,
        (byte) 10,
        (byte) 34,
        (byte) 45,
        (byte) 221,
        (byte) 110,
        (byte) 17,
        (byte) 97,
        (byte) 39,
        (byte) 35,
        (byte) 224 /*0xE0*/,
        (byte) 88,
        (byte) 75,
        (byte) 84,
        (byte) 254,
        (byte) 204,
        (byte) 121,
        (byte) 152,
        (byte) 139,
        (byte) 221,
        (byte) 146,
        (byte) 44,
        (byte) 140,
        (byte) 152,
        (byte) 212,
        (byte) 112 /*0x70*/,
        (byte) 112 /*0x70*/,
        (byte) 241,
        (byte) 92,
        (byte) 211,
        (byte) 130,
        (byte) 217
      };
      byte[] numArray5 = new byte[55];
      numArray5[30] = (byte) 51;
      numArray5[1] = (byte) 4;
      numArray5[2] = (byte) 219;
      numArray5[3] = (byte) 87;
      numArray5[20] = (byte) 137;
      numArray5[41] = (byte) 1;
      numArray5[43] = (byte) 71;
      numArray5[15] = (byte) 189;
      numArray5[8] = (byte) 236;
      numArray5[9] = (byte) 109;
      numArray5[47] = (byte) 158;
      numArray5[11] = (byte) 86;
      numArray5[12] = (byte) 30;
      numArray5[13] = (byte) 250;
      numArray5[14] = (byte) 82;
      numArray5[49] = (byte) 63 /*0x3F*/;
      numArray5[38] = (byte) 147;
      numArray5[22] = (byte) 67;
      numArray5[18] = (byte) 47;
      numArray5[26] = (byte) 141;
      numArray5[4] = (byte) 31 /*0x1F*/;
      numArray5[29] = (byte) 230;
      numArray5[10] = (byte) 66;
      numArray5[17] = (byte) 74;
      numArray5[31 /*0x1F*/] = (byte) 72;
      numArray5[25] = (byte) 170;
      numArray5[37] = (byte) 66;
      numArray5[33] = (byte) 203;
      numArray5[28] = (byte) 94;
      numArray5[54] = (byte) 100;
      numArray5[24] = (byte) 169;
      numArray5[35] = (byte) 223;
      numArray5[0] = (byte) 136;
      numArray5[16 /*0x10*/] = (byte) 123;
      numArray5[51] = (byte) 175;
      numArray5[32 /*0x20*/] = (byte) 27;
      numArray5[36] = (byte) 144 /*0x90*/;
      numArray5[5] = (byte) 83;
      numArray5[44] = (byte) 230;
      numArray5[39] = (byte) 146;
      numArray5[40] = (byte) 188;
      numArray5[7] = (byte) 114;
      numArray5[42] = (byte) 240 /*0xF0*/;
      numArray5[21] = (byte) 26;
      numArray5[23] = (byte) 6;
      numArray5[46] = (byte) 194;
      numArray5[19] = (byte) 208 /*0xD0*/;
      numArray5[34] = (byte) 22;
      numArray5[48 /*0x30*/] = (byte) 145;
      numArray5[27] = (byte) 88;
      numArray5[50] = (byte) 116;
      numArray5[45] = (byte) 34;
      numArray5[52] = (byte) 200;
      numArray5[53] = (byte) 189;
      numArray5[6] = (byte) 107;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[55]
      {
        (byte) 87,
        (byte) 246,
        (byte) 114,
        (byte) 59,
        (byte) 56,
        (byte) 203,
        (byte) 6,
        (byte) 197,
        (byte) 62,
        (byte) 18,
        (byte) 203,
        (byte) 101,
        (byte) 37,
        (byte) 100,
        (byte) 10,
        (byte) 162,
        (byte) 73,
        (byte) 14,
        (byte) 238,
        (byte) 110,
        (byte) 101,
        (byte) 235,
        (byte) 165,
        (byte) 196,
        (byte) 186,
        (byte) 128 /*0x80*/,
        (byte) 103,
        (byte) 193,
        (byte) 26,
        (byte) 174,
        (byte) 31 /*0x1F*/,
        (byte) 220,
        (byte) 110,
        (byte) 162,
        (byte) 31 /*0x1F*/,
        (byte) 145,
        (byte) 93,
        (byte) 170,
        (byte) 187,
        (byte) 246,
        (byte) 175,
        (byte) 3,
        (byte) 91,
        (byte) 199,
        (byte) 250,
        (byte) 57,
        (byte) 198,
        (byte) 50,
        (byte) 84,
        (byte) 71,
        (byte) 90,
        (byte) 114,
        (byte) 58,
        (byte) 244,
        (byte) 88
      };
      byte[] numArray7 = new byte[55]
      {
        (byte) 170,
        (byte) 124,
        (byte) 135,
        (byte) 249,
        (byte) 157,
        (byte) 139,
        (byte) 136,
        (byte) 1,
        (byte) 167,
        (byte) 109,
        (byte) 8,
        (byte) 204,
        (byte) 41,
        (byte) 78,
        (byte) 135,
        (byte) 32 /*0x20*/,
        (byte) 38,
        (byte) 89,
        (byte) 47,
        (byte) 233,
        (byte) 250,
        (byte) 232,
        (byte) 20,
        (byte) 58,
        (byte) 192 /*0xC0*/,
        (byte) 67,
        (byte) 52,
        (byte) 155,
        (byte) 123,
        (byte) 239,
        (byte) 26,
        (byte) 227,
        (byte) 58,
        (byte) 223,
        (byte) 184,
        (byte) 234,
        (byte) 185,
        (byte) 44,
        (byte) 117,
        (byte) 141,
        (byte) 116,
        (byte) 205,
        (byte) 178,
        (byte) 159,
        (byte) 177,
        (byte) 180,
        (byte) 78,
        (byte) 140,
        (byte) 241,
        (byte) 125,
        (byte) 5,
        (byte) 110,
        (byte) 72,
        (byte) 132,
        (byte) 225
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[55];
      numArray8[7] = (byte) 242;
      numArray8[23] = (byte) 186;
      numArray8[44] = (byte) 68;
      numArray8[2] = (byte) 183;
      numArray8[46] = (byte) 219;
      numArray8[5] = (byte) 171;
      numArray8[6] = (byte) 143;
      numArray8[13] = (byte) 69;
      numArray8[30] = (byte) 122;
      numArray8[16 /*0x10*/] = (byte) 248;
      numArray8[10] = (byte) 130;
      numArray8[50] = (byte) 66;
      numArray8[9] = (byte) 247;
      numArray8[3] = (byte) 100;
      numArray8[32 /*0x20*/] = (byte) 179;
      numArray8[15] = (byte) 82;
      numArray8[0] = (byte) 173;
      numArray8[17] = (byte) 191;
      numArray8[18] = (byte) 97;
      numArray8[38] = (byte) 106;
      numArray8[20] = (byte) 222;
      numArray8[12] = (byte) 120;
      numArray8[39] = (byte) 88;
      numArray8[51] = (byte) 52;
      numArray8[24] = (byte) 53;
      numArray8[11] = (byte) 68;
      numArray8[26] = (byte) 210;
      numArray8[19] = (byte) 144 /*0x90*/;
      numArray8[25] = (byte) 109;
      numArray8[21] = (byte) 202;
      numArray8[49] = (byte) 31 /*0x1F*/;
      numArray8[31 /*0x1F*/] = (byte) 74;
      numArray8[22] = (byte) 128 /*0x80*/;
      numArray8[33] = (byte) 63 /*0x3F*/;
      numArray8[37] = (byte) 145;
      numArray8[35] = (byte) 41;
      numArray8[36] = (byte) 4;
      numArray8[43] = (byte) 66;
      numArray8[8] = (byte) 238;
      numArray8[42] = (byte) 11;
      numArray8[40] = (byte) 203;
      numArray8[41] = (byte) 213;
      numArray8[29] = (byte) 161;
      numArray8[4] = (byte) 69;
      numArray8[53] = (byte) 219;
      numArray8[45] = (byte) 237;
      numArray8[34] = (byte) 175;
      numArray8[47] = (byte) 1;
      numArray8[48 /*0x30*/] = (byte) 187;
      numArray8[28] = (byte) 173;
      numArray8[14] = (byte) 159;
      numArray8[27] = (byte) 83;
      numArray8[52] = (byte) 247;
      numArray8[1] = (byte) 129;
      numArray8[54] = (byte) 115;
      byte[] numArray9 = new byte[55];
      numArray9[28] = (byte) 108;
      numArray9[32 /*0x20*/] = (byte) 227;
      numArray9[52] = (byte) 252;
      numArray9[10] = (byte) 46;
      numArray9[3] = (byte) 101;
      numArray9[19] = (byte) 243;
      numArray9[6] = (byte) 43;
      numArray9[30] = (byte) 76;
      numArray9[8] = (byte) 116;
      numArray9[9] = (byte) 144 /*0x90*/;
      numArray9[24] = (byte) 133;
      numArray9[11] = (byte) 112 /*0x70*/;
      numArray9[12] = (byte) 229;
      numArray9[13] = (byte) 84;
      numArray9[14] = (byte) 23;
      numArray9[15] = (byte) 192 /*0xC0*/;
      numArray9[48 /*0x30*/] = (byte) 132;
      numArray9[34] = (byte) 80 /*0x50*/;
      numArray9[17] = (byte) 141;
      numArray9[35] = (byte) 85;
      numArray9[20] = (byte) 60;
      numArray9[1] = (byte) 177;
      numArray9[23] = (byte) 49;
      numArray9[7] = (byte) 227;
      numArray9[25] = (byte) 183;
      numArray9[45] = (byte) 100;
      numArray9[26] = (byte) 4;
      numArray9[27] = (byte) 17;
      numArray9[5] = (byte) 117;
      numArray9[36] = (byte) 146;
      numArray9[16 /*0x10*/] = (byte) 183;
      numArray9[31 /*0x1F*/] = (byte) 42;
      numArray9[18] = (byte) 205;
      numArray9[21] = (byte) 242;
      numArray9[40] = (byte) 192 /*0xC0*/;
      numArray9[22] = (byte) 85;
      numArray9[43] = (byte) 140;
      numArray9[37] = (byte) 80 /*0x50*/;
      numArray9[41] = (byte) 16 /*0x10*/;
      numArray9[39] = (byte) 179;
      numArray9[46] = (byte) 56;
      numArray9[0] = (byte) 100;
      numArray9[42] = (byte) 5;
      numArray9[33] = (byte) 117;
      numArray9[44] = (byte) 188;
      numArray9[2] = (byte) 240 /*0xF0*/;
      numArray9[54] = (byte) 170;
      numArray9[47] = (byte) 176 /*0xB0*/;
      numArray9[4] = (byte) 175;
      numArray9[38] = (byte) 177;
      numArray9[50] = (byte) 178;
      numArray9[51] = (byte) 52;
      numArray9[29] = (byte) 34;
      numArray9[53] = (byte) 129;
      numArray9[49] = (byte) 180;
      key.Query(true, 335, numArray8, numArray8);
      Array.Copy((Array) numArray8, 0, (Array) numArray1, 165, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 165] ^= numArray9[index];
      byte[] numArray10 = new byte[55]
      {
        (byte) 28,
        (byte) 250,
        (byte) 87,
        (byte) 162,
        (byte) 80 /*0x50*/,
        (byte) 250,
        (byte) 105,
        (byte) 209,
        (byte) 128 /*0x80*/,
        (byte) 246,
        (byte) 10,
        (byte) 37,
        (byte) 230,
        (byte) 198,
        (byte) 133,
        (byte) 119,
        (byte) 15,
        (byte) 72,
        (byte) 158,
        (byte) 46,
        (byte) 235,
        (byte) 66,
        (byte) 71,
        (byte) 184,
        (byte) 198,
        (byte) 91,
        (byte) 49,
        (byte) 170,
        (byte) 73,
        (byte) 252,
        (byte) 223,
        (byte) 110,
        (byte) 128 /*0x80*/,
        (byte) 30,
        (byte) 99,
        (byte) 130,
        (byte) 244,
        (byte) 214,
        (byte) 155,
        (byte) 222,
        (byte) 103,
        (byte) 187,
        (byte) 166,
        (byte) 174,
        (byte) 43,
        (byte) 122,
        (byte) 17,
        (byte) 195,
        (byte) 82,
        (byte) 165,
        (byte) 87,
        (byte) 153,
        (byte) 84,
        (byte) 246,
        (byte) 92
      };
      byte[] numArray11 = new byte[55];
      numArray11[39] = (byte) 143;
      numArray11[1] = (byte) 118;
      numArray11[2] = (byte) 150;
      numArray11[0] = (byte) 124;
      numArray11[4] = (byte) 79;
      numArray11[3] = (byte) 165;
      numArray11[6] = (byte) 241;
      numArray11[40] = (byte) 201;
      numArray11[35] = (byte) 46;
      numArray11[9] = (byte) 177;
      numArray11[25] = (byte) 70;
      numArray11[11] = (byte) 217;
      numArray11[28] = (byte) 42;
      numArray11[44] = (byte) 143;
      numArray11[14] = (byte) 40;
      numArray11[15] = (byte) 25;
      numArray11[16 /*0x10*/] = (byte) 132;
      numArray11[17] = (byte) 158;
      numArray11[18] = (byte) 100;
      numArray11[19] = (byte) 41;
      numArray11[38] = (byte) 227;
      numArray11[21] = (byte) 25;
      numArray11[24] = (byte) 151;
      numArray11[30] = (byte) 79;
      numArray11[27] = (byte) 10;
      numArray11[48 /*0x30*/] = (byte) 244;
      numArray11[23] = (byte) 67;
      numArray11[47] = (byte) 110;
      numArray11[33] = (byte) 37;
      numArray11[29] = (byte) 213;
      numArray11[22] = (byte) 228;
      numArray11[31 /*0x1F*/] = (byte) 240 /*0xF0*/;
      numArray11[45] = (byte) 131;
      numArray11[37] = (byte) 80 /*0x50*/;
      numArray11[34] = (byte) 6;
      numArray11[5] = (byte) 8;
      numArray11[32 /*0x20*/] = (byte) 221;
      numArray11[36] = (byte) 230;
      numArray11[54] = (byte) 49;
      numArray11[8] = (byte) 104;
      numArray11[10] = (byte) 208 /*0xD0*/;
      numArray11[50] = (byte) 168;
      numArray11[42] = (byte) 242;
      numArray11[20] = (byte) 86;
      numArray11[13] = (byte) 193;
      numArray11[26] = (byte) 45;
      numArray11[51] = (byte) 208 /*0xD0*/;
      numArray11[46] = (byte) 146;
      numArray11[53] = (byte) 186;
      numArray11[49] = (byte) 13;
      numArray11[12] = (byte) 93;
      numArray11[41] = (byte) 28;
      numArray11[52] = (byte) 4;
      numArray11[7] = (byte) 63 /*0x3F*/;
      numArray11[43] = (byte) 228;
      key.Query(true, 335, numArray10, numArray10);
      Array.Copy((Array) numArray10, 0, (Array) numArray1, 220, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 220] ^= numArray11[index];
      byte[] numArray12 = new byte[9];
      numArray12[5] = (byte) 228;
      numArray12[1] = (byte) 144 /*0x90*/;
      numArray12[0] = (byte) 18;
      numArray12[6] = (byte) 223;
      numArray12[4] = (byte) 103;
      numArray12[7] = (byte) 209;
      numArray12[8] = (byte) 129;
      numArray12[3] = (byte) 90;
      numArray12[2] = (byte) 170;
      byte[] numArray13 = new byte[9];
      numArray13[7] = (byte) 126;
      numArray13[1] = (byte) 73;
      numArray13[2] = (byte) 253;
      numArray13[3] = (byte) 117;
      numArray13[5] = (byte) 198;
      numArray13[8] = (byte) 110;
      numArray13[6] = (byte) 88;
      numArray13[4] = (byte) 123;
      numArray13[0] = (byte) 234;
      key.Query(true, 335, numArray12, numArray12);
      Array.Copy((Array) numArray12, 0, (Array) numArray1, 275, 9);
      for (int index = 0; index < 9; ++index)
        numArray1[index + 275] ^= numArray13[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray14 = new byte[284];
    byte[] numArray15 = new byte[55]
    {
      (byte) 90,
      (byte) 149,
      (byte) 75,
      (byte) 179,
      (byte) 67,
      (byte) 106,
      (byte) 252,
      (byte) 186,
      (byte) 64 /*0x40*/,
      (byte) 79,
      (byte) 67,
      (byte) 198,
      (byte) 14,
      (byte) 186,
      (byte) 67,
      (byte) 135,
      (byte) 26,
      (byte) 245,
      (byte) 224 /*0xE0*/,
      (byte) 230,
      (byte) 233,
      (byte) 16 /*0x10*/,
      (byte) 48 /*0x30*/,
      (byte) 196,
      (byte) 25,
      (byte) 20,
      (byte) 25,
      (byte) 169,
      (byte) 77,
      (byte) 94,
      (byte) 98,
      (byte) 89,
      (byte) 120,
      (byte) 250,
      (byte) 192 /*0xC0*/,
      (byte) 112 /*0x70*/,
      (byte) 141,
      (byte) 128 /*0x80*/,
      (byte) 233,
      (byte) 212,
      (byte) 9,
      (byte) 239,
      (byte) 222,
      (byte) 173,
      (byte) 54,
      (byte) 59,
      (byte) 238,
      (byte) 187,
      (byte) 189,
      (byte) 134,
      (byte) 238,
      (byte) 139,
      (byte) 165,
      (byte) 114,
      (byte) 156
    };
    byte[] numArray16 = new byte[55]
    {
      (byte) 254,
      (byte) 153,
      (byte) 149,
      (byte) 84,
      (byte) 18,
      (byte) 161,
      (byte) 220,
      (byte) 69,
      (byte) 6,
      (byte) 95,
      (byte) 131,
      (byte) 253,
      (byte) 113,
      (byte) 216,
      (byte) 234,
      (byte) 246,
      (byte) 169,
      (byte) 60,
      (byte) 41,
      (byte) 42,
      (byte) 203,
      (byte) 32 /*0x20*/,
      (byte) 135,
      (byte) 45,
      (byte) 72,
      (byte) 188,
      (byte) 125,
      (byte) 54,
      (byte) 42,
      (byte) 80 /*0x50*/,
      (byte) 104,
      (byte) 197,
      (byte) 163,
      (byte) 169,
      (byte) 149,
      (byte) 15,
      (byte) 155,
      (byte) 158,
      (byte) 147,
      (byte) 217,
      (byte) 213,
      (byte) 243,
      (byte) 243,
      (byte) 156,
      (byte) 210,
      (byte) 119,
      (byte) 25,
      (byte) 27,
      (byte) 151,
      (byte) 19,
      (byte) 246,
      (byte) 17,
      (byte) 64 /*0x40*/,
      (byte) 47,
      (byte) 84
    };
    key.Query(true, 335, numArray15, numArray15);
    Array.Copy((Array) numArray15, 0, (Array) numArray14, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray14[index] ^= numArray16[index];
    byte[] numArray17 = new byte[55]
    {
      (byte) 135,
      (byte) 154,
      (byte) 46,
      (byte) 80 /*0x50*/,
      (byte) 83,
      (byte) 109,
      (byte) 225,
      (byte) 236,
      (byte) 234,
      (byte) 77,
      (byte) 20,
      (byte) 249,
      (byte) 210,
      (byte) 249,
      (byte) 153,
      (byte) 11,
      (byte) 11,
      (byte) 221,
      (byte) 250,
      (byte) 50,
      (byte) 130,
      (byte) 141,
      (byte) 221,
      (byte) 37,
      (byte) 93,
      (byte) 235,
      (byte) 199,
      (byte) 112 /*0x70*/,
      (byte) 214,
      (byte) 179,
      (byte) 6,
      (byte) 93,
      (byte) 119,
      (byte) 11,
      (byte) 5,
      (byte) 230,
      (byte) 187,
      (byte) 132,
      (byte) 181,
      (byte) 130,
      (byte) 252,
      (byte) 192 /*0xC0*/,
      (byte) 76,
      (byte) 237,
      (byte) 218,
      (byte) 194,
      (byte) 234,
      (byte) 140,
      (byte) 84,
      (byte) 128 /*0x80*/,
      (byte) 154,
      (byte) 148,
      (byte) 162,
      (byte) 22,
      (byte) 226
    };
    byte[] numArray18 = new byte[55];
    numArray18[36] = (byte) 96 /*0x60*/;
    numArray18[1] = (byte) 159;
    numArray18[23] = (byte) 3;
    numArray18[25] = (byte) 190;
    numArray18[4] = (byte) 36;
    numArray18[5] = (byte) 62;
    numArray18[6] = (byte) 234;
    numArray18[42] = (byte) 69;
    numArray18[40] = byte.MaxValue;
    numArray18[15] = (byte) 173;
    numArray18[32 /*0x20*/] = (byte) 101;
    numArray18[11] = (byte) 12;
    numArray18[16 /*0x10*/] = (byte) 49;
    numArray18[22] = (byte) 199;
    numArray18[17] = (byte) 193;
    numArray18[37] = (byte) 99;
    numArray18[3] = (byte) 46;
    numArray18[24] = (byte) 220;
    numArray18[18] = (byte) 176 /*0xB0*/;
    numArray18[9] = (byte) 254;
    numArray18[20] = (byte) 244;
    numArray18[19] = (byte) 205;
    numArray18[27] = (byte) 115;
    numArray18[44] = (byte) 169;
    numArray18[8] = (byte) 4;
    numArray18[47] = (byte) 56;
    numArray18[26] = (byte) 2;
    numArray18[14] = (byte) 21;
    numArray18[28] = (byte) 134;
    numArray18[29] = (byte) 71;
    numArray18[30] = (byte) 35;
    numArray18[31 /*0x1F*/] = (byte) 127 /*0x7F*/;
    numArray18[35] = (byte) 182;
    numArray18[50] = (byte) 127 /*0x7F*/;
    numArray18[34] = (byte) 148;
    numArray18[33] = (byte) 17;
    numArray18[7] = byte.MaxValue;
    numArray18[39] = (byte) 66;
    numArray18[12] = (byte) 46;
    numArray18[53] = (byte) 123;
    numArray18[46] = (byte) 227;
    numArray18[41] = (byte) 246;
    numArray18[21] = (byte) 102;
    numArray18[43] = (byte) 124;
    numArray18[0] = (byte) 105;
    numArray18[45] = (byte) 155;
    numArray18[10] = (byte) 91;
    numArray18[13] = (byte) 166;
    numArray18[48 /*0x30*/] = (byte) 224 /*0xE0*/;
    numArray18[38] = (byte) 6;
    numArray18[49] = (byte) 180;
    numArray18[51] = (byte) 103;
    numArray18[52] = (byte) 119;
    numArray18[2] = (byte) 45;
    numArray18[54] = (byte) 182;
    key.Query(true, 335, numArray17, numArray17);
    Array.Copy((Array) numArray17, 0, (Array) numArray14, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray14[index + 55] ^= numArray18[index];
    byte[] numArray19 = new byte[55];
    numArray19[22] = (byte) 92;
    numArray19[1] = (byte) 86;
    numArray19[2] = (byte) 102;
    numArray19[3] = (byte) 107;
    numArray19[4] = (byte) 85;
    numArray19[5] = (byte) 190;
    numArray19[49] = (byte) 147;
    numArray19[10] = (byte) 158;
    numArray19[18] = (byte) 23;
    numArray19[9] = (byte) 33;
    numArray19[30] = (byte) 128 /*0x80*/;
    numArray19[11] = (byte) 232;
    numArray19[17] = (byte) 170;
    numArray19[13] = (byte) 157;
    numArray19[14] = (byte) 245;
    numArray19[50] = (byte) 144 /*0x90*/;
    numArray19[16 /*0x10*/] = (byte) 154;
    numArray19[54] = (byte) 130;
    numArray19[23] = (byte) 221;
    numArray19[28] = (byte) 4;
    numArray19[20] = (byte) 74;
    numArray19[21] = (byte) 38;
    numArray19[38] = (byte) 54;
    numArray19[6] = (byte) 30;
    numArray19[24] = (byte) 162;
    numArray19[26] = (byte) 38;
    numArray19[12] = (byte) 137;
    numArray19[53] = (byte) 139;
    numArray19[37] = (byte) 113;
    numArray19[29] = (byte) 143;
    numArray19[39] = (byte) 235;
    numArray19[0] = (byte) 190;
    numArray19[31 /*0x1F*/] = (byte) 8;
    numArray19[32 /*0x20*/] = (byte) 82;
    numArray19[34] = (byte) 244;
    numArray19[25] = (byte) 121;
    numArray19[36] = (byte) 231;
    numArray19[8] = (byte) 251;
    numArray19[15] = (byte) 217;
    numArray19[35] = (byte) 59;
    numArray19[33] = (byte) 92;
    numArray19[42] = (byte) 41;
    numArray19[40] = (byte) 246;
    numArray19[43] = (byte) 29;
    numArray19[27] = (byte) 6;
    numArray19[45] = (byte) 175;
    numArray19[52] = (byte) 8;
    numArray19[47] = (byte) 145;
    numArray19[48 /*0x30*/] = (byte) 29;
    numArray19[41] = (byte) 208 /*0xD0*/;
    numArray19[19] = (byte) 44;
    numArray19[51] = (byte) 53;
    numArray19[46] = (byte) 24;
    numArray19[7] = (byte) 213;
    numArray19[44] = (byte) 132;
    byte[] numArray20 = new byte[55]
    {
      (byte) 234,
      (byte) 155,
      (byte) 66,
      (byte) 195,
      (byte) 195,
      (byte) 59,
      (byte) 159,
      (byte) 126,
      (byte) 191,
      (byte) 18,
      (byte) 32 /*0x20*/,
      (byte) 28,
      (byte) 220,
      (byte) 145,
      (byte) 122,
      (byte) 226,
      (byte) 120,
      (byte) 43,
      (byte) 135,
      (byte) 141,
      byte.MaxValue,
      (byte) 94,
      (byte) 20,
      (byte) 159,
      (byte) 111,
      (byte) 239,
      (byte) 111,
      (byte) 139,
      (byte) 118,
      (byte) 212,
      (byte) 56,
      (byte) 215,
      (byte) 170,
      (byte) 163,
      (byte) 149,
      (byte) 246,
      (byte) 44,
      (byte) 220,
      (byte) 78,
      (byte) 207,
      (byte) 225,
      (byte) 200,
      (byte) 56,
      (byte) 10,
      (byte) 228,
      (byte) 100,
      (byte) 6,
      (byte) 99,
      (byte) 48 /*0x30*/,
      (byte) 38,
      (byte) 233,
      (byte) 138,
      (byte) 79,
      (byte) 119,
      (byte) 167
    };
    key.Query(true, 335, numArray19, numArray19);
    Array.Copy((Array) numArray19, 0, (Array) numArray14, 110, 55);
    for (int index = 0; index < 55; ++index)
      numArray14[index + 110] ^= numArray20[index];
    byte[] numArray21 = new byte[55];
    numArray21[46] = (byte) 233;
    numArray21[29] = (byte) 35;
    numArray21[2] = (byte) 121;
    numArray21[36] = (byte) 225;
    numArray21[26] = (byte) 209;
    numArray21[1] = (byte) 213;
    numArray21[21] = (byte) 105;
    numArray21[38] = (byte) 175;
    numArray21[3] = (byte) 2;
    numArray21[22] = (byte) 217;
    numArray21[10] = (byte) 82;
    numArray21[11] = (byte) 76;
    numArray21[12] = (byte) 121;
    numArray21[13] = (byte) 147;
    numArray21[48 /*0x30*/] = (byte) 229;
    numArray21[5] = (byte) 95;
    numArray21[16 /*0x10*/] = (byte) 215;
    numArray21[15] = (byte) 25;
    numArray21[44] = (byte) 152;
    numArray21[49] = (byte) 167;
    numArray21[20] = (byte) 194;
    numArray21[53] = (byte) 92;
    numArray21[19] = (byte) 62;
    numArray21[42] = (byte) 90;
    numArray21[32 /*0x20*/] = (byte) 193;
    numArray21[7] = (byte) 86;
    numArray21[37] = (byte) 80 /*0x50*/;
    numArray21[27] = (byte) 152;
    numArray21[0] = (byte) 47;
    numArray21[34] = (byte) 47;
    numArray21[30] = (byte) 113;
    numArray21[8] = (byte) 148;
    numArray21[33] = (byte) 132;
    numArray21[23] = (byte) 118;
    numArray21[18] = (byte) 121;
    numArray21[35] = (byte) 210;
    numArray21[6] = (byte) 107;
    numArray21[47] = (byte) 12;
    numArray21[52] = (byte) 144 /*0x90*/;
    numArray21[39] = (byte) 160 /*0xA0*/;
    numArray21[40] = (byte) 82;
    numArray21[41] = (byte) 37;
    numArray21[24] = (byte) 172;
    numArray21[43] = (byte) 74;
    numArray21[14] = (byte) 129;
    numArray21[4] = (byte) 47;
    numArray21[25] = (byte) 133;
    numArray21[31 /*0x1F*/] = (byte) 208 /*0xD0*/;
    numArray21[9] = (byte) 206;
    numArray21[17] = (byte) 87;
    numArray21[28] = (byte) 138;
    numArray21[51] = (byte) 254;
    numArray21[50] = (byte) 4;
    numArray21[45] = (byte) 155;
    numArray21[54] = (byte) 250;
    byte[] numArray22 = new byte[55];
    numArray22[43] = (byte) 140;
    numArray22[1] = (byte) 192 /*0xC0*/;
    numArray22[26] = (byte) 27;
    numArray22[50] = (byte) 6;
    numArray22[4] = (byte) 115;
    numArray22[14] = (byte) 4;
    numArray22[16 /*0x10*/] = (byte) 161;
    numArray22[0] = (byte) 133;
    numArray22[25] = byte.MaxValue;
    numArray22[9] = (byte) 99;
    numArray22[5] = (byte) 75;
    numArray22[44] = (byte) 39;
    numArray22[12] = byte.MaxValue;
    numArray22[48 /*0x30*/] = (byte) 242;
    numArray22[33] = (byte) 191;
    numArray22[2] = (byte) 50;
    numArray22[6] = (byte) 171;
    numArray22[35] = (byte) 22;
    numArray22[53] = (byte) 51;
    numArray22[19] = (byte) 8;
    numArray22[20] = (byte) 204;
    numArray22[3] = (byte) 98;
    numArray22[18] = (byte) 250;
    numArray22[23] = (byte) 81;
    numArray22[24] = (byte) 234;
    numArray22[21] = (byte) 107;
    numArray22[40] = (byte) 86;
    numArray22[27] = (byte) 24;
    numArray22[28] = (byte) 91;
    numArray22[49] = (byte) 16 /*0x10*/;
    numArray22[30] = (byte) 32 /*0x20*/;
    numArray22[31 /*0x1F*/] = (byte) 207;
    numArray22[15] = (byte) 192 /*0xC0*/;
    numArray22[13] = (byte) 38;
    numArray22[34] = (byte) 139;
    numArray22[22] = (byte) 236;
    numArray22[36] = (byte) 58;
    numArray22[38] = (byte) 143;
    numArray22[39] = (byte) 148;
    numArray22[8] = (byte) 7;
    numArray22[11] = (byte) 162;
    numArray22[41] = (byte) 130;
    numArray22[42] = (byte) 90;
    numArray22[29] = (byte) 107;
    numArray22[10] = (byte) 191;
    numArray22[45] = (byte) 14;
    numArray22[46] = (byte) 117;
    numArray22[47] = (byte) 44;
    numArray22[52] = (byte) 55;
    numArray22[32 /*0x20*/] = (byte) 167;
    numArray22[17] = (byte) 16 /*0x10*/;
    numArray22[51] = (byte) 175;
    numArray22[7] = (byte) 145;
    numArray22[37] = (byte) 197;
    numArray22[54] = (byte) 20;
    key.Query(true, 335, numArray21, numArray21);
    Array.Copy((Array) numArray21, 0, (Array) numArray14, 165, 55);
    for (int index = 0; index < 55; ++index)
      numArray14[index + 165] ^= numArray22[index];
    byte[] numArray23 = new byte[55];
    numArray23[44] = (byte) 76;
    numArray23[24] = (byte) 112 /*0x70*/;
    numArray23[2] = (byte) 105;
    numArray23[41] = (byte) 49;
    numArray23[4] = (byte) 162;
    numArray23[42] = (byte) 29;
    numArray23[6] = (byte) 177;
    numArray23[7] = (byte) 208 /*0xD0*/;
    numArray23[8] = (byte) 184;
    numArray23[39] = (byte) 249;
    numArray23[10] = (byte) 61;
    numArray23[20] = (byte) 160 /*0xA0*/;
    numArray23[11] = (byte) 73;
    numArray23[13] = (byte) 157;
    numArray23[26] = (byte) 91;
    numArray23[5] = (byte) 118;
    numArray23[16 /*0x10*/] = (byte) 124;
    numArray23[48 /*0x30*/] = (byte) 25;
    numArray23[18] = (byte) 211;
    numArray23[46] = (byte) 236;
    numArray23[14] = (byte) 216;
    numArray23[45] = (byte) 98;
    numArray23[12] = (byte) 112 /*0x70*/;
    numArray23[47] = (byte) 121;
    numArray23[1] = (byte) 69;
    numArray23[25] = (byte) 31 /*0x1F*/;
    numArray23[38] = (byte) 190;
    numArray23[27] = (byte) 175;
    numArray23[17] = (byte) 10;
    numArray23[29] = (byte) 150;
    numArray23[30] = (byte) 237;
    numArray23[23] = (byte) 28;
    numArray23[32 /*0x20*/] = (byte) 32 /*0x20*/;
    numArray23[33] = (byte) 156;
    numArray23[34] = (byte) 94;
    numArray23[35] = (byte) 110;
    numArray23[3] = (byte) 173;
    numArray23[37] = (byte) 155;
    numArray23[15] = (byte) 173;
    numArray23[9] = (byte) 51;
    numArray23[40] = (byte) 229;
    numArray23[21] = (byte) 242;
    numArray23[36] = (byte) 140;
    numArray23[43] = (byte) 182;
    numArray23[28] = (byte) 103;
    numArray23[19] = (byte) 142;
    numArray23[0] = (byte) 92;
    numArray23[22] = (byte) 22;
    numArray23[50] = (byte) 118;
    numArray23[49] = (byte) 6;
    numArray23[53] = (byte) 3;
    numArray23[51] = (byte) 250;
    numArray23[52] = (byte) 70;
    numArray23[54] = (byte) 43;
    numArray23[31 /*0x1F*/] = (byte) 18;
    byte[] numArray24 = new byte[55]
    {
      (byte) 36,
      (byte) 235,
      (byte) 53,
      (byte) 49,
      (byte) 80 /*0x50*/,
      (byte) 168,
      (byte) 60,
      (byte) 43,
      (byte) 22,
      (byte) 4,
      (byte) 208 /*0xD0*/,
      (byte) 75,
      (byte) 62,
      (byte) 229,
      (byte) 68,
      (byte) 233,
      (byte) 245,
      (byte) 199,
      (byte) 103,
      (byte) 130,
      (byte) 102,
      (byte) 143,
      (byte) 98,
      (byte) 210,
      (byte) 150,
      (byte) 155,
      (byte) 150,
      (byte) 203,
      (byte) 222,
      (byte) 144 /*0x90*/,
      (byte) 32 /*0x20*/,
      (byte) 132,
      (byte) 109,
      (byte) 209,
      (byte) 162,
      (byte) 204,
      (byte) 89,
      (byte) 33,
      (byte) 24,
      (byte) 0,
      (byte) 118,
      (byte) 90,
      (byte) 226,
      (byte) 247,
      (byte) 154,
      (byte) 247,
      (byte) 108,
      (byte) 191,
      (byte) 5,
      (byte) 208 /*0xD0*/,
      (byte) 249,
      (byte) 59,
      (byte) 50,
      (byte) 22,
      (byte) 159
    };
    key.Query(true, 335, numArray23, numArray23);
    Array.Copy((Array) numArray23, 0, (Array) numArray14, 220, 55);
    for (int index = 0; index < 55; ++index)
      numArray14[index + 220] ^= numArray24[index];
    byte[] numArray25 = new byte[9];
    numArray25[6] = (byte) 0;
    numArray25[3] = (byte) 56;
    numArray25[5] = (byte) 8;
    numArray25[1] = (byte) 47;
    numArray25[4] = (byte) 108;
    numArray25[2] = (byte) 80 /*0x50*/;
    numArray25[8] = (byte) 94;
    numArray25[7] = (byte) 82;
    numArray25[0] = (byte) 124;
    byte[] numArray26 = new byte[9]
    {
      (byte) 254,
      (byte) 91,
      (byte) 233,
      (byte) 93,
      (byte) 226,
      (byte) 178,
      (byte) 155,
      (byte) 93,
      (byte) 83
    };
    key.Query(true, 335, numArray25, numArray25);
    Array.Copy((Array) numArray25, 0, (Array) numArray14, 275, 9);
    for (int index = 0; index < 9; ++index)
      numArray14[index + 275] ^= numArray26[index];
    return Encoding.UTF8.GetString(numArray14);
  }

  internal static string ssp_appserver_12472()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[99];
      byte[] numArray2 = new byte[55]
      {
        (byte) 211,
        (byte) 26,
        (byte) 166,
        (byte) 230,
        (byte) 122,
        (byte) 139,
        (byte) 68,
        (byte) 97,
        (byte) 194,
        (byte) 181,
        (byte) 96 /*0x60*/,
        (byte) 113,
        (byte) 194,
        (byte) 47,
        (byte) 234,
        (byte) 166,
        (byte) 119,
        (byte) 192 /*0xC0*/,
        (byte) 251,
        (byte) 31 /*0x1F*/,
        (byte) 195,
        (byte) 47,
        (byte) 181,
        (byte) 240 /*0xF0*/,
        (byte) 134,
        (byte) 4,
        (byte) 20,
        (byte) 157,
        (byte) 111,
        (byte) 149,
        (byte) 21,
        (byte) 186,
        (byte) 223,
        (byte) 206,
        (byte) 92,
        (byte) 54,
        (byte) 145,
        (byte) 134,
        (byte) 141,
        (byte) 117,
        (byte) 13,
        (byte) 253,
        (byte) 71,
        (byte) 173,
        (byte) 233,
        (byte) 183,
        (byte) 30,
        (byte) 82,
        (byte) 39,
        (byte) 85,
        (byte) 77,
        (byte) 230,
        (byte) 221,
        (byte) 208 /*0xD0*/,
        (byte) 235
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 249,
        (byte) 253,
        (byte) 134,
        (byte) 98,
        (byte) 199,
        (byte) 155,
        (byte) 132,
        (byte) 99,
        (byte) 132,
        (byte) 211,
        (byte) 24,
        (byte) 11,
        (byte) 6,
        (byte) 23,
        (byte) 183,
        (byte) 231,
        (byte) 13,
        (byte) 132,
        (byte) 106,
        (byte) 62,
        (byte) 113,
        (byte) 49,
        (byte) 3,
        (byte) 125,
        (byte) 27,
        (byte) 180,
        (byte) 251,
        (byte) 84,
        (byte) 60,
        (byte) 159,
        (byte) 127 /*0x7F*/,
        (byte) 94,
        (byte) 44,
        (byte) 48 /*0x30*/,
        (byte) 192 /*0xC0*/,
        (byte) 204,
        (byte) 94,
        (byte) 96 /*0x60*/,
        (byte) 189,
        (byte) 144 /*0x90*/,
        (byte) 127 /*0x7F*/,
        (byte) 219,
        (byte) 202,
        (byte) 20,
        (byte) 215,
        (byte) 254,
        (byte) 117,
        (byte) 242,
        (byte) 31 /*0x1F*/,
        (byte) 53,
        (byte) 199,
        (byte) 147,
        (byte) 150,
        (byte) 211,
        (byte) 145
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[44]
      {
        (byte) 182,
        (byte) 95,
        (byte) 118,
        (byte) 118,
        (byte) 167,
        (byte) 241,
        (byte) 44,
        (byte) 160 /*0xA0*/,
        (byte) 165,
        (byte) 146,
        (byte) 244,
        (byte) 210,
        (byte) 179,
        (byte) 20,
        (byte) 240 /*0xF0*/,
        (byte) 51,
        (byte) 134,
        (byte) 17,
        (byte) 198,
        (byte) 231,
        (byte) 7,
        (byte) 7,
        (byte) 206,
        (byte) 230,
        (byte) 193,
        (byte) 117,
        (byte) 25,
        (byte) 72,
        (byte) 46,
        (byte) 220,
        (byte) 15,
        (byte) 227,
        (byte) 185,
        (byte) 209,
        (byte) 219,
        (byte) 38,
        (byte) 23,
        (byte) 254,
        (byte) 101,
        (byte) 11,
        (byte) 26,
        (byte) 13,
        (byte) 68,
        (byte) 37
      };
      byte[] numArray5 = new byte[44]
      {
        (byte) 18,
        (byte) 5,
        (byte) 73,
        (byte) 239,
        (byte) 111,
        (byte) 40,
        (byte) 57,
        (byte) 24,
        (byte) 26,
        (byte) 22,
        (byte) 28,
        (byte) 163,
        byte.MaxValue,
        (byte) 244,
        (byte) 56,
        (byte) 126,
        (byte) 8,
        (byte) 190,
        (byte) 180,
        (byte) 137,
        (byte) 23,
        (byte) 52,
        (byte) 177,
        (byte) 82,
        (byte) 31 /*0x1F*/,
        (byte) 18,
        (byte) 239,
        (byte) 71,
        (byte) 250,
        (byte) 7,
        (byte) 232,
        (byte) 218,
        (byte) 94,
        (byte) 55,
        (byte) 200,
        (byte) 217,
        (byte) 5,
        (byte) 24,
        (byte) 142,
        (byte) 215,
        (byte) 226,
        (byte) 207,
        (byte) 105,
        (byte) 53
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 44);
      for (int index = 0; index < 44; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[20];
      byte[] response = new byte[20];
      Array.Copy((Array) sc_12465.sspq, 0, (Array) numArray6, 0, 20);
      key.Query(true, 335, numArray6, response);
      Array.Copy((Array) sc_12465.sspr, 0, (Array) numArray6, 0, 20);
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
    byte[] numArray7 = new byte[99];
    byte[] numArray8 = new byte[55];
    numArray8[51] = (byte) 49;
    numArray8[32 /*0x20*/] = (byte) 103;
    numArray8[42] = (byte) 236;
    numArray8[30] = (byte) 162;
    numArray8[0] = (byte) 57;
    numArray8[29] = (byte) 123;
    numArray8[6] = (byte) 205;
    numArray8[54] = (byte) 109;
    numArray8[10] = (byte) 188;
    numArray8[9] = (byte) 169;
    numArray8[28] = (byte) 156;
    numArray8[11] = (byte) 65;
    numArray8[46] = (byte) 47;
    numArray8[36] = (byte) 150;
    numArray8[25] = (byte) 52;
    numArray8[15] = (byte) 123;
    numArray8[16 /*0x10*/] = (byte) 133;
    numArray8[41] = (byte) 100;
    numArray8[27] = (byte) 53;
    numArray8[39] = (byte) 106;
    numArray8[7] = (byte) 249;
    numArray8[21] = (byte) 44;
    numArray8[22] = (byte) 150;
    numArray8[4] = (byte) 171;
    numArray8[26] = (byte) 51;
    numArray8[12] = (byte) 2;
    numArray8[24] = (byte) 73;
    numArray8[20] = (byte) 115;
    numArray8[17] = (byte) 80 /*0x50*/;
    numArray8[23] = (byte) 135;
    numArray8[3] = (byte) 135;
    numArray8[31 /*0x1F*/] = (byte) 25;
    numArray8[5] = (byte) 186;
    numArray8[33] = (byte) 146;
    numArray8[19] = (byte) 253;
    numArray8[35] = (byte) 146;
    numArray8[18] = (byte) 190;
    numArray8[37] = (byte) 208 /*0xD0*/;
    numArray8[38] = (byte) 18;
    numArray8[34] = (byte) 8;
    numArray8[40] = (byte) 238;
    numArray8[13] = (byte) 216;
    numArray8[2] = (byte) 28;
    numArray8[43] = (byte) 231;
    numArray8[44] = (byte) 211;
    numArray8[45] = (byte) 115;
    numArray8[8] = (byte) 194;
    numArray8[47] = byte.MaxValue;
    numArray8[1] = (byte) 202;
    numArray8[49] = (byte) 245;
    numArray8[50] = (byte) 67;
    numArray8[52] = (byte) 81;
    numArray8[14] = (byte) 235;
    numArray8[53] = (byte) 27;
    numArray8[48 /*0x30*/] = (byte) 249;
    byte[] numArray9 = new byte[55]
    {
      (byte) 148,
      (byte) 213,
      (byte) 108,
      (byte) 193,
      (byte) 242,
      (byte) 169,
      (byte) 27,
      (byte) 20,
      (byte) 218,
      (byte) 76,
      (byte) 235,
      (byte) 26,
      (byte) 176 /*0xB0*/,
      (byte) 148,
      (byte) 52,
      (byte) 134,
      (byte) 100,
      (byte) 215,
      (byte) 60,
      (byte) 181,
      (byte) 169,
      (byte) 75,
      (byte) 197,
      (byte) 179,
      (byte) 33,
      (byte) 215,
      (byte) 1,
      (byte) 18,
      (byte) 191,
      (byte) 185,
      (byte) 68,
      (byte) 215,
      (byte) 210,
      (byte) 73,
      (byte) 108,
      (byte) 5,
      (byte) 25,
      byte.MaxValue,
      (byte) 62,
      (byte) 80 /*0x50*/,
      (byte) 28,
      (byte) 83,
      (byte) 102,
      (byte) 204,
      (byte) 240 /*0xF0*/,
      (byte) 216,
      (byte) 188,
      (byte) 60,
      (byte) 37,
      (byte) 36,
      (byte) 57,
      (byte) 77,
      (byte) 162,
      (byte) 138,
      (byte) 65
    };
    key.Query(true, 335, numArray8, numArray8);
    Array.Copy((Array) numArray8, 0, (Array) numArray7, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray7[index] ^= numArray9[index];
    byte[] numArray10 = new byte[44]
    {
      (byte) 224 /*0xE0*/,
      (byte) 105,
      (byte) 114,
      (byte) 83,
      (byte) 90,
      (byte) 181,
      (byte) 14,
      (byte) 149,
      (byte) 218,
      (byte) 142,
      (byte) 74,
      (byte) 213,
      (byte) 250,
      (byte) 226,
      (byte) 35,
      (byte) 162,
      (byte) 114,
      (byte) 183,
      (byte) 7,
      (byte) 154,
      (byte) 205,
      (byte) 130,
      (byte) 59,
      (byte) 68,
      (byte) 4,
      (byte) 155,
      (byte) 150,
      (byte) 170,
      (byte) 125,
      (byte) 202,
      (byte) 64 /*0x40*/,
      (byte) 198,
      (byte) 46,
      (byte) 216,
      (byte) 34,
      (byte) 39,
      (byte) 40,
      (byte) 142,
      (byte) 252,
      (byte) 81,
      (byte) 151,
      (byte) 76,
      (byte) 86,
      (byte) 11
    };
    byte[] numArray11 = new byte[44]
    {
      (byte) 202,
      (byte) 131,
      (byte) 253,
      (byte) 49,
      (byte) 14,
      (byte) 200,
      (byte) 4,
      (byte) 63 /*0x3F*/,
      (byte) 33,
      (byte) 198,
      (byte) 153,
      (byte) 85,
      (byte) 180,
      (byte) 250,
      byte.MaxValue,
      (byte) 88,
      (byte) 209,
      (byte) 122,
      (byte) 106,
      (byte) 161,
      (byte) 78,
      (byte) 136,
      (byte) 91,
      (byte) 25,
      (byte) 162,
      (byte) 194,
      (byte) 135,
      (byte) 146,
      (byte) 13,
      (byte) 37,
      (byte) 38,
      (byte) 51,
      (byte) 40,
      (byte) 94,
      (byte) 201,
      (byte) 29,
      (byte) 123,
      (byte) 92,
      (byte) 241,
      (byte) 188,
      (byte) 68,
      (byte) 144 /*0x90*/,
      (byte) 3,
      (byte) 155
    };
    key.Query(true, 335, numArray10, numArray10);
    Array.Copy((Array) numArray10, 0, (Array) numArray7, 55, 44);
    for (int index = 0; index < 44; ++index)
      numArray7[index + 55] ^= numArray11[index];
    return Encoding.UTF8.GetString(numArray7);
  }

  internal static int ssp_appserver_12473(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[5] = (byte) 184;
    sourceArray1[13] = (byte) 23;
    sourceArray1[18] = (byte) 33;
    sourceArray1[6] = (byte) 93;
    sourceArray1[4] = (byte) 50;
    sourceArray1[28] = (byte) 48 /*0x30*/;
    sourceArray1[3] = (byte) 86;
    sourceArray1[7] = (byte) 46;
    sourceArray1[8] = (byte) 45;
    sourceArray1[35] = (byte) 63 /*0x3F*/;
    sourceArray1[31 /*0x1F*/] = (byte) 245;
    sourceArray1[11] = (byte) 31 /*0x1F*/;
    sourceArray1[12] = (byte) 99;
    sourceArray1[40] = (byte) 196;
    sourceArray1[14] = (byte) 16 /*0x10*/;
    sourceArray1[15] = (byte) 78;
    sourceArray1[21] = (byte) 121;
    sourceArray1[38] = (byte) 251;
    sourceArray1[2] = (byte) 83;
    sourceArray1[26] = (byte) 128 /*0x80*/;
    sourceArray1[9] = (byte) 79;
    sourceArray1[20] = (byte) 252;
    sourceArray1[22] = (byte) 227;
    sourceArray1[16 /*0x10*/] = (byte) 131;
    sourceArray1[24] = (byte) 181;
    sourceArray1[25] = (byte) 3;
    sourceArray1[10] = (byte) 190;
    sourceArray1[0] = (byte) 81;
    sourceArray1[36] = (byte) 236;
    sourceArray1[29] = (byte) 207;
    sourceArray1[30] = (byte) 170;
    sourceArray1[27] = (byte) 44;
    sourceArray1[43] = (byte) 62;
    sourceArray1[33] = (byte) 64 /*0x40*/;
    sourceArray1[34] = (byte) 44;
    sourceArray1[37] = (byte) 112 /*0x70*/;
    sourceArray1[17] = (byte) 167;
    sourceArray1[41] = (byte) 249;
    sourceArray1[1] = (byte) 139;
    sourceArray1[23] = (byte) 186;
    sourceArray1[19] = (byte) 192 /*0xC0*/;
    sourceArray1[32 /*0x20*/] = (byte) 104;
    sourceArray1[42] = (byte) 251;
    sourceArray1[39] = (byte) 32 /*0x20*/;
    sourceArray1[44] = (byte) 62;
    sourceArray1[45] = (byte) 50;
    sourceArray1[46] = (byte) 51;
    sourceArray1[47] = (byte) 52;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 60,
      (byte) 30,
      (byte) 211,
      (byte) 66,
      (byte) 11,
      (byte) 204,
      (byte) 89,
      (byte) 156,
      (byte) 199,
      (byte) 81,
      (byte) 230,
      (byte) 0,
      (byte) 151,
      (byte) 96 /*0x60*/,
      (byte) 39,
      (byte) 41,
      (byte) 133,
      (byte) 117,
      (byte) 38,
      (byte) 67,
      (byte) 70,
      (byte) 32 /*0x20*/,
      (byte) 158,
      (byte) 192 /*0xC0*/,
      (byte) 103,
      (byte) 214,
      (byte) 163,
      (byte) 84,
      (byte) 139,
      (byte) 43,
      (byte) 157,
      (byte) 185,
      (byte) 171,
      (byte) 199,
      (byte) 112 /*0x70*/,
      (byte) 53,
      (byte) 248,
      (byte) 115,
      (byte) 137,
      (byte) 106,
      (byte) 55,
      (byte) 120,
      (byte) 98,
      (byte) 84,
      byte.MaxValue,
      (byte) 119,
      (byte) 246,
      (byte) 156
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12474(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 15,
      (byte) 72,
      (byte) 116,
      (byte) 90,
      (byte) 221,
      (byte) 7,
      (byte) 132,
      (byte) 113,
      (byte) 106,
      (byte) 88,
      (byte) 3,
      (byte) 28,
      (byte) 30,
      (byte) 95,
      (byte) 234,
      (byte) 73,
      (byte) 138,
      (byte) 128 /*0x80*/,
      (byte) 174,
      (byte) 88,
      (byte) 186,
      (byte) 80 /*0x50*/,
      (byte) 150,
      (byte) 58,
      (byte) 15,
      (byte) 68,
      (byte) 223,
      (byte) 23,
      (byte) 103,
      (byte) 7,
      (byte) 226,
      (byte) 10,
      (byte) 7,
      (byte) 6,
      (byte) 191,
      (byte) 4,
      (byte) 114,
      (byte) 25,
      byte.MaxValue,
      (byte) 239,
      (byte) 235,
      (byte) 137,
      (byte) 103,
      (byte) 252,
      (byte) 148,
      (byte) 54,
      (byte) 176 /*0xB0*/,
      (byte) 108
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[11] = (byte) 1;
    sourceArray2[1] = (byte) 173;
    sourceArray2[2] = (byte) 33;
    sourceArray2[28] = (byte) 151;
    sourceArray2[4] = (byte) 31 /*0x1F*/;
    sourceArray2[5] = (byte) 88;
    sourceArray2[22] = (byte) 114;
    sourceArray2[7] = (byte) 168;
    sourceArray2[3] = (byte) 137;
    sourceArray2[19] = (byte) 161;
    sourceArray2[0] = (byte) 63 /*0x3F*/;
    sourceArray2[16 /*0x10*/] = byte.MaxValue;
    sourceArray2[12] = (byte) 242;
    sourceArray2[34] = (byte) 117;
    sourceArray2[45] = (byte) 143;
    sourceArray2[15] = (byte) 46;
    sourceArray2[43] = (byte) 240 /*0xF0*/;
    sourceArray2[17] = (byte) 159;
    sourceArray2[24] = (byte) 66;
    sourceArray2[8] = (byte) 115;
    sourceArray2[20] = (byte) 238;
    sourceArray2[21] = (byte) 13;
    sourceArray2[18] = (byte) 161;
    sourceArray2[23] = (byte) 25;
    sourceArray2[32 /*0x20*/] = (byte) 61;
    sourceArray2[25] = (byte) 199;
    sourceArray2[31 /*0x1F*/] = (byte) 44;
    sourceArray2[6] = (byte) 47;
    sourceArray2[10] = (byte) 56;
    sourceArray2[13] = (byte) 59;
    sourceArray2[27] = (byte) 122;
    sourceArray2[37] = (byte) 161;
    sourceArray2[42] = (byte) 106;
    sourceArray2[33] = (byte) 0;
    sourceArray2[14] = (byte) 231;
    sourceArray2[35] = (byte) 58;
    sourceArray2[36] = (byte) 32 /*0x20*/;
    sourceArray2[30] = (byte) 230;
    sourceArray2[29] = (byte) 243;
    sourceArray2[39] = (byte) 77;
    sourceArray2[41] = (byte) 89;
    sourceArray2[44] = (byte) 50;
    sourceArray2[38] = (byte) 223;
    sourceArray2[9] = (byte) 156;
    sourceArray2[26] = (byte) 155;
    sourceArray2[40] = (byte) 172;
    sourceArray2[46] = (byte) 150;
    sourceArray2[47] = (byte) 118;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12475(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 152,
      (byte) 205,
      (byte) 23,
      (byte) 114,
      (byte) 0,
      (byte) 154,
      (byte) 44,
      (byte) 175,
      (byte) 145,
      (byte) 213,
      (byte) 23,
      (byte) 148,
      (byte) 113,
      (byte) 4,
      (byte) 182,
      (byte) 83,
      (byte) 235,
      (byte) 204,
      (byte) 175,
      (byte) 216,
      (byte) 42,
      (byte) 211,
      (byte) 158,
      (byte) 148,
      (byte) 159,
      (byte) 161,
      (byte) 26,
      (byte) 248,
      (byte) 237,
      (byte) 176 /*0xB0*/,
      (byte) 227,
      (byte) 19,
      (byte) 9,
      (byte) 183,
      (byte) 252,
      (byte) 165,
      (byte) 1,
      (byte) 171,
      (byte) 149,
      (byte) 44,
      (byte) 243,
      (byte) 58,
      (byte) 179,
      (byte) 43,
      (byte) 58,
      (byte) 163,
      (byte) 93,
      (byte) 98
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 128 /*0x80*/,
      (byte) 138,
      (byte) 112 /*0x70*/,
      (byte) 206,
      (byte) 20,
      (byte) 81,
      (byte) 122,
      (byte) 61,
      (byte) 37,
      (byte) 229,
      (byte) 242,
      (byte) 224 /*0xE0*/,
      (byte) 241,
      (byte) 97,
      (byte) 230,
      (byte) 60,
      (byte) 164,
      (byte) 92,
      (byte) 243,
      (byte) 146,
      (byte) 204,
      (byte) 187,
      (byte) 244,
      (byte) 68,
      (byte) 126,
      (byte) 190,
      (byte) 250,
      (byte) 207,
      (byte) 29,
      (byte) 34,
      (byte) 86,
      (byte) 62,
      (byte) 34,
      (byte) 155,
      (byte) 197,
      (byte) 16 /*0x10*/,
      (byte) 137,
      (byte) 237,
      (byte) 184,
      (byte) 107,
      (byte) 34,
      (byte) 106,
      (byte) 163,
      (byte) 182,
      (byte) 240 /*0xF0*/,
      (byte) 234,
      (byte) 131,
      (byte) 42
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_12476()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[97];
      byte[] numArray2 = new byte[55]
      {
        (byte) 214,
        (byte) 195,
        (byte) 246,
        (byte) 196,
        (byte) 46,
        (byte) 131,
        (byte) 211,
        (byte) 95,
        (byte) 128 /*0x80*/,
        (byte) 52,
        (byte) 194,
        (byte) 224 /*0xE0*/,
        (byte) 241,
        (byte) 4,
        (byte) 138,
        (byte) 186,
        (byte) 8,
        (byte) 16 /*0x10*/,
        (byte) 81,
        (byte) 63 /*0x3F*/,
        (byte) 54,
        (byte) 131,
        (byte) 229,
        (byte) 240 /*0xF0*/,
        (byte) 32 /*0x20*/,
        (byte) 115,
        (byte) 90,
        (byte) 251,
        (byte) 68,
        (byte) 183,
        (byte) 231,
        (byte) 193,
        (byte) 27,
        (byte) 170,
        (byte) 60,
        (byte) 70,
        (byte) 193,
        (byte) 56,
        (byte) 118,
        (byte) 126,
        (byte) 216,
        (byte) 54,
        (byte) 172,
        (byte) 191,
        (byte) 120,
        (byte) 210,
        (byte) 57,
        (byte) 53,
        (byte) 24,
        (byte) 163,
        (byte) 90,
        byte.MaxValue,
        (byte) 218,
        (byte) 84,
        (byte) 2
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 173,
        (byte) 126,
        (byte) 62,
        (byte) 214,
        (byte) 179,
        (byte) 253,
        (byte) 63 /*0x3F*/,
        (byte) 217,
        (byte) 135,
        (byte) 187,
        (byte) 145,
        (byte) 136,
        (byte) 108,
        (byte) 248,
        (byte) 142,
        (byte) 116,
        (byte) 35,
        (byte) 77,
        (byte) 49,
        (byte) 217,
        (byte) 158,
        (byte) 156,
        (byte) 111,
        (byte) 240 /*0xF0*/,
        (byte) 217,
        (byte) 2,
        (byte) 253,
        (byte) 211,
        (byte) 57,
        (byte) 201,
        (byte) 203,
        (byte) 217,
        (byte) 211,
        (byte) 208 /*0xD0*/,
        (byte) 61,
        (byte) 160 /*0xA0*/,
        (byte) 8,
        (byte) 147,
        (byte) 118,
        (byte) 56,
        (byte) 32 /*0x20*/,
        (byte) 171,
        (byte) 117,
        (byte) 5,
        (byte) 128 /*0x80*/,
        (byte) 218,
        (byte) 15,
        (byte) 65,
        (byte) 13,
        (byte) 32 /*0x20*/,
        (byte) 15,
        (byte) 75,
        (byte) 96 /*0x60*/,
        (byte) 60,
        (byte) 179
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[42]
      {
        (byte) 6,
        (byte) 163,
        (byte) 187,
        (byte) 41,
        (byte) 215,
        (byte) 26,
        (byte) 200,
        (byte) 76,
        (byte) 1,
        (byte) 196,
        (byte) 71,
        (byte) 199,
        (byte) 68,
        (byte) 248,
        (byte) 208 /*0xD0*/,
        (byte) 153,
        (byte) 211,
        (byte) 103,
        byte.MaxValue,
        (byte) 215,
        (byte) 38,
        (byte) 119,
        (byte) 77,
        (byte) 58,
        (byte) 36,
        (byte) 226,
        (byte) 161,
        (byte) 93,
        (byte) 231,
        (byte) 230,
        (byte) 198,
        (byte) 168,
        (byte) 233,
        (byte) 103,
        (byte) 187,
        (byte) 148,
        (byte) 187,
        (byte) 84,
        (byte) 1,
        (byte) 27,
        (byte) 227,
        (byte) 114
      };
      byte[] numArray5 = new byte[42]
      {
        (byte) 5,
        (byte) 179,
        (byte) 209,
        (byte) 33,
        (byte) 185,
        (byte) 216,
        (byte) 145,
        (byte) 190,
        (byte) 168,
        (byte) 137,
        (byte) 243,
        (byte) 67,
        (byte) 59,
        (byte) 18,
        (byte) 36,
        (byte) 7,
        (byte) 115,
        (byte) 182,
        (byte) 173,
        (byte) 189,
        (byte) 204,
        (byte) 101,
        (byte) 30,
        (byte) 19,
        (byte) 144 /*0x90*/,
        (byte) 249,
        (byte) 109,
        (byte) 1,
        (byte) 165,
        (byte) 194,
        (byte) 250,
        (byte) 136,
        (byte) 196,
        (byte) 176 /*0xB0*/,
        (byte) 135,
        (byte) 11,
        (byte) 239,
        (byte) 63 /*0x3F*/,
        (byte) 154,
        byte.MaxValue,
        (byte) 81,
        (byte) 18
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 42);
      for (int index = 0; index < 42; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[97];
    byte[] numArray7 = new byte[55];
    numArray7[48 /*0x30*/] = (byte) 12;
    numArray7[23] = (byte) 60;
    numArray7[2] = (byte) 170;
    numArray7[51] = (byte) 13;
    numArray7[10] = (byte) 142;
    numArray7[5] = (byte) 7;
    numArray7[36] = (byte) 246;
    numArray7[7] = (byte) 2;
    numArray7[8] = (byte) 124;
    numArray7[9] = (byte) 184;
    numArray7[17] = (byte) 100;
    numArray7[13] = (byte) 5;
    numArray7[12] = (byte) 129;
    numArray7[19] = (byte) 253;
    numArray7[14] = (byte) 244;
    numArray7[15] = (byte) 121;
    numArray7[16 /*0x10*/] = (byte) 106;
    numArray7[21] = (byte) 221;
    numArray7[44] = (byte) 45;
    numArray7[3] = (byte) 138;
    numArray7[20] = (byte) 158;
    numArray7[28] = (byte) 227;
    numArray7[43] = (byte) 175;
    numArray7[47] = (byte) 79;
    numArray7[24] = (byte) 59;
    numArray7[0] = (byte) 110;
    numArray7[18] = (byte) 26;
    numArray7[27] = (byte) 101;
    numArray7[35] = (byte) 14;
    numArray7[4] = (byte) 101;
    numArray7[30] = (byte) 179;
    numArray7[26] = (byte) 126;
    numArray7[42] = (byte) 109;
    numArray7[50] = (byte) 121;
    numArray7[25] = (byte) 222;
    numArray7[32 /*0x20*/] = (byte) 63 /*0x3F*/;
    numArray7[46] = (byte) 58;
    numArray7[6] = (byte) 237;
    numArray7[38] = (byte) 130;
    numArray7[39] = (byte) 222;
    numArray7[40] = (byte) 216;
    numArray7[41] = (byte) 165;
    numArray7[22] = (byte) 220;
    numArray7[11] = (byte) 85;
    numArray7[29] = (byte) 243;
    numArray7[45] = (byte) 62;
    numArray7[34] = (byte) 181;
    numArray7[37] = (byte) 210;
    numArray7[31 /*0x1F*/] = (byte) 0;
    numArray7[49] = (byte) 162;
    numArray7[33] = (byte) 71;
    numArray7[1] = (byte) 212;
    numArray7[52] = (byte) 92;
    numArray7[53] = (byte) 16 /*0x10*/;
    numArray7[54] = (byte) 83;
    byte[] numArray8 = new byte[55]
    {
      (byte) 150,
      (byte) 113,
      (byte) 94,
      (byte) 233,
      (byte) 12,
      (byte) 251,
      (byte) 121,
      (byte) 77,
      (byte) 5,
      (byte) 214,
      (byte) 137,
      (byte) 77,
      (byte) 208 /*0xD0*/,
      (byte) 245,
      (byte) 110,
      (byte) 16 /*0x10*/,
      (byte) 71,
      (byte) 207,
      (byte) 131,
      (byte) 108,
      (byte) 110,
      (byte) 1,
      (byte) 4,
      (byte) 114,
      (byte) 135,
      (byte) 220,
      (byte) 9,
      (byte) 83,
      (byte) 219,
      (byte) 160 /*0xA0*/,
      (byte) 48 /*0x30*/,
      (byte) 113,
      (byte) 162,
      (byte) 171,
      (byte) 165,
      (byte) 72,
      (byte) 1,
      (byte) 14,
      (byte) 3,
      (byte) 95,
      (byte) 138,
      (byte) 83,
      (byte) 41,
      (byte) 10,
      (byte) 224 /*0xE0*/,
      (byte) 145,
      (byte) 122,
      (byte) 188,
      (byte) 179,
      (byte) 139,
      (byte) 25,
      (byte) 29,
      (byte) 241,
      (byte) 188,
      (byte) 207
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[42];
    numArray9[24] = (byte) 46;
    numArray9[26] = (byte) 33;
    numArray9[2] = (byte) 228;
    numArray9[3] = (byte) 91;
    numArray9[20] = (byte) 130;
    numArray9[5] = (byte) 221;
    numArray9[4] = (byte) 46;
    numArray9[6] = (byte) 14;
    numArray9[8] = (byte) 233;
    numArray9[13] = (byte) 249;
    numArray9[10] = (byte) 68;
    numArray9[39] = (byte) 168;
    numArray9[12] = (byte) 207;
    numArray9[28] = (byte) 123;
    numArray9[14] = (byte) 19;
    numArray9[11] = (byte) 43;
    numArray9[16 /*0x10*/] = (byte) 230;
    numArray9[15] = (byte) 232;
    numArray9[18] = (byte) 115;
    numArray9[1] = (byte) 82;
    numArray9[23] = (byte) 161;
    numArray9[9] = (byte) 231;
    numArray9[22] = (byte) 212;
    numArray9[37] = (byte) 250;
    numArray9[17] = (byte) 211;
    numArray9[41] = (byte) 76;
    numArray9[21] = (byte) 111;
    numArray9[34] = (byte) 43;
    numArray9[29] = (byte) 131;
    numArray9[27] = (byte) 152;
    numArray9[38] = (byte) 34;
    numArray9[31 /*0x1F*/] = (byte) 155;
    numArray9[19] = (byte) 139;
    numArray9[33] = (byte) 76;
    numArray9[7] = (byte) 94;
    numArray9[35] = (byte) 49;
    numArray9[36] = (byte) 22;
    numArray9[30] = (byte) 66;
    numArray9[32 /*0x20*/] = (byte) 4;
    numArray9[25] = (byte) 51;
    numArray9[40] = (byte) 213;
    numArray9[0] = (byte) 94;
    byte[] numArray10 = new byte[42]
    {
      (byte) 235,
      (byte) 233,
      (byte) 41,
      (byte) 79,
      (byte) 141,
      (byte) 224 /*0xE0*/,
      (byte) 197,
      (byte) 47,
      (byte) 186,
      (byte) 30,
      (byte) 227,
      (byte) 174,
      (byte) 245,
      (byte) 164,
      (byte) 233,
      (byte) 208 /*0xD0*/,
      (byte) 168,
      (byte) 248,
      (byte) 104,
      (byte) 37,
      (byte) 180,
      (byte) 33,
      (byte) 243,
      (byte) 23,
      (byte) 171,
      (byte) 11,
      (byte) 110,
      (byte) 110,
      (byte) 214,
      (byte) 248,
      (byte) 66,
      (byte) 195,
      (byte) 241,
      (byte) 196,
      (byte) 156,
      (byte) 73,
      (byte) 12,
      (byte) 41,
      (byte) 182,
      (byte) 43,
      (byte) 65,
      (byte) 68
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 42);
    for (int index = 0; index < 42; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static int ssp_appserver_12477(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[23] = (byte) 94;
    sourceArray1[47] = (byte) 162;
    sourceArray1[43] = (byte) 248;
    sourceArray1[21] = (byte) 53;
    sourceArray1[4] = (byte) 40;
    sourceArray1[5] = (byte) 236;
    sourceArray1[29] = (byte) 28;
    sourceArray1[7] = (byte) 106;
    sourceArray1[28] = (byte) 170;
    sourceArray1[44] = (byte) 207;
    sourceArray1[3] = (byte) 205;
    sourceArray1[39] = (byte) 221;
    sourceArray1[12] = (byte) 211;
    sourceArray1[13] = (byte) 88;
    sourceArray1[14] = (byte) 235;
    sourceArray1[0] = (byte) 60;
    sourceArray1[16 /*0x10*/] = (byte) 77;
    sourceArray1[17] = (byte) 103;
    sourceArray1[24] = (byte) 209;
    sourceArray1[27] = (byte) 171;
    sourceArray1[20] = (byte) 18;
    sourceArray1[45] = (byte) 173;
    sourceArray1[31 /*0x1F*/] = (byte) 244;
    sourceArray1[25] = (byte) 28;
    sourceArray1[15] = (byte) 112 /*0x70*/;
    sourceArray1[42] = (byte) 103;
    sourceArray1[26] = (byte) 192 /*0xC0*/;
    sourceArray1[11] = (byte) 173;
    sourceArray1[34] = (byte) 145;
    sourceArray1[22] = (byte) 233;
    sourceArray1[30] = (byte) 123;
    sourceArray1[36] = (byte) 254;
    sourceArray1[10] = (byte) 20;
    sourceArray1[33] = (byte) 125;
    sourceArray1[19] = (byte) 222;
    sourceArray1[35] = (byte) 45;
    sourceArray1[9] = (byte) 155;
    sourceArray1[37] = (byte) 30;
    sourceArray1[38] = (byte) 136;
    sourceArray1[2] = (byte) 211;
    sourceArray1[40] = (byte) 87;
    sourceArray1[41] = (byte) 203;
    sourceArray1[8] = (byte) 81;
    sourceArray1[18] = (byte) 28;
    sourceArray1[6] = (byte) 95;
    sourceArray1[1] = (byte) 180;
    sourceArray1[46] = (byte) 10;
    sourceArray1[32 /*0x20*/] = (byte) 183;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 211,
      (byte) 30,
      (byte) 165,
      (byte) 159,
      (byte) 65,
      (byte) 45,
      (byte) 88,
      (byte) 223,
      (byte) 32 /*0x20*/,
      (byte) 229,
      (byte) 175,
      (byte) 27,
      (byte) 47,
      (byte) 248,
      (byte) 76,
      (byte) 20,
      (byte) 124,
      (byte) 166,
      (byte) 111,
      (byte) 211,
      (byte) 205,
      (byte) 209,
      (byte) 145,
      (byte) 154,
      (byte) 229,
      (byte) 197,
      (byte) 148,
      (byte) 141,
      (byte) 195,
      (byte) 217,
      (byte) 105,
      (byte) 219,
      (byte) 177,
      (byte) 24,
      (byte) 197,
      (byte) 229,
      (byte) 97,
      (byte) 28,
      (byte) 7,
      (byte) 242,
      (byte) 190,
      (byte) 200,
      (byte) 199,
      (byte) 27,
      (byte) 42,
      (byte) 9,
      (byte) 83,
      (byte) 119
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12478(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[27] = (byte) 141;
    sourceArray1[36] = (byte) 172;
    sourceArray1[16 /*0x10*/] = (byte) 221;
    sourceArray1[26] = (byte) 97;
    sourceArray1[4] = (byte) 205;
    sourceArray1[5] = (byte) 227;
    sourceArray1[10] = (byte) 216;
    sourceArray1[7] = (byte) 10;
    sourceArray1[8] = (byte) 171;
    sourceArray1[3] = (byte) 85;
    sourceArray1[11] = (byte) 229;
    sourceArray1[9] = (byte) 56;
    sourceArray1[12] = (byte) 215;
    sourceArray1[13] = (byte) 248;
    sourceArray1[14] = (byte) 13;
    sourceArray1[15] = (byte) 231;
    sourceArray1[38] = (byte) 82;
    sourceArray1[43] = (byte) 82;
    sourceArray1[18] = (byte) 104;
    sourceArray1[23] = (byte) 91;
    sourceArray1[20] = (byte) 207;
    sourceArray1[33] = (byte) 108;
    sourceArray1[22] = (byte) 59;
    sourceArray1[41] = (byte) 27;
    sourceArray1[24] = (byte) 232;
    sourceArray1[19] = (byte) 195;
    sourceArray1[29] = (byte) 59;
    sourceArray1[2] = (byte) 32 /*0x20*/;
    sourceArray1[28] = (byte) 227;
    sourceArray1[6] = (byte) 89;
    sourceArray1[1] = (byte) 220;
    sourceArray1[31 /*0x1F*/] = (byte) 26;
    sourceArray1[32 /*0x20*/] = (byte) 223;
    sourceArray1[46] = (byte) 75;
    sourceArray1[34] = (byte) 93;
    sourceArray1[17] = (byte) 21;
    sourceArray1[44] = (byte) 138;
    sourceArray1[37] = (byte) 140;
    sourceArray1[21] = (byte) 201;
    sourceArray1[35] = (byte) 65;
    sourceArray1[40] = (byte) 3;
    sourceArray1[0] = (byte) 180;
    sourceArray1[42] = (byte) 224 /*0xE0*/;
    sourceArray1[25] = (byte) 11;
    sourceArray1[39] = (byte) 212;
    sourceArray1[45] = (byte) 163;
    sourceArray1[30] = (byte) 155;
    sourceArray1[47] = (byte) 132;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 163,
      (byte) 40,
      (byte) 177,
      (byte) 183,
      (byte) 82,
      (byte) 47,
      (byte) 19,
      (byte) 166,
      (byte) 193,
      (byte) 193,
      (byte) 243,
      (byte) 65,
      (byte) 120,
      (byte) 182,
      (byte) 87,
      (byte) 190,
      (byte) 175,
      (byte) 112 /*0x70*/,
      (byte) 189,
      (byte) 81,
      (byte) 210,
      (byte) 16 /*0x10*/,
      (byte) 248,
      (byte) 229,
      (byte) 136,
      (byte) 152,
      (byte) 248,
      (byte) 41,
      (byte) 171,
      (byte) 240 /*0xF0*/,
      (byte) 214,
      (byte) 191,
      (byte) 0,
      (byte) 217,
      (byte) 142,
      (byte) 189,
      (byte) 160 /*0xA0*/,
      (byte) 11,
      (byte) 7,
      (byte) 195,
      (byte) 237,
      (byte) 79,
      (byte) 130,
      (byte) 242,
      (byte) 46,
      (byte) 47,
      (byte) 250,
      (byte) 118
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12479(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 224 /*0xE0*/,
      (byte) 233,
      (byte) 215,
      (byte) 128 /*0x80*/,
      (byte) 220,
      (byte) 90,
      (byte) 220,
      (byte) 44,
      (byte) 51,
      (byte) 1,
      (byte) 184,
      (byte) 243,
      (byte) 39,
      (byte) 2,
      (byte) 165,
      (byte) 226,
      (byte) 254,
      (byte) 97,
      (byte) 30,
      (byte) 20,
      (byte) 20,
      (byte) 229,
      (byte) 39,
      (byte) 43,
      (byte) 83,
      (byte) 48 /*0x30*/,
      (byte) 50,
      (byte) 64 /*0x40*/,
      (byte) 137,
      (byte) 191,
      (byte) 121,
      (byte) 51,
      (byte) 6,
      (byte) 168,
      (byte) 223,
      (byte) 243,
      (byte) 17,
      (byte) 167,
      (byte) 49,
      (byte) 35,
      (byte) 206,
      (byte) 198,
      (byte) 81,
      (byte) 114,
      (byte) 232,
      (byte) 244,
      (byte) 249,
      (byte) 142
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[27] = (byte) 204;
    sourceArray2[1] = (byte) 115;
    sourceArray2[26] = (byte) 70;
    sourceArray2[3] = (byte) 80 /*0x50*/;
    sourceArray2[14] = (byte) 197;
    sourceArray2[22] = (byte) 159;
    sourceArray2[6] = (byte) 43;
    sourceArray2[47] = (byte) 15;
    sourceArray2[8] = (byte) 101;
    sourceArray2[9] = (byte) 43;
    sourceArray2[10] = (byte) 146;
    sourceArray2[34] = (byte) 129;
    sourceArray2[38] = (byte) 160 /*0xA0*/;
    sourceArray2[4] = (byte) 210;
    sourceArray2[24] = (byte) 140;
    sourceArray2[15] = (byte) 32 /*0x20*/;
    sourceArray2[45] = (byte) 54;
    sourceArray2[17] = (byte) 198;
    sourceArray2[13] = (byte) 237;
    sourceArray2[19] = (byte) 230;
    sourceArray2[5] = (byte) 148;
    sourceArray2[21] = (byte) 184;
    sourceArray2[43] = (byte) 83;
    sourceArray2[35] = (byte) 123;
    sourceArray2[39] = (byte) 194;
    sourceArray2[32 /*0x20*/] = (byte) 106;
    sourceArray2[11] = (byte) 248;
    sourceArray2[20] = (byte) 177;
    sourceArray2[28] = (byte) 56;
    sourceArray2[0] = (byte) 203;
    sourceArray2[30] = (byte) 23;
    sourceArray2[31 /*0x1F*/] = (byte) 237;
    sourceArray2[25] = (byte) 240 /*0xF0*/;
    sourceArray2[33] = (byte) 140;
    sourceArray2[29] = (byte) 182;
    sourceArray2[36] = (byte) 61;
    sourceArray2[37] = (byte) 220;
    sourceArray2[44] = (byte) 87;
    sourceArray2[18] = (byte) 25;
    sourceArray2[12] = (byte) 98;
    sourceArray2[40] = (byte) 82;
    sourceArray2[41] = (byte) 146;
    sourceArray2[42] = (byte) 114;
    sourceArray2[7] = (byte) 64 /*0x40*/;
    sourceArray2[23] = (byte) 46;
    sourceArray2[2] = (byte) 124;
    sourceArray2[46] = (byte) 120;
    sourceArray2[16 /*0x10*/] = (byte) 245;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_12480()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[98];
      byte[] numArray2 = new byte[55];
      numArray2[40] = (byte) 141;
      numArray2[23] = (byte) 63 /*0x3F*/;
      numArray2[11] = (byte) 11;
      numArray2[41] = byte.MaxValue;
      numArray2[6] = (byte) 221;
      numArray2[1] = (byte) 122;
      numArray2[3] = (byte) 251;
      numArray2[36] = (byte) 57;
      numArray2[4] = (byte) 132;
      numArray2[52] = (byte) 51;
      numArray2[10] = (byte) 103;
      numArray2[20] = (byte) 233;
      numArray2[12] = (byte) 106;
      numArray2[13] = (byte) 204;
      numArray2[25] = (byte) 107;
      numArray2[15] = (byte) 130;
      numArray2[33] = (byte) 222;
      numArray2[5] = (byte) 158;
      numArray2[18] = (byte) 169;
      numArray2[17] = (byte) 110;
      numArray2[31 /*0x1F*/] = (byte) 182;
      numArray2[21] = (byte) 158;
      numArray2[22] = (byte) 94;
      numArray2[26] = (byte) 220;
      numArray2[45] = (byte) 37;
      numArray2[8] = (byte) 112 /*0x70*/;
      numArray2[9] = (byte) 58;
      numArray2[27] = (byte) 171;
      numArray2[28] = (byte) 254;
      numArray2[29] = (byte) 20;
      numArray2[2] = (byte) 107;
      numArray2[7] = (byte) 156;
      numArray2[32 /*0x20*/] = (byte) 203;
      numArray2[14] = (byte) 116;
      numArray2[34] = (byte) 88;
      numArray2[35] = (byte) 188;
      numArray2[30] = (byte) 42;
      numArray2[43] = (byte) 229;
      numArray2[38] = (byte) 176 /*0xB0*/;
      numArray2[50] = (byte) 41;
      numArray2[24] = (byte) 128 /*0x80*/;
      numArray2[16 /*0x10*/] = (byte) 8;
      numArray2[39] = (byte) 43;
      numArray2[42] = (byte) 222;
      numArray2[44] = (byte) 124;
      numArray2[37] = (byte) 151;
      numArray2[46] = (byte) 251;
      numArray2[49] = (byte) 121;
      numArray2[48 /*0x30*/] = (byte) 16 /*0x10*/;
      numArray2[0] = (byte) 7;
      numArray2[19] = (byte) 28;
      numArray2[51] = (byte) 180;
      numArray2[47] = (byte) 174;
      numArray2[53] = (byte) 205;
      numArray2[54] = (byte) 122;
      byte[] numArray3 = new byte[55];
      numArray3[30] = (byte) 16 /*0x10*/;
      numArray3[29] = (byte) 37;
      numArray3[18] = (byte) 252;
      numArray3[35] = (byte) 190;
      numArray3[12] = (byte) 177;
      numArray3[13] = (byte) 212;
      numArray3[6] = (byte) 170;
      numArray3[39] = (byte) 217;
      numArray3[37] = (byte) 158;
      numArray3[9] = (byte) 250;
      numArray3[10] = (byte) 125;
      numArray3[28] = (byte) 211;
      numArray3[5] = (byte) 227;
      numArray3[49] = (byte) 161;
      numArray3[14] = (byte) 46;
      numArray3[8] = (byte) 18;
      numArray3[50] = (byte) 219;
      numArray3[17] = (byte) 10;
      numArray3[0] = (byte) 4;
      numArray3[19] = (byte) 212;
      numArray3[20] = (byte) 104;
      numArray3[21] = (byte) 199;
      numArray3[16 /*0x10*/] = (byte) 75;
      numArray3[23] = (byte) 189;
      numArray3[1] = (byte) 95;
      numArray3[25] = (byte) 51;
      numArray3[26] = (byte) 9;
      numArray3[27] = (byte) 12;
      numArray3[44] = (byte) 203;
      numArray3[34] = (byte) 227;
      numArray3[51] = (byte) 178;
      numArray3[15] = (byte) 78;
      numArray3[7] = (byte) 214;
      numArray3[33] = (byte) 130;
      numArray3[4] = (byte) 27;
      numArray3[24] = (byte) 190;
      numArray3[36] = (byte) 141;
      numArray3[43] = (byte) 52;
      numArray3[38] = (byte) 209;
      numArray3[46] = (byte) 22;
      numArray3[45] = (byte) 58;
      numArray3[41] = (byte) 6;
      numArray3[42] = (byte) 76;
      numArray3[2] = (byte) 237;
      numArray3[22] = (byte) 210;
      numArray3[32 /*0x20*/] = (byte) 151;
      numArray3[3] = (byte) 3;
      numArray3[47] = (byte) 198;
      numArray3[48 /*0x30*/] = (byte) 141;
      numArray3[11] = (byte) 27;
      numArray3[40] = (byte) 30;
      numArray3[31 /*0x1F*/] = (byte) 8;
      numArray3[52] = (byte) 32 /*0x20*/;
      numArray3[53] = (byte) 201;
      numArray3[54] = (byte) 40;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[43];
      numArray4[11] = (byte) 60;
      numArray4[19] = (byte) 47;
      numArray4[9] = (byte) 129;
      numArray4[3] = (byte) 160 /*0xA0*/;
      numArray4[4] = (byte) 84;
      numArray4[38] = (byte) 111;
      numArray4[32 /*0x20*/] = (byte) 221;
      numArray4[7] = (byte) 27;
      numArray4[28] = (byte) 174;
      numArray4[41] = (byte) 32 /*0x20*/;
      numArray4[6] = (byte) 65;
      numArray4[5] = (byte) 198;
      numArray4[0] = (byte) 114;
      numArray4[35] = (byte) 81;
      numArray4[14] = (byte) 87;
      numArray4[15] = (byte) 57;
      numArray4[42] = (byte) 62;
      numArray4[17] = (byte) 111;
      numArray4[18] = (byte) 67;
      numArray4[21] = (byte) 88;
      numArray4[20] = (byte) 102;
      numArray4[34] = (byte) 49;
      numArray4[24] = (byte) 61;
      numArray4[23] = (byte) 182;
      numArray4[12] = (byte) 28;
      numArray4[25] = (byte) 0;
      numArray4[22] = (byte) 210;
      numArray4[27] = (byte) 204;
      numArray4[13] = (byte) 151;
      numArray4[29] = (byte) 44;
      numArray4[30] = (byte) 183;
      numArray4[31 /*0x1F*/] = (byte) 205;
      numArray4[26] = (byte) 95;
      numArray4[33] = (byte) 248;
      numArray4[1] = (byte) 170;
      numArray4[8] = (byte) 96 /*0x60*/;
      numArray4[40] = (byte) 205;
      numArray4[10] = (byte) 82;
      numArray4[2] = (byte) 169;
      numArray4[39] = (byte) 127 /*0x7F*/;
      numArray4[36] = (byte) 68;
      numArray4[16 /*0x10*/] = (byte) 63 /*0x3F*/;
      numArray4[37] = (byte) 53;
      byte[] numArray5 = new byte[43];
      numArray5[8] = (byte) 143;
      numArray5[7] = (byte) 90;
      numArray5[20] = (byte) 225;
      numArray5[2] = (byte) 210;
      numArray5[4] = (byte) 39;
      numArray5[40] = (byte) 166;
      numArray5[6] = (byte) 170;
      numArray5[36] = (byte) 217;
      numArray5[28] = (byte) 6;
      numArray5[37] = (byte) 166;
      numArray5[10] = (byte) 78;
      numArray5[11] = (byte) 156;
      numArray5[33] = (byte) 107;
      numArray5[13] = (byte) 160 /*0xA0*/;
      numArray5[14] = (byte) 48 /*0x30*/;
      numArray5[15] = (byte) 241;
      numArray5[18] = (byte) 211;
      numArray5[24] = (byte) 217;
      numArray5[5] = (byte) 202;
      numArray5[19] = (byte) 239;
      numArray5[26] = (byte) 199;
      numArray5[21] = (byte) 179;
      numArray5[22] = (byte) 94;
      numArray5[23] = (byte) 85;
      numArray5[9] = (byte) 216;
      numArray5[25] = (byte) 253;
      numArray5[32 /*0x20*/] = (byte) 181;
      numArray5[27] = (byte) 198;
      numArray5[16 /*0x10*/] = (byte) 241;
      numArray5[29] = (byte) 124;
      numArray5[17] = (byte) 229;
      numArray5[31 /*0x1F*/] = (byte) 47;
      numArray5[12] = (byte) 112 /*0x70*/;
      numArray5[42] = (byte) 83;
      numArray5[34] = (byte) 178;
      numArray5[35] = (byte) 53;
      numArray5[38] = (byte) 238;
      numArray5[30] = (byte) 124;
      numArray5[3] = (byte) 183;
      numArray5[39] = (byte) 253;
      numArray5[0] = (byte) 7;
      numArray5[41] = (byte) 182;
      numArray5[1] = (byte) 150;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 43);
      for (int index = 0; index < 43; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[98];
    byte[] numArray7 = new byte[55]
    {
      (byte) 88,
      (byte) 79,
      (byte) 171,
      (byte) 218,
      (byte) 11,
      (byte) 183,
      (byte) 117,
      (byte) 114,
      (byte) 132,
      (byte) 11,
      (byte) 203,
      (byte) 6,
      (byte) 22,
      (byte) 82,
      (byte) 29,
      (byte) 53,
      (byte) 162,
      (byte) 181,
      (byte) 162,
      (byte) 94,
      (byte) 159,
      (byte) 171,
      (byte) 245,
      (byte) 92,
      (byte) 57,
      (byte) 168,
      (byte) 123,
      (byte) 254,
      (byte) 33,
      (byte) 180,
      (byte) 84,
      (byte) 206,
      (byte) 195,
      (byte) 240 /*0xF0*/,
      (byte) 206,
      (byte) 227,
      (byte) 241,
      (byte) 158,
      (byte) 88,
      (byte) 254,
      (byte) 223,
      (byte) 247,
      (byte) 39,
      (byte) 111,
      (byte) 241,
      (byte) 193,
      (byte) 56,
      (byte) 57,
      (byte) 134,
      (byte) 1,
      (byte) 66,
      (byte) 245,
      (byte) 103,
      (byte) 52,
      (byte) 112 /*0x70*/
    };
    byte[] numArray8 = new byte[55];
    numArray8[21] = (byte) 88;
    numArray8[31 /*0x1F*/] = (byte) 196;
    numArray8[5] = (byte) 180;
    numArray8[3] = (byte) 179;
    numArray8[4] = (byte) 243;
    numArray8[19] = (byte) 83;
    numArray8[10] = (byte) 165;
    numArray8[7] = (byte) 230;
    numArray8[0] = (byte) 101;
    numArray8[35] = (byte) 152;
    numArray8[50] = (byte) 107;
    numArray8[27] = (byte) 0;
    numArray8[28] = (byte) 162;
    numArray8[38] = (byte) 118;
    numArray8[14] = (byte) 178;
    numArray8[8] = (byte) 202;
    numArray8[17] = (byte) 64 /*0x40*/;
    numArray8[52] = (byte) 248;
    numArray8[23] = (byte) 168;
    numArray8[6] = (byte) 224 /*0xE0*/;
    numArray8[20] = (byte) 152;
    numArray8[39] = (byte) 151;
    numArray8[22] = (byte) 107;
    numArray8[30] = (byte) 235;
    numArray8[45] = (byte) 152;
    numArray8[25] = (byte) 33;
    numArray8[26] = (byte) 197;
    numArray8[46] = (byte) 122;
    numArray8[1] = (byte) 97;
    numArray8[40] = (byte) 231;
    numArray8[9] = (byte) 212;
    numArray8[2] = (byte) 90;
    numArray8[13] = (byte) 86;
    numArray8[33] = (byte) 36;
    numArray8[34] = (byte) 204;
    numArray8[32 /*0x20*/] = (byte) 252;
    numArray8[11] = (byte) 0;
    numArray8[37] = (byte) 219;
    numArray8[15] = (byte) 92;
    numArray8[36] = (byte) 230;
    numArray8[24] = (byte) 136;
    numArray8[41] = (byte) 129;
    numArray8[42] = (byte) 246;
    numArray8[18] = (byte) 100;
    numArray8[44] = (byte) 145;
    numArray8[43] = (byte) 96 /*0x60*/;
    numArray8[16 /*0x10*/] = (byte) 196;
    numArray8[47] = (byte) 193;
    numArray8[48 /*0x30*/] = (byte) 73;
    numArray8[49] = (byte) 12;
    numArray8[12] = (byte) 83;
    numArray8[51] = (byte) 199;
    numArray8[53] = (byte) 96 /*0x60*/;
    numArray8[29] = (byte) 151;
    numArray8[54] = (byte) 224 /*0xE0*/;
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[43]
    {
      (byte) 228,
      (byte) 41,
      (byte) 242,
      (byte) 250,
      byte.MaxValue,
      (byte) 49,
      (byte) 93,
      (byte) 87,
      (byte) 150,
      (byte) 179,
      (byte) 166,
      (byte) 99,
      (byte) 177,
      (byte) 161,
      (byte) 6,
      (byte) 102,
      (byte) 226,
      (byte) 22,
      (byte) 0,
      (byte) 43,
      (byte) 206,
      (byte) 117,
      (byte) 249,
      (byte) 93,
      (byte) 140,
      (byte) 44,
      (byte) 98,
      (byte) 50,
      (byte) 198,
      (byte) 164,
      (byte) 217,
      (byte) 182,
      (byte) 248,
      (byte) 181,
      (byte) 190,
      (byte) 212,
      (byte) 3,
      (byte) 199,
      (byte) 104,
      (byte) 117,
      (byte) 251,
      (byte) 96 /*0x60*/,
      (byte) 94
    };
    byte[] numArray10 = new byte[43];
    numArray10[13] = (byte) 254;
    numArray10[38] = (byte) 195;
    numArray10[2] = (byte) 200;
    numArray10[3] = (byte) 133;
    numArray10[5] = (byte) 6;
    numArray10[1] = (byte) 151;
    numArray10[26] = (byte) 203;
    numArray10[35] = (byte) 29;
    numArray10[40] = (byte) 201;
    numArray10[9] = (byte) 208 /*0xD0*/;
    numArray10[18] = (byte) 205;
    numArray10[7] = (byte) 248;
    numArray10[11] = (byte) 182;
    numArray10[8] = (byte) 74;
    numArray10[0] = (byte) 103;
    numArray10[31 /*0x1F*/] = (byte) 213;
    numArray10[20] = (byte) 81;
    numArray10[17] = (byte) 163;
    numArray10[16 /*0x10*/] = (byte) 198;
    numArray10[6] = (byte) 162;
    numArray10[12] = (byte) 161;
    numArray10[21] = byte.MaxValue;
    numArray10[22] = (byte) 95;
    numArray10[30] = (byte) 9;
    numArray10[15] = (byte) 134;
    numArray10[34] = (byte) 138;
    numArray10[24] = (byte) 60;
    numArray10[27] = (byte) 217;
    numArray10[28] = (byte) 50;
    numArray10[19] = (byte) 193;
    numArray10[14] = (byte) 1;
    numArray10[10] = (byte) 165;
    numArray10[32 /*0x20*/] = (byte) 216;
    numArray10[33] = (byte) 89;
    numArray10[25] = (byte) 241;
    numArray10[4] = (byte) 240 /*0xF0*/;
    numArray10[36] = (byte) 3;
    numArray10[37] = (byte) 232;
    numArray10[23] = (byte) 41;
    numArray10[39] = (byte) 85;
    numArray10[42] = (byte) 68;
    numArray10[41] = (byte) 3;
    numArray10[29] = (byte) 26;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 43);
    for (int index = 0; index < 43; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static int ssp_appserver_12481(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[41] = (byte) 60;
    sourceArray1[3] = (byte) 166;
    sourceArray1[1] = (byte) 89;
    sourceArray1[26] = (byte) 252;
    sourceArray1[45] = (byte) 11;
    sourceArray1[39] = (byte) 157;
    sourceArray1[6] = (byte) 108;
    sourceArray1[10] = (byte) 159;
    sourceArray1[8] = (byte) 123;
    sourceArray1[42] = (byte) 117;
    sourceArray1[0] = (byte) 110;
    sourceArray1[7] = (byte) 241;
    sourceArray1[30] = (byte) 112 /*0x70*/;
    sourceArray1[11] = (byte) 57;
    sourceArray1[14] = (byte) 197;
    sourceArray1[15] = (byte) 241;
    sourceArray1[16 /*0x10*/] = (byte) 61;
    sourceArray1[17] = (byte) 205;
    sourceArray1[18] = (byte) 152;
    sourceArray1[28] = (byte) 160 /*0xA0*/;
    sourceArray1[29] = (byte) 2;
    sourceArray1[21] = (byte) 163;
    sourceArray1[22] = (byte) 51;
    sourceArray1[5] = (byte) 208 /*0xD0*/;
    sourceArray1[47] = (byte) 197;
    sourceArray1[25] = (byte) 48 /*0x30*/;
    sourceArray1[13] = (byte) 207;
    sourceArray1[9] = (byte) 231;
    sourceArray1[35] = (byte) 194;
    sourceArray1[34] = (byte) 244;
    sourceArray1[12] = (byte) 6;
    sourceArray1[31 /*0x1F*/] = (byte) 75;
    sourceArray1[32 /*0x20*/] = (byte) 147;
    sourceArray1[33] = (byte) 142;
    sourceArray1[24] = (byte) 138;
    sourceArray1[19] = (byte) 87;
    sourceArray1[36] = (byte) 23;
    sourceArray1[37] = (byte) 229;
    sourceArray1[38] = (byte) 95;
    sourceArray1[44] = (byte) 242;
    sourceArray1[23] = (byte) 64 /*0x40*/;
    sourceArray1[2] = (byte) 150;
    sourceArray1[27] = (byte) 46;
    sourceArray1[43] = (byte) 83;
    sourceArray1[20] = (byte) 2;
    sourceArray1[4] = (byte) 208 /*0xD0*/;
    sourceArray1[46] = (byte) 243;
    sourceArray1[40] = (byte) 170;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 52,
      (byte) 233,
      (byte) 106,
      (byte) 204,
      (byte) 28,
      (byte) 254,
      (byte) 46,
      (byte) 165,
      (byte) 215,
      (byte) 53,
      (byte) 43,
      (byte) 7,
      (byte) 69,
      (byte) 135,
      (byte) 241,
      (byte) 31 /*0x1F*/,
      (byte) 146,
      (byte) 8,
      (byte) 59,
      (byte) 133,
      (byte) 241,
      (byte) 46,
      (byte) 160 /*0xA0*/,
      (byte) 244,
      (byte) 234,
      (byte) 105,
      (byte) 244,
      (byte) 103,
      (byte) 65,
      (byte) 115,
      (byte) 3,
      (byte) 228,
      (byte) 122,
      (byte) 105,
      (byte) 123,
      (byte) 153,
      (byte) 216,
      (byte) 107,
      (byte) 28,
      (byte) 10,
      (byte) 160 /*0xA0*/,
      (byte) 38,
      (byte) 191,
      (byte) 8,
      (byte) 239,
      (byte) 39,
      (byte) 90,
      (byte) 146
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12482(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 122,
      (byte) 234,
      (byte) 251,
      (byte) 109,
      (byte) 88,
      (byte) 191,
      (byte) 29,
      (byte) 75,
      (byte) 219,
      (byte) 156,
      (byte) 68,
      (byte) 7,
      (byte) 157,
      (byte) 98,
      (byte) 157,
      (byte) 15,
      (byte) 249,
      (byte) 39,
      (byte) 50,
      (byte) 33,
      (byte) 10,
      (byte) 139,
      (byte) 176 /*0xB0*/,
      (byte) 101,
      (byte) 24,
      (byte) 143,
      (byte) 173,
      (byte) 49,
      (byte) 64 /*0x40*/,
      (byte) 219,
      (byte) 49,
      (byte) 3,
      (byte) 174,
      (byte) 25,
      (byte) 195,
      (byte) 76,
      (byte) 51,
      (byte) 234,
      (byte) 249,
      (byte) 132,
      (byte) 21,
      (byte) 79,
      (byte) 17,
      (byte) 141,
      (byte) 88,
      (byte) 185,
      (byte) 43,
      (byte) 93
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[36] = (byte) 82;
    sourceArray2[1] = (byte) 179;
    sourceArray2[2] = (byte) 142;
    sourceArray2[3] = (byte) 156;
    sourceArray2[4] = (byte) 242;
    sourceArray2[37] = (byte) 183;
    sourceArray2[27] = (byte) 8;
    sourceArray2[24] = (byte) 91;
    sourceArray2[8] = (byte) 10;
    sourceArray2[42] = (byte) 69;
    sourceArray2[10] = (byte) 111;
    sourceArray2[11] = (byte) 78;
    sourceArray2[35] = (byte) 107;
    sourceArray2[23] = (byte) 47;
    sourceArray2[0] = (byte) 211;
    sourceArray2[20] = (byte) 214;
    sourceArray2[16 /*0x10*/] = (byte) 145;
    sourceArray2[17] = (byte) 37;
    sourceArray2[22] = (byte) 155;
    sourceArray2[19] = (byte) 236;
    sourceArray2[5] = (byte) 137;
    sourceArray2[32 /*0x20*/] = (byte) 100;
    sourceArray2[33] = (byte) 90;
    sourceArray2[28] = (byte) 102;
    sourceArray2[18] = (byte) 147;
    sourceArray2[26] = (byte) 249;
    sourceArray2[34] = (byte) 158;
    sourceArray2[21] = (byte) 131;
    sourceArray2[7] = (byte) 39;
    sourceArray2[12] = (byte) 23;
    sourceArray2[30] = (byte) 19;
    sourceArray2[29] = (byte) 126;
    sourceArray2[38] = (byte) 55;
    sourceArray2[40] = (byte) 239;
    sourceArray2[9] = (byte) 204;
    sourceArray2[13] = (byte) 199;
    sourceArray2[6] = (byte) 7;
    sourceArray2[15] = (byte) 234;
    sourceArray2[14] = (byte) 168;
    sourceArray2[39] = (byte) 204;
    sourceArray2[25] = (byte) 69;
    sourceArray2[41] = (byte) 219;
    sourceArray2[31 /*0x1F*/] = (byte) 252;
    sourceArray2[43] = (byte) 149;
    sourceArray2[44] = (byte) 227;
    sourceArray2[45] = (byte) 226;
    sourceArray2[46] = (byte) 142;
    sourceArray2[47] = (byte) 19;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_12483()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[98];
      byte[] numArray2 = new byte[55];
      numArray2[53] = (byte) 22;
      numArray2[40] = (byte) 183;
      numArray2[52] = (byte) 249;
      numArray2[3] = (byte) 220;
      numArray2[4] = (byte) 129;
      numArray2[31 /*0x1F*/] = (byte) 106;
      numArray2[47] = (byte) 159;
      numArray2[21] = (byte) 39;
      numArray2[15] = (byte) 193;
      numArray2[29] = (byte) 227;
      numArray2[10] = (byte) 115;
      numArray2[2] = (byte) 190;
      numArray2[48 /*0x30*/] = (byte) 201;
      numArray2[13] = (byte) 246;
      numArray2[46] = (byte) 21;
      numArray2[35] = (byte) 188;
      numArray2[16 /*0x10*/] = (byte) 231;
      numArray2[17] = (byte) 39;
      numArray2[18] = (byte) 23;
      numArray2[19] = (byte) 12;
      numArray2[20] = (byte) 231;
      numArray2[45] = (byte) 252;
      numArray2[9] = (byte) 158;
      numArray2[23] = (byte) 202;
      numArray2[22] = (byte) 197;
      numArray2[25] = (byte) 60;
      numArray2[26] = (byte) 121;
      numArray2[27] = (byte) 254;
      numArray2[28] = (byte) 58;
      numArray2[44] = (byte) 97;
      numArray2[14] = (byte) 101;
      numArray2[1] = (byte) 166;
      numArray2[32 /*0x20*/] = byte.MaxValue;
      numArray2[8] = (byte) 113;
      numArray2[34] = (byte) 254;
      numArray2[37] = (byte) 40;
      numArray2[54] = (byte) 48 /*0x30*/;
      numArray2[0] = (byte) 96 /*0x60*/;
      numArray2[38] = (byte) 97;
      numArray2[42] = (byte) 191;
      numArray2[24] = (byte) 61;
      numArray2[7] = (byte) 253;
      numArray2[6] = (byte) 165;
      numArray2[43] = (byte) 31 /*0x1F*/;
      numArray2[33] = (byte) 244;
      numArray2[36] = (byte) 145;
      numArray2[5] = (byte) 227;
      numArray2[30] = (byte) 119;
      numArray2[41] = (byte) 91;
      numArray2[50] = (byte) 64 /*0x40*/;
      numArray2[49] = (byte) 178;
      numArray2[12] = (byte) 136;
      numArray2[51] = (byte) 28;
      numArray2[39] = (byte) 6;
      numArray2[11] = (byte) 22;
      byte[] numArray3 = new byte[55];
      numArray3[18] = (byte) 148;
      numArray3[12] = (byte) 140;
      numArray3[22] = (byte) 83;
      numArray3[29] = (byte) 190;
      numArray3[52] = (byte) 136;
      numArray3[43] = (byte) 4;
      numArray3[30] = (byte) 44;
      numArray3[7] = (byte) 25;
      numArray3[8] = (byte) 50;
      numArray3[6] = (byte) 119;
      numArray3[2] = (byte) 27;
      numArray3[11] = (byte) 37;
      numArray3[4] = (byte) 132;
      numArray3[17] = (byte) 228;
      numArray3[24] = (byte) 253;
      numArray3[5] = (byte) 31 /*0x1F*/;
      numArray3[46] = (byte) 43;
      numArray3[50] = (byte) 122;
      numArray3[33] = (byte) 97;
      numArray3[19] = (byte) 29;
      numArray3[20] = (byte) 57;
      numArray3[3] = (byte) 233;
      numArray3[21] = (byte) 120;
      numArray3[1] = (byte) 115;
      numArray3[10] = (byte) 251;
      numArray3[44] = (byte) 144 /*0x90*/;
      numArray3[26] = (byte) 22;
      numArray3[27] = (byte) 41;
      numArray3[16 /*0x10*/] = (byte) 229;
      numArray3[38] = (byte) 151;
      numArray3[14] = (byte) 95;
      numArray3[31 /*0x1F*/] = (byte) 146;
      numArray3[32 /*0x20*/] = (byte) 49;
      numArray3[54] = (byte) 238;
      numArray3[34] = (byte) 206;
      numArray3[35] = (byte) 223;
      numArray3[36] = (byte) 128 /*0x80*/;
      numArray3[37] = (byte) 84;
      numArray3[28] = (byte) 168;
      numArray3[39] = (byte) 93;
      numArray3[9] = (byte) 203;
      numArray3[41] = (byte) 126;
      numArray3[42] = (byte) 47;
      numArray3[25] = (byte) 219;
      numArray3[40] = (byte) 130;
      numArray3[45] = (byte) 209;
      numArray3[23] = (byte) 201;
      numArray3[47] = (byte) 2;
      numArray3[48 /*0x30*/] = (byte) 61;
      numArray3[49] = (byte) 208 /*0xD0*/;
      numArray3[15] = (byte) 254;
      numArray3[51] = (byte) 217;
      numArray3[0] = (byte) 100;
      numArray3[13] = (byte) 143;
      numArray3[53] = (byte) 128 /*0x80*/;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[43]
      {
        (byte) 122,
        (byte) 1,
        (byte) 120,
        (byte) 94,
        (byte) 94,
        (byte) 54,
        (byte) 202,
        (byte) 200,
        (byte) 15,
        (byte) 8,
        (byte) 175,
        (byte) 62,
        (byte) 44,
        (byte) 131,
        (byte) 10,
        (byte) 170,
        (byte) 105,
        (byte) 165,
        (byte) 149,
        (byte) 239,
        (byte) 131,
        (byte) 81,
        (byte) 166,
        (byte) 242,
        (byte) 189,
        (byte) 192 /*0xC0*/,
        (byte) 22,
        (byte) 212,
        (byte) 87,
        (byte) 22,
        (byte) 169,
        (byte) 109,
        (byte) 89,
        (byte) 20,
        (byte) 149,
        (byte) 41,
        (byte) 134,
        (byte) 173,
        (byte) 64 /*0x40*/,
        (byte) 39,
        (byte) 123,
        (byte) 98,
        (byte) 175
      };
      byte[] numArray5 = new byte[43]
      {
        (byte) 41,
        (byte) 220,
        (byte) 149,
        (byte) 73,
        (byte) 183,
        (byte) 4,
        (byte) 84,
        (byte) 121,
        (byte) 164,
        (byte) 45,
        (byte) 27,
        (byte) 249,
        (byte) 106,
        (byte) 62,
        (byte) 165,
        (byte) 176 /*0xB0*/,
        (byte) 224 /*0xE0*/,
        (byte) 224 /*0xE0*/,
        (byte) 202,
        (byte) 209,
        (byte) 147,
        (byte) 56,
        (byte) 9,
        (byte) 197,
        (byte) 212,
        (byte) 16 /*0x10*/,
        (byte) 152,
        (byte) 105,
        (byte) 238,
        (byte) 166,
        (byte) 212,
        (byte) 4,
        (byte) 115,
        (byte) 210,
        (byte) 91,
        (byte) 43,
        (byte) 76,
        (byte) 150,
        (byte) 187,
        (byte) 67,
        (byte) 79,
        (byte) 86,
        (byte) 230
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 43);
      for (int index = 0; index < 43; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[98];
    byte[] numArray7 = new byte[55]
    {
      (byte) 39,
      (byte) 107,
      (byte) 65,
      (byte) 2,
      (byte) 9,
      (byte) 215,
      (byte) 28,
      (byte) 171,
      (byte) 8,
      (byte) 40,
      (byte) 90,
      (byte) 134,
      (byte) 160 /*0xA0*/,
      (byte) 157,
      (byte) 31 /*0x1F*/,
      (byte) 245,
      (byte) 113,
      (byte) 249,
      (byte) 94,
      (byte) 65,
      (byte) 229,
      (byte) 23,
      (byte) 237,
      (byte) 224 /*0xE0*/,
      (byte) 35,
      (byte) 187,
      (byte) 101,
      (byte) 110,
      (byte) 115,
      (byte) 33,
      (byte) 162,
      (byte) 249,
      (byte) 238,
      (byte) 136,
      (byte) 153,
      (byte) 171,
      (byte) 147,
      (byte) 84,
      (byte) 12,
      (byte) 209,
      (byte) 81,
      (byte) 173,
      (byte) 11,
      (byte) 108,
      (byte) 207,
      (byte) 0,
      (byte) 13,
      (byte) 162,
      (byte) 7,
      (byte) 133,
      (byte) 241,
      (byte) 182,
      (byte) 78,
      (byte) 193,
      (byte) 79
    };
    byte[] numArray8 = new byte[55]
    {
      (byte) 84,
      (byte) 215,
      (byte) 208 /*0xD0*/,
      (byte) 164,
      (byte) 155,
      (byte) 122,
      (byte) 24,
      (byte) 134,
      (byte) 67,
      (byte) 199,
      (byte) 110,
      (byte) 106,
      (byte) 77,
      (byte) 110,
      (byte) 25,
      (byte) 153,
      (byte) 0,
      (byte) 239,
      (byte) 25,
      (byte) 36,
      (byte) 195,
      (byte) 47,
      (byte) 133,
      (byte) 65,
      (byte) 57,
      (byte) 165,
      (byte) 228,
      (byte) 224 /*0xE0*/,
      (byte) 171,
      (byte) 137,
      (byte) 131,
      (byte) 147,
      (byte) 128 /*0x80*/,
      (byte) 166,
      (byte) 73,
      (byte) 152,
      (byte) 244,
      (byte) 13,
      (byte) 172,
      (byte) 114,
      (byte) 199,
      (byte) 234,
      (byte) 100,
      (byte) 159,
      (byte) 139,
      (byte) 45,
      (byte) 196,
      (byte) 253,
      (byte) 140,
      (byte) 218,
      (byte) 131,
      (byte) 28,
      (byte) 122,
      (byte) 24,
      (byte) 181
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[43]
    {
      (byte) 97,
      (byte) 223,
      (byte) 77,
      (byte) 114,
      (byte) 173,
      (byte) 55,
      (byte) 231,
      (byte) 134,
      (byte) 96 /*0x60*/,
      (byte) 151,
      (byte) 24,
      (byte) 4,
      (byte) 116,
      (byte) 98,
      (byte) 28,
      (byte) 237,
      (byte) 140,
      (byte) 240 /*0xF0*/,
      (byte) 56,
      (byte) 11,
      (byte) 129,
      (byte) 16 /*0x10*/,
      (byte) 35,
      (byte) 174,
      (byte) 194,
      (byte) 103,
      (byte) 237,
      (byte) 48 /*0x30*/,
      (byte) 235,
      (byte) 171,
      (byte) 45,
      (byte) 56,
      (byte) 136,
      (byte) 45,
      (byte) 49,
      (byte) 116,
      (byte) 190,
      (byte) 95,
      (byte) 164,
      (byte) 117,
      (byte) 36,
      (byte) 230,
      (byte) 110
    };
    byte[] numArray10 = new byte[43];
    numArray10[21] = (byte) 79;
    numArray10[1] = (byte) 15;
    numArray10[2] = (byte) 198;
    numArray10[37] = (byte) 208 /*0xD0*/;
    numArray10[0] = (byte) 153;
    numArray10[17] = (byte) 102;
    numArray10[6] = (byte) 241;
    numArray10[7] = (byte) 31 /*0x1F*/;
    numArray10[23] = (byte) 92;
    numArray10[9] = (byte) 230;
    numArray10[10] = (byte) 89;
    numArray10[11] = (byte) 237;
    numArray10[12] = (byte) 118;
    numArray10[31 /*0x1F*/] = (byte) 0;
    numArray10[14] = (byte) 98;
    numArray10[15] = (byte) 128 /*0x80*/;
    numArray10[16 /*0x10*/] = (byte) 191;
    numArray10[30] = (byte) 144 /*0x90*/;
    numArray10[5] = (byte) 18;
    numArray10[19] = (byte) 122;
    numArray10[4] = (byte) 222;
    numArray10[28] = (byte) 78;
    numArray10[22] = (byte) 23;
    numArray10[41] = (byte) 172;
    numArray10[8] = (byte) 237;
    numArray10[20] = (byte) 252;
    numArray10[26] = (byte) 45;
    numArray10[27] = (byte) 11;
    numArray10[39] = (byte) 140;
    numArray10[18] = (byte) 158;
    numArray10[24] = (byte) 18;
    numArray10[38] = (byte) 61;
    numArray10[32 /*0x20*/] = (byte) 237;
    numArray10[3] = (byte) 42;
    numArray10[34] = (byte) 154;
    numArray10[33] = (byte) 123;
    numArray10[36] = (byte) 124;
    numArray10[29] = (byte) 175;
    numArray10[40] = (byte) 54;
    numArray10[42] = (byte) 81;
    numArray10[35] = (byte) 167;
    numArray10[25] = (byte) 53;
    numArray10[13] = (byte) 172;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 43);
    for (int index = 0; index < 43; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_12484()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[102];
      byte[] numArray2 = new byte[55]
      {
        (byte) 184,
        (byte) 31 /*0x1F*/,
        (byte) 175,
        (byte) 94,
        (byte) 43,
        (byte) 108,
        (byte) 74,
        (byte) 184,
        (byte) 253,
        (byte) 20,
        (byte) 34,
        (byte) 76,
        (byte) 242,
        (byte) 44,
        (byte) 155,
        (byte) 148,
        (byte) 133,
        (byte) 84,
        (byte) 29,
        (byte) 97,
        (byte) 174,
        (byte) 1,
        (byte) 229,
        (byte) 138,
        (byte) 114,
        (byte) 127 /*0x7F*/,
        (byte) 91,
        (byte) 120,
        (byte) 204,
        (byte) 14,
        (byte) 190,
        (byte) 247,
        (byte) 20,
        (byte) 80 /*0x50*/,
        (byte) 239,
        (byte) 14,
        (byte) 125,
        (byte) 237,
        (byte) 151,
        (byte) 122,
        (byte) 80 /*0x50*/,
        (byte) 233,
        (byte) 188,
        (byte) 164,
        (byte) 2,
        (byte) 134,
        (byte) 216,
        (byte) 46,
        (byte) 135,
        (byte) 85,
        (byte) 214,
        (byte) 204,
        (byte) 102,
        (byte) 208 /*0xD0*/,
        (byte) 50
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 123,
        (byte) 200,
        (byte) 205,
        (byte) 43,
        (byte) 81,
        (byte) 0,
        (byte) 70,
        (byte) 233,
        (byte) 8,
        (byte) 45,
        (byte) 111,
        (byte) 173,
        (byte) 215,
        (byte) 192 /*0xC0*/,
        (byte) 235,
        (byte) 238,
        (byte) 75,
        (byte) 112 /*0x70*/,
        (byte) 241,
        (byte) 99,
        (byte) 18,
        (byte) 130,
        (byte) 169,
        (byte) 132,
        (byte) 181,
        (byte) 168,
        (byte) 199,
        (byte) 65,
        (byte) 119,
        (byte) 80 /*0x50*/,
        (byte) 122,
        (byte) 119,
        (byte) 240 /*0xF0*/,
        (byte) 116,
        (byte) 151,
        (byte) 55,
        (byte) 53,
        (byte) 162,
        (byte) 16 /*0x10*/,
        (byte) 243,
        (byte) 248,
        (byte) 208 /*0xD0*/,
        (byte) 150,
        (byte) 26,
        (byte) 121,
        (byte) 55,
        (byte) 183,
        (byte) 254,
        (byte) 215,
        (byte) 94,
        (byte) 238,
        (byte) 110,
        (byte) 5,
        (byte) 38,
        (byte) 206
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[47]
      {
        (byte) 37,
        (byte) 235,
        (byte) 172,
        (byte) 16 /*0x10*/,
        (byte) 209,
        (byte) 236,
        (byte) 239,
        (byte) 86,
        (byte) 42,
        (byte) 56,
        (byte) 162,
        (byte) 61,
        (byte) 140,
        (byte) 22,
        (byte) 152,
        (byte) 189,
        (byte) 163,
        (byte) 16 /*0x10*/,
        (byte) 196,
        (byte) 69,
        (byte) 64 /*0x40*/,
        (byte) 135,
        (byte) 235,
        (byte) 166,
        (byte) 45,
        (byte) 241,
        (byte) 118,
        (byte) 56,
        (byte) 242,
        (byte) 145,
        (byte) 67,
        (byte) 29,
        (byte) 56,
        (byte) 212,
        (byte) 88,
        (byte) 1,
        (byte) 234,
        (byte) 105,
        (byte) 126,
        (byte) 253,
        (byte) 205,
        (byte) 78,
        (byte) 216,
        (byte) 78,
        (byte) 54,
        (byte) 241,
        (byte) 250
      };
      byte[] numArray5 = new byte[47];
      numArray5[36] = (byte) 216;
      numArray5[1] = (byte) 104;
      numArray5[2] = (byte) 114;
      numArray5[40] = (byte) 117;
      numArray5[25] = (byte) 134;
      numArray5[0] = (byte) 38;
      numArray5[32 /*0x20*/] = (byte) 121;
      numArray5[7] = (byte) 141;
      numArray5[5] = (byte) 103;
      numArray5[46] = (byte) 107;
      numArray5[10] = (byte) 160 /*0xA0*/;
      numArray5[11] = (byte) 171;
      numArray5[23] = (byte) 240 /*0xF0*/;
      numArray5[38] = (byte) 55;
      numArray5[18] = (byte) 149;
      numArray5[15] = (byte) 221;
      numArray5[3] = (byte) 103;
      numArray5[17] = (byte) 197;
      numArray5[35] = (byte) 80 /*0x50*/;
      numArray5[19] = (byte) 48 /*0x30*/;
      numArray5[21] = (byte) 72;
      numArray5[37] = (byte) 17;
      numArray5[29] = (byte) 5;
      numArray5[27] = (byte) 101;
      numArray5[13] = (byte) 37;
      numArray5[26] = (byte) 133;
      numArray5[8] = (byte) 38;
      numArray5[22] = (byte) 194;
      numArray5[34] = (byte) 193;
      numArray5[33] = (byte) 143;
      numArray5[30] = (byte) 64 /*0x40*/;
      numArray5[31 /*0x1F*/] = (byte) 100;
      numArray5[12] = (byte) 171;
      numArray5[14] = (byte) 63 /*0x3F*/;
      numArray5[4] = (byte) 130;
      numArray5[9] = (byte) 79;
      numArray5[16 /*0x10*/] = (byte) 145;
      numArray5[44] = (byte) 223;
      numArray5[43] = (byte) 174;
      numArray5[39] = (byte) 133;
      numArray5[6] = (byte) 141;
      numArray5[41] = (byte) 25;
      numArray5[42] = (byte) 97;
      numArray5[24] = (byte) 114;
      numArray5[20] = (byte) 229;
      numArray5[45] = (byte) 253;
      numArray5[28] = (byte) 151;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 47);
      for (int index = 0; index < 47; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[102];
    byte[] numArray7 = new byte[55];
    numArray7[35] = (byte) 66;
    numArray7[9] = (byte) 45;
    numArray7[2] = (byte) 177;
    numArray7[3] = (byte) 236;
    numArray7[4] = (byte) 67;
    numArray7[42] = (byte) 70;
    numArray7[29] = (byte) 180;
    numArray7[7] = (byte) 60;
    numArray7[8] = (byte) 136;
    numArray7[22] = (byte) 112 /*0x70*/;
    numArray7[10] = (byte) 167;
    numArray7[11] = (byte) 250;
    numArray7[12] = (byte) 8;
    numArray7[37] = (byte) 79;
    numArray7[0] = (byte) 63 /*0x3F*/;
    numArray7[15] = (byte) 148;
    numArray7[16 /*0x10*/] = (byte) 167;
    numArray7[17] = (byte) 233;
    numArray7[26] = (byte) 3;
    numArray7[30] = (byte) 244;
    numArray7[20] = (byte) 128 /*0x80*/;
    numArray7[21] = (byte) 202;
    numArray7[52] = (byte) 73;
    numArray7[23] = (byte) 213;
    numArray7[24] = (byte) 61;
    numArray7[18] = (byte) 62;
    numArray7[34] = (byte) 112 /*0x70*/;
    numArray7[40] = (byte) 83;
    numArray7[50] = (byte) 222;
    numArray7[14] = (byte) 99;
    numArray7[54] = (byte) 107;
    numArray7[31 /*0x1F*/] = (byte) 63 /*0x3F*/;
    numArray7[28] = (byte) 237;
    numArray7[25] = (byte) 220;
    numArray7[33] = (byte) 6;
    numArray7[27] = (byte) 149;
    numArray7[36] = (byte) 191;
    numArray7[49] = (byte) 80 /*0x50*/;
    numArray7[38] = (byte) 179;
    numArray7[39] = (byte) 182;
    numArray7[6] = (byte) 167;
    numArray7[41] = (byte) 166;
    numArray7[19] = (byte) 188;
    numArray7[43] = (byte) 86;
    numArray7[44] = (byte) 7;
    numArray7[47] = (byte) 203;
    numArray7[46] = (byte) 210;
    numArray7[5] = (byte) 129;
    numArray7[48 /*0x30*/] = (byte) 157;
    numArray7[1] = (byte) 90;
    numArray7[13] = (byte) 238;
    numArray7[51] = (byte) 180;
    numArray7[45] = (byte) 205;
    numArray7[53] = (byte) 90;
    numArray7[32 /*0x20*/] = (byte) 96 /*0x60*/;
    byte[] numArray8 = new byte[55];
    numArray8[20] = (byte) 249;
    numArray8[1] = (byte) 165;
    numArray8[2] = (byte) 73;
    numArray8[3] = (byte) 164;
    numArray8[51] = (byte) 74;
    numArray8[5] = (byte) 241;
    numArray8[25] = (byte) 152;
    numArray8[21] = (byte) 146;
    numArray8[8] = (byte) 43;
    numArray8[9] = (byte) 120;
    numArray8[32 /*0x20*/] = (byte) 80 /*0x50*/;
    numArray8[11] = (byte) 212;
    numArray8[0] = (byte) 76;
    numArray8[28] = (byte) 95;
    numArray8[14] = (byte) 27;
    numArray8[15] = (byte) 182;
    numArray8[17] = (byte) 156;
    numArray8[45] = (byte) 28;
    numArray8[41] = (byte) 93;
    numArray8[47] = (byte) 120;
    numArray8[40] = (byte) 228;
    numArray8[33] = (byte) 130;
    numArray8[12] = (byte) 191;
    numArray8[23] = (byte) 198;
    numArray8[13] = (byte) 207;
    numArray8[4] = (byte) 244;
    numArray8[26] = (byte) 113;
    numArray8[27] = (byte) 20;
    numArray8[35] = (byte) 210;
    numArray8[6] = (byte) 148;
    numArray8[38] = (byte) 134;
    numArray8[24] = (byte) 40;
    numArray8[10] = (byte) 247;
    numArray8[50] = (byte) 51;
    numArray8[34] = (byte) 231;
    numArray8[22] = (byte) 9;
    numArray8[36] = (byte) 130;
    numArray8[37] = (byte) 11;
    numArray8[43] = (byte) 160 /*0xA0*/;
    numArray8[39] = (byte) 138;
    numArray8[16 /*0x10*/] = (byte) 17;
    numArray8[54] = (byte) 225;
    numArray8[42] = (byte) 107;
    numArray8[30] = (byte) 86;
    numArray8[44] = (byte) 29;
    numArray8[19] = (byte) 246;
    numArray8[18] = (byte) 253;
    numArray8[31 /*0x1F*/] = (byte) 181;
    numArray8[48 /*0x30*/] = (byte) 136;
    numArray8[49] = (byte) 232;
    numArray8[46] = (byte) 96 /*0x60*/;
    numArray8[29] = (byte) 128 /*0x80*/;
    numArray8[52] = (byte) 88;
    numArray8[53] = (byte) 231;
    numArray8[7] = (byte) 32 /*0x20*/;
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[47];
    numArray9[26] = (byte) 152;
    numArray9[1] = (byte) 1;
    numArray9[11] = (byte) 120;
    numArray9[34] = (byte) 247;
    numArray9[0] = (byte) 223;
    numArray9[5] = (byte) 145;
    numArray9[24] = (byte) 31 /*0x1F*/;
    numArray9[7] = (byte) 56;
    numArray9[6] = (byte) 174;
    numArray9[4] = (byte) 156;
    numArray9[10] = (byte) 94;
    numArray9[15] = (byte) 248;
    numArray9[37] = (byte) 152;
    numArray9[13] = (byte) 26;
    numArray9[33] = (byte) 180;
    numArray9[20] = (byte) 213;
    numArray9[39] = (byte) 19;
    numArray9[32 /*0x20*/] = (byte) 43;
    numArray9[18] = (byte) 221;
    numArray9[19] = (byte) 156;
    numArray9[45] = (byte) 82;
    numArray9[21] = byte.MaxValue;
    numArray9[12] = (byte) 62;
    numArray9[23] = (byte) 210;
    numArray9[31 /*0x1F*/] = (byte) 249;
    numArray9[8] = (byte) 61;
    numArray9[46] = (byte) 117;
    numArray9[27] = (byte) 127 /*0x7F*/;
    numArray9[2] = (byte) 33;
    numArray9[30] = (byte) 72;
    numArray9[3] = (byte) 91;
    numArray9[35] = (byte) 97;
    numArray9[29] = (byte) 192 /*0xC0*/;
    numArray9[16 /*0x10*/] = (byte) 90;
    numArray9[38] = (byte) 139;
    numArray9[25] = (byte) 201;
    numArray9[36] = (byte) 206;
    numArray9[22] = (byte) 157;
    numArray9[14] = (byte) 84;
    numArray9[28] = (byte) 42;
    numArray9[40] = (byte) 198;
    numArray9[41] = (byte) 237;
    numArray9[42] = (byte) 241;
    numArray9[43] = (byte) 30;
    numArray9[44] = (byte) 10;
    numArray9[9] = (byte) 128 /*0x80*/;
    numArray9[17] = byte.MaxValue;
    byte[] numArray10 = new byte[47]
    {
      (byte) 29,
      (byte) 36,
      (byte) 210,
      (byte) 193,
      (byte) 150,
      (byte) 162,
      (byte) 34,
      (byte) 189,
      (byte) 201,
      (byte) 218,
      (byte) 117,
      (byte) 201,
      (byte) 94,
      (byte) 38,
      (byte) 81,
      (byte) 45,
      (byte) 108,
      (byte) 170,
      (byte) 32 /*0x20*/,
      (byte) 51,
      (byte) 21,
      (byte) 129,
      (byte) 104,
      (byte) 201,
      (byte) 47,
      (byte) 145,
      (byte) 167,
      (byte) 247,
      (byte) 250,
      (byte) 212,
      (byte) 59,
      (byte) 132,
      (byte) 237,
      (byte) 133,
      (byte) 10,
      (byte) 190,
      (byte) 222,
      (byte) 13,
      (byte) 149,
      (byte) 209,
      (byte) 78,
      (byte) 244,
      (byte) 99,
      (byte) 205,
      (byte) 28,
      (byte) 22,
      (byte) 218
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 47);
    for (int index = 0; index < 47; ++index)
      numArray6[index + 55] ^= numArray10[index];
    byte[] numArray11 = new byte[33];
    byte[] response = new byte[33];
    Array.Copy((Array) sc_12465.sspq, 20, (Array) numArray11, 0, 33);
    key.Query(true, 335, numArray11, response);
    Array.Copy((Array) sc_12465.sspr, 20, (Array) numArray11, 0, 33);
    for (int index = 0; index < numArray11.Length; ++index)
    {
      if ((int) numArray11[index] != (int) response[index])
      {
        key.TagValue = (int) response[index];
        break;
      }
    }
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_12485()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[97];
      byte[] numArray2 = new byte[55]
      {
        (byte) 152,
        (byte) 34,
        (byte) 200,
        (byte) 104,
        (byte) 221,
        (byte) 123,
        (byte) 24,
        (byte) 81,
        (byte) 36,
        (byte) 97,
        (byte) 124,
        (byte) 160 /*0xA0*/,
        (byte) 68,
        (byte) 24,
        (byte) 249,
        (byte) 239,
        (byte) 57,
        (byte) 74,
        (byte) 108,
        (byte) 93,
        (byte) 161,
        (byte) 43,
        (byte) 10,
        (byte) 160 /*0xA0*/,
        (byte) 23,
        (byte) 63 /*0x3F*/,
        (byte) 147,
        (byte) 201,
        (byte) 242,
        (byte) 158,
        (byte) 182,
        (byte) 44,
        (byte) 12,
        (byte) 59,
        (byte) 76,
        (byte) 99,
        (byte) 165,
        (byte) 22,
        (byte) 242,
        (byte) 175,
        (byte) 104,
        (byte) 130,
        (byte) 150,
        (byte) 58,
        (byte) 232,
        (byte) 51,
        (byte) 200,
        (byte) 220,
        (byte) 176 /*0xB0*/,
        (byte) 116,
        (byte) 128 /*0x80*/,
        (byte) 102,
        (byte) 211,
        (byte) 172,
        (byte) 246
      };
      byte[] numArray3 = new byte[55];
      numArray3[42] = (byte) 50;
      numArray3[1] = (byte) 49;
      numArray3[34] = (byte) 190;
      numArray3[3] = (byte) 94;
      numArray3[4] = (byte) 252;
      numArray3[9] = (byte) 39;
      numArray3[16 /*0x10*/] = (byte) 44;
      numArray3[36] = (byte) 117;
      numArray3[8] = (byte) 15;
      numArray3[24] = (byte) 138;
      numArray3[10] = (byte) 134;
      numArray3[11] = (byte) 159;
      numArray3[19] = (byte) 42;
      numArray3[13] = (byte) 240 /*0xF0*/;
      numArray3[45] = (byte) 62;
      numArray3[26] = (byte) 75;
      numArray3[6] = (byte) 210;
      numArray3[17] = (byte) 52;
      numArray3[18] = (byte) 246;
      numArray3[14] = (byte) 189;
      numArray3[20] = (byte) 214;
      numArray3[21] = (byte) 140;
      numArray3[22] = (byte) 185;
      numArray3[23] = (byte) 74;
      numArray3[12] = (byte) 99;
      numArray3[29] = (byte) 77;
      numArray3[25] = (byte) 177;
      numArray3[46] = (byte) 81;
      numArray3[32 /*0x20*/] = (byte) 48 /*0x30*/;
      numArray3[31 /*0x1F*/] = (byte) 141;
      numArray3[0] = (byte) 187;
      numArray3[53] = (byte) 56;
      numArray3[28] = (byte) 70;
      numArray3[33] = (byte) 191;
      numArray3[15] = (byte) 183;
      numArray3[30] = (byte) 47;
      numArray3[5] = (byte) 14;
      numArray3[37] = (byte) 49;
      numArray3[52] = (byte) 176 /*0xB0*/;
      numArray3[51] = (byte) 106;
      numArray3[40] = (byte) 254;
      numArray3[48 /*0x30*/] = (byte) 156;
      numArray3[38] = (byte) 163;
      numArray3[44] = (byte) 204;
      numArray3[35] = (byte) 104;
      numArray3[39] = (byte) 145;
      numArray3[27] = (byte) 231;
      numArray3[47] = (byte) 47;
      numArray3[7] = (byte) 104;
      numArray3[49] = (byte) 28;
      numArray3[50] = (byte) 238;
      numArray3[43] = (byte) 115;
      numArray3[2] = (byte) 166;
      numArray3[41] = (byte) 237;
      numArray3[54] = (byte) 198;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[42];
      numArray4[21] = (byte) 30;
      numArray4[38] = (byte) 227;
      numArray4[35] = (byte) 88;
      numArray4[3] = (byte) 50;
      numArray4[13] = (byte) 205;
      numArray4[25] = (byte) 253;
      numArray4[5] = (byte) 223;
      numArray4[0] = (byte) 113;
      numArray4[31 /*0x1F*/] = (byte) 108;
      numArray4[2] = (byte) 38;
      numArray4[10] = (byte) 196;
      numArray4[32 /*0x20*/] = (byte) 6;
      numArray4[16 /*0x10*/] = (byte) 245;
      numArray4[1] = (byte) 108;
      numArray4[14] = (byte) 94;
      numArray4[15] = (byte) 3;
      numArray4[17] = (byte) 218;
      numArray4[8] = (byte) 246;
      numArray4[18] = (byte) 249;
      numArray4[19] = (byte) 131;
      numArray4[24] = (byte) 166;
      numArray4[9] = (byte) 52;
      numArray4[23] = (byte) 184;
      numArray4[11] = (byte) 237;
      numArray4[26] = (byte) 84;
      numArray4[7] = (byte) 91;
      numArray4[39] = (byte) 68;
      numArray4[27] = (byte) 192 /*0xC0*/;
      numArray4[28] = (byte) 64 /*0x40*/;
      numArray4[6] = (byte) 104;
      numArray4[40] = (byte) 168;
      numArray4[30] = (byte) 168;
      numArray4[37] = (byte) 148;
      numArray4[4] = (byte) 54;
      numArray4[34] = (byte) 11;
      numArray4[36] = (byte) 45;
      numArray4[12] = (byte) 74;
      numArray4[22] = (byte) 69;
      numArray4[20] = (byte) 64 /*0x40*/;
      numArray4[33] = (byte) 2;
      numArray4[29] = (byte) 66;
      numArray4[41] = (byte) 51;
      byte[] numArray5 = new byte[42]
      {
        (byte) 196,
        (byte) 3,
        (byte) 204,
        (byte) 11,
        (byte) 60,
        (byte) 248,
        (byte) 187,
        (byte) 106,
        (byte) 104,
        (byte) 27,
        (byte) 60,
        (byte) 230,
        (byte) 131,
        (byte) 165,
        (byte) 184,
        (byte) 231,
        (byte) 96 /*0x60*/,
        (byte) 30,
        (byte) 93,
        (byte) 19,
        (byte) 71,
        (byte) 135,
        (byte) 17,
        (byte) 113,
        (byte) 54,
        (byte) 246,
        (byte) 40,
        (byte) 136,
        (byte) 42,
        (byte) 141,
        (byte) 42,
        (byte) 185,
        (byte) 128 /*0x80*/,
        (byte) 214,
        (byte) 84,
        (byte) 49,
        (byte) 62,
        (byte) 11,
        (byte) 207,
        (byte) 53,
        (byte) 129,
        (byte) 109
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 42);
      for (int index = 0; index < 42; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[97];
    byte[] numArray7 = new byte[55]
    {
      (byte) 177,
      (byte) 250,
      (byte) 184,
      (byte) 48 /*0x30*/,
      (byte) 190,
      (byte) 9,
      (byte) 171,
      (byte) 185,
      (byte) 0,
      (byte) 24,
      (byte) 238,
      (byte) 223,
      (byte) 163,
      (byte) 59,
      (byte) 136,
      (byte) 153,
      (byte) 248,
      (byte) 53,
      (byte) 192 /*0xC0*/,
      (byte) 229,
      (byte) 41,
      (byte) 121,
      (byte) 250,
      (byte) 215,
      (byte) 77,
      (byte) 177,
      (byte) 7,
      (byte) 10,
      (byte) 147,
      (byte) 229,
      (byte) 229,
      (byte) 146,
      (byte) 192 /*0xC0*/,
      (byte) 200,
      (byte) 52,
      (byte) 77,
      (byte) 10,
      (byte) 214,
      (byte) 24,
      (byte) 87,
      (byte) 75,
      (byte) 251,
      (byte) 218,
      (byte) 245,
      (byte) 140,
      (byte) 250,
      (byte) 25,
      (byte) 71,
      (byte) 164,
      (byte) 93,
      (byte) 201,
      (byte) 0,
      (byte) 97,
      (byte) 229,
      (byte) 101
    };
    byte[] numArray8 = new byte[55];
    numArray8[15] = (byte) 55;
    numArray8[45] = (byte) 178;
    numArray8[41] = (byte) 0;
    numArray8[3] = (byte) 12;
    numArray8[34] = (byte) 116;
    numArray8[33] = (byte) 133;
    numArray8[6] = (byte) 111;
    numArray8[2] = (byte) 216;
    numArray8[35] = (byte) 24;
    numArray8[9] = (byte) 201;
    numArray8[10] = (byte) 200;
    numArray8[1] = (byte) 7;
    numArray8[12] = (byte) 110;
    numArray8[11] = (byte) 119;
    numArray8[14] = (byte) 213;
    numArray8[4] = (byte) 154;
    numArray8[16 /*0x10*/] = (byte) 122;
    numArray8[17] = (byte) 9;
    numArray8[50] = (byte) 142;
    numArray8[8] = (byte) 8;
    numArray8[20] = (byte) 57;
    numArray8[36] = (byte) 195;
    numArray8[18] = (byte) 239;
    numArray8[21] = (byte) 235;
    numArray8[19] = (byte) 80 /*0x50*/;
    numArray8[25] = (byte) 146;
    numArray8[23] = (byte) 69;
    numArray8[13] = (byte) 121;
    numArray8[28] = (byte) 191;
    numArray8[29] = (byte) 105;
    numArray8[30] = (byte) 188;
    numArray8[31 /*0x1F*/] = (byte) 43;
    numArray8[32 /*0x20*/] = (byte) 22;
    numArray8[51] = (byte) 91;
    numArray8[24] = (byte) 114;
    numArray8[38] = (byte) 39;
    numArray8[0] = (byte) 232;
    numArray8[37] = (byte) 130;
    numArray8[40] = (byte) 77;
    numArray8[39] = (byte) 173;
    numArray8[26] = (byte) 134;
    numArray8[22] = (byte) 108;
    numArray8[42] = (byte) 71;
    numArray8[27] = (byte) 135;
    numArray8[44] = (byte) 190;
    numArray8[7] = (byte) 81;
    numArray8[46] = (byte) 210;
    numArray8[47] = (byte) 249;
    numArray8[48 /*0x30*/] = (byte) 105;
    numArray8[49] = (byte) 13;
    numArray8[43] = (byte) 172;
    numArray8[5] = (byte) 178;
    numArray8[52] = (byte) 131;
    numArray8[53] = (byte) 186;
    numArray8[54] = (byte) 177;
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[42];
    numArray9[24] = (byte) 127 /*0x7F*/;
    numArray9[1] = (byte) 143;
    numArray9[17] = (byte) 239;
    numArray9[3] = (byte) 65;
    numArray9[21] = (byte) 25;
    numArray9[0] = (byte) 150;
    numArray9[6] = byte.MaxValue;
    numArray9[7] = (byte) 77;
    numArray9[38] = (byte) 112 /*0x70*/;
    numArray9[9] = (byte) 66;
    numArray9[10] = (byte) 119;
    numArray9[2] = (byte) 116;
    numArray9[29] = (byte) 56;
    numArray9[13] = (byte) 102;
    numArray9[14] = (byte) 46;
    numArray9[8] = (byte) 42;
    numArray9[18] = (byte) 87;
    numArray9[37] = (byte) 20;
    numArray9[40] = (byte) 191;
    numArray9[25] = (byte) 252;
    numArray9[12] = (byte) 236;
    numArray9[15] = (byte) 17;
    numArray9[22] = (byte) 118;
    numArray9[20] = (byte) 66;
    numArray9[16 /*0x10*/] = (byte) 18;
    numArray9[36] = (byte) 151;
    numArray9[26] = (byte) 103;
    numArray9[27] = (byte) 115;
    numArray9[23] = (byte) 89;
    numArray9[11] = (byte) 10;
    numArray9[34] = (byte) 147;
    numArray9[31 /*0x1F*/] = (byte) 43;
    numArray9[28] = (byte) 2;
    numArray9[33] = (byte) 83;
    numArray9[5] = (byte) 239;
    numArray9[35] = (byte) 100;
    numArray9[30] = (byte) 144 /*0x90*/;
    numArray9[4] = (byte) 58;
    numArray9[39] = (byte) 230;
    numArray9[32 /*0x20*/] = (byte) 98;
    numArray9[19] = (byte) 224 /*0xE0*/;
    numArray9[41] = (byte) 117;
    byte[] numArray10 = new byte[42]
    {
      (byte) 119,
      (byte) 207,
      (byte) 47,
      (byte) 219,
      (byte) 170,
      (byte) 154,
      (byte) 32 /*0x20*/,
      (byte) 217,
      (byte) 180,
      (byte) 208 /*0xD0*/,
      (byte) 185,
      (byte) 123,
      (byte) 59,
      (byte) 169,
      (byte) 245,
      (byte) 6,
      (byte) 8,
      (byte) 45,
      (byte) 58,
      (byte) 152,
      (byte) 131,
      (byte) 118,
      (byte) 45,
      (byte) 3,
      (byte) 17,
      (byte) 189,
      (byte) 164,
      (byte) 17,
      (byte) 250,
      (byte) 15,
      (byte) 242,
      (byte) 8,
      (byte) 252,
      (byte) 138,
      (byte) 36,
      (byte) 84,
      (byte) 114,
      (byte) 146,
      (byte) 89,
      (byte) 254,
      (byte) 131,
      (byte) 170
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 42);
    for (int index = 0; index < 42; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_12486()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 155,
        (byte) 23,
        (byte) 75,
        (byte) 125,
        (byte) 82,
        (byte) 50,
        (byte) 95,
        (byte) 51,
        (byte) 42,
        (byte) 19
      };
      byte[] numArray3 = new byte[10]
      {
        (byte) 133,
        (byte) 58,
        (byte) 215,
        (byte) 0,
        (byte) 30,
        (byte) 168,
        (byte) 192 /*0xC0*/,
        (byte) 89,
        (byte) 48 /*0x30*/,
        (byte) 150
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[19];
      byte[] response = new byte[19];
      Array.Copy((Array) sc_12465.sspq, 53, (Array) numArray4, 0, 19);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_12465.sspr, 53, (Array) numArray4, 0, 19);
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
    byte[] numArray5 = new byte[10];
    byte[] numArray6 = new byte[10];
    numArray6[4] = (byte) 252;
    numArray6[7] = (byte) 17;
    numArray6[2] = (byte) 187;
    numArray6[3] = (byte) 125;
    numArray6[8] = (byte) 237;
    numArray6[6] = (byte) 75;
    numArray6[5] = (byte) 171;
    numArray6[0] = (byte) 254;
    numArray6[1] = (byte) 253;
    numArray6[9] = (byte) 162;
    byte[] numArray7 = new byte[10]
    {
      (byte) 172,
      (byte) 5,
      (byte) 128 /*0x80*/,
      (byte) 207,
      (byte) 47,
      (byte) 148,
      (byte) 5,
      (byte) 138,
      (byte) 171,
      (byte) 116
    };
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_appserver_12487()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[95];
      byte[] numArray2 = new byte[55];
      numArray2[24] = (byte) 36;
      numArray2[27] = (byte) 63 /*0x3F*/;
      numArray2[2] = (byte) 216;
      numArray2[41] = (byte) 142;
      numArray2[4] = (byte) 215;
      numArray2[11] = (byte) 230;
      numArray2[8] = (byte) 103;
      numArray2[7] = (byte) 113;
      numArray2[14] = (byte) 91;
      numArray2[29] = (byte) 39;
      numArray2[10] = (byte) 143;
      numArray2[13] = (byte) 49;
      numArray2[5] = (byte) 161;
      numArray2[16 /*0x10*/] = (byte) 145;
      numArray2[19] = (byte) 71;
      numArray2[15] = (byte) 60;
      numArray2[25] = (byte) 29;
      numArray2[9] = (byte) 121;
      numArray2[12] = (byte) 217;
      numArray2[33] = (byte) 12;
      numArray2[39] = (byte) 75;
      numArray2[20] = (byte) 103;
      numArray2[34] = (byte) 109;
      numArray2[23] = (byte) 190;
      numArray2[38] = (byte) 58;
      numArray2[18] = (byte) 170;
      numArray2[26] = (byte) 70;
      numArray2[6] = (byte) 17;
      numArray2[32 /*0x20*/] = (byte) 75;
      numArray2[36] = (byte) 211;
      numArray2[30] = (byte) 36;
      numArray2[31 /*0x1F*/] = (byte) 237;
      numArray2[1] = (byte) 246;
      numArray2[45] = (byte) 179;
      numArray2[42] = (byte) 143;
      numArray2[35] = (byte) 5;
      numArray2[0] = (byte) 16 /*0x10*/;
      numArray2[37] = (byte) 216;
      numArray2[52] = (byte) 102;
      numArray2[46] = (byte) 144 /*0x90*/;
      numArray2[21] = (byte) 68;
      numArray2[17] = (byte) 93;
      numArray2[28] = (byte) 56;
      numArray2[43] = (byte) 160 /*0xA0*/;
      numArray2[44] = (byte) 43;
      numArray2[22] = (byte) 109;
      numArray2[3] = (byte) 115;
      numArray2[40] = (byte) 145;
      numArray2[48 /*0x30*/] = (byte) 126;
      numArray2[49] = (byte) 44;
      numArray2[50] = (byte) 197;
      numArray2[51] = (byte) 126;
      numArray2[47] = (byte) 25;
      numArray2[53] = (byte) 106;
      numArray2[54] = (byte) 70;
      byte[] numArray3 = new byte[55];
      numArray3[13] = (byte) 101;
      numArray3[0] = (byte) 123;
      numArray3[51] = (byte) 27;
      numArray3[48 /*0x30*/] = (byte) 8;
      numArray3[47] = (byte) 153;
      numArray3[11] = (byte) 109;
      numArray3[39] = (byte) 4;
      numArray3[19] = (byte) 2;
      numArray3[21] = (byte) 40;
      numArray3[53] = (byte) 188;
      numArray3[10] = (byte) 72;
      numArray3[3] = (byte) 131;
      numArray3[12] = (byte) 175;
      numArray3[35] = (byte) 244;
      numArray3[14] = (byte) 103;
      numArray3[15] = (byte) 107;
      numArray3[9] = (byte) 100;
      numArray3[5] = (byte) 105;
      numArray3[18] = (byte) 209;
      numArray3[31 /*0x1F*/] = (byte) 201;
      numArray3[20] = (byte) 112 /*0x70*/;
      numArray3[4] = (byte) 14;
      numArray3[6] = (byte) 110;
      numArray3[23] = (byte) 13;
      numArray3[16 /*0x10*/] = (byte) 125;
      numArray3[25] = (byte) 108;
      numArray3[26] = (byte) 145;
      numArray3[27] = (byte) 33;
      numArray3[17] = (byte) 180;
      numArray3[2] = (byte) 25;
      numArray3[30] = (byte) 108;
      numArray3[8] = (byte) 139;
      numArray3[32 /*0x20*/] = (byte) 158;
      numArray3[33] = (byte) 208 /*0xD0*/;
      numArray3[34] = (byte) 25;
      numArray3[7] = (byte) 112 /*0x70*/;
      numArray3[36] = (byte) 76;
      numArray3[44] = (byte) 208 /*0xD0*/;
      numArray3[22] = (byte) 61;
      numArray3[29] = (byte) 87;
      numArray3[40] = (byte) 252;
      numArray3[41] = (byte) 9;
      numArray3[42] = (byte) 4;
      numArray3[43] = (byte) 93;
      numArray3[28] = (byte) 21;
      numArray3[1] = (byte) 210;
      numArray3[37] = (byte) 222;
      numArray3[24] = (byte) 118;
      numArray3[38] = (byte) 176 /*0xB0*/;
      numArray3[49] = (byte) 50;
      numArray3[50] = (byte) 171;
      numArray3[46] = (byte) 132;
      numArray3[52] = (byte) 24;
      numArray3[45] = (byte) 169;
      numArray3[54] = (byte) 136;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[40]
      {
        (byte) 127 /*0x7F*/,
        (byte) 135,
        (byte) 100,
        (byte) 86,
        (byte) 120,
        (byte) 227,
        (byte) 134,
        (byte) 98,
        (byte) 156,
        (byte) 141,
        (byte) 174,
        (byte) 154,
        (byte) 26,
        (byte) 128 /*0x80*/,
        (byte) 133,
        (byte) 78,
        (byte) 172,
        (byte) 71,
        (byte) 91,
        (byte) 243,
        (byte) 226,
        (byte) 154,
        (byte) 247,
        (byte) 10,
        (byte) 166,
        (byte) 72,
        (byte) 56,
        (byte) 122,
        (byte) 146,
        (byte) 228,
        (byte) 109,
        (byte) 110,
        (byte) 215,
        (byte) 130,
        (byte) 176 /*0xB0*/,
        (byte) 35,
        (byte) 239,
        (byte) 107,
        (byte) 54,
        (byte) 68
      };
      byte[] numArray5 = new byte[40]
      {
        (byte) 185,
        (byte) 143,
        (byte) 30,
        (byte) 192 /*0xC0*/,
        (byte) 249,
        (byte) 170,
        (byte) 232,
        (byte) 161,
        (byte) 181,
        (byte) 250,
        (byte) 164,
        (byte) 90,
        (byte) 107,
        (byte) 173,
        (byte) 98,
        (byte) 84,
        (byte) 62,
        (byte) 247,
        (byte) 1,
        (byte) 201,
        (byte) 78,
        (byte) 35,
        (byte) 254,
        (byte) 18,
        (byte) 210,
        (byte) 247,
        (byte) 78,
        (byte) 155,
        (byte) 1,
        (byte) 62,
        (byte) 225,
        (byte) 28,
        (byte) 8,
        (byte) 165,
        (byte) 226,
        (byte) 120,
        (byte) 73,
        (byte) 251,
        (byte) 203,
        (byte) 46
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 40);
      for (int index = 0; index < 40; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[95];
    byte[] numArray7 = new byte[55]
    {
      (byte) 210,
      (byte) 15,
      (byte) 150,
      (byte) 53,
      (byte) 144 /*0x90*/,
      (byte) 86,
      (byte) 106,
      (byte) 237,
      (byte) 119,
      (byte) 142,
      (byte) 190,
      (byte) 89,
      (byte) 229,
      (byte) 143,
      (byte) 89,
      (byte) 176 /*0xB0*/,
      (byte) 48 /*0x30*/,
      (byte) 170,
      (byte) 12,
      (byte) 208 /*0xD0*/,
      (byte) 184,
      (byte) 31 /*0x1F*/,
      (byte) 63 /*0x3F*/,
      (byte) 195,
      (byte) 151,
      (byte) 207,
      (byte) 98,
      (byte) 95,
      (byte) 64 /*0x40*/,
      (byte) 17,
      (byte) 22,
      (byte) 8,
      (byte) 167,
      (byte) 200,
      (byte) 37,
      (byte) 251,
      (byte) 15,
      (byte) 18,
      (byte) 168,
      (byte) 35,
      (byte) 39,
      (byte) 246,
      (byte) 245,
      (byte) 224 /*0xE0*/,
      (byte) 25,
      (byte) 41,
      (byte) 203,
      (byte) 147,
      (byte) 149,
      (byte) 186,
      (byte) 5,
      (byte) 154,
      (byte) 222,
      (byte) 106,
      (byte) 24
    };
    byte[] numArray8 = new byte[55];
    numArray8[20] = (byte) 89;
    numArray8[35] = (byte) 156;
    numArray8[12] = (byte) 171;
    numArray8[47] = (byte) 26;
    numArray8[28] = (byte) 43;
    numArray8[5] = (byte) 166;
    numArray8[6] = (byte) 7;
    numArray8[7] = (byte) 241;
    numArray8[2] = (byte) 75;
    numArray8[1] = (byte) 14;
    numArray8[14] = (byte) 165;
    numArray8[4] = (byte) 24;
    numArray8[18] = (byte) 43;
    numArray8[42] = (byte) 50;
    numArray8[27] = (byte) 100;
    numArray8[51] = (byte) 214;
    numArray8[31 /*0x1F*/] = (byte) 252;
    numArray8[54] = (byte) 8;
    numArray8[43] = (byte) 76;
    numArray8[46] = (byte) 113;
    numArray8[9] = (byte) 91;
    numArray8[26] = (byte) 62;
    numArray8[36] = (byte) 228;
    numArray8[23] = (byte) 32 /*0x20*/;
    numArray8[10] = (byte) 39;
    numArray8[25] = (byte) 54;
    numArray8[45] = (byte) 1;
    numArray8[17] = (byte) 132;
    numArray8[8] = (byte) 104;
    numArray8[29] = (byte) 145;
    numArray8[30] = (byte) 74;
    numArray8[16 /*0x10*/] = (byte) 229;
    numArray8[32 /*0x20*/] = (byte) 148;
    numArray8[33] = (byte) 132;
    numArray8[34] = (byte) 155;
    numArray8[52] = (byte) 144 /*0x90*/;
    numArray8[50] = (byte) 245;
    numArray8[37] = (byte) 225;
    numArray8[38] = (byte) 224 /*0xE0*/;
    numArray8[3] = (byte) 164;
    numArray8[15] = (byte) 207;
    numArray8[41] = (byte) 4;
    numArray8[49] = (byte) 117;
    numArray8[11] = (byte) 126;
    numArray8[39] = (byte) 124;
    numArray8[19] = (byte) 194;
    numArray8[24] = (byte) 192 /*0xC0*/;
    numArray8[0] = (byte) 245;
    numArray8[48 /*0x30*/] = (byte) 37;
    numArray8[21] = (byte) 250;
    numArray8[13] = (byte) 190;
    numArray8[22] = (byte) 37;
    numArray8[40] = (byte) 108;
    numArray8[53] = (byte) 231;
    numArray8[44] = (byte) 103;
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[40];
    numArray9[23] = (byte) 85;
    numArray9[1] = (byte) 48 /*0x30*/;
    numArray9[29] = (byte) 51;
    numArray9[32 /*0x20*/] = (byte) 235;
    numArray9[4] = (byte) 64 /*0x40*/;
    numArray9[5] = (byte) 38;
    numArray9[6] = (byte) 91;
    numArray9[7] = (byte) 250;
    numArray9[8] = (byte) 140;
    numArray9[9] = (byte) 117;
    numArray9[2] = (byte) 187;
    numArray9[34] = (byte) 19;
    numArray9[12] = (byte) 50;
    numArray9[28] = (byte) 227;
    numArray9[26] = (byte) 108;
    numArray9[39] = (byte) 35;
    numArray9[19] = (byte) 155;
    numArray9[18] = (byte) 120;
    numArray9[14] = (byte) 169;
    numArray9[15] = (byte) 25;
    numArray9[20] = (byte) 246;
    numArray9[21] = (byte) 70;
    numArray9[22] = (byte) 247;
    numArray9[0] = (byte) 41;
    numArray9[24] = (byte) 165;
    numArray9[25] = (byte) 146;
    numArray9[11] = (byte) 246;
    numArray9[27] = (byte) 36;
    numArray9[31 /*0x1F*/] = (byte) 88;
    numArray9[3] = (byte) 37;
    numArray9[30] = (byte) 122;
    numArray9[36] = (byte) 43;
    numArray9[10] = (byte) 108;
    numArray9[33] = (byte) 187;
    numArray9[16 /*0x10*/] = (byte) 186;
    numArray9[35] = (byte) 68;
    numArray9[17] = (byte) 139;
    numArray9[37] = (byte) 9;
    numArray9[38] = (byte) 220;
    numArray9[13] = (byte) 99;
    byte[] numArray10 = new byte[40];
    numArray10[0] = (byte) 197;
    numArray10[39] = (byte) 230;
    numArray10[2] = (byte) 140;
    numArray10[3] = (byte) 37;
    numArray10[32 /*0x20*/] = (byte) 39;
    numArray10[5] = (byte) 136;
    numArray10[6] = (byte) 105;
    numArray10[19] = (byte) 24;
    numArray10[25] = (byte) 234;
    numArray10[1] = (byte) 45;
    numArray10[8] = (byte) 88;
    numArray10[11] = (byte) 200;
    numArray10[17] = (byte) 218;
    numArray10[13] = (byte) 230;
    numArray10[7] = (byte) 61;
    numArray10[15] = (byte) 43;
    numArray10[38] = (byte) 6;
    numArray10[24] = (byte) 79;
    numArray10[18] = (byte) 210;
    numArray10[9] = (byte) 176 /*0xB0*/;
    numArray10[10] = (byte) 38;
    numArray10[35] = (byte) 109;
    numArray10[14] = (byte) 230;
    numArray10[23] = (byte) 111;
    numArray10[12] = (byte) 74;
    numArray10[21] = (byte) 66;
    numArray10[26] = (byte) 83;
    numArray10[27] = (byte) 143;
    numArray10[28] = (byte) 104;
    numArray10[4] = (byte) 109;
    numArray10[30] = (byte) 218;
    numArray10[31 /*0x1F*/] = (byte) 39;
    numArray10[33] = (byte) 184;
    numArray10[29] = (byte) 63 /*0x3F*/;
    numArray10[34] = (byte) 162;
    numArray10[20] = (byte) 6;
    numArray10[36] = (byte) 139;
    numArray10[37] = (byte) 56;
    numArray10[16 /*0x10*/] = (byte) 170;
    numArray10[22] = (byte) 71;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 40);
    for (int index = 0; index < 40; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static int ssp_appserver_12488(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 40,
      (byte) 40,
      (byte) 129,
      (byte) 236,
      (byte) 107,
      (byte) 48 /*0x30*/,
      (byte) 241,
      (byte) 240 /*0xF0*/,
      (byte) 228,
      (byte) 74,
      (byte) 1,
      (byte) 79,
      (byte) 14,
      (byte) 212,
      (byte) 111,
      (byte) 187,
      (byte) 239,
      (byte) 122,
      (byte) 78,
      (byte) 190,
      (byte) 201,
      (byte) 114,
      (byte) 122,
      (byte) 14,
      (byte) 99,
      (byte) 172,
      (byte) 164,
      (byte) 30,
      (byte) 77,
      (byte) 148,
      (byte) 27,
      (byte) 86,
      (byte) 72,
      (byte) 126,
      (byte) 104,
      (byte) 97,
      (byte) 6,
      (byte) 157,
      (byte) 25,
      (byte) 193,
      (byte) 129,
      (byte) 81,
      (byte) 110,
      (byte) 195,
      (byte) 29,
      (byte) 181,
      (byte) 66,
      (byte) 221
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 98,
      (byte) 209,
      (byte) 132,
      (byte) 251,
      (byte) 91,
      (byte) 203,
      (byte) 50,
      (byte) 244,
      (byte) 91,
      (byte) 4,
      (byte) 88,
      (byte) 214,
      (byte) 216,
      (byte) 75,
      (byte) 59,
      (byte) 204,
      (byte) 223,
      (byte) 207,
      (byte) 80 /*0x50*/,
      (byte) 207,
      (byte) 33,
      (byte) 34,
      (byte) 215,
      (byte) 1,
      (byte) 163,
      (byte) 59,
      (byte) 201,
      (byte) 23,
      (byte) 45,
      (byte) 127 /*0x7F*/,
      (byte) 65,
      (byte) 249,
      (byte) 209,
      (byte) 21,
      (byte) 158,
      (byte) 216,
      (byte) 68,
      (byte) 171,
      (byte) 179,
      (byte) 247,
      (byte) 197,
      (byte) 6,
      (byte) 33,
      (byte) 142,
      (byte) 150,
      (byte) 15,
      byte.MaxValue,
      (byte) 95
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_12489()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[95];
      byte[] numArray2 = new byte[55]
      {
        (byte) 232,
        (byte) 95,
        (byte) 144 /*0x90*/,
        (byte) 182,
        (byte) 173,
        (byte) 114,
        (byte) 136,
        (byte) 191,
        (byte) 140,
        (byte) 210,
        (byte) 163,
        (byte) 80 /*0x50*/,
        (byte) 47,
        (byte) 171,
        (byte) 6,
        (byte) 143,
        (byte) 108,
        (byte) 14,
        (byte) 181,
        (byte) 81,
        (byte) 97,
        (byte) 140,
        (byte) 61,
        (byte) 119,
        (byte) 3,
        (byte) 183,
        (byte) 240 /*0xF0*/,
        (byte) 173,
        (byte) 106,
        (byte) 198,
        (byte) 46,
        (byte) 80 /*0x50*/,
        (byte) 233,
        (byte) 136,
        (byte) 109,
        (byte) 232,
        (byte) 237,
        (byte) 177,
        (byte) 179,
        (byte) 147,
        (byte) 21,
        (byte) 90,
        (byte) 53,
        (byte) 174,
        (byte) 173,
        (byte) 30,
        (byte) 148,
        (byte) 114,
        (byte) 174,
        (byte) 178,
        (byte) 113,
        (byte) 225,
        (byte) 70,
        (byte) 89,
        (byte) 49
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 34,
        (byte) 39,
        (byte) 221,
        (byte) 251,
        (byte) 5,
        (byte) 151,
        (byte) 138,
        (byte) 33,
        (byte) 14,
        (byte) 162,
        (byte) 225,
        (byte) 98,
        (byte) 106,
        (byte) 22,
        (byte) 151,
        (byte) 1,
        (byte) 207,
        (byte) 143,
        (byte) 73,
        (byte) 235,
        (byte) 69,
        (byte) 179,
        (byte) 7,
        (byte) 192 /*0xC0*/,
        (byte) 250,
        (byte) 144 /*0x90*/,
        (byte) 26,
        (byte) 192 /*0xC0*/,
        byte.MaxValue,
        (byte) 240 /*0xF0*/,
        (byte) 88,
        (byte) 99,
        (byte) 78,
        (byte) 49,
        (byte) 22,
        (byte) 216,
        (byte) 98,
        (byte) 160 /*0xA0*/,
        (byte) 60,
        (byte) 121,
        (byte) 63 /*0x3F*/,
        (byte) 157,
        (byte) 187,
        (byte) 154,
        (byte) 55,
        (byte) 113,
        (byte) 24,
        (byte) 217,
        (byte) 3,
        (byte) 124,
        (byte) 185,
        (byte) 101,
        (byte) 105,
        (byte) 91,
        (byte) 136
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[40]
      {
        (byte) 24,
        (byte) 178,
        (byte) 246,
        (byte) 129,
        (byte) 218,
        (byte) 85,
        (byte) 197,
        (byte) 164,
        (byte) 82,
        (byte) 102,
        (byte) 166,
        (byte) 61,
        (byte) 30,
        (byte) 162,
        (byte) 186,
        (byte) 90,
        (byte) 33,
        (byte) 60,
        (byte) 6,
        (byte) 86,
        (byte) 143,
        (byte) 202,
        (byte) 229,
        (byte) 34,
        (byte) 126,
        byte.MaxValue,
        (byte) 183,
        (byte) 85,
        (byte) 76,
        (byte) 10,
        (byte) 144 /*0x90*/,
        (byte) 87,
        (byte) 17,
        (byte) 197,
        (byte) 212,
        byte.MaxValue,
        (byte) 188,
        (byte) 18,
        (byte) 58,
        (byte) 5
      };
      byte[] numArray5 = new byte[40];
      numArray5[33] = (byte) 135;
      numArray5[38] = (byte) 115;
      numArray5[2] = (byte) 103;
      numArray5[3] = (byte) 179;
      numArray5[24] = (byte) 113;
      numArray5[5] = (byte) 53;
      numArray5[23] = (byte) 133;
      numArray5[17] = (byte) 211;
      numArray5[11] = (byte) 92;
      numArray5[32 /*0x20*/] = (byte) 105;
      numArray5[15] = (byte) 130;
      numArray5[4] = (byte) 12;
      numArray5[9] = (byte) 249;
      numArray5[13] = (byte) 52;
      numArray5[14] = (byte) 197;
      numArray5[34] = (byte) 79;
      numArray5[1] = (byte) 251;
      numArray5[22] = (byte) 103;
      numArray5[10] = (byte) 123;
      numArray5[0] = (byte) 221;
      numArray5[20] = (byte) 208 /*0xD0*/;
      numArray5[21] = (byte) 176 /*0xB0*/;
      numArray5[28] = (byte) 49;
      numArray5[26] = (byte) 95;
      numArray5[7] = (byte) 103;
      numArray5[16 /*0x10*/] = (byte) 47;
      numArray5[6] = (byte) 203;
      numArray5[25] = (byte) 184;
      numArray5[8] = (byte) 145;
      numArray5[12] = (byte) 104;
      numArray5[30] = (byte) 54;
      numArray5[31 /*0x1F*/] = (byte) 13;
      numArray5[19] = (byte) 137;
      numArray5[29] = (byte) 138;
      numArray5[18] = (byte) 228;
      numArray5[35] = (byte) 50;
      numArray5[36] = (byte) 34;
      numArray5[37] = (byte) 35;
      numArray5[27] = (byte) 148;
      numArray5[39] = (byte) 45;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 40);
      for (int index = 0; index < 40; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[95];
    byte[] numArray7 = new byte[55]
    {
      (byte) 196,
      (byte) 229,
      (byte) 115,
      (byte) 45,
      (byte) 12,
      (byte) 50,
      (byte) 134,
      (byte) 50,
      (byte) 233,
      (byte) 14,
      (byte) 94,
      (byte) 103,
      byte.MaxValue,
      (byte) 177,
      (byte) 17,
      (byte) 224 /*0xE0*/,
      (byte) 158,
      (byte) 10,
      (byte) 55,
      (byte) 66,
      (byte) 251,
      (byte) 233,
      (byte) 20,
      (byte) 26,
      (byte) 40,
      (byte) 30,
      (byte) 13,
      (byte) 223,
      (byte) 158,
      (byte) 158,
      (byte) 186,
      (byte) 108,
      (byte) 245,
      (byte) 210,
      (byte) 147,
      (byte) 99,
      (byte) 98,
      (byte) 33,
      (byte) 30,
      (byte) 38,
      (byte) 71,
      (byte) 137,
      (byte) 180,
      (byte) 1,
      (byte) 162,
      (byte) 153,
      (byte) 230,
      (byte) 212,
      (byte) 93,
      (byte) 77,
      (byte) 150,
      (byte) 62,
      (byte) 55,
      (byte) 101,
      (byte) 162
    };
    byte[] numArray8 = new byte[55];
    numArray8[45] = (byte) 226;
    numArray8[14] = (byte) 95;
    numArray8[52] = (byte) 197;
    numArray8[3] = (byte) 182;
    numArray8[4] = (byte) 135;
    numArray8[33] = (byte) 239;
    numArray8[6] = (byte) 181;
    numArray8[20] = (byte) 246;
    numArray8[38] = (byte) 139;
    numArray8[11] = (byte) 182;
    numArray8[42] = (byte) 181;
    numArray8[1] = (byte) 82;
    numArray8[41] = (byte) 71;
    numArray8[26] = (byte) 39;
    numArray8[44] = (byte) 170;
    numArray8[15] = (byte) 93;
    numArray8[19] = (byte) 59;
    numArray8[17] = (byte) 229;
    numArray8[18] = (byte) 132;
    numArray8[28] = (byte) 36;
    numArray8[34] = (byte) 96 /*0x60*/;
    numArray8[21] = (byte) 120;
    numArray8[22] = (byte) 97;
    numArray8[23] = (byte) 73;
    numArray8[31 /*0x1F*/] = (byte) 21;
    numArray8[36] = (byte) 85;
    numArray8[0] = (byte) 8;
    numArray8[27] = (byte) 183;
    numArray8[35] = (byte) 147;
    numArray8[29] = (byte) 31 /*0x1F*/;
    numArray8[7] = (byte) 139;
    numArray8[12] = (byte) 3;
    numArray8[5] = (byte) 37;
    numArray8[30] = (byte) 225;
    numArray8[16 /*0x10*/] = (byte) 113;
    numArray8[48 /*0x30*/] = (byte) 95;
    numArray8[13] = (byte) 245;
    numArray8[46] = (byte) 2;
    numArray8[37] = (byte) 54;
    numArray8[24] = (byte) 35;
    numArray8[9] = (byte) 118;
    numArray8[54] = (byte) 40;
    numArray8[25] = (byte) 187;
    numArray8[32 /*0x20*/] = (byte) 202;
    numArray8[39] = (byte) 29;
    numArray8[10] = (byte) 113;
    numArray8[2] = (byte) 233;
    numArray8[47] = (byte) 82;
    numArray8[8] = (byte) 221;
    numArray8[49] = (byte) 192 /*0xC0*/;
    numArray8[50] = (byte) 115;
    numArray8[40] = (byte) 150;
    numArray8[43] = byte.MaxValue;
    numArray8[53] = (byte) 32 /*0x20*/;
    numArray8[51] = (byte) 59;
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[40]
    {
      (byte) 225,
      (byte) 118,
      (byte) 181,
      (byte) 235,
      (byte) 37,
      (byte) 67,
      (byte) 66,
      (byte) 215,
      (byte) 188,
      (byte) 227,
      (byte) 254,
      (byte) 102,
      (byte) 28,
      (byte) 117,
      (byte) 97,
      (byte) 186,
      (byte) 15,
      (byte) 90,
      (byte) 125,
      (byte) 203,
      (byte) 102,
      (byte) 198,
      (byte) 103,
      (byte) 127 /*0x7F*/,
      (byte) 131,
      (byte) 210,
      (byte) 129,
      (byte) 239,
      (byte) 41,
      (byte) 15,
      (byte) 37,
      (byte) 0,
      (byte) 18,
      (byte) 213,
      (byte) 39,
      (byte) 165,
      (byte) 123,
      (byte) 22,
      (byte) 122,
      (byte) 178
    };
    byte[] numArray10 = new byte[40]
    {
      (byte) 143,
      (byte) 103,
      (byte) 152,
      (byte) 134,
      (byte) 33,
      (byte) 119,
      (byte) 162,
      (byte) 158,
      (byte) 228,
      (byte) 73,
      (byte) 193,
      (byte) 125,
      (byte) 76,
      (byte) 244,
      (byte) 70,
      (byte) 254,
      (byte) 101,
      (byte) 52,
      (byte) 76,
      (byte) 148,
      (byte) 220,
      (byte) 78,
      (byte) 232,
      (byte) 126,
      (byte) 94,
      (byte) 163,
      (byte) 119,
      (byte) 211,
      (byte) 235,
      (byte) 172,
      (byte) 118,
      (byte) 60,
      (byte) 38,
      (byte) 12,
      (byte) 220,
      (byte) 197,
      (byte) 76,
      (byte) 244,
      (byte) 42,
      (byte) 186
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 40);
    for (int index = 0; index < 40; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_12490()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[97];
      byte[] numArray2 = new byte[55];
      numArray2[18] = (byte) 173;
      numArray2[32 /*0x20*/] = (byte) 209;
      numArray2[31 /*0x1F*/] = (byte) 144 /*0x90*/;
      numArray2[3] = (byte) 90;
      numArray2[4] = (byte) 183;
      numArray2[5] = (byte) 152;
      numArray2[51] = (byte) 212;
      numArray2[25] = (byte) 187;
      numArray2[17] = (byte) 83;
      numArray2[9] = (byte) 217;
      numArray2[1] = (byte) 79;
      numArray2[27] = (byte) 249;
      numArray2[12] = (byte) 14;
      numArray2[53] = (byte) 71;
      numArray2[14] = (byte) 103;
      numArray2[6] = (byte) 22;
      numArray2[21] = (byte) 254;
      numArray2[2] = (byte) 146;
      numArray2[33] = (byte) 35;
      numArray2[23] = (byte) 71;
      numArray2[37] = (byte) 38;
      numArray2[20] = (byte) 189;
      numArray2[36] = (byte) 64 /*0x40*/;
      numArray2[39] = (byte) 145;
      numArray2[11] = (byte) 27;
      numArray2[46] = (byte) 150;
      numArray2[26] = (byte) 62;
      numArray2[19] = (byte) 172;
      numArray2[28] = (byte) 224 /*0xE0*/;
      numArray2[29] = (byte) 203;
      numArray2[30] = (byte) 164;
      numArray2[15] = (byte) 82;
      numArray2[38] = (byte) 56;
      numArray2[42] = (byte) 77;
      numArray2[34] = (byte) 196;
      numArray2[35] = (byte) 51;
      numArray2[41] = (byte) 22;
      numArray2[22] = (byte) 83;
      numArray2[40] = (byte) 248;
      numArray2[13] = (byte) 53;
      numArray2[7] = (byte) 15;
      numArray2[24] = (byte) 78;
      numArray2[16 /*0x10*/] = (byte) 193;
      numArray2[43] = (byte) 56;
      numArray2[44] = (byte) 126;
      numArray2[45] = (byte) 11;
      numArray2[8] = (byte) 178;
      numArray2[0] = (byte) 186;
      numArray2[48 /*0x30*/] = (byte) 174;
      numArray2[10] = (byte) 86;
      numArray2[50] = (byte) 240 /*0xF0*/;
      numArray2[49] = (byte) 39;
      numArray2[52] = (byte) 40;
      numArray2[47] = (byte) 70;
      numArray2[54] = (byte) 134;
      byte[] numArray3 = new byte[55];
      numArray3[41] = (byte) 23;
      numArray3[23] = (byte) 45;
      numArray3[2] = (byte) 44;
      numArray3[3] = (byte) 221;
      numArray3[10] = (byte) 133;
      numArray3[5] = (byte) 177;
      numArray3[51] = (byte) 83;
      numArray3[1] = (byte) 195;
      numArray3[46] = (byte) 81;
      numArray3[9] = (byte) 145;
      numArray3[14] = (byte) 124;
      numArray3[29] = (byte) 79;
      numArray3[19] = (byte) 27;
      numArray3[0] = (byte) 98;
      numArray3[37] = (byte) 195;
      numArray3[15] = (byte) 112 /*0x70*/;
      numArray3[18] = (byte) 199;
      numArray3[17] = (byte) 129;
      numArray3[48 /*0x30*/] = (byte) 71;
      numArray3[28] = (byte) 227;
      numArray3[33] = (byte) 205;
      numArray3[21] = (byte) 27;
      numArray3[20] = (byte) 33;
      numArray3[12] = (byte) 69;
      numArray3[35] = (byte) 7;
      numArray3[25] = (byte) 86;
      numArray3[26] = (byte) 0;
      numArray3[50] = (byte) 106;
      numArray3[47] = (byte) 135;
      numArray3[6] = (byte) 107;
      numArray3[30] = (byte) 83;
      numArray3[13] = (byte) 213;
      numArray3[27] = (byte) 235;
      numArray3[4] = (byte) 162;
      numArray3[34] = (byte) 123;
      numArray3[53] = (byte) 208 /*0xD0*/;
      numArray3[42] = (byte) 71;
      numArray3[40] = (byte) 88;
      numArray3[38] = (byte) 74;
      numArray3[7] = (byte) 196;
      numArray3[16 /*0x10*/] = (byte) 115;
      numArray3[39] = (byte) 194;
      numArray3[22] = (byte) 181;
      numArray3[43] = (byte) 40;
      numArray3[44] = (byte) 148;
      numArray3[11] = (byte) 250;
      numArray3[54] = (byte) 105;
      numArray3[45] = (byte) 79;
      numArray3[31 /*0x1F*/] = (byte) 125;
      numArray3[49] = (byte) 35;
      numArray3[32 /*0x20*/] = (byte) 102;
      numArray3[24] = (byte) 18;
      numArray3[52] = (byte) 228;
      numArray3[8] = (byte) 231;
      numArray3[36] = (byte) 157;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[42]
      {
        (byte) 111,
        (byte) 73,
        (byte) 93,
        (byte) 62,
        (byte) 3,
        (byte) 212,
        (byte) 134,
        (byte) 77,
        (byte) 5,
        (byte) 200,
        (byte) 143,
        (byte) 108,
        (byte) 164,
        (byte) 169,
        (byte) 49,
        (byte) 121,
        (byte) 13,
        (byte) 207,
        (byte) 80 /*0x50*/,
        (byte) 240 /*0xF0*/,
        (byte) 121,
        (byte) 185,
        (byte) 125,
        (byte) 28,
        (byte) 181,
        (byte) 80 /*0x50*/,
        (byte) 171,
        (byte) 178,
        (byte) 61,
        (byte) 48 /*0x30*/,
        (byte) 58,
        (byte) 159,
        (byte) 57,
        (byte) 142,
        (byte) 3,
        (byte) 6,
        (byte) 8,
        (byte) 151,
        (byte) 5,
        (byte) 155,
        (byte) 211,
        (byte) 81
      };
      byte[] numArray5 = new byte[42]
      {
        (byte) 228,
        (byte) 196,
        (byte) 185,
        (byte) 77,
        (byte) 248,
        (byte) 91,
        (byte) 234,
        (byte) 54,
        (byte) 127 /*0x7F*/,
        (byte) 164,
        (byte) 146,
        (byte) 209,
        (byte) 142,
        (byte) 246,
        (byte) 1,
        (byte) 133,
        (byte) 21,
        (byte) 13,
        (byte) 235,
        (byte) 64 /*0x40*/,
        (byte) 137,
        (byte) 106,
        (byte) 30,
        (byte) 122,
        (byte) 89,
        (byte) 17,
        (byte) 182,
        (byte) 9,
        (byte) 232,
        (byte) 58,
        (byte) 103,
        (byte) 189,
        (byte) 154,
        (byte) 167,
        (byte) 137,
        (byte) 126,
        (byte) 156,
        (byte) 60,
        (byte) 115,
        (byte) 38,
        (byte) 112 /*0x70*/,
        (byte) 173
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 42);
      for (int index = 0; index < 42; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[97];
    byte[] numArray7 = new byte[55]
    {
      (byte) 0,
      (byte) 218,
      (byte) 78,
      (byte) 162,
      (byte) 97,
      (byte) 28,
      (byte) 236,
      (byte) 206,
      (byte) 209,
      (byte) 79,
      (byte) 182,
      (byte) 204,
      (byte) 65,
      (byte) 137,
      (byte) 179,
      (byte) 41,
      (byte) 212,
      (byte) 15,
      (byte) 212,
      (byte) 142,
      (byte) 240 /*0xF0*/,
      (byte) 51,
      (byte) 22,
      (byte) 165,
      (byte) 42,
      (byte) 112 /*0x70*/,
      (byte) 117,
      (byte) 202,
      (byte) 180,
      (byte) 29,
      (byte) 39,
      (byte) 102,
      (byte) 67,
      (byte) 78,
      (byte) 181,
      (byte) 117,
      (byte) 92,
      (byte) 15,
      (byte) 220,
      (byte) 20,
      (byte) 78,
      (byte) 208 /*0xD0*/,
      (byte) 103,
      (byte) 62,
      (byte) 77,
      (byte) 57,
      (byte) 199,
      (byte) 14,
      (byte) 81,
      (byte) 114,
      (byte) 254,
      (byte) 65,
      (byte) 126,
      (byte) 171,
      (byte) 221
    };
    byte[] numArray8 = new byte[55]
    {
      (byte) 195,
      (byte) 121,
      (byte) 51,
      (byte) 146,
      (byte) 188,
      (byte) 65,
      byte.MaxValue,
      (byte) 192 /*0xC0*/,
      (byte) 67,
      (byte) 74,
      byte.MaxValue,
      (byte) 30,
      (byte) 248,
      (byte) 100,
      (byte) 92,
      (byte) 180,
      (byte) 171,
      (byte) 77,
      (byte) 95,
      (byte) 85,
      (byte) 243,
      (byte) 177,
      (byte) 103,
      (byte) 225,
      (byte) 7,
      (byte) 92,
      (byte) 100,
      (byte) 206,
      (byte) 206,
      (byte) 32 /*0x20*/,
      (byte) 237,
      (byte) 245,
      (byte) 65,
      (byte) 18,
      (byte) 187,
      (byte) 40,
      (byte) 202,
      (byte) 156,
      (byte) 109,
      (byte) 47,
      (byte) 185,
      (byte) 23,
      (byte) 103,
      (byte) 7,
      (byte) 190,
      (byte) 161,
      (byte) 203,
      byte.MaxValue,
      (byte) 84,
      (byte) 76,
      (byte) 243,
      (byte) 190,
      (byte) 240 /*0xF0*/,
      (byte) 8,
      (byte) 207
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[42]
    {
      (byte) 168,
      (byte) 119,
      (byte) 240 /*0xF0*/,
      (byte) 167,
      (byte) 33,
      (byte) 26,
      (byte) 180,
      (byte) 40,
      (byte) 79,
      (byte) 137,
      (byte) 242,
      (byte) 211,
      (byte) 61,
      (byte) 204,
      (byte) 196,
      (byte) 51,
      (byte) 191,
      (byte) 63 /*0x3F*/,
      (byte) 31 /*0x1F*/,
      (byte) 203,
      (byte) 215,
      (byte) 88,
      (byte) 240 /*0xF0*/,
      (byte) 169,
      (byte) 102,
      (byte) 120,
      (byte) 65,
      (byte) 30,
      (byte) 229,
      (byte) 231,
      (byte) 151,
      (byte) 113,
      (byte) 232,
      (byte) 177,
      (byte) 180,
      (byte) 152,
      (byte) 235,
      (byte) 109,
      (byte) 44,
      (byte) 182,
      (byte) 178,
      (byte) 22
    };
    byte[] numArray10 = new byte[42];
    numArray10[1] = (byte) 42;
    numArray10[38] = (byte) 100;
    numArray10[2] = (byte) 23;
    numArray10[11] = (byte) 42;
    numArray10[4] = (byte) 131;
    numArray10[35] = (byte) 157;
    numArray10[13] = (byte) 89;
    numArray10[14] = (byte) 241;
    numArray10[41] = (byte) 240 /*0xF0*/;
    numArray10[9] = (byte) 21;
    numArray10[10] = (byte) 83;
    numArray10[27] = (byte) 71;
    numArray10[12] = (byte) 119;
    numArray10[30] = (byte) 154;
    numArray10[0] = (byte) 23;
    numArray10[34] = (byte) 77;
    numArray10[23] = (byte) 2;
    numArray10[17] = (byte) 7;
    numArray10[7] = (byte) 157;
    numArray10[19] = (byte) 237;
    numArray10[20] = (byte) 141;
    numArray10[15] = (byte) 143;
    numArray10[22] = (byte) 197;
    numArray10[18] = (byte) 229;
    numArray10[24] = (byte) 99;
    numArray10[39] = (byte) 6;
    numArray10[6] = (byte) 133;
    numArray10[31 /*0x1F*/] = (byte) 31 /*0x1F*/;
    numArray10[21] = (byte) 44;
    numArray10[29] = (byte) 214;
    numArray10[26] = (byte) 193;
    numArray10[37] = (byte) 220;
    numArray10[32 /*0x20*/] = (byte) 110;
    numArray10[33] = (byte) 89;
    numArray10[28] = (byte) 99;
    numArray10[5] = (byte) 32 /*0x20*/;
    numArray10[36] = (byte) 37;
    numArray10[16 /*0x10*/] = (byte) 175;
    numArray10[8] = (byte) 92;
    numArray10[25] = (byte) 45;
    numArray10[40] = (byte) 97;
    numArray10[3] = (byte) 229;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 42);
    for (int index = 0; index < 42; ++index)
      numArray6[index + 55] ^= numArray10[index];
    byte[] numArray11 = new byte[41];
    byte[] response = new byte[41];
    Array.Copy((Array) sc_12465.sspq, 72, (Array) numArray11, 0, 41);
    key.Query(true, 335, numArray11, response);
    Array.Copy((Array) sc_12465.sspr, 72, (Array) numArray11, 0, 41);
    for (int index = 0; index < numArray11.Length; ++index)
    {
      if ((int) numArray11[index] != (int) response[index])
      {
        key.TagValue = (int) response[index];
        break;
      }
    }
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_12491()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[96 /*0x60*/];
      byte[] numArray2 = new byte[55];
      numArray2[8] = (byte) 17;
      numArray2[12] = (byte) 253;
      numArray2[53] = (byte) 232;
      numArray2[3] = (byte) 129;
      numArray2[4] = (byte) 220;
      numArray2[47] = (byte) 43;
      numArray2[6] = (byte) 13;
      numArray2[9] = (byte) 187;
      numArray2[5] = (byte) 218;
      numArray2[2] = (byte) 129;
      numArray2[10] = (byte) 210;
      numArray2[40] = (byte) 47;
      numArray2[22] = (byte) 85;
      numArray2[27] = (byte) 219;
      numArray2[15] = (byte) 202;
      numArray2[19] = (byte) 161;
      numArray2[26] = (byte) 195;
      numArray2[17] = (byte) 24;
      numArray2[1] = (byte) 197;
      numArray2[36] = (byte) 197;
      numArray2[20] = (byte) 106;
      numArray2[21] = (byte) 103;
      numArray2[24] = (byte) 133;
      numArray2[23] = (byte) 248;
      numArray2[46] = (byte) 6;
      numArray2[25] = (byte) 74;
      numArray2[45] = (byte) 109;
      numArray2[16 /*0x10*/] = (byte) 52;
      numArray2[28] = (byte) 242;
      numArray2[54] = (byte) 145;
      numArray2[32 /*0x20*/] = (byte) 201;
      numArray2[31 /*0x1F*/] = (byte) 201;
      numArray2[38] = (byte) 56;
      numArray2[33] = (byte) 236;
      numArray2[30] = (byte) 71;
      numArray2[35] = (byte) 47;
      numArray2[51] = (byte) 13;
      numArray2[11] = (byte) 95;
      numArray2[0] = (byte) 238;
      numArray2[39] = (byte) 107;
      numArray2[14] = (byte) 111;
      numArray2[41] = (byte) 219;
      numArray2[42] = (byte) 90;
      numArray2[43] = (byte) 132;
      numArray2[44] = (byte) 243;
      numArray2[37] = (byte) 239;
      numArray2[13] = (byte) 154;
      numArray2[50] = (byte) 110;
      numArray2[48 /*0x30*/] = (byte) 67;
      numArray2[49] = (byte) 79;
      numArray2[29] = (byte) 35;
      numArray2[18] = (byte) 62;
      numArray2[52] = (byte) 80 /*0x50*/;
      numArray2[7] = (byte) 214;
      numArray2[34] = (byte) 61;
      byte[] numArray3 = new byte[55]
      {
        (byte) 127 /*0x7F*/,
        (byte) 58,
        (byte) 183,
        (byte) 18,
        (byte) 61,
        (byte) 18,
        (byte) 164,
        (byte) 241,
        (byte) 44,
        (byte) 27,
        (byte) 10,
        (byte) 130,
        (byte) 238,
        (byte) 96 /*0x60*/,
        (byte) 139,
        (byte) 8,
        (byte) 65,
        (byte) 244,
        (byte) 234,
        (byte) 68,
        (byte) 248,
        (byte) 178,
        (byte) 234,
        (byte) 17,
        (byte) 79,
        (byte) 20,
        (byte) 130,
        (byte) 184,
        (byte) 169,
        (byte) 227,
        (byte) 176 /*0xB0*/,
        (byte) 190,
        (byte) 67,
        (byte) 76,
        (byte) 211,
        (byte) 241,
        (byte) 144 /*0x90*/,
        (byte) 41,
        (byte) 199,
        (byte) 158,
        (byte) 29,
        (byte) 70,
        (byte) 5,
        (byte) 253,
        (byte) 135,
        (byte) 6,
        (byte) 90,
        (byte) 56,
        (byte) 202,
        (byte) 224 /*0xE0*/,
        (byte) 51,
        (byte) 31 /*0x1F*/,
        (byte) 146,
        (byte) 95,
        (byte) 94
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[41]
      {
        (byte) 130,
        (byte) 228,
        (byte) 77,
        (byte) 126,
        (byte) 154,
        (byte) 64 /*0x40*/,
        (byte) 56,
        (byte) 57,
        (byte) 143,
        (byte) 181,
        (byte) 181,
        (byte) 166,
        (byte) 129,
        (byte) 200,
        (byte) 81,
        (byte) 56,
        (byte) 127 /*0x7F*/,
        (byte) 116,
        (byte) 151,
        (byte) 159,
        (byte) 192 /*0xC0*/,
        (byte) 26,
        (byte) 63 /*0x3F*/,
        (byte) 77,
        (byte) 201,
        (byte) 148,
        (byte) 88,
        (byte) 138,
        (byte) 12,
        (byte) 251,
        (byte) 106,
        (byte) 89,
        (byte) 60,
        (byte) 31 /*0x1F*/,
        (byte) 186,
        (byte) 14,
        (byte) 89,
        (byte) 213,
        (byte) 99,
        (byte) 183,
        (byte) 52
      };
      byte[] numArray5 = new byte[41]
      {
        (byte) 198,
        (byte) 47,
        (byte) 169,
        (byte) 197,
        (byte) 247,
        (byte) 88,
        (byte) 189,
        (byte) 143,
        (byte) 254,
        (byte) 231,
        (byte) 135,
        (byte) 52,
        (byte) 80 /*0x50*/,
        (byte) 224 /*0xE0*/,
        (byte) 188,
        (byte) 177,
        (byte) 177,
        (byte) 241,
        (byte) 22,
        (byte) 196,
        (byte) 25,
        (byte) 161,
        (byte) 237,
        (byte) 148,
        (byte) 84,
        (byte) 146,
        (byte) 111,
        (byte) 225,
        (byte) 191,
        (byte) 130,
        (byte) 170,
        (byte) 37,
        (byte) 130,
        (byte) 22,
        (byte) 252,
        (byte) 2,
        (byte) 136,
        (byte) 37,
        (byte) 64 /*0x40*/,
        (byte) 28,
        (byte) 237
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 41);
      for (int index = 0; index < 41; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[96 /*0x60*/];
    byte[] numArray7 = new byte[55]
    {
      (byte) 64 /*0x40*/,
      (byte) 39,
      (byte) 129,
      (byte) 129,
      (byte) 216,
      (byte) 245,
      (byte) 254,
      (byte) 1,
      (byte) 106,
      (byte) 218,
      (byte) 223,
      (byte) 103,
      (byte) 164,
      (byte) 0,
      (byte) 181,
      (byte) 5,
      (byte) 232,
      (byte) 173,
      (byte) 44,
      (byte) 147,
      (byte) 75,
      (byte) 92,
      (byte) 106,
      (byte) 98,
      (byte) 14,
      (byte) 66,
      (byte) 222,
      (byte) 169,
      (byte) 17,
      (byte) 224 /*0xE0*/,
      (byte) 106,
      (byte) 232,
      (byte) 191,
      (byte) 29,
      (byte) 13,
      (byte) 162,
      (byte) 19,
      (byte) 214,
      (byte) 178,
      (byte) 23,
      (byte) 253,
      (byte) 104,
      (byte) 238,
      (byte) 97,
      (byte) 64 /*0x40*/,
      (byte) 82,
      (byte) 105,
      (byte) 89,
      (byte) 31 /*0x1F*/,
      (byte) 93,
      (byte) 206,
      (byte) 134,
      (byte) 171,
      (byte) 200,
      (byte) 214
    };
    byte[] numArray8 = new byte[55];
    numArray8[26] = (byte) 144 /*0x90*/;
    numArray8[1] = (byte) 228;
    numArray8[35] = (byte) 161;
    numArray8[20] = (byte) 170;
    numArray8[52] = (byte) 238;
    numArray8[17] = (byte) 3;
    numArray8[37] = (byte) 47;
    numArray8[34] = (byte) 171;
    numArray8[8] = (byte) 113;
    numArray8[9] = (byte) 141;
    numArray8[42] = (byte) 117;
    numArray8[12] = (byte) 131;
    numArray8[53] = (byte) 177;
    numArray8[13] = (byte) 238;
    numArray8[6] = (byte) 177;
    numArray8[15] = (byte) 147;
    numArray8[4] = (byte) 99;
    numArray8[3] = (byte) 152;
    numArray8[18] = (byte) 220;
    numArray8[49] = (byte) 198;
    numArray8[14] = (byte) 67;
    numArray8[0] = (byte) 139;
    numArray8[22] = (byte) 62;
    numArray8[25] = (byte) 231;
    numArray8[24] = byte.MaxValue;
    numArray8[54] = (byte) 252;
    numArray8[44] = (byte) 237;
    numArray8[38] = (byte) 0;
    numArray8[28] = (byte) 110;
    numArray8[29] = (byte) 114;
    numArray8[30] = (byte) 9;
    numArray8[41] = (byte) 55;
    numArray8[27] = (byte) 41;
    numArray8[33] = (byte) 116;
    numArray8[2] = (byte) 188;
    numArray8[31 /*0x1F*/] = (byte) 19;
    numArray8[36] = (byte) 155;
    numArray8[51] = (byte) 167;
    numArray8[32 /*0x20*/] = (byte) 77;
    numArray8[39] = (byte) 178;
    numArray8[43] = (byte) 39;
    numArray8[21] = (byte) 156;
    numArray8[11] = (byte) 184;
    numArray8[16 /*0x10*/] = (byte) 4;
    numArray8[5] = (byte) 43;
    numArray8[45] = (byte) 127 /*0x7F*/;
    numArray8[46] = (byte) 231;
    numArray8[47] = (byte) 159;
    numArray8[48 /*0x30*/] = (byte) 104;
    numArray8[19] = (byte) 122;
    numArray8[50] = (byte) 7;
    numArray8[7] = (byte) 229;
    numArray8[40] = (byte) 119;
    numArray8[10] = (byte) 135;
    numArray8[23] = (byte) 49;
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[41];
    numArray9[17] = (byte) 236;
    numArray9[13] = (byte) 253;
    numArray9[8] = (byte) 184;
    numArray9[3] = (byte) 145;
    numArray9[23] = (byte) 246;
    numArray9[1] = (byte) 184;
    numArray9[22] = (byte) 18;
    numArray9[2] = (byte) 188;
    numArray9[20] = (byte) 23;
    numArray9[21] = (byte) 216;
    numArray9[10] = (byte) 64 /*0x40*/;
    numArray9[9] = (byte) 177;
    numArray9[32 /*0x20*/] = (byte) 177;
    numArray9[5] = (byte) 60;
    numArray9[14] = (byte) 174;
    numArray9[15] = (byte) 222;
    numArray9[16 /*0x10*/] = (byte) 21;
    numArray9[29] = (byte) 168;
    numArray9[6] = (byte) 120;
    numArray9[19] = (byte) 8;
    numArray9[11] = (byte) 202;
    numArray9[25] = (byte) 125;
    numArray9[12] = (byte) 45;
    numArray9[7] = (byte) 139;
    numArray9[28] = (byte) 89;
    numArray9[24] = (byte) 31 /*0x1F*/;
    numArray9[26] = (byte) 72;
    numArray9[27] = (byte) 83;
    numArray9[0] = (byte) 252;
    numArray9[37] = (byte) 111;
    numArray9[30] = (byte) 197;
    numArray9[31 /*0x1F*/] = (byte) 82;
    numArray9[18] = (byte) 105;
    numArray9[33] = (byte) 50;
    numArray9[34] = (byte) 248;
    numArray9[35] = (byte) 171;
    numArray9[36] = (byte) 96 /*0x60*/;
    numArray9[39] = (byte) 60;
    numArray9[38] = (byte) 223;
    numArray9[4] = (byte) 202;
    numArray9[40] = (byte) 236;
    byte[] numArray10 = new byte[41];
    numArray10[13] = (byte) 141;
    numArray10[1] = (byte) 0;
    numArray10[2] = (byte) 34;
    numArray10[3] = (byte) 225;
    numArray10[15] = (byte) 107;
    numArray10[28] = (byte) 85;
    numArray10[6] = (byte) 229;
    numArray10[18] = (byte) 220;
    numArray10[8] = (byte) 4;
    numArray10[9] = (byte) 92;
    numArray10[24] = (byte) 203;
    numArray10[4] = (byte) 196;
    numArray10[12] = (byte) 180;
    numArray10[11] = (byte) 94;
    numArray10[36] = (byte) 166;
    numArray10[25] = (byte) 37;
    numArray10[21] = (byte) 169;
    numArray10[31 /*0x1F*/] = (byte) 64 /*0x40*/;
    numArray10[17] = (byte) 72;
    numArray10[19] = (byte) 57;
    numArray10[10] = (byte) 204;
    numArray10[39] = (byte) 219;
    numArray10[22] = (byte) 118;
    numArray10[16 /*0x10*/] = (byte) 122;
    numArray10[0] = (byte) 34;
    numArray10[30] = (byte) 23;
    numArray10[5] = (byte) 150;
    numArray10[27] = (byte) 81;
    numArray10[26] = (byte) 20;
    numArray10[20] = (byte) 171;
    numArray10[23] = (byte) 172;
    numArray10[7] = (byte) 71;
    numArray10[32 /*0x20*/] = (byte) 38;
    numArray10[33] = (byte) 37;
    numArray10[34] = (byte) 166;
    numArray10[35] = (byte) 187;
    numArray10[14] = (byte) 129;
    numArray10[37] = (byte) 37;
    numArray10[38] = (byte) 144 /*0x90*/;
    numArray10[29] = (byte) 1;
    numArray10[40] = (byte) 0;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 41);
    for (int index = 0; index < 41; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_12492()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[94];
      byte[] numArray2 = new byte[55]
      {
        (byte) 123,
        (byte) 228,
        (byte) 83,
        (byte) 126,
        (byte) 198,
        (byte) 14,
        (byte) 159,
        (byte) 0,
        (byte) 185,
        (byte) 111,
        (byte) 56,
        (byte) 126,
        (byte) 77,
        (byte) 72,
        (byte) 107,
        (byte) 238,
        (byte) 173,
        (byte) 248,
        (byte) 121,
        (byte) 41,
        (byte) 15,
        (byte) 226,
        (byte) 46,
        (byte) 232,
        (byte) 174,
        (byte) 152,
        (byte) 159,
        (byte) 143,
        (byte) 172,
        (byte) 127 /*0x7F*/,
        (byte) 58,
        (byte) 14,
        (byte) 75,
        (byte) 50,
        (byte) 187,
        (byte) 120,
        (byte) 0,
        (byte) 106,
        (byte) 22,
        (byte) 125,
        (byte) 209,
        (byte) 237,
        (byte) 246,
        (byte) 170,
        (byte) 167,
        (byte) 7,
        (byte) 155,
        (byte) 35,
        (byte) 185,
        (byte) 231,
        (byte) 183,
        (byte) 55,
        (byte) 132,
        (byte) 186,
        (byte) 152
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 138,
        (byte) 173,
        (byte) 201,
        (byte) 12,
        (byte) 78,
        (byte) 118,
        (byte) 111,
        (byte) 212,
        (byte) 102,
        (byte) 160 /*0xA0*/,
        (byte) 207,
        (byte) 253,
        (byte) 198,
        (byte) 210,
        (byte) 68,
        (byte) 3,
        (byte) 122,
        (byte) 16 /*0x10*/,
        (byte) 138,
        (byte) 89,
        (byte) 74,
        (byte) 22,
        (byte) 137,
        (byte) 23,
        (byte) 222,
        (byte) 227,
        (byte) 214,
        (byte) 95,
        (byte) 121,
        (byte) 117,
        (byte) 35,
        (byte) 7,
        (byte) 6,
        (byte) 251,
        (byte) 222,
        (byte) 210,
        (byte) 160 /*0xA0*/,
        (byte) 41,
        (byte) 254,
        (byte) 167,
        (byte) 176 /*0xB0*/,
        (byte) 211,
        (byte) 133,
        (byte) 148,
        (byte) 4,
        (byte) 86,
        (byte) 31 /*0x1F*/,
        (byte) 23,
        (byte) 131,
        (byte) 6,
        (byte) 130,
        (byte) 132,
        (byte) 12,
        (byte) 206,
        (byte) 66
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[39]
      {
        (byte) 107,
        (byte) 208 /*0xD0*/,
        (byte) 46,
        (byte) 111,
        (byte) 16 /*0x10*/,
        (byte) 84,
        (byte) 57,
        (byte) 53,
        (byte) 41,
        (byte) 51,
        (byte) 27,
        (byte) 140,
        (byte) 243,
        (byte) 238,
        (byte) 66,
        (byte) 226,
        (byte) 251,
        (byte) 87,
        (byte) 34,
        (byte) 236,
        (byte) 131,
        (byte) 64 /*0x40*/,
        (byte) 166,
        (byte) 252,
        (byte) 123,
        (byte) 213,
        (byte) 196,
        (byte) 199,
        (byte) 3,
        (byte) 137,
        (byte) 144 /*0x90*/,
        (byte) 153,
        (byte) 34,
        (byte) 193,
        (byte) 149,
        (byte) 154,
        (byte) 232,
        (byte) 14,
        (byte) 192 /*0xC0*/
      };
      byte[] numArray5 = new byte[39]
      {
        (byte) 111,
        (byte) 233,
        (byte) 34,
        (byte) 121,
        (byte) 133,
        (byte) 107,
        (byte) 196,
        (byte) 219,
        (byte) 205,
        (byte) 17,
        (byte) 14,
        (byte) 206,
        (byte) 212,
        (byte) 148,
        (byte) 3,
        (byte) 139,
        (byte) 145,
        (byte) 201,
        (byte) 171,
        (byte) 171,
        (byte) 249,
        (byte) 221,
        (byte) 73,
        (byte) 234,
        (byte) 52,
        (byte) 187,
        (byte) 20,
        (byte) 89,
        (byte) 249,
        (byte) 8,
        (byte) 118,
        (byte) 5,
        (byte) 165,
        (byte) 181,
        (byte) 174,
        (byte) 220,
        (byte) 209,
        (byte) 203,
        (byte) 137
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 39);
      for (int index = 0; index < 39; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[94];
    byte[] numArray7 = new byte[55]
    {
      (byte) 125,
      (byte) 212,
      (byte) 128 /*0x80*/,
      (byte) 150,
      (byte) 197,
      (byte) 249,
      (byte) 117,
      (byte) 85,
      (byte) 137,
      (byte) 98,
      (byte) 117,
      (byte) 172,
      (byte) 50,
      (byte) 42,
      (byte) 41,
      (byte) 49,
      (byte) 243,
      (byte) 58,
      (byte) 143,
      (byte) 114,
      (byte) 49,
      (byte) 160 /*0xA0*/,
      (byte) 162,
      (byte) 129,
      (byte) 106,
      (byte) 49,
      (byte) 89,
      (byte) 167,
      (byte) 249,
      (byte) 237,
      (byte) 150,
      (byte) 137,
      (byte) 48 /*0x30*/,
      (byte) 14,
      (byte) 194,
      (byte) 210,
      (byte) 124,
      (byte) 228,
      (byte) 16 /*0x10*/,
      (byte) 203,
      (byte) 82,
      (byte) 174,
      (byte) 0,
      (byte) 46,
      (byte) 36,
      (byte) 117,
      (byte) 143,
      (byte) 106,
      (byte) 112 /*0x70*/,
      (byte) 182,
      (byte) 26,
      (byte) 133,
      (byte) 9,
      (byte) 74,
      (byte) 67
    };
    byte[] numArray8 = new byte[55]
    {
      (byte) 236,
      (byte) 3,
      (byte) 27,
      (byte) 35,
      (byte) 14,
      (byte) 39,
      (byte) 34,
      (byte) 106,
      (byte) 173,
      (byte) 98,
      (byte) 75,
      (byte) 251,
      (byte) 23,
      (byte) 190,
      (byte) 2,
      (byte) 114,
      (byte) 90,
      (byte) 157,
      (byte) 7,
      (byte) 219,
      (byte) 154,
      (byte) 110,
      (byte) 26,
      (byte) 21,
      (byte) 67,
      (byte) 59,
      (byte) 200,
      (byte) 216,
      (byte) 165,
      (byte) 49,
      (byte) 92,
      (byte) 146,
      (byte) 26,
      (byte) 157,
      (byte) 216,
      (byte) 196,
      (byte) 29,
      (byte) 147,
      (byte) 55,
      (byte) 224 /*0xE0*/,
      (byte) 240 /*0xF0*/,
      (byte) 96 /*0x60*/,
      (byte) 144 /*0x90*/,
      (byte) 161,
      (byte) 157,
      (byte) 202,
      (byte) 235,
      (byte) 213,
      (byte) 134,
      (byte) 26,
      (byte) 106,
      (byte) 48 /*0x30*/,
      (byte) 163,
      (byte) 18,
      (byte) 133
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[39]
    {
      (byte) 254,
      (byte) 44,
      (byte) 147,
      (byte) 160 /*0xA0*/,
      (byte) 205,
      (byte) 91,
      (byte) 155,
      (byte) 204,
      (byte) 236,
      (byte) 124,
      (byte) 36,
      (byte) 103,
      (byte) 86,
      (byte) 180,
      (byte) 15,
      (byte) 42,
      (byte) 196,
      (byte) 31 /*0x1F*/,
      (byte) 131,
      (byte) 159,
      (byte) 115,
      (byte) 93,
      (byte) 245,
      (byte) 162,
      (byte) 238,
      (byte) 55,
      (byte) 67,
      (byte) 210,
      (byte) 16 /*0x10*/,
      (byte) 127 /*0x7F*/,
      (byte) 229,
      (byte) 202,
      (byte) 229,
      (byte) 160 /*0xA0*/,
      (byte) 125,
      (byte) 183,
      (byte) 124,
      (byte) 42,
      (byte) 132
    };
    byte[] numArray10 = new byte[39]
    {
      (byte) 184,
      (byte) 182,
      (byte) 234,
      (byte) 96 /*0x60*/,
      (byte) 159,
      (byte) 25,
      (byte) 220,
      (byte) 95,
      (byte) 251,
      (byte) 8,
      (byte) 254,
      (byte) 132,
      (byte) 37,
      (byte) 112 /*0x70*/,
      (byte) 214,
      (byte) 213,
      (byte) 248,
      (byte) 239,
      (byte) 212,
      (byte) 188,
      (byte) 11,
      (byte) 101,
      (byte) 122,
      (byte) 12,
      (byte) 62,
      (byte) 136,
      (byte) 200,
      (byte) 109,
      (byte) 208 /*0xD0*/,
      (byte) 44,
      (byte) 60,
      (byte) 101,
      (byte) 85,
      (byte) 153,
      (byte) 30,
      (byte) 33,
      (byte) 232,
      (byte) 23,
      (byte) 215
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 39);
    for (int index = 0; index < 39; ++index)
      numArray6[index + 55] ^= numArray10[index];
    byte[] numArray11 = new byte[12];
    byte[] response = new byte[12];
    Array.Copy((Array) sc_12465.sspq, 113, (Array) numArray11, 0, 12);
    key.Query(true, 335, numArray11, response);
    Array.Copy((Array) sc_12465.sspr, 113, (Array) numArray11, 0, 12);
    for (int index = 0; index < numArray11.Length; ++index)
    {
      if ((int) numArray11[index] != (int) response[index])
      {
        key.TagValue = (int) response[index];
        break;
      }
    }
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static int ssp_appserver_12493(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 241,
      (byte) 177,
      (byte) 150,
      (byte) 24,
      (byte) 201,
      (byte) 187,
      (byte) 108,
      (byte) 150,
      (byte) 90,
      (byte) 130,
      (byte) 182,
      (byte) 55,
      (byte) 87,
      (byte) 174,
      (byte) 153,
      (byte) 154,
      (byte) 48 /*0x30*/,
      (byte) 59,
      (byte) 18,
      (byte) 37,
      (byte) 252,
      (byte) 55,
      (byte) 235,
      (byte) 29,
      (byte) 65,
      (byte) 200,
      (byte) 181,
      (byte) 58,
      (byte) 187,
      (byte) 10,
      (byte) 167,
      (byte) 226,
      (byte) 220,
      (byte) 128 /*0x80*/,
      (byte) 138,
      (byte) 121,
      (byte) 238,
      (byte) 58,
      (byte) 155,
      (byte) 166,
      (byte) 203,
      (byte) 97,
      (byte) 16 /*0x10*/,
      (byte) 128 /*0x80*/,
      (byte) 99,
      (byte) 202,
      (byte) 178,
      (byte) 172
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[23] = (byte) 12;
    sourceArray2[1] = (byte) 56;
    sourceArray2[2] = (byte) 131;
    sourceArray2[3] = (byte) 2;
    sourceArray2[16 /*0x10*/] = (byte) 28;
    sourceArray2[5] = (byte) 39;
    sourceArray2[6] = (byte) 133;
    sourceArray2[19] = (byte) 77;
    sourceArray2[15] = (byte) 6;
    sourceArray2[9] = (byte) 216;
    sourceArray2[10] = (byte) 170;
    sourceArray2[11] = (byte) 1;
    sourceArray2[17] = (byte) 180;
    sourceArray2[35] = (byte) 216;
    sourceArray2[33] = (byte) 212;
    sourceArray2[29] = (byte) 132;
    sourceArray2[36] = (byte) 230;
    sourceArray2[41] = (byte) 23;
    sourceArray2[18] = (byte) 5;
    sourceArray2[47] = (byte) 38;
    sourceArray2[20] = (byte) 34;
    sourceArray2[21] = (byte) 190;
    sourceArray2[40] = (byte) 166;
    sourceArray2[24] = (byte) 54;
    sourceArray2[38] = (byte) 139;
    sourceArray2[14] = (byte) 38;
    sourceArray2[26] = (byte) 156;
    sourceArray2[27] = (byte) 238;
    sourceArray2[28] = (byte) 173;
    sourceArray2[34] = (byte) 225;
    sourceArray2[30] = (byte) 17;
    sourceArray2[31 /*0x1F*/] = (byte) 31 /*0x1F*/;
    sourceArray2[7] = (byte) 25;
    sourceArray2[32 /*0x20*/] = (byte) 55;
    sourceArray2[44] = (byte) 171;
    sourceArray2[8] = (byte) 149;
    sourceArray2[42] = (byte) 200;
    sourceArray2[43] = (byte) 224 /*0xE0*/;
    sourceArray2[4] = (byte) 83;
    sourceArray2[39] = (byte) 195;
    sourceArray2[0] = (byte) 226;
    sourceArray2[37] = (byte) 138;
    sourceArray2[12] = (byte) 186;
    sourceArray2[25] = (byte) 100;
    sourceArray2[13] = (byte) 71;
    sourceArray2[45] = (byte) 197;
    sourceArray2[46] = (byte) 137;
    sourceArray2[22] = (byte) 7;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[26];
    byte[] response2 = new byte[26];
    Array.Copy((Array) sc_12465.sspq, 125, (Array) numArray2, 0, 26);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_12465.sspr, 125, (Array) numArray2, 0, 26);
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

  internal static int ssp_appserver_12494(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 204,
      (byte) 191,
      (byte) 20,
      (byte) 101,
      (byte) 196,
      (byte) 40,
      (byte) 7,
      (byte) 57,
      (byte) 127 /*0x7F*/,
      (byte) 1,
      (byte) 71,
      (byte) 224 /*0xE0*/,
      (byte) 66,
      (byte) 245,
      (byte) 62,
      (byte) 165,
      (byte) 60,
      (byte) 86,
      (byte) 182,
      (byte) 3,
      (byte) 175,
      (byte) 161,
      (byte) 245,
      (byte) 214,
      (byte) 234,
      (byte) 112 /*0x70*/,
      (byte) 242,
      (byte) 117,
      (byte) 74,
      (byte) 53,
      (byte) 30,
      (byte) 173,
      (byte) 8,
      (byte) 93,
      (byte) 116,
      (byte) 98,
      (byte) 140,
      (byte) 46,
      (byte) 150,
      (byte) 150,
      (byte) 89,
      (byte) 177,
      (byte) 127 /*0x7F*/,
      (byte) 162,
      (byte) 104,
      (byte) 245,
      (byte) 242,
      (byte) 167
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[21] = (byte) 247;
    sourceArray2[42] = (byte) 117;
    sourceArray2[2] = (byte) 37;
    sourceArray2[28] = (byte) 88;
    sourceArray2[46] = (byte) 242;
    sourceArray2[5] = (byte) 218;
    sourceArray2[11] = (byte) 67;
    sourceArray2[16 /*0x10*/] = (byte) 62;
    sourceArray2[8] = (byte) 69;
    sourceArray2[41] = (byte) 155;
    sourceArray2[10] = (byte) 37;
    sourceArray2[31 /*0x1F*/] = (byte) 75;
    sourceArray2[25] = (byte) 212;
    sourceArray2[6] = (byte) 135;
    sourceArray2[14] = (byte) 79;
    sourceArray2[15] = (byte) 232;
    sourceArray2[32 /*0x20*/] = (byte) 254;
    sourceArray2[27] = (byte) 64 /*0x40*/;
    sourceArray2[20] = (byte) 35;
    sourceArray2[19] = (byte) 243;
    sourceArray2[35] = (byte) 228;
    sourceArray2[38] = (byte) 80 /*0x50*/;
    sourceArray2[9] = (byte) 222;
    sourceArray2[13] = (byte) 61;
    sourceArray2[24] = (byte) 108;
    sourceArray2[29] = (byte) 250;
    sourceArray2[26] = (byte) 179;
    sourceArray2[7] = (byte) 39;
    sourceArray2[1] = (byte) 160 /*0xA0*/;
    sourceArray2[3] = (byte) 91;
    sourceArray2[0] = (byte) 193;
    sourceArray2[36] = (byte) 74;
    sourceArray2[23] = (byte) 213;
    sourceArray2[33] = (byte) 231;
    sourceArray2[4] = (byte) 223;
    sourceArray2[17] = (byte) 81;
    sourceArray2[30] = (byte) 246;
    sourceArray2[37] = (byte) 179;
    sourceArray2[34] = (byte) 166;
    sourceArray2[39] = (byte) 167;
    sourceArray2[22] = (byte) 105;
    sourceArray2[18] = (byte) 3;
    sourceArray2[40] = (byte) 11;
    sourceArray2[43] = (byte) 76;
    sourceArray2[44] = (byte) 82;
    sourceArray2[45] = (byte) 73;
    sourceArray2[12] = (byte) 56;
    sourceArray2[47] = (byte) 205;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_12495()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[105];
      byte[] numArray2 = new byte[55]
      {
        (byte) 29,
        (byte) 13,
        (byte) 38,
        (byte) 224 /*0xE0*/,
        (byte) 210,
        (byte) 8,
        (byte) 86,
        (byte) 96 /*0x60*/,
        (byte) 228,
        (byte) 88,
        (byte) 8,
        (byte) 118,
        (byte) 207,
        (byte) 133,
        (byte) 215,
        (byte) 234,
        (byte) 216,
        (byte) 144 /*0x90*/,
        (byte) 42,
        (byte) 220,
        (byte) 107,
        (byte) 94,
        (byte) 222,
        (byte) 56,
        (byte) 66,
        (byte) 202,
        (byte) 90,
        (byte) 125,
        (byte) 190,
        (byte) 226,
        (byte) 58,
        (byte) 236,
        (byte) 149,
        (byte) 159,
        (byte) 216,
        (byte) 112 /*0x70*/,
        (byte) 37,
        (byte) 216,
        (byte) 140,
        (byte) 22,
        (byte) 177,
        (byte) 205,
        (byte) 149,
        (byte) 174,
        (byte) 65,
        (byte) 34,
        (byte) 127 /*0x7F*/,
        (byte) 46,
        (byte) 107,
        (byte) 117,
        (byte) 238,
        (byte) 50,
        (byte) 12,
        (byte) 226,
        (byte) 98
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 149,
        (byte) 189,
        (byte) 203,
        (byte) 68,
        (byte) 35,
        (byte) 238,
        (byte) 166,
        (byte) 181,
        (byte) 152,
        (byte) 71,
        (byte) 235,
        (byte) 76,
        (byte) 71,
        (byte) 108,
        (byte) 52,
        (byte) 135,
        (byte) 84,
        (byte) 100,
        (byte) 9,
        (byte) 223,
        (byte) 171,
        (byte) 243,
        (byte) 52,
        (byte) 212,
        (byte) 81,
        (byte) 52,
        (byte) 128 /*0x80*/,
        (byte) 73,
        (byte) 0,
        (byte) 201,
        (byte) 223,
        (byte) 241,
        (byte) 149,
        (byte) 33,
        (byte) 97,
        (byte) 181,
        (byte) 212,
        (byte) 65,
        (byte) 53,
        (byte) 71,
        (byte) 109,
        (byte) 35,
        (byte) 178,
        (byte) 109,
        (byte) 154,
        (byte) 64 /*0x40*/,
        (byte) 210,
        (byte) 139,
        (byte) 137,
        (byte) 160 /*0xA0*/,
        (byte) 4,
        (byte) 68,
        (byte) 77,
        (byte) 128 /*0x80*/,
        (byte) 71
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[50]
      {
        (byte) 237,
        (byte) 158,
        (byte) 7,
        (byte) 173,
        (byte) 216,
        (byte) 161,
        (byte) 1,
        (byte) 30,
        (byte) 28,
        (byte) 225,
        (byte) 47,
        (byte) 172,
        (byte) 102,
        (byte) 197,
        (byte) 0,
        (byte) 3,
        (byte) 19,
        (byte) 43,
        (byte) 157,
        (byte) 201,
        (byte) 48 /*0x30*/,
        (byte) 247,
        (byte) 31 /*0x1F*/,
        (byte) 74,
        (byte) 44,
        (byte) 18,
        (byte) 72,
        (byte) 244,
        (byte) 7,
        (byte) 224 /*0xE0*/,
        (byte) 178,
        (byte) 61,
        (byte) 26,
        (byte) 65,
        (byte) 55,
        (byte) 57,
        (byte) 132,
        (byte) 104,
        (byte) 216,
        (byte) 44,
        (byte) 148,
        (byte) 143,
        (byte) 36,
        (byte) 65,
        (byte) 80 /*0x50*/,
        (byte) 129,
        (byte) 4,
        (byte) 176 /*0xB0*/,
        (byte) 237,
        (byte) 47
      };
      byte[] numArray5 = new byte[50]
      {
        (byte) 177,
        (byte) 103,
        (byte) 178,
        (byte) 160 /*0xA0*/,
        (byte) 69,
        (byte) 196,
        (byte) 99,
        (byte) 110,
        (byte) 9,
        (byte) 36,
        (byte) 80 /*0x50*/,
        (byte) 196,
        (byte) 134,
        (byte) 212,
        (byte) 18,
        (byte) 73,
        (byte) 253,
        (byte) 182,
        (byte) 139,
        (byte) 69,
        (byte) 140,
        (byte) 240 /*0xF0*/,
        (byte) 188,
        (byte) 0,
        (byte) 166,
        (byte) 209,
        (byte) 225,
        (byte) 85,
        (byte) 175,
        (byte) 240 /*0xF0*/,
        (byte) 65,
        (byte) 117,
        (byte) 194,
        (byte) 180,
        (byte) 61,
        (byte) 42,
        (byte) 238,
        (byte) 143,
        (byte) 111,
        (byte) 88,
        (byte) 152,
        (byte) 231,
        (byte) 153,
        (byte) 127 /*0x7F*/,
        (byte) 159,
        (byte) 208 /*0xD0*/,
        (byte) 190,
        (byte) 169,
        (byte) 242,
        (byte) 146
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 50);
      for (int index = 0; index < 50; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[18];
      byte[] response = new byte[18];
      Array.Copy((Array) sc_12465.sspq, 151, (Array) numArray6, 0, 18);
      key.Query(true, 335, numArray6, response);
      Array.Copy((Array) sc_12465.sspr, 151, (Array) numArray6, 0, 18);
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
    byte[] numArray7 = new byte[105];
    byte[] numArray8 = new byte[55]
    {
      (byte) 82,
      (byte) 113,
      (byte) 53,
      (byte) 160 /*0xA0*/,
      (byte) 210,
      (byte) 222,
      (byte) 249,
      (byte) 171,
      (byte) 14,
      (byte) 152,
      (byte) 6,
      (byte) 216,
      (byte) 231,
      (byte) 251,
      (byte) 120,
      (byte) 36,
      (byte) 66,
      (byte) 64 /*0x40*/,
      (byte) 187,
      (byte) 199,
      (byte) 232,
      (byte) 18,
      (byte) 212,
      (byte) 135,
      (byte) 120,
      (byte) 232,
      (byte) 19,
      (byte) 94,
      (byte) 239,
      (byte) 222,
      (byte) 33,
      (byte) 38,
      (byte) 176 /*0xB0*/,
      (byte) 77,
      (byte) 100,
      (byte) 83,
      (byte) 192 /*0xC0*/,
      (byte) 94,
      (byte) 36,
      (byte) 159,
      (byte) 195,
      (byte) 213,
      (byte) 183,
      (byte) 191,
      (byte) 5,
      (byte) 62,
      (byte) 84,
      (byte) 51,
      (byte) 122,
      (byte) 109,
      (byte) 227,
      (byte) 78,
      (byte) 11,
      (byte) 182,
      (byte) 194
    };
    byte[] numArray9 = new byte[55]
    {
      (byte) 229,
      (byte) 73,
      (byte) 120,
      (byte) 140,
      (byte) 109,
      (byte) 161,
      (byte) 160 /*0xA0*/,
      (byte) 12,
      (byte) 154,
      (byte) 20,
      (byte) 136,
      (byte) 202,
      (byte) 161,
      (byte) 105,
      (byte) 223,
      (byte) 177,
      (byte) 99,
      (byte) 206,
      (byte) 121,
      (byte) 0,
      (byte) 154,
      (byte) 61,
      (byte) 33,
      (byte) 97,
      (byte) 41,
      (byte) 218,
      (byte) 1,
      (byte) 118,
      (byte) 185,
      (byte) 22,
      (byte) 39,
      (byte) 206,
      (byte) 201,
      (byte) 7,
      (byte) 132,
      (byte) 60,
      (byte) 161,
      (byte) 204,
      (byte) 118,
      (byte) 230,
      (byte) 68,
      (byte) 93,
      (byte) 75,
      (byte) 157,
      (byte) 120,
      (byte) 216,
      (byte) 179,
      (byte) 45,
      (byte) 19,
      (byte) 213,
      (byte) 87,
      (byte) 64 /*0x40*/,
      (byte) 134,
      (byte) 0,
      (byte) 126
    };
    key.Query(true, 335, numArray8, numArray8);
    Array.Copy((Array) numArray8, 0, (Array) numArray7, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray7[index] ^= numArray9[index];
    byte[] numArray10 = new byte[50]
    {
      (byte) 173,
      (byte) 40,
      (byte) 233,
      (byte) 203,
      (byte) 155,
      (byte) 188,
      (byte) 47,
      (byte) 118,
      (byte) 224 /*0xE0*/,
      (byte) 40,
      (byte) 153,
      (byte) 150,
      (byte) 36,
      (byte) 100,
      (byte) 227,
      (byte) 27,
      (byte) 27,
      (byte) 4,
      (byte) 18,
      (byte) 90,
      (byte) 20,
      (byte) 129,
      (byte) 57,
      (byte) 148,
      (byte) 224 /*0xE0*/,
      (byte) 228,
      (byte) 129,
      (byte) 251,
      (byte) 211,
      (byte) 27,
      (byte) 249,
      (byte) 139,
      (byte) 36,
      (byte) 182,
      (byte) 18,
      (byte) 54,
      (byte) 59,
      (byte) 212,
      (byte) 39,
      (byte) 25,
      (byte) 135,
      (byte) 73,
      (byte) 37,
      (byte) 22,
      (byte) 186,
      (byte) 156,
      (byte) 85,
      (byte) 8,
      byte.MaxValue,
      (byte) 50
    };
    byte[] numArray11 = new byte[50]
    {
      (byte) 239,
      (byte) 0,
      (byte) 129,
      (byte) 177,
      (byte) 220,
      (byte) 25,
      (byte) 96 /*0x60*/,
      (byte) 209,
      (byte) 253,
      (byte) 223,
      (byte) 76,
      (byte) 241,
      (byte) 242,
      (byte) 53,
      (byte) 106,
      (byte) 151,
      (byte) 106,
      (byte) 9,
      (byte) 19,
      (byte) 154,
      (byte) 253,
      (byte) 117,
      (byte) 103,
      (byte) 4,
      (byte) 111,
      (byte) 86,
      (byte) 137,
      (byte) 31 /*0x1F*/,
      (byte) 159,
      (byte) 31 /*0x1F*/,
      (byte) 222,
      (byte) 140,
      (byte) 20,
      (byte) 111,
      (byte) 88,
      (byte) 95,
      (byte) 86,
      (byte) 42,
      (byte) 72,
      (byte) 182,
      (byte) 188,
      (byte) 201,
      (byte) 234,
      (byte) 178,
      (byte) 38,
      (byte) 150,
      (byte) 226,
      (byte) 210,
      (byte) 192 /*0xC0*/,
      (byte) 104
    };
    key.Query(true, 335, numArray10, numArray10);
    Array.Copy((Array) numArray10, 0, (Array) numArray7, 55, 50);
    for (int index = 0; index < 50; ++index)
      numArray7[index + 55] ^= numArray11[index];
    return Encoding.UTF8.GetString(numArray7);
  }

  internal static string ssp_appserver_12496()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10];
      numArray2[9] = (byte) 196;
      numArray2[1] = (byte) 44;
      numArray2[2] = (byte) 238;
      numArray2[5] = (byte) 182;
      numArray2[4] = (byte) 235;
      numArray2[3] = (byte) 85;
      numArray2[8] = (byte) 162;
      numArray2[7] = (byte) 52;
      numArray2[0] = (byte) 136;
      numArray2[6] = (byte) 58;
      byte[] numArray3 = new byte[10]
      {
        (byte) 163,
        (byte) 247,
        (byte) 30,
        (byte) 140,
        (byte) 251,
        (byte) 132,
        (byte) 93,
        (byte) 87,
        (byte) 70,
        (byte) 170
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
      (byte) 35,
      (byte) 217,
      (byte) 67,
      (byte) 142,
      (byte) 120,
      (byte) 251,
      (byte) 161,
      (byte) 43,
      (byte) 71,
      (byte) 219
    };
    byte[] numArray6 = new byte[10];
    numArray6[5] = (byte) 178;
    numArray6[1] = (byte) 182;
    numArray6[4] = (byte) 105;
    numArray6[3] = (byte) 248;
    numArray6[6] = (byte) 18;
    numArray6[0] = (byte) 77;
    numArray6[8] = (byte) 91;
    numArray6[7] = (byte) 13;
    numArray6[2] = (byte) 160 /*0xA0*/;
    numArray6[9] = (byte) 159;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[42];
    byte[] response = new byte[42];
    Array.Copy((Array) sc_12465.sspq, 169, (Array) numArray7, 0, 42);
    key.Query(true, 335, numArray7, response);
    Array.Copy((Array) sc_12465.sspr, 169, (Array) numArray7, 0, 42);
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

  internal static string ssp_appserver_12497()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[97];
      byte[] numArray2 = new byte[55]
      {
        (byte) 20,
        (byte) 125,
        (byte) 106,
        (byte) 65,
        (byte) 73,
        (byte) 29,
        (byte) 27,
        (byte) 165,
        (byte) 200,
        (byte) 0,
        (byte) 120,
        (byte) 247,
        (byte) 167,
        (byte) 73,
        (byte) 237,
        (byte) 87,
        (byte) 75,
        (byte) 105,
        (byte) 157,
        (byte) 147,
        (byte) 152,
        (byte) 214,
        (byte) 142,
        (byte) 77,
        (byte) 143,
        (byte) 128 /*0x80*/,
        byte.MaxValue,
        (byte) 143,
        (byte) 177,
        (byte) 21,
        (byte) 116,
        (byte) 15,
        (byte) 6,
        (byte) 208 /*0xD0*/,
        (byte) 87,
        (byte) 231,
        (byte) 144 /*0x90*/,
        (byte) 27,
        (byte) 34,
        (byte) 252,
        (byte) 100,
        (byte) 127 /*0x7F*/,
        (byte) 49,
        (byte) 59,
        (byte) 181,
        (byte) 32 /*0x20*/,
        byte.MaxValue,
        (byte) 120,
        (byte) 188,
        (byte) 248,
        (byte) 56,
        (byte) 8,
        (byte) 133,
        (byte) 153,
        (byte) 215
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 112 /*0x70*/,
        (byte) 35,
        (byte) 145,
        (byte) 0,
        (byte) 151,
        (byte) 229,
        (byte) 227,
        (byte) 25,
        (byte) 61,
        (byte) 138,
        (byte) 32 /*0x20*/,
        (byte) 101,
        (byte) 219,
        (byte) 7,
        (byte) 189,
        (byte) 83,
        (byte) 99,
        (byte) 25,
        (byte) 49,
        (byte) 142,
        (byte) 193,
        (byte) 101,
        (byte) 64 /*0x40*/,
        (byte) 245,
        (byte) 190,
        (byte) 152,
        (byte) 97,
        (byte) 171,
        (byte) 230,
        (byte) 42,
        (byte) 87,
        (byte) 226,
        (byte) 221,
        (byte) 33,
        (byte) 238,
        (byte) 64 /*0x40*/,
        (byte) 161,
        (byte) 211,
        (byte) 142,
        (byte) 52,
        (byte) 72,
        (byte) 181,
        (byte) 41,
        (byte) 11,
        (byte) 247,
        (byte) 79,
        (byte) 121,
        (byte) 84,
        (byte) 75,
        (byte) 48 /*0x30*/,
        (byte) 157,
        (byte) 168,
        (byte) 181,
        (byte) 42,
        (byte) 228
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[42]
      {
        (byte) 28,
        (byte) 178,
        (byte) 200,
        (byte) 29,
        (byte) 139,
        (byte) 243,
        (byte) 179,
        (byte) 139,
        (byte) 104,
        (byte) 241,
        (byte) 215,
        (byte) 241,
        (byte) 5,
        (byte) 199,
        (byte) 47,
        (byte) 71,
        (byte) 161,
        (byte) 47,
        (byte) 24,
        (byte) 164,
        (byte) 83,
        (byte) 151,
        (byte) 110,
        (byte) 128 /*0x80*/,
        (byte) 134,
        (byte) 211,
        (byte) 185,
        (byte) 221,
        (byte) 108,
        (byte) 138,
        (byte) 109,
        (byte) 248,
        (byte) 25,
        (byte) 247,
        (byte) 115,
        (byte) 104,
        (byte) 89,
        (byte) 222,
        (byte) 216,
        (byte) 140,
        (byte) 126,
        (byte) 175
      };
      byte[] numArray5 = new byte[42]
      {
        (byte) 228,
        (byte) 240 /*0xF0*/,
        (byte) 83,
        (byte) 229,
        (byte) 154,
        (byte) 16 /*0x10*/,
        (byte) 88,
        (byte) 118,
        (byte) 156,
        (byte) 22,
        (byte) 56,
        (byte) 127 /*0x7F*/,
        (byte) 158,
        (byte) 236,
        (byte) 221,
        (byte) 6,
        (byte) 6,
        (byte) 30,
        (byte) 205,
        (byte) 138,
        (byte) 208 /*0xD0*/,
        (byte) 150,
        (byte) 148,
        (byte) 119,
        (byte) 135,
        (byte) 132,
        (byte) 205,
        (byte) 7,
        (byte) 21,
        (byte) 208 /*0xD0*/,
        (byte) 73,
        (byte) 110,
        (byte) 230,
        (byte) 78,
        (byte) 7,
        (byte) 246,
        (byte) 45,
        (byte) 203,
        (byte) 116,
        (byte) 163,
        (byte) 232,
        (byte) 222
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 42);
      for (int index = 0; index < 42; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[97];
    byte[] numArray7 = new byte[55];
    numArray7[50] = (byte) 168;
    numArray7[40] = (byte) 3;
    numArray7[0] = (byte) 134;
    numArray7[36] = (byte) 136;
    numArray7[4] = (byte) 193;
    numArray7[54] = (byte) 73;
    numArray7[19] = (byte) 132;
    numArray7[7] = (byte) 190;
    numArray7[8] = (byte) 153;
    numArray7[9] = (byte) 156;
    numArray7[10] = (byte) 133;
    numArray7[16 /*0x10*/] = (byte) 214;
    numArray7[41] = (byte) 57;
    numArray7[13] = (byte) 150;
    numArray7[37] = (byte) 212;
    numArray7[1] = (byte) 182;
    numArray7[23] = (byte) 83;
    numArray7[52] = (byte) 110;
    numArray7[18] = (byte) 222;
    numArray7[31 /*0x1F*/] = (byte) 74;
    numArray7[47] = (byte) 72;
    numArray7[21] = (byte) 216;
    numArray7[12] = (byte) 72;
    numArray7[6] = (byte) 105;
    numArray7[24] = (byte) 198;
    numArray7[34] = (byte) 0;
    numArray7[26] = (byte) 194;
    numArray7[27] = (byte) 87;
    numArray7[28] = (byte) 140;
    numArray7[29] = (byte) 38;
    numArray7[30] = (byte) 163;
    numArray7[51] = (byte) 144 /*0x90*/;
    numArray7[32 /*0x20*/] = (byte) 63 /*0x3F*/;
    numArray7[42] = (byte) 128 /*0x80*/;
    numArray7[3] = (byte) 8;
    numArray7[35] = (byte) 28;
    numArray7[48 /*0x30*/] = (byte) 31 /*0x1F*/;
    numArray7[20] = (byte) 222;
    numArray7[38] = (byte) 171;
    numArray7[39] = (byte) 166;
    numArray7[11] = (byte) 68;
    numArray7[33] = (byte) 87;
    numArray7[49] = (byte) 82;
    numArray7[25] = (byte) 237;
    numArray7[2] = (byte) 49;
    numArray7[45] = (byte) 36;
    numArray7[46] = (byte) 16 /*0x10*/;
    numArray7[15] = (byte) 57;
    numArray7[43] = (byte) 91;
    numArray7[5] = (byte) 228;
    numArray7[44] = (byte) 238;
    numArray7[53] = (byte) 170;
    numArray7[17] = (byte) 31 /*0x1F*/;
    numArray7[14] = (byte) 184;
    numArray7[22] = (byte) 14;
    byte[] numArray8 = new byte[55];
    numArray8[52] = (byte) 190;
    numArray8[1] = (byte) 205;
    numArray8[13] = (byte) 26;
    numArray8[44] = (byte) 189;
    numArray8[4] = (byte) 77;
    numArray8[42] = (byte) 129;
    numArray8[7] = (byte) 15;
    numArray8[23] = (byte) 197;
    numArray8[8] = (byte) 222;
    numArray8[32 /*0x20*/] = (byte) 131;
    numArray8[11] = (byte) 164;
    numArray8[38] = (byte) 165;
    numArray8[12] = (byte) 57;
    numArray8[50] = (byte) 254;
    numArray8[14] = (byte) 76;
    numArray8[53] = (byte) 110;
    numArray8[16 /*0x10*/] = (byte) 120;
    numArray8[9] = (byte) 135;
    numArray8[18] = (byte) 240 /*0xF0*/;
    numArray8[19] = (byte) 25;
    numArray8[27] = (byte) 85;
    numArray8[21] = (byte) 77;
    numArray8[22] = (byte) 16 /*0x10*/;
    numArray8[5] = (byte) 80 /*0x50*/;
    numArray8[39] = (byte) 158;
    numArray8[25] = (byte) 233;
    numArray8[26] = (byte) 23;
    numArray8[33] = (byte) 22;
    numArray8[28] = (byte) 38;
    numArray8[29] = (byte) 137;
    numArray8[46] = (byte) 146;
    numArray8[31 /*0x1F*/] = (byte) 30;
    numArray8[10] = (byte) 133;
    numArray8[6] = (byte) 246;
    numArray8[34] = (byte) 72;
    numArray8[35] = (byte) 121;
    numArray8[40] = (byte) 16 /*0x10*/;
    numArray8[45] = (byte) 208 /*0xD0*/;
    numArray8[54] = (byte) 98;
    numArray8[37] = (byte) 195;
    numArray8[51] = (byte) 96 /*0x60*/;
    numArray8[43] = (byte) 57;
    numArray8[3] = (byte) 168;
    numArray8[49] = (byte) 62;
    numArray8[0] = (byte) 149;
    numArray8[24] = (byte) 82;
    numArray8[17] = (byte) 85;
    numArray8[47] = (byte) 204;
    numArray8[48 /*0x30*/] = (byte) 119;
    numArray8[36] = (byte) 9;
    numArray8[20] = (byte) 16 /*0x10*/;
    numArray8[41] = (byte) 239;
    numArray8[30] = (byte) 172;
    numArray8[15] = (byte) 195;
    numArray8[2] = (byte) 241;
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[42];
    numArray9[13] = (byte) 211;
    numArray9[4] = (byte) 38;
    numArray9[1] = (byte) 29;
    numArray9[3] = (byte) 222;
    numArray9[0] = (byte) 197;
    numArray9[2] = (byte) 57;
    numArray9[11] = (byte) 74;
    numArray9[29] = (byte) 34;
    numArray9[8] = (byte) 184;
    numArray9[24] = (byte) 91;
    numArray9[10] = (byte) 121;
    numArray9[34] = (byte) 109;
    numArray9[14] = (byte) 122;
    numArray9[39] = (byte) 177;
    numArray9[28] = (byte) 117;
    numArray9[15] = (byte) 186;
    numArray9[16 /*0x10*/] = (byte) 214;
    numArray9[17] = (byte) 122;
    numArray9[37] = byte.MaxValue;
    numArray9[31 /*0x1F*/] = (byte) 97;
    numArray9[20] = (byte) 211;
    numArray9[9] = (byte) 6;
    numArray9[22] = (byte) 211;
    numArray9[23] = (byte) 127 /*0x7F*/;
    numArray9[21] = (byte) 92;
    numArray9[6] = (byte) 29;
    numArray9[27] = (byte) 158;
    numArray9[18] = (byte) 141;
    numArray9[7] = (byte) 240 /*0xF0*/;
    numArray9[19] = (byte) 49;
    numArray9[36] = (byte) 168;
    numArray9[25] = (byte) 87;
    numArray9[32 /*0x20*/] = (byte) 238;
    numArray9[33] = (byte) 217;
    numArray9[5] = (byte) 94;
    numArray9[26] = byte.MaxValue;
    numArray9[35] = (byte) 213;
    numArray9[12] = (byte) 167;
    numArray9[38] = (byte) 238;
    numArray9[30] = (byte) 208 /*0xD0*/;
    numArray9[40] = (byte) 251;
    numArray9[41] = (byte) 140;
    byte[] numArray10 = new byte[42]
    {
      (byte) 203,
      (byte) 189,
      (byte) 148,
      (byte) 110,
      byte.MaxValue,
      (byte) 202,
      (byte) 0,
      (byte) 90,
      (byte) 45,
      (byte) 72,
      (byte) 87,
      (byte) 197,
      (byte) 172,
      (byte) 3,
      (byte) 122,
      (byte) 185,
      (byte) 230,
      (byte) 15,
      (byte) 189,
      (byte) 212,
      (byte) 247,
      (byte) 13,
      (byte) 143,
      (byte) 111,
      (byte) 219,
      (byte) 195,
      (byte) 103,
      (byte) 130,
      (byte) 67,
      (byte) 50,
      (byte) 176 /*0xB0*/,
      (byte) 146,
      (byte) 37,
      (byte) 130,
      (byte) 67,
      (byte) 63 /*0x3F*/,
      (byte) 70,
      (byte) 69,
      (byte) 154,
      (byte) 211,
      (byte) 94,
      (byte) 126
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 42);
    for (int index = 0; index < 42; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_12498()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[97];
      byte[] numArray2 = new byte[55]
      {
        (byte) 6,
        (byte) 3,
        (byte) 140,
        (byte) 237,
        (byte) 124,
        (byte) 31 /*0x1F*/,
        (byte) 83,
        (byte) 124,
        (byte) 17,
        (byte) 6,
        (byte) 114,
        (byte) 171,
        (byte) 199,
        (byte) 230,
        (byte) 94,
        (byte) 24,
        (byte) 180,
        (byte) 213,
        (byte) 147,
        (byte) 159,
        (byte) 163,
        (byte) 140,
        (byte) 169,
        (byte) 30,
        byte.MaxValue,
        (byte) 73,
        (byte) 45,
        (byte) 36,
        (byte) 158,
        (byte) 203,
        (byte) 31 /*0x1F*/,
        (byte) 132,
        (byte) 50,
        (byte) 70,
        (byte) 199,
        (byte) 99,
        (byte) 24,
        (byte) 36,
        (byte) 90,
        (byte) 108,
        (byte) 135,
        (byte) 186,
        (byte) 52,
        (byte) 184,
        (byte) 185,
        (byte) 16 /*0x10*/,
        (byte) 114,
        (byte) 187,
        (byte) 219,
        (byte) 199,
        (byte) 250,
        (byte) 160 /*0xA0*/,
        (byte) 29,
        (byte) 71,
        (byte) 113
      };
      byte[] numArray3 = new byte[55];
      numArray3[3] = (byte) 22;
      numArray3[18] = (byte) 85;
      numArray3[2] = (byte) 137;
      numArray3[46] = (byte) 121;
      numArray3[7] = (byte) 180;
      numArray3[37] = (byte) 17;
      numArray3[5] = (byte) 97;
      numArray3[16 /*0x10*/] = (byte) 215;
      numArray3[8] = (byte) 114;
      numArray3[4] = (byte) 94;
      numArray3[50] = (byte) 55;
      numArray3[1] = (byte) 44;
      numArray3[12] = (byte) 92;
      numArray3[13] = (byte) 187;
      numArray3[21] = (byte) 188;
      numArray3[9] = (byte) 243;
      numArray3[19] = (byte) 157;
      numArray3[17] = (byte) 26;
      numArray3[42] = (byte) 83;
      numArray3[32 /*0x20*/] = (byte) 40;
      numArray3[34] = (byte) 78;
      numArray3[48 /*0x30*/] = (byte) 164;
      numArray3[33] = (byte) 233;
      numArray3[23] = (byte) 88;
      numArray3[24] = (byte) 243;
      numArray3[51] = (byte) 211;
      numArray3[26] = (byte) 222;
      numArray3[27] = (byte) 106;
      numArray3[14] = (byte) 18;
      numArray3[29] = (byte) 102;
      numArray3[30] = (byte) 18;
      numArray3[31 /*0x1F*/] = (byte) 191;
      numArray3[6] = (byte) 234;
      numArray3[28] = (byte) 67;
      numArray3[10] = (byte) 96 /*0x60*/;
      numArray3[20] = (byte) 241;
      numArray3[36] = (byte) 196;
      numArray3[54] = (byte) 36;
      numArray3[38] = (byte) 182;
      numArray3[39] = (byte) 3;
      numArray3[40] = (byte) 236;
      numArray3[41] = (byte) 190;
      numArray3[15] = (byte) 15;
      numArray3[35] = (byte) 216;
      numArray3[44] = (byte) 240 /*0xF0*/;
      numArray3[45] = (byte) 7;
      numArray3[25] = (byte) 224 /*0xE0*/;
      numArray3[47] = (byte) 38;
      numArray3[0] = (byte) 201;
      numArray3[53] = (byte) 166;
      numArray3[43] = (byte) 124;
      numArray3[22] = (byte) 93;
      numArray3[52] = (byte) 77;
      numArray3[11] = (byte) 14;
      numArray3[49] = (byte) 40;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[42]
      {
        (byte) 8,
        (byte) 254,
        (byte) 91,
        (byte) 126,
        (byte) 230,
        (byte) 6,
        (byte) 117,
        (byte) 104,
        (byte) 135,
        (byte) 142,
        (byte) 172,
        (byte) 209,
        (byte) 242,
        (byte) 85,
        (byte) 89,
        (byte) 82,
        (byte) 171,
        (byte) 191,
        (byte) 11,
        (byte) 105,
        (byte) 31 /*0x1F*/,
        (byte) 154,
        (byte) 87,
        (byte) 230,
        (byte) 214,
        (byte) 105,
        (byte) 15,
        (byte) 102,
        (byte) 88,
        (byte) 111,
        (byte) 187,
        (byte) 74,
        (byte) 43,
        (byte) 160 /*0xA0*/,
        (byte) 161,
        (byte) 55,
        (byte) 205,
        (byte) 252,
        (byte) 113,
        (byte) 214,
        (byte) 121,
        (byte) 104
      };
      byte[] numArray5 = new byte[42]
      {
        (byte) 131,
        (byte) 60,
        (byte) 69,
        (byte) 55,
        (byte) 142,
        (byte) 217,
        (byte) 182,
        (byte) 72,
        (byte) 201,
        (byte) 37,
        (byte) 118,
        (byte) 18,
        (byte) 81,
        (byte) 100,
        (byte) 82,
        (byte) 110,
        (byte) 157,
        (byte) 20,
        (byte) 222,
        (byte) 237,
        (byte) 181,
        (byte) 209,
        (byte) 38,
        (byte) 237,
        (byte) 234,
        (byte) 199,
        (byte) 206,
        (byte) 186,
        (byte) 137,
        (byte) 48 /*0x30*/,
        (byte) 228,
        (byte) 152,
        (byte) 121,
        (byte) 189,
        (byte) 103,
        (byte) 136,
        (byte) 210,
        (byte) 116,
        (byte) 95,
        (byte) 249,
        (byte) 81,
        (byte) 233
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 42);
      for (int index = 0; index < 42; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[97];
    byte[] numArray7 = new byte[55]
    {
      (byte) 203,
      (byte) 237,
      (byte) 114,
      (byte) 17,
      (byte) 212,
      (byte) 75,
      (byte) 111,
      (byte) 125,
      (byte) 184,
      (byte) 73,
      (byte) 110,
      (byte) 167,
      (byte) 116,
      (byte) 168,
      (byte) 124,
      (byte) 246,
      (byte) 14,
      (byte) 16 /*0x10*/,
      (byte) 24,
      (byte) 210,
      (byte) 97,
      (byte) 165,
      (byte) 81,
      (byte) 179,
      (byte) 33,
      (byte) 132,
      (byte) 175,
      (byte) 78,
      (byte) 52,
      (byte) 240 /*0xF0*/,
      (byte) 182,
      (byte) 86,
      (byte) 78,
      (byte) 161,
      (byte) 11,
      (byte) 124,
      (byte) 157,
      (byte) 85,
      (byte) 131,
      (byte) 35,
      (byte) 76,
      (byte) 205,
      (byte) 114,
      (byte) 86,
      (byte) 51,
      (byte) 144 /*0x90*/,
      (byte) 88,
      (byte) 83,
      (byte) 245,
      (byte) 223,
      (byte) 106,
      (byte) 88,
      (byte) 43,
      (byte) 54,
      (byte) 226
    };
    byte[] numArray8 = new byte[55]
    {
      (byte) 4,
      (byte) 196,
      (byte) 252,
      (byte) 134,
      (byte) 100,
      (byte) 141,
      (byte) 18,
      (byte) 181,
      (byte) 103,
      (byte) 177,
      (byte) 45,
      (byte) 12,
      (byte) 169,
      (byte) 130,
      (byte) 224 /*0xE0*/,
      (byte) 146,
      (byte) 60,
      (byte) 238,
      (byte) 220,
      (byte) 226,
      (byte) 146,
      (byte) 145,
      (byte) 205,
      (byte) 156,
      (byte) 252,
      (byte) 215,
      (byte) 226,
      (byte) 223,
      (byte) 232,
      (byte) 31 /*0x1F*/,
      (byte) 233,
      (byte) 181,
      (byte) 116,
      (byte) 132,
      (byte) 173,
      (byte) 224 /*0xE0*/,
      (byte) 166,
      (byte) 73,
      (byte) 240 /*0xF0*/,
      (byte) 184,
      (byte) 230,
      (byte) 188,
      (byte) 51,
      (byte) 231,
      (byte) 144 /*0x90*/,
      (byte) 64 /*0x40*/,
      (byte) 45,
      (byte) 46,
      (byte) 41,
      (byte) 100,
      (byte) 189,
      (byte) 89,
      (byte) 203,
      (byte) 153,
      (byte) 200
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[42]
    {
      (byte) 190,
      (byte) 239,
      (byte) 79,
      (byte) 156,
      (byte) 33,
      (byte) 72,
      (byte) 200,
      (byte) 146,
      (byte) 23,
      (byte) 88,
      (byte) 211,
      (byte) 157,
      byte.MaxValue,
      (byte) 89,
      (byte) 185,
      (byte) 140,
      (byte) 244,
      (byte) 133,
      (byte) 148,
      (byte) 51,
      (byte) 251,
      (byte) 128 /*0x80*/,
      (byte) 145,
      (byte) 44,
      (byte) 91,
      (byte) 89,
      (byte) 84,
      (byte) 133,
      (byte) 94,
      (byte) 43,
      (byte) 23,
      (byte) 190,
      (byte) 229,
      (byte) 225,
      (byte) 223,
      (byte) 102,
      (byte) 52,
      (byte) 135,
      (byte) 43,
      (byte) 132,
      (byte) 170,
      (byte) 196
    };
    byte[] numArray10 = new byte[42]
    {
      (byte) 236,
      (byte) 178,
      (byte) 169,
      (byte) 227,
      (byte) 192 /*0xC0*/,
      (byte) 213,
      (byte) 187,
      (byte) 231,
      (byte) 61,
      (byte) 101,
      (byte) 219,
      (byte) 52,
      (byte) 126,
      (byte) 8,
      (byte) 133,
      (byte) 48 /*0x30*/,
      (byte) 25,
      (byte) 182,
      (byte) 68,
      (byte) 49,
      (byte) 9,
      (byte) 139,
      (byte) 166,
      (byte) 225,
      (byte) 175,
      (byte) 111,
      (byte) 188,
      (byte) 27,
      (byte) 145,
      (byte) 216,
      (byte) 192 /*0xC0*/,
      (byte) 45,
      (byte) 37,
      (byte) 159,
      (byte) 193,
      (byte) 83,
      (byte) 206,
      (byte) 21,
      (byte) 60,
      (byte) 31 /*0x1F*/,
      (byte) 161,
      (byte) 77
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 42);
    for (int index = 0; index < 42; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }
}
