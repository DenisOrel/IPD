// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_12780
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_12780
{
  private static byte[] sspq = new byte[636]
  {
    (byte) 145,
    (byte) 43,
    (byte) 3,
    (byte) 91,
    (byte) 74,
    (byte) 89,
    (byte) 141,
    (byte) 47,
    (byte) 60,
    (byte) 96 /*0x60*/,
    (byte) 33,
    (byte) 31 /*0x1F*/,
    (byte) 47,
    (byte) 119,
    (byte) 185,
    (byte) 81,
    (byte) 166,
    (byte) 37,
    (byte) 187,
    (byte) 28,
    (byte) 92,
    (byte) 121,
    (byte) 119,
    (byte) 46,
    (byte) 76,
    (byte) 66,
    (byte) 141,
    (byte) 41,
    (byte) 146,
    (byte) 215,
    (byte) 102,
    (byte) 98,
    (byte) 187,
    (byte) 241,
    (byte) 253,
    (byte) 193,
    (byte) 16 /*0x10*/,
    (byte) 35,
    (byte) 18,
    (byte) 88,
    (byte) 115,
    (byte) 54,
    (byte) 214,
    (byte) 80 /*0x50*/,
    (byte) 131,
    (byte) 155,
    (byte) 225,
    (byte) 107,
    (byte) 245,
    (byte) 177,
    (byte) 191,
    (byte) 139,
    (byte) 130,
    (byte) 18,
    (byte) 79,
    (byte) 211,
    (byte) 96 /*0x60*/,
    (byte) 169,
    (byte) 175,
    (byte) 133,
    (byte) 34,
    (byte) 76,
    (byte) 99,
    (byte) 107,
    (byte) 164,
    (byte) 15,
    (byte) 33,
    (byte) 218,
    (byte) 204,
    (byte) 188,
    (byte) 177,
    (byte) 28,
    (byte) 12,
    (byte) 193,
    (byte) 72,
    (byte) 248,
    (byte) 35,
    (byte) 97,
    (byte) 109,
    (byte) 126,
    (byte) 113,
    (byte) 84,
    (byte) 24,
    (byte) 25,
    (byte) 11,
    (byte) 16 /*0x10*/,
    (byte) 254,
    (byte) 240 /*0xF0*/,
    (byte) 63 /*0x3F*/,
    (byte) 122,
    (byte) 42,
    (byte) 80 /*0x50*/,
    (byte) 12,
    (byte) 197,
    (byte) 97,
    (byte) 70,
    (byte) 16 /*0x10*/,
    (byte) 92,
    (byte) 216,
    (byte) 131,
    (byte) 92,
    (byte) 50,
    (byte) 54,
    (byte) 187,
    (byte) 148,
    (byte) 223,
    (byte) 237,
    (byte) 199,
    (byte) 158,
    (byte) 22,
    (byte) 206,
    (byte) 111,
    (byte) 141,
    (byte) 180,
    (byte) 188,
    (byte) 95,
    (byte) 149,
    (byte) 19,
    (byte) 140,
    (byte) 82,
    (byte) 134,
    (byte) 203,
    (byte) 190,
    (byte) 36,
    (byte) 82,
    (byte) 163,
    (byte) 15,
    (byte) 248,
    (byte) 33,
    (byte) 190,
    (byte) 220,
    (byte) 65,
    (byte) 96 /*0x60*/,
    (byte) 164,
    (byte) 24,
    (byte) 3,
    (byte) 210,
    (byte) 59,
    (byte) 46,
    (byte) 88,
    (byte) 251,
    (byte) 106,
    (byte) 186,
    (byte) 211,
    (byte) 140,
    (byte) 111,
    (byte) 180,
    (byte) 165,
    (byte) 15,
    (byte) 134,
    (byte) 78,
    (byte) 223,
    (byte) 150,
    (byte) 200,
    (byte) 189,
    (byte) 242,
    (byte) 63 /*0x3F*/,
    (byte) 55,
    (byte) 181,
    (byte) 26,
    (byte) 148,
    (byte) 85,
    (byte) 33,
    (byte) 222,
    (byte) 98,
    (byte) 131,
    (byte) 4,
    (byte) 42,
    (byte) 188,
    (byte) 82,
    (byte) 213,
    (byte) 26,
    (byte) 76,
    (byte) 204,
    (byte) 40,
    (byte) 43,
    (byte) 32 /*0x20*/,
    (byte) 198,
    (byte) 54,
    (byte) 150,
    (byte) 200,
    (byte) 121,
    (byte) 121,
    (byte) 24,
    (byte) 196,
    (byte) 228,
    (byte) 158,
    (byte) 172,
    (byte) 88,
    (byte) 143,
    (byte) 110,
    (byte) 169,
    (byte) 52,
    (byte) 199,
    (byte) 235,
    (byte) 133,
    (byte) 42,
    (byte) 153,
    (byte) 190,
    (byte) 209,
    (byte) 159,
    (byte) 79,
    (byte) 100,
    (byte) 42,
    (byte) 41,
    (byte) 10,
    (byte) 110,
    (byte) 172,
    (byte) 142,
    (byte) 127 /*0x7F*/,
    (byte) 38,
    (byte) 179,
    (byte) 173,
    (byte) 106,
    (byte) 146,
    (byte) 113,
    byte.MaxValue,
    (byte) 240 /*0xF0*/,
    (byte) 21,
    (byte) 166,
    (byte) 169,
    (byte) 252,
    (byte) 58,
    (byte) 249,
    (byte) 139,
    (byte) 72,
    (byte) 57,
    (byte) 222,
    (byte) 45,
    (byte) 245,
    (byte) 170,
    (byte) 63 /*0x3F*/,
    (byte) 16 /*0x10*/,
    (byte) 57,
    (byte) 127 /*0x7F*/,
    (byte) 9,
    (byte) 205,
    (byte) 173,
    (byte) 241,
    (byte) 189,
    (byte) 30,
    (byte) 224 /*0xE0*/,
    (byte) 43,
    (byte) 241,
    (byte) 226,
    (byte) 67,
    (byte) 112 /*0x70*/,
    (byte) 166,
    (byte) 20,
    (byte) 201,
    (byte) 134,
    (byte) 20,
    (byte) 212,
    (byte) 92,
    (byte) 130,
    (byte) 170,
    (byte) 165,
    (byte) 121,
    (byte) 10,
    (byte) 202,
    (byte) 44,
    (byte) 7,
    (byte) 226,
    (byte) 102,
    (byte) 171,
    (byte) 68,
    (byte) 178,
    (byte) 110,
    (byte) 183,
    (byte) 239,
    (byte) 170,
    (byte) 242,
    (byte) 233,
    (byte) 205,
    (byte) 234,
    (byte) 158,
    (byte) 71,
    (byte) 41,
    (byte) 232,
    (byte) 94,
    (byte) 56,
    (byte) 174,
    (byte) 84,
    (byte) 226,
    (byte) 187,
    (byte) 23,
    (byte) 0,
    (byte) 8,
    (byte) 190,
    (byte) 56,
    (byte) 218,
    (byte) 170,
    (byte) 40,
    (byte) 232,
    (byte) 83,
    (byte) 243,
    (byte) 225,
    (byte) 7,
    (byte) 229,
    (byte) 115,
    (byte) 47,
    (byte) 108,
    (byte) 221,
    (byte) 115,
    (byte) 150,
    (byte) 91,
    (byte) 102,
    (byte) 238,
    (byte) 50,
    (byte) 138,
    (byte) 251,
    (byte) 217,
    (byte) 133,
    (byte) 95,
    (byte) 44,
    (byte) 42,
    (byte) 1,
    (byte) 221,
    (byte) 244,
    (byte) 37,
    (byte) 4,
    (byte) 32 /*0x20*/,
    (byte) 85,
    (byte) 14,
    (byte) 189,
    (byte) 60,
    (byte) 225,
    (byte) 187,
    (byte) 215,
    (byte) 245,
    (byte) 84,
    (byte) 241,
    (byte) 47,
    (byte) 172,
    (byte) 40,
    (byte) 96 /*0x60*/,
    (byte) 172,
    (byte) 186,
    (byte) 23,
    (byte) 246,
    (byte) 167,
    (byte) 63 /*0x3F*/,
    (byte) 58,
    (byte) 168,
    (byte) 240 /*0xF0*/,
    (byte) 18,
    (byte) 66,
    (byte) 182,
    (byte) 206,
    (byte) 11,
    (byte) 244,
    (byte) 158,
    (byte) 82,
    byte.MaxValue,
    (byte) 14,
    (byte) 132,
    (byte) 102,
    (byte) 50,
    (byte) 0,
    (byte) 210,
    (byte) 13,
    (byte) 175,
    (byte) 110,
    (byte) 72,
    (byte) 206,
    (byte) 163,
    (byte) 250,
    (byte) 99,
    (byte) 22,
    (byte) 28,
    (byte) 167,
    (byte) 238,
    (byte) 96 /*0x60*/,
    (byte) 58,
    (byte) 47,
    (byte) 213,
    (byte) 92,
    (byte) 114,
    (byte) 211,
    (byte) 155,
    (byte) 105,
    (byte) 162,
    (byte) 69,
    (byte) 192 /*0xC0*/,
    (byte) 139,
    (byte) 98,
    (byte) 69,
    (byte) 116,
    (byte) 69,
    (byte) 246,
    (byte) 187,
    (byte) 184,
    (byte) 13,
    (byte) 84,
    (byte) 117,
    (byte) 221,
    (byte) 73,
    (byte) 74,
    (byte) 31 /*0x1F*/,
    (byte) 172,
    (byte) 114,
    (byte) 181,
    (byte) 160 /*0xA0*/,
    (byte) 8,
    (byte) 12,
    (byte) 108,
    (byte) 210,
    (byte) 170,
    (byte) 83,
    (byte) 36,
    (byte) 246,
    (byte) 71,
    (byte) 38,
    (byte) 28,
    (byte) 50,
    (byte) 23,
    (byte) 141,
    (byte) 119,
    (byte) 189,
    (byte) 20,
    (byte) 169,
    (byte) 164,
    (byte) 183,
    (byte) 209,
    (byte) 114,
    (byte) 127 /*0x7F*/,
    (byte) 105,
    (byte) 204,
    (byte) 253,
    (byte) 80 /*0x50*/,
    (byte) 60,
    (byte) 3,
    (byte) 57,
    (byte) 212,
    (byte) 53,
    (byte) 181,
    (byte) 59,
    (byte) 245,
    (byte) 157,
    (byte) 51,
    (byte) 16 /*0x10*/,
    (byte) 155,
    (byte) 20,
    (byte) 200,
    (byte) 195,
    (byte) 47,
    (byte) 116,
    (byte) 104,
    (byte) 165,
    (byte) 251,
    (byte) 144 /*0x90*/,
    (byte) 37,
    (byte) 126,
    (byte) 143,
    (byte) 214,
    (byte) 160 /*0xA0*/,
    (byte) 201,
    (byte) 97,
    (byte) 231,
    (byte) 20,
    (byte) 61,
    (byte) 146,
    (byte) 229,
    (byte) 219,
    (byte) 42,
    (byte) 172,
    (byte) 254,
    (byte) 243,
    (byte) 207,
    (byte) 12,
    (byte) 64 /*0x40*/,
    (byte) 99,
    (byte) 202,
    (byte) 180,
    (byte) 96 /*0x60*/,
    (byte) 47,
    (byte) 211,
    (byte) 113,
    (byte) 80 /*0x50*/,
    (byte) 18,
    (byte) 163,
    (byte) 211,
    (byte) 1,
    (byte) 130,
    (byte) 55,
    (byte) 31 /*0x1F*/,
    (byte) 129,
    (byte) 83,
    (byte) 126,
    (byte) 132,
    (byte) 78,
    (byte) 98,
    (byte) 213,
    (byte) 248,
    (byte) 210,
    (byte) 221,
    (byte) 179,
    (byte) 222,
    (byte) 124,
    (byte) 254,
    (byte) 140,
    (byte) 242,
    (byte) 169,
    (byte) 236,
    (byte) 135,
    (byte) 187,
    (byte) 33,
    (byte) 25,
    (byte) 131,
    (byte) 228,
    (byte) 112 /*0x70*/,
    (byte) 94,
    (byte) 63 /*0x3F*/,
    (byte) 239,
    (byte) 159,
    (byte) 67,
    (byte) 144 /*0x90*/,
    (byte) 90,
    (byte) 103,
    (byte) 14,
    (byte) 242,
    (byte) 181,
    (byte) 246,
    (byte) 186,
    (byte) 160 /*0xA0*/,
    (byte) 56,
    (byte) 246,
    (byte) 235,
    (byte) 210,
    (byte) 132,
    (byte) 96 /*0x60*/,
    (byte) 124,
    (byte) 58,
    (byte) 204,
    (byte) 23,
    (byte) 6,
    (byte) 243,
    (byte) 66,
    (byte) 108,
    (byte) 152,
    (byte) 185,
    (byte) 146,
    (byte) 148,
    (byte) 31 /*0x1F*/,
    (byte) 122,
    (byte) 16 /*0x10*/,
    (byte) 25,
    (byte) 227,
    (byte) 231,
    (byte) 231,
    (byte) 102,
    (byte) 231,
    (byte) 205,
    (byte) 47,
    (byte) 60,
    (byte) 3,
    (byte) 163,
    (byte) 109,
    (byte) 74,
    (byte) 92,
    (byte) 27,
    (byte) 141,
    (byte) 111,
    (byte) 239,
    (byte) 163,
    (byte) 97,
    (byte) 236,
    (byte) 102,
    (byte) 104,
    (byte) 115,
    (byte) 87,
    (byte) 129,
    (byte) 114,
    (byte) 38,
    (byte) 137,
    (byte) 196,
    (byte) 42,
    (byte) 16 /*0x10*/,
    (byte) 232,
    (byte) 73,
    (byte) 195,
    (byte) 73,
    (byte) 80 /*0x50*/,
    (byte) 210,
    (byte) 26,
    (byte) 202,
    (byte) 251,
    (byte) 94,
    (byte) 39,
    (byte) 174,
    (byte) 116,
    (byte) 238,
    (byte) 90,
    (byte) 145,
    (byte) 86,
    (byte) 143,
    (byte) 254,
    (byte) 7,
    (byte) 245,
    (byte) 118,
    (byte) 71,
    (byte) 217,
    (byte) 176 /*0xB0*/,
    (byte) 31 /*0x1F*/,
    (byte) 158,
    (byte) 185,
    (byte) 197,
    (byte) 220,
    (byte) 126,
    (byte) 94,
    (byte) 143,
    (byte) 80 /*0x50*/,
    (byte) 143,
    (byte) 67,
    (byte) 91,
    (byte) 121,
    (byte) 8,
    (byte) 156,
    (byte) 115,
    (byte) 116,
    (byte) 22,
    (byte) 164,
    (byte) 200,
    (byte) 224 /*0xE0*/,
    (byte) 232,
    (byte) 27,
    (byte) 164,
    (byte) 146,
    (byte) 72,
    (byte) 117,
    (byte) 71,
    (byte) 180,
    (byte) 228,
    (byte) 64 /*0x40*/,
    (byte) 110,
    (byte) 105
  };
  private static byte[] sspr = new byte[636]
  {
    (byte) 174,
    (byte) 23,
    (byte) 82,
    (byte) 247,
    (byte) 95,
    (byte) 143,
    (byte) 110,
    (byte) 79,
    (byte) 39,
    (byte) 96 /*0x60*/,
    (byte) 118,
    (byte) 138,
    (byte) 69,
    (byte) 242,
    (byte) 245,
    (byte) 87,
    (byte) 165,
    (byte) 188,
    (byte) 206,
    (byte) 209,
    (byte) 241,
    (byte) 141,
    (byte) 66,
    (byte) 78,
    (byte) 135,
    (byte) 186,
    (byte) 127 /*0x7F*/,
    (byte) 251,
    (byte) 201,
    (byte) 97,
    byte.MaxValue,
    (byte) 237,
    (byte) 165,
    (byte) 16 /*0x10*/,
    (byte) 216,
    (byte) 135,
    (byte) 201,
    (byte) 150,
    (byte) 249,
    (byte) 224 /*0xE0*/,
    (byte) 114,
    (byte) 53,
    (byte) 192 /*0xC0*/,
    (byte) 115,
    (byte) 107,
    (byte) 192 /*0xC0*/,
    (byte) 214,
    (byte) 70,
    (byte) 44,
    (byte) 70,
    (byte) 79,
    (byte) 177,
    (byte) 168,
    (byte) 41,
    (byte) 207,
    (byte) 92,
    (byte) 197,
    (byte) 23,
    (byte) 103,
    (byte) 166,
    (byte) 118,
    (byte) 71,
    (byte) 12,
    (byte) 11,
    (byte) 102,
    (byte) 162,
    (byte) 163,
    (byte) 4,
    (byte) 238,
    (byte) 100,
    (byte) 93,
    (byte) 236,
    (byte) 169,
    (byte) 152,
    (byte) 185,
    (byte) 113,
    (byte) 110,
    (byte) 160 /*0xA0*/,
    (byte) 218,
    (byte) 37,
    (byte) 223,
    (byte) 206,
    (byte) 37,
    (byte) 212,
    (byte) 24,
    (byte) 61,
    (byte) 231,
    (byte) 65,
    (byte) 99,
    (byte) 159,
    (byte) 111,
    (byte) 26,
    (byte) 5,
    (byte) 154,
    (byte) 202,
    (byte) 40,
    (byte) 219,
    (byte) 140,
    (byte) 195,
    (byte) 17,
    (byte) 36,
    (byte) 109,
    (byte) 225,
    (byte) 248,
    (byte) 191,
    (byte) 25,
    (byte) 176 /*0xB0*/,
    (byte) 227,
    (byte) 111,
    (byte) 4,
    (byte) 145,
    (byte) 55,
    (byte) 161,
    (byte) 184,
    (byte) 127 /*0x7F*/,
    (byte) 188,
    (byte) 239,
    (byte) 246,
    (byte) 24,
    (byte) 123,
    (byte) 80 /*0x50*/,
    (byte) 84,
    (byte) 226,
    (byte) 20,
    (byte) 112 /*0x70*/,
    (byte) 114,
    (byte) 119,
    (byte) 165,
    (byte) 19,
    (byte) 31 /*0x1F*/,
    (byte) 10,
    (byte) 110,
    (byte) 241,
    (byte) 154,
    (byte) 149,
    (byte) 188,
    (byte) 183,
    (byte) 77,
    (byte) 233,
    (byte) 180,
    (byte) 39,
    (byte) 59,
    (byte) 132,
    (byte) 221,
    (byte) 91,
    (byte) 87,
    (byte) 102,
    (byte) 117,
    (byte) 226,
    (byte) 92,
    (byte) 115,
    (byte) 204,
    (byte) 5,
    (byte) 18,
    (byte) 128 /*0x80*/,
    (byte) 153,
    (byte) 222,
    (byte) 86,
    (byte) 62,
    (byte) 67,
    (byte) 233,
    (byte) 12,
    (byte) 103,
    (byte) 176 /*0xB0*/,
    (byte) 135,
    (byte) 241,
    (byte) 200,
    (byte) 174,
    (byte) 253,
    (byte) 78,
    (byte) 19,
    (byte) 58,
    (byte) 45,
    (byte) 214,
    (byte) 6,
    (byte) 64 /*0x40*/,
    (byte) 161,
    (byte) 185,
    (byte) 45,
    (byte) 208 /*0xD0*/,
    (byte) 68,
    (byte) 223,
    (byte) 57,
    (byte) 238,
    (byte) 158,
    (byte) 38,
    (byte) 146,
    (byte) 39,
    (byte) 247,
    (byte) 241,
    (byte) 188,
    (byte) 173,
    (byte) 29,
    (byte) 98,
    (byte) 30,
    (byte) 226,
    (byte) 168,
    (byte) 115,
    (byte) 248,
    (byte) 246,
    (byte) 112 /*0x70*/,
    (byte) 38,
    (byte) 109,
    (byte) 42,
    (byte) 233,
    (byte) 205,
    (byte) 50,
    (byte) 173,
    (byte) 108,
    (byte) 19,
    (byte) 9,
    (byte) 158,
    (byte) 183,
    (byte) 248,
    (byte) 230,
    (byte) 135,
    (byte) 184,
    (byte) 100,
    (byte) 182,
    (byte) 137,
    (byte) 221,
    (byte) 156,
    (byte) 240 /*0xF0*/,
    (byte) 159,
    (byte) 38,
    (byte) 248,
    (byte) 181,
    (byte) 129,
    (byte) 239,
    (byte) 103,
    (byte) 132,
    (byte) 160 /*0xA0*/,
    (byte) 109,
    (byte) 101,
    (byte) 89,
    (byte) 2,
    (byte) 222,
    (byte) 226,
    (byte) 225,
    (byte) 0,
    (byte) 126,
    (byte) 11,
    (byte) 175,
    (byte) 244,
    (byte) 94,
    (byte) 19,
    (byte) 98,
    (byte) 192 /*0xC0*/,
    (byte) 131,
    (byte) 128 /*0x80*/,
    (byte) 110,
    (byte) 102,
    (byte) 194,
    (byte) 19,
    (byte) 60,
    (byte) 179,
    (byte) 207,
    (byte) 233,
    (byte) 104,
    (byte) 161,
    (byte) 54,
    (byte) 86,
    (byte) 15,
    (byte) 225,
    (byte) 120,
    (byte) 90,
    (byte) 108,
    (byte) 1,
    (byte) 204,
    (byte) 252,
    (byte) 0,
    (byte) 197,
    (byte) 226,
    (byte) 76,
    (byte) 242,
    (byte) 22,
    (byte) 201,
    (byte) 235,
    (byte) 225,
    (byte) 230,
    (byte) 220,
    (byte) 86,
    (byte) 148,
    (byte) 249,
    (byte) 134,
    (byte) 162,
    (byte) 80 /*0x50*/,
    (byte) 154,
    (byte) 72,
    (byte) 42,
    (byte) 218,
    (byte) 4,
    (byte) 202,
    (byte) 193,
    (byte) 123,
    (byte) 246,
    (byte) 131,
    (byte) 221,
    (byte) 211,
    (byte) 152,
    (byte) 97,
    (byte) 88,
    (byte) 96 /*0x60*/,
    (byte) 232,
    (byte) 62,
    (byte) 27,
    (byte) 174,
    (byte) 46,
    (byte) 35,
    (byte) 32 /*0x20*/,
    (byte) 217,
    (byte) 12,
    (byte) 58,
    (byte) 139,
    (byte) 106,
    (byte) 231,
    (byte) 97,
    (byte) 132,
    (byte) 39,
    (byte) 55,
    (byte) 196,
    (byte) 121,
    (byte) 122,
    (byte) 248,
    (byte) 194,
    (byte) 222,
    (byte) 172,
    (byte) 131,
    (byte) 204,
    (byte) 51,
    (byte) 181,
    (byte) 6,
    (byte) 246,
    (byte) 251,
    (byte) 238,
    (byte) 56,
    (byte) 153,
    (byte) 69,
    (byte) 191,
    (byte) 10,
    (byte) 147,
    (byte) 164,
    (byte) 41,
    (byte) 193,
    (byte) 244,
    (byte) 210,
    (byte) 59,
    (byte) 253,
    (byte) 121,
    (byte) 111,
    (byte) 154,
    (byte) 147,
    (byte) 114,
    (byte) 240 /*0xF0*/,
    (byte) 200,
    (byte) 83,
    (byte) 34,
    (byte) 176 /*0xB0*/,
    (byte) 149,
    (byte) 119,
    (byte) 212,
    (byte) 249,
    (byte) 6,
    (byte) 126,
    (byte) 45,
    (byte) 79,
    (byte) 182,
    (byte) 1,
    (byte) 151,
    (byte) 61,
    (byte) 78,
    (byte) 254,
    (byte) 23,
    (byte) 146,
    (byte) 156,
    (byte) 98,
    (byte) 18,
    (byte) 235,
    (byte) 7,
    (byte) 235,
    (byte) 158,
    (byte) 226,
    (byte) 208 /*0xD0*/,
    (byte) 82,
    (byte) 148,
    (byte) 194,
    (byte) 223,
    (byte) 82,
    (byte) 42,
    (byte) 118,
    (byte) 34,
    (byte) 93,
    (byte) 101,
    (byte) 189,
    (byte) 115,
    (byte) 190,
    (byte) 163,
    (byte) 192 /*0xC0*/,
    (byte) 23,
    (byte) 40,
    (byte) 44,
    (byte) 69,
    (byte) 149,
    (byte) 119,
    (byte) 10,
    (byte) 96 /*0x60*/,
    (byte) 37,
    (byte) 166,
    (byte) 110,
    (byte) 5,
    (byte) 18,
    (byte) 177,
    (byte) 152,
    (byte) 87,
    (byte) 91,
    (byte) 165,
    (byte) 128 /*0x80*/,
    (byte) 171,
    (byte) 249,
    (byte) 71,
    (byte) 254,
    (byte) 192 /*0xC0*/,
    (byte) 33,
    (byte) 56,
    (byte) 75,
    (byte) 206,
    (byte) 21,
    (byte) 16 /*0x10*/,
    (byte) 146,
    (byte) 244,
    (byte) 92,
    (byte) 77,
    (byte) 254,
    (byte) 14,
    (byte) 211,
    (byte) 181,
    (byte) 242,
    (byte) 232,
    (byte) 228,
    (byte) 149,
    (byte) 3,
    (byte) 91,
    (byte) 46,
    (byte) 79,
    (byte) 36,
    (byte) 234,
    (byte) 43,
    (byte) 180,
    (byte) 159,
    (byte) 242,
    (byte) 77,
    (byte) 120,
    (byte) 113,
    (byte) 91,
    (byte) 109,
    (byte) 102,
    (byte) 60,
    (byte) 79,
    (byte) 80 /*0x50*/,
    (byte) 204,
    (byte) 51,
    (byte) 127 /*0x7F*/,
    (byte) 233,
    (byte) 135,
    (byte) 6,
    (byte) 114,
    (byte) 92,
    (byte) 167,
    (byte) 80 /*0x50*/,
    (byte) 188,
    (byte) 106,
    (byte) 148,
    (byte) 238,
    (byte) 12,
    (byte) 101,
    (byte) 217,
    (byte) 122,
    (byte) 66,
    (byte) 198,
    (byte) 186,
    (byte) 197,
    (byte) 9,
    (byte) 111,
    (byte) 18,
    (byte) 44,
    (byte) 177,
    (byte) 103,
    (byte) 42,
    (byte) 81,
    (byte) 45,
    (byte) 72,
    (byte) 16 /*0x10*/,
    (byte) 140,
    (byte) 226,
    (byte) 140,
    (byte) 35,
    (byte) 34,
    (byte) 63 /*0x3F*/,
    (byte) 146,
    (byte) 77,
    (byte) 2,
    (byte) 213,
    (byte) 148,
    (byte) 95,
    (byte) 38,
    (byte) 20,
    (byte) 229,
    (byte) 79,
    (byte) 5,
    (byte) 108,
    (byte) 101,
    (byte) 234,
    (byte) 3,
    (byte) 33,
    (byte) 104,
    (byte) 46,
    (byte) 17,
    (byte) 234,
    (byte) 131,
    (byte) 104,
    (byte) 41,
    (byte) 113,
    (byte) 103,
    (byte) 216,
    (byte) 59,
    (byte) 95,
    (byte) 115,
    (byte) 82,
    (byte) 117,
    (byte) 191,
    (byte) 78,
    (byte) 217,
    (byte) 56,
    (byte) 178,
    (byte) 168,
    (byte) 84,
    (byte) 171,
    (byte) 238,
    (byte) 229,
    (byte) 165,
    (byte) 13,
    (byte) 223,
    (byte) 242,
    (byte) 238,
    (byte) 73,
    (byte) 209,
    (byte) 156,
    (byte) 117,
    (byte) 124,
    (byte) 131,
    (byte) 116,
    (byte) 74,
    (byte) 67,
    (byte) 156,
    (byte) 225,
    (byte) 90,
    (byte) 15,
    (byte) 149,
    (byte) 144 /*0x90*/,
    (byte) 203,
    (byte) 141,
    (byte) 254,
    (byte) 198,
    (byte) 113,
    (byte) 172,
    (byte) 249,
    (byte) 6,
    (byte) 115,
    (byte) 52,
    (byte) 223,
    (byte) 97,
    (byte) 10,
    (byte) 143,
    (byte) 253,
    (byte) 134,
    (byte) 184,
    (byte) 74,
    (byte) 129,
    (byte) 16 /*0x10*/,
    (byte) 207,
    (byte) 93,
    (byte) 17,
    (byte) 210,
    (byte) 102,
    (byte) 175,
    (byte) 160 /*0xA0*/,
    (byte) 127 /*0x7F*/,
    (byte) 14,
    (byte) 39,
    (byte) 167,
    (byte) 175,
    (byte) 104,
    (byte) 93,
    (byte) 168,
    (byte) 160 /*0xA0*/,
    (byte) 167,
    (byte) 107,
    (byte) 128 /*0x80*/,
    (byte) 45,
    (byte) 178,
    (byte) 81,
    (byte) 29,
    (byte) 176 /*0xB0*/,
    (byte) 96 /*0x60*/,
    (byte) 177,
    (byte) 175,
    (byte) 92,
    (byte) 194,
    (byte) 60,
    (byte) 120,
    (byte) 134,
    (byte) 116,
    (byte) 5,
    (byte) 201,
    (byte) 143,
    (byte) 212,
    (byte) 220,
    (byte) 239,
    (byte) 192 /*0xC0*/,
    (byte) 228,
    (byte) 134,
    (byte) 104,
    (byte) 25,
    (byte) 161,
    (byte) 221,
    (byte) 131,
    (byte) 103,
    (byte) 238,
    (byte) 151,
    (byte) 48 /*0x30*/,
    (byte) 231,
    (byte) 30,
    (byte) 71,
    (byte) 87,
    (byte) 18,
    (byte) 195
  };

  internal static string ssp_appserver_12781()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[31 /*0x1F*/];
      byte[] numArray2 = new byte[31 /*0x1F*/]
      {
        (byte) 29,
        (byte) 211,
        (byte) 168,
        (byte) 49,
        (byte) 114,
        (byte) 33,
        (byte) 202,
        (byte) 155,
        (byte) 201,
        (byte) 79,
        (byte) 161,
        (byte) 163,
        (byte) 76,
        (byte) 212,
        (byte) 221,
        (byte) 245,
        (byte) 17,
        (byte) 130,
        (byte) 125,
        (byte) 184,
        (byte) 25,
        (byte) 123,
        (byte) 157,
        (byte) 63 /*0x3F*/,
        (byte) 7,
        (byte) 166,
        (byte) 101,
        (byte) 37,
        (byte) 73,
        (byte) 209,
        (byte) 140
      };
      byte[] numArray3 = new byte[31 /*0x1F*/];
      numArray3[10] = (byte) 212;
      numArray3[17] = (byte) 6;
      numArray3[2] = (byte) 232;
      numArray3[14] = (byte) 10;
      numArray3[4] = (byte) 184;
      numArray3[21] = (byte) 106;
      numArray3[3] = (byte) 77;
      numArray3[23] = (byte) 147;
      numArray3[19] = (byte) 99;
      numArray3[8] = (byte) 201;
      numArray3[13] = (byte) 84;
      numArray3[20] = (byte) 12;
      numArray3[12] = (byte) 92;
      numArray3[29] = (byte) 202;
      numArray3[16 /*0x10*/] = (byte) 208 /*0xD0*/;
      numArray3[1] = (byte) 209;
      numArray3[15] = (byte) 232;
      numArray3[9] = (byte) 134;
      numArray3[18] = (byte) 168;
      numArray3[11] = (byte) 44;
      numArray3[5] = (byte) 193;
      numArray3[7] = (byte) 72;
      numArray3[22] = (byte) 140;
      numArray3[6] = (byte) 167;
      numArray3[24] = (byte) 52;
      numArray3[25] = (byte) 6;
      numArray3[26] = (byte) 203;
      numArray3[27] = (byte) 197;
      numArray3[28] = (byte) 123;
      numArray3[0] = (byte) 103;
      numArray3[30] = (byte) 175;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 31 /*0x1F*/);
      for (int index = 0; index < 31 /*0x1F*/; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[38];
      byte[] response = new byte[38];
      Array.Copy((Array) sc_12780.sspq, 0, (Array) numArray4, 0, 38);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_12780.sspr, 0, (Array) numArray4, 0, 38);
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
      (byte) 216,
      (byte) 188,
      (byte) 226,
      (byte) 247,
      (byte) 91,
      (byte) 73,
      (byte) 128 /*0x80*/,
      (byte) 22,
      (byte) 57,
      (byte) 178,
      (byte) 18,
      (byte) 106,
      (byte) 254,
      (byte) 44,
      (byte) 60,
      (byte) 166,
      (byte) 194,
      (byte) 23,
      (byte) 180,
      (byte) 213,
      (byte) 15,
      (byte) 8,
      (byte) 204,
      (byte) 100,
      (byte) 156,
      (byte) 101,
      (byte) 249,
      (byte) 190,
      (byte) 99,
      (byte) 61,
      (byte) 122
    };
    byte[] numArray7 = new byte[31 /*0x1F*/]
    {
      (byte) 48 /*0x30*/,
      (byte) 41,
      (byte) 151,
      (byte) 9,
      (byte) 159,
      (byte) 89,
      (byte) 159,
      (byte) 65,
      (byte) 55,
      (byte) 62,
      (byte) 110,
      (byte) 87,
      (byte) 197,
      (byte) 71,
      (byte) 57,
      (byte) 131,
      (byte) 39,
      (byte) 164,
      (byte) 111,
      (byte) 155,
      (byte) 115,
      (byte) 0,
      (byte) 93,
      (byte) 23,
      (byte) 143,
      (byte) 18,
      (byte) 3,
      (byte) 125,
      (byte) 195,
      (byte) 163,
      (byte) 110
    };
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 31 /*0x1F*/);
    for (int index = 0; index < 31 /*0x1F*/; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_appserver_12782()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[35];
      byte[] numArray2 = new byte[35]
      {
        byte.MaxValue,
        (byte) 57,
        (byte) 100,
        (byte) 30,
        (byte) 46,
        (byte) 174,
        (byte) 53,
        (byte) 8,
        (byte) 219,
        (byte) 22,
        (byte) 40,
        (byte) 216,
        (byte) 58,
        (byte) 37,
        (byte) 20,
        (byte) 47,
        (byte) 230,
        (byte) 13,
        (byte) 59,
        (byte) 20,
        (byte) 207,
        (byte) 240 /*0xF0*/,
        (byte) 126,
        (byte) 91,
        (byte) 246,
        (byte) 79,
        (byte) 193,
        (byte) 178,
        (byte) 252,
        (byte) 56,
        (byte) 26,
        (byte) 41,
        (byte) 124,
        (byte) 223,
        (byte) 61
      };
      byte[] numArray3 = new byte[35];
      numArray3[32 /*0x20*/] = (byte) 37;
      numArray3[4] = (byte) 209;
      numArray3[2] = (byte) 160 /*0xA0*/;
      numArray3[3] = (byte) 119;
      numArray3[8] = (byte) 168;
      numArray3[5] = (byte) 102;
      numArray3[6] = (byte) 202;
      numArray3[7] = (byte) 218;
      numArray3[14] = (byte) 70;
      numArray3[16 /*0x10*/] = byte.MaxValue;
      numArray3[17] = (byte) 225;
      numArray3[0] = (byte) 63 /*0x3F*/;
      numArray3[12] = (byte) 88;
      numArray3[13] = (byte) 160 /*0xA0*/;
      numArray3[9] = (byte) 34;
      numArray3[15] = (byte) 49;
      numArray3[28] = (byte) 103;
      numArray3[24] = (byte) 6;
      numArray3[18] = (byte) 204;
      numArray3[19] = (byte) 37;
      numArray3[20] = (byte) 99;
      numArray3[21] = (byte) 187;
      numArray3[22] = (byte) 33;
      numArray3[25] = (byte) 104;
      numArray3[33] = (byte) 71;
      numArray3[26] = (byte) 251;
      numArray3[31 /*0x1F*/] = (byte) 135;
      numArray3[27] = (byte) 248;
      numArray3[11] = (byte) 2;
      numArray3[30] = (byte) 17;
      numArray3[29] = (byte) 221;
      numArray3[10] = (byte) 124;
      numArray3[23] = (byte) 40;
      numArray3[34] = (byte) 37;
      numArray3[1] = (byte) 125;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 35);
      for (int index = 0; index < 35; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[35];
    byte[] numArray5 = new byte[35]
    {
      (byte) 21,
      (byte) 58,
      (byte) 207,
      (byte) 254,
      (byte) 49,
      (byte) 231,
      (byte) 105,
      (byte) 144 /*0x90*/,
      (byte) 40,
      (byte) 99,
      (byte) 238,
      (byte) 164,
      (byte) 47,
      (byte) 36,
      (byte) 13,
      (byte) 98,
      (byte) 45,
      (byte) 178,
      (byte) 84,
      (byte) 209,
      (byte) 166,
      (byte) 56,
      (byte) 86,
      (byte) 244,
      (byte) 78,
      (byte) 80 /*0x50*/,
      (byte) 117,
      (byte) 48 /*0x30*/,
      (byte) 100,
      (byte) 176 /*0xB0*/,
      (byte) 167,
      (byte) 60,
      (byte) 166,
      (byte) 99,
      (byte) 239
    };
    byte[] numArray6 = new byte[35];
    numArray6[0] = (byte) 69;
    numArray6[1] = (byte) 92;
    numArray6[20] = (byte) 83;
    numArray6[24] = (byte) 126;
    numArray6[21] = (byte) 237;
    numArray6[5] = (byte) 167;
    numArray6[2] = (byte) 91;
    numArray6[11] = (byte) 84;
    numArray6[29] = (byte) 19;
    numArray6[9] = (byte) 96 /*0x60*/;
    numArray6[10] = (byte) 180;
    numArray6[17] = (byte) 80 /*0x50*/;
    numArray6[19] = (byte) 238;
    numArray6[13] = (byte) 105;
    numArray6[15] = (byte) 164;
    numArray6[33] = (byte) 72;
    numArray6[22] = (byte) 66;
    numArray6[4] = (byte) 150;
    numArray6[18] = (byte) 83;
    numArray6[3] = (byte) 252;
    numArray6[8] = (byte) 68;
    numArray6[14] = (byte) 5;
    numArray6[31 /*0x1F*/] = (byte) 223;
    numArray6[23] = (byte) 197;
    numArray6[12] = (byte) 213;
    numArray6[34] = (byte) 75;
    numArray6[26] = (byte) 15;
    numArray6[27] = (byte) 215;
    numArray6[28] = (byte) 225;
    numArray6[6] = (byte) 251;
    numArray6[30] = (byte) 6;
    numArray6[16 /*0x10*/] = (byte) 59;
    numArray6[32 /*0x20*/] = (byte) 108;
    numArray6[7] = (byte) 45;
    numArray6[25] = (byte) 71;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 35);
    for (int index = 0; index < 35; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[26];
    byte[] response = new byte[26];
    Array.Copy((Array) sc_12780.sspq, 38, (Array) numArray7, 0, 26);
    key.Query(true, 335, numArray7, response);
    Array.Copy((Array) sc_12780.sspr, 38, (Array) numArray7, 0, 26);
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

  internal static string ssp_appserver_12783()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[31 /*0x1F*/];
      byte[] numArray2 = new byte[31 /*0x1F*/];
      numArray2[20] = (byte) 114;
      numArray2[1] = (byte) 164;
      numArray2[2] = (byte) 116;
      numArray2[29] = (byte) 54;
      numArray2[4] = (byte) 157;
      numArray2[14] = (byte) 214;
      numArray2[6] = (byte) 73;
      numArray2[23] = (byte) 137;
      numArray2[0] = (byte) 0;
      numArray2[9] = (byte) 104;
      numArray2[27] = (byte) 132;
      numArray2[11] = (byte) 3;
      numArray2[12] = (byte) 95;
      numArray2[13] = (byte) 92;
      numArray2[3] = (byte) 68;
      numArray2[15] = (byte) 87;
      numArray2[10] = (byte) 173;
      numArray2[17] = (byte) 131;
      numArray2[18] = (byte) 10;
      numArray2[19] = (byte) 242;
      numArray2[5] = (byte) 205;
      numArray2[16 /*0x10*/] = (byte) 192 /*0xC0*/;
      numArray2[22] = (byte) 34;
      numArray2[21] = (byte) 73;
      numArray2[30] = (byte) 39;
      numArray2[8] = (byte) 135;
      numArray2[26] = (byte) 126;
      numArray2[25] = (byte) 189;
      numArray2[28] = (byte) 141;
      numArray2[7] = (byte) 8;
      numArray2[24] = (byte) 225;
      byte[] numArray3 = new byte[31 /*0x1F*/];
      numArray3[23] = (byte) 141;
      numArray3[1] = (byte) 64 /*0x40*/;
      numArray3[4] = (byte) 98;
      numArray3[13] = (byte) 162;
      numArray3[0] = (byte) 57;
      numArray3[16 /*0x10*/] = (byte) 186;
      numArray3[6] = (byte) 222;
      numArray3[7] = (byte) 100;
      numArray3[14] = (byte) 86;
      numArray3[8] = (byte) 80 /*0x50*/;
      numArray3[28] = (byte) 5;
      numArray3[11] = (byte) 41;
      numArray3[12] = (byte) 100;
      numArray3[9] = (byte) 72;
      numArray3[24] = (byte) 85;
      numArray3[5] = (byte) 24;
      numArray3[10] = (byte) 41;
      numArray3[2] = (byte) 10;
      numArray3[29] = (byte) 49;
      numArray3[19] = (byte) 161;
      numArray3[20] = (byte) 28;
      numArray3[21] = (byte) 72;
      numArray3[18] = (byte) 61;
      numArray3[22] = (byte) 140;
      numArray3[3] = (byte) 104;
      numArray3[17] = (byte) 136;
      numArray3[26] = (byte) 39;
      numArray3[15] = (byte) 201;
      numArray3[25] = (byte) 76;
      numArray3[27] = (byte) 14;
      numArray3[30] = (byte) 201;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 31 /*0x1F*/);
      for (int index = 0; index < 31 /*0x1F*/; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[31 /*0x1F*/];
    byte[] numArray5 = new byte[31 /*0x1F*/]
    {
      (byte) 7,
      (byte) 169,
      (byte) 104,
      (byte) 198,
      (byte) 55,
      (byte) 240 /*0xF0*/,
      (byte) 116,
      (byte) 250,
      (byte) 67,
      (byte) 199,
      (byte) 157,
      (byte) 49,
      (byte) 251,
      (byte) 158,
      (byte) 105,
      (byte) 8,
      (byte) 3,
      (byte) 81,
      (byte) 35,
      (byte) 31 /*0x1F*/,
      (byte) 25,
      (byte) 27,
      (byte) 125,
      (byte) 226,
      (byte) 97,
      (byte) 164,
      (byte) 78,
      (byte) 14,
      (byte) 103,
      (byte) 104,
      (byte) 214
    };
    byte[] numArray6 = new byte[31 /*0x1F*/]
    {
      (byte) 116,
      (byte) 148,
      (byte) 84,
      (byte) 111,
      (byte) 76,
      (byte) 130,
      (byte) 2,
      (byte) 134,
      (byte) 88,
      (byte) 223,
      (byte) 183,
      (byte) 9,
      (byte) 131,
      (byte) 6,
      (byte) 127 /*0x7F*/,
      (byte) 51,
      (byte) 88,
      (byte) 132,
      (byte) 16 /*0x10*/,
      (byte) 236,
      (byte) 253,
      (byte) 82,
      (byte) 245,
      (byte) 190,
      (byte) 217,
      (byte) 162,
      (byte) 240 /*0xF0*/,
      (byte) 124,
      (byte) 20,
      (byte) 104,
      (byte) 54
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 31 /*0x1F*/);
    for (int index = 0; index < 31 /*0x1F*/; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[18];
    byte[] response = new byte[18];
    Array.Copy((Array) sc_12780.sspq, 64 /*0x40*/, (Array) numArray7, 0, 18);
    key.Query(true, 335, numArray7, response);
    Array.Copy((Array) sc_12780.sspr, 64 /*0x40*/, (Array) numArray7, 0, 18);
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

  internal static string ssp_appserver_12784()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[35];
      byte[] numArray2 = new byte[35];
      numArray2[11] = (byte) 233;
      numArray2[1] = (byte) 236;
      numArray2[28] = (byte) 172;
      numArray2[3] = (byte) 133;
      numArray2[13] = (byte) 46;
      numArray2[14] = (byte) 182;
      numArray2[6] = (byte) 161;
      numArray2[34] = (byte) 54;
      numArray2[16 /*0x10*/] = (byte) 248;
      numArray2[32 /*0x20*/] = (byte) 94;
      numArray2[2] = (byte) 43;
      numArray2[19] = (byte) 86;
      numArray2[8] = (byte) 146;
      numArray2[10] = (byte) 241;
      numArray2[33] = (byte) 30;
      numArray2[15] = (byte) 196;
      numArray2[21] = (byte) 104;
      numArray2[17] = (byte) 226;
      numArray2[18] = (byte) 119;
      numArray2[5] = (byte) 146;
      numArray2[20] = (byte) 98;
      numArray2[7] = (byte) 102;
      numArray2[22] = byte.MaxValue;
      numArray2[23] = (byte) 113;
      numArray2[24] = (byte) 33;
      numArray2[27] = (byte) 43;
      numArray2[26] = (byte) 172;
      numArray2[4] = (byte) 101;
      numArray2[9] = (byte) 219;
      numArray2[12] = (byte) 158;
      numArray2[30] = (byte) 233;
      numArray2[31 /*0x1F*/] = (byte) 101;
      numArray2[0] = (byte) 150;
      numArray2[25] = (byte) 24;
      numArray2[29] = (byte) 250;
      byte[] numArray3 = new byte[35];
      numArray3[23] = (byte) 163;
      numArray3[27] = (byte) 190;
      numArray3[2] = (byte) 41;
      numArray3[3] = (byte) 228;
      numArray3[4] = (byte) 189;
      numArray3[5] = (byte) 59;
      numArray3[24] = (byte) 114;
      numArray3[1] = (byte) 199;
      numArray3[29] = (byte) 182;
      numArray3[6] = (byte) 138;
      numArray3[12] = (byte) 151;
      numArray3[30] = (byte) 135;
      numArray3[11] = (byte) 62;
      numArray3[0] = (byte) 160 /*0xA0*/;
      numArray3[10] = (byte) 132;
      numArray3[15] = (byte) 29;
      numArray3[16 /*0x10*/] = (byte) 159;
      numArray3[17] = (byte) 164;
      numArray3[18] = (byte) 57;
      numArray3[9] = (byte) 93;
      numArray3[20] = (byte) 197;
      numArray3[21] = (byte) 89;
      numArray3[22] = (byte) 17;
      numArray3[8] = (byte) 170;
      numArray3[7] = (byte) 122;
      numArray3[25] = (byte) 251;
      numArray3[31 /*0x1F*/] = (byte) 176 /*0xB0*/;
      numArray3[26] = (byte) 15;
      numArray3[28] = (byte) 62;
      numArray3[13] = (byte) 31 /*0x1F*/;
      numArray3[19] = (byte) 98;
      numArray3[14] = (byte) 74;
      numArray3[32 /*0x20*/] = (byte) 39;
      numArray3[33] = (byte) 236;
      numArray3[34] = (byte) 170;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 35);
      for (int index = 0; index < 35; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[35];
    byte[] numArray5 = new byte[35]
    {
      (byte) 211,
      (byte) 5,
      (byte) 229,
      (byte) 46,
      (byte) 100,
      (byte) 25,
      (byte) 221,
      (byte) 3,
      (byte) 183,
      (byte) 169,
      (byte) 18,
      (byte) 164,
      (byte) 32 /*0x20*/,
      (byte) 42,
      (byte) 115,
      (byte) 240 /*0xF0*/,
      (byte) 172,
      (byte) 140,
      (byte) 72,
      (byte) 182,
      (byte) 204,
      (byte) 118,
      (byte) 111,
      (byte) 193,
      (byte) 112 /*0x70*/,
      (byte) 199,
      (byte) 254,
      (byte) 67,
      (byte) 15,
      (byte) 12,
      (byte) 190,
      (byte) 127 /*0x7F*/,
      (byte) 216,
      (byte) 160 /*0xA0*/,
      (byte) 141
    };
    byte[] numArray6 = new byte[35]
    {
      (byte) 41,
      (byte) 94,
      (byte) 18,
      (byte) 42,
      (byte) 179,
      (byte) 133,
      (byte) 116,
      (byte) 249,
      (byte) 68,
      (byte) 200,
      (byte) 42,
      (byte) 227,
      (byte) 49,
      (byte) 8,
      (byte) 51,
      (byte) 19,
      (byte) 218,
      (byte) 27,
      (byte) 127 /*0x7F*/,
      (byte) 230,
      (byte) 246,
      (byte) 78,
      (byte) 3,
      (byte) 90,
      (byte) 111,
      (byte) 139,
      (byte) 2,
      (byte) 103,
      (byte) 104,
      (byte) 85,
      (byte) 8,
      (byte) 34,
      (byte) 11,
      (byte) 125,
      (byte) 141
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 35);
    for (int index = 0; index < 35; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12785()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[90];
      byte[] numArray2 = new byte[55];
      numArray2[29] = (byte) 157;
      numArray2[39] = (byte) 76;
      numArray2[2] = (byte) 95;
      numArray2[3] = (byte) 253;
      numArray2[22] = (byte) 154;
      numArray2[5] = (byte) 117;
      numArray2[51] = (byte) 112 /*0x70*/;
      numArray2[30] = (byte) 108;
      numArray2[32 /*0x20*/] = (byte) 204;
      numArray2[9] = (byte) 183;
      numArray2[10] = (byte) 225;
      numArray2[20] = (byte) 156;
      numArray2[13] = (byte) 189;
      numArray2[47] = (byte) 194;
      numArray2[14] = (byte) 190;
      numArray2[42] = (byte) 221;
      numArray2[41] = (byte) 88;
      numArray2[6] = (byte) 170;
      numArray2[45] = (byte) 6;
      numArray2[53] = (byte) 33;
      numArray2[36] = (byte) 109;
      numArray2[19] = (byte) 190;
      numArray2[37] = (byte) 120;
      numArray2[23] = (byte) 243;
      numArray2[4] = (byte) 243;
      numArray2[27] = (byte) 204;
      numArray2[26] = (byte) 146;
      numArray2[48 /*0x30*/] = (byte) 25;
      numArray2[28] = (byte) 128 /*0x80*/;
      numArray2[46] = (byte) 249;
      numArray2[12] = (byte) 197;
      numArray2[31 /*0x1F*/] = (byte) 207;
      numArray2[0] = (byte) 13;
      numArray2[33] = (byte) 49;
      numArray2[34] = (byte) 59;
      numArray2[21] = (byte) 26;
      numArray2[8] = (byte) 32 /*0x20*/;
      numArray2[35] = (byte) 36;
      numArray2[38] = (byte) 124;
      numArray2[16 /*0x10*/] = (byte) 92;
      numArray2[40] = (byte) 197;
      numArray2[25] = (byte) 235;
      numArray2[15] = (byte) 254;
      numArray2[11] = (byte) 5;
      numArray2[44] = (byte) 46;
      numArray2[24] = (byte) 170;
      numArray2[43] = (byte) 139;
      numArray2[1] = (byte) 145;
      numArray2[7] = (byte) 110;
      numArray2[49] = (byte) 161;
      numArray2[50] = (byte) 48 /*0x30*/;
      numArray2[17] = (byte) 150;
      numArray2[52] = (byte) 252;
      numArray2[18] = (byte) 36;
      numArray2[54] = (byte) 189;
      byte[] numArray3 = new byte[55]
      {
        (byte) 149,
        (byte) 128 /*0x80*/,
        (byte) 94,
        (byte) 248,
        (byte) 196,
        (byte) 95,
        (byte) 247,
        (byte) 49,
        (byte) 246,
        (byte) 49,
        (byte) 101,
        (byte) 243,
        (byte) 33,
        (byte) 156,
        (byte) 134,
        (byte) 15,
        (byte) 237,
        (byte) 228,
        (byte) 110,
        (byte) 254,
        (byte) 94,
        (byte) 138,
        (byte) 80 /*0x50*/,
        (byte) 215,
        (byte) 238,
        (byte) 141,
        (byte) 206,
        (byte) 63 /*0x3F*/,
        (byte) 118,
        (byte) 240 /*0xF0*/,
        (byte) 133,
        (byte) 161,
        (byte) 123,
        (byte) 111,
        (byte) 206,
        (byte) 113,
        (byte) 225,
        (byte) 106,
        (byte) 82,
        (byte) 134,
        (byte) 78,
        (byte) 112 /*0x70*/,
        (byte) 247,
        (byte) 173,
        (byte) 91,
        (byte) 124,
        (byte) 32 /*0x20*/,
        byte.MaxValue,
        (byte) 70,
        (byte) 144 /*0x90*/,
        (byte) 158,
        (byte) 99,
        (byte) 123,
        (byte) 3,
        (byte) 62
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[35];
      numArray4[2] = (byte) 22;
      numArray4[10] = (byte) 32 /*0x20*/;
      numArray4[28] = (byte) 165;
      numArray4[3] = (byte) 19;
      numArray4[33] = (byte) 67;
      numArray4[4] = (byte) 43;
      numArray4[6] = (byte) 21;
      numArray4[32 /*0x20*/] = (byte) 146;
      numArray4[8] = (byte) 64 /*0x40*/;
      numArray4[9] = (byte) 141;
      numArray4[18] = (byte) 216;
      numArray4[11] = (byte) 56;
      numArray4[12] = (byte) 108;
      numArray4[34] = (byte) 91;
      numArray4[5] = (byte) 14;
      numArray4[15] = (byte) 166;
      numArray4[16 /*0x10*/] = (byte) 110;
      numArray4[17] = (byte) 17;
      numArray4[19] = (byte) 225;
      numArray4[13] = (byte) 143;
      numArray4[21] = (byte) 169;
      numArray4[22] = (byte) 214;
      numArray4[31 /*0x1F*/] = (byte) 249;
      numArray4[1] = (byte) 174;
      numArray4[24] = (byte) 142;
      numArray4[0] = (byte) 166;
      numArray4[26] = (byte) 207;
      numArray4[27] = (byte) 33;
      numArray4[23] = (byte) 140;
      numArray4[29] = (byte) 173;
      numArray4[30] = (byte) 131;
      numArray4[7] = (byte) 78;
      numArray4[25] = (byte) 52;
      numArray4[20] = (byte) 205;
      numArray4[14] = (byte) 190;
      byte[] numArray5 = new byte[35]
      {
        (byte) 244,
        (byte) 65,
        (byte) 98,
        (byte) 243,
        (byte) 196,
        (byte) 222,
        (byte) 194,
        (byte) 222,
        (byte) 10,
        (byte) 189,
        (byte) 111,
        (byte) 185,
        (byte) 70,
        (byte) 214,
        (byte) 231,
        (byte) 118,
        (byte) 83,
        (byte) 96 /*0x60*/,
        (byte) 126,
        (byte) 242,
        (byte) 228,
        (byte) 132,
        (byte) 125,
        (byte) 93,
        (byte) 92,
        (byte) 70,
        (byte) 151,
        (byte) 12,
        (byte) 139,
        (byte) 151,
        (byte) 97,
        (byte) 241,
        (byte) 208 /*0xD0*/,
        (byte) 38,
        (byte) 118
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 35);
      for (int index = 0; index < 35; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[90];
    byte[] numArray7 = new byte[55]
    {
      (byte) 124,
      (byte) 155,
      (byte) 218,
      (byte) 205,
      (byte) 151,
      (byte) 70,
      (byte) 134,
      (byte) 34,
      (byte) 80 /*0x50*/,
      (byte) 149,
      (byte) 245,
      (byte) 247,
      (byte) 108,
      (byte) 88,
      (byte) 189,
      (byte) 39,
      (byte) 94,
      (byte) 158,
      (byte) 210,
      (byte) 208 /*0xD0*/,
      (byte) 197,
      (byte) 30,
      (byte) 82,
      (byte) 73,
      (byte) 73,
      (byte) 52,
      (byte) 30,
      (byte) 97,
      (byte) 233,
      (byte) 75,
      (byte) 68,
      (byte) 103,
      (byte) 156,
      (byte) 193,
      (byte) 36,
      (byte) 57,
      (byte) 65,
      (byte) 47,
      (byte) 64 /*0x40*/,
      (byte) 67,
      byte.MaxValue,
      (byte) 212,
      (byte) 238,
      (byte) 94,
      (byte) 178,
      (byte) 154,
      (byte) 158,
      (byte) 205,
      (byte) 233,
      (byte) 1,
      (byte) 213,
      (byte) 169,
      (byte) 118,
      (byte) 72,
      (byte) 143
    };
    byte[] numArray8 = new byte[55]
    {
      (byte) 128 /*0x80*/,
      (byte) 183,
      (byte) 68,
      (byte) 150,
      (byte) 84,
      (byte) 13,
      (byte) 162,
      (byte) 74,
      (byte) 167,
      (byte) 164,
      (byte) 229,
      (byte) 205,
      (byte) 230,
      (byte) 73,
      (byte) 129,
      (byte) 146,
      (byte) 46,
      (byte) 105,
      (byte) 150,
      (byte) 240 /*0xF0*/,
      (byte) 160 /*0xA0*/,
      (byte) 19,
      (byte) 98,
      (byte) 129,
      (byte) 169,
      (byte) 84,
      (byte) 65,
      (byte) 189,
      (byte) 229,
      (byte) 234,
      (byte) 214,
      (byte) 76,
      (byte) 222,
      (byte) 216,
      (byte) 101,
      (byte) 4,
      (byte) 206,
      (byte) 74,
      (byte) 67,
      (byte) 68,
      (byte) 174,
      (byte) 144 /*0x90*/,
      (byte) 189,
      (byte) 248,
      (byte) 28,
      (byte) 124,
      (byte) 59,
      (byte) 108,
      (byte) 40,
      (byte) 20,
      (byte) 176 /*0xB0*/,
      (byte) 252,
      (byte) 215,
      (byte) 19,
      (byte) 201
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[35];
    numArray9[23] = (byte) 150;
    numArray9[32 /*0x20*/] = (byte) 199;
    numArray9[2] = (byte) 98;
    numArray9[3] = (byte) 40;
    numArray9[20] = (byte) 244;
    numArray9[17] = (byte) 209;
    numArray9[6] = (byte) 4;
    numArray9[7] = (byte) 80 /*0x50*/;
    numArray9[8] = (byte) 142;
    numArray9[22] = (byte) 174;
    numArray9[14] = (byte) 47;
    numArray9[11] = (byte) 52;
    numArray9[21] = (byte) 150;
    numArray9[13] = (byte) 230;
    numArray9[1] = (byte) 132;
    numArray9[29] = (byte) 240 /*0xF0*/;
    numArray9[16 /*0x10*/] = (byte) 142;
    numArray9[10] = (byte) 170;
    numArray9[18] = (byte) 240 /*0xF0*/;
    numArray9[19] = (byte) 48 /*0x30*/;
    numArray9[4] = (byte) 36;
    numArray9[15] = (byte) 94;
    numArray9[30] = (byte) 84;
    numArray9[5] = (byte) 57;
    numArray9[24] = (byte) 80 /*0x50*/;
    numArray9[25] = (byte) 120;
    numArray9[9] = (byte) 91;
    numArray9[27] = (byte) 118;
    numArray9[28] = (byte) 155;
    numArray9[12] = (byte) 131;
    numArray9[33] = (byte) 239;
    numArray9[31 /*0x1F*/] = (byte) 49;
    numArray9[26] = (byte) 231;
    numArray9[0] = (byte) 144 /*0x90*/;
    numArray9[34] = (byte) 176 /*0xB0*/;
    byte[] numArray10 = new byte[35]
    {
      (byte) 199,
      (byte) 73,
      (byte) 48 /*0x30*/,
      (byte) 40,
      (byte) 32 /*0x20*/,
      (byte) 181,
      (byte) 87,
      (byte) 134,
      (byte) 94,
      (byte) 65,
      (byte) 11,
      (byte) 135,
      (byte) 226,
      (byte) 45,
      (byte) 223,
      (byte) 64 /*0x40*/,
      (byte) 191,
      (byte) 113,
      (byte) 73,
      (byte) 45,
      (byte) 98,
      (byte) 239,
      (byte) 250,
      (byte) 0,
      (byte) 209,
      (byte) 214,
      (byte) 8,
      (byte) 20,
      (byte) 56,
      (byte) 71,
      (byte) 65,
      (byte) 79,
      (byte) 114,
      (byte) 173,
      (byte) 238
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 35);
    for (int index = 0; index < 35; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_12786()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[42];
      byte[] numArray2 = new byte[42]
      {
        (byte) 71,
        (byte) 230,
        (byte) 114,
        (byte) 111,
        (byte) 162,
        (byte) 183,
        (byte) 18,
        (byte) 204,
        (byte) 209,
        (byte) 80 /*0x50*/,
        (byte) 9,
        (byte) 220,
        (byte) 146,
        (byte) 154,
        (byte) 154,
        (byte) 182,
        (byte) 213,
        (byte) 17,
        (byte) 68,
        (byte) 46,
        (byte) 74,
        (byte) 90,
        (byte) 89,
        (byte) 134,
        (byte) 55,
        (byte) 122,
        (byte) 44,
        (byte) 209,
        (byte) 229,
        (byte) 32 /*0x20*/,
        (byte) 202,
        (byte) 115,
        (byte) 129,
        (byte) 74,
        (byte) 229,
        (byte) 111,
        (byte) 135,
        (byte) 101,
        (byte) 41,
        (byte) 32 /*0x20*/,
        (byte) 153,
        (byte) 132
      };
      byte[] numArray3 = new byte[42]
      {
        (byte) 210,
        (byte) 168,
        (byte) 100,
        (byte) 2,
        (byte) 252,
        (byte) 80 /*0x50*/,
        (byte) 52,
        (byte) 169,
        (byte) 57,
        (byte) 253,
        (byte) 79,
        (byte) 96 /*0x60*/,
        (byte) 153,
        (byte) 144 /*0x90*/,
        (byte) 86,
        (byte) 245,
        (byte) 51,
        (byte) 74,
        (byte) 20,
        (byte) 250,
        (byte) 152,
        (byte) 19,
        (byte) 30,
        (byte) 63 /*0x3F*/,
        (byte) 203,
        (byte) 171,
        (byte) 240 /*0xF0*/,
        (byte) 202,
        (byte) 78,
        (byte) 0,
        (byte) 238,
        (byte) 58,
        (byte) 199,
        (byte) 63 /*0x3F*/,
        (byte) 22,
        (byte) 41,
        (byte) 84,
        (byte) 156,
        (byte) 248,
        (byte) 185,
        (byte) 29,
        (byte) 48 /*0x30*/
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 42);
      for (int index = 0; index < 42; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[42];
    byte[] numArray5 = new byte[42];
    numArray5[3] = (byte) 249;
    numArray5[14] = (byte) 40;
    numArray5[29] = (byte) 149;
    numArray5[15] = (byte) 138;
    numArray5[4] = (byte) 126;
    numArray5[5] = (byte) 175;
    numArray5[11] = (byte) 140;
    numArray5[7] = (byte) 47;
    numArray5[10] = (byte) 236;
    numArray5[35] = (byte) 148;
    numArray5[8] = (byte) 199;
    numArray5[19] = (byte) 133;
    numArray5[0] = (byte) 42;
    numArray5[13] = (byte) 55;
    numArray5[28] = (byte) 35;
    numArray5[27] = (byte) 232;
    numArray5[1] = (byte) 230;
    numArray5[17] = (byte) 231;
    numArray5[33] = (byte) 242;
    numArray5[12] = (byte) 235;
    numArray5[20] = (byte) 149;
    numArray5[18] = (byte) 176 /*0xB0*/;
    numArray5[22] = (byte) 71;
    numArray5[23] = (byte) 180;
    numArray5[24] = (byte) 43;
    numArray5[25] = (byte) 47;
    numArray5[38] = (byte) 102;
    numArray5[30] = (byte) 197;
    numArray5[34] = (byte) 13;
    numArray5[21] = (byte) 91;
    numArray5[6] = (byte) 110;
    numArray5[40] = (byte) 91;
    numArray5[32 /*0x20*/] = (byte) 244;
    numArray5[2] = (byte) 210;
    numArray5[31 /*0x1F*/] = (byte) 157;
    numArray5[26] = (byte) 111;
    numArray5[36] = (byte) 89;
    numArray5[37] = (byte) 215;
    numArray5[9] = (byte) 10;
    numArray5[39] = (byte) 112 /*0x70*/;
    numArray5[16 /*0x10*/] = (byte) 65;
    numArray5[41] = (byte) 68;
    byte[] numArray6 = new byte[42];
    numArray6[24] = (byte) 244;
    numArray6[19] = (byte) 253;
    numArray6[2] = (byte) 15;
    numArray6[39] = (byte) 193;
    numArray6[4] = (byte) 61;
    numArray6[5] = (byte) 5;
    numArray6[29] = (byte) 152;
    numArray6[6] = (byte) 40;
    numArray6[8] = (byte) 154;
    numArray6[0] = (byte) 49;
    numArray6[27] = (byte) 67;
    numArray6[11] = (byte) 11;
    numArray6[12] = (byte) 11;
    numArray6[13] = (byte) 135;
    numArray6[14] = (byte) 168;
    numArray6[25] = (byte) 215;
    numArray6[16 /*0x10*/] = (byte) 214;
    numArray6[38] = (byte) 120;
    numArray6[3] = (byte) 173;
    numArray6[18] = (byte) 178;
    numArray6[41] = (byte) 84;
    numArray6[9] = (byte) 17;
    numArray6[23] = (byte) 121;
    numArray6[26] = (byte) 137;
    numArray6[17] = (byte) 7;
    numArray6[37] = (byte) 229;
    numArray6[10] = (byte) 14;
    numArray6[36] = (byte) 2;
    numArray6[28] = (byte) 16 /*0x10*/;
    numArray6[1] = (byte) 97;
    numArray6[30] = (byte) 153;
    numArray6[21] = (byte) 28;
    numArray6[32 /*0x20*/] = (byte) 204;
    numArray6[33] = (byte) 216;
    numArray6[34] = (byte) 252;
    numArray6[35] = (byte) 155;
    numArray6[15] = (byte) 243;
    numArray6[7] = (byte) 65;
    numArray6[40] = (byte) 143;
    numArray6[22] = (byte) 50;
    numArray6[20] = (byte) 227;
    numArray6[31 /*0x1F*/] = (byte) 12;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 42);
    for (int index = 0; index < 42; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[50];
    byte[] response = new byte[50];
    Array.Copy((Array) sc_12780.sspq, 82, (Array) numArray7, 0, 50);
    key.Query(true, 335, numArray7, response);
    Array.Copy((Array) sc_12780.sspr, 82, (Array) numArray7, 0, 50);
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

  internal static string ssp_appserver_12787()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[50];
      byte[] numArray2 = new byte[50]
      {
        (byte) 220,
        (byte) 12,
        (byte) 220,
        (byte) 30,
        (byte) 92,
        (byte) 133,
        (byte) 44,
        (byte) 34,
        (byte) 55,
        (byte) 82,
        (byte) 40,
        (byte) 171,
        (byte) 186,
        (byte) 194,
        (byte) 87,
        (byte) 9,
        (byte) 1,
        (byte) 177,
        (byte) 252,
        (byte) 208 /*0xD0*/,
        (byte) 133,
        (byte) 253,
        (byte) 145,
        (byte) 116,
        (byte) 18,
        (byte) 105,
        (byte) 240 /*0xF0*/,
        (byte) 207,
        (byte) 245,
        (byte) 166,
        (byte) 223,
        (byte) 58,
        (byte) 140,
        (byte) 82,
        (byte) 177,
        (byte) 168,
        (byte) 223,
        (byte) 65,
        (byte) 56,
        (byte) 142,
        (byte) 96 /*0x60*/,
        (byte) 47,
        (byte) 79,
        (byte) 120,
        (byte) 149,
        (byte) 120,
        (byte) 152,
        (byte) 225,
        (byte) 183,
        (byte) 238
      };
      byte[] numArray3 = new byte[50];
      numArray3[47] = (byte) 67;
      numArray3[32 /*0x20*/] = (byte) 193;
      numArray3[2] = (byte) 14;
      numArray3[8] = (byte) 177;
      numArray3[43] = (byte) 252;
      numArray3[28] = (byte) 106;
      numArray3[3] = (byte) 135;
      numArray3[7] = (byte) 235;
      numArray3[23] = (byte) 151;
      numArray3[9] = (byte) 73;
      numArray3[31 /*0x1F*/] = (byte) 12;
      numArray3[1] = (byte) 245;
      numArray3[12] = (byte) 101;
      numArray3[22] = (byte) 248;
      numArray3[14] = (byte) 246;
      numArray3[33] = (byte) 17;
      numArray3[21] = (byte) 153;
      numArray3[17] = (byte) 118;
      numArray3[35] = (byte) 183;
      numArray3[0] = (byte) 146;
      numArray3[11] = (byte) 248;
      numArray3[25] = (byte) 86;
      numArray3[44] = (byte) 144 /*0x90*/;
      numArray3[41] = (byte) 217;
      numArray3[24] = (byte) 127 /*0x7F*/;
      numArray3[13] = (byte) 236;
      numArray3[26] = (byte) 73;
      numArray3[6] = (byte) 103;
      numArray3[19] = (byte) 9;
      numArray3[4] = (byte) 57;
      numArray3[30] = (byte) 15;
      numArray3[15] = (byte) 234;
      numArray3[5] = (byte) 56;
      numArray3[18] = (byte) 18;
      numArray3[34] = (byte) 198;
      numArray3[20] = (byte) 42;
      numArray3[36] = (byte) 99;
      numArray3[16 /*0x10*/] = (byte) 141;
      numArray3[38] = (byte) 212;
      numArray3[39] = (byte) 131;
      numArray3[46] = (byte) 157;
      numArray3[29] = (byte) 138;
      numArray3[10] = (byte) 192 /*0xC0*/;
      numArray3[40] = (byte) 173;
      numArray3[27] = (byte) 86;
      numArray3[45] = (byte) 183;
      numArray3[37] = (byte) 80 /*0x50*/;
      numArray3[42] = (byte) 101;
      numArray3[48 /*0x30*/] = (byte) 143;
      numArray3[49] = (byte) 83;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 50);
      for (int index = 0; index < 50; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[50];
    byte[] numArray5 = new byte[50]
    {
      (byte) 8,
      (byte) 196,
      (byte) 94,
      (byte) 149,
      (byte) 113,
      (byte) 2,
      (byte) 65,
      (byte) 49,
      (byte) 98,
      (byte) 137,
      (byte) 113,
      (byte) 34,
      (byte) 132,
      (byte) 69,
      (byte) 39,
      (byte) 9,
      (byte) 161,
      (byte) 147,
      (byte) 4,
      (byte) 172,
      (byte) 62,
      (byte) 130,
      (byte) 179,
      (byte) 138,
      (byte) 50,
      (byte) 116,
      (byte) 107,
      (byte) 83,
      (byte) 238,
      (byte) 19,
      (byte) 56,
      (byte) 65,
      (byte) 160 /*0xA0*/,
      (byte) 63 /*0x3F*/,
      (byte) 15,
      (byte) 65,
      (byte) 40,
      (byte) 59,
      (byte) 233,
      (byte) 24,
      (byte) 122,
      (byte) 160 /*0xA0*/,
      (byte) 203,
      (byte) 237,
      (byte) 196,
      (byte) 35,
      (byte) 117,
      (byte) 190,
      (byte) 218,
      (byte) 182
    };
    byte[] numArray6 = new byte[50]
    {
      (byte) 117,
      (byte) 190,
      (byte) 142,
      (byte) 147,
      (byte) 195,
      (byte) 81,
      (byte) 190,
      (byte) 38,
      (byte) 234,
      (byte) 20,
      (byte) 200,
      (byte) 53,
      (byte) 21,
      (byte) 79,
      (byte) 202,
      (byte) 26,
      (byte) 59,
      (byte) 245,
      (byte) 4,
      (byte) 75,
      (byte) 29,
      (byte) 18,
      (byte) 69,
      (byte) 229,
      (byte) 221,
      (byte) 191,
      (byte) 209,
      (byte) 95,
      (byte) 50,
      (byte) 6,
      (byte) 221,
      (byte) 117,
      (byte) 201,
      (byte) 204,
      (byte) 12,
      (byte) 250,
      (byte) 202,
      (byte) 79,
      (byte) 212,
      (byte) 72,
      (byte) 111,
      (byte) 201,
      (byte) 207,
      (byte) 36,
      (byte) 245,
      (byte) 101,
      (byte) 203,
      (byte) 50,
      (byte) 155,
      (byte) 242
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 50);
    for (int index = 0; index < 50; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12788()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[70];
      byte[] numArray2 = new byte[55]
      {
        (byte) 145,
        (byte) 133,
        (byte) 17,
        (byte) 211,
        (byte) 99,
        (byte) 253,
        (byte) 151,
        (byte) 237,
        (byte) 67,
        (byte) 78,
        (byte) 81,
        (byte) 47,
        (byte) 129,
        (byte) 68,
        (byte) 53,
        (byte) 230,
        (byte) 224 /*0xE0*/,
        (byte) 101,
        (byte) 170,
        (byte) 236,
        (byte) 50,
        (byte) 157,
        (byte) 183,
        (byte) 197,
        (byte) 197,
        (byte) 14,
        (byte) 254,
        (byte) 172,
        (byte) 121,
        (byte) 56,
        (byte) 175,
        (byte) 37,
        (byte) 236,
        (byte) 29,
        (byte) 173,
        (byte) 138,
        (byte) 48 /*0x30*/,
        (byte) 85,
        (byte) 216,
        (byte) 236,
        (byte) 129,
        (byte) 140,
        (byte) 93,
        (byte) 124,
        (byte) 156,
        (byte) 148,
        (byte) 162,
        (byte) 49,
        (byte) 137,
        (byte) 244,
        (byte) 213,
        (byte) 254,
        (byte) 104,
        (byte) 104,
        (byte) 81
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 139,
        (byte) 122,
        (byte) 166,
        (byte) 9,
        (byte) 65,
        (byte) 194,
        (byte) 121,
        (byte) 133,
        (byte) 135,
        (byte) 75,
        (byte) 19,
        (byte) 48 /*0x30*/,
        (byte) 48 /*0x30*/,
        (byte) 95,
        (byte) 71,
        (byte) 165,
        (byte) 41,
        (byte) 136,
        (byte) 171,
        (byte) 223,
        (byte) 136,
        (byte) 89,
        (byte) 6,
        (byte) 94,
        (byte) 82,
        (byte) 121,
        (byte) 2,
        (byte) 243,
        (byte) 147,
        (byte) 184,
        (byte) 226,
        (byte) 63 /*0x3F*/,
        (byte) 2,
        (byte) 233,
        (byte) 173,
        (byte) 86,
        (byte) 115,
        (byte) 215,
        (byte) 29,
        (byte) 15,
        (byte) 210,
        (byte) 35,
        (byte) 254,
        (byte) 138,
        (byte) 166,
        (byte) 24,
        (byte) 2,
        (byte) 78,
        (byte) 6,
        (byte) 66,
        (byte) 136,
        (byte) 26,
        (byte) 63 /*0x3F*/,
        (byte) 110,
        (byte) 45
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[15]
      {
        (byte) 75,
        (byte) 14,
        (byte) 84,
        (byte) 254,
        (byte) 234,
        (byte) 116,
        (byte) 10,
        (byte) 159,
        (byte) 44,
        (byte) 67,
        (byte) 100,
        (byte) 150,
        (byte) 171,
        (byte) 181,
        (byte) 138
      };
      byte[] numArray5 = new byte[15]
      {
        (byte) 21,
        (byte) 179,
        (byte) 119,
        (byte) 188,
        (byte) 159,
        (byte) 244,
        (byte) 193,
        (byte) 253,
        (byte) 61,
        (byte) 229,
        (byte) 227,
        (byte) 122,
        (byte) 28,
        (byte) 148,
        (byte) 77
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 15);
      for (int index = 0; index < 15; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[70];
    byte[] numArray7 = new byte[55];
    numArray7[21] = (byte) 23;
    numArray7[18] = (byte) 131;
    numArray7[3] = (byte) 41;
    numArray7[17] = (byte) 71;
    numArray7[54] = (byte) 211;
    numArray7[5] = (byte) 108;
    numArray7[6] = (byte) 205;
    numArray7[4] = (byte) 49;
    numArray7[8] = (byte) 132;
    numArray7[9] = (byte) 98;
    numArray7[10] = (byte) 176 /*0xB0*/;
    numArray7[36] = (byte) 146;
    numArray7[12] = (byte) 131;
    numArray7[45] = (byte) 112 /*0x70*/;
    numArray7[40] = (byte) 49;
    numArray7[51] = (byte) 67;
    numArray7[16 /*0x10*/] = (byte) 228;
    numArray7[32 /*0x20*/] = (byte) 171;
    numArray7[44] = (byte) 239;
    numArray7[19] = (byte) 237;
    numArray7[20] = (byte) 150;
    numArray7[47] = (byte) 152;
    numArray7[22] = (byte) 82;
    numArray7[0] = (byte) 165;
    numArray7[24] = (byte) 111;
    numArray7[25] = (byte) 81;
    numArray7[26] = (byte) 62;
    numArray7[23] = (byte) 220;
    numArray7[28] = (byte) 221;
    numArray7[29] = (byte) 62;
    numArray7[30] = (byte) 74;
    numArray7[11] = (byte) 188;
    numArray7[14] = (byte) 185;
    numArray7[7] = (byte) 35;
    numArray7[2] = (byte) 59;
    numArray7[35] = (byte) 180;
    numArray7[46] = (byte) 191;
    numArray7[37] = (byte) 33;
    numArray7[38] = (byte) 221;
    numArray7[39] = (byte) 232;
    numArray7[1] = (byte) 64 /*0x40*/;
    numArray7[33] = (byte) 253;
    numArray7[52] = (byte) 9;
    numArray7[13] = (byte) 203;
    numArray7[34] = (byte) 171;
    numArray7[41] = (byte) 199;
    numArray7[49] = (byte) 239;
    numArray7[31 /*0x1F*/] = (byte) 113;
    numArray7[48 /*0x30*/] = (byte) 221;
    numArray7[43] = (byte) 168;
    numArray7[50] = (byte) 209;
    numArray7[42] = (byte) 239;
    numArray7[15] = (byte) 187;
    numArray7[27] = (byte) 154;
    numArray7[53] = (byte) 207;
    byte[] numArray8 = new byte[55]
    {
      (byte) 187,
      (byte) 197,
      (byte) 62,
      (byte) 37,
      (byte) 4,
      (byte) 134,
      (byte) 203,
      (byte) 247,
      (byte) 47,
      (byte) 34,
      (byte) 83,
      (byte) 100,
      (byte) 234,
      (byte) 73,
      (byte) 182,
      (byte) 2,
      (byte) 70,
      (byte) 87,
      (byte) 111,
      (byte) 137,
      (byte) 26,
      (byte) 61,
      (byte) 75,
      (byte) 166,
      (byte) 100,
      (byte) 242,
      (byte) 219,
      (byte) 55,
      (byte) 44,
      (byte) 89,
      (byte) 163,
      (byte) 242,
      (byte) 245,
      (byte) 89,
      (byte) 167,
      (byte) 55,
      (byte) 192 /*0xC0*/,
      (byte) 210,
      (byte) 109,
      (byte) 94,
      (byte) 109,
      (byte) 161,
      (byte) 228,
      (byte) 179,
      (byte) 40,
      (byte) 208 /*0xD0*/,
      (byte) 252,
      (byte) 65,
      (byte) 209,
      (byte) 104,
      (byte) 183,
      (byte) 31 /*0x1F*/,
      (byte) 3,
      (byte) 244,
      (byte) 27
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[15];
    numArray9[0] = (byte) 113;
    numArray9[1] = (byte) 185;
    numArray9[13] = (byte) 244;
    numArray9[10] = (byte) 91;
    numArray9[3] = (byte) 135;
    numArray9[5] = (byte) 88;
    numArray9[6] = (byte) 217;
    numArray9[7] = (byte) 16 /*0x10*/;
    numArray9[8] = (byte) 215;
    numArray9[11] = (byte) 2;
    numArray9[12] = (byte) 62;
    numArray9[2] = (byte) 77;
    numArray9[9] = (byte) 53;
    numArray9[4] = (byte) 117;
    numArray9[14] = (byte) 91;
    byte[] numArray10 = new byte[15]
    {
      (byte) 197,
      (byte) 207,
      (byte) 225,
      (byte) 49,
      (byte) 192 /*0xC0*/,
      (byte) 238,
      (byte) 180,
      (byte) 58,
      (byte) 133,
      (byte) 48 /*0x30*/,
      (byte) 171,
      (byte) 213,
      (byte) 127 /*0x7F*/,
      (byte) 230,
      (byte) 236
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 15);
    for (int index = 0; index < 15; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_12789()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[42];
      byte[] numArray2 = new byte[42]
      {
        (byte) 194,
        (byte) 173,
        (byte) 175,
        (byte) 51,
        (byte) 136,
        (byte) 40,
        (byte) 208 /*0xD0*/,
        (byte) 128 /*0x80*/,
        (byte) 216,
        (byte) 41,
        (byte) 221,
        (byte) 118,
        (byte) 152,
        (byte) 210,
        (byte) 179,
        (byte) 109,
        (byte) 143,
        (byte) 86,
        (byte) 182,
        (byte) 231,
        (byte) 41,
        (byte) 52,
        (byte) 25,
        (byte) 245,
        (byte) 89,
        (byte) 144 /*0x90*/,
        (byte) 49,
        (byte) 122,
        (byte) 227,
        (byte) 222,
        (byte) 109,
        (byte) 54,
        (byte) 74,
        (byte) 186,
        (byte) 50,
        (byte) 173,
        (byte) 189,
        (byte) 172,
        (byte) 52,
        (byte) 37,
        (byte) 174,
        (byte) 152
      };
      byte[] numArray3 = new byte[42];
      numArray3[0] = (byte) 211;
      numArray3[3] = (byte) 56;
      numArray3[2] = (byte) 84;
      numArray3[23] = (byte) 211;
      numArray3[4] = (byte) 163;
      numArray3[24] = (byte) 185;
      numArray3[6] = (byte) 78;
      numArray3[7] = (byte) 64 /*0x40*/;
      numArray3[28] = (byte) 175;
      numArray3[1] = (byte) 135;
      numArray3[10] = (byte) 254;
      numArray3[11] = (byte) 71;
      numArray3[32 /*0x20*/] = (byte) 183;
      numArray3[13] = (byte) 130;
      numArray3[38] = (byte) 72;
      numArray3[15] = (byte) 24;
      numArray3[20] = (byte) 78;
      numArray3[17] = (byte) 159;
      numArray3[5] = (byte) 20;
      numArray3[19] = (byte) 82;
      numArray3[16 /*0x10*/] = (byte) 5;
      numArray3[21] = (byte) 205;
      numArray3[18] = (byte) 124;
      numArray3[8] = (byte) 236;
      numArray3[22] = (byte) 224 /*0xE0*/;
      numArray3[30] = (byte) 196;
      numArray3[26] = (byte) 244;
      numArray3[14] = (byte) 249;
      numArray3[27] = (byte) 155;
      numArray3[29] = (byte) 72;
      numArray3[31 /*0x1F*/] = (byte) 205;
      numArray3[34] = (byte) 128 /*0x80*/;
      numArray3[12] = (byte) 226;
      numArray3[33] = (byte) 7;
      numArray3[9] = (byte) 112 /*0x70*/;
      numArray3[35] = (byte) 178;
      numArray3[36] = (byte) 136;
      numArray3[37] = (byte) 217;
      numArray3[39] = (byte) 33;
      numArray3[25] = (byte) 57;
      numArray3[40] = (byte) 33;
      numArray3[41] = (byte) 154;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 42);
      for (int index = 0; index < 42; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[28];
      byte[] response = new byte[28];
      Array.Copy((Array) sc_12780.sspq, 132, (Array) numArray4, 0, 28);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_12780.sspr, 132, (Array) numArray4, 0, 28);
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
    byte[] numArray6 = new byte[42];
    numArray6[2] = (byte) 132;
    numArray6[8] = (byte) 237;
    numArray6[17] = (byte) 167;
    numArray6[6] = (byte) 124;
    numArray6[22] = (byte) 19;
    numArray6[5] = (byte) 177;
    numArray6[0] = (byte) 134;
    numArray6[41] = (byte) 185;
    numArray6[10] = (byte) 15;
    numArray6[20] = (byte) 39;
    numArray6[11] = (byte) 230;
    numArray6[4] = (byte) 224 /*0xE0*/;
    numArray6[12] = (byte) 98;
    numArray6[13] = (byte) 225;
    numArray6[14] = (byte) 149;
    numArray6[15] = (byte) 101;
    numArray6[16 /*0x10*/] = (byte) 232;
    numArray6[23] = (byte) 238;
    numArray6[18] = (byte) 22;
    numArray6[19] = (byte) 212;
    numArray6[32 /*0x20*/] = (byte) 133;
    numArray6[3] = (byte) 191;
    numArray6[39] = (byte) 219;
    numArray6[30] = (byte) 196;
    numArray6[26] = (byte) 227;
    numArray6[25] = (byte) 231;
    numArray6[31 /*0x1F*/] = (byte) 155;
    numArray6[40] = (byte) 242;
    numArray6[1] = (byte) 122;
    numArray6[33] = (byte) 19;
    numArray6[7] = (byte) 251;
    numArray6[27] = (byte) 152;
    numArray6[38] = (byte) 243;
    numArray6[24] = (byte) 173;
    numArray6[34] = (byte) 209;
    numArray6[35] = (byte) 129;
    numArray6[36] = (byte) 230;
    numArray6[37] = (byte) 250;
    numArray6[21] = (byte) 41;
    numArray6[28] = (byte) 231;
    numArray6[29] = (byte) 38;
    numArray6[9] = (byte) 172;
    byte[] numArray7 = new byte[42];
    numArray7[33] = (byte) 41;
    numArray7[8] = (byte) 33;
    numArray7[2] = (byte) 168;
    numArray7[3] = (byte) 235;
    numArray7[16 /*0x10*/] = (byte) 25;
    numArray7[4] = (byte) 144 /*0x90*/;
    numArray7[19] = (byte) 175;
    numArray7[7] = (byte) 106;
    numArray7[31 /*0x1F*/] = (byte) 141;
    numArray7[9] = (byte) 254;
    numArray7[12] = (byte) 186;
    numArray7[27] = (byte) 201;
    numArray7[14] = (byte) 15;
    numArray7[13] = (byte) 169;
    numArray7[23] = (byte) 235;
    numArray7[15] = (byte) 179;
    numArray7[39] = (byte) 216;
    numArray7[25] = (byte) 105;
    numArray7[0] = (byte) 189;
    numArray7[22] = (byte) 66;
    numArray7[1] = (byte) 13;
    numArray7[21] = (byte) 106;
    numArray7[18] = (byte) 213;
    numArray7[36] = (byte) 73;
    numArray7[24] = (byte) 18;
    numArray7[32 /*0x20*/] = (byte) 125;
    numArray7[26] = (byte) 77;
    numArray7[10] = (byte) 25;
    numArray7[41] = (byte) 170;
    numArray7[29] = (byte) 151;
    numArray7[30] = (byte) 218;
    numArray7[5] = (byte) 165;
    numArray7[20] = (byte) 47;
    numArray7[17] = (byte) 144 /*0x90*/;
    numArray7[34] = (byte) 57;
    numArray7[35] = (byte) 125;
    numArray7[28] = (byte) 249;
    numArray7[37] = (byte) 92;
    numArray7[38] = (byte) 74;
    numArray7[6] = (byte) 210;
    numArray7[40] = (byte) 201;
    numArray7[11] = (byte) 171;
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 42);
    for (int index = 0; index < 42; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_appserver_12790()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[50];
      byte[] numArray2 = new byte[50];
      numArray2[38] = (byte) 141;
      numArray2[24] = (byte) 9;
      numArray2[13] = (byte) 156;
      numArray2[27] = (byte) 8;
      numArray2[0] = (byte) 199;
      numArray2[5] = (byte) 61;
      numArray2[47] = (byte) 144 /*0x90*/;
      numArray2[9] = (byte) 0;
      numArray2[8] = (byte) 182;
      numArray2[39] = (byte) 80 /*0x50*/;
      numArray2[30] = (byte) 23;
      numArray2[32 /*0x20*/] = (byte) 184;
      numArray2[12] = (byte) 164;
      numArray2[34] = (byte) 188;
      numArray2[31 /*0x1F*/] = (byte) 41;
      numArray2[15] = (byte) 99;
      numArray2[16 /*0x10*/] = (byte) 49;
      numArray2[17] = (byte) 29;
      numArray2[23] = (byte) 190;
      numArray2[19] = (byte) 111;
      numArray2[20] = (byte) 141;
      numArray2[21] = (byte) 34;
      numArray2[29] = (byte) 170;
      numArray2[18] = (byte) 109;
      numArray2[49] = (byte) 150;
      numArray2[48 /*0x30*/] = (byte) 22;
      numArray2[26] = (byte) 0;
      numArray2[2] = (byte) 53;
      numArray2[3] = (byte) 116;
      numArray2[1] = (byte) 126;
      numArray2[10] = (byte) 73;
      numArray2[43] = (byte) 169;
      numArray2[28] = (byte) 35;
      numArray2[33] = (byte) 44;
      numArray2[36] = (byte) 128 /*0x80*/;
      numArray2[35] = (byte) 111;
      numArray2[7] = (byte) 70;
      numArray2[41] = (byte) 57;
      numArray2[14] = (byte) 134;
      numArray2[6] = (byte) 137;
      numArray2[40] = (byte) 21;
      numArray2[22] = (byte) 72;
      numArray2[37] = (byte) 170;
      numArray2[11] = (byte) 148;
      numArray2[44] = (byte) 114;
      numArray2[45] = (byte) 101;
      numArray2[46] = (byte) 23;
      numArray2[25] = (byte) 74;
      numArray2[42] = (byte) 204;
      numArray2[4] = (byte) 62;
      byte[] numArray3 = new byte[50]
      {
        (byte) 151,
        (byte) 12,
        (byte) 41,
        (byte) 99,
        (byte) 64 /*0x40*/,
        (byte) 140,
        (byte) 236,
        (byte) 85,
        (byte) 88,
        (byte) 111,
        (byte) 106,
        (byte) 101,
        (byte) 186,
        (byte) 177,
        (byte) 201,
        (byte) 172,
        (byte) 236,
        (byte) 17,
        (byte) 69,
        (byte) 210,
        (byte) 240 /*0xF0*/,
        (byte) 15,
        (byte) 229,
        (byte) 113,
        (byte) 47,
        (byte) 82,
        (byte) 4,
        (byte) 92,
        (byte) 188,
        (byte) 149,
        (byte) 198,
        (byte) 64 /*0x40*/,
        (byte) 248,
        (byte) 93,
        (byte) 45,
        (byte) 102,
        (byte) 44,
        (byte) 183,
        (byte) 24,
        (byte) 67,
        (byte) 49,
        (byte) 125,
        (byte) 43,
        (byte) 171,
        (byte) 117,
        (byte) 151,
        (byte) 42,
        (byte) 9,
        (byte) 100,
        (byte) 134
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 50);
      for (int index = 0; index < 50; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[50];
    byte[] numArray5 = new byte[50];
    numArray5[41] = (byte) 200;
    numArray5[22] = (byte) 216;
    numArray5[2] = (byte) 116;
    numArray5[3] = (byte) 87;
    numArray5[8] = (byte) 151;
    numArray5[25] = (byte) 9;
    numArray5[24] = (byte) 246;
    numArray5[34] = (byte) 243;
    numArray5[48 /*0x30*/] = (byte) 128 /*0x80*/;
    numArray5[9] = (byte) 41;
    numArray5[10] = (byte) 199;
    numArray5[11] = (byte) 192 /*0xC0*/;
    numArray5[12] = (byte) 204;
    numArray5[31 /*0x1F*/] = (byte) 25;
    numArray5[30] = (byte) 250;
    numArray5[28] = (byte) 149;
    numArray5[15] = (byte) 93;
    numArray5[17] = (byte) 67;
    numArray5[18] = (byte) 224 /*0xE0*/;
    numArray5[19] = (byte) 146;
    numArray5[0] = byte.MaxValue;
    numArray5[21] = (byte) 220;
    numArray5[45] = (byte) 216;
    numArray5[23] = (byte) 72;
    numArray5[20] = (byte) 200;
    numArray5[16 /*0x10*/] = (byte) 184;
    numArray5[5] = (byte) 99;
    numArray5[13] = (byte) 141;
    numArray5[27] = (byte) 62;
    numArray5[29] = (byte) 221;
    numArray5[7] = (byte) 79;
    numArray5[47] = (byte) 6;
    numArray5[1] = (byte) 179;
    numArray5[33] = (byte) 190;
    numArray5[36] = (byte) 58;
    numArray5[39] = (byte) 151;
    numArray5[6] = (byte) 241;
    numArray5[4] = (byte) 47;
    numArray5[35] = (byte) 218;
    numArray5[26] = (byte) 197;
    numArray5[40] = (byte) 244;
    numArray5[32 /*0x20*/] = (byte) 147;
    numArray5[44] = (byte) 119;
    numArray5[43] = (byte) 224 /*0xE0*/;
    numArray5[14] = (byte) 149;
    numArray5[38] = (byte) 172;
    numArray5[46] = (byte) 70;
    numArray5[42] = (byte) 162;
    numArray5[37] = (byte) 216;
    numArray5[49] = (byte) 99;
    byte[] numArray6 = new byte[50];
    numArray6[35] = (byte) 121;
    numArray6[44] = (byte) 34;
    numArray6[21] = (byte) 5;
    numArray6[27] = (byte) 136;
    numArray6[4] = (byte) 26;
    numArray6[5] = (byte) 36;
    numArray6[6] = (byte) 96 /*0x60*/;
    numArray6[7] = (byte) 52;
    numArray6[28] = (byte) 36;
    numArray6[25] = (byte) 203;
    numArray6[10] = (byte) 153;
    numArray6[11] = (byte) 246;
    numArray6[12] = (byte) 173;
    numArray6[13] = (byte) 115;
    numArray6[17] = (byte) 215;
    numArray6[22] = (byte) 59;
    numArray6[16 /*0x10*/] = (byte) 29;
    numArray6[49] = (byte) 35;
    numArray6[23] = (byte) 218;
    numArray6[2] = (byte) 84;
    numArray6[32 /*0x20*/] = (byte) 130;
    numArray6[45] = (byte) 122;
    numArray6[0] = (byte) 51;
    numArray6[8] = (byte) 133;
    numArray6[24] = (byte) 24;
    numArray6[41] = (byte) 194;
    numArray6[31 /*0x1F*/] = (byte) 157;
    numArray6[26] = (byte) 240 /*0xF0*/;
    numArray6[1] = (byte) 19;
    numArray6[29] = (byte) 170;
    numArray6[30] = (byte) 119;
    numArray6[15] = (byte) 40;
    numArray6[18] = (byte) 26;
    numArray6[33] = (byte) 65;
    numArray6[34] = (byte) 193;
    numArray6[48 /*0x30*/] = (byte) 227;
    numArray6[36] = (byte) 34;
    numArray6[37] = (byte) 222;
    numArray6[46] = (byte) 196;
    numArray6[3] = (byte) 215;
    numArray6[40] = (byte) 45;
    numArray6[14] = (byte) 230;
    numArray6[42] = (byte) 143;
    numArray6[43] = (byte) 27;
    numArray6[9] = (byte) 60;
    numArray6[39] = (byte) 63 /*0x3F*/;
    numArray6[19] = (byte) 196;
    numArray6[47] = (byte) 54;
    numArray6[38] = (byte) 131;
    numArray6[20] = (byte) 119;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 50);
    for (int index = 0; index < 50; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12791()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[70];
      byte[] numArray2 = new byte[55];
      numArray2[36] = (byte) 135;
      numArray2[3] = (byte) 161;
      numArray2[29] = (byte) 55;
      numArray2[33] = (byte) 216;
      numArray2[35] = (byte) 109;
      numArray2[34] = (byte) 124;
      numArray2[0] = (byte) 139;
      numArray2[7] = (byte) 102;
      numArray2[23] = (byte) 72;
      numArray2[9] = (byte) 19;
      numArray2[32 /*0x20*/] = (byte) 78;
      numArray2[11] = (byte) 151;
      numArray2[12] = (byte) 154;
      numArray2[13] = (byte) 83;
      numArray2[26] = (byte) 96 /*0x60*/;
      numArray2[39] = (byte) 195;
      numArray2[16 /*0x10*/] = (byte) 141;
      numArray2[17] = (byte) 252;
      numArray2[18] = (byte) 150;
      numArray2[31 /*0x1F*/] = (byte) 106;
      numArray2[20] = (byte) 204;
      numArray2[43] = (byte) 4;
      numArray2[22] = (byte) 141;
      numArray2[54] = (byte) 163;
      numArray2[10] = (byte) 246;
      numArray2[25] = (byte) 157;
      numArray2[19] = (byte) 58;
      numArray2[52] = (byte) 240 /*0xF0*/;
      numArray2[28] = (byte) 237;
      numArray2[27] = (byte) 49;
      numArray2[30] = (byte) 13;
      numArray2[51] = (byte) 80 /*0x50*/;
      numArray2[47] = (byte) 95;
      numArray2[8] = (byte) 15;
      numArray2[1] = (byte) 5;
      numArray2[50] = (byte) 140;
      numArray2[4] = (byte) 10;
      numArray2[14] = (byte) 206;
      numArray2[38] = (byte) 237;
      numArray2[37] = (byte) 176 /*0xB0*/;
      numArray2[15] = (byte) 218;
      numArray2[41] = (byte) 14;
      numArray2[53] = (byte) 200;
      numArray2[21] = (byte) 45;
      numArray2[44] = (byte) 247;
      numArray2[45] = (byte) 80 /*0x50*/;
      numArray2[46] = (byte) 131;
      numArray2[42] = (byte) 0;
      numArray2[48 /*0x30*/] = (byte) 103;
      numArray2[49] = (byte) 72;
      numArray2[6] = (byte) 193;
      numArray2[24] = (byte) 227;
      numArray2[5] = (byte) 44;
      numArray2[2] = (byte) 1;
      numArray2[40] = (byte) 233;
      byte[] numArray3 = new byte[55];
      numArray3[9] = (byte) 226;
      numArray3[1] = (byte) 62;
      numArray3[39] = (byte) 158;
      numArray3[3] = (byte) 129;
      numArray3[4] = (byte) 199;
      numArray3[5] = (byte) 216;
      numArray3[30] = (byte) 210;
      numArray3[49] = (byte) 201;
      numArray3[29] = (byte) 65;
      numArray3[50] = (byte) 215;
      numArray3[18] = (byte) 157;
      numArray3[32 /*0x20*/] = (byte) 217;
      numArray3[12] = (byte) 245;
      numArray3[44] = (byte) 214;
      numArray3[14] = (byte) 211;
      numArray3[21] = (byte) 223;
      numArray3[11] = (byte) 104;
      numArray3[17] = (byte) 166;
      numArray3[51] = (byte) 199;
      numArray3[19] = (byte) 7;
      numArray3[20] = (byte) 142;
      numArray3[46] = (byte) 191;
      numArray3[22] = (byte) 54;
      numArray3[10] = (byte) 64 /*0x40*/;
      numArray3[43] = (byte) 176 /*0xB0*/;
      numArray3[25] = (byte) 245;
      numArray3[26] = (byte) 103;
      numArray3[27] = (byte) 174;
      numArray3[28] = (byte) 189;
      numArray3[6] = (byte) 125;
      numArray3[0] = (byte) 67;
      numArray3[52] = (byte) 4;
      numArray3[53] = (byte) 43;
      numArray3[31 /*0x1F*/] = (byte) 197;
      numArray3[34] = (byte) 156;
      numArray3[35] = (byte) 121;
      numArray3[36] = (byte) 162;
      numArray3[8] = (byte) 176 /*0xB0*/;
      numArray3[38] = (byte) 125;
      numArray3[16 /*0x10*/] = (byte) 178;
      numArray3[40] = (byte) 59;
      numArray3[15] = (byte) 145;
      numArray3[33] = (byte) 129;
      numArray3[24] = (byte) 242;
      numArray3[7] = (byte) 25;
      numArray3[45] = (byte) 166;
      numArray3[23] = (byte) 61;
      numArray3[47] = (byte) 34;
      numArray3[48 /*0x30*/] = (byte) 166;
      numArray3[37] = (byte) 65;
      numArray3[2] = (byte) 248;
      numArray3[42] = (byte) 98;
      numArray3[41] = byte.MaxValue;
      numArray3[13] = (byte) 25;
      numArray3[54] = (byte) 172;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[15];
      numArray4[4] = (byte) 1;
      numArray4[9] = (byte) 179;
      numArray4[2] = (byte) 249;
      numArray4[3] = (byte) 83;
      numArray4[13] = (byte) 28;
      numArray4[0] = (byte) 129;
      numArray4[6] = (byte) 51;
      numArray4[1] = (byte) 159;
      numArray4[11] = (byte) 202;
      numArray4[5] = (byte) 39;
      numArray4[12] = (byte) 101;
      numArray4[10] = (byte) 62;
      numArray4[7] = (byte) 205;
      numArray4[8] = (byte) 182;
      numArray4[14] = (byte) 36;
      byte[] numArray5 = new byte[15]
      {
        (byte) 25,
        (byte) 13,
        (byte) 8,
        (byte) 35,
        (byte) 45,
        (byte) 63 /*0x3F*/,
        (byte) 44,
        (byte) 219,
        (byte) 59,
        (byte) 171,
        (byte) 38,
        (byte) 111,
        (byte) 68,
        (byte) 73,
        (byte) 64 /*0x40*/
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 15);
      for (int index = 0; index < 15; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[70];
    byte[] numArray7 = new byte[55];
    numArray7[38] = (byte) 215;
    numArray7[39] = (byte) 214;
    numArray7[4] = (byte) 216;
    numArray7[27] = (byte) 236;
    numArray7[30] = (byte) 155;
    numArray7[5] = (byte) 144 /*0x90*/;
    numArray7[6] = (byte) 152;
    numArray7[7] = (byte) 199;
    numArray7[21] = (byte) 143;
    numArray7[9] = (byte) 162;
    numArray7[10] = (byte) 144 /*0x90*/;
    numArray7[17] = (byte) 146;
    numArray7[1] = (byte) 228;
    numArray7[13] = (byte) 51;
    numArray7[32 /*0x20*/] = (byte) 172;
    numArray7[15] = (byte) 161;
    numArray7[16 /*0x10*/] = (byte) 82;
    numArray7[42] = (byte) 235;
    numArray7[43] = (byte) 235;
    numArray7[49] = (byte) 147;
    numArray7[20] = (byte) 35;
    numArray7[44] = (byte) 183;
    numArray7[22] = (byte) 244;
    numArray7[23] = (byte) 88;
    numArray7[28] = (byte) 254;
    numArray7[25] = (byte) 172;
    numArray7[26] = (byte) 214;
    numArray7[40] = (byte) 208 /*0xD0*/;
    numArray7[29] = (byte) 5;
    numArray7[48 /*0x30*/] = (byte) 228;
    numArray7[8] = (byte) 232;
    numArray7[47] = (byte) 247;
    numArray7[18] = (byte) 23;
    numArray7[33] = (byte) 10;
    numArray7[34] = (byte) 195;
    numArray7[35] = (byte) 55;
    numArray7[36] = (byte) 254;
    numArray7[50] = (byte) 118;
    numArray7[19] = (byte) 84;
    numArray7[0] = (byte) 162;
    numArray7[14] = (byte) 86;
    numArray7[41] = (byte) 65;
    numArray7[2] = (byte) 38;
    numArray7[45] = (byte) 48 /*0x30*/;
    numArray7[24] = (byte) 212;
    numArray7[3] = (byte) 51;
    numArray7[46] = (byte) 162;
    numArray7[11] = (byte) 245;
    numArray7[37] = (byte) 44;
    numArray7[31 /*0x1F*/] = (byte) 34;
    numArray7[12] = (byte) 5;
    numArray7[51] = (byte) 135;
    numArray7[52] = (byte) 122;
    numArray7[53] = (byte) 61;
    numArray7[54] = (byte) 156;
    byte[] numArray8 = new byte[55]
    {
      (byte) 46,
      (byte) 231,
      (byte) 195,
      (byte) 170,
      (byte) 142,
      (byte) 210,
      (byte) 52,
      (byte) 96 /*0x60*/,
      (byte) 122,
      (byte) 191,
      (byte) 231,
      (byte) 119,
      (byte) 33,
      (byte) 207,
      (byte) 214,
      (byte) 66,
      (byte) 186,
      (byte) 24,
      (byte) 216,
      (byte) 5,
      (byte) 13,
      (byte) 228,
      (byte) 29,
      (byte) 124,
      (byte) 172,
      (byte) 103,
      (byte) 47,
      (byte) 140,
      (byte) 232,
      (byte) 80 /*0x50*/,
      (byte) 60,
      (byte) 22,
      (byte) 12,
      (byte) 45,
      (byte) 90,
      (byte) 188,
      (byte) 242,
      (byte) 185,
      (byte) 121,
      (byte) 170,
      (byte) 178,
      (byte) 18,
      (byte) 104,
      (byte) 183,
      (byte) 134,
      (byte) 171,
      (byte) 23,
      (byte) 229,
      (byte) 155,
      (byte) 222,
      (byte) 248,
      (byte) 92,
      (byte) 234,
      (byte) 19,
      (byte) 208 /*0xD0*/
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[15];
    numArray9[9] = (byte) 218;
    numArray9[3] = (byte) 217;
    numArray9[7] = (byte) 242;
    numArray9[11] = (byte) 192 /*0xC0*/;
    numArray9[12] = (byte) 215;
    numArray9[5] = (byte) 94;
    numArray9[13] = (byte) 115;
    numArray9[2] = (byte) 34;
    numArray9[8] = (byte) 198;
    numArray9[6] = (byte) 84;
    numArray9[10] = (byte) 141;
    numArray9[4] = (byte) 162;
    numArray9[0] = (byte) 45;
    numArray9[1] = (byte) 188;
    numArray9[14] = (byte) 88;
    byte[] numArray10 = new byte[15];
    numArray10[1] = (byte) 29;
    numArray10[10] = (byte) 70;
    numArray10[6] = (byte) 177;
    numArray10[3] = (byte) 238;
    numArray10[9] = (byte) 166;
    numArray10[5] = (byte) 196;
    numArray10[8] = (byte) 179;
    numArray10[7] = (byte) 230;
    numArray10[0] = (byte) 91;
    numArray10[12] = (byte) 137;
    numArray10[4] = (byte) 74;
    numArray10[11] = (byte) 52;
    numArray10[2] = (byte) 128 /*0x80*/;
    numArray10[13] = (byte) 32 /*0x20*/;
    numArray10[14] = (byte) 234;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 15);
    for (int index = 0; index < 15; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_12792()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[27];
      byte[] numArray2 = new byte[27]
      {
        (byte) 188,
        (byte) 22,
        (byte) 89,
        (byte) 80 /*0x50*/,
        (byte) 211,
        (byte) 197,
        (byte) 213,
        (byte) 1,
        (byte) 191,
        (byte) 128 /*0x80*/,
        (byte) 94,
        (byte) 228,
        (byte) 22,
        (byte) 170,
        (byte) 87,
        (byte) 36,
        (byte) 218,
        (byte) 126,
        (byte) 120,
        (byte) 112 /*0x70*/,
        (byte) 71,
        (byte) 134,
        (byte) 224 /*0xE0*/,
        (byte) 220,
        (byte) 80 /*0x50*/,
        (byte) 232,
        (byte) 48 /*0x30*/
      };
      byte[] numArray3 = new byte[27]
      {
        (byte) 64 /*0x40*/,
        (byte) 65,
        (byte) 58,
        (byte) 20,
        (byte) 79,
        (byte) 5,
        (byte) 77,
        (byte) 191,
        (byte) 253,
        (byte) 168,
        (byte) 2,
        (byte) 245,
        (byte) 55,
        (byte) 250,
        (byte) 65,
        (byte) 22,
        (byte) 189,
        (byte) 55,
        (byte) 58,
        (byte) 105,
        (byte) 32 /*0x20*/,
        (byte) 177,
        (byte) 185,
        (byte) 194,
        (byte) 19,
        (byte) 93,
        (byte) 106
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 27);
      for (int index = 0; index < 27; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[27];
    byte[] numArray5 = new byte[27]
    {
      (byte) 146,
      (byte) 85,
      (byte) 252,
      (byte) 69,
      (byte) 139,
      (byte) 87,
      (byte) 91,
      (byte) 98,
      (byte) 110,
      (byte) 153,
      (byte) 44,
      (byte) 15,
      (byte) 201,
      (byte) 194,
      (byte) 35,
      (byte) 52,
      (byte) 156,
      (byte) 72,
      (byte) 43,
      (byte) 233,
      (byte) 63 /*0x3F*/,
      (byte) 71,
      (byte) 117,
      (byte) 12,
      (byte) 177,
      (byte) 53,
      (byte) 54
    };
    byte[] numArray6 = new byte[27];
    numArray6[22] = (byte) 47;
    numArray6[1] = (byte) 180;
    numArray6[26] = (byte) 230;
    numArray6[3] = (byte) 109;
    numArray6[4] = (byte) 212;
    numArray6[21] = (byte) 115;
    numArray6[12] = (byte) 165;
    numArray6[7] = (byte) 173;
    numArray6[23] = (byte) 68;
    numArray6[17] = (byte) 217;
    numArray6[18] = (byte) 130;
    numArray6[11] = (byte) 213;
    numArray6[9] = (byte) 6;
    numArray6[0] = (byte) 73;
    numArray6[15] = (byte) 210;
    numArray6[14] = (byte) 96 /*0x60*/;
    numArray6[16 /*0x10*/] = (byte) 210;
    numArray6[24] = (byte) 130;
    numArray6[25] = (byte) 254;
    numArray6[19] = (byte) 252;
    numArray6[20] = (byte) 163;
    numArray6[13] = (byte) 28;
    numArray6[8] = (byte) 16 /*0x10*/;
    numArray6[5] = (byte) 95;
    numArray6[6] = (byte) 22;
    numArray6[2] = (byte) 132;
    numArray6[10] = (byte) 244;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 27);
    for (int index = 0; index < 27; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[31 /*0x1F*/];
    byte[] response = new byte[31 /*0x1F*/];
    Array.Copy((Array) sc_12780.sspq, 160 /*0xA0*/, (Array) numArray7, 0, 31 /*0x1F*/);
    key.Query(true, 335, numArray7, response);
    Array.Copy((Array) sc_12780.sspr, 160 /*0xA0*/, (Array) numArray7, 0, 31 /*0x1F*/);
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

  internal static string ssp_appserver_12793()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[31 /*0x1F*/];
      byte[] numArray2 = new byte[31 /*0x1F*/];
      numArray2[1] = (byte) 249;
      numArray2[0] = (byte) 41;
      numArray2[22] = (byte) 38;
      numArray2[2] = (byte) 60;
      numArray2[8] = (byte) 218;
      numArray2[5] = (byte) 234;
      numArray2[4] = (byte) 222;
      numArray2[7] = (byte) 140;
      numArray2[15] = (byte) 127 /*0x7F*/;
      numArray2[18] = (byte) 163;
      numArray2[10] = (byte) 123;
      numArray2[16 /*0x10*/] = (byte) 191;
      numArray2[23] = (byte) 27;
      numArray2[11] = (byte) 106;
      numArray2[14] = (byte) 207;
      numArray2[13] = (byte) 158;
      numArray2[6] = (byte) 21;
      numArray2[17] = (byte) 125;
      numArray2[29] = (byte) 88;
      numArray2[19] = (byte) 66;
      numArray2[20] = (byte) 20;
      numArray2[9] = (byte) 25;
      numArray2[3] = (byte) 201;
      numArray2[12] = (byte) 71;
      numArray2[24] = (byte) 220;
      numArray2[25] = (byte) 8;
      numArray2[26] = (byte) 25;
      numArray2[27] = (byte) 126;
      numArray2[28] = (byte) 229;
      numArray2[21] = (byte) 73;
      numArray2[30] = (byte) 210;
      byte[] numArray3 = new byte[31 /*0x1F*/]
      {
        (byte) 59,
        (byte) 47,
        (byte) 82,
        (byte) 80 /*0x50*/,
        (byte) 193,
        (byte) 199,
        (byte) 77,
        (byte) 104,
        (byte) 103,
        (byte) 153,
        (byte) 48 /*0x30*/,
        (byte) 135,
        (byte) 31 /*0x1F*/,
        (byte) 96 /*0x60*/,
        (byte) 91,
        (byte) 22,
        (byte) 120,
        (byte) 136,
        (byte) 15,
        (byte) 24,
        (byte) 19,
        (byte) 30,
        (byte) 61,
        (byte) 244,
        (byte) 62,
        (byte) 135,
        (byte) 79,
        (byte) 215,
        (byte) 211,
        (byte) 249,
        (byte) 60
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 31 /*0x1F*/);
      for (int index = 0; index < 31 /*0x1F*/; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[31 /*0x1F*/];
    byte[] numArray5 = new byte[31 /*0x1F*/]
    {
      (byte) 78,
      (byte) 134,
      (byte) 20,
      (byte) 174,
      (byte) 124,
      (byte) 239,
      (byte) 96 /*0x60*/,
      (byte) 48 /*0x30*/,
      (byte) 108,
      (byte) 144 /*0x90*/,
      (byte) 106,
      (byte) 140,
      (byte) 119,
      (byte) 29,
      (byte) 205,
      (byte) 55,
      (byte) 244,
      (byte) 105,
      (byte) 198,
      (byte) 107,
      (byte) 247,
      (byte) 64 /*0x40*/,
      (byte) 183,
      (byte) 166,
      (byte) 15,
      (byte) 129,
      (byte) 110,
      (byte) 113,
      (byte) 119,
      (byte) 102,
      (byte) 25
    };
    byte[] numArray6 = new byte[31 /*0x1F*/]
    {
      (byte) 178,
      (byte) 2,
      (byte) 113,
      (byte) 189,
      (byte) 198,
      byte.MaxValue,
      (byte) 117,
      (byte) 213,
      (byte) 1,
      (byte) 173,
      (byte) 165,
      (byte) 201,
      (byte) 135,
      (byte) 30,
      (byte) 31 /*0x1F*/,
      (byte) 132,
      (byte) 2,
      (byte) 32 /*0x20*/,
      (byte) 181,
      (byte) 7,
      (byte) 103,
      (byte) 198,
      (byte) 118,
      (byte) 80 /*0x50*/,
      (byte) 250,
      (byte) 178,
      (byte) 66,
      (byte) 176 /*0xB0*/,
      (byte) 133,
      (byte) 19,
      (byte) 1
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 31 /*0x1F*/);
    for (int index = 0; index < 31 /*0x1F*/; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12794()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[86];
      byte[] numArray2 = new byte[55];
      numArray2[51] = (byte) 33;
      numArray2[1] = (byte) 188;
      numArray2[6] = (byte) 170;
      numArray2[38] = (byte) 150;
      numArray2[17] = (byte) 225;
      numArray2[21] = (byte) 207;
      numArray2[44] = (byte) 187;
      numArray2[7] = (byte) 127 /*0x7F*/;
      numArray2[0] = (byte) 136;
      numArray2[52] = (byte) 37;
      numArray2[10] = (byte) 84;
      numArray2[11] = (byte) 90;
      numArray2[41] = (byte) 193;
      numArray2[13] = (byte) 32 /*0x20*/;
      numArray2[14] = (byte) 157;
      numArray2[18] = (byte) 21;
      numArray2[48 /*0x30*/] = (byte) 218;
      numArray2[31 /*0x1F*/] = (byte) 81;
      numArray2[39] = (byte) 223;
      numArray2[40] = (byte) 76;
      numArray2[5] = (byte) 140;
      numArray2[15] = (byte) 220;
      numArray2[22] = (byte) 109;
      numArray2[33] = (byte) 110;
      numArray2[49] = (byte) 137;
      numArray2[8] = (byte) 155;
      numArray2[26] = (byte) 239;
      numArray2[27] = (byte) 184;
      numArray2[28] = (byte) 6;
      numArray2[12] = (byte) 3;
      numArray2[54] = (byte) 160 /*0xA0*/;
      numArray2[2] = (byte) 119;
      numArray2[47] = (byte) 81;
      numArray2[46] = (byte) 106;
      numArray2[34] = (byte) 68;
      numArray2[35] = (byte) 122;
      numArray2[53] = (byte) 23;
      numArray2[37] = (byte) 210;
      numArray2[25] = (byte) 129;
      numArray2[9] = (byte) 34;
      numArray2[20] = (byte) 14;
      numArray2[36] = (byte) 124;
      numArray2[42] = (byte) 27;
      numArray2[32 /*0x20*/] = (byte) 16 /*0x10*/;
      numArray2[16 /*0x10*/] = (byte) 45;
      numArray2[50] = (byte) 236;
      numArray2[45] = (byte) 38;
      numArray2[4] = (byte) 47;
      numArray2[30] = (byte) 103;
      numArray2[3] = (byte) 151;
      numArray2[24] = (byte) 251;
      numArray2[19] = (byte) 80 /*0x50*/;
      numArray2[29] = (byte) 119;
      numArray2[23] = (byte) 158;
      numArray2[43] = (byte) 132;
      byte[] numArray3 = new byte[55];
      numArray3[38] = (byte) 119;
      numArray3[35] = (byte) 56;
      numArray3[3] = (byte) 250;
      numArray3[47] = (byte) 217;
      numArray3[4] = (byte) 52;
      numArray3[16 /*0x10*/] = (byte) 177;
      numArray3[5] = (byte) 32 /*0x20*/;
      numArray3[7] = (byte) 201;
      numArray3[43] = (byte) 115;
      numArray3[9] = (byte) 74;
      numArray3[6] = (byte) 7;
      numArray3[19] = (byte) 18;
      numArray3[30] = (byte) 28;
      numArray3[13] = (byte) 17;
      numArray3[37] = (byte) 156;
      numArray3[15] = (byte) 31 /*0x1F*/;
      numArray3[50] = (byte) 128 /*0x80*/;
      numArray3[17] = (byte) 23;
      numArray3[42] = (byte) 219;
      numArray3[45] = (byte) 250;
      numArray3[27] = (byte) 30;
      numArray3[18] = (byte) 94;
      numArray3[22] = (byte) 145;
      numArray3[23] = (byte) 221;
      numArray3[24] = (byte) 25;
      numArray3[52] = (byte) 181;
      numArray3[26] = (byte) 30;
      numArray3[34] = (byte) 121;
      numArray3[40] = (byte) 64 /*0x40*/;
      numArray3[29] = (byte) 171;
      numArray3[21] = (byte) 209;
      numArray3[28] = (byte) 64 /*0x40*/;
      numArray3[32 /*0x20*/] = (byte) 76;
      numArray3[14] = (byte) 45;
      numArray3[10] = (byte) 164;
      numArray3[36] = (byte) 37;
      numArray3[1] = (byte) 107;
      numArray3[53] = (byte) 147;
      numArray3[31 /*0x1F*/] = (byte) 131;
      numArray3[39] = (byte) 156;
      numArray3[20] = (byte) 216;
      numArray3[41] = (byte) 121;
      numArray3[51] = (byte) 39;
      numArray3[33] = (byte) 35;
      numArray3[44] = (byte) 201;
      numArray3[25] = (byte) 207;
      numArray3[46] = (byte) 220;
      numArray3[11] = (byte) 105;
      numArray3[48 /*0x30*/] = (byte) 131;
      numArray3[49] = (byte) 55;
      numArray3[0] = (byte) 182;
      numArray3[2] = (byte) 142;
      numArray3[12] = (byte) 21;
      numArray3[8] = (byte) 182;
      numArray3[54] = (byte) 12;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[31 /*0x1F*/]
      {
        (byte) 203,
        (byte) 208 /*0xD0*/,
        (byte) 86,
        (byte) 214,
        (byte) 237,
        (byte) 169,
        (byte) 157,
        (byte) 6,
        (byte) 153,
        (byte) 108,
        (byte) 75,
        (byte) 200,
        (byte) 49,
        (byte) 14,
        (byte) 183,
        (byte) 15,
        (byte) 176 /*0xB0*/,
        (byte) 100,
        (byte) 184,
        (byte) 93,
        (byte) 203,
        (byte) 72,
        (byte) 107,
        (byte) 48 /*0x30*/,
        (byte) 2,
        (byte) 102,
        (byte) 228,
        (byte) 143,
        (byte) 247,
        (byte) 66,
        (byte) 123
      };
      byte[] numArray5 = new byte[31 /*0x1F*/]
      {
        (byte) 164,
        (byte) 108,
        (byte) 245,
        (byte) 14,
        (byte) 81,
        (byte) 157,
        (byte) 83,
        (byte) 218,
        (byte) 155,
        (byte) 227,
        (byte) 152,
        (byte) 2,
        (byte) 116,
        (byte) 112 /*0x70*/,
        (byte) 152,
        (byte) 19,
        (byte) 181,
        (byte) 115,
        (byte) 44,
        (byte) 14,
        (byte) 4,
        (byte) 107,
        (byte) 20,
        (byte) 95,
        (byte) 226,
        (byte) 136,
        (byte) 67,
        (byte) 152,
        (byte) 140,
        (byte) 65,
        (byte) 3
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 31 /*0x1F*/);
      for (int index = 0; index < 31 /*0x1F*/; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[86];
    byte[] numArray7 = new byte[55]
    {
      (byte) 225,
      (byte) 158,
      (byte) 251,
      (byte) 108,
      (byte) 88,
      (byte) 162,
      (byte) 193,
      (byte) 77,
      (byte) 72,
      (byte) 31 /*0x1F*/,
      (byte) 196,
      (byte) 93,
      (byte) 142,
      (byte) 95,
      (byte) 179,
      (byte) 102,
      (byte) 74,
      (byte) 5,
      (byte) 149,
      (byte) 235,
      (byte) 121,
      (byte) 131,
      (byte) 100,
      (byte) 37,
      (byte) 212,
      (byte) 134,
      (byte) 197,
      (byte) 14,
      (byte) 15,
      (byte) 87,
      (byte) 241,
      (byte) 30,
      (byte) 138,
      (byte) 177,
      (byte) 44,
      (byte) 25,
      (byte) 20,
      (byte) 68,
      (byte) 12,
      (byte) 22,
      (byte) 156,
      (byte) 126,
      byte.MaxValue,
      (byte) 76,
      (byte) 108,
      (byte) 164,
      (byte) 215,
      (byte) 132,
      (byte) 219,
      (byte) 144 /*0x90*/,
      (byte) 248,
      (byte) 117,
      (byte) 150,
      (byte) 83,
      (byte) 6
    };
    byte[] numArray8 = new byte[55]
    {
      (byte) 214,
      (byte) 213,
      (byte) 146,
      (byte) 143,
      (byte) 29,
      (byte) 102,
      (byte) 254,
      (byte) 217,
      (byte) 50,
      (byte) 225,
      (byte) 198,
      (byte) 146,
      (byte) 144 /*0x90*/,
      (byte) 217,
      (byte) 245,
      (byte) 66,
      byte.MaxValue,
      (byte) 40,
      (byte) 170,
      (byte) 164,
      (byte) 165,
      (byte) 0,
      (byte) 71,
      (byte) 158,
      (byte) 134,
      (byte) 175,
      (byte) 124,
      (byte) 183,
      (byte) 206,
      (byte) 220,
      (byte) 197,
      (byte) 247,
      (byte) 249,
      (byte) 70,
      (byte) 39,
      (byte) 42,
      (byte) 195,
      (byte) 72,
      (byte) 205,
      (byte) 38,
      (byte) 148,
      (byte) 248,
      (byte) 240 /*0xF0*/,
      (byte) 244,
      (byte) 36,
      (byte) 189,
      (byte) 106,
      (byte) 223,
      (byte) 110,
      (byte) 231,
      (byte) 69,
      (byte) 235,
      (byte) 130,
      (byte) 206,
      (byte) 93
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[31 /*0x1F*/];
    numArray9[1] = (byte) 151;
    numArray9[27] = (byte) 94;
    numArray9[2] = (byte) 209;
    numArray9[29] = (byte) 135;
    numArray9[4] = (byte) 178;
    numArray9[5] = (byte) 62;
    numArray9[6] = (byte) 241;
    numArray9[7] = (byte) 45;
    numArray9[11] = (byte) 51;
    numArray9[14] = (byte) 5;
    numArray9[10] = (byte) 221;
    numArray9[0] = (byte) 213;
    numArray9[12] = (byte) 154;
    numArray9[13] = (byte) 82;
    numArray9[17] = (byte) 78;
    numArray9[15] = (byte) 249;
    numArray9[9] = (byte) 211;
    numArray9[24] = (byte) 64 /*0x40*/;
    numArray9[18] = (byte) 251;
    numArray9[19] = (byte) 132;
    numArray9[25] = (byte) 50;
    numArray9[21] = (byte) 185;
    numArray9[22] = (byte) 47;
    numArray9[23] = (byte) 174;
    numArray9[8] = (byte) 165;
    numArray9[3] = (byte) 221;
    numArray9[16 /*0x10*/] = (byte) 127 /*0x7F*/;
    numArray9[20] = (byte) 23;
    numArray9[28] = (byte) 123;
    numArray9[26] = (byte) 126;
    numArray9[30] = (byte) 54;
    byte[] numArray10 = new byte[31 /*0x1F*/];
    numArray10[13] = (byte) 79;
    numArray10[1] = (byte) 207;
    numArray10[27] = (byte) 109;
    numArray10[0] = (byte) 244;
    numArray10[4] = (byte) 20;
    numArray10[3] = (byte) 34;
    numArray10[6] = (byte) 63 /*0x3F*/;
    numArray10[21] = (byte) 177;
    numArray10[8] = (byte) 57;
    numArray10[10] = (byte) 163;
    numArray10[9] = (byte) 177;
    numArray10[11] = (byte) 53;
    numArray10[16 /*0x10*/] = (byte) 75;
    numArray10[19] = (byte) 201;
    numArray10[14] = (byte) 185;
    numArray10[15] = (byte) 42;
    numArray10[2] = (byte) 115;
    numArray10[18] = (byte) 147;
    numArray10[29] = (byte) 196;
    numArray10[26] = (byte) 168;
    numArray10[20] = (byte) 15;
    numArray10[17] = (byte) 144 /*0x90*/;
    numArray10[22] = (byte) 239;
    numArray10[23] = (byte) 88;
    numArray10[24] = (byte) 239;
    numArray10[12] = (byte) 251;
    numArray10[7] = (byte) 54;
    numArray10[5] = (byte) 153;
    numArray10[28] = (byte) 83;
    numArray10[25] = (byte) 177;
    numArray10[30] = (byte) 211;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 31 /*0x1F*/);
    for (int index = 0; index < 31 /*0x1F*/; ++index)
      numArray6[index + 55] ^= numArray10[index];
    byte[] numArray11 = new byte[35];
    byte[] response = new byte[35];
    Array.Copy((Array) sc_12780.sspq, 191, (Array) numArray11, 0, 35);
    key.Query(true, 335, numArray11, response);
    Array.Copy((Array) sc_12780.sspr, 191, (Array) numArray11, 0, 35);
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

  internal static string ssp_appserver_12795()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[31 /*0x1F*/];
      byte[] numArray2 = new byte[31 /*0x1F*/]
      {
        (byte) 237,
        (byte) 4,
        (byte) 108,
        (byte) 21,
        (byte) 210,
        (byte) 64 /*0x40*/,
        (byte) 158,
        (byte) 146,
        (byte) 80 /*0x50*/,
        (byte) 143,
        (byte) 146,
        (byte) 249,
        (byte) 57,
        (byte) 169,
        (byte) 239,
        (byte) 218,
        (byte) 0,
        (byte) 85,
        (byte) 185,
        (byte) 251,
        (byte) 201,
        (byte) 52,
        (byte) 121,
        (byte) 134,
        (byte) 58,
        (byte) 132,
        (byte) 23,
        (byte) 40,
        (byte) 163,
        (byte) 20,
        (byte) 106
      };
      byte[] numArray3 = new byte[31 /*0x1F*/]
      {
        (byte) 142,
        (byte) 17,
        (byte) 91,
        (byte) 52,
        (byte) 10,
        (byte) 143,
        (byte) 130,
        (byte) 110,
        (byte) 43,
        (byte) 137,
        (byte) 149,
        (byte) 146,
        (byte) 104,
        (byte) 29,
        (byte) 126,
        (byte) 36,
        (byte) 67,
        (byte) 28,
        (byte) 41,
        (byte) 195,
        (byte) 209,
        (byte) 11,
        (byte) 22,
        (byte) 41,
        (byte) 25,
        (byte) 171,
        (byte) 60,
        (byte) 46,
        (byte) 22,
        (byte) 71,
        (byte) 2
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 31 /*0x1F*/);
      for (int index = 0; index < 31 /*0x1F*/; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[31 /*0x1F*/];
    byte[] numArray5 = new byte[31 /*0x1F*/]
    {
      (byte) 129,
      (byte) 168,
      (byte) 34,
      (byte) 105,
      (byte) 83,
      (byte) 114,
      (byte) 15,
      (byte) 215,
      (byte) 65,
      (byte) 225,
      (byte) 108,
      (byte) 246,
      (byte) 219,
      (byte) 253,
      (byte) 185,
      (byte) 33,
      (byte) 58,
      (byte) 164,
      (byte) 210,
      (byte) 77,
      (byte) 95,
      (byte) 184,
      (byte) 138,
      (byte) 229,
      (byte) 219,
      (byte) 78,
      (byte) 41,
      (byte) 117,
      (byte) 239,
      (byte) 60,
      (byte) 37
    };
    byte[] numArray6 = new byte[31 /*0x1F*/]
    {
      (byte) 38,
      (byte) 235,
      (byte) 85,
      (byte) 71,
      (byte) 18,
      (byte) 103,
      (byte) 197,
      (byte) 117,
      (byte) 226,
      (byte) 223,
      (byte) 118,
      (byte) 124,
      (byte) 49,
      (byte) 40,
      (byte) 174,
      (byte) 25,
      (byte) 192 /*0xC0*/,
      (byte) 213,
      (byte) 21,
      (byte) 215,
      (byte) 148,
      (byte) 222,
      (byte) 112 /*0x70*/,
      (byte) 205,
      (byte) 6,
      (byte) 60,
      (byte) 56,
      (byte) 145,
      (byte) 152,
      (byte) 216,
      (byte) 162
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 31 /*0x1F*/);
    for (int index = 0; index < 31 /*0x1F*/; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12796()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[118];
      byte[] numArray2 = new byte[55]
      {
        (byte) 71,
        (byte) 184,
        (byte) 150,
        (byte) 2,
        (byte) 70,
        (byte) 236,
        (byte) 241,
        (byte) 242,
        (byte) 152,
        (byte) 25,
        (byte) 68,
        (byte) 168,
        (byte) 26,
        (byte) 111,
        (byte) 129,
        (byte) 218,
        (byte) 197,
        (byte) 203,
        (byte) 147,
        (byte) 128 /*0x80*/,
        (byte) 200,
        (byte) 29,
        (byte) 60,
        (byte) 142,
        (byte) 4,
        (byte) 73,
        (byte) 154,
        (byte) 232,
        (byte) 5,
        (byte) 187,
        (byte) 120,
        (byte) 220,
        (byte) 129,
        (byte) 212,
        (byte) 114,
        (byte) 105,
        (byte) 163,
        (byte) 162,
        (byte) 140,
        (byte) 96 /*0x60*/,
        (byte) 110,
        (byte) 197,
        (byte) 242,
        (byte) 204,
        (byte) 150,
        (byte) 165,
        (byte) 14,
        (byte) 6,
        (byte) 109,
        (byte) 98,
        (byte) 95,
        (byte) 40,
        (byte) 133,
        (byte) 217,
        (byte) 96 /*0x60*/
      };
      byte[] numArray3 = new byte[55];
      numArray3[29] = (byte) 216;
      numArray3[1] = (byte) 7;
      numArray3[2] = (byte) 16 /*0x10*/;
      numArray3[51] = (byte) 133;
      numArray3[24] = (byte) 54;
      numArray3[27] = (byte) 172;
      numArray3[20] = (byte) 241;
      numArray3[7] = (byte) 149;
      numArray3[13] = (byte) 75;
      numArray3[9] = (byte) 58;
      numArray3[46] = (byte) 133;
      numArray3[31 /*0x1F*/] = (byte) 30;
      numArray3[35] = (byte) 11;
      numArray3[22] = (byte) 48 /*0x30*/;
      numArray3[0] = (byte) 207;
      numArray3[15] = (byte) 7;
      numArray3[37] = (byte) 103;
      numArray3[8] = (byte) 109;
      numArray3[34] = (byte) 189;
      numArray3[45] = (byte) 46;
      numArray3[43] = (byte) 254;
      numArray3[12] = (byte) 56;
      numArray3[25] = (byte) 105;
      numArray3[23] = (byte) 116;
      numArray3[21] = (byte) 123;
      numArray3[14] = (byte) 166;
      numArray3[26] = (byte) 192 /*0xC0*/;
      numArray3[6] = (byte) 132;
      numArray3[28] = (byte) 97;
      numArray3[44] = (byte) 77;
      numArray3[30] = (byte) 97;
      numArray3[19] = (byte) 90;
      numArray3[42] = (byte) 152;
      numArray3[33] = (byte) 252;
      numArray3[11] = (byte) 18;
      numArray3[10] = (byte) 224 /*0xE0*/;
      numArray3[36] = (byte) 20;
      numArray3[54] = (byte) 120;
      numArray3[38] = (byte) 26;
      numArray3[39] = (byte) 143;
      numArray3[40] = (byte) 190;
      numArray3[41] = (byte) 67;
      numArray3[4] = (byte) 73;
      numArray3[16 /*0x10*/] = (byte) 118;
      numArray3[3] = (byte) 51;
      numArray3[18] = (byte) 214;
      numArray3[47] = (byte) 109;
      numArray3[5] = (byte) 10;
      numArray3[48 /*0x30*/] = (byte) 67;
      numArray3[49] = (byte) 25;
      numArray3[50] = (byte) 6;
      numArray3[32 /*0x20*/] = (byte) 102;
      numArray3[52] = (byte) 201;
      numArray3[53] = (byte) 101;
      numArray3[17] = (byte) 42;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55];
      numArray4[51] = (byte) 25;
      numArray4[42] = (byte) 30;
      numArray4[2] = (byte) 193;
      numArray4[31 /*0x1F*/] = (byte) 23;
      numArray4[43] = byte.MaxValue;
      numArray4[5] = (byte) 75;
      numArray4[6] = (byte) 128 /*0x80*/;
      numArray4[7] = (byte) 81;
      numArray4[8] = (byte) 16 /*0x10*/;
      numArray4[9] = (byte) 126;
      numArray4[10] = (byte) 209;
      numArray4[27] = (byte) 31 /*0x1F*/;
      numArray4[12] = (byte) 43;
      numArray4[38] = (byte) 213;
      numArray4[14] = (byte) 126;
      numArray4[33] = (byte) 225;
      numArray4[16 /*0x10*/] = (byte) 36;
      numArray4[28] = (byte) 102;
      numArray4[18] = (byte) 42;
      numArray4[11] = (byte) 179;
      numArray4[48 /*0x30*/] = (byte) 189;
      numArray4[49] = (byte) 58;
      numArray4[45] = (byte) 85;
      numArray4[0] = (byte) 16 /*0x10*/;
      numArray4[34] = (byte) 138;
      numArray4[25] = (byte) 37;
      numArray4[26] = (byte) 119;
      numArray4[52] = (byte) 51;
      numArray4[47] = (byte) 143;
      numArray4[1] = (byte) 158;
      numArray4[17] = (byte) 173;
      numArray4[37] = (byte) 79;
      numArray4[32 /*0x20*/] = (byte) 75;
      numArray4[54] = (byte) 198;
      numArray4[15] = (byte) 175;
      numArray4[35] = (byte) 6;
      numArray4[53] = (byte) 232;
      numArray4[4] = (byte) 142;
      numArray4[13] = (byte) 156;
      numArray4[30] = (byte) 224 /*0xE0*/;
      numArray4[40] = (byte) 149;
      numArray4[41] = (byte) 218;
      numArray4[44] = (byte) 247;
      numArray4[23] = (byte) 114;
      numArray4[36] = (byte) 206;
      numArray4[29] = (byte) 3;
      numArray4[46] = (byte) 41;
      numArray4[24] = (byte) 138;
      numArray4[22] = (byte) 224 /*0xE0*/;
      numArray4[39] = (byte) 141;
      numArray4[19] = (byte) 21;
      numArray4[21] = (byte) 201;
      numArray4[50] = (byte) 46;
      numArray4[20] = (byte) 69;
      numArray4[3] = (byte) 190;
      byte[] numArray5 = new byte[55]
      {
        (byte) 242,
        (byte) 27,
        (byte) 50,
        (byte) 134,
        (byte) 41,
        (byte) 112 /*0x70*/,
        (byte) 91,
        (byte) 161,
        (byte) 30,
        (byte) 47,
        (byte) 42,
        (byte) 144 /*0x90*/,
        (byte) 244,
        (byte) 215,
        (byte) 55,
        (byte) 202,
        (byte) 182,
        (byte) 158,
        (byte) 7,
        (byte) 32 /*0x20*/,
        (byte) 142,
        (byte) 224 /*0xE0*/,
        (byte) 224 /*0xE0*/,
        (byte) 6,
        (byte) 192 /*0xC0*/,
        (byte) 69,
        (byte) 215,
        (byte) 119,
        (byte) 108,
        (byte) 79,
        (byte) 174,
        (byte) 80 /*0x50*/,
        (byte) 136,
        (byte) 170,
        (byte) 251,
        (byte) 80 /*0x50*/,
        (byte) 42,
        (byte) 164,
        (byte) 182,
        (byte) 166,
        (byte) 94,
        (byte) 212,
        (byte) 95,
        (byte) 177,
        (byte) 192 /*0xC0*/,
        (byte) 165,
        (byte) 248,
        (byte) 45,
        (byte) 17,
        (byte) 11,
        (byte) 169,
        (byte) 105,
        (byte) 231,
        (byte) 155,
        (byte) 176 /*0xB0*/
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[8];
      numArray6[3] = (byte) 235;
      numArray6[2] = (byte) 108;
      numArray6[1] = (byte) 254;
      numArray6[7] = (byte) 214;
      numArray6[4] = byte.MaxValue;
      numArray6[5] = (byte) 189;
      numArray6[6] = (byte) 55;
      numArray6[0] = (byte) 211;
      byte[] numArray7 = new byte[8];
      numArray7[3] = (byte) 74;
      numArray7[0] = (byte) 145;
      numArray7[2] = (byte) 239;
      numArray7[6] = (byte) 23;
      numArray7[1] = (byte) 56;
      numArray7[5] = (byte) 89;
      numArray7[7] = (byte) 166;
      numArray7[4] = (byte) 44;
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 8);
      for (int index = 0; index < 8; ++index)
        numArray1[index + 110] ^= numArray7[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray8 = new byte[118];
    byte[] numArray9 = new byte[55]
    {
      (byte) 123,
      (byte) 134,
      (byte) 76,
      (byte) 249,
      (byte) 191,
      (byte) 167,
      (byte) 105,
      (byte) 171,
      (byte) 29,
      (byte) 0,
      (byte) 205,
      (byte) 53,
      (byte) 49,
      (byte) 217,
      (byte) 139,
      (byte) 102,
      (byte) 232,
      (byte) 177,
      (byte) 176 /*0xB0*/,
      (byte) 21,
      (byte) 248,
      (byte) 161,
      (byte) 214,
      (byte) 179,
      (byte) 30,
      (byte) 224 /*0xE0*/,
      (byte) 2,
      (byte) 95,
      (byte) 149,
      (byte) 183,
      (byte) 79,
      (byte) 129,
      (byte) 227,
      (byte) 234,
      (byte) 188,
      (byte) 80 /*0x50*/,
      (byte) 138,
      (byte) 11,
      (byte) 110,
      (byte) 37,
      (byte) 198,
      (byte) 181,
      (byte) 154,
      (byte) 106,
      (byte) 21,
      (byte) 21,
      (byte) 73,
      (byte) 192 /*0xC0*/,
      (byte) 243,
      (byte) 215,
      (byte) 95,
      (byte) 107,
      (byte) 185,
      (byte) 5,
      (byte) 218
    };
    byte[] numArray10 = new byte[55]
    {
      (byte) 161,
      (byte) 115,
      (byte) 9,
      (byte) 185,
      (byte) 203,
      (byte) 168,
      (byte) 243,
      (byte) 106,
      (byte) 139,
      (byte) 134,
      (byte) 223,
      (byte) 192 /*0xC0*/,
      (byte) 123,
      (byte) 204,
      (byte) 53,
      (byte) 89,
      (byte) 217,
      (byte) 111,
      (byte) 12,
      (byte) 66,
      (byte) 209,
      (byte) 182,
      (byte) 171,
      (byte) 112 /*0x70*/,
      (byte) 33,
      (byte) 46,
      (byte) 64 /*0x40*/,
      (byte) 112 /*0x70*/,
      (byte) 150,
      (byte) 230,
      (byte) 67,
      (byte) 29,
      (byte) 17,
      (byte) 211,
      (byte) 250,
      (byte) 25,
      (byte) 89,
      (byte) 164,
      (byte) 178,
      (byte) 232,
      (byte) 16 /*0x10*/,
      (byte) 156,
      (byte) 124,
      (byte) 150,
      (byte) 230,
      (byte) 244,
      (byte) 53,
      (byte) 3,
      (byte) 92,
      (byte) 127 /*0x7F*/,
      (byte) 48 /*0x30*/,
      (byte) 91,
      (byte) 58,
      (byte) 193,
      (byte) 113
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray8, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index] ^= numArray10[index];
    byte[] numArray11 = new byte[55]
    {
      (byte) 152,
      (byte) 219,
      (byte) 223,
      (byte) 165,
      (byte) 10,
      (byte) 21,
      (byte) 102,
      (byte) 177,
      (byte) 76,
      (byte) 82,
      (byte) 70,
      (byte) 29,
      (byte) 59,
      (byte) 220,
      (byte) 220,
      (byte) 67,
      (byte) 138,
      (byte) 79,
      (byte) 67,
      (byte) 6,
      (byte) 107,
      (byte) 158,
      (byte) 9,
      (byte) 150,
      (byte) 66,
      (byte) 108,
      (byte) 190,
      (byte) 88,
      (byte) 227,
      (byte) 200,
      (byte) 221,
      (byte) 16 /*0x10*/,
      (byte) 226,
      (byte) 160 /*0xA0*/,
      (byte) 241,
      (byte) 47,
      (byte) 142,
      (byte) 128 /*0x80*/,
      (byte) 187,
      (byte) 159,
      (byte) 52,
      (byte) 185,
      (byte) 201,
      (byte) 195,
      (byte) 2,
      (byte) 163,
      (byte) 183,
      (byte) 250,
      (byte) 130,
      (byte) 40,
      (byte) 46,
      (byte) 182,
      (byte) 151,
      (byte) 211,
      (byte) 68
    };
    byte[] numArray12 = new byte[55];
    numArray12[14] = (byte) 203;
    numArray12[1] = (byte) 76;
    numArray12[2] = (byte) 142;
    numArray12[13] = (byte) 176 /*0xB0*/;
    numArray12[4] = byte.MaxValue;
    numArray12[7] = (byte) 39;
    numArray12[15] = (byte) 91;
    numArray12[38] = (byte) 117;
    numArray12[8] = (byte) 78;
    numArray12[16 /*0x10*/] = (byte) 175;
    numArray12[10] = (byte) 162;
    numArray12[11] = (byte) 120;
    numArray12[30] = (byte) 43;
    numArray12[20] = (byte) 166;
    numArray12[6] = (byte) 130;
    numArray12[32 /*0x20*/] = (byte) 1;
    numArray12[12] = (byte) 133;
    numArray12[9] = (byte) 153;
    numArray12[18] = (byte) 191;
    numArray12[19] = (byte) 45;
    numArray12[3] = (byte) 70;
    numArray12[21] = (byte) 13;
    numArray12[22] = (byte) 33;
    numArray12[26] = (byte) 136;
    numArray12[24] = (byte) 23;
    numArray12[23] = (byte) 203;
    numArray12[42] = (byte) 172;
    numArray12[27] = (byte) 114;
    numArray12[28] = (byte) 125;
    numArray12[29] = (byte) 66;
    numArray12[0] = (byte) 213;
    numArray12[43] = (byte) 160 /*0xA0*/;
    numArray12[52] = (byte) 207;
    numArray12[51] = (byte) 16 /*0x10*/;
    numArray12[50] = (byte) 237;
    numArray12[35] = (byte) 248;
    numArray12[40] = (byte) 43;
    numArray12[37] = (byte) 4;
    numArray12[46] = (byte) 155;
    numArray12[34] = (byte) 61;
    numArray12[33] = (byte) 107;
    numArray12[36] = (byte) 214;
    numArray12[31 /*0x1F*/] = (byte) 198;
    numArray12[25] = (byte) 244;
    numArray12[44] = (byte) 43;
    numArray12[41] = (byte) 168;
    numArray12[45] = (byte) 31 /*0x1F*/;
    numArray12[47] = (byte) 151;
    numArray12[5] = (byte) 138;
    numArray12[49] = (byte) 79;
    numArray12[17] = (byte) 194;
    numArray12[48 /*0x30*/] = (byte) 237;
    numArray12[39] = (byte) 219;
    numArray12[53] = (byte) 105;
    numArray12[54] = (byte) 67;
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray8, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index + 55] ^= numArray12[index];
    byte[] numArray13 = new byte[8];
    numArray13[2] = (byte) 232;
    numArray13[4] = (byte) 56;
    numArray13[3] = (byte) 147;
    numArray13[1] = (byte) 195;
    numArray13[7] = (byte) 76;
    numArray13[5] = (byte) 18;
    numArray13[6] = (byte) 234;
    numArray13[0] = (byte) 179;
    byte[] numArray14 = new byte[8]
    {
      (byte) 10,
      (byte) 24,
      (byte) 21,
      (byte) 75,
      (byte) 97,
      (byte) 192 /*0xC0*/,
      (byte) 181,
      (byte) 142
    };
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray8, 110, 8);
    for (int index = 0; index < 8; ++index)
      numArray8[index + 110] ^= numArray14[index];
    return Encoding.UTF8.GetString(numArray8);
  }

  internal static string ssp_appserver_12797()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[120];
      byte[] numArray2 = new byte[55]
      {
        (byte) 247,
        (byte) 200,
        (byte) 54,
        (byte) 151,
        (byte) 179,
        (byte) 1,
        (byte) 71,
        (byte) 117,
        (byte) 5,
        (byte) 133,
        (byte) 13,
        (byte) 202,
        (byte) 209,
        (byte) 208 /*0xD0*/,
        (byte) 146,
        (byte) 90,
        (byte) 54,
        (byte) 180,
        (byte) 238,
        (byte) 217,
        (byte) 90,
        (byte) 2,
        (byte) 13,
        (byte) 60,
        (byte) 222,
        (byte) 28,
        (byte) 132,
        (byte) 5,
        (byte) 55,
        (byte) 172,
        (byte) 151,
        (byte) 86,
        (byte) 202,
        (byte) 150,
        (byte) 149,
        (byte) 235,
        (byte) 140,
        (byte) 80 /*0x50*/,
        (byte) 223,
        (byte) 54,
        (byte) 202,
        (byte) 117,
        (byte) 71,
        (byte) 56,
        (byte) 30,
        (byte) 116,
        (byte) 34,
        (byte) 128 /*0x80*/,
        (byte) 219,
        (byte) 145,
        (byte) 63 /*0x3F*/,
        (byte) 167,
        (byte) 188,
        (byte) 238,
        (byte) 52
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 220,
        (byte) 179,
        (byte) 44,
        (byte) 157,
        (byte) 91,
        (byte) 11,
        (byte) 54,
        (byte) 248,
        (byte) 12,
        (byte) 48 /*0x30*/,
        (byte) 201,
        (byte) 85,
        (byte) 21,
        (byte) 99,
        (byte) 110,
        (byte) 130,
        (byte) 204,
        (byte) 135,
        (byte) 51,
        (byte) 199,
        (byte) 60,
        (byte) 135,
        (byte) 50,
        (byte) 152,
        (byte) 120,
        (byte) 234,
        (byte) 87,
        (byte) 180,
        (byte) 155,
        (byte) 96 /*0x60*/,
        (byte) 79,
        (byte) 204,
        (byte) 232,
        (byte) 151,
        (byte) 23,
        (byte) 177,
        (byte) 158,
        (byte) 60,
        (byte) 107,
        (byte) 220,
        (byte) 86,
        (byte) 187,
        (byte) 184,
        (byte) 5,
        (byte) 252,
        (byte) 232,
        (byte) 32 /*0x20*/,
        (byte) 214,
        (byte) 85,
        (byte) 204,
        (byte) 11,
        (byte) 199,
        (byte) 72,
        (byte) 123,
        (byte) 28
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55];
      numArray4[26] = (byte) 166;
      numArray4[1] = (byte) 185;
      numArray4[21] = (byte) 29;
      numArray4[3] = (byte) 124;
      numArray4[4] = (byte) 65;
      numArray4[48 /*0x30*/] = (byte) 90;
      numArray4[34] = (byte) 146;
      numArray4[7] = (byte) 238;
      numArray4[40] = (byte) 238;
      numArray4[35] = (byte) 151;
      numArray4[27] = (byte) 109;
      numArray4[31 /*0x1F*/] = (byte) 97;
      numArray4[11] = (byte) 58;
      numArray4[13] = (byte) 206;
      numArray4[49] = (byte) 211;
      numArray4[15] = (byte) 212;
      numArray4[6] = (byte) 88;
      numArray4[17] = (byte) 14;
      numArray4[18] = (byte) 229;
      numArray4[19] = (byte) 186;
      numArray4[20] = (byte) 213;
      numArray4[5] = (byte) 61;
      numArray4[52] = (byte) 78;
      numArray4[42] = (byte) 104;
      numArray4[24] = (byte) 3;
      numArray4[25] = (byte) 41;
      numArray4[9] = byte.MaxValue;
      numArray4[28] = (byte) 239;
      numArray4[43] = (byte) 117;
      numArray4[44] = (byte) 230;
      numArray4[39] = (byte) 56;
      numArray4[29] = (byte) 153;
      numArray4[32 /*0x20*/] = (byte) 160 /*0xA0*/;
      numArray4[30] = (byte) 220;
      numArray4[10] = (byte) 59;
      numArray4[23] = (byte) 250;
      numArray4[36] = (byte) 73;
      numArray4[37] = (byte) 185;
      numArray4[38] = (byte) 136;
      numArray4[33] = (byte) 47;
      numArray4[2] = (byte) 150;
      numArray4[41] = (byte) 74;
      numArray4[22] = (byte) 105;
      numArray4[45] = (byte) 69;
      numArray4[53] = (byte) 58;
      numArray4[14] = (byte) 229;
      numArray4[46] = (byte) 52;
      numArray4[47] = (byte) 195;
      numArray4[12] = (byte) 167;
      numArray4[16 /*0x10*/] = (byte) 183;
      numArray4[50] = (byte) 5;
      numArray4[51] = (byte) 223;
      numArray4[8] = (byte) 182;
      numArray4[0] = (byte) 250;
      numArray4[54] = (byte) 230;
      byte[] numArray5 = new byte[55];
      numArray5[50] = (byte) 81;
      numArray5[1] = (byte) 43;
      numArray5[37] = (byte) 146;
      numArray5[3] = (byte) 76;
      numArray5[4] = (byte) 73;
      numArray5[49] = (byte) 77;
      numArray5[6] = (byte) 248;
      numArray5[46] = (byte) 213;
      numArray5[8] = (byte) 242;
      numArray5[27] = (byte) 68;
      numArray5[10] = (byte) 159;
      numArray5[2] = (byte) 244;
      numArray5[12] = (byte) 28;
      numArray5[9] = (byte) 62;
      numArray5[47] = (byte) 114;
      numArray5[15] = (byte) 197;
      numArray5[21] = (byte) 169;
      numArray5[7] = (byte) 202;
      numArray5[19] = (byte) 119;
      numArray5[26] = (byte) 160 /*0xA0*/;
      numArray5[30] = (byte) 76;
      numArray5[14] = (byte) 89;
      numArray5[44] = (byte) 192 /*0xC0*/;
      numArray5[23] = (byte) 241;
      numArray5[24] = (byte) 104;
      numArray5[25] = (byte) 220;
      numArray5[13] = (byte) 50;
      numArray5[22] = (byte) 80 /*0x50*/;
      numArray5[5] = (byte) 155;
      numArray5[52] = (byte) 199;
      numArray5[38] = (byte) 158;
      numArray5[31 /*0x1F*/] = (byte) 247;
      numArray5[48 /*0x30*/] = (byte) 175;
      numArray5[33] = (byte) 207;
      numArray5[34] = (byte) 251;
      numArray5[35] = (byte) 172;
      numArray5[36] = (byte) 32 /*0x20*/;
      numArray5[29] = (byte) 183;
      numArray5[41] = (byte) 208 /*0xD0*/;
      numArray5[45] = (byte) 141;
      numArray5[54] = (byte) 231;
      numArray5[40] = (byte) 124;
      numArray5[16 /*0x10*/] = (byte) 16 /*0x10*/;
      numArray5[43] = (byte) 206;
      numArray5[39] = (byte) 30;
      numArray5[18] = (byte) 242;
      numArray5[11] = (byte) 5;
      numArray5[32 /*0x20*/] = (byte) 89;
      numArray5[17] = (byte) 192 /*0xC0*/;
      numArray5[0] = (byte) 252;
      numArray5[28] = (byte) 192 /*0xC0*/;
      numArray5[51] = (byte) 62;
      numArray5[20] = (byte) 111;
      numArray5[53] = (byte) 12;
      numArray5[42] = (byte) 33;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[10];
      numArray6[9] = (byte) 60;
      numArray6[1] = (byte) 168;
      numArray6[8] = (byte) 85;
      numArray6[3] = (byte) 89;
      numArray6[4] = (byte) 208 /*0xD0*/;
      numArray6[5] = (byte) 16 /*0x10*/;
      numArray6[6] = (byte) 250;
      numArray6[2] = (byte) 234;
      numArray6[0] = (byte) 129;
      numArray6[7] = (byte) 133;
      byte[] numArray7 = new byte[10]
      {
        (byte) 132,
        (byte) 99,
        (byte) 50,
        (byte) 83,
        (byte) 20,
        (byte) 183,
        (byte) 227,
        (byte) 32 /*0x20*/,
        (byte) 44,
        (byte) 20
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index + 110] ^= numArray7[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray8 = new byte[120];
    byte[] numArray9 = new byte[55]
    {
      (byte) 168,
      (byte) 90,
      (byte) 0,
      (byte) 105,
      (byte) 67,
      (byte) 91,
      (byte) 220,
      (byte) 82,
      (byte) 212,
      (byte) 208 /*0xD0*/,
      (byte) 47,
      (byte) 162,
      (byte) 221,
      (byte) 99,
      (byte) 145,
      (byte) 92,
      (byte) 86,
      (byte) 200,
      (byte) 165,
      (byte) 240 /*0xF0*/,
      (byte) 59,
      (byte) 80 /*0x50*/,
      (byte) 8,
      (byte) 233,
      (byte) 175,
      (byte) 19,
      (byte) 127 /*0x7F*/,
      (byte) 116,
      (byte) 9,
      (byte) 65,
      (byte) 23,
      (byte) 67,
      (byte) 193,
      (byte) 13,
      (byte) 65,
      byte.MaxValue,
      (byte) 207,
      (byte) 166,
      (byte) 183,
      (byte) 41,
      (byte) 120,
      (byte) 234,
      (byte) 106,
      (byte) 40,
      (byte) 236,
      (byte) 23,
      (byte) 89,
      (byte) 98,
      (byte) 142,
      (byte) 63 /*0x3F*/,
      (byte) 130,
      (byte) 221,
      (byte) 209,
      (byte) 207,
      (byte) 95
    };
    byte[] numArray10 = new byte[55];
    numArray10[9] = (byte) 155;
    numArray10[6] = (byte) 144 /*0x90*/;
    numArray10[18] = (byte) 120;
    numArray10[3] = (byte) 180;
    numArray10[4] = (byte) 142;
    numArray10[11] = (byte) 77;
    numArray10[32 /*0x20*/] = (byte) 74;
    numArray10[12] = (byte) 117;
    numArray10[53] = (byte) 100;
    numArray10[35] = (byte) 163;
    numArray10[0] = (byte) 148;
    numArray10[50] = (byte) 176 /*0xB0*/;
    numArray10[1] = (byte) 38;
    numArray10[26] = (byte) 107;
    numArray10[13] = (byte) 188;
    numArray10[27] = (byte) 90;
    numArray10[16 /*0x10*/] = (byte) 59;
    numArray10[17] = (byte) 15;
    numArray10[14] = (byte) 128 /*0x80*/;
    numArray10[15] = (byte) 240 /*0xF0*/;
    numArray10[20] = (byte) 95;
    numArray10[21] = (byte) 129;
    numArray10[22] = (byte) 11;
    numArray10[23] = (byte) 161;
    numArray10[24] = (byte) 245;
    numArray10[25] = (byte) 46;
    numArray10[28] = (byte) 147;
    numArray10[36] = (byte) 142;
    numArray10[8] = (byte) 125;
    numArray10[5] = (byte) 126;
    numArray10[30] = (byte) 66;
    numArray10[31 /*0x1F*/] = (byte) 82;
    numArray10[7] = (byte) 92;
    numArray10[33] = (byte) 34;
    numArray10[19] = (byte) 108;
    numArray10[48 /*0x30*/] = (byte) 253;
    numArray10[37] = (byte) 47;
    numArray10[34] = (byte) 21;
    numArray10[38] = (byte) 132;
    numArray10[39] = (byte) 94;
    numArray10[40] = (byte) 151;
    numArray10[29] = (byte) 149;
    numArray10[42] = (byte) 229;
    numArray10[43] = (byte) 187;
    numArray10[44] = (byte) 241;
    numArray10[45] = (byte) 236;
    numArray10[46] = (byte) 107;
    numArray10[52] = (byte) 126;
    numArray10[47] = (byte) 122;
    numArray10[49] = (byte) 146;
    numArray10[10] = (byte) 59;
    numArray10[51] = (byte) 169;
    numArray10[41] = (byte) 114;
    numArray10[2] = (byte) 93;
    numArray10[54] = (byte) 203;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray8, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index] ^= numArray10[index];
    byte[] numArray11 = new byte[55]
    {
      (byte) 26,
      (byte) 34,
      (byte) 117,
      (byte) 190,
      (byte) 131,
      (byte) 125,
      (byte) 211,
      (byte) 126,
      (byte) 121,
      (byte) 239,
      (byte) 34,
      (byte) 57,
      (byte) 228,
      (byte) 212,
      (byte) 195,
      (byte) 138,
      (byte) 46,
      (byte) 103,
      (byte) 175,
      (byte) 13,
      (byte) 112 /*0x70*/,
      (byte) 251,
      (byte) 97,
      (byte) 49,
      (byte) 221,
      (byte) 232,
      (byte) 23,
      (byte) 236,
      (byte) 110,
      (byte) 28,
      (byte) 154,
      (byte) 158,
      (byte) 118,
      (byte) 59,
      (byte) 157,
      (byte) 202,
      (byte) 17,
      (byte) 142,
      (byte) 140,
      (byte) 35,
      (byte) 4,
      (byte) 240 /*0xF0*/,
      (byte) 136,
      (byte) 70,
      (byte) 102,
      (byte) 6,
      (byte) 24,
      (byte) 221,
      (byte) 14,
      (byte) 65,
      (byte) 38,
      (byte) 239,
      (byte) 95,
      (byte) 101,
      (byte) 1
    };
    byte[] numArray12 = new byte[55]
    {
      (byte) 97,
      (byte) 163,
      (byte) 54,
      (byte) 156,
      (byte) 105,
      (byte) 202,
      (byte) 124,
      (byte) 237,
      (byte) 24,
      (byte) 147,
      (byte) 96 /*0x60*/,
      (byte) 129,
      (byte) 195,
      (byte) 236,
      (byte) 162,
      (byte) 100,
      (byte) 162,
      (byte) 164,
      (byte) 235,
      (byte) 147,
      (byte) 223,
      (byte) 202,
      (byte) 159,
      (byte) 216,
      (byte) 29,
      (byte) 162,
      (byte) 119,
      (byte) 2,
      (byte) 22,
      (byte) 29,
      (byte) 122,
      (byte) 167,
      (byte) 167,
      (byte) 104,
      (byte) 228,
      (byte) 46,
      (byte) 62,
      (byte) 143,
      (byte) 81,
      (byte) 108,
      (byte) 230,
      (byte) 202,
      (byte) 243,
      (byte) 218,
      (byte) 122,
      (byte) 227,
      (byte) 171,
      (byte) 120,
      (byte) 95,
      (byte) 41,
      (byte) 112 /*0x70*/,
      (byte) 11,
      (byte) 225,
      (byte) 100,
      (byte) 223
    };
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray8, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index + 55] ^= numArray12[index];
    byte[] numArray13 = new byte[10]
    {
      (byte) 68,
      (byte) 225,
      (byte) 154,
      (byte) 102,
      (byte) 230,
      (byte) 16 /*0x10*/,
      (byte) 92,
      (byte) 85,
      (byte) 131,
      (byte) 194
    };
    byte[] numArray14 = new byte[10];
    numArray14[7] = (byte) 70;
    numArray14[1] = (byte) 177;
    numArray14[4] = (byte) 119;
    numArray14[3] = (byte) 85;
    numArray14[6] = (byte) 195;
    numArray14[0] = (byte) 140;
    numArray14[2] = (byte) 177;
    numArray14[8] = (byte) 4;
    numArray14[5] = (byte) 134;
    numArray14[9] = (byte) 153;
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray8, 110, 10);
    for (int index = 0; index < 10; ++index)
      numArray8[index + 110] ^= numArray14[index];
    byte[] numArray15 = new byte[53];
    byte[] response = new byte[53];
    Array.Copy((Array) sc_12780.sspq, 226, (Array) numArray15, 0, 53);
    key.Query(true, 335, numArray15, response);
    Array.Copy((Array) sc_12780.sspr, 226, (Array) numArray15, 0, 53);
    for (int index = 0; index < numArray15.Length; ++index)
    {
      if ((int) numArray15[index] != (int) response[index])
      {
        key.TagValue = (int) response[index];
        break;
      }
    }
    return Encoding.UTF8.GetString(numArray8);
  }

  internal static string ssp_appserver_12798()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[113];
      byte[] numArray2 = new byte[55]
      {
        (byte) 95,
        (byte) 146,
        (byte) 201,
        (byte) 99,
        (byte) 111,
        (byte) 149,
        (byte) 107,
        (byte) 179,
        (byte) 183,
        (byte) 212,
        (byte) 100,
        (byte) 83,
        (byte) 163,
        (byte) 39,
        (byte) 145,
        (byte) 22,
        (byte) 253,
        (byte) 197,
        (byte) 69,
        (byte) 158,
        (byte) 145,
        (byte) 2,
        (byte) 239,
        (byte) 37,
        (byte) 30,
        (byte) 147,
        (byte) 43,
        (byte) 6,
        (byte) 114,
        (byte) 227,
        (byte) 99,
        (byte) 123,
        (byte) 145,
        (byte) 119,
        (byte) 107,
        (byte) 153,
        (byte) 62,
        (byte) 127 /*0x7F*/,
        (byte) 247,
        (byte) 21,
        (byte) 217,
        (byte) 254,
        (byte) 182,
        (byte) 226,
        (byte) 139,
        (byte) 86,
        (byte) 79,
        (byte) 194,
        (byte) 60,
        (byte) 212,
        (byte) 160 /*0xA0*/,
        (byte) 130,
        (byte) 227,
        (byte) 73,
        (byte) 1
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 142,
        (byte) 31 /*0x1F*/,
        (byte) 151,
        (byte) 25,
        (byte) 95,
        (byte) 2,
        (byte) 226,
        (byte) 7,
        (byte) 24,
        (byte) 115,
        (byte) 54,
        (byte) 96 /*0x60*/,
        (byte) 155,
        (byte) 66,
        (byte) 209,
        (byte) 176 /*0xB0*/,
        (byte) 130,
        (byte) 133,
        (byte) 55,
        (byte) 107,
        (byte) 165,
        (byte) 43,
        (byte) 94,
        (byte) 10,
        (byte) 3,
        (byte) 175,
        (byte) 100,
        (byte) 86,
        (byte) 13,
        (byte) 8,
        (byte) 83,
        (byte) 90,
        (byte) 236,
        (byte) 214,
        (byte) 139,
        (byte) 169,
        (byte) 11,
        (byte) 110,
        (byte) 111,
        (byte) 240 /*0xF0*/,
        (byte) 116,
        (byte) 152,
        (byte) 97,
        (byte) 210,
        (byte) 151,
        (byte) 81,
        (byte) 230,
        (byte) 193,
        (byte) 248,
        (byte) 8,
        (byte) 239,
        (byte) 223,
        (byte) 192 /*0xC0*/,
        (byte) 229,
        (byte) 1
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 164,
        (byte) 68,
        (byte) 219,
        (byte) 106,
        (byte) 100,
        (byte) 65,
        (byte) 211,
        (byte) 113,
        (byte) 232,
        (byte) 194,
        (byte) 44,
        (byte) 187,
        (byte) 247,
        (byte) 215,
        (byte) 126,
        (byte) 205,
        (byte) 48 /*0x30*/,
        (byte) 197,
        (byte) 147,
        (byte) 75,
        (byte) 12,
        (byte) 153,
        (byte) 200,
        (byte) 68,
        (byte) 105,
        (byte) 202,
        (byte) 157,
        (byte) 67,
        (byte) 224 /*0xE0*/,
        (byte) 152,
        (byte) 71,
        (byte) 117,
        (byte) 14,
        (byte) 246,
        (byte) 154,
        (byte) 58,
        (byte) 28,
        (byte) 171,
        (byte) 116,
        (byte) 188,
        (byte) 227,
        (byte) 113,
        (byte) 163,
        (byte) 148,
        (byte) 35,
        (byte) 6,
        (byte) 68,
        (byte) 252,
        (byte) 34,
        (byte) 180,
        (byte) 179,
        (byte) 132,
        (byte) 109,
        (byte) 17,
        (byte) 150
      };
      byte[] numArray5 = new byte[55]
      {
        (byte) 59,
        (byte) 200,
        (byte) 199,
        (byte) 148,
        (byte) 106,
        (byte) 83,
        (byte) 159,
        (byte) 94,
        (byte) 97,
        (byte) 106,
        (byte) 34,
        (byte) 141,
        (byte) 242,
        (byte) 246,
        (byte) 216,
        (byte) 172,
        (byte) 248,
        (byte) 83,
        (byte) 74,
        (byte) 254,
        (byte) 63 /*0x3F*/,
        (byte) 53,
        (byte) 147,
        (byte) 192 /*0xC0*/,
        (byte) 174,
        (byte) 107,
        (byte) 120,
        (byte) 165,
        (byte) 92,
        (byte) 196,
        (byte) 208 /*0xD0*/,
        (byte) 114,
        (byte) 116,
        (byte) 84,
        (byte) 235,
        (byte) 242,
        (byte) 144 /*0x90*/,
        (byte) 131,
        (byte) 12,
        (byte) 150,
        (byte) 253,
        (byte) 100,
        (byte) 173,
        (byte) 104,
        (byte) 130,
        (byte) 15,
        (byte) 240 /*0xF0*/,
        (byte) 82,
        (byte) 47,
        (byte) 27,
        (byte) 237,
        (byte) 214,
        byte.MaxValue,
        (byte) 203,
        (byte) 123
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[3]
      {
        (byte) 150,
        (byte) 1,
        (byte) 153
      };
      byte[] numArray7 = new byte[3]
      {
        (byte) 80 /*0x50*/,
        (byte) 26,
        (byte) 224 /*0xE0*/
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 3);
      for (int index = 0; index < 3; ++index)
        numArray1[index + 110] ^= numArray7[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray8 = new byte[113];
    byte[] numArray9 = new byte[55]
    {
      (byte) 80 /*0x50*/,
      (byte) 122,
      (byte) 182,
      (byte) 173,
      (byte) 1,
      (byte) 61,
      (byte) 67,
      (byte) 177,
      (byte) 133,
      (byte) 80 /*0x50*/,
      (byte) 214,
      (byte) 104,
      (byte) 155,
      (byte) 246,
      (byte) 51,
      (byte) 33,
      (byte) 99,
      (byte) 133,
      (byte) 161,
      (byte) 33,
      (byte) 43,
      (byte) 228,
      (byte) 194,
      (byte) 8,
      (byte) 252,
      (byte) 95,
      (byte) 143,
      (byte) 190,
      (byte) 133,
      (byte) 170,
      (byte) 218,
      (byte) 25,
      (byte) 83,
      (byte) 223,
      (byte) 83,
      (byte) 25,
      (byte) 108,
      (byte) 88,
      (byte) 67,
      (byte) 191,
      (byte) 222,
      (byte) 3,
      (byte) 46,
      (byte) 52,
      (byte) 150,
      (byte) 169,
      (byte) 251,
      (byte) 19,
      (byte) 72,
      (byte) 117,
      (byte) 157,
      (byte) 20,
      (byte) 2,
      (byte) 111,
      (byte) 101
    };
    byte[] numArray10 = new byte[55];
    numArray10[33] = (byte) 200;
    numArray10[35] = (byte) 2;
    numArray10[26] = (byte) 145;
    numArray10[52] = (byte) 194;
    numArray10[4] = (byte) 113;
    numArray10[24] = (byte) 199;
    numArray10[42] = (byte) 110;
    numArray10[7] = (byte) 148;
    numArray10[44] = (byte) 57;
    numArray10[9] = (byte) 166;
    numArray10[10] = (byte) 236;
    numArray10[39] = (byte) 239;
    numArray10[17] = (byte) 180;
    numArray10[13] = (byte) 206;
    numArray10[23] = (byte) 219;
    numArray10[11] = (byte) 153;
    numArray10[19] = (byte) 220;
    numArray10[15] = (byte) 195;
    numArray10[18] = (byte) 50;
    numArray10[12] = (byte) 75;
    numArray10[20] = (byte) 51;
    numArray10[21] = (byte) 242;
    numArray10[25] = (byte) 149;
    numArray10[1] = (byte) 138;
    numArray10[6] = (byte) 92;
    numArray10[5] = (byte) 55;
    numArray10[16 /*0x10*/] = (byte) 152;
    numArray10[46] = (byte) 56;
    numArray10[28] = (byte) 236;
    numArray10[29] = (byte) 164;
    numArray10[30] = (byte) 212;
    numArray10[8] = (byte) 124;
    numArray10[47] = (byte) 6;
    numArray10[49] = (byte) 231;
    numArray10[34] = (byte) 203;
    numArray10[31 /*0x1F*/] = (byte) 230;
    numArray10[54] = (byte) 197;
    numArray10[45] = (byte) 221;
    numArray10[38] = (byte) 223;
    numArray10[0] = (byte) 120;
    numArray10[40] = (byte) 116;
    numArray10[41] = (byte) 9;
    numArray10[14] = (byte) 152;
    numArray10[43] = (byte) 24;
    numArray10[3] = (byte) 99;
    numArray10[32 /*0x20*/] = (byte) 236;
    numArray10[2] = (byte) 152;
    numArray10[22] = (byte) 123;
    numArray10[48 /*0x30*/] = (byte) 124;
    numArray10[27] = (byte) 40;
    numArray10[50] = (byte) 86;
    numArray10[51] = (byte) 219;
    numArray10[37] = (byte) 47;
    numArray10[53] = (byte) 61;
    numArray10[36] = (byte) 174;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray8, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index] ^= numArray10[index];
    byte[] numArray11 = new byte[55];
    numArray11[20] = (byte) 207;
    numArray11[28] = (byte) 245;
    numArray11[47] = (byte) 204;
    numArray11[50] = (byte) 80 /*0x50*/;
    numArray11[25] = (byte) 72;
    numArray11[5] = (byte) 28;
    numArray11[38] = (byte) 74;
    numArray11[18] = (byte) 81;
    numArray11[4] = (byte) 15;
    numArray11[42] = (byte) 50;
    numArray11[10] = (byte) 65;
    numArray11[22] = (byte) 189;
    numArray11[12] = (byte) 184;
    numArray11[13] = (byte) 245;
    numArray11[9] = (byte) 49;
    numArray11[36] = (byte) 98;
    numArray11[16 /*0x10*/] = (byte) 208 /*0xD0*/;
    numArray11[1] = (byte) 95;
    numArray11[0] = (byte) 60;
    numArray11[19] = (byte) 99;
    numArray11[17] = (byte) 233;
    numArray11[7] = (byte) 174;
    numArray11[21] = (byte) 152;
    numArray11[23] = (byte) 36;
    numArray11[35] = (byte) 71;
    numArray11[31 /*0x1F*/] = (byte) 61;
    numArray11[14] = (byte) 100;
    numArray11[27] = (byte) 120;
    numArray11[53] = (byte) 80 /*0x50*/;
    numArray11[26] = (byte) 78;
    numArray11[30] = (byte) 183;
    numArray11[24] = (byte) 12;
    numArray11[32 /*0x20*/] = (byte) 51;
    numArray11[8] = (byte) 166;
    numArray11[34] = (byte) 130;
    numArray11[37] = (byte) 224 /*0xE0*/;
    numArray11[40] = (byte) 131;
    numArray11[52] = (byte) 235;
    numArray11[43] = (byte) 99;
    numArray11[2] = (byte) 61;
    numArray11[29] = (byte) 246;
    numArray11[41] = (byte) 16 /*0x10*/;
    numArray11[45] = (byte) 180;
    numArray11[15] = (byte) 161;
    numArray11[46] = (byte) 117;
    numArray11[39] = (byte) 186;
    numArray11[3] = (byte) 49;
    numArray11[11] = (byte) 187;
    numArray11[48 /*0x30*/] = (byte) 46;
    numArray11[49] = (byte) 47;
    numArray11[54] = (byte) 89;
    numArray11[51] = (byte) 67;
    numArray11[33] = (byte) 56;
    numArray11[44] = (byte) 254;
    numArray11[6] = (byte) 207;
    byte[] numArray12 = new byte[55]
    {
      (byte) 225,
      (byte) 162,
      (byte) 206,
      (byte) 234,
      (byte) 46,
      (byte) 205,
      (byte) 135,
      (byte) 126,
      (byte) 61,
      (byte) 29,
      (byte) 170,
      (byte) 36,
      (byte) 42,
      (byte) 4,
      (byte) 73,
      (byte) 0,
      (byte) 166,
      (byte) 198,
      (byte) 30,
      (byte) 184,
      (byte) 143,
      (byte) 63 /*0x3F*/,
      (byte) 179,
      (byte) 92,
      (byte) 100,
      (byte) 69,
      (byte) 244,
      (byte) 14,
      (byte) 188,
      (byte) 172,
      (byte) 119,
      (byte) 245,
      (byte) 238,
      (byte) 24,
      (byte) 155,
      (byte) 111,
      (byte) 65,
      (byte) 220,
      (byte) 51,
      (byte) 19,
      (byte) 69,
      (byte) 212,
      (byte) 38,
      (byte) 107,
      (byte) 89,
      (byte) 250,
      (byte) 131,
      (byte) 3,
      (byte) 145,
      (byte) 137,
      (byte) 235,
      (byte) 39,
      (byte) 165,
      (byte) 175,
      (byte) 23
    };
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray8, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index + 55] ^= numArray12[index];
    byte[] numArray13 = new byte[3]
    {
      (byte) 242,
      (byte) 204,
      (byte) 244
    };
    byte[] numArray14 = new byte[3]
    {
      (byte) 26,
      (byte) 90,
      (byte) 112 /*0x70*/
    };
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray8, 110, 3);
    for (int index = 0; index < 3; ++index)
      numArray8[index + 110] ^= numArray14[index];
    return Encoding.UTF8.GetString(numArray8);
  }

  internal static string ssp_appserver_12799()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[71];
      byte[] numArray2 = new byte[55]
      {
        (byte) 160 /*0xA0*/,
        (byte) 148,
        (byte) 119,
        (byte) 244,
        (byte) 51,
        (byte) 28,
        (byte) 172,
        (byte) 179,
        (byte) 103,
        (byte) 228,
        (byte) 111,
        (byte) 44,
        (byte) 44,
        (byte) 219,
        (byte) 40,
        (byte) 56,
        (byte) 212,
        (byte) 70,
        (byte) 207,
        (byte) 254,
        (byte) 115,
        (byte) 67,
        (byte) 161,
        (byte) 92,
        (byte) 87,
        (byte) 239,
        (byte) 142,
        (byte) 158,
        (byte) 33,
        (byte) 100,
        (byte) 119,
        (byte) 227,
        (byte) 72,
        (byte) 135,
        (byte) 85,
        (byte) 197,
        (byte) 153,
        (byte) 251,
        (byte) 44,
        (byte) 136,
        (byte) 167,
        (byte) 188,
        (byte) 52,
        (byte) 88,
        (byte) 216,
        (byte) 198,
        (byte) 160 /*0xA0*/,
        (byte) 25,
        (byte) 35,
        (byte) 212,
        (byte) 30,
        (byte) 196,
        (byte) 54,
        (byte) 179,
        (byte) 49
      };
      byte[] numArray3 = new byte[55];
      numArray3[49] = (byte) 114;
      numArray3[33] = (byte) 59;
      numArray3[2] = (byte) 213;
      numArray3[52] = (byte) 146;
      numArray3[28] = (byte) 106;
      numArray3[5] = (byte) 155;
      numArray3[0] = (byte) 231;
      numArray3[19] = (byte) 229;
      numArray3[22] = (byte) 142;
      numArray3[9] = byte.MaxValue;
      numArray3[46] = (byte) 197;
      numArray3[43] = (byte) 16 /*0x10*/;
      numArray3[6] = (byte) 162;
      numArray3[10] = (byte) 5;
      numArray3[14] = (byte) 142;
      numArray3[15] = (byte) 183;
      numArray3[13] = (byte) 134;
      numArray3[23] = (byte) 226;
      numArray3[51] = (byte) 28;
      numArray3[42] = (byte) 218;
      numArray3[20] = (byte) 206;
      numArray3[21] = (byte) 50;
      numArray3[41] = (byte) 95;
      numArray3[8] = (byte) 39;
      numArray3[24] = (byte) 2;
      numArray3[3] = (byte) 173;
      numArray3[37] = (byte) 106;
      numArray3[27] = (byte) 129;
      numArray3[7] = (byte) 177;
      numArray3[26] = (byte) 180;
      numArray3[30] = (byte) 27;
      numArray3[31 /*0x1F*/] = (byte) 144 /*0x90*/;
      numArray3[32 /*0x20*/] = (byte) 211;
      numArray3[35] = (byte) 140;
      numArray3[16 /*0x10*/] = (byte) 15;
      numArray3[34] = (byte) 204;
      numArray3[36] = (byte) 192 /*0xC0*/;
      numArray3[53] = (byte) 254;
      numArray3[39] = (byte) 79;
      numArray3[18] = (byte) 211;
      numArray3[25] = (byte) 71;
      numArray3[29] = (byte) 66;
      numArray3[11] = (byte) 98;
      numArray3[1] = (byte) 187;
      numArray3[44] = (byte) 70;
      numArray3[45] = (byte) 160 /*0xA0*/;
      numArray3[17] = (byte) 32 /*0x20*/;
      numArray3[47] = (byte) 194;
      numArray3[48 /*0x30*/] = (byte) 84;
      numArray3[12] = (byte) 178;
      numArray3[50] = (byte) 228;
      numArray3[40] = (byte) 174;
      numArray3[38] = (byte) 74;
      numArray3[4] = (byte) 233;
      numArray3[54] = (byte) 68;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[16 /*0x10*/]
      {
        (byte) 150,
        (byte) 11,
        (byte) 244,
        (byte) 24,
        (byte) 146,
        (byte) 69,
        (byte) 189,
        (byte) 187,
        (byte) 223,
        (byte) 77,
        (byte) 78,
        (byte) 141,
        (byte) 145,
        (byte) 31 /*0x1F*/,
        (byte) 122,
        (byte) 49
      };
      byte[] numArray5 = new byte[16 /*0x10*/]
      {
        (byte) 10,
        (byte) 39,
        (byte) 240 /*0xF0*/,
        (byte) 251,
        (byte) 75,
        (byte) 69,
        (byte) 69,
        (byte) 165,
        (byte) 205,
        (byte) 198,
        (byte) 131,
        (byte) 173,
        (byte) 210,
        (byte) 119,
        (byte) 233,
        (byte) 51
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 16 /*0x10*/);
      for (int index = 0; index < 16 /*0x10*/; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[23];
      byte[] response = new byte[23];
      Array.Copy((Array) sc_12780.sspq, 279, (Array) numArray6, 0, 23);
      key.Query(true, 335, numArray6, response);
      Array.Copy((Array) sc_12780.sspr, 279, (Array) numArray6, 0, 23);
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
    byte[] numArray7 = new byte[71];
    byte[] numArray8 = new byte[55];
    numArray8[35] = (byte) 167;
    numArray8[50] = (byte) 48 /*0x30*/;
    numArray8[54] = (byte) 76;
    numArray8[47] = (byte) 245;
    numArray8[4] = (byte) 22;
    numArray8[5] = (byte) 203;
    numArray8[6] = (byte) 120;
    numArray8[41] = (byte) 29;
    numArray8[8] = (byte) 160 /*0xA0*/;
    numArray8[10] = (byte) 111;
    numArray8[36] = (byte) 220;
    numArray8[11] = (byte) 75;
    numArray8[31 /*0x1F*/] = (byte) 106;
    numArray8[7] = (byte) 161;
    numArray8[2] = (byte) 5;
    numArray8[3] = (byte) 46;
    numArray8[16 /*0x10*/] = (byte) 215;
    numArray8[17] = (byte) 45;
    numArray8[48 /*0x30*/] = (byte) 82;
    numArray8[19] = (byte) 178;
    numArray8[20] = (byte) 32 /*0x20*/;
    numArray8[23] = (byte) 226;
    numArray8[28] = (byte) 107;
    numArray8[40] = (byte) 143;
    numArray8[24] = (byte) 105;
    numArray8[21] = (byte) 20;
    numArray8[26] = (byte) 39;
    numArray8[13] = (byte) 93;
    numArray8[53] = (byte) 124;
    numArray8[29] = (byte) 180;
    numArray8[15] = (byte) 54;
    numArray8[18] = (byte) 189;
    numArray8[32 /*0x20*/] = (byte) 46;
    numArray8[33] = (byte) 203;
    numArray8[34] = (byte) 24;
    numArray8[12] = (byte) 94;
    numArray8[30] = (byte) 166;
    numArray8[37] = (byte) 140;
    numArray8[49] = (byte) 171;
    numArray8[9] = (byte) 239;
    numArray8[38] = (byte) 1;
    numArray8[22] = (byte) 116;
    numArray8[42] = (byte) 152;
    numArray8[43] = (byte) 235;
    numArray8[44] = (byte) 59;
    numArray8[45] = (byte) 220;
    numArray8[46] = (byte) 138;
    numArray8[25] = (byte) 55;
    numArray8[52] = (byte) 181;
    numArray8[14] = (byte) 160 /*0xA0*/;
    numArray8[1] = (byte) 172;
    numArray8[51] = (byte) 183;
    numArray8[27] = (byte) 62;
    numArray8[39] = (byte) 52;
    numArray8[0] = (byte) 149;
    byte[] numArray9 = new byte[55]
    {
      (byte) 34,
      byte.MaxValue,
      (byte) 175,
      (byte) 139,
      (byte) 72,
      (byte) 141,
      (byte) 248,
      (byte) 19,
      (byte) 179,
      (byte) 63 /*0x3F*/,
      (byte) 115,
      (byte) 117,
      (byte) 154,
      (byte) 90,
      (byte) 92,
      (byte) 189,
      (byte) 150,
      (byte) 101,
      (byte) 148,
      (byte) 221,
      (byte) 146,
      (byte) 104,
      (byte) 98,
      (byte) 140,
      (byte) 147,
      (byte) 49,
      (byte) 203,
      (byte) 133,
      (byte) 201,
      (byte) 220,
      (byte) 6,
      (byte) 181,
      (byte) 32 /*0x20*/,
      (byte) 207,
      (byte) 104,
      (byte) 144 /*0x90*/,
      (byte) 245,
      (byte) 119,
      (byte) 22,
      (byte) 232,
      (byte) 31 /*0x1F*/,
      (byte) 197,
      (byte) 203,
      (byte) 201,
      (byte) 51,
      (byte) 244,
      (byte) 211,
      (byte) 200,
      (byte) 233,
      (byte) 220,
      (byte) 100,
      (byte) 144 /*0x90*/,
      (byte) 239,
      (byte) 166,
      (byte) 232
    };
    key.Query(true, 335, numArray8, numArray8);
    Array.Copy((Array) numArray8, 0, (Array) numArray7, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray7[index] ^= numArray9[index];
    byte[] numArray10 = new byte[16 /*0x10*/];
    numArray10[13] = (byte) 160 /*0xA0*/;
    numArray10[9] = (byte) 107;
    numArray10[11] = (byte) 172;
    numArray10[3] = (byte) 114;
    numArray10[4] = (byte) 78;
    numArray10[0] = (byte) 122;
    numArray10[12] = (byte) 137;
    numArray10[7] = (byte) 90;
    numArray10[6] = (byte) 103;
    numArray10[15] = (byte) 10;
    numArray10[2] = (byte) 49;
    numArray10[8] = (byte) 79;
    numArray10[5] = (byte) 117;
    numArray10[10] = (byte) 19;
    numArray10[14] = (byte) 131;
    numArray10[1] = (byte) 55;
    byte[] numArray11 = new byte[16 /*0x10*/]
    {
      (byte) 142,
      (byte) 232,
      (byte) 139,
      (byte) 193,
      (byte) 107,
      (byte) 164,
      (byte) 111,
      (byte) 231,
      (byte) 95,
      (byte) 22,
      (byte) 54,
      (byte) 22,
      (byte) 92,
      (byte) 45,
      (byte) 96 /*0x60*/,
      (byte) 54
    };
    key.Query(true, 335, numArray10, numArray10);
    Array.Copy((Array) numArray10, 0, (Array) numArray7, 55, 16 /*0x10*/);
    for (int index = 0; index < 16 /*0x10*/; ++index)
      numArray7[index + 55] ^= numArray11[index];
    return Encoding.UTF8.GetString(numArray7);
  }

  internal static string ssp_appserver_12800()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[71];
      byte[] numArray2 = new byte[55]
      {
        (byte) 241,
        (byte) 30,
        (byte) 7,
        (byte) 113,
        (byte) 69,
        (byte) 71,
        (byte) 230,
        (byte) 82,
        (byte) 228,
        (byte) 4,
        (byte) 208 /*0xD0*/,
        (byte) 177,
        (byte) 98,
        (byte) 129,
        (byte) 53,
        (byte) 140,
        (byte) 44,
        (byte) 54,
        (byte) 199,
        (byte) 9,
        (byte) 105,
        (byte) 123,
        (byte) 242,
        (byte) 188,
        (byte) 6,
        (byte) 201,
        (byte) 246,
        (byte) 200,
        (byte) 20,
        (byte) 221,
        (byte) 25,
        (byte) 201,
        (byte) 96 /*0x60*/,
        (byte) 243,
        (byte) 24,
        (byte) 217,
        (byte) 198,
        (byte) 170,
        (byte) 137,
        (byte) 159,
        (byte) 139,
        (byte) 123,
        (byte) 209,
        (byte) 123,
        (byte) 193,
        (byte) 189,
        (byte) 59,
        (byte) 226,
        (byte) 82,
        (byte) 203,
        (byte) 9,
        (byte) 92,
        (byte) 248,
        (byte) 54,
        (byte) 37
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 138,
        (byte) 120,
        (byte) 72,
        (byte) 196,
        (byte) 201,
        (byte) 191,
        (byte) 163,
        (byte) 132,
        (byte) 49,
        (byte) 145,
        byte.MaxValue,
        (byte) 236,
        (byte) 76,
        (byte) 35,
        (byte) 6,
        (byte) 56,
        (byte) 98,
        (byte) 239,
        (byte) 162,
        (byte) 176 /*0xB0*/,
        (byte) 248,
        (byte) 94,
        (byte) 211,
        (byte) 138,
        (byte) 101,
        (byte) 118,
        (byte) 204,
        (byte) 191,
        (byte) 217,
        (byte) 183,
        (byte) 159,
        (byte) 239,
        (byte) 7,
        (byte) 136,
        (byte) 140,
        (byte) 43,
        (byte) 171,
        (byte) 89,
        (byte) 16 /*0x10*/,
        (byte) 242,
        (byte) 126,
        (byte) 213,
        (byte) 188,
        (byte) 2,
        (byte) 140,
        (byte) 27,
        (byte) 75,
        (byte) 152,
        (byte) 50,
        (byte) 126,
        (byte) 180,
        (byte) 103,
        (byte) 138,
        (byte) 157,
        (byte) 129
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[16 /*0x10*/]
      {
        (byte) 74,
        (byte) 106,
        (byte) 124,
        (byte) 80 /*0x50*/,
        (byte) 30,
        (byte) 62,
        (byte) 7,
        (byte) 234,
        (byte) 7,
        (byte) 80 /*0x50*/,
        (byte) 111,
        (byte) 104,
        (byte) 92,
        (byte) 197,
        (byte) 130,
        (byte) 172
      };
      byte[] numArray5 = new byte[16 /*0x10*/];
      numArray5[5] = (byte) 12;
      numArray5[1] = (byte) 220;
      numArray5[2] = (byte) 66;
      numArray5[3] = (byte) 112 /*0x70*/;
      numArray5[4] = (byte) 231;
      numArray5[8] = (byte) 121;
      numArray5[6] = (byte) 59;
      numArray5[7] = (byte) 181;
      numArray5[13] = (byte) 83;
      numArray5[0] = (byte) 192 /*0xC0*/;
      numArray5[10] = (byte) 191;
      numArray5[11] = (byte) 32 /*0x20*/;
      numArray5[14] = (byte) 74;
      numArray5[12] = (byte) 2;
      numArray5[9] = (byte) 187;
      numArray5[15] = (byte) 28;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 16 /*0x10*/);
      for (int index = 0; index < 16 /*0x10*/; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[71];
    byte[] numArray7 = new byte[55];
    numArray7[5] = (byte) 201;
    numArray7[34] = (byte) 66;
    numArray7[51] = (byte) 150;
    numArray7[3] = (byte) 108;
    numArray7[47] = (byte) 77;
    numArray7[44] = (byte) 154;
    numArray7[6] = (byte) 144 /*0x90*/;
    numArray7[7] = (byte) 186;
    numArray7[32 /*0x20*/] = (byte) 175;
    numArray7[30] = (byte) 161;
    numArray7[48 /*0x30*/] = (byte) 12;
    numArray7[11] = (byte) 245;
    numArray7[12] = (byte) 5;
    numArray7[28] = (byte) 54;
    numArray7[14] = (byte) 176 /*0xB0*/;
    numArray7[15] = (byte) 208 /*0xD0*/;
    numArray7[4] = (byte) 60;
    numArray7[17] = (byte) 55;
    numArray7[2] = (byte) 78;
    numArray7[19] = (byte) 182;
    numArray7[8] = (byte) 108;
    numArray7[21] = (byte) 33;
    numArray7[39] = (byte) 225;
    numArray7[35] = (byte) 159;
    numArray7[36] = (byte) 217;
    numArray7[25] = (byte) 246;
    numArray7[18] = (byte) 202;
    numArray7[27] = (byte) 46;
    numArray7[9] = (byte) 204;
    numArray7[29] = (byte) 37;
    numArray7[54] = (byte) 44;
    numArray7[31 /*0x1F*/] = (byte) 196;
    numArray7[13] = (byte) 134;
    numArray7[33] = (byte) 74;
    numArray7[0] = (byte) 63 /*0x3F*/;
    numArray7[43] = (byte) 192 /*0xC0*/;
    numArray7[16 /*0x10*/] = (byte) 184;
    numArray7[1] = (byte) 13;
    numArray7[38] = (byte) 178;
    numArray7[45] = (byte) 128 /*0x80*/;
    numArray7[37] = (byte) 17;
    numArray7[41] = (byte) 65;
    numArray7[42] = (byte) 220;
    numArray7[20] = (byte) 79;
    numArray7[22] = (byte) 239;
    numArray7[46] = (byte) 66;
    numArray7[23] = (byte) 60;
    numArray7[26] = (byte) 51;
    numArray7[24] = (byte) 168;
    numArray7[49] = (byte) 0;
    numArray7[50] = (byte) 114;
    numArray7[10] = (byte) 122;
    numArray7[52] = (byte) 217;
    numArray7[53] = (byte) 246;
    numArray7[40] = (byte) 229;
    byte[] numArray8 = new byte[55];
    numArray8[42] = (byte) 4;
    numArray8[1] = (byte) 46;
    numArray8[53] = (byte) 3;
    numArray8[3] = (byte) 6;
    numArray8[48 /*0x30*/] = (byte) 14;
    numArray8[5] = (byte) 235;
    numArray8[6] = (byte) 76;
    numArray8[40] = (byte) 167;
    numArray8[44] = (byte) 246;
    numArray8[9] = (byte) 165;
    numArray8[13] = (byte) 48 /*0x30*/;
    numArray8[32 /*0x20*/] = (byte) 17;
    numArray8[12] = (byte) 60;
    numArray8[24] = (byte) 46;
    numArray8[14] = (byte) 27;
    numArray8[18] = (byte) 230;
    numArray8[8] = (byte) 36;
    numArray8[17] = (byte) 200;
    numArray8[39] = (byte) 30;
    numArray8[43] = (byte) 178;
    numArray8[20] = (byte) 250;
    numArray8[21] = (byte) 85;
    numArray8[10] = (byte) 37;
    numArray8[0] = (byte) 41;
    numArray8[2] = (byte) 220;
    numArray8[25] = (byte) 230;
    numArray8[26] = (byte) 66;
    numArray8[27] = (byte) 195;
    numArray8[49] = (byte) 244;
    numArray8[29] = (byte) 93;
    numArray8[30] = (byte) 151;
    numArray8[31 /*0x1F*/] = (byte) 29;
    numArray8[16 /*0x10*/] = (byte) 128 /*0x80*/;
    numArray8[33] = (byte) 139;
    numArray8[22] = (byte) 128 /*0x80*/;
    numArray8[35] = (byte) 89;
    numArray8[36] = (byte) 0;
    numArray8[7] = (byte) 109;
    numArray8[41] = (byte) 50;
    numArray8[15] = (byte) 127 /*0x7F*/;
    numArray8[38] = (byte) 90;
    numArray8[37] = (byte) 66;
    numArray8[54] = (byte) 190;
    numArray8[23] = (byte) 151;
    numArray8[51] = (byte) 171;
    numArray8[45] = (byte) 123;
    numArray8[34] = (byte) 185;
    numArray8[47] = (byte) 222;
    numArray8[11] = (byte) 133;
    numArray8[50] = (byte) 108;
    numArray8[4] = (byte) 55;
    numArray8[28] = (byte) 122;
    numArray8[52] = (byte) 135;
    numArray8[19] = (byte) 238;
    numArray8[46] = (byte) 125;
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[16 /*0x10*/]
    {
      (byte) 178,
      (byte) 173,
      (byte) 101,
      (byte) 209,
      (byte) 27,
      (byte) 33,
      (byte) 173,
      (byte) 36,
      (byte) 208 /*0xD0*/,
      (byte) 247,
      (byte) 158,
      (byte) 216,
      (byte) 253,
      (byte) 217,
      (byte) 168,
      (byte) 62
    };
    byte[] numArray10 = new byte[16 /*0x10*/]
    {
      (byte) 239,
      (byte) 35,
      (byte) 206,
      (byte) 17,
      (byte) 106,
      (byte) 4,
      (byte) 84,
      (byte) 222,
      (byte) 69,
      (byte) 140,
      (byte) 249,
      (byte) 14,
      (byte) 119,
      (byte) 233,
      (byte) 222,
      (byte) 172
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 16 /*0x10*/);
    for (int index = 0; index < 16 /*0x10*/; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_12801()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[87];
      byte[] numArray2 = new byte[55];
      numArray2[2] = (byte) 63 /*0x3F*/;
      numArray2[12] = (byte) 28;
      numArray2[28] = (byte) 201;
      numArray2[3] = (byte) 67;
      numArray2[4] = (byte) 246;
      numArray2[41] = (byte) 56;
      numArray2[6] = (byte) 81;
      numArray2[7] = (byte) 116;
      numArray2[17] = (byte) 203;
      numArray2[9] = (byte) 168;
      numArray2[43] = (byte) 40;
      numArray2[40] = (byte) 207;
      numArray2[18] = (byte) 37;
      numArray2[13] = (byte) 58;
      numArray2[14] = (byte) 164;
      numArray2[25] = (byte) 15;
      numArray2[37] = (byte) 33;
      numArray2[42] = (byte) 33;
      numArray2[33] = (byte) 126;
      numArray2[21] = (byte) 83;
      numArray2[20] = (byte) 83;
      numArray2[50] = (byte) 201;
      numArray2[22] = (byte) 72;
      numArray2[23] = (byte) 182;
      numArray2[24] = (byte) 74;
      numArray2[30] = (byte) 81;
      numArray2[26] = (byte) 111;
      numArray2[27] = (byte) 0;
      numArray2[16 /*0x10*/] = (byte) 116;
      numArray2[5] = (byte) 184;
      numArray2[29] = (byte) 185;
      numArray2[32 /*0x20*/] = (byte) 237;
      numArray2[38] = (byte) 218;
      numArray2[31 /*0x1F*/] = (byte) 203;
      numArray2[34] = (byte) 55;
      numArray2[35] = (byte) 13;
      numArray2[36] = (byte) 110;
      numArray2[0] = (byte) 32 /*0x20*/;
      numArray2[49] = (byte) 165;
      numArray2[39] = (byte) 99;
      numArray2[19] = (byte) 78;
      numArray2[46] = (byte) 187;
      numArray2[10] = (byte) 188;
      numArray2[53] = (byte) 146;
      numArray2[51] = (byte) 54;
      numArray2[15] = (byte) 3;
      numArray2[11] = (byte) 49;
      numArray2[47] = (byte) 194;
      numArray2[48 /*0x30*/] = (byte) 53;
      numArray2[1] = (byte) 165;
      numArray2[45] = (byte) 108;
      numArray2[44] = (byte) 59;
      numArray2[52] = (byte) 230;
      numArray2[54] = (byte) 89;
      numArray2[8] = (byte) 2;
      byte[] numArray3 = new byte[55];
      numArray3[30] = (byte) 13;
      numArray3[1] = (byte) 14;
      numArray3[36] = (byte) 211;
      numArray3[17] = (byte) 185;
      numArray3[4] = (byte) 118;
      numArray3[5] = (byte) 252;
      numArray3[12] = (byte) 132;
      numArray3[7] = (byte) 27;
      numArray3[2] = (byte) 158;
      numArray3[43] = (byte) 8;
      numArray3[10] = (byte) 104;
      numArray3[35] = (byte) 166;
      numArray3[25] = (byte) 9;
      numArray3[29] = (byte) 112 /*0x70*/;
      numArray3[52] = (byte) 86;
      numArray3[15] = (byte) 7;
      numArray3[16 /*0x10*/] = (byte) 205;
      numArray3[20] = (byte) 102;
      numArray3[41] = (byte) 211;
      numArray3[31 /*0x1F*/] = (byte) 249;
      numArray3[6] = (byte) 28;
      numArray3[21] = (byte) 4;
      numArray3[22] = (byte) 229;
      numArray3[33] = (byte) 250;
      numArray3[24] = (byte) 18;
      numArray3[8] = (byte) 91;
      numArray3[26] = (byte) 72;
      numArray3[54] = (byte) 49;
      numArray3[11] = (byte) 170;
      numArray3[38] = (byte) 92;
      numArray3[18] = (byte) 25;
      numArray3[46] = (byte) 181;
      numArray3[27] = (byte) 135;
      numArray3[32 /*0x20*/] = (byte) 200;
      numArray3[28] = (byte) 89;
      numArray3[34] = (byte) 199;
      numArray3[13] = (byte) 177;
      numArray3[37] = (byte) 240 /*0xF0*/;
      numArray3[53] = (byte) 206;
      numArray3[39] = (byte) 139;
      numArray3[40] = (byte) 179;
      numArray3[0] = (byte) 109;
      numArray3[42] = (byte) 131;
      numArray3[51] = (byte) 57;
      numArray3[14] = (byte) 105;
      numArray3[45] = (byte) 28;
      numArray3[19] = (byte) 68;
      numArray3[47] = (byte) 244;
      numArray3[48 /*0x30*/] = (byte) 225;
      numArray3[49] = (byte) 188;
      numArray3[50] = (byte) 34;
      numArray3[44] = (byte) 254;
      numArray3[23] = (byte) 193;
      numArray3[9] = (byte) 187;
      numArray3[3] = (byte) 4;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[32 /*0x20*/]
      {
        (byte) 221,
        (byte) 99,
        (byte) 91,
        (byte) 175,
        (byte) 88,
        (byte) 113,
        (byte) 221,
        (byte) 102,
        (byte) 74,
        (byte) 161,
        (byte) 148,
        (byte) 198,
        (byte) 112 /*0x70*/,
        (byte) 215,
        (byte) 29,
        (byte) 155,
        (byte) 41,
        (byte) 6,
        (byte) 132,
        (byte) 214,
        (byte) 185,
        (byte) 241,
        (byte) 119,
        (byte) 148,
        (byte) 194,
        (byte) 116,
        (byte) 179,
        (byte) 47,
        (byte) 139,
        (byte) 70,
        (byte) 51,
        (byte) 50
      };
      byte[] numArray5 = new byte[32 /*0x20*/];
      numArray5[23] = (byte) 80 /*0x50*/;
      numArray5[15] = (byte) 224 /*0xE0*/;
      numArray5[2] = (byte) 51;
      numArray5[20] = (byte) 180;
      numArray5[30] = (byte) 132;
      numArray5[4] = (byte) 233;
      numArray5[28] = (byte) 65;
      numArray5[9] = (byte) 159;
      numArray5[29] = (byte) 126;
      numArray5[16 /*0x10*/] = (byte) 157;
      numArray5[1] = (byte) 152;
      numArray5[11] = (byte) 172;
      numArray5[22] = (byte) 115;
      numArray5[13] = (byte) 134;
      numArray5[25] = (byte) 144 /*0x90*/;
      numArray5[8] = (byte) 16 /*0x10*/;
      numArray5[3] = (byte) 79;
      numArray5[17] = (byte) 212;
      numArray5[18] = (byte) 131;
      numArray5[0] = (byte) 96 /*0x60*/;
      numArray5[14] = (byte) 60;
      numArray5[10] = (byte) 176 /*0xB0*/;
      numArray5[6] = (byte) 39;
      numArray5[12] = (byte) 18;
      numArray5[24] = (byte) 225;
      numArray5[21] = (byte) 237;
      numArray5[26] = (byte) 36;
      numArray5[27] = (byte) 32 /*0x20*/;
      numArray5[19] = (byte) 168;
      numArray5[5] = (byte) 249;
      numArray5[7] = (byte) 92;
      numArray5[31 /*0x1F*/] = (byte) 110;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 32 /*0x20*/);
      for (int index = 0; index < 32 /*0x20*/; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[45];
      byte[] response = new byte[45];
      Array.Copy((Array) sc_12780.sspq, 302, (Array) numArray6, 0, 45);
      key.Query(true, 335, numArray6, response);
      Array.Copy((Array) sc_12780.sspr, 302, (Array) numArray6, 0, 45);
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
    byte[] numArray7 = new byte[87];
    byte[] numArray8 = new byte[55];
    numArray8[25] = (byte) 198;
    numArray8[1] = (byte) 184;
    numArray8[2] = (byte) 148;
    numArray8[3] = (byte) 172;
    numArray8[11] = (byte) 181;
    numArray8[5] = (byte) 15;
    numArray8[37] = (byte) 73;
    numArray8[27] = (byte) 126;
    numArray8[13] = (byte) 121;
    numArray8[9] = (byte) 145;
    numArray8[10] = (byte) 207;
    numArray8[15] = (byte) 22;
    numArray8[12] = (byte) 89;
    numArray8[48 /*0x30*/] = (byte) 252;
    numArray8[14] = (byte) 170;
    numArray8[43] = (byte) 206;
    numArray8[6] = (byte) 72;
    numArray8[40] = (byte) 231;
    numArray8[20] = (byte) 217;
    numArray8[19] = (byte) 170;
    numArray8[51] = (byte) 114;
    numArray8[50] = (byte) 253;
    numArray8[22] = (byte) 204;
    numArray8[23] = (byte) 126;
    numArray8[30] = (byte) 173;
    numArray8[41] = (byte) 79;
    numArray8[26] = (byte) 254;
    numArray8[17] = (byte) 54;
    numArray8[49] = (byte) 41;
    numArray8[29] = (byte) 230;
    numArray8[28] = (byte) 196;
    numArray8[24] = (byte) 71;
    numArray8[32 /*0x20*/] = (byte) 170;
    numArray8[33] = (byte) 250;
    numArray8[34] = (byte) 84;
    numArray8[18] = (byte) 10;
    numArray8[36] = (byte) 67;
    numArray8[21] = (byte) 55;
    numArray8[52] = (byte) 229;
    numArray8[47] = (byte) 124;
    numArray8[7] = (byte) 93;
    numArray8[0] = (byte) 141;
    numArray8[39] = (byte) 226;
    numArray8[35] = (byte) 3;
    numArray8[44] = (byte) 221;
    numArray8[45] = (byte) 4;
    numArray8[46] = (byte) 189;
    numArray8[4] = (byte) 252;
    numArray8[38] = (byte) 87;
    numArray8[31 /*0x1F*/] = (byte) 29;
    numArray8[53] = (byte) 104;
    numArray8[16 /*0x10*/] = (byte) 60;
    numArray8[8] = (byte) 25;
    numArray8[42] = (byte) 15;
    numArray8[54] = (byte) 105;
    byte[] numArray9 = new byte[55]
    {
      (byte) 31 /*0x1F*/,
      (byte) 210,
      (byte) 134,
      (byte) 210,
      (byte) 82,
      (byte) 253,
      (byte) 77,
      (byte) 145,
      (byte) 96 /*0x60*/,
      (byte) 213,
      (byte) 252,
      (byte) 6,
      (byte) 2,
      (byte) 118,
      (byte) 106,
      (byte) 65,
      (byte) 183,
      (byte) 167,
      (byte) 65,
      (byte) 242,
      (byte) 49,
      (byte) 217,
      (byte) 140,
      (byte) 174,
      (byte) 101,
      (byte) 130,
      (byte) 101,
      (byte) 126,
      (byte) 114,
      (byte) 102,
      (byte) 119,
      (byte) 9,
      (byte) 186,
      (byte) 249,
      (byte) 253,
      (byte) 120,
      (byte) 69,
      (byte) 118,
      (byte) 167,
      (byte) 226,
      (byte) 82,
      (byte) 146,
      (byte) 63 /*0x3F*/,
      (byte) 106,
      (byte) 198,
      (byte) 19,
      (byte) 98,
      (byte) 196,
      (byte) 181,
      (byte) 181,
      (byte) 178,
      (byte) 251,
      (byte) 4,
      (byte) 232,
      (byte) 109
    };
    key.Query(true, 335, numArray8, numArray8);
    Array.Copy((Array) numArray8, 0, (Array) numArray7, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray7[index] ^= numArray9[index];
    byte[] numArray10 = new byte[32 /*0x20*/]
    {
      (byte) 117,
      (byte) 34,
      (byte) 44,
      (byte) 142,
      (byte) 71,
      (byte) 254,
      (byte) 74,
      (byte) 169,
      (byte) 120,
      (byte) 200,
      (byte) 205,
      (byte) 39,
      (byte) 144 /*0x90*/,
      (byte) 164,
      (byte) 15,
      (byte) 188,
      (byte) 87,
      (byte) 165,
      (byte) 211,
      (byte) 76,
      (byte) 19,
      (byte) 116,
      (byte) 72,
      (byte) 171,
      (byte) 43,
      (byte) 45,
      (byte) 67,
      (byte) 222,
      (byte) 166,
      (byte) 123,
      (byte) 156,
      (byte) 178
    };
    byte[] numArray11 = new byte[32 /*0x20*/];
    numArray11[11] = (byte) 36;
    numArray11[1] = (byte) 54;
    numArray11[2] = (byte) 232;
    numArray11[3] = (byte) 217;
    numArray11[7] = (byte) 2;
    numArray11[6] = (byte) 94;
    numArray11[4] = (byte) 46;
    numArray11[19] = (byte) 159;
    numArray11[23] = (byte) 129;
    numArray11[16 /*0x10*/] = (byte) 87;
    numArray11[10] = (byte) 198;
    numArray11[5] = (byte) 213;
    numArray11[12] = (byte) 142;
    numArray11[13] = (byte) 57;
    numArray11[14] = (byte) 16 /*0x10*/;
    numArray11[29] = (byte) 162;
    numArray11[30] = (byte) 251;
    numArray11[18] = (byte) 51;
    numArray11[9] = (byte) 194;
    numArray11[15] = (byte) 207;
    numArray11[20] = (byte) 207;
    numArray11[21] = (byte) 19;
    numArray11[28] = (byte) 114;
    numArray11[31 /*0x1F*/] = (byte) 61;
    numArray11[17] = (byte) 237;
    numArray11[25] = (byte) 160 /*0xA0*/;
    numArray11[26] = (byte) 216;
    numArray11[27] = (byte) 184;
    numArray11[8] = (byte) 174;
    numArray11[0] = (byte) 48 /*0x30*/;
    numArray11[24] = (byte) 182;
    numArray11[22] = (byte) 103;
    key.Query(true, 335, numArray10, numArray10);
    Array.Copy((Array) numArray10, 0, (Array) numArray7, 55, 32 /*0x20*/);
    for (int index = 0; index < 32 /*0x20*/; ++index)
      numArray7[index + 55] ^= numArray11[index];
    return Encoding.UTF8.GetString(numArray7);
  }

  internal static string ssp_appserver_12802()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[120];
      byte[] numArray2 = new byte[55];
      numArray2[2] = (byte) 199;
      numArray2[24] = (byte) 223;
      numArray2[39] = (byte) 238;
      numArray2[3] = (byte) 20;
      numArray2[4] = (byte) 207;
      numArray2[47] = (byte) 41;
      numArray2[1] = (byte) 77;
      numArray2[21] = (byte) 184;
      numArray2[6] = (byte) 187;
      numArray2[5] = (byte) 28;
      numArray2[45] = (byte) 102;
      numArray2[9] = (byte) 23;
      numArray2[0] = (byte) 236;
      numArray2[13] = (byte) 144 /*0x90*/;
      numArray2[50] = (byte) 231;
      numArray2[36] = (byte) 16 /*0x10*/;
      numArray2[38] = (byte) 49;
      numArray2[7] = (byte) 211;
      numArray2[18] = (byte) 167;
      numArray2[35] = (byte) 173;
      numArray2[20] = (byte) 78;
      numArray2[51] = (byte) 144 /*0x90*/;
      numArray2[22] = (byte) 231;
      numArray2[23] = (byte) 116;
      numArray2[25] = (byte) 188;
      numArray2[15] = (byte) 207;
      numArray2[26] = (byte) 253;
      numArray2[27] = (byte) 35;
      numArray2[10] = (byte) 25;
      numArray2[43] = (byte) 194;
      numArray2[46] = (byte) 176 /*0xB0*/;
      numArray2[30] = (byte) 4;
      numArray2[41] = (byte) 6;
      numArray2[19] = (byte) 238;
      numArray2[33] = (byte) 92;
      numArray2[40] = (byte) 37;
      numArray2[12] = (byte) 105;
      numArray2[37] = (byte) 80 /*0x50*/;
      numArray2[31 /*0x1F*/] = (byte) 118;
      numArray2[34] = (byte) 198;
      numArray2[8] = (byte) 193;
      numArray2[29] = (byte) 181;
      numArray2[42] = (byte) 62;
      numArray2[16 /*0x10*/] = (byte) 21;
      numArray2[44] = (byte) 103;
      numArray2[11] = (byte) 252;
      numArray2[17] = (byte) 155;
      numArray2[49] = (byte) 51;
      numArray2[48 /*0x30*/] = (byte) 137;
      numArray2[28] = (byte) 61;
      numArray2[32 /*0x20*/] = (byte) 215;
      numArray2[14] = (byte) 227;
      numArray2[52] = (byte) 114;
      numArray2[53] = (byte) 104;
      numArray2[54] = (byte) 25;
      byte[] numArray3 = new byte[55]
      {
        (byte) 185,
        (byte) 39,
        (byte) 185,
        (byte) 32 /*0x20*/,
        (byte) 178,
        (byte) 233,
        (byte) 138,
        (byte) 85,
        (byte) 79,
        (byte) 60,
        (byte) 129,
        (byte) 99,
        (byte) 208 /*0xD0*/,
        (byte) 165,
        (byte) 148,
        (byte) 149,
        (byte) 58,
        (byte) 74,
        (byte) 88,
        (byte) 14,
        (byte) 31 /*0x1F*/,
        (byte) 141,
        (byte) 121,
        (byte) 116,
        (byte) 170,
        (byte) 133,
        (byte) 119,
        (byte) 220,
        (byte) 238,
        (byte) 102,
        (byte) 186,
        (byte) 177,
        (byte) 39,
        (byte) 112 /*0x70*/,
        (byte) 144 /*0x90*/,
        (byte) 45,
        (byte) 96 /*0x60*/,
        (byte) 218,
        (byte) 89,
        (byte) 172,
        (byte) 191,
        (byte) 239,
        (byte) 135,
        (byte) 153,
        (byte) 129,
        (byte) 176 /*0xB0*/,
        (byte) 9,
        (byte) 104,
        (byte) 43,
        (byte) 97,
        (byte) 248,
        (byte) 73,
        (byte) 98,
        (byte) 53,
        (byte) 224 /*0xE0*/
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 69,
        (byte) 188,
        (byte) 7,
        (byte) 75,
        (byte) 253,
        (byte) 141,
        (byte) 62,
        (byte) 157,
        (byte) 158,
        (byte) 180,
        (byte) 202,
        (byte) 55,
        (byte) 238,
        (byte) 0,
        (byte) 168,
        (byte) 76,
        (byte) 232,
        (byte) 58,
        (byte) 215,
        (byte) 71,
        (byte) 116,
        (byte) 73,
        (byte) 53,
        (byte) 103,
        (byte) 116,
        (byte) 70,
        (byte) 148,
        (byte) 170,
        (byte) 78,
        (byte) 74,
        (byte) 207,
        (byte) 112 /*0x70*/,
        (byte) 216,
        (byte) 5,
        (byte) 120,
        (byte) 95,
        (byte) 22,
        (byte) 132,
        (byte) 145,
        (byte) 226,
        (byte) 243,
        (byte) 18,
        (byte) 37,
        (byte) 141,
        (byte) 217,
        (byte) 39,
        (byte) 100,
        (byte) 116,
        (byte) 122,
        (byte) 217,
        (byte) 208 /*0xD0*/,
        (byte) 100,
        (byte) 251,
        (byte) 114,
        (byte) 204
      };
      byte[] numArray5 = new byte[55];
      numArray5[24] = (byte) 18;
      numArray5[1] = (byte) 249;
      numArray5[2] = (byte) 193;
      numArray5[44] = (byte) 88;
      numArray5[46] = (byte) 53;
      numArray5[5] = (byte) 178;
      numArray5[33] = (byte) 65;
      numArray5[40] = (byte) 109;
      numArray5[38] = (byte) 115;
      numArray5[39] = (byte) 96 /*0x60*/;
      numArray5[10] = (byte) 229;
      numArray5[13] = (byte) 245;
      numArray5[12] = (byte) 13;
      numArray5[7] = (byte) 127 /*0x7F*/;
      numArray5[14] = (byte) 217;
      numArray5[4] = (byte) 81;
      numArray5[16 /*0x10*/] = (byte) 225;
      numArray5[17] = (byte) 160 /*0xA0*/;
      numArray5[30] = (byte) 113;
      numArray5[3] = (byte) 144 /*0x90*/;
      numArray5[34] = (byte) 206;
      numArray5[21] = (byte) 169;
      numArray5[22] = (byte) 239;
      numArray5[19] = (byte) 94;
      numArray5[26] = (byte) 136;
      numArray5[25] = (byte) 86;
      numArray5[9] = (byte) 155;
      numArray5[27] = (byte) 214;
      numArray5[23] = (byte) 254;
      numArray5[29] = (byte) 46;
      numArray5[6] = (byte) 159;
      numArray5[31 /*0x1F*/] = (byte) 230;
      numArray5[37] = (byte) 233;
      numArray5[48 /*0x30*/] = (byte) 42;
      numArray5[54] = (byte) 222;
      numArray5[35] = (byte) 118;
      numArray5[36] = (byte) 33;
      numArray5[20] = (byte) 47;
      numArray5[43] = (byte) 27;
      numArray5[42] = (byte) 66;
      numArray5[0] = (byte) 84;
      numArray5[41] = (byte) 58;
      numArray5[15] = (byte) 179;
      numArray5[8] = (byte) 128 /*0x80*/;
      numArray5[18] = (byte) 78;
      numArray5[45] = (byte) 233;
      numArray5[52] = (byte) 110;
      numArray5[47] = (byte) 139;
      numArray5[28] = (byte) 78;
      numArray5[49] = (byte) 12;
      numArray5[11] = (byte) 143;
      numArray5[51] = (byte) 246;
      numArray5[50] = (byte) 160 /*0xA0*/;
      numArray5[53] = (byte) 57;
      numArray5[32 /*0x20*/] = (byte) 231;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[10]
      {
        (byte) 17,
        (byte) 138,
        (byte) 212,
        (byte) 218,
        (byte) 99,
        (byte) 217,
        (byte) 217,
        (byte) 121,
        (byte) 229,
        (byte) 248
      };
      byte[] numArray7 = new byte[10]
      {
        (byte) 96 /*0x60*/,
        (byte) 10,
        (byte) 113,
        (byte) 107,
        (byte) 126,
        (byte) 101,
        (byte) 150,
        (byte) 171,
        (byte) 195,
        (byte) 3
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[20];
      byte[] response = new byte[20];
      Array.Copy((Array) sc_12780.sspq, 347, (Array) numArray8, 0, 20);
      key.Query(true, 335, numArray8, response);
      Array.Copy((Array) sc_12780.sspr, 347, (Array) numArray8, 0, 20);
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
    byte[] numArray9 = new byte[120];
    byte[] numArray10 = new byte[55]
    {
      (byte) 101,
      (byte) 35,
      (byte) 6,
      (byte) 15,
      (byte) 252,
      (byte) 233,
      (byte) 248,
      (byte) 47,
      (byte) 154,
      (byte) 207,
      (byte) 203,
      (byte) 224 /*0xE0*/,
      (byte) 191,
      (byte) 228,
      (byte) 182,
      (byte) 236,
      (byte) 251,
      (byte) 41,
      (byte) 203,
      (byte) 134,
      (byte) 223,
      (byte) 2,
      (byte) 6,
      (byte) 109,
      (byte) 167,
      (byte) 99,
      (byte) 7,
      (byte) 203,
      (byte) 201,
      (byte) 118,
      (byte) 175,
      (byte) 226,
      (byte) 104,
      (byte) 118,
      (byte) 89,
      (byte) 147,
      (byte) 200,
      (byte) 252,
      (byte) 247,
      (byte) 181,
      (byte) 90,
      (byte) 208 /*0xD0*/,
      (byte) 116,
      (byte) 180,
      (byte) 155,
      (byte) 82,
      (byte) 64 /*0x40*/,
      (byte) 237,
      (byte) 87,
      (byte) 195,
      (byte) 115,
      (byte) 31 /*0x1F*/,
      (byte) 2,
      (byte) 29,
      (byte) 26
    };
    byte[] numArray11 = new byte[55];
    numArray11[1] = (byte) 95;
    numArray11[4] = (byte) 17;
    numArray11[42] = (byte) 165;
    numArray11[3] = (byte) 148;
    numArray11[44] = (byte) 254;
    numArray11[5] = (byte) 75;
    numArray11[51] = (byte) 170;
    numArray11[7] = (byte) 45;
    numArray11[20] = (byte) 56;
    numArray11[35] = (byte) 30;
    numArray11[10] = (byte) 239;
    numArray11[52] = (byte) 90;
    numArray11[12] = (byte) 32 /*0x20*/;
    numArray11[31 /*0x1F*/] = (byte) 250;
    numArray11[14] = (byte) 17;
    numArray11[15] = (byte) 210;
    numArray11[38] = (byte) 151;
    numArray11[17] = (byte) 189;
    numArray11[18] = (byte) 187;
    numArray11[19] = (byte) 63 /*0x3F*/;
    numArray11[9] = (byte) 192 /*0xC0*/;
    numArray11[21] = (byte) 173;
    numArray11[48 /*0x30*/] = (byte) 193;
    numArray11[16 /*0x10*/] = (byte) 220;
    numArray11[24] = (byte) 199;
    numArray11[13] = (byte) 251;
    numArray11[41] = (byte) 13;
    numArray11[6] = (byte) 193;
    numArray11[2] = (byte) 186;
    numArray11[27] = (byte) 147;
    numArray11[30] = (byte) 233;
    numArray11[45] = (byte) 90;
    numArray11[54] = (byte) 124;
    numArray11[29] = (byte) 173;
    numArray11[22] = (byte) 89;
    numArray11[40] = (byte) 78;
    numArray11[8] = (byte) 17;
    numArray11[37] = (byte) 140;
    numArray11[32 /*0x20*/] = (byte) 91;
    numArray11[23] = (byte) 134;
    numArray11[39] = (byte) 74;
    numArray11[46] = (byte) 209;
    numArray11[36] = (byte) 212;
    numArray11[43] = (byte) 2;
    numArray11[25] = (byte) 222;
    numArray11[28] = (byte) 89;
    numArray11[0] = (byte) 235;
    numArray11[34] = (byte) 213;
    numArray11[11] = (byte) 179;
    numArray11[47] = (byte) 170;
    numArray11[50] = (byte) 67;
    numArray11[33] = (byte) 67;
    numArray11[26] = (byte) 78;
    numArray11[53] = (byte) 225;
    numArray11[49] = (byte) 4;
    key.Query(true, 335, numArray10, numArray10);
    Array.Copy((Array) numArray10, 0, (Array) numArray9, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray9[index] ^= numArray11[index];
    byte[] numArray12 = new byte[55];
    numArray12[48 /*0x30*/] = (byte) 10;
    numArray12[1] = (byte) 19;
    numArray12[28] = (byte) 164;
    numArray12[3] = (byte) 199;
    numArray12[49] = (byte) 23;
    numArray12[25] = (byte) 64 /*0x40*/;
    numArray12[6] = (byte) 32 /*0x20*/;
    numArray12[12] = (byte) 173;
    numArray12[50] = (byte) 168;
    numArray12[9] = (byte) 44;
    numArray12[31 /*0x1F*/] = (byte) 80 /*0x50*/;
    numArray12[11] = (byte) 146;
    numArray12[16 /*0x10*/] = (byte) 66;
    numArray12[13] = (byte) 243;
    numArray12[14] = (byte) 106;
    numArray12[29] = (byte) 244;
    numArray12[22] = (byte) 121;
    numArray12[30] = (byte) 89;
    numArray12[10] = (byte) 236;
    numArray12[19] = (byte) 62;
    numArray12[20] = (byte) 189;
    numArray12[47] = (byte) 172;
    numArray12[41] = (byte) 5;
    numArray12[32 /*0x20*/] = (byte) 35;
    numArray12[24] = (byte) 182;
    numArray12[34] = (byte) 104;
    numArray12[26] = (byte) 102;
    numArray12[27] = (byte) 142;
    numArray12[4] = (byte) 183;
    numArray12[23] = (byte) 42;
    numArray12[33] = (byte) 171;
    numArray12[44] = (byte) 11;
    numArray12[45] = (byte) 208 /*0xD0*/;
    numArray12[2] = (byte) 119;
    numArray12[5] = (byte) 94;
    numArray12[35] = (byte) 129;
    numArray12[15] = (byte) 212;
    numArray12[37] = (byte) 11;
    numArray12[17] = (byte) 254;
    numArray12[39] = (byte) 12;
    numArray12[18] = (byte) 163;
    numArray12[40] = (byte) 47;
    numArray12[42] = (byte) 170;
    numArray12[43] = (byte) 174;
    numArray12[8] = (byte) 8;
    numArray12[36] = (byte) 230;
    numArray12[46] = (byte) 134;
    numArray12[51] = (byte) 243;
    numArray12[38] = (byte) 118;
    numArray12[21] = (byte) 31 /*0x1F*/;
    numArray12[7] = (byte) 143;
    numArray12[0] = (byte) 198;
    numArray12[52] = (byte) 103;
    numArray12[53] = (byte) 149;
    numArray12[54] = (byte) 13;
    byte[] numArray13 = new byte[55]
    {
      (byte) 79,
      (byte) 115,
      (byte) 19,
      (byte) 34,
      (byte) 215,
      (byte) 97,
      (byte) 94,
      (byte) 182,
      (byte) 74,
      (byte) 107,
      (byte) 30,
      (byte) 117,
      (byte) 127 /*0x7F*/,
      (byte) 112 /*0x70*/,
      (byte) 85,
      (byte) 190,
      (byte) 140,
      (byte) 147,
      (byte) 182,
      (byte) 122,
      (byte) 225,
      (byte) 75,
      (byte) 53,
      (byte) 71,
      (byte) 68,
      (byte) 11,
      (byte) 127 /*0x7F*/,
      (byte) 14,
      (byte) 87,
      (byte) 50,
      (byte) 20,
      (byte) 180,
      (byte) 58,
      (byte) 56,
      (byte) 141,
      (byte) 106,
      (byte) 73,
      (byte) 52,
      (byte) 73,
      (byte) 173,
      (byte) 212,
      (byte) 234,
      (byte) 184,
      byte.MaxValue,
      (byte) 199,
      (byte) 75,
      (byte) 1,
      (byte) 111,
      (byte) 247,
      (byte) 144 /*0x90*/,
      (byte) 91,
      (byte) 117,
      (byte) 79,
      (byte) 31 /*0x1F*/,
      (byte) 135
    };
    key.Query(true, 335, numArray12, numArray12);
    Array.Copy((Array) numArray12, 0, (Array) numArray9, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray9[index + 55] ^= numArray13[index];
    byte[] numArray14 = new byte[10];
    numArray14[8] = (byte) 179;
    numArray14[0] = (byte) 45;
    numArray14[2] = (byte) 188;
    numArray14[5] = (byte) 94;
    numArray14[1] = (byte) 44;
    numArray14[3] = (byte) 62;
    numArray14[6] = (byte) 200;
    numArray14[7] = (byte) 108;
    numArray14[4] = (byte) 12;
    numArray14[9] = (byte) 225;
    byte[] numArray15 = new byte[10];
    numArray15[2] = (byte) 202;
    numArray15[1] = (byte) 243;
    numArray15[9] = (byte) 182;
    numArray15[6] = (byte) 116;
    numArray15[4] = (byte) 103;
    numArray15[5] = (byte) 158;
    numArray15[7] = (byte) 209;
    numArray15[3] = (byte) 180;
    numArray15[0] = (byte) 42;
    numArray15[8] = (byte) 214;
    key.Query(true, 335, numArray14, numArray14);
    Array.Copy((Array) numArray14, 0, (Array) numArray9, 110, 10);
    for (int index = 0; index < 10; ++index)
      numArray9[index + 110] ^= numArray15[index];
    return Encoding.UTF8.GetString(numArray9);
  }

  internal static string ssp_appserver_12803()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[140];
      byte[] numArray2 = new byte[55];
      numArray2[44] = (byte) 22;
      numArray2[48 /*0x30*/] = (byte) 165;
      numArray2[37] = (byte) 174;
      numArray2[19] = (byte) 83;
      numArray2[11] = (byte) 86;
      numArray2[5] = (byte) 187;
      numArray2[20] = (byte) 222;
      numArray2[34] = (byte) 232;
      numArray2[8] = (byte) 188;
      numArray2[9] = (byte) 128 /*0x80*/;
      numArray2[1] = (byte) 205;
      numArray2[40] = (byte) 17;
      numArray2[13] = (byte) 212;
      numArray2[14] = (byte) 52;
      numArray2[16 /*0x10*/] = (byte) 75;
      numArray2[15] = (byte) 227;
      numArray2[35] = (byte) 112 /*0x70*/;
      numArray2[28] = (byte) 113;
      numArray2[18] = (byte) 183;
      numArray2[0] = (byte) 108;
      numArray2[52] = (byte) 144 /*0x90*/;
      numArray2[21] = (byte) 125;
      numArray2[22] = (byte) 168;
      numArray2[23] = (byte) 134;
      numArray2[24] = (byte) 165;
      numArray2[25] = (byte) 169;
      numArray2[39] = (byte) 226;
      numArray2[27] = (byte) 239;
      numArray2[26] = (byte) 252;
      numArray2[29] = (byte) 154;
      numArray2[54] = (byte) 59;
      numArray2[31 /*0x1F*/] = (byte) 28;
      numArray2[32 /*0x20*/] = (byte) 59;
      numArray2[33] = (byte) 231;
      numArray2[17] = (byte) 22;
      numArray2[50] = (byte) 205;
      numArray2[36] = (byte) 69;
      numArray2[30] = (byte) 217;
      numArray2[38] = (byte) 222;
      numArray2[42] = (byte) 243;
      numArray2[12] = (byte) 31 /*0x1F*/;
      numArray2[6] = (byte) 204;
      numArray2[49] = (byte) 23;
      numArray2[43] = (byte) 3;
      numArray2[10] = (byte) 31 /*0x1F*/;
      numArray2[51] = (byte) 240 /*0xF0*/;
      numArray2[46] = (byte) 67;
      numArray2[47] = (byte) 142;
      numArray2[7] = (byte) 111;
      numArray2[41] = (byte) 128 /*0x80*/;
      numArray2[2] = (byte) 66;
      numArray2[3] = (byte) 213;
      numArray2[4] = (byte) 145;
      numArray2[53] = (byte) 58;
      numArray2[45] = (byte) 66;
      byte[] numArray3 = new byte[55];
      numArray3[21] = (byte) 239;
      numArray3[49] = (byte) 58;
      numArray3[2] = (byte) 101;
      numArray3[3] = (byte) 166;
      numArray3[26] = (byte) 105;
      numArray3[11] = (byte) 62;
      numArray3[22] = (byte) 177;
      numArray3[25] = (byte) 59;
      numArray3[8] = (byte) 246;
      numArray3[9] = (byte) 170;
      numArray3[35] = (byte) 27;
      numArray3[24] = (byte) 63 /*0x3F*/;
      numArray3[12] = (byte) 5;
      numArray3[52] = (byte) 124;
      numArray3[14] = (byte) 239;
      numArray3[23] = (byte) 56;
      numArray3[20] = (byte) 236;
      numArray3[51] = (byte) 20;
      numArray3[18] = (byte) 225;
      numArray3[19] = (byte) 36;
      numArray3[13] = (byte) 1;
      numArray3[41] = (byte) 20;
      numArray3[16 /*0x10*/] = (byte) 108;
      numArray3[31 /*0x1F*/] = (byte) 194;
      numArray3[27] = (byte) 148;
      numArray3[4] = (byte) 118;
      numArray3[42] = (byte) 70;
      numArray3[30] = (byte) 195;
      numArray3[28] = (byte) 154;
      numArray3[29] = (byte) 160 /*0xA0*/;
      numArray3[15] = (byte) 12;
      numArray3[34] = (byte) 102;
      numArray3[1] = (byte) 251;
      numArray3[33] = (byte) 126;
      numArray3[5] = (byte) 148;
      numArray3[45] = (byte) 154;
      numArray3[47] = (byte) 78;
      numArray3[40] = (byte) 172;
      numArray3[43] = (byte) 36;
      numArray3[39] = (byte) 245;
      numArray3[37] = (byte) 15;
      numArray3[0] = (byte) 8;
      numArray3[48 /*0x30*/] = (byte) 20;
      numArray3[44] = (byte) 113;
      numArray3[32 /*0x20*/] = (byte) 82;
      numArray3[7] = (byte) 19;
      numArray3[38] = (byte) 186;
      numArray3[10] = (byte) 81;
      numArray3[36] = (byte) 159;
      numArray3[17] = (byte) 54;
      numArray3[50] = (byte) 0;
      numArray3[6] = (byte) 191;
      numArray3[46] = (byte) 111;
      numArray3[53] = (byte) 2;
      numArray3[54] = (byte) 39;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 238,
        (byte) 146,
        (byte) 39,
        (byte) 133,
        (byte) 43,
        (byte) 196,
        (byte) 184,
        (byte) 141,
        (byte) 167,
        (byte) 15,
        (byte) 187,
        (byte) 92,
        (byte) 171,
        (byte) 213,
        (byte) 108,
        (byte) 203,
        (byte) 221,
        (byte) 109,
        (byte) 252,
        (byte) 60,
        (byte) 168,
        (byte) 193,
        (byte) 44,
        (byte) 218,
        (byte) 26,
        (byte) 0,
        (byte) 120,
        (byte) 139,
        (byte) 182,
        (byte) 195,
        (byte) 31 /*0x1F*/,
        (byte) 205,
        (byte) 233,
        (byte) 150,
        (byte) 79,
        (byte) 30,
        (byte) 77,
        (byte) 76,
        (byte) 117,
        (byte) 26,
        (byte) 195,
        (byte) 17,
        (byte) 244,
        byte.MaxValue,
        (byte) 197,
        (byte) 84,
        (byte) 188,
        (byte) 145,
        (byte) 164,
        (byte) 173,
        (byte) 45,
        (byte) 124,
        (byte) 6,
        (byte) 111,
        (byte) 141
      };
      byte[] numArray5 = new byte[55]
      {
        (byte) 128 /*0x80*/,
        (byte) 219,
        (byte) 119,
        (byte) 196,
        (byte) 252,
        (byte) 55,
        (byte) 180,
        (byte) 25,
        (byte) 92,
        (byte) 9,
        (byte) 173,
        (byte) 145,
        (byte) 141,
        (byte) 109,
        (byte) 131,
        (byte) 131,
        (byte) 33,
        (byte) 192 /*0xC0*/,
        (byte) 36,
        (byte) 245,
        (byte) 240 /*0xF0*/,
        (byte) 210,
        (byte) 251,
        (byte) 184,
        (byte) 148,
        (byte) 204,
        (byte) 6,
        (byte) 137,
        (byte) 73,
        (byte) 220,
        (byte) 238,
        (byte) 14,
        (byte) 81,
        (byte) 32 /*0x20*/,
        (byte) 46,
        (byte) 11,
        (byte) 185,
        (byte) 13,
        (byte) 49,
        (byte) 116,
        (byte) 139,
        (byte) 33,
        (byte) 133,
        (byte) 203,
        (byte) 88,
        (byte) 13,
        (byte) 131,
        (byte) 32 /*0x20*/,
        (byte) 47,
        (byte) 88,
        (byte) 152,
        (byte) 72,
        (byte) 79,
        (byte) 253,
        (byte) 24
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[30]
      {
        (byte) 76,
        (byte) 107,
        (byte) 42,
        (byte) 76,
        (byte) 44,
        (byte) 214,
        (byte) 227,
        (byte) 240 /*0xF0*/,
        (byte) 237,
        (byte) 114,
        (byte) 21,
        (byte) 133,
        (byte) 78,
        (byte) 125,
        (byte) 144 /*0x90*/,
        (byte) 248,
        (byte) 226,
        (byte) 121,
        (byte) 150,
        (byte) 205,
        (byte) 44,
        (byte) 20,
        (byte) 197,
        (byte) 67,
        (byte) 230,
        (byte) 230,
        (byte) 9,
        (byte) 149,
        (byte) 162,
        (byte) 199
      };
      byte[] numArray7 = new byte[30]
      {
        (byte) 112 /*0x70*/,
        (byte) 67,
        (byte) 248,
        (byte) 35,
        (byte) 44,
        (byte) 227,
        (byte) 125,
        (byte) 209,
        (byte) 235,
        (byte) 109,
        (byte) 118,
        (byte) 30,
        (byte) 169,
        (byte) 206,
        (byte) 161,
        (byte) 170,
        (byte) 133,
        (byte) 187,
        (byte) 126,
        (byte) 206,
        (byte) 230,
        (byte) 116,
        (byte) 40,
        (byte) 160 /*0xA0*/,
        (byte) 145,
        (byte) 203,
        (byte) 234,
        (byte) 167,
        (byte) 196,
        (byte) 125
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 30);
      for (int index = 0; index < 30; ++index)
        numArray1[index + 110] ^= numArray7[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray8 = new byte[140];
    byte[] numArray9 = new byte[55]
    {
      (byte) 184,
      (byte) 0,
      (byte) 225,
      (byte) 74,
      (byte) 205,
      (byte) 151,
      (byte) 59,
      (byte) 16 /*0x10*/,
      (byte) 14,
      (byte) 215,
      (byte) 225,
      (byte) 167,
      (byte) 11,
      (byte) 86,
      (byte) 47,
      (byte) 156,
      (byte) 132,
      (byte) 35,
      (byte) 246,
      (byte) 246,
      (byte) 96 /*0x60*/,
      (byte) 57,
      (byte) 56,
      (byte) 166,
      (byte) 231,
      (byte) 102,
      (byte) 70,
      (byte) 205,
      (byte) 77,
      (byte) 78,
      (byte) 38,
      (byte) 248,
      (byte) 11,
      (byte) 99,
      (byte) 149,
      (byte) 124,
      (byte) 175,
      (byte) 173,
      (byte) 20,
      (byte) 225,
      (byte) 91,
      (byte) 188,
      (byte) 30,
      (byte) 61,
      (byte) 227,
      (byte) 155,
      (byte) 218,
      (byte) 143,
      (byte) 218,
      (byte) 248,
      (byte) 29,
      (byte) 163,
      (byte) 132,
      (byte) 18,
      (byte) 193
    };
    byte[] numArray10 = new byte[55];
    numArray10[29] = (byte) 25;
    numArray10[1] = (byte) 54;
    numArray10[5] = (byte) 21;
    numArray10[53] = (byte) 93;
    numArray10[4] = (byte) 210;
    numArray10[22] = (byte) 63 /*0x3F*/;
    numArray10[8] = (byte) 173;
    numArray10[30] = (byte) 232;
    numArray10[20] = (byte) 51;
    numArray10[45] = (byte) 46;
    numArray10[10] = (byte) 140;
    numArray10[11] = (byte) 56;
    numArray10[35] = (byte) 12;
    numArray10[13] = (byte) 44;
    numArray10[14] = (byte) 70;
    numArray10[15] = (byte) 232;
    numArray10[16 /*0x10*/] = (byte) 254;
    numArray10[47] = (byte) 149;
    numArray10[51] = (byte) 14;
    numArray10[38] = (byte) 125;
    numArray10[36] = (byte) 155;
    numArray10[21] = (byte) 149;
    numArray10[44] = (byte) 201;
    numArray10[17] = (byte) 120;
    numArray10[3] = (byte) 58;
    numArray10[25] = (byte) 227;
    numArray10[26] = (byte) 100;
    numArray10[27] = (byte) 170;
    numArray10[28] = (byte) 214;
    numArray10[46] = byte.MaxValue;
    numArray10[50] = (byte) 52;
    numArray10[31 /*0x1F*/] = (byte) 126;
    numArray10[9] = (byte) 223;
    numArray10[0] = (byte) 85;
    numArray10[34] = (byte) 44;
    numArray10[7] = (byte) 242;
    numArray10[33] = (byte) 158;
    numArray10[37] = (byte) 163;
    numArray10[39] = (byte) 187;
    numArray10[23] = (byte) 155;
    numArray10[19] = (byte) 225;
    numArray10[41] = (byte) 157;
    numArray10[42] = (byte) 176 /*0xB0*/;
    numArray10[43] = (byte) 174;
    numArray10[12] = (byte) 28;
    numArray10[54] = (byte) 33;
    numArray10[24] = (byte) 33;
    numArray10[6] = (byte) 152;
    numArray10[48 /*0x30*/] = (byte) 118;
    numArray10[49] = (byte) 110;
    numArray10[52] = (byte) 44;
    numArray10[18] = (byte) 235;
    numArray10[32 /*0x20*/] = (byte) 180;
    numArray10[2] = (byte) 167;
    numArray10[40] = (byte) 246;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray8, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index] ^= numArray10[index];
    byte[] numArray11 = new byte[55]
    {
      (byte) 119,
      (byte) 24,
      (byte) 219,
      (byte) 154,
      (byte) 122,
      (byte) 109,
      (byte) 143,
      (byte) 46,
      (byte) 117,
      (byte) 162,
      (byte) 5,
      (byte) 169,
      byte.MaxValue,
      (byte) 119,
      (byte) 141,
      (byte) 97,
      (byte) 131,
      (byte) 172,
      (byte) 168,
      (byte) 190,
      (byte) 109,
      (byte) 201,
      (byte) 75,
      (byte) 89,
      (byte) 231,
      (byte) 204,
      (byte) 73,
      (byte) 235,
      (byte) 238,
      (byte) 4,
      (byte) 129,
      (byte) 131,
      (byte) 180,
      (byte) 229,
      (byte) 253,
      (byte) 49,
      (byte) 210,
      (byte) 246,
      (byte) 115,
      (byte) 141,
      (byte) 206,
      (byte) 94,
      (byte) 132,
      (byte) 93,
      (byte) 45,
      (byte) 36,
      (byte) 39,
      (byte) 147,
      (byte) 184,
      (byte) 50,
      (byte) 236,
      (byte) 203,
      (byte) 117,
      (byte) 164,
      (byte) 96 /*0x60*/
    };
    byte[] numArray12 = new byte[55]
    {
      (byte) 179,
      (byte) 47,
      (byte) 37,
      (byte) 6,
      (byte) 231,
      (byte) 30,
      (byte) 38,
      (byte) 250,
      (byte) 166,
      (byte) 120,
      (byte) 172,
      (byte) 125,
      (byte) 238,
      (byte) 246,
      (byte) 140,
      (byte) 55,
      (byte) 48 /*0x30*/,
      (byte) 231,
      (byte) 103,
      (byte) 227,
      (byte) 174,
      (byte) 5,
      (byte) 225,
      (byte) 71,
      (byte) 162,
      (byte) 181,
      (byte) 154,
      (byte) 226,
      (byte) 142,
      (byte) 38,
      (byte) 13,
      (byte) 170,
      (byte) 166,
      (byte) 10,
      (byte) 62,
      (byte) 237,
      (byte) 7,
      (byte) 148,
      (byte) 120,
      (byte) 248,
      (byte) 146,
      (byte) 82,
      (byte) 105,
      (byte) 23,
      (byte) 168,
      (byte) 57,
      (byte) 100,
      (byte) 62,
      (byte) 241,
      (byte) 97,
      (byte) 80 /*0x50*/,
      (byte) 23,
      (byte) 195,
      (byte) 224 /*0xE0*/,
      (byte) 253
    };
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray8, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index + 55] ^= numArray12[index];
    byte[] numArray13 = new byte[30]
    {
      (byte) 130,
      (byte) 179,
      (byte) 174,
      (byte) 36,
      (byte) 164,
      (byte) 63 /*0x3F*/,
      (byte) 112 /*0x70*/,
      (byte) 32 /*0x20*/,
      (byte) 129,
      (byte) 245,
      (byte) 26,
      (byte) 122,
      (byte) 92,
      (byte) 143,
      (byte) 140,
      (byte) 57,
      (byte) 27,
      (byte) 239,
      (byte) 15,
      (byte) 68,
      (byte) 237,
      (byte) 44,
      (byte) 194,
      (byte) 164,
      (byte) 181,
      (byte) 50,
      (byte) 188,
      (byte) 23,
      (byte) 182,
      (byte) 14
    };
    byte[] numArray14 = new byte[30]
    {
      (byte) 57,
      (byte) 165,
      (byte) 242,
      (byte) 28,
      (byte) 249,
      (byte) 59,
      (byte) 143,
      (byte) 233,
      (byte) 49,
      (byte) 183,
      (byte) 174,
      (byte) 164,
      (byte) 83,
      (byte) 199,
      (byte) 166,
      (byte) 27,
      (byte) 141,
      (byte) 222,
      (byte) 131,
      (byte) 85,
      (byte) 135,
      (byte) 192 /*0xC0*/,
      (byte) 69,
      (byte) 4,
      (byte) 212,
      (byte) 247,
      (byte) 66,
      (byte) 213,
      (byte) 128 /*0x80*/,
      (byte) 219
    };
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray8, 110, 30);
    for (int index = 0; index < 30; ++index)
      numArray8[index + 110] ^= numArray14[index];
    return Encoding.UTF8.GetString(numArray8);
  }

  internal static string ssp_appserver_12804()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[32 /*0x20*/];
      byte[] numArray2 = new byte[32 /*0x20*/];
      numArray2[30] = (byte) 230;
      numArray2[10] = (byte) 13;
      numArray2[8] = (byte) 171;
      numArray2[21] = (byte) 86;
      numArray2[4] = (byte) 41;
      numArray2[26] = (byte) 131;
      numArray2[31 /*0x1F*/] = (byte) 149;
      numArray2[6] = (byte) 20;
      numArray2[20] = (byte) 224 /*0xE0*/;
      numArray2[9] = (byte) 47;
      numArray2[0] = (byte) 80 /*0x50*/;
      numArray2[2] = (byte) 196;
      numArray2[12] = (byte) 92;
      numArray2[13] = (byte) 163;
      numArray2[14] = (byte) 96 /*0x60*/;
      numArray2[5] = (byte) 30;
      numArray2[1] = (byte) 67;
      numArray2[17] = (byte) 180;
      numArray2[18] = (byte) 81;
      numArray2[19] = (byte) 106;
      numArray2[16 /*0x10*/] = (byte) 90;
      numArray2[11] = (byte) 25;
      numArray2[22] = (byte) 240 /*0xF0*/;
      numArray2[23] = (byte) 10;
      numArray2[24] = (byte) 111;
      numArray2[25] = (byte) 113;
      numArray2[27] = (byte) 89;
      numArray2[29] = (byte) 115;
      numArray2[28] = (byte) 106;
      numArray2[15] = (byte) 145;
      numArray2[3] = (byte) 104;
      numArray2[7] = (byte) 61;
      byte[] numArray3 = new byte[32 /*0x20*/]
      {
        (byte) 242,
        (byte) 2,
        (byte) 220,
        (byte) 247,
        (byte) 63 /*0x3F*/,
        (byte) 232,
        byte.MaxValue,
        (byte) 161,
        (byte) 81,
        (byte) 141,
        (byte) 199,
        (byte) 78,
        (byte) 22,
        (byte) 58,
        (byte) 21,
        (byte) 202,
        (byte) 184,
        (byte) 170,
        (byte) 49,
        (byte) 230,
        (byte) 76,
        (byte) 239,
        (byte) 7,
        (byte) 25,
        (byte) 38,
        (byte) 92,
        (byte) 108,
        (byte) 12,
        (byte) 32 /*0x20*/,
        (byte) 99,
        (byte) 19,
        (byte) 56
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 32 /*0x20*/);
      for (int index = 0; index < 32 /*0x20*/; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[54];
      byte[] response = new byte[54];
      Array.Copy((Array) sc_12780.sspq, 367, (Array) numArray4, 0, 54);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_12780.sspr, 367, (Array) numArray4, 0, 54);
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
    byte[] numArray5 = new byte[32 /*0x20*/];
    byte[] numArray6 = new byte[32 /*0x20*/];
    numArray6[15] = (byte) 2;
    numArray6[1] = (byte) 30;
    numArray6[2] = (byte) 31 /*0x1F*/;
    numArray6[12] = (byte) 38;
    numArray6[4] = (byte) 238;
    numArray6[9] = (byte) 139;
    numArray6[19] = (byte) 144 /*0x90*/;
    numArray6[7] = (byte) 196;
    numArray6[13] = (byte) 41;
    numArray6[26] = (byte) 241;
    numArray6[31 /*0x1F*/] = (byte) 141;
    numArray6[11] = (byte) 83;
    numArray6[18] = (byte) 31 /*0x1F*/;
    numArray6[16 /*0x10*/] = (byte) 60;
    numArray6[14] = (byte) 180;
    numArray6[6] = (byte) 12;
    numArray6[21] = (byte) 32 /*0x20*/;
    numArray6[30] = (byte) 210;
    numArray6[22] = (byte) 32 /*0x20*/;
    numArray6[0] = (byte) 239;
    numArray6[10] = (byte) 17;
    numArray6[8] = (byte) 82;
    numArray6[24] = (byte) 115;
    numArray6[20] = (byte) 154;
    numArray6[3] = (byte) 97;
    numArray6[25] = (byte) 146;
    numArray6[23] = byte.MaxValue;
    numArray6[27] = (byte) 34;
    numArray6[28] = (byte) 93;
    numArray6[29] = (byte) 80 /*0x50*/;
    numArray6[5] = (byte) 224 /*0xE0*/;
    numArray6[17] = (byte) 179;
    byte[] numArray7 = new byte[32 /*0x20*/]
    {
      (byte) 243,
      (byte) 10,
      (byte) 62,
      (byte) 102,
      (byte) 69,
      (byte) 36,
      (byte) 36,
      (byte) 211,
      (byte) 73,
      (byte) 204,
      (byte) 147,
      (byte) 221,
      (byte) 182,
      (byte) 160 /*0xA0*/,
      (byte) 247,
      (byte) 242,
      (byte) 237,
      (byte) 99,
      (byte) 26,
      (byte) 211,
      (byte) 81,
      (byte) 170,
      (byte) 95,
      (byte) 157,
      (byte) 193,
      (byte) 13,
      (byte) 137,
      (byte) 180,
      (byte) 125,
      (byte) 180,
      (byte) 152,
      (byte) 185
    };
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 32 /*0x20*/);
    for (int index = 0; index < 32 /*0x20*/; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_appserver_12805()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[163];
      byte[] numArray2 = new byte[55]
      {
        (byte) 166,
        (byte) 29,
        (byte) 129,
        (byte) 148,
        (byte) 149,
        (byte) 147,
        (byte) 201,
        (byte) 180,
        (byte) 31 /*0x1F*/,
        (byte) 134,
        (byte) 26,
        (byte) 127 /*0x7F*/,
        (byte) 147,
        (byte) 253,
        (byte) 164,
        (byte) 72,
        byte.MaxValue,
        (byte) 15,
        (byte) 238,
        (byte) 134,
        (byte) 245,
        (byte) 180,
        (byte) 152,
        (byte) 96 /*0x60*/,
        (byte) 186,
        (byte) 193,
        (byte) 155,
        (byte) 221,
        (byte) 127 /*0x7F*/,
        byte.MaxValue,
        (byte) 225,
        (byte) 96 /*0x60*/,
        (byte) 171,
        (byte) 220,
        (byte) 77,
        (byte) 96 /*0x60*/,
        (byte) 148,
        (byte) 188,
        (byte) 45,
        (byte) 10,
        (byte) 187,
        (byte) 89,
        (byte) 174,
        (byte) 120,
        (byte) 5,
        (byte) 109,
        (byte) 200,
        (byte) 242,
        (byte) 68,
        (byte) 30,
        (byte) 2,
        (byte) 169,
        (byte) 149,
        (byte) 194,
        (byte) 45
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 164,
        (byte) 83,
        (byte) 238,
        (byte) 130,
        (byte) 105,
        (byte) 165,
        (byte) 193,
        (byte) 159,
        (byte) 196,
        (byte) 21,
        (byte) 168,
        (byte) 214,
        (byte) 201,
        (byte) 36,
        (byte) 251,
        (byte) 17,
        (byte) 218,
        (byte) 226,
        (byte) 203,
        (byte) 249,
        (byte) 78,
        (byte) 162,
        (byte) 83,
        (byte) 202,
        (byte) 46,
        (byte) 113,
        (byte) 234,
        (byte) 143,
        (byte) 146,
        (byte) 29,
        (byte) 62,
        (byte) 98,
        (byte) 217,
        (byte) 225,
        (byte) 251,
        (byte) 230,
        (byte) 107,
        (byte) 193,
        (byte) 20,
        (byte) 180,
        (byte) 136,
        (byte) 95,
        (byte) 133,
        (byte) 85,
        (byte) 3,
        (byte) 200,
        (byte) 202,
        (byte) 173,
        (byte) 182,
        (byte) 183,
        (byte) 152,
        (byte) 116,
        (byte) 22,
        (byte) 105,
        (byte) 85
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 32 /*0x20*/,
        (byte) 218,
        (byte) 212,
        (byte) 248,
        (byte) 236,
        (byte) 52,
        (byte) 32 /*0x20*/,
        (byte) 165,
        (byte) 185,
        (byte) 212,
        (byte) 183,
        (byte) 175,
        (byte) 68,
        (byte) 180,
        (byte) 66,
        (byte) 225,
        (byte) 227,
        (byte) 203,
        (byte) 155,
        (byte) 6,
        (byte) 32 /*0x20*/,
        (byte) 91,
        (byte) 76,
        (byte) 248,
        (byte) 169,
        (byte) 152,
        (byte) 97,
        (byte) 252,
        (byte) 55,
        (byte) 202,
        (byte) 233,
        (byte) 174,
        (byte) 92,
        (byte) 218,
        (byte) 115,
        (byte) 226,
        (byte) 89,
        (byte) 18,
        (byte) 206,
        (byte) 37,
        (byte) 141,
        (byte) 210,
        (byte) 75,
        (byte) 153,
        (byte) 17,
        (byte) 66,
        byte.MaxValue,
        (byte) 105,
        (byte) 192 /*0xC0*/,
        (byte) 199,
        (byte) 178,
        (byte) 247,
        (byte) 146,
        (byte) 235,
        (byte) 101
      };
      byte[] numArray5 = new byte[55]
      {
        (byte) 122,
        (byte) 224 /*0xE0*/,
        (byte) 4,
        (byte) 156,
        (byte) 131,
        (byte) 185,
        (byte) 209,
        (byte) 146,
        (byte) 17,
        (byte) 214,
        (byte) 62,
        (byte) 247,
        (byte) 29,
        (byte) 73,
        (byte) 211,
        (byte) 75,
        (byte) 59,
        (byte) 141,
        (byte) 58,
        (byte) 34,
        (byte) 21,
        (byte) 138,
        (byte) 52,
        (byte) 197,
        (byte) 25,
        (byte) 237,
        (byte) 130,
        (byte) 190,
        (byte) 157,
        (byte) 59,
        (byte) 92,
        (byte) 186,
        (byte) 31 /*0x1F*/,
        (byte) 109,
        (byte) 200,
        (byte) 14,
        (byte) 21,
        (byte) 43,
        (byte) 66,
        (byte) 252,
        (byte) 89,
        (byte) 93,
        (byte) 208 /*0xD0*/,
        (byte) 52,
        (byte) 73,
        (byte) 231,
        (byte) 145,
        (byte) 175,
        (byte) 21,
        (byte) 244,
        (byte) 132,
        (byte) 5,
        (byte) 54,
        (byte) 31 /*0x1F*/,
        (byte) 198
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[53]
      {
        (byte) 219,
        (byte) 79,
        (byte) 83,
        (byte) 210,
        (byte) 36,
        (byte) 109,
        (byte) 238,
        (byte) 235,
        (byte) 8,
        (byte) 83,
        (byte) 106,
        (byte) 65,
        (byte) 96 /*0x60*/,
        (byte) 135,
        (byte) 208 /*0xD0*/,
        (byte) 252,
        (byte) 118,
        (byte) 120,
        (byte) 77,
        (byte) 16 /*0x10*/,
        (byte) 179,
        (byte) 231,
        (byte) 102,
        (byte) 153,
        (byte) 248,
        (byte) 59,
        (byte) 112 /*0x70*/,
        (byte) 24,
        (byte) 242,
        (byte) 28,
        (byte) 112 /*0x70*/,
        (byte) 117,
        (byte) 173,
        (byte) 84,
        (byte) 126,
        (byte) 195,
        (byte) 122,
        (byte) 82,
        (byte) 105,
        (byte) 101,
        (byte) 92,
        (byte) 174,
        (byte) 8,
        (byte) 238,
        (byte) 149,
        (byte) 40,
        (byte) 95,
        (byte) 63 /*0x3F*/,
        (byte) 225,
        (byte) 250,
        (byte) 27,
        (byte) 115,
        (byte) 23
      };
      byte[] numArray7 = new byte[53]
      {
        (byte) 176 /*0xB0*/,
        (byte) 68,
        (byte) 60,
        (byte) 72,
        (byte) 179,
        (byte) 87,
        (byte) 102,
        (byte) 58,
        (byte) 103,
        (byte) 170,
        (byte) 191,
        (byte) 112 /*0x70*/,
        (byte) 161,
        (byte) 81,
        (byte) 234,
        (byte) 172,
        (byte) 28,
        (byte) 35,
        (byte) 248,
        (byte) 248,
        (byte) 67,
        (byte) 162,
        (byte) 235,
        (byte) 135,
        (byte) 10,
        (byte) 229,
        (byte) 145,
        (byte) 249,
        (byte) 43,
        (byte) 117,
        (byte) 107,
        (byte) 42,
        (byte) 244,
        (byte) 109,
        (byte) 179,
        (byte) 86,
        (byte) 235,
        (byte) 126,
        (byte) 195,
        (byte) 154,
        (byte) 3,
        (byte) 152,
        (byte) 166,
        (byte) 205,
        (byte) 96 /*0x60*/,
        (byte) 123,
        (byte) 81,
        (byte) 175,
        (byte) 37,
        (byte) 220,
        (byte) 201,
        (byte) 12,
        (byte) 146
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 53);
      for (int index = 0; index < 53; ++index)
        numArray1[index + 110] ^= numArray7[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray8 = new byte[163];
    byte[] numArray9 = new byte[55];
    numArray9[37] = (byte) 128 /*0x80*/;
    numArray9[2] = (byte) 218;
    numArray9[27] = (byte) 247;
    numArray9[40] = (byte) 181;
    numArray9[4] = (byte) 253;
    numArray9[5] = (byte) 214;
    numArray9[6] = (byte) 248;
    numArray9[18] = (byte) 179;
    numArray9[31 /*0x1F*/] = (byte) 248;
    numArray9[47] = (byte) 212;
    numArray9[10] = (byte) 147;
    numArray9[34] = (byte) 219;
    numArray9[41] = (byte) 28;
    numArray9[13] = (byte) 13;
    numArray9[14] = (byte) 129;
    numArray9[15] = (byte) 36;
    numArray9[16 /*0x10*/] = (byte) 14;
    numArray9[1] = (byte) 186;
    numArray9[49] = (byte) 247;
    numArray9[19] = (byte) 5;
    numArray9[7] = (byte) 236;
    numArray9[11] = (byte) 12;
    numArray9[22] = (byte) 3;
    numArray9[23] = (byte) 207;
    numArray9[24] = (byte) 220;
    numArray9[25] = (byte) 48 /*0x30*/;
    numArray9[32 /*0x20*/] = (byte) 122;
    numArray9[30] = (byte) 93;
    numArray9[3] = (byte) 232;
    numArray9[42] = (byte) 11;
    numArray9[20] = (byte) 222;
    numArray9[17] = (byte) 232;
    numArray9[0] = (byte) 232;
    numArray9[12] = (byte) 9;
    numArray9[39] = (byte) 134;
    numArray9[50] = (byte) 100;
    numArray9[36] = (byte) 119;
    numArray9[21] = (byte) 120;
    numArray9[26] = (byte) 55;
    numArray9[35] = (byte) 192 /*0xC0*/;
    numArray9[8] = (byte) 227;
    numArray9[43] = (byte) 161;
    numArray9[38] = (byte) 104;
    numArray9[28] = (byte) 0;
    numArray9[44] = (byte) 149;
    numArray9[9] = (byte) 244;
    numArray9[46] = (byte) 54;
    numArray9[54] = (byte) 21;
    numArray9[48 /*0x30*/] = (byte) 120;
    numArray9[45] = (byte) 82;
    numArray9[29] = (byte) 253;
    numArray9[51] = (byte) 47;
    numArray9[52] = (byte) 1;
    numArray9[53] = (byte) 212;
    numArray9[33] = (byte) 138;
    byte[] numArray10 = new byte[55];
    numArray10[38] = (byte) 230;
    numArray10[19] = (byte) 40;
    numArray10[6] = (byte) 24;
    numArray10[12] = (byte) 38;
    numArray10[4] = (byte) 67;
    numArray10[36] = (byte) 237;
    numArray10[47] = (byte) 202;
    numArray10[9] = (byte) 101;
    numArray10[8] = (byte) 191;
    numArray10[0] = (byte) 20;
    numArray10[3] = (byte) 88;
    numArray10[46] = (byte) 234;
    numArray10[27] = (byte) 184;
    numArray10[42] = (byte) 34;
    numArray10[14] = (byte) 123;
    numArray10[26] = (byte) 179;
    numArray10[23] = (byte) 117;
    numArray10[21] = (byte) 143;
    numArray10[18] = (byte) 152;
    numArray10[31 /*0x1F*/] = (byte) 116;
    numArray10[43] = (byte) 158;
    numArray10[44] = (byte) 248;
    numArray10[13] = (byte) 101;
    numArray10[11] = (byte) 139;
    numArray10[24] = (byte) 9;
    numArray10[2] = (byte) 244;
    numArray10[50] = (byte) 36;
    numArray10[17] = (byte) 72;
    numArray10[28] = (byte) 167;
    numArray10[29] = (byte) 209;
    numArray10[10] = (byte) 66;
    numArray10[7] = (byte) 137;
    numArray10[32 /*0x20*/] = (byte) 4;
    numArray10[33] = (byte) 27;
    numArray10[34] = (byte) 157;
    numArray10[39] = (byte) 142;
    numArray10[40] = (byte) 168;
    numArray10[37] = (byte) 190;
    numArray10[30] = (byte) 216;
    numArray10[35] = (byte) 146;
    numArray10[5] = (byte) 188;
    numArray10[41] = (byte) 245;
    numArray10[49] = (byte) 216;
    numArray10[15] = (byte) 64 /*0x40*/;
    numArray10[51] = (byte) 41;
    numArray10[45] = (byte) 127 /*0x7F*/;
    numArray10[16 /*0x10*/] = (byte) 131;
    numArray10[22] = (byte) 220;
    numArray10[48 /*0x30*/] = (byte) 251;
    numArray10[1] = (byte) 192 /*0xC0*/;
    numArray10[20] = (byte) 77;
    numArray10[25] = (byte) 143;
    numArray10[52] = (byte) 203;
    numArray10[53] = (byte) 47;
    numArray10[54] = (byte) 117;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray8, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index] ^= numArray10[index];
    byte[] numArray11 = new byte[55];
    numArray11[28] = (byte) 181;
    numArray11[1] = (byte) 185;
    numArray11[16 /*0x10*/] = (byte) 155;
    numArray11[3] = (byte) 123;
    numArray11[30] = (byte) 54;
    numArray11[5] = (byte) 35;
    numArray11[0] = (byte) 145;
    numArray11[7] = (byte) 232;
    numArray11[32 /*0x20*/] = (byte) 174;
    numArray11[9] = (byte) 204;
    numArray11[10] = (byte) 232;
    numArray11[11] = (byte) 93;
    numArray11[12] = (byte) 1;
    numArray11[13] = (byte) 165;
    numArray11[20] = (byte) 181;
    numArray11[53] = (byte) 72;
    numArray11[29] = (byte) 150;
    numArray11[38] = (byte) 119;
    numArray11[18] = (byte) 68;
    numArray11[19] = (byte) 113;
    numArray11[51] = (byte) 128 /*0x80*/;
    numArray11[43] = (byte) 8;
    numArray11[40] = (byte) 74;
    numArray11[2] = (byte) 145;
    numArray11[41] = (byte) 186;
    numArray11[47] = (byte) 100;
    numArray11[45] = (byte) 126;
    numArray11[25] = (byte) 199;
    numArray11[14] = (byte) 38;
    numArray11[48 /*0x30*/] = (byte) 150;
    numArray11[39] = (byte) 42;
    numArray11[31 /*0x1F*/] = (byte) 96 /*0x60*/;
    numArray11[15] = (byte) 29;
    numArray11[17] = (byte) 105;
    numArray11[22] = (byte) 244;
    numArray11[35] = (byte) 56;
    numArray11[33] = (byte) 186;
    numArray11[37] = (byte) 48 /*0x30*/;
    numArray11[8] = (byte) 128 /*0x80*/;
    numArray11[36] = (byte) 54;
    numArray11[26] = (byte) 180;
    numArray11[24] = (byte) 228;
    numArray11[42] = (byte) 214;
    numArray11[27] = (byte) 4;
    numArray11[44] = (byte) 197;
    numArray11[49] = (byte) 173;
    numArray11[23] = (byte) 147;
    numArray11[6] = (byte) 140;
    numArray11[50] = (byte) 241;
    numArray11[46] = byte.MaxValue;
    numArray11[21] = (byte) 232;
    numArray11[4] = (byte) 186;
    numArray11[52] = (byte) 124;
    numArray11[34] = (byte) 9;
    numArray11[54] = (byte) 69;
    byte[] numArray12 = new byte[55]
    {
      (byte) 175,
      (byte) 139,
      (byte) 69,
      (byte) 249,
      (byte) 22,
      (byte) 191,
      (byte) 123,
      (byte) 208 /*0xD0*/,
      (byte) 23,
      (byte) 0,
      (byte) 79,
      (byte) 91,
      (byte) 10,
      (byte) 1,
      (byte) 113,
      (byte) 224 /*0xE0*/,
      (byte) 180,
      (byte) 72,
      (byte) 77,
      (byte) 9,
      (byte) 237,
      (byte) 131,
      (byte) 39,
      (byte) 22,
      (byte) 38,
      (byte) 45,
      (byte) 186,
      (byte) 226,
      (byte) 77,
      (byte) 82,
      (byte) 207,
      (byte) 235,
      (byte) 68,
      (byte) 48 /*0x30*/,
      (byte) 2,
      (byte) 71,
      (byte) 177,
      (byte) 38,
      (byte) 138,
      (byte) 71,
      (byte) 148,
      (byte) 242,
      (byte) 178,
      (byte) 96 /*0x60*/,
      (byte) 52,
      (byte) 60,
      (byte) 232,
      (byte) 188,
      (byte) 76,
      (byte) 119,
      (byte) 192 /*0xC0*/,
      (byte) 86,
      (byte) 76,
      (byte) 158,
      (byte) 133
    };
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray8, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index + 55] ^= numArray12[index];
    byte[] numArray13 = new byte[53];
    numArray13[9] = (byte) 72;
    numArray13[18] = (byte) 116;
    numArray13[34] = (byte) 7;
    numArray13[36] = (byte) 87;
    numArray13[23] = (byte) 15;
    numArray13[5] = (byte) 43;
    numArray13[6] = (byte) 68;
    numArray13[51] = (byte) 187;
    numArray13[8] = (byte) 164;
    numArray13[47] = (byte) 23;
    numArray13[50] = (byte) 3;
    numArray13[11] = (byte) 59;
    numArray13[7] = (byte) 223;
    numArray13[13] = (byte) 174;
    numArray13[12] = (byte) 122;
    numArray13[38] = (byte) 166;
    numArray13[26] = (byte) 65;
    numArray13[17] = (byte) 101;
    numArray13[32 /*0x20*/] = (byte) 201;
    numArray13[30] = (byte) 69;
    numArray13[20] = (byte) 120;
    numArray13[1] = (byte) 48 /*0x30*/;
    numArray13[15] = (byte) 110;
    numArray13[35] = (byte) 105;
    numArray13[4] = (byte) 26;
    numArray13[27] = (byte) 31 /*0x1F*/;
    numArray13[14] = (byte) 209;
    numArray13[28] = (byte) 39;
    numArray13[31 /*0x1F*/] = (byte) 42;
    numArray13[33] = (byte) 153;
    numArray13[21] = (byte) 144 /*0x90*/;
    numArray13[48 /*0x30*/] = (byte) 179;
    numArray13[49] = (byte) 32 /*0x20*/;
    numArray13[10] = (byte) 210;
    numArray13[16 /*0x10*/] = (byte) 94;
    numArray13[19] = (byte) 37;
    numArray13[0] = (byte) 48 /*0x30*/;
    numArray13[25] = (byte) 196;
    numArray13[43] = (byte) 135;
    numArray13[39] = (byte) 90;
    numArray13[40] = (byte) 168;
    numArray13[41] = (byte) 147;
    numArray13[42] = (byte) 153;
    numArray13[22] = (byte) 149;
    numArray13[44] = (byte) 230;
    numArray13[45] = (byte) 141;
    numArray13[46] = (byte) 46;
    numArray13[3] = (byte) 30;
    numArray13[24] = (byte) 134;
    numArray13[2] = (byte) 43;
    numArray13[29] = (byte) 129;
    numArray13[37] = (byte) 157;
    numArray13[52] = (byte) 213;
    byte[] numArray14 = new byte[53];
    numArray14[9] = (byte) 41;
    numArray14[13] = (byte) 200;
    numArray14[39] = (byte) 127 /*0x7F*/;
    numArray14[14] = (byte) 2;
    numArray14[4] = (byte) 184;
    numArray14[5] = (byte) 171;
    numArray14[43] = (byte) 175;
    numArray14[23] = (byte) 169;
    numArray14[37] = (byte) 236;
    numArray14[45] = (byte) 218;
    numArray14[10] = (byte) 181;
    numArray14[11] = (byte) 130;
    numArray14[51] = (byte) 219;
    numArray14[6] = (byte) 229;
    numArray14[30] = (byte) 0;
    numArray14[15] = (byte) 199;
    numArray14[0] = (byte) 136;
    numArray14[22] = (byte) 79;
    numArray14[18] = (byte) 159;
    numArray14[19] = (byte) 52;
    numArray14[20] = (byte) 150;
    numArray14[8] = (byte) 161;
    numArray14[25] = (byte) 193;
    numArray14[17] = (byte) 5;
    numArray14[24] = (byte) 38;
    numArray14[2] = (byte) 232;
    numArray14[26] = (byte) 69;
    numArray14[27] = (byte) 3;
    numArray14[46] = (byte) 84;
    numArray14[29] = (byte) 141;
    numArray14[52] = (byte) 248;
    numArray14[16 /*0x10*/] = (byte) 128 /*0x80*/;
    numArray14[32 /*0x20*/] = (byte) 61;
    numArray14[12] = (byte) 102;
    numArray14[3] = (byte) 81;
    numArray14[35] = (byte) 84;
    numArray14[36] = (byte) 78;
    numArray14[40] = (byte) 149;
    numArray14[38] = (byte) 118;
    numArray14[49] = (byte) 185;
    numArray14[42] = (byte) 245;
    numArray14[41] = (byte) 91;
    numArray14[21] = (byte) 170;
    numArray14[33] = (byte) 24;
    numArray14[31 /*0x1F*/] = (byte) 128 /*0x80*/;
    numArray14[48 /*0x30*/] = (byte) 178;
    numArray14[1] = (byte) 174;
    numArray14[47] = (byte) 194;
    numArray14[34] = (byte) 180;
    numArray14[28] = (byte) 102;
    numArray14[50] = (byte) 21;
    numArray14[44] = (byte) 140;
    numArray14[7] = (byte) 2;
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray8, 110, 53);
    for (int index = 0; index < 53; ++index)
      numArray8[index + 110] ^= numArray14[index];
    return Encoding.UTF8.GetString(numArray8);
  }

  internal static string ssp_appserver_12806()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[32 /*0x20*/];
      byte[] numArray2 = new byte[32 /*0x20*/]
      {
        (byte) 249,
        (byte) 114,
        (byte) 40,
        (byte) 2,
        (byte) 7,
        (byte) 192 /*0xC0*/,
        (byte) 118,
        (byte) 6,
        (byte) 152,
        (byte) 179,
        (byte) 106,
        (byte) 235,
        (byte) 186,
        (byte) 178,
        (byte) 238,
        (byte) 129,
        (byte) 87,
        (byte) 145,
        (byte) 82,
        (byte) 221,
        (byte) 249,
        (byte) 47,
        (byte) 223,
        (byte) 173,
        (byte) 125,
        byte.MaxValue,
        (byte) 218,
        (byte) 166,
        (byte) 137,
        (byte) 110,
        (byte) 206,
        (byte) 177
      };
      byte[] numArray3 = new byte[32 /*0x20*/];
      numArray3[2] = (byte) 228;
      numArray3[1] = (byte) 17;
      numArray3[8] = (byte) 99;
      numArray3[29] = (byte) 100;
      numArray3[15] = (byte) 17;
      numArray3[16 /*0x10*/] = (byte) 240 /*0xF0*/;
      numArray3[6] = (byte) 66;
      numArray3[7] = (byte) 164;
      numArray3[9] = (byte) 208 /*0xD0*/;
      numArray3[24] = (byte) 170;
      numArray3[11] = (byte) 25;
      numArray3[0] = (byte) 21;
      numArray3[10] = (byte) 113;
      numArray3[18] = (byte) 189;
      numArray3[14] = (byte) 245;
      numArray3[12] = (byte) 184;
      numArray3[25] = (byte) 33;
      numArray3[5] = (byte) 8;
      numArray3[3] = (byte) 243;
      numArray3[19] = (byte) 237;
      numArray3[20] = (byte) 86;
      numArray3[21] = (byte) 125;
      numArray3[26] = (byte) 115;
      numArray3[23] = (byte) 78;
      numArray3[13] = (byte) 71;
      numArray3[4] = (byte) 41;
      numArray3[30] = (byte) 84;
      numArray3[27] = (byte) 230;
      numArray3[28] = (byte) 76;
      numArray3[22] = (byte) 92;
      numArray3[17] = (byte) 210;
      numArray3[31 /*0x1F*/] = (byte) 232;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 32 /*0x20*/);
      for (int index = 0; index < 32 /*0x20*/; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[32 /*0x20*/];
    byte[] numArray5 = new byte[32 /*0x20*/];
    numArray5[15] = (byte) 241;
    numArray5[1] = (byte) 70;
    numArray5[20] = (byte) 78;
    numArray5[3] = (byte) 235;
    numArray5[29] = (byte) 44;
    numArray5[4] = (byte) 24;
    numArray5[23] = (byte) 181;
    numArray5[6] = (byte) 170;
    numArray5[8] = (byte) 80 /*0x50*/;
    numArray5[11] = (byte) 69;
    numArray5[2] = (byte) 166;
    numArray5[13] = (byte) 207;
    numArray5[12] = (byte) 93;
    numArray5[27] = (byte) 192 /*0xC0*/;
    numArray5[21] = (byte) 109;
    numArray5[25] = (byte) 186;
    numArray5[0] = (byte) 51;
    numArray5[10] = (byte) 105;
    numArray5[18] = (byte) 49;
    numArray5[19] = (byte) 122;
    numArray5[9] = (byte) 253;
    numArray5[5] = (byte) 79;
    numArray5[22] = (byte) 161;
    numArray5[30] = (byte) 8;
    numArray5[24] = (byte) 26;
    numArray5[16 /*0x10*/] = (byte) 219;
    numArray5[26] = (byte) 57;
    numArray5[31 /*0x1F*/] = (byte) 146;
    numArray5[28] = (byte) 74;
    numArray5[17] = (byte) 21;
    numArray5[7] = (byte) 194;
    numArray5[14] = (byte) 137;
    byte[] numArray6 = new byte[32 /*0x20*/]
    {
      (byte) 223,
      (byte) 157,
      (byte) 147,
      (byte) 13,
      (byte) 106,
      (byte) 147,
      (byte) 26,
      (byte) 136,
      (byte) 6,
      (byte) 39,
      (byte) 9,
      (byte) 214,
      (byte) 40,
      (byte) 33,
      (byte) 235,
      (byte) 229,
      (byte) 183,
      (byte) 99,
      (byte) 200,
      (byte) 187,
      (byte) 73,
      (byte) 136,
      (byte) 228,
      (byte) 36,
      (byte) 67,
      (byte) 185,
      (byte) 150,
      (byte) 252,
      (byte) 207,
      (byte) 166,
      (byte) 152,
      (byte) 211
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 32 /*0x20*/);
    for (int index = 0; index < 32 /*0x20*/; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12807()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[125];
      byte[] numArray2 = new byte[55]
      {
        (byte) 210,
        (byte) 126,
        (byte) 227,
        (byte) 25,
        (byte) 212,
        (byte) 227,
        (byte) 238,
        (byte) 95,
        (byte) 165,
        (byte) 212,
        (byte) 140,
        (byte) 229,
        (byte) 146,
        (byte) 0,
        (byte) 99,
        (byte) 107,
        (byte) 139,
        (byte) 244,
        (byte) 222,
        (byte) 113,
        (byte) 150,
        (byte) 54,
        (byte) 102,
        (byte) 29,
        (byte) 241,
        (byte) 226,
        (byte) 55,
        (byte) 161,
        (byte) 117,
        (byte) 164,
        (byte) 216,
        (byte) 82,
        (byte) 160 /*0xA0*/,
        (byte) 254,
        (byte) 102,
        (byte) 222,
        (byte) 250,
        (byte) 135,
        (byte) 63 /*0x3F*/,
        (byte) 208 /*0xD0*/,
        (byte) 80 /*0x50*/,
        (byte) 62,
        (byte) 32 /*0x20*/,
        (byte) 207,
        (byte) 100,
        (byte) 235,
        (byte) 203,
        (byte) 22,
        (byte) 135,
        (byte) 12,
        (byte) 152,
        (byte) 0,
        (byte) 248,
        (byte) 36,
        (byte) 146
      };
      byte[] numArray3 = new byte[55];
      numArray3[28] = (byte) 238;
      numArray3[5] = (byte) 252;
      numArray3[2] = (byte) 131;
      numArray3[9] = (byte) 20;
      numArray3[4] = (byte) 105;
      numArray3[22] = (byte) 183;
      numArray3[36] = (byte) 171;
      numArray3[7] = (byte) 189;
      numArray3[3] = (byte) 36;
      numArray3[6] = (byte) 117;
      numArray3[15] = (byte) 151;
      numArray3[1] = (byte) 252;
      numArray3[12] = (byte) 239;
      numArray3[13] = (byte) 229;
      numArray3[14] = (byte) 230;
      numArray3[52] = (byte) 246;
      numArray3[16 /*0x10*/] = (byte) 119;
      numArray3[11] = (byte) 239;
      numArray3[0] = (byte) 111;
      numArray3[19] = (byte) 37;
      numArray3[31 /*0x1F*/] = (byte) 66;
      numArray3[46] = (byte) 238;
      numArray3[43] = (byte) 81;
      numArray3[23] = (byte) 215;
      numArray3[24] = (byte) 181;
      numArray3[41] = (byte) 84;
      numArray3[26] = (byte) 59;
      numArray3[21] = (byte) 42;
      numArray3[29] = (byte) 230;
      numArray3[40] = (byte) 54;
      numArray3[30] = (byte) 146;
      numArray3[10] = (byte) 8;
      numArray3[20] = (byte) 22;
      numArray3[51] = (byte) 172;
      numArray3[34] = (byte) 155;
      numArray3[35] = (byte) 113;
      numArray3[8] = (byte) 193;
      numArray3[37] = (byte) 114;
      numArray3[38] = (byte) 179;
      numArray3[17] = (byte) 228;
      numArray3[25] = (byte) 125;
      numArray3[32 /*0x20*/] = (byte) 136;
      numArray3[42] = (byte) 144 /*0x90*/;
      numArray3[47] = (byte) 100;
      numArray3[44] = (byte) 227;
      numArray3[45] = (byte) 12;
      numArray3[27] = (byte) 109;
      numArray3[39] = (byte) 129;
      numArray3[48 /*0x30*/] = (byte) 179;
      numArray3[49] = (byte) 62;
      numArray3[50] = (byte) 96 /*0x60*/;
      numArray3[33] = (byte) 251;
      numArray3[18] = (byte) 191;
      numArray3[53] = (byte) 158;
      numArray3[54] = (byte) 119;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 12,
        (byte) 35,
        (byte) 218,
        (byte) 139,
        (byte) 50,
        (byte) 129,
        (byte) 243,
        (byte) 35,
        (byte) 224 /*0xE0*/,
        (byte) 201,
        (byte) 39,
        (byte) 206,
        (byte) 190,
        (byte) 59,
        (byte) 164,
        (byte) 101,
        (byte) 234,
        (byte) 29,
        (byte) 4,
        (byte) 246,
        (byte) 193,
        (byte) 143,
        (byte) 94,
        (byte) 224 /*0xE0*/,
        (byte) 164,
        (byte) 23,
        (byte) 204,
        (byte) 119,
        (byte) 139,
        (byte) 111,
        (byte) 120,
        (byte) 50,
        (byte) 69,
        (byte) 67,
        (byte) 40,
        (byte) 59,
        (byte) 116,
        (byte) 152,
        (byte) 209,
        (byte) 209,
        (byte) 44,
        (byte) 176 /*0xB0*/,
        (byte) 73,
        (byte) 221,
        (byte) 15,
        (byte) 173,
        (byte) 68,
        (byte) 95,
        (byte) 240 /*0xF0*/,
        (byte) 230,
        byte.MaxValue,
        (byte) 229,
        (byte) 53,
        (byte) 143,
        (byte) 192 /*0xC0*/
      };
      byte[] numArray5 = new byte[55]
      {
        (byte) 46,
        (byte) 201,
        (byte) 146,
        (byte) 149,
        (byte) 248,
        (byte) 169,
        (byte) 210,
        (byte) 240 /*0xF0*/,
        (byte) 38,
        (byte) 244,
        (byte) 229,
        (byte) 127 /*0x7F*/,
        (byte) 230,
        (byte) 13,
        (byte) 175,
        (byte) 115,
        (byte) 89,
        (byte) 115,
        (byte) 138,
        (byte) 122,
        (byte) 92,
        (byte) 159,
        (byte) 253,
        (byte) 32 /*0x20*/,
        (byte) 124,
        (byte) 192 /*0xC0*/,
        (byte) 227,
        (byte) 166,
        (byte) 11,
        (byte) 126,
        (byte) 226,
        (byte) 66,
        (byte) 47,
        (byte) 224 /*0xE0*/,
        (byte) 210,
        (byte) 37,
        (byte) 233,
        (byte) 65,
        (byte) 142,
        (byte) 182,
        (byte) 252,
        (byte) 200,
        (byte) 137,
        (byte) 162,
        (byte) 59,
        (byte) 157,
        (byte) 167,
        (byte) 228,
        (byte) 99,
        (byte) 198,
        (byte) 223,
        (byte) 15,
        (byte) 22,
        (byte) 38,
        (byte) 144 /*0x90*/
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[15];
      numArray6[0] = (byte) 249;
      numArray6[7] = (byte) 80 /*0x50*/;
      numArray6[2] = (byte) 26;
      numArray6[12] = (byte) 104;
      numArray6[4] = (byte) 123;
      numArray6[5] = (byte) 151;
      numArray6[6] = (byte) 112 /*0x70*/;
      numArray6[10] = (byte) 122;
      numArray6[11] = (byte) 19;
      numArray6[1] = (byte) 231;
      numArray6[3] = (byte) 181;
      numArray6[8] = (byte) 245;
      numArray6[9] = (byte) 136;
      numArray6[14] = (byte) 81;
      numArray6[13] = (byte) 241;
      byte[] numArray7 = new byte[15]
      {
        (byte) 2,
        (byte) 44,
        (byte) 215,
        (byte) 72,
        (byte) 226,
        (byte) 177,
        (byte) 208 /*0xD0*/,
        (byte) 238,
        (byte) 155,
        (byte) 80 /*0x50*/,
        (byte) 157,
        (byte) 166,
        byte.MaxValue,
        (byte) 99,
        (byte) 235
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 15);
      for (int index = 0; index < 15; ++index)
        numArray1[index + 110] ^= numArray7[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray8 = new byte[125];
    byte[] numArray9 = new byte[55]
    {
      (byte) 118,
      (byte) 122,
      (byte) 177,
      (byte) 183,
      (byte) 28,
      (byte) 176 /*0xB0*/,
      (byte) 24,
      (byte) 216,
      (byte) 140,
      (byte) 26,
      (byte) 36,
      (byte) 201,
      (byte) 76,
      (byte) 135,
      (byte) 245,
      (byte) 174,
      (byte) 153,
      (byte) 2,
      (byte) 197,
      (byte) 175,
      (byte) 54,
      (byte) 68,
      (byte) 41,
      (byte) 240 /*0xF0*/,
      (byte) 71,
      (byte) 72,
      (byte) 243,
      (byte) 242,
      (byte) 0,
      (byte) 189,
      (byte) 248,
      (byte) 210,
      (byte) 247,
      (byte) 102,
      (byte) 241,
      (byte) 119,
      (byte) 160 /*0xA0*/,
      (byte) 148,
      (byte) 21,
      (byte) 24,
      (byte) 248,
      (byte) 109,
      (byte) 243,
      (byte) 182,
      (byte) 209,
      (byte) 9,
      (byte) 31 /*0x1F*/,
      (byte) 0,
      (byte) 175,
      (byte) 193,
      (byte) 249,
      (byte) 250,
      (byte) 241,
      (byte) 211,
      (byte) 118
    };
    byte[] numArray10 = new byte[55]
    {
      (byte) 198,
      (byte) 235,
      (byte) 148,
      (byte) 188,
      (byte) 53,
      (byte) 230,
      (byte) 156,
      (byte) 243,
      (byte) 50,
      (byte) 176 /*0xB0*/,
      (byte) 135,
      (byte) 189,
      (byte) 241,
      (byte) 199,
      (byte) 236,
      (byte) 76,
      (byte) 254,
      (byte) 100,
      (byte) 160 /*0xA0*/,
      (byte) 215,
      (byte) 160 /*0xA0*/,
      (byte) 18,
      (byte) 45,
      (byte) 135,
      (byte) 211,
      (byte) 213,
      (byte) 244,
      (byte) 170,
      (byte) 165,
      (byte) 192 /*0xC0*/,
      (byte) 223,
      (byte) 87,
      (byte) 61,
      (byte) 8,
      (byte) 163,
      (byte) 112 /*0x70*/,
      (byte) 248,
      (byte) 249,
      (byte) 105,
      (byte) 83,
      (byte) 74,
      (byte) 212,
      (byte) 47,
      (byte) 108,
      (byte) 27,
      (byte) 178,
      (byte) 197,
      (byte) 143,
      (byte) 149,
      (byte) 66,
      (byte) 95,
      (byte) 233,
      (byte) 87,
      (byte) 25,
      (byte) 149
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray8, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index] ^= numArray10[index];
    byte[] numArray11 = new byte[55];
    numArray11[27] = (byte) 196;
    numArray11[24] = (byte) 243;
    numArray11[44] = (byte) 57;
    numArray11[3] = (byte) 188;
    numArray11[4] = (byte) 37;
    numArray11[5] = (byte) 216;
    numArray11[6] = (byte) 207;
    numArray11[25] = (byte) 66;
    numArray11[54] = (byte) 222;
    numArray11[46] = (byte) 50;
    numArray11[10] = (byte) 230;
    numArray11[8] = (byte) 82;
    numArray11[12] = (byte) 125;
    numArray11[7] = (byte) 34;
    numArray11[33] = (byte) 38;
    numArray11[38] = (byte) 210;
    numArray11[19] = (byte) 80 /*0x50*/;
    numArray11[17] = (byte) 182;
    numArray11[48 /*0x30*/] = (byte) 243;
    numArray11[23] = (byte) 40;
    numArray11[20] = (byte) 114;
    numArray11[0] = (byte) 192 /*0xC0*/;
    numArray11[21] = (byte) 30;
    numArray11[28] = (byte) 253;
    numArray11[45] = (byte) 106;
    numArray11[22] = (byte) 62;
    numArray11[26] = (byte) 63 /*0x3F*/;
    numArray11[1] = (byte) 111;
    numArray11[15] = (byte) 236;
    numArray11[42] = (byte) 127 /*0x7F*/;
    numArray11[30] = (byte) 35;
    numArray11[9] = (byte) 210;
    numArray11[32 /*0x20*/] = (byte) 191;
    numArray11[29] = (byte) 225;
    numArray11[36] = (byte) 65;
    numArray11[2] = (byte) 212;
    numArray11[18] = (byte) 200;
    numArray11[35] = (byte) 36;
    numArray11[16 /*0x10*/] = (byte) 172;
    numArray11[43] = (byte) 54;
    numArray11[40] = (byte) 36;
    numArray11[39] = (byte) 149;
    numArray11[49] = (byte) 13;
    numArray11[11] = (byte) 149;
    numArray11[13] = (byte) 14;
    numArray11[34] = (byte) 223;
    numArray11[51] = (byte) 29;
    numArray11[47] = (byte) 179;
    numArray11[14] = (byte) 5;
    numArray11[41] = (byte) 148;
    numArray11[50] = (byte) 68;
    numArray11[37] = (byte) 91;
    numArray11[52] = (byte) 173;
    numArray11[53] = (byte) 225;
    numArray11[31 /*0x1F*/] = (byte) 240 /*0xF0*/;
    byte[] numArray12 = new byte[55]
    {
      (byte) 243,
      (byte) 34,
      (byte) 41,
      (byte) 184,
      (byte) 120,
      (byte) 17,
      (byte) 133,
      (byte) 3,
      (byte) 173,
      (byte) 251,
      (byte) 168,
      (byte) 119,
      (byte) 170,
      (byte) 79,
      (byte) 174,
      (byte) 13,
      (byte) 97,
      (byte) 239,
      (byte) 227,
      (byte) 251,
      (byte) 112 /*0x70*/,
      (byte) 104,
      (byte) 172,
      (byte) 251,
      (byte) 38,
      (byte) 207,
      (byte) 40,
      (byte) 117,
      (byte) 52,
      (byte) 114,
      (byte) 82,
      (byte) 218,
      (byte) 37,
      (byte) 192 /*0xC0*/,
      (byte) 140,
      (byte) 176 /*0xB0*/,
      (byte) 211,
      (byte) 104,
      (byte) 36,
      (byte) 220,
      (byte) 230,
      (byte) 119,
      (byte) 78,
      (byte) 63 /*0x3F*/,
      (byte) 241,
      (byte) 80 /*0x50*/,
      (byte) 230,
      (byte) 72,
      (byte) 182,
      (byte) 196,
      (byte) 237,
      (byte) 71,
      (byte) 9,
      (byte) 91,
      (byte) 239
    };
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray8, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index + 55] ^= numArray12[index];
    byte[] numArray13 = new byte[15];
    numArray13[2] = (byte) 210;
    numArray13[12] = (byte) 237;
    numArray13[10] = (byte) 15;
    numArray13[3] = (byte) 13;
    numArray13[6] = (byte) 212;
    numArray13[5] = (byte) 53;
    numArray13[11] = (byte) 241;
    numArray13[7] = (byte) 167;
    numArray13[8] = (byte) 179;
    numArray13[13] = (byte) 21;
    numArray13[1] = (byte) 65;
    numArray13[4] = (byte) 57;
    numArray13[9] = (byte) 248;
    numArray13[0] = (byte) 188;
    numArray13[14] = (byte) 191;
    byte[] numArray14 = new byte[15];
    numArray14[6] = (byte) 198;
    numArray14[11] = (byte) 169;
    numArray14[2] = (byte) 16 /*0x10*/;
    numArray14[3] = (byte) 29;
    numArray14[13] = (byte) 137;
    numArray14[5] = (byte) 16 /*0x10*/;
    numArray14[7] = (byte) 152;
    numArray14[9] = (byte) 164;
    numArray14[8] = (byte) 72;
    numArray14[1] = (byte) 134;
    numArray14[0] = (byte) 118;
    numArray14[10] = (byte) 205;
    numArray14[4] = (byte) 214;
    numArray14[12] = (byte) 76;
    numArray14[14] = (byte) 57;
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray8, 110, 15);
    for (int index = 0; index < 15; ++index)
      numArray8[index + 110] ^= numArray14[index];
    return Encoding.UTF8.GetString(numArray8);
  }

  internal static string ssp_appserver_12808()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[35];
      byte[] numArray2 = new byte[35]
      {
        (byte) 69,
        (byte) 65,
        (byte) 162,
        (byte) 144 /*0x90*/,
        (byte) 113,
        (byte) 171,
        (byte) 16 /*0x10*/,
        (byte) 76,
        (byte) 156,
        (byte) 6,
        (byte) 124,
        (byte) 168,
        (byte) 42,
        (byte) 218,
        (byte) 234,
        (byte) 207,
        (byte) 199,
        (byte) 182,
        (byte) 36,
        (byte) 59,
        (byte) 109,
        (byte) 214,
        (byte) 73,
        (byte) 49,
        (byte) 53,
        (byte) 87,
        (byte) 159,
        (byte) 27,
        (byte) 88,
        (byte) 106,
        (byte) 115,
        (byte) 150,
        (byte) 63 /*0x3F*/,
        (byte) 118,
        (byte) 136
      };
      byte[] numArray3 = new byte[35];
      numArray3[0] = (byte) 129;
      numArray3[1] = (byte) 6;
      numArray3[2] = (byte) 127 /*0x7F*/;
      numArray3[16 /*0x10*/] = (byte) 101;
      numArray3[4] = (byte) 94;
      numArray3[3] = (byte) 29;
      numArray3[6] = (byte) 158;
      numArray3[29] = byte.MaxValue;
      numArray3[28] = (byte) 211;
      numArray3[9] = (byte) 152;
      numArray3[17] = (byte) 144 /*0x90*/;
      numArray3[11] = (byte) 6;
      numArray3[24] = (byte) 206;
      numArray3[15] = (byte) 151;
      numArray3[12] = (byte) 237;
      numArray3[25] = (byte) 165;
      numArray3[27] = (byte) 199;
      numArray3[20] = (byte) 78;
      numArray3[14] = (byte) 245;
      numArray3[19] = (byte) 230;
      numArray3[8] = (byte) 204;
      numArray3[18] = (byte) 17;
      numArray3[22] = (byte) 147;
      numArray3[13] = (byte) 207;
      numArray3[23] = (byte) 228;
      numArray3[10] = (byte) 131;
      numArray3[26] = (byte) 95;
      numArray3[7] = (byte) 101;
      numArray3[21] = (byte) 167;
      numArray3[5] = (byte) 152;
      numArray3[30] = (byte) 198;
      numArray3[31 /*0x1F*/] = (byte) 6;
      numArray3[32 /*0x20*/] = (byte) 189;
      numArray3[33] = (byte) 186;
      numArray3[34] = (byte) 88;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 35);
      for (int index = 0; index < 35; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[35];
    byte[] numArray5 = new byte[35]
    {
      (byte) 6,
      (byte) 114,
      (byte) 105,
      (byte) 85,
      (byte) 98,
      (byte) 204,
      (byte) 163,
      (byte) 72,
      (byte) 153,
      (byte) 117,
      (byte) 139,
      (byte) 101,
      (byte) 150,
      (byte) 148,
      (byte) 44,
      (byte) 176 /*0xB0*/,
      (byte) 25,
      (byte) 142,
      (byte) 20,
      (byte) 91,
      (byte) 176 /*0xB0*/,
      (byte) 17,
      (byte) 212,
      (byte) 15,
      (byte) 78,
      (byte) 84,
      (byte) 126,
      (byte) 39,
      (byte) 91,
      (byte) 179,
      (byte) 66,
      (byte) 130,
      (byte) 243,
      (byte) 240 /*0xF0*/,
      (byte) 250
    };
    byte[] numArray6 = new byte[35]
    {
      (byte) 5,
      byte.MaxValue,
      (byte) 3,
      (byte) 241,
      (byte) 174,
      (byte) 53,
      (byte) 230,
      (byte) 62,
      (byte) 174,
      (byte) 236,
      (byte) 208 /*0xD0*/,
      (byte) 82,
      (byte) 97,
      (byte) 148,
      (byte) 209,
      (byte) 166,
      (byte) 167,
      (byte) 205,
      (byte) 122,
      (byte) 208 /*0xD0*/,
      (byte) 214,
      (byte) 57,
      (byte) 198,
      (byte) 70,
      (byte) 161,
      (byte) 150,
      (byte) 104,
      (byte) 43,
      (byte) 116,
      (byte) 155,
      (byte) 96 /*0x60*/,
      (byte) 171,
      (byte) 245,
      (byte) 84,
      (byte) 210
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 35);
    for (int index = 0; index < 35; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[18];
    byte[] response = new byte[18];
    Array.Copy((Array) sc_12780.sspq, 421, (Array) numArray7, 0, 18);
    key.Query(true, 335, numArray7, response);
    Array.Copy((Array) sc_12780.sspr, 421, (Array) numArray7, 0, 18);
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

  internal static string ssp_appserver_12809()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[119];
      byte[] numArray2 = new byte[55]
      {
        (byte) 6,
        (byte) 13,
        (byte) 78,
        (byte) 124,
        (byte) 236,
        (byte) 230,
        (byte) 51,
        (byte) 8,
        (byte) 114,
        (byte) 165,
        (byte) 153,
        (byte) 140,
        (byte) 84,
        byte.MaxValue,
        (byte) 248,
        (byte) 111,
        (byte) 163,
        (byte) 223,
        (byte) 158,
        (byte) 84,
        (byte) 166,
        (byte) 130,
        (byte) 87,
        (byte) 45,
        (byte) 234,
        (byte) 88,
        (byte) 122,
        (byte) 109,
        (byte) 77,
        (byte) 61,
        (byte) 180,
        (byte) 5,
        (byte) 2,
        (byte) 1,
        (byte) 19,
        (byte) 136,
        (byte) 86,
        (byte) 59,
        (byte) 228,
        (byte) 140,
        (byte) 75,
        (byte) 96 /*0x60*/,
        (byte) 90,
        (byte) 87,
        (byte) 182,
        (byte) 175,
        (byte) 174,
        (byte) 58,
        (byte) 220,
        (byte) 197,
        (byte) 18,
        (byte) 92,
        (byte) 86,
        (byte) 72,
        (byte) 179
      };
      byte[] numArray3 = new byte[55];
      numArray3[28] = (byte) 118;
      numArray3[17] = (byte) 115;
      numArray3[2] = (byte) 136;
      numArray3[46] = (byte) 118;
      numArray3[4] = (byte) 116;
      numArray3[0] = (byte) 56;
      numArray3[8] = (byte) 215;
      numArray3[7] = (byte) 126;
      numArray3[29] = (byte) 217;
      numArray3[9] = (byte) 17;
      numArray3[10] = (byte) 235;
      numArray3[11] = (byte) 137;
      numArray3[12] = (byte) 77;
      numArray3[13] = (byte) 200;
      numArray3[6] = (byte) 70;
      numArray3[15] = (byte) 0;
      numArray3[48 /*0x30*/] = (byte) 89;
      numArray3[33] = (byte) 252;
      numArray3[18] = (byte) 241;
      numArray3[34] = (byte) 53;
      numArray3[16 /*0x10*/] = (byte) 121;
      numArray3[20] = (byte) 148;
      numArray3[22] = (byte) 119;
      numArray3[23] = (byte) 130;
      numArray3[24] = (byte) 59;
      numArray3[26] = (byte) 10;
      numArray3[1] = byte.MaxValue;
      numArray3[27] = (byte) 148;
      numArray3[37] = (byte) 62;
      numArray3[54] = (byte) 177;
      numArray3[21] = (byte) 82;
      numArray3[25] = (byte) 14;
      numArray3[49] = (byte) 77;
      numArray3[30] = (byte) 122;
      numArray3[44] = (byte) 164;
      numArray3[35] = (byte) 86;
      numArray3[36] = (byte) 200;
      numArray3[40] = (byte) 73;
      numArray3[39] = (byte) 64 /*0x40*/;
      numArray3[19] = (byte) 225;
      numArray3[43] = (byte) 50;
      numArray3[41] = (byte) 15;
      numArray3[42] = (byte) 20;
      numArray3[51] = (byte) 226;
      numArray3[50] = (byte) 244;
      numArray3[45] = (byte) 44;
      numArray3[31 /*0x1F*/] = (byte) 110;
      numArray3[47] = (byte) 109;
      numArray3[3] = (byte) 25;
      numArray3[5] = (byte) 80 /*0x50*/;
      numArray3[14] = (byte) 42;
      numArray3[32 /*0x20*/] = (byte) 217;
      numArray3[52] = (byte) 119;
      numArray3[53] = (byte) 111;
      numArray3[38] = (byte) 233;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 169,
        (byte) 87,
        (byte) 233,
        (byte) 43,
        (byte) 51,
        (byte) 50,
        (byte) 9,
        (byte) 152,
        (byte) 229,
        (byte) 209,
        (byte) 4,
        (byte) 91,
        (byte) 158,
        (byte) 226,
        (byte) 190,
        (byte) 169,
        (byte) 241,
        (byte) 130,
        (byte) 160 /*0xA0*/,
        (byte) 2,
        (byte) 152,
        (byte) 84,
        (byte) 21,
        (byte) 77,
        (byte) 241,
        (byte) 37,
        (byte) 219,
        (byte) 128 /*0x80*/,
        (byte) 246,
        (byte) 107,
        (byte) 121,
        (byte) 224 /*0xE0*/,
        (byte) 51,
        (byte) 25,
        (byte) 141,
        (byte) 82,
        (byte) 103,
        (byte) 244,
        (byte) 82,
        (byte) 114,
        (byte) 28,
        (byte) 206,
        (byte) 61,
        (byte) 83,
        (byte) 87,
        (byte) 242,
        (byte) 205,
        (byte) 202,
        (byte) 236,
        (byte) 184,
        (byte) 82,
        (byte) 189,
        (byte) 204,
        (byte) 88,
        (byte) 31 /*0x1F*/
      };
      byte[] numArray5 = new byte[55];
      numArray5[23] = (byte) 218;
      numArray5[0] = (byte) 202;
      numArray5[10] = (byte) 36;
      numArray5[3] = (byte) 130;
      numArray5[13] = (byte) 99;
      numArray5[42] = (byte) 250;
      numArray5[6] = (byte) 116;
      numArray5[7] = (byte) 138;
      numArray5[8] = (byte) 46;
      numArray5[9] = (byte) 146;
      numArray5[32 /*0x20*/] = (byte) 97;
      numArray5[39] = (byte) 233;
      numArray5[12] = (byte) 35;
      numArray5[47] = (byte) 23;
      numArray5[53] = (byte) 106;
      numArray5[2] = (byte) 177;
      numArray5[44] = (byte) 208 /*0xD0*/;
      numArray5[17] = (byte) 90;
      numArray5[1] = (byte) 33;
      numArray5[30] = (byte) 35;
      numArray5[20] = (byte) 5;
      numArray5[21] = (byte) 140;
      numArray5[29] = (byte) 235;
      numArray5[49] = (byte) 196;
      numArray5[24] = (byte) 71;
      numArray5[25] = (byte) 188;
      numArray5[34] = (byte) 5;
      numArray5[27] = (byte) 203;
      numArray5[28] = (byte) 177;
      numArray5[11] = (byte) 167;
      numArray5[14] = (byte) 178;
      numArray5[31 /*0x1F*/] = (byte) 163;
      numArray5[41] = (byte) 32 /*0x20*/;
      numArray5[38] = (byte) 165;
      numArray5[19] = (byte) 194;
      numArray5[35] = (byte) 31 /*0x1F*/;
      numArray5[22] = (byte) 150;
      numArray5[37] = (byte) 224 /*0xE0*/;
      numArray5[15] = (byte) 32 /*0x20*/;
      numArray5[18] = (byte) 105;
      numArray5[40] = (byte) 77;
      numArray5[16 /*0x10*/] = (byte) 252;
      numArray5[4] = (byte) 45;
      numArray5[43] = (byte) 9;
      numArray5[45] = (byte) 149;
      numArray5[36] = (byte) 209;
      numArray5[46] = (byte) 99;
      numArray5[54] = (byte) 252;
      numArray5[48 /*0x30*/] = (byte) 50;
      numArray5[5] = (byte) 1;
      numArray5[50] = (byte) 167;
      numArray5[51] = (byte) 74;
      numArray5[52] = (byte) 78;
      numArray5[26] = (byte) 107;
      numArray5[33] = (byte) 49;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[9];
      numArray6[1] = (byte) 59;
      numArray6[3] = (byte) 69;
      numArray6[2] = (byte) 181;
      numArray6[4] = (byte) 243;
      numArray6[0] = (byte) 165;
      numArray6[5] = (byte) 99;
      numArray6[6] = (byte) 97;
      numArray6[7] = (byte) 156;
      numArray6[8] = (byte) 17;
      byte[] numArray7 = new byte[9]
      {
        (byte) 139,
        (byte) 128 /*0x80*/,
        (byte) 72,
        (byte) 250,
        (byte) 8,
        (byte) 125,
        (byte) 163,
        (byte) 234,
        (byte) 229
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 9);
      for (int index = 0; index < 9; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[19];
      byte[] response = new byte[19];
      Array.Copy((Array) sc_12780.sspq, 439, (Array) numArray8, 0, 19);
      key.Query(true, 335, numArray8, response);
      Array.Copy((Array) sc_12780.sspr, 439, (Array) numArray8, 0, 19);
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
    byte[] numArray9 = new byte[119];
    byte[] numArray10 = new byte[55]
    {
      (byte) 94,
      (byte) 33,
      (byte) 126,
      (byte) 190,
      (byte) 90,
      (byte) 36,
      (byte) 254,
      (byte) 19,
      (byte) 160 /*0xA0*/,
      (byte) 140,
      (byte) 31 /*0x1F*/,
      (byte) 184,
      (byte) 233,
      (byte) 184,
      (byte) 225,
      (byte) 7,
      (byte) 164,
      (byte) 169,
      (byte) 223,
      (byte) 155,
      (byte) 158,
      (byte) 108,
      (byte) 157,
      (byte) 227,
      (byte) 228,
      (byte) 31 /*0x1F*/,
      (byte) 202,
      (byte) 81,
      (byte) 195,
      (byte) 92,
      (byte) 59,
      (byte) 178,
      (byte) 211,
      (byte) 242,
      (byte) 104,
      (byte) 163,
      (byte) 181,
      (byte) 82,
      (byte) 65,
      (byte) 239,
      (byte) 201,
      (byte) 175,
      (byte) 143,
      (byte) 231,
      (byte) 73,
      (byte) 236,
      (byte) 18,
      (byte) 216,
      (byte) 68,
      (byte) 119,
      (byte) 234,
      (byte) 44,
      (byte) 192 /*0xC0*/,
      (byte) 46,
      (byte) 195
    };
    byte[] numArray11 = new byte[55];
    numArray11[35] = (byte) 72;
    numArray11[12] = (byte) 213;
    numArray11[2] = (byte) 161;
    numArray11[34] = (byte) 116;
    numArray11[4] = (byte) 147;
    numArray11[25] = (byte) 228;
    numArray11[43] = (byte) 17;
    numArray11[7] = (byte) 209;
    numArray11[8] = (byte) 52;
    numArray11[40] = (byte) 90;
    numArray11[30] = (byte) 230;
    numArray11[5] = (byte) 17;
    numArray11[1] = (byte) 98;
    numArray11[13] = (byte) 147;
    numArray11[18] = (byte) 97;
    numArray11[15] = (byte) 155;
    numArray11[26] = (byte) 73;
    numArray11[16 /*0x10*/] = (byte) 171;
    numArray11[48 /*0x30*/] = (byte) 211;
    numArray11[19] = (byte) 3;
    numArray11[51] = (byte) 222;
    numArray11[21] = (byte) 154;
    numArray11[22] = (byte) 139;
    numArray11[44] = (byte) 64 /*0x40*/;
    numArray11[28] = (byte) 247;
    numArray11[36] = (byte) 233;
    numArray11[32 /*0x20*/] = (byte) 37;
    numArray11[27] = (byte) 79;
    numArray11[31 /*0x1F*/] = (byte) 204;
    numArray11[29] = (byte) 26;
    numArray11[24] = (byte) 62;
    numArray11[23] = (byte) 236;
    numArray11[41] = (byte) 190;
    numArray11[33] = (byte) 24;
    numArray11[0] = (byte) 39;
    numArray11[53] = (byte) 78;
    numArray11[10] = (byte) 59;
    numArray11[37] = (byte) 34;
    numArray11[14] = (byte) 221;
    numArray11[39] = (byte) 46;
    numArray11[3] = (byte) 138;
    numArray11[46] = (byte) 104;
    numArray11[42] = (byte) 183;
    numArray11[11] = (byte) 52;
    numArray11[17] = (byte) 124;
    numArray11[45] = (byte) 175;
    numArray11[20] = (byte) 117;
    numArray11[47] = (byte) 138;
    numArray11[6] = (byte) 230;
    numArray11[9] = (byte) 208 /*0xD0*/;
    numArray11[50] = (byte) 59;
    numArray11[49] = (byte) 185;
    numArray11[52] = (byte) 224 /*0xE0*/;
    numArray11[38] = (byte) 236;
    numArray11[54] = (byte) 53;
    key.Query(true, 335, numArray10, numArray10);
    Array.Copy((Array) numArray10, 0, (Array) numArray9, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray9[index] ^= numArray11[index];
    byte[] numArray12 = new byte[55];
    numArray12[16 /*0x10*/] = (byte) 90;
    numArray12[1] = (byte) 224 /*0xE0*/;
    numArray12[54] = (byte) 173;
    numArray12[3] = (byte) 104;
    numArray12[4] = (byte) 161;
    numArray12[29] = (byte) 32 /*0x20*/;
    numArray12[6] = (byte) 210;
    numArray12[14] = (byte) 216;
    numArray12[12] = (byte) 30;
    numArray12[8] = (byte) 123;
    numArray12[28] = (byte) 74;
    numArray12[52] = (byte) 70;
    numArray12[37] = (byte) 44;
    numArray12[10] = (byte) 200;
    numArray12[34] = (byte) 27;
    numArray12[11] = (byte) 219;
    numArray12[41] = (byte) 83;
    numArray12[17] = (byte) 181;
    numArray12[50] = (byte) 118;
    numArray12[7] = (byte) 66;
    numArray12[20] = (byte) 32 /*0x20*/;
    numArray12[21] = (byte) 190;
    numArray12[22] = (byte) 242;
    numArray12[23] = (byte) 242;
    numArray12[19] = (byte) 146;
    numArray12[25] = (byte) 5;
    numArray12[26] = (byte) 39;
    numArray12[32 /*0x20*/] = (byte) 96 /*0x60*/;
    numArray12[5] = (byte) 3;
    numArray12[2] = (byte) 45;
    numArray12[30] = (byte) 71;
    numArray12[13] = (byte) 55;
    numArray12[47] = (byte) 237;
    numArray12[33] = (byte) 81;
    numArray12[24] = (byte) 106;
    numArray12[15] = (byte) 97;
    numArray12[36] = (byte) 217;
    numArray12[31 /*0x1F*/] = (byte) 89;
    numArray12[49] = (byte) 99;
    numArray12[44] = (byte) 82;
    numArray12[0] = byte.MaxValue;
    numArray12[48 /*0x30*/] = (byte) 192 /*0xC0*/;
    numArray12[9] = (byte) 65;
    numArray12[43] = (byte) 197;
    numArray12[51] = (byte) 166;
    numArray12[45] = (byte) 172;
    numArray12[35] = (byte) 152;
    numArray12[27] = (byte) 216;
    numArray12[38] = (byte) 122;
    numArray12[46] = (byte) 232;
    numArray12[18] = (byte) 142;
    numArray12[40] = (byte) 121;
    numArray12[39] = (byte) 91;
    numArray12[53] = (byte) 130;
    numArray12[42] = (byte) 96 /*0x60*/;
    byte[] numArray13 = new byte[55];
    numArray13[27] = (byte) 21;
    numArray13[5] = (byte) 136;
    numArray13[2] = (byte) 27;
    numArray13[50] = (byte) 198;
    numArray13[30] = (byte) 131;
    numArray13[39] = (byte) 102;
    numArray13[6] = (byte) 53;
    numArray13[7] = (byte) 43;
    numArray13[8] = (byte) 79;
    numArray13[38] = (byte) 2;
    numArray13[37] = (byte) 88;
    numArray13[53] = (byte) 178;
    numArray13[12] = (byte) 167;
    numArray13[13] = (byte) 42;
    numArray13[16 /*0x10*/] = (byte) 40;
    numArray13[0] = (byte) 151;
    numArray13[29] = (byte) 75;
    numArray13[11] = (byte) 28;
    numArray13[18] = (byte) 39;
    numArray13[19] = (byte) 167;
    numArray13[20] = (byte) 80 /*0x50*/;
    numArray13[9] = (byte) 41;
    numArray13[14] = (byte) 165;
    numArray13[23] = (byte) 107;
    numArray13[24] = (byte) 44;
    numArray13[25] = (byte) 54;
    numArray13[34] = (byte) 49;
    numArray13[40] = (byte) 230;
    numArray13[26] = (byte) 36;
    numArray13[4] = (byte) 233;
    numArray13[3] = (byte) 18;
    numArray13[31 /*0x1F*/] = (byte) 196;
    numArray13[32 /*0x20*/] = (byte) 118;
    numArray13[21] = (byte) 5;
    numArray13[22] = (byte) 195;
    numArray13[35] = (byte) 75;
    numArray13[42] = (byte) 110;
    numArray13[47] = (byte) 206;
    numArray13[45] = (byte) 205;
    numArray13[41] = (byte) 214;
    numArray13[28] = (byte) 54;
    numArray13[36] = (byte) 89;
    numArray13[17] = (byte) 39;
    numArray13[43] = (byte) 48 /*0x30*/;
    numArray13[44] = (byte) 180;
    numArray13[46] = (byte) 156;
    numArray13[33] = (byte) 89;
    numArray13[1] = (byte) 24;
    numArray13[48 /*0x30*/] = (byte) 227;
    numArray13[49] = (byte) 150;
    numArray13[10] = (byte) 154;
    numArray13[51] = (byte) 125;
    numArray13[52] = (byte) 103;
    numArray13[15] = (byte) 2;
    numArray13[54] = (byte) 42;
    key.Query(true, 335, numArray12, numArray12);
    Array.Copy((Array) numArray12, 0, (Array) numArray9, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray9[index + 55] ^= numArray13[index];
    byte[] numArray14 = new byte[9]
    {
      (byte) 222,
      (byte) 180,
      (byte) 124,
      (byte) 57,
      (byte) 73,
      (byte) 6,
      (byte) 81,
      (byte) 34,
      (byte) 91
    };
    byte[] numArray15 = new byte[9];
    numArray15[2] = (byte) 136;
    numArray15[1] = (byte) 74;
    numArray15[6] = (byte) 75;
    numArray15[3] = (byte) 69;
    numArray15[4] = (byte) 241;
    numArray15[5] = (byte) 15;
    numArray15[8] = (byte) 179;
    numArray15[7] = (byte) 240 /*0xF0*/;
    numArray15[0] = (byte) 180;
    key.Query(true, 335, numArray14, numArray14);
    Array.Copy((Array) numArray14, 0, (Array) numArray9, 110, 9);
    for (int index = 0; index < 9; ++index)
      numArray9[index + 110] ^= numArray15[index];
    return Encoding.UTF8.GetString(numArray9);
  }

  internal static string ssp_appserver_12810()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[131];
      byte[] numArray2 = new byte[55]
      {
        (byte) 246,
        (byte) 14,
        (byte) 214,
        (byte) 174,
        (byte) 22,
        (byte) 21,
        (byte) 38,
        (byte) 160 /*0xA0*/,
        (byte) 164,
        (byte) 84,
        (byte) 112 /*0x70*/,
        (byte) 135,
        (byte) 36,
        (byte) 58,
        (byte) 228,
        (byte) 164,
        (byte) 85,
        (byte) 67,
        (byte) 1,
        (byte) 80 /*0x50*/,
        (byte) 43,
        (byte) 109,
        (byte) 142,
        (byte) 73,
        (byte) 59,
        (byte) 15,
        (byte) 59,
        (byte) 18,
        (byte) 137,
        (byte) 213,
        (byte) 71,
        (byte) 128 /*0x80*/,
        (byte) 114,
        (byte) 169,
        (byte) 190,
        (byte) 110,
        (byte) 98,
        (byte) 154,
        (byte) 239,
        (byte) 59,
        (byte) 251,
        (byte) 187,
        (byte) 131,
        (byte) 16 /*0x10*/,
        (byte) 214,
        (byte) 39,
        (byte) 191,
        (byte) 212,
        (byte) 23,
        (byte) 49,
        (byte) 165,
        (byte) 228,
        (byte) 6,
        (byte) 157,
        (byte) 10
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 20,
        (byte) 151,
        (byte) 125,
        (byte) 25,
        (byte) 12,
        (byte) 4,
        (byte) 195,
        (byte) 176 /*0xB0*/,
        (byte) 175,
        (byte) 129,
        (byte) 36,
        (byte) 10,
        (byte) 188,
        (byte) 31 /*0x1F*/,
        (byte) 197,
        (byte) 149,
        (byte) 195,
        (byte) 242,
        (byte) 228,
        (byte) 136,
        (byte) 124,
        (byte) 168,
        (byte) 63 /*0x3F*/,
        (byte) 153,
        (byte) 199,
        (byte) 243,
        (byte) 138,
        (byte) 61,
        (byte) 95,
        (byte) 195,
        (byte) 71,
        (byte) 204,
        (byte) 111,
        (byte) 249,
        (byte) 0,
        (byte) 188,
        (byte) 111,
        (byte) 190,
        (byte) 68,
        (byte) 156,
        (byte) 58,
        (byte) 199,
        (byte) 215,
        (byte) 71,
        (byte) 187,
        (byte) 65,
        (byte) 131,
        (byte) 54,
        (byte) 99,
        (byte) 185,
        (byte) 80 /*0x50*/,
        (byte) 167,
        (byte) 244,
        (byte) 25,
        (byte) 128 /*0x80*/
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55];
      numArray4[40] = (byte) 180;
      numArray4[39] = (byte) 164;
      numArray4[10] = (byte) 123;
      numArray4[20] = (byte) 126;
      numArray4[4] = (byte) 20;
      numArray4[1] = (byte) 115;
      numArray4[0] = (byte) 141;
      numArray4[7] = (byte) 13;
      numArray4[21] = (byte) 114;
      numArray4[9] = (byte) 20;
      numArray4[14] = (byte) 186;
      numArray4[2] = (byte) 7;
      numArray4[12] = (byte) 117;
      numArray4[6] = (byte) 206;
      numArray4[38] = (byte) 231;
      numArray4[19] = (byte) 233;
      numArray4[16 /*0x10*/] = (byte) 197;
      numArray4[48 /*0x30*/] = (byte) 234;
      numArray4[18] = (byte) 111;
      numArray4[33] = (byte) 238;
      numArray4[53] = (byte) 159;
      numArray4[23] = (byte) 79;
      numArray4[11] = (byte) 124;
      numArray4[8] = (byte) 240 /*0xF0*/;
      numArray4[24] = (byte) 103;
      numArray4[13] = (byte) 65;
      numArray4[22] = (byte) 19;
      numArray4[27] = (byte) 48 /*0x30*/;
      numArray4[28] = (byte) 98;
      numArray4[29] = (byte) 121;
      numArray4[30] = (byte) 212;
      numArray4[31 /*0x1F*/] = (byte) 159;
      numArray4[32 /*0x20*/] = (byte) 53;
      numArray4[37] = (byte) 97;
      numArray4[35] = (byte) 39;
      numArray4[41] = (byte) 2;
      numArray4[15] = (byte) 83;
      numArray4[50] = (byte) 65;
      numArray4[34] = (byte) 213;
      numArray4[26] = (byte) 87;
      numArray4[5] = (byte) 22;
      numArray4[44] = (byte) 68;
      numArray4[45] = (byte) 87;
      numArray4[43] = (byte) 18;
      numArray4[42] = (byte) 229;
      numArray4[17] = (byte) 84;
      numArray4[46] = (byte) 161;
      numArray4[47] = (byte) 240 /*0xF0*/;
      numArray4[36] = (byte) 232;
      numArray4[49] = (byte) 145;
      numArray4[25] = (byte) 251;
      numArray4[51] = (byte) 73;
      numArray4[52] = (byte) 154;
      numArray4[3] = (byte) 218;
      numArray4[54] = (byte) 0;
      byte[] numArray5 = new byte[55];
      numArray5[4] = (byte) 247;
      numArray5[35] = (byte) 185;
      numArray5[11] = (byte) 85;
      numArray5[3] = (byte) 99;
      numArray5[21] = (byte) 63 /*0x3F*/;
      numArray5[54] = (byte) 177;
      numArray5[6] = (byte) 209;
      numArray5[7] = (byte) 217;
      numArray5[0] = (byte) 107;
      numArray5[32 /*0x20*/] = (byte) 237;
      numArray5[8] = (byte) 125;
      numArray5[13] = (byte) 185;
      numArray5[1] = (byte) 125;
      numArray5[15] = (byte) 199;
      numArray5[10] = (byte) 203;
      numArray5[30] = (byte) 80 /*0x50*/;
      numArray5[44] = (byte) 196;
      numArray5[47] = (byte) 222;
      numArray5[48 /*0x30*/] = (byte) 54;
      numArray5[19] = (byte) 47;
      numArray5[20] = (byte) 39;
      numArray5[37] = (byte) 176 /*0xB0*/;
      numArray5[22] = (byte) 178;
      numArray5[5] = (byte) 70;
      numArray5[51] = (byte) 38;
      numArray5[25] = (byte) 229;
      numArray5[26] = (byte) 130;
      numArray5[27] = (byte) 40;
      numArray5[28] = (byte) 196;
      numArray5[29] = (byte) 141;
      numArray5[49] = (byte) 216;
      numArray5[40] = (byte) 29;
      numArray5[14] = (byte) 165;
      numArray5[33] = (byte) 210;
      numArray5[34] = (byte) 228;
      numArray5[43] = (byte) 186;
      numArray5[36] = (byte) 136;
      numArray5[9] = (byte) 140;
      numArray5[38] = (byte) 11;
      numArray5[39] = (byte) 46;
      numArray5[31 /*0x1F*/] = (byte) 157;
      numArray5[41] = (byte) 1;
      numArray5[42] = (byte) 8;
      numArray5[2] = (byte) 120;
      numArray5[17] = (byte) 110;
      numArray5[45] = (byte) 14;
      numArray5[46] = (byte) 41;
      numArray5[23] = (byte) 239;
      numArray5[50] = (byte) 202;
      numArray5[16 /*0x10*/] = (byte) 147;
      numArray5[12] = (byte) 232;
      numArray5[24] = (byte) 200;
      numArray5[52] = (byte) 97;
      numArray5[18] = (byte) 181;
      numArray5[53] = (byte) 111;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[21];
      numArray6[17] = (byte) 96 /*0x60*/;
      numArray6[7] = (byte) 6;
      numArray6[4] = (byte) 106;
      numArray6[15] = (byte) 141;
      numArray6[12] = (byte) 97;
      numArray6[5] = (byte) 49;
      numArray6[6] = (byte) 228;
      numArray6[0] = (byte) 10;
      numArray6[8] = (byte) 159;
      numArray6[9] = (byte) 132;
      numArray6[13] = (byte) 183;
      numArray6[11] = (byte) 132;
      numArray6[10] = (byte) 47;
      numArray6[2] = (byte) 128 /*0x80*/;
      numArray6[14] = (byte) 235;
      numArray6[3] = (byte) 116;
      numArray6[16 /*0x10*/] = (byte) 88;
      numArray6[1] = (byte) 101;
      numArray6[18] = (byte) 169;
      numArray6[19] = (byte) 238;
      numArray6[20] = (byte) 15;
      byte[] numArray7 = new byte[21]
      {
        (byte) 185,
        (byte) 8,
        (byte) 23,
        (byte) 208 /*0xD0*/,
        (byte) 105,
        (byte) 32 /*0x20*/,
        (byte) 174,
        (byte) 84,
        (byte) 122,
        (byte) 70,
        (byte) 29,
        (byte) 215,
        (byte) 179,
        (byte) 176 /*0xB0*/,
        (byte) 128 /*0x80*/,
        (byte) 245,
        (byte) 140,
        (byte) 151,
        (byte) 49,
        (byte) 166,
        (byte) 221
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 21);
      for (int index = 0; index < 21; ++index)
        numArray1[index + 110] ^= numArray7[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray8 = new byte[131];
    byte[] numArray9 = new byte[55]
    {
      (byte) 78,
      (byte) 43,
      (byte) 52,
      (byte) 123,
      (byte) 178,
      (byte) 144 /*0x90*/,
      (byte) 235,
      (byte) 182,
      (byte) 128 /*0x80*/,
      (byte) 33,
      (byte) 250,
      (byte) 198,
      (byte) 196,
      (byte) 99,
      (byte) 8,
      (byte) 115,
      (byte) 241,
      (byte) 152,
      (byte) 80 /*0x50*/,
      (byte) 164,
      (byte) 38,
      (byte) 199,
      (byte) 40,
      (byte) 62,
      (byte) 198,
      (byte) 253,
      (byte) 62,
      (byte) 13,
      (byte) 94,
      (byte) 163,
      (byte) 247,
      (byte) 28,
      (byte) 194,
      (byte) 106,
      (byte) 193,
      (byte) 54,
      (byte) 137,
      (byte) 119,
      (byte) 187,
      (byte) 190,
      (byte) 244,
      (byte) 82,
      (byte) 60,
      (byte) 22,
      (byte) 126,
      (byte) 94,
      (byte) 203,
      (byte) 182,
      (byte) 75,
      (byte) 214,
      (byte) 89,
      (byte) 127 /*0x7F*/,
      (byte) 162,
      (byte) 56,
      (byte) 44
    };
    byte[] numArray10 = new byte[55]
    {
      (byte) 127 /*0x7F*/,
      (byte) 210,
      (byte) 25,
      (byte) 118,
      (byte) 20,
      (byte) 1,
      (byte) 52,
      (byte) 0,
      (byte) 233,
      (byte) 149,
      (byte) 114,
      (byte) 169,
      (byte) 24,
      (byte) 42,
      (byte) 158,
      (byte) 44,
      (byte) 189,
      (byte) 80 /*0x50*/,
      (byte) 239,
      (byte) 118,
      (byte) 166,
      (byte) 23,
      (byte) 239,
      (byte) 81,
      (byte) 37,
      (byte) 118,
      (byte) 185,
      (byte) 6,
      (byte) 132,
      (byte) 192 /*0xC0*/,
      (byte) 42,
      (byte) 153,
      (byte) 101,
      (byte) 206,
      (byte) 145,
      (byte) 238,
      (byte) 244,
      (byte) 252,
      (byte) 32 /*0x20*/,
      (byte) 233,
      (byte) 138,
      (byte) 202,
      (byte) 29,
      (byte) 105,
      (byte) 241,
      (byte) 20,
      (byte) 98,
      (byte) 196,
      (byte) 211,
      (byte) 222,
      (byte) 169,
      (byte) 62,
      (byte) 175,
      (byte) 121,
      (byte) 248
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray8, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index] ^= numArray10[index];
    byte[] numArray11 = new byte[55];
    numArray11[50] = (byte) 17;
    numArray11[1] = (byte) 181;
    numArray11[6] = (byte) 221;
    numArray11[15] = (byte) 221;
    numArray11[4] = (byte) 221;
    numArray11[5] = (byte) 88;
    numArray11[35] = (byte) 194;
    numArray11[7] = (byte) 42;
    numArray11[13] = (byte) 234;
    numArray11[31 /*0x1F*/] = (byte) 61;
    numArray11[9] = (byte) 4;
    numArray11[48 /*0x30*/] = (byte) 224 /*0xE0*/;
    numArray11[39] = (byte) 250;
    numArray11[8] = (byte) 221;
    numArray11[53] = (byte) 252;
    numArray11[11] = (byte) 162;
    numArray11[16 /*0x10*/] = (byte) 145;
    numArray11[17] = (byte) 175;
    numArray11[18] = (byte) 211;
    numArray11[19] = (byte) 138;
    numArray11[20] = (byte) 18;
    numArray11[21] = (byte) 192 /*0xC0*/;
    numArray11[29] = (byte) 122;
    numArray11[10] = (byte) 89;
    numArray11[26] = (byte) 136;
    numArray11[25] = (byte) 194;
    numArray11[42] = (byte) 160 /*0xA0*/;
    numArray11[52] = (byte) 89;
    numArray11[40] = (byte) 95;
    numArray11[22] = (byte) 104;
    numArray11[14] = (byte) 121;
    numArray11[37] = (byte) 108;
    numArray11[32 /*0x20*/] = (byte) 58;
    numArray11[33] = (byte) 52;
    numArray11[36] = (byte) 6;
    numArray11[34] = (byte) 147;
    numArray11[38] = (byte) 23;
    numArray11[2] = (byte) 181;
    numArray11[30] = (byte) 138;
    numArray11[3] = (byte) 82;
    numArray11[0] = (byte) 245;
    numArray11[41] = (byte) 236;
    numArray11[28] = (byte) 119;
    numArray11[47] = (byte) 235;
    numArray11[44] = (byte) 113;
    numArray11[45] = (byte) 20;
    numArray11[46] = (byte) 121;
    numArray11[12] = (byte) 209;
    numArray11[24] = (byte) 135;
    numArray11[49] = (byte) 162;
    numArray11[43] = (byte) 90;
    numArray11[51] = (byte) 136;
    numArray11[23] = (byte) 50;
    numArray11[27] = (byte) 73;
    numArray11[54] = (byte) 240 /*0xF0*/;
    byte[] numArray12 = new byte[55];
    numArray12[52] = (byte) 193;
    numArray12[1] = (byte) 50;
    numArray12[28] = (byte) 200;
    numArray12[3] = (byte) 41;
    numArray12[49] = (byte) 46;
    numArray12[5] = (byte) 28;
    numArray12[6] = (byte) 143;
    numArray12[11] = (byte) 55;
    numArray12[8] = (byte) 98;
    numArray12[53] = (byte) 95;
    numArray12[24] = (byte) 203;
    numArray12[45] = (byte) 212;
    numArray12[13] = (byte) 194;
    numArray12[44] = (byte) 125;
    numArray12[14] = (byte) 178;
    numArray12[15] = (byte) 228;
    numArray12[16 /*0x10*/] = (byte) 22;
    numArray12[2] = (byte) 165;
    numArray12[27] = (byte) 184;
    numArray12[20] = (byte) 64 /*0x40*/;
    numArray12[22] = (byte) 238;
    numArray12[19] = byte.MaxValue;
    numArray12[18] = (byte) 43;
    numArray12[40] = (byte) 18;
    numArray12[38] = (byte) 177;
    numArray12[10] = (byte) 190;
    numArray12[26] = (byte) 222;
    numArray12[0] = (byte) 107;
    numArray12[4] = (byte) 92;
    numArray12[12] = (byte) 17;
    numArray12[31 /*0x1F*/] = (byte) 209;
    numArray12[30] = (byte) 52;
    numArray12[32 /*0x20*/] = (byte) 9;
    numArray12[33] = (byte) 25;
    numArray12[17] = (byte) 19;
    numArray12[35] = (byte) 55;
    numArray12[36] = (byte) 59;
    numArray12[37] = (byte) 124;
    numArray12[21] = (byte) 236;
    numArray12[43] = (byte) 237;
    numArray12[46] = (byte) 17;
    numArray12[41] = (byte) 242;
    numArray12[42] = (byte) 27;
    numArray12[48 /*0x30*/] = (byte) 95;
    numArray12[9] = (byte) 213;
    numArray12[23] = (byte) 43;
    numArray12[34] = (byte) 185;
    numArray12[47] = (byte) 35;
    numArray12[7] = (byte) 225;
    numArray12[29] = (byte) 55;
    numArray12[50] = (byte) 1;
    numArray12[25] = (byte) 47;
    numArray12[51] = (byte) 226;
    numArray12[39] = (byte) 206;
    numArray12[54] = (byte) 18;
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray8, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index + 55] ^= numArray12[index];
    byte[] numArray13 = new byte[21]
    {
      (byte) 85,
      (byte) 142,
      (byte) 103,
      (byte) 43,
      (byte) 232,
      (byte) 211,
      (byte) 126,
      (byte) 103,
      (byte) 211,
      (byte) 189,
      (byte) 223,
      (byte) 174,
      (byte) 10,
      (byte) 145,
      (byte) 54,
      (byte) 114,
      (byte) 182,
      (byte) 189,
      (byte) 144 /*0x90*/,
      (byte) 161,
      (byte) 197
    };
    byte[] numArray14 = new byte[21]
    {
      (byte) 89,
      (byte) 67,
      (byte) 178,
      (byte) 57,
      (byte) 56,
      (byte) 157,
      (byte) 154,
      (byte) 202,
      (byte) 41,
      (byte) 199,
      (byte) 184,
      (byte) 171,
      (byte) 112 /*0x70*/,
      (byte) 243,
      (byte) 34,
      (byte) 75,
      (byte) 18,
      (byte) 151,
      (byte) 124,
      (byte) 32 /*0x20*/,
      (byte) 15
    };
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray8, 110, 21);
    for (int index = 0; index < 21; ++index)
      numArray8[index + 110] ^= numArray14[index];
    return Encoding.UTF8.GetString(numArray8);
  }

  internal static string ssp_appserver_12811()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[35];
      byte[] numArray2 = new byte[35];
      numArray2[24] = (byte) 227;
      numArray2[26] = (byte) 180;
      numArray2[2] = (byte) 19;
      numArray2[3] = (byte) 186;
      numArray2[4] = (byte) 145;
      numArray2[5] = (byte) 251;
      numArray2[6] = (byte) 155;
      numArray2[10] = (byte) 189;
      numArray2[8] = (byte) 37;
      numArray2[21] = (byte) 221;
      numArray2[29] = (byte) 75;
      numArray2[32 /*0x20*/] = (byte) 127 /*0x7F*/;
      numArray2[12] = (byte) 203;
      numArray2[16 /*0x10*/] = (byte) 0;
      numArray2[0] = (byte) 89;
      numArray2[13] = (byte) 186;
      numArray2[1] = (byte) 146;
      numArray2[25] = (byte) 27;
      numArray2[18] = (byte) 195;
      numArray2[19] = (byte) 164;
      numArray2[20] = (byte) 237;
      numArray2[14] = (byte) 136;
      numArray2[11] = (byte) 194;
      numArray2[23] = (byte) 51;
      numArray2[33] = (byte) 192 /*0xC0*/;
      numArray2[17] = (byte) 74;
      numArray2[34] = (byte) 142;
      numArray2[27] = (byte) 77;
      numArray2[22] = (byte) 230;
      numArray2[9] = (byte) 109;
      numArray2[28] = (byte) 142;
      numArray2[31 /*0x1F*/] = (byte) 72;
      numArray2[30] = (byte) 6;
      numArray2[15] = (byte) 195;
      numArray2[7] = (byte) 24;
      byte[] numArray3 = new byte[35];
      numArray3[31 /*0x1F*/] = (byte) 115;
      numArray3[1] = (byte) 93;
      numArray3[2] = (byte) 100;
      numArray3[3] = (byte) 27;
      numArray3[24] = (byte) 0;
      numArray3[5] = (byte) 198;
      numArray3[6] = (byte) 108;
      numArray3[7] = (byte) 14;
      numArray3[8] = (byte) 30;
      numArray3[20] = (byte) 137;
      numArray3[10] = (byte) 131;
      numArray3[11] = (byte) 179;
      numArray3[12] = (byte) 90;
      numArray3[13] = (byte) 2;
      numArray3[14] = (byte) 235;
      numArray3[33] = (byte) 170;
      numArray3[21] = (byte) 25;
      numArray3[23] = (byte) 83;
      numArray3[16 /*0x10*/] = (byte) 128 /*0x80*/;
      numArray3[18] = (byte) 138;
      numArray3[22] = (byte) 67;
      numArray3[29] = (byte) 218;
      numArray3[4] = (byte) 157;
      numArray3[30] = (byte) 215;
      numArray3[25] = (byte) 130;
      numArray3[15] = (byte) 140;
      numArray3[26] = (byte) 93;
      numArray3[19] = (byte) 140;
      numArray3[28] = (byte) 91;
      numArray3[9] = (byte) 72;
      numArray3[34] = (byte) 3;
      numArray3[0] = (byte) 30;
      numArray3[32 /*0x20*/] = (byte) 103;
      numArray3[27] = (byte) 83;
      numArray3[17] = (byte) 225;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 35);
      for (int index = 0; index < 35; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[35];
    byte[] numArray5 = new byte[35];
    numArray5[34] = (byte) 91;
    numArray5[1] = (byte) 37;
    numArray5[6] = (byte) 68;
    numArray5[3] = (byte) 232;
    numArray5[0] = (byte) 226;
    numArray5[5] = (byte) 163;
    numArray5[2] = (byte) 131;
    numArray5[7] = (byte) 149;
    numArray5[22] = (byte) 76;
    numArray5[33] = (byte) 68;
    numArray5[20] = (byte) 133;
    numArray5[8] = (byte) 9;
    numArray5[11] = (byte) 99;
    numArray5[13] = (byte) 4;
    numArray5[27] = (byte) 81;
    numArray5[9] = (byte) 217;
    numArray5[16 /*0x10*/] = (byte) 178;
    numArray5[17] = (byte) 205;
    numArray5[32 /*0x20*/] = (byte) 6;
    numArray5[31 /*0x1F*/] = (byte) 21;
    numArray5[25] = (byte) 183;
    numArray5[30] = (byte) 43;
    numArray5[12] = (byte) 175;
    numArray5[23] = (byte) 238;
    numArray5[24] = (byte) 5;
    numArray5[21] = (byte) 106;
    numArray5[26] = (byte) 198;
    numArray5[10] = (byte) 82;
    numArray5[28] = (byte) 218;
    numArray5[29] = (byte) 132;
    numArray5[18] = (byte) 124;
    numArray5[14] = (byte) 200;
    numArray5[15] = (byte) 24;
    numArray5[4] = (byte) 36;
    numArray5[19] = (byte) 60;
    byte[] numArray6 = new byte[35]
    {
      (byte) 252,
      (byte) 33,
      (byte) 140,
      (byte) 206,
      (byte) 147,
      (byte) 54,
      (byte) 123,
      (byte) 119,
      (byte) 252,
      (byte) 90,
      (byte) 181,
      (byte) 84,
      (byte) 162,
      (byte) 80 /*0x50*/,
      (byte) 102,
      (byte) 122,
      (byte) 101,
      (byte) 121,
      (byte) 85,
      (byte) 146,
      (byte) 247,
      (byte) 212,
      (byte) 67,
      (byte) 61,
      (byte) 252,
      (byte) 186,
      (byte) 160 /*0xA0*/,
      (byte) 254,
      (byte) 76,
      (byte) 31 /*0x1F*/,
      (byte) 60,
      (byte) 214,
      (byte) 4,
      (byte) 183,
      (byte) 177
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 35);
    for (int index = 0; index < 35; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12812()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[147];
      byte[] numArray2 = new byte[55]
      {
        (byte) 181,
        (byte) 158,
        (byte) 126,
        (byte) 26,
        (byte) 54,
        (byte) 171,
        (byte) 98,
        (byte) 93,
        (byte) 57,
        (byte) 27,
        (byte) 117,
        (byte) 114,
        (byte) 31 /*0x1F*/,
        (byte) 71,
        (byte) 71,
        (byte) 227,
        (byte) 69,
        (byte) 249,
        (byte) 201,
        (byte) 250,
        (byte) 86,
        (byte) 147,
        (byte) 43,
        (byte) 102,
        (byte) 132,
        (byte) 122,
        (byte) 252,
        (byte) 233,
        (byte) 190,
        (byte) 108,
        (byte) 53,
        (byte) 9,
        (byte) 82,
        (byte) 172,
        (byte) 9,
        (byte) 94,
        (byte) 248,
        (byte) 189,
        (byte) 94,
        (byte) 56,
        (byte) 10,
        (byte) 127 /*0x7F*/,
        (byte) 185,
        (byte) 159,
        (byte) 15,
        (byte) 59,
        (byte) 29,
        (byte) 21,
        (byte) 34,
        (byte) 198,
        (byte) 100,
        (byte) 143,
        (byte) 59,
        (byte) 107,
        (byte) 242
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 46,
        (byte) 86,
        (byte) 231,
        (byte) 224 /*0xE0*/,
        (byte) 114,
        (byte) 116,
        (byte) 121,
        (byte) 85,
        (byte) 105,
        (byte) 121,
        (byte) 170,
        (byte) 186,
        (byte) 75,
        (byte) 168,
        (byte) 121,
        (byte) 113,
        (byte) 25,
        (byte) 189,
        (byte) 195,
        (byte) 16 /*0x10*/,
        (byte) 224 /*0xE0*/,
        (byte) 93,
        (byte) 25,
        (byte) 164,
        (byte) 231,
        (byte) 113,
        (byte) 222,
        (byte) 37,
        (byte) 235,
        (byte) 74,
        (byte) 34,
        (byte) 165,
        (byte) 76,
        (byte) 117,
        (byte) 39,
        (byte) 107,
        (byte) 160 /*0xA0*/,
        (byte) 254,
        (byte) 132,
        (byte) 214,
        (byte) 193,
        (byte) 39,
        (byte) 15,
        (byte) 253,
        (byte) 216,
        (byte) 248,
        (byte) 162,
        (byte) 241,
        (byte) 229,
        (byte) 40,
        (byte) 155,
        (byte) 218,
        (byte) 149,
        (byte) 218,
        (byte) 90
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 24,
        (byte) 150,
        (byte) 188,
        (byte) 174,
        (byte) 120,
        (byte) 158,
        (byte) 205,
        (byte) 230,
        (byte) 108,
        (byte) 31 /*0x1F*/,
        (byte) 115,
        (byte) 62,
        (byte) 233,
        (byte) 235,
        (byte) 136,
        (byte) 154,
        (byte) 145,
        (byte) 239,
        (byte) 214,
        (byte) 217,
        (byte) 139,
        (byte) 87,
        (byte) 72,
        (byte) 92,
        (byte) 231,
        (byte) 199,
        (byte) 248,
        (byte) 7,
        (byte) 165,
        (byte) 205,
        (byte) 231,
        (byte) 186,
        (byte) 230,
        (byte) 234,
        (byte) 224 /*0xE0*/,
        (byte) 38,
        (byte) 162,
        (byte) 137,
        (byte) 145,
        (byte) 225,
        (byte) 236,
        (byte) 185,
        (byte) 162,
        (byte) 27,
        (byte) 170,
        (byte) 214,
        (byte) 57,
        (byte) 218,
        (byte) 220,
        (byte) 244,
        (byte) 170,
        (byte) 123,
        (byte) 27,
        (byte) 72,
        (byte) 231
      };
      byte[] numArray5 = new byte[55]
      {
        (byte) 229,
        (byte) 121,
        (byte) 73,
        (byte) 168,
        (byte) 39,
        (byte) 208 /*0xD0*/,
        (byte) 169,
        (byte) 164,
        (byte) 127 /*0x7F*/,
        (byte) 204,
        (byte) 250,
        (byte) 61,
        (byte) 17,
        (byte) 210,
        (byte) 109,
        (byte) 5,
        (byte) 139,
        (byte) 64 /*0x40*/,
        (byte) 246,
        (byte) 209,
        (byte) 246,
        (byte) 227,
        (byte) 60,
        (byte) 22,
        (byte) 26,
        (byte) 52,
        (byte) 132,
        (byte) 30,
        (byte) 42,
        (byte) 114,
        (byte) 4,
        (byte) 237,
        (byte) 95,
        (byte) 108,
        (byte) 222,
        (byte) 143,
        (byte) 36,
        (byte) 8,
        (byte) 22,
        (byte) 175,
        (byte) 1,
        (byte) 201,
        (byte) 247,
        (byte) 32 /*0x20*/,
        (byte) 16 /*0x10*/,
        (byte) 253,
        (byte) 215,
        (byte) 81,
        (byte) 58,
        (byte) 16 /*0x10*/,
        (byte) 22,
        (byte) 237,
        (byte) 56,
        (byte) 111,
        (byte) 11
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[37]
      {
        (byte) 96 /*0x60*/,
        (byte) 198,
        (byte) 177,
        (byte) 165,
        (byte) 197,
        (byte) 64 /*0x40*/,
        (byte) 158,
        (byte) 56,
        (byte) 184,
        (byte) 88,
        (byte) 254,
        (byte) 11,
        (byte) 97,
        (byte) 248,
        (byte) 7,
        (byte) 94,
        (byte) 245,
        (byte) 237,
        (byte) 209,
        (byte) 180,
        (byte) 44,
        (byte) 177,
        (byte) 17,
        (byte) 142,
        (byte) 30,
        (byte) 43,
        (byte) 176 /*0xB0*/,
        (byte) 35,
        (byte) 107,
        (byte) 158,
        (byte) 157,
        (byte) 3,
        (byte) 31 /*0x1F*/,
        (byte) 216,
        (byte) 66,
        (byte) 195,
        (byte) 223
      };
      byte[] numArray7 = new byte[37];
      numArray7[17] = (byte) 83;
      numArray7[1] = (byte) 54;
      numArray7[2] = (byte) 98;
      numArray7[3] = (byte) 239;
      numArray7[4] = (byte) 39;
      numArray7[12] = (byte) 99;
      numArray7[24] = (byte) 100;
      numArray7[36] = (byte) 254;
      numArray7[20] = (byte) 143;
      numArray7[23] = (byte) 191;
      numArray7[7] = (byte) 118;
      numArray7[11] = (byte) 203;
      numArray7[26] = (byte) 228;
      numArray7[8] = (byte) 165;
      numArray7[14] = (byte) 138;
      numArray7[31 /*0x1F*/] = (byte) 244;
      numArray7[16 /*0x10*/] = (byte) 215;
      numArray7[10] = (byte) 217;
      numArray7[27] = (byte) 100;
      numArray7[19] = (byte) 61;
      numArray7[15] = (byte) 77;
      numArray7[21] = (byte) 149;
      numArray7[22] = (byte) 166;
      numArray7[0] = (byte) 252;
      numArray7[18] = (byte) 184;
      numArray7[25] = (byte) 126;
      numArray7[13] = (byte) 229;
      numArray7[5] = (byte) 117;
      numArray7[34] = (byte) 57;
      numArray7[29] = (byte) 42;
      numArray7[30] = (byte) 172;
      numArray7[28] = (byte) 251;
      numArray7[32 /*0x20*/] = (byte) 244;
      numArray7[33] = (byte) 177;
      numArray7[6] = (byte) 117;
      numArray7[35] = (byte) 115;
      numArray7[9] = (byte) 125;
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 37);
      for (int index = 0; index < 37; ++index)
        numArray1[index + 110] ^= numArray7[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray8 = new byte[147];
    byte[] numArray9 = new byte[55]
    {
      (byte) 147,
      (byte) 83,
      (byte) 78,
      (byte) 176 /*0xB0*/,
      (byte) 180,
      (byte) 129,
      (byte) 78,
      (byte) 120,
      (byte) 65,
      (byte) 236,
      (byte) 18,
      (byte) 182,
      (byte) 148,
      (byte) 14,
      (byte) 188,
      (byte) 179,
      (byte) 140,
      (byte) 233,
      (byte) 136,
      (byte) 22,
      (byte) 215,
      (byte) 194,
      (byte) 62,
      (byte) 227,
      (byte) 240 /*0xF0*/,
      (byte) 70,
      (byte) 93,
      (byte) 158,
      (byte) 107,
      (byte) 149,
      (byte) 189,
      (byte) 92,
      (byte) 216,
      (byte) 238,
      (byte) 32 /*0x20*/,
      (byte) 207,
      (byte) 57,
      (byte) 233,
      (byte) 219,
      (byte) 249,
      (byte) 126,
      (byte) 161,
      (byte) 121,
      (byte) 14,
      (byte) 218,
      (byte) 211,
      (byte) 33,
      (byte) 54,
      (byte) 194,
      (byte) 6,
      (byte) 57,
      (byte) 111,
      (byte) 39,
      (byte) 51,
      (byte) 232
    };
    byte[] numArray10 = new byte[55];
    numArray10[51] = (byte) 70;
    numArray10[41] = (byte) 156;
    numArray10[47] = (byte) 11;
    numArray10[4] = (byte) 15;
    numArray10[16 /*0x10*/] = (byte) 109;
    numArray10[33] = (byte) 6;
    numArray10[6] = (byte) 37;
    numArray10[7] = (byte) 221;
    numArray10[34] = (byte) 218;
    numArray10[38] = (byte) 87;
    numArray10[5] = (byte) 136;
    numArray10[11] = (byte) 16 /*0x10*/;
    numArray10[12] = (byte) 98;
    numArray10[28] = (byte) 114;
    numArray10[14] = (byte) 177;
    numArray10[26] = (byte) 40;
    numArray10[50] = (byte) 211;
    numArray10[9] = (byte) 150;
    numArray10[1] = (byte) 83;
    numArray10[49] = (byte) 6;
    numArray10[36] = (byte) 73;
    numArray10[21] = (byte) 229;
    numArray10[22] = (byte) 102;
    numArray10[10] = (byte) 151;
    numArray10[24] = (byte) 108;
    numArray10[3] = (byte) 61;
    numArray10[13] = (byte) 47;
    numArray10[0] = (byte) 63 /*0x3F*/;
    numArray10[2] = (byte) 224 /*0xE0*/;
    numArray10[29] = (byte) 95;
    numArray10[30] = (byte) 138;
    numArray10[20] = (byte) 158;
    numArray10[32 /*0x20*/] = (byte) 214;
    numArray10[27] = (byte) 218;
    numArray10[15] = (byte) 228;
    numArray10[35] = (byte) 43;
    numArray10[17] = (byte) 40;
    numArray10[37] = (byte) 240 /*0xF0*/;
    numArray10[52] = (byte) 218;
    numArray10[39] = (byte) 53;
    numArray10[54] = (byte) 174;
    numArray10[8] = (byte) 26;
    numArray10[42] = (byte) 211;
    numArray10[43] = (byte) 197;
    numArray10[44] = (byte) 175;
    numArray10[53] = (byte) 71;
    numArray10[46] = (byte) 212;
    numArray10[23] = (byte) 105;
    numArray10[48 /*0x30*/] = (byte) 228;
    numArray10[45] = (byte) 140;
    numArray10[18] = (byte) 70;
    numArray10[19] = (byte) 192 /*0xC0*/;
    numArray10[40] = (byte) 202;
    numArray10[31 /*0x1F*/] = (byte) 179;
    numArray10[25] = (byte) 139;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray8, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index] ^= numArray10[index];
    byte[] numArray11 = new byte[55];
    numArray11[16 /*0x10*/] = (byte) 185;
    numArray11[1] = (byte) 162;
    numArray11[0] = (byte) 98;
    numArray11[38] = (byte) 249;
    numArray11[4] = (byte) 212;
    numArray11[30] = (byte) 9;
    numArray11[32 /*0x20*/] = (byte) 199;
    numArray11[17] = (byte) 201;
    numArray11[8] = (byte) 128 /*0x80*/;
    numArray11[9] = (byte) 57;
    numArray11[15] = (byte) 159;
    numArray11[11] = (byte) 157;
    numArray11[49] = (byte) 193;
    numArray11[14] = (byte) 121;
    numArray11[53] = (byte) 11;
    numArray11[50] = (byte) 223;
    numArray11[54] = (byte) 148;
    numArray11[5] = (byte) 168;
    numArray11[19] = (byte) 124;
    numArray11[20] = (byte) 80 /*0x50*/;
    numArray11[29] = (byte) 156;
    numArray11[48 /*0x30*/] = (byte) 28;
    numArray11[21] = (byte) 228;
    numArray11[3] = (byte) 252;
    numArray11[24] = (byte) 222;
    numArray11[25] = (byte) 5;
    numArray11[26] = (byte) 216;
    numArray11[27] = (byte) 198;
    numArray11[28] = (byte) 237;
    numArray11[10] = (byte) 158;
    numArray11[34] = (byte) 107;
    numArray11[31 /*0x1F*/] = (byte) 84;
    numArray11[23] = (byte) 168;
    numArray11[7] = (byte) 224 /*0xE0*/;
    numArray11[33] = (byte) 213;
    numArray11[35] = (byte) 134;
    numArray11[36] = (byte) 37;
    numArray11[37] = (byte) 216;
    numArray11[51] = (byte) 209;
    numArray11[39] = (byte) 235;
    numArray11[40] = (byte) 125;
    numArray11[41] = byte.MaxValue;
    numArray11[42] = (byte) 163;
    numArray11[43] = (byte) 188;
    numArray11[13] = (byte) 173;
    numArray11[45] = (byte) 171;
    numArray11[18] = (byte) 161;
    numArray11[47] = (byte) 222;
    numArray11[2] = (byte) 72;
    numArray11[12] = (byte) 49;
    numArray11[46] = (byte) 165;
    numArray11[6] = (byte) 79;
    numArray11[52] = (byte) 16 /*0x10*/;
    numArray11[44] = (byte) 222;
    numArray11[22] = (byte) 40;
    byte[] numArray12 = new byte[55]
    {
      (byte) 171,
      (byte) 181,
      (byte) 217,
      (byte) 197,
      (byte) 24,
      (byte) 239,
      (byte) 24,
      (byte) 29,
      (byte) 2,
      (byte) 137,
      (byte) 19,
      (byte) 151,
      (byte) 133,
      (byte) 53,
      (byte) 227,
      (byte) 31 /*0x1F*/,
      (byte) 92,
      (byte) 64 /*0x40*/,
      (byte) 247,
      (byte) 168,
      (byte) 92,
      (byte) 180,
      (byte) 193,
      (byte) 68,
      (byte) 110,
      (byte) 201,
      (byte) 116,
      (byte) 176 /*0xB0*/,
      (byte) 121,
      (byte) 139,
      (byte) 222,
      (byte) 71,
      (byte) 64 /*0x40*/,
      (byte) 147,
      (byte) 142,
      (byte) 160 /*0xA0*/,
      (byte) 143,
      (byte) 99,
      (byte) 229,
      (byte) 173,
      (byte) 173,
      (byte) 160 /*0xA0*/,
      (byte) 95,
      (byte) 11,
      (byte) 181,
      (byte) 110,
      (byte) 111,
      (byte) 170,
      (byte) 198,
      (byte) 91,
      (byte) 113,
      (byte) 218,
      (byte) 119,
      (byte) 204,
      (byte) 151
    };
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray8, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index + 55] ^= numArray12[index];
    byte[] numArray13 = new byte[37]
    {
      (byte) 214,
      (byte) 235,
      (byte) 110,
      (byte) 214,
      (byte) 223,
      (byte) 199,
      (byte) 39,
      (byte) 247,
      (byte) 6,
      (byte) 25,
      (byte) 26,
      (byte) 84,
      (byte) 230,
      (byte) 246,
      (byte) 38,
      (byte) 166,
      (byte) 155,
      (byte) 10,
      (byte) 68,
      (byte) 129,
      (byte) 223,
      (byte) 53,
      (byte) 93,
      (byte) 22,
      (byte) 92,
      (byte) 128 /*0x80*/,
      (byte) 227,
      (byte) 190,
      (byte) 54,
      (byte) 233,
      (byte) 174,
      (byte) 253,
      (byte) 176 /*0xB0*/,
      (byte) 254,
      (byte) 173,
      (byte) 206,
      (byte) 187
    };
    byte[] numArray14 = new byte[37];
    numArray14[23] = (byte) 35;
    numArray14[0] = (byte) 198;
    numArray14[13] = (byte) 1;
    numArray14[3] = (byte) 59;
    numArray14[27] = (byte) 190;
    numArray14[5] = (byte) 196;
    numArray14[8] = (byte) 64 /*0x40*/;
    numArray14[29] = (byte) 143;
    numArray14[7] = (byte) 60;
    numArray14[32 /*0x20*/] = (byte) 95;
    numArray14[26] = (byte) 227;
    numArray14[11] = (byte) 230;
    numArray14[10] = (byte) 31 /*0x1F*/;
    numArray14[16 /*0x10*/] = (byte) 59;
    numArray14[36] = (byte) 174;
    numArray14[35] = (byte) 248;
    numArray14[33] = (byte) 214;
    numArray14[17] = (byte) 59;
    numArray14[18] = (byte) 192 /*0xC0*/;
    numArray14[19] = (byte) 139;
    numArray14[20] = (byte) 179;
    numArray14[21] = (byte) 152;
    numArray14[22] = (byte) 237;
    numArray14[24] = (byte) 84;
    numArray14[31 /*0x1F*/] = (byte) 17;
    numArray14[25] = (byte) 197;
    numArray14[15] = (byte) 57;
    numArray14[1] = (byte) 206;
    numArray14[28] = (byte) 120;
    numArray14[14] = (byte) 173;
    numArray14[12] = (byte) 254;
    numArray14[4] = (byte) 34;
    numArray14[30] = (byte) 17;
    numArray14[9] = (byte) 125;
    numArray14[34] = (byte) 87;
    numArray14[6] = (byte) 149;
    numArray14[2] = (byte) 22;
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray8, 110, 37);
    for (int index = 0; index < 37; ++index)
      numArray8[index + 110] ^= numArray14[index];
    byte[] numArray15 = new byte[23];
    byte[] response = new byte[23];
    Array.Copy((Array) sc_12780.sspq, 458, (Array) numArray15, 0, 23);
    key.Query(true, 335, numArray15, response);
    Array.Copy((Array) sc_12780.sspr, 458, (Array) numArray15, 0, 23);
    for (int index = 0; index < numArray15.Length; ++index)
    {
      if ((int) numArray15[index] != (int) response[index])
      {
        key.TagValue = (int) response[index];
        break;
      }
    }
    return Encoding.UTF8.GetString(numArray8);
  }

  internal static string ssp_appserver_12813()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[117];
      byte[] numArray2 = new byte[55]
      {
        (byte) 8,
        (byte) 129,
        (byte) 112 /*0x70*/,
        (byte) 104,
        (byte) 10,
        (byte) 254,
        (byte) 211,
        (byte) 228,
        (byte) 248,
        (byte) 235,
        (byte) 112 /*0x70*/,
        (byte) 101,
        (byte) 33,
        (byte) 81,
        (byte) 132,
        (byte) 229,
        (byte) 241,
        (byte) 190,
        (byte) 30,
        (byte) 176 /*0xB0*/,
        (byte) 25,
        (byte) 60,
        (byte) 22,
        (byte) 134,
        (byte) 140,
        (byte) 66,
        (byte) 159,
        (byte) 185,
        (byte) 119,
        (byte) 54,
        (byte) 81,
        (byte) 213,
        (byte) 113,
        (byte) 227,
        (byte) 138,
        (byte) 99,
        (byte) 106,
        (byte) 194,
        (byte) 161,
        byte.MaxValue,
        (byte) 32 /*0x20*/,
        (byte) 234,
        (byte) 19,
        (byte) 85,
        (byte) 243,
        (byte) 101,
        (byte) 147,
        (byte) 15,
        (byte) 171,
        (byte) 237,
        (byte) 127 /*0x7F*/,
        (byte) 145,
        (byte) 243,
        (byte) 242,
        (byte) 65
      };
      byte[] numArray3 = new byte[55];
      numArray3[33] = (byte) 52;
      numArray3[1] = (byte) 161;
      numArray3[8] = (byte) 181;
      numArray3[3] = (byte) 157;
      numArray3[18] = (byte) 161;
      numArray3[52] = (byte) 94;
      numArray3[6] = (byte) 243;
      numArray3[37] = (byte) 154;
      numArray3[0] = (byte) 210;
      numArray3[9] = (byte) 243;
      numArray3[2] = (byte) 119;
      numArray3[11] = (byte) 163;
      numArray3[23] = (byte) 0;
      numArray3[13] = (byte) 107;
      numArray3[54] = (byte) 69;
      numArray3[10] = (byte) 158;
      numArray3[16 /*0x10*/] = (byte) 125;
      numArray3[27] = (byte) 129;
      numArray3[45] = (byte) 214;
      numArray3[19] = (byte) 143;
      numArray3[35] = (byte) 78;
      numArray3[15] = (byte) 46;
      numArray3[17] = (byte) 142;
      numArray3[14] = (byte) 70;
      numArray3[4] = (byte) 218;
      numArray3[22] = (byte) 87;
      numArray3[26] = (byte) 126;
      numArray3[43] = (byte) 239;
      numArray3[28] = (byte) 64 /*0x40*/;
      numArray3[30] = (byte) 245;
      numArray3[25] = (byte) 18;
      numArray3[31 /*0x1F*/] = (byte) 180;
      numArray3[32 /*0x20*/] = (byte) 176 /*0xB0*/;
      numArray3[41] = (byte) 183;
      numArray3[34] = (byte) 50;
      numArray3[7] = (byte) 57;
      numArray3[5] = (byte) 12;
      numArray3[21] = (byte) 118;
      numArray3[38] = (byte) 153;
      numArray3[39] = byte.MaxValue;
      numArray3[40] = (byte) 174;
      numArray3[47] = (byte) 240 /*0xF0*/;
      numArray3[42] = (byte) 208 /*0xD0*/;
      numArray3[20] = (byte) 162;
      numArray3[44] = (byte) 232;
      numArray3[36] = (byte) 40;
      numArray3[46] = (byte) 254;
      numArray3[24] = (byte) 48 /*0x30*/;
      numArray3[48 /*0x30*/] = (byte) 157;
      numArray3[12] = (byte) 10;
      numArray3[50] = (byte) 31 /*0x1F*/;
      numArray3[51] = (byte) 125;
      numArray3[49] = (byte) 210;
      numArray3[53] = (byte) 178;
      numArray3[29] = (byte) 34;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 86,
        (byte) 180,
        (byte) 24,
        (byte) 206,
        (byte) 11,
        (byte) 227,
        (byte) 12,
        (byte) 217,
        (byte) 185,
        (byte) 116,
        (byte) 155,
        (byte) 8,
        (byte) 39,
        (byte) 251,
        (byte) 72,
        (byte) 67,
        (byte) 126,
        (byte) 138,
        (byte) 176 /*0xB0*/,
        (byte) 215,
        (byte) 5,
        (byte) 59,
        (byte) 181,
        (byte) 89,
        (byte) 72,
        (byte) 131,
        (byte) 23,
        (byte) 101,
        (byte) 75,
        (byte) 13,
        (byte) 29,
        (byte) 143,
        (byte) 27,
        (byte) 79,
        (byte) 41,
        (byte) 49,
        (byte) 192 /*0xC0*/,
        (byte) 210,
        (byte) 250,
        (byte) 153,
        (byte) 68,
        (byte) 226,
        (byte) 157,
        (byte) 252,
        (byte) 186,
        (byte) 63 /*0x3F*/,
        (byte) 99,
        (byte) 118,
        (byte) 7,
        (byte) 49,
        (byte) 85,
        (byte) 215,
        (byte) 97,
        (byte) 203,
        (byte) 164
      };
      byte[] numArray5 = new byte[55]
      {
        (byte) 181,
        (byte) 239,
        (byte) 83,
        (byte) 221,
        (byte) 141,
        (byte) 20,
        (byte) 42,
        (byte) 227,
        (byte) 60,
        (byte) 123,
        (byte) 184,
        (byte) 232,
        (byte) 175,
        (byte) 28,
        (byte) 11,
        (byte) 77,
        (byte) 59,
        (byte) 219,
        (byte) 29,
        (byte) 185,
        (byte) 125,
        (byte) 140,
        (byte) 26,
        (byte) 185,
        (byte) 173,
        (byte) 58,
        (byte) 194,
        (byte) 105,
        (byte) 65,
        (byte) 174,
        (byte) 186,
        (byte) 183,
        (byte) 199,
        (byte) 123,
        (byte) 74,
        (byte) 217,
        (byte) 7,
        (byte) 164,
        (byte) 74,
        (byte) 44,
        (byte) 225,
        (byte) 181,
        (byte) 32 /*0x20*/,
        (byte) 120,
        (byte) 246,
        (byte) 98,
        (byte) 6,
        (byte) 233,
        (byte) 173,
        (byte) 231,
        (byte) 183,
        (byte) 156,
        (byte) 46,
        (byte) 149,
        (byte) 155
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[7]
      {
        (byte) 190,
        (byte) 133,
        (byte) 136,
        (byte) 204,
        (byte) 166,
        (byte) 142,
        (byte) 172
      };
      byte[] numArray7 = new byte[7];
      numArray7[4] = (byte) 63 /*0x3F*/;
      numArray7[2] = (byte) 151;
      numArray7[3] = (byte) 245;
      numArray7[5] = (byte) 115;
      numArray7[1] = (byte) 29;
      numArray7[0] = (byte) 103;
      numArray7[6] = (byte) 177;
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 7);
      for (int index = 0; index < 7; ++index)
        numArray1[index + 110] ^= numArray7[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray8 = new byte[117];
    byte[] numArray9 = new byte[55]
    {
      (byte) 146,
      (byte) 35,
      (byte) 157,
      (byte) 185,
      (byte) 159,
      (byte) 8,
      (byte) 32 /*0x20*/,
      (byte) 160 /*0xA0*/,
      (byte) 94,
      (byte) 25,
      (byte) 184,
      (byte) 70,
      (byte) 45,
      (byte) 79,
      (byte) 171,
      (byte) 14,
      (byte) 50,
      (byte) 1,
      (byte) 76,
      (byte) 114,
      (byte) 205,
      (byte) 158,
      (byte) 83,
      (byte) 241,
      (byte) 165,
      (byte) 174,
      (byte) 82,
      (byte) 139,
      (byte) 165,
      (byte) 240 /*0xF0*/,
      (byte) 174,
      (byte) 168,
      (byte) 72,
      (byte) 74,
      (byte) 56,
      (byte) 128 /*0x80*/,
      (byte) 125,
      (byte) 69,
      (byte) 31 /*0x1F*/,
      (byte) 125,
      (byte) 103,
      (byte) 44,
      (byte) 72,
      (byte) 116,
      (byte) 244,
      (byte) 17,
      (byte) 222,
      (byte) 157,
      (byte) 45,
      (byte) 195,
      (byte) 229,
      (byte) 129,
      (byte) 95,
      (byte) 28,
      (byte) 74
    };
    byte[] numArray10 = new byte[55]
    {
      (byte) 13,
      (byte) 1,
      (byte) 73,
      (byte) 98,
      (byte) 97,
      (byte) 113,
      (byte) 108,
      (byte) 197,
      (byte) 119,
      (byte) 230,
      (byte) 241,
      (byte) 248,
      (byte) 4,
      (byte) 234,
      (byte) 201,
      (byte) 243,
      (byte) 213,
      (byte) 63 /*0x3F*/,
      (byte) 171,
      (byte) 53,
      (byte) 243,
      (byte) 85,
      (byte) 146,
      (byte) 180,
      (byte) 46,
      (byte) 146,
      (byte) 146,
      (byte) 243,
      (byte) 188,
      (byte) 190,
      (byte) 86,
      (byte) 231,
      (byte) 233,
      (byte) 224 /*0xE0*/,
      (byte) 11,
      (byte) 152,
      (byte) 45,
      (byte) 194,
      (byte) 189,
      (byte) 75,
      (byte) 41,
      (byte) 169,
      (byte) 126,
      (byte) 142,
      (byte) 22,
      (byte) 26,
      (byte) 97,
      (byte) 79,
      (byte) 5,
      (byte) 174,
      (byte) 248,
      (byte) 3,
      (byte) 182,
      (byte) 111,
      (byte) 115
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray8, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index] ^= numArray10[index];
    byte[] numArray11 = new byte[55]
    {
      (byte) 172,
      (byte) 19,
      (byte) 241,
      (byte) 181,
      (byte) 149,
      (byte) 251,
      (byte) 109,
      (byte) 106,
      (byte) 15,
      (byte) 253,
      (byte) 226,
      (byte) 22,
      (byte) 42,
      (byte) 144 /*0x90*/,
      (byte) 236,
      (byte) 226,
      (byte) 207,
      (byte) 10,
      (byte) 160 /*0xA0*/,
      (byte) 86,
      (byte) 223,
      (byte) 252,
      (byte) 148,
      (byte) 208 /*0xD0*/,
      (byte) 180,
      (byte) 94,
      (byte) 225,
      (byte) 11,
      (byte) 45,
      (byte) 72,
      (byte) 43,
      (byte) 0,
      (byte) 68,
      (byte) 177,
      (byte) 209,
      (byte) 50,
      (byte) 46,
      (byte) 199,
      (byte) 210,
      (byte) 49,
      (byte) 218,
      (byte) 9,
      (byte) 228,
      (byte) 19,
      (byte) 251,
      (byte) 135,
      (byte) 2,
      (byte) 50,
      (byte) 248,
      (byte) 158,
      (byte) 144 /*0x90*/,
      (byte) 17,
      (byte) 169,
      (byte) 157,
      (byte) 240 /*0xF0*/
    };
    byte[] numArray12 = new byte[55]
    {
      (byte) 122,
      (byte) 66,
      (byte) 47,
      (byte) 130,
      (byte) 70,
      (byte) 157,
      (byte) 45,
      (byte) 246,
      (byte) 78,
      (byte) 14,
      (byte) 96 /*0x60*/,
      (byte) 246,
      (byte) 92,
      (byte) 171,
      (byte) 115,
      (byte) 168,
      (byte) 129,
      (byte) 219,
      (byte) 231,
      (byte) 243,
      (byte) 163,
      (byte) 134,
      (byte) 19,
      (byte) 50,
      (byte) 141,
      (byte) 94,
      (byte) 71,
      (byte) 112 /*0x70*/,
      (byte) 55,
      (byte) 70,
      (byte) 198,
      (byte) 167,
      (byte) 20,
      (byte) 209,
      (byte) 124,
      (byte) 167,
      (byte) 132,
      (byte) 177,
      (byte) 176 /*0xB0*/,
      (byte) 237,
      (byte) 33,
      (byte) 33,
      (byte) 253,
      (byte) 55,
      (byte) 22,
      (byte) 53,
      (byte) 9,
      (byte) 235,
      (byte) 115,
      (byte) 80 /*0x50*/,
      (byte) 47,
      (byte) 102,
      (byte) 45,
      (byte) 174,
      (byte) 75
    };
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray8, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index + 55] ^= numArray12[index];
    byte[] numArray13 = new byte[7]
    {
      (byte) 33,
      (byte) 1,
      (byte) 55,
      (byte) 25,
      (byte) 98,
      (byte) 61,
      (byte) 198
    };
    byte[] numArray14 = new byte[7]
    {
      (byte) 193,
      (byte) 47,
      (byte) 230,
      (byte) 39,
      (byte) 5,
      (byte) 52,
      (byte) 111
    };
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray8, 110, 7);
    for (int index = 0; index < 7; ++index)
      numArray8[index + 110] ^= numArray14[index];
    return Encoding.UTF8.GetString(numArray8);
  }

  internal static string ssp_appserver_12814()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[87];
      byte[] numArray2 = new byte[55];
      numArray2[50] = (byte) 14;
      numArray2[7] = (byte) 213;
      numArray2[31 /*0x1F*/] = (byte) 25;
      numArray2[2] = (byte) 179;
      numArray2[4] = (byte) 56;
      numArray2[46] = (byte) 219;
      numArray2[6] = (byte) 123;
      numArray2[38] = (byte) 217;
      numArray2[39] = (byte) 57;
      numArray2[9] = (byte) 129;
      numArray2[40] = (byte) 33;
      numArray2[11] = (byte) 45;
      numArray2[12] = (byte) 91;
      numArray2[13] = (byte) 175;
      numArray2[0] = (byte) 191;
      numArray2[10] = (byte) 212;
      numArray2[16 /*0x10*/] = (byte) 184;
      numArray2[1] = (byte) 186;
      numArray2[42] = (byte) 151;
      numArray2[43] = (byte) 254;
      numArray2[20] = (byte) 193;
      numArray2[21] = (byte) 33;
      numArray2[52] = (byte) 138;
      numArray2[8] = (byte) 24;
      numArray2[24] = (byte) 198;
      numArray2[29] = (byte) 29;
      numArray2[26] = (byte) 23;
      numArray2[27] = (byte) 120;
      numArray2[44] = (byte) 76;
      numArray2[47] = (byte) 105;
      numArray2[30] = (byte) 65;
      numArray2[23] = (byte) 13;
      numArray2[17] = (byte) 25;
      numArray2[33] = (byte) 138;
      numArray2[34] = (byte) 115;
      numArray2[35] = (byte) 62;
      numArray2[36] = (byte) 234;
      numArray2[5] = (byte) 111;
      numArray2[15] = (byte) 142;
      numArray2[19] = (byte) 167;
      numArray2[32 /*0x20*/] = (byte) 181;
      numArray2[41] = (byte) 26;
      numArray2[25] = (byte) 155;
      numArray2[14] = (byte) 18;
      numArray2[28] = (byte) 204;
      numArray2[45] = (byte) 211;
      numArray2[48 /*0x30*/] = (byte) 79;
      numArray2[49] = (byte) 174;
      numArray2[3] = (byte) 7;
      numArray2[37] = (byte) 136;
      numArray2[22] = (byte) 178;
      numArray2[51] = (byte) 184;
      numArray2[18] = (byte) 87;
      numArray2[53] = (byte) 42;
      numArray2[54] = (byte) 183;
      byte[] numArray3 = new byte[55];
      numArray3[41] = (byte) 183;
      numArray3[1] = (byte) 76;
      numArray3[31 /*0x1F*/] = (byte) 244;
      numArray3[47] = (byte) 27;
      numArray3[4] = (byte) 235;
      numArray3[5] = (byte) 40;
      numArray3[16 /*0x10*/] = (byte) 125;
      numArray3[7] = (byte) 179;
      numArray3[0] = (byte) 235;
      numArray3[9] = (byte) 52;
      numArray3[2] = (byte) 228;
      numArray3[11] = (byte) 185;
      numArray3[34] = (byte) 183;
      numArray3[14] = (byte) 235;
      numArray3[44] = (byte) 106;
      numArray3[3] = (byte) 98;
      numArray3[50] = (byte) 70;
      numArray3[6] = (byte) 133;
      numArray3[18] = (byte) 113;
      numArray3[45] = (byte) 202;
      numArray3[51] = (byte) 129;
      numArray3[12] = (byte) 131;
      numArray3[22] = (byte) 9;
      numArray3[27] = (byte) 34;
      numArray3[24] = (byte) 58;
      numArray3[25] = (byte) 204;
      numArray3[26] = (byte) 164;
      numArray3[48 /*0x30*/] = (byte) 103;
      numArray3[13] = (byte) 184;
      numArray3[19] = (byte) 87;
      numArray3[30] = (byte) 143;
      numArray3[49] = (byte) 138;
      numArray3[39] = (byte) 168;
      numArray3[53] = (byte) 177;
      numArray3[36] = (byte) 118;
      numArray3[35] = (byte) 104;
      numArray3[17] = (byte) 206;
      numArray3[8] = (byte) 178;
      numArray3[38] = (byte) 175;
      numArray3[42] = (byte) 217;
      numArray3[40] = (byte) 153;
      numArray3[29] = (byte) 99;
      numArray3[33] = (byte) 111;
      numArray3[43] = (byte) 248;
      numArray3[21] = byte.MaxValue;
      numArray3[28] = (byte) 210;
      numArray3[46] = (byte) 24;
      numArray3[37] = (byte) 234;
      numArray3[10] = (byte) 107;
      numArray3[52] = (byte) 43;
      numArray3[32 /*0x20*/] = (byte) 104;
      numArray3[20] = (byte) 88;
      numArray3[15] = (byte) 74;
      numArray3[23] = (byte) 167;
      numArray3[54] = (byte) 87;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[32 /*0x20*/];
      numArray4[1] = (byte) 183;
      numArray4[15] = (byte) 161;
      numArray4[28] = (byte) 154;
      numArray4[12] = (byte) 186;
      numArray4[4] = (byte) 47;
      numArray4[5] = (byte) 214;
      numArray4[6] = (byte) 186;
      numArray4[17] = (byte) 37;
      numArray4[8] = (byte) 8;
      numArray4[10] = (byte) 60;
      numArray4[13] = (byte) 2;
      numArray4[11] = (byte) 164;
      numArray4[24] = (byte) 185;
      numArray4[19] = (byte) 60;
      numArray4[14] = (byte) 240 /*0xF0*/;
      numArray4[21] = (byte) 200;
      numArray4[27] = (byte) 57;
      numArray4[9] = (byte) 6;
      numArray4[16 /*0x10*/] = (byte) 5;
      numArray4[2] = (byte) 39;
      numArray4[22] = (byte) 39;
      numArray4[18] = (byte) 108;
      numArray4[23] = (byte) 168;
      numArray4[20] = (byte) 238;
      numArray4[3] = (byte) 41;
      numArray4[25] = (byte) 206;
      numArray4[26] = (byte) 105;
      numArray4[0] = (byte) 183;
      numArray4[7] = (byte) 166;
      numArray4[29] = (byte) 243;
      numArray4[30] = (byte) 168;
      numArray4[31 /*0x1F*/] = (byte) 69;
      byte[] numArray5 = new byte[32 /*0x20*/];
      numArray5[30] = (byte) 2;
      numArray5[31 /*0x1F*/] = (byte) 59;
      numArray5[18] = (byte) 2;
      numArray5[2] = (byte) 69;
      numArray5[29] = (byte) 92;
      numArray5[5] = (byte) 215;
      numArray5[6] = (byte) 102;
      numArray5[7] = (byte) 162;
      numArray5[15] = (byte) 7;
      numArray5[14] = (byte) 154;
      numArray5[28] = (byte) 33;
      numArray5[21] = (byte) 96 /*0x60*/;
      numArray5[17] = (byte) 63 /*0x3F*/;
      numArray5[13] = (byte) 120;
      numArray5[1] = (byte) 109;
      numArray5[11] = (byte) 223;
      numArray5[16 /*0x10*/] = (byte) 111;
      numArray5[27] = (byte) 97;
      numArray5[23] = (byte) 171;
      numArray5[0] = (byte) 114;
      numArray5[20] = (byte) 85;
      numArray5[25] = (byte) 161;
      numArray5[22] = (byte) 19;
      numArray5[10] = (byte) 193;
      numArray5[24] = (byte) 152;
      numArray5[3] = (byte) 29;
      numArray5[26] = (byte) 47;
      numArray5[19] = (byte) 87;
      numArray5[12] = (byte) 29;
      numArray5[8] = (byte) 97;
      numArray5[4] = (byte) 24;
      numArray5[9] = (byte) 192 /*0xC0*/;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 32 /*0x20*/);
      for (int index = 0; index < 32 /*0x20*/; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[87];
    byte[] numArray7 = new byte[55]
    {
      (byte) 217,
      (byte) 27,
      (byte) 237,
      (byte) 76,
      (byte) 86,
      (byte) 107,
      (byte) 50,
      (byte) 4,
      (byte) 243,
      (byte) 163,
      (byte) 225,
      (byte) 131,
      (byte) 209,
      (byte) 167,
      (byte) 216,
      (byte) 182,
      (byte) 171,
      (byte) 77,
      (byte) 70,
      (byte) 75,
      (byte) 204,
      (byte) 193,
      (byte) 81,
      (byte) 32 /*0x20*/,
      (byte) 163,
      byte.MaxValue,
      (byte) 174,
      (byte) 89,
      (byte) 97,
      (byte) 55,
      (byte) 206,
      (byte) 58,
      (byte) 34,
      (byte) 180,
      (byte) 254,
      (byte) 123,
      (byte) 188,
      (byte) 117,
      (byte) 220,
      (byte) 90,
      (byte) 151,
      (byte) 141,
      (byte) 65,
      (byte) 17,
      (byte) 51,
      (byte) 52,
      (byte) 222,
      (byte) 18,
      (byte) 198,
      (byte) 250,
      (byte) 56,
      (byte) 41,
      (byte) 13,
      (byte) 253,
      (byte) 61
    };
    byte[] numArray8 = new byte[55]
    {
      (byte) 195,
      (byte) 144 /*0x90*/,
      (byte) 93,
      (byte) 93,
      (byte) 110,
      (byte) 23,
      (byte) 135,
      (byte) 84,
      (byte) 92,
      (byte) 148,
      (byte) 5,
      (byte) 14,
      (byte) 140,
      (byte) 72,
      (byte) 216,
      (byte) 249,
      (byte) 127 /*0x7F*/,
      (byte) 105,
      (byte) 200,
      (byte) 249,
      (byte) 12,
      (byte) 51,
      (byte) 251,
      (byte) 239,
      (byte) 34,
      (byte) 244,
      (byte) 35,
      (byte) 112 /*0x70*/,
      (byte) 143,
      (byte) 27,
      (byte) 185,
      (byte) 207,
      (byte) 76,
      (byte) 82,
      (byte) 52,
      (byte) 170,
      (byte) 62,
      (byte) 251,
      (byte) 14,
      (byte) 169,
      (byte) 215,
      (byte) 45,
      (byte) 71,
      (byte) 239,
      (byte) 147,
      (byte) 226,
      (byte) 183,
      (byte) 70,
      (byte) 245,
      (byte) 80 /*0x50*/,
      (byte) 239,
      (byte) 76,
      (byte) 16 /*0x10*/,
      (byte) 247,
      (byte) 183
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[32 /*0x20*/];
    numArray9[18] = (byte) 63 /*0x3F*/;
    numArray9[22] = (byte) 111;
    numArray9[2] = (byte) 87;
    numArray9[30] = (byte) 37;
    numArray9[29] = (byte) 44;
    numArray9[27] = byte.MaxValue;
    numArray9[12] = (byte) 92;
    numArray9[7] = (byte) 212;
    numArray9[15] = (byte) 112 /*0x70*/;
    numArray9[9] = (byte) 96 /*0x60*/;
    numArray9[3] = (byte) 189;
    numArray9[11] = (byte) 169;
    numArray9[14] = (byte) 53;
    numArray9[13] = (byte) 249;
    numArray9[10] = (byte) 29;
    numArray9[24] = (byte) 155;
    numArray9[16 /*0x10*/] = (byte) 235;
    numArray9[17] = (byte) 179;
    numArray9[26] = (byte) 103;
    numArray9[31 /*0x1F*/] = (byte) 118;
    numArray9[8] = (byte) 167;
    numArray9[21] = (byte) 237;
    numArray9[19] = (byte) 127 /*0x7F*/;
    numArray9[23] = (byte) 33;
    numArray9[6] = (byte) 187;
    numArray9[4] = (byte) 217;
    numArray9[1] = (byte) 168;
    numArray9[5] = (byte) 162;
    numArray9[28] = (byte) 165;
    numArray9[20] = (byte) 63 /*0x3F*/;
    numArray9[0] = (byte) 205;
    numArray9[25] = (byte) 146;
    byte[] numArray10 = new byte[32 /*0x20*/]
    {
      (byte) 17,
      (byte) 192 /*0xC0*/,
      (byte) 166,
      (byte) 234,
      (byte) 109,
      (byte) 117,
      (byte) 235,
      (byte) 155,
      (byte) 136,
      (byte) 91,
      (byte) 191,
      (byte) 143,
      (byte) 76,
      (byte) 147,
      (byte) 135,
      (byte) 50,
      (byte) 185,
      (byte) 177,
      (byte) 178,
      (byte) 72,
      (byte) 228,
      (byte) 193,
      (byte) 225,
      (byte) 39,
      (byte) 238,
      (byte) 76,
      (byte) 79,
      (byte) 134,
      (byte) 103,
      (byte) 195,
      (byte) 184,
      (byte) 63 /*0x3F*/
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 32 /*0x20*/);
    for (int index = 0; index < 32 /*0x20*/; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_12815()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[79];
      byte[] numArray2 = new byte[55];
      numArray2[3] = (byte) 78;
      numArray2[51] = (byte) 94;
      numArray2[11] = (byte) 50;
      numArray2[22] = (byte) 148;
      numArray2[4] = (byte) 245;
      numArray2[5] = (byte) 84;
      numArray2[45] = (byte) 239;
      numArray2[7] = (byte) 3;
      numArray2[8] = (byte) 97;
      numArray2[30] = (byte) 153;
      numArray2[46] = (byte) 30;
      numArray2[38] = (byte) 84;
      numArray2[12] = (byte) 40;
      numArray2[13] = (byte) 102;
      numArray2[14] = (byte) 251;
      numArray2[43] = (byte) 157;
      numArray2[37] = (byte) 211;
      numArray2[17] = (byte) 220;
      numArray2[18] = (byte) 119;
      numArray2[52] = (byte) 47;
      numArray2[20] = (byte) 39;
      numArray2[21] = (byte) 16 /*0x10*/;
      numArray2[1] = (byte) 104;
      numArray2[34] = (byte) 246;
      numArray2[24] = (byte) 123;
      numArray2[25] = (byte) 119;
      numArray2[49] = (byte) 66;
      numArray2[31 /*0x1F*/] = (byte) 25;
      numArray2[28] = (byte) 13;
      numArray2[26] = (byte) 175;
      numArray2[54] = (byte) 131;
      numArray2[23] = (byte) 79;
      numArray2[6] = (byte) 249;
      numArray2[15] = (byte) 236;
      numArray2[9] = (byte) 155;
      numArray2[27] = (byte) 201;
      numArray2[16 /*0x10*/] = (byte) 10;
      numArray2[0] = (byte) 10;
      numArray2[33] = (byte) 191;
      numArray2[39] = (byte) 169;
      numArray2[40] = (byte) 160 /*0xA0*/;
      numArray2[44] = (byte) 213;
      numArray2[42] = (byte) 125;
      numArray2[35] = (byte) 154;
      numArray2[29] = (byte) 16 /*0x10*/;
      numArray2[2] = (byte) 235;
      numArray2[10] = (byte) 96 /*0x60*/;
      numArray2[19] = (byte) 76;
      numArray2[48 /*0x30*/] = (byte) 192 /*0xC0*/;
      numArray2[41] = (byte) 134;
      numArray2[50] = (byte) 113;
      numArray2[47] = (byte) 212;
      numArray2[36] = (byte) 205;
      numArray2[53] = (byte) 118;
      numArray2[32 /*0x20*/] = (byte) 166;
      byte[] numArray3 = new byte[55]
      {
        (byte) 1,
        (byte) 45,
        (byte) 102,
        (byte) 102,
        (byte) 145,
        (byte) 147,
        (byte) 48 /*0x30*/,
        (byte) 251,
        (byte) 225,
        (byte) 97,
        (byte) 247,
        (byte) 127 /*0x7F*/,
        (byte) 158,
        (byte) 200,
        (byte) 202,
        byte.MaxValue,
        (byte) 114,
        (byte) 49,
        (byte) 203,
        (byte) 207,
        (byte) 129,
        (byte) 110,
        (byte) 188,
        (byte) 6,
        (byte) 8,
        (byte) 59,
        (byte) 198,
        (byte) 145,
        (byte) 58,
        (byte) 209,
        (byte) 198,
        (byte) 253,
        (byte) 143,
        (byte) 1,
        (byte) 251,
        (byte) 239,
        (byte) 198,
        (byte) 77,
        (byte) 81,
        (byte) 166,
        (byte) 249,
        (byte) 71,
        (byte) 238,
        (byte) 42,
        (byte) 122,
        (byte) 64 /*0x40*/,
        (byte) 36,
        (byte) 62,
        (byte) 42,
        (byte) 251,
        (byte) 128 /*0x80*/,
        (byte) 157,
        (byte) 5,
        (byte) 125,
        (byte) 96 /*0x60*/
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[24]
      {
        (byte) 34,
        (byte) 71,
        (byte) 36,
        (byte) 201,
        (byte) 39,
        (byte) 43,
        (byte) 178,
        (byte) 199,
        (byte) 93,
        (byte) 92,
        (byte) 44,
        (byte) 114,
        (byte) 155,
        (byte) 237,
        (byte) 53,
        (byte) 177,
        (byte) 200,
        (byte) 59,
        (byte) 15,
        (byte) 101,
        (byte) 91,
        (byte) 235,
        (byte) 40,
        (byte) 163
      };
      byte[] numArray5 = new byte[24];
      numArray5[10] = (byte) 13;
      numArray5[1] = (byte) 56;
      numArray5[8] = (byte) 138;
      numArray5[21] = (byte) 78;
      numArray5[13] = (byte) 197;
      numArray5[20] = (byte) 62;
      numArray5[18] = (byte) 179;
      numArray5[7] = (byte) 239;
      numArray5[0] = (byte) 52;
      numArray5[5] = (byte) 215;
      numArray5[6] = (byte) 239;
      numArray5[19] = (byte) 211;
      numArray5[12] = (byte) 75;
      numArray5[2] = (byte) 192 /*0xC0*/;
      numArray5[11] = (byte) 73;
      numArray5[4] = (byte) 232;
      numArray5[16 /*0x10*/] = (byte) 39;
      numArray5[17] = (byte) 149;
      numArray5[3] = (byte) 142;
      numArray5[14] = (byte) 16 /*0x10*/;
      numArray5[15] = (byte) 162;
      numArray5[9] = (byte) 14;
      numArray5[22] = (byte) 172;
      numArray5[23] = (byte) 3;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 24);
      for (int index = 0; index < 24; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[33];
      byte[] response = new byte[33];
      Array.Copy((Array) sc_12780.sspq, 481, (Array) numArray6, 0, 33);
      key.Query(true, 335, numArray6, response);
      Array.Copy((Array) sc_12780.sspr, 481, (Array) numArray6, 0, 33);
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
    byte[] numArray7 = new byte[79];
    byte[] numArray8 = new byte[55]
    {
      (byte) 212,
      (byte) 134,
      (byte) 147,
      (byte) 101,
      (byte) 215,
      (byte) 153,
      (byte) 49,
      (byte) 232,
      (byte) 202,
      (byte) 225,
      (byte) 43,
      (byte) 113,
      (byte) 91,
      (byte) 13,
      (byte) 183,
      (byte) 21,
      (byte) 15,
      (byte) 87,
      (byte) 136,
      (byte) 38,
      (byte) 12,
      (byte) 157,
      (byte) 42,
      (byte) 21,
      (byte) 251,
      (byte) 123,
      (byte) 253,
      (byte) 103,
      (byte) 208 /*0xD0*/,
      (byte) 153,
      (byte) 19,
      (byte) 239,
      (byte) 35,
      (byte) 229,
      (byte) 36,
      (byte) 75,
      (byte) 73,
      (byte) 78,
      (byte) 6,
      (byte) 226,
      (byte) 205,
      (byte) 164,
      (byte) 39,
      (byte) 199,
      (byte) 192 /*0xC0*/,
      (byte) 21,
      (byte) 95,
      (byte) 19,
      (byte) 97,
      (byte) 109,
      (byte) 212,
      (byte) 24,
      (byte) 214,
      (byte) 24,
      (byte) 227
    };
    byte[] numArray9 = new byte[55];
    numArray9[8] = (byte) 140;
    numArray9[36] = (byte) 157;
    numArray9[2] = (byte) 51;
    numArray9[42] = (byte) 55;
    numArray9[38] = (byte) 185;
    numArray9[27] = (byte) 124;
    numArray9[6] = (byte) 66;
    numArray9[7] = (byte) 227;
    numArray9[54] = (byte) 203;
    numArray9[9] = (byte) 59;
    numArray9[10] = (byte) 249;
    numArray9[45] = (byte) 179;
    numArray9[4] = (byte) 42;
    numArray9[39] = (byte) 232;
    numArray9[3] = (byte) 124;
    numArray9[15] = (byte) 157;
    numArray9[5] = (byte) 253;
    numArray9[17] = (byte) 211;
    numArray9[18] = (byte) 40;
    numArray9[21] = (byte) 2;
    numArray9[20] = (byte) 99;
    numArray9[48 /*0x30*/] = (byte) 207;
    numArray9[12] = (byte) 27;
    numArray9[23] = (byte) 177;
    numArray9[0] = (byte) 134;
    numArray9[46] = (byte) 222;
    numArray9[26] = (byte) 76;
    numArray9[53] = (byte) 175;
    numArray9[13] = (byte) 175;
    numArray9[29] = (byte) 53;
    numArray9[30] = (byte) 54;
    numArray9[31 /*0x1F*/] = (byte) 242;
    numArray9[32 /*0x20*/] = (byte) 206;
    numArray9[33] = (byte) 229;
    numArray9[34] = (byte) 162;
    numArray9[35] = (byte) 188;
    numArray9[40] = (byte) 231;
    numArray9[37] = (byte) 34;
    numArray9[51] = (byte) 161;
    numArray9[25] = (byte) 72;
    numArray9[11] = (byte) 212;
    numArray9[41] = (byte) 17;
    numArray9[52] = (byte) 150;
    numArray9[43] = (byte) 204;
    numArray9[44] = (byte) 9;
    numArray9[16 /*0x10*/] = (byte) 204;
    numArray9[1] = (byte) 104;
    numArray9[47] = (byte) 141;
    numArray9[28] = (byte) 232;
    numArray9[24] = (byte) 141;
    numArray9[50] = (byte) 30;
    numArray9[22] = (byte) 199;
    numArray9[14] = (byte) 225;
    numArray9[49] = (byte) 109;
    numArray9[19] = (byte) 194;
    key.Query(true, 335, numArray8, numArray8);
    Array.Copy((Array) numArray8, 0, (Array) numArray7, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray7[index] ^= numArray9[index];
    byte[] numArray10 = new byte[24];
    numArray10[2] = (byte) 63 /*0x3F*/;
    numArray10[17] = (byte) 86;
    numArray10[22] = (byte) 29;
    numArray10[3] = (byte) 178;
    numArray10[6] = (byte) 107;
    numArray10[11] = (byte) 252;
    numArray10[4] = (byte) 196;
    numArray10[7] = (byte) 210;
    numArray10[8] = (byte) 34;
    numArray10[9] = (byte) 67;
    numArray10[15] = (byte) 165;
    numArray10[20] = (byte) 105;
    numArray10[12] = (byte) 123;
    numArray10[13] = (byte) 1;
    numArray10[14] = (byte) 220;
    numArray10[0] = (byte) 42;
    numArray10[1] = (byte) 132;
    numArray10[16 /*0x10*/] = (byte) 96 /*0x60*/;
    numArray10[5] = (byte) 182;
    numArray10[19] = (byte) 126;
    numArray10[10] = (byte) 114;
    numArray10[21] = (byte) 207;
    numArray10[18] = (byte) 220;
    numArray10[23] = (byte) 49;
    byte[] numArray11 = new byte[24]
    {
      (byte) 199,
      (byte) 112 /*0x70*/,
      (byte) 201,
      (byte) 99,
      (byte) 111,
      (byte) 84,
      (byte) 226,
      (byte) 164,
      (byte) 162,
      (byte) 89,
      (byte) 56,
      (byte) 29,
      byte.MaxValue,
      (byte) 57,
      (byte) 105,
      (byte) 135,
      (byte) 83,
      (byte) 25,
      (byte) 185,
      (byte) 100,
      (byte) 101,
      (byte) 245,
      (byte) 220,
      (byte) 128 /*0x80*/
    };
    key.Query(true, 335, numArray10, numArray10);
    Array.Copy((Array) numArray10, 0, (Array) numArray7, 55, 24);
    for (int index = 0; index < 24; ++index)
      numArray7[index + 55] ^= numArray11[index];
    byte[] numArray12 = new byte[37];
    byte[] response1 = new byte[37];
    Array.Copy((Array) sc_12780.sspq, 514, (Array) numArray12, 0, 37);
    key.Query(true, 335, numArray12, response1);
    Array.Copy((Array) sc_12780.sspr, 514, (Array) numArray12, 0, 37);
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

  internal static string ssp_appserver_12816()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[23];
      byte[] numArray2 = new byte[23]
      {
        (byte) 249,
        (byte) 92,
        (byte) 161,
        (byte) 137,
        (byte) 223,
        (byte) 208 /*0xD0*/,
        (byte) 134,
        (byte) 41,
        (byte) 141,
        (byte) 140,
        (byte) 90,
        (byte) 245,
        (byte) 64 /*0x40*/,
        (byte) 186,
        (byte) 231,
        (byte) 3,
        (byte) 81,
        (byte) 152,
        (byte) 80 /*0x50*/,
        (byte) 77,
        (byte) 238,
        (byte) 206,
        (byte) 85
      };
      byte[] numArray3 = new byte[23];
      numArray3[4] = (byte) 24;
      numArray3[12] = (byte) 17;
      numArray3[2] = (byte) 201;
      numArray3[10] = (byte) 201;
      numArray3[17] = (byte) 69;
      numArray3[21] = (byte) 189;
      numArray3[6] = (byte) 190;
      numArray3[7] = (byte) 165;
      numArray3[3] = (byte) 19;
      numArray3[5] = (byte) 79;
      numArray3[0] = (byte) 145;
      numArray3[8] = (byte) 252;
      numArray3[1] = (byte) 57;
      numArray3[13] = (byte) 220;
      numArray3[9] = (byte) 174;
      numArray3[15] = (byte) 45;
      numArray3[16 /*0x10*/] = (byte) 22;
      numArray3[14] = (byte) 55;
      numArray3[18] = (byte) 77;
      numArray3[19] = (byte) 54;
      numArray3[20] = (byte) 224 /*0xE0*/;
      numArray3[22] = (byte) 21;
      numArray3[11] = (byte) 232;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 23);
      for (int index = 0; index < 23; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[23];
    byte[] numArray5 = new byte[23]
    {
      (byte) 21,
      (byte) 146,
      (byte) 79,
      (byte) 150,
      (byte) 24,
      (byte) 87,
      (byte) 42,
      (byte) 7,
      (byte) 66,
      (byte) 104,
      (byte) 8,
      (byte) 143,
      (byte) 246,
      (byte) 10,
      (byte) 122,
      (byte) 137,
      (byte) 111,
      (byte) 216,
      (byte) 38,
      (byte) 55,
      (byte) 9,
      (byte) 217,
      (byte) 195
    };
    byte[] numArray6 = new byte[23];
    numArray6[15] = (byte) 141;
    numArray6[0] = (byte) 8;
    numArray6[10] = (byte) 181;
    numArray6[1] = (byte) 137;
    numArray6[8] = (byte) 161;
    numArray6[5] = (byte) 29;
    numArray6[20] = (byte) 101;
    numArray6[7] = (byte) 134;
    numArray6[14] = (byte) 0;
    numArray6[18] = (byte) 199;
    numArray6[3] = (byte) 203;
    numArray6[11] = (byte) 161;
    numArray6[4] = (byte) 89;
    numArray6[17] = (byte) 141;
    numArray6[2] = (byte) 161;
    numArray6[9] = (byte) 224 /*0xE0*/;
    numArray6[16 /*0x10*/] = (byte) 189;
    numArray6[12] = (byte) 244;
    numArray6[19] = (byte) 70;
    numArray6[6] = (byte) 50;
    numArray6[13] = (byte) 230;
    numArray6[21] = (byte) 206;
    numArray6[22] = (byte) 108;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 23);
    for (int index = 0; index < 23; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12817()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[27];
      byte[] numArray2 = new byte[27];
      numArray2[25] = (byte) 50;
      numArray2[1] = (byte) 198;
      numArray2[22] = (byte) 210;
      numArray2[12] = (byte) 222;
      numArray2[20] = (byte) 212;
      numArray2[21] = (byte) 27;
      numArray2[6] = (byte) 206;
      numArray2[7] = (byte) 221;
      numArray2[17] = (byte) 142;
      numArray2[3] = (byte) 136;
      numArray2[9] = (byte) 134;
      numArray2[11] = (byte) 103;
      numArray2[5] = (byte) 60;
      numArray2[13] = (byte) 26;
      numArray2[14] = (byte) 230;
      numArray2[15] = (byte) 17;
      numArray2[16 /*0x10*/] = (byte) 14;
      numArray2[0] = (byte) 200;
      numArray2[18] = (byte) 0;
      numArray2[2] = (byte) 184;
      numArray2[19] = (byte) 249;
      numArray2[4] = (byte) 30;
      numArray2[8] = (byte) 105;
      numArray2[10] = (byte) 171;
      numArray2[24] = (byte) 222;
      numArray2[23] = (byte) 99;
      numArray2[26] = (byte) 241;
      byte[] numArray3 = new byte[27]
      {
        (byte) 182,
        (byte) 127 /*0x7F*/,
        (byte) 252,
        (byte) 201,
        (byte) 253,
        (byte) 225,
        (byte) 101,
        (byte) 48 /*0x30*/,
        (byte) 170,
        (byte) 99,
        (byte) 234,
        (byte) 145,
        (byte) 193,
        (byte) 58,
        (byte) 66,
        (byte) 40,
        (byte) 201,
        (byte) 126,
        (byte) 180,
        (byte) 34,
        (byte) 196,
        (byte) 100,
        (byte) 67,
        (byte) 156,
        (byte) 234,
        (byte) 136,
        (byte) 105
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 27);
      for (int index = 0; index < 27; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[27];
    byte[] numArray5 = new byte[27]
    {
      (byte) 85,
      (byte) 169,
      (byte) 235,
      (byte) 75,
      (byte) 48 /*0x30*/,
      (byte) 138,
      (byte) 206,
      (byte) 186,
      (byte) 55,
      (byte) 52,
      (byte) 89,
      (byte) 187,
      (byte) 43,
      (byte) 210,
      (byte) 22,
      (byte) 69,
      (byte) 51,
      (byte) 71,
      (byte) 253,
      (byte) 48 /*0x30*/,
      (byte) 84,
      (byte) 76,
      (byte) 237,
      (byte) 58,
      (byte) 35,
      (byte) 96 /*0x60*/,
      (byte) 69
    };
    byte[] numArray6 = new byte[27];
    numArray6[22] = (byte) 101;
    numArray6[1] = (byte) 168;
    numArray6[0] = (byte) 203;
    numArray6[20] = (byte) 49;
    numArray6[4] = (byte) 173;
    numArray6[5] = (byte) 49;
    numArray6[6] = (byte) 205;
    numArray6[25] = (byte) 54;
    numArray6[11] = (byte) 14;
    numArray6[9] = (byte) 200;
    numArray6[3] = (byte) 173;
    numArray6[10] = (byte) 250;
    numArray6[18] = (byte) 46;
    numArray6[13] = (byte) 3;
    numArray6[14] = (byte) 136;
    numArray6[15] = (byte) 4;
    numArray6[7] = (byte) 77;
    numArray6[2] = (byte) 153;
    numArray6[17] = (byte) 232;
    numArray6[16 /*0x10*/] = (byte) 254;
    numArray6[8] = (byte) 156;
    numArray6[21] = (byte) 161;
    numArray6[19] = (byte) 108;
    numArray6[23] = (byte) 37;
    numArray6[24] = (byte) 221;
    numArray6[12] = (byte) 173;
    numArray6[26] = (byte) 203;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 27);
    for (int index = 0; index < 27; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12818()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[78];
      byte[] numArray2 = new byte[55];
      numArray2[49] = (byte) 32 /*0x20*/;
      numArray2[1] = (byte) 207;
      numArray2[25] = (byte) 164;
      numArray2[29] = (byte) 55;
      numArray2[41] = (byte) 251;
      numArray2[5] = (byte) 249;
      numArray2[6] = (byte) 152;
      numArray2[7] = (byte) 163;
      numArray2[16 /*0x10*/] = (byte) 19;
      numArray2[14] = (byte) 198;
      numArray2[9] = (byte) 98;
      numArray2[11] = (byte) 199;
      numArray2[12] = (byte) 232;
      numArray2[19] = (byte) 49;
      numArray2[20] = (byte) 155;
      numArray2[15] = (byte) 177;
      numArray2[3] = (byte) 182;
      numArray2[0] = (byte) 39;
      numArray2[18] = (byte) 107;
      numArray2[43] = (byte) 176 /*0xB0*/;
      numArray2[36] = (byte) 8;
      numArray2[23] = (byte) 11;
      numArray2[22] = (byte) 77;
      numArray2[37] = (byte) 102;
      numArray2[28] = (byte) 229;
      numArray2[17] = (byte) 144 /*0x90*/;
      numArray2[21] = (byte) 17;
      numArray2[27] = (byte) 77;
      numArray2[10] = (byte) 233;
      numArray2[33] = (byte) 171;
      numArray2[30] = (byte) 41;
      numArray2[31 /*0x1F*/] = (byte) 224 /*0xE0*/;
      numArray2[24] = (byte) 169;
      numArray2[2] = (byte) 68;
      numArray2[51] = (byte) 13;
      numArray2[46] = (byte) 9;
      numArray2[34] = (byte) 90;
      numArray2[35] = (byte) 177;
      numArray2[52] = (byte) 157;
      numArray2[39] = (byte) 230;
      numArray2[40] = (byte) 35;
      numArray2[50] = (byte) 103;
      numArray2[42] = (byte) 8;
      numArray2[54] = (byte) 21;
      numArray2[44] = (byte) 81;
      numArray2[45] = (byte) 166;
      numArray2[26] = (byte) 58;
      numArray2[47] = (byte) 81;
      numArray2[48 /*0x30*/] = (byte) 136;
      numArray2[38] = (byte) 159;
      numArray2[13] = (byte) 177;
      numArray2[4] = (byte) 36;
      numArray2[8] = (byte) 8;
      numArray2[53] = (byte) 138;
      numArray2[32 /*0x20*/] = (byte) 146;
      byte[] numArray3 = new byte[55]
      {
        (byte) 76,
        (byte) 131,
        (byte) 208 /*0xD0*/,
        (byte) 254,
        (byte) 134,
        (byte) 88,
        (byte) 65,
        (byte) 173,
        (byte) 153,
        (byte) 179,
        (byte) 172,
        (byte) 140,
        (byte) 9,
        (byte) 227,
        (byte) 183,
        (byte) 236,
        (byte) 137,
        (byte) 197,
        (byte) 67,
        (byte) 46,
        (byte) 38,
        (byte) 228,
        (byte) 3,
        (byte) 108,
        (byte) 141,
        (byte) 116,
        (byte) 108,
        (byte) 136,
        (byte) 104,
        (byte) 97,
        (byte) 24,
        (byte) 157,
        (byte) 127 /*0x7F*/,
        (byte) 104,
        (byte) 157,
        (byte) 192 /*0xC0*/,
        (byte) 101,
        (byte) 182,
        (byte) 148,
        (byte) 141,
        (byte) 250,
        (byte) 34,
        (byte) 128 /*0x80*/,
        (byte) 115,
        (byte) 165,
        (byte) 25,
        (byte) 34,
        (byte) 6,
        (byte) 226,
        (byte) 72,
        (byte) 2,
        (byte) 25,
        (byte) 98,
        (byte) 182,
        (byte) 16 /*0x10*/
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[23];
      numArray4[9] = (byte) 43;
      numArray4[13] = (byte) 217;
      numArray4[2] = (byte) 229;
      numArray4[16 /*0x10*/] = (byte) 93;
      numArray4[8] = (byte) 197;
      numArray4[10] = (byte) 165;
      numArray4[6] = (byte) 2;
      numArray4[7] = (byte) 29;
      numArray4[17] = (byte) 53;
      numArray4[22] = (byte) 144 /*0x90*/;
      numArray4[4] = (byte) 132;
      numArray4[11] = (byte) 18;
      numArray4[12] = (byte) 228;
      numArray4[21] = (byte) 112 /*0x70*/;
      numArray4[15] = (byte) 210;
      numArray4[5] = (byte) 248;
      numArray4[3] = (byte) 215;
      numArray4[0] = (byte) 127 /*0x7F*/;
      numArray4[18] = (byte) 99;
      numArray4[19] = (byte) 209;
      numArray4[20] = (byte) 38;
      numArray4[1] = (byte) 10;
      numArray4[14] = (byte) 84;
      byte[] numArray5 = new byte[23]
      {
        (byte) 127 /*0x7F*/,
        (byte) 133,
        (byte) 227,
        (byte) 87,
        (byte) 204,
        (byte) 226,
        (byte) 122,
        (byte) 194,
        (byte) 35,
        (byte) 59,
        (byte) 108,
        (byte) 6,
        (byte) 176 /*0xB0*/,
        (byte) 141,
        (byte) 171,
        (byte) 153,
        (byte) 181,
        (byte) 63 /*0x3F*/,
        (byte) 89,
        (byte) 69,
        (byte) 34,
        (byte) 206,
        (byte) 122
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 23);
      for (int index = 0; index < 23; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[78];
    byte[] numArray7 = new byte[55]
    {
      (byte) 74,
      (byte) 186,
      (byte) 69,
      (byte) 17,
      (byte) 59,
      (byte) 190,
      (byte) 14,
      (byte) 121,
      (byte) 23,
      (byte) 234,
      (byte) 50,
      (byte) 171,
      (byte) 236,
      (byte) 30,
      (byte) 155,
      (byte) 158,
      (byte) 131,
      (byte) 218,
      (byte) 153,
      (byte) 137,
      (byte) 5,
      (byte) 152,
      (byte) 14,
      (byte) 225,
      (byte) 30,
      (byte) 156,
      (byte) 205,
      (byte) 237,
      (byte) 74,
      (byte) 166,
      (byte) 4,
      (byte) 194,
      (byte) 175,
      (byte) 176 /*0xB0*/,
      (byte) 180,
      (byte) 111,
      (byte) 71,
      (byte) 15,
      (byte) 252,
      (byte) 140,
      (byte) 65,
      (byte) 213,
      (byte) 34,
      (byte) 101,
      (byte) 33,
      (byte) 152,
      (byte) 72,
      (byte) 57,
      (byte) 231,
      (byte) 168,
      (byte) 163,
      (byte) 14,
      (byte) 245,
      (byte) 20,
      (byte) 95
    };
    byte[] numArray8 = new byte[55]
    {
      (byte) 139,
      (byte) 28,
      (byte) 5,
      (byte) 197,
      (byte) 83,
      (byte) 245,
      (byte) 201,
      (byte) 55,
      (byte) 214,
      (byte) 196,
      (byte) 58,
      (byte) 144 /*0x90*/,
      (byte) 39,
      (byte) 217,
      (byte) 181,
      (byte) 63 /*0x3F*/,
      (byte) 43,
      (byte) 214,
      (byte) 192 /*0xC0*/,
      (byte) 241,
      (byte) 174,
      (byte) 70,
      (byte) 105,
      (byte) 108,
      (byte) 94,
      (byte) 86,
      (byte) 124,
      (byte) 161,
      (byte) 67,
      (byte) 252,
      (byte) 162,
      (byte) 153,
      (byte) 253,
      (byte) 207,
      (byte) 98,
      (byte) 161,
      (byte) 39,
      (byte) 1,
      (byte) 137,
      (byte) 83,
      (byte) 27,
      (byte) 57,
      (byte) 96 /*0x60*/,
      (byte) 15,
      (byte) 34,
      (byte) 234,
      (byte) 125,
      (byte) 107,
      (byte) 186,
      (byte) 250,
      (byte) 227,
      (byte) 4,
      (byte) 203,
      (byte) 2,
      (byte) 207
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[23]
    {
      (byte) 99,
      (byte) 243,
      (byte) 158,
      (byte) 240 /*0xF0*/,
      (byte) 32 /*0x20*/,
      (byte) 46,
      (byte) 113,
      (byte) 230,
      (byte) 112 /*0x70*/,
      (byte) 251,
      (byte) 60,
      (byte) 105,
      (byte) 43,
      (byte) 86,
      (byte) 115,
      (byte) 222,
      (byte) 13,
      (byte) 71,
      (byte) 48 /*0x30*/,
      (byte) 117,
      (byte) 169,
      (byte) 192 /*0xC0*/,
      (byte) 133
    };
    byte[] numArray10 = new byte[23];
    numArray10[13] = (byte) 79;
    numArray10[1] = (byte) 9;
    numArray10[0] = (byte) 63 /*0x3F*/;
    numArray10[3] = (byte) 160 /*0xA0*/;
    numArray10[21] = (byte) 237;
    numArray10[10] = (byte) 183;
    numArray10[6] = (byte) 123;
    numArray10[7] = (byte) 166;
    numArray10[19] = (byte) 162;
    numArray10[20] = (byte) 127 /*0x7F*/;
    numArray10[9] = (byte) 100;
    numArray10[11] = (byte) 245;
    numArray10[12] = (byte) 102;
    numArray10[5] = (byte) 38;
    numArray10[14] = (byte) 74;
    numArray10[4] = (byte) 16 /*0x10*/;
    numArray10[18] = (byte) 182;
    numArray10[17] = (byte) 141;
    numArray10[16 /*0x10*/] = (byte) 167;
    numArray10[2] = (byte) 105;
    numArray10[15] = (byte) 97;
    numArray10[8] = (byte) 64 /*0x40*/;
    numArray10[22] = (byte) 77;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 23);
    for (int index = 0; index < 23; ++index)
      numArray6[index + 55] ^= numArray10[index];
    byte[] numArray11 = new byte[33];
    byte[] response = new byte[33];
    Array.Copy((Array) sc_12780.sspq, 551, (Array) numArray11, 0, 33);
    key.Query(true, 335, numArray11, response);
    Array.Copy((Array) sc_12780.sspr, 551, (Array) numArray11, 0, 33);
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

  internal static string ssp_appserver_12819()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[27];
      byte[] numArray2 = new byte[27]
      {
        (byte) 229,
        (byte) 205,
        (byte) 32 /*0x20*/,
        (byte) 198,
        (byte) 191,
        (byte) 64 /*0x40*/,
        (byte) 175,
        (byte) 19,
        (byte) 62,
        (byte) 5,
        (byte) 25,
        (byte) 66,
        (byte) 142,
        (byte) 12,
        (byte) 59,
        (byte) 30,
        (byte) 105,
        (byte) 83,
        (byte) 222,
        (byte) 216,
        (byte) 174,
        (byte) 117,
        (byte) 166,
        (byte) 166,
        (byte) 119,
        (byte) 217,
        (byte) 146
      };
      byte[] numArray3 = new byte[27];
      numArray3[11] = (byte) 195;
      numArray3[1] = (byte) 145;
      numArray3[15] = (byte) 12;
      numArray3[3] = (byte) 161;
      numArray3[12] = (byte) 124;
      numArray3[9] = (byte) 130;
      numArray3[20] = (byte) 181;
      numArray3[19] = (byte) 45;
      numArray3[14] = (byte) 78;
      numArray3[7] = (byte) 85;
      numArray3[13] = (byte) 109;
      numArray3[4] = (byte) 4;
      numArray3[6] = (byte) 217;
      numArray3[16 /*0x10*/] = (byte) 8;
      numArray3[5] = (byte) 46;
      numArray3[18] = (byte) 26;
      numArray3[2] = (byte) 148;
      numArray3[17] = (byte) 20;
      numArray3[22] = (byte) 12;
      numArray3[0] = (byte) 232;
      numArray3[25] = (byte) 25;
      numArray3[21] = (byte) 215;
      numArray3[8] = (byte) 46;
      numArray3[23] = (byte) 245;
      numArray3[24] = (byte) 101;
      numArray3[10] = (byte) 48 /*0x30*/;
      numArray3[26] = (byte) 246;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 27);
      for (int index = 0; index < 27; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[27];
    byte[] numArray5 = new byte[27];
    numArray5[6] = (byte) 165;
    numArray5[7] = (byte) 52;
    numArray5[11] = (byte) 78;
    numArray5[3] = (byte) 173;
    numArray5[16 /*0x10*/] = (byte) 171;
    numArray5[5] = (byte) 18;
    numArray5[21] = (byte) 54;
    numArray5[18] = (byte) 64 /*0x40*/;
    numArray5[8] = (byte) 221;
    numArray5[9] = (byte) 165;
    numArray5[0] = (byte) 122;
    numArray5[26] = (byte) 214;
    numArray5[2] = (byte) 230;
    numArray5[22] = (byte) 245;
    numArray5[1] = (byte) 78;
    numArray5[15] = (byte) 171;
    numArray5[14] = (byte) 201;
    numArray5[17] = (byte) 49;
    numArray5[25] = (byte) 215;
    numArray5[19] = (byte) 65;
    numArray5[24] = (byte) 142;
    numArray5[4] = (byte) 43;
    numArray5[12] = (byte) 167;
    numArray5[13] = (byte) 83;
    numArray5[23] = (byte) 61;
    numArray5[20] = (byte) 133;
    numArray5[10] = (byte) 101;
    byte[] numArray6 = new byte[27]
    {
      (byte) 188,
      (byte) 164,
      (byte) 237,
      (byte) 107,
      (byte) 143,
      (byte) 222,
      (byte) 76,
      (byte) 178,
      (byte) 228,
      (byte) 63 /*0x3F*/,
      (byte) 194,
      (byte) 0,
      (byte) 142,
      (byte) 196,
      (byte) 144 /*0x90*/,
      (byte) 40,
      (byte) 34,
      (byte) 117,
      (byte) 142,
      (byte) 212,
      (byte) 245,
      (byte) 33,
      (byte) 147,
      (byte) 120,
      (byte) 131,
      (byte) 93,
      (byte) 61
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 27);
    for (int index = 0; index < 27; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12820()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[110];
      byte[] numArray2 = new byte[55]
      {
        (byte) 30,
        (byte) 83,
        (byte) 59,
        (byte) 102,
        (byte) 88,
        (byte) 18,
        (byte) 2,
        (byte) 85,
        (byte) 0,
        (byte) 57,
        (byte) 1,
        (byte) 119,
        (byte) 233,
        (byte) 39,
        (byte) 181,
        (byte) 228,
        (byte) 179,
        (byte) 152,
        (byte) 164,
        (byte) 24,
        (byte) 8,
        (byte) 217,
        (byte) 209,
        (byte) 254,
        (byte) 174,
        (byte) 145,
        (byte) 5,
        (byte) 218,
        (byte) 56,
        (byte) 30,
        (byte) 204,
        (byte) 13,
        (byte) 248,
        (byte) 25,
        (byte) 101,
        (byte) 40,
        (byte) 32 /*0x20*/,
        (byte) 39,
        (byte) 148,
        (byte) 128 /*0x80*/,
        (byte) 218,
        (byte) 224 /*0xE0*/,
        (byte) 180,
        (byte) 204,
        (byte) 213,
        (byte) 25,
        (byte) 97,
        (byte) 158,
        (byte) 19,
        (byte) 243,
        (byte) 123,
        (byte) 9,
        (byte) 158,
        (byte) 65,
        (byte) 160 /*0xA0*/
      };
      byte[] numArray3 = new byte[55];
      numArray3[5] = (byte) 147;
      numArray3[1] = (byte) 6;
      numArray3[46] = (byte) 176 /*0xB0*/;
      numArray3[36] = (byte) 174;
      numArray3[4] = (byte) 235;
      numArray3[19] = (byte) 46;
      numArray3[6] = (byte) 241;
      numArray3[8] = (byte) 81;
      numArray3[21] = (byte) 129;
      numArray3[51] = (byte) 164;
      numArray3[2] = (byte) 38;
      numArray3[11] = (byte) 148;
      numArray3[17] = (byte) 191;
      numArray3[44] = (byte) 123;
      numArray3[14] = (byte) 4;
      numArray3[26] = (byte) 66;
      numArray3[16 /*0x10*/] = (byte) 160 /*0xA0*/;
      numArray3[0] = (byte) 202;
      numArray3[31 /*0x1F*/] = (byte) 239;
      numArray3[13] = (byte) 18;
      numArray3[20] = (byte) 83;
      numArray3[24] = (byte) 219;
      numArray3[32 /*0x20*/] = (byte) 63 /*0x3F*/;
      numArray3[23] = byte.MaxValue;
      numArray3[37] = (byte) 192 /*0xC0*/;
      numArray3[25] = (byte) 73;
      numArray3[54] = (byte) 105;
      numArray3[27] = (byte) 240 /*0xF0*/;
      numArray3[40] = (byte) 96 /*0x60*/;
      numArray3[53] = (byte) 95;
      numArray3[30] = (byte) 148;
      numArray3[28] = (byte) 111;
      numArray3[7] = (byte) 233;
      numArray3[33] = (byte) 80 /*0x50*/;
      numArray3[34] = (byte) 177;
      numArray3[52] = (byte) 126;
      numArray3[47] = (byte) 80 /*0x50*/;
      numArray3[18] = (byte) 181;
      numArray3[38] = (byte) 68;
      numArray3[39] = (byte) 129;
      numArray3[22] = (byte) 28;
      numArray3[41] = (byte) 216;
      numArray3[42] = (byte) 208 /*0xD0*/;
      numArray3[43] = (byte) 57;
      numArray3[9] = (byte) 213;
      numArray3[45] = (byte) 34;
      numArray3[15] = (byte) 208 /*0xD0*/;
      numArray3[3] = (byte) 75;
      numArray3[48 /*0x30*/] = (byte) 174;
      numArray3[49] = (byte) 60;
      numArray3[50] = (byte) 240 /*0xF0*/;
      numArray3[10] = (byte) 147;
      numArray3[35] = (byte) 110;
      numArray3[29] = (byte) 152;
      numArray3[12] = (byte) 117;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55];
      numArray4[30] = (byte) 179;
      numArray4[12] = (byte) 231;
      numArray4[47] = (byte) 77;
      numArray4[43] = (byte) 209;
      numArray4[2] = (byte) 151;
      numArray4[8] = (byte) 132;
      numArray4[54] = (byte) 216;
      numArray4[7] = (byte) 170;
      numArray4[3] = (byte) 166;
      numArray4[35] = (byte) 75;
      numArray4[10] = (byte) 86;
      numArray4[11] = (byte) 161;
      numArray4[17] = (byte) 139;
      numArray4[13] = (byte) 225;
      numArray4[48 /*0x30*/] = (byte) 212;
      numArray4[15] = (byte) 54;
      numArray4[40] = (byte) 148;
      numArray4[31 /*0x1F*/] = (byte) 0;
      numArray4[18] = (byte) 99;
      numArray4[19] = (byte) 81;
      numArray4[42] = (byte) 93;
      numArray4[41] = (byte) 180;
      numArray4[22] = (byte) 250;
      numArray4[14] = (byte) 28;
      numArray4[16 /*0x10*/] = (byte) 177;
      numArray4[25] = (byte) 65;
      numArray4[26] = (byte) 23;
      numArray4[20] = (byte) 165;
      numArray4[28] = (byte) 189;
      numArray4[29] = (byte) 244;
      numArray4[49] = (byte) 206;
      numArray4[44] = (byte) 58;
      numArray4[32 /*0x20*/] = (byte) 0;
      numArray4[33] = (byte) 39;
      numArray4[1] = (byte) 35;
      numArray4[4] = (byte) 96 /*0x60*/;
      numArray4[36] = (byte) 53;
      numArray4[37] = (byte) 204;
      numArray4[23] = (byte) 158;
      numArray4[5] = (byte) 214;
      numArray4[34] = (byte) 207;
      numArray4[24] = (byte) 137;
      numArray4[52] = (byte) 20;
      numArray4[0] = (byte) 19;
      numArray4[6] = (byte) 62;
      numArray4[45] = (byte) 59;
      numArray4[46] = (byte) 158;
      numArray4[53] = (byte) 206;
      numArray4[39] = (byte) 237;
      numArray4[38] = (byte) 82;
      numArray4[50] = (byte) 201;
      numArray4[51] = (byte) 120;
      numArray4[27] = (byte) 178;
      numArray4[21] = (byte) 70;
      numArray4[9] = (byte) 200;
      byte[] numArray5 = new byte[55]
      {
        (byte) 44,
        (byte) 123,
        (byte) 10,
        (byte) 231,
        (byte) 91,
        (byte) 175,
        (byte) 180,
        (byte) 231,
        (byte) 241,
        (byte) 48 /*0x30*/,
        (byte) 120,
        (byte) 174,
        (byte) 87,
        (byte) 46,
        (byte) 44,
        (byte) 223,
        (byte) 5,
        (byte) 86,
        (byte) 107,
        (byte) 15,
        (byte) 44,
        (byte) 137,
        (byte) 199,
        (byte) 61,
        (byte) 163,
        (byte) 72,
        (byte) 79,
        (byte) 214,
        (byte) 79,
        (byte) 138,
        (byte) 242,
        (byte) 63 /*0x3F*/,
        (byte) 63 /*0x3F*/,
        (byte) 123,
        (byte) 152,
        (byte) 126,
        (byte) 20,
        (byte) 199,
        (byte) 84,
        (byte) 60,
        (byte) 178,
        (byte) 45,
        (byte) 93,
        (byte) 1,
        (byte) 154,
        (byte) 103,
        (byte) 115,
        (byte) 92,
        (byte) 9,
        (byte) 122,
        (byte) 89,
        (byte) 111,
        (byte) 61,
        (byte) 30,
        (byte) 3
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[110];
    byte[] numArray7 = new byte[55];
    numArray7[30] = (byte) 102;
    numArray7[42] = (byte) 43;
    numArray7[2] = (byte) 245;
    numArray7[9] = (byte) 113;
    numArray7[4] = (byte) 254;
    numArray7[7] = (byte) 105;
    numArray7[33] = (byte) 168;
    numArray7[36] = (byte) 57;
    numArray7[8] = (byte) 111;
    numArray7[32 /*0x20*/] = (byte) 34;
    numArray7[10] = (byte) 14;
    numArray7[23] = (byte) 142;
    numArray7[51] = (byte) 190;
    numArray7[13] = (byte) 53;
    numArray7[14] = (byte) 5;
    numArray7[11] = (byte) 174;
    numArray7[53] = (byte) 146;
    numArray7[6] = (byte) 218;
    numArray7[34] = (byte) 224 /*0xE0*/;
    numArray7[19] = (byte) 109;
    numArray7[20] = (byte) 128 /*0x80*/;
    numArray7[21] = (byte) 179;
    numArray7[35] = (byte) 183;
    numArray7[26] = (byte) 70;
    numArray7[24] = (byte) 218;
    numArray7[0] = (byte) 207;
    numArray7[5] = (byte) 100;
    numArray7[27] = (byte) 251;
    numArray7[28] = (byte) 78;
    numArray7[1] = (byte) 123;
    numArray7[29] = (byte) 31 /*0x1F*/;
    numArray7[31 /*0x1F*/] = (byte) 31 /*0x1F*/;
    numArray7[22] = (byte) 197;
    numArray7[18] = (byte) 27;
    numArray7[49] = (byte) 87;
    numArray7[48 /*0x30*/] = (byte) 58;
    numArray7[54] = (byte) 19;
    numArray7[17] = (byte) 41;
    numArray7[38] = (byte) 200;
    numArray7[39] = (byte) 104;
    numArray7[40] = (byte) 226;
    numArray7[37] = (byte) 90;
    numArray7[12] = (byte) 195;
    numArray7[43] = (byte) 108;
    numArray7[52] = (byte) 199;
    numArray7[45] = (byte) 84;
    numArray7[46] = (byte) 10;
    numArray7[47] = (byte) 174;
    numArray7[16 /*0x10*/] = (byte) 180;
    numArray7[15] = (byte) 71;
    numArray7[50] = (byte) 93;
    numArray7[25] = (byte) 32 /*0x20*/;
    numArray7[44] = (byte) 45;
    numArray7[41] = (byte) 122;
    numArray7[3] = (byte) 174;
    byte[] numArray8 = new byte[55]
    {
      (byte) 17,
      (byte) 147,
      (byte) 142,
      (byte) 118,
      (byte) 111,
      (byte) 158,
      (byte) 48 /*0x30*/,
      (byte) 110,
      (byte) 227,
      (byte) 249,
      (byte) 42,
      (byte) 45,
      (byte) 55,
      (byte) 123,
      (byte) 73,
      (byte) 82,
      (byte) 45,
      (byte) 75,
      (byte) 186,
      (byte) 27,
      (byte) 41,
      (byte) 35,
      (byte) 253,
      (byte) 178,
      (byte) 48 /*0x30*/,
      (byte) 159,
      (byte) 35,
      (byte) 19,
      (byte) 226,
      (byte) 151,
      (byte) 66,
      (byte) 167,
      (byte) 137,
      (byte) 99,
      (byte) 167,
      (byte) 103,
      (byte) 201,
      (byte) 96 /*0x60*/,
      (byte) 49,
      (byte) 189,
      (byte) 236,
      (byte) 216,
      (byte) 2,
      (byte) 234,
      (byte) 200,
      (byte) 20,
      (byte) 187,
      (byte) 37,
      (byte) 81,
      (byte) 223,
      (byte) 71,
      (byte) 23,
      (byte) 62,
      (byte) 81,
      (byte) 86
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[55];
    numArray9[45] = (byte) 200;
    numArray9[30] = (byte) 137;
    numArray9[31 /*0x1F*/] = (byte) 135;
    numArray9[35] = (byte) 57;
    numArray9[21] = (byte) 35;
    numArray9[28] = (byte) 74;
    numArray9[6] = (byte) 141;
    numArray9[7] = (byte) 98;
    numArray9[8] = (byte) 151;
    numArray9[9] = (byte) 251;
    numArray9[34] = (byte) 204;
    numArray9[18] = (byte) 167;
    numArray9[24] = (byte) 62;
    numArray9[13] = (byte) 58;
    numArray9[41] = byte.MaxValue;
    numArray9[10] = (byte) 133;
    numArray9[16 /*0x10*/] = (byte) 105;
    numArray9[17] = (byte) 135;
    numArray9[14] = (byte) 211;
    numArray9[19] = (byte) 173;
    numArray9[20] = (byte) 246;
    numArray9[54] = (byte) 103;
    numArray9[1] = (byte) 77;
    numArray9[52] = (byte) 102;
    numArray9[27] = (byte) 191;
    numArray9[11] = (byte) 144 /*0x90*/;
    numArray9[26] = (byte) 173;
    numArray9[38] = (byte) 230;
    numArray9[2] = (byte) 201;
    numArray9[29] = (byte) 67;
    numArray9[51] = (byte) 127 /*0x7F*/;
    numArray9[42] = (byte) 166;
    numArray9[48 /*0x30*/] = (byte) 47;
    numArray9[33] = (byte) 162;
    numArray9[22] = (byte) 189;
    numArray9[0] = (byte) 5;
    numArray9[36] = (byte) 32 /*0x20*/;
    numArray9[37] = (byte) 196;
    numArray9[46] = (byte) 160 /*0xA0*/;
    numArray9[39] = (byte) 57;
    numArray9[40] = (byte) 77;
    numArray9[12] = (byte) 76;
    numArray9[32 /*0x20*/] = (byte) 49;
    numArray9[43] = (byte) 205;
    numArray9[44] = (byte) 106;
    numArray9[3] = (byte) 133;
    numArray9[4] = (byte) 29;
    numArray9[47] = (byte) 14;
    numArray9[50] = (byte) 171;
    numArray9[49] = (byte) 230;
    numArray9[23] = (byte) 37;
    numArray9[53] = (byte) 230;
    numArray9[25] = (byte) 212;
    numArray9[5] = (byte) 36;
    numArray9[15] = (byte) 119;
    byte[] numArray10 = new byte[55]
    {
      (byte) 94,
      (byte) 54,
      (byte) 60,
      (byte) 176 /*0xB0*/,
      (byte) 141,
      (byte) 218,
      (byte) 78,
      (byte) 0,
      (byte) 83,
      (byte) 7,
      (byte) 241,
      (byte) 28,
      (byte) 85,
      (byte) 175,
      (byte) 151,
      (byte) 64 /*0x40*/,
      (byte) 128 /*0x80*/,
      (byte) 90,
      (byte) 213,
      (byte) 161,
      (byte) 138,
      (byte) 232,
      (byte) 35,
      (byte) 48 /*0x30*/,
      (byte) 135,
      (byte) 48 /*0x30*/,
      (byte) 138,
      byte.MaxValue,
      (byte) 247,
      (byte) 45,
      (byte) 55,
      (byte) 168,
      (byte) 229,
      (byte) 165,
      (byte) 150,
      (byte) 193,
      (byte) 8,
      (byte) 60,
      (byte) 228,
      (byte) 92,
      (byte) 9,
      (byte) 142,
      (byte) 24,
      (byte) 156,
      (byte) 166,
      (byte) 214,
      (byte) 174,
      (byte) 61,
      (byte) 167,
      (byte) 211,
      (byte) 218,
      (byte) 215,
      (byte) 3,
      (byte) 2,
      (byte) 41
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_12821()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[112 /*0x70*/];
      byte[] numArray2 = new byte[55]
      {
        (byte) 20,
        (byte) 216,
        (byte) 61,
        (byte) 181,
        (byte) 188,
        (byte) 104,
        (byte) 221,
        (byte) 20,
        (byte) 110,
        (byte) 131,
        (byte) 239,
        (byte) 218,
        (byte) 39,
        (byte) 4,
        (byte) 171,
        (byte) 223,
        (byte) 15,
        (byte) 43,
        (byte) 149,
        (byte) 79,
        (byte) 14,
        (byte) 61,
        (byte) 254,
        (byte) 250,
        (byte) 211,
        (byte) 145,
        (byte) 43,
        (byte) 41,
        (byte) 196,
        (byte) 201,
        (byte) 132,
        (byte) 11,
        (byte) 147,
        (byte) 173,
        (byte) 137,
        (byte) 234,
        (byte) 67,
        (byte) 50,
        (byte) 149,
        (byte) 242,
        (byte) 142,
        (byte) 83,
        (byte) 136,
        (byte) 28,
        (byte) 167,
        (byte) 181,
        (byte) 223,
        (byte) 217,
        (byte) 76,
        (byte) 167,
        (byte) 97,
        (byte) 58,
        (byte) 9,
        (byte) 153,
        (byte) 194
      };
      byte[] numArray3 = new byte[55];
      numArray3[48 /*0x30*/] = (byte) 64 /*0x40*/;
      numArray3[0] = (byte) 35;
      numArray3[12] = (byte) 237;
      numArray3[3] = (byte) 50;
      numArray3[4] = (byte) 243;
      numArray3[5] = (byte) 190;
      numArray3[39] = (byte) 127 /*0x7F*/;
      numArray3[7] = (byte) 182;
      numArray3[8] = (byte) 135;
      numArray3[20] = (byte) 114;
      numArray3[1] = (byte) 75;
      numArray3[11] = (byte) 246;
      numArray3[30] = (byte) 42;
      numArray3[13] = (byte) 32 /*0x20*/;
      numArray3[42] = (byte) 53;
      numArray3[15] = (byte) 192 /*0xC0*/;
      numArray3[33] = (byte) 100;
      numArray3[46] = (byte) 199;
      numArray3[10] = (byte) 53;
      numArray3[19] = (byte) 88;
      numArray3[14] = (byte) 174;
      numArray3[21] = (byte) 202;
      numArray3[22] = (byte) 211;
      numArray3[53] = (byte) 83;
      numArray3[9] = (byte) 56;
      numArray3[40] = (byte) 26;
      numArray3[17] = (byte) 134;
      numArray3[27] = (byte) 189;
      numArray3[41] = (byte) 157;
      numArray3[49] = (byte) 189;
      numArray3[2] = (byte) 98;
      numArray3[31 /*0x1F*/] = (byte) 68;
      numArray3[32 /*0x20*/] = (byte) 109;
      numArray3[6] = (byte) 18;
      numArray3[34] = (byte) 164;
      numArray3[29] = (byte) 127 /*0x7F*/;
      numArray3[36] = (byte) 22;
      numArray3[37] = (byte) 188;
      numArray3[25] = (byte) 206;
      numArray3[18] = (byte) 89;
      numArray3[47] = (byte) 158;
      numArray3[23] = byte.MaxValue;
      numArray3[51] = (byte) 100;
      numArray3[43] = (byte) 12;
      numArray3[26] = (byte) 209;
      numArray3[45] = (byte) 244;
      numArray3[35] = (byte) 71;
      numArray3[24] = (byte) 4;
      numArray3[44] = (byte) 243;
      numArray3[28] = (byte) 241;
      numArray3[50] = (byte) 119;
      numArray3[16 /*0x10*/] = (byte) 15;
      numArray3[52] = (byte) 71;
      numArray3[38] = (byte) 211;
      numArray3[54] = (byte) 85;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 239,
        (byte) 196,
        (byte) 120,
        (byte) 234,
        (byte) 215,
        (byte) 51,
        (byte) 59,
        (byte) 181,
        (byte) 226,
        (byte) 77,
        (byte) 143,
        (byte) 199,
        (byte) 176 /*0xB0*/,
        (byte) 155,
        (byte) 141,
        (byte) 43,
        (byte) 236,
        (byte) 230,
        (byte) 182,
        (byte) 209,
        (byte) 196,
        (byte) 144 /*0x90*/,
        (byte) 175,
        (byte) 77,
        (byte) 242,
        (byte) 81,
        (byte) 17,
        (byte) 228,
        (byte) 164,
        (byte) 154,
        (byte) 33,
        (byte) 226,
        (byte) 146,
        (byte) 24,
        (byte) 17,
        (byte) 140,
        (byte) 242,
        (byte) 203,
        (byte) 2,
        (byte) 207,
        (byte) 168,
        (byte) 103,
        (byte) 224 /*0xE0*/,
        (byte) 32 /*0x20*/,
        (byte) 214,
        (byte) 151,
        (byte) 218,
        (byte) 112 /*0x70*/,
        (byte) 246,
        (byte) 121,
        (byte) 121,
        (byte) 242,
        (byte) 81,
        (byte) 79,
        (byte) 51
      };
      byte[] numArray5 = new byte[55];
      numArray5[5] = (byte) 50;
      numArray5[18] = (byte) 173;
      numArray5[2] = (byte) 186;
      numArray5[33] = (byte) 127 /*0x7F*/;
      numArray5[41] = (byte) 208 /*0xD0*/;
      numArray5[30] = (byte) 185;
      numArray5[6] = (byte) 33;
      numArray5[40] = (byte) 36;
      numArray5[8] = (byte) 203;
      numArray5[9] = (byte) 183;
      numArray5[10] = (byte) 12;
      numArray5[11] = (byte) 252;
      numArray5[3] = (byte) 246;
      numArray5[42] = (byte) 2;
      numArray5[14] = (byte) 94;
      numArray5[15] = (byte) 122;
      numArray5[24] = (byte) 219;
      numArray5[0] = (byte) 118;
      numArray5[12] = (byte) 94;
      numArray5[19] = (byte) 206;
      numArray5[20] = (byte) 104;
      numArray5[21] = (byte) 19;
      numArray5[48 /*0x30*/] = (byte) 123;
      numArray5[43] = (byte) 118;
      numArray5[50] = (byte) 210;
      numArray5[26] = (byte) 79;
      numArray5[29] = (byte) 130;
      numArray5[27] = (byte) 18;
      numArray5[13] = (byte) 88;
      numArray5[37] = (byte) 195;
      numArray5[54] = (byte) 21;
      numArray5[52] = (byte) 133;
      numArray5[28] = (byte) 250;
      numArray5[22] = (byte) 157;
      numArray5[31 /*0x1F*/] = (byte) 235;
      numArray5[35] = (byte) 164;
      numArray5[36] = (byte) 205;
      numArray5[4] = (byte) 74;
      numArray5[38] = (byte) 169;
      numArray5[39] = (byte) 107;
      numArray5[17] = (byte) 85;
      numArray5[53] = (byte) 125;
      numArray5[16 /*0x10*/] = (byte) 240 /*0xF0*/;
      numArray5[1] = (byte) 23;
      numArray5[44] = (byte) 212;
      numArray5[45] = (byte) 190;
      numArray5[46] = (byte) 74;
      numArray5[23] = (byte) 158;
      numArray5[32 /*0x20*/] = (byte) 13;
      numArray5[49] = (byte) 148;
      numArray5[34] = (byte) 235;
      numArray5[51] = (byte) 94;
      numArray5[25] = (byte) 32 /*0x20*/;
      numArray5[47] = (byte) 201;
      numArray5[7] = (byte) 162;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[2]
      {
        (byte) 180,
        (byte) 157
      };
      byte[] numArray7 = new byte[2]{ (byte) 0, (byte) 186 };
      numArray7[0] = (byte) 87;
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 2);
      for (int index = 0; index < 2; ++index)
        numArray1[index + 110] ^= numArray7[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray8 = new byte[112 /*0x70*/];
    byte[] numArray9 = new byte[55];
    numArray9[45] = (byte) 205;
    numArray9[0] = (byte) 253;
    numArray9[10] = (byte) 216;
    numArray9[42] = (byte) 165;
    numArray9[29] = (byte) 108;
    numArray9[53] = (byte) 252;
    numArray9[30] = (byte) 44;
    numArray9[41] = (byte) 11;
    numArray9[5] = (byte) 137;
    numArray9[49] = (byte) 164;
    numArray9[1] = (byte) 26;
    numArray9[33] = (byte) 114;
    numArray9[12] = (byte) 21;
    numArray9[13] = (byte) 153;
    numArray9[16 /*0x10*/] = (byte) 52;
    numArray9[15] = (byte) 248;
    numArray9[38] = (byte) 13;
    numArray9[17] = (byte) 187;
    numArray9[18] = (byte) 247;
    numArray9[19] = (byte) 233;
    numArray9[6] = (byte) 113;
    numArray9[21] = (byte) 33;
    numArray9[11] = (byte) 7;
    numArray9[23] = (byte) 28;
    numArray9[4] = (byte) 216;
    numArray9[3] = (byte) 178;
    numArray9[26] = (byte) 241;
    numArray9[27] = (byte) 18;
    numArray9[54] = (byte) 94;
    numArray9[24] = (byte) 16 /*0x10*/;
    numArray9[22] = (byte) 40;
    numArray9[31 /*0x1F*/] = (byte) 231;
    numArray9[32 /*0x20*/] = (byte) 111;
    numArray9[34] = (byte) 247;
    numArray9[2] = (byte) 242;
    numArray9[35] = (byte) 15;
    numArray9[36] = (byte) 111;
    numArray9[37] = (byte) 144 /*0x90*/;
    numArray9[25] = (byte) 1;
    numArray9[39] = (byte) 108;
    numArray9[46] = (byte) 154;
    numArray9[7] = (byte) 77;
    numArray9[14] = (byte) 77;
    numArray9[43] = (byte) 48 /*0x30*/;
    numArray9[44] = (byte) 78;
    numArray9[20] = (byte) 200;
    numArray9[51] = (byte) 197;
    numArray9[47] = (byte) 236;
    numArray9[9] = (byte) 57;
    numArray9[40] = (byte) 207;
    numArray9[28] = (byte) 54;
    numArray9[48 /*0x30*/] = (byte) 103;
    numArray9[52] = (byte) 114;
    numArray9[50] = (byte) 219;
    numArray9[8] = (byte) 104;
    byte[] numArray10 = new byte[55]
    {
      (byte) 240 /*0xF0*/,
      (byte) 81,
      (byte) 59,
      (byte) 100,
      (byte) 254,
      (byte) 170,
      (byte) 208 /*0xD0*/,
      (byte) 187,
      (byte) 33,
      (byte) 46,
      (byte) 98,
      (byte) 242,
      (byte) 241,
      (byte) 68,
      (byte) 24,
      (byte) 170,
      (byte) 185,
      (byte) 51,
      (byte) 141,
      (byte) 53,
      (byte) 17,
      (byte) 20,
      (byte) 145,
      (byte) 203,
      (byte) 182,
      (byte) 29,
      (byte) 160 /*0xA0*/,
      (byte) 61,
      (byte) 146,
      (byte) 230,
      (byte) 163,
      (byte) 127 /*0x7F*/,
      (byte) 186,
      (byte) 164,
      (byte) 132,
      (byte) 70,
      (byte) 180,
      (byte) 228,
      (byte) 208 /*0xD0*/,
      (byte) 179,
      (byte) 220,
      (byte) 94,
      (byte) 22,
      (byte) 141,
      (byte) 95,
      (byte) 1,
      (byte) 178,
      (byte) 153,
      (byte) 154,
      (byte) 139,
      (byte) 37,
      (byte) 96 /*0x60*/,
      (byte) 60,
      (byte) 166,
      (byte) 109
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray8, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index] ^= numArray10[index];
    byte[] numArray11 = new byte[55]
    {
      (byte) 8,
      (byte) 95,
      (byte) 110,
      (byte) 146,
      (byte) 179,
      (byte) 133,
      (byte) 106,
      (byte) 165,
      (byte) 123,
      (byte) 37,
      (byte) 142,
      (byte) 133,
      (byte) 31 /*0x1F*/,
      (byte) 239,
      (byte) 182,
      (byte) 119,
      (byte) 182,
      (byte) 170,
      (byte) 200,
      (byte) 209,
      (byte) 212,
      (byte) 112 /*0x70*/,
      (byte) 14,
      (byte) 112 /*0x70*/,
      (byte) 186,
      (byte) 129,
      (byte) 117,
      (byte) 90,
      (byte) 80 /*0x50*/,
      (byte) 142,
      (byte) 145,
      (byte) 115,
      (byte) 187,
      (byte) 35,
      (byte) 144 /*0x90*/,
      (byte) 142,
      (byte) 200,
      (byte) 65,
      (byte) 188,
      (byte) 110,
      (byte) 63 /*0x3F*/,
      (byte) 126,
      (byte) 225,
      (byte) 15,
      (byte) 104,
      (byte) 196,
      (byte) 10,
      (byte) 45,
      (byte) 128 /*0x80*/,
      (byte) 159,
      (byte) 48 /*0x30*/,
      (byte) 55,
      (byte) 216,
      (byte) 143,
      (byte) 37
    };
    byte[] numArray12 = new byte[55]
    {
      (byte) 7,
      (byte) 178,
      (byte) 79,
      (byte) 73,
      (byte) 218,
      (byte) 24,
      (byte) 54,
      (byte) 47,
      (byte) 220,
      (byte) 235,
      (byte) 227,
      (byte) 227,
      (byte) 32 /*0x20*/,
      (byte) 61,
      (byte) 13,
      (byte) 141,
      (byte) 43,
      (byte) 194,
      (byte) 120,
      (byte) 136,
      (byte) 81,
      (byte) 33,
      (byte) 241,
      (byte) 59,
      (byte) 118,
      (byte) 131,
      (byte) 219,
      (byte) 93,
      (byte) 107,
      (byte) 129,
      (byte) 210,
      (byte) 195,
      (byte) 189,
      (byte) 15,
      (byte) 80 /*0x50*/,
      (byte) 29,
      (byte) 30,
      (byte) 90,
      (byte) 174,
      (byte) 122,
      (byte) 98,
      (byte) 141,
      (byte) 183,
      (byte) 134,
      (byte) 252,
      (byte) 149,
      (byte) 114,
      (byte) 225,
      (byte) 176 /*0xB0*/,
      (byte) 158,
      (byte) 9,
      (byte) 215,
      (byte) 150,
      (byte) 93,
      (byte) 144 /*0x90*/
    };
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray8, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index + 55] ^= numArray12[index];
    byte[] numArray13 = new byte[2]
    {
      (byte) 254,
      (byte) 216
    };
    byte[] numArray14 = new byte[2]
    {
      (byte) 148,
      (byte) 118
    };
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray8, 110, 2);
    for (int index = 0; index < 2; ++index)
      numArray8[index + 110] ^= numArray14[index];
    byte[] numArray15 = new byte[19];
    byte[] response = new byte[19];
    Array.Copy((Array) sc_12780.sspq, 584, (Array) numArray15, 0, 19);
    key.Query(true, 335, numArray15, response);
    Array.Copy((Array) sc_12780.sspr, 584, (Array) numArray15, 0, 19);
    for (int index = 0; index < numArray15.Length; ++index)
    {
      if ((int) numArray15[index] != (int) response[index])
      {
        key.TagValue = (int) response[index];
        break;
      }
    }
    return Encoding.UTF8.GetString(numArray8);
  }

  internal static string ssp_appserver_12822()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[105];
      byte[] numArray2 = new byte[55]
      {
        (byte) 89,
        (byte) 155,
        (byte) 43,
        (byte) 254,
        (byte) 248,
        (byte) 61,
        (byte) 201,
        (byte) 105,
        (byte) 137,
        (byte) 94,
        (byte) 47,
        (byte) 42,
        (byte) 243,
        (byte) 123,
        (byte) 78,
        (byte) 249,
        (byte) 14,
        (byte) 206,
        (byte) 89,
        (byte) 240 /*0xF0*/,
        (byte) 187,
        (byte) 73,
        (byte) 125,
        (byte) 254,
        (byte) 207,
        (byte) 8,
        (byte) 82,
        (byte) 246,
        (byte) 226,
        (byte) 188,
        (byte) 200,
        (byte) 54,
        (byte) 163,
        (byte) 144 /*0x90*/,
        (byte) 58,
        (byte) 127 /*0x7F*/,
        (byte) 159,
        (byte) 253,
        (byte) 102,
        (byte) 233,
        (byte) 228,
        (byte) 11,
        (byte) 100,
        (byte) 251,
        (byte) 98,
        (byte) 44,
        (byte) 156,
        (byte) 131,
        (byte) 185,
        (byte) 252,
        (byte) 103,
        (byte) 121,
        (byte) 128 /*0x80*/,
        (byte) 191,
        (byte) 161
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 162,
        (byte) 197,
        (byte) 68,
        (byte) 99,
        (byte) 37,
        (byte) 172,
        (byte) 138,
        (byte) 18,
        (byte) 53,
        (byte) 247,
        (byte) 172,
        (byte) 140,
        (byte) 93,
        (byte) 99,
        (byte) 134,
        (byte) 92,
        (byte) 78,
        (byte) 161,
        (byte) 65,
        (byte) 26,
        (byte) 23,
        (byte) 76,
        (byte) 26,
        (byte) 70,
        (byte) 232,
        (byte) 45,
        (byte) 163,
        (byte) 195,
        (byte) 12,
        (byte) 79,
        (byte) 213,
        (byte) 106,
        (byte) 89,
        (byte) 129,
        (byte) 98,
        (byte) 12,
        (byte) 25,
        (byte) 178,
        (byte) 156,
        (byte) 97,
        (byte) 77,
        (byte) 206,
        (byte) 75,
        (byte) 184,
        (byte) 228,
        (byte) 140,
        (byte) 253,
        (byte) 200,
        (byte) 193,
        (byte) 220,
        (byte) 222,
        (byte) 224 /*0xE0*/,
        (byte) 179,
        (byte) 114,
        (byte) 78
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[50]
      {
        (byte) 44,
        (byte) 47,
        (byte) 239,
        (byte) 235,
        (byte) 211,
        (byte) 134,
        (byte) 205,
        (byte) 149,
        (byte) 248,
        (byte) 134,
        (byte) 98,
        (byte) 64 /*0x40*/,
        (byte) 206,
        (byte) 90,
        (byte) 17,
        (byte) 104,
        (byte) 111,
        (byte) 12,
        (byte) 176 /*0xB0*/,
        (byte) 229,
        (byte) 130,
        (byte) 156,
        (byte) 162,
        (byte) 108,
        (byte) 207,
        (byte) 61,
        (byte) 230,
        (byte) 84,
        (byte) 79,
        (byte) 182,
        (byte) 227,
        (byte) 239,
        (byte) 42,
        (byte) 98,
        (byte) 114,
        (byte) 206,
        (byte) 118,
        (byte) 254,
        (byte) 17,
        (byte) 133,
        (byte) 150,
        (byte) 101,
        (byte) 105,
        (byte) 165,
        (byte) 57,
        (byte) 66,
        (byte) 235,
        (byte) 162,
        (byte) 85,
        (byte) 17
      };
      byte[] numArray5 = new byte[50];
      numArray5[13] = (byte) 246;
      numArray5[1] = (byte) 202;
      numArray5[6] = (byte) 97;
      numArray5[3] = (byte) 214;
      numArray5[4] = (byte) 207;
      numArray5[5] = (byte) 160 /*0xA0*/;
      numArray5[19] = (byte) 112 /*0x70*/;
      numArray5[7] = (byte) 1;
      numArray5[46] = (byte) 107;
      numArray5[15] = (byte) 18;
      numArray5[42] = (byte) 87;
      numArray5[8] = (byte) 69;
      numArray5[12] = (byte) 97;
      numArray5[9] = (byte) 217;
      numArray5[14] = (byte) 164;
      numArray5[21] = (byte) 209;
      numArray5[2] = (byte) 145;
      numArray5[17] = (byte) 6;
      numArray5[18] = (byte) 159;
      numArray5[23] = (byte) 132;
      numArray5[10] = (byte) 67;
      numArray5[32 /*0x20*/] = (byte) 182;
      numArray5[43] = (byte) 10;
      numArray5[20] = (byte) 66;
      numArray5[24] = (byte) 111;
      numArray5[25] = (byte) 118;
      numArray5[16 /*0x10*/] = (byte) 77;
      numArray5[27] = (byte) 249;
      numArray5[28] = (byte) 34;
      numArray5[48 /*0x30*/] = (byte) 92;
      numArray5[30] = (byte) 3;
      numArray5[36] = (byte) 251;
      numArray5[31 /*0x1F*/] = (byte) 102;
      numArray5[34] = (byte) 184;
      numArray5[11] = (byte) 158;
      numArray5[35] = (byte) 87;
      numArray5[22] = (byte) 254;
      numArray5[37] = (byte) 85;
      numArray5[40] = (byte) 103;
      numArray5[39] = (byte) 25;
      numArray5[29] = (byte) 168;
      numArray5[41] = (byte) 115;
      numArray5[26] = (byte) 146;
      numArray5[33] = (byte) 147;
      numArray5[44] = (byte) 51;
      numArray5[45] = (byte) 71;
      numArray5[0] = (byte) 21;
      numArray5[47] = (byte) 138;
      numArray5[38] = (byte) 249;
      numArray5[49] = (byte) 209;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 50);
      for (int index = 0; index < 50; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[19];
      byte[] response = new byte[19];
      Array.Copy((Array) sc_12780.sspq, 603, (Array) numArray6, 0, 19);
      key.Query(true, 335, numArray6, response);
      Array.Copy((Array) sc_12780.sspr, 603, (Array) numArray6, 0, 19);
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
    byte[] numArray7 = new byte[105];
    byte[] numArray8 = new byte[55]
    {
      (byte) 5,
      (byte) 2,
      (byte) 254,
      (byte) 143,
      (byte) 126,
      (byte) 70,
      (byte) 48 /*0x30*/,
      (byte) 253,
      (byte) 11,
      (byte) 250,
      (byte) 108,
      (byte) 96 /*0x60*/,
      (byte) 210,
      (byte) 137,
      (byte) 236,
      (byte) 30,
      (byte) 7,
      (byte) 118,
      (byte) 56,
      (byte) 34,
      (byte) 72,
      (byte) 131,
      (byte) 178,
      (byte) 161,
      (byte) 82,
      (byte) 56,
      (byte) 137,
      (byte) 164,
      (byte) 22,
      (byte) 139,
      (byte) 142,
      (byte) 81,
      (byte) 21,
      (byte) 218,
      (byte) 181,
      (byte) 16 /*0x10*/,
      (byte) 32 /*0x20*/,
      (byte) 15,
      (byte) 59,
      (byte) 127 /*0x7F*/,
      (byte) 172,
      (byte) 129,
      (byte) 83,
      (byte) 148,
      (byte) 228,
      (byte) 46,
      (byte) 112 /*0x70*/,
      (byte) 210,
      (byte) 141,
      (byte) 233,
      (byte) 148,
      (byte) 253,
      (byte) 60,
      (byte) 244,
      (byte) 91
    };
    byte[] numArray9 = new byte[55];
    numArray9[38] = (byte) 207;
    numArray9[31 /*0x1F*/] = (byte) 146;
    numArray9[2] = (byte) 109;
    numArray9[3] = (byte) 181;
    numArray9[4] = (byte) 83;
    numArray9[15] = (byte) 173;
    numArray9[6] = (byte) 20;
    numArray9[7] = (byte) 140;
    numArray9[8] = (byte) 197;
    numArray9[9] = (byte) 87;
    numArray9[10] = (byte) 119;
    numArray9[0] = (byte) 117;
    numArray9[12] = (byte) 119;
    numArray9[34] = (byte) 77;
    numArray9[14] = (byte) 107;
    numArray9[27] = (byte) 167;
    numArray9[16 /*0x10*/] = (byte) 240 /*0xF0*/;
    numArray9[17] = (byte) 206;
    numArray9[18] = (byte) 105;
    numArray9[30] = (byte) 237;
    numArray9[20] = (byte) 161;
    numArray9[24] = (byte) 65;
    numArray9[22] = (byte) 28;
    numArray9[51] = (byte) 169;
    numArray9[23] = (byte) 202;
    numArray9[48 /*0x30*/] = (byte) 106;
    numArray9[26] = (byte) 81;
    numArray9[42] = (byte) 35;
    numArray9[28] = (byte) 85;
    numArray9[33] = (byte) 169;
    numArray9[25] = (byte) 75;
    numArray9[29] = (byte) 7;
    numArray9[32 /*0x20*/] = (byte) 215;
    numArray9[43] = (byte) 232;
    numArray9[52] = (byte) 241;
    numArray9[5] = (byte) 122;
    numArray9[1] = (byte) 122;
    numArray9[37] = (byte) 103;
    numArray9[44] = (byte) 223;
    numArray9[39] = (byte) 141;
    numArray9[40] = (byte) 215;
    numArray9[41] = (byte) 194;
    numArray9[13] = (byte) 203;
    numArray9[19] = (byte) 213;
    numArray9[36] = (byte) 129;
    numArray9[45] = (byte) 38;
    numArray9[46] = (byte) 243;
    numArray9[35] = (byte) 0;
    numArray9[47] = byte.MaxValue;
    numArray9[49] = (byte) 122;
    numArray9[50] = (byte) 141;
    numArray9[11] = (byte) 238;
    numArray9[53] = (byte) 177;
    numArray9[21] = (byte) 189;
    numArray9[54] = (byte) 152;
    key.Query(true, 335, numArray8, numArray8);
    Array.Copy((Array) numArray8, 0, (Array) numArray7, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray7[index] ^= numArray9[index];
    byte[] numArray10 = new byte[50]
    {
      (byte) 159,
      (byte) 24,
      (byte) 126,
      (byte) 5,
      (byte) 117,
      (byte) 185,
      (byte) 137,
      (byte) 218,
      (byte) 5,
      (byte) 41,
      (byte) 121,
      (byte) 160 /*0xA0*/,
      (byte) 28,
      (byte) 58,
      (byte) 37,
      (byte) 69,
      (byte) 245,
      (byte) 85,
      (byte) 43,
      (byte) 92,
      (byte) 245,
      (byte) 243,
      (byte) 63 /*0x3F*/,
      (byte) 78,
      (byte) 184,
      (byte) 102,
      (byte) 65,
      (byte) 89,
      (byte) 207,
      (byte) 146,
      (byte) 163,
      (byte) 234,
      (byte) 225,
      (byte) 142,
      (byte) 35,
      (byte) 185,
      (byte) 118,
      (byte) 189,
      (byte) 171,
      (byte) 225,
      (byte) 73,
      (byte) 78,
      (byte) 195,
      (byte) 186,
      byte.MaxValue,
      (byte) 93,
      (byte) 148,
      (byte) 176 /*0xB0*/,
      (byte) 119,
      (byte) 124
    };
    byte[] numArray11 = new byte[50]
    {
      (byte) 110,
      (byte) 80 /*0x50*/,
      (byte) 10,
      (byte) 96 /*0x60*/,
      (byte) 151,
      (byte) 107,
      (byte) 163,
      (byte) 183,
      (byte) 64 /*0x40*/,
      (byte) 170,
      (byte) 214,
      (byte) 108,
      (byte) 125,
      (byte) 41,
      (byte) 0,
      (byte) 64 /*0x40*/,
      (byte) 221,
      (byte) 51,
      (byte) 121,
      (byte) 102,
      (byte) 108,
      (byte) 202,
      (byte) 161,
      (byte) 138,
      (byte) 182,
      (byte) 121,
      (byte) 65,
      (byte) 20,
      (byte) 185,
      (byte) 21,
      (byte) 166,
      (byte) 140,
      (byte) 75,
      (byte) 114,
      (byte) 158,
      (byte) 239,
      (byte) 217,
      (byte) 106,
      (byte) 59,
      (byte) 170,
      (byte) 254,
      (byte) 70,
      (byte) 74,
      (byte) 78,
      (byte) 232,
      (byte) 247,
      (byte) 166,
      (byte) 243,
      (byte) 243,
      (byte) 89
    };
    key.Query(true, 335, numArray10, numArray10);
    Array.Copy((Array) numArray10, 0, (Array) numArray7, 55, 50);
    for (int index = 0; index < 50; ++index)
      numArray7[index + 55] ^= numArray11[index];
    return Encoding.UTF8.GetString(numArray7);
  }

  internal static string ssp_appserver_12823()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[63 /*0x3F*/];
      byte[] numArray2 = new byte[55]
      {
        (byte) 60,
        (byte) 171,
        (byte) 217,
        (byte) 47,
        (byte) 77,
        (byte) 14,
        (byte) 120,
        (byte) 47,
        (byte) 11,
        (byte) 115,
        (byte) 134,
        (byte) 142,
        (byte) 191,
        (byte) 141,
        (byte) 23,
        (byte) 107,
        (byte) 206,
        (byte) 56,
        (byte) 169,
        (byte) 74,
        (byte) 18,
        (byte) 167,
        (byte) 47,
        (byte) 133,
        (byte) 79,
        (byte) 90,
        (byte) 209,
        (byte) 144 /*0x90*/,
        (byte) 226,
        (byte) 82,
        (byte) 4,
        (byte) 123,
        (byte) 129,
        (byte) 236,
        (byte) 82,
        (byte) 119,
        (byte) 224 /*0xE0*/,
        (byte) 71,
        (byte) 112 /*0x70*/,
        (byte) 156,
        (byte) 105,
        (byte) 26,
        (byte) 67,
        (byte) 80 /*0x50*/,
        (byte) 52,
        (byte) 112 /*0x70*/,
        (byte) 158,
        (byte) 7,
        (byte) 153,
        (byte) 11,
        (byte) 173,
        (byte) 84,
        (byte) 126,
        (byte) 31 /*0x1F*/,
        (byte) 225
      };
      byte[] numArray3 = new byte[55];
      numArray3[11] = (byte) 122;
      numArray3[1] = (byte) 52;
      numArray3[21] = (byte) 203;
      numArray3[3] = (byte) 79;
      numArray3[19] = (byte) 87;
      numArray3[5] = (byte) 118;
      numArray3[30] = (byte) 12;
      numArray3[22] = (byte) 48 /*0x30*/;
      numArray3[47] = (byte) 193;
      numArray3[44] = (byte) 8;
      numArray3[39] = (byte) 79;
      numArray3[24] = (byte) 195;
      numArray3[12] = (byte) 137;
      numArray3[13] = (byte) 218;
      numArray3[14] = (byte) 172;
      numArray3[15] = (byte) 1;
      numArray3[16 /*0x10*/] = (byte) 160 /*0xA0*/;
      numArray3[17] = (byte) 56;
      numArray3[18] = (byte) 233;
      numArray3[2] = (byte) 98;
      numArray3[20] = (byte) 97;
      numArray3[10] = (byte) 99;
      numArray3[49] = (byte) 118;
      numArray3[28] = (byte) 193;
      numArray3[51] = (byte) 13;
      numArray3[23] = (byte) 137;
      numArray3[26] = (byte) 123;
      numArray3[38] = (byte) 223;
      numArray3[34] = (byte) 133;
      numArray3[42] = (byte) 238;
      numArray3[6] = (byte) 215;
      numArray3[31 /*0x1F*/] = (byte) 209;
      numArray3[32 /*0x20*/] = (byte) 155;
      numArray3[33] = (byte) 4;
      numArray3[52] = (byte) 137;
      numArray3[7] = (byte) 190;
      numArray3[8] = (byte) 108;
      numArray3[37] = (byte) 126;
      numArray3[53] = (byte) 130;
      numArray3[25] = (byte) 10;
      numArray3[40] = (byte) 180;
      numArray3[41] = (byte) 243;
      numArray3[54] = (byte) 222;
      numArray3[29] = (byte) 206;
      numArray3[27] = (byte) 248;
      numArray3[45] = (byte) 143;
      numArray3[36] = (byte) 81;
      numArray3[43] = (byte) 74;
      numArray3[48 /*0x30*/] = (byte) 30;
      numArray3[35] = (byte) 110;
      numArray3[50] = (byte) 51;
      numArray3[9] = (byte) 193;
      numArray3[46] = (byte) 123;
      numArray3[4] = (byte) 26;
      numArray3[0] = (byte) 53;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[8];
      numArray4[6] = (byte) 20;
      numArray4[0] = (byte) 2;
      numArray4[1] = (byte) 0;
      numArray4[3] = (byte) 115;
      numArray4[7] = (byte) 191;
      numArray4[5] = (byte) 119;
      numArray4[4] = (byte) 240 /*0xF0*/;
      numArray4[2] = (byte) 53;
      byte[] numArray5 = new byte[8]
      {
        (byte) 239,
        (byte) 252,
        (byte) 165,
        (byte) 131,
        (byte) 26,
        (byte) 20,
        (byte) 60,
        (byte) 43
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
      (byte) 217,
      (byte) 254,
      (byte) 39,
      (byte) 124,
      (byte) 240 /*0xF0*/,
      (byte) 252,
      (byte) 252,
      (byte) 85,
      (byte) 126,
      (byte) 34,
      (byte) 139,
      (byte) 35,
      (byte) 33,
      (byte) 140,
      (byte) 95,
      (byte) 137,
      (byte) 102,
      (byte) 73,
      (byte) 235,
      (byte) 145,
      (byte) 84,
      (byte) 117,
      (byte) 33,
      (byte) 27,
      (byte) 182,
      (byte) 3,
      (byte) 79,
      (byte) 87,
      (byte) 130,
      (byte) 6,
      (byte) 67,
      (byte) 195,
      (byte) 224 /*0xE0*/,
      (byte) 188,
      (byte) 86,
      (byte) 157,
      (byte) 219,
      (byte) 132,
      (byte) 68,
      (byte) 118,
      (byte) 243,
      (byte) 34,
      (byte) 150,
      (byte) 156,
      (byte) 130,
      (byte) 136,
      (byte) 166,
      (byte) 240 /*0xF0*/,
      (byte) 67,
      (byte) 116,
      (byte) 185,
      (byte) 0,
      (byte) 194,
      (byte) 204,
      (byte) 215
    };
    byte[] numArray8 = new byte[55];
    numArray8[7] = (byte) 106;
    numArray8[1] = (byte) 226;
    numArray8[45] = (byte) 235;
    numArray8[10] = (byte) 207;
    numArray8[14] = (byte) 59;
    numArray8[33] = (byte) 116;
    numArray8[31 /*0x1F*/] = (byte) 154;
    numArray8[41] = (byte) 196;
    numArray8[8] = (byte) 136;
    numArray8[9] = (byte) 188;
    numArray8[6] = (byte) 235;
    numArray8[30] = (byte) 233;
    numArray8[44] = (byte) 188;
    numArray8[4] = (byte) 184;
    numArray8[40] = (byte) 79;
    numArray8[12] = (byte) 16 /*0x10*/;
    numArray8[26] = (byte) 224 /*0xE0*/;
    numArray8[17] = (byte) 215;
    numArray8[29] = (byte) 125;
    numArray8[0] = (byte) 31 /*0x1F*/;
    numArray8[32 /*0x20*/] = (byte) 125;
    numArray8[52] = (byte) 63 /*0x3F*/;
    numArray8[13] = (byte) 39;
    numArray8[23] = (byte) 204;
    numArray8[3] = (byte) 35;
    numArray8[25] = (byte) 5;
    numArray8[18] = (byte) 106;
    numArray8[27] = (byte) 145;
    numArray8[28] = (byte) 125;
    numArray8[5] = (byte) 161;
    numArray8[38] = (byte) 165;
    numArray8[53] = (byte) 245;
    numArray8[11] = (byte) 182;
    numArray8[43] = (byte) 39;
    numArray8[34] = (byte) 104;
    numArray8[21] = (byte) 107;
    numArray8[36] = (byte) 63 /*0x3F*/;
    numArray8[37] = (byte) 136;
    numArray8[24] = (byte) 11;
    numArray8[39] = (byte) 42;
    numArray8[22] = (byte) 81;
    numArray8[48 /*0x30*/] = (byte) 182;
    numArray8[16 /*0x10*/] = (byte) 134;
    numArray8[35] = (byte) 205;
    numArray8[42] = (byte) 152;
    numArray8[47] = (byte) 253;
    numArray8[46] = (byte) 101;
    numArray8[15] = (byte) 161;
    numArray8[19] = (byte) 224 /*0xE0*/;
    numArray8[49] = (byte) 60;
    numArray8[2] = (byte) 215;
    numArray8[51] = (byte) 41;
    numArray8[50] = (byte) 135;
    numArray8[20] = (byte) 12;
    numArray8[54] = (byte) 58;
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[8]
    {
      (byte) 113,
      (byte) 151,
      (byte) 144 /*0x90*/,
      (byte) 78,
      (byte) 145,
      (byte) 195,
      (byte) 42,
      (byte) 156
    };
    byte[] numArray10 = new byte[8]
    {
      (byte) 118,
      (byte) 105,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 12,
      (byte) 0,
      (byte) 204
    };
    numArray10[2] = (byte) 19;
    numArray10[4] = (byte) 236;
    numArray10[6] = (byte) 117;
    numArray10[3] = (byte) 108;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 8);
    for (int index = 0; index < 8; ++index)
      numArray6[index + 55] ^= numArray10[index];
    byte[] numArray11 = new byte[14];
    byte[] response = new byte[14];
    Array.Copy((Array) sc_12780.sspq, 622, (Array) numArray11, 0, 14);
    key.Query(true, 335, numArray11, response);
    Array.Copy((Array) sc_12780.sspr, 622, (Array) numArray11, 0, 14);
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

  internal static string ssp_appserver_12824()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[57];
      byte[] numArray2 = new byte[55]
      {
        (byte) 38,
        (byte) 198,
        (byte) 123,
        (byte) 188,
        (byte) 141,
        (byte) 244,
        (byte) 32 /*0x20*/,
        (byte) 242,
        (byte) 21,
        (byte) 194,
        (byte) 119,
        (byte) 127 /*0x7F*/,
        (byte) 232,
        (byte) 180,
        (byte) 78,
        (byte) 161,
        (byte) 222,
        (byte) 195,
        (byte) 195,
        (byte) 142,
        (byte) 145,
        (byte) 175,
        (byte) 116,
        (byte) 169,
        byte.MaxValue,
        (byte) 122,
        (byte) 180,
        (byte) 85,
        (byte) 18,
        (byte) 75,
        (byte) 63 /*0x3F*/,
        (byte) 212,
        (byte) 151,
        (byte) 126,
        (byte) 252,
        (byte) 230,
        (byte) 48 /*0x30*/,
        (byte) 147,
        (byte) 84,
        (byte) 204,
        (byte) 167,
        (byte) 237,
        (byte) 211,
        (byte) 89,
        (byte) 44,
        (byte) 18,
        (byte) 96 /*0x60*/,
        (byte) 193,
        (byte) 248,
        (byte) 19,
        (byte) 6,
        (byte) 125,
        (byte) 171,
        (byte) 219,
        (byte) 205
      };
      byte[] numArray3 = new byte[55];
      numArray3[50] = (byte) 163;
      numArray3[24] = (byte) 224 /*0xE0*/;
      numArray3[4] = (byte) 105;
      numArray3[38] = (byte) 71;
      numArray3[30] = (byte) 213;
      numArray3[5] = (byte) 229;
      numArray3[6] = (byte) 209;
      numArray3[7] = (byte) 61;
      numArray3[2] = (byte) 178;
      numArray3[44] = (byte) 53;
      numArray3[45] = (byte) 28;
      numArray3[11] = (byte) 124;
      numArray3[9] = (byte) 157;
      numArray3[43] = (byte) 78;
      numArray3[14] = (byte) 92;
      numArray3[15] = (byte) 248;
      numArray3[16 /*0x10*/] = (byte) 150;
      numArray3[17] = (byte) 44;
      numArray3[18] = (byte) 126;
      numArray3[48 /*0x30*/] = (byte) 53;
      numArray3[10] = (byte) 23;
      numArray3[12] = (byte) 231;
      numArray3[21] = (byte) 198;
      numArray3[23] = (byte) 188;
      numArray3[34] = (byte) 112 /*0x70*/;
      numArray3[22] = (byte) 220;
      numArray3[26] = (byte) 213;
      numArray3[27] = (byte) 35;
      numArray3[28] = (byte) 161;
      numArray3[29] = (byte) 248;
      numArray3[52] = (byte) 167;
      numArray3[31 /*0x1F*/] = (byte) 23;
      numArray3[32 /*0x20*/] = (byte) 90;
      numArray3[33] = (byte) 176 /*0xB0*/;
      numArray3[0] = (byte) 83;
      numArray3[13] = (byte) 131;
      numArray3[8] = (byte) 105;
      numArray3[37] = (byte) 134;
      numArray3[49] = (byte) 76;
      numArray3[39] = (byte) 40;
      numArray3[40] = (byte) 253;
      numArray3[41] = (byte) 252;
      numArray3[42] = (byte) 155;
      numArray3[25] = (byte) 27;
      numArray3[1] = (byte) 247;
      numArray3[20] = (byte) 143;
      numArray3[46] = (byte) 156;
      numArray3[47] = (byte) 188;
      numArray3[36] = (byte) 250;
      numArray3[19] = (byte) 25;
      numArray3[35] = (byte) 46;
      numArray3[51] = (byte) 74;
      numArray3[3] = (byte) 148;
      numArray3[53] = (byte) 150;
      numArray3[54] = (byte) 64 /*0x40*/;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[2]
      {
        (byte) 84,
        (byte) 207
      };
      byte[] numArray5 = new byte[2]{ (byte) 0, (byte) 131 };
      numArray5[0] = (byte) 1;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 2);
      for (int index = 0; index < 2; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[57];
    byte[] numArray7 = new byte[55]
    {
      (byte) 192 /*0xC0*/,
      (byte) 122,
      (byte) 43,
      (byte) 184,
      (byte) 50,
      (byte) 210,
      (byte) 18,
      (byte) 115,
      (byte) 43,
      (byte) 105,
      (byte) 10,
      (byte) 148,
      (byte) 210,
      (byte) 69,
      (byte) 244,
      (byte) 194,
      (byte) 35,
      (byte) 232,
      (byte) 203,
      (byte) 93,
      (byte) 137,
      (byte) 179,
      (byte) 115,
      (byte) 200,
      (byte) 134,
      (byte) 233,
      (byte) 94,
      (byte) 9,
      (byte) 7,
      (byte) 74,
      (byte) 64 /*0x40*/,
      (byte) 109,
      (byte) 47,
      (byte) 131,
      (byte) 21,
      (byte) 14,
      byte.MaxValue,
      (byte) 62,
      (byte) 215,
      (byte) 190,
      (byte) 143,
      (byte) 72,
      (byte) 227,
      (byte) 75,
      (byte) 233,
      (byte) 149,
      (byte) 200,
      (byte) 240 /*0xF0*/,
      (byte) 96 /*0x60*/,
      (byte) 13,
      (byte) 212,
      (byte) 100,
      (byte) 163,
      (byte) 129,
      (byte) 252
    };
    byte[] numArray8 = new byte[55];
    numArray8[34] = (byte) 219;
    numArray8[19] = (byte) 146;
    numArray8[33] = (byte) 198;
    numArray8[3] = (byte) 36;
    numArray8[4] = (byte) 186;
    numArray8[5] = (byte) 203;
    numArray8[37] = (byte) 223;
    numArray8[23] = (byte) 33;
    numArray8[8] = (byte) 55;
    numArray8[9] = (byte) 190;
    numArray8[10] = (byte) 124;
    numArray8[7] = (byte) 222;
    numArray8[21] = (byte) 133;
    numArray8[12] = (byte) 217;
    numArray8[13] = (byte) 249;
    numArray8[50] = (byte) 12;
    numArray8[18] = (byte) 170;
    numArray8[17] = (byte) 36;
    numArray8[42] = (byte) 15;
    numArray8[27] = (byte) 44;
    numArray8[25] = (byte) 246;
    numArray8[52] = (byte) 114;
    numArray8[22] = (byte) 82;
    numArray8[24] = (byte) 169;
    numArray8[1] = (byte) 180;
    numArray8[20] = (byte) 75;
    numArray8[6] = (byte) 201;
    numArray8[45] = (byte) 8;
    numArray8[28] = (byte) 62;
    numArray8[46] = (byte) 199;
    numArray8[30] = (byte) 211;
    numArray8[31 /*0x1F*/] = (byte) 64 /*0x40*/;
    numArray8[32 /*0x20*/] = (byte) 226;
    numArray8[39] = (byte) 238;
    numArray8[16 /*0x10*/] = (byte) 3;
    numArray8[35] = (byte) 92;
    numArray8[36] = (byte) 245;
    numArray8[11] = (byte) 110;
    numArray8[26] = (byte) 17;
    numArray8[38] = (byte) 188;
    numArray8[40] = (byte) 106;
    numArray8[41] = (byte) 35;
    numArray8[44] = (byte) 140;
    numArray8[43] = (byte) 144 /*0x90*/;
    numArray8[49] = (byte) 83;
    numArray8[2] = (byte) 51;
    numArray8[15] = (byte) 103;
    numArray8[47] = (byte) 18;
    numArray8[48 /*0x30*/] = (byte) 158;
    numArray8[29] = (byte) 150;
    numArray8[0] = (byte) 110;
    numArray8[51] = (byte) 38;
    numArray8[14] = (byte) 140;
    numArray8[53] = (byte) 192 /*0xC0*/;
    numArray8[54] = (byte) 39;
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[2]
    {
      (byte) 132,
      (byte) 64 /*0x40*/
    };
    byte[] numArray10 = new byte[2]
    {
      (byte) 147,
      (byte) 164
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 2);
    for (int index = 0; index < 2; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_12825()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[80 /*0x50*/];
      byte[] numArray2 = new byte[55];
      numArray2[28] = (byte) 24;
      numArray2[53] = (byte) 48 /*0x30*/;
      numArray2[27] = (byte) 40;
      numArray2[3] = (byte) 184;
      numArray2[4] = (byte) 199;
      numArray2[5] = (byte) 40;
      numArray2[6] = (byte) 223;
      numArray2[7] = (byte) 38;
      numArray2[2] = (byte) 210;
      numArray2[48 /*0x30*/] = (byte) 216;
      numArray2[9] = (byte) 131;
      numArray2[11] = (byte) 72;
      numArray2[20] = (byte) 175;
      numArray2[13] = (byte) 206;
      numArray2[37] = (byte) 119;
      numArray2[15] = (byte) 145;
      numArray2[16 /*0x10*/] = (byte) 193;
      numArray2[25] = (byte) 204;
      numArray2[18] = (byte) 174;
      numArray2[36] = (byte) 123;
      numArray2[14] = (byte) 5;
      numArray2[21] = (byte) 135;
      numArray2[22] = (byte) 136;
      numArray2[10] = (byte) 194;
      numArray2[43] = (byte) 207;
      numArray2[24] = (byte) 250;
      numArray2[0] = (byte) 107;
      numArray2[23] = (byte) 52;
      numArray2[29] = (byte) 204;
      numArray2[1] = (byte) 77;
      numArray2[17] = (byte) 167;
      numArray2[31 /*0x1F*/] = (byte) 69;
      numArray2[32 /*0x20*/] = (byte) 38;
      numArray2[33] = (byte) 107;
      numArray2[34] = (byte) 150;
      numArray2[12] = (byte) 99;
      numArray2[40] = (byte) 154;
      numArray2[46] = (byte) 87;
      numArray2[38] = (byte) 177;
      numArray2[39] = (byte) 151;
      numArray2[35] = (byte) 212;
      numArray2[41] = (byte) 35;
      numArray2[42] = (byte) 21;
      numArray2[47] = (byte) 110;
      numArray2[44] = (byte) 210;
      numArray2[26] = (byte) 202;
      numArray2[52] = (byte) 86;
      numArray2[8] = (byte) 217;
      numArray2[19] = (byte) 47;
      numArray2[49] = (byte) 242;
      numArray2[50] = (byte) 126;
      numArray2[51] = (byte) 129;
      numArray2[45] = (byte) 106;
      numArray2[30] = (byte) 101;
      numArray2[54] = (byte) 193;
      byte[] numArray3 = new byte[55]
      {
        (byte) 34,
        (byte) 194,
        (byte) 40,
        (byte) 11,
        (byte) 66,
        (byte) 197,
        (byte) 105,
        (byte) 172,
        (byte) 229,
        (byte) 152,
        (byte) 36,
        (byte) 236,
        (byte) 230,
        (byte) 111,
        (byte) 228,
        (byte) 178,
        (byte) 116,
        (byte) 190,
        (byte) 212,
        (byte) 0,
        (byte) 205,
        (byte) 23,
        (byte) 0,
        (byte) 187,
        (byte) 77,
        (byte) 106,
        (byte) 14,
        (byte) 36,
        (byte) 210,
        (byte) 156,
        (byte) 178,
        (byte) 70,
        (byte) 142,
        (byte) 182,
        (byte) 156,
        (byte) 99,
        (byte) 219,
        (byte) 166,
        (byte) 127 /*0x7F*/,
        (byte) 129,
        (byte) 22,
        byte.MaxValue,
        (byte) 250,
        (byte) 40,
        (byte) 10,
        (byte) 217,
        (byte) 102,
        (byte) 50,
        (byte) 103,
        (byte) 54,
        (byte) 97,
        (byte) 146,
        (byte) 162,
        (byte) 34,
        (byte) 111
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[25];
      numArray4[9] = (byte) 42;
      numArray4[4] = (byte) 102;
      numArray4[2] = (byte) 135;
      numArray4[3] = (byte) 1;
      numArray4[22] = (byte) 71;
      numArray4[5] = (byte) 232;
      numArray4[6] = (byte) 191;
      numArray4[14] = (byte) 143;
      numArray4[0] = (byte) 59;
      numArray4[7] = (byte) 57;
      numArray4[11] = (byte) 133;
      numArray4[16 /*0x10*/] = (byte) 220;
      numArray4[13] = (byte) 56;
      numArray4[12] = (byte) 32 /*0x20*/;
      numArray4[17] = (byte) 176 /*0xB0*/;
      numArray4[24] = (byte) 37;
      numArray4[15] = (byte) 63 /*0x3F*/;
      numArray4[19] = (byte) 106;
      numArray4[1] = (byte) 44;
      numArray4[18] = (byte) 118;
      numArray4[20] = (byte) 7;
      numArray4[21] = (byte) 12;
      numArray4[8] = (byte) 143;
      numArray4[23] = (byte) 20;
      numArray4[10] = (byte) 77;
      byte[] numArray5 = new byte[25]
      {
        (byte) 197,
        (byte) 105,
        (byte) 213,
        (byte) 220,
        (byte) 107,
        (byte) 171,
        (byte) 63 /*0x3F*/,
        (byte) 94,
        (byte) 126,
        (byte) 75,
        (byte) 136,
        (byte) 206,
        (byte) 150,
        (byte) 225,
        (byte) 22,
        (byte) 34,
        (byte) 144 /*0x90*/,
        (byte) 21,
        (byte) 145,
        (byte) 180,
        (byte) 211,
        (byte) 91,
        (byte) 249,
        (byte) 56,
        (byte) 202
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 25);
      for (int index = 0; index < 25; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[80 /*0x50*/];
    byte[] numArray7 = new byte[55]
    {
      (byte) 7,
      (byte) 4,
      (byte) 46,
      (byte) 229,
      (byte) 115,
      (byte) 220,
      (byte) 191,
      (byte) 160 /*0xA0*/,
      (byte) 165,
      (byte) 224 /*0xE0*/,
      (byte) 225,
      (byte) 207,
      (byte) 216,
      (byte) 68,
      (byte) 27,
      (byte) 149,
      (byte) 1,
      (byte) 16 /*0x10*/,
      (byte) 163,
      (byte) 50,
      (byte) 73,
      (byte) 33,
      (byte) 142,
      (byte) 70,
      (byte) 41,
      (byte) 1,
      (byte) 115,
      (byte) 32 /*0x20*/,
      (byte) 164,
      (byte) 116,
      (byte) 12,
      (byte) 73,
      (byte) 111,
      (byte) 181,
      (byte) 57,
      (byte) 0,
      (byte) 116,
      (byte) 168,
      (byte) 187,
      (byte) 92,
      (byte) 190,
      (byte) 140,
      (byte) 163,
      (byte) 151,
      (byte) 227,
      (byte) 79,
      (byte) 58,
      (byte) 109,
      (byte) 108,
      (byte) 15,
      (byte) 163,
      (byte) 59,
      (byte) 193,
      (byte) 185,
      (byte) 117
    };
    byte[] numArray8 = new byte[55]
    {
      (byte) 77,
      (byte) 73,
      (byte) 131,
      (byte) 152,
      (byte) 209,
      (byte) 112 /*0x70*/,
      (byte) 249,
      (byte) 175,
      (byte) 120,
      (byte) 149,
      (byte) 224 /*0xE0*/,
      (byte) 231,
      (byte) 43,
      (byte) 7,
      (byte) 57,
      (byte) 13,
      (byte) 119,
      (byte) 105,
      (byte) 170,
      (byte) 214,
      (byte) 171,
      (byte) 53,
      (byte) 204,
      (byte) 163,
      (byte) 183,
      (byte) 183,
      (byte) 50,
      (byte) 34,
      (byte) 83,
      (byte) 223,
      (byte) 200,
      (byte) 186,
      (byte) 221,
      (byte) 12,
      (byte) 16 /*0x10*/,
      (byte) 52,
      (byte) 208 /*0xD0*/,
      (byte) 229,
      (byte) 23,
      (byte) 154,
      (byte) 208 /*0xD0*/,
      (byte) 30,
      (byte) 215,
      (byte) 123,
      (byte) 56,
      (byte) 144 /*0x90*/,
      (byte) 253,
      (byte) 246,
      (byte) 110,
      (byte) 132,
      (byte) 245,
      (byte) 85,
      (byte) 143,
      (byte) 149,
      (byte) 78
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[25]
    {
      (byte) 231,
      (byte) 188,
      (byte) 113,
      (byte) 104,
      (byte) 159,
      (byte) 252,
      (byte) 49,
      (byte) 212,
      (byte) 152,
      (byte) 95,
      (byte) 34,
      (byte) 10,
      (byte) 26,
      (byte) 33,
      (byte) 89,
      (byte) 85,
      (byte) 70,
      (byte) 115,
      (byte) 189,
      (byte) 125,
      (byte) 246,
      (byte) 99,
      (byte) 218,
      (byte) 199,
      (byte) 6
    };
    byte[] numArray10 = new byte[25];
    numArray10[19] = (byte) 45;
    numArray10[9] = (byte) 104;
    numArray10[24] = (byte) 119;
    numArray10[6] = (byte) 190;
    numArray10[0] = (byte) 178;
    numArray10[20] = (byte) 185;
    numArray10[15] = (byte) 121;
    numArray10[7] = (byte) 127 /*0x7F*/;
    numArray10[8] = (byte) 79;
    numArray10[3] = (byte) 107;
    numArray10[10] = (byte) 77;
    numArray10[11] = (byte) 85;
    numArray10[12] = (byte) 32 /*0x20*/;
    numArray10[1] = (byte) 242;
    numArray10[14] = (byte) 1;
    numArray10[4] = (byte) 33;
    numArray10[23] = (byte) 188;
    numArray10[17] = (byte) 3;
    numArray10[2] = (byte) 34;
    numArray10[5] = (byte) 63 /*0x3F*/;
    numArray10[13] = (byte) 230;
    numArray10[21] = (byte) 239;
    numArray10[22] = (byte) 229;
    numArray10[18] = (byte) 56;
    numArray10[16 /*0x10*/] = (byte) 28;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 25);
    for (int index = 0; index < 25; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }
}
