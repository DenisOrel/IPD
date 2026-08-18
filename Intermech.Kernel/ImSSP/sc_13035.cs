// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_13035
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_13035
{
  private static byte[] sspq = new byte[81]
  {
    (byte) 201,
    (byte) 66,
    (byte) 172,
    (byte) 192 /*0xC0*/,
    (byte) 143,
    (byte) 198,
    (byte) 47,
    (byte) 150,
    (byte) 110,
    (byte) 99,
    (byte) 197,
    (byte) 111,
    (byte) 25,
    (byte) 73,
    (byte) 82,
    (byte) 102,
    (byte) 196,
    (byte) 218,
    (byte) 74,
    (byte) 10,
    (byte) 221,
    (byte) 234,
    (byte) 88,
    (byte) 219,
    (byte) 18,
    (byte) 136,
    (byte) 35,
    (byte) 190,
    (byte) 93,
    (byte) 21,
    (byte) 222,
    (byte) 8,
    (byte) 210,
    (byte) 51,
    (byte) 36,
    (byte) 78,
    (byte) 80 /*0x50*/,
    (byte) 109,
    (byte) 70,
    (byte) 79,
    (byte) 170,
    (byte) 51,
    (byte) 98,
    (byte) 174,
    (byte) 190,
    (byte) 162,
    (byte) 15,
    (byte) 158,
    (byte) 116,
    (byte) 250,
    (byte) 140,
    (byte) 24,
    (byte) 60,
    (byte) 136,
    (byte) 83,
    (byte) 0,
    (byte) 136,
    (byte) 103,
    (byte) 153,
    (byte) 212,
    (byte) 17,
    (byte) 185,
    (byte) 69,
    (byte) 54,
    (byte) 98,
    (byte) 31 /*0x1F*/,
    (byte) 43,
    (byte) 172,
    (byte) 202,
    (byte) 88,
    (byte) 235,
    (byte) 154,
    (byte) 127 /*0x7F*/,
    (byte) 79,
    (byte) 47,
    (byte) 51,
    (byte) 25,
    (byte) 95,
    (byte) 147,
    (byte) 83,
    (byte) 249
  };
  private static byte[] sspr = new byte[81]
  {
    (byte) 51,
    (byte) 93,
    (byte) 143,
    (byte) 222,
    (byte) 163,
    (byte) 10,
    (byte) 64 /*0x40*/,
    (byte) 105,
    (byte) 131,
    (byte) 109,
    (byte) 93,
    (byte) 168,
    (byte) 220,
    (byte) 92,
    (byte) 92,
    (byte) 69,
    (byte) 115,
    (byte) 104,
    (byte) 24,
    (byte) 199,
    (byte) 71,
    (byte) 208 /*0xD0*/,
    (byte) 250,
    (byte) 94,
    (byte) 171,
    (byte) 226,
    (byte) 65,
    (byte) 125,
    (byte) 253,
    (byte) 104,
    (byte) 224 /*0xE0*/,
    (byte) 93,
    (byte) 246,
    (byte) 202,
    (byte) 237,
    (byte) 109,
    (byte) 206,
    (byte) 109,
    (byte) 118,
    (byte) 118,
    (byte) 80 /*0x50*/,
    (byte) 201,
    (byte) 191,
    (byte) 36,
    (byte) 166,
    (byte) 226,
    (byte) 151,
    (byte) 152,
    (byte) 247,
    (byte) 208 /*0xD0*/,
    (byte) 247,
    (byte) 167,
    (byte) 156,
    (byte) 125,
    (byte) 116,
    (byte) 228,
    (byte) 101,
    (byte) 223,
    (byte) 27,
    (byte) 171,
    (byte) 47,
    (byte) 238,
    (byte) 235,
    (byte) 143,
    (byte) 34,
    (byte) 228,
    (byte) 93,
    (byte) 128 /*0x80*/,
    (byte) 21,
    (byte) 52,
    (byte) 164,
    (byte) 161,
    (byte) 144 /*0x90*/,
    (byte) 163,
    (byte) 219,
    (byte) 118,
    (byte) 72,
    (byte) 224 /*0xE0*/,
    (byte) 215,
    (byte) 252,
    (byte) 172
  };

  internal static string ssp_appserver_13036()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[52];
      byte[] numArray2 = new byte[52]
      {
        (byte) 152,
        (byte) 172,
        (byte) 237,
        (byte) 26,
        (byte) 185,
        (byte) 19,
        (byte) 23,
        (byte) 169,
        (byte) 89,
        byte.MaxValue,
        (byte) 193,
        (byte) 70,
        (byte) 77,
        (byte) 46,
        (byte) 171,
        (byte) 159,
        (byte) 224 /*0xE0*/,
        (byte) 250,
        (byte) 174,
        (byte) 134,
        (byte) 178,
        (byte) 7,
        (byte) 10,
        (byte) 35,
        (byte) 156,
        (byte) 177,
        (byte) 5,
        (byte) 41,
        (byte) 81,
        (byte) 40,
        (byte) 165,
        (byte) 88,
        (byte) 21,
        (byte) 238,
        (byte) 57,
        (byte) 184,
        (byte) 203,
        (byte) 35,
        (byte) 161,
        (byte) 237,
        (byte) 243,
        (byte) 170,
        (byte) 39,
        (byte) 111,
        (byte) 107,
        (byte) 44,
        (byte) 50,
        (byte) 182,
        (byte) 242,
        (byte) 65,
        (byte) 189,
        (byte) 123
      };
      byte[] numArray3 = new byte[52]
      {
        (byte) 68,
        (byte) 120,
        (byte) 188,
        (byte) 133,
        (byte) 153,
        (byte) 150,
        (byte) 178,
        (byte) 150,
        (byte) 49,
        (byte) 117,
        (byte) 165,
        (byte) 8,
        (byte) 131,
        (byte) 223,
        (byte) 116,
        (byte) 69,
        (byte) 217,
        (byte) 131,
        (byte) 211,
        (byte) 20,
        (byte) 108,
        (byte) 69,
        (byte) 157,
        (byte) 45,
        (byte) 113,
        (byte) 121,
        (byte) 128 /*0x80*/,
        (byte) 150,
        (byte) 110,
        (byte) 3,
        (byte) 153,
        (byte) 101,
        (byte) 233,
        (byte) 48 /*0x30*/,
        (byte) 48 /*0x30*/,
        (byte) 150,
        (byte) 232,
        (byte) 208 /*0xD0*/,
        (byte) 132,
        (byte) 42,
        (byte) 85,
        (byte) 83,
        (byte) 204,
        (byte) 20,
        (byte) 97,
        (byte) 61,
        (byte) 84,
        (byte) 153,
        (byte) 252,
        (byte) 233,
        (byte) 150,
        (byte) 92
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 52);
      for (int index = 0; index < 52; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[52];
    byte[] numArray5 = new byte[52];
    numArray5[36] = (byte) 1;
    numArray5[47] = (byte) 193;
    numArray5[37] = (byte) 164;
    numArray5[15] = (byte) 211;
    numArray5[4] = (byte) 8;
    numArray5[5] = (byte) 13;
    numArray5[6] = (byte) 238;
    numArray5[38] = (byte) 153;
    numArray5[32 /*0x20*/] = (byte) 215;
    numArray5[9] = (byte) 89;
    numArray5[10] = (byte) 104;
    numArray5[48 /*0x30*/] = (byte) 0;
    numArray5[12] = (byte) 95;
    numArray5[11] = (byte) 48 /*0x30*/;
    numArray5[40] = (byte) 4;
    numArray5[17] = (byte) 88;
    numArray5[23] = (byte) 183;
    numArray5[39] = (byte) 224 /*0xE0*/;
    numArray5[46] = (byte) 210;
    numArray5[49] = (byte) 109;
    numArray5[20] = (byte) 7;
    numArray5[21] = (byte) 223;
    numArray5[22] = (byte) 155;
    numArray5[2] = (byte) 201;
    numArray5[24] = (byte) 111;
    numArray5[7] = (byte) 88;
    numArray5[43] = (byte) 21;
    numArray5[27] = (byte) 54;
    numArray5[28] = (byte) 16 /*0x10*/;
    numArray5[18] = (byte) 192 /*0xC0*/;
    numArray5[19] = (byte) 147;
    numArray5[31 /*0x1F*/] = (byte) 60;
    numArray5[34] = (byte) 168;
    numArray5[33] = (byte) 64 /*0x40*/;
    numArray5[30] = (byte) 42;
    numArray5[1] = (byte) 161;
    numArray5[8] = (byte) 48 /*0x30*/;
    numArray5[26] = (byte) 78;
    numArray5[0] = (byte) 144 /*0x90*/;
    numArray5[3] = (byte) 126;
    numArray5[35] = (byte) 22;
    numArray5[41] = (byte) 82;
    numArray5[42] = (byte) 247;
    numArray5[45] = (byte) 251;
    numArray5[25] = (byte) 127 /*0x7F*/;
    numArray5[16 /*0x10*/] = (byte) 189;
    numArray5[51] = (byte) 34;
    numArray5[14] = (byte) 242;
    numArray5[44] = (byte) 3;
    numArray5[13] = (byte) 245;
    numArray5[50] = (byte) 0;
    numArray5[29] = (byte) 106;
    byte[] numArray6 = new byte[52]
    {
      (byte) 211,
      (byte) 64 /*0x40*/,
      (byte) 140,
      (byte) 224 /*0xE0*/,
      (byte) 15,
      (byte) 43,
      (byte) 224 /*0xE0*/,
      (byte) 230,
      (byte) 140,
      (byte) 139,
      (byte) 107,
      (byte) 159,
      (byte) 44,
      (byte) 10,
      (byte) 97,
      (byte) 133,
      (byte) 92,
      (byte) 31 /*0x1F*/,
      (byte) 181,
      (byte) 50,
      (byte) 66,
      (byte) 227,
      (byte) 220,
      (byte) 203,
      (byte) 140,
      (byte) 28,
      (byte) 216,
      (byte) 27,
      (byte) 202,
      (byte) 80 /*0x50*/,
      (byte) 90,
      (byte) 35,
      (byte) 135,
      (byte) 113,
      (byte) 49,
      (byte) 49,
      (byte) 121,
      (byte) 50,
      (byte) 56,
      (byte) 78,
      (byte) 165,
      (byte) 93,
      (byte) 94,
      (byte) 112 /*0x70*/,
      (byte) 135,
      (byte) 170,
      (byte) 207,
      (byte) 207,
      (byte) 111,
      (byte) 123,
      (byte) 110,
      (byte) 122
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 52);
    for (int index = 0; index < 52; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static int ssp_appserver_13037(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 241,
      (byte) 200,
      (byte) 128 /*0x80*/,
      (byte) 93,
      (byte) 154,
      (byte) 221,
      (byte) 49,
      (byte) 130,
      (byte) 164,
      (byte) 91,
      (byte) 39,
      (byte) 75,
      (byte) 44,
      (byte) 132,
      (byte) 85,
      (byte) 203,
      (byte) 226,
      (byte) 130,
      (byte) 175,
      (byte) 190,
      (byte) 67,
      (byte) 169,
      (byte) 182,
      (byte) 158,
      (byte) 221,
      (byte) 210,
      (byte) 248,
      (byte) 24,
      (byte) 17,
      (byte) 7,
      (byte) 130,
      (byte) 163,
      (byte) 101,
      (byte) 17,
      (byte) 168,
      (byte) 46,
      (byte) 143,
      (byte) 146,
      (byte) 252,
      (byte) 50,
      (byte) 239,
      (byte) 201,
      (byte) 102,
      (byte) 214,
      (byte) 191,
      (byte) 139,
      (byte) 117,
      (byte) 48 /*0x30*/
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 85,
      (byte) 229,
      (byte) 143,
      (byte) 110,
      (byte) 89,
      (byte) 93,
      (byte) 24,
      (byte) 141,
      (byte) 64 /*0x40*/,
      (byte) 10,
      (byte) 166,
      (byte) 176 /*0xB0*/,
      (byte) 185,
      (byte) 9,
      (byte) 200,
      (byte) 185,
      (byte) 149,
      (byte) 34,
      (byte) 223,
      (byte) 119,
      (byte) 128 /*0x80*/,
      (byte) 102,
      (byte) 215,
      (byte) 208 /*0xD0*/,
      (byte) 98,
      (byte) 230,
      (byte) 206,
      (byte) 216,
      (byte) 18,
      (byte) 66,
      (byte) 185,
      (byte) 243,
      (byte) 60,
      (byte) 55,
      (byte) 34,
      (byte) 196,
      (byte) 113,
      (byte) 47,
      (byte) 192 /*0xC0*/,
      (byte) 216,
      (byte) 225,
      (byte) 120,
      (byte) 146,
      (byte) 1,
      (byte) 95,
      (byte) 119,
      (byte) 4,
      (byte) 232
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_13038()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[143];
      byte[] numArray2 = new byte[55]
      {
        (byte) 179,
        (byte) 188,
        (byte) 216,
        (byte) 72,
        (byte) 17,
        (byte) 226,
        (byte) 127 /*0x7F*/,
        (byte) 199,
        (byte) 229,
        (byte) 25,
        (byte) 112 /*0x70*/,
        (byte) 157,
        (byte) 217,
        (byte) 131,
        (byte) 229,
        (byte) 27,
        (byte) 219,
        (byte) 38,
        (byte) 59,
        (byte) 79,
        (byte) 134,
        (byte) 125,
        (byte) 207,
        (byte) 121,
        (byte) 67,
        (byte) 224 /*0xE0*/,
        (byte) 223,
        (byte) 82,
        (byte) 93,
        (byte) 199,
        (byte) 248,
        (byte) 184,
        (byte) 78,
        (byte) 28,
        (byte) 240 /*0xF0*/,
        (byte) 169,
        (byte) 56,
        (byte) 96 /*0x60*/,
        (byte) 29,
        (byte) 173,
        (byte) 18,
        (byte) 52,
        (byte) 77,
        (byte) 213,
        (byte) 89,
        (byte) 200,
        (byte) 253,
        (byte) 59,
        (byte) 165,
        (byte) 90,
        (byte) 84,
        (byte) 249,
        (byte) 29,
        (byte) 105,
        (byte) 223
      };
      byte[] numArray3 = new byte[55];
      numArray3[49] = (byte) 80 /*0x50*/;
      numArray3[24] = (byte) 129;
      numArray3[11] = (byte) 175;
      numArray3[54] = (byte) 157;
      numArray3[26] = (byte) 216;
      numArray3[3] = (byte) 46;
      numArray3[23] = (byte) 72;
      numArray3[7] = (byte) 217;
      numArray3[8] = (byte) 228;
      numArray3[10] = (byte) 34;
      numArray3[32 /*0x20*/] = (byte) 95;
      numArray3[36] = (byte) 228;
      numArray3[51] = (byte) 111;
      numArray3[13] = (byte) 64 /*0x40*/;
      numArray3[14] = (byte) 14;
      numArray3[15] = (byte) 148;
      numArray3[16 /*0x10*/] = (byte) 106;
      numArray3[27] = (byte) 150;
      numArray3[18] = (byte) 185;
      numArray3[22] = (byte) 79;
      numArray3[12] = (byte) 13;
      numArray3[21] = (byte) 26;
      numArray3[47] = (byte) 230;
      numArray3[0] = (byte) 205;
      numArray3[29] = (byte) 88;
      numArray3[42] = (byte) 132;
      numArray3[38] = (byte) 134;
      numArray3[6] = (byte) 253;
      numArray3[19] = (byte) 95;
      numArray3[5] = (byte) 165;
      numArray3[30] = (byte) 84;
      numArray3[31 /*0x1F*/] = (byte) 201;
      numArray3[28] = (byte) 1;
      numArray3[33] = (byte) 190;
      numArray3[43] = (byte) 36;
      numArray3[17] = (byte) 166;
      numArray3[1] = (byte) 71;
      numArray3[34] = (byte) 20;
      numArray3[40] = (byte) 83;
      numArray3[39] = (byte) 116;
      numArray3[4] = (byte) 248;
      numArray3[41] = (byte) 210;
      numArray3[37] = (byte) 51;
      numArray3[2] = (byte) 234;
      numArray3[44] = (byte) 144 /*0x90*/;
      numArray3[45] = (byte) 96 /*0x60*/;
      numArray3[46] = (byte) 47;
      numArray3[9] = (byte) 181;
      numArray3[48 /*0x30*/] = (byte) 74;
      numArray3[35] = (byte) 84;
      numArray3[50] = byte.MaxValue;
      numArray3[20] = (byte) 153;
      numArray3[52] = (byte) 215;
      numArray3[53] = (byte) 98;
      numArray3[25] = (byte) 169;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55];
      numArray4[27] = (byte) 153;
      numArray4[32 /*0x20*/] = (byte) 179;
      numArray4[2] = (byte) 78;
      numArray4[16 /*0x10*/] = (byte) 226;
      numArray4[4] = (byte) 128 /*0x80*/;
      numArray4[5] = (byte) 102;
      numArray4[9] = (byte) 105;
      numArray4[7] = (byte) 33;
      numArray4[8] = (byte) 28;
      numArray4[15] = (byte) 29;
      numArray4[17] = (byte) 87;
      numArray4[25] = (byte) 67;
      numArray4[12] = (byte) 128 /*0x80*/;
      numArray4[31 /*0x1F*/] = (byte) 174;
      numArray4[33] = (byte) 201;
      numArray4[19] = (byte) 0;
      numArray4[52] = (byte) 7;
      numArray4[0] = (byte) 62;
      numArray4[3] = (byte) 93;
      numArray4[1] = (byte) 138;
      numArray4[20] = (byte) 69;
      numArray4[21] = (byte) 125;
      numArray4[22] = (byte) 0;
      numArray4[47] = (byte) 111;
      numArray4[24] = (byte) 253;
      numArray4[53] = (byte) 6;
      numArray4[26] = (byte) 139;
      numArray4[23] = (byte) 162;
      numArray4[46] = (byte) 232;
      numArray4[10] = (byte) 127 /*0x7F*/;
      numArray4[30] = (byte) 184;
      numArray4[28] = (byte) 15;
      numArray4[29] = (byte) 10;
      numArray4[38] = (byte) 100;
      numArray4[34] = (byte) 155;
      numArray4[35] = (byte) 208 /*0xD0*/;
      numArray4[36] = (byte) 157;
      numArray4[37] = (byte) 77;
      numArray4[11] = (byte) 217;
      numArray4[39] = (byte) 99;
      numArray4[40] = (byte) 133;
      numArray4[50] = (byte) 173;
      numArray4[42] = (byte) 133;
      numArray4[43] = (byte) 16 /*0x10*/;
      numArray4[18] = (byte) 134;
      numArray4[45] = (byte) 205;
      numArray4[14] = (byte) 31 /*0x1F*/;
      numArray4[41] = (byte) 147;
      numArray4[13] = (byte) 8;
      numArray4[44] = (byte) 192 /*0xC0*/;
      numArray4[49] = (byte) 210;
      numArray4[51] = (byte) 213;
      numArray4[6] = (byte) 170;
      numArray4[48 /*0x30*/] = (byte) 186;
      numArray4[54] = (byte) 173;
      byte[] numArray5 = new byte[55]
      {
        (byte) 91,
        (byte) 227,
        (byte) 70,
        (byte) 248,
        (byte) 125,
        (byte) 80 /*0x50*/,
        (byte) 161,
        (byte) 254,
        (byte) 75,
        (byte) 226,
        (byte) 157,
        (byte) 89,
        (byte) 28,
        (byte) 157,
        (byte) 15,
        (byte) 63 /*0x3F*/,
        (byte) 44,
        (byte) 163,
        (byte) 51,
        (byte) 125,
        (byte) 217,
        (byte) 27,
        (byte) 116,
        (byte) 251,
        (byte) 9,
        (byte) 33,
        (byte) 0,
        (byte) 31 /*0x1F*/,
        (byte) 39,
        (byte) 212,
        (byte) 218,
        (byte) 15,
        (byte) 162,
        (byte) 107,
        (byte) 147,
        (byte) 182,
        (byte) 178,
        (byte) 36,
        (byte) 36,
        (byte) 80 /*0x50*/,
        (byte) 196,
        (byte) 208 /*0xD0*/,
        (byte) 237,
        (byte) 2,
        (byte) 31 /*0x1F*/,
        (byte) 123,
        (byte) 136,
        (byte) 216,
        (byte) 173,
        (byte) 48 /*0x30*/,
        (byte) 95,
        (byte) 47,
        (byte) 119,
        (byte) 35,
        (byte) 154
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[33]
      {
        (byte) 201,
        (byte) 49,
        (byte) 225,
        (byte) 190,
        (byte) 50,
        (byte) 212,
        (byte) 180,
        (byte) 207,
        (byte) 41,
        (byte) 120,
        (byte) 155,
        (byte) 147,
        (byte) 8,
        (byte) 141,
        (byte) 168,
        (byte) 202,
        (byte) 154,
        (byte) 5,
        (byte) 47,
        (byte) 132,
        (byte) 145,
        (byte) 242,
        (byte) 135,
        (byte) 41,
        (byte) 26,
        (byte) 115,
        (byte) 234,
        (byte) 69,
        (byte) 254,
        (byte) 195,
        (byte) 33,
        (byte) 31 /*0x1F*/,
        (byte) 17
      };
      byte[] numArray7 = new byte[33]
      {
        (byte) 196,
        (byte) 230,
        (byte) 3,
        (byte) 45,
        (byte) 24,
        (byte) 114,
        (byte) 159,
        (byte) 97,
        (byte) 34,
        (byte) 152,
        (byte) 114,
        (byte) 89,
        (byte) 144 /*0x90*/,
        (byte) 17,
        (byte) 137,
        (byte) 136,
        (byte) 96 /*0x60*/,
        (byte) 116,
        (byte) 194,
        (byte) 29,
        (byte) 125,
        (byte) 243,
        (byte) 178,
        (byte) 143,
        (byte) 95,
        (byte) 110,
        (byte) 14,
        (byte) 0,
        byte.MaxValue,
        (byte) 157,
        (byte) 111,
        (byte) 40,
        (byte) 98
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 33);
      for (int index = 0; index < 33; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[14];
      byte[] response = new byte[14];
      Array.Copy((Array) sc_13035.sspq, 0, (Array) numArray8, 0, 14);
      key.Query(true, 335, numArray8, response);
      Array.Copy((Array) sc_13035.sspr, 0, (Array) numArray8, 0, 14);
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
    byte[] numArray9 = new byte[143];
    byte[] numArray10 = new byte[55];
    numArray10[34] = (byte) 154;
    numArray10[1] = (byte) 204;
    numArray10[15] = (byte) 129;
    numArray10[0] = (byte) 134;
    numArray10[25] = (byte) 44;
    numArray10[49] = (byte) 237;
    numArray10[6] = (byte) 183;
    numArray10[7] = (byte) 164;
    numArray10[8] = (byte) 190;
    numArray10[14] = (byte) 124;
    numArray10[53] = (byte) 149;
    numArray10[9] = (byte) 234;
    numArray10[17] = (byte) 100;
    numArray10[13] = (byte) 86;
    numArray10[22] = (byte) 131;
    numArray10[33] = (byte) 121;
    numArray10[46] = (byte) 95;
    numArray10[19] = (byte) 132;
    numArray10[18] = (byte) 238;
    numArray10[37] = (byte) 163;
    numArray10[20] = (byte) 209;
    numArray10[21] = (byte) 194;
    numArray10[39] = (byte) 16 /*0x10*/;
    numArray10[45] = (byte) 191;
    numArray10[24] = (byte) 1;
    numArray10[54] = (byte) 120;
    numArray10[2] = (byte) 68;
    numArray10[5] = (byte) 170;
    numArray10[44] = (byte) 81;
    numArray10[12] = (byte) 102;
    numArray10[23] = (byte) 195;
    numArray10[11] = (byte) 208 /*0xD0*/;
    numArray10[32 /*0x20*/] = (byte) 229;
    numArray10[4] = (byte) 214;
    numArray10[48 /*0x30*/] = (byte) 25;
    numArray10[35] = (byte) 82;
    numArray10[36] = (byte) 205;
    numArray10[10] = (byte) 110;
    numArray10[16 /*0x10*/] = (byte) 92;
    numArray10[47] = (byte) 217;
    numArray10[40] = (byte) 142;
    numArray10[41] = (byte) 36;
    numArray10[42] = (byte) 132;
    numArray10[26] = (byte) 87;
    numArray10[27] = (byte) 147;
    numArray10[31 /*0x1F*/] = (byte) 213;
    numArray10[51] = (byte) 97;
    numArray10[3] = (byte) 8;
    numArray10[30] = (byte) 37;
    numArray10[29] = (byte) 16 /*0x10*/;
    numArray10[50] = (byte) 6;
    numArray10[43] = (byte) 194;
    numArray10[52] = (byte) 129;
    numArray10[28] = (byte) 171;
    numArray10[38] = (byte) 82;
    byte[] numArray11 = new byte[55];
    numArray11[39] = (byte) 132;
    numArray11[54] = (byte) 204;
    numArray11[2] = (byte) 76;
    numArray11[3] = (byte) 132;
    numArray11[42] = (byte) 179;
    numArray11[12] = (byte) 7;
    numArray11[6] = (byte) 50;
    numArray11[10] = (byte) 6;
    numArray11[29] = (byte) 133;
    numArray11[9] = (byte) 156;
    numArray11[25] = (byte) 46;
    numArray11[30] = (byte) 54;
    numArray11[53] = (byte) 245;
    numArray11[13] = (byte) 244;
    numArray11[50] = (byte) 234;
    numArray11[20] = (byte) 9;
    numArray11[37] = (byte) 224 /*0xE0*/;
    numArray11[17] = (byte) 120;
    numArray11[46] = (byte) 101;
    numArray11[43] = (byte) 211;
    numArray11[14] = (byte) 59;
    numArray11[51] = (byte) 66;
    numArray11[24] = (byte) 147;
    numArray11[1] = (byte) 99;
    numArray11[8] = (byte) 115;
    numArray11[4] = (byte) 191;
    numArray11[26] = (byte) 121;
    numArray11[7] = (byte) 42;
    numArray11[28] = (byte) 36;
    numArray11[40] = (byte) 220;
    numArray11[16 /*0x10*/] = (byte) 56;
    numArray11[44] = (byte) 39;
    numArray11[32 /*0x20*/] = (byte) 243;
    numArray11[36] = (byte) 49;
    numArray11[34] = (byte) 152;
    numArray11[33] = (byte) 47;
    numArray11[22] = (byte) 225;
    numArray11[31 /*0x1F*/] = (byte) 27;
    numArray11[38] = (byte) 65;
    numArray11[11] = (byte) 202;
    numArray11[23] = (byte) 206;
    numArray11[41] = (byte) 120;
    numArray11[35] = (byte) 127 /*0x7F*/;
    numArray11[19] = (byte) 93;
    numArray11[47] = (byte) 246;
    numArray11[45] = (byte) 140;
    numArray11[15] = (byte) 74;
    numArray11[27] = (byte) 74;
    numArray11[48 /*0x30*/] = (byte) 100;
    numArray11[49] = (byte) 105;
    numArray11[18] = (byte) 109;
    numArray11[5] = (byte) 63 /*0x3F*/;
    numArray11[52] = (byte) 120;
    numArray11[21] = (byte) 33;
    numArray11[0] = (byte) 49;
    key.Query(true, 335, numArray10, numArray10);
    Array.Copy((Array) numArray10, 0, (Array) numArray9, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray9[index] ^= numArray11[index];
    byte[] numArray12 = new byte[55]
    {
      (byte) 83,
      (byte) 87,
      (byte) 133,
      (byte) 114,
      (byte) 33,
      (byte) 181,
      (byte) 233,
      (byte) 140,
      (byte) 147,
      (byte) 232,
      (byte) 180,
      (byte) 149,
      (byte) 157,
      (byte) 40,
      (byte) 70,
      (byte) 160 /*0xA0*/,
      (byte) 51,
      (byte) 204,
      (byte) 142,
      (byte) 54,
      (byte) 200,
      (byte) 70,
      (byte) 49,
      (byte) 207,
      (byte) 40,
      (byte) 183,
      (byte) 179,
      (byte) 138,
      (byte) 143,
      (byte) 43,
      (byte) 158,
      (byte) 235,
      (byte) 48 /*0x30*/,
      (byte) 190,
      (byte) 234,
      (byte) 126,
      (byte) 67,
      (byte) 119,
      (byte) 158,
      (byte) 60,
      (byte) 71,
      (byte) 111,
      (byte) 224 /*0xE0*/,
      (byte) 83,
      (byte) 212,
      (byte) 162,
      (byte) 52,
      (byte) 168,
      (byte) 234,
      (byte) 69,
      (byte) 81,
      (byte) 70,
      (byte) 188,
      (byte) 232,
      (byte) 250
    };
    byte[] numArray13 = new byte[55]
    {
      (byte) 83,
      (byte) 126,
      (byte) 81,
      (byte) 81,
      (byte) 43,
      (byte) 136,
      (byte) 82,
      (byte) 5,
      (byte) 165,
      (byte) 214,
      (byte) 37,
      (byte) 249,
      (byte) 138,
      (byte) 151,
      (byte) 163,
      (byte) 35,
      (byte) 61,
      (byte) 44,
      (byte) 121,
      (byte) 95,
      (byte) 25,
      (byte) 241,
      (byte) 95,
      (byte) 76,
      (byte) 43,
      (byte) 30,
      (byte) 29,
      (byte) 164,
      (byte) 192 /*0xC0*/,
      (byte) 82,
      (byte) 86,
      (byte) 67,
      (byte) 224 /*0xE0*/,
      (byte) 235,
      (byte) 93,
      (byte) 112 /*0x70*/,
      (byte) 222,
      (byte) 113,
      (byte) 199,
      (byte) 104,
      (byte) 185,
      (byte) 76,
      (byte) 180,
      (byte) 203,
      (byte) 97,
      (byte) 98,
      (byte) 213,
      (byte) 96 /*0x60*/,
      (byte) 72,
      (byte) 168,
      (byte) 59,
      (byte) 189,
      (byte) 134,
      (byte) 228,
      (byte) 116
    };
    key.Query(true, 335, numArray12, numArray12);
    Array.Copy((Array) numArray12, 0, (Array) numArray9, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray9[index + 55] ^= numArray13[index];
    byte[] numArray14 = new byte[33]
    {
      (byte) 181,
      (byte) 74,
      (byte) 105,
      (byte) 1,
      (byte) 94,
      (byte) 253,
      (byte) 104,
      (byte) 73,
      (byte) 201,
      (byte) 101,
      (byte) 222,
      (byte) 62,
      (byte) 199,
      (byte) 92,
      (byte) 188,
      (byte) 45,
      (byte) 81,
      (byte) 239,
      (byte) 88,
      (byte) 102,
      (byte) 222,
      (byte) 251,
      (byte) 133,
      (byte) 131,
      (byte) 10,
      (byte) 160 /*0xA0*/,
      (byte) 73,
      (byte) 218,
      (byte) 88,
      (byte) 46,
      (byte) 72,
      (byte) 195,
      (byte) 221
    };
    byte[] numArray15 = new byte[33];
    numArray15[27] = (byte) 196;
    numArray15[21] = (byte) 102;
    numArray15[2] = (byte) 246;
    numArray15[8] = (byte) 237;
    numArray15[20] = (byte) 200;
    numArray15[5] = (byte) 67;
    numArray15[6] = (byte) 160 /*0xA0*/;
    numArray15[7] = (byte) 191;
    numArray15[13] = (byte) 153;
    numArray15[30] = (byte) 151;
    numArray15[0] = (byte) 19;
    numArray15[28] = (byte) 180;
    numArray15[12] = (byte) 147;
    numArray15[3] = (byte) 117;
    numArray15[14] = (byte) 134;
    numArray15[15] = (byte) 228;
    numArray15[24] = (byte) 142;
    numArray15[18] = (byte) 91;
    numArray15[17] = (byte) 197;
    numArray15[19] = (byte) 128 /*0x80*/;
    numArray15[22] = (byte) 29;
    numArray15[26] = (byte) 180;
    numArray15[16 /*0x10*/] = (byte) 199;
    numArray15[23] = (byte) 229;
    numArray15[10] = (byte) 103;
    numArray15[25] = (byte) 3;
    numArray15[11] = (byte) 154;
    numArray15[1] = (byte) 186;
    numArray15[32 /*0x20*/] = (byte) 96 /*0x60*/;
    numArray15[29] = (byte) 17;
    numArray15[31 /*0x1F*/] = (byte) 192 /*0xC0*/;
    numArray15[9] = (byte) 170;
    numArray15[4] = (byte) 212;
    key.Query(true, 335, numArray14, numArray14);
    Array.Copy((Array) numArray14, 0, (Array) numArray9, 110, 33);
    for (int index = 0; index < 33; ++index)
      numArray9[index + 110] ^= numArray15[index];
    return Encoding.UTF8.GetString(numArray9);
  }

  internal static string ssp_appserver_13039()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[211];
      byte[] numArray2 = new byte[55];
      numArray2[21] = (byte) 79;
      numArray2[1] = (byte) 23;
      numArray2[11] = (byte) 97;
      numArray2[38] = (byte) 184;
      numArray2[17] = (byte) 54;
      numArray2[15] = (byte) 80 /*0x50*/;
      numArray2[25] = (byte) 233;
      numArray2[7] = (byte) 71;
      numArray2[4] = (byte) 238;
      numArray2[8] = (byte) 46;
      numArray2[3] = (byte) 230;
      numArray2[36] = (byte) 241;
      numArray2[12] = (byte) 69;
      numArray2[13] = (byte) 63 /*0x3F*/;
      numArray2[0] = (byte) 248;
      numArray2[35] = (byte) 83;
      numArray2[18] = (byte) 16 /*0x10*/;
      numArray2[40] = (byte) 203;
      numArray2[33] = (byte) 5;
      numArray2[19] = (byte) 210;
      numArray2[20] = (byte) 103;
      numArray2[28] = (byte) 32 /*0x20*/;
      numArray2[22] = (byte) 67;
      numArray2[23] = (byte) 42;
      numArray2[24] = (byte) 191;
      numArray2[53] = (byte) 252;
      numArray2[16 /*0x10*/] = (byte) 33;
      numArray2[43] = (byte) 87;
      numArray2[5] = (byte) 212;
      numArray2[14] = (byte) 195;
      numArray2[30] = (byte) 10;
      numArray2[31 /*0x1F*/] = (byte) 179;
      numArray2[32 /*0x20*/] = (byte) 25;
      numArray2[9] = (byte) 159;
      numArray2[34] = (byte) 32 /*0x20*/;
      numArray2[39] = (byte) 115;
      numArray2[6] = (byte) 239;
      numArray2[37] = (byte) 187;
      numArray2[46] = (byte) 73;
      numArray2[44] = (byte) 63 /*0x3F*/;
      numArray2[54] = (byte) 40;
      numArray2[41] = (byte) 126;
      numArray2[2] = (byte) 7;
      numArray2[42] = (byte) 183;
      numArray2[49] = (byte) 204;
      numArray2[26] = (byte) 25;
      numArray2[27] = (byte) 112 /*0x70*/;
      numArray2[10] = (byte) 126;
      numArray2[48 /*0x30*/] = (byte) 215;
      numArray2[47] = (byte) 177;
      numArray2[50] = (byte) 249;
      numArray2[51] = (byte) 42;
      numArray2[52] = (byte) 94;
      numArray2[45] = (byte) 81;
      numArray2[29] = (byte) 58;
      byte[] numArray3 = new byte[55]
      {
        (byte) 92,
        (byte) 10,
        (byte) 160 /*0xA0*/,
        (byte) 236,
        (byte) 162,
        (byte) 245,
        (byte) 79,
        (byte) 78,
        (byte) 240 /*0xF0*/,
        (byte) 216,
        (byte) 231,
        (byte) 29,
        (byte) 153,
        (byte) 207,
        (byte) 246,
        (byte) 148,
        (byte) 212,
        (byte) 163,
        (byte) 28,
        (byte) 27,
        (byte) 90,
        (byte) 80 /*0x50*/,
        (byte) 30,
        (byte) 198,
        (byte) 131,
        (byte) 51,
        (byte) 133,
        (byte) 67,
        (byte) 47,
        (byte) 61,
        (byte) 120,
        (byte) 103,
        (byte) 130,
        (byte) 239,
        (byte) 92,
        (byte) 13,
        (byte) 231,
        (byte) 139,
        (byte) 131,
        (byte) 250,
        (byte) 147,
        (byte) 246,
        (byte) 252,
        (byte) 195,
        (byte) 86,
        (byte) 90,
        (byte) 160 /*0xA0*/,
        (byte) 154,
        (byte) 228,
        (byte) 114,
        (byte) 92,
        (byte) 250,
        (byte) 92,
        (byte) 151,
        (byte) 107
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55];
      numArray4[25] = (byte) 48 /*0x30*/;
      numArray4[11] = (byte) 167;
      numArray4[7] = (byte) 182;
      numArray4[13] = (byte) 74;
      numArray4[4] = (byte) 230;
      numArray4[5] = (byte) 210;
      numArray4[6] = (byte) 180;
      numArray4[35] = (byte) 219;
      numArray4[8] = (byte) 118;
      numArray4[32 /*0x20*/] = (byte) 103;
      numArray4[27] = (byte) 172;
      numArray4[31 /*0x1F*/] = (byte) 157;
      numArray4[12] = (byte) 238;
      numArray4[51] = (byte) 58;
      numArray4[22] = (byte) 100;
      numArray4[15] = (byte) 202;
      numArray4[16 /*0x10*/] = (byte) 228;
      numArray4[49] = (byte) 226;
      numArray4[18] = (byte) 145;
      numArray4[48 /*0x30*/] = (byte) 194;
      numArray4[19] = (byte) 213;
      numArray4[0] = (byte) 241;
      numArray4[29] = (byte) 147;
      numArray4[2] = (byte) 101;
      numArray4[42] = (byte) 254;
      numArray4[44] = (byte) 168;
      numArray4[20] = (byte) 43;
      numArray4[24] = (byte) 212;
      numArray4[10] = (byte) 97;
      numArray4[30] = (byte) 39;
      numArray4[3] = (byte) 83;
      numArray4[40] = (byte) 14;
      numArray4[1] = (byte) 185;
      numArray4[33] = (byte) 154;
      numArray4[34] = (byte) 34;
      numArray4[9] = (byte) 43;
      numArray4[36] = (byte) 77;
      numArray4[37] = (byte) 100;
      numArray4[38] = (byte) 102;
      numArray4[39] = (byte) 133;
      numArray4[23] = (byte) 149;
      numArray4[41] = (byte) 10;
      numArray4[50] = (byte) 221;
      numArray4[43] = (byte) 221;
      numArray4[26] = (byte) 230;
      numArray4[45] = (byte) 235;
      numArray4[47] = (byte) 35;
      numArray4[14] = (byte) 209;
      numArray4[28] = (byte) 50;
      numArray4[21] = (byte) 117;
      numArray4[46] = (byte) 121;
      numArray4[17] = (byte) 187;
      numArray4[52] = (byte) 174;
      numArray4[53] = (byte) 59;
      numArray4[54] = (byte) 186;
      byte[] numArray5 = new byte[55]
      {
        (byte) 174,
        (byte) 190,
        (byte) 43,
        (byte) 140,
        (byte) 180,
        (byte) 249,
        (byte) 98,
        (byte) 175,
        (byte) 118,
        (byte) 211,
        (byte) 4,
        (byte) 67,
        (byte) 155,
        (byte) 33,
        (byte) 191,
        (byte) 75,
        (byte) 204,
        (byte) 81,
        (byte) 244,
        (byte) 198,
        (byte) 173,
        (byte) 155,
        (byte) 45,
        (byte) 94,
        (byte) 151,
        (byte) 88,
        (byte) 53,
        (byte) 43,
        (byte) 2,
        (byte) 11,
        (byte) 16 /*0x10*/,
        (byte) 236,
        (byte) 90,
        (byte) 35,
        (byte) 43,
        (byte) 5,
        (byte) 122,
        (byte) 22,
        (byte) 178,
        (byte) 53,
        (byte) 249,
        (byte) 137,
        (byte) 8,
        (byte) 178,
        (byte) 198,
        (byte) 28,
        (byte) 3,
        (byte) 27,
        (byte) 184,
        (byte) 204,
        (byte) 196,
        (byte) 27,
        (byte) 165,
        (byte) 34,
        (byte) 240 /*0xF0*/
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[55]
      {
        (byte) 226,
        (byte) 149,
        (byte) 254,
        (byte) 41,
        (byte) 30,
        (byte) 51,
        (byte) 45,
        (byte) 22,
        (byte) 212,
        (byte) 19,
        (byte) 117,
        (byte) 128 /*0x80*/,
        (byte) 110,
        byte.MaxValue,
        (byte) 153,
        (byte) 164,
        (byte) 115,
        (byte) 191,
        (byte) 153,
        (byte) 91,
        (byte) 173,
        (byte) 9,
        (byte) 61,
        (byte) 146,
        (byte) 198,
        (byte) 27,
        (byte) 234,
        (byte) 87,
        (byte) 8,
        (byte) 76,
        (byte) 183,
        (byte) 68,
        (byte) 162,
        (byte) 185,
        (byte) 155,
        (byte) 80 /*0x50*/,
        (byte) 178,
        (byte) 111,
        (byte) 231,
        (byte) 83,
        (byte) 238,
        (byte) 195,
        (byte) 16 /*0x10*/,
        (byte) 151,
        (byte) 174,
        (byte) 123,
        (byte) 94,
        (byte) 102,
        (byte) 197,
        (byte) 64 /*0x40*/,
        (byte) 47,
        (byte) 181,
        (byte) 1,
        (byte) 168,
        (byte) 32 /*0x20*/
      };
      byte[] numArray7 = new byte[55]
      {
        (byte) 36,
        (byte) 252,
        (byte) 24,
        (byte) 0,
        (byte) 101,
        (byte) 63 /*0x3F*/,
        (byte) 246,
        (byte) 153,
        (byte) 158,
        (byte) 99,
        (byte) 47,
        (byte) 128 /*0x80*/,
        (byte) 122,
        (byte) 113,
        (byte) 222,
        (byte) 53,
        (byte) 149,
        (byte) 61,
        (byte) 154,
        (byte) 151,
        (byte) 209,
        (byte) 195,
        (byte) 53,
        (byte) 147,
        (byte) 160 /*0xA0*/,
        (byte) 171,
        (byte) 232,
        (byte) 218,
        (byte) 71,
        (byte) 219,
        (byte) 49,
        (byte) 224 /*0xE0*/,
        (byte) 226,
        (byte) 109,
        (byte) 112 /*0x70*/,
        (byte) 170,
        (byte) 24,
        (byte) 52,
        (byte) 119,
        (byte) 46,
        (byte) 200,
        (byte) 247,
        (byte) 127 /*0x7F*/,
        (byte) 138,
        (byte) 143,
        (byte) 6,
        (byte) 253,
        (byte) 108,
        (byte) 82,
        (byte) 205,
        (byte) 230,
        (byte) 128 /*0x80*/,
        (byte) 234,
        (byte) 150,
        (byte) 66
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[46];
      numArray8[44] = (byte) 170;
      numArray8[30] = (byte) 249;
      numArray8[38] = (byte) 23;
      numArray8[13] = (byte) 242;
      numArray8[4] = (byte) 214;
      numArray8[27] = (byte) 29;
      numArray8[6] = (byte) 135;
      numArray8[33] = (byte) 249;
      numArray8[8] = (byte) 48 /*0x30*/;
      numArray8[9] = (byte) 221;
      numArray8[10] = (byte) 180;
      numArray8[5] = (byte) 13;
      numArray8[7] = (byte) 92;
      numArray8[19] = (byte) 249;
      numArray8[14] = (byte) 3;
      numArray8[2] = (byte) 3;
      numArray8[16 /*0x10*/] = (byte) 69;
      numArray8[17] = (byte) 109;
      numArray8[18] = (byte) 233;
      numArray8[20] = (byte) 166;
      numArray8[0] = (byte) 107;
      numArray8[40] = (byte) 136;
      numArray8[22] = (byte) 75;
      numArray8[35] = (byte) 13;
      numArray8[3] = (byte) 73;
      numArray8[1] = (byte) 98;
      numArray8[26] = (byte) 199;
      numArray8[34] = (byte) 70;
      numArray8[28] = (byte) 2;
      numArray8[29] = (byte) 67;
      numArray8[12] = (byte) 72;
      numArray8[31 /*0x1F*/] = (byte) 153;
      numArray8[45] = (byte) 2;
      numArray8[24] = (byte) 131;
      numArray8[41] = (byte) 59;
      numArray8[43] = (byte) 158;
      numArray8[21] = (byte) 227;
      numArray8[37] = (byte) 10;
      numArray8[15] = (byte) 215;
      numArray8[39] = (byte) 181;
      numArray8[25] = (byte) 146;
      numArray8[32 /*0x20*/] = (byte) 194;
      numArray8[42] = (byte) 126;
      numArray8[11] = (byte) 99;
      numArray8[23] = (byte) 188;
      numArray8[36] = (byte) 228;
      byte[] numArray9 = new byte[46];
      numArray9[33] = (byte) 68;
      numArray9[1] = (byte) 105;
      numArray9[26] = (byte) 67;
      numArray9[38] = (byte) 224 /*0xE0*/;
      numArray9[3] = (byte) 75;
      numArray9[25] = (byte) 189;
      numArray9[27] = (byte) 27;
      numArray9[7] = (byte) 212;
      numArray9[28] = (byte) 109;
      numArray9[0] = (byte) 22;
      numArray9[31 /*0x1F*/] = (byte) 226;
      numArray9[11] = (byte) 13;
      numArray9[21] = (byte) 103;
      numArray9[29] = (byte) 3;
      numArray9[14] = (byte) 204;
      numArray9[15] = (byte) 235;
      numArray9[19] = (byte) 47;
      numArray9[2] = (byte) 135;
      numArray9[18] = (byte) 84;
      numArray9[17] = (byte) 6;
      numArray9[20] = (byte) 44;
      numArray9[35] = (byte) 33;
      numArray9[22] = (byte) 156;
      numArray9[30] = (byte) 110;
      numArray9[24] = (byte) 74;
      numArray9[36] = (byte) 104;
      numArray9[42] = (byte) 183;
      numArray9[9] = (byte) 0;
      numArray9[44] = (byte) 99;
      numArray9[10] = (byte) 141;
      numArray9[8] = (byte) 144 /*0x90*/;
      numArray9[23] = (byte) 50;
      numArray9[4] = (byte) 73;
      numArray9[32 /*0x20*/] = (byte) 30;
      numArray9[34] = (byte) 148;
      numArray9[37] = (byte) 134;
      numArray9[6] = (byte) 165;
      numArray9[5] = (byte) 160 /*0xA0*/;
      numArray9[12] = (byte) 171;
      numArray9[13] = (byte) 29;
      numArray9[40] = (byte) 56;
      numArray9[41] = (byte) 139;
      numArray9[39] = (byte) 43;
      numArray9[43] = (byte) 9;
      numArray9[16 /*0x10*/] = (byte) 137;
      numArray9[45] = (byte) 113;
      key.Query(true, 335, numArray8, numArray8);
      Array.Copy((Array) numArray8, 0, (Array) numArray1, 165, 46);
      for (int index = 0; index < 46; ++index)
        numArray1[index + 165] ^= numArray9[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray10 = new byte[211];
    byte[] numArray11 = new byte[55]
    {
      (byte) 96 /*0x60*/,
      (byte) 114,
      (byte) 1,
      (byte) 156,
      (byte) 140,
      (byte) 106,
      (byte) 0,
      (byte) 165,
      (byte) 161,
      (byte) 165,
      (byte) 171,
      (byte) 42,
      (byte) 149,
      (byte) 215,
      (byte) 161,
      (byte) 38,
      (byte) 182,
      (byte) 53,
      (byte) 150,
      (byte) 139,
      (byte) 101,
      (byte) 61,
      (byte) 29,
      (byte) 189,
      (byte) 77,
      (byte) 119,
      (byte) 176 /*0xB0*/,
      (byte) 36,
      (byte) 70,
      (byte) 247,
      (byte) 189,
      (byte) 252,
      (byte) 95,
      (byte) 77,
      (byte) 86,
      (byte) 77,
      (byte) 5,
      (byte) 80 /*0x50*/,
      (byte) 116,
      (byte) 233,
      (byte) 245,
      (byte) 219,
      (byte) 35,
      (byte) 172,
      (byte) 222,
      (byte) 170,
      (byte) 92,
      (byte) 171,
      (byte) 126,
      (byte) 193,
      (byte) 31 /*0x1F*/,
      (byte) 133,
      (byte) 162,
      (byte) 122,
      (byte) 114
    };
    byte[] numArray12 = new byte[55];
    numArray12[40] = (byte) 1;
    numArray12[1] = (byte) 68;
    numArray12[2] = (byte) 36;
    numArray12[3] = (byte) 72;
    numArray12[9] = (byte) 104;
    numArray12[5] = (byte) 212;
    numArray12[6] = (byte) 141;
    numArray12[53] = (byte) 189;
    numArray12[18] = (byte) 214;
    numArray12[45] = (byte) 149;
    numArray12[34] = (byte) 58;
    numArray12[11] = (byte) 220;
    numArray12[54] = (byte) 11;
    numArray12[36] = (byte) 75;
    numArray12[15] = (byte) 101;
    numArray12[21] = (byte) 184;
    numArray12[12] = (byte) 18;
    numArray12[28] = (byte) 82;
    numArray12[26] = (byte) 128 /*0x80*/;
    numArray12[19] = (byte) 90;
    numArray12[20] = (byte) 167;
    numArray12[25] = (byte) 64 /*0x40*/;
    numArray12[30] = (byte) 107;
    numArray12[23] = (byte) 146;
    numArray12[24] = (byte) 102;
    numArray12[27] = (byte) 130;
    numArray12[17] = (byte) 122;
    numArray12[44] = (byte) 6;
    numArray12[38] = (byte) 53;
    numArray12[31 /*0x1F*/] = (byte) 69;
    numArray12[0] = (byte) 175;
    numArray12[13] = (byte) 21;
    numArray12[32 /*0x20*/] = (byte) 121;
    numArray12[33] = (byte) 196;
    numArray12[43] = (byte) 153;
    numArray12[35] = (byte) 36;
    numArray12[16 /*0x10*/] = (byte) 114;
    numArray12[37] = (byte) 69;
    numArray12[22] = (byte) 254;
    numArray12[39] = (byte) 102;
    numArray12[4] = (byte) 148;
    numArray12[8] = (byte) 173;
    numArray12[42] = (byte) 54;
    numArray12[10] = (byte) 204;
    numArray12[14] = (byte) 205;
    numArray12[41] = (byte) 30;
    numArray12[46] = (byte) 235;
    numArray12[47] = (byte) 83;
    numArray12[48 /*0x30*/] = (byte) 254;
    numArray12[50] = (byte) 92;
    numArray12[29] = (byte) 67;
    numArray12[51] = (byte) 148;
    numArray12[52] = (byte) 119;
    numArray12[7] = (byte) 22;
    numArray12[49] = (byte) 208 /*0xD0*/;
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray10, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index] ^= numArray12[index];
    byte[] numArray13 = new byte[55]
    {
      (byte) 101,
      (byte) 172,
      (byte) 249,
      (byte) 181,
      (byte) 53,
      (byte) 217,
      (byte) 209,
      (byte) 177,
      (byte) 83,
      (byte) 230,
      (byte) 108,
      (byte) 77,
      (byte) 91,
      (byte) 94,
      (byte) 101,
      (byte) 97,
      (byte) 126,
      (byte) 124,
      (byte) 12,
      (byte) 160 /*0xA0*/,
      (byte) 174,
      (byte) 174,
      (byte) 196,
      (byte) 5,
      (byte) 218,
      (byte) 123,
      (byte) 227,
      (byte) 31 /*0x1F*/,
      (byte) 115,
      (byte) 101,
      (byte) 1,
      (byte) 241,
      (byte) 42,
      (byte) 20,
      (byte) 92,
      (byte) 83,
      (byte) 102,
      (byte) 121,
      (byte) 229,
      (byte) 163,
      (byte) 168,
      (byte) 17,
      (byte) 220,
      (byte) 248,
      (byte) 169,
      (byte) 221,
      (byte) 206,
      (byte) 66,
      (byte) 83,
      (byte) 98,
      (byte) 118,
      (byte) 104,
      (byte) 221,
      (byte) 104,
      (byte) 53
    };
    byte[] numArray14 = new byte[55];
    numArray14[52] = (byte) 173;
    numArray14[14] = (byte) 81;
    numArray14[2] = (byte) 35;
    numArray14[23] = (byte) 18;
    numArray14[11] = (byte) 85;
    numArray14[18] = (byte) 187;
    numArray14[32 /*0x20*/] = (byte) 108;
    numArray14[7] = (byte) 9;
    numArray14[8] = (byte) 117;
    numArray14[6] = (byte) 123;
    numArray14[42] = (byte) 153;
    numArray14[53] = (byte) 178;
    numArray14[3] = (byte) 122;
    numArray14[17] = (byte) 171;
    numArray14[10] = (byte) 125;
    numArray14[15] = (byte) 133;
    numArray14[43] = (byte) 62;
    numArray14[4] = (byte) 155;
    numArray14[44] = (byte) 129;
    numArray14[19] = (byte) 12;
    numArray14[36] = (byte) 136;
    numArray14[21] = (byte) 222;
    numArray14[20] = (byte) 173;
    numArray14[39] = (byte) 175;
    numArray14[24] = (byte) 227;
    numArray14[51] = (byte) 156;
    numArray14[26] = (byte) 50;
    numArray14[27] = (byte) 132;
    numArray14[12] = (byte) 128 /*0x80*/;
    numArray14[29] = (byte) 153;
    numArray14[30] = (byte) 30;
    numArray14[31 /*0x1F*/] = (byte) 160 /*0xA0*/;
    numArray14[48 /*0x30*/] = (byte) 87;
    numArray14[25] = (byte) 163;
    numArray14[34] = (byte) 21;
    numArray14[33] = (byte) 91;
    numArray14[28] = (byte) 104;
    numArray14[40] = (byte) 85;
    numArray14[38] = (byte) 169;
    numArray14[13] = (byte) 205;
    numArray14[37] = (byte) 96 /*0x60*/;
    numArray14[41] = (byte) 135;
    numArray14[45] = (byte) 127 /*0x7F*/;
    numArray14[1] = (byte) 239;
    numArray14[5] = (byte) 240 /*0xF0*/;
    numArray14[35] = (byte) 28;
    numArray14[46] = (byte) 88;
    numArray14[54] = (byte) 88;
    numArray14[0] = (byte) 46;
    numArray14[49] = (byte) 227;
    numArray14[50] = (byte) 221;
    numArray14[16 /*0x10*/] = (byte) 84;
    numArray14[9] = (byte) 251;
    numArray14[47] = (byte) 122;
    numArray14[22] = (byte) 187;
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray10, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index + 55] ^= numArray14[index];
    byte[] numArray15 = new byte[55]
    {
      (byte) 151,
      (byte) 222,
      (byte) 12,
      (byte) 85,
      (byte) 230,
      (byte) 44,
      byte.MaxValue,
      (byte) 76,
      (byte) 101,
      (byte) 97,
      (byte) 103,
      (byte) 93,
      (byte) 166,
      (byte) 136,
      (byte) 101,
      (byte) 107,
      (byte) 187,
      (byte) 1,
      (byte) 200,
      (byte) 227,
      (byte) 183,
      (byte) 45,
      (byte) 66,
      (byte) 37,
      (byte) 96 /*0x60*/,
      (byte) 137,
      (byte) 170,
      (byte) 124,
      (byte) 200,
      (byte) 160 /*0xA0*/,
      (byte) 81,
      (byte) 71,
      (byte) 190,
      (byte) 78,
      (byte) 218,
      (byte) 192 /*0xC0*/,
      (byte) 31 /*0x1F*/,
      (byte) 88,
      (byte) 16 /*0x10*/,
      (byte) 167,
      (byte) 238,
      (byte) 199,
      (byte) 201,
      (byte) 88,
      (byte) 31 /*0x1F*/,
      (byte) 226,
      (byte) 155,
      (byte) 221,
      (byte) 12,
      (byte) 101,
      (byte) 184,
      (byte) 111,
      (byte) 208 /*0xD0*/,
      (byte) 2,
      (byte) 101
    };
    byte[] numArray16 = new byte[55]
    {
      (byte) 134,
      (byte) 101,
      (byte) 211,
      (byte) 11,
      (byte) 66,
      (byte) 144 /*0x90*/,
      (byte) 229,
      (byte) 127 /*0x7F*/,
      (byte) 111,
      (byte) 74,
      (byte) 138,
      (byte) 214,
      (byte) 232,
      (byte) 137,
      (byte) 170,
      (byte) 28,
      (byte) 202,
      (byte) 57,
      (byte) 63 /*0x3F*/,
      (byte) 130,
      (byte) 88,
      (byte) 20,
      (byte) 154,
      (byte) 229,
      (byte) 71,
      (byte) 2,
      (byte) 114,
      (byte) 73,
      (byte) 18,
      (byte) 35,
      (byte) 0,
      (byte) 48 /*0x30*/,
      (byte) 219,
      (byte) 120,
      (byte) 8,
      (byte) 72,
      (byte) 227,
      (byte) 27,
      (byte) 128 /*0x80*/,
      (byte) 124,
      (byte) 122,
      (byte) 65,
      (byte) 66,
      (byte) 156,
      (byte) 108,
      (byte) 135,
      (byte) 154,
      (byte) 7,
      (byte) 104,
      (byte) 203,
      (byte) 102,
      (byte) 193,
      (byte) 133,
      (byte) 221,
      (byte) 226
    };
    key.Query(true, 335, numArray15, numArray15);
    Array.Copy((Array) numArray15, 0, (Array) numArray10, 110, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index + 110] ^= numArray16[index];
    byte[] numArray17 = new byte[46];
    numArray17[39] = (byte) 67;
    numArray17[1] = (byte) 244;
    numArray17[37] = (byte) 37;
    numArray17[14] = (byte) 99;
    numArray17[21] = (byte) 6;
    numArray17[11] = (byte) 173;
    numArray17[6] = (byte) 197;
    numArray17[20] = (byte) 233;
    numArray17[22] = (byte) 126;
    numArray17[3] = (byte) 130;
    numArray17[0] = (byte) 230;
    numArray17[27] = (byte) 231;
    numArray17[4] = (byte) 130;
    numArray17[13] = (byte) 75;
    numArray17[18] = (byte) 240 /*0xF0*/;
    numArray17[5] = (byte) 132;
    numArray17[16 /*0x10*/] = (byte) 33;
    numArray17[17] = (byte) 244;
    numArray17[12] = (byte) 164;
    numArray17[19] = (byte) 100;
    numArray17[31 /*0x1F*/] = (byte) 232;
    numArray17[2] = (byte) 92;
    numArray17[15] = (byte) 240 /*0xF0*/;
    numArray17[44] = (byte) 67;
    numArray17[24] = (byte) 204;
    numArray17[28] = (byte) 225;
    numArray17[23] = (byte) 237;
    numArray17[25] = (byte) 111;
    numArray17[38] = (byte) 108;
    numArray17[29] = (byte) 118;
    numArray17[26] = (byte) 98;
    numArray17[10] = (byte) 88;
    numArray17[32 /*0x20*/] = (byte) 112 /*0x70*/;
    numArray17[33] = (byte) 37;
    numArray17[36] = (byte) 179;
    numArray17[35] = (byte) 99;
    numArray17[7] = (byte) 16 /*0x10*/;
    numArray17[30] = (byte) 188;
    numArray17[8] = (byte) 32 /*0x20*/;
    numArray17[41] = (byte) 72;
    numArray17[40] = (byte) 3;
    numArray17[45] = (byte) 48 /*0x30*/;
    numArray17[42] = byte.MaxValue;
    numArray17[43] = (byte) 67;
    numArray17[9] = (byte) 159;
    numArray17[34] = (byte) 57;
    byte[] numArray18 = new byte[46];
    numArray18[41] = (byte) 29;
    numArray18[20] = (byte) 135;
    numArray18[27] = (byte) 231;
    numArray18[3] = (byte) 150;
    numArray18[30] = (byte) 179;
    numArray18[5] = (byte) 164;
    numArray18[22] = (byte) 154;
    numArray18[7] = (byte) 210;
    numArray18[8] = (byte) 108;
    numArray18[9] = (byte) 146;
    numArray18[18] = (byte) 75;
    numArray18[11] = (byte) 37;
    numArray18[1] = (byte) 27;
    numArray18[10] = (byte) 204;
    numArray18[6] = (byte) 195;
    numArray18[15] = (byte) 182;
    numArray18[16 /*0x10*/] = (byte) 210;
    numArray18[37] = (byte) 125;
    numArray18[4] = (byte) 14;
    numArray18[19] = (byte) 119;
    numArray18[42] = (byte) 0;
    numArray18[21] = (byte) 77;
    numArray18[14] = (byte) 221;
    numArray18[38] = (byte) 96 /*0x60*/;
    numArray18[13] = (byte) 84;
    numArray18[25] = (byte) 67;
    numArray18[26] = (byte) 230;
    numArray18[23] = (byte) 182;
    numArray18[29] = (byte) 7;
    numArray18[2] = (byte) 222;
    numArray18[17] = (byte) 198;
    numArray18[31 /*0x1F*/] = (byte) 224 /*0xE0*/;
    numArray18[32 /*0x20*/] = (byte) 45;
    numArray18[33] = (byte) 174;
    numArray18[34] = (byte) 246;
    numArray18[0] = (byte) 81;
    numArray18[35] = (byte) 7;
    numArray18[28] = (byte) 47;
    numArray18[39] = (byte) 201;
    numArray18[36] = (byte) 178;
    numArray18[40] = (byte) 104;
    numArray18[24] = (byte) 197;
    numArray18[12] = (byte) 13;
    numArray18[43] = (byte) 116;
    numArray18[44] = (byte) 126;
    numArray18[45] = (byte) 75;
    key.Query(true, 335, numArray17, numArray17);
    Array.Copy((Array) numArray17, 0, (Array) numArray10, 165, 46);
    for (int index = 0; index < 46; ++index)
      numArray10[index + 165] ^= numArray18[index];
    return Encoding.UTF8.GetString(numArray10);
  }

  internal static string ssp_appserver_13040()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[203];
      byte[] numArray2 = new byte[55];
      numArray2[29] = (byte) 13;
      numArray2[22] = (byte) 137;
      numArray2[18] = (byte) 38;
      numArray2[35] = (byte) 168;
      numArray2[4] = (byte) 127 /*0x7F*/;
      numArray2[5] = (byte) 86;
      numArray2[6] = (byte) 77;
      numArray2[53] = (byte) 211;
      numArray2[8] = (byte) 219;
      numArray2[9] = (byte) 78;
      numArray2[17] = (byte) 72;
      numArray2[10] = (byte) 116;
      numArray2[11] = (byte) 44;
      numArray2[13] = (byte) 32 /*0x20*/;
      numArray2[14] = (byte) 220;
      numArray2[48 /*0x30*/] = (byte) 66;
      numArray2[16 /*0x10*/] = (byte) 66;
      numArray2[27] = (byte) 236;
      numArray2[7] = (byte) 71;
      numArray2[36] = (byte) 57;
      numArray2[21] = (byte) 106;
      numArray2[34] = (byte) 13;
      numArray2[31 /*0x1F*/] = (byte) 204;
      numArray2[47] = (byte) 136;
      numArray2[38] = (byte) 121;
      numArray2[25] = (byte) 238;
      numArray2[24] = (byte) 96 /*0x60*/;
      numArray2[26] = (byte) 111;
      numArray2[1] = (byte) 99;
      numArray2[3] = (byte) 178;
      numArray2[39] = (byte) 53;
      numArray2[12] = (byte) 215;
      numArray2[32 /*0x20*/] = (byte) 10;
      numArray2[33] = (byte) 80 /*0x50*/;
      numArray2[19] = (byte) 85;
      numArray2[28] = (byte) 153;
      numArray2[23] = (byte) 40;
      numArray2[37] = (byte) 96 /*0x60*/;
      numArray2[15] = (byte) 78;
      numArray2[45] = (byte) 173;
      numArray2[40] = (byte) 226;
      numArray2[41] = (byte) 67;
      numArray2[42] = (byte) 95;
      numArray2[43] = (byte) 56;
      numArray2[20] = (byte) 61;
      numArray2[2] = (byte) 127 /*0x7F*/;
      numArray2[46] = (byte) 254;
      numArray2[52] = (byte) 238;
      numArray2[30] = (byte) 170;
      numArray2[49] = (byte) 241;
      numArray2[50] = (byte) 251;
      numArray2[44] = (byte) 61;
      numArray2[0] = (byte) 197;
      numArray2[51] = (byte) 216;
      numArray2[54] = (byte) 110;
      byte[] numArray3 = new byte[55]
      {
        (byte) 224 /*0xE0*/,
        (byte) 127 /*0x7F*/,
        (byte) 119,
        (byte) 144 /*0x90*/,
        (byte) 51,
        (byte) 50,
        (byte) 79,
        (byte) 46,
        (byte) 112 /*0x70*/,
        (byte) 241,
        (byte) 170,
        (byte) 63 /*0x3F*/,
        (byte) 20,
        (byte) 204,
        (byte) 140,
        (byte) 210,
        (byte) 140,
        (byte) 172,
        (byte) 250,
        (byte) 183,
        (byte) 12,
        (byte) 165,
        (byte) 243,
        (byte) 118,
        (byte) 205,
        (byte) 57,
        (byte) 205,
        (byte) 20,
        (byte) 65,
        (byte) 222,
        (byte) 188,
        (byte) 170,
        (byte) 27,
        (byte) 101,
        (byte) 208 /*0xD0*/,
        (byte) 30,
        (byte) 134,
        (byte) 199,
        (byte) 162,
        (byte) 197,
        (byte) 154,
        (byte) 162,
        (byte) 190,
        (byte) 91,
        (byte) 234,
        (byte) 122,
        (byte) 48 /*0x30*/,
        (byte) 89,
        (byte) 127 /*0x7F*/,
        (byte) 61,
        (byte) 177,
        (byte) 110,
        (byte) 244,
        (byte) 28,
        (byte) 32 /*0x20*/
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 108,
        (byte) 158,
        (byte) 160 /*0xA0*/,
        (byte) 0,
        (byte) 183,
        (byte) 5,
        (byte) 188,
        (byte) 210,
        (byte) 201,
        (byte) 235,
        (byte) 165,
        (byte) 96 /*0x60*/,
        (byte) 101,
        (byte) 50,
        (byte) 211,
        (byte) 229,
        (byte) 161,
        (byte) 242,
        (byte) 100,
        (byte) 121,
        (byte) 189,
        (byte) 85,
        (byte) 40,
        (byte) 121,
        (byte) 113,
        (byte) 156,
        (byte) 203,
        (byte) 247,
        (byte) 198,
        (byte) 223,
        (byte) 215,
        (byte) 109,
        (byte) 98,
        (byte) 70,
        (byte) 118,
        (byte) 112 /*0x70*/,
        (byte) 72,
        (byte) 78,
        (byte) 201,
        (byte) 143,
        (byte) 212,
        (byte) 193,
        (byte) 203,
        (byte) 159,
        (byte) 209,
        (byte) 20,
        (byte) 167,
        (byte) 169,
        (byte) 69,
        (byte) 37,
        (byte) 92,
        (byte) 109,
        (byte) 16 /*0x10*/,
        (byte) 213,
        (byte) 203
      };
      byte[] numArray5 = new byte[55];
      numArray5[8] = (byte) 56;
      numArray5[29] = (byte) 47;
      numArray5[2] = (byte) 253;
      numArray5[36] = (byte) 169;
      numArray5[5] = (byte) 183;
      numArray5[41] = (byte) 42;
      numArray5[26] = (byte) 157;
      numArray5[7] = (byte) 17;
      numArray5[47] = (byte) 65;
      numArray5[9] = (byte) 236;
      numArray5[10] = (byte) 199;
      numArray5[21] = (byte) 65;
      numArray5[27] = (byte) 152;
      numArray5[25] = (byte) 196;
      numArray5[43] = (byte) 87;
      numArray5[40] = (byte) 117;
      numArray5[16 /*0x10*/] = (byte) 157;
      numArray5[0] = (byte) 185;
      numArray5[53] = (byte) 216;
      numArray5[38] = (byte) 214;
      numArray5[20] = (byte) 254;
      numArray5[11] = (byte) 117;
      numArray5[14] = (byte) 31 /*0x1F*/;
      numArray5[23] = (byte) 238;
      numArray5[49] = (byte) 122;
      numArray5[54] = (byte) 61;
      numArray5[3] = (byte) 79;
      numArray5[1] = (byte) 23;
      numArray5[28] = (byte) 225;
      numArray5[22] = (byte) 148;
      numArray5[42] = (byte) 103;
      numArray5[46] = (byte) 9;
      numArray5[32 /*0x20*/] = (byte) 214;
      numArray5[33] = (byte) 31 /*0x1F*/;
      numArray5[19] = (byte) 148;
      numArray5[35] = (byte) 94;
      numArray5[12] = (byte) 11;
      numArray5[34] = (byte) 53;
      numArray5[6] = (byte) 79;
      numArray5[39] = (byte) 1;
      numArray5[4] = (byte) 225;
      numArray5[30] = (byte) 164;
      numArray5[52] = (byte) 59;
      numArray5[15] = (byte) 48 /*0x30*/;
      numArray5[44] = (byte) 231;
      numArray5[45] = (byte) 251;
      numArray5[13] = (byte) 44;
      numArray5[37] = (byte) 68;
      numArray5[48 /*0x30*/] = (byte) 144 /*0x90*/;
      numArray5[31 /*0x1F*/] = (byte) 20;
      numArray5[50] = (byte) 195;
      numArray5[51] = (byte) 79;
      numArray5[18] = (byte) 112 /*0x70*/;
      numArray5[17] = (byte) 4;
      numArray5[24] = (byte) 144 /*0x90*/;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[55]
      {
        (byte) 127 /*0x7F*/,
        (byte) 50,
        (byte) 38,
        (byte) 23,
        (byte) 18,
        (byte) 106,
        (byte) 102,
        (byte) 170,
        (byte) 219,
        (byte) 112 /*0x70*/,
        (byte) 164,
        (byte) 92,
        (byte) 105,
        (byte) 122,
        (byte) 146,
        (byte) 122,
        (byte) 126,
        (byte) 149,
        (byte) 70,
        (byte) 168,
        (byte) 253,
        (byte) 15,
        (byte) 70,
        (byte) 108,
        (byte) 52,
        (byte) 207,
        (byte) 24,
        (byte) 122,
        (byte) 37,
        (byte) 32 /*0x20*/,
        (byte) 92,
        (byte) 171,
        (byte) 211,
        (byte) 12,
        (byte) 162,
        (byte) 142,
        (byte) 135,
        (byte) 62,
        (byte) 253,
        (byte) 75,
        (byte) 123,
        (byte) 224 /*0xE0*/,
        (byte) 188,
        (byte) 165,
        (byte) 227,
        (byte) 145,
        (byte) 121,
        (byte) 14,
        (byte) 118,
        (byte) 201,
        (byte) 189,
        (byte) 155,
        (byte) 137,
        (byte) 228,
        (byte) 239
      };
      byte[] numArray7 = new byte[55];
      numArray7[27] = (byte) 70;
      numArray7[1] = (byte) 52;
      numArray7[49] = (byte) 119;
      numArray7[0] = (byte) 97;
      numArray7[4] = (byte) 23;
      numArray7[3] = (byte) 133;
      numArray7[36] = (byte) 80 /*0x50*/;
      numArray7[32 /*0x20*/] = (byte) 111;
      numArray7[8] = (byte) 65;
      numArray7[9] = (byte) 183;
      numArray7[48 /*0x30*/] = (byte) 98;
      numArray7[18] = (byte) 110;
      numArray7[2] = (byte) 134;
      numArray7[17] = (byte) 175;
      numArray7[29] = (byte) 145;
      numArray7[15] = (byte) 91;
      numArray7[37] = (byte) 233;
      numArray7[23] = (byte) 98;
      numArray7[28] = (byte) 56;
      numArray7[19] = (byte) 40;
      numArray7[35] = (byte) 65;
      numArray7[24] = (byte) 89;
      numArray7[22] = (byte) 228;
      numArray7[39] = (byte) 71;
      numArray7[45] = (byte) 119;
      numArray7[25] = (byte) 73;
      numArray7[12] = (byte) 233;
      numArray7[10] = (byte) 37;
      numArray7[20] = (byte) 146;
      numArray7[13] = (byte) 91;
      numArray7[7] = (byte) 238;
      numArray7[31 /*0x1F*/] = (byte) 44;
      numArray7[30] = (byte) 1;
      numArray7[38] = (byte) 154;
      numArray7[34] = (byte) 95;
      numArray7[21] = (byte) 144 /*0x90*/;
      numArray7[26] = (byte) 163;
      numArray7[14] = (byte) 167;
      numArray7[16 /*0x10*/] = (byte) 28;
      numArray7[51] = (byte) 163;
      numArray7[40] = (byte) 202;
      numArray7[41] = (byte) 142;
      numArray7[42] = (byte) 39;
      numArray7[43] = (byte) 13;
      numArray7[54] = (byte) 54;
      numArray7[44] = (byte) 116;
      numArray7[46] = byte.MaxValue;
      numArray7[47] = (byte) 50;
      numArray7[6] = (byte) 203;
      numArray7[33] = (byte) 250;
      numArray7[50] = (byte) 89;
      numArray7[5] = (byte) 130;
      numArray7[52] = (byte) 38;
      numArray7[53] = (byte) 242;
      numArray7[11] = (byte) 174;
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[38]
      {
        (byte) 49,
        (byte) 63 /*0x3F*/,
        (byte) 236,
        (byte) 70,
        (byte) 233,
        (byte) 42,
        (byte) 87,
        (byte) 82,
        (byte) 216,
        (byte) 153,
        (byte) 63 /*0x3F*/,
        (byte) 165,
        (byte) 98,
        (byte) 39,
        (byte) 227,
        (byte) 221,
        (byte) 0,
        (byte) 158,
        (byte) 194,
        (byte) 81,
        (byte) 14,
        (byte) 160 /*0xA0*/,
        (byte) 154,
        (byte) 57,
        (byte) 212,
        (byte) 145,
        (byte) 133,
        (byte) 229,
        (byte) 190,
        (byte) 146,
        (byte) 55,
        (byte) 74,
        (byte) 22,
        (byte) 86,
        (byte) 155,
        (byte) 27,
        (byte) 205,
        (byte) 179
      };
      byte[] numArray9 = new byte[38];
      numArray9[3] = (byte) 115;
      numArray9[11] = (byte) 112 /*0x70*/;
      numArray9[26] = (byte) 143;
      numArray9[15] = (byte) 210;
      numArray9[4] = (byte) 2;
      numArray9[6] = (byte) 129;
      numArray9[0] = (byte) 130;
      numArray9[7] = (byte) 173;
      numArray9[8] = (byte) 151;
      numArray9[9] = (byte) 252;
      numArray9[13] = (byte) 53;
      numArray9[19] = (byte) 25;
      numArray9[12] = (byte) 115;
      numArray9[30] = (byte) 240 /*0xF0*/;
      numArray9[14] = (byte) 226;
      numArray9[27] = (byte) 12;
      numArray9[16 /*0x10*/] = (byte) 102;
      numArray9[31 /*0x1F*/] = (byte) 248;
      numArray9[23] = (byte) 19;
      numArray9[33] = (byte) 1;
      numArray9[17] = (byte) 150;
      numArray9[21] = (byte) 142;
      numArray9[36] = (byte) 185;
      numArray9[1] = (byte) 179;
      numArray9[2] = (byte) 231;
      numArray9[24] = (byte) 121;
      numArray9[25] = (byte) 76;
      numArray9[10] = (byte) 102;
      numArray9[28] = (byte) 139;
      numArray9[20] = (byte) 81;
      numArray9[29] = (byte) 132;
      numArray9[5] = (byte) 204;
      numArray9[32 /*0x20*/] = (byte) 149;
      numArray9[22] = (byte) 60;
      numArray9[34] = (byte) 137;
      numArray9[35] = (byte) 179;
      numArray9[18] = (byte) 227;
      numArray9[37] = (byte) 126;
      key.Query(true, 335, numArray8, numArray8);
      Array.Copy((Array) numArray8, 0, (Array) numArray1, 165, 38);
      for (int index = 0; index < 38; ++index)
        numArray1[index + 165] ^= numArray9[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray10 = new byte[203];
    byte[] numArray11 = new byte[55]
    {
      (byte) 67,
      (byte) 129,
      (byte) 214,
      (byte) 75,
      (byte) 152,
      (byte) 4,
      (byte) 30,
      (byte) 58,
      (byte) 76,
      (byte) 26,
      (byte) 9,
      (byte) 118,
      (byte) 27,
      (byte) 64 /*0x40*/,
      (byte) 77,
      (byte) 140,
      (byte) 55,
      (byte) 170,
      (byte) 202,
      (byte) 13,
      (byte) 232,
      (byte) 1,
      (byte) 3,
      (byte) 34,
      (byte) 210,
      (byte) 22,
      (byte) 63 /*0x3F*/,
      (byte) 32 /*0x20*/,
      (byte) 126,
      (byte) 34,
      (byte) 108,
      (byte) 1,
      (byte) 121,
      (byte) 86,
      (byte) 46,
      (byte) 214,
      (byte) 205,
      (byte) 39,
      (byte) 251,
      (byte) 64 /*0x40*/,
      (byte) 66,
      (byte) 186,
      (byte) 66,
      (byte) 241,
      (byte) 130,
      (byte) 81,
      (byte) 244,
      (byte) 13,
      (byte) 170,
      (byte) 5,
      (byte) 113,
      (byte) 37,
      (byte) 196,
      (byte) 29,
      (byte) 115
    };
    byte[] numArray12 = new byte[55]
    {
      (byte) 159,
      (byte) 216,
      (byte) 138,
      (byte) 247,
      (byte) 78,
      (byte) 195,
      (byte) 51,
      (byte) 101,
      (byte) 236,
      (byte) 107,
      (byte) 40,
      (byte) 176 /*0xB0*/,
      (byte) 166,
      (byte) 148,
      (byte) 119,
      (byte) 127 /*0x7F*/,
      (byte) 88,
      (byte) 237,
      (byte) 58,
      (byte) 186,
      (byte) 244,
      (byte) 244,
      (byte) 180,
      (byte) 249,
      (byte) 33,
      (byte) 140,
      (byte) 113,
      (byte) 179,
      (byte) 66,
      (byte) 219,
      (byte) 209,
      (byte) 101,
      (byte) 189,
      (byte) 154,
      (byte) 5,
      (byte) 78,
      (byte) 56,
      (byte) 175,
      (byte) 197,
      (byte) 118,
      (byte) 106,
      (byte) 147,
      (byte) 128 /*0x80*/,
      (byte) 147,
      (byte) 178,
      (byte) 115,
      (byte) 96 /*0x60*/,
      (byte) 75,
      (byte) 233,
      (byte) 30,
      (byte) 147,
      (byte) 180,
      (byte) 218,
      (byte) 244,
      (byte) 215
    };
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray10, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index] ^= numArray12[index];
    byte[] numArray13 = new byte[55]
    {
      (byte) 180,
      (byte) 121,
      (byte) 130,
      (byte) 197,
      (byte) 253,
      (byte) 187,
      (byte) 41,
      (byte) 173,
      (byte) 7,
      (byte) 253,
      (byte) 197,
      (byte) 17,
      (byte) 118,
      (byte) 190,
      (byte) 15,
      (byte) 174,
      (byte) 138,
      (byte) 202,
      (byte) 46,
      (byte) 191,
      (byte) 18,
      (byte) 160 /*0xA0*/,
      (byte) 129,
      (byte) 33,
      (byte) 50,
      (byte) 117,
      (byte) 121,
      (byte) 177,
      (byte) 71,
      (byte) 61,
      (byte) 91,
      (byte) 226,
      (byte) 236,
      (byte) 89,
      (byte) 25,
      (byte) 174,
      (byte) 121,
      (byte) 123,
      (byte) 69,
      byte.MaxValue,
      (byte) 24,
      (byte) 67,
      (byte) 122,
      (byte) 83,
      (byte) 47,
      (byte) 251,
      (byte) 52,
      (byte) 70,
      (byte) 97,
      (byte) 118,
      (byte) 58,
      (byte) 82,
      (byte) 69,
      (byte) 130,
      (byte) 106
    };
    byte[] numArray14 = new byte[55]
    {
      (byte) 115,
      (byte) 196,
      (byte) 231,
      (byte) 254,
      (byte) 142,
      (byte) 205,
      (byte) 124,
      (byte) 79,
      (byte) 74,
      (byte) 134,
      (byte) 209,
      (byte) 249,
      (byte) 181,
      (byte) 250,
      (byte) 149,
      (byte) 73,
      (byte) 148,
      (byte) 110,
      (byte) 8,
      (byte) 91,
      (byte) 183,
      (byte) 143,
      (byte) 175,
      (byte) 10,
      (byte) 235,
      (byte) 208 /*0xD0*/,
      (byte) 182,
      (byte) 43,
      (byte) 12,
      (byte) 102,
      (byte) 175,
      (byte) 19,
      (byte) 88,
      (byte) 127 /*0x7F*/,
      (byte) 101,
      (byte) 226,
      (byte) 186,
      (byte) 89,
      (byte) 223,
      (byte) 22,
      (byte) 200,
      (byte) 56,
      (byte) 245,
      (byte) 224 /*0xE0*/,
      (byte) 160 /*0xA0*/,
      (byte) 64 /*0x40*/,
      (byte) 45,
      (byte) 98,
      (byte) 182,
      (byte) 79,
      (byte) 178,
      (byte) 113,
      (byte) 218,
      (byte) 89,
      (byte) 194
    };
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray10, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index + 55] ^= numArray14[index];
    byte[] numArray15 = new byte[55];
    numArray15[16 /*0x10*/] = (byte) 46;
    numArray15[52] = (byte) 156;
    numArray15[15] = (byte) 133;
    numArray15[1] = (byte) 146;
    numArray15[43] = (byte) 213;
    numArray15[5] = (byte) 101;
    numArray15[27] = (byte) 2;
    numArray15[48 /*0x30*/] = (byte) 44;
    numArray15[17] = (byte) 166;
    numArray15[37] = (byte) 189;
    numArray15[2] = (byte) 135;
    numArray15[25] = (byte) 80 /*0x50*/;
    numArray15[9] = (byte) 37;
    numArray15[6] = (byte) 202;
    numArray15[14] = (byte) 14;
    numArray15[32 /*0x20*/] = (byte) 5;
    numArray15[0] = (byte) 96 /*0x60*/;
    numArray15[22] = (byte) 55;
    numArray15[18] = (byte) 206;
    numArray15[10] = (byte) 29;
    numArray15[11] = (byte) 165;
    numArray15[21] = (byte) 242;
    numArray15[47] = (byte) 64 /*0x40*/;
    numArray15[35] = (byte) 233;
    numArray15[3] = (byte) 65;
    numArray15[53] = (byte) 238;
    numArray15[26] = (byte) 209;
    numArray15[45] = (byte) 160 /*0xA0*/;
    numArray15[4] = (byte) 120;
    numArray15[29] = (byte) 157;
    numArray15[30] = (byte) 105;
    numArray15[31 /*0x1F*/] = (byte) 238;
    numArray15[24] = (byte) 51;
    numArray15[33] = (byte) 182;
    numArray15[34] = (byte) 25;
    numArray15[28] = (byte) 9;
    numArray15[36] = (byte) 189;
    numArray15[19] = (byte) 37;
    numArray15[23] = (byte) 65;
    numArray15[39] = (byte) 221;
    numArray15[40] = (byte) 175;
    numArray15[41] = (byte) 178;
    numArray15[42] = (byte) 76;
    numArray15[7] = (byte) 161;
    numArray15[38] = (byte) 160 /*0xA0*/;
    numArray15[13] = (byte) 53;
    numArray15[46] = byte.MaxValue;
    numArray15[12] = (byte) 97;
    numArray15[49] = (byte) 204;
    numArray15[20] = (byte) 45;
    numArray15[50] = (byte) 22;
    numArray15[51] = (byte) 220;
    numArray15[8] = (byte) 226;
    numArray15[44] = (byte) 22;
    numArray15[54] = (byte) 202;
    byte[] numArray16 = new byte[55];
    numArray16[25] = (byte) 60;
    numArray16[21] = (byte) 203;
    numArray16[46] = (byte) 123;
    numArray16[14] = (byte) 117;
    numArray16[4] = (byte) 90;
    numArray16[42] = (byte) 18;
    numArray16[6] = (byte) 158;
    numArray16[51] = (byte) 225;
    numArray16[8] = (byte) 236;
    numArray16[44] = (byte) 18;
    numArray16[13] = (byte) 75;
    numArray16[11] = (byte) 71;
    numArray16[39] = (byte) 147;
    numArray16[33] = (byte) 48 /*0x30*/;
    numArray16[35] = (byte) 248;
    numArray16[22] = (byte) 147;
    numArray16[16 /*0x10*/] = (byte) 49;
    numArray16[19] = (byte) 25;
    numArray16[18] = (byte) 154;
    numArray16[37] = (byte) 74;
    numArray16[30] = (byte) 137;
    numArray16[34] = (byte) 11;
    numArray16[24] = (byte) 111;
    numArray16[23] = (byte) 240 /*0xF0*/;
    numArray16[45] = (byte) 222;
    numArray16[17] = (byte) 78;
    numArray16[26] = (byte) 115;
    numArray16[27] = (byte) 176 /*0xB0*/;
    numArray16[15] = (byte) 228;
    numArray16[29] = (byte) 96 /*0x60*/;
    numArray16[2] = (byte) 208 /*0xD0*/;
    numArray16[31 /*0x1F*/] = (byte) 25;
    numArray16[32 /*0x20*/] = (byte) 221;
    numArray16[20] = (byte) 131;
    numArray16[9] = (byte) 169;
    numArray16[7] = (byte) 63 /*0x3F*/;
    numArray16[36] = (byte) 48 /*0x30*/;
    numArray16[3] = (byte) 223;
    numArray16[0] = (byte) 235;
    numArray16[38] = (byte) 49;
    numArray16[40] = (byte) 3;
    numArray16[41] = (byte) 250;
    numArray16[1] = (byte) 55;
    numArray16[10] = (byte) 127 /*0x7F*/;
    numArray16[12] = (byte) 13;
    numArray16[50] = (byte) 169;
    numArray16[5] = (byte) 100;
    numArray16[47] = (byte) 196;
    numArray16[48 /*0x30*/] = (byte) 181;
    numArray16[49] = (byte) 194;
    numArray16[43] = (byte) 151;
    numArray16[28] = (byte) 43;
    numArray16[52] = (byte) 128 /*0x80*/;
    numArray16[53] = (byte) 185;
    numArray16[54] = (byte) 235;
    key.Query(true, 335, numArray15, numArray15);
    Array.Copy((Array) numArray15, 0, (Array) numArray10, 110, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index + 110] ^= numArray16[index];
    byte[] numArray17 = new byte[38]
    {
      (byte) 214,
      (byte) 234,
      (byte) 220,
      (byte) 150,
      (byte) 32 /*0x20*/,
      (byte) 36,
      (byte) 220,
      (byte) 119,
      (byte) 54,
      (byte) 234,
      (byte) 36,
      (byte) 141,
      (byte) 2,
      (byte) 245,
      (byte) 143,
      (byte) 240 /*0xF0*/,
      (byte) 97,
      (byte) 150,
      (byte) 164,
      (byte) 85,
      (byte) 92,
      (byte) 58,
      (byte) 26,
      (byte) 103,
      (byte) 185,
      (byte) 95,
      (byte) 133,
      (byte) 254,
      (byte) 112 /*0x70*/,
      (byte) 163,
      (byte) 180,
      (byte) 174,
      (byte) 185,
      (byte) 226,
      (byte) 134,
      (byte) 71,
      (byte) 194,
      (byte) 174
    };
    byte[] numArray18 = new byte[38]
    {
      (byte) 157,
      (byte) 80 /*0x50*/,
      (byte) 207,
      (byte) 241,
      (byte) 229,
      (byte) 6,
      (byte) 181,
      (byte) 22,
      (byte) 228,
      (byte) 152,
      (byte) 181,
      (byte) 75,
      (byte) 122,
      (byte) 194,
      (byte) 121,
      (byte) 183,
      (byte) 52,
      (byte) 95,
      (byte) 127 /*0x7F*/,
      (byte) 103,
      (byte) 92,
      (byte) 226,
      (byte) 35,
      (byte) 245,
      (byte) 130,
      (byte) 0,
      (byte) 40,
      (byte) 109,
      (byte) 210,
      (byte) 38,
      (byte) 122,
      (byte) 121,
      (byte) 222,
      (byte) 65,
      (byte) 30,
      (byte) 186,
      (byte) 93,
      (byte) 57
    };
    key.Query(true, 335, numArray17, numArray17);
    Array.Copy((Array) numArray17, 0, (Array) numArray10, 165, 38);
    for (int index = 0; index < 38; ++index)
      numArray10[index + 165] ^= numArray18[index];
    return Encoding.UTF8.GetString(numArray10);
  }

  internal static string ssp_appserver_13041()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[144 /*0x90*/];
      byte[] numArray2 = new byte[55]
      {
        (byte) 211,
        (byte) 139,
        (byte) 112 /*0x70*/,
        (byte) 37,
        (byte) 109,
        (byte) 189,
        (byte) 84,
        (byte) 86,
        (byte) 161,
        (byte) 83,
        (byte) 73,
        (byte) 212,
        (byte) 85,
        (byte) 155,
        (byte) 208 /*0xD0*/,
        (byte) 116,
        (byte) 221,
        (byte) 193,
        (byte) 159,
        (byte) 222,
        (byte) 7,
        (byte) 146,
        (byte) 54,
        (byte) 117,
        (byte) 212,
        (byte) 32 /*0x20*/,
        (byte) 110,
        (byte) 71,
        (byte) 45,
        (byte) 178,
        (byte) 192 /*0xC0*/,
        (byte) 12,
        (byte) 232,
        (byte) 234,
        (byte) 102,
        (byte) 142,
        (byte) 247,
        (byte) 147,
        (byte) 241,
        (byte) 199,
        (byte) 169,
        (byte) 25,
        (byte) 2,
        (byte) 97,
        (byte) 101,
        (byte) 228,
        (byte) 140,
        (byte) 234,
        (byte) 119,
        (byte) 78,
        (byte) 209,
        (byte) 20,
        (byte) 249,
        (byte) 141,
        (byte) 106
      };
      byte[] numArray3 = new byte[55];
      numArray3[47] = (byte) 154;
      numArray3[39] = (byte) 123;
      numArray3[2] = (byte) 30;
      numArray3[54] = (byte) 42;
      numArray3[0] = (byte) 115;
      numArray3[5] = (byte) 22;
      numArray3[52] = (byte) 204;
      numArray3[7] = (byte) 137;
      numArray3[1] = (byte) 235;
      numArray3[13] = (byte) 100;
      numArray3[48 /*0x30*/] = (byte) 79;
      numArray3[11] = (byte) 172;
      numArray3[12] = (byte) 192 /*0xC0*/;
      numArray3[45] = (byte) 29;
      numArray3[14] = (byte) 238;
      numArray3[15] = (byte) 98;
      numArray3[27] = (byte) 241;
      numArray3[44] = (byte) 75;
      numArray3[18] = (byte) 47;
      numArray3[19] = (byte) 194;
      numArray3[9] = (byte) 152;
      numArray3[21] = (byte) 190;
      numArray3[17] = (byte) 64 /*0x40*/;
      numArray3[38] = (byte) 90;
      numArray3[24] = (byte) 170;
      numArray3[25] = (byte) 141;
      numArray3[50] = (byte) 11;
      numArray3[4] = (byte) 31 /*0x1F*/;
      numArray3[28] = (byte) 16 /*0x10*/;
      numArray3[29] = (byte) 52;
      numArray3[36] = (byte) 46;
      numArray3[26] = (byte) 231;
      numArray3[32 /*0x20*/] = (byte) 173;
      numArray3[33] = (byte) 52;
      numArray3[34] = (byte) 43;
      numArray3[30] = (byte) 205;
      numArray3[37] = (byte) 218;
      numArray3[20] = (byte) 76;
      numArray3[8] = (byte) 164;
      numArray3[49] = (byte) 203;
      numArray3[16 /*0x10*/] = (byte) 78;
      numArray3[41] = (byte) 151;
      numArray3[10] = (byte) 29;
      numArray3[35] = (byte) 200;
      numArray3[3] = (byte) 240 /*0xF0*/;
      numArray3[23] = (byte) 111;
      numArray3[46] = (byte) 217;
      numArray3[51] = (byte) 241;
      numArray3[43] = (byte) 80 /*0x50*/;
      numArray3[40] = (byte) 180;
      numArray3[6] = (byte) 127 /*0x7F*/;
      numArray3[22] = (byte) 21;
      numArray3[31 /*0x1F*/] = (byte) 159;
      numArray3[53] = (byte) 39;
      numArray3[42] = (byte) 97;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 233,
        (byte) 98,
        (byte) 185,
        (byte) 202,
        (byte) 61,
        (byte) 75,
        (byte) 13,
        (byte) 231,
        (byte) 42,
        (byte) 144 /*0x90*/,
        (byte) 236,
        (byte) 15,
        (byte) 8,
        (byte) 126,
        (byte) 7,
        (byte) 42,
        (byte) 94,
        (byte) 151,
        (byte) 254,
        (byte) 124,
        (byte) 5,
        (byte) 232,
        (byte) 246,
        (byte) 114,
        (byte) 206,
        (byte) 152,
        (byte) 11,
        (byte) 89,
        (byte) 102,
        (byte) 241,
        (byte) 2,
        (byte) 2,
        (byte) 115,
        (byte) 183,
        (byte) 20,
        (byte) 223,
        (byte) 235,
        (byte) 174,
        (byte) 95,
        (byte) 164,
        (byte) 6,
        (byte) 231,
        (byte) 253,
        (byte) 104,
        (byte) 52,
        (byte) 166,
        (byte) 46,
        (byte) 84,
        (byte) 128 /*0x80*/,
        (byte) 26,
        (byte) 194,
        (byte) 192 /*0xC0*/,
        (byte) 126,
        (byte) 52,
        (byte) 123
      };
      byte[] numArray5 = new byte[55]
      {
        (byte) 193,
        (byte) 244,
        (byte) 162,
        (byte) 116,
        (byte) 106,
        (byte) 45,
        (byte) 193,
        (byte) 213,
        (byte) 124,
        (byte) 195,
        (byte) 217,
        (byte) 50,
        (byte) 145,
        (byte) 0,
        (byte) 4,
        (byte) 86,
        (byte) 17,
        (byte) 99,
        (byte) 223,
        (byte) 155,
        (byte) 26,
        (byte) 110,
        (byte) 222,
        (byte) 194,
        (byte) 136,
        (byte) 203,
        (byte) 1,
        (byte) 69,
        (byte) 139,
        (byte) 24,
        (byte) 22,
        (byte) 33,
        (byte) 118,
        (byte) 158,
        (byte) 170,
        (byte) 138,
        (byte) 2,
        (byte) 52,
        (byte) 212,
        (byte) 134,
        (byte) 100,
        (byte) 208 /*0xD0*/,
        (byte) 125,
        (byte) 60,
        (byte) 29,
        (byte) 235,
        (byte) 76,
        (byte) 5,
        (byte) 15,
        (byte) 226,
        (byte) 16 /*0x10*/,
        (byte) 37,
        (byte) 239,
        (byte) 87,
        (byte) 174
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[34]
      {
        (byte) 70,
        (byte) 251,
        (byte) 164,
        (byte) 64 /*0x40*/,
        (byte) 179,
        (byte) 129,
        (byte) 57,
        (byte) 141,
        (byte) 233,
        (byte) 155,
        (byte) 80 /*0x50*/,
        (byte) 105,
        (byte) 39,
        (byte) 63 /*0x3F*/,
        (byte) 176 /*0xB0*/,
        (byte) 56,
        (byte) 90,
        (byte) 118,
        (byte) 30,
        (byte) 235,
        (byte) 141,
        (byte) 61,
        (byte) 40,
        (byte) 106,
        (byte) 183,
        (byte) 217,
        (byte) 75,
        (byte) 46,
        (byte) 65,
        (byte) 132,
        (byte) 63 /*0x3F*/,
        (byte) 60,
        (byte) 20,
        (byte) 115
      };
      byte[] numArray7 = new byte[34]
      {
        (byte) 200,
        (byte) 215,
        (byte) 176 /*0xB0*/,
        (byte) 92,
        (byte) 144 /*0x90*/,
        (byte) 225,
        (byte) 11,
        (byte) 143,
        (byte) 228,
        (byte) 27,
        (byte) 179,
        (byte) 198,
        (byte) 139,
        (byte) 102,
        (byte) 94,
        (byte) 33,
        (byte) 52,
        (byte) 204,
        (byte) 206,
        (byte) 106,
        (byte) 114,
        (byte) 160 /*0xA0*/,
        (byte) 66,
        (byte) 41,
        (byte) 47,
        (byte) 57,
        (byte) 78,
        (byte) 182,
        (byte) 129,
        (byte) 187,
        (byte) 249,
        (byte) 251,
        (byte) 227,
        (byte) 19
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 34);
      for (int index = 0; index < 34; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[19];
      byte[] response = new byte[19];
      Array.Copy((Array) sc_13035.sspq, 14, (Array) numArray8, 0, 19);
      key.Query(true, 335, numArray8, response);
      Array.Copy((Array) sc_13035.sspr, 14, (Array) numArray8, 0, 19);
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
    byte[] numArray9 = new byte[144 /*0x90*/];
    byte[] numArray10 = new byte[55]
    {
      (byte) 58,
      (byte) 136,
      (byte) 218,
      (byte) 54,
      (byte) 10,
      (byte) 76,
      (byte) 101,
      (byte) 92,
      (byte) 20,
      (byte) 85,
      (byte) 179,
      (byte) 154,
      (byte) 252,
      (byte) 125,
      (byte) 246,
      (byte) 35,
      (byte) 183,
      (byte) 38,
      (byte) 179,
      (byte) 243,
      (byte) 29,
      (byte) 140,
      (byte) 204,
      (byte) 128 /*0x80*/,
      (byte) 111,
      (byte) 27,
      (byte) 184,
      (byte) 96 /*0x60*/,
      (byte) 128 /*0x80*/,
      (byte) 222,
      byte.MaxValue,
      (byte) 148,
      (byte) 156,
      (byte) 224 /*0xE0*/,
      (byte) 128 /*0x80*/,
      (byte) 219,
      (byte) 120,
      (byte) 48 /*0x30*/,
      (byte) 185,
      (byte) 142,
      (byte) 229,
      (byte) 174,
      (byte) 201,
      (byte) 149,
      (byte) 164,
      (byte) 65,
      (byte) 21,
      (byte) 11,
      (byte) 45,
      (byte) 154,
      (byte) 165,
      (byte) 238,
      (byte) 43,
      (byte) 21,
      (byte) 180
    };
    byte[] numArray11 = new byte[55]
    {
      (byte) 13,
      (byte) 4,
      (byte) 94,
      (byte) 186,
      (byte) 70,
      (byte) 173,
      (byte) 11,
      (byte) 121,
      (byte) 249,
      (byte) 2,
      (byte) 224 /*0xE0*/,
      (byte) 15,
      (byte) 138,
      (byte) 212,
      (byte) 33,
      (byte) 160 /*0xA0*/,
      (byte) 150,
      (byte) 242,
      (byte) 183,
      (byte) 42,
      (byte) 162,
      (byte) 168,
      (byte) 158,
      (byte) 215,
      (byte) 108,
      (byte) 252,
      (byte) 18,
      (byte) 87,
      (byte) 98,
      (byte) 13,
      (byte) 74,
      (byte) 41,
      (byte) 145,
      (byte) 4,
      (byte) 44,
      (byte) 165,
      (byte) 154,
      (byte) 65,
      (byte) 70,
      (byte) 193,
      (byte) 138,
      (byte) 119,
      (byte) 140,
      (byte) 25,
      (byte) 55,
      (byte) 110,
      (byte) 33,
      (byte) 193,
      (byte) 227,
      (byte) 23,
      (byte) 64 /*0x40*/,
      (byte) 217,
      (byte) 166,
      (byte) 38,
      (byte) 7
    };
    key.Query(true, 335, numArray10, numArray10);
    Array.Copy((Array) numArray10, 0, (Array) numArray9, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray9[index] ^= numArray11[index];
    byte[] numArray12 = new byte[55]
    {
      (byte) 89,
      (byte) 198,
      (byte) 239,
      (byte) 147,
      (byte) 4,
      (byte) 219,
      (byte) 53,
      (byte) 85,
      (byte) 31 /*0x1F*/,
      (byte) 253,
      (byte) 28,
      (byte) 252,
      (byte) 26,
      (byte) 171,
      (byte) 135,
      (byte) 108,
      (byte) 36,
      (byte) 13,
      (byte) 111,
      (byte) 195,
      (byte) 54,
      (byte) 219,
      (byte) 45,
      (byte) 5,
      (byte) 173,
      (byte) 51,
      (byte) 230,
      (byte) 57,
      (byte) 16 /*0x10*/,
      (byte) 105,
      (byte) 134,
      (byte) 44,
      (byte) 211,
      (byte) 30,
      (byte) 31 /*0x1F*/,
      (byte) 105,
      (byte) 202,
      (byte) 250,
      (byte) 224 /*0xE0*/,
      (byte) 211,
      (byte) 147,
      (byte) 64 /*0x40*/,
      (byte) 133,
      (byte) 68,
      (byte) 249,
      (byte) 15,
      (byte) 18,
      (byte) 238,
      (byte) 30,
      (byte) 130,
      (byte) 6,
      (byte) 7,
      (byte) 69,
      (byte) 233,
      (byte) 133
    };
    byte[] numArray13 = new byte[55];
    numArray13[7] = (byte) 208 /*0xD0*/;
    numArray13[21] = (byte) 184;
    numArray13[1] = (byte) 179;
    numArray13[0] = (byte) 198;
    numArray13[11] = (byte) 147;
    numArray13[5] = (byte) 115;
    numArray13[6] = (byte) 88;
    numArray13[45] = (byte) 250;
    numArray13[8] = (byte) 40;
    numArray13[33] = (byte) 80 /*0x50*/;
    numArray13[10] = (byte) 64 /*0x40*/;
    numArray13[3] = (byte) 219;
    numArray13[12] = (byte) 66;
    numArray13[52] = (byte) 94;
    numArray13[14] = (byte) 110;
    numArray13[15] = (byte) 142;
    numArray13[47] = (byte) 55;
    numArray13[31 /*0x1F*/] = (byte) 177;
    numArray13[18] = (byte) 223;
    numArray13[20] = (byte) 148;
    numArray13[25] = (byte) 210;
    numArray13[32 /*0x20*/] = (byte) 156;
    numArray13[22] = (byte) 239;
    numArray13[30] = (byte) 23;
    numArray13[24] = (byte) 69;
    numArray13[37] = (byte) 220;
    numArray13[17] = (byte) 136;
    numArray13[19] = (byte) 173;
    numArray13[28] = (byte) 101;
    numArray13[29] = (byte) 182;
    numArray13[50] = (byte) 40;
    numArray13[48 /*0x30*/] = (byte) 79;
    numArray13[49] = (byte) 182;
    numArray13[35] = (byte) 228;
    numArray13[34] = (byte) 180;
    numArray13[23] = (byte) 82;
    numArray13[36] = (byte) 174;
    numArray13[46] = (byte) 146;
    numArray13[38] = (byte) 169;
    numArray13[39] = (byte) 116;
    numArray13[40] = (byte) 101;
    numArray13[41] = (byte) 172;
    numArray13[16 /*0x10*/] = (byte) 186;
    numArray13[2] = (byte) 16 /*0x10*/;
    numArray13[44] = (byte) 121;
    numArray13[42] = (byte) 104;
    numArray13[51] = (byte) 230;
    numArray13[4] = (byte) 135;
    numArray13[26] = (byte) 187;
    numArray13[43] = (byte) 115;
    numArray13[27] = (byte) 99;
    numArray13[9] = (byte) 46;
    numArray13[53] = (byte) 6;
    numArray13[13] = (byte) 112 /*0x70*/;
    numArray13[54] = (byte) 227;
    key.Query(true, 335, numArray12, numArray12);
    Array.Copy((Array) numArray12, 0, (Array) numArray9, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray9[index + 55] ^= numArray13[index];
    byte[] numArray14 = new byte[34]
    {
      (byte) 221,
      (byte) 71,
      (byte) 177,
      (byte) 252,
      (byte) 225,
      (byte) 181,
      (byte) 83,
      (byte) 247,
      (byte) 9,
      (byte) 62,
      (byte) 99,
      (byte) 234,
      (byte) 78,
      (byte) 192 /*0xC0*/,
      (byte) 21,
      (byte) 31 /*0x1F*/,
      (byte) 189,
      (byte) 153,
      (byte) 250,
      (byte) 134,
      (byte) 27,
      (byte) 2,
      (byte) 183,
      (byte) 9,
      (byte) 149,
      (byte) 27,
      (byte) 174,
      (byte) 133,
      (byte) 165,
      (byte) 62,
      (byte) 49,
      (byte) 9,
      (byte) 123,
      (byte) 30
    };
    byte[] numArray15 = new byte[34];
    numArray15[31 /*0x1F*/] = (byte) 192 /*0xC0*/;
    numArray15[1] = (byte) 104;
    numArray15[29] = (byte) 130;
    numArray15[3] = (byte) 0;
    numArray15[4] = (byte) 253;
    numArray15[15] = (byte) 182;
    numArray15[21] = (byte) 87;
    numArray15[22] = (byte) 189;
    numArray15[12] = (byte) 56;
    numArray15[9] = (byte) 95;
    numArray15[32 /*0x20*/] = (byte) 37;
    numArray15[11] = (byte) 243;
    numArray15[27] = (byte) 45;
    numArray15[13] = (byte) 164;
    numArray15[17] = (byte) 164;
    numArray15[10] = (byte) 198;
    numArray15[20] = (byte) 70;
    numArray15[30] = (byte) 178;
    numArray15[18] = (byte) 41;
    numArray15[14] = (byte) 51;
    numArray15[5] = (byte) 21;
    numArray15[2] = (byte) 71;
    numArray15[16 /*0x10*/] = (byte) 221;
    numArray15[23] = (byte) 249;
    numArray15[24] = (byte) 93;
    numArray15[25] = (byte) 97;
    numArray15[26] = (byte) 204;
    numArray15[28] = (byte) 196;
    numArray15[6] = (byte) 19;
    numArray15[8] = (byte) 166;
    numArray15[7] = (byte) 203;
    numArray15[19] = (byte) 168;
    numArray15[0] = (byte) 175;
    numArray15[33] = (byte) 70;
    key.Query(true, 335, numArray14, numArray14);
    Array.Copy((Array) numArray14, 0, (Array) numArray9, 110, 34);
    for (int index = 0; index < 34; ++index)
      numArray9[index + 110] ^= numArray15[index];
    return Encoding.UTF8.GetString(numArray9);
  }

  internal static int ssp_appserver_13042(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[5] = (byte) 86;
    sourceArray1[1] = (byte) 10;
    sourceArray1[2] = (byte) 68;
    sourceArray1[35] = (byte) 150;
    sourceArray1[4] = (byte) 15;
    sourceArray1[46] = (byte) 251;
    sourceArray1[6] = (byte) 28;
    sourceArray1[7] = (byte) 105;
    sourceArray1[25] = (byte) 154;
    sourceArray1[40] = (byte) 74;
    sourceArray1[22] = (byte) 174;
    sourceArray1[11] = (byte) 83;
    sourceArray1[0] = (byte) 180;
    sourceArray1[13] = (byte) 117;
    sourceArray1[8] = (byte) 143;
    sourceArray1[14] = (byte) 218;
    sourceArray1[45] = (byte) 119;
    sourceArray1[24] = (byte) 53;
    sourceArray1[18] = (byte) 180;
    sourceArray1[3] = (byte) 2;
    sourceArray1[20] = (byte) 239;
    sourceArray1[36] = (byte) 80 /*0x50*/;
    sourceArray1[16 /*0x10*/] = (byte) 40;
    sourceArray1[23] = (byte) 132;
    sourceArray1[15] = (byte) 137;
    sourceArray1[17] = (byte) 60;
    sourceArray1[26] = (byte) 89;
    sourceArray1[27] = (byte) 9;
    sourceArray1[29] = (byte) 98;
    sourceArray1[9] = (byte) 189;
    sourceArray1[30] = (byte) 127 /*0x7F*/;
    sourceArray1[31 /*0x1F*/] = (byte) 248;
    sourceArray1[43] = (byte) 16 /*0x10*/;
    sourceArray1[21] = (byte) 63 /*0x3F*/;
    sourceArray1[34] = (byte) 223;
    sourceArray1[12] = (byte) 18;
    sourceArray1[19] = (byte) 121;
    sourceArray1[37] = (byte) 217;
    sourceArray1[38] = (byte) 128 /*0x80*/;
    sourceArray1[39] = (byte) 81;
    sourceArray1[33] = (byte) 59;
    sourceArray1[41] = (byte) 92;
    sourceArray1[42] = (byte) 119;
    sourceArray1[28] = (byte) 129;
    sourceArray1[44] = (byte) 103;
    sourceArray1[32 /*0x20*/] = (byte) 14;
    sourceArray1[10] = (byte) 234;
    sourceArray1[47] = (byte) 228;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 182,
      (byte) 7,
      (byte) 231,
      (byte) 13,
      (byte) 253,
      (byte) 171,
      (byte) 79,
      (byte) 190,
      (byte) 100,
      (byte) 162,
      (byte) 27,
      (byte) 114,
      (byte) 229,
      (byte) 61,
      (byte) 248,
      (byte) 161,
      (byte) 202,
      (byte) 246,
      (byte) 62,
      (byte) 93,
      (byte) 186,
      (byte) 242,
      (byte) 116,
      (byte) 249,
      (byte) 135,
      (byte) 137,
      (byte) 78,
      (byte) 187,
      (byte) 180,
      (byte) 187,
      (byte) 97,
      (byte) 76,
      (byte) 199,
      (byte) 101,
      (byte) 78,
      (byte) 198,
      (byte) 85,
      (byte) 178,
      (byte) 137,
      (byte) 140,
      (byte) 124,
      (byte) 228,
      (byte) 254,
      (byte) 222,
      (byte) 253,
      (byte) 171,
      (byte) 144 /*0x90*/,
      (byte) 125
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[14];
    byte[] response2 = new byte[14];
    Array.Copy((Array) sc_13035.sspq, 33, (Array) numArray2, 0, 14);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13035.sspr, 33, (Array) numArray2, 0, 14);
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

  internal static string ssp_appserver_13043()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[109];
      byte[] numArray2 = new byte[55]
      {
        (byte) 134,
        (byte) 159,
        (byte) 11,
        (byte) 96 /*0x60*/,
        (byte) 201,
        (byte) 7,
        (byte) 151,
        (byte) 144 /*0x90*/,
        (byte) 44,
        (byte) 9,
        (byte) 168,
        (byte) 66,
        (byte) 177,
        (byte) 14,
        (byte) 247,
        (byte) 29,
        (byte) 125,
        (byte) 202,
        (byte) 168,
        (byte) 137,
        (byte) 123,
        (byte) 38,
        (byte) 35,
        (byte) 110,
        (byte) 134,
        (byte) 37,
        (byte) 64 /*0x40*/,
        (byte) 121,
        (byte) 228,
        (byte) 187,
        (byte) 123,
        (byte) 237,
        (byte) 253,
        (byte) 53,
        (byte) 206,
        (byte) 152,
        (byte) 105,
        (byte) 233,
        (byte) 9,
        (byte) 109,
        (byte) 155,
        (byte) 148,
        (byte) 34,
        (byte) 71,
        (byte) 197,
        (byte) 3,
        (byte) 158,
        (byte) 156,
        (byte) 59,
        (byte) 68,
        (byte) 49,
        (byte) 244,
        (byte) 250,
        (byte) 133,
        (byte) 90
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 138,
        (byte) 149,
        (byte) 184,
        (byte) 151,
        (byte) 150,
        (byte) 75,
        (byte) 150,
        (byte) 54,
        (byte) 220,
        (byte) 225,
        (byte) 224 /*0xE0*/,
        (byte) 62,
        (byte) 68,
        (byte) 243,
        (byte) 229,
        (byte) 250,
        (byte) 211,
        (byte) 103,
        (byte) 62,
        (byte) 202,
        (byte) 76,
        (byte) 245,
        (byte) 160 /*0xA0*/,
        (byte) 32 /*0x20*/,
        (byte) 179,
        (byte) 131,
        (byte) 144 /*0x90*/,
        (byte) 52,
        (byte) 191,
        (byte) 11,
        (byte) 153,
        (byte) 247,
        (byte) 48 /*0x30*/,
        (byte) 164,
        (byte) 134,
        (byte) 137,
        (byte) 94,
        (byte) 102,
        (byte) 124,
        (byte) 40,
        (byte) 233,
        (byte) 202,
        (byte) 2,
        (byte) 176 /*0xB0*/,
        (byte) 101,
        (byte) 166,
        (byte) 5,
        (byte) 225,
        (byte) 166,
        (byte) 162,
        (byte) 40,
        (byte) 209,
        (byte) 75,
        (byte) 35,
        (byte) 65
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[54]
      {
        (byte) 157,
        (byte) 217,
        (byte) 164,
        (byte) 198,
        (byte) 30,
        (byte) 171,
        (byte) 112 /*0x70*/,
        (byte) 142,
        (byte) 186,
        (byte) 69,
        (byte) 123,
        (byte) 63 /*0x3F*/,
        (byte) 95,
        (byte) 180,
        (byte) 147,
        (byte) 193,
        (byte) 58,
        (byte) 238,
        (byte) 231,
        (byte) 3,
        (byte) 220,
        (byte) 168,
        (byte) 130,
        (byte) 135,
        (byte) 164,
        (byte) 62,
        (byte) 159,
        (byte) 138,
        (byte) 135,
        (byte) 243,
        (byte) 119,
        (byte) 219,
        (byte) 109,
        (byte) 28,
        (byte) 203,
        (byte) 15,
        (byte) 100,
        (byte) 167,
        (byte) 125,
        (byte) 232,
        (byte) 177,
        (byte) 185,
        (byte) 10,
        (byte) 190,
        (byte) 34,
        (byte) 92,
        (byte) 219,
        (byte) 144 /*0x90*/,
        (byte) 157,
        (byte) 50,
        (byte) 192 /*0xC0*/,
        (byte) 151,
        (byte) 115,
        (byte) 93
      };
      byte[] numArray5 = new byte[54];
      numArray5[44] = (byte) 87;
      numArray5[10] = (byte) 239;
      numArray5[37] = (byte) 192 /*0xC0*/;
      numArray5[22] = (byte) 112 /*0x70*/;
      numArray5[4] = (byte) 52;
      numArray5[5] = (byte) 0;
      numArray5[6] = (byte) 135;
      numArray5[7] = (byte) 188;
      numArray5[9] = (byte) 219;
      numArray5[28] = (byte) 215;
      numArray5[8] = (byte) 251;
      numArray5[38] = (byte) 52;
      numArray5[45] = (byte) 85;
      numArray5[19] = (byte) 125;
      numArray5[11] = (byte) 170;
      numArray5[15] = (byte) 249;
      numArray5[18] = (byte) 56;
      numArray5[17] = (byte) 253;
      numArray5[48 /*0x30*/] = (byte) 87;
      numArray5[2] = (byte) 52;
      numArray5[20] = (byte) 70;
      numArray5[21] = (byte) 203;
      numArray5[27] = (byte) 172;
      numArray5[23] = (byte) 123;
      numArray5[16 /*0x10*/] = (byte) 108;
      numArray5[42] = (byte) 253;
      numArray5[0] = (byte) 143;
      numArray5[26] = (byte) 89;
      numArray5[35] = (byte) 135;
      numArray5[39] = (byte) 98;
      numArray5[31 /*0x1F*/] = (byte) 153;
      numArray5[47] = (byte) 215;
      numArray5[32 /*0x20*/] = (byte) 185;
      numArray5[33] = (byte) 36;
      numArray5[34] = (byte) 212;
      numArray5[52] = (byte) 47;
      numArray5[49] = (byte) 98;
      numArray5[3] = (byte) 170;
      numArray5[51] = (byte) 120;
      numArray5[25] = (byte) 49;
      numArray5[40] = (byte) 21;
      numArray5[41] = (byte) 249;
      numArray5[14] = (byte) 99;
      numArray5[43] = (byte) 24;
      numArray5[36] = (byte) 88;
      numArray5[12] = (byte) 132;
      numArray5[46] = (byte) 222;
      numArray5[13] = (byte) 9;
      numArray5[24] = (byte) 61;
      numArray5[1] = (byte) 42;
      numArray5[50] = (byte) 31 /*0x1F*/;
      numArray5[29] = (byte) 117;
      numArray5[30] = (byte) 53;
      numArray5[53] = (byte) 145;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 54);
      for (int index = 0; index < 54; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[109];
    byte[] numArray7 = new byte[55];
    numArray7[41] = (byte) 174;
    numArray7[5] = (byte) 249;
    numArray7[2] = (byte) 217;
    numArray7[13] = (byte) 203;
    numArray7[33] = (byte) 127 /*0x7F*/;
    numArray7[43] = (byte) 76;
    numArray7[42] = (byte) 3;
    numArray7[47] = (byte) 77;
    numArray7[3] = (byte) 125;
    numArray7[9] = (byte) 54;
    numArray7[10] = (byte) 193;
    numArray7[46] = (byte) 150;
    numArray7[12] = (byte) 17;
    numArray7[28] = (byte) 5;
    numArray7[14] = (byte) 21;
    numArray7[26] = (byte) 211;
    numArray7[49] = (byte) 31 /*0x1F*/;
    numArray7[7] = (byte) 148;
    numArray7[32 /*0x20*/] = (byte) 1;
    numArray7[19] = (byte) 112 /*0x70*/;
    numArray7[20] = (byte) 90;
    numArray7[0] = (byte) 146;
    numArray7[22] = (byte) 224 /*0xE0*/;
    numArray7[17] = (byte) 33;
    numArray7[24] = (byte) 201;
    numArray7[25] = (byte) 103;
    numArray7[21] = (byte) 212;
    numArray7[27] = (byte) 126;
    numArray7[15] = (byte) 13;
    numArray7[8] = (byte) 237;
    numArray7[30] = (byte) 244;
    numArray7[1] = (byte) 185;
    numArray7[44] = (byte) 201;
    numArray7[11] = (byte) 197;
    numArray7[34] = (byte) 38;
    numArray7[16 /*0x10*/] = (byte) 212;
    numArray7[48 /*0x30*/] = (byte) 192 /*0xC0*/;
    numArray7[37] = (byte) 30;
    numArray7[38] = (byte) 224 /*0xE0*/;
    numArray7[39] = (byte) 209;
    numArray7[40] = byte.MaxValue;
    numArray7[31 /*0x1F*/] = (byte) 5;
    numArray7[4] = (byte) 252;
    numArray7[29] = (byte) 186;
    numArray7[36] = (byte) 171;
    numArray7[23] = (byte) 189;
    numArray7[53] = (byte) 11;
    numArray7[45] = (byte) 211;
    numArray7[35] = (byte) 141;
    numArray7[18] = (byte) 134;
    numArray7[50] = (byte) 81;
    numArray7[51] = (byte) 82;
    numArray7[52] = (byte) 132;
    numArray7[6] = (byte) 178;
    numArray7[54] = (byte) 252;
    byte[] numArray8 = new byte[55];
    numArray8[40] = (byte) 50;
    numArray8[1] = (byte) 85;
    numArray8[38] = (byte) 58;
    numArray8[3] = (byte) 93;
    numArray8[9] = (byte) 111;
    numArray8[2] = (byte) 19;
    numArray8[15] = (byte) 244;
    numArray8[36] = (byte) 68;
    numArray8[16 /*0x10*/] = (byte) 82;
    numArray8[37] = (byte) 59;
    numArray8[10] = (byte) 143;
    numArray8[11] = (byte) 33;
    numArray8[12] = (byte) 18;
    numArray8[13] = (byte) 194;
    numArray8[14] = (byte) 76;
    numArray8[52] = (byte) 106;
    numArray8[44] = (byte) 8;
    numArray8[17] = (byte) 71;
    numArray8[18] = (byte) 196;
    numArray8[46] = (byte) 91;
    numArray8[22] = (byte) 225;
    numArray8[21] = (byte) 37;
    numArray8[0] = (byte) 229;
    numArray8[34] = (byte) 228;
    numArray8[27] = (byte) 235;
    numArray8[7] = (byte) 208 /*0xD0*/;
    numArray8[42] = (byte) 177;
    numArray8[8] = (byte) 244;
    numArray8[24] = (byte) 109;
    numArray8[29] = (byte) 23;
    numArray8[30] = (byte) 189;
    numArray8[31 /*0x1F*/] = (byte) 72;
    numArray8[32 /*0x20*/] = (byte) 90;
    numArray8[4] = (byte) 10;
    numArray8[5] = (byte) 204;
    numArray8[35] = (byte) 175;
    numArray8[39] = (byte) 89;
    numArray8[51] = (byte) 191;
    numArray8[6] = (byte) 104;
    numArray8[53] = (byte) 187;
    numArray8[54] = (byte) 77;
    numArray8[45] = (byte) 56;
    numArray8[33] = (byte) 97;
    numArray8[43] = (byte) 0;
    numArray8[28] = (byte) 99;
    numArray8[25] = (byte) 63 /*0x3F*/;
    numArray8[23] = (byte) 187;
    numArray8[47] = (byte) 88;
    numArray8[48 /*0x30*/] = (byte) 228;
    numArray8[49] = (byte) 153;
    numArray8[50] = (byte) 105;
    numArray8[41] = (byte) 134;
    numArray8[26] = (byte) 194;
    numArray8[19] = (byte) 196;
    numArray8[20] = (byte) 184;
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[54];
    numArray9[7] = (byte) 16 /*0x10*/;
    numArray9[19] = (byte) 248;
    numArray9[14] = (byte) 159;
    numArray9[3] = (byte) 195;
    numArray9[42] = (byte) 69;
    numArray9[47] = (byte) 129;
    numArray9[53] = (byte) 244;
    numArray9[6] = (byte) 252;
    numArray9[1] = (byte) 216;
    numArray9[13] = (byte) 105;
    numArray9[8] = (byte) 139;
    numArray9[11] = (byte) 192 /*0xC0*/;
    numArray9[12] = (byte) 97;
    numArray9[27] = (byte) 124;
    numArray9[28] = (byte) 118;
    numArray9[9] = (byte) 116;
    numArray9[16 /*0x10*/] = (byte) 0;
    numArray9[17] = (byte) 80 /*0x50*/;
    numArray9[15] = (byte) 113;
    numArray9[2] = (byte) 248;
    numArray9[4] = (byte) 241;
    numArray9[46] = (byte) 4;
    numArray9[34] = (byte) 41;
    numArray9[23] = (byte) 215;
    numArray9[51] = (byte) 165;
    numArray9[25] = (byte) 31 /*0x1F*/;
    numArray9[26] = (byte) 62;
    numArray9[5] = (byte) 126;
    numArray9[44] = (byte) 23;
    numArray9[29] = (byte) 235;
    numArray9[18] = (byte) 19;
    numArray9[31 /*0x1F*/] = (byte) 108;
    numArray9[32 /*0x20*/] = (byte) 142;
    numArray9[22] = (byte) 165;
    numArray9[24] = (byte) 111;
    numArray9[35] = (byte) 211;
    numArray9[49] = (byte) 168;
    numArray9[37] = (byte) 70;
    numArray9[38] = (byte) 98;
    numArray9[39] = (byte) 249;
    numArray9[40] = (byte) 11;
    numArray9[36] = (byte) 141;
    numArray9[20] = (byte) 34;
    numArray9[43] = (byte) 204;
    numArray9[30] = (byte) 100;
    numArray9[45] = (byte) 172;
    numArray9[41] = (byte) 87;
    numArray9[33] = (byte) 184;
    numArray9[48 /*0x30*/] = (byte) 177;
    numArray9[10] = (byte) 221;
    numArray9[50] = (byte) 196;
    numArray9[21] = (byte) 22;
    numArray9[52] = (byte) 177;
    numArray9[0] = (byte) 63 /*0x3F*/;
    byte[] numArray10 = new byte[54]
    {
      (byte) 22,
      (byte) 91,
      (byte) 180,
      (byte) 71,
      (byte) 164,
      (byte) 220,
      (byte) 56,
      (byte) 12,
      (byte) 53,
      (byte) 178,
      (byte) 132,
      (byte) 143,
      (byte) 197,
      (byte) 235,
      (byte) 28,
      (byte) 43,
      (byte) 222,
      (byte) 104,
      (byte) 152,
      (byte) 186,
      (byte) 119,
      (byte) 119,
      (byte) 217,
      (byte) 195,
      (byte) 78,
      (byte) 21,
      (byte) 245,
      (byte) 21,
      (byte) 142,
      (byte) 148,
      (byte) 106,
      (byte) 183,
      (byte) 71,
      (byte) 143,
      (byte) 125,
      (byte) 137,
      (byte) 104,
      (byte) 132,
      (byte) 139,
      (byte) 7,
      (byte) 134,
      (byte) 51,
      (byte) 37,
      (byte) 149,
      (byte) 253,
      (byte) 9,
      (byte) 212,
      (byte) 170,
      (byte) 239,
      (byte) 233,
      (byte) 242,
      (byte) 76,
      (byte) 153,
      (byte) 49
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 54);
    for (int index = 0; index < 54; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_13044()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[109];
      byte[] numArray2 = new byte[55]
      {
        (byte) 96 /*0x60*/,
        (byte) 146,
        (byte) 183,
        (byte) 131,
        (byte) 180,
        (byte) 203,
        (byte) 120,
        (byte) 81,
        (byte) 182,
        (byte) 152,
        (byte) 133,
        (byte) 64 /*0x40*/,
        (byte) 44,
        (byte) 119,
        (byte) 200,
        (byte) 93,
        (byte) 97,
        (byte) 98,
        (byte) 14,
        (byte) 42,
        (byte) 27,
        (byte) 26,
        (byte) 71,
        (byte) 190,
        (byte) 123,
        (byte) 230,
        (byte) 9,
        (byte) 29,
        (byte) 7,
        (byte) 35,
        (byte) 146,
        (byte) 88,
        (byte) 168,
        (byte) 69,
        (byte) 12,
        (byte) 94,
        (byte) 216,
        (byte) 54,
        (byte) 214,
        (byte) 242,
        (byte) 46,
        (byte) 107,
        (byte) 239,
        (byte) 226,
        (byte) 214,
        (byte) 1,
        (byte) 184,
        (byte) 77,
        (byte) 65,
        (byte) 89,
        (byte) 53,
        (byte) 165,
        (byte) 188,
        (byte) 45,
        (byte) 63 /*0x3F*/
      };
      byte[] numArray3 = new byte[55];
      numArray3[51] = (byte) 247;
      numArray3[42] = (byte) 162;
      numArray3[7] = (byte) 163;
      numArray3[3] = (byte) 217;
      numArray3[26] = (byte) 126;
      numArray3[5] = (byte) 144 /*0x90*/;
      numArray3[6] = (byte) 197;
      numArray3[53] = (byte) 111;
      numArray3[2] = (byte) 52;
      numArray3[16 /*0x10*/] = (byte) 189;
      numArray3[10] = (byte) 189;
      numArray3[54] = (byte) 181;
      numArray3[11] = (byte) 153;
      numArray3[28] = (byte) 33;
      numArray3[14] = (byte) 96 /*0x60*/;
      numArray3[49] = (byte) 199;
      numArray3[48 /*0x30*/] = (byte) 157;
      numArray3[35] = (byte) 245;
      numArray3[50] = (byte) 139;
      numArray3[19] = (byte) 245;
      numArray3[31 /*0x1F*/] = (byte) 198;
      numArray3[13] = (byte) 163;
      numArray3[22] = (byte) 131;
      numArray3[23] = (byte) 137;
      numArray3[24] = (byte) 110;
      numArray3[25] = (byte) 8;
      numArray3[43] = (byte) 225;
      numArray3[27] = (byte) 73;
      numArray3[36] = (byte) 163;
      numArray3[12] = (byte) 192 /*0xC0*/;
      numArray3[30] = (byte) 145;
      numArray3[52] = (byte) 24;
      numArray3[32 /*0x20*/] = (byte) 87;
      numArray3[45] = (byte) 34;
      numArray3[8] = (byte) 219;
      numArray3[4] = (byte) 22;
      numArray3[46] = (byte) 105;
      numArray3[37] = (byte) 53;
      numArray3[18] = (byte) 176 /*0xB0*/;
      numArray3[39] = (byte) 25;
      numArray3[40] = (byte) 112 /*0x70*/;
      numArray3[41] = (byte) 46;
      numArray3[1] = (byte) 143;
      numArray3[29] = (byte) 191;
      numArray3[44] = (byte) 95;
      numArray3[38] = (byte) 44;
      numArray3[47] = (byte) 5;
      numArray3[17] = (byte) 78;
      numArray3[33] = (byte) 112 /*0x70*/;
      numArray3[34] = (byte) 42;
      numArray3[9] = (byte) 221;
      numArray3[15] = (byte) 229;
      numArray3[20] = (byte) 6;
      numArray3[21] = (byte) 194;
      numArray3[0] = (byte) 138;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[54];
      numArray4[3] = (byte) 4;
      numArray4[1] = (byte) 140;
      numArray4[0] = (byte) 17;
      numArray4[22] = (byte) 17;
      numArray4[53] = (byte) 76;
      numArray4[10] = (byte) 224 /*0xE0*/;
      numArray4[6] = (byte) 164;
      numArray4[9] = (byte) 29;
      numArray4[7] = (byte) 24;
      numArray4[39] = (byte) 132;
      numArray4[37] = (byte) 44;
      numArray4[25] = (byte) 3;
      numArray4[12] = (byte) 85;
      numArray4[51] = (byte) 7;
      numArray4[24] = (byte) 218;
      numArray4[30] = (byte) 73;
      numArray4[16 /*0x10*/] = (byte) 234;
      numArray4[17] = (byte) 41;
      numArray4[31 /*0x1F*/] = (byte) 118;
      numArray4[19] = (byte) 206;
      numArray4[20] = (byte) 136;
      numArray4[21] = (byte) 61;
      numArray4[8] = (byte) 143;
      numArray4[4] = (byte) 127 /*0x7F*/;
      numArray4[5] = (byte) 157;
      numArray4[27] = (byte) 228;
      numArray4[26] = (byte) 216;
      numArray4[15] = (byte) 130;
      numArray4[23] = (byte) 91;
      numArray4[29] = (byte) 252;
      numArray4[42] = (byte) 26;
      numArray4[34] = (byte) 206;
      numArray4[32 /*0x20*/] = (byte) 171;
      numArray4[38] = (byte) 83;
      numArray4[35] = (byte) 162;
      numArray4[2] = (byte) 160 /*0xA0*/;
      numArray4[36] = (byte) 104;
      numArray4[14] = (byte) 252;
      numArray4[13] = (byte) 165;
      numArray4[11] = (byte) 164;
      numArray4[18] = (byte) 142;
      numArray4[41] = (byte) 196;
      numArray4[46] = (byte) 130;
      numArray4[43] = (byte) 75;
      numArray4[28] = (byte) 211;
      numArray4[45] = (byte) 61;
      numArray4[44] = (byte) 241;
      numArray4[47] = (byte) 47;
      numArray4[48 /*0x30*/] = (byte) 13;
      numArray4[33] = (byte) 46;
      numArray4[50] = (byte) 181;
      numArray4[40] = (byte) 183;
      numArray4[52] = (byte) 1;
      numArray4[49] = (byte) 151;
      byte[] numArray5 = new byte[54]
      {
        (byte) 114,
        (byte) 212,
        (byte) 120,
        (byte) 99,
        (byte) 2,
        (byte) 221,
        (byte) 100,
        (byte) 199,
        (byte) 2,
        (byte) 47,
        (byte) 81,
        (byte) 197,
        (byte) 234,
        (byte) 29,
        (byte) 211,
        (byte) 100,
        (byte) 175,
        (byte) 35,
        (byte) 211,
        (byte) 7,
        (byte) 11,
        (byte) 38,
        (byte) 219,
        (byte) 53,
        (byte) 59,
        (byte) 190,
        (byte) 177,
        (byte) 32 /*0x20*/,
        (byte) 196,
        (byte) 124,
        (byte) 234,
        (byte) 122,
        (byte) 60,
        (byte) 253,
        (byte) 53,
        (byte) 53,
        (byte) 60,
        (byte) 38,
        (byte) 13,
        (byte) 83,
        (byte) 171,
        (byte) 221,
        (byte) 152,
        (byte) 208 /*0xD0*/,
        (byte) 203,
        (byte) 129,
        (byte) 109,
        (byte) 96 /*0x60*/,
        (byte) 99,
        (byte) 165,
        (byte) 33,
        (byte) 212,
        (byte) 35,
        (byte) 237
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 54);
      for (int index = 0; index < 54; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[109];
    byte[] numArray7 = new byte[55]
    {
      (byte) 180,
      (byte) 218,
      (byte) 141,
      (byte) 250,
      (byte) 27,
      (byte) 233,
      (byte) 8,
      (byte) 76,
      (byte) 145,
      (byte) 245,
      (byte) 155,
      (byte) 86,
      (byte) 220,
      (byte) 50,
      (byte) 244,
      (byte) 188,
      (byte) 253,
      (byte) 74,
      (byte) 31 /*0x1F*/,
      (byte) 69,
      (byte) 155,
      (byte) 78,
      (byte) 227,
      (byte) 88,
      (byte) 39,
      (byte) 80 /*0x50*/,
      (byte) 36,
      (byte) 189,
      (byte) 79,
      (byte) 16 /*0x10*/,
      (byte) 126,
      (byte) 40,
      (byte) 57,
      (byte) 72,
      (byte) 115,
      (byte) 74,
      (byte) 116,
      (byte) 221,
      (byte) 163,
      (byte) 14,
      (byte) 118,
      (byte) 59,
      (byte) 230,
      (byte) 145,
      (byte) 168,
      (byte) 4,
      (byte) 123,
      (byte) 128 /*0x80*/,
      (byte) 138,
      (byte) 58,
      (byte) 52,
      (byte) 171,
      (byte) 22,
      (byte) 83,
      (byte) 22
    };
    byte[] numArray8 = new byte[55];
    numArray8[20] = (byte) 242;
    numArray8[33] = (byte) 2;
    numArray8[12] = (byte) 163;
    numArray8[39] = (byte) 122;
    numArray8[4] = (byte) 37;
    numArray8[5] = (byte) 55;
    numArray8[6] = (byte) 199;
    numArray8[42] = (byte) 66;
    numArray8[53] = (byte) 97;
    numArray8[11] = (byte) 148;
    numArray8[10] = (byte) 167;
    numArray8[48 /*0x30*/] = (byte) 126;
    numArray8[0] = (byte) 85;
    numArray8[13] = (byte) 177;
    numArray8[14] = (byte) 218;
    numArray8[46] = (byte) 200;
    numArray8[16 /*0x10*/] = (byte) 71;
    numArray8[1] = (byte) 133;
    numArray8[2] = (byte) 185;
    numArray8[19] = byte.MaxValue;
    numArray8[40] = (byte) 2;
    numArray8[51] = (byte) 101;
    numArray8[21] = (byte) 166;
    numArray8[23] = (byte) 115;
    numArray8[18] = (byte) 39;
    numArray8[9] = (byte) 80 /*0x50*/;
    numArray8[26] = (byte) 116;
    numArray8[3] = (byte) 212;
    numArray8[28] = (byte) 71;
    numArray8[29] = (byte) 172;
    numArray8[52] = (byte) 140;
    numArray8[31 /*0x1F*/] = (byte) 41;
    numArray8[32 /*0x20*/] = (byte) 115;
    numArray8[8] = (byte) 101;
    numArray8[38] = (byte) 73;
    numArray8[35] = (byte) 215;
    numArray8[45] = (byte) 25;
    numArray8[27] = (byte) 54;
    numArray8[15] = (byte) 126;
    numArray8[37] = (byte) 112 /*0x70*/;
    numArray8[22] = (byte) 64 /*0x40*/;
    numArray8[41] = (byte) 75;
    numArray8[30] = (byte) 203;
    numArray8[43] = (byte) 241;
    numArray8[44] = (byte) 235;
    numArray8[36] = (byte) 24;
    numArray8[17] = (byte) 76;
    numArray8[7] = (byte) 44;
    numArray8[34] = (byte) 189;
    numArray8[49] = (byte) 239;
    numArray8[50] = (byte) 55;
    numArray8[25] = (byte) 100;
    numArray8[24] = (byte) 42;
    numArray8[47] = (byte) 164;
    numArray8[54] = (byte) 66;
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[54];
    numArray9[5] = (byte) 231;
    numArray9[24] = (byte) 224 /*0xE0*/;
    numArray9[2] = (byte) 245;
    numArray9[3] = (byte) 33;
    numArray9[4] = (byte) 24;
    numArray9[44] = (byte) 133;
    numArray9[10] = (byte) 97;
    numArray9[7] = (byte) 164;
    numArray9[12] = (byte) 87;
    numArray9[39] = (byte) 87;
    numArray9[20] = (byte) 55;
    numArray9[19] = (byte) 2;
    numArray9[8] = (byte) 97;
    numArray9[6] = (byte) 165;
    numArray9[14] = (byte) 20;
    numArray9[22] = (byte) 59;
    numArray9[16 /*0x10*/] = (byte) 199;
    numArray9[17] = (byte) 163;
    numArray9[18] = (byte) 32 /*0x20*/;
    numArray9[15] = (byte) 206;
    numArray9[0] = (byte) 132;
    numArray9[21] = (byte) 123;
    numArray9[52] = (byte) 149;
    numArray9[48 /*0x30*/] = (byte) 0;
    numArray9[25] = (byte) 83;
    numArray9[26] = (byte) 34;
    numArray9[11] = (byte) 175;
    numArray9[13] = (byte) 8;
    numArray9[28] = (byte) 206;
    numArray9[29] = (byte) 11;
    numArray9[30] = (byte) 97;
    numArray9[46] = (byte) 136;
    numArray9[38] = (byte) 17;
    numArray9[33] = (byte) 174;
    numArray9[34] = (byte) 187;
    numArray9[35] = (byte) 90;
    numArray9[23] = (byte) 238;
    numArray9[37] = (byte) 39;
    numArray9[9] = (byte) 54;
    numArray9[36] = (byte) 226;
    numArray9[40] = (byte) 191;
    numArray9[41] = (byte) 118;
    numArray9[32 /*0x20*/] = (byte) 16 /*0x10*/;
    numArray9[43] = (byte) 177;
    numArray9[42] = (byte) 211;
    numArray9[27] = (byte) 198;
    numArray9[45] = (byte) 19;
    numArray9[47] = (byte) 234;
    numArray9[31 /*0x1F*/] = (byte) 123;
    numArray9[49] = (byte) 53;
    numArray9[50] = (byte) 62;
    numArray9[51] = (byte) 224 /*0xE0*/;
    numArray9[1] = (byte) 176 /*0xB0*/;
    numArray9[53] = (byte) 250;
    byte[] numArray10 = new byte[54]
    {
      (byte) 193,
      (byte) 254,
      (byte) 160 /*0xA0*/,
      (byte) 237,
      (byte) 75,
      (byte) 175,
      (byte) 50,
      (byte) 84,
      (byte) 59,
      (byte) 180,
      (byte) 138,
      (byte) 237,
      (byte) 51,
      (byte) 199,
      (byte) 148,
      (byte) 118,
      (byte) 165,
      (byte) 132,
      (byte) 155,
      (byte) 218,
      (byte) 231,
      (byte) 176 /*0xB0*/,
      (byte) 47,
      (byte) 190,
      (byte) 233,
      (byte) 17,
      (byte) 120,
      (byte) 43,
      (byte) 142,
      (byte) 4,
      (byte) 210,
      (byte) 145,
      (byte) 172,
      (byte) 69,
      (byte) 160 /*0xA0*/,
      (byte) 16 /*0x10*/,
      (byte) 109,
      (byte) 51,
      (byte) 65,
      (byte) 58,
      (byte) 217,
      (byte) 59,
      (byte) 105,
      (byte) 222,
      (byte) 211,
      (byte) 201,
      (byte) 146,
      (byte) 21,
      (byte) 184,
      (byte) 139,
      (byte) 131,
      (byte) 152,
      (byte) 232,
      (byte) 162
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 54);
    for (int index = 0; index < 54; ++index)
      numArray6[index + 55] ^= numArray10[index];
    byte[] numArray11 = new byte[34];
    byte[] response = new byte[34];
    Array.Copy((Array) sc_13035.sspq, 47, (Array) numArray11, 0, 34);
    key.Query(true, 335, numArray11, response);
    Array.Copy((Array) sc_13035.sspr, 47, (Array) numArray11, 0, 34);
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

  internal static int ssp_appserver_13045(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 81,
      (byte) 6,
      (byte) 200,
      (byte) 100,
      (byte) 243,
      (byte) 137,
      (byte) 158,
      (byte) 236,
      (byte) 119,
      (byte) 3,
      (byte) 114,
      (byte) 56,
      (byte) 228,
      (byte) 8,
      (byte) 150,
      (byte) 85,
      (byte) 141,
      (byte) 52,
      (byte) 62,
      (byte) 212,
      (byte) 28,
      (byte) 15,
      (byte) 241,
      (byte) 153,
      (byte) 236,
      (byte) 89,
      (byte) 32 /*0x20*/,
      (byte) 230,
      (byte) 41,
      (byte) 212,
      (byte) 70,
      (byte) 218,
      (byte) 212,
      (byte) 72,
      (byte) 244,
      (byte) 47,
      (byte) 153,
      (byte) 186,
      (byte) 55,
      (byte) 35,
      (byte) 43,
      (byte) 108,
      (byte) 115,
      (byte) 151,
      (byte) 13,
      (byte) 23,
      (byte) 74,
      (byte) 42
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[7] = (byte) 142;
    sourceArray2[46] = (byte) 189;
    sourceArray2[47] = (byte) 133;
    sourceArray2[19] = (byte) 133;
    sourceArray2[16 /*0x10*/] = (byte) 109;
    sourceArray2[15] = (byte) 153;
    sourceArray2[22] = (byte) 102;
    sourceArray2[5] = (byte) 59;
    sourceArray2[8] = (byte) 175;
    sourceArray2[9] = (byte) 168;
    sourceArray2[10] = (byte) 3;
    sourceArray2[29] = (byte) 29;
    sourceArray2[17] = (byte) 83;
    sourceArray2[44] = (byte) 87;
    sourceArray2[14] = (byte) 165;
    sourceArray2[41] = (byte) 130;
    sourceArray2[13] = (byte) 167;
    sourceArray2[24] = (byte) 33;
    sourceArray2[18] = (byte) 35;
    sourceArray2[6] = (byte) 5;
    sourceArray2[20] = (byte) 100;
    sourceArray2[2] = (byte) 16 /*0x10*/;
    sourceArray2[3] = (byte) 49;
    sourceArray2[23] = (byte) 5;
    sourceArray2[1] = (byte) 93;
    sourceArray2[12] = (byte) 70;
    sourceArray2[26] = (byte) 186;
    sourceArray2[27] = (byte) 183;
    sourceArray2[28] = (byte) 84;
    sourceArray2[0] = (byte) 8;
    sourceArray2[33] = (byte) 120;
    sourceArray2[31 /*0x1F*/] = (byte) 126;
    sourceArray2[11] = (byte) 234;
    sourceArray2[21] = (byte) 230;
    sourceArray2[34] = (byte) 230;
    sourceArray2[35] = (byte) 121;
    sourceArray2[40] = (byte) 127 /*0x7F*/;
    sourceArray2[32 /*0x20*/] = (byte) 232;
    sourceArray2[38] = (byte) 238;
    sourceArray2[39] = (byte) 37;
    sourceArray2[25] = (byte) 209;
    sourceArray2[37] = (byte) 133;
    sourceArray2[42] = (byte) 130;
    sourceArray2[43] = (byte) 236;
    sourceArray2[36] = (byte) 249;
    sourceArray2[45] = (byte) 9;
    sourceArray2[30] = (byte) 96 /*0x60*/;
    sourceArray2[4] = (byte) 88;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13046(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 174,
      (byte) 22,
      (byte) 28,
      (byte) 78,
      (byte) 50,
      (byte) 34,
      (byte) 159,
      (byte) 191,
      (byte) 165,
      (byte) 166,
      (byte) 186,
      (byte) 163,
      (byte) 163,
      (byte) 139,
      (byte) 116,
      (byte) 84,
      (byte) 163,
      (byte) 212,
      (byte) 150,
      (byte) 120,
      (byte) 248,
      (byte) 169,
      (byte) 98,
      (byte) 76,
      (byte) 81,
      (byte) 145,
      (byte) 94,
      (byte) 98,
      (byte) 53,
      (byte) 7,
      (byte) 204,
      (byte) 129,
      (byte) 22,
      (byte) 33,
      (byte) 96 /*0x60*/,
      (byte) 222,
      (byte) 70,
      (byte) 209,
      (byte) 157,
      (byte) 4,
      (byte) 127 /*0x7F*/,
      (byte) 172,
      (byte) 77,
      (byte) 11,
      (byte) 95,
      (byte) 198,
      (byte) 158,
      (byte) 18
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[39] = (byte) 135;
    sourceArray2[42] = (byte) 248;
    sourceArray2[2] = (byte) 58;
    sourceArray2[3] = (byte) 208 /*0xD0*/;
    sourceArray2[27] = (byte) 174;
    sourceArray2[34] = (byte) 107;
    sourceArray2[6] = (byte) 139;
    sourceArray2[33] = (byte) 101;
    sourceArray2[8] = (byte) 163;
    sourceArray2[9] = (byte) 23;
    sourceArray2[10] = (byte) 59;
    sourceArray2[0] = (byte) 59;
    sourceArray2[35] = (byte) 233;
    sourceArray2[26] = (byte) 3;
    sourceArray2[15] = (byte) 20;
    sourceArray2[24] = (byte) 247;
    sourceArray2[7] = (byte) 71;
    sourceArray2[38] = (byte) 37;
    sourceArray2[32 /*0x20*/] = (byte) 40;
    sourceArray2[44] = (byte) 93;
    sourceArray2[20] = (byte) 138;
    sourceArray2[5] = (byte) 246;
    sourceArray2[31 /*0x1F*/] = (byte) 151;
    sourceArray2[22] = (byte) 243;
    sourceArray2[17] = (byte) 234;
    sourceArray2[25] = (byte) 139;
    sourceArray2[30] = (byte) 20;
    sourceArray2[11] = (byte) 119;
    sourceArray2[28] = (byte) 210;
    sourceArray2[23] = (byte) 51;
    sourceArray2[14] = (byte) 253;
    sourceArray2[47] = (byte) 67;
    sourceArray2[21] = (byte) 229;
    sourceArray2[12] = (byte) 105;
    sourceArray2[13] = (byte) 121;
    sourceArray2[1] = (byte) 92;
    sourceArray2[36] = (byte) 248;
    sourceArray2[37] = (byte) 111;
    sourceArray2[4] = (byte) 159;
    sourceArray2[16 /*0x10*/] = (byte) 33;
    sourceArray2[40] = (byte) 82;
    sourceArray2[41] = (byte) 179;
    sourceArray2[43] = (byte) 88;
    sourceArray2[19] = (byte) 100;
    sourceArray2[29] = (byte) 45;
    sourceArray2[45] = (byte) 69;
    sourceArray2[46] = (byte) 142;
    sourceArray2[18] = (byte) 29;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13047(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[32 /*0x20*/] = (byte) 199;
    sourceArray1[1] = (byte) 231;
    sourceArray1[30] = (byte) 173;
    sourceArray1[36] = (byte) 109;
    sourceArray1[4] = (byte) 50;
    sourceArray1[42] = (byte) 246;
    sourceArray1[39] = (byte) 13;
    sourceArray1[38] = (byte) 206;
    sourceArray1[11] = (byte) 227;
    sourceArray1[3] = (byte) 17;
    sourceArray1[10] = (byte) 94;
    sourceArray1[46] = (byte) 98;
    sourceArray1[12] = (byte) 113;
    sourceArray1[22] = (byte) 208 /*0xD0*/;
    sourceArray1[14] = (byte) 119;
    sourceArray1[25] = (byte) 254;
    sourceArray1[16 /*0x10*/] = (byte) 114;
    sourceArray1[17] = (byte) 156;
    sourceArray1[18] = (byte) 8;
    sourceArray1[19] = (byte) 251;
    sourceArray1[20] = (byte) 114;
    sourceArray1[21] = (byte) 29;
    sourceArray1[33] = (byte) 81;
    sourceArray1[0] = (byte) 27;
    sourceArray1[5] = (byte) 195;
    sourceArray1[9] = (byte) 169;
    sourceArray1[13] = (byte) 173;
    sourceArray1[27] = (byte) 81;
    sourceArray1[23] = (byte) 107;
    sourceArray1[29] = (byte) 41;
    sourceArray1[24] = (byte) 150;
    sourceArray1[47] = (byte) 78;
    sourceArray1[6] = (byte) 176 /*0xB0*/;
    sourceArray1[45] = (byte) 147;
    sourceArray1[37] = (byte) 5;
    sourceArray1[35] = (byte) 100;
    sourceArray1[7] = (byte) 2;
    sourceArray1[8] = (byte) 111;
    sourceArray1[40] = (byte) 141;
    sourceArray1[31 /*0x1F*/] = (byte) 36;
    sourceArray1[41] = (byte) 24;
    sourceArray1[26] = (byte) 58;
    sourceArray1[34] = (byte) 214;
    sourceArray1[43] = (byte) 176 /*0xB0*/;
    sourceArray1[44] = (byte) 203;
    sourceArray1[15] = (byte) 32 /*0x20*/;
    sourceArray1[2] = (byte) 175;
    sourceArray1[28] = (byte) 183;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[30] = (byte) 167;
    sourceArray2[29] = (byte) 126;
    sourceArray2[2] = (byte) 60;
    sourceArray2[45] = (byte) 168;
    sourceArray2[12] = (byte) 12;
    sourceArray2[5] = (byte) 3;
    sourceArray2[20] = (byte) 157;
    sourceArray2[7] = (byte) 99;
    sourceArray2[11] = (byte) 19;
    sourceArray2[3] = (byte) 194;
    sourceArray2[10] = (byte) 45;
    sourceArray2[24] = (byte) 250;
    sourceArray2[9] = (byte) 238;
    sourceArray2[13] = (byte) 135;
    sourceArray2[35] = (byte) 195;
    sourceArray2[46] = (byte) 189;
    sourceArray2[0] = (byte) 95;
    sourceArray2[6] = (byte) 50;
    sourceArray2[31 /*0x1F*/] = (byte) 210;
    sourceArray2[8] = (byte) 41;
    sourceArray2[1] = (byte) 30;
    sourceArray2[40] = (byte) 110;
    sourceArray2[15] = (byte) 19;
    sourceArray2[14] = (byte) 81;
    sourceArray2[36] = (byte) 57;
    sourceArray2[25] = (byte) 155;
    sourceArray2[26] = (byte) 42;
    sourceArray2[27] = (byte) 66;
    sourceArray2[28] = (byte) 33;
    sourceArray2[32 /*0x20*/] = (byte) 114;
    sourceArray2[4] = (byte) 53;
    sourceArray2[17] = (byte) 227;
    sourceArray2[22] = (byte) 121;
    sourceArray2[33] = (byte) 218;
    sourceArray2[34] = (byte) 26;
    sourceArray2[39] = (byte) 73;
    sourceArray2[16 /*0x10*/] = (byte) 202;
    sourceArray2[37] = (byte) 134;
    sourceArray2[38] = (byte) 219;
    sourceArray2[19] = (byte) 66;
    sourceArray2[23] = (byte) 61;
    sourceArray2[41] = (byte) 153;
    sourceArray2[42] = (byte) 28;
    sourceArray2[43] = (byte) 199;
    sourceArray2[44] = (byte) 245;
    sourceArray2[21] = (byte) 161;
    sourceArray2[18] = (byte) 58;
    sourceArray2[47] = (byte) 144 /*0x90*/;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13048(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 231,
      (byte) 89,
      (byte) 24,
      (byte) 229,
      (byte) 82,
      (byte) 81,
      (byte) 248,
      (byte) 172,
      (byte) 119,
      (byte) 241,
      (byte) 230,
      (byte) 97,
      (byte) 16 /*0x10*/,
      (byte) 184,
      (byte) 125,
      (byte) 224 /*0xE0*/,
      (byte) 98,
      (byte) 138,
      (byte) 149,
      (byte) 234,
      (byte) 64 /*0x40*/,
      (byte) 39,
      (byte) 3,
      (byte) 141,
      (byte) 147,
      (byte) 10,
      (byte) 117,
      (byte) 156,
      (byte) 121,
      byte.MaxValue,
      (byte) 109,
      (byte) 1,
      (byte) 203,
      (byte) 51,
      (byte) 142,
      (byte) 229,
      (byte) 116,
      (byte) 144 /*0x90*/,
      (byte) 25,
      (byte) 188,
      (byte) 147,
      (byte) 172,
      (byte) 253,
      (byte) 225,
      (byte) 64 /*0x40*/,
      (byte) 213,
      (byte) 70,
      (byte) 17
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[3] = (byte) 43;
    sourceArray2[1] = (byte) 227;
    sourceArray2[2] = (byte) 94;
    sourceArray2[21] = (byte) 41;
    sourceArray2[13] = (byte) 196;
    sourceArray2[47] = (byte) 164;
    sourceArray2[6] = (byte) 254;
    sourceArray2[44] = (byte) 128 /*0x80*/;
    sourceArray2[8] = (byte) 76;
    sourceArray2[39] = (byte) 91;
    sourceArray2[22] = (byte) 81;
    sourceArray2[15] = (byte) 164;
    sourceArray2[12] = (byte) 241;
    sourceArray2[34] = (byte) 4;
    sourceArray2[19] = (byte) 9;
    sourceArray2[7] = (byte) 59;
    sourceArray2[16 /*0x10*/] = (byte) 133;
    sourceArray2[33] = (byte) 59;
    sourceArray2[18] = (byte) 46;
    sourceArray2[11] = (byte) 251;
    sourceArray2[20] = (byte) 27;
    sourceArray2[0] = (byte) 191;
    sourceArray2[46] = (byte) 155;
    sourceArray2[23] = byte.MaxValue;
    sourceArray2[24] = (byte) 249;
    sourceArray2[25] = (byte) 170;
    sourceArray2[26] = (byte) 0;
    sourceArray2[27] = (byte) 202;
    sourceArray2[28] = (byte) 253;
    sourceArray2[35] = (byte) 71;
    sourceArray2[30] = (byte) 161;
    sourceArray2[31 /*0x1F*/] = (byte) 86;
    sourceArray2[32 /*0x20*/] = (byte) 113;
    sourceArray2[17] = (byte) 27;
    sourceArray2[41] = (byte) 127 /*0x7F*/;
    sourceArray2[14] = (byte) 165;
    sourceArray2[36] = (byte) 28;
    sourceArray2[37] = (byte) 79;
    sourceArray2[38] = (byte) 155;
    sourceArray2[42] = (byte) 212;
    sourceArray2[40] = (byte) 56;
    sourceArray2[10] = (byte) 213;
    sourceArray2[29] = (byte) 105;
    sourceArray2[43] = (byte) 98;
    sourceArray2[5] = (byte) 46;
    sourceArray2[45] = (byte) 116;
    sourceArray2[4] = (byte) 65;
    sourceArray2[9] = (byte) 222;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13049(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[0] = (byte) 105;
    sourceArray1[1] = (byte) 231;
    sourceArray1[2] = (byte) 204;
    sourceArray1[3] = (byte) 208 /*0xD0*/;
    sourceArray1[28] = (byte) 31 /*0x1F*/;
    sourceArray1[5] = (byte) 92;
    sourceArray1[33] = (byte) 126;
    sourceArray1[7] = (byte) 212;
    sourceArray1[8] = (byte) 43;
    sourceArray1[44] = (byte) 90;
    sourceArray1[10] = (byte) 52;
    sourceArray1[11] = (byte) 62;
    sourceArray1[15] = (byte) 34;
    sourceArray1[6] = (byte) 24;
    sourceArray1[47] = (byte) 78;
    sourceArray1[41] = (byte) 203;
    sourceArray1[43] = (byte) 54;
    sourceArray1[12] = (byte) 147;
    sourceArray1[24] = (byte) 130;
    sourceArray1[19] = (byte) 212;
    sourceArray1[20] = (byte) 245;
    sourceArray1[21] = (byte) 105;
    sourceArray1[22] = (byte) 92;
    sourceArray1[23] = (byte) 8;
    sourceArray1[16 /*0x10*/] = (byte) 246;
    sourceArray1[39] = (byte) 68;
    sourceArray1[25] = (byte) 146;
    sourceArray1[27] = (byte) 248;
    sourceArray1[17] = (byte) 21;
    sourceArray1[29] = (byte) 27;
    sourceArray1[30] = (byte) 164;
    sourceArray1[14] = (byte) 121;
    sourceArray1[34] = (byte) 91;
    sourceArray1[31 /*0x1F*/] = (byte) 196;
    sourceArray1[13] = (byte) 131;
    sourceArray1[35] = (byte) 123;
    sourceArray1[36] = (byte) 108;
    sourceArray1[18] = (byte) 228;
    sourceArray1[38] = (byte) 150;
    sourceArray1[32 /*0x20*/] = (byte) 240 /*0xF0*/;
    sourceArray1[40] = (byte) 247;
    sourceArray1[9] = (byte) 151;
    sourceArray1[42] = (byte) 71;
    sourceArray1[26] = (byte) 158;
    sourceArray1[46] = (byte) 171;
    sourceArray1[45] = (byte) 170;
    sourceArray1[37] = (byte) 107;
    sourceArray1[4] = (byte) 203;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 164,
      (byte) 111,
      (byte) 204,
      (byte) 129,
      (byte) 128 /*0x80*/,
      (byte) 113,
      (byte) 132,
      (byte) 217,
      (byte) 203,
      (byte) 202,
      (byte) 47,
      (byte) 11,
      (byte) 194,
      (byte) 154,
      (byte) 206,
      (byte) 226,
      (byte) 152,
      (byte) 62,
      (byte) 35,
      (byte) 252,
      (byte) 246,
      (byte) 254,
      (byte) 104,
      (byte) 248,
      (byte) 98,
      (byte) 65,
      (byte) 247,
      (byte) 183,
      (byte) 159,
      (byte) 198,
      (byte) 242,
      (byte) 182,
      (byte) 179,
      (byte) 101,
      (byte) 235,
      (byte) 203,
      (byte) 7,
      (byte) 77,
      (byte) 20,
      (byte) 193,
      (byte) 69,
      (byte) 12,
      (byte) 23,
      (byte) 244,
      (byte) 5,
      (byte) 107,
      (byte) 131,
      (byte) 1
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_13050()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[135];
      byte[] numArray2 = new byte[55]
      {
        (byte) 92,
        (byte) 151,
        (byte) 39,
        (byte) 105,
        (byte) 94,
        (byte) 247,
        (byte) 240 /*0xF0*/,
        (byte) 217,
        (byte) 170,
        (byte) 31 /*0x1F*/,
        (byte) 220,
        (byte) 102,
        (byte) 21,
        (byte) 119,
        (byte) 82,
        (byte) 109,
        (byte) 141,
        (byte) 170,
        (byte) 119,
        (byte) 189,
        (byte) 36,
        (byte) 229,
        (byte) 223,
        (byte) 203,
        (byte) 14,
        (byte) 12,
        (byte) 110,
        (byte) 34,
        (byte) 25,
        (byte) 5,
        (byte) 19,
        (byte) 234,
        (byte) 224 /*0xE0*/,
        (byte) 194,
        (byte) 123,
        (byte) 79,
        (byte) 40,
        (byte) 40,
        (byte) 172,
        (byte) 161,
        (byte) 68,
        (byte) 114,
        (byte) 190,
        (byte) 21,
        (byte) 127 /*0x7F*/,
        (byte) 137,
        (byte) 148,
        (byte) 165,
        (byte) 11,
        (byte) 25,
        (byte) 73,
        (byte) 107,
        (byte) 191,
        (byte) 134,
        (byte) 198
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 207,
        (byte) 28,
        (byte) 150,
        (byte) 215,
        byte.MaxValue,
        (byte) 40,
        (byte) 102,
        (byte) 234,
        (byte) 128 /*0x80*/,
        (byte) 181,
        (byte) 101,
        (byte) 243,
        (byte) 242,
        (byte) 74,
        (byte) 154,
        (byte) 76,
        (byte) 159,
        (byte) 174,
        (byte) 52,
        (byte) 196,
        (byte) 114,
        (byte) 232,
        (byte) 58,
        (byte) 177,
        (byte) 6,
        (byte) 101,
        (byte) 114,
        (byte) 249,
        (byte) 165,
        (byte) 127 /*0x7F*/,
        (byte) 106,
        (byte) 76,
        (byte) 59,
        (byte) 209,
        (byte) 111,
        (byte) 38,
        (byte) 107,
        (byte) 232,
        (byte) 170,
        (byte) 78,
        (byte) 223,
        (byte) 192 /*0xC0*/,
        (byte) 147,
        (byte) 240 /*0xF0*/,
        (byte) 145,
        (byte) 15,
        (byte) 206,
        (byte) 218,
        (byte) 99,
        (byte) 70,
        (byte) 122,
        (byte) 32 /*0x20*/,
        (byte) 24,
        (byte) 150,
        (byte) 222
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 91,
        (byte) 91,
        (byte) 81,
        (byte) 136,
        (byte) 206,
        (byte) 192 /*0xC0*/,
        (byte) 164,
        (byte) 12,
        (byte) 242,
        (byte) 133,
        (byte) 82,
        (byte) 252,
        (byte) 2,
        (byte) 68,
        (byte) 101,
        (byte) 254,
        (byte) 213,
        (byte) 121,
        (byte) 177,
        (byte) 38,
        (byte) 201,
        (byte) 76,
        (byte) 132,
        (byte) 120,
        (byte) 200,
        (byte) 23,
        byte.MaxValue,
        (byte) 188,
        (byte) 167,
        (byte) 42,
        (byte) 90,
        (byte) 252,
        (byte) 3,
        (byte) 151,
        (byte) 204,
        (byte) 205,
        (byte) 90,
        (byte) 25,
        (byte) 118,
        (byte) 178,
        (byte) 26,
        (byte) 9,
        (byte) 141,
        (byte) 3,
        (byte) 65,
        (byte) 169,
        (byte) 8,
        (byte) 212,
        (byte) 228,
        (byte) 109,
        (byte) 234,
        (byte) 13,
        (byte) 21,
        (byte) 80 /*0x50*/,
        (byte) 238
      };
      byte[] numArray5 = new byte[55]
      {
        (byte) 157,
        (byte) 217,
        (byte) 155,
        (byte) 62,
        (byte) 36,
        (byte) 168,
        (byte) 123,
        (byte) 92,
        (byte) 196,
        (byte) 69,
        (byte) 82,
        (byte) 207,
        (byte) 51,
        (byte) 177,
        (byte) 248,
        (byte) 59,
        (byte) 28,
        (byte) 5,
        (byte) 123,
        (byte) 136,
        (byte) 182,
        (byte) 222,
        (byte) 216,
        (byte) 197,
        (byte) 52,
        (byte) 234,
        (byte) 66,
        (byte) 63 /*0x3F*/,
        (byte) 232,
        (byte) 178,
        (byte) 143,
        (byte) 87,
        (byte) 148,
        (byte) 77,
        (byte) 242,
        (byte) 212,
        (byte) 199,
        (byte) 145,
        (byte) 117,
        (byte) 183,
        (byte) 78,
        (byte) 78,
        (byte) 42,
        (byte) 244,
        (byte) 194,
        (byte) 1,
        (byte) 236,
        (byte) 25,
        (byte) 141,
        (byte) 22,
        (byte) 231,
        (byte) 50,
        (byte) 108,
        (byte) 154,
        (byte) 118
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[25];
      numArray6[12] = (byte) 151;
      numArray6[1] = (byte) 191;
      numArray6[11] = (byte) 120;
      numArray6[3] = (byte) 192 /*0xC0*/;
      numArray6[9] = (byte) 248;
      numArray6[4] = (byte) 231;
      numArray6[23] = (byte) 14;
      numArray6[2] = (byte) 183;
      numArray6[8] = (byte) 86;
      numArray6[7] = (byte) 42;
      numArray6[10] = (byte) 107;
      numArray6[19] = (byte) 190;
      numArray6[20] = (byte) 129;
      numArray6[13] = (byte) 163;
      numArray6[14] = (byte) 188;
      numArray6[15] = (byte) 2;
      numArray6[5] = (byte) 100;
      numArray6[18] = (byte) 192 /*0xC0*/;
      numArray6[16 /*0x10*/] = (byte) 254;
      numArray6[22] = (byte) 31 /*0x1F*/;
      numArray6[17] = (byte) 11;
      numArray6[21] = (byte) 54;
      numArray6[6] = (byte) 199;
      numArray6[0] = (byte) 217;
      numArray6[24] = (byte) 43;
      byte[] numArray7 = new byte[25]
      {
        (byte) 113,
        (byte) 233,
        (byte) 114,
        (byte) 217,
        (byte) 4,
        (byte) 102,
        (byte) 146,
        (byte) 153,
        (byte) 110,
        (byte) 210,
        (byte) 250,
        (byte) 68,
        (byte) 76,
        (byte) 62,
        (byte) 239,
        (byte) 33,
        (byte) 159,
        (byte) 132,
        (byte) 60,
        (byte) 100,
        (byte) 159,
        (byte) 198,
        (byte) 148,
        byte.MaxValue,
        (byte) 143
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 25);
      for (int index = 0; index < 25; ++index)
        numArray1[index + 110] ^= numArray7[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray8 = new byte[135];
    byte[] numArray9 = new byte[55]
    {
      (byte) 80 /*0x50*/,
      (byte) 85,
      (byte) 162,
      (byte) 160 /*0xA0*/,
      (byte) 168,
      (byte) 149,
      (byte) 16 /*0x10*/,
      (byte) 42,
      (byte) 135,
      (byte) 221,
      (byte) 111,
      (byte) 69,
      (byte) 194,
      (byte) 186,
      (byte) 160 /*0xA0*/,
      (byte) 34,
      (byte) 98,
      (byte) 67,
      (byte) 12,
      (byte) 216,
      (byte) 27,
      (byte) 249,
      (byte) 168,
      (byte) 75,
      (byte) 110,
      (byte) 113,
      (byte) 206,
      (byte) 139,
      (byte) 112 /*0x70*/,
      (byte) 18,
      (byte) 66,
      (byte) 207,
      (byte) 109,
      (byte) 121,
      (byte) 108,
      (byte) 161,
      (byte) 222,
      (byte) 185,
      (byte) 23,
      (byte) 93,
      (byte) 34,
      (byte) 121,
      (byte) 62,
      (byte) 250,
      (byte) 123,
      (byte) 107,
      (byte) 217,
      (byte) 161,
      (byte) 172,
      (byte) 167,
      (byte) 202,
      (byte) 199,
      (byte) 10,
      (byte) 108,
      (byte) 18
    };
    byte[] numArray10 = new byte[55]
    {
      (byte) 59,
      (byte) 3,
      (byte) 14,
      (byte) 218,
      (byte) 91,
      (byte) 86,
      (byte) 114,
      (byte) 241,
      (byte) 78,
      (byte) 13,
      (byte) 104,
      (byte) 185,
      (byte) 72,
      (byte) 131,
      (byte) 97,
      (byte) 226,
      (byte) 123,
      (byte) 249,
      (byte) 171,
      (byte) 34,
      (byte) 148,
      (byte) 184,
      (byte) 129,
      (byte) 15,
      (byte) 182,
      (byte) 201,
      (byte) 210,
      (byte) 130,
      (byte) 190,
      (byte) 151,
      (byte) 200,
      (byte) 118,
      (byte) 138,
      (byte) 156,
      (byte) 37,
      (byte) 69,
      (byte) 118,
      (byte) 222,
      (byte) 67,
      (byte) 89,
      (byte) 110,
      (byte) 39,
      (byte) 58,
      (byte) 226,
      (byte) 138,
      byte.MaxValue,
      (byte) 58,
      (byte) 188,
      (byte) 164,
      (byte) 126,
      (byte) 138,
      (byte) 132,
      (byte) 45,
      (byte) 125,
      (byte) 248
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray8, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index] ^= numArray10[index];
    byte[] numArray11 = new byte[55]
    {
      (byte) 87,
      (byte) 50,
      (byte) 54,
      (byte) 198,
      (byte) 133,
      (byte) 186,
      (byte) 116,
      (byte) 155,
      (byte) 160 /*0xA0*/,
      (byte) 216,
      (byte) 73,
      (byte) 77,
      (byte) 254,
      (byte) 67,
      (byte) 169,
      (byte) 43,
      (byte) 175,
      (byte) 181,
      (byte) 162,
      (byte) 186,
      (byte) 173,
      (byte) 207,
      (byte) 2,
      (byte) 152,
      (byte) 45,
      (byte) 222,
      (byte) 201,
      (byte) 71,
      (byte) 122,
      (byte) 197,
      (byte) 0,
      (byte) 102,
      (byte) 21,
      (byte) 244,
      (byte) 135,
      (byte) 135,
      (byte) 224 /*0xE0*/,
      (byte) 150,
      (byte) 157,
      (byte) 190,
      (byte) 202,
      (byte) 95,
      (byte) 219,
      (byte) 147,
      (byte) 144 /*0x90*/,
      (byte) 83,
      (byte) 173,
      (byte) 99,
      (byte) 33,
      (byte) 156,
      (byte) 91,
      (byte) 182,
      (byte) 112 /*0x70*/,
      (byte) 156,
      byte.MaxValue
    };
    byte[] numArray12 = new byte[55];
    numArray12[51] = (byte) 169;
    numArray12[1] = (byte) 219;
    numArray12[2] = (byte) 19;
    numArray12[3] = (byte) 86;
    numArray12[4] = (byte) 17;
    numArray12[13] = (byte) 90;
    numArray12[6] = (byte) 88;
    numArray12[7] = (byte) 20;
    numArray12[46] = (byte) 200;
    numArray12[5] = (byte) 228;
    numArray12[8] = (byte) 60;
    numArray12[49] = (byte) 207;
    numArray12[12] = (byte) 142;
    numArray12[11] = (byte) 143;
    numArray12[0] = (byte) 110;
    numArray12[15] = (byte) 17;
    numArray12[19] = (byte) 102;
    numArray12[10] = (byte) 133;
    numArray12[28] = (byte) 157;
    numArray12[42] = (byte) 21;
    numArray12[20] = (byte) 154;
    numArray12[21] = (byte) 50;
    numArray12[22] = (byte) 245;
    numArray12[32 /*0x20*/] = (byte) 89;
    numArray12[48 /*0x30*/] = (byte) 16 /*0x10*/;
    numArray12[24] = (byte) 89;
    numArray12[26] = (byte) 230;
    numArray12[27] = (byte) 30;
    numArray12[45] = (byte) 57;
    numArray12[29] = (byte) 190;
    numArray12[30] = (byte) 123;
    numArray12[9] = (byte) 229;
    numArray12[40] = (byte) 187;
    numArray12[44] = (byte) 233;
    numArray12[53] = (byte) 103;
    numArray12[50] = (byte) 169;
    numArray12[31 /*0x1F*/] = (byte) 164;
    numArray12[37] = (byte) 165;
    numArray12[18] = (byte) 36;
    numArray12[39] = (byte) 50;
    numArray12[35] = (byte) 240 /*0xF0*/;
    numArray12[41] = (byte) 99;
    numArray12[38] = (byte) 84;
    numArray12[43] = (byte) 47;
    numArray12[14] = (byte) 33;
    numArray12[36] = (byte) 209;
    numArray12[16 /*0x10*/] = (byte) 76;
    numArray12[47] = (byte) 19;
    numArray12[33] = (byte) 130;
    numArray12[23] = (byte) 202;
    numArray12[17] = (byte) 187;
    numArray12[25] = (byte) 98;
    numArray12[52] = (byte) 21;
    numArray12[34] = (byte) 107;
    numArray12[54] = (byte) 7;
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray8, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index + 55] ^= numArray12[index];
    byte[] numArray13 = new byte[25]
    {
      (byte) 150,
      (byte) 191,
      (byte) 116,
      (byte) 139,
      (byte) 6,
      (byte) 67,
      (byte) 112 /*0x70*/,
      (byte) 142,
      (byte) 236,
      (byte) 191,
      (byte) 20,
      (byte) 188,
      (byte) 203,
      (byte) 8,
      (byte) 26,
      (byte) 32 /*0x20*/,
      (byte) 21,
      (byte) 82,
      (byte) 16 /*0x10*/,
      (byte) 244,
      (byte) 179,
      (byte) 89,
      (byte) 189,
      (byte) 141,
      (byte) 191
    };
    byte[] numArray14 = new byte[25]
    {
      (byte) 10,
      (byte) 230,
      (byte) 218,
      (byte) 209,
      (byte) 14,
      (byte) 165,
      (byte) 57,
      (byte) 13,
      (byte) 94,
      (byte) 77,
      (byte) 83,
      (byte) 252,
      (byte) 215,
      (byte) 74,
      (byte) 146,
      (byte) 189,
      (byte) 108,
      (byte) 98,
      (byte) 109,
      (byte) 123,
      (byte) 131,
      (byte) 136,
      (byte) 211,
      (byte) 12,
      (byte) 166
    };
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray8, 110, 25);
    for (int index = 0; index < 25; ++index)
      numArray8[index + 110] ^= numArray14[index];
    return Encoding.UTF8.GetString(numArray8);
  }
}
