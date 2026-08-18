// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_14238
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_14238
{
  private static byte[] sspq = new byte[527]
  {
    (byte) 1,
    (byte) 112 /*0x70*/,
    (byte) 72,
    (byte) 168,
    (byte) 243,
    (byte) 204,
    (byte) 90,
    (byte) 158,
    (byte) 5,
    (byte) 5,
    (byte) 28,
    (byte) 148,
    (byte) 226,
    (byte) 107,
    (byte) 148,
    (byte) 98,
    (byte) 117,
    (byte) 233,
    (byte) 66,
    (byte) 161,
    (byte) 223,
    (byte) 65,
    (byte) 178,
    (byte) 70,
    (byte) 79,
    (byte) 116,
    (byte) 190,
    (byte) 84,
    (byte) 235,
    (byte) 112 /*0x70*/,
    (byte) 111,
    (byte) 95,
    (byte) 81,
    (byte) 240 /*0xF0*/,
    (byte) 208 /*0xD0*/,
    (byte) 101,
    (byte) 126,
    (byte) 135,
    (byte) 104,
    (byte) 72,
    (byte) 81,
    (byte) 198,
    (byte) 88,
    (byte) 236,
    (byte) 201,
    (byte) 43,
    (byte) 147,
    (byte) 143,
    (byte) 174,
    (byte) 247,
    (byte) 0,
    (byte) 226,
    byte.MaxValue,
    (byte) 21,
    (byte) 157,
    (byte) 234,
    (byte) 127 /*0x7F*/,
    (byte) 12,
    (byte) 13,
    (byte) 8,
    (byte) 222,
    (byte) 165,
    (byte) 181,
    (byte) 46,
    (byte) 226,
    (byte) 252,
    (byte) 246,
    (byte) 126,
    (byte) 156,
    (byte) 40,
    (byte) 140,
    (byte) 10,
    (byte) 41,
    (byte) 87,
    (byte) 44,
    (byte) 220,
    (byte) 221,
    (byte) 69,
    (byte) 58,
    (byte) 110,
    (byte) 223,
    (byte) 123,
    (byte) 183,
    (byte) 10,
    (byte) 114,
    (byte) 90,
    (byte) 246,
    (byte) 243,
    (byte) 134,
    (byte) 106,
    (byte) 245,
    (byte) 185,
    (byte) 167,
    (byte) 233,
    (byte) 192 /*0xC0*/,
    (byte) 12,
    (byte) 58,
    (byte) 210,
    (byte) 159,
    (byte) 41,
    (byte) 99,
    (byte) 238,
    (byte) 171,
    (byte) 187,
    (byte) 135,
    (byte) 5,
    (byte) 9,
    (byte) 159,
    (byte) 100,
    (byte) 103,
    (byte) 15,
    (byte) 145,
    (byte) 219,
    (byte) 83,
    (byte) 99,
    (byte) 19,
    (byte) 195,
    (byte) 69,
    (byte) 1,
    (byte) 146,
    (byte) 45,
    (byte) 92,
    (byte) 40,
    (byte) 231,
    (byte) 34,
    (byte) 190,
    (byte) 36,
    (byte) 144 /*0x90*/,
    (byte) 108,
    (byte) 76,
    (byte) 119,
    (byte) 41,
    (byte) 36,
    (byte) 121,
    (byte) 217,
    (byte) 48 /*0x30*/,
    (byte) 160 /*0xA0*/,
    (byte) 64 /*0x40*/,
    (byte) 33,
    (byte) 55,
    (byte) 208 /*0xD0*/,
    (byte) 14,
    (byte) 98,
    (byte) 171,
    (byte) 215,
    (byte) 71,
    (byte) 104,
    (byte) 47,
    (byte) 146,
    (byte) 152,
    (byte) 224 /*0xE0*/,
    (byte) 76,
    (byte) 72,
    (byte) 239,
    (byte) 34,
    (byte) 241,
    (byte) 180,
    (byte) 232,
    (byte) 73,
    (byte) 86,
    (byte) 64 /*0x40*/,
    (byte) 92,
    (byte) 86,
    (byte) 9,
    (byte) 58,
    (byte) 251,
    (byte) 189,
    (byte) 250,
    (byte) 83,
    (byte) 140,
    (byte) 143,
    (byte) 195,
    byte.MaxValue,
    (byte) 252,
    (byte) 40,
    (byte) 174,
    (byte) 21,
    (byte) 221,
    (byte) 227,
    (byte) 219,
    (byte) 237,
    (byte) 11,
    (byte) 253,
    (byte) 138,
    (byte) 139,
    (byte) 139,
    (byte) 70,
    (byte) 8,
    (byte) 43,
    (byte) 159,
    (byte) 128 /*0x80*/,
    (byte) 105,
    (byte) 94,
    (byte) 239,
    (byte) 113,
    (byte) 27,
    (byte) 2,
    (byte) 201,
    (byte) 113,
    (byte) 180,
    (byte) 22,
    (byte) 218,
    (byte) 74,
    (byte) 6,
    (byte) 228,
    (byte) 167,
    (byte) 150,
    (byte) 82,
    (byte) 69,
    (byte) 128 /*0x80*/,
    (byte) 239,
    (byte) 9,
    (byte) 191,
    (byte) 78,
    (byte) 231,
    (byte) 201,
    (byte) 124,
    (byte) 62,
    (byte) 210,
    (byte) 149,
    (byte) 208 /*0xD0*/,
    (byte) 42,
    (byte) 145,
    (byte) 25,
    (byte) 231,
    (byte) 225,
    (byte) 208 /*0xD0*/,
    (byte) 250,
    (byte) 98,
    (byte) 72,
    (byte) 69,
    (byte) 102,
    (byte) 206,
    (byte) 172,
    (byte) 169,
    (byte) 68,
    (byte) 145,
    (byte) 244,
    (byte) 110,
    (byte) 6,
    (byte) 157,
    (byte) 179,
    (byte) 169,
    (byte) 207,
    (byte) 53,
    (byte) 21,
    (byte) 182,
    byte.MaxValue,
    (byte) 117,
    (byte) 131,
    (byte) 51,
    (byte) 236,
    (byte) 133,
    (byte) 218,
    (byte) 64 /*0x40*/,
    (byte) 159,
    (byte) 94,
    (byte) 202,
    (byte) 85,
    (byte) 63 /*0x3F*/,
    (byte) 216,
    (byte) 217,
    (byte) 122,
    (byte) 135,
    (byte) 202,
    (byte) 94,
    (byte) 18,
    (byte) 252,
    (byte) 130,
    (byte) 78,
    (byte) 114,
    (byte) 82,
    (byte) 11,
    (byte) 38,
    (byte) 201,
    (byte) 178,
    (byte) 224 /*0xE0*/,
    (byte) 117,
    (byte) 235,
    (byte) 190,
    byte.MaxValue,
    (byte) 121,
    (byte) 132,
    (byte) 6,
    (byte) 190,
    (byte) 147,
    (byte) 128 /*0x80*/,
    (byte) 193,
    (byte) 157,
    (byte) 110,
    (byte) 66,
    (byte) 178,
    (byte) 176 /*0xB0*/,
    (byte) 49,
    (byte) 197,
    (byte) 87,
    (byte) 136,
    (byte) 69,
    (byte) 191,
    (byte) 18,
    (byte) 113,
    (byte) 44,
    (byte) 8,
    (byte) 62,
    (byte) 59,
    (byte) 3,
    (byte) 161,
    (byte) 70,
    (byte) 20,
    (byte) 201,
    (byte) 30,
    (byte) 14,
    (byte) 184,
    (byte) 99,
    (byte) 237,
    (byte) 121,
    (byte) 222,
    (byte) 53,
    (byte) 117,
    (byte) 229,
    (byte) 225,
    (byte) 6,
    (byte) 27,
    (byte) 134,
    (byte) 60,
    (byte) 112 /*0x70*/,
    (byte) 200,
    (byte) 48 /*0x30*/,
    (byte) 193,
    (byte) 84,
    (byte) 174,
    (byte) 190,
    (byte) 34,
    (byte) 101,
    (byte) 151,
    (byte) 225,
    (byte) 200,
    (byte) 218,
    (byte) 57,
    (byte) 221,
    (byte) 213,
    (byte) 14,
    (byte) 167,
    (byte) 213,
    (byte) 33,
    (byte) 149,
    (byte) 10,
    (byte) 56,
    (byte) 90,
    (byte) 125,
    (byte) 210,
    (byte) 63 /*0x3F*/,
    (byte) 4,
    (byte) 66,
    (byte) 56,
    (byte) 6,
    (byte) 149,
    (byte) 47,
    (byte) 144 /*0x90*/,
    (byte) 249,
    (byte) 115,
    (byte) 113,
    (byte) 254,
    (byte) 76,
    (byte) 220,
    (byte) 254,
    (byte) 119,
    (byte) 155,
    (byte) 160 /*0xA0*/,
    (byte) 54,
    (byte) 46,
    (byte) 63 /*0x3F*/,
    (byte) 164,
    (byte) 117,
    (byte) 145,
    (byte) 119,
    (byte) 12,
    (byte) 58,
    (byte) 210,
    (byte) 156,
    (byte) 203,
    (byte) 55,
    (byte) 10,
    (byte) 158,
    (byte) 204,
    (byte) 119,
    (byte) 200,
    (byte) 123,
    (byte) 70,
    (byte) 45,
    (byte) 8,
    (byte) 16 /*0x10*/,
    (byte) 119,
    (byte) 191,
    (byte) 63 /*0x3F*/,
    (byte) 40,
    (byte) 62,
    (byte) 51,
    (byte) 107,
    (byte) 226,
    (byte) 196,
    (byte) 0,
    (byte) 8,
    (byte) 44,
    (byte) 83,
    (byte) 51,
    (byte) 77,
    (byte) 136,
    (byte) 83,
    (byte) 146,
    (byte) 110,
    (byte) 137,
    (byte) 79,
    (byte) 68,
    (byte) 164,
    (byte) 194,
    (byte) 75,
    (byte) 216,
    (byte) 164,
    (byte) 163,
    (byte) 48 /*0x30*/,
    (byte) 130,
    (byte) 38,
    (byte) 21,
    (byte) 187,
    (byte) 58,
    (byte) 183,
    (byte) 170,
    (byte) 148,
    (byte) 239,
    (byte) 72,
    (byte) 104,
    (byte) 214,
    (byte) 63 /*0x3F*/,
    (byte) 239,
    (byte) 33,
    (byte) 235,
    (byte) 114,
    (byte) 31 /*0x1F*/,
    (byte) 102,
    (byte) 80 /*0x50*/,
    (byte) 191,
    (byte) 116,
    (byte) 65,
    (byte) 88,
    (byte) 52,
    (byte) 27,
    (byte) 219,
    (byte) 96 /*0x60*/,
    (byte) 37,
    (byte) 13,
    (byte) 195,
    (byte) 183,
    (byte) 247,
    (byte) 7,
    (byte) 131,
    (byte) 81,
    (byte) 242,
    (byte) 164,
    (byte) 117,
    (byte) 229,
    (byte) 97,
    (byte) 176 /*0xB0*/,
    (byte) 79,
    (byte) 61,
    (byte) 240 /*0xF0*/,
    (byte) 214,
    (byte) 194,
    (byte) 78,
    (byte) 136,
    (byte) 50,
    (byte) 172,
    (byte) 252,
    (byte) 248,
    (byte) 89,
    (byte) 217,
    (byte) 47,
    (byte) 201,
    (byte) 80 /*0x50*/,
    (byte) 136,
    (byte) 54,
    (byte) 237,
    (byte) 10,
    (byte) 22,
    (byte) 238,
    (byte) 11,
    (byte) 2,
    (byte) 75,
    (byte) 175,
    (byte) 12,
    (byte) 242,
    (byte) 104,
    (byte) 113,
    (byte) 51,
    (byte) 176 /*0xB0*/,
    (byte) 181,
    (byte) 128 /*0x80*/,
    (byte) 130,
    (byte) 93,
    (byte) 106,
    (byte) 252,
    (byte) 253,
    (byte) 24,
    (byte) 26,
    (byte) 49,
    (byte) 237,
    (byte) 138,
    (byte) 19,
    (byte) 65,
    (byte) 149,
    (byte) 202,
    (byte) 59,
    (byte) 201,
    (byte) 72,
    (byte) 164,
    (byte) 88,
    (byte) 172,
    (byte) 107,
    (byte) 230,
    (byte) 139,
    (byte) 56,
    (byte) 212,
    (byte) 240 /*0xF0*/,
    (byte) 175,
    (byte) 12,
    (byte) 118,
    (byte) 215
  };
  private static byte[] sspr = new byte[527]
  {
    (byte) 120,
    (byte) 84,
    (byte) 119,
    (byte) 58,
    (byte) 137,
    (byte) 228,
    (byte) 196,
    (byte) 178,
    (byte) 206,
    (byte) 13,
    (byte) 128 /*0x80*/,
    (byte) 27,
    (byte) 140,
    (byte) 246,
    (byte) 89,
    (byte) 13,
    (byte) 8,
    (byte) 208 /*0xD0*/,
    (byte) 189,
    (byte) 82,
    (byte) 78,
    (byte) 242,
    (byte) 229,
    (byte) 245,
    (byte) 157,
    (byte) 67,
    (byte) 104,
    (byte) 118,
    (byte) 111,
    (byte) 2,
    (byte) 245,
    (byte) 212,
    (byte) 44,
    (byte) 250,
    (byte) 140,
    (byte) 232,
    (byte) 238,
    (byte) 38,
    (byte) 228,
    (byte) 183,
    (byte) 6,
    (byte) 18,
    (byte) 20,
    (byte) 235,
    (byte) 37,
    (byte) 180,
    (byte) 227,
    (byte) 211,
    (byte) 157,
    (byte) 166,
    (byte) 89,
    (byte) 144 /*0x90*/,
    (byte) 209,
    (byte) 50,
    (byte) 197,
    (byte) 93,
    (byte) 45,
    (byte) 37,
    (byte) 45,
    (byte) 21,
    (byte) 58,
    (byte) 39,
    (byte) 200,
    (byte) 38,
    (byte) 33,
    (byte) 29,
    (byte) 72,
    (byte) 176 /*0xB0*/,
    (byte) 70,
    (byte) 203,
    (byte) 69,
    (byte) 41,
    (byte) 196,
    (byte) 154,
    (byte) 189,
    (byte) 141,
    (byte) 152,
    (byte) 107,
    (byte) 162,
    (byte) 38,
    (byte) 89,
    (byte) 218,
    (byte) 28,
    (byte) 42,
    (byte) 225,
    (byte) 225,
    (byte) 193,
    (byte) 169,
    (byte) 28,
    (byte) 252,
    (byte) 3,
    (byte) 14,
    (byte) 114,
    (byte) 30,
    (byte) 192 /*0xC0*/,
    (byte) 96 /*0x60*/,
    (byte) 49,
    (byte) 153,
    (byte) 184,
    (byte) 36,
    (byte) 29,
    (byte) 228,
    (byte) 218,
    (byte) 227,
    (byte) 47,
    (byte) 226,
    (byte) 95,
    (byte) 74,
    (byte) 159,
    (byte) 224 /*0xE0*/,
    (byte) 103,
    (byte) 153,
    (byte) 182,
    (byte) 194,
    (byte) 224 /*0xE0*/,
    (byte) 49,
    (byte) 156,
    (byte) 24,
    (byte) 99,
    (byte) 55,
    (byte) 157,
    (byte) 216,
    (byte) 109,
    (byte) 62,
    (byte) 107,
    (byte) 247,
    (byte) 190,
    (byte) 173,
    (byte) 64 /*0x40*/,
    (byte) 29,
    (byte) 176 /*0xB0*/,
    (byte) 55,
    (byte) 96 /*0x60*/,
    (byte) 184,
    (byte) 99,
    (byte) 214,
    (byte) 195,
    (byte) 184,
    (byte) 242,
    (byte) 193,
    (byte) 241,
    (byte) 73,
    (byte) 238,
    (byte) 43,
    (byte) 7,
    (byte) 229,
    (byte) 42,
    (byte) 177,
    (byte) 60,
    (byte) 92,
    (byte) 59,
    (byte) 82,
    (byte) 62,
    (byte) 167,
    (byte) 247,
    (byte) 198,
    (byte) 126,
    (byte) 153,
    (byte) 73,
    (byte) 64 /*0x40*/,
    (byte) 231,
    (byte) 175,
    (byte) 62,
    (byte) 111,
    (byte) 203,
    (byte) 197,
    (byte) 47,
    (byte) 33,
    (byte) 158,
    (byte) 131,
    (byte) 88,
    (byte) 160 /*0xA0*/,
    (byte) 231,
    (byte) 44,
    (byte) 151,
    (byte) 242,
    (byte) 152,
    (byte) 209,
    (byte) 220,
    (byte) 103,
    (byte) 233,
    (byte) 189,
    (byte) 26,
    (byte) 208 /*0xD0*/,
    (byte) 29,
    (byte) 217,
    (byte) 237,
    (byte) 144 /*0x90*/,
    (byte) 106,
    (byte) 150,
    (byte) 88,
    (byte) 55,
    (byte) 0,
    (byte) 85,
    (byte) 177,
    (byte) 139,
    (byte) 185,
    (byte) 142,
    (byte) 84,
    (byte) 156,
    (byte) 203,
    (byte) 132,
    (byte) 250,
    (byte) 23,
    (byte) 0,
    (byte) 67,
    (byte) 4,
    (byte) 10,
    (byte) 101,
    (byte) 109,
    (byte) 14,
    (byte) 25,
    (byte) 36,
    (byte) 249,
    (byte) 215,
    (byte) 226,
    (byte) 205,
    (byte) 22,
    (byte) 76,
    (byte) 60,
    (byte) 70,
    (byte) 231,
    (byte) 73,
    (byte) 244,
    (byte) 222,
    (byte) 76,
    (byte) 192 /*0xC0*/,
    (byte) 240 /*0xF0*/,
    (byte) 100,
    (byte) 190,
    (byte) 222,
    (byte) 15,
    (byte) 215,
    (byte) 108,
    (byte) 90,
    (byte) 92,
    (byte) 223,
    (byte) 235,
    (byte) 32 /*0x20*/,
    (byte) 251,
    (byte) 117,
    (byte) 90,
    (byte) 124,
    (byte) 35,
    (byte) 165,
    (byte) 166,
    (byte) 215,
    (byte) 202,
    (byte) 197,
    (byte) 64 /*0x40*/,
    (byte) 152,
    (byte) 78,
    (byte) 214,
    (byte) 100,
    (byte) 27,
    (byte) 247,
    (byte) 204,
    (byte) 232,
    (byte) 6,
    (byte) 44,
    (byte) 11,
    (byte) 183,
    (byte) 243,
    (byte) 250,
    (byte) 49,
    (byte) 197,
    (byte) 28,
    (byte) 44,
    (byte) 238,
    (byte) 224 /*0xE0*/,
    (byte) 38,
    (byte) 240 /*0xF0*/,
    (byte) 248,
    (byte) 83,
    (byte) 22,
    (byte) 166,
    (byte) 82,
    (byte) 194,
    (byte) 71,
    (byte) 81,
    (byte) 132,
    (byte) 61,
    (byte) 12,
    (byte) 54,
    (byte) 124,
    (byte) 198,
    (byte) 53,
    (byte) 116,
    (byte) 249,
    (byte) 111,
    (byte) 46,
    (byte) 131,
    (byte) 207,
    (byte) 203,
    (byte) 73,
    (byte) 125,
    (byte) 154,
    (byte) 252,
    (byte) 95,
    (byte) 142,
    (byte) 196,
    (byte) 157,
    (byte) 6,
    (byte) 91,
    (byte) 143,
    (byte) 190,
    (byte) 87,
    (byte) 165,
    (byte) 48 /*0x30*/,
    (byte) 80 /*0x50*/,
    (byte) 78,
    (byte) 246,
    (byte) 132,
    (byte) 18,
    (byte) 80 /*0x50*/,
    (byte) 72,
    (byte) 112 /*0x70*/,
    (byte) 145,
    (byte) 180,
    (byte) 75,
    (byte) 129,
    (byte) 153,
    (byte) 14,
    (byte) 13,
    (byte) 89,
    (byte) 48 /*0x30*/,
    (byte) 147,
    (byte) 44,
    (byte) 10,
    (byte) 120,
    (byte) 39,
    (byte) 102,
    (byte) 97,
    (byte) 102,
    (byte) 2,
    (byte) 45,
    (byte) 220,
    (byte) 224 /*0xE0*/,
    (byte) 129,
    (byte) 40,
    (byte) 82,
    (byte) 247,
    (byte) 57,
    (byte) 239,
    (byte) 2,
    (byte) 205,
    (byte) 186,
    (byte) 133,
    (byte) 37,
    (byte) 149,
    (byte) 6,
    (byte) 103,
    (byte) 185,
    (byte) 158,
    (byte) 18,
    (byte) 247,
    (byte) 77,
    (byte) 212,
    (byte) 157,
    (byte) 39,
    (byte) 227,
    (byte) 165,
    (byte) 228,
    (byte) 7,
    (byte) 242,
    (byte) 79,
    (byte) 109,
    (byte) 49,
    (byte) 151,
    (byte) 233,
    (byte) 197,
    (byte) 97,
    (byte) 120,
    (byte) 156,
    (byte) 25,
    (byte) 40,
    (byte) 125,
    (byte) 225,
    (byte) 145,
    (byte) 135,
    (byte) 12,
    (byte) 141,
    (byte) 86,
    (byte) 167,
    (byte) 123,
    (byte) 165,
    (byte) 82,
    (byte) 143,
    (byte) 131,
    (byte) 101,
    (byte) 226,
    (byte) 184,
    (byte) 218,
    (byte) 48 /*0x30*/,
    (byte) 132,
    (byte) 66,
    (byte) 32 /*0x20*/,
    (byte) 32 /*0x20*/,
    (byte) 215,
    (byte) 241,
    (byte) 36,
    (byte) 158,
    (byte) 188,
    (byte) 168,
    (byte) 201,
    (byte) 205,
    (byte) 178,
    (byte) 203,
    (byte) 89,
    (byte) 190,
    (byte) 147,
    (byte) 209,
    (byte) 164,
    (byte) 172,
    (byte) 118,
    (byte) 247,
    (byte) 151,
    (byte) 2,
    (byte) 77,
    (byte) 23,
    (byte) 219,
    (byte) 82,
    (byte) 197,
    (byte) 82,
    (byte) 68,
    (byte) 184,
    (byte) 27,
    (byte) 151,
    (byte) 112 /*0x70*/,
    (byte) 51,
    (byte) 177,
    (byte) 42,
    (byte) 219,
    (byte) 180,
    (byte) 197,
    (byte) 187,
    (byte) 254,
    (byte) 142,
    (byte) 177,
    (byte) 179,
    (byte) 123,
    (byte) 163,
    (byte) 86,
    (byte) 86,
    (byte) 206,
    (byte) 225,
    (byte) 200,
    (byte) 71,
    (byte) 8,
    (byte) 108,
    (byte) 167,
    (byte) 105,
    (byte) 133,
    (byte) 163,
    (byte) 108,
    (byte) 198,
    (byte) 147,
    (byte) 11,
    (byte) 27,
    (byte) 56,
    (byte) 120,
    (byte) 170,
    (byte) 226,
    (byte) 105,
    (byte) 238,
    (byte) 30,
    (byte) 50,
    (byte) 26,
    (byte) 187,
    (byte) 145,
    (byte) 132,
    (byte) 208 /*0xD0*/,
    (byte) 105,
    (byte) 26,
    (byte) 182,
    (byte) 178,
    (byte) 82,
    (byte) 240 /*0xF0*/,
    (byte) 51,
    (byte) 20,
    (byte) 200,
    (byte) 97,
    byte.MaxValue,
    (byte) 35,
    (byte) 153,
    (byte) 72,
    (byte) 229,
    (byte) 14,
    (byte) 196,
    (byte) 135,
    (byte) 153,
    (byte) 196,
    (byte) 48 /*0x30*/,
    (byte) 65,
    (byte) 55,
    (byte) 153,
    (byte) 69,
    (byte) 134,
    (byte) 249,
    (byte) 158,
    (byte) 23,
    (byte) 225,
    (byte) 53,
    (byte) 91,
    (byte) 186,
    (byte) 176 /*0xB0*/,
    (byte) 12,
    (byte) 127 /*0x7F*/,
    (byte) 222,
    (byte) 215,
    (byte) 71,
    (byte) 252,
    (byte) 198,
    (byte) 166,
    (byte) 229,
    (byte) 227,
    (byte) 48 /*0x30*/,
    (byte) 57,
    (byte) 85,
    (byte) 154,
    (byte) 136,
    (byte) 190,
    (byte) 195,
    (byte) 198,
    (byte) 15,
    (byte) 246,
    (byte) 72
  };

  internal static int ssp_appserver_14239(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 45,
      (byte) 15,
      (byte) 31 /*0x1F*/,
      (byte) 175,
      (byte) 181,
      (byte) 111,
      (byte) 59,
      (byte) 218,
      (byte) 220,
      (byte) 70,
      (byte) 168,
      (byte) 223,
      (byte) 3,
      (byte) 164,
      (byte) 131,
      (byte) 226,
      (byte) 31 /*0x1F*/,
      (byte) 185,
      (byte) 96 /*0x60*/,
      (byte) 223,
      (byte) 72,
      (byte) 168,
      (byte) 216,
      (byte) 178,
      (byte) 208 /*0xD0*/,
      (byte) 3,
      (byte) 114,
      (byte) 188,
      (byte) 75,
      (byte) 217,
      (byte) 20,
      (byte) 137,
      (byte) 110,
      (byte) 234,
      (byte) 243,
      (byte) 127 /*0x7F*/,
      (byte) 33,
      (byte) 61,
      (byte) 7,
      (byte) 130,
      (byte) 18,
      (byte) 148,
      (byte) 74,
      (byte) 199,
      (byte) 105,
      (byte) 109,
      (byte) 214,
      (byte) 204
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[36] = (byte) 78;
    sourceArray2[3] = (byte) 218;
    sourceArray2[21] = (byte) 222;
    sourceArray2[24] = (byte) 118;
    sourceArray2[16 /*0x10*/] = (byte) 33;
    sourceArray2[5] = (byte) 42;
    sourceArray2[6] = (byte) 121;
    sourceArray2[7] = (byte) 250;
    sourceArray2[25] = (byte) 168;
    sourceArray2[35] = (byte) 183;
    sourceArray2[11] = (byte) 250;
    sourceArray2[8] = (byte) 213;
    sourceArray2[12] = (byte) 157;
    sourceArray2[10] = (byte) 147;
    sourceArray2[14] = (byte) 113;
    sourceArray2[44] = (byte) 18;
    sourceArray2[30] = (byte) 247;
    sourceArray2[17] = (byte) 112 /*0x70*/;
    sourceArray2[45] = (byte) 218;
    sourceArray2[15] = (byte) 84;
    sourceArray2[29] = (byte) 96 /*0x60*/;
    sourceArray2[28] = (byte) 76;
    sourceArray2[22] = (byte) 239;
    sourceArray2[33] = (byte) 68;
    sourceArray2[18] = (byte) 8;
    sourceArray2[2] = (byte) 141;
    sourceArray2[0] = (byte) 63 /*0x3F*/;
    sourceArray2[27] = (byte) 50;
    sourceArray2[1] = (byte) 42;
    sourceArray2[39] = (byte) 40;
    sourceArray2[26] = (byte) 76;
    sourceArray2[31 /*0x1F*/] = (byte) 11;
    sourceArray2[32 /*0x20*/] = (byte) 139;
    sourceArray2[23] = (byte) 80 /*0x50*/;
    sourceArray2[34] = (byte) 71;
    sourceArray2[19] = (byte) 139;
    sourceArray2[20] = (byte) 24;
    sourceArray2[37] = (byte) 91;
    sourceArray2[41] = (byte) 62;
    sourceArray2[9] = (byte) 106;
    sourceArray2[40] = (byte) 18;
    sourceArray2[4] = (byte) 86;
    sourceArray2[42] = (byte) 153;
    sourceArray2[43] = (byte) 53;
    sourceArray2[38] = (byte) 194;
    sourceArray2[47] = (byte) 169;
    sourceArray2[46] = (byte) 116;
    sourceArray2[13] = (byte) 58;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_14240(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[22] = (byte) 66;
    sourceArray1[45] = (byte) 145;
    sourceArray1[2] = (byte) 12;
    sourceArray1[8] = (byte) 60;
    sourceArray1[27] = (byte) 87;
    sourceArray1[34] = (byte) 110;
    sourceArray1[13] = (byte) 133;
    sourceArray1[11] = (byte) 35;
    sourceArray1[0] = (byte) 189;
    sourceArray1[17] = (byte) 107;
    sourceArray1[41] = (byte) 127 /*0x7F*/;
    sourceArray1[4] = (byte) 96 /*0x60*/;
    sourceArray1[16 /*0x10*/] = (byte) 124;
    sourceArray1[44] = (byte) 34;
    sourceArray1[10] = (byte) 38;
    sourceArray1[39] = (byte) 184;
    sourceArray1[29] = (byte) 174;
    sourceArray1[43] = (byte) 72;
    sourceArray1[18] = (byte) 36;
    sourceArray1[19] = (byte) 30;
    sourceArray1[20] = (byte) 223;
    sourceArray1[1] = (byte) 27;
    sourceArray1[5] = (byte) 84;
    sourceArray1[7] = (byte) 17;
    sourceArray1[24] = (byte) 12;
    sourceArray1[37] = (byte) 154;
    sourceArray1[26] = (byte) 93;
    sourceArray1[15] = (byte) 210;
    sourceArray1[6] = (byte) 170;
    sourceArray1[23] = (byte) 187;
    sourceArray1[30] = (byte) 22;
    sourceArray1[31 /*0x1F*/] = (byte) 122;
    sourceArray1[32 /*0x20*/] = byte.MaxValue;
    sourceArray1[36] = (byte) 159;
    sourceArray1[33] = (byte) 102;
    sourceArray1[9] = (byte) 38;
    sourceArray1[12] = (byte) 123;
    sourceArray1[38] = (byte) 54;
    sourceArray1[14] = (byte) 86;
    sourceArray1[3] = (byte) 119;
    sourceArray1[40] = (byte) 86;
    sourceArray1[21] = (byte) 31 /*0x1F*/;
    sourceArray1[42] = (byte) 188;
    sourceArray1[28] = (byte) 56;
    sourceArray1[35] = (byte) 138;
    sourceArray1[25] = (byte) 131;
    sourceArray1[46] = (byte) 227;
    sourceArray1[47] = (byte) 207;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 194,
      (byte) 198,
      (byte) 6,
      (byte) 208 /*0xD0*/,
      (byte) 132,
      (byte) 127 /*0x7F*/,
      (byte) 237,
      (byte) 197,
      (byte) 9,
      (byte) 116,
      (byte) 81,
      (byte) 126,
      (byte) 40,
      (byte) 103,
      (byte) 242,
      (byte) 150,
      (byte) 70,
      (byte) 131,
      (byte) 146,
      (byte) 158,
      (byte) 58,
      (byte) 8,
      (byte) 4,
      (byte) 117,
      (byte) 69,
      (byte) 202,
      (byte) 3,
      (byte) 207,
      (byte) 87,
      (byte) 212,
      (byte) 155,
      (byte) 100,
      (byte) 196,
      (byte) 244,
      (byte) 122,
      (byte) 153,
      (byte) 38,
      (byte) 104,
      (byte) 191,
      (byte) 121,
      (byte) 234,
      (byte) 210,
      (byte) 15,
      (byte) 48 /*0x30*/,
      (byte) 238,
      (byte) 45,
      (byte) 42,
      (byte) 46
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_14241()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[26];
      byte[] numArray2 = new byte[26]
      {
        (byte) 109,
        (byte) 205,
        (byte) 183,
        (byte) 98,
        (byte) 141,
        (byte) 166,
        (byte) 167,
        (byte) 6,
        (byte) 99,
        (byte) 189,
        (byte) 59,
        (byte) 222,
        (byte) 184,
        (byte) 240 /*0xF0*/,
        (byte) 61,
        (byte) 106,
        (byte) 41,
        (byte) 96 /*0x60*/,
        (byte) 104,
        (byte) 213,
        (byte) 163,
        (byte) 79,
        (byte) 204,
        (byte) 71,
        (byte) 26,
        (byte) 229
      };
      byte[] numArray3 = new byte[26];
      numArray3[23] = (byte) 8;
      numArray3[1] = (byte) 57;
      numArray3[8] = (byte) 138;
      numArray3[3] = (byte) 71;
      numArray3[22] = (byte) 98;
      numArray3[5] = (byte) 51;
      numArray3[2] = (byte) 154;
      numArray3[7] = (byte) 216;
      numArray3[16 /*0x10*/] = (byte) 6;
      numArray3[4] = (byte) 184;
      numArray3[10] = (byte) 37;
      numArray3[6] = (byte) 14;
      numArray3[12] = byte.MaxValue;
      numArray3[13] = (byte) 246;
      numArray3[17] = (byte) 250;
      numArray3[21] = (byte) 23;
      numArray3[14] = (byte) 236;
      numArray3[9] = (byte) 210;
      numArray3[18] = (byte) 154;
      numArray3[15] = (byte) 184;
      numArray3[20] = (byte) 94;
      numArray3[19] = (byte) 129;
      numArray3[11] = (byte) 9;
      numArray3[0] = (byte) 107;
      numArray3[24] = (byte) 158;
      numArray3[25] = (byte) 63 /*0x3F*/;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 26);
      for (int index = 0; index < 26; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[37];
      byte[] response = new byte[37];
      Array.Copy((Array) sc_14238.sspq, 0, (Array) numArray4, 0, 37);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_14238.sspr, 0, (Array) numArray4, 0, 37);
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
    byte[] numArray5 = new byte[26];
    byte[] numArray6 = new byte[26]
    {
      (byte) 66,
      (byte) 139,
      (byte) 197,
      (byte) 167,
      (byte) 210,
      (byte) 134,
      (byte) 142,
      (byte) 183,
      (byte) 1,
      (byte) 91,
      (byte) 177,
      (byte) 193,
      (byte) 118,
      (byte) 210,
      (byte) 219,
      (byte) 31 /*0x1F*/,
      (byte) 48 /*0x30*/,
      (byte) 62,
      (byte) 191,
      (byte) 189,
      (byte) 2,
      (byte) 88,
      (byte) 127 /*0x7F*/,
      (byte) 14,
      (byte) 6,
      (byte) 178
    };
    byte[] numArray7 = new byte[26]
    {
      (byte) 168,
      (byte) 145,
      (byte) 162,
      (byte) 3,
      (byte) 170,
      (byte) 251,
      (byte) 233,
      (byte) 48 /*0x30*/,
      (byte) 188,
      (byte) 55,
      (byte) 68,
      (byte) 21,
      (byte) 147,
      (byte) 118,
      (byte) 34,
      (byte) 172,
      (byte) 74,
      (byte) 172,
      (byte) 125,
      (byte) 40,
      (byte) 126,
      (byte) 38,
      (byte) 127 /*0x7F*/,
      (byte) 232,
      (byte) 22,
      (byte) 37
    };
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 26);
    for (int index = 0; index < 26; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_appserver_14242()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10];
      numArray2[0] = (byte) 242;
      numArray2[1] = (byte) 154;
      numArray2[4] = (byte) 50;
      numArray2[3] = (byte) 228;
      numArray2[7] = (byte) 250;
      numArray2[5] = (byte) 93;
      numArray2[6] = (byte) 125;
      numArray2[2] = (byte) 47;
      numArray2[9] = (byte) 187;
      numArray2[8] = (byte) 12;
      byte[] numArray3 = new byte[10];
      numArray3[0] = (byte) 63 /*0x3F*/;
      numArray3[6] = (byte) 186;
      numArray3[2] = (byte) 216;
      numArray3[1] = (byte) 218;
      numArray3[4] = (byte) 48 /*0x30*/;
      numArray3[5] = (byte) 252;
      numArray3[8] = (byte) 186;
      numArray3[9] = (byte) 15;
      numArray3[3] = (byte) 143;
      numArray3[7] = (byte) 168;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10]
    {
      (byte) 190,
      (byte) 105,
      (byte) 22,
      (byte) 146,
      (byte) 146,
      (byte) 159,
      (byte) 125,
      (byte) 162,
      (byte) 54,
      (byte) 49
    };
    byte[] numArray6 = new byte[10];
    numArray6[3] = (byte) 51;
    numArray6[2] = (byte) 35;
    numArray6[0] = (byte) 115;
    numArray6[4] = (byte) 250;
    numArray6[5] = (byte) 195;
    numArray6[6] = (byte) 13;
    numArray6[1] = (byte) 56;
    numArray6[7] = (byte) 238;
    numArray6[8] = (byte) 57;
    numArray6[9] = (byte) 190;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[50];
    byte[] response = new byte[50];
    Array.Copy((Array) sc_14238.sspq, 37, (Array) numArray7, 0, 50);
    key.Query(true, 335, numArray7, response);
    Array.Copy((Array) sc_14238.sspr, 37, (Array) numArray7, 0, 50);
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

  internal static string ssp_appserver_14243()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 223,
        (byte) 55,
        (byte) 193,
        (byte) 125,
        (byte) 121,
        (byte) 95,
        (byte) 105,
        (byte) 171,
        (byte) 16 /*0x10*/,
        (byte) 42
      };
      byte[] numArray3 = new byte[10]
      {
        (byte) 164,
        (byte) 231,
        (byte) 220,
        (byte) 56,
        (byte) 4,
        (byte) 189,
        (byte) 22,
        (byte) 244,
        (byte) 178,
        (byte) 20
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[25];
      byte[] response = new byte[25];
      Array.Copy((Array) sc_14238.sspq, 87, (Array) numArray4, 0, 25);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_14238.sspr, 87, (Array) numArray4, 0, 25);
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
      (byte) 201,
      (byte) 237,
      (byte) 129,
      (byte) 204,
      (byte) 179,
      (byte) 59,
      (byte) 109,
      (byte) 57,
      (byte) 202,
      (byte) 136
    };
    byte[] numArray7 = new byte[10]
    {
      (byte) 166,
      (byte) 18,
      (byte) 134,
      (byte) 197,
      (byte) 253,
      (byte) 5,
      (byte) 117,
      (byte) 82,
      (byte) 143,
      (byte) 80 /*0x50*/
    };
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_appserver_14244()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 78,
        (byte) 3,
        (byte) 30,
        (byte) 42,
        (byte) 134,
        (byte) 170,
        (byte) 218,
        (byte) 54,
        (byte) 6,
        (byte) 189
      };
      byte[] numArray3 = new byte[10]
      {
        (byte) 214,
        (byte) 216,
        (byte) 2,
        (byte) 122,
        (byte) 118,
        (byte) 12,
        (byte) 134,
        (byte) 1,
        (byte) 243,
        (byte) 8
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
      (byte) 199,
      (byte) 42,
      (byte) 118,
      (byte) 142,
      (byte) 30,
      (byte) 91,
      (byte) 100,
      (byte) 239,
      (byte) 186,
      (byte) 103
    };
    byte[] numArray6 = new byte[10];
    numArray6[8] = (byte) 63 /*0x3F*/;
    numArray6[1] = (byte) 235;
    numArray6[7] = (byte) 232;
    numArray6[0] = (byte) 225;
    numArray6[4] = (byte) 224 /*0xE0*/;
    numArray6[5] = (byte) 192 /*0xC0*/;
    numArray6[9] = (byte) 52;
    numArray6[2] = (byte) 45;
    numArray6[6] = (byte) 189;
    numArray6[3] = (byte) 195;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_14245()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[16 /*0x10*/];
      byte[] numArray2 = new byte[16 /*0x10*/]
      {
        (byte) 57,
        (byte) 224 /*0xE0*/,
        (byte) 100,
        (byte) 182,
        (byte) 147,
        (byte) 230,
        (byte) 196,
        (byte) 103,
        (byte) 67,
        (byte) 184,
        (byte) 165,
        (byte) 14,
        (byte) 84,
        (byte) 249,
        (byte) 18,
        (byte) 67
      };
      byte[] numArray3 = new byte[16 /*0x10*/]
      {
        (byte) 61,
        (byte) 50,
        (byte) 58,
        (byte) 211,
        (byte) 82,
        (byte) 82,
        (byte) 2,
        (byte) 69,
        (byte) 103,
        (byte) 225,
        (byte) 194,
        (byte) 22,
        (byte) 0,
        (byte) 213,
        (byte) 65,
        (byte) 18
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 16 /*0x10*/);
      for (int index = 0; index < 16 /*0x10*/; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[16 /*0x10*/];
    byte[] numArray5 = new byte[16 /*0x10*/]
    {
      (byte) 155,
      (byte) 41,
      (byte) 209,
      (byte) 135,
      (byte) 69,
      (byte) 116,
      (byte) 215,
      (byte) 43,
      (byte) 0,
      (byte) 88,
      (byte) 49,
      (byte) 156,
      (byte) 16 /*0x10*/,
      (byte) 36,
      (byte) 245,
      (byte) 85
    };
    byte[] numArray6 = new byte[16 /*0x10*/]
    {
      (byte) 67,
      (byte) 233,
      (byte) 236,
      (byte) 181,
      (byte) 190,
      (byte) 47,
      (byte) 54,
      (byte) 200,
      (byte) 57,
      (byte) 219,
      (byte) 181,
      (byte) 203,
      (byte) 127 /*0x7F*/,
      (byte) 157,
      (byte) 40,
      (byte) 231
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 16 /*0x10*/);
    for (int index = 0; index < 16 /*0x10*/; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_14246()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10];
      numArray2[5] = (byte) 191;
      numArray2[1] = (byte) 163;
      numArray2[2] = (byte) 140;
      numArray2[6] = (byte) 86;
      numArray2[4] = (byte) 70;
      numArray2[3] = (byte) 21;
      numArray2[0] = (byte) 211;
      numArray2[7] = (byte) 11;
      numArray2[8] = (byte) 225;
      numArray2[9] = (byte) 170;
      byte[] numArray3 = new byte[10]
      {
        (byte) 13,
        (byte) 34,
        (byte) 166,
        (byte) 239,
        (byte) 230,
        (byte) 71,
        (byte) 230,
        (byte) 64 /*0x40*/,
        (byte) 100,
        (byte) 110
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
      (byte) 15,
      (byte) 18,
      (byte) 27,
      (byte) 149,
      (byte) 234,
      (byte) 94,
      (byte) 164,
      (byte) 212,
      (byte) 76,
      (byte) 107
    };
    byte[] numArray6 = new byte[10];
    numArray6[6] = (byte) 91;
    numArray6[1] = (byte) 94;
    numArray6[2] = (byte) 64 /*0x40*/;
    numArray6[3] = (byte) 166;
    numArray6[7] = (byte) 46;
    numArray6[9] = (byte) 159;
    numArray6[5] = (byte) 23;
    numArray6[0] = (byte) 233;
    numArray6[8] = (byte) 113;
    numArray6[4] = (byte) 253;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_14247()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[17];
      byte[] numArray2 = new byte[17];
      numArray2[11] = (byte) 3;
      numArray2[9] = (byte) 223;
      numArray2[4] = (byte) 228;
      numArray2[12] = (byte) 42;
      numArray2[14] = (byte) 108;
      numArray2[5] = (byte) 217;
      numArray2[1] = (byte) 184;
      numArray2[7] = (byte) 160 /*0xA0*/;
      numArray2[8] = (byte) 23;
      numArray2[13] = (byte) 41;
      numArray2[10] = (byte) 125;
      numArray2[0] = (byte) 237;
      numArray2[2] = (byte) 158;
      numArray2[6] = (byte) 106;
      numArray2[3] = (byte) 236;
      numArray2[15] = (byte) 172;
      numArray2[16 /*0x10*/] = (byte) 61;
      byte[] numArray3 = new byte[17];
      numArray3[4] = (byte) 99;
      numArray3[14] = (byte) 184;
      numArray3[2] = (byte) 71;
      numArray3[9] = (byte) 252;
      numArray3[3] = (byte) 104;
      numArray3[5] = (byte) 175;
      numArray3[6] = (byte) 161;
      numArray3[8] = (byte) 18;
      numArray3[10] = (byte) 170;
      numArray3[15] = (byte) 0;
      numArray3[13] = (byte) 235;
      numArray3[7] = (byte) 216;
      numArray3[12] = (byte) 245;
      numArray3[11] = (byte) 39;
      numArray3[1] = (byte) 9;
      numArray3[0] = (byte) 138;
      numArray3[16 /*0x10*/] = (byte) 231;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 17);
      for (int index = 0; index < 17; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[17];
    byte[] numArray5 = new byte[17];
    numArray5[1] = (byte) 140;
    numArray5[3] = (byte) 228;
    numArray5[10] = (byte) 113;
    numArray5[12] = (byte) 254;
    numArray5[4] = (byte) 63 /*0x3F*/;
    numArray5[2] = (byte) 69;
    numArray5[6] = (byte) 65;
    numArray5[7] = (byte) 70;
    numArray5[8] = (byte) 177;
    numArray5[9] = (byte) 167;
    numArray5[5] = (byte) 203;
    numArray5[11] = (byte) 253;
    numArray5[14] = (byte) 128 /*0x80*/;
    numArray5[13] = (byte) 119;
    numArray5[0] = (byte) 139;
    numArray5[15] = (byte) 58;
    numArray5[16 /*0x10*/] = (byte) 235;
    byte[] numArray6 = new byte[17]
    {
      byte.MaxValue,
      (byte) 153,
      (byte) 38,
      (byte) 67,
      (byte) 171,
      (byte) 13,
      (byte) 243,
      (byte) 137,
      (byte) 161,
      (byte) 132,
      (byte) 15,
      (byte) 95,
      (byte) 202,
      (byte) 223,
      (byte) 40,
      (byte) 95,
      (byte) 46
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 17);
    for (int index = 0; index < 17; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_14248()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10];
      numArray2[6] = (byte) 167;
      numArray2[1] = (byte) 36;
      numArray2[0] = (byte) 119;
      numArray2[3] = (byte) 186;
      numArray2[2] = (byte) 49;
      numArray2[4] = (byte) 4;
      numArray2[9] = (byte) 157;
      numArray2[7] = (byte) 33;
      numArray2[8] = (byte) 185;
      numArray2[5] = (byte) 2;
      byte[] numArray3 = new byte[10]
      {
        (byte) 51,
        (byte) 224 /*0xE0*/,
        (byte) 84,
        (byte) 97,
        (byte) 60,
        (byte) 194,
        (byte) 22,
        (byte) 245,
        (byte) 223,
        (byte) 78
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
      (byte) 8,
      (byte) 211,
      (byte) 226,
      (byte) 68,
      (byte) 131,
      (byte) 144 /*0x90*/,
      (byte) 77,
      (byte) 80 /*0x50*/,
      (byte) 101,
      (byte) 77
    };
    byte[] numArray6 = new byte[10];
    numArray6[3] = (byte) 139;
    numArray6[1] = (byte) 56;
    numArray6[0] = (byte) 203;
    numArray6[6] = (byte) 177;
    numArray6[4] = (byte) 177;
    numArray6[2] = (byte) 240 /*0xF0*/;
    numArray6[8] = (byte) 6;
    numArray6[7] = (byte) 229;
    numArray6[5] = (byte) 103;
    numArray6[9] = (byte) 95;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[36];
    byte[] response = new byte[36];
    Array.Copy((Array) sc_14238.sspq, 112 /*0x70*/, (Array) numArray7, 0, 36);
    key.Query(true, 335, numArray7, response);
    Array.Copy((Array) sc_14238.sspr, 112 /*0x70*/, (Array) numArray7, 0, 36);
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

  internal static int ssp_appserver_14249(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[3] = (byte) 124;
    sourceArray1[1] = (byte) 31 /*0x1F*/;
    sourceArray1[35] = (byte) 87;
    sourceArray1[2] = (byte) 76;
    sourceArray1[20] = (byte) 115;
    sourceArray1[5] = (byte) 100;
    sourceArray1[27] = (byte) 125;
    sourceArray1[7] = (byte) 20;
    sourceArray1[36] = (byte) 220;
    sourceArray1[11] = (byte) 232;
    sourceArray1[18] = (byte) 122;
    sourceArray1[0] = (byte) 141;
    sourceArray1[31 /*0x1F*/] = (byte) 244;
    sourceArray1[26] = (byte) 250;
    sourceArray1[10] = (byte) 143;
    sourceArray1[15] = (byte) 210;
    sourceArray1[29] = (byte) 218;
    sourceArray1[38] = (byte) 153;
    sourceArray1[9] = (byte) 246;
    sourceArray1[19] = (byte) 50;
    sourceArray1[42] = (byte) 63 /*0x3F*/;
    sourceArray1[21] = (byte) 130;
    sourceArray1[22] = (byte) 36;
    sourceArray1[23] = (byte) 201;
    sourceArray1[24] = (byte) 193;
    sourceArray1[12] = (byte) 207;
    sourceArray1[13] = (byte) 24;
    sourceArray1[34] = (byte) 191;
    sourceArray1[46] = (byte) 63 /*0x3F*/;
    sourceArray1[25] = (byte) 30;
    sourceArray1[44] = (byte) 168;
    sourceArray1[14] = (byte) 229;
    sourceArray1[32 /*0x20*/] = (byte) 99;
    sourceArray1[17] = (byte) 98;
    sourceArray1[8] = (byte) 1;
    sourceArray1[6] = (byte) 175;
    sourceArray1[47] = (byte) 73;
    sourceArray1[37] = (byte) 168;
    sourceArray1[39] = (byte) 116;
    sourceArray1[43] = (byte) 42;
    sourceArray1[40] = (byte) 128 /*0x80*/;
    sourceArray1[41] = (byte) 139;
    sourceArray1[16 /*0x10*/] = (byte) 117;
    sourceArray1[30] = (byte) 86;
    sourceArray1[33] = (byte) 50;
    sourceArray1[45] = (byte) 232;
    sourceArray1[28] = (byte) 16 /*0x10*/;
    sourceArray1[4] = (byte) 224 /*0xE0*/;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 0,
      (byte) 133,
      (byte) 160 /*0xA0*/,
      (byte) 195,
      (byte) 33,
      (byte) 39,
      (byte) 115,
      (byte) 114,
      (byte) 202,
      (byte) 126,
      (byte) 79,
      (byte) 133,
      (byte) 65,
      (byte) 250,
      (byte) 210,
      (byte) 195,
      (byte) 25,
      (byte) 78,
      (byte) 194,
      (byte) 65,
      (byte) 189,
      (byte) 102,
      (byte) 247,
      (byte) 62,
      (byte) 76,
      (byte) 5,
      (byte) 148,
      (byte) 233,
      (byte) 159,
      (byte) 84,
      (byte) 87,
      (byte) 145,
      (byte) 65,
      (byte) 165,
      (byte) 53,
      (byte) 48 /*0x30*/,
      (byte) 131,
      (byte) 239,
      (byte) 22,
      (byte) 119,
      (byte) 52,
      (byte) 103,
      (byte) 41,
      (byte) 55,
      (byte) 251,
      (byte) 126,
      (byte) 58,
      (byte) 149
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_14250()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[192 /*0xC0*/];
      byte[] numArray2 = new byte[55];
      numArray2[40] = (byte) 18;
      numArray2[1] = (byte) 161;
      numArray2[2] = (byte) 240 /*0xF0*/;
      numArray2[3] = (byte) 55;
      numArray2[4] = (byte) 14;
      numArray2[34] = (byte) 116;
      numArray2[25] = (byte) 155;
      numArray2[7] = (byte) 174;
      numArray2[49] = (byte) 80 /*0x50*/;
      numArray2[18] = (byte) 29;
      numArray2[10] = (byte) 200;
      numArray2[47] = (byte) 11;
      numArray2[54] = (byte) 178;
      numArray2[13] = (byte) 226;
      numArray2[46] = (byte) 249;
      numArray2[20] = (byte) 90;
      numArray2[39] = (byte) 4;
      numArray2[5] = (byte) 4;
      numArray2[33] = (byte) 20;
      numArray2[15] = (byte) 185;
      numArray2[36] = (byte) 191;
      numArray2[21] = (byte) 28;
      numArray2[23] = (byte) 165;
      numArray2[37] = (byte) 127 /*0x7F*/;
      numArray2[44] = (byte) 234;
      numArray2[14] = (byte) 225;
      numArray2[26] = (byte) 224 /*0xE0*/;
      numArray2[27] = (byte) 240 /*0xF0*/;
      numArray2[28] = (byte) 251;
      numArray2[29] = (byte) 99;
      numArray2[30] = (byte) 96 /*0x60*/;
      numArray2[16 /*0x10*/] = (byte) 122;
      numArray2[32 /*0x20*/] = (byte) 110;
      numArray2[17] = (byte) 248;
      numArray2[12] = (byte) 96 /*0x60*/;
      numArray2[35] = (byte) 62;
      numArray2[43] = (byte) 141;
      numArray2[19] = (byte) 80 /*0x50*/;
      numArray2[41] = (byte) 145;
      numArray2[9] = (byte) 222;
      numArray2[22] = (byte) 49;
      numArray2[38] = (byte) 253;
      numArray2[42] = (byte) 31 /*0x1F*/;
      numArray2[0] = (byte) 100;
      numArray2[24] = (byte) 50;
      numArray2[31 /*0x1F*/] = (byte) 41;
      numArray2[6] = (byte) 61;
      numArray2[11] = (byte) 2;
      numArray2[48 /*0x30*/] = (byte) 246;
      numArray2[8] = (byte) 72;
      numArray2[50] = (byte) 211;
      numArray2[51] = (byte) 143;
      numArray2[52] = (byte) 251;
      numArray2[53] = (byte) 58;
      numArray2[45] = (byte) 89;
      byte[] numArray3 = new byte[55];
      numArray3[30] = (byte) 240 /*0xF0*/;
      numArray3[6] = (byte) 102;
      numArray3[1] = (byte) 128 /*0x80*/;
      numArray3[41] = (byte) 88;
      numArray3[4] = (byte) 24;
      numArray3[5] = (byte) 35;
      numArray3[47] = (byte) 235;
      numArray3[7] = (byte) 240 /*0xF0*/;
      numArray3[20] = (byte) 174;
      numArray3[9] = (byte) 249;
      numArray3[16 /*0x10*/] = (byte) 246;
      numArray3[22] = (byte) 235;
      numArray3[40] = (byte) 76;
      numArray3[39] = (byte) 140;
      numArray3[52] = (byte) 130;
      numArray3[15] = (byte) 125;
      numArray3[54] = (byte) 152;
      numArray3[17] = (byte) 56;
      numArray3[2] = (byte) 82;
      numArray3[51] = (byte) 212;
      numArray3[35] = (byte) 226;
      numArray3[11] = (byte) 66;
      numArray3[26] = (byte) 120;
      numArray3[23] = (byte) 108;
      numArray3[24] = (byte) 168;
      numArray3[25] = (byte) 16 /*0x10*/;
      numArray3[0] = (byte) 209;
      numArray3[27] = (byte) 252;
      numArray3[3] = (byte) 174;
      numArray3[28] = (byte) 91;
      numArray3[37] = (byte) 16 /*0x10*/;
      numArray3[14] = (byte) 245;
      numArray3[32 /*0x20*/] = (byte) 188;
      numArray3[21] = (byte) 168;
      numArray3[42] = (byte) 3;
      numArray3[48 /*0x30*/] = (byte) 93;
      numArray3[12] = (byte) 88;
      numArray3[18] = (byte) 204;
      numArray3[38] = (byte) 111;
      numArray3[13] = (byte) 65;
      numArray3[29] = (byte) 88;
      numArray3[10] = (byte) 5;
      numArray3[8] = (byte) 223;
      numArray3[43] = (byte) 107;
      numArray3[44] = (byte) 99;
      numArray3[45] = (byte) 126;
      numArray3[46] = (byte) 89;
      numArray3[36] = (byte) 29;
      numArray3[34] = (byte) 229;
      numArray3[49] = (byte) 241;
      numArray3[50] = (byte) 249;
      numArray3[31 /*0x1F*/] = (byte) 31 /*0x1F*/;
      numArray3[19] = (byte) 226;
      numArray3[53] = (byte) 161;
      numArray3[33] = (byte) 30;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 31 /*0x1F*/,
        (byte) 25,
        (byte) 220,
        (byte) 24,
        (byte) 172,
        (byte) 182,
        (byte) 159,
        (byte) 110,
        (byte) 38,
        (byte) 39,
        (byte) 111,
        (byte) 94,
        (byte) 212,
        (byte) 159,
        (byte) 50,
        (byte) 182,
        (byte) 169,
        (byte) 5,
        (byte) 24,
        (byte) 220,
        (byte) 46,
        (byte) 151,
        (byte) 172,
        (byte) 49,
        (byte) 216,
        (byte) 40,
        (byte) 129,
        (byte) 93,
        (byte) 25,
        (byte) 163,
        (byte) 179,
        (byte) 126,
        (byte) 87,
        (byte) 199,
        (byte) 198,
        (byte) 52,
        (byte) 221,
        (byte) 120,
        (byte) 198,
        (byte) 30,
        (byte) 69,
        (byte) 246,
        (byte) 178,
        (byte) 62,
        (byte) 84,
        byte.MaxValue,
        (byte) 168,
        (byte) 50,
        (byte) 38,
        (byte) 109,
        (byte) 254,
        (byte) 126,
        (byte) 227,
        (byte) 173,
        (byte) 11
      };
      byte[] numArray5 = new byte[55];
      numArray5[45] = (byte) 23;
      numArray5[3] = (byte) 94;
      numArray5[37] = (byte) 196;
      numArray5[27] = (byte) 115;
      numArray5[4] = (byte) 47;
      numArray5[5] = (byte) 59;
      numArray5[6] = (byte) 159;
      numArray5[15] = (byte) 46;
      numArray5[8] = (byte) 19;
      numArray5[40] = (byte) 106;
      numArray5[44] = (byte) 8;
      numArray5[52] = (byte) 193;
      numArray5[21] = (byte) 112 /*0x70*/;
      numArray5[51] = (byte) 232;
      numArray5[14] = (byte) 139;
      numArray5[49] = (byte) 20;
      numArray5[9] = (byte) 237;
      numArray5[17] = (byte) 183;
      numArray5[1] = (byte) 122;
      numArray5[47] = (byte) 11;
      numArray5[54] = (byte) 104;
      numArray5[0] = (byte) 77;
      numArray5[22] = (byte) 54;
      numArray5[23] = (byte) 170;
      numArray5[32 /*0x20*/] = (byte) 83;
      numArray5[25] = (byte) 3;
      numArray5[2] = (byte) 118;
      numArray5[16 /*0x10*/] = (byte) 77;
      numArray5[28] = (byte) 154;
      numArray5[29] = (byte) 103;
      numArray5[12] = (byte) 52;
      numArray5[31 /*0x1F*/] = (byte) 43;
      numArray5[48 /*0x30*/] = (byte) 75;
      numArray5[11] = (byte) 115;
      numArray5[20] = (byte) 168;
      numArray5[35] = (byte) 180;
      numArray5[7] = (byte) 109;
      numArray5[10] = (byte) 129;
      numArray5[19] = (byte) 192 /*0xC0*/;
      numArray5[30] = (byte) 116;
      numArray5[39] = (byte) 171;
      numArray5[41] = (byte) 153;
      numArray5[42] = (byte) 121;
      numArray5[43] = (byte) 221;
      numArray5[13] = (byte) 126;
      numArray5[38] = (byte) 185;
      numArray5[46] = (byte) 72;
      numArray5[34] = (byte) 65;
      numArray5[18] = (byte) 213;
      numArray5[26] = (byte) 186;
      numArray5[50] = (byte) 216;
      numArray5[33] = (byte) 155;
      numArray5[24] = (byte) 102;
      numArray5[53] = (byte) 245;
      numArray5[36] = (byte) 151;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[55];
      numArray6[27] = (byte) 215;
      numArray6[53] = (byte) 235;
      numArray6[45] = (byte) 192 /*0xC0*/;
      numArray6[3] = (byte) 230;
      numArray6[39] = (byte) 248;
      numArray6[17] = (byte) 147;
      numArray6[32 /*0x20*/] = (byte) 157;
      numArray6[47] = (byte) 237;
      numArray6[8] = (byte) 86;
      numArray6[6] = (byte) 221;
      numArray6[10] = (byte) 80 /*0x50*/;
      numArray6[11] = (byte) 153;
      numArray6[12] = (byte) 54;
      numArray6[13] = (byte) 166;
      numArray6[14] = (byte) 248;
      numArray6[52] = (byte) 60;
      numArray6[2] = (byte) 3;
      numArray6[40] = (byte) 81;
      numArray6[4] = (byte) 11;
      numArray6[19] = (byte) 182;
      numArray6[20] = (byte) 164;
      numArray6[43] = (byte) 155;
      numArray6[22] = (byte) 223;
      numArray6[23] = (byte) 228;
      numArray6[24] = (byte) 244;
      numArray6[35] = (byte) 210;
      numArray6[26] = (byte) 23;
      numArray6[44] = (byte) 75;
      numArray6[28] = (byte) 245;
      numArray6[25] = (byte) 13;
      numArray6[30] = (byte) 146;
      numArray6[31 /*0x1F*/] = (byte) 233;
      numArray6[37] = (byte) 2;
      numArray6[21] = (byte) 122;
      numArray6[54] = (byte) 89;
      numArray6[50] = (byte) 215;
      numArray6[36] = (byte) 62;
      numArray6[48 /*0x30*/] = (byte) 213;
      numArray6[1] = (byte) 102;
      numArray6[33] = (byte) 201;
      numArray6[16 /*0x10*/] = (byte) 17;
      numArray6[9] = (byte) 93;
      numArray6[34] = (byte) 100;
      numArray6[7] = (byte) 64 /*0x40*/;
      numArray6[46] = (byte) 242;
      numArray6[5] = (byte) 168;
      numArray6[29] = (byte) 178;
      numArray6[38] = (byte) 247;
      numArray6[15] = (byte) 253;
      numArray6[49] = (byte) 65;
      numArray6[0] = (byte) 206;
      numArray6[51] = (byte) 131;
      numArray6[18] = (byte) 150;
      numArray6[41] = (byte) 137;
      numArray6[42] = (byte) 12;
      byte[] numArray7 = new byte[55]
      {
        (byte) 211,
        (byte) 173,
        (byte) 136,
        (byte) 192 /*0xC0*/,
        (byte) 42,
        (byte) 195,
        (byte) 43,
        (byte) 42,
        (byte) 116,
        (byte) 163,
        (byte) 228,
        (byte) 171,
        (byte) 58,
        (byte) 230,
        (byte) 97,
        (byte) 133,
        (byte) 131,
        (byte) 27,
        (byte) 135,
        (byte) 201,
        (byte) 229,
        (byte) 122,
        (byte) 8,
        (byte) 191,
        (byte) 130,
        (byte) 83,
        (byte) 105,
        (byte) 127 /*0x7F*/,
        (byte) 121,
        (byte) 25,
        (byte) 46,
        (byte) 32 /*0x20*/,
        (byte) 95,
        (byte) 126,
        (byte) 174,
        (byte) 110,
        (byte) 187,
        (byte) 195,
        (byte) 244,
        (byte) 84,
        (byte) 195,
        (byte) 165,
        (byte) 233,
        (byte) 94,
        (byte) 106,
        (byte) 201,
        (byte) 238,
        byte.MaxValue,
        (byte) 22,
        (byte) 139,
        (byte) 102,
        (byte) 236,
        (byte) 11,
        (byte) 137,
        (byte) 72
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[27]
      {
        (byte) 134,
        (byte) 191,
        (byte) 226,
        (byte) 103,
        (byte) 3,
        (byte) 150,
        (byte) 200,
        (byte) 188,
        (byte) 187,
        (byte) 74,
        (byte) 253,
        (byte) 48 /*0x30*/,
        (byte) 218,
        (byte) 159,
        (byte) 254,
        (byte) 162,
        (byte) 53,
        (byte) 23,
        (byte) 221,
        (byte) 4,
        (byte) 114,
        (byte) 181,
        (byte) 110,
        (byte) 150,
        (byte) 125,
        (byte) 82,
        (byte) 49
      };
      byte[] numArray9 = new byte[27]
      {
        (byte) 81,
        (byte) 114,
        (byte) 152,
        (byte) 135,
        (byte) 28,
        (byte) 125,
        (byte) 168,
        (byte) 54,
        (byte) 11,
        (byte) 94,
        (byte) 190,
        (byte) 56,
        (byte) 1,
        (byte) 182,
        (byte) 124,
        (byte) 192 /*0xC0*/,
        (byte) 254,
        (byte) 76,
        (byte) 101,
        (byte) 177,
        (byte) 240 /*0xF0*/,
        (byte) 245,
        (byte) 229,
        (byte) 205,
        (byte) 13,
        (byte) 242,
        (byte) 72
      };
      key.Query(true, 335, numArray8, numArray8);
      Array.Copy((Array) numArray8, 0, (Array) numArray1, 165, 27);
      for (int index = 0; index < 27; ++index)
        numArray1[index + 165] ^= numArray9[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray10 = new byte[192 /*0xC0*/];
    byte[] numArray11 = new byte[55]
    {
      (byte) 42,
      (byte) 50,
      (byte) 42,
      (byte) 8,
      (byte) 152,
      (byte) 81,
      (byte) 23,
      (byte) 0,
      (byte) 72,
      (byte) 168,
      (byte) 160 /*0xA0*/,
      (byte) 20,
      (byte) 40,
      (byte) 122,
      (byte) 135,
      (byte) 123,
      (byte) 213,
      (byte) 227,
      (byte) 202,
      (byte) 69,
      (byte) 105,
      (byte) 111,
      (byte) 96 /*0x60*/,
      (byte) 225,
      (byte) 208 /*0xD0*/,
      (byte) 208 /*0xD0*/,
      (byte) 80 /*0x50*/,
      (byte) 75,
      (byte) 233,
      (byte) 176 /*0xB0*/,
      (byte) 101,
      (byte) 182,
      (byte) 150,
      (byte) 173,
      (byte) 31 /*0x1F*/,
      (byte) 203,
      (byte) 6,
      (byte) 209,
      (byte) 7,
      (byte) 173,
      (byte) 139,
      (byte) 53,
      (byte) 206,
      (byte) 52,
      (byte) 100,
      (byte) 94,
      (byte) 141,
      (byte) 243,
      (byte) 15,
      (byte) 2,
      (byte) 124,
      (byte) 78,
      (byte) 72,
      (byte) 224 /*0xE0*/,
      (byte) 164
    };
    byte[] numArray12 = new byte[55];
    numArray12[17] = (byte) 189;
    numArray12[1] = (byte) 52;
    numArray12[47] = (byte) 2;
    numArray12[3] = (byte) 41;
    numArray12[33] = (byte) 121;
    numArray12[18] = (byte) 30;
    numArray12[20] = (byte) 233;
    numArray12[6] = (byte) 21;
    numArray12[8] = (byte) 84;
    numArray12[9] = (byte) 147;
    numArray12[10] = (byte) 93;
    numArray12[19] = (byte) 45;
    numArray12[40] = (byte) 141;
    numArray12[4] = (byte) 59;
    numArray12[14] = (byte) 106;
    numArray12[15] = (byte) 140;
    numArray12[16 /*0x10*/] = (byte) 152;
    numArray12[48 /*0x30*/] = (byte) 7;
    numArray12[21] = (byte) 43;
    numArray12[38] = (byte) 37;
    numArray12[13] = (byte) 20;
    numArray12[35] = (byte) 227;
    numArray12[22] = (byte) 17;
    numArray12[25] = (byte) 39;
    numArray12[0] = (byte) 58;
    numArray12[39] = (byte) 67;
    numArray12[26] = (byte) 146;
    numArray12[27] = (byte) 246;
    numArray12[28] = (byte) 92;
    numArray12[29] = (byte) 153;
    numArray12[43] = (byte) 249;
    numArray12[52] = (byte) 66;
    numArray12[32 /*0x20*/] = (byte) 49;
    numArray12[54] = (byte) 132;
    numArray12[31 /*0x1F*/] = (byte) 177;
    numArray12[7] = (byte) 79;
    numArray12[2] = (byte) 231;
    numArray12[37] = (byte) 216;
    numArray12[41] = (byte) 235;
    numArray12[12] = (byte) 48 /*0x30*/;
    numArray12[46] = (byte) 86;
    numArray12[34] = (byte) 161;
    numArray12[11] = (byte) 206;
    numArray12[36] = (byte) 114;
    numArray12[42] = (byte) 67;
    numArray12[45] = (byte) 107;
    numArray12[24] = (byte) 201;
    numArray12[30] = (byte) 147;
    numArray12[5] = (byte) 8;
    numArray12[49] = (byte) 210;
    numArray12[44] = (byte) 161;
    numArray12[51] = (byte) 186;
    numArray12[50] = (byte) 236;
    numArray12[53] = (byte) 17;
    numArray12[23] = (byte) 186;
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray10, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index] ^= numArray12[index];
    byte[] numArray13 = new byte[55];
    numArray13[4] = (byte) 144 /*0x90*/;
    numArray13[6] = (byte) 100;
    numArray13[3] = (byte) 163;
    numArray13[11] = (byte) 246;
    numArray13[40] = (byte) 210;
    numArray13[26] = (byte) 88;
    numArray13[52] = (byte) 89;
    numArray13[27] = (byte) 187;
    numArray13[38] = (byte) 46;
    numArray13[23] = (byte) 49;
    numArray13[10] = (byte) 112 /*0x70*/;
    numArray13[44] = (byte) 161;
    numArray13[12] = (byte) 194;
    numArray13[13] = (byte) 183;
    numArray13[46] = (byte) 144 /*0x90*/;
    numArray13[15] = (byte) 156;
    numArray13[29] = (byte) 66;
    numArray13[20] = (byte) 57;
    numArray13[50] = (byte) 188;
    numArray13[19] = (byte) 105;
    numArray13[51] = (byte) 42;
    numArray13[35] = (byte) 211;
    numArray13[9] = (byte) 199;
    numArray13[45] = (byte) 161;
    numArray13[24] = (byte) 196;
    numArray13[25] = (byte) 232;
    numArray13[7] = (byte) 43;
    numArray13[30] = (byte) 144 /*0x90*/;
    numArray13[28] = (byte) 165;
    numArray13[21] = (byte) 1;
    numArray13[54] = (byte) 108;
    numArray13[31 /*0x1F*/] = (byte) 153;
    numArray13[32 /*0x20*/] = (byte) 74;
    numArray13[33] = (byte) 83;
    numArray13[16 /*0x10*/] = (byte) 111;
    numArray13[18] = (byte) 22;
    numArray13[36] = (byte) 188;
    numArray13[37] = (byte) 9;
    numArray13[49] = (byte) 213;
    numArray13[39] = (byte) 236;
    numArray13[47] = (byte) 214;
    numArray13[41] = (byte) 198;
    numArray13[42] = byte.MaxValue;
    numArray13[1] = (byte) 197;
    numArray13[0] = (byte) 205;
    numArray13[17] = (byte) 251;
    numArray13[14] = (byte) 106;
    numArray13[8] = (byte) 123;
    numArray13[48 /*0x30*/] = (byte) 14;
    numArray13[34] = (byte) 239;
    numArray13[43] = (byte) 25;
    numArray13[5] = (byte) 49;
    numArray13[22] = (byte) 145;
    numArray13[53] = (byte) 176 /*0xB0*/;
    numArray13[2] = (byte) 131;
    byte[] numArray14 = new byte[55];
    numArray14[14] = (byte) 198;
    numArray14[25] = (byte) 205;
    numArray14[2] = (byte) 136;
    numArray14[12] = (byte) 136;
    numArray14[4] = (byte) 11;
    numArray14[24] = (byte) 58;
    numArray14[23] = (byte) 204;
    numArray14[7] = (byte) 26;
    numArray14[41] = (byte) 123;
    numArray14[9] = (byte) 171;
    numArray14[30] = (byte) 137;
    numArray14[11] = (byte) 100;
    numArray14[52] = (byte) 177;
    numArray14[13] = (byte) 122;
    numArray14[37] = (byte) 234;
    numArray14[8] = (byte) 3;
    numArray14[47] = (byte) 234;
    numArray14[20] = (byte) 139;
    numArray14[18] = (byte) 238;
    numArray14[1] = (byte) 199;
    numArray14[10] = (byte) 116;
    numArray14[44] = (byte) 227;
    numArray14[17] = (byte) 199;
    numArray14[5] = (byte) 143;
    numArray14[32 /*0x20*/] = (byte) 115;
    numArray14[28] = (byte) 128 /*0x80*/;
    numArray14[31 /*0x1F*/] = (byte) 248;
    numArray14[27] = (byte) 233;
    numArray14[0] = (byte) 248;
    numArray14[29] = (byte) 40;
    numArray14[21] = (byte) 130;
    numArray14[33] = (byte) 156;
    numArray14[22] = (byte) 226;
    numArray14[15] = (byte) 82;
    numArray14[34] = (byte) 250;
    numArray14[19] = (byte) 193;
    numArray14[36] = (byte) 169;
    numArray14[16 /*0x10*/] = (byte) 88;
    numArray14[38] = (byte) 77;
    numArray14[39] = (byte) 99;
    numArray14[40] = (byte) 110;
    numArray14[51] = (byte) 203;
    numArray14[42] = (byte) 167;
    numArray14[43] = (byte) 180;
    numArray14[54] = (byte) 79;
    numArray14[45] = (byte) 113;
    numArray14[46] = (byte) 237;
    numArray14[26] = (byte) 137;
    numArray14[48 /*0x30*/] = (byte) 224 /*0xE0*/;
    numArray14[49] = (byte) 61;
    numArray14[35] = (byte) 232;
    numArray14[50] = (byte) 250;
    numArray14[6] = (byte) 56;
    numArray14[53] = (byte) 224 /*0xE0*/;
    numArray14[3] = (byte) 252;
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray10, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index + 55] ^= numArray14[index];
    byte[] numArray15 = new byte[55]
    {
      (byte) 252,
      (byte) 141,
      (byte) 24,
      (byte) 227,
      (byte) 200,
      (byte) 221,
      (byte) 131,
      (byte) 201,
      (byte) 132,
      (byte) 147,
      (byte) 157,
      (byte) 74,
      (byte) 20,
      (byte) 206,
      (byte) 205,
      (byte) 14,
      (byte) 91,
      (byte) 63 /*0x3F*/,
      (byte) 169,
      (byte) 40,
      (byte) 134,
      (byte) 145,
      (byte) 77,
      (byte) 159,
      (byte) 10,
      (byte) 177,
      (byte) 89,
      (byte) 21,
      (byte) 240 /*0xF0*/,
      (byte) 250,
      (byte) 211,
      (byte) 36,
      (byte) 203,
      (byte) 188,
      (byte) 170,
      (byte) 224 /*0xE0*/,
      (byte) 30,
      (byte) 216,
      (byte) 100,
      (byte) 171,
      (byte) 144 /*0x90*/,
      (byte) 71,
      (byte) 174,
      (byte) 77,
      (byte) 249,
      (byte) 31 /*0x1F*/,
      (byte) 109,
      (byte) 176 /*0xB0*/,
      (byte) 77,
      (byte) 9,
      (byte) 187,
      (byte) 108,
      (byte) 29,
      (byte) 102,
      (byte) 160 /*0xA0*/
    };
    byte[] numArray16 = new byte[55]
    {
      (byte) 236,
      (byte) 126,
      (byte) 47,
      (byte) 251,
      (byte) 245,
      (byte) 246,
      (byte) 234,
      (byte) 63 /*0x3F*/,
      (byte) 21,
      (byte) 155,
      (byte) 31 /*0x1F*/,
      (byte) 172,
      (byte) 117,
      (byte) 227,
      (byte) 30,
      (byte) 250,
      (byte) 128 /*0x80*/,
      (byte) 78,
      (byte) 195,
      (byte) 133,
      (byte) 211,
      (byte) 165,
      (byte) 33,
      (byte) 229,
      (byte) 138,
      (byte) 110,
      (byte) 146,
      (byte) 172,
      (byte) 83,
      (byte) 154,
      (byte) 229,
      (byte) 58,
      (byte) 85,
      (byte) 203,
      (byte) 191,
      (byte) 222,
      (byte) 116,
      (byte) 193,
      (byte) 27,
      (byte) 225,
      (byte) 142,
      (byte) 180,
      (byte) 249,
      (byte) 157,
      (byte) 173,
      (byte) 30,
      (byte) 159,
      (byte) 177,
      (byte) 170,
      (byte) 88,
      (byte) 221,
      (byte) 75,
      (byte) 18,
      (byte) 66,
      (byte) 81
    };
    key.Query(true, 335, numArray15, numArray15);
    Array.Copy((Array) numArray15, 0, (Array) numArray10, 110, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index + 110] ^= numArray16[index];
    byte[] numArray17 = new byte[27];
    numArray17[4] = (byte) 109;
    numArray17[1] = (byte) 217;
    numArray17[14] = (byte) 131;
    numArray17[21] = (byte) 165;
    numArray17[13] = (byte) 240 /*0xF0*/;
    numArray17[8] = (byte) 120;
    numArray17[6] = (byte) 138;
    numArray17[18] = (byte) 63 /*0x3F*/;
    numArray17[2] = (byte) 23;
    numArray17[3] = (byte) 131;
    numArray17[25] = (byte) 199;
    numArray17[11] = (byte) 36;
    numArray17[12] = (byte) 236;
    numArray17[9] = (byte) 127 /*0x7F*/;
    numArray17[7] = (byte) 191;
    numArray17[23] = (byte) 68;
    numArray17[16 /*0x10*/] = (byte) 254;
    numArray17[17] = (byte) 151;
    numArray17[0] = (byte) 121;
    numArray17[19] = (byte) 226;
    numArray17[5] = (byte) 216;
    numArray17[10] = (byte) 88;
    numArray17[22] = (byte) 234;
    numArray17[15] = (byte) 218;
    numArray17[24] = (byte) 169;
    numArray17[20] = byte.MaxValue;
    numArray17[26] = (byte) 231;
    byte[] numArray18 = new byte[27]
    {
      (byte) 225,
      (byte) 116,
      (byte) 15,
      (byte) 83,
      (byte) 244,
      (byte) 177,
      (byte) 163,
      (byte) 59,
      (byte) 136,
      (byte) 90,
      (byte) 39,
      (byte) 252,
      (byte) 80 /*0x50*/,
      (byte) 107,
      (byte) 167,
      (byte) 105,
      (byte) 202,
      (byte) 45,
      (byte) 117,
      (byte) 217,
      (byte) 170,
      (byte) 133,
      (byte) 78,
      (byte) 36,
      (byte) 70,
      (byte) 4,
      (byte) 173
    };
    key.Query(true, 335, numArray17, numArray17);
    Array.Copy((Array) numArray17, 0, (Array) numArray10, 165, 27);
    for (int index = 0; index < 27; ++index)
      numArray10[index + 165] ^= numArray18[index];
    return Encoding.UTF8.GetString(numArray10);
  }

  internal static int ssp_appserver_14251(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 166,
      (byte) 241,
      (byte) 76,
      (byte) 89,
      (byte) 113,
      (byte) 26,
      (byte) 60,
      (byte) 229,
      (byte) 76,
      (byte) 75,
      (byte) 132,
      (byte) 141,
      (byte) 215,
      (byte) 117,
      (byte) 208 /*0xD0*/,
      (byte) 190,
      (byte) 113,
      (byte) 37,
      (byte) 74,
      (byte) 47,
      (byte) 28,
      (byte) 102,
      (byte) 206,
      (byte) 6,
      (byte) 212,
      (byte) 232,
      (byte) 149,
      (byte) 187,
      (byte) 183,
      (byte) 95,
      (byte) 209,
      (byte) 16 /*0x10*/,
      (byte) 122,
      (byte) 76,
      (byte) 123,
      (byte) 124,
      (byte) 216,
      (byte) 52,
      (byte) 15,
      (byte) 133,
      (byte) 216,
      (byte) 167,
      (byte) 21,
      (byte) 205,
      (byte) 179,
      (byte) 4,
      (byte) 71,
      (byte) 89
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 10,
      (byte) 190,
      (byte) 74,
      (byte) 123,
      (byte) 8,
      (byte) 165,
      (byte) 218,
      (byte) 213,
      (byte) 224 /*0xE0*/,
      (byte) 168,
      (byte) 0,
      (byte) 29,
      (byte) 46,
      (byte) 226,
      (byte) 52,
      (byte) 233,
      (byte) 167,
      (byte) 17,
      (byte) 166,
      (byte) 249,
      (byte) 53,
      (byte) 47,
      (byte) 179,
      (byte) 219,
      (byte) 49,
      (byte) 40,
      (byte) 142,
      (byte) 69,
      (byte) 115,
      (byte) 61,
      (byte) 181,
      (byte) 205,
      (byte) 89,
      (byte) 49,
      (byte) 224 /*0xE0*/,
      (byte) 221,
      (byte) 132,
      (byte) 106,
      (byte) 149,
      (byte) 161,
      (byte) 72,
      (byte) 79,
      (byte) 81,
      (byte) 112 /*0x70*/,
      (byte) 215,
      (byte) 93,
      (byte) 222,
      (byte) 74
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_14252(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 163,
      (byte) 189,
      (byte) 64 /*0x40*/,
      (byte) 35,
      (byte) 69,
      (byte) 132,
      (byte) 137,
      (byte) 133,
      (byte) 129,
      (byte) 46,
      (byte) 236,
      (byte) 122,
      (byte) 116,
      (byte) 18,
      (byte) 138,
      (byte) 249,
      (byte) 83,
      (byte) 230,
      (byte) 60,
      (byte) 21,
      (byte) 196,
      (byte) 86,
      (byte) 117,
      (byte) 81,
      (byte) 1,
      (byte) 83,
      (byte) 207,
      (byte) 160 /*0xA0*/,
      (byte) 59,
      (byte) 42,
      (byte) 237,
      (byte) 144 /*0x90*/,
      (byte) 35,
      (byte) 107,
      (byte) 215,
      (byte) 142,
      (byte) 59,
      (byte) 89,
      (byte) 146,
      (byte) 176 /*0xB0*/,
      (byte) 134,
      (byte) 0,
      (byte) 87,
      (byte) 121,
      (byte) 40,
      (byte) 82,
      (byte) 62,
      (byte) 242
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 73,
      (byte) 20,
      (byte) 205,
      (byte) 200,
      (byte) 153,
      (byte) 44,
      (byte) 194,
      (byte) 74,
      (byte) 52,
      (byte) 205,
      (byte) 194,
      (byte) 163,
      (byte) 248,
      (byte) 140,
      (byte) 125,
      (byte) 221,
      (byte) 90,
      (byte) 62,
      (byte) 159,
      (byte) 174,
      (byte) 17,
      (byte) 50,
      (byte) 251,
      (byte) 189,
      (byte) 54,
      (byte) 98,
      (byte) 171,
      (byte) 171,
      (byte) 20,
      (byte) 12,
      (byte) 164,
      (byte) 7,
      (byte) 64 /*0x40*/,
      (byte) 234,
      (byte) 107,
      (byte) 30,
      (byte) 53,
      (byte) 81,
      (byte) 139,
      (byte) 38,
      (byte) 61,
      (byte) 213,
      (byte) 175,
      (byte) 108,
      (byte) 11,
      (byte) 170,
      (byte) 245,
      (byte) 118
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[39];
    byte[] response2 = new byte[39];
    Array.Copy((Array) sc_14238.sspq, 148, (Array) numArray2, 0, 39);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_14238.sspr, 148, (Array) numArray2, 0, 39);
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

  internal static int ssp_appserver_14253(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[8] = (byte) 53;
    sourceArray1[34] = (byte) 178;
    sourceArray1[2] = (byte) 157;
    sourceArray1[22] = (byte) 215;
    sourceArray1[25] = (byte) 104;
    sourceArray1[31 /*0x1F*/] = (byte) 180;
    sourceArray1[6] = (byte) 204;
    sourceArray1[7] = (byte) 251;
    sourceArray1[37] = (byte) 12;
    sourceArray1[5] = (byte) 56;
    sourceArray1[21] = (byte) 220;
    sourceArray1[9] = (byte) 183;
    sourceArray1[12] = (byte) 66;
    sourceArray1[13] = (byte) 142;
    sourceArray1[14] = (byte) 6;
    sourceArray1[15] = (byte) 9;
    sourceArray1[26] = (byte) 53;
    sourceArray1[17] = (byte) 77;
    sourceArray1[42] = (byte) 224 /*0xE0*/;
    sourceArray1[19] = (byte) 198;
    sourceArray1[4] = (byte) 0;
    sourceArray1[32 /*0x20*/] = (byte) 113;
    sourceArray1[23] = (byte) 5;
    sourceArray1[20] = (byte) 115;
    sourceArray1[24] = (byte) 109;
    sourceArray1[33] = (byte) 195;
    sourceArray1[16 /*0x10*/] = (byte) 28;
    sourceArray1[0] = (byte) 161;
    sourceArray1[28] = (byte) 204;
    sourceArray1[36] = (byte) 241;
    sourceArray1[30] = (byte) 175;
    sourceArray1[1] = (byte) 251;
    sourceArray1[11] = (byte) 162;
    sourceArray1[18] = (byte) 76;
    sourceArray1[47] = (byte) 124;
    sourceArray1[35] = (byte) 144 /*0x90*/;
    sourceArray1[3] = (byte) 183;
    sourceArray1[29] = (byte) 125;
    sourceArray1[38] = (byte) 254;
    sourceArray1[39] = (byte) 195;
    sourceArray1[40] = (byte) 92;
    sourceArray1[41] = (byte) 178;
    sourceArray1[27] = (byte) 94;
    sourceArray1[43] = (byte) 37;
    sourceArray1[44] = (byte) 32 /*0x20*/;
    sourceArray1[45] = (byte) 57;
    sourceArray1[46] = (byte) 171;
    sourceArray1[10] = (byte) 194;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 233,
      (byte) 253,
      (byte) 81,
      (byte) 204,
      (byte) 105,
      (byte) 24,
      (byte) 132,
      (byte) 102,
      (byte) 29,
      (byte) 33,
      (byte) 217,
      (byte) 176 /*0xB0*/,
      (byte) 12,
      (byte) 26,
      (byte) 132,
      (byte) 139,
      (byte) 228,
      (byte) 130,
      (byte) 247,
      (byte) 208 /*0xD0*/,
      (byte) 247,
      (byte) 36,
      (byte) 169,
      (byte) 6,
      (byte) 226,
      (byte) 224 /*0xE0*/,
      (byte) 98,
      (byte) 187,
      (byte) 210,
      (byte) 157,
      (byte) 241,
      (byte) 61,
      (byte) 151,
      (byte) 137,
      (byte) 59,
      (byte) 83,
      (byte) 212,
      (byte) 53,
      (byte) 92,
      (byte) 18,
      (byte) 192 /*0xC0*/,
      (byte) 232,
      (byte) 156,
      (byte) 154,
      (byte) 1,
      (byte) 14,
      (byte) 63 /*0x3F*/,
      (byte) 36
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[39];
    byte[] response2 = new byte[39];
    Array.Copy((Array) sc_14238.sspq, 187, (Array) numArray2, 0, 39);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_14238.sspr, 187, (Array) numArray2, 0, 39);
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

  internal static string ssp_appserver_14254()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[26];
      byte[] numArray2 = new byte[26]
      {
        (byte) 129,
        (byte) 73,
        (byte) 208 /*0xD0*/,
        (byte) 253,
        (byte) 35,
        (byte) 251,
        (byte) 164,
        (byte) 170,
        (byte) 140,
        (byte) 116,
        (byte) 231,
        (byte) 158,
        (byte) 129,
        (byte) 134,
        (byte) 193,
        (byte) 195,
        (byte) 171,
        (byte) 176 /*0xB0*/,
        (byte) 118,
        (byte) 169,
        (byte) 15,
        (byte) 7,
        (byte) 167,
        (byte) 158,
        (byte) 196,
        (byte) 247
      };
      byte[] numArray3 = new byte[26]
      {
        (byte) 254,
        (byte) 233,
        (byte) 147,
        (byte) 214,
        (byte) 172,
        (byte) 21,
        (byte) 233,
        (byte) 44,
        (byte) 144 /*0x90*/,
        (byte) 211,
        (byte) 153,
        (byte) 17,
        (byte) 72,
        (byte) 230,
        (byte) 165,
        (byte) 226,
        (byte) 2,
        byte.MaxValue,
        (byte) 106,
        (byte) 190,
        (byte) 77,
        (byte) 84,
        (byte) 63 /*0x3F*/,
        (byte) 56,
        (byte) 211,
        (byte) 205
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 26);
      for (int index = 0; index < 26; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[26];
    byte[] numArray5 = new byte[26]
    {
      (byte) 72,
      (byte) 49,
      (byte) 31 /*0x1F*/,
      (byte) 2,
      (byte) 247,
      (byte) 20,
      (byte) 8,
      (byte) 116,
      (byte) 130,
      (byte) 203,
      (byte) 14,
      (byte) 26,
      (byte) 163,
      (byte) 91,
      (byte) 84,
      (byte) 149,
      (byte) 160 /*0xA0*/,
      (byte) 81,
      (byte) 15,
      (byte) 15,
      (byte) 102,
      (byte) 230,
      (byte) 104,
      (byte) 112 /*0x70*/,
      (byte) 56,
      (byte) 161
    };
    byte[] numArray6 = new byte[26];
    numArray6[10] = (byte) 59;
    numArray6[1] = (byte) 218;
    numArray6[2] = (byte) 199;
    numArray6[6] = (byte) 245;
    numArray6[23] = (byte) 25;
    numArray6[5] = (byte) 208 /*0xD0*/;
    numArray6[16 /*0x10*/] = (byte) 58;
    numArray6[11] = (byte) 109;
    numArray6[17] = (byte) 168;
    numArray6[9] = (byte) 102;
    numArray6[3] = (byte) 134;
    numArray6[0] = (byte) 225;
    numArray6[12] = (byte) 230;
    numArray6[13] = (byte) 142;
    numArray6[14] = (byte) 47;
    numArray6[15] = (byte) 7;
    numArray6[19] = (byte) 82;
    numArray6[24] = (byte) 251;
    numArray6[18] = (byte) 237;
    numArray6[4] = (byte) 56;
    numArray6[20] = (byte) 119;
    numArray6[21] = (byte) 30;
    numArray6[22] = (byte) 3;
    numArray6[7] = (byte) 225;
    numArray6[8] = (byte) 246;
    numArray6[25] = (byte) 208 /*0xD0*/;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 26);
    for (int index = 0; index < 26; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static int ssp_appserver_14255(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 175,
      (byte) 202,
      (byte) 85,
      (byte) 154,
      (byte) 103,
      (byte) 67,
      (byte) 73,
      (byte) 159,
      (byte) 238,
      (byte) 89,
      (byte) 229,
      (byte) 24,
      (byte) 17,
      (byte) 204,
      (byte) 4,
      (byte) 198,
      (byte) 157,
      (byte) 87,
      (byte) 23,
      (byte) 189,
      (byte) 131,
      (byte) 7,
      (byte) 168,
      (byte) 53,
      (byte) 206,
      (byte) 78,
      (byte) 39,
      (byte) 21,
      (byte) 134,
      (byte) 84,
      (byte) 237,
      (byte) 57,
      (byte) 225,
      (byte) 122,
      (byte) 24,
      (byte) 208 /*0xD0*/,
      (byte) 52,
      (byte) 215,
      (byte) 81,
      (byte) 11,
      (byte) 222,
      (byte) 69,
      (byte) 122,
      (byte) 53,
      (byte) 134,
      (byte) 147,
      (byte) 65,
      (byte) 60
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[28] = (byte) 148;
    sourceArray2[1] = (byte) 35;
    sourceArray2[2] = (byte) 94;
    sourceArray2[3] = (byte) 73;
    sourceArray2[4] = (byte) 106;
    sourceArray2[8] = (byte) 6;
    sourceArray2[6] = (byte) 91;
    sourceArray2[7] = (byte) 200;
    sourceArray2[34] = (byte) 18;
    sourceArray2[43] = (byte) 165;
    sourceArray2[10] = (byte) 29;
    sourceArray2[11] = (byte) 232;
    sourceArray2[20] = (byte) 108;
    sourceArray2[13] = (byte) 27;
    sourceArray2[0] = (byte) 196;
    sourceArray2[15] = (byte) 191;
    sourceArray2[16 /*0x10*/] = (byte) 73;
    sourceArray2[47] = (byte) 83;
    sourceArray2[18] = (byte) 200;
    sourceArray2[45] = (byte) 77;
    sourceArray2[14] = (byte) 18;
    sourceArray2[21] = (byte) 33;
    sourceArray2[41] = (byte) 105;
    sourceArray2[23] = (byte) 245;
    sourceArray2[24] = (byte) 145;
    sourceArray2[25] = (byte) 20;
    sourceArray2[40] = (byte) 178;
    sourceArray2[27] = (byte) 156;
    sourceArray2[33] = (byte) 45;
    sourceArray2[32 /*0x20*/] = (byte) 112 /*0x70*/;
    sourceArray2[30] = (byte) 163;
    sourceArray2[31 /*0x1F*/] = (byte) 151;
    sourceArray2[39] = (byte) 11;
    sourceArray2[12] = (byte) 64 /*0x40*/;
    sourceArray2[26] = (byte) 7;
    sourceArray2[5] = (byte) 206;
    sourceArray2[36] = (byte) 64 /*0x40*/;
    sourceArray2[35] = (byte) 92;
    sourceArray2[38] = (byte) 224 /*0xE0*/;
    sourceArray2[29] = (byte) 86;
    sourceArray2[17] = (byte) 198;
    sourceArray2[9] = (byte) 34;
    sourceArray2[42] = (byte) 244;
    sourceArray2[19] = (byte) 249;
    sourceArray2[22] = (byte) 88;
    sourceArray2[37] = (byte) 25;
    sourceArray2[46] = (byte) 224 /*0xE0*/;
    sourceArray2[44] = (byte) 248;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_14256(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 170,
      (byte) 136,
      (byte) 235,
      (byte) 76,
      (byte) 103,
      (byte) 118,
      (byte) 97,
      (byte) 197,
      (byte) 6,
      (byte) 26,
      (byte) 174,
      (byte) 113,
      (byte) 145,
      (byte) 11,
      (byte) 49,
      (byte) 63 /*0x3F*/,
      (byte) 112 /*0x70*/,
      (byte) 116,
      (byte) 250,
      (byte) 224 /*0xE0*/,
      (byte) 250,
      (byte) 97,
      (byte) 66,
      (byte) 133,
      (byte) 126,
      (byte) 186,
      (byte) 205,
      (byte) 28,
      (byte) 52,
      (byte) 17,
      (byte) 172,
      (byte) 168,
      (byte) 212,
      (byte) 221,
      (byte) 98,
      (byte) 97,
      (byte) 10,
      (byte) 91,
      (byte) 189,
      (byte) 250,
      (byte) 106,
      (byte) 118,
      (byte) 46,
      (byte) 86,
      byte.MaxValue,
      (byte) 163,
      (byte) 122,
      (byte) 45
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 93,
      (byte) 118,
      (byte) 223,
      (byte) 154,
      (byte) 231,
      (byte) 100,
      (byte) 155,
      (byte) 237,
      (byte) 132,
      (byte) 204,
      (byte) 54,
      (byte) 172,
      (byte) 243,
      (byte) 125,
      (byte) 0,
      (byte) 57,
      (byte) 8,
      (byte) 201,
      (byte) 108,
      (byte) 221,
      (byte) 190,
      (byte) 7,
      (byte) 3,
      (byte) 249,
      (byte) 199,
      (byte) 135,
      (byte) 120,
      (byte) 191,
      (byte) 197,
      (byte) 68,
      (byte) 0,
      (byte) 68,
      (byte) 49,
      (byte) 193,
      (byte) 99,
      (byte) 203,
      (byte) 107,
      (byte) 201,
      (byte) 156,
      (byte) 230,
      (byte) 159,
      (byte) 220,
      (byte) 133,
      (byte) 181,
      (byte) 44,
      (byte) 187,
      (byte) 177,
      (byte) 189
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[37];
    byte[] response2 = new byte[37];
    Array.Copy((Array) sc_14238.sspq, 226, (Array) numArray2, 0, 37);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_14238.sspr, 226, (Array) numArray2, 0, 37);
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

  internal static int ssp_appserver_14257(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 163,
      (byte) 140,
      (byte) 16 /*0x10*/,
      (byte) 81,
      (byte) 38,
      (byte) 229,
      (byte) 57,
      (byte) 231,
      (byte) 249,
      (byte) 5,
      (byte) 198,
      (byte) 131,
      (byte) 239,
      (byte) 53,
      (byte) 166,
      (byte) 182,
      (byte) 208 /*0xD0*/,
      (byte) 132,
      (byte) 223,
      (byte) 21,
      (byte) 89,
      (byte) 224 /*0xE0*/,
      (byte) 243,
      (byte) 108,
      (byte) 9,
      (byte) 230,
      (byte) 198,
      (byte) 158,
      (byte) 46,
      (byte) 45,
      (byte) 195,
      (byte) 56,
      (byte) 144 /*0x90*/,
      (byte) 179,
      (byte) 145,
      (byte) 136,
      (byte) 165,
      (byte) 173,
      (byte) 79,
      (byte) 158,
      (byte) 249,
      (byte) 4,
      (byte) 140,
      (byte) 212,
      (byte) 122,
      (byte) 27,
      (byte) 110,
      (byte) 148
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 24,
      (byte) 169,
      (byte) 183,
      (byte) 221,
      (byte) 122,
      (byte) 62,
      (byte) 101,
      (byte) 72,
      (byte) 36,
      (byte) 191,
      (byte) 118,
      (byte) 107,
      (byte) 173,
      (byte) 138,
      (byte) 210,
      (byte) 138,
      (byte) 78,
      (byte) 96 /*0x60*/,
      (byte) 234,
      (byte) 244,
      (byte) 102,
      (byte) 132,
      (byte) 149,
      (byte) 108,
      (byte) 87,
      (byte) 232,
      (byte) 200,
      (byte) 122,
      (byte) 202,
      (byte) 86,
      (byte) 207,
      (byte) 4,
      (byte) 241,
      (byte) 45,
      (byte) 147,
      (byte) 45,
      (byte) 212,
      (byte) 22,
      (byte) 159,
      (byte) 125,
      (byte) 68,
      (byte) 175,
      (byte) 142,
      (byte) 49,
      (byte) 199,
      (byte) 38,
      (byte) 215,
      (byte) 193
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_14258(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 3,
      (byte) 194,
      (byte) 194,
      (byte) 153,
      (byte) 164,
      (byte) 71,
      (byte) 63 /*0x3F*/,
      (byte) 30,
      (byte) 154,
      (byte) 184,
      (byte) 203,
      (byte) 66,
      (byte) 141,
      (byte) 243,
      (byte) 59,
      (byte) 164,
      (byte) 30,
      (byte) 17,
      (byte) 34,
      (byte) 53,
      (byte) 65,
      (byte) 28,
      (byte) 84,
      (byte) 84,
      (byte) 30,
      (byte) 241,
      (byte) 237,
      (byte) 119,
      (byte) 49,
      (byte) 239,
      (byte) 165,
      (byte) 38,
      (byte) 110,
      (byte) 157,
      (byte) 141,
      (byte) 240 /*0xF0*/,
      (byte) 206,
      (byte) 238,
      (byte) 0,
      (byte) 102,
      (byte) 15,
      (byte) 127 /*0x7F*/,
      (byte) 94,
      (byte) 75,
      (byte) 192 /*0xC0*/,
      (byte) 146,
      (byte) 237,
      (byte) 40
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[39] = (byte) 174;
    sourceArray2[28] = (byte) 68;
    sourceArray2[1] = (byte) 26;
    sourceArray2[40] = (byte) 27;
    sourceArray2[20] = (byte) 223;
    sourceArray2[7] = (byte) 52;
    sourceArray2[18] = byte.MaxValue;
    sourceArray2[2] = (byte) 60;
    sourceArray2[22] = (byte) 91;
    sourceArray2[42] = (byte) 113;
    sourceArray2[10] = (byte) 25;
    sourceArray2[19] = (byte) 145;
    sourceArray2[12] = (byte) 168;
    sourceArray2[25] = (byte) 244;
    sourceArray2[14] = (byte) 29;
    sourceArray2[15] = (byte) 137;
    sourceArray2[0] = (byte) 122;
    sourceArray2[17] = (byte) 76;
    sourceArray2[29] = (byte) 9;
    sourceArray2[9] = (byte) 52;
    sourceArray2[5] = (byte) 102;
    sourceArray2[21] = (byte) 38;
    sourceArray2[41] = (byte) 207;
    sourceArray2[23] = (byte) 182;
    sourceArray2[24] = (byte) 25;
    sourceArray2[8] = (byte) 154;
    sourceArray2[3] = (byte) 92;
    sourceArray2[27] = (byte) 213;
    sourceArray2[6] = (byte) 36;
    sourceArray2[34] = (byte) 239;
    sourceArray2[30] = (byte) 23;
    sourceArray2[31 /*0x1F*/] = (byte) 234;
    sourceArray2[32 /*0x20*/] = (byte) 115;
    sourceArray2[26] = (byte) 95;
    sourceArray2[47] = (byte) 235;
    sourceArray2[35] = (byte) 246;
    sourceArray2[36] = (byte) 39;
    sourceArray2[37] = (byte) 133;
    sourceArray2[38] = (byte) 93;
    sourceArray2[13] = (byte) 38;
    sourceArray2[33] = (byte) 28;
    sourceArray2[43] = (byte) 250;
    sourceArray2[4] = (byte) 96 /*0x60*/;
    sourceArray2[16 /*0x10*/] = (byte) 251;
    sourceArray2[44] = (byte) 195;
    sourceArray2[45] = (byte) 126;
    sourceArray2[46] = (byte) 10;
    sourceArray2[11] = (byte) 169;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_14259(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 252,
      (byte) 194,
      (byte) 232,
      (byte) 27,
      (byte) 98,
      (byte) 103,
      (byte) 40,
      (byte) 250,
      (byte) 152,
      (byte) 42,
      (byte) 101,
      (byte) 217,
      (byte) 238,
      (byte) 174,
      (byte) 148,
      (byte) 132,
      (byte) 172,
      (byte) 76,
      (byte) 229,
      (byte) 100,
      (byte) 132,
      (byte) 172,
      (byte) 192 /*0xC0*/,
      (byte) 199,
      (byte) 117,
      (byte) 187,
      (byte) 195,
      (byte) 176 /*0xB0*/,
      (byte) 222,
      (byte) 48 /*0x30*/,
      (byte) 9,
      (byte) 207,
      (byte) 120,
      (byte) 205,
      (byte) 7,
      (byte) 3,
      (byte) 92,
      (byte) 37,
      (byte) 252,
      (byte) 198,
      (byte) 24,
      (byte) 85,
      (byte) 171,
      (byte) 123,
      (byte) 29,
      (byte) 134,
      (byte) 133,
      (byte) 158
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[4] = (byte) 3;
    sourceArray2[1] = (byte) 152;
    sourceArray2[5] = (byte) 61;
    sourceArray2[47] = (byte) 100;
    sourceArray2[25] = (byte) 143;
    sourceArray2[44] = (byte) 116;
    sourceArray2[13] = (byte) 57;
    sourceArray2[24] = (byte) 82;
    sourceArray2[8] = (byte) 211;
    sourceArray2[21] = (byte) 179;
    sourceArray2[3] = (byte) 230;
    sourceArray2[6] = (byte) 89;
    sourceArray2[12] = (byte) 81;
    sourceArray2[39] = (byte) 75;
    sourceArray2[14] = (byte) 216;
    sourceArray2[17] = (byte) 88;
    sourceArray2[22] = (byte) 52;
    sourceArray2[18] = (byte) 116;
    sourceArray2[10] = (byte) 138;
    sourceArray2[19] = (byte) 228;
    sourceArray2[20] = (byte) 75;
    sourceArray2[23] = (byte) 105;
    sourceArray2[41] = (byte) 239;
    sourceArray2[15] = (byte) 89;
    sourceArray2[9] = (byte) 87;
    sourceArray2[38] = (byte) 199;
    sourceArray2[0] = (byte) 54;
    sourceArray2[27] = (byte) 119;
    sourceArray2[28] = (byte) 249;
    sourceArray2[29] = (byte) 157;
    sourceArray2[45] = (byte) 133;
    sourceArray2[30] = (byte) 139;
    sourceArray2[32 /*0x20*/] = (byte) 245;
    sourceArray2[2] = (byte) 104;
    sourceArray2[34] = (byte) 59;
    sourceArray2[35] = (byte) 129;
    sourceArray2[7] = (byte) 92;
    sourceArray2[37] = (byte) 188;
    sourceArray2[31 /*0x1F*/] = (byte) 219;
    sourceArray2[46] = (byte) 203;
    sourceArray2[16 /*0x10*/] = (byte) 243;
    sourceArray2[26] = (byte) 106;
    sourceArray2[42] = (byte) 1;
    sourceArray2[43] = (byte) 31 /*0x1F*/;
    sourceArray2[40] = (byte) 41;
    sourceArray2[33] = (byte) 184;
    sourceArray2[11] = (byte) 68;
    sourceArray2[36] = (byte) 78;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_14260(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 99,
      (byte) 247,
      (byte) 230,
      (byte) 154,
      (byte) 95,
      (byte) 22,
      (byte) 52,
      (byte) 107,
      (byte) 12,
      (byte) 78,
      (byte) 187,
      (byte) 118,
      (byte) 182,
      (byte) 113,
      (byte) 126,
      (byte) 63 /*0x3F*/,
      (byte) 0,
      (byte) 29,
      (byte) 126,
      (byte) 24,
      (byte) 159,
      (byte) 143,
      (byte) 239,
      (byte) 143,
      (byte) 236,
      (byte) 6,
      (byte) 252,
      (byte) 250,
      (byte) 171,
      (byte) 245,
      (byte) 190,
      (byte) 235,
      (byte) 33,
      (byte) 201,
      (byte) 210,
      (byte) 45,
      (byte) 82,
      (byte) 128 /*0x80*/,
      (byte) 234,
      (byte) 140,
      (byte) 71,
      (byte) 85,
      (byte) 133,
      (byte) 105,
      (byte) 70,
      (byte) 197,
      (byte) 201,
      (byte) 227
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 153,
      (byte) 230,
      (byte) 79,
      (byte) 245,
      (byte) 173,
      (byte) 80 /*0x50*/,
      (byte) 57,
      (byte) 92,
      (byte) 66,
      (byte) 170,
      (byte) 39,
      (byte) 48 /*0x30*/,
      (byte) 197,
      (byte) 31 /*0x1F*/,
      (byte) 30,
      (byte) 188,
      (byte) 242,
      (byte) 170,
      (byte) 208 /*0xD0*/,
      (byte) 139,
      (byte) 92,
      (byte) 87,
      (byte) 19,
      (byte) 87,
      (byte) 224 /*0xE0*/,
      (byte) 111,
      (byte) 231,
      (byte) 35,
      (byte) 247,
      (byte) 124,
      (byte) 43,
      (byte) 131,
      (byte) 96 /*0x60*/,
      (byte) 174,
      (byte) 196,
      (byte) 182,
      (byte) 216,
      (byte) 27,
      (byte) 247,
      (byte) 128 /*0x80*/,
      (byte) 186,
      (byte) 34,
      (byte) 225,
      (byte) 168,
      (byte) 89,
      (byte) 162,
      (byte) 239,
      (byte) 114
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_14261(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[39] = (byte) 210;
    sourceArray1[1] = (byte) 133;
    sourceArray1[2] = (byte) 212;
    sourceArray1[3] = (byte) 8;
    sourceArray1[9] = (byte) 192 /*0xC0*/;
    sourceArray1[11] = (byte) 143;
    sourceArray1[6] = (byte) 88;
    sourceArray1[7] = (byte) 26;
    sourceArray1[8] = (byte) 58;
    sourceArray1[30] = (byte) 74;
    sourceArray1[43] = (byte) 149;
    sourceArray1[32 /*0x20*/] = (byte) 153;
    sourceArray1[12] = (byte) 208 /*0xD0*/;
    sourceArray1[13] = (byte) 84;
    sourceArray1[42] = (byte) 13;
    sourceArray1[28] = (byte) 24;
    sourceArray1[25] = (byte) 72;
    sourceArray1[17] = (byte) 236;
    sourceArray1[15] = (byte) 159;
    sourceArray1[19] = (byte) 50;
    sourceArray1[22] = (byte) 73;
    sourceArray1[21] = (byte) 209;
    sourceArray1[35] = (byte) 67;
    sourceArray1[5] = (byte) 173;
    sourceArray1[14] = (byte) 237;
    sourceArray1[40] = (byte) 146;
    sourceArray1[41] = (byte) 114;
    sourceArray1[29] = (byte) 85;
    sourceArray1[26] = (byte) 38;
    sourceArray1[18] = (byte) 128 /*0x80*/;
    sourceArray1[4] = (byte) 87;
    sourceArray1[45] = (byte) 217;
    sourceArray1[0] = (byte) 111;
    sourceArray1[33] = (byte) 15;
    sourceArray1[34] = (byte) 177;
    sourceArray1[10] = (byte) 22;
    sourceArray1[36] = (byte) 254;
    sourceArray1[37] = (byte) 37;
    sourceArray1[38] = (byte) 251;
    sourceArray1[16 /*0x10*/] = (byte) 80 /*0x50*/;
    sourceArray1[20] = (byte) 242;
    sourceArray1[27] = (byte) 61;
    sourceArray1[23] = (byte) 9;
    sourceArray1[31 /*0x1F*/] = (byte) 188;
    sourceArray1[44] = (byte) 110;
    sourceArray1[47] = (byte) 113;
    sourceArray1[46] = (byte) 118;
    sourceArray1[24] = (byte) 134;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[28] = (byte) 224 /*0xE0*/;
    sourceArray2[14] = (byte) 251;
    sourceArray2[0] = (byte) 95;
    sourceArray2[16 /*0x10*/] = (byte) 7;
    sourceArray2[10] = (byte) 237;
    sourceArray2[5] = (byte) 202;
    sourceArray2[6] = (byte) 41;
    sourceArray2[7] = (byte) 203;
    sourceArray2[46] = (byte) 218;
    sourceArray2[32 /*0x20*/] = (byte) 213;
    sourceArray2[1] = (byte) 42;
    sourceArray2[11] = (byte) 110;
    sourceArray2[12] = (byte) 235;
    sourceArray2[13] = (byte) 250;
    sourceArray2[39] = (byte) 77;
    sourceArray2[15] = (byte) 29;
    sourceArray2[29] = (byte) 150;
    sourceArray2[17] = (byte) 198;
    sourceArray2[18] = (byte) 120;
    sourceArray2[19] = (byte) 113;
    sourceArray2[20] = (byte) 194;
    sourceArray2[21] = (byte) 233;
    sourceArray2[24] = (byte) 115;
    sourceArray2[45] = (byte) 89;
    sourceArray2[34] = (byte) 48 /*0x30*/;
    sourceArray2[4] = (byte) 166;
    sourceArray2[3] = (byte) 209;
    sourceArray2[27] = (byte) 153;
    sourceArray2[26] = (byte) 185;
    sourceArray2[33] = (byte) 50;
    sourceArray2[30] = (byte) 189;
    sourceArray2[31 /*0x1F*/] = (byte) 199;
    sourceArray2[2] = (byte) 35;
    sourceArray2[41] = (byte) 249;
    sourceArray2[47] = (byte) 168;
    sourceArray2[40] = (byte) 204;
    sourceArray2[36] = (byte) 74;
    sourceArray2[37] = (byte) 107;
    sourceArray2[38] = (byte) 49;
    sourceArray2[43] = (byte) 56;
    sourceArray2[23] = (byte) 77;
    sourceArray2[8] = (byte) 78;
    sourceArray2[42] = (byte) 92;
    sourceArray2[25] = (byte) 124;
    sourceArray2[44] = (byte) 82;
    sourceArray2[22] = (byte) 188;
    sourceArray2[9] = (byte) 169;
    sourceArray2[35] = (byte) 254;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_14262(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 121,
      (byte) 189,
      (byte) 67,
      (byte) 68,
      (byte) 125,
      (byte) 192 /*0xC0*/,
      (byte) 149,
      (byte) 28,
      (byte) 62,
      (byte) 144 /*0x90*/,
      (byte) 54,
      (byte) 161,
      (byte) 129,
      (byte) 167,
      (byte) 40,
      (byte) 88,
      (byte) 63 /*0x3F*/,
      (byte) 241,
      (byte) 170,
      (byte) 13,
      (byte) 31 /*0x1F*/,
      (byte) 234,
      (byte) 138,
      (byte) 252,
      (byte) 12,
      (byte) 80 /*0x50*/,
      (byte) 89,
      (byte) 248,
      (byte) 219,
      (byte) 88,
      (byte) 32 /*0x20*/,
      (byte) 105,
      (byte) 186,
      (byte) 81,
      (byte) 232,
      (byte) 107,
      (byte) 204,
      (byte) 86,
      (byte) 23,
      (byte) 197,
      (byte) 50,
      (byte) 0,
      (byte) 169,
      (byte) 158,
      (byte) 250,
      (byte) 53,
      (byte) 207,
      (byte) 83
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 204,
      (byte) 94,
      (byte) 78,
      (byte) 53,
      (byte) 228,
      (byte) 18,
      (byte) 83,
      (byte) 114,
      (byte) 119,
      (byte) 68,
      (byte) 225,
      (byte) 169,
      (byte) 91,
      (byte) 83,
      (byte) 128 /*0x80*/,
      (byte) 31 /*0x1F*/,
      (byte) 32 /*0x20*/,
      (byte) 135,
      (byte) 254,
      (byte) 202,
      (byte) 126,
      (byte) 54,
      (byte) 206,
      (byte) 75,
      (byte) 60,
      (byte) 154,
      (byte) 133,
      (byte) 25,
      (byte) 160 /*0xA0*/,
      (byte) 9,
      (byte) 26,
      (byte) 83,
      (byte) 167,
      (byte) 191,
      (byte) 108,
      (byte) 167,
      (byte) 63 /*0x3F*/,
      (byte) 207,
      (byte) 231,
      (byte) 10,
      (byte) 163,
      (byte) 36,
      (byte) 165,
      (byte) 63 /*0x3F*/,
      (byte) 95,
      (byte) 73,
      (byte) 149,
      (byte) 94
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_14263(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 192 /*0xC0*/,
      (byte) 146,
      (byte) 143,
      (byte) 51,
      (byte) 70,
      (byte) 56,
      (byte) 45,
      (byte) 102,
      (byte) 157,
      (byte) 64 /*0x40*/,
      (byte) 230,
      (byte) 112 /*0x70*/,
      (byte) 204,
      (byte) 231,
      (byte) 47,
      (byte) 190,
      (byte) 188,
      (byte) 140,
      (byte) 233,
      (byte) 217,
      (byte) 229,
      (byte) 218,
      (byte) 30,
      (byte) 65,
      (byte) 236,
      (byte) 2,
      (byte) 215,
      (byte) 128 /*0x80*/,
      (byte) 6,
      (byte) 202,
      (byte) 117,
      (byte) 95,
      (byte) 145,
      (byte) 16 /*0x10*/,
      (byte) 249,
      (byte) 191,
      (byte) 89,
      (byte) 56,
      (byte) 134,
      (byte) 30,
      (byte) 234,
      (byte) 94,
      (byte) 149,
      (byte) 191,
      (byte) 195,
      (byte) 46,
      (byte) 45,
      (byte) 78
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 223,
      (byte) 172,
      (byte) 52,
      (byte) 193,
      (byte) 8,
      (byte) 6,
      (byte) 149,
      (byte) 155,
      (byte) 137,
      (byte) 235,
      (byte) 126,
      (byte) 65,
      (byte) 195,
      (byte) 89,
      (byte) 37,
      (byte) 74,
      (byte) 178,
      (byte) 79,
      (byte) 85,
      (byte) 208 /*0xD0*/,
      (byte) 129,
      (byte) 77,
      (byte) 241,
      (byte) 205,
      (byte) 224 /*0xE0*/,
      (byte) 197,
      (byte) 239,
      (byte) 55,
      (byte) 26,
      (byte) 114,
      (byte) 202,
      (byte) 72,
      (byte) 206,
      (byte) 74,
      (byte) 49,
      (byte) 1,
      (byte) 80 /*0x50*/,
      (byte) 165,
      (byte) 90,
      (byte) 146,
      (byte) 232,
      (byte) 247,
      (byte) 49,
      (byte) 0,
      (byte) 249,
      (byte) 61,
      (byte) 241,
      (byte) 111
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_14264(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[2] = (byte) 230;
    sourceArray1[42] = (byte) 61;
    sourceArray1[45] = (byte) 244;
    sourceArray1[12] = (byte) 134;
    sourceArray1[1] = (byte) 116;
    sourceArray1[3] = (byte) 116;
    sourceArray1[6] = (byte) 67;
    sourceArray1[0] = (byte) 181;
    sourceArray1[23] = (byte) 68;
    sourceArray1[28] = (byte) 137;
    sourceArray1[7] = (byte) 96 /*0x60*/;
    sourceArray1[5] = (byte) 70;
    sourceArray1[16 /*0x10*/] = (byte) 112 /*0x70*/;
    sourceArray1[13] = (byte) 222;
    sourceArray1[14] = (byte) 188;
    sourceArray1[15] = (byte) 238;
    sourceArray1[9] = (byte) 96 /*0x60*/;
    sourceArray1[17] = (byte) 100;
    sourceArray1[18] = (byte) 132;
    sourceArray1[24] = (byte) 94;
    sourceArray1[8] = (byte) 172;
    sourceArray1[46] = (byte) 60;
    sourceArray1[4] = (byte) 78;
    sourceArray1[20] = (byte) 203;
    sourceArray1[38] = (byte) 11;
    sourceArray1[44] = (byte) 194;
    sourceArray1[26] = (byte) 190;
    sourceArray1[27] = (byte) 213;
    sourceArray1[11] = (byte) 203;
    sourceArray1[29] = (byte) 224 /*0xE0*/;
    sourceArray1[30] = (byte) 113;
    sourceArray1[47] = (byte) 79;
    sourceArray1[32 /*0x20*/] = (byte) 156;
    sourceArray1[33] = (byte) 81;
    sourceArray1[34] = (byte) 220;
    sourceArray1[35] = (byte) 156;
    sourceArray1[25] = (byte) 125;
    sourceArray1[21] = (byte) 239;
    sourceArray1[10] = (byte) 210;
    sourceArray1[39] = (byte) 241;
    sourceArray1[40] = (byte) 66;
    sourceArray1[41] = (byte) 189;
    sourceArray1[36] = (byte) 18;
    sourceArray1[43] = (byte) 254;
    sourceArray1[19] = (byte) 5;
    sourceArray1[37] = (byte) 74;
    sourceArray1[31 /*0x1F*/] = (byte) 90;
    sourceArray1[22] = (byte) 134;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[43] = (byte) 6;
    sourceArray2[1] = (byte) 165;
    sourceArray2[6] = (byte) 160 /*0xA0*/;
    sourceArray2[46] = (byte) 71;
    sourceArray2[32 /*0x20*/] = (byte) 5;
    sourceArray2[5] = (byte) 240 /*0xF0*/;
    sourceArray2[4] = (byte) 17;
    sourceArray2[7] = (byte) 34;
    sourceArray2[8] = (byte) 200;
    sourceArray2[16 /*0x10*/] = (byte) 201;
    sourceArray2[47] = (byte) 43;
    sourceArray2[37] = (byte) 54;
    sourceArray2[12] = (byte) 215;
    sourceArray2[35] = (byte) 221;
    sourceArray2[0] = (byte) 194;
    sourceArray2[15] = (byte) 36;
    sourceArray2[31 /*0x1F*/] = (byte) 58;
    sourceArray2[17] = (byte) 234;
    sourceArray2[18] = (byte) 107;
    sourceArray2[19] = (byte) 53;
    sourceArray2[20] = (byte) 176 /*0xB0*/;
    sourceArray2[30] = (byte) 184;
    sourceArray2[13] = (byte) 95;
    sourceArray2[23] = (byte) 128 /*0x80*/;
    sourceArray2[24] = (byte) 56;
    sourceArray2[41] = (byte) 167;
    sourceArray2[42] = (byte) 94;
    sourceArray2[27] = (byte) 6;
    sourceArray2[14] = (byte) 0;
    sourceArray2[29] = (byte) 34;
    sourceArray2[10] = (byte) 219;
    sourceArray2[26] = (byte) 159;
    sourceArray2[3] = (byte) 117;
    sourceArray2[33] = (byte) 24;
    sourceArray2[34] = (byte) 44;
    sourceArray2[45] = (byte) 139;
    sourceArray2[36] = (byte) 136;
    sourceArray2[2] = (byte) 15;
    sourceArray2[21] = (byte) 168;
    sourceArray2[28] = (byte) 21;
    sourceArray2[22] = (byte) 28;
    sourceArray2[11] = (byte) 170;
    sourceArray2[9] = (byte) 234;
    sourceArray2[38] = (byte) 137;
    sourceArray2[44] = (byte) 197;
    sourceArray2[39] = (byte) 109;
    sourceArray2[40] = byte.MaxValue;
    sourceArray2[25] = (byte) 71;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_14265(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 78,
      (byte) 202,
      (byte) 55,
      (byte) 84,
      (byte) 97,
      (byte) 64 /*0x40*/,
      (byte) 97,
      (byte) 225,
      (byte) 98,
      (byte) 221,
      (byte) 27,
      (byte) 26,
      (byte) 184,
      (byte) 47,
      (byte) 33,
      (byte) 12,
      (byte) 212,
      (byte) 238,
      (byte) 232,
      (byte) 8,
      (byte) 85,
      (byte) 249,
      (byte) 245,
      (byte) 9,
      (byte) 118,
      (byte) 67,
      (byte) 245,
      (byte) 171,
      (byte) 76,
      (byte) 234,
      (byte) 42,
      (byte) 24,
      (byte) 103,
      (byte) 114,
      (byte) 218,
      (byte) 35,
      (byte) 240 /*0xF0*/,
      (byte) 236,
      (byte) 21,
      (byte) 246,
      (byte) 65,
      (byte) 88,
      (byte) 189,
      (byte) 54,
      (byte) 137,
      (byte) 139,
      (byte) 178,
      (byte) 229
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[22] = (byte) 35;
    sourceArray2[45] = (byte) 169;
    sourceArray2[2] = (byte) 225;
    sourceArray2[15] = (byte) 100;
    sourceArray2[3] = (byte) 236;
    sourceArray2[47] = (byte) 208 /*0xD0*/;
    sourceArray2[10] = (byte) 104;
    sourceArray2[17] = (byte) 167;
    sourceArray2[8] = (byte) 208 /*0xD0*/;
    sourceArray2[9] = (byte) 126;
    sourceArray2[7] = (byte) 60;
    sourceArray2[39] = (byte) 120;
    sourceArray2[14] = (byte) 251;
    sourceArray2[33] = (byte) 185;
    sourceArray2[34] = (byte) 216;
    sourceArray2[6] = (byte) 118;
    sourceArray2[11] = (byte) 26;
    sourceArray2[42] = (byte) 191;
    sourceArray2[18] = (byte) 127 /*0x7F*/;
    sourceArray2[27] = (byte) 155;
    sourceArray2[20] = (byte) 127 /*0x7F*/;
    sourceArray2[21] = (byte) 209;
    sourceArray2[37] = (byte) 51;
    sourceArray2[23] = (byte) 169;
    sourceArray2[24] = (byte) 142;
    sourceArray2[25] = (byte) 11;
    sourceArray2[26] = (byte) 66;
    sourceArray2[28] = (byte) 103;
    sourceArray2[19] = (byte) 241;
    sourceArray2[29] = (byte) 77;
    sourceArray2[30] = (byte) 162;
    sourceArray2[31 /*0x1F*/] = (byte) 3;
    sourceArray2[0] = (byte) 189;
    sourceArray2[43] = (byte) 74;
    sourceArray2[35] = (byte) 91;
    sourceArray2[5] = (byte) 142;
    sourceArray2[36] = (byte) 88;
    sourceArray2[16 /*0x10*/] = (byte) 16 /*0x10*/;
    sourceArray2[13] = (byte) 201;
    sourceArray2[12] = (byte) 6;
    sourceArray2[38] = (byte) 229;
    sourceArray2[41] = (byte) 189;
    sourceArray2[32 /*0x20*/] = (byte) 233;
    sourceArray2[40] = (byte) 237;
    sourceArray2[1] = (byte) 221;
    sourceArray2[4] = (byte) 167;
    sourceArray2[46] = (byte) 225;
    sourceArray2[44] = (byte) 133;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_14266(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[0] = (byte) 107;
    sourceArray1[1] = (byte) 144 /*0x90*/;
    sourceArray1[3] = (byte) 178;
    sourceArray1[14] = (byte) 59;
    sourceArray1[5] = (byte) 127 /*0x7F*/;
    sourceArray1[18] = (byte) 225;
    sourceArray1[25] = (byte) 216;
    sourceArray1[2] = (byte) 153;
    sourceArray1[8] = (byte) 88;
    sourceArray1[17] = (byte) 186;
    sourceArray1[7] = (byte) 184;
    sourceArray1[11] = (byte) 172;
    sourceArray1[6] = (byte) 119;
    sourceArray1[13] = (byte) 141;
    sourceArray1[24] = (byte) 88;
    sourceArray1[15] = (byte) 161;
    sourceArray1[16 /*0x10*/] = (byte) 21;
    sourceArray1[9] = (byte) 24;
    sourceArray1[12] = (byte) 241;
    sourceArray1[19] = (byte) 84;
    sourceArray1[20] = (byte) 203;
    sourceArray1[21] = (byte) 42;
    sourceArray1[45] = (byte) 21;
    sourceArray1[29] = (byte) 58;
    sourceArray1[43] = (byte) 108;
    sourceArray1[31 /*0x1F*/] = (byte) 59;
    sourceArray1[23] = (byte) 175;
    sourceArray1[27] = (byte) 71;
    sourceArray1[38] = (byte) 121;
    sourceArray1[42] = (byte) 195;
    sourceArray1[30] = (byte) 246;
    sourceArray1[36] = (byte) 122;
    sourceArray1[28] = (byte) 59;
    sourceArray1[4] = (byte) 68;
    sourceArray1[34] = (byte) 37;
    sourceArray1[35] = (byte) 127 /*0x7F*/;
    sourceArray1[33] = (byte) 19;
    sourceArray1[37] = (byte) 204;
    sourceArray1[32 /*0x20*/] = (byte) 250;
    sourceArray1[39] = (byte) 82;
    sourceArray1[40] = (byte) 55;
    sourceArray1[41] = (byte) 46;
    sourceArray1[26] = (byte) 225;
    sourceArray1[10] = (byte) 22;
    sourceArray1[44] = (byte) 55;
    sourceArray1[22] = (byte) 206;
    sourceArray1[46] = (byte) 29;
    sourceArray1[47] = (byte) 105;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[25] = (byte) 147;
    sourceArray2[1] = (byte) 26;
    sourceArray2[2] = (byte) 90;
    sourceArray2[37] = (byte) 78;
    sourceArray2[4] = (byte) 144 /*0x90*/;
    sourceArray2[33] = byte.MaxValue;
    sourceArray2[35] = (byte) 190;
    sourceArray2[30] = (byte) 73;
    sourceArray2[16 /*0x10*/] = (byte) 207;
    sourceArray2[28] = (byte) 248;
    sourceArray2[10] = (byte) 82;
    sourceArray2[11] = (byte) 213;
    sourceArray2[34] = (byte) 179;
    sourceArray2[43] = (byte) 23;
    sourceArray2[14] = (byte) 136;
    sourceArray2[15] = (byte) 234;
    sourceArray2[3] = (byte) 180;
    sourceArray2[17] = (byte) 179;
    sourceArray2[18] = (byte) 127 /*0x7F*/;
    sourceArray2[13] = (byte) 96 /*0x60*/;
    sourceArray2[20] = (byte) 215;
    sourceArray2[21] = (byte) 28;
    sourceArray2[12] = (byte) 239;
    sourceArray2[23] = (byte) 50;
    sourceArray2[24] = (byte) 210;
    sourceArray2[0] = (byte) 24;
    sourceArray2[42] = (byte) 17;
    sourceArray2[41] = (byte) 112 /*0x70*/;
    sourceArray2[27] = (byte) 163;
    sourceArray2[29] = (byte) 165;
    sourceArray2[6] = (byte) 63 /*0x3F*/;
    sourceArray2[31 /*0x1F*/] = (byte) 206;
    sourceArray2[46] = (byte) 94;
    sourceArray2[26] = (byte) 239;
    sourceArray2[5] = (byte) 9;
    sourceArray2[39] = (byte) 213;
    sourceArray2[8] = (byte) 217;
    sourceArray2[7] = (byte) 138;
    sourceArray2[38] = (byte) 12;
    sourceArray2[36] = (byte) 203;
    sourceArray2[40] = (byte) 145;
    sourceArray2[9] = (byte) 44;
    sourceArray2[19] = (byte) 24;
    sourceArray2[32 /*0x20*/] = (byte) 99;
    sourceArray2[44] = (byte) 164;
    sourceArray2[45] = (byte) 203;
    sourceArray2[22] = (byte) 147;
    sourceArray2[47] = (byte) 85;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_14267(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 142,
      (byte) 178,
      (byte) 133,
      (byte) 20,
      (byte) 148,
      (byte) 26,
      (byte) 121,
      (byte) 103,
      (byte) 126,
      (byte) 179,
      (byte) 43,
      (byte) 141,
      (byte) 22,
      (byte) 57,
      (byte) 1,
      (byte) 129,
      (byte) 25,
      (byte) 72,
      (byte) 86,
      (byte) 55,
      (byte) 51,
      (byte) 175,
      (byte) 46,
      (byte) 155,
      (byte) 103,
      (byte) 149,
      (byte) 189,
      (byte) 54,
      (byte) 63 /*0x3F*/,
      (byte) 122,
      (byte) 161,
      (byte) 119,
      (byte) 121,
      (byte) 214,
      (byte) 75,
      (byte) 97,
      (byte) 103,
      (byte) 172,
      (byte) 130,
      (byte) 0,
      (byte) 101,
      (byte) 21,
      (byte) 247,
      (byte) 116,
      (byte) 99,
      (byte) 155,
      (byte) 10,
      (byte) 75
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[9] = (byte) 52;
    sourceArray2[1] = (byte) 149;
    sourceArray2[2] = (byte) 172;
    sourceArray2[20] = (byte) 252;
    sourceArray2[45] = (byte) 124;
    sourceArray2[29] = (byte) 35;
    sourceArray2[32 /*0x20*/] = (byte) 103;
    sourceArray2[4] = (byte) 31 /*0x1F*/;
    sourceArray2[39] = (byte) 119;
    sourceArray2[15] = (byte) 252;
    sourceArray2[16 /*0x10*/] = (byte) 9;
    sourceArray2[10] = (byte) 79;
    sourceArray2[12] = (byte) 231;
    sourceArray2[13] = (byte) 0;
    sourceArray2[27] = (byte) 216;
    sourceArray2[35] = (byte) 230;
    sourceArray2[47] = (byte) 51;
    sourceArray2[5] = (byte) 92;
    sourceArray2[18] = (byte) 27;
    sourceArray2[11] = (byte) 191;
    sourceArray2[17] = (byte) 207;
    sourceArray2[21] = (byte) 3;
    sourceArray2[8] = (byte) 30;
    sourceArray2[25] = (byte) 91;
    sourceArray2[24] = (byte) 174;
    sourceArray2[30] = (byte) 110;
    sourceArray2[26] = (byte) 175;
    sourceArray2[22] = (byte) 38;
    sourceArray2[44] = (byte) 19;
    sourceArray2[33] = (byte) 202;
    sourceArray2[6] = (byte) 155;
    sourceArray2[31 /*0x1F*/] = (byte) 220;
    sourceArray2[28] = (byte) 244;
    sourceArray2[0] = (byte) 33;
    sourceArray2[34] = (byte) 25;
    sourceArray2[38] = (byte) 116;
    sourceArray2[36] = (byte) 129;
    sourceArray2[23] = (byte) 91;
    sourceArray2[3] = (byte) 80 /*0x50*/;
    sourceArray2[42] = (byte) 52;
    sourceArray2[40] = (byte) 41;
    sourceArray2[14] = (byte) 148;
    sourceArray2[19] = (byte) 150;
    sourceArray2[43] = (byte) 198;
    sourceArray2[37] = (byte) 236;
    sourceArray2[7] = (byte) 26;
    sourceArray2[46] = (byte) 5;
    sourceArray2[41] = (byte) 188;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[54];
    byte[] response2 = new byte[54];
    Array.Copy((Array) sc_14238.sspq, 263, (Array) numArray2, 0, 54);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_14238.sspr, 263, (Array) numArray2, 0, 54);
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

  internal static int ssp_appserver_14268(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 54,
      (byte) 222,
      (byte) 81,
      (byte) 94,
      (byte) 19,
      (byte) 12,
      (byte) 192 /*0xC0*/,
      (byte) 162,
      (byte) 217,
      (byte) 97,
      (byte) 113,
      (byte) 42,
      (byte) 155,
      (byte) 181,
      (byte) 216,
      (byte) 77,
      (byte) 211,
      (byte) 113,
      (byte) 69,
      (byte) 202,
      (byte) 187,
      (byte) 242,
      (byte) 57,
      (byte) 173,
      (byte) 211,
      (byte) 80 /*0x50*/,
      (byte) 51,
      (byte) 249,
      (byte) 76,
      (byte) 67,
      (byte) 75,
      (byte) 121,
      (byte) 224 /*0xE0*/,
      (byte) 124,
      (byte) 136,
      (byte) 52,
      (byte) 32 /*0x20*/,
      (byte) 206,
      (byte) 244,
      (byte) 49,
      (byte) 123,
      (byte) 97,
      (byte) 199,
      (byte) 229,
      (byte) 163,
      (byte) 159,
      (byte) 130,
      (byte) 89
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 247,
      (byte) 70,
      (byte) 20,
      (byte) 152,
      (byte) 49,
      (byte) 159,
      (byte) 49,
      (byte) 126,
      (byte) 173,
      (byte) 145,
      (byte) 58,
      (byte) 120,
      (byte) 69,
      (byte) 80 /*0x50*/,
      (byte) 97,
      (byte) 123,
      (byte) 173,
      (byte) 181,
      (byte) 140,
      (byte) 172,
      (byte) 57,
      (byte) 139,
      (byte) 133,
      (byte) 50,
      (byte) 241,
      (byte) 50,
      (byte) 55,
      (byte) 238,
      (byte) 94,
      (byte) 177,
      (byte) 56,
      (byte) 235,
      (byte) 51,
      (byte) 130,
      (byte) 106,
      (byte) 184,
      (byte) 178,
      (byte) 243,
      (byte) 128 /*0x80*/,
      (byte) 229,
      (byte) 224 /*0xE0*/,
      (byte) 213,
      (byte) 182,
      (byte) 176 /*0xB0*/,
      (byte) 230,
      (byte) 5,
      (byte) 43,
      (byte) 221
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_14269(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 221,
      (byte) 13,
      (byte) 67,
      (byte) 164,
      (byte) 164,
      (byte) 138,
      (byte) 195,
      (byte) 217,
      (byte) 198,
      (byte) 85,
      (byte) 150,
      (byte) 21,
      (byte) 248,
      (byte) 74,
      (byte) 30,
      (byte) 44,
      (byte) 163,
      (byte) 44,
      (byte) 4,
      (byte) 124,
      (byte) 19,
      (byte) 79,
      (byte) 90,
      (byte) 43,
      (byte) 84,
      (byte) 9,
      (byte) 52,
      (byte) 205,
      (byte) 218,
      (byte) 25,
      (byte) 53,
      (byte) 41,
      (byte) 67,
      (byte) 224 /*0xE0*/,
      (byte) 2,
      (byte) 111,
      (byte) 69,
      (byte) 143,
      (byte) 124,
      (byte) 67,
      (byte) 48 /*0x30*/,
      (byte) 88,
      (byte) 180,
      (byte) 11,
      (byte) 49,
      (byte) 207,
      (byte) 171,
      (byte) 85
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[44] = (byte) 124;
    sourceArray2[1] = (byte) 222;
    sourceArray2[10] = (byte) 178;
    sourceArray2[47] = (byte) 77;
    sourceArray2[25] = (byte) 242;
    sourceArray2[41] = (byte) 58;
    sourceArray2[20] = (byte) 70;
    sourceArray2[7] = (byte) 177;
    sourceArray2[8] = (byte) 170;
    sourceArray2[0] = (byte) 249;
    sourceArray2[11] = (byte) 99;
    sourceArray2[46] = (byte) 50;
    sourceArray2[13] = (byte) 138;
    sourceArray2[2] = (byte) 193;
    sourceArray2[40] = (byte) 202;
    sourceArray2[15] = (byte) 245;
    sourceArray2[14] = (byte) 97;
    sourceArray2[17] = (byte) 32 /*0x20*/;
    sourceArray2[29] = (byte) 240 /*0xF0*/;
    sourceArray2[19] = (byte) 151;
    sourceArray2[12] = (byte) 71;
    sourceArray2[21] = (byte) 79;
    sourceArray2[22] = (byte) 109;
    sourceArray2[45] = (byte) 186;
    sourceArray2[24] = (byte) 139;
    sourceArray2[3] = (byte) 193;
    sourceArray2[37] = (byte) 71;
    sourceArray2[33] = (byte) 137;
    sourceArray2[28] = (byte) 152;
    sourceArray2[27] = (byte) 67;
    sourceArray2[6] = (byte) 142;
    sourceArray2[31 /*0x1F*/] = (byte) 223;
    sourceArray2[32 /*0x20*/] = (byte) 190;
    sourceArray2[23] = (byte) 113;
    sourceArray2[34] = (byte) 31 /*0x1F*/;
    sourceArray2[35] = (byte) 21;
    sourceArray2[26] = (byte) 195;
    sourceArray2[5] = (byte) 221;
    sourceArray2[38] = (byte) 122;
    sourceArray2[36] = (byte) 8;
    sourceArray2[30] = (byte) 189;
    sourceArray2[16 /*0x10*/] = (byte) 61;
    sourceArray2[42] = (byte) 31 /*0x1F*/;
    sourceArray2[43] = (byte) 67;
    sourceArray2[9] = (byte) 97;
    sourceArray2[39] = (byte) 126;
    sourceArray2[18] = (byte) 103;
    sourceArray2[4] = (byte) 63 /*0x3F*/;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_14270(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 147,
      (byte) 13,
      (byte) 180,
      (byte) 69,
      (byte) 39,
      (byte) 57,
      (byte) 84,
      (byte) 63 /*0x3F*/,
      (byte) 201,
      (byte) 242,
      (byte) 100,
      (byte) 29,
      (byte) 202,
      (byte) 59,
      (byte) 119,
      (byte) 141,
      (byte) 81,
      (byte) 207,
      (byte) 211,
      (byte) 88,
      (byte) 26,
      (byte) 188,
      (byte) 211,
      (byte) 243,
      (byte) 186,
      (byte) 213,
      (byte) 205,
      (byte) 210,
      (byte) 18,
      (byte) 97,
      (byte) 148,
      (byte) 98,
      (byte) 54,
      (byte) 137,
      (byte) 203,
      (byte) 209,
      (byte) 180,
      (byte) 104,
      (byte) 181,
      (byte) 18,
      (byte) 190,
      (byte) 120,
      (byte) 145,
      (byte) 130,
      (byte) 87,
      (byte) 15,
      (byte) 116,
      (byte) 166
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[13] = (byte) 134;
    sourceArray2[3] = (byte) 245;
    sourceArray2[2] = (byte) 102;
    sourceArray2[34] = (byte) 181;
    sourceArray2[4] = (byte) 72;
    sourceArray2[5] = (byte) 160 /*0xA0*/;
    sourceArray2[6] = (byte) 72;
    sourceArray2[7] = (byte) 151;
    sourceArray2[31 /*0x1F*/] = (byte) 224 /*0xE0*/;
    sourceArray2[9] = (byte) 158;
    sourceArray2[10] = (byte) 240 /*0xF0*/;
    sourceArray2[11] = (byte) 83;
    sourceArray2[43] = (byte) 83;
    sourceArray2[30] = (byte) 35;
    sourceArray2[23] = (byte) 115;
    sourceArray2[15] = (byte) 235;
    sourceArray2[16 /*0x10*/] = (byte) 74;
    sourceArray2[17] = (byte) 233;
    sourceArray2[33] = (byte) 86;
    sourceArray2[19] = (byte) 224 /*0xE0*/;
    sourceArray2[20] = (byte) 227;
    sourceArray2[44] = (byte) 73;
    sourceArray2[22] = (byte) 75;
    sourceArray2[37] = (byte) 56;
    sourceArray2[24] = (byte) 138;
    sourceArray2[32 /*0x20*/] = (byte) 143;
    sourceArray2[26] = (byte) 42;
    sourceArray2[27] = (byte) 160 /*0xA0*/;
    sourceArray2[18] = (byte) 175;
    sourceArray2[39] = (byte) 80 /*0x50*/;
    sourceArray2[45] = (byte) 22;
    sourceArray2[12] = (byte) 65;
    sourceArray2[29] = (byte) 32 /*0x20*/;
    sourceArray2[40] = (byte) 223;
    sourceArray2[36] = (byte) 68;
    sourceArray2[35] = (byte) 124;
    sourceArray2[14] = (byte) 205;
    sourceArray2[28] = (byte) 225;
    sourceArray2[1] = (byte) 174;
    sourceArray2[46] = (byte) 172;
    sourceArray2[21] = (byte) 55;
    sourceArray2[41] = (byte) 167;
    sourceArray2[25] = (byte) 120;
    sourceArray2[47] = (byte) 220;
    sourceArray2[38] = (byte) 49;
    sourceArray2[0] = (byte) 137;
    sourceArray2[8] = (byte) 186;
    sourceArray2[42] = (byte) 187;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_14271(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 50,
      (byte) 54,
      (byte) 208 /*0xD0*/,
      (byte) 211,
      (byte) 30,
      (byte) 29,
      (byte) 86,
      (byte) 209,
      (byte) 64 /*0x40*/,
      (byte) 172,
      (byte) 234,
      (byte) 37,
      (byte) 223,
      (byte) 242,
      (byte) 68,
      (byte) 37,
      (byte) 199,
      (byte) 13,
      (byte) 0,
      (byte) 182,
      (byte) 28,
      (byte) 154,
      (byte) 123,
      (byte) 15,
      (byte) 229,
      (byte) 11,
      (byte) 183,
      (byte) 27,
      (byte) 92,
      (byte) 85,
      (byte) 0,
      (byte) 43,
      (byte) 70,
      (byte) 130,
      (byte) 45,
      (byte) 240 /*0xF0*/,
      (byte) 140,
      (byte) 145,
      (byte) 59,
      (byte) 63 /*0x3F*/,
      (byte) 244,
      (byte) 248,
      (byte) 103,
      (byte) 72,
      (byte) 172,
      (byte) 205,
      (byte) 254,
      (byte) 112 /*0x70*/
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 11,
      (byte) 193,
      (byte) 173,
      (byte) 176 /*0xB0*/,
      (byte) 203,
      (byte) 76,
      (byte) 118,
      (byte) 2,
      (byte) 18,
      (byte) 4,
      (byte) 36,
      (byte) 191,
      (byte) 173,
      (byte) 28,
      (byte) 193,
      (byte) 178,
      (byte) 199,
      (byte) 68,
      (byte) 193,
      (byte) 89,
      (byte) 246,
      (byte) 55,
      (byte) 221,
      (byte) 42,
      (byte) 48 /*0x30*/,
      (byte) 239,
      (byte) 148,
      (byte) 210,
      (byte) 133,
      (byte) 94,
      (byte) 120,
      (byte) 43,
      (byte) 192 /*0xC0*/,
      (byte) 10,
      (byte) 45,
      (byte) 99,
      (byte) 7,
      (byte) 113,
      (byte) 231,
      (byte) 73,
      (byte) 13,
      (byte) 201,
      (byte) 239,
      (byte) 28,
      (byte) 35,
      (byte) 220,
      (byte) 216,
      (byte) 44
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_14272(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 158,
      (byte) 12,
      (byte) 152,
      (byte) 187,
      (byte) 192 /*0xC0*/,
      (byte) 237,
      (byte) 19,
      (byte) 102,
      (byte) 59,
      (byte) 117,
      (byte) 234,
      (byte) 172,
      (byte) 190,
      (byte) 223,
      (byte) 93,
      (byte) 196,
      (byte) 84,
      (byte) 153,
      (byte) 53,
      (byte) 210,
      (byte) 193,
      (byte) 194,
      (byte) 35,
      (byte) 51,
      (byte) 50,
      (byte) 99,
      (byte) 24,
      (byte) 12,
      (byte) 70,
      (byte) 185,
      (byte) 69,
      (byte) 208 /*0xD0*/,
      (byte) 227,
      (byte) 7,
      (byte) 167,
      (byte) 117,
      (byte) 149,
      (byte) 52,
      (byte) 203,
      (byte) 163,
      (byte) 39,
      (byte) 217,
      (byte) 185,
      (byte) 131,
      (byte) 124,
      (byte) 156,
      (byte) 238,
      (byte) 237
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[15] = (byte) 247;
    sourceArray2[25] = (byte) 140;
    sourceArray2[28] = (byte) 43;
    sourceArray2[31 /*0x1F*/] = (byte) 138;
    sourceArray2[40] = (byte) 127 /*0x7F*/;
    sourceArray2[21] = (byte) 29;
    sourceArray2[12] = (byte) 164;
    sourceArray2[7] = (byte) 33;
    sourceArray2[8] = (byte) 13;
    sourceArray2[1] = (byte) 209;
    sourceArray2[33] = (byte) 186;
    sourceArray2[11] = (byte) 135;
    sourceArray2[27] = (byte) 77;
    sourceArray2[13] = (byte) 114;
    sourceArray2[14] = (byte) 64 /*0x40*/;
    sourceArray2[9] = (byte) 16 /*0x10*/;
    sourceArray2[16 /*0x10*/] = (byte) 122;
    sourceArray2[39] = (byte) 233;
    sourceArray2[18] = (byte) 93;
    sourceArray2[19] = (byte) 137;
    sourceArray2[22] = (byte) 12;
    sourceArray2[0] = (byte) 77;
    sourceArray2[24] = (byte) 14;
    sourceArray2[23] = (byte) 153;
    sourceArray2[4] = (byte) 63 /*0x3F*/;
    sourceArray2[17] = (byte) 248;
    sourceArray2[26] = (byte) 103;
    sourceArray2[46] = (byte) 64 /*0x40*/;
    sourceArray2[38] = (byte) 104;
    sourceArray2[29] = (byte) 17;
    sourceArray2[30] = (byte) 33;
    sourceArray2[37] = (byte) 210;
    sourceArray2[32 /*0x20*/] = (byte) 99;
    sourceArray2[2] = (byte) 227;
    sourceArray2[34] = (byte) 118;
    sourceArray2[35] = (byte) 140;
    sourceArray2[36] = (byte) 73;
    sourceArray2[20] = (byte) 215;
    sourceArray2[6] = (byte) 67;
    sourceArray2[45] = (byte) 61;
    sourceArray2[5] = (byte) 160 /*0xA0*/;
    sourceArray2[41] = (byte) 124;
    sourceArray2[42] = (byte) 100;
    sourceArray2[43] = (byte) 21;
    sourceArray2[44] = (byte) 220;
    sourceArray2[3] = (byte) 82;
    sourceArray2[10] = (byte) 168;
    sourceArray2[47] = (byte) 75;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_14273(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 56,
      (byte) 181,
      (byte) 185,
      (byte) 125,
      (byte) 237,
      (byte) 177,
      (byte) 189,
      (byte) 158,
      (byte) 122,
      (byte) 211,
      (byte) 72,
      (byte) 175,
      (byte) 241,
      (byte) 232,
      (byte) 171,
      (byte) 155,
      (byte) 134,
      (byte) 212,
      (byte) 104,
      (byte) 151,
      (byte) 46,
      (byte) 17,
      (byte) 110,
      (byte) 90,
      byte.MaxValue,
      (byte) 115,
      (byte) 94,
      (byte) 195,
      (byte) 138,
      (byte) 46,
      (byte) 208 /*0xD0*/,
      (byte) 76,
      (byte) 82,
      (byte) 195,
      (byte) 60,
      (byte) 117,
      (byte) 177,
      (byte) 27,
      (byte) 190,
      (byte) 21,
      (byte) 196,
      (byte) 251,
      (byte) 42,
      (byte) 246,
      (byte) 69,
      (byte) 128 /*0x80*/,
      (byte) 186,
      (byte) 134
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[38] = (byte) 27;
    sourceArray2[41] = (byte) 190;
    sourceArray2[16 /*0x10*/] = (byte) 43;
    sourceArray2[26] = (byte) 117;
    sourceArray2[18] = (byte) 97;
    sourceArray2[5] = (byte) 11;
    sourceArray2[39] = (byte) 71;
    sourceArray2[23] = (byte) 114;
    sourceArray2[8] = (byte) 82;
    sourceArray2[9] = (byte) 83;
    sourceArray2[14] = (byte) 150;
    sourceArray2[37] = (byte) 219;
    sourceArray2[12] = (byte) 239;
    sourceArray2[30] = (byte) 33;
    sourceArray2[45] = (byte) 76;
    sourceArray2[15] = (byte) 213;
    sourceArray2[44] = (byte) 15;
    sourceArray2[47] = (byte) 111;
    sourceArray2[35] = (byte) 223;
    sourceArray2[19] = (byte) 219;
    sourceArray2[20] = (byte) 214;
    sourceArray2[21] = (byte) 102;
    sourceArray2[22] = (byte) 117;
    sourceArray2[4] = (byte) 126;
    sourceArray2[24] = (byte) 65;
    sourceArray2[25] = (byte) 193;
    sourceArray2[32 /*0x20*/] = (byte) 90;
    sourceArray2[13] = (byte) 121;
    sourceArray2[28] = (byte) 138;
    sourceArray2[43] = (byte) 53;
    sourceArray2[3] = (byte) 158;
    sourceArray2[31 /*0x1F*/] = (byte) 204;
    sourceArray2[2] = (byte) 64 /*0x40*/;
    sourceArray2[33] = (byte) 198;
    sourceArray2[42] = (byte) 105;
    sourceArray2[1] = (byte) 216;
    sourceArray2[36] = (byte) 170;
    sourceArray2[27] = (byte) 80 /*0x50*/;
    sourceArray2[40] = (byte) 132;
    sourceArray2[11] = (byte) 40;
    sourceArray2[17] = (byte) 90;
    sourceArray2[6] = (byte) 76;
    sourceArray2[7] = (byte) 195;
    sourceArray2[29] = (byte) 118;
    sourceArray2[34] = (byte) 208 /*0xD0*/;
    sourceArray2[10] = (byte) 201;
    sourceArray2[46] = (byte) 28;
    sourceArray2[0] = (byte) 118;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_14274(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 139,
      (byte) 219,
      (byte) 237,
      (byte) 154,
      (byte) 199,
      (byte) 77,
      (byte) 152,
      (byte) 248,
      (byte) 112 /*0x70*/,
      (byte) 64 /*0x40*/,
      (byte) 207,
      (byte) 74,
      byte.MaxValue,
      (byte) 192 /*0xC0*/,
      (byte) 122,
      (byte) 90,
      (byte) 14,
      (byte) 79,
      (byte) 150,
      (byte) 153,
      (byte) 213,
      (byte) 15,
      (byte) 149,
      (byte) 7,
      (byte) 152,
      (byte) 205,
      (byte) 208 /*0xD0*/,
      (byte) 143,
      (byte) 9,
      (byte) 77,
      (byte) 8,
      (byte) 132,
      (byte) 202,
      (byte) 90,
      (byte) 45,
      (byte) 252,
      (byte) 178,
      (byte) 107,
      (byte) 53,
      (byte) 31 /*0x1F*/,
      (byte) 201,
      (byte) 142,
      (byte) 174,
      (byte) 213,
      (byte) 94,
      (byte) 6,
      (byte) 157,
      (byte) 132
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 179,
      (byte) 170,
      (byte) 49,
      (byte) 49,
      (byte) 19,
      (byte) 159,
      (byte) 30,
      (byte) 34,
      (byte) 80 /*0x50*/,
      (byte) 73,
      (byte) 171,
      (byte) 235,
      (byte) 215,
      (byte) 180,
      (byte) 99,
      (byte) 58,
      (byte) 100,
      (byte) 27,
      (byte) 149,
      (byte) 167,
      (byte) 55,
      (byte) 113,
      (byte) 157,
      (byte) 208 /*0xD0*/,
      (byte) 208 /*0xD0*/,
      (byte) 136,
      (byte) 33,
      (byte) 71,
      (byte) 242,
      (byte) 103,
      (byte) 184,
      (byte) 120,
      (byte) 19,
      (byte) 132,
      (byte) 160 /*0xA0*/,
      (byte) 152,
      (byte) 190,
      (byte) 222,
      (byte) 59,
      (byte) 99,
      (byte) 138,
      (byte) 32 /*0x20*/,
      (byte) 231,
      (byte) 58,
      (byte) 233,
      (byte) 229,
      (byte) 180,
      (byte) 193
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_14275(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 176 /*0xB0*/,
      (byte) 77,
      (byte) 124,
      (byte) 70,
      (byte) 229,
      (byte) 1,
      (byte) 249,
      (byte) 125,
      (byte) 8,
      (byte) 238,
      (byte) 35,
      (byte) 55,
      (byte) 74,
      (byte) 127 /*0x7F*/,
      (byte) 164,
      (byte) 146,
      (byte) 126,
      (byte) 167,
      (byte) 163,
      (byte) 25,
      (byte) 119,
      (byte) 207,
      (byte) 71,
      (byte) 96 /*0x60*/,
      (byte) 191,
      (byte) 169,
      (byte) 145,
      (byte) 48 /*0x30*/,
      (byte) 76,
      (byte) 70,
      (byte) 154,
      (byte) 116,
      (byte) 121,
      (byte) 39,
      (byte) 26,
      (byte) 13,
      (byte) 177,
      (byte) 182,
      (byte) 205,
      (byte) 105,
      (byte) 60,
      (byte) 162,
      (byte) 192 /*0xC0*/,
      (byte) 160 /*0xA0*/,
      (byte) 139,
      (byte) 157,
      (byte) 20,
      (byte) 134
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 151,
      (byte) 23,
      (byte) 123,
      (byte) 125,
      (byte) 104,
      (byte) 119,
      (byte) 13,
      (byte) 164,
      (byte) 44,
      (byte) 253,
      (byte) 179,
      (byte) 37,
      (byte) 163,
      (byte) 157,
      (byte) 90,
      (byte) 101,
      (byte) 207,
      (byte) 45,
      (byte) 83,
      (byte) 11,
      (byte) 123,
      (byte) 89,
      (byte) 14,
      (byte) 61,
      (byte) 217,
      (byte) 198,
      (byte) 183,
      (byte) 254,
      (byte) 168,
      (byte) 215,
      (byte) 229,
      (byte) 61,
      (byte) 116,
      (byte) 231,
      (byte) 209,
      (byte) 45,
      (byte) 189,
      (byte) 35,
      (byte) 185,
      (byte) 34,
      (byte) 37,
      (byte) 205,
      (byte) 147,
      (byte) 197,
      (byte) 174,
      (byte) 214,
      (byte) 184,
      (byte) 217
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[48 /*0x30*/];
    byte[] response2 = new byte[48 /*0x30*/];
    Array.Copy((Array) sc_14238.sspq, 317, (Array) numArray2, 0, 48 /*0x30*/);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_14238.sspr, 317, (Array) numArray2, 0, 48 /*0x30*/);
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

  internal static int ssp_appserver_14276(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[27] = (byte) 106;
    sourceArray1[8] = (byte) 210;
    sourceArray1[2] = (byte) 16 /*0x10*/;
    sourceArray1[47] = (byte) 147;
    sourceArray1[18] = (byte) 111;
    sourceArray1[5] = (byte) 251;
    sourceArray1[7] = (byte) 16 /*0x10*/;
    sourceArray1[41] = (byte) 49;
    sourceArray1[34] = (byte) 233;
    sourceArray1[9] = (byte) 95;
    sourceArray1[10] = (byte) 70;
    sourceArray1[11] = (byte) 100;
    sourceArray1[44] = (byte) 79;
    sourceArray1[13] = (byte) 101;
    sourceArray1[14] = (byte) 144 /*0x90*/;
    sourceArray1[3] = (byte) 1;
    sourceArray1[16 /*0x10*/] = (byte) 68;
    sourceArray1[17] = (byte) 129;
    sourceArray1[6] = (byte) 151;
    sourceArray1[19] = (byte) 186;
    sourceArray1[36] = (byte) 74;
    sourceArray1[21] = (byte) 45;
    sourceArray1[12] = (byte) 167;
    sourceArray1[25] = (byte) 73;
    sourceArray1[24] = (byte) 152;
    sourceArray1[22] = (byte) 145;
    sourceArray1[15] = (byte) 213;
    sourceArray1[1] = (byte) 96 /*0x60*/;
    sourceArray1[28] = (byte) 99;
    sourceArray1[29] = (byte) 15;
    sourceArray1[38] = (byte) 229;
    sourceArray1[26] = (byte) 64 /*0x40*/;
    sourceArray1[43] = (byte) 167;
    sourceArray1[33] = (byte) 163;
    sourceArray1[40] = (byte) 108;
    sourceArray1[0] = (byte) 33;
    sourceArray1[35] = (byte) 215;
    sourceArray1[32 /*0x20*/] = (byte) 66;
    sourceArray1[4] = (byte) 237;
    sourceArray1[39] = (byte) 67;
    sourceArray1[45] = (byte) 203;
    sourceArray1[37] = (byte) 123;
    sourceArray1[42] = (byte) 102;
    sourceArray1[30] = (byte) 90;
    sourceArray1[31 /*0x1F*/] = (byte) 248;
    sourceArray1[23] = (byte) 75;
    sourceArray1[46] = (byte) 233;
    sourceArray1[20] = (byte) 224 /*0xE0*/;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[32 /*0x20*/] = (byte) 181;
    sourceArray2[23] = (byte) 99;
    sourceArray2[2] = (byte) 14;
    sourceArray2[3] = (byte) 242;
    sourceArray2[26] = (byte) 187;
    sourceArray2[29] = (byte) 153;
    sourceArray2[9] = (byte) 144 /*0x90*/;
    sourceArray2[7] = (byte) 72;
    sourceArray2[8] = (byte) 182;
    sourceArray2[1] = (byte) 178;
    sourceArray2[10] = (byte) 110;
    sourceArray2[11] = (byte) 37;
    sourceArray2[12] = (byte) 139;
    sourceArray2[13] = (byte) 140;
    sourceArray2[14] = (byte) 184;
    sourceArray2[46] = (byte) 37;
    sourceArray2[45] = (byte) 212;
    sourceArray2[18] = (byte) 55;
    sourceArray2[37] = (byte) 56;
    sourceArray2[19] = (byte) 150;
    sourceArray2[20] = (byte) 54;
    sourceArray2[17] = (byte) 190;
    sourceArray2[22] = (byte) 221;
    sourceArray2[16 /*0x10*/] = (byte) 187;
    sourceArray2[15] = (byte) 29;
    sourceArray2[25] = (byte) 4;
    sourceArray2[0] = (byte) 236;
    sourceArray2[27] = (byte) 233;
    sourceArray2[31 /*0x1F*/] = (byte) 162;
    sourceArray2[5] = (byte) 46;
    sourceArray2[4] = (byte) 165;
    sourceArray2[41] = (byte) 236;
    sourceArray2[30] = (byte) 91;
    sourceArray2[38] = (byte) 124;
    sourceArray2[34] = (byte) 107;
    sourceArray2[40] = (byte) 37;
    sourceArray2[36] = (byte) 174;
    sourceArray2[21] = (byte) 82;
    sourceArray2[35] = (byte) 112 /*0x70*/;
    sourceArray2[39] = (byte) 19;
    sourceArray2[33] = (byte) 152;
    sourceArray2[24] = (byte) 158;
    sourceArray2[28] = (byte) 159;
    sourceArray2[43] = (byte) 211;
    sourceArray2[44] = (byte) 0;
    sourceArray2[6] = (byte) 93;
    sourceArray2[42] = (byte) 132;
    sourceArray2[47] = (byte) 204;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[30];
    byte[] response2 = new byte[30];
    Array.Copy((Array) sc_14238.sspq, 365, (Array) numArray2, 0, 30);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_14238.sspr, 365, (Array) numArray2, 0, 30);
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

  internal static int ssp_appserver_14277(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 69,
      (byte) 62,
      (byte) 92,
      (byte) 44,
      (byte) 113,
      (byte) 138,
      (byte) 236,
      (byte) 24,
      (byte) 187,
      (byte) 127 /*0x7F*/,
      (byte) 5,
      (byte) 2,
      (byte) 231,
      (byte) 170,
      (byte) 41,
      (byte) 145,
      (byte) 173,
      (byte) 121,
      (byte) 249,
      (byte) 189,
      (byte) 131,
      (byte) 242,
      byte.MaxValue,
      (byte) 54,
      (byte) 251,
      (byte) 85,
      (byte) 114,
      (byte) 46,
      (byte) 30,
      (byte) 200,
      (byte) 228,
      (byte) 54,
      (byte) 210,
      (byte) 231,
      (byte) 198,
      (byte) 251,
      (byte) 117,
      (byte) 111,
      (byte) 89,
      (byte) 173,
      (byte) 29,
      (byte) 134,
      (byte) 17,
      (byte) 248,
      (byte) 195,
      (byte) 121,
      (byte) 95,
      (byte) 131
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[46] = (byte) 113;
    sourceArray2[7] = (byte) 216;
    sourceArray2[42] = (byte) 22;
    sourceArray2[37] = (byte) 237;
    sourceArray2[14] = (byte) 142;
    sourceArray2[5] = (byte) 166;
    sourceArray2[6] = (byte) 122;
    sourceArray2[32 /*0x20*/] = (byte) 152;
    sourceArray2[39] = (byte) 230;
    sourceArray2[9] = (byte) 49;
    sourceArray2[40] = (byte) 217;
    sourceArray2[11] = (byte) 155;
    sourceArray2[8] = (byte) 56;
    sourceArray2[44] = (byte) 204;
    sourceArray2[0] = (byte) 69;
    sourceArray2[15] = (byte) 36;
    sourceArray2[16 /*0x10*/] = (byte) 95;
    sourceArray2[17] = (byte) 239;
    sourceArray2[30] = (byte) 91;
    sourceArray2[19] = (byte) 0;
    sourceArray2[23] = (byte) 56;
    sourceArray2[21] = (byte) 254;
    sourceArray2[38] = (byte) 236;
    sourceArray2[3] = (byte) 114;
    sourceArray2[4] = (byte) 146;
    sourceArray2[25] = (byte) 41;
    sourceArray2[26] = (byte) 84;
    sourceArray2[12] = (byte) 183;
    sourceArray2[28] = (byte) 69;
    sourceArray2[45] = (byte) 73;
    sourceArray2[36] = (byte) 44;
    sourceArray2[31 /*0x1F*/] = (byte) 252;
    sourceArray2[13] = (byte) 149;
    sourceArray2[24] = (byte) 81;
    sourceArray2[34] = (byte) 34;
    sourceArray2[35] = (byte) 112 /*0x70*/;
    sourceArray2[27] = (byte) 12;
    sourceArray2[18] = (byte) 101;
    sourceArray2[22] = (byte) 32 /*0x20*/;
    sourceArray2[29] = (byte) 3;
    sourceArray2[33] = (byte) 108;
    sourceArray2[41] = (byte) 0;
    sourceArray2[1] = (byte) 224 /*0xE0*/;
    sourceArray2[2] = (byte) 167;
    sourceArray2[43] = (byte) 19;
    sourceArray2[10] = (byte) 230;
    sourceArray2[20] = (byte) 46;
    sourceArray2[47] = (byte) 104;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_14278(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[9] = (byte) 114;
    sourceArray1[1] = (byte) 159;
    sourceArray1[30] = (byte) 194;
    sourceArray1[3] = (byte) 99;
    sourceArray1[22] = (byte) 111;
    sourceArray1[5] = (byte) 37;
    sourceArray1[2] = (byte) 176 /*0xB0*/;
    sourceArray1[7] = (byte) 97;
    sourceArray1[8] = (byte) 15;
    sourceArray1[12] = (byte) 174;
    sourceArray1[10] = (byte) 54;
    sourceArray1[11] = (byte) 179;
    sourceArray1[24] = (byte) 204;
    sourceArray1[37] = (byte) 60;
    sourceArray1[42] = (byte) 202;
    sourceArray1[15] = (byte) 110;
    sourceArray1[16 /*0x10*/] = (byte) 90;
    sourceArray1[17] = (byte) 243;
    sourceArray1[18] = (byte) 251;
    sourceArray1[41] = (byte) 112 /*0x70*/;
    sourceArray1[20] = (byte) 93;
    sourceArray1[21] = (byte) 37;
    sourceArray1[44] = (byte) 134;
    sourceArray1[19] = (byte) 221;
    sourceArray1[23] = (byte) 112 /*0x70*/;
    sourceArray1[39] = (byte) 120;
    sourceArray1[26] = (byte) 225;
    sourceArray1[27] = (byte) 183;
    sourceArray1[38] = (byte) 116;
    sourceArray1[29] = (byte) 112 /*0x70*/;
    sourceArray1[40] = (byte) 248;
    sourceArray1[0] = (byte) 95;
    sourceArray1[32 /*0x20*/] = (byte) 126;
    sourceArray1[33] = (byte) 57;
    sourceArray1[28] = (byte) 64 /*0x40*/;
    sourceArray1[35] = (byte) 79;
    sourceArray1[13] = (byte) 37;
    sourceArray1[4] = (byte) 139;
    sourceArray1[47] = (byte) 24;
    sourceArray1[34] = (byte) 150;
    sourceArray1[25] = byte.MaxValue;
    sourceArray1[31 /*0x1F*/] = (byte) 170;
    sourceArray1[14] = (byte) 40;
    sourceArray1[43] = (byte) 1;
    sourceArray1[36] = (byte) 159;
    sourceArray1[45] = (byte) 155;
    sourceArray1[46] = (byte) 144 /*0x90*/;
    sourceArray1[6] = (byte) 248;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[22] = (byte) 57;
    sourceArray2[2] = (byte) 74;
    sourceArray2[43] = (byte) 26;
    sourceArray2[20] = (byte) 120;
    sourceArray2[11] = (byte) 242;
    sourceArray2[25] = (byte) 215;
    sourceArray2[46] = (byte) 248;
    sourceArray2[0] = (byte) 105;
    sourceArray2[10] = (byte) 34;
    sourceArray2[9] = (byte) 204;
    sourceArray2[4] = (byte) 27;
    sourceArray2[42] = (byte) 121;
    sourceArray2[26] = (byte) 21;
    sourceArray2[45] = (byte) 214;
    sourceArray2[36] = (byte) 89;
    sourceArray2[15] = (byte) 89;
    sourceArray2[16 /*0x10*/] = (byte) 156;
    sourceArray2[6] = (byte) 161;
    sourceArray2[18] = (byte) 27;
    sourceArray2[19] = (byte) 14;
    sourceArray2[1] = (byte) 119;
    sourceArray2[21] = (byte) 227;
    sourceArray2[32 /*0x20*/] = (byte) 48 /*0x30*/;
    sourceArray2[23] = (byte) 78;
    sourceArray2[40] = (byte) 134;
    sourceArray2[30] = (byte) 99;
    sourceArray2[34] = (byte) 85;
    sourceArray2[27] = (byte) 231;
    sourceArray2[28] = (byte) 72;
    sourceArray2[29] = (byte) 180;
    sourceArray2[24] = (byte) 223;
    sourceArray2[31 /*0x1F*/] = (byte) 56;
    sourceArray2[13] = (byte) 252;
    sourceArray2[33] = (byte) 161;
    sourceArray2[44] = (byte) 10;
    sourceArray2[35] = (byte) 249;
    sourceArray2[17] = (byte) 226;
    sourceArray2[47] = (byte) 11;
    sourceArray2[3] = (byte) 28;
    sourceArray2[39] = (byte) 112 /*0x70*/;
    sourceArray2[7] = (byte) 11;
    sourceArray2[14] = (byte) 203;
    sourceArray2[8] = (byte) 74;
    sourceArray2[38] = (byte) 98;
    sourceArray2[12] = (byte) 3;
    sourceArray2[5] = (byte) 246;
    sourceArray2[37] = (byte) 144 /*0x90*/;
    sourceArray2[41] = (byte) 139;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_14279(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 186,
      (byte) 192 /*0xC0*/,
      (byte) 126,
      (byte) 203,
      (byte) 58,
      (byte) 183,
      (byte) 150,
      (byte) 244,
      (byte) 88,
      (byte) 219,
      (byte) 176 /*0xB0*/,
      (byte) 156,
      (byte) 20,
      (byte) 143,
      (byte) 6,
      (byte) 98,
      (byte) 177,
      (byte) 164,
      (byte) 100,
      (byte) 155,
      (byte) 202,
      (byte) 59,
      (byte) 64 /*0x40*/,
      (byte) 162,
      (byte) 53,
      (byte) 43,
      (byte) 215,
      (byte) 154,
      (byte) 216,
      (byte) 181,
      (byte) 89,
      (byte) 102,
      (byte) 56,
      (byte) 203,
      (byte) 148,
      (byte) 107,
      (byte) 30,
      (byte) 98,
      (byte) 226,
      (byte) 57,
      (byte) 52,
      (byte) 93,
      (byte) 230,
      (byte) 218,
      (byte) 126,
      (byte) 142,
      (byte) 216,
      (byte) 219
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 251,
      (byte) 68,
      (byte) 70,
      (byte) 241,
      (byte) 16 /*0x10*/,
      (byte) 126,
      (byte) 33,
      (byte) 241,
      (byte) 190,
      (byte) 152,
      (byte) 17,
      (byte) 238,
      (byte) 44,
      (byte) 62,
      (byte) 70,
      (byte) 51,
      (byte) 50,
      (byte) 89,
      (byte) 103,
      (byte) 242,
      (byte) 160 /*0xA0*/,
      (byte) 54,
      (byte) 235,
      (byte) 218,
      (byte) 253,
      (byte) 235,
      (byte) 40,
      (byte) 25,
      (byte) 161,
      (byte) 132,
      (byte) 220,
      (byte) 197,
      (byte) 235,
      (byte) 230,
      (byte) 114,
      (byte) 212,
      (byte) 155,
      (byte) 49,
      (byte) 74,
      (byte) 212,
      (byte) 95,
      (byte) 87,
      (byte) 221,
      (byte) 90,
      (byte) 197,
      (byte) 204,
      (byte) 67,
      (byte) 138
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_14280(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 164,
      (byte) 1,
      (byte) 126,
      (byte) 128 /*0x80*/,
      (byte) 220,
      (byte) 150,
      (byte) 14,
      (byte) 223,
      (byte) 252,
      (byte) 28,
      (byte) 163,
      (byte) 130,
      (byte) 73,
      (byte) 99,
      (byte) 73,
      (byte) 251,
      (byte) 154,
      (byte) 68,
      (byte) 78,
      (byte) 193,
      (byte) 43,
      (byte) 102,
      (byte) 108,
      (byte) 84,
      (byte) 102,
      (byte) 35,
      (byte) 167,
      (byte) 82,
      (byte) 252,
      (byte) 108,
      (byte) 236,
      (byte) 233,
      (byte) 158,
      (byte) 193,
      (byte) 148,
      (byte) 201,
      (byte) 21,
      (byte) 235,
      (byte) 65,
      (byte) 204,
      (byte) 211,
      (byte) 89,
      (byte) 56,
      (byte) 65,
      (byte) 67,
      (byte) 87,
      (byte) 53,
      (byte) 42
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[32 /*0x20*/] = (byte) 247;
    sourceArray2[29] = (byte) 192 /*0xC0*/;
    sourceArray2[5] = (byte) 85;
    sourceArray2[40] = (byte) 15;
    sourceArray2[4] = (byte) 152;
    sourceArray2[46] = (byte) 237;
    sourceArray2[6] = (byte) 220;
    sourceArray2[18] = (byte) 57;
    sourceArray2[9] = (byte) 96 /*0x60*/;
    sourceArray2[24] = (byte) 163;
    sourceArray2[10] = (byte) 154;
    sourceArray2[36] = (byte) 94;
    sourceArray2[1] = (byte) 0;
    sourceArray2[13] = (byte) 82;
    sourceArray2[14] = (byte) 211;
    sourceArray2[15] = (byte) 159;
    sourceArray2[16 /*0x10*/] = (byte) 200;
    sourceArray2[0] = (byte) 68;
    sourceArray2[20] = (byte) 202;
    sourceArray2[17] = (byte) 172;
    sourceArray2[26] = (byte) 121;
    sourceArray2[11] = (byte) 68;
    sourceArray2[22] = (byte) 93;
    sourceArray2[23] = (byte) 212;
    sourceArray2[47] = (byte) 180;
    sourceArray2[44] = (byte) 39;
    sourceArray2[12] = (byte) 93;
    sourceArray2[2] = (byte) 148;
    sourceArray2[28] = (byte) 59;
    sourceArray2[30] = (byte) 199;
    sourceArray2[27] = (byte) 130;
    sourceArray2[31 /*0x1F*/] = (byte) 122;
    sourceArray2[3] = (byte) 48 /*0x30*/;
    sourceArray2[33] = (byte) 28;
    sourceArray2[34] = (byte) 67;
    sourceArray2[19] = (byte) 181;
    sourceArray2[35] = (byte) 5;
    sourceArray2[37] = (byte) 238;
    sourceArray2[21] = (byte) 22;
    sourceArray2[43] = (byte) 158;
    sourceArray2[38] = (byte) 223;
    sourceArray2[41] = (byte) 17;
    sourceArray2[42] = (byte) 204;
    sourceArray2[8] = (byte) 180;
    sourceArray2[7] = (byte) 14;
    sourceArray2[45] = (byte) 199;
    sourceArray2[39] = (byte) 135;
    sourceArray2[25] = (byte) 234;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_14281(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 172,
      (byte) 90,
      (byte) 15,
      (byte) 250,
      (byte) 229,
      (byte) 165,
      (byte) 249,
      (byte) 10,
      (byte) 168,
      (byte) 14,
      (byte) 107,
      (byte) 24,
      (byte) 10,
      (byte) 202,
      (byte) 113,
      (byte) 248,
      (byte) 198,
      (byte) 182,
      (byte) 234,
      (byte) 76,
      (byte) 63 /*0x3F*/,
      (byte) 65,
      (byte) 120,
      (byte) 75,
      (byte) 249,
      (byte) 220,
      (byte) 121,
      (byte) 165,
      (byte) 214,
      (byte) 90,
      (byte) 134,
      (byte) 23,
      (byte) 134,
      (byte) 155,
      (byte) 65,
      (byte) 34,
      (byte) 180,
      (byte) 34,
      (byte) 243,
      (byte) 159,
      (byte) 115,
      (byte) 154,
      (byte) 158,
      (byte) 71,
      (byte) 200,
      (byte) 211,
      (byte) 199,
      (byte) 189
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 196,
      (byte) 190,
      (byte) 159,
      (byte) 187,
      (byte) 86,
      (byte) 168,
      (byte) 129,
      (byte) 167,
      (byte) 215,
      (byte) 66,
      (byte) 245,
      (byte) 113,
      (byte) 189,
      (byte) 185,
      (byte) 137,
      (byte) 174,
      (byte) 191,
      (byte) 209,
      (byte) 34,
      (byte) 161,
      (byte) 6,
      (byte) 247,
      (byte) 204,
      (byte) 200,
      (byte) 100,
      (byte) 123,
      (byte) 216,
      (byte) 235,
      (byte) 48 /*0x30*/,
      (byte) 203,
      (byte) 177,
      (byte) 179,
      (byte) 186,
      (byte) 104,
      (byte) 75,
      (byte) 218,
      (byte) 122,
      (byte) 61,
      (byte) 135,
      (byte) 244,
      (byte) 248,
      (byte) 65,
      (byte) 20,
      (byte) 243,
      (byte) 64 /*0x40*/,
      (byte) 101,
      (byte) 11,
      (byte) 52
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[53];
    byte[] response2 = new byte[53];
    Array.Copy((Array) sc_14238.sspq, 395, (Array) numArray2, 0, 53);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_14238.sspr, 395, (Array) numArray2, 0, 53);
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

  internal static int ssp_appserver_14282(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[26] = (byte) 13;
    sourceArray1[42] = (byte) 2;
    sourceArray1[2] = (byte) 143;
    sourceArray1[33] = (byte) 101;
    sourceArray1[4] = (byte) 177;
    sourceArray1[27] = (byte) 228;
    sourceArray1[6] = (byte) 218;
    sourceArray1[7] = (byte) 242;
    sourceArray1[29] = (byte) 135;
    sourceArray1[10] = (byte) 124;
    sourceArray1[24] = (byte) 82;
    sourceArray1[9] = (byte) 25;
    sourceArray1[8] = (byte) 112 /*0x70*/;
    sourceArray1[36] = (byte) 217;
    sourceArray1[23] = (byte) 62;
    sourceArray1[18] = (byte) 84;
    sourceArray1[16 /*0x10*/] = (byte) 179;
    sourceArray1[17] = (byte) 16 /*0x10*/;
    sourceArray1[37] = (byte) 14;
    sourceArray1[19] = (byte) 130;
    sourceArray1[1] = (byte) 101;
    sourceArray1[13] = (byte) 100;
    sourceArray1[22] = (byte) 185;
    sourceArray1[31 /*0x1F*/] = (byte) 220;
    sourceArray1[44] = (byte) 234;
    sourceArray1[25] = (byte) 113;
    sourceArray1[11] = (byte) 162;
    sourceArray1[21] = (byte) 216;
    sourceArray1[12] = (byte) 244;
    sourceArray1[32 /*0x20*/] = (byte) 159;
    sourceArray1[30] = (byte) 31 /*0x1F*/;
    sourceArray1[5] = (byte) 66;
    sourceArray1[40] = (byte) 71;
    sourceArray1[34] = (byte) 18;
    sourceArray1[14] = (byte) 73;
    sourceArray1[35] = (byte) 227;
    sourceArray1[20] = (byte) 215;
    sourceArray1[28] = (byte) 189;
    sourceArray1[38] = (byte) 142;
    sourceArray1[0] = (byte) 221;
    sourceArray1[39] = (byte) 173;
    sourceArray1[41] = (byte) 248;
    sourceArray1[3] = (byte) 200;
    sourceArray1[43] = (byte) 6;
    sourceArray1[45] = (byte) 173;
    sourceArray1[15] = (byte) 6;
    sourceArray1[46] = (byte) 202;
    sourceArray1[47] = (byte) 16 /*0x10*/;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 152,
      (byte) 110,
      (byte) 117,
      (byte) 40,
      (byte) 48 /*0x30*/,
      (byte) 185,
      (byte) 207,
      (byte) 128 /*0x80*/,
      (byte) 229,
      (byte) 219,
      (byte) 204,
      (byte) 71,
      (byte) 6,
      (byte) 109,
      (byte) 98,
      (byte) 198,
      (byte) 9,
      (byte) 83,
      (byte) 71,
      (byte) 42,
      (byte) 195,
      (byte) 198,
      (byte) 118,
      (byte) 102,
      (byte) 143,
      (byte) 35,
      (byte) 196,
      (byte) 120,
      (byte) 30,
      (byte) 170,
      (byte) 11,
      (byte) 99,
      (byte) 95,
      (byte) 159,
      (byte) 64 /*0x40*/,
      (byte) 24,
      (byte) 209,
      (byte) 82,
      (byte) 89,
      (byte) 157,
      (byte) 239,
      (byte) 93,
      (byte) 227,
      (byte) 221,
      (byte) 118,
      (byte) 252,
      (byte) 22,
      (byte) 72
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_14283()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 169,
        (byte) 59,
        (byte) 189,
        (byte) 145,
        (byte) 36,
        (byte) 220,
        (byte) 137,
        (byte) 223,
        (byte) 78,
        (byte) 121
      };
      byte[] numArray3 = new byte[10]
      {
        (byte) 37,
        (byte) 14,
        (byte) 58,
        (byte) 153,
        (byte) 23,
        (byte) 109,
        (byte) 215,
        (byte) 200,
        (byte) 171,
        (byte) 254
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
      (byte) 208 /*0xD0*/,
      (byte) 152,
      (byte) 55,
      (byte) 8,
      (byte) 202,
      (byte) 227,
      (byte) 228,
      (byte) 74,
      (byte) 237,
      (byte) 129
    };
    byte[] numArray6 = new byte[10]
    {
      (byte) 42,
      (byte) 26,
      (byte) 81,
      (byte) 168,
      (byte) 51,
      (byte) 25,
      (byte) 98,
      (byte) 169,
      (byte) 254,
      (byte) 118
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static int ssp_appserver_14284(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 46,
      (byte) 232,
      (byte) 27,
      (byte) 189,
      (byte) 165,
      (byte) 9,
      (byte) 251,
      (byte) 108,
      (byte) 63 /*0x3F*/,
      (byte) 91,
      (byte) 71,
      (byte) 41,
      (byte) 139,
      (byte) 118,
      (byte) 199,
      (byte) 148,
      (byte) 222,
      (byte) 61,
      (byte) 139,
      (byte) 200,
      (byte) 182,
      (byte) 64 /*0x40*/,
      (byte) 73,
      (byte) 248,
      (byte) 52,
      (byte) 40,
      (byte) 0,
      (byte) 188,
      (byte) 185,
      (byte) 120,
      (byte) 98,
      (byte) 126,
      (byte) 207,
      (byte) 86,
      (byte) 176 /*0xB0*/,
      (byte) 101,
      (byte) 93,
      (byte) 112 /*0x70*/,
      (byte) 84,
      (byte) 243,
      (byte) 213,
      (byte) 44,
      (byte) 247,
      (byte) 172,
      (byte) 131,
      (byte) 186,
      (byte) 89,
      (byte) 211
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[32 /*0x20*/] = (byte) 27;
    sourceArray2[16 /*0x10*/] = (byte) 223;
    sourceArray2[2] = (byte) 146;
    sourceArray2[3] = (byte) 46;
    sourceArray2[28] = (byte) 73;
    sourceArray2[5] = (byte) 6;
    sourceArray2[0] = (byte) 194;
    sourceArray2[24] = (byte) 97;
    sourceArray2[7] = (byte) 187;
    sourceArray2[11] = (byte) 52;
    sourceArray2[10] = (byte) 45;
    sourceArray2[4] = (byte) 132;
    sourceArray2[45] = (byte) 48 /*0x30*/;
    sourceArray2[44] = (byte) 232;
    sourceArray2[14] = (byte) 213;
    sourceArray2[15] = (byte) 18;
    sourceArray2[41] = (byte) 195;
    sourceArray2[17] = (byte) 84;
    sourceArray2[18] = (byte) 124;
    sourceArray2[19] = (byte) 117;
    sourceArray2[31 /*0x1F*/] = (byte) 20;
    sourceArray2[21] = (byte) 35;
    sourceArray2[8] = (byte) 209;
    sourceArray2[23] = (byte) 24;
    sourceArray2[9] = (byte) 239;
    sourceArray2[25] = (byte) 193;
    sourceArray2[47] = (byte) 34;
    sourceArray2[27] = (byte) 95;
    sourceArray2[33] = (byte) 62;
    sourceArray2[29] = (byte) 194;
    sourceArray2[30] = (byte) 89;
    sourceArray2[34] = (byte) 9;
    sourceArray2[13] = (byte) 88;
    sourceArray2[26] = (byte) 178;
    sourceArray2[46] = (byte) 181;
    sourceArray2[35] = (byte) 151;
    sourceArray2[36] = (byte) 113;
    sourceArray2[20] = (byte) 219;
    sourceArray2[38] = (byte) 192 /*0xC0*/;
    sourceArray2[39] = (byte) 163;
    sourceArray2[22] = (byte) 43;
    sourceArray2[12] = (byte) 21;
    sourceArray2[40] = (byte) 19;
    sourceArray2[6] = (byte) 84;
    sourceArray2[1] = (byte) 220;
    sourceArray2[37] = (byte) 247;
    sourceArray2[43] = (byte) 159;
    sourceArray2[42] = (byte) 230;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[11];
    byte[] response2 = new byte[11];
    Array.Copy((Array) sc_14238.sspq, 448, (Array) numArray2, 0, 11);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_14238.sspr, 448, (Array) numArray2, 0, 11);
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

  internal static int ssp_appserver_14285(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 15,
      (byte) 173,
      (byte) 220,
      (byte) 167,
      (byte) 231,
      (byte) 181,
      (byte) 136,
      (byte) 99,
      (byte) 206,
      (byte) 89,
      (byte) 3,
      (byte) 154,
      (byte) 250,
      (byte) 135,
      (byte) 18,
      (byte) 172,
      (byte) 91,
      (byte) 16 /*0x10*/,
      (byte) 25,
      (byte) 234,
      (byte) 186,
      (byte) 25,
      (byte) 170,
      (byte) 30,
      (byte) 211,
      (byte) 20,
      (byte) 26,
      (byte) 105,
      (byte) 225,
      (byte) 57,
      (byte) 232,
      (byte) 30,
      (byte) 198,
      (byte) 143,
      (byte) 215,
      (byte) 41,
      (byte) 14,
      (byte) 239,
      (byte) 146,
      (byte) 122,
      (byte) 42,
      (byte) 87,
      (byte) 145,
      (byte) 203,
      (byte) 10,
      (byte) 41,
      (byte) 188,
      (byte) 112 /*0x70*/
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[5] = (byte) 65;
    sourceArray2[4] = (byte) 78;
    sourceArray2[2] = (byte) 114;
    sourceArray2[3] = (byte) 105;
    sourceArray2[16 /*0x10*/] = (byte) 220;
    sourceArray2[20] = (byte) 146;
    sourceArray2[6] = (byte) 163;
    sourceArray2[22] = (byte) 123;
    sourceArray2[28] = (byte) 206;
    sourceArray2[1] = (byte) 139;
    sourceArray2[47] = (byte) 192 /*0xC0*/;
    sourceArray2[26] = (byte) 103;
    sourceArray2[25] = (byte) 195;
    sourceArray2[45] = (byte) 161;
    sourceArray2[14] = (byte) 121;
    sourceArray2[15] = (byte) 187;
    sourceArray2[44] = (byte) 126;
    sourceArray2[17] = (byte) 65;
    sourceArray2[11] = (byte) 200;
    sourceArray2[19] = (byte) 66;
    sourceArray2[35] = (byte) 88;
    sourceArray2[0] = (byte) 137;
    sourceArray2[18] = (byte) 50;
    sourceArray2[7] = (byte) 95;
    sourceArray2[24] = (byte) 243;
    sourceArray2[42] = (byte) 31 /*0x1F*/;
    sourceArray2[8] = (byte) 131;
    sourceArray2[27] = (byte) 175;
    sourceArray2[33] = (byte) 19;
    sourceArray2[29] = (byte) 179;
    sourceArray2[30] = (byte) 202;
    sourceArray2[21] = (byte) 29;
    sourceArray2[32 /*0x20*/] = (byte) 4;
    sourceArray2[23] = (byte) 63 /*0x3F*/;
    sourceArray2[34] = (byte) 155;
    sourceArray2[9] = (byte) 133;
    sourceArray2[36] = (byte) 34;
    sourceArray2[37] = (byte) 232;
    sourceArray2[39] = (byte) 6;
    sourceArray2[31 /*0x1F*/] = (byte) 106;
    sourceArray2[13] = (byte) 60;
    sourceArray2[41] = (byte) 165;
    sourceArray2[12] = (byte) 217;
    sourceArray2[43] = (byte) 203;
    sourceArray2[38] = (byte) 130;
    sourceArray2[40] = (byte) 101;
    sourceArray2[46] = (byte) 30;
    sourceArray2[10] = (byte) 161;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_14286()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10];
      numArray2[3] = (byte) 12;
      numArray2[1] = (byte) 81;
      numArray2[7] = (byte) 237;
      numArray2[5] = (byte) 138;
      numArray2[2] = (byte) 192 /*0xC0*/;
      numArray2[9] = (byte) 142;
      numArray2[6] = (byte) 99;
      numArray2[0] = (byte) 85;
      numArray2[8] = (byte) 85;
      numArray2[4] = (byte) 57;
      byte[] numArray3 = new byte[10];
      numArray3[7] = (byte) 47;
      numArray3[1] = (byte) 218;
      numArray3[3] = (byte) 20;
      numArray3[5] = (byte) 72;
      numArray3[4] = (byte) 11;
      numArray3[9] = (byte) 53;
      numArray3[6] = (byte) 179;
      numArray3[2] = (byte) 240 /*0xF0*/;
      numArray3[8] = (byte) 226;
      numArray3[0] = (byte) 202;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10];
    numArray5[8] = (byte) 165;
    numArray5[6] = (byte) 185;
    numArray5[3] = (byte) 233;
    numArray5[0] = (byte) 239;
    numArray5[4] = (byte) 103;
    numArray5[9] = (byte) 90;
    numArray5[5] = (byte) 209;
    numArray5[7] = (byte) 164;
    numArray5[1] = (byte) 85;
    numArray5[2] = (byte) 5;
    byte[] numArray6 = new byte[10]
    {
      (byte) 33,
      (byte) 131,
      (byte) 189,
      (byte) 170,
      (byte) 179,
      (byte) 156,
      (byte) 26,
      (byte) 246,
      (byte) 226,
      (byte) 181
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static int ssp_appserver_14287(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 73,
      (byte) 236,
      (byte) 58,
      (byte) 163,
      (byte) 45,
      (byte) 160 /*0xA0*/,
      byte.MaxValue,
      (byte) 226,
      (byte) 160 /*0xA0*/,
      (byte) 185,
      (byte) 211,
      (byte) 118,
      (byte) 7,
      (byte) 67,
      (byte) 167,
      (byte) 152,
      (byte) 197,
      (byte) 173,
      (byte) 201,
      (byte) 190,
      (byte) 89,
      (byte) 193,
      (byte) 164,
      (byte) 92,
      (byte) 124,
      (byte) 209,
      (byte) 85,
      (byte) 51,
      (byte) 223,
      (byte) 147,
      (byte) 29,
      (byte) 36,
      (byte) 129,
      (byte) 247,
      (byte) 30,
      (byte) 228,
      (byte) 126,
      (byte) 0,
      (byte) 119,
      (byte) 185,
      (byte) 15,
      (byte) 145,
      (byte) 96 /*0x60*/,
      (byte) 239,
      (byte) 31 /*0x1F*/,
      (byte) 203,
      (byte) 230,
      (byte) 28
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 151,
      (byte) 183,
      (byte) 172,
      (byte) 222,
      (byte) 75,
      (byte) 79,
      (byte) 9,
      (byte) 31 /*0x1F*/,
      (byte) 166,
      (byte) 130,
      (byte) 118,
      (byte) 241,
      (byte) 149,
      (byte) 12,
      (byte) 166,
      (byte) 5,
      (byte) 143,
      (byte) 148,
      (byte) 99,
      (byte) 8,
      (byte) 190,
      (byte) 137,
      (byte) 165,
      (byte) 153,
      (byte) 215,
      (byte) 4,
      (byte) 61,
      (byte) 177,
      (byte) 224 /*0xE0*/,
      (byte) 181,
      (byte) 12,
      (byte) 116,
      (byte) 82,
      (byte) 25,
      (byte) 13,
      (byte) 94,
      (byte) 160 /*0xA0*/,
      (byte) 68,
      (byte) 159,
      (byte) 236,
      (byte) 198,
      (byte) 139,
      (byte) 27,
      (byte) 97,
      (byte) 251,
      (byte) 65,
      (byte) 251,
      (byte) 198
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_14288(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 197,
      (byte) 152,
      (byte) 135,
      (byte) 71,
      (byte) 221,
      (byte) 38,
      (byte) 91,
      (byte) 75,
      (byte) 204,
      (byte) 3,
      (byte) 13,
      (byte) 155,
      (byte) 175,
      (byte) 245,
      (byte) 15,
      (byte) 36,
      (byte) 194,
      (byte) 25,
      (byte) 197,
      (byte) 53,
      (byte) 15,
      (byte) 175,
      (byte) 200,
      (byte) 97,
      (byte) 181,
      (byte) 61,
      (byte) 177,
      (byte) 235,
      (byte) 56,
      (byte) 126,
      (byte) 203,
      (byte) 104,
      (byte) 248,
      (byte) 188,
      (byte) 188,
      (byte) 94,
      (byte) 150,
      (byte) 156,
      (byte) 161,
      (byte) 217,
      (byte) 27,
      (byte) 110,
      (byte) 67,
      (byte) 141,
      (byte) 82,
      (byte) 83,
      (byte) 112 /*0x70*/,
      (byte) 214
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 176 /*0xB0*/,
      (byte) 47,
      (byte) 116,
      (byte) 142,
      (byte) 139,
      (byte) 164,
      (byte) 169,
      (byte) 250,
      (byte) 233,
      (byte) 109,
      (byte) 185,
      (byte) 62,
      (byte) 235,
      (byte) 249,
      (byte) 120,
      (byte) 193,
      (byte) 189,
      (byte) 169,
      (byte) 1,
      (byte) 94,
      (byte) 150,
      (byte) 39,
      (byte) 253,
      (byte) 128 /*0x80*/,
      (byte) 151,
      (byte) 148,
      (byte) 170,
      (byte) 98,
      (byte) 247,
      (byte) 186,
      (byte) 217,
      (byte) 75,
      (byte) 235,
      (byte) 133,
      (byte) 140,
      (byte) 190,
      (byte) 172,
      (byte) 49,
      (byte) 242,
      (byte) 0,
      (byte) 5,
      (byte) 171,
      (byte) 11,
      (byte) 23,
      (byte) 132,
      (byte) 22,
      (byte) 130,
      (byte) 0
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_14289(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 239,
      (byte) 88,
      (byte) 21,
      (byte) 208 /*0xD0*/,
      (byte) 38,
      (byte) 146,
      (byte) 159,
      (byte) 117,
      (byte) 112 /*0x70*/,
      (byte) 19,
      (byte) 77,
      (byte) 56,
      (byte) 165,
      (byte) 162,
      (byte) 242,
      (byte) 56,
      (byte) 177,
      (byte) 142,
      (byte) 38,
      (byte) 119,
      (byte) 235,
      (byte) 25,
      (byte) 161,
      (byte) 107,
      (byte) 58,
      (byte) 15,
      (byte) 97,
      (byte) 205,
      (byte) 218,
      (byte) 21,
      (byte) 64 /*0x40*/,
      (byte) 141,
      (byte) 209,
      (byte) 2,
      (byte) 8,
      (byte) 100,
      (byte) 166,
      (byte) 142,
      (byte) 112 /*0x70*/,
      (byte) 10,
      (byte) 1,
      (byte) 100,
      (byte) 171,
      (byte) 91,
      (byte) 246,
      (byte) 84,
      (byte) 173,
      (byte) 176 /*0xB0*/
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 253,
      (byte) 83,
      (byte) 91,
      (byte) 135,
      (byte) 26,
      (byte) 133,
      (byte) 194,
      (byte) 145,
      (byte) 224 /*0xE0*/,
      (byte) 51,
      (byte) 18,
      (byte) 100,
      (byte) 215,
      (byte) 118,
      (byte) 184,
      (byte) 25,
      (byte) 79,
      (byte) 54,
      (byte) 242,
      (byte) 10,
      (byte) 107,
      (byte) 215,
      (byte) 105,
      (byte) 165,
      (byte) 227,
      (byte) 249,
      (byte) 240 /*0xF0*/,
      (byte) 66,
      (byte) 91,
      (byte) 119,
      (byte) 188,
      (byte) 179,
      (byte) 133,
      (byte) 101,
      (byte) 158,
      (byte) 194,
      (byte) 28,
      (byte) 0,
      (byte) 11,
      (byte) 242,
      (byte) 45,
      (byte) 185,
      (byte) 238,
      (byte) 168,
      (byte) 40,
      (byte) 81,
      (byte) 52,
      (byte) 245
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_14290()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[11];
      byte[] numArray2 = new byte[11];
      numArray2[10] = (byte) 154;
      numArray2[4] = (byte) 183;
      numArray2[2] = (byte) 122;
      numArray2[3] = (byte) 214;
      numArray2[8] = (byte) 49;
      numArray2[5] = (byte) 169;
      numArray2[6] = (byte) 149;
      numArray2[7] = (byte) 23;
      numArray2[9] = (byte) 210;
      numArray2[0] = (byte) 203;
      numArray2[1] = (byte) 130;
      byte[] numArray3 = new byte[11]
      {
        (byte) 137,
        (byte) 25,
        (byte) 197,
        (byte) 141,
        (byte) 47,
        (byte) 17,
        (byte) 217,
        (byte) 121,
        (byte) 140,
        (byte) 168,
        (byte) 163
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 11);
      for (int index = 0; index < 11; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[45];
      byte[] response = new byte[45];
      Array.Copy((Array) sc_14238.sspq, 459, (Array) numArray4, 0, 45);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_14238.sspr, 459, (Array) numArray4, 0, 45);
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
    byte[] numArray5 = new byte[11];
    byte[] numArray6 = new byte[11]
    {
      (byte) 103,
      (byte) 226,
      (byte) 149,
      (byte) 42,
      (byte) 196,
      (byte) 66,
      (byte) 70,
      (byte) 190,
      (byte) 29,
      (byte) 189,
      (byte) 63 /*0x3F*/
    };
    byte[] numArray7 = new byte[11]
    {
      (byte) 3,
      (byte) 186,
      (byte) 214,
      (byte) 27,
      (byte) 232,
      (byte) 109,
      (byte) 199,
      (byte) 132,
      (byte) 192 /*0xC0*/,
      (byte) 50,
      (byte) 194
    };
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 11);
    for (int index = 0; index < 11; ++index)
      numArray5[index] ^= numArray7[index];
    byte[] numArray8 = new byte[23];
    byte[] response1 = new byte[23];
    Array.Copy((Array) sc_14238.sspq, 504, (Array) numArray8, 0, 23);
    key.Query(true, 335, numArray8, response1);
    Array.Copy((Array) sc_14238.sspr, 504, (Array) numArray8, 0, 23);
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

  internal static string ssp_appserver_14291()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[11];
      byte[] numArray2 = new byte[11];
      numArray2[8] = (byte) 230;
      numArray2[1] = (byte) 197;
      numArray2[2] = (byte) 52;
      numArray2[7] = (byte) 93;
      numArray2[3] = (byte) 74;
      numArray2[5] = (byte) 244;
      numArray2[10] = (byte) 44;
      numArray2[6] = (byte) 183;
      numArray2[0] = (byte) 108;
      numArray2[9] = (byte) 33;
      numArray2[4] = (byte) 179;
      byte[] numArray3 = new byte[11]
      {
        (byte) 148,
        (byte) 222,
        (byte) 141,
        (byte) 124,
        (byte) 232,
        (byte) 244,
        (byte) 124,
        (byte) 252,
        (byte) 101,
        (byte) 112 /*0x70*/,
        (byte) 231
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 11);
      for (int index = 0; index < 11; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[11];
    byte[] numArray5 = new byte[11]
    {
      (byte) 49,
      (byte) 6,
      (byte) 35,
      (byte) 103,
      (byte) 234,
      (byte) 84,
      (byte) 187,
      (byte) 21,
      (byte) 4,
      (byte) 166,
      (byte) 124
    };
    byte[] numArray6 = new byte[11];
    numArray6[3] = (byte) 63 /*0x3F*/;
    numArray6[1] = (byte) 231;
    numArray6[6] = (byte) 144 /*0x90*/;
    numArray6[0] = (byte) 138;
    numArray6[9] = (byte) 134;
    numArray6[5] = (byte) 79;
    numArray6[7] = (byte) 136;
    numArray6[4] = (byte) 117;
    numArray6[8] = (byte) 251;
    numArray6[2] = (byte) 162;
    numArray6[10] = (byte) 192 /*0xC0*/;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 11);
    for (int index = 0; index < 11; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_14292()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[11];
      byte[] numArray2 = new byte[11];
      numArray2[4] = (byte) 130;
      numArray2[1] = (byte) 170;
      numArray2[2] = (byte) 245;
      numArray2[5] = (byte) 42;
      numArray2[3] = (byte) 102;
      numArray2[8] = (byte) 130;
      numArray2[6] = (byte) 225;
      numArray2[7] = (byte) 19;
      numArray2[9] = (byte) 237;
      numArray2[10] = (byte) 148;
      numArray2[0] = (byte) 101;
      byte[] numArray3 = new byte[11]
      {
        (byte) 59,
        (byte) 142,
        (byte) 111,
        (byte) 192 /*0xC0*/,
        (byte) 14,
        (byte) 150,
        (byte) 169,
        (byte) 189,
        (byte) 158,
        (byte) 237,
        (byte) 54
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 11);
      for (int index = 0; index < 11; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[11];
    byte[] numArray5 = new byte[11]
    {
      (byte) 119,
      (byte) 15,
      (byte) 18,
      (byte) 206,
      (byte) 23,
      (byte) 184,
      (byte) 38,
      (byte) 251,
      (byte) 215,
      (byte) 238,
      (byte) 16 /*0x10*/
    };
    byte[] numArray6 = new byte[11];
    numArray6[4] = (byte) 170;
    numArray6[2] = (byte) 22;
    numArray6[3] = (byte) 196;
    numArray6[1] = byte.MaxValue;
    numArray6[9] = (byte) 132;
    numArray6[5] = (byte) 29;
    numArray6[0] = (byte) 53;
    numArray6[7] = (byte) 29;
    numArray6[8] = (byte) 213;
    numArray6[6] = (byte) 41;
    numArray6[10] = (byte) 37;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 11);
    for (int index = 0; index < 11; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static int ssp_appserver_14293(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 162,
      (byte) 72,
      (byte) 246,
      (byte) 70,
      (byte) 179,
      (byte) 163,
      (byte) 140,
      (byte) 10,
      (byte) 27,
      (byte) 207,
      (byte) 69,
      (byte) 106,
      (byte) 52,
      (byte) 109,
      (byte) 250,
      (byte) 36,
      (byte) 221,
      (byte) 96 /*0x60*/,
      (byte) 70,
      (byte) 207,
      (byte) 31 /*0x1F*/,
      (byte) 9,
      (byte) 163,
      (byte) 176 /*0xB0*/,
      (byte) 26,
      (byte) 16 /*0x10*/,
      (byte) 85,
      (byte) 136,
      (byte) 1,
      (byte) 81,
      (byte) 68,
      (byte) 203,
      (byte) 107,
      (byte) 174,
      (byte) 131,
      (byte) 178,
      (byte) 224 /*0xE0*/,
      (byte) 168,
      (byte) 93,
      (byte) 124,
      (byte) 17,
      (byte) 25,
      (byte) 54,
      (byte) 176 /*0xB0*/,
      (byte) 38,
      (byte) 154,
      (byte) 178,
      (byte) 142
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[6] = (byte) 32 /*0x20*/;
    sourceArray2[25] = (byte) 7;
    sourceArray2[16 /*0x10*/] = (byte) 211;
    sourceArray2[18] = (byte) 121;
    sourceArray2[34] = (byte) 115;
    sourceArray2[5] = (byte) 116;
    sourceArray2[2] = (byte) 141;
    sourceArray2[7] = (byte) 72;
    sourceArray2[17] = (byte) 73;
    sourceArray2[9] = (byte) 132;
    sourceArray2[10] = (byte) 129;
    sourceArray2[45] = (byte) 128 /*0x80*/;
    sourceArray2[35] = (byte) 166;
    sourceArray2[13] = (byte) 173;
    sourceArray2[14] = (byte) 62;
    sourceArray2[15] = (byte) 149;
    sourceArray2[27] = (byte) 106;
    sourceArray2[33] = (byte) 78;
    sourceArray2[23] = (byte) 21;
    sourceArray2[19] = (byte) 30;
    sourceArray2[30] = (byte) 167;
    sourceArray2[21] = (byte) 37;
    sourceArray2[22] = (byte) 160 /*0xA0*/;
    sourceArray2[8] = (byte) 10;
    sourceArray2[24] = (byte) 137;
    sourceArray2[46] = (byte) 17;
    sourceArray2[0] = (byte) 220;
    sourceArray2[32 /*0x20*/] = (byte) 36;
    sourceArray2[28] = (byte) 134;
    sourceArray2[38] = (byte) 144 /*0x90*/;
    sourceArray2[36] = (byte) 177;
    sourceArray2[31 /*0x1F*/] = (byte) 55;
    sourceArray2[40] = (byte) 6;
    sourceArray2[4] = (byte) 47;
    sourceArray2[41] = (byte) 216;
    sourceArray2[20] = (byte) 88;
    sourceArray2[1] = (byte) 109;
    sourceArray2[37] = (byte) 45;
    sourceArray2[12] = (byte) 77;
    sourceArray2[39] = (byte) 33;
    sourceArray2[26] = (byte) 161;
    sourceArray2[29] = (byte) 82;
    sourceArray2[42] = (byte) 55;
    sourceArray2[43] = (byte) 71;
    sourceArray2[44] = (byte) 230;
    sourceArray2[11] = (byte) 230;
    sourceArray2[3] = (byte) 96 /*0x60*/;
    sourceArray2[47] = (byte) 8;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_14294(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[2] = (byte) 14;
    sourceArray1[14] = (byte) 134;
    sourceArray1[40] = (byte) 29;
    sourceArray1[27] = (byte) 154;
    sourceArray1[32 /*0x20*/] = (byte) 220;
    sourceArray1[5] = (byte) 107;
    sourceArray1[47] = (byte) 117;
    sourceArray1[7] = (byte) 29;
    sourceArray1[11] = (byte) 189;
    sourceArray1[9] = (byte) 198;
    sourceArray1[10] = (byte) 178;
    sourceArray1[15] = (byte) 209;
    sourceArray1[16 /*0x10*/] = (byte) 89;
    sourceArray1[28] = (byte) 176 /*0xB0*/;
    sourceArray1[8] = (byte) 228;
    sourceArray1[38] = (byte) 198;
    sourceArray1[13] = (byte) 68;
    sourceArray1[26] = (byte) 72;
    sourceArray1[18] = (byte) 180;
    sourceArray1[0] = (byte) 43;
    sourceArray1[3] = (byte) 13;
    sourceArray1[21] = (byte) 195;
    sourceArray1[22] = (byte) 1;
    sourceArray1[23] = (byte) 58;
    sourceArray1[24] = (byte) 170;
    sourceArray1[1] = (byte) 163;
    sourceArray1[46] = (byte) 206;
    sourceArray1[17] = (byte) 87;
    sourceArray1[6] = (byte) 242;
    sourceArray1[29] = (byte) 246;
    sourceArray1[30] = (byte) 101;
    sourceArray1[31 /*0x1F*/] = (byte) 204;
    sourceArray1[33] = (byte) 226;
    sourceArray1[20] = (byte) 110;
    sourceArray1[34] = (byte) 50;
    sourceArray1[35] = (byte) 35;
    sourceArray1[36] = (byte) 35;
    sourceArray1[12] = (byte) 173;
    sourceArray1[25] = (byte) 185;
    sourceArray1[39] = (byte) 106;
    sourceArray1[4] = (byte) 170;
    sourceArray1[41] = (byte) 113;
    sourceArray1[42] = (byte) 222;
    sourceArray1[43] = (byte) 34;
    sourceArray1[44] = (byte) 90;
    sourceArray1[45] = (byte) 220;
    sourceArray1[19] = (byte) 106;
    sourceArray1[37] = (byte) 162;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 239,
      (byte) 184,
      (byte) 208 /*0xD0*/,
      (byte) 237,
      (byte) 228,
      (byte) 20,
      (byte) 250,
      (byte) 38,
      (byte) 13,
      (byte) 113,
      (byte) 70,
      (byte) 18,
      (byte) 85,
      (byte) 100,
      (byte) 108,
      (byte) 116,
      (byte) 24,
      (byte) 239,
      (byte) 181,
      (byte) 244,
      (byte) 237,
      (byte) 84,
      (byte) 214,
      (byte) 13,
      (byte) 55,
      (byte) 251,
      (byte) 55,
      (byte) 39,
      (byte) 86,
      (byte) 230,
      (byte) 117,
      (byte) 105,
      (byte) 237,
      (byte) 14,
      (byte) 139,
      (byte) 33,
      (byte) 194,
      (byte) 181,
      (byte) 142,
      (byte) 33,
      (byte) 208 /*0xD0*/,
      (byte) 23,
      (byte) 52,
      (byte) 197,
      (byte) 152,
      (byte) 138,
      (byte) 41,
      (byte) 236
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_14295(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[37] = (byte) 227;
    sourceArray1[36] = (byte) 171;
    sourceArray1[2] = (byte) 70;
    sourceArray1[19] = (byte) 137;
    sourceArray1[40] = (byte) 52;
    sourceArray1[5] = (byte) 132;
    sourceArray1[41] = (byte) 164;
    sourceArray1[35] = (byte) 111;
    sourceArray1[31 /*0x1F*/] = (byte) 28;
    sourceArray1[9] = (byte) 131;
    sourceArray1[45] = (byte) 91;
    sourceArray1[11] = (byte) 243;
    sourceArray1[29] = (byte) 248;
    sourceArray1[14] = (byte) 80 /*0x50*/;
    sourceArray1[6] = (byte) 39;
    sourceArray1[3] = (byte) 143;
    sourceArray1[16 /*0x10*/] = (byte) 250;
    sourceArray1[23] = (byte) 225;
    sourceArray1[18] = (byte) 16 /*0x10*/;
    sourceArray1[32 /*0x20*/] = (byte) 108;
    sourceArray1[20] = (byte) 70;
    sourceArray1[21] = (byte) 22;
    sourceArray1[7] = (byte) 245;
    sourceArray1[12] = (byte) 150;
    sourceArray1[24] = (byte) 94;
    sourceArray1[47] = (byte) 207;
    sourceArray1[26] = (byte) 187;
    sourceArray1[17] = (byte) 174;
    sourceArray1[28] = (byte) 135;
    sourceArray1[0] = (byte) 3;
    sourceArray1[30] = (byte) 244;
    sourceArray1[42] = (byte) 132;
    sourceArray1[33] = (byte) 208 /*0xD0*/;
    sourceArray1[22] = (byte) 72;
    sourceArray1[34] = (byte) 232;
    sourceArray1[25] = (byte) 120;
    sourceArray1[27] = (byte) 1;
    sourceArray1[10] = (byte) 196;
    sourceArray1[38] = (byte) 65;
    sourceArray1[39] = (byte) 204;
    sourceArray1[15] = (byte) 102;
    sourceArray1[4] = (byte) 254;
    sourceArray1[44] = (byte) 188;
    sourceArray1[43] = (byte) 70;
    sourceArray1[13] = (byte) 5;
    sourceArray1[1] = (byte) 99;
    sourceArray1[46] = (byte) 168;
    sourceArray1[8] = (byte) 48 /*0x30*/;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[38] = (byte) 132;
    sourceArray2[9] = (byte) 16 /*0x10*/;
    sourceArray2[2] = (byte) 20;
    sourceArray2[3] = (byte) 183;
    sourceArray2[39] = (byte) 189;
    sourceArray2[44] = (byte) 113;
    sourceArray2[29] = (byte) 23;
    sourceArray2[7] = (byte) 134;
    sourceArray2[12] = (byte) 209;
    sourceArray2[8] = (byte) 112 /*0x70*/;
    sourceArray2[17] = (byte) 194;
    sourceArray2[11] = (byte) 250;
    sourceArray2[47] = (byte) 131;
    sourceArray2[13] = (byte) 218;
    sourceArray2[25] = (byte) 37;
    sourceArray2[19] = (byte) 47;
    sourceArray2[21] = (byte) 39;
    sourceArray2[26] = (byte) 241;
    sourceArray2[18] = (byte) 9;
    sourceArray2[41] = (byte) 148;
    sourceArray2[20] = (byte) 125;
    sourceArray2[4] = (byte) 235;
    sourceArray2[22] = (byte) 100;
    sourceArray2[23] = (byte) 192 /*0xC0*/;
    sourceArray2[24] = (byte) 22;
    sourceArray2[1] = (byte) 134;
    sourceArray2[15] = (byte) 140;
    sourceArray2[27] = (byte) 17;
    sourceArray2[33] = (byte) 252;
    sourceArray2[6] = (byte) 78;
    sourceArray2[30] = (byte) 25;
    sourceArray2[31 /*0x1F*/] = (byte) 45;
    sourceArray2[32 /*0x20*/] = (byte) 135;
    sourceArray2[16 /*0x10*/] = (byte) 134;
    sourceArray2[36] = (byte) 125;
    sourceArray2[14] = (byte) 141;
    sourceArray2[10] = (byte) 240 /*0xF0*/;
    sourceArray2[35] = (byte) 133;
    sourceArray2[34] = (byte) 25;
    sourceArray2[28] = (byte) 61;
    sourceArray2[37] = (byte) 146;
    sourceArray2[0] = (byte) 67;
    sourceArray2[42] = (byte) 1;
    sourceArray2[43] = (byte) 116;
    sourceArray2[40] = (byte) 245;
    sourceArray2[45] = (byte) 54;
    sourceArray2[46] = (byte) 192 /*0xC0*/;
    sourceArray2[5] = (byte) 176 /*0xB0*/;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_14296(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[42] = (byte) 169;
    sourceArray1[33] = (byte) 170;
    sourceArray1[4] = (byte) 219;
    sourceArray1[3] = (byte) 4;
    sourceArray1[38] = (byte) 109;
    sourceArray1[5] = (byte) 217;
    sourceArray1[6] = (byte) 207;
    sourceArray1[7] = (byte) 57;
    sourceArray1[8] = (byte) 143;
    sourceArray1[9] = (byte) 137;
    sourceArray1[18] = (byte) 48 /*0x30*/;
    sourceArray1[14] = (byte) 78;
    sourceArray1[29] = (byte) 130;
    sourceArray1[11] = (byte) 143;
    sourceArray1[23] = (byte) 191;
    sourceArray1[15] = (byte) 168;
    sourceArray1[1] = (byte) 243;
    sourceArray1[10] = (byte) 47;
    sourceArray1[41] = (byte) 179;
    sourceArray1[19] = (byte) 0;
    sourceArray1[44] = (byte) 85;
    sourceArray1[12] = (byte) 35;
    sourceArray1[22] = (byte) 40;
    sourceArray1[43] = (byte) 105;
    sourceArray1[24] = (byte) 76;
    sourceArray1[13] = (byte) 44;
    sourceArray1[21] = (byte) 162;
    sourceArray1[27] = (byte) 23;
    sourceArray1[25] = (byte) 254;
    sourceArray1[16 /*0x10*/] = (byte) 21;
    sourceArray1[36] = (byte) 121;
    sourceArray1[32 /*0x20*/] = (byte) 224 /*0xE0*/;
    sourceArray1[28] = (byte) 162;
    sourceArray1[45] = (byte) 108;
    sourceArray1[34] = (byte) 24;
    sourceArray1[35] = (byte) 82;
    sourceArray1[0] = (byte) 39;
    sourceArray1[37] = (byte) 65;
    sourceArray1[20] = (byte) 200;
    sourceArray1[39] = (byte) 53;
    sourceArray1[40] = (byte) 94;
    sourceArray1[26] = (byte) 60;
    sourceArray1[31 /*0x1F*/] = (byte) 108;
    sourceArray1[2] = (byte) 242;
    sourceArray1[30] = (byte) 83;
    sourceArray1[17] = (byte) 208 /*0xD0*/;
    sourceArray1[46] = (byte) 186;
    sourceArray1[47] = (byte) 210;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 213,
      (byte) 51,
      (byte) 48 /*0x30*/,
      (byte) 181,
      (byte) 51,
      (byte) 22,
      (byte) 221,
      (byte) 179,
      (byte) 113,
      (byte) 187,
      (byte) 157,
      (byte) 93,
      (byte) 80 /*0x50*/,
      (byte) 121,
      (byte) 159,
      (byte) 1,
      (byte) 84,
      (byte) 136,
      (byte) 194,
      (byte) 22,
      (byte) 210,
      (byte) 127 /*0x7F*/,
      (byte) 220,
      (byte) 163,
      (byte) 66,
      (byte) 50,
      (byte) 217,
      (byte) 183,
      (byte) 250,
      (byte) 245,
      (byte) 33,
      (byte) 223,
      (byte) 11,
      (byte) 144 /*0x90*/,
      (byte) 167,
      (byte) 72,
      (byte) 34,
      (byte) 23,
      (byte) 188,
      (byte) 72,
      (byte) 205,
      (byte) 249,
      (byte) 152,
      (byte) 254,
      (byte) 168,
      (byte) 209,
      (byte) 164,
      (byte) 243
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
