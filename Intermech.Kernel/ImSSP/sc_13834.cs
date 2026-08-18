// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_13834
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;


namespace ImSSP;

internal static class sc_13834
{
  private static byte[] sspq = new byte[215]
  {
    (byte) 185,
    (byte) 247,
    (byte) 93,
    (byte) 56,
    (byte) 132,
    (byte) 32 /*0x20*/,
    (byte) 38,
    (byte) 36,
    (byte) 223,
    (byte) 17,
    (byte) 4,
    (byte) 142,
    (byte) 70,
    (byte) 87,
    (byte) 179,
    (byte) 74,
    (byte) 222,
    (byte) 94,
    (byte) 12,
    (byte) 129,
    (byte) 22,
    (byte) 113,
    (byte) 9,
    (byte) 100,
    (byte) 68,
    (byte) 77,
    (byte) 54,
    (byte) 176 /*0xB0*/,
    (byte) 148,
    (byte) 200,
    (byte) 172,
    (byte) 211,
    (byte) 202,
    (byte) 58,
    (byte) 167,
    (byte) 217,
    (byte) 74,
    (byte) 58,
    (byte) 239,
    (byte) 162,
    (byte) 28,
    (byte) 129,
    (byte) 127 /*0x7F*/,
    (byte) 192 /*0xC0*/,
    (byte) 61,
    (byte) 187,
    (byte) 124,
    (byte) 223,
    (byte) 78,
    (byte) 187,
    (byte) 146,
    (byte) 106,
    (byte) 30,
    (byte) 137,
    (byte) 113,
    (byte) 70,
    (byte) 232,
    (byte) 147,
    (byte) 1,
    (byte) 199,
    (byte) 174,
    (byte) 126,
    (byte) 27,
    (byte) 62,
    (byte) 86,
    (byte) 230,
    (byte) 154,
    (byte) 31 /*0x1F*/,
    (byte) 229,
    (byte) 147,
    (byte) 4,
    (byte) 200,
    (byte) 92,
    (byte) 168,
    (byte) 55,
    (byte) 121,
    (byte) 83,
    (byte) 9,
    (byte) 232,
    (byte) 133,
    (byte) 154,
    (byte) 245,
    (byte) 8,
    (byte) 242,
    (byte) 250,
    (byte) 79,
    (byte) 214,
    (byte) 101,
    (byte) 219,
    (byte) 45,
    (byte) 174,
    (byte) 213,
    (byte) 233,
    (byte) 120,
    (byte) 130,
    (byte) 176 /*0xB0*/,
    (byte) 158,
    (byte) 84,
    (byte) 198,
    (byte) 109,
    (byte) 135,
    (byte) 214,
    (byte) 2,
    (byte) 149,
    (byte) 71,
    (byte) 176 /*0xB0*/,
    (byte) 67,
    (byte) 71,
    (byte) 205,
    (byte) 249,
    (byte) 86,
    (byte) 220,
    (byte) 129,
    (byte) 139,
    (byte) 80 /*0x50*/,
    (byte) 11,
    (byte) 254,
    (byte) 161,
    (byte) 16 /*0x10*/,
    (byte) 112 /*0x70*/,
    (byte) 187,
    (byte) 170,
    (byte) 180,
    (byte) 35,
    (byte) 135,
    (byte) 74,
    (byte) 141,
    (byte) 251,
    (byte) 148,
    (byte) 97,
    (byte) 98,
    (byte) 2,
    (byte) 92,
    (byte) 42,
    (byte) 96 /*0x60*/,
    (byte) 169,
    (byte) 209,
    (byte) 237,
    (byte) 149,
    (byte) 163,
    (byte) 199,
    (byte) 138,
    (byte) 196,
    (byte) 38,
    (byte) 245,
    (byte) 17,
    (byte) 94,
    (byte) 203,
    (byte) 73,
    (byte) 97,
    (byte) 108,
    (byte) 104,
    (byte) 60,
    (byte) 19,
    (byte) 245,
    (byte) 74,
    (byte) 47,
    (byte) 41,
    (byte) 180,
    (byte) 210,
    (byte) 173,
    (byte) 44,
    (byte) 238,
    (byte) 56,
    (byte) 209,
    (byte) 74,
    (byte) 131,
    (byte) 232,
    (byte) 128 /*0x80*/,
    (byte) 127 /*0x7F*/,
    (byte) 184,
    (byte) 230,
    (byte) 106,
    (byte) 207,
    (byte) 113,
    (byte) 66,
    (byte) 181,
    (byte) 159,
    (byte) 22,
    (byte) 242,
    (byte) 32 /*0x20*/,
    (byte) 93,
    (byte) 141,
    (byte) 48 /*0x30*/,
    (byte) 152,
    (byte) 104,
    (byte) 107,
    (byte) 227,
    (byte) 17,
    (byte) 168,
    (byte) 241,
    (byte) 22,
    (byte) 107,
    (byte) 16 /*0x10*/,
    (byte) 166,
    (byte) 15,
    (byte) 88,
    (byte) 200,
    (byte) 67,
    (byte) 181,
    (byte) 128 /*0x80*/,
    (byte) 67,
    (byte) 69,
    (byte) 206,
    (byte) 23,
    (byte) 27,
    (byte) 222,
    (byte) 155,
    (byte) 38,
    (byte) 149,
    (byte) 47,
    (byte) 81,
    (byte) 131,
    (byte) 51,
    (byte) 27
  };
  private static byte[] sspr = new byte[215]
  {
    (byte) 88,
    (byte) 206,
    (byte) 173,
    (byte) 57,
    (byte) 137,
    (byte) 64 /*0x40*/,
    (byte) 107,
    (byte) 249,
    (byte) 141,
    (byte) 172,
    (byte) 178,
    (byte) 79,
    (byte) 96 /*0x60*/,
    (byte) 65,
    (byte) 182,
    (byte) 182,
    (byte) 45,
    (byte) 234,
    (byte) 82,
    (byte) 131,
    (byte) 61,
    (byte) 133,
    (byte) 31 /*0x1F*/,
    (byte) 247,
    (byte) 153,
    (byte) 214,
    (byte) 138,
    (byte) 20,
    (byte) 79,
    (byte) 218,
    (byte) 173,
    (byte) 245,
    (byte) 200,
    (byte) 208 /*0xD0*/,
    (byte) 14,
    (byte) 164,
    (byte) 80 /*0x50*/,
    (byte) 243,
    (byte) 217,
    (byte) 62,
    (byte) 43,
    (byte) 25,
    (byte) 94,
    (byte) 75,
    (byte) 231,
    (byte) 143,
    (byte) 184,
    (byte) 192 /*0xC0*/,
    (byte) 201,
    (byte) 48 /*0x30*/,
    (byte) 64 /*0x40*/,
    (byte) 87,
    (byte) 86,
    (byte) 170,
    (byte) 36,
    (byte) 155,
    (byte) 73,
    (byte) 140,
    (byte) 254,
    (byte) 98,
    (byte) 9,
    (byte) 142,
    (byte) 52,
    (byte) 122,
    (byte) 2,
    (byte) 253,
    (byte) 173,
    (byte) 245,
    (byte) 185,
    (byte) 194,
    (byte) 79,
    (byte) 199,
    (byte) 128 /*0x80*/,
    (byte) 180,
    (byte) 71,
    (byte) 200,
    (byte) 53,
    (byte) 97,
    (byte) 215,
    (byte) 242,
    (byte) 25,
    (byte) 99,
    (byte) 38,
    (byte) 113,
    (byte) 95,
    (byte) 86,
    (byte) 174,
    (byte) 209,
    (byte) 254,
    (byte) 231,
    (byte) 45,
    (byte) 94,
    (byte) 166,
    (byte) 210,
    (byte) 99,
    (byte) 29,
    (byte) 0,
    (byte) 6,
    (byte) 233,
    (byte) 222,
    (byte) 78,
    (byte) 221,
    (byte) 149,
    (byte) 16 /*0x10*/,
    (byte) 78,
    (byte) 62,
    (byte) 254,
    (byte) 152,
    (byte) 60,
    (byte) 242,
    (byte) 161,
    (byte) 192 /*0xC0*/,
    (byte) 249,
    (byte) 119,
    (byte) 190,
    (byte) 246,
    (byte) 22,
    (byte) 120,
    (byte) 219,
    (byte) 185,
    (byte) 252,
    (byte) 31 /*0x1F*/,
    (byte) 60,
    (byte) 185,
    (byte) 166,
    (byte) 68,
    (byte) 55,
    (byte) 70,
    (byte) 196,
    (byte) 78,
    (byte) 90,
    (byte) 38,
    (byte) 44,
    (byte) 118,
    (byte) 183,
    (byte) 71,
    (byte) 2,
    (byte) 200,
    (byte) 113,
    (byte) 141,
    (byte) 69,
    (byte) 96 /*0x60*/,
    (byte) 172,
    (byte) 79,
    (byte) 63 /*0x3F*/,
    (byte) 10,
    (byte) 192 /*0xC0*/,
    (byte) 120,
    (byte) 4,
    (byte) 210,
    (byte) 130,
    (byte) 198,
    (byte) 74,
    (byte) 151,
    (byte) 6,
    (byte) 17,
    (byte) 225,
    (byte) 40,
    (byte) 194,
    (byte) 40,
    (byte) 177,
    (byte) 244,
    (byte) 40,
    (byte) 14,
    (byte) 246,
    (byte) 128 /*0x80*/,
    (byte) 248,
    (byte) 221,
    (byte) 254,
    (byte) 40,
    (byte) 136,
    (byte) 132,
    (byte) 50,
    (byte) 222,
    (byte) 159,
    (byte) 71,
    (byte) 222,
    (byte) 2,
    (byte) 131,
    (byte) 161,
    (byte) 202,
    (byte) 253,
    (byte) 123,
    (byte) 172,
    (byte) 140,
    (byte) 42,
    (byte) 68,
    (byte) 208 /*0xD0*/,
    (byte) 242,
    (byte) 98,
    (byte) 40,
    (byte) 4,
    (byte) 99,
    (byte) 140,
    (byte) 210,
    (byte) 17,
    (byte) 131,
    (byte) 134,
    (byte) 9,
    (byte) 64 /*0x40*/,
    (byte) 22,
    (byte) 0,
    (byte) 130,
    (byte) 91,
    (byte) 44,
    (byte) 205,
    (byte) 200,
    (byte) 36,
    (byte) 54,
    (byte) 17,
    (byte) 165,
    (byte) 181,
    (byte) 226,
    (byte) 170,
    (byte) 0
  };

  internal static int ssp_appserver_13835(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 153,
      (byte) 104,
      (byte) 167,
      (byte) 254,
      (byte) 138,
      (byte) 237,
      (byte) 29,
      (byte) 252,
      (byte) 30,
      (byte) 143,
      (byte) 185,
      (byte) 161,
      (byte) 58,
      (byte) 6,
      (byte) 190,
      (byte) 79,
      (byte) 216,
      (byte) 110,
      (byte) 205,
      (byte) 55,
      (byte) 218,
      (byte) 50,
      (byte) 56,
      (byte) 248,
      (byte) 241,
      (byte) 99,
      (byte) 184,
      (byte) 98,
      (byte) 158,
      (byte) 53,
      (byte) 153,
      (byte) 18,
      (byte) 146,
      (byte) 54,
      (byte) 9,
      (byte) 81,
      (byte) 52,
      (byte) 111,
      (byte) 81,
      (byte) 234,
      (byte) 37,
      (byte) 83,
      (byte) 208 /*0xD0*/,
      (byte) 237,
      (byte) 102,
      (byte) 137,
      (byte) 184,
      (byte) 133
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 188,
      (byte) 232,
      (byte) 105,
      (byte) 247,
      (byte) 125,
      (byte) 231,
      (byte) 10,
      (byte) 120,
      (byte) 119,
      (byte) 212,
      (byte) 86,
      (byte) 44,
      (byte) 205,
      (byte) 206,
      (byte) 243,
      (byte) 187,
      (byte) 198,
      (byte) 239,
      (byte) 211,
      (byte) 137,
      (byte) 240 /*0xF0*/,
      (byte) 45,
      (byte) 85,
      (byte) 118,
      (byte) 15,
      (byte) 27,
      (byte) 160 /*0xA0*/,
      (byte) 211,
      (byte) 212,
      (byte) 235,
      (byte) 209,
      (byte) 206,
      (byte) 242,
      (byte) 246,
      (byte) 156,
      (byte) 72,
      (byte) 19,
      (byte) 105,
      (byte) 71,
      (byte) 104,
      (byte) 139,
      (byte) 55,
      (byte) 47,
      (byte) 114,
      (byte) 240 /*0xF0*/,
      (byte) 145,
      (byte) 21,
      (byte) 119
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13836(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 70,
      (byte) 192 /*0xC0*/,
      (byte) 102,
      (byte) 47,
      (byte) 175,
      (byte) 12,
      (byte) 39,
      (byte) 52,
      (byte) 186,
      (byte) 93,
      (byte) 232,
      (byte) 246,
      (byte) 167,
      (byte) 14,
      (byte) 3,
      (byte) 253,
      (byte) 108,
      (byte) 27,
      (byte) 105,
      (byte) 29,
      (byte) 227,
      (byte) 18,
      (byte) 135,
      (byte) 98,
      (byte) 74,
      (byte) 145,
      (byte) 104,
      (byte) 171,
      (byte) 51,
      (byte) 161,
      (byte) 226,
      (byte) 146,
      (byte) 154,
      (byte) 40,
      (byte) 76,
      (byte) 118,
      (byte) 163,
      (byte) 34,
      (byte) 132,
      (byte) 98,
      (byte) 41,
      (byte) 182,
      (byte) 107,
      (byte) 245,
      (byte) 231,
      (byte) 247,
      (byte) 190,
      (byte) 122
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[28] = (byte) 137;
    sourceArray2[34] = (byte) 190;
    sourceArray2[33] = (byte) 7;
    sourceArray2[38] = (byte) 96 /*0x60*/;
    sourceArray2[1] = (byte) 84;
    sourceArray2[47] = (byte) 0;
    sourceArray2[6] = (byte) 56;
    sourceArray2[7] = (byte) 165;
    sourceArray2[8] = (byte) 122;
    sourceArray2[39] = (byte) 54;
    sourceArray2[10] = (byte) 200;
    sourceArray2[24] = (byte) 138;
    sourceArray2[12] = (byte) 220;
    sourceArray2[41] = (byte) 167;
    sourceArray2[13] = (byte) 233;
    sourceArray2[15] = (byte) 40;
    sourceArray2[3] = (byte) 182;
    sourceArray2[17] = (byte) 149;
    sourceArray2[9] = (byte) 50;
    sourceArray2[19] = (byte) 111;
    sourceArray2[20] = (byte) 63 /*0x3F*/;
    sourceArray2[21] = (byte) 244;
    sourceArray2[22] = (byte) 147;
    sourceArray2[37] = (byte) 27;
    sourceArray2[45] = (byte) 180;
    sourceArray2[18] = (byte) 3;
    sourceArray2[46] = (byte) 213;
    sourceArray2[27] = (byte) 177;
    sourceArray2[11] = (byte) 123;
    sourceArray2[5] = (byte) 22;
    sourceArray2[30] = (byte) 180;
    sourceArray2[31 /*0x1F*/] = (byte) 22;
    sourceArray2[32 /*0x20*/] = (byte) 76;
    sourceArray2[42] = (byte) 73;
    sourceArray2[36] = (byte) 92;
    sourceArray2[26] = (byte) 85;
    sourceArray2[35] = (byte) 221;
    sourceArray2[23] = (byte) 171;
    sourceArray2[0] = (byte) 90;
    sourceArray2[16 /*0x10*/] = (byte) 10;
    sourceArray2[40] = (byte) 24;
    sourceArray2[25] = (byte) 235;
    sourceArray2[2] = (byte) 23;
    sourceArray2[43] = (byte) 193;
    sourceArray2[44] = (byte) 25;
    sourceArray2[29] = (byte) 240 /*0xF0*/;
    sourceArray2[4] = (byte) 46;
    sourceArray2[14] = (byte) 65;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13837(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 64 /*0x40*/,
      (byte) 183,
      (byte) 38,
      (byte) 46,
      (byte) 11,
      (byte) 9,
      (byte) 50,
      (byte) 236,
      (byte) 217,
      (byte) 141,
      (byte) 166,
      (byte) 136,
      (byte) 146,
      (byte) 75,
      (byte) 180,
      (byte) 132,
      (byte) 87,
      (byte) 205,
      (byte) 58,
      (byte) 253,
      (byte) 161,
      (byte) 91,
      (byte) 170,
      (byte) 80 /*0x50*/,
      (byte) 187,
      (byte) 12,
      (byte) 48 /*0x30*/,
      (byte) 72,
      (byte) 69,
      (byte) 120,
      (byte) 85,
      (byte) 24,
      (byte) 234,
      (byte) 182,
      (byte) 146,
      (byte) 55,
      (byte) 208 /*0xD0*/,
      (byte) 123,
      (byte) 64 /*0x40*/,
      (byte) 55,
      (byte) 38,
      (byte) 204,
      (byte) 73,
      (byte) 138,
      (byte) 150,
      (byte) 22,
      (byte) 170,
      (byte) 27
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 184,
      (byte) 86,
      (byte) 48 /*0x30*/,
      (byte) 83,
      (byte) 136,
      (byte) 191,
      (byte) 147,
      (byte) 95,
      (byte) 210,
      (byte) 5,
      (byte) 62,
      (byte) 105,
      (byte) 211,
      (byte) 31 /*0x1F*/,
      (byte) 107,
      (byte) 229,
      (byte) 0,
      (byte) 170,
      (byte) 231,
      (byte) 254,
      (byte) 47,
      (byte) 35,
      (byte) 19,
      (byte) 241,
      (byte) 106,
      (byte) 236,
      (byte) 71,
      (byte) 61,
      (byte) 25,
      (byte) 192 /*0xC0*/,
      (byte) 183,
      (byte) 103,
      (byte) 53,
      (byte) 115,
      (byte) 115,
      (byte) 130,
      (byte) 70,
      (byte) 46,
      (byte) 92,
      (byte) 74,
      (byte) 80 /*0x50*/,
      (byte) 42,
      (byte) 160 /*0xA0*/,
      (byte) 217,
      (byte) 22,
      (byte) 77,
      (byte) 227,
      (byte) 249
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13838(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 147,
      (byte) 251,
      (byte) 228,
      (byte) 12,
      (byte) 214,
      (byte) 115,
      (byte) 254,
      (byte) 217,
      (byte) 233,
      (byte) 167,
      (byte) 96 /*0x60*/,
      (byte) 56,
      (byte) 142,
      (byte) 157,
      (byte) 220,
      (byte) 184,
      (byte) 125,
      (byte) 180,
      (byte) 219,
      (byte) 141,
      (byte) 2,
      (byte) 214,
      (byte) 212,
      (byte) 17,
      (byte) 32 /*0x20*/,
      (byte) 185,
      (byte) 164,
      (byte) 98,
      (byte) 21,
      (byte) 62,
      (byte) 28,
      (byte) 26,
      (byte) 208 /*0xD0*/,
      (byte) 216,
      (byte) 87,
      (byte) 186,
      (byte) 173,
      (byte) 42,
      (byte) 250,
      (byte) 7,
      (byte) 65,
      (byte) 94,
      (byte) 60,
      (byte) 37,
      (byte) 233,
      (byte) 82,
      (byte) 7,
      (byte) 120
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 49,
      (byte) 199,
      (byte) 207,
      (byte) 180,
      (byte) 243,
      (byte) 191,
      (byte) 149,
      (byte) 18,
      (byte) 203,
      (byte) 5,
      (byte) 225,
      (byte) 221,
      (byte) 215,
      (byte) 234,
      (byte) 44,
      (byte) 43,
      (byte) 27,
      (byte) 230,
      (byte) 159,
      (byte) 238,
      (byte) 75,
      (byte) 221,
      (byte) 40,
      (byte) 99,
      (byte) 38,
      (byte) 24,
      (byte) 179,
      (byte) 22,
      (byte) 195,
      (byte) 87,
      (byte) 171,
      (byte) 77,
      (byte) 87,
      (byte) 170,
      (byte) 106,
      (byte) 109,
      (byte) 2,
      (byte) 100,
      (byte) 229,
      (byte) 116,
      (byte) 127 /*0x7F*/,
      (byte) 104,
      (byte) 213,
      (byte) 84,
      (byte) 225,
      (byte) 10,
      (byte) 70,
      (byte) 60
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13839(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 247,
      (byte) 60,
      (byte) 189,
      (byte) 38,
      (byte) 210,
      (byte) 236,
      (byte) 28,
      (byte) 207,
      (byte) 156,
      (byte) 196,
      (byte) 171,
      (byte) 138,
      (byte) 69,
      (byte) 103,
      (byte) 181,
      (byte) 70,
      (byte) 33,
      (byte) 11,
      (byte) 83,
      (byte) 185,
      (byte) 212,
      (byte) 75,
      (byte) 164,
      (byte) 153,
      (byte) 174,
      (byte) 40,
      (byte) 205,
      (byte) 177,
      (byte) 43,
      (byte) 147,
      (byte) 178,
      (byte) 177,
      (byte) 167,
      (byte) 153,
      (byte) 217,
      (byte) 156,
      (byte) 153,
      (byte) 148,
      (byte) 218,
      (byte) 62,
      (byte) 222,
      (byte) 56,
      (byte) 164,
      (byte) 154,
      (byte) 145,
      (byte) 155,
      (byte) 163,
      (byte) 235
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[0] = (byte) 112 /*0x70*/;
    sourceArray2[1] = (byte) 202;
    sourceArray2[14] = (byte) 126;
    sourceArray2[3] = (byte) 205;
    sourceArray2[44] = (byte) 59;
    sourceArray2[5] = (byte) 226;
    sourceArray2[16 /*0x10*/] = (byte) 235;
    sourceArray2[27] = (byte) 153;
    sourceArray2[24] = (byte) 55;
    sourceArray2[15] = (byte) 134;
    sourceArray2[36] = (byte) 156;
    sourceArray2[11] = (byte) 104;
    sourceArray2[46] = (byte) 26;
    sourceArray2[13] = (byte) 160 /*0xA0*/;
    sourceArray2[2] = (byte) 172;
    sourceArray2[4] = (byte) 223;
    sourceArray2[6] = (byte) 212;
    sourceArray2[45] = (byte) 44;
    sourceArray2[18] = (byte) 106;
    sourceArray2[19] = (byte) 187;
    sourceArray2[9] = (byte) 205;
    sourceArray2[21] = (byte) 233;
    sourceArray2[26] = (byte) 149;
    sourceArray2[23] = (byte) 180;
    sourceArray2[28] = (byte) 12;
    sourceArray2[25] = (byte) 23;
    sourceArray2[17] = (byte) 248;
    sourceArray2[8] = (byte) 204;
    sourceArray2[10] = (byte) 115;
    sourceArray2[29] = (byte) 229;
    sourceArray2[30] = (byte) 209;
    sourceArray2[31 /*0x1F*/] = (byte) 64 /*0x40*/;
    sourceArray2[32 /*0x20*/] = (byte) 116;
    sourceArray2[33] = (byte) 231;
    sourceArray2[34] = (byte) 75;
    sourceArray2[35] = (byte) 201;
    sourceArray2[7] = (byte) 204;
    sourceArray2[37] = (byte) 229;
    sourceArray2[38] = (byte) 153;
    sourceArray2[39] = (byte) 63 /*0x3F*/;
    sourceArray2[43] = (byte) 116;
    sourceArray2[41] = (byte) 93;
    sourceArray2[42] = (byte) 225;
    sourceArray2[12] = (byte) 160 /*0xA0*/;
    sourceArray2[20] = (byte) 217;
    sourceArray2[40] = (byte) 168;
    sourceArray2[22] = (byte) 229;
    sourceArray2[47] = (byte) 220;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[12];
    byte[] response2 = new byte[12];
    Array.Copy((Array) sc_13834.sspq, 0, (Array) numArray2, 0, 12);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13834.sspr, 0, (Array) numArray2, 0, 12);
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

  internal static int ssp_appserver_13840(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[1] = (byte) 60;
    sourceArray1[5] = (byte) 181;
    sourceArray1[44] = (byte) 34;
    sourceArray1[3] = (byte) 169;
    sourceArray1[8] = (byte) 191;
    sourceArray1[29] = (byte) 219;
    sourceArray1[6] = (byte) 129;
    sourceArray1[18] = (byte) 129;
    sourceArray1[26] = (byte) 52;
    sourceArray1[17] = (byte) 11;
    sourceArray1[10] = (byte) 22;
    sourceArray1[11] = (byte) 55;
    sourceArray1[12] = (byte) 113;
    sourceArray1[35] = (byte) 226;
    sourceArray1[45] = (byte) 77;
    sourceArray1[20] = (byte) 89;
    sourceArray1[16 /*0x10*/] = (byte) 6;
    sourceArray1[36] = (byte) 104;
    sourceArray1[33] = (byte) 182;
    sourceArray1[19] = (byte) 113;
    sourceArray1[7] = (byte) 245;
    sourceArray1[21] = (byte) 142;
    sourceArray1[22] = (byte) 132;
    sourceArray1[23] = (byte) 38;
    sourceArray1[24] = (byte) 117;
    sourceArray1[25] = (byte) 198;
    sourceArray1[14] = (byte) 245;
    sourceArray1[39] = (byte) 211;
    sourceArray1[28] = (byte) 14;
    sourceArray1[42] = (byte) 55;
    sourceArray1[38] = (byte) 120;
    sourceArray1[31 /*0x1F*/] = (byte) 116;
    sourceArray1[32 /*0x20*/] = (byte) 89;
    sourceArray1[41] = (byte) 196;
    sourceArray1[34] = (byte) 11;
    sourceArray1[13] = (byte) 0;
    sourceArray1[0] = (byte) 37;
    sourceArray1[2] = (byte) 11;
    sourceArray1[37] = (byte) 235;
    sourceArray1[40] = (byte) 21;
    sourceArray1[9] = (byte) 96 /*0x60*/;
    sourceArray1[15] = (byte) 104;
    sourceArray1[46] = (byte) 174;
    sourceArray1[4] = (byte) 35;
    sourceArray1[27] = (byte) 83;
    sourceArray1[30] = (byte) 40;
    sourceArray1[43] = (byte) 252;
    sourceArray1[47] = (byte) 38;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 214,
      (byte) 252,
      (byte) 147,
      (byte) 10,
      (byte) 102,
      (byte) 109,
      (byte) 115,
      (byte) 80 /*0x50*/,
      (byte) 13,
      (byte) 120,
      (byte) 171,
      (byte) 47,
      (byte) 210,
      (byte) 164,
      (byte) 128 /*0x80*/,
      (byte) 207,
      (byte) 45,
      (byte) 22,
      (byte) 58,
      (byte) 34,
      (byte) 48 /*0x30*/,
      (byte) 213,
      (byte) 106,
      (byte) 130,
      (byte) 242,
      (byte) 118,
      (byte) 233,
      (byte) 28,
      (byte) 253,
      (byte) 170,
      (byte) 99,
      (byte) 83,
      (byte) 57,
      (byte) 36,
      (byte) 48 /*0x30*/,
      (byte) 41,
      (byte) 211,
      (byte) 10,
      (byte) 91,
      (byte) 192 /*0xC0*/,
      (byte) 240 /*0xF0*/,
      (byte) 162,
      (byte) 97,
      (byte) 92,
      (byte) 95,
      (byte) 5,
      (byte) 67,
      (byte) 11
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13841(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[20] = (byte) 46;
    sourceArray1[1] = (byte) 49;
    sourceArray1[2] = (byte) 27;
    sourceArray1[3] = (byte) 119;
    sourceArray1[4] = (byte) 239;
    sourceArray1[14] = (byte) 114;
    sourceArray1[6] = (byte) 95;
    sourceArray1[7] = (byte) 78;
    sourceArray1[17] = (byte) 19;
    sourceArray1[40] = (byte) 8;
    sourceArray1[38] = (byte) 225;
    sourceArray1[11] = (byte) 224 /*0xE0*/;
    sourceArray1[28] = (byte) 116;
    sourceArray1[13] = (byte) 69;
    sourceArray1[32 /*0x20*/] = (byte) 130;
    sourceArray1[12] = (byte) 12;
    sourceArray1[16 /*0x10*/] = (byte) 79;
    sourceArray1[46] = (byte) 171;
    sourceArray1[18] = (byte) 7;
    sourceArray1[19] = (byte) 57;
    sourceArray1[36] = (byte) 9;
    sourceArray1[10] = (byte) 47;
    sourceArray1[21] = (byte) 15;
    sourceArray1[41] = (byte) 96 /*0x60*/;
    sourceArray1[24] = (byte) 123;
    sourceArray1[25] = (byte) 99;
    sourceArray1[26] = (byte) 120;
    sourceArray1[44] = (byte) 250;
    sourceArray1[45] = (byte) 163;
    sourceArray1[29] = (byte) 184;
    sourceArray1[30] = (byte) 4;
    sourceArray1[31 /*0x1F*/] = (byte) 194;
    sourceArray1[27] = (byte) 91;
    sourceArray1[39] = (byte) 202;
    sourceArray1[22] = (byte) 128 /*0x80*/;
    sourceArray1[23] = (byte) 232;
    sourceArray1[9] = (byte) 63 /*0x3F*/;
    sourceArray1[37] = (byte) 32 /*0x20*/;
    sourceArray1[8] = (byte) 118;
    sourceArray1[35] = (byte) 220;
    sourceArray1[5] = (byte) 101;
    sourceArray1[34] = (byte) 35;
    sourceArray1[42] = (byte) 52;
    sourceArray1[43] = (byte) 52;
    sourceArray1[15] = (byte) 9;
    sourceArray1[33] = (byte) 184;
    sourceArray1[0] = (byte) 244;
    sourceArray1[47] = (byte) 213;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 198,
      (byte) 198,
      (byte) 137,
      (byte) 69,
      (byte) 129,
      (byte) 215,
      (byte) 129,
      (byte) 175,
      (byte) 27,
      (byte) 10,
      (byte) 88,
      (byte) 51,
      (byte) 36,
      (byte) 125,
      (byte) 135,
      (byte) 12,
      (byte) 22,
      (byte) 204,
      (byte) 240 /*0xF0*/,
      (byte) 52,
      (byte) 115,
      (byte) 72,
      (byte) 201,
      (byte) 22,
      (byte) 150,
      (byte) 44,
      (byte) 194,
      (byte) 226,
      (byte) 165,
      (byte) 176 /*0xB0*/,
      (byte) 100,
      (byte) 124,
      (byte) 176 /*0xB0*/,
      (byte) 2,
      (byte) 83,
      (byte) 71,
      (byte) 191,
      (byte) 97,
      (byte) 144 /*0x90*/,
      (byte) 134,
      (byte) 211,
      (byte) 229,
      (byte) 239,
      (byte) 23,
      (byte) 105,
      (byte) 230,
      (byte) 45,
      (byte) 226
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[13];
    byte[] response2 = new byte[13];
    Array.Copy((Array) sc_13834.sspq, 12, (Array) numArray2, 0, 13);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13834.sspr, 12, (Array) numArray2, 0, 13);
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

  internal static int ssp_appserver_13842(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 74,
      (byte) 136,
      (byte) 67,
      (byte) 146,
      (byte) 117,
      (byte) 104,
      (byte) 34,
      (byte) 219,
      (byte) 228,
      (byte) 106,
      (byte) 40,
      (byte) 10,
      (byte) 219,
      (byte) 94,
      (byte) 45,
      (byte) 72,
      (byte) 99,
      (byte) 83,
      (byte) 97,
      (byte) 145,
      (byte) 97,
      (byte) 239,
      (byte) 49,
      (byte) 184,
      (byte) 205,
      (byte) 185,
      (byte) 131,
      (byte) 36,
      (byte) 6,
      (byte) 52,
      (byte) 198,
      (byte) 146,
      (byte) 1,
      (byte) 132,
      (byte) 148,
      (byte) 124,
      (byte) 66,
      (byte) 229,
      (byte) 131,
      (byte) 36,
      (byte) 35,
      (byte) 123,
      (byte) 207,
      (byte) 223,
      (byte) 181,
      (byte) 83,
      (byte) 48 /*0x30*/,
      (byte) 35
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[16 /*0x10*/] = (byte) 109;
    sourceArray2[1] = (byte) 34;
    sourceArray2[2] = (byte) 101;
    sourceArray2[38] = (byte) 133;
    sourceArray2[22] = (byte) 244;
    sourceArray2[3] = (byte) 94;
    sourceArray2[6] = (byte) 166;
    sourceArray2[7] = (byte) 161;
    sourceArray2[39] = (byte) 113;
    sourceArray2[33] = (byte) 157;
    sourceArray2[10] = (byte) 167;
    sourceArray2[11] = (byte) 130;
    sourceArray2[12] = (byte) 102;
    sourceArray2[13] = (byte) 12;
    sourceArray2[14] = (byte) 92;
    sourceArray2[20] = (byte) 157;
    sourceArray2[45] = (byte) 133;
    sourceArray2[24] = (byte) 76;
    sourceArray2[18] = (byte) 3;
    sourceArray2[21] = (byte) 4;
    sourceArray2[47] = (byte) 62;
    sourceArray2[9] = (byte) 23;
    sourceArray2[5] = (byte) 41;
    sourceArray2[23] = (byte) 175;
    sourceArray2[8] = (byte) 72;
    sourceArray2[25] = (byte) 129;
    sourceArray2[19] = (byte) 65;
    sourceArray2[27] = (byte) 81;
    sourceArray2[28] = (byte) 233;
    sourceArray2[37] = (byte) 101;
    sourceArray2[41] = (byte) 219;
    sourceArray2[31 /*0x1F*/] = (byte) 241;
    sourceArray2[32 /*0x20*/] = (byte) 229;
    sourceArray2[26] = byte.MaxValue;
    sourceArray2[34] = (byte) 92;
    sourceArray2[35] = (byte) 74;
    sourceArray2[36] = (byte) 81;
    sourceArray2[40] = (byte) 155;
    sourceArray2[17] = (byte) 234;
    sourceArray2[30] = (byte) 231;
    sourceArray2[29] = (byte) 41;
    sourceArray2[0] = (byte) 245;
    sourceArray2[42] = (byte) 172;
    sourceArray2[43] = (byte) 87;
    sourceArray2[44] = (byte) 42;
    sourceArray2[4] = (byte) 21;
    sourceArray2[46] = (byte) 251;
    sourceArray2[15] = (byte) 75;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13843(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 217,
      (byte) 89,
      (byte) 56,
      (byte) 34,
      (byte) 40,
      (byte) 66,
      (byte) 166,
      (byte) 35,
      (byte) 138,
      (byte) 58,
      (byte) 5,
      (byte) 135,
      (byte) 75,
      (byte) 50,
      (byte) 133,
      (byte) 195,
      (byte) 248,
      (byte) 33,
      (byte) 238,
      (byte) 232,
      (byte) 153,
      (byte) 68,
      (byte) 144 /*0x90*/,
      (byte) 139,
      (byte) 78,
      (byte) 93,
      (byte) 7,
      (byte) 254,
      (byte) 73,
      (byte) 64 /*0x40*/,
      (byte) 0,
      (byte) 21,
      (byte) 228,
      (byte) 46,
      (byte) 176 /*0xB0*/,
      (byte) 6,
      (byte) 2,
      (byte) 186,
      (byte) 101,
      (byte) 114,
      (byte) 8,
      (byte) 75,
      (byte) 136,
      (byte) 254,
      (byte) 32 /*0x20*/,
      (byte) 48 /*0x30*/,
      (byte) 212,
      (byte) 150
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 52,
      (byte) 186,
      (byte) 39,
      (byte) 50,
      (byte) 56,
      (byte) 160 /*0xA0*/,
      (byte) 199,
      (byte) 227,
      (byte) 123,
      (byte) 94,
      (byte) 74,
      (byte) 27,
      (byte) 145,
      (byte) 57,
      (byte) 154,
      (byte) 38,
      (byte) 2,
      (byte) 113,
      (byte) 132,
      (byte) 157,
      (byte) 136,
      (byte) 52,
      (byte) 205,
      (byte) 234,
      (byte) 116,
      (byte) 59,
      (byte) 188,
      (byte) 11,
      (byte) 144 /*0x90*/,
      (byte) 199,
      (byte) 58,
      (byte) 232,
      (byte) 215,
      (byte) 41,
      (byte) 116,
      (byte) 88,
      (byte) 18,
      (byte) 144 /*0x90*/,
      (byte) 87,
      (byte) 253,
      (byte) 242,
      (byte) 194,
      (byte) 45,
      (byte) 211,
      (byte) 172,
      (byte) 148,
      (byte) 182,
      (byte) 59
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[42];
    byte[] response2 = new byte[42];
    Array.Copy((Array) sc_13834.sspq, 25, (Array) numArray2, 0, 42);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13834.sspr, 25, (Array) numArray2, 0, 42);
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

  internal static int ssp_appserver_13844(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 113,
      (byte) 62,
      (byte) 195,
      (byte) 48 /*0x30*/,
      (byte) 141,
      (byte) 183,
      (byte) 20,
      (byte) 56,
      (byte) 160 /*0xA0*/,
      (byte) 111,
      (byte) 67,
      (byte) 209,
      (byte) 86,
      (byte) 98,
      (byte) 217,
      (byte) 53,
      (byte) 65,
      (byte) 181,
      (byte) 65,
      (byte) 201,
      (byte) 97,
      (byte) 241,
      (byte) 182,
      (byte) 57,
      (byte) 40,
      (byte) 244,
      (byte) 158,
      (byte) 102,
      (byte) 65,
      (byte) 106,
      (byte) 86,
      (byte) 32 /*0x20*/,
      (byte) 68,
      (byte) 127 /*0x7F*/,
      (byte) 220,
      (byte) 125,
      (byte) 206,
      (byte) 58,
      (byte) 144 /*0x90*/,
      (byte) 209,
      (byte) 92,
      (byte) 13,
      (byte) 71,
      (byte) 36,
      (byte) 190,
      (byte) 246,
      (byte) 88,
      (byte) 27
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 210,
      (byte) 228,
      (byte) 245,
      (byte) 37,
      (byte) 134,
      (byte) 238,
      (byte) 49,
      (byte) 2,
      (byte) 23,
      (byte) 89,
      (byte) 121,
      (byte) 149,
      (byte) 7,
      (byte) 151,
      (byte) 247,
      (byte) 69,
      (byte) 249,
      (byte) 233,
      (byte) 107,
      (byte) 233,
      (byte) 217,
      (byte) 251,
      (byte) 198,
      (byte) 79,
      (byte) 52,
      (byte) 117,
      (byte) 151,
      (byte) 199,
      (byte) 92,
      (byte) 83,
      (byte) 25,
      (byte) 49,
      (byte) 110,
      (byte) 100,
      (byte) 32 /*0x20*/,
      (byte) 91,
      (byte) 80 /*0x50*/,
      (byte) 38,
      (byte) 254,
      (byte) 215,
      (byte) 102,
      (byte) 22,
      (byte) 129,
      (byte) 3,
      (byte) 48 /*0x30*/,
      (byte) 24,
      (byte) 150,
      (byte) 125
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13845(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 110,
      (byte) 254,
      (byte) 128 /*0x80*/,
      (byte) 136,
      (byte) 138,
      (byte) 8,
      (byte) 152,
      (byte) 25,
      (byte) 174,
      (byte) 247,
      (byte) 54,
      (byte) 24,
      (byte) 34,
      (byte) 141,
      (byte) 214,
      (byte) 133,
      (byte) 91,
      (byte) 102,
      (byte) 7,
      (byte) 36,
      (byte) 111,
      (byte) 108,
      (byte) 84,
      (byte) 169,
      (byte) 145,
      (byte) 123,
      (byte) 50,
      (byte) 156,
      (byte) 130,
      (byte) 35,
      (byte) 56,
      (byte) 86,
      (byte) 37,
      (byte) 199,
      (byte) 213,
      (byte) 129,
      (byte) 92,
      (byte) 245,
      (byte) 68,
      (byte) 50,
      (byte) 248,
      (byte) 184,
      (byte) 174,
      (byte) 21,
      (byte) 17,
      (byte) 12,
      (byte) 156,
      (byte) 105
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 109,
      (byte) 37,
      (byte) 157,
      (byte) 203,
      (byte) 173,
      (byte) 67,
      (byte) 204,
      (byte) 173,
      (byte) 131,
      (byte) 81,
      (byte) 212,
      (byte) 65,
      (byte) 72,
      (byte) 17,
      (byte) 163,
      (byte) 100,
      (byte) 136,
      (byte) 249,
      (byte) 166,
      (byte) 63 /*0x3F*/,
      (byte) 43,
      (byte) 48 /*0x30*/,
      (byte) 105,
      (byte) 92,
      (byte) 35,
      (byte) 114,
      byte.MaxValue,
      (byte) 40,
      (byte) 114,
      (byte) 229,
      (byte) 1,
      (byte) 226,
      (byte) 175,
      (byte) 13,
      (byte) 121,
      (byte) 172,
      (byte) 217,
      (byte) 28,
      (byte) 124,
      (byte) 45,
      (byte) 98,
      (byte) 230,
      (byte) 70,
      (byte) 104,
      (byte) 214,
      (byte) 208 /*0xD0*/,
      (byte) 248,
      (byte) 57
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13846(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 12,
      (byte) 233,
      (byte) 1,
      (byte) 170,
      (byte) 214,
      (byte) 246,
      (byte) 14,
      (byte) 213,
      (byte) 252,
      (byte) 150,
      (byte) 138,
      (byte) 191,
      (byte) 224 /*0xE0*/,
      (byte) 242,
      (byte) 90,
      (byte) 183,
      (byte) 85,
      (byte) 41,
      (byte) 102,
      (byte) 34,
      (byte) 212,
      (byte) 44,
      (byte) 183,
      (byte) 189,
      (byte) 63 /*0x3F*/,
      (byte) 152,
      (byte) 133,
      (byte) 223,
      (byte) 200,
      (byte) 27,
      (byte) 236,
      (byte) 61,
      (byte) 151,
      (byte) 73,
      (byte) 25,
      (byte) 222,
      (byte) 211,
      (byte) 214,
      (byte) 133,
      (byte) 254,
      (byte) 53,
      (byte) 92,
      (byte) 251,
      (byte) 33,
      (byte) 36,
      (byte) 86,
      (byte) 49,
      (byte) 25
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 146,
      (byte) 159,
      (byte) 59,
      (byte) 38,
      (byte) 249,
      (byte) 123,
      (byte) 97,
      (byte) 236,
      (byte) 14,
      byte.MaxValue,
      (byte) 91,
      (byte) 166,
      (byte) 107,
      (byte) 224 /*0xE0*/,
      (byte) 29,
      (byte) 149,
      (byte) 139,
      (byte) 18,
      (byte) 129,
      (byte) 136,
      (byte) 2,
      (byte) 142,
      (byte) 144 /*0x90*/,
      (byte) 98,
      (byte) 144 /*0x90*/,
      (byte) 143,
      (byte) 177,
      (byte) 162,
      (byte) 66,
      (byte) 220,
      (byte) 87,
      (byte) 213,
      (byte) 109,
      (byte) 238,
      (byte) 200,
      (byte) 22,
      (byte) 82,
      (byte) 138,
      (byte) 0,
      (byte) 217,
      (byte) 211,
      (byte) 51,
      (byte) 181,
      (byte) 156,
      (byte) 202,
      (byte) 234,
      (byte) 187,
      (byte) 223
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13847(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 127 /*0x7F*/,
      (byte) 56,
      (byte) 223,
      (byte) 49,
      (byte) 68,
      (byte) 106,
      (byte) 62,
      (byte) 113,
      (byte) 47,
      (byte) 12,
      (byte) 225,
      (byte) 170,
      (byte) 76,
      (byte) 39,
      (byte) 150,
      (byte) 217,
      (byte) 124,
      (byte) 227,
      (byte) 126,
      (byte) 163,
      (byte) 103,
      (byte) 236,
      (byte) 120,
      (byte) 49,
      (byte) 149,
      (byte) 152,
      (byte) 232,
      (byte) 103,
      (byte) 107,
      (byte) 157,
      (byte) 22,
      (byte) 239,
      (byte) 126,
      (byte) 188,
      (byte) 23,
      (byte) 17,
      (byte) 57,
      (byte) 172,
      (byte) 143,
      (byte) 107,
      (byte) 70,
      (byte) 141,
      (byte) 6,
      (byte) 79,
      (byte) 26,
      (byte) 119,
      (byte) 215,
      (byte) 46
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 179,
      (byte) 211,
      (byte) 111,
      (byte) 181,
      (byte) 77,
      (byte) 53,
      (byte) 104,
      (byte) 137,
      (byte) 229,
      (byte) 192 /*0xC0*/,
      (byte) 58,
      (byte) 215,
      (byte) 167,
      (byte) 25,
      (byte) 172,
      (byte) 28,
      (byte) 63 /*0x3F*/,
      (byte) 49,
      (byte) 215,
      (byte) 200,
      (byte) 175,
      (byte) 203,
      (byte) 213,
      (byte) 84,
      (byte) 184,
      (byte) 195,
      (byte) 61,
      (byte) 13,
      (byte) 17,
      (byte) 45,
      (byte) 213,
      (byte) 170,
      (byte) 250,
      (byte) 167,
      (byte) 250,
      (byte) 13,
      (byte) 246,
      (byte) 99,
      (byte) 225,
      (byte) 189,
      (byte) 213,
      (byte) 55,
      (byte) 33,
      (byte) 207,
      (byte) 59,
      (byte) 1,
      (byte) 62,
      (byte) 133
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13848(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[11] = (byte) 246;
    sourceArray1[1] = (byte) 24;
    sourceArray1[2] = (byte) 147;
    sourceArray1[40] = (byte) 228;
    sourceArray1[31 /*0x1F*/] = (byte) 217;
    sourceArray1[19] = (byte) 83;
    sourceArray1[16 /*0x10*/] = (byte) 133;
    sourceArray1[46] = (byte) 40;
    sourceArray1[35] = (byte) 163;
    sourceArray1[9] = (byte) 5;
    sourceArray1[10] = (byte) 145;
    sourceArray1[25] = (byte) 22;
    sourceArray1[20] = (byte) 18;
    sourceArray1[13] = (byte) 43;
    sourceArray1[0] = (byte) 201;
    sourceArray1[7] = (byte) 130;
    sourceArray1[45] = (byte) 89;
    sourceArray1[17] = (byte) 159;
    sourceArray1[18] = (byte) 208 /*0xD0*/;
    sourceArray1[12] = (byte) 241;
    sourceArray1[15] = (byte) 16 /*0x10*/;
    sourceArray1[21] = (byte) 92;
    sourceArray1[22] = (byte) 218;
    sourceArray1[3] = (byte) 191;
    sourceArray1[37] = (byte) 81;
    sourceArray1[5] = (byte) 155;
    sourceArray1[26] = (byte) 30;
    sourceArray1[32 /*0x20*/] = (byte) 72;
    sourceArray1[41] = (byte) 233;
    sourceArray1[29] = (byte) 204;
    sourceArray1[30] = (byte) 43;
    sourceArray1[28] = (byte) 186;
    sourceArray1[4] = (byte) 174;
    sourceArray1[33] = (byte) 66;
    sourceArray1[34] = (byte) 180;
    sourceArray1[8] = (byte) 215;
    sourceArray1[36] = (byte) 132;
    sourceArray1[6] = (byte) 171;
    sourceArray1[38] = (byte) 139;
    sourceArray1[39] = (byte) 16 /*0x10*/;
    sourceArray1[24] = (byte) 40;
    sourceArray1[23] = (byte) 229;
    sourceArray1[42] = (byte) 111;
    sourceArray1[43] = (byte) 135;
    sourceArray1[44] = (byte) 116;
    sourceArray1[14] = (byte) 144 /*0x90*/;
    sourceArray1[27] = (byte) 193;
    sourceArray1[47] = (byte) 75;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[8] = (byte) 74;
    sourceArray2[1] = (byte) 206;
    sourceArray2[2] = (byte) 8;
    sourceArray2[3] = (byte) 203;
    sourceArray2[40] = (byte) 252;
    sourceArray2[29] = (byte) 84;
    sourceArray2[24] = (byte) 46;
    sourceArray2[7] = (byte) 150;
    sourceArray2[23] = (byte) 112 /*0x70*/;
    sourceArray2[9] = (byte) 210;
    sourceArray2[10] = (byte) 71;
    sourceArray2[27] = (byte) 149;
    sourceArray2[35] = (byte) 55;
    sourceArray2[39] = (byte) 45;
    sourceArray2[34] = (byte) 38;
    sourceArray2[15] = (byte) 75;
    sourceArray2[16 /*0x10*/] = (byte) 107;
    sourceArray2[26] = (byte) 39;
    sourceArray2[18] = (byte) 111;
    sourceArray2[43] = (byte) 54;
    sourceArray2[21] = (byte) 164;
    sourceArray2[4] = (byte) 251;
    sourceArray2[22] = (byte) 11;
    sourceArray2[37] = (byte) 219;
    sourceArray2[5] = (byte) 184;
    sourceArray2[25] = (byte) 81;
    sourceArray2[38] = (byte) 68;
    sourceArray2[0] = (byte) 68;
    sourceArray2[28] = (byte) 2;
    sourceArray2[20] = (byte) 132;
    sourceArray2[44] = (byte) 71;
    sourceArray2[31 /*0x1F*/] = (byte) 41;
    sourceArray2[32 /*0x20*/] = (byte) 144 /*0x90*/;
    sourceArray2[33] = (byte) 189;
    sourceArray2[19] = (byte) 143;
    sourceArray2[6] = (byte) 65;
    sourceArray2[36] = (byte) 34;
    sourceArray2[41] = (byte) 137;
    sourceArray2[30] = (byte) 11;
    sourceArray2[11] = (byte) 70;
    sourceArray2[12] = (byte) 86;
    sourceArray2[17] = (byte) 150;
    sourceArray2[42] = (byte) 30;
    sourceArray2[13] = (byte) 223;
    sourceArray2[14] = (byte) 233;
    sourceArray2[45] = (byte) 157;
    sourceArray2[46] = (byte) 213;
    sourceArray2[47] = (byte) 7;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13849(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 233,
      (byte) 137,
      (byte) 246,
      (byte) 17,
      (byte) 93,
      (byte) 232,
      (byte) 202,
      (byte) 116,
      (byte) 163,
      (byte) 212,
      (byte) 222,
      (byte) 103,
      (byte) 110,
      (byte) 6,
      (byte) 152,
      (byte) 105,
      (byte) 248,
      (byte) 71,
      (byte) 133,
      (byte) 165,
      (byte) 53,
      (byte) 170,
      (byte) 82,
      (byte) 34,
      (byte) 124,
      (byte) 83,
      (byte) 34,
      (byte) 33,
      (byte) 205,
      (byte) 105,
      (byte) 13,
      (byte) 24,
      (byte) 136,
      (byte) 212,
      (byte) 67,
      (byte) 234,
      (byte) 16 /*0x10*/,
      (byte) 118,
      (byte) 70,
      (byte) 205,
      (byte) 88,
      (byte) 254,
      (byte) 91,
      (byte) 73,
      (byte) 42,
      (byte) 111,
      (byte) 142,
      (byte) 170
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 151,
      (byte) 126,
      (byte) 89,
      (byte) 223,
      (byte) 134,
      (byte) 99,
      (byte) 72,
      (byte) 200,
      (byte) 126,
      (byte) 254,
      (byte) 79,
      (byte) 55,
      (byte) 97,
      (byte) 241,
      (byte) 64 /*0x40*/,
      (byte) 226,
      (byte) 233,
      (byte) 239,
      (byte) 248,
      (byte) 97,
      (byte) 31 /*0x1F*/,
      (byte) 40,
      (byte) 200,
      (byte) 121,
      (byte) 92,
      (byte) 118,
      (byte) 185,
      (byte) 12,
      (byte) 19,
      (byte) 34,
      (byte) 172,
      (byte) 116,
      (byte) 214,
      (byte) 142,
      (byte) 25,
      (byte) 180,
      (byte) 18,
      (byte) 112 /*0x70*/,
      (byte) 78,
      (byte) 216,
      (byte) 40,
      (byte) 143,
      (byte) 179,
      (byte) 231,
      (byte) 46,
      (byte) 121,
      (byte) 38,
      (byte) 136
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13850(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 75,
      (byte) 21,
      (byte) 62,
      (byte) 55,
      (byte) 212,
      (byte) 149,
      (byte) 10,
      (byte) 197,
      (byte) 168,
      (byte) 167,
      (byte) 57,
      (byte) 199,
      (byte) 197,
      (byte) 223,
      (byte) 154,
      (byte) 194,
      (byte) 174,
      (byte) 89,
      (byte) 129,
      (byte) 0,
      (byte) 184,
      (byte) 77,
      (byte) 54,
      (byte) 78,
      (byte) 8,
      (byte) 247,
      (byte) 13,
      (byte) 197,
      (byte) 120,
      (byte) 250,
      (byte) 254,
      (byte) 155,
      (byte) 219,
      (byte) 3,
      (byte) 60,
      (byte) 190,
      (byte) 5,
      (byte) 178,
      (byte) 59,
      (byte) 225,
      (byte) 60,
      (byte) 7,
      (byte) 175,
      (byte) 87,
      (byte) 33,
      (byte) 130,
      (byte) 101,
      (byte) 144 /*0x90*/
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 194,
      (byte) 108,
      (byte) 134,
      (byte) 74,
      (byte) 203,
      (byte) 126,
      (byte) 8,
      (byte) 90,
      (byte) 142,
      (byte) 126,
      (byte) 67,
      (byte) 180,
      (byte) 183,
      (byte) 115,
      (byte) 221,
      (byte) 225,
      (byte) 172,
      (byte) 50,
      (byte) 226,
      (byte) 237,
      (byte) 144 /*0x90*/,
      (byte) 236,
      (byte) 195,
      (byte) 101,
      (byte) 186,
      (byte) 19,
      (byte) 139,
      (byte) 100,
      (byte) 177,
      (byte) 88,
      (byte) 107,
      (byte) 9,
      (byte) 82,
      (byte) 96 /*0x60*/,
      (byte) 194,
      (byte) 189,
      (byte) 203,
      (byte) 13,
      (byte) 236,
      (byte) 214,
      (byte) 58,
      (byte) 191,
      (byte) 197,
      (byte) 93,
      (byte) 103,
      (byte) 143,
      (byte) 182,
      (byte) 239
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13851(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 49,
      (byte) 93,
      (byte) 254,
      (byte) 223,
      (byte) 240 /*0xF0*/,
      (byte) 46,
      (byte) 221,
      (byte) 135,
      (byte) 69,
      (byte) 77,
      (byte) 174,
      (byte) 168,
      (byte) 158,
      (byte) 235,
      (byte) 194,
      (byte) 162,
      (byte) 219,
      (byte) 189,
      (byte) 251,
      (byte) 29,
      (byte) 159,
      (byte) 195,
      (byte) 177,
      (byte) 157,
      (byte) 223,
      (byte) 44,
      (byte) 134,
      (byte) 146,
      (byte) 125,
      (byte) 209,
      (byte) 144 /*0x90*/,
      (byte) 128 /*0x80*/,
      (byte) 138,
      (byte) 78,
      (byte) 170,
      (byte) 166,
      (byte) 62,
      (byte) 223,
      (byte) 20,
      (byte) 132,
      (byte) 94,
      (byte) 89,
      (byte) 247,
      (byte) 185,
      (byte) 1,
      (byte) 174,
      (byte) 131,
      (byte) 150
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[9] = (byte) 27;
    sourceArray2[22] = (byte) 21;
    sourceArray2[13] = (byte) 115;
    sourceArray2[1] = (byte) 29;
    sourceArray2[10] = (byte) 133;
    sourceArray2[37] = (byte) 44;
    sourceArray2[6] = (byte) 176 /*0xB0*/;
    sourceArray2[7] = (byte) 205;
    sourceArray2[40] = (byte) 185;
    sourceArray2[32 /*0x20*/] = (byte) 55;
    sourceArray2[45] = (byte) 208 /*0xD0*/;
    sourceArray2[42] = (byte) 106;
    sourceArray2[12] = (byte) 163;
    sourceArray2[38] = (byte) 84;
    sourceArray2[20] = (byte) 171;
    sourceArray2[17] = (byte) 92;
    sourceArray2[44] = (byte) 39;
    sourceArray2[2] = (byte) 116;
    sourceArray2[18] = (byte) 11;
    sourceArray2[0] = (byte) 26;
    sourceArray2[4] = (byte) 104;
    sourceArray2[21] = (byte) 244;
    sourceArray2[5] = (byte) 245;
    sourceArray2[29] = (byte) 108;
    sourceArray2[24] = (byte) 247;
    sourceArray2[25] = (byte) 19;
    sourceArray2[26] = (byte) 189;
    sourceArray2[27] = (byte) 143;
    sourceArray2[19] = (byte) 105;
    sourceArray2[28] = (byte) 61;
    sourceArray2[30] = (byte) 50;
    sourceArray2[3] = (byte) 250;
    sourceArray2[23] = (byte) 66;
    sourceArray2[33] = (byte) 2;
    sourceArray2[34] = (byte) 55;
    sourceArray2[8] = (byte) 161;
    sourceArray2[36] = (byte) 29;
    sourceArray2[39] = (byte) 24;
    sourceArray2[43] = (byte) 72;
    sourceArray2[15] = (byte) 89;
    sourceArray2[16 /*0x10*/] = (byte) 130;
    sourceArray2[41] = (byte) 192 /*0xC0*/;
    sourceArray2[14] = (byte) 190;
    sourceArray2[35] = (byte) 211;
    sourceArray2[31 /*0x1F*/] = (byte) 78;
    sourceArray2[47] = (byte) 251;
    sourceArray2[46] = (byte) 127 /*0x7F*/;
    sourceArray2[11] = (byte) 197;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13852(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 50,
      (byte) 85,
      (byte) 133,
      (byte) 138,
      (byte) 28,
      (byte) 128 /*0x80*/,
      (byte) 6,
      (byte) 151,
      (byte) 104,
      (byte) 160 /*0xA0*/,
      (byte) 180,
      (byte) 196,
      (byte) 206,
      (byte) 26,
      (byte) 184,
      (byte) 90,
      (byte) 123,
      (byte) 187,
      (byte) 85,
      (byte) 42,
      (byte) 235,
      (byte) 176 /*0xB0*/,
      (byte) 4,
      (byte) 34,
      (byte) 133,
      (byte) 28,
      (byte) 87,
      (byte) 155,
      (byte) 171,
      (byte) 246,
      (byte) 73,
      (byte) 250,
      (byte) 200,
      (byte) 162,
      (byte) 118,
      (byte) 154,
      (byte) 106,
      (byte) 4,
      (byte) 156,
      (byte) 230,
      (byte) 252,
      (byte) 155,
      (byte) 55,
      (byte) 156,
      (byte) 69,
      (byte) 26,
      (byte) 17,
      (byte) 55
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[32 /*0x20*/] = (byte) 103;
    sourceArray2[1] = (byte) 206;
    sourceArray2[15] = (byte) 138;
    sourceArray2[27] = (byte) 99;
    sourceArray2[4] = (byte) 14;
    sourceArray2[5] = (byte) 160 /*0xA0*/;
    sourceArray2[30] = (byte) 168;
    sourceArray2[2] = (byte) 136;
    sourceArray2[7] = (byte) 57;
    sourceArray2[0] = (byte) 28;
    sourceArray2[22] = (byte) 188;
    sourceArray2[11] = (byte) 128 /*0x80*/;
    sourceArray2[12] = (byte) 71;
    sourceArray2[43] = (byte) 202;
    sourceArray2[14] = (byte) 124;
    sourceArray2[41] = (byte) 128 /*0x80*/;
    sourceArray2[16 /*0x10*/] = (byte) 78;
    sourceArray2[6] = (byte) 179;
    sourceArray2[18] = (byte) 238;
    sourceArray2[35] = (byte) 105;
    sourceArray2[20] = (byte) 125;
    sourceArray2[3] = (byte) 179;
    sourceArray2[34] = (byte) 106;
    sourceArray2[23] = (byte) 87;
    sourceArray2[24] = (byte) 3;
    sourceArray2[25] = (byte) 21;
    sourceArray2[26] = (byte) 206;
    sourceArray2[39] = (byte) 54;
    sourceArray2[19] = (byte) 81;
    sourceArray2[29] = (byte) 85;
    sourceArray2[38] = (byte) 21;
    sourceArray2[31 /*0x1F*/] = (byte) 124;
    sourceArray2[9] = (byte) 135;
    sourceArray2[33] = (byte) 57;
    sourceArray2[10] = (byte) 143;
    sourceArray2[40] = (byte) 110;
    sourceArray2[42] = (byte) 68;
    sourceArray2[37] = (byte) 140;
    sourceArray2[8] = (byte) 101;
    sourceArray2[17] = (byte) 191;
    sourceArray2[36] = (byte) 133;
    sourceArray2[21] = (byte) 134;
    sourceArray2[47] = (byte) 100;
    sourceArray2[13] = (byte) 42;
    sourceArray2[44] = (byte) 169;
    sourceArray2[45] = (byte) 16 /*0x10*/;
    sourceArray2[46] = (byte) 245;
    sourceArray2[28] = (byte) 143;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[30];
    byte[] response2 = new byte[30];
    Array.Copy((Array) sc_13834.sspq, 67, (Array) numArray2, 0, 30);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13834.sspr, 67, (Array) numArray2, 0, 30);
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

  internal static int ssp_appserver_13853(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[26] = (byte) 47;
    sourceArray1[19] = (byte) 180;
    sourceArray1[30] = (byte) 135;
    sourceArray1[3] = (byte) 37;
    sourceArray1[4] = (byte) 140;
    sourceArray1[14] = (byte) 129;
    sourceArray1[6] = (byte) 42;
    sourceArray1[13] = (byte) 96 /*0x60*/;
    sourceArray1[8] = (byte) 216;
    sourceArray1[18] = (byte) 157;
    sourceArray1[10] = (byte) 166;
    sourceArray1[5] = (byte) 115;
    sourceArray1[40] = (byte) 208 /*0xD0*/;
    sourceArray1[1] = (byte) 81;
    sourceArray1[42] = (byte) 253;
    sourceArray1[15] = (byte) 100;
    sourceArray1[16 /*0x10*/] = (byte) 167;
    sourceArray1[12] = (byte) 235;
    sourceArray1[27] = (byte) 222;
    sourceArray1[36] = (byte) 78;
    sourceArray1[20] = (byte) 118;
    sourceArray1[17] = (byte) 181;
    sourceArray1[31 /*0x1F*/] = (byte) 219;
    sourceArray1[43] = (byte) 73;
    sourceArray1[44] = (byte) 52;
    sourceArray1[25] = (byte) 229;
    sourceArray1[33] = (byte) 86;
    sourceArray1[9] = (byte) 28;
    sourceArray1[28] = (byte) 228;
    sourceArray1[29] = (byte) 38;
    sourceArray1[22] = (byte) 37;
    sourceArray1[21] = (byte) 110;
    sourceArray1[32 /*0x20*/] = (byte) 177;
    sourceArray1[0] = (byte) 35;
    sourceArray1[34] = (byte) 197;
    sourceArray1[35] = (byte) 51;
    sourceArray1[38] = (byte) 123;
    sourceArray1[37] = (byte) 207;
    sourceArray1[7] = (byte) 198;
    sourceArray1[39] = (byte) 127 /*0x7F*/;
    sourceArray1[23] = (byte) 68;
    sourceArray1[41] = (byte) 136;
    sourceArray1[2] = (byte) 75;
    sourceArray1[45] = (byte) 129;
    sourceArray1[11] = (byte) 171;
    sourceArray1[24] = (byte) 183;
    sourceArray1[46] = (byte) 194;
    sourceArray1[47] = (byte) 116;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 22,
      (byte) 167,
      (byte) 102,
      (byte) 221,
      (byte) 219,
      (byte) 177,
      (byte) 144 /*0x90*/,
      (byte) 14,
      (byte) 239,
      (byte) 175,
      (byte) 159,
      (byte) 88,
      (byte) 0,
      (byte) 134,
      (byte) 197,
      (byte) 205,
      (byte) 38,
      (byte) 46,
      (byte) 63 /*0x3F*/,
      (byte) 225,
      (byte) 252,
      (byte) 27,
      (byte) 155,
      (byte) 71,
      (byte) 70,
      (byte) 22,
      (byte) 198,
      (byte) 31 /*0x1F*/,
      (byte) 9,
      (byte) 22,
      (byte) 159,
      (byte) 253,
      (byte) 92,
      (byte) 245,
      (byte) 49,
      (byte) 101,
      (byte) 141,
      (byte) 56,
      (byte) 214,
      (byte) 66,
      (byte) 62,
      (byte) 99,
      (byte) 106,
      (byte) 177,
      (byte) 146,
      (byte) 1,
      (byte) 6,
      (byte) 116
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13854(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 46,
      (byte) 218,
      (byte) 27,
      (byte) 254,
      (byte) 3,
      (byte) 193,
      (byte) 225,
      (byte) 9,
      (byte) 79,
      (byte) 67,
      (byte) 80 /*0x50*/,
      (byte) 248,
      (byte) 55,
      (byte) 131,
      (byte) 137,
      (byte) 214,
      (byte) 18,
      (byte) 102,
      (byte) 9,
      (byte) 238,
      (byte) 54,
      (byte) 122,
      (byte) 137,
      (byte) 69,
      (byte) 206,
      (byte) 117,
      (byte) 101,
      (byte) 227,
      (byte) 95,
      (byte) 70,
      (byte) 37,
      (byte) 39,
      (byte) 164,
      (byte) 174,
      (byte) 244,
      (byte) 224 /*0xE0*/,
      (byte) 90,
      (byte) 210,
      (byte) 72,
      (byte) 202,
      (byte) 249,
      (byte) 186,
      (byte) 202,
      (byte) 205,
      (byte) 183,
      (byte) 78,
      (byte) 42,
      (byte) 80 /*0x50*/
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 206,
      (byte) 148,
      (byte) 101,
      (byte) 47,
      (byte) 139,
      (byte) 113,
      (byte) 132,
      (byte) 110,
      (byte) 3,
      (byte) 234,
      (byte) 85,
      (byte) 175,
      (byte) 126,
      (byte) 210,
      (byte) 179,
      (byte) 8,
      (byte) 121,
      (byte) 75,
      (byte) 212,
      (byte) 95,
      (byte) 45,
      (byte) 208 /*0xD0*/,
      (byte) 71,
      (byte) 175,
      (byte) 34,
      (byte) 176 /*0xB0*/,
      (byte) 119,
      (byte) 61,
      (byte) 156,
      (byte) 163,
      (byte) 249,
      (byte) 25,
      (byte) 151,
      (byte) 207,
      (byte) 175,
      (byte) 10,
      (byte) 231,
      (byte) 248,
      (byte) 136,
      (byte) 106,
      (byte) 214,
      (byte) 212,
      (byte) 91,
      (byte) 206,
      (byte) 165,
      (byte) 155,
      (byte) 48 /*0x30*/,
      (byte) 240 /*0xF0*/
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13855(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 123,
      (byte) 81,
      (byte) 179,
      (byte) 80 /*0x50*/,
      (byte) 213,
      (byte) 47,
      (byte) 142,
      (byte) 91,
      (byte) 254,
      (byte) 170,
      (byte) 8,
      (byte) 29,
      (byte) 40,
      (byte) 83,
      (byte) 137,
      (byte) 142,
      (byte) 168,
      (byte) 124,
      (byte) 63 /*0x3F*/,
      (byte) 30,
      (byte) 62,
      (byte) 244,
      (byte) 123,
      (byte) 176 /*0xB0*/,
      (byte) 187,
      (byte) 142,
      (byte) 127 /*0x7F*/,
      (byte) 74,
      (byte) 20,
      (byte) 49,
      (byte) 176 /*0xB0*/,
      (byte) 178,
      (byte) 64 /*0x40*/,
      (byte) 124,
      (byte) 40,
      (byte) 92,
      (byte) 65,
      (byte) 144 /*0x90*/,
      (byte) 132,
      (byte) 162,
      (byte) 186,
      (byte) 111,
      (byte) 251,
      (byte) 16 /*0x10*/,
      (byte) 193,
      (byte) 176 /*0xB0*/,
      (byte) 142,
      (byte) 251
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 247,
      (byte) 24,
      (byte) 86,
      (byte) 140,
      (byte) 82,
      (byte) 51,
      (byte) 92,
      (byte) 94,
      (byte) 37,
      (byte) 151,
      (byte) 99,
      (byte) 46,
      (byte) 115,
      (byte) 100,
      (byte) 36,
      (byte) 50,
      (byte) 17,
      (byte) 99,
      (byte) 88,
      (byte) 68,
      (byte) 143,
      (byte) 188,
      (byte) 105,
      (byte) 221,
      (byte) 86,
      (byte) 65,
      (byte) 152,
      (byte) 28,
      (byte) 193,
      (byte) 71,
      (byte) 101,
      (byte) 231,
      (byte) 230,
      (byte) 89,
      (byte) 201,
      (byte) 96 /*0x60*/,
      (byte) 229,
      (byte) 32 /*0x20*/,
      (byte) 5,
      (byte) 229,
      (byte) 120,
      (byte) 192 /*0xC0*/,
      (byte) 106,
      (byte) 207,
      (byte) 88,
      (byte) 91,
      (byte) 67,
      (byte) 239
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[15];
    byte[] response2 = new byte[15];
    Array.Copy((Array) sc_13834.sspq, 97, (Array) numArray2, 0, 15);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13834.sspr, 97, (Array) numArray2, 0, 15);
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

  internal static int ssp_appserver_13856(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 45,
      (byte) 77,
      (byte) 24,
      (byte) 247,
      (byte) 157,
      (byte) 100,
      (byte) 207,
      (byte) 249,
      (byte) 107,
      (byte) 238,
      (byte) 11,
      byte.MaxValue,
      (byte) 132,
      (byte) 169,
      (byte) 82,
      (byte) 39,
      (byte) 126,
      (byte) 93,
      (byte) 121,
      (byte) 14,
      (byte) 161,
      (byte) 134,
      (byte) 62,
      (byte) 251,
      (byte) 60,
      (byte) 242,
      (byte) 118,
      (byte) 39,
      (byte) 45,
      (byte) 23,
      (byte) 107,
      (byte) 211,
      (byte) 225,
      (byte) 208 /*0xD0*/,
      (byte) 116,
      (byte) 116,
      (byte) 241,
      (byte) 58,
      (byte) 183,
      (byte) 98,
      (byte) 158,
      (byte) 141,
      (byte) 107,
      (byte) 20,
      (byte) 138,
      (byte) 71,
      (byte) 43,
      (byte) 154
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 77,
      (byte) 80 /*0x50*/,
      (byte) 225,
      (byte) 166,
      (byte) 137,
      (byte) 2,
      (byte) 81,
      (byte) 26,
      (byte) 152,
      (byte) 208 /*0xD0*/,
      (byte) 10,
      (byte) 65,
      (byte) 33,
      (byte) 9,
      (byte) 238,
      (byte) 155,
      (byte) 133,
      (byte) 80 /*0x50*/,
      (byte) 117,
      (byte) 168,
      (byte) 82,
      (byte) 88,
      (byte) 35,
      (byte) 151,
      (byte) 229,
      (byte) 18,
      (byte) 134,
      (byte) 103,
      (byte) 205,
      (byte) 78,
      (byte) 89,
      (byte) 45,
      (byte) 34,
      (byte) 73,
      (byte) 98,
      (byte) 156,
      (byte) 121,
      (byte) 121,
      (byte) 169,
      (byte) 101,
      (byte) 235,
      (byte) 222,
      (byte) 126,
      (byte) 40,
      (byte) 198,
      (byte) 42,
      (byte) 176 /*0xB0*/,
      (byte) 49
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13857(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[33] = (byte) 112 /*0x70*/;
    sourceArray1[44] = (byte) 22;
    sourceArray1[10] = (byte) 84;
    sourceArray1[27] = (byte) 198;
    sourceArray1[40] = (byte) 13;
    sourceArray1[5] = (byte) 187;
    sourceArray1[36] = (byte) 205;
    sourceArray1[7] = (byte) 237;
    sourceArray1[8] = (byte) 167;
    sourceArray1[9] = (byte) 203;
    sourceArray1[39] = (byte) 126;
    sourceArray1[11] = (byte) 130;
    sourceArray1[12] = (byte) 55;
    sourceArray1[22] = (byte) 20;
    sourceArray1[29] = (byte) 50;
    sourceArray1[15] = (byte) 52;
    sourceArray1[16 /*0x10*/] = (byte) 221;
    sourceArray1[21] = (byte) 112 /*0x70*/;
    sourceArray1[18] = (byte) 68;
    sourceArray1[37] = (byte) 102;
    sourceArray1[20] = (byte) 23;
    sourceArray1[3] = (byte) 14;
    sourceArray1[24] = (byte) 42;
    sourceArray1[0] = (byte) 157;
    sourceArray1[4] = (byte) 28;
    sourceArray1[17] = (byte) 53;
    sourceArray1[46] = (byte) 82;
    sourceArray1[47] = (byte) 42;
    sourceArray1[28] = (byte) 219;
    sourceArray1[41] = (byte) 117;
    sourceArray1[1] = (byte) 136;
    sourceArray1[31 /*0x1F*/] = (byte) 156;
    sourceArray1[32 /*0x20*/] = (byte) 114;
    sourceArray1[23] = (byte) 27;
    sourceArray1[45] = (byte) 145;
    sourceArray1[25] = (byte) 113;
    sourceArray1[35] = (byte) 71;
    sourceArray1[43] = (byte) 194;
    sourceArray1[34] = (byte) 67;
    sourceArray1[14] = (byte) 179;
    sourceArray1[30] = (byte) 135;
    sourceArray1[38] = (byte) 212;
    sourceArray1[42] = (byte) 246;
    sourceArray1[2] = (byte) 167;
    sourceArray1[6] = (byte) 170;
    sourceArray1[13] = (byte) 29;
    sourceArray1[19] = (byte) 165;
    sourceArray1[26] = (byte) 245;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 161,
      (byte) 185,
      (byte) 149,
      (byte) 154,
      (byte) 92,
      (byte) 34,
      (byte) 13,
      (byte) 56,
      (byte) 243,
      (byte) 230,
      (byte) 147,
      (byte) 141,
      (byte) 33,
      (byte) 41,
      (byte) 171,
      (byte) 173,
      (byte) 107,
      (byte) 248,
      (byte) 150,
      (byte) 77,
      (byte) 245,
      (byte) 249,
      (byte) 232,
      (byte) 124,
      (byte) 111,
      (byte) 128 /*0x80*/,
      (byte) 49,
      (byte) 0,
      (byte) 157,
      (byte) 104,
      (byte) 134,
      (byte) 183,
      (byte) 123,
      (byte) 122,
      (byte) 123,
      (byte) 229,
      (byte) 91,
      (byte) 75,
      (byte) 162,
      (byte) 10,
      (byte) 198,
      (byte) 103,
      (byte) 117,
      (byte) 145,
      (byte) 115,
      (byte) 223,
      (byte) 84,
      (byte) 21
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[17];
    byte[] response2 = new byte[17];
    Array.Copy((Array) sc_13834.sspq, 112 /*0x70*/, (Array) numArray2, 0, 17);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13834.sspr, 112 /*0x70*/, (Array) numArray2, 0, 17);
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

  internal static int ssp_appserver_13858(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[34] = (byte) 82;
    sourceArray1[1] = (byte) 81;
    sourceArray1[2] = (byte) 241;
    sourceArray1[3] = (byte) 162;
    sourceArray1[4] = (byte) 195;
    sourceArray1[7] = (byte) 243;
    sourceArray1[25] = (byte) 98;
    sourceArray1[17] = (byte) 122;
    sourceArray1[32 /*0x20*/] = (byte) 199;
    sourceArray1[22] = (byte) 246;
    sourceArray1[10] = (byte) 110;
    sourceArray1[6] = (byte) 38;
    sourceArray1[0] = (byte) 44;
    sourceArray1[8] = (byte) 55;
    sourceArray1[14] = (byte) 164;
    sourceArray1[15] = (byte) 23;
    sourceArray1[16 /*0x10*/] = (byte) 146;
    sourceArray1[11] = (byte) 62;
    sourceArray1[18] = (byte) 68;
    sourceArray1[19] = (byte) 119;
    sourceArray1[20] = (byte) 120;
    sourceArray1[21] = (byte) 6;
    sourceArray1[23] = (byte) 99;
    sourceArray1[36] = (byte) 143;
    sourceArray1[44] = (byte) 76;
    sourceArray1[37] = (byte) 233;
    sourceArray1[26] = (byte) 175;
    sourceArray1[27] = (byte) 185;
    sourceArray1[28] = (byte) 56;
    sourceArray1[39] = (byte) 113;
    sourceArray1[24] = (byte) 122;
    sourceArray1[43] = (byte) 99;
    sourceArray1[45] = (byte) 107;
    sourceArray1[33] = (byte) 39;
    sourceArray1[30] = (byte) 189;
    sourceArray1[35] = (byte) 13;
    sourceArray1[40] = (byte) 215;
    sourceArray1[12] = (byte) 247;
    sourceArray1[38] = (byte) 141;
    sourceArray1[13] = (byte) 210;
    sourceArray1[47] = (byte) 80 /*0x50*/;
    sourceArray1[31 /*0x1F*/] = (byte) 149;
    sourceArray1[42] = (byte) 216;
    sourceArray1[41] = (byte) 174;
    sourceArray1[29] = (byte) 155;
    sourceArray1[9] = (byte) 12;
    sourceArray1[46] = (byte) 188;
    sourceArray1[5] = (byte) 153;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[46] = (byte) 135;
    sourceArray2[19] = (byte) 151;
    sourceArray2[2] = (byte) 204;
    sourceArray2[3] = (byte) 5;
    sourceArray2[8] = (byte) 158;
    sourceArray2[26] = (byte) 183;
    sourceArray2[43] = (byte) 227;
    sourceArray2[7] = (byte) 240 /*0xF0*/;
    sourceArray2[39] = (byte) 135;
    sourceArray2[9] = (byte) 9;
    sourceArray2[36] = (byte) 66;
    sourceArray2[30] = (byte) 139;
    sourceArray2[12] = (byte) 100;
    sourceArray2[13] = (byte) 90;
    sourceArray2[14] = (byte) 60;
    sourceArray2[15] = (byte) 123;
    sourceArray2[16 /*0x10*/] = (byte) 139;
    sourceArray2[17] = (byte) 77;
    sourceArray2[18] = (byte) 210;
    sourceArray2[11] = (byte) 211;
    sourceArray2[37] = (byte) 78;
    sourceArray2[21] = (byte) 8;
    sourceArray2[22] = (byte) 210;
    sourceArray2[6] = (byte) 112 /*0x70*/;
    sourceArray2[45] = (byte) 90;
    sourceArray2[1] = (byte) 30;
    sourceArray2[25] = (byte) 173;
    sourceArray2[32 /*0x20*/] = (byte) 226;
    sourceArray2[29] = (byte) 237;
    sourceArray2[5] = (byte) 155;
    sourceArray2[27] = (byte) 163;
    sourceArray2[42] = (byte) 149;
    sourceArray2[4] = (byte) 93;
    sourceArray2[23] = (byte) 10;
    sourceArray2[34] = (byte) 108;
    sourceArray2[35] = (byte) 203;
    sourceArray2[41] = (byte) 178;
    sourceArray2[0] = (byte) 151;
    sourceArray2[38] = (byte) 22;
    sourceArray2[31 /*0x1F*/] = (byte) 180;
    sourceArray2[28] = (byte) 120;
    sourceArray2[33] = (byte) 113;
    sourceArray2[44] = (byte) 121;
    sourceArray2[20] = (byte) 171;
    sourceArray2[24] = (byte) 9;
    sourceArray2[40] = (byte) 56;
    sourceArray2[10] = (byte) 7;
    sourceArray2[47] = (byte) 78;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13859(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 251,
      (byte) 210,
      (byte) 62,
      (byte) 66,
      (byte) 3,
      (byte) 224 /*0xE0*/,
      (byte) 33,
      (byte) 98,
      (byte) 124,
      (byte) 103,
      (byte) 36,
      (byte) 103,
      (byte) 209,
      (byte) 49,
      (byte) 76,
      (byte) 65,
      (byte) 6,
      (byte) 75,
      (byte) 211,
      byte.MaxValue,
      (byte) 2,
      (byte) 252,
      (byte) 12,
      (byte) 90,
      (byte) 169,
      (byte) 103,
      (byte) 203,
      (byte) 179,
      (byte) 91,
      (byte) 29,
      (byte) 251,
      (byte) 250,
      (byte) 64 /*0x40*/,
      (byte) 79,
      (byte) 80 /*0x50*/,
      (byte) 100,
      (byte) 151,
      (byte) 174,
      (byte) 122,
      (byte) 195,
      (byte) 248,
      (byte) 85,
      (byte) 18,
      (byte) 35,
      (byte) 54,
      (byte) 233,
      (byte) 216,
      (byte) 150
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[21] = (byte) 29;
    sourceArray2[20] = (byte) 139;
    sourceArray2[19] = (byte) 20;
    sourceArray2[33] = (byte) 113;
    sourceArray2[25] = (byte) 168;
    sourceArray2[31 /*0x1F*/] = (byte) 72;
    sourceArray2[0] = (byte) 202;
    sourceArray2[7] = (byte) 249;
    sourceArray2[8] = (byte) 107;
    sourceArray2[18] = (byte) 2;
    sourceArray2[32 /*0x20*/] = (byte) 124;
    sourceArray2[2] = (byte) 243;
    sourceArray2[30] = (byte) 127 /*0x7F*/;
    sourceArray2[14] = (byte) 213;
    sourceArray2[42] = (byte) 73;
    sourceArray2[15] = (byte) 223;
    sourceArray2[16 /*0x10*/] = (byte) 234;
    sourceArray2[13] = (byte) 223;
    sourceArray2[26] = (byte) 195;
    sourceArray2[1] = (byte) 166;
    sourceArray2[12] = (byte) 16 /*0x10*/;
    sourceArray2[46] = (byte) 186;
    sourceArray2[22] = (byte) 33;
    sourceArray2[3] = (byte) 7;
    sourceArray2[10] = (byte) 91;
    sourceArray2[47] = (byte) 15;
    sourceArray2[6] = (byte) 212;
    sourceArray2[27] = (byte) 76;
    sourceArray2[28] = (byte) 65;
    sourceArray2[29] = (byte) 191;
    sourceArray2[11] = (byte) 180;
    sourceArray2[9] = (byte) 58;
    sourceArray2[34] = (byte) 27;
    sourceArray2[5] = (byte) 193;
    sourceArray2[40] = (byte) 74;
    sourceArray2[35] = (byte) 58;
    sourceArray2[36] = (byte) 253;
    sourceArray2[4] = (byte) 250;
    sourceArray2[38] = (byte) 150;
    sourceArray2[39] = (byte) 13;
    sourceArray2[24] = (byte) 5;
    sourceArray2[41] = (byte) 120;
    sourceArray2[37] = (byte) 26;
    sourceArray2[43] = (byte) 82;
    sourceArray2[44] = (byte) 228;
    sourceArray2[45] = (byte) 252;
    sourceArray2[17] = (byte) 224 /*0xE0*/;
    sourceArray2[23] = (byte) 96 /*0x60*/;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13860(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[15] = (byte) 143;
    sourceArray1[3] = (byte) 66;
    sourceArray1[23] = (byte) 74;
    sourceArray1[45] = (byte) 241;
    sourceArray1[42] = (byte) 29;
    sourceArray1[5] = (byte) 205;
    sourceArray1[6] = (byte) 168;
    sourceArray1[7] = (byte) 160 /*0xA0*/;
    sourceArray1[24] = (byte) 247;
    sourceArray1[1] = (byte) 181;
    sourceArray1[39] = (byte) 143;
    sourceArray1[10] = (byte) 220;
    sourceArray1[12] = (byte) 217;
    sourceArray1[13] = (byte) 13;
    sourceArray1[14] = (byte) 222;
    sourceArray1[0] = (byte) 87;
    sourceArray1[4] = (byte) 216;
    sourceArray1[17] = (byte) 112 /*0x70*/;
    sourceArray1[34] = (byte) 55;
    sourceArray1[19] = (byte) 231;
    sourceArray1[20] = (byte) 142;
    sourceArray1[21] = (byte) 160 /*0xA0*/;
    sourceArray1[36] = (byte) 244;
    sourceArray1[26] = (byte) 242;
    sourceArray1[38] = (byte) 22;
    sourceArray1[37] = (byte) 44;
    sourceArray1[33] = (byte) 254;
    sourceArray1[27] = (byte) 96 /*0x60*/;
    sourceArray1[16 /*0x10*/] = (byte) 151;
    sourceArray1[29] = (byte) 93;
    sourceArray1[30] = (byte) 53;
    sourceArray1[31 /*0x1F*/] = (byte) 50;
    sourceArray1[32 /*0x20*/] = (byte) 23;
    sourceArray1[9] = (byte) 21;
    sourceArray1[47] = (byte) 208 /*0xD0*/;
    sourceArray1[11] = (byte) 192 /*0xC0*/;
    sourceArray1[25] = (byte) 66;
    sourceArray1[18] = (byte) 15;
    sourceArray1[22] = (byte) 143;
    sourceArray1[2] = (byte) 234;
    sourceArray1[40] = (byte) 153;
    sourceArray1[8] = (byte) 194;
    sourceArray1[28] = (byte) 126;
    sourceArray1[43] = (byte) 60;
    sourceArray1[44] = (byte) 193;
    sourceArray1[46] = (byte) 59;
    sourceArray1[41] = (byte) 248;
    sourceArray1[35] = (byte) 128 /*0x80*/;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 75,
      (byte) 187,
      byte.MaxValue,
      (byte) 97,
      (byte) 183,
      (byte) 121,
      (byte) 76,
      (byte) 20,
      (byte) 124,
      (byte) 191,
      (byte) 178,
      (byte) 122,
      (byte) 163,
      (byte) 199,
      (byte) 196,
      (byte) 22,
      (byte) 189,
      (byte) 243,
      (byte) 129,
      (byte) 13,
      (byte) 118,
      (byte) 49,
      (byte) 113,
      (byte) 68,
      (byte) 50,
      (byte) 173,
      (byte) 80 /*0x50*/,
      (byte) 52,
      (byte) 148,
      (byte) 227,
      (byte) 93,
      (byte) 194,
      (byte) 150,
      (byte) 20,
      (byte) 215,
      (byte) 143,
      (byte) 47,
      (byte) 163,
      (byte) 82,
      (byte) 3,
      (byte) 101,
      (byte) 85,
      (byte) 98,
      (byte) 164,
      (byte) 169,
      (byte) 53,
      (byte) 117,
      (byte) 141
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13861(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[24] = (byte) 69;
    sourceArray1[34] = (byte) 165;
    sourceArray1[11] = (byte) 23;
    sourceArray1[3] = (byte) 96 /*0x60*/;
    sourceArray1[18] = (byte) 239;
    sourceArray1[42] = (byte) 44;
    sourceArray1[6] = (byte) 40;
    sourceArray1[47] = (byte) 240 /*0xF0*/;
    sourceArray1[8] = (byte) 157;
    sourceArray1[45] = (byte) 61;
    sourceArray1[10] = (byte) 210;
    sourceArray1[15] = (byte) 185;
    sourceArray1[12] = (byte) 69;
    sourceArray1[13] = (byte) 143;
    sourceArray1[22] = (byte) 85;
    sourceArray1[36] = (byte) 28;
    sourceArray1[16 /*0x10*/] = (byte) 158;
    sourceArray1[17] = (byte) 191;
    sourceArray1[31 /*0x1F*/] = (byte) 246;
    sourceArray1[19] = (byte) 32 /*0x20*/;
    sourceArray1[21] = (byte) 16 /*0x10*/;
    sourceArray1[40] = (byte) 97;
    sourceArray1[43] = (byte) 77;
    sourceArray1[23] = (byte) 3;
    sourceArray1[4] = (byte) 76;
    sourceArray1[25] = (byte) 137;
    sourceArray1[14] = (byte) 184;
    sourceArray1[27] = (byte) 202;
    sourceArray1[28] = (byte) 254;
    sourceArray1[29] = (byte) 191;
    sourceArray1[30] = (byte) 202;
    sourceArray1[39] = (byte) 6;
    sourceArray1[7] = (byte) 213;
    sourceArray1[33] = (byte) 188;
    sourceArray1[35] = (byte) 39;
    sourceArray1[1] = (byte) 250;
    sourceArray1[44] = (byte) 136;
    sourceArray1[37] = (byte) 35;
    sourceArray1[20] = (byte) 180;
    sourceArray1[41] = (byte) 30;
    sourceArray1[2] = (byte) 114;
    sourceArray1[0] = (byte) 97;
    sourceArray1[46] = (byte) 156;
    sourceArray1[38] = (byte) 89;
    sourceArray1[26] = (byte) 217;
    sourceArray1[9] = (byte) 51;
    sourceArray1[32 /*0x20*/] = (byte) 113;
    sourceArray1[5] = (byte) 234;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 59,
      (byte) 253,
      (byte) 58,
      (byte) 251,
      (byte) 209,
      (byte) 231,
      (byte) 124,
      (byte) 54,
      (byte) 19,
      (byte) 166,
      (byte) 59,
      (byte) 103,
      (byte) 177,
      (byte) 42,
      (byte) 154,
      (byte) 29,
      (byte) 117,
      (byte) 144 /*0x90*/,
      (byte) 135,
      (byte) 240 /*0xF0*/,
      (byte) 110,
      (byte) 202,
      (byte) 131,
      (byte) 199,
      (byte) 93,
      (byte) 95,
      (byte) 185,
      (byte) 211,
      (byte) 151,
      (byte) 128 /*0x80*/,
      (byte) 37,
      (byte) 173,
      (byte) 4,
      (byte) 202,
      (byte) 156,
      (byte) 16 /*0x10*/,
      (byte) 208 /*0xD0*/,
      (byte) 106,
      (byte) 160 /*0xA0*/,
      (byte) 158,
      (byte) 149,
      (byte) 80 /*0x50*/,
      (byte) 141,
      (byte) 227,
      (byte) 158,
      (byte) 123,
      (byte) 121,
      (byte) 9
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13862(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[41] = (byte) 66;
    sourceArray1[1] = (byte) 7;
    sourceArray1[21] = (byte) 96 /*0x60*/;
    sourceArray1[3] = (byte) 250;
    sourceArray1[10] = (byte) 158;
    sourceArray1[24] = (byte) 239;
    sourceArray1[6] = (byte) 132;
    sourceArray1[7] = (byte) 56;
    sourceArray1[15] = (byte) 144 /*0x90*/;
    sourceArray1[26] = (byte) 207;
    sourceArray1[13] = (byte) 213;
    sourceArray1[2] = (byte) 107;
    sourceArray1[28] = (byte) 187;
    sourceArray1[8] = (byte) 253;
    sourceArray1[19] = (byte) 81;
    sourceArray1[0] = (byte) 19;
    sourceArray1[25] = (byte) 113;
    sourceArray1[17] = (byte) 87;
    sourceArray1[18] = (byte) 149;
    sourceArray1[5] = (byte) 146;
    sourceArray1[11] = (byte) 244;
    sourceArray1[40] = (byte) 5;
    sourceArray1[22] = (byte) 149;
    sourceArray1[23] = (byte) 14;
    sourceArray1[32 /*0x20*/] = (byte) 206;
    sourceArray1[46] = (byte) 246;
    sourceArray1[27] = (byte) 241;
    sourceArray1[29] = (byte) 78;
    sourceArray1[9] = (byte) 108;
    sourceArray1[16 /*0x10*/] = (byte) 111;
    sourceArray1[30] = (byte) 149;
    sourceArray1[31 /*0x1F*/] = (byte) 33;
    sourceArray1[14] = (byte) 39;
    sourceArray1[33] = (byte) 113;
    sourceArray1[34] = (byte) 217;
    sourceArray1[35] = (byte) 225;
    sourceArray1[36] = (byte) 98;
    sourceArray1[37] = (byte) 109;
    sourceArray1[38] = (byte) 201;
    sourceArray1[39] = (byte) 64 /*0x40*/;
    sourceArray1[43] = (byte) 231;
    sourceArray1[4] = (byte) 189;
    sourceArray1[42] = (byte) 42;
    sourceArray1[20] = (byte) 43;
    sourceArray1[44] = (byte) 228;
    sourceArray1[45] = (byte) 184;
    sourceArray1[12] = (byte) 18;
    sourceArray1[47] = (byte) 193;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 219,
      (byte) 151,
      (byte) 83,
      (byte) 112 /*0x70*/,
      (byte) 149,
      (byte) 144 /*0x90*/,
      (byte) 26,
      (byte) 144 /*0x90*/,
      (byte) 177,
      (byte) 64 /*0x40*/,
      (byte) 151,
      (byte) 228,
      (byte) 198,
      (byte) 110,
      (byte) 74,
      (byte) 27,
      (byte) 240 /*0xF0*/,
      (byte) 52,
      (byte) 92,
      (byte) 217,
      (byte) 154,
      (byte) 196,
      (byte) 185,
      (byte) 42,
      (byte) 251,
      (byte) 212,
      (byte) 5,
      (byte) 128 /*0x80*/,
      (byte) 232,
      (byte) 35,
      (byte) 50,
      (byte) 204,
      (byte) 95,
      (byte) 215,
      (byte) 176 /*0xB0*/,
      (byte) 30,
      (byte) 239,
      (byte) 99,
      (byte) 29,
      (byte) 159,
      (byte) 113,
      (byte) 124,
      (byte) 20,
      (byte) 38,
      (byte) 179,
      (byte) 188,
      (byte) 231,
      (byte) 71
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13863(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 84,
      (byte) 197,
      (byte) 138,
      (byte) 69,
      (byte) 214,
      (byte) 105,
      (byte) 249,
      (byte) 13,
      (byte) 129,
      (byte) 124,
      (byte) 196,
      (byte) 50,
      (byte) 210,
      (byte) 208 /*0xD0*/,
      (byte) 180,
      (byte) 112 /*0x70*/,
      (byte) 52,
      (byte) 56,
      (byte) 57,
      (byte) 169,
      (byte) 177,
      (byte) 155,
      (byte) 212,
      (byte) 213,
      (byte) 41,
      (byte) 159,
      (byte) 16 /*0x10*/,
      (byte) 216,
      (byte) 217,
      (byte) 35,
      (byte) 186,
      (byte) 157,
      (byte) 194,
      (byte) 165,
      (byte) 38,
      (byte) 73,
      (byte) 151,
      (byte) 2,
      (byte) 159,
      (byte) 52,
      (byte) 216,
      (byte) 102,
      (byte) 132,
      (byte) 171,
      (byte) 171,
      (byte) 209,
      (byte) 143,
      (byte) 172
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[9] = (byte) 72;
    sourceArray2[24] = (byte) 116;
    sourceArray2[2] = (byte) 111;
    sourceArray2[18] = (byte) 182;
    sourceArray2[21] = (byte) 149;
    sourceArray2[5] = (byte) 249;
    sourceArray2[4] = (byte) 106;
    sourceArray2[10] = (byte) 75;
    sourceArray2[8] = (byte) 90;
    sourceArray2[3] = (byte) 206;
    sourceArray2[15] = (byte) 22;
    sourceArray2[11] = (byte) 211;
    sourceArray2[1] = (byte) 86;
    sourceArray2[7] = (byte) 93;
    sourceArray2[31 /*0x1F*/] = (byte) 86;
    sourceArray2[16 /*0x10*/] = (byte) 52;
    sourceArray2[40] = (byte) 251;
    sourceArray2[6] = (byte) 160 /*0xA0*/;
    sourceArray2[27] = (byte) 147;
    sourceArray2[29] = (byte) 227;
    sourceArray2[20] = (byte) 175;
    sourceArray2[19] = (byte) 131;
    sourceArray2[22] = (byte) 36;
    sourceArray2[47] = (byte) 113;
    sourceArray2[30] = (byte) 199;
    sourceArray2[33] = (byte) 91;
    sourceArray2[26] = (byte) 122;
    sourceArray2[37] = (byte) 122;
    sourceArray2[28] = (byte) 172;
    sourceArray2[13] = (byte) 211;
    sourceArray2[12] = (byte) 81;
    sourceArray2[35] = (byte) 233;
    sourceArray2[32 /*0x20*/] = (byte) 215;
    sourceArray2[23] = (byte) 43;
    sourceArray2[34] = (byte) 146;
    sourceArray2[17] = (byte) 213;
    sourceArray2[36] = (byte) 105;
    sourceArray2[41] = (byte) 215;
    sourceArray2[38] = (byte) 187;
    sourceArray2[39] = (byte) 79;
    sourceArray2[25] = (byte) 135;
    sourceArray2[0] = (byte) 243;
    sourceArray2[42] = (byte) 242;
    sourceArray2[43] = (byte) 128 /*0x80*/;
    sourceArray2[44] = (byte) 216;
    sourceArray2[45] = (byte) 202;
    sourceArray2[14] = (byte) 45;
    sourceArray2[46] = (byte) 168;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[29];
    byte[] response2 = new byte[29];
    Array.Copy((Array) sc_13834.sspq, 129, (Array) numArray2, 0, 29);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13834.sspr, 129, (Array) numArray2, 0, 29);
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

  internal static int ssp_appserver_13864(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[38] = (byte) 120;
    sourceArray1[1] = (byte) 63 /*0x3F*/;
    sourceArray1[32 /*0x20*/] = (byte) 212;
    sourceArray1[3] = (byte) 246;
    sourceArray1[0] = (byte) 239;
    sourceArray1[5] = (byte) 97;
    sourceArray1[6] = (byte) 178;
    sourceArray1[2] = (byte) 166;
    sourceArray1[8] = (byte) 49;
    sourceArray1[24] = (byte) 244;
    sourceArray1[11] = (byte) 22;
    sourceArray1[31 /*0x1F*/] = (byte) 167;
    sourceArray1[28] = (byte) 52;
    sourceArray1[13] = (byte) 202;
    sourceArray1[19] = (byte) 165;
    sourceArray1[43] = (byte) 122;
    sourceArray1[9] = (byte) 206;
    sourceArray1[44] = (byte) 109;
    sourceArray1[18] = (byte) 229;
    sourceArray1[16 /*0x10*/] = (byte) 34;
    sourceArray1[20] = (byte) 211;
    sourceArray1[7] = (byte) 172;
    sourceArray1[22] = (byte) 160 /*0xA0*/;
    sourceArray1[23] = (byte) 33;
    sourceArray1[4] = (byte) 174;
    sourceArray1[14] = (byte) 145;
    sourceArray1[33] = (byte) 125;
    sourceArray1[41] = (byte) 177;
    sourceArray1[15] = (byte) 87;
    sourceArray1[29] = (byte) 22;
    sourceArray1[30] = (byte) 121;
    sourceArray1[10] = (byte) 244;
    sourceArray1[12] = (byte) 47;
    sourceArray1[25] = (byte) 249;
    sourceArray1[34] = (byte) 179;
    sourceArray1[35] = (byte) 135;
    sourceArray1[36] = (byte) 228;
    sourceArray1[37] = (byte) 206;
    sourceArray1[40] = (byte) 94;
    sourceArray1[39] = (byte) 147;
    sourceArray1[47] = (byte) 113;
    sourceArray1[26] = (byte) 198;
    sourceArray1[42] = (byte) 154;
    sourceArray1[21] = (byte) 91;
    sourceArray1[27] = (byte) 73;
    sourceArray1[45] = (byte) 86;
    sourceArray1[46] = (byte) 57;
    sourceArray1[17] = (byte) 66;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[27] = (byte) 85;
    sourceArray2[1] = (byte) 178;
    sourceArray2[26] = (byte) 119;
    sourceArray2[9] = (byte) 97;
    sourceArray2[41] = (byte) 147;
    sourceArray2[5] = (byte) 237;
    sourceArray2[21] = (byte) 39;
    sourceArray2[45] = (byte) 101;
    sourceArray2[22] = (byte) 68;
    sourceArray2[4] = (byte) 115;
    sourceArray2[43] = (byte) 110;
    sourceArray2[15] = (byte) 27;
    sourceArray2[12] = (byte) 178;
    sourceArray2[13] = (byte) 245;
    sourceArray2[14] = (byte) 63 /*0x3F*/;
    sourceArray2[7] = (byte) 228;
    sourceArray2[37] = (byte) 13;
    sourceArray2[17] = (byte) 241;
    sourceArray2[8] = (byte) 51;
    sourceArray2[38] = (byte) 253;
    sourceArray2[20] = (byte) 230;
    sourceArray2[0] = (byte) 215;
    sourceArray2[28] = (byte) 252;
    sourceArray2[23] = (byte) 8;
    sourceArray2[24] = (byte) 250;
    sourceArray2[25] = (byte) 8;
    sourceArray2[47] = (byte) 43;
    sourceArray2[16 /*0x10*/] = (byte) 238;
    sourceArray2[32 /*0x20*/] = (byte) 33;
    sourceArray2[29] = (byte) 196;
    sourceArray2[30] = (byte) 90;
    sourceArray2[44] = (byte) 215;
    sourceArray2[3] = (byte) 237;
    sourceArray2[33] = (byte) 131;
    sourceArray2[34] = (byte) 58;
    sourceArray2[35] = (byte) 202;
    sourceArray2[36] = (byte) 155;
    sourceArray2[46] = (byte) 109;
    sourceArray2[18] = (byte) 86;
    sourceArray2[39] = (byte) 228;
    sourceArray2[10] = (byte) 155;
    sourceArray2[2] = (byte) 131;
    sourceArray2[42] = (byte) 241;
    sourceArray2[6] = (byte) 86;
    sourceArray2[40] = (byte) 23;
    sourceArray2[11] = (byte) 95;
    sourceArray2[31 /*0x1F*/] = (byte) 77;
    sourceArray2[19] = (byte) 147;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13865(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 177,
      (byte) 84,
      (byte) 87,
      (byte) 81,
      (byte) 91,
      (byte) 56,
      (byte) 50,
      (byte) 13,
      (byte) 101,
      (byte) 87,
      (byte) 22,
      (byte) 78,
      (byte) 27,
      (byte) 152,
      (byte) 220,
      (byte) 58,
      (byte) 143,
      (byte) 117,
      (byte) 60,
      (byte) 217,
      (byte) 186,
      (byte) 78,
      (byte) 81,
      (byte) 118,
      (byte) 20,
      (byte) 95,
      (byte) 118,
      (byte) 174,
      (byte) 99,
      (byte) 168,
      (byte) 175,
      (byte) 133,
      (byte) 28,
      (byte) 54,
      (byte) 167,
      (byte) 252,
      (byte) 144 /*0x90*/,
      (byte) 241,
      (byte) 166,
      (byte) 91,
      (byte) 87,
      (byte) 43,
      (byte) 147,
      (byte) 248,
      (byte) 85,
      (byte) 201,
      (byte) 78,
      (byte) 217
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 173,
      (byte) 12,
      (byte) 203,
      (byte) 21,
      (byte) 74,
      (byte) 74,
      (byte) 213,
      (byte) 73,
      (byte) 177,
      (byte) 254,
      (byte) 66,
      (byte) 220,
      (byte) 184,
      (byte) 198,
      (byte) 143,
      (byte) 111,
      (byte) 213,
      (byte) 237,
      (byte) 84,
      (byte) 125,
      (byte) 10,
      (byte) 63 /*0x3F*/,
      (byte) 2,
      (byte) 77,
      (byte) 91,
      (byte) 55,
      (byte) 90,
      (byte) 204,
      (byte) 241,
      (byte) 132,
      (byte) 250,
      (byte) 99,
      (byte) 205,
      (byte) 169,
      (byte) 243,
      (byte) 252,
      (byte) 218,
      (byte) 111,
      (byte) 250,
      (byte) 184,
      (byte) 155,
      (byte) 9,
      (byte) 142,
      (byte) 141,
      (byte) 200,
      (byte) 233,
      (byte) 80 /*0x50*/,
      (byte) 7
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13866(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[47] = (byte) 151;
    sourceArray1[1] = (byte) 22;
    sourceArray1[2] = (byte) 99;
    sourceArray1[14] = (byte) 229;
    sourceArray1[4] = (byte) 225;
    sourceArray1[5] = (byte) 60;
    sourceArray1[37] = (byte) 249;
    sourceArray1[45] = (byte) 80 /*0x50*/;
    sourceArray1[0] = (byte) 144 /*0x90*/;
    sourceArray1[27] = (byte) 16 /*0x10*/;
    sourceArray1[10] = (byte) 189;
    sourceArray1[25] = (byte) 32 /*0x20*/;
    sourceArray1[15] = (byte) 240 /*0xF0*/;
    sourceArray1[36] = (byte) 68;
    sourceArray1[12] = (byte) 145;
    sourceArray1[26] = (byte) 74;
    sourceArray1[44] = (byte) 214;
    sourceArray1[17] = (byte) 169;
    sourceArray1[40] = (byte) 170;
    sourceArray1[19] = (byte) 207;
    sourceArray1[20] = (byte) 225;
    sourceArray1[32 /*0x20*/] = (byte) 179;
    sourceArray1[22] = (byte) 142;
    sourceArray1[23] = (byte) 186;
    sourceArray1[24] = (byte) 88;
    sourceArray1[38] = (byte) 174;
    sourceArray1[3] = (byte) 3;
    sourceArray1[6] = (byte) 252;
    sourceArray1[43] = (byte) 130;
    sourceArray1[16 /*0x10*/] = (byte) 224 /*0xE0*/;
    sourceArray1[30] = (byte) 136;
    sourceArray1[31 /*0x1F*/] = (byte) 233;
    sourceArray1[28] = (byte) 156;
    sourceArray1[33] = (byte) 50;
    sourceArray1[34] = (byte) 139;
    sourceArray1[35] = (byte) 31 /*0x1F*/;
    sourceArray1[13] = (byte) 50;
    sourceArray1[21] = (byte) 129;
    sourceArray1[18] = (byte) 158;
    sourceArray1[8] = (byte) 131;
    sourceArray1[29] = (byte) 88;
    sourceArray1[9] = (byte) 169;
    sourceArray1[42] = (byte) 212;
    sourceArray1[7] = (byte) 194;
    sourceArray1[11] = (byte) 168;
    sourceArray1[39] = (byte) 180;
    sourceArray1[46] = (byte) 195;
    sourceArray1[41] = (byte) 153;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 200,
      (byte) 85,
      (byte) 131,
      (byte) 3,
      (byte) 5,
      (byte) 251,
      (byte) 0,
      (byte) 87,
      (byte) 89,
      (byte) 240 /*0xF0*/,
      (byte) 189,
      (byte) 228,
      (byte) 125,
      (byte) 243,
      (byte) 170,
      (byte) 250,
      (byte) 214,
      (byte) 14,
      (byte) 201,
      (byte) 169,
      (byte) 26,
      (byte) 201,
      (byte) 132,
      (byte) 182,
      (byte) 233,
      (byte) 10,
      (byte) 226,
      (byte) 214,
      (byte) 59,
      (byte) 217,
      (byte) 27,
      (byte) 38,
      (byte) 76,
      (byte) 160 /*0xA0*/,
      (byte) 70,
      (byte) 123,
      (byte) 132,
      (byte) 163,
      (byte) 19,
      (byte) 55,
      (byte) 37,
      (byte) 222,
      (byte) 54,
      (byte) 192 /*0xC0*/,
      (byte) 200,
      (byte) 167,
      (byte) 240 /*0xF0*/,
      (byte) 33
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13867(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[31 /*0x1F*/] = (byte) 171;
    sourceArray1[4] = (byte) 143;
    sourceArray1[2] = (byte) 163;
    sourceArray1[3] = (byte) 60;
    sourceArray1[1] = (byte) 239;
    sourceArray1[20] = (byte) 108;
    sourceArray1[43] = (byte) 25;
    sourceArray1[39] = (byte) 32 /*0x20*/;
    sourceArray1[8] = (byte) 41;
    sourceArray1[45] = (byte) 214;
    sourceArray1[10] = (byte) 123;
    sourceArray1[11] = (byte) 196;
    sourceArray1[12] = (byte) 161;
    sourceArray1[13] = (byte) 100;
    sourceArray1[14] = (byte) 62;
    sourceArray1[0] = (byte) 101;
    sourceArray1[42] = (byte) 151;
    sourceArray1[17] = (byte) 73;
    sourceArray1[6] = (byte) 54;
    sourceArray1[37] = (byte) 119;
    sourceArray1[24] = (byte) 16 /*0x10*/;
    sourceArray1[21] = (byte) 158;
    sourceArray1[18] = (byte) 203;
    sourceArray1[41] = (byte) 129;
    sourceArray1[16 /*0x10*/] = (byte) 113;
    sourceArray1[25] = (byte) 151;
    sourceArray1[23] = (byte) 45;
    sourceArray1[15] = (byte) 67;
    sourceArray1[19] = (byte) 10;
    sourceArray1[28] = (byte) 102;
    sourceArray1[30] = (byte) 3;
    sourceArray1[9] = (byte) 98;
    sourceArray1[32 /*0x20*/] = (byte) 225;
    sourceArray1[33] = (byte) 173;
    sourceArray1[34] = (byte) 247;
    sourceArray1[27] = (byte) 39;
    sourceArray1[36] = (byte) 4;
    sourceArray1[7] = (byte) 6;
    sourceArray1[38] = (byte) 82;
    sourceArray1[29] = (byte) 212;
    sourceArray1[40] = (byte) 146;
    sourceArray1[47] = (byte) 137;
    sourceArray1[5] = (byte) 54;
    sourceArray1[22] = (byte) 42;
    sourceArray1[44] = (byte) 27;
    sourceArray1[26] = (byte) 13;
    sourceArray1[46] = (byte) 174;
    sourceArray1[35] = (byte) 195;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[28] = (byte) 45;
    sourceArray2[37] = (byte) 226;
    sourceArray2[9] = (byte) 227;
    sourceArray2[3] = (byte) 177;
    sourceArray2[4] = (byte) 249;
    sourceArray2[5] = (byte) 118;
    sourceArray2[11] = (byte) 6;
    sourceArray2[1] = (byte) 164;
    sourceArray2[19] = (byte) 218;
    sourceArray2[36] = (byte) 11;
    sourceArray2[10] = (byte) 198;
    sourceArray2[42] = (byte) 71;
    sourceArray2[23] = (byte) 144 /*0x90*/;
    sourceArray2[13] = (byte) 237;
    sourceArray2[7] = (byte) 205;
    sourceArray2[0] = (byte) 37;
    sourceArray2[47] = (byte) 33;
    sourceArray2[17] = (byte) 84;
    sourceArray2[29] = byte.MaxValue;
    sourceArray2[2] = (byte) 31 /*0x1F*/;
    sourceArray2[20] = (byte) 164;
    sourceArray2[25] = (byte) 173;
    sourceArray2[22] = (byte) 74;
    sourceArray2[6] = (byte) 1;
    sourceArray2[24] = (byte) 58;
    sourceArray2[27] = (byte) 204;
    sourceArray2[26] = (byte) 54;
    sourceArray2[21] = (byte) 138;
    sourceArray2[46] = (byte) 136;
    sourceArray2[14] = (byte) 0;
    sourceArray2[30] = (byte) 254;
    sourceArray2[31 /*0x1F*/] = (byte) 90;
    sourceArray2[32 /*0x20*/] = (byte) 239;
    sourceArray2[33] = (byte) 72;
    sourceArray2[35] = (byte) 6;
    sourceArray2[12] = (byte) 242;
    sourceArray2[38] = (byte) 216;
    sourceArray2[34] = (byte) 169;
    sourceArray2[8] = (byte) 174;
    sourceArray2[39] = (byte) 44;
    sourceArray2[40] = (byte) 32 /*0x20*/;
    sourceArray2[41] = (byte) 116;
    sourceArray2[45] = (byte) 173;
    sourceArray2[43] = (byte) 81;
    sourceArray2[44] = (byte) 38;
    sourceArray2[15] = (byte) 230;
    sourceArray2[16 /*0x10*/] = (byte) 220;
    sourceArray2[18] = (byte) 189;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13868(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 83,
      (byte) 23,
      (byte) 42,
      (byte) 189,
      (byte) 254,
      (byte) 145,
      (byte) 247,
      (byte) 133,
      (byte) 226,
      (byte) 140,
      (byte) 143,
      (byte) 88,
      (byte) 205,
      (byte) 55,
      (byte) 138,
      (byte) 248,
      (byte) 250,
      (byte) 93,
      (byte) 156,
      (byte) 170,
      (byte) 223,
      (byte) 60,
      (byte) 208 /*0xD0*/,
      (byte) 9,
      (byte) 186,
      (byte) 131,
      (byte) 142,
      (byte) 146,
      (byte) 120,
      (byte) 202,
      (byte) 49,
      (byte) 130,
      (byte) 86,
      (byte) 79,
      (byte) 95,
      (byte) 221,
      (byte) 223,
      (byte) 7,
      (byte) 88,
      (byte) 53,
      (byte) 80 /*0x50*/,
      (byte) 169,
      (byte) 133,
      (byte) 14,
      (byte) 29,
      (byte) 112 /*0x70*/,
      (byte) 107,
      (byte) 243
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 99,
      (byte) 207,
      (byte) 75,
      (byte) 30,
      (byte) 223,
      (byte) 83,
      (byte) 52,
      (byte) 83,
      (byte) 128 /*0x80*/,
      (byte) 197,
      (byte) 199,
      (byte) 50,
      (byte) 156,
      (byte) 154,
      (byte) 124,
      (byte) 19,
      (byte) 88,
      (byte) 40,
      (byte) 94,
      (byte) 63 /*0x3F*/,
      (byte) 54,
      (byte) 180,
      (byte) 153,
      (byte) 161,
      (byte) 64 /*0x40*/,
      (byte) 8,
      (byte) 71,
      (byte) 236,
      (byte) 68,
      (byte) 198,
      (byte) 240 /*0xF0*/,
      (byte) 71,
      (byte) 220,
      (byte) 68,
      (byte) 214,
      (byte) 51,
      (byte) 196,
      (byte) 112 /*0x70*/,
      (byte) 218,
      (byte) 221,
      (byte) 60,
      (byte) 215,
      (byte) 158,
      (byte) 139,
      (byte) 231,
      (byte) 44,
      byte.MaxValue,
      (byte) 136
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13869(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 174,
      (byte) 108,
      (byte) 23,
      (byte) 71,
      (byte) 33,
      (byte) 2,
      (byte) 123,
      (byte) 2,
      (byte) 101,
      (byte) 12,
      (byte) 23,
      (byte) 90,
      (byte) 12,
      (byte) 2,
      (byte) 126,
      (byte) 216,
      (byte) 173,
      (byte) 24,
      (byte) 242,
      (byte) 5,
      (byte) 103,
      (byte) 90,
      (byte) 54,
      (byte) 183,
      (byte) 193,
      (byte) 236,
      (byte) 74,
      (byte) 23,
      (byte) 155,
      (byte) 179,
      (byte) 181,
      (byte) 83,
      (byte) 68,
      (byte) 243,
      (byte) 167,
      (byte) 226,
      (byte) 72,
      (byte) 149,
      (byte) 190,
      (byte) 4,
      (byte) 221,
      (byte) 50,
      (byte) 234,
      (byte) 156,
      (byte) 109,
      (byte) 180,
      (byte) 16 /*0x10*/,
      (byte) 110
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[37] = (byte) 222;
    sourceArray2[1] = (byte) 50;
    sourceArray2[27] = (byte) 23;
    sourceArray2[40] = (byte) 224 /*0xE0*/;
    sourceArray2[18] = (byte) 152;
    sourceArray2[5] = (byte) 175;
    sourceArray2[6] = (byte) 254;
    sourceArray2[7] = (byte) 14;
    sourceArray2[41] = (byte) 152;
    sourceArray2[9] = (byte) 130;
    sourceArray2[10] = (byte) 190;
    sourceArray2[11] = byte.MaxValue;
    sourceArray2[12] = (byte) 114;
    sourceArray2[13] = (byte) 99;
    sourceArray2[38] = (byte) 67;
    sourceArray2[15] = (byte) 121;
    sourceArray2[29] = (byte) 189;
    sourceArray2[39] = (byte) 94;
    sourceArray2[21] = (byte) 52;
    sourceArray2[19] = (byte) 116;
    sourceArray2[33] = (byte) 86;
    sourceArray2[0] = (byte) 103;
    sourceArray2[26] = (byte) 240 /*0xF0*/;
    sourceArray2[23] = (byte) 170;
    sourceArray2[22] = (byte) 187;
    sourceArray2[25] = (byte) 56;
    sourceArray2[43] = (byte) 25;
    sourceArray2[46] = (byte) 108;
    sourceArray2[28] = (byte) 166;
    sourceArray2[20] = (byte) 209;
    sourceArray2[17] = (byte) 3;
    sourceArray2[31 /*0x1F*/] = (byte) 86;
    sourceArray2[32 /*0x20*/] = (byte) 158;
    sourceArray2[47] = (byte) 174;
    sourceArray2[30] = (byte) 237;
    sourceArray2[35] = (byte) 123;
    sourceArray2[36] = (byte) 242;
    sourceArray2[8] = (byte) 150;
    sourceArray2[16 /*0x10*/] = (byte) 234;
    sourceArray2[14] = (byte) 174;
    sourceArray2[4] = (byte) 69;
    sourceArray2[24] = (byte) 39;
    sourceArray2[42] = (byte) 109;
    sourceArray2[2] = (byte) 137;
    sourceArray2[44] = (byte) 167;
    sourceArray2[45] = (byte) 0;
    sourceArray2[3] = (byte) 185;
    sourceArray2[34] = (byte) 162;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13870(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 246,
      (byte) 19,
      (byte) 81,
      (byte) 138,
      (byte) 26,
      (byte) 81,
      (byte) 149,
      (byte) 192 /*0xC0*/,
      (byte) 242,
      (byte) 228,
      (byte) 157,
      (byte) 20,
      (byte) 121,
      (byte) 89,
      (byte) 220,
      (byte) 56,
      (byte) 231,
      (byte) 47,
      (byte) 77,
      (byte) 243,
      (byte) 184,
      (byte) 78,
      (byte) 6,
      (byte) 239,
      (byte) 168,
      (byte) 7,
      (byte) 208 /*0xD0*/,
      (byte) 162,
      (byte) 174,
      (byte) 105,
      (byte) 54,
      (byte) 84,
      (byte) 161,
      (byte) 109,
      (byte) 228,
      (byte) 200,
      (byte) 216,
      (byte) 246,
      (byte) 103,
      (byte) 184,
      (byte) 59,
      (byte) 156,
      (byte) 216,
      (byte) 111,
      (byte) 14,
      (byte) 211,
      (byte) 150,
      (byte) 47
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 0,
      (byte) 37,
      (byte) 227,
      (byte) 199,
      (byte) 122,
      (byte) 175,
      (byte) 44,
      (byte) 148,
      (byte) 161,
      (byte) 144 /*0x90*/,
      (byte) 182,
      (byte) 212,
      (byte) 82,
      (byte) 60,
      (byte) 161,
      (byte) 40,
      (byte) 183,
      (byte) 41,
      (byte) 21,
      (byte) 135,
      (byte) 204,
      (byte) 232,
      (byte) 46,
      (byte) 89,
      (byte) 56,
      (byte) 81,
      (byte) 141,
      (byte) 98,
      (byte) 147,
      (byte) 221,
      (byte) 111,
      (byte) 249,
      (byte) 69,
      (byte) 126,
      (byte) 36,
      (byte) 31 /*0x1F*/,
      (byte) 174,
      (byte) 203,
      (byte) 28,
      (byte) 52,
      (byte) 154,
      (byte) 170,
      (byte) 160 /*0xA0*/,
      (byte) 222,
      (byte) 252,
      (byte) 212,
      (byte) 146,
      (byte) 48 /*0x30*/
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13871(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[16 /*0x10*/] = (byte) 20;
    sourceArray1[1] = (byte) 230;
    sourceArray1[26] = (byte) 212;
    sourceArray1[25] = (byte) 224 /*0xE0*/;
    sourceArray1[32 /*0x20*/] = (byte) 44;
    sourceArray1[22] = (byte) 28;
    sourceArray1[14] = (byte) 11;
    sourceArray1[5] = (byte) 225;
    sourceArray1[18] = (byte) 241;
    sourceArray1[42] = (byte) 72;
    sourceArray1[10] = (byte) 136;
    sourceArray1[0] = (byte) 181;
    sourceArray1[6] = (byte) 19;
    sourceArray1[46] = (byte) 197;
    sourceArray1[9] = (byte) 18;
    sourceArray1[15] = (byte) 116;
    sourceArray1[34] = (byte) 235;
    sourceArray1[17] = (byte) 95;
    sourceArray1[37] = (byte) 188;
    sourceArray1[19] = (byte) 199;
    sourceArray1[38] = (byte) 118;
    sourceArray1[21] = (byte) 184;
    sourceArray1[30] = (byte) 108;
    sourceArray1[23] = (byte) 118;
    sourceArray1[24] = (byte) 151;
    sourceArray1[8] = (byte) 243;
    sourceArray1[12] = (byte) 73;
    sourceArray1[27] = (byte) 112 /*0x70*/;
    sourceArray1[28] = (byte) 161;
    sourceArray1[29] = (byte) 50;
    sourceArray1[39] = (byte) 20;
    sourceArray1[35] = (byte) 135;
    sourceArray1[13] = (byte) 115;
    sourceArray1[7] = (byte) 129;
    sourceArray1[11] = (byte) 174;
    sourceArray1[33] = (byte) 92;
    sourceArray1[36] = (byte) 60;
    sourceArray1[4] = (byte) 98;
    sourceArray1[3] = (byte) 12;
    sourceArray1[47] = (byte) 96 /*0x60*/;
    sourceArray1[40] = (byte) 84;
    sourceArray1[41] = (byte) 55;
    sourceArray1[2] = (byte) 168;
    sourceArray1[31 /*0x1F*/] = (byte) 134;
    sourceArray1[44] = (byte) 79;
    sourceArray1[45] = (byte) 193;
    sourceArray1[20] = (byte) 148;
    sourceArray1[43] = (byte) 138;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[47] = (byte) 28;
    sourceArray2[1] = (byte) 233;
    sourceArray2[24] = (byte) 244;
    sourceArray2[2] = (byte) 77;
    sourceArray2[4] = (byte) 247;
    sourceArray2[5] = (byte) 31 /*0x1F*/;
    sourceArray2[6] = (byte) 100;
    sourceArray2[7] = (byte) 81;
    sourceArray2[11] = (byte) 16 /*0x10*/;
    sourceArray2[9] = (byte) 68;
    sourceArray2[30] = (byte) 175;
    sourceArray2[38] = (byte) 14;
    sourceArray2[25] = (byte) 210;
    sourceArray2[15] = (byte) 41;
    sourceArray2[31 /*0x1F*/] = (byte) 119;
    sourceArray2[22] = (byte) 56;
    sourceArray2[16 /*0x10*/] = (byte) 58;
    sourceArray2[8] = (byte) 39;
    sourceArray2[18] = (byte) 195;
    sourceArray2[19] = (byte) 202;
    sourceArray2[20] = (byte) 245;
    sourceArray2[35] = (byte) 131;
    sourceArray2[12] = (byte) 253;
    sourceArray2[46] = (byte) 61;
    sourceArray2[21] = (byte) 198;
    sourceArray2[26] = (byte) 45;
    sourceArray2[37] = (byte) 233;
    sourceArray2[27] = (byte) 29;
    sourceArray2[28] = (byte) 191;
    sourceArray2[29] = (byte) 74;
    sourceArray2[14] = (byte) 213;
    sourceArray2[13] = (byte) 186;
    sourceArray2[32 /*0x20*/] = (byte) 4;
    sourceArray2[33] = (byte) 144 /*0x90*/;
    sourceArray2[10] = (byte) 7;
    sourceArray2[34] = (byte) 109;
    sourceArray2[42] = (byte) 164;
    sourceArray2[17] = (byte) 244;
    sourceArray2[3] = (byte) 39;
    sourceArray2[39] = (byte) 82;
    sourceArray2[40] = (byte) 253;
    sourceArray2[41] = (byte) 56;
    sourceArray2[43] = (byte) 7;
    sourceArray2[0] = (byte) 143;
    sourceArray2[44] = (byte) 91;
    sourceArray2[23] = (byte) 10;
    sourceArray2[36] = (byte) 27;
    sourceArray2[45] = (byte) 30;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13872(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 108,
      (byte) 244,
      (byte) 94,
      (byte) 159,
      (byte) 203,
      (byte) 222,
      (byte) 170,
      (byte) 228,
      (byte) 64 /*0x40*/,
      (byte) 67,
      (byte) 84,
      (byte) 242,
      (byte) 3,
      (byte) 61,
      (byte) 14,
      (byte) 0,
      (byte) 164,
      (byte) 137,
      (byte) 49,
      (byte) 8,
      (byte) 35,
      byte.MaxValue,
      (byte) 225,
      (byte) 148,
      (byte) 19,
      (byte) 134,
      (byte) 238,
      (byte) 121,
      (byte) 1,
      (byte) 54,
      (byte) 61,
      (byte) 46,
      (byte) 104,
      (byte) 102,
      (byte) 106,
      (byte) 204,
      (byte) 167,
      (byte) 72,
      (byte) 226,
      (byte) 251,
      (byte) 236,
      (byte) 94,
      (byte) 90,
      (byte) 152,
      (byte) 15,
      (byte) 226,
      (byte) 143,
      (byte) 65
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[45] = (byte) 205;
    sourceArray2[1] = (byte) 233;
    sourceArray2[44] = (byte) 195;
    sourceArray2[21] = (byte) 133;
    sourceArray2[7] = (byte) 149;
    sourceArray2[27] = (byte) 76;
    sourceArray2[5] = (byte) 146;
    sourceArray2[23] = (byte) 30;
    sourceArray2[8] = (byte) 189;
    sourceArray2[9] = (byte) 135;
    sourceArray2[30] = (byte) 113;
    sourceArray2[0] = (byte) 246;
    sourceArray2[12] = (byte) 56;
    sourceArray2[22] = (byte) 45;
    sourceArray2[28] = (byte) 182;
    sourceArray2[15] = (byte) 30;
    sourceArray2[6] = (byte) 242;
    sourceArray2[17] = (byte) 50;
    sourceArray2[19] = (byte) 162;
    sourceArray2[25] = (byte) 66;
    sourceArray2[20] = (byte) 62;
    sourceArray2[3] = (byte) 24;
    sourceArray2[41] = (byte) 207;
    sourceArray2[14] = (byte) 187;
    sourceArray2[47] = (byte) 217;
    sourceArray2[24] = (byte) 36;
    sourceArray2[26] = (byte) 202;
    sourceArray2[18] = (byte) 226;
    sourceArray2[40] = (byte) 44;
    sourceArray2[29] = (byte) 22;
    sourceArray2[33] = (byte) 178;
    sourceArray2[11] = (byte) 8;
    sourceArray2[32 /*0x20*/] = (byte) 75;
    sourceArray2[31 /*0x1F*/] = (byte) 12;
    sourceArray2[39] = (byte) 214;
    sourceArray2[2] = (byte) 218;
    sourceArray2[10] = (byte) 133;
    sourceArray2[37] = (byte) 100;
    sourceArray2[38] = (byte) 197;
    sourceArray2[16 /*0x10*/] = (byte) 60;
    sourceArray2[34] = (byte) 30;
    sourceArray2[13] = (byte) 77;
    sourceArray2[42] = (byte) 248;
    sourceArray2[43] = (byte) 243;
    sourceArray2[35] = (byte) 224 /*0xE0*/;
    sourceArray2[36] = (byte) 244;
    sourceArray2[46] = (byte) 248;
    sourceArray2[4] = (byte) 157;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13873(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 242,
      (byte) 110,
      (byte) 204,
      (byte) 152,
      (byte) 97,
      (byte) 87,
      (byte) 150,
      (byte) 132,
      (byte) 165,
      (byte) 200,
      (byte) 201,
      (byte) 68,
      (byte) 91,
      (byte) 79,
      (byte) 66,
      (byte) 196,
      (byte) 210,
      (byte) 144 /*0x90*/,
      (byte) 197,
      (byte) 197,
      (byte) 193,
      (byte) 161,
      (byte) 213,
      (byte) 135,
      (byte) 123,
      (byte) 159,
      (byte) 9,
      (byte) 43,
      (byte) 95,
      (byte) 73,
      (byte) 83,
      (byte) 182,
      (byte) 156,
      (byte) 215,
      (byte) 3,
      (byte) 62,
      (byte) 159,
      (byte) 119,
      (byte) 23,
      (byte) 232,
      (byte) 201,
      (byte) 66,
      (byte) 86,
      (byte) 53,
      (byte) 241,
      (byte) 153,
      (byte) 97,
      (byte) 78
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 232,
      (byte) 61,
      (byte) 171,
      (byte) 190,
      (byte) 158,
      (byte) 130,
      (byte) 207,
      (byte) 145,
      (byte) 231,
      (byte) 88,
      (byte) 247,
      (byte) 197,
      (byte) 216,
      (byte) 45,
      (byte) 107,
      (byte) 213,
      (byte) 29,
      (byte) 1,
      (byte) 104,
      byte.MaxValue,
      (byte) 22,
      (byte) 181,
      (byte) 91,
      (byte) 179,
      (byte) 90,
      (byte) 249,
      (byte) 248,
      (byte) 247,
      (byte) 2,
      (byte) 253,
      (byte) 98,
      (byte) 201,
      (byte) 103,
      (byte) 77,
      (byte) 10,
      (byte) 253,
      (byte) 196,
      (byte) 168,
      (byte) 15,
      (byte) 243,
      (byte) 179,
      (byte) 220,
      (byte) 50,
      (byte) 253,
      (byte) 232,
      (byte) 210,
      (byte) 120,
      (byte) 62
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13874(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[17] = (byte) 37;
    sourceArray1[4] = (byte) 69;
    sourceArray1[8] = (byte) 107;
    sourceArray1[3] = (byte) 152;
    sourceArray1[6] = (byte) 194;
    sourceArray1[45] = (byte) 192 /*0xC0*/;
    sourceArray1[27] = (byte) 62;
    sourceArray1[25] = (byte) 150;
    sourceArray1[29] = (byte) 99;
    sourceArray1[40] = (byte) 84;
    sourceArray1[11] = (byte) 116;
    sourceArray1[43] = (byte) 29;
    sourceArray1[19] = (byte) 121;
    sourceArray1[13] = (byte) 167;
    sourceArray1[23] = (byte) 75;
    sourceArray1[15] = (byte) 75;
    sourceArray1[1] = (byte) 151;
    sourceArray1[35] = (byte) 64 /*0x40*/;
    sourceArray1[18] = (byte) 91;
    sourceArray1[33] = (byte) 233;
    sourceArray1[20] = (byte) 221;
    sourceArray1[38] = (byte) 90;
    sourceArray1[10] = (byte) 92;
    sourceArray1[46] = (byte) 222;
    sourceArray1[24] = (byte) 160 /*0xA0*/;
    sourceArray1[21] = (byte) 252;
    sourceArray1[0] = (byte) 34;
    sourceArray1[14] = (byte) 131;
    sourceArray1[9] = (byte) 37;
    sourceArray1[7] = (byte) 77;
    sourceArray1[30] = (byte) 104;
    sourceArray1[31 /*0x1F*/] = (byte) 159;
    sourceArray1[32 /*0x20*/] = (byte) 147;
    sourceArray1[12] = (byte) 19;
    sourceArray1[34] = (byte) 121;
    sourceArray1[2] = (byte) 80 /*0x50*/;
    sourceArray1[36] = (byte) 178;
    sourceArray1[37] = (byte) 166;
    sourceArray1[5] = (byte) 29;
    sourceArray1[28] = (byte) 186;
    sourceArray1[16 /*0x10*/] = (byte) 3;
    sourceArray1[41] = (byte) 154;
    sourceArray1[42] = (byte) 94;
    sourceArray1[26] = (byte) 225;
    sourceArray1[44] = (byte) 249;
    sourceArray1[22] = (byte) 126;
    sourceArray1[47] = (byte) 242;
    sourceArray1[39] = (byte) 89;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[24] = (byte) 2;
    sourceArray2[1] = (byte) 178;
    sourceArray2[35] = (byte) 203;
    sourceArray2[3] = (byte) 172;
    sourceArray2[4] = (byte) 58;
    sourceArray2[5] = (byte) 158;
    sourceArray2[41] = (byte) 191;
    sourceArray2[7] = (byte) 251;
    sourceArray2[8] = (byte) 236;
    sourceArray2[17] = (byte) 101;
    sourceArray2[30] = (byte) 106;
    sourceArray2[11] = (byte) 74;
    sourceArray2[0] = (byte) 254;
    sourceArray2[13] = (byte) 55;
    sourceArray2[44] = (byte) 165;
    sourceArray2[15] = (byte) 185;
    sourceArray2[38] = (byte) 229;
    sourceArray2[28] = (byte) 139;
    sourceArray2[18] = (byte) 21;
    sourceArray2[39] = (byte) 73;
    sourceArray2[20] = (byte) 236;
    sourceArray2[32 /*0x20*/] = (byte) 191;
    sourceArray2[22] = (byte) 73;
    sourceArray2[23] = (byte) 105;
    sourceArray2[19] = (byte) 211;
    sourceArray2[25] = (byte) 243;
    sourceArray2[37] = (byte) 185;
    sourceArray2[43] = (byte) 224 /*0xE0*/;
    sourceArray2[21] = (byte) 89;
    sourceArray2[29] = (byte) 224 /*0xE0*/;
    sourceArray2[16 /*0x10*/] = (byte) 243;
    sourceArray2[12] = (byte) 110;
    sourceArray2[33] = (byte) 129;
    sourceArray2[31 /*0x1F*/] = (byte) 149;
    sourceArray2[34] = (byte) 209;
    sourceArray2[6] = (byte) 164;
    sourceArray2[36] = (byte) 35;
    sourceArray2[27] = (byte) 35;
    sourceArray2[10] = (byte) 144 /*0x90*/;
    sourceArray2[2] = (byte) 141;
    sourceArray2[40] = (byte) 113;
    sourceArray2[9] = (byte) 114;
    sourceArray2[46] = (byte) 98;
    sourceArray2[42] = (byte) 221;
    sourceArray2[14] = (byte) 247;
    sourceArray2[45] = (byte) 84;
    sourceArray2[26] = (byte) 18;
    sourceArray2[47] = (byte) 70;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[46];
    byte[] response2 = new byte[46];
    Array.Copy((Array) sc_13834.sspq, 158, (Array) numArray2, 0, 46);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13834.sspr, 158, (Array) numArray2, 0, 46);
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

  internal static int ssp_appserver_13875(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[24] = (byte) 234;
    sourceArray1[36] = (byte) 8;
    sourceArray1[20] = (byte) 122;
    sourceArray1[40] = (byte) 149;
    sourceArray1[41] = (byte) 170;
    sourceArray1[5] = (byte) 90;
    sourceArray1[6] = (byte) 129;
    sourceArray1[7] = (byte) 193;
    sourceArray1[21] = (byte) 89;
    sourceArray1[9] = (byte) 79;
    sourceArray1[13] = (byte) 105;
    sourceArray1[34] = (byte) 206;
    sourceArray1[12] = (byte) 160 /*0xA0*/;
    sourceArray1[3] = (byte) 134;
    sourceArray1[14] = (byte) 223;
    sourceArray1[1] = (byte) 90;
    sourceArray1[30] = (byte) 24;
    sourceArray1[22] = (byte) 40;
    sourceArray1[18] = (byte) 141;
    sourceArray1[32 /*0x20*/] = (byte) 176 /*0xB0*/;
    sourceArray1[39] = (byte) 150;
    sourceArray1[45] = (byte) 154;
    sourceArray1[17] = (byte) 215;
    sourceArray1[23] = (byte) 97;
    sourceArray1[4] = (byte) 187;
    sourceArray1[31 /*0x1F*/] = (byte) 190;
    sourceArray1[26] = (byte) 91;
    sourceArray1[37] = (byte) 139;
    sourceArray1[28] = (byte) 113;
    sourceArray1[29] = (byte) 221;
    sourceArray1[27] = (byte) 96 /*0x60*/;
    sourceArray1[10] = (byte) 158;
    sourceArray1[2] = (byte) 219;
    sourceArray1[43] = (byte) 87;
    sourceArray1[19] = (byte) 162;
    sourceArray1[35] = (byte) 17;
    sourceArray1[16 /*0x10*/] = (byte) 214;
    sourceArray1[11] = (byte) 190;
    sourceArray1[38] = (byte) 237;
    sourceArray1[42] = (byte) 94;
    sourceArray1[15] = (byte) 159;
    sourceArray1[8] = (byte) 177;
    sourceArray1[47] = (byte) 218;
    sourceArray1[33] = (byte) 238;
    sourceArray1[44] = (byte) 112 /*0x70*/;
    sourceArray1[25] = (byte) 237;
    sourceArray1[0] = (byte) 153;
    sourceArray1[46] = (byte) 39;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[43] = (byte) 47;
    sourceArray2[33] = (byte) 85;
    sourceArray2[2] = (byte) 201;
    sourceArray2[3] = (byte) 4;
    sourceArray2[24] = (byte) 48 /*0x30*/;
    sourceArray2[1] = (byte) 23;
    sourceArray2[40] = (byte) 232;
    sourceArray2[27] = (byte) 68;
    sourceArray2[26] = (byte) 74;
    sourceArray2[22] = (byte) 124;
    sourceArray2[10] = (byte) 40;
    sourceArray2[11] = (byte) 178;
    sourceArray2[36] = (byte) 107;
    sourceArray2[32 /*0x20*/] = (byte) 2;
    sourceArray2[7] = (byte) 96 /*0x60*/;
    sourceArray2[35] = (byte) 153;
    sourceArray2[42] = (byte) 156;
    sourceArray2[17] = (byte) 53;
    sourceArray2[9] = (byte) 61;
    sourceArray2[19] = (byte) 245;
    sourceArray2[20] = (byte) 52;
    sourceArray2[15] = (byte) 41;
    sourceArray2[16 /*0x10*/] = (byte) 91;
    sourceArray2[6] = (byte) 36;
    sourceArray2[5] = (byte) 50;
    sourceArray2[25] = (byte) 105;
    sourceArray2[44] = (byte) 14;
    sourceArray2[38] = (byte) 119;
    sourceArray2[39] = (byte) 232;
    sourceArray2[29] = (byte) 130;
    sourceArray2[30] = (byte) 214;
    sourceArray2[31 /*0x1F*/] = (byte) 105;
    sourceArray2[28] = (byte) 42;
    sourceArray2[8] = (byte) 82;
    sourceArray2[4] = (byte) 151;
    sourceArray2[0] = (byte) 59;
    sourceArray2[21] = (byte) 95;
    sourceArray2[12] = (byte) 91;
    sourceArray2[23] = (byte) 186;
    sourceArray2[37] = (byte) 232;
    sourceArray2[18] = (byte) 123;
    sourceArray2[41] = (byte) 122;
    sourceArray2[14] = (byte) 207;
    sourceArray2[34] = (byte) 159;
    sourceArray2[13] = (byte) 17;
    sourceArray2[45] = (byte) 150;
    sourceArray2[46] = (byte) 145;
    sourceArray2[47] = (byte) 108;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[11];
    byte[] response2 = new byte[11];
    Array.Copy((Array) sc_13834.sspq, 204, (Array) numArray2, 0, 11);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13834.sspr, 204, (Array) numArray2, 0, 11);
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
}
