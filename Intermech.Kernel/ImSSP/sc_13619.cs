// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_13619
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_13619
{
  private static byte[] sspq = new byte[450]
  {
    (byte) 173,
    (byte) 119,
    (byte) 141,
    (byte) 173,
    (byte) 46,
    (byte) 77,
    (byte) 84,
    (byte) 68,
    (byte) 95,
    (byte) 246,
    (byte) 222,
    (byte) 207,
    (byte) 195,
    (byte) 61,
    (byte) 140,
    (byte) 184,
    (byte) 239,
    (byte) 164,
    (byte) 216,
    (byte) 148,
    (byte) 24,
    (byte) 172,
    (byte) 200,
    (byte) 17,
    (byte) 71,
    (byte) 178,
    (byte) 241,
    (byte) 108,
    (byte) 91,
    (byte) 239,
    (byte) 236,
    (byte) 23,
    (byte) 208 /*0xD0*/,
    (byte) 101,
    (byte) 112 /*0x70*/,
    (byte) 10,
    (byte) 199,
    byte.MaxValue,
    (byte) 94,
    (byte) 13,
    (byte) 232,
    (byte) 15,
    (byte) 205,
    (byte) 174,
    (byte) 49,
    (byte) 188,
    (byte) 157,
    (byte) 103,
    (byte) 60,
    (byte) 44,
    (byte) 6,
    (byte) 25,
    (byte) 96 /*0x60*/,
    (byte) 143,
    (byte) 90,
    (byte) 20,
    (byte) 233,
    (byte) 205,
    (byte) 233,
    (byte) 109,
    (byte) 219,
    (byte) 60,
    (byte) 113,
    (byte) 152,
    byte.MaxValue,
    (byte) 124,
    (byte) 140,
    (byte) 129,
    (byte) 25,
    (byte) 141,
    (byte) 182,
    (byte) 21,
    (byte) 73,
    (byte) 228,
    (byte) 70,
    (byte) 119,
    (byte) 248,
    (byte) 142,
    (byte) 125,
    (byte) 149,
    (byte) 143,
    (byte) 105,
    (byte) 89,
    (byte) 134,
    (byte) 231,
    (byte) 158,
    (byte) 161,
    (byte) 109,
    (byte) 222,
    (byte) 78,
    (byte) 81,
    (byte) 36,
    (byte) 152,
    (byte) 169,
    (byte) 50,
    (byte) 133,
    (byte) 11,
    (byte) 193,
    (byte) 165,
    (byte) 161,
    (byte) 155,
    (byte) 42,
    (byte) 130,
    (byte) 94,
    (byte) 228,
    (byte) 81,
    (byte) 98,
    (byte) 110,
    (byte) 233,
    (byte) 0,
    (byte) 12,
    (byte) 129,
    (byte) 13,
    (byte) 185,
    (byte) 92,
    (byte) 254,
    (byte) 132,
    (byte) 121,
    (byte) 116,
    (byte) 40,
    (byte) 35,
    (byte) 190,
    (byte) 192 /*0xC0*/,
    (byte) 123,
    (byte) 80 /*0x50*/,
    (byte) 4,
    (byte) 143,
    (byte) 103,
    (byte) 178,
    (byte) 110,
    (byte) 221,
    (byte) 191,
    (byte) 228,
    (byte) 86,
    (byte) 196,
    (byte) 247,
    (byte) 19,
    (byte) 1,
    (byte) 186,
    (byte) 156,
    (byte) 242,
    (byte) 173,
    (byte) 152,
    (byte) 149,
    (byte) 253,
    (byte) 134,
    (byte) 100,
    (byte) 26,
    (byte) 113,
    (byte) 93,
    (byte) 226,
    (byte) 177,
    (byte) 10,
    (byte) 100,
    (byte) 73,
    (byte) 3,
    (byte) 232,
    (byte) 129,
    (byte) 1,
    (byte) 210,
    (byte) 209,
    (byte) 44,
    (byte) 73,
    (byte) 73,
    (byte) 115,
    (byte) 197,
    (byte) 113,
    (byte) 129,
    (byte) 51,
    (byte) 216,
    (byte) 145,
    (byte) 234,
    (byte) 34,
    (byte) 177,
    (byte) 32 /*0x20*/,
    (byte) 200,
    (byte) 163,
    (byte) 233,
    (byte) 94,
    (byte) 172,
    (byte) 40,
    (byte) 59,
    (byte) 116,
    (byte) 224 /*0xE0*/,
    (byte) 219,
    (byte) 201,
    (byte) 186,
    (byte) 244,
    (byte) 231,
    (byte) 241,
    (byte) 12,
    (byte) 248,
    (byte) 73,
    (byte) 6,
    (byte) 18,
    (byte) 246,
    (byte) 8,
    (byte) 193,
    (byte) 194,
    (byte) 254,
    (byte) 165,
    (byte) 157,
    (byte) 50,
    (byte) 198,
    (byte) 249,
    (byte) 119,
    (byte) 74,
    (byte) 2,
    (byte) 223,
    (byte) 135,
    (byte) 248,
    (byte) 104,
    (byte) 86,
    (byte) 163,
    (byte) 244,
    (byte) 55,
    (byte) 22,
    (byte) 201,
    (byte) 132,
    (byte) 68,
    (byte) 6,
    (byte) 16 /*0x10*/,
    (byte) 134,
    (byte) 29,
    (byte) 152,
    (byte) 90,
    (byte) 80 /*0x50*/,
    (byte) 46,
    (byte) 83,
    (byte) 36,
    (byte) 84,
    (byte) 176 /*0xB0*/,
    (byte) 0,
    (byte) 237,
    (byte) 29,
    (byte) 32 /*0x20*/,
    (byte) 15,
    (byte) 136,
    (byte) 108,
    (byte) 4,
    (byte) 6,
    (byte) 142,
    (byte) 155,
    (byte) 43,
    (byte) 104,
    (byte) 113,
    (byte) 242,
    (byte) 49,
    (byte) 62,
    (byte) 16 /*0x10*/,
    (byte) 176 /*0xB0*/,
    (byte) 151,
    (byte) 138,
    (byte) 236,
    (byte) 93,
    (byte) 133,
    (byte) 67,
    (byte) 56,
    (byte) 143,
    (byte) 242,
    (byte) 9,
    (byte) 50,
    (byte) 140,
    (byte) 209,
    (byte) 118,
    (byte) 131,
    (byte) 144 /*0x90*/,
    (byte) 220,
    (byte) 170,
    (byte) 184,
    (byte) 104,
    (byte) 88,
    (byte) 114,
    (byte) 191,
    (byte) 12,
    (byte) 82,
    (byte) 123,
    (byte) 212,
    (byte) 76,
    (byte) 0,
    (byte) 128 /*0x80*/,
    (byte) 72,
    (byte) 94,
    (byte) 55,
    (byte) 226,
    (byte) 75,
    (byte) 221,
    (byte) 76,
    (byte) 74,
    (byte) 214,
    (byte) 165,
    (byte) 80 /*0x50*/,
    (byte) 186,
    (byte) 160 /*0xA0*/,
    (byte) 66,
    (byte) 224 /*0xE0*/,
    (byte) 246,
    (byte) 191,
    (byte) 2,
    (byte) 40,
    (byte) 26,
    (byte) 55,
    (byte) 124,
    (byte) 57,
    (byte) 132,
    (byte) 38,
    (byte) 174,
    (byte) 248,
    (byte) 235,
    (byte) 158,
    (byte) 99,
    (byte) 42,
    (byte) 88,
    (byte) 172,
    (byte) 108,
    (byte) 177,
    (byte) 61,
    (byte) 37,
    (byte) 242,
    (byte) 0,
    (byte) 86,
    (byte) 227,
    (byte) 159,
    (byte) 194,
    (byte) 142,
    (byte) 250,
    (byte) 117,
    (byte) 34,
    (byte) 78,
    (byte) 129,
    (byte) 52,
    (byte) 52,
    (byte) 89,
    (byte) 195,
    (byte) 110,
    (byte) 238,
    (byte) 188,
    (byte) 36,
    (byte) 125,
    (byte) 23,
    (byte) 109,
    (byte) 58,
    (byte) 237,
    (byte) 76,
    (byte) 248,
    (byte) 237,
    (byte) 72,
    (byte) 83,
    (byte) 22,
    (byte) 57,
    (byte) 213,
    (byte) 252,
    (byte) 21,
    (byte) 240 /*0xF0*/,
    (byte) 248,
    (byte) 196,
    (byte) 195,
    (byte) 54,
    (byte) 172,
    (byte) 10,
    (byte) 190,
    (byte) 22,
    (byte) 172,
    (byte) 251,
    (byte) 83,
    (byte) 40,
    (byte) 147,
    (byte) 106,
    (byte) 146,
    (byte) 166,
    (byte) 211,
    (byte) 144 /*0x90*/,
    (byte) 158,
    (byte) 111,
    (byte) 141,
    (byte) 105,
    (byte) 149,
    (byte) 116,
    (byte) 144 /*0x90*/,
    (byte) 64 /*0x40*/,
    (byte) 42,
    (byte) 158,
    (byte) 195,
    (byte) 118,
    (byte) 195,
    (byte) 9,
    (byte) 185,
    (byte) 175,
    (byte) 184,
    (byte) 151,
    (byte) 254,
    (byte) 28,
    (byte) 237,
    (byte) 120,
    (byte) 216,
    (byte) 149,
    (byte) 200,
    (byte) 28,
    (byte) 162,
    (byte) 153,
    (byte) 231,
    (byte) 67,
    (byte) 245,
    (byte) 156,
    (byte) 125,
    (byte) 127 /*0x7F*/,
    (byte) 126,
    (byte) 226,
    (byte) 212,
    (byte) 193,
    (byte) 42,
    (byte) 28,
    (byte) 148,
    (byte) 138,
    (byte) 120,
    (byte) 212,
    (byte) 166,
    (byte) 7,
    (byte) 232,
    (byte) 203,
    (byte) 196,
    (byte) 100,
    (byte) 100,
    (byte) 26,
    (byte) 160 /*0xA0*/,
    (byte) 179,
    (byte) 99,
    (byte) 237,
    (byte) 128 /*0x80*/,
    (byte) 97,
    (byte) 90,
    (byte) 156,
    (byte) 70,
    (byte) 28,
    (byte) 206,
    (byte) 95,
    (byte) 245,
    (byte) 29,
    (byte) 32 /*0x20*/,
    (byte) 64 /*0x40*/,
    (byte) 206,
    (byte) 5,
    (byte) 110,
    (byte) 51,
    (byte) 114,
    (byte) 97,
    (byte) 234,
    (byte) 243,
    (byte) 169,
    (byte) 247
  };
  private static byte[] sspr = new byte[450]
  {
    (byte) 197,
    (byte) 199,
    (byte) 66,
    (byte) 156,
    (byte) 90,
    (byte) 72,
    (byte) 29,
    (byte) 112 /*0x70*/,
    (byte) 199,
    (byte) 79,
    (byte) 6,
    (byte) 136,
    (byte) 192 /*0xC0*/,
    (byte) 70,
    (byte) 63 /*0x3F*/,
    (byte) 209,
    (byte) 164,
    (byte) 46,
    (byte) 0,
    (byte) 8,
    (byte) 16 /*0x10*/,
    (byte) 118,
    (byte) 208 /*0xD0*/,
    (byte) 32 /*0x20*/,
    (byte) 223,
    (byte) 84,
    (byte) 149,
    (byte) 141,
    (byte) 30,
    (byte) 175,
    (byte) 135,
    (byte) 219,
    (byte) 221,
    (byte) 242,
    (byte) 161,
    (byte) 142,
    (byte) 64 /*0x40*/,
    (byte) 104,
    (byte) 94,
    (byte) 29,
    (byte) 26,
    (byte) 109,
    (byte) 95,
    (byte) 192 /*0xC0*/,
    (byte) 179,
    (byte) 94,
    (byte) 141,
    (byte) 145,
    (byte) 166,
    (byte) 121,
    (byte) 92,
    (byte) 242,
    (byte) 205,
    (byte) 75,
    (byte) 17,
    (byte) 11,
    (byte) 105,
    (byte) 161,
    (byte) 198,
    (byte) 52,
    (byte) 179,
    (byte) 63 /*0x3F*/,
    (byte) 198,
    (byte) 57,
    (byte) 72,
    (byte) 94,
    (byte) 88,
    (byte) 216,
    (byte) 162,
    (byte) 158,
    (byte) 43,
    (byte) 183,
    (byte) 46,
    (byte) 54,
    (byte) 235,
    (byte) 197,
    (byte) 252,
    (byte) 213,
    (byte) 186,
    (byte) 225,
    (byte) 61,
    (byte) 128 /*0x80*/,
    (byte) 100,
    (byte) 27,
    (byte) 155,
    (byte) 82,
    (byte) 118,
    (byte) 51,
    (byte) 236,
    (byte) 98,
    (byte) 63 /*0x3F*/,
    (byte) 203,
    (byte) 87,
    (byte) 10,
    (byte) 81,
    (byte) 111,
    (byte) 248,
    (byte) 46,
    (byte) 165,
    (byte) 141,
    (byte) 112 /*0x70*/,
    (byte) 34,
    (byte) 189,
    (byte) 171,
    (byte) 45,
    (byte) 113,
    (byte) 52,
    (byte) 167,
    (byte) 125,
    (byte) 210,
    (byte) 128 /*0x80*/,
    (byte) 47,
    (byte) 104,
    (byte) 114,
    (byte) 161,
    (byte) 82,
    (byte) 49,
    (byte) 167,
    (byte) 216,
    (byte) 161,
    (byte) 132,
    (byte) 164,
    (byte) 197,
    (byte) 43,
    (byte) 194,
    (byte) 42,
    (byte) 159,
    (byte) 235,
    (byte) 74,
    (byte) 246,
    (byte) 211,
    (byte) 242,
    (byte) 3,
    (byte) 8,
    (byte) 247,
    (byte) 200,
    (byte) 247,
    (byte) 116,
    (byte) 207,
    (byte) 237,
    (byte) 194,
    (byte) 36,
    (byte) 49,
    (byte) 59,
    (byte) 202,
    (byte) 107,
    (byte) 102,
    (byte) 53,
    (byte) 106,
    (byte) 119,
    (byte) 71,
    (byte) 102,
    (byte) 28,
    (byte) 94,
    (byte) 132,
    (byte) 63 /*0x3F*/,
    (byte) 229,
    (byte) 106,
    (byte) 78,
    (byte) 36,
    (byte) 136,
    (byte) 29,
    (byte) 109,
    (byte) 119,
    (byte) 15,
    (byte) 102,
    (byte) 65,
    (byte) 86,
    (byte) 43,
    (byte) 164,
    (byte) 222,
    (byte) 58,
    (byte) 174,
    (byte) 247,
    (byte) 41,
    (byte) 78,
    byte.MaxValue,
    (byte) 195,
    (byte) 169,
    (byte) 128 /*0x80*/,
    (byte) 208 /*0xD0*/,
    (byte) 121,
    (byte) 160 /*0xA0*/,
    (byte) 219,
    (byte) 156,
    (byte) 16 /*0x10*/,
    (byte) 234,
    (byte) 172,
    (byte) 185,
    (byte) 237,
    (byte) 238,
    (byte) 179,
    (byte) 161,
    (byte) 116,
    (byte) 59,
    (byte) 75,
    (byte) 105,
    (byte) 227,
    (byte) 81,
    (byte) 167,
    (byte) 111,
    (byte) 56,
    (byte) 197,
    (byte) 149,
    (byte) 239,
    (byte) 130,
    (byte) 28,
    (byte) 76,
    (byte) 165,
    (byte) 104,
    (byte) 254,
    (byte) 120,
    (byte) 247,
    (byte) 154,
    (byte) 58,
    (byte) 53,
    (byte) 88,
    (byte) 179,
    (byte) 159,
    (byte) 127 /*0x7F*/,
    (byte) 85,
    (byte) 87,
    (byte) 126,
    (byte) 141,
    (byte) 160 /*0xA0*/,
    (byte) 240 /*0xF0*/,
    (byte) 244,
    (byte) 87,
    (byte) 196,
    (byte) 10,
    (byte) 196,
    (byte) 144 /*0x90*/,
    (byte) 47,
    (byte) 59,
    (byte) 3,
    (byte) 208 /*0xD0*/,
    (byte) 242,
    (byte) 253,
    (byte) 218,
    (byte) 113,
    (byte) 95,
    (byte) 11,
    (byte) 68,
    (byte) 69,
    byte.MaxValue,
    (byte) 191,
    (byte) 172,
    (byte) 140,
    (byte) 59,
    (byte) 9,
    (byte) 42,
    (byte) 7,
    (byte) 214,
    (byte) 147,
    (byte) 16 /*0x10*/,
    (byte) 224 /*0xE0*/,
    (byte) 17,
    (byte) 13,
    (byte) 157,
    (byte) 51,
    (byte) 43,
    (byte) 67,
    (byte) 26,
    (byte) 197,
    (byte) 59,
    (byte) 192 /*0xC0*/,
    (byte) 199,
    (byte) 140,
    (byte) 29,
    (byte) 165,
    (byte) 227,
    (byte) 173,
    (byte) 181,
    (byte) 201,
    (byte) 181,
    (byte) 56,
    (byte) 133,
    (byte) 159,
    (byte) 109,
    (byte) 41,
    (byte) 13,
    (byte) 215,
    (byte) 163,
    (byte) 214,
    (byte) 77,
    (byte) 0,
    (byte) 53,
    (byte) 42,
    (byte) 95,
    (byte) 84,
    (byte) 176 /*0xB0*/,
    (byte) 108,
    (byte) 201,
    (byte) 49,
    (byte) 140,
    (byte) 5,
    (byte) 149,
    (byte) 206,
    (byte) 66,
    (byte) 28,
    (byte) 85,
    (byte) 145,
    (byte) 197,
    (byte) 92,
    (byte) 233,
    (byte) 245,
    (byte) 130,
    (byte) 128 /*0x80*/,
    (byte) 229,
    (byte) 173,
    (byte) 105,
    (byte) 52,
    (byte) 189,
    (byte) 33,
    (byte) 249,
    (byte) 175,
    (byte) 181,
    (byte) 142,
    (byte) 67,
    (byte) 112 /*0x70*/,
    (byte) 173,
    (byte) 131,
    byte.MaxValue,
    (byte) 209,
    (byte) 128 /*0x80*/,
    (byte) 102,
    (byte) 217,
    (byte) 222,
    (byte) 106,
    (byte) 198,
    (byte) 29,
    (byte) 127 /*0x7F*/,
    (byte) 161,
    (byte) 176 /*0xB0*/,
    (byte) 23,
    (byte) 249,
    (byte) 224 /*0xE0*/,
    (byte) 62,
    (byte) 142,
    (byte) 43,
    (byte) 202,
    (byte) 196,
    (byte) 26,
    (byte) 58,
    (byte) 81,
    (byte) 88,
    (byte) 94,
    (byte) 17,
    (byte) 213,
    (byte) 102,
    (byte) 172,
    (byte) 219,
    (byte) 144 /*0x90*/,
    (byte) 85,
    (byte) 101,
    (byte) 69,
    (byte) 131,
    (byte) 22,
    (byte) 233,
    (byte) 114,
    (byte) 141,
    (byte) 144 /*0x90*/,
    (byte) 24,
    (byte) 164,
    (byte) 243,
    (byte) 129,
    (byte) 24,
    (byte) 191,
    (byte) 51,
    (byte) 198,
    (byte) 209,
    (byte) 193,
    (byte) 3,
    (byte) 192 /*0xC0*/,
    (byte) 54,
    (byte) 222,
    (byte) 236,
    (byte) 44,
    (byte) 29,
    (byte) 140,
    (byte) 131,
    (byte) 152,
    (byte) 217,
    (byte) 21,
    (byte) 73,
    (byte) 180,
    (byte) 128 /*0x80*/,
    (byte) 156,
    (byte) 201,
    (byte) 75,
    (byte) 82,
    (byte) 89,
    (byte) 157,
    (byte) 130,
    (byte) 115,
    (byte) 93,
    (byte) 32 /*0x20*/,
    (byte) 104,
    (byte) 197,
    (byte) 198,
    (byte) 97,
    (byte) 46,
    (byte) 240 /*0xF0*/,
    (byte) 177,
    (byte) 120,
    (byte) 98,
    (byte) 0,
    (byte) 153,
    (byte) 250,
    (byte) 3,
    (byte) 219,
    (byte) 133,
    (byte) 99,
    (byte) 171,
    (byte) 232,
    (byte) 215,
    (byte) 3,
    (byte) 131,
    (byte) 110,
    (byte) 41,
    (byte) 70,
    (byte) 146,
    (byte) 240 /*0xF0*/,
    (byte) 199,
    (byte) 235,
    (byte) 242,
    (byte) 73,
    (byte) 42,
    (byte) 75,
    (byte) 70,
    (byte) 173,
    (byte) 143,
    (byte) 186,
    (byte) 167,
    (byte) 209,
    (byte) 161,
    (byte) 106,
    (byte) 201,
    (byte) 164,
    (byte) 156,
    (byte) 10,
    (byte) 6,
    (byte) 201,
    (byte) 38,
    (byte) 35,
    (byte) 149,
    (byte) 207,
    (byte) 186,
    (byte) 80 /*0x50*/,
    (byte) 115
  };

  internal static string ssp_appserver_13620()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[57];
      byte[] numArray2 = new byte[55];
      numArray2[6] = (byte) 104;
      numArray2[20] = (byte) 9;
      numArray2[2] = (byte) 189;
      numArray2[29] = (byte) 118;
      numArray2[9] = (byte) 117;
      numArray2[21] = (byte) 95;
      numArray2[54] = (byte) 185;
      numArray2[32 /*0x20*/] = (byte) 192 /*0xC0*/;
      numArray2[22] = (byte) 85;
      numArray2[53] = (byte) 128 /*0x80*/;
      numArray2[52] = (byte) 216;
      numArray2[50] = (byte) 97;
      numArray2[12] = (byte) 67;
      numArray2[13] = (byte) 201;
      numArray2[37] = (byte) 98;
      numArray2[15] = (byte) 208 /*0xD0*/;
      numArray2[26] = (byte) 62;
      numArray2[46] = (byte) 28;
      numArray2[18] = (byte) 26;
      numArray2[19] = (byte) 139;
      numArray2[5] = (byte) 206;
      numArray2[35] = (byte) 146;
      numArray2[11] = (byte) 137;
      numArray2[23] = (byte) 207;
      numArray2[30] = (byte) 238;
      numArray2[51] = (byte) 116;
      numArray2[7] = (byte) 78;
      numArray2[17] = (byte) 44;
      numArray2[28] = (byte) 71;
      numArray2[31 /*0x1F*/] = (byte) 134;
      numArray2[4] = (byte) 42;
      numArray2[3] = (byte) 59;
      numArray2[0] = (byte) 179;
      numArray2[33] = (byte) 21;
      numArray2[24] = (byte) 127 /*0x7F*/;
      numArray2[34] = (byte) 1;
      numArray2[36] = (byte) 192 /*0xC0*/;
      numArray2[1] = (byte) 22;
      numArray2[38] = (byte) 89;
      numArray2[39] = (byte) 145;
      numArray2[16 /*0x10*/] = (byte) 231;
      numArray2[41] = (byte) 26;
      numArray2[42] = (byte) 131;
      numArray2[43] = (byte) 240 /*0xF0*/;
      numArray2[44] = (byte) 110;
      numArray2[10] = (byte) 223;
      numArray2[8] = (byte) 19;
      numArray2[25] = (byte) 66;
      numArray2[45] = (byte) 62;
      numArray2[49] = (byte) 128 /*0x80*/;
      numArray2[47] = (byte) 141;
      numArray2[40] = (byte) 73;
      numArray2[27] = (byte) 199;
      numArray2[48 /*0x30*/] = (byte) 44;
      numArray2[14] = (byte) 241;
      byte[] numArray3 = new byte[55];
      numArray3[5] = (byte) 98;
      numArray3[12] = (byte) 154;
      numArray3[17] = (byte) 136;
      numArray3[9] = (byte) 111;
      numArray3[15] = (byte) 83;
      numArray3[7] = (byte) 7;
      numArray3[18] = (byte) 180;
      numArray3[20] = (byte) 68;
      numArray3[8] = (byte) 217;
      numArray3[43] = (byte) 120;
      numArray3[10] = (byte) 146;
      numArray3[11] = (byte) 246;
      numArray3[28] = (byte) 110;
      numArray3[54] = (byte) 61;
      numArray3[48 /*0x30*/] = (byte) 73;
      numArray3[37] = (byte) 83;
      numArray3[27] = (byte) 245;
      numArray3[41] = (byte) 251;
      numArray3[52] = (byte) 24;
      numArray3[19] = (byte) 4;
      numArray3[13] = (byte) 155;
      numArray3[21] = (byte) 170;
      numArray3[35] = (byte) 241;
      numArray3[23] = (byte) 81;
      numArray3[24] = (byte) 150;
      numArray3[25] = (byte) 14;
      numArray3[26] = (byte) 167;
      numArray3[22] = (byte) 1;
      numArray3[0] = (byte) 123;
      numArray3[29] = (byte) 176 /*0xB0*/;
      numArray3[40] = (byte) 100;
      numArray3[47] = (byte) 193;
      numArray3[32 /*0x20*/] = (byte) 85;
      numArray3[33] = (byte) 69;
      numArray3[34] = (byte) 184;
      numArray3[2] = (byte) 206;
      numArray3[36] = (byte) 43;
      numArray3[45] = (byte) 155;
      numArray3[4] = (byte) 162;
      numArray3[39] = (byte) 173;
      numArray3[53] = (byte) 89;
      numArray3[38] = (byte) 144 /*0x90*/;
      numArray3[42] = (byte) 88;
      numArray3[3] = (byte) 59;
      numArray3[30] = (byte) 186;
      numArray3[31 /*0x1F*/] = (byte) 102;
      numArray3[51] = (byte) 13;
      numArray3[46] = (byte) 25;
      numArray3[44] = (byte) 195;
      numArray3[16 /*0x10*/] = (byte) 157;
      numArray3[50] = (byte) 100;
      numArray3[14] = (byte) 182;
      numArray3[6] = (byte) 250;
      numArray3[1] = (byte) 48 /*0x30*/;
      numArray3[49] = (byte) 210;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[2]
      {
        (byte) 65,
        (byte) 174
      };
      byte[] numArray5 = new byte[2]
      {
        (byte) 220,
        (byte) 15
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 2);
      for (int index = 0; index < 2; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[57];
    byte[] numArray7 = new byte[55]
    {
      (byte) 52,
      (byte) 75,
      (byte) 135,
      (byte) 65,
      (byte) 230,
      (byte) 26,
      (byte) 247,
      (byte) 176 /*0xB0*/,
      (byte) 181,
      (byte) 102,
      (byte) 245,
      (byte) 6,
      (byte) 150,
      (byte) 160 /*0xA0*/,
      (byte) 226,
      (byte) 223,
      (byte) 28,
      (byte) 64 /*0x40*/,
      (byte) 142,
      (byte) 36,
      (byte) 55,
      (byte) 155,
      (byte) 153,
      (byte) 92,
      (byte) 162,
      (byte) 178,
      (byte) 18,
      (byte) 4,
      (byte) 125,
      (byte) 33,
      (byte) 45,
      (byte) 107,
      (byte) 237,
      (byte) 15,
      (byte) 39,
      (byte) 30,
      (byte) 101,
      (byte) 217,
      (byte) 195,
      (byte) 36,
      (byte) 59,
      (byte) 79,
      (byte) 136,
      (byte) 95,
      (byte) 175,
      (byte) 232,
      (byte) 236,
      (byte) 192 /*0xC0*/,
      (byte) 94,
      (byte) 207,
      (byte) 111,
      (byte) 180,
      (byte) 169,
      (byte) 195,
      (byte) 23
    };
    byte[] numArray8 = new byte[55]
    {
      (byte) 203,
      (byte) 126,
      (byte) 85,
      (byte) 49,
      (byte) 248,
      (byte) 207,
      (byte) 159,
      (byte) 158,
      (byte) 178,
      (byte) 158,
      (byte) 35,
      (byte) 62,
      (byte) 128 /*0x80*/,
      (byte) 247,
      (byte) 232,
      (byte) 204,
      (byte) 104,
      (byte) 61,
      (byte) 94,
      (byte) 76,
      (byte) 212,
      (byte) 57,
      (byte) 151,
      (byte) 6,
      (byte) 65,
      (byte) 213,
      (byte) 111,
      (byte) 75,
      (byte) 20,
      (byte) 245,
      (byte) 39,
      (byte) 1,
      (byte) 38,
      (byte) 254,
      (byte) 58,
      (byte) 125,
      (byte) 106,
      (byte) 100,
      (byte) 107,
      (byte) 147,
      (byte) 156,
      (byte) 252,
      (byte) 170,
      (byte) 108,
      (byte) 239,
      (byte) 112 /*0x70*/,
      (byte) 244,
      (byte) 191,
      (byte) 236,
      (byte) 94,
      (byte) 2,
      (byte) 225,
      (byte) 159,
      (byte) 170,
      (byte) 213
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[2]{ (byte) 42, (byte) 158 };
    byte[] numArray10 = new byte[2]
    {
      (byte) 173,
      (byte) 179
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 2);
    for (int index = 0; index < 2; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_13621()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10];
      numArray2[1] = (byte) 24;
      numArray2[4] = (byte) 195;
      numArray2[2] = (byte) 135;
      numArray2[3] = (byte) 121;
      numArray2[0] = (byte) 138;
      numArray2[5] = (byte) 121;
      numArray2[6] = (byte) 67;
      numArray2[9] = (byte) 232;
      numArray2[7] = (byte) 124;
      numArray2[8] = (byte) 106;
      byte[] numArray3 = new byte[10];
      numArray3[0] = (byte) 7;
      numArray3[5] = (byte) 54;
      numArray3[6] = (byte) 250;
      numArray3[3] = (byte) 106;
      numArray3[4] = (byte) 36;
      numArray3[9] = (byte) 104;
      numArray3[2] = (byte) 82;
      numArray3[7] = (byte) 26;
      numArray3[8] = (byte) 156;
      numArray3[1] = (byte) 127 /*0x7F*/;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10];
    numArray5[9] = (byte) 63 /*0x3F*/;
    numArray5[0] = (byte) 38;
    numArray5[8] = (byte) 55;
    numArray5[5] = (byte) 155;
    numArray5[6] = (byte) 85;
    numArray5[4] = (byte) 71;
    numArray5[2] = (byte) 208 /*0xD0*/;
    numArray5[7] = (byte) 44;
    numArray5[3] = (byte) 101;
    numArray5[1] = (byte) 36;
    byte[] numArray6 = new byte[10]
    {
      (byte) 47,
      (byte) 80 /*0x50*/,
      (byte) 139,
      (byte) 89,
      (byte) 62,
      (byte) 102,
      (byte) 251,
      (byte) 142,
      (byte) 150,
      (byte) 54
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13622()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[46];
      byte[] numArray2 = new byte[46];
      numArray2[25] = (byte) 86;
      numArray2[1] = (byte) 234;
      numArray2[43] = (byte) 102;
      numArray2[11] = (byte) 136;
      numArray2[4] = (byte) 7;
      numArray2[28] = (byte) 250;
      numArray2[6] = (byte) 77;
      numArray2[42] = (byte) 98;
      numArray2[8] = (byte) 204;
      numArray2[2] = (byte) 87;
      numArray2[18] = (byte) 65;
      numArray2[30] = (byte) 16 /*0x10*/;
      numArray2[12] = (byte) 233;
      numArray2[13] = (byte) 96 /*0x60*/;
      numArray2[10] = (byte) 19;
      numArray2[35] = (byte) 92;
      numArray2[16 /*0x10*/] = (byte) 87;
      numArray2[22] = (byte) 8;
      numArray2[34] = (byte) 213;
      numArray2[23] = (byte) 242;
      numArray2[17] = (byte) 133;
      numArray2[32 /*0x20*/] = (byte) 91;
      numArray2[20] = (byte) 79;
      numArray2[38] = (byte) 36;
      numArray2[24] = (byte) 188;
      numArray2[14] = (byte) 10;
      numArray2[26] = (byte) 33;
      numArray2[27] = (byte) 103;
      numArray2[44] = (byte) 101;
      numArray2[39] = (byte) 74;
      numArray2[40] = (byte) 241;
      numArray2[31 /*0x1F*/] = (byte) 101;
      numArray2[29] = (byte) 151;
      numArray2[36] = (byte) 67;
      numArray2[0] = (byte) 143;
      numArray2[7] = (byte) 160 /*0xA0*/;
      numArray2[3] = (byte) 63 /*0x3F*/;
      numArray2[37] = (byte) 22;
      numArray2[15] = (byte) 10;
      numArray2[33] = (byte) 225;
      numArray2[21] = (byte) 200;
      numArray2[41] = (byte) 58;
      numArray2[9] = (byte) 31 /*0x1F*/;
      numArray2[5] = (byte) 236;
      numArray2[19] = (byte) 130;
      numArray2[45] = (byte) 245;
      byte[] numArray3 = new byte[46]
      {
        (byte) 142,
        (byte) 203,
        (byte) 45,
        (byte) 92,
        (byte) 189,
        (byte) 75,
        (byte) 59,
        (byte) 153,
        (byte) 125,
        (byte) 45,
        (byte) 37,
        (byte) 124,
        (byte) 161,
        (byte) 56,
        (byte) 172,
        (byte) 237,
        (byte) 34,
        (byte) 111,
        (byte) 182,
        (byte) 35,
        (byte) 193,
        (byte) 214,
        (byte) 161,
        (byte) 213,
        (byte) 239,
        (byte) 173,
        (byte) 57,
        (byte) 208 /*0xD0*/,
        (byte) 142,
        (byte) 34,
        (byte) 131,
        (byte) 27,
        (byte) 120,
        (byte) 126,
        (byte) 221,
        (byte) 156,
        (byte) 87,
        (byte) 69,
        (byte) 47,
        (byte) 10,
        (byte) 81,
        (byte) 238,
        (byte) 239,
        (byte) 249,
        (byte) 101,
        (byte) 231
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 46);
      for (int index = 0; index < 46; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[46];
    byte[] numArray5 = new byte[46];
    numArray5[39] = (byte) 0;
    numArray5[44] = (byte) 125;
    numArray5[2] = (byte) 45;
    numArray5[45] = (byte) 184;
    numArray5[15] = (byte) 97;
    numArray5[4] = (byte) 184;
    numArray5[6] = (byte) 242;
    numArray5[35] = (byte) 127 /*0x7F*/;
    numArray5[8] = (byte) 252;
    numArray5[9] = (byte) 124;
    numArray5[10] = (byte) 236;
    numArray5[11] = (byte) 251;
    numArray5[17] = (byte) 215;
    numArray5[12] = (byte) 136;
    numArray5[14] = (byte) 139;
    numArray5[1] = (byte) 149;
    numArray5[22] = (byte) 238;
    numArray5[7] = (byte) 130;
    numArray5[37] = (byte) 74;
    numArray5[19] = (byte) 84;
    numArray5[20] = (byte) 184;
    numArray5[43] = (byte) 58;
    numArray5[0] = (byte) 11;
    numArray5[23] = (byte) 203;
    numArray5[24] = (byte) 91;
    numArray5[25] = (byte) 198;
    numArray5[26] = (byte) 24;
    numArray5[27] = (byte) 197;
    numArray5[3] = (byte) 87;
    numArray5[29] = (byte) 52;
    numArray5[32 /*0x20*/] = (byte) 237;
    numArray5[5] = (byte) 199;
    numArray5[21] = (byte) 41;
    numArray5[33] = (byte) 119;
    numArray5[36] = (byte) 56;
    numArray5[34] = (byte) 133;
    numArray5[13] = (byte) 182;
    numArray5[18] = (byte) 186;
    numArray5[38] = (byte) 212;
    numArray5[28] = (byte) 125;
    numArray5[40] = (byte) 105;
    numArray5[41] = (byte) 144 /*0x90*/;
    numArray5[42] = (byte) 192 /*0xC0*/;
    numArray5[31 /*0x1F*/] = (byte) 172;
    numArray5[30] = (byte) 232;
    numArray5[16 /*0x10*/] = (byte) 65;
    byte[] numArray6 = new byte[46]
    {
      (byte) 158,
      (byte) 59,
      (byte) 32 /*0x20*/,
      (byte) 6,
      (byte) 221,
      (byte) 233,
      (byte) 229,
      (byte) 93,
      (byte) 32 /*0x20*/,
      (byte) 60,
      (byte) 125,
      (byte) 97,
      (byte) 34,
      (byte) 152,
      (byte) 214,
      (byte) 72,
      (byte) 46,
      (byte) 167,
      (byte) 209,
      (byte) 188,
      (byte) 1,
      (byte) 125,
      (byte) 69,
      (byte) 102,
      (byte) 119,
      (byte) 251,
      (byte) 155,
      (byte) 153,
      (byte) 135,
      (byte) 197,
      (byte) 69,
      (byte) 89,
      (byte) 101,
      (byte) 54,
      (byte) 177,
      (byte) 175,
      (byte) 154,
      (byte) 1,
      (byte) 46,
      (byte) 225,
      (byte) 175,
      (byte) 65,
      (byte) 62,
      (byte) 157,
      (byte) 141,
      (byte) 213
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 46);
    for (int index = 0; index < 46; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13623()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[25];
      byte[] numArray2 = new byte[25]
      {
        (byte) 139,
        (byte) 249,
        (byte) 166,
        (byte) 61,
        (byte) 171,
        (byte) 76,
        (byte) 45,
        (byte) 171,
        (byte) 130,
        (byte) 65,
        (byte) 3,
        (byte) 32 /*0x20*/,
        (byte) 50,
        (byte) 158,
        (byte) 121,
        (byte) 242,
        (byte) 19,
        (byte) 89,
        (byte) 137,
        (byte) 245,
        (byte) 31 /*0x1F*/,
        (byte) 51,
        (byte) 203,
        (byte) 177,
        (byte) 109
      };
      byte[] numArray3 = new byte[25]
      {
        (byte) 142,
        (byte) 215,
        (byte) 248,
        (byte) 69,
        (byte) 192 /*0xC0*/,
        (byte) 49,
        (byte) 216,
        (byte) 112 /*0x70*/,
        (byte) 155,
        (byte) 249,
        (byte) 228,
        (byte) 196,
        (byte) 174,
        (byte) 91,
        (byte) 133,
        (byte) 182,
        (byte) 18,
        (byte) 45,
        (byte) 39,
        (byte) 141,
        (byte) 45,
        (byte) 219,
        (byte) 62,
        (byte) 206,
        (byte) 202
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 25);
      for (int index = 0; index < 25; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[25];
    byte[] numArray5 = new byte[25]
    {
      (byte) 51,
      (byte) 83,
      (byte) 168,
      (byte) 33,
      (byte) 105,
      (byte) 176 /*0xB0*/,
      (byte) 155,
      (byte) 111,
      (byte) 13,
      (byte) 16 /*0x10*/,
      (byte) 247,
      (byte) 28,
      (byte) 140,
      (byte) 98,
      (byte) 78,
      (byte) 186,
      (byte) 223,
      (byte) 178,
      (byte) 156,
      (byte) 18,
      (byte) 41,
      (byte) 230,
      (byte) 194,
      (byte) 244,
      (byte) 88
    };
    byte[] numArray6 = new byte[25];
    numArray6[18] = (byte) 69;
    numArray6[20] = (byte) 144 /*0x90*/;
    numArray6[21] = byte.MaxValue;
    numArray6[3] = (byte) 183;
    numArray6[8] = (byte) 183;
    numArray6[5] = (byte) 237;
    numArray6[6] = (byte) 251;
    numArray6[19] = (byte) 173;
    numArray6[2] = (byte) 209;
    numArray6[9] = (byte) 138;
    numArray6[7] = (byte) 79;
    numArray6[11] = (byte) 11;
    numArray6[12] = (byte) 138;
    numArray6[13] = (byte) 90;
    numArray6[1] = (byte) 164;
    numArray6[15] = (byte) 6;
    numArray6[16 /*0x10*/] = (byte) 95;
    numArray6[17] = (byte) 116;
    numArray6[14] = (byte) 0;
    numArray6[10] = (byte) 66;
    numArray6[24] = (byte) 151;
    numArray6[0] = (byte) 43;
    numArray6[22] = (byte) 39;
    numArray6[23] = (byte) 228;
    numArray6[4] = (byte) 238;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 25);
    for (int index = 0; index < 25; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13624()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 100,
        (byte) 77,
        (byte) 144 /*0x90*/,
        (byte) 179,
        (byte) 200,
        (byte) 14,
        (byte) 148,
        (byte) 67,
        (byte) 65,
        (byte) 23
      };
      byte[] numArray3 = new byte[10]
      {
        (byte) 4,
        (byte) 228,
        (byte) 176 /*0xB0*/,
        (byte) 163,
        (byte) 247,
        (byte) 152,
        (byte) 224 /*0xE0*/,
        (byte) 248,
        (byte) 91,
        (byte) 104
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
      (byte) 237,
      (byte) 235,
      (byte) 30,
      (byte) 33,
      (byte) 253,
      (byte) 251,
      (byte) 26,
      (byte) 15,
      (byte) 32 /*0x20*/,
      (byte) 1
    };
    byte[] numArray6 = new byte[10];
    numArray6[5] = (byte) 13;
    numArray6[2] = (byte) 179;
    numArray6[4] = (byte) 220;
    numArray6[6] = (byte) 165;
    numArray6[1] = (byte) 216;
    numArray6[9] = (byte) 112 /*0x70*/;
    numArray6[0] = (byte) 124;
    numArray6[7] = (byte) 58;
    numArray6[8] = (byte) 84;
    numArray6[3] = (byte) 242;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13625()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[12];
      byte[] numArray2 = new byte[12]
      {
        (byte) 228,
        (byte) 25,
        (byte) 168,
        (byte) 237,
        (byte) 191,
        (byte) 200,
        (byte) 205,
        (byte) 193,
        (byte) 215,
        (byte) 61,
        (byte) 123,
        (byte) 209
      };
      byte[] numArray3 = new byte[12]
      {
        (byte) 32 /*0x20*/,
        (byte) 235,
        (byte) 20,
        (byte) 240 /*0xF0*/,
        (byte) 125,
        (byte) 60,
        (byte) 27,
        (byte) 125,
        (byte) 200,
        (byte) 57,
        (byte) 104,
        (byte) 205
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 12);
      for (int index = 0; index < 12; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[12];
    byte[] numArray5 = new byte[12];
    numArray5[9] = (byte) 53;
    numArray5[1] = (byte) 135;
    numArray5[2] = (byte) 62;
    numArray5[4] = (byte) 75;
    numArray5[10] = (byte) 207;
    numArray5[5] = (byte) 207;
    numArray5[3] = (byte) 68;
    numArray5[0] = (byte) 191;
    numArray5[8] = (byte) 27;
    numArray5[6] = (byte) 33;
    numArray5[7] = (byte) 220;
    numArray5[11] = (byte) 36;
    byte[] numArray6 = new byte[12]
    {
      (byte) 105,
      (byte) 245,
      (byte) 68,
      (byte) 73,
      (byte) 245,
      (byte) 164,
      (byte) 174,
      (byte) 123,
      (byte) 88,
      (byte) 7,
      (byte) 187,
      (byte) 177
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 12);
    for (int index = 0; index < 12; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[34];
    byte[] response = new byte[34];
    Array.Copy((Array) sc_13619.sspq, 0, (Array) numArray7, 0, 34);
    key.Query(true, 335, numArray7, response);
    Array.Copy((Array) sc_13619.sspr, 0, (Array) numArray7, 0, 34);
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

  internal static string ssp_appserver_13626()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[12];
      byte[] numArray2 = new byte[12]
      {
        (byte) 164,
        (byte) 168,
        (byte) 34,
        (byte) 245,
        (byte) 166,
        (byte) 216,
        (byte) 122,
        (byte) 49,
        (byte) 187,
        (byte) 225,
        (byte) 174,
        (byte) 121
      };
      byte[] numArray3 = new byte[12];
      numArray3[6] = (byte) 192 /*0xC0*/;
      numArray3[10] = (byte) 82;
      numArray3[0] = (byte) 37;
      numArray3[3] = (byte) 142;
      numArray3[11] = (byte) 53;
      numArray3[5] = (byte) 132;
      numArray3[1] = (byte) 161;
      numArray3[4] = (byte) 196;
      numArray3[8] = (byte) 20;
      numArray3[9] = (byte) 118;
      numArray3[2] = (byte) 246;
      numArray3[7] = (byte) 241;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 12);
      for (int index = 0; index < 12; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[13];
      byte[] response = new byte[13];
      Array.Copy((Array) sc_13619.sspq, 34, (Array) numArray4, 0, 13);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_13619.sspr, 34, (Array) numArray4, 0, 13);
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
    byte[] numArray5 = new byte[12];
    byte[] numArray6 = new byte[12]
    {
      (byte) 122,
      (byte) 13,
      (byte) 47,
      (byte) 152,
      (byte) 57,
      (byte) 75,
      (byte) 166,
      (byte) 230,
      (byte) 143,
      (byte) 56,
      (byte) 187,
      (byte) 102
    };
    byte[] numArray7 = new byte[12];
    numArray7[8] = (byte) 152;
    numArray7[1] = (byte) 159;
    numArray7[2] = (byte) 236;
    numArray7[0] = (byte) 245;
    numArray7[10] = (byte) 119;
    numArray7[4] = (byte) 131;
    numArray7[3] = (byte) 201;
    numArray7[7] = (byte) 226;
    numArray7[6] = (byte) 39;
    numArray7[9] = (byte) 55;
    numArray7[5] = (byte) 9;
    numArray7[11] = (byte) 216;
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 12);
    for (int index = 0; index < 12; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_appserver_13627()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[25];
      byte[] numArray2 = new byte[25]
      {
        (byte) 100,
        (byte) 146,
        (byte) 5,
        (byte) 42,
        (byte) 173,
        (byte) 5,
        (byte) 134,
        (byte) 161,
        (byte) 76,
        (byte) 205,
        (byte) 66,
        (byte) 16 /*0x10*/,
        (byte) 65,
        (byte) 2,
        (byte) 224 /*0xE0*/,
        (byte) 209,
        (byte) 11,
        (byte) 152,
        (byte) 155,
        (byte) 224 /*0xE0*/,
        (byte) 242,
        (byte) 226,
        (byte) 16 /*0x10*/,
        (byte) 52,
        (byte) 230
      };
      byte[] numArray3 = new byte[25]
      {
        (byte) 127 /*0x7F*/,
        (byte) 226,
        (byte) 232,
        (byte) 130,
        (byte) 76,
        (byte) 170,
        (byte) 27,
        (byte) 205,
        (byte) 191,
        (byte) 243,
        (byte) 76,
        (byte) 166,
        (byte) 67,
        (byte) 95,
        (byte) 240 /*0xF0*/,
        (byte) 79,
        (byte) 198,
        (byte) 124,
        (byte) 236,
        (byte) 14,
        (byte) 210,
        (byte) 55,
        (byte) 83,
        (byte) 222,
        (byte) 219
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 25);
      for (int index = 0; index < 25; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[25];
    byte[] numArray5 = new byte[25]
    {
      (byte) 92,
      (byte) 21,
      (byte) 232,
      (byte) 30,
      (byte) 179,
      (byte) 199,
      (byte) 130,
      (byte) 253,
      (byte) 51,
      (byte) 145,
      (byte) 185,
      (byte) 189,
      (byte) 166,
      (byte) 84,
      (byte) 224 /*0xE0*/,
      (byte) 240 /*0xF0*/,
      (byte) 55,
      (byte) 58,
      (byte) 174,
      (byte) 143,
      (byte) 224 /*0xE0*/,
      (byte) 223,
      (byte) 14,
      (byte) 23,
      (byte) 126
    };
    byte[] numArray6 = new byte[25]
    {
      (byte) 52,
      (byte) 28,
      (byte) 24,
      (byte) 185,
      (byte) 226,
      (byte) 20,
      (byte) 139,
      (byte) 214,
      (byte) 34,
      (byte) 160 /*0xA0*/,
      (byte) 153,
      (byte) 111,
      (byte) 138,
      (byte) 231,
      (byte) 158,
      (byte) 106,
      (byte) 98,
      (byte) 160 /*0xA0*/,
      (byte) 91,
      (byte) 155,
      (byte) 118,
      (byte) 143,
      (byte) 177,
      (byte) 66,
      (byte) 46
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 25);
    for (int index = 0; index < 25; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13628()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 209,
        (byte) 67,
        (byte) 225,
        (byte) 248,
        (byte) 205,
        (byte) 69,
        (byte) 55,
        (byte) 228,
        (byte) 36,
        (byte) 92
      };
      byte[] numArray3 = new byte[10]
      {
        (byte) 52,
        (byte) 142,
        (byte) 90,
        (byte) 204,
        (byte) 241,
        (byte) 183,
        (byte) 101,
        (byte) 65,
        (byte) 154,
        (byte) 235
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10];
    numArray5[2] = (byte) 222;
    numArray5[1] = (byte) 122;
    numArray5[0] = (byte) 158;
    numArray5[3] = (byte) 222;
    numArray5[4] = (byte) 57;
    numArray5[7] = (byte) 32 /*0x20*/;
    numArray5[6] = (byte) 2;
    numArray5[8] = (byte) 252;
    numArray5[5] = (byte) 157;
    numArray5[9] = (byte) 171;
    byte[] numArray6 = new byte[10]
    {
      (byte) 245,
      (byte) 26,
      (byte) 235,
      (byte) 234,
      (byte) 9,
      (byte) 154,
      (byte) 147,
      (byte) 2,
      (byte) 96 /*0x60*/,
      (byte) 247
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13629()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[13];
      byte[] numArray2 = new byte[13]
      {
        (byte) 196,
        (byte) 217,
        (byte) 14,
        (byte) 168,
        (byte) 11,
        (byte) 19,
        (byte) 222,
        (byte) 52,
        (byte) 15,
        (byte) 253,
        (byte) 64 /*0x40*/,
        (byte) 148,
        (byte) 56
      };
      byte[] numArray3 = new byte[13];
      numArray3[0] = (byte) 206;
      numArray3[1] = (byte) 59;
      numArray3[9] = (byte) 152;
      numArray3[3] = (byte) 111;
      numArray3[8] = (byte) 209;
      numArray3[4] = (byte) 159;
      numArray3[6] = (byte) 34;
      numArray3[7] = (byte) 195;
      numArray3[2] = (byte) 192 /*0xC0*/;
      numArray3[10] = (byte) 64 /*0x40*/;
      numArray3[5] = (byte) 149;
      numArray3[11] = (byte) 218;
      numArray3[12] = (byte) 51;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 13);
      for (int index = 0; index < 13; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[34];
      byte[] response = new byte[34];
      Array.Copy((Array) sc_13619.sspq, 47, (Array) numArray4, 0, 34);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_13619.sspr, 47, (Array) numArray4, 0, 34);
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
    byte[] numArray5 = new byte[13];
    byte[] numArray6 = new byte[13];
    numArray6[12] = (byte) 14;
    numArray6[1] = (byte) 4;
    numArray6[11] = (byte) 217;
    numArray6[3] = (byte) 50;
    numArray6[5] = (byte) 30;
    numArray6[4] = (byte) 134;
    numArray6[6] = (byte) 191;
    numArray6[7] = (byte) 36;
    numArray6[8] = (byte) 77;
    numArray6[2] = (byte) 104;
    numArray6[10] = (byte) 201;
    numArray6[0] = (byte) 149;
    numArray6[9] = (byte) 234;
    byte[] numArray7 = new byte[13];
    numArray7[7] = (byte) 108;
    numArray7[0] = (byte) 206;
    numArray7[11] = (byte) 96 /*0x60*/;
    numArray7[12] = (byte) 171;
    numArray7[1] = (byte) 195;
    numArray7[5] = (byte) 90;
    numArray7[3] = (byte) 196;
    numArray7[6] = (byte) 186;
    numArray7[8] = (byte) 162;
    numArray7[4] = (byte) 86;
    numArray7[10] = (byte) 208 /*0xD0*/;
    numArray7[9] = (byte) 129;
    numArray7[2] = (byte) 174;
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 13);
    for (int index = 0; index < 13; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_appserver_13630()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[13];
      byte[] numArray2 = new byte[13]
      {
        (byte) 163,
        (byte) 236,
        (byte) 59,
        (byte) 59,
        (byte) 202,
        (byte) 185,
        (byte) 183,
        (byte) 125,
        (byte) 9,
        (byte) 152,
        (byte) 131,
        (byte) 145,
        (byte) 70
      };
      byte[] numArray3 = new byte[13];
      numArray3[8] = (byte) 26;
      numArray3[11] = (byte) 111;
      numArray3[2] = (byte) 1;
      numArray3[3] = (byte) 198;
      numArray3[4] = (byte) 231;
      numArray3[6] = (byte) 249;
      numArray3[10] = (byte) 96 /*0x60*/;
      numArray3[1] = (byte) 140;
      numArray3[0] = (byte) 236;
      numArray3[9] = (byte) 212;
      numArray3[7] = (byte) 59;
      numArray3[5] = (byte) 209;
      numArray3[12] = (byte) 92;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 13);
      for (int index = 0; index < 13; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[13];
    byte[] numArray5 = new byte[13]
    {
      (byte) 12,
      (byte) 134,
      (byte) 68,
      (byte) 160 /*0xA0*/,
      (byte) 129,
      (byte) 11,
      (byte) 138,
      (byte) 181,
      (byte) 149,
      (byte) 177,
      (byte) 143,
      (byte) 43,
      (byte) 158
    };
    byte[] numArray6 = new byte[13]
    {
      (byte) 80 /*0x50*/,
      (byte) 117,
      (byte) 128 /*0x80*/,
      (byte) 120,
      (byte) 107,
      (byte) 159,
      (byte) 110,
      (byte) 61,
      (byte) 167,
      (byte) 46,
      (byte) 235,
      (byte) 25,
      (byte) 83
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 13);
    for (int index = 0; index < 13; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13631()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[25];
      byte[] numArray2 = new byte[25]
      {
        byte.MaxValue,
        (byte) 225,
        (byte) 23,
        (byte) 37,
        (byte) 194,
        (byte) 187,
        (byte) 87,
        (byte) 27,
        (byte) 155,
        (byte) 19,
        (byte) 115,
        (byte) 183,
        (byte) 10,
        (byte) 246,
        (byte) 101,
        (byte) 0,
        (byte) 104,
        (byte) 253,
        (byte) 18,
        (byte) 62,
        (byte) 241,
        (byte) 197,
        (byte) 74,
        (byte) 103,
        (byte) 139
      };
      byte[] numArray3 = new byte[25]
      {
        (byte) 246,
        (byte) 90,
        (byte) 119,
        (byte) 42,
        (byte) 231,
        (byte) 95,
        (byte) 107,
        (byte) 30,
        (byte) 242,
        (byte) 167,
        (byte) 93,
        (byte) 145,
        (byte) 245,
        (byte) 66,
        (byte) 30,
        (byte) 65,
        (byte) 83,
        (byte) 25,
        (byte) 250,
        (byte) 114,
        (byte) 121,
        (byte) 163,
        (byte) 114,
        (byte) 179,
        (byte) 208 /*0xD0*/
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 25);
      for (int index = 0; index < 25; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[25];
    byte[] numArray5 = new byte[25]
    {
      (byte) 21,
      (byte) 145,
      (byte) 62,
      (byte) 193,
      (byte) 179,
      (byte) 228,
      (byte) 212,
      (byte) 114,
      (byte) 159,
      (byte) 238,
      (byte) 73,
      (byte) 203,
      (byte) 237,
      (byte) 133,
      (byte) 229,
      (byte) 247,
      (byte) 23,
      (byte) 254,
      (byte) 138,
      (byte) 187,
      (byte) 41,
      (byte) 43,
      (byte) 54,
      (byte) 83,
      (byte) 165
    };
    byte[] numArray6 = new byte[25];
    numArray6[23] = (byte) 91;
    numArray6[16 /*0x10*/] = (byte) 139;
    numArray6[2] = (byte) 61;
    numArray6[11] = (byte) 133;
    numArray6[12] = (byte) 55;
    numArray6[19] = (byte) 42;
    numArray6[6] = (byte) 136;
    numArray6[17] = (byte) 138;
    numArray6[8] = (byte) 202;
    numArray6[9] = (byte) 203;
    numArray6[18] = (byte) 39;
    numArray6[4] = (byte) 9;
    numArray6[22] = (byte) 177;
    numArray6[3] = (byte) 63 /*0x3F*/;
    numArray6[14] = (byte) 143;
    numArray6[1] = (byte) 210;
    numArray6[15] = (byte) 111;
    numArray6[0] = (byte) 19;
    numArray6[24] = (byte) 41;
    numArray6[21] = (byte) 95;
    numArray6[20] = (byte) 195;
    numArray6[13] = (byte) 16 /*0x10*/;
    numArray6[10] = (byte) 147;
    numArray6[5] = (byte) 27;
    numArray6[7] = (byte) 132;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 25);
    for (int index = 0; index < 25; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13632()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 189,
        (byte) 234,
        (byte) 91,
        (byte) 235,
        (byte) 27,
        (byte) 33,
        (byte) 137,
        (byte) 154,
        (byte) 219,
        (byte) 239
      };
      byte[] numArray3 = new byte[10];
      numArray3[5] = (byte) 123;
      numArray3[1] = (byte) 73;
      numArray3[6] = (byte) 191;
      numArray3[3] = (byte) 94;
      numArray3[4] = (byte) 226;
      numArray3[7] = (byte) 57;
      numArray3[8] = (byte) 55;
      numArray3[2] = (byte) 46;
      numArray3[0] = (byte) 51;
      numArray3[9] = (byte) 218;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10]
    {
      (byte) 188,
      (byte) 159,
      (byte) 94,
      (byte) 65,
      (byte) 238,
      (byte) 89,
      (byte) 67,
      (byte) 95,
      (byte) 13,
      (byte) 13
    };
    byte[] numArray6 = new byte[10];
    numArray6[9] = (byte) 91;
    numArray6[1] = (byte) 68;
    numArray6[0] = (byte) 242;
    numArray6[5] = (byte) 195;
    numArray6[3] = (byte) 59;
    numArray6[4] = (byte) 41;
    numArray6[6] = (byte) 220;
    numArray6[7] = (byte) 35;
    numArray6[8] = (byte) 195;
    numArray6[2] = (byte) 112 /*0x70*/;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13633()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[39];
      byte[] numArray2 = new byte[39];
      numArray2[31 /*0x1F*/] = (byte) 151;
      numArray2[1] = (byte) 236;
      numArray2[33] = (byte) 25;
      numArray2[23] = (byte) 45;
      numArray2[38] = (byte) 115;
      numArray2[5] = (byte) 145;
      numArray2[6] = (byte) 49;
      numArray2[7] = (byte) 156;
      numArray2[2] = (byte) 4;
      numArray2[9] = (byte) 32 /*0x20*/;
      numArray2[17] = (byte) 144 /*0x90*/;
      numArray2[29] = (byte) 31 /*0x1F*/;
      numArray2[36] = (byte) 85;
      numArray2[4] = (byte) 235;
      numArray2[24] = (byte) 58;
      numArray2[15] = (byte) 64 /*0x40*/;
      numArray2[16 /*0x10*/] = (byte) 151;
      numArray2[3] = (byte) 47;
      numArray2[26] = (byte) 24;
      numArray2[8] = (byte) 135;
      numArray2[20] = (byte) 109;
      numArray2[27] = (byte) 198;
      numArray2[18] = (byte) 70;
      numArray2[25] = (byte) 205;
      numArray2[12] = (byte) 188;
      numArray2[37] = (byte) 19;
      numArray2[28] = (byte) 17;
      numArray2[21] = (byte) 204;
      numArray2[0] = (byte) 211;
      numArray2[14] = (byte) 154;
      numArray2[30] = (byte) 89;
      numArray2[11] = (byte) 75;
      numArray2[32 /*0x20*/] = (byte) 146;
      numArray2[10] = (byte) 159;
      numArray2[34] = (byte) 109;
      numArray2[35] = (byte) 2;
      numArray2[22] = (byte) 100;
      numArray2[19] = (byte) 153;
      numArray2[13] = (byte) 223;
      byte[] numArray3 = new byte[39]
      {
        (byte) 46,
        (byte) 140,
        (byte) 80 /*0x50*/,
        (byte) 28,
        (byte) 247,
        (byte) 63 /*0x3F*/,
        (byte) 205,
        (byte) 208 /*0xD0*/,
        (byte) 194,
        (byte) 8,
        (byte) 38,
        (byte) 202,
        (byte) 174,
        (byte) 133,
        (byte) 73,
        (byte) 220,
        (byte) 126,
        (byte) 234,
        (byte) 208 /*0xD0*/,
        (byte) 171,
        (byte) 20,
        (byte) 177,
        (byte) 135,
        (byte) 167,
        (byte) 165,
        (byte) 86,
        (byte) 10,
        (byte) 47,
        (byte) 98,
        (byte) 193,
        (byte) 83,
        (byte) 130,
        (byte) 124,
        (byte) 53,
        (byte) 11,
        (byte) 185,
        (byte) 97,
        (byte) 56,
        (byte) 149
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 39);
      for (int index = 0; index < 39; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[39];
    byte[] numArray5 = new byte[39];
    numArray5[12] = (byte) 85;
    numArray5[31 /*0x1F*/] = (byte) 183;
    numArray5[3] = (byte) 159;
    numArray5[32 /*0x20*/] = (byte) 89;
    numArray5[4] = (byte) 120;
    numArray5[5] = (byte) 184;
    numArray5[26] = (byte) 18;
    numArray5[14] = (byte) 113;
    numArray5[8] = (byte) 102;
    numArray5[9] = (byte) 122;
    numArray5[10] = (byte) 126;
    numArray5[11] = (byte) 30;
    numArray5[22] = (byte) 106;
    numArray5[7] = (byte) 64 /*0x40*/;
    numArray5[17] = (byte) 51;
    numArray5[24] = (byte) 251;
    numArray5[16 /*0x10*/] = (byte) 194;
    numArray5[27] = (byte) 182;
    numArray5[33] = (byte) 42;
    numArray5[1] = (byte) 195;
    numArray5[20] = (byte) 54;
    numArray5[23] = (byte) 91;
    numArray5[35] = (byte) 167;
    numArray5[19] = (byte) 228;
    numArray5[13] = (byte) 2;
    numArray5[25] = (byte) 195;
    numArray5[0] = (byte) 21;
    numArray5[2] = (byte) 202;
    numArray5[28] = (byte) 98;
    numArray5[29] = (byte) 101;
    numArray5[30] = (byte) 29;
    numArray5[6] = (byte) 30;
    numArray5[34] = (byte) 99;
    numArray5[21] = (byte) 144 /*0x90*/;
    numArray5[18] = (byte) 6;
    numArray5[15] = (byte) 107;
    numArray5[36] = (byte) 192 /*0xC0*/;
    numArray5[37] = (byte) 200;
    numArray5[38] = (byte) 78;
    byte[] numArray6 = new byte[39]
    {
      (byte) 176 /*0xB0*/,
      (byte) 247,
      (byte) 146,
      (byte) 56,
      (byte) 175,
      (byte) 172,
      (byte) 214,
      (byte) 243,
      (byte) 94,
      (byte) 9,
      (byte) 64 /*0x40*/,
      (byte) 87,
      (byte) 191,
      (byte) 68,
      (byte) 34,
      (byte) 48 /*0x30*/,
      (byte) 187,
      (byte) 21,
      (byte) 249,
      (byte) 57,
      (byte) 115,
      (byte) 65,
      (byte) 40,
      (byte) 220,
      (byte) 140,
      (byte) 67,
      (byte) 221,
      (byte) 101,
      (byte) 113,
      (byte) 29,
      (byte) 196,
      (byte) 160 /*0xA0*/,
      (byte) 254,
      (byte) 11,
      (byte) 5,
      (byte) 11,
      (byte) 174,
      (byte) 238,
      (byte) 157
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 39);
    for (int index = 0; index < 39; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[43];
    byte[] response = new byte[43];
    Array.Copy((Array) sc_13619.sspq, 81, (Array) numArray7, 0, 43);
    key.Query(true, 335, numArray7, response);
    Array.Copy((Array) sc_13619.sspr, 81, (Array) numArray7, 0, 43);
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

  internal static string ssp_appserver_13634()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[25];
      byte[] numArray2 = new byte[25]
      {
        (byte) 213,
        (byte) 76,
        (byte) 12,
        (byte) 23,
        (byte) 77,
        (byte) 121,
        (byte) 139,
        (byte) 72,
        (byte) 244,
        (byte) 96 /*0x60*/,
        (byte) 31 /*0x1F*/,
        (byte) 231,
        (byte) 250,
        (byte) 162,
        (byte) 150,
        (byte) 59,
        (byte) 51,
        (byte) 132,
        (byte) 221,
        (byte) 66,
        (byte) 207,
        (byte) 22,
        (byte) 223,
        (byte) 77,
        (byte) 51
      };
      byte[] numArray3 = new byte[25];
      numArray3[14] = (byte) 124;
      numArray3[21] = (byte) 96 /*0x60*/;
      numArray3[23] = (byte) 136;
      numArray3[11] = (byte) 55;
      numArray3[10] = (byte) 253;
      numArray3[5] = (byte) 139;
      numArray3[6] = (byte) 3;
      numArray3[7] = (byte) 176 /*0xB0*/;
      numArray3[22] = (byte) 192 /*0xC0*/;
      numArray3[9] = (byte) 72;
      numArray3[2] = (byte) 145;
      numArray3[3] = (byte) 186;
      numArray3[0] = (byte) 157;
      numArray3[13] = (byte) 253;
      numArray3[12] = (byte) 200;
      numArray3[15] = (byte) 227;
      numArray3[16 /*0x10*/] = (byte) 146;
      numArray3[17] = (byte) 187;
      numArray3[18] = (byte) 213;
      numArray3[20] = (byte) 129;
      numArray3[1] = (byte) 90;
      numArray3[19] = (byte) 99;
      numArray3[4] = (byte) 142;
      numArray3[8] = (byte) 16 /*0x10*/;
      numArray3[24] = (byte) 79;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 25);
      for (int index = 0; index < 25; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[25];
    byte[] numArray5 = new byte[25];
    numArray5[6] = (byte) 76;
    numArray5[23] = (byte) 253;
    numArray5[5] = (byte) 227;
    numArray5[16 /*0x10*/] = (byte) 145;
    numArray5[4] = (byte) 190;
    numArray5[0] = (byte) 57;
    numArray5[20] = (byte) 86;
    numArray5[7] = (byte) 231;
    numArray5[8] = (byte) 62;
    numArray5[17] = (byte) 229;
    numArray5[10] = (byte) 119;
    numArray5[12] = (byte) 181;
    numArray5[11] = (byte) 130;
    numArray5[13] = (byte) 225;
    numArray5[14] = (byte) 245;
    numArray5[15] = (byte) 146;
    numArray5[1] = (byte) 137;
    numArray5[2] = (byte) 191;
    numArray5[18] = (byte) 219;
    numArray5[24] = (byte) 133;
    numArray5[3] = (byte) 109;
    numArray5[21] = (byte) 193;
    numArray5[22] = (byte) 151;
    numArray5[9] = (byte) 203;
    numArray5[19] = (byte) 233;
    byte[] numArray6 = new byte[25]
    {
      (byte) 225,
      (byte) 226,
      (byte) 5,
      (byte) 45,
      (byte) 18,
      (byte) 171,
      (byte) 35,
      (byte) 122,
      (byte) 189,
      (byte) 205,
      (byte) 178,
      (byte) 183,
      (byte) 19,
      (byte) 175,
      (byte) 81,
      (byte) 219,
      (byte) 59,
      (byte) 25,
      (byte) 79,
      (byte) 23,
      (byte) 42,
      (byte) 171,
      (byte) 63 /*0x3F*/,
      (byte) 210,
      (byte) 90
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 25);
    for (int index = 0; index < 25; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13635()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 141,
        (byte) 120,
        (byte) 242,
        (byte) 193,
        (byte) 231,
        (byte) 174,
        (byte) 199,
        (byte) 212,
        (byte) 44,
        (byte) 209
      };
      byte[] numArray3 = new byte[10]
      {
        (byte) 228,
        (byte) 184,
        (byte) 100,
        (byte) 146,
        (byte) 219,
        (byte) 210,
        (byte) 82,
        (byte) 125,
        (byte) 189,
        byte.MaxValue
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
      (byte) 46,
      (byte) 20,
      (byte) 122,
      (byte) 67,
      (byte) 249,
      (byte) 78,
      (byte) 104,
      (byte) 154,
      (byte) 181,
      (byte) 127 /*0x7F*/
    };
    byte[] numArray6 = new byte[10]
    {
      (byte) 183,
      (byte) 240 /*0xF0*/,
      (byte) 189,
      (byte) 32 /*0x20*/,
      (byte) 245,
      (byte) 138,
      (byte) 59,
      (byte) 237,
      (byte) 35,
      (byte) 85
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13636()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[45];
      byte[] numArray2 = new byte[45];
      numArray2[33] = (byte) 37;
      numArray2[1] = (byte) 226;
      numArray2[20] = (byte) 89;
      numArray2[0] = (byte) 230;
      numArray2[31 /*0x1F*/] = (byte) 101;
      numArray2[5] = (byte) 121;
      numArray2[6] = (byte) 84;
      numArray2[3] = (byte) 32 /*0x20*/;
      numArray2[24] = (byte) 84;
      numArray2[9] = (byte) 136;
      numArray2[39] = (byte) 111;
      numArray2[11] = (byte) 176 /*0xB0*/;
      numArray2[12] = (byte) 232;
      numArray2[37] = (byte) 176 /*0xB0*/;
      numArray2[14] = (byte) 121;
      numArray2[15] = (byte) 115;
      numArray2[16 /*0x10*/] = (byte) 112 /*0x70*/;
      numArray2[17] = (byte) 50;
      numArray2[18] = (byte) 184;
      numArray2[32 /*0x20*/] = (byte) 56;
      numArray2[36] = (byte) 182;
      numArray2[28] = (byte) 215;
      numArray2[22] = (byte) 26;
      numArray2[42] = (byte) 191;
      numArray2[21] = (byte) 33;
      numArray2[4] = (byte) 137;
      numArray2[10] = (byte) 142;
      numArray2[27] = (byte) 181;
      numArray2[25] = (byte) 184;
      numArray2[35] = (byte) 242;
      numArray2[30] = (byte) 200;
      numArray2[26] = (byte) 171;
      numArray2[13] = (byte) 171;
      numArray2[7] = (byte) 249;
      numArray2[34] = (byte) 224 /*0xE0*/;
      numArray2[2] = (byte) 118;
      numArray2[23] = (byte) 1;
      numArray2[29] = (byte) 248;
      numArray2[38] = (byte) 177;
      numArray2[19] = (byte) 248;
      numArray2[40] = (byte) 138;
      numArray2[41] = (byte) 52;
      numArray2[8] = (byte) 84;
      numArray2[43] = (byte) 242;
      numArray2[44] = (byte) 180;
      byte[] numArray3 = new byte[45]
      {
        (byte) 51,
        (byte) 4,
        (byte) 105,
        (byte) 215,
        (byte) 250,
        (byte) 152,
        (byte) 106,
        (byte) 167,
        (byte) 100,
        (byte) 229,
        (byte) 63 /*0x3F*/,
        (byte) 177,
        (byte) 247,
        (byte) 23,
        (byte) 76,
        (byte) 175,
        (byte) 246,
        (byte) 67,
        (byte) 53,
        (byte) 47,
        (byte) 156,
        (byte) 254,
        (byte) 27,
        (byte) 56,
        (byte) 230,
        (byte) 37,
        (byte) 115,
        (byte) 13,
        (byte) 36,
        (byte) 192 /*0xC0*/,
        (byte) 241,
        (byte) 246,
        (byte) 41,
        (byte) 212,
        (byte) 5,
        (byte) 60,
        (byte) 95,
        (byte) 253,
        (byte) 14,
        (byte) 49,
        (byte) 80 /*0x50*/,
        (byte) 174,
        (byte) 202,
        (byte) 114,
        (byte) 111
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 45);
      for (int index = 0; index < 45; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[45];
    byte[] numArray5 = new byte[45];
    numArray5[25] = (byte) 114;
    numArray5[1] = (byte) 161;
    numArray5[0] = (byte) 224 /*0xE0*/;
    numArray5[4] = (byte) 99;
    numArray5[28] = (byte) 246;
    numArray5[39] = (byte) 245;
    numArray5[30] = (byte) 174;
    numArray5[38] = (byte) 254;
    numArray5[31 /*0x1F*/] = (byte) 93;
    numArray5[20] = (byte) 193;
    numArray5[10] = (byte) 153;
    numArray5[2] = (byte) 235;
    numArray5[12] = (byte) 79;
    numArray5[24] = (byte) 142;
    numArray5[33] = (byte) 218;
    numArray5[15] = (byte) 71;
    numArray5[16 /*0x10*/] = (byte) 9;
    numArray5[17] = (byte) 98;
    numArray5[18] = (byte) 160 /*0xA0*/;
    numArray5[14] = (byte) 91;
    numArray5[3] = (byte) 17;
    numArray5[13] = (byte) 119;
    numArray5[21] = (byte) 126;
    numArray5[6] = (byte) 138;
    numArray5[11] = (byte) 133;
    numArray5[23] = (byte) 10;
    numArray5[26] = (byte) 238;
    numArray5[27] = (byte) 104;
    numArray5[5] = (byte) 50;
    numArray5[42] = (byte) 176 /*0xB0*/;
    numArray5[9] = (byte) 241;
    numArray5[7] = (byte) 208 /*0xD0*/;
    numArray5[32 /*0x20*/] = (byte) 122;
    numArray5[34] = (byte) 159;
    numArray5[8] = (byte) 162;
    numArray5[35] = (byte) 166;
    numArray5[22] = (byte) 223;
    numArray5[37] = (byte) 20;
    numArray5[19] = (byte) 39;
    numArray5[29] = (byte) 205;
    numArray5[40] = (byte) 194;
    numArray5[41] = (byte) 58;
    numArray5[36] = (byte) 131;
    numArray5[43] = (byte) 253;
    numArray5[44] = (byte) 250;
    byte[] numArray6 = new byte[45]
    {
      (byte) 40,
      (byte) 72,
      (byte) 23,
      (byte) 171,
      (byte) 93,
      (byte) 170,
      (byte) 86,
      (byte) 60,
      (byte) 168,
      (byte) 91,
      (byte) 183,
      (byte) 6,
      (byte) 82,
      (byte) 15,
      (byte) 32 /*0x20*/,
      (byte) 92,
      (byte) 8,
      (byte) 128 /*0x80*/,
      (byte) 251,
      (byte) 38,
      (byte) 121,
      (byte) 40,
      (byte) 230,
      (byte) 132,
      (byte) 234,
      (byte) 24,
      (byte) 230,
      (byte) 122,
      (byte) 128 /*0x80*/,
      (byte) 42,
      (byte) 57,
      (byte) 87,
      (byte) 126,
      (byte) 197,
      (byte) 10,
      (byte) 54,
      (byte) 232,
      (byte) 17,
      (byte) 228,
      (byte) 178,
      (byte) 108,
      (byte) 147,
      (byte) 247,
      (byte) 110,
      (byte) 23
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 45);
    for (int index = 0; index < 45; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13637()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[25];
      byte[] numArray2 = new byte[25]
      {
        (byte) 178,
        (byte) 160 /*0xA0*/,
        (byte) 97,
        (byte) 219,
        (byte) 186,
        (byte) 190,
        (byte) 3,
        (byte) 37,
        (byte) 130,
        (byte) 177,
        (byte) 117,
        (byte) 246,
        (byte) 85,
        (byte) 223,
        (byte) 181,
        (byte) 157,
        (byte) 148,
        (byte) 224 /*0xE0*/,
        (byte) 209,
        (byte) 4,
        (byte) 24,
        (byte) 174,
        (byte) 31 /*0x1F*/,
        (byte) 28,
        (byte) 243
      };
      byte[] numArray3 = new byte[25];
      numArray3[8] = (byte) 236;
      numArray3[13] = (byte) 25;
      numArray3[2] = (byte) 118;
      numArray3[7] = (byte) 105;
      numArray3[4] = (byte) 109;
      numArray3[5] = (byte) 0;
      numArray3[16 /*0x10*/] = (byte) 182;
      numArray3[1] = (byte) 199;
      numArray3[15] = (byte) 177;
      numArray3[19] = (byte) 28;
      numArray3[10] = (byte) 100;
      numArray3[11] = (byte) 197;
      numArray3[12] = (byte) 20;
      numArray3[18] = (byte) 124;
      numArray3[6] = (byte) 220;
      numArray3[0] = (byte) 152;
      numArray3[20] = (byte) 106;
      numArray3[17] = (byte) 206;
      numArray3[3] = (byte) 87;
      numArray3[14] = (byte) 238;
      numArray3[22] = (byte) 160 /*0xA0*/;
      numArray3[21] = (byte) 186;
      numArray3[9] = (byte) 200;
      numArray3[23] = (byte) 129;
      numArray3[24] = (byte) 157;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 25);
      for (int index = 0; index < 25; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[25];
    byte[] numArray5 = new byte[25]
    {
      (byte) 141,
      (byte) 245,
      (byte) 3,
      (byte) 128 /*0x80*/,
      (byte) 68,
      (byte) 231,
      (byte) 108,
      (byte) 86,
      (byte) 88,
      (byte) 104,
      (byte) 232,
      (byte) 224 /*0xE0*/,
      (byte) 83,
      (byte) 146,
      (byte) 254,
      (byte) 230,
      (byte) 241,
      (byte) 120,
      (byte) 161,
      (byte) 184,
      (byte) 128 /*0x80*/,
      (byte) 136,
      (byte) 101,
      (byte) 155,
      (byte) 206
    };
    byte[] numArray6 = new byte[25];
    numArray6[13] = (byte) 165;
    numArray6[2] = (byte) 125;
    numArray6[12] = (byte) 199;
    numArray6[3] = (byte) 240 /*0xF0*/;
    numArray6[4] = (byte) 174;
    numArray6[5] = (byte) 44;
    numArray6[6] = (byte) 253;
    numArray6[10] = (byte) 110;
    numArray6[7] = (byte) 17;
    numArray6[8] = (byte) 203;
    numArray6[11] = (byte) 152;
    numArray6[1] = (byte) 242;
    numArray6[19] = (byte) 129;
    numArray6[16 /*0x10*/] = (byte) 199;
    numArray6[14] = (byte) 220;
    numArray6[24] = (byte) 96 /*0x60*/;
    numArray6[22] = (byte) 59;
    numArray6[17] = (byte) 10;
    numArray6[18] = (byte) 118;
    numArray6[9] = (byte) 180;
    numArray6[20] = (byte) 167;
    numArray6[15] = (byte) 32 /*0x20*/;
    numArray6[21] = (byte) 163;
    numArray6[23] = (byte) 241;
    numArray6[0] = (byte) 244;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 25);
    for (int index = 0; index < 25; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[37];
    byte[] response = new byte[37];
    Array.Copy((Array) sc_13619.sspq, 124, (Array) numArray7, 0, 37);
    key.Query(true, 335, numArray7, response);
    Array.Copy((Array) sc_13619.sspr, 124, (Array) numArray7, 0, 37);
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

  internal static string ssp_appserver_13638()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10];
      numArray2[6] = (byte) 134;
      numArray2[1] = (byte) 114;
      numArray2[2] = (byte) 45;
      numArray2[3] = (byte) 251;
      numArray2[5] = (byte) 205;
      numArray2[9] = (byte) 242;
      numArray2[8] = (byte) 93;
      numArray2[7] = (byte) 50;
      numArray2[0] = (byte) 145;
      numArray2[4] = (byte) 21;
      byte[] numArray3 = new byte[10];
      numArray3[1] = (byte) 156;
      numArray3[9] = (byte) 157;
      numArray3[2] = (byte) 208 /*0xD0*/;
      numArray3[6] = (byte) 33;
      numArray3[4] = (byte) 184;
      numArray3[5] = (byte) 50;
      numArray3[3] = (byte) 242;
      numArray3[7] = (byte) 199;
      numArray3[0] = (byte) 133;
      numArray3[8] = (byte) 165;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10]
    {
      (byte) 101,
      (byte) 27,
      (byte) 38,
      (byte) 104,
      (byte) 1,
      (byte) 112 /*0x70*/,
      (byte) 151,
      (byte) 148,
      (byte) 63 /*0x3F*/,
      (byte) 0
    };
    byte[] numArray6 = new byte[10];
    numArray6[0] = (byte) 242;
    numArray6[8] = (byte) 242;
    numArray6[2] = (byte) 232;
    numArray6[1] = (byte) 126;
    numArray6[5] = (byte) 8;
    numArray6[3] = (byte) 42;
    numArray6[4] = (byte) 218;
    numArray6[6] = (byte) 175;
    numArray6[7] = (byte) 138;
    numArray6[9] = (byte) 150;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13639()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[68];
      byte[] numArray2 = new byte[55]
      {
        (byte) 1,
        (byte) 70,
        (byte) 149,
        (byte) 192 /*0xC0*/,
        (byte) 196,
        (byte) 34,
        (byte) 202,
        (byte) 242,
        (byte) 180,
        (byte) 58,
        (byte) 182,
        (byte) 239,
        (byte) 226,
        (byte) 9,
        (byte) 76,
        (byte) 89,
        (byte) 8,
        (byte) 225,
        (byte) 183,
        (byte) 84,
        (byte) 155,
        (byte) 23,
        (byte) 175,
        (byte) 55,
        (byte) 206,
        (byte) 198,
        (byte) 152,
        (byte) 95,
        (byte) 199,
        (byte) 180,
        (byte) 109,
        (byte) 206,
        (byte) 222,
        (byte) 63 /*0x3F*/,
        (byte) 102,
        (byte) 187,
        (byte) 218,
        (byte) 214,
        (byte) 207,
        (byte) 28,
        (byte) 53,
        (byte) 253,
        (byte) 95,
        (byte) 202,
        (byte) 145,
        (byte) 78,
        (byte) 24,
        (byte) 108,
        (byte) 230,
        (byte) 222,
        (byte) 43,
        (byte) 123,
        (byte) 223,
        (byte) 232,
        (byte) 74
      };
      byte[] numArray3 = new byte[55];
      numArray3[29] = (byte) 182;
      numArray3[23] = (byte) 33;
      numArray3[24] = (byte) 114;
      numArray3[33] = (byte) 78;
      numArray3[35] = (byte) 236;
      numArray3[13] = (byte) 85;
      numArray3[6] = (byte) 254;
      numArray3[7] = (byte) 108;
      numArray3[8] = (byte) 26;
      numArray3[0] = (byte) 78;
      numArray3[21] = (byte) 225;
      numArray3[11] = (byte) 87;
      numArray3[9] = (byte) 228;
      numArray3[19] = (byte) 245;
      numArray3[4] = (byte) 11;
      numArray3[27] = (byte) 176 /*0xB0*/;
      numArray3[16 /*0x10*/] = (byte) 168;
      numArray3[20] = (byte) 21;
      numArray3[26] = (byte) 235;
      numArray3[12] = (byte) 212;
      numArray3[2] = (byte) 76;
      numArray3[3] = (byte) 243;
      numArray3[22] = (byte) 55;
      numArray3[18] = (byte) 23;
      numArray3[39] = (byte) 5;
      numArray3[51] = (byte) 210;
      numArray3[38] = (byte) 46;
      numArray3[45] = (byte) 69;
      numArray3[28] = (byte) 52;
      numArray3[15] = (byte) 236;
      numArray3[30] = (byte) 108;
      numArray3[31 /*0x1F*/] = (byte) 110;
      numArray3[1] = (byte) 143;
      numArray3[48 /*0x30*/] = (byte) 170;
      numArray3[43] = (byte) 130;
      numArray3[53] = (byte) 13;
      numArray3[36] = (byte) 10;
      numArray3[34] = (byte) 147;
      numArray3[10] = (byte) 219;
      numArray3[25] = (byte) 98;
      numArray3[40] = (byte) 34;
      numArray3[41] = (byte) 104;
      numArray3[42] = (byte) 139;
      numArray3[14] = (byte) 28;
      numArray3[50] = (byte) 113;
      numArray3[37] = (byte) 112 /*0x70*/;
      numArray3[46] = (byte) 84;
      numArray3[47] = (byte) 93;
      numArray3[52] = (byte) 75;
      numArray3[49] = (byte) 0;
      numArray3[32 /*0x20*/] = (byte) 102;
      numArray3[5] = (byte) 55;
      numArray3[17] = (byte) 191;
      numArray3[44] = (byte) 209;
      numArray3[54] = (byte) 250;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[13]
      {
        (byte) 183,
        (byte) 114,
        (byte) 79,
        (byte) 94,
        (byte) 241,
        (byte) 65,
        (byte) 117,
        (byte) 218,
        (byte) 248,
        (byte) 28,
        (byte) 173,
        (byte) 38,
        (byte) 107
      };
      byte[] numArray5 = new byte[13];
      numArray5[2] = (byte) 83;
      numArray5[4] = (byte) 88;
      numArray5[7] = (byte) 163;
      numArray5[3] = (byte) 202;
      numArray5[8] = (byte) 182;
      numArray5[5] = (byte) 252;
      numArray5[6] = (byte) 254;
      numArray5[9] = (byte) 158;
      numArray5[10] = (byte) 189;
      numArray5[1] = (byte) 157;
      numArray5[0] = (byte) 11;
      numArray5[11] = (byte) 227;
      numArray5[12] = (byte) 243;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 13);
      for (int index = 0; index < 13; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[26];
      byte[] response = new byte[26];
      Array.Copy((Array) sc_13619.sspq, 161, (Array) numArray6, 0, 26);
      key.Query(true, 335, numArray6, response);
      Array.Copy((Array) sc_13619.sspr, 161, (Array) numArray6, 0, 26);
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
    byte[] numArray7 = new byte[68];
    byte[] numArray8 = new byte[55]
    {
      (byte) 48 /*0x30*/,
      (byte) 22,
      (byte) 252,
      (byte) 205,
      (byte) 184,
      (byte) 229,
      (byte) 81,
      (byte) 76,
      (byte) 240 /*0xF0*/,
      (byte) 100,
      (byte) 221,
      (byte) 248,
      (byte) 59,
      (byte) 219,
      (byte) 190,
      (byte) 212,
      (byte) 125,
      (byte) 58,
      (byte) 109,
      (byte) 197,
      (byte) 75,
      (byte) 33,
      (byte) 112 /*0x70*/,
      (byte) 190,
      (byte) 58,
      (byte) 42,
      (byte) 227,
      (byte) 96 /*0x60*/,
      (byte) 111,
      (byte) 183,
      (byte) 104,
      (byte) 109,
      (byte) 169,
      (byte) 11,
      (byte) 65,
      (byte) 107,
      (byte) 55,
      (byte) 10,
      (byte) 216,
      (byte) 4,
      (byte) 209,
      (byte) 100,
      (byte) 48 /*0x30*/,
      (byte) 99,
      (byte) 198,
      (byte) 241,
      (byte) 35,
      (byte) 209,
      (byte) 105,
      (byte) 103,
      (byte) 246,
      (byte) 165,
      (byte) 110,
      (byte) 4,
      (byte) 110
    };
    byte[] numArray9 = new byte[55]
    {
      (byte) 29,
      (byte) 58,
      (byte) 165,
      (byte) 19,
      (byte) 135,
      (byte) 162,
      (byte) 137,
      (byte) 136,
      (byte) 231,
      (byte) 244,
      (byte) 97,
      (byte) 36,
      (byte) 228,
      (byte) 1,
      (byte) 232,
      (byte) 36,
      (byte) 2,
      (byte) 150,
      (byte) 56,
      (byte) 4,
      (byte) 193,
      (byte) 163,
      (byte) 212,
      (byte) 138,
      (byte) 103,
      (byte) 181,
      (byte) 101,
      (byte) 164,
      (byte) 191,
      (byte) 84,
      (byte) 26,
      (byte) 34,
      (byte) 31 /*0x1F*/,
      (byte) 93,
      (byte) 106,
      (byte) 184,
      (byte) 164,
      (byte) 42,
      (byte) 251,
      (byte) 81,
      (byte) 109,
      (byte) 142,
      (byte) 37,
      (byte) 10,
      (byte) 226,
      (byte) 41,
      (byte) 224 /*0xE0*/,
      (byte) 122,
      (byte) 33,
      (byte) 34,
      (byte) 194,
      (byte) 84,
      (byte) 162,
      (byte) 64 /*0x40*/,
      (byte) 94
    };
    key.Query(true, 335, numArray8, numArray8);
    Array.Copy((Array) numArray8, 0, (Array) numArray7, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray7[index] ^= numArray9[index];
    byte[] numArray10 = new byte[13]
    {
      (byte) 62,
      (byte) 147,
      (byte) 142,
      (byte) 2,
      (byte) 241,
      (byte) 221,
      (byte) 56,
      (byte) 251,
      (byte) 112 /*0x70*/,
      (byte) 78,
      (byte) 47,
      (byte) 153,
      (byte) 83
    };
    byte[] numArray11 = new byte[13]
    {
      (byte) 159,
      (byte) 91,
      (byte) 181,
      (byte) 73,
      (byte) 148,
      (byte) 73,
      (byte) 83,
      (byte) 48 /*0x30*/,
      (byte) 221,
      (byte) 171,
      (byte) 89,
      (byte) 191,
      (byte) 114
    };
    key.Query(true, 335, numArray10, numArray10);
    Array.Copy((Array) numArray10, 0, (Array) numArray7, 55, 13);
    for (int index = 0; index < 13; ++index)
      numArray7[index + 55] ^= numArray11[index];
    byte[] numArray12 = new byte[38];
    byte[] response1 = new byte[38];
    Array.Copy((Array) sc_13619.sspq, 187, (Array) numArray12, 0, 38);
    key.Query(true, 335, numArray12, response1);
    Array.Copy((Array) sc_13619.sspr, 187, (Array) numArray12, 0, 38);
    for (int index = 0; index < numArray12.Length; ++index)
    {
      if ((int) numArray12[index] != (int) response1[index])
      {
        key.TagValue = (int) response1[index];
        break;
      }
    }
    return Encoding.UTF8.GetString(numArray7);
  }

  internal static string ssp_appserver_13640()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[69];
      byte[] numArray2 = new byte[55]
      {
        (byte) 74,
        (byte) 135,
        (byte) 240 /*0xF0*/,
        (byte) 250,
        (byte) 212,
        (byte) 119,
        (byte) 57,
        (byte) 113,
        (byte) 169,
        (byte) 145,
        (byte) 103,
        (byte) 141,
        (byte) 163,
        (byte) 251,
        (byte) 23,
        (byte) 102,
        (byte) 20,
        (byte) 96 /*0x60*/,
        (byte) 248,
        (byte) 164,
        (byte) 161,
        (byte) 165,
        (byte) 18,
        (byte) 218,
        (byte) 224 /*0xE0*/,
        (byte) 62,
        (byte) 60,
        (byte) 126,
        (byte) 248,
        (byte) 148,
        (byte) 23,
        (byte) 21,
        (byte) 45,
        (byte) 179,
        (byte) 45,
        (byte) 194,
        (byte) 17,
        (byte) 228,
        (byte) 178,
        (byte) 129,
        (byte) 47,
        (byte) 228,
        (byte) 249,
        (byte) 98,
        (byte) 208 /*0xD0*/,
        (byte) 137,
        (byte) 37,
        (byte) 137,
        (byte) 44,
        (byte) 159,
        (byte) 252,
        (byte) 38,
        (byte) 36,
        (byte) 243,
        (byte) 164
      };
      byte[] numArray3 = new byte[55];
      numArray3[43] = (byte) 228;
      numArray3[1] = (byte) 170;
      numArray3[54] = (byte) 208 /*0xD0*/;
      numArray3[13] = (byte) 239;
      numArray3[37] = (byte) 50;
      numArray3[5] = (byte) 20;
      numArray3[53] = (byte) 81;
      numArray3[2] = (byte) 77;
      numArray3[52] = (byte) 226;
      numArray3[9] = (byte) 88;
      numArray3[10] = (byte) 6;
      numArray3[11] = (byte) 52;
      numArray3[12] = (byte) 223;
      numArray3[7] = (byte) 118;
      numArray3[39] = (byte) 188;
      numArray3[15] = (byte) 53;
      numArray3[3] = (byte) 125;
      numArray3[17] = (byte) 171;
      numArray3[18] = (byte) 240 /*0xF0*/;
      numArray3[21] = (byte) 135;
      numArray3[48 /*0x30*/] = (byte) 186;
      numArray3[31 /*0x1F*/] = (byte) 15;
      numArray3[32 /*0x20*/] = (byte) 250;
      numArray3[8] = (byte) 104;
      numArray3[24] = (byte) 209;
      numArray3[25] = (byte) 112 /*0x70*/;
      numArray3[26] = (byte) 50;
      numArray3[27] = (byte) 98;
      numArray3[33] = (byte) 70;
      numArray3[41] = (byte) 197;
      numArray3[22] = (byte) 240 /*0xF0*/;
      numArray3[29] = (byte) 193;
      numArray3[14] = (byte) 18;
      numArray3[47] = (byte) 56;
      numArray3[0] = (byte) 194;
      numArray3[28] = (byte) 68;
      numArray3[36] = (byte) 22;
      numArray3[30] = (byte) 102;
      numArray3[38] = (byte) 168;
      numArray3[34] = (byte) 16 /*0x10*/;
      numArray3[40] = (byte) 250;
      numArray3[4] = (byte) 243;
      numArray3[44] = (byte) 41;
      numArray3[23] = (byte) 163;
      numArray3[6] = (byte) 22;
      numArray3[45] = (byte) 127 /*0x7F*/;
      numArray3[46] = (byte) 175;
      numArray3[20] = (byte) 66;
      numArray3[35] = (byte) 79;
      numArray3[49] = (byte) 94;
      numArray3[50] = (byte) 199;
      numArray3[51] = (byte) 52;
      numArray3[19] = (byte) 198;
      numArray3[42] = (byte) 2;
      numArray3[16 /*0x10*/] = (byte) 91;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[14]
      {
        (byte) 11,
        (byte) 195,
        (byte) 124,
        (byte) 135,
        (byte) 104,
        (byte) 33,
        (byte) 124,
        (byte) 137,
        (byte) 97,
        (byte) 37,
        (byte) 225,
        (byte) 68,
        (byte) 6,
        (byte) 88
      };
      byte[] numArray5 = new byte[14];
      numArray5[7] = (byte) 24;
      numArray5[3] = (byte) 75;
      numArray5[13] = (byte) 135;
      numArray5[6] = (byte) 3;
      numArray5[1] = (byte) 111;
      numArray5[5] = (byte) 238;
      numArray5[8] = (byte) 141;
      numArray5[2] = (byte) 143;
      numArray5[0] = (byte) 99;
      numArray5[9] = (byte) 248;
      numArray5[10] = (byte) 249;
      numArray5[11] = (byte) 156;
      numArray5[12] = (byte) 178;
      numArray5[4] = (byte) 203;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 14);
      for (int index = 0; index < 14; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[69];
    byte[] numArray7 = new byte[55];
    numArray7[29] = (byte) 20;
    numArray7[44] = (byte) 106;
    numArray7[2] = (byte) 137;
    numArray7[3] = (byte) 240 /*0xF0*/;
    numArray7[6] = (byte) 191;
    numArray7[5] = (byte) 160 /*0xA0*/;
    numArray7[42] = (byte) 9;
    numArray7[7] = (byte) 12;
    numArray7[13] = (byte) 83;
    numArray7[9] = (byte) 104;
    numArray7[28] = (byte) 238;
    numArray7[11] = (byte) 29;
    numArray7[12] = (byte) 4;
    numArray7[27] = (byte) 144 /*0x90*/;
    numArray7[21] = (byte) 147;
    numArray7[51] = (byte) 99;
    numArray7[39] = (byte) 238;
    numArray7[4] = (byte) 6;
    numArray7[14] = (byte) 119;
    numArray7[19] = (byte) 125;
    numArray7[20] = (byte) 124;
    numArray7[46] = (byte) 40;
    numArray7[38] = (byte) 95;
    numArray7[15] = (byte) 209;
    numArray7[24] = (byte) 127 /*0x7F*/;
    numArray7[22] = (byte) 49;
    numArray7[45] = (byte) 109;
    numArray7[30] = (byte) 177;
    numArray7[18] = (byte) 233;
    numArray7[17] = (byte) 103;
    numArray7[23] = (byte) 251;
    numArray7[31 /*0x1F*/] = (byte) 18;
    numArray7[32 /*0x20*/] = (byte) 30;
    numArray7[33] = (byte) 104;
    numArray7[34] = (byte) 185;
    numArray7[35] = (byte) 179;
    numArray7[36] = (byte) 125;
    numArray7[37] = (byte) 103;
    numArray7[26] = (byte) 152;
    numArray7[10] = (byte) 93;
    numArray7[40] = (byte) 112 /*0x70*/;
    numArray7[41] = (byte) 244;
    numArray7[25] = (byte) 150;
    numArray7[43] = (byte) 94;
    numArray7[8] = (byte) 73;
    numArray7[16 /*0x10*/] = (byte) 242;
    numArray7[53] = (byte) 196;
    numArray7[47] = (byte) 145;
    numArray7[48 /*0x30*/] = (byte) 127 /*0x7F*/;
    numArray7[49] = (byte) 168;
    numArray7[50] = (byte) 26;
    numArray7[54] = (byte) 63 /*0x3F*/;
    numArray7[52] = (byte) 141;
    numArray7[0] = (byte) 40;
    numArray7[1] = (byte) 144 /*0x90*/;
    byte[] numArray8 = new byte[55]
    {
      (byte) 125,
      (byte) 71,
      (byte) 84,
      (byte) 124,
      (byte) 195,
      (byte) 218,
      (byte) 156,
      (byte) 161,
      (byte) 220,
      (byte) 78,
      (byte) 155,
      (byte) 5,
      (byte) 17,
      (byte) 223,
      (byte) 38,
      (byte) 175,
      (byte) 119,
      (byte) 200,
      (byte) 140,
      (byte) 197,
      (byte) 146,
      (byte) 194,
      (byte) 152,
      (byte) 110,
      (byte) 238,
      (byte) 174,
      (byte) 156,
      (byte) 79,
      (byte) 199,
      (byte) 7,
      (byte) 7,
      (byte) 245,
      (byte) 17,
      (byte) 207,
      (byte) 223,
      (byte) 62,
      (byte) 83,
      (byte) 159,
      (byte) 198,
      (byte) 181,
      (byte) 163,
      (byte) 232,
      (byte) 145,
      (byte) 239,
      (byte) 137,
      (byte) 182,
      (byte) 97,
      (byte) 117,
      (byte) 168,
      (byte) 68,
      (byte) 139,
      (byte) 108,
      (byte) 236,
      (byte) 52,
      (byte) 88
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[14];
    numArray9[1] = (byte) 94;
    numArray9[7] = (byte) 104;
    numArray9[11] = (byte) 116;
    numArray9[3] = (byte) 0;
    numArray9[4] = (byte) 31 /*0x1F*/;
    numArray9[5] = (byte) 216;
    numArray9[6] = (byte) 59;
    numArray9[10] = (byte) 115;
    numArray9[8] = (byte) 197;
    numArray9[9] = (byte) 165;
    numArray9[2] = (byte) 1;
    numArray9[0] = (byte) 252;
    numArray9[12] = (byte) 111;
    numArray9[13] = (byte) 100;
    byte[] numArray10 = new byte[14]
    {
      (byte) 173,
      (byte) 219,
      (byte) 228,
      (byte) 12,
      (byte) 55,
      (byte) 131,
      (byte) 26,
      (byte) 128 /*0x80*/,
      (byte) 222,
      (byte) 65,
      (byte) 40,
      (byte) 242,
      (byte) 106,
      (byte) 9
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 14);
    for (int index = 0; index < 14; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_13641()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[42];
      byte[] numArray2 = new byte[42];
      numArray2[9] = (byte) 63 /*0x3F*/;
      numArray2[19] = (byte) 235;
      numArray2[24] = (byte) 186;
      numArray2[41] = (byte) 178;
      numArray2[12] = (byte) 25;
      numArray2[5] = (byte) 229;
      numArray2[6] = (byte) 34;
      numArray2[7] = (byte) 254;
      numArray2[4] = (byte) 237;
      numArray2[0] = (byte) 26;
      numArray2[10] = (byte) 89;
      numArray2[11] = (byte) 200;
      numArray2[18] = (byte) 126;
      numArray2[13] = (byte) 130;
      numArray2[14] = (byte) 77;
      numArray2[15] = (byte) 239;
      numArray2[29] = (byte) 209;
      numArray2[23] = (byte) 138;
      numArray2[17] = (byte) 66;
      numArray2[31 /*0x1F*/] = (byte) 211;
      numArray2[20] = (byte) 6;
      numArray2[26] = (byte) 241;
      numArray2[27] = (byte) 203;
      numArray2[39] = (byte) 46;
      numArray2[38] = (byte) 43;
      numArray2[3] = (byte) 151;
      numArray2[33] = (byte) 2;
      numArray2[30] = (byte) 118;
      numArray2[28] = (byte) 230;
      numArray2[25] = (byte) 32 /*0x20*/;
      numArray2[21] = (byte) 84;
      numArray2[16 /*0x10*/] = (byte) 250;
      numArray2[32 /*0x20*/] = (byte) 3;
      numArray2[8] = (byte) 152;
      numArray2[34] = (byte) 90;
      numArray2[35] = (byte) 166;
      numArray2[36] = (byte) 113;
      numArray2[37] = (byte) 252;
      numArray2[1] = (byte) 200;
      numArray2[2] = (byte) 243;
      numArray2[40] = (byte) 169;
      numArray2[22] = (byte) 97;
      byte[] numArray3 = new byte[42]
      {
        (byte) 90,
        (byte) 34,
        (byte) 172,
        (byte) 105,
        (byte) 154,
        (byte) 78,
        (byte) 125,
        (byte) 31 /*0x1F*/,
        (byte) 199,
        (byte) 74,
        (byte) 9,
        (byte) 138,
        (byte) 52,
        (byte) 147,
        (byte) 167,
        (byte) 154,
        (byte) 90,
        (byte) 139,
        (byte) 223,
        (byte) 70,
        (byte) 179,
        (byte) 7,
        (byte) 158,
        (byte) 233,
        (byte) 219,
        (byte) 68,
        (byte) 254,
        (byte) 8,
        (byte) 156,
        (byte) 17,
        (byte) 23,
        (byte) 88,
        (byte) 225,
        (byte) 88,
        (byte) 70,
        (byte) 160 /*0xA0*/,
        (byte) 248,
        (byte) 187,
        (byte) 86,
        (byte) 232,
        (byte) 29,
        (byte) 237
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 42);
      for (int index = 0; index < 42; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[42];
    byte[] numArray5 = new byte[42]
    {
      (byte) 76,
      (byte) 151,
      (byte) 147,
      (byte) 63 /*0x3F*/,
      (byte) 8,
      (byte) 31 /*0x1F*/,
      (byte) 94,
      (byte) 58,
      (byte) 82,
      (byte) 153,
      (byte) 86,
      (byte) 34,
      (byte) 144 /*0x90*/,
      (byte) 21,
      (byte) 176 /*0xB0*/,
      (byte) 100,
      (byte) 96 /*0x60*/,
      (byte) 132,
      (byte) 114,
      (byte) 151,
      (byte) 35,
      (byte) 116,
      (byte) 22,
      (byte) 143,
      (byte) 241,
      (byte) 245,
      (byte) 253,
      (byte) 116,
      (byte) 144 /*0x90*/,
      (byte) 147,
      (byte) 53,
      (byte) 102,
      (byte) 1,
      (byte) 39,
      (byte) 121,
      (byte) 127 /*0x7F*/,
      (byte) 149,
      (byte) 102,
      (byte) 117,
      (byte) 221,
      (byte) 184,
      (byte) 21
    };
    byte[] numArray6 = new byte[42]
    {
      (byte) 196,
      (byte) 139,
      (byte) 244,
      (byte) 8,
      (byte) 10,
      (byte) 74,
      (byte) 8,
      (byte) 198,
      (byte) 244,
      (byte) 230,
      (byte) 149,
      (byte) 3,
      (byte) 116,
      (byte) 193,
      (byte) 27,
      (byte) 117,
      (byte) 111,
      (byte) 75,
      (byte) 252,
      (byte) 215,
      (byte) 163,
      (byte) 9,
      (byte) 149,
      (byte) 133,
      (byte) 2,
      (byte) 148,
      (byte) 226,
      (byte) 104,
      (byte) 81,
      (byte) 218,
      (byte) 185,
      (byte) 46,
      (byte) 7,
      (byte) 143,
      (byte) 55,
      (byte) 49,
      (byte) 38,
      (byte) 107,
      (byte) 21,
      (byte) 191,
      (byte) 76,
      (byte) 57
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 42);
    for (int index = 0; index < 42; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13642()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[18];
      byte[] numArray2 = new byte[18];
      numArray2[10] = (byte) 129;
      numArray2[1] = (byte) 201;
      numArray2[2] = (byte) 167;
      numArray2[3] = byte.MaxValue;
      numArray2[9] = (byte) 216;
      numArray2[5] = (byte) 95;
      numArray2[6] = (byte) 238;
      numArray2[8] = (byte) 59;
      numArray2[16 /*0x10*/] = (byte) 169;
      numArray2[11] = (byte) 3;
      numArray2[7] = (byte) 47;
      numArray2[0] = (byte) 226;
      numArray2[12] = (byte) 78;
      numArray2[13] = (byte) 19;
      numArray2[14] = (byte) 33;
      numArray2[15] = (byte) 234;
      numArray2[4] = (byte) 99;
      numArray2[17] = (byte) 191;
      byte[] numArray3 = new byte[18]
      {
        (byte) 54,
        (byte) 9,
        (byte) 169,
        (byte) 30,
        (byte) 115,
        (byte) 226,
        (byte) 253,
        (byte) 139,
        (byte) 6,
        (byte) 76,
        (byte) 213,
        (byte) 4,
        (byte) 193,
        (byte) 46,
        (byte) 147,
        (byte) 118,
        (byte) 37,
        (byte) 210
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 18);
      for (int index = 0; index < 18; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[18];
    byte[] numArray5 = new byte[18]
    {
      (byte) 214,
      (byte) 119,
      (byte) 209,
      (byte) 235,
      (byte) 161,
      (byte) 254,
      (byte) 145,
      (byte) 23,
      (byte) 98,
      (byte) 188,
      (byte) 206,
      (byte) 108,
      (byte) 48 /*0x30*/,
      (byte) 85,
      (byte) 240 /*0xF0*/,
      (byte) 49,
      (byte) 120,
      (byte) 45
    };
    byte[] numArray6 = new byte[18]
    {
      (byte) 22,
      (byte) 174,
      (byte) 190,
      (byte) 139,
      (byte) 163,
      (byte) 249,
      (byte) 168,
      (byte) 110,
      (byte) 107,
      (byte) 175,
      (byte) 34,
      (byte) 147,
      (byte) 237,
      (byte) 177,
      (byte) 83,
      (byte) 250,
      (byte) 227,
      (byte) 11
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 18);
    for (int index = 0; index < 18; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[10];
    byte[] response = new byte[10];
    Array.Copy((Array) sc_13619.sspq, 225, (Array) numArray7, 0, 10);
    key.Query(true, 335, numArray7, response);
    Array.Copy((Array) sc_13619.sspr, 225, (Array) numArray7, 0, 10);
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

  internal static int ssp_appserver_13643(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 142,
      (byte) 119,
      (byte) 79,
      (byte) 126,
      (byte) 251,
      (byte) 49,
      (byte) 61,
      (byte) 226,
      (byte) 172,
      (byte) 216,
      (byte) 15,
      (byte) 161,
      (byte) 83,
      (byte) 6,
      (byte) 190,
      (byte) 109,
      (byte) 153,
      (byte) 6,
      (byte) 85,
      (byte) 96 /*0x60*/,
      (byte) 198,
      (byte) 134,
      (byte) 29,
      (byte) 128 /*0x80*/,
      (byte) 189,
      (byte) 77,
      (byte) 217,
      (byte) 61,
      (byte) 103,
      (byte) 97,
      (byte) 206,
      (byte) 177,
      (byte) 84,
      (byte) 161,
      (byte) 198,
      (byte) 120,
      (byte) 225,
      (byte) 250,
      (byte) 49,
      (byte) 69,
      (byte) 242,
      (byte) 133,
      (byte) 163,
      (byte) 129,
      (byte) 65,
      (byte) 140,
      (byte) 0,
      (byte) 30
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 165,
      (byte) 8,
      (byte) 40,
      (byte) 10,
      (byte) 46,
      (byte) 132,
      (byte) 22,
      (byte) 45,
      (byte) 143,
      (byte) 175,
      (byte) 91,
      (byte) 210,
      (byte) 149,
      (byte) 115,
      (byte) 153,
      (byte) 213,
      (byte) 206,
      (byte) 77,
      (byte) 92,
      (byte) 146,
      (byte) 186,
      (byte) 210,
      (byte) 21,
      (byte) 163,
      (byte) 18,
      (byte) 188,
      (byte) 198,
      (byte) 134,
      (byte) 30,
      (byte) 123,
      (byte) 157,
      (byte) 131,
      (byte) 48 /*0x30*/,
      (byte) 28,
      (byte) 133,
      (byte) 248,
      (byte) 117,
      (byte) 49,
      (byte) 20,
      (byte) 145,
      (byte) 212,
      (byte) 232,
      (byte) 184,
      (byte) 93,
      (byte) 69,
      (byte) 17,
      (byte) 246,
      (byte) 42
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_13644()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[39];
      byte[] numArray2 = new byte[39]
      {
        (byte) 200,
        (byte) 183,
        (byte) 200,
        (byte) 8,
        (byte) 89,
        (byte) 207,
        (byte) 193,
        (byte) 174,
        (byte) 87,
        (byte) 0,
        (byte) 123,
        (byte) 75,
        (byte) 10,
        (byte) 94,
        (byte) 76,
        (byte) 177,
        (byte) 63 /*0x3F*/,
        (byte) 70,
        (byte) 139,
        (byte) 159,
        (byte) 212,
        (byte) 15,
        (byte) 110,
        (byte) 64 /*0x40*/,
        (byte) 226,
        (byte) 156,
        (byte) 63 /*0x3F*/,
        (byte) 48 /*0x30*/,
        (byte) 76,
        (byte) 187,
        (byte) 168,
        (byte) 36,
        (byte) 250,
        (byte) 230,
        (byte) 233,
        (byte) 156,
        (byte) 216,
        (byte) 190,
        (byte) 31 /*0x1F*/
      };
      byte[] numArray3 = new byte[39]
      {
        (byte) 70,
        (byte) 204,
        (byte) 213,
        (byte) 106,
        (byte) 153,
        (byte) 219,
        (byte) 28,
        (byte) 21,
        (byte) 64 /*0x40*/,
        (byte) 109,
        (byte) 1,
        (byte) 122,
        (byte) 107,
        (byte) 117,
        (byte) 170,
        (byte) 172,
        byte.MaxValue,
        (byte) 250,
        (byte) 114,
        (byte) 4,
        (byte) 152,
        (byte) 42,
        (byte) 211,
        (byte) 172,
        (byte) 133,
        (byte) 101,
        (byte) 23,
        (byte) 48 /*0x30*/,
        (byte) 134,
        (byte) 217,
        (byte) 150,
        (byte) 36,
        (byte) 127 /*0x7F*/,
        (byte) 225,
        (byte) 13,
        (byte) 187,
        (byte) 206,
        (byte) 106,
        (byte) 37
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 39);
      for (int index = 0; index < 39; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[39];
    byte[] numArray5 = new byte[39];
    numArray5[3] = (byte) 87;
    numArray5[13] = (byte) 184;
    numArray5[2] = (byte) 10;
    numArray5[12] = (byte) 18;
    numArray5[16 /*0x10*/] = (byte) 37;
    numArray5[38] = (byte) 36;
    numArray5[6] = (byte) 63 /*0x3F*/;
    numArray5[37] = (byte) 139;
    numArray5[30] = (byte) 38;
    numArray5[9] = (byte) 78;
    numArray5[8] = (byte) 222;
    numArray5[11] = (byte) 200;
    numArray5[26] = (byte) 117;
    numArray5[23] = (byte) 87;
    numArray5[14] = (byte) 73;
    numArray5[15] = (byte) 135;
    numArray5[5] = (byte) 226;
    numArray5[17] = (byte) 189;
    numArray5[21] = (byte) 156;
    numArray5[0] = (byte) 237;
    numArray5[20] = (byte) 97;
    numArray5[18] = (byte) 143;
    numArray5[22] = (byte) 88;
    numArray5[34] = (byte) 34;
    numArray5[27] = (byte) 176 /*0xB0*/;
    numArray5[25] = (byte) 35;
    numArray5[29] = (byte) 129;
    numArray5[19] = (byte) 144 /*0x90*/;
    numArray5[28] = (byte) 216;
    numArray5[10] = (byte) 183;
    numArray5[4] = (byte) 119;
    numArray5[31 /*0x1F*/] = (byte) 121;
    numArray5[32 /*0x20*/] = (byte) 101;
    numArray5[33] = (byte) 81;
    numArray5[24] = (byte) 81;
    numArray5[35] = (byte) 124;
    numArray5[36] = (byte) 54;
    numArray5[7] = (byte) 78;
    numArray5[1] = (byte) 242;
    byte[] numArray6 = new byte[39]
    {
      (byte) 29,
      (byte) 179,
      (byte) 213,
      (byte) 172,
      (byte) 28,
      (byte) 169,
      (byte) 242,
      (byte) 34,
      (byte) 112 /*0x70*/,
      (byte) 16 /*0x10*/,
      (byte) 149,
      (byte) 70,
      (byte) 75,
      (byte) 148,
      (byte) 163,
      (byte) 221,
      (byte) 238,
      (byte) 58,
      (byte) 98,
      (byte) 95,
      (byte) 74,
      (byte) 21,
      (byte) 242,
      (byte) 166,
      (byte) 128 /*0x80*/,
      (byte) 173,
      (byte) 55,
      (byte) 118,
      (byte) 158,
      (byte) 252,
      (byte) 192 /*0xC0*/,
      (byte) 91,
      (byte) 202,
      (byte) 90,
      (byte) 206,
      (byte) 107,
      (byte) 227,
      (byte) 205,
      (byte) 116
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 39);
    for (int index = 0; index < 39; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13645()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[18];
      byte[] numArray2 = new byte[18];
      numArray2[3] = (byte) 197;
      numArray2[11] = (byte) 209;
      numArray2[2] = (byte) 232;
      numArray2[7] = (byte) 209;
      numArray2[16 /*0x10*/] = (byte) 49;
      numArray2[5] = (byte) 188;
      numArray2[10] = (byte) 132;
      numArray2[14] = (byte) 135;
      numArray2[8] = (byte) 226;
      numArray2[9] = (byte) 131;
      numArray2[4] = (byte) 247;
      numArray2[1] = (byte) 135;
      numArray2[12] = (byte) 239;
      numArray2[13] = (byte) 96 /*0x60*/;
      numArray2[6] = (byte) 47;
      numArray2[15] = (byte) 128 /*0x80*/;
      numArray2[0] = (byte) 92;
      numArray2[17] = (byte) 46;
      byte[] numArray3 = new byte[18];
      numArray3[16 /*0x10*/] = (byte) 230;
      numArray3[1] = (byte) 2;
      numArray3[2] = (byte) 23;
      numArray3[5] = (byte) 0;
      numArray3[7] = (byte) 46;
      numArray3[10] = (byte) 248;
      numArray3[12] = (byte) 72;
      numArray3[11] = (byte) 42;
      numArray3[8] = (byte) 143;
      numArray3[9] = (byte) 114;
      numArray3[3] = (byte) 220;
      numArray3[17] = (byte) 61;
      numArray3[0] = (byte) 159;
      numArray3[13] = (byte) 165;
      numArray3[14] = (byte) 141;
      numArray3[15] = (byte) 150;
      numArray3[6] = (byte) 63 /*0x3F*/;
      numArray3[4] = (byte) 155;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 18);
      for (int index = 0; index < 18; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[18];
    byte[] numArray5 = new byte[18];
    numArray5[9] = (byte) 205;
    numArray5[14] = (byte) 206;
    numArray5[2] = (byte) 167;
    numArray5[13] = (byte) 12;
    numArray5[4] = (byte) 114;
    numArray5[5] = (byte) 62;
    numArray5[6] = (byte) 70;
    numArray5[7] = (byte) 185;
    numArray5[17] = (byte) 127 /*0x7F*/;
    numArray5[0] = (byte) 234;
    numArray5[10] = (byte) 199;
    numArray5[16 /*0x10*/] = (byte) 42;
    numArray5[12] = (byte) 153;
    numArray5[11] = (byte) 46;
    numArray5[15] = (byte) 232;
    numArray5[8] = (byte) 157;
    numArray5[3] = (byte) 180;
    numArray5[1] = (byte) 252;
    byte[] numArray6 = new byte[18]
    {
      (byte) 211,
      (byte) 77,
      (byte) 134,
      (byte) 103,
      (byte) 142,
      (byte) 107,
      (byte) 146,
      (byte) 66,
      (byte) 5,
      (byte) 75,
      (byte) 37,
      (byte) 37,
      (byte) 77,
      (byte) 61,
      (byte) 49,
      (byte) 21,
      (byte) 197,
      (byte) 159
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 18);
    for (int index = 0; index < 18; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13646()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 163,
        (byte) 20,
        (byte) 119,
        (byte) 249,
        (byte) 123,
        (byte) 16 /*0x10*/,
        (byte) 67,
        (byte) 50,
        (byte) 254,
        (byte) 211
      };
      byte[] numArray3 = new byte[10]
      {
        (byte) 47,
        (byte) 151,
        (byte) 73,
        (byte) 165,
        (byte) 167,
        (byte) 7,
        (byte) 153,
        (byte) 178,
        (byte) 101,
        (byte) 175
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10];
    numArray5[0] = (byte) 59;
    numArray5[1] = (byte) 86;
    numArray5[4] = (byte) 154;
    numArray5[3] = (byte) 250;
    numArray5[2] = (byte) 1;
    numArray5[5] = (byte) 234;
    numArray5[6] = (byte) 145;
    numArray5[7] = (byte) 234;
    numArray5[8] = (byte) 120;
    numArray5[9] = (byte) 171;
    byte[] numArray6 = new byte[10];
    numArray6[6] = (byte) 236;
    numArray6[3] = (byte) 215;
    numArray6[2] = (byte) 52;
    numArray6[7] = (byte) 12;
    numArray6[4] = (byte) 93;
    numArray6[0] = (byte) 61;
    numArray6[1] = (byte) 97;
    numArray6[5] = (byte) 27;
    numArray6[8] = (byte) 43;
    numArray6[9] = (byte) 88;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[27];
    byte[] response = new byte[27];
    Array.Copy((Array) sc_13619.sspq, 235, (Array) numArray7, 0, 27);
    key.Query(true, 335, numArray7, response);
    Array.Copy((Array) sc_13619.sspr, 235, (Array) numArray7, 0, 27);
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

  internal static string ssp_appserver_13647()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[42];
      byte[] numArray2 = new byte[42]
      {
        (byte) 201,
        (byte) 169,
        (byte) 112 /*0x70*/,
        (byte) 147,
        (byte) 254,
        (byte) 160 /*0xA0*/,
        (byte) 177,
        (byte) 35,
        (byte) 249,
        (byte) 250,
        (byte) 67,
        (byte) 50,
        (byte) 209,
        (byte) 180,
        (byte) 204,
        (byte) 180,
        byte.MaxValue,
        (byte) 21,
        (byte) 95,
        (byte) 122,
        (byte) 140,
        (byte) 113,
        (byte) 55,
        (byte) 176 /*0xB0*/,
        (byte) 134,
        (byte) 171,
        (byte) 36,
        (byte) 221,
        (byte) 214,
        (byte) 248,
        (byte) 122,
        (byte) 251,
        (byte) 211,
        (byte) 153,
        (byte) 209,
        (byte) 129,
        (byte) 248,
        (byte) 222,
        (byte) 244,
        (byte) 145,
        (byte) 140,
        (byte) 236
      };
      byte[] numArray3 = new byte[42]
      {
        (byte) 103,
        (byte) 25,
        (byte) 120,
        (byte) 152,
        (byte) 41,
        (byte) 157,
        (byte) 247,
        (byte) 149,
        (byte) 251,
        (byte) 111,
        (byte) 105,
        (byte) 247,
        (byte) 101,
        (byte) 60,
        (byte) 221,
        (byte) 251,
        (byte) 225,
        (byte) 230,
        (byte) 42,
        (byte) 18,
        (byte) 240 /*0xF0*/,
        (byte) 5,
        (byte) 241,
        (byte) 121,
        (byte) 233,
        (byte) 115,
        (byte) 244,
        (byte) 226,
        (byte) 147,
        (byte) 143,
        (byte) 137,
        (byte) 80 /*0x50*/,
        (byte) 70,
        (byte) 190,
        (byte) 222,
        (byte) 79,
        (byte) 97,
        (byte) 217,
        (byte) 177,
        (byte) 204,
        (byte) 233,
        (byte) 249
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 42);
      for (int index = 0; index < 42; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[24];
      byte[] response = new byte[24];
      Array.Copy((Array) sc_13619.sspq, 262, (Array) numArray4, 0, 24);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_13619.sspr, 262, (Array) numArray4, 0, 24);
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
    byte[] numArray5 = new byte[42];
    byte[] numArray6 = new byte[42]
    {
      (byte) 188,
      (byte) 32 /*0x20*/,
      (byte) 32 /*0x20*/,
      (byte) 205,
      (byte) 237,
      (byte) 160 /*0xA0*/,
      (byte) 186,
      (byte) 155,
      (byte) 105,
      (byte) 203,
      (byte) 30,
      (byte) 83,
      (byte) 103,
      (byte) 82,
      (byte) 104,
      (byte) 158,
      (byte) 81,
      (byte) 27,
      (byte) 48 /*0x30*/,
      (byte) 121,
      (byte) 15,
      (byte) 56,
      (byte) 210,
      (byte) 161,
      (byte) 21,
      (byte) 184,
      (byte) 159,
      (byte) 181,
      (byte) 28,
      (byte) 254,
      (byte) 91,
      (byte) 84,
      (byte) 238,
      (byte) 43,
      (byte) 198,
      (byte) 111,
      (byte) 187,
      (byte) 220,
      (byte) 202,
      (byte) 200,
      (byte) 158,
      (byte) 214
    };
    byte[] numArray7 = new byte[42]
    {
      (byte) 44,
      (byte) 104,
      (byte) 180,
      (byte) 216,
      (byte) 101,
      (byte) 44,
      (byte) 120,
      (byte) 119,
      (byte) 163,
      (byte) 8,
      (byte) 52,
      (byte) 218,
      (byte) 70,
      (byte) 165,
      (byte) 211,
      (byte) 122,
      (byte) 222,
      (byte) 109,
      (byte) 112 /*0x70*/,
      (byte) 168,
      (byte) 191,
      (byte) 93,
      (byte) 119,
      (byte) 184,
      (byte) 171,
      (byte) 233,
      (byte) 84,
      (byte) 100,
      (byte) 239,
      (byte) 173,
      (byte) 225,
      (byte) 66,
      (byte) 67,
      (byte) 242,
      (byte) 32 /*0x20*/,
      (byte) 37,
      (byte) 124,
      (byte) 103,
      (byte) 118,
      (byte) 199,
      (byte) 243,
      (byte) 6
    };
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 42);
    for (int index = 0; index < 42; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_appserver_13648()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[18];
      byte[] numArray2 = new byte[18];
      numArray2[1] = (byte) 250;
      numArray2[6] = (byte) 7;
      numArray2[16 /*0x10*/] = (byte) 127 /*0x7F*/;
      numArray2[3] = (byte) 226;
      numArray2[4] = (byte) 247;
      numArray2[5] = (byte) 66;
      numArray2[12] = (byte) 184;
      numArray2[7] = (byte) 104;
      numArray2[13] = (byte) 52;
      numArray2[9] = (byte) 132;
      numArray2[10] = (byte) 78;
      numArray2[11] = (byte) 127 /*0x7F*/;
      numArray2[15] = (byte) 4;
      numArray2[14] = (byte) 0;
      numArray2[2] = (byte) 152;
      numArray2[8] = (byte) 59;
      numArray2[0] = (byte) 108;
      numArray2[17] = (byte) 107;
      byte[] numArray3 = new byte[18]
      {
        (byte) 109,
        (byte) 190,
        (byte) 17,
        (byte) 177,
        (byte) 83,
        (byte) 244,
        (byte) 114,
        (byte) 43,
        (byte) 32 /*0x20*/,
        (byte) 79,
        (byte) 208 /*0xD0*/,
        (byte) 4,
        (byte) 252,
        (byte) 71,
        (byte) 224 /*0xE0*/,
        (byte) 72,
        (byte) 61,
        (byte) 125
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 18);
      for (int index = 0; index < 18; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[18];
    byte[] numArray5 = new byte[18]
    {
      (byte) 25,
      (byte) 77,
      (byte) 134,
      (byte) 239,
      (byte) 65,
      (byte) 33,
      (byte) 60,
      (byte) 36,
      (byte) 149,
      (byte) 199,
      (byte) 153,
      (byte) 87,
      (byte) 235,
      (byte) 209,
      (byte) 106,
      (byte) 196,
      (byte) 49,
      (byte) 110
    };
    byte[] numArray6 = new byte[18]
    {
      (byte) 96 /*0x60*/,
      (byte) 97,
      (byte) 133,
      (byte) 118,
      (byte) 100,
      (byte) 14,
      (byte) 133,
      (byte) 170,
      (byte) 218,
      (byte) 253,
      (byte) 38,
      (byte) 215,
      (byte) 6,
      (byte) 134,
      (byte) 141,
      (byte) 230,
      (byte) 93,
      (byte) 208 /*0xD0*/
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 18);
    for (int index = 0; index < 18; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13649()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 158,
        (byte) 141,
        (byte) 3,
        (byte) 163,
        (byte) 189,
        (byte) 17,
        (byte) 203,
        (byte) 233,
        (byte) 91,
        (byte) 231
      };
      byte[] numArray3 = new byte[10]
      {
        (byte) 40,
        (byte) 131,
        (byte) 7,
        (byte) 121,
        (byte) 179,
        (byte) 188,
        (byte) 211,
        (byte) 125,
        (byte) 236,
        (byte) 29
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[30];
      byte[] response = new byte[30];
      Array.Copy((Array) sc_13619.sspq, 286, (Array) numArray4, 0, 30);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_13619.sspr, 286, (Array) numArray4, 0, 30);
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
      (byte) 18,
      (byte) 134,
      (byte) 122,
      (byte) 237,
      (byte) 230,
      (byte) 228,
      (byte) 36,
      (byte) 201,
      (byte) 90,
      (byte) 49
    };
    byte[] numArray7 = new byte[10]
    {
      (byte) 123,
      (byte) 69,
      (byte) 220,
      (byte) 118,
      (byte) 118,
      (byte) 176 /*0xB0*/,
      (byte) 214,
      (byte) 63 /*0x3F*/,
      (byte) 138,
      (byte) 29
    };
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_appserver_13650()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[49];
      byte[] numArray2 = new byte[49];
      numArray2[30] = (byte) 239;
      numArray2[26] = (byte) 19;
      numArray2[2] = (byte) 14;
      numArray2[3] = (byte) 163;
      numArray2[4] = (byte) 206;
      numArray2[0] = (byte) 172;
      numArray2[6] = (byte) 106;
      numArray2[1] = (byte) 254;
      numArray2[17] = (byte) 14;
      numArray2[15] = (byte) 123;
      numArray2[10] = (byte) 205;
      numArray2[23] = (byte) 21;
      numArray2[36] = (byte) 27;
      numArray2[13] = (byte) 171;
      numArray2[44] = (byte) 32 /*0x20*/;
      numArray2[38] = (byte) 221;
      numArray2[14] = (byte) 115;
      numArray2[7] = (byte) 12;
      numArray2[5] = (byte) 36;
      numArray2[19] = (byte) 144 /*0x90*/;
      numArray2[20] = (byte) 206;
      numArray2[21] = (byte) 111;
      numArray2[22] = (byte) 68;
      numArray2[32 /*0x20*/] = (byte) 173;
      numArray2[24] = (byte) 65;
      numArray2[8] = (byte) 223;
      numArray2[47] = (byte) 120;
      numArray2[27] = (byte) 179;
      numArray2[28] = (byte) 223;
      numArray2[29] = (byte) 83;
      numArray2[12] = (byte) 96 /*0x60*/;
      numArray2[11] = (byte) 229;
      numArray2[9] = (byte) 47;
      numArray2[18] = (byte) 52;
      numArray2[25] = (byte) 53;
      numArray2[39] = (byte) 72;
      numArray2[34] = (byte) 242;
      numArray2[37] = (byte) 181;
      numArray2[31 /*0x1F*/] = (byte) 176 /*0xB0*/;
      numArray2[43] = (byte) 74;
      numArray2[35] = (byte) 101;
      numArray2[41] = (byte) 6;
      numArray2[42] = (byte) 167;
      numArray2[45] = (byte) 1;
      numArray2[40] = (byte) 70;
      numArray2[16 /*0x10*/] = (byte) 247;
      numArray2[46] = (byte) 131;
      numArray2[33] = (byte) 200;
      numArray2[48 /*0x30*/] = (byte) 86;
      byte[] numArray3 = new byte[49]
      {
        (byte) 221,
        (byte) 57,
        (byte) 49,
        (byte) 196,
        (byte) 3,
        (byte) 60,
        (byte) 16 /*0x10*/,
        (byte) 91,
        (byte) 233,
        (byte) 170,
        (byte) 62,
        (byte) 212,
        (byte) 5,
        (byte) 67,
        (byte) 57,
        (byte) 175,
        (byte) 88,
        (byte) 143,
        (byte) 111,
        (byte) 75,
        (byte) 229,
        (byte) 89,
        (byte) 62,
        (byte) 135,
        (byte) 175,
        (byte) 40,
        byte.MaxValue,
        (byte) 239,
        (byte) 153,
        (byte) 156,
        (byte) 55,
        (byte) 247,
        (byte) 98,
        (byte) 121,
        (byte) 51,
        (byte) 107,
        (byte) 165,
        (byte) 102,
        (byte) 37,
        (byte) 213,
        (byte) 199,
        (byte) 144 /*0x90*/,
        (byte) 210,
        (byte) 85,
        (byte) 21,
        (byte) 77,
        (byte) 217,
        (byte) 71,
        byte.MaxValue
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 49);
      for (int index = 0; index < 49; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[49];
    byte[] numArray5 = new byte[49];
    numArray5[30] = (byte) 172;
    numArray5[12] = (byte) 34;
    numArray5[2] = (byte) 72;
    numArray5[21] = (byte) 35;
    numArray5[4] = (byte) 248;
    numArray5[9] = (byte) 116;
    numArray5[42] = (byte) 232;
    numArray5[32 /*0x20*/] = (byte) 241;
    numArray5[43] = (byte) 65;
    numArray5[18] = (byte) 121;
    numArray5[10] = (byte) 190;
    numArray5[39] = (byte) 205;
    numArray5[26] = (byte) 234;
    numArray5[13] = (byte) 230;
    numArray5[33] = (byte) 117;
    numArray5[15] = (byte) 89;
    numArray5[23] = (byte) 96 /*0x60*/;
    numArray5[17] = (byte) 114;
    numArray5[7] = (byte) 113;
    numArray5[19] = (byte) 54;
    numArray5[20] = (byte) 68;
    numArray5[37] = (byte) 128 /*0x80*/;
    numArray5[22] = (byte) 3;
    numArray5[29] = (byte) 239;
    numArray5[24] = (byte) 61;
    numArray5[11] = (byte) 4;
    numArray5[31 /*0x1F*/] = (byte) 240 /*0xF0*/;
    numArray5[27] = (byte) 180;
    numArray5[47] = (byte) 94;
    numArray5[35] = (byte) 218;
    numArray5[41] = (byte) 129;
    numArray5[5] = (byte) 249;
    numArray5[38] = (byte) 33;
    numArray5[45] = (byte) 29;
    numArray5[34] = (byte) 197;
    numArray5[16 /*0x10*/] = (byte) 44;
    numArray5[36] = (byte) 21;
    numArray5[0] = (byte) 236;
    numArray5[8] = (byte) 139;
    numArray5[48 /*0x30*/] = (byte) 193;
    numArray5[40] = (byte) 9;
    numArray5[1] = (byte) 191;
    numArray5[28] = (byte) 185;
    numArray5[3] = (byte) 127 /*0x7F*/;
    numArray5[44] = (byte) 237;
    numArray5[6] = (byte) 221;
    numArray5[46] = (byte) 50;
    numArray5[25] = (byte) 237;
    numArray5[14] = (byte) 118;
    byte[] numArray6 = new byte[49]
    {
      (byte) 199,
      (byte) 0,
      (byte) 32 /*0x20*/,
      (byte) 166,
      (byte) 164,
      (byte) 64 /*0x40*/,
      (byte) 214,
      (byte) 27,
      (byte) 83,
      (byte) 107,
      (byte) 81,
      (byte) 152,
      (byte) 217,
      (byte) 92,
      (byte) 225,
      (byte) 1,
      (byte) 219,
      (byte) 215,
      (byte) 249,
      (byte) 223,
      (byte) 95,
      (byte) 161,
      (byte) 71,
      (byte) 50,
      (byte) 129,
      (byte) 61,
      (byte) 140,
      (byte) 13,
      (byte) 38,
      (byte) 140,
      (byte) 221,
      (byte) 110,
      (byte) 233,
      (byte) 16 /*0x10*/,
      (byte) 244,
      (byte) 35,
      (byte) 161,
      (byte) 13,
      (byte) 126,
      (byte) 215,
      (byte) 243,
      (byte) 94,
      (byte) 228,
      (byte) 182,
      (byte) 235,
      (byte) 154,
      (byte) 11,
      (byte) 80 /*0x50*/,
      (byte) 189
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 49);
    for (int index = 0; index < 49; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[33];
    byte[] response = new byte[33];
    Array.Copy((Array) sc_13619.sspq, 316, (Array) numArray7, 0, 33);
    key.Query(true, 335, numArray7, response);
    Array.Copy((Array) sc_13619.sspr, 316, (Array) numArray7, 0, 33);
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

  internal static string ssp_appserver_13651()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[18];
      byte[] numArray2 = new byte[18];
      numArray2[14] = (byte) 171;
      numArray2[4] = (byte) 173;
      numArray2[11] = (byte) 28;
      numArray2[17] = (byte) 181;
      numArray2[1] = (byte) 206;
      numArray2[5] = (byte) 124;
      numArray2[6] = (byte) 133;
      numArray2[7] = (byte) 45;
      numArray2[8] = (byte) 110;
      numArray2[9] = (byte) 35;
      numArray2[0] = (byte) 79;
      numArray2[16 /*0x10*/] = (byte) 243;
      numArray2[12] = (byte) 200;
      numArray2[13] = (byte) 61;
      numArray2[10] = (byte) 183;
      numArray2[15] = (byte) 149;
      numArray2[2] = (byte) 143;
      numArray2[3] = (byte) 65;
      byte[] numArray3 = new byte[18]
      {
        (byte) 186,
        (byte) 144 /*0x90*/,
        (byte) 11,
        (byte) 51,
        (byte) 27,
        (byte) 9,
        (byte) 70,
        (byte) 88,
        (byte) 155,
        (byte) 64 /*0x40*/,
        (byte) 193,
        (byte) 34,
        (byte) 194,
        (byte) 97,
        (byte) 150,
        (byte) 244,
        (byte) 124,
        (byte) 125
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 18);
      for (int index = 0; index < 18; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[18];
    byte[] numArray5 = new byte[18];
    numArray5[10] = (byte) 99;
    numArray5[1] = (byte) 105;
    numArray5[2] = (byte) 142;
    numArray5[15] = (byte) 109;
    numArray5[3] = (byte) 182;
    numArray5[14] = (byte) 250;
    numArray5[6] = (byte) 81;
    numArray5[16 /*0x10*/] = (byte) 54;
    numArray5[5] = (byte) 95;
    numArray5[9] = (byte) 160 /*0xA0*/;
    numArray5[8] = (byte) 175;
    numArray5[11] = (byte) 250;
    numArray5[17] = (byte) 38;
    numArray5[13] = (byte) 162;
    numArray5[0] = (byte) 242;
    numArray5[7] = (byte) 114;
    numArray5[12] = (byte) 146;
    numArray5[4] = (byte) 90;
    byte[] numArray6 = new byte[18]
    {
      (byte) 6,
      (byte) 193,
      (byte) 170,
      (byte) 199,
      (byte) 30,
      (byte) 254,
      (byte) 44,
      (byte) 124,
      (byte) 166,
      (byte) 35,
      (byte) 191,
      (byte) 51,
      (byte) 151,
      (byte) 115,
      (byte) 189,
      (byte) 109,
      (byte) 186,
      (byte) 132
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 18);
    for (int index = 0; index < 18; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13652()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 65,
        (byte) 169,
        (byte) 70,
        (byte) 179,
        (byte) 102,
        (byte) 166,
        (byte) 31 /*0x1F*/,
        (byte) 127 /*0x7F*/,
        (byte) 17,
        (byte) 170
      };
      byte[] numArray3 = new byte[10]
      {
        (byte) 38,
        (byte) 195,
        (byte) 200,
        (byte) 156,
        (byte) 159,
        (byte) 163,
        (byte) 205,
        (byte) 210,
        (byte) 117,
        (byte) 126
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
      (byte) 104,
      (byte) 132,
      (byte) 108,
      (byte) 55,
      (byte) 42,
      (byte) 233,
      (byte) 233,
      (byte) 202,
      (byte) 65,
      (byte) 193
    };
    byte[] numArray6 = new byte[10];
    numArray6[1] = (byte) 221;
    numArray6[0] = (byte) 100;
    numArray6[2] = (byte) 99;
    numArray6[9] = (byte) 140;
    numArray6[8] = (byte) 33;
    numArray6[5] = (byte) 125;
    numArray6[6] = (byte) 200;
    numArray6[7] = (byte) 152;
    numArray6[4] = (byte) 70;
    numArray6[3] = (byte) 184;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static int ssp_appserver_13653(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[21] = (byte) 243;
    sourceArray1[1] = (byte) 37;
    sourceArray1[4] = byte.MaxValue;
    sourceArray1[31 /*0x1F*/] = (byte) 157;
    sourceArray1[5] = (byte) 5;
    sourceArray1[15] = (byte) 100;
    sourceArray1[6] = (byte) 15;
    sourceArray1[7] = (byte) 4;
    sourceArray1[33] = (byte) 186;
    sourceArray1[41] = (byte) 85;
    sourceArray1[10] = (byte) 27;
    sourceArray1[11] = (byte) 81;
    sourceArray1[12] = (byte) 122;
    sourceArray1[42] = (byte) 116;
    sourceArray1[39] = (byte) 188;
    sourceArray1[2] = (byte) 80 /*0x50*/;
    sourceArray1[23] = (byte) 45;
    sourceArray1[8] = (byte) 254;
    sourceArray1[18] = (byte) 232;
    sourceArray1[19] = (byte) 162;
    sourceArray1[0] = (byte) 253;
    sourceArray1[35] = (byte) 195;
    sourceArray1[22] = (byte) 135;
    sourceArray1[46] = (byte) 205;
    sourceArray1[32 /*0x20*/] = (byte) 44;
    sourceArray1[25] = (byte) 200;
    sourceArray1[26] = (byte) 120;
    sourceArray1[27] = (byte) 41;
    sourceArray1[24] = (byte) 154;
    sourceArray1[28] = (byte) 103;
    sourceArray1[30] = (byte) 251;
    sourceArray1[38] = (byte) 66;
    sourceArray1[47] = (byte) 35;
    sourceArray1[9] = (byte) 114;
    sourceArray1[34] = (byte) 45;
    sourceArray1[13] = (byte) 15;
    sourceArray1[36] = (byte) 152;
    sourceArray1[37] = (byte) 180;
    sourceArray1[17] = (byte) 0;
    sourceArray1[3] = (byte) 21;
    sourceArray1[40] = (byte) 148;
    sourceArray1[16 /*0x10*/] = (byte) 177;
    sourceArray1[20] = (byte) 88;
    sourceArray1[43] = (byte) 172;
    sourceArray1[44] = (byte) 215;
    sourceArray1[45] = (byte) 146;
    sourceArray1[29] = (byte) 185;
    sourceArray1[14] = (byte) 121;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[24] = (byte) 47;
    sourceArray2[12] = (byte) 196;
    sourceArray2[13] = (byte) 127 /*0x7F*/;
    sourceArray2[4] = (byte) 254;
    sourceArray2[44] = (byte) 174;
    sourceArray2[15] = (byte) 230;
    sourceArray2[35] = (byte) 79;
    sourceArray2[36] = (byte) 88;
    sourceArray2[1] = (byte) 244;
    sourceArray2[7] = (byte) 236;
    sourceArray2[6] = (byte) 106;
    sourceArray2[2] = (byte) 9;
    sourceArray2[5] = (byte) 142;
    sourceArray2[31 /*0x1F*/] = (byte) 82;
    sourceArray2[14] = (byte) 222;
    sourceArray2[11] = (byte) 228;
    sourceArray2[20] = (byte) 10;
    sourceArray2[17] = (byte) 51;
    sourceArray2[42] = (byte) 143;
    sourceArray2[19] = (byte) 19;
    sourceArray2[22] = (byte) 52;
    sourceArray2[10] = (byte) 182;
    sourceArray2[40] = (byte) 206;
    sourceArray2[9] = (byte) 44;
    sourceArray2[8] = (byte) 182;
    sourceArray2[25] = (byte) 169;
    sourceArray2[26] = (byte) 236;
    sourceArray2[27] = (byte) 119;
    sourceArray2[16 /*0x10*/] = (byte) 95;
    sourceArray2[21] = (byte) 188;
    sourceArray2[30] = (byte) 8;
    sourceArray2[18] = (byte) 148;
    sourceArray2[3] = (byte) 215;
    sourceArray2[33] = (byte) 42;
    sourceArray2[34] = (byte) 93;
    sourceArray2[28] = (byte) 217;
    sourceArray2[29] = (byte) 113;
    sourceArray2[37] = (byte) 249;
    sourceArray2[38] = (byte) 69;
    sourceArray2[39] = (byte) 64 /*0x40*/;
    sourceArray2[23] = (byte) 14;
    sourceArray2[41] = (byte) 7;
    sourceArray2[0] = (byte) 110;
    sourceArray2[43] = (byte) 47;
    sourceArray2[32 /*0x20*/] = (byte) 60;
    sourceArray2[45] = (byte) 190;
    sourceArray2[46] = (byte) 218;
    sourceArray2[47] = (byte) 136;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_13654()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[45];
      byte[] numArray2 = new byte[45]
      {
        (byte) 208 /*0xD0*/,
        (byte) 245,
        (byte) 152,
        (byte) 15,
        (byte) 28,
        (byte) 77,
        (byte) 121,
        (byte) 213,
        (byte) 227,
        (byte) 129,
        (byte) 18,
        (byte) 80 /*0x50*/,
        (byte) 197,
        (byte) 251,
        (byte) 214,
        (byte) 49,
        (byte) 215,
        (byte) 59,
        (byte) 28,
        (byte) 209,
        (byte) 215,
        (byte) 77,
        (byte) 37,
        (byte) 232,
        (byte) 93,
        (byte) 103,
        (byte) 6,
        (byte) 22,
        (byte) 153,
        (byte) 182,
        (byte) 41,
        (byte) 45,
        (byte) 176 /*0xB0*/,
        (byte) 252,
        (byte) 225,
        (byte) 7,
        (byte) 198,
        (byte) 58,
        (byte) 51,
        (byte) 88,
        (byte) 131,
        (byte) 197,
        (byte) 10,
        (byte) 146,
        (byte) 27
      };
      byte[] numArray3 = new byte[45];
      numArray3[29] = (byte) 226;
      numArray3[1] = (byte) 23;
      numArray3[32 /*0x20*/] = (byte) 143;
      numArray3[20] = (byte) 156;
      numArray3[3] = (byte) 101;
      numArray3[15] = (byte) 244;
      numArray3[4] = (byte) 154;
      numArray3[10] = (byte) 33;
      numArray3[8] = (byte) 195;
      numArray3[44] = (byte) 94;
      numArray3[27] = (byte) 192 /*0xC0*/;
      numArray3[33] = (byte) 88;
      numArray3[39] = (byte) 209;
      numArray3[2] = (byte) 251;
      numArray3[0] = (byte) 39;
      numArray3[26] = (byte) 109;
      numArray3[18] = (byte) 71;
      numArray3[17] = (byte) 66;
      numArray3[5] = (byte) 71;
      numArray3[9] = (byte) 21;
      numArray3[21] = (byte) 73;
      numArray3[34] = (byte) 11;
      numArray3[22] = (byte) 176 /*0xB0*/;
      numArray3[23] = (byte) 28;
      numArray3[24] = (byte) 45;
      numArray3[6] = (byte) 181;
      numArray3[11] = (byte) 98;
      numArray3[41] = (byte) 90;
      numArray3[28] = (byte) 250;
      numArray3[13] = (byte) 150;
      numArray3[12] = (byte) 246;
      numArray3[14] = (byte) 122;
      numArray3[25] = (byte) 29;
      numArray3[30] = (byte) 154;
      numArray3[31 /*0x1F*/] = (byte) 35;
      numArray3[35] = (byte) 251;
      numArray3[36] = (byte) 220;
      numArray3[37] = (byte) 119;
      numArray3[38] = (byte) 209;
      numArray3[7] = (byte) 229;
      numArray3[40] = (byte) 42;
      numArray3[19] = (byte) 101;
      numArray3[42] = (byte) 92;
      numArray3[43] = (byte) 62;
      numArray3[16 /*0x10*/] = (byte) 129;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 45);
      for (int index = 0; index < 45; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[45];
    byte[] numArray5 = new byte[45]
    {
      (byte) 126,
      (byte) 17,
      (byte) 32 /*0x20*/,
      (byte) 137,
      (byte) 221,
      (byte) 58,
      (byte) 54,
      (byte) 91,
      (byte) 28,
      (byte) 202,
      (byte) 37,
      (byte) 98,
      (byte) 0,
      (byte) 82,
      (byte) 131,
      (byte) 136,
      (byte) 221,
      (byte) 63 /*0x3F*/,
      (byte) 61,
      (byte) 226,
      (byte) 80 /*0x50*/,
      (byte) 49,
      (byte) 37,
      (byte) 65,
      (byte) 112 /*0x70*/,
      (byte) 19,
      (byte) 138,
      byte.MaxValue,
      (byte) 198,
      (byte) 186,
      (byte) 188,
      (byte) 151,
      (byte) 82,
      (byte) 20,
      (byte) 137,
      (byte) 178,
      (byte) 45,
      (byte) 136,
      (byte) 101,
      (byte) 88,
      (byte) 150,
      (byte) 6,
      (byte) 102,
      (byte) 86,
      (byte) 252
    };
    byte[] numArray6 = new byte[45];
    numArray6[23] = (byte) 197;
    numArray6[17] = (byte) 5;
    numArray6[25] = (byte) 10;
    numArray6[21] = (byte) 168;
    numArray6[27] = (byte) 42;
    numArray6[5] = (byte) 169;
    numArray6[8] = (byte) 208 /*0xD0*/;
    numArray6[7] = (byte) 99;
    numArray6[30] = (byte) 232;
    numArray6[9] = (byte) 82;
    numArray6[6] = (byte) 92;
    numArray6[11] = (byte) 48 /*0x30*/;
    numArray6[0] = (byte) 178;
    numArray6[13] = (byte) 122;
    numArray6[14] = (byte) 38;
    numArray6[12] = (byte) 74;
    numArray6[37] = (byte) 106;
    numArray6[34] = (byte) 187;
    numArray6[36] = (byte) 81;
    numArray6[19] = (byte) 24;
    numArray6[20] = (byte) 80 /*0x50*/;
    numArray6[1] = (byte) 32 /*0x20*/;
    numArray6[22] = (byte) 196;
    numArray6[16 /*0x10*/] = (byte) 231;
    numArray6[2] = (byte) 229;
    numArray6[15] = (byte) 134;
    numArray6[26] = (byte) 124;
    numArray6[3] = (byte) 98;
    numArray6[28] = (byte) 145;
    numArray6[29] = (byte) 240 /*0xF0*/;
    numArray6[40] = (byte) 178;
    numArray6[31 /*0x1F*/] = (byte) 127 /*0x7F*/;
    numArray6[32 /*0x20*/] = (byte) 207;
    numArray6[33] = (byte) 212;
    numArray6[4] = (byte) 81;
    numArray6[38] = (byte) 140;
    numArray6[35] = (byte) 153;
    numArray6[43] = (byte) 231;
    numArray6[18] = (byte) 104;
    numArray6[39] = (byte) 137;
    numArray6[44] = (byte) 6;
    numArray6[41] = (byte) 56;
    numArray6[42] = (byte) 178;
    numArray6[10] = (byte) 142;
    numArray6[24] = (byte) 186;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 45);
    for (int index = 0; index < 45; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13655()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[18];
      byte[] numArray2 = new byte[18]
      {
        (byte) 168,
        (byte) 94,
        (byte) 113,
        (byte) 5,
        (byte) 72,
        (byte) 110,
        (byte) 89,
        (byte) 45,
        (byte) 178,
        (byte) 144 /*0x90*/,
        (byte) 247,
        (byte) 218,
        (byte) 68,
        (byte) 21,
        (byte) 232,
        (byte) 238,
        (byte) 130,
        (byte) 28
      };
      byte[] numArray3 = new byte[18];
      numArray3[15] = (byte) 85;
      numArray3[13] = (byte) 109;
      numArray3[2] = (byte) 91;
      numArray3[7] = (byte) 239;
      numArray3[4] = (byte) 207;
      numArray3[3] = (byte) 137;
      numArray3[6] = (byte) 38;
      numArray3[5] = (byte) 216;
      numArray3[8] = (byte) 24;
      numArray3[1] = (byte) 62;
      numArray3[10] = (byte) 94;
      numArray3[11] = (byte) 73;
      numArray3[12] = (byte) 67;
      numArray3[9] = (byte) 192 /*0xC0*/;
      numArray3[14] = (byte) 26;
      numArray3[0] = (byte) 122;
      numArray3[16 /*0x10*/] = (byte) 222;
      numArray3[17] = (byte) 241;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 18);
      for (int index = 0; index < 18; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[18];
    byte[] numArray5 = new byte[18];
    numArray5[8] = (byte) 136;
    numArray5[3] = (byte) 92;
    numArray5[2] = (byte) 203;
    numArray5[13] = (byte) 180;
    numArray5[6] = (byte) 177;
    numArray5[4] = (byte) 12;
    numArray5[0] = (byte) 214;
    numArray5[16 /*0x10*/] = (byte) 7;
    numArray5[1] = (byte) 172;
    numArray5[5] = (byte) 124;
    numArray5[10] = (byte) 237;
    numArray5[11] = (byte) 185;
    numArray5[12] = (byte) 171;
    numArray5[17] = (byte) 196;
    numArray5[7] = (byte) 88;
    numArray5[15] = (byte) 202;
    numArray5[9] = (byte) 53;
    numArray5[14] = (byte) 93;
    byte[] numArray6 = new byte[18]
    {
      (byte) 177,
      (byte) 15,
      (byte) 118,
      (byte) 175,
      (byte) 144 /*0x90*/,
      (byte) 8,
      (byte) 156,
      (byte) 96 /*0x60*/,
      (byte) 233,
      (byte) 164,
      (byte) 38,
      (byte) 221,
      (byte) 102,
      (byte) 227,
      (byte) 0,
      (byte) 41,
      (byte) 27,
      (byte) 37
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 18);
    for (int index = 0; index < 18; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13656()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10];
      numArray2[2] = (byte) 104;
      numArray2[1] = (byte) 24;
      numArray2[7] = (byte) 87;
      numArray2[3] = (byte) 188;
      numArray2[9] = (byte) 116;
      numArray2[5] = (byte) 210;
      numArray2[6] = (byte) 94;
      numArray2[0] = (byte) 37;
      numArray2[4] = (byte) 34;
      numArray2[8] = (byte) 136;
      byte[] numArray3 = new byte[10]
      {
        (byte) 158,
        (byte) 39,
        (byte) 26,
        (byte) 52,
        (byte) 14,
        (byte) 33,
        (byte) 196,
        (byte) 171,
        (byte) 211,
        (byte) 44
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[23];
      byte[] response = new byte[23];
      Array.Copy((Array) sc_13619.sspq, 349, (Array) numArray4, 0, 23);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_13619.sspr, 349, (Array) numArray4, 0, 23);
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
    numArray6[4] = (byte) 87;
    numArray6[1] = (byte) 229;
    numArray6[2] = (byte) 107;
    numArray6[7] = (byte) 18;
    numArray6[0] = (byte) 39;
    numArray6[5] = (byte) 13;
    numArray6[6] = (byte) 9;
    numArray6[8] = (byte) 103;
    numArray6[3] = (byte) 32 /*0x20*/;
    numArray6[9] = (byte) 198;
    byte[] numArray7 = new byte[10]
    {
      (byte) 8,
      (byte) 103,
      (byte) 63 /*0x3F*/,
      (byte) 14,
      (byte) 244,
      (byte) 132,
      (byte) 64 /*0x40*/,
      (byte) 246,
      (byte) 176 /*0xB0*/,
      (byte) 119
    };
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_appserver_13657()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 151,
        (byte) 178,
        (byte) 227,
        (byte) 92,
        (byte) 253,
        (byte) 82,
        (byte) 191,
        (byte) 7,
        (byte) 102,
        (byte) 69
      };
      byte[] numArray3 = new byte[10];
      numArray3[6] = (byte) 15;
      numArray3[2] = (byte) 190;
      numArray3[0] = (byte) 21;
      numArray3[3] = (byte) 248;
      numArray3[4] = (byte) 226;
      numArray3[1] = (byte) 203;
      numArray3[9] = (byte) 217;
      numArray3[5] = (byte) 202;
      numArray3[8] = (byte) 177;
      numArray3[7] = (byte) 121;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10]
    {
      (byte) 125,
      (byte) 223,
      (byte) 44,
      (byte) 218,
      (byte) 226,
      (byte) 54,
      (byte) 225,
      (byte) 182,
      (byte) 156,
      (byte) 244
    };
    byte[] numArray6 = new byte[10]
    {
      (byte) 208 /*0xD0*/,
      (byte) 119,
      (byte) 93,
      (byte) 97,
      (byte) 179,
      (byte) 104,
      (byte) 26,
      (byte) 137,
      (byte) 62,
      (byte) 175
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13658()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[63 /*0x3F*/];
      byte[] numArray2 = new byte[55]
      {
        (byte) 172,
        (byte) 20,
        (byte) 21,
        (byte) 6,
        (byte) 177,
        (byte) 27,
        (byte) 7,
        (byte) 1,
        (byte) 24,
        (byte) 225,
        (byte) 184,
        (byte) 66,
        (byte) 186,
        (byte) 196,
        (byte) 64 /*0x40*/,
        (byte) 232,
        (byte) 249,
        (byte) 229,
        (byte) 109,
        (byte) 4,
        (byte) 102,
        (byte) 124,
        (byte) 209,
        (byte) 119,
        (byte) 220,
        (byte) 209,
        (byte) 133,
        (byte) 142,
        (byte) 55,
        (byte) 59,
        (byte) 91,
        (byte) 79,
        (byte) 199,
        (byte) 139,
        (byte) 128 /*0x80*/,
        (byte) 132,
        (byte) 141,
        (byte) 240 /*0xF0*/,
        (byte) 170,
        (byte) 14,
        (byte) 166,
        (byte) 123,
        (byte) 199,
        (byte) 74,
        (byte) 34,
        (byte) 160 /*0xA0*/,
        (byte) 123,
        (byte) 28,
        (byte) 117,
        (byte) 180,
        (byte) 251,
        (byte) 67,
        (byte) 26,
        (byte) 161,
        (byte) 195
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 180,
        (byte) 158,
        (byte) 158,
        (byte) 142,
        (byte) 145,
        (byte) 20,
        (byte) 26,
        (byte) 0,
        (byte) 3,
        (byte) 71,
        (byte) 9,
        (byte) 75,
        (byte) 246,
        (byte) 151,
        (byte) 96 /*0x60*/,
        (byte) 241,
        (byte) 160 /*0xA0*/,
        (byte) 11,
        (byte) 143,
        (byte) 106,
        (byte) 92,
        (byte) 192 /*0xC0*/,
        (byte) 16 /*0x10*/,
        (byte) 146,
        (byte) 118,
        (byte) 83,
        (byte) 86,
        (byte) 165,
        (byte) 233,
        (byte) 61,
        (byte) 199,
        (byte) 141,
        (byte) 141,
        (byte) 228,
        (byte) 22,
        (byte) 102,
        (byte) 73,
        (byte) 118,
        (byte) 141,
        (byte) 98,
        (byte) 111,
        (byte) 151,
        (byte) 93,
        (byte) 128 /*0x80*/,
        (byte) 182,
        (byte) 91,
        (byte) 105,
        (byte) 179,
        (byte) 149,
        (byte) 246,
        (byte) 41,
        (byte) 134,
        (byte) 221,
        (byte) 248,
        (byte) 110
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[8];
      numArray4[4] = (byte) 120;
      numArray4[3] = (byte) 150;
      numArray4[2] = (byte) 223;
      numArray4[0] = (byte) 157;
      numArray4[7] = (byte) 134;
      numArray4[5] = (byte) 201;
      numArray4[6] = (byte) 221;
      numArray4[1] = (byte) 42;
      byte[] numArray5 = new byte[8]
      {
        (byte) 109,
        (byte) 245,
        (byte) 100,
        (byte) 7,
        (byte) 2,
        (byte) 206,
        (byte) 237,
        (byte) 239
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 8);
      for (int index = 0; index < 8; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[63 /*0x3F*/];
    byte[] numArray7 = new byte[55]
    {
      (byte) 181,
      (byte) 134,
      (byte) 85,
      (byte) 60,
      (byte) 86,
      (byte) 104,
      (byte) 25,
      (byte) 131,
      (byte) 63 /*0x3F*/,
      (byte) 24,
      (byte) 52,
      (byte) 38,
      (byte) 200,
      (byte) 79,
      (byte) 64 /*0x40*/,
      (byte) 240 /*0xF0*/,
      (byte) 198,
      (byte) 202,
      (byte) 119,
      (byte) 46,
      (byte) 177,
      (byte) 233,
      (byte) 196,
      (byte) 185,
      (byte) 37,
      (byte) 76,
      (byte) 215,
      (byte) 185,
      (byte) 89,
      (byte) 243,
      (byte) 7,
      (byte) 187,
      (byte) 67,
      (byte) 216,
      (byte) 122,
      (byte) 188,
      (byte) 71,
      (byte) 237,
      (byte) 137,
      (byte) 46,
      (byte) 95,
      (byte) 69,
      (byte) 157,
      (byte) 109,
      (byte) 52,
      (byte) 83,
      (byte) 102,
      (byte) 244,
      (byte) 123,
      (byte) 106,
      (byte) 65,
      (byte) 112 /*0x70*/,
      (byte) 232,
      (byte) 46,
      (byte) 57
    };
    byte[] numArray8 = new byte[55]
    {
      (byte) 124,
      (byte) 226,
      (byte) 236,
      (byte) 233,
      (byte) 19,
      (byte) 87,
      (byte) 25,
      (byte) 45,
      (byte) 190,
      (byte) 159,
      (byte) 182,
      (byte) 101,
      (byte) 121,
      (byte) 172,
      (byte) 18,
      (byte) 160 /*0xA0*/,
      (byte) 132,
      (byte) 205,
      (byte) 197,
      (byte) 109,
      (byte) 112 /*0x70*/,
      (byte) 48 /*0x30*/,
      (byte) 142,
      (byte) 11,
      (byte) 160 /*0xA0*/,
      (byte) 166,
      (byte) 40,
      (byte) 60,
      (byte) 80 /*0x50*/,
      (byte) 117,
      (byte) 242,
      (byte) 86,
      (byte) 88,
      (byte) 232,
      (byte) 173,
      (byte) 85,
      (byte) 55,
      (byte) 113,
      (byte) 204,
      (byte) 226,
      (byte) 30,
      (byte) 86,
      (byte) 47,
      (byte) 212,
      (byte) 176 /*0xB0*/,
      (byte) 116,
      (byte) 3,
      (byte) 63 /*0x3F*/,
      (byte) 23,
      (byte) 171,
      (byte) 236,
      (byte) 117,
      (byte) 195,
      (byte) 174,
      (byte) 222
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[8]
    {
      (byte) 156,
      (byte) 22,
      (byte) 10,
      (byte) 144 /*0x90*/,
      (byte) 96 /*0x60*/,
      (byte) 42,
      (byte) 76,
      (byte) 16 /*0x10*/
    };
    byte[] numArray10 = new byte[8];
    numArray10[2] = (byte) 97;
    numArray10[7] = (byte) 240 /*0xF0*/;
    numArray10[0] = (byte) 18;
    numArray10[3] = (byte) 210;
    numArray10[4] = (byte) 4;
    numArray10[5] = (byte) 242;
    numArray10[6] = (byte) 216;
    numArray10[1] = (byte) 134;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 8);
    for (int index = 0; index < 8; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_13659()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[141];
      byte[] numArray2 = new byte[55];
      numArray2[2] = (byte) 210;
      numArray2[19] = (byte) 27;
      numArray2[23] = (byte) 112 /*0x70*/;
      numArray2[3] = (byte) 229;
      numArray2[27] = (byte) 193;
      numArray2[5] = (byte) 62;
      numArray2[6] = (byte) 238;
      numArray2[51] = (byte) 171;
      numArray2[8] = (byte) 150;
      numArray2[9] = (byte) 210;
      numArray2[11] = (byte) 159;
      numArray2[44] = (byte) 206;
      numArray2[12] = (byte) 174;
      numArray2[13] = (byte) 250;
      numArray2[52] = (byte) 208 /*0xD0*/;
      numArray2[16 /*0x10*/] = (byte) 127 /*0x7F*/;
      numArray2[0] = (byte) 86;
      numArray2[29] = (byte) 47;
      numArray2[18] = byte.MaxValue;
      numArray2[4] = (byte) 227;
      numArray2[20] = (byte) 81;
      numArray2[53] = (byte) 80 /*0x50*/;
      numArray2[22] = (byte) 186;
      numArray2[25] = (byte) 31 /*0x1F*/;
      numArray2[48 /*0x30*/] = (byte) 253;
      numArray2[15] = (byte) 215;
      numArray2[10] = (byte) 20;
      numArray2[33] = (byte) 128 /*0x80*/;
      numArray2[28] = (byte) 104;
      numArray2[26] = (byte) 246;
      numArray2[7] = (byte) 177;
      numArray2[31 /*0x1F*/] = (byte) 95;
      numArray2[24] = (byte) 118;
      numArray2[30] = (byte) 159;
      numArray2[14] = (byte) 171;
      numArray2[1] = (byte) 133;
      numArray2[36] = (byte) 68;
      numArray2[42] = (byte) 121;
      numArray2[38] = (byte) 173;
      numArray2[17] = (byte) 157;
      numArray2[40] = (byte) 59;
      numArray2[41] = (byte) 154;
      numArray2[49] = (byte) 1;
      numArray2[45] = (byte) 80 /*0x50*/;
      numArray2[54] = (byte) 249;
      numArray2[32 /*0x20*/] = (byte) 205;
      numArray2[46] = (byte) 38;
      numArray2[47] = (byte) 138;
      numArray2[43] = (byte) 166;
      numArray2[21] = (byte) 92;
      numArray2[50] = (byte) 194;
      numArray2[34] = (byte) 107;
      numArray2[37] = (byte) 36;
      numArray2[39] = (byte) 34;
      numArray2[35] = (byte) 23;
      byte[] numArray3 = new byte[55]
      {
        (byte) 132,
        (byte) 234,
        (byte) 146,
        (byte) 164,
        (byte) 246,
        (byte) 95,
        (byte) 14,
        (byte) 248,
        (byte) 141,
        (byte) 156,
        (byte) 220,
        (byte) 219,
        (byte) 198,
        (byte) 10,
        (byte) 72,
        (byte) 175,
        (byte) 57,
        (byte) 201,
        (byte) 162,
        (byte) 247,
        (byte) 23,
        (byte) 221,
        (byte) 124,
        (byte) 24,
        (byte) 91,
        (byte) 64 /*0x40*/,
        (byte) 110,
        (byte) 194,
        (byte) 205,
        (byte) 184,
        (byte) 80 /*0x50*/,
        (byte) 189,
        (byte) 82,
        (byte) 198,
        (byte) 119,
        (byte) 182,
        (byte) 235,
        (byte) 212,
        (byte) 89,
        (byte) 198,
        (byte) 182,
        (byte) 16 /*0x10*/,
        (byte) 130,
        (byte) 221,
        (byte) 135,
        (byte) 28,
        (byte) 171,
        (byte) 64 /*0x40*/,
        (byte) 14,
        (byte) 22,
        (byte) 3,
        (byte) 212,
        (byte) 31 /*0x1F*/,
        (byte) 109,
        (byte) 224 /*0xE0*/
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 190,
        (byte) 73,
        (byte) 202,
        (byte) 218,
        (byte) 105,
        (byte) 226,
        (byte) 249,
        (byte) 63 /*0x3F*/,
        (byte) 37,
        (byte) 85,
        (byte) 167,
        (byte) 28,
        (byte) 70,
        (byte) 176 /*0xB0*/,
        (byte) 165,
        (byte) 5,
        (byte) 2,
        (byte) 208 /*0xD0*/,
        (byte) 102,
        (byte) 208 /*0xD0*/,
        (byte) 160 /*0xA0*/,
        (byte) 137,
        (byte) 166,
        (byte) 1,
        (byte) 113,
        (byte) 89,
        (byte) 248,
        (byte) 6,
        (byte) 55,
        (byte) 156,
        (byte) 10,
        (byte) 68,
        (byte) 90,
        (byte) 150,
        (byte) 35,
        (byte) 108,
        (byte) 32 /*0x20*/,
        (byte) 0,
        (byte) 165,
        (byte) 110,
        (byte) 25,
        (byte) 51,
        (byte) 243,
        (byte) 41,
        (byte) 80 /*0x50*/,
        (byte) 222,
        (byte) 35,
        (byte) 189,
        (byte) 194,
        (byte) 182,
        (byte) 240 /*0xF0*/,
        (byte) 161,
        (byte) 219,
        (byte) 71,
        (byte) 21
      };
      byte[] numArray5 = new byte[55]
      {
        (byte) 221,
        (byte) 141,
        (byte) 143,
        (byte) 14,
        (byte) 179,
        (byte) 215,
        (byte) 89,
        (byte) 135,
        (byte) 250,
        (byte) 254,
        (byte) 161,
        (byte) 195,
        (byte) 126,
        (byte) 217,
        (byte) 121,
        (byte) 209,
        (byte) 216,
        (byte) 207,
        (byte) 178,
        (byte) 106,
        (byte) 207,
        (byte) 202,
        (byte) 80 /*0x50*/,
        (byte) 130,
        (byte) 92,
        (byte) 253,
        (byte) 245,
        (byte) 166,
        (byte) 25,
        (byte) 108,
        (byte) 116,
        (byte) 91,
        (byte) 104,
        (byte) 66,
        (byte) 153,
        (byte) 159,
        (byte) 80 /*0x50*/,
        (byte) 212,
        (byte) 23,
        (byte) 233,
        (byte) 160 /*0xA0*/,
        (byte) 238,
        (byte) 25,
        (byte) 153,
        (byte) 6,
        (byte) 36,
        (byte) 119,
        (byte) 190,
        (byte) 237,
        (byte) 20,
        (byte) 197,
        (byte) 43,
        (byte) 158,
        (byte) 71,
        (byte) 1
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[31 /*0x1F*/];
      numArray6[2] = (byte) 15;
      numArray6[9] = (byte) 194;
      numArray6[23] = (byte) 54;
      numArray6[4] = (byte) 243;
      numArray6[12] = (byte) 56;
      numArray6[3] = (byte) 234;
      numArray6[30] = (byte) 19;
      numArray6[7] = (byte) 74;
      numArray6[28] = (byte) 105;
      numArray6[20] = (byte) 125;
      numArray6[10] = (byte) 176 /*0xB0*/;
      numArray6[0] = (byte) 200;
      numArray6[15] = (byte) 43;
      numArray6[13] = (byte) 229;
      numArray6[6] = (byte) 136;
      numArray6[19] = (byte) 149;
      numArray6[16 /*0x10*/] = (byte) 248;
      numArray6[17] = (byte) 182;
      numArray6[18] = (byte) 108;
      numArray6[14] = (byte) 1;
      numArray6[21] = (byte) 176 /*0xB0*/;
      numArray6[24] = (byte) 58;
      numArray6[22] = (byte) 147;
      numArray6[25] = (byte) 67;
      numArray6[1] = (byte) 104;
      numArray6[11] = (byte) 140;
      numArray6[26] = (byte) 129;
      numArray6[27] = (byte) 171;
      numArray6[5] = (byte) 7;
      numArray6[29] = (byte) 104;
      numArray6[8] = (byte) 98;
      byte[] numArray7 = new byte[31 /*0x1F*/]
      {
        (byte) 1,
        (byte) 149,
        (byte) 49,
        (byte) 135,
        (byte) 35,
        (byte) 64 /*0x40*/,
        (byte) 99,
        (byte) 74,
        (byte) 59,
        (byte) 78,
        (byte) 79,
        (byte) 251,
        (byte) 208 /*0xD0*/,
        (byte) 211,
        (byte) 89,
        (byte) 61,
        (byte) 53,
        (byte) 55,
        (byte) 27,
        (byte) 221,
        (byte) 118,
        (byte) 91,
        (byte) 31 /*0x1F*/,
        (byte) 102,
        (byte) 167,
        (byte) 252,
        (byte) 117,
        (byte) 24,
        (byte) 254,
        (byte) 250,
        (byte) 159
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 31 /*0x1F*/);
      for (int index = 0; index < 31 /*0x1F*/; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[16 /*0x10*/];
      byte[] response = new byte[16 /*0x10*/];
      Array.Copy((Array) sc_13619.sspq, 372, (Array) numArray8, 0, 16 /*0x10*/);
      key.Query(true, 335, numArray8, response);
      Array.Copy((Array) sc_13619.sspr, 372, (Array) numArray8, 0, 16 /*0x10*/);
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
    byte[] numArray9 = new byte[141];
    byte[] numArray10 = new byte[55]
    {
      (byte) 188,
      (byte) 243,
      (byte) 133,
      (byte) 131,
      (byte) 215,
      (byte) 78,
      (byte) 164,
      (byte) 63 /*0x3F*/,
      (byte) 193,
      (byte) 188,
      (byte) 212,
      (byte) 117,
      (byte) 165,
      (byte) 141,
      (byte) 253,
      (byte) 243,
      (byte) 109,
      (byte) 68,
      (byte) 146,
      (byte) 26,
      (byte) 67,
      (byte) 133,
      (byte) 224 /*0xE0*/,
      (byte) 110,
      byte.MaxValue,
      (byte) 104,
      (byte) 116,
      (byte) 134,
      (byte) 84,
      (byte) 94,
      (byte) 21,
      (byte) 166,
      (byte) 246,
      (byte) 214,
      (byte) 32 /*0x20*/,
      (byte) 237,
      (byte) 116,
      (byte) 43,
      (byte) 87,
      (byte) 79,
      (byte) 202,
      (byte) 78,
      (byte) 168,
      (byte) 216,
      (byte) 160 /*0xA0*/,
      (byte) 26,
      (byte) 154,
      (byte) 157,
      (byte) 161,
      (byte) 207,
      (byte) 9,
      (byte) 127 /*0x7F*/,
      (byte) 119,
      (byte) 159,
      (byte) 108
    };
    byte[] numArray11 = new byte[55];
    numArray11[33] = (byte) 180;
    numArray11[14] = (byte) 57;
    numArray11[2] = (byte) 244;
    numArray11[3] = (byte) 28;
    numArray11[12] = (byte) 197;
    numArray11[50] = (byte) 238;
    numArray11[6] = (byte) 39;
    numArray11[7] = (byte) 236;
    numArray11[8] = (byte) 25;
    numArray11[9] = (byte) 197;
    numArray11[10] = (byte) 47;
    numArray11[25] = (byte) 150;
    numArray11[53] = (byte) 161;
    numArray11[30] = (byte) 60;
    numArray11[43] = (byte) 179;
    numArray11[39] = (byte) 0;
    numArray11[46] = (byte) 181;
    numArray11[17] = byte.MaxValue;
    numArray11[18] = (byte) 216;
    numArray11[19] = (byte) 151;
    numArray11[20] = (byte) 132;
    numArray11[21] = (byte) 109;
    numArray11[13] = (byte) 56;
    numArray11[23] = (byte) 145;
    numArray11[1] = (byte) 71;
    numArray11[45] = (byte) 170;
    numArray11[26] = (byte) 157;
    numArray11[40] = byte.MaxValue;
    numArray11[28] = (byte) 19;
    numArray11[0] = (byte) 230;
    numArray11[32 /*0x20*/] = (byte) 40;
    numArray11[36] = (byte) 187;
    numArray11[29] = (byte) 254;
    numArray11[42] = (byte) 35;
    numArray11[11] = (byte) 230;
    numArray11[35] = (byte) 50;
    numArray11[44] = (byte) 111;
    numArray11[27] = (byte) 253;
    numArray11[38] = (byte) 145;
    numArray11[22] = (byte) 204;
    numArray11[16 /*0x10*/] = (byte) 149;
    numArray11[41] = (byte) 101;
    numArray11[24] = (byte) 240 /*0xF0*/;
    numArray11[5] = (byte) 108;
    numArray11[52] = (byte) 75;
    numArray11[4] = (byte) 137;
    numArray11[31 /*0x1F*/] = (byte) 93;
    numArray11[47] = (byte) 123;
    numArray11[48 /*0x30*/] = (byte) 184;
    numArray11[49] = (byte) 189;
    numArray11[34] = (byte) 28;
    numArray11[51] = (byte) 25;
    numArray11[37] = (byte) 65;
    numArray11[15] = (byte) 24;
    numArray11[54] = (byte) 59;
    key.Query(true, 335, numArray10, numArray10);
    Array.Copy((Array) numArray10, 0, (Array) numArray9, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray9[index] ^= numArray11[index];
    byte[] numArray12 = new byte[55]
    {
      (byte) 73,
      (byte) 158,
      (byte) 160 /*0xA0*/,
      (byte) 2,
      (byte) 57,
      (byte) 102,
      (byte) 13,
      (byte) 16 /*0x10*/,
      (byte) 254,
      (byte) 40,
      (byte) 101,
      (byte) 131,
      (byte) 54,
      (byte) 34,
      (byte) 153,
      (byte) 3,
      (byte) 205,
      (byte) 163,
      (byte) 146,
      (byte) 152,
      (byte) 213,
      (byte) 90,
      (byte) 73,
      (byte) 3,
      (byte) 167,
      (byte) 83,
      (byte) 52,
      (byte) 141,
      (byte) 3,
      (byte) 250,
      (byte) 7,
      (byte) 243,
      (byte) 144 /*0x90*/,
      (byte) 204,
      (byte) 18,
      (byte) 10,
      (byte) 72,
      (byte) 114,
      (byte) 114,
      (byte) 252,
      (byte) 55,
      (byte) 33,
      (byte) 116,
      (byte) 114,
      (byte) 101,
      (byte) 172,
      (byte) 90,
      (byte) 45,
      (byte) 47,
      (byte) 142,
      (byte) 103,
      (byte) 172,
      (byte) 227,
      (byte) 193,
      (byte) 55
    };
    byte[] numArray13 = new byte[55]
    {
      (byte) 74,
      (byte) 158,
      (byte) 251,
      (byte) 56,
      (byte) 12,
      (byte) 57,
      (byte) 169,
      (byte) 178,
      (byte) 43,
      (byte) 108,
      (byte) 15,
      (byte) 245,
      (byte) 195,
      (byte) 28,
      (byte) 194,
      (byte) 31 /*0x1F*/,
      (byte) 240 /*0xF0*/,
      (byte) 113,
      (byte) 54,
      (byte) 129,
      (byte) 227,
      (byte) 123,
      (byte) 106,
      (byte) 215,
      (byte) 153,
      (byte) 3,
      (byte) 50,
      (byte) 204,
      (byte) 116,
      (byte) 75,
      (byte) 10,
      (byte) 189,
      (byte) 30,
      (byte) 74,
      (byte) 87,
      (byte) 133,
      byte.MaxValue,
      (byte) 163,
      (byte) 65,
      (byte) 77,
      (byte) 103,
      (byte) 223,
      (byte) 235,
      (byte) 51,
      (byte) 174,
      (byte) 217,
      (byte) 190,
      (byte) 233,
      (byte) 111,
      (byte) 222,
      (byte) 132,
      (byte) 109,
      (byte) 179,
      (byte) 77,
      (byte) 232
    };
    key.Query(true, 335, numArray12, numArray12);
    Array.Copy((Array) numArray12, 0, (Array) numArray9, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray9[index + 55] ^= numArray13[index];
    byte[] numArray14 = new byte[31 /*0x1F*/]
    {
      (byte) 156,
      (byte) 91,
      (byte) 230,
      (byte) 49,
      (byte) 128 /*0x80*/,
      (byte) 13,
      (byte) 4,
      (byte) 33,
      (byte) 113,
      (byte) 243,
      (byte) 106,
      (byte) 15,
      (byte) 142,
      (byte) 187,
      (byte) 90,
      (byte) 48 /*0x30*/,
      (byte) 149,
      (byte) 97,
      (byte) 179,
      (byte) 230,
      (byte) 215,
      (byte) 158,
      (byte) 250,
      (byte) 248,
      (byte) 7,
      (byte) 94,
      (byte) 116,
      (byte) 147,
      (byte) 91,
      (byte) 16 /*0x10*/,
      (byte) 206
    };
    byte[] numArray15 = new byte[31 /*0x1F*/];
    numArray15[16 /*0x10*/] = (byte) 99;
    numArray15[19] = (byte) 52;
    numArray15[2] = (byte) 86;
    numArray15[23] = (byte) 208 /*0xD0*/;
    numArray15[22] = (byte) 39;
    numArray15[5] = (byte) 123;
    numArray15[3] = (byte) 43;
    numArray15[7] = (byte) 242;
    numArray15[29] = (byte) 47;
    numArray15[9] = (byte) 55;
    numArray15[10] = (byte) 234;
    numArray15[6] = (byte) 76;
    numArray15[12] = (byte) 173;
    numArray15[27] = (byte) 134;
    numArray15[15] = (byte) 81;
    numArray15[28] = (byte) 235;
    numArray15[24] = (byte) 71;
    numArray15[8] = (byte) 13;
    numArray15[25] = (byte) 36;
    numArray15[17] = (byte) 233;
    numArray15[20] = (byte) 101;
    numArray15[21] = (byte) 152;
    numArray15[11] = (byte) 32 /*0x20*/;
    numArray15[18] = (byte) 207;
    numArray15[13] = (byte) 173;
    numArray15[4] = (byte) 231;
    numArray15[26] = (byte) 76;
    numArray15[1] = (byte) 39;
    numArray15[14] = (byte) 211;
    numArray15[0] = (byte) 129;
    numArray15[30] = (byte) 239;
    key.Query(true, 335, numArray14, numArray14);
    Array.Copy((Array) numArray14, 0, (Array) numArray9, 110, 31 /*0x1F*/);
    for (int index = 0; index < 31 /*0x1F*/; ++index)
      numArray9[index + 110] ^= numArray15[index];
    return Encoding.UTF8.GetString(numArray9);
  }

  internal static string ssp_appserver_13660()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[60];
      byte[] numArray2 = new byte[55];
      numArray2[11] = (byte) 64 /*0x40*/;
      numArray2[22] = (byte) 57;
      numArray2[38] = (byte) 214;
      numArray2[3] = (byte) 66;
      numArray2[51] = (byte) 230;
      numArray2[0] = (byte) 235;
      numArray2[16 /*0x10*/] = (byte) 134;
      numArray2[7] = (byte) 159;
      numArray2[6] = (byte) 170;
      numArray2[9] = (byte) 134;
      numArray2[10] = (byte) 170;
      numArray2[40] = (byte) 181;
      numArray2[12] = (byte) 8;
      numArray2[13] = (byte) 233;
      numArray2[14] = (byte) 189;
      numArray2[17] = (byte) 223;
      numArray2[47] = (byte) 64 /*0x40*/;
      numArray2[21] = (byte) 27;
      numArray2[37] = (byte) 201;
      numArray2[19] = (byte) 109;
      numArray2[49] = (byte) 247;
      numArray2[1] = (byte) 216;
      numArray2[35] = (byte) 199;
      numArray2[23] = (byte) 173;
      numArray2[24] = (byte) 183;
      numArray2[25] = (byte) 158;
      numArray2[26] = (byte) 161;
      numArray2[18] = (byte) 194;
      numArray2[39] = (byte) 131;
      numArray2[29] = (byte) 254;
      numArray2[36] = (byte) 196;
      numArray2[31 /*0x1F*/] = (byte) 146;
      numArray2[32 /*0x20*/] = (byte) 149;
      numArray2[8] = (byte) 118;
      numArray2[34] = (byte) 49;
      numArray2[5] = (byte) 202;
      numArray2[44] = (byte) 83;
      numArray2[2] = (byte) 15;
      numArray2[42] = (byte) 145;
      numArray2[52] = (byte) 159;
      numArray2[30] = (byte) 139;
      numArray2[41] = (byte) 227;
      numArray2[48 /*0x30*/] = (byte) 100;
      numArray2[43] = (byte) 246;
      numArray2[20] = (byte) 17;
      numArray2[28] = (byte) 123;
      numArray2[46] = (byte) 229;
      numArray2[45] = (byte) 31 /*0x1F*/;
      numArray2[15] = (byte) 160 /*0xA0*/;
      numArray2[53] = (byte) 182;
      numArray2[50] = (byte) 233;
      numArray2[33] = (byte) 27;
      numArray2[4] = (byte) 164;
      numArray2[27] = (byte) 147;
      numArray2[54] = (byte) 195;
      byte[] numArray3 = new byte[55];
      numArray3[51] = (byte) 18;
      numArray3[1] = (byte) 39;
      numArray3[44] = (byte) 88;
      numArray3[33] = (byte) 185;
      numArray3[43] = (byte) 187;
      numArray3[39] = (byte) 37;
      numArray3[6] = (byte) 181;
      numArray3[7] = (byte) 185;
      numArray3[8] = (byte) 157;
      numArray3[2] = (byte) 167;
      numArray3[10] = (byte) 220;
      numArray3[50] = (byte) 94;
      numArray3[53] = (byte) 69;
      numArray3[13] = (byte) 166;
      numArray3[14] = (byte) 90;
      numArray3[15] = (byte) 96 /*0x60*/;
      numArray3[24] = (byte) 132;
      numArray3[17] = (byte) 107;
      numArray3[18] = (byte) 22;
      numArray3[9] = byte.MaxValue;
      numArray3[5] = (byte) 28;
      numArray3[46] = (byte) 232;
      numArray3[34] = (byte) 221;
      numArray3[23] = (byte) 101;
      numArray3[52] = (byte) 233;
      numArray3[25] = (byte) 18;
      numArray3[21] = (byte) 211;
      numArray3[27] = (byte) 229;
      numArray3[29] = (byte) 192 /*0xC0*/;
      numArray3[19] = (byte) 89;
      numArray3[30] = (byte) 142;
      numArray3[0] = (byte) 195;
      numArray3[47] = (byte) 172;
      numArray3[31 /*0x1F*/] = (byte) 137;
      numArray3[12] = (byte) 157;
      numArray3[35] = (byte) 38;
      numArray3[36] = (byte) 116;
      numArray3[37] = (byte) 162;
      numArray3[4] = (byte) 154;
      numArray3[48 /*0x30*/] = (byte) 121;
      numArray3[20] = (byte) 127 /*0x7F*/;
      numArray3[41] = (byte) 91;
      numArray3[16 /*0x10*/] = (byte) 127 /*0x7F*/;
      numArray3[42] = (byte) 238;
      numArray3[32 /*0x20*/] = (byte) 204;
      numArray3[45] = (byte) 148;
      numArray3[3] = (byte) 222;
      numArray3[38] = (byte) 136;
      numArray3[11] = (byte) 42;
      numArray3[49] = (byte) 159;
      numArray3[40] = (byte) 226;
      numArray3[28] = (byte) 50;
      numArray3[26] = (byte) 228;
      numArray3[22] = (byte) 68;
      numArray3[54] = (byte) 251;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[5]
      {
        (byte) 233,
        (byte) 207,
        (byte) 139,
        (byte) 240 /*0xF0*/,
        (byte) 38
      };
      byte[] numArray5 = new byte[5]
      {
        (byte) 226,
        (byte) 179,
        (byte) 100,
        (byte) 149,
        (byte) 246
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 5);
      for (int index = 0; index < 5; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[60];
    byte[] numArray7 = new byte[55]
    {
      (byte) 145,
      (byte) 237,
      (byte) 211,
      (byte) 60,
      (byte) 3,
      (byte) 32 /*0x20*/,
      (byte) 53,
      (byte) 181,
      (byte) 82,
      (byte) 97,
      (byte) 79,
      (byte) 184,
      (byte) 163,
      (byte) 6,
      (byte) 125,
      (byte) 96 /*0x60*/,
      (byte) 165,
      (byte) 32 /*0x20*/,
      (byte) 199,
      (byte) 59,
      (byte) 35,
      (byte) 23,
      (byte) 250,
      (byte) 154,
      (byte) 66,
      (byte) 181,
      (byte) 112 /*0x70*/,
      (byte) 122,
      (byte) 248,
      (byte) 161,
      (byte) 144 /*0x90*/,
      (byte) 230,
      (byte) 60,
      (byte) 22,
      (byte) 134,
      (byte) 185,
      (byte) 141,
      (byte) 245,
      (byte) 156,
      (byte) 128 /*0x80*/,
      (byte) 119,
      (byte) 238,
      (byte) 40,
      (byte) 132,
      (byte) 216,
      (byte) 31 /*0x1F*/,
      (byte) 114,
      (byte) 227,
      (byte) 81,
      (byte) 42,
      (byte) 75,
      (byte) 6,
      (byte) 112 /*0x70*/,
      (byte) 63 /*0x3F*/,
      (byte) 107
    };
    byte[] numArray8 = new byte[55]
    {
      (byte) 140,
      (byte) 186,
      (byte) 215,
      (byte) 209,
      (byte) 36,
      (byte) 107,
      (byte) 213,
      (byte) 196,
      (byte) 17,
      (byte) 107,
      (byte) 5,
      (byte) 211,
      (byte) 234,
      (byte) 171,
      (byte) 26,
      (byte) 111,
      (byte) 151,
      (byte) 13,
      (byte) 131,
      (byte) 168,
      (byte) 242,
      (byte) 148,
      (byte) 84,
      (byte) 25,
      (byte) 119,
      (byte) 160 /*0xA0*/,
      (byte) 231,
      (byte) 205,
      (byte) 194,
      (byte) 6,
      (byte) 146,
      (byte) 0,
      (byte) 8,
      (byte) 120,
      (byte) 81,
      (byte) 237,
      (byte) 96 /*0x60*/,
      (byte) 203,
      (byte) 247,
      (byte) 52,
      (byte) 120,
      (byte) 160 /*0xA0*/,
      (byte) 161,
      (byte) 136,
      (byte) 30,
      (byte) 252,
      (byte) 143,
      (byte) 175,
      (byte) 129,
      (byte) 83,
      (byte) 85,
      (byte) 109,
      (byte) 229,
      (byte) 135,
      (byte) 12
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[5]
    {
      (byte) 57,
      (byte) 250,
      (byte) 77,
      (byte) 175,
      (byte) 187
    };
    byte[] numArray10 = new byte[5]
    {
      (byte) 108,
      (byte) 182,
      (byte) 58,
      (byte) 161,
      (byte) 194
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 5);
    for (int index = 0; index < 5; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_13661()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[55];
      byte[] numArray2 = new byte[55];
      numArray2[31 /*0x1F*/] = (byte) 119;
      numArray2[1] = (byte) 140;
      numArray2[39] = (byte) 155;
      numArray2[3] = (byte) 196;
      numArray2[4] = (byte) 210;
      numArray2[5] = (byte) 175;
      numArray2[6] = (byte) 132;
      numArray2[7] = (byte) 71;
      numArray2[8] = (byte) 195;
      numArray2[9] = (byte) 52;
      numArray2[37] = (byte) 141;
      numArray2[11] = (byte) 117;
      numArray2[50] = (byte) 146;
      numArray2[45] = (byte) 126;
      numArray2[42] = (byte) 123;
      numArray2[28] = (byte) 208 /*0xD0*/;
      numArray2[40] = (byte) 129;
      numArray2[41] = (byte) 40;
      numArray2[53] = (byte) 69;
      numArray2[14] = (byte) 67;
      numArray2[19] = (byte) 138;
      numArray2[21] = (byte) 253;
      numArray2[20] = (byte) 117;
      numArray2[30] = (byte) 49;
      numArray2[24] = (byte) 197;
      numArray2[25] = (byte) 84;
      numArray2[26] = (byte) 251;
      numArray2[27] = (byte) 78;
      numArray2[22] = (byte) 170;
      numArray2[10] = (byte) 238;
      numArray2[13] = (byte) 35;
      numArray2[2] = (byte) 128 /*0x80*/;
      numArray2[32 /*0x20*/] = (byte) 180;
      numArray2[33] = (byte) 111;
      numArray2[34] = (byte) 163;
      numArray2[16 /*0x10*/] = (byte) 40;
      numArray2[36] = (byte) 99;
      numArray2[51] = (byte) 240 /*0xF0*/;
      numArray2[38] = (byte) 88;
      numArray2[0] = (byte) 123;
      numArray2[47] = (byte) 215;
      numArray2[35] = (byte) 229;
      numArray2[54] = (byte) 242;
      numArray2[43] = (byte) 54;
      numArray2[29] = (byte) 188;
      numArray2[52] = (byte) 91;
      numArray2[46] = (byte) 119;
      numArray2[49] = (byte) 34;
      numArray2[48 /*0x30*/] = (byte) 9;
      numArray2[23] = (byte) 12;
      numArray2[17] = (byte) 205;
      numArray2[12] = (byte) 194;
      numArray2[15] = (byte) 140;
      numArray2[18] = (byte) 85;
      numArray2[44] = (byte) 138;
      byte[] numArray3 = new byte[55];
      numArray3[54] = (byte) 194;
      numArray3[14] = (byte) 21;
      numArray3[16 /*0x10*/] = (byte) 81;
      numArray3[36] = (byte) 74;
      numArray3[4] = (byte) 66;
      numArray3[5] = (byte) 19;
      numArray3[6] = (byte) 209;
      numArray3[7] = (byte) 126;
      numArray3[8] = (byte) 214;
      numArray3[15] = (byte) 16 /*0x10*/;
      numArray3[10] = (byte) 156;
      numArray3[43] = (byte) 117;
      numArray3[53] = (byte) 254;
      numArray3[13] = (byte) 6;
      numArray3[23] = (byte) 104;
      numArray3[19] = (byte) 7;
      numArray3[24] = (byte) 156;
      numArray3[44] = (byte) 2;
      numArray3[18] = (byte) 119;
      numArray3[40] = (byte) 37;
      numArray3[11] = (byte) 149;
      numArray3[2] = (byte) 215;
      numArray3[51] = (byte) 17;
      numArray3[22] = (byte) 39;
      numArray3[27] = (byte) 95;
      numArray3[25] = (byte) 69;
      numArray3[26] = (byte) 164;
      numArray3[0] = (byte) 178;
      numArray3[28] = (byte) 26;
      numArray3[3] = (byte) 148;
      numArray3[12] = (byte) 214;
      numArray3[31 /*0x1F*/] = (byte) 128 /*0x80*/;
      numArray3[32 /*0x20*/] = (byte) 74;
      numArray3[33] = (byte) 32 /*0x20*/;
      numArray3[48 /*0x30*/] = (byte) 158;
      numArray3[35] = (byte) 75;
      numArray3[20] = (byte) 153;
      numArray3[37] = (byte) 114;
      numArray3[38] = (byte) 162;
      numArray3[39] = (byte) 190;
      numArray3[30] = (byte) 5;
      numArray3[29] = (byte) 90;
      numArray3[42] = (byte) 240 /*0xF0*/;
      numArray3[17] = (byte) 105;
      numArray3[34] = (byte) 143;
      numArray3[45] = (byte) 231;
      numArray3[46] = (byte) 78;
      numArray3[47] = (byte) 66;
      numArray3[49] = (byte) 123;
      numArray3[21] = (byte) 40;
      numArray3[50] = (byte) 83;
      numArray3[1] = (byte) 106;
      numArray3[52] = (byte) 243;
      numArray3[41] = (byte) 11;
      numArray3[9] = (byte) 76;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[55];
    byte[] numArray5 = new byte[55];
    numArray5[19] = (byte) 208 /*0xD0*/;
    numArray5[15] = (byte) 124;
    numArray5[2] = (byte) 196;
    numArray5[16 /*0x10*/] = (byte) 97;
    numArray5[48 /*0x30*/] = (byte) 126;
    numArray5[5] = (byte) 170;
    numArray5[0] = (byte) 44;
    numArray5[53] = (byte) 92;
    numArray5[14] = (byte) 97;
    numArray5[9] = byte.MaxValue;
    numArray5[10] = (byte) 52;
    numArray5[51] = (byte) 89;
    numArray5[12] = (byte) 60;
    numArray5[13] = (byte) 53;
    numArray5[25] = (byte) 246;
    numArray5[49] = (byte) 83;
    numArray5[39] = (byte) 236;
    numArray5[6] = (byte) 252;
    numArray5[4] = (byte) 19;
    numArray5[20] = (byte) 182;
    numArray5[7] = (byte) 225;
    numArray5[21] = (byte) 30;
    numArray5[22] = (byte) 107;
    numArray5[23] = (byte) 208 /*0xD0*/;
    numArray5[24] = (byte) 75;
    numArray5[1] = (byte) 231;
    numArray5[26] = (byte) 72;
    numArray5[11] = (byte) 198;
    numArray5[28] = (byte) 170;
    numArray5[35] = (byte) 81;
    numArray5[50] = (byte) 29;
    numArray5[27] = (byte) 229;
    numArray5[32 /*0x20*/] = (byte) 104;
    numArray5[33] = (byte) 121;
    numArray5[17] = (byte) 177;
    numArray5[45] = (byte) 136;
    numArray5[36] = (byte) 145;
    numArray5[30] = (byte) 203;
    numArray5[38] = (byte) 213;
    numArray5[43] = (byte) 78;
    numArray5[8] = (byte) 30;
    numArray5[41] = (byte) 213;
    numArray5[42] = (byte) 77;
    numArray5[31 /*0x1F*/] = (byte) 53;
    numArray5[44] = (byte) 198;
    numArray5[3] = (byte) 85;
    numArray5[46] = (byte) 129;
    numArray5[18] = (byte) 116;
    numArray5[37] = (byte) 5;
    numArray5[40] = (byte) 116;
    numArray5[29] = (byte) 180;
    numArray5[47] = (byte) 180;
    numArray5[52] = (byte) 124;
    numArray5[34] = (byte) 170;
    numArray5[54] = (byte) 204;
    byte[] numArray6 = new byte[55]
    {
      (byte) 139,
      (byte) 163,
      (byte) 74,
      (byte) 171,
      (byte) 161,
      (byte) 187,
      (byte) 240 /*0xF0*/,
      (byte) 127 /*0x7F*/,
      (byte) 60,
      (byte) 1,
      (byte) 30,
      (byte) 63 /*0x3F*/,
      (byte) 156,
      (byte) 221,
      (byte) 37,
      (byte) 219,
      (byte) 205,
      (byte) 210,
      (byte) 118,
      (byte) 82,
      (byte) 127 /*0x7F*/,
      (byte) 199,
      (byte) 44,
      (byte) 42,
      (byte) 254,
      (byte) 216,
      (byte) 236,
      (byte) 138,
      (byte) 116,
      (byte) 19,
      (byte) 62,
      (byte) 106,
      (byte) 77,
      (byte) 88,
      (byte) 113,
      (byte) 106,
      (byte) 154,
      (byte) 104,
      (byte) 199,
      (byte) 7,
      (byte) 164,
      (byte) 91,
      (byte) 165,
      (byte) 42,
      (byte) 12,
      (byte) 47,
      (byte) 243,
      (byte) 94,
      (byte) 240 /*0xF0*/,
      (byte) 57,
      (byte) 188,
      (byte) 95,
      (byte) 34,
      (byte) 153,
      (byte) 131
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13662()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[18];
      byte[] numArray2 = new byte[18]
      {
        (byte) 169,
        (byte) 207,
        (byte) 177,
        (byte) 224 /*0xE0*/,
        (byte) 211,
        (byte) 194,
        (byte) 70,
        (byte) 81,
        (byte) 21,
        (byte) 239,
        (byte) 60,
        (byte) 76,
        (byte) 96 /*0x60*/,
        (byte) 36,
        (byte) 253,
        byte.MaxValue,
        (byte) 67,
        (byte) 236
      };
      byte[] numArray3 = new byte[18];
      numArray3[17] = (byte) 97;
      numArray3[1] = (byte) 112 /*0x70*/;
      numArray3[2] = (byte) 228;
      numArray3[11] = (byte) 237;
      numArray3[4] = (byte) 8;
      numArray3[5] = (byte) 4;
      numArray3[3] = (byte) 9;
      numArray3[7] = (byte) 212;
      numArray3[8] = (byte) 181;
      numArray3[9] = (byte) 44;
      numArray3[10] = (byte) 253;
      numArray3[12] = (byte) 228;
      numArray3[15] = (byte) 97;
      numArray3[6] = (byte) 209;
      numArray3[0] = (byte) 61;
      numArray3[13] = (byte) 9;
      numArray3[16 /*0x10*/] = (byte) 143;
      numArray3[14] = (byte) 239;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 18);
      for (int index = 0; index < 18; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[18];
    byte[] numArray5 = new byte[18]
    {
      (byte) 88,
      (byte) 129,
      (byte) 216,
      (byte) 169,
      (byte) 58,
      (byte) 49,
      (byte) 28,
      (byte) 96 /*0x60*/,
      (byte) 89,
      (byte) 247,
      (byte) 0,
      (byte) 168,
      (byte) 81,
      (byte) 93,
      (byte) 168,
      (byte) 174,
      (byte) 244,
      (byte) 101
    };
    byte[] numArray6 = new byte[18];
    numArray6[15] = (byte) 234;
    numArray6[11] = (byte) 143;
    numArray6[9] = (byte) 42;
    numArray6[3] = (byte) 135;
    numArray6[4] = (byte) 35;
    numArray6[5] = (byte) 76;
    numArray6[6] = (byte) 249;
    numArray6[7] = (byte) 35;
    numArray6[8] = (byte) 152;
    numArray6[0] = (byte) 63 /*0x3F*/;
    numArray6[14] = (byte) 200;
    numArray6[12] = (byte) 251;
    numArray6[2] = (byte) 66;
    numArray6[10] = (byte) 26;
    numArray6[1] = (byte) 240 /*0xF0*/;
    numArray6[13] = (byte) 232;
    numArray6[16 /*0x10*/] = (byte) 232;
    numArray6[17] = (byte) 113;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 18);
    for (int index = 0; index < 18; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13663()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[18];
      byte[] numArray2 = new byte[18];
      numArray2[17] = (byte) 146;
      numArray2[14] = (byte) 18;
      numArray2[7] = (byte) 21;
      numArray2[1] = (byte) 117;
      numArray2[4] = (byte) 11;
      numArray2[13] = (byte) 202;
      numArray2[6] = (byte) 12;
      numArray2[11] = (byte) 84;
      numArray2[8] = (byte) 9;
      numArray2[9] = (byte) 6;
      numArray2[10] = (byte) 26;
      numArray2[0] = (byte) 209;
      numArray2[3] = (byte) 204;
      numArray2[2] = (byte) 154;
      numArray2[5] = (byte) 25;
      numArray2[15] = (byte) 138;
      numArray2[16 /*0x10*/] = (byte) 254;
      numArray2[12] = (byte) 243;
      byte[] numArray3 = new byte[18];
      numArray3[16 /*0x10*/] = (byte) 142;
      numArray3[1] = (byte) 181;
      numArray3[9] = (byte) 204;
      numArray3[2] = (byte) 29;
      numArray3[4] = (byte) 233;
      numArray3[6] = (byte) 245;
      numArray3[5] = (byte) 101;
      numArray3[14] = (byte) 136;
      numArray3[3] = (byte) 145;
      numArray3[10] = (byte) 201;
      numArray3[0] = (byte) 124;
      numArray3[11] = (byte) 234;
      numArray3[12] = (byte) 17;
      numArray3[13] = (byte) 48 /*0x30*/;
      numArray3[7] = (byte) 63 /*0x3F*/;
      numArray3[15] = (byte) 8;
      numArray3[8] = (byte) 41;
      numArray3[17] = (byte) 53;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 18);
      for (int index = 0; index < 18; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[18];
    byte[] numArray5 = new byte[18];
    numArray5[17] = (byte) 34;
    numArray5[1] = (byte) 23;
    numArray5[2] = (byte) 95;
    numArray5[3] = (byte) 246;
    numArray5[4] = (byte) 151;
    numArray5[8] = (byte) 50;
    numArray5[13] = (byte) 86;
    numArray5[7] = (byte) 24;
    numArray5[5] = (byte) 98;
    numArray5[11] = (byte) 169;
    numArray5[15] = (byte) 88;
    numArray5[6] = (byte) 33;
    numArray5[10] = (byte) 2;
    numArray5[9] = (byte) 30;
    numArray5[0] = (byte) 85;
    numArray5[14] = (byte) 71;
    numArray5[16 /*0x10*/] = (byte) 25;
    numArray5[12] = (byte) 138;
    byte[] numArray6 = new byte[18]
    {
      (byte) 194,
      (byte) 182,
      (byte) 155,
      (byte) 166,
      (byte) 221,
      (byte) 0,
      (byte) 8,
      (byte) 153,
      (byte) 88,
      (byte) 55,
      (byte) 50,
      (byte) 46,
      (byte) 199,
      (byte) 146,
      (byte) 91,
      (byte) 181,
      (byte) 21,
      (byte) 195
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 18);
    for (int index = 0; index < 18; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13664()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10];
      numArray2[6] = (byte) 63 /*0x3F*/;
      numArray2[1] = (byte) 141;
      numArray2[7] = (byte) 201;
      numArray2[3] = (byte) 207;
      numArray2[4] = (byte) 94;
      numArray2[5] = (byte) 17;
      numArray2[8] = (byte) 122;
      numArray2[2] = (byte) 200;
      numArray2[0] = (byte) 110;
      numArray2[9] = (byte) 212;
      byte[] numArray3 = new byte[10];
      numArray3[1] = (byte) 50;
      numArray3[4] = (byte) 62;
      numArray3[2] = (byte) 113;
      numArray3[3] = (byte) 197;
      numArray3[6] = (byte) 57;
      numArray3[5] = (byte) 6;
      numArray3[8] = (byte) 56;
      numArray3[7] = (byte) 138;
      numArray3[0] = (byte) 183;
      numArray3[9] = (byte) 241;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10];
    numArray5[7] = (byte) 106;
    numArray5[5] = (byte) 235;
    numArray5[2] = (byte) 130;
    numArray5[0] = (byte) 101;
    numArray5[6] = (byte) 85;
    numArray5[4] = (byte) 247;
    numArray5[8] = (byte) 202;
    numArray5[3] = (byte) 127 /*0x7F*/;
    numArray5[1] = (byte) 130;
    numArray5[9] = (byte) 170;
    byte[] numArray6 = new byte[10];
    numArray6[3] = (byte) 178;
    numArray6[7] = (byte) 245;
    numArray6[9] = (byte) 175;
    numArray6[4] = (byte) 202;
    numArray6[5] = (byte) 254;
    numArray6[1] = (byte) 102;
    numArray6[6] = (byte) 90;
    numArray6[2] = (byte) 141;
    numArray6[8] = (byte) 194;
    numArray6[0] = (byte) 240 /*0xF0*/;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13665()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 68,
        (byte) 218,
        (byte) 46,
        (byte) 141,
        (byte) 89,
        (byte) 41,
        (byte) 56,
        (byte) 130,
        (byte) 3,
        (byte) 80 /*0x50*/
      };
      byte[] numArray3 = new byte[10];
      numArray3[0] = (byte) 97;
      numArray3[7] = (byte) 220;
      numArray3[2] = (byte) 88;
      numArray3[3] = (byte) 162;
      numArray3[1] = (byte) 168;
      numArray3[9] = (byte) 127 /*0x7F*/;
      numArray3[6] = (byte) 20;
      numArray3[5] = (byte) 104;
      numArray3[4] = (byte) 90;
      numArray3[8] = (byte) 167;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[19];
      byte[] response = new byte[19];
      Array.Copy((Array) sc_13619.sspq, 388, (Array) numArray4, 0, 19);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_13619.sspr, 388, (Array) numArray4, 0, 19);
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
      (byte) 34,
      (byte) 204,
      (byte) 87,
      (byte) 60,
      (byte) 98,
      (byte) 213,
      (byte) 209,
      (byte) 170,
      (byte) 120,
      (byte) 160 /*0xA0*/
    };
    byte[] numArray7 = new byte[10]
    {
      (byte) 82,
      (byte) 179,
      (byte) 125,
      (byte) 139,
      (byte) 106,
      (byte) 29,
      byte.MaxValue,
      (byte) 173,
      (byte) 82,
      (byte) 146
    };
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray5[index] ^= numArray7[index];
    byte[] numArray8 = new byte[43];
    byte[] response1 = new byte[43];
    Array.Copy((Array) sc_13619.sspq, 407, (Array) numArray8, 0, 43);
    key.Query(true, 335, numArray8, response1);
    Array.Copy((Array) sc_13619.sspr, 407, (Array) numArray8, 0, 43);
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

  internal static string ssp_appserver_13666()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[44];
      byte[] numArray2 = new byte[44]
      {
        (byte) 53,
        (byte) 122,
        (byte) 10,
        (byte) 13,
        (byte) 70,
        (byte) 19,
        (byte) 134,
        (byte) 11,
        (byte) 72,
        (byte) 254,
        (byte) 220,
        (byte) 98,
        (byte) 114,
        (byte) 102,
        (byte) 142,
        (byte) 213,
        (byte) 120,
        (byte) 188,
        (byte) 24,
        (byte) 223,
        (byte) 137,
        (byte) 230,
        (byte) 167,
        (byte) 252,
        (byte) 190,
        (byte) 245,
        (byte) 49,
        (byte) 168,
        (byte) 63 /*0x3F*/,
        (byte) 200,
        (byte) 111,
        (byte) 68,
        (byte) 136,
        (byte) 177,
        (byte) 37,
        (byte) 34,
        (byte) 60,
        (byte) 15,
        (byte) 136,
        (byte) 75,
        (byte) 110,
        (byte) 18,
        (byte) 215,
        (byte) 66
      };
      byte[] numArray3 = new byte[44]
      {
        (byte) 186,
        (byte) 182,
        (byte) 9,
        (byte) 165,
        (byte) 216,
        (byte) 236,
        (byte) 73,
        (byte) 162,
        (byte) 14,
        (byte) 171,
        (byte) 41,
        (byte) 242,
        (byte) 240 /*0xF0*/,
        (byte) 234,
        (byte) 77,
        (byte) 166,
        (byte) 182,
        (byte) 181,
        (byte) 39,
        (byte) 40,
        (byte) 76,
        (byte) 249,
        (byte) 234,
        (byte) 160 /*0xA0*/,
        (byte) 103,
        (byte) 23,
        (byte) 212,
        (byte) 176 /*0xB0*/,
        (byte) 90,
        (byte) 81,
        (byte) 78,
        (byte) 63 /*0x3F*/,
        (byte) 43,
        (byte) 175,
        (byte) 62,
        (byte) 224 /*0xE0*/,
        (byte) 162,
        (byte) 244,
        (byte) 184,
        (byte) 110,
        (byte) 85,
        (byte) 79,
        (byte) 76,
        (byte) 225
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 44);
      for (int index = 0; index < 44; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[44];
    byte[] numArray5 = new byte[44]
    {
      (byte) 204,
      (byte) 227,
      (byte) 139,
      (byte) 114,
      (byte) 55,
      (byte) 144 /*0x90*/,
      (byte) 24,
      (byte) 38,
      (byte) 78,
      (byte) 210,
      (byte) 13,
      (byte) 78,
      (byte) 80 /*0x50*/,
      (byte) 226,
      (byte) 221,
      (byte) 204,
      (byte) 127 /*0x7F*/,
      (byte) 142,
      (byte) 152,
      (byte) 218,
      (byte) 192 /*0xC0*/,
      (byte) 65,
      (byte) 107,
      (byte) 153,
      (byte) 104,
      (byte) 51,
      (byte) 204,
      (byte) 112 /*0x70*/,
      (byte) 119,
      (byte) 15,
      (byte) 100,
      (byte) 11,
      (byte) 27,
      (byte) 248,
      (byte) 100,
      (byte) 196,
      (byte) 179,
      (byte) 86,
      (byte) 56,
      (byte) 179,
      (byte) 112 /*0x70*/,
      (byte) 139,
      (byte) 215,
      (byte) 79
    };
    byte[] numArray6 = new byte[44];
    numArray6[6] = (byte) 229;
    numArray6[1] = (byte) 113;
    numArray6[41] = (byte) 111;
    numArray6[16 /*0x10*/] = (byte) 144 /*0x90*/;
    numArray6[10] = (byte) 11;
    numArray6[5] = (byte) 14;
    numArray6[4] = (byte) 126;
    numArray6[7] = (byte) 167;
    numArray6[8] = (byte) 39;
    numArray6[21] = (byte) 82;
    numArray6[3] = (byte) 241;
    numArray6[11] = (byte) 194;
    numArray6[12] = (byte) 2;
    numArray6[13] = (byte) 70;
    numArray6[24] = (byte) 207;
    numArray6[15] = (byte) 232;
    numArray6[2] = (byte) 176 /*0xB0*/;
    numArray6[29] = (byte) 239;
    numArray6[18] = (byte) 138;
    numArray6[19] = (byte) 171;
    numArray6[20] = (byte) 153;
    numArray6[43] = (byte) 126;
    numArray6[14] = (byte) 165;
    numArray6[23] = (byte) 184;
    numArray6[42] = (byte) 14;
    numArray6[25] = (byte) 64 /*0x40*/;
    numArray6[22] = (byte) 205;
    numArray6[26] = (byte) 154;
    numArray6[27] = (byte) 77;
    numArray6[9] = (byte) 179;
    numArray6[30] = (byte) 213;
    numArray6[38] = (byte) 201;
    numArray6[32 /*0x20*/] = (byte) 214;
    numArray6[0] = (byte) 107;
    numArray6[28] = (byte) 176 /*0xB0*/;
    numArray6[35] = (byte) 81;
    numArray6[36] = (byte) 161;
    numArray6[17] = (byte) 85;
    numArray6[34] = (byte) 41;
    numArray6[33] = (byte) 115;
    numArray6[40] = (byte) 68;
    numArray6[31 /*0x1F*/] = (byte) 123;
    numArray6[39] = (byte) 125;
    numArray6[37] = (byte) 15;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 44);
    for (int index = 0; index < 44; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
