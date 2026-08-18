// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_12860
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_12860
{
  private static byte[] sspq = new byte[52]
  {
    (byte) 31 /*0x1F*/,
    (byte) 74,
    (byte) 206,
    (byte) 206,
    (byte) 120,
    (byte) 63 /*0x3F*/,
    (byte) 221,
    (byte) 157,
    (byte) 60,
    (byte) 136,
    (byte) 6,
    (byte) 228,
    (byte) 0,
    (byte) 45,
    (byte) 2,
    (byte) 148,
    (byte) 243,
    (byte) 29,
    (byte) 200,
    (byte) 205,
    (byte) 198,
    (byte) 8,
    (byte) 102,
    (byte) 37,
    (byte) 234,
    (byte) 4,
    (byte) 212,
    (byte) 40,
    (byte) 161,
    (byte) 51,
    (byte) 225,
    (byte) 58,
    (byte) 89,
    (byte) 69,
    (byte) 35,
    (byte) 155,
    (byte) 218,
    (byte) 74,
    (byte) 252,
    (byte) 56,
    (byte) 251,
    (byte) 242,
    (byte) 141,
    (byte) 130,
    (byte) 70,
    (byte) 72,
    (byte) 149,
    (byte) 30,
    (byte) 202,
    (byte) 12,
    (byte) 198,
    (byte) 245
  };
  private static byte[] sspr = new byte[52]
  {
    (byte) 110,
    (byte) 106,
    (byte) 174,
    (byte) 168,
    (byte) 130,
    (byte) 183,
    (byte) 93,
    (byte) 47,
    (byte) 251,
    (byte) 77,
    (byte) 88,
    (byte) 66,
    (byte) 62,
    (byte) 248,
    (byte) 96 /*0x60*/,
    (byte) 124,
    (byte) 54,
    (byte) 97,
    (byte) 126,
    (byte) 76,
    (byte) 130,
    (byte) 104,
    (byte) 96 /*0x60*/,
    (byte) 154,
    (byte) 201,
    (byte) 75,
    (byte) 66,
    (byte) 8,
    (byte) 182,
    (byte) 218,
    (byte) 165,
    (byte) 43,
    (byte) 32 /*0x20*/,
    (byte) 31 /*0x1F*/,
    (byte) 242,
    (byte) 213,
    (byte) 12,
    (byte) 242,
    (byte) 179,
    (byte) 194,
    (byte) 74,
    (byte) 14,
    (byte) 151,
    (byte) 1,
    (byte) 97,
    (byte) 195,
    (byte) 192 /*0xC0*/,
    (byte) 178,
    (byte) 192 /*0xC0*/,
    (byte) 226,
    (byte) 142,
    (byte) 113
  };

  internal static int ssp_appserver_12861(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 132,
      (byte) 226,
      (byte) 169,
      (byte) 98,
      (byte) 234,
      (byte) 103,
      (byte) 96 /*0x60*/,
      (byte) 4,
      (byte) 16 /*0x10*/,
      (byte) 168,
      (byte) 227,
      (byte) 182,
      (byte) 143,
      (byte) 148,
      (byte) 108,
      (byte) 11,
      (byte) 82,
      (byte) 84,
      (byte) 44,
      (byte) 11,
      (byte) 52,
      (byte) 24,
      (byte) 234,
      (byte) 176 /*0xB0*/,
      (byte) 163,
      (byte) 98,
      (byte) 167,
      (byte) 134,
      (byte) 234,
      (byte) 12,
      (byte) 113,
      (byte) 134,
      (byte) 235,
      (byte) 12,
      (byte) 7,
      (byte) 10,
      (byte) 160 /*0xA0*/,
      (byte) 145,
      (byte) 52,
      (byte) 41,
      (byte) 17,
      (byte) 190,
      (byte) 58,
      (byte) 70,
      (byte) 52,
      (byte) 172,
      (byte) 194,
      (byte) 88
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[36] = (byte) 179;
    sourceArray2[1] = (byte) 225;
    sourceArray2[2] = (byte) 47;
    sourceArray2[18] = (byte) 39;
    sourceArray2[4] = (byte) 155;
    sourceArray2[23] = (byte) 74;
    sourceArray2[31 /*0x1F*/] = (byte) 66;
    sourceArray2[7] = (byte) 106;
    sourceArray2[8] = (byte) 222;
    sourceArray2[9] = (byte) 195;
    sourceArray2[16 /*0x10*/] = (byte) 145;
    sourceArray2[11] = (byte) 174;
    sourceArray2[15] = (byte) 232;
    sourceArray2[28] = (byte) 237;
    sourceArray2[10] = (byte) 246;
    sourceArray2[13] = (byte) 29;
    sourceArray2[26] = (byte) 20;
    sourceArray2[19] = (byte) 140;
    sourceArray2[3] = (byte) 119;
    sourceArray2[20] = (byte) 107;
    sourceArray2[22] = (byte) 165;
    sourceArray2[6] = (byte) 133;
    sourceArray2[43] = (byte) 41;
    sourceArray2[21] = (byte) 99;
    sourceArray2[14] = (byte) 122;
    sourceArray2[25] = (byte) 149;
    sourceArray2[45] = (byte) 228;
    sourceArray2[27] = (byte) 84;
    sourceArray2[39] = (byte) 80 /*0x50*/;
    sourceArray2[29] = (byte) 84;
    sourceArray2[46] = (byte) 118;
    sourceArray2[12] = (byte) 156;
    sourceArray2[32 /*0x20*/] = (byte) 67;
    sourceArray2[33] = (byte) 252;
    sourceArray2[34] = (byte) 119;
    sourceArray2[35] = (byte) 29;
    sourceArray2[37] = (byte) 18;
    sourceArray2[17] = (byte) 218;
    sourceArray2[38] = (byte) 108;
    sourceArray2[42] = (byte) 69;
    sourceArray2[40] = (byte) 77;
    sourceArray2[41] = (byte) 92;
    sourceArray2[47] = (byte) 183;
    sourceArray2[30] = (byte) 244;
    sourceArray2[44] = (byte) 246;
    sourceArray2[24] = (byte) 127 /*0x7F*/;
    sourceArray2[0] = (byte) 126;
    sourceArray2[5] = (byte) 172;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12862(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 63 /*0x3F*/,
      (byte) 203,
      (byte) 122,
      (byte) 111,
      (byte) 104,
      (byte) 208 /*0xD0*/,
      (byte) 136,
      (byte) 98,
      (byte) 212,
      (byte) 142,
      (byte) 84,
      (byte) 125,
      (byte) 200,
      (byte) 232,
      (byte) 113,
      (byte) 47,
      (byte) 9,
      (byte) 31 /*0x1F*/,
      (byte) 182,
      (byte) 97,
      (byte) 60,
      (byte) 192 /*0xC0*/,
      (byte) 241,
      (byte) 80 /*0x50*/,
      (byte) 196,
      (byte) 238,
      (byte) 115,
      (byte) 154,
      (byte) 193,
      (byte) 208 /*0xD0*/,
      (byte) 166,
      (byte) 148,
      (byte) 164,
      (byte) 21,
      (byte) 96 /*0x60*/,
      (byte) 130,
      (byte) 46,
      (byte) 160 /*0xA0*/,
      (byte) 123,
      (byte) 176 /*0xB0*/,
      (byte) 114,
      (byte) 44,
      (byte) 237,
      (byte) 12,
      (byte) 190,
      (byte) 41,
      (byte) 9,
      (byte) 195
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[38] = (byte) 113;
    sourceArray2[0] = (byte) 137;
    sourceArray2[22] = (byte) 57;
    sourceArray2[3] = (byte) 218;
    sourceArray2[31 /*0x1F*/] = (byte) 156;
    sourceArray2[5] = (byte) 39;
    sourceArray2[6] = (byte) 177;
    sourceArray2[9] = (byte) 122;
    sourceArray2[42] = (byte) 231;
    sourceArray2[25] = (byte) 85;
    sourceArray2[46] = (byte) 216;
    sourceArray2[11] = (byte) 67;
    sourceArray2[13] = (byte) 176 /*0xB0*/;
    sourceArray2[2] = (byte) 243;
    sourceArray2[14] = (byte) 84;
    sourceArray2[16 /*0x10*/] = (byte) 121;
    sourceArray2[26] = (byte) 67;
    sourceArray2[44] = (byte) 192 /*0xC0*/;
    sourceArray2[18] = (byte) 213;
    sourceArray2[19] = (byte) 207;
    sourceArray2[20] = (byte) 28;
    sourceArray2[43] = (byte) 91;
    sourceArray2[8] = (byte) 164;
    sourceArray2[23] = (byte) 217;
    sourceArray2[24] = (byte) 65;
    sourceArray2[15] = (byte) 54;
    sourceArray2[30] = (byte) 243;
    sourceArray2[12] = (byte) 77;
    sourceArray2[28] = (byte) 45;
    sourceArray2[29] = (byte) 45;
    sourceArray2[7] = (byte) 21;
    sourceArray2[47] = (byte) 151;
    sourceArray2[32 /*0x20*/] = (byte) 107;
    sourceArray2[35] = (byte) 128 /*0x80*/;
    sourceArray2[34] = (byte) 183;
    sourceArray2[4] = (byte) 57;
    sourceArray2[36] = (byte) 37;
    sourceArray2[33] = (byte) 110;
    sourceArray2[45] = (byte) 10;
    sourceArray2[39] = (byte) 0;
    sourceArray2[40] = (byte) 83;
    sourceArray2[1] = (byte) 248;
    sourceArray2[10] = (byte) 219;
    sourceArray2[37] = (byte) 195;
    sourceArray2[17] = (byte) 224 /*0xE0*/;
    sourceArray2[41] = (byte) 7;
    sourceArray2[27] = (byte) 138;
    sourceArray2[21] = (byte) 99;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12863(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 85,
      (byte) 60,
      (byte) 7,
      (byte) 7,
      (byte) 51,
      (byte) 114,
      (byte) 82,
      (byte) 135,
      (byte) 148,
      (byte) 173,
      (byte) 245,
      (byte) 152,
      (byte) 15,
      (byte) 149,
      (byte) 172,
      (byte) 73,
      (byte) 232,
      (byte) 208 /*0xD0*/,
      (byte) 88,
      (byte) 132,
      (byte) 170,
      (byte) 67,
      (byte) 167,
      (byte) 70,
      (byte) 252,
      (byte) 115,
      (byte) 163,
      (byte) 178,
      (byte) 104,
      (byte) 132,
      (byte) 233,
      (byte) 192 /*0xC0*/,
      (byte) 148,
      (byte) 74,
      (byte) 143,
      (byte) 115,
      (byte) 155,
      (byte) 176 /*0xB0*/,
      (byte) 0,
      (byte) 29,
      (byte) 232,
      (byte) 166,
      (byte) 219,
      (byte) 50,
      (byte) 179,
      (byte) 107,
      (byte) 229,
      (byte) 107
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 142,
      (byte) 62,
      (byte) 122,
      (byte) 129,
      (byte) 157,
      (byte) 84,
      (byte) 171,
      (byte) 113,
      (byte) 88,
      (byte) 218,
      (byte) 72,
      (byte) 208 /*0xD0*/,
      (byte) 10,
      (byte) 149,
      (byte) 131,
      (byte) 55,
      (byte) 203,
      (byte) 28,
      (byte) 163,
      (byte) 87,
      (byte) 133,
      (byte) 155,
      (byte) 197,
      (byte) 250,
      (byte) 48 /*0x30*/,
      (byte) 92,
      (byte) 233,
      (byte) 212,
      (byte) 115,
      (byte) 134,
      (byte) 62,
      (byte) 166,
      (byte) 129,
      (byte) 165,
      (byte) 245,
      (byte) 137,
      (byte) 177,
      (byte) 119,
      (byte) 235,
      (byte) 66,
      (byte) 212,
      (byte) 248,
      (byte) 241,
      (byte) 195,
      (byte) 75,
      (byte) 88,
      (byte) 123,
      (byte) 18
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12864(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[11] = (byte) 195;
    sourceArray1[3] = (byte) 148;
    sourceArray1[46] = (byte) 126;
    sourceArray1[2] = (byte) 17;
    sourceArray1[4] = (byte) 193;
    sourceArray1[30] = (byte) 44;
    sourceArray1[17] = (byte) 173;
    sourceArray1[7] = (byte) 206;
    sourceArray1[19] = (byte) 207;
    sourceArray1[18] = (byte) 55;
    sourceArray1[24] = (byte) 116;
    sourceArray1[33] = (byte) 244;
    sourceArray1[5] = (byte) 11;
    sourceArray1[13] = (byte) 4;
    sourceArray1[14] = (byte) 197;
    sourceArray1[15] = (byte) 5;
    sourceArray1[16 /*0x10*/] = (byte) 56;
    sourceArray1[6] = (byte) 192 /*0xC0*/;
    sourceArray1[1] = (byte) 166;
    sourceArray1[22] = (byte) 4;
    sourceArray1[20] = (byte) 58;
    sourceArray1[31 /*0x1F*/] = (byte) 226;
    sourceArray1[38] = (byte) 218;
    sourceArray1[23] = (byte) 104;
    sourceArray1[26] = (byte) 144 /*0x90*/;
    sourceArray1[29] = (byte) 11;
    sourceArray1[9] = (byte) 31 /*0x1F*/;
    sourceArray1[41] = (byte) 78;
    sourceArray1[21] = (byte) 156;
    sourceArray1[8] = (byte) 160 /*0xA0*/;
    sourceArray1[25] = (byte) 147;
    sourceArray1[28] = (byte) 55;
    sourceArray1[32 /*0x20*/] = (byte) 209;
    sourceArray1[36] = (byte) 125;
    sourceArray1[34] = (byte) 26;
    sourceArray1[35] = (byte) 164;
    sourceArray1[10] = (byte) 16 /*0x10*/;
    sourceArray1[37] = (byte) 223;
    sourceArray1[47] = (byte) 103;
    sourceArray1[39] = (byte) 165;
    sourceArray1[40] = (byte) 64 /*0x40*/;
    sourceArray1[12] = (byte) 52;
    sourceArray1[42] = (byte) 72;
    sourceArray1[43] = (byte) 135;
    sourceArray1[44] = (byte) 203;
    sourceArray1[45] = (byte) 111;
    sourceArray1[27] = (byte) 141;
    sourceArray1[0] = (byte) 121;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 153,
      (byte) 144 /*0x90*/,
      (byte) 3,
      (byte) 202,
      (byte) 87,
      (byte) 25,
      (byte) 0,
      (byte) 16 /*0x10*/,
      (byte) 45,
      (byte) 131,
      (byte) 53,
      (byte) 134,
      (byte) 7,
      (byte) 158,
      byte.MaxValue,
      (byte) 204,
      (byte) 187,
      (byte) 196,
      (byte) 228,
      (byte) 218,
      (byte) 176 /*0xB0*/,
      (byte) 162,
      (byte) 189,
      (byte) 185,
      (byte) 224 /*0xE0*/,
      (byte) 170,
      (byte) 39,
      (byte) 193,
      (byte) 241,
      (byte) 140,
      (byte) 46,
      (byte) 189,
      (byte) 79,
      (byte) 49,
      (byte) 209,
      (byte) 209,
      (byte) 212,
      (byte) 160 /*0xA0*/,
      (byte) 10,
      (byte) 107,
      (byte) 126,
      (byte) 10,
      (byte) 229,
      (byte) 247,
      (byte) 216,
      (byte) 172,
      (byte) 39,
      (byte) 251
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_12865()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[33];
      byte[] numArray2 = new byte[33];
      numArray2[32 /*0x20*/] = (byte) 232;
      numArray2[2] = (byte) 126;
      numArray2[21] = (byte) 105;
      numArray2[3] = (byte) 254;
      numArray2[7] = (byte) 216;
      numArray2[5] = (byte) 21;
      numArray2[6] = (byte) 187;
      numArray2[0] = (byte) 125;
      numArray2[9] = (byte) 165;
      numArray2[30] = (byte) 52;
      numArray2[10] = (byte) 136;
      numArray2[11] = (byte) 36;
      numArray2[12] = (byte) 12;
      numArray2[13] = (byte) 26;
      numArray2[26] = (byte) 153;
      numArray2[15] = (byte) 81;
      numArray2[16 /*0x10*/] = (byte) 197;
      numArray2[18] = (byte) 248;
      numArray2[4] = (byte) 221;
      numArray2[14] = (byte) 23;
      numArray2[20] = (byte) 182;
      numArray2[31 /*0x1F*/] = (byte) 160 /*0xA0*/;
      numArray2[22] = (byte) 221;
      numArray2[23] = (byte) 171;
      numArray2[24] = (byte) 31 /*0x1F*/;
      numArray2[27] = (byte) 37;
      numArray2[17] = (byte) 197;
      numArray2[8] = (byte) 185;
      numArray2[28] = (byte) 103;
      numArray2[29] = (byte) 129;
      numArray2[25] = (byte) 180;
      numArray2[19] = (byte) 84;
      numArray2[1] = (byte) 61;
      byte[] numArray3 = new byte[33]
      {
        (byte) 105,
        (byte) 76,
        (byte) 237,
        (byte) 112 /*0x70*/,
        (byte) 81,
        (byte) 184,
        (byte) 251,
        (byte) 120,
        (byte) 29,
        (byte) 70,
        (byte) 83,
        (byte) 216,
        (byte) 167,
        (byte) 56,
        (byte) 128 /*0x80*/,
        (byte) 94,
        (byte) 126,
        (byte) 239,
        (byte) 187,
        (byte) 118,
        (byte) 57,
        (byte) 205,
        (byte) 138,
        (byte) 238,
        (byte) 137,
        (byte) 86,
        (byte) 78,
        (byte) 147,
        (byte) 177,
        (byte) 237,
        (byte) 175,
        (byte) 57,
        (byte) 34
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 33);
      for (int index = 0; index < 33; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[33];
    byte[] numArray5 = new byte[33]
    {
      (byte) 64 /*0x40*/,
      (byte) 166,
      (byte) 85,
      (byte) 210,
      (byte) 113,
      (byte) 113,
      (byte) 23,
      (byte) 154,
      (byte) 32 /*0x20*/,
      (byte) 245,
      (byte) 65,
      (byte) 237,
      (byte) 244,
      (byte) 143,
      (byte) 141,
      (byte) 56,
      (byte) 67,
      (byte) 229,
      (byte) 168,
      (byte) 25,
      (byte) 116,
      (byte) 119,
      (byte) 216,
      (byte) 143,
      (byte) 201,
      (byte) 10,
      (byte) 76,
      (byte) 45,
      (byte) 149,
      (byte) 76,
      (byte) 205,
      (byte) 160 /*0xA0*/,
      (byte) 185
    };
    byte[] numArray6 = new byte[33]
    {
      (byte) 108,
      (byte) 188,
      (byte) 226,
      (byte) 254,
      (byte) 5,
      (byte) 218,
      (byte) 44,
      (byte) 159,
      (byte) 123,
      (byte) 93,
      (byte) 225,
      (byte) 23,
      (byte) 22,
      (byte) 103,
      (byte) 142,
      (byte) 182,
      (byte) 187,
      (byte) 212,
      (byte) 146,
      (byte) 20,
      (byte) 207,
      (byte) 119,
      (byte) 208 /*0xD0*/,
      (byte) 195,
      (byte) 11,
      (byte) 238,
      (byte) 88,
      (byte) 124,
      (byte) 185,
      (byte) 105,
      (byte) 194,
      (byte) 24,
      (byte) 207
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 33);
    for (int index = 0; index < 33; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[52];
    byte[] response = new byte[52];
    Array.Copy((Array) sc_12860.sspq, 0, (Array) numArray7, 0, 52);
    key.Query(true, 335, numArray7, response);
    Array.Copy((Array) sc_12860.sspr, 0, (Array) numArray7, 0, 52);
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
