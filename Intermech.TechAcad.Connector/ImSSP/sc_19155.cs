// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_19155
// Assembly: Intermech.TechAcad.Connector, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5A35A651-9A96-41F3-9839-2AAB5A952CB8
// Assembly location: D:\IPS\Client\Intermech.TechAcad.Connector.dll

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_19155
{
  private static byte[] sspq = new byte[33]
  {
    (byte) 245,
    (byte) 208 /*0xD0*/,
    (byte) 114,
    (byte) 234,
    (byte) 130,
    (byte) 240 /*0xF0*/,
    (byte) 198,
    (byte) 33,
    (byte) 48 /*0x30*/,
    (byte) 127 /*0x7F*/,
    (byte) 30,
    (byte) 23,
    (byte) 96 /*0x60*/,
    (byte) 27,
    (byte) 164,
    (byte) 238,
    (byte) 180,
    (byte) 105,
    (byte) 242,
    (byte) 94,
    (byte) 156,
    (byte) 85,
    (byte) 37,
    (byte) 252,
    (byte) 175,
    (byte) 29,
    (byte) 237,
    (byte) 113,
    (byte) 100,
    (byte) 72,
    (byte) 110,
    (byte) 83,
    (byte) 42
  };
  private static byte[] sspr = new byte[33]
  {
    (byte) 13,
    (byte) 113,
    (byte) 62,
    (byte) 7,
    (byte) 230,
    (byte) 72,
    (byte) 182,
    (byte) 233,
    (byte) 165,
    (byte) 80 /*0x50*/,
    (byte) 168,
    (byte) 190,
    (byte) 176 /*0xB0*/,
    (byte) 76,
    (byte) 102,
    (byte) 136,
    (byte) 54,
    (byte) 123,
    (byte) 116,
    (byte) 25,
    (byte) 190,
    (byte) 88,
    (byte) 55,
    (byte) 197,
    (byte) 150,
    (byte) 87,
    (byte) 32 /*0x20*/,
    (byte) 71,
    (byte) 76,
    (byte) 45,
    (byte) 130,
    (byte) 61,
    (byte) 57
  };

  internal static string ssp_techacad_19156()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[47];
      byte[] numArray2 = new byte[47]
      {
        (byte) 33,
        (byte) 64 /*0x40*/,
        (byte) 101,
        (byte) 146,
        (byte) 214,
        (byte) 117,
        byte.MaxValue,
        (byte) 133,
        (byte) 138,
        (byte) 132,
        (byte) 165,
        (byte) 171,
        (byte) 171,
        (byte) 180,
        (byte) 25,
        (byte) 164,
        (byte) 109,
        (byte) 153,
        (byte) 237,
        (byte) 214,
        (byte) 132,
        (byte) 182,
        (byte) 157,
        (byte) 41,
        (byte) 51,
        (byte) 105,
        (byte) 186,
        (byte) 78,
        (byte) 93,
        (byte) 73,
        (byte) 114,
        (byte) 208 /*0xD0*/,
        (byte) 213,
        (byte) 208 /*0xD0*/,
        (byte) 87,
        (byte) 184,
        (byte) 50,
        (byte) 153,
        (byte) 183,
        (byte) 223,
        (byte) 118,
        (byte) 210,
        (byte) 6,
        (byte) 36,
        (byte) 238,
        (byte) 93,
        (byte) 126
      };
      byte[] numArray3 = new byte[47]
      {
        (byte) 79,
        (byte) 22,
        (byte) 81,
        (byte) 248,
        (byte) 5,
        (byte) 21,
        (byte) 250,
        (byte) 235,
        (byte) 114,
        (byte) 14,
        (byte) 14,
        (byte) 23,
        (byte) 216,
        (byte) 198,
        (byte) 77,
        (byte) 129,
        (byte) 105,
        (byte) 233,
        (byte) 245,
        (byte) 131,
        (byte) 208 /*0xD0*/,
        (byte) 162,
        (byte) 203,
        (byte) 37,
        (byte) 55,
        (byte) 228,
        (byte) 48 /*0x30*/,
        (byte) 110,
        (byte) 197,
        (byte) 234,
        (byte) 45,
        (byte) 57,
        (byte) 165,
        (byte) 134,
        (byte) 249,
        (byte) 238,
        (byte) 8,
        (byte) 119,
        (byte) 218,
        (byte) 63 /*0x3F*/,
        (byte) 88,
        (byte) 69,
        (byte) 152,
        (byte) 53,
        (byte) 8,
        (byte) 170,
        (byte) 121
      };
      key.Query(true, 357, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 47);
      for (int index = 0; index < 47; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[47];
    byte[] numArray5 = new byte[47];
    numArray5[6] = (byte) 163;
    numArray5[41] = (byte) 246;
    numArray5[2] = (byte) 220;
    numArray5[3] = (byte) 207;
    numArray5[30] = (byte) 235;
    numArray5[5] = (byte) 106;
    numArray5[19] = (byte) 96 /*0x60*/;
    numArray5[7] = (byte) 59;
    numArray5[4] = (byte) 67;
    numArray5[31 /*0x1F*/] = (byte) 121;
    numArray5[32 /*0x20*/] = (byte) 221;
    numArray5[11] = (byte) 177;
    numArray5[8] = (byte) 95;
    numArray5[29] = (byte) 213;
    numArray5[14] = (byte) 17;
    numArray5[15] = (byte) 212;
    numArray5[46] = (byte) 170;
    numArray5[9] = (byte) 108;
    numArray5[16 /*0x10*/] = (byte) 213;
    numArray5[39] = (byte) 237;
    numArray5[20] = (byte) 114;
    numArray5[37] = (byte) 6;
    numArray5[22] = (byte) 225;
    numArray5[23] = (byte) 13;
    numArray5[38] = (byte) 141;
    numArray5[43] = (byte) 75;
    numArray5[26] = (byte) 176 /*0xB0*/;
    numArray5[27] = (byte) 121;
    numArray5[25] = (byte) 177;
    numArray5[1] = (byte) 254;
    numArray5[21] = (byte) 212;
    numArray5[28] = (byte) 58;
    numArray5[18] = (byte) 92;
    numArray5[12] = (byte) 39;
    numArray5[34] = (byte) 54;
    numArray5[40] = (byte) 21;
    numArray5[36] = (byte) 244;
    numArray5[10] = (byte) 186;
    numArray5[33] = (byte) 135;
    numArray5[24] = (byte) 95;
    numArray5[13] = (byte) 65;
    numArray5[0] = (byte) 19;
    numArray5[42] = (byte) 58;
    numArray5[35] = (byte) 116;
    numArray5[44] = (byte) 36;
    numArray5[45] = (byte) 196;
    numArray5[17] = (byte) 218;
    byte[] numArray6 = new byte[47];
    numArray6[1] = (byte) 47;
    numArray6[8] = (byte) 50;
    numArray6[28] = (byte) 164;
    numArray6[26] = (byte) 4;
    numArray6[31 /*0x1F*/] = (byte) 78;
    numArray6[40] = (byte) 67;
    numArray6[20] = (byte) 234;
    numArray6[12] = (byte) 47;
    numArray6[16 /*0x10*/] = (byte) 208 /*0xD0*/;
    numArray6[11] = (byte) 67;
    numArray6[10] = (byte) 43;
    numArray6[19] = (byte) 135;
    numArray6[4] = (byte) 42;
    numArray6[17] = (byte) 240 /*0xF0*/;
    numArray6[24] = (byte) 91;
    numArray6[5] = (byte) 154;
    numArray6[41] = (byte) 120;
    numArray6[9] = (byte) 40;
    numArray6[18] = (byte) 155;
    numArray6[22] = (byte) 226;
    numArray6[45] = (byte) 235;
    numArray6[6] = (byte) 82;
    numArray6[38] = (byte) 70;
    numArray6[23] = (byte) 67;
    numArray6[2] = (byte) 117;
    numArray6[25] = (byte) 54;
    numArray6[3] = (byte) 43;
    numArray6[15] = (byte) 138;
    numArray6[46] = (byte) 117;
    numArray6[27] = (byte) 126;
    numArray6[30] = (byte) 137;
    numArray6[14] = (byte) 197;
    numArray6[29] = (byte) 156;
    numArray6[33] = (byte) 59;
    numArray6[34] = (byte) 57;
    numArray6[35] = (byte) 239;
    numArray6[21] = (byte) 147;
    numArray6[37] = (byte) 162;
    numArray6[0] = (byte) 95;
    numArray6[7] = (byte) 217;
    numArray6[36] = (byte) 141;
    numArray6[32 /*0x20*/] = (byte) 31 /*0x1F*/;
    numArray6[42] = (byte) 54;
    numArray6[43] = (byte) 134;
    numArray6[44] = (byte) 49;
    numArray6[39] = (byte) 188;
    numArray6[13] = (byte) 187;
    key.Query(true, 357, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 47);
    for (int index = 0; index < 47; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_techacad_19157()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[34];
      byte[] numArray2 = new byte[34];
      numArray2[5] = (byte) 39;
      numArray2[27] = (byte) 249;
      numArray2[25] = (byte) 155;
      numArray2[15] = (byte) 130;
      numArray2[4] = (byte) 101;
      numArray2[7] = (byte) 115;
      numArray2[32 /*0x20*/] = (byte) 67;
      numArray2[17] = (byte) 57;
      numArray2[6] = (byte) 21;
      numArray2[9] = (byte) 54;
      numArray2[2] = (byte) 194;
      numArray2[18] = (byte) 99;
      numArray2[8] = (byte) 145;
      numArray2[13] = (byte) 48 /*0x30*/;
      numArray2[14] = (byte) 127 /*0x7F*/;
      numArray2[0] = (byte) 208 /*0xD0*/;
      numArray2[16 /*0x10*/] = (byte) 2;
      numArray2[26] = (byte) 211;
      numArray2[28] = (byte) 169;
      numArray2[11] = (byte) 140;
      numArray2[20] = (byte) 185;
      numArray2[1] = (byte) 64 /*0x40*/;
      numArray2[22] = (byte) 223;
      numArray2[23] = (byte) 42;
      numArray2[24] = (byte) 141;
      numArray2[19] = (byte) 98;
      numArray2[33] = (byte) 191;
      numArray2[12] = (byte) 166;
      numArray2[21] = (byte) 188;
      numArray2[29] = (byte) 69;
      numArray2[30] = (byte) 140;
      numArray2[31 /*0x1F*/] = (byte) 251;
      numArray2[3] = (byte) 112 /*0x70*/;
      numArray2[10] = (byte) 235;
      byte[] numArray3 = new byte[34];
      numArray3[0] = (byte) 248;
      numArray3[28] = (byte) 112 /*0x70*/;
      numArray3[6] = (byte) 62;
      numArray3[7] = (byte) 30;
      numArray3[4] = (byte) 155;
      numArray3[16 /*0x10*/] = (byte) 134;
      numArray3[10] = (byte) 6;
      numArray3[1] = (byte) 153;
      numArray3[12] = (byte) 228;
      numArray3[13] = (byte) 154;
      numArray3[18] = (byte) 181;
      numArray3[2] = (byte) 120;
      numArray3[20] = (byte) 211;
      numArray3[32 /*0x20*/] = (byte) 142;
      numArray3[14] = (byte) 187;
      numArray3[15] = (byte) 206;
      numArray3[30] = (byte) 195;
      numArray3[17] = (byte) 102;
      numArray3[11] = (byte) 5;
      numArray3[31 /*0x1F*/] = (byte) 92;
      numArray3[19] = (byte) 155;
      numArray3[21] = (byte) 71;
      numArray3[9] = (byte) 117;
      numArray3[23] = (byte) 125;
      numArray3[24] = (byte) 92;
      numArray3[25] = (byte) 137;
      numArray3[26] = (byte) 86;
      numArray3[27] = (byte) 240 /*0xF0*/;
      numArray3[22] = (byte) 178;
      numArray3[29] = (byte) 214;
      numArray3[8] = (byte) 85;
      numArray3[3] = (byte) 118;
      numArray3[33] = (byte) 223;
      numArray3[5] = (byte) 222;
      key.Query(true, 357, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 34);
      for (int index = 0; index < 34; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[34];
    byte[] numArray5 = new byte[34]
    {
      (byte) 206,
      (byte) 130,
      (byte) 124,
      (byte) 230,
      (byte) 88,
      (byte) 145,
      (byte) 81,
      (byte) 42,
      (byte) 198,
      (byte) 187,
      (byte) 62,
      (byte) 138,
      (byte) 49,
      (byte) 247,
      (byte) 3,
      (byte) 155,
      (byte) 221,
      (byte) 194,
      (byte) 91,
      (byte) 239,
      (byte) 48 /*0x30*/,
      (byte) 34,
      (byte) 88,
      (byte) 67,
      (byte) 158,
      (byte) 150,
      (byte) 149,
      (byte) 234,
      (byte) 159,
      (byte) 244,
      (byte) 106,
      (byte) 17,
      (byte) 49,
      (byte) 149
    };
    byte[] numArray6 = new byte[34];
    numArray6[3] = (byte) 98;
    numArray6[25] = (byte) 42;
    numArray6[2] = (byte) 164;
    numArray6[27] = (byte) 179;
    numArray6[4] = (byte) 53;
    numArray6[5] = (byte) 214;
    numArray6[29] = (byte) 34;
    numArray6[7] = (byte) 2;
    numArray6[22] = (byte) 161;
    numArray6[18] = (byte) 112 /*0x70*/;
    numArray6[28] = (byte) 74;
    numArray6[6] = (byte) 140;
    numArray6[9] = (byte) 254;
    numArray6[14] = (byte) 63 /*0x3F*/;
    numArray6[24] = (byte) 157;
    numArray6[32 /*0x20*/] = (byte) 222;
    numArray6[26] = (byte) 140;
    numArray6[17] = (byte) 155;
    numArray6[23] = (byte) 133;
    numArray6[19] = (byte) 228;
    numArray6[20] = (byte) 174;
    numArray6[11] = (byte) 145;
    numArray6[16 /*0x10*/] = (byte) 28;
    numArray6[12] = (byte) 238;
    numArray6[10] = (byte) 82;
    numArray6[13] = (byte) 63 /*0x3F*/;
    numArray6[15] = (byte) 82;
    numArray6[8] = (byte) 77;
    numArray6[21] = (byte) 149;
    numArray6[0] = (byte) 200;
    numArray6[30] = (byte) 116;
    numArray6[31 /*0x1F*/] = (byte) 104;
    numArray6[1] = (byte) 9;
    numArray6[33] = (byte) 49;
    key.Query(true, 357, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 34);
    for (int index = 0; index < 34; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_techacad_19158()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[32 /*0x20*/];
      byte[] numArray2 = new byte[32 /*0x20*/]
      {
        (byte) 247,
        (byte) 140,
        (byte) 42,
        (byte) 125,
        (byte) 234,
        (byte) 8,
        (byte) 156,
        (byte) 212,
        (byte) 26,
        (byte) 159,
        (byte) 199,
        (byte) 108,
        (byte) 170,
        (byte) 136,
        (byte) 106,
        (byte) 192 /*0xC0*/,
        (byte) 194,
        (byte) 218,
        (byte) 249,
        (byte) 159,
        (byte) 60,
        (byte) 9,
        (byte) 210,
        (byte) 93,
        (byte) 39,
        (byte) 103,
        byte.MaxValue,
        (byte) 121,
        (byte) 113,
        (byte) 224 /*0xE0*/,
        (byte) 132,
        (byte) 67
      };
      byte[] numArray3 = new byte[32 /*0x20*/]
      {
        (byte) 166,
        (byte) 69,
        (byte) 42,
        (byte) 20,
        (byte) 181,
        (byte) 129,
        (byte) 214,
        (byte) 159,
        (byte) 200,
        (byte) 166,
        (byte) 104,
        (byte) 238,
        (byte) 217,
        (byte) 162,
        (byte) 223,
        (byte) 204,
        (byte) 181,
        (byte) 112 /*0x70*/,
        (byte) 233,
        (byte) 73,
        (byte) 39,
        (byte) 196,
        (byte) 220,
        (byte) 39,
        (byte) 137,
        (byte) 115,
        (byte) 52,
        (byte) 58,
        (byte) 51,
        (byte) 65,
        (byte) 234,
        (byte) 112 /*0x70*/
      };
      key.Query(true, 357, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 32 /*0x20*/);
      for (int index = 0; index < 32 /*0x20*/; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[32 /*0x20*/];
    byte[] numArray5 = new byte[32 /*0x20*/];
    numArray5[21] = (byte) 185;
    numArray5[1] = (byte) 70;
    numArray5[2] = (byte) 85;
    numArray5[3] = (byte) 193;
    numArray5[4] = (byte) 34;
    numArray5[5] = (byte) 156;
    numArray5[31 /*0x1F*/] = (byte) 251;
    numArray5[30] = (byte) 132;
    numArray5[7] = (byte) 167;
    numArray5[14] = (byte) 151;
    numArray5[10] = (byte) 31 /*0x1F*/;
    numArray5[11] = (byte) 39;
    numArray5[12] = (byte) 200;
    numArray5[15] = (byte) 216;
    numArray5[28] = (byte) 58;
    numArray5[18] = (byte) 34;
    numArray5[6] = (byte) 115;
    numArray5[25] = (byte) 8;
    numArray5[17] = (byte) 229;
    numArray5[0] = (byte) 26;
    numArray5[20] = (byte) 238;
    numArray5[19] = (byte) 27;
    numArray5[13] = (byte) 195;
    numArray5[23] = (byte) 86;
    numArray5[24] = (byte) 131;
    numArray5[26] = (byte) 34;
    numArray5[8] = (byte) 92;
    numArray5[22] = (byte) 204;
    numArray5[9] = (byte) 159;
    numArray5[29] = (byte) 26;
    numArray5[27] = (byte) 99;
    numArray5[16 /*0x10*/] = (byte) 150;
    byte[] numArray6 = new byte[32 /*0x20*/]
    {
      (byte) 35,
      (byte) 145,
      (byte) 89,
      (byte) 193,
      (byte) 220,
      (byte) 184,
      (byte) 216,
      (byte) 141,
      (byte) 149,
      (byte) 204,
      (byte) 128 /*0x80*/,
      (byte) 87,
      (byte) 22,
      (byte) 72,
      (byte) 178,
      (byte) 64 /*0x40*/,
      (byte) 142,
      (byte) 2,
      (byte) 145,
      (byte) 218,
      (byte) 66,
      (byte) 39,
      (byte) 53,
      (byte) 193,
      (byte) 137,
      (byte) 193,
      (byte) 74,
      (byte) 60,
      (byte) 223,
      (byte) 29,
      (byte) 112 /*0x70*/,
      (byte) 10
    };
    key.Query(true, 357, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 32 /*0x20*/);
    for (int index = 0; index < 32 /*0x20*/; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_techacad_19159()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[31 /*0x1F*/];
      byte[] numArray2 = new byte[31 /*0x1F*/]
      {
        (byte) 115,
        (byte) 29,
        (byte) 108,
        (byte) 126,
        (byte) 14,
        (byte) 31 /*0x1F*/,
        (byte) 82,
        (byte) 140,
        (byte) 67,
        (byte) 14,
        (byte) 78,
        (byte) 67,
        (byte) 209,
        (byte) 107,
        (byte) 137,
        (byte) 51,
        (byte) 44,
        (byte) 38,
        (byte) 174,
        (byte) 225,
        (byte) 161,
        (byte) 238,
        byte.MaxValue,
        (byte) 108,
        (byte) 217,
        (byte) 189,
        (byte) 137,
        (byte) 184,
        (byte) 244,
        (byte) 53,
        (byte) 162
      };
      byte[] numArray3 = new byte[31 /*0x1F*/]
      {
        (byte) 47,
        (byte) 234,
        (byte) 50,
        (byte) 88,
        (byte) 129,
        (byte) 38,
        (byte) 13,
        (byte) 251,
        (byte) 5,
        (byte) 81,
        (byte) 112 /*0x70*/,
        (byte) 59,
        (byte) 70,
        (byte) 71,
        (byte) 182,
        (byte) 120,
        (byte) 95,
        (byte) 79,
        (byte) 5,
        (byte) 138,
        (byte) 143,
        (byte) 163,
        (byte) 76,
        (byte) 132,
        (byte) 67,
        (byte) 211,
        (byte) 127 /*0x7F*/,
        (byte) 41,
        (byte) 149,
        (byte) 145,
        (byte) 228
      };
      key.Query(true, 357, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 31 /*0x1F*/);
      for (int index = 0; index < 31 /*0x1F*/; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[31 /*0x1F*/];
    byte[] numArray5 = new byte[31 /*0x1F*/];
    numArray5[1] = (byte) 188;
    numArray5[23] = byte.MaxValue;
    numArray5[2] = (byte) 155;
    numArray5[3] = (byte) 111;
    numArray5[4] = (byte) 79;
    numArray5[8] = (byte) 29;
    numArray5[14] = (byte) 135;
    numArray5[7] = (byte) 112 /*0x70*/;
    numArray5[12] = (byte) 72;
    numArray5[18] = (byte) 116;
    numArray5[29] = (byte) 13;
    numArray5[11] = (byte) 251;
    numArray5[25] = (byte) 80 /*0x50*/;
    numArray5[13] = (byte) 13;
    numArray5[26] = (byte) 149;
    numArray5[15] = (byte) 58;
    numArray5[6] = (byte) 131;
    numArray5[17] = (byte) 245;
    numArray5[28] = (byte) 152;
    numArray5[19] = (byte) 142;
    numArray5[20] = (byte) 137;
    numArray5[21] = (byte) 132;
    numArray5[22] = (byte) 29;
    numArray5[10] = (byte) 177;
    numArray5[16 /*0x10*/] = (byte) 20;
    numArray5[0] = (byte) 66;
    numArray5[9] = (byte) 107;
    numArray5[24] = (byte) 165;
    numArray5[5] = (byte) 46;
    numArray5[27] = (byte) 154;
    numArray5[30] = (byte) 108;
    byte[] numArray6 = new byte[31 /*0x1F*/]
    {
      (byte) 70,
      (byte) 128 /*0x80*/,
      (byte) 237,
      (byte) 205,
      (byte) 141,
      (byte) 145,
      (byte) 210,
      (byte) 15,
      (byte) 129,
      (byte) 230,
      (byte) 18,
      (byte) 148,
      (byte) 104,
      (byte) 253,
      (byte) 56,
      (byte) 137,
      (byte) 188,
      (byte) 247,
      (byte) 18,
      (byte) 219,
      (byte) 137,
      (byte) 100,
      (byte) 175,
      (byte) 69,
      (byte) 136,
      (byte) 180,
      (byte) 197,
      (byte) 112 /*0x70*/,
      (byte) 194,
      (byte) 89,
      (byte) 12
    };
    key.Query(true, 357, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 31 /*0x1F*/);
    for (int index = 0; index < 31 /*0x1F*/; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_techacad_19160()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[31 /*0x1F*/];
      byte[] numArray2 = new byte[31 /*0x1F*/]
      {
        (byte) 136,
        (byte) 131,
        (byte) 228,
        (byte) 185,
        (byte) 162,
        (byte) 87,
        (byte) 30,
        (byte) 244,
        (byte) 48 /*0x30*/,
        (byte) 88,
        (byte) 179,
        (byte) 138,
        (byte) 68,
        (byte) 199,
        (byte) 219,
        (byte) 25,
        (byte) 104,
        (byte) 221,
        (byte) 235,
        (byte) 191,
        (byte) 250,
        (byte) 48 /*0x30*/,
        (byte) 65,
        (byte) 107,
        (byte) 47,
        (byte) 252,
        (byte) 241,
        (byte) 215,
        (byte) 232,
        (byte) 115,
        (byte) 88
      };
      byte[] numArray3 = new byte[31 /*0x1F*/];
      numArray3[0] = (byte) 172;
      numArray3[1] = (byte) 205;
      numArray3[30] = (byte) 12;
      numArray3[3] = (byte) 81;
      numArray3[22] = (byte) 185;
      numArray3[5] = (byte) 138;
      numArray3[7] = (byte) 125;
      numArray3[21] = (byte) 103;
      numArray3[19] = (byte) 201;
      numArray3[13] = (byte) 185;
      numArray3[26] = (byte) 209;
      numArray3[14] = (byte) 55;
      numArray3[12] = (byte) 212;
      numArray3[9] = (byte) 194;
      numArray3[8] = (byte) 39;
      numArray3[4] = (byte) 2;
      numArray3[16 /*0x10*/] = (byte) 92;
      numArray3[17] = (byte) 83;
      numArray3[18] = (byte) 7;
      numArray3[2] = (byte) 242;
      numArray3[20] = (byte) 101;
      numArray3[10] = (byte) 92;
      numArray3[15] = (byte) 108;
      numArray3[23] = (byte) 23;
      numArray3[24] = (byte) 23;
      numArray3[6] = (byte) 70;
      numArray3[25] = (byte) 229;
      numArray3[27] = (byte) 164;
      numArray3[28] = (byte) 215;
      numArray3[29] = (byte) 221;
      numArray3[11] = (byte) 29;
      key.Query(true, 357, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 31 /*0x1F*/);
      for (int index = 0; index < 31 /*0x1F*/; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[33];
      byte[] response = new byte[33];
      Array.Copy((Array) sc_19155.sspq, 0, (Array) numArray4, 0, 33);
      key.Query(true, 357, numArray4, response);
      Array.Copy((Array) sc_19155.sspr, 0, (Array) numArray4, 0, 33);
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
    byte[] numArray5 = new byte[31 /*0x1F*/];
    byte[] numArray6 = new byte[31 /*0x1F*/]
    {
      (byte) 23,
      (byte) 30,
      (byte) 134,
      (byte) 145,
      (byte) 56,
      (byte) 251,
      (byte) 144 /*0x90*/,
      (byte) 52,
      (byte) 3,
      (byte) 15,
      (byte) 206,
      (byte) 57,
      (byte) 42,
      (byte) 24,
      (byte) 111,
      (byte) 1,
      (byte) 119,
      (byte) 145,
      (byte) 162,
      (byte) 202,
      (byte) 161,
      (byte) 86,
      (byte) 160 /*0xA0*/,
      (byte) 171,
      (byte) 118,
      (byte) 59,
      (byte) 100,
      (byte) 3,
      (byte) 84,
      (byte) 189,
      (byte) 231
    };
    byte[] numArray7 = new byte[31 /*0x1F*/]
    {
      (byte) 238,
      (byte) 227,
      (byte) 173,
      (byte) 246,
      (byte) 11,
      (byte) 253,
      (byte) 174,
      (byte) 129,
      (byte) 214,
      (byte) 140,
      (byte) 145,
      (byte) 37,
      (byte) 86,
      (byte) 243,
      (byte) 207,
      (byte) 126,
      (byte) 86,
      (byte) 234,
      (byte) 102,
      (byte) 67,
      (byte) 154,
      (byte) 161,
      (byte) 228,
      (byte) 64 /*0x40*/,
      (byte) 114,
      (byte) 77,
      (byte) 90,
      (byte) 31 /*0x1F*/,
      (byte) 196,
      (byte) 182,
      (byte) 114
    };
    key.Query(true, 357, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 31 /*0x1F*/);
    for (int index = 0; index < 31 /*0x1F*/; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_techacad_19161()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[74];
      byte[] numArray2 = new byte[55]
      {
        (byte) 252,
        (byte) 29,
        (byte) 92,
        (byte) 123,
        (byte) 109,
        (byte) 98,
        (byte) 42,
        (byte) 153,
        (byte) 69,
        (byte) 87,
        (byte) 194,
        (byte) 132,
        (byte) 5,
        (byte) 125,
        (byte) 111,
        (byte) 236,
        (byte) 183,
        (byte) 203,
        (byte) 136,
        (byte) 136,
        (byte) 193,
        (byte) 126,
        (byte) 219,
        (byte) 27,
        (byte) 253,
        (byte) 150,
        (byte) 140,
        (byte) 223,
        (byte) 69,
        (byte) 39,
        (byte) 190,
        (byte) 110,
        (byte) 61,
        (byte) 207,
        (byte) 115,
        (byte) 227,
        (byte) 26,
        (byte) 21,
        (byte) 52,
        (byte) 104,
        (byte) 118,
        (byte) 188,
        (byte) 90,
        (byte) 83,
        (byte) 179,
        (byte) 182,
        (byte) 95,
        (byte) 133,
        (byte) 228,
        (byte) 208 /*0xD0*/,
        (byte) 48 /*0x30*/,
        (byte) 68,
        (byte) 66,
        (byte) 100,
        (byte) 135
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 93,
        (byte) 55,
        (byte) 226,
        (byte) 158,
        (byte) 9,
        (byte) 41,
        (byte) 42,
        (byte) 99,
        (byte) 135,
        (byte) 48 /*0x30*/,
        (byte) 12,
        (byte) 86,
        (byte) 155,
        (byte) 195,
        (byte) 237,
        (byte) 158,
        (byte) 87,
        (byte) 25,
        (byte) 8,
        (byte) 73,
        (byte) 28,
        (byte) 230,
        (byte) 128 /*0x80*/,
        (byte) 85,
        (byte) 133,
        (byte) 149,
        (byte) 30,
        (byte) 204,
        (byte) 7,
        (byte) 234,
        (byte) 77,
        (byte) 105,
        (byte) 86,
        (byte) 72,
        (byte) 95,
        (byte) 122,
        (byte) 125,
        (byte) 145,
        (byte) 44,
        (byte) 204,
        (byte) 77,
        (byte) 214,
        (byte) 156,
        (byte) 92,
        (byte) 196,
        (byte) 48 /*0x30*/,
        (byte) 11,
        (byte) 94,
        (byte) 244,
        (byte) 165,
        (byte) 159,
        (byte) 244,
        (byte) 108,
        (byte) 51,
        (byte) 246
      };
      key.Query(true, 357, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[19]
      {
        (byte) 65,
        (byte) 126,
        (byte) 214,
        (byte) 213,
        (byte) 75,
        (byte) 83,
        (byte) 29,
        (byte) 153,
        (byte) 83,
        (byte) 70,
        (byte) 54,
        (byte) 10,
        (byte) 139,
        (byte) 210,
        (byte) 161,
        (byte) 150,
        (byte) 32 /*0x20*/,
        (byte) 18,
        (byte) 4
      };
      byte[] numArray5 = new byte[19]
      {
        (byte) 250,
        (byte) 173,
        (byte) 118,
        (byte) 115,
        (byte) 50,
        (byte) 167,
        (byte) 229,
        (byte) 25,
        (byte) 7,
        (byte) 88,
        (byte) 105,
        (byte) 89,
        (byte) 120,
        (byte) 170,
        (byte) 183,
        (byte) 154,
        (byte) 253,
        (byte) 90,
        (byte) 220
      };
      key.Query(true, 357, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[74];
    byte[] numArray7 = new byte[55]
    {
      (byte) 70,
      (byte) 55,
      (byte) 6,
      (byte) 250,
      (byte) 117,
      (byte) 247,
      (byte) 121,
      (byte) 44,
      (byte) 217,
      (byte) 71,
      (byte) 245,
      (byte) 161,
      (byte) 216,
      (byte) 151,
      (byte) 93,
      (byte) 146,
      (byte) 43,
      (byte) 105,
      (byte) 60,
      (byte) 186,
      (byte) 95,
      (byte) 112 /*0x70*/,
      (byte) 40,
      (byte) 123,
      (byte) 89,
      (byte) 253,
      (byte) 143,
      (byte) 173,
      (byte) 33,
      (byte) 82,
      (byte) 130,
      (byte) 89,
      (byte) 186,
      (byte) 218,
      (byte) 56,
      (byte) 159,
      (byte) 206,
      (byte) 80 /*0x50*/,
      (byte) 222,
      (byte) 38,
      (byte) 31 /*0x1F*/,
      (byte) 39,
      (byte) 109,
      (byte) 238,
      (byte) 20,
      (byte) 234,
      (byte) 249,
      (byte) 10,
      (byte) 56,
      (byte) 141,
      (byte) 230,
      (byte) 155,
      (byte) 231,
      (byte) 204,
      (byte) 7
    };
    byte[] numArray8 = new byte[55]
    {
      (byte) 16 /*0x10*/,
      (byte) 254,
      (byte) 13,
      (byte) 48 /*0x30*/,
      (byte) 165,
      (byte) 162,
      (byte) 55,
      (byte) 131,
      (byte) 0,
      (byte) 118,
      (byte) 144 /*0x90*/,
      (byte) 18,
      (byte) 221,
      (byte) 73,
      (byte) 49,
      (byte) 254,
      (byte) 42,
      (byte) 27,
      (byte) 54,
      (byte) 149,
      (byte) 27,
      (byte) 11,
      (byte) 57,
      (byte) 32 /*0x20*/,
      (byte) 88,
      (byte) 209,
      (byte) 79,
      (byte) 91,
      (byte) 254,
      (byte) 228,
      (byte) 228,
      (byte) 72,
      (byte) 224 /*0xE0*/,
      (byte) 125,
      (byte) 246,
      (byte) 118,
      (byte) 212,
      (byte) 207,
      (byte) 251,
      (byte) 242,
      (byte) 152,
      (byte) 141,
      (byte) 200,
      (byte) 80 /*0x50*/,
      (byte) 102,
      (byte) 100,
      (byte) 235,
      (byte) 117,
      (byte) 94,
      (byte) 113,
      (byte) 26,
      (byte) 23,
      (byte) 209,
      (byte) 105,
      (byte) 73
    };
    key.Query(true, 357, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[19];
    numArray9[18] = (byte) 56;
    numArray9[1] = (byte) 161;
    numArray9[15] = (byte) 138;
    numArray9[5] = (byte) 10;
    numArray9[0] = (byte) 139;
    numArray9[4] = (byte) 203;
    numArray9[8] = (byte) 156;
    numArray9[7] = (byte) 196;
    numArray9[3] = (byte) 104;
    numArray9[9] = (byte) 230;
    numArray9[10] = (byte) 254;
    numArray9[11] = (byte) 95;
    numArray9[6] = (byte) 135;
    numArray9[13] = (byte) 195;
    numArray9[2] = (byte) 119;
    numArray9[14] = (byte) 219;
    numArray9[16 /*0x10*/] = (byte) 22;
    numArray9[17] = (byte) 154;
    numArray9[12] = (byte) 190;
    byte[] numArray10 = new byte[19];
    numArray10[18] = (byte) 178;
    numArray10[1] = (byte) 48 /*0x30*/;
    numArray10[2] = (byte) 163;
    numArray10[17] = (byte) 100;
    numArray10[3] = (byte) 176 /*0xB0*/;
    numArray10[5] = (byte) 195;
    numArray10[14] = (byte) 96 /*0x60*/;
    numArray10[7] = (byte) 45;
    numArray10[8] = (byte) 201;
    numArray10[15] = (byte) 83;
    numArray10[10] = (byte) 82;
    numArray10[12] = (byte) 69;
    numArray10[11] = (byte) 189;
    numArray10[13] = (byte) 229;
    numArray10[16 /*0x10*/] = (byte) 140;
    numArray10[0] = (byte) 144 /*0x90*/;
    numArray10[4] = (byte) 170;
    numArray10[6] = (byte) 134;
    numArray10[9] = (byte) 148;
    key.Query(true, 357, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 19);
    for (int index = 0; index < 19; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_techacad_19162()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[33];
      byte[] numArray2 = new byte[33]
      {
        (byte) 27,
        (byte) 221,
        (byte) 42,
        (byte) 248,
        (byte) 44,
        (byte) 253,
        (byte) 46,
        (byte) 128 /*0x80*/,
        (byte) 44,
        (byte) 35,
        (byte) 87,
        (byte) 70,
        (byte) 27,
        (byte) 28,
        (byte) 16 /*0x10*/,
        (byte) 238,
        (byte) 107,
        (byte) 9,
        (byte) 218,
        (byte) 250,
        (byte) 202,
        (byte) 96 /*0x60*/,
        (byte) 214,
        (byte) 160 /*0xA0*/,
        (byte) 187,
        (byte) 2,
        (byte) 142,
        (byte) 187,
        (byte) 29,
        (byte) 124,
        (byte) 244,
        (byte) 219,
        (byte) 253
      };
      byte[] numArray3 = new byte[33];
      numArray3[7] = (byte) 10;
      numArray3[1] = (byte) 158;
      numArray3[2] = (byte) 91;
      numArray3[3] = (byte) 18;
      numArray3[4] = (byte) 12;
      numArray3[22] = (byte) 121;
      numArray3[5] = (byte) 108;
      numArray3[27] = (byte) 183;
      numArray3[8] = (byte) 163;
      numArray3[6] = (byte) 103;
      numArray3[13] = (byte) 56;
      numArray3[28] = (byte) 132;
      numArray3[12] = (byte) 172;
      numArray3[19] = (byte) 181;
      numArray3[31 /*0x1F*/] = (byte) 117;
      numArray3[15] = (byte) 49;
      numArray3[14] = (byte) 126;
      numArray3[17] = (byte) 65;
      numArray3[20] = (byte) 47;
      numArray3[32 /*0x20*/] = (byte) 166;
      numArray3[16 /*0x10*/] = (byte) 36;
      numArray3[25] = (byte) 240 /*0xF0*/;
      numArray3[30] = (byte) 177;
      numArray3[23] = (byte) 86;
      numArray3[29] = (byte) 185;
      numArray3[9] = (byte) 211;
      numArray3[26] = (byte) 245;
      numArray3[0] = (byte) 119;
      numArray3[24] = (byte) 100;
      numArray3[18] = (byte) 223;
      numArray3[21] = (byte) 101;
      numArray3[11] = (byte) 116;
      numArray3[10] = (byte) 79;
      key.Query(true, 357, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 33);
      for (int index = 0; index < 33; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[33];
    byte[] numArray5 = new byte[33]
    {
      (byte) 195,
      (byte) 160 /*0xA0*/,
      (byte) 96 /*0x60*/,
      (byte) 58,
      (byte) 44,
      (byte) 175,
      (byte) 129,
      (byte) 129,
      (byte) 233,
      (byte) 38,
      (byte) 207,
      (byte) 77,
      (byte) 215,
      (byte) 246,
      (byte) 229,
      (byte) 181,
      (byte) 138,
      (byte) 210,
      (byte) 161,
      (byte) 249,
      (byte) 15,
      (byte) 69,
      (byte) 194,
      (byte) 206,
      (byte) 164,
      (byte) 62,
      (byte) 216,
      (byte) 229,
      (byte) 47,
      (byte) 189,
      (byte) 253,
      (byte) 163,
      (byte) 62
    };
    byte[] numArray6 = new byte[33];
    numArray6[6] = (byte) 96 /*0x60*/;
    numArray6[1] = (byte) 3;
    numArray6[27] = (byte) 250;
    numArray6[3] = (byte) 178;
    numArray6[25] = (byte) 6;
    numArray6[4] = (byte) 160 /*0xA0*/;
    numArray6[23] = (byte) 8;
    numArray6[7] = (byte) 193;
    numArray6[8] = (byte) 136;
    numArray6[0] = (byte) 145;
    numArray6[10] = (byte) 67;
    numArray6[30] = (byte) 100;
    numArray6[12] = (byte) 159;
    numArray6[32 /*0x20*/] = (byte) 122;
    numArray6[14] = (byte) 187;
    numArray6[13] = (byte) 218;
    numArray6[11] = (byte) 230;
    numArray6[17] = (byte) 178;
    numArray6[18] = (byte) 204;
    numArray6[19] = (byte) 240 /*0xF0*/;
    numArray6[9] = (byte) 217;
    numArray6[21] = (byte) 80 /*0x50*/;
    numArray6[22] = (byte) 140;
    numArray6[15] = (byte) 77;
    numArray6[24] = (byte) 101;
    numArray6[29] = (byte) 227;
    numArray6[2] = (byte) 176 /*0xB0*/;
    numArray6[28] = (byte) 185;
    numArray6[5] = (byte) 238;
    numArray6[16 /*0x10*/] = (byte) 168;
    numArray6[26] = (byte) 82;
    numArray6[31 /*0x1F*/] = (byte) 111;
    numArray6[20] = (byte) 110;
    key.Query(true, 357, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 33);
    for (int index = 0; index < 33; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_techacad_19163()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[36];
      byte[] numArray2 = new byte[36];
      numArray2[17] = (byte) 195;
      numArray2[31 /*0x1F*/] = (byte) 37;
      numArray2[2] = (byte) 245;
      numArray2[6] = (byte) 40;
      numArray2[9] = (byte) 199;
      numArray2[5] = (byte) 215;
      numArray2[35] = (byte) 73;
      numArray2[25] = (byte) 142;
      numArray2[8] = (byte) 97;
      numArray2[4] = (byte) 206;
      numArray2[15] = (byte) 83;
      numArray2[11] = (byte) 91;
      numArray2[0] = (byte) 79;
      numArray2[13] = (byte) 53;
      numArray2[33] = (byte) 46;
      numArray2[22] = (byte) 152;
      numArray2[16 /*0x10*/] = (byte) 251;
      numArray2[1] = (byte) 91;
      numArray2[10] = (byte) 166;
      numArray2[19] = (byte) 130;
      numArray2[26] = (byte) 230;
      numArray2[21] = (byte) 212;
      numArray2[34] = (byte) 170;
      numArray2[28] = (byte) 60;
      numArray2[24] = (byte) 214;
      numArray2[20] = (byte) 184;
      numArray2[30] = (byte) 205;
      numArray2[27] = (byte) 241;
      numArray2[29] = (byte) 64 /*0x40*/;
      numArray2[18] = (byte) 35;
      numArray2[14] = (byte) 24;
      numArray2[23] = (byte) 238;
      numArray2[32 /*0x20*/] = (byte) 135;
      numArray2[3] = (byte) 244;
      numArray2[7] = (byte) 93;
      numArray2[12] = (byte) 103;
      byte[] numArray3 = new byte[36];
      numArray3[3] = (byte) 249;
      numArray3[30] = (byte) 22;
      numArray3[16 /*0x10*/] = (byte) 165;
      numArray3[22] = (byte) 177;
      numArray3[4] = (byte) 206;
      numArray3[31 /*0x1F*/] = (byte) 2;
      numArray3[6] = (byte) 152;
      numArray3[7] = (byte) 55;
      numArray3[21] = (byte) 244;
      numArray3[0] = (byte) 153;
      numArray3[23] = (byte) 249;
      numArray3[11] = (byte) 33;
      numArray3[12] = (byte) 253;
      numArray3[5] = (byte) 139;
      numArray3[19] = (byte) 94;
      numArray3[1] = (byte) 146;
      numArray3[35] = (byte) 113;
      numArray3[17] = (byte) 41;
      numArray3[24] = (byte) 60;
      numArray3[29] = (byte) 123;
      numArray3[28] = (byte) 34;
      numArray3[8] = (byte) 42;
      numArray3[9] = (byte) 100;
      numArray3[20] = (byte) 85;
      numArray3[15] = (byte) 83;
      numArray3[25] = (byte) 136;
      numArray3[26] = (byte) 219;
      numArray3[27] = (byte) 120;
      numArray3[32 /*0x20*/] = (byte) 168;
      numArray3[2] = (byte) 152;
      numArray3[13] = (byte) 55;
      numArray3[10] = (byte) 103;
      numArray3[14] = (byte) 39;
      numArray3[33] = (byte) 175;
      numArray3[34] = (byte) 201;
      numArray3[18] = (byte) 58;
      key.Query(true, 357, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 36);
      for (int index = 0; index < 36; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[36];
    byte[] numArray5 = new byte[36];
    numArray5[32 /*0x20*/] = (byte) 57;
    numArray5[22] = (byte) 226;
    numArray5[9] = (byte) 171;
    numArray5[11] = (byte) 160 /*0xA0*/;
    numArray5[8] = (byte) 121;
    numArray5[30] = (byte) 64 /*0x40*/;
    numArray5[6] = (byte) 185;
    numArray5[14] = (byte) 239;
    numArray5[23] = (byte) 178;
    numArray5[15] = (byte) 179;
    numArray5[0] = (byte) 249;
    numArray5[10] = (byte) 191;
    numArray5[17] = (byte) 63 /*0x3F*/;
    numArray5[13] = (byte) 73;
    numArray5[27] = (byte) 49;
    numArray5[18] = (byte) 13;
    numArray5[16 /*0x10*/] = (byte) 169;
    numArray5[3] = (byte) 229;
    numArray5[5] = (byte) 89;
    numArray5[2] = (byte) 195;
    numArray5[20] = (byte) 146;
    numArray5[21] = (byte) 246;
    numArray5[28] = (byte) 37;
    numArray5[35] = (byte) 80 /*0x50*/;
    numArray5[24] = (byte) 94;
    numArray5[33] = (byte) 87;
    numArray5[26] = (byte) 154;
    numArray5[12] = (byte) 91;
    numArray5[7] = (byte) 17;
    numArray5[29] = (byte) 75;
    numArray5[19] = (byte) 215;
    numArray5[31 /*0x1F*/] = (byte) 205;
    numArray5[4] = (byte) 40;
    numArray5[1] = (byte) 94;
    numArray5[34] = (byte) 140;
    numArray5[25] = (byte) 54;
    byte[] numArray6 = new byte[36]
    {
      (byte) 103,
      (byte) 211,
      (byte) 113,
      (byte) 197,
      (byte) 129,
      (byte) 150,
      (byte) 3,
      (byte) 167,
      (byte) 128 /*0x80*/,
      (byte) 251,
      (byte) 142,
      (byte) 100,
      (byte) 52,
      (byte) 227,
      (byte) 228,
      (byte) 9,
      (byte) 208 /*0xD0*/,
      (byte) 153,
      (byte) 54,
      (byte) 231,
      (byte) 174,
      (byte) 132,
      (byte) 31 /*0x1F*/,
      (byte) 40,
      (byte) 76,
      (byte) 86,
      (byte) 154,
      (byte) 249,
      (byte) 15,
      (byte) 55,
      (byte) 250,
      (byte) 131,
      (byte) 123,
      (byte) 122,
      (byte) 79,
      (byte) 83
    };
    key.Query(true, 357, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 36);
    for (int index = 0; index < 36; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
