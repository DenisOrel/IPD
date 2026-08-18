// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_8027
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_8027
{
  private static byte[] sspq = new byte[46]
  {
    (byte) 44,
    (byte) 170,
    (byte) 216,
    (byte) 222,
    (byte) 182,
    (byte) 39,
    (byte) 16 /*0x10*/,
    (byte) 248,
    (byte) 127 /*0x7F*/,
    (byte) 137,
    (byte) 121,
    (byte) 85,
    (byte) 46,
    (byte) 97,
    (byte) 179,
    (byte) 27,
    (byte) 136,
    (byte) 175,
    (byte) 206,
    (byte) 57,
    (byte) 18,
    (byte) 147,
    (byte) 153,
    (byte) 106,
    (byte) 116,
    (byte) 185,
    (byte) 150,
    (byte) 249,
    (byte) 44,
    (byte) 67,
    (byte) 189,
    (byte) 247,
    (byte) 228,
    (byte) 47,
    (byte) 124,
    (byte) 135,
    (byte) 62,
    (byte) 115,
    (byte) 192 /*0xC0*/,
    (byte) 251,
    (byte) 118,
    (byte) 0,
    (byte) 150,
    (byte) 180,
    byte.MaxValue,
    (byte) 170
  };
  private static byte[] sspr = new byte[46]
  {
    (byte) 196,
    (byte) 117,
    (byte) 178,
    (byte) 28,
    (byte) 73,
    (byte) 36,
    (byte) 28,
    (byte) 194,
    (byte) 34,
    (byte) 40,
    (byte) 98,
    (byte) 22,
    (byte) 59,
    (byte) 100,
    (byte) 170,
    (byte) 134,
    (byte) 91,
    (byte) 79,
    (byte) 228,
    (byte) 79,
    (byte) 15,
    (byte) 148,
    (byte) 167,
    (byte) 41,
    (byte) 209,
    (byte) 118,
    (byte) 90,
    (byte) 14,
    (byte) 200,
    (byte) 84,
    (byte) 44,
    (byte) 41,
    (byte) 120,
    (byte) 189,
    (byte) 107,
    (byte) 79,
    (byte) 166,
    (byte) 11,
    (byte) 242,
    (byte) 189,
    (byte) 145,
    (byte) 134,
    (byte) 57,
    (byte) 212,
    (byte) 197,
    (byte) 179
  };

  internal static string ssp_appserver_8028()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[11];
      byte[] numArray2 = new byte[11]
      {
        (byte) 106,
        (byte) 223,
        (byte) 204,
        (byte) 228,
        (byte) 118,
        (byte) 25,
        (byte) 40,
        (byte) 54,
        (byte) 118,
        (byte) 216,
        (byte) 252
      };
      byte[] numArray3 = new byte[11]
      {
        (byte) 74,
        (byte) 178,
        (byte) 222,
        (byte) 105,
        (byte) 222,
        (byte) 125,
        (byte) 217,
        (byte) 243,
        (byte) 175,
        (byte) 244,
        (byte) 226
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 11);
      for (int index = 0; index < 11; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[11];
    byte[] numArray5 = new byte[11]
    {
      (byte) 95,
      (byte) 200,
      (byte) 187,
      (byte) 202,
      (byte) 223,
      (byte) 135,
      (byte) 54,
      (byte) 115,
      (byte) 186,
      (byte) 202,
      (byte) 161
    };
    byte[] numArray6 = new byte[11];
    numArray6[10] = (byte) 5;
    numArray6[9] = (byte) 12;
    numArray6[3] = (byte) 34;
    numArray6[0] = (byte) 212;
    numArray6[2] = (byte) 108;
    numArray6[5] = (byte) 113;
    numArray6[4] = (byte) 81;
    numArray6[7] = (byte) 253;
    numArray6[8] = (byte) 185;
    numArray6[6] = (byte) 126;
    numArray6[1] = (byte) 253;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 11);
    for (int index = 0; index < 11; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_8029()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[27];
      byte[] numArray2 = new byte[27]
      {
        (byte) 54,
        (byte) 250,
        (byte) 180,
        (byte) 62,
        (byte) 6,
        (byte) 91,
        (byte) 112 /*0x70*/,
        (byte) 50,
        (byte) 168,
        (byte) 226,
        (byte) 8,
        (byte) 164,
        (byte) 209,
        (byte) 148,
        (byte) 26,
        (byte) 247,
        (byte) 37,
        (byte) 1,
        (byte) 66,
        (byte) 30,
        (byte) 203,
        (byte) 193,
        (byte) 127 /*0x7F*/,
        (byte) 229,
        (byte) 202,
        (byte) 7,
        (byte) 118
      };
      byte[] numArray3 = new byte[27]
      {
        (byte) 82,
        (byte) 201,
        (byte) 249,
        (byte) 145,
        (byte) 156,
        (byte) 178,
        (byte) 85,
        (byte) 58,
        (byte) 172,
        (byte) 229,
        (byte) 56,
        (byte) 30,
        (byte) 156,
        (byte) 196,
        (byte) 121,
        (byte) 215,
        (byte) 42,
        (byte) 57,
        (byte) 4,
        (byte) 46,
        (byte) 203,
        (byte) 14,
        (byte) 87,
        (byte) 63 /*0x3F*/,
        (byte) 186,
        (byte) 13,
        (byte) 214
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 27);
      for (int index = 0; index < 27; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[27];
    byte[] numArray5 = new byte[27];
    numArray5[17] = (byte) 227;
    numArray5[1] = (byte) 103;
    numArray5[16 /*0x10*/] = (byte) 36;
    numArray5[12] = (byte) 191;
    numArray5[4] = (byte) 80 /*0x50*/;
    numArray5[11] = (byte) 27;
    numArray5[22] = (byte) 17;
    numArray5[7] = (byte) 123;
    numArray5[2] = (byte) 191;
    numArray5[15] = (byte) 104;
    numArray5[10] = (byte) 231;
    numArray5[25] = (byte) 117;
    numArray5[8] = (byte) 195;
    numArray5[13] = (byte) 212;
    numArray5[9] = (byte) 70;
    numArray5[5] = (byte) 60;
    numArray5[3] = (byte) 72;
    numArray5[26] = (byte) 123;
    numArray5[18] = (byte) 86;
    numArray5[19] = (byte) 245;
    numArray5[20] = (byte) 224 /*0xE0*/;
    numArray5[21] = (byte) 147;
    numArray5[6] = (byte) 191;
    numArray5[23] = (byte) 237;
    numArray5[14] = (byte) 136;
    numArray5[0] = (byte) 51;
    numArray5[24] = (byte) 202;
    byte[] numArray6 = new byte[27];
    numArray6[1] = (byte) 63 /*0x3F*/;
    numArray6[22] = (byte) 32 /*0x20*/;
    numArray6[2] = (byte) 208 /*0xD0*/;
    numArray6[3] = (byte) 119;
    numArray6[19] = (byte) 235;
    numArray6[23] = (byte) 101;
    numArray6[17] = (byte) 190;
    numArray6[12] = (byte) 3;
    numArray6[8] = (byte) 79;
    numArray6[4] = (byte) 144 /*0x90*/;
    numArray6[9] = (byte) 0;
    numArray6[11] = (byte) 62;
    numArray6[0] = (byte) 117;
    numArray6[13] = (byte) 3;
    numArray6[5] = (byte) 206;
    numArray6[10] = (byte) 195;
    numArray6[16 /*0x10*/] = (byte) 99;
    numArray6[15] = (byte) 177;
    numArray6[14] = (byte) 71;
    numArray6[18] = (byte) 195;
    numArray6[7] = (byte) 221;
    numArray6[21] = byte.MaxValue;
    numArray6[6] = (byte) 33;
    numArray6[20] = (byte) 8;
    numArray6[24] = (byte) 116;
    numArray6[25] = (byte) 117;
    numArray6[26] = (byte) 89;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 27);
    for (int index = 0; index < 27; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_8030()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[87];
      byte[] numArray2 = new byte[55]
      {
        (byte) 82,
        (byte) 180,
        (byte) 77,
        (byte) 214,
        (byte) 14,
        (byte) 74,
        (byte) 19,
        (byte) 10,
        (byte) 105,
        (byte) 29,
        (byte) 70,
        (byte) 163,
        (byte) 121,
        (byte) 154,
        (byte) 182,
        (byte) 90,
        (byte) 145,
        (byte) 132,
        (byte) 118,
        (byte) 222,
        (byte) 70,
        (byte) 243,
        (byte) 58,
        (byte) 186,
        (byte) 65,
        (byte) 74,
        (byte) 65,
        (byte) 28,
        (byte) 99,
        (byte) 102,
        (byte) 239,
        (byte) 127 /*0x7F*/,
        (byte) 244,
        (byte) 167,
        (byte) 14,
        (byte) 204,
        (byte) 8,
        (byte) 154,
        (byte) 23,
        (byte) 156,
        (byte) 111,
        (byte) 146,
        (byte) 53,
        (byte) 90,
        (byte) 118,
        (byte) 147,
        (byte) 220,
        (byte) 122,
        byte.MaxValue,
        (byte) 191,
        (byte) 226,
        (byte) 77,
        (byte) 251,
        (byte) 51,
        (byte) 214
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 123,
        (byte) 3,
        (byte) 230,
        (byte) 208 /*0xD0*/,
        (byte) 203,
        (byte) 135,
        (byte) 74,
        (byte) 53,
        (byte) 44,
        (byte) 52,
        (byte) 184,
        (byte) 7,
        (byte) 21,
        (byte) 129,
        (byte) 188,
        (byte) 111,
        (byte) 174,
        (byte) 200,
        (byte) 194,
        (byte) 53,
        (byte) 160 /*0xA0*/,
        (byte) 106,
        (byte) 155,
        (byte) 143,
        (byte) 224 /*0xE0*/,
        (byte) 143,
        (byte) 23,
        (byte) 166,
        (byte) 3,
        (byte) 150,
        (byte) 33,
        (byte) 35,
        (byte) 196,
        (byte) 136,
        (byte) 191,
        (byte) 85,
        (byte) 150,
        (byte) 246,
        (byte) 115,
        (byte) 136,
        (byte) 175,
        (byte) 68,
        (byte) 124,
        (byte) 249,
        (byte) 8,
        (byte) 246,
        (byte) 134,
        (byte) 141,
        (byte) 138,
        (byte) 154,
        (byte) 143,
        (byte) 243,
        (byte) 102,
        (byte) 45,
        (byte) 239
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[32 /*0x20*/]
      {
        (byte) 146,
        (byte) 148,
        (byte) 196,
        (byte) 8,
        (byte) 247,
        (byte) 166,
        (byte) 3,
        (byte) 45,
        (byte) 199,
        (byte) 174,
        (byte) 209,
        (byte) 139,
        (byte) 233,
        (byte) 81,
        (byte) 247,
        (byte) 108,
        (byte) 217,
        (byte) 110,
        (byte) 180,
        (byte) 189,
        (byte) 224 /*0xE0*/,
        (byte) 67,
        (byte) 174,
        (byte) 109,
        (byte) 199,
        (byte) 28,
        (byte) 163,
        (byte) 132,
        (byte) 162,
        (byte) 132,
        (byte) 192 /*0xC0*/,
        (byte) 209
      };
      byte[] numArray5 = new byte[32 /*0x20*/];
      numArray5[30] = (byte) 67;
      numArray5[1] = (byte) 220;
      numArray5[14] = (byte) 249;
      numArray5[3] = (byte) 228;
      numArray5[5] = (byte) 55;
      numArray5[11] = (byte) 143;
      numArray5[16 /*0x10*/] = (byte) 215;
      numArray5[7] = (byte) 227;
      numArray5[13] = (byte) 143;
      numArray5[2] = (byte) 29;
      numArray5[26] = (byte) 35;
      numArray5[28] = (byte) 180;
      numArray5[21] = (byte) 86;
      numArray5[25] = (byte) 248;
      numArray5[4] = (byte) 38;
      numArray5[15] = (byte) 4;
      numArray5[23] = (byte) 194;
      numArray5[6] = (byte) 98;
      numArray5[12] = (byte) 208 /*0xD0*/;
      numArray5[19] = (byte) 136;
      numArray5[20] = (byte) 240 /*0xF0*/;
      numArray5[10] = (byte) 26;
      numArray5[22] = (byte) 87;
      numArray5[17] = (byte) 242;
      numArray5[24] = (byte) 42;
      numArray5[0] = (byte) 243;
      numArray5[8] = (byte) 122;
      numArray5[27] = (byte) 40;
      numArray5[18] = (byte) 76;
      numArray5[29] = (byte) 109;
      numArray5[9] = (byte) 2;
      numArray5[31 /*0x1F*/] = (byte) 233;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 32 /*0x20*/);
      for (int index = 0; index < 32 /*0x20*/; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[87];
    byte[] numArray7 = new byte[55]
    {
      (byte) 150,
      (byte) 219,
      (byte) 240 /*0xF0*/,
      (byte) 25,
      (byte) 69,
      (byte) 246,
      (byte) 199,
      (byte) 139,
      (byte) 179,
      (byte) 148,
      (byte) 201,
      (byte) 99,
      (byte) 217,
      (byte) 6,
      (byte) 53,
      (byte) 153,
      (byte) 194,
      (byte) 99,
      (byte) 251,
      (byte) 174,
      (byte) 13,
      (byte) 185,
      (byte) 38,
      (byte) 32 /*0x20*/,
      (byte) 252,
      (byte) 83,
      (byte) 157,
      (byte) 233,
      (byte) 178,
      (byte) 161,
      (byte) 108,
      (byte) 246,
      (byte) 9,
      (byte) 18,
      (byte) 126,
      (byte) 146,
      (byte) 250,
      (byte) 198,
      (byte) 219,
      (byte) 206,
      (byte) 90,
      (byte) 21,
      (byte) 230,
      (byte) 186,
      (byte) 27,
      (byte) 243,
      (byte) 103,
      (byte) 212,
      (byte) 204,
      (byte) 230,
      (byte) 201,
      (byte) 223,
      (byte) 242,
      (byte) 82,
      (byte) 206
    };
    byte[] numArray8 = new byte[55]
    {
      (byte) 57,
      (byte) 99,
      (byte) 216,
      (byte) 157,
      (byte) 72,
      (byte) 80 /*0x50*/,
      byte.MaxValue,
      (byte) 41,
      (byte) 23,
      (byte) 116,
      (byte) 149,
      (byte) 46,
      (byte) 43,
      (byte) 111,
      (byte) 225,
      (byte) 181,
      (byte) 107,
      (byte) 241,
      (byte) 89,
      (byte) 79,
      (byte) 189,
      (byte) 148,
      (byte) 39,
      (byte) 121,
      (byte) 176 /*0xB0*/,
      (byte) 74,
      (byte) 135,
      (byte) 171,
      (byte) 146,
      (byte) 100,
      (byte) 86,
      (byte) 110,
      (byte) 166,
      (byte) 141,
      (byte) 133,
      (byte) 96 /*0x60*/,
      (byte) 97,
      (byte) 108,
      (byte) 58,
      (byte) 83,
      (byte) 59,
      (byte) 32 /*0x20*/,
      (byte) 198,
      (byte) 132,
      (byte) 153,
      (byte) 206,
      (byte) 240 /*0xF0*/,
      (byte) 105,
      (byte) 217,
      (byte) 221,
      (byte) 230,
      (byte) 18,
      (byte) 47,
      (byte) 82,
      (byte) 138
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[32 /*0x20*/]
    {
      (byte) 207,
      (byte) 29,
      (byte) 242,
      (byte) 89,
      (byte) 222,
      (byte) 217,
      (byte) 17,
      (byte) 40,
      (byte) 211,
      (byte) 89,
      (byte) 199,
      (byte) 135,
      (byte) 163,
      (byte) 158,
      (byte) 251,
      (byte) 135,
      (byte) 45,
      (byte) 83,
      (byte) 247,
      (byte) 211,
      (byte) 107,
      (byte) 5,
      (byte) 9,
      (byte) 235,
      (byte) 200,
      (byte) 29,
      (byte) 204,
      (byte) 216,
      (byte) 140,
      (byte) 3,
      (byte) 183,
      (byte) 67
    };
    byte[] numArray10 = new byte[32 /*0x20*/];
    numArray10[26] = (byte) 164;
    numArray10[19] = (byte) 163;
    numArray10[2] = (byte) 153;
    numArray10[3] = (byte) 2;
    numArray10[4] = (byte) 229;
    numArray10[5] = (byte) 211;
    numArray10[6] = (byte) 31 /*0x1F*/;
    numArray10[1] = (byte) 146;
    numArray10[24] = (byte) 166;
    numArray10[9] = (byte) 108;
    numArray10[23] = (byte) 120;
    numArray10[31 /*0x1F*/] = (byte) 106;
    numArray10[12] = (byte) 96 /*0x60*/;
    numArray10[13] = (byte) 232;
    numArray10[10] = (byte) 226;
    numArray10[8] = (byte) 159;
    numArray10[15] = (byte) 223;
    numArray10[14] = (byte) 128 /*0x80*/;
    numArray10[18] = (byte) 2;
    numArray10[0] = (byte) 55;
    numArray10[17] = (byte) 143;
    numArray10[21] = (byte) 68;
    numArray10[22] = (byte) 118;
    numArray10[7] = (byte) 66;
    numArray10[16 /*0x10*/] = (byte) 24;
    numArray10[25] = (byte) 65;
    numArray10[20] = (byte) 223;
    numArray10[27] = (byte) 174;
    numArray10[11] = (byte) 127 /*0x7F*/;
    numArray10[29] = (byte) 228;
    numArray10[30] = (byte) 214;
    numArray10[28] = (byte) 46;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 32 /*0x20*/);
    for (int index = 0; index < 32 /*0x20*/; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_8031()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[79];
      byte[] numArray2 = new byte[55];
      numArray2[36] = (byte) 186;
      numArray2[31 /*0x1F*/] = (byte) 28;
      numArray2[2] = (byte) 158;
      numArray2[32 /*0x20*/] = (byte) 103;
      numArray2[1] = (byte) 81;
      numArray2[5] = (byte) 187;
      numArray2[21] = (byte) 96 /*0x60*/;
      numArray2[17] = (byte) 184;
      numArray2[8] = (byte) 188;
      numArray2[15] = (byte) 51;
      numArray2[10] = (byte) 75;
      numArray2[11] = (byte) 123;
      numArray2[3] = (byte) 184;
      numArray2[45] = (byte) 21;
      numArray2[14] = (byte) 10;
      numArray2[46] = (byte) 204;
      numArray2[47] = (byte) 135;
      numArray2[23] = (byte) 225;
      numArray2[12] = (byte) 244;
      numArray2[43] = (byte) 164;
      numArray2[20] = (byte) 147;
      numArray2[16 /*0x10*/] = (byte) 62;
      numArray2[22] = (byte) 149;
      numArray2[7] = (byte) 217;
      numArray2[24] = (byte) 228;
      numArray2[26] = (byte) 47;
      numArray2[13] = (byte) 133;
      numArray2[27] = (byte) 155;
      numArray2[28] = (byte) 247;
      numArray2[0] = (byte) 116;
      numArray2[25] = (byte) 187;
      numArray2[41] = (byte) 112 /*0x70*/;
      numArray2[34] = (byte) 211;
      numArray2[33] = (byte) 98;
      numArray2[48 /*0x30*/] = (byte) 46;
      numArray2[44] = (byte) 187;
      numArray2[18] = (byte) 109;
      numArray2[37] = (byte) 12;
      numArray2[38] = (byte) 38;
      numArray2[6] = (byte) 254;
      numArray2[40] = (byte) 11;
      numArray2[4] = (byte) 215;
      numArray2[42] = (byte) 13;
      numArray2[39] = (byte) 27;
      numArray2[52] = (byte) 22;
      numArray2[35] = (byte) 123;
      numArray2[49] = (byte) 103;
      numArray2[30] = (byte) 246;
      numArray2[29] = (byte) 120;
      numArray2[9] = (byte) 111;
      numArray2[50] = (byte) 145;
      numArray2[51] = (byte) 233;
      numArray2[19] = (byte) 150;
      numArray2[53] = (byte) 79;
      numArray2[54] = (byte) 51;
      byte[] numArray3 = new byte[55]
      {
        (byte) 29,
        (byte) 80 /*0x50*/,
        (byte) 124,
        (byte) 25,
        (byte) 59,
        (byte) 4,
        (byte) 147,
        (byte) 200,
        (byte) 25,
        (byte) 42,
        (byte) 83,
        (byte) 214,
        (byte) 161,
        (byte) 192 /*0xC0*/,
        (byte) 201,
        (byte) 88,
        (byte) 229,
        (byte) 121,
        (byte) 142,
        (byte) 173,
        (byte) 38,
        (byte) 64 /*0x40*/,
        (byte) 29,
        (byte) 19,
        (byte) 184,
        (byte) 111,
        (byte) 6,
        (byte) 130,
        (byte) 132,
        (byte) 195,
        (byte) 29,
        (byte) 100,
        (byte) 174,
        (byte) 83,
        (byte) 129,
        (byte) 251,
        (byte) 206,
        (byte) 74,
        (byte) 158,
        (byte) 167,
        (byte) 93,
        (byte) 146,
        (byte) 96 /*0x60*/,
        (byte) 142,
        (byte) 242,
        (byte) 75,
        (byte) 174,
        (byte) 91,
        (byte) 104,
        (byte) 187,
        (byte) 20,
        (byte) 104,
        (byte) 240 /*0xF0*/,
        (byte) 242,
        (byte) 74
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[24]
      {
        (byte) 151,
        (byte) 5,
        (byte) 45,
        (byte) 227,
        (byte) 246,
        (byte) 82,
        (byte) 92,
        (byte) 108,
        (byte) 39,
        (byte) 252,
        (byte) 99,
        (byte) 242,
        (byte) 207,
        (byte) 124,
        (byte) 248,
        (byte) 135,
        (byte) 194,
        (byte) 226,
        (byte) 148,
        (byte) 136,
        (byte) 198,
        (byte) 152,
        (byte) 205,
        (byte) 180
      };
      byte[] numArray5 = new byte[24]
      {
        (byte) 5,
        (byte) 86,
        (byte) 13,
        (byte) 196,
        byte.MaxValue,
        (byte) 29,
        (byte) 152,
        (byte) 165,
        (byte) 185,
        (byte) 91,
        (byte) 195,
        (byte) 189,
        (byte) 194,
        (byte) 225,
        (byte) 228,
        (byte) 37,
        (byte) 140,
        (byte) 80 /*0x50*/,
        (byte) 22,
        (byte) 251,
        (byte) 147,
        (byte) 168,
        (byte) 36,
        (byte) 220
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 24);
      for (int index = 0; index < 24; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[79];
    byte[] numArray7 = new byte[55];
    numArray7[26] = (byte) 56;
    numArray7[10] = (byte) 172;
    numArray7[42] = (byte) 197;
    numArray7[3] = (byte) 83;
    numArray7[30] = (byte) 120;
    numArray7[11] = (byte) 178;
    numArray7[6] = (byte) 244;
    numArray7[37] = (byte) 168;
    numArray7[40] = (byte) 219;
    numArray7[50] = (byte) 200;
    numArray7[33] = (byte) 176 /*0xB0*/;
    numArray7[23] = (byte) 2;
    numArray7[12] = (byte) 60;
    numArray7[13] = (byte) 108;
    numArray7[43] = (byte) 169;
    numArray7[15] = (byte) 101;
    numArray7[16 /*0x10*/] = (byte) 5;
    numArray7[17] = (byte) 215;
    numArray7[45] = (byte) 48 /*0x30*/;
    numArray7[19] = (byte) 209;
    numArray7[9] = (byte) 221;
    numArray7[41] = (byte) 22;
    numArray7[22] = (byte) 208 /*0xD0*/;
    numArray7[2] = (byte) 66;
    numArray7[54] = (byte) 48 /*0x30*/;
    numArray7[52] = (byte) 238;
    numArray7[36] = (byte) 229;
    numArray7[27] = (byte) 4;
    numArray7[31 /*0x1F*/] = (byte) 16 /*0x10*/;
    numArray7[20] = (byte) 113;
    numArray7[14] = (byte) 190;
    numArray7[38] = (byte) 68;
    numArray7[25] = (byte) 124;
    numArray7[39] = (byte) 244;
    numArray7[34] = (byte) 212;
    numArray7[35] = (byte) 183;
    numArray7[53] = (byte) 251;
    numArray7[8] = (byte) 51;
    numArray7[18] = (byte) 227;
    numArray7[28] = byte.MaxValue;
    numArray7[4] = (byte) 30;
    numArray7[29] = (byte) 55;
    numArray7[5] = (byte) 253;
    numArray7[21] = (byte) 102;
    numArray7[44] = (byte) 21;
    numArray7[1] = (byte) 180;
    numArray7[46] = (byte) 88;
    numArray7[47] = (byte) 28;
    numArray7[32 /*0x20*/] = (byte) 42;
    numArray7[49] = (byte) 50;
    numArray7[48 /*0x30*/] = (byte) 8;
    numArray7[51] = (byte) 221;
    numArray7[0] = (byte) 136;
    numArray7[24] = (byte) 226;
    numArray7[7] = (byte) 101;
    byte[] numArray8 = new byte[55]
    {
      (byte) 82,
      (byte) 139,
      (byte) 224 /*0xE0*/,
      (byte) 254,
      (byte) 5,
      (byte) 238,
      (byte) 26,
      (byte) 208 /*0xD0*/,
      (byte) 208 /*0xD0*/,
      (byte) 190,
      (byte) 7,
      (byte) 217,
      (byte) 239,
      byte.MaxValue,
      (byte) 80 /*0x50*/,
      (byte) 139,
      (byte) 83,
      (byte) 85,
      (byte) 80 /*0x50*/,
      (byte) 66,
      (byte) 93,
      (byte) 175,
      (byte) 181,
      (byte) 200,
      (byte) 196,
      (byte) 36,
      (byte) 175,
      (byte) 145,
      (byte) 141,
      (byte) 132,
      (byte) 226,
      (byte) 183,
      (byte) 38,
      (byte) 81,
      (byte) 57,
      (byte) 248,
      (byte) 173,
      (byte) 3,
      (byte) 44,
      (byte) 136,
      (byte) 35,
      (byte) 135,
      (byte) 226,
      (byte) 44,
      (byte) 12,
      (byte) 102,
      (byte) 137,
      (byte) 108,
      (byte) 159,
      (byte) 177,
      (byte) 183,
      (byte) 217,
      (byte) 205,
      (byte) 186,
      (byte) 175
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[24]
    {
      (byte) 82,
      (byte) 191,
      (byte) 126,
      (byte) 168,
      (byte) 243,
      (byte) 153,
      (byte) 140,
      (byte) 202,
      (byte) 121,
      (byte) 88,
      (byte) 11,
      (byte) 229,
      (byte) 51,
      (byte) 218,
      (byte) 38,
      (byte) 241,
      (byte) 192 /*0xC0*/,
      (byte) 10,
      (byte) 169,
      (byte) 214,
      (byte) 227,
      (byte) 115,
      (byte) 9,
      (byte) 87
    };
    byte[] numArray10 = new byte[24];
    numArray10[23] = (byte) 89;
    numArray10[1] = (byte) 142;
    numArray10[2] = (byte) 27;
    numArray10[21] = (byte) 39;
    numArray10[4] = (byte) 79;
    numArray10[5] = (byte) 172;
    numArray10[9] = (byte) 5;
    numArray10[7] = (byte) 84;
    numArray10[6] = (byte) 193;
    numArray10[20] = (byte) 153;
    numArray10[22] = (byte) 140;
    numArray10[11] = (byte) 84;
    numArray10[12] = (byte) 161;
    numArray10[18] = (byte) 196;
    numArray10[14] = (byte) 14;
    numArray10[0] = (byte) 150;
    numArray10[15] = (byte) 43;
    numArray10[17] = (byte) 118;
    numArray10[8] = (byte) 184;
    numArray10[19] = (byte) 175;
    numArray10[3] = (byte) 149;
    numArray10[16 /*0x10*/] = (byte) 111;
    numArray10[10] = (byte) 200;
    numArray10[13] = (byte) 99;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 24);
    for (int index = 0; index < 24; ++index)
      numArray6[index + 55] ^= numArray10[index];
    byte[] numArray11 = new byte[46];
    byte[] response = new byte[46];
    Array.Copy((Array) sc_8027.sspq, 0, (Array) numArray11, 0, 46);
    key.Query(true, 335, numArray11, response);
    Array.Copy((Array) sc_8027.sspr, 0, (Array) numArray11, 0, 46);
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

  internal static string ssp_appserver_8032()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[77];
      byte[] numArray2 = new byte[55]
      {
        (byte) 95,
        (byte) 235,
        (byte) 178,
        (byte) 79,
        (byte) 149,
        (byte) 138,
        (byte) 5,
        (byte) 232,
        (byte) 153,
        (byte) 16 /*0x10*/,
        (byte) 110,
        (byte) 106,
        (byte) 252,
        (byte) 144 /*0x90*/,
        (byte) 23,
        (byte) 203,
        (byte) 60,
        (byte) 203,
        (byte) 129,
        (byte) 112 /*0x70*/,
        byte.MaxValue,
        (byte) 228,
        (byte) 44,
        (byte) 220,
        (byte) 20,
        (byte) 239,
        (byte) 210,
        (byte) 194,
        (byte) 17,
        (byte) 252,
        (byte) 229,
        (byte) 106,
        (byte) 202,
        (byte) 152,
        (byte) 55,
        (byte) 14,
        (byte) 121,
        (byte) 243,
        (byte) 40,
        (byte) 109,
        (byte) 223,
        (byte) 70,
        (byte) 227,
        (byte) 118,
        (byte) 208 /*0xD0*/,
        (byte) 8,
        (byte) 195,
        (byte) 107,
        (byte) 95,
        (byte) 52,
        (byte) 90,
        (byte) 233,
        (byte) 125,
        (byte) 57,
        (byte) 34
      };
      byte[] numArray3 = new byte[55];
      numArray3[15] = (byte) 81;
      numArray3[1] = (byte) 59;
      numArray3[2] = (byte) 136;
      numArray3[3] = (byte) 65;
      numArray3[4] = (byte) 16 /*0x10*/;
      numArray3[5] = (byte) 234;
      numArray3[6] = (byte) 103;
      numArray3[7] = (byte) 24;
      numArray3[14] = byte.MaxValue;
      numArray3[32 /*0x20*/] = (byte) 87;
      numArray3[10] = (byte) 232;
      numArray3[44] = (byte) 188;
      numArray3[29] = (byte) 253;
      numArray3[35] = (byte) 250;
      numArray3[28] = (byte) 101;
      numArray3[50] = (byte) 234;
      numArray3[24] = (byte) 22;
      numArray3[17] = (byte) 249;
      numArray3[18] = (byte) 212;
      numArray3[43] = (byte) 193;
      numArray3[16 /*0x10*/] = (byte) 56;
      numArray3[0] = (byte) 99;
      numArray3[22] = (byte) 186;
      numArray3[23] = (byte) 89;
      numArray3[33] = (byte) 17;
      numArray3[25] = (byte) 19;
      numArray3[27] = (byte) 27;
      numArray3[36] = (byte) 95;
      numArray3[37] = (byte) 6;
      numArray3[38] = (byte) 212;
      numArray3[11] = (byte) 45;
      numArray3[31 /*0x1F*/] = (byte) 70;
      numArray3[9] = (byte) 61;
      numArray3[13] = (byte) 63 /*0x3F*/;
      numArray3[34] = (byte) 50;
      numArray3[21] = (byte) 201;
      numArray3[19] = (byte) 110;
      numArray3[20] = (byte) 32 /*0x20*/;
      numArray3[41] = (byte) 1;
      numArray3[8] = (byte) 47;
      numArray3[40] = (byte) 8;
      numArray3[26] = (byte) 174;
      numArray3[42] = (byte) 106;
      numArray3[30] = (byte) 55;
      numArray3[49] = (byte) 241;
      numArray3[45] = (byte) 106;
      numArray3[46] = (byte) 61;
      numArray3[47] = (byte) 73;
      numArray3[51] = (byte) 9;
      numArray3[48 /*0x30*/] = (byte) 48 /*0x30*/;
      numArray3[12] = (byte) 188;
      numArray3[39] = (byte) 185;
      numArray3[52] = (byte) 217;
      numArray3[53] = (byte) 27;
      numArray3[54] = (byte) 8;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[22];
      numArray4[9] = (byte) 15;
      numArray4[18] = (byte) 73;
      numArray4[2] = (byte) 123;
      numArray4[3] = (byte) 80 /*0x50*/;
      numArray4[11] = (byte) 156;
      numArray4[8] = (byte) 23;
      numArray4[7] = (byte) 47;
      numArray4[1] = (byte) 47;
      numArray4[10] = (byte) 237;
      numArray4[5] = (byte) 249;
      numArray4[4] = (byte) 223;
      numArray4[0] = (byte) 201;
      numArray4[12] = (byte) 185;
      numArray4[13] = (byte) 173;
      numArray4[14] = (byte) 144 /*0x90*/;
      numArray4[6] = (byte) 250;
      numArray4[16 /*0x10*/] = (byte) 95;
      numArray4[17] = (byte) 244;
      numArray4[15] = (byte) 33;
      numArray4[19] = (byte) 185;
      numArray4[20] = (byte) 106;
      numArray4[21] = (byte) 64 /*0x40*/;
      byte[] numArray5 = new byte[22]
      {
        (byte) 141,
        (byte) 226,
        (byte) 81,
        (byte) 32 /*0x20*/,
        (byte) 50,
        (byte) 62,
        (byte) 172,
        (byte) 211,
        (byte) 64 /*0x40*/,
        (byte) 108,
        (byte) 32 /*0x20*/,
        (byte) 249,
        (byte) 55,
        (byte) 0,
        (byte) 179,
        (byte) 140,
        (byte) 52,
        (byte) 110,
        (byte) 139,
        (byte) 165,
        (byte) 27,
        (byte) 84
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 22);
      for (int index = 0; index < 22; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[77];
    byte[] numArray7 = new byte[55]
    {
      (byte) 52,
      (byte) 129,
      (byte) 37,
      (byte) 9,
      (byte) 178,
      (byte) 31 /*0x1F*/,
      (byte) 191,
      (byte) 159,
      (byte) 136,
      (byte) 140,
      (byte) 175,
      (byte) 249,
      (byte) 210,
      (byte) 253,
      (byte) 105,
      (byte) 121,
      (byte) 148,
      (byte) 38,
      (byte) 224 /*0xE0*/,
      (byte) 89,
      (byte) 114,
      (byte) 210,
      (byte) 8,
      (byte) 26,
      (byte) 250,
      (byte) 55,
      (byte) 211,
      (byte) 107,
      (byte) 77,
      (byte) 139,
      (byte) 74,
      (byte) 89,
      (byte) 225,
      (byte) 111,
      (byte) 198,
      (byte) 15,
      (byte) 144 /*0x90*/,
      (byte) 121,
      (byte) 54,
      (byte) 237,
      (byte) 131,
      (byte) 167,
      (byte) 82,
      (byte) 251,
      (byte) 86,
      (byte) 107,
      (byte) 235,
      (byte) 120,
      (byte) 227,
      (byte) 162,
      (byte) 31 /*0x1F*/,
      (byte) 50,
      (byte) 145,
      (byte) 26,
      (byte) 42
    };
    byte[] numArray8 = new byte[55];
    numArray8[14] = (byte) 114;
    numArray8[19] = (byte) 4;
    numArray8[9] = (byte) 100;
    numArray8[3] = (byte) 6;
    numArray8[20] = (byte) 215;
    numArray8[29] = (byte) 220;
    numArray8[6] = (byte) 54;
    numArray8[17] = (byte) 156;
    numArray8[48 /*0x30*/] = (byte) 151;
    numArray8[8] = (byte) 37;
    numArray8[32 /*0x20*/] = (byte) 11;
    numArray8[27] = (byte) 214;
    numArray8[12] = (byte) 25;
    numArray8[13] = (byte) 80 /*0x50*/;
    numArray8[25] = (byte) 1;
    numArray8[0] = (byte) 74;
    numArray8[16 /*0x10*/] = (byte) 222;
    numArray8[5] = (byte) 112 /*0x70*/;
    numArray8[18] = (byte) 93;
    numArray8[40] = (byte) 127 /*0x7F*/;
    numArray8[11] = (byte) 2;
    numArray8[21] = (byte) 28;
    numArray8[22] = (byte) 179;
    numArray8[23] = (byte) 134;
    numArray8[24] = (byte) 104;
    numArray8[15] = (byte) 188;
    numArray8[26] = (byte) 2;
    numArray8[41] = (byte) 152;
    numArray8[45] = (byte) 32 /*0x20*/;
    numArray8[35] = (byte) 241;
    numArray8[30] = (byte) 89;
    numArray8[31 /*0x1F*/] = (byte) 45;
    numArray8[7] = (byte) 213;
    numArray8[33] = (byte) 127 /*0x7F*/;
    numArray8[42] = (byte) 88;
    numArray8[28] = (byte) 180;
    numArray8[36] = (byte) 66;
    numArray8[38] = (byte) 176 /*0xB0*/;
    numArray8[34] = (byte) 13;
    numArray8[39] = (byte) 180;
    numArray8[37] = (byte) 132;
    numArray8[1] = (byte) 89;
    numArray8[10] = (byte) 52;
    numArray8[43] = (byte) 72;
    numArray8[44] = (byte) 84;
    numArray8[51] = (byte) 138;
    numArray8[46] = (byte) 62;
    numArray8[47] = (byte) 57;
    numArray8[53] = (byte) 141;
    numArray8[2] = (byte) 148;
    numArray8[50] = (byte) 120;
    numArray8[4] = (byte) 157;
    numArray8[52] = (byte) 76;
    numArray8[49] = (byte) 68;
    numArray8[54] = (byte) 167;
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[22]
    {
      (byte) 70,
      (byte) 128 /*0x80*/,
      (byte) 47,
      (byte) 241,
      (byte) 203,
      (byte) 166,
      (byte) 144 /*0x90*/,
      (byte) 42,
      (byte) 141,
      (byte) 80 /*0x50*/,
      (byte) 30,
      (byte) 210,
      (byte) 195,
      (byte) 97,
      (byte) 177,
      (byte) 53,
      (byte) 81,
      (byte) 166,
      (byte) 118,
      (byte) 43,
      (byte) 83,
      (byte) 195
    };
    byte[] numArray10 = new byte[22]
    {
      (byte) 61,
      (byte) 205,
      (byte) 242,
      (byte) 172,
      (byte) 201,
      (byte) 16 /*0x10*/,
      (byte) 106,
      (byte) 57,
      (byte) 154,
      (byte) 217,
      (byte) 149,
      (byte) 90,
      (byte) 57,
      (byte) 102,
      (byte) 97,
      (byte) 249,
      (byte) 135,
      (byte) 164,
      (byte) 237,
      (byte) 129,
      (byte) 21,
      (byte) 125
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 22);
    for (int index = 0; index < 22; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }
}
