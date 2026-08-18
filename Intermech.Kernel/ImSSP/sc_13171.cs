// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_13171
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_13171
{
  private static byte[] sspq = new byte[357]
  {
    (byte) 253,
    (byte) 32 /*0x20*/,
    (byte) 87,
    (byte) 63 /*0x3F*/,
    (byte) 129,
    (byte) 146,
    (byte) 190,
    (byte) 251,
    (byte) 128 /*0x80*/,
    (byte) 176 /*0xB0*/,
    (byte) 15,
    (byte) 12,
    (byte) 206,
    byte.MaxValue,
    (byte) 149,
    (byte) 113,
    (byte) 65,
    (byte) 209,
    (byte) 203,
    (byte) 90,
    (byte) 254,
    (byte) 224 /*0xE0*/,
    (byte) 178,
    (byte) 9,
    (byte) 29,
    (byte) 83,
    (byte) 196,
    (byte) 211,
    (byte) 89,
    (byte) 79,
    (byte) 1,
    (byte) 199,
    (byte) 71,
    (byte) 153,
    (byte) 237,
    (byte) 236,
    (byte) 49,
    (byte) 189,
    (byte) 165,
    (byte) 120,
    (byte) 146,
    (byte) 200,
    (byte) 206,
    (byte) 28,
    (byte) 43,
    (byte) 186,
    (byte) 203,
    (byte) 244,
    (byte) 249,
    (byte) 123,
    (byte) 232,
    (byte) 97,
    (byte) 244,
    (byte) 156,
    (byte) 147,
    (byte) 29,
    (byte) 129,
    (byte) 148,
    (byte) 119,
    (byte) 231,
    (byte) 35,
    (byte) 95,
    (byte) 99,
    (byte) 221,
    (byte) 44,
    (byte) 221,
    (byte) 115,
    (byte) 56,
    (byte) 240 /*0xF0*/,
    (byte) 88,
    (byte) 73,
    (byte) 245,
    (byte) 212,
    (byte) 52,
    (byte) 77,
    (byte) 223,
    (byte) 125,
    (byte) 119,
    (byte) 173,
    (byte) 236,
    (byte) 122,
    (byte) 194,
    (byte) 193,
    (byte) 116,
    (byte) 125,
    (byte) 14,
    byte.MaxValue,
    (byte) 116,
    (byte) 196,
    (byte) 214,
    (byte) 25,
    (byte) 10,
    (byte) 218,
    (byte) 217,
    (byte) 152,
    (byte) 46,
    (byte) 16 /*0x10*/,
    (byte) 60,
    (byte) 90,
    (byte) 193,
    (byte) 196,
    (byte) 103,
    (byte) 79,
    (byte) 65,
    (byte) 254,
    (byte) 135,
    (byte) 181,
    (byte) 208 /*0xD0*/,
    (byte) 250,
    (byte) 201,
    (byte) 73,
    (byte) 215,
    (byte) 150,
    (byte) 201,
    (byte) 250,
    (byte) 221,
    (byte) 159,
    (byte) 152,
    (byte) 38,
    (byte) 156,
    (byte) 176 /*0xB0*/,
    (byte) 59,
    (byte) 125,
    (byte) 42,
    (byte) 164,
    (byte) 106,
    (byte) 83,
    (byte) 130,
    (byte) 253,
    (byte) 207,
    (byte) 110,
    (byte) 131,
    (byte) 224 /*0xE0*/,
    (byte) 106,
    (byte) 14,
    (byte) 196,
    (byte) 139,
    (byte) 19,
    (byte) 11,
    (byte) 235,
    (byte) 90,
    (byte) 120,
    (byte) 134,
    (byte) 26,
    (byte) 157,
    (byte) 241,
    (byte) 100,
    (byte) 47,
    (byte) 87,
    (byte) 74,
    (byte) 150,
    (byte) 101,
    (byte) 155,
    (byte) 188,
    (byte) 143,
    (byte) 129,
    (byte) 119,
    (byte) 233,
    (byte) 69,
    (byte) 225,
    (byte) 135,
    (byte) 38,
    (byte) 247,
    (byte) 24,
    (byte) 161,
    (byte) 121,
    (byte) 51,
    (byte) 234,
    (byte) 187,
    (byte) 162,
    (byte) 9,
    byte.MaxValue,
    (byte) 241,
    (byte) 237,
    (byte) 108,
    (byte) 236,
    (byte) 128 /*0x80*/,
    (byte) 91,
    (byte) 215,
    (byte) 89,
    (byte) 155,
    (byte) 80 /*0x50*/,
    (byte) 147,
    (byte) 76,
    (byte) 248,
    (byte) 64 /*0x40*/,
    (byte) 17,
    (byte) 103,
    (byte) 142,
    (byte) 229,
    (byte) 30,
    (byte) 40,
    (byte) 114,
    (byte) 51,
    (byte) 39,
    (byte) 233,
    (byte) 180,
    (byte) 198,
    (byte) 36,
    (byte) 100,
    (byte) 179,
    (byte) 139,
    (byte) 194,
    (byte) 119,
    (byte) 179,
    (byte) 240 /*0xF0*/,
    (byte) 80 /*0x50*/,
    (byte) 127 /*0x7F*/,
    (byte) 85,
    (byte) 209,
    (byte) 186,
    (byte) 140,
    (byte) 56,
    (byte) 159,
    (byte) 67,
    (byte) 105,
    (byte) 96 /*0x60*/,
    (byte) 90,
    (byte) 97,
    (byte) 114,
    (byte) 165,
    (byte) 15,
    (byte) 141,
    (byte) 28,
    (byte) 152,
    (byte) 8,
    (byte) 240 /*0xF0*/,
    (byte) 145,
    (byte) 17,
    (byte) 188,
    (byte) 43,
    (byte) 145,
    (byte) 239,
    (byte) 210,
    (byte) 240 /*0xF0*/,
    (byte) 190,
    (byte) 136,
    (byte) 90,
    (byte) 100,
    (byte) 110,
    (byte) 140,
    (byte) 159,
    (byte) 179,
    (byte) 252,
    (byte) 113,
    (byte) 195,
    (byte) 43,
    (byte) 150,
    (byte) 109,
    (byte) 62,
    (byte) 36,
    (byte) 118,
    (byte) 130,
    (byte) 154,
    (byte) 72,
    (byte) 236,
    (byte) 31 /*0x1F*/,
    (byte) 194,
    (byte) 166,
    (byte) 80 /*0x50*/,
    (byte) 202,
    (byte) 104,
    (byte) 228,
    (byte) 63 /*0x3F*/,
    (byte) 140,
    (byte) 18,
    (byte) 26,
    (byte) 234,
    (byte) 220,
    (byte) 216,
    (byte) 223,
    (byte) 22,
    (byte) 236,
    (byte) 2,
    (byte) 78,
    (byte) 122,
    (byte) 141,
    (byte) 53,
    (byte) 37,
    (byte) 219,
    (byte) 165,
    (byte) 126,
    (byte) 98,
    (byte) 99,
    (byte) 122,
    (byte) 241,
    (byte) 66,
    (byte) 149,
    (byte) 101,
    (byte) 36,
    (byte) 131,
    (byte) 174,
    (byte) 12,
    (byte) 1,
    (byte) 170,
    (byte) 178,
    (byte) 54,
    (byte) 162,
    (byte) 53,
    (byte) 130,
    (byte) 41,
    (byte) 38,
    (byte) 172,
    (byte) 206,
    (byte) 35,
    (byte) 28,
    (byte) 104,
    (byte) 99,
    (byte) 220,
    (byte) 78,
    (byte) 200,
    (byte) 247,
    (byte) 174,
    (byte) 206,
    (byte) 76,
    (byte) 200,
    (byte) 54,
    (byte) 17,
    (byte) 196,
    (byte) 238,
    (byte) 221,
    (byte) 122,
    (byte) 191,
    (byte) 27,
    (byte) 223,
    (byte) 75,
    (byte) 202,
    (byte) 5,
    (byte) 42,
    (byte) 235,
    (byte) 189,
    (byte) 164,
    (byte) 244,
    (byte) 216,
    (byte) 188,
    (byte) 69,
    (byte) 242,
    (byte) 149,
    (byte) 76,
    (byte) 168,
    (byte) 72,
    (byte) 39,
    (byte) 2,
    (byte) 206,
    (byte) 46,
    (byte) 7,
    (byte) 222,
    (byte) 67,
    (byte) 1,
    (byte) 7,
    (byte) 155,
    (byte) 221,
    (byte) 179,
    (byte) 231,
    (byte) 102,
    (byte) 92,
    (byte) 115
  };
  private static byte[] sspr = new byte[357]
  {
    (byte) 34,
    (byte) 161,
    (byte) 155,
    (byte) 58,
    (byte) 210,
    (byte) 102,
    (byte) 83,
    (byte) 192 /*0xC0*/,
    (byte) 216,
    (byte) 239,
    (byte) 86,
    (byte) 228,
    (byte) 32 /*0x20*/,
    (byte) 205,
    (byte) 78,
    (byte) 126,
    (byte) 139,
    (byte) 136,
    (byte) 117,
    (byte) 108,
    (byte) 167,
    (byte) 27,
    (byte) 191,
    (byte) 178,
    (byte) 17,
    (byte) 128 /*0x80*/,
    (byte) 161,
    (byte) 191,
    (byte) 154,
    (byte) 246,
    (byte) 8,
    (byte) 78,
    (byte) 29,
    (byte) 43,
    (byte) 24,
    (byte) 55,
    (byte) 42,
    (byte) 216,
    (byte) 223,
    (byte) 57,
    (byte) 157,
    (byte) 100,
    (byte) 241,
    (byte) 129,
    (byte) 220,
    (byte) 85,
    (byte) 160 /*0xA0*/,
    (byte) 226,
    (byte) 118,
    (byte) 51,
    (byte) 132,
    (byte) 54,
    (byte) 109,
    (byte) 86,
    (byte) 7,
    (byte) 182,
    (byte) 117,
    (byte) 250,
    (byte) 93,
    (byte) 124,
    (byte) 119,
    (byte) 48 /*0x30*/,
    (byte) 108,
    (byte) 248,
    (byte) 254,
    (byte) 121,
    (byte) 18,
    (byte) 128 /*0x80*/,
    (byte) 86,
    (byte) 11,
    (byte) 198,
    (byte) 10,
    (byte) 225,
    (byte) 218,
    (byte) 55,
    (byte) 170,
    (byte) 50,
    (byte) 246,
    (byte) 215,
    (byte) 240 /*0xF0*/,
    (byte) 216,
    (byte) 33,
    (byte) 11,
    (byte) 101,
    (byte) 208 /*0xD0*/,
    (byte) 61,
    (byte) 130,
    byte.MaxValue,
    (byte) 65,
    (byte) 114,
    (byte) 83,
    (byte) 185,
    (byte) 9,
    (byte) 84,
    (byte) 181,
    (byte) 96 /*0x60*/,
    (byte) 56,
    (byte) 105,
    (byte) 128 /*0x80*/,
    (byte) 117,
    (byte) 185,
    (byte) 98,
    (byte) 61,
    (byte) 248,
    (byte) 179,
    (byte) 76,
    (byte) 38,
    (byte) 12,
    (byte) 100,
    (byte) 120,
    (byte) 3,
    (byte) 30,
    (byte) 239,
    (byte) 49,
    (byte) 40,
    (byte) 214,
    (byte) 6,
    (byte) 217,
    (byte) 125,
    (byte) 223,
    (byte) 6,
    (byte) 121,
    (byte) 175,
    (byte) 81,
    (byte) 56,
    (byte) 23,
    (byte) 80 /*0x50*/,
    (byte) 91,
    (byte) 228,
    (byte) 29,
    (byte) 13,
    (byte) 205,
    (byte) 109,
    (byte) 152,
    (byte) 53,
    (byte) 239,
    (byte) 239,
    (byte) 77,
    (byte) 202,
    (byte) 252,
    (byte) 187,
    (byte) 177,
    (byte) 73,
    (byte) 131,
    (byte) 97,
    (byte) 59,
    (byte) 44,
    (byte) 221,
    (byte) 155,
    (byte) 140,
    (byte) 23,
    (byte) 73,
    (byte) 203,
    (byte) 109,
    (byte) 94,
    (byte) 171,
    (byte) 181,
    (byte) 243,
    (byte) 246,
    (byte) 200,
    (byte) 101,
    (byte) 242,
    (byte) 74,
    (byte) 54,
    (byte) 111,
    (byte) 100,
    (byte) 69,
    (byte) 24,
    (byte) 252,
    (byte) 166,
    (byte) 103,
    (byte) 181,
    (byte) 152,
    (byte) 113,
    (byte) 155,
    (byte) 11,
    (byte) 184,
    (byte) 105,
    (byte) 46,
    (byte) 40,
    (byte) 223,
    (byte) 199,
    (byte) 243,
    (byte) 221,
    (byte) 146,
    (byte) 3,
    (byte) 155,
    (byte) 70,
    (byte) 74,
    (byte) 143,
    (byte) 45,
    (byte) 37,
    (byte) 40,
    (byte) 215,
    (byte) 120,
    (byte) 39,
    (byte) 66,
    (byte) 204,
    (byte) 164,
    (byte) 152,
    (byte) 81,
    (byte) 75,
    (byte) 108,
    (byte) 196,
    (byte) 95,
    (byte) 37,
    (byte) 129,
    (byte) 201,
    (byte) 37,
    (byte) 229,
    (byte) 67,
    (byte) 19,
    (byte) 70,
    (byte) 230,
    (byte) 30,
    (byte) 96 /*0x60*/,
    (byte) 91,
    (byte) 20,
    (byte) 141,
    (byte) 114,
    (byte) 183,
    (byte) 200,
    (byte) 133,
    (byte) 192 /*0xC0*/,
    (byte) 200,
    (byte) 81,
    (byte) 196,
    (byte) 17,
    (byte) 185,
    (byte) 204,
    (byte) 174,
    (byte) 188,
    (byte) 102,
    (byte) 173,
    (byte) 139,
    (byte) 79,
    (byte) 154,
    (byte) 81,
    (byte) 123,
    (byte) 139,
    (byte) 126,
    (byte) 112 /*0x70*/,
    (byte) 98,
    (byte) 9,
    (byte) 111,
    (byte) 210,
    (byte) 66,
    (byte) 101,
    (byte) 207,
    (byte) 150,
    (byte) 245,
    (byte) 48 /*0x30*/,
    (byte) 215,
    (byte) 99,
    (byte) 251,
    (byte) 5,
    (byte) 3,
    (byte) 244,
    (byte) 199,
    (byte) 15,
    (byte) 226,
    (byte) 101,
    (byte) 86,
    (byte) 226,
    (byte) 57,
    (byte) 129,
    (byte) 90,
    (byte) 219,
    (byte) 75,
    (byte) 131,
    (byte) 61,
    (byte) 182,
    (byte) 160 /*0xA0*/,
    (byte) 55,
    (byte) 167,
    (byte) 174,
    (byte) 91,
    (byte) 229,
    (byte) 194,
    (byte) 29,
    (byte) 102,
    (byte) 171,
    (byte) 48 /*0x30*/,
    (byte) 167,
    (byte) 198,
    (byte) 33,
    (byte) 42,
    (byte) 204,
    (byte) 87,
    (byte) 29,
    (byte) 55,
    (byte) 236,
    (byte) 36,
    (byte) 252,
    (byte) 220,
    (byte) 161,
    (byte) 159,
    (byte) 137,
    (byte) 57,
    (byte) 103,
    (byte) 60,
    (byte) 119,
    (byte) 166,
    (byte) 192 /*0xC0*/,
    (byte) 234,
    (byte) 234,
    (byte) 4,
    (byte) 187,
    (byte) 32 /*0x20*/,
    (byte) 240 /*0xF0*/,
    (byte) 28,
    (byte) 66,
    (byte) 238,
    (byte) 104,
    (byte) 124,
    (byte) 88,
    (byte) 93,
    (byte) 151,
    (byte) 26,
    (byte) 205,
    (byte) 192 /*0xC0*/,
    (byte) 133,
    (byte) 99,
    (byte) 139,
    (byte) 247,
    (byte) 17,
    (byte) 236,
    (byte) 204,
    (byte) 92,
    (byte) 44,
    (byte) 148,
    (byte) 86,
    (byte) 25,
    (byte) 100,
    (byte) 131,
    (byte) 109,
    (byte) 198,
    (byte) 212,
    (byte) 130,
    (byte) 152,
    (byte) 29,
    (byte) 24,
    (byte) 0,
    (byte) 210,
    (byte) 141,
    (byte) 232,
    (byte) 240 /*0xF0*/,
    (byte) 161,
    (byte) 38,
    (byte) 193,
    (byte) 120,
    (byte) 127 /*0x7F*/,
    (byte) 16 /*0x10*/,
    (byte) 77,
    (byte) 186,
    (byte) 193,
    (byte) 178
  };

  internal static int ssp_appserver_13172(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 190,
      (byte) 180,
      (byte) 41,
      (byte) 106,
      (byte) 165,
      (byte) 106,
      (byte) 167,
      (byte) 151,
      (byte) 213,
      (byte) 8,
      (byte) 68,
      (byte) 120,
      (byte) 61,
      (byte) 32 /*0x20*/,
      (byte) 67,
      (byte) 34,
      (byte) 54,
      (byte) 209,
      (byte) 128 /*0x80*/,
      (byte) 178,
      (byte) 99,
      (byte) 134,
      (byte) 26,
      (byte) 57,
      (byte) 37,
      (byte) 91,
      (byte) 164,
      (byte) 50,
      (byte) 6,
      (byte) 104,
      (byte) 52,
      (byte) 81,
      (byte) 29,
      (byte) 245,
      (byte) 148,
      (byte) 83,
      (byte) 153,
      (byte) 126,
      (byte) 15,
      (byte) 70,
      (byte) 35,
      (byte) 251,
      (byte) 200,
      (byte) 247,
      (byte) 110,
      (byte) 117,
      (byte) 116,
      (byte) 124
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[16 /*0x10*/] = (byte) 235;
    sourceArray2[1] = (byte) 74;
    sourceArray2[24] = (byte) 169;
    sourceArray2[25] = (byte) 136;
    sourceArray2[4] = (byte) 213;
    sourceArray2[14] = (byte) 194;
    sourceArray2[6] = (byte) 31 /*0x1F*/;
    sourceArray2[7] = (byte) 214;
    sourceArray2[8] = (byte) 173;
    sourceArray2[9] = (byte) 196;
    sourceArray2[44] = (byte) 92;
    sourceArray2[20] = (byte) 53;
    sourceArray2[33] = (byte) 38;
    sourceArray2[32 /*0x20*/] = (byte) 238;
    sourceArray2[2] = (byte) 199;
    sourceArray2[12] = (byte) 38;
    sourceArray2[31 /*0x1F*/] = (byte) 250;
    sourceArray2[17] = (byte) 122;
    sourceArray2[18] = (byte) 87;
    sourceArray2[19] = (byte) 34;
    sourceArray2[11] = (byte) 50;
    sourceArray2[41] = (byte) 29;
    sourceArray2[21] = (byte) 237;
    sourceArray2[23] = (byte) 131;
    sourceArray2[27] = (byte) 1;
    sourceArray2[37] = (byte) 93;
    sourceArray2[13] = (byte) 170;
    sourceArray2[43] = (byte) 65;
    sourceArray2[28] = (byte) 209;
    sourceArray2[29] = (byte) 186;
    sourceArray2[30] = (byte) 181;
    sourceArray2[0] = (byte) 182;
    sourceArray2[26] = (byte) 126;
    sourceArray2[10] = (byte) 173;
    sourceArray2[45] = (byte) 196;
    sourceArray2[35] = (byte) 51;
    sourceArray2[36] = (byte) 97;
    sourceArray2[3] = (byte) 79;
    sourceArray2[38] = (byte) 169;
    sourceArray2[39] = (byte) 161;
    sourceArray2[46] = (byte) 171;
    sourceArray2[5] = (byte) 91;
    sourceArray2[34] = (byte) 72;
    sourceArray2[42] = (byte) 126;
    sourceArray2[40] = (byte) 95;
    sourceArray2[15] = (byte) 29;
    sourceArray2[22] = (byte) 177;
    sourceArray2[47] = (byte) 239;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_13173()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[22];
      byte[] numArray2 = new byte[22];
      numArray2[1] = (byte) 43;
      numArray2[5] = byte.MaxValue;
      numArray2[4] = (byte) 88;
      numArray2[3] = (byte) 106;
      numArray2[14] = (byte) 99;
      numArray2[21] = (byte) 253;
      numArray2[6] = (byte) 19;
      numArray2[19] = (byte) 117;
      numArray2[2] = (byte) 240 /*0xF0*/;
      numArray2[9] = (byte) 118;
      numArray2[11] = (byte) 118;
      numArray2[8] = (byte) 47;
      numArray2[18] = (byte) 187;
      numArray2[13] = (byte) 148;
      numArray2[0] = (byte) 25;
      numArray2[15] = (byte) 158;
      numArray2[16 /*0x10*/] = (byte) 156;
      numArray2[17] = (byte) 108;
      numArray2[7] = (byte) 100;
      numArray2[12] = (byte) 35;
      numArray2[20] = (byte) 63 /*0x3F*/;
      numArray2[10] = (byte) 120;
      byte[] numArray3 = new byte[22]
      {
        (byte) 11,
        (byte) 113,
        (byte) 35,
        (byte) 182,
        (byte) 126,
        (byte) 27,
        (byte) 38,
        (byte) 90,
        (byte) 150,
        (byte) 48 /*0x30*/,
        (byte) 18,
        (byte) 250,
        (byte) 229,
        (byte) 46,
        (byte) 203,
        (byte) 13,
        (byte) 60,
        (byte) 209,
        (byte) 181,
        (byte) 28,
        (byte) 204,
        (byte) 216
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 22);
      for (int index = 0; index < 22; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[22];
    byte[] numArray5 = new byte[22]
    {
      (byte) 165,
      (byte) 97,
      (byte) 226,
      (byte) 18,
      (byte) 253,
      (byte) 160 /*0xA0*/,
      (byte) 174,
      (byte) 59,
      (byte) 28,
      (byte) 249,
      (byte) 193,
      (byte) 51,
      (byte) 122,
      (byte) 130,
      (byte) 22,
      (byte) 171,
      (byte) 79,
      (byte) 121,
      (byte) 21,
      (byte) 118,
      (byte) 211,
      (byte) 254
    };
    byte[] numArray6 = new byte[22]
    {
      (byte) 199,
      (byte) 236,
      (byte) 53,
      (byte) 50,
      (byte) 185,
      (byte) 54,
      (byte) 115,
      (byte) 43,
      (byte) 30,
      (byte) 53,
      (byte) 99,
      (byte) 99,
      (byte) 239,
      (byte) 18,
      (byte) 102,
      (byte) 234,
      (byte) 2,
      (byte) 16 /*0x10*/,
      (byte) 63 /*0x3F*/,
      (byte) 201,
      (byte) 83,
      (byte) 176 /*0xB0*/
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 22);
    for (int index = 0; index < 22; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13174()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[7];
      byte[] numArray2 = new byte[7]
      {
        (byte) 9,
        (byte) 42,
        (byte) 241,
        (byte) 206,
        (byte) 145,
        (byte) 73,
        (byte) 243
      };
      byte[] numArray3 = new byte[7];
      numArray3[6] = (byte) 180;
      numArray3[0] = (byte) 207;
      numArray3[2] = (byte) 62;
      numArray3[3] = (byte) 78;
      numArray3[4] = (byte) 40;
      numArray3[5] = (byte) 61;
      numArray3[1] = (byte) 206;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 7);
      for (int index = 0; index < 7; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[7];
    byte[] numArray5 = new byte[7]
    {
      (byte) 193,
      (byte) 118,
      (byte) 57,
      (byte) 81,
      (byte) 46,
      (byte) 51,
      (byte) 162
    };
    byte[] numArray6 = new byte[7];
    numArray6[1] = (byte) 100;
    numArray6[0] = (byte) 205;
    numArray6[5] = (byte) 23;
    numArray6[3] = (byte) 217;
    numArray6[4] = (byte) 237;
    numArray6[2] = (byte) 237;
    numArray6[6] = (byte) 97;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 7);
    for (int index = 0; index < 7; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13175()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[3];
      byte[] numArray2 = new byte[3]
      {
        (byte) 0,
        (byte) 0,
        (byte) 78
      };
      numArray2[0] = (byte) 131;
      numArray2[1] = (byte) 73;
      byte[] numArray3 = new byte[3]
      {
        (byte) 250,
        (byte) 80 /*0x50*/,
        (byte) 150
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 3);
      for (int index = 0; index < 3; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[3];
    byte[] numArray5 = new byte[3]
    {
      (byte) 212,
      (byte) 130,
      (byte) 122
    };
    byte[] numArray6 = new byte[3]
    {
      (byte) 74,
      (byte) 9,
      (byte) 103
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 3);
    for (int index = 0; index < 3; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13176()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 79,
        (byte) 239,
        (byte) 106,
        (byte) 121,
        (byte) 39,
        (byte) 165,
        (byte) 48 /*0x30*/,
        (byte) 225,
        (byte) 90,
        (byte) 234
      };
      byte[] numArray3 = new byte[10]
      {
        (byte) 16 /*0x10*/,
        (byte) 119,
        (byte) 35,
        (byte) 204,
        (byte) 202,
        (byte) 107,
        (byte) 212,
        (byte) 210,
        (byte) 160 /*0xA0*/,
        (byte) 45
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10]
    {
      (byte) 203,
      (byte) 43,
      (byte) 88,
      (byte) 253,
      (byte) 249,
      (byte) 47,
      (byte) 131,
      (byte) 184,
      (byte) 205,
      (byte) 92
    };
    byte[] numArray6 = new byte[10];
    numArray6[9] = (byte) 241;
    numArray6[1] = (byte) 161;
    numArray6[8] = (byte) 5;
    numArray6[5] = (byte) 28;
    numArray6[2] = (byte) 3;
    numArray6[4] = (byte) 216;
    numArray6[6] = (byte) 48 /*0x30*/;
    numArray6[7] = (byte) 108;
    numArray6[3] = (byte) 14;
    numArray6[0] = (byte) 23;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13177()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        byte.MaxValue,
        (byte) 24,
        (byte) 124,
        (byte) 122,
        (byte) 154,
        (byte) 121,
        (byte) 151,
        (byte) 216,
        (byte) 36,
        (byte) 87
      };
      byte[] numArray3 = new byte[10]
      {
        (byte) 43,
        (byte) 56,
        (byte) 207,
        (byte) 155,
        (byte) 33,
        (byte) 201,
        (byte) 81,
        (byte) 209,
        (byte) 201,
        (byte) 65
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[36];
      byte[] response = new byte[36];
      Array.Copy((Array) sc_13171.sspq, 0, (Array) numArray4, 0, 36);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_13171.sspr, 0, (Array) numArray4, 0, 36);
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
    byte[] numArray5 = new byte[10];
    byte[] numArray6 = new byte[10];
    numArray6[2] = (byte) 120;
    numArray6[1] = (byte) 77;
    numArray6[4] = (byte) 64 /*0x40*/;
    numArray6[3] = (byte) 204;
    numArray6[0] = (byte) 99;
    numArray6[5] = (byte) 3;
    numArray6[6] = (byte) 151;
    numArray6[9] = (byte) 141;
    numArray6[7] = (byte) 204;
    numArray6[8] = (byte) 106;
    byte[] numArray7 = new byte[10]
    {
      (byte) 96 /*0x60*/,
      (byte) 171,
      (byte) 86,
      (byte) 227,
      (byte) 114,
      (byte) 51,
      (byte) 85,
      (byte) 64 /*0x40*/,
      (byte) 166,
      (byte) 227
    };
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_appserver_13178()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[22];
      byte[] numArray2 = new byte[22]
      {
        (byte) 206,
        (byte) 234,
        (byte) 162,
        (byte) 49,
        (byte) 175,
        (byte) 72,
        (byte) 197,
        (byte) 52,
        (byte) 17,
        (byte) 169,
        (byte) 13,
        (byte) 243,
        (byte) 88,
        byte.MaxValue,
        (byte) 26,
        (byte) 250,
        (byte) 72,
        (byte) 229,
        (byte) 158,
        (byte) 143,
        (byte) 168,
        (byte) 43
      };
      byte[] numArray3 = new byte[22]
      {
        (byte) 3,
        (byte) 94,
        (byte) 5,
        (byte) 155,
        (byte) 208 /*0xD0*/,
        (byte) 129,
        (byte) 177,
        (byte) 87,
        (byte) 15,
        (byte) 52,
        (byte) 215,
        (byte) 59,
        (byte) 174,
        (byte) 98,
        (byte) 36,
        (byte) 158,
        (byte) 252,
        (byte) 65,
        (byte) 32 /*0x20*/,
        (byte) 44,
        (byte) 94,
        (byte) 96 /*0x60*/
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 22);
      for (int index = 0; index < 22; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[22];
    byte[] numArray5 = new byte[22];
    numArray5[1] = (byte) 56;
    numArray5[5] = (byte) 125;
    numArray5[6] = (byte) 22;
    numArray5[20] = (byte) 35;
    numArray5[4] = (byte) 209;
    numArray5[19] = (byte) 165;
    numArray5[17] = (byte) 135;
    numArray5[2] = (byte) 21;
    numArray5[8] = (byte) 74;
    numArray5[13] = (byte) 28;
    numArray5[10] = (byte) 86;
    numArray5[11] = (byte) 97;
    numArray5[12] = (byte) 102;
    numArray5[3] = (byte) 165;
    numArray5[14] = (byte) 34;
    numArray5[9] = (byte) 253;
    numArray5[16 /*0x10*/] = (byte) 119;
    numArray5[15] = (byte) 200;
    numArray5[18] = (byte) 153;
    numArray5[21] = (byte) 201;
    numArray5[7] = (byte) 44;
    numArray5[0] = (byte) 223;
    byte[] numArray6 = new byte[22]
    {
      (byte) 238,
      (byte) 177,
      (byte) 208 /*0xD0*/,
      (byte) 102,
      (byte) 213,
      (byte) 66,
      (byte) 219,
      (byte) 186,
      (byte) 8,
      (byte) 31 /*0x1F*/,
      (byte) 152,
      (byte) 110,
      (byte) 194,
      (byte) 175,
      (byte) 122,
      (byte) 89,
      (byte) 145,
      (byte) 229,
      (byte) 34,
      (byte) 71,
      byte.MaxValue,
      (byte) 218
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 22);
    for (int index = 0; index < 22; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13179()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[7];
      byte[] numArray2 = new byte[7]
      {
        (byte) 124,
        (byte) 199,
        (byte) 49,
        (byte) 164,
        (byte) 247,
        (byte) 1,
        (byte) 21
      };
      byte[] numArray3 = new byte[7]
      {
        (byte) 187,
        (byte) 221,
        (byte) 65,
        (byte) 198,
        (byte) 213,
        (byte) 125,
        (byte) 90
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 7);
      for (int index = 0; index < 7; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[7];
    byte[] numArray5 = new byte[7]
    {
      (byte) 171,
      (byte) 253,
      (byte) 245,
      (byte) 103,
      (byte) 154,
      (byte) 197,
      (byte) 90
    };
    byte[] numArray6 = new byte[7];
    numArray6[6] = (byte) 189;
    numArray6[1] = (byte) 39;
    numArray6[0] = (byte) 147;
    numArray6[3] = (byte) 150;
    numArray6[4] = (byte) 134;
    numArray6[2] = (byte) 94;
    numArray6[5] = (byte) 172;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 7);
    for (int index = 0; index < 7; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13180()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[3];
      byte[] numArray2 = new byte[3]
      {
        (byte) 187,
        (byte) 140,
        (byte) 83
      };
      byte[] numArray3 = new byte[3]
      {
        (byte) 112 /*0x70*/,
        (byte) 12,
        (byte) 180
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 3);
      for (int index = 0; index < 3; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[3];
    byte[] numArray5 = new byte[3]
    {
      (byte) 0,
      (byte) 97,
      (byte) 0
    };
    numArray5[0] = (byte) 254;
    numArray5[2] = (byte) 251;
    byte[] numArray6 = new byte[3]
    {
      (byte) 0,
      (byte) 81,
      (byte) 0
    };
    numArray6[0] = (byte) 106;
    numArray6[2] = (byte) 22;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 3);
    for (int index = 0; index < 3; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13181()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 167,
        (byte) 199,
        (byte) 52,
        (byte) 117,
        (byte) 123,
        (byte) 104,
        (byte) 174,
        (byte) 73,
        (byte) 93,
        (byte) 137
      };
      byte[] numArray3 = new byte[10];
      numArray3[7] = (byte) 198;
      numArray3[1] = (byte) 181;
      numArray3[0] = (byte) 121;
      numArray3[4] = (byte) 10;
      numArray3[6] = (byte) 115;
      numArray3[5] = (byte) 242;
      numArray3[8] = (byte) 58;
      numArray3[9] = (byte) 142;
      numArray3[3] = (byte) 231;
      numArray3[2] = (byte) 196;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10]
    {
      (byte) 105,
      (byte) 137,
      (byte) 147,
      (byte) 174,
      (byte) 101,
      (byte) 4,
      (byte) 4,
      (byte) 55,
      (byte) 30,
      (byte) 46
    };
    byte[] numArray6 = new byte[10]
    {
      (byte) 224 /*0xE0*/,
      (byte) 232,
      (byte) 105,
      (byte) 252,
      byte.MaxValue,
      (byte) 81,
      (byte) 40,
      (byte) 83,
      (byte) 229,
      (byte) 96 /*0x60*/
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13182()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[22];
      byte[] numArray2 = new byte[22]
      {
        (byte) 124,
        (byte) 67,
        (byte) 201,
        (byte) 27,
        (byte) 10,
        (byte) 160 /*0xA0*/,
        (byte) 216,
        (byte) 167,
        (byte) 42,
        (byte) 94,
        (byte) 71,
        (byte) 9,
        (byte) 152,
        (byte) 53,
        (byte) 1,
        (byte) 90,
        (byte) 93,
        (byte) 214,
        (byte) 75,
        (byte) 43,
        (byte) 202,
        (byte) 235
      };
      byte[] numArray3 = new byte[22]
      {
        (byte) 41,
        (byte) 76,
        (byte) 147,
        (byte) 163,
        (byte) 207,
        (byte) 124,
        (byte) 24,
        (byte) 183,
        (byte) 168,
        (byte) 56,
        (byte) 198,
        (byte) 25,
        (byte) 151,
        (byte) 248,
        (byte) 6,
        (byte) 249,
        (byte) 215,
        (byte) 39,
        (byte) 126,
        (byte) 230,
        byte.MaxValue,
        (byte) 40
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 22);
      for (int index = 0; index < 22; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[22];
    byte[] numArray5 = new byte[22];
    numArray5[16 /*0x10*/] = (byte) 193;
    numArray5[12] = (byte) 180;
    numArray5[6] = (byte) 57;
    numArray5[3] = (byte) 53;
    numArray5[7] = (byte) 28;
    numArray5[19] = (byte) 103;
    numArray5[1] = (byte) 89;
    numArray5[14] = (byte) 187;
    numArray5[15] = (byte) 195;
    numArray5[0] = (byte) 69;
    numArray5[10] = (byte) 241;
    numArray5[11] = (byte) 107;
    numArray5[18] = (byte) 53;
    numArray5[13] = (byte) 102;
    numArray5[2] = (byte) 82;
    numArray5[8] = (byte) 30;
    numArray5[20] = (byte) 92;
    numArray5[17] = (byte) 95;
    numArray5[5] = (byte) 232;
    numArray5[9] = (byte) 144 /*0x90*/;
    numArray5[4] = (byte) 139;
    numArray5[21] = (byte) 107;
    byte[] numArray6 = new byte[22];
    numArray6[14] = (byte) 235;
    numArray6[7] = (byte) 123;
    numArray6[10] = (byte) 50;
    numArray6[3] = (byte) 225;
    numArray6[2] = (byte) 53;
    numArray6[1] = (byte) 136;
    numArray6[6] = (byte) 23;
    numArray6[0] = (byte) 95;
    numArray6[5] = (byte) 77;
    numArray6[9] = (byte) 175;
    numArray6[12] = (byte) 165;
    numArray6[11] = (byte) 30;
    numArray6[8] = (byte) 245;
    numArray6[19] = (byte) 254;
    numArray6[16 /*0x10*/] = (byte) 125;
    numArray6[4] = (byte) 24;
    numArray6[15] = (byte) 84;
    numArray6[17] = (byte) 0;
    numArray6[18] = (byte) 117;
    numArray6[13] = (byte) 16 /*0x10*/;
    numArray6[20] = (byte) 217;
    numArray6[21] = (byte) 24;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 22);
    for (int index = 0; index < 22; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13183()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[22];
      byte[] numArray2 = new byte[22]
      {
        (byte) 61,
        (byte) 78,
        (byte) 193,
        (byte) 191,
        (byte) 89,
        (byte) 111,
        (byte) 241,
        (byte) 41,
        (byte) 39,
        (byte) 213,
        (byte) 39,
        (byte) 45,
        (byte) 7,
        (byte) 248,
        (byte) 88,
        (byte) 4,
        (byte) 167,
        (byte) 189,
        (byte) 231,
        (byte) 235,
        (byte) 188,
        (byte) 106
      };
      byte[] numArray3 = new byte[22]
      {
        (byte) 110,
        (byte) 8,
        (byte) 234,
        (byte) 166,
        (byte) 219,
        (byte) 65,
        (byte) 84,
        (byte) 180,
        (byte) 247,
        (byte) 84,
        (byte) 24,
        (byte) 25,
        (byte) 212,
        (byte) 62,
        (byte) 26,
        (byte) 25,
        (byte) 76,
        (byte) 87,
        (byte) 29,
        (byte) 123,
        (byte) 169,
        (byte) 191
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 22);
      for (int index = 0; index < 22; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[20];
      byte[] response = new byte[20];
      Array.Copy((Array) sc_13171.sspq, 36, (Array) numArray4, 0, 20);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_13171.sspr, 36, (Array) numArray4, 0, 20);
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
    byte[] numArray5 = new byte[22];
    byte[] numArray6 = new byte[22]
    {
      (byte) 152,
      (byte) 194,
      (byte) 100,
      (byte) 251,
      (byte) 238,
      (byte) 158,
      (byte) 249,
      (byte) 181,
      (byte) 183,
      (byte) 210,
      (byte) 77,
      (byte) 221,
      (byte) 148,
      (byte) 236,
      (byte) 62,
      (byte) 51,
      (byte) 132,
      (byte) 174,
      (byte) 66,
      (byte) 94,
      (byte) 95,
      (byte) 235
    };
    byte[] numArray7 = new byte[22]
    {
      (byte) 182,
      (byte) 15,
      (byte) 163,
      (byte) 146,
      (byte) 110,
      (byte) 93,
      (byte) 29,
      (byte) 44,
      (byte) 229,
      (byte) 33,
      (byte) 20,
      (byte) 91,
      (byte) 17,
      (byte) 238,
      (byte) 173,
      (byte) 27,
      (byte) 136,
      (byte) 234,
      (byte) 231,
      (byte) 75,
      (byte) 219,
      (byte) 105
    };
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 22);
    for (int index = 0; index < 22; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_appserver_13184()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 196,
        (byte) 107,
        (byte) 3,
        (byte) 91,
        (byte) 118,
        (byte) 101,
        (byte) 79,
        (byte) 13,
        (byte) 212,
        (byte) 110
      };
      byte[] numArray3 = new byte[10]
      {
        (byte) 115,
        (byte) 251,
        (byte) 95,
        (byte) 173,
        (byte) 129,
        (byte) 33,
        (byte) 213,
        (byte) 233,
        (byte) 253,
        (byte) 169
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10];
    numArray5[7] = (byte) 96 /*0x60*/;
    numArray5[4] = (byte) 88;
    numArray5[2] = (byte) 24;
    numArray5[3] = (byte) 100;
    numArray5[1] = (byte) 207;
    numArray5[0] = (byte) 244;
    numArray5[8] = (byte) 12;
    numArray5[6] = (byte) 237;
    numArray5[5] = (byte) 108;
    numArray5[9] = (byte) 61;
    byte[] numArray6 = new byte[10]
    {
      (byte) 156,
      (byte) 71,
      (byte) 200,
      (byte) 252,
      (byte) 21,
      (byte) 58,
      (byte) 123,
      (byte) 243,
      (byte) 243,
      (byte) 202
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13185()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[22];
      byte[] numArray2 = new byte[22];
      numArray2[8] = (byte) 219;
      numArray2[10] = (byte) 77;
      numArray2[2] = (byte) 232;
      numArray2[0] = (byte) 92;
      numArray2[4] = (byte) 134;
      numArray2[5] = (byte) 23;
      numArray2[6] = (byte) 208 /*0xD0*/;
      numArray2[17] = (byte) 83;
      numArray2[1] = (byte) 92;
      numArray2[7] = (byte) 219;
      numArray2[16 /*0x10*/] = (byte) 75;
      numArray2[11] = (byte) 143;
      numArray2[12] = (byte) 70;
      numArray2[13] = (byte) 134;
      numArray2[14] = (byte) 116;
      numArray2[9] = (byte) 36;
      numArray2[3] = (byte) 142;
      numArray2[18] = (byte) 183;
      numArray2[19] = (byte) 46;
      numArray2[15] = (byte) 49;
      numArray2[20] = (byte) 78;
      numArray2[21] = (byte) 126;
      byte[] numArray3 = new byte[22]
      {
        (byte) 228,
        (byte) 137,
        (byte) 232,
        (byte) 196,
        (byte) 218,
        (byte) 77,
        (byte) 6,
        (byte) 49,
        (byte) 120,
        (byte) 68,
        (byte) 124,
        (byte) 75,
        (byte) 87,
        (byte) 106,
        (byte) 185,
        (byte) 179,
        (byte) 32 /*0x20*/,
        (byte) 132,
        (byte) 43,
        (byte) 56,
        (byte) 99,
        (byte) 212
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 22);
      for (int index = 0; index < 22; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[32 /*0x20*/];
      byte[] response = new byte[32 /*0x20*/];
      Array.Copy((Array) sc_13171.sspq, 56, (Array) numArray4, 0, 32 /*0x20*/);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_13171.sspr, 56, (Array) numArray4, 0, 32 /*0x20*/);
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
    byte[] numArray5 = new byte[22];
    byte[] numArray6 = new byte[22];
    numArray6[4] = (byte) 111;
    numArray6[1] = (byte) 0;
    numArray6[2] = (byte) 246;
    numArray6[7] = (byte) 226;
    numArray6[9] = (byte) 43;
    numArray6[19] = (byte) 215;
    numArray6[6] = (byte) 246;
    numArray6[11] = (byte) 166;
    numArray6[0] = (byte) 214;
    numArray6[5] = (byte) 103;
    numArray6[10] = (byte) 207;
    numArray6[8] = (byte) 3;
    numArray6[14] = (byte) 4;
    numArray6[13] = (byte) 161;
    numArray6[12] = (byte) 235;
    numArray6[15] = (byte) 20;
    numArray6[16 /*0x10*/] = (byte) 143;
    numArray6[17] = (byte) 12;
    numArray6[3] = (byte) 252;
    numArray6[20] = (byte) 89;
    numArray6[18] = (byte) 76;
    numArray6[21] = (byte) 152;
    byte[] numArray7 = new byte[22]
    {
      (byte) 229,
      (byte) 71,
      (byte) 29,
      (byte) 13,
      (byte) 127 /*0x7F*/,
      (byte) 41,
      (byte) 86,
      (byte) 94,
      (byte) 201,
      (byte) 13,
      (byte) 155,
      (byte) 85,
      (byte) 199,
      (byte) 108,
      (byte) 184,
      (byte) 28,
      (byte) 130,
      (byte) 234,
      (byte) 210,
      (byte) 135,
      (byte) 96 /*0x60*/,
      (byte) 66
    };
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 22);
    for (int index = 0; index < 22; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_appserver_13186()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[22];
      byte[] numArray2 = new byte[22]
      {
        (byte) 189,
        (byte) 172,
        (byte) 229,
        (byte) 113,
        (byte) 193,
        (byte) 39,
        (byte) 27,
        (byte) 62,
        (byte) 88,
        (byte) 237,
        (byte) 21,
        (byte) 228,
        (byte) 250,
        (byte) 149,
        (byte) 68,
        (byte) 20,
        (byte) 204,
        (byte) 177,
        (byte) 200,
        (byte) 235,
        (byte) 63 /*0x3F*/,
        (byte) 21
      };
      byte[] numArray3 = new byte[22]
      {
        (byte) 43,
        (byte) 212,
        (byte) 111,
        (byte) 218,
        (byte) 202,
        (byte) 96 /*0x60*/,
        (byte) 135,
        (byte) 48 /*0x30*/,
        (byte) 225,
        (byte) 186,
        (byte) 116,
        (byte) 143,
        (byte) 1,
        (byte) 209,
        (byte) 234,
        (byte) 197,
        (byte) 147,
        (byte) 242,
        (byte) 99,
        (byte) 193,
        (byte) 202,
        (byte) 177
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 22);
      for (int index = 0; index < 22; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[22];
    byte[] numArray5 = new byte[22];
    numArray5[18] = (byte) 115;
    numArray5[19] = (byte) 54;
    numArray5[1] = (byte) 154;
    numArray5[3] = (byte) 187;
    numArray5[17] = (byte) 151;
    numArray5[11] = (byte) 9;
    numArray5[6] = (byte) 223;
    numArray5[7] = (byte) 136;
    numArray5[8] = (byte) 148;
    numArray5[2] = (byte) 238;
    numArray5[5] = (byte) 97;
    numArray5[0] = (byte) 224 /*0xE0*/;
    numArray5[21] = (byte) 114;
    numArray5[13] = (byte) 93;
    numArray5[9] = (byte) 163;
    numArray5[15] = (byte) 228;
    numArray5[12] = (byte) 237;
    numArray5[4] = (byte) 86;
    numArray5[14] = (byte) 181;
    numArray5[20] = (byte) 160 /*0xA0*/;
    numArray5[16 /*0x10*/] = (byte) 34;
    numArray5[10] = (byte) 240 /*0xF0*/;
    byte[] numArray6 = new byte[22]
    {
      (byte) 26,
      (byte) 127 /*0x7F*/,
      (byte) 143,
      (byte) 70,
      (byte) 237,
      (byte) 151,
      (byte) 122,
      (byte) 137,
      (byte) 196,
      (byte) 186,
      (byte) 165,
      (byte) 170,
      (byte) 79,
      (byte) 6,
      (byte) 26,
      (byte) 91,
      (byte) 145,
      (byte) 137,
      (byte) 113,
      (byte) 59,
      (byte) 109,
      (byte) 199
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 22);
    for (int index = 0; index < 22; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13187()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[22];
      byte[] numArray2 = new byte[22];
      numArray2[6] = (byte) 229;
      numArray2[14] = (byte) 115;
      numArray2[3] = (byte) 125;
      numArray2[0] = (byte) 19;
      numArray2[4] = (byte) 63 /*0x3F*/;
      numArray2[7] = (byte) 109;
      numArray2[17] = (byte) 247;
      numArray2[1] = (byte) 7;
      numArray2[8] = (byte) 242;
      numArray2[9] = (byte) 201;
      numArray2[10] = (byte) 243;
      numArray2[11] = (byte) 208 /*0xD0*/;
      numArray2[12] = (byte) 106;
      numArray2[13] = (byte) 161;
      numArray2[21] = (byte) 40;
      numArray2[16 /*0x10*/] = (byte) 252;
      numArray2[5] = (byte) 12;
      numArray2[2] = (byte) 28;
      numArray2[18] = (byte) 103;
      numArray2[19] = (byte) 1;
      numArray2[20] = (byte) 124;
      numArray2[15] = (byte) 251;
      byte[] numArray3 = new byte[22]
      {
        (byte) 237,
        (byte) 193,
        (byte) 171,
        (byte) 73,
        (byte) 43,
        (byte) 228,
        (byte) 122,
        (byte) 23,
        byte.MaxValue,
        (byte) 99,
        (byte) 203,
        (byte) 131,
        (byte) 226,
        (byte) 47,
        (byte) 112 /*0x70*/,
        (byte) 161,
        (byte) 166,
        (byte) 56,
        (byte) 239,
        (byte) 116,
        (byte) 179,
        (byte) 48 /*0x30*/
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 22);
      for (int index = 0; index < 22; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[22];
    byte[] numArray5 = new byte[22]
    {
      (byte) 148,
      (byte) 233,
      (byte) 107,
      (byte) 245,
      (byte) 92,
      (byte) 162,
      (byte) 221,
      (byte) 215,
      (byte) 234,
      (byte) 79,
      (byte) 124,
      (byte) 232,
      (byte) 87,
      (byte) 11,
      (byte) 160 /*0xA0*/,
      (byte) 98,
      (byte) 60,
      (byte) 247,
      (byte) 52,
      (byte) 41,
      (byte) 199,
      (byte) 118
    };
    byte[] numArray6 = new byte[22]
    {
      (byte) 4,
      (byte) 129,
      (byte) 144 /*0x90*/,
      (byte) 130,
      (byte) 187,
      (byte) 50,
      (byte) 101,
      (byte) 244,
      (byte) 245,
      (byte) 234,
      (byte) 10,
      (byte) 81,
      (byte) 77,
      (byte) 243,
      (byte) 72,
      (byte) 218,
      (byte) 194,
      (byte) 22,
      (byte) 38,
      (byte) 94,
      (byte) 112 /*0x70*/,
      (byte) 55
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 22);
    for (int index = 0; index < 22; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13188()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 210,
        (byte) 251,
        (byte) 9,
        (byte) 142,
        (byte) 84,
        (byte) 16 /*0x10*/,
        (byte) 100,
        (byte) 198,
        (byte) 108,
        (byte) 91
      };
      byte[] numArray3 = new byte[10]
      {
        (byte) 149,
        (byte) 109,
        (byte) 234,
        (byte) 246,
        (byte) 228,
        (byte) 202,
        (byte) 63 /*0x3F*/,
        (byte) 102,
        (byte) 155,
        (byte) 224 /*0xE0*/
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10];
    numArray5[0] = (byte) 27;
    numArray5[9] = (byte) 101;
    numArray5[2] = (byte) 202;
    numArray5[1] = (byte) 201;
    numArray5[8] = (byte) 138;
    numArray5[5] = byte.MaxValue;
    numArray5[7] = (byte) 98;
    numArray5[3] = (byte) 64 /*0x40*/;
    numArray5[6] = (byte) 65;
    numArray5[4] = (byte) 191;
    byte[] numArray6 = new byte[10]
    {
      (byte) 75,
      (byte) 133,
      (byte) 31 /*0x1F*/,
      (byte) 218,
      (byte) 56,
      (byte) 207,
      (byte) 130,
      (byte) 114,
      (byte) 56,
      (byte) 11
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13189()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[22];
      byte[] numArray2 = new byte[22];
      numArray2[2] = (byte) 108;
      numArray2[1] = (byte) 250;
      numArray2[11] = (byte) 96 /*0x60*/;
      numArray2[3] = (byte) 132;
      numArray2[13] = (byte) 54;
      numArray2[10] = (byte) 24;
      numArray2[7] = (byte) 193;
      numArray2[6] = (byte) 69;
      numArray2[8] = (byte) 124;
      numArray2[9] = (byte) 130;
      numArray2[21] = (byte) 48 /*0x30*/;
      numArray2[16 /*0x10*/] = (byte) 144 /*0x90*/;
      numArray2[12] = (byte) 252;
      numArray2[5] = (byte) 159;
      numArray2[4] = (byte) 144 /*0x90*/;
      numArray2[14] = (byte) 165;
      numArray2[15] = (byte) 127 /*0x7F*/;
      numArray2[17] = (byte) 218;
      numArray2[18] = (byte) 71;
      numArray2[0] = (byte) 92;
      numArray2[20] = (byte) 248;
      numArray2[19] = (byte) 150;
      byte[] numArray3 = new byte[22];
      numArray3[9] = (byte) 212;
      numArray3[1] = (byte) 64 /*0x40*/;
      numArray3[2] = (byte) 1;
      numArray3[5] = (byte) 188;
      numArray3[4] = (byte) 25;
      numArray3[15] = (byte) 124;
      numArray3[6] = (byte) 72;
      numArray3[7] = (byte) 216;
      numArray3[16 /*0x10*/] = (byte) 1;
      numArray3[0] = (byte) 194;
      numArray3[10] = (byte) 155;
      numArray3[3] = (byte) 226;
      numArray3[19] = (byte) 214;
      numArray3[13] = (byte) 253;
      numArray3[12] = (byte) 61;
      numArray3[20] = (byte) 211;
      numArray3[8] = (byte) 78;
      numArray3[17] = (byte) 166;
      numArray3[14] = (byte) 18;
      numArray3[18] = (byte) 109;
      numArray3[11] = (byte) 42;
      numArray3[21] = (byte) 9;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 22);
      for (int index = 0; index < 22; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[22];
    byte[] numArray5 = new byte[22]
    {
      (byte) 225,
      (byte) 236,
      (byte) 211,
      (byte) 8,
      (byte) 92,
      (byte) 118,
      (byte) 169,
      (byte) 80 /*0x50*/,
      byte.MaxValue,
      (byte) 115,
      (byte) 182,
      (byte) 17,
      (byte) 102,
      (byte) 248,
      (byte) 72,
      (byte) 9,
      (byte) 18,
      (byte) 195,
      (byte) 157,
      (byte) 38,
      (byte) 184,
      (byte) 178
    };
    byte[] numArray6 = new byte[22];
    numArray6[2] = (byte) 225;
    numArray6[13] = (byte) 194;
    numArray6[4] = (byte) 181;
    numArray6[1] = (byte) 224 /*0xE0*/;
    numArray6[18] = (byte) 181;
    numArray6[12] = (byte) 115;
    numArray6[6] = (byte) 238;
    numArray6[7] = (byte) 115;
    numArray6[8] = (byte) 124;
    numArray6[9] = (byte) 122;
    numArray6[20] = (byte) 127 /*0x7F*/;
    numArray6[11] = (byte) 206;
    numArray6[10] = (byte) 172;
    numArray6[19] = (byte) 240 /*0xF0*/;
    numArray6[14] = (byte) 52;
    numArray6[15] = (byte) 163;
    numArray6[17] = (byte) 107;
    numArray6[16 /*0x10*/] = (byte) 36;
    numArray6[0] = (byte) 98;
    numArray6[3] = (byte) 3;
    numArray6[21] = (byte) 161;
    numArray6[5] = (byte) 161;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 22);
    for (int index = 0; index < 22; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[49];
    byte[] response = new byte[49];
    Array.Copy((Array) sc_13171.sspq, 88, (Array) numArray7, 0, 49);
    key.Query(true, 335, numArray7, response);
    Array.Copy((Array) sc_13171.sspr, 88, (Array) numArray7, 0, 49);
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

  internal static string ssp_appserver_13190()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 69,
        (byte) 232,
        (byte) 93,
        (byte) 25,
        (byte) 123,
        (byte) 251,
        (byte) 140,
        (byte) 65,
        (byte) 115,
        (byte) 17
      };
      byte[] numArray3 = new byte[10]
      {
        (byte) 30,
        (byte) 19,
        (byte) 158,
        (byte) 37,
        (byte) 14,
        (byte) 28,
        (byte) 82,
        (byte) 77,
        (byte) 241,
        (byte) 80 /*0x50*/
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10];
    numArray5[7] = (byte) 156;
    numArray5[3] = (byte) 16 /*0x10*/;
    numArray5[2] = (byte) 189;
    numArray5[9] = (byte) 51;
    numArray5[4] = (byte) 136;
    numArray5[5] = (byte) 234;
    numArray5[0] = (byte) 120;
    numArray5[1] = (byte) 21;
    numArray5[8] = (byte) 147;
    numArray5[6] = (byte) 36;
    byte[] numArray6 = new byte[10]
    {
      (byte) 223,
      (byte) 27,
      (byte) 49,
      (byte) 240 /*0xF0*/,
      (byte) 43,
      (byte) 231,
      (byte) 23,
      (byte) 210,
      (byte) 86,
      (byte) 253
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13192()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[63 /*0x3F*/];
      byte[] numArray2 = new byte[55];
      numArray2[52] = (byte) 162;
      numArray2[16 /*0x10*/] = (byte) 103;
      numArray2[25] = (byte) 74;
      numArray2[3] = (byte) 157;
      numArray2[26] = (byte) 70;
      numArray2[5] = (byte) 247;
      numArray2[6] = (byte) 66;
      numArray2[7] = (byte) 163;
      numArray2[28] = (byte) 169;
      numArray2[9] = (byte) 64 /*0x40*/;
      numArray2[34] = (byte) 121;
      numArray2[37] = (byte) 134;
      numArray2[12] = (byte) 35;
      numArray2[47] = (byte) 210;
      numArray2[14] = (byte) 133;
      numArray2[20] = (byte) 42;
      numArray2[29] = (byte) 105;
      numArray2[17] = (byte) 52;
      numArray2[38] = (byte) 57;
      numArray2[19] = (byte) 180;
      numArray2[1] = (byte) 192 /*0xC0*/;
      numArray2[21] = (byte) 69;
      numArray2[44] = (byte) 100;
      numArray2[42] = (byte) 145;
      numArray2[36] = (byte) 104;
      numArray2[32 /*0x20*/] = (byte) 215;
      numArray2[2] = (byte) 130;
      numArray2[27] = (byte) 86;
      numArray2[23] = (byte) 160 /*0xA0*/;
      numArray2[45] = (byte) 88;
      numArray2[8] = (byte) 66;
      numArray2[31 /*0x1F*/] = (byte) 36;
      numArray2[30] = (byte) 191;
      numArray2[15] = (byte) 218;
      numArray2[4] = (byte) 99;
      numArray2[35] = (byte) 200;
      numArray2[18] = (byte) 177;
      numArray2[41] = (byte) 143;
      numArray2[13] = (byte) 89;
      numArray2[10] = (byte) 246;
      numArray2[40] = (byte) 240 /*0xF0*/;
      numArray2[22] = (byte) 80 /*0x50*/;
      numArray2[24] = (byte) 198;
      numArray2[43] = (byte) 234;
      numArray2[11] = (byte) 109;
      numArray2[51] = (byte) 154;
      numArray2[46] = (byte) 211;
      numArray2[49] = (byte) 91;
      numArray2[48 /*0x30*/] = (byte) 52;
      numArray2[0] = (byte) 151;
      numArray2[50] = (byte) 234;
      numArray2[39] = (byte) 156;
      numArray2[33] = (byte) 125;
      numArray2[53] = (byte) 137;
      numArray2[54] = (byte) 21;
      byte[] numArray3 = new byte[55];
      numArray3[1] = (byte) 188;
      numArray3[42] = (byte) 141;
      numArray3[7] = (byte) 202;
      numArray3[2] = (byte) 165;
      numArray3[4] = (byte) 78;
      numArray3[5] = (byte) 125;
      numArray3[46] = (byte) 75;
      numArray3[37] = (byte) 41;
      numArray3[3] = (byte) 171;
      numArray3[32 /*0x20*/] = (byte) 174;
      numArray3[43] = (byte) 144 /*0x90*/;
      numArray3[11] = (byte) 10;
      numArray3[18] = (byte) 213;
      numArray3[13] = (byte) 241;
      numArray3[54] = (byte) 194;
      numArray3[9] = (byte) 200;
      numArray3[14] = (byte) 32 /*0x20*/;
      numArray3[30] = (byte) 136;
      numArray3[0] = (byte) 229;
      numArray3[26] = (byte) 15;
      numArray3[20] = (byte) 101;
      numArray3[21] = (byte) 210;
      numArray3[16 /*0x10*/] = (byte) 70;
      numArray3[23] = (byte) 213;
      numArray3[24] = (byte) 248;
      numArray3[25] = (byte) 27;
      numArray3[53] = (byte) 216;
      numArray3[6] = (byte) 153;
      numArray3[28] = (byte) 178;
      numArray3[27] = (byte) 23;
      numArray3[22] = (byte) 148;
      numArray3[12] = (byte) 165;
      numArray3[8] = (byte) 95;
      numArray3[33] = (byte) 81;
      numArray3[47] = (byte) 112 /*0x70*/;
      numArray3[35] = (byte) 206;
      numArray3[49] = (byte) 130;
      numArray3[38] = (byte) 48 /*0x30*/;
      numArray3[29] = (byte) 213;
      numArray3[39] = (byte) 204;
      numArray3[40] = (byte) 115;
      numArray3[41] = (byte) 152;
      numArray3[10] = (byte) 182;
      numArray3[31 /*0x1F*/] = (byte) 190;
      numArray3[15] = (byte) 51;
      numArray3[45] = (byte) 158;
      numArray3[50] = (byte) 190;
      numArray3[17] = (byte) 188;
      numArray3[48 /*0x30*/] = (byte) 27;
      numArray3[19] = (byte) 247;
      numArray3[44] = (byte) 85;
      numArray3[51] = (byte) 179;
      numArray3[52] = byte.MaxValue;
      numArray3[34] = (byte) 93;
      numArray3[36] = (byte) 172;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[8]
      {
        (byte) 253,
        (byte) 190,
        (byte) 89,
        (byte) 185,
        (byte) 32 /*0x20*/,
        (byte) 72,
        (byte) 229,
        (byte) 216
      };
      byte[] numArray5 = new byte[8]
      {
        (byte) 207,
        (byte) 129,
        (byte) 216,
        (byte) 69,
        (byte) 6,
        (byte) 89,
        (byte) 201,
        (byte) 30
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 8);
      for (int index = 0; index < 8; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[41];
      byte[] response = new byte[41];
      Array.Copy((Array) sc_13171.sspq, 137, (Array) numArray6, 0, 41);
      key.Query(true, 335, numArray6, response);
      Array.Copy((Array) sc_13171.sspr, 137, (Array) numArray6, 0, 41);
      for (int index = 0; index < numArray6.Length; ++index)
      {
        if ((int) numArray6[index] != (int) response[index])
        {
          key.TagValue = (int) response[index];
          break;
        }
      }
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray7 = new byte[63 /*0x3F*/];
    byte[] numArray8 = new byte[55]
    {
      (byte) 2,
      (byte) 25,
      (byte) 153,
      (byte) 142,
      (byte) 1,
      (byte) 111,
      (byte) 137,
      (byte) 226,
      (byte) 73,
      (byte) 27,
      (byte) 202,
      (byte) 61,
      (byte) 214,
      (byte) 68,
      (byte) 206,
      (byte) 42,
      (byte) 100,
      (byte) 52,
      (byte) 129,
      (byte) 109,
      (byte) 121,
      (byte) 36,
      (byte) 190,
      (byte) 121,
      (byte) 116,
      (byte) 180,
      (byte) 64 /*0x40*/,
      (byte) 151,
      (byte) 115,
      (byte) 173,
      (byte) 80 /*0x50*/,
      (byte) 162,
      (byte) 0,
      (byte) 28,
      (byte) 230,
      (byte) 43,
      (byte) 71,
      (byte) 248,
      (byte) 36,
      (byte) 136,
      (byte) 142,
      (byte) 190,
      (byte) 47,
      (byte) 23,
      (byte) 31 /*0x1F*/,
      (byte) 126,
      (byte) 203,
      (byte) 197,
      (byte) 48 /*0x30*/,
      (byte) 199,
      (byte) 137,
      (byte) 56,
      (byte) 107,
      (byte) 19,
      (byte) 226
    };
    byte[] numArray9 = new byte[55]
    {
      (byte) 152,
      (byte) 218,
      (byte) 161,
      (byte) 150,
      (byte) 198,
      (byte) 37,
      (byte) 104,
      (byte) 113,
      (byte) 49,
      (byte) 167,
      (byte) 27,
      (byte) 207,
      (byte) 73,
      (byte) 116,
      (byte) 231,
      (byte) 159,
      (byte) 38,
      (byte) 87,
      (byte) 136,
      (byte) 0,
      (byte) 120,
      (byte) 102,
      (byte) 59,
      (byte) 252,
      (byte) 111,
      (byte) 184,
      (byte) 130,
      (byte) 32 /*0x20*/,
      (byte) 66,
      (byte) 86,
      (byte) 48 /*0x30*/,
      (byte) 48 /*0x30*/,
      (byte) 189,
      (byte) 65,
      (byte) 192 /*0xC0*/,
      (byte) 39,
      (byte) 22,
      (byte) 246,
      (byte) 35,
      (byte) 164,
      (byte) 153,
      (byte) 173,
      (byte) 230,
      (byte) 179,
      (byte) 154,
      (byte) 6,
      (byte) 210,
      (byte) 50,
      (byte) 251,
      (byte) 239,
      (byte) 218,
      (byte) 7,
      (byte) 135,
      (byte) 212,
      (byte) 52
    };
    key.Query(true, 335, numArray8, numArray8);
    Array.Copy((Array) numArray8, 0, (Array) numArray7, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray7[index] ^= numArray9[index];
    byte[] numArray10 = new byte[8]
    {
      (byte) 32 /*0x20*/,
      (byte) 26,
      (byte) 77,
      (byte) 46,
      (byte) 242,
      (byte) 110,
      (byte) 155,
      (byte) 203
    };
    byte[] numArray11 = new byte[8]
    {
      (byte) 164,
      (byte) 99,
      (byte) 104,
      (byte) 87,
      (byte) 72,
      (byte) 141,
      (byte) 50,
      (byte) 6
    };
    key.Query(true, 335, numArray10, numArray10);
    Array.Copy((Array) numArray10, 0, (Array) numArray7, 55, 8);
    for (int index = 0; index < 8; ++index)
      numArray7[index + 55] ^= numArray11[index];
    return Encoding.UTF8.GetString(numArray7);
  }

  internal static string ssp_appserver_13193()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 162,
        (byte) 226,
        (byte) 107,
        (byte) 16 /*0x10*/,
        (byte) 249,
        (byte) 223,
        (byte) 186,
        (byte) 244,
        (byte) 166,
        (byte) 90
      };
      byte[] numArray3 = new byte[10];
      numArray3[7] = (byte) 41;
      numArray3[0] = (byte) 28;
      numArray3[9] = (byte) 162;
      numArray3[3] = (byte) 176 /*0xB0*/;
      numArray3[2] = (byte) 193;
      numArray3[1] = (byte) 122;
      numArray3[6] = (byte) 143;
      numArray3[5] = (byte) 87;
      numArray3[8] = (byte) 5;
      numArray3[4] = (byte) 98;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[41];
      byte[] response = new byte[41];
      Array.Copy((Array) sc_13171.sspq, 178, (Array) numArray4, 0, 41);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_13171.sspr, 178, (Array) numArray4, 0, 41);
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
    byte[] numArray5 = new byte[10];
    byte[] numArray6 = new byte[10]
    {
      (byte) 181,
      (byte) 215,
      (byte) 19,
      (byte) 216,
      (byte) 198,
      (byte) 6,
      (byte) 239,
      (byte) 196,
      (byte) 185,
      (byte) 18
    };
    byte[] numArray7 = new byte[10]
    {
      (byte) 225,
      (byte) 152,
      (byte) 36,
      (byte) 35,
      (byte) 214,
      (byte) 100,
      (byte) 110,
      (byte) 15,
      (byte) 169,
      (byte) 230
    };
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray5[index] ^= numArray7[index];
    byte[] numArray8 = new byte[30];
    byte[] response1 = new byte[30];
    Array.Copy((Array) sc_13171.sspq, 219, (Array) numArray8, 0, 30);
    key.Query(true, 335, numArray8, response1);
    Array.Copy((Array) sc_13171.sspr, 219, (Array) numArray8, 0, 30);
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

  internal static string ssp_appserver_13194()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 181,
        (byte) 88,
        (byte) 209,
        (byte) 197,
        (byte) 250,
        (byte) 51,
        (byte) 220,
        (byte) 68,
        (byte) 193,
        (byte) 88
      };
      byte[] numArray3 = new byte[10];
      numArray3[8] = (byte) 53;
      numArray3[6] = (byte) 127 /*0x7F*/;
      numArray3[2] = (byte) 151;
      numArray3[7] = (byte) 234;
      numArray3[4] = (byte) 101;
      numArray3[5] = (byte) 166;
      numArray3[1] = (byte) 36;
      numArray3[9] = (byte) 212;
      numArray3[0] = (byte) 92;
      numArray3[3] = (byte) 236;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10]
    {
      (byte) 75,
      (byte) 230,
      (byte) 5,
      (byte) 92,
      (byte) 145,
      (byte) 171,
      (byte) 212,
      (byte) 221,
      (byte) 89,
      (byte) 36
    };
    byte[] numArray6 = new byte[10]
    {
      (byte) 81,
      (byte) 72,
      (byte) 12,
      (byte) 83,
      (byte) 168,
      (byte) 129,
      (byte) 7,
      (byte) 58,
      (byte) 42,
      (byte) 127 /*0x7F*/
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13195()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[29];
      byte[] numArray2 = new byte[29];
      numArray2[9] = (byte) 157;
      numArray2[1] = (byte) 216;
      numArray2[2] = (byte) 189;
      numArray2[3] = (byte) 225;
      numArray2[14] = (byte) 57;
      numArray2[0] = (byte) 41;
      numArray2[28] = (byte) 107;
      numArray2[4] = (byte) 68;
      numArray2[8] = (byte) 70;
      numArray2[6] = (byte) 11;
      numArray2[16 /*0x10*/] = (byte) 25;
      numArray2[11] = (byte) 97;
      numArray2[12] = (byte) 253;
      numArray2[13] = (byte) 195;
      numArray2[25] = (byte) 153;
      numArray2[27] = (byte) 123;
      numArray2[15] = (byte) 205;
      numArray2[17] = (byte) 161;
      numArray2[22] = (byte) 244;
      numArray2[19] = (byte) 90;
      numArray2[18] = (byte) 243;
      numArray2[26] = (byte) 237;
      numArray2[10] = (byte) 151;
      numArray2[5] = (byte) 152;
      numArray2[24] = (byte) 74;
      numArray2[20] = (byte) 134;
      numArray2[23] = (byte) 214;
      numArray2[7] = (byte) 169;
      numArray2[21] = (byte) 223;
      byte[] numArray3 = new byte[29]
      {
        (byte) 217,
        (byte) 60,
        (byte) 198,
        (byte) 164,
        (byte) 140,
        (byte) 61,
        (byte) 173,
        (byte) 230,
        (byte) 186,
        (byte) 254,
        (byte) 212,
        (byte) 151,
        (byte) 211,
        (byte) 176 /*0xB0*/,
        (byte) 42,
        (byte) 235,
        (byte) 221,
        (byte) 221,
        (byte) 44,
        (byte) 188,
        byte.MaxValue,
        (byte) 184,
        (byte) 122,
        (byte) 135,
        (byte) 150,
        (byte) 233,
        (byte) 41,
        (byte) 187,
        (byte) 47
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 29);
      for (int index = 0; index < 29; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[42];
      byte[] response = new byte[42];
      Array.Copy((Array) sc_13171.sspq, 249, (Array) numArray4, 0, 42);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_13171.sspr, 249, (Array) numArray4, 0, 42);
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
    byte[] numArray5 = new byte[29];
    byte[] numArray6 = new byte[29]
    {
      (byte) 201,
      (byte) 100,
      (byte) 233,
      (byte) 72,
      (byte) 15,
      (byte) 85,
      (byte) 9,
      (byte) 87,
      (byte) 212,
      (byte) 124,
      (byte) 192 /*0xC0*/,
      (byte) 206,
      (byte) 183,
      (byte) 94,
      (byte) 13,
      (byte) 137,
      (byte) 127 /*0x7F*/,
      (byte) 76,
      (byte) 206,
      (byte) 95,
      (byte) 234,
      (byte) 157,
      (byte) 203,
      (byte) 170,
      (byte) 75,
      (byte) 6,
      (byte) 128 /*0x80*/,
      (byte) 197,
      (byte) 154
    };
    byte[] numArray7 = new byte[29]
    {
      (byte) 177,
      (byte) 237,
      (byte) 20,
      (byte) 247,
      (byte) 100,
      (byte) 183,
      (byte) 27,
      (byte) 108,
      (byte) 53,
      (byte) 174,
      (byte) 44,
      (byte) 105,
      (byte) 142,
      (byte) 231,
      (byte) 215,
      (byte) 90,
      (byte) 82,
      (byte) 100,
      (byte) 136,
      (byte) 42,
      (byte) 126,
      (byte) 246,
      (byte) 108,
      (byte) 214,
      (byte) 35,
      (byte) 120,
      (byte) 87,
      (byte) 164,
      (byte) 221
    };
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 29);
    for (int index = 0; index < 29; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_appserver_13196()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[22];
      byte[] numArray2 = new byte[22]
      {
        (byte) 84,
        (byte) 22,
        (byte) 178,
        (byte) 3,
        (byte) 186,
        (byte) 62,
        (byte) 37,
        (byte) 131,
        (byte) 221,
        (byte) 81,
        (byte) 94,
        (byte) 228,
        (byte) 176 /*0xB0*/,
        (byte) 170,
        (byte) 4,
        (byte) 109,
        (byte) 103,
        (byte) 195,
        (byte) 73,
        (byte) 151,
        (byte) 196,
        (byte) 153
      };
      byte[] numArray3 = new byte[22]
      {
        (byte) 152,
        (byte) 129,
        (byte) 203,
        (byte) 152,
        (byte) 79,
        (byte) 132,
        (byte) 185,
        (byte) 181,
        (byte) 192 /*0xC0*/,
        (byte) 34,
        (byte) 167,
        (byte) 61,
        (byte) 97,
        (byte) 40,
        (byte) 102,
        (byte) 79,
        (byte) 233,
        (byte) 79,
        (byte) 101,
        (byte) 37,
        (byte) 190,
        (byte) 14
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 22);
      for (int index = 0; index < 22; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[22];
    byte[] numArray5 = new byte[22]
    {
      (byte) 252,
      (byte) 9,
      (byte) 212,
      (byte) 68,
      (byte) 188,
      (byte) 4,
      (byte) 247,
      (byte) 59,
      (byte) 22,
      (byte) 98,
      (byte) 72,
      (byte) 91,
      (byte) 134,
      (byte) 196,
      (byte) 13,
      (byte) 168,
      (byte) 219,
      (byte) 12,
      (byte) 75,
      (byte) 109,
      (byte) 197,
      (byte) 155
    };
    byte[] numArray6 = new byte[22];
    numArray6[17] = (byte) 103;
    numArray6[5] = (byte) 30;
    numArray6[14] = (byte) 140;
    numArray6[3] = (byte) 165;
    numArray6[4] = (byte) 95;
    numArray6[1] = (byte) 138;
    numArray6[6] = (byte) 242;
    numArray6[0] = (byte) 8;
    numArray6[8] = (byte) 74;
    numArray6[7] = (byte) 143;
    numArray6[10] = (byte) 98;
    numArray6[2] = (byte) 225;
    numArray6[20] = (byte) 19;
    numArray6[13] = (byte) 111;
    numArray6[15] = (byte) 175;
    numArray6[12] = (byte) 91;
    numArray6[11] = (byte) 42;
    numArray6[9] = (byte) 134;
    numArray6[16 /*0x10*/] = (byte) 51;
    numArray6[19] = (byte) 122;
    numArray6[18] = (byte) 213;
    numArray6[21] = (byte) 73;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 22);
    for (int index = 0; index < 22; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13197()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[7];
      byte[] numArray2 = new byte[7];
      numArray2[6] = (byte) 121;
      numArray2[1] = (byte) 238;
      numArray2[2] = (byte) 232;
      numArray2[0] = (byte) 59;
      numArray2[4] = (byte) 224 /*0xE0*/;
      numArray2[5] = (byte) 43;
      numArray2[3] = (byte) 140;
      byte[] numArray3 = new byte[7]
      {
        (byte) 36,
        (byte) 47,
        (byte) 28,
        (byte) 226,
        (byte) 8,
        (byte) 172,
        (byte) 110
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 7);
      for (int index = 0; index < 7; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[7];
    byte[] numArray5 = new byte[7]
    {
      (byte) 94,
      (byte) 64 /*0x40*/,
      (byte) 194,
      (byte) 5,
      (byte) 216,
      (byte) 88,
      (byte) 157
    };
    byte[] numArray6 = new byte[7]
    {
      (byte) 233,
      (byte) 184,
      (byte) 65,
      (byte) 140,
      (byte) 112 /*0x70*/,
      (byte) 129,
      (byte) 214
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 7);
    for (int index = 0; index < 7; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13198()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[3];
      byte[] numArray2 = new byte[3]
      {
        (byte) 1,
        (byte) 19,
        (byte) 191
      };
      byte[] numArray3 = new byte[3]
      {
        (byte) 26,
        (byte) 131,
        (byte) 133
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 3);
      for (int index = 0; index < 3; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[3];
    byte[] numArray5 = new byte[3]
    {
      byte.MaxValue,
      byte.MaxValue,
      (byte) 10
    };
    byte[] numArray6 = new byte[3]
    {
      (byte) 0,
      (byte) 0,
      (byte) 192 /*0xC0*/
    };
    numArray6[0] = (byte) 73;
    numArray6[1] = (byte) 9;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 3);
    for (int index = 0; index < 3; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13199()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10];
      numArray2[7] = (byte) 94;
      numArray2[6] = (byte) 161;
      numArray2[3] = (byte) 171;
      numArray2[0] = (byte) 81;
      numArray2[4] = (byte) 92;
      numArray2[5] = (byte) 119;
      numArray2[8] = (byte) 208 /*0xD0*/;
      numArray2[1] = (byte) 151;
      numArray2[2] = (byte) 83;
      numArray2[9] = (byte) 30;
      byte[] numArray3 = new byte[10]
      {
        (byte) 99,
        (byte) 79,
        (byte) 201,
        (byte) 124,
        (byte) 163,
        (byte) 125,
        (byte) 166,
        (byte) 107,
        (byte) 124,
        (byte) 42
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10]
    {
      (byte) 236,
      (byte) 187,
      (byte) 77,
      (byte) 23,
      (byte) 119,
      (byte) 84,
      (byte) 81,
      (byte) 120,
      (byte) 167,
      (byte) 79
    };
    byte[] numArray6 = new byte[10]
    {
      (byte) 43,
      (byte) 0,
      (byte) 166,
      (byte) 8,
      (byte) 95,
      (byte) 186,
      (byte) 206,
      (byte) 54,
      (byte) 9,
      (byte) 160 /*0xA0*/
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13200()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[115];
      byte[] numArray2 = new byte[55];
      numArray2[9] = (byte) 106;
      numArray2[1] = (byte) 191;
      numArray2[41] = (byte) 218;
      numArray2[33] = (byte) 53;
      numArray2[4] = (byte) 97;
      numArray2[5] = (byte) 117;
      numArray2[30] = (byte) 52;
      numArray2[32 /*0x20*/] = (byte) 77;
      numArray2[18] = (byte) 171;
      numArray2[7] = (byte) 18;
      numArray2[27] = (byte) 166;
      numArray2[47] = (byte) 135;
      numArray2[2] = (byte) 137;
      numArray2[13] = (byte) 202;
      numArray2[14] = (byte) 237;
      numArray2[15] = (byte) 254;
      numArray2[25] = (byte) 9;
      numArray2[8] = (byte) 156;
      numArray2[29] = (byte) 20;
      numArray2[38] = (byte) 56;
      numArray2[20] = (byte) 102;
      numArray2[21] = (byte) 239;
      numArray2[12] = (byte) 40;
      numArray2[23] = (byte) 95;
      numArray2[24] = (byte) 41;
      numArray2[26] = (byte) 127 /*0x7F*/;
      numArray2[3] = (byte) 114;
      numArray2[17] = (byte) 93;
      numArray2[36] = (byte) 213;
      numArray2[53] = (byte) 244;
      numArray2[48 /*0x30*/] = (byte) 139;
      numArray2[31 /*0x1F*/] = (byte) 0;
      numArray2[54] = (byte) 68;
      numArray2[52] = (byte) 160 /*0xA0*/;
      numArray2[6] = (byte) 51;
      numArray2[19] = (byte) 95;
      numArray2[22] = (byte) 15;
      numArray2[37] = (byte) 39;
      numArray2[28] = (byte) 82;
      numArray2[39] = (byte) 49;
      numArray2[40] = (byte) 4;
      numArray2[0] = (byte) 90;
      numArray2[42] = (byte) 31 /*0x1F*/;
      numArray2[43] = (byte) 226;
      numArray2[34] = (byte) 202;
      numArray2[45] = (byte) 234;
      numArray2[46] = (byte) 44;
      numArray2[44] = (byte) 89;
      numArray2[16 /*0x10*/] = (byte) 239;
      numArray2[49] = (byte) 169;
      numArray2[10] = (byte) 190;
      numArray2[51] = (byte) 3;
      numArray2[35] = (byte) 158;
      numArray2[11] = (byte) 141;
      numArray2[50] = (byte) 96 /*0x60*/;
      byte[] numArray3 = new byte[55];
      numArray3[44] = (byte) 24;
      numArray3[24] = (byte) 49;
      numArray3[27] = (byte) 1;
      numArray3[21] = (byte) 240 /*0xF0*/;
      numArray3[14] = (byte) 249;
      numArray3[15] = (byte) 35;
      numArray3[6] = (byte) 19;
      numArray3[26] = (byte) 141;
      numArray3[8] = (byte) 183;
      numArray3[4] = (byte) 37;
      numArray3[12] = (byte) 129;
      numArray3[38] = (byte) 229;
      numArray3[53] = (byte) 38;
      numArray3[13] = (byte) 47;
      numArray3[2] = (byte) 205;
      numArray3[18] = (byte) 64 /*0x40*/;
      numArray3[16 /*0x10*/] = (byte) 5;
      numArray3[50] = (byte) 82;
      numArray3[43] = (byte) 156;
      numArray3[19] = (byte) 93;
      numArray3[20] = (byte) 226;
      numArray3[49] = (byte) 125;
      numArray3[32 /*0x20*/] = (byte) 159;
      numArray3[23] = (byte) 232;
      numArray3[1] = (byte) 163;
      numArray3[25] = (byte) 176 /*0xB0*/;
      numArray3[9] = (byte) 113;
      numArray3[51] = (byte) 60;
      numArray3[3] = (byte) 134;
      numArray3[48 /*0x30*/] = (byte) 190;
      numArray3[5] = (byte) 20;
      numArray3[11] = (byte) 222;
      numArray3[41] = (byte) 108;
      numArray3[33] = (byte) 160 /*0xA0*/;
      numArray3[34] = (byte) 222;
      numArray3[7] = (byte) 132;
      numArray3[36] = (byte) 12;
      numArray3[37] = (byte) 191;
      numArray3[35] = (byte) 197;
      numArray3[29] = (byte) 131;
      numArray3[40] = (byte) 186;
      numArray3[30] = (byte) 98;
      numArray3[42] = (byte) 3;
      numArray3[46] = (byte) 56;
      numArray3[39] = (byte) 98;
      numArray3[45] = (byte) 199;
      numArray3[31 /*0x1F*/] = (byte) 244;
      numArray3[47] = (byte) 22;
      numArray3[28] = (byte) 248;
      numArray3[54] = (byte) 165;
      numArray3[0] = (byte) 251;
      numArray3[17] = (byte) 153;
      numArray3[52] = (byte) 232;
      numArray3[10] = (byte) 103;
      numArray3[22] = (byte) 28;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 183,
        (byte) 114,
        (byte) 214,
        (byte) 142,
        (byte) 147,
        (byte) 238,
        (byte) 184,
        (byte) 177,
        (byte) 35,
        (byte) 39,
        (byte) 202,
        (byte) 220,
        (byte) 245,
        (byte) 247,
        (byte) 18,
        (byte) 222,
        (byte) 2,
        (byte) 224 /*0xE0*/,
        (byte) 112 /*0x70*/,
        (byte) 118,
        (byte) 213,
        (byte) 47,
        (byte) 211,
        (byte) 109,
        (byte) 138,
        (byte) 3,
        (byte) 40,
        (byte) 108,
        (byte) 184,
        (byte) 83,
        (byte) 11,
        (byte) 243,
        (byte) 100,
        (byte) 103,
        (byte) 206,
        (byte) 100,
        (byte) 142,
        (byte) 173,
        (byte) 112 /*0x70*/,
        (byte) 20,
        (byte) 10,
        (byte) 248,
        (byte) 24,
        (byte) 154,
        (byte) 108,
        (byte) 10,
        (byte) 230,
        (byte) 9,
        (byte) 155,
        (byte) 187,
        (byte) 227,
        (byte) 211,
        (byte) 212,
        (byte) 234,
        (byte) 46
      };
      byte[] numArray5 = new byte[55]
      {
        (byte) 100,
        (byte) 128 /*0x80*/,
        (byte) 244,
        (byte) 250,
        (byte) 54,
        (byte) 9,
        (byte) 158,
        (byte) 84,
        (byte) 182,
        (byte) 103,
        (byte) 180,
        (byte) 215,
        (byte) 187,
        (byte) 1,
        (byte) 114,
        (byte) 221,
        (byte) 169,
        (byte) 233,
        (byte) 203,
        (byte) 182,
        (byte) 114,
        (byte) 143,
        (byte) 99,
        (byte) 162,
        (byte) 180,
        (byte) 110,
        (byte) 208 /*0xD0*/,
        (byte) 29,
        (byte) 73,
        (byte) 37,
        (byte) 153,
        (byte) 218,
        (byte) 191,
        (byte) 212,
        (byte) 93,
        (byte) 122,
        (byte) 34,
        (byte) 64 /*0x40*/,
        (byte) 102,
        (byte) 67,
        (byte) 190,
        (byte) 33,
        (byte) 184,
        (byte) 226,
        (byte) 136,
        (byte) 47,
        (byte) 161,
        (byte) 118,
        (byte) 135,
        (byte) 25,
        (byte) 141,
        (byte) 42,
        (byte) 184,
        (byte) 42,
        (byte) 138
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[5]
      {
        (byte) 105,
        (byte) 3,
        (byte) 143,
        (byte) 197,
        (byte) 75
      };
      byte[] numArray7 = new byte[5]
      {
        byte.MaxValue,
        (byte) 254,
        (byte) 174,
        (byte) 82,
        (byte) 140
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 5);
      for (int index = 0; index < 5; ++index)
        numArray1[index + 110] ^= numArray7[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray8 = new byte[115];
    byte[] numArray9 = new byte[55]
    {
      (byte) 214,
      (byte) 119,
      (byte) 141,
      (byte) 40,
      (byte) 174,
      (byte) 79,
      (byte) 84,
      (byte) 111,
      (byte) 92,
      (byte) 107,
      (byte) 221,
      (byte) 23,
      (byte) 57,
      (byte) 0,
      (byte) 128 /*0x80*/,
      (byte) 29,
      (byte) 31 /*0x1F*/,
      (byte) 208 /*0xD0*/,
      (byte) 252,
      (byte) 111,
      (byte) 56,
      (byte) 31 /*0x1F*/,
      (byte) 121,
      (byte) 57,
      (byte) 70,
      (byte) 198,
      (byte) 37,
      (byte) 170,
      (byte) 225,
      (byte) 78,
      (byte) 191,
      (byte) 254,
      (byte) 72,
      (byte) 126,
      (byte) 150,
      (byte) 146,
      (byte) 88,
      (byte) 224 /*0xE0*/,
      (byte) 237,
      (byte) 107,
      (byte) 143,
      (byte) 99,
      (byte) 119,
      (byte) 127 /*0x7F*/,
      (byte) 80 /*0x50*/,
      (byte) 113,
      (byte) 100,
      (byte) 104,
      (byte) 131,
      (byte) 114,
      (byte) 165,
      (byte) 122,
      (byte) 252,
      (byte) 96 /*0x60*/,
      (byte) 227
    };
    byte[] numArray10 = new byte[55]
    {
      (byte) 136,
      (byte) 8,
      (byte) 113,
      (byte) 133,
      (byte) 127 /*0x7F*/,
      (byte) 145,
      (byte) 159,
      (byte) 131,
      (byte) 49,
      (byte) 180,
      (byte) 194,
      (byte) 8,
      (byte) 166,
      (byte) 220,
      (byte) 74,
      (byte) 89,
      (byte) 88,
      (byte) 11,
      (byte) 210,
      (byte) 117,
      (byte) 203,
      (byte) 185,
      (byte) 217,
      (byte) 142,
      (byte) 131,
      (byte) 87,
      (byte) 158,
      (byte) 238,
      (byte) 221,
      (byte) 244,
      (byte) 75,
      (byte) 10,
      (byte) 157,
      (byte) 233,
      (byte) 64 /*0x40*/,
      (byte) 101,
      (byte) 188,
      (byte) 94,
      (byte) 144 /*0x90*/,
      (byte) 161,
      (byte) 97,
      (byte) 14,
      (byte) 243,
      (byte) 157,
      (byte) 189,
      (byte) 224 /*0xE0*/,
      (byte) 136,
      (byte) 63 /*0x3F*/,
      (byte) 217,
      (byte) 176 /*0xB0*/,
      (byte) 71,
      (byte) 189,
      (byte) 94,
      (byte) 247,
      (byte) 121
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray8, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index] ^= numArray10[index];
    byte[] numArray11 = new byte[55]
    {
      (byte) 83,
      (byte) 226,
      (byte) 232,
      (byte) 42,
      (byte) 170,
      (byte) 142,
      (byte) 13,
      (byte) 171,
      (byte) 222,
      (byte) 206,
      (byte) 186,
      (byte) 106,
      (byte) 238,
      (byte) 196,
      (byte) 63 /*0x3F*/,
      (byte) 227,
      (byte) 144 /*0x90*/,
      (byte) 224 /*0xE0*/,
      (byte) 213,
      (byte) 167,
      (byte) 249,
      (byte) 232,
      (byte) 213,
      (byte) 97,
      (byte) 188,
      (byte) 38,
      (byte) 110,
      (byte) 168,
      (byte) 68,
      (byte) 2,
      (byte) 231,
      (byte) 155,
      (byte) 222,
      (byte) 147,
      (byte) 4,
      (byte) 253,
      (byte) 5,
      (byte) 65,
      (byte) 228,
      (byte) 212,
      (byte) 105,
      (byte) 211,
      (byte) 114,
      (byte) 162,
      (byte) 170,
      (byte) 253,
      (byte) 149,
      (byte) 173,
      (byte) 102,
      (byte) 151,
      (byte) 108,
      (byte) 128 /*0x80*/,
      (byte) 14,
      (byte) 15,
      (byte) 4
    };
    byte[] numArray12 = new byte[55];
    numArray12[13] = (byte) 37;
    numArray12[1] = (byte) 163;
    numArray12[2] = (byte) 49;
    numArray12[9] = (byte) 152;
    numArray12[45] = byte.MaxValue;
    numArray12[40] = (byte) 158;
    numArray12[6] = (byte) 78;
    numArray12[7] = (byte) 78;
    numArray12[18] = (byte) 219;
    numArray12[5] = (byte) 89;
    numArray12[16 /*0x10*/] = (byte) 169;
    numArray12[11] = (byte) 26;
    numArray12[41] = (byte) 60;
    numArray12[8] = (byte) 231;
    numArray12[12] = (byte) 245;
    numArray12[21] = (byte) 191;
    numArray12[50] = (byte) 46;
    numArray12[17] = (byte) 232;
    numArray12[36] = (byte) 225;
    numArray12[19] = (byte) 131;
    numArray12[30] = (byte) 196;
    numArray12[20] = (byte) 88;
    numArray12[22] = (byte) 152;
    numArray12[3] = (byte) 127 /*0x7F*/;
    numArray12[24] = (byte) 21;
    numArray12[4] = (byte) 155;
    numArray12[54] = (byte) 139;
    numArray12[27] = (byte) 46;
    numArray12[28] = (byte) 245;
    numArray12[10] = (byte) 117;
    numArray12[14] = (byte) 35;
    numArray12[32 /*0x20*/] = (byte) 206;
    numArray12[31 /*0x1F*/] = (byte) 34;
    numArray12[0] = (byte) 36;
    numArray12[34] = (byte) 104;
    numArray12[35] = (byte) 245;
    numArray12[25] = (byte) 47;
    numArray12[37] = (byte) 186;
    numArray12[15] = (byte) 215;
    numArray12[39] = (byte) 156;
    numArray12[23] = (byte) 213;
    numArray12[47] = (byte) 103;
    numArray12[42] = (byte) 206;
    numArray12[33] = (byte) 39;
    numArray12[44] = (byte) 131;
    numArray12[43] = (byte) 64 /*0x40*/;
    numArray12[52] = (byte) 235;
    numArray12[26] = (byte) 38;
    numArray12[29] = (byte) 205;
    numArray12[46] = (byte) 91;
    numArray12[49] = byte.MaxValue;
    numArray12[51] = (byte) 91;
    numArray12[38] = (byte) 245;
    numArray12[53] = (byte) 189;
    numArray12[48 /*0x30*/] = (byte) 135;
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray8, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index + 55] ^= numArray12[index];
    byte[] numArray13 = new byte[5]
    {
      (byte) 207,
      (byte) 174,
      (byte) 242,
      (byte) 95,
      (byte) 34
    };
    byte[] numArray14 = new byte[5]
    {
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 184,
      (byte) 0
    };
    numArray14[0] = (byte) 4;
    numArray14[2] = (byte) 232;
    numArray14[1] = (byte) 204;
    numArray14[4] = (byte) 2;
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray8, 110, 5);
    for (int index = 0; index < 5; ++index)
      numArray8[index + 110] ^= numArray14[index];
    return Encoding.UTF8.GetString(numArray8);
  }

  internal static string ssp_appserver_13201()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[115];
      byte[] numArray2 = new byte[55]
      {
        (byte) 123,
        (byte) 155,
        (byte) 75,
        (byte) 57,
        (byte) 185,
        (byte) 224 /*0xE0*/,
        (byte) 119,
        (byte) 43,
        (byte) 77,
        (byte) 86,
        (byte) 210,
        (byte) 29,
        (byte) 244,
        (byte) 120,
        (byte) 227,
        (byte) 127 /*0x7F*/,
        (byte) 254,
        (byte) 175,
        (byte) 164,
        (byte) 22,
        (byte) 175,
        (byte) 37,
        (byte) 109,
        (byte) 85,
        (byte) 225,
        (byte) 179,
        (byte) 79,
        (byte) 60,
        (byte) 249,
        (byte) 181,
        (byte) 110,
        (byte) 200,
        (byte) 212,
        (byte) 90,
        (byte) 161,
        (byte) 231,
        (byte) 193,
        (byte) 151,
        (byte) 37,
        (byte) 20,
        (byte) 99,
        (byte) 163,
        (byte) 96 /*0x60*/,
        (byte) 155,
        (byte) 141,
        (byte) 5,
        (byte) 196,
        (byte) 206,
        (byte) 155,
        (byte) 17,
        (byte) 189,
        (byte) 27,
        (byte) 129,
        (byte) 58,
        (byte) 175
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 141,
        (byte) 104,
        (byte) 32 /*0x20*/,
        (byte) 74,
        (byte) 243,
        (byte) 250,
        (byte) 41,
        (byte) 97,
        (byte) 110,
        (byte) 163,
        (byte) 118,
        (byte) 167,
        (byte) 63 /*0x3F*/,
        (byte) 4,
        (byte) 38,
        (byte) 75,
        (byte) 213,
        (byte) 66,
        (byte) 116,
        (byte) 185,
        (byte) 40,
        (byte) 204,
        (byte) 115,
        (byte) 173,
        (byte) 56,
        (byte) 31 /*0x1F*/,
        (byte) 159,
        (byte) 64 /*0x40*/,
        (byte) 56,
        (byte) 186,
        (byte) 33,
        (byte) 187,
        (byte) 25,
        (byte) 140,
        (byte) 124,
        (byte) 139,
        (byte) 91,
        (byte) 62,
        (byte) 44,
        (byte) 221,
        (byte) 6,
        (byte) 185,
        (byte) 27,
        (byte) 92,
        (byte) 172,
        (byte) 221,
        (byte) 25,
        (byte) 144 /*0x90*/,
        (byte) 42,
        (byte) 2,
        (byte) 41,
        (byte) 67,
        (byte) 85,
        (byte) 7,
        (byte) 130
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55];
      numArray4[39] = (byte) 113;
      numArray4[1] = (byte) 87;
      numArray4[40] = (byte) 200;
      numArray4[48 /*0x30*/] = (byte) 85;
      numArray4[4] = (byte) 59;
      numArray4[22] = (byte) 220;
      numArray4[47] = (byte) 84;
      numArray4[37] = (byte) 131;
      numArray4[10] = (byte) 154;
      numArray4[15] = (byte) 138;
      numArray4[5] = (byte) 49;
      numArray4[11] = (byte) 214;
      numArray4[44] = (byte) 42;
      numArray4[12] = (byte) 251;
      numArray4[0] = (byte) 246;
      numArray4[49] = (byte) 145;
      numArray4[16 /*0x10*/] = (byte) 143;
      numArray4[50] = (byte) 242;
      numArray4[21] = (byte) 200;
      numArray4[26] = (byte) 231;
      numArray4[20] = (byte) 209;
      numArray4[14] = (byte) 103;
      numArray4[3] = (byte) 145;
      numArray4[23] = (byte) 239;
      numArray4[24] = (byte) 128 /*0x80*/;
      numArray4[25] = (byte) 160 /*0xA0*/;
      numArray4[31 /*0x1F*/] = (byte) 170;
      numArray4[17] = (byte) 179;
      numArray4[28] = (byte) 83;
      numArray4[7] = (byte) 231;
      numArray4[30] = (byte) 154;
      numArray4[38] = (byte) 39;
      numArray4[6] = (byte) 152;
      numArray4[27] = (byte) 247;
      numArray4[34] = (byte) 203;
      numArray4[35] = (byte) 63 /*0x3F*/;
      numArray4[36] = (byte) 32 /*0x20*/;
      numArray4[54] = (byte) 92;
      numArray4[2] = (byte) 5;
      numArray4[13] = (byte) 189;
      numArray4[33] = (byte) 185;
      numArray4[41] = (byte) 180;
      numArray4[42] = (byte) 130;
      numArray4[43] = (byte) 188;
      numArray4[19] = (byte) 196;
      numArray4[45] = (byte) 247;
      numArray4[46] = (byte) 160 /*0xA0*/;
      numArray4[18] = (byte) 11;
      numArray4[32 /*0x20*/] = (byte) 75;
      numArray4[8] = (byte) 10;
      numArray4[29] = (byte) 70;
      numArray4[51] = (byte) 134;
      numArray4[52] = (byte) 163;
      numArray4[9] = (byte) 73;
      numArray4[53] = (byte) 145;
      byte[] numArray5 = new byte[55];
      numArray5[51] = (byte) 94;
      numArray5[50] = (byte) 137;
      numArray5[14] = (byte) 130;
      numArray5[3] = (byte) 131;
      numArray5[4] = (byte) 43;
      numArray5[5] = (byte) 126;
      numArray5[30] = (byte) 45;
      numArray5[7] = (byte) 116;
      numArray5[39] = (byte) 214;
      numArray5[9] = (byte) 107;
      numArray5[10] = (byte) 194;
      numArray5[11] = (byte) 196;
      numArray5[12] = (byte) 179;
      numArray5[17] = (byte) 124;
      numArray5[49] = (byte) 112 /*0x70*/;
      numArray5[0] = (byte) 67;
      numArray5[16 /*0x10*/] = (byte) 207;
      numArray5[13] = (byte) 3;
      numArray5[18] = (byte) 79;
      numArray5[46] = (byte) 20;
      numArray5[20] = (byte) 0;
      numArray5[23] = (byte) 39;
      numArray5[8] = (byte) 5;
      numArray5[38] = (byte) 139;
      numArray5[25] = (byte) 190;
      numArray5[24] = (byte) 158;
      numArray5[26] = (byte) 72;
      numArray5[27] = (byte) 108;
      numArray5[22] = (byte) 128 /*0x80*/;
      numArray5[29] = (byte) 197;
      numArray5[2] = (byte) 33;
      numArray5[28] = (byte) 169;
      numArray5[1] = (byte) 35;
      numArray5[33] = (byte) 59;
      numArray5[19] = (byte) 126;
      numArray5[35] = (byte) 228;
      numArray5[54] = (byte) 193;
      numArray5[15] = (byte) 219;
      numArray5[21] = (byte) 7;
      numArray5[45] = (byte) 120;
      numArray5[40] = (byte) 146;
      numArray5[41] = (byte) 237;
      numArray5[42] = (byte) 45;
      numArray5[31 /*0x1F*/] = (byte) 45;
      numArray5[32 /*0x20*/] = (byte) 180;
      numArray5[6] = (byte) 64 /*0x40*/;
      numArray5[48 /*0x30*/] = (byte) 117;
      numArray5[52] = (byte) 79;
      numArray5[43] = (byte) 107;
      numArray5[47] = (byte) 96 /*0x60*/;
      numArray5[34] = (byte) 249;
      numArray5[36] = (byte) 247;
      numArray5[37] = (byte) 61;
      numArray5[53] = (byte) 252;
      numArray5[44] = (byte) 166;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[5]
      {
        (byte) 199,
        (byte) 105,
        (byte) 249,
        (byte) 132,
        (byte) 136
      };
      byte[] numArray7 = new byte[5]
      {
        (byte) 24,
        (byte) 0,
        (byte) 0,
        (byte) 31 /*0x1F*/,
        (byte) 0
      };
      numArray7[2] = (byte) 90;
      numArray7[1] = (byte) 101;
      numArray7[4] = (byte) 211;
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 5);
      for (int index = 0; index < 5; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[19];
      byte[] response = new byte[19];
      Array.Copy((Array) sc_13171.sspq, 291, (Array) numArray8, 0, 19);
      key.Query(true, 335, numArray8, response);
      Array.Copy((Array) sc_13171.sspr, 291, (Array) numArray8, 0, 19);
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
    byte[] numArray9 = new byte[115];
    byte[] numArray10 = new byte[55];
    numArray10[34] = (byte) 216;
    numArray10[1] = (byte) 241;
    numArray10[24] = (byte) 207;
    numArray10[47] = (byte) 222;
    numArray10[4] = (byte) 236;
    numArray10[5] = (byte) 160 /*0xA0*/;
    numArray10[36] = (byte) 44;
    numArray10[3] = (byte) 249;
    numArray10[27] = (byte) 107;
    numArray10[9] = (byte) 106;
    numArray10[10] = (byte) 238;
    numArray10[11] = (byte) 176 /*0xB0*/;
    numArray10[12] = (byte) 103;
    numArray10[13] = (byte) 205;
    numArray10[44] = (byte) 147;
    numArray10[16 /*0x10*/] = (byte) 239;
    numArray10[53] = (byte) 77;
    numArray10[17] = (byte) 148;
    numArray10[52] = (byte) 76;
    numArray10[39] = (byte) 9;
    numArray10[8] = (byte) 238;
    numArray10[54] = (byte) 33;
    numArray10[7] = (byte) 79;
    numArray10[38] = (byte) 21;
    numArray10[43] = (byte) 203;
    numArray10[25] = (byte) 94;
    numArray10[26] = (byte) 220;
    numArray10[6] = (byte) 17;
    numArray10[28] = (byte) 221;
    numArray10[22] = (byte) 145;
    numArray10[23] = (byte) 231;
    numArray10[31 /*0x1F*/] = (byte) 73;
    numArray10[18] = (byte) 22;
    numArray10[33] = (byte) 85;
    numArray10[0] = (byte) 34;
    numArray10[35] = (byte) 179;
    numArray10[14] = (byte) 36;
    numArray10[37] = (byte) 19;
    numArray10[20] = (byte) 45;
    numArray10[21] = (byte) 164;
    numArray10[40] = (byte) 161;
    numArray10[41] = (byte) 231;
    numArray10[29] = (byte) 102;
    numArray10[15] = (byte) 157;
    numArray10[51] = (byte) 211;
    numArray10[45] = (byte) 120;
    numArray10[19] = (byte) 142;
    numArray10[32 /*0x20*/] = (byte) 235;
    numArray10[48 /*0x30*/] = (byte) 170;
    numArray10[42] = (byte) 179;
    numArray10[50] = (byte) 44;
    numArray10[49] = (byte) 133;
    numArray10[2] = (byte) 129;
    numArray10[46] = (byte) 191;
    numArray10[30] = (byte) 129;
    byte[] numArray11 = new byte[55]
    {
      (byte) 45,
      (byte) 171,
      (byte) 229,
      (byte) 151,
      (byte) 235,
      (byte) 90,
      (byte) 173,
      (byte) 249,
      (byte) 117,
      (byte) 27,
      (byte) 206,
      (byte) 212,
      (byte) 70,
      (byte) 161,
      (byte) 238,
      (byte) 181,
      (byte) 34,
      (byte) 235,
      (byte) 77,
      (byte) 4,
      (byte) 45,
      (byte) 70,
      (byte) 29,
      (byte) 113,
      (byte) 118,
      (byte) 223,
      (byte) 117,
      (byte) 85,
      (byte) 180,
      (byte) 62,
      (byte) 20,
      (byte) 99,
      (byte) 129,
      (byte) 253,
      (byte) 227,
      byte.MaxValue,
      (byte) 131,
      (byte) 125,
      (byte) 243,
      (byte) 142,
      (byte) 171,
      (byte) 80 /*0x50*/,
      (byte) 36,
      (byte) 132,
      (byte) 73,
      (byte) 108,
      (byte) 157,
      (byte) 178,
      (byte) 180,
      byte.MaxValue,
      (byte) 60,
      (byte) 237,
      (byte) 142,
      (byte) 203,
      (byte) 188
    };
    key.Query(true, 335, numArray10, numArray10);
    Array.Copy((Array) numArray10, 0, (Array) numArray9, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray9[index] ^= numArray11[index];
    byte[] numArray12 = new byte[55]
    {
      (byte) 71,
      (byte) 209,
      (byte) 237,
      (byte) 197,
      (byte) 228,
      (byte) 25,
      (byte) 79,
      (byte) 59,
      (byte) 168,
      (byte) 39,
      (byte) 222,
      (byte) 154,
      (byte) 28,
      (byte) 223,
      (byte) 242,
      (byte) 246,
      (byte) 200,
      (byte) 242,
      (byte) 207,
      (byte) 198,
      (byte) 227,
      (byte) 100,
      (byte) 163,
      (byte) 242,
      (byte) 21,
      (byte) 135,
      (byte) 67,
      (byte) 233,
      (byte) 88,
      (byte) 189,
      (byte) 61,
      (byte) 88,
      (byte) 206,
      (byte) 186,
      (byte) 74,
      (byte) 24,
      (byte) 154,
      (byte) 127 /*0x7F*/,
      (byte) 22,
      (byte) 92,
      (byte) 219,
      (byte) 167,
      (byte) 70,
      (byte) 140,
      (byte) 206,
      (byte) 78,
      (byte) 28,
      (byte) 53,
      (byte) 94,
      (byte) 94,
      (byte) 118,
      (byte) 110,
      (byte) 11,
      (byte) 198,
      (byte) 181
    };
    byte[] numArray13 = new byte[55];
    numArray13[51] = (byte) 183;
    numArray13[48 /*0x30*/] = (byte) 119;
    numArray13[8] = (byte) 248;
    numArray13[3] = (byte) 182;
    numArray13[27] = (byte) 233;
    numArray13[4] = (byte) 182;
    numArray13[24] = (byte) 173;
    numArray13[7] = (byte) 105;
    numArray13[45] = (byte) 122;
    numArray13[9] = (byte) 210;
    numArray13[10] = (byte) 66;
    numArray13[11] = (byte) 80 /*0x50*/;
    numArray13[12] = (byte) 92;
    numArray13[6] = (byte) 204;
    numArray13[14] = (byte) 8;
    numArray13[15] = (byte) 241;
    numArray13[29] = (byte) 235;
    numArray13[5] = (byte) 70;
    numArray13[18] = (byte) 248;
    numArray13[19] = (byte) 43;
    numArray13[20] = (byte) 100;
    numArray13[33] = (byte) 208 /*0xD0*/;
    numArray13[22] = (byte) 3;
    numArray13[13] = (byte) 199;
    numArray13[36] = (byte) 227;
    numArray13[41] = (byte) 220;
    numArray13[26] = (byte) 207;
    numArray13[30] = (byte) 112 /*0x70*/;
    numArray13[28] = (byte) 170;
    numArray13[23] = (byte) 149;
    numArray13[44] = (byte) 44;
    numArray13[31 /*0x1F*/] = (byte) 186;
    numArray13[1] = (byte) 221;
    numArray13[0] = (byte) 217;
    numArray13[37] = (byte) 178;
    numArray13[17] = (byte) 15;
    numArray13[25] = (byte) 156;
    numArray13[34] = (byte) 208 /*0xD0*/;
    numArray13[38] = (byte) 227;
    numArray13[39] = (byte) 102;
    numArray13[40] = (byte) 162;
    numArray13[42] = (byte) 156;
    numArray13[2] = (byte) 77;
    numArray13[43] = (byte) 69;
    numArray13[21] = (byte) 45;
    numArray13[35] = (byte) 222;
    numArray13[32 /*0x20*/] = (byte) 127 /*0x7F*/;
    numArray13[16 /*0x10*/] = (byte) 195;
    numArray13[47] = (byte) 212;
    numArray13[49] = (byte) 90;
    numArray13[50] = (byte) 30;
    numArray13[46] = (byte) 102;
    numArray13[52] = (byte) 228;
    numArray13[53] = (byte) 151;
    numArray13[54] = (byte) 234;
    key.Query(true, 335, numArray12, numArray12);
    Array.Copy((Array) numArray12, 0, (Array) numArray9, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray9[index + 55] ^= numArray13[index];
    byte[] numArray14 = new byte[5]
    {
      (byte) 215,
      (byte) 233,
      (byte) 240 /*0xF0*/,
      (byte) 200,
      (byte) 219
    };
    byte[] numArray15 = new byte[5]
    {
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 192 /*0xC0*/,
      (byte) 0
    };
    numArray15[0] = (byte) 207;
    numArray15[2] = (byte) 209;
    numArray15[1] = (byte) 249;
    numArray15[4] = (byte) 218;
    key.Query(true, 335, numArray14, numArray14);
    Array.Copy((Array) numArray14, 0, (Array) numArray9, 110, 5);
    for (int index = 0; index < 5; ++index)
      numArray9[index + 110] ^= numArray15[index];
    return Encoding.UTF8.GetString(numArray9);
  }

  internal static int ssp_appserver_13202(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[38] = (byte) 5;
    sourceArray1[1] = (byte) 236;
    sourceArray1[2] = (byte) 116;
    sourceArray1[3] = (byte) 118;
    sourceArray1[31 /*0x1F*/] = (byte) 150;
    sourceArray1[42] = (byte) 63 /*0x3F*/;
    sourceArray1[6] = (byte) 218;
    sourceArray1[11] = (byte) 248;
    sourceArray1[8] = (byte) 177;
    sourceArray1[5] = (byte) 117;
    sourceArray1[10] = (byte) 5;
    sourceArray1[15] = (byte) 43;
    sourceArray1[19] = (byte) 198;
    sourceArray1[7] = (byte) 69;
    sourceArray1[47] = (byte) 221;
    sourceArray1[20] = (byte) 229;
    sourceArray1[37] = (byte) 3;
    sourceArray1[18] = (byte) 212;
    sourceArray1[23] = (byte) 154;
    sourceArray1[28] = (byte) 188;
    sourceArray1[4] = (byte) 192 /*0xC0*/;
    sourceArray1[9] = (byte) 108;
    sourceArray1[22] = (byte) 190;
    sourceArray1[24] = (byte) 222;
    sourceArray1[26] = (byte) 136;
    sourceArray1[25] = (byte) 186;
    sourceArray1[13] = (byte) 60;
    sourceArray1[29] = (byte) 92;
    sourceArray1[17] = (byte) 161;
    sourceArray1[40] = (byte) 253;
    sourceArray1[30] = (byte) 115;
    sourceArray1[16 /*0x10*/] = (byte) 138;
    sourceArray1[44] = (byte) 213;
    sourceArray1[33] = (byte) 179;
    sourceArray1[41] = (byte) 163;
    sourceArray1[35] = (byte) 171;
    sourceArray1[36] = (byte) 103;
    sourceArray1[12] = (byte) 25;
    sourceArray1[34] = (byte) 114;
    sourceArray1[39] = (byte) 160 /*0xA0*/;
    sourceArray1[32 /*0x20*/] = (byte) 69;
    sourceArray1[21] = (byte) 201;
    sourceArray1[27] = (byte) 16 /*0x10*/;
    sourceArray1[43] = (byte) 91;
    sourceArray1[0] = (byte) 32 /*0x20*/;
    sourceArray1[45] = (byte) 239;
    sourceArray1[46] = (byte) 137;
    sourceArray1[14] = (byte) 180;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 154,
      (byte) 163,
      (byte) 219,
      (byte) 187,
      (byte) 242,
      (byte) 82,
      (byte) 235,
      (byte) 228,
      (byte) 66,
      (byte) 152,
      (byte) 173,
      (byte) 127 /*0x7F*/,
      (byte) 214,
      (byte) 89,
      (byte) 12,
      (byte) 45,
      (byte) 27,
      (byte) 186,
      (byte) 130,
      (byte) 178,
      (byte) 120,
      (byte) 18,
      (byte) 250,
      (byte) 65,
      (byte) 181,
      (byte) 132,
      (byte) 69,
      (byte) 95,
      (byte) 243,
      (byte) 37,
      (byte) 51,
      (byte) 109,
      (byte) 91,
      (byte) 155,
      (byte) 74,
      (byte) 28,
      (byte) 175,
      (byte) 159,
      (byte) 125,
      (byte) 174,
      (byte) 28,
      byte.MaxValue,
      (byte) 32 /*0x20*/,
      (byte) 211,
      (byte) 60,
      (byte) 229,
      (byte) 201,
      (byte) 205
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_13203()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[31 /*0x1F*/];
      byte[] numArray2 = new byte[31 /*0x1F*/];
      numArray2[26] = (byte) 26;
      numArray2[16 /*0x10*/] = (byte) 60;
      numArray2[2] = (byte) 19;
      numArray2[0] = (byte) 60;
      numArray2[7] = (byte) 229;
      numArray2[1] = (byte) 34;
      numArray2[20] = (byte) 243;
      numArray2[12] = (byte) 21;
      numArray2[15] = (byte) 67;
      numArray2[14] = (byte) 213;
      numArray2[10] = (byte) 205;
      numArray2[11] = (byte) 204;
      numArray2[5] = (byte) 75;
      numArray2[25] = (byte) 55;
      numArray2[13] = (byte) 169;
      numArray2[27] = (byte) 157;
      numArray2[4] = (byte) 213;
      numArray2[17] = (byte) 101;
      numArray2[3] = (byte) 122;
      numArray2[6] = (byte) 239;
      numArray2[18] = (byte) 203;
      numArray2[21] = (byte) 100;
      numArray2[22] = (byte) 72;
      numArray2[23] = (byte) 175;
      numArray2[24] = (byte) 140;
      numArray2[9] = (byte) 83;
      numArray2[19] = (byte) 17;
      numArray2[30] = (byte) 38;
      numArray2[28] = (byte) 129;
      numArray2[29] = (byte) 149;
      numArray2[8] = (byte) 44;
      byte[] numArray3 = new byte[31 /*0x1F*/];
      numArray3[22] = (byte) 114;
      numArray3[27] = (byte) 246;
      numArray3[2] = (byte) 145;
      numArray3[3] = (byte) 169;
      numArray3[13] = (byte) 39;
      numArray3[14] = (byte) 194;
      numArray3[8] = (byte) 69;
      numArray3[1] = (byte) 28;
      numArray3[18] = (byte) 42;
      numArray3[9] = (byte) 104;
      numArray3[21] = (byte) 227;
      numArray3[11] = (byte) 51;
      numArray3[17] = (byte) 193;
      numArray3[28] = (byte) 235;
      numArray3[30] = (byte) 243;
      numArray3[15] = (byte) 236;
      numArray3[16 /*0x10*/] = (byte) 149;
      numArray3[5] = (byte) 195;
      numArray3[0] = (byte) 44;
      numArray3[19] = (byte) 234;
      numArray3[25] = (byte) 130;
      numArray3[12] = (byte) 5;
      numArray3[10] = (byte) 90;
      numArray3[23] = (byte) 82;
      numArray3[24] = (byte) 22;
      numArray3[26] = (byte) 92;
      numArray3[4] = (byte) 148;
      numArray3[29] = (byte) 223;
      numArray3[20] = (byte) 251;
      numArray3[6] = (byte) 248;
      numArray3[7] = (byte) 143;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 31 /*0x1F*/);
      for (int index = 0; index < 31 /*0x1F*/; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[31 /*0x1F*/];
    byte[] numArray5 = new byte[31 /*0x1F*/]
    {
      (byte) 51,
      (byte) 138,
      (byte) 156,
      (byte) 32 /*0x20*/,
      (byte) 228,
      (byte) 193,
      (byte) 203,
      (byte) 98,
      (byte) 196,
      (byte) 205,
      (byte) 158,
      (byte) 145,
      (byte) 55,
      (byte) 22,
      (byte) 149,
      (byte) 92,
      (byte) 1,
      (byte) 18,
      (byte) 221,
      (byte) 216,
      (byte) 221,
      (byte) 82,
      (byte) 250,
      (byte) 189,
      (byte) 172,
      (byte) 28,
      (byte) 201,
      (byte) 28,
      (byte) 147,
      (byte) 120,
      (byte) 5
    };
    byte[] numArray6 = new byte[31 /*0x1F*/]
    {
      (byte) 102,
      (byte) 117,
      (byte) 131,
      (byte) 205,
      (byte) 193,
      (byte) 33,
      (byte) 19,
      (byte) 204,
      (byte) 236,
      (byte) 231,
      (byte) 100,
      (byte) 185,
      (byte) 19,
      (byte) 80 /*0x50*/,
      (byte) 17,
      (byte) 91,
      (byte) 19,
      (byte) 213,
      (byte) 123,
      (byte) 48 /*0x30*/,
      (byte) 108,
      (byte) 90,
      (byte) 218,
      (byte) 212,
      (byte) 143,
      (byte) 32 /*0x20*/,
      (byte) 174,
      (byte) 158,
      (byte) 2,
      (byte) 170,
      (byte) 48 /*0x30*/
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 31 /*0x1F*/);
    for (int index = 0; index < 31 /*0x1F*/; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13204()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[20];
      byte[] numArray2 = new byte[20]
      {
        (byte) 183,
        (byte) 225,
        (byte) 65,
        (byte) 246,
        (byte) 254,
        (byte) 141,
        (byte) 18,
        (byte) 77,
        (byte) 207,
        (byte) 210,
        (byte) 238,
        (byte) 120,
        (byte) 213,
        (byte) 68,
        (byte) 215,
        (byte) 50,
        (byte) 18,
        (byte) 210,
        (byte) 52,
        (byte) 201
      };
      byte[] numArray3 = new byte[20]
      {
        (byte) 53,
        (byte) 120,
        (byte) 254,
        (byte) 13,
        (byte) 84,
        (byte) 65,
        (byte) 106,
        (byte) 202,
        (byte) 227,
        (byte) 114,
        (byte) 129,
        (byte) 216,
        (byte) 111,
        (byte) 89,
        (byte) 135,
        (byte) 159,
        (byte) 200,
        (byte) 184,
        (byte) 219,
        (byte) 215
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 20);
      for (int index = 0; index < 20; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[47];
      byte[] response = new byte[47];
      Array.Copy((Array) sc_13171.sspq, 310, (Array) numArray4, 0, 47);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_13171.sspr, 310, (Array) numArray4, 0, 47);
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
    byte[] numArray5 = new byte[20];
    byte[] numArray6 = new byte[20]
    {
      (byte) 247,
      (byte) 243,
      (byte) 232,
      byte.MaxValue,
      (byte) 218,
      (byte) 101,
      (byte) 243,
      (byte) 182,
      (byte) 36,
      (byte) 3,
      (byte) 214,
      (byte) 18,
      (byte) 6,
      (byte) 214,
      (byte) 161,
      (byte) 197,
      (byte) 27,
      (byte) 165,
      (byte) 182,
      (byte) 119
    };
    byte[] numArray7 = new byte[20];
    numArray7[7] = (byte) 4;
    numArray7[1] = (byte) 181;
    numArray7[19] = (byte) 83;
    numArray7[12] = (byte) 15;
    numArray7[4] = (byte) 178;
    numArray7[3] = (byte) 242;
    numArray7[6] = (byte) 25;
    numArray7[17] = (byte) 173;
    numArray7[9] = (byte) 7;
    numArray7[15] = (byte) 111;
    numArray7[10] = (byte) 176 /*0xB0*/;
    numArray7[11] = (byte) 120;
    numArray7[8] = (byte) 84;
    numArray7[5] = (byte) 79;
    numArray7[14] = (byte) 168;
    numArray7[18] = (byte) 112 /*0x70*/;
    numArray7[16 /*0x10*/] = (byte) 33;
    numArray7[0] = (byte) 44;
    numArray7[13] = (byte) 53;
    numArray7[2] = (byte) 161;
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 20);
    for (int index = 0; index < 20; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }
}
