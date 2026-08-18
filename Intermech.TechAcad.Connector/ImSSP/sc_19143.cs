// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_19143
// Assembly: Intermech.TechAcad.Connector, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5A35A651-9A96-41F3-9839-2AAB5A952CB8
// Assembly location: D:\IPS\Client\Intermech.TechAcad.Connector.dll

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_19143
{
  private static byte[] sspq = new byte[(int) sbyte.MaxValue]
  {
    (byte) 40,
    (byte) 146,
    (byte) 80 /*0x50*/,
    (byte) 212,
    (byte) 138,
    (byte) 89,
    (byte) 180,
    (byte) 87,
    (byte) 200,
    (byte) 13,
    (byte) 188,
    (byte) 14,
    (byte) 27,
    (byte) 253,
    (byte) 155,
    (byte) 96 /*0x60*/,
    (byte) 251,
    (byte) 123,
    (byte) 49,
    (byte) 30,
    (byte) 229,
    (byte) 25,
    (byte) 66,
    (byte) 213,
    (byte) 97,
    (byte) 251,
    (byte) 71,
    (byte) 59,
    (byte) 1,
    (byte) 28,
    (byte) 129,
    (byte) 219,
    (byte) 148,
    (byte) 223,
    (byte) 145,
    (byte) 234,
    (byte) 184,
    (byte) 124,
    (byte) 115,
    (byte) 22,
    (byte) 111,
    (byte) 201,
    (byte) 240 /*0xF0*/,
    (byte) 58,
    (byte) 119,
    (byte) 192 /*0xC0*/,
    (byte) 203,
    (byte) 250,
    (byte) 106,
    (byte) 126,
    (byte) 149,
    (byte) 165,
    (byte) 63 /*0x3F*/,
    (byte) 163,
    (byte) 186,
    (byte) 99,
    (byte) 177,
    (byte) 172,
    (byte) 137,
    (byte) 95,
    (byte) 204,
    (byte) 1,
    (byte) 40,
    (byte) 196,
    (byte) 78,
    (byte) 218,
    (byte) 222,
    (byte) 8,
    (byte) 47,
    (byte) 174,
    (byte) 132,
    (byte) 67,
    (byte) 52,
    (byte) 94,
    (byte) 220,
    (byte) 97,
    (byte) 220,
    (byte) 232,
    (byte) 141,
    (byte) 32 /*0x20*/,
    (byte) 20,
    (byte) 111,
    (byte) 192 /*0xC0*/,
    (byte) 89,
    (byte) 139,
    (byte) 19,
    (byte) 43,
    (byte) 12,
    (byte) 49,
    (byte) 43,
    (byte) 2,
    (byte) 67,
    (byte) 153,
    (byte) 107,
    (byte) 42,
    (byte) 21,
    (byte) 22,
    (byte) 152,
    (byte) 159,
    (byte) 57,
    (byte) 132,
    (byte) 122,
    (byte) 83,
    (byte) 116,
    (byte) 253,
    (byte) 168,
    (byte) 14,
    (byte) 245,
    (byte) 40,
    (byte) 144 /*0x90*/,
    (byte) 223,
    (byte) 17,
    (byte) 196,
    (byte) 80 /*0x50*/,
    (byte) 203,
    (byte) 88,
    (byte) 151,
    (byte) 92,
    (byte) 209,
    (byte) 220,
    (byte) 170,
    (byte) 101,
    (byte) 129,
    (byte) 166,
    (byte) 152,
    (byte) 107,
    (byte) 124
  };
  private static byte[] sspr = new byte[(int) sbyte.MaxValue]
  {
    (byte) 164,
    (byte) 169,
    (byte) 230,
    (byte) 76,
    (byte) 124,
    (byte) 27,
    (byte) 216,
    (byte) 3,
    (byte) 36,
    (byte) 156,
    (byte) 179,
    (byte) 40,
    (byte) 220,
    (byte) 87,
    (byte) 73,
    (byte) 51,
    (byte) 184,
    (byte) 164,
    (byte) 164,
    (byte) 121,
    (byte) 93,
    (byte) 119,
    (byte) 153,
    (byte) 161,
    (byte) 234,
    (byte) 97,
    (byte) 241,
    (byte) 17,
    (byte) 151,
    (byte) 86,
    (byte) 226,
    (byte) 165,
    (byte) 132,
    (byte) 203,
    (byte) 4,
    (byte) 239,
    (byte) 242,
    (byte) 209,
    (byte) 100,
    (byte) 4,
    (byte) 66,
    (byte) 249,
    (byte) 179,
    (byte) 227,
    (byte) 118,
    (byte) 146,
    (byte) 115,
    (byte) 120,
    (byte) 91,
    (byte) 70,
    (byte) 238,
    (byte) 25,
    (byte) 247,
    (byte) 59,
    (byte) 83,
    (byte) 155,
    (byte) 27,
    (byte) 153,
    (byte) 220,
    (byte) 191,
    (byte) 83,
    (byte) 26,
    (byte) 132,
    (byte) 254,
    (byte) 154,
    (byte) 178,
    (byte) 114,
    (byte) 41,
    (byte) 27,
    (byte) 126,
    (byte) 178,
    (byte) 26,
    (byte) 109,
    (byte) 105,
    (byte) 76,
    (byte) 149,
    byte.MaxValue,
    (byte) 62,
    (byte) 51,
    (byte) 215,
    (byte) 189,
    (byte) 220,
    (byte) 240 /*0xF0*/,
    (byte) 165,
    (byte) 83,
    (byte) 27,
    (byte) 139,
    (byte) 35,
    (byte) 64 /*0x40*/,
    (byte) 142,
    (byte) 81,
    (byte) 55,
    (byte) 12,
    (byte) 20,
    (byte) 46,
    (byte) 49,
    (byte) 248,
    (byte) 51,
    (byte) 63 /*0x3F*/,
    (byte) 122,
    (byte) 121,
    (byte) 154,
    (byte) 46,
    (byte) 235,
    (byte) 87,
    (byte) 212,
    (byte) 175,
    byte.MaxValue,
    (byte) 4,
    (byte) 167,
    (byte) 95,
    (byte) 208 /*0xD0*/,
    (byte) 164,
    (byte) 226,
    (byte) 1,
    (byte) 208 /*0xD0*/,
    (byte) 53,
    (byte) 57,
    (byte) 70,
    (byte) 43,
    (byte) 249,
    (byte) 18,
    (byte) 167,
    (byte) 80 /*0x50*/,
    (byte) 178,
    (byte) 236,
    (byte) 202
  };

  internal static string ssp_techacad_19144()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[45];
      byte[] numArray2 = new byte[45]
      {
        (byte) 59,
        (byte) 217,
        (byte) 84,
        (byte) 104,
        (byte) 197,
        (byte) 49,
        (byte) 166,
        (byte) 183,
        (byte) 23,
        (byte) 105,
        (byte) 76,
        (byte) 128 /*0x80*/,
        (byte) 237,
        (byte) 191,
        (byte) 79,
        (byte) 25,
        (byte) 159,
        (byte) 159,
        (byte) 181,
        (byte) 22,
        (byte) 234,
        (byte) 134,
        (byte) 129,
        (byte) 139,
        (byte) 90,
        (byte) 8,
        (byte) 34,
        (byte) 139,
        (byte) 186,
        (byte) 124,
        (byte) 9,
        (byte) 80 /*0x50*/,
        (byte) 5,
        (byte) 187,
        (byte) 160 /*0xA0*/,
        (byte) 106,
        byte.MaxValue,
        (byte) 92,
        (byte) 202,
        (byte) 84,
        (byte) 27,
        (byte) 162,
        (byte) 74,
        (byte) 128 /*0x80*/,
        (byte) 111
      };
      byte[] numArray3 = new byte[45]
      {
        (byte) 95,
        (byte) 49,
        (byte) 143,
        (byte) 195,
        (byte) 35,
        (byte) 238,
        (byte) 201,
        (byte) 234,
        (byte) 172,
        (byte) 92,
        (byte) 232,
        (byte) 122,
        (byte) 73,
        (byte) 251,
        (byte) 230,
        (byte) 190,
        (byte) 192 /*0xC0*/,
        (byte) 40,
        (byte) 140,
        (byte) 102,
        (byte) 77,
        (byte) 94,
        (byte) 69,
        (byte) 248,
        (byte) 219,
        (byte) 74,
        (byte) 201,
        (byte) 60,
        (byte) 247,
        (byte) 200,
        (byte) 148,
        (byte) 161,
        (byte) 143,
        (byte) 50,
        (byte) 97,
        (byte) 243,
        (byte) 228,
        (byte) 170,
        (byte) 156,
        (byte) 199,
        (byte) 176 /*0xB0*/,
        (byte) 182,
        (byte) 16 /*0x10*/,
        (byte) 178,
        (byte) 184
      };
      key.Query(true, 357, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 45);
      for (int index = 0; index < 45; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[38];
      byte[] response = new byte[38];
      Array.Copy((Array) sc_19143.sspq, 0, (Array) numArray4, 0, 38);
      key.Query(true, 357, numArray4, response);
      Array.Copy((Array) sc_19143.sspr, 0, (Array) numArray4, 0, 38);
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
    byte[] numArray5 = new byte[45];
    byte[] numArray6 = new byte[45]
    {
      (byte) 122,
      (byte) 98,
      (byte) 235,
      (byte) 18,
      (byte) 55,
      (byte) 79,
      (byte) 13,
      (byte) 70,
      (byte) 0,
      (byte) 165,
      (byte) 175,
      (byte) 31 /*0x1F*/,
      (byte) 115,
      (byte) 94,
      (byte) 251,
      (byte) 111,
      (byte) 197,
      (byte) 14,
      (byte) 79,
      (byte) 123,
      (byte) 115,
      (byte) 143,
      (byte) 17,
      (byte) 121,
      (byte) 86,
      (byte) 172,
      (byte) 140,
      (byte) 225,
      (byte) 121,
      (byte) 59,
      (byte) 107,
      (byte) 177,
      (byte) 167,
      (byte) 127 /*0x7F*/,
      (byte) 0,
      (byte) 207,
      (byte) 50,
      (byte) 211,
      (byte) 226,
      (byte) 243,
      (byte) 199,
      (byte) 27,
      (byte) 250,
      (byte) 162,
      (byte) 139
    };
    byte[] numArray7 = new byte[45]
    {
      (byte) 230,
      (byte) 80 /*0x50*/,
      (byte) 47,
      (byte) 137,
      (byte) 194,
      (byte) 114,
      (byte) 190,
      (byte) 60,
      (byte) 127 /*0x7F*/,
      (byte) 204,
      (byte) 2,
      (byte) 181,
      (byte) 114,
      (byte) 250,
      (byte) 210,
      (byte) 118,
      (byte) 87,
      (byte) 184,
      (byte) 170,
      (byte) 97,
      (byte) 66,
      (byte) 56,
      (byte) 16 /*0x10*/,
      (byte) 218,
      (byte) 161,
      (byte) 221,
      (byte) 8,
      (byte) 194,
      (byte) 141,
      (byte) 108,
      (byte) 30,
      (byte) 127 /*0x7F*/,
      (byte) 243,
      (byte) 116,
      (byte) 90,
      (byte) 235,
      (byte) 196,
      (byte) 43,
      (byte) 192 /*0xC0*/,
      (byte) 213,
      (byte) 109,
      (byte) 87,
      (byte) 174,
      (byte) 55,
      (byte) 138
    };
    key.Query(true, 357, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 45);
    for (int index = 0; index < 45; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_techacad_19145()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[45];
      byte[] numArray2 = new byte[45]
      {
        (byte) 37,
        (byte) 25,
        (byte) 130,
        (byte) 154,
        (byte) 61,
        (byte) 8,
        (byte) 235,
        (byte) 80 /*0x50*/,
        (byte) 114,
        (byte) 188,
        (byte) 139,
        (byte) 194,
        (byte) 44,
        (byte) 204,
        (byte) 196,
        (byte) 57,
        (byte) 254,
        (byte) 120,
        (byte) 243,
        (byte) 93,
        (byte) 44,
        (byte) 60,
        (byte) 242,
        (byte) 44,
        (byte) 92,
        (byte) 179,
        (byte) 88,
        (byte) 148,
        (byte) 111,
        (byte) 237,
        (byte) 117,
        (byte) 3,
        (byte) 105,
        (byte) 1,
        (byte) 84,
        (byte) 33,
        (byte) 233,
        (byte) 22,
        (byte) 106,
        (byte) 119,
        (byte) 21,
        (byte) 127 /*0x7F*/,
        (byte) 191,
        (byte) 23,
        (byte) 86
      };
      byte[] numArray3 = new byte[45];
      numArray3[0] = (byte) 155;
      numArray3[28] = (byte) 71;
      numArray3[19] = (byte) 39;
      numArray3[41] = (byte) 39;
      numArray3[4] = (byte) 145;
      numArray3[7] = (byte) 109;
      numArray3[21] = (byte) 27;
      numArray3[30] = (byte) 8;
      numArray3[8] = (byte) 208 /*0xD0*/;
      numArray3[9] = (byte) 43;
      numArray3[10] = (byte) 201;
      numArray3[25] = (byte) 137;
      numArray3[12] = (byte) 81;
      numArray3[29] = (byte) 17;
      numArray3[14] = (byte) 210;
      numArray3[15] = (byte) 17;
      numArray3[16 /*0x10*/] = (byte) 235;
      numArray3[17] = (byte) 106;
      numArray3[3] = (byte) 143;
      numArray3[27] = (byte) 11;
      numArray3[20] = (byte) 23;
      numArray3[42] = (byte) 119;
      numArray3[22] = (byte) 52;
      numArray3[23] = (byte) 134;
      numArray3[18] = (byte) 109;
      numArray3[2] = (byte) 179;
      numArray3[26] = (byte) 11;
      numArray3[35] = (byte) 214;
      numArray3[31 /*0x1F*/] = (byte) 34;
      numArray3[24] = (byte) 242;
      numArray3[13] = byte.MaxValue;
      numArray3[11] = (byte) 45;
      numArray3[32 /*0x20*/] = (byte) 85;
      numArray3[33] = (byte) 208 /*0xD0*/;
      numArray3[34] = (byte) 233;
      numArray3[37] = (byte) 94;
      numArray3[36] = (byte) 29;
      numArray3[44] = (byte) 119;
      numArray3[38] = (byte) 167;
      numArray3[5] = (byte) 249;
      numArray3[40] = (byte) 107;
      numArray3[39] = (byte) 122;
      numArray3[6] = (byte) 158;
      numArray3[43] = (byte) 200;
      numArray3[1] = (byte) 104;
      key.Query(true, 357, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 45);
      for (int index = 0; index < 45; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[45];
    byte[] numArray5 = new byte[45];
    numArray5[11] = (byte) 25;
    numArray5[13] = (byte) 91;
    numArray5[24] = (byte) 219;
    numArray5[3] = (byte) 157;
    numArray5[4] = (byte) 58;
    numArray5[5] = (byte) 208 /*0xD0*/;
    numArray5[35] = (byte) 181;
    numArray5[26] = (byte) 58;
    numArray5[28] = (byte) 182;
    numArray5[37] = (byte) 15;
    numArray5[10] = (byte) 147;
    numArray5[6] = (byte) 207;
    numArray5[12] = (byte) 164;
    numArray5[14] = (byte) 55;
    numArray5[15] = (byte) 125;
    numArray5[8] = (byte) 111;
    numArray5[16 /*0x10*/] = (byte) 6;
    numArray5[27] = (byte) 56;
    numArray5[18] = (byte) 75;
    numArray5[29] = (byte) 221;
    numArray5[20] = (byte) 19;
    numArray5[1] = (byte) 199;
    numArray5[22] = (byte) 166;
    numArray5[17] = (byte) 107;
    numArray5[23] = (byte) 247;
    numArray5[43] = (byte) 49;
    numArray5[36] = (byte) 137;
    numArray5[19] = (byte) 127 /*0x7F*/;
    numArray5[30] = (byte) 11;
    numArray5[7] = (byte) 215;
    numArray5[0] = (byte) 224 /*0xE0*/;
    numArray5[31 /*0x1F*/] = (byte) 168;
    numArray5[32 /*0x20*/] = (byte) 187;
    numArray5[33] = (byte) 15;
    numArray5[34] = (byte) 45;
    numArray5[9] = (byte) 215;
    numArray5[41] = (byte) 83;
    numArray5[42] = (byte) 169;
    numArray5[38] = (byte) 66;
    numArray5[39] = (byte) 11;
    numArray5[2] = (byte) 190;
    numArray5[40] = (byte) 6;
    numArray5[25] = (byte) 37;
    numArray5[21] = (byte) 112 /*0x70*/;
    numArray5[44] = (byte) 203;
    byte[] numArray6 = new byte[45]
    {
      (byte) 155,
      (byte) 64 /*0x40*/,
      (byte) 79,
      (byte) 72,
      (byte) 223,
      (byte) 100,
      (byte) 26,
      (byte) 86,
      (byte) 220,
      (byte) 121,
      (byte) 119,
      (byte) 223,
      (byte) 39,
      (byte) 20,
      (byte) 216,
      (byte) 119,
      (byte) 168,
      (byte) 211,
      (byte) 102,
      (byte) 248,
      (byte) 12,
      (byte) 92,
      (byte) 223,
      (byte) 127 /*0x7F*/,
      (byte) 194,
      (byte) 83,
      (byte) 10,
      (byte) 158,
      (byte) 245,
      (byte) 166,
      (byte) 183,
      (byte) 137,
      (byte) 200,
      (byte) 8,
      (byte) 188,
      (byte) 75,
      (byte) 194,
      (byte) 91,
      (byte) 241,
      (byte) 251,
      (byte) 15,
      (byte) 69,
      (byte) 122,
      (byte) 73,
      (byte) 91
    };
    key.Query(true, 357, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 45);
    for (int index = 0; index < 45; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_techacad_19146()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[47];
      byte[] numArray2 = new byte[47];
      numArray2[6] = (byte) 142;
      numArray2[18] = (byte) 154;
      numArray2[43] = (byte) 140;
      numArray2[11] = (byte) 79;
      numArray2[37] = (byte) 176 /*0xB0*/;
      numArray2[44] = (byte) 94;
      numArray2[2] = (byte) 50;
      numArray2[20] = (byte) 172;
      numArray2[8] = (byte) 131;
      numArray2[9] = (byte) 148;
      numArray2[34] = (byte) 203;
      numArray2[1] = (byte) 111;
      numArray2[17] = (byte) 171;
      numArray2[39] = (byte) 75;
      numArray2[14] = (byte) 154;
      numArray2[26] = (byte) 242;
      numArray2[16 /*0x10*/] = (byte) 168;
      numArray2[35] = (byte) 9;
      numArray2[24] = (byte) 251;
      numArray2[19] = (byte) 212;
      numArray2[30] = (byte) 237;
      numArray2[7] = (byte) 38;
      numArray2[22] = (byte) 117;
      numArray2[21] = (byte) 12;
      numArray2[4] = (byte) 102;
      numArray2[23] = (byte) 145;
      numArray2[15] = (byte) 5;
      numArray2[27] = (byte) 182;
      numArray2[28] = (byte) 75;
      numArray2[29] = (byte) 112 /*0x70*/;
      numArray2[33] = (byte) 252;
      numArray2[31 /*0x1F*/] = (byte) 49;
      numArray2[32 /*0x20*/] = (byte) 171;
      numArray2[12] = (byte) 215;
      numArray2[45] = (byte) 71;
      numArray2[13] = (byte) 110;
      numArray2[36] = (byte) 141;
      numArray2[0] = (byte) 117;
      numArray2[38] = (byte) 14;
      numArray2[42] = (byte) 5;
      numArray2[40] = (byte) 95;
      numArray2[41] = (byte) 66;
      numArray2[25] = (byte) 191;
      numArray2[10] = (byte) 49;
      numArray2[5] = (byte) 224 /*0xE0*/;
      numArray2[3] = (byte) 22;
      numArray2[46] = (byte) 52;
      byte[] numArray3 = new byte[47]
      {
        (byte) 104,
        (byte) 191,
        (byte) 7,
        (byte) 196,
        (byte) 49,
        (byte) 50,
        (byte) 60,
        (byte) 37,
        (byte) 141,
        (byte) 173,
        (byte) 46,
        (byte) 189,
        (byte) 193,
        (byte) 10,
        (byte) 42,
        (byte) 205,
        (byte) 17,
        (byte) 18,
        (byte) 1,
        (byte) 97,
        (byte) 101,
        (byte) 86,
        (byte) 157,
        (byte) 176 /*0xB0*/,
        (byte) 11,
        (byte) 89,
        (byte) 122,
        (byte) 229,
        (byte) 211,
        (byte) 160 /*0xA0*/,
        (byte) 209,
        (byte) 7,
        (byte) 244,
        (byte) 40,
        (byte) 105,
        (byte) 92,
        (byte) 194,
        (byte) 33,
        (byte) 144 /*0x90*/,
        (byte) 167,
        (byte) 244,
        (byte) 118,
        (byte) 140,
        (byte) 222,
        (byte) 252,
        (byte) 158,
        (byte) 193
      };
      key.Query(true, 357, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 47);
      for (int index = 0; index < 47; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[47];
    byte[] numArray5 = new byte[47]
    {
      (byte) 242,
      (byte) 229,
      (byte) 94,
      (byte) 163,
      (byte) 89,
      (byte) 230,
      (byte) 123,
      (byte) 203,
      (byte) 29,
      (byte) 77,
      (byte) 216,
      (byte) 4,
      (byte) 23,
      (byte) 97,
      (byte) 17,
      (byte) 109,
      (byte) 102,
      (byte) 27,
      (byte) 134,
      (byte) 47,
      (byte) 212,
      (byte) 183,
      (byte) 163,
      (byte) 246,
      (byte) 113,
      (byte) 95,
      (byte) 64 /*0x40*/,
      (byte) 130,
      (byte) 140,
      (byte) 97,
      (byte) 52,
      (byte) 241,
      (byte) 237,
      (byte) 1,
      (byte) 20,
      (byte) 3,
      (byte) 213,
      (byte) 17,
      (byte) 49,
      (byte) 130,
      (byte) 45,
      (byte) 59,
      (byte) 62,
      (byte) 181,
      (byte) 49,
      (byte) 46,
      (byte) 99
    };
    byte[] numArray6 = new byte[47]
    {
      (byte) 114,
      (byte) 27,
      (byte) 161,
      (byte) 143,
      (byte) 70,
      (byte) 89,
      (byte) 143,
      (byte) 174,
      (byte) 237,
      (byte) 78,
      (byte) 143,
      (byte) 202,
      (byte) 74,
      (byte) 111,
      (byte) 143,
      (byte) 87,
      (byte) 140,
      (byte) 128 /*0x80*/,
      (byte) 196,
      (byte) 34,
      (byte) 84,
      (byte) 20,
      (byte) 3,
      (byte) 101,
      (byte) 90,
      (byte) 208 /*0xD0*/,
      (byte) 90,
      (byte) 135,
      (byte) 240 /*0xF0*/,
      (byte) 249,
      (byte) 56,
      (byte) 175,
      (byte) 9,
      (byte) 49,
      (byte) 196,
      (byte) 237,
      (byte) 219,
      (byte) 0,
      (byte) 177,
      (byte) 41,
      (byte) 162,
      (byte) 67,
      (byte) 122,
      (byte) 241,
      (byte) 16 /*0x10*/,
      (byte) 106,
      (byte) 98
    };
    key.Query(true, 357, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 47);
    for (int index = 0; index < 47; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_techacad_19147()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[44];
      byte[] numArray2 = new byte[44]
      {
        (byte) 77,
        (byte) 214,
        (byte) 88,
        (byte) 147,
        (byte) 59,
        (byte) 65,
        (byte) 104,
        (byte) 49,
        (byte) 249,
        (byte) 165,
        (byte) 249,
        (byte) 62,
        (byte) 187,
        (byte) 25,
        (byte) 230,
        (byte) 23,
        (byte) 22,
        (byte) 77,
        (byte) 13,
        (byte) 152,
        (byte) 85,
        (byte) 233,
        (byte) 3,
        (byte) 1,
        (byte) 152,
        (byte) 121,
        (byte) 238,
        (byte) 196,
        (byte) 66,
        (byte) 252,
        (byte) 63 /*0x3F*/,
        (byte) 133,
        (byte) 13,
        (byte) 50,
        (byte) 231,
        (byte) 26,
        (byte) 149,
        (byte) 110,
        (byte) 216,
        (byte) 194,
        (byte) 109,
        (byte) 223,
        (byte) 55,
        (byte) 221
      };
      byte[] numArray3 = new byte[44];
      numArray3[25] = (byte) 60;
      numArray3[2] = (byte) 47;
      numArray3[23] = (byte) 231;
      numArray3[35] = (byte) 153;
      numArray3[16 /*0x10*/] = (byte) 62;
      numArray3[14] = (byte) 110;
      numArray3[6] = (byte) 230;
      numArray3[7] = (byte) 100;
      numArray3[8] = (byte) 135;
      numArray3[15] = (byte) 230;
      numArray3[10] = (byte) 36;
      numArray3[24] = (byte) 29;
      numArray3[9] = (byte) 104;
      numArray3[38] = (byte) 187;
      numArray3[27] = (byte) 16 /*0x10*/;
      numArray3[1] = (byte) 65;
      numArray3[37] = (byte) 142;
      numArray3[17] = (byte) 101;
      numArray3[18] = (byte) 116;
      numArray3[19] = (byte) 27;
      numArray3[20] = (byte) 120;
      numArray3[21] = (byte) 243;
      numArray3[0] = (byte) 180;
      numArray3[22] = (byte) 132;
      numArray3[3] = (byte) 247;
      numArray3[11] = (byte) 240 /*0xF0*/;
      numArray3[26] = (byte) 16 /*0x10*/;
      numArray3[41] = (byte) 44;
      numArray3[28] = (byte) 47;
      numArray3[29] = (byte) 236;
      numArray3[12] = (byte) 139;
      numArray3[31 /*0x1F*/] = (byte) 69;
      numArray3[32 /*0x20*/] = (byte) 209;
      numArray3[33] = (byte) 159;
      numArray3[34] = (byte) 111;
      numArray3[40] = (byte) 141;
      numArray3[36] = (byte) 182;
      numArray3[13] = (byte) 193;
      numArray3[30] = (byte) 21;
      numArray3[39] = (byte) 169;
      numArray3[5] = (byte) 78;
      numArray3[4] = (byte) 30;
      numArray3[42] = (byte) 209;
      numArray3[43] = (byte) 100;
      key.Query(true, 357, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 44);
      for (int index = 0; index < 44; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[38];
      byte[] response = new byte[38];
      Array.Copy((Array) sc_19143.sspq, 38, (Array) numArray4, 0, 38);
      key.Query(true, 357, numArray4, response);
      Array.Copy((Array) sc_19143.sspr, 38, (Array) numArray4, 0, 38);
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
    byte[] numArray5 = new byte[44];
    byte[] numArray6 = new byte[44]
    {
      (byte) 133,
      (byte) 119,
      (byte) 175,
      (byte) 13,
      (byte) 207,
      (byte) 251,
      (byte) 231,
      (byte) 252,
      (byte) 149,
      (byte) 9,
      (byte) 76,
      (byte) 241,
      (byte) 109,
      (byte) 244,
      (byte) 210,
      (byte) 155,
      (byte) 181,
      (byte) 19,
      (byte) 74,
      (byte) 109,
      (byte) 161,
      (byte) 44,
      (byte) 236,
      (byte) 156,
      (byte) 226,
      (byte) 194,
      (byte) 202,
      (byte) 187,
      (byte) 249,
      (byte) 82,
      (byte) 112 /*0x70*/,
      (byte) 224 /*0xE0*/,
      (byte) 199,
      (byte) 88,
      (byte) 222,
      (byte) 57,
      (byte) 252,
      (byte) 124,
      (byte) 143,
      (byte) 208 /*0xD0*/,
      (byte) 26,
      (byte) 44,
      (byte) 46,
      (byte) 68
    };
    byte[] numArray7 = new byte[44]
    {
      (byte) 178,
      (byte) 161,
      (byte) 73,
      (byte) 35,
      (byte) 228,
      (byte) 62,
      (byte) 146,
      (byte) 82,
      (byte) 233,
      (byte) 224 /*0xE0*/,
      (byte) 86,
      (byte) 197,
      (byte) 7,
      (byte) 231,
      (byte) 209,
      (byte) 8,
      (byte) 199,
      (byte) 72,
      (byte) 77,
      (byte) 190,
      (byte) 6,
      (byte) 1,
      (byte) 18,
      (byte) 56,
      (byte) 88,
      (byte) 228,
      (byte) 176 /*0xB0*/,
      (byte) 212,
      (byte) 129,
      (byte) 163,
      (byte) 144 /*0x90*/,
      (byte) 62,
      (byte) 128 /*0x80*/,
      (byte) 146,
      (byte) 213,
      (byte) 188,
      (byte) 117,
      (byte) 63 /*0x3F*/,
      (byte) 221,
      (byte) 48 /*0x30*/,
      (byte) 78,
      (byte) 160 /*0xA0*/,
      (byte) 190,
      (byte) 129
    };
    key.Query(true, 357, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 44);
    for (int index = 0; index < 44; ++index)
      numArray5[index] ^= numArray7[index];
    byte[] numArray8 = new byte[41];
    byte[] response1 = new byte[41];
    Array.Copy((Array) sc_19143.sspq, 76, (Array) numArray8, 0, 41);
    key.Query(true, 357, numArray8, response1);
    Array.Copy((Array) sc_19143.sspr, 76, (Array) numArray8, 0, 41);
    for (int index = 0; index < numArray8.Length; ++index)
    {
      if ((int) numArray8[index] != (int) response1[index])
      {
        key.TagValue = (int) response1[index];
        break;
      }
    }
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_techacad_19148()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[45];
      byte[] numArray2 = new byte[45];
      numArray2[8] = (byte) 170;
      numArray2[16 /*0x10*/] = (byte) 156;
      numArray2[2] = (byte) 81;
      numArray2[3] = (byte) 130;
      numArray2[4] = (byte) 158;
      numArray2[40] = (byte) 74;
      numArray2[6] = (byte) 41;
      numArray2[30] = (byte) 159;
      numArray2[1] = (byte) 233;
      numArray2[9] = (byte) 34;
      numArray2[10] = (byte) 193;
      numArray2[43] = (byte) 235;
      numArray2[24] = (byte) 228;
      numArray2[5] = (byte) 238;
      numArray2[14] = (byte) 73;
      numArray2[25] = (byte) 161;
      numArray2[37] = (byte) 1;
      numArray2[17] = (byte) 154;
      numArray2[18] = (byte) 27;
      numArray2[35] = (byte) 182;
      numArray2[22] = (byte) 4;
      numArray2[20] = (byte) 92;
      numArray2[12] = (byte) 36;
      numArray2[23] = (byte) 53;
      numArray2[21] = (byte) 227;
      numArray2[15] = (byte) 84;
      numArray2[26] = (byte) 76;
      numArray2[27] = (byte) 241;
      numArray2[28] = (byte) 127 /*0x7F*/;
      numArray2[29] = (byte) 73;
      numArray2[33] = (byte) 145;
      numArray2[31 /*0x1F*/] = (byte) 210;
      numArray2[19] = (byte) 76;
      numArray2[32 /*0x20*/] = (byte) 126;
      numArray2[34] = (byte) 234;
      numArray2[7] = (byte) 235;
      numArray2[44] = (byte) 52;
      numArray2[11] = (byte) 133;
      numArray2[38] = (byte) 219;
      numArray2[39] = (byte) 101;
      numArray2[41] = (byte) 18;
      numArray2[13] = (byte) 67;
      numArray2[42] = (byte) 253;
      numArray2[36] = (byte) 232;
      numArray2[0] = (byte) 174;
      byte[] numArray3 = new byte[45];
      numArray3[15] = (byte) 27;
      numArray3[35] = (byte) 228;
      numArray3[31 /*0x1F*/] = (byte) 57;
      numArray3[44] = (byte) 56;
      numArray3[4] = (byte) 240 /*0xF0*/;
      numArray3[5] = (byte) 223;
      numArray3[6] = (byte) 41;
      numArray3[9] = (byte) 130;
      numArray3[8] = (byte) 136;
      numArray3[23] = (byte) 22;
      numArray3[10] = (byte) 12;
      numArray3[11] = (byte) 186;
      numArray3[12] = (byte) 209;
      numArray3[1] = (byte) 123;
      numArray3[26] = (byte) 176 /*0xB0*/;
      numArray3[28] = (byte) 160 /*0xA0*/;
      numArray3[0] = (byte) 218;
      numArray3[17] = (byte) 18;
      numArray3[18] = (byte) 120;
      numArray3[20] = (byte) 156;
      numArray3[14] = (byte) 228;
      numArray3[22] = (byte) 106;
      numArray3[3] = (byte) 233;
      numArray3[21] = (byte) 27;
      numArray3[27] = (byte) 27;
      numArray3[25] = (byte) 138;
      numArray3[16 /*0x10*/] = (byte) 57;
      numArray3[39] = (byte) 215;
      numArray3[32 /*0x20*/] = (byte) 210;
      numArray3[29] = (byte) 23;
      numArray3[30] = (byte) 167;
      numArray3[24] = (byte) 202;
      numArray3[2] = (byte) 78;
      numArray3[19] = (byte) 240 /*0xF0*/;
      numArray3[13] = (byte) 28;
      numArray3[33] = (byte) 34;
      numArray3[36] = (byte) 162;
      numArray3[37] = (byte) 181;
      numArray3[38] = (byte) 217;
      numArray3[7] = (byte) 25;
      numArray3[40] = (byte) 84;
      numArray3[41] = (byte) 156;
      numArray3[42] = (byte) 157;
      numArray3[43] = (byte) 29;
      numArray3[34] = (byte) 105;
      key.Query(true, 357, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 45);
      for (int index = 0; index < 45; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[45];
    byte[] numArray5 = new byte[45]
    {
      (byte) 11,
      (byte) 123,
      (byte) 252,
      (byte) 15,
      (byte) 233,
      (byte) 138,
      (byte) 56,
      (byte) 6,
      (byte) 178,
      (byte) 8,
      (byte) 21,
      (byte) 209,
      (byte) 3,
      (byte) 74,
      (byte) 206,
      (byte) 84,
      (byte) 235,
      (byte) 201,
      (byte) 176 /*0xB0*/,
      (byte) 7,
      (byte) 114,
      (byte) 100,
      (byte) 227,
      (byte) 144 /*0x90*/,
      (byte) 147,
      (byte) 90,
      (byte) 59,
      (byte) 155,
      (byte) 26,
      (byte) 93,
      (byte) 182,
      (byte) 108,
      (byte) 132,
      (byte) 26,
      (byte) 80 /*0x50*/,
      (byte) 81,
      (byte) 24,
      (byte) 73,
      (byte) 233,
      (byte) 157,
      (byte) 2,
      (byte) 187,
      (byte) 236,
      (byte) 45,
      (byte) 204
    };
    byte[] numArray6 = new byte[45]
    {
      (byte) 183,
      (byte) 105,
      (byte) 147,
      (byte) 46,
      (byte) 39,
      (byte) 245,
      (byte) 57,
      (byte) 210,
      (byte) 252,
      (byte) 132,
      (byte) 44,
      (byte) 74,
      (byte) 74,
      (byte) 106,
      (byte) 12,
      (byte) 12,
      (byte) 17,
      (byte) 39,
      (byte) 136,
      (byte) 133,
      (byte) 54,
      (byte) 61,
      (byte) 183,
      (byte) 206,
      (byte) 51,
      (byte) 101,
      (byte) 118,
      (byte) 223,
      (byte) 193,
      (byte) 38,
      (byte) 158,
      (byte) 26,
      (byte) 237,
      (byte) 247,
      (byte) 97,
      (byte) 146,
      (byte) 18,
      (byte) 154,
      (byte) 140,
      (byte) 56,
      (byte) 241,
      (byte) 151,
      (byte) 135,
      (byte) 158,
      (byte) 17
    };
    key.Query(true, 357, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 45);
    for (int index = 0; index < 45; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[10];
    byte[] response = new byte[10];
    Array.Copy((Array) sc_19143.sspq, 117, (Array) numArray7, 0, 10);
    key.Query(true, 357, numArray7, response);
    Array.Copy((Array) sc_19143.sspr, 117, (Array) numArray7, 0, 10);
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
