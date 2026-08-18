// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_13021
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_13021
{
  private static byte[] sspq = new byte[23]
  {
    (byte) 195,
    (byte) 157,
    (byte) 35,
    (byte) 108,
    (byte) 175,
    (byte) 213,
    (byte) 99,
    (byte) 33,
    (byte) 214,
    (byte) 116,
    (byte) 50,
    (byte) 227,
    (byte) 162,
    (byte) 101,
    (byte) 171,
    (byte) 149,
    (byte) 78,
    (byte) 234,
    (byte) 189,
    (byte) 135,
    (byte) 54,
    (byte) 182,
    (byte) 166
  };
  private static byte[] sspr = new byte[23]
  {
    (byte) 131,
    (byte) 42,
    (byte) 159,
    (byte) 139,
    (byte) 31 /*0x1F*/,
    (byte) 143,
    (byte) 246,
    (byte) 0,
    (byte) 119,
    (byte) 115,
    (byte) 249,
    (byte) 27,
    (byte) 7,
    (byte) 77,
    (byte) 159,
    (byte) 53,
    (byte) 167,
    (byte) 191,
    (byte) 42,
    (byte) 222,
    (byte) 36,
    (byte) 214,
    (byte) 150
  };

  internal static string ssp_appserver_13022()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[109];
      byte[] numArray2 = new byte[55]
      {
        (byte) 45,
        (byte) 65,
        (byte) 169,
        (byte) 181,
        (byte) 148,
        (byte) 125,
        (byte) 138,
        (byte) 199,
        (byte) 148,
        (byte) 118,
        (byte) 10,
        (byte) 154,
        (byte) 104,
        (byte) 65,
        (byte) 91,
        (byte) 59,
        (byte) 18,
        (byte) 162,
        (byte) 84,
        (byte) 47,
        (byte) 21,
        (byte) 169,
        (byte) 125,
        (byte) 110,
        (byte) 72,
        (byte) 13,
        (byte) 163,
        (byte) 53,
        (byte) 25,
        (byte) 146,
        (byte) 20,
        (byte) 239,
        (byte) 12,
        (byte) 41,
        (byte) 94,
        (byte) 105,
        (byte) 111,
        (byte) 152,
        (byte) 117,
        (byte) 106,
        (byte) 163,
        (byte) 53,
        (byte) 9,
        (byte) 88,
        (byte) 22,
        (byte) 8,
        (byte) 221,
        (byte) 56,
        (byte) 80 /*0x50*/,
        (byte) 245,
        (byte) 55,
        (byte) 153,
        (byte) 115,
        (byte) 241,
        (byte) 69
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 160 /*0xA0*/,
        (byte) 237,
        (byte) 208 /*0xD0*/,
        (byte) 157,
        (byte) 191,
        (byte) 193,
        (byte) 8,
        (byte) 208 /*0xD0*/,
        (byte) 229,
        (byte) 96 /*0x60*/,
        (byte) 29,
        (byte) 36,
        (byte) 52,
        (byte) 148,
        (byte) 63 /*0x3F*/,
        (byte) 13,
        (byte) 58,
        (byte) 187,
        (byte) 87,
        (byte) 68,
        (byte) 213,
        (byte) 244,
        (byte) 175,
        (byte) 93,
        (byte) 68,
        (byte) 136,
        (byte) 169,
        (byte) 121,
        (byte) 195,
        (byte) 128 /*0x80*/,
        (byte) 157,
        (byte) 108,
        (byte) 228,
        (byte) 201,
        (byte) 91,
        (byte) 28,
        (byte) 88,
        (byte) 72,
        (byte) 158,
        (byte) 203,
        (byte) 115,
        (byte) 166,
        (byte) 160 /*0xA0*/,
        (byte) 190,
        (byte) 148,
        (byte) 231,
        (byte) 131,
        (byte) 30,
        (byte) 176 /*0xB0*/,
        (byte) 54,
        (byte) 210,
        (byte) 76,
        (byte) 191,
        (byte) 71,
        (byte) 26
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[54]
      {
        (byte) 58,
        (byte) 109,
        (byte) 135,
        (byte) 217,
        (byte) 84,
        (byte) 173,
        (byte) 2,
        (byte) 97,
        (byte) 27,
        (byte) 141,
        (byte) 62,
        (byte) 226,
        (byte) 242,
        (byte) 203,
        (byte) 122,
        (byte) 45,
        (byte) 234,
        (byte) 139,
        (byte) 223,
        (byte) 159,
        (byte) 37,
        (byte) 88,
        (byte) 63 /*0x3F*/,
        (byte) 48 /*0x30*/,
        (byte) 107,
        (byte) 229,
        (byte) 36,
        (byte) 90,
        (byte) 122,
        (byte) 123,
        (byte) 27,
        (byte) 228,
        (byte) 218,
        (byte) 165,
        (byte) 53,
        (byte) 43,
        (byte) 238,
        (byte) 144 /*0x90*/,
        (byte) 78,
        (byte) 136,
        (byte) 7,
        (byte) 246,
        (byte) 250,
        (byte) 123,
        (byte) 158,
        (byte) 85,
        (byte) 94,
        (byte) 42,
        (byte) 188,
        (byte) 108,
        (byte) 137,
        (byte) 101,
        (byte) 101,
        (byte) 229
      };
      byte[] numArray5 = new byte[54]
      {
        (byte) 130,
        (byte) 59,
        (byte) 151,
        (byte) 60,
        (byte) 107,
        (byte) 136,
        (byte) 25,
        (byte) 98,
        (byte) 30,
        (byte) 6,
        (byte) 125,
        (byte) 165,
        (byte) 132,
        (byte) 182,
        (byte) 42,
        (byte) 194,
        (byte) 60,
        (byte) 253,
        (byte) 235,
        (byte) 138,
        (byte) 142,
        (byte) 200,
        (byte) 221,
        (byte) 204,
        (byte) 40,
        (byte) 226,
        (byte) 231,
        (byte) 223,
        (byte) 12,
        (byte) 27,
        (byte) 224 /*0xE0*/,
        (byte) 130,
        (byte) 241,
        (byte) 49,
        (byte) 159,
        (byte) 3,
        (byte) 42,
        (byte) 1,
        (byte) 56,
        (byte) 209,
        (byte) 80 /*0x50*/,
        (byte) 191,
        (byte) 26,
        (byte) 80 /*0x50*/,
        (byte) 94,
        (byte) 227,
        (byte) 151,
        (byte) 235,
        (byte) 99,
        (byte) 152,
        (byte) 151,
        (byte) 197,
        (byte) 66,
        (byte) 215
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
      (byte) 86,
      (byte) 110,
      (byte) 111,
      (byte) 137,
      (byte) 167,
      (byte) 231,
      (byte) 159,
      (byte) 114,
      (byte) 90,
      (byte) 8,
      (byte) 76,
      (byte) 150,
      (byte) 79,
      (byte) 62,
      (byte) 89,
      (byte) 60,
      (byte) 87,
      (byte) 152,
      (byte) 46,
      (byte) 94,
      (byte) 196,
      (byte) 146,
      (byte) 21,
      (byte) 134,
      (byte) 249,
      (byte) 158,
      (byte) 14,
      (byte) 242,
      (byte) 182,
      (byte) 126,
      (byte) 245,
      (byte) 7,
      (byte) 83,
      (byte) 253,
      (byte) 152,
      (byte) 34,
      (byte) 223,
      (byte) 254,
      (byte) 95,
      (byte) 14,
      (byte) 90,
      (byte) 9,
      (byte) 67,
      (byte) 77,
      (byte) 17,
      (byte) 147,
      (byte) 109,
      (byte) 45,
      (byte) 48 /*0x30*/,
      (byte) 40,
      (byte) 13,
      (byte) 76,
      (byte) 111,
      (byte) 131,
      (byte) 105
    };
    byte[] numArray8 = new byte[55]
    {
      (byte) 4,
      (byte) 42,
      (byte) 166,
      (byte) 100,
      (byte) 99,
      (byte) 211,
      (byte) 213,
      (byte) 69,
      (byte) 102,
      (byte) 252,
      (byte) 111,
      (byte) 132,
      (byte) 184,
      (byte) 106,
      (byte) 172,
      (byte) 242,
      (byte) 43,
      (byte) 147,
      (byte) 111,
      (byte) 138,
      (byte) 240 /*0xF0*/,
      (byte) 19,
      (byte) 140,
      (byte) 32 /*0x20*/,
      (byte) 202,
      (byte) 75,
      (byte) 176 /*0xB0*/,
      (byte) 96 /*0x60*/,
      (byte) 127 /*0x7F*/,
      (byte) 216,
      (byte) 180,
      (byte) 93,
      (byte) 245,
      (byte) 252,
      (byte) 230,
      (byte) 227,
      (byte) 208 /*0xD0*/,
      (byte) 0,
      (byte) 145,
      (byte) 10,
      (byte) 80 /*0x50*/,
      (byte) 181,
      (byte) 227,
      (byte) 170,
      (byte) 57,
      (byte) 248,
      (byte) 132,
      (byte) 104,
      (byte) 104,
      (byte) 150,
      (byte) 34,
      (byte) 89,
      (byte) 66,
      (byte) 13,
      (byte) 111
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[54]
    {
      (byte) 232,
      (byte) 143,
      (byte) 9,
      (byte) 216,
      (byte) 173,
      (byte) 187,
      (byte) 220,
      (byte) 19,
      (byte) 68,
      (byte) 66,
      (byte) 81,
      (byte) 166,
      (byte) 54,
      (byte) 93,
      (byte) 88,
      (byte) 56,
      (byte) 31 /*0x1F*/,
      (byte) 3,
      (byte) 187,
      (byte) 78,
      (byte) 200,
      (byte) 116,
      (byte) 102,
      (byte) 48 /*0x30*/,
      (byte) 224 /*0xE0*/,
      (byte) 193,
      (byte) 141,
      (byte) 112 /*0x70*/,
      (byte) 168,
      (byte) 152,
      (byte) 208 /*0xD0*/,
      (byte) 148,
      (byte) 212,
      (byte) 202,
      (byte) 246,
      (byte) 111,
      (byte) 85,
      (byte) 54,
      (byte) 172,
      (byte) 77,
      (byte) 103,
      (byte) 58,
      (byte) 204,
      (byte) 81,
      (byte) 27,
      (byte) 134,
      (byte) 249,
      (byte) 203,
      (byte) 181,
      (byte) 19,
      (byte) 79,
      (byte) 127 /*0x7F*/,
      (byte) 173,
      (byte) 117
    };
    byte[] numArray10 = new byte[54];
    numArray10[8] = (byte) 254;
    numArray10[27] = (byte) 47;
    numArray10[1] = (byte) 187;
    numArray10[3] = (byte) 146;
    numArray10[4] = (byte) 247;
    numArray10[50] = (byte) 153;
    numArray10[6] = (byte) 115;
    numArray10[7] = (byte) 4;
    numArray10[14] = (byte) 226;
    numArray10[37] = (byte) 193;
    numArray10[41] = (byte) 228;
    numArray10[0] = (byte) 0;
    numArray10[2] = (byte) 229;
    numArray10[20] = (byte) 82;
    numArray10[15] = (byte) 185;
    numArray10[17] = (byte) 231;
    numArray10[16 /*0x10*/] = (byte) 51;
    numArray10[26] = (byte) 0;
    numArray10[23] = (byte) 154;
    numArray10[5] = (byte) 212;
    numArray10[49] = (byte) 35;
    numArray10[21] = (byte) 70;
    numArray10[18] = (byte) 34;
    numArray10[25] = (byte) 252;
    numArray10[51] = (byte) 100;
    numArray10[47] = byte.MaxValue;
    numArray10[40] = (byte) 164;
    numArray10[19] = (byte) 181;
    numArray10[28] = (byte) 206;
    numArray10[10] = (byte) 41;
    numArray10[53] = (byte) 231;
    numArray10[31 /*0x1F*/] = (byte) 43;
    numArray10[32 /*0x20*/] = (byte) 177;
    numArray10[33] = (byte) 177;
    numArray10[30] = (byte) 249;
    numArray10[35] = (byte) 244;
    numArray10[43] = (byte) 70;
    numArray10[24] = (byte) 176 /*0xB0*/;
    numArray10[38] = (byte) 108;
    numArray10[29] = (byte) 202;
    numArray10[39] = (byte) 75;
    numArray10[34] = (byte) 187;
    numArray10[42] = (byte) 111;
    numArray10[22] = (byte) 22;
    numArray10[44] = (byte) 156;
    numArray10[9] = (byte) 249;
    numArray10[46] = (byte) 189;
    numArray10[12] = (byte) 171;
    numArray10[48 /*0x30*/] = (byte) 184;
    numArray10[45] = (byte) 230;
    numArray10[52] = (byte) 124;
    numArray10[13] = (byte) 230;
    numArray10[36] = (byte) 35;
    numArray10[11] = (byte) 111;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 54);
    for (int index = 0; index < 54; ++index)
      numArray6[index + 55] ^= numArray10[index];
    byte[] numArray11 = new byte[23];
    byte[] response = new byte[23];
    Array.Copy((Array) sc_13021.sspq, 0, (Array) numArray11, 0, 23);
    key.Query(true, 335, numArray11, response);
    Array.Copy((Array) sc_13021.sspr, 0, (Array) numArray11, 0, 23);
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

  internal static string ssp_appserver_13023()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[116];
      byte[] numArray2 = new byte[55]
      {
        (byte) 93,
        (byte) 238,
        (byte) 99,
        (byte) 227,
        (byte) 39,
        (byte) 166,
        (byte) 208 /*0xD0*/,
        (byte) 55,
        (byte) 183,
        (byte) 50,
        (byte) 185,
        (byte) 160 /*0xA0*/,
        (byte) 157,
        (byte) 178,
        (byte) 76,
        (byte) 250,
        (byte) 189,
        (byte) 188,
        (byte) 135,
        (byte) 63 /*0x3F*/,
        (byte) 90,
        (byte) 129,
        (byte) 41,
        (byte) 202,
        (byte) 185,
        (byte) 148,
        (byte) 241,
        (byte) 163,
        (byte) 68,
        (byte) 68,
        (byte) 222,
        (byte) 12,
        (byte) 247,
        (byte) 231,
        (byte) 15,
        (byte) 193,
        (byte) 113,
        (byte) 127 /*0x7F*/,
        (byte) 250,
        (byte) 47,
        (byte) 163,
        (byte) 251,
        (byte) 44,
        (byte) 112 /*0x70*/,
        (byte) 172,
        (byte) 11,
        (byte) 247,
        (byte) 156,
        (byte) 158,
        (byte) 195,
        (byte) 202,
        (byte) 121,
        (byte) 47,
        (byte) 102,
        (byte) 225
      };
      byte[] numArray3 = new byte[55];
      numArray3[27] = (byte) 131;
      numArray3[38] = (byte) 178;
      numArray3[2] = (byte) 172;
      numArray3[13] = (byte) 8;
      numArray3[0] = (byte) 129;
      numArray3[5] = (byte) 89;
      numArray3[18] = (byte) 127 /*0x7F*/;
      numArray3[7] = (byte) 199;
      numArray3[30] = (byte) 211;
      numArray3[9] = (byte) 37;
      numArray3[32 /*0x20*/] = (byte) 14;
      numArray3[11] = (byte) 26;
      numArray3[42] = (byte) 31 /*0x1F*/;
      numArray3[49] = (byte) 251;
      numArray3[3] = (byte) 216;
      numArray3[47] = (byte) 63 /*0x3F*/;
      numArray3[48 /*0x30*/] = (byte) 224 /*0xE0*/;
      numArray3[15] = (byte) 199;
      numArray3[35] = (byte) 111;
      numArray3[6] = (byte) 230;
      numArray3[16 /*0x10*/] = (byte) 152;
      numArray3[21] = (byte) 134;
      numArray3[22] = (byte) 0;
      numArray3[23] = (byte) 243;
      numArray3[26] = (byte) 211;
      numArray3[25] = (byte) 0;
      numArray3[10] = (byte) 15;
      numArray3[4] = (byte) 239;
      numArray3[28] = (byte) 159;
      numArray3[39] = (byte) 120;
      numArray3[20] = (byte) 213;
      numArray3[31 /*0x1F*/] = (byte) 120;
      numArray3[14] = (byte) 130;
      numArray3[33] = (byte) 191;
      numArray3[53] = (byte) 88;
      numArray3[29] = (byte) 253;
      numArray3[12] = (byte) 127 /*0x7F*/;
      numArray3[1] = (byte) 248;
      numArray3[17] = (byte) 113;
      numArray3[34] = (byte) 210;
      numArray3[40] = (byte) 93;
      numArray3[41] = (byte) 233;
      numArray3[24] = (byte) 236;
      numArray3[43] = (byte) 146;
      numArray3[44] = (byte) 56;
      numArray3[45] = (byte) 161;
      numArray3[46] = (byte) 20;
      numArray3[36] = (byte) 81;
      numArray3[37] = (byte) 203;
      numArray3[8] = (byte) 143;
      numArray3[50] = (byte) 56;
      numArray3[51] = (byte) 56;
      numArray3[52] = (byte) 52;
      numArray3[19] = (byte) 90;
      numArray3[54] = (byte) 197;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 122,
        (byte) 124,
        (byte) 246,
        (byte) 97,
        (byte) 40,
        (byte) 112 /*0x70*/,
        (byte) 248,
        (byte) 239,
        (byte) 206,
        (byte) 24,
        (byte) 176 /*0xB0*/,
        (byte) 111,
        (byte) 143,
        (byte) 175,
        (byte) 232,
        (byte) 90,
        (byte) 221,
        (byte) 38,
        (byte) 84,
        (byte) 154,
        (byte) 157,
        (byte) 29,
        (byte) 75,
        (byte) 91,
        (byte) 91,
        (byte) 241,
        (byte) 70,
        (byte) 237,
        (byte) 173,
        (byte) 5,
        (byte) 198,
        (byte) 131,
        (byte) 171,
        (byte) 240 /*0xF0*/,
        (byte) 93,
        (byte) 216,
        (byte) 112 /*0x70*/,
        (byte) 25,
        (byte) 77,
        (byte) 152,
        (byte) 193,
        (byte) 44,
        (byte) 49,
        (byte) 73,
        (byte) 144 /*0x90*/,
        (byte) 137,
        (byte) 132,
        (byte) 139,
        (byte) 163,
        (byte) 92,
        (byte) 250,
        (byte) 163,
        (byte) 46,
        (byte) 237,
        (byte) 109
      };
      byte[] numArray5 = new byte[55];
      numArray5[46] = (byte) 75;
      numArray5[47] = (byte) 174;
      numArray5[40] = (byte) 234;
      numArray5[2] = (byte) 23;
      numArray5[19] = (byte) 38;
      numArray5[4] = (byte) 207;
      numArray5[20] = (byte) 105;
      numArray5[35] = (byte) 176 /*0xB0*/;
      numArray5[37] = (byte) 186;
      numArray5[18] = (byte) 9;
      numArray5[10] = (byte) 201;
      numArray5[11] = (byte) 249;
      numArray5[9] = (byte) 156;
      numArray5[13] = (byte) 151;
      numArray5[14] = (byte) 22;
      numArray5[15] = (byte) 194;
      numArray5[27] = (byte) 206;
      numArray5[38] = (byte) 111;
      numArray5[25] = (byte) 83;
      numArray5[32 /*0x20*/] = (byte) 203;
      numArray5[39] = (byte) 87;
      numArray5[21] = (byte) 237;
      numArray5[22] = (byte) 143;
      numArray5[23] = (byte) 77;
      numArray5[24] = (byte) 191;
      numArray5[8] = (byte) 226;
      numArray5[26] = (byte) 84;
      numArray5[6] = (byte) 36;
      numArray5[36] = (byte) 202;
      numArray5[33] = (byte) 199;
      numArray5[1] = (byte) 126;
      numArray5[49] = (byte) 197;
      numArray5[50] = (byte) 184;
      numArray5[53] = (byte) 214;
      numArray5[34] = (byte) 47;
      numArray5[5] = (byte) 220;
      numArray5[48 /*0x30*/] = (byte) 37;
      numArray5[16 /*0x10*/] = (byte) 44;
      numArray5[7] = (byte) 196;
      numArray5[54] = (byte) 237;
      numArray5[0] = (byte) 67;
      numArray5[41] = (byte) 237;
      numArray5[42] = (byte) 103;
      numArray5[43] = (byte) 149;
      numArray5[44] = (byte) 212;
      numArray5[45] = (byte) 0;
      numArray5[31 /*0x1F*/] = (byte) 225;
      numArray5[28] = (byte) 17;
      numArray5[29] = (byte) 91;
      numArray5[17] = (byte) 93;
      numArray5[30] = (byte) 136;
      numArray5[51] = (byte) 94;
      numArray5[52] = (byte) 254;
      numArray5[12] = (byte) 141;
      numArray5[3] = (byte) 104;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[6];
      numArray6[3] = (byte) 111;
      numArray6[1] = (byte) 52;
      numArray6[2] = (byte) 202;
      numArray6[0] = (byte) 207;
      numArray6[4] = (byte) 193;
      numArray6[5] = (byte) 240 /*0xF0*/;
      byte[] numArray7 = new byte[6]
      {
        (byte) 224 /*0xE0*/,
        (byte) 202,
        (byte) 223,
        (byte) 141,
        (byte) 79,
        (byte) 18
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 6);
      for (int index = 0; index < 6; ++index)
        numArray1[index + 110] ^= numArray7[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray8 = new byte[116];
    byte[] numArray9 = new byte[55];
    numArray9[16 /*0x10*/] = (byte) 134;
    numArray9[27] = (byte) 139;
    numArray9[24] = (byte) 249;
    numArray9[3] = (byte) 134;
    numArray9[4] = (byte) 132;
    numArray9[40] = (byte) 104;
    numArray9[6] = (byte) 103;
    numArray9[21] = (byte) 75;
    numArray9[8] = (byte) 135;
    numArray9[49] = (byte) 93;
    numArray9[22] = byte.MaxValue;
    numArray9[11] = (byte) 223;
    numArray9[18] = (byte) 120;
    numArray9[13] = (byte) 87;
    numArray9[29] = (byte) 201;
    numArray9[15] = (byte) 169;
    numArray9[43] = (byte) 237;
    numArray9[1] = (byte) 77;
    numArray9[41] = (byte) 39;
    numArray9[32 /*0x20*/] = (byte) 93;
    numArray9[20] = (byte) 222;
    numArray9[7] = (byte) 12;
    numArray9[44] = (byte) 11;
    numArray9[17] = (byte) 144 /*0x90*/;
    numArray9[28] = (byte) 189;
    numArray9[10] = (byte) 182;
    numArray9[14] = (byte) 226;
    numArray9[5] = (byte) 11;
    numArray9[45] = (byte) 17;
    numArray9[31 /*0x1F*/] = (byte) 41;
    numArray9[30] = (byte) 232;
    numArray9[53] = (byte) 13;
    numArray9[51] = (byte) 193;
    numArray9[33] = (byte) 132;
    numArray9[34] = (byte) 7;
    numArray9[35] = (byte) 36;
    numArray9[36] = (byte) 241;
    numArray9[39] = (byte) 34;
    numArray9[38] = (byte) 236;
    numArray9[19] = (byte) 6;
    numArray9[46] = (byte) 232;
    numArray9[37] = (byte) 118;
    numArray9[2] = (byte) 35;
    numArray9[23] = (byte) 244;
    numArray9[26] = (byte) 198;
    numArray9[42] = (byte) 66;
    numArray9[12] = (byte) 213;
    numArray9[47] = (byte) 216;
    numArray9[0] = (byte) 194;
    numArray9[48 /*0x30*/] = (byte) 245;
    numArray9[50] = (byte) 149;
    numArray9[9] = (byte) 129;
    numArray9[52] = (byte) 240 /*0xF0*/;
    numArray9[25] = (byte) 170;
    numArray9[54] = (byte) 210;
    byte[] numArray10 = new byte[55]
    {
      (byte) 226,
      (byte) 193,
      (byte) 242,
      (byte) 216,
      (byte) 167,
      (byte) 6,
      (byte) 36,
      (byte) 13,
      (byte) 191,
      (byte) 75,
      (byte) 108,
      (byte) 254,
      (byte) 120,
      (byte) 40,
      (byte) 162,
      (byte) 64 /*0x40*/,
      (byte) 44,
      (byte) 55,
      (byte) 225,
      (byte) 53,
      (byte) 96 /*0x60*/,
      (byte) 141,
      (byte) 29,
      (byte) 228,
      (byte) 54,
      (byte) 109,
      (byte) 38,
      (byte) 223,
      (byte) 181,
      (byte) 230,
      (byte) 73,
      (byte) 231,
      (byte) 248,
      (byte) 179,
      (byte) 81,
      (byte) 178,
      (byte) 129,
      (byte) 65,
      (byte) 53,
      (byte) 135,
      (byte) 94,
      (byte) 104,
      (byte) 119,
      (byte) 62,
      (byte) 4,
      (byte) 134,
      (byte) 213,
      (byte) 118,
      (byte) 90,
      (byte) 218,
      (byte) 230,
      (byte) 119,
      (byte) 201,
      (byte) 25,
      (byte) 119
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray8, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index] ^= numArray10[index];
    byte[] numArray11 = new byte[55];
    numArray11[5] = (byte) 238;
    numArray11[1] = (byte) 164;
    numArray11[2] = (byte) 22;
    numArray11[3] = (byte) 87;
    numArray11[51] = (byte) 92;
    numArray11[20] = (byte) 190;
    numArray11[30] = (byte) 224 /*0xE0*/;
    numArray11[47] = (byte) 176 /*0xB0*/;
    numArray11[41] = (byte) 224 /*0xE0*/;
    numArray11[9] = (byte) 48 /*0x30*/;
    numArray11[45] = (byte) 202;
    numArray11[11] = (byte) 190;
    numArray11[28] = (byte) 9;
    numArray11[31 /*0x1F*/] = (byte) 150;
    numArray11[13] = (byte) 245;
    numArray11[15] = (byte) 144 /*0x90*/;
    numArray11[16 /*0x10*/] = (byte) 120;
    numArray11[53] = (byte) 232;
    numArray11[18] = (byte) 221;
    numArray11[19] = (byte) 133;
    numArray11[7] = (byte) 114;
    numArray11[21] = (byte) 106;
    numArray11[33] = (byte) 154;
    numArray11[23] = (byte) 42;
    numArray11[24] = (byte) 100;
    numArray11[25] = (byte) 94;
    numArray11[0] = (byte) 69;
    numArray11[32 /*0x20*/] = (byte) 35;
    numArray11[34] = (byte) 124;
    numArray11[29] = (byte) 159;
    numArray11[50] = (byte) 33;
    numArray11[8] = (byte) 83;
    numArray11[14] = (byte) 37;
    numArray11[17] = (byte) 18;
    numArray11[26] = (byte) 185;
    numArray11[35] = (byte) 69;
    numArray11[36] = (byte) 252;
    numArray11[37] = (byte) 133;
    numArray11[42] = (byte) 43;
    numArray11[4] = (byte) 70;
    numArray11[22] = (byte) 87;
    numArray11[10] = (byte) 157;
    numArray11[54] = (byte) 126;
    numArray11[43] = (byte) 95;
    numArray11[39] = (byte) 234;
    numArray11[44] = (byte) 249;
    numArray11[6] = (byte) 137;
    numArray11[48 /*0x30*/] = (byte) 96 /*0x60*/;
    numArray11[46] = (byte) 80 /*0x50*/;
    numArray11[49] = (byte) 141;
    numArray11[40] = (byte) 251;
    numArray11[27] = (byte) 174;
    numArray11[52] = (byte) 62;
    numArray11[12] = (byte) 231;
    numArray11[38] = (byte) 69;
    byte[] numArray12 = new byte[55]
    {
      (byte) 31 /*0x1F*/,
      (byte) 227,
      (byte) 186,
      (byte) 151,
      (byte) 245,
      (byte) 4,
      (byte) 42,
      (byte) 191,
      (byte) 167,
      (byte) 159,
      (byte) 98,
      (byte) 167,
      (byte) 192 /*0xC0*/,
      (byte) 4,
      (byte) 240 /*0xF0*/,
      (byte) 254,
      (byte) 94,
      (byte) 128 /*0x80*/,
      (byte) 155,
      (byte) 41,
      (byte) 220,
      (byte) 144 /*0x90*/,
      (byte) 217,
      (byte) 172,
      (byte) 37,
      (byte) 83,
      (byte) 125,
      (byte) 70,
      (byte) 148,
      (byte) 188,
      (byte) 200,
      (byte) 166,
      (byte) 139,
      (byte) 110,
      (byte) 19,
      (byte) 128 /*0x80*/,
      (byte) 231,
      (byte) 208 /*0xD0*/,
      (byte) 131,
      (byte) 81,
      (byte) 125,
      (byte) 107,
      (byte) 16 /*0x10*/,
      (byte) 72,
      (byte) 113,
      (byte) 93,
      (byte) 249,
      (byte) 158,
      (byte) 133,
      (byte) 205,
      (byte) 74,
      (byte) 57,
      (byte) 119,
      (byte) 85,
      (byte) 238
    };
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray8, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index + 55] ^= numArray12[index];
    byte[] numArray13 = new byte[6];
    numArray13[1] = (byte) 90;
    numArray13[0] = (byte) 187;
    numArray13[5] = (byte) 238;
    numArray13[3] = (byte) 44;
    numArray13[4] = (byte) 140;
    numArray13[2] = (byte) 217;
    byte[] numArray14 = new byte[6]
    {
      (byte) 25,
      (byte) 148,
      (byte) 21,
      (byte) 0,
      (byte) 32 /*0x20*/,
      (byte) 0
    };
    numArray14[3] = (byte) 8;
    numArray14[5] = (byte) 78;
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray8, 110, 6);
    for (int index = 0; index < 6; ++index)
      numArray8[index + 110] ^= numArray14[index];
    return Encoding.UTF8.GetString(numArray8);
  }
}
