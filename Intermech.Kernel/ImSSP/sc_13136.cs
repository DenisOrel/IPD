// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_13136
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_13136
{
  private static byte[] sspq = new byte[424]
  {
    (byte) 59,
    (byte) 130,
    (byte) 161,
    (byte) 175,
    (byte) 211,
    (byte) 64 /*0x40*/,
    (byte) 242,
    (byte) 171,
    (byte) 49,
    (byte) 45,
    (byte) 120,
    (byte) 97,
    (byte) 195,
    (byte) 31 /*0x1F*/,
    (byte) 84,
    (byte) 53,
    (byte) 212,
    (byte) 62,
    (byte) 108,
    (byte) 170,
    (byte) 213,
    (byte) 21,
    (byte) 70,
    (byte) 146,
    (byte) 38,
    (byte) 32 /*0x20*/,
    (byte) 215,
    (byte) 236,
    (byte) 15,
    (byte) 190,
    (byte) 108,
    (byte) 194,
    (byte) 74,
    (byte) 153,
    (byte) 121,
    (byte) 128 /*0x80*/,
    (byte) 249,
    (byte) 163,
    (byte) 52,
    (byte) 180,
    (byte) 179,
    (byte) 36,
    (byte) 236,
    (byte) 71,
    (byte) 73,
    (byte) 50,
    (byte) 158,
    (byte) 206,
    (byte) 33,
    (byte) 236,
    (byte) 135,
    (byte) 19,
    (byte) 175,
    (byte) 142,
    (byte) 226,
    (byte) 142,
    (byte) 251,
    (byte) 195,
    (byte) 102,
    (byte) 185,
    (byte) 20,
    (byte) 205,
    (byte) 131,
    (byte) 118,
    (byte) 138,
    (byte) 47,
    (byte) 27,
    (byte) 137,
    (byte) 222,
    (byte) 25,
    (byte) 183,
    (byte) 21,
    (byte) 131,
    (byte) 152,
    (byte) 69,
    (byte) 72,
    (byte) 39,
    (byte) 162,
    (byte) 149,
    (byte) 104,
    (byte) 3,
    (byte) 197,
    (byte) 215,
    (byte) 199,
    (byte) 175,
    (byte) 147,
    (byte) 107,
    (byte) 252,
    (byte) 153,
    (byte) 98,
    (byte) 6,
    (byte) 241,
    (byte) 91,
    (byte) 211,
    (byte) 181,
    (byte) 99,
    (byte) 125,
    (byte) 155,
    (byte) 113,
    (byte) 81,
    (byte) 215,
    (byte) 144 /*0x90*/,
    (byte) 209,
    (byte) 228,
    (byte) 159,
    (byte) 29,
    (byte) 212,
    (byte) 196,
    (byte) 115,
    (byte) 72,
    (byte) 71,
    (byte) 46,
    (byte) 150,
    (byte) 150,
    (byte) 75,
    (byte) 42,
    (byte) 121,
    (byte) 167,
    (byte) 146,
    (byte) 180,
    (byte) 212,
    (byte) 77,
    (byte) 11,
    (byte) 81,
    (byte) 36,
    (byte) 87,
    (byte) 96 /*0x60*/,
    (byte) 111,
    (byte) 130,
    (byte) 12,
    (byte) 56,
    (byte) 62,
    (byte) 207,
    (byte) 228,
    (byte) 74,
    (byte) 228,
    (byte) 137,
    (byte) 25,
    (byte) 42,
    (byte) 240 /*0xF0*/,
    (byte) 141,
    (byte) 207,
    (byte) 7,
    byte.MaxValue,
    (byte) 221,
    (byte) 52,
    (byte) 246,
    (byte) 88,
    (byte) 23,
    (byte) 33,
    (byte) 67,
    (byte) 30,
    (byte) 223,
    (byte) 157,
    (byte) 31 /*0x1F*/,
    (byte) 229,
    (byte) 44,
    (byte) 187,
    (byte) 21,
    (byte) 34,
    (byte) 232,
    (byte) 16 /*0x10*/,
    (byte) 224 /*0xE0*/,
    (byte) 42,
    (byte) 47,
    (byte) 173,
    (byte) 218,
    (byte) 213,
    (byte) 112 /*0x70*/,
    (byte) 40,
    (byte) 15,
    (byte) 247,
    (byte) 169,
    (byte) 170,
    (byte) 44,
    (byte) 64 /*0x40*/,
    (byte) 145,
    (byte) 36,
    (byte) 240 /*0xF0*/,
    (byte) 188,
    (byte) 23,
    (byte) 46,
    (byte) 5,
    (byte) 247,
    (byte) 15,
    (byte) 8,
    (byte) 201,
    (byte) 14,
    (byte) 154,
    (byte) 229,
    (byte) 189,
    (byte) 52,
    (byte) 132,
    (byte) 122,
    (byte) 169,
    (byte) 222,
    (byte) 177,
    (byte) 138,
    (byte) 47,
    (byte) 14,
    (byte) 165,
    (byte) 134,
    (byte) 139,
    (byte) 173,
    (byte) 128 /*0x80*/,
    (byte) 173,
    (byte) 155,
    (byte) 251,
    (byte) 202,
    (byte) 184,
    (byte) 119,
    (byte) 103,
    (byte) 27,
    (byte) 192 /*0xC0*/,
    (byte) 77,
    (byte) 67,
    (byte) 147,
    (byte) 116,
    (byte) 11,
    (byte) 171,
    (byte) 113,
    (byte) 1,
    (byte) 85,
    (byte) 186,
    (byte) 86,
    (byte) 65,
    (byte) 135,
    (byte) 81,
    (byte) 218,
    (byte) 123,
    (byte) 8,
    (byte) 199,
    (byte) 80 /*0x50*/,
    (byte) 128 /*0x80*/,
    (byte) 125,
    (byte) 60,
    (byte) 167,
    (byte) 35,
    (byte) 220,
    (byte) 222,
    byte.MaxValue,
    (byte) 69,
    (byte) 132,
    (byte) 6,
    (byte) 81,
    (byte) 138,
    (byte) 97,
    (byte) 9,
    byte.MaxValue,
    (byte) 2,
    (byte) 234,
    (byte) 75,
    (byte) 144 /*0x90*/,
    (byte) 182,
    (byte) 28,
    (byte) 1,
    (byte) 157,
    (byte) 107,
    (byte) 69,
    (byte) 196,
    (byte) 21,
    (byte) 160 /*0xA0*/,
    (byte) 237,
    (byte) 152,
    (byte) 166,
    (byte) 134,
    (byte) 3,
    (byte) 239,
    (byte) 103,
    (byte) 217,
    (byte) 90,
    (byte) 159,
    (byte) 139,
    (byte) 198,
    (byte) 160 /*0xA0*/,
    (byte) 43,
    (byte) 203,
    (byte) 104,
    (byte) 25,
    (byte) 10,
    (byte) 115,
    (byte) 131,
    (byte) 248,
    (byte) 47,
    (byte) 62,
    (byte) 241,
    (byte) 40,
    (byte) 50,
    (byte) 246,
    (byte) 179,
    (byte) 77,
    (byte) 179,
    (byte) 58,
    (byte) 227,
    (byte) 8,
    (byte) 24,
    (byte) 4,
    (byte) 42,
    (byte) 214,
    (byte) 84,
    (byte) 193,
    (byte) 45,
    (byte) 100,
    (byte) 19,
    (byte) 161,
    (byte) 133,
    (byte) 254,
    (byte) 161,
    (byte) 145,
    (byte) 196,
    (byte) 73,
    (byte) 70,
    (byte) 30,
    (byte) 228,
    (byte) 142,
    (byte) 185,
    (byte) 242,
    (byte) 120,
    (byte) 201,
    (byte) 58,
    (byte) 26,
    (byte) 57,
    (byte) 31 /*0x1F*/,
    (byte) 39,
    (byte) 79,
    (byte) 52,
    (byte) 103,
    (byte) 6,
    byte.MaxValue,
    (byte) 107,
    (byte) 246,
    (byte) 1,
    (byte) 127 /*0x7F*/,
    (byte) 154,
    (byte) 150,
    (byte) 141,
    (byte) 243,
    (byte) 16 /*0x10*/,
    (byte) 232,
    (byte) 118,
    (byte) 24,
    (byte) 52,
    (byte) 114,
    (byte) 227,
    byte.MaxValue,
    (byte) 104,
    (byte) 192 /*0xC0*/,
    (byte) 116,
    (byte) 85,
    (byte) 92,
    (byte) 0,
    (byte) 111,
    (byte) 59,
    (byte) 71,
    (byte) 110,
    (byte) 219,
    (byte) 216,
    (byte) 220,
    (byte) 94,
    (byte) 30,
    (byte) 25,
    (byte) 118,
    (byte) 155,
    (byte) 54,
    (byte) 222,
    (byte) 86,
    (byte) 10,
    (byte) 134,
    (byte) 137,
    (byte) 20,
    (byte) 50,
    (byte) 8,
    (byte) 5,
    (byte) 24,
    (byte) 165,
    (byte) 89,
    (byte) 37,
    (byte) 88,
    (byte) 74,
    (byte) 99,
    (byte) 80 /*0x50*/,
    (byte) 25,
    (byte) 244,
    (byte) 59,
    (byte) 41,
    (byte) 25,
    (byte) 148,
    (byte) 61,
    (byte) 41,
    (byte) 159,
    (byte) 129,
    (byte) 221,
    (byte) 32 /*0x20*/,
    (byte) 172,
    (byte) 139,
    (byte) 140,
    (byte) 107,
    (byte) 188,
    (byte) 20,
    (byte) 55,
    (byte) 125,
    (byte) 128 /*0x80*/,
    (byte) 106,
    (byte) 143,
    (byte) 198,
    (byte) 59,
    (byte) 71,
    (byte) 96 /*0x60*/,
    (byte) 241,
    (byte) 211,
    (byte) 24,
    (byte) 213,
    (byte) 70,
    (byte) 156,
    (byte) 94,
    (byte) 85,
    (byte) 43,
    (byte) 218,
    (byte) 61,
    (byte) 136,
    (byte) 123,
    (byte) 212,
    (byte) 207,
    (byte) 4
  };
  private static byte[] sspr = new byte[424]
  {
    (byte) 31 /*0x1F*/,
    (byte) 96 /*0x60*/,
    (byte) 7,
    (byte) 218,
    (byte) 159,
    (byte) 122,
    (byte) 157,
    (byte) 247,
    (byte) 110,
    (byte) 99,
    (byte) 53,
    (byte) 91,
    (byte) 240 /*0xF0*/,
    (byte) 161,
    (byte) 226,
    (byte) 59,
    (byte) 99,
    (byte) 248,
    (byte) 20,
    (byte) 4,
    (byte) 96 /*0x60*/,
    (byte) 76,
    (byte) 196,
    (byte) 200,
    (byte) 250,
    (byte) 216,
    (byte) 113,
    (byte) 131,
    (byte) 225,
    (byte) 141,
    (byte) 57,
    (byte) 105,
    (byte) 209,
    (byte) 71,
    (byte) 195,
    (byte) 211,
    (byte) 55,
    (byte) 207,
    (byte) 237,
    (byte) 248,
    (byte) 65,
    (byte) 133,
    (byte) 35,
    (byte) 112 /*0x70*/,
    (byte) 173,
    (byte) 137,
    (byte) 134,
    (byte) 8,
    (byte) 197,
    (byte) 9,
    (byte) 50,
    (byte) 29,
    (byte) 234,
    (byte) 214,
    (byte) 125,
    (byte) 243,
    (byte) 230,
    (byte) 215,
    (byte) 102,
    (byte) 251,
    (byte) 106,
    (byte) 199,
    (byte) 251,
    (byte) 249,
    (byte) 68,
    (byte) 230,
    (byte) 126,
    (byte) 102,
    (byte) 176 /*0xB0*/,
    (byte) 129,
    (byte) 207,
    (byte) 178,
    (byte) 45,
    (byte) 103,
    (byte) 186,
    (byte) 3,
    (byte) 213,
    (byte) 10,
    (byte) 252,
    (byte) 160 /*0xA0*/,
    (byte) 92,
    (byte) 167,
    (byte) 69,
    (byte) 214,
    (byte) 6,
    (byte) 149,
    (byte) 163,
    (byte) 72,
    (byte) 15,
    (byte) 95,
    (byte) 158,
    (byte) 143,
    (byte) 135,
    (byte) 237,
    (byte) 223,
    (byte) 140,
    (byte) 171,
    (byte) 237,
    (byte) 95,
    (byte) 186,
    byte.MaxValue,
    (byte) 74,
    (byte) 78,
    (byte) 38,
    (byte) 96 /*0x60*/,
    (byte) 86,
    (byte) 44,
    (byte) 115,
    (byte) 180,
    (byte) 51,
    (byte) 79,
    (byte) 68,
    (byte) 232,
    (byte) 215,
    (byte) 109,
    (byte) 3,
    (byte) 78,
    (byte) 149,
    (byte) 20,
    (byte) 116,
    (byte) 64 /*0x40*/,
    (byte) 89,
    (byte) 83,
    (byte) 28,
    (byte) 140,
    (byte) 96 /*0x60*/,
    (byte) 199,
    (byte) 233,
    (byte) 43,
    (byte) 15,
    (byte) 206,
    (byte) 170,
    (byte) 124,
    (byte) 200,
    (byte) 148,
    (byte) 106,
    (byte) 65,
    (byte) 92,
    (byte) 197,
    (byte) 45,
    (byte) 90,
    (byte) 12,
    (byte) 52,
    (byte) 33,
    (byte) 196,
    (byte) 94,
    (byte) 237,
    (byte) 73,
    (byte) 131,
    (byte) 145,
    (byte) 156,
    (byte) 155,
    (byte) 87,
    (byte) 60,
    (byte) 13,
    (byte) 201,
    (byte) 99,
    (byte) 250,
    (byte) 157,
    (byte) 68,
    (byte) 64 /*0x40*/,
    (byte) 191,
    (byte) 112 /*0x70*/,
    (byte) 122,
    (byte) 171,
    (byte) 249,
    (byte) 38,
    (byte) 150,
    (byte) 237,
    (byte) 59,
    (byte) 242,
    (byte) 90,
    (byte) 120,
    (byte) 128 /*0x80*/,
    (byte) 57,
    (byte) 195,
    (byte) 136,
    (byte) 212,
    (byte) 132,
    (byte) 67,
    (byte) 118,
    (byte) 164,
    (byte) 19,
    (byte) 109,
    (byte) 32 /*0x20*/,
    (byte) 139,
    (byte) 75,
    (byte) 224 /*0xE0*/,
    (byte) 22,
    (byte) 142,
    (byte) 7,
    (byte) 105,
    (byte) 65,
    (byte) 229,
    (byte) 46,
    (byte) 123,
    (byte) 206,
    (byte) 73,
    (byte) 249,
    (byte) 8,
    (byte) 223,
    (byte) 220,
    (byte) 142,
    (byte) 133,
    (byte) 128 /*0x80*/,
    (byte) 174,
    (byte) 97,
    (byte) 234,
    (byte) 99,
    (byte) 134,
    (byte) 118,
    (byte) 37,
    (byte) 242,
    (byte) 55,
    (byte) 111,
    (byte) 77,
    (byte) 75,
    (byte) 206,
    (byte) 37,
    (byte) 50,
    (byte) 32 /*0x20*/,
    (byte) 67,
    (byte) 14,
    (byte) 86,
    (byte) 58,
    (byte) 246,
    (byte) 130,
    (byte) 88,
    (byte) 68,
    (byte) 47,
    (byte) 134,
    (byte) 91,
    (byte) 240 /*0xF0*/,
    (byte) 156,
    (byte) 45,
    (byte) 101,
    (byte) 53,
    (byte) 246,
    (byte) 26,
    (byte) 152,
    (byte) 50,
    (byte) 79,
    (byte) 181,
    (byte) 18,
    (byte) 56,
    (byte) 240 /*0xF0*/,
    (byte) 6,
    (byte) 241,
    (byte) 5,
    (byte) 202,
    (byte) 238,
    (byte) 124,
    (byte) 7,
    (byte) 81,
    (byte) 147,
    (byte) 73,
    (byte) 161,
    (byte) 104,
    (byte) 49,
    (byte) 219,
    (byte) 208 /*0xD0*/,
    (byte) 187,
    (byte) 49,
    (byte) 90,
    (byte) 239,
    (byte) 218,
    (byte) 221,
    (byte) 175,
    (byte) 114,
    (byte) 73,
    (byte) 175,
    (byte) 201,
    (byte) 91,
    (byte) 72,
    (byte) 207,
    byte.MaxValue,
    (byte) 62,
    (byte) 40,
    (byte) 2,
    (byte) 220,
    (byte) 75,
    (byte) 116,
    (byte) 19,
    (byte) 47,
    (byte) 27,
    (byte) 16 /*0x10*/,
    (byte) 168,
    (byte) 126,
    (byte) 188,
    (byte) 78,
    (byte) 250,
    (byte) 174,
    (byte) 3,
    (byte) 123,
    (byte) 48 /*0x30*/,
    (byte) 195,
    (byte) 25,
    (byte) 253,
    (byte) 245,
    (byte) 218,
    (byte) 168,
    (byte) 154,
    (byte) 183,
    (byte) 178,
    (byte) 95,
    (byte) 197,
    (byte) 169,
    (byte) 8,
    (byte) 244,
    (byte) 25,
    (byte) 83,
    (byte) 146,
    (byte) 169,
    (byte) 197,
    (byte) 187,
    (byte) 14,
    (byte) 133,
    (byte) 250,
    (byte) 176 /*0xB0*/,
    (byte) 52,
    (byte) 109,
    (byte) 210,
    (byte) 220,
    (byte) 226,
    (byte) 168,
    (byte) 145,
    (byte) 88,
    (byte) 98,
    (byte) 102,
    (byte) 128 /*0x80*/,
    (byte) 88,
    (byte) 107,
    (byte) 54,
    (byte) 148,
    (byte) 205,
    (byte) 92,
    (byte) 13,
    (byte) 65,
    (byte) 170,
    (byte) 23,
    (byte) 199,
    (byte) 67,
    (byte) 63 /*0x3F*/,
    (byte) 149,
    (byte) 206,
    (byte) 74,
    (byte) 87,
    (byte) 5,
    (byte) 121,
    (byte) 183,
    (byte) 42,
    (byte) 167,
    (byte) 211,
    (byte) 92,
    (byte) 63 /*0x3F*/,
    (byte) 98,
    (byte) 183,
    (byte) 99,
    (byte) 94,
    (byte) 11,
    (byte) 237,
    (byte) 12,
    (byte) 123,
    (byte) 144 /*0x90*/,
    (byte) 155,
    (byte) 7,
    (byte) 200,
    (byte) 71,
    (byte) 221,
    (byte) 112 /*0x70*/,
    (byte) 106,
    (byte) 181,
    (byte) 125,
    (byte) 40,
    (byte) 51,
    (byte) 91,
    (byte) 137,
    (byte) 19,
    (byte) 245,
    (byte) 146,
    (byte) 74,
    (byte) 119,
    (byte) 215,
    (byte) 74,
    (byte) 29,
    (byte) 152,
    (byte) 105,
    (byte) 40,
    (byte) 65,
    (byte) 247,
    (byte) 55,
    (byte) 110,
    (byte) 131,
    (byte) 250,
    (byte) 93,
    (byte) 45,
    (byte) 35,
    (byte) 96 /*0x60*/,
    (byte) 34,
    (byte) 57,
    (byte) 237,
    (byte) 141,
    (byte) 225,
    (byte) 64 /*0x40*/,
    byte.MaxValue,
    (byte) 7,
    (byte) 126,
    (byte) 235,
    (byte) 160 /*0xA0*/,
    (byte) 111,
    (byte) 240 /*0xF0*/,
    (byte) 199,
    (byte) 217,
    (byte) 161,
    (byte) 246,
    (byte) 115,
    (byte) 229,
    (byte) 50,
    (byte) 57,
    (byte) 229,
    (byte) 238,
    (byte) 226,
    (byte) 159,
    (byte) 163
  };

  internal static int ssp_appserver_13137(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 139,
      (byte) 173,
      (byte) 116,
      (byte) 84,
      (byte) 167,
      (byte) 77,
      (byte) 138,
      (byte) 230,
      (byte) 85,
      (byte) 157,
      (byte) 152,
      (byte) 71,
      (byte) 223,
      (byte) 127 /*0x7F*/,
      (byte) 215,
      (byte) 71,
      (byte) 169,
      (byte) 47,
      (byte) 138,
      (byte) 59,
      (byte) 45,
      (byte) 219,
      (byte) 218,
      (byte) 52,
      (byte) 97,
      (byte) 251,
      (byte) 27,
      (byte) 93,
      (byte) 109,
      (byte) 243,
      (byte) 218,
      (byte) 121,
      (byte) 243,
      (byte) 169,
      (byte) 171,
      (byte) 153,
      (byte) 219,
      byte.MaxValue,
      (byte) 89,
      (byte) 116,
      (byte) 245,
      (byte) 155,
      (byte) 168,
      (byte) 117,
      (byte) 160 /*0xA0*/,
      (byte) 229,
      (byte) 241,
      (byte) 68
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 69,
      (byte) 114,
      (byte) 234,
      (byte) 129,
      (byte) 133,
      (byte) 227,
      (byte) 232,
      (byte) 52,
      (byte) 191,
      (byte) 84,
      (byte) 150,
      (byte) 158,
      (byte) 131,
      (byte) 125,
      (byte) 92,
      (byte) 49,
      (byte) 0,
      (byte) 227,
      (byte) 19,
      (byte) 225,
      (byte) 99,
      (byte) 90,
      (byte) 251,
      (byte) 36,
      (byte) 172,
      (byte) 135,
      (byte) 185,
      (byte) 167,
      byte.MaxValue,
      (byte) 25,
      (byte) 132,
      (byte) 13,
      (byte) 129,
      (byte) 40,
      (byte) 18,
      (byte) 222,
      (byte) 216,
      (byte) 43,
      (byte) 176 /*0xB0*/,
      (byte) 34,
      (byte) 171,
      (byte) 246,
      (byte) 56,
      (byte) 106,
      (byte) 51,
      (byte) 175,
      (byte) 58,
      (byte) 239
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[24];
    byte[] response2 = new byte[24];
    Array.Copy((Array) sc_13136.sspq, 0, (Array) numArray2, 0, 24);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13136.sspr, 0, (Array) numArray2, 0, 24);
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

  internal static string ssp_appserver_13138()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[65];
      byte[] numArray2 = new byte[55]
      {
        (byte) 100,
        (byte) 147,
        (byte) 124,
        (byte) 10,
        (byte) 214,
        (byte) 113,
        (byte) 236,
        (byte) 173,
        (byte) 11,
        (byte) 62,
        (byte) 113,
        (byte) 8,
        (byte) 251,
        (byte) 197,
        (byte) 154,
        (byte) 12,
        (byte) 173,
        (byte) 66,
        (byte) 199,
        (byte) 35,
        (byte) 71,
        (byte) 177,
        (byte) 128 /*0x80*/,
        (byte) 145,
        (byte) 187,
        (byte) 112 /*0x70*/,
        (byte) 173,
        (byte) 21,
        (byte) 156,
        (byte) 248,
        (byte) 37,
        (byte) 59,
        (byte) 15,
        (byte) 125,
        (byte) 110,
        (byte) 172,
        (byte) 118,
        (byte) 160 /*0xA0*/,
        (byte) 91,
        (byte) 187,
        (byte) 140,
        (byte) 180,
        (byte) 184,
        (byte) 225,
        (byte) 226,
        (byte) 45,
        (byte) 217,
        (byte) 120,
        (byte) 209,
        (byte) 158,
        (byte) 39,
        (byte) 210,
        (byte) 126,
        (byte) 111,
        (byte) 235
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 10,
        (byte) 91,
        (byte) 82,
        (byte) 68,
        (byte) 49,
        (byte) 213,
        (byte) 200,
        (byte) 55,
        (byte) 34,
        (byte) 127 /*0x7F*/,
        (byte) 36,
        (byte) 122,
        (byte) 19,
        (byte) 249,
        (byte) 220,
        (byte) 23,
        (byte) 125,
        (byte) 182,
        (byte) 35,
        (byte) 205,
        (byte) 140,
        (byte) 49,
        (byte) 183,
        (byte) 216,
        (byte) 61,
        (byte) 240 /*0xF0*/,
        (byte) 7,
        (byte) 110,
        (byte) 174,
        (byte) 90,
        (byte) 124,
        (byte) 78,
        (byte) 31 /*0x1F*/,
        (byte) 169,
        (byte) 212,
        (byte) 175,
        (byte) 239,
        (byte) 48 /*0x30*/,
        (byte) 48 /*0x30*/,
        (byte) 76,
        (byte) 184,
        (byte) 158,
        (byte) 34,
        (byte) 55,
        (byte) 208 /*0xD0*/,
        (byte) 132,
        (byte) 11,
        (byte) 101,
        (byte) 25,
        (byte) 139,
        (byte) 223,
        (byte) 242,
        (byte) 141,
        (byte) 244,
        (byte) 254
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[10];
      numArray4[9] = (byte) 248;
      numArray4[0] = (byte) 235;
      numArray4[2] = (byte) 102;
      numArray4[1] = (byte) 78;
      numArray4[4] = (byte) 215;
      numArray4[5] = (byte) 16 /*0x10*/;
      numArray4[6] = (byte) 19;
      numArray4[7] = (byte) 24;
      numArray4[3] = (byte) 196;
      numArray4[8] = (byte) 53;
      byte[] numArray5 = new byte[10]
      {
        (byte) 106,
        (byte) 130,
        (byte) 23,
        (byte) 17,
        (byte) 166,
        (byte) 162,
        (byte) 20,
        (byte) 123,
        (byte) 215,
        (byte) 246
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[65];
    byte[] numArray7 = new byte[55]
    {
      (byte) 151,
      (byte) 52,
      (byte) 67,
      (byte) 254,
      (byte) 208 /*0xD0*/,
      (byte) 82,
      (byte) 189,
      (byte) 160 /*0xA0*/,
      (byte) 146,
      (byte) 187,
      (byte) 153,
      (byte) 138,
      (byte) 81,
      (byte) 245,
      (byte) 247,
      (byte) 181,
      (byte) 163,
      (byte) 164,
      (byte) 200,
      (byte) 173,
      (byte) 52,
      (byte) 91,
      (byte) 33,
      (byte) 228,
      (byte) 8,
      (byte) 26,
      (byte) 235,
      (byte) 114,
      (byte) 216,
      (byte) 88,
      (byte) 186,
      (byte) 116,
      (byte) 61,
      (byte) 192 /*0xC0*/,
      (byte) 206,
      (byte) 143,
      (byte) 147,
      (byte) 18,
      (byte) 66,
      (byte) 198,
      (byte) 119,
      (byte) 88,
      (byte) 235,
      (byte) 155,
      (byte) 83,
      (byte) 10,
      (byte) 187,
      (byte) 241,
      (byte) 20,
      (byte) 225,
      (byte) 238,
      (byte) 84,
      (byte) 214,
      (byte) 0,
      (byte) 89
    };
    byte[] numArray8 = new byte[55]
    {
      (byte) 166,
      (byte) 216,
      (byte) 31 /*0x1F*/,
      (byte) 41,
      (byte) 150,
      (byte) 55,
      (byte) 124,
      (byte) 134,
      (byte) 34,
      (byte) 230,
      (byte) 224 /*0xE0*/,
      (byte) 36,
      (byte) 7,
      (byte) 32 /*0x20*/,
      (byte) 215,
      (byte) 182,
      (byte) 234,
      (byte) 225,
      (byte) 112 /*0x70*/,
      (byte) 63 /*0x3F*/,
      (byte) 237,
      (byte) 69,
      (byte) 53,
      (byte) 242,
      (byte) 187,
      (byte) 173,
      (byte) 93,
      (byte) 147,
      (byte) 71,
      (byte) 56,
      (byte) 91,
      (byte) 42,
      (byte) 188,
      (byte) 67,
      (byte) 210,
      (byte) 230,
      (byte) 111,
      (byte) 77,
      (byte) 208 /*0xD0*/,
      (byte) 1,
      (byte) 37,
      (byte) 32 /*0x20*/,
      (byte) 87,
      (byte) 194,
      (byte) 47,
      (byte) 4,
      (byte) 111,
      (byte) 92,
      (byte) 164,
      (byte) 85,
      (byte) 105,
      (byte) 54,
      (byte) 124,
      (byte) 34,
      (byte) 145
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[10]
    {
      (byte) 95,
      (byte) 245,
      (byte) 182,
      (byte) 102,
      (byte) 74,
      (byte) 200,
      (byte) 57,
      (byte) 1,
      (byte) 37,
      (byte) 77
    };
    byte[] numArray10 = new byte[10]
    {
      (byte) 76,
      (byte) 58,
      (byte) 57,
      (byte) 223,
      (byte) 92,
      (byte) 140,
      (byte) 63 /*0x3F*/,
      byte.MaxValue,
      (byte) 38,
      (byte) 40
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 10);
    for (int index = 0; index < 10; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_13139()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[14];
      byte[] numArray2 = new byte[14];
      numArray2[2] = (byte) 2;
      numArray2[1] = (byte) 98;
      numArray2[5] = (byte) 111;
      numArray2[3] = (byte) 53;
      numArray2[4] = (byte) 220;
      numArray2[0] = (byte) 145;
      numArray2[6] = (byte) 134;
      numArray2[7] = (byte) 144 /*0x90*/;
      numArray2[11] = (byte) 93;
      numArray2[9] = (byte) 95;
      numArray2[10] = (byte) 249;
      numArray2[8] = (byte) 253;
      numArray2[12] = (byte) 247;
      numArray2[13] = (byte) 106;
      byte[] numArray3 = new byte[14]
      {
        (byte) 73,
        (byte) 180,
        (byte) 22,
        (byte) 16 /*0x10*/,
        (byte) 93,
        (byte) 221,
        (byte) 71,
        (byte) 120,
        (byte) 236,
        (byte) 161,
        (byte) 168,
        (byte) 95,
        (byte) 38,
        (byte) 109
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 14);
      for (int index = 0; index < 14; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[14];
    byte[] numArray5 = new byte[14];
    numArray5[3] = (byte) 93;
    numArray5[1] = (byte) 172;
    numArray5[8] = (byte) 203;
    numArray5[2] = (byte) 191;
    numArray5[4] = (byte) 241;
    numArray5[5] = (byte) 211;
    numArray5[6] = (byte) 202;
    numArray5[9] = (byte) 141;
    numArray5[12] = (byte) 86;
    numArray5[0] = (byte) 203;
    numArray5[10] = (byte) 206;
    numArray5[11] = (byte) 248;
    numArray5[7] = (byte) 125;
    numArray5[13] = (byte) 108;
    byte[] numArray6 = new byte[14]
    {
      (byte) 85,
      (byte) 228,
      (byte) 94,
      (byte) 131,
      (byte) 143,
      (byte) 54,
      (byte) 23,
      (byte) 178,
      (byte) 72,
      (byte) 234,
      (byte) 29,
      (byte) 125,
      (byte) 130,
      (byte) 151
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 14);
    for (int index = 0; index < 14; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13140()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[66];
      byte[] numArray2 = new byte[55]
      {
        (byte) 64 /*0x40*/,
        (byte) 53,
        (byte) 46,
        (byte) 177,
        (byte) 55,
        (byte) 12,
        (byte) 115,
        (byte) 202,
        (byte) 114,
        (byte) 233,
        (byte) 199,
        (byte) 238,
        (byte) 101,
        (byte) 36,
        (byte) 53,
        (byte) 242,
        (byte) 64 /*0x40*/,
        (byte) 191,
        (byte) 227,
        byte.MaxValue,
        (byte) 168,
        (byte) 172,
        byte.MaxValue,
        (byte) 55,
        (byte) 191,
        (byte) 209,
        (byte) 233,
        (byte) 27,
        (byte) 48 /*0x30*/,
        (byte) 193,
        (byte) 48 /*0x30*/,
        (byte) 14,
        (byte) 161,
        (byte) 108,
        (byte) 183,
        (byte) 33,
        (byte) 61,
        (byte) 59,
        (byte) 152,
        (byte) 194,
        (byte) 39,
        (byte) 187,
        (byte) 157,
        (byte) 64 /*0x40*/,
        (byte) 203,
        (byte) 253,
        (byte) 135,
        (byte) 79,
        (byte) 161,
        (byte) 176 /*0xB0*/,
        (byte) 200,
        (byte) 140,
        (byte) 217,
        (byte) 236,
        (byte) 185
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 40,
        (byte) 164,
        (byte) 124,
        (byte) 229,
        (byte) 50,
        (byte) 240 /*0xF0*/,
        (byte) 201,
        (byte) 209,
        (byte) 252,
        (byte) 224 /*0xE0*/,
        (byte) 188,
        (byte) 131,
        (byte) 185,
        (byte) 158,
        (byte) 196,
        (byte) 202,
        (byte) 13,
        (byte) 201,
        (byte) 58,
        (byte) 246,
        (byte) 220,
        (byte) 149,
        (byte) 115,
        (byte) 35,
        (byte) 157,
        (byte) 221,
        (byte) 207,
        (byte) 122,
        (byte) 13,
        (byte) 117,
        (byte) 2,
        (byte) 168,
        (byte) 16 /*0x10*/,
        (byte) 216,
        (byte) 35,
        (byte) 155,
        (byte) 248,
        (byte) 74,
        (byte) 136,
        (byte) 78,
        (byte) 226,
        (byte) 79,
        (byte) 38,
        (byte) 178,
        (byte) 14,
        (byte) 141,
        (byte) 62,
        (byte) 111,
        (byte) 188,
        (byte) 254,
        (byte) 239,
        (byte) 122,
        (byte) 14,
        (byte) 5,
        (byte) 16 /*0x10*/
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[11]
      {
        (byte) 246,
        (byte) 241,
        (byte) 101,
        (byte) 35,
        (byte) 88,
        (byte) 154,
        (byte) 177,
        (byte) 184,
        (byte) 184,
        (byte) 77,
        (byte) 248
      };
      byte[] numArray5 = new byte[11]
      {
        (byte) 182,
        (byte) 249,
        (byte) 13,
        (byte) 32 /*0x20*/,
        (byte) 163,
        (byte) 39,
        (byte) 175,
        (byte) 242,
        (byte) 40,
        (byte) 185,
        (byte) 177
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 11);
      for (int index = 0; index < 11; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[53];
      byte[] response = new byte[53];
      Array.Copy((Array) sc_13136.sspq, 24, (Array) numArray6, 0, 53);
      key.Query(true, 335, numArray6, response);
      Array.Copy((Array) sc_13136.sspr, 24, (Array) numArray6, 0, 53);
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
    byte[] numArray7 = new byte[66];
    byte[] numArray8 = new byte[55];
    numArray8[7] = (byte) 55;
    numArray8[16 /*0x10*/] = (byte) 123;
    numArray8[28] = (byte) 114;
    numArray8[3] = (byte) 129;
    numArray8[4] = (byte) 8;
    numArray8[8] = (byte) 88;
    numArray8[6] = (byte) 34;
    numArray8[49] = (byte) 215;
    numArray8[54] = (byte) 177;
    numArray8[9] = (byte) 18;
    numArray8[10] = (byte) 187;
    numArray8[52] = (byte) 95;
    numArray8[12] = (byte) 235;
    numArray8[13] = (byte) 133;
    numArray8[26] = (byte) 8;
    numArray8[0] = (byte) 109;
    numArray8[30] = (byte) 22;
    numArray8[43] = (byte) 28;
    numArray8[18] = (byte) 230;
    numArray8[19] = (byte) 160 /*0xA0*/;
    numArray8[39] = (byte) 2;
    numArray8[1] = (byte) 187;
    numArray8[22] = (byte) 26;
    numArray8[23] = (byte) 23;
    numArray8[20] = (byte) 117;
    numArray8[15] = (byte) 61;
    numArray8[36] = (byte) 114;
    numArray8[29] = (byte) 143;
    numArray8[45] = (byte) 210;
    numArray8[42] = (byte) 49;
    numArray8[5] = (byte) 202;
    numArray8[27] = (byte) 52;
    numArray8[33] = (byte) 102;
    numArray8[21] = (byte) 188;
    numArray8[34] = (byte) 141;
    numArray8[35] = (byte) 7;
    numArray8[46] = (byte) 242;
    numArray8[37] = (byte) 55;
    numArray8[31 /*0x1F*/] = (byte) 73;
    numArray8[50] = (byte) 8;
    numArray8[40] = (byte) 97;
    numArray8[41] = (byte) 63 /*0x3F*/;
    numArray8[47] = (byte) 147;
    numArray8[24] = (byte) 21;
    numArray8[44] = (byte) 96 /*0x60*/;
    numArray8[25] = (byte) 240 /*0xF0*/;
    numArray8[2] = (byte) 243;
    numArray8[32 /*0x20*/] = (byte) 153;
    numArray8[38] = (byte) 12;
    numArray8[48 /*0x30*/] = (byte) 129;
    numArray8[14] = (byte) 54;
    numArray8[51] = (byte) 248;
    numArray8[11] = (byte) 183;
    numArray8[53] = (byte) 167;
    numArray8[17] = (byte) 159;
    byte[] numArray9 = new byte[55]
    {
      (byte) 158,
      (byte) 227,
      (byte) 18,
      (byte) 217,
      (byte) 156,
      (byte) 57,
      (byte) 35,
      (byte) 80 /*0x50*/,
      (byte) 67,
      (byte) 198,
      (byte) 228,
      (byte) 45,
      (byte) 8,
      (byte) 187,
      (byte) 31 /*0x1F*/,
      (byte) 116,
      (byte) 78,
      (byte) 175,
      (byte) 62,
      (byte) 176 /*0xB0*/,
      (byte) 196,
      (byte) 187,
      (byte) 171,
      (byte) 196,
      (byte) 113,
      (byte) 91,
      (byte) 24,
      (byte) 239,
      (byte) 20,
      (byte) 60,
      (byte) 60,
      (byte) 179,
      (byte) 47,
      (byte) 226,
      (byte) 50,
      (byte) 108,
      (byte) 199,
      (byte) 43,
      (byte) 41,
      (byte) 123,
      (byte) 236,
      (byte) 73,
      (byte) 197,
      (byte) 251,
      (byte) 47,
      (byte) 162,
      (byte) 227,
      (byte) 178,
      (byte) 140,
      (byte) 168,
      (byte) 2,
      (byte) 184,
      (byte) 244,
      (byte) 100,
      (byte) 61
    };
    key.Query(true, 335, numArray8, numArray8);
    Array.Copy((Array) numArray8, 0, (Array) numArray7, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray7[index] ^= numArray9[index];
    byte[] numArray10 = new byte[11]
    {
      (byte) 37,
      (byte) 41,
      (byte) 27,
      (byte) 77,
      (byte) 58,
      (byte) 42,
      (byte) 66,
      (byte) 221,
      (byte) 135,
      (byte) 43,
      (byte) 207
    };
    byte[] numArray11 = new byte[11]
    {
      (byte) 5,
      (byte) 100,
      (byte) 135,
      (byte) 19,
      (byte) 6,
      (byte) 71,
      (byte) 144 /*0x90*/,
      (byte) 74,
      (byte) 215,
      (byte) 165,
      (byte) 10
    };
    key.Query(true, 335, numArray10, numArray10);
    Array.Copy((Array) numArray10, 0, (Array) numArray7, 55, 11);
    for (int index = 0; index < 11; ++index)
      numArray7[index + 55] ^= numArray11[index];
    return Encoding.UTF8.GetString(numArray7);
  }

  internal static string ssp_appserver_13141()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[14];
      byte[] numArray2 = new byte[14]
      {
        (byte) 159,
        (byte) 162,
        (byte) 204,
        (byte) 187,
        (byte) 34,
        (byte) 220,
        (byte) 200,
        (byte) 142,
        (byte) 143,
        (byte) 217,
        (byte) 95,
        (byte) 141,
        (byte) 195,
        (byte) 184
      };
      byte[] numArray3 = new byte[14]
      {
        (byte) 28,
        (byte) 89,
        (byte) 242,
        (byte) 52,
        (byte) 223,
        (byte) 34,
        (byte) 84,
        (byte) 126,
        (byte) 59,
        (byte) 244,
        (byte) 80 /*0x50*/,
        (byte) 153,
        (byte) 131,
        (byte) 18
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 14);
      for (int index = 0; index < 14; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[14];
    byte[] numArray5 = new byte[14]
    {
      (byte) 180,
      (byte) 195,
      (byte) 111,
      (byte) 161,
      (byte) 216,
      (byte) 243,
      (byte) 56,
      (byte) 80 /*0x50*/,
      (byte) 37,
      (byte) 67,
      (byte) 102,
      (byte) 136,
      (byte) 158,
      (byte) 125
    };
    byte[] numArray6 = new byte[14];
    numArray6[9] = (byte) 161;
    numArray6[1] = (byte) 135;
    numArray6[2] = (byte) 245;
    numArray6[4] = (byte) 126;
    numArray6[12] = (byte) 44;
    numArray6[5] = (byte) 128 /*0x80*/;
    numArray6[6] = (byte) 139;
    numArray6[10] = (byte) 194;
    numArray6[7] = (byte) 199;
    numArray6[3] = (byte) 67;
    numArray6[0] = (byte) 213;
    numArray6[11] = (byte) 212;
    numArray6[8] = (byte) 163;
    numArray6[13] = (byte) 51;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 14);
    for (int index = 0; index < 14; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[18];
    byte[] response = new byte[18];
    Array.Copy((Array) sc_13136.sspq, 77, (Array) numArray7, 0, 18);
    key.Query(true, 335, numArray7, response);
    Array.Copy((Array) sc_13136.sspr, 77, (Array) numArray7, 0, 18);
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

  internal static int ssp_appserver_13142(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[26] = (byte) 251;
    sourceArray1[14] = (byte) 229;
    sourceArray1[2] = (byte) 122;
    sourceArray1[18] = (byte) 18;
    sourceArray1[4] = (byte) 252;
    sourceArray1[5] = (byte) 18;
    sourceArray1[1] = (byte) 237;
    sourceArray1[29] = (byte) 23;
    sourceArray1[23] = (byte) 83;
    sourceArray1[7] = (byte) 77;
    sourceArray1[10] = (byte) 179;
    sourceArray1[39] = (byte) 178;
    sourceArray1[12] = (byte) 30;
    sourceArray1[13] = (byte) 77;
    sourceArray1[38] = (byte) 7;
    sourceArray1[15] = (byte) 8;
    sourceArray1[3] = (byte) 175;
    sourceArray1[17] = (byte) 136;
    sourceArray1[34] = (byte) 131;
    sourceArray1[19] = (byte) 115;
    sourceArray1[20] = (byte) 253;
    sourceArray1[21] = (byte) 41;
    sourceArray1[22] = (byte) 9;
    sourceArray1[9] = (byte) 94;
    sourceArray1[37] = (byte) 184;
    sourceArray1[25] = (byte) 45;
    sourceArray1[36] = (byte) 26;
    sourceArray1[24] = (byte) 8;
    sourceArray1[11] = (byte) 166;
    sourceArray1[30] = (byte) 7;
    sourceArray1[27] = (byte) 148;
    sourceArray1[31 /*0x1F*/] = (byte) 230;
    sourceArray1[32 /*0x20*/] = (byte) 7;
    sourceArray1[28] = (byte) 153;
    sourceArray1[16 /*0x10*/] = (byte) 21;
    sourceArray1[35] = (byte) 181;
    sourceArray1[45] = (byte) 85;
    sourceArray1[44] = (byte) 184;
    sourceArray1[0] = (byte) 149;
    sourceArray1[33] = (byte) 49;
    sourceArray1[40] = (byte) 208 /*0xD0*/;
    sourceArray1[41] = (byte) 122;
    sourceArray1[6] = (byte) 6;
    sourceArray1[43] = (byte) 53;
    sourceArray1[8] = (byte) 127 /*0x7F*/;
    sourceArray1[42] = (byte) 47;
    sourceArray1[46] = (byte) 122;
    sourceArray1[47] = (byte) 20;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[39] = (byte) 135;
    sourceArray2[10] = (byte) 196;
    sourceArray2[2] = (byte) 183;
    sourceArray2[3] = (byte) 42;
    sourceArray2[7] = (byte) 50;
    sourceArray2[47] = (byte) 161;
    sourceArray2[14] = (byte) 40;
    sourceArray2[1] = (byte) 158;
    sourceArray2[8] = (byte) 189;
    sourceArray2[24] = (byte) 150;
    sourceArray2[13] = (byte) 75;
    sourceArray2[11] = (byte) 70;
    sourceArray2[9] = (byte) 84;
    sourceArray2[40] = (byte) 228;
    sourceArray2[35] = (byte) 128 /*0x80*/;
    sourceArray2[15] = (byte) 182;
    sourceArray2[41] = (byte) 24;
    sourceArray2[17] = (byte) 122;
    sourceArray2[36] = (byte) 0;
    sourceArray2[6] = (byte) 191;
    sourceArray2[5] = (byte) 8;
    sourceArray2[21] = (byte) 3;
    sourceArray2[22] = (byte) 207;
    sourceArray2[23] = (byte) 159;
    sourceArray2[29] = (byte) 85;
    sourceArray2[37] = (byte) 146;
    sourceArray2[46] = (byte) 233;
    sourceArray2[27] = (byte) 24;
    sourceArray2[45] = (byte) 67;
    sourceArray2[38] = (byte) 70;
    sourceArray2[30] = (byte) 6;
    sourceArray2[31 /*0x1F*/] = (byte) 23;
    sourceArray2[32 /*0x20*/] = (byte) 109;
    sourceArray2[33] = (byte) 118;
    sourceArray2[34] = (byte) 122;
    sourceArray2[12] = (byte) 99;
    sourceArray2[0] = (byte) 205;
    sourceArray2[19] = (byte) 122;
    sourceArray2[16 /*0x10*/] = (byte) 194;
    sourceArray2[4] = (byte) 170;
    sourceArray2[18] = (byte) 63 /*0x3F*/;
    sourceArray2[44] = (byte) 190;
    sourceArray2[42] = (byte) 148;
    sourceArray2[43] = (byte) 44;
    sourceArray2[26] = (byte) 189;
    sourceArray2[20] = (byte) 207;
    sourceArray2[28] = (byte) 182;
    sourceArray2[25] = (byte) 19;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_13143()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[67];
      byte[] numArray2 = new byte[55];
      numArray2[15] = (byte) 27;
      numArray2[12] = (byte) 158;
      numArray2[39] = (byte) 177;
      numArray2[3] = (byte) 196;
      numArray2[4] = (byte) 49;
      numArray2[20] = (byte) 247;
      numArray2[10] = (byte) 144 /*0x90*/;
      numArray2[7] = (byte) 24;
      numArray2[31 /*0x1F*/] = (byte) 158;
      numArray2[9] = (byte) 152;
      numArray2[34] = (byte) 43;
      numArray2[11] = (byte) 0;
      numArray2[48 /*0x30*/] = (byte) 128 /*0x80*/;
      numArray2[5] = (byte) 67;
      numArray2[14] = (byte) 127 /*0x7F*/;
      numArray2[33] = (byte) 47;
      numArray2[16 /*0x10*/] = (byte) 181;
      numArray2[17] = (byte) 64 /*0x40*/;
      numArray2[8] = (byte) 221;
      numArray2[19] = (byte) 221;
      numArray2[27] = (byte) 137;
      numArray2[21] = (byte) 247;
      numArray2[18] = (byte) 26;
      numArray2[23] = (byte) 123;
      numArray2[24] = (byte) 76;
      numArray2[25] = (byte) 174;
      numArray2[26] = (byte) 168;
      numArray2[13] = (byte) 130;
      numArray2[45] = (byte) 180;
      numArray2[29] = (byte) 230;
      numArray2[28] = (byte) 251;
      numArray2[30] = (byte) 13;
      numArray2[0] = (byte) 28;
      numArray2[52] = (byte) 166;
      numArray2[2] = (byte) 95;
      numArray2[35] = (byte) 90;
      numArray2[22] = (byte) 150;
      numArray2[41] = (byte) 228;
      numArray2[38] = (byte) 0;
      numArray2[43] = (byte) 2;
      numArray2[40] = (byte) 76;
      numArray2[36] = (byte) 230;
      numArray2[42] = (byte) 71;
      numArray2[32 /*0x20*/] = (byte) 148;
      numArray2[44] = (byte) 243;
      numArray2[37] = (byte) 247;
      numArray2[46] = (byte) 73;
      numArray2[47] = (byte) 111;
      numArray2[1] = (byte) 105;
      numArray2[49] = (byte) 133;
      numArray2[50] = (byte) 20;
      numArray2[51] = (byte) 101;
      numArray2[6] = (byte) 98;
      numArray2[53] = (byte) 137;
      numArray2[54] = (byte) 36;
      byte[] numArray3 = new byte[55]
      {
        (byte) 115,
        (byte) 31 /*0x1F*/,
        (byte) 163,
        (byte) 125,
        (byte) 62,
        (byte) 119,
        (byte) 92,
        (byte) 43,
        (byte) 96 /*0x60*/,
        (byte) 222,
        (byte) 97,
        (byte) 113,
        (byte) 202,
        (byte) 62,
        (byte) 88,
        (byte) 186,
        (byte) 159,
        (byte) 1,
        (byte) 120,
        (byte) 102,
        (byte) 10,
        (byte) 97,
        (byte) 68,
        (byte) 157,
        (byte) 171,
        (byte) 169,
        (byte) 70,
        (byte) 233,
        (byte) 136,
        (byte) 190,
        (byte) 28,
        byte.MaxValue,
        (byte) 30,
        (byte) 83,
        (byte) 159,
        (byte) 63 /*0x3F*/,
        (byte) 61,
        (byte) 115,
        (byte) 246,
        (byte) 155,
        (byte) 155,
        (byte) 248,
        (byte) 90,
        (byte) 27,
        (byte) 173,
        (byte) 252,
        (byte) 2,
        (byte) 186,
        (byte) 72,
        (byte) 179,
        (byte) 47,
        (byte) 186,
        (byte) 15,
        (byte) 229,
        (byte) 189
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[12]
      {
        (byte) 91,
        (byte) 223,
        (byte) 29,
        (byte) 98,
        (byte) 209,
        (byte) 168,
        (byte) 36,
        (byte) 227,
        (byte) 249,
        (byte) 97,
        (byte) 99,
        (byte) 79
      };
      byte[] numArray5 = new byte[12]
      {
        (byte) 95,
        (byte) 165,
        (byte) 12,
        (byte) 227,
        (byte) 191,
        (byte) 132,
        (byte) 43,
        (byte) 115,
        (byte) 210,
        (byte) 127 /*0x7F*/,
        (byte) 69,
        (byte) 97
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 12);
      for (int index = 0; index < 12; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[67];
    byte[] numArray7 = new byte[55]
    {
      (byte) 192 /*0xC0*/,
      (byte) 82,
      (byte) 30,
      (byte) 76,
      (byte) 96 /*0x60*/,
      (byte) 29,
      (byte) 134,
      (byte) 79,
      (byte) 138,
      (byte) 31 /*0x1F*/,
      (byte) 173,
      (byte) 37,
      (byte) 122,
      (byte) 130,
      (byte) 187,
      (byte) 18,
      (byte) 250,
      (byte) 146,
      (byte) 71,
      (byte) 166,
      (byte) 160 /*0xA0*/,
      (byte) 96 /*0x60*/,
      (byte) 180,
      (byte) 161,
      (byte) 30,
      (byte) 43,
      (byte) 147,
      (byte) 8,
      (byte) 231,
      (byte) 119,
      (byte) 201,
      (byte) 189,
      (byte) 28,
      (byte) 25,
      (byte) 100,
      (byte) 235,
      (byte) 218,
      (byte) 196,
      (byte) 19,
      (byte) 33,
      (byte) 212,
      (byte) 144 /*0x90*/,
      (byte) 147,
      (byte) 66,
      (byte) 35,
      (byte) 131,
      (byte) 170,
      (byte) 96 /*0x60*/,
      (byte) 61,
      (byte) 78,
      (byte) 105,
      (byte) 189,
      (byte) 86,
      (byte) 140,
      (byte) 157
    };
    byte[] numArray8 = new byte[55]
    {
      (byte) 214,
      (byte) 180,
      (byte) 245,
      (byte) 128 /*0x80*/,
      byte.MaxValue,
      (byte) 3,
      (byte) 192 /*0xC0*/,
      (byte) 40,
      (byte) 143,
      (byte) 65,
      (byte) 37,
      (byte) 63 /*0x3F*/,
      (byte) 133,
      (byte) 80 /*0x50*/,
      (byte) 47,
      (byte) 209,
      (byte) 251,
      (byte) 208 /*0xD0*/,
      (byte) 127 /*0x7F*/,
      (byte) 58,
      (byte) 108,
      (byte) 172,
      (byte) 93,
      (byte) 103,
      (byte) 108,
      (byte) 197,
      (byte) 39,
      (byte) 42,
      (byte) 236,
      (byte) 33,
      (byte) 45,
      (byte) 127 /*0x7F*/,
      (byte) 245,
      (byte) 103,
      (byte) 114,
      (byte) 30,
      (byte) 130,
      (byte) 142,
      (byte) 136,
      (byte) 118,
      (byte) 95,
      (byte) 29,
      (byte) 180,
      (byte) 38,
      (byte) 172,
      (byte) 150,
      (byte) 232,
      (byte) 167,
      (byte) 214,
      (byte) 241,
      (byte) 197,
      (byte) 10,
      (byte) 149,
      (byte) 214,
      (byte) 43
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[12]
    {
      (byte) 124,
      (byte) 45,
      (byte) 52,
      (byte) 138,
      (byte) 126,
      (byte) 104,
      (byte) 19,
      (byte) 86,
      (byte) 240 /*0xF0*/,
      (byte) 8,
      (byte) 96 /*0x60*/,
      (byte) 29
    };
    byte[] numArray10 = new byte[12];
    numArray10[10] = (byte) 125;
    numArray10[0] = (byte) 39;
    numArray10[1] = (byte) 231;
    numArray10[4] = (byte) 172;
    numArray10[5] = (byte) 150;
    numArray10[6] = (byte) 70;
    numArray10[2] = (byte) 127 /*0x7F*/;
    numArray10[7] = (byte) 134;
    numArray10[8] = (byte) 250;
    numArray10[9] = (byte) 56;
    numArray10[3] = (byte) 39;
    numArray10[11] = (byte) 186;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 12);
    for (int index = 0; index < 12; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_13144()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[14];
      byte[] numArray2 = new byte[14];
      numArray2[5] = (byte) 76;
      numArray2[11] = (byte) 39;
      numArray2[12] = (byte) 128 /*0x80*/;
      numArray2[13] = (byte) 235;
      numArray2[4] = (byte) 4;
      numArray2[6] = (byte) 93;
      numArray2[3] = (byte) 124;
      numArray2[8] = (byte) 223;
      numArray2[2] = (byte) 144 /*0x90*/;
      numArray2[9] = (byte) 150;
      numArray2[10] = (byte) 86;
      numArray2[0] = (byte) 12;
      numArray2[1] = (byte) 102;
      numArray2[7] = (byte) 150;
      byte[] numArray3 = new byte[14]
      {
        (byte) 163,
        (byte) 79,
        (byte) 44,
        (byte) 15,
        (byte) 206,
        (byte) 190,
        (byte) 231,
        (byte) 238,
        (byte) 195,
        (byte) 41,
        (byte) 173,
        (byte) 93,
        (byte) 184,
        (byte) 170
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 14);
      for (int index = 0; index < 14; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[14];
    byte[] numArray5 = new byte[14]
    {
      (byte) 179,
      (byte) 207,
      (byte) 4,
      (byte) 74,
      (byte) 137,
      (byte) 120,
      (byte) 228,
      (byte) 24,
      (byte) 131,
      (byte) 219,
      (byte) 159,
      (byte) 158,
      (byte) 143,
      (byte) 123
    };
    byte[] numArray6 = new byte[14];
    numArray6[9] = (byte) 187;
    numArray6[10] = (byte) 217;
    numArray6[2] = (byte) 86;
    numArray6[3] = (byte) 217;
    numArray6[5] = (byte) 211;
    numArray6[8] = (byte) 78;
    numArray6[6] = (byte) 16 /*0x10*/;
    numArray6[7] = (byte) 127 /*0x7F*/;
    numArray6[4] = (byte) 66;
    numArray6[11] = (byte) 213;
    numArray6[0] = (byte) 156;
    numArray6[1] = (byte) 155;
    numArray6[12] = (byte) 209;
    numArray6[13] = (byte) 168;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 14);
    for (int index = 0; index < 14; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13145()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[6];
      byte[] numArray2 = new byte[6]
      {
        (byte) 45,
        (byte) 62,
        (byte) 163,
        (byte) 88,
        (byte) 123,
        (byte) 161
      };
      byte[] numArray3 = new byte[6]
      {
        (byte) 161,
        (byte) 226,
        (byte) 32 /*0x20*/,
        (byte) 64 /*0x40*/,
        (byte) 253,
        (byte) 15
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 6);
      for (int index = 0; index < 6; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[6];
    byte[] numArray5 = new byte[6]
    {
      (byte) 63 /*0x3F*/,
      (byte) 164,
      (byte) 70,
      (byte) 166,
      (byte) 181,
      (byte) 113
    };
    byte[] numArray6 = new byte[6];
    numArray6[2] = (byte) 60;
    numArray6[1] = (byte) 77;
    numArray6[3] = (byte) 84;
    numArray6[0] = (byte) 140;
    numArray6[4] = (byte) 57;
    numArray6[5] = (byte) 126;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 6);
    for (int index = 0; index < 6; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[34];
    byte[] response = new byte[34];
    Array.Copy((Array) sc_13136.sspq, 95, (Array) numArray7, 0, 34);
    key.Query(true, 335, numArray7, response);
    Array.Copy((Array) sc_13136.sspr, 95, (Array) numArray7, 0, 34);
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

  internal static string ssp_appserver_13146()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[72];
      byte[] numArray2 = new byte[55]
      {
        (byte) 188,
        (byte) 248,
        (byte) 45,
        (byte) 78,
        (byte) 103,
        (byte) 20,
        (byte) 117,
        (byte) 194,
        (byte) 22,
        (byte) 83,
        (byte) 165,
        (byte) 165,
        (byte) 226,
        (byte) 4,
        (byte) 226,
        (byte) 78,
        (byte) 188,
        (byte) 11,
        (byte) 169,
        (byte) 124,
        (byte) 233,
        (byte) 214,
        (byte) 9,
        (byte) 189,
        (byte) 124,
        (byte) 137,
        (byte) 132,
        (byte) 127 /*0x7F*/,
        (byte) 191,
        (byte) 71,
        (byte) 189,
        (byte) 100,
        (byte) 65,
        (byte) 235,
        (byte) 139,
        (byte) 79,
        (byte) 0,
        (byte) 71,
        (byte) 46,
        (byte) 54,
        (byte) 181,
        (byte) 182,
        (byte) 144 /*0x90*/,
        (byte) 62,
        (byte) 14,
        (byte) 47,
        (byte) 107,
        (byte) 7,
        (byte) 111,
        (byte) 214,
        (byte) 155,
        (byte) 84,
        (byte) 198,
        (byte) 6,
        (byte) 157
      };
      byte[] numArray3 = new byte[55];
      numArray3[6] = (byte) 111;
      numArray3[12] = (byte) 154;
      numArray3[29] = (byte) 154;
      numArray3[3] = (byte) 130;
      numArray3[34] = (byte) 130;
      numArray3[5] = (byte) 130;
      numArray3[11] = (byte) 110;
      numArray3[10] = (byte) 9;
      numArray3[8] = (byte) 206;
      numArray3[23] = (byte) 191;
      numArray3[33] = (byte) 225;
      numArray3[2] = (byte) 2;
      numArray3[26] = (byte) 36;
      numArray3[35] = (byte) 101;
      numArray3[14] = (byte) 11;
      numArray3[15] = (byte) 232;
      numArray3[19] = (byte) 83;
      numArray3[17] = (byte) 207;
      numArray3[13] = (byte) 131;
      numArray3[44] = (byte) 228;
      numArray3[20] = (byte) 237;
      numArray3[48 /*0x30*/] = (byte) 239;
      numArray3[22] = (byte) 32 /*0x20*/;
      numArray3[43] = (byte) 91;
      numArray3[24] = (byte) 16 /*0x10*/;
      numArray3[25] = (byte) 110;
      numArray3[21] = (byte) 30;
      numArray3[27] = (byte) 190;
      numArray3[41] = (byte) 224 /*0xE0*/;
      numArray3[51] = (byte) 140;
      numArray3[37] = (byte) 167;
      numArray3[31 /*0x1F*/] = (byte) 155;
      numArray3[46] = (byte) 3;
      numArray3[0] = (byte) 210;
      numArray3[28] = (byte) 216;
      numArray3[30] = (byte) 131;
      numArray3[54] = (byte) 140;
      numArray3[9] = (byte) 86;
      numArray3[18] = (byte) 142;
      numArray3[39] = (byte) 203;
      numArray3[40] = (byte) 184;
      numArray3[42] = (byte) 58;
      numArray3[53] = (byte) 41;
      numArray3[1] = (byte) 92;
      numArray3[45] = (byte) 38;
      numArray3[36] = (byte) 101;
      numArray3[7] = (byte) 63 /*0x3F*/;
      numArray3[47] = (byte) 212;
      numArray3[50] = (byte) 127 /*0x7F*/;
      numArray3[49] = (byte) 16 /*0x10*/;
      numArray3[38] = (byte) 210;
      numArray3[16 /*0x10*/] = (byte) 203;
      numArray3[52] = (byte) 190;
      numArray3[32 /*0x20*/] = (byte) 167;
      numArray3[4] = (byte) 223;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[17]
      {
        (byte) 194,
        (byte) 132,
        (byte) 191,
        (byte) 18,
        (byte) 229,
        (byte) 24,
        (byte) 36,
        (byte) 44,
        (byte) 162,
        (byte) 41,
        (byte) 168,
        (byte) 210,
        (byte) 50,
        (byte) 189,
        byte.MaxValue,
        (byte) 140,
        (byte) 204
      };
      byte[] numArray5 = new byte[17];
      numArray5[7] = (byte) 131;
      numArray5[8] = (byte) 63 /*0x3F*/;
      numArray5[2] = (byte) 26;
      numArray5[1] = (byte) 210;
      numArray5[0] = (byte) 39;
      numArray5[4] = (byte) 20;
      numArray5[6] = (byte) 203;
      numArray5[14] = (byte) 234;
      numArray5[13] = (byte) 25;
      numArray5[9] = (byte) 106;
      numArray5[10] = (byte) 166;
      numArray5[11] = (byte) 175;
      numArray5[12] = (byte) 102;
      numArray5[3] = (byte) 44;
      numArray5[5] = (byte) 99;
      numArray5[15] = (byte) 69;
      numArray5[16 /*0x10*/] = (byte) 20;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 17);
      for (int index = 0; index < 17; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[72];
    byte[] numArray7 = new byte[55];
    numArray7[12] = (byte) 234;
    numArray7[1] = (byte) 218;
    numArray7[10] = (byte) 203;
    numArray7[22] = (byte) 178;
    numArray7[4] = (byte) 123;
    numArray7[5] = (byte) 43;
    numArray7[6] = (byte) 162;
    numArray7[45] = (byte) 93;
    numArray7[37] = (byte) 232;
    numArray7[9] = (byte) 170;
    numArray7[17] = (byte) 1;
    numArray7[21] = (byte) 36;
    numArray7[13] = (byte) 134;
    numArray7[0] = (byte) 148;
    numArray7[54] = (byte) 124;
    numArray7[29] = (byte) 205;
    numArray7[25] = (byte) 58;
    numArray7[11] = (byte) 38;
    numArray7[52] = (byte) 236;
    numArray7[28] = (byte) 196;
    numArray7[20] = (byte) 21;
    numArray7[18] = (byte) 225;
    numArray7[39] = (byte) 181;
    numArray7[8] = (byte) 188;
    numArray7[26] = (byte) 87;
    numArray7[38] = (byte) 167;
    numArray7[33] = (byte) 250;
    numArray7[27] = (byte) 46;
    numArray7[19] = (byte) 2;
    numArray7[30] = (byte) 245;
    numArray7[43] = (byte) 136;
    numArray7[31 /*0x1F*/] = (byte) 239;
    numArray7[42] = (byte) 50;
    numArray7[2] = (byte) 155;
    numArray7[48 /*0x30*/] = (byte) 193;
    numArray7[50] = (byte) 234;
    numArray7[36] = (byte) 227;
    numArray7[32 /*0x20*/] = (byte) 15;
    numArray7[35] = (byte) 43;
    numArray7[15] = (byte) 236;
    numArray7[40] = (byte) 224 /*0xE0*/;
    numArray7[24] = (byte) 159;
    numArray7[23] = (byte) 183;
    numArray7[49] = (byte) 247;
    numArray7[44] = (byte) 150;
    numArray7[53] = (byte) 237;
    numArray7[46] = (byte) 7;
    numArray7[47] = (byte) 196;
    numArray7[16 /*0x10*/] = (byte) 27;
    numArray7[7] = (byte) 203;
    numArray7[3] = (byte) 156;
    numArray7[51] = (byte) 59;
    numArray7[14] = (byte) 57;
    numArray7[41] = (byte) 222;
    numArray7[34] = (byte) 56;
    byte[] numArray8 = new byte[55]
    {
      (byte) 175,
      (byte) 250,
      (byte) 210,
      (byte) 173,
      (byte) 193,
      (byte) 31 /*0x1F*/,
      (byte) 239,
      (byte) 189,
      (byte) 68,
      (byte) 59,
      (byte) 212,
      (byte) 89,
      (byte) 145,
      (byte) 127 /*0x7F*/,
      (byte) 12,
      (byte) 28,
      (byte) 26,
      (byte) 88,
      (byte) 24,
      (byte) 127 /*0x7F*/,
      (byte) 135,
      (byte) 77,
      (byte) 66,
      (byte) 121,
      (byte) 142,
      (byte) 1,
      (byte) 198,
      (byte) 239,
      (byte) 229,
      (byte) 37,
      (byte) 202,
      (byte) 48 /*0x30*/,
      (byte) 4,
      (byte) 189,
      (byte) 46,
      (byte) 243,
      (byte) 245,
      (byte) 148,
      (byte) 106,
      (byte) 177,
      (byte) 160 /*0xA0*/,
      (byte) 230,
      (byte) 148,
      (byte) 202,
      (byte) 233,
      (byte) 76,
      (byte) 16 /*0x10*/,
      (byte) 61,
      (byte) 73,
      (byte) 236,
      (byte) 162,
      (byte) 95,
      (byte) 79,
      (byte) 213,
      (byte) 111
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[17]
    {
      (byte) 194,
      (byte) 186,
      (byte) 12,
      (byte) 64 /*0x40*/,
      (byte) 7,
      (byte) 58,
      (byte) 12,
      (byte) 250,
      (byte) 215,
      (byte) 245,
      (byte) 113,
      (byte) 134,
      (byte) 10,
      (byte) 9,
      (byte) 69,
      (byte) 242,
      (byte) 75
    };
    byte[] numArray10 = new byte[17];
    numArray10[16 /*0x10*/] = byte.MaxValue;
    numArray10[1] = (byte) 40;
    numArray10[9] = (byte) 169;
    numArray10[10] = (byte) 186;
    numArray10[2] = (byte) 109;
    numArray10[13] = (byte) 198;
    numArray10[12] = (byte) 158;
    numArray10[7] = (byte) 136;
    numArray10[0] = (byte) 185;
    numArray10[3] = (byte) 218;
    numArray10[4] = (byte) 227;
    numArray10[11] = (byte) 228;
    numArray10[15] = (byte) 253;
    numArray10[5] = (byte) 14;
    numArray10[14] = (byte) 173;
    numArray10[6] = (byte) 54;
    numArray10[8] = (byte) 242;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 17);
    for (int index = 0; index < 17; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_13147()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[14];
      byte[] numArray2 = new byte[14]
      {
        (byte) 100,
        (byte) 157,
        (byte) 55,
        (byte) 215,
        (byte) 226,
        (byte) 80 /*0x50*/,
        (byte) 164,
        (byte) 239,
        (byte) 168,
        (byte) 229,
        (byte) 114,
        (byte) 239,
        (byte) 116,
        (byte) 21
      };
      byte[] numArray3 = new byte[14]
      {
        (byte) 10,
        (byte) 88,
        (byte) 79,
        (byte) 33,
        (byte) 220,
        (byte) 152,
        (byte) 93,
        (byte) 44,
        (byte) 243,
        (byte) 226,
        (byte) 134,
        (byte) 172,
        (byte) 156,
        (byte) 128 /*0x80*/
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 14);
      for (int index = 0; index < 14; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[14];
    byte[] numArray5 = new byte[14];
    numArray5[2] = (byte) 239;
    numArray5[1] = (byte) 191;
    numArray5[5] = (byte) 91;
    numArray5[11] = (byte) 60;
    numArray5[8] = (byte) 147;
    numArray5[13] = (byte) 203;
    numArray5[6] = (byte) 226;
    numArray5[10] = (byte) 57;
    numArray5[4] = (byte) 78;
    numArray5[9] = (byte) 160 /*0xA0*/;
    numArray5[0] = (byte) 91;
    numArray5[7] = (byte) 34;
    numArray5[12] = (byte) 6;
    numArray5[3] = (byte) 35;
    byte[] numArray6 = new byte[14]
    {
      (byte) 190,
      (byte) 249,
      (byte) 29,
      (byte) 134,
      (byte) 15,
      (byte) 64 /*0x40*/,
      (byte) 153,
      (byte) 37,
      (byte) 26,
      (byte) 188,
      (byte) 230,
      (byte) 57,
      (byte) 23,
      (byte) 239
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 14);
    for (int index = 0; index < 14; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13148()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[73];
      byte[] numArray2 = new byte[55];
      numArray2[25] = (byte) 120;
      numArray2[0] = (byte) 121;
      numArray2[9] = (byte) 181;
      numArray2[3] = (byte) 114;
      numArray2[45] = (byte) 250;
      numArray2[5] = (byte) 124;
      numArray2[49] = (byte) 223;
      numArray2[27] = (byte) 62;
      numArray2[6] = (byte) 21;
      numArray2[46] = (byte) 12;
      numArray2[1] = (byte) 93;
      numArray2[11] = (byte) 176 /*0xB0*/;
      numArray2[8] = (byte) 84;
      numArray2[13] = (byte) 178;
      numArray2[51] = (byte) 145;
      numArray2[53] = (byte) 124;
      numArray2[16 /*0x10*/] = (byte) 94;
      numArray2[17] = (byte) 88;
      numArray2[18] = (byte) 201;
      numArray2[12] = (byte) 107;
      numArray2[2] = (byte) 225;
      numArray2[4] = (byte) 106;
      numArray2[43] = (byte) 201;
      numArray2[54] = (byte) 46;
      numArray2[41] = (byte) 129;
      numArray2[22] = (byte) 39;
      numArray2[26] = (byte) 245;
      numArray2[35] = (byte) 168;
      numArray2[23] = (byte) 215;
      numArray2[29] = (byte) 42;
      numArray2[30] = (byte) 57;
      numArray2[31 /*0x1F*/] = (byte) 196;
      numArray2[7] = (byte) 65;
      numArray2[10] = (byte) 78;
      numArray2[34] = (byte) 194;
      numArray2[47] = (byte) 123;
      numArray2[15] = (byte) 25;
      numArray2[36] = (byte) 234;
      numArray2[38] = (byte) 44;
      numArray2[39] = (byte) 45;
      numArray2[40] = (byte) 59;
      numArray2[32 /*0x20*/] = (byte) 96 /*0x60*/;
      numArray2[24] = (byte) 20;
      numArray2[14] = (byte) 162;
      numArray2[44] = (byte) 52;
      numArray2[20] = (byte) 35;
      numArray2[42] = (byte) 34;
      numArray2[19] = (byte) 196;
      numArray2[48 /*0x30*/] = (byte) 109;
      numArray2[37] = (byte) 161;
      numArray2[50] = (byte) 88;
      numArray2[28] = (byte) 98;
      numArray2[52] = (byte) 5;
      numArray2[21] = (byte) 89;
      numArray2[33] = (byte) 231;
      byte[] numArray3 = new byte[55];
      numArray3[50] = (byte) 78;
      numArray3[1] = (byte) 125;
      numArray3[2] = (byte) 146;
      numArray3[32 /*0x20*/] = (byte) 13;
      numArray3[51] = (byte) 185;
      numArray3[13] = (byte) 148;
      numArray3[26] = (byte) 185;
      numArray3[3] = (byte) 60;
      numArray3[12] = (byte) 121;
      numArray3[9] = (byte) 125;
      numArray3[10] = (byte) 45;
      numArray3[53] = (byte) 142;
      numArray3[49] = (byte) 174;
      numArray3[33] = (byte) 127 /*0x7F*/;
      numArray3[14] = (byte) 94;
      numArray3[17] = (byte) 240 /*0xF0*/;
      numArray3[39] = (byte) 149;
      numArray3[52] = (byte) 199;
      numArray3[18] = (byte) 48 /*0x30*/;
      numArray3[8] = (byte) 239;
      numArray3[20] = (byte) 185;
      numArray3[38] = (byte) 11;
      numArray3[22] = (byte) 97;
      numArray3[5] = (byte) 217;
      numArray3[24] = (byte) 77;
      numArray3[21] = (byte) 61;
      numArray3[15] = (byte) 105;
      numArray3[27] = (byte) 184;
      numArray3[28] = (byte) 253;
      numArray3[29] = (byte) 11;
      numArray3[44] = (byte) 158;
      numArray3[31 /*0x1F*/] = (byte) 4;
      numArray3[6] = (byte) 61;
      numArray3[45] = (byte) 32 /*0x20*/;
      numArray3[34] = (byte) 67;
      numArray3[11] = (byte) 62;
      numArray3[0] = (byte) 118;
      numArray3[37] = (byte) 149;
      numArray3[47] = (byte) 65;
      numArray3[54] = (byte) 211;
      numArray3[40] = (byte) 5;
      numArray3[41] = (byte) 81;
      numArray3[42] = (byte) 214;
      numArray3[43] = (byte) 9;
      numArray3[25] = (byte) 118;
      numArray3[7] = byte.MaxValue;
      numArray3[36] = (byte) 173;
      numArray3[30] = (byte) 233;
      numArray3[16 /*0x10*/] = (byte) 193;
      numArray3[46] = (byte) 182;
      numArray3[48 /*0x30*/] = (byte) 194;
      numArray3[19] = (byte) 62;
      numArray3[4] = (byte) 67;
      numArray3[23] = (byte) 117;
      numArray3[35] = (byte) 208 /*0xD0*/;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[18]
      {
        (byte) 109,
        (byte) 210,
        (byte) 163,
        (byte) 32 /*0x20*/,
        (byte) 166,
        (byte) 137,
        (byte) 32 /*0x20*/,
        (byte) 146,
        (byte) 119,
        (byte) 124,
        (byte) 160 /*0xA0*/,
        (byte) 150,
        (byte) 237,
        (byte) 213,
        (byte) 119,
        (byte) 62,
        (byte) 93,
        (byte) 195
      };
      byte[] numArray5 = new byte[18]
      {
        (byte) 135,
        (byte) 132,
        (byte) 26,
        (byte) 182,
        (byte) 182,
        (byte) 3,
        (byte) 120,
        (byte) 65,
        (byte) 186,
        (byte) 63 /*0x3F*/,
        (byte) 247,
        (byte) 123,
        (byte) 110,
        (byte) 252,
        (byte) 18,
        (byte) 241,
        (byte) 164,
        (byte) 33
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 18);
      for (int index = 0; index < 18; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[73];
    byte[] numArray7 = new byte[55];
    numArray7[2] = (byte) 39;
    numArray7[8] = (byte) 223;
    numArray7[16 /*0x10*/] = (byte) 119;
    numArray7[0] = (byte) 26;
    numArray7[15] = (byte) 15;
    numArray7[5] = (byte) 114;
    numArray7[6] = (byte) 21;
    numArray7[13] = (byte) 11;
    numArray7[50] = (byte) 230;
    numArray7[46] = (byte) 25;
    numArray7[10] = (byte) 159;
    numArray7[11] = (byte) 12;
    numArray7[38] = (byte) 51;
    numArray7[19] = (byte) 128 /*0x80*/;
    numArray7[52] = (byte) 253;
    numArray7[25] = (byte) 253;
    numArray7[22] = (byte) 193;
    numArray7[7] = (byte) 207;
    numArray7[28] = (byte) 52;
    numArray7[26] = (byte) 139;
    numArray7[20] = (byte) 230;
    numArray7[21] = (byte) 102;
    numArray7[47] = (byte) 201;
    numArray7[14] = (byte) 47;
    numArray7[31 /*0x1F*/] = (byte) 209;
    numArray7[4] = (byte) 115;
    numArray7[35] = (byte) 158;
    numArray7[27] = (byte) 170;
    numArray7[41] = (byte) 194;
    numArray7[29] = (byte) 22;
    numArray7[30] = (byte) 13;
    numArray7[9] = (byte) 31 /*0x1F*/;
    numArray7[3] = (byte) 175;
    numArray7[33] = (byte) 46;
    numArray7[51] = (byte) 10;
    numArray7[18] = (byte) 188;
    numArray7[36] = (byte) 123;
    numArray7[37] = (byte) 113;
    numArray7[49] = (byte) 47;
    numArray7[39] = (byte) 23;
    numArray7[32 /*0x20*/] = (byte) 14;
    numArray7[34] = (byte) 83;
    numArray7[42] = (byte) 151;
    numArray7[43] = (byte) 92;
    numArray7[44] = (byte) 0;
    numArray7[45] = (byte) 137;
    numArray7[17] = (byte) 185;
    numArray7[24] = (byte) 168;
    numArray7[48 /*0x30*/] = (byte) 72;
    numArray7[12] = (byte) 211;
    numArray7[40] = (byte) 11;
    numArray7[23] = (byte) 27;
    numArray7[1] = (byte) 51;
    numArray7[53] = (byte) 28;
    numArray7[54] = (byte) 178;
    byte[] numArray8 = new byte[55];
    numArray8[50] = (byte) 99;
    numArray8[23] = (byte) 18;
    numArray8[7] = (byte) 197;
    numArray8[16 /*0x10*/] = (byte) 235;
    numArray8[51] = (byte) 89;
    numArray8[1] = (byte) 79;
    numArray8[34] = (byte) 110;
    numArray8[0] = (byte) 3;
    numArray8[54] = (byte) 54;
    numArray8[13] = (byte) 186;
    numArray8[6] = (byte) 66;
    numArray8[32 /*0x20*/] = (byte) 65;
    numArray8[12] = (byte) 143;
    numArray8[38] = (byte) 204;
    numArray8[14] = (byte) 33;
    numArray8[2] = (byte) 207;
    numArray8[15] = (byte) 0;
    numArray8[17] = (byte) 47;
    numArray8[18] = (byte) 141;
    numArray8[19] = (byte) 54;
    numArray8[28] = (byte) 49;
    numArray8[33] = (byte) 202;
    numArray8[25] = (byte) 114;
    numArray8[43] = (byte) 15;
    numArray8[24] = (byte) 173;
    numArray8[8] = (byte) 151;
    numArray8[26] = (byte) 100;
    numArray8[27] = (byte) 60;
    numArray8[11] = (byte) 81;
    numArray8[29] = (byte) 240 /*0xF0*/;
    numArray8[30] = (byte) 94;
    numArray8[31 /*0x1F*/] = (byte) 115;
    numArray8[44] = (byte) 54;
    numArray8[5] = (byte) 223;
    numArray8[40] = (byte) 10;
    numArray8[35] = (byte) 189;
    numArray8[36] = (byte) 4;
    numArray8[37] = (byte) 22;
    numArray8[3] = (byte) 61;
    numArray8[39] = (byte) 209;
    numArray8[45] = (byte) 18;
    numArray8[4] = (byte) 94;
    numArray8[42] = (byte) 131;
    numArray8[41] = (byte) 75;
    numArray8[10] = (byte) 130;
    numArray8[53] = (byte) 176 /*0xB0*/;
    numArray8[46] = (byte) 63 /*0x3F*/;
    numArray8[47] = (byte) 107;
    numArray8[48 /*0x30*/] = (byte) 238;
    numArray8[49] = (byte) 222;
    numArray8[9] = (byte) 52;
    numArray8[20] = (byte) 80 /*0x50*/;
    numArray8[52] = (byte) 166;
    numArray8[22] = (byte) 216;
    numArray8[21] = (byte) 131;
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[18];
    numArray9[8] = (byte) 126;
    numArray9[4] = (byte) 96 /*0x60*/;
    numArray9[0] = (byte) 7;
    numArray9[11] = (byte) 94;
    numArray9[10] = (byte) 61;
    numArray9[17] = (byte) 62;
    numArray9[3] = (byte) 114;
    numArray9[7] = (byte) 144 /*0x90*/;
    numArray9[1] = (byte) 86;
    numArray9[2] = (byte) 33;
    numArray9[6] = (byte) 73;
    numArray9[16 /*0x10*/] = (byte) 104;
    numArray9[12] = (byte) 68;
    numArray9[13] = (byte) 163;
    numArray9[14] = (byte) 26;
    numArray9[15] = (byte) 153;
    numArray9[5] = (byte) 42;
    numArray9[9] = (byte) 194;
    byte[] numArray10 = new byte[18]
    {
      (byte) 151,
      (byte) 181,
      (byte) 221,
      (byte) 71,
      (byte) 241,
      (byte) 15,
      (byte) 18,
      (byte) 228,
      (byte) 65,
      (byte) 84,
      (byte) 74,
      (byte) 110,
      (byte) 239,
      (byte) 152,
      (byte) 216,
      (byte) 155,
      (byte) 227,
      (byte) 228
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 18);
    for (int index = 0; index < 18; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_13149()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[15];
      byte[] numArray2 = new byte[15]
      {
        (byte) 236,
        (byte) 170,
        (byte) 186,
        (byte) 187,
        (byte) 13,
        (byte) 152,
        (byte) 16 /*0x10*/,
        (byte) 211,
        (byte) 42,
        (byte) 35,
        (byte) 122,
        (byte) 147,
        (byte) 44,
        (byte) 26,
        (byte) 21
      };
      byte[] numArray3 = new byte[15]
      {
        (byte) 52,
        (byte) 139,
        (byte) 106,
        (byte) 9,
        (byte) 11,
        (byte) 3,
        (byte) 238,
        (byte) 146,
        (byte) 58,
        (byte) 101,
        (byte) 164,
        (byte) 118,
        (byte) 81,
        (byte) 119,
        (byte) 60
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 15);
      for (int index = 0; index < 15; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[44];
      byte[] response = new byte[44];
      Array.Copy((Array) sc_13136.sspq, 129, (Array) numArray4, 0, 44);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_13136.sspr, 129, (Array) numArray4, 0, 44);
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
    byte[] numArray5 = new byte[15];
    byte[] numArray6 = new byte[15];
    numArray6[4] = (byte) 45;
    numArray6[0] = (byte) 58;
    numArray6[2] = (byte) 170;
    numArray6[5] = (byte) 61;
    numArray6[10] = (byte) 151;
    numArray6[6] = (byte) 225;
    numArray6[1] = (byte) 150;
    numArray6[8] = (byte) 233;
    numArray6[11] = (byte) 200;
    numArray6[9] = (byte) 227;
    numArray6[13] = (byte) 95;
    numArray6[3] = (byte) 139;
    numArray6[12] = (byte) 25;
    numArray6[7] = (byte) 95;
    numArray6[14] = (byte) 50;
    byte[] numArray7 = new byte[15];
    numArray7[4] = (byte) 1;
    numArray7[1] = (byte) 35;
    numArray7[8] = (byte) 192 /*0xC0*/;
    numArray7[12] = (byte) 209;
    numArray7[0] = (byte) 110;
    numArray7[5] = (byte) 204;
    numArray7[2] = (byte) 77;
    numArray7[7] = (byte) 42;
    numArray7[10] = (byte) 199;
    numArray7[9] = (byte) 26;
    numArray7[14] = (byte) 131;
    numArray7[11] = (byte) 6;
    numArray7[3] = (byte) 0;
    numArray7[13] = (byte) 134;
    numArray7[6] = (byte) 39;
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 15);
    for (int index = 0; index < 15; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_appserver_13150()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[70];
      byte[] numArray2 = new byte[55]
      {
        (byte) 15,
        (byte) 11,
        (byte) 112 /*0x70*/,
        (byte) 64 /*0x40*/,
        (byte) 244,
        (byte) 161,
        (byte) 50,
        (byte) 25,
        (byte) 188,
        (byte) 174,
        (byte) 93,
        (byte) 13,
        (byte) 135,
        (byte) 61,
        (byte) 1,
        (byte) 244,
        (byte) 132,
        (byte) 125,
        (byte) 164,
        (byte) 186,
        (byte) 44,
        (byte) 81,
        (byte) 12,
        (byte) 185,
        (byte) 80 /*0x50*/,
        (byte) 206,
        (byte) 67,
        (byte) 8,
        (byte) 80 /*0x50*/,
        (byte) 164,
        (byte) 123,
        (byte) 185,
        (byte) 224 /*0xE0*/,
        (byte) 177,
        (byte) 18,
        (byte) 220,
        (byte) 127 /*0x7F*/,
        (byte) 209,
        (byte) 86,
        (byte) 212,
        (byte) 114,
        (byte) 237,
        (byte) 183,
        (byte) 170,
        (byte) 161,
        (byte) 111,
        (byte) 172,
        (byte) 134,
        (byte) 253,
        (byte) 55,
        (byte) 197,
        (byte) 228,
        (byte) 113,
        (byte) 148,
        (byte) 123
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 217,
        (byte) 115,
        (byte) 123,
        (byte) 88,
        (byte) 161,
        (byte) 102,
        (byte) 179,
        (byte) 195,
        (byte) 104,
        (byte) 30,
        (byte) 156,
        (byte) 116,
        (byte) 196,
        byte.MaxValue,
        (byte) 179,
        (byte) 238,
        (byte) 216,
        (byte) 68,
        (byte) 227,
        (byte) 118,
        (byte) 159,
        (byte) 44,
        (byte) 162,
        (byte) 193,
        (byte) 200,
        (byte) 235,
        (byte) 146,
        (byte) 45,
        (byte) 184,
        (byte) 59,
        (byte) 168,
        (byte) 8,
        (byte) 232,
        (byte) 189,
        (byte) 98,
        (byte) 179,
        (byte) 161,
        (byte) 92,
        (byte) 142,
        (byte) 132,
        (byte) 240 /*0xF0*/,
        (byte) 37,
        (byte) 196,
        (byte) 16 /*0x10*/,
        (byte) 28,
        (byte) 246,
        (byte) 199,
        (byte) 70,
        (byte) 103,
        (byte) 30,
        (byte) 197,
        (byte) 18,
        (byte) 134,
        (byte) 188,
        (byte) 94
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[15];
      numArray4[5] = (byte) 200;
      numArray4[7] = (byte) 51;
      numArray4[2] = (byte) 37;
      numArray4[12] = (byte) 37;
      numArray4[14] = (byte) 178;
      numArray4[11] = (byte) 42;
      numArray4[4] = (byte) 42;
      numArray4[1] = (byte) 240 /*0xF0*/;
      numArray4[8] = (byte) 164;
      numArray4[9] = (byte) 44;
      numArray4[0] = (byte) 182;
      numArray4[6] = (byte) 23;
      numArray4[10] = (byte) 213;
      numArray4[13] = (byte) 117;
      numArray4[3] = (byte) 93;
      byte[] numArray5 = new byte[15];
      numArray5[7] = (byte) 150;
      numArray5[1] = (byte) 32 /*0x20*/;
      numArray5[2] = (byte) 25;
      numArray5[3] = (byte) 158;
      numArray5[0] = (byte) 240 /*0xF0*/;
      numArray5[5] = (byte) 76;
      numArray5[10] = (byte) 12;
      numArray5[8] = (byte) 71;
      numArray5[6] = (byte) 129;
      numArray5[9] = (byte) 7;
      numArray5[13] = (byte) 20;
      numArray5[11] = (byte) 237;
      numArray5[12] = (byte) 67;
      numArray5[4] = (byte) 185;
      numArray5[14] = (byte) 179;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 15);
      for (int index = 0; index < 15; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[70];
    byte[] numArray7 = new byte[55]
    {
      (byte) 18,
      (byte) 189,
      (byte) 161,
      (byte) 76,
      (byte) 60,
      (byte) 35,
      (byte) 158,
      (byte) 158,
      (byte) 15,
      (byte) 4,
      (byte) 160 /*0xA0*/,
      (byte) 31 /*0x1F*/,
      (byte) 175,
      (byte) 56,
      (byte) 156,
      (byte) 92,
      (byte) 224 /*0xE0*/,
      (byte) 159,
      (byte) 213,
      (byte) 72,
      (byte) 172,
      (byte) 208 /*0xD0*/,
      (byte) 245,
      (byte) 89,
      (byte) 0,
      (byte) 26,
      (byte) 249,
      (byte) 233,
      (byte) 27,
      (byte) 157,
      (byte) 74,
      (byte) 96 /*0x60*/,
      (byte) 50,
      (byte) 86,
      (byte) 204,
      (byte) 217,
      (byte) 166,
      byte.MaxValue,
      (byte) 250,
      (byte) 49,
      (byte) 62,
      (byte) 42,
      (byte) 210,
      (byte) 44,
      (byte) 214,
      (byte) 39,
      (byte) 116,
      (byte) 103,
      (byte) 219,
      (byte) 159,
      (byte) 98,
      (byte) 55,
      (byte) 135,
      (byte) 143,
      (byte) 21
    };
    byte[] numArray8 = new byte[55];
    numArray8[17] = (byte) 161;
    numArray8[7] = (byte) 233;
    numArray8[2] = (byte) 155;
    numArray8[23] = (byte) 127 /*0x7F*/;
    numArray8[39] = (byte) 206;
    numArray8[33] = (byte) 23;
    numArray8[4] = (byte) 0;
    numArray8[3] = (byte) 133;
    numArray8[8] = (byte) 32 /*0x20*/;
    numArray8[9] = (byte) 201;
    numArray8[10] = (byte) 215;
    numArray8[44] = (byte) 24;
    numArray8[5] = (byte) 239;
    numArray8[13] = (byte) 225;
    numArray8[34] = (byte) 36;
    numArray8[15] = (byte) 131;
    numArray8[12] = (byte) 228;
    numArray8[47] = (byte) 213;
    numArray8[29] = (byte) 166;
    numArray8[19] = (byte) 218;
    numArray8[20] = (byte) 216;
    numArray8[37] = (byte) 223;
    numArray8[22] = (byte) 163;
    numArray8[42] = (byte) 187;
    numArray8[1] = (byte) 102;
    numArray8[25] = (byte) 133;
    numArray8[18] = (byte) 56;
    numArray8[27] = (byte) 160 /*0xA0*/;
    numArray8[36] = (byte) 46;
    numArray8[24] = (byte) 240 /*0xF0*/;
    numArray8[49] = (byte) 121;
    numArray8[31 /*0x1F*/] = (byte) 213;
    numArray8[32 /*0x20*/] = (byte) 184;
    numArray8[14] = (byte) 243;
    numArray8[45] = (byte) 81;
    numArray8[35] = (byte) 82;
    numArray8[0] = (byte) 64 /*0x40*/;
    numArray8[21] = (byte) 54;
    numArray8[38] = (byte) 133;
    numArray8[11] = (byte) 187;
    numArray8[40] = (byte) 153;
    numArray8[41] = (byte) 129;
    numArray8[28] = (byte) 32 /*0x20*/;
    numArray8[43] = (byte) 53;
    numArray8[6] = (byte) 170;
    numArray8[30] = (byte) 180;
    numArray8[46] = (byte) 65;
    numArray8[16 /*0x10*/] = (byte) 77;
    numArray8[48 /*0x30*/] = (byte) 64 /*0x40*/;
    numArray8[53] = (byte) 30;
    numArray8[50] = (byte) 177;
    numArray8[51] = (byte) 103;
    numArray8[52] = (byte) 230;
    numArray8[26] = (byte) 182;
    numArray8[54] = (byte) 114;
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[15]
    {
      (byte) 195,
      (byte) 115,
      (byte) 123,
      (byte) 151,
      (byte) 166,
      (byte) 6,
      (byte) 54,
      (byte) 166,
      (byte) 101,
      (byte) 74,
      (byte) 23,
      (byte) 225,
      (byte) 177,
      (byte) 86,
      (byte) 45
    };
    byte[] numArray10 = new byte[15]
    {
      (byte) 115,
      (byte) 190,
      (byte) 194,
      (byte) 113,
      (byte) 56,
      (byte) 20,
      (byte) 8,
      (byte) 99,
      (byte) 6,
      (byte) 73,
      (byte) 237,
      (byte) 194,
      (byte) 185,
      (byte) 222,
      (byte) 218
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 15);
    for (int index = 0; index < 15; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_13151()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[71];
      byte[] numArray2 = new byte[55]
      {
        (byte) 156,
        (byte) 85,
        (byte) 237,
        (byte) 158,
        (byte) 137,
        (byte) 149,
        (byte) 245,
        (byte) 146,
        (byte) 185,
        (byte) 127 /*0x7F*/,
        (byte) 246,
        (byte) 28,
        (byte) 173,
        (byte) 69,
        (byte) 18,
        (byte) 155,
        (byte) 63 /*0x3F*/,
        (byte) 93,
        (byte) 112 /*0x70*/,
        (byte) 228,
        (byte) 162,
        (byte) 42,
        (byte) 204,
        (byte) 49,
        (byte) 239,
        (byte) 4,
        (byte) 27,
        (byte) 103,
        (byte) 146,
        (byte) 83,
        (byte) 200,
        (byte) 72,
        (byte) 6,
        (byte) 186,
        (byte) 167,
        (byte) 169,
        (byte) 214,
        (byte) 213,
        (byte) 241,
        (byte) 1,
        (byte) 177,
        (byte) 115,
        (byte) 125,
        (byte) 39,
        (byte) 111,
        (byte) 46,
        (byte) 105,
        (byte) 155,
        (byte) 158,
        (byte) 186,
        (byte) 238,
        (byte) 109,
        (byte) 42,
        (byte) 155,
        (byte) 55
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 158,
        (byte) 236,
        (byte) 13,
        (byte) 150,
        (byte) 145,
        (byte) 47,
        (byte) 245,
        (byte) 51,
        (byte) 193,
        (byte) 166,
        (byte) 169,
        (byte) 34,
        (byte) 9,
        (byte) 137,
        (byte) 72,
        (byte) 190,
        (byte) 86,
        (byte) 202,
        (byte) 169,
        (byte) 145,
        (byte) 10,
        (byte) 23,
        (byte) 105,
        (byte) 9,
        (byte) 47,
        (byte) 242,
        (byte) 187,
        (byte) 140,
        (byte) 137,
        (byte) 9,
        (byte) 107,
        (byte) 203,
        (byte) 191,
        (byte) 205,
        (byte) 143,
        (byte) 84,
        (byte) 245,
        (byte) 136,
        (byte) 154,
        (byte) 205,
        (byte) 219,
        (byte) 173,
        (byte) 113,
        (byte) 199,
        (byte) 24,
        (byte) 93,
        (byte) 232,
        (byte) 236,
        (byte) 9,
        (byte) 102,
        (byte) 26,
        (byte) 33,
        (byte) 226,
        (byte) 4,
        (byte) 178
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[16 /*0x10*/]
      {
        (byte) 187,
        (byte) 175,
        (byte) 133,
        (byte) 122,
        (byte) 141,
        byte.MaxValue,
        (byte) 101,
        (byte) 183,
        (byte) 173,
        (byte) 21,
        (byte) 243,
        (byte) 158,
        (byte) 104,
        (byte) 197,
        (byte) 105,
        (byte) 108
      };
      byte[] numArray5 = new byte[16 /*0x10*/];
      numArray5[0] = (byte) 103;
      numArray5[1] = (byte) 60;
      numArray5[2] = (byte) 179;
      numArray5[4] = (byte) 16 /*0x10*/;
      numArray5[13] = (byte) 54;
      numArray5[11] = (byte) 246;
      numArray5[6] = (byte) 6;
      numArray5[9] = (byte) 8;
      numArray5[8] = (byte) 247;
      numArray5[3] = (byte) 44;
      numArray5[7] = (byte) 68;
      numArray5[5] = (byte) 115;
      numArray5[12] = (byte) 47;
      numArray5[10] = (byte) 195;
      numArray5[14] = (byte) 90;
      numArray5[15] = (byte) 119;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 16 /*0x10*/);
      for (int index = 0; index < 16 /*0x10*/; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[71];
    byte[] numArray7 = new byte[55]
    {
      (byte) 38,
      (byte) 108,
      (byte) 135,
      (byte) 35,
      (byte) 115,
      (byte) 23,
      (byte) 111,
      (byte) 194,
      (byte) 25,
      (byte) 118,
      (byte) 252,
      (byte) 53,
      (byte) 245,
      (byte) 132,
      (byte) 1,
      (byte) 187,
      (byte) 104,
      (byte) 8,
      (byte) 197,
      (byte) 226,
      (byte) 250,
      (byte) 238,
      (byte) 234,
      (byte) 249,
      (byte) 196,
      (byte) 40,
      (byte) 0,
      (byte) 92,
      (byte) 142,
      (byte) 169,
      (byte) 112 /*0x70*/,
      (byte) 105,
      (byte) 71,
      (byte) 171,
      (byte) 67,
      byte.MaxValue,
      (byte) 231,
      (byte) 15,
      (byte) 186,
      (byte) 149,
      (byte) 53,
      byte.MaxValue,
      (byte) 167,
      (byte) 36,
      (byte) 131,
      (byte) 147,
      (byte) 226,
      (byte) 224 /*0xE0*/,
      (byte) 88,
      (byte) 99,
      (byte) 3,
      (byte) 122,
      (byte) 112 /*0x70*/,
      (byte) 62,
      (byte) 31 /*0x1F*/
    };
    byte[] numArray8 = new byte[55]
    {
      (byte) 23,
      (byte) 254,
      (byte) 162,
      (byte) 232,
      (byte) 243,
      (byte) 50,
      (byte) 121,
      (byte) 73,
      (byte) 0,
      (byte) 189,
      (byte) 125,
      (byte) 186,
      (byte) 79,
      (byte) 188,
      (byte) 58,
      (byte) 24,
      (byte) 109,
      (byte) 180,
      (byte) 172,
      (byte) 107,
      (byte) 205,
      (byte) 238,
      (byte) 14,
      (byte) 205,
      (byte) 190,
      (byte) 109,
      (byte) 105,
      (byte) 196,
      (byte) 93,
      (byte) 19,
      (byte) 3,
      (byte) 217,
      (byte) 177,
      (byte) 119,
      (byte) 251,
      (byte) 235,
      (byte) 155,
      (byte) 238,
      (byte) 42,
      (byte) 108,
      (byte) 156,
      (byte) 169,
      (byte) 240 /*0xF0*/,
      (byte) 156,
      (byte) 101,
      (byte) 40,
      (byte) 59,
      (byte) 24,
      (byte) 85,
      (byte) 2,
      (byte) 243,
      (byte) 66,
      (byte) 182,
      (byte) 186,
      (byte) 30
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[16 /*0x10*/]
    {
      (byte) 141,
      (byte) 95,
      (byte) 75,
      (byte) 23,
      (byte) 18,
      (byte) 51,
      (byte) 111,
      (byte) 5,
      (byte) 147,
      (byte) 238,
      (byte) 73,
      (byte) 65,
      (byte) 2,
      (byte) 211,
      (byte) 88,
      (byte) 77
    };
    byte[] numArray10 = new byte[16 /*0x10*/];
    numArray10[2] = (byte) 154;
    numArray10[9] = (byte) 42;
    numArray10[3] = (byte) 89;
    numArray10[11] = (byte) 56;
    numArray10[0] = (byte) 49;
    numArray10[5] = (byte) 97;
    numArray10[6] = (byte) 199;
    numArray10[8] = (byte) 85;
    numArray10[12] = (byte) 173;
    numArray10[7] = (byte) 226;
    numArray10[10] = (byte) 129;
    numArray10[4] = (byte) 5;
    numArray10[1] = (byte) 163;
    numArray10[13] = (byte) 248;
    numArray10[15] = (byte) 85;
    numArray10[14] = (byte) 84;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 16 /*0x10*/);
    for (int index = 0; index < 16 /*0x10*/; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_13152()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[4];
      byte[] numArray2 = new byte[4]
      {
        (byte) 111,
        (byte) 126,
        (byte) 34,
        (byte) 203
      };
      byte[] numArray3 = new byte[4]
      {
        (byte) 0,
        (byte) 243,
        (byte) 0,
        (byte) 0
      };
      numArray3[0] = (byte) 5;
      numArray3[2] = (byte) 70;
      numArray3[3] = (byte) 193;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 4);
      for (int index = 0; index < 4; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[41];
      byte[] response = new byte[41];
      Array.Copy((Array) sc_13136.sspq, 173, (Array) numArray4, 0, 41);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_13136.sspr, 173, (Array) numArray4, 0, 41);
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
    byte[] numArray5 = new byte[4];
    byte[] numArray6 = new byte[4]
    {
      (byte) 66,
      (byte) 129,
      (byte) 0,
      (byte) 134
    };
    numArray6[2] = (byte) 105;
    byte[] numArray7 = new byte[4]
    {
      (byte) 96 /*0x60*/,
      (byte) 14,
      (byte) 126,
      (byte) 44
    };
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 4);
    for (int index = 0; index < 4; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_appserver_13153()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10];
      numArray2[2] = (byte) 1;
      numArray2[1] = (byte) 21;
      numArray2[3] = (byte) 77;
      numArray2[0] = (byte) 209;
      numArray2[9] = (byte) 144 /*0x90*/;
      numArray2[5] = (byte) 7;
      numArray2[6] = (byte) 245;
      numArray2[7] = (byte) 226;
      numArray2[8] = (byte) 22;
      numArray2[4] = (byte) 143;
      byte[] numArray3 = new byte[10]
      {
        (byte) 22,
        (byte) 113,
        (byte) 236,
        (byte) 186,
        (byte) 219,
        (byte) 225,
        (byte) 38,
        (byte) 94,
        (byte) 105,
        (byte) 212
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
      (byte) 39,
      (byte) 117,
      (byte) 42,
      (byte) 118,
      (byte) 109,
      (byte) 159,
      (byte) 148,
      (byte) 49,
      (byte) 203,
      (byte) 210
    };
    byte[] numArray6 = new byte[10]
    {
      (byte) 135,
      (byte) 69,
      (byte) 232,
      (byte) 98,
      (byte) 194,
      (byte) 204,
      (byte) 83,
      (byte) 119,
      (byte) 177,
      (byte) 108
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13154()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[68];
      byte[] numArray2 = new byte[55]
      {
        (byte) 125,
        (byte) 40,
        (byte) 112 /*0x70*/,
        (byte) 113,
        (byte) 57,
        (byte) 169,
        (byte) 178,
        (byte) 222,
        (byte) 16 /*0x10*/,
        (byte) 14,
        (byte) 178,
        (byte) 85,
        (byte) 160 /*0xA0*/,
        (byte) 206,
        (byte) 114,
        (byte) 82,
        (byte) 131,
        (byte) 81,
        (byte) 13,
        (byte) 222,
        (byte) 217,
        (byte) 215,
        (byte) 100,
        (byte) 145,
        (byte) 135,
        (byte) 66,
        (byte) 130,
        (byte) 94,
        (byte) 215,
        (byte) 134,
        (byte) 98,
        (byte) 134,
        (byte) 231,
        (byte) 212,
        (byte) 197,
        (byte) 185,
        (byte) 252,
        (byte) 228,
        (byte) 224 /*0xE0*/,
        (byte) 27,
        (byte) 133,
        (byte) 73,
        (byte) 100,
        (byte) 27,
        (byte) 196,
        (byte) 23,
        (byte) 254,
        (byte) 198,
        (byte) 190,
        (byte) 120,
        (byte) 78,
        (byte) 196,
        (byte) 205,
        (byte) 167,
        (byte) 31 /*0x1F*/
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 206,
        (byte) 141,
        (byte) 170,
        (byte) 164,
        (byte) 132,
        (byte) 58,
        (byte) 9,
        (byte) 5,
        (byte) 26,
        (byte) 16 /*0x10*/,
        (byte) 47,
        (byte) 155,
        (byte) 219,
        (byte) 190,
        (byte) 127 /*0x7F*/,
        (byte) 138,
        (byte) 190,
        (byte) 235,
        (byte) 40,
        (byte) 3,
        (byte) 23,
        (byte) 247,
        (byte) 99,
        (byte) 88,
        (byte) 181,
        (byte) 250,
        (byte) 36,
        (byte) 118,
        (byte) 124,
        (byte) 243,
        (byte) 121,
        (byte) 214,
        (byte) 94,
        (byte) 163,
        (byte) 8,
        (byte) 104,
        (byte) 25,
        (byte) 104,
        (byte) 112 /*0x70*/,
        (byte) 99,
        (byte) 34,
        (byte) 179,
        (byte) 26,
        (byte) 237,
        (byte) 175,
        (byte) 221,
        (byte) 74,
        (byte) 107,
        (byte) 27,
        (byte) 227,
        (byte) 203,
        (byte) 219,
        (byte) 116,
        (byte) 224 /*0xE0*/,
        (byte) 166
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[13]
      {
        (byte) 223,
        (byte) 233,
        (byte) 246,
        (byte) 38,
        (byte) 83,
        (byte) 7,
        (byte) 138,
        (byte) 172,
        (byte) 43,
        (byte) 109,
        (byte) 203,
        (byte) 8,
        (byte) 185
      };
      byte[] numArray5 = new byte[13];
      numArray5[10] = (byte) 223;
      numArray5[1] = (byte) 253;
      numArray5[4] = (byte) 184;
      numArray5[12] = (byte) 212;
      numArray5[3] = (byte) 142;
      numArray5[7] = (byte) 98;
      numArray5[6] = (byte) 98;
      numArray5[2] = (byte) 156;
      numArray5[8] = (byte) 81;
      numArray5[9] = (byte) 143;
      numArray5[0] = (byte) 120;
      numArray5[11] = (byte) 63 /*0x3F*/;
      numArray5[5] = (byte) 125;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 13);
      for (int index = 0; index < 13; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[68];
    byte[] numArray7 = new byte[55];
    numArray7[27] = (byte) 121;
    numArray7[1] = (byte) 64 /*0x40*/;
    numArray7[11] = (byte) 218;
    numArray7[51] = (byte) 154;
    numArray7[47] = (byte) 52;
    numArray7[5] = (byte) 29;
    numArray7[6] = (byte) 251;
    numArray7[10] = (byte) 66;
    numArray7[32 /*0x20*/] = (byte) 82;
    numArray7[9] = (byte) 50;
    numArray7[24] = (byte) 51;
    numArray7[39] = (byte) 57;
    numArray7[4] = (byte) 245;
    numArray7[52] = (byte) 59;
    numArray7[14] = (byte) 170;
    numArray7[8] = (byte) 244;
    numArray7[16 /*0x10*/] = (byte) 189;
    numArray7[17] = (byte) 5;
    numArray7[18] = (byte) 34;
    numArray7[19] = (byte) 175;
    numArray7[20] = (byte) 82;
    numArray7[30] = (byte) 179;
    numArray7[53] = (byte) 136;
    numArray7[0] = (byte) 185;
    numArray7[7] = (byte) 233;
    numArray7[25] = (byte) 123;
    numArray7[33] = (byte) 14;
    numArray7[3] = (byte) 31 /*0x1F*/;
    numArray7[28] = (byte) 178;
    numArray7[13] = (byte) 152;
    numArray7[38] = (byte) 196;
    numArray7[31 /*0x1F*/] = (byte) 169;
    numArray7[41] = (byte) 135;
    numArray7[37] = (byte) 232;
    numArray7[43] = (byte) 38;
    numArray7[29] = (byte) 106;
    numArray7[34] = (byte) 114;
    numArray7[21] = (byte) 219;
    numArray7[22] = (byte) 79;
    numArray7[50] = (byte) 254;
    numArray7[40] = (byte) 163;
    numArray7[26] = (byte) 18;
    numArray7[42] = (byte) 254;
    numArray7[23] = (byte) 143;
    numArray7[44] = (byte) 77;
    numArray7[45] = (byte) 151;
    numArray7[46] = (byte) 217;
    numArray7[36] = (byte) 22;
    numArray7[48 /*0x30*/] = (byte) 197;
    numArray7[49] = (byte) 16 /*0x10*/;
    numArray7[2] = (byte) 181;
    numArray7[15] = (byte) 51;
    numArray7[12] = (byte) 111;
    numArray7[54] = (byte) 75;
    numArray7[35] = (byte) 45;
    byte[] numArray8 = new byte[55]
    {
      (byte) 6,
      (byte) 125,
      (byte) 165,
      (byte) 163,
      (byte) 114,
      (byte) 205,
      (byte) 48 /*0x30*/,
      (byte) 168,
      (byte) 115,
      (byte) 141,
      (byte) 234,
      (byte) 143,
      (byte) 117,
      (byte) 230,
      (byte) 248,
      (byte) 134,
      (byte) 177,
      (byte) 141,
      (byte) 185,
      (byte) 82,
      (byte) 80 /*0x50*/,
      (byte) 199,
      (byte) 99,
      (byte) 161,
      (byte) 43,
      (byte) 110,
      (byte) 129,
      (byte) 64 /*0x40*/,
      (byte) 99,
      (byte) 92,
      (byte) 117,
      (byte) 63 /*0x3F*/,
      (byte) 13,
      (byte) 171,
      (byte) 133,
      (byte) 0,
      (byte) 53,
      (byte) 31 /*0x1F*/,
      (byte) 175,
      (byte) 219,
      (byte) 102,
      (byte) 211,
      (byte) 235,
      (byte) 176 /*0xB0*/,
      (byte) 155,
      (byte) 84,
      (byte) 88,
      (byte) 188,
      (byte) 153,
      (byte) 242,
      (byte) 223,
      (byte) 59,
      (byte) 50,
      (byte) 0,
      (byte) 40
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[13]
    {
      (byte) 228,
      (byte) 80 /*0x50*/,
      (byte) 216,
      (byte) 45,
      (byte) 205,
      (byte) 149,
      (byte) 30,
      (byte) 102,
      (byte) 44,
      (byte) 189,
      (byte) 157,
      (byte) 146,
      (byte) 64 /*0x40*/
    };
    byte[] numArray10 = new byte[13];
    numArray10[4] = (byte) 58;
    numArray10[0] = (byte) 69;
    numArray10[2] = (byte) 35;
    numArray10[7] = (byte) 111;
    numArray10[3] = (byte) 106;
    numArray10[5] = (byte) 139;
    numArray10[1] = (byte) 28;
    numArray10[6] = (byte) 232;
    numArray10[8] = (byte) 106;
    numArray10[9] = (byte) 56;
    numArray10[10] = (byte) 98;
    numArray10[11] = (byte) 112 /*0x70*/;
    numArray10[12] = (byte) 62;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 13);
    for (int index = 0; index < 13; ++index)
      numArray6[index + 55] ^= numArray10[index];
    byte[] numArray11 = new byte[19];
    byte[] response = new byte[19];
    Array.Copy((Array) sc_13136.sspq, 214, (Array) numArray11, 0, 19);
    key.Query(true, 335, numArray11, response);
    Array.Copy((Array) sc_13136.sspr, 214, (Array) numArray11, 0, 19);
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

  internal static string ssp_appserver_13155()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[14];
      byte[] numArray2 = new byte[14]
      {
        (byte) 152,
        (byte) 147,
        (byte) 5,
        (byte) 151,
        (byte) 70,
        (byte) 87,
        (byte) 196,
        (byte) 175,
        (byte) 93,
        (byte) 98,
        (byte) 232,
        (byte) 219,
        (byte) 167,
        (byte) 18
      };
      byte[] numArray3 = new byte[14]
      {
        (byte) 129,
        (byte) 93,
        (byte) 73,
        (byte) 198,
        (byte) 100,
        (byte) 126,
        (byte) 97,
        (byte) 121,
        (byte) 130,
        (byte) 172,
        (byte) 21,
        (byte) 110,
        (byte) 149,
        (byte) 177
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 14);
      for (int index = 0; index < 14; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[14];
    byte[] numArray5 = new byte[14]
    {
      (byte) 203,
      (byte) 32 /*0x20*/,
      (byte) 44,
      (byte) 133,
      (byte) 185,
      (byte) 147,
      (byte) 191,
      (byte) 18,
      (byte) 64 /*0x40*/,
      (byte) 5,
      (byte) 58,
      (byte) 77,
      (byte) 84,
      (byte) 98
    };
    byte[] numArray6 = new byte[14];
    numArray6[5] = (byte) 30;
    numArray6[1] = (byte) 240 /*0xF0*/;
    numArray6[11] = (byte) 212;
    numArray6[3] = (byte) 232;
    numArray6[6] = (byte) 163;
    numArray6[10] = (byte) 68;
    numArray6[0] = (byte) 75;
    numArray6[9] = (byte) 147;
    numArray6[8] = (byte) 30;
    numArray6[4] = (byte) 55;
    numArray6[2] = (byte) 201;
    numArray6[7] = (byte) 228;
    numArray6[12] = (byte) 21;
    numArray6[13] = (byte) 182;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 14);
    for (int index = 0; index < 14; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[32 /*0x20*/];
    byte[] response = new byte[32 /*0x20*/];
    Array.Copy((Array) sc_13136.sspq, 233, (Array) numArray7, 0, 32 /*0x20*/);
    key.Query(true, 335, numArray7, response);
    Array.Copy((Array) sc_13136.sspr, 233, (Array) numArray7, 0, 32 /*0x20*/);
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

  internal static string ssp_appserver_13156()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[9];
      byte[] numArray2 = new byte[9]
      {
        (byte) 88,
        (byte) 205,
        (byte) 215,
        (byte) 37,
        (byte) 62,
        (byte) 205,
        (byte) 127 /*0x7F*/,
        (byte) 162,
        (byte) 186
      };
      byte[] numArray3 = new byte[9];
      numArray3[0] = (byte) 89;
      numArray3[8] = (byte) 2;
      numArray3[2] = (byte) 134;
      numArray3[3] = (byte) 85;
      numArray3[4] = (byte) 0;
      numArray3[5] = (byte) 164;
      numArray3[6] = (byte) 223;
      numArray3[7] = (byte) 95;
      numArray3[1] = (byte) 102;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 9);
      for (int index = 0; index < 9; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[9];
    byte[] numArray5 = new byte[9]
    {
      (byte) 43,
      (byte) 99,
      (byte) 123,
      (byte) 159,
      (byte) 59,
      (byte) 222,
      (byte) 24,
      (byte) 200,
      (byte) 95
    };
    byte[] numArray6 = new byte[9]
    {
      (byte) 86,
      (byte) 208 /*0xD0*/,
      (byte) 52,
      (byte) 30,
      (byte) 118,
      (byte) 38,
      (byte) 26,
      (byte) 14,
      (byte) 79
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 9);
    for (int index = 0; index < 9; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13157()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 49,
        (byte) 148,
        (byte) 50,
        (byte) 218,
        (byte) 196,
        (byte) 28,
        (byte) 203,
        (byte) 237,
        (byte) 251,
        (byte) 36
      };
      byte[] numArray3 = new byte[10];
      numArray3[7] = (byte) 220;
      numArray3[1] = (byte) 97;
      numArray3[6] = (byte) 113;
      numArray3[3] = (byte) 75;
      numArray3[4] = (byte) 212;
      numArray3[2] = (byte) 128 /*0x80*/;
      numArray3[0] = (byte) 231;
      numArray3[5] = (byte) 141;
      numArray3[8] = (byte) 127 /*0x7F*/;
      numArray3[9] = (byte) 138;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[42];
      byte[] response = new byte[42];
      Array.Copy((Array) sc_13136.sspq, 265, (Array) numArray4, 0, 42);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_13136.sspr, 265, (Array) numArray4, 0, 42);
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
    numArray6[0] = (byte) 175;
    numArray6[2] = (byte) 188;
    numArray6[9] = (byte) 177;
    numArray6[5] = (byte) 147;
    numArray6[4] = (byte) 19;
    numArray6[3] = (byte) 153;
    numArray6[8] = (byte) 212;
    numArray6[7] = (byte) 148;
    numArray6[1] = (byte) 104;
    numArray6[6] = (byte) 18;
    byte[] numArray7 = new byte[10]
    {
      (byte) 157,
      (byte) 220,
      (byte) 34,
      (byte) 208 /*0xD0*/,
      (byte) 185,
      (byte) 135,
      (byte) 39,
      (byte) 114,
      (byte) 111,
      (byte) 103
    };
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray5[index] ^= numArray7[index];
    byte[] numArray8 = new byte[24];
    byte[] response1 = new byte[24];
    Array.Copy((Array) sc_13136.sspq, 307, (Array) numArray8, 0, 24);
    key.Query(true, 335, numArray8, response1);
    Array.Copy((Array) sc_13136.sspr, 307, (Array) numArray8, 0, 24);
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

  internal static string ssp_appserver_13158()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 52,
        (byte) 177,
        (byte) 0,
        (byte) 0,
        (byte) 32 /*0x20*/,
        (byte) 0,
        (byte) 241,
        (byte) 224 /*0xE0*/,
        (byte) 0,
        (byte) 0
      };
      numArray2[5] = (byte) 87;
      numArray2[2] = (byte) 134;
      numArray2[3] = (byte) 250;
      numArray2[8] = (byte) 149;
      numArray2[9] = (byte) 247;
      byte[] numArray3 = new byte[10]
      {
        (byte) 212,
        (byte) 215,
        (byte) 38,
        (byte) 173,
        (byte) 226,
        (byte) 119,
        (byte) 216,
        (byte) 133,
        (byte) 133,
        (byte) 245
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
      (byte) 82,
      (byte) 135,
      (byte) 60,
      (byte) 87,
      (byte) 137,
      (byte) 32 /*0x20*/,
      (byte) 32 /*0x20*/,
      (byte) 162,
      (byte) 37,
      (byte) 206
    };
    byte[] numArray6 = new byte[10];
    numArray6[8] = (byte) 36;
    numArray6[1] = (byte) 202;
    numArray6[7] = (byte) 17;
    numArray6[2] = (byte) 3;
    numArray6[4] = (byte) 170;
    numArray6[9] = (byte) 177;
    numArray6[3] = (byte) 37;
    numArray6[6] = (byte) 200;
    numArray6[5] = (byte) 99;
    numArray6[0] = (byte) 219;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[40];
    byte[] response = new byte[40];
    Array.Copy((Array) sc_13136.sspq, 331, (Array) numArray7, 0, 40);
    key.Query(true, 335, numArray7, response);
    Array.Copy((Array) sc_13136.sspr, 331, (Array) numArray7, 0, 40);
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

  internal static int ssp_appserver_13159(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[37] = (byte) 187;
    sourceArray1[13] = (byte) 198;
    sourceArray1[26] = (byte) 169;
    sourceArray1[2] = (byte) 41;
    sourceArray1[40] = (byte) 64 /*0x40*/;
    sourceArray1[5] = (byte) 156;
    sourceArray1[23] = (byte) 153;
    sourceArray1[24] = (byte) 60;
    sourceArray1[8] = (byte) 48 /*0x30*/;
    sourceArray1[9] = (byte) 204;
    sourceArray1[10] = (byte) 91;
    sourceArray1[11] = (byte) 80 /*0x50*/;
    sourceArray1[12] = (byte) 238;
    sourceArray1[0] = (byte) 155;
    sourceArray1[28] = (byte) 58;
    sourceArray1[1] = (byte) 85;
    sourceArray1[44] = (byte) 63 /*0x3F*/;
    sourceArray1[16 /*0x10*/] = (byte) 187;
    sourceArray1[34] = (byte) 160 /*0xA0*/;
    sourceArray1[19] = (byte) 177;
    sourceArray1[20] = (byte) 201;
    sourceArray1[41] = (byte) 246;
    sourceArray1[22] = (byte) 162;
    sourceArray1[21] = (byte) 92;
    sourceArray1[17] = (byte) 181;
    sourceArray1[25] = (byte) 5;
    sourceArray1[33] = (byte) 254;
    sourceArray1[35] = (byte) 241;
    sourceArray1[7] = (byte) 40;
    sourceArray1[29] = (byte) 234;
    sourceArray1[30] = (byte) 65;
    sourceArray1[39] = (byte) 64 /*0x40*/;
    sourceArray1[18] = (byte) 22;
    sourceArray1[43] = (byte) 169;
    sourceArray1[4] = (byte) 181;
    sourceArray1[45] = (byte) 229;
    sourceArray1[36] = (byte) 69;
    sourceArray1[31 /*0x1F*/] = (byte) 41;
    sourceArray1[38] = (byte) 34;
    sourceArray1[6] = (byte) 30;
    sourceArray1[32 /*0x20*/] = (byte) 66;
    sourceArray1[3] = (byte) 32 /*0x20*/;
    sourceArray1[42] = (byte) 232;
    sourceArray1[27] = (byte) 93;
    sourceArray1[14] = (byte) 55;
    sourceArray1[15] = (byte) 43;
    sourceArray1[46] = (byte) 239;
    sourceArray1[47] = (byte) 143;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 120,
      (byte) 49,
      (byte) 123,
      (byte) 174,
      (byte) 190,
      (byte) 183,
      (byte) 169,
      (byte) 168,
      (byte) 35,
      (byte) 3,
      (byte) 11,
      (byte) 32 /*0x20*/,
      (byte) 50,
      (byte) 244,
      (byte) 99,
      (byte) 139,
      (byte) 197,
      (byte) 79,
      (byte) 194,
      (byte) 56,
      (byte) 205,
      (byte) 182,
      (byte) 85,
      (byte) 236,
      (byte) 18,
      (byte) 49,
      (byte) 93,
      (byte) 155,
      (byte) 48 /*0x30*/,
      (byte) 183,
      (byte) 25,
      (byte) 8,
      (byte) 154,
      (byte) 186,
      (byte) 61,
      (byte) 219,
      (byte) 49,
      (byte) 80 /*0x50*/,
      (byte) 13,
      (byte) 38,
      (byte) 29,
      (byte) 120,
      (byte) 201,
      (byte) 12,
      (byte) 115,
      (byte) 48 /*0x30*/,
      (byte) 47,
      (byte) 70
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_13160()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[52];
      byte[] numArray2 = new byte[52]
      {
        (byte) 215,
        byte.MaxValue,
        (byte) 105,
        (byte) 141,
        (byte) 115,
        (byte) 206,
        (byte) 203,
        (byte) 69,
        (byte) 154,
        (byte) 212,
        (byte) 202,
        (byte) 187,
        (byte) 54,
        (byte) 142,
        (byte) 154,
        (byte) 217,
        (byte) 53,
        (byte) 14,
        (byte) 41,
        (byte) 168,
        (byte) 35,
        (byte) 215,
        (byte) 153,
        (byte) 196,
        (byte) 7,
        (byte) 50,
        (byte) 41,
        (byte) 33,
        (byte) 136,
        (byte) 241,
        (byte) 203,
        (byte) 208 /*0xD0*/,
        (byte) 218,
        (byte) 76,
        (byte) 126,
        (byte) 22,
        (byte) 175,
        (byte) 253,
        (byte) 108,
        (byte) 77,
        (byte) 22,
        (byte) 201,
        (byte) 156,
        (byte) 130,
        (byte) 150,
        (byte) 131,
        (byte) 125,
        (byte) 71,
        (byte) 215,
        (byte) 115,
        (byte) 74,
        (byte) 248
      };
      byte[] numArray3 = new byte[52]
      {
        (byte) 35,
        (byte) 53,
        (byte) 199,
        (byte) 81,
        (byte) 122,
        (byte) 37,
        (byte) 125,
        (byte) 81,
        (byte) 180,
        (byte) 117,
        (byte) 24,
        (byte) 28,
        (byte) 24,
        (byte) 163,
        (byte) 31 /*0x1F*/,
        (byte) 128 /*0x80*/,
        (byte) 85,
        (byte) 185,
        (byte) 170,
        (byte) 114,
        (byte) 122,
        (byte) 186,
        (byte) 120,
        (byte) 225,
        (byte) 71,
        (byte) 249,
        (byte) 173,
        (byte) 57,
        (byte) 169,
        (byte) 26,
        (byte) 18,
        (byte) 121,
        (byte) 227,
        (byte) 30,
        (byte) 70,
        (byte) 69,
        (byte) 83,
        (byte) 128 /*0x80*/,
        (byte) 175,
        (byte) 25,
        (byte) 239,
        (byte) 43,
        (byte) 246,
        (byte) 133,
        (byte) 16 /*0x10*/,
        (byte) 19,
        (byte) 141,
        (byte) 173,
        (byte) 128 /*0x80*/,
        (byte) 125,
        (byte) 250,
        (byte) 180
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 52);
      for (int index = 0; index < 52; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[52];
    byte[] numArray5 = new byte[52]
    {
      byte.MaxValue,
      (byte) 102,
      (byte) 164,
      (byte) 133,
      (byte) 65,
      (byte) 164,
      (byte) 170,
      (byte) 188,
      (byte) 168,
      (byte) 8,
      (byte) 250,
      (byte) 225,
      (byte) 234,
      (byte) 15,
      (byte) 131,
      (byte) 42,
      (byte) 55,
      (byte) 162,
      (byte) 220,
      (byte) 145,
      (byte) 90,
      (byte) 59,
      (byte) 22,
      (byte) 46,
      (byte) 132,
      (byte) 181,
      (byte) 225,
      (byte) 73,
      (byte) 20,
      (byte) 167,
      (byte) 210,
      (byte) 87,
      (byte) 99,
      (byte) 97,
      (byte) 127 /*0x7F*/,
      (byte) 176 /*0xB0*/,
      (byte) 11,
      (byte) 119,
      (byte) 43,
      (byte) 168,
      (byte) 108,
      (byte) 12,
      (byte) 243,
      (byte) 121,
      (byte) 156,
      (byte) 162,
      (byte) 146,
      (byte) 56,
      (byte) 83,
      (byte) 73,
      (byte) 18,
      (byte) 85
    };
    byte[] numArray6 = new byte[52]
    {
      (byte) 7,
      (byte) 39,
      (byte) 221,
      (byte) 11,
      (byte) 191,
      (byte) 144 /*0x90*/,
      (byte) 159,
      (byte) 2,
      (byte) 152,
      (byte) 103,
      (byte) 111,
      (byte) 19,
      (byte) 125,
      (byte) 12,
      (byte) 34,
      (byte) 47,
      (byte) 235,
      (byte) 188,
      (byte) 64 /*0x40*/,
      (byte) 89,
      (byte) 154,
      (byte) 49,
      (byte) 90,
      byte.MaxValue,
      (byte) 231,
      (byte) 240 /*0xF0*/,
      (byte) 9,
      (byte) 181,
      (byte) 239,
      (byte) 112 /*0x70*/,
      (byte) 187,
      (byte) 138,
      (byte) 208 /*0xD0*/,
      (byte) 136,
      (byte) 83,
      (byte) 203,
      (byte) 1,
      (byte) 82,
      (byte) 144 /*0x90*/,
      (byte) 209,
      (byte) 136,
      (byte) 77,
      (byte) 76,
      (byte) 167,
      (byte) 92,
      (byte) 253,
      (byte) 189,
      (byte) 212,
      (byte) 170,
      (byte) 208 /*0xD0*/,
      (byte) 20,
      (byte) 163
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 52);
    for (int index = 0; index < 52; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13161()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[50];
      byte[] numArray2 = new byte[50]
      {
        (byte) 79,
        (byte) 118,
        (byte) 1,
        (byte) 140,
        (byte) 195,
        (byte) 96 /*0x60*/,
        (byte) 167,
        (byte) 1,
        (byte) 54,
        (byte) 162,
        (byte) 125,
        (byte) 137,
        (byte) 144 /*0x90*/,
        (byte) 211,
        (byte) 31 /*0x1F*/,
        (byte) 191,
        (byte) 119,
        (byte) 52,
        (byte) 37,
        (byte) 77,
        (byte) 72,
        (byte) 157,
        (byte) 146,
        (byte) 225,
        (byte) 34,
        (byte) 169,
        (byte) 246,
        (byte) 203,
        (byte) 149,
        (byte) 125,
        (byte) 186,
        (byte) 127 /*0x7F*/,
        (byte) 209,
        (byte) 187,
        (byte) 97,
        (byte) 148,
        (byte) 118,
        (byte) 158,
        (byte) 229,
        (byte) 11,
        (byte) 100,
        (byte) 242,
        (byte) 67,
        (byte) 249,
        (byte) 37,
        (byte) 9,
        (byte) 168,
        (byte) 128 /*0x80*/,
        (byte) 42,
        (byte) 82
      };
      byte[] numArray3 = new byte[50];
      numArray3[48 /*0x30*/] = (byte) 13;
      numArray3[1] = (byte) 207;
      numArray3[2] = (byte) 64 /*0x40*/;
      numArray3[27] = (byte) 82;
      numArray3[4] = (byte) 155;
      numArray3[5] = (byte) 133;
      numArray3[6] = (byte) 116;
      numArray3[49] = (byte) 65;
      numArray3[8] = (byte) 158;
      numArray3[9] = (byte) 227;
      numArray3[43] = (byte) 248;
      numArray3[11] = (byte) 49;
      numArray3[14] = (byte) 73;
      numArray3[13] = (byte) 89;
      numArray3[0] = (byte) 55;
      numArray3[3] = (byte) 49;
      numArray3[10] = (byte) 82;
      numArray3[17] = (byte) 193;
      numArray3[46] = (byte) 233;
      numArray3[19] = (byte) 58;
      numArray3[20] = (byte) 58;
      numArray3[21] = (byte) 237;
      numArray3[33] = (byte) 157;
      numArray3[23] = (byte) 15;
      numArray3[40] = (byte) 118;
      numArray3[25] = (byte) 144 /*0x90*/;
      numArray3[26] = (byte) 118;
      numArray3[32 /*0x20*/] = (byte) 224 /*0xE0*/;
      numArray3[12] = (byte) 85;
      numArray3[29] = byte.MaxValue;
      numArray3[22] = (byte) 157;
      numArray3[37] = (byte) 37;
      numArray3[41] = (byte) 228;
      numArray3[24] = (byte) 203;
      numArray3[30] = (byte) 125;
      numArray3[36] = (byte) 48 /*0x30*/;
      numArray3[47] = (byte) 159;
      numArray3[15] = (byte) 209;
      numArray3[38] = (byte) 35;
      numArray3[34] = (byte) 86;
      numArray3[35] = (byte) 221;
      numArray3[44] = (byte) 9;
      numArray3[42] = (byte) 21;
      numArray3[7] = (byte) 187;
      numArray3[28] = (byte) 88;
      numArray3[45] = (byte) 248;
      numArray3[39] = (byte) 170;
      numArray3[16 /*0x10*/] = (byte) 154;
      numArray3[31 /*0x1F*/] = (byte) 214;
      numArray3[18] = (byte) 204;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 50);
      for (int index = 0; index < 50; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[18];
      byte[] response = new byte[18];
      Array.Copy((Array) sc_13136.sspq, 371, (Array) numArray4, 0, 18);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_13136.sspr, 371, (Array) numArray4, 0, 18);
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
    byte[] numArray5 = new byte[50];
    byte[] numArray6 = new byte[50];
    numArray6[43] = (byte) 138;
    numArray6[37] = (byte) 21;
    numArray6[29] = (byte) 92;
    numArray6[1] = (byte) 194;
    numArray6[20] = (byte) 53;
    numArray6[6] = (byte) 121;
    numArray6[5] = (byte) 132;
    numArray6[32 /*0x20*/] = (byte) 234;
    numArray6[8] = (byte) 208 /*0xD0*/;
    numArray6[9] = (byte) 206;
    numArray6[33] = (byte) 39;
    numArray6[11] = (byte) 149;
    numArray6[12] = (byte) 200;
    numArray6[48 /*0x30*/] = (byte) 142;
    numArray6[7] = (byte) 97;
    numArray6[15] = (byte) 0;
    numArray6[47] = (byte) 236;
    numArray6[16 /*0x10*/] = (byte) 57;
    numArray6[18] = (byte) 58;
    numArray6[19] = (byte) 26;
    numArray6[2] = (byte) 217;
    numArray6[21] = (byte) 25;
    numArray6[0] = (byte) 149;
    numArray6[34] = (byte) 62;
    numArray6[36] = (byte) 198;
    numArray6[39] = (byte) 147;
    numArray6[26] = (byte) 177;
    numArray6[27] = (byte) 171;
    numArray6[23] = (byte) 102;
    numArray6[31 /*0x1F*/] = (byte) 124;
    numArray6[30] = (byte) 112 /*0x70*/;
    numArray6[40] = (byte) 44;
    numArray6[14] = (byte) 102;
    numArray6[17] = (byte) 153;
    numArray6[24] = (byte) 242;
    numArray6[49] = (byte) 59;
    numArray6[13] = (byte) 45;
    numArray6[25] = (byte) 229;
    numArray6[38] = (byte) 97;
    numArray6[41] = (byte) 247;
    numArray6[28] = (byte) 75;
    numArray6[35] = (byte) 76;
    numArray6[42] = (byte) 85;
    numArray6[45] = (byte) 206;
    numArray6[44] = (byte) 194;
    numArray6[22] = (byte) 86;
    numArray6[46] = (byte) 135;
    numArray6[3] = (byte) 243;
    numArray6[4] = (byte) 163;
    numArray6[10] = (byte) 135;
    byte[] numArray7 = new byte[50]
    {
      (byte) 117,
      (byte) 137,
      (byte) 29,
      (byte) 223,
      (byte) 235,
      (byte) 80 /*0x50*/,
      (byte) 32 /*0x20*/,
      (byte) 58,
      (byte) 76,
      (byte) 157,
      (byte) 36,
      (byte) 179,
      (byte) 131,
      (byte) 127 /*0x7F*/,
      (byte) 5,
      (byte) 116,
      (byte) 12,
      (byte) 77,
      (byte) 123,
      (byte) 19,
      (byte) 59,
      (byte) 218,
      (byte) 247,
      (byte) 161,
      (byte) 109,
      (byte) 30,
      (byte) 191,
      (byte) 62,
      (byte) 75,
      (byte) 12,
      (byte) 102,
      (byte) 84,
      (byte) 234,
      (byte) 72,
      (byte) 199,
      (byte) 233,
      (byte) 187,
      (byte) 229,
      (byte) 53,
      (byte) 187,
      (byte) 50,
      (byte) 149,
      (byte) 152,
      (byte) 237,
      (byte) 106,
      (byte) 122,
      (byte) 186,
      (byte) 201,
      (byte) 111,
      (byte) 166
    };
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 50);
    for (int index = 0; index < 50; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_appserver_13162()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[69];
      byte[] numArray2 = new byte[55]
      {
        (byte) 1,
        (byte) 202,
        (byte) 72,
        (byte) 179,
        (byte) 51,
        (byte) 182,
        (byte) 184,
        (byte) 160 /*0xA0*/,
        (byte) 207,
        (byte) 220,
        (byte) 96 /*0x60*/,
        (byte) 13,
        (byte) 176 /*0xB0*/,
        (byte) 4,
        (byte) 7,
        (byte) 109,
        (byte) 232,
        (byte) 227,
        (byte) 110,
        (byte) 44,
        (byte) 59,
        (byte) 230,
        (byte) 16 /*0x10*/,
        (byte) 148,
        (byte) 229,
        (byte) 144 /*0x90*/,
        (byte) 91,
        (byte) 225,
        (byte) 159,
        (byte) 235,
        (byte) 1,
        (byte) 119,
        (byte) 200,
        (byte) 170,
        (byte) 15,
        (byte) 161,
        (byte) 168,
        (byte) 110,
        (byte) 147,
        (byte) 124,
        (byte) 9,
        (byte) 62,
        (byte) 131,
        (byte) 56,
        (byte) 67,
        (byte) 179,
        (byte) 174,
        (byte) 221,
        (byte) 219,
        (byte) 190,
        (byte) 207,
        (byte) 63 /*0x3F*/,
        (byte) 119,
        (byte) 208 /*0xD0*/,
        (byte) 55
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 12,
        (byte) 216,
        (byte) 70,
        (byte) 122,
        (byte) 229,
        (byte) 126,
        (byte) 48 /*0x30*/,
        (byte) 229,
        (byte) 145,
        (byte) 247,
        (byte) 205,
        (byte) 58,
        (byte) 209,
        (byte) 244,
        (byte) 247,
        (byte) 18,
        (byte) 187,
        (byte) 83,
        (byte) 105,
        (byte) 212,
        (byte) 135,
        (byte) 242,
        (byte) 19,
        (byte) 103,
        (byte) 68,
        (byte) 197,
        (byte) 15,
        (byte) 184,
        (byte) 190,
        (byte) 120,
        (byte) 42,
        (byte) 11,
        (byte) 4,
        (byte) 175,
        (byte) 11,
        (byte) 144 /*0x90*/,
        (byte) 63 /*0x3F*/,
        (byte) 38,
        (byte) 200,
        (byte) 68,
        (byte) 223,
        (byte) 161,
        (byte) 37,
        (byte) 7,
        (byte) 212,
        (byte) 56,
        (byte) 93,
        (byte) 95,
        (byte) 145,
        (byte) 7,
        (byte) 134,
        (byte) 85,
        (byte) 55,
        (byte) 106,
        (byte) 77
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[14]
      {
        (byte) 180,
        (byte) 206,
        (byte) 163,
        (byte) 90,
        (byte) 214,
        (byte) 1,
        (byte) 228,
        (byte) 218,
        (byte) 232,
        (byte) 69,
        (byte) 5,
        (byte) 245,
        (byte) 101,
        (byte) 197
      };
      byte[] numArray5 = new byte[14];
      numArray5[13] = (byte) 117;
      numArray5[7] = (byte) 64 /*0x40*/;
      numArray5[6] = (byte) 102;
      numArray5[2] = (byte) 232;
      numArray5[3] = (byte) 77;
      numArray5[5] = (byte) 0;
      numArray5[4] = (byte) 124;
      numArray5[8] = (byte) 192 /*0xC0*/;
      numArray5[11] = (byte) 134;
      numArray5[9] = (byte) 20;
      numArray5[10] = (byte) 181;
      numArray5[0] = (byte) 26;
      numArray5[12] = (byte) 43;
      numArray5[1] = (byte) 142;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 14);
      for (int index = 0; index < 14; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[69];
    byte[] numArray7 = new byte[55];
    numArray7[47] = (byte) 57;
    numArray7[1] = (byte) 244;
    numArray7[2] = (byte) 231;
    numArray7[52] = (byte) 209;
    numArray7[4] = (byte) 160 /*0xA0*/;
    numArray7[5] = (byte) 239;
    numArray7[24] = (byte) 99;
    numArray7[39] = (byte) 96 /*0x60*/;
    numArray7[8] = (byte) 61;
    numArray7[9] = (byte) 159;
    numArray7[33] = (byte) 76;
    numArray7[36] = (byte) 126;
    numArray7[18] = (byte) 239;
    numArray7[27] = (byte) 220;
    numArray7[46] = (byte) 195;
    numArray7[15] = (byte) 117;
    numArray7[19] = (byte) 134;
    numArray7[30] = (byte) 200;
    numArray7[14] = (byte) 231;
    numArray7[17] = (byte) 84;
    numArray7[20] = (byte) 172;
    numArray7[21] = (byte) 58;
    numArray7[38] = (byte) 218;
    numArray7[23] = (byte) 226;
    numArray7[6] = (byte) 33;
    numArray7[3] = (byte) 243;
    numArray7[11] = (byte) 91;
    numArray7[35] = (byte) 1;
    numArray7[13] = (byte) 168;
    numArray7[29] = (byte) 31 /*0x1F*/;
    numArray7[12] = (byte) 110;
    numArray7[50] = (byte) 232;
    numArray7[32 /*0x20*/] = (byte) 232;
    numArray7[37] = (byte) 209;
    numArray7[34] = (byte) 147;
    numArray7[10] = (byte) 93;
    numArray7[51] = (byte) 199;
    numArray7[22] = (byte) 185;
    numArray7[41] = (byte) 240 /*0xF0*/;
    numArray7[16 /*0x10*/] = (byte) 249;
    numArray7[40] = (byte) 207;
    numArray7[54] = (byte) 75;
    numArray7[42] = (byte) 191;
    numArray7[43] = (byte) 194;
    numArray7[7] = (byte) 50;
    numArray7[28] = (byte) 208 /*0xD0*/;
    numArray7[25] = (byte) 156;
    numArray7[45] = (byte) 245;
    numArray7[48 /*0x30*/] = (byte) 150;
    numArray7[49] = (byte) 144 /*0x90*/;
    numArray7[0] = (byte) 114;
    numArray7[26] = (byte) 193;
    numArray7[53] = (byte) 121;
    numArray7[44] = (byte) 122;
    numArray7[31 /*0x1F*/] = (byte) 25;
    byte[] numArray8 = new byte[55]
    {
      (byte) 46,
      (byte) 197,
      (byte) 14,
      (byte) 100,
      (byte) 140,
      (byte) 111,
      (byte) 59,
      (byte) 213,
      (byte) 44,
      (byte) 221,
      (byte) 234,
      (byte) 23,
      (byte) 127 /*0x7F*/,
      (byte) 90,
      (byte) 58,
      (byte) 115,
      (byte) 22,
      (byte) 90,
      (byte) 173,
      (byte) 170,
      (byte) 53,
      (byte) 183,
      (byte) 165,
      (byte) 203,
      (byte) 27,
      (byte) 70,
      (byte) 125,
      (byte) 184,
      (byte) 62,
      (byte) 250,
      (byte) 72,
      (byte) 61,
      (byte) 97,
      (byte) 119,
      (byte) 204,
      (byte) 184,
      (byte) 241,
      (byte) 102,
      (byte) 1,
      (byte) 115,
      (byte) 238,
      (byte) 169,
      (byte) 12,
      (byte) 184,
      (byte) 162,
      (byte) 24,
      (byte) 91,
      (byte) 170,
      (byte) 186,
      (byte) 17,
      (byte) 169,
      (byte) 91,
      (byte) 163,
      (byte) 98,
      (byte) 124
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[14]
    {
      (byte) 155,
      (byte) 33,
      (byte) 42,
      (byte) 166,
      (byte) 254,
      (byte) 34,
      (byte) 229,
      (byte) 214,
      (byte) 253,
      (byte) 4,
      (byte) 32 /*0x20*/,
      (byte) 185,
      (byte) 32 /*0x20*/,
      (byte) 126
    };
    byte[] numArray10 = new byte[14];
    numArray10[1] = (byte) 154;
    numArray10[4] = (byte) 8;
    numArray10[2] = (byte) 68;
    numArray10[3] = (byte) 12;
    numArray10[8] = (byte) 243;
    numArray10[5] = (byte) 199;
    numArray10[7] = (byte) 91;
    numArray10[0] = (byte) 40;
    numArray10[6] = (byte) 213;
    numArray10[9] = (byte) 144 /*0x90*/;
    numArray10[11] = (byte) 167;
    numArray10[10] = (byte) 74;
    numArray10[12] = (byte) 186;
    numArray10[13] = (byte) 59;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 14);
    for (int index = 0; index < 14; ++index)
      numArray6[index + 55] ^= numArray10[index];
    byte[] numArray11 = new byte[15];
    byte[] response = new byte[15];
    Array.Copy((Array) sc_13136.sspq, 389, (Array) numArray11, 0, 15);
    key.Query(true, 335, numArray11, response);
    Array.Copy((Array) sc_13136.sspr, 389, (Array) numArray11, 0, 15);
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

  internal static string ssp_appserver_13163()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[14];
      byte[] numArray2 = new byte[14];
      numArray2[4] = (byte) 96 /*0x60*/;
      numArray2[7] = (byte) 104;
      numArray2[2] = (byte) 227;
      numArray2[3] = (byte) 151;
      numArray2[13] = (byte) 110;
      numArray2[12] = (byte) 124;
      numArray2[11] = (byte) 104;
      numArray2[1] = (byte) 169;
      numArray2[8] = (byte) 53;
      numArray2[9] = (byte) 242;
      numArray2[0] = (byte) 219;
      numArray2[6] = (byte) 57;
      numArray2[5] = (byte) 227;
      numArray2[10] = (byte) 65;
      byte[] numArray3 = new byte[14];
      numArray3[6] = (byte) 148;
      numArray3[1] = (byte) 72;
      numArray3[3] = (byte) 86;
      numArray3[11] = (byte) 211;
      numArray3[4] = (byte) 180;
      numArray3[5] = (byte) 139;
      numArray3[13] = (byte) 138;
      numArray3[7] = (byte) 240 /*0xF0*/;
      numArray3[2] = (byte) 166;
      numArray3[0] = (byte) 159;
      numArray3[10] = (byte) 143;
      numArray3[8] = (byte) 155;
      numArray3[12] = (byte) 42;
      numArray3[9] = (byte) 104;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 14);
      for (int index = 0; index < 14; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[14];
    byte[] numArray5 = new byte[14]
    {
      (byte) 35,
      (byte) 238,
      (byte) 77,
      (byte) 241,
      (byte) 64 /*0x40*/,
      (byte) 35,
      (byte) 210,
      (byte) 79,
      (byte) 240 /*0xF0*/,
      (byte) 122,
      (byte) 22,
      (byte) 177,
      (byte) 17,
      (byte) 248
    };
    byte[] numArray6 = new byte[14]
    {
      (byte) 106,
      (byte) 238,
      (byte) 164,
      (byte) 44,
      (byte) 156,
      (byte) 45,
      (byte) 145,
      (byte) 147,
      (byte) 52,
      (byte) 201,
      (byte) 114,
      (byte) 84,
      (byte) 207,
      (byte) 148
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 14);
    for (int index = 0; index < 14; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[20];
    byte[] response = new byte[20];
    Array.Copy((Array) sc_13136.sspq, 404, (Array) numArray7, 0, 20);
    key.Query(true, 335, numArray7, response);
    Array.Copy((Array) sc_13136.sspr, 404, (Array) numArray7, 0, 20);
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

  internal static string ssp_appserver_13164()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 75,
        (byte) 228,
        (byte) 171,
        (byte) 22,
        (byte) 16 /*0x10*/,
        (byte) 132,
        (byte) 88,
        (byte) 16 /*0x10*/,
        (byte) 26,
        (byte) 116
      };
      byte[] numArray3 = new byte[10];
      numArray3[4] = (byte) 230;
      numArray3[1] = (byte) 254;
      numArray3[9] = (byte) 17;
      numArray3[3] = (byte) 250;
      numArray3[2] = (byte) 235;
      numArray3[6] = (byte) 74;
      numArray3[5] = (byte) 147;
      numArray3[0] = (byte) 149;
      numArray3[8] = (byte) 22;
      numArray3[7] = (byte) 31 /*0x1F*/;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10]
    {
      (byte) 240 /*0xF0*/,
      (byte) 155,
      (byte) 79,
      (byte) 60,
      (byte) 115,
      (byte) 136,
      (byte) 124,
      (byte) 200,
      (byte) 246,
      (byte) 62
    };
    byte[] numArray6 = new byte[10]
    {
      (byte) 162,
      byte.MaxValue,
      (byte) 199,
      (byte) 93,
      (byte) 224 /*0xE0*/,
      (byte) 248,
      (byte) 44,
      (byte) 58,
      (byte) 110,
      (byte) 69
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
