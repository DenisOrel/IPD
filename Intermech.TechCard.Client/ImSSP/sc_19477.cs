// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_19477
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_19477
{
  private static byte[] sspq = new byte[126]
  {
    (byte) 26,
    (byte) 78,
    (byte) 230,
    (byte) 198,
    (byte) 154,
    (byte) 65,
    (byte) 236,
    (byte) 88,
    (byte) 254,
    (byte) 76,
    (byte) 44,
    (byte) 185,
    (byte) 126,
    (byte) 225,
    (byte) 77,
    (byte) 6,
    (byte) 67,
    (byte) 88,
    (byte) 53,
    (byte) 180,
    (byte) 218,
    (byte) 64 /*0x40*/,
    (byte) 213,
    (byte) 203,
    (byte) 157,
    (byte) 224 /*0xE0*/,
    (byte) 217,
    (byte) 244,
    (byte) 213,
    (byte) 72,
    (byte) 142,
    (byte) 195,
    (byte) 39,
    (byte) 20,
    (byte) 184,
    (byte) 141,
    (byte) 91,
    (byte) 254,
    (byte) 23,
    (byte) 167,
    (byte) 65,
    (byte) 160 /*0xA0*/,
    (byte) 242,
    (byte) 78,
    (byte) 48 /*0x30*/,
    (byte) 121,
    (byte) 176 /*0xB0*/,
    (byte) 221,
    (byte) 20,
    (byte) 65,
    (byte) 214,
    (byte) 94,
    (byte) 139,
    (byte) 117,
    (byte) 224 /*0xE0*/,
    (byte) 79,
    (byte) 223,
    (byte) 19,
    (byte) 228,
    (byte) 59,
    (byte) 19,
    (byte) 187,
    (byte) 109,
    (byte) 27,
    (byte) 203,
    (byte) 190,
    (byte) 222,
    (byte) 145,
    (byte) 47,
    (byte) 147,
    (byte) 52,
    (byte) 70,
    (byte) 189,
    (byte) 71,
    (byte) 32 /*0x20*/,
    (byte) 162,
    (byte) 28,
    (byte) 153,
    (byte) 146,
    (byte) 246,
    (byte) 191,
    (byte) 41,
    (byte) 190,
    (byte) 245,
    (byte) 203,
    (byte) 32 /*0x20*/,
    (byte) 104,
    (byte) 203,
    (byte) 226,
    (byte) 32 /*0x20*/,
    (byte) 90,
    (byte) 123,
    (byte) 233,
    (byte) 253,
    (byte) 120,
    (byte) 80 /*0x50*/,
    (byte) 170,
    (byte) 86,
    (byte) 19,
    (byte) 174,
    (byte) 15,
    (byte) 57,
    (byte) 48 /*0x30*/,
    (byte) 114,
    (byte) 173,
    (byte) 175,
    (byte) 114,
    (byte) 161,
    (byte) 85,
    (byte) 173,
    (byte) 126,
    byte.MaxValue,
    (byte) 110,
    (byte) 130,
    (byte) 138,
    (byte) 182,
    (byte) 155,
    (byte) 63 /*0x3F*/,
    (byte) 97,
    (byte) 213,
    (byte) 165,
    (byte) 238,
    (byte) 20,
    (byte) 131,
    (byte) 231,
    (byte) 148
  };
  private static byte[] sspr = new byte[126]
  {
    (byte) 202,
    (byte) 238,
    (byte) 43,
    (byte) 26,
    (byte) 74,
    (byte) 1,
    (byte) 100,
    (byte) 167,
    (byte) 74,
    (byte) 121,
    (byte) 171,
    (byte) 72,
    (byte) 60,
    (byte) 179,
    (byte) 103,
    (byte) 217,
    (byte) 216,
    (byte) 223,
    (byte) 166,
    (byte) 249,
    (byte) 212,
    (byte) 181,
    (byte) 104,
    (byte) 170,
    (byte) 0,
    (byte) 38,
    (byte) 203,
    (byte) 113,
    (byte) 1,
    (byte) 163,
    (byte) 173,
    (byte) 47,
    (byte) 109,
    (byte) 196,
    (byte) 47,
    (byte) 238,
    (byte) 27,
    (byte) 26,
    (byte) 70,
    (byte) 81,
    (byte) 125,
    (byte) 147,
    (byte) 178,
    (byte) 248,
    (byte) 77,
    (byte) 195,
    (byte) 91,
    (byte) 233,
    (byte) 38,
    (byte) 155,
    (byte) 30,
    (byte) 155,
    (byte) 167,
    (byte) 45,
    (byte) 79,
    (byte) 70,
    (byte) 35,
    (byte) 2,
    (byte) 119,
    (byte) 100,
    (byte) 92,
    (byte) 102,
    (byte) 103,
    (byte) 225,
    (byte) 191,
    (byte) 37,
    (byte) 106,
    (byte) 91,
    (byte) 223,
    (byte) 48 /*0x30*/,
    (byte) 187,
    (byte) 154,
    (byte) 242,
    (byte) 81,
    (byte) 201,
    (byte) 180,
    (byte) 191,
    (byte) 118,
    (byte) 68,
    (byte) 124,
    (byte) 55,
    (byte) 124,
    (byte) 155,
    (byte) 136,
    (byte) 60,
    (byte) 216,
    (byte) 125,
    (byte) 204,
    (byte) 170,
    (byte) 253,
    (byte) 110,
    (byte) 60,
    (byte) 96 /*0x60*/,
    (byte) 247,
    (byte) 146,
    (byte) 101,
    (byte) 99,
    (byte) 3,
    (byte) 176 /*0xB0*/,
    (byte) 167,
    (byte) 48 /*0x30*/,
    (byte) 60,
    (byte) 52,
    (byte) 213,
    (byte) 21,
    (byte) 145,
    (byte) 113,
    (byte) 64 /*0x40*/,
    (byte) 219,
    (byte) 147,
    (byte) 214,
    (byte) 180,
    (byte) 242,
    (byte) 144 /*0x90*/,
    (byte) 228,
    (byte) 90,
    (byte) 205,
    (byte) 36,
    (byte) 116,
    (byte) 223,
    (byte) 206,
    (byte) 121,
    (byte) 226,
    (byte) 121,
    (byte) 23,
    (byte) 59
  };

  internal static int ssp_techcard_19478(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 171,
      (byte) 224 /*0xE0*/,
      (byte) 99,
      (byte) 10,
      (byte) 44,
      (byte) 174,
      (byte) 180,
      (byte) 88,
      (byte) 122,
      (byte) 62,
      (byte) 200,
      (byte) 235,
      (byte) 110,
      (byte) 53,
      (byte) 8,
      (byte) 9,
      (byte) 16 /*0x10*/,
      (byte) 82,
      (byte) 215,
      (byte) 77,
      (byte) 50,
      (byte) 61,
      (byte) 189,
      (byte) 126,
      (byte) 142,
      (byte) 207,
      (byte) 25,
      (byte) 140,
      (byte) 158,
      (byte) 107,
      (byte) 111,
      (byte) 167,
      (byte) 67,
      (byte) 152,
      (byte) 116,
      (byte) 200,
      (byte) 32 /*0x20*/,
      (byte) 237,
      (byte) 67,
      (byte) 210,
      (byte) 220,
      (byte) 116,
      (byte) 5,
      (byte) 89,
      (byte) 29,
      (byte) 25,
      (byte) 6,
      (byte) 159
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[20] = (byte) 98;
    sourceArray2[36] = (byte) 3;
    sourceArray2[2] = (byte) 100;
    sourceArray2[0] = (byte) 211;
    sourceArray2[32 /*0x20*/] = (byte) 40;
    sourceArray2[5] = (byte) 172;
    sourceArray2[4] = (byte) 36;
    sourceArray2[14] = (byte) 26;
    sourceArray2[8] = (byte) 184;
    sourceArray2[6] = (byte) 142;
    sourceArray2[10] = (byte) 38;
    sourceArray2[11] = (byte) 122;
    sourceArray2[39] = (byte) 102;
    sourceArray2[13] = (byte) 158;
    sourceArray2[12] = (byte) 82;
    sourceArray2[15] = (byte) 97;
    sourceArray2[25] = (byte) 104;
    sourceArray2[22] = (byte) 77;
    sourceArray2[18] = (byte) 244;
    sourceArray2[19] = (byte) 74;
    sourceArray2[17] = (byte) 57;
    sourceArray2[1] = (byte) 38;
    sourceArray2[26] = (byte) 43;
    sourceArray2[7] = (byte) 237;
    sourceArray2[42] = (byte) 147;
    sourceArray2[38] = (byte) 226;
    sourceArray2[9] = (byte) 40;
    sourceArray2[23] = (byte) 67;
    sourceArray2[40] = (byte) 196;
    sourceArray2[29] = (byte) 45;
    sourceArray2[30] = (byte) 9;
    sourceArray2[31 /*0x1F*/] = (byte) 35;
    sourceArray2[21] = (byte) 24;
    sourceArray2[28] = (byte) 57;
    sourceArray2[34] = (byte) 245;
    sourceArray2[35] = (byte) 233;
    sourceArray2[27] = (byte) 137;
    sourceArray2[37] = (byte) 211;
    sourceArray2[16 /*0x10*/] = (byte) 175;
    sourceArray2[33] = (byte) 47;
    sourceArray2[24] = (byte) 74;
    sourceArray2[41] = (byte) 209;
    sourceArray2[43] = (byte) 189;
    sourceArray2[3] = (byte) 205;
    sourceArray2[44] = (byte) 251;
    sourceArray2[45] = (byte) 9;
    sourceArray2[46] = (byte) 117;
    sourceArray2[47] = (byte) 251;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 359, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_techcard_19479(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 48 /*0x30*/,
      (byte) 222,
      (byte) 13,
      (byte) 112 /*0x70*/,
      (byte) 247,
      (byte) 51,
      (byte) 94,
      (byte) 29,
      (byte) 152,
      (byte) 54,
      (byte) 189,
      (byte) 132,
      (byte) 7,
      (byte) 147,
      (byte) 27,
      (byte) 233,
      (byte) 117,
      (byte) 190,
      (byte) 162,
      (byte) 219,
      (byte) 244,
      (byte) 146,
      (byte) 186,
      (byte) 153,
      (byte) 174,
      (byte) 178,
      (byte) 40,
      (byte) 198,
      (byte) 189,
      (byte) 2,
      (byte) 65,
      (byte) 218,
      (byte) 99,
      (byte) 3,
      (byte) 153,
      (byte) 155,
      (byte) 50,
      (byte) 211,
      (byte) 89,
      (byte) 248,
      (byte) 229,
      (byte) 150,
      (byte) 170,
      (byte) 208 /*0xD0*/,
      (byte) 232,
      (byte) 181,
      (byte) 105,
      (byte) 82
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[37] = (byte) 244;
    sourceArray2[1] = (byte) 69;
    sourceArray2[12] = (byte) 122;
    sourceArray2[19] = (byte) 90;
    sourceArray2[4] = (byte) 170;
    sourceArray2[25] = (byte) 181;
    sourceArray2[0] = (byte) 157;
    sourceArray2[5] = (byte) 227;
    sourceArray2[8] = (byte) 177;
    sourceArray2[9] = (byte) 44;
    sourceArray2[26] = (byte) 28;
    sourceArray2[11] = (byte) 153;
    sourceArray2[7] = (byte) 112 /*0x70*/;
    sourceArray2[17] = (byte) 40;
    sourceArray2[45] = (byte) 229;
    sourceArray2[27] = (byte) 117;
    sourceArray2[16 /*0x10*/] = (byte) 4;
    sourceArray2[10] = (byte) 168;
    sourceArray2[2] = (byte) 149;
    sourceArray2[38] = (byte) 20;
    sourceArray2[24] = (byte) 109;
    sourceArray2[33] = (byte) 125;
    sourceArray2[20] = (byte) 211;
    sourceArray2[23] = (byte) 8;
    sourceArray2[21] = (byte) 7;
    sourceArray2[36] = (byte) 148;
    sourceArray2[18] = (byte) 94;
    sourceArray2[30] = (byte) 30;
    sourceArray2[43] = (byte) 156;
    sourceArray2[29] = (byte) 18;
    sourceArray2[42] = (byte) 145;
    sourceArray2[31 /*0x1F*/] = (byte) 158;
    sourceArray2[32 /*0x20*/] = (byte) 244;
    sourceArray2[14] = (byte) 85;
    sourceArray2[6] = (byte) 87;
    sourceArray2[35] = (byte) 236;
    sourceArray2[13] = (byte) 46;
    sourceArray2[34] = (byte) 77;
    sourceArray2[40] = (byte) 137;
    sourceArray2[39] = (byte) 178;
    sourceArray2[44] = (byte) 127 /*0x7F*/;
    sourceArray2[41] = (byte) 239;
    sourceArray2[3] = (byte) 137;
    sourceArray2[28] = (byte) 5;
    sourceArray2[22] = (byte) 69;
    sourceArray2[15] = (byte) 178;
    sourceArray2[46] = (byte) 46;
    sourceArray2[47] = (byte) 6;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 359, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_techcard_19480(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 101,
      (byte) 88,
      (byte) 161,
      (byte) 19,
      (byte) 164,
      (byte) 210,
      (byte) 62,
      (byte) 43,
      (byte) 31 /*0x1F*/,
      (byte) 181,
      (byte) 58,
      (byte) 194,
      (byte) 201,
      (byte) 251,
      (byte) 53,
      (byte) 244,
      (byte) 108,
      (byte) 217,
      (byte) 234,
      (byte) 239,
      (byte) 224 /*0xE0*/,
      (byte) 125,
      (byte) 254,
      (byte) 10,
      (byte) 248,
      (byte) 143,
      (byte) 81,
      (byte) 188,
      (byte) 169,
      (byte) 37,
      (byte) 210,
      (byte) 13,
      (byte) 170,
      (byte) 100,
      (byte) 227,
      (byte) 125,
      (byte) 2,
      (byte) 223,
      (byte) 23,
      (byte) 55,
      (byte) 180,
      (byte) 22,
      (byte) 102,
      (byte) 69,
      (byte) 59,
      (byte) 252,
      (byte) 72,
      (byte) 24
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[45] = (byte) 226;
    sourceArray2[1] = (byte) 16 /*0x10*/;
    sourceArray2[29] = (byte) 44;
    sourceArray2[3] = (byte) 171;
    sourceArray2[4] = (byte) 150;
    sourceArray2[23] = (byte) 197;
    sourceArray2[11] = (byte) 212;
    sourceArray2[20] = (byte) 55;
    sourceArray2[8] = (byte) 184;
    sourceArray2[9] = (byte) 42;
    sourceArray2[10] = (byte) 140;
    sourceArray2[28] = (byte) 237;
    sourceArray2[12] = (byte) 10;
    sourceArray2[38] = (byte) 227;
    sourceArray2[34] = (byte) 220;
    sourceArray2[15] = (byte) 229;
    sourceArray2[16 /*0x10*/] = (byte) 1;
    sourceArray2[17] = (byte) 244;
    sourceArray2[25] = (byte) 75;
    sourceArray2[2] = (byte) 4;
    sourceArray2[44] = (byte) 3;
    sourceArray2[21] = (byte) 193;
    sourceArray2[22] = (byte) 151;
    sourceArray2[43] = byte.MaxValue;
    sourceArray2[24] = (byte) 57;
    sourceArray2[0] = (byte) 204;
    sourceArray2[19] = (byte) 173;
    sourceArray2[27] = (byte) 245;
    sourceArray2[35] = (byte) 120;
    sourceArray2[5] = (byte) 169;
    sourceArray2[41] = (byte) 232;
    sourceArray2[13] = (byte) 176 /*0xB0*/;
    sourceArray2[32 /*0x20*/] = (byte) 2;
    sourceArray2[33] = (byte) 15;
    sourceArray2[18] = (byte) 204;
    sourceArray2[31 /*0x1F*/] = (byte) 112 /*0x70*/;
    sourceArray2[36] = (byte) 232;
    sourceArray2[37] = (byte) 142;
    sourceArray2[7] = (byte) 148;
    sourceArray2[39] = (byte) 90;
    sourceArray2[40] = (byte) 109;
    sourceArray2[42] = (byte) 196;
    sourceArray2[6] = (byte) 109;
    sourceArray2[14] = (byte) 10;
    sourceArray2[26] = (byte) 20;
    sourceArray2[30] = (byte) 196;
    sourceArray2[46] = (byte) 142;
    sourceArray2[47] = (byte) 82;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 359, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_techcard_19481()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19]
      {
        (byte) 229,
        (byte) 227,
        byte.MaxValue,
        (byte) 254,
        (byte) 103,
        (byte) 61,
        (byte) 121,
        (byte) 63 /*0x3F*/,
        (byte) 218,
        (byte) 233,
        (byte) 209,
        (byte) 159,
        (byte) 200,
        (byte) 32 /*0x20*/,
        (byte) 206,
        (byte) 71,
        (byte) 124,
        (byte) 49,
        (byte) 158
      };
      byte[] numArray3 = new byte[19];
      numArray3[16 /*0x10*/] = (byte) 92;
      numArray3[5] = (byte) 18;
      numArray3[17] = (byte) 184;
      numArray3[3] = (byte) 125;
      numArray3[4] = (byte) 124;
      numArray3[10] = (byte) 121;
      numArray3[6] = (byte) 120;
      numArray3[7] = (byte) 148;
      numArray3[8] = (byte) 111;
      numArray3[13] = (byte) 218;
      numArray3[11] = (byte) 54;
      numArray3[0] = (byte) 198;
      numArray3[12] = (byte) 133;
      numArray3[2] = (byte) 101;
      numArray3[14] = (byte) 135;
      numArray3[9] = (byte) 141;
      numArray3[1] = (byte) 237;
      numArray3[15] = (byte) 94;
      numArray3[18] = (byte) 8;
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[19];
    byte[] numArray5 = new byte[19]
    {
      (byte) 220,
      (byte) 185,
      (byte) 199,
      (byte) 40,
      (byte) 131,
      (byte) 162,
      (byte) 163,
      (byte) 51,
      (byte) 112 /*0x70*/,
      (byte) 181,
      (byte) 99,
      (byte) 206,
      (byte) 21,
      (byte) 252,
      (byte) 99,
      (byte) 46,
      (byte) 225,
      (byte) 105,
      (byte) 225
    };
    byte[] numArray6 = new byte[19]
    {
      byte.MaxValue,
      (byte) 119,
      (byte) 3,
      (byte) 158,
      (byte) 168,
      (byte) 144 /*0x90*/,
      (byte) 153,
      (byte) 107,
      (byte) 176 /*0xB0*/,
      (byte) 197,
      (byte) 23,
      (byte) 16 /*0x10*/,
      (byte) 116,
      (byte) 56,
      (byte) 157,
      (byte) 200,
      (byte) 141,
      (byte) 22,
      (byte) 170
    };
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_techcard_19482()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19];
      numArray2[0] = (byte) 179;
      numArray2[1] = (byte) 40;
      numArray2[13] = (byte) 237;
      numArray2[2] = (byte) 72;
      numArray2[14] = (byte) 245;
      numArray2[5] = (byte) 106;
      numArray2[10] = (byte) 47;
      numArray2[8] = (byte) 72;
      numArray2[11] = (byte) 53;
      numArray2[9] = (byte) 128 /*0x80*/;
      numArray2[4] = (byte) 10;
      numArray2[3] = (byte) 8;
      numArray2[12] = (byte) 200;
      numArray2[6] = (byte) 13;
      numArray2[7] = (byte) 118;
      numArray2[15] = (byte) 17;
      numArray2[16 /*0x10*/] = (byte) 227;
      numArray2[17] = (byte) 38;
      numArray2[18] = (byte) 140;
      byte[] numArray3 = new byte[19]
      {
        (byte) 19,
        (byte) 229,
        (byte) 33,
        (byte) 41,
        (byte) 241,
        (byte) 167,
        (byte) 36,
        (byte) 208 /*0xD0*/,
        (byte) 67,
        (byte) 50,
        (byte) 107,
        (byte) 35,
        (byte) 210,
        (byte) 243,
        (byte) 77,
        (byte) 36,
        (byte) 52,
        (byte) 12,
        (byte) 24
      };
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[39];
      byte[] response = new byte[39];
      Array.Copy((Array) sc_19477.sspq, 0, (Array) numArray4, 0, 39);
      key.Query(true, 359, numArray4, response);
      Array.Copy((Array) sc_19477.sspr, 0, (Array) numArray4, 0, 39);
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
    byte[] numArray5 = new byte[19];
    byte[] numArray6 = new byte[19]
    {
      (byte) 243,
      (byte) 108,
      (byte) 67,
      (byte) 9,
      (byte) 94,
      (byte) 54,
      (byte) 222,
      (byte) 105,
      (byte) 50,
      (byte) 179,
      (byte) 52,
      (byte) 86,
      (byte) 63 /*0x3F*/,
      (byte) 147,
      (byte) 174,
      (byte) 99,
      (byte) 218,
      (byte) 120,
      (byte) 26
    };
    byte[] numArray7 = new byte[19];
    numArray7[16 /*0x10*/] = (byte) 116;
    numArray7[1] = (byte) 141;
    numArray7[9] = (byte) 184;
    numArray7[14] = (byte) 94;
    numArray7[4] = (byte) 88;
    numArray7[5] = (byte) 172;
    numArray7[2] = (byte) 200;
    numArray7[7] = (byte) 250;
    numArray7[15] = (byte) 156;
    numArray7[6] = (byte) 58;
    numArray7[10] = (byte) 71;
    numArray7[13] = (byte) 70;
    numArray7[11] = (byte) 52;
    numArray7[12] = (byte) 86;
    numArray7[8] = (byte) 96 /*0x60*/;
    numArray7[0] = (byte) 92;
    numArray7[3] = (byte) 112 /*0x70*/;
    numArray7[17] = (byte) 176 /*0xB0*/;
    numArray7[18] = (byte) 201;
    key.Query(true, 359, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray5[index] ^= numArray7[index];
    byte[] numArray8 = new byte[15];
    byte[] response1 = new byte[15];
    Array.Copy((Array) sc_19477.sspq, 39, (Array) numArray8, 0, 15);
    key.Query(true, 359, numArray8, response1);
    Array.Copy((Array) sc_19477.sspr, 39, (Array) numArray8, 0, 15);
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

  internal static string ssp_techcard_19483()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19]
      {
        (byte) 84,
        (byte) 159,
        (byte) 205,
        (byte) 76,
        byte.MaxValue,
        (byte) 8,
        (byte) 243,
        (byte) 138,
        (byte) 17,
        byte.MaxValue,
        (byte) 80 /*0x50*/,
        (byte) 101,
        (byte) 204,
        (byte) 162,
        (byte) 1,
        (byte) 133,
        (byte) 104,
        (byte) 66,
        (byte) 191
      };
      byte[] numArray3 = new byte[19];
      numArray3[17] = (byte) 111;
      numArray3[4] = (byte) 119;
      numArray3[2] = (byte) 143;
      numArray3[8] = (byte) 229;
      numArray3[10] = (byte) 55;
      numArray3[5] = (byte) 26;
      numArray3[13] = (byte) 242;
      numArray3[7] = (byte) 50;
      numArray3[9] = (byte) 118;
      numArray3[3] = (byte) 14;
      numArray3[12] = (byte) 58;
      numArray3[11] = (byte) 227;
      numArray3[0] = (byte) 223;
      numArray3[1] = (byte) 27;
      numArray3[14] = (byte) 29;
      numArray3[15] = (byte) 158;
      numArray3[16 /*0x10*/] = (byte) 171;
      numArray3[6] = (byte) 70;
      numArray3[18] = (byte) 0;
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[37];
      byte[] response = new byte[37];
      Array.Copy((Array) sc_19477.sspq, 54, (Array) numArray4, 0, 37);
      key.Query(true, 359, numArray4, response);
      Array.Copy((Array) sc_19477.sspr, 54, (Array) numArray4, 0, 37);
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
    byte[] numArray5 = new byte[19];
    byte[] numArray6 = new byte[19]
    {
      (byte) 4,
      (byte) 212,
      (byte) 16 /*0x10*/,
      (byte) 152,
      (byte) 174,
      (byte) 1,
      (byte) 198,
      (byte) 236,
      (byte) 20,
      (byte) 247,
      (byte) 81,
      (byte) 167,
      (byte) 51,
      (byte) 62,
      (byte) 48 /*0x30*/,
      (byte) 225,
      (byte) 6,
      (byte) 245,
      (byte) 61
    };
    byte[] numArray7 = new byte[19]
    {
      (byte) 230,
      (byte) 58,
      (byte) 63 /*0x3F*/,
      (byte) 1,
      (byte) 115,
      (byte) 111,
      (byte) 230,
      (byte) 134,
      (byte) 75,
      (byte) 211,
      (byte) 238,
      (byte) 46,
      (byte) 193,
      (byte) 36,
      (byte) 226,
      (byte) 160 /*0xA0*/,
      (byte) 156,
      (byte) 229,
      (byte) 203
    };
    key.Query(true, 359, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray5[index] ^= numArray7[index];
    byte[] numArray8 = new byte[35];
    byte[] response1 = new byte[35];
    Array.Copy((Array) sc_19477.sspq, 91, (Array) numArray8, 0, 35);
    key.Query(true, 359, numArray8, response1);
    Array.Copy((Array) sc_19477.sspr, 91, (Array) numArray8, 0, 35);
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

  internal static string ssp_techcard_19484()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19]
      {
        (byte) 176 /*0xB0*/,
        (byte) 136,
        (byte) 118,
        (byte) 65,
        (byte) 189,
        (byte) 227,
        (byte) 163,
        (byte) 152,
        (byte) 250,
        (byte) 14,
        (byte) 190,
        (byte) 51,
        (byte) 87,
        (byte) 131,
        (byte) 156,
        (byte) 220,
        (byte) 152,
        (byte) 2,
        (byte) 238
      };
      byte[] numArray3 = new byte[19]
      {
        (byte) 224 /*0xE0*/,
        (byte) 91,
        (byte) 155,
        (byte) 86,
        (byte) 9,
        (byte) 34,
        (byte) 240 /*0xF0*/,
        (byte) 202,
        (byte) 174,
        (byte) 193,
        (byte) 144 /*0x90*/,
        (byte) 98,
        (byte) 181,
        (byte) 93,
        (byte) 62,
        (byte) 45,
        (byte) 253,
        (byte) 237,
        (byte) 50
      };
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[19];
    byte[] numArray5 = new byte[19];
    numArray5[3] = (byte) 244;
    numArray5[16 /*0x10*/] = (byte) 193;
    numArray5[2] = (byte) 107;
    numArray5[4] = (byte) 62;
    numArray5[11] = (byte) 0;
    numArray5[1] = (byte) 180;
    numArray5[6] = (byte) 32 /*0x20*/;
    numArray5[7] = (byte) 37;
    numArray5[8] = (byte) 173;
    numArray5[17] = (byte) 27;
    numArray5[10] = (byte) 24;
    numArray5[12] = (byte) 193;
    numArray5[13] = (byte) 24;
    numArray5[15] = (byte) 107;
    numArray5[0] = (byte) 194;
    numArray5[9] = (byte) 138;
    numArray5[5] = (byte) 9;
    numArray5[14] = (byte) 222;
    numArray5[18] = (byte) 202;
    byte[] numArray6 = new byte[19]
    {
      (byte) 232,
      (byte) 235,
      (byte) 159,
      (byte) 214,
      (byte) 79,
      (byte) 243,
      (byte) 162,
      (byte) 163,
      (byte) 16 /*0x10*/,
      (byte) 44,
      (byte) 212,
      (byte) 182,
      (byte) 94,
      (byte) 130,
      (byte) 157,
      (byte) 88,
      (byte) 215,
      (byte) 225,
      byte.MaxValue
    };
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_techcard_19485()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19]
      {
        (byte) 97,
        (byte) 51,
        (byte) 26,
        (byte) 86,
        (byte) 193,
        (byte) 132,
        (byte) 253,
        (byte) 45,
        (byte) 65,
        (byte) 210,
        (byte) 66,
        (byte) 125,
        (byte) 227,
        (byte) 26,
        (byte) 215,
        (byte) 95,
        (byte) 233,
        (byte) 236,
        (byte) 0
      };
      byte[] numArray3 = new byte[19]
      {
        (byte) 11,
        (byte) 42,
        (byte) 28,
        (byte) 42,
        (byte) 41,
        (byte) 19,
        (byte) 196,
        (byte) 91,
        (byte) 213,
        (byte) 173,
        (byte) 189,
        (byte) 99,
        (byte) 26,
        (byte) 129,
        (byte) 162,
        (byte) 252,
        (byte) 190,
        (byte) 24,
        (byte) 51
      };
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[19];
    byte[] numArray5 = new byte[19]
    {
      (byte) 145,
      (byte) 145,
      (byte) 185,
      (byte) 131,
      (byte) 81,
      (byte) 28,
      (byte) 149,
      (byte) 248,
      (byte) 118,
      (byte) 48 /*0x30*/,
      (byte) 114,
      (byte) 186,
      (byte) 70,
      (byte) 169,
      (byte) 240 /*0xF0*/,
      (byte) 180,
      (byte) 144 /*0x90*/,
      (byte) 132,
      (byte) 19
    };
    byte[] numArray6 = new byte[19];
    numArray6[14] = (byte) 207;
    numArray6[1] = (byte) 145;
    numArray6[2] = (byte) 170;
    numArray6[3] = (byte) 228;
    numArray6[16 /*0x10*/] = (byte) 60;
    numArray6[0] = (byte) 144 /*0x90*/;
    numArray6[17] = (byte) 61;
    numArray6[7] = (byte) 224 /*0xE0*/;
    numArray6[8] = (byte) 211;
    numArray6[10] = (byte) 75;
    numArray6[5] = (byte) 164;
    numArray6[11] = (byte) 191;
    numArray6[9] = (byte) 184;
    numArray6[13] = (byte) 213;
    numArray6[6] = (byte) 17;
    numArray6[15] = (byte) 254;
    numArray6[4] = (byte) 202;
    numArray6[18] = (byte) 53;
    numArray6[12] = (byte) 105;
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
