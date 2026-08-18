// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_12699
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_12699
{
  private static byte[] sspq = new byte[37]
  {
    (byte) 122,
    (byte) 131,
    (byte) 237,
    (byte) 87,
    (byte) 85,
    (byte) 56,
    (byte) 37,
    (byte) 96 /*0x60*/,
    (byte) 252,
    (byte) 123,
    (byte) 205,
    (byte) 59,
    (byte) 66,
    (byte) 158,
    (byte) 212,
    (byte) 172,
    (byte) 224 /*0xE0*/,
    (byte) 31 /*0x1F*/,
    (byte) 236,
    byte.MaxValue,
    (byte) 41,
    (byte) 237,
    (byte) 194,
    (byte) 247,
    (byte) 37,
    (byte) 137,
    byte.MaxValue,
    (byte) 3,
    (byte) 182,
    (byte) 144 /*0x90*/,
    (byte) 71,
    (byte) 34,
    (byte) 233,
    (byte) 94,
    (byte) 70,
    (byte) 68,
    (byte) 230
  };
  private static byte[] sspr = new byte[37]
  {
    (byte) 218,
    (byte) 47,
    (byte) 78,
    (byte) 235,
    (byte) 133,
    (byte) 19,
    (byte) 223,
    (byte) 164,
    (byte) 55,
    (byte) 207,
    (byte) 141,
    (byte) 84,
    (byte) 182,
    (byte) 95,
    (byte) 199,
    (byte) 182,
    (byte) 121,
    (byte) 245,
    (byte) 40,
    byte.MaxValue,
    (byte) 119,
    (byte) 201,
    (byte) 102,
    (byte) 197,
    (byte) 63 /*0x3F*/,
    (byte) 19,
    (byte) 87,
    (byte) 41,
    (byte) 230,
    (byte) 33,
    (byte) 141,
    (byte) 72,
    (byte) 3,
    (byte) 74,
    (byte) 117,
    (byte) 188,
    (byte) 107
  };

  internal static string ssp_appserver_12700()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[101];
      byte[] numArray2 = new byte[55]
      {
        (byte) 216,
        (byte) 23,
        (byte) 26,
        (byte) 105,
        (byte) 164,
        (byte) 148,
        (byte) 215,
        (byte) 154,
        (byte) 150,
        (byte) 69,
        (byte) 102,
        (byte) 209,
        (byte) 23,
        (byte) 178,
        (byte) 234,
        (byte) 246,
        (byte) 55,
        (byte) 79,
        (byte) 231,
        (byte) 58,
        (byte) 171,
        (byte) 19,
        (byte) 153,
        (byte) 221,
        (byte) 185,
        (byte) 39,
        (byte) 62,
        (byte) 66,
        (byte) 50,
        (byte) 189,
        (byte) 17,
        (byte) 11,
        (byte) 225,
        (byte) 148,
        (byte) 193,
        (byte) 5,
        (byte) 203,
        (byte) 19,
        (byte) 141,
        (byte) 244,
        (byte) 181,
        (byte) 35,
        (byte) 98,
        (byte) 116,
        (byte) 67,
        (byte) 166,
        (byte) 196,
        (byte) 5,
        (byte) 125,
        (byte) 227,
        (byte) 50,
        byte.MaxValue,
        (byte) 102,
        (byte) 173,
        (byte) 76
      };
      byte[] numArray3 = new byte[55];
      numArray3[8] = (byte) 221;
      numArray3[1] = byte.MaxValue;
      numArray3[53] = (byte) 207;
      numArray3[50] = (byte) 59;
      numArray3[4] = (byte) 207;
      numArray3[43] = (byte) 203;
      numArray3[36] = (byte) 167;
      numArray3[2] = (byte) 72;
      numArray3[31 /*0x1F*/] = (byte) 178;
      numArray3[9] = (byte) 20;
      numArray3[40] = (byte) 101;
      numArray3[14] = (byte) 184;
      numArray3[39] = (byte) 243;
      numArray3[0] = (byte) 37;
      numArray3[20] = (byte) 33;
      numArray3[44] = (byte) 91;
      numArray3[29] = (byte) 21;
      numArray3[26] = (byte) 135;
      numArray3[18] = (byte) 200;
      numArray3[19] = (byte) 173;
      numArray3[11] = (byte) 122;
      numArray3[21] = (byte) 166;
      numArray3[22] = (byte) 250;
      numArray3[23] = (byte) 254;
      numArray3[30] = (byte) 18;
      numArray3[25] = (byte) 214;
      numArray3[45] = (byte) 236;
      numArray3[27] = (byte) 224 /*0xE0*/;
      numArray3[35] = (byte) 164;
      numArray3[3] = (byte) 6;
      numArray3[17] = (byte) 47;
      numArray3[46] = (byte) 170;
      numArray3[32 /*0x20*/] = (byte) 219;
      numArray3[33] = (byte) 27;
      numArray3[34] = (byte) 46;
      numArray3[5] = (byte) 68;
      numArray3[51] = (byte) 61;
      numArray3[37] = (byte) 20;
      numArray3[38] = (byte) 30;
      numArray3[6] = (byte) 54;
      numArray3[42] = (byte) 46;
      numArray3[41] = (byte) 132;
      numArray3[15] = (byte) 211;
      numArray3[7] = (byte) 90;
      numArray3[16 /*0x10*/] = (byte) 157;
      numArray3[54] = (byte) 84;
      numArray3[28] = (byte) 245;
      numArray3[47] = (byte) 11;
      numArray3[48 /*0x30*/] = (byte) 165;
      numArray3[49] = (byte) 200;
      numArray3[10] = (byte) 125;
      numArray3[24] = (byte) 74;
      numArray3[52] = (byte) 95;
      numArray3[12] = (byte) 1;
      numArray3[13] = (byte) 109;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[46]
      {
        (byte) 163,
        (byte) 156,
        (byte) 26,
        (byte) 28,
        (byte) 207,
        (byte) 24,
        (byte) 78,
        (byte) 33,
        (byte) 181,
        (byte) 187,
        (byte) 60,
        (byte) 244,
        (byte) 92,
        (byte) 157,
        (byte) 191,
        (byte) 178,
        (byte) 84,
        (byte) 101,
        (byte) 205,
        (byte) 39,
        (byte) 228,
        (byte) 177,
        (byte) 92,
        (byte) 33,
        (byte) 37,
        (byte) 45,
        (byte) 152,
        (byte) 123,
        (byte) 212,
        (byte) 49,
        (byte) 12,
        (byte) 130,
        (byte) 232,
        (byte) 47,
        (byte) 234,
        (byte) 198,
        (byte) 245,
        (byte) 130,
        (byte) 79,
        (byte) 161,
        (byte) 59,
        (byte) 31 /*0x1F*/,
        (byte) 50,
        (byte) 251,
        (byte) 4,
        (byte) 30
      };
      byte[] numArray5 = new byte[46]
      {
        (byte) 8,
        (byte) 182,
        (byte) 17,
        (byte) 68,
        (byte) 64 /*0x40*/,
        (byte) 87,
        (byte) 182,
        (byte) 56,
        (byte) 62,
        (byte) 14,
        (byte) 114,
        (byte) 35,
        (byte) 116,
        (byte) 100,
        (byte) 98,
        (byte) 68,
        (byte) 113,
        (byte) 47,
        (byte) 74,
        (byte) 201,
        (byte) 205,
        (byte) 52,
        (byte) 170,
        (byte) 30,
        (byte) 228,
        (byte) 132,
        (byte) 35,
        (byte) 63 /*0x3F*/,
        (byte) 221,
        (byte) 71,
        (byte) 176 /*0xB0*/,
        (byte) 185,
        (byte) 214,
        (byte) 251,
        (byte) 225,
        (byte) 181,
        (byte) 159,
        (byte) 185,
        (byte) 8,
        (byte) 215,
        (byte) 118,
        (byte) 209,
        (byte) 140,
        (byte) 224 /*0xE0*/,
        (byte) 123,
        (byte) 198
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 46);
      for (int index = 0; index < 46; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[101];
    byte[] numArray7 = new byte[55];
    numArray7[18] = (byte) 45;
    numArray7[21] = (byte) 188;
    numArray7[53] = (byte) 101;
    numArray7[7] = (byte) 64 /*0x40*/;
    numArray7[25] = (byte) 236;
    numArray7[2] = (byte) 194;
    numArray7[6] = (byte) 241;
    numArray7[27] = (byte) 126;
    numArray7[17] = (byte) 12;
    numArray7[9] = (byte) 246;
    numArray7[51] = (byte) 17;
    numArray7[1] = (byte) 31 /*0x1F*/;
    numArray7[34] = (byte) 73;
    numArray7[13] = (byte) 77;
    numArray7[38] = (byte) 251;
    numArray7[30] = (byte) 6;
    numArray7[16 /*0x10*/] = (byte) 186;
    numArray7[8] = (byte) 249;
    numArray7[23] = (byte) 21;
    numArray7[19] = (byte) 179;
    numArray7[20] = (byte) 249;
    numArray7[39] = (byte) 186;
    numArray7[0] = (byte) 139;
    numArray7[54] = (byte) 85;
    numArray7[49] = (byte) 76;
    numArray7[14] = (byte) 132;
    numArray7[26] = (byte) 196;
    numArray7[24] = (byte) 43;
    numArray7[28] = (byte) 97;
    numArray7[29] = (byte) 28;
    numArray7[22] = (byte) 209;
    numArray7[3] = (byte) 247;
    numArray7[32 /*0x20*/] = (byte) 127 /*0x7F*/;
    numArray7[33] = (byte) 44;
    numArray7[12] = (byte) 210;
    numArray7[35] = (byte) 18;
    numArray7[36] = (byte) 74;
    numArray7[37] = (byte) 139;
    numArray7[44] = (byte) 172;
    numArray7[4] = (byte) 236;
    numArray7[15] = (byte) 175;
    numArray7[41] = (byte) 134;
    numArray7[40] = (byte) 248;
    numArray7[43] = (byte) 140;
    numArray7[10] = (byte) 162;
    numArray7[45] = (byte) 33;
    numArray7[5] = (byte) 116;
    numArray7[50] = (byte) 74;
    numArray7[48 /*0x30*/] = (byte) 174;
    numArray7[11] = (byte) 128 /*0x80*/;
    numArray7[42] = (byte) 252;
    numArray7[47] = (byte) 210;
    numArray7[52] = (byte) 13;
    numArray7[46] = (byte) 104;
    numArray7[31 /*0x1F*/] = (byte) 241;
    byte[] numArray8 = new byte[55]
    {
      (byte) 228,
      (byte) 62,
      (byte) 50,
      (byte) 174,
      (byte) 2,
      (byte) 93,
      (byte) 88,
      (byte) 110,
      (byte) 18,
      (byte) 97,
      (byte) 27,
      (byte) 131,
      (byte) 26,
      (byte) 81,
      (byte) 170,
      (byte) 141,
      (byte) 231,
      (byte) 234,
      (byte) 75,
      (byte) 174,
      (byte) 123,
      (byte) 158,
      (byte) 43,
      (byte) 2,
      (byte) 190,
      (byte) 65,
      (byte) 40,
      (byte) 174,
      (byte) 203,
      (byte) 5,
      (byte) 178,
      (byte) 192 /*0xC0*/,
      (byte) 109,
      (byte) 50,
      (byte) 19,
      (byte) 165,
      (byte) 227,
      (byte) 192 /*0xC0*/,
      (byte) 5,
      (byte) 237,
      (byte) 242,
      (byte) 218,
      (byte) 15,
      (byte) 125,
      (byte) 145,
      (byte) 163,
      (byte) 207,
      (byte) 98,
      (byte) 177,
      (byte) 214,
      (byte) 47,
      (byte) 254,
      (byte) 245,
      (byte) 23,
      (byte) 86
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[46]
    {
      (byte) 67,
      (byte) 193,
      (byte) 124,
      (byte) 188,
      (byte) 163,
      (byte) 202,
      (byte) 9,
      (byte) 250,
      (byte) 205,
      (byte) 231,
      (byte) 207,
      (byte) 138,
      (byte) 172,
      (byte) 199,
      (byte) 111,
      (byte) 22,
      (byte) 103,
      (byte) 53,
      (byte) 203,
      (byte) 159,
      (byte) 168,
      (byte) 109,
      (byte) 233,
      (byte) 193,
      (byte) 36,
      (byte) 147,
      (byte) 45,
      (byte) 182,
      (byte) 244,
      (byte) 73,
      (byte) 110,
      (byte) 243,
      (byte) 107,
      (byte) 189,
      (byte) 90,
      (byte) 253,
      (byte) 126,
      (byte) 78,
      (byte) 226,
      (byte) 221,
      (byte) 22,
      (byte) 161,
      (byte) 46,
      (byte) 56,
      (byte) 242,
      (byte) 68
    };
    byte[] numArray10 = new byte[46]
    {
      (byte) 225,
      (byte) 161,
      (byte) 21,
      (byte) 63 /*0x3F*/,
      (byte) 171,
      (byte) 98,
      (byte) 152,
      (byte) 111,
      (byte) 32 /*0x20*/,
      (byte) 228,
      (byte) 61,
      (byte) 242,
      (byte) 234,
      (byte) 140,
      (byte) 233,
      (byte) 6,
      (byte) 127 /*0x7F*/,
      (byte) 151,
      (byte) 61,
      (byte) 174,
      (byte) 46,
      (byte) 136,
      (byte) 36,
      (byte) 58,
      (byte) 62,
      (byte) 211,
      (byte) 144 /*0x90*/,
      (byte) 71,
      (byte) 89,
      (byte) 82,
      (byte) 252,
      (byte) 104,
      (byte) 49,
      (byte) 198,
      (byte) 45,
      (byte) 197,
      (byte) 247,
      (byte) 20,
      (byte) 186,
      (byte) 16 /*0x10*/,
      (byte) 2,
      (byte) 31 /*0x1F*/,
      (byte) 7,
      (byte) 89,
      (byte) 71,
      (byte) 134
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 46);
    for (int index = 0; index < 46; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_12701()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[68];
      byte[] numArray2 = new byte[55];
      numArray2[33] = (byte) 10;
      numArray2[5] = (byte) 213;
      numArray2[2] = (byte) 23;
      numArray2[0] = (byte) 51;
      numArray2[53] = (byte) 58;
      numArray2[30] = (byte) 3;
      numArray2[27] = (byte) 177;
      numArray2[52] = (byte) 179;
      numArray2[15] = (byte) 220;
      numArray2[9] = (byte) 15;
      numArray2[4] = (byte) 95;
      numArray2[3] = (byte) 111;
      numArray2[12] = (byte) 6;
      numArray2[8] = (byte) 151;
      numArray2[14] = (byte) 121;
      numArray2[1] = (byte) 117;
      numArray2[24] = (byte) 136;
      numArray2[34] = (byte) 22;
      numArray2[40] = (byte) 37;
      numArray2[19] = (byte) 113;
      numArray2[35] = (byte) 249;
      numArray2[17] = (byte) 239;
      numArray2[22] = (byte) 83;
      numArray2[23] = (byte) 238;
      numArray2[26] = (byte) 62;
      numArray2[25] = (byte) 6;
      numArray2[21] = (byte) 25;
      numArray2[43] = (byte) 210;
      numArray2[10] = (byte) 156;
      numArray2[54] = (byte) 82;
      numArray2[7] = (byte) 186;
      numArray2[47] = (byte) 170;
      numArray2[32 /*0x20*/] = (byte) 10;
      numArray2[38] = (byte) 93;
      numArray2[49] = (byte) 22;
      numArray2[39] = (byte) 132;
      numArray2[36] = (byte) 219;
      numArray2[37] = (byte) 251;
      numArray2[6] = (byte) 216;
      numArray2[11] = (byte) 83;
      numArray2[16 /*0x10*/] = (byte) 31 /*0x1F*/;
      numArray2[41] = (byte) 196;
      numArray2[42] = (byte) 182;
      numArray2[31 /*0x1F*/] = (byte) 13;
      numArray2[28] = (byte) 206;
      numArray2[45] = (byte) 195;
      numArray2[46] = (byte) 155;
      numArray2[13] = (byte) 132;
      numArray2[48 /*0x30*/] = (byte) 190;
      numArray2[18] = (byte) 108;
      numArray2[50] = (byte) 37;
      numArray2[51] = (byte) 160 /*0xA0*/;
      numArray2[44] = (byte) 64 /*0x40*/;
      numArray2[29] = (byte) 224 /*0xE0*/;
      numArray2[20] = (byte) 151;
      byte[] numArray3 = new byte[55]
      {
        (byte) 248,
        (byte) 185,
        (byte) 135,
        (byte) 176 /*0xB0*/,
        (byte) 42,
        (byte) 109,
        (byte) 85,
        (byte) 136,
        (byte) 166,
        (byte) 221,
        (byte) 200,
        (byte) 206,
        byte.MaxValue,
        byte.MaxValue,
        (byte) 210,
        (byte) 221,
        (byte) 57,
        (byte) 161,
        (byte) 126,
        (byte) 214,
        (byte) 118,
        (byte) 250,
        (byte) 84,
        (byte) 150,
        (byte) 195,
        (byte) 195,
        (byte) 33,
        (byte) 30,
        (byte) 111,
        (byte) 82,
        (byte) 117,
        (byte) 4,
        (byte) 232,
        (byte) 97,
        (byte) 19,
        (byte) 172,
        (byte) 34,
        (byte) 124,
        (byte) 94,
        (byte) 216,
        (byte) 207,
        (byte) 142,
        (byte) 180,
        (byte) 222,
        (byte) 178,
        (byte) 92,
        (byte) 143,
        (byte) 210,
        (byte) 164,
        (byte) 29,
        (byte) 227,
        (byte) 109,
        (byte) 180,
        (byte) 123,
        (byte) 123
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[13];
      numArray4[11] = (byte) 113;
      numArray4[8] = (byte) 53;
      numArray4[3] = (byte) 2;
      numArray4[0] = (byte) 123;
      numArray4[4] = (byte) 52;
      numArray4[10] = (byte) 67;
      numArray4[2] = (byte) 236;
      numArray4[9] = (byte) 121;
      numArray4[6] = (byte) 251;
      numArray4[1] = (byte) 33;
      numArray4[12] = (byte) 169;
      numArray4[5] = (byte) 51;
      numArray4[7] = (byte) 169;
      byte[] numArray5 = new byte[13]
      {
        (byte) 109,
        (byte) 122,
        (byte) 34,
        (byte) 60,
        (byte) 168,
        (byte) 64 /*0x40*/,
        (byte) 34,
        (byte) 226,
        (byte) 168,
        (byte) 52,
        (byte) 28,
        (byte) 82,
        (byte) 95
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 13);
      for (int index = 0; index < 13; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[68];
    byte[] numArray7 = new byte[55]
    {
      (byte) 141,
      (byte) 225,
      (byte) 57,
      (byte) 69,
      (byte) 53,
      (byte) 54,
      (byte) 244,
      (byte) 24,
      (byte) 235,
      (byte) 201,
      (byte) 164,
      (byte) 3,
      (byte) 29,
      (byte) 123,
      (byte) 1,
      (byte) 91,
      (byte) 203,
      (byte) 148,
      (byte) 199,
      (byte) 243,
      (byte) 130,
      (byte) 210,
      (byte) 159,
      (byte) 131,
      (byte) 83,
      (byte) 231,
      (byte) 128 /*0x80*/,
      (byte) 214,
      (byte) 32 /*0x20*/,
      (byte) 170,
      (byte) 127 /*0x7F*/,
      (byte) 144 /*0x90*/,
      (byte) 65,
      (byte) 85,
      (byte) 168,
      (byte) 152,
      (byte) 10,
      (byte) 43,
      (byte) 116,
      (byte) 22,
      (byte) 26,
      (byte) 115,
      (byte) 14,
      (byte) 132,
      (byte) 86,
      (byte) 96 /*0x60*/,
      (byte) 156,
      (byte) 127 /*0x7F*/,
      (byte) 73,
      (byte) 112 /*0x70*/,
      (byte) 140,
      (byte) 31 /*0x1F*/,
      (byte) 218,
      (byte) 198,
      (byte) 209
    };
    byte[] numArray8 = new byte[55]
    {
      (byte) 88,
      (byte) 139,
      (byte) 169,
      (byte) 59,
      (byte) 171,
      (byte) 194,
      (byte) 29,
      (byte) 157,
      (byte) 125,
      (byte) 45,
      (byte) 92,
      (byte) 204,
      (byte) 207,
      (byte) 68,
      (byte) 134,
      (byte) 159,
      (byte) 151,
      (byte) 234,
      (byte) 232,
      (byte) 74,
      (byte) 18,
      (byte) 93,
      (byte) 28,
      (byte) 21,
      (byte) 113,
      (byte) 77,
      (byte) 45,
      (byte) 201,
      (byte) 113,
      (byte) 74,
      (byte) 33,
      (byte) 162,
      (byte) 6,
      (byte) 11,
      (byte) 32 /*0x20*/,
      (byte) 14,
      (byte) 12,
      (byte) 237,
      (byte) 73,
      (byte) 60,
      (byte) 247,
      (byte) 232,
      (byte) 133,
      (byte) 222,
      (byte) 96 /*0x60*/,
      (byte) 161,
      (byte) 72,
      (byte) 136,
      (byte) 126,
      (byte) 89,
      (byte) 186,
      (byte) 142,
      (byte) 190,
      (byte) 172,
      (byte) 99
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[13];
    numArray9[10] = (byte) 200;
    numArray9[12] = (byte) 104;
    numArray9[4] = (byte) 29;
    numArray9[3] = (byte) 182;
    numArray9[5] = (byte) 247;
    numArray9[11] = (byte) 211;
    numArray9[1] = (byte) 242;
    numArray9[7] = (byte) 73;
    numArray9[8] = (byte) 20;
    numArray9[9] = (byte) 193;
    numArray9[0] = (byte) 181;
    numArray9[2] = (byte) 78;
    numArray9[6] = (byte) 64 /*0x40*/;
    byte[] numArray10 = new byte[13]
    {
      (byte) 240 /*0xF0*/,
      (byte) 237,
      (byte) 9,
      (byte) 205,
      (byte) 192 /*0xC0*/,
      (byte) 88,
      (byte) 184,
      (byte) 151,
      (byte) 179,
      (byte) 215,
      (byte) 90,
      (byte) 142,
      (byte) 19
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 13);
    for (int index = 0; index < 13; ++index)
      numArray6[index + 55] ^= numArray10[index];
    byte[] numArray11 = new byte[37];
    byte[] response = new byte[37];
    Array.Copy((Array) sc_12699.sspq, 0, (Array) numArray11, 0, 37);
    key.Query(true, 335, numArray11, response);
    Array.Copy((Array) sc_12699.sspr, 0, (Array) numArray11, 0, 37);
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
}
