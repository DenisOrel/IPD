// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_12771
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_12771
{
  internal static string ssp_appserver_12772()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[62];
      byte[] numArray2 = new byte[55]
      {
        (byte) 80 /*0x50*/,
        (byte) 86,
        (byte) 139,
        (byte) 75,
        (byte) 95,
        (byte) 12,
        (byte) 240 /*0xF0*/,
        (byte) 250,
        (byte) 26,
        (byte) 47,
        (byte) 189,
        (byte) 152,
        (byte) 241,
        (byte) 21,
        (byte) 66,
        (byte) 74,
        (byte) 172,
        (byte) 101,
        (byte) 224 /*0xE0*/,
        (byte) 214,
        (byte) 185,
        (byte) 1,
        (byte) 2,
        (byte) 209,
        (byte) 66,
        (byte) 21,
        (byte) 48 /*0x30*/,
        (byte) 230,
        (byte) 167,
        (byte) 212,
        (byte) 239,
        (byte) 57,
        (byte) 106,
        (byte) 63 /*0x3F*/,
        (byte) 57,
        (byte) 52,
        (byte) 132,
        (byte) 226,
        (byte) 201,
        (byte) 246,
        (byte) 220,
        (byte) 76,
        (byte) 32 /*0x20*/,
        (byte) 124,
        (byte) 19,
        (byte) 115,
        (byte) 33,
        (byte) 20,
        (byte) 43,
        (byte) 214,
        (byte) 51,
        (byte) 85,
        (byte) 212,
        (byte) 25,
        (byte) 2
      };
      byte[] numArray3 = new byte[55];
      numArray3[9] = (byte) 83;
      numArray3[1] = (byte) 9;
      numArray3[2] = (byte) 4;
      numArray3[40] = (byte) 8;
      numArray3[4] = (byte) 88;
      numArray3[12] = (byte) 129;
      numArray3[45] = (byte) 38;
      numArray3[6] = (byte) 40;
      numArray3[37] = (byte) 89;
      numArray3[18] = (byte) 239;
      numArray3[51] = (byte) 28;
      numArray3[19] = (byte) 226;
      numArray3[31 /*0x1F*/] = (byte) 203;
      numArray3[7] = (byte) 35;
      numArray3[3] = (byte) 72;
      numArray3[11] = (byte) 50;
      numArray3[39] = (byte) 240 /*0xF0*/;
      numArray3[34] = (byte) 87;
      numArray3[13] = (byte) 125;
      numArray3[14] = (byte) 77;
      numArray3[20] = (byte) 103;
      numArray3[21] = (byte) 123;
      numArray3[22] = (byte) 165;
      numArray3[23] = (byte) 14;
      numArray3[24] = (byte) 199;
      numArray3[44] = (byte) 1;
      numArray3[0] = (byte) 66;
      numArray3[27] = (byte) 43;
      numArray3[28] = (byte) 70;
      numArray3[29] = (byte) 80 /*0x50*/;
      numArray3[30] = (byte) 191;
      numArray3[33] = (byte) 125;
      numArray3[36] = (byte) 22;
      numArray3[48 /*0x30*/] = (byte) 11;
      numArray3[8] = (byte) 5;
      numArray3[35] = (byte) 248;
      numArray3[15] = (byte) 164;
      numArray3[25] = (byte) 104;
      numArray3[38] = (byte) 84;
      numArray3[41] = (byte) 27;
      numArray3[5] = (byte) 137;
      numArray3[32 /*0x20*/] = (byte) 235;
      numArray3[42] = (byte) 124;
      numArray3[43] = (byte) 204;
      numArray3[16 /*0x10*/] = (byte) 7;
      numArray3[17] = (byte) 178;
      numArray3[46] = (byte) 226;
      numArray3[47] = (byte) 39;
      numArray3[10] = (byte) 206;
      numArray3[49] = (byte) 48 /*0x30*/;
      numArray3[50] = (byte) 121;
      numArray3[52] = (byte) 248;
      numArray3[26] = (byte) 22;
      numArray3[53] = (byte) 217;
      numArray3[54] = (byte) 82;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[7];
      numArray4[5] = (byte) 129;
      numArray4[1] = (byte) 57;
      numArray4[2] = (byte) 84;
      numArray4[3] = (byte) 190;
      numArray4[0] = (byte) 253;
      numArray4[4] = (byte) 79;
      numArray4[6] = (byte) 165;
      byte[] numArray5 = new byte[7]
      {
        (byte) 42,
        (byte) 157,
        (byte) 214,
        (byte) 40,
        (byte) 41,
        (byte) 81,
        (byte) 212
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 7);
      for (int index = 0; index < 7; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[62];
    byte[] numArray7 = new byte[55]
    {
      (byte) 39,
      (byte) 114,
      (byte) 5,
      (byte) 72,
      (byte) 45,
      (byte) 136,
      (byte) 133,
      (byte) 228,
      (byte) 157,
      (byte) 191,
      (byte) 55,
      (byte) 225,
      (byte) 123,
      (byte) 49,
      (byte) 166,
      (byte) 123,
      (byte) 18,
      (byte) 205,
      (byte) 160 /*0xA0*/,
      (byte) 42,
      (byte) 93,
      (byte) 130,
      (byte) 120,
      (byte) 52,
      (byte) 152,
      (byte) 114,
      (byte) 105,
      (byte) 250,
      (byte) 70,
      (byte) 239,
      (byte) 22,
      (byte) 120,
      (byte) 156,
      (byte) 29,
      (byte) 248,
      (byte) 35,
      (byte) 105,
      (byte) 133,
      (byte) 207,
      (byte) 176 /*0xB0*/,
      (byte) 207,
      (byte) 218,
      (byte) 178,
      (byte) 194,
      (byte) 110,
      (byte) 194,
      (byte) 162,
      (byte) 171,
      (byte) 205,
      (byte) 118,
      (byte) 26,
      (byte) 45,
      (byte) 80 /*0x50*/,
      (byte) 241,
      (byte) 112 /*0x70*/
    };
    byte[] numArray8 = new byte[55]
    {
      (byte) 18,
      (byte) 13,
      (byte) 236,
      (byte) 30,
      (byte) 180,
      (byte) 70,
      (byte) 245,
      (byte) 240 /*0xF0*/,
      (byte) 195,
      (byte) 56,
      (byte) 40,
      (byte) 63 /*0x3F*/,
      (byte) 206,
      (byte) 7,
      (byte) 179,
      (byte) 30,
      (byte) 244,
      (byte) 63 /*0x3F*/,
      (byte) 161,
      (byte) 106,
      (byte) 29,
      (byte) 203,
      (byte) 151,
      (byte) 141,
      (byte) 253,
      (byte) 160 /*0xA0*/,
      (byte) 83,
      (byte) 164,
      (byte) 227,
      (byte) 138,
      (byte) 178,
      (byte) 12,
      (byte) 57,
      (byte) 196,
      (byte) 149,
      (byte) 21,
      (byte) 73,
      (byte) 4,
      (byte) 97,
      (byte) 183,
      (byte) 81,
      (byte) 91,
      (byte) 44,
      (byte) 115,
      (byte) 7,
      (byte) 188,
      (byte) 207,
      (byte) 12,
      (byte) 109,
      (byte) 198,
      (byte) 39,
      (byte) 132,
      (byte) 117,
      (byte) 13,
      (byte) 224 /*0xE0*/
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[7];
    numArray9[1] = (byte) 176 /*0xB0*/;
    numArray9[3] = (byte) 31 /*0x1F*/;
    numArray9[2] = (byte) 186;
    numArray9[6] = (byte) 173;
    numArray9[0] = (byte) 209;
    numArray9[5] = (byte) 157;
    numArray9[4] = (byte) 138;
    byte[] numArray10 = new byte[7]
    {
      (byte) 92,
      (byte) 186,
      (byte) 153,
      (byte) 215,
      (byte) 150,
      (byte) 1,
      (byte) 38
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 7);
    for (int index = 0; index < 7; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_12773()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[42];
      byte[] numArray2 = new byte[42]
      {
        (byte) 130,
        (byte) 207,
        (byte) 182,
        (byte) 197,
        (byte) 213,
        (byte) 207,
        (byte) 190,
        (byte) 44,
        (byte) 208 /*0xD0*/,
        (byte) 213,
        (byte) 232,
        (byte) 40,
        (byte) 170,
        (byte) 172,
        (byte) 83,
        (byte) 42,
        (byte) 151,
        (byte) 212,
        (byte) 21,
        (byte) 145,
        (byte) 68,
        (byte) 60,
        (byte) 5,
        (byte) 25,
        (byte) 175,
        (byte) 125,
        (byte) 68,
        (byte) 114,
        (byte) 74,
        (byte) 100,
        (byte) 140,
        (byte) 48 /*0x30*/,
        (byte) 96 /*0x60*/,
        (byte) 150,
        (byte) 237,
        (byte) 213,
        (byte) 67,
        (byte) 11,
        (byte) 218,
        (byte) 235,
        (byte) 97,
        (byte) 94
      };
      byte[] numArray3 = new byte[42];
      numArray3[31 /*0x1F*/] = (byte) 134;
      numArray3[16 /*0x10*/] = (byte) 185;
      numArray3[11] = (byte) 40;
      numArray3[20] = (byte) 40;
      numArray3[15] = (byte) 227;
      numArray3[5] = (byte) 62;
      numArray3[6] = (byte) 13;
      numArray3[8] = (byte) 57;
      numArray3[34] = (byte) 162;
      numArray3[9] = (byte) 163;
      numArray3[10] = (byte) 25;
      numArray3[3] = (byte) 45;
      numArray3[13] = (byte) 9;
      numArray3[30] = (byte) 46;
      numArray3[14] = (byte) 102;
      numArray3[19] = (byte) 84;
      numArray3[21] = (byte) 169;
      numArray3[17] = (byte) 189;
      numArray3[37] = (byte) 166;
      numArray3[40] = (byte) 229;
      numArray3[12] = (byte) 167;
      numArray3[7] = (byte) 1;
      numArray3[22] = (byte) 33;
      numArray3[23] = (byte) 37;
      numArray3[24] = (byte) 85;
      numArray3[25] = (byte) 165;
      numArray3[26] = (byte) 57;
      numArray3[27] = (byte) 64 /*0x40*/;
      numArray3[1] = (byte) 16 /*0x10*/;
      numArray3[2] = (byte) 75;
      numArray3[4] = (byte) 166;
      numArray3[29] = (byte) 214;
      numArray3[32 /*0x20*/] = (byte) 198;
      numArray3[33] = (byte) 144 /*0x90*/;
      numArray3[39] = (byte) 143;
      numArray3[0] = (byte) 132;
      numArray3[18] = (byte) 72;
      numArray3[28] = byte.MaxValue;
      numArray3[35] = (byte) 237;
      numArray3[36] = (byte) 222;
      numArray3[38] = (byte) 10;
      numArray3[41] = (byte) 173;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 42);
      for (int index = 0; index < 42; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[42];
    byte[] numArray5 = new byte[42]
    {
      (byte) 2,
      (byte) 127 /*0x7F*/,
      (byte) 167,
      (byte) 10,
      (byte) 166,
      (byte) 245,
      (byte) 131,
      (byte) 96 /*0x60*/,
      (byte) 22,
      (byte) 124,
      (byte) 97,
      (byte) 143,
      (byte) 8,
      (byte) 205,
      (byte) 133,
      (byte) 242,
      (byte) 102,
      (byte) 113,
      (byte) 237,
      (byte) 190,
      (byte) 190,
      (byte) 45,
      (byte) 48 /*0x30*/,
      (byte) 214,
      (byte) 157,
      (byte) 66,
      (byte) 252,
      (byte) 116,
      (byte) 247,
      (byte) 189,
      (byte) 240 /*0xF0*/,
      (byte) 128 /*0x80*/,
      (byte) 117,
      (byte) 111,
      (byte) 198,
      (byte) 148,
      (byte) 23,
      (byte) 115,
      (byte) 64 /*0x40*/,
      (byte) 150,
      (byte) 118,
      (byte) 201
    };
    byte[] numArray6 = new byte[42]
    {
      (byte) 169,
      (byte) 153,
      (byte) 71,
      (byte) 143,
      (byte) 145,
      (byte) 201,
      (byte) 144 /*0x90*/,
      (byte) 206,
      (byte) 182,
      (byte) 146,
      (byte) 225,
      (byte) 57,
      (byte) 43,
      (byte) 78,
      (byte) 173,
      (byte) 223,
      (byte) 180,
      (byte) 41,
      (byte) 198,
      (byte) 167,
      (byte) 39,
      (byte) 33,
      (byte) 149,
      (byte) 62,
      (byte) 57,
      (byte) 15,
      (byte) 160 /*0xA0*/,
      (byte) 36,
      (byte) 52,
      (byte) 194,
      (byte) 25,
      (byte) 87,
      (byte) 79,
      (byte) 127 /*0x7F*/,
      (byte) 205,
      (byte) 95,
      (byte) 209,
      (byte) 253,
      (byte) 232,
      (byte) 46,
      (byte) 82,
      (byte) 46
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 42);
    for (int index = 0; index < 42; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static int ssp_appserver_12774(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 76,
      (byte) 93,
      (byte) 161,
      (byte) 39,
      (byte) 127 /*0x7F*/,
      (byte) 252,
      (byte) 21,
      (byte) 138,
      (byte) 212,
      (byte) 78,
      (byte) 208 /*0xD0*/,
      (byte) 108,
      (byte) 99,
      (byte) 249,
      (byte) 14,
      (byte) 104,
      (byte) 89,
      (byte) 139,
      (byte) 225,
      (byte) 26,
      (byte) 152,
      (byte) 7,
      (byte) 241,
      (byte) 218,
      (byte) 242,
      (byte) 49,
      (byte) 87,
      (byte) 71,
      (byte) 245,
      (byte) 19,
      (byte) 198,
      (byte) 200,
      byte.MaxValue,
      (byte) 241,
      (byte) 175,
      (byte) 22,
      (byte) 86,
      (byte) 149,
      (byte) 112 /*0x70*/,
      (byte) 131,
      (byte) 95,
      (byte) 229,
      (byte) 241,
      (byte) 70,
      (byte) 70,
      (byte) 6,
      (byte) 221,
      (byte) 156
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 98,
      (byte) 197,
      (byte) 138,
      (byte) 202,
      (byte) 225,
      (byte) 148,
      (byte) 135,
      (byte) 2,
      (byte) 19,
      (byte) 123,
      (byte) 8,
      (byte) 236,
      (byte) 110,
      (byte) 95,
      (byte) 210,
      (byte) 170,
      (byte) 54,
      (byte) 137,
      (byte) 11,
      (byte) 28,
      (byte) 3,
      (byte) 52,
      (byte) 235,
      (byte) 61,
      (byte) 72,
      (byte) 238,
      (byte) 119,
      (byte) 146,
      (byte) 222,
      (byte) 70,
      (byte) 111,
      (byte) 65,
      (byte) 101,
      (byte) 138,
      (byte) 207,
      (byte) 153,
      (byte) 41,
      (byte) 4,
      (byte) 127 /*0x7F*/,
      (byte) 149,
      (byte) 6,
      (byte) 31 /*0x1F*/,
      (byte) 212,
      (byte) 196,
      (byte) 26,
      (byte) 118,
      (byte) 219,
      (byte) 84
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12775(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 131,
      (byte) 184,
      (byte) 69,
      (byte) 108,
      (byte) 198,
      (byte) 52,
      (byte) 78,
      (byte) 165,
      (byte) 205,
      (byte) 148,
      (byte) 192 /*0xC0*/,
      (byte) 136,
      (byte) 7,
      (byte) 109,
      (byte) 114,
      (byte) 74,
      (byte) 248,
      (byte) 18,
      (byte) 195,
      (byte) 27,
      (byte) 93,
      (byte) 186,
      (byte) 179,
      (byte) 21,
      (byte) 171,
      (byte) 147,
      (byte) 236,
      (byte) 84,
      (byte) 186,
      (byte) 223,
      (byte) 148,
      (byte) 82,
      (byte) 231,
      (byte) 156,
      (byte) 123,
      (byte) 56,
      (byte) 105,
      (byte) 170,
      (byte) 143,
      (byte) 97,
      (byte) 33,
      (byte) 222,
      (byte) 146,
      (byte) 80 /*0x50*/,
      (byte) 49,
      (byte) 189,
      (byte) 62,
      (byte) 153
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[4] = (byte) 187;
    sourceArray2[31 /*0x1F*/] = (byte) 212;
    sourceArray2[2] = (byte) 47;
    sourceArray2[3] = (byte) 252;
    sourceArray2[29] = (byte) 111;
    sourceArray2[5] = (byte) 122;
    sourceArray2[32 /*0x20*/] = (byte) 171;
    sourceArray2[37] = (byte) 46;
    sourceArray2[24] = (byte) 92;
    sourceArray2[41] = (byte) 16 /*0x10*/;
    sourceArray2[10] = (byte) 230;
    sourceArray2[39] = (byte) 21;
    sourceArray2[44] = (byte) 170;
    sourceArray2[6] = (byte) 57;
    sourceArray2[9] = (byte) 17;
    sourceArray2[15] = (byte) 211;
    sourceArray2[16 /*0x10*/] = (byte) 57;
    sourceArray2[35] = (byte) 242;
    sourceArray2[38] = (byte) 165;
    sourceArray2[19] = (byte) 157;
    sourceArray2[11] = (byte) 87;
    sourceArray2[23] = (byte) 87;
    sourceArray2[21] = (byte) 215;
    sourceArray2[12] = (byte) 54;
    sourceArray2[47] = (byte) 209;
    sourceArray2[13] = (byte) 245;
    sourceArray2[14] = (byte) 158;
    sourceArray2[27] = (byte) 74;
    sourceArray2[28] = (byte) 8;
    sourceArray2[33] = (byte) 196;
    sourceArray2[8] = (byte) 227;
    sourceArray2[22] = (byte) 39;
    sourceArray2[30] = (byte) 129;
    sourceArray2[18] = (byte) 52;
    sourceArray2[7] = (byte) 203;
    sourceArray2[34] = (byte) 214;
    sourceArray2[36] = (byte) 211;
    sourceArray2[42] = (byte) 193;
    sourceArray2[0] = (byte) 20;
    sourceArray2[26] = (byte) 160 /*0xA0*/;
    sourceArray2[40] = (byte) 67;
    sourceArray2[25] = (byte) 17;
    sourceArray2[20] = (byte) 82;
    sourceArray2[43] = (byte) 109;
    sourceArray2[1] = (byte) 252;
    sourceArray2[45] = (byte) 170;
    sourceArray2[46] = (byte) 164;
    sourceArray2[17] = (byte) 62;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
