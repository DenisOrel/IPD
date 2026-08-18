
// Type: ImSSP.sc_2512
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_2512
{
  internal static string ssp_imclient_2513()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[43];
      byte[] numArray2 = new byte[43]
      {
        (byte) 51,
        (byte) 125,
        (byte) 75,
        (byte) 139,
        (byte) 168,
        (byte) 48 /*0x30*/,
        (byte) 47,
        (byte) 26,
        (byte) 69,
        (byte) 234,
        (byte) 144 /*0x90*/,
        (byte) 52,
        (byte) 188,
        (byte) 100,
        (byte) 42,
        (byte) 115,
        (byte) 141,
        (byte) 211,
        (byte) 82,
        (byte) 223,
        (byte) 30,
        (byte) 124,
        (byte) 1,
        (byte) 243,
        (byte) 171,
        (byte) 144 /*0x90*/,
        (byte) 210,
        (byte) 8,
        (byte) 21,
        (byte) 46,
        (byte) 105,
        (byte) 152,
        (byte) 17,
        (byte) 139,
        (byte) 67,
        (byte) 58,
        (byte) 69,
        (byte) 89,
        (byte) 177,
        (byte) 7,
        (byte) 203,
        (byte) 192 /*0xC0*/,
        (byte) 18
      };
      byte[] numArray3 = new byte[43];
      numArray3[22] = (byte) 224 /*0xE0*/;
      numArray3[37] = (byte) 158;
      numArray3[36] = (byte) 119;
      numArray3[0] = (byte) 117;
      numArray3[28] = (byte) 125;
      numArray3[25] = (byte) 118;
      numArray3[6] = (byte) 236;
      numArray3[7] = (byte) 21;
      numArray3[40] = (byte) 30;
      numArray3[32 /*0x20*/] = (byte) 240 /*0xF0*/;
      numArray3[23] = (byte) 243;
      numArray3[11] = (byte) 87;
      numArray3[12] = (byte) 33;
      numArray3[13] = (byte) 36;
      numArray3[2] = (byte) 202;
      numArray3[15] = (byte) 206;
      numArray3[14] = (byte) 249;
      numArray3[17] = (byte) 247;
      numArray3[3] = (byte) 181;
      numArray3[19] = (byte) 168;
      numArray3[20] = (byte) 222;
      numArray3[21] = (byte) 49;
      numArray3[4] = (byte) 54;
      numArray3[9] = (byte) 121;
      numArray3[24] = (byte) 23;
      numArray3[39] = (byte) 174;
      numArray3[8] = (byte) 250;
      numArray3[26] = (byte) 46;
      numArray3[1] = (byte) 88;
      numArray3[29] = (byte) 212;
      numArray3[30] = (byte) 121;
      numArray3[16 /*0x10*/] = (byte) 157;
      numArray3[5] = (byte) 89;
      numArray3[33] = (byte) 40;
      numArray3[34] = (byte) 247;
      numArray3[35] = (byte) 171;
      numArray3[31 /*0x1F*/] = (byte) 205;
      numArray3[10] = (byte) 6;
      numArray3[38] = (byte) 26;
      numArray3[18] = (byte) 225;
      numArray3[27] = (byte) 70;
      numArray3[41] = (byte) 196;
      numArray3[42] = (byte) 181;
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 43);
      for (int index = 0; index < 43; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[43];
    byte[] numArray5 = new byte[43];
    numArray5[7] = (byte) 187;
    numArray5[1] = (byte) 189;
    numArray5[2] = (byte) 236;
    numArray5[37] = (byte) 185;
    numArray5[40] = (byte) 110;
    numArray5[14] = (byte) 41;
    numArray5[29] = (byte) 111;
    numArray5[10] = (byte) 134;
    numArray5[8] = (byte) 252;
    numArray5[9] = (byte) 249;
    numArray5[32 /*0x20*/] = (byte) 212;
    numArray5[11] = (byte) 199;
    numArray5[12] = (byte) 151;
    numArray5[19] = (byte) 175;
    numArray5[30] = (byte) 211;
    numArray5[15] = (byte) 235;
    numArray5[17] = (byte) 153;
    numArray5[13] = (byte) 210;
    numArray5[24] = (byte) 30;
    numArray5[23] = (byte) 226;
    numArray5[41] = (byte) 162;
    numArray5[6] = (byte) 165;
    numArray5[22] = (byte) 245;
    numArray5[18] = (byte) 11;
    numArray5[21] = (byte) 78;
    numArray5[25] = (byte) 80 /*0x50*/;
    numArray5[26] = (byte) 128 /*0x80*/;
    numArray5[27] = (byte) 55;
    numArray5[28] = (byte) 10;
    numArray5[42] = (byte) 109;
    numArray5[3] = (byte) 121;
    numArray5[4] = (byte) 152;
    numArray5[31 /*0x1F*/] = (byte) 181;
    numArray5[33] = (byte) 14;
    numArray5[34] = (byte) 149;
    numArray5[16 /*0x10*/] = (byte) 84;
    numArray5[36] = (byte) 65;
    numArray5[20] = (byte) 11;
    numArray5[38] = (byte) 138;
    numArray5[5] = (byte) 169;
    numArray5[0] = (byte) 139;
    numArray5[35] = (byte) 50;
    numArray5[39] = (byte) 161;
    byte[] numArray6 = new byte[43]
    {
      (byte) 204,
      (byte) 97,
      (byte) 64 /*0x40*/,
      (byte) 96 /*0x60*/,
      (byte) 243,
      (byte) 144 /*0x90*/,
      (byte) 136,
      (byte) 148,
      (byte) 175,
      (byte) 118,
      (byte) 98,
      (byte) 223,
      (byte) 225,
      (byte) 61,
      (byte) 147,
      (byte) 60,
      (byte) 95,
      (byte) 116,
      (byte) 172,
      (byte) 120,
      (byte) 70,
      (byte) 241,
      (byte) 9,
      (byte) 40,
      (byte) 200,
      (byte) 33,
      (byte) 20,
      (byte) 180,
      (byte) 123,
      (byte) 127 /*0x7F*/,
      (byte) 11,
      (byte) 102,
      (byte) 205,
      (byte) 94,
      (byte) 15,
      (byte) 103,
      (byte) 183,
      (byte) 173,
      (byte) 104,
      (byte) 144 /*0x90*/,
      (byte) 62,
      (byte) 176 /*0xB0*/,
      (byte) 81
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 43);
    for (int index = 0; index < 43; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_imclient_2514()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[43];
      byte[] numArray2 = new byte[43]
      {
        (byte) 106,
        (byte) 54,
        (byte) 94,
        (byte) 66,
        (byte) 120,
        (byte) 0,
        (byte) 241,
        (byte) 225,
        (byte) 104,
        (byte) 31 /*0x1F*/,
        (byte) 6,
        (byte) 167,
        (byte) 205,
        (byte) 206,
        (byte) 100,
        (byte) 182,
        (byte) 5,
        (byte) 203,
        (byte) 94,
        (byte) 175,
        (byte) 93,
        (byte) 96 /*0x60*/,
        (byte) 23,
        (byte) 54,
        (byte) 39,
        (byte) 180,
        (byte) 251,
        (byte) 182,
        (byte) 110,
        (byte) 102,
        (byte) 80 /*0x50*/,
        (byte) 189,
        (byte) 146,
        (byte) 229,
        (byte) 140,
        (byte) 115,
        (byte) 60,
        (byte) 204,
        (byte) 241,
        (byte) 64 /*0x40*/,
        (byte) 118,
        byte.MaxValue,
        byte.MaxValue
      };
      byte[] numArray3 = new byte[43]
      {
        (byte) 155,
        (byte) 105,
        (byte) 30,
        (byte) 142,
        (byte) 238,
        (byte) 184,
        (byte) 125,
        (byte) 254,
        (byte) 34,
        (byte) 86,
        (byte) 188,
        (byte) 201,
        (byte) 188,
        (byte) 17,
        (byte) 16 /*0x10*/,
        (byte) 246,
        (byte) 167,
        (byte) 226,
        (byte) 114,
        (byte) 241,
        (byte) 202,
        (byte) 214,
        (byte) 98,
        (byte) 121,
        (byte) 93,
        (byte) 92,
        (byte) 93,
        (byte) 243,
        (byte) 120,
        (byte) 186,
        (byte) 85,
        (byte) 32 /*0x20*/,
        (byte) 205,
        (byte) 115,
        (byte) 83,
        (byte) 94,
        (byte) 129,
        (byte) 216,
        (byte) 153,
        (byte) 65,
        (byte) 97,
        (byte) 159,
        (byte) 232
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 43);
      for (int index = 0; index < 43; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[43];
    byte[] numArray5 = new byte[43];
    numArray5[30] = (byte) 96 /*0x60*/;
    numArray5[1] = (byte) 164;
    numArray5[5] = (byte) 179;
    numArray5[3] = (byte) 103;
    numArray5[4] = (byte) 185;
    numArray5[10] = (byte) 31 /*0x1F*/;
    numArray5[16 /*0x10*/] = (byte) 115;
    numArray5[7] = (byte) 71;
    numArray5[29] = (byte) 94;
    numArray5[9] = (byte) 10;
    numArray5[28] = (byte) 56;
    numArray5[23] = (byte) 240 /*0xF0*/;
    numArray5[13] = (byte) 195;
    numArray5[15] = (byte) 59;
    numArray5[14] = (byte) 4;
    numArray5[42] = (byte) 234;
    numArray5[41] = (byte) 74;
    numArray5[17] = (byte) 1;
    numArray5[26] = (byte) 218;
    numArray5[2] = (byte) 150;
    numArray5[12] = (byte) 27;
    numArray5[21] = (byte) 232;
    numArray5[22] = (byte) 65;
    numArray5[24] = (byte) 122;
    numArray5[20] = (byte) 20;
    numArray5[25] = (byte) 56;
    numArray5[27] = (byte) 29;
    numArray5[8] = (byte) 250;
    numArray5[18] = (byte) 206;
    numArray5[32 /*0x20*/] = (byte) 24;
    numArray5[6] = (byte) 215;
    numArray5[19] = (byte) 73;
    numArray5[0] = (byte) 166;
    numArray5[33] = (byte) 173;
    numArray5[34] = (byte) 13;
    numArray5[35] = (byte) 87;
    numArray5[36] = (byte) 31 /*0x1F*/;
    numArray5[37] = (byte) 7;
    numArray5[11] = (byte) 40;
    numArray5[39] = (byte) 156;
    numArray5[40] = (byte) 230;
    numArray5[38] = (byte) 173;
    numArray5[31 /*0x1F*/] = (byte) 67;
    byte[] numArray6 = new byte[43];
    numArray6[24] = (byte) 199;
    numArray6[2] = (byte) 23;
    numArray6[21] = (byte) 120;
    numArray6[3] = (byte) 142;
    numArray6[12] = (byte) 209;
    numArray6[8] = (byte) 62;
    numArray6[6] = (byte) 81;
    numArray6[7] = (byte) 65;
    numArray6[29] = (byte) 130;
    numArray6[39] = (byte) 148;
    numArray6[14] = (byte) 98;
    numArray6[11] = (byte) 67;
    numArray6[1] = (byte) 201;
    numArray6[13] = (byte) 78;
    numArray6[36] = (byte) 19;
    numArray6[15] = (byte) 204;
    numArray6[16 /*0x10*/] = (byte) 85;
    numArray6[35] = (byte) 198;
    numArray6[18] = (byte) 94;
    numArray6[19] = (byte) 96 /*0x60*/;
    numArray6[17] = (byte) 243;
    numArray6[27] = (byte) 174;
    numArray6[30] = (byte) 166;
    numArray6[28] = (byte) 75;
    numArray6[34] = (byte) 250;
    numArray6[25] = (byte) 98;
    numArray6[26] = (byte) 21;
    numArray6[4] = (byte) 32 /*0x20*/;
    numArray6[5] = (byte) 236;
    numArray6[33] = (byte) 242;
    numArray6[0] = (byte) 207;
    numArray6[23] = (byte) 151;
    numArray6[32 /*0x20*/] = (byte) 121;
    numArray6[20] = (byte) 253;
    numArray6[37] = (byte) 40;
    numArray6[31 /*0x1F*/] = (byte) 13;
    numArray6[38] = (byte) 216;
    numArray6[10] = (byte) 82;
    numArray6[41] = (byte) 23;
    numArray6[9] = (byte) 75;
    numArray6[40] = (byte) 144 /*0x90*/;
    numArray6[42] = (byte) 191;
    numArray6[22] = (byte) 134;
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 43);
    for (int index = 0; index < 43; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_imclient_2515()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[43];
      byte[] numArray2 = new byte[43]
      {
        (byte) 36,
        (byte) 99,
        (byte) 68,
        (byte) 50,
        (byte) 84,
        (byte) 220,
        (byte) 178,
        (byte) 182,
        (byte) 105,
        (byte) 208 /*0xD0*/,
        (byte) 175,
        (byte) 142,
        (byte) 81,
        (byte) 111,
        (byte) 149,
        (byte) 49,
        (byte) 149,
        (byte) 85,
        (byte) 39,
        (byte) 131,
        (byte) 144 /*0x90*/,
        (byte) 217,
        (byte) 54,
        (byte) 31 /*0x1F*/,
        (byte) 18,
        (byte) 213,
        (byte) 125,
        (byte) 206,
        (byte) 130,
        (byte) 101,
        (byte) 60,
        (byte) 10,
        (byte) 17,
        (byte) 190,
        (byte) 229,
        (byte) 34,
        (byte) 181,
        (byte) 251,
        (byte) 131,
        (byte) 54,
        (byte) 23,
        (byte) 145,
        (byte) 53
      };
      byte[] numArray3 = new byte[43]
      {
        (byte) 18,
        (byte) 226,
        (byte) 202,
        (byte) 55,
        (byte) 175,
        (byte) 123,
        (byte) 153,
        (byte) 187,
        (byte) 226,
        (byte) 68,
        (byte) 214,
        (byte) 76,
        (byte) 231,
        (byte) 46,
        (byte) 254,
        (byte) 235,
        (byte) 206,
        (byte) 221,
        (byte) 205,
        (byte) 208 /*0xD0*/,
        (byte) 170,
        (byte) 12,
        (byte) 238,
        (byte) 50,
        (byte) 13,
        (byte) 15,
        (byte) 12,
        (byte) 246,
        (byte) 157,
        (byte) 247,
        (byte) 114,
        (byte) 95,
        (byte) 212,
        (byte) 56,
        (byte) 209,
        (byte) 152,
        (byte) 179,
        (byte) 251,
        (byte) 229,
        (byte) 234,
        (byte) 135,
        (byte) 48 /*0x30*/,
        (byte) 187
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 43);
      for (int index = 0; index < 43; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[43];
    byte[] numArray5 = new byte[43]
    {
      (byte) 103,
      (byte) 50,
      (byte) 188,
      (byte) 246,
      (byte) 217,
      (byte) 224 /*0xE0*/,
      (byte) 103,
      (byte) 43,
      (byte) 85,
      (byte) 74,
      (byte) 45,
      (byte) 37,
      (byte) 32 /*0x20*/,
      (byte) 126,
      (byte) 94,
      (byte) 228,
      (byte) 52,
      (byte) 3,
      (byte) 147,
      (byte) 165,
      (byte) 124,
      (byte) 147,
      (byte) 137,
      (byte) 114,
      (byte) 124,
      (byte) 154,
      (byte) 210,
      (byte) 241,
      (byte) 107,
      byte.MaxValue,
      (byte) 164,
      (byte) 56,
      (byte) 171,
      (byte) 59,
      (byte) 110,
      (byte) 75,
      (byte) 17,
      (byte) 139,
      (byte) 140,
      (byte) 92,
      (byte) 162,
      (byte) 230,
      (byte) 104
    };
    byte[] numArray6 = new byte[43]
    {
      (byte) 123,
      (byte) 126,
      (byte) 218,
      (byte) 246,
      (byte) 180,
      (byte) 61,
      (byte) 156,
      (byte) 246,
      (byte) 93,
      (byte) 249,
      (byte) 21,
      (byte) 183,
      (byte) 65,
      (byte) 135,
      (byte) 63 /*0x3F*/,
      (byte) 142,
      (byte) 10,
      (byte) 187,
      (byte) 227,
      (byte) 146,
      (byte) 99,
      (byte) 126,
      (byte) 199,
      (byte) 93,
      (byte) 77,
      (byte) 107,
      (byte) 40,
      (byte) 50,
      (byte) 173,
      (byte) 56,
      (byte) 200,
      (byte) 184,
      (byte) 144 /*0x90*/,
      (byte) 210,
      (byte) 223,
      (byte) 89,
      (byte) 39,
      (byte) 136,
      (byte) 76,
      (byte) 46,
      (byte) 56,
      (byte) 161,
      (byte) 243
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 43);
    for (int index = 0; index < 43; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_imclient_2516()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[43];
      byte[] numArray2 = new byte[43]
      {
        (byte) 4,
        (byte) 215,
        (byte) 51,
        (byte) 92,
        (byte) 49,
        (byte) 105,
        (byte) 206,
        (byte) 90,
        (byte) 212,
        (byte) 168,
        (byte) 73,
        (byte) 121,
        (byte) 63 /*0x3F*/,
        (byte) 13,
        (byte) 118,
        (byte) 191,
        (byte) 85,
        (byte) 166,
        (byte) 244,
        (byte) 122,
        (byte) 228,
        (byte) 15,
        (byte) 19,
        (byte) 210,
        (byte) 168,
        (byte) 118,
        (byte) 54,
        (byte) 3,
        (byte) 150,
        (byte) 42,
        (byte) 238,
        (byte) 125,
        (byte) 236,
        (byte) 190,
        (byte) 205,
        (byte) 25,
        (byte) 55,
        (byte) 163,
        (byte) 114,
        (byte) 206,
        (byte) 220,
        (byte) 224 /*0xE0*/,
        (byte) 154
      };
      byte[] numArray3 = new byte[43];
      numArray3[32 /*0x20*/] = (byte) 85;
      numArray3[1] = (byte) 180;
      numArray3[9] = (byte) 71;
      numArray3[6] = (byte) 131;
      numArray3[41] = (byte) 40;
      numArray3[5] = (byte) 174;
      numArray3[34] = (byte) 128 /*0x80*/;
      numArray3[18] = (byte) 13;
      numArray3[8] = (byte) 7;
      numArray3[28] = (byte) 191;
      numArray3[10] = (byte) 204;
      numArray3[7] = (byte) 72;
      numArray3[39] = (byte) 227;
      numArray3[24] = (byte) 176 /*0xB0*/;
      numArray3[14] = (byte) 83;
      numArray3[11] = (byte) 59;
      numArray3[4] = (byte) 83;
      numArray3[17] = (byte) 99;
      numArray3[0] = (byte) 117;
      numArray3[19] = (byte) 13;
      numArray3[33] = (byte) 51;
      numArray3[12] = (byte) 105;
      numArray3[22] = (byte) 73;
      numArray3[23] = (byte) 221;
      numArray3[21] = (byte) 174;
      numArray3[26] = (byte) 231;
      numArray3[25] = (byte) 121;
      numArray3[15] = (byte) 253;
      numArray3[30] = (byte) 20;
      numArray3[29] = (byte) 248;
      numArray3[16 /*0x10*/] = (byte) 177;
      numArray3[13] = (byte) 155;
      numArray3[36] = (byte) 198;
      numArray3[38] = (byte) 95;
      numArray3[27] = (byte) 99;
      numArray3[35] = (byte) 244;
      numArray3[31 /*0x1F*/] = (byte) 223;
      numArray3[37] = (byte) 129;
      numArray3[3] = (byte) 95;
      numArray3[20] = (byte) 75;
      numArray3[40] = (byte) 222;
      numArray3[2] = (byte) 13;
      numArray3[42] = (byte) 163;
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 43);
      for (int index = 0; index < 43; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[43];
    byte[] numArray5 = new byte[43];
    numArray5[3] = (byte) 239;
    numArray5[42] = (byte) 132;
    numArray5[2] = (byte) 214;
    numArray5[40] = (byte) 139;
    numArray5[4] = (byte) 183;
    numArray5[16 /*0x10*/] = (byte) 97;
    numArray5[1] = (byte) 85;
    numArray5[6] = (byte) 18;
    numArray5[34] = (byte) 103;
    numArray5[17] = (byte) 193;
    numArray5[25] = (byte) 146;
    numArray5[10] = (byte) 253;
    numArray5[12] = (byte) 205;
    numArray5[32 /*0x20*/] = (byte) 183;
    numArray5[14] = (byte) 206;
    numArray5[15] = (byte) 159;
    numArray5[38] = (byte) 124;
    numArray5[5] = (byte) 155;
    numArray5[18] = (byte) 219;
    numArray5[19] = (byte) 120;
    numArray5[0] = (byte) 216;
    numArray5[21] = (byte) 128 /*0x80*/;
    numArray5[22] = (byte) 83;
    numArray5[23] = (byte) 88;
    numArray5[24] = (byte) 29;
    numArray5[7] = (byte) 28;
    numArray5[26] = (byte) 75;
    numArray5[27] = (byte) 242;
    numArray5[28] = (byte) 84;
    numArray5[36] = (byte) 177;
    numArray5[30] = (byte) 191;
    numArray5[31 /*0x1F*/] = (byte) 78;
    numArray5[33] = (byte) 183;
    numArray5[29] = (byte) 147;
    numArray5[37] = (byte) 195;
    numArray5[35] = (byte) 8;
    numArray5[41] = (byte) 231;
    numArray5[9] = (byte) 233;
    numArray5[13] = (byte) 16 /*0x10*/;
    numArray5[39] = (byte) 137;
    numArray5[8] = (byte) 240 /*0xF0*/;
    numArray5[20] = (byte) 242;
    numArray5[11] = (byte) 132;
    byte[] numArray6 = new byte[43]
    {
      (byte) 251,
      (byte) 239,
      (byte) 45,
      (byte) 19,
      (byte) 178,
      (byte) 177,
      (byte) 68,
      (byte) 64 /*0x40*/,
      (byte) 124,
      (byte) 216,
      (byte) 179,
      (byte) 120,
      (byte) 16 /*0x10*/,
      (byte) 12,
      (byte) 225,
      (byte) 42,
      (byte) 254,
      (byte) 205,
      (byte) 143,
      (byte) 3,
      (byte) 79,
      (byte) 190,
      (byte) 135,
      (byte) 78,
      (byte) 42,
      (byte) 210,
      (byte) 210,
      (byte) 217,
      (byte) 190,
      (byte) 100,
      (byte) 4,
      (byte) 177,
      (byte) 175,
      (byte) 234,
      (byte) 76,
      (byte) 254,
      (byte) 39,
      (byte) 114,
      (byte) 54,
      (byte) 174,
      (byte) 73,
      (byte) 105,
      (byte) 51
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 43);
    for (int index = 0; index < 43; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
